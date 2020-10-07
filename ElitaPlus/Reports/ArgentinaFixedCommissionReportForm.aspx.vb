Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.Web.Forms
Namespace Reports

    Partial Class ArgentinaFixedCommissionReportForm
        Inherits ElitaPlusPage


#Region "Parameters"

#End Region

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "FIXED_COMMISSION"
        'Private Const RPT_FILENAME As String = "BillingAndCollection"
        'Private Const RPT_FILENAME_EXPORT As String = "BillingAndCollection-EXP"
        Private Const RPT_FILENAME As String = "FixedCommissionReport"
        Private Const RPT_FILENAME_EXPORT As String = "FixedCommissionReport-Exp"
        Private Const LABEL_SELECT_DEALER As String = "Dealer"

        Public Const ALL As String = "*"
        Public Const NOTHING_SELECTED As Int16 = -1
        Public Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Public Const YES As String = "Y"
        Public Const NO As String = "N"

        Public Const PAGETITLE As String = "FIXED_COMMISSION"
        Public Const PAGETAB As String = "REPORTS"
        Public Const DOCTITLE As String = "FIXED_COMMISSION"

#End Region

#Region "variables"
        Dim moReportFormat As ReportCeBaseForm.RptFormat
#End Region

#Region "Properties"
        Public ReadOnly Property DealerMultipleDrop() As MultipleColumnDDLabelControl_New
            Get
                If multipleDropControl Is Nothing Then
                    multipleDropControl = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl_New)
                End If
                Return multipleDropControl
            End Get
        End Property

#End Region

#Region "Handlers-DropDown"

#End Region

#Region " Web Form Designer Generated Code "
        Protected WithEvents multipleDropControl As MultipleColumnDDLabelControl_New
        Protected WithEvents UsercontrolAvailableSelectedDealers As Global.Assurant.ElitaPlus.ElitaPlusWebApp.Generic.UserControlAvailableSelected_New

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            MasterPage.MessageController.Clear_Hide()
            ClearLabelsErrSign()
            Title = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
            Try
                If Not IsPostBack Then
                    SetFormTitle(PAGETITLE)
                    SetFormTab(PAGETAB)
                    MasterPage.UsePageTabTitleInBreadCrum = False
                    UpdateBreadCrum()

                    InitializeForm()
                    AddCalendar(BtnBeginDate, txtBeginDate)
                    AddCalendar(BtnEndDate, txtEndDate)
                End If
                InstallProgressBar()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub
        Public Sub ClearLabelsErrSign()
            Try
                ClearLabelErrSign(lblEndDate)
                ClearLabelErrSign(lblBeginDate)
                ClearLabelErrSign(DealerMultipleDrop.CaptionLabel)
                If rdealer.Checked Then DealerMultipleDrop.SelectedIndex = -1
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Sub populateDealersList()
            Dim dv As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE")

            UsercontrolAvailableSelectedDealers.SetAvailableData(dv, LookupListNew.COL_CODE_NAME, LookupListNew.COL_ID_NAME)
            ControlMgr.SetVisibleControl(Me, UsercontrolAvailableSelectedDealers, True)
        End Sub

        Sub PopulateDealerDropDown()
            Dim dv As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
            DealerMultipleDrop.NothingSelected = True
            DealerMultipleDrop.SetControl(False, DealerMultipleDrop.MODES.NEW_MODE, True, dv, "  " + TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER), True, True, "document.getElementById('ctl00_BodyPlaceHolder_moDealerGroupList').selectedIndex = 0;document.getElementById('ctl00_BodyPlaceHolder_rdealer').checked = false;RemoveAllSelectedDealersForReports('ctl00_BodyPlaceHolder_UsercontrolAvailableSelectedDealers');")
        End Sub

        Sub PopulateDealerGroupDropDown()
            'Dim _dal As New AcctSettingDAL
            'Dim ds As DataSet
            'ds = _dal.LoadDealerGroups("", "", ElitaPlusIdentity.Current.ActiveUser.Company.CompanyGroupId)
            'Me.BindListControlToDataView(Me.moDealerGroupList, ds.Tables(0).DefaultView, , "Dealer_Group_ID", True)

            Dim DealerGroups As DataElements.ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:="DealerGroupByCompanyGroup",
                                                                context:=New ListContext() With
                                                                {
                                                                  .CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                                                                })

            moDealerGroupList.Populate(DealerGroups.ToArray(),
                                        New PopulateOptions() With
                                        {
                                         .AddBlankItem = True
                                        })
        End Sub
        Private Sub InitializeForm()
            PopulateDealerDropDown()
            populateDealersList()
            PopulateDealerGroupDropDown()
            TheReportCeInputControl.populateReportLanguages(RPT_FILENAME)
        End Sub
        Private Sub UpdateBreadCrum()
            MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(PAGETITLE)
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        End Sub

        Private Sub btnGenRpt_Click(sender As System.Object, e As System.EventArgs) Handles btnGenRpt.Click
            Try
                GenerateReport()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub GenerateReport()
            Dim oUserId As Guid = ElitaPlusIdentity.Current.ActiveUser.Id
            Dim endDate As String
            Dim beginDate As String
            Dim selectedDealerId As Guid = DealerMultipleDrop.SelectedGuid
            Dim dealerCode As String = DealerMultipleDrop.SelectedCode
            Dim dealerGroup As Guid = Guid.Empty
            Dim dealerList As String

            'Validating the month and year
            If txtBeginDate.Text.Equals(String.Empty) And txtEndDate.Text.Equals(String.Empty) Then
                ElitaPlusPage.SetLabelError(lblBeginDate)
                ElitaPlusPage.SetLabelError(lblEndDate)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_BEGIN_AND_END_DATES_MUST_BE_SELECTED_ERR)
            ElseIf ((Not txtBeginDate.Text.Equals(String.Empty) And txtEndDate.Text.Equals(String.Empty)) Or (txtBeginDate.Text.Equals(String.Empty) And Not txtEndDate.Text.Equals(String.Empty))) Then
                ElitaPlusPage.SetLabelError(lblBeginDate)
                ElitaPlusPage.SetLabelError(lblEndDate)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_BEGIN_AND_END_DATES_MUST_BE_SELECTED_ERR)
            Else
                ReportCeBase.ValidateBeginEndDate(lblBeginDate, txtBeginDate.Text, lblEndDate, txtEndDate.Text)
                endDate = ReportCeBase.FormatDate(lblEndDate, txtEndDate.Text)
                beginDate = ReportCeBase.FormatDate(lblBeginDate, txtBeginDate.Text)

                'If Date.Parse(txtEndDate.Text) >= Date.Parse(DateTime.Today.Date.ToString) Then
                '    ElitaPlusPage.SetLabelError(lblEndDate)
                '    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_END_DATE_MUST_NOT_BE_HIGHER_THAN_YESTERDAY_ERR)
                'End If
            End If

            If rdealer.Checked Then
                dealerCode = ALL
            End If

            dealerGroup = New Guid(moDealerGroupList.SelectedValue)

            If UsercontrolAvailableSelectedDealers.SelectedList.Count > 0 Then
                Dim arrDealerList As String() = UsercontrolAvailableSelectedDealers.SelectedListwithCommaSep.Split(New Char() {","c})
                Dim tempList As String
                For Each s As String In arrDealerList
                    tempList = tempList & s & ";"
                Next
                dealerList = tempList
            End If
            ReportCeBase.EnableReportCe(Me, TheReportCeInputControl)

            Dim params As ReportCeBaseForm.Params = SetParameters(GuidControl.GuidToHexString(oUserId), dealerCode, beginDate, endDate, GuidControl.GuidToHexString(dealerGroup), dealerList)
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params
        End Sub

        Function SetParameters(oUserId As String, dealerCode As String, _
                               beginDate As String, endDate As String, _
                               dealerGroup As String, dealerList As String) As ReportCeBaseForm.Params

            Dim params As New ReportCeBaseForm.Params
            Dim culturecode As String = TheReportCeInputControl.getCultureValue(False)
            Dim reportName As String = TheReportCeInputControl.getReportName(RPT_FILENAME, False)

            moReportFormat = ReportCeBase.GetReportFormat(Me)
            If (moReportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (moReportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                reportName = TheReportCeInputControl.getReportName(RPT_FILENAME_EXPORT, True)
                culturecode = TheReportCeInputControl.getCultureValue(True, GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID))
            End If

            Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
                    { _
                     New ReportCeBaseForm.RptParam("V_USER_ID", oUserId), _
                     New ReportCeBaseForm.RptParam("V_DEALER_CODE", dealerCode), _
                     New ReportCeBaseForm.RptParam("V_BEGIN_DATE", beginDate), _
                     New ReportCeBaseForm.RptParam("V_END_DATE", endDate), _
                     New ReportCeBaseForm.RptParam("V_DEALER_GROUP", dealerGroup), _
                     New ReportCeBaseForm.RptParam("V_DEALER_LIST", dealerList), _
                     New ReportCeBaseForm.RptParam("LANG_CULTURE_VALUE", culturecode)}

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
