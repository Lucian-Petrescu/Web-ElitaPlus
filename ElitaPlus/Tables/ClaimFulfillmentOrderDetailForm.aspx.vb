Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.Security

Namespace Tables
    Partial Class ClaimFulfillmentOrderDetailForm
        Inherits ElitaPlusSearchPage

#Region "Page State"

#Region "MyState"

        Class MyState

            Public moCFOrderDetailId As Guid = Guid.Empty
            Public IsCFOrderDetailNew As Boolean = False
            Public IsNewWithCopy As Boolean = False
            Public IsUndo As Boolean = False
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public LastOperation As DetailPageCommand = DetailPageCommand.Nothing_
            Public boChanged As Boolean = False
            Public LastErrMsg As String
            Public HasDataChanged As Boolean
            Public moClaimFulfillmentOrderDetail As ClaimFulfillmentOrderDetail
            Public ScreenSnapShotBO As ClaimFulfillmentOrderDetail

        End Class
#End Region

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property

        Private Sub SetStateProperties()
            State.moCFOrderDetailId = CType(CallingParameters, Guid)
            If State.moCFOrderDetailId.Equals(Guid.Empty) Then
                State.IsCFOrderDetailNew = True
                BindBoPropertiesToLabels()
                AddLabelDecorations(TheClaimFulfillmentOrderDetail)
                ClearAll()
                PopulateAll()
            Else
                State.IsCFOrderDetailNew = False
                BindBoPropertiesToLabels()
                AddLabelDecorations(TheClaimFulfillmentOrderDetail)
                PopulateAll()
            End If
        End Sub

#End Region

#Region "Constants"

        Private Const CF_ORDER_DETAIL_FORM001 As String = "CF_ORDER_DETAIL_FORM001" ' Billing Cycle List Exception
        Private Const CF_ORDER_DETAIL_FORM002 As String = "CF_ORDER_DETAIL_FORM002" ' Billing Cycle Code Field Exception
        Private Const CF_ORDER_DETAIL_FORM003 As String = "CF_ORDER_DETAIL_FORM003" ' Billing Cycle Update Exception
        Private Const CONFIRM_MSG As String = "MGS_CONFIRM_PROMPT" '"Are you sure you want to delete the selected records?"
        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK" '"The record has been successfully saved"
        Private Const MSG_UNIQUE_VIOLATION As String = "MSG_DUPLICATE_KEY_CONSTRAINT_VIOLATED" '"Unique value is in use"
        Private Const UNIQUE_VIOLATION As String = "unique constraint"
        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"
        Private Const NO_ROW_SELECTED_INDEX As Integer = -1
        Private Const EDIT_COMMAND As String = "EditRecord"
        Private Const CANCEL_COMMAND As String = "CancelRecord"
        Private Const SAVE_COMMAND As String = "SaveRecord"
        Private Const DELETE_COMMAND As String = "DeleteRecord"
        Private Const SORT_COMMAND As String = "Sort"
        Public Const LABEL_SELECT_CF_ORDER_DETAIL As String = "CF_ORDER_DETAIL"
        Private Const FIRST_POS As Integer = 0
        Private Const COL_NAME As String = "ID"
        Private Const LABEL_DEALER As String = "DEALER_NAME"
        Private Const YES As String = "Y"
        Private Const NO As String = "N"
        Public Const URL As String = "ClaimFulfillmentOrderDetailForm.aspx"

        Public Const CLAIM_FULLFILLMENT_CODE_PROPERTY As String = "Code"
        Public Const CLAIM_FULLFILLMENT_DESCRIPTION_PROPERTY As String = "Description"
        Public Const PRICE_LIST_SOURCE_PROPERTY As String = "PriceListSourceXcd"
        Public Const COUNTRY_PROPERTY As String = "CountryId"
        Public Const PRICE_LIST_CODE_PROPERTY As String = "PriceListCode"
        Public Const EQUIPMENT_TYPE_PROPERTY As String = "EquipmentTypeXcd"
        Public Const SERVICE_CLASS_PROPERTY As String = "ServiceClassXcd"
        Public Const SERVICE_TYPE_PROPERTY As String = "ServiceTypeXcd"
        Public Const SERVICE_LEVEL_PROPERTY As String = "ServiceLevelXcd"
        Public Const STOCK_ITEM_TYPE_PROPERTY As String = "StockItemTypeXcd"

#End Region

#Region "Page Return Type"
        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public moCFOrderDetailleId As Guid
            Public HasDataChanged As Boolean
            Public Sub New(LastOp As DetailPageCommand, oCFOrderDetaileId As Guid, hasDataChanged As Boolean)
                LastOperation = LastOp
                moCFOrderDetailleId = oCFOrderDetaileId
                Me.HasDataChanged = hasDataChanged
            End Sub
        End Class

#End Region

#Region "Properties"
        Private ReadOnly Property TheClaimFulfillmentOrderDetail As ClaimFulfillmentOrderDetail
            Get
                If State.moClaimFulfillmentOrderDetail Is Nothing Then
                    If State.IsCFOrderDetailNew = True Then
                        ' For creating, inserting
                        State.moClaimFulfillmentOrderDetail = New ClaimFulfillmentOrderDetail
                        State.moCFOrderDetailId = State.moClaimFulfillmentOrderDetail.Id
                    Else
                        ' For updating, deleting
                        State.moClaimFulfillmentOrderDetail = New ClaimFulfillmentOrderDetail(State.moCFOrderDetailId)
                    End If
                End If

                Return State.moClaimFulfillmentOrderDetail
            End Get
        End Property

#End Region

#Region "Handlers"

#Region "Handlers-Init"

        Protected WithEvents moErrorController As ErrorController

        Protected WithEvents moPanel As System.Web.UI.WebControls.Panel
        Protected WithEvents EditPanel_WRITE As System.Web.UI.WebControls.Panel
        Protected WithEvents Button1 As System.Web.UI.WebControls.Button
        Protected WithEvents moCoverageEditPanel As System.Web.UI.WebControls.Panel
        Protected WithEvents moDealerMultipleDrop As MultipleColumnDDLabelControl

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                MasterPage.MessageController.Clear_Hide()
                ClearLabelsErrSign()
                If Not Page.IsPostBack Then
                    MasterPage.MessageController.Clear()
                    MasterPage.UsePageTabTitleInBreadCrum = False
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")
                    UpdateBreadCrum()
                    SetStateProperties()
                    AddControlMsg(btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO,
                                                                        MSG_TYPE_CONFIRM, True)

                    If State.IsCFOrderDetailNew = True Then
                        CreateNew()
                    End If

                End If

                BindBoPropertiesToLabels()
                CheckIfComingFromConfirm()
                If Not IsPostBack Then
                    AddLabelDecorations(TheClaimFulfillmentOrderDetail)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)

            End Try
            If Me.State.LastOperation = DetailPageCommand.Redirect_ Then
                MasterPage.MessageController.Clear_Hide()
                ClearLabelsErrSign()
                State.LastOperation = DetailPageCommand.Nothing_
            Else
                ShowMissingTranslations(MasterPage.MessageController)
            End If
        End Sub

        Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall

            Try
                If CallingParameters IsNot Nothing Then
                    'Get the id from the parent
                    State.moClaimFulfillmentOrderDetail = New ClaimFulfillmentOrderDetail(CType(CallingParameters, Guid))
                Else
                    State.IsCFOrderDetailNew = True
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub btnApply_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnApply_WRITE.Click
            ApplyChanges()
        End Sub

        Private Sub GoBack()
            Dim retType As New BillingCycleForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back,
                                                                State.moCFOrderDetailId, State.boChanged)
            ReturnToCallingPage(retType)
        End Sub

        Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
            Try
                If IsDirtyBO() = True Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM,
                                                HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    GoBack()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try
                If Not State.IsCFOrderDetailNew Then
                    'Reload from the DB
                    State.moClaimFulfillmentOrderDetail = New ClaimFulfillmentOrderDetail(State.moCFOrderDetailId)
                ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                    'It was a new with copy
                    State.moClaimFulfillmentOrderDetail.Clone(State.ScreenSnapShotBO)
                Else
                    State.moClaimFulfillmentOrderDetail = New ClaimFulfillmentOrderDetail
                End If
                PopulateAll()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CreateNew()
            State.ScreenSnapShotBO = Nothing
            State.moCFOrderDetailId = Guid.Empty
            State.IsCFOrderDetailNew = True
            State.moClaimFulfillmentOrderDetail = New ClaimFulfillmentOrderDetail
            ClearAll()
            SetButtonsState(True)
            PopulateAll()
            DisablePriceFields()

            If State.IsCFOrderDetailNew Then
                ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
            End If

        End Sub

        Private Sub btnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNew_WRITE.Click
            Try
                If IsDirtyBO() = True Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
                Else
                    CreateNew()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CreateNewCopy()

            PopulateBOsFromForm()
            State.moCFOrderDetailId = Guid.Empty
            Dim newObj As New ClaimFulfillmentOrderDetail
            newObj.Copy(TheClaimFulfillmentOrderDetail)

            State.moClaimFulfillmentOrderDetail = newObj

            State.IsCFOrderDetailNew = True

            SetButtonsState(True)

            'create the backup copy
            State.ScreenSnapShotBO = New ClaimFulfillmentOrderDetail
            State.ScreenSnapShotBO.Copy(TheClaimFulfillmentOrderDetail)

        End Sub

        Private Sub btnCopy_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnCopy_WRITE.Click
            Try
                If IsDirtyBO() = True Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
                Else
                    CreateNewCopy()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            End Try
        End Sub

        Private Sub btnDelete_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnDelete_WRITE.Click
            Try
                Try
                    DisplayMessage(Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
                Catch ex As Threading.ThreadAbortException
                Catch ex As Exception
                    HandleErrors(ex, MasterPage.MessageController)
                End Try

            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Clear"

        Private Sub ClearTexts()
            moCodeText.Text = Nothing
            moDescriptionText.Text = Nothing
        End Sub

        Private Sub ClearAll()
            ClearTexts()
            ClearList(moPriceListSourceDrop)
            ClearList(moCountryDrop)
            ClearList(moPriceListCodeDrop)
            ClearList(moEquipmentTypeDrop)
            ClearList(moServiceClassDrop)
            ClearList(moServiceTypeDrop)
            ClearList(moServiceLevelDrop)
            ClearList(moStockItemTypeDrop)
        End Sub

#End Region

#Region "Populate"

        Private Sub UpdateBreadCrum()
            If (State IsNot Nothing) Then
                If (State IsNot Nothing) Then
                    MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("CLAIM_FULFILLMENT_ORDER_DETAIL")
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("CLAIM_FULFILLMENT_ORDER_DETAIL")
                End If
            End If
        End Sub

        Private Sub PopulateDropDowns()
            Dim oLanguageId As Guid = Authentication.LangId
            Try
                Dim pricelistsourceLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("PRICE_LIST_SOURCE", Thread.CurrentPrincipal.GetLanguageCode())
                moPriceListSourceDrop.Populate(pricelistsourceLkl, New PopulateOptions() With
                                                      {
                                                        .ValueFunc = AddressOf PopulateOptions.GetExtendedCode
                                                       })

                Dim CountryList As DataElements.ListItem() =
                            CommonConfigManager.Current.ListManager.GetList(listCode:=ListCodes.Country)
                Dim UserCountries As DataElements.ListItem() = (From Country In CountryList
                                                                Where ElitaPlusIdentity.Current.ActiveUser.Countries.Contains(Country.ListItemId)
                                                                Select Country).ToArray()
                moCountryDrop.Populate(UserCountries.ToArray(),
                                    New PopulateOptions() With
                                    {
                                        .AddBlankItem = True
                                    })

                Dim equipmenttypeLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("CLAIM_EQUIP_TYPE", Thread.CurrentPrincipal.GetLanguageCode())
                moEquipmentTypeDrop.Populate(equipmenttypeLkl, New PopulateOptions() With
                                                      {
                                                        .ValueFunc = AddressOf PopulateOptions.GetExtendedCode
                                                       })

                Dim serviceclassLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("SVCCLASS", Thread.CurrentPrincipal.GetLanguageCode())
                moServiceClassDrop.Populate(serviceclassLkl, New PopulateOptions() With
                                                      {
                                                        .ValueFunc = AddressOf PopulateOptions.GetExtendedCode
                                                       })

                Dim servicetypelLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("SVCTYP", Thread.CurrentPrincipal.GetLanguageCode())
                moServiceTypeDrop.Populate(servicetypelLkl, New PopulateOptions() With
                                                      {
                                                         .ValueFunc = AddressOf PopulateOptions.GetExtendedCode
                                                       })

                Dim servicelevelLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("SVC_LVL", Thread.CurrentPrincipal.GetLanguageCode())
                moServiceLevelDrop.Populate(servicetypelLkl, New PopulateOptions() With
                                                      {
                                                        .ValueFunc = AddressOf PopulateOptions.GetExtendedCode
                                                       })

                Dim stockitemtypeLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("STCK_ITEM_TYP", Thread.CurrentPrincipal.GetLanguageCode())
                moStockItemTypeDrop.Populate(stockitemtypeLkl, New PopulateOptions() With
                                                      {
                                                        .ValueFunc = AddressOf PopulateOptions.GetExtendedCode
                                                       })
                'Price List information
                If Not State.moClaimFulfillmentOrderDetail.CountryId.Equals(Guid.Empty) Then
                    Dim oListContext1 As New ListContext
                    oListContext1.CountryId = State.moClaimFulfillmentOrderDetail.CountryId
                    Dim PriceList As DataElements.ListItem() =
                                            CommonConfigManager.Current.ListManager.GetList(listCode:="PriceListByCountry", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=oListContext1)
                    moPriceListCodeDrop.Populate(PriceList.ToArray(), New PopulateOptions() With
                                {
                                    .AddBlankItem = False
                                })
                End If

            Catch ex As Exception
                MasterPage.MessageController.AddError(CF_ORDER_DETAIL_FORM001)
                MasterPage.MessageController.AddError(ex.Message, False)
                MasterPage.MessageController.Show()
            End Try
        End Sub

        Private Sub PopulateTexts()
            Try
                With TheClaimFulfillmentOrderDetail
                    If State.IsCFOrderDetailNew = True Then
                        moCodeText.Text = Nothing
                        moDescriptionText.Text = Nothing
                    Else
                        BindSelectItem(.PriceListSourceXcd, moPriceListSourceDrop)
                        BindSelectItem(.EquipmentTypeXcd, moEquipmentTypeDrop)
                        PopulateControlFromBOProperty(moCodeText, .Code)
                        PopulateControlFromBOProperty(moDescriptionText, .Description)
                        SetSelectedItem(moCountryDrop, .CountryId)
                        If moPriceListSourceDrop.SelectedValue = "PRICE_LIST_SOURCE-PRICE_LIST" Then
                            EnablePriceFields()
                            If Not .CountryId.Equals(Guid.Empty) Then
                                Dim list As DataView = LookupListNew.GetPriceListLookupList(.CountryId)
                                Dim selectedItemId As Guid = LookupListNew.GetIdFromCode(list, .PriceListCode)
                                PopulateControlFromBOProperty(moPriceListCodeDrop, selectedItemId)
                            End If
                        Else
                            DisablePriceFields()
                        End If
                    End If


                End With
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateAll()
            If State.IsCFOrderDetailNew = True Then
                PopulateDropDowns()
            Else
                ClearAll()
                PopulateDropDowns()
                PopulateTexts()
            End If
        End Sub

        Protected Sub PopulateBOsFromForm()

            With TheClaimFulfillmentOrderDetail
                Dim strPriceListCode As String
                PopulateBOProperty(TheClaimFulfillmentOrderDetail, CLAIM_FULLFILLMENT_CODE_PROPERTY, moCodeText)
                PopulateBOProperty(TheClaimFulfillmentOrderDetail, CLAIM_FULLFILLMENT_DESCRIPTION_PROPERTY, moDescriptionText)
                PopulateBOProperty(TheClaimFulfillmentOrderDetail, COUNTRY_PROPERTY, moCountryDrop)
                PopulateBOProperty(TheClaimFulfillmentOrderDetail, PRICE_LIST_SOURCE_PROPERTY, moPriceListSourceDrop, False, True)
                If moPriceListCodeDrop.SelectedValue.Equals(String.Empty) Then
                    strPriceListCode = String.Empty
                ElseIf moPriceListCodeDrop.SelectedValue <> LookupListNew.GetIdFromCode(LookupListCache.LK_PRICE_LIST, .PriceListCode).ToString() Then
                    strPriceListCode = LookupListNew.GetCodeFromId(LookupListNew.LK_PRICE_LIST, New Guid(moPriceListCodeDrop.SelectedValue))
                    PopulateBOProperty(TheClaimFulfillmentOrderDetail, PRICE_LIST_CODE_PROPERTY, strPriceListCode)
                End If
                PopulateBOProperty(TheClaimFulfillmentOrderDetail, EQUIPMENT_TYPE_PROPERTY, moEquipmentTypeDrop, False, True)
                PopulateBOProperty(TheClaimFulfillmentOrderDetail, SERVICE_CLASS_PROPERTY, moServiceClassDrop, False, True)
                PopulateBOProperty(TheClaimFulfillmentOrderDetail, SERVICE_TYPE_PROPERTY, moServiceTypeDrop, False, True)
                PopulateBOProperty(TheClaimFulfillmentOrderDetail, SERVICE_LEVEL_PROPERTY, moServiceLevelDrop, False, True)
                PopulateBOProperty(TheClaimFulfillmentOrderDetail, STOCK_ITEM_TYPE_PROPERTY, moStockItemTypeDrop, False, True)
            End With
            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If

        End Sub

#End Region

#Region "Gui-Validation"

        Private Sub SetButtonsState(bIsNew As Boolean)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, Not bIsNew)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, Not bIsNew)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, Not bIsNew)
            ControlMgr.SetEnableControl(Me, moCodeText, bIsNew)
        End Sub

#End Region

#Region "Business Part"

        Private Function IsDirtyBO() As Boolean
            Dim bIsDirty As Boolean = True

            Try
                With TheClaimFulfillmentOrderDetail
                    PopulateBOsFromForm()
                    bIsDirty = .IsDirty
                End With
            Catch ex As Exception
                MasterPage.MessageController.AddError(CF_ORDER_DETAIL_FORM001)
                MasterPage.MessageController.AddError(ex.Message, False)
                MasterPage.MessageController.Show()
            End Try
            Return bIsDirty
        End Function

        Private Function ApplyChanges() As Boolean

            Try
                Dim bIsOk As Boolean = True
                PopulateBOsFromForm()

                If TheClaimFulfillmentOrderDetail.IsDirty() Then
                    If State.IsCFOrderDetailNew = True Then
                        If TheClaimFulfillmentOrderDetail.CFCodeExists(TheClaimFulfillmentOrderDetail.Code) Then
                            MasterPage.MessageController.AddError(Message.MSG_CODE_EXISTS_PROMPT)
                            MasterPage.MessageController.Show()
                            bIsOk = False
                        Else
                            TheClaimFulfillmentOrderDetail.Save()
                        End If
                    Else
                        TheClaimFulfillmentOrderDetail.Save()
                    End If
                    If bIsOk = True Then
                        State.boChanged = True
                        If State.IsCFOrderDetailNew = True Then
                            State.IsCFOrderDetailNew = False
                        End If
                        PopulateAll()
                        SetButtonsState(State.IsCFOrderDetailNew)
                        MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                    Else
                        MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
                    End If

                Else
                        MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Function
#End Region

#Region "State-Management"

        Protected Sub ComingFromBack()
            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                ' Return from the Back Button

                Select Case confResponse
                    Case MSG_VALUE_YES
                        ' Save and go back to Search Page
                        If ApplyChanges() = True Then
                            State.boChanged = True
                            GoBack()
                        End If
                    Case MSG_VALUE_NO
                        GoBack()
                End Select
            End If

        End Sub

        Protected Sub ComingFromNewCopy()
            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                ' Return from the New Copy Button

                Select Case confResponse
                    Case MSG_VALUE_YES
                        ' Save and create a new Copy BO
                        If ApplyChanges() = True Then
                            State.boChanged = True
                            CreateNewCopy()
                        End If
                    Case MSG_VALUE_NO
                        ' create a new BO
                        CreateNewCopy()
                End Select
            End If

        End Sub
        Protected Sub ComingFromNew()
            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                ' Return from the New Copy Button

                Select Case confResponse
                    Case MSG_VALUE_YES
                        ' Save and create a new Copy BO
                        If ApplyChanges() = True Then
                            State.boChanged = True
                            CreateNew()
                        End If
                    Case MSG_VALUE_NO
                        ' create a new BO
                        CreateNew()
                End Select
            End If

        End Sub

        Private Sub DoDelete()
            Try
                State.moClaimFulfillmentOrderDetail.DeleteAndSave()
                State.HasDataChanged = True
                ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, State.moCFOrderDetailId, State.HasDataChanged))
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub


        Protected Sub CheckIfComingFromConfirm()
            Try
                Select Case State.ActionInProgress
                    ' Period
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ComingFromBack()
                    Case ElitaPlusPage.DetailPageCommand.New_
                        ComingFromNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        ComingFromNewCopy()
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        DoDelete()
                End Select

                'Clean after consuming the action
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                HiddenSaveChangesPromptResponse.Value = String.Empty
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Handlers-Labels"

        Private Sub BindBoPropertiesToLabels()
            BindBOPropertyToLabel(TheClaimFulfillmentOrderDetail, "Code", moCodeLabel)
            BindBOPropertyToLabel(TheClaimFulfillmentOrderDetail, "Description", moDescriptionLabel)
            BindBOPropertyToLabel(TheClaimFulfillmentOrderDetail, "PriceListSourceXcd", moPriceListSourceLabel)
            BindBOPropertyToLabel(TheClaimFulfillmentOrderDetail, "CountryId", moCountryLabel)
            BindBOPropertyToLabel(TheClaimFulfillmentOrderDetail, "PriceListCode", moPriceListCodeLabel)
            BindBOPropertyToLabel(TheClaimFulfillmentOrderDetail, "EquipmentTypeXcd", moEquipmentTypeLabel)
            BindBOPropertyToLabel(TheClaimFulfillmentOrderDetail, "ServiceClassXcd", moServiceClassLabel)
            BindBOPropertyToLabel(TheClaimFulfillmentOrderDetail, "ServiceTypeXcd", moServiceClassLabel)
            BindBOPropertyToLabel(TheClaimFulfillmentOrderDetail, "ServiceLevelXcd", moServiceLevelLabel)
            BindBOPropertyToLabel(TheClaimFulfillmentOrderDetail, "StockItemTypeXcd", moStockItemTypeLabel)
        End Sub

        Private Sub ClearLabelsErrSign()
            ClearLabelErrSign(moCodeLabel)
            ClearLabelErrSign(moDescriptionLabel)
            ClearLabelErrSign(moPriceListSourceLabel)
            ClearLabelErrSign(moCountryLabel)
            ClearLabelErrSign(moPriceListCodeLabel)
            ClearLabelErrSign(moEquipmentTypeLabel)
            ClearLabelErrSign(moServiceClassLabel)
            ClearLabelErrSign(moServiceTypeLabel)
            ClearLabelErrSign(moServiceLevelLabel)
            ClearLabelErrSign(moStockItemTypeLabel)
        End Sub

        Public Shared Sub SetLabelColor(lbl As Label)
            lbl.ForeColor = Color.Black
        End Sub

        Protected Sub moCountryDrop_SelectedIndexChanged(sender As Object, e As EventArgs) Handles moCountryDrop.SelectedIndexChanged

            If moCountryDrop.SelectedItem IsNot Nothing Then
                Dim oListContext1 As New ListContext
                oListContext1.CountryId = New Guid(moCountryDrop.SelectedValue)
                Dim PriceList As DataElements.ListItem() =
                                        CommonConfigManager.Current.ListManager.GetList(listCode:="PriceListByCountry", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=oListContext1)
                moPriceListCodeDrop.Populate(PriceList.ToArray(), New PopulateOptions() With
                            {
                                .AddBlankItem = True
                            })
            End If
        End Sub

        Protected Sub moPriceListSourceDrop_SelectedIndexChanged(sender As Object, e As EventArgs) Handles moPriceListSourceDrop.SelectedIndexChanged

            If moPriceListSourceDrop.SelectedItem IsNot Nothing And moPriceListSourceDrop.SelectedValue = "PRICE_LIST_SOURCE-PRICE_LIST" Then
                EnablePriceFields()
            Else
                DisablePriceFields()
            End If
        End Sub
#End Region

#Region "Controlling Logic"
        Protected Sub DisablePriceFields()

            moPriceListCodeDrop.SelectedIndex = -1
            moCountryDrop.SelectedIndex = -1
            moCountryDrop.Visible = False
            moPriceListCodeDrop.Visible = False
            moCountryLabel.Visible = False
            moPriceListCodeLabel.Visible = False

        End Sub

        Protected Sub EnablePriceFields()

            moCountryDrop.Visible = True
            moPriceListCodeDrop.Visible = True
            moCountryLabel.Visible = True
            moPriceListCodeLabel.Visible = True

        End Sub
#End Region

#End Region
    End Class
End Namespace
