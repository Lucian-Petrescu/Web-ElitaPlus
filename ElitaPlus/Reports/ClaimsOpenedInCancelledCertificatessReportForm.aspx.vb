Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Text.RegularExpressions
Imports System.Security.Cryptography.X509Certificates
Imports Assurant.ElitaPlus.DALObjects
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Reports
    Partial Public Class ClaimsOpenedInCancelledCertificatessReportForm
        Inherits ElitaPlusPage

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "CLAIMS_OPENED_IN_CANCELLED_CERTS"
        Private Const RPT_FILENAME As String = "ClaimsOpenedInCancelledCerts"
        Private Const RPT_FILENAME_EXPORT As String = "ClaimsOpenedInCancelledCerts-Exp"

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
        Public Const ALL As String = "*"
        Public Const NOTHING_SELECTED As Int16 = -1
        '  Public Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Public Const YES As String = "Y"
        Public Const NO As String = "N"

        Public Const PAGETITLE As String = "CLAIMS_OPENED_IN_CANCELLED_CERTS"
        Public Const PAGETAB As String = "REPORTS"
        Public Const DOCTITLE As String = "CLAIMS_OPENED_IN_CANCELLED_CERTS"
        Public Const DEALER_CODE As String = "0"
        Public Const DEALER_NAME As String = "1"

        Private Const TOTAL_PARAMS As Integer = 7
        Private Const TOTAL_EXP_PARAMS As Integer = 7
        Private Const PARAMS_PER_REPORT As Integer = 7

        Private Const LABEL_SELECT_DEALER As String = "OR A SINGLE DEALER"



#End Region

#Region "variables"
        Dim moReportFormat As ReportCeBaseForm.RptFormat
#End Region

#Region "Parameters"

        Public Structure ReportParams
            Public userid As String
            Public begindate As String
            Public enddate As String
            Public svcCode As String
            Public dealerCode As String
            Public createdby As String
            Public sortBy As String
            Public culturevalue As String
        End Structure

#End Region

#Region "Properties"

        Public ReadOnly Property DealerMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If moDealerMultipleDrop Is Nothing Then
                    moDealerMultipleDrop = CType(FindControl("moDealerMultipleDrop"), MultipleColumnDDLabelControl)
                End If
                Return moDealerMultipleDrop
            End Get
        End Property

#End Region

#Region "JavaScript"

        Public Sub JavascriptCalls()
            rbAllSvcCenters.Attributes.Add("onclick", "return ToggleSingleDropDownSelection('" + cboSvcCenter.ClientID + "','" + rbAllSvcCenters.ClientID + "',false);")
            cboSvcCenter.Attributes.Add("onchange", "return ToggleSingleDropDownSelection('" + cboSvcCenter.ClientID + "','" + rbAllSvcCenters.ClientID + "',true);")
            rbAllUsers.Attributes.Add("onclick", "return ToggleSingleTextBoxSelection('" + txtUserId.ClientID + "','" + rbAllUsers.ClientID + "',false);")
            txtUserId.Attributes.Add("onfocus", "return ToggleSingleTextBoxSelection('" + txtUserId.ClientID + "','" + rbAllUsers.ClientID + "',true);")
        End Sub
#End Region

#Region "Web Form Designer Generated Code "
        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents Label1 As System.Web.UI.WebControls.Label
        Protected WithEvents isReportCeVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents isProgressVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents moReportCeStatus As System.Web.UI.HtmlControls.HtmlInputHidden
        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub
#End Region

#Region "Handlers-Init"

        Private Sub InitializeForm()
            Dim t As Date = Date.Now.AddDays(-1)
            Me.BeginDateText.Text = GetDateFormattedString(t)
            Me.EndDateText.Text = GetDateFormattedString(Date.Now)
            PopulateSvcCtrDropDown()
            PopulateDealerDropDown()
            Me.rbAllSvcCenters.Checked = True
            Me.rdealer.Checked = True
            Me.rbAllUsers.Checked = True
            TheReportCeInputControl.populateReportLanguages(RPT_FILENAME)
        End Sub

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Me.ErrControllerMaster.Clear_Hide()
            Me.Title = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
            Try
                If Not Me.IsPostBack Then
                    Me.SetFormTitle(PAGETITLE)
                    Me.SetFormTab(PAGETAB)
                    JavascriptCalls()
                    InitializeForm()
                    Me.AddCalendar(Me.BtnBeginDate, Me.BeginDateText)
                    Me.AddCalendar(Me.BtnEndDate, Me.EndDateText)
                Else
                    ClearErrLabels()
                End If
                Me.InstallProgressBar()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
            Me.ShowMissingTranslations(Me.ErrControllerMaster)
        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub btnGenRpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRpt.Click
            Try
                GenerateReport()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub


#End Region

#Region "Handlers-DropDown"

#End Region

#Region "Clear"

        Private Sub ClearErrLabels()
            Me.ClearLabelErrSign(BeginDateLabel)
            Me.ClearLabelErrSign(EndDateLabel)
        End Sub

#End Region

#Region "Populate"

        Private Sub PopulateSvcCtrDropDown()
            '  Me.BindListControlToDataView(Me.cboSvcCenter, LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Countries), , , True)
            Dim UserCountries As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Countries
            Dim Index As Integer
            Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
            Dim serviceCenterListLkl As New Collections.Generic.List(Of Assurant.Elita.CommonConfiguration.DataElements.ListItem)
            For Index = 0 To UserCountries.Count - 1
                'UserCompanyList &= ",'" & GuidControl.GuidToHexString(UserCompanyies(Index))
                oListContext.CountryId = UserCountries(Index)
                Dim oServiceCenter As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="ServiceCenterListByCountry", context:=oListContext)
                If oServiceCenter.Count > 0 Then
                    If Not serviceCenterListLkl Is Nothing Then
                        serviceCenterListLkl.AddRange(oServiceCenter)
                    Else
                        serviceCenterListLkl = oServiceCenter.Clone()
                    End If

                End If
            Next
            Me.cboSvcCenter.Populate(serviceCenterListLkl.ToArray(), New PopulateOptions() With
        {
          .AddBlankItem = True
        })
            'Me.BindListControlToDataView(Me.cboSvcCtr, LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID).Id), , , True)
        End Sub

        Private Sub PopulateDealerDropDown()

            Dim dv As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
            DealerMultipleDrop.NothingSelected = True
            DealerMultipleDrop.SetControl(False, DealerMultipleDrop.MODES.NEW_MODE, True, dv, TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER), True, True, " document.forms[0]." + rdealer.ClientID + ".checked = false;")

        End Sub

#End Region

#Region "Crystal Enterprise"

        Function SetParameters(ByVal userId As String, ByVal begindate As String, ByVal enddate As String, _
                               ByVal svcCtrCode As String, ByVal dealerCode As String, ByVal createdby As String, _
                               ByVal sortBy As String) As ReportCeBaseForm.Params


            Dim params As New ReportCeBaseForm.Params
            Dim repParams(TOTAL_PARAMS) As ReportCeBaseForm.RptParam
            Dim exportData As String = NO
            Dim culturevalue As String
            Dim reportName As String
            Dim rptParams As ReportParams

            moReportFormat = ReportCeBase.GetReportFormat(Me)
            If (moReportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (moReportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then

                culturevalue = TheReportCeInputControl.getCultureValue(True)
                reportName = TheReportCeInputControl.getReportName(RPT_FILENAME_EXPORT, True)
            Else

                culturevalue = TheReportCeInputControl.getCultureValue(False)
                reportName = TheReportCeInputControl.getReportName(RPT_FILENAME, False)
            End If

            With rptParams
                .userid = userId
                .begindate = begindate
                .enddate = enddate
                .svcCode = svcCtrCode
                .dealerCode = dealerCode
                .createdby = createdby
                .sortBy = sortBy
                .culturevalue = culturevalue
            End With

            SetReportParams(rptParams, repParams, String.Empty, PARAMS_PER_REPORT * 0)  ' Main Report

            With params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = moReportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With
            Return params
        End Function

        Sub SetReportParams(ByVal rptParams As ReportParams, ByVal repParams() As ReportCeBaseForm.RptParam, _
                          ByVal rptName As String, ByVal startIndex As Integer)

            With rptParams
                repParams(startIndex) = New ReportCeBaseForm.RptParam("V_USER_KEY", .userid, rptName)
                repParams(startIndex + 1) = New ReportCeBaseForm.RptParam("V_BEGIN_DATE", .begindate, rptName)
                repParams(startIndex + 2) = New ReportCeBaseForm.RptParam("V_END_DATE", .enddate, rptName)
                repParams(startIndex + 3) = New ReportCeBaseForm.RptParam("V_SVC_CODE", .svcCode, rptName)
                repParams(startIndex + 4) = New ReportCeBaseForm.RptParam("V_DEALER", .dealerCode, rptName)
                repParams(startIndex + 5) = New ReportCeBaseForm.RptParam("V_CREATED_BY", .createdby, rptName)
                repParams(startIndex + 6) = New ReportCeBaseForm.RptParam("V_SORT_BY", .sortBy, rptName)
                repParams(startIndex + 7) = New ReportCeBaseForm.RptParam("LANG_CULTURE_VALUE", .culturevalue, rptName)
            End With

        End Sub

        Private Sub GenerateReport()
            Dim userId As String = GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.Id)
            Dim endDate As String
            Dim beginDate As String
            Dim createdby As String
            Dim svcCtrCode As String
            Dim riskTypeDesc As String
            Dim selectedDealerId As Guid = DealerMultipleDrop.SelectedGuid
            Dim dealerCode As String = DealerMultipleDrop.SelectedCode
            Dim dealerDesc As String = DealerMultipleDrop.SelectedDesc
            Dim sortBy As String = Me.rdReportSortOrder.SelectedValue

            'Dates
            ReportCeBase.ValidateBeginEndDate(BeginDateLabel, BeginDateText.Text, EndDateLabel, EndDateText.Text)
            endDate = ReportCeBase.FormatDate(EndDateLabel, EndDateText.Text)
            beginDate = ReportCeBase.FormatDate(BeginDateLabel, BeginDateText.Text)



            'Dealer
            If Me.rdealer.Checked Then
                dealerCode = ALL
            Else
                If selectedDealerId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(DealerMultipleDrop.CaptionLabel)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                End If
            End If

            'SvcCenter
            If Me.rbAllSvcCenters.Checked Then
                svcCtrCode = ALL
            Else
                Dim selectedSvcCtrId As Guid = Me.GetSelectedItem(Me.cboSvcCenter)
                Dim dvSvcCtr As DataView = LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID).Id)
                svcCtrCode = LookupListNew.GetCodeFromId(dvSvcCtr, selectedSvcCtrId)
            End If

            'User
            If Me.rbAllUsers.Checked Then
                createdby = ALL
            Else
                createdby = txtUserId.Text.Trim.ToString()
                If createdby = String.Empty Then
                    ElitaPlusPage.SetLabelError(lblUserId)
                    Throw New GUIException(Message.MSG_INVALID_USER_ID, Assurant.ElitaPlus.Common.ErrorCodes.GUI_USER_MUST_BE_ENTERED_ERR)
                End If
            End If

            Dim params As ReportCeBaseForm.Params
            ReportCeBase.EnableReportCe(Me, TheReportCeInputControl)

            params = SetParameters(userId, beginDate, endDate, svcCtrCode, dealerCode, createdby, sortBy)

            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

        End Sub

#End Region

    End Class
End Namespace
