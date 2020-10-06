Imports Microsoft.VisualBasic
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Imports AjaxControlToolkit
Imports System.Collections.Generic

Public Class PriceListDetailForm
    Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "



    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub



#End Region

#Region "Constants"
    Public Const URL As String = "PriceListDetailForm.aspx"

    'Price List Detail Columns
    Public Const GRID_COL_SERVICE_CLASSID_IDX As Integer = 2
    Public Const GRID_COL_SERVICE_TYPEID_IDX As Integer = 3
    Public Const GRID_COL_SERVICE_LEVELID_IDX As Integer = 4
    Public Const GRID_COL_RISK_TYPEID_IDX As Integer = 5
    Public Const GRID_COL_EQUIPMENT_CLASSID_IDX As Integer = 6
    Public Const GRID_COL_MAKE_IDX As Integer = 7
    Public Const GRID_COL_MODEL_IDX As Integer = 8
    Public Const GRID_COL_PART_IDX As Integer = 9
    Public Const GRID_COL_CONDITIONID_IDX As Integer = 10
    Public Const GRID_COL_VERDOR_SKUID_IDX As Integer = 11
    Public Const GRID_COL_DESCRIPTIONID_IDX As Integer = 12

    Public Const GRID_COL_REQUESTEDBY_IDX As Integer = 13
    Public Const GRID_COL_REQUESTEDDATE_IDX As Integer = 14
    Public Const GRID_COL_STATUS_IDX As Integer = 15
    Public Const GRID_COL_STATUSDATE_IDX As Integer = 16
    Public Const GRID_COL_STATUSBY_IDX As Integer = 17

    Public Const GRID_COL_MANORI_IDX As Integer = 18
    Public Const GRID_COL_PRICEID_IDX As Integer = 19
    Public Const GRID_COL_QUANTITY_IDX As Integer = 20

    Public Const GRID_COL_LOW_PRICE_IDX As Integer = 21
    Public Const GRID_COL_HIGH_PRICE_IDX As Integer = 22

    Public Const GRID_COL_EFFECTIVE_DATEID_IDX As Integer = 23
    Public Const GRID_COL_EXPIRATION_DATEID_IDX As Integer = 24
    Public Const GRID_COL_EDITID_IDX As Integer = 1
    Public Const GRID_COL_DELETEID_IDX As Integer = 0
    Public Const GRID_COL_PRICE_LIST_DETAIL_IDX As Integer = 25
    Public Const GRID_COL_STATUS_XCD_IDX As Integer = 26
    Public Const GRID_COL_VIEW_HISTORY_IDX As Integer = 27



    'controls used in the form
    Public Const DDL_CONTROL_SERVICE_CLASS As String = "ddlServiceClass"
    Public Const DDL_CONTROL_SERVICE_TYPE As String = "ddlServiceType"
    Public Const DDL_CONTROL_EQUIPMENT_LIST As String = "ddlEquipmentlist"
    Public Const BTN_CONTROL_EQUIPMENT_LIST As String = "btnAddEquipment"
    Public Const DDL_CONTROL_PAGE_SIZE As String = "ddlPageSize"
    Public Const GRID_CONTROL_PRICE_LIST_DETAIL As String = "Grid"

    Public Const BTN_CONTROL_EDIT_DETAIL_LIST As String = "btn_edit"
    Public Const BTN_CONTROL_DELETE_DETAIL_LIST As String = "btn_delete"

    Public Const TEXTBOX_EFFECTIVE_DATE As String = "txtEffective"
    Public Const TEXTBOX_EXPIRATION_DATE As String = "txtExpirationDate"

    Public Const NO_VALUE_ENTERED As String = "N/A"

    Public Const TABLES As String = "Tables"
    Public Const ANY As String = "ANY"
    '   Public Const USER_DEFINED As String = "User Defined"
    Public Const MANAGE_INVENTORY_VISIBILITY_ITEM As String = "Price List"
    Public Const NO_PRICE As Decimal = 0
    Public Const PRICE_BAND_RANGE_FROM As Decimal = 0
    Public Const PRICE_BAND_RANGE_TO As Decimal = CDec(9999999.99)


#End Region
    Public isPendingApprovalRefresh As Boolean = True

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As PriceList
        Public HasDataChanged As Boolean
        Public Sub New(LastOp As DetailPageCommand, curEditingBo As PriceList, hasDataChanged As Boolean)
            LastOperation = LastOp
            EditingBo = curEditingBo
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class
#End Region

#Region "Enums"
    Public Enum InternalStates
        Regular
        ConfirmBackOnError
    End Enum
#End Region

#Region "Page State & Privates"

    ' Page MyState
    ' Page_call
    ' Page_PageReturn
    ' SaveGuiState
    ' RestoreGuiState

    Private IsReturningFromChild As Boolean = False
    Private msServiceClassColumnName As String = "DESCRIPTION"

    Class MyState
        Public PriceListId As Guid = Guid.Empty

        Public MyBO As PriceList
        Public ScreenSnapShotBO As PriceList

        Public MyChildBO As PriceListDetail
        ' Public ScreenSnapShotChildBO As PriceListDetail

        Public MyChildEquipmentBO As Equipment
        'Public ScreenSnapShotChildVendorBO As ServiceCenter

        Public IsACopy As Boolean

        Public Code As String = String.Empty
        Public Description As String = String.Empty
        Public EffectiveDate As DateType = Nothing
        Public ExpirationDate As DateType = Nothing

        'Public IsPriceListEditing As Boolean = False
        Public OverlapExists As Boolean = False
        Public ChildOverlapExists As Boolean = False
        Public IsEditMode As Boolean = False

        Public IsGridInEditMode As Boolean = False
        Public SelectedGridValueToEdit As Guid
        'Public SelectedChildId As Guid = Guid.Empty
        Public PriceListDetailSelectedChildId As Guid = Guid.Empty

        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public ChildActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_

        Public LastErrMsg As String
        Public HasDataChanged As Boolean = False

        Public LastState As InternalStates = InternalStates.Regular

        Public SortExpression As String = PriceListDetail.PriceListDetailSearchDV.COL_EXPIRATION
        Public DetailSearchDV As PriceList.PriceListDetailSelectionView = Nothing
        Public PageIndex As Integer = 0
        Public PageSize As Integer = 30

        Public IsGridVisible As Boolean
        Public SelectedPageSize As Integer

        Public IsNew As Boolean = False
        Public SelectedSvcCtrCount As Integer = 0

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


    Private ReadOnly Property IsNewPriceList() As Boolean
        Get
            Return State.MyBO.IsNew
        End Get

    End Property

    Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
        Try
            If CallingParameters IsNot Nothing Then
                State.MyBO = New PriceList(CType(CallingParameters, Guid))

                State.IsEditMode = True
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
        Try
            MenuEnabled = True
            IsReturningFromChild = True
            Dim retObj As PriceListDetailForm.ReturnType = CType(ReturnPar, PriceListDetailForm.ReturnType)
            State.HasDataChanged = retObj.HasDataChanged
            Select Case retObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Back
                    If retObj IsNot Nothing Then
                        If Not retObj.EditingBo.IsNew Then
                            State.PriceListId = retObj.EditingBo.Id
                        End If
                    End If
                Case ElitaPlusPage.DetailPageCommand.Delete
                    DisplayMessage(Message.DELETE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
            End Select
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Page Events"

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try

            UpdateBreadcrum()
            MasterPage.MessageController.Clear()

            If Not IsPostBack Then
                MenuEnabled = False
                AddControlMsg(btnDelete, Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, True)

                If State.MyBO Is Nothing Then
                    State.MyBO = New PriceList
                    State.IsNew = True
                End If

                TranslateGridHeader(Grid)
                TranslateGridHeader(gvPendingApprovals)
                TranslateGridHeader(gvHistory)
                PopulateDropdowns()
                PopulateFormFromBOs()
                EnableDisableFields(True)

                moSelectedTitle.Text = TranslationBase.TranslateLabelOrMessage("SELECTED VENDORS")
                cboPageSize.SelectedValue = State.PageSize.ToString()
                cboPageSizePendingApproval.SelectedValue = State.PageSize.ToString()
            End If

            CheckIfComingFromSaveConfirm()
            BindBoPropertiesToLabels()

            If Not IsPostBack Then
                AddLabelDecorations(State.MyBO)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub

#End Region

    Private Sub UpdateBreadcrum()
        'Breadcrumb and titles
        MasterPage.UsePageTabTitleInBreadCrum = False
        MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("SERVICE_NETWORK")
        MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("PRICE_LIST_DETAIL")
        MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage("TABLES") & ElitaBase.Sperator & MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("EDIT_PRICE_LIST")
    End Sub

#Region "Button Clicks"

    ''' <summary>
    ''' main back button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
        Try
            PopulateBOsFormFrom()
            If State.MyBO.IsDirty Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
            DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            State.LastErrMsg = MasterPage.MessageController.Text
        End Try
    End Sub

    ''' <summary>
    ''' Main save button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' 
    Private Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click
        Try
            ' if the bo is not already in the past
            If Not State.IsNew Then
                If DateHelper.GetDateValue(State.MyBO.Effective.Value.ToString) > DateHelper.GetDateValue(CStr(DateTime.Now.Date)) Then
                    ' if new effective date is in the past throw exception
                    If DateHelper.GetDateValue(txtEffective.Text.ToString()) < DateHelper.GetDateValue(CStr(DateTime.Now.Date)) Then
                        Throw New GUIException(Message.MSG_GUI_INVALID_EFFECTIVE_DATE_SMALLER_THAN_SYSDATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_EFFECTIVE_DATE_SMALLER_THAN_SYSDATE)
                    End If
                End If
            End If

            PopulateBOsFormFrom()
            State.MyBO.Validate()

            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If

            If txtEffective.Text = String.Empty Then
                Throw New GUIException(Message.PRICELIST_INVALID_EFFECIVE_DATE, Assurant.ElitaPlus.Common.ErrorCodes.PRICELIST_INVALID_EFFECIVE_DATE)
            End If

            If CheckOverlap() Then
                If CheckExistingFutureListOverlap() Then
                    Throw New GUIException(Message.MSG_GUI_OVERLAPPING_PRICE_LIST, Assurant.ElitaPlus.Common.ErrorCodes.PRICELIST_INVALID_EFFECIVE_DATE)
                End If
                DisplayMessage(Message.MSG_GUI_OVERLAPPING_PRICE_LIST, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = DetailPageCommand.Accept
                State.OverlapExists = True
                Exit Sub
            End If

            If (State.MyBO.IsDirty OrElse State.MyBO.IsFamilyDirty) Then
                State.HasDataChanged = True
                State.MyBO.Save()
                State.HasDataChanged = False
                PopulateFormFromBOs()
                EnableDisableFields(True)
                ClearGridViewHeadersAndLabelsErrSign()
                MasterPage.MessageController.AddSuccess(ElitaPlus.ElitaPlusWebApp.Message.SAVE_RECORD_CONFIRMATION)
            Else
                MasterPage.MessageController.AddError(ElitaPlus.ElitaPlusWebApp.Message.MSG_RECORD_NOT_SAVED)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    ''' <summary>
    ''' Main undo button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnUndo_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo.Click
        Try
            If Not State.MyBO.IsNew Then
                'Reload from the DB
                State.MyBO = New PriceList(State.MyBO.Id)
            ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                'It was a new with copy
                State.MyBO.Clone(State.ScreenSnapShotBO)
            Else
                State.MyBO = New PriceList
            End If
            PopulateFormFromBOs()
            EnableDisableFields(True)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    ''' <summary>
    ''' Main delete button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnDelete_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete.Click
        Try
            If State.MyBO.IsPriceListAssignedtoServiceCenter Then
                Throw New GUIException(Message.MSG_GUI_PRICE_LIST_ASSIGNED_TO_SERVICE_CENTER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_LIST_CODE_ERR)
            Else
                If State.MyBO.Effective.Value > DateTime.Now Then
                    State.MyBO.BeginEdit()
                    State.MyBO.Delete()
                    State.MyBO.Save()
                    State.HasDataChanged = True
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, State.MyBO, State.HasDataChanged))
                Else
                    State.MyBO.Accept(New ExpirationVisitor)
                    State.MyBO.Save()
                    State.HasDataChanged = True
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Expire, State.MyBO, State.HasDataChanged))
                End If
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            'undo the delete
            State.MyBO.RejectChanges()
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    ''' <summary>
    ''' Main new/add button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnNew_Click(sender As System.Object, e As System.EventArgs) Handles btnAdd.Click
        Try
            PopulateBOsFormFrom()
            If (State.MyBO.IsDirty) And Not (State.MyBO.Code = String.Empty Or State.MyBO.Code = Nothing) Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                CreateNew()
                ClearGridHeadersAndLabelsErrSign()
                'Me.callPage(PriceListDetailForm.URL)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    ''' <summary>
    ''' main copy button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCopy_Click(sender As System.Object, e As System.EventArgs) Handles btnClone.Click
        Try
            If State.MyBO.IsDirty Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            Else
                CreateNewWithCopy()
            End If
            PopulateBOsFormFrom()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Additionl Buttons and helper functions"

    ''' <summary>
    ''' Button to add equipments in bulk
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnAddEquipment_Click(sender As System.Object, e As System.EventArgs) Handles btnAddEquipment.Click
        Try
            If (ddlEquipmentlist.SelectedIndex <> BLANK_ITEM_SELECTED) Then
                If BulkAddEquipment() Then
                    State.HasDataChanged = False
                    PopulateGrid()
                    ClearGridViewHeadersAndLabelsErrSign()

                    ddlEquipmentlist.SelectedIndex = BLANK_ITEM_SELECTED
                    ddlServiceClass.SelectedIndex = BLANK_ITEM_SELECTED
                    ddlServiceType.SelectedIndex = BLANK_ITEM_SELECTED
                    txtNewEquipVendorSKU.Text = String.Empty
                    txtNewEquipSKUDescription.Text = String.Empty

                    MasterPage.MessageController.AddSuccess(Message.RECORD_ADDED_OK)
                Else
                    MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
                End If
            Else
                MasterPage.MessageController.AddError(Message.INVALID_SELECT_EQUIPMENT_LIST)
            End If
        Catch BO_ex As BOValidationException
            HandleErrors(BO_ex, MasterPage.MessageController)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    ''' <summary>
    ''' Helper function for btnAddEquipment_Click
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 
    Private Function BulkAddEquipment() As Boolean
        Try

            If ddlEquipmentlist.SelectedIndex <> BLANK_ITEM_SELECTED Then
                'BeginPriceListDetailChildEdit()
                State.MyChildBO = State.MyBO.GetNewPriceListDetailChild
                'Me.State.MyChildBO.BeginEdit()

                ' Equipment Id
                State.MyChildBO.EquipmentId = New Guid(ddlEquipmentlist.SelectedValue)

                ' Service Type
                If ddlServiceType IsNot Nothing Then
                    PopulateBOProperty(State.MyChildBO, "ServiceTypeId", New Guid(ddlServiceType.SelectedValue))
                    State.MyChildBO.ServiceTypeCode = ddlServiceType.SelectedItem.Text.ToString()
                End If

                ' Service Class
                If ddlServiceClass IsNot Nothing Then
                    PopulateBOProperty(State.MyChildBO, "ServiceClassId", New Guid(ddlServiceClass.SelectedValue))
                    State.MyChildBO.ServiceClassCode = ddlServiceClass.SelectedItem.Text.ToString()
                End If

                'Get make model from the equipment
                If Not State.MyChildBO.EquipmentId.Equals(Guid.Empty) Then
                    Dim ds As New DataSet
                    ds = (New PriceListDetail).GetMakeModelByEquipmentId(State.MyChildBO.EquipmentId, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
                    If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                        PopulateBOProperty(State.MyChildBO, "Make", ds.Tables(0).Rows(0)("Make").ToString())
                        PopulateBOProperty(State.MyChildBO, "Model", ds.Tables(0).Rows(0)("Model").ToString())
                        PopulateBOProperty(State.MyChildBO, "MakeId", GuidControl.ByteArrayToGuid(ds.Tables(0).Rows(0)("manufacturer_id")))
                    End If
                End If

                ' Currency
                Dim company As New ElitaPlus.BusinessObjectsNew.Company(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
                Dim currencyobj As New ElitaPlus.BusinessObjectsNew.Country(company.CountryId)
                State.MyChildBO.CurrencyId = currencyobj.PrimaryCurrencyId

                ' PriceList Detail Type
                State.MyChildBO.PriceListDetailTypeId = LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListCache.LK_PRICE_LIST_DETAIL_TYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId), LookupListCache.LK_USER_DEFINED_CODE)

                State.MyChildBO.PriceListId = State.MyBO.Id

                'Vendor SKU
                State.MyChildBO.VendorSku = txtNewEquipVendorSKU.Text

                'Vendor Description
                State.MyChildBO.VendorSkuDescription = txtNewEquipSKUDescription.Text

                Me.State.MyChildBO.Price = CType(NO_PRICE, Decimal)
                Me.State.MyChildBO.PriceBandRangeFrom = PRICE_BAND_RANGE_FROM
                Me.State.MyChildBO.PriceBandRangeTo = PRICE_BAND_RANGE_TO

                'if parent effective date is in the past, then use current date or else use parent effective date                
                If (DateHelper.GetDateValue(txtEffective.Text.ToString()) > PriceListDetail.GetCurrentDateTime()) Then
                    State.MyChildBO.Effective = DateHelper.GetDateValue(txtEffective.Text.ToString())
                Else
                    State.MyChildBO.Effective = PriceListDetail.GetCurrentDateTime()
                End If

                State.MyChildBO.Expiration = DateHelper.GetDateValue(txtExpirationDate.Text.ToString())
                State.MyChildBO.Validate()

                If State.MyChildBO.IsDirty Then
                    ' check if already exists
                    ' same equipment id, condition id, service_class id, service type id and sku
                    If State.MyChildBO.OverlapExists(False) Then
                        If State.MyChildBO.ExpireOverLappingList() Then
                            'Me.State.MyChildBO.BeginEdit()
                            State.MyChildBO.Effective = PriceListDetail.GetCurrentDateTime()
                            'Me.State.MyChildBO.EndEdit()
                            State.ChildOverlapExists = False
                        Else
                            State.ChildOverlapExists = True
                        End If
                    End If

                    Try
                        If Not (State.ChildOverlapExists) Then
                            'Me.State.MyChildBO.BeginEdit()
                            State.MyChildBO.Save()
                        End If
                    Catch ex As Exception
                        'Me.State.MyChildBO.RejectChanges()
                        Throw ex
                    End Try

                End If
            End If

            Return True
        Catch BO_ex As BOValidationException
            'Me.State.MyChildBO.cancelEdit()
            Throw New BOValidationException(BO_ex.ValidationErrorList(), BO_ex.BusinessObjectName, BO_ex.UniqueId)
        Catch ex As Exception
            'Me.State.MyChildBO.cancelEdit()
            Throw ex
        End Try
    End Function

    Private Function GetEquipmentList() As ArrayList
        Try
            Dim oEquipmentList As ArrayList = New ArrayList()
            oEquipmentList = EquipmentListDetail.GetEquipmentsInList(New Guid(ddlEquipmentlist.SelectedValue))
            If oEquipmentList.Count > 0 Then
                Return oEquipmentList
            Else
                DisplayMessage(Message.MSG_NO_RECORDS_FOUND, "", MSG_BTN_OK, MSG_TYPE_INFO)
                Exit Function
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Function

    ''' <summary>
    ''' The new button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' 
    Private Sub addBtn_Click(sender As Object, e As System.EventArgs) Handles addBtnNewListItem.Click
        Try
            State.ChildActionInProgress = DetailPageCommand.New_
            PopulateModalControls()
            ShowHideQuantity()
            'Me.State.MyChildBO = Nothing
            State.PriceListDetailSelectedChildId = Guid.Empty
            BeginPriceListDetailChildEdit()
            'Me.SetSelectedItemByText(ddldetailtype, USER_DEFINED)
            SetSelectedItem(ddldetailtype, LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListCache.LK_PRICE_LIST_DETAIL_TYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId), LookupListCache.LK_USER_DEFINED_CODE))
            If State.MyChildBO.CurrencyId.Equals(Guid.Empty) Then
                Dim company As New ElitaPlus.BusinessObjectsNew.Company(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
                Dim currencyobj As New ElitaPlus.BusinessObjectsNew.Country(company.CountryId)
                SetSelectedItem(ddlcurrency, currencyobj.PrimaryCurrencyId)
            Else
                PopulateControlFromBOProperty(ddlcurrency, State.MyChildBO.CurrencyId)
            End If
            ControlMgr.SetEnableControl(Me, ddldetailtype, True)
            ControlMgr.SetEnableControl(Me, ddlcurrency, True)
            ControlMgr.SetEnableControl(Me, txtNewItemPrice, True)
            ControlMgr.SetEnableControl(Me, lblNewItemPrice, True)

            'ddlRiskType.Items.Insert(0, New ListItem(TranslationBase.TranslateLabelOrMessage(ANY), Guid.Empty.ToString()))
            mdlPopup.Show()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Sub BeginPriceListDetailChildEdit()
        With State
            If Not .PriceListDetailSelectedChildId.Equals(Guid.Empty) Then
                .MyChildBO = .MyBO.GetPriceListDetailChild(.PriceListDetailSelectedChildId)
            Else
                .MyChildBO = .MyBO.GetNewPriceListDetailChild
            End If
            .MyChildBO.BeginEdit()
        End With

        BindBoPropertiesToModalPopupLabels()
        AddLabelDecorations(State.MyChildBO)
        lblcurrency.Text = lblcurrency.Text.Substring(0, lblcurrency.Text.Length - 1)
    End Sub

    Private Sub ShowHideQuantity()
        If (ddlManageInventory.SelectedItem.Text.ToUpper() = MANAGE_INVENTORY_VISIBILITY_ITEM.ToUpper()) Then
            lblNewItemQuantity.Visible = True
            txtNewItemQuantity.Visible = True
        Else
            lblNewItemQuantity.Visible = False
            txtNewItemQuantity.Visible = False
        End If
        If (ddlManageInventory.SelectedItem.Text.ToUpper() = MANAGE_INVENTORY_VISIBILITY_ITEM.ToUpper()) Then
            Grid.Columns(GRID_COL_QUANTITY_IDX).Visible = True
        Else
            Grid.Columns(GRID_COL_QUANTITY_IDX).Visible = False
        End If
    End Sub

    ''' <summary>
    ''' Modal pupup cancel button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' 
    Private Sub btnNewItemCancel_Click(sender As Object, e As System.EventArgs) Handles btnNewItemCancel.Click
        Try

            State.MyChildBO.cancelEdit()
            If State.MyChildBO.IsSaveNew Then
                State.MyChildBO.Delete()
                State.MyChildBO.Save()
            End If
            mdlPopup.Hide()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    ''' <summary>
    ''' This is the save button inside the modal popup for new item
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub btnNewItemSave_Click(sender As Object, e As System.EventArgs) Handles btnNewItemSave.Click
        Try

            'get the service type list id
            If (ddlNewItemServiceType.SelectedIndex <> NO_ITEM_SELECTED_INDEX) Then
                PopulateBOProperty(State.MyChildBO, "ServiceTypeId", New Guid(ddlNewItemServiceType.SelectedValue))
                PopulateBOProperty(State.MyChildBO, "ServiceTypeCode", ddlNewItemServiceType.SelectedItem.Text.ToString())
            End If

            'get the service class list id
            If (ddlNewItemServiceClass.SelectedIndex <> NO_ITEM_SELECTED_INDEX) Then
                PopulateBOProperty(State.MyChildBO, "ServiceClassId", New Guid(ddlNewItemServiceClass.SelectedValue))
                PopulateBOProperty(State.MyChildBO, "ServiceClassCode", ddlNewItemServiceClass.SelectedItem.Text.ToString())
            End If

            'get the service level list id
            If (ddlNewItemServiceLevel.SelectedIndex <> NO_ITEM_SELECTED_INDEX) Then
                PopulateBOProperty(State.MyChildBO, "ServiceLevelId", New Guid(ddlNewItemServiceLevel.SelectedValue))
                PopulateBOProperty(State.MyChildBO, "ServiceLevelCode", ddlNewItemServiceLevel.SelectedItem.Text.ToString())
            End If

            'get the service class list id
            If (ddlNewItemCondition.SelectedIndex <> NO_ITEM_SELECTED_INDEX AndAlso ddlNewItemMake.SelectedIndex <> BLANK_ITEM_SELECTED) Then
                PopulateBOProperty(State.MyChildBO, "ConditionId", New Guid(ddlNewItemCondition.SelectedValue))
                PopulateBOProperty(State.MyChildBO, "ConditionTypeCode", ddlNewItemCondition.SelectedItem.Text.ToString())
            Else
                PopulateBOProperty(State.MyChildBO, "ConditionId", Guid.Empty)
                PopulateBOProperty(State.MyChildBO, "ConditionTypeCode", String.Empty)
            End If

            'get the service class list id
            If (ddlNewItemReplacementTaxType.SelectedIndex <> NO_ITEM_SELECTED_INDEX) Then
                PopulateBOProperty(State.MyChildBO, "ReplacementTaxType", New Guid(ddlNewItemReplacementTaxType.SelectedValue))
            End If

            If (ddlNewItemPart.SelectedIndex <> NO_ITEM_SELECTED_INDEX) Then
                PopulateBOProperty(State.MyChildBO, "PartId", New Guid(ddlNewItemPart.SelectedValue))
                PopulateBOProperty(State.MyChildBO, "PartDescription", ddlNewItemPart.SelectedItem.Text.ToString())
            Else
                PopulateBOProperty(State.MyChildBO, "PartId", Guid.Empty)
                PopulateBOProperty(State.MyChildBO, "PartDescription", String.Empty)
            End If

            If (ddlNewManOri.SelectedIndex <> NO_ITEM_SELECTED_INDEX AndAlso ddlNewItemPart.SelectedIndex <> BLANK_ITEM_SELECTED) Then
                PopulateBOProperty(State.MyChildBO, "ManufacturerOriginCode", ddlNewManOri.SelectedValue)
                PopulateBOProperty(State.MyChildBO, "ManufacturerOriginDescription", ddlNewManOri.SelectedItem.Text.ToString())
            End If


            State.MyChildBO.VendorSku = txtNewItemVendorSKU.Text
            State.MyChildBO.VendorSkuDescription = txtNewItemSKUDescription.Text

            PopulateBOProperty(State.MyChildBO, "PriceBandRangeFrom", txtNewItemLowPrice)
            PopulateBOProperty(State.MyChildBO, "PriceBandRangeTo", txtNewItemHighPrice)

            Dim Currencyobj As String = GetCurrencyId(0)
            State.MyChildBO.PriceLowWithSymbol = Currencyobj & "" & txtNewItemLowPrice.Text
            State.MyChildBO.PriceHighWithSymbol = Currencyobj & "" & txtNewItemHighPrice.Text

            If txtNewItemEffectiveDate.Text <> "" Then
                If DateHelper.GetDateValue(State.MyChildBO.Effective.ToString) <> DateHelper.GetDateValue(txtNewItemEffectiveDate.Text.ToString) Then
                    If (DateHelper.GetDateValue(txtNewItemEffectiveDate.Text.ToString()) < DateTime.Today()) Then
                        Throw New GUIException(Message.MSG_GUI_INVALID_EFFECTIVE_DATE_SMALLER_THAN_SYSDATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_EFFECTIVE_DATE_SMALLER_THAN_SYSDATE)
                    End If
                End If
                State.MyChildBO.Effective = DateHelper.GetDateValue(txtNewItemEffectiveDate.Text.Trim())
            Else
                ' The effecitve and exp dates will be set to parents dates
                'if parent effective date is in the past, then use current date or else use parent effective date
                'or else it will fail but bo
                If (DateHelper.GetDateValue(txtEffective.Text.ToString()) > PriceListDetail.GetCurrentDateTime()) Then
                    State.MyChildBO.Effective = DateHelper.GetDateValue(txtEffective.Text.ToString())
                Else
                    State.MyChildBO.Effective = PriceListDetail.GetCurrentDateTime()
                End If
            End If

            If txtNewItemExpirationDate.Text <> "" Then
                State.MyChildBO.Expiration = DateHelper.GetDateValue(txtNewItemExpirationDate.Text.Trim())
            Else
                State.MyChildBO.Expiration = DateHelper.GetDateValue(txtExpirationDate.Text.ToString())
            End If

            If IsNumeric(txtNewItemPrice.Text) Then
                State.MyChildBO.Price = CType(txtNewItemPrice.Text.Trim(), Decimal)
            End If

            State.MyChildBO.PriceWithSymbol = Currencyobj & "" & txtNewItemPrice.Text

            State.MyChildBO.PriceListId = State.MyBO.Id

            'Equipment
            'get the equipment id
            State.MyChildEquipmentBO = New Equipment
            If (ddlNewItemMake.SelectedIndex <> NO_ITEM_SELECTED_INDEX) Then
                PopulateBOProperty(State.MyChildEquipmentBO, "ManufacturerId", New Guid(ddlNewItemMake.SelectedValue))
                PopulateBOProperty(State.MyChildBO, "MakeId", New Guid(ddlNewItemMake.SelectedValue))
                PopulateBOProperty(State.MyChildBO, "Make", ddlNewItemMake.SelectedItem.Text.ToString())
            Else
                PopulateBOProperty(State.MyChildEquipmentBO, "ManufacturerId", Guid.Empty)
                PopulateBOProperty(State.MyChildBO, "MakeId", Guid.Empty)
                PopulateBOProperty(State.MyChildBO, "Make", String.Empty)
            End If
            If (ddlNewItemModel.SelectedIndex <> NO_ITEM_SELECTED_INDEX) Then
                PopulateBOProperty(State.MyChildBO, "EquipmentId", New Guid(ddlNewItemModel.SelectedValue))
                PopulateBOProperty(State.MyChildEquipmentBO, "Model", ddlNewItemModel.SelectedItem.Text.ToString())
                PopulateBOProperty(State.MyChildBO, "Model", ddlNewItemModel.SelectedItem.Text.ToString())
            Else
                PopulateBOProperty(State.MyChildBO, "EquipmentId", Guid.Empty)
                PopulateBOProperty(State.MyChildEquipmentBO, "Model", String.Empty)
                PopulateBOProperty(State.MyChildBO, "Model", String.Empty)
            End If


            If (ddlNewItemPart.SelectedIndex <> NO_ITEM_SELECTED_INDEX AndAlso ddlNewItemMake.SelectedIndex <> NO_ITEM_SELECTED_INDEX) Then
                PopulateBOProperty(State.MyChildBO, "Parent_MakeId", New Guid(ddlNewItemMake.SelectedValue))
                PopulateBOProperty(State.MyChildBO, "Parent_Make", ddlNewItemMake.SelectedItem.Text.ToString())

                Dim eqId As Guid = If(String.IsNullOrEmpty(ddlNewItemModel.SelectedValue), Guid.Empty, New Guid(ddlNewItemModel.SelectedValue))
                PopulateBOProperty(State.MyChildBO, "Parent_EquipmentId", eqId)

                Dim mdl = If(ddlNewItemModel.SelectedItem Is Nothing, String.Empty, ddlNewItemModel.SelectedItem.Text.ToString())
                State.MyChildBO.Parent_Model = mdl

                Dim condId = If(String.IsNullOrEmpty(ddlNewItemCondition.SelectedValue), Guid.Empty, New Guid(ddlNewItemCondition.SelectedValue))
                Me.PopulateBOProperty(State.MyChildBO, "Parent_ConditionId", condId)

                Dim condText = If(ddlNewItemCondition.SelectedItem Is Nothing, String.Empty, ddlNewItemCondition.SelectedItem.Text.ToString())
                State.MyChildBO.Parent_ConditionTypeCode = condText
            End If
            'populate Risk Type id and Code
            PopulateBOProperty(State.MyChildBO, "RiskTypeId", New Guid(ddlRiskType.SelectedValue))
            PopulateBOProperty(State.MyChildBO, "RiskTypeCode", ddlRiskType.SelectedItem.Text.ToString())

            'Populate Equipment ClassID and Code
            PopulateBOProperty(State.MyChildBO, "EquipmentClassId", New Guid(ddlEquipmentClass.SelectedValue))
            PopulateBOProperty(State.MyChildBO, "EquipmentCode", ddlEquipmentClass.SelectedItem.Text.ToString())

            ' Populate Item Quantity
            PopulateBOProperty(State.MyChildBO, "VendorQuantity", txtNewItemQuantity.Text.ToString())

            'populate Currency
            PopulateBOProperty(State.MyChildBO, "CurrencyId", New Guid(ddlcurrency.SelectedValue))

            'PriceListDetailType
            PopulateBOProperty(State.MyChildBO, "PriceListDetailTypeId", New Guid(ddldetailtype.SelectedValue))

            'Populate the vendor quantity
            If (txtNewItemQuantity.Text <> "") Then
                State.MyChildBO.VendorQuantity = CType(txtNewItemQuantity.Text.Trim(), LongType)
            End If

            BindChildBoPropertiesToLabels()

            If State.MyChildBO.IsDirty Then
                ' check if already exists
                ' same equipment id, condition id, service_class id, service type id and sku
                If State.MyChildBO.OverlapExists(True) Then
                    If State.MyChildBO.ExpireOverLappingList() Then
                        State.ChildOverlapExists = False
                    Else
                        State.ChildOverlapExists = True
                    End If
                End If

                If IsNumeric(txtNewItemPrice.Text) Then
                    State.MyChildBO.Price = CType(txtNewItemPrice.Text.Trim(), Decimal)
                Else
                    Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.GUI_ONLY_DIGITS_ALLOWED_FOR_PRICE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_ONLY_DIGITS_ALLOWED_FOR_PRICE)
                End If

                State.MyChildBO.Validate()

                Try
                    If Not (State.ChildOverlapExists) Then
                        If State.MyChildBO.IsDirty Then
                            State.MyChildBO.Save()
                            State.MyChildBO.EndEdit()
                        End If

                        State.HasDataChanged = False
                    End If

                Catch ex As Exception
                    State.MyChildBO.RejectChanges()
                    State.MyChildEquipmentBO.RejectChanges()
                    Throw ex
                End Try

                PopulateGrid()
                EnableDisableFields(True)
                ClearGridViewHeadersAndLabelsErrSign()

                'Me.MasterPage.MessageController.AddSuccess(ElitaPlus.ElitaPlusWebApp.Message.SAVE_RECORD_CONFIRMATION)
                'Else
                'Me.MasterPage.MessageController.AddError(ElitaPlus.ElitaPlusWebApp.Message.MSG_RECORD_NOT_SAVED)
            End If
            'to reset the boolen value
            State.IsGridInEditMode = False
            'Me.State.MyChildBO.EndEdit()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
            mdlPopup.Show()
        End Try
    End Sub


    Protected Sub BindChildBoPropertiesToLabels()
        BindBOPropertyToLabel(State.MyChildBO, "ServiceClassId", lblNewItemServiceClass)
        BindBOPropertyToLabel(State.MyChildBO, "ConditionId", lblNewItemCondition)
        BindBOPropertyToLabel(State.MyChildBO, "MakeId", lblNewItemMake)
        BindBOPropertyToLabel(State.MyChildBO, "EquipmentId", lblNewItemModel)
        BindBOPropertyToLabel(State.MyChildBO, "VendorSku", lblNewItemVendorSKU)
        BindBOPropertyToLabel(State.MyChildBO, "VendorSkuDescription", lblNewItemSKUDesciption)
        BindBOPropertyToLabel(State.MyChildBO, "Price", lblNewItemPrice)
        BindBOPropertyToLabel(State.MyChildBO, "Effective", lblNewItemEffectiveDate)
        BindBOPropertyToLabel(State.MyChildBO, "Expiration", lblNewItemExpirationDate)
        BindBOPropertyToLabel(State.MyChildBO, "VendorQuantity", lblNewItemQuantity)
        BindBOPropertyToLabel(State.MyChildBO, "PriceBandRangeFrom", lblNewItemLowPrice)
        BindBOPropertyToLabel(State.MyChildBO, "PriceBandRangeTo", lblNewItemHighPrice)
        BindBOPropertyToLabel(State.MyChildBO, "ReplacementTaxType", lblNewItemSelectTaxType)
        BindBOPropertyToLabel(State.MyChildBO, "CurrencyId", lblcurrency)
        BindBOPropertyToLabel(State.MyChildBO, "PartId", lblNewItemPart)
        BindBOPropertyToLabel(State.MyChildBO, "ManufacturerOriginCode", lblNewManOri)

    End Sub

#End Region

#Region "Controlling Logic"

    Private Function CheckOverlap() As Boolean
        Return State.MyBO.Accept(New OverlapValidationVisitor)
    End Function

    Private Function CheckExistingFutureListOverlap() As Boolean
        Return State.MyBO.Accept(New FutureOverlapValidationVisitor)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub BindBoPropertiesToLabels()
        BindBOPropertyToLabel(State.MyBO, "Code", lblCode)
        BindBOPropertyToLabel(State.MyBO, "Description", lblDescription)
        BindBOPropertyToLabel(State.MyBO, "CountryId", lblCountry)
        BindBOPropertyToLabel(State.MyBO, "DefaultCurrencyId", lblDefaultCurrency)
        BindBOPropertyToLabel(State.MyBO, "ManageInventoryId", lblManageInventory)
        BindBOPropertyToLabel(State.MyBO, "Effective", lblEffectiveDate)
        BindBOPropertyToLabel(State.MyBO, "Expiration", lblExpirationDate)
        '''''appending : as the above 3 are part of the childbo but childbo is not loaded on the page load.
        If Not lblEquipmentList.Text.EndsWith(":") Then
            lblEquipmentList.Text = lblEquipmentList.Text & ":"
        End If
        If Not lblServiceClass.Text.EndsWith(":") Then
            lblServiceClass.Text = lblServiceClass.Text & ":"
        End If
        If Not lblServiceType.Text.EndsWith(":") Then
            lblServiceType.Text = lblServiceType.Text & ":"
        End If
    End Sub

    Protected Sub BindBoPropertiesToModalPopupLabels()
        'Modal popup labels
        BindBOPropertyToLabel(State.MyChildBO, "ServiceClassId", lblNewItemServiceClass)
        BindBOPropertyToLabel(State.MyChildBO, "Price", lblNewItemPrice)
        BindBOPropertyToLabel(State.MyChildBO, "ServiceTypeId", lblNewItemServiceType)
        BindBOPropertyToLabel(State.MyChildBO, "VendorQuantity", lblNewItemQuantity)
        BindBOPropertyToLabel(State.MyChildBO, "MakeId", lblNewItemMake)
        BindBOPropertyToLabel(State.MyChildBO, "Effective", lblNewItemEffectiveDate)
        BindBOPropertyToLabel(State.MyChildBO, "EquipmentId", lblNewItemModel)
        BindBOPropertyToLabel(State.MyChildBO, "Expiration", lblNewItemExpirationDate)
        BindBOPropertyToLabel(State.MyChildBO, "RiskTypeId", lblRiskType)
        BindBOPropertyToLabel(State.MyChildBO, "EquipmentClassId", lblEquipmentClass)
        BindBOPropertyToLabel(State.MyChildBO, "ConditionId", lblNewItemCondition)
        BindBOPropertyToLabel(State.MyChildBO, "ServiceLevelId", lblNewItemServiceLevel)
        BindBOPropertyToLabel(State.MyChildBO, "VendorSku", lblNewItemVendorSKU)
        BindBOPropertyToLabel(State.MyChildBO, "VendorSkuDescription", lblNewItemSKUDesciption)
        BindBOPropertyToLabel(State.MyChildBO, "PriceBandRangeFrom", lblNewItemLowPrice)
        BindBOPropertyToLabel(State.MyChildBO, "PriceBandRangeTo", lblNewItemHighPrice)
        BindBOPropertyToLabel(State.MyChildBO, "ReplacementTaxType", lblNewItemSelectTaxType)
        BindBOPropertyToLabel(State.MyChildBO, "CurrencyId", lblcurrency)
        BindBOPropertyToLabel(State.MyChildBO, "PriceListDetailTypeId", lbldetailtype)
        BindBOPropertyToLabel(State.MyChildBO, "CalculationPercent", lblcalculation)
        BindBOPropertyToLabel(State.MyChildBO, "ManufacturerOriginCode", lblNewManOri)
        BindBOPropertyToLabel(State.MyChildBO, "PartId", lblNewItemPart)
    End Sub

    ''' <summary>
    ''' Put back the current form values to BO
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub PopulateBOsFormFrom()
        With State.MyBO
            PopulateBOProperty(State.MyBO, "Code", txtCode.Text)
            PopulateBOProperty(State.MyBO, "Description", txtDescription.Text)
            PopulateBOProperty(State.MyBO, "Effective", txtEffective.Text)
            PopulateBOProperty(State.MyBO, "Expiration", txtExpirationDate.Text)
            PopulateBOProperty(State.MyBO, "CountryId", ddlCountry)
            PopulateBOProperty(State.MyBO, "DefaultCurrencyId", ddlDefaultCurrency)
            PopulateBOProperty(State.MyBO, "ManageInventoryId", ddlManageInventory, True)
        End With

        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub

    Protected Sub PopulateFormFromBOs()
        With State.MyBO
            PopulateControlFromBOProperty(txtCode, .Code)
            If txtCode.Text = "" Then
                ControlMgr.SetEnableControl(Me, txtCode, False)
            Else
                ControlMgr.SetEnableControl(Me, txtCode, True)
            End If

            PopulateControlFromBOProperty(txtDescription, .Description)
            PopulateControlFromBOProperty(txtEffective, .Effective)
            PopulateControlFromBOProperty(txtExpirationDate, .Expiration)

            'Populate dropdowns
            If State.IsNew Then
                'Set the country to the User's country
                If ddlCountry.Items.Count = 2 Then
                    'set the country as default selected
                    ddlCountry.SelectedIndex = 1
                    State.MyBO.CountryId = New Guid(ddlCountry.SelectedValue.ToString())
                End If
            Else
                PopulateControlFromBOProperty(ddlCountry, .CountryId)
            End If

            PopulateControlFromBOProperty(ddlManageInventory, .ManageInventoryId)
            If (.DefaultCurrencyId.Equals(Guid.Empty)) Then
                Dim company As New ElitaPlus.BusinessObjectsNew.Company(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
                Dim currencyobj As New ElitaPlus.BusinessObjectsNew.Country(company.CountryId)
                PopulateControlFromBOProperty(ddlDefaultCurrency, currencyobj.PrimaryCurrencyId)
            Else
                PopulateControlFromBOProperty(ddlDefaultCurrency, .DefaultCurrencyId)
            End If

            ' user should allowed to edit effective date of future lists 
            ' any list having effective Date (not time) less than today will not be able to change effective date
            If .IsNew AndAlso Not .Effective.Value.Date < DateTime.Now.Date Then
                ControlMgr.SetEnableControl(Me, txtEffective, True)
                ControlMgr.SetEnableControl(Me, btneffective, True)
                ControlMgr.SetEnableControl(Me, txtCode, True)
            Else
                ControlMgr.SetEnableControl(Me, txtEffective, False)
                ControlMgr.SetEnableControl(Me, btneffective, False)
                ControlMgr.SetEnableControl(Me, txtCode, False)
            End If

            'Populate Selected Vendors list
            PopulateSelectedVendors()
            PopulateGrid()
            PopulategvPendingApprovals()

            'if the rule list is expired then lock the form preventing change
            If State.MyBO.Expiration.Value < DateTime.Now Then
                'disable everything
                EnableDisableFields(False)
            End If

            If State.MyBO.ManageInventoryId <> Guid.Empty Then
                ddlManageInventory.Enabled = False
            Else
                ddlManageInventory.Enabled = True
            End If

        End With
    End Sub

    Private Sub PopulateSelectedVendors()
        If Not (State.MyBO.Code = Nothing Or State.MyBO.Code = String.Empty) Then
            'ElitaPlusPage.BindListControlToDataView(moSelectedList, Me.State.MyBO.GetServiceCenterSelectionView(), "description", "service_center_id", False)
            Dim ServiceCenterList As New Collections.Generic.List(Of DataElements.ListItem)
            For Each detail As ServiceCenter In State.MyBO.ServiceCenterChildren
                Dim item As DataElements.ListItem = New DataElements.ListItem()
                item.Translation = detail.Description
                item.ListItemId = detail.Id
                item.Code = detail.PriceListCode
                ServiceCenterList.Add(item)
            Next
            moSelectedList.Populate(ServiceCenterList.ToArray(), New PopulateOptions() With
                {
                    .AddBlankItem = False
                })
        End If
    End Sub

    Sub EnableDisableButtons(enableToggle As Boolean)
        ControlMgr.SetEnableControl(Me, btnClone, enableToggle)
        ControlMgr.SetEnableControl(Me, btnDelete, enableToggle)
        ControlMgr.SetEnableControl(Me, btnSave, enableToggle)
        ControlMgr.SetEnableControl(Me, btnUndo, enableToggle)
        ControlMgr.SetEnableControl(Me, btnBack, enableToggle)
        ControlMgr.SetEnableControl(Me, btnAdd, enableToggle)
    End Sub

    Protected Sub EnableDisableFields(toggle As Boolean)

        EnableDisableButtons(toggle)

        ControlMgr.SetEnableControl(Me, btnDelete, True)
        ControlMgr.SetEnableControl(Me, btnAdd, True)
        ControlMgr.SetEnableControl(Me, btnClone, True)

        If State.MyBO.IsNew Then
            ControlMgr.SetEnableControl(Me, btnDelete, False)
            ControlMgr.SetEnableControl(Me, btnAdd, False)
            ControlMgr.SetEnableControl(Me, btnClone, False)
            ControlMgr.SetEnableControl(Me, btnDelete, False)
            ControlMgr.SetEnableControl(Me, btnSave, True)
            ControlMgr.SetEnableControl(Me, btnUndo, False)
            ControlMgr.SetEnableControl(Me, txtEffective, True)
            ControlMgr.SetEnableControl(Me, btneffective, True)
            ControlMgr.SetEnableControl(Me, txtCode, True)
            ControlMgr.SetEnableControl(Me, txtExpirationDate, True)
            ControlMgr.SetEnableControl(Me, btnExpiration, True)
            ControlMgr.SetEnableControl(Me, txtDescription, True)
        End If

        If State.IsEditMode Then
            ControlMgr.SetEnableControl(Me, btnUndo, True)
        Else
            ControlMgr.SetEnableControl(Me, btnUndo, False)
        End If

        'Effective date should be Editable for Price List where Effective Date >= Sysdate Defect 2735
        If State.MyBO.Effective.Value >= DateTime.Now Then
            ControlMgr.SetEnableControl(Me, txtEffective, True)
            ControlMgr.SetEnableControl(Me, btneffective, True)
        End If

        If State.MyBO.Expiration.Value < DateTime.Now Then
            ControlMgr.SetEnableControl(Me, btnDelete, False)
            ControlMgr.SetEnableControl(Me, btnAdd, False)
            ControlMgr.SetEnableControl(Me, btnSave, False)
            ControlMgr.SetEnableControl(Me, btnUndo, False)
            ControlMgr.SetEnableControl(Me, txtEffective, False)
            ControlMgr.SetEnableControl(Me, btneffective, False)
            ControlMgr.SetEnableControl(Me, txtCode, False)
            ControlMgr.SetEnableControl(Me, txtExpirationDate, False)
            ControlMgr.SetEnableControl(Me, btnExpiration, False)
            ControlMgr.SetEnableControl(Me, txtDescription, False)
            ControlMgr.SetEnableControl(Me, ddlCountry, False)
        End If

    End Sub

    Protected Sub CreateNew()
        State.ScreenSnapShotBO = Nothing 'Reset the backup copy
        State.MyBO = New PriceList
        State.MyChildBO = New PriceListDetail
        PopulateFormFromBOs()
        State.IsEditMode = False
        EnableDisableFields(True)
    End Sub

    Protected Sub CreateNewWithCopy()
        State.IsACopy = True
        Dim newObj As New PriceList
        newObj.Copy(State.MyBO)

        State.MyBO = newObj
        State.MyBO.Code = State.MyBO.Code & "clone"
        PopulateFormFromBOs()
        PopulateControlFromBOProperty(txtCode, State.MyBO.Code)

        EnableDisableFields(True)
        'create the backup copy
        State.ScreenSnapShotBO = New PriceList
        State.ScreenSnapShotBO.Clone(State.MyBO)
        State.IsACopy = False
        State.IsEditMode = False
        EnableDisableFields(True)

        'Ovrride the usual edit feidls
        ControlMgr.SetVisibleControl(Me, moSelectedList, True)
        ControlMgr.SetEnableControl(Me, txtExpirationDate, True)
        ControlMgr.SetEnableControl(Me, txtDescription, True)
        ControlMgr.SetEnableControl(Me, ddlManageInventory, True)
        ControlMgr.SetEnableControl(Me, ddlCountry, True)
        ControlMgr.SetEnableControl(Me, btnSave, True)
        ControlMgr.SetEnableControl(Me, txtCode, True)
        ControlMgr.SetEnableControl(Me, txtEffective, True)
    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
        If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then

            If State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr AndAlso State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.Accept Then
                BindBoPropertiesToLabels()
                State.MyBO.Save()
            End If

            Select Case State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    MasterPage.MessageController.AddSuccess(ElitaPlus.ElitaPlusWebApp.Message.SAVE_RECORD_CONFIRMATION)
                    CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    MasterPage.MessageController.AddSuccess(ElitaPlus.ElitaPlusWebApp.Message.SAVE_RECORD_CONFIRMATION)
                    CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    ReturnToCallingPage(New ReturnType(State.ActionInProgress, State.MyBO, State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.Accept
                    If State.MyBO.IsDirty Then
                        If State.OverlapExists Then
                            If State.MyBO.ExpireOverLappingList() Then
                                State.OverlapExists = False
                            End If
                        End If
                        State.MyBO.Save()
                        State.HasDataChanged = True
                        PopulateFormFromBOs()
                        MasterPage.MessageController.AddSuccess(ElitaPlus.ElitaPlusWebApp.Message.SAVE_RECORD_CONFIRMATION)
                    Else
                        MasterPage.MessageController.AddError(ElitaPlus.ElitaPlusWebApp.Message.MSG_RECORD_NOT_SAVED)
                    End If
                Case ElitaPlusPage.DetailPageCommand.Delete
                    If Not State.PriceListDetailSelectedChildId.Equals(Guid.Empty) Then
                        BeginPriceListDetailChildEdit()
                    End If

                    Try
                        If (State.MyChildBO.Effective.Value <= PriceListDetail.GetCurrentDateTime()) Then
                            State.MyChildBO.BeginEdit()
                            State.MyChildBO.Accept(New ExpirationVisitor())
                            State.MyChildBO.EndEdit()
                            State.MyChildBO.Save()
                        ElseIf (State.MyChildBO.Effective.Value > PriceListDetail.GetCurrentDateTime()) Then
                            State.MyChildBO.Delete()
                            State.MyChildBO.Save()
                            State.MyChildBO.EndEdit()
                            State.PriceListDetailSelectedChildId = Guid.Empty
                        End If
                    Catch ex As Exception
                        State.MyChildBO.RejectChanges()
                        MasterPage.MessageController.AddError(ElitaPlus.ElitaPlusWebApp.Message.ERR_DELETING_DATA)
                        Throw ex
                    End Try
                    PopulateGrid()
            End Select
        ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
            Select Case State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    MasterPage.MessageController.AddErrorAndShow(State.LastErrMsg)
                Case ElitaPlusPage.DetailPageCommand.Accept
                    EnableDisableFields(True)
            End Select
        End If

        State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        HiddenSaveChangesPromptResponse.Value = ""
    End Sub

    Protected Sub PopulateDropdowns()
        'Dim languageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        'Dim companyGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
        'Me.BindListControlToDataView(ddlServiceClass, LookupListNew.GetServiceClassLookupList(languageId))
        ddlServiceClass.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="SVCCLASS", languageCode:=Thread.CurrentPrincipal.GetLanguageCode()),
                        New PopulateOptions() With
                        {
                            .AddBlankItem = True
                        })
        'Me.BindListControlToDataView(ddlEquipmentlist, LookupListNew.GetEquipmentListLookupListforPriceList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, DateTime.Now), , , False)
        Dim oListContext1 As New ListContext
        oListContext1.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
        Dim EquipmentList As DataElements.ListItem() =
                                CommonConfigManager.Current.ListManager.GetList(listCode:="EquipmentByCompanyGroup", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=oListContext1)
        ddlEquipmentlist.Populate(EquipmentList.ToArray(), New PopulateOptions() With
            {
                .AddBlankItem = False,
                .TextFunc = Function(x)
                                Return x.ExtendedCode + "_" + x.Translation
                            End Function,
                .SortFunc = Function(x)
                                Return x.ExtendedCode + "_" + x.Translation
                            End Function
            })

        ddlEquipmentlist.Items.Insert(0, New ListItem(TranslationBase.TranslateLabelOrMessage(ANY), Guid.Empty.ToString()))
        'Country          
        'Me.BindListControlToDataView(Me.ddlCountry, LookupListNew.GetUserCountriesLookupList())
        Dim CountryList As DataElements.ListItem() =
                            CommonConfigManager.Current.ListManager.GetList(listCode:=ListCodes.Country)

        Dim UserCountries As DataElements.ListItem() = (From Country In CountryList
                                                        Where ElitaPlusIdentity.Current.ActiveUser.Countries.Contains(Country.ListItemId)
                                                        Select Country).ToArray()
        ddlCountry.Populate(UserCountries.ToArray(),
                        New PopulateOptions() With
                        {
                            .AddBlankItem = True
                        })
        'Currency
        'Me.BindListControlToDataView(Me.ddlDefaultCurrency, LookupListNew.GetCurrencyTypeLookupList(), , , False)
        Dim CurrencyTypeList As DataElements.ListItem() =
                            CommonConfigManager.Current.ListManager.GetList(listCode:="CurrencyTypeList", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
        ddlDefaultCurrency.Populate(CurrencyTypeList.ToArray(),
                        New PopulateOptions() With
                        {
                            .AddBlankItem = False
                        })

        ' Manage inventory
        'Me.BindListControlToDataView(Me.ddlManageInventory, LookupListNew.GetManageInventoryLookupList(languageId))
        ddlManageInventory.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="MNGINVENT", languageCode:=Thread.CurrentPrincipal.GetLanguageCode()),
                        New PopulateOptions() With
                        {
                            .AddBlankItem = True
                        })
        AddCalendarwithTime_New(btneffective, txtEffective, , txtEffective.Text)
        AddCalendarwithTime_New(btnExpiration, txtExpirationDate, , txtExpirationDate.Text)
    End Sub

#End Region

#Region "Modal Page"

    Protected Sub PopulateModalControls()
        'Initialize Modal form
        'Dim languageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        'Dim companyGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
        'The following dropdowns are for the modal, 
        'Populate these without any values
        'Me.BindListControlToDataView(ddlNewItemServiceClass, LookupListNew.GetServiceClassLookupList(languageId))
        ddlNewItemServiceClass.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="SVCCLASS", languageCode:=Thread.CurrentPrincipal.GetLanguageCode()),
                        New PopulateOptions() With
                        {
                            .AddBlankItem = True
                        })
        If ddlNewItemServiceType.Items.Count > 0 Then
            ddlNewItemServiceType.Items.Clear()
        End If

        'Me.BindListControlToDataView(ddlNewItemServiceLevel, LookupListNew.GetServiceLevelLookupList(languageId))
        ddlNewItemServiceLevel.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="SVC_LVL", languageCode:=Thread.CurrentPrincipal.GetLanguageCode()),
                        New PopulateOptions() With
                        {
                            .AddBlankItem = True
                        })

        'Me.BindListControlToDataView(ddlNewItemMake, LookupListNew.GetManufacturerLookupList(companyGroupId))
        Dim oListContext2 As New ListContext
        oListContext2.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
        Dim MakeList As DataElements.ListItem() =
                                CommonConfigManager.Current.ListManager.GetList(listCode:="ManufacturerByCompanyGroup", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=oListContext2)
        ddlNewItemMake.Populate(MakeList.ToArray(), New PopulateOptions() With
            {
                .AddBlankItem = True
            })

        'populate tax type
        'Dim oTaxTypeList As DataView = LookupListNew.DropdownLookupList("TTYP", ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
        'oTaxTypeList.RowFilter &= " and CODE IN ('7', '8')"
        'Me.BindListControlToDataView(ddlNewItemReplacementTaxType, oTaxTypeList)

        Dim TaxTypeList As DataElements.ListItem() =
                            CommonConfigManager.Current.ListManager.GetList(listCode:="TTYP", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())

        Dim FilteredTaxTypeList As DataElements.ListItem() = (From lst In TaxTypeList
                                                              Where lst.Code = "7" Or lst.Code = "8"
                                                              Select lst).ToArray()

        ddlNewItemReplacementTaxType.Populate(FilteredTaxTypeList.ToArray(), New PopulateOptions() With
            {
                .AddBlankItem = True
            })

        'Dim manufacturerIdString As String = Guid.Empty.ToString()
        If (ddlNewItemMake.SelectedIndex <> NO_ITEM_SELECTED_INDEX) Then
            'manufacturerIdString = ddlNewItemMake.Items(ddlNewItemMake.SelectedIndex).Value
            'Me.BindListControlToDataView(ddlNewItemModel, PriceListDetail.GetModelsByMake(ElitaPlusPage.GetSelectedItem(ddlNewItemMake)).Tables(0).DefaultView)
            Dim oListContext1 As New ListContext
            oListContext1.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            oListContext1.ManufacturerId = ElitaPlusPage.GetSelectedItem(ddlNewItemMake)
            Dim ModelList As DataElements.ListItem() =
                                CommonConfigManager.Current.ListManager.GetList(listCode:="EquipmentByCompanyGroupEquipmentType", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=oListContext1)
            ddlNewItemModel.Populate(ModelList, New PopulateOptions() With
                {
                    .AddBlankItem = True
                })
        End If
        'Me.BindListControlToDataView(ddlNewItemCondition, LookupListNew.GetConditionLookupList(languageId))
        ddlNewItemCondition.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="TEQP", languageCode:=Thread.CurrentPrincipal.GetLanguageCode()),
                        New PopulateOptions() With
                        {
                            .AddBlankItem = True
                        })
        ddlNewItemCondition.Enabled = False

        'Risk Type and Equipment Class
        'Me.BindListControlToDataView(ddlRiskType, LookupListNew.GetRiskTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id), , , False)
        Dim oListContext As New ListContext
        oListContext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
        Dim RiskTypeList As DataElements.ListItem() =
                                    CommonConfigManager.Current.ListManager.GetList(listCode:="RiskTypeByCompanyGroup", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=oListContext)
        ddlRiskType.Populate(RiskTypeList, New PopulateOptions() With
            {
                .AddBlankItem = True
            })
        'ddlRiskType.Items.Insert(0, New ListItem(TranslationBase.TranslateLabelOrMessage(ANY), Guid.Empty.ToString()))
        'Me.BindListControlToDataView(ddlEquipmentClass, LookupListNew.GetEquipmentClassLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId))
        ddlEquipmentClass.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="EQPCLS", languageCode:=Thread.CurrentPrincipal.GetLanguageCode()),
                        New PopulateOptions() With
                        {
                            .AddBlankItem = True
                        })
        'Me.BindListControlToDataView(ddlcurrency, LookupListNew.GetCurrencyTypeLookupList(), , , False)
        ddlcurrency.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="CurrencyTypeList", languageCode:=Thread.CurrentPrincipal.GetLanguageCode()),
                        New PopulateOptions() With
                        {
                            .AddBlankItem = False
                        })
        'Me.BindListControlToDataView(ddldetailtype, LookupListNew.DropdownLookupList(LookupListCache.LK_PRICE_LIST_DETAIL_TYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId))
        ddldetailtype.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="PLDTYP", languageCode:=Thread.CurrentPrincipal.GetLanguageCode()),
                        New PopulateOptions() With
                        {
                            .AddBlankItem = True
                        })

        If ddlNewItemPart.Items.Count > 0 Then
            ddlNewItemPart.Items.Clear()
            ddlNewItemPart.Enabled = False
        End If

        ddlNewManOri.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="MAN_ORI", languageCode:=Thread.CurrentPrincipal.GetLanguageCode()),
                New PopulateOptions() With
                {
                    .AddBlankItem = True,
                    .BlankItemValue = Nothing,
                    .TextFunc = AddressOf PopulateOptions.GetDescription,
                    .ValueFunc = AddressOf PopulateOptions.GetExtendedCode
                })
        ddlNewManOri.Enabled = False

        AddCalendarwithTime_New(imgNewItemEffectiveDate, txtNewItemEffectiveDate, , txtNewItemEffectiveDate.Text)
        AddCalendarwithTime_New(imgNewItemExpirationDate, txtNewItemExpirationDate, , txtNewItemExpirationDate.Text)
        txtNewItemEffectiveDate.Text = String.Empty
        txtNewItemExpirationDate.Text = String.Empty
        txtNewItemPrice.Text = String.Empty
        txtNewItemQuantity.Text = String.Empty
        txtNewItemVendorSKU.Text = String.Empty
        txtNewItemSKUDescription.Text = String.Empty
        txtNewItemLowPrice.Text = String.Empty
        txtNewItemHighPrice.Text = String.Empty
    End Sub

    Protected Sub ddlManageInventory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlManageInventory.SelectedIndexChanged
        Try
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Protected Sub ddlNewItemMake_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNewItemMake.SelectedIndexChanged
        Try
            ddlNewItemModel.Items.Clear()
            ddlNewItemModel.Enabled = False

            ddlNewItemCondition.ClearSelection()

            EnableDisableControls(ddlNewItemCondition, True)

            If (ddlNewItemMake.SelectedIndex <> NO_ITEM_SELECTED_INDEX AndAlso ddlNewItemMake.SelectedIndex <> BLANK_ITEM_SELECTED) Then
                ddlNewItemModel.Enabled = True
                'Dim dv As DataView = PriceListDetail.GetModelsByMake(ElitaPlusPage.GetSelectedItem(ddlNewItemMake)).Tables(0).DefaultView
                'ElitaPlusPage.BindListControlToDataView(ddlNewItemModel, dv, , , True)
                Dim oListContext As New ListContext
                oListContext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                oListContext.ManufacturerId = ElitaPlusPage.GetSelectedItem(ddlNewItemMake)
                Dim ModelList As DataElements.ListItem() =
                                    CommonConfigManager.Current.ListManager.GetList(listCode:="EquipmentByCompanyGroupEquipmentType", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=oListContext)
                ddlNewItemModel.Populate(ModelList, New PopulateOptions() With
                    {
                        .AddBlankItem = True
                    })


            End If

            mdlPopup.Show()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub ddlRiskType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlRiskType.SelectedIndexChanged
        Try
            ddlNewItemPart.Items.Clear()
            ddlNewItemPart.Enabled = False

            ddlNewManOri.ClearSelection()

            If (ddlRiskType.SelectedIndex <> NO_ITEM_SELECTED_INDEX AndAlso ddlRiskType.SelectedIndex <> BLANK_ITEM_SELECTED) Then
                ddlNewItemPart.Enabled = True

                Dim oListContext As New ListContext
                oListContext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                Dim RiskTypeId As Guid = ElitaPlusPage.GetSelectedItem(ddlRiskType)
                Dim oRisktType As RiskType = New RiskType(RiskTypeId)
                oListContext.RiskGroupId = oRisktType.RiskGroupId

                Dim PartList As DataElements.ListItem() =
                                        CommonConfigManager.Current.ListManager.GetList(listCode:="PartsByCompanyGroupRiskGroup", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=oListContext)
                ddlNewItemPart.Populate(PartList.ToArray(), New PopulateOptions() With
                    {
                        .AddBlankItem = True
                    })
            Else
                ddlNewManOri.Enabled = False
            End If

            mdlPopup.Show()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub ddlServiceClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlServiceClass.SelectedIndexChanged
        Try
            ddlServiceType.Items.Clear()
            If (ddlServiceClass.SelectedIndex <> NO_ITEM_SELECTED_INDEX) Then
                'Dim dv As DataView = SpecialService.getServiceTypesForServiceClass(ElitaPlusPage.GetSelectedItem(ddlServiceClass), ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0).DefaultView
                'ElitaPlusPage.BindListControlToDataView(ddlServiceType, dv, msServiceClassColumnName, , True)
                Dim oListContext As New ListContext
                oListContext.ServiceClassId = ElitaPlusPage.GetSelectedItem(ddlServiceClass)
                oListContext.LanguageId = Thread.CurrentPrincipal.GetLanguageId()
                Dim ServiceTypeList As DataElements.ListItem() =
                                    CommonConfigManager.Current.ListManager.GetList(listCode:="ServiceTypeByServiceClass", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=oListContext)
                ddlServiceType.Populate(ServiceTypeList, New PopulateOptions() With
                    {
                        .AddBlankItem = True
                    })
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Protected Sub ddlNewItemServiceClass_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlNewItemServiceClass.SelectedIndexChanged
        Try
            ddlNewItemServiceType.Items.Clear()
            ddlNewItemServiceType.Enabled = False

            If (ddlNewItemServiceClass.SelectedIndex <> NO_ITEM_SELECTED_INDEX) Then
                'Dim dsServiceTypes As DataSet
                'Dim dv As DataView = SpecialService.getServiceTypesForServiceClass(ElitaPlusPage.GetSelectedItem(ddlNewItemServiceClass), ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0).DefaultView
                'ElitaPlusPage.BindListControlToDataView(ddlNewItemServiceType, dv, msServiceClassColumnName, , True)
                Dim oListContext As New ListContext
                oListContext.ServiceClassId = ElitaPlusPage.GetSelectedItem(ddlNewItemServiceClass)
                oListContext.LanguageId = Thread.CurrentPrincipal.GetLanguageId()
                Dim ServiceTypeList As DataElements.ListItem() =
                                    CommonConfigManager.Current.ListManager.GetList(listCode:="ServiceTypeByServiceClass", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=oListContext)
                ddlNewItemServiceType.Populate(ServiceTypeList, New PopulateOptions() With
                    {
                        .AddBlankItem = True
                    })
                ddlNewItemServiceType.Enabled = True
            End If
            mdlPopup.Show()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Detail GRid"


    ''' <summary>
    ''' Populate the main detail lits grid
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Sub PopulateGrid()
        Try
            Dim dv As PriceList.PriceListDetailSelectionView = State.MyBO.GetPriceListSelectionView
            dv.Sort = State.SortExpression
            State.DetailSearchDV = dv
            Grid.AutoGenerateColumns = False

            If Not Page.IsPostBack Then
                Dim requestedByList As New List(Of String)
                If dv.Table.Rows.Count > 0 Then
                    For Each dr As DataRow In dv.Table(0).Table.Rows
                        requestedByList.Add(dr("requested_by").ToString())
                    Next
                    ddlsearch.DataSource = requestedByList.Distinct()
                    ddlsearch.DataBind()
                End If
                ddlsearch.Items.Insert(0, "select")
            End If
            Grid.PageSize = State.PageSize
            SetPageAndSelectedIndexFromGuid(dv, State.PriceListDetailSelectedChildId, Grid, State.PageIndex)

            If dv.Table.Rows.Count > 0 Then
                If Not Page.IsPostBack Then
                    Dim requestedByList As New List(Of String)
                    For Each dr As DataRow In dv.Table(0).Table.Rows
                        requestedByList.Add(dr("requested_by").ToString())
                    Next
                    ddlsearch.DataSource = requestedByList.Distinct()
                    ddlsearch.DataBind()
                    ddlsearch.Items.Insert(0, "select")
                End If
            Else
                ddlsearch.Items.Clear()
                ddlsearch.Items.Insert(0, "select")
            End If

            Grid.PageSize = State.PageSize
            SetPageAndSelectedIndexFromGuid(dv, State.PriceListDetailSelectedChildId, Grid, State.PageIndex)

            If Not ddlsearch.SelectedValue = "select" Then 'requested_by
                dv.RowFilter = "requested_by = '" & ddlsearch.SelectedValue & "'"
                Grid.DataSource = dv
            Else
                Grid.DataSource = dv 'Me.State.DetailSearchDV
            End If
            Grid.DataBind()
            State.PageIndex = Grid.PageIndex
            ShowHideQuantity()

            ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
            ControlMgr.SetVisibleControl(Me, cboPageSize, Grid.Visible)
            ControlMgr.SetVisibleControl(Me, lblRecordCounts, True)
            ControlMgr.SetVisibleControl(Me, lblPageSize, True)

            Session("recCount") = dv.Count 'Me.State.DetailSearchDV.Count
            lblRecordCounts.Text = dv.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)

            If dv.Count = 0 Then
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                'ControlMgr.SetVisibleControl(Me, cboPageSize, False)
                ControlMgr.SetEnableControl(Me, btnSubmitforApproval, False)
            Else
                dv.RowFilter = "status_xcd='PL_RECON_PROCESS-PENDINGSUBMISSION'"
                Dim pendingSubmissionRows = dv.Count
                If pendingSubmissionRows = 0 Then
                    ControlMgr.SetVisibleControl(Me, btnSubmitforApproval, False)
                Else
                    ControlMgr.SetVisibleControl(Me, btnSubmitforApproval, True)
                End If
            End If


        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
        Try
            If State.SortExpression.StartsWith(e.SortExpression) Then
                If State.SortExpression.EndsWith(" DESC") Then
                    State.SortExpression = e.SortExpression
                Else
                    State.SortExpression &= " DESC"
                End If
            Else
                State.SortExpression = e.SortExpression
            End If
            State.PageIndex = 0
            PopulateGrid()
            HighLightSortColumn(Grid, State.SortExpression, True)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Private Sub Grid_PageIndexChanged(sender As Object, e As System.EventArgs) Handles Grid.PageIndexChanged
        Try
            State.PageIndex = Grid.PageIndex
            State.PriceListId = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Grid.PageIndex = e.NewPageIndex
            State.PageIndex = Grid.PageIndex
            State.PriceListDetailSelectedChildId = Guid.Empty
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub Grid_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
        Try
            Dim priceListDetailId As Guid
            Dim oDataView As PriceListDetail.PriceListDetailSearchDV

            'Populate the grid with detail info
            If e.CommandName = ElitaPlusSearchPage.EDIT_COMMAND_NAME Then
                State.IsGridInEditMode = True
                State.SelectedGridValueToEdit = New Guid(e.CommandArgument.ToString())
                State.ChildActionInProgress = DetailPageCommand.NewAndCopy

                'priceListDetailId = New Guid(e.CommandArgument.ToString())
                State.PriceListDetailSelectedChildId = New Guid(e.CommandArgument.ToString())
                ' Me.State.MyChildBO = New PriceListDetail(Me.State.PriceListDetailSelectedChildId)
                PopulateModalControls()
                BeginPriceListDetailChildEdit()
                PopulateDetailFromPriceListDetailChildBO()


                '' condition
                'Me.PopulateControlFromBOProperty(ddlNewItemCondition, Me.State.MyChildBO.ConditionId)
                ' vendor sku
                txtNewItemVendorSKU.Text = State.MyChildBO.VendorSku
                ' description
                txtNewItemSKUDescription.Text = State.MyChildBO.VendorSkuDescription
                ' price
                txtNewItemPrice.Text = State.MyChildBO.Price.ToString()
                'Calculation Percentage
                If State.MyChildBO.CalculationPercent IsNot Nothing Then
                    txtcalculationpercent.Text = State.MyChildBO.CalculationPercent.ToString()
                Else
                    txtcalculationpercent.Text = 0.0
                End If
                ' effective date
                txtNewItemEffectiveDate.Text = State.MyChildBO.Effective.ToString()
                ' expiration date
                txtNewItemExpirationDate.Text = State.MyChildBO.Expiration.ToString()
                'Low Price
                txtNewItemLowPrice.Text = State.MyChildBO.PriceBandRangeFrom.ToString()
                'High Price
                txtNewItemHighPrice.Text = State.MyChildBO.PriceBandRangeTo.ToString()
                'vendor quantity

                If Not State.MyChildBO.GetVendorQuantiy().Equals(Guid.Empty) AndAlso Not String.IsNullOrEmpty(State.MyChildBO.VendorQuantity.ToString()) Then
                    txtNewItemQuantity.Text = State.MyChildBO.VendorQuantity.ToString()
                Else
                    txtNewItemQuantity.Text = String.Empty
                End If


                ' BeginPriceListDetailChildEdit()
                ModalPopupenabledisable()
                mdlPopup.Show()
            ElseIf e.CommandName = ElitaPlusSearchPage.DELETE_COMMAND_NAME Then
                State.IsGridInEditMode = False
                priceListDetailId = New Guid(e.CommandArgument.ToString())
                State.PriceListDetailSelectedChildId = New Guid(e.CommandArgument.ToString())
                DisplayMessage(Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete

                ' Me.MasterPage.MessageController.AddSuccess(ElitaPlus.ElitaPlusWebApp.Message.DELETE_RECORD_CONFIRMATION)
            ElseIf e.CommandName = ElitaPlusSearchPage.HISTORY_COMMAND_NAME Then
                State.IsGridInEditMode = False
                priceListDetailId = New Guid(e.CommandArgument.ToString())
                State.PriceListDetailSelectedChildId = New Guid(e.CommandArgument.ToString())
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.ViewHistory
                PopulateGridHistory()
                mpeHistory.Show()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Public Sub ModalPopupenabledisable()
        Dim controlCurrent As Control
        Dim dv As DataView = LookupListNew.DropdownLookupList(LookupListCache.LK_PRICE_LIST_DETAIL_TYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
        If Not ddldetailtype.SelectedValue.ToString() = LookupListNew.GetIdFromCode(dv, "UD").ToString() Then
            pnlud.Enabled = False
        Else
            pnlud.Enabled = True
        End If
    End Sub

    Private Sub Grid_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then

            ' Assign the detail id to the command agrument
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim btnEditItem As ImageButton
            Dim btnDeleteItem As ImageButton

            'Dim lnkbtnhistory As LinkButton
            'Dim lknlinkdummy As LinkButton

            'If (Not e.Row.Cells(Me.GRID_COL_EDITID_IDX).FindControl(BTN_CONTROL_EDIT_DETAIL_LIST) Is Nothing) Then
            '    'Edit Button argument changed to id
            '    btnEditItem = CType(e.Row.Cells(Me.GRID_COL_EDITID_IDX).FindControl(BTN_CONTROL_EDIT_DETAIL_LIST), ImageButton)
            '    btnEditItem.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(PriceListDetail.PriceListDetailSearchDV.COL_PRICE_LIST_DETAIL_ID), Byte()))
            '    btnEditItem.CommandName = ElitaPlusSearchPage.EDIT_COMMAND_NAME

            '    If Not (e.Row.Cells(Me.GRID_COL_EXPIRATION_DATEID_IDX).Text.ToString().Equals(String.Empty)) Then
            '        If (DateHelper.GetDateValue(e.Row.Cells(Me.GRID_COL_EXPIRATION_DATEID_IDX).Text.ToString()) < DateTime.Now) Then
            '            'e.Row.Cells(Me.GRID_COL_EDITID_IDX).Visible = False
            '            btnEditItem.Visible = False
            '        End If
            '    End If
            'End If
            Dim lblStatusXCD As Label
            lblStatusXCD = CType(e.Row.Cells(GRID_COL_STATUS_XCD_IDX).FindControl("lblStatusXCD"), Label)

            If (e.Row.Cells(GRID_COL_EDITID_IDX).FindControl(BTN_CONTROL_EDIT_DETAIL_LIST) IsNot Nothing) Then
                'Edit Button argument changed to id
                btnEditItem = CType(e.Row.Cells(GRID_COL_EDITID_IDX).FindControl(BTN_CONTROL_EDIT_DETAIL_LIST), ImageButton)
                btnEditItem.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(PriceListDetail.PriceListDetailSearchDV.COL_PRICE_LIST_DETAIL_ID), Byte()))
                btnEditItem.CommandName = ElitaPlusSearchPage.EDIT_COMMAND_NAME

                If Not (e.Row.Cells(GRID_COL_EXPIRATION_DATEID_IDX).Text.ToString().Equals(String.Empty)) Then
                    If (DateHelper.GetDateValue(e.Row.Cells(GRID_COL_EXPIRATION_DATEID_IDX).Text.ToString()) < DateTime.Now And lblStatusXCD.Text = "PL_RECON_PROCESS-APPROVED") Then
                        'e.Row.Cells(Me.GRID_COL_EDITID_IDX).Visible = False
                        btnEditItem.Visible = False
                    End If
                End If
                If lblStatusXCD.Text = "PL_RECON_PROCESS-PENDINGAPPROVAL" Then
                    btnEditItem.Visible = False
                End If
            End If

            If (e.Row.Cells(GRID_COL_DELETEID_IDX).FindControl(BTN_CONTROL_DELETE_DETAIL_LIST) IsNot Nothing) Then
                'Delete Button argument changed to id
                btnDeleteItem = CType(e.Row.Cells(GRID_COL_DELETEID_IDX).FindControl(BTN_CONTROL_DELETE_DETAIL_LIST), ImageButton)
                btnDeleteItem.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(PriceListDetail.PriceListDetailSearchDV.COL_PRICE_LIST_DETAIL_ID), Byte()))
                btnDeleteItem.CommandName = ElitaPlusSearchPage.DELETE_COMMAND_NAME
                'Me.AddControlMsg(btnDeleteItem, Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, True)

                If Not (e.Row.Cells(GRID_COL_EXPIRATION_DATEID_IDX).Text.ToString().Equals(String.Empty)) Then
                    If (DateHelper.GetDateValue(e.Row.Cells(GRID_COL_EXPIRATION_DATEID_IDX).Text.ToString()) < DateTime.Now And lblStatusXCD.Text = "PL_RECON_PROCESS-APPROVED") Then
                        btnDeleteItem.Visible = False
                    End If
                End If
                If lblStatusXCD.Text = "PL_RECON_PROCESS-PENDINGAPPROVAL" Then
                    btnDeleteItem.Visible = False
                End If
            End If

            Dim btnViewHistory As LinkButton = CType(e.Row.Cells(GRID_COL_VIEW_HISTORY_IDX).FindControl("btnViewHistory"), LinkButton)
            btnViewHistory.Visible = True
            btnViewHistory.Text = TranslationBase.TranslateLabelOrMessage("VIEWHISTORY")
            btnViewHistory.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(PriceListDetail.PriceListDetailSearchDV.COL_PRICE_LIST_DETAIL_ID), Byte()))
            btnViewHistory.CommandName = ElitaPlusSearchPage.HISTORY_COMMAND_NAME

            ChekAndReplaceWithParentData(e.Row, PriceListDetail.PriceListDetailSearchDV.COL_MAKE, PriceListDetail.PriceListDetailSearchDV.COL_PARENT_MAKE)
            ChekAndReplaceWithParentData(e.Row, PriceListDetail.PriceListDetailSearchDV.COL_MODEL, PriceListDetail.PriceListDetailSearchDV.COL_PARENT_MODEL)
            ChekAndReplaceWithParentData(e.Row, PriceListDetail.PriceListDetailSearchDV.COL_CONDITION_ID, PriceListDetail.PriceListDetailSearchDV.COL_PARENT_CONDITION_ID)
            ChekAndReplaceWithParentText(e.Row, GRID_COL_MODEL_IDX, PriceListDetail.PriceListDetailSearchDV.COL_PARENT_MODEL_DESCRIPTION)
            ChekAndReplaceWithParentText(e.Row, GRID_COL_CONDITIONID_IDX, PriceListDetail.PriceListDetailSearchDV.COL_PARENT_CONDITION_DESCRIPTION)
        End If
    End Sub

    Private Sub ChekAndReplaceWithParentData(row As GridViewRow, valueField As String, parentValueField As String)
        'Dim cellField As String = row.Cells(cellIndex)
        Dim dvRow As DataRowView = CType(row.DataItem, DataRowView)

        If ((dvRow(valueField) Is DBNull.Value Or
            (TypeOf (dvRow(valueField)) Is Byte() AndAlso (New Guid(CType(dvRow(valueField), Byte())) <> Guid.Empty))) AndAlso
            (dvRow(parentValueField) IsNot DBNull.Value Or
            (TypeOf (dvRow(parentValueField)) Is Byte() AndAlso (New Guid(CType(dvRow(parentValueField), Byte())) = Guid.Empty)))) Then

            dvRow(valueField) = dvRow(parentValueField)
        End If
    End Sub

    Private Sub ChekAndReplaceWithParentText(row As GridViewRow, cellIndex As Integer, parentValueField As String)
        'Dim cellField As String = row.Cells(cellIndex)
        Dim dvRow As DataRowView = CType(row.DataItem, DataRowView)

        If (dvRow(parentValueField.Replace("parent_", String.Empty)) Is DBNull.Value AndAlso dvRow(parentValueField) IsNot DBNull.Value) Then
            row.Cells(cellIndex).Text = dvRow(parentValueField).ToString()
        End If
    End Sub
    Private Sub cboPageSize_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            State.SelectedPageSize = State.PageSize
            State.PageIndex = NewCurrentPageIndex(Grid, State.DetailSearchDV.Count, State.PageSize)
            Grid.PageIndex = State.PageIndex
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Detail GRid for Approval"
    Dim isfromdropdown As Boolean = False

    ' <summary>
    ' Populate the main detail lits grid
    ' </summary>
    ' <remarks></remarks>
    ' 
    Sub PopulategvPendingApprovals()
        Try
            Dim dv As PriceList.PriceListDetailSelectionView = State.MyBO.GetPriceListSelectionView
            dv.Sort = State.SortExpression
            State.DetailSearchDV = dv
            gvPendingApprovals.AutoGenerateColumns = False
            dv.RowFilter = "status_xcd='PL_RECON_PROCESS-PENDINGAPPROVAL'"
            If isfromdropdown = False Then 'Not Page.IsPostBack Then
                Dim requestedByList As New List(Of String)
                If dv.Table.Rows.Count > 0 Then
                    For Each dr As DataRow In dv.Table(0).Table.Rows
                        If dr("status_xcd").ToString() = "PL_RECON_PROCESS-PENDINGAPPROVAL" Then
                            requestedByList.Add(dr("requested_by").ToString())
                        End If
                    Next
                    ddlpasearch.DataSource = requestedByList.Distinct()
                    ddlpasearch.DataBind()
                End If
                ddlpasearch.Items.Insert(0, "select")
            End If
            gvPendingApprovals.PageSize = State.PageSize
            SetPageAndSelectedIndexFromGuid(dv, State.PriceListDetailSelectedChildId, gvPendingApprovals, State.PageIndex)


            If Not ddlpasearch.SelectedValue = "select" Then
                dv.RowFilter = "requested_by = '" & ddlpasearch.SelectedValue & "' and status_xcd='PL_RECON_PROCESS-PENDINGAPPROVAL'"
                gvPendingApprovals.DataSource = dv
            Else
                dv.RowFilter = "status_xcd='PL_RECON_PROCESS-PENDINGAPPROVAL'"
                gvPendingApprovals.DataSource = dv
            End If
            gvPendingApprovals.DataBind()
            State.PageIndex = gvPendingApprovals.PageIndex
            ShowHideQuantity()

            ControlMgr.SetVisibleControl(Me, trPageSizePendingApprovals, gvPendingApprovals.Visible)
            ControlMgr.SetVisibleControl(Me, cboPageSizePendingApproval, gvPendingApprovals.Visible)
            ControlMgr.SetVisibleControl(Me, lblPendingApprovalRecordCounts, True)
            'ControlMgr.SetVisibleControl(Me, btnApprove, True)
            'ControlMgr.SetVisibleControl(Me, btnReject, True)
            ControlMgr.SetEnableControl(Me, btnApprove, True)
            ControlMgr.SetEnableControl(Me, btnReject, True)
            Session("recCount") = dv.Count
            lblPendingApprovalRecordCounts.Text = dv.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)

            If dv.Count = 0 Then
                ControlMgr.SetVisibleControl(Me, trPageSizePendingApprovals, False)
                'ControlMgr.SetVisibleControl(Me, cboPageSizePendingApproval, False)
                'ControlMgr.SetVisibleControl(Me, btnApprove, False)
                'ControlMgr.SetVisibleControl(Me, btnReject, False)
                ControlMgr.SetEnableControl(Me, btnApprove, False)
                ControlMgr.SetEnableControl(Me, btnReject, False)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Private Sub gvPendingApprovals_SortCommand(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvPendingApprovals.Sorting
        Try
            If State.SortExpression.StartsWith(e.SortExpression) Then
                If State.SortExpression.EndsWith(" DESC") Then
                    State.SortExpression = e.SortExpression
                Else
                    State.SortExpression &= " DESC"
                End If
            Else
                State.SortExpression = e.SortExpression
            End If
            State.PageIndex = 0
            PopulategvPendingApprovals()
            HighLightSortColumn(gvPendingApprovals, State.SortExpression, True)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub
    Dim CheckBoxArray As ArrayList

    Private Sub gvPendingApprovals_PageIndexChanged(sender As Object, e As System.EventArgs) Handles gvPendingApprovals.PageIndexChanged
        Try
            State.PageIndex = gvPendingApprovals.PageIndex
            State.PriceListId = Guid.Empty
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub gvPendingApprovals_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvPendingApprovals.PageIndexChanging
        Try
            Dim i As Integer = e.NewPageIndex
            If i = 0 Then
                i = 0
            Else
                i = e.NewPageIndex - 1
            End If
            savecheckedcheckboxes(i)
            gvPendingApprovals.PageIndex = e.NewPageIndex
            State.PageIndex = gvPendingApprovals.PageIndex
            State.PriceListDetailSelectedChildId = Guid.Empty
            PopulategvPendingApprovals()
            populateCheckBoxValues(i)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub gvPendingApprovals_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvPendingApprovals.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub cboPageSizePendingApproval_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboPageSizePendingApproval.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSizePendingApproval.SelectedValue, Integer)
            State.SelectedPageSize = State.PageSize
            State.PageIndex = NewCurrentPageIndex(gvPendingApprovals, State.DetailSearchDV.Count, State.PageSize)
            gvPendingApprovals.PageIndex = State.PageIndex
            PopulategvPendingApprovals()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "View History Grid"


    ''' <summary>
    ''' Populate the main detail lits grid
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Sub PopulateGridHistory()
        Try
            Dim ds As DataSet = State.MyBO.ViewPriceListDetailHistory(State.PriceListDetailSelectedChildId, Authentication.CurrentUser.LanguageId)
            gvHistory.AutoGenerateColumns = False
            gvHistory.PageSize = State.PageSize
            gvHistory.DataSource = ds 'Me.State.DetailSearchDV
            gvHistory.DataBind()
            State.PageIndex = gvHistory.PageIndex
            ShowHideQuantity()

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Private Sub gvHistory_SortCommand(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvHistory.Sorting
        Try
            If State.SortExpression.StartsWith(e.SortExpression) Then
                If State.SortExpression.EndsWith(" DESC") Then
                    State.SortExpression = e.SortExpression
                Else
                    State.SortExpression &= " DESC"
                End If
            Else
                State.SortExpression = e.SortExpression
            End If
            State.PageIndex = 0
            PopulateGridHistory()
            HighLightSortColumn(Grid, State.SortExpression, True)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Private Sub gvHistory_PageIndexChanged(sender As Object, e As System.EventArgs) Handles gvHistory.PageIndexChanged
        Try
            State.PageIndex = gvHistory.PageIndex
            State.PriceListId = Guid.Empty
            PopulateGridHistory()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)

        End Try
    End Sub

    Private Sub gvHistory_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvHistory.PageIndexChanging
        Try
            gvHistory.PageIndex = e.NewPageIndex
            State.PageIndex = gvHistory.PageIndex
            State.PriceListDetailSelectedChildId = Guid.Empty
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub gvHistory_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvHistory.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region
    Sub PopulateDetailFromPriceListDetailChildBO()
        ' service class
        PopulateControlFromBOProperty(ddlNewItemServiceClass, State.MyChildBO.ServiceClassId)

        If (ddlNewItemServiceClass.SelectedIndex <> NO_ITEM_SELECTED_INDEX) Then
            'Dim dsServiceTypes As DataSet
            'Dim dv As DataView = SpecialService.getServiceTypesForServiceClass(ElitaPlusPage.GetSelectedItem(ddlNewItemServiceClass), ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0).DefaultView
            'ElitaPlusPage.BindListControlToDataView(ddlNewItemServiceType, dv, msServiceClassColumnName, , True)
            Dim oListContext As New ListContext
            oListContext.ServiceClassId = ElitaPlusPage.GetSelectedItem(ddlNewItemServiceClass)
            oListContext.LanguageId = Thread.CurrentPrincipal.GetLanguageId()
            Dim ServiceTypeList As DataElements.ListItem() =
                                    CommonConfigManager.Current.ListManager.GetList(listCode:="ServiceTypeByServiceClass", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=oListContext)
            ddlNewItemServiceType.Populate(ServiceTypeList, New PopulateOptions() With
                    {
                        .AddBlankItem = True
                    })
            ddlNewItemServiceType.Enabled = True
        End If

        ' service type
        If Not (State.MyChildBO.ServiceTypeId = Guid.Empty) Then
            PopulateControlFromBOProperty(ddlNewItemServiceType, State.MyChildBO.ServiceTypeId)
        End If

        'select Make
        If (ddlNewItemMake.Items.FindByValue(State.MyChildBO.MakeId.ToString())) IsNot Nothing Then
            PopulateControlFromBOProperty(ddlNewItemMake, State.MyChildBO.MakeId)
        End If

        'make
        'US 255424 - Using Parent Equipment ID if it is coming on dataset
        If (State.MyChildBO.EquipmentId <> Guid.Empty OrElse State.MyChildBO.Parent_EquipmentId <> Guid.Empty) Then
            ddlNewItemModel.Enabled = True

            'Dim companyGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            Dim manufacturerIdString As String = Guid.Empty.ToString()
            If (ddlNewItemMake.SelectedIndex <> NO_ITEM_SELECTED_INDEX) Then
                manufacturerIdString = ddlNewItemMake.Items(ddlNewItemMake.SelectedIndex).Value
                'Me.BindListControlToDataView(ddlNewItemModel, LookupListNew.GetEquipmentLookupList(companyGroupId))
                Dim oListContext As New ListContext
                oListContext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                Dim EquipmentList As DataElements.ListItem() =
                                    CommonConfigManager.Current.ListManager.GetList(listCode:="EquipmentByCompanyGroup", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=oListContext)
                ddlNewItemModel.Populate(EquipmentList, New PopulateOptions() With
                    {
                        .AddBlankItem = True
                    })

                'US 255424 - Using Parent Equipment ID if it is coming on dataset
                Dim eqmnt_Id As Guid = If(State.MyChildBO.Parent_EquipmentId = Guid.Empty, State.MyChildBO.EquipmentId, State.MyChildBO.Parent_EquipmentId)
                If (ddlNewItemModel.Items.FindByValue(eqmnt_Id.ToString())) IsNot Nothing Then
                    PopulateControlFromBOProperty(ddlNewItemModel, eqmnt_Id)
                End If
            End If
        End If

        If State.MyChildBO.CurrencyId.Equals(Guid.Empty) Then
            Dim company As New Company(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
            Dim currencyobj As New Country(company.CountryId)
            SetSelectedItem(ddlcurrency, currencyobj.PrimaryCurrencyId)
        Else
            PopulateControlFromBOProperty(ddlcurrency, State.MyChildBO.CurrencyId)
        End If

        PopulateControlFromBOProperty(ddldetailtype, State.MyChildBO.PriceListDetailTypeId)
        'Dim dv1 As DataView = LookupListNew.DropdownLookupList(LookupListCache.LK_PRICE_LIST_DETAIL_TYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
        Dim PriceListDetailTypeList As DataElements.ListItem() =
                                    CommonConfigManager.Current.ListManager.GetList(listCode:="PLDTYP", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
        Dim templateId As Guid = (From lst In PriceListDetailTypeList
                                  Where lst.Code = LookupListCache.LK_TEMPLATE_CODE
                                  Select lst.ListItemId).FirstOrDefault()

        If ddldetailtype.SelectedValue.ToString() = templateId.ToString() Then
            txtcalculationpercent.Visible = True
            lblcalculation.Visible = True
            ddlcurrency.Visible = False
            lblNewItemPrice.Visible = False
            txtNewItemPrice.Visible = False
            lblcurrency.Visible = False
            ddldetailtype.Enabled = False
        Else
            txtcalculationpercent.Visible = False
            lblcalculation.Visible = False
            ddlcurrency.Visible = True
            lblNewItemPrice.Visible = True
            txtNewItemPrice.Visible = True
            lblcurrency.Visible = True
            ddldetailtype.Enabled = False
        End If

        PopulateControlFromBOProperty(ddlNewItemServiceLevel, State.MyChildBO.ServiceLevelId)

        'US 255424 - Using Parent Condition ID if it's coming on dataset
        Dim condition_id As Guid = If(State.MyChildBO.Parent_ConditionId = Guid.Empty, State.MyChildBO.ConditionId, State.MyChildBO.Parent_ConditionId)
        PopulateControlFromBOProperty(ddlNewItemCondition, condition_id)

        EnableDisableControls(ddlNewItemCondition, (State.MyChildBO.EquipmentId = Guid.Empty))

        PopulateControlFromBOProperty(ddlRiskType, State.MyChildBO.RiskTypeId)
        PopulateControlFromBOProperty(ddlEquipmentClass, State.MyChildBO.EquipmentClassId)
        PopulateControlFromBOProperty(ddlNewItemReplacementTaxType, State.MyChildBO.ReplacementTaxType)

        If (ddlRiskType.SelectedIndex <> NO_ITEM_SELECTED_INDEX AndAlso ddlRiskType.SelectedIndex <> BLANK_ITEM_SELECTED) Then

            Dim oListContext As New ListContext
            oListContext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            oListContext.RiskGroupId = Nothing

            Dim RiskTypeId As Guid = ElitaPlusPage.GetSelectedItem(ddlRiskType)
            Dim oRisktType As RiskType = New RiskType(RiskTypeId)

            oListContext.RiskGroupId = oRisktType.RiskGroupId

            Dim PartList As DataElements.ListItem() =
                                CommonConfigManager.Current.ListManager.GetList(listCode:="PartsByCompanyGroupRiskGroup", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=oListContext)
            ddlNewItemPart.Populate(PartList.ToArray(), New PopulateOptions() With
            {
                .AddBlankItem = True
            })
            ddlNewItemPart.Enabled = True
        End If

        ' part 
        If Not (State.MyChildBO.PartId.Equals(Guid.Empty)) Then

            ddlNewManOri.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="MAN_ORI", languageCode:=Thread.CurrentPrincipal.GetLanguageCode()),
                New PopulateOptions() With
                {
                    .AddBlankItem = True,
                    .BlankItemValue = Nothing,
                    .TextFunc = AddressOf PopulateOptions.GetDescription,
                    .ValueFunc = AddressOf PopulateOptions.GetExtendedCode
                })

            If (ddlNewItemPart.Items.FindByValue(State.MyChildBO.PartId.ToString()) IsNot Nothing) Then
                PopulateControlFromBOProperty(ddlNewItemPart, State.MyChildBO.PartId)

                ddlNewManOri.Enabled = True
            End If
        End If

        If Not (State.MyChildBO.ManufacturerOriginCode?.ToString() Is Nothing AndAlso ddlNewManOri.Items IsNot Nothing AndAlso ddlNewManOri.Items.Count > 0) Then
            'Fix for Bug 257627 - Cannot use Me.PopulateControlFromBOProperty because it's expecting Me.State.MyChildBO.ManufacturerOriginCode as GUID but it's a string
            Dim selctedItem As ListItem = ddlNewManOri.Items.FindByValue(State.MyChildBO.ManufacturerOriginCode)
            If (selctedItem IsNot Nothing) Then
                selctedItem.Selected = True
            Else
                ddlNewManOri.SelectedIndex = BLANK_ITEM_SELECTED
            End If
        End If

    End Sub

    Private Sub ddlNewItemModel_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlNewItemModel.SelectedIndexChanged
        Try
            If (ddlNewItemModel.SelectedIndex <> NO_ITEM_SELECTED_INDEX AndAlso ddlNewItemModel.SelectedIndex <> BLANK_ITEM_SELECTED) Then
                'US 255424 - Part selection keeps its Enable Status when Model is selected
                'EnableDisableControls(ddlNewItemPart, True)
                EnableDisableControls(ddlNewItemCondition, False)
            Else
                'US 255424 - Part selection keeps its Enable Status when Model is selected
                'EnableDisableControls(ddlNewItemPart, False)
                EnableDisableControls(ddlNewItemCondition, True)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub ddlNewItemPart_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlNewItemPart.SelectedIndexChanged
        Try
            If (ddlNewItemPart.SelectedIndex <> NO_ITEM_SELECTED_INDEX AndAlso ddlNewItemPart.SelectedIndex <> BLANK_ITEM_SELECTED) Then
                'US 255424 - Model selection keeps its Enable Status when part is selected
                'EnableDisableControls(ddlNewItemModel, True)
                EnableDisableControls(ddlNewManOri, False)
            Else
                'US 255424 - Model selection keeps its Enable Status when part is selected
                'EnableDisableControls(ddlNewItemModel, False)
                EnableDisableControls(ddlNewManOri, True)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub ddlCountry_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCountry.SelectedIndexChanged
        Dim currencyid As Guid = Guid.Parse(GetCurrencyId(1))
        SetSelectedItem(ddlDefaultCurrency, currencyid)
    End Sub

    Private Function GetCurrencyId(input As Boolean) As String
        Dim selectedcountry As String = ddlCountry.SelectedItem.ToString()
        Dim country As New ElitaPlus.BusinessObjectsNew.Country(LookupListNew.GetIdFromDescription("COUNTRIES", selectedcountry))
        Dim currency As New Currency(country.PrimaryCurrencyId)
        If (input) Then
            Return country.PrimaryCurrencyId.ToString()
        Else
            Return currency.Notation.ToString()
        End If
    End Function

    Protected Sub btnSubmitforApproval_Click(sender As Object, e As EventArgs) Handles btnSubmitforApproval.Click

        Try
            isPendingApprovalRefresh = True
            State.MyBO.ProcessPriceListByStatus(State.MyBO.Id, String.Empty, Authentication.CurrentUser.NetworkId, "PL_RECON_PROCESS-PENDINGAPPROVAL")
            PopulateGrid()
            MasterPage.MessageController.AddSuccess(ElitaPlus.ElitaPlusWebApp.Message.PRICE_LIST_SUBMISSION_PROCESS_SUCCESS)
            PopulategvPendingApprovals()
        Catch ex As Exception
            MasterPage.MessageController.AddError(ElitaPlus.ElitaPlusWebApp.Message.PRICE_LIST_SUBMISSION_PROCESS_FAILED)
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnApprove_Click(sender As Object, e As EventArgs) Handles btnApprove.Click
        Try
            Dim isChecked As Boolean = False
            Dim PricelistDetailIdList As String = String.Empty
            Dim lstPriceListDetail As New Collections.Generic.List(Of String)
            Dim HeaderCheckbox As CheckBox = CType(gvPendingApprovals.HeaderRow.FindControl("chkHeaderApproveOrReject"), CheckBox)

            If ViewState("SelectedPriceListDeatilId") IsNot Nothing Then
                lstPriceListDetail = DirectCast(ViewState("SelectedPriceListDeatilId"), Collections.Generic.List(Of String))
                If lstPriceListDetail.Count > 0 Then
                    PricelistDetailIdList = String.Join(",", lstPriceListDetail.ToArray())
                    isChecked = True
                End If
            Else
                If HeaderCheckbox.Checked = False Then
                    'Dim lstPriceListDetail As New Collections.Generic.List(Of String)
                    For Each gvrow As GridViewRow In gvPendingApprovals.Rows
                        Dim chkApproveOrReject As CheckBox = CType(gvrow.FindControl("chkApproveOrReject"), CheckBox)
                        If chkApproveOrReject.Checked Then
                            Dim lblCtrl As Label
                            lblCtrl = CType(gvrow.Cells(23).FindControl("lblPriceListDetailID"), Label)
                            lstPriceListDetail.Add(MiscUtil.GetDbStringFromGuid(New Guid(lblCtrl.Text)))
                            isChecked = True
                        End If
                    Next
                    PricelistDetailIdList = String.Join(",", lstPriceListDetail.ToArray())
                Else
                    isChecked = True
                End If
            End If
            If isChecked Then
                State.MyBO.ProcessPriceListByStatus(State.MyBO.Id, PricelistDetailIdList, Authentication.CurrentUser.NetworkId, "PL_RECON_PROCESS-APPROVED")
                MasterPage.MessageController.AddSuccess(ElitaPlus.ElitaPlusWebApp.Message.PRICE_LIST_APPROVAL_PROCESS_SUCCESS)
            Else
                MasterPage.MessageController.AddError(ElitaPlus.ElitaPlusWebApp.Message.PRICE_LIST_APPROVE_OR_REJECT_CHECKBOX_NOT_SELECTED)
            End If
            isPendingApprovalRefresh = True
            PopulateGrid()
            PopulategvPendingApprovals()
        Catch ex As Exception
            MasterPage.MessageController.AddError(ElitaPlus.ElitaPlusWebApp.Message.PRICE_LIST_APPROVAL_PROCESS_FAILED)
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnReject_Click(sender As Object, e As EventArgs) Handles btnReject.Click
        Try
            Dim isChecked As Boolean = False
            Dim PricelistDetailIdList As String = String.Empty
            Dim lstPriceListDetail As New Collections.Generic.List(Of String)
            Dim HeaderCheckbox As CheckBox = CType(gvPendingApprovals.HeaderRow.FindControl("chkHeaderApproveOrReject"), CheckBox)
            If ViewState("SelectedPriceListDeatilId") IsNot Nothing Then
                lstPriceListDetail = DirectCast(ViewState("SelectedPriceListDeatilId"), Collections.Generic.List(Of String))
                If lstPriceListDetail.Count > 0 Then
                    PricelistDetailIdList = String.Join(",", lstPriceListDetail.ToArray())
                    isChecked = True
                End If
            Else
                If HeaderCheckbox.Checked = False Then
                    For Each gvrow As GridViewRow In gvPendingApprovals.Rows
                        Dim chkApproveOrReject As CheckBox = CType(gvrow.FindControl("chkApproveOrReject"), CheckBox)
                        If chkApproveOrReject.Checked Then
                            Dim lblCtrl As Label
                            lblCtrl = CType(gvrow.Cells(23).FindControl("lblPriceListDetailID"), Label)
                            lstPriceListDetail.Add(MiscUtil.GetDbStringFromGuid(New Guid(lblCtrl.Text)))
                            isChecked = True
                        End If
                    Next
                    PricelistDetailIdList = String.Join(",", lstPriceListDetail.ToArray())
                Else
                    isChecked = True
                End If
            End If
            If isChecked Then
                State.MyBO.ProcessPriceListByStatus(State.MyBO.Id, PricelistDetailIdList, Authentication.CurrentUser.NetworkId, "PL_RECON_PROCESS-REJECTED")
                MasterPage.MessageController.AddSuccess(ElitaPlus.ElitaPlusWebApp.Message.PRICE_LIST_REJECTION_PROCESS_SUCCESS)
            Else
                MasterPage.MessageController.AddError(ElitaPlus.ElitaPlusWebApp.Message.PRICE_LIST_APPROVE_OR_REJECT_CHECKBOX_NOT_SELECTED)
            End If
            isPendingApprovalRefresh = True
            PopulateGrid()
            PopulategvPendingApprovals()
        Catch ex As Exception
            MasterPage.MessageController.AddError(ElitaPlus.ElitaPlusWebApp.Message.PRICE_LIST_REJECTION_PROCESS_FAILED)
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btntxtsearch_Click(sender As Object, e As EventArgs) Handles btntxtsearch.Click
        PopulateGrid()
    End Sub

    Protected Sub btnpaSearch_Click(sender As Object, e As EventArgs) Handles btnpaSearch.Click
        isfromdropdown = True
        PopulategvPendingApprovals()
    End Sub
    Private Function savecheckedcheckboxes(rIndex As Integer)
        Dim boxlist As ArrayList = New ArrayList()
        Dim index As Integer = -1
        Dim chkHeader As CheckBox = CType(gvPendingApprovals.HeaderRow.FindControl("chkHeaderApproveOrReject"), CheckBox)
        Dim chAll As String = "ChkAll -" + rIndex.ToString()
        Dim lstPriceListDetail As New Collections.Generic.List(Of String)
        If ViewState("SelectedValues") IsNot Nothing Then
            boxlist = DirectCast(ViewState("SelectedValues"), ArrayList)
        End If
        If ViewState("SelectedPriceListDeatilId") IsNot Nothing Then
            lstPriceListDetail = DirectCast(ViewState("SelectedPriceListDeatilId"), Collections.Generic.List(Of String))
        End If
        If boxlist.IndexOf(chAll) = -1 Then
            boxlist.Add(chAll)
            index = 0
            For Each gvrow As GridViewRow In gvPendingApprovals.Rows
                Dim chkApproveOrReject As CheckBox = CType(gvrow.FindControl("chkApproveOrReject"), CheckBox)
                'index = CType(gvPendingApprovals.DataKeys(gvrow.RowIndex).Value, Integer)

                If chkApproveOrReject.Checked Then
                    Dim lblCtrl As Label
                    lblCtrl = CType(gvrow.Cells(23).FindControl("lblPriceListDetailID"), Label)
                    Dim currentListDetailId As String = MiscUtil.GetDbStringFromGuid(New Guid(lblCtrl.Text))
                    lstPriceListDetail.Add(currentListDetailId)
                    If boxlist.IndexOf(index) = -1 Then
                        boxlist.Add(index.ToString())
                    Else
                        boxlist.Remove(index.ToString())
                    End If
                    If lstPriceListDetail.IndexOf(currentListDetailId) = -1 Then
                        lstPriceListDetail.Add(currentListDetailId.ToString())
                    Else
                        lstPriceListDetail.Remove(currentListDetailId.ToString())
                    End If
                End If


                index = index + 1
            Next
            If boxlist IsNot Nothing And boxlist.Count > 0 Then
                ViewState("SelectedValues") = boxlist
            End If
            If lstPriceListDetail IsNot Nothing And lstPriceListDetail.Count > 0 Then
                ViewState("SelectedPriceListDeatilId") = lstPriceListDetail
            End If
        End If
        Return 0
    End Function

    Private Function populateCheckBoxValues(rIndex As Integer)
        Dim boxlist As ArrayList = New ArrayList()
        rIndex = gvPendingApprovals.PageIndex
        Dim ChAll As String = "ChkAll -" + rIndex.ToString()
        Dim index As Integer = -1
        If ViewState("SelectedValues") IsNot Nothing Then
            boxlist = DirectCast(ViewState("SelectedValues"), ArrayList)
        End If
        If boxlist.IndexOf(ChAll) <> -1 Then
            index = 0
            Dim chkHeader As CheckBox = CType(gvPendingApprovals.HeaderRow.FindControl("chkHeaderApproveOrReject"), CheckBox)
            chkHeader.Checked = True
            For Each gvrow As GridViewRow In gvPendingApprovals.Rows
                Dim chkApproveOrReject As CheckBox = CType(gvrow.FindControl("chkApproveOrReject"), CheckBox)
                'index = CType(gvPendingApprovals.DataKeys(gvrow.RowIndex).Value, Integer)
                If boxlist.IndexOf(index.ToString()) <> -1 Then
                    chkApproveOrReject.Checked = True
                End If
                index = index + 1
            Next
        End If
        Return 0
    End Function
End Class
