Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Tables

    Partial Class ItemForm
        Inherits ElitaPlusPage
#Region "Page State"

#Region "MyState"
        Class MyState
            Public MyBO As Item
            Public Id As Guid
            Public moItemId As Guid
            Public IsItemNew As Boolean = False
            Public DealerId As Guid
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public boChanged As Boolean = False
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
            ' Me.State.MyBO = New Item(CType(Me.CallingParameters, Guid))
            'Me.State.moItemId = CType(Me.CallingParameters, Guid)
            State.moItemId = State.MyBO.Id
            If State.moItemId.Equals(Guid.Empty) Then
                State.IsItemNew = True
                ClearAll()
                SetButtonsState(True)
                PopulateAll()
            Else
                State.IsItemNew = False
                SetButtonsState(False)
                PopulateAll()
            End If
        End Sub


#End Region

#Region "Constants"

        Private Const ITEM_FORM001 As String = "ITEM_FORM001" ' Maintain Item Fetch Exception
        Private Const ITEM_FORM002 As String = "ITEM_FORM002" ' Maintain Item Update Exception
        Private Const ITEM_FORM003 As String = "ITEM_FORM003" ' Max Replacement Cost should be greater or equal to 0 and less or equal to 1000000
        Protected Const CONFIRM_MSG As String = "MGS_CONFIRM_PROMPT" '"Are you sure you want to delete the selected records?"
        Private Const ITEM_LIST As String = "ItemSearchForm.aspx"
        Public Const URL As String = "ItemForm.aspx"
        Private Const MSG_UNIQUE_VIOLATION As String = "MSG_DUPLICATE_KEY_CONSTRAINT_VIOLATED" '"Unique value is in use"
        Private Const UNIQUE_VIOLATION As String = "unique constraint"
        'Actions
        Private Const ACTION_NONE As String = "ACTION_NONE"
        Private Const ACTION_BACK As String = "ACTION_BACK"
        Private Const ACTION_PRODUCTCODE_EXISTS As String = "ACTION_PRODUCTCODE_EXISTS"
        Private Const ACTION_AFTER_PRODUCTCODE_EXISTS As String = "ACTION_AFTER_PRODUCTCODE_EXISTS"
        Private Const ACTION_BACK_PRODUCTCODE_EXISTS As String = "ACTION_BACK_PRODUCTCODE_EXISTS"
        Private Const MIN_MAX_REPLACEMENT_COST As Double = 0
        Private Const MAX_MAX_REPLACEMENT_COST As Double = 1000000
        Public Const DEALER_ID_PROPERTY As String = "DealerId"
        Public Const PRODUCT_CODE_ID_PROPERTY As String = "ProductCodeId"
        Public Const RISK_TYPE_ID_PROPERTY As String = "RiskTypeId"
        Public Const MAX_REPLACEMENT_COST_PROPERTY As String = "MaxReplacementCost"
        Public Const LABEL_SELECT_DEALERCODE As String = "Dealer"

#End Region

#Region "Attributes"

        Private moItem As Item

#End Region

#Region "Properties"

        Private ReadOnly Property TheItem() As Item
            Get

                If moItem Is Nothing Then
                    If State.IsItemNew = True Then
                        ' For creating, inserting
                        'moItem = New Item
                        'Me.State.moItemId = moItem.Id
                        State.moItemId = State.MyBO.Id
                        ' moItem = New Item(Me.State.moItemId)
                    Else
                        ' For updating, deleting
                        State.moItemId = State.MyBO.Id
                        moItem = New Item(State.moItemId)
                    End If
                End If

                Return moItem
            End Get
        End Property

        Private Property ItemId() As String
            Get
                Return moItemIdLabel.Text
            End Get
            Set(Value As String)
                moItemIdLabel.Text = Value
            End Set
        End Property

        Private Property Action() As String
            Get
                Return moActionLabel.Text
            End Get
            Set(Value As String)
                moActionLabel.Text = Value
            End Set
        End Property

        Public ReadOnly Property TheDealerControl() As MultipleColumnDDLabelControl
            Get
                If multipleDropControl Is Nothing Then
                    multipleDropControl = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
                End If
                Return multipleDropControl
            End Get
        End Property

#End Region

#Region "Handlers"

#Region "Handlers-Init"

        Protected WithEvents moErrorController As ErrorController
        Protected WithEvents multipleDropControl As MultipleColumnDDLabelControl


#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                moErrorController.Clear_Hide()
                ClearLabelsErrSign()
                If Not Page.IsPostBack Then
                    'Me.SetStateProperties()

                    Action = ACTION_NONE
                    AddControlMsg(btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO,
                                                                       MSG_TYPE_CONFIRM, True)


                    If State.IsItemNew = True Then
                        CreateNew()
                    End If
                    PopulateAll()
                    'PopulateFormFromBOs()
                End If
                BindBoPropertiesToLabels()
                CheckIfComingFromConfirm()
                If Not IsPostBack Then
                    AddLabelDecorations(State.MyBO)
                End If
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
            ShowMissingTranslations(moErrorController)
        End Sub
        Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
            Try
                If CallingParameters IsNot Nothing Then
                    'Get the id from the parent
                    State.MyBO = New Item(CType(CallingParameters, Guid))
                Else
                    State.IsItemNew = True
                End If
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub ItemForm_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
            If State.MyBO.Inuseflag = "Y" Then ' The coverage record is in use and should not allow changes except Configuration Super User Roles
                'Display a warning of this record is in use when opening the page first time
                If Not Page.IsPostBack Then
                    'Me.MasterPage.MessageController.AddWarning("RECORD_IN_USE")
                    'moErrorController.AddErrorAndShow("RECORD_IN_USE", True)
                    DisplayMessage("RECORD_IN_USE", "", MSG_BTN_OK, MSG_TYPE_ALERT)

                    If ElitaPlusPrincipal.Current.IsInRole(CoverageForm.ConfigurationSuperUserRole) = False Then
                        'diable the save button to prevent any change to the coverage record
                        btnApply_WRITE.Enabled = False
                        btnDelete_WRITE.Enabled = False
                    End If
                End If
            End If
        End Sub
#End Region

#Region "Handlers-DropDown"

        'Private Sub moDealerDrop_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moDealerDrop.SelectedIndexChanged
        '    Try
        '        Me.ClearList(moProductCodeDrop)
        '        If moDealerDrop.SelectedIndex > 0 Then
        '            PopulateProductCode()
        '        End If
        '    Catch ex As Exception
        '        Me.HandleErrors(ex, moErrorController)
        '    End Try
        'End Sub

        Private Sub OnFromDrop_Changed(fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
        Handles multipleDropControl.SelectedDropChanged
            Try
                State.MyBO.DealerID = TheDealerControl.SelectedGuid
                PopulateDealer()
                ClearList(moProductCodeDrop)
                If TheDealerControl.SelectedIndex > 0 Then
                    PopulateProductCode()
                End If
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub SaveChanges()

            If ApplyChanges() = True Then
                State.boChanged = True
                If State.IsItemNew = True Then
                    State.IsItemNew = False
                End If
                PopulateAll()
            End If
        End Sub

        Private Sub GoBack()
            Dim retType As New ItemSearchForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back,
                                                                                State.moItemId, State.boChanged)
            ReturnToCallingPage(retType)
        End Sub

        Private Function IsEditAllowed() As Boolean
            If State.MyBO.Inuseflag = "Y" AndAlso ElitaPlusPrincipal.Current.IsInRole(CoverageForm.ConfigurationSuperUserRole) = False Then
                Return False
            Else
                Return True
            End If
        End Function

        Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
            Try
                If IsEditAllowed() AndAlso IsDirtyBO() = True Then
                    Action = ACTION_BACK
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM,
                                                HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    GoBack()
                End If
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub btnApply_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnApply_WRITE.Click
            Try
                SaveChanges()
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try

                If Not State.IsItemNew Then
                    'Reload from the DB
                    State.MyBO = New Item(State.MyBO.Id)
                    'ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                    '    'It was a new with copy
                    '    Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
                Else
                    CreateNew()
                End If

                PopulateAll()
                'Me.PopulateFormFromBOs()
                'Me.EnableDisableFields()

                PopulateAll()
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub CreateNew()
            State.IsItemNew = True
            State.MyBO = New Item
            ClearAll()
            SetButtonsState(True)
            'Me.TheItem.DealerID = Guid.Empty
            PopulateAll()
        End Sub


        Private Sub btnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNew_WRITE.Click

            Try
                PopulateBOsFromForm()
                If IsEditAllowed() AndAlso IsDirtyBO() = True Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
                Else
                    If IsEditAllowed() = False Then 'enable the save and delete button disabled when open the page
                        btnApply_WRITE.Enabled = True
                        btnDelete_WRITE.Enabled = True
                    End If

                    CreateNew()
                End If
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try

        End Sub

        Private Sub CreateNewCopy()

            PopulateBOsFromForm()
            Dim newObj As New Item
            newObj.CopyFrom(State.MyBO)
            newObj.Inuseflag = "N"

            State.MyBO = newObj

            'Me.State.moItemId = Guid.Empty
            State.IsItemNew = True
            SetButtonsState(True)
            State.MyBO.ItemNumber = Nothing
        End Sub

        Private Sub btnCopy_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnCopy_WRITE.Click

            Try
                If IsEditAllowed() AndAlso IsDirtyBO() = True Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
                Else
                    If IsEditAllowed() = False Then 'enable the save and delete button disabled when open the page
                        btnApply_WRITE.Enabled = True
                        btnDelete_WRITE.Enabled = True
                    End If
                    CreateNewCopy()
                End If
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub btnDelete_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete_WRITE.Click

            Try
                State.MyBO.Delete()
                State.MyBO.Save()
                State.boChanged = True
                ReturnToCallingPage(New ItemSearchForm.ReturnType(ElitaPlusPage.DetailPageCommand.Delete, State.moItemId, State.boChanged))
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try

        End Sub

#End Region

#Region "Handlers-Labels"

        Private Sub BindBoPropertiesToLabels()
            BindBOPropertyToLabel(State.MyBO, "DealerID", TheDealerControl.CaptionLabel)
            BindBOPropertyToLabel(State.MyBO, "ProductCodeId", moProductCodeLabel)
            BindBOPropertyToLabel(State.MyBO, "RiskTypeId", moRiskTypeLabel)
            BindBOPropertyToLabel(State.MyBO, "MaxReplacementCost", moMaxReplacementCostLabel)
            BindBOPropertyToLabel(State.MyBO, "OptionalItem", moOptionalItem)
            BindBOPropertyToLabel(State.MyBO, "OptionalItemCode", moOptionalItemCode)
            ClearGridHeadersAndLabelsErrSign()
        End Sub

        Private Sub ClearLabelsErrSign()
            ClearLabelErrSign(TheDealerControl.CaptionLabel)
            ClearLabelErrSign(moProductCodeLabel)
            ClearLabelErrSign(moRiskTypeLabel)
            ClearLabelErrSign(moMaxReplacementCostLabel)
            ClearLabelErrSign(moOptionalItem)
            ClearLabelErrSign(moOptionalItemCode)
        End Sub
#End Region

#End Region

#Region "Clear"

        Private Sub ClearTexts()
            moItemNumberText.Text = Nothing
            moMaxReplacementCostText.Text = Nothing
        End Sub

        Private Sub ClearAll()
            ClearTexts()
            TheDealerControl.ClearMultipleDrop()
            ClearList(moProductCodeDrop)
            ClearList(moRiskTypeDrop)
        End Sub

#End Region

#Region "Populate"
        Public Function GetNoID() As Guid
            Dim yesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_N)
            Return yesId
        End Function

        Private Sub PopulateDealer()
            Dim oDealerview As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
            TheDealerControl.SetControl(True,
                                            TheDealerControl.MODES.NEW_MODE,
                                            True,
                                            oDealerview,
                                            "* " + TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALERCODE),
                                            True, True)
            Try
                TheDealerControl.NothingSelected = True
                TheDealerControl.SelectedGuid = State.MyBO.DealerID
                PopulateProductCode()
            Catch ex As Exception
                moErrorController.AddError(ITEM_FORM001)
                moErrorController.AddError(ex.Message, False)
                moErrorController.Show()
            End Try
        End Sub

        Private Sub PopulateOptionalItem()
            Dim yesNoLkL As DataView = LookupListNew.DropdownLookupList("YESNO", ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
            'Me.BindListControlToDataView(Me.moOptionalItemDrop, yesNoLkL, "DESCRIPTION", "ID", False)
            moOptionalItemDrop.Populate(CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
           {
              .AddBlankItem = False
           })
            Try
                moOptionalItemDrop.ClearSelection()
                If State.MyBO.OptionalItem = Guid.Empty Then
                    BindSelectItem(GetNoID.ToString, moOptionalItemDrop) 'default to N
                Else
                    BindSelectItem(State.MyBO.OptionalItem.ToString, moOptionalItemDrop)
                End If

            Catch ex As Exception
                moErrorController.AddError(ITEM_FORM001)
                moErrorController.AddError(ex.Message, False)
                moErrorController.Show()
            End Try
        End Sub
        Private Sub AddProductArrayForClientSide(dealerID As Guid)
            ' Dim dvPC As ProductCode.ProductCodeSearchByDealerDV = ProductCode.getListByDealer(dealerID, ElitaPlusIdentity.Current.ActiveUser.LanguageId, Guid.Empty)
            '  BindListTextToDataView(Me.moProductCodeDropBundledFlag, dvPC, ProductCode.ProductCodeSearchByDealerDV.COL_PRODUCT_CODE, ProductCode.ProductCodeSearchByDealerDV.COL_BUNDLED_ITEM)
            Dim listcontext As ListContext = New ListContext()
            listcontext.DealerId = dealerID
            listcontext.LanguageId = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim prodLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.ProductCodeWithBundledItemByDealer, Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            moProductCodeDropBundledFlag.Populate(prodLkl, New PopulateOptions() With
             {
            .AddBlankItem = True,
            .TextFunc = AddressOf .GetExtendedCode,
            .SortFunc = AddressOf .GetExtendedCode,
            .ValueFunc = AddressOf .GetCode
             })
        End Sub

        Private Sub PopulateProductCode()
            Dim oDealerId As Guid = TheDealerControl.SelectedGuid ' Me.GetSelectedItem(moDealerDrop)
            Try
                ' Me.BindListControlToDataView(moProductCodeDrop, LookupListNew.GetProductCodeLookupList(oDealerId), "CODE") 'only prod_code table ,sort and text column should be CODE
                Dim listcontext As ListContext = New ListContext()
                listcontext.DealerId = oDealerId
                listcontext.LanguageId = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                Dim prodLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.ProductCodeByDealer, Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                moProductCodeDrop.Populate(prodLkl, New PopulateOptions() With
               {
              .AddBlankItem = True,
              .TextFunc = AddressOf .GetCode,
              .SortFunc = AddressOf .GetCode
             })

                If State.IsItemNew = True Then
                    BindSelectItem(Nothing, moProductCodeDrop)
                Else
                    BindSelectItem(State.MyBO.ProductCodeId.ToString, moProductCodeDrop)
                End If
                PopulateRiskType()
                AddProductArrayForClientSide(oDealerId)
            Catch ex As Exception
                moErrorController.AddError(ITEM_FORM001)
                moErrorController.AddError(ex.Message, False)
                moErrorController.Show()
            End Try
        End Sub

        Private Sub PopulateRiskType()
            Try
                ' Me.BindListControlToDataView(Me.moRiskTypeDrop, LookupListNew.GetRiskTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id), , , True)
                Dim listcontext As ListContext = New ListContext()
                listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                Dim riskTypeLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("RiskTypeByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                moRiskTypeDrop.Populate(riskTypeLkl, New PopulateOptions() With
             {
               .AddBlankItem = True
                })

                If State.IsItemNew = True Then
                    BindSelectItem(Nothing, moRiskTypeDrop)
                Else
                    BindSelectItem(State.MyBO.RiskTypeId.ToString, moRiskTypeDrop)
                End If
                PopulateTexts()
            Catch ex As Exception
                moErrorController.AddError(ITEM_FORM001)
                moErrorController.AddError(ex.Message, False)
                moErrorController.Show()
            End Try
        End Sub

        Private Sub PopulateTexts()

            If State.IsItemNew = True Then
                moMaxReplacementCostText.Text = GetAmountFormattedDoubleString("0")
            Else
                With State.MyBO
                    PopulateControlFromBOProperty(moMaxReplacementCostText, .MaxReplacementCost)
                    PopulateControlFromBOProperty(moItemNumberText, .ItemNumber)
                    PopulateControlFromBOProperty(moOptionalItemCodeText, .OptionalItemCode)
                End With
            End If
        End Sub

        Private Sub PopulateAll()
            PopulateDealer()
            PopulateOptionalItem()
        End Sub

#End Region

#Region "Gui-Validation"

        Private Sub SetButtonsState(bIsNew As Boolean)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, Not bIsNew)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, Not bIsNew)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, Not bIsNew)
            ControlMgr.SetVisibleControl(Me, moItemNumberLabel, Not bIsNew)
            ControlMgr.SetVisibleControl(Me, moItemNumberText, Not bIsNew)
        End Sub

#End Region

#Region "Business Part"

        Private Sub PopulateBOsFromForm()
            With State.MyBO
                ' DropDowns
                PopulateBOProperty(State.MyBO, "DealerID", TheDealerControl.SelectedGuid)
                PopulateBOProperty(State.MyBO, "ProductCodeId", moProductCodeDrop)
                PopulateBOProperty(State.MyBO, "RiskTypeId", moRiskTypeDrop)
                PopulateBOProperty(State.MyBO, "OptionalItem", moOptionalItemDrop)
                ' Texts
                If moMaxReplacementCostText.Text = String.Empty Then
                    moMaxReplacementCostText.Text = GetAmountFormattedDoubleString("0")
                End If
                PopulateBOProperty(State.MyBO, "MaxReplacementCost", moMaxReplacementCostText)
                PopulateBOProperty(State.MyBO, "OptionalItemCode", moOptionalItemCodeText)
            End With
            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Private Function IsDirtyBO() As Boolean
            Dim bIsDirty As Boolean = True
            Try
                With State.MyBO
                    PopulateBOsFromForm()
                    bIsDirty = .IsDirty
                End With
            Catch ex As Exception
                moErrorController.AddError(ITEM_FORM002)
                moErrorController.AddError(ex.Message, False)
                moErrorController.Show()
            End Try
            Return bIsDirty
        End Function

        Private Function ApplyChanges() As Boolean
            Page.Validate()
            If Page.IsValid = False Then Return False
            Dim bIsOk As Boolean = True
            Dim bIsDirty As Boolean = False
            PopulateBOsFromForm()
            With State.MyBO
                bIsDirty = .IsDirty
                Dim tempMaxReplacementCost As Double = CType(moMaxReplacementCostText.Text, Double)
                If tempMaxReplacementCost < MIN_MAX_REPLACEMENT_COST Or tempMaxReplacementCost > MAX_MAX_REPLACEMENT_COST Then
                    moErrorController.AddError(ITEM_FORM003)
                    moErrorController.Show()
                    bIsOk = False
                ElseIf ((Action <> ACTION_AFTER_PRODUCTCODE_EXISTS) AndAlso (.ProductCodeExists = True)) Then
                    If Action = ACTION_BACK Then
                        Action = ACTION_BACK_PRODUCTCODE_EXISTS
                    Else
                        Action = ACTION_PRODUCTCODE_EXISTS
                    End If
                    moErrorController.AddError(Message.MSG_PRODUCTCODE_ITEM_EXISTS_PROMPT)
                    moErrorController.Show()
                    bIsOk = False
                Else
                    .Save()
                    SetButtonsState(False)
                End If
            End With
            If bIsOk = True Then
                If bIsDirty = True Then
                    AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                Else
                    AddInfoMsg(Message.MSG_RECORD_NOT_SAVED)
                End If
            End If
            Return bIsOk
        End Function

#End Region

#Region "State-Management"

        Protected Sub ComingFromBack()
            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
            Dim sItemForm As String

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

        Protected Sub ComingFromProductCodeExits()
            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                ' Return from the Back Button

                Select Case confResponse
                    Case MSG_VALUE_YES
                        ' Save the changes
                        Action = ACTION_AFTER_PRODUCTCODE_EXISTS
                        SaveChanges()
                    Case MSG_VALUE_NO
                        ' Do nothing
                        Action = ACTION_NONE
                End Select
            End If

        End Sub

        Protected Sub ComingFromBackProductCodeExits()
            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
            Dim sItemForm As String

            If Not confResponse = String.Empty Then
                ' Return from the Back Button

                Select Case confResponse
                    Case MSG_VALUE_YES
                        ' Save and go back to Search Page
                        Action = ACTION_AFTER_PRODUCTCODE_EXISTS
                        If ApplyChanges() = True Then
                            State.boChanged = True
                            GoBack()
                        End If
                    Case MSG_VALUE_NO
                        ' Go back to Search Page
                        Action = ACTION_NONE
                        GoBack()
                End Select
            End If

        End Sub

        Protected Sub CheckIfComingFromConfirm()
            Select Case Action
                Case ACTION_BACK
                    ComingFromBack()
                    If Action = ACTION_BACK Then Action = ACTION_NONE
                Case ACTION_PRODUCTCODE_EXISTS
                    ComingFromProductCodeExits()
                Case ACTION_BACK_PRODUCTCODE_EXISTS
                    ComingFromBackProductCodeExits()
                Case ACTION_AFTER_PRODUCTCODE_EXISTS
                    Action = ACTION_NONE
            End Select

            'Clean after consuming the action

            HiddenSaveChangesPromptResponse.Value = String.Empty

        End Sub

#End Region


    End Class

End Namespace
