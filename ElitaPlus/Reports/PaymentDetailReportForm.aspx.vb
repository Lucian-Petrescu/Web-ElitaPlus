Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports System.Linq
Imports System.Collections.Generic
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms

Namespace Reports
    Public Class PaymentDetailReportForm
        Inherits ElitaPlusPage
#Region "Constants"

        Public Const PAGETITLE As String = "PAYMENT_DETAIL"
        Public Const PAGETAB As String = "REPORTS"
        Public Const DOCTITLE As String = "PAYMENT_DETAIL"
        Public Const ALL As String = "*"
        Public Const NOTHING_SELECTED As Int16 = -1

        Private Const COL_DESCRIPTION_NAME As String = "description"
        Private Const COL_ID_NAME As String = "id"
        Public Const AVAILABLE_COMPANIES As String = "AVAILABLE_COMPANIES"
        Public Const SELECTED_COMPANIES As String = "SELECTED_COMPANIES"

#Region "Page State"
        Class MyState
            Public MyBO As ReportRequests
            Public IsNew As Boolean = False
            Public IsACopy As Boolean

            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public LastErrMsg As String
            Public HasDataChanged As Boolean
            Public ForEdit As Boolean = False
            Public selectedCompanyCodes As New List(Of String)
            Public selectedDealerCodes As New List(Of String)
            Public AvailableCompanyDV As ListItem()
            Public AvailableDealerDV As ListItem()
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

#End Region
        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            'Put user code to initialize the page here
            MasterPage.MessageController.Clear_Hide()
            ClearLabelsErrSign()

            Try
                If Not IsPostBack Then


                    TheReportExtractInputControl.ViewVisible = False
                    TheReportExtractInputControl.PdfVisible = False
                    TheReportExtractInputControl.ExportDataVisible = False
                    TheReportExtractInputControl.DestinationVisible = False
                    TheReportExtractInputControl.SchedulerVisible = False
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

        Public Sub ClearLabelsErrSign()
            Try
                ClearLabelErrSign(moBeginDateLabel)
                ClearLabelErrSign(moEndDateLabel)
                ClearLabelErrSign(lblCompany)
                ClearLabelErrSign(lblDealer)

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

        Protected Sub LoadCompanyList()
            'State.AvailableCompanyDV = Assurant.ElitaPlus.BusinessObjectsNew.User.GetSelectedCompanies(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, ElitaPlusIdentity.Current.ActiveUser.Id)
            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            listcontext.UserId = ElitaPlusIdentity.Current.ActiveUser.Id
            Dim compLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.CompanyByCompanyGroupAndUser, Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            AvailableCompanyList.Populate(compLkl, New PopulateOptions() With
                 {
                .AddBlankItem = False
                 })
            'BindListControlToDataView(AvailableCompanyList, State.AvailableCompanyDV, "DESCRIPTION", "ID", False)
            State.AvailableCompanyDV = compLkl

            If AvailableCompanyList.Items.Count > 0 Then
                AvailableCompanyList.SelectedIndex = 0
            End If

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
            Dim reportParams As New System.Text.StringBuilder
            Dim endDate As String
            Dim beginDate As String
            Dim strSelectedDealersCodes As String
            Dim strSelectedCompanyCodes As String


            'Dates
            ReportExtractBase.ValidateBeginEndDate(moBeginDateLabel, moBeginDateText.Text, moEndDateLabel, moEndDateText.Text)
            endDate = ReportExtractBase.FormatDate(moEndDateLabel, moEndDateText.Text)
            beginDate = ReportExtractBase.FormatDate(moBeginDateLabel, moBeginDateText.Text)

            'Company selection validation
            If State.selectedCompanyCodes Is Nothing OrElse State.selectedCompanyCodes.Count = 0 Then
                ElitaPlusPage.SetLabelError(lblCompany)
                Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_REQUIRED)
            Else
                strSelectedCompanyCodes = ConvertToCommSepString(State.selectedCompanyCodes)
            End If

            'Dealer selection validation
            If State.selectedDealerCodes IsNot Nothing OrElse State.selectedDealerCodes.Count > 0 Then
                strSelectedDealersCodes = ConvertToCommSepString(State.selectedDealerCodes)
            End If

            reportParams.AppendFormat("pi_Begin_Date => '{0}',", beginDate)
            reportParams.AppendFormat("pi_End_Date => '{0}',", endDate)
            reportParams.AppendFormat("pi_Company_Codes => '{0}',", strSelectedCompanyCodes)
            reportParams.AppendFormat("pi_Dealer_Codes => '{0}'", strSelectedDealersCodes)


            State.MyBO = New ReportRequests
            State.ForEdit = True
            PopulateBOProperty(State.MyBO, "ReportType", "PAYMENT_DETAIL")
            PopulateBOProperty(State.MyBO, "ReportProc", "elita.R_PaymentDetail.Report")
            PopulateBOProperty(State.MyBO, "ReportParameters", reportParams.ToString())
            PopulateBOProperty(State.MyBO, "UserEmailAddress", ElitaPlusIdentity.Current.EmailAddress)

            ScheduleReport()
        End Sub

        Public Function ConvertToCommSepString(strList As List(Of String)) As String
            Dim strResult As String
            For Each s As String In strList
                strResult = strResult + s + ","
            Next
            Return strResult
        End Function

        Private Sub ScheduleReport()
            Try
                Dim scheduleDate As DateTime = DateTime.Now
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
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#Region "Regions: Attach - Detach Event Handlers"

        Protected Sub AddCompany_Click(sender As Object, e As EventArgs) Handles AddCompany.Click
            If AvailableCompanyList.SelectedIndex <> -1 Then
                Dim SelectedItem As System.Web.UI.WebControls.ListItem
                SelectedItem = AvailableCompanyList.SelectedItem
                SelectedCompanyList.Items.Add(SelectedItem)
                AvailableCompanyList.Items.Remove(SelectedItem)
                AvailableCompanyList.ClearSelection()
                SelectedCompanyList.ClearSelection()

                SaveSelectedCompany("A")
            End If
        End Sub

        Private Sub SaveSelectedCompany(strAction As String)
            State.selectedCompanyCodes.Clear()
            Dim companyIDList As New ArrayList
            Dim CompanyID As Guid
            Dim i As Integer, guidTemp As Guid, companycode As String
            For i = 0 To SelectedCompanyList.Items.Count - 1
                CompanyID = New Guid(SelectedCompanyList.Items.Item(i).Value)
                companyIDList.Add(CompanyID)

                For j As Integer = 0 To State.AvailableCompanyDV.Count - 1
                    guidTemp = State.AvailableCompanyDV(j).ListItemId
                    If guidTemp = CompanyID Then
                        companycode = State.AvailableCompanyDV(j).Code
                        State.selectedCompanyCodes.Add(companycode)
                        Exit For
                    End If
                Next
            Next
            PopulateUserControlAvailableSelectedDealers(companyIDList, strAction)
        End Sub
        Protected Sub RemoveCompany_Click(sender As Object, e As EventArgs) Handles RemoveCompany.Click
            If SelectedCompanyList.SelectedIndex <> -1 Then
                Dim SelectedItem As System.Web.UI.WebControls.ListItem
                SelectedItem = SelectedCompanyList.SelectedItem
                AvailableCompanyList.Items.Add(SelectedItem)
                SelectedCompanyList.Items.Remove(SelectedItem)
                SelectedCompanyList.ClearSelection()
                AvailableCompanyList.ClearSelection()

                SaveSelectedCompany("R")
            End If
        End Sub

        Sub PopulateUserControlAvailableSelectedDealers(companyIDList As ArrayList, strAction As String)
            If companyIDList.Count > 0 Then
                ' State.AvailableDealerDV = LookupListNew.GetDealerLookupList(companyIDList, True, Nothing, "description", False, True)
                Dim selectedDv As DataView = Nothing
                'BindListControlToDataView(AvailableDealerList, State.AvailableDealerDV, "DESCRIPTION", "ID", False)
                Dim oDealerList = GetDealerListByCompanyForUser(companyIDList)
                Dim dealerTextFunc As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                                                                                   Return li.ExtendedCode + "-" + li.Translation + " " + "(" + li.Code + ")"
                                                                               End Function

                AvailableDealerList.Populate(oDealerList, New PopulateOptions() With
                                           {
                                            .TextFunc = dealerTextFunc,
                                            .SortFunc = dealerTextFunc
                                           })
                State.AvailableDealerDV = oDealerList
                Dim dealersToRemove As New List(Of System.Web.UI.WebControls.ListItem)

                If strAction = "R" Then 'remove any dealer belongs to removed company
                    Dim blnRemove As Boolean
                    For Each objSelected As System.Web.UI.WebControls.ListItem In SelectedDealerList.Items
                        blnRemove = True
                        For Each objAvail As System.Web.UI.WebControls.ListItem In AvailableDealerList.Items
                            If objSelected.Value = objAvail.Value Then
                                'AvailableDealerList.Items.Remove(objAvail) 'found, remove from available
                                blnRemove = False
                                Exit For
                            End If
                        Next
                        If blnRemove = True Then
                            'SelectedDealerList.Items.Remove(objSelected) 
                            dealersToRemove.Add(objSelected) 'not found, remove from select
                        End If
                    Next

                    For Each objItem As System.Web.UI.WebControls.ListItem In dealersToRemove
                        SelectedDealerList.Items.Remove(objItem)
                    Next

                    For Each objItem As System.Web.UI.WebControls.ListItem In SelectedDealerList.Items
                        AvailableDealerList.Items.Remove(objItem)
                    Next
                End If
            Else
                State.AvailableDealerDV = Nothing
                'BindListControlToDataView(AvailableDealerList, State.AvailableDealerDV, "DESCRIPTION", "ID", False)
                Dim oDealerList As New ListItem()

                'Dim dealerTextFunc As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                '                                                                   Return li.ExtendedCode + "-" + li.Translation + " " + "(" + li.Code + ")"
                '                                                               End Function

                AvailableDealerList.Items.Clear()


                ' BindListControlToDataView(SelectedDealerList, State.AvailableDealerDV, "DESCRIPTION", "ID", False)
                SelectedDealerList.Items.Clear()
            End If
        End Sub
        Private Function GetDealerListByCompanyForUser(companyIDList As ArrayList) As Assurant.Elita.CommonConfiguration.DataElements.ListItem()
            Dim Index As Integer
            Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext

            Dim UserCompanies As ArrayList = companyIDList

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
        Protected Sub AddDealer_Click(sender As Object, e As EventArgs) Handles AddDealer.Click
            If AvailableDealerList.SelectedIndex <> -1 Then
                Dim SelectedItem As System.Web.UI.WebControls.ListItem
                SelectedItem = AvailableDealerList.SelectedItem
                SelectedDealerList.Items.Add(SelectedItem)
                AvailableDealerList.Items.Remove(SelectedItem)
                AvailableDealerList.ClearSelection()
                SelectedDealerList.ClearSelection()

                SaveSelectedDealer()

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

                SaveSelectedDealer()
            End If
        End Sub

        Private Sub SaveSelectedDealer()
            State.selectedDealerCodes.Clear()
            Dim DealerID As Guid
            Dim i As Integer, guidTemp As Guid, dealercode As String

            For i = 0 To SelectedDealerList.Items.Count - 1
                DealerID = New Guid(SelectedDealerList.Items.Item(i).Value)

                For j As Integer = 0 To State.AvailableDealerDV.Count - 1
                    guidTemp = State.AvailableDealerDV(j).ListItemId
                    If guidTemp = DealerID Then
                        dealercode = State.AvailableDealerDV(j).code
                        State.selectedDealerCodes.Add(dealercode)
                    End If
                Next
            Next
        End Sub


#End Region

    End Class

End Namespace
