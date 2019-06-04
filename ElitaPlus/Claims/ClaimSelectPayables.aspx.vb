
Public Class ClaimSelectPayables
    Inherits ElitaPlusSearchPage

#Region "Constants"
    Public Const URL As String = "ClaimSelectPayables.aspx"
    Public Const UPDATE_ADDRESS_COMMAND As String = "UpdateAddress"

    Public Const GRID_CHECK_ALL_CLAIM_AUTH_CHECKBOX As String = "chkAllClaimAuths"
    Public Const GRID_CHECK_ADD_CLAIM_AUTH_CHECKBOX As String = "chkAddClaimAuth"
    Public Const GRID_CLAIM_EXCLUDE_DEDUCTIBLE_CHECKBOX As String = "chkExcludeDeductible"
    'Public Const GRID_PYMNTGRP_DETAIL_ID_LABEL As String = "lblPaymentGroupDetailID"
    Public Const GRID_CLAIM_AUTH_ID_LABEL As String = "lblClaimAuthID"
    Public Const GRID_INVOICE_ID_LABEL As String = "lblInvoiceID"
    Public Const GRID_DESCRIPTION_LABEL As String = "lblSvcCenterName"
    Public Const GRID_CLAIM_NUMBER_LABEL As String = "lblClaimNumber"
    Public Const GRID_AUTHORIZATION_NUMBER_LABEL As String = "lblAuthorizationNumber"
    Public Const GRID_INVOICE_NUMBER_LABEL As String = "lblInvoiceNumber"
    Public Const GRID_INVOICE_NUMBER_DATE As String = "lblInvoiceDate"
    Public Const GRID_RECONCILED_AMOUNT_LABEL As String = "lblTotalAmount"
    Public Const GRID_DUE_DATE_LABEL As String = "lblInvoiceDueDate"

    Public Const CLAIM_AUTH_TOOLTIP As String = "Claim Authorization can not be Paid in Elita"

    Public Const GRID_CLAIM_COL_PGD_ID_IDX As Integer = 0
    Public Const GRID_CLAIM_COL_SELECTED_IDX As Integer = 1
    Public Const GRID_CLAIM_COL_EXCLUDE_DEDUCTIBLE_IDX As Integer = 2
    Public Const GRID_CLAIM_COL_DESCRIPTION_IDX As Integer = 3
    Public Const GRID_CLAIM_COL_CLAIM_NUMBER_IDX As Integer = 4
    Public Const GRID_CLAIM_COL_AUTHORIZATION_NUMBER_IDX As Integer = 5
    Public Const GRID_CLAIM_COL_INVOICE_NUMBER_IDX As Integer = 6
    Public Const GRID_CLAIM_COL_INVOICE_DATE_IDX As Integer = 7
    Public Const GRID_CLAIM_COL_RECONCILED_AMOUNT_IDX As Integer = 8
    Public Const GRID_CLAIM_COL_DUE_DATE_IDX As Integer = 9

#End Region

#Region "Page State"

    Class MyState
        Public PageIndex As Integer = 0
        Public PageSize As Integer = 10
        Public searchDV As DataView = Nothing
        Public MyClaimAuthDV As DataView = Nothing
        Public SearchClicked As Boolean = False
        Public IsGridVisible As Boolean = False
        Public PymntGroupId As Guid = Guid.Empty
        Public MyPaymentGroupBO As ClaimPaymentGroup
        Public MyPymntGrpDetailChildBO As ClaimPaymentGroupDetail
        Public SortExpression As String = ClaimPaymentGroupDetail.PaymentGroupDetailSearchDV.COL_NAME_DUE_DATE & " DESC"

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

#Region "Page Parameters"

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                Me.State.PymntGroupId = CType(Me.CallingParameters, Guid)

                If Not Me.State.PymntGroupId.Equals(Guid.Empty) Then
                    Me.State.MyPaymentGroupBO = New ClaimPaymentGroup(Me.State.PymntGroupId)
                Else
                    Me.State.MyPaymentGroupBO = New ClaimPaymentGroup()
                    Me.State.MyPaymentGroupBO.CompanyId = ElitaPlusIdentity.Current.ActiveUser.CompanyId
                    Me.State.MyPaymentGroupBO.PaymentGroupNumber = "1" 'This is a temporary Number assignment for New PaymentGroup
                    Me.State.MyPaymentGroupBO.PaymentGroupStatusId = LookupListNew.GetIdFromCode(LookupListNew.LK_PAYMENT_GRP_STAT, _
                                                                                                 Codes.PYMNT_GRP_STATUS_OPEN)
                    Me.State.MyPaymentGroupBO.PaymentGroupDate = Date.Now
                    Me.State.MyPaymentGroupBO.PaymentGroupTotal = 0D
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region


#Region "Page Events"

    Private Sub UpdateBreadCrum()
        If (Not Me.State Is Nothing) Then
            If (Not Me.State Is Nothing) Then
                Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & _
                    TranslationBase.TranslateLabelOrMessage("PAYMENT_GROUP_SEARCH") & Me.MasterPage.PageTab & ElitaBase.Sperator & _
                    TranslationBase.TranslateLabelOrMessage("PAYMENT_GROUP_DETAIL") & Me.MasterPage.PageTab & ElitaBase.Sperator & _
                    TranslationBase.TranslateLabelOrMessage("SELECT_PAYABLES")
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("SELECT_PAYABLES")
            End If
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.MasterPage.MessageController.Clear()
        UpdateBreadCrum()
        If Not Page.IsPostBack Then
            TranslateGridHeader(Grid)
        End If
        'PopulateGrid()
    End Sub
#End Region

#Region "Button Click Handlers"
    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.ReturnToCallingPage()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        Dim hasErrors As Boolean = False
        If (Not moInvoiceDateRange.IsEmpty) Then hasErrors = hasErrors Or Not moInvoiceDateRange.Validate()
        If (hasErrors) Then Exit Sub

        Me.State.SearchClicked = True
        Me.State.searchDV = Nothing
        PopulateGrid()
    End Sub

    Private Sub btnContinue_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnContinue.Click
        Dim payableSelected As Boolean = False
        Dim chk As CheckBox
        Dim pgdRow As GridViewRow
        'Check if there is atleast one selection Made otherwise show Error Message
        Try
            'Loop through the visible items in the grid and check the status of the checkbox on each one.
            For Each pgdRow In Grid.Rows
                chk = CType(pgdRow.FindControl(Me.GRID_CHECK_ADD_CLAIM_AUTH_CHECKBOX), CheckBox)
                If Not chk Is Nothing And chk.Checked = True Then
                    payableSelected = True
                    Exit For
                End If
            Next

            If Not payableSelected Then
                Me.MasterPage.MessageController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_NO_RECORD_SELECTED, True)
                Exit Sub
            End If

            AddClaims()

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    'loop thru all records in the Grid
    'For each record create a new payment group detail object
    'Based on user's selection or deselection, Assign the Invioce Recon Id to the payment group detail object
    'Save the payment group detail object
    Private Sub AddClaims(Optional ByVal pageIndexChanging As Boolean = False)
        Dim pgdRow As GridViewRow
        Dim chk As CheckBox
        Dim chkExcludeDed As CheckBox
        Dim claimAuthId As Guid
        Dim paymentAmount As DecimalType = 0D
        Dim claimAuthReconAmount As DecimalType = 0D
        Dim excludeDed As String
        Try
            'Loop through the visible items in the grid and check the status of the checkbox on each one.
            For Each pgdRow In Grid.Rows
                claimAuthReconAmount = 0D
                chk = CType(pgdRow.FindControl(Me.GRID_CHECK_ADD_CLAIM_AUTH_CHECKBOX), CheckBox)
                chkExcludeDed = CType(pgdRow.FindControl(Me.GRID_CLAIM_EXCLUDE_DEDUCTIBLE_CHECKBOX), CheckBox)
                If Not chk Is Nothing Then
                    claimAuthId = New Guid(CType(pgdRow.FindControl(Me.GRID_CLAIM_AUTH_ID_LABEL), Label).Text)
                    claimAuthReconAmount = New DecimalType(CDec(CType(pgdRow.FindControl(Me.GRID_RECONCILED_AMOUNT_LABEL), Label).Text))

                    If chk.Checked = True Then

                        If Not chkExcludeDed Is Nothing And chkExcludeDed.Checked = True Then
                            excludeDed = "Y"
                        Else
                            excludeDed = "N"
                        End If

                        Me.State.MyPaymentGroupBO.CreatePaymentGroupDetail(claimAuthId, excludeDed, claimAuthReconAmount, paymentAmount)

                    End If
                End If
            Next

            Me.State.MyPaymentGroupBO.PaymentGroupTotal = Me.State.MyPaymentGroupBO.PaymentGroupTotal.Value + paymentAmount.Value

            If Not pageIndexChanging Then
                Me.State.MyPaymentGroupBO.Save()
                Me.ReturnToCallingPage(Me.State.MyPaymentGroupBO.Id)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region


#Region "Datagrid Related"

    Private Sub PopulateGrid()
        Try
            If Me.State.searchDV Is Nothing Then

                Me.State.searchDV = ClaimPaymentGroupDetail.SelectPayables(Me.moClaimNumber.Text, _
                                                                           Me.moInvoiceGroupNumber.Text, _
                                                                           Me.moInvoiceNumber.Text, Me.moMobileNumber.Text, _
                                                                           DirectCast(moInvoiceDateRange.Value, SearchCriteriaStructType(Of Date)), _
                                                                           Me.moAccountNumber.Text, Me.moServiceCenter.Text, _
                                                                           Me.moAuthNumber.Text)
                If Me.State.SearchClicked Then
                    Me.ValidSearchResultCountNew(Me.State.searchDV.Count, True)
                    Me.State.SearchClicked = False
                End If
            End If

            'Following Code can look up for any Claims that are still Pending Review for Payment.
            'If Me.State.searchDV.Count > 0 Then
            '    'Loop through the Claims and find if there are any claims that have claim status in 
            '    'the'pending for payment' status. If so, display an error Message to the User
            '    Dim i As Integer
            '    Dim claimAuthId As Guid
            '    Dim objclaimAuth As ClaimAuthorization
            '    For i = 0 To Me.State.searchDV.Count - 1
            '        claimAuthId = New Guid(CType(Me.State.searchDV(i)(ClaimPaymentGroupDetail.PaymentGroupDetailSearchDV.COL_NAME_AUTHORIZATION_ID), Byte()))
            '        objclaimAuth = DirectCast(New ClaimAuthorization(claimAuthId), ClaimAuthorization)
            '        'Get the latest Claim Status for this Claim and Check if it is 'Pending Review for Payment'
            '        Dim maxClaimStatus As ClaimStatus = ClaimStatus.GetLatestClaimStatus(objclaimAuth.Claim_Id)
            '        If Not maxClaimStatus Is Nothing AndAlso maxClaimStatus.StatusCode = Codes.CLAIM_EXTENDED_STATUS__PENDING_REVIEW_FOR_PAYMENT Then
            '            Me.DisplayMessage(Message.MSG_PAY_INVOICE_ALERT_FOR_CLAIM_PENDING_REVIEW, "", Me.MSG_BTN_OK, Me.MSG_TYPE_ALERT, , True)
            '            Exit For
            '        End If
            '    Next
            'End If

            Me.Grid.AutoGenerateColumns = False
            Grid.DataSource = Me.State.searchDV

            If (Not Me.State.SortExpression.Equals(String.Empty)) Then
                Me.State.searchDV.Sort = Me.State.SortExpression 'Me.SortDirection
            End If

            HighLightSortColumn(Grid, Me.State.SortExpression, True) 'Me.SortDirection

            Grid.DataBind()

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
        Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
        Dim claimAuthId As Guid
        Dim objclaimAuth As ClaimAuthorization
        Try
            If (e.Row.RowType = DataControlRowType.Header) Then
                'adding an attribute for onclick event on the check box in the header 
                'and passing the ClientID of the Select All checkbox
                Dim chkAll As CheckBox
                chkAll = CType(e.Row.Cells(Me.GRID_CLAIM_COL_SELECTED_IDX).FindControl(Me.GRID_CHECK_ALL_CLAIM_AUTH_CHECKBOX), CheckBox)
                If Not chkAll Is Nothing Then
                    chkAll.Attributes("onClick") = "javascript:SelectAll('" & chkAll.ClientID & "')"
                End If
            End If

            If Not dvRow Is Nothing Then
                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then

                    Dim txtDueDate As Label
                    txtDueDate = CType(e.Row.Cells(Me.GRID_CLAIM_COL_DUE_DATE_IDX).FindControl(Me.GRID_DUE_DATE_LABEL), Label)
                    If Not txtDueDate Is Nothing AndAlso Not dvRow(ClaimPaymentGroupDetail.PaymentGroupDetailSearchDV.COL_NAME_DUE_DATE) Is DBNull.Value Then
                        txtDueDate.Text = GetDateFormattedStringNullable(CType(dvRow(ClaimPaymentGroupDetail.PaymentGroupDetailSearchDV.COL_NAME_DUE_DATE), Date))
                    End If

                    claimAuthId = New Guid(CType(dvRow(ClaimPaymentGroupDetail.PaymentGroupDetailSearchDV.COL_NAME_AUTHORIZATION_ID), Byte()))
                    objclaimAuth = DirectCast(New ClaimAuthorization(claimAuthId), ClaimAuthorization)

                    Dim chkSelectClaimAuth As CheckBox
                    chkSelectClaimAuth = CType(e.Row.Cells(Me.GRID_CLAIM_COL_SELECTED_IDX).FindControl(Me.GRID_CHECK_ADD_CLAIM_AUTH_CHECKBOX), CheckBox)
                    If Not chkSelectClaimAuth Is Nothing Then
                        'Get the latest Claim Status for this Claim and Check if it is 'Pending Review for Payment'
                        Dim maxClaimStatus As ClaimStatus = ClaimStatus.GetLatestClaimStatus(objclaimAuth.Claim_Id)
                        If Not maxClaimStatus Is Nothing AndAlso maxClaimStatus.StatusCode = Codes.CLAIM_EXTENDED_STATUS__PENDING_REVIEW_FOR_PAYMENT Then
                            ControlMgr.SetEnableControl(Me, chkSelectClaimAuth, False)
                            chkSelectClaimAuth.ToolTip = CLAIM_AUTH_TOOLTIP
                        End If
                    End If

                    Dim chkExcludeDed As CheckBox
                    chkExcludeDed = CType(e.Row.Cells(Me.GRID_CLAIM_COL_EXCLUDE_DEDUCTIBLE_IDX).FindControl(Me.GRID_CLAIM_EXCLUDE_DEDUCTIBLE_CHECKBOX), CheckBox)
                    If Not chkExcludeDed Is Nothing Then
                        chkExcludeDed.ToolTip = String.Empty
                        If Not objclaimAuth.ContainsDeductible Then
                            ControlMgr.SetEnableControl(Me, chkExcludeDed, False)
                            chkExcludeDed.ToolTip = "Claim Auth does not contain Deductble"
                        End If
                        Dim noId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_N)
                        If chkExcludeDed.ToolTip.Equals(String.Empty) AndAlso objclaimAuth.PayDeductibleId.Equals(noId) Then
                            ControlMgr.SetEnableControl(Me, chkExcludeDed, False)
                            chkExcludeDed.ToolTip = "Dealer Does Not Pay Deductble"
                        End If
                        Me.PopulateControlFromBOProperty(chkExcludeDed, dvRow(ClaimPaymentGroupDetail.PaymentGroupDetailSearchDV.COL_NAME_EXCLUDE_DEDUCTIBLE))
                    End If

                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Grid_PageIndexChanging(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging

        AddClaims(True)
        Try
            Me.State.PageIndex = e.NewPageIndex
            Me.Grid.PageIndex = Me.State.PageIndex
            Me.PopulateGrid()
            Me.Grid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
        Try
            If Me.State.SortExpression.StartsWith(e.SortExpression) Then
                If Me.State.SortExpression.EndsWith(" DESC") Then
                    Me.State.SortExpression = e.SortExpression
                Else
                    Me.State.SortExpression &= " DESC"
                End If
            Else
                Me.State.SortExpression = e.SortExpression
            End If

            Me.State.PageIndex = 0
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region


    Private Sub btnClearSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
        Me.State.SearchClicked = False
        Me.moClaimNumber.Text = String.Empty
        Me.moInvoiceGroupNumber.Text = String.Empty
        Me.moInvoiceNumber.Text = String.Empty
        Me.moMobileNumber.Text = String.Empty
        Me.moAccountNumber.Text = String.Empty
        Me.moServiceCenter.Text = String.Empty
        Me.moAuthNumber.Text = String.Empty
        Me.moInvoiceDateRange.Clear()
    End Sub

End Class
