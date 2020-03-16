Imports Microsoft.VisualBasic
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Imports AjaxControlToolkit

Public Class PriceListDetailForm
    Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "



    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As PriceList
        Public HasDataChanged As Boolean
        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As PriceList, ByVal hasDataChanged As Boolean)
            Me.LastOperation = LastOp
            Me.EditingBo = curEditingBo
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

        Public MyChildVendorBO As VendorQuantity
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
            Return Me.State.MyBO.IsNew
        End Get

    End Property

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                Me.State.MyBO = New PriceList(CType(Me.CallingParameters, Guid))

                Me.State.IsEditMode = True
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        Try
            Me.MenuEnabled = True
            Me.IsReturningFromChild = True
            Dim retObj As PriceListDetailForm.ReturnType = CType(ReturnPar, PriceListDetailForm.ReturnType)
            Me.State.HasDataChanged = retObj.HasDataChanged
            Select Case retObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Back
                    If Not retObj Is Nothing Then
                        If Not retObj.EditingBo.IsNew Then
                            Me.State.PriceListId = retObj.EditingBo.Id
                        End If
                    End If
                Case ElitaPlusPage.DetailPageCommand.Delete
                    Me.DisplayMessage(Message.DELETE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
            End Select
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Page Events"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            Me.UpdateBreadcrum()
            Me.MasterPage.MessageController.Clear()

            If Not Me.IsPostBack Then
                Me.MenuEnabled = False
                Me.AddControlMsg(Me.btnDelete, Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, True)

                If Me.State.MyBO Is Nothing Then
                    Me.State.MyBO = New PriceList
                    Me.State.IsNew = True
                End If

                Me.TranslateGridHeader(Grid)
                Me.TranslateGridHeader(gvPendingApprovals)
                Me.TranslateGridHeader(gvHistory)
                Me.PopulateDropdowns()
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields(True)

                moSelectedTitle.Text = TranslationBase.TranslateLabelOrMessage("SELECTED VENDORS")
                cboPageSize.SelectedValue = Me.State.PageSize.ToString()
                cboPageSizePendingApproval.SelectedValue = Me.State.PageSize.ToString()
            End If

            Me.CheckIfComingFromSaveConfirm()
            Me.BindBoPropertiesToLabels()

            If Not Me.IsPostBack Then
                Me.AddLabelDecorations(Me.State.MyBO)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub

#End Region

    Private Sub UpdateBreadcrum()
        'Breadcrumb and titles
        Me.MasterPage.UsePageTabTitleInBreadCrum = False
        Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("SERVICE_NETWORK")
        Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("PRICE_LIST_DETAIL")
        Me.MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage("TABLES") & ElitaBase.Sperator & Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("EDIT_PRICE_LIST")
    End Sub

#Region "Button Clicks"

    ''' <summary>
    ''' main back button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.PopulateBOsFormFrom()
            If Me.State.MyBO.IsDirty Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
            Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            Me.State.LastErrMsg = Me.MasterPage.MessageController.Text
        End Try
    End Sub

    ''' <summary>
    ''' Main save button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' 
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            ' if the bo is not already in the past
            If Not Me.State.IsNew Then
                If DateHelper.GetDateValue(Me.State.MyBO.Effective.Value.ToString) > DateHelper.GetDateValue(CStr(DateTime.Now.Date)) Then
                    ' if new effective date is in the past throw exception
                    If DateHelper.GetDateValue(Me.txtEffective.Text.ToString()) < DateHelper.GetDateValue(CStr(DateTime.Now.Date)) Then
                        Throw New GUIException(Message.MSG_GUI_INVALID_EFFECTIVE_DATE_SMALLER_THAN_SYSDATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_EFFECTIVE_DATE_SMALLER_THAN_SYSDATE)
                    End If
                End If
            End If

            Me.PopulateBOsFormFrom()
            Me.State.MyBO.Validate()

            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If

            If Me.txtEffective.Text = String.Empty Then
                Throw New GUIException(Message.PRICELIST_INVALID_EFFECIVE_DATE, Assurant.ElitaPlus.Common.ErrorCodes.PRICELIST_INVALID_EFFECIVE_DATE)
            End If

            If Me.CheckOverlap() Then
                If Me.CheckExistingFutureListOverlap() Then
                    Throw New GUIException(Message.MSG_GUI_OVERLAPPING_PRICE_LIST, Assurant.ElitaPlus.Common.ErrorCodes.PRICELIST_INVALID_EFFECIVE_DATE)
                End If
                Me.DisplayMessage(Message.MSG_GUI_OVERLAPPING_PRICE_LIST, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = DetailPageCommand.Accept
                Me.State.OverlapExists = True
                Exit Sub
            End If

            If (Me.State.MyBO.IsDirty OrElse Me.State.MyBO.IsFamilyDirty) Then
                Me.State.HasDataChanged = True
                Me.State.MyBO.Save()
                Me.State.HasDataChanged = False
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields(True)
                Me.ClearGridViewHeadersAndLabelsErrSign()
                Me.MasterPage.MessageController.AddSuccess(ElitaPlus.ElitaPlusWebApp.Message.SAVE_RECORD_CONFIRMATION)
            Else
                Me.MasterPage.MessageController.AddError(ElitaPlus.ElitaPlusWebApp.Message.MSG_RECORD_NOT_SAVED)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    ''' <summary>
    ''' Main undo button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnUndo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo.Click
        Try
            If Not Me.State.MyBO.IsNew Then
                'Reload from the DB
                Me.State.MyBO = New PriceList(Me.State.MyBO.Id)
            ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                'It was a new with copy
                Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
            Else
                Me.State.MyBO = New PriceList
            End If
            Me.PopulateFormFromBOs()
            Me.EnableDisableFields(True)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    ''' <summary>
    ''' Main delete button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If Me.State.MyBO.IsPriceListAssignedtoServiceCenter Then
                Throw New GUIException(Message.MSG_GUI_PRICE_LIST_ASSIGNED_TO_SERVICE_CENTER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_LIST_CODE_ERR)
            Else
                If Me.State.MyBO.Effective.Value > DateTime.Now Then
                    Me.State.MyBO.BeginEdit()
                    Me.State.MyBO.Delete()
                    Me.State.MyBO.Save()
                    Me.State.HasDataChanged = True
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, Me.State.MyBO, Me.State.HasDataChanged))
                Else
                    Me.State.MyBO.Accept(New ExpirationVisitor)
                    Me.State.MyBO.Save()
                    Me.State.HasDataChanged = True
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Expire, Me.State.MyBO, Me.State.HasDataChanged))
                End If
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            'undo the delete
            Me.State.MyBO.RejectChanges()
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    ''' <summary>
    ''' Main new/add button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Me.PopulateBOsFormFrom()
            If (Me.State.MyBO.IsDirty) And Not (Me.State.MyBO.Code = String.Empty Or Me.State.MyBO.Code = Nothing) Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                Me.CreateNew()
                Me.ClearGridHeadersAndLabelsErrSign()
                'Me.callPage(PriceListDetailForm.URL)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    ''' <summary>
    ''' main copy button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClone.Click
        Try
            If Me.State.MyBO.IsDirty Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            Else
                Me.CreateNewWithCopy()
            End If
            Me.PopulateBOsFormFrom()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
    Private Sub btnAddEquipment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddEquipment.Click
        Try
            If (ddlEquipmentlist.SelectedIndex <> BLANK_ITEM_SELECTED) Then
                If BulkAddEquipment() Then
                    Me.State.HasDataChanged = False
                    Me.PopulateGrid()
                    Me.ClearGridViewHeadersAndLabelsErrSign()

                    ddlEquipmentlist.SelectedIndex = BLANK_ITEM_SELECTED
                    ddlServiceClass.SelectedIndex = BLANK_ITEM_SELECTED
                    ddlServiceType.SelectedIndex = BLANK_ITEM_SELECTED
                    txtNewEquipVendorSKU.Text = String.Empty
                    txtNewEquipSKUDescription.Text = String.Empty

                    Me.MasterPage.MessageController.AddSuccess(Message.RECORD_ADDED_OK)
                Else
                    Me.MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
                End If
            Else
                Me.MasterPage.MessageController.AddError(Message.INVALID_SELECT_EQUIPMENT_LIST)
            End If
        Catch BO_ex As BOValidationException
            Me.HandleErrors(BO_ex, Me.MasterPage.MessageController)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
                Me.State.MyChildBO = Me.State.MyBO.GetNewPriceListDetailChild
                'Me.State.MyChildBO.BeginEdit()

                ' Equipment Id
                Me.State.MyChildBO.EquipmentId = New Guid(ddlEquipmentlist.SelectedValue)

                ' Service Type
                If Not ddlServiceType Is Nothing Then
                    Me.PopulateBOProperty(Me.State.MyChildBO, "ServiceTypeId", New Guid(ddlServiceType.SelectedValue))
                    Me.State.MyChildBO.ServiceTypeCode = ddlServiceType.SelectedItem.Text.ToString()
                End If

                ' Service Class
                If Not ddlServiceClass Is Nothing Then
                    Me.PopulateBOProperty(Me.State.MyChildBO, "ServiceClassId", New Guid(ddlServiceClass.SelectedValue))
                    Me.State.MyChildBO.ServiceClassCode = ddlServiceClass.SelectedItem.Text.ToString()
                End If

                'Get make model from the equipment
                If Not Me.State.MyChildBO.EquipmentId.Equals(Guid.Empty) Then
                    Dim ds As New DataSet
                    ds = (New PriceListDetail).GetMakeModelByEquipmentId(Me.State.MyChildBO.EquipmentId, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
                    If ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
                        Me.PopulateBOProperty(Me.State.MyChildBO, "Make", ds.Tables(0).Rows(0)("Make").ToString())
                        Me.PopulateBOProperty(Me.State.MyChildBO, "Model", ds.Tables(0).Rows(0)("Model").ToString())
                        Me.PopulateBOProperty(Me.State.MyChildBO, "MakeId", GuidControl.ByteArrayToGuid(ds.Tables(0).Rows(0)("manufacturer_id")))
                    End If
                End If

                ' Currency
                Dim company As New ElitaPlus.BusinessObjectsNew.Company(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
                Dim currencyobj As New ElitaPlus.BusinessObjectsNew.Country(company.CountryId)
                Me.State.MyChildBO.CurrencyId = currencyobj.PrimaryCurrencyId

                ' PriceList Detail Type
                Me.State.MyChildBO.PriceListDetailTypeId = LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListCache.LK_PRICE_LIST_DETAIL_TYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId), LookupListCache.LK_USER_DEFINED_CODE)

                Me.State.MyChildBO.PriceListId = Me.State.MyBO.Id

                'Vendor SKU
                Me.State.MyChildBO.VendorSku = Me.txtNewEquipVendorSKU.Text

                'Vendor Description
                Me.State.MyChildBO.VendorSkuDescription = Me.txtNewEquipSKUDescription.Text

                Me.State.MyChildBO.Price = CType(NO_PRICE, Decimal)
                Me.State.MyChildBO.PriceBandRangeFrom = PRICE_BAND_RANGE_FROM
                Me.State.MyChildBO.PriceBandRangeTo = PRICE_BAND_RANGE_TO

                'if parent effective date is in the past, then use current date or else use parent effective date                
                If (DateHelper.GetDateValue(Me.txtEffective.Text.ToString()) > PriceListDetail.GetCurrentDateTime()) Then
                    Me.State.MyChildBO.Effective = DateHelper.GetDateValue(Me.txtEffective.Text.ToString())
                Else
                    Me.State.MyChildBO.Effective = PriceListDetail.GetCurrentDateTime()
                End If

                Me.State.MyChildBO.Expiration = DateHelper.GetDateValue(Me.txtExpirationDate.Text.ToString())
                Me.State.MyChildBO.Validate()

                If Me.State.MyChildBO.IsDirty Then
                    ' check if already exists
                    ' same equipment id, condition id, service_class id, service type id and sku
                    If Me.State.MyChildBO.OverlapExists(False) Then
                        If Me.State.MyChildBO.ExpireOverLappingList() Then
                            'Me.State.MyChildBO.BeginEdit()
                            Me.State.MyChildBO.Effective = PriceListDetail.GetCurrentDateTime()
                            'Me.State.MyChildBO.EndEdit()
                            Me.State.ChildOverlapExists = False
                        Else
                            Me.State.ChildOverlapExists = True
                        End If
                    End If

                    Try
                        If Not (Me.State.ChildOverlapExists) Then
                            'Me.State.MyChildBO.BeginEdit()
                            Me.State.MyChildBO.Save()
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
                Me.DisplayMessage(Message.MSG_NO_RECORDS_FOUND, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                Exit Function
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Function

    ''' <summary>
    ''' The new button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' 
    Private Sub addBtn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles addBtnNewListItem.Click
        Try
            Me.State.ChildActionInProgress = DetailPageCommand.New_
            Me.PopulateModalControls()
            Me.ShowHideQuantity()
            'Me.State.MyChildBO = Nothing
            Me.State.PriceListDetailSelectedChildId = Guid.Empty
            BeginPriceListDetailChildEdit()
            'Me.SetSelectedItemByText(ddldetailtype, USER_DEFINED)
            Me.SetSelectedItem(ddldetailtype, LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListCache.LK_PRICE_LIST_DETAIL_TYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId), LookupListCache.LK_USER_DEFINED_CODE))
            If Me.State.MyChildBO.CurrencyId.Equals(Guid.Empty) Then
                Dim company As New ElitaPlus.BusinessObjectsNew.Company(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
                Dim currencyobj As New ElitaPlus.BusinessObjectsNew.Country(company.CountryId)
                Me.SetSelectedItem(ddlcurrency, currencyobj.PrimaryCurrencyId)
            Else
                Me.PopulateControlFromBOProperty(ddlcurrency, Me.State.MyChildBO.CurrencyId)
            End If
            ControlMgr.SetEnableControl(Me, ddldetailtype, True)
            ControlMgr.SetEnableControl(Me, ddlcurrency, True)
            ControlMgr.SetEnableControl(Me, txtNewItemPrice, True)
            ControlMgr.SetEnableControl(Me, lblNewItemPrice, True)

            'ddlRiskType.Items.Insert(0, New ListItem(TranslationBase.TranslateLabelOrMessage(ANY), Guid.Empty.ToString()))
            mdlPopup.Show()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Sub BeginPriceListDetailChildEdit()
        With Me.State
            If Not .PriceListDetailSelectedChildId.Equals(Guid.Empty) Then
                .MyChildBO = .MyBO.GetPriceListDetailChild(.PriceListDetailSelectedChildId)
            Else
                .MyChildBO = .MyBO.GetNewPriceListDetailChild
            End If
            .MyChildBO.BeginEdit()
        End With

        BindBoPropertiesToModalPopupLabels()
        Me.AddLabelDecorations(Me.State.MyChildBO)
        Me.lblcurrency.Text = lblcurrency.Text.Substring(0, lblcurrency.Text.Length - 1)
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
    Private Sub btnNewItemCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewItemCancel.Click
        Try

            Me.State.MyChildBO.cancelEdit()
            If Me.State.MyChildBO.IsSaveNew Then
                Me.State.MyChildBO.Delete()
                Me.State.MyChildBO.Save()
            End If
            mdlPopup.Hide()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    ''' <summary>
    ''' This is the save button inside the modal popup for new item
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Sub btnNewItemSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNewItemSave.Click
        Try

            'get the service type list id
            If (ddlNewItemServiceType.SelectedIndex <> NO_ITEM_SELECTED_INDEX) Then
                Me.PopulateBOProperty(Me.State.MyChildBO, "ServiceTypeId", New Guid(ddlNewItemServiceType.SelectedValue))
                Me.PopulateBOProperty(Me.State.MyChildBO, "ServiceTypeCode", ddlNewItemServiceType.SelectedItem.Text.ToString())
            End If

            'get the service class list id
            If (ddlNewItemServiceClass.SelectedIndex <> NO_ITEM_SELECTED_INDEX) Then
                Me.PopulateBOProperty(Me.State.MyChildBO, "ServiceClassId", New Guid(ddlNewItemServiceClass.SelectedValue))
                Me.PopulateBOProperty(Me.State.MyChildBO, "ServiceClassCode", ddlNewItemServiceClass.SelectedItem.Text.ToString())
            End If

            'get the service level list id
            If (ddlNewItemServiceLevel.SelectedIndex <> NO_ITEM_SELECTED_INDEX) Then
                Me.PopulateBOProperty(Me.State.MyChildBO, "ServiceLevelId", New Guid(ddlNewItemServiceLevel.SelectedValue))
                Me.PopulateBOProperty(Me.State.MyChildBO, "ServiceLevelCode", ddlNewItemServiceLevel.SelectedItem.Text.ToString())
            End If

            'get the service class list id
            If (ddlNewItemCondition.SelectedIndex <> NO_ITEM_SELECTED_INDEX AndAlso ddlNewItemMake.SelectedIndex <> BLANK_ITEM_SELECTED) Then
                Me.PopulateBOProperty(Me.State.MyChildBO, "ConditionId", New Guid(ddlNewItemCondition.SelectedValue))
                Me.PopulateBOProperty(Me.State.MyChildBO, "ConditionTypeCode", ddlNewItemCondition.SelectedItem.Text.ToString())
            Else
                Me.PopulateBOProperty(Me.State.MyChildBO, "ConditionId", Guid.Empty)
                Me.PopulateBOProperty(Me.State.MyChildBO, "ConditionTypeCode", String.Empty)
            End If

            'get the service class list id
            If (ddlNewItemReplacementTaxType.SelectedIndex <> NO_ITEM_SELECTED_INDEX) Then
                Me.PopulateBOProperty(Me.State.MyChildBO, "ReplacementTaxType", New Guid(ddlNewItemReplacementTaxType.SelectedValue))
            End If

            If (ddlNewItemPart.SelectedIndex <> NO_ITEM_SELECTED_INDEX) Then
                Me.PopulateBOProperty(Me.State.MyChildBO, "PartId", New Guid(ddlNewItemPart.SelectedValue))
                Me.PopulateBOProperty(Me.State.MyChildBO, "PartDescription", ddlNewItemPart.SelectedItem.Text.ToString())
            Else
                Me.PopulateBOProperty(Me.State.MyChildBO, "PartId", Guid.Empty)
                Me.PopulateBOProperty(Me.State.MyChildBO, "PartDescription", String.Empty)
            End If

            If (ddlNewManOri.SelectedIndex <> NO_ITEM_SELECTED_INDEX AndAlso ddlNewItemPart.SelectedIndex <> BLANK_ITEM_SELECTED) Then
                Me.PopulateBOProperty(Me.State.MyChildBO, "ManufacturerOriginCode", ddlNewManOri.SelectedValue)
                Me.PopulateBOProperty(Me.State.MyChildBO, "ManufacturerOriginDescription", ddlNewManOri.SelectedItem.Text.ToString())
            End If


            Me.State.MyChildBO.VendorSku = txtNewItemVendorSKU.Text
            Me.State.MyChildBO.VendorSkuDescription = txtNewItemSKUDescription.Text

            Me.PopulateBOProperty(Me.State.MyChildBO, "PriceBandRangeFrom", Me.txtNewItemLowPrice)
            Me.PopulateBOProperty(Me.State.MyChildBO, "PriceBandRangeTo", Me.txtNewItemHighPrice)

            Dim Currencyobj As String = GetCurrencyId(0)
            Me.State.MyChildBO.PriceLowWithSymbol = Currencyobj & "" & txtNewItemLowPrice.Text
            Me.State.MyChildBO.PriceHighWithSymbol = Currencyobj & "" & txtNewItemHighPrice.Text

            If txtNewItemEffectiveDate.Text <> "" Then
                If DateHelper.GetDateValue(Me.State.MyChildBO.Effective.ToString) <> DateHelper.GetDateValue(txtNewItemEffectiveDate.Text.ToString) Then
                    If (DateHelper.GetDateValue(Me.txtNewItemEffectiveDate.Text.ToString()) < DateTime.Today()) Then
                        Throw New GUIException(Message.MSG_GUI_INVALID_EFFECTIVE_DATE_SMALLER_THAN_SYSDATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_EFFECTIVE_DATE_SMALLER_THAN_SYSDATE)
                    End If
                End If
                Me.State.MyChildBO.Effective = DateHelper.GetDateValue(txtNewItemEffectiveDate.Text.Trim())
            Else
                ' The effecitve and exp dates will be set to parents dates
                'if parent effective date is in the past, then use current date or else use parent effective date
                'or else it will fail but bo
                If (DateHelper.GetDateValue(Me.txtEffective.Text.ToString()) > PriceListDetail.GetCurrentDateTime()) Then
                    Me.State.MyChildBO.Effective = DateHelper.GetDateValue(Me.txtEffective.Text.ToString())
                Else
                    Me.State.MyChildBO.Effective = PriceListDetail.GetCurrentDateTime()
                End If
            End If

            If txtNewItemExpirationDate.Text <> "" Then
                Me.State.MyChildBO.Expiration = DateHelper.GetDateValue(txtNewItemExpirationDate.Text.Trim())
            Else
                Me.State.MyChildBO.Expiration = DateHelper.GetDateValue(Me.txtExpirationDate.Text.ToString())
            End If

            If IsNumeric(txtNewItemPrice.Text) Then
                Me.State.MyChildBO.Price = CType(txtNewItemPrice.Text.Trim(), Decimal)
            End If

            Me.State.MyChildBO.PriceWithSymbol = Currencyobj & "" & txtNewItemPrice.Text

            Me.State.MyChildBO.PriceListId = Me.State.MyBO.Id

            'Equipment
            'get the equipment id
            Me.State.MyChildEquipmentBO = New Equipment
            If (ddlNewItemMake.SelectedIndex <> NO_ITEM_SELECTED_INDEX) Then
                Me.PopulateBOProperty(Me.State.MyChildEquipmentBO, "ManufacturerId", New Guid(ddlNewItemMake.SelectedValue))
                Me.PopulateBOProperty(Me.State.MyChildBO, "MakeId", New Guid(ddlNewItemMake.SelectedValue))
                Me.PopulateBOProperty(Me.State.MyChildBO, "Make", ddlNewItemMake.SelectedItem.Text.ToString())
            Else
                Me.PopulateBOProperty(Me.State.MyChildEquipmentBO, "ManufacturerId", Guid.Empty)
                Me.PopulateBOProperty(Me.State.MyChildBO, "MakeId", Guid.Empty)
                Me.PopulateBOProperty(Me.State.MyChildBO, "Make", String.Empty)
            End If
            If (ddlNewItemModel.SelectedIndex <> NO_ITEM_SELECTED_INDEX) Then
                Me.PopulateBOProperty(Me.State.MyChildBO, "EquipmentId", New Guid(ddlNewItemModel.SelectedValue))
                Me.PopulateBOProperty(Me.State.MyChildEquipmentBO, "Model", ddlNewItemModel.SelectedItem.Text.ToString())
                Me.PopulateBOProperty(Me.State.MyChildBO, "Model", ddlNewItemModel.SelectedItem.Text.ToString())
            Else
                Me.PopulateBOProperty(Me.State.MyChildBO, "EquipmentId", Guid.Empty)
                Me.PopulateBOProperty(Me.State.MyChildEquipmentBO, "Model", String.Empty)
                Me.PopulateBOProperty(Me.State.MyChildBO, "Model", String.Empty)
            End If


            If (ddlNewItemPart.SelectedIndex <> NO_ITEM_SELECTED_INDEX AndAlso ddlNewItemMake.SelectedIndex <> NO_ITEM_SELECTED_INDEX) Then
                Me.PopulateBOProperty(Me.State.MyChildBO, "Parent_MakeId", New Guid(ddlNewItemMake.SelectedValue))
                Me.PopulateBOProperty(Me.State.MyChildBO, "Parent_Make", ddlNewItemMake.SelectedItem.Text.ToString())

                Dim eqId As Guid = If(String.IsNullOrEmpty(ddlNewItemModel.SelectedValue), Guid.Empty, New Guid(ddlNewItemModel.SelectedValue))
                Me.PopulateBOProperty(Me.State.MyChildBO, "Parent_EquipmentId", eqId)

                Dim mdl = If(ddlNewItemModel.SelectedItem Is Nothing, String.Empty, ddlNewItemModel.SelectedItem.Text.ToString())
                Me.State.MyChildBO.Parent_Model = mdl

                Dim condId = If(String.IsNullOrEmpty(ddlNewItemCondition.SelectedValue), Guid.Empty, New Guid(ddlNewItemCondition.SelectedValue))
                Me.PopulateBOProperty(Me.State.MyChildBO, "Parent_ConditionId", condId)

                Dim condText = If(ddlNewItemCondition.SelectedItem Is Nothing, String.Empty, ddlNewItemCondition.SelectedItem.Text.ToString())
                Me.State.MyChildBO.Parent_ConditionTypeCode = condText
            End If
            'populate Risk Type id and Code
            Me.PopulateBOProperty(Me.State.MyChildBO, "RiskTypeId", New Guid(ddlRiskType.SelectedValue))
            Me.PopulateBOProperty(Me.State.MyChildBO, "RiskTypeCode", ddlRiskType.SelectedItem.Text.ToString())

            'Populate Equipment ClassID and Code
            Me.PopulateBOProperty(Me.State.MyChildBO, "EquipmentClassId", New Guid(ddlEquipmentClass.SelectedValue))
            Me.PopulateBOProperty(Me.State.MyChildBO, "EquipmentCode", ddlEquipmentClass.SelectedItem.Text.ToString())

            ' Populate Item Quantity
            Me.PopulateBOProperty(Me.State.MyChildBO, "Quantity", txtNewItemQuantity.Text.ToString())

            'populate Currency
            Me.PopulateBOProperty(Me.State.MyChildBO, "CurrencyId", New Guid(ddlcurrency.SelectedValue))

            'PriceListDetailType
            Me.PopulateBOProperty(Me.State.MyChildBO, "PriceListDetailTypeId", New Guid(ddldetailtype.SelectedValue))

            'Populate the vendor quantity
            If Not Me.State.MyChildBO.GetVendorQuantiy().Equals(Guid.Empty) Then
                Me.State.MyChildVendorBO = Me.State.MyBO.GetNewVendorQuantityChild(Me.State.MyChildBO.GetVendorQuantiy())
                'Me.State.MyChildVendorBO = New VendorQuantity(Me.State.MyChildBO.GetVendorQuantiy())
            Else
                Me.State.MyChildVendorBO = Me.State.MyBO.GetNewVendorQuantityChild()
                'Me.State.MyChildVendorBO = New VendorQuantity()
                Me.State.MyChildVendorBO.TableName = "PRICE_LIST_DETAIL"
                Me.State.MyChildVendorBO.ReferenceId = Me.State.MyChildBO.Id
            End If

            If (txtNewItemQuantity.Text <> "") Then
                Me.State.MyChildVendorBO.Quantity = CType(Me.txtNewItemQuantity.Text.Trim(), LongType)
            End If

            Me.State.MyChildVendorBO.Sku = Me.State.MyChildBO.VendorSku
            Me.State.MyChildVendorBO.SkuDescription = Me.State.MyChildBO.VendorSkuDescription
            Me.State.MyChildVendorBO.PriceListDetailID = Me.State.MyChildBO.Id

            Me.BindChildBoPropertiesToLabels()

            If Not Me.State.MyChildVendorBO.Quantity Is Nothing Then
                Me.State.MyChildVendorBO.Validate()
            End If

            If Me.State.MyChildBO.IsDirty Or Me.State.MyChildVendorBO.IsDirty Then
                ' check if already exists
                ' same equipment id, condition id, service_class id, service type id and sku
                If Me.State.MyChildBO.OverlapExists(True) Then
                    If Me.State.MyChildBO.ExpireOverLappingList() Then
                        Me.State.ChildOverlapExists = False
                    Else
                        Me.State.ChildOverlapExists = True
                    End If
                End If

                If IsNumeric(txtNewItemPrice.Text) Then
                    Me.State.MyChildBO.Price = CType(txtNewItemPrice.Text.Trim(), Decimal)
                Else
                    Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.GUI_ONLY_DIGITS_ALLOWED_FOR_PRICE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_ONLY_DIGITS_ALLOWED_FOR_PRICE)
                End If

                Me.State.MyChildBO.Validate()

                Try
                    If Not (Me.State.ChildOverlapExists) Then
                        If Me.State.MyChildBO.IsDirty Then
                            Me.State.MyChildBO.Save()
                            Me.State.MyChildBO.EndEdit()
                        End If

                        If Me.State.MyChildVendorBO.IsDirty Then
                            If Not Me.State.MyChildVendorBO.Quantity Is Nothing Then
                                Me.State.MyChildVendorBO.Save()
                                Me.State.MyChildVendorBO.EndEdit()
                            End If
                        End If
                        Me.State.HasDataChanged = False
                    End If

                Catch ex As Exception
                    Me.State.MyChildBO.RejectChanges()
                    Me.State.MyChildEquipmentBO.RejectChanges()
                    Me.State.MyChildVendorBO.RejectChanges()
                    Throw ex
                End Try

                Me.PopulateGrid()
                Me.EnableDisableFields(True)
                Me.ClearGridViewHeadersAndLabelsErrSign()

                'Me.MasterPage.MessageController.AddSuccess(ElitaPlus.ElitaPlusWebApp.Message.SAVE_RECORD_CONFIRMATION)
                'Else
                'Me.MasterPage.MessageController.AddError(ElitaPlus.ElitaPlusWebApp.Message.MSG_RECORD_NOT_SAVED)
            End If
            'to reset the boolen value
            Me.State.IsGridInEditMode = False
            'Me.State.MyChildBO.EndEdit()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
            Me.mdlPopup.Show()
        End Try
    End Sub


    Protected Sub BindChildBoPropertiesToLabels()
        Me.BindBOPropertyToLabel(Me.State.MyChildBO, "ServiceClassId", Me.lblNewItemServiceClass)
        Me.BindBOPropertyToLabel(Me.State.MyChildBO, "ConditionId", Me.lblNewItemCondition)
        Me.BindBOPropertyToLabel(Me.State.MyChildBO, "MakeId", Me.lblNewItemMake)
        Me.BindBOPropertyToLabel(Me.State.MyChildBO, "EquipmentId", Me.lblNewItemModel)
        Me.BindBOPropertyToLabel(Me.State.MyChildBO, "VendorSku", Me.lblNewItemVendorSKU)
        Me.BindBOPropertyToLabel(Me.State.MyChildBO, "VendorSkuDescription", Me.lblNewItemSKUDesciption)
        Me.BindBOPropertyToLabel(Me.State.MyChildBO, "Price", Me.lblNewItemPrice)
        Me.BindBOPropertyToLabel(Me.State.MyChildBO, "Effective", Me.lblNewItemEffectiveDate)
        Me.BindBOPropertyToLabel(Me.State.MyChildBO, "Expiration", Me.lblNewItemExpirationDate)
        Me.BindBOPropertyToLabel(Me.State.MyChildVendorBO, "Quantity", Me.lblNewItemQuantity)
        Me.BindBOPropertyToLabel(Me.State.MyChildBO, "PriceBandRangeFrom", Me.lblNewItemLowPrice)
        Me.BindBOPropertyToLabel(Me.State.MyChildBO, "PriceBandRangeTo", Me.lblNewItemHighPrice)
        Me.BindBOPropertyToLabel(Me.State.MyChildBO, "ReplacementTaxType", Me.lblNewItemSelectTaxType)
        Me.BindBOPropertyToLabel(Me.State.MyChildBO, "CurrencyId", Me.lblcurrency)
        Me.BindBOPropertyToLabel(Me.State.MyChildBO, "PartId", Me.lblNewItemPart)
        Me.BindBOPropertyToLabel(Me.State.MyChildBO, "ManufacturerOriginCode", Me.lblNewManOri)

    End Sub

#End Region

#Region "Controlling Logic"

    Private Function CheckOverlap() As Boolean
        Return Me.State.MyBO.Accept(New OverlapValidationVisitor)
    End Function

    Private Function CheckExistingFutureListOverlap() As Boolean
        Return Me.State.MyBO.Accept(New FutureOverlapValidationVisitor)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub BindBoPropertiesToLabels()
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Code", Me.lblCode)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Description", Me.lblDescription)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "CountryId", Me.lblCountry)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "DefaultCurrencyId", Me.lblDefaultCurrency)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ManageInventoryId", Me.lblManageInventory)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Effective", Me.lblEffectiveDate)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Expiration", Me.lblExpirationDate)
        '''''appending : as the above 3 are part of the childbo but childbo is not loaded on the page load.
        If Not lblEquipmentList.Text.EndsWith(":") Then
            Me.lblEquipmentList.Text = Me.lblEquipmentList.Text & ":"
        End If
        If Not lblServiceClass.Text.EndsWith(":") Then
            Me.lblServiceClass.Text = Me.lblServiceClass.Text & ":"
        End If
        If Not lblServiceType.Text.EndsWith(":") Then
            Me.lblServiceType.Text = Me.lblServiceType.Text & ":"
        End If
    End Sub

    Protected Sub BindBoPropertiesToModalPopupLabels()
        'Modal popup labels
        Me.BindBOPropertyToLabel(Me.State.MyChildBO, "ServiceClassId", Me.lblNewItemServiceClass)
        Me.BindBOPropertyToLabel(Me.State.MyChildBO, "Price", Me.lblNewItemPrice)
        Me.BindBOPropertyToLabel(Me.State.MyChildBO, "ServiceTypeId", Me.lblNewItemServiceType)
        Me.BindBOPropertyToLabel(Me.State.MyChildBO, "Quantity", Me.lblNewItemQuantity)
        Me.BindBOPropertyToLabel(Me.State.MyChildBO, "MakeId", Me.lblNewItemMake)
        Me.BindBOPropertyToLabel(Me.State.MyChildBO, "Effective", Me.lblNewItemEffectiveDate)
        Me.BindBOPropertyToLabel(Me.State.MyChildBO, "EquipmentId", Me.lblNewItemModel)
        Me.BindBOPropertyToLabel(Me.State.MyChildBO, "Expiration", Me.lblNewItemExpirationDate)
        Me.BindBOPropertyToLabel(Me.State.MyChildBO, "RiskTypeId", Me.lblRiskType)
        Me.BindBOPropertyToLabel(Me.State.MyChildBO, "EquipmentClassId", Me.lblEquipmentClass)
        Me.BindBOPropertyToLabel(Me.State.MyChildBO, "ConditionId", Me.lblNewItemCondition)
        Me.BindBOPropertyToLabel(Me.State.MyChildBO, "ServiceLevelId", Me.lblNewItemServiceLevel)
        Me.BindBOPropertyToLabel(Me.State.MyChildBO, "VendorSku", Me.lblNewItemVendorSKU)
        Me.BindBOPropertyToLabel(Me.State.MyChildBO, "VendorSkuDescription", Me.lblNewItemSKUDesciption)
        Me.BindBOPropertyToLabel(Me.State.MyChildBO, "PriceBandRangeFrom", Me.lblNewItemLowPrice)
        Me.BindBOPropertyToLabel(Me.State.MyChildBO, "PriceBandRangeTo", Me.lblNewItemHighPrice)
        Me.BindBOPropertyToLabel(Me.State.MyChildBO, "ReplacementTaxType", Me.lblNewItemSelectTaxType)
        Me.BindBOPropertyToLabel(Me.State.MyChildBO, "CurrencyId", Me.lblcurrency)
        Me.BindBOPropertyToLabel(Me.State.MyChildBO, "PriceListDetailTypeId", Me.lbldetailtype)
        Me.BindBOPropertyToLabel(Me.State.MyChildBO, "CalculationPercent", Me.lblcalculation)
        Me.BindBOPropertyToLabel(Me.State.MyChildBO, "ManufacturerOriginCode", Me.lblNewManOri)
        Me.BindBOPropertyToLabel(Me.State.MyChildBO, "PartId", Me.lblNewItemPart)
    End Sub

    ''' <summary>
    ''' Put back the current form values to BO
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub PopulateBOsFormFrom()
        With Me.State.MyBO
            Me.PopulateBOProperty(Me.State.MyBO, "Code", Me.txtCode.Text)
            Me.PopulateBOProperty(Me.State.MyBO, "Description", Me.txtDescription.Text)
            Me.PopulateBOProperty(Me.State.MyBO, "Effective", Me.txtEffective.Text)
            Me.PopulateBOProperty(Me.State.MyBO, "Expiration", Me.txtExpirationDate.Text)
            Me.PopulateBOProperty(Me.State.MyBO, "CountryId", Me.ddlCountry)
            Me.PopulateBOProperty(Me.State.MyBO, "DefaultCurrencyId", Me.ddlDefaultCurrency)
            Me.PopulateBOProperty(Me.State.MyBO, "ManageInventoryId", Me.ddlManageInventory, True)
        End With

        If Me.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub

    Protected Sub PopulateFormFromBOs()
        With Me.State.MyBO
            Me.PopulateControlFromBOProperty(txtCode, .Code)
            If txtCode.Text = "" Then
                ControlMgr.SetEnableControl(Me, txtCode, False)
            Else
                ControlMgr.SetEnableControl(Me, txtCode, True)
            End If

            Me.PopulateControlFromBOProperty(txtDescription, .Description)
            Me.PopulateControlFromBOProperty(txtEffective, .Effective)
            Me.PopulateControlFromBOProperty(txtExpirationDate, .Expiration)

            'Populate dropdowns
            If Me.State.IsNew Then
                'Set the country to the User's country
                If ddlCountry.Items.Count = 2 Then
                    'set the country as default selected
                    ddlCountry.SelectedIndex = 1
                    Me.State.MyBO.CountryId = New Guid(ddlCountry.SelectedValue.ToString())
                End If
            Else
                Me.PopulateControlFromBOProperty(ddlCountry, .CountryId)
            End If

            Me.PopulateControlFromBOProperty(ddlManageInventory, .ManageInventoryId)
            If (.DefaultCurrencyId.Equals(Guid.Empty)) Then
                Dim company As New ElitaPlus.BusinessObjectsNew.Company(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
                Dim currencyobj As New ElitaPlus.BusinessObjectsNew.Country(company.CountryId)
                Me.PopulateControlFromBOProperty(ddlDefaultCurrency, currencyobj.PrimaryCurrencyId)
            Else
                Me.PopulateControlFromBOProperty(ddlDefaultCurrency, .DefaultCurrencyId)
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
            Me.PopulateGrid()
            Me.PopulategvPendingApprovals()

            'if the rule list is expired then lock the form preventing change
            If Me.State.MyBO.Expiration.Value < DateTime.Now Then
                'disable everything
                EnableDisableFields(False)
            End If

            If Me.State.MyBO.ManageInventoryId <> Guid.Empty Then
                ddlManageInventory.Enabled = False
            Else
                ddlManageInventory.Enabled = True
            End If

        End With
    End Sub

    Private Sub PopulateSelectedVendors()
        If Not (Me.State.MyBO.Code = Nothing Or Me.State.MyBO.Code = String.Empty) Then
            'ElitaPlusPage.BindListControlToDataView(moSelectedList, Me.State.MyBO.GetServiceCenterSelectionView(), "description", "service_center_id", False)
            Dim ServiceCenterList As New Collections.Generic.List(Of DataElements.ListItem)
            For Each detail As ServiceCenter In Me.State.MyBO.ServiceCenterChildren
                Dim item As DataElements.ListItem = New DataElements.ListItem()
                item.Translation = detail.Description
                item.ListItemId = detail.Id
                item.Code = detail.PriceListCode
                ServiceCenterList.Add(item)
            Next
            Me.moSelectedList.Populate(ServiceCenterList.ToArray(), New PopulateOptions() With
                {
                    .AddBlankItem = False
                })
        End If
    End Sub

    Sub EnableDisableButtons(ByVal enableToggle As Boolean)
        ControlMgr.SetEnableControl(Me, btnClone, enableToggle)
        ControlMgr.SetEnableControl(Me, btnDelete, enableToggle)
        ControlMgr.SetEnableControl(Me, btnSave, enableToggle)
        ControlMgr.SetEnableControl(Me, btnUndo, enableToggle)
        ControlMgr.SetEnableControl(Me, btnBack, enableToggle)
        ControlMgr.SetEnableControl(Me, btnAdd, enableToggle)
    End Sub

    Protected Sub EnableDisableFields(ByVal toggle As Boolean)

        Me.EnableDisableButtons(toggle)

        ControlMgr.SetEnableControl(Me, btnDelete, True)
        ControlMgr.SetEnableControl(Me, btnAdd, True)
        ControlMgr.SetEnableControl(Me, btnClone, True)

        If Me.State.MyBO.IsNew Then
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

        If Me.State.IsEditMode Then
            ControlMgr.SetEnableControl(Me, btnUndo, True)
        Else
            ControlMgr.SetEnableControl(Me, btnUndo, False)
        End If

        'Effective date should be Editable for Price List where Effective Date >= Sysdate Defect 2735
        If Me.State.MyBO.Effective.Value >= DateTime.Now Then
            ControlMgr.SetEnableControl(Me, txtEffective, True)
            ControlMgr.SetEnableControl(Me, btneffective, True)
        End If

        If Me.State.MyBO.Expiration.Value < DateTime.Now Then
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
        Me.State.ScreenSnapShotBO = Nothing 'Reset the backup copy
        Me.State.MyBO = New PriceList
        Me.State.MyChildBO = New PriceListDetail
        Me.PopulateFormFromBOs()
        Me.State.IsEditMode = False
        Me.EnableDisableFields(True)
    End Sub

    Protected Sub CreateNewWithCopy()
        Me.State.IsACopy = True
        Dim newObj As New PriceList
        newObj.Copy(Me.State.MyBO)

        Me.State.MyBO = newObj
        Me.State.MyBO.Code = Me.State.MyBO.Code & "clone"
        Me.PopulateFormFromBOs()
        Me.PopulateControlFromBOProperty(txtCode, Me.State.MyBO.Code)

        Me.EnableDisableFields(True)
        'create the backup copy
        Me.State.ScreenSnapShotBO = New PriceList
        Me.State.ScreenSnapShotBO.Clone(Me.State.MyBO)
        Me.State.IsACopy = False
        Me.State.IsEditMode = False
        Me.EnableDisableFields(True)

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
        Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
        If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then

            If Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr AndAlso Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.Accept Then
                Me.BindBoPropertiesToLabels()
                Me.State.MyBO.Save()
            End If

            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    Me.MasterPage.MessageController.AddSuccess(ElitaPlus.ElitaPlusWebApp.Message.SAVE_RECORD_CONFIRMATION)
                    Me.CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    Me.MasterPage.MessageController.AddSuccess(ElitaPlus.ElitaPlusWebApp.Message.SAVE_RECORD_CONFIRMATION)
                    Me.CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    Me.ReturnToCallingPage(New ReturnType(Me.State.ActionInProgress, Me.State.MyBO, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.Accept
                    If Me.State.MyBO.IsDirty Then
                        If Me.State.OverlapExists Then
                            If Me.State.MyBO.ExpireOverLappingList() Then
                                Me.State.OverlapExists = False
                            End If
                        End If
                        Me.State.MyBO.Save()
                        Me.State.HasDataChanged = True
                        Me.PopulateFormFromBOs()
                        Me.MasterPage.MessageController.AddSuccess(ElitaPlus.ElitaPlusWebApp.Message.SAVE_RECORD_CONFIRMATION)
                    Else
                        Me.MasterPage.MessageController.AddError(ElitaPlus.ElitaPlusWebApp.Message.MSG_RECORD_NOT_SAVED)
                    End If
                Case ElitaPlusPage.DetailPageCommand.Delete
                    If Not Me.State.PriceListDetailSelectedChildId.Equals(Guid.Empty) Then
                        BeginPriceListDetailChildEdit()
                    End If

                    If Not Me.State.MyChildBO.GetVendorQuantiy().Equals(Guid.Empty) Then
                        Me.State.MyChildVendorBO = New VendorQuantity(Me.State.MyChildBO.GetVendorQuantiy())
                    End If

                    Try
                        If (Me.State.MyChildBO.Effective.Value <= PriceListDetail.GetCurrentDateTime()) Then
                            Me.State.MyChildBO.BeginEdit()
                            Me.State.MyChildBO.Accept(New ExpirationVisitor())
                            Me.State.MyChildBO.EndEdit()
                            Me.State.MyChildBO.Save()
                        ElseIf (Me.State.MyChildBO.Effective.Value > PriceListDetail.GetCurrentDateTime()) Then
                            If Not Me.State.MyChildBO.GetVendorQuantiy().Equals(Guid.Empty) Then
                                Me.State.MyChildVendorBO.Delete()
                                Me.State.MyChildVendorBO.Save()
                                Me.State.MyChildVendorBO.EndEdit()
                            End If
                            Me.State.MyChildBO.Delete()
                            Me.State.MyChildBO.Save()
                            Me.State.MyChildBO.EndEdit()
                            Me.State.PriceListDetailSelectedChildId = Guid.Empty
                        End If
                    Catch ex As Exception
                        Me.State.MyChildBO.RejectChanges()
                        Me.State.MyChildVendorBO.RejectChanges()
                        Me.MasterPage.MessageController.AddError(ElitaPlus.ElitaPlusWebApp.Message.ERR_DELETING_DATA)
                        Throw ex
                    End Try
                    PopulateGrid()
            End Select
        ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    Me.CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    Me.CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    Me.MasterPage.MessageController.AddErrorAndShow(Me.State.LastErrMsg)
                Case ElitaPlusPage.DetailPageCommand.Accept
                    Me.EnableDisableFields(True)
            End Select
        End If

        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        Me.HiddenSaveChangesPromptResponse.Value = ""
    End Sub

    Protected Sub PopulateDropdowns()
        'Dim languageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        'Dim companyGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
        'Me.BindListControlToDataView(ddlServiceClass, LookupListNew.GetServiceClassLookupList(languageId))
        Me.ddlServiceClass.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="SVCCLASS", languageCode:=Thread.CurrentPrincipal.GetLanguageCode()),
                        New PopulateOptions() With
                        {
                            .AddBlankItem = True
                        })
        'Me.BindListControlToDataView(ddlEquipmentlist, LookupListNew.GetEquipmentListLookupListforPriceList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, DateTime.Now), , , False)
        Dim oListContext1 As New ListContext
        oListContext1.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
        Dim EquipmentList As DataElements.ListItem() =
                                CommonConfigManager.Current.ListManager.GetList(listCode:="EquipmentByCompanyGroup", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=oListContext1)
        Me.ddlEquipmentlist.Populate(EquipmentList.ToArray(), New PopulateOptions() With
            {
                .AddBlankItem = False,
                .TextFunc = Function(x)
                                Return x.ExtendedCode + "_" + x.Translation
                            End Function,
                .SortFunc = Function(x)
                                Return x.ExtendedCode + "_" + x.Translation
                            End Function
            })

        Me.ddlEquipmentlist.Items.Insert(0, New ListItem(TranslationBase.TranslateLabelOrMessage(ANY), Guid.Empty.ToString()))
        'Country          
        'Me.BindListControlToDataView(Me.ddlCountry, LookupListNew.GetUserCountriesLookupList())
        Dim CountryList As DataElements.ListItem() =
                            CommonConfigManager.Current.ListManager.GetList(listCode:=ListCodes.Country)

        Dim UserCountries As DataElements.ListItem() = (From Country In CountryList
                                                        Where ElitaPlusIdentity.Current.ActiveUser.Countries.Contains(Country.ListItemId)
                                                        Select Country).ToArray()
        Me.ddlCountry.Populate(UserCountries.ToArray(),
                        New PopulateOptions() With
                        {
                            .AddBlankItem = True
                        })
        'Currency
        'Me.BindListControlToDataView(Me.ddlDefaultCurrency, LookupListNew.GetCurrencyTypeLookupList(), , , False)
        Dim CurrencyTypeList As DataElements.ListItem() =
                            CommonConfigManager.Current.ListManager.GetList(listCode:="CurrencyTypeList", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
        Me.ddlDefaultCurrency.Populate(CurrencyTypeList.ToArray(),
                        New PopulateOptions() With
                        {
                            .AddBlankItem = False
                        })

        ' Manage inventory
        'Me.BindListControlToDataView(Me.ddlManageInventory, LookupListNew.GetManageInventoryLookupList(languageId))
        Me.ddlManageInventory.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="MNGINVENT", languageCode:=Thread.CurrentPrincipal.GetLanguageCode()),
                        New PopulateOptions() With
                        {
                            .AddBlankItem = True
                        })
        Me.AddCalendarwithTime_New(btneffective, txtEffective, , txtEffective.Text)
        Me.AddCalendarwithTime_New(btnExpiration, txtExpirationDate, , txtExpirationDate.Text)
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
        Me.ddlNewItemServiceClass.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="SVCCLASS", languageCode:=Thread.CurrentPrincipal.GetLanguageCode()),
                        New PopulateOptions() With
                        {
                            .AddBlankItem = True
                        })
        If ddlNewItemServiceType.Items.Count > 0 Then
            ddlNewItemServiceType.Items.Clear()
        End If

        'Me.BindListControlToDataView(ddlNewItemServiceLevel, LookupListNew.GetServiceLevelLookupList(languageId))
        Me.ddlNewItemServiceLevel.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="SVC_LVL", languageCode:=Thread.CurrentPrincipal.GetLanguageCode()),
                        New PopulateOptions() With
                        {
                            .AddBlankItem = True
                        })

        'Me.BindListControlToDataView(ddlNewItemMake, LookupListNew.GetManufacturerLookupList(companyGroupId))
        Dim oListContext2 As New ListContext
        oListContext2.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
        Dim MakeList As DataElements.ListItem() =
                                CommonConfigManager.Current.ListManager.GetList(listCode:="ManufacturerByCompanyGroup", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=oListContext2)
        Me.ddlNewItemMake.Populate(MakeList.ToArray(), New PopulateOptions() With
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

        Me.ddlNewItemReplacementTaxType.Populate(FilteredTaxTypeList.ToArray(), New PopulateOptions() With
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
            Me.ddlNewItemModel.Populate(ModelList, New PopulateOptions() With
                {
                    .AddBlankItem = True
                })
        End If
        'Me.BindListControlToDataView(ddlNewItemCondition, LookupListNew.GetConditionLookupList(languageId))
        Me.ddlNewItemCondition.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="TEQP", languageCode:=Thread.CurrentPrincipal.GetLanguageCode()),
                        New PopulateOptions() With
                        {
                            .AddBlankItem = True
                        })
        Me.ddlNewItemCondition.Enabled = False

        'Risk Type and Equipment Class
        'Me.BindListControlToDataView(ddlRiskType, LookupListNew.GetRiskTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id), , , False)
        Dim oListContext As New ListContext
        oListContext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
        Dim RiskTypeList As DataElements.ListItem() =
                                    CommonConfigManager.Current.ListManager.GetList(listCode:="RiskTypeByCompanyGroup", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=oListContext)
        Me.ddlRiskType.Populate(RiskTypeList, New PopulateOptions() With
            {
                .AddBlankItem = True
            })
        'ddlRiskType.Items.Insert(0, New ListItem(TranslationBase.TranslateLabelOrMessage(ANY), Guid.Empty.ToString()))
        'Me.BindListControlToDataView(ddlEquipmentClass, LookupListNew.GetEquipmentClassLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId))
        Me.ddlEquipmentClass.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="EQPCLS", languageCode:=Thread.CurrentPrincipal.GetLanguageCode()),
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
        Me.ddldetailtype.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="PLDTYP", languageCode:=Thread.CurrentPrincipal.GetLanguageCode()),
                        New PopulateOptions() With
                        {
                            .AddBlankItem = True
                        })

        If ddlNewItemPart.Items.Count > 0 Then
            ddlNewItemPart.Items.Clear()
            ddlNewItemPart.Enabled = False
        End If

        Me.ddlNewManOri.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="MAN_ORI", languageCode:=Thread.CurrentPrincipal.GetLanguageCode()),
                New PopulateOptions() With
                {
                    .AddBlankItem = True,
                    .BlankItemValue = Nothing,
                    .TextFunc = AddressOf PopulateOptions.GetDescription,
                    .ValueFunc = AddressOf PopulateOptions.GetExtendedCode
                })
        ddlNewManOri.Enabled = False

        Me.AddCalendarwithTime_New(imgNewItemEffectiveDate, txtNewItemEffectiveDate, , txtNewItemEffectiveDate.Text)
        Me.AddCalendarwithTime_New(imgNewItemExpirationDate, txtNewItemExpirationDate, , txtNewItemExpirationDate.Text)
        Me.txtNewItemEffectiveDate.Text = String.Empty
        Me.txtNewItemExpirationDate.Text = String.Empty
        Me.txtNewItemPrice.Text = String.Empty
        Me.txtNewItemQuantity.Text = String.Empty
        Me.txtNewItemVendorSKU.Text = String.Empty
        Me.txtNewItemSKUDescription.Text = String.Empty
        Me.txtNewItemLowPrice.Text = String.Empty
        Me.txtNewItemHighPrice.Text = String.Empty
    End Sub

    Protected Sub ddlManageInventory_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlManageInventory.SelectedIndexChanged
        Try
            PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
    Protected Sub ddlNewItemMake_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlNewItemMake.SelectedIndexChanged
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
                Me.ddlNewItemModel.Populate(ModelList, New PopulateOptions() With
                    {
                        .AddBlankItem = True
                    })


            End If

            mdlPopup.Show()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub ddlRiskType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlRiskType.SelectedIndexChanged
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
                Me.ddlNewItemPart.Populate(PartList.ToArray(), New PopulateOptions() With
                    {
                        .AddBlankItem = True
                    })
            Else
                ddlNewManOri.Enabled = False
            End If

            mdlPopup.Show()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub ddlServiceClass_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlServiceClass.SelectedIndexChanged
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
                Me.ddlServiceType.Populate(ServiceTypeList, New PopulateOptions() With
                    {
                        .AddBlankItem = True
                    })
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
    Protected Sub ddlNewItemServiceClass_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlNewItemServiceClass.SelectedIndexChanged
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
                Me.ddlNewItemServiceType.Populate(ServiceTypeList, New PopulateOptions() With
                    {
                        .AddBlankItem = True
                    })
                ddlNewItemServiceType.Enabled = True
            End If
            mdlPopup.Show()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
            Dim dv As PriceList.PriceListDetailSelectionView = Me.State.MyBO.GetPriceListSelectionView
            dv.Sort = Me.State.SortExpression
            Me.State.DetailSearchDV = dv
            Me.Grid.AutoGenerateColumns = False

            Me.Grid.PageSize = Me.State.PageSize
            SetPageAndSelectedIndexFromGuid(dv, Me.State.PriceListDetailSelectedChildId, Me.Grid, Me.State.PageIndex)

            If Not txtSearch.Text = "" Then 'requested_by
                dv.RowFilter = "requested_by like '%" & txtSearch.Text & "%'"
                Me.Grid.DataSource = dv
            Else
                Me.Grid.DataSource = dv 'Me.State.DetailSearchDV
            End If
            Me.Grid.DataBind()
                Me.State.PageIndex = Me.Grid.PageIndex
                Me.ShowHideQuantity()

                ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)
                ControlMgr.SetVisibleControl(Me, cboPageSize, Me.Grid.Visible)
            ControlMgr.SetVisibleControl(Me, lblRecordCounts, True)
            ControlMgr.SetVisibleControl(Me, lblPageSize, True)

            Session("recCount") = dv.Count 'Me.State.DetailSearchDV.Count
            Me.lblRecordCounts.Text = dv.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)

            If dv.Count = 0 Then
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                'ControlMgr.SetVisibleControl(Me, cboPageSize, False)
                ControlMgr.SetVisibleControl(Me, btnSubmitforApproval, False)
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
            Me.HighLightSortColumn(Grid, Me.State.SortExpression, True)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub Grid_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.PageIndexChanged
        Try
            Me.State.PageIndex = Grid.PageIndex
            Me.State.PriceListId = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Grid.PageIndex = e.NewPageIndex
            State.PageIndex = Grid.PageIndex
            State.PriceListDetailSelectedChildId = Guid.Empty
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub Grid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
        Try
            Dim priceListDetailId As Guid
            Dim oDataView As PriceListDetail.PriceListDetailSearchDV

            'Populate the grid with detail info
            If e.CommandName = ElitaPlusSearchPage.EDIT_COMMAND_NAME Then
                Me.State.IsGridInEditMode = True
                Me.State.SelectedGridValueToEdit = New Guid(e.CommandArgument.ToString())
                Me.State.ChildActionInProgress = DetailPageCommand.NewAndCopy

                'priceListDetailId = New Guid(e.CommandArgument.ToString())
                Me.State.PriceListDetailSelectedChildId = New Guid(e.CommandArgument.ToString())
                ' Me.State.MyChildBO = New PriceListDetail(Me.State.PriceListDetailSelectedChildId)
                Me.PopulateModalControls()
                BeginPriceListDetailChildEdit()
                Me.PopulateDetailFromPriceListDetailChildBO()

                If Not Me.State.MyChildBO.GetVendorQuantiy().Equals(Guid.Empty) Then
                    Me.State.MyChildVendorBO = New VendorQuantity(Me.State.MyChildBO.GetVendorQuantiy())
                End If

                '' condition
                'Me.PopulateControlFromBOProperty(ddlNewItemCondition, Me.State.MyChildBO.ConditionId)
                ' vendor sku
                txtNewItemVendorSKU.Text = Me.State.MyChildBO.VendorSku
                ' description
                txtNewItemSKUDescription.Text = Me.State.MyChildBO.VendorSkuDescription
                ' price
                txtNewItemPrice.Text = Me.State.MyChildBO.Price.ToString()
                'Calculation Percentage
                If Not Me.State.MyChildBO.CalculationPercent Is Nothing Then
                    txtcalculationpercent.Text = Me.State.MyChildBO.CalculationPercent.ToString()
                Else
                    txtcalculationpercent.Text = 0.0
                End If
                ' effective date
                txtNewItemEffectiveDate.Text = Me.State.MyChildBO.Effective.ToString()
                ' expiration date
                txtNewItemExpirationDate.Text = Me.State.MyChildBO.Expiration.ToString()
                'Low Price
                txtNewItemLowPrice.Text = Me.State.MyChildBO.PriceBandRangeFrom.ToString()
                'High Price
                txtNewItemHighPrice.Text = Me.State.MyChildBO.PriceBandRangeTo.ToString()
                'vendor quantity

                If Not Me.State.MyChildBO.GetVendorQuantiy().Equals(Guid.Empty) AndAlso Not Me.State.MyChildVendorBO Is Nothing AndAlso Not Me.State.MyChildVendorBO.Quantity Is Nothing Then
                    txtNewItemQuantity.Text = Me.State.MyChildVendorBO.Quantity.ToString()
                Else
                    txtNewItemQuantity.Text = String.Empty
                End If


                ' BeginPriceListDetailChildEdit()
                ModalPopupenabledisable()
                mdlPopup.Show()
            ElseIf e.CommandName = ElitaPlusSearchPage.DELETE_COMMAND_NAME Then
                Me.State.IsGridInEditMode = False
                priceListDetailId = New Guid(e.CommandArgument.ToString())
                Me.State.PriceListDetailSelectedChildId = New Guid(e.CommandArgument.ToString())
                Me.DisplayMessage(Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete

                ' Me.MasterPage.MessageController.AddSuccess(ElitaPlus.ElitaPlusWebApp.Message.DELETE_RECORD_CONFIRMATION)
            ElseIf e.CommandName = ElitaPlusSearchPage.HISTORY_COMMAND_NAME Then
                Me.State.IsGridInEditMode = False
                priceListDetailId = New Guid(e.CommandArgument.ToString())
                Me.State.PriceListDetailSelectedChildId = New Guid(e.CommandArgument.ToString())
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.ViewHistory
                Me.PopulateGridHistory()
                mpeHistory.Show()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
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

    Private Sub Grid_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
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
            lblStatusXCD = CType(e.Row.Cells(Me.GRID_COL_STATUS_XCD_IDX).FindControl("lblStatusXCD"), Label)

            If (Not e.Row.Cells(Me.GRID_COL_EDITID_IDX).FindControl(BTN_CONTROL_EDIT_DETAIL_LIST) Is Nothing) Then
                'Edit Button argument changed to id
                btnEditItem = CType(e.Row.Cells(Me.GRID_COL_EDITID_IDX).FindControl(BTN_CONTROL_EDIT_DETAIL_LIST), ImageButton)
                btnEditItem.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(PriceListDetail.PriceListDetailSearchDV.COL_PRICE_LIST_DETAIL_ID), Byte()))
                btnEditItem.CommandName = ElitaPlusSearchPage.EDIT_COMMAND_NAME

                If Not (e.Row.Cells(Me.GRID_COL_EXPIRATION_DATEID_IDX).Text.ToString().Equals(String.Empty)) Then
                    If (DateHelper.GetDateValue(e.Row.Cells(Me.GRID_COL_EXPIRATION_DATEID_IDX).Text.ToString()) < DateTime.Now And lblStatusXCD.Text = "PL_RECON_PROCESS-APPROVED") Then
                        'e.Row.Cells(Me.GRID_COL_EDITID_IDX).Visible = False
                        btnEditItem.Visible = False
                    End If
                End If
                If lblStatusXCD.Text = "PL_RECON_PROCESS-PENDINGAPPROVAL" Then
                    btnEditItem.Visible = False
                End If
            End If

            If (Not e.Row.Cells(Me.GRID_COL_DELETEID_IDX).FindControl(BTN_CONTROL_DELETE_DETAIL_LIST) Is Nothing) Then
                'Delete Button argument changed to id
                btnDeleteItem = CType(e.Row.Cells(Me.GRID_COL_DELETEID_IDX).FindControl(BTN_CONTROL_DELETE_DETAIL_LIST), ImageButton)
                btnDeleteItem.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(PriceListDetail.PriceListDetailSearchDV.COL_PRICE_LIST_DETAIL_ID), Byte()))
                btnDeleteItem.CommandName = ElitaPlusSearchPage.DELETE_COMMAND_NAME
                'Me.AddControlMsg(btnDeleteItem, Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, True)

                If Not (e.Row.Cells(Me.GRID_COL_EXPIRATION_DATEID_IDX).Text.ToString().Equals(String.Empty)) Then
                    If (DateHelper.GetDateValue(e.Row.Cells(Me.GRID_COL_EXPIRATION_DATEID_IDX).Text.ToString()) < DateTime.Now And lblStatusXCD.Text = "PL_RECON_PROCESS-APPROVED") Then
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
    Private Sub cboPageSize_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Me.State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            Me.State.SelectedPageSize = Me.State.PageSize
            Me.State.PageIndex = NewCurrentPageIndex(Grid, State.DetailSearchDV.Count, State.PageSize)
            Me.Grid.PageIndex = Me.State.PageIndex
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Detail GRid for Approval"


    ' <summary>
    ' Populate the main detail lits grid
    ' </summary>
    ' <remarks></remarks>
    ' 
    Sub PopulategvPendingApprovals()
        Try
            Dim dv As PriceList.PriceListDetailSelectionView = Me.State.MyBO.GetPriceListSelectionView
            dv.Sort = Me.State.SortExpression
            Me.State.DetailSearchDV = dv
            Me.gvPendingApprovals.AutoGenerateColumns = False

            Me.gvPendingApprovals.PageSize = Me.State.PageSize
            SetPageAndSelectedIndexFromGuid(dv, Me.State.PriceListDetailSelectedChildId, Me.gvPendingApprovals, Me.State.PageIndex)

            dv.RowFilter = "status_xcd='PL_RECON_PROCESS-PENDINGAPPROVAL'"
            If Not txtpaSearch.Text = "" Then
                dv.RowFilter = "requested_by like '%" & txtpaSearch.Text & "%'"
                Me.gvPendingApprovals.DataSource = dv
            Else
                Me.gvPendingApprovals.DataSource = dv
            End If
            Me.gvPendingApprovals.DataSource = dv
            Me.gvPendingApprovals.DataBind()
            Me.State.PageIndex = Me.gvPendingApprovals.PageIndex
            Me.ShowHideQuantity()

            ControlMgr.SetVisibleControl(Me, trPageSizePendingApprovals, Me.gvPendingApprovals.Visible)
            ControlMgr.SetVisibleControl(Me, cboPageSizePendingApproval, Me.gvPendingApprovals.Visible)
            ControlMgr.SetVisibleControl(Me, lblPendingApprovalRecordCounts, True)
            ControlMgr.SetVisibleControl(Me, btnApprove, True)
            ControlMgr.SetVisibleControl(Me, btnReject, True)
            Session("recCount") = dv.Count
            Me.lblPendingApprovalRecordCounts.Text = dv.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)

            If dv.Count = 0 Then
                ControlMgr.SetVisibleControl(Me, trPageSizePendingApprovals, False)
                'ControlMgr.SetVisibleControl(Me, cboPageSizePendingApproval, False)
                ControlMgr.SetVisibleControl(Me, btnApprove, False)
                ControlMgr.SetVisibleControl(Me, btnReject, False)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub gvPendingApprovals_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvPendingApprovals.Sorting
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
            Me.PopulategvPendingApprovals()
            Me.HighLightSortColumn(gvPendingApprovals, Me.State.SortExpression, True)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub gvPendingApprovals_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvPendingApprovals.PageIndexChanged
        Try
            Me.State.PageIndex = gvPendingApprovals.PageIndex
            Me.State.PriceListId = Guid.Empty
            PopulategvPendingApprovals()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub gvPendingApprovals_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvPendingApprovals.PageIndexChanging
        Try
            gvPendingApprovals.PageIndex = e.NewPageIndex
            State.PageIndex = gvPendingApprovals.PageIndex
            State.PriceListDetailSelectedChildId = Guid.Empty
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub gvPendingApprovals_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvPendingApprovals.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub cboPageSizePendingApproval_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPageSizePendingApproval.SelectedIndexChanged
        Try
            Me.State.PageSize = CType(cboPageSizePendingApproval.SelectedValue, Integer)
            Me.State.SelectedPageSize = Me.State.PageSize
            Me.State.PageIndex = NewCurrentPageIndex(gvPendingApprovals, State.DetailSearchDV.Count, State.PageSize)
            Me.gvPendingApprovals.PageIndex = Me.State.PageIndex
            Me.PopulategvPendingApprovals()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
            Dim ds As DataSet = Me.State.MyBO.ViewPriceListDetailHistory(Me.State.PriceListDetailSelectedChildId, Authentication.CurrentUser.LanguageId)
            Me.gvHistory.AutoGenerateColumns = False
            Me.gvHistory.PageSize = Me.State.PageSize
            Me.gvHistory.DataSource = ds 'Me.State.DetailSearchDV
            Me.gvHistory.DataBind()
            Me.State.PageIndex = Me.gvHistory.PageIndex
            Me.ShowHideQuantity()

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub gvHistory_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles gvHistory.Sorting
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
            Me.PopulateGridHistory()
            Me.HighLightSortColumn(Grid, Me.State.SortExpression, True)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub gvHistory_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles gvHistory.PageIndexChanged
        Try
            Me.State.PageIndex = gvHistory.PageIndex
            Me.State.PriceListId = Guid.Empty
            PopulateGridHistory()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)

        End Try
    End Sub

    Private Sub gvHistory_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvHistory.PageIndexChanging
        Try
            gvHistory.PageIndex = e.NewPageIndex
            State.PageIndex = gvHistory.PageIndex
            State.PriceListDetailSelectedChildId = Guid.Empty
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub gvHistory_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvHistory.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region
    Sub PopulateDetailFromPriceListDetailChildBO()
        ' service class
        Me.PopulateControlFromBOProperty(ddlNewItemServiceClass, Me.State.MyChildBO.ServiceClassId)

        If (ddlNewItemServiceClass.SelectedIndex <> NO_ITEM_SELECTED_INDEX) Then
            'Dim dsServiceTypes As DataSet
            'Dim dv As DataView = SpecialService.getServiceTypesForServiceClass(ElitaPlusPage.GetSelectedItem(ddlNewItemServiceClass), ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0).DefaultView
            'ElitaPlusPage.BindListControlToDataView(ddlNewItemServiceType, dv, msServiceClassColumnName, , True)
            Dim oListContext As New ListContext
            oListContext.ServiceClassId = ElitaPlusPage.GetSelectedItem(ddlNewItemServiceClass)
            oListContext.LanguageId = Thread.CurrentPrincipal.GetLanguageId()
            Dim ServiceTypeList As DataElements.ListItem() =
                                    CommonConfigManager.Current.ListManager.GetList(listCode:="ServiceTypeByServiceClass", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=oListContext)
            Me.ddlNewItemServiceType.Populate(ServiceTypeList, New PopulateOptions() With
                    {
                        .AddBlankItem = True
                    })
            ddlNewItemServiceType.Enabled = True
        End If

        ' service type
        If Not (Me.State.MyChildBO.ServiceTypeId = Guid.Empty) Then
            Me.PopulateControlFromBOProperty(ddlNewItemServiceType, Me.State.MyChildBO.ServiceTypeId)
        End If

        'select Make
        If Not (ddlNewItemMake.Items.FindByValue(Me.State.MyChildBO.MakeId.ToString())) Is Nothing Then
            Me.PopulateControlFromBOProperty(ddlNewItemMake, Me.State.MyChildBO.MakeId)
        End If

        'make
        'US 255424 - Using Parent Equipment ID if it is coming on dataset
        If (Me.State.MyChildBO.EquipmentId <> Guid.Empty OrElse Me.State.MyChildBO.Parent_EquipmentId <> Guid.Empty) Then
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
                Me.ddlNewItemModel.Populate(EquipmentList, New PopulateOptions() With
                    {
                        .AddBlankItem = True
                    })

                'US 255424 - Using Parent Equipment ID if it is coming on dataset
                Dim eqmnt_Id As Guid = If(Me.State.MyChildBO.Parent_EquipmentId = Guid.Empty, Me.State.MyChildBO.EquipmentId, Me.State.MyChildBO.Parent_EquipmentId)
                If Not (ddlNewItemModel.Items.FindByValue(eqmnt_Id.ToString())) Is Nothing Then
                    Me.PopulateControlFromBOProperty(ddlNewItemModel, eqmnt_Id)
                End If
            End If
        End If

        If Me.State.MyChildBO.CurrencyId.Equals(Guid.Empty) Then
            Dim company As New Company(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
            Dim currencyobj As New Country(company.CountryId)
            Me.SetSelectedItem(ddlcurrency, currencyobj.PrimaryCurrencyId)
        Else
            Me.PopulateControlFromBOProperty(ddlcurrency, Me.State.MyChildBO.CurrencyId)
        End If

        Me.PopulateControlFromBOProperty(ddldetailtype, Me.State.MyChildBO.PriceListDetailTypeId)
        'Dim dv1 As DataView = LookupListNew.DropdownLookupList(LookupListCache.LK_PRICE_LIST_DETAIL_TYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
        Dim PriceListDetailTypeList As DataElements.ListItem() =
                                    CommonConfigManager.Current.ListManager.GetList(listCode:="PLDTYP", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
        Dim templateId As Guid = (From lst In PriceListDetailTypeList
                                  Where lst.Code = LookupListCache.LK_TEMPLATE_CODE
                                  Select lst.ListItemId).FirstOrDefault()

        If ddldetailtype.SelectedValue.ToString() = templateId.ToString() Then
            Me.txtcalculationpercent.Visible = True
            Me.lblcalculation.Visible = True
            Me.ddlcurrency.Visible = False
            Me.lblNewItemPrice.Visible = False
            Me.txtNewItemPrice.Visible = False
            Me.lblcurrency.Visible = False
            Me.ddldetailtype.Enabled = False
        Else
            Me.txtcalculationpercent.Visible = False
            Me.lblcalculation.Visible = False
            Me.ddlcurrency.Visible = True
            Me.lblNewItemPrice.Visible = True
            Me.txtNewItemPrice.Visible = True
            Me.lblcurrency.Visible = True
            Me.ddldetailtype.Enabled = False
        End If

        Me.PopulateControlFromBOProperty(ddlNewItemServiceLevel, Me.State.MyChildBO.ServiceLevelId)

        'US 255424 - Using Parent Condition ID if it's coming on dataset
        Dim condition_id As Guid = If(Me.State.MyChildBO.Parent_ConditionId = Guid.Empty, Me.State.MyChildBO.ConditionId, Me.State.MyChildBO.Parent_ConditionId)
        Me.PopulateControlFromBOProperty(ddlNewItemCondition, condition_id)

        EnableDisableControls(ddlNewItemCondition, (Me.State.MyChildBO.EquipmentId = Guid.Empty))

        Me.PopulateControlFromBOProperty(ddlRiskType, Me.State.MyChildBO.RiskTypeId)
        Me.PopulateControlFromBOProperty(ddlEquipmentClass, Me.State.MyChildBO.EquipmentClassId)
        Me.PopulateControlFromBOProperty(ddlNewItemReplacementTaxType, Me.State.MyChildBO.ReplacementTaxType)

        If (ddlRiskType.SelectedIndex <> NO_ITEM_SELECTED_INDEX AndAlso ddlRiskType.SelectedIndex <> BLANK_ITEM_SELECTED) Then

            Dim oListContext As New ListContext
            oListContext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            oListContext.RiskGroupId = Nothing

            Dim RiskTypeId As Guid = ElitaPlusPage.GetSelectedItem(ddlRiskType)
            Dim oRisktType As RiskType = New RiskType(RiskTypeId)

            oListContext.RiskGroupId = oRisktType.RiskGroupId

            Dim PartList As DataElements.ListItem() =
                                CommonConfigManager.Current.ListManager.GetList(listCode:="PartsByCompanyGroupRiskGroup", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=oListContext)
            Me.ddlNewItemPart.Populate(PartList.ToArray(), New PopulateOptions() With
            {
                .AddBlankItem = True
            })
            ddlNewItemPart.Enabled = True
        End If

        ' part 
        If Not (Me.State.MyChildBO.PartId.Equals(Guid.Empty)) Then

            Me.ddlNewManOri.Populate(CommonConfigManager.Current.ListManager.GetList(listCode:="MAN_ORI", languageCode:=Thread.CurrentPrincipal.GetLanguageCode()),
                New PopulateOptions() With
                {
                    .AddBlankItem = True,
                    .BlankItemValue = Nothing,
                    .TextFunc = AddressOf PopulateOptions.GetDescription,
                    .ValueFunc = AddressOf PopulateOptions.GetExtendedCode
                })

            If (ddlNewItemPart.Items.FindByValue(Me.State.MyChildBO.PartId.ToString()) IsNot Nothing) Then
                Me.PopulateControlFromBOProperty(ddlNewItemPart, Me.State.MyChildBO.PartId)

                ddlNewManOri.Enabled = True
            End If
        End If

        If Not (State.MyChildBO.ManufacturerOriginCode?.ToString() Is Nothing AndAlso ddlNewManOri.Items IsNot Nothing AndAlso ddlNewManOri.Items.Count > 0) Then
            'Fix for Bug 257627 - Cannot use Me.PopulateControlFromBOProperty because it's expecting Me.State.MyChildBO.ManufacturerOriginCode as GUID but it's a string
            Dim selctedItem As ListItem = ddlNewManOri.Items.FindByValue(Me.State.MyChildBO.ManufacturerOriginCode)
            If (selctedItem IsNot Nothing) Then
                selctedItem.Selected = True
            Else
                ddlNewManOri.SelectedIndex = BLANK_ITEM_SELECTED
            End If
        End If

    End Sub

    Private Sub ddlNewItemModel_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlNewItemModel.SelectedIndexChanged
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
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub ddlNewItemPart_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlNewItemPart.SelectedIndexChanged
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
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub ddlCountry_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCountry.SelectedIndexChanged
        Dim currencyid As Guid = Guid.Parse(GetCurrencyId(1))
        Me.SetSelectedItem(ddlDefaultCurrency, currencyid)
    End Sub

    Private Function GetCurrencyId(ByVal input As Boolean) As String
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
            Me.State.MyBO.ProcessPriceListByStatus(Me.State.MyBO.Id, String.Empty, Authentication.CurrentUser.NetworkId, "PL_RECON_PROCESS-PENDINGAPPROVAL")
            Me.PopulateGrid()
            Me.PopulategvPendingApprovals()
            Me.MasterPage.MessageController.AddSuccess(ElitaPlus.ElitaPlusWebApp.Message.PRICE_LIST_SUBMISSION_PROCESS_SUCCESS)
        Catch ex As Exception
            Me.MasterPage.MessageController.AddError(ElitaPlus.ElitaPlusWebApp.Message.PRICE_LIST_SUBMISSION_PROCESS_FAILED)
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnApprove_Click(sender As Object, e As EventArgs) Handles btnApprove.Click
        Try
            Dim isChecked As Boolean = False
            Dim PricelistDetailIdList As String = String.Empty
            Dim HeaderCheckbox As CheckBox = CType(gvPendingApprovals.HeaderRow.FindControl("chkHeaderApproveOrReject"), CheckBox)
            If HeaderCheckbox.Checked = False Then
                Dim lstPriceListDetail As New Collections.Generic.List(Of String)
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
            If isChecked Then
                Me.State.MyBO.ProcessPriceListByStatus(Me.State.MyBO.Id, PricelistDetailIdList, Authentication.CurrentUser.NetworkId, "PL_RECON_PROCESS-APPROVED")
                Me.PopulateGrid()
                Me.PopulategvPendingApprovals()
                Me.MasterPage.MessageController.AddSuccess(ElitaPlus.ElitaPlusWebApp.Message.PRICE_LIST_APPROVAL_PROCESS_SUCCESS)
            Else
                Me.MasterPage.MessageController.AddError(ElitaPlus.ElitaPlusWebApp.Message.PRICE_LIST_APPROVE_OR_REJECT_CHECKBOX_NOT_SELECTED)
            End If
        Catch ex As Exception
            Me.MasterPage.MessageController.AddError(ElitaPlus.ElitaPlusWebApp.Message.PRICE_LIST_APPROVAL_PROCESS_FAILED)
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnReject_Click(sender As Object, e As EventArgs) Handles btnReject.Click
        Try
            Dim isChecked As Boolean = False
            Dim PricelistDetailIdList As String = String.Empty
            Dim HeaderCheckbox As CheckBox = CType(gvPendingApprovals.HeaderRow.FindControl("chkHeaderApproveOrReject"), CheckBox)
            If HeaderCheckbox.Checked = False Then
                Dim lstPriceListDetail As New Collections.Generic.List(Of String)
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
            If isChecked Then
                Me.State.MyBO.ProcessPriceListByStatus(Me.State.MyBO.Id, PricelistDetailIdList, Authentication.CurrentUser.NetworkId, "PL_RECON_PROCESS-REJECTED")
                Me.PopulateGrid()
                Me.PopulategvPendingApprovals()
                Me.MasterPage.MessageController.AddSuccess(ElitaPlus.ElitaPlusWebApp.Message.PRICE_LIST_REJECTION_PROCESS_SUCCESS)
            Else
                Me.MasterPage.MessageController.AddError(ElitaPlus.ElitaPlusWebApp.Message.PRICE_LIST_APPROVE_OR_REJECT_CHECKBOX_NOT_SELECTED)
            End If
        Catch ex As Exception
            Me.MasterPage.MessageController.AddError(ElitaPlus.ElitaPlusWebApp.Message.PRICE_LIST_REJECTION_PROCESS_FAILED)
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btntxtsearch_Click(sender As Object, e As EventArgs) Handles btntxtsearch.Click
        Me.PopulateGrid()
    End Sub

    Protected Sub btnpaSearch_Click(sender As Object, e As EventArgs) Handles btnpaSearch.Click
        Me.PopulategvPendingApprovals()
    End Sub
End Class
