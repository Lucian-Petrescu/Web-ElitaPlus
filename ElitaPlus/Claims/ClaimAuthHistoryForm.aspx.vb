Imports System.Collections.Generic

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
            If Me.NavController.State Is Nothing Then
                Me.NavController.State = New MyState
                InitializeFromFlowSession()
            End If
            Return CType(Me.NavController.State, MyState)
        End Get
    End Property
    Protected Sub InitializeFromFlowSession()

        Me.State.InputParameters = TryCast(Me.NavController.ParametersPassed, Parameters)

        Try
            If Not Me.State.InputParameters Is Nothing Then
                Me.State.InputParameters = CType(Me.NavController.ParametersPassed, Parameters)
                Me.State.ClaimBO = Me.State.InputParameters.ClaimBO
                Me.State.MyBO = CType(Me.State.ClaimBO.ClaimAuthorizationChildren.GetChild(Me.State.InputParameters.ClaimAuthorizationId), ClaimAuthorization)
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Page_PageCall(ByVal callFromUrl As String, ByVal callingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then

            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Page Parameters"
    Public Class Parameters
        Public ClaimBO As MultiAuthClaim
        Public ClaimAuthorizationId As Guid
        Public Sub New(ByVal claimBO As MultiAuthClaim, ByVal claimAuthorizationId As Guid)
            Me.ClaimBO = claimBO
            Me.ClaimAuthorizationId = claimAuthorizationId
        End Sub
    End Class
#End Region

#Region "Page Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.MasterPage.MessageController.Clear()
            If Not Me.IsPostBack Then
                InitializeUI()
                PopulateFormFromBO()
            End If
        Catch ex As Threading.ThreadAbortException
            System.Threading.Thread.ResetAbort()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub
#End Region

#Region "Event Handlers"
    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBack.Click
        Try
            Me.NavController.Navigate(Me, FlowEvents.EVENT_BACK)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub
#End Region

#Region "Controlling Logic"
    Private Sub InitializeUI()
        Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("CLAIM_AUTH_HISTORY")
        Me.MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage("CLAIM_SUMMARY") & ElitaBase.Sperator _
                                  & TranslationBase.TranslateLabelOrMessage("CERTIFICATE") & Me.State.MyBO.Claim.CertificateNumber & ElitaBase.Sperator _
                                  & TranslationBase.TranslateLabelOrMessage("CLAIM_AUTHORIZATION_DETAIL") & Me.State.MyBO.AuthorizationNumber & ElitaBase.Sperator _
                                  & TranslationBase.TranslateLabelOrMessage("CLAIM_AUTH_HISTORY")
        Me.TranslateGridHeader(GridClaimAuthorization)
    End Sub

    Private Sub PopulateFormFromBO()
        PopulateGrid()
    End Sub
#End Region

#Region "Grid Functions"
    Public Sub PopulateGrid()

        Me.GridClaimAuthorization.AutoGenerateColumns = False
        Me.GridClaimAuthorization.PageSize = Me.State.PageSize
        Me.ValidSearchResultCountNew(Me.State.MyBO.ClaimAuthorizationHistoryChildren.Count, True)
        Me.SetPageAndSelectedIndexFromGuid(Me.State.MyBO.ClaimAuthorizationHistoryChildren, Me.State.selectedClaimAuthorizationHistoryId, Me.GridClaimAuthorization, Me.State.PageIndex)
        Me.GridClaimAuthorization.DataSource = Me.State.MyBO.ClaimAuthorizationHistoryChildren.OrderBy(Function(x) (x.HistCreatedDate)).ToList()
        Me.GridClaimAuthorization.DataBind()
        Me.lblRecordCount.Text = Me.State.MyBO.ClaimAuthorizationHistoryChildren.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)

    End Sub

    Private Sub GridClaimAuthorization_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridClaimAuthorization.PageIndexChanged
        Try
            Me.State.PageIndex = GridClaimAuthorization.PageIndex
            Me.State.selectedClaimAuthorizationHistoryId = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub GridClaimAuthorization_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridClaimAuthorization.PageIndexChanging
        Try
            GridClaimAuthorization.PageIndex = e.NewPageIndex
            State.PageIndex = GridClaimAuthorization.PageIndex
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub GridClaimAuthorization_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridClaimAuthorization.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub GridClaimAuthorization_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridClaimAuthorization.RowDataBound
        Try
            Dim item As ClaimAuthHistory = CType(e.Row.DataItem, ClaimAuthHistory)
            Dim btnEditItem As LinkButton
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            If (e.Row.RowType = DataControlRowType.DataRow) _
                OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                If (Not e.Row.Cells(1).FindControl("EditButton_WRITE") Is Nothing) Then
                    btnEditItem = CType(e.Row.Cells(0).FindControl("EditButton_WRITE"), LinkButton)
                    btnEditItem.CommandArgument = item.Id.ToString
                    btnEditItem.CommandName = SELECT_ACTION_COMMAND
                    btnEditItem.Text = item.AuthorizationNumber
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub GridClaimAuthorization_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridClaimAuthorization.RowCommand
        Try
            If e.CommandName = SELECT_ACTION_COMMAND Then
                If Not e.CommandArgument.ToString().Equals(String.Empty) Then
                    Me.State.selectedClaimAuthorizationHistoryId = New Guid(e.CommandArgument.ToString())
                    Me.NavController.Navigate(Me, FlowEvents.EVENT_BACK)
                End If
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            If (TypeOf ex Is System.Reflection.TargetInvocationException) AndAlso _
           (TypeOf ex.InnerException Is Threading.ThreadAbortException) Then Return
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub cboPageSize_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Me.State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            Me.State.PageIndex = NewCurrentPageIndex(GridClaimAuthorization, Me.State.MyBO.ClaimAuthorizationHistoryChildren.Count, State.PageSize)
            Me.GridClaimAuthorization.PageIndex = Me.State.PageIndex
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GridClaimAuthorization.Sorting
        Try

            Me.GridClaimAuthorization.DataSource = Sort(Me.State.MyBO.ClaimAuthorizationHistoryChildren.ToList, e.SortExpression, e.SortDirection)
            Me.GridClaimAuthorization.DataBind()
            Me.State.PageIndex = 0
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub
    Private Function Sort(ByVal list As List(Of ClaimAuthHistory), ByVal sortBy As String, ByVal sortDirection As WebControls.SortDirection) As List(Of ClaimAuthHistory)
        Dim propInfo As Reflection.PropertyInfo = list.GetType().GetGenericArguments()(0).GetProperty(sortBy)

        If sortDirection = WebControls.SortDirection.Ascending Then
            Return list.OrderBy(Function(i) propInfo.GetValue(i, Nothing)).ToList()
        Else
            Return list.OrderByDescending(Function(i) propInfo.GetValue(i, Nothing)).ToList()
        End If
    End Function
#End Region

End Class