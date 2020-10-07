Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Public Class EquipmentForm
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
    Public Const URL As String = "EquipmentForm.aspx"

    Public Const GRID_COL_EDIT As Integer = 0
    Public Const GRID_COL_DELETE As Integer = 1

    ' Comment Grid
    Public Const GRID_COL_COMMENT As Integer = 1
    Public Const GRID_COL_EQUIPMENT_COMMENT_ID As Integer = 2

    ' Attribute Grid
    Public Const GRID_COL_ATTRIBUTE_NAME As Integer = 1
    Public Const GRID_COL_ATTRIBUTE_VALUE As Integer = 2
    Public Const GRID_COL_ATTRIBUTE_VALUE_ID As Integer = 3

    ' Images Grid
    Public Const GRID_COL_CODE As Integer = 1
    Public Const GRID_COL_DESCRIPTION As Integer = 2
    Public Const GRID_COL_IMAGE_TYPE As Integer = 3
    Public Const GRID_COL_EQUIPMENT_IMAGE_ID As Integer = 3

    'Related Equipment
    Public Const GRID_COL_RELATED_EQUIPMENT_ID As Integer = 2
    Public Const GRID_COL_EQUIPMENT_TYPE As Integer = 3
    Public Const GRID_COL_EQUIPMENT_DESCRIPTION As Integer = 4
    Public Const GRID_COL_MANUFACTURER As Integer = 5
    Public Const GRID_COL_MODEL As Integer = 6
    Public Const GRID_COL_IN_OEM_BOX As Integer = 7
    Public Const GRID_COL_IS_COVERED As Integer = 8

    'control in Related Equipment Grid
    Public Const GRID_CONTROL_IS_IN_OEM_BOX As String = "moInOemBoxDrop"
    Public Const GRID_CONTROL_IS_COVERED As String = "moIsCoveredDrop"

    Private Const TAB_TOTAL_COUNT As Integer = 4
    Private Const TAB_IMAGES As Integer = 2
    Public Const PAGETITLE As String = "EQUIPMENT"
#End Region
#Region "Variables"
    Private listDisabledTabs As New Collections.Generic.List(Of Integer)
    Private SelectedTabIndex As Integer = 0
#End Region
#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As Equipment
        Public HasDataChanged As Boolean
        Public Sub New(LastOp As DetailPageCommand, curEditingBo As Equipment, hasDataChanged As Boolean)
            LastOperation = LastOp
            EditingBo = curEditingBo
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class
#End Region

#Region "Page State"

    Class MyState
        Public MyBO As Equipment
        Public MyCommentChildBO As EquipmentComment
        Public MyImageChildBO As EquipmentImage
        Public MyRelatedEquipmentChildBO As RelatedEquipment
        Public ScreenSnapShotBO As Equipment
        Public ScreenSnapCommentChildBO As EquipmentComment
        Public ScreenSnapImageChildBO As EquipmentImage
        Public screenSnapRelatedEquipmentChildBO As RelatedEquipment

        Public IsImageEditing As Boolean = False
        Public IsCommentEditing As Boolean = False
        Public IsRelatedEquipmentEditing As Boolean = False

        Public SortExpressionCommentDetailGrid As String = Equipment.EquipmentCommentSelectionView.COL_NAME_COMMENT
        Public SortExpressionImageDetailGrid As String = Equipment.EquipmentImageSelectionView.COL_NAME_CODE
        Public SortExpressionRelatedEquipmentDetailGrid As String = Equipment.RelatedEquipmentSelectionView.COL_NAME_MANUFACTURER & ", " &
            Equipment.RelatedEquipmentSelectionView.COL_NAME_EQUIPMENT_TYPE & ", " & Equipment.RelatedEquipmentSelectionView.COL_NAME_DESCRIPTION & "," & Equipment.RelatedEquipmentSelectionView.COL_NAME_MODEL


        Public CommentDetailPageIndex As Integer = 0
        Public ImageDetailPageIndex As Integer = 0
        Public AttributeValueDetailPageIndex As Integer = 0
        Public RelatedEquipmentDetailPageIndex As Integer = 0

        Public CommentSelectedChildId As Guid = Guid.Empty
        Public ImageSelectedChildId As Guid = Guid.Empty
        Public AttributeValueSelectedChildId As Guid = Guid.Empty
        Public RelatedEquipmentSelectedChildId As Guid = Guid.Empty

        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public LastErrMsg As String
        Public HasDataChanged As Boolean
    End Class

    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            Return CType(MyBase.State, MyState)
        End Get
    End Property

    Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
        Try
            If CallingParameters IsNot Nothing Then
                'Get the id from the parent
                State.MyBO = New Equipment(CType(CallingParameters, Guid))
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Page Events"
    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Try
            MasterPage.MessageController.Clear_Hide()

            If Not IsPostBack Then
                UpdateBreadCrum()
                'Date Calendars
                '  Me.MenuEnabled = False
                AddControlMsg(btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, True)
                AddControlMsg(btnCommentDeleteChild_Write, Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, True)
                'Me.AddControlMsg(Me.btnImageDeleteChild_Write, Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, True)
                If State.MyBO Is Nothing Then
                    State.MyBO = New Equipment
                End If
                PopulateDropdowns()
                'Def-27047:Added folloiwng code to cast Equipment BO to type IAttributable.
                AttributeValues.ParentBusinessObject = CType(State.MyBO, IAttributable)
                PopulateFormFromBOs()
                EnableDisableFields()
                AttributeValues.TranslateHeaders()
                ShowUserControl_on_RelatedEquipment(False)
                UserControlSearchAvailableEquipment.ShowCancelButton = True
            Else
                'Def-27047:Added folloiwng code to cast Equipment BO to type IAttributable.
                AttributeValues.ParentBusinessObject = CType(State.MyBO, IAttributable)
                SelectedTabIndex = hdnSelectedTab.Value
            End If
            BindBoPropertiesToLabels()
            BindCommentsDetailBoPropertiesToLabels()
            BindImagesDetailBoPropertiesToLabels()
            CheckIfComingFromSaveConfirm()
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

    Private Sub UpdateBreadCrum()
        If (State IsNot Nothing) Then
            If (State IsNot Nothing) Then
                MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(PAGETITLE)
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
            End If
        End If
    End Sub

#Region "Controlling Logic"

    Protected Sub EnableDisableFields()
        If State.IsCommentEditing Then
            ControlMgr.SetVisibleControl(Me, PanelCommentEditDetail, True)
            EnableDisableParentControls(False)
        ElseIf State.IsImageEditing Then
            ControlMgr.SetVisibleControl(Me, PanelImageEditDetail, True)
            EnableDisableParentControls(False)
        ElseIf State.IsRelatedEquipmentEditing Then
            ControlMgr.SetVisibleControl(Me, RelatedEquipmentScroller, True)
            EnableDisableRelatedEquipmentControl(True)
            EnableDisableParentControls(False)
            'DISABLE THE TRASH CAN HERE
        Else
            ControlMgr.SetVisibleControl(Me, PanelCommentEditDetail, False)
            ControlMgr.SetVisibleControl(Me, PanelImageEditDetail, False)
            EnableDisableRelatedEquipmentControl(False)
            EnableDisableParentControls(True)
        End If

        'Enabled by Default
        ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnCopy_WRITE, True)
        'Now disable depebding on the object state
        If State.MyBO.IsNew Then
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
        End If

        'WRITE YOU OWN CODE HERE
    End Sub

    Sub EnableDisableParentControls(enableToggle As Boolean)
        ControlMgr.SetEnableControl(Me, btnBack, enableToggle)
        ControlMgr.SetEnableControl(Me, btnCopy_WRITE, enableToggle)
        ControlMgr.SetEnableControl(Me, btnDelete_WRITE, enableToggle)
        ControlMgr.SetEnableControl(Me, btnSave_WRITE, enableToggle)
        ControlMgr.SetEnableControl(Me, btnUndo_Write, enableToggle)
        'ControlMgr.SetEnableControl(Me, tsEquipment, enableToggle)
        EnableTab(enableToggle)

        Dim isMasterEquipment As Boolean = True
        If (moIsMasterModelDrop.SelectedIndex <> NO_ITEM_SELECTED_INDEX) Then
            Dim yesNoId As Guid = New Guid(moIsMasterModelDrop.SelectedValue)
            If (LookupListNew.GetCodeFromId(LookupListNew.GetYesNoLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), yesNoId) = "N") Then
                isMasterEquipment = False
            End If
        End If
        ControlMgr.SetEnableControl(Me, moManufacturerDrop, enableToggle And State.MyBO.IsNew)
        ControlMgr.SetEnableControl(Me, moIsMasterModelDrop, enableToggle And State.MyBO.IsNew)
        ControlMgr.SetEnableControl(Me, moModelText, enableToggle And ((Not isMasterEquipment And State.MyBO.IsNew) Or (IsMasterEquipment)))
        ControlMgr.SetEnableControl(Me, moDescriptionText, enableToggle And ((State.MyBO.IsNew And isMasterEquipment) Or (Not isMasterEquipment)))
        ControlMgr.SetEnableControl(Me, moMasterEquipmentDrop, enableToggle And Not isMasterEquipment)
        ControlMgr.SetEnableControl(Me, moManufacturerWarrentyText, enableToggle And Not isMasterEquipment)
        ControlMgr.SetEnableControl(Me, moEquipmentClassDrop, enableToggle)
        ControlMgr.SetEnableControl(Me, moEquipmentTypeDrop, enableToggle)
        ControlMgr.SetEnableControl(Me, moRepairableDrop, enableToggle)

        ControlMgr.SetEnableControl(Me, moColor, enableToggle)
        ControlMgr.SetEnableControl(Me, moMemory, enableToggle)
        ControlMgr.SetEnableControl(Me, moCarrier, enableToggle)

        ControlMgr.SetVisibleControl(Me, DataGridImageDetail, enableToggle)
        ControlMgr.SetVisibleControl(Me, DataGridCommentDetail, enableToggle)

        ControlMgr.SetVisibleControl(Me, btnAddNewCommentFromGrid_WRITE, enableToggle)

    End Sub

    Sub EnableDisableRelatedEquipmentControl(enableToggle As Boolean)
        ControlMgr.SetEnableControl(Me, btnRelatedEquipmentCancelChild_Write, enableToggle)
        ControlMgr.SetEnableControl(Me, btnRelatedEquipmentOkChild_Write, enableToggle)
        ControlMgr.SetEnableControl(Me, btnRelatedEquipmentSelectChild_Write, Not enableToggle)
    End Sub

    Protected Sub BindBoPropertiesToLabels()
        BindBOPropertyToLabel(State.MyBO, "ManufacturerId", moManufacturerLabel)
        BindBOPropertyToLabel(State.MyBO, "IsMasterEquipment", moIsMasterModelLabel)
        BindBOPropertyToLabel(State.MyBO, "Model", moModelLabel)
        BindBOPropertyToLabel(State.MyBO, "Description", moDescriptionLabel)
        BindBOPropertyToLabel(State.MyBO, "MasterEquipmentId", moMasterEquipmentlLabel)
        BindBOPropertyToLabel(State.MyBO, "ManufacturerWarrenty", moManufacturerWarrentyLabel)
        BindBOPropertyToLabel(State.MyBO, "EquipmentClassId", moEquipmentClassLabel)
        BindBOPropertyToLabel(State.MyBO, "EquipmentTypeId", moEquipmentTypeLabel)
        BindBOPropertyToLabel(State.MyBO, "RepairableId", moRepairableLabel)

        BindBOPropertyToLabel(State.MyBO, "Color", lblColor)
        BindBOPropertyToLabel(State.MyBO, "Memory", lblMemory)
        BindBOPropertyToLabel(State.MyBO, "Carrier", lblCarrier)

        ClearGridHeadersAndLabelsErrSign()
    End Sub

    Protected Sub PopulateDropdowns()
        Dim languageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        Dim companyGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id

        ' Me.BindListControlToDataView(moIsMasterModelDrop, LookupListNew.GetYesNoLookupList(languageId))
        moIsMasterModelDrop.Populate(CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                           {
                                            .AddBlankItem = True
                                           })
        'Me.BindListControlToDataView(moManufacturerDrop, LookupListNew.GetManufacturerLookupList(companyGroupId))
        Dim listcontext As ListContext = New ListContext()
        listcontext.CompanyGroupId = companyGroupId
        Dim manLKL As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.ManufacturerByCompanyGroup, Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
        moManufacturerDrop.Populate(manLKL, New PopulateOptions() With
                {
                .AddBlankItem = True
                })
        'Me.BindListControlToDataView(moEquipmentClassDrop, LookupListNew.GetEquipmentClassLookupList(languageId)) 'EQPCLS
        moEquipmentClassDrop.Populate(CommonConfigManager.Current.ListManager.GetList("EQPCLS", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                {
                .AddBlankItem = True
                })
        'Me.BindListControlToDataView(moEquipmentTypeDrop, LookupListNew.GetEquipmentTypeLookupList(languageId)) 'EQPTYPE
        moEquipmentTypeDrop.Populate(CommonConfigManager.Current.ListManager.GetList("EQPTYPE", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                {
                .AddBlankItem = True
                })
        'Me.BindListControlToDataView(moRepairableDrop, LookupListNew.GetYesNoLookupList(languageId))
        moRepairableDrop.Populate(CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                          {
                                           .AddBlankItem = True
                                          })

        Dim colorSource As ListItem() = CommonConfigManager.Current.ListManager.GetList("EQPCOLOR", Thread.CurrentPrincipal.GetLanguageCode())
        mocolor.Populate(colorSource, New PopulateOptions() With
                              {
                              .ValueFunc = AddressOf PopulateOptions.GetExtendedCode,
                              .BlankItemValue = String.Empty,
                              .AddBlankItem = True
                              })

        Dim memorySource As ListItem() = CommonConfigManager.Current.ListManager.GetList("EQPMEMORY", Thread.CurrentPrincipal.GetLanguageCode())
        momemory.Populate(memorySource, New PopulateOptions() With
                            {
                            .ValueFunc = AddressOf PopulateOptions.GetExtendedCode,
                            .AddBlankItem = True,
                             .BlankItemValue = String.Empty
                            })

        Dim carrierSource As ListItem() = CommonConfigManager.Current.ListManager.GetList("EQPCARRIER", Thread.CurrentPrincipal.GetLanguageCode())
        moCarrier.Populate(carrierSource, New PopulateOptions() With
                            {
                            .ValueFunc = AddressOf PopulateOptions.GetExtendedCode,
                              .BlankItemValue = String.Empty,
                            .AddBlankItem = True
                            })       
    End Sub

    Protected Sub PopulateMasterModelDropDown()
        Dim companyGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
        Dim manufacturerIdString As String = Guid.Empty.ToString()
        Dim manufacturerId As Guid
        If (moManufacturerDrop.SelectedIndex <> NO_ITEM_SELECTED_INDEX) Then
            manufacturerIdString = moManufacturerDrop.SelectedValue
            manufacturerId = New Guid(moManufacturerDrop.SelectedValue)
        End If
         BindListControlToDataView(moMasterEquipmentDrop, LookupListNew.GetEquipmentLookupList(companyGroupId, manufacturerIdString, True)) 'EquipmentByCompanyGroupEquipmentType
        'Dim listcontext As ListContext = New ListContext()
        'listcontext.CompanyGroupId = companyGroupId
        'listcontext.ManufacturerId = manufacturerId
        'listcontext.EquipmentTypeId = Guid.Empty
        'Dim manLKL As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.EquipmentByCompanyGroupEquipmentType, Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
        'moMasterEquipmentDrop.Populate(manLKL, New PopulateOptions() With
        '        {
        '        .AddBlankItem = True
        '        })
    End Sub

    Protected Sub PopulateFormFromBOs()
        With State.MyBO
            PopulateCommentDetailGrid()
            PopulateImageDetailGrid()
            ' Populate Attributes
            AttributeValues.DataBind()

            'Me.SetGridItemStyleColor(GVRelatedEquipmentDetail)
            TranslateGridHeader(GVRelatedEquipmentDetail)
            PopulateRelatedEquipmentDetailGrid()

            PopulateControlFromBOProperty(moManufacturerDrop, .ManufacturerId)
            PopulateMasterModelDropDown()
            PopulateControlFromBOProperty(moIsMasterModelDrop, .IsMasterEquipment)
            PopulateControlFromBOProperty(moModelText, .Model)
            PopulateControlFromBOProperty(moDescriptionText, .Description)
            PopulateControlFromBOProperty(moMasterEquipmentDrop, .MasterEquipmentId)
            PopulateControlFromBOProperty(moManufacturerWarrentyText, .ManufacturerWarrenty)
            PopulateControlFromBOProperty(moEquipmentClassDrop, .EquipmentClassId)
            PopulateControlFromBOProperty(moEquipmentTypeDrop, .EquipmentTypeId)
            PopulateControlFromBOProperty(moRepairableDrop, .RepairableId)
            
            BindSelectItem(.Color, moColor)
            BindSelectItem(.Memory, moMemory)
            BindSelectItem(.Carrier, moCarrier)

            'Me.PopulateControlFromBOProperty(Me.moColor, .Color)
            'Me.PopulateControlFromBOProperty(Me.moMemory, .Memory)
            'Me.PopulateControlFromBOProperty(Me.moCarrier, .Carrier)

        End With

    End Sub

    Sub PopulateCommentDetailGrid()
        'This is a temporary Binding Logic. BEGIN        
        Dim dv As Equipment.EquipmentCommentSelectionView = State.MyBO.GetCommentSelectionView()
        dv.Sort = State.SortExpressionCommentDetailGrid

        DataGridCommentDetail.Columns(GRID_COL_COMMENT).SortExpression = Equipment.EquipmentCommentSelectionView.COL_NAME_COMMENT
        SetGridItemStyleColor(DataGridCommentDetail)

        SetPageAndSelectedIndexFromGuid(dv, State.CommentSelectedChildId, DataGridCommentDetail, State.CommentDetailPageIndex)
        State.CommentDetailPageIndex = DataGridCommentDetail.CurrentPageIndex

        DataGridCommentDetail.DataSource = dv
        DataGridCommentDetail.AutoGenerateColumns = False
        DataGridCommentDetail.DataBind()
        'This is a temporary Binding Logic. END
    End Sub

    Sub PopulateImageDetailGrid()
        'This is a temporary Binding Logic. BEGIN        
        Dim dv As Equipment.EquipmentImageSelectionView = State.MyBO.GetImageSelectionView()
        dv.Sort = State.SortExpressionImageDetailGrid

        DataGridImageDetail.Columns(GRID_COL_CODE).SortExpression = Equipment.EquipmentImageSelectionView.COL_NAME_CODE
        DataGridImageDetail.Columns(GRID_COL_DESCRIPTION).SortExpression = Equipment.EquipmentImageSelectionView.COL_NAME_DESCRIPTION
        DataGridImageDetail.Columns(GRID_COL_IMAGE_TYPE).SortExpression = Equipment.EquipmentImageSelectionView.COL_NAME_IMAGE_TYPE
        SetGridItemStyleColor(DataGridImageDetail)

        SetPageAndSelectedIndexFromGuid(dv, State.ImageSelectedChildId, DataGridImageDetail, State.ImageDetailPageIndex)
        State.ImageDetailPageIndex = DataGridImageDetail.CurrentPageIndex

        DataGridImageDetail.DataSource = dv
        DataGridImageDetail.AutoGenerateColumns = False
        DataGridImageDetail.DataBind()
        'This is a temporary Binding Logic. END
    End Sub

    Sub PopulateRelatedEquipmentDetailGrid()
        'This is a temporary Binding Logic . BEGIN
        Dim dv As Equipment.RelatedEquipmentSelectionView = State.MyBO.GetEquipmentSelectionView()
        dv.Sort = State.SortExpressionRelatedEquipmentDetailGrid
        'also set the related equipment id in the grid
        GVRelatedEquipmentDetail.Columns(GRID_COL_EQUIPMENT_TYPE).SortExpression = Equipment.RelatedEquipmentSelectionView.COL_NAME_EQUIPMENT_TYPE
        GVRelatedEquipmentDetail.Columns(GRID_COL_EQUIPMENT_DESCRIPTION).SortExpression = Equipment.RelatedEquipmentSelectionView.COL_NAME_DESCRIPTION
        GVRelatedEquipmentDetail.Columns(GRID_COL_MANUFACTURER).SortExpression = Equipment.RelatedEquipmentSelectionView.COL_NAME_MANUFACTURER
        GVRelatedEquipmentDetail.Columns(GRID_COL_MODEL).SortExpression = Equipment.RelatedEquipmentSelectionView.COL_NAME_MODEL
        GVRelatedEquipmentDetail.Columns(GRID_COL_IN_OEM_BOX).SortExpression = Equipment.RelatedEquipmentSelectionView.COL_NAME_IN_OEM_BOX
        GVRelatedEquipmentDetail.Columns(GRID_COL_IS_COVERED).SortExpression = Equipment.RelatedEquipmentSelectionView.COL_NAME_IS_COVERED
        SetGridItemStyleColor(GVRelatedEquipmentDetail)

        If State.IsRelatedEquipmentEditing Then
            SetPageAndSelectedIndexFromGuid(dv, State.RelatedEquipmentSelectedChildId, GVRelatedEquipmentDetail,
                                    GVRelatedEquipmentDetail.PageIndex, True)
        Else
            SetPageAndSelectedIndexFromGuid(dv, State.RelatedEquipmentSelectedChildId, GVRelatedEquipmentDetail, State.RelatedEquipmentDetailPageIndex)
            State.RelatedEquipmentDetailPageIndex = GVRelatedEquipmentDetail.PageIndex
        End If

        If dv.Count > 0 Then
            GVRelatedEquipmentDetail.DataSource = dv
            GVRelatedEquipmentDetail.AutoGenerateColumns = False
            GVRelatedEquipmentDetail.DataBind()
        Else
            dv.AddNew()
            dv(0)(Equipment.RelatedEquipmentSelectionView.COL_NAME_RELATED_EQUIPMENT_ID) = Guid.Empty.ToByteArray
            GVRelatedEquipmentDetail.DataSource = dv
            GVRelatedEquipmentDetail.DataBind()
            GVRelatedEquipmentDetail.Rows(0).Visible = False
            GVRelatedEquipmentDetail.Rows(0).Controls.Clear()
        End If

    End Sub

    Protected Sub PopulateBOsFormFrom()
        With State.MyBO
            PopulateBOProperty(State.MyBO, "ManufacturerId", moManufacturerDrop)
            PopulateBOProperty(State.MyBO, "IsMasterEquipment", moIsMasterModelDrop)
            PopulateBOProperty(State.MyBO, "Model", moModelText)
            PopulateBOProperty(State.MyBO, "Description", moDescriptionText)
            PopulateBOProperty(State.MyBO, "MasterEquipmentId", moMasterEquipmentDrop)
            PopulateBOProperty(State.MyBO, "EquipmentClassId", moEquipmentClassDrop)
            PopulateBOProperty(State.MyBO, "EquipmentTypeId", moEquipmentTypeDrop)
            PopulateBOProperty(State.MyBO, "RepairableId", moRepairableDrop)
            PopulateBOProperty(State.MyBO, "ManufacturerWarrenty", moManufacturerWarrentyText)
            
            PopulateBOProperty(State.MyBO, "Color", moColor,False,True)
            PopulateBOProperty(State.MyBO, "Memory", moMemory,False,True)                
            PopulateBOProperty(State.MyBO, "Carrier", moCarrier ,False,True)
            
        End With
        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub

    Protected Sub CreateNew()
        State.ScreenSnapShotBO = Nothing 'Reset the backup copy
        State.MyBO = New Equipment
        PopulateFormFromBOs()
        EnableDisableFields()
    End Sub

    Protected Sub CreateNewWithCopy()
        Dim newObj As New Equipment
        newObj.Copy(State.MyBO)

        State.MyBO = newObj
        PopulateFormFromBOs()
        EnableDisableFields()

        'create the backup copy
        State.ScreenSnapShotBO = New Equipment
        State.ScreenSnapShotBO.Copy(State.MyBO)
    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
        If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
            If State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr AndAlso State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.Accept Then
                State.MyBO.Save()
            End If
            Select Case State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                    CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                    CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    ReturnToCallingPage(New ReturnType(State.ActionInProgress, State.MyBO, State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.Accept
                    If (State.IsCommentEditing) Then
                        PopulateCommentChildBOFromDetail()
                        State.MyCommentChildBO.Save()
                        State.MyCommentChildBO.EndEdit()
                        State.IsCommentEditing = False
                    ElseIf (State.IsImageEditing) Then
                        PopulateImageChildBOFromDetail()
                        State.MyImageChildBO.Save()
                        State.MyImageChildBO.EndEdit()
                        State.IsImageEditing = False
                    ElseIf (State.IsRelatedEquipmentEditing) Then
                        PopulateRelatedEquipmentChildBOFromDetail()
                        State.MyRelatedEquipmentChildBO.Save()
                        State.MyRelatedEquipmentChildBO.EndEdit()
                    End If
                    EnableDisableFields()
                    PopulateCommentDetailGrid()
                    PopulateImageDetailGrid()
                    AttributeValues.DataBind()
                    PopulateRelatedEquipmentDetailGrid()
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
                    HandleErrors(New Exception(State.LastErrMsg), MasterPage.MessageController)

                Case ElitaPlusPage.DetailPageCommand.Accept
                    If (State.IsCommentEditing) Then
                        State.MyCommentChildBO.cancelEdit()
                        If State.MyCommentChildBO.IsSaveNew Then
                            State.MyCommentChildBO.Delete()
                            State.MyCommentChildBO.Save()
                        End If
                        State.IsCommentEditing = False
                    ElseIf (State.IsImageEditing) Then
                        State.MyImageChildBO.cancelEdit()
                        If State.MyCommentChildBO.IsSaveNew Then
                            State.MyImageChildBO.Delete()
                            State.MyImageChildBO.Save()
                        End If
                        State.IsImageEditing = False
                    ElseIf (State.IsRelatedEquipmentEditing) Then
                        State.MyRelatedEquipmentChildBO.cancelEdit()
                        If State.MyRelatedEquipmentChildBO.IsSaveNew Then
                            State.MyRelatedEquipmentChildBO.Delete()
                            State.MyRelatedEquipmentChildBO.Save()
                        End If
                        State.IsRelatedEquipmentEditing = False
                    End If
                    EnableDisableFields()
                    PopulateCommentDetailGrid()
                    PopulateImageDetailGrid()
                    AttributeValues.DataBind()
                    PopulateRelatedEquipmentDetailGrid()
            End Select
        End If
        'Clean after consuming the action
        State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        HiddenSaveChangesPromptResponse.Value = ""
    End Sub

    Sub BeginCommentChildEdit()
        State.IsCommentEditing = True
        EnableDisableFields()
        With State
            If Not .CommentSelectedChildId.Equals(Guid.Empty) Then
                .MyCommentChildBO = .MyBO.GetCommentChild(.CommentSelectedChildId)
            Else
                .MyCommentChildBO = .MyBO.GetNewCommentChild
            End If
            .MyCommentChildBO.BeginEdit()
        End With
        PopulateDetailFromCommentChildBO()
    End Sub

    Sub BeginImageChildEdit()
        State.IsImageEditing = True
        EnableDisableFields()
        With State
            If Not .ImageSelectedChildId.Equals(Guid.Empty) Then
                .MyImageChildBO = .MyBO.GetImageChild(.ImageSelectedChildId)
            Else
                .MyImageChildBO = .MyBO.GetNewImageChild
            End If
            .MyImageChildBO.BeginEdit()
        End With
        PopulateDetailFromImageChildBO()
    End Sub

    Sub EndCommentChildEdit(lastop As ElitaPlusPage.DetailPageCommand)
        Try
            With State
                Select Case lastop
                    Case ElitaPlusPage.DetailPageCommand.OK
                        PopulateCommentChildBOFromDetail()
                        .MyCommentChildBO.Save()
                        .MyCommentChildBO.EndEdit()
                    Case ElitaPlusPage.DetailPageCommand.Cancel
                        .MyCommentChildBO.cancelEdit()
                        If .MyCommentChildBO.IsSaveNew Then
                            .MyCommentChildBO.Delete()
                            .MyCommentChildBO.Save()
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Back
                        .MyCommentChildBO.cancelEdit()
                        If .MyCommentChildBO.IsSaveNew Then
                            .MyCommentChildBO.Delete()
                            .MyCommentChildBO.Save()
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        .MyCommentChildBO.Delete()
                        .MyCommentChildBO.Save()
                        .MyCommentChildBO.EndEdit()
                        .CommentSelectedChildId = Guid.Empty
                End Select
            End With
            State.IsCommentEditing = False
            EnableDisableFields()
            PopulateCommentDetailGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Sub EndImageChildEdit(lastop As ElitaPlusPage.DetailPageCommand)
        Try
            With State
                Select Case lastop
                    Case ElitaPlusPage.DetailPageCommand.OK
                        PopulateImageChildBOFromDetail()
                        .MyImageChildBO.Save()
                        .MyImageChildBO.EndEdit()
                        .MyBO.Save()
                    Case ElitaPlusPage.DetailPageCommand.Cancel
                        .MyImageChildBO.cancelEdit()
                        If .MyImageChildBO.IsSaveNew Then
                            .MyImageChildBO.Delete()
                            .MyImageChildBO.Save()
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Back
                        .MyImageChildBO.cancelEdit()
                        If .MyImageChildBO.IsSaveNew Then
                            .MyImageChildBO.Delete()
                            .MyImageChildBO.Save()
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        .MyImageChildBO.Delete()
                        .MyImageChildBO.Save()
                        .MyImageChildBO.EndEdit()
                        .ImageSelectedChildId = Guid.Empty
                End Select
            End With
            State.IsImageEditing = False
            EnableDisableFields()
            PopulateImageDetailGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Sub EndRelatedEquipmentChildEdit(lastop As ElitaPlusPage.DetailPageCommand)
        Try
            With State
                Select Case lastop
                    Case ElitaPlusPage.DetailPageCommand.OK
                        PopulateRelatedEquipmentChildBOFromDetail()
                        .MyRelatedEquipmentChildBO.Save()
                        .MyRelatedEquipmentChildBO.EndEdit()
                    Case ElitaPlusPage.DetailPageCommand.Cancel
                        .MyRelatedEquipmentChildBO.cancelEdit()
                        If .MyRelatedEquipmentChildBO.IsSaveNew Then
                            .MyRelatedEquipmentChildBO.Delete()
                            .MyRelatedEquipmentChildBO.Save()
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        .MyRelatedEquipmentChildBO.Delete()
                        .MyRelatedEquipmentChildBO.Save()
                        .MyRelatedEquipmentChildBO.EndEdit()
                        .RelatedEquipmentSelectedChildId = Guid.Empty
                End Select
            End With
            State.IsRelatedEquipmentEditing = False
            EnableDisableFields()
            PopulateRelatedEquipmentDetailGrid()
            EnableDisableRelatedEquipmentControl(False)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Sub PopulateDetailFromCommentChildBO()
        With State.MyCommentChildBO
            PopulateControlFromBOProperty(moCommentTextBox, .Comment)
        End With
    End Sub

    Sub PopulateDetailFromImageChildBO()
        With State.MyImageChildBO
            'Throw New NotImplementedException()
        End With
    End Sub

    Sub PopulateImageChildBOFromDetail()
        With State.MyImageChildBO
            'Throw New NotImplementedException 
        End With
        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub

    Sub PopulateCommentChildBOFromDetail()
        With State.MyCommentChildBO
            PopulateBOProperty(State.MyCommentChildBO, "Comment", moCommentTextBox)
        End With
        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub

    Protected Sub BindCommentsDetailBoPropertiesToLabels()
        With State
            BindBOPropertyToLabel(.MyCommentChildBO, "Comment", moCommentLabel)
        End With
        ClearGridHeadersAndLabelsErrSign()
    End Sub

    Protected Sub BindImagesDetailBoPropertiesToLabels()
        With State
            'Throw New NotImplementedException 
        End With
        ClearGridHeadersAndLabelsErrSign()
    End Sub

    Protected Sub BindRelatedEquipmentDetailBoPropertiesToLabels()
        With State
            'Throw New NotImplementedException 
        End With
        ClearGridHeadersAndLabelsErrSign()
    End Sub

    Sub BeginRelatedEquipmentChildEdit()
        State.IsRelatedEquipmentEditing = True
        ''Me.EnableDisableFields()
        With State
            If Not .RelatedEquipmentSelectedChildId.Equals(Guid.Empty) Then
                .MyRelatedEquipmentChildBO = .MyBO.GetRelatedEquipmentChild(.RelatedEquipmentSelectedChildId)
            Else
                .MyRelatedEquipmentChildBO = .MyBO.GetNewRelatedEquipmentChild
            End If
            .MyRelatedEquipmentChildBO.BeginEdit()
        End With
    End Sub

    Sub PopulateDetailFromRelatedEquipmentChildBO(gRow As GridViewRow)

        'fill the drop downs
        Dim moInOemBoxDrop As DropDownList = CType(gRow.Cells(GRID_COL_IN_OEM_BOX).FindControl(GRID_CONTROL_IS_IN_OEM_BOX), DropDownList)
        Dim moIsCoveredDrop As DropDownList = CType(gRow.Cells(GRID_COL_IS_COVERED).FindControl(GRID_CONTROL_IS_COVERED), DropDownList)
        Dim languageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId

        With State.MyRelatedEquipmentChildBO
            If moInOemBoxDrop IsNot Nothing Then
                PopulateControlFromBOProperty(moInOemBoxDrop, .IsInOemBoxId)
            End If
            If moIsCoveredDrop IsNot Nothing Then
                PopulateControlFromBOProperty(moIsCoveredDrop, .IsCoveredId)
            End If
        End With
    End Sub

    Sub PopulateRelatedEquipmentChildBOFromDetail()
        With State.MyRelatedEquipmentChildBO
            Dim moInOemBoxDrop As DropDownList = CType(GVRelatedEquipmentDetail.Rows(GVRelatedEquipmentDetail.EditIndex).Cells(GRID_COL_IN_OEM_BOX).FindControl(GRID_CONTROL_IS_IN_OEM_BOX), DropDownList)
            If moInOemBoxDrop IsNot Nothing Then
                PopulateBOProperty(State.MyRelatedEquipmentChildBO, "IsInOemBoxId", New Guid(moInOemBoxDrop.SelectedValue))
            End If
            Dim moIsCoveredDrop As DropDownList = CType(GVRelatedEquipmentDetail.Rows(GVRelatedEquipmentDetail.EditIndex).Cells(GRID_COL_IS_COVERED).FindControl(GRID_CONTROL_IS_COVERED), DropDownList)
            If moIsCoveredDrop IsNot Nothing Then
                PopulateBOProperty(State.MyRelatedEquipmentChildBO, "IsCoveredId", New Guid(moIsCoveredDrop.SelectedValue))
            End If
        End With
        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub

#End Region

#Region "Button Clicks"

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
            DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
        End Try
    End Sub

    Private Sub btnSave_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            PopulateBOsFormFrom()
            If State.MyBO.IsDirty OrElse State.MyBO.IsFamilyDirty Then
                State.MyBO.Save()
                State.HasDataChanged = True
                PopulateFormFromBOs()
                EnableDisableFields()
                DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
            Else
                DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", MSG_BTN_OK, MSG_TYPE_INFO)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnUndo_Write_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_Write.Click
        Try
            If Not State.MyBO.IsNew Then
                'Reload from the DB
                State.MyBO = New Equipment(State.MyBO.Id)
            ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                'It was a new with copy
                State.MyBO.Clone(State.ScreenSnapShotBO)
            Else
                State.MyBO = New Equipment
            End If
            PopulateFormFromBOs()
            EnableDisableFields()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnDelete_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete_WRITE.Click
        Try
            State.MyBO.Delete()
            State.MyBO.Save()
            State.HasDataChanged = True
            ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, State.MyBO, State.HasDataChanged))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            'undo the delete
            State.MyBO.RejectChanges()
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            PopulateBOsFormFrom()
            If State.MyBO.IsDirty Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                CreateNew()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnCopy_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnCopy_WRITE.Click
        Try
            PopulateBOsFormFrom()
            If State.MyBO.IsDirty Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            Else
                CreateNewWithCopy()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnRelatedEquipmentSelectChild_Write_Click(sender As System.Object, e As System.EventArgs) Handles btnRelatedEquipmentSelectChild_Write.Click
        Try
            ShowUserControl_on_RelatedEquipment(True)
            With UserControlSearchAvailableEquipment
                .dvSelectedEquipment = State.MyBO.GetRelatedEquipmentDV()
                .BindSelected(.dvSelectedEquipment)
            End With
            EnableDisableParentControls(False)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub


#End Region

#Region "Detail Grid Events"

    Public Sub ItemCreated(sender As System.Object, e As DataGridItemEventArgs)
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub


#Region "Images Grid"
    Private Sub DataGridImageDetail_ItemCreated(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridImageDetail.ItemCreated
        Try
            MyBase.BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Comment Grid"

    Private Sub DataGridCommentDetail_ItemCreated(sender As Object, e As DataGridItemEventArgs) Handles DataGridCommentDetail.ItemCreated
        Try
            MyBase.BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Public Sub DataGridCommentDetail_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles DataGridCommentDetail.ItemDataBound
        Try
            Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                e.Item.Cells(GRID_COL_EQUIPMENT_COMMENT_ID).Text = New Guid(CType(dvRow(Equipment.EquipmentCommentSelectionView.COL_NAME_EQUIPMENT_COMMENT_ID), Byte())).ToString
                e.Item.Cells(GRID_COL_COMMENT).Text = dvRow(Equipment.EquipmentCommentSelectionView.COL_NAME_COMMENT).ToString
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub DataGridCommentDetail_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles DataGridCommentDetail.SortCommand
        Try
            If State.SortExpressionCommentDetailGrid.StartsWith(e.SortExpression) Then
                If State.SortExpressionCommentDetailGrid.StartsWith(e.SortExpression & " DESC") Then
                    State.SortExpressionCommentDetailGrid = e.SortExpression
                Else
                    State.SortExpressionCommentDetailGrid = e.SortExpression & " DESC"
                End If
            Else
                State.SortExpressionCommentDetailGrid = e.SortExpression
            End If
            PopulateCommentDetailGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub DataGridCommentDetail_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles DataGridCommentDetail.ItemCommand
        Try
            If e.CommandName = "ViewRecord" Then
                State.IsCommentEditing = True
                State.CommentSelectedChildId = New Guid(e.Item.Cells(GRID_COL_EQUIPMENT_COMMENT_ID).Text)
                BeginCommentChildEdit()
                EnableDisableFields()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub DataGridCommentDetail_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles DataGridCommentDetail.PageIndexChanged
        Try
            State.CommentDetailPageIndex = e.NewPageIndex
            State.CommentSelectedChildId = Guid.Empty
            PopulateCommentDetailGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Related Equipment"
    Protected Sub GVRelatedEquipmentDetail_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GVRelatedEquipmentDetail.PageIndexChanging
        Try
            State.RelatedEquipmentDetailPageIndex = e.NewPageIndex
            State.RelatedEquipmentSelectedChildId = Guid.Empty
            PopulateRelatedEquipmentDetailGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub GVRelatedEquipmentDetail_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Try
            Dim nIndex As Integer
            If e.CommandName = EDIT_COMMAND_NAME Then
                nIndex = CInt(e.CommandArgument)
                State.RelatedEquipmentSelectedChildId = New Guid(CType(GVRelatedEquipmentDetail.Rows(nIndex).Cells(GRID_COL_RELATED_EQUIPMENT_ID).Controls(1), Label).Text)
                State.IsRelatedEquipmentEditing = True
                BeginRelatedEquipmentChildEdit()
                PopulateRelatedEquipmentDetailGrid()
                FillDropdownList(GVRelatedEquipmentDetail.Rows(nIndex))
                PopulateDetailFromRelatedEquipmentChildBO(GVRelatedEquipmentDetail.Rows(nIndex))
                SetGridControls(GVRelatedEquipmentDetail, False)
                EnableDisableFields()
            ElseIf e.CommandName = DELETE_COMMAND_NAME Then
                nIndex = CInt(e.CommandArgument)
                State.RelatedEquipmentSelectedChildId = New Guid(CType(GVRelatedEquipmentDetail.Rows(nIndex).Cells(GRID_COL_RELATED_EQUIPMENT_ID).Controls(1), Label).Text)
                State.IsRelatedEquipmentEditing = True
                BeginRelatedEquipmentChildEdit()
                EndRelatedEquipmentChildEdit(ElitaPlusPage.DetailPageCommand.Delete)
                PopulateRelatedEquipmentDetailGrid()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub


    Protected Sub GVRelatedEquipmentDetail_ItemCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            MyBase.BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub GVRelatedEquipmentDetail_Sorting(sender As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GVRelatedEquipmentDetail.Sorting
        Try
            If State.SortExpressionRelatedEquipmentDetailGrid.StartsWith(e.SortExpression) Then
                If State.SortExpressionRelatedEquipmentDetailGrid.StartsWith(e.SortExpression & " DESC") Then
                    State.SortExpressionRelatedEquipmentDetailGrid = e.SortExpression
                Else
                    State.SortExpressionRelatedEquipmentDetailGrid = e.SortExpression & " DESC"
                End If
            Else
                State.SortExpressionRelatedEquipmentDetailGrid = e.SortExpression
            End If
            PopulateRelatedEquipmentDetailGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub ShowUserControl_on_RelatedEquipment(attrVisible As Boolean)
        'show the user control
        RelatedEquipmentScroller.Visible = Not attrVisible

        'hide the Grid
        divSearchAvailableSelected.Visible = attrVisible

    End Sub

    Private Sub FillDropdownList(gRow As GridViewRow)

        'fill the drop downs
        Dim moInOemBoxDrop As DropDownList = DirectCast(gRow.Cells(GRID_COL_IN_OEM_BOX).FindControl(GRID_CONTROL_IS_IN_OEM_BOX), DropDownList)
        Dim moIsCoveredDrop As DropDownList = DirectCast(gRow.Cells(GRID_COL_IS_COVERED).FindControl(GRID_CONTROL_IS_COVERED), DropDownList)
        Dim languageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId

        With State.MyRelatedEquipmentChildBO
            If moInOemBoxDrop IsNot Nothing Then
                '  Me.BindListControlToDataView(moInOemBoxDrop, LookupListNew.GetYesNoLookupList(languageId), , , False)
                moInOemBoxDrop.Populate(CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions())
            End If
            If moIsCoveredDrop IsNot Nothing Then
                'Me.BindListControlToDataView(moIsCoveredDrop, LookupListNew.GetYesNoLookupList(languageId), , , False)
                moIsCoveredDrop.Populate(CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions())
            End If
        End With
    End Sub

#End Region

#End Region

#Region "Detail Clicks"

#Region "Comments"

    Private Sub btnAddNewCommentFromGrid_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnAddNewCommentFromGrid_WRITE.Click
        Try
            State.CommentSelectedChildId = Guid.Empty
            BeginCommentChildEdit()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnCommentBackChild_Click(sender As System.Object, e As System.EventArgs) Handles btnCommentBackChild.Click
        Try
            PopulateCommentChildBOFromDetail()
            If State.MyCommentChildBO.IsDirty Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Accept
            Else
                EndCommentChildEdit(ElitaPlusPage.DetailPageCommand.Back)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
            DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
        End Try
    End Sub

    Private Sub btnCommentCancelChild_Click(sender As Object, e As System.EventArgs) Handles btnCommentCancelChild.Click
        Try
            EndCommentChildEdit(ElitaPlusPage.DetailPageCommand.Cancel)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Private Sub btnCommentOkChild_Write_Click(sender As Object, e As System.EventArgs) Handles btnCommentOkChild_Write.Click
        Try
            EndCommentChildEdit(ElitaPlusPage.DetailPageCommand.OK)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnCommentDeleteChild_Write_Click(sender As Object, e As System.EventArgs) Handles btnCommentDeleteChild_Write.Click
        Try
            EndCommentChildEdit(ElitaPlusPage.DetailPageCommand.Delete)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Related Equipment"
    Private Sub btnRelatedEquipmentCancelChild_Write_Click(sender As Object, e As System.EventArgs) Handles btnRelatedEquipmentCancelChild_Write.Click

        Try
            SetGridControls(GVRelatedEquipmentDetail, False)
            EndRelatedEquipmentChildEdit(ElitaPlusPage.DetailPageCommand.Cancel)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Private Sub btnRelatedEquipmentOkChild_Write_Click(sender As Object, e As System.EventArgs) Handles btnRelatedEquipmentOkChild_Write.Click
        Try
            EndRelatedEquipmentChildEdit(ElitaPlusPage.DetailPageCommand.OK)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnSaveRelatedEquipmentChild_Click(sender As Object, e As System.EventArgs)
        Try
            State.RelatedEquipmentSelectedChildId = Guid.Empty
            BeginRelatedEquipmentChildEdit()
            SetGridControls(GVRelatedEquipmentDetail, False)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#End Region

#Region "Handle-Drop"

    Private Sub moIsMasterModelDrop_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles moIsMasterModelDrop.SelectedIndexChanged
        Try
            EnableDisableParentControls(True)
            PopulateMasterModelDropDown()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

    Private Sub moManufacturerDrop_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles moManufacturerDrop.SelectedIndexChanged
        Try
            PopulateMasterModelDropDown()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#Region "User Control Event Handler"
    Protected Sub ExecuteSearchFilter(sender As Object, args As SearchAvailableSelectedEventArgs) Handles UserControlSearchAvailableEquipment.ExecuteSearchFilter
        Dim equip As New Equipment
        Try
            args.dvAvailableEquipment = equip.ExecuteEquipmentListFilter(args.ManufactorerID, args.EquipmentClass, args.EquipmentType, args.Model, args.Description, State.MyBO.EquipmentTypeId)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub SaveClicked(sender As Object, args As SearchAvailableSelectedEventArgs) Handles UserControlSearchAvailableEquipment.EventSaveEquipmentListDetail
        Try
            ShowUserControl_on_RelatedEquipment(False)
            EnableDisableParentControls(True)
            RefreshEquipmentSelection(args.listSelectedEquipment)
            PopulateRelatedEquipmentDetailGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub CancelButtonClicked(sender As Object, args As SearchAvailableSelectedEventArgs) Handles UserControlSearchAvailableEquipment.EventCancelButtonClicked
        Try
            ShowUserControl_on_RelatedEquipment(False)
            EnableDisableParentControls(True)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub CustomPopulateDropDown(sender As Object, args As SearchAvailableSelectedEventArgs) Handles UserControlSearchAvailableEquipment.EventCustomPopulateDropDown
        Try
            If sender IsNot Nothing Then
                Dim cboEquipmenttype As DropDownList = CType(sender, DropDownList)
                args.dvmakeList = RelatedEquipment.GetEquipmentType(State.MyBO.EquipmentTypeId)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub


#End Region

    Private Function IsRelatedEquipmentEditing() As Boolean
        Throw New NotImplementedException
    End Function

    Private Sub RefreshEquipmentSelection(listSelectedEquipment As ArrayList)
        Dim NoId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "N")
        'add newly added equipment children
        For Each obj As Object In listSelectedEquipment
            Dim childEquipmentId As String = DirectCast(obj, String)
            Dim Childfound As Boolean = False
            For Each REChild As RelatedEquipment In State.MyBO.RelatedEquipmentChildren
                If REChild.ChildEquipmentId = New Guid(childEquipmentId) Then
                    Childfound = True
                End If
            Next
            If Not Childfound Then
                State.RelatedEquipmentSelectedChildId = Guid.Empty
                BeginRelatedEquipmentChildEdit()
                With State.MyRelatedEquipmentChildBO
                    .ChildEquipmentId = New Guid(childEquipmentId)
                    .IsInOemBoxId = NoId
                    .IsCoveredId = NoId
                    .Save()
                    .EndEdit()
                End With
            End If
        Next
        'remove that got deselected 
        For Each REChild As RelatedEquipment In State.MyBO.RelatedEquipmentChildren
            Dim Childfound As Boolean = False
            For Each obj As Object In listSelectedEquipment
                Dim childEquipmentId As String = DirectCast(obj, String)
                If REChild.ChildEquipmentId = New Guid(childEquipmentId) Then
                    Childfound = True
                End If
            Next
            If Not Childfound Then
                State.RelatedEquipmentSelectedChildId = REChild.Id
                BeginRelatedEquipmentChildEdit()
                EndRelatedEquipmentChildEdit(ElitaPlusPage.DetailPageCommand.Delete)
            End If
        Next
        State.IsRelatedEquipmentEditing = False
    End Sub

    Private Sub EquipmentForm_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        Dim strTemp As String = String.Empty

        hdnSelectedTab.Value = SelectedTabIndex

        If listDisabledTabs.Count > 0 Then
            For Each i As Integer In listDisabledTabs
                strTemp = strTemp + "," + i.ToString
            Next
            strTemp = strTemp.Substring(1) 'remove the first comma
            hdnDisabledTabs.Value = strTemp
        Else
            hdnDisabledTabs.Value = TAB_IMAGES
        End If

    End Sub
    Private Sub EnableTab(blnFlag As Boolean)
        Dim cnt As Integer
        If blnFlag = True Then 'enable - remove from disabled list
            listDisabledTabs.Clear()
        Else 'disable - add to the disabled list
            For cnt = 0 To TAB_TOTAL_COUNT - 1
                If listDisabledTabs.Contains(cnt) = False Then
                    listDisabledTabs.Add(cnt)
                End If
            Next
        End If
    End Sub
End Class
