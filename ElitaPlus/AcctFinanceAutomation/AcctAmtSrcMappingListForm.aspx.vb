Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Partial Public Class AcctAmtSrcMappingListForm
    Inherits ElitaPlusSearchPage

#Region "Constants"
    Public Const URL As String = "~/AcctFinanceAutomation/AcctAmtSrcMappingListForm.aspx"
    Public Const SUMMARYTITLE As String = "SEARCH_RESULTS"
    Public Const PAGETAB As String = "FINANCE_AUTOMATION"
    Public Const PAGETITLE As String = "ACCT_AMT_SOURCE_MAPPING"
#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    Class MyState
        ' Selected Item Information
        Public SearchDVMappped As DataView = Nothing
        Public SearchDVNotMappped As DataView = Nothing
        Public searchedDealerID As Guid

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

#Region "Page Events"
    Private Sub UpdateBreadCrum()
        MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
        MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(SUMMARYTITLE)
        MasterPage.UsePageTabTitleInBreadCrum = False
        MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(PAGETAB) + ElitaBase.Sperator + TranslationBase.TranslateLabelOrMessage(PAGETITLE)
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If (Not IsReturningFromChild) Then MasterPage.MessageController.Clear()
        Form.DefaultButton = btnSearch.UniqueID
        Try
            If (Not IsPostBack) Then
                PopulateDropdowns()

                ' Translate Grid Headers
                TranslateGridHeader(gridFieldMapped)
                TranslateGridHeader(gridFieldNotMapped)

                ' Populate Search Criteria if Returning from Page and Information is Provided
                If (IsReturningFromChild) Then
                    ddlDealer.SelectedValue = State.searchedDealerID.ToString
                    PopulateGrid()
                End If
            End If

            UpdateBreadCrum()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub

    Private Sub Page_Return(ReturnFromUrl As String, ReturnParameter As Object) Handles Me.PageReturn, Me.PageCall
        MasterPage.MessageController.Clear()
        Try
            MenuEnabled = True
            IsReturningFromChild = True
            If (ReturnParameter Is Nothing) Then
                Exit Sub
            End If

            'Dim returnObj As PageReturnType(Of AfaAcctAmtSrc) = CType(ReturnParameter, PageReturnType(Of AfaAcctAmtSrc))
            Dim returnObj As Boolean = CType(ReturnParameter, Boolean)
            'If returnObj.HasDataChanged Then
            If returnObj = True Then
                State.SearchDVMappped = Nothing
                State.SearchDVNotMappped = Nothing
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Helper functions"
    Private Sub PopulateDropdowns()
        'Me.BindCodeNameToListControl(ddlDealer, LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, True, "Code"), , , , True)
        Dim oDealerList = GetDealerListByCompanyForUser()
        Dim dealerTextFunc As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                                                                           Return li.ExtendedCode + " - " + li.Translation + " " + "(" + li.Code + ")"
                                                                       End Function
        ddlDealer.Populate(oDealerList, New PopulateOptions() With
                                           {
                                            .AddBlankItem = True,
                                            .TextFunc = dealerTextFunc
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
                If oDealerList IsNot Nothing Then
                    oDealerList.AddRange(oDealerListForCompany)
                Else
                    oDealerList = oDealerListForCompany.Clone()
                End If

            End If
        Next

        Return oDealerList.ToArray()

    End Function

    Private Sub PopulateGrid()
        Dim recCount As Integer
        Try

            Dim result As Role.RoleSearchDV
            If State.SearchDVMappped Is Nothing AndAlso State.SearchDVNotMappped Is Nothing Then
                AfaAcctAmtSrc.getList(State.searchedDealerID, State.SearchDVMappped, State.SearchDVNotMappped)
            End If

            recCount = State.SearchDVMappped.Count + State.SearchDVNotMappped.Count

            If State.SearchDVMappped.Count > 0 Then
                gridFieldMapped.DataSource = State.SearchDVMappped
                gridFieldMapped.DataBind()
                gridFieldMapped.Visible = True
            Else
                gridFieldMapped.Visible = False
            End If

            If State.SearchDVNotMappped.Count > 0 Then
                gridFieldNotMapped.DataSource = State.SearchDVNotMappped
                gridFieldNotMapped.DataBind()
                gridFieldNotMapped.Visible = True
            Else
                gridFieldNotMapped.Visible = False
            End If

            If (recCount = 0) Then
                MasterPage.MessageController.AddInformation(Message.MSG_NO_RECORDS_FOUND, True)
                ControlMgr.SetVisibleControl(Me, moSearchResults, False)
            Else
                ControlMgr.SetVisibleControl(Me, moSearchResults, True)
                lblMappedCnt.Text = State.SearchDVMappped.Count.ToString
                lblNotMappedCnt.Text = State.SearchDVNotMappped.Count.ToString
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Button events handlers"
    Protected Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Try
            ddlDealer.SelectedValue = Guid.Empty.ToString
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try

            State.searchedDealerID = New Guid(ddlDealer.SelectedValue)

            If (State.searchedDealerID = Guid.Empty) Then
                MasterPage.MessageController.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR, True)
                Exit Sub
            End If
            'Reset the Caching on Search Results

            State.SearchDVMappped = Nothing
            State.SearchDVNotMappped = Nothing
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Handle grids"
    Private Sub gridFieldMapped_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gridFieldMapped.RowCommand
        Try
            Select Case e.CommandName.ToString().ToUpper()
                Case "SELECTACTION"
                    Dim objParam As New AcctAmtSrcMappingDetailForm.PageCallType
                    objParam.ID = New Guid(e.CommandArgument.ToString)
                    objParam.ActionType = AcctAmtSrcMappingDetailForm.PageCallActionType.EditExistingMapping
                    objParam.DealerID = State.searchedDealerID
                    callPage(AcctAmtSrcMappingDetailForm.URL, objParam)
            End Select
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub gridFieldMapped_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridFieldMapped.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Private Sub gridFieldNotMapped_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gridFieldNotMapped.RowCommand
        Try
            Select Case e.CommandName.ToString().ToUpper()
                Case "SELECTACTION"
                    Dim objParam As New AcctAmtSrcMappingDetailForm.PageCallType
                    objParam.ID = New Guid(e.CommandArgument.ToString)
                    objParam.ActionType = AcctAmtSrcMappingDetailForm.PageCallActionType.AddNewMapping
                    objParam.DealerID = State.searchedDealerID
                    callPage(AcctAmtSrcMappingDetailForm.URL, objParam)
            End Select
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub gridFieldNotMapped_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridFieldNotMapped.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub
#End Region


End Class