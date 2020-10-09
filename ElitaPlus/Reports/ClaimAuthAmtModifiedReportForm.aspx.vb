Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Microsoft.VisualBasic
Imports System.Globalization
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.Web.Forms

Namespace Reports
    Partial Public Class ClaimAuthAmtModifiedReportForm
        Inherits ElitaPlusPage

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "CLAIM_AUTHORIZED_AMOUNT_MODIFIED"
        Private Const RPT_FILENAME As String = "ClaimsWithAuthorizedAmountModified"
        Private Const RPT_FILENAME_EXPORT As String = "ClaimsWithAuthorizedAmountModified-Exp"

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

        Public Const PAGETITLE As String = "CLAIM_AUTHORIZED_AMOUNT_MODIFIED"
        Public Const PAGETAB As String = "REPORTS"
        Public Const DOCTITLE As String = "CLAIM_AUTHORIZED_AMOUNT_MODIFIED"
        Public Const DEALER_CODE As String = "0"
        Public Const DEALER_NAME As String = "1"

        Private Const TOTAL_PARAMS As Integer = 7
        Private Const TOTAL_EXP_PARAMS As Integer = 7
        Private Const PARAMS_PER_REPORT As Integer = 7


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
            Public riskTypeDesc As String
            Public modifiedby As String
            Public sortBy As String
            Public culturevalue As String
        End Structure

#End Region

#Region "Properties"

#End Region

#Region "JavaScript"

        Public Sub JavascriptCalls()
            rbAllSvcCenters.Attributes.Add("onclick", "return ToggleSingleDropDownSelection('" + cboSvcCenter.ClientID + "','" + rbAllSvcCenters.ClientID + "',false);")
            cboSvcCenter.Attributes.Add("onchange", "return ToggleSingleDropDownSelection('" + cboSvcCenter.ClientID + "','" + rbAllSvcCenters.ClientID + "',true);")
            rbAllRisktypes.Attributes.Add("onclick", "return ToggleSingleDropDownSelection('" + cborisktype.ClientID + "','" + rbAllRisktypes.ClientID + "',false);")
            cborisktype.Attributes.Add("onchange", "return ToggleSingleDropDownSelection('" + cborisktype.ClientID + "','" + rbAllRisktypes.ClientID + "',true);")
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

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub
#End Region

#Region "Handlers-Init"

        Private Sub InitializeForm()
            Dim t As Date = Date.Now.AddDays(-1)
            BeginDateText.Text = GetDateFormattedString(t)
            EndDateText.Text = GetDateFormattedString(Date.Now)
            PopulateSvcCtrDropDown()
            PopulateRiskTypeDropDown()
            rbAllSvcCenters.Checked = True
            rbAllRisktypes.Checked = True
            rbAllUsers.Checked = True
            TheReportCeInputControl.populateReportLanguages(RPT_FILENAME)
        End Sub

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            ErrControllerMaster.Clear_Hide()
            Title = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
            Try
                If Not IsPostBack Then
                    SetFormTitle(PAGETITLE)
                    SetFormTab(PAGETAB)
                    JavascriptCalls()
                    InitializeForm()
                    AddCalendar(BtnBeginDate, BeginDateText)
                    AddCalendar(BtnEndDate, EndDateText)
                Else
                    ClearErrLabels()
                End If
                InstallProgressBar()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
            ShowMissingTranslations(ErrControllerMaster)
        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub btnGenRpt_Click(sender As System.Object, e As System.EventArgs) Handles btnGenRpt.Click
            Try
                GenerateReport()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub


#End Region

#Region "Handlers-DropDown"

#End Region

#Region "Clear"

        Private Sub ClearErrLabels()
            ClearLabelErrSign(BeginDateLabel)
            ClearLabelErrSign(EndDateLabel)
        End Sub

#End Region

#Region "Populate"

        Private Sub PopulateSvcCtrDropDown()
            Dim countryList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="Country")

            Dim filteredCountryList As DataElements.ListItem() = (From x In countryList
                                                                  Where ElitaPlusIdentity.Current.ActiveUser.Countries.Contains(x.ListItemId)
                                                                  Select x).ToArray()
            Dim oServiceCenterList As New Collections.Generic.List(Of Assurant.Elita.CommonConfiguration.DataElements.ListItem)
            Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
            For Each _country As Guid In ElitaPlusIdentity.Current.ActiveUser.Countries
                oListContext.CountryId = _country
                Dim oServiceCenterListForCountry As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="ServiceCenterListByCountry", context:=oListContext)
                If oServiceCenterListForCountry.Count > 0 Then
                    If oServiceCenterList IsNot Nothing Then
                        oServiceCenterList.AddRange(oServiceCenterListForCountry)
                    Else
                        oServiceCenterList = oServiceCenterListForCountry.Clone()
                    End If
                End If
            Next

            cboSvcCenter.Populate(oServiceCenterList.ToArray(), New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = True
                                                   })
            'Me.BindListControlToDataView(Me.cboSvcCenter, LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Countries), , , True)
        End Sub

        Private Sub PopulateRiskTypeDropDown()
            Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
            oListContext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            Dim coverageLst As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="RiskTypeByCompanyGroup", context:=oListContext)
            cborisktype.Populate(coverageLst, New PopulateOptions() With
                {
                    .AddBlankItem = True
                })

            'Me.BindListControlToDataView(Me.cborisktype, LookupListNew.GetRiskTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id), , , True)
        End Sub

#End Region

#Region "Crystal Enterprise"

        Function SetParameters(userId As String, begindate As String, enddate As String,
                               svcCtrCode As String, riskTypeDescription As String, modifiedby As String,
                               sortBy As String) As ReportCeBaseForm.Params


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
                .riskTypeDesc = riskTypeDescription
                .modifiedby = modifiedby
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

        Sub SetReportParams(rptParams As ReportParams, repParams() As ReportCeBaseForm.RptParam,
                          rptName As String, startIndex As Integer)

            With rptParams
                repParams(startIndex) = New ReportCeBaseForm.RptParam("V_USER_KEY", .userid, rptName)
                repParams(startIndex + 1) = New ReportCeBaseForm.RptParam("V_BEGIN_DATE", .begindate, rptName)
                repParams(startIndex + 2) = New ReportCeBaseForm.RptParam("V_END_DATE", .enddate, rptName)
                repParams(startIndex + 3) = New ReportCeBaseForm.RptParam("V_SVC_CODE", .svcCode, rptName)
                repParams(startIndex + 4) = New ReportCeBaseForm.RptParam("V_RISK_TYPE", .riskTypeDesc, rptName)
                repParams(startIndex + 5) = New ReportCeBaseForm.RptParam("V_MODIFIED_BY", .modifiedby, rptName)
                repParams(startIndex + 6) = New ReportCeBaseForm.RptParam("V_SORT_BY", .sortBy, rptName)
                repParams(startIndex + 7) = New ReportCeBaseForm.RptParam("LANG_CULTURE_VALUE", .culturevalue, rptName)
            End With

        End Sub

        Private Sub GenerateReport()
            Dim userId As String = GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.Id)
            Dim endDate As String
            Dim beginDate As String
            Dim modifiedby As String
            Dim svcCtrCode As String
            Dim riskTypeDesc As String
            'Dates
            ReportCeBase.ValidateBeginEndDate(BeginDateLabel, BeginDateText.Text, EndDateLabel, EndDateText.Text)
            endDate = ReportCeBase.FormatDate(EndDateLabel, EndDateText.Text)
            beginDate = ReportCeBase.FormatDate(BeginDateLabel, BeginDateText.Text)
            Dim sortBy As String = rdReportSortOrder.SelectedValue


            'SvcCenter
            If rbAllSvcCenters.Checked Then
                svcCtrCode = ALL
            Else
                Dim selectedSvcCtrId As Guid = GetSelectedItem(cboSvcCenter)
                Dim dvSvcCtr As DataView = LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID).Id)
                svcCtrCode = LookupListNew.GetCodeFromId(dvSvcCtr, selectedSvcCtrId)
            End If

            'Risktype
            If rbAllRisktypes.Checked Then
                riskTypeDesc = ALL
            Else
                Dim selectedRiskTypeId As Guid = GetSelectedItem(cborisktype)
                Dim dvRiskType As DataView = LookupListNew.GetRiskTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
                riskTypeDesc = LookupListNew.GetDescriptionFromId(dvRiskType, selectedRiskTypeId)
            End If

            'User
            If rbAllUsers.Checked Then
                modifiedby = ALL
            Else
                modifiedby = txtUserId.Text.Trim.ToString()
                If modifiedby = String.Empty Then
                    ElitaPlusPage.SetLabelError(lblUserId)
                    Throw New GUIException(Message.MSG_INVALID_USER_ID, Assurant.ElitaPlus.Common.ErrorCodes.GUI_USER_MUST_BE_ENTERED_ERR)
                End If
            End If

            Dim params As ReportCeBaseForm.Params
            ReportCeBase.EnableReportCe(Me, TheReportCeInputControl)

            params = SetParameters(userId, beginDate, endDate, svcCtrCode, riskTypeDesc, modifiedby, sortBy)

            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

        End Sub

#End Region

    End Class
End Namespace
