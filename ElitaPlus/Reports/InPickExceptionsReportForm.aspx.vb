Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Microsoft.VisualBasic
Namespace Reports
    Partial Public Class InPickExceptionsReportForm
        Inherits ElitaPlusPage
#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "CLAIMS_INPICK_EXCEPTIONS"
        Private Const RPT_FILENAME As String = "ClaimsInPickExceptions"
        Public Const ALL As String = "*"
        Public Const YES As String = "Y"
        Protected WithEvents btnViewHidden As System.Web.UI.WebControls.Button
        Public Const NO As String = "N"
        Private Const RPT_FILENAME_EXPORT As String = "ClaimsInPickExceptions-Exp"
        Private Const LABEL_SELECT_DEALER As String = "OR A SINGLE DEALER"
        Private Const LABEL_SELECT_COMPANY As String = "SELECT_COMPANY"
        Private Const TOTALPARAMS As Integer = 6  ' 23
        Private Const TOTALEXPPARAMS As Integer = 6  ' 7
        Private Const PARAMS_PER_REPORT As Integer = 6 '8
        Private Const ONE_ITEM As Integer = 1
        Public Const DEFAULT_NUMBER_ACTIVE_DAYS As String = "3"
        Public Const BY_SERVICE_CENTER As String = "By Service Center"
        Public Const BY_STORE As String = "By Store"
#End Region

#Region "parameters"
        Public Structure ReportParams
            Public companycode As String
            Public companyDesc As String
            Public dealerCode As String
            Public dealerDesc As String
            Public numberActiveDays As String
            Public selectionType As String
            Public culturevalue As String
        End Structure
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
        Public ReadOnly Property DealerMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If moDealerMultipleDrop Is Nothing Then
                    moDealerMultipleDrop = CType(FindControl("moDealerMultipleDrop"), MultipleColumnDDLabelControl)
                End If
                Return moDealerMultipleDrop
            End Get
        End Property
        Public ReadOnly Property UserCompanyMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If moUserCompanyMultipleDrop Is Nothing Then
                    moUserCompanyMultipleDrop = CType(FindControl("moUserCompanyMultipleDrop"), MultipleColumnDDLabelControl)
                End If
                Return moUserCompanyMultipleDrop
            End Get
        End Property
#End Region

#Region "Handlers"


#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Private designerPlaceholderDeclaration As System.Object
        Private currentAccountingMonth As Integer
        Private currentAccountingYear As Integer


        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Handlers-Init"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Me.ErrorCtrl.Clear_Hide()
            Try
                If Not Me.IsPostBack Then
                    InitializeForm()
                Else
                    ClearErrLabels()
                    EnableorDisableControls()
                End If
                Me.InstallProgressBar()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
            Me.ShowMissingTranslations(Me.ErrorCtrl)
        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub btnGenRpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRpt.Click
            Try
                GenerateReport()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

#End Region

#Region "Handlers-DropDown"

        Private Sub OnFromDrop_Changed(ByVal fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
            Handles moUserCompanyMultipleDrop.SelectedDropChanged
            Try
                PopulateDealerDropDown()
            Catch ex As Exception
                HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

#End Region

#End Region

#Region "Clear"

        Private Sub ClearErrLabels()
            ' Me.ClearLabelErrSign(MonthYearLabel)
            Me.ClearLabelErrSign(moDaysActiveLabel)
            Me.ClearLabelErrSign(UserCompanyMultipleDrop.CaptionLabel)
            Me.ClearLabelErrSign(DealerMultipleDrop.CaptionLabel)
            If Me.rdealer.Checked Then DealerMultipleDrop.SelectedIndex = -1
        End Sub
        Public Sub EnableorDisableControls()
            If Me.rSvcenter.Checked = True Then
                Me.txtActiveDays.Enabled = False
                Me.txtActiveDays.Text = String.Empty
            Else
                Me.txtActiveDays.Enabled = True
            End If
        End Sub

#End Region

#Region "Populate"

        Private Sub PopulateCompaniesDropdown()
            Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()

            UserCompanyMultipleDrop.NothingSelected = True
            UserCompanyMultipleDrop.SetControl(True, UserCompanyMultipleDrop.MODES.NEW_MODE, True, dv, "* " + TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY), True)
            If dv.Count.Equals(ONE_ITEM) Then
                HideHtmlElement("ddSeparator")
                UserCompanyMultipleDrop.SelectedIndex = Me.ONE_ITEM
                UserCompanyMultipleDrop.Visible = False

            End If
        End Sub

        Private Sub PopulateDealerDropDown()
            Dim dv As DataView = LookupListNew.GetDealerLookupList(New Guid(UserCompanyMultipleDrop.SelectedGuid.ToString))
            DealerMultipleDrop.NothingSelected = True
            DealerMultipleDrop.SetControl(False,
                                              DealerMultipleDrop.MODES.NEW_MODE,
                                              True,
                                              dv,
                                              TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER),
                                              True,
                                              False,
                                              " document.forms[0].rdealer.checked = false;",
                                              "moDealerMultipleDrop_moMultipleColumnDrop",
                                              "moDealerMultipleDrop_moMultipleColumnDropDesc", "moDealerMultipleDrop_lb_DropDown")

        End Sub

        Private Sub InitializeForm()
            PopulateCompaniesDropdown()
            PopulateDealerDropDown()
            Me.rdealer.Checked = True
            Me.rSvcenter.Checked = True
            Me.txtActiveDays.Enabled = False
            Me.txtActiveDays.Text = String.Empty
            Me.PopulateControlFromBOProperty(Me.txtActiveDays, Me.DEFAULT_NUMBER_ACTIVE_DAYS)
            TheRptCeInputControl.populateReportLanguages(RPT_FILENAME)
        End Sub

#End Region

#Region "Crystal Enterprise"

        Function SetParameters(ByVal CompanyCode As String, ByVal CompanyDesc As String, ByVal dealerCode As String, ByVal dealerDesc As String,
                                  ByVal numberActiveDays As Integer, ByVal selectionType As String) As ReportCeBaseForm.Params

            Dim Params As New ReportCeBaseForm.Params
            Dim repParams(TOTALPARAMS) As ReportCeBaseForm.RptParam
            Dim rptParams As ReportParams
            moReportFormat = ReportCeBase.GetReportFormat(Me)
            Dim culturevalue As String = TheRptCeInputControl.getCultureValue(False, GuidControl.GuidToHexString(UserCompanyMultipleDrop.SelectedGuid))
            Dim reportName As String = TheRptCeInputControl.getReportName(RPT_FILENAME, False)

            With rptParams
                .companycode = CompanyCode
                .companyDesc = CompanyDesc
                .dealerCode = dealerCode
                .dealerDesc = dealerDesc
                .numberActiveDays = numberActiveDays.ToString
                .selectionType = selectionType
                .culturevalue = culturevalue
            End With
            SetReportParams(rptParams, repParams, String.Empty, PARAMS_PER_REPORT * 0)     ' Main Report

            Me.rptWindowTitle.InnerText = TheRptCeInputControl.getReportWindowTitle(TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW))

            With Params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = moReportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With

            Return Params

        End Function
        Function SetExpParameters(ByVal CompanyCode As String, ByVal CompanyDesc As String, ByVal dealerCode As String, ByVal dealerDesc As String,
                                  ByVal numberActiveDays As Integer, ByVal selectionType As String) As ReportCeBaseForm.Params

            'Dim reportName As String = RPT_FILENAME_EXPORT
            Dim Params As New ReportCeBaseForm.Params
            Dim repParams(TOTALEXPPARAMS) As ReportCeBaseForm.RptParam
            Dim rptParams As ReportParams
            moReportFormat = ReportCeBase.GetReportFormat(Me)
            Dim culturevalue As String = TheRptCeInputControl.getCultureValue(True, GuidControl.GuidToHexString(UserCompanyMultipleDrop.SelectedGuid))
            Dim reportName As String = TheRptCeInputControl.getReportName(RPT_FILENAME_EXPORT, True)
            '    rptParams.isDetail = "Y"


            With rptParams
                .companycode = CompanyCode
                .companyDesc = CompanyDesc
                .dealerCode = dealerCode
                .dealerDesc = dealerDesc
                .numberActiveDays = numberActiveDays.ToString
                .selectionType = selectionType
                .culturevalue = culturevalue
            End With
            SetReportParams(rptParams, repParams, String.Empty, PARAMS_PER_REPORT * 0)     ' Main Report

            Me.rptWindowTitle.InnerText = TheRptCeInputControl.getReportWindowTitle(TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW))

            With Params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = moReportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With

            Return Params
        End Function

        Sub SetReportParams(ByVal rptParams As ReportParams, ByVal repParams() As ReportCeBaseForm.RptParam,
                          ByVal reportName As String, ByVal startIndex As Integer)

            With rptParams
                repParams(startIndex) = New ReportCeBaseForm.RptParam("V_COMPANY", .companycode, reportName)
                repParams(startIndex + 1) = New ReportCeBaseForm.RptParam("V_COMPANY_DESC", .companyDesc, reportName)
                repParams(startIndex + 2) = New ReportCeBaseForm.RptParam("V_DEALER", .dealerCode, reportName)
                repParams(startIndex + 3) = New ReportCeBaseForm.RptParam("V_DEALER_DESC", .dealerDesc, reportName)
                repParams(startIndex + 4) = New ReportCeBaseForm.RptParam("V_DAYS", .numberActiveDays, reportName)
                repParams(startIndex + 5) = New ReportCeBaseForm.RptParam("V_TYPE", .selectionType, reportName)
                repParams(startIndex + 6) = New ReportCeBaseForm.RptParam("LANG_CULTURE_VALUE", .culturevalue, reportName)
            End With
            '  End If
        End Sub
        Private Sub GenerateReport()
            Dim userId As Guid = ElitaPlusIdentity.Current.ActiveUser.Id
            Dim selectedDealerId As Guid = DealerMultipleDrop.SelectedGuid
            Dim dealerCode As String = DealerMultipleDrop.SelectedCode
            Dim dealerDesc As String = DealerMultipleDrop.SelectedDesc
            Dim numberActiveDays As Integer = 0
            Dim CompanyId As String = GuidControl.GuidToHexString(UserCompanyMultipleDrop.SelectedGuid)
            Dim CompanyCode As String = UserCompanyMultipleDrop.SelectedCode
            Dim CompanyDesc As String = UserCompanyMultipleDrop.SelectedDesc
            Dim selectionType As String
            Dim params As ReportCeBaseForm.Params


            'Validating the Company selection
            If UserCompanyMultipleDrop.SelectedGuid.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(UserCompanyMultipleDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
            End If


            'Dealer
            If Me.rdealer.Checked Then
                dealerCode = ALL
            Else
                If selectedDealerId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(DealerMultipleDrop.CaptionLabel)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                End If
            End If

            If rSvcenter.Checked = True Then
                selectionType = BY_SERVICE_CENTER
            Else
                selectionType = BY_STORE
            End If


            If selectionType = BY_STORE Then
                If txtActiveDays.Text.Trim.ToString = String.Empty Or Not IsNumeric(txtActiveDays.Text.Trim.ToString) Then
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_NUMBER_OF_ACTIVE_DAYS_ERR)
                Else
                    numberActiveDays = CType(Me.txtActiveDays.Text, Integer)
                    If ((numberActiveDays < 0) OrElse (numberActiveDays > 999)) Then
                        Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_NUMBER_OF_ACTIVE_DAYS_ERR)
                    End If
                End If
            End If

            ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

            moReportFormat = ReportCeBase.GetReportFormat(Me)
            If (moReportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (moReportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                'Export 
                params = SetExpParameters(CompanyCode, CompanyDesc, dealerCode, dealerDesc, numberActiveDays, selectionType)
            Else
                'View Report
                params = SetParameters(CompanyCode, CompanyDesc, dealerCode, dealerDesc, numberActiveDays, selectionType)
            End If
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

        End Sub

#End Region
    End Class
End Namespace