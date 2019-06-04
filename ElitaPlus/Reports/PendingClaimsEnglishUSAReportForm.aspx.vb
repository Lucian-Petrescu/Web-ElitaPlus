Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Reports


    Partial Class PendingClaimsEnglishUSAReportForm
        Inherits ElitaPlusPage

#Region "Parameters"

        Public Structure ReportParams
            Public UserId As String
            Public dealerCode As String
            Public svcCenterCode As String
            Public numberActiveDays As String
            Public sortOrder As String
            Public langCode As String
            Public svcCenterName As String
        End Structure

#End Region

#Region "Constants"

        '   Private Const RPT_FILENAME As String = "Claims Following Service Warranty English (USA)_TEST7"
        Private Const RPT_FILENAME_WINDOW As String = "PENDING CLAIMS"
        Private Const RPT_FILENAME As String = "PendingClaimsEnglishUSA"
        Private Const RPT_FILENAME_EXPORT As String = "PendingClaimsEnglishUSA_Exp"

        Public Const CRYSTAL As String = "0"
        'Public Const PDF As String = "1"
        Public Const HTML As String = "2"
        Public Const CSV As String = "3"
        'Public Const EXCEL As String = "4"

        Public Const VIEW_REPORT As String = "1"
        Public Const PDF As String = "2"
        Public Const TAB As String = "3"
        Public Const EXCEL As String = "4"
        Public Const EXCEL_DATA_ONLY As String = "5"
        Public Const JAVA As String = "6"

        Public Const BY_NUMBER_PENDING_DAYS As String = "0"
        Public Const BY_CLAIM_NUMBER As String = "1"
        Public Const BY_SERVICE_CENTER_NAME As String = "2"

        Public Const SORT_BY_NUMBER_PENDING_DAYS As String = "A"
        Public Const SORT_BY_CLAIM_NUMBER As String = "C"
        Public Const SORT_BY_SERVICE_CENTER_NAME As String = "S"

        Public Const ALL As String = "*"
        Public Const NOTHING_SELECTED As Int16 = -1
        Public Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Public Const YES As String = "Y"
        Public Const NO As String = "N"

        Private Const TOTALPARAMS As Integer = 20  ' 24
        Private Const TOTALEXPPARAMS As Integer = 6  ' 8
        Private Const PARAMS_PER_REPORT As Integer = 7

        'Private Const TOTALPARAMS As Integer = 23  ' 24
        'Private Const TOTALEXPPARAMS As Integer = 7  ' 8
        'Private Const PARAMS_PER_REPORT As Integer = 8

        Protected WithEvents isProgressVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents isReportCeVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents moReportCeStatus As System.Web.UI.HtmlControls.HtmlInputHidden
        Public Const DEFAULT_NUMBER_PENDING_DAYS As String = "1 "

#End Region

#Region "variables"
        Private moReportFormat As ReportCeBaseForm.RptFormat
#End Region

#Region "Properties"
        Public ReadOnly Property TheRptCeInputControl() As ReportCeInputControl
            Get
                If moReportCeInputControl Is Nothing Then
                    moReportCeInputControl = CType(FindControl("moReportCeInputControl"), ReportCeInputControl)
                End If
                Return moReportCeInputControl
            End Get
        End Property
#End Region

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents RadiobuttonEXCEL As System.Web.UI.WebControls.RadioButton
        Protected WithEvents RadiobuttonAllClaims As System.Web.UI.WebControls.RadioButton
        Protected WithEvents RadiobuttonExcludeRepairedClaims As System.Web.UI.WebControls.RadioButton
        Protected WithEvents ErrorCtrl As ErrorController
        Protected WithEvents moReportCeInputControl As ReportCeInputControl

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Me.ErrorCtrl.Clear_Hide()
            Try
                If Not Me.IsPostBack Then
                    InitializeForm()

                End If
                '    Me.DisplayProgressBarOnClick(Me.btnGenRpt, "Loading_Claims")
                Me.InstallProgressBar()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
            Me.ShowMissingTranslations(Me.ErrorCtrl)
        End Sub

        Sub PopulateDealerDropDown()
            ' Me.BindListControlToDataView(Me.cboDealer, LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE"))
            Dim oDealerList = GetDealerListByCompanyForUser()
            Me.cboDealer.Populate(oDealerList, New PopulateOptions() With
                                               {
                                                .AddBlankItem = True
                                               })
        End Sub
        Private Function GetDealerListByCompanyForUser() As Assurant.Elita.CommonConfiguration.DataElements.ListItem()
            Dim Index As Integer
            Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext

            Dim UserCompanies As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies

            Dim oDealerList As New Collections.Generic.List(Of Assurant.Elita.CommonConfiguration.DataElements.ListItem)

            For Index = 0 To UserCompanies.Count - 1
                'UserCompanyList &= ",'" & GuidControl.GuidToHexString(UserCompanyies(Index))
                oListContext.CompanyId = UserCompanies(Index)
                Dim oDealerListForCompany As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerListByCompany", context:=oListContext)
                If oDealerListForCompany.Count > 0 Then
                    If Not oDealerList Is Nothing Then
                        oDealerList.AddRange(oDealerListForCompany)
                    Else
                        oDealerList = oDealerListForCompany.Clone()
                    End If

                End If
            Next

            Return oDealerList.ToArray()

        End Function
        Sub PopulateServiceCenterDropDown()
            'Me.BindListControlToDataView(Me.cboSvcCenter, LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID).Id), , , True) 'ServiceGroupByCountry
            Dim listcontext As ListContext = New ListContext()
            listcontext.CountryId = ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID).Id
            Dim svcCenterLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.ServiceCenterListByCountry, Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            Me.cboSvcCenter.Populate(svcCenterLkl, New PopulateOptions() With
           {
              .AddBlankItem = True
           })
            'Me.SetSelectedItem(Me.cboSearchServiceCenter, Me.State.selectedServiceCenterId)
        End Sub

        Private Sub InitializeForm()
            PopulateDealerDropDown()
            PopulateServiceCenterDropDown()
            Me.rsvccenter.Checked = True
            Me.rdealer.Checked = True
            ' Me.RadiobuttonAllClaims.Checked = True
            Me.rdReportSortOrder.Items(0).Selected = True
            Me.PopulateControlFromBOProperty(Me.txtActiveDays, Me.DEFAULT_NUMBER_PENDING_DAYS)
            'Dim re As RadioButton = CType(Me.moReportCeInputControl.FindControl("RadiobuttonEXCEL"), RadioButton)
            'me.rbEXCEL.
        End Sub

        Private Sub btnGenRpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRpt.Click
            Try
                Dim UserId As String = GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.Id)
                Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                Dim langCode As String = LookupListNew.GetCodeFromId("LANGUAGES", langId)
                Dim selectedDealerId As Guid = Me.GetSelectedItem(Me.cboDealer)
                Dim dealerDV As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE")
                Dim dealerCode As String = LookupListNew.GetCodeFromId(dealerDV, selectedDealerId)
                Dim selectedServiceCenterId As Guid = Me.GetSelectedItem(Me.cboSvcCenter)
                Dim svcCenterDV As DataView = LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID).Id)
                Dim svcCenterCode As String = LookupListNew.GetCodeFromId(svcCenterDV, selectedServiceCenterId)
                Dim svcCenterName As String = LookupListNew.GetDescriptionFromId(svcCenterDV, selectedServiceCenterId)
                Dim numberActiveDays As Integer = CType(Me.txtActiveDays.Text, Integer)
                Dim sortOrder As String
                Dim includeAllClaims As String = NO
                Dim params As ReportCeBaseForm.Params

                If Me.rdealer.Checked Then
                    dealerCode = ALL
                Else
                    If selectedDealerId.Equals(Guid.Empty) Then
                        Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                    End If
                End If

                If Me.rsvccenter.Checked Then
                    svcCenterCode = ALL
                    svcCenterName = " "
                Else
                    If selectedServiceCenterId.Equals(Guid.Empty) Then
                        Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                    End If
                End If

                If ((numberActiveDays < 0) OrElse (numberActiveDays > 999)) Then
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_NUMBER_OF_PENDING_DAYS_ERR)
                End If


                ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

                Select Case Me.rdReportSortOrder.SelectedValue()
                    Case BY_NUMBER_PENDING_DAYS
                        sortOrder = "A"
                    Case BY_CLAIM_NUMBER
                        sortOrder = "C"
                    Case BY_SERVICE_CENTER_NAME
                        sortOrder = "S"
                End Select
                moReportFormat = ReportCeBase.GetReportFormat(Me)
                If (moReportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (moReportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                    'Export Report
                    params = SetExpParameters(UserId, dealerCode, svcCenterCode, svcCenterName,
                                  numberActiveDays, sortOrder, langCode)
                Else
                    'View Report
                    params = SetParameters(UserId, dealerCode, svcCenterCode, svcCenterName,
                                  numberActiveDays, sortOrder, langCode)
                End If
                Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Sub SetReportParams(ByVal oReportParams As ReportParams, ByVal repParams() As ReportCeBaseForm.RptParam,
                            ByVal reportName As String, ByVal startIndex As Integer)
            With oReportParams
                repParams(startIndex) = New ReportCeBaseForm.RptParam("V_USER_KEY", .UserId, reportName)
                repParams(startIndex + 1) = New ReportCeBaseForm.RptParam("V_DEALER_CODE", .dealerCode, reportName)
                repParams(startIndex + 2) = New ReportCeBaseForm.RptParam("V_SVCCENTER_CODE", .svcCenterCode, reportName)
                repParams(startIndex + 3) = New ReportCeBaseForm.RptParam("V_NUMBER_PENDING_DAYS", .numberActiveDays, reportName)
                repParams(startIndex + 4) = New ReportCeBaseForm.RptParam("V_SORT_ORDER", .sortOrder, reportName)
                repParams(startIndex + 5) = New ReportCeBaseForm.RptParam("V_LANGUAGE_CODE", .langCode, reportName)
                repParams(startIndex + 6) = New ReportCeBaseForm.RptParam("P_SERVICE_CENTER_NAME", .svcCenterName, reportName)

            End With

        End Sub

        Function SetParameters(ByVal UserId As String, ByVal dealerCode As String, ByVal svcCenterCode As String,
                                 ByVal svcCenterName As String,
                                 ByVal numberActiveDays As Integer, ByVal sortOrder As String,
                                 ByVal langCode As String) As ReportCeBaseForm.Params

            Dim reportFormat As ReportCeBaseForm.RptFormat
            Dim reportName As String = RPT_FILENAME
            Dim params As New ReportCeBaseForm.Params
            Dim repParams(TOTALPARAMS) As ReportCeBaseForm.RptParam
            Dim oReportParams As ReportParams

            With oReportParams
                .UserId = UserId
                .dealerCode = dealerCode
                .svcCenterCode = svcCenterCode
                .numberActiveDays = numberActiveDays.ToString
                .sortOrder = sortOrder
                .langCode = langCode
                .svcCenterName = svcCenterName
            End With
            SetReportParams(oReportParams, repParams, String.Empty, PARAMS_PER_REPORT * 0)     ' Main Report

            With params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = moReportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With
            Return params
        End Function

        Function SetExpParameters(ByVal UserId As String, ByVal dealerCode As String, ByVal svcCenterCode As String,
                                 ByVal svcCenterName As String,
                                 ByVal numberActiveDays As Integer, ByVal sortOrder As String,
                                 ByVal langCode As String) As ReportCeBaseForm.Params

            Dim reportFormat As ReportCeBaseForm.RptFormat
            Dim reportName As String = RPT_FILENAME_EXPORT
            Dim params As New ReportCeBaseForm.Params
            Dim repParams(TOTALEXPPARAMS) As ReportCeBaseForm.RptParam
            Dim oReportParams As ReportParams

            With oReportParams
                .UserId = UserId
                .dealerCode = dealerCode
                .svcCenterCode = svcCenterCode
                .numberActiveDays = numberActiveDays.ToString
                .sortOrder = sortOrder
                .langCode = langCode
                .svcCenterName = svcCenterName
            End With
            SetReportParams(oReportParams, repParams, String.Empty, PARAMS_PER_REPORT * 0)     ' Main Report

            With params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = moReportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With


            Return params
        End Function

    End Class
End Namespace