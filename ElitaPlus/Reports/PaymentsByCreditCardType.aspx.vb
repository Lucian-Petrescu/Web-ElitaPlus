Imports Assurant.ElitaPlus.BusinessObjects.Common
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Reports

    Public Class BillingSummaryByCreditCard
        Inherits ElitaPlusPage
#Region "Constants"

        Public Const PAGETITLE As String = "PAYMENTS_BY_CREDIT_CARD"
        Public Const PAGETAB As String = "REPORTS"
        Public Const DOCTITLE As String = "PAYMENTS_BY_CREDIT_CARD"
        Public Const ALL As String = "*"
        Public Const NOTHING_SELECTED As Int16 = -1

        Private Const COL_DESCRIPTION_NAME As String = "description"
        Private Const COL_ID_NAME As String = "id"
        Public Const AVAILABLE_COMPANIES As String = "AVAILABLE_COMPANIES"
        Public Const SELECTED_COMPANIES As String = "SELECTED_COMPANIES"
        Public Const COL_NAME_CREDIT_CARD_TYPE As String = "credit_card_type"
        Public Const COL_NAME_COMPANY_CREDIT_CARD_ID As String = "company_credit_card_id"


#End Region

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents isProgressVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents isReportCeVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents moReportCeStatus As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents moDaysActiveLabel As System.Web.UI.WebControls.Label

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub



#End Region


#Region "Page State"

        Class MyState
            Public MyBO As ReportRequests
            Public IsNew As Boolean = False
            Public IsACopy As Boolean

            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public LastErrMsg As String
            Public HasDataChanged As Boolean
            Public ForEdit As Boolean = False
            Public selectedCompaniesGuidStrCollection As ArrayList
            Public selectedDealersGuidStrCollection As ArrayList
            Public selectedCreditCardsGuidStrCollection As ArrayList

        End Class

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property

#End Region

#Region "Handlers-Init"


        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            MasterPage.MessageController.Clear_Hide()
            ClearLabelsErrSign()

            Try
                If Not IsPostBack Then
                    TheReportExtractInputControl.ViewVisible = False
                    TheReportExtractInputControl.PdfVisible = False
                    TheReportExtractInputControl.ExportDataVisible = False
                    TheReportExtractInputControl.DestinationVisible = False
                    MasterPage.UsePageTabTitleInBreadCrum = False
                    AddCalendar(BtnBeginDate, moBeginDateText)
                    AddCalendar(BtnEndDate, moEndDateText)
                    InitializeForm()
                    SetFormTab(PAGETAB)
                    UpdateBreadCrum()

                    LoadCompanyList()

                End If
                InstallDisplayNewReportProgressBar()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub

#End Region
        Public Sub ClearLabelsErrSign()
            Try
                ClearLabelErrSign(moBeginDateLabel)
                ClearLabelErrSign(moEndDateLabel)
                ClearLabelErrSign(lblCompany)
                ClearLabelErrSign(lblDealer)
                ClearLabelErrSign(lblCreditCard)

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub InitializeForm()
            'RadiobuttonTotalsOnly.Checked = True
            Dim t As Date = Date.Now.AddMonths(-1).AddDays(1)
            moBeginDateText.Text = GetDateFormattedString(t)
            moEndDateText.Text = GetDateFormattedString(Date.Now)
        End Sub

        Private Sub UpdateBreadCrum()
            MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(PAGETITLE)
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        End Sub

#Region "Handlers-Buttons"


        Private Sub btnGenRpt_Click(sender As System.Object, e As System.EventArgs) Handles btnGenRpt.Click
            Try
                GenerateReport()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub


#End Region


        Private Sub GenerateReport()
            Dim reportParams As New System.Text.StringBuilder
            Dim langId As String = GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            Dim endDate As String
            Dim beginDate As String
            Dim strSelectedDealersIds As String
            Dim strSelectedCreditCardIds As String


            'Dates
            ReportExtractBase.ValidateBeginEndDate(moBeginDateLabel, moBeginDateText.Text, moEndDateLabel, moEndDateText.Text)
            endDate = ReportExtractBase.FormatDate(moEndDateLabel, moEndDateText.Text)
            beginDate = ReportExtractBase.FormatDate(moBeginDateLabel, moBeginDateText.Text)

            'Company selection validation
            If State.selectedCompaniesGuidStrCollection Is Nothing OrElse State.selectedCompaniesGuidStrCollection.Count <= 0 Then
                ElitaPlusPage.SetLabelError(lblCompany)
                Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_REQUIRED)
            End If

            'Dealer selection validation
            If State.selectedDealersGuidStrCollection Is Nothing OrElse State.selectedDealersGuidStrCollection.Count <= 0 Then
                ElitaPlusPage.SetLabelError(lblDealer)
                Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
            Else
                strSelectedDealersIds = SelectedListValuewithCommaSep(SelectedDealerList)
            End If

            'Credit Card selection validation
            If State.selectedCreditCardsGuidStrCollection Is Nothing OrElse State.selectedCreditCardsGuidStrCollection.Count <= 0 Then
                ElitaPlusPage.SetLabelError(lblCreditCard)
                Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_CREDIT_CARD_MUST_BE_SELECTED_ERR)
            Else
                strSelectedCreditCardIds = SelectedListValuewithCommaSep(SelectedCreditCardList)
            End If

            reportParams.AppendFormat("pi_Begin_Date => '{0}',", beginDate)
            reportParams.AppendFormat("pi_End_Date => '{0}',", endDate)
            reportParams.AppendFormat("pi_Dealer_Ids => '{0}',", strSelectedDealersIds)
            reportParams.AppendFormat("pi_Credit_Cards_Ids => '{0}',", strSelectedCreditCardIds)
            reportParams.AppendFormat("pi_language => '{0}'", langId)

            State.MyBO = New ReportRequests
            State.ForEdit = True
            PopulateBOProperty(State.MyBO, "ReportType", "PAYMENTS_BY_CREDIT_CARD")
            PopulateBOProperty(State.MyBO, "ReportProc", "R_BillingSummaryByCreditCard.Report")
            PopulateBOProperty(State.MyBO, "ReportParameters", reportParams.ToString())
            PopulateBOProperty(State.MyBO, "UserEmailAddress", ElitaPlusIdentity.Current.EmailAddress)

            ScheduleReport()
        End Sub

        Private Sub ScheduleReport()
            Try
                Dim scheduleDate As DateTime = TheReportExtractInputControl.GetSchedDate()
                If State.MyBO.IsDirty Then
                    State.MyBO.Save()

                    State.IsNew = False
                    State.HasDataChanged = True
                    State.MyBO.CreateJob(scheduleDate)

                    If String.IsNullOrEmpty(ElitaPlusIdentity.Current.EmailAddress) Then
                        DisplayMessage(Message.MSG_Email_not_configured, "", MSG_BTN_OK, MSG_TYPE_ALERT, , True)
                    Else
                        DisplayMessage(Message.MSG_REPORT_REQUEST_IS_GENERATED, "", MSG_BTN_OK, MSG_TYPE_ALERT, , True)
                    End If

                    btnGenRpt.Enabled = False

                Else
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub PopulateBosFromForm()
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Try
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub
        Protected Sub LoadCompanyList()
            BindListControlToDataView(AvailableCompanyList, Assurant.ElitaPlus.BusinessObjectsNew.User.GetSelectedCompanies(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, ElitaPlusIdentity.Current.ActiveUser.Id), "DESCRIPTION", "ID", False)
            If AvailableCompanyList.Items.Count > 0 Then
                AvailableCompanyList.SelectedIndex = 0
            End If

        End Sub

        Sub PopulateUserControlAvailableSelectedDealers()
            Dim oDealerList = GetDealerListByCompanyForUser()
            Dim dealerTextFunc As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                                                                               Return li.ExtendedCode + "-" + li.Code
                                                                           End Function

            If State.selectedCompaniesGuidStrCollection.Count > 0 Then

                'Dim availableDv As DataView = LookupListNew.GetDealerLookupList(Me.State.selectedCompaniesGuidStrCollection, True, Nothing, "description1", False, True)
                Dim selectedDv As DataView = Nothing
                'BindListControlToDataView(AvailableDealerList, availableDv, "DESCRIPTION1", "ID", False)
                AvailableDealerList.Populate(oDealerList, New PopulateOptions() With
                                          {
                                           .TextFunc = dealerTextFunc,
                                           .SortFunc = dealerTextFunc
                                          })
                'BindListControlToDataView(SelectedDealerList, selectedDv, "DESCRIPTION1", "ID", False)
                Dim selectedDealerLkl As ListItem()
                'SelectedDealerList.Populate(selectedDealerLkl, New PopulateOptions())
                'Me.UserControlAvailableSelectedDealers.SetAvailableData(availableDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
                'Me.UserControlAvailableSelectedDealers.SetSelectedData(selectedDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
                'ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedDealers, True)
            Else
                'Dim blankDv As DataView = Nothing
                'BindListControlToDataView(AvailableDealerList, blankDv, "DESCRIPTION1", "ID", False)
                'Dim selectedDealerLkl As ListItem()
                AvailableDealerList.Items.Clear()
                'BindListControlToDataView(SelectedDealerList, blankDv, "DESCRIPTION1", "ID", False)
                SelectedDealerList.Items.Clear()
                'Me.UserControlAvailableSelectedDealers.SetAvailableData(blankDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
                'Me.UserControlAvailableSelectedDealers.SetSelectedData(blankDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
                'Me.UserControlAvailableSelectedCreditCards.SetAvailableData(blankDv, COL_NAME_CREDIT_CARD_TYPE, COL_NAME_COMPANY_CREDIT_CARD_ID)
                'Me.UserControlAvailableSelectedCreditCards.SetSelectedData(blankDv, COL_NAME_CREDIT_CARD_TYPE, COL_NAME_COMPANY_CREDIT_CARD_ID)

            End If

        End Sub
        Private Function GetDealerListByCompanyForUser() As Assurant.Elita.CommonConfiguration.DataElements.ListItem()
            Dim Index As Integer
            Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext

            Dim UserCompanies As ArrayList = State.selectedCompaniesGuidStrCollection

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
        Sub PopulateUserControlAvailableSelectedCreditCards()

            If State.selectedCompaniesGuidStrCollection.Count > 0 Then

                Dim availableDv As DataView = CompanyCreditCard.LoadList(State.selectedCompaniesGuidStrCollection)
                Dim selectedDv As DataView = Nothing

                BindListControlToDataView(AvailableCreditCardList, availableDv, COL_NAME_CREDIT_CARD_TYPE, COL_NAME_COMPANY_CREDIT_CARD_ID, False)
                BindListControlToDataView(SelectedCreditCardList, selectedDv, COL_NAME_CREDIT_CARD_TYPE, COL_NAME_COMPANY_CREDIT_CARD_ID, False)

            Else
                Dim blankDv As DataView = Nothing
                BindListControlToDataView(AvailableCreditCardList, blankDv, COL_NAME_CREDIT_CARD_TYPE, COL_NAME_COMPANY_CREDIT_CARD_ID, False)
                BindListControlToDataView(SelectedCreditCardList, blankDv, COL_NAME_CREDIT_CARD_TYPE, COL_NAME_COMPANY_CREDIT_CARD_ID, False)
            End If

        End Sub

        Public Function SelectedListValuewithCommaSep(moSelectedList As ListBox) As String

            Dim oSelectedlist As String
            Dim oItem As System.Web.UI.WebControls.ListItem
            oSelectedlist = String.Empty
            For Each oItem In moSelectedList.Items
                oSelectedlist = oSelectedlist + DALObjects.DALBase.GuidToSQLString(New Guid(oItem.Value)) + ","
            Next
            Return oSelectedlist.Substring(0, oSelectedlist.Length - 1)


        End Function
#Region "Regions: Attach - Detach Event Handlers"

        Protected Sub AddCompany_Click(sender As Object, e As EventArgs) Handles AddCompany.Click
            If AvailableCompanyList.SelectedIndex <> -1 Then
                Dim SelectedItem As System.Web.UI.WebControls.ListItem
                SelectedItem = AvailableCompanyList.SelectedItem
                SelectedCompanyList.Items.Add(SelectedItem)
                AvailableCompanyList.Items.Remove(SelectedItem)
                AvailableCompanyList.ClearSelection()
                SelectedCompanyList.ClearSelection()

                State.selectedCompaniesGuidStrCollection = New ArrayList
                Dim i As Integer
                For i = 0 To SelectedCompanyList.Items.Count - 1
                    Dim CompanyID As Guid = New Guid(SelectedCompanyList.Items.Item(i).Value)
                    State.selectedCompaniesGuidStrCollection.Add(CompanyID)
                Next

                PopulateUserControlAvailableSelectedDealers()
                PopulateUserControlAvailableSelectedCreditCards()
            End If
        End Sub

        Protected Sub RemoveCompany_Click(sender As Object, e As EventArgs) Handles RemoveCompany.Click
            If SelectedCompanyList.SelectedIndex <> -1 Then
                Dim SelectedItem As System.Web.UI.WebControls.ListItem
                SelectedItem = SelectedCompanyList.SelectedItem
                AvailableCompanyList.Items.Add(SelectedItem)
                SelectedCompanyList.Items.Remove(SelectedItem)
                SelectedCompanyList.ClearSelection()
                AvailableCompanyList.ClearSelection()
                State.selectedCompaniesGuidStrCollection = New ArrayList
                Dim i As Integer
                For i = 0 To SelectedCompanyList.Items.Count - 1
                    Dim CompanyID As Guid = New Guid(SelectedCompanyList.Items.Item(i).Value)
                    State.selectedCompaniesGuidStrCollection.Add(CompanyID)
                Next

                PopulateUserControlAvailableSelectedDealers()
                PopulateUserControlAvailableSelectedCreditCards()
            End If
        End Sub

        Protected Sub AddDealer_Click(sender As Object, e As EventArgs) Handles AddDealer.Click
            If AvailableDealerList.SelectedIndex <> -1 Then
                Dim SelectedItem As System.Web.UI.WebControls.ListItem
                SelectedItem = AvailableDealerList.SelectedItem
                SelectedDealerList.Items.Add(SelectedItem)
                AvailableDealerList.Items.Remove(SelectedItem)
                AvailableDealerList.ClearSelection()
                SelectedDealerList.ClearSelection()

                State.selectedDealersGuidStrCollection = New ArrayList
                Dim i As Integer
                For i = 0 To SelectedDealerList.Items.Count - 1
                    Dim DealerID As Guid = New Guid(SelectedDealerList.Items.Item(i).Value)
                    State.selectedDealersGuidStrCollection.Add(DealerID)
                Next

            End If
        End Sub

        Protected Sub RemoveDealer_Click(sender As Object, e As EventArgs) Handles RemoveDealer.Click
            If SelectedDealerList.SelectedIndex <> -1 Then
                Dim SelectedItem As System.Web.UI.WebControls.ListItem
                SelectedItem = SelectedDealerList.SelectedItem
                AvailableDealerList.Items.Add(SelectedItem)
                SelectedDealerList.Items.Remove(SelectedItem)
                SelectedDealerList.ClearSelection()
                AvailableDealerList.ClearSelection()
                State.selectedDealersGuidStrCollection = New ArrayList
                Dim i As Integer
                For i = 0 To SelectedDealerList.Items.Count - 1
                    Dim DealerID As Guid = New Guid(SelectedDealerList.Items.Item(i).Value)
                    State.selectedDealersGuidStrCollection.Add(DealerID)
                Next

            End If
        End Sub

        Protected Sub AddCreditCard_Click(sender As Object, e As EventArgs) Handles AddCreditCard.Click
            If AvailableCreditCardList.SelectedIndex <> -1 Then
                Dim SelectedItem As System.Web.UI.WebControls.ListItem
                SelectedItem = AvailableCreditCardList.SelectedItem
                SelectedCreditCardList.Items.Add(SelectedItem)
                AvailableCreditCardList.Items.Remove(SelectedItem)
                AvailableCreditCardList.ClearSelection()
                SelectedCreditCardList.ClearSelection()

                State.selectedCreditCardsGuidStrCollection = New ArrayList
                Dim i As Integer
                For i = 0 To SelectedCreditCardList.Items.Count - 1
                    Dim Credit_cardID As Guid = New Guid(SelectedCreditCardList.Items.Item(i).Value)
                    State.selectedCreditCardsGuidStrCollection.Add(Credit_cardID)
                Next

            End If
        End Sub

        Protected Sub RemoveCreditCard_Click(sender As Object, e As EventArgs) Handles RemoveCreditCard.Click
            If SelectedCreditCardList.SelectedIndex <> -1 Then
                Dim SelectedItem As System.Web.UI.WebControls.ListItem
                SelectedItem = SelectedCreditCardList.SelectedItem
                AvailableCreditCardList.Items.Add(SelectedItem)
                SelectedCreditCardList.Items.Remove(SelectedItem)
                SelectedCreditCardList.ClearSelection()
                AvailableCreditCardList.ClearSelection()
                State.selectedCreditCardsGuidStrCollection = New ArrayList
                Dim i As Integer
                For i = 0 To SelectedCreditCardList.Items.Count - 1
                    Dim Credit_cardID As Guid = New Guid(SelectedCreditCardList.Items.Item(i).Value)
                    State.selectedCreditCardsGuidStrCollection.Add(Credit_cardID)
                Next

            End If
        End Sub
        'Protected Sub UserControlAvailableSelectedCompanies_Attach1(ByVal aSrc As Generic.UserControlAvailableSelected_New, ByVal attachedList As System.Collections.ArrayList)
        '    Try
        '        If attachedList.Count > 0 Then
        '            Me.State.selectedCompaniesGuidStrCollection = attachedList
        '            Me.PopulateUserControlAvailableSelectedDealers()
        '            PopulateUserControlAvailableSelectedCreditCards()
        '        End If
        '    Catch ex As Exception
        '        Me.HandleErrors(ex, Me.MasterPage.MessageController)
        '    End Try

        'End Sub

#End Region
    End Class
End Namespace
