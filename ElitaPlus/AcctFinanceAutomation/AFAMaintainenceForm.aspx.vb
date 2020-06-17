Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.Web.Forms

Public Class AFAMaintainenceForm
    Inherits ElitaPlusSearchPage

#Region "Constants"
    Private Const NoRowSelectedIndex As Integer = -1
#End Region

#Region "Page State"

    Class MyState
        Public PageIndex As Integer = 0
        Public PageSize As Integer = DEFAULT_NEW_UI_PAGE_SIZE
        Public SearchClicked As Boolean = False
        Public IsGridVisible As Boolean = False
        Public DealerIdInSearch As Guid = Guid.Empty
        Public DealerObjInSearch As Dealer
        Public FirstDayOfMonth As Date
        Public LastDayOfMonth As Date
        Public SearchActiveDv As AFAMaintainence.AFAProcessStatusDV


        'Public DealerIdInSearch As Guid = Guid.Empty
        'Public DealerObjInSearch As Dealer
        'Public firstDayOfMonth As String
        'Public lastDayOfMonth As String

        Sub New()
        End Sub
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

#Region "Page_Events"


    Private Sub UpdateBreadCrum()
        If (Not State Is Nothing) Then
            If (Not State Is Nothing) Then
                MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator &
                    TranslationBase.TranslateLabelOrMessage("AFAMAINTAINENCEFORM")
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("AFAMAINTAINENCEFORM")
            End If
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        MasterPage.MessageController.Clear()
        Try
            If Not IsPostBack Then

                MasterPage.MessageController.Clear()
                MasterPage.UsePageTabTitleInBreadCrum = False
                MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("FINANCE_AUTOMATION")
                UpdateBreadCrum()
                TranslateGridHeader(GridProcessStatus)
                ControlMgr.SetEnableControl(Me, BtnReRunInvoice, False)
                ControlMgr.SetEnableControl(Me, BtnReRunRecon, False)
                divDataContainer.Visible = False
                State.SearchActiveDv = Nothing
                cboPageSize.SelectedValue = CType(State.PageSize, String)
                PopulateDropdowns()

            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

    Protected Sub PopulateDropdowns()

        Dim oDealerList = GetDealerListByCompanyForUser()
        Dim dealerTextFunc As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                                                                           Return li.ExtendedCode + " - " + li.Translation + " " + "(" + li.Code + ")"
                                                                       End Function
        ddlDealer.Populate(oDealerList, New PopulateOptions() With
                                           {
                                            .TextFunc = dealerTextFunc,
                                            .AddBlankItem = True
                                           })
        Dim intYear As Integer = DateTime.Today.Year

        'ddlAcctPeriodYear.Items.Add(New ListItem("", Guid.Empty.ToString))

        For i As Integer = (intYear - 7) To intYear
            ddlAcctPeriodYear.Items.Add(New ListItem(i.ToString, i.ToString))
        Next
        ddlAcctPeriodYear.SelectedValue = intYear.ToString

        'ddlAcctPeriodMonth.Items.Add(New ListItem("", Guid.Empty.ToString))

        For month As Integer = 1 To 12
            Dim monthName As String = DateTimeFormatInfo.CurrentInfo.GetMonthName(month)
            ddlAcctPeriodMonth.Items.Add(New ListItem(monthName, month.ToString().PadLeft(2, CChar("0"))))
        Next

        Dim strMonth As String = DateTime.Today.Month.ToString().PadLeft(2, CChar("0"))
        strMonth = strMonth.Substring(strMonth.Length - 2)
        ddlAcctPeriodMonth.SelectedValue = strMonth

    End Sub
    Private Function GetDealerListByCompanyForUser() As DataElements.ListItem()
        Dim index As Integer
        Dim oListContext As New ListContext

        Dim userCompanies As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies

        Dim oDealerList As New Collections.Generic.List(Of DataElements.ListItem)

        For index = 0 To userCompanies.Count - 1
            oListContext.CompanyId = userCompanies(index)
            Dim oDealerListForCompany As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerListByCompany", context:=oListContext)
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

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Try

            If GetSelectedItem(ddlDealer) = Guid.Empty Then
                MasterPage.MessageController.AddInformation(Message.MSG_DEALER_REQUIRED)
                Exit Sub
            End If
            State.PageIndex = 0
            State.SearchClicked = True
            State.IsGridVisible = True
            State.SearchActiveDv = Nothing
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub PopulateGrid()
        Try

            If Not PopulateStateFromSearchFields() Then Exit Sub

            divDataContainer.Visible = True

            'GridProcessStatus
            If (State.SearchActiveDv Is Nothing) Then
                State.SearchActiveDv = AFAMaintainence.GetProcessStatus(State.DealerIdInSearch, ElitaPlusIdentity.Current.ActiveUser.LanguageId, State.FirstDayOfMonth, State.LastDayOfMonth)
                If State.SearchClicked Then
                    ValidSearchResultCountNew(State.SearchActiveDv.Count, True)
                    State.SearchClicked = False
                End If
            End If

            GridProcessStatus.PageSize = State.PageSize
            If State.SearchActiveDv.Count = 0 Then
                ControlMgr.SetVisibleControl(Me, GridProcessStatus, False)
            Else
                GridProcessStatus.DataSource = State.SearchActiveDv

                ControlMgr.SetVisibleControl(Me, GridProcessStatus, True)
            End If

            State.PageIndex = GridProcessStatus.PageIndex

            GridProcessStatus.DataBind()

            Session("recCount") = State.SearchActiveDv.Count

            lblActiveSearchResults.Visible = True
            lblActiveSearchResults.Text = String.Format("{0} {1}", State.SearchActiveDv.Count, TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND))
            'End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Public Function PopulateStateFromSearchFields() As Boolean
        Dim iMonth As Integer
        Dim iYear As Integer
        Try
            State.DealerIdInSearch = GetSelectedItem(ddlDealer)
            If Not State.DealerIdInSearch = Guid.Empty Then
                State.DealerIdInSearch = GetSelectedItem(ddlDealer)
                State.DealerObjInSearch = New Dealer(State.DealerIdInSearch)
                Integer.TryParse(GetSelectedValue(ddlAcctPeriodMonth), iMonth)
                Integer.TryParse(GetSelectedValue(ddlAcctPeriodYear), iYear)
                State.FirstDayOfMonth = GetFirstDayOfMonth(iMonth, iYear)
                State.LastDayOfMonth = GetLastDayOfMonth(iMonth, iYear)
                ControlMgr.SetEnableControl(Me, BtnReRunInvoice, True)
                ControlMgr.SetEnableControl(Me, BtnReRunRecon, True)
                Return True
            Else
                'ControlMgr.SetVisibleControl(Me, Grid, False)

                ControlMgr.SetEnableControl(Me, BtnReRunInvoice, False)
                ControlMgr.SetEnableControl(Me, BtnReRunRecon, False)
                GridProcessStatus.Visible = False

                Return False
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Function

#Region "Date Functions"


    Private Function GetFirstDayOfMonth(ByVal iMonth As Int32, ByVal iYear As Int32) As DateTime
        'set return value to the last day of the month for any date passed in to the method
        'create a datetime variable set to the passed in date
        Dim dtFrom As New DateTime(iYear, iMonth, 1)
        'remove all of the days in the month except the first day and set the variable to hold that date
        dtFrom = dtFrom.AddDays(-(dtFrom.Day - 1))
        'return the first day of the month
        Return dtFrom
    End Function

    Private Function GetLastDayOfMonth(ByVal iMonth As Int32, ByVal iYear As Int32) As DateTime
        'set return value to the last day of the month for any date passed in to the method
        'create a datetime variable set to the passed in date
        Dim dtTo As New DateTime(iYear, iMonth, 1)
        'overshoot the date by a month
        dtTo = dtTo.AddMonths(1)
        'remove all of the days in the next month to get bumped down to the last day of the previous(month)
        dtTo = dtTo.AddDays(-(dtTo.Day))
        'return the last day of the month
        Return dtTo
    End Function

#End Region

    Private Sub btnClearSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClearSearch.Click
        ddlDealer.SelectedIndex = NoRowSelectedIndex

        ClearSearchResults()
    End Sub

    Private Sub ClearSearchResults()
        Try
            With State
                .DealerIdInSearch = Guid.Empty
                .DealerObjInSearch = Nothing
                .FirstDayOfMonth = String.Empty
                .LastDayOfMonth = String.Empty
            End With
            State.SearchActiveDv = Nothing
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub


    Private Sub GridProcessStatus_PageIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles GridProcessStatus.PageIndexChanged
        Try
            State.PageIndex = GridProcessStatus.PageIndex
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub GridProcessStatus_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles GridProcessStatus.PageIndexChanging
        Try
            GridProcessStatus.PageIndex = e.NewPageIndex
            State.PageIndex = GridProcessStatus.PageIndex
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Public Sub GridRowGridProcessStatus_RowCreated(ByVal sender As System.Object, ByVal e As GridViewRowEventArgs) Handles GridProcessStatus.RowCreated
        Try
            BaseItemCreated(sender, e)

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub


    Private Sub cboPageSize_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            State.PageIndex = NewCurrentPageIndex(GridProcessStatus, State.SearchActiveDv.Count, State.PageSize)
            GridProcessStatus.PageIndex = State.PageIndex
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub BtnReRunRecon_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnReRunRecon.Click
        Dim result As Boolean
        Try


            result = AFAMaintainence.ReRunReconciliation(State.DealerIdInSearch, State.FirstDayOfMonth, State.LastDayOfMonth, ElitaPlusIdentity.Current.ActiveUser.UserName)

            If result Then
                MasterPage.MessageController.AddSuccess(Message.MSG_RECON_PROCESS_START_SUCESSFULL, True)
            Else
                MasterPage.MessageController.AddErrorAndShow(Message.MSG_RECON_PROCESS_START_FAILED, True)
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Private Sub BtnReRunInvoice_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnReRunInvoice.Click
        Dim result As Boolean
        Try

            result = AFAMaintainence.ReRunInvoice(State.DealerIdInSearch, State.FirstDayOfMonth, State.LastDayOfMonth, ElitaPlusIdentity.Current.ActiveUser.UserName)

            If result Then
                MasterPage.MessageController.AddSuccess(Message.MSG_INVOICE_PROCESS_START_SUCESSFULL, True)
            Else
                MasterPage.MessageController.AddErrorAndShow(Message.MSG_INVOICE_PROCESS_START_FAILED, True)
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub



End Class