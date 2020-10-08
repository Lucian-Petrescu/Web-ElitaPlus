Imports System.Collections.Generic
Imports System.Reflection
Imports System.Threading

Public Class ClaimAuthHistoryForm
    Inherits ElitaPlusSearchPage


#Region "Constants"
    Public Const SELECT_ACTION_COMMAND As String = "SelectAction"
    Public Const URL As String = "~/Claims/ClaimAuthHistoryForm.aspx"
#End Region

#Region "Page State"

    Class BaseState
        Public NavCtrl As INavigationController
    End Class


    Class MyState
        Public ClaimBO As MultiAuthClaim
        Public MyBO As ClaimAuthorization
        Public InputParameters As Parameters
        Public IsEditMode As Boolean = False
        Public PageSize As Integer = 10
        Public PageIndex As Integer
        Public selectedClaimAuthorizationHistoryId As Guid

    End Class

    Public Sub New()
        MyBase.New(New BaseState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            If NavController.State Is Nothing Then
                NavController.State = New MyState
                InitializeFromFlowSession()
            End If
            Return CType(NavController.State, MyState)
        End Get
    End Property
    Protected Sub InitializeFromFlowSession()

        State.InputParameters = TryCast(NavController.ParametersPassed, Parameters)

        Try
            If State.InputParameters IsNot Nothing Then
                State.InputParameters = CType(NavController.ParametersPassed, Parameters)
                State.ClaimBO = State.InputParameters.ClaimBO
                State.MyBO = CType(State.ClaimBO.ClaimAuthorizationChildren.GetChild(State.InputParameters.ClaimAuthorizationId), ClaimAuthorization)
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Page_PageCall(callFromUrl As String, callingPar As Object) Handles MyBase.PageCall
        Try
            If CallingParameters IsNot Nothing Then

            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Page Parameters"
    Public Class Parameters
        Public ClaimBO As MultiAuthClaim
        Public ClaimAuthorizationId As Guid
        Public Sub New(claimBO As MultiAuthClaim, claimAuthorizationId As Guid)
            Me.ClaimBO = claimBO
            Me.ClaimAuthorizationId = claimAuthorizationId
        End Sub
    End Class
#End Region

#Region "Page Events"

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            MasterPage.MessageController.Clear()
            If Not IsPostBack Then
                InitializeUI()
                PopulateFormFromBO()
            End If
        Catch ex As ThreadAbortException
            Thread.ResetAbort()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub
#End Region

#Region "Event Handlers"
    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            NavController.Navigate(Me, FlowEvents.EVENT_BACK)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub
#End Region

#Region "Controlling Logic"
    Private Sub InitializeUI()
        MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("CLAIM_AUTH_HISTORY")
        MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage("CLAIM_SUMMARY") & ElitaBase.Sperator _
                                  & TranslationBase.TranslateLabelOrMessage("CERTIFICATE") & State.MyBO.Claim.CertificateNumber & ElitaBase.Sperator _
                                  & TranslationBase.TranslateLabelOrMessage("CLAIM_AUTHORIZATION_DETAIL") & State.MyBO.AuthorizationNumber & ElitaBase.Sperator _
                                  & TranslationBase.TranslateLabelOrMessage("CLAIM_AUTH_HISTORY")
        TranslateGridHeader(GridClaimAuthorization)
    End Sub

    Private Sub PopulateFormFromBO()
        PopulateGrid()
    End Sub
#End Region

#Region "Grid Functions"
    Public Sub PopulateGrid()

        GridClaimAuthorization.AutoGenerateColumns = False
        GridClaimAuthorization.PageSize = State.PageSize
        ValidSearchResultCountNew(State.MyBO.ClaimAuthorizationHistoryChildren.Count, True)
        SetPageAndSelectedIndexFromGuid(State.MyBO.ClaimAuthorizationHistoryChildren, State.selectedClaimAuthorizationHistoryId, GridClaimAuthorization, State.PageIndex)
        GridClaimAuthorization.DataSource = State.MyBO.ClaimAuthorizationHistoryChildren.OrderBy(Function(x) (x.HistCreatedDate)).ToList()
        GridClaimAuthorization.DataBind()
        lblRecordCount.Text = State.MyBO.ClaimAuthorizationHistoryChildren.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)

    End Sub

    Private Sub GridClaimAuthorization_PageIndexChanged(sender As Object, e As EventArgs) Handles GridClaimAuthorization.PageIndexChanged
        Try
            State.PageIndex = GridClaimAuthorization.PageIndex
            State.selectedClaimAuthorizationHistoryId = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub GridClaimAuthorization_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles GridClaimAuthorization.PageIndexChanging
        Try
            GridClaimAuthorization.PageIndex = e.NewPageIndex
            State.PageIndex = GridClaimAuthorization.PageIndex
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub GridClaimAuthorization_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles GridClaimAuthorization.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub GridClaimAuthorization_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridClaimAuthorization.RowDataBound
        Try
            Dim item As ClaimAuthHistory = CType(e.Row.DataItem, ClaimAuthHistory)
            Dim btnEditItem As LinkButton
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            If (e.Row.RowType = DataControlRowType.DataRow) _
                OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                If (e.Row.Cells(1).FindControl("EditButton_WRITE") IsNot Nothing) Then
                    btnEditItem = CType(e.Row.Cells(0).FindControl("EditButton_WRITE"), LinkButton)
                    btnEditItem.CommandArgument = item.Id.ToString
                    btnEditItem.CommandName = SELECT_ACTION_COMMAND
                    btnEditItem.Text = item.AuthorizationNumber
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub GridClaimAuthorization_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridClaimAuthorization.RowCommand
        Try
            If e.CommandName = SELECT_ACTION_COMMAND Then
                If Not e.CommandArgument.ToString().Equals(String.Empty) Then
                    State.selectedClaimAuthorizationHistoryId = New Guid(e.CommandArgument.ToString())
                    NavController.Navigate(Me, FlowEvents.EVENT_BACK)
                End If
            End If
        Catch ex As ThreadAbortException
        Catch ex As Exception
            If (TypeOf ex Is TargetInvocationException) AndAlso _
           (TypeOf ex.InnerException Is ThreadAbortException) Then Return
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub cboPageSize_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            State.PageIndex = NewCurrentPageIndex(GridClaimAuthorization, State.MyBO.ClaimAuthorizationHistoryChildren.Count, State.PageSize)
            GridClaimAuthorization.PageIndex = State.PageIndex
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_SortCommand(source As Object, e As GridViewSortEventArgs) Handles GridClaimAuthorization.Sorting
        Try

            GridClaimAuthorization.DataSource = Sort(State.MyBO.ClaimAuthorizationHistoryChildren.ToList, e.SortExpression, e.SortDirection)
            GridClaimAuthorization.DataBind()
            State.PageIndex = 0
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub
    Private Function Sort(list As List(Of ClaimAuthHistory), sortBy As String, sortDirection As SortDirection) As List(Of ClaimAuthHistory)
        Dim propInfo As PropertyInfo = list.GetType().GetGenericArguments()(0).GetProperty(sortBy)

        If sortDirection = SortDirection.Ascending Then
            Return list.OrderBy(Function(i) propInfo.GetValue(i, Nothing)).ToList()
        Else
            Return list.OrderByDescending(Function(i) propInfo.GetValue(i, Nothing)).ToList()
        End If
    End Function
#End Region

End Class