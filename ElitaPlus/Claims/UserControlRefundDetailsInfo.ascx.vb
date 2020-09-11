Imports System.Collections.Generic
Imports System.Reflection
Imports System.Threading
Imports Assurant.Elita.ClientIntegration
Imports Assurant.Elita.ClientIntegration.Headers
Imports Assurant.ElitaPlus.ElitaPlusWebApp.ClaimFulfillmentService
Imports Assurant.ElitaPlus.ElitaPlusWebApp.ElitaPlusPage
Public Class UserControlRefundDetailsInfo
    Inherits UserControl

#Region "Constants"
    Private Const REFUND_METHOD_XCD As String = "refund_method_xcd"
    Private Const REFUND_AMOUNT As String = "refund_amount"
    Private Const REFUND_REQUESTED_DATE As String = "refund_requested_date"
    Private Const REFUND_REQUESTED_BY As String = "refund_requested_by"
    Private Const REFUND_AUTHORIZATION_STATUS As String = "refund_authorization_status"

#End Region

#Region "Page State"
    Class MyState
        Public ClaimBo As List(Of ClaimAuthorization)

        Public ClaimRefundsListDv As DataView

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
    Public Sub PopulateClaimRefundDetails(ByVal oClaim As List(Of ClaimAuthorization))
        State.ClaimBo = oClaim
        PopulateClaimRefundDetailsGrid()
    End Sub
    Public Sub Translate()
        Page.TranslateGridHeader(GridViewRefundDetails)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

    End Sub


    Private Sub PopulateClaimRefundDetailsGrid()
        Try
            Dim claimRefundDatatable As New DataTable()
            claimRefundDatatable.Columns.AddRange(New DataColumn() {New DataColumn(REFUND_METHOD_XCD, GetType(String)),
                                                   New DataColumn(REFUND_AMOUNT, GetType(String)),
                                                   New DataColumn(REFUND_REQUESTED_DATE, GetType(String)),
                                                   New DataColumn(REFUND_REQUESTED_BY, GetType(String)),
                                                   New DataColumn(REFUND_AUTHORIZATION_STATUS, GetType(String))})
            For Each item As ClaimAuthorization In State.ClaimBo
                If item.AuthTypeXcd = Codes.CLAIM_EXTENDED_STATUS_AUTH_TYPE_CREDIT_NOTE And item.PartyTypeXcd = Codes.CLAIM_EXTENDED_STATUS_PARTY_TYPE_CUSTOMER Then
                    For Each lineItem As ClaimAuthItem In item.ClaimAuthorizationItemChildren.Where(Function(i) (i.ServiceTypeCode = Codes.SERVICE_TYPE__DEDUCTIBLE AndAlso i.ServiceClassCode = Codes.SERVICE_CLASS__DEDUCTIBLE))
                        claimRefundDatatable.Rows.Add(item.RefundMethodXcd, Decimal.Negate(item.AuthorizationAmount), GetLongDateFormattedString(lineItem.CreatedDate), item.RefundCreatedBy, item.RefundAuthorizationStatus)
                    Next
                End If
            Next
            State.ClaimRefundsListDv = claimRefundDatatable.DefaultView

            If Not State.ClaimRefundsListDv Is Nothing AndAlso State.ClaimRefundsListDv.Count > 0 Then
                lblRefundRecordCount.Visible = False
                GridViewRefundDetails.DataSource = State.ClaimRefundsListDv
                GridViewRefundDetails.DataBind()
            Else
                lblRefundRecordCount.Visible = True
                lblRefundRecordCount.Text = TranslationBase.TranslateLabelOrMessage(Message.MSG_NO_RECORDS_FOUND)
            End If

        Catch ex As Exception
            Page.HandleErrors(ex, Page.MasterPage.MessageController)
        End Try
    End Sub


End Class