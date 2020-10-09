Imports System.Collections.Generic
Imports System.Reflection
Imports System.Threading
Imports Assurant.Elita.ClientIntegration
Imports Assurant.Elita.ClientIntegration.Headers
Imports Assurant.ElitaPlus.ElitaPlusWebApp.ClaimFulfillmentService
Imports Assurant.ElitaPlus.ElitaPlusWebApp.ElitaPlusPage
Public Class UserControlConsequentialDamage
    Inherits UserControl

#Region "Constants"
    Private Const GridNameConseqDamageIssue As String = "GridViewConseqDamageIssue"
    Private Const SelectActionCommand As String = "SelectAction"
    Private Const SelectActionButton As String = "SelectActionButton"
    Private Const ClaimIssueList As String = "CLMISSUESTATUS"

    Private Const GridConseqdamageIssueColStatusCodeIdx As Integer = 4

#End Region

#Region "Page State"
    Class MyState
        Public ClaimBo As ClaimBase
        Public ConsequentialDamageListDv As DataView
        Public ConsequentialDamageStatus As Boolean = True
    End Class
    Public ReadOnly Property State() As MyState
        Get
            If Page.StateSession.Item(UniqueID) Is Nothing Then
                Page.StateSession.Item(UniqueID) = New MyState
            End If
            Return CType(Page.StateSession.Item(UniqueID), MyState)
        End Get
    End Property
    Private Shadows ReadOnly Property Page() As ElitaPlusSearchPage
        Get
            Return CType(MyBase.Page, ElitaPlusSearchPage)
        End Get
    End Property
#End Region
    ' This is the initialization Method
    Public Sub PopulateConsequentialDamage(oClaim As ClaimBase)
        State.ClaimBo = oClaim
        PopulateConsequentialDamageGrid()
    End Sub
    Public Sub Translate()
        Page.TranslateGridHeader(GridViewConsequentialDamage)
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub
    Private Sub LoadConsequentialDamage()
        State.ConsequentialDamageListDv = CaseConseqDamage.LoadListConsequentialDamage(State.ClaimBo.Id)
    End Sub
    Public Sub UpdateConsequentialDamagestatus(oClaim As ClaimBase)
        State.ClaimBo = oClaim
        ' load all consequentail damage list
        If (State.ConsequentialDamageListDv Is Nothing) Then
            LoadConsequentialDamage()
        End If
        If State.ConsequentialDamageListDv IsNot Nothing AndAlso State.ConsequentialDamageListDv.Count > 0 Then
            ' update the status of each consequentail damage
            For Each dvRow As DataRowView In State.ConsequentialDamageListDv
                Dim caseConseqDamageID As Guid = GetGuidFromString(GetGuidStringFromByteArray(CType(dvRow("case_conseq_damage_id"), Byte())))

                If Not caseConseqDamageID.Equals(Guid.Empty) Then
                    Dim oCaseConseqDamage As CaseConseqDamage = New CaseConseqDamage(caseConseqDamageID)
                    Dim CdStatus As String = oCaseConseqDamage.StatusXcd

                    If (Not String.IsNullOrWhiteSpace(oCaseConseqDamage.CoverageConseqDamageId.ToString()) AndAlso Not oCaseConseqDamage.CoverageConseqDamageId.Equals(Guid.Empty)) Then
                        If CdStatus <> Codes.CONSEQUENTIAL_DAMAGE_STATUS__APPROVED _
                            AndAlso oCaseConseqDamage.IssuesStatus = Codes.CONSEQUENTIAL_DAMAGE_STATUS__APPROVED Then
                            oCaseConseqDamage.StatusXcd = oCaseConseqDamage.IssuesStatus
                            oCaseConseqDamage.Save()
                            If (Page.NavController IsNot Nothing) AndAlso Page.NavController.CurrentNavState.Name = "CLAIM_DETAIL" Then
                                ' Call only when status is changed from some other status to approved
                                Dim blnsuccess As Boolean
                                blnsuccess = CallConseqDamageFulfillmentWs(oCaseConseqDamage.Id)
                                If Not blnsuccess Then
                                    oCaseConseqDamage.StatusXcd = CdStatus
                                    oCaseConseqDamage.Save()
                                End If
                            End If
                        ElseIf CdStatus <> oCaseConseqDamage.IssuesStatus Then
                            oCaseConseqDamage.StatusXcd = oCaseConseqDamage.IssuesStatus
                            oCaseConseqDamage.Save()
                        End If
                    End If
                End If
            Next
        End If
    End Sub
    'Private Function GetConsequentialDamageStatus() As Boolean
    '    Dim isAllCdApproved As Boolean = True
    '    If Not State.ConsequentialDamageListDv Is Nothing AndAlso State.ConsequentialDamageListDv.Count > 0 Then
    '        ' update the status of each consequentail damage
    '        For Each dvRow As DataRowView In State.ConsequentialDamageListDv
    '            Dim caseConseqDamageID As Guid = GetGuidFromString(GetGuidStringFromByteArray(CType(dvRow("case_conseq_damage_id"), Byte())))
    '            If Not caseConseqDamageID.Equals(Guid.Empty) Then
    '                Dim oCaseConseqDamage As CaseConseqDamage = New CaseConseqDamage(caseConseqDamageID)
    '                If oCaseConseqDamage.StatusXcd = Codes.CONSEQUENTIAL_DAMAGE_STATUS__REQUESTED Then
    '                    isAllCdApproved = False
    '                End If
    '            End If
    '        Next
    '    End If
    '    Return isAllCdApproved
    'End Function
    Private Sub PopulateConsequentialDamageGrid()
        Try
            LoadConsequentialDamage()
            State.ConsequentialDamageStatus = True
            If State.ConsequentialDamageListDv IsNot Nothing AndAlso State.ConsequentialDamageListDv.Count > 0 Then
                GridViewConsequentialDamage.DataSource = State.ConsequentialDamageListDv
                GridViewConsequentialDamage.DataBind()
                If Not State.ConsequentialDamageStatus Then
                    Page.MasterPage.MessageController.AddInformation("CONSEQUENTIAL_DAMAGE_IS_IN_REQUESTED_STATUS", True)
                End If
            Else
                lblCdRecordCount.Text = TranslationBase.TranslateLabelOrMessage(Message.MSG_NO_RECORDS_FOUND)
            End If

        Catch ex As Exception
            Page.HandleErrors(ex, Page.MasterPage.MessageController)
        End Try
    End Sub
#Region "Grid function"
    Private Sub GridViewConsequentialDamage_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridViewConsequentialDamage.RowDataBound
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim oCaseConseqDamage As CaseConseqDamage = New CaseConseqDamage(GetGuidFromString(GetGuidStringFromByteArray(CType(dvRow("case_conseq_damage_id"), Byte()))))
            If oCaseConseqDamage.StatusXcd <> Codes.CONSEQUENTIAL_DAMAGE_STATUS__DENIED Then
                Dim gridViewConseqDamageIssue As GridView = CType(e.Row.FindControl(GridNameConseqDamageIssue), GridView)
                Page.TranslateGridHeader(gridViewConseqDamageIssue)
                If oCaseConseqDamage.StatusXcd = Codes.CONSEQUENTIAL_DAMAGE_STATUS__REQUESTED Then
                    State.ConsequentialDamageStatus = False
                End If
                gridViewConseqDamageIssue.DataSource = oCaseConseqDamage.GetConsequentialDamageIssuesView() ' EntityIssue.LoadListConsequentialDamageIssue(GetGuidFromString(GetGuidStringFromByteArray(CType(dvRow("case_conseq_damage_id"), Byte()))))
                gridViewConseqDamageIssue.DataBind()
            End If
        End If
    End Sub
    Protected Sub GridViewConseqDamageIssue_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        Try
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim btnEditItem As LinkButton
            If (e.Row.RowType = DataControlRowType.DataRow) _
                OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                If (e.Row.Cells(1).FindControl(SelectActionButton) IsNot Nothing) Then
                    btnEditItem = CType(e.Row.Cells(0).FindControl(SelectActionButton), LinkButton)
                    btnEditItem.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(CaseConseqDamage.ConsequentialDamageIssuesView.COL_ENTITY_ISSUE_ID), Byte()))
                    btnEditItem.CommandName = SelectActionCommand
                    btnEditItem.Text = dvRow(CaseConseqDamage.ConsequentialDamageIssuesView.COL_ISSUE_DESC).ToString
                End If

                '' Convert short status codes to full description with css
                e.Row.Cells(GridConseqdamageIssueColStatusCodeIdx).Text = LookupListNew.GetDescriptionFromCode(ClaimIssueList, dvRow(CaseConseqDamage.ConsequentialDamageIssuesView.COL_STATUS_CODE).ToString)
                If (dvRow(CaseConseqDamage.ConsequentialDamageIssuesView.COL_STATUS_CODE).ToString = Codes.CLAIMISSUE_STATUS__RESOLVED OrElse dvRow(CaseConseqDamage.ConsequentialDamageIssuesView.COL_STATUS_CODE).ToString = Codes.CLAIMISSUE_STATUS__WAIVED) Then
                    e.Row.Cells(GridConseqdamageIssueColStatusCodeIdx).CssClass = "StatActive"
                Else
                    e.Row.Cells(GridConseqdamageIssueColStatusCodeIdx).CssClass = "StatInactive"
                End If

            End If
        Catch ex As Exception
            Page.HandleErrors(ex, Page.MasterPage.MessageController)
        End Try
    End Sub
    Protected Sub GridViewConseqDamageIssue_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        Try
            If e.CommandName = SelectActionCommand Then
                If Not e.CommandArgument.ToString().Equals(String.Empty) Then
                    Dim entityIssueId As Guid
                    entityIssueId = New Guid(e.CommandArgument.ToString())
                    If (Page.NavController IsNot Nothing) Then
                        Page.NavController.Navigate(Page, FlowEvents.EVENT_NEXT, New ClaimIssueDetailForm.Parameters(State.ClaimBo, entityIssueId, Codes.ISSUE_TYPE_CONSEQUENTIAL_DAMAGE))
                    Else
                        Page.callPage(ClaimIssueDetailForm.URL, New ClaimIssueDetailForm.Parameters(State.ClaimBo, entityIssueId, Codes.ISSUE_TYPE_CONSEQUENTIAL_DAMAGE))
                    End If
                End If
            End If
        Catch ex As ThreadAbortException
        Catch ex As Exception
            If (TypeOf ex Is TargetInvocationException) AndAlso
           (TypeOf ex.InnerException Is ThreadAbortException) Then Return
            Page.HandleErrors(ex, Page.MasterPage.MessageController)
        End Try
    End Sub
#End Region
#Region "Fulfillment Web Service - Add Consequential Damage Authorization"
    Private Const ResponseStatusFailure As String = "Failure"
    ''' <summary>
    ''' Gets New Instance of Claim fulfillment Service Client with Credentials Configured
    ''' </summary>
    ''' <returns>Instance of <see cref="FulfillmentServiceClient"/></returns>
    Private Shared Function GetClient() As FulfillmentServiceClient
        Dim oWebPasswd As WebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE__CLAIMS_FULFILLMENT_SERVICE), False)
        Dim client = New FulfillmentServiceClient("CustomBinding_IFulfillmentService", oWebPasswd.Url)
        client.ClientCredentials.UserName.UserName = oWebPasswd.UserId
        client.ClientCredentials.UserName.Password = oWebPasswd.Password
        Return client
    End Function
    Private Sub DisplayWsErrorMessage(errCode As String, errDescription As String)
        Page.MasterPage.MessageController.AddError(errCode & " - " & errDescription, False)
    End Sub
    Private Function CallConseqDamageFulfillmentWs(CaseConseqDamageId As Guid) As Boolean
        Dim wsRequest As ConseqDamageFulfillmentRequest = New ConseqDamageFulfillmentRequest()
        Dim wsResponse As BaseFulfillmentResponse
        Dim blnsuccess As Boolean = True

        wsRequest.CaseConseqDamageId = CaseConseqDamageId

        Try
            wsResponse = WcfClientHelper.Execute(Of FulfillmentServiceClient, IFulfillmentService, BaseFulfillmentResponse)(
                                                               GetClient(),
                                                               New List(Of Object) From {New InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                                                               Function(c As FulfillmentServiceClient)
                                                                   Return c.AddConseqDamageAuthorization(wsRequest)
                                                               End Function)
        Catch ex As Exception
            blnsuccess = False
            Page.MasterPage.MessageController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.GUI_CLAIM_FULFILLMENT_SERVICE_ERR, True)
            Throw
        End Try

        If wsResponse IsNot Nothing Then
            If wsResponse.GetType() Is GetType(BaseFulfillmentResponse) Then
                If wsResponse.ResponseStatus.Equals(ResponseStatusFailure) Then
                    DisplayWsErrorMessage(wsResponse.Error.ErrorCode, wsResponse.Error.ErrorMessage)
                    blnsuccess = False
                Else
                    Page.MasterPage.MessageController.AddSuccess("CONSEQUENTIAL_DAMAGE_AUTH_ADDED", True)
                    blnsuccess = True
                End If
            End If
        End If

        Return blnsuccess

    End Function
#End Region
End Class