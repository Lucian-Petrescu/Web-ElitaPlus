Imports System
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.Web.Forms
Imports Assurant.ElitaPlus.ElitaPlusWebApp.ElitaPlusPage
Imports Assurant.ElitaPlus.Security

Public Class UserControlAuthorizationInfo
    Inherits System.Web.UI.UserControl


#Region "Constants"

    Public Const SourceViewState As String = "AuthorizationInfoVS"

    Public Const GRID_COL_COMMANDS As Integer = 8
    Public Const GRID_CONTROL_EDIT As String = "EditButton_WRITE"
    Public Const GRID_CONTROL_DELETE As String = "DeleteButton_WRITE"
    Public Const GRID_CONTROL_SAVE As String = "btnSave"
    Public Const GRID_CONTROL_CANCEL As String = "btnCancel"

    'Related Equipment
    Public Const GRID_COL_AUTHORIZATION_ITEM_ID_IDX As Integer = 0
    Public Const GRID_COL_LINE_ITEM_NUMBER_IDX As Integer = 1
    Public Const GRID_COL_SERVICE_CLASS_IDX As Integer = 2
    Public Const GRID_COL_SERVICE_TYPE_IDX As Integer = 3
    Public Const GRID_COL_VENDOR_SKU_IDX As Integer = 4
    Public Const GRID_COL_VENDOR_SKU_DESCRIPTION_IDX As Integer = 5
    Public Const GRID_COL_AMOUNT_IDX As Integer = 6
    Public Const GRID_COL_ADJUSTMENT_REASON_IDX As Integer = 7

    Public Const TABLE_KEY_NAME As String = "claim_auth_item_id"

    Public Const COL_NAME_CLAIM_AUTH_ITEM_ID As String = "claim_auth_item_id"
    Public Const COL_NAME_CLAIM_AUTHORIZATION_ID As String = "claim_authorization_id"
    Public Const COL_NAME_SERVICE_CLASS_ID As String = "service_class_id"
    Public Const COL_NAME_SERVICE_TYPE_ID As String = "service_type_id"
    Public Const COL_NAME_LINE_ITEM_NUMBER As String = "line_item_number"
    Public Const COL_NAME_VENDOR_SKU As String = "vendor_sku"
    Public Const COL_NAME_VENDOR_SKU_DESCRIPTION As String = "vendor_sku_description"
    Public Const COL_NAME_AMOUNT As String = "amount"
    Public Const COL_NAME_ADJUSTMENT_REASON_ID As String = "adjustment_reason_id"

    'control in Grid

    Public Const GRID_CONTROL_SERVICE_CLASS_DDL As String = "ddlService_class"
    Public Const GRID_CONTROL_SERVICE_TYPE_DDL As String = "ddlService_type"
    Public Const GRID_CONTROL_VENDOR_SKU_txt As String = "txtvendor_sku"
    Public Const GRID_CONTROL_VENDOR_SKU_DESCRIPTION_txt As String = "txtvendor_sku_description"
    Public Const GRID_CONTROL_AMOUNT_txt As String = "txtAMOUNT"

    Public Const GRID_CONTROL_ADJUSTMENT_REASON_DDL As String = "ddlAdjustment_reason"
    Public Const GRID_label_ADJUSTMENT_REASON As String = "lblAdjustment_Reason"
    Public Const GRID_CONTROL_ADJUSTMENT_REASON_txt As String = "txtAdjustment_Reason"
       
    Public Const GRID_LABEL_AUTHORIZATION_ITEM_ID As String = "lblAuthorization_item_ID"
    Public Const GRID_LABEL_SERVICE_CLASS As String = "lblService_class"
    Public Const GRID_Label_Service_type As String = "lblService_type"
    Public Const GRID_LABEL_VENDOR_SKU As String = "lblvendor_sku"
    Public Const GRID_Label_VENDOR_SKU_DESCRIPTION As String = "lblvendor_sku_description"
    Public Const GRID_Label_AMOUNT As String = "lblAMOUNT"


    'Actions
    Private Const ACTION_NONE As String = "ACTION_NONE"
    Private Const ACTION_SAVE As String = "ACTION_SAVE"
    Private Const ACTION_CANCEL_DELETE As String = "ACTION_CANCEL_DELETE"
    Private Const ACTION_EDIT As String = "ACTION_EDIT"
    Private Const ACTION_NEW As String = "ACTION_NEW"

    Private Const MSG_MULTIPLE_SKU As String = "Multiple Vendor SKU found"
#End Region


#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    Class MyState
        Public oCA As ClaimAuthorization

        Public isNewItem As Boolean

        Public isEditMode As Boolean

        Public DeletionIndex As Guid = Guid.Empty


        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
    End Class


    Public ReadOnly Property State() As MyState
        Get
            If Me.Page.StateSession.Item(Me.UniqueID) Is Nothing Then
                Me.Page.StateSession.Item(Me.UniqueID) = New MyState
            End If
            Return CType(Me.Page.StateSession.Item(Me.UniqueID), MyState)
        End Get
    End Property

    Private Shadows ReadOnly Property Page() As ElitaPlusSearchPage
        Get
            Return CType(MyBase.Page, ElitaPlusSearchPage)
        End Get
    End Property


#End Region

    ' This is the initialization Method
    Public Sub InitController(ByVal oClaimAuthorization As ClaimAuthorization, Optional ByVal isEditable As Boolean = True)
        'ViewState(SourceViewState) = oClaimAuthorization
        Me.State.oCA = oClaimAuthorization
        Me.State.isEditMode = isEditable
        PopulateFormFromClaimCtrl(Me.State.oCA)
        Me.AuthorizationGrid.Columns(GRID_COL_COMMANDS).Visible = isEditable

        BtnNew_WRITE.Visible = isEditable
        BtnNew_WRITE.Enabled = isEditable

    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Page.ClearGridViewHeadersAndLabelsErrSign()
        If Me.Page.IsPostBack Then
            CheckIfComingFromDeleteConfirm()
        End If
    End Sub

    Public Sub BindBOProperties(ByVal claimAuthItem As ClaimAuthItem)
        Me.Page.BindBOPropertyToGridHeader(claimAuthItem, "ServiceClassId", Me.AuthorizationGrid.Columns(GRID_COL_SERVICE_CLASS_IDX))
        Me.Page.BindBOPropertyToGridHeader(claimAuthItem, "VendorSku", Me.AuthorizationGrid.Columns(GRID_COL_VENDOR_SKU_IDX))
        Me.Page.BindBOPropertyToGridHeader(claimAuthItem, "VendorSkuDescription", Me.AuthorizationGrid.Columns(GRID_COL_VENDOR_SKU_DESCRIPTION_IDX))
        Me.Page.BindBOPropertyToGridHeader(claimAuthItem, "Amount", Me.AuthorizationGrid.Columns(GRID_COL_AMOUNT_IDX))
        Me.Page.BindBOPropertyToGridHeader(claimAuthItem, "AdjustmentReasonId", Me.AuthorizationGrid.Columns(GRID_COL_ADJUSTMENT_REASON_IDX))
    End Sub

    Public Sub Translate()
        Me.Page.TranslateGridHeader(AuthorizationGrid)

    End Sub

    Private Sub PopulateFormFromClaimCtrl(ByVal oClaimAuthorization As ClaimAuthorization)
        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        Dim LineItem As ClaimAuthItem
        Try
            With oClaimAuthorization

                If Not Me.State.isEditMode Then ResetIndexes()
                Me.AuthorizationGrid.DataSource = oClaimAuthorization.ClaimAuthorizationItemChildren.Where(Function(i) i.IsDeleted = False)
                Me.AuthorizationGrid.DataBind()

            End With
        Catch ex As Exception
            Me.Page.HandleErrors(ex, Me.Page.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub AuthorizationGrid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles AuthorizationGrid.RowCommand
        Try
            Dim nIndex As Integer
            nIndex = CInt(e.CommandArgument)
            If e.CommandName = ElitaPlusSearchPage.EDIT_COMMAND_NAME Then

                Me.State.isEditMode = True
                Me.State.isNewItem = False
                AuthorizationGrid.EditIndex = nIndex
                AuthorizationGrid.DataSource = Me.State.oCA.ClaimAuthorizationItemChildren.Where(Function(i) i.IsDeleted = False)
                AuthorizationGrid.DataBind()
                SetControls(ControlClicked.EditAuthItem)
            ElseIf e.CommandName = ElitaPlusSearchPage.DELETE_COMMAND_NAME Then

                Me.AuthorizationGrid.DataSource = Me.State.oCA.ClaimAuthorizationItemChildren.Where(Function(i) i.IsDeleted = False)
                Me.AuthorizationGrid.DataBind()

                'Save the Id in the Session
                Me.State.DeletionIndex = New Guid(AuthorizationGrid.Rows(nIndex).Cells(Me.GRID_COL_AUTHORIZATION_ITEM_ID_IDX).Text.ToString())

                Me.Page.DisplayMessage(Message.DELETE_RECORD_PROMPT, "", Me.Page.MSG_BTN_YES_NO, Me.Page.MSG_TYPE_CONFIRM, Me.HiddenDeletePromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete

            ElseIf e.CommandName = ElitaPlusSearchPage.SAVE_COMMAND_NAME Then
                SaveChanges()
                SetControls(ControlClicked.SaveAuthItem)
            ElseIf e.CommandName = ElitaPlusSearchPage.CANCEL_COMMAND_NAME Then
                CancelItem(nIndex)
                SetControls(ControlClicked.Cancel)
            End If

        Catch ex As Exception
            Me.Page.HandleErrors(ex, Me.Page.MasterPage.MessageController)
        End Try
    End Sub

    Private Function DeleteAuthItem(ByVal AuthItemId As Guid) As Boolean
        Dim bIsOk As Boolean = True
        Dim claimAuthitem As ClaimAuthItem


        claimAuthitem = CType(Me.State.oCA.ClaimAuthorizationItemChildren.GetChild(AuthItemId), ClaimAuthItem)

        If claimAuthitem.IsNew Then
            claimAuthitem.Delete()
        Else
            claimAuthitem.IsDeleted = True
        End If


        'Me.State.oCA.Save()
        Me.State.isNewItem = False
        Return bIsOk
    End Function

    Public Sub ServiceClassSelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ddl As DropDownList
        Dim txt As TextBox
        Dim ServiceClassId As Guid = Guid.Empty

        If Not (DBNull.Value.Equals(DirectCast(sender, System.Web.UI.WebControls.DropDownList).SelectedValue)) Then
            ServiceClassId = Me.Page.GetGuidFromString(DirectCast(sender, System.Web.UI.WebControls.DropDownList).SelectedValue)
            If (Not AuthorizationGrid.Rows(AuthorizationGrid.EditIndex).Cells(Me.GRID_COL_SERVICE_TYPE_IDX).FindControl(GRID_CONTROL_SERVICE_TYPE_DDL) Is Nothing) Then
                ddl = CType(AuthorizationGrid.Rows(AuthorizationGrid.EditIndex).Cells(Me.GRID_COL_SERVICE_TYPE_IDX).FindControl(GRID_CONTROL_SERVICE_TYPE_DDL), DropDownList)
                'Me.Page.BindListControlToDataView(ddl, LookupListNew.GetNewServiceTypeByServiceClassLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId, ServiceClassId))

                Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
                oListContext.ServiceClassId = ServiceClassId
                oListContext.LanguageId = Thread.CurrentPrincipal.GetLanguageId()
                Dim oServiceTypeList As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="ServiceTypeByServiceClass", context:=oListContext)
                ddl.Populate(oServiceTypeList, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })

                If (Not AuthorizationGrid.Rows(AuthorizationGrid.EditIndex).Cells(Me.GRID_COL_VENDOR_SKU_IDX).FindControl(GRID_CONTROL_VENDOR_SKU_txt) Is Nothing) Then
                    txt = CType(AuthorizationGrid.Rows(AuthorizationGrid.EditIndex).Cells(Me.GRID_COL_VENDOR_SKU_IDX).FindControl(GRID_CONTROL_VENDOR_SKU_txt), TextBox)
                    txt.Text = String.Empty
                End If
                If (Not AuthorizationGrid.Rows(AuthorizationGrid.EditIndex).Cells(Me.GRID_COL_VENDOR_SKU_DESCRIPTION_IDX).FindControl(GRID_CONTROL_VENDOR_SKU_DESCRIPTION_txt) Is Nothing) Then
                    txt = CType(AuthorizationGrid.Rows(AuthorizationGrid.EditIndex).Cells(Me.GRID_COL_VENDOR_SKU_DESCRIPTION_IDX).FindControl(GRID_CONTROL_VENDOR_SKU_DESCRIPTION_txt), TextBox)
                    txt.Text = String.Empty
                End If
            End If
        End If
    End Sub

    Public Sub ServiceTypeSelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim txt As TextBox
        Dim ServiceClassId As Guid = Guid.Empty
        Dim ServiceTypeId As Guid = Guid.Empty
        Dim dv As PriceListDetail.PriceListResultsDV

        Try
            If Not (DBNull.Value.Equals(DirectCast(sender, System.Web.UI.WebControls.DropDownList).SelectedValue)) Then
                ServiceClassId = Me.Page.GetGuidFromString(CType(AuthorizationGrid.Rows(AuthorizationGrid.EditIndex).Cells(Me.GRID_COL_SERVICE_CLASS_IDX).FindControl(GRID_CONTROL_SERVICE_CLASS_DDL), DropDownList).SelectedValue)
                ServiceTypeId = Me.Page.GetGuidFromString(DirectCast(sender, System.Web.UI.WebControls.DropDownList).SelectedValue)

                Dim equipmentId As Guid, equipmentclassId As Guid, conditionId As Guid
                If (Me.State.oCA.Claim.Dealer.UseEquipmentId = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y")) Then
                    If (Not Me.State.oCA.Claim.ClaimedEquipment Is Nothing) Then
                        equipmentId = Me.State.oCA.Claim.ClaimedEquipment.EquipmentId
                        equipmentclassId = Me.State.oCA.Claim.ClaimedEquipment.EquipmentBO.EquipmentClassId
                        conditionId = LookupListNew.GetIdFromCode(LookupListNew.LK_CONDITION, Codes.EQUIPMENT_COND__NEW)
                    Else
                        Throw New GUIException("NO_CLAIMED_EQUIPMENT_FOUND", "NO_CLAIMED_EQUIPMENT_FOUND")
                    End If
                End If
                dv = PriceListDetail.GetPricesForServiceType(Me.State.oCA.Claim.CompanyId, Me.State.oCA.ServiceCenterObject.Code, Me.State.oCA.Claim.RiskTypeId,
                                                                                                 CType(Me.State.oCA.Claim.LossDate, Date), CType(Me.State.oCA.Claim.Certificate.SalesPrice, Decimal),
                                                                                                 ServiceClassId,
                                                                                                 ServiceTypeId,
                                                                                                 equipmentclassId, equipmentId, conditionId,
                                                                                                 Me.State.oCA.Claim.Dealer.Id, String.Empty)
                If dv Is Nothing OrElse dv.Count = 0 Then
                    Throw New GUIException(Messages.PRICE_LIST_NOT_FOUND, Messages.PRICE_LIST_NOT_FOUND, Nothing)
                ElseIf (dv.Count = 1) Then
                    If (Not AuthorizationGrid.Rows(AuthorizationGrid.EditIndex).Cells(Me.GRID_COL_VENDOR_SKU_IDX).FindControl(GRID_CONTROL_VENDOR_SKU_txt) Is Nothing) Then
                        txt = CType(AuthorizationGrid.Rows(AuthorizationGrid.EditIndex).Cells(Me.GRID_COL_VENDOR_SKU_IDX).FindControl(GRID_CONTROL_VENDOR_SKU_txt), TextBox)
                        txt.Text = dv(0)(PriceListDetail.PriceListResultsDV.COL_NAME_VENDOR_SKU).ToString
                    End If
                    If (Not AuthorizationGrid.Rows(AuthorizationGrid.EditIndex).Cells(Me.GRID_COL_VENDOR_SKU_DESCRIPTION_IDX).FindControl(GRID_CONTROL_VENDOR_SKU_DESCRIPTION_txt) Is Nothing) Then
                        txt = CType(AuthorizationGrid.Rows(AuthorizationGrid.EditIndex).Cells(Me.GRID_COL_VENDOR_SKU_DESCRIPTION_IDX).FindControl(GRID_CONTROL_VENDOR_SKU_DESCRIPTION_txt), TextBox)
                        txt.Text = dv(0)(PriceListDetail.PriceListResultsDV.COL_NAME_VENDOR_SKU_DESC).ToString
                    End If
                ElseIf (dv.Count > 1) Then
                    Throw New GUIException(MSG_MULTIPLE_SKU, MSG_MULTIPLE_SKU, Nothing)
                End If
            End If
        Catch ex As Exception
            Me.Page.HandleErrors(ex, Me.Page.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub AuthorizationGrid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles AuthorizationGrid.RowDataBound
        Dim CAItem As ClaimAuthItem = CType(e.Row.DataItem, ClaimAuthItem)
        Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
        Dim lbl As Label
        Dim txt As TextBox
        Dim ddl As DropDownList
        Try
            If Not CAItem Is Nothing Then

                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Or itemType = ListItemType.EditItem Then
                    If Me.State.isEditMode And AuthorizationGrid.EditIndex = e.Row.RowIndex Then

                        e.Row.Cells(Me.GRID_COL_AUTHORIZATION_ITEM_ID_IDX).Text = Page.GetGuidStringFromByteArray(CAItem.Id.ToByteArray)
                        e.Row.Cells(Me.GRID_COL_LINE_ITEM_NUMBER_IDX).Text = CAItem.LineItemNumber.ToString
                        If (Not e.Row.Cells(Me.GRID_COL_AMOUNT_IDX).FindControl(GRID_CONTROL_AMOUNT_txt) Is Nothing) Then
                            txt = CType(e.Row.Cells(Me.GRID_COL_AMOUNT_IDX).FindControl(GRID_CONTROL_AMOUNT_txt), TextBox)
                            txt.Text = If(CAItem.Amount Is Nothing, String.Empty, Me.Page.GetAmountFormattedString(CAItem.Amount.Value))
                        End If
                        If (Not e.Row.Cells(Me.GRID_COL_VENDOR_SKU_IDX).FindControl(GRID_CONTROL_VENDOR_SKU_txt) Is Nothing) Then
                            txt = CType(e.Row.Cells(Me.GRID_COL_VENDOR_SKU_IDX).FindControl(GRID_CONTROL_VENDOR_SKU_txt), TextBox)
                            txt.Text = CType(If(DBNull.Value.Equals(CAItem.VendorSku), String.Empty, CAItem.VendorSku), String)
                        End If
                        If (Not e.Row.Cells(Me.GRID_COL_VENDOR_SKU_DESCRIPTION_IDX).FindControl(GRID_CONTROL_VENDOR_SKU_DESCRIPTION_txt) Is Nothing) Then
                            txt = CType(e.Row.Cells(Me.GRID_COL_VENDOR_SKU_DESCRIPTION_IDX).FindControl(GRID_CONTROL_VENDOR_SKU_DESCRIPTION_txt), TextBox)
                            txt.Text = CType(If(DBNull.Value.Equals(CAItem.VendorSkuDescription), String.Empty, CAItem.VendorSkuDescription), String)
                        End If
                        If (Not e.Row.Cells(Me.GRID_COL_SERVICE_CLASS_IDX).FindControl(GRID_CONTROL_SERVICE_CLASS_DDL) Is Nothing) Then
                            ddl = CType(e.Row.Cells(Me.GRID_COL_SERVICE_CLASS_IDX).FindControl(GRID_CONTROL_SERVICE_CLASS_DDL), DropDownList)
                            'Dim dv As DataView = LookupListNew.GetServiceClassList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                            'Me.Page.BindListControlToDataView(ddl, dv, , )
                           
                            ddl.Populate(CommonConfigManager.Current.ListManager.GetList("SVCCLASS", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })


                            If Not (DBNull.Value.Equals(CAItem.ServiceClassId)) Then
                                Me.Page.SetSelectedItem(ddl, CAItem.ServiceClassId.ToString())
                                If (Not e.Row.Cells(Me.GRID_COL_SERVICE_TYPE_IDX).FindControl(GRID_CONTROL_SERVICE_TYPE_DDL) Is Nothing) Then
                                    ddl = CType(e.Row.Cells(Me.GRID_COL_SERVICE_TYPE_IDX).FindControl(GRID_CONTROL_SERVICE_TYPE_DDL), DropDownList)
                                    'Me.Page.BindListControlToDataView(ddl, LookupListNew.GetNewServiceTypeByServiceClassLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId, CAItem.ServiceClassId))

                                    Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
                                    oListContext.ServiceClassId = CAItem.ServiceClassId
                                    oListContext.LanguageId = Thread.CurrentPrincipal.GetLanguageId()
                                    Dim oServiceTypeList As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="ServiceTypeByServiceClass", context:=oListContext)
                                    ddl.Populate(oServiceTypeList, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })

                                    If Not (DBNull.Value.Equals(CAItem.ServiceTypeId)) Then
                                        Me.Page.SetSelectedItem(ddl, CAItem.ServiceTypeId)
                                    End If

                                End If
                            End If
                        End If

                        If (Not e.Row.Cells(Me.GRID_COL_ADJUSTMENT_REASON_IDX).FindControl(GRID_CONTROL_ADJUSTMENT_REASON_txt) Is Nothing) Then
                            lbl = CType(e.Row.Cells(Me.GRID_COL_ADJUSTMENT_REASON_IDX).FindControl(GRID_CONTROL_ADJUSTMENT_REASON_txt), Label)
                            lbl.Text = LookupListNew.GetDescriptionFromId(LookupListNew.LK_ADJUSTMENT_REASON, CAItem.AdjustmentReasonId, True)
                        End If


                    Else
                        e.Row.Cells(Me.GRID_COL_AUTHORIZATION_ITEM_ID_IDX).Text = Page.GetGuidStringFromByteArray(CAItem.Id.ToByteArray)
                        e.Row.Cells(Me.GRID_COL_LINE_ITEM_NUMBER_IDX).Text = CAItem.LineItemNumber.ToString
                        If (Not e.Row.Cells(Me.GRID_COL_VENDOR_SKU_IDX).FindControl(GRID_LABEL_VENDOR_SKU) Is Nothing) Then
                            lbl = CType(e.Row.Cells(Me.GRID_COL_VENDOR_SKU_IDX).FindControl(GRID_LABEL_VENDOR_SKU), Label)
                            lbl.Text = CType(CAItem.VendorSku, String)
                        End If
                        If (Not e.Row.Cells(Me.GRID_COL_VENDOR_SKU_DESCRIPTION_IDX).FindControl(GRID_Label_VENDOR_SKU_DESCRIPTION) Is Nothing) Then
                            lbl = CType(e.Row.Cells(Me.GRID_COL_VENDOR_SKU_DESCRIPTION_IDX).FindControl(GRID_Label_VENDOR_SKU_DESCRIPTION), Label)
                            lbl.Text = CType(CAItem.VendorSkuDescription, String)
                        End If
                        If (Not e.Row.Cells(Me.GRID_COL_AMOUNT_IDX).FindControl(GRID_Label_AMOUNT) Is Nothing) Then
                            lbl = CType(e.Row.Cells(Me.GRID_COL_AMOUNT_IDX).FindControl(GRID_Label_AMOUNT), Label)
                            lbl.Text = Me.Page.GetAmountFormattedString(CAItem.Amount.Value)
                        End If
                        If (Not e.Row.Cells(Me.GRID_COL_SERVICE_CLASS_IDX).FindControl(GRID_LABEL_SERVICE_CLASS) Is Nothing) Then
                            lbl = CType(e.Row.Cells(Me.GRID_COL_SERVICE_CLASS_IDX).FindControl(GRID_LABEL_SERVICE_CLASS), Label)
                            lbl.Text = LookupListNew.GetDescriptionFromId(LookupListNew.LK_SERVICE_CLASS, CAItem.ServiceClassId, True)
                        End If
                        If (Not e.Row.Cells(Me.GRID_COL_SERVICE_TYPE_IDX).FindControl(GRID_Label_Service_type) Is Nothing) Then
                            lbl = CType(e.Row.Cells(Me.GRID_COL_SERVICE_TYPE_IDX).FindControl(GRID_Label_Service_type), Label)
                            lbl.Text = LookupListNew.GetDescriptionFromId(LookupListNew.LK_SERVICE_TYPE_NEW, CAItem.ServiceTypeId, True)
                        End If

                        If (Not e.Row.Cells(Me.GRID_COL_ADJUSTMENT_REASON_IDX).FindControl(GRID_label_ADJUSTMENT_REASON) Is Nothing) Then
                            lbl = CType(e.Row.Cells(Me.GRID_COL_ADJUSTMENT_REASON_IDX).FindControl(GRID_label_ADJUSTMENT_REASON), Label)
                            lbl.Text = LookupListNew.GetDescriptionFromId(LookupListNew.LK_ADJUSTMENT_REASON, CAItem.AdjustmentReasonId, True)
                        End If
                    End If
                End If


            End If



        Catch ex As Exception
            Me.Page.HandleErrors(ex, Me.Page.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub ResetIndexes()
        Me.State.isEditMode = False
        Me.State.isNewItem = False
        AuthorizationGrid.EditIndex = Me.Page.NO_ITEM_SELECTED_INDEX
        AuthorizationGrid.SelectedIndex = Me.Page.NO_ITEM_SELECTED_INDEX
    End Sub

    Private Function SaveChanges() As Boolean
        Dim bIsOk As Boolean = True
        Dim bIsDirty As Boolean
        Dim AuthItemId As Guid
        Dim ddl As DropDownList


        If AuthorizationGrid.EditIndex < 0 Then Return False ' Coverage Rate is not in edit mode

        Dim claimAuthitem As ClaimAuthItem


        AuthItemId = New Guid(AuthorizationGrid.Rows(AuthorizationGrid.EditIndex).Cells(Me.GRID_COL_AUTHORIZATION_ITEM_ID_IDX).Text.ToString())
        claimAuthitem = CType(Me.State.oCA.ClaimAuthorizationItemChildren.GetChild(AuthItemId), ClaimAuthItem)
        BindBOProperties(claimAuthitem)
        If Me.State.isNewItem Then
            claimAuthitem.ClaimAuthorizationId = Me.State.oCA.Id
            ddl = CType(AuthorizationGrid.Rows(AuthorizationGrid.EditIndex).Cells(Me.GRID_COL_SERVICE_CLASS_IDX).FindControl(GRID_CONTROL_SERVICE_CLASS_DDL), DropDownList)
            claimAuthitem.ServiceClassId = Me.Page.GetGuidFromString(ddl.SelectedValue)
            ddl = CType(AuthorizationGrid.Rows(AuthorizationGrid.EditIndex).Cells(Me.GRID_COL_SERVICE_TYPE_IDX).FindControl(GRID_CONTROL_SERVICE_TYPE_DDL), DropDownList)
            claimAuthitem.ServiceTypeId = Me.Page.GetGuidFromString(ddl.SelectedValue)
            claimAuthitem.LineItemNumber = CInt(AuthorizationGrid.Rows(AuthorizationGrid.EditIndex).Cells(Me.GRID_COL_LINE_ITEM_NUMBER_IDX).Text)
            claimAuthitem.VendorSku = CType(AuthorizationGrid.Rows(AuthorizationGrid.EditIndex).Cells(Me.GRID_COL_VENDOR_SKU_IDX).FindControl(GRID_CONTROL_VENDOR_SKU_txt), TextBox).Text
            claimAuthitem.VendorSkuDescription = CType(AuthorizationGrid.Rows(AuthorizationGrid.EditIndex).Cells(Me.GRID_COL_VENDOR_SKU_DESCRIPTION_IDX).FindControl(GRID_CONTROL_VENDOR_SKU_DESCRIPTION_txt), TextBox).Text

        End If
        Dim amt As Decimal = New Decimal(0D)
        If (Decimal.TryParse(CType(AuthorizationGrid.Rows(AuthorizationGrid.EditIndex).Cells(Me.GRID_COL_AMOUNT_IDX).FindControl(GRID_CONTROL_AMOUNT_txt), TextBox).Text, amt)) Then
            claimAuthitem.Amount = amt
        Else
            claimAuthitem.Amount = Nothing
        End If
        claimAuthitem.Save()


        'Me.State.oCA.Save()
        SetControls(ControlClicked.SaveAuthItem)

        Return bIsOk
    End Function

    Private Function CancelItem(ByVal RowIndex As Integer) As Boolean
        Dim bIsOk As Boolean = True

        If Me.State.isNewItem Then
            Dim AuthItemId As Guid

            AuthItemId = New Guid(AuthorizationGrid.Rows(RowIndex).Cells(Me.GRID_COL_AUTHORIZATION_ITEM_ID_IDX).Text.ToString())
            Dim claimAuthitem As ClaimAuthItem


            claimAuthitem = CType(Me.State.oCA.ClaimAuthorizationItemChildren.GetChild(AuthItemId), ClaimAuthItem)

            claimAuthitem.Delete()
        End If
        ResetIndexes()
        AuthorizationGrid.DataSource = Me.State.oCA.ClaimAuthorizationItemChildren.Where(Function(i) i.IsDeleted = False)
        AuthorizationGrid.DataBind()
        Me.State.isEditMode = False
        Me.State.isNewItem = False
        Return bIsOk

    End Function

    Private Sub BtnNew_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew_WRITE.Click
        Dim NewLineItemNumber As Long
        Try
            Me.State.isNewItem = True
            Me.State.isEditMode = True
            Dim CAItem As ClaimAuthItem = CType(Me.State.oCA.ClaimAuthorizationItemChildren.GetNewChild(Me.State.oCA.Id), ClaimAuthItem)

            AuthorizationGrid.EditIndex = AuthorizationGrid.Rows.Count
            AuthorizationGrid.SelectedIndex = AuthorizationGrid.EditIndex
            AuthorizationGrid.DataSource = Me.State.oCA.ClaimAuthorizationItemChildren.Where(Function(i) i.IsDeleted = False)
            AuthorizationGrid.DataBind()
            SetControls(ControlClicked.NewAuthItem, AuthorizationGrid.EditIndex)
        Catch ex As Exception
            Me.Page.HandleErrors(ex, Me.Page.MasterPage.MessageController)
        End Try
    End Sub

    Private Enum ControlClicked
        NewAuthItem
        EditAuthItem
        SaveAuthItem
        DeleteAuthItem
        Cancel
    End Enum

    Private Sub SetControls(ByVal Ctrl As ControlClicked, Optional ByVal RowIndex As Integer = -1)
        Select Case Ctrl
            Case ControlClicked.Cancel
                Me.BtnNew_WRITE.Enabled = True
                Me.SetGridControls(AuthorizationGrid, GRID_COL_COMMANDS, GRID_CONTROL_EDIT, True)
                Me.SetGridControls(AuthorizationGrid, GRID_COL_COMMANDS, GRID_CONTROL_DELETE, True)
                ResetIndexes()
                AuthorizationGrid.DataSource = Me.State.oCA.ClaimAuthorizationItemChildren.Where(Function(i) i.IsDeleted = False)
                AuthorizationGrid.DataBind()
            Case ControlClicked.NewAuthItem
                Me.BtnNew_WRITE.Enabled = False
                Me.SetGridControls(AuthorizationGrid, GRID_COL_COMMANDS, GRID_CONTROL_SAVE, True, RowIndex)
                Me.SetGridControls(AuthorizationGrid, GRID_COL_COMMANDS, GRID_CONTROL_CANCEL, True, RowIndex)
                Me.SetGridControls(AuthorizationGrid, GRID_COL_COMMANDS, GRID_CONTROL_EDIT, False)
                Me.SetGridControls(AuthorizationGrid, GRID_COL_COMMANDS, GRID_CONTROL_DELETE, False)

            Case ControlClicked.EditAuthItem
                Me.BtnNew_WRITE.Enabled = False
                Me.SetGridControls(AuthorizationGrid, GRID_COL_COMMANDS, GRID_CONTROL_SAVE, True, RowIndex)
                Me.SetGridControls(AuthorizationGrid, GRID_COL_COMMANDS, GRID_CONTROL_CANCEL, True, RowIndex)
                Me.SetGridControls(AuthorizationGrid, GRID_COL_COMMANDS, GRID_CONTROL_EDIT, False)
                Me.SetGridControls(AuthorizationGrid, GRID_COL_COMMANDS, GRID_CONTROL_DELETE, False)

                Me.SetGridControls(AuthorizationGrid, GRID_COL_SERVICE_CLASS_IDX, GRID_CONTROL_SERVICE_CLASS_DDL, False)
                Me.SetGridControls(AuthorizationGrid, GRID_COL_SERVICE_TYPE_IDX, GRID_CONTROL_SERVICE_TYPE_DDL, False)
                Me.SetGridControls(AuthorizationGrid, GRID_COL_VENDOR_SKU_IDX, GRID_CONTROL_VENDOR_SKU_txt, False)
                Me.SetGridControls(AuthorizationGrid, GRID_COL_VENDOR_SKU_DESCRIPTION_IDX, GRID_CONTROL_VENDOR_SKU_DESCRIPTION_txt, False)
                Me.SetGridControls(AuthorizationGrid, GRID_COL_AMOUNT_IDX, GRID_CONTROL_AMOUNT_txt, True)
                Me.SetGridControls(AuthorizationGrid, GRID_COL_ADJUSTMENT_REASON_IDX, GRID_CONTROL_ADJUSTMENT_REASON_txt, False)

            Case ControlClicked.SaveAuthItem
                Me.BtnNew_WRITE.Enabled = True
                Me.SetGridControls(AuthorizationGrid, GRID_COL_COMMANDS, GRID_CONTROL_EDIT, True)
                Me.SetGridControls(AuthorizationGrid, GRID_COL_COMMANDS, GRID_CONTROL_DELETE, True)
                ResetIndexes()
                AuthorizationGrid.DataSource = Me.State.oCA.ClaimAuthorizationItemChildren.Where(Function(i) i.IsDeleted = False)
                AuthorizationGrid.DataBind()
            Case ControlClicked.DeleteAuthItem

        End Select
    End Sub

    Private Sub SetGridControls(ByVal grid As GridView, ByVal cellIndex As Integer, ByVal ControlName As String, ByVal enable As Boolean, Optional ByVal RowIndex As Integer = -1)
        Dim edt As WebControl
        Dim i As Integer
        For i = 0 To (grid.Rows.Count - 1)
            edt = CType(grid.Rows(i).Cells(cellIndex).FindControl(ControlName), WebControl)
            If Not edt Is Nothing Then
                If RowIndex > -1 Then
                    If RowIndex = i Then
                        edt.Enabled = enable
                    Else
                        edt.Enabled = Not enable
                    End If

                Else
                    edt.Enabled = enable
                End If
            End If
        Next
    End Sub

    Protected Sub CheckIfComingFromDeleteConfirm()
        Dim confResponse As String = Me.HiddenDeletePromptResponse.Value
        If Not confResponse Is Nothing AndAlso confResponse = Me.Page.MSG_VALUE_YES Then
            If Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete Then
                If DeleteAuthItem(Me.State.DeletionIndex) = True Then
                    ResetIndexes()
                    AuthorizationGrid.DataSource = Me.State.oCA.ClaimAuthorizationItemChildren.Where(Function(i) i.IsDeleted = False)
                    AuthorizationGrid.DataBind()
                End If
                SetControls(ControlClicked.DeleteAuthItem)
                'Clean after consuming the action
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                Me.HiddenDeletePromptResponse.Value = ""
            End If
        ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.Page.MSG_VALUE_NO Then
            'Clean after consuming the action
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            Me.HiddenDeletePromptResponse.Value = ""
        End If
    End Sub

End Class