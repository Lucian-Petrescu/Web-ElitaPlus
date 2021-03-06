﻿Imports System.Web.Services
Imports System.Globalization
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Imports Assurant.Elita.CommonConfiguration.DataElements

Public Class ClaimPaymentGroupForm
    Inherits ElitaPlusSearchPage

#Region "Constants"
    Public Const URL As String = "ClaimPaymentGroupForm.aspx"
    Public Const UPDATE_ADDRESS_COMMAND As String = "UpdateAddress"
    Public Const DELETE_PAYABLE_COMMAND As String = "DeletePayable"
    Private Const MSG_PAYMENT_GROUP_PAID As String = "MSG_PAYMENT_GROUP_PAID"
    Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    Class MyState
        Public PageIndex As Integer = 0
        Public PageSize As Integer = 10
        Public MyPagedDataSource As New PagedDataSource
        Public searchDV As DataView = Nothing
        Public SearchClicked As Boolean = False
        Public IsGridVisible As Boolean = False
        Public selectedPymntGroupId As Guid = Guid.Empty
        Public selectedPymntGroupDetailId As Guid = Guid.Empty
        Public MyPaymentGroupBO As ClaimPaymentGroup
        Public MyPymntGrpDetailChildBO As ClaimPaymentGroupDetail
        Public SortExpression As String = ClaimPaymentGroup.PaymentGroupSearchDV.COL_NAME_PAYMENT_GROUP_NUMBER
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        'Public IsReadOnly As Boolean = False
        Public SortExpressionItem As String = ClaimPaymentGroupDetail.PaymentGroupDetailSearchDV.COL_NAME_CLAIM_NUMBER

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
                Me.State.selectedPymntGroupId = CType(Me.CallingParameters, Guid)

                If Not Me.State.selectedPymntGroupId.Equals(Guid.Empty) Then
                    Me.State.MyPaymentGroupBO = New ClaimPaymentGroup(Me.State.selectedPymntGroupId)
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region

    Private Sub EnableDisableButtons()
        If Not Me.State.MyPaymentGroupBO Is Nothing Then
            If Me.State.MyPaymentGroupBO.PaymentGroupStatusId = LookupListNew.GetIdFromCode(LookupListNew.LK_PAYMENT_GRP_STAT, _
                                                                                Codes.PYMNT_GRP_STATUS_APPROVED_FOR_PAYMENT) Then
                Me.btnCreatePayment.Enabled = False
                Me.btnAddPayables.Enabled = False
            End If
        Else
            'Its a New Payment Group having no Payables
            Me.btnCreatePayment.Enabled = False
        End If
    End Sub


#Region "Page_Events"

    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        Try
            Me.IsReturningFromChild = True
            If Not Me.ReturnedValues Is Nothing Then
                Me.State.selectedPymntGroupId = CType(Me.ReturnedValues, Guid)
                If Not Me.State.selectedPymntGroupId.Equals(Guid.Empty) Then
                    Me.State.MyPaymentGroupBO = New ClaimPaymentGroup(Me.State.selectedPymntGroupId)
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub UpdateBreadCrum()
        If (Not Me.State Is Nothing) Then
            If (Not Me.State Is Nothing) Then
                Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & _
                    TranslationBase.TranslateLabelOrMessage("PAYMENT_GROUP_SEARCH") & Me.MasterPage.PageTab & ElitaBase.Sperator & _
                    TranslationBase.TranslateLabelOrMessage("PAYMENT_GROUP_DETAIL")
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("PAYMENT_GROUP_DETAIL")
            End If
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.MasterPage.MessageController.Clear()
            If Not Me.IsPostBack Then

                Me.AddCalendar_New(Me.imgExpectedPymntDate, Me.moExpectedPaymentDate)

                EnableDisableButtons()

                If Me.State.MyPaymentGroupBO Is Nothing Then
                    Me.State.MyPaymentGroupBO = New ClaimPaymentGroup()
                End If
                'BindBoPropertiesToLabels()
                UpdateBreadCrum()
                Me.State.MyPagedDataSource = New PagedDataSource
                Me.State.MyPagedDataSource.AllowPaging = True
                Me.State.MyPagedDataSource.PageSize = Me.State.PageSize
                'Me.AddLabelDecorations(Me.State.MyPaymentGroupBO)
            Else
                If ((Not Me.Request("__EVENTARGUMENT") Is Nothing) AndAlso (Me.Request("__EVENTARGUMENT").StartsWith(PAGE_COMMAND_NAME))) Then
                    Me.State.PageIndex = Integer.Parse(Me.Request("__EVENTARGUMENT").Split(New Char() {":"c})(1))
                End If
                CheckIfComingFromDeleteConfirm()
            End If

            Me.State.searchDV = Nothing
            PopulateHeader()
            If Not Me.State.selectedPymntGroupId.Equals(Guid.Empty) Or Me.IsReturningFromChild Then
                PopulateGrid()
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub

    'Protected Sub BindBoPropertiesToLabels()
    '    Try
    '        Me.BindBOPropertyToLabel(Me.State.MyPaymentGroupBO, "PaymentGroupDate", Me.lblPaymentGroupDate)
    '        Me.BindBOPropertyToLabel(Me.State.MyPaymentGroupBO, "PaymentGroupNumber", Me.lblPaymentGroupNumber)
    '        Me.BindBOPropertyToLabel(Me.State.MyPaymentGroupBO, "PaymentGroupTotal", Me.lblPaymentGroupTotal)
    '        Me.BindBOPropertyToLabel(Me.State.MyPaymentGroupBO, "PaymentGroupStatusId", Me.lblPaymentGroupStatus)

    '        Me.ClearGridHeadersAndLabelsErrSign()
    '    Catch ex As Exception
    '        Me.HandleErrors(ex, Me.MasterPage.MessageController)
    '    End Try
    'End Sub

#End Region

#Region "Button Click Handlers"

    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.ReturnToCallingPage()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnAddPayables_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddPayables.Click
        Me.callPage(ClaimSelectPayables.URL, Me.State.selectedPymntGroupId)
    End Sub

    Private Sub btnCreatePayment_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCreatePayment.Click
        Try
            Me.State.MyPaymentGroupBO.processClaimAuthorizations()
            EnableDisableButtons()
            Me.MasterPage.MessageController.AddSuccess(Me.MSG_PAYMENT_GROUP_PAID, True)
            Me.State.searchDV = Nothing
            PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

#End Region

#Region "Web Methods"
    <WebMethod(), Script.Services.ScriptMethod()> _
    Public Shared Function GetClaimAuthLineItems(ByVal claimAuthId As String) As String
        Try
            Dim claimAuthLineItems As DataView
            Dim ds As DataSet
            claimAuthLineItems = ClaimPaymentGroupDetail.GetClaimAuthLineItems(New Guid(claimAuthId))
            ds = claimAuthLineItems.Table.DataSet
            ds.DataSetName = "ClaimAuthLineItemDS"
            ds.Tables(0).TableName = "ClaimAuthLineItems"

            Dim dtHeaders As New DataTable("Headers")
            dtHeaders.Columns.Add("UI_PROG_CODE", GetType(String))
            dtHeaders.Columns.Add("TRANSLATION", GetType(String))
            ds.Tables.Add(dtHeaders)

            dtHeaders.Rows.Add(New String() {"SERVICE_CLASS", TranslationBase.TranslateLabelOrMessage("SERVICE_CLASS")})
            dtHeaders.Rows.Add(New String() {"SERVICE_TYPE", TranslationBase.TranslateLabelOrMessage("SERVICE_TYPE")})
            dtHeaders.Rows.Add(New String() {"LINE_ITEM_NUMBER", TranslationBase.TranslateLabelOrMessage("LINE_ITEM_NUMBER")})
            dtHeaders.Rows.Add(New String() {"AUTHORIZED_AMOUNT", TranslationBase.TranslateLabelOrMessage("AUTHORIZED_AMOUNT")})
            dtHeaders.Rows.Add(New String() {"INVOICE_AMOUNT", TranslationBase.TranslateLabelOrMessage("INVOICE_AMOUNT")})
            dtHeaders.Rows.Add(New String() {"RECONCILED_AMOUNT", TranslationBase.TranslateLabelOrMessage("RECONCILED_AMOUNT")})

            Return ds.GetXml()
        Catch ex As Exception
            Return "Error!"
        End Try
    End Function

#End Region

#Region "Datagrid Related"

    Private Sub PopulateGrid(Optional ByVal updatePageIndex As Boolean = False)
        Dim recCount As Integer
        Try
            If (Me.State.searchDV Is Nothing) Then
                Me.State.searchDV = ClaimPaymentGroupDetail.GetPaymentGroupDetail(Me.State.selectedPymntGroupId)
                updatePageIndex = True
            End If

            recCount = Me.State.searchDV.Count
            If (updatePageIndex) Then
                Me.State.PageIndex = NewCurrentPageIndex(Me.State.PageIndex, Me.State.MyPagedDataSource.PageSize, Me.State.PageSize)
            End If

            Me.State.searchDV.Sort = Me.State.SortExpressionItem
            Me.State.MyPagedDataSource.DataSource = Me.State.searchDV
            Me.State.MyPagedDataSource.AllowPaging = True
            Me.State.MyPagedDataSource.PageSize = Me.State.PageSize
            Me.State.MyPagedDataSource.CurrentPageIndex = Me.State.PageIndex
            Me.moPaymentRepeater.DataSource = Me.State.MyPagedDataSource
            Me.moPaymentRepeater.DataBind()

            If (recCount = 0) Then
                Me.btnCreatePayment.Enabled = False
                Me.MasterPage.MessageController.AddInformation(Message.MSG_NO_RECORDS_FOUND, True)
                ControlMgr.SetVisibleControl(Me, moSearchResults, False)
            Else
                If Not Me.State.MyPaymentGroupBO.PaymentGroupStatusId = LookupListNew.GetIdFromCode(LookupListNew.LK_PAYMENT_GRP_STAT, _
                                                                                                    Codes.PYMNT_GRP_STATUS_APPROVED_FOR_PAYMENT) Then
                    Me.btnCreatePayment.Enabled = True
                End If
                ControlMgr.SetVisibleControl(Me, moSearchResults, True)
                lblRecordCount.Text = recCount & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub PopulateHeader()
        Try
            If Not Me.State.selectedPymntGroupId.Equals(Guid.Empty) Then
                moPaymentGroupNumber.Text = Me.State.MyPaymentGroupBO.PaymentGroupNumber
                moPaymentGroupTotal.Text = Me.State.MyPaymentGroupBO.PaymentGroupTotal.ToString
                moPaymentGroupDate.Text = Me.GetDateFormattedString(CDate(Me.State.MyPaymentGroupBO.PaymentGroupDate))
                moPaymentGroupStatus.Text = LookupListNew.GetDescriptionFromId(LookupListCache.LK_PAYMENT_GRP_STAT, _
                                                                               Me.State.MyPaymentGroupBO.PaymentGroupStatusId)
                moExpectedPaymentDate.Enabled = False
                imgExpectedPymntDate.Visible = False
            Else
                moExpectedPaymentDate.Enabled = True
                imgExpectedPymntDate.Visible = True
                moPaymentGroupNumber.Text = String.Empty
                moPaymentGroupTotal.Text = CStr(0D)
                'New Payment Group's Date is the System Date
                moPaymentGroupDate.Text = Me.GetDateFormattedString(Date.Now)
                'Default Payment Group Status is Open
                moPaymentGroupStatus.Text = LookupListNew.GetDescriptionFromId(LookupListCache.LK_PAYMENT_GRP_STAT, _
                                                                               LookupListNew.GetIdFromCode(LookupListNew.LK_PAYMENT_GRP_STAT, _
                                                                                                           Codes.PYMNT_GRP_STATUS_OPEN))
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub PopulatePaymentStatusDropdown(ByVal DropDown As DropDownList)
        Try
            Dim paymentStatus As ListItem() = CommonConfigManager.Current.ListManager.GetList("PMTGRPSTAT", Thread.CurrentPrincipal.GetLanguageCode(), Nothing)
            DropDown.Populate(paymentStatus, New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = False
                                                   })

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub moPaymentRepeater_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles moPaymentRepeater.ItemCommand
        Dim index As Integer
        Try
            Select Case e.CommandName
                'Case UPDATE_ADDRESS_COMMAND
                '    If Not e.CommandArgument.ToString().Equals(String.Empty) Then
                '        Me.State.selectedPymntGroupDetailId = New Guid(e.CommandArgument.ToString())
                '        'Me.callPage(ClaimPaymentGroupForm.URL, Me.State.selectedPaymentGroupId)
                '        Me.callPage(ServiceCenterForm.URL)
                '    End If
                Case DELETE_PAYABLE_COMMAND
                    If Not e.CommandArgument.ToString().Equals(String.Empty) Then

                        Me.PopulateGrid()
                        'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                        'moPaymentRepeater.SelectedIndex = Me.NO_ROW_SELECTED_INDEX

                        'Save the Id in the Session
                        Me.State.selectedPymntGroupDetailId = New Guid(e.CommandArgument.ToString())

                        Me.DisplayMessage(Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenDeletePromptResponse)
                        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
                    End If
                Case SORT_COMMAND_NAME
                    If Me.State.SortExpressionItem.StartsWith(e.CommandArgument.ToString()) Then
                        If Me.State.SortExpressionItem.StartsWith(e.CommandArgument.ToString() & " DESC") Then
                            Me.State.SortExpressionItem = e.CommandArgument.ToString()
                        Else
                            Me.State.SortExpressionItem = e.CommandArgument.ToString() & " DESC"
                        End If
                    Else
                        Me.State.SortExpressionItem = e.CommandArgument.ToString()
                    End If
                    PopulateGrid()
            End Select
           
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub


    Private Sub DoDelete()
        Dim oInvRecon As InvoiceReconciliation
        Dim oClaimAuthBO As ClaimAuthorization

        Me.State.MyPymntGrpDetailChildBO = Me.State.MyPaymentGroupBO.GetPymntGroupDetailChild(Me.State.selectedPymntGroupDetailId)

        Try
            oClaimAuthBO = New ClaimAuthorization(Me.State.MyPymntGrpDetailChildBO.ClaimAuthorizationId)

            'Get all the Invoice Recons for the Claim Authorization and Mark them back to Reconciled
            For Each ClaimAuthItem As ClaimAuthItem In oClaimAuthBO.ClaimAuthorizationItemChildren
                If Not ClaimAuthItem.InvoiceReconciliationId = Guid.Empty Then
                    oInvRecon = New InvoiceReconciliation(ClaimAuthItem.InvoiceReconciliationId)
                    Me.State.MyPaymentGroupBO.PaymentGroupTotal = Me.State.MyPaymentGroupBO.PaymentGroupTotal.Value - oInvRecon.ReconciledAmount.Value
                    oInvRecon.ReconciliationStatusId = LookupListNew.GetIdFromCode(LookupListNew.LK_INV_RECON_STAT, _
                                                                                   Codes.INVOICE_RECON_STATUS_RECONCILED)
                    oInvRecon.Save()
                    'Else
                    '    Me.MasterPage.MessageController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.BO_IS_DELETED, True)
                    '    Exit Sub
                End If
            Next

            '''Mark the Claim Authorization back to Reconciled from Selected for Payment.
            oClaimAuthBO.ClaimAuthStatus = ClaimAuthorizationStatus.Reconsiled
            oClaimAuthBO.Save()

            Me.State.MyPymntGrpDetailChildBO.Delete()
            Me.State.MyPymntGrpDetailChildBO.Save()
            Me.State.MyPymntGrpDetailChildBO.EndEdit()
            Me.State.selectedPymntGroupDetailId = Guid.Empty

        Catch ex As Exception
            Me.State.MyPaymentGroupBO.RejectChanges()
            Throw ex
        End Try

        Me.State.MyPaymentGroupBO.Save()
        Me.MasterPage.MessageController.AddSuccess(Me.MSG_RECORD_DELETED_OK, True)
        Me.State.searchDV = Nothing
        PopulateGrid()
    End Sub


    Protected Sub moPaymentRepeater_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs) Handles moPaymentRepeater.ItemDataBound

        Select Case e.Item.ItemType
            Case ListItemType.Item, ListItemType.AlternatingItem
                Dim label As Label
                Dim pymntDR As DataRow = DirectCast(e.Item.DataItem, System.Data.DataRowView).Row
                With DirectCast(e.Item.FindControl("lblSvcCenterCode"), LinkButton)
                    .Text = ClaimPaymentGroupDetail.PaymentGroupDetailSearchDV.SvcCode(pymntDR)
                    .CommandArgument = ClaimPaymentGroupDetail.PaymentGroupDetailSearchDV.AuthorizationNumber(pymntDR)
                    .CommandName = "Invoice"
                End With
                DirectCast(e.Item.FindControl("lblClaimNumber"), Label).Text = ClaimPaymentGroupDetail.PaymentGroupDetailSearchDV.ClaimNumber(pymntDR)
                DirectCast(e.Item.FindControl("lblAuthorizationNumber"), Label).Text = ClaimPaymentGroupDetail.PaymentGroupDetailSearchDV.AuthorizationNumber(pymntDR)
                DirectCast(e.Item.FindControl("lblInvoiceNumber"), Label).Text = ClaimPaymentGroupDetail.PaymentGroupDetailSearchDV.InvoiceNumber(pymntDR)
                DirectCast(e.Item.FindControl("lblItemCount"), Label).Text = ClaimPaymentGroupDetail.PaymentGroupDetailSearchDV.Count(pymntDR)
                DirectCast(e.Item.FindControl("lblTotalAmount"), Label).Text = GetAmountFormattedDoubleString(ClaimPaymentGroupDetail.PaymentGroupDetailSearchDV.InvoiceReconciledAmount(pymntDR).Value.ToString)
                DirectCast(e.Item.FindControl("lblInvoiceDate"), Label).Text = GetDateFormattedStringNullable(ClaimPaymentGroupDetail.PaymentGroupDetailSearchDV.InvoiceDate(pymntDR).Value)
                If Not ClaimPaymentGroupDetail.PaymentGroupDetailSearchDV.DueDate(pymntDR) Is Nothing Then
                    DirectCast(e.Item.FindControl("lblInvoiceDueDate"), Label).Text = GetDateFormattedStringNullable(ClaimPaymentGroupDetail.PaymentGroupDetailSearchDV.DueDate(pymntDR).Value)
                Else
                    DirectCast(e.Item.FindControl("lblInvoiceDueDate"), Label).Text = String.Empty
                End If
                If Me.State.MyPaymentGroupBO.PaymentGroupStatusId = LookupListNew.GetIdFromCode(LookupListNew.LK_PAYMENT_GRP_STAT, Codes.PYMNT_GRP_STATUS_APPROVED_FOR_PAYMENT) Then
                    DirectCast(e.Item.FindControl("btnDeletePayable"), LinkButton).Visible = False
                End If
            Case ListItemType.Header
                BaseItemCreated(DirectCast(sender, Repeater), e)
                HighLightSortColumn(DirectCast(e.Item.FindControl("moSvcCenterCodeSort"), LinkButton), Me.State.SortExpressionItem, ClaimPaymentGroupDetail.PaymentGroupDetailSearchDV.COL_NAME_SERVICE_CENTER_CODE)
                HighLightSortColumn(DirectCast(e.Item.FindControl("moClaimNumberSort"), LinkButton), Me.State.SortExpressionItem, ClaimPaymentGroupDetail.PaymentGroupDetailSearchDV.COL_NAME_CLAIM_NUMBER)
                HighLightSortColumn(DirectCast(e.Item.FindControl("moAuthorizationNumberSort"), LinkButton), Me.State.SortExpressionItem, ClaimPaymentGroupDetail.PaymentGroupDetailSearchDV.COL_NAME_AUTHORIZATION_NUMBER)
                HighLightSortColumn(DirectCast(e.Item.FindControl("moInvoiceNumberSort"), LinkButton), Me.State.SortExpressionItem, ClaimPaymentGroupDetail.PaymentGroupDetailSearchDV.COL_NAME_INVOICE_NUMBER)
                HighLightSortColumn(DirectCast(e.Item.FindControl("moInvoiceDateSort"), LinkButton), Me.State.SortExpressionItem, ClaimPaymentGroupDetail.PaymentGroupDetailSearchDV.COL_NAME_INVOICE_DATE)
                HighLightSortColumn(DirectCast(e.Item.FindControl("moTotalAmountSort"), LinkButton), Me.State.SortExpressionItem, ClaimPaymentGroupDetail.PaymentGroupDetailSearchDV.COL_NAME_RECONCILED_AMOUNT)
                HighLightSortColumn(DirectCast(e.Item.FindControl("moItemCountSort"), LinkButton), Me.State.SortExpressionItem, ClaimPaymentGroupDetail.PaymentGroupDetailSearchDV.COL_NAME_COUNT)
                HighLightSortColumn(DirectCast(e.Item.FindControl("moInvoiceDueDateSort"), LinkButton), Me.State.SortExpressionItem, ClaimPaymentGroupDetail.PaymentGroupDetailSearchDV.COL_NAME_DUE_DATE)
            Case ListItemType.Footer
                BaseItemCreated(DirectCast(sender, Repeater), e)
        End Select

    End Sub

#End Region

    Protected Sub cboPageSize_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Me.State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            Me.PopulateGrid(True)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub CheckIfComingFromDeleteConfirm()
        Dim confResponse As String = Me.HiddenDeletePromptResponse.Value
        If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
            If Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete Then
                DoDelete()
                'Clean after consuming the action
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                Me.HiddenDeletePromptResponse.Value = ""
            End If
        ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
            'Clean after consuming the action
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            Me.HiddenDeletePromptResponse.Value = ""
        End If
    End Sub
End Class
