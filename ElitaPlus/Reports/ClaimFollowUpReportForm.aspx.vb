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
    Partial Class ClaimFollowUpReportForm
        Inherits ElitaPlusPage


#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "CLAIM FOLLOW UP"
        Private Const RPT_FILENAME As String = "ClaimFollowUp"
        Private Const RPT_FILENAME_EXPORT As String = "ClaimFollowUp-Exp"

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
        '   Public Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Public Const YES As String = "Y"
        Public Const NO As String = "N"
        Protected WithEvents moReportCeStatus As System.Web.UI.HtmlControls.HtmlInputHidden
        Public Const None As Integer = 0

#End Region

#Region "variables"
        Dim moReportFormat As ReportCeBaseForm.RptFormat
        Dim reportName As String = RPT_FILENAME
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

#Region "Handlers"

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents isProgressVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents isReportCeVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents ErrorCtrl As ErrorController
        Protected WithEvents moReportCeInputControl As ReportCeInputControl

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
            PopulateDropDowns()
            Dim t As Date = Date.Now.AddDays(-1)
            moBeginFollowUpDateText.Text = GetDateFormattedString(t)
            moEndFollowUpDateText.Text = GetDateFormattedString(Date.Now)
            rDealer.Checked = True
            rSvcCtr.Checked = True
        End Sub

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            ErrorCtrl.Clear_Hide()
            Try
                If Not IsPostBack Then
                    InitializeForm()
                    TheRptCeInputControl.populateReportLanguages(RPT_FILENAME)
                    'Date Calendars
                    AddCalendar(BtnBeginFollowUpDate, moBeginFollowUpDateText)
                    AddCalendar(BtnEndFollowUpDate, moEndFollowUpDateText)
                Else
                    ClearErrLabels()
                End If
                InstallProgressBar()
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
            ShowMissingTranslations(ErrorCtrl)

        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub btnGenRpt_Click(sender As System.Object, e As System.EventArgs) Handles btnGenRpt.Click
            Try
                GenerateReport()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

#End Region

#End Region

#Region "Clear"

        Private Sub ClearErrLabels()
            ClearLabelErrSign(moBeginFollowUpDateLabel)
            ClearLabelErrSign(moEndFollowUpDateLabel)
            ClearLabelErrSign(moDealerLabel)
            ClearLabelErrSign(SvcCtrLabel)

        End Sub

#End Region

#Region "Populate"

        Private Sub PopulateDropDowns()
            PopulateDealerDropDown()
            PopulateSvcCtrDropDown()
        End Sub

        Private Sub PopulateDealerDropDown()
            'Me.BindListControlToDataView(Me.cboDealer, LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE"), , , True)
            Dim oDealerList = GetDealerListByCompanyForUser()
            cboDealer.Populate(oDealerList, New PopulateOptions() With
                                               {
                                                .AddBlankItem = True
                                                })


        End Sub

        Private Sub PopulateSvcCtrDropDown()
            'Me.BindListControlToDataView(Me.cboSvcCtr, LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Countries), , , True)
            Dim UserCountries As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Countries
            Dim Index As Integer
            Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
            Dim serviceCenterListLkl As New Collections.Generic.List(Of Assurant.Elita.CommonConfiguration.DataElements.ListItem)
            For Index = 0 To UserCountries.Count - 1
                'UserCompanyList &= ",'" & GuidControl.GuidToHexString(UserCompanyies(Index))
                oListContext.CountryId = UserCountries(Index)
                Dim oServiceCenter As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="ServiceCenterListByCountry", context:=oListContext)
                If oServiceCenter.Count > 0 Then
                    If serviceCenterListLkl IsNot Nothing Then
                        serviceCenterListLkl.AddRange(oServiceCenter)
                    Else
                        serviceCenterListLkl = oServiceCenter.Clone()
                    End If

                End If
            Next
            cboSvcCtr.Populate(serviceCenterListLkl.ToArray(), New PopulateOptions() With
        {
          .AddBlankItem = True
        })
            'Me.BindListControlToDataView(Me.cboSvcCtr, LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID).Id), , , True)
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
                    If oDealerList IsNot Nothing Then
                        oDealerList.AddRange(oDealerListForCompany)
                    Else
                        oDealerList = oDealerListForCompany.Clone()
                    End If

                End If
            Next

            Return oDealerList.ToArray()

        End Function

#End Region

#Region "Crystal Enterprise"

        Function SetParameters(userId As String, langCode As String, dealerCode As String, svcCtrCode As String, beginFollowUpDate As String,
                                  endFollowUpDate As String, createdby As String, sortBy As String) As ReportCeBaseForm.Params

            Dim reportFormat As ReportCeBaseForm.RptFormat
            Dim params As New ReportCeBaseForm.Params
            Dim culturecode As String = TheRptCeInputControl.getCultureValue(False)
            Dim reportName As String = TheRptCeInputControl.getReportName(RPT_FILENAME, False)

            reportFormat = ReportCeBase.GetReportFormat(Me)

            If (reportFormat = ReportCeBase.RptFormat.TEXT_TAB OrElse
              reportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                reportName = TheRptCeInputControl.getReportName(RPT_FILENAME_EXPORT, True)
                culturecode = TheRptCeInputControl.getCultureValue(True)
            End If

            rptWindowTitle.InnerText = TheRptCeInputControl.getReportWindowTitle(TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW))

            Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
                    {
                     New ReportCeBaseForm.RptParam("V_DEALER", dealerCode),
                     New ReportCeBaseForm.RptParam("V_SERVICE_CENTER_CODE", svcCtrCode),
                     New ReportCeBaseForm.RptParam("V_SORT_BY", sortBy),
                     New ReportCeBaseForm.RptParam("V_BEGIN_FOLLOW_UP_DATE", beginFollowUpDate),
                     New ReportCeBaseForm.RptParam("V_END_FOLLOW_UP_DATE", endFollowUpDate),
                     New ReportCeBaseForm.RptParam("V_USER_KEY", userId)}

            With params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = reportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With
            Return params
        End Function

        Private Sub GenerateReport()
            Dim userId As String = GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.Id)
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim langCode As String = LookupListNew.GetCodeFromId("LANGUAGES", langId)
            Dim selectedDealerId As Guid = GetSelectedItem(cboDealer)
            Dim dvDealer As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE")
            Dim dealerCode As String = LookupListNew.GetCodeFromId(dvDealer, selectedDealerId)
            Dim selectedSvcCtrId As Guid = GetSelectedItem(cboSvcCtr)
            'Dim dvSvcCtr As DataView = LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID).Id)
            Dim dvSvcCtr As DataView = LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Countries)
            Dim svcCtrCode As String = LookupListNew.GetCodeFromId(dvSvcCtr, selectedSvcCtrId)
            Dim svcCtrName As String = LookupListNew.GetDescriptionFromId(dvSvcCtr, selectedSvcCtrId)
            Dim dvRiskType As DataView = LookupListNew.GetRiskTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
            Dim dvRepairType As DataView = LookupListNew.GetMethodOfRepairLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            Dim sortBy As String = rdReportSortOrder.SelectedValue


            Dim dvCoverageType As DataView = LookupListNew.GetCoverageTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)

            Dim selectionType As Integer
            Dim endDate As String
            Dim beginDate As String
            Dim createdby As String

            'Dates
            ReportCeBase.ValidateBeginEndDate(moBeginFollowUpDateLabel, moBeginFollowUpDateText.Text, moEndFollowUpDateLabel, moEndFollowUpDateText.Text)
            endDate = ReportCeBase.FormatDate(moEndFollowUpDateLabel, moEndFollowUpDateText.Text)
            beginDate = ReportCeBase.FormatDate(moBeginFollowUpDateLabel, moBeginFollowUpDateText.Text)

            If rDealer.Checked Then
                dealerCode = ALL
            Else
                If selectedDealerId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(moDealerLabel)
                    Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                End If
            End If

            If rSvcCtr.Checked Then
                svcCtrCode = ALL
            Else
                If selectedSvcCtrId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(SvcCtrLabel)
                    Throw New GUIException(Message.MSG_INVALID_SERVICE_CENTER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_SERVICE_CENTER_MUST_BE_SELECTED_ERR)
                End If
            End If
            'End If


            ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

            Dim params As ReportCeBaseForm.Params = SetParameters(userId, langCode, dealerCode, svcCtrCode, beginDate, endDate, createdby, sortBy)
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params
        End Sub

#End Region


    End Class
End Namespace