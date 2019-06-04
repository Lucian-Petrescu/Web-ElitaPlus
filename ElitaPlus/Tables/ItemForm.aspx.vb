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
            Me.State.moItemId = Me.State.MyBO.Id
            If Me.State.moItemId.Equals(Guid.Empty) Then
                Me.State.IsItemNew = True
                ClearAll()
                SetButtonsState(True)
                PopulateAll()
            Else
                Me.State.IsItemNew = False
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
                    If Me.State.IsItemNew = True Then
                        ' For creating, inserting
                        'moItem = New Item
                        'Me.State.moItemId = moItem.Id
                        Me.State.moItemId = Me.State.MyBO.Id
                        ' moItem = New Item(Me.State.moItemId)
                    Else
                        ' For updating, deleting
                        Me.State.moItemId = Me.State.MyBO.Id
                        moItem = New Item(Me.State.moItemId)
                    End If
                End If

                Return moItem
            End Get
        End Property

        Private Property ItemId() As String
            Get
                Return moItemIdLabel.Text
            End Get
            Set(ByVal Value As String)
                moItemIdLabel.Text = Value
            End Set
        End Property

        Private Property Action() As String
            Get
                Return moActionLabel.Text
            End Get
            Set(ByVal Value As String)
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

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                moErrorController.Clear_Hide()
                ClearLabelsErrSign()
                If Not Page.IsPostBack Then
                    'Me.SetStateProperties()

                    Action = ACTION_NONE
                    Me.AddControlMsg(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO,
                                                                       Me.MSG_TYPE_CONFIRM, True)


                    If Me.State.IsItemNew = True Then
                        CreateNew()
                    End If
                    PopulateAll()
                    'PopulateFormFromBOs()
                End If
                BindBoPropertiesToLabels()
                CheckIfComingFromConfirm()
                If Not Me.IsPostBack Then
                    Me.AddLabelDecorations(Me.State.MyBO)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, moErrorController)
            End Try
            Me.ShowMissingTranslations(moErrorController)
        End Sub
        Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
            Try
                If Not Me.CallingParameters Is Nothing Then
                    'Get the id from the parent
                    Me.State.MyBO = New Item(CType(Me.CallingParameters, Guid))
                Else
                    Me.State.IsItemNew = True
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub ItemForm_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
            If State.MyBO.Inuseflag = "Y" Then ' The coverage record is in use and should not allow changes except Configuration Super User Roles
                'Display a warning of this record is in use when opening the page first time
                If Not Page.IsPostBack Then
                    'Me.MasterPage.MessageController.AddWarning("RECORD_IN_USE")
                    'moErrorController.AddErrorAndShow("RECORD_IN_USE", True)
                    Me.DisplayMessage("RECORD_IN_USE", "", Me.MSG_BTN_OK, Me.MSG_TYPE_ALERT)

                    If ElitaPlusPrincipal.Current.IsInRole(CoverageForm.ConfigurationSuperUserRole) = False Then
                        'diable the save button to prevent any change to the coverage record
                        Me.btnApply_WRITE.Enabled = False
                        Me.btnDelete_WRITE.Enabled = False
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

        Private Sub OnFromDrop_Changed(ByVal fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
        Handles multipleDropControl.SelectedDropChanged
            Try
                Me.State.MyBO.DealerID = TheDealerControl.SelectedGuid
                PopulateDealer()
                Me.ClearList(moProductCodeDrop)
                If TheDealerControl.SelectedIndex > 0 Then
                    PopulateProductCode()
                End If
            Catch ex As Exception
                HandleErrors(ex, Me.moErrorController)
            End Try
        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub SaveChanges()

            If ApplyChanges() = True Then
                Me.State.boChanged = True
                If Me.State.IsItemNew = True Then
                    Me.State.IsItemNew = False
                End If
                PopulateAll()
            End If
        End Sub

        Private Sub GoBack()
            Dim retType As New ItemSearchForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back,
                                                                                Me.State.moItemId, Me.State.boChanged)
            Me.ReturnToCallingPage(retType)
        End Sub

        Private Function IsEditAllowed() As Boolean
            If State.MyBO.Inuseflag = "Y" AndAlso ElitaPlusPrincipal.Current.IsInRole(CoverageForm.ConfigurationSuperUserRole) = False Then
                Return False
            Else
                Return True
            End If
        End Function

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try
                If IsEditAllowed() AndAlso IsDirtyBO() = True Then
                    Action = ACTION_BACK
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM,
                                                Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    GoBack()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub btnApply_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply_WRITE.Click
            Try
                SaveChanges()
            Catch ex As Exception
                Me.HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try

                If Not Me.State.IsItemNew Then
                    'Reload from the DB
                    Me.State.MyBO = New Item(Me.State.MyBO.Id)
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
                Me.HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub CreateNew()
            Me.State.IsItemNew = True
            Me.State.MyBO = New Item
            ClearAll()
            Me.SetButtonsState(True)
            'Me.TheItem.DealerID = Guid.Empty
            Me.PopulateAll()
        End Sub


        Private Sub btnNew_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click

            Try
                PopulateBOsFromForm()
                If IsEditAllowed() AndAlso IsDirtyBO() = True Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
                Else
                    If IsEditAllowed() = False Then 'enable the save and delete button disabled when open the page
                        Me.btnApply_WRITE.Enabled = True
                        Me.btnDelete_WRITE.Enabled = True
                    End If

                    CreateNew()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, moErrorController)
            End Try

        End Sub

        Private Sub CreateNewCopy()

            PopulateBOsFromForm()
            Dim newObj As New Item
            newObj.CopyFrom(Me.State.MyBO)
            newObj.Inuseflag = "N"

            Me.State.MyBO = newObj

            'Me.State.moItemId = Guid.Empty
            Me.State.IsItemNew = True
            Me.SetButtonsState(True)
            Me.State.MyBO.ItemNumber = Nothing
        End Sub

        Private Sub btnCopy_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy_WRITE.Click

            Try
                If IsEditAllowed() AndAlso IsDirtyBO() = True Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
                Else
                    If IsEditAllowed() = False Then 'enable the save and delete button disabled when open the page
                        Me.btnApply_WRITE.Enabled = True
                        Me.btnDelete_WRITE.Enabled = True
                    End If
                    CreateNewCopy()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub btnDelete_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click

            Try
                Me.State.MyBO.Delete()
                Me.State.MyBO.Save()
                Me.State.boChanged = True
                Me.ReturnToCallingPage(New ItemSearchForm.ReturnType(ElitaPlusPage.DetailPageCommand.Delete, Me.State.moItemId, Me.State.boChanged))
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try

        End Sub

#End Region

#Region "Handlers-Labels"

        Private Sub BindBoPropertiesToLabels()
            Me.BindBOPropertyToLabel(Me.State.MyBO, "DealerID", TheDealerControl.CaptionLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "ProductCodeId", moProductCodeLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "RiskTypeId", moRiskTypeLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "MaxReplacementCost", moMaxReplacementCostLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "OptionalItem", moOptionalItem)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "OptionalItemCode", moOptionalItemCode)
            Me.ClearGridHeadersAndLabelsErrSign()
        End Sub

        Private Sub ClearLabelsErrSign()
            Me.ClearLabelErrSign(Me.TheDealerControl.CaptionLabel)
            Me.ClearLabelErrSign(Me.moProductCodeLabel)
            Me.ClearLabelErrSign(Me.moRiskTypeLabel)
            Me.ClearLabelErrSign(Me.moMaxReplacementCostLabel)
            Me.ClearLabelErrSign(Me.moOptionalItem)
            Me.ClearLabelErrSign(Me.moOptionalItemCode)
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
                TheDealerControl.SelectedGuid = Me.State.MyBO.DealerID
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
            Me.moOptionalItemDrop.Populate(CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
           {
              .AddBlankItem = False
           })
            Try
                moOptionalItemDrop.ClearSelection()
                If State.MyBO.OptionalItem = Guid.Empty Then
                    BindSelectItem(GetNoID.ToString, moOptionalItemDrop) 'default to N
                Else
                    BindSelectItem(Me.State.MyBO.OptionalItem.ToString, moOptionalItemDrop)
                End If

            Catch ex As Exception
                moErrorController.AddError(ITEM_FORM001)
                moErrorController.AddError(ex.Message, False)
                moErrorController.Show()
            End Try
        End Sub
        Private Sub AddProductArrayForClientSide(ByVal dealerID As Guid)
            ' Dim dvPC As ProductCode.ProductCodeSearchByDealerDV = ProductCode.getListByDealer(dealerID, ElitaPlusIdentity.Current.ActiveUser.LanguageId, Guid.Empty)
            '  BindListTextToDataView(Me.moProductCodeDropBundledFlag, dvPC, ProductCode.ProductCodeSearchByDealerDV.COL_PRODUCT_CODE, ProductCode.ProductCodeSearchByDealerDV.COL_BUNDLED_ITEM)
            Dim listcontext As ListContext = New ListContext()
            listcontext.DealerId = dealerID
            listcontext.LanguageId = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim prodLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.ProductCodeWithBundledItemByDealer, Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            Me.moProductCodeDropBundledFlag.Populate(prodLkl, New PopulateOptions() With
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

                If Me.State.IsItemNew = True Then
                    BindSelectItem(Nothing, moProductCodeDrop)
                Else
                    BindSelectItem(Me.State.MyBO.ProductCodeId.ToString, moProductCodeDrop)
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
                Me.moRiskTypeDrop.Populate(riskTypeLkl, New PopulateOptions() With
             {
               .AddBlankItem = True
                })

                If Me.State.IsItemNew = True Then
                    BindSelectItem(Nothing, moRiskTypeDrop)
                Else
                    BindSelectItem(Me.State.MyBO.RiskTypeId.ToString, moRiskTypeDrop)
                End If
                PopulateTexts()
            Catch ex As Exception
                moErrorController.AddError(ITEM_FORM001)
                moErrorController.AddError(ex.Message, False)
                moErrorController.Show()
            End Try
        End Sub

        Private Sub PopulateTexts()

            If Me.State.IsItemNew = True Then
                moMaxReplacementCostText.Text = Me.GetAmountFormattedDoubleString("0")
            Else
                With Me.State.MyBO
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

        Private Sub SetButtonsState(ByVal bIsNew As Boolean)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, Not bIsNew)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, Not bIsNew)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, Not bIsNew)
            ControlMgr.SetVisibleControl(Me, moItemNumberLabel, Not bIsNew)
            ControlMgr.SetVisibleControl(Me, moItemNumberText, Not bIsNew)
        End Sub

#End Region

#Region "Business Part"

        Private Sub PopulateBOsFromForm()
            With Me.State.MyBO
                ' DropDowns
                PopulateBOProperty(Me.State.MyBO, "DealerID", TheDealerControl.SelectedGuid)
                PopulateBOProperty(Me.State.MyBO, "ProductCodeId", moProductCodeDrop)
                PopulateBOProperty(Me.State.MyBO, "RiskTypeId", moRiskTypeDrop)
                PopulateBOProperty(Me.State.MyBO, "OptionalItem", moOptionalItemDrop)
                ' Texts
                If moMaxReplacementCostText.Text = String.Empty Then
                    moMaxReplacementCostText.Text = Me.GetAmountFormattedDoubleString("0")
                End If
                PopulateBOProperty(Me.State.MyBO, "MaxReplacementCost", moMaxReplacementCostText)
                PopulateBOProperty(Me.State.MyBO, "OptionalItemCode", moOptionalItemCodeText)
            End With
            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Private Function IsDirtyBO() As Boolean
            Dim bIsDirty As Boolean = True
            Try
                With Me.State.MyBO
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
            With Me.State.MyBO
                bIsDirty = .IsDirty
                Dim tempMaxReplacementCost As Double = CType(moMaxReplacementCostText.Text, Double)
                If tempMaxReplacementCost < Me.MIN_MAX_REPLACEMENT_COST Or tempMaxReplacementCost > Me.MAX_MAX_REPLACEMENT_COST Then
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
                    Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                Else
                    Me.AddInfoMsg(Message.MSG_RECORD_NOT_SAVED)
                End If
            End If
            Return bIsOk
        End Function

#End Region

#Region "State-Management"

        Protected Sub ComingFromBack()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
            Dim sItemForm As String

            If Not confResponse = String.Empty Then
                ' Return from the Back Button

                Select Case confResponse
                    Case Me.MSG_VALUE_YES
                        ' Save and go back to Search Page
                        If ApplyChanges() = True Then
                            Me.State.boChanged = True
                            GoBack()
                        End If
                    Case Me.MSG_VALUE_NO
                        GoBack()
                End Select
            End If

        End Sub

        Protected Sub ComingFromProductCodeExits()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                ' Return from the Back Button

                Select Case confResponse
                    Case Me.MSG_VALUE_YES
                        ' Save the changes
                        Action = ACTION_AFTER_PRODUCTCODE_EXISTS
                        SaveChanges()
                    Case Me.MSG_VALUE_NO
                        ' Do nothing
                        Action = ACTION_NONE
                End Select
            End If

        End Sub

        Protected Sub ComingFromBackProductCodeExits()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
            Dim sItemForm As String

            If Not confResponse = String.Empty Then
                ' Return from the Back Button

                Select Case confResponse
                    Case Me.MSG_VALUE_YES
                        ' Save and go back to Search Page
                        Action = ACTION_AFTER_PRODUCTCODE_EXISTS
                        If ApplyChanges() = True Then
                            Me.State.boChanged = True
                            GoBack()
                        End If
                    Case Me.MSG_VALUE_NO
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

            Me.HiddenSaveChangesPromptResponse.Value = String.Empty

        End Sub

#End Region


    End Class

End Namespace
