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

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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
        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As Equipment, ByVal hasDataChanged As Boolean)
            Me.LastOperation = LastOp
            Me.EditingBo = curEditingBo
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

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                'Get the id from the parent
                Me.State.MyBO = New Equipment(CType(Me.CallingParameters, Guid))
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Page Events"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Try
            Me.MasterPage.MessageController.Clear_Hide()

            If Not Me.IsPostBack Then
                UpdateBreadCrum()
                'Date Calendars
                '  Me.MenuEnabled = False
                Me.AddControlMsg(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, True)
                Me.AddControlMsg(Me.btnCommentDeleteChild_Write, Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, True)
                'Me.AddControlMsg(Me.btnImageDeleteChild_Write, Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, True)
                If Me.State.MyBO Is Nothing Then
                    Me.State.MyBO = New Equipment
                End If
                PopulateDropdowns()
                'Def-27047:Added folloiwng code to cast Equipment BO to type IAttributable.
                AttributeValues.ParentBusinessObject = CType(Me.State.MyBO, IAttributable)
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
                AttributeValues.TranslateHeaders()
                ShowUserControl_on_RelatedEquipment(False)
                UserControlSearchAvailableEquipment.ShowCancelButton = True
            Else
                'Def-27047:Added folloiwng code to cast Equipment BO to type IAttributable.
                AttributeValues.ParentBusinessObject = CType(Me.State.MyBO, IAttributable)
                SelectedTabIndex = hdnSelectedTab.Value
            End If
            BindBoPropertiesToLabels()
            BindCommentsDetailBoPropertiesToLabels()
            BindImagesDetailBoPropertiesToLabels()
            CheckIfComingFromSaveConfirm()
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

    Private Sub UpdateBreadCrum()
        If (Not Me.State Is Nothing) Then
            If (Not Me.State Is Nothing) Then
                Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(PAGETITLE)
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
            End If
        End If
    End Sub

#Region "Controlling Logic"

    Protected Sub EnableDisableFields()
        If Me.State.IsCommentEditing Then
            ControlMgr.SetVisibleControl(Me, PanelCommentEditDetail, True)
            EnableDisableParentControls(False)
        ElseIf Me.State.IsImageEditing Then
            ControlMgr.SetVisibleControl(Me, PanelImageEditDetail, True)
            EnableDisableParentControls(False)
        ElseIf Me.State.IsRelatedEquipmentEditing Then
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
        If Me.State.MyBO.IsNew Then
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
        End If

        'WRITE YOU OWN CODE HERE
    End Sub

    Sub EnableDisableParentControls(ByVal enableToggle As Boolean)
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
        ControlMgr.SetEnableControl(Me, moManufacturerDrop, enableToggle And Me.State.MyBO.IsNew)
        ControlMgr.SetEnableControl(Me, moIsMasterModelDrop, enableToggle And Me.State.MyBO.IsNew)
        ControlMgr.SetEnableControl(Me, moModelText, enableToggle And ((Not isMasterEquipment And Me.State.MyBO.IsNew) Or (IsMasterEquipment)))
        ControlMgr.SetEnableControl(Me, moDescriptionText, enableToggle And ((Me.State.MyBO.IsNew And isMasterEquipment) Or (Not isMasterEquipment)))
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

    Sub EnableDisableRelatedEquipmentControl(ByVal enableToggle As Boolean)
        ControlMgr.SetEnableControl(Me, btnRelatedEquipmentCancelChild_Write, enableToggle)
        ControlMgr.SetEnableControl(Me, btnRelatedEquipmentOkChild_Write, enableToggle)
        ControlMgr.SetEnableControl(Me, btnRelatedEquipmentSelectChild_Write, Not enableToggle)
    End Sub

    Protected Sub BindBoPropertiesToLabels()
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ManufacturerId", Me.moManufacturerLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "IsMasterEquipment", Me.moIsMasterModelLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Model", Me.moModelLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Description", Me.moDescriptionLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "MasterEquipmentId", Me.moMasterEquipmentlLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ManufacturerWarrenty", Me.moManufacturerWarrentyLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "EquipmentClassId", Me.moEquipmentClassLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "EquipmentTypeId", Me.moEquipmentTypeLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "RepairableId", Me.moRepairableLabel)

        Me.BindBOPropertyToLabel(Me.State.MyBO, "Color", Me.lblColor)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Memory", Me.lblMemory)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Carrier", Me.lblCarrier)

        Me.ClearGridHeadersAndLabelsErrSign()
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
        With Me.State.MyBO
            PopulateCommentDetailGrid()
            PopulateImageDetailGrid()
            ' Populate Attributes
            AttributeValues.DataBind()

            'Me.SetGridItemStyleColor(GVRelatedEquipmentDetail)
            Me.TranslateGridHeader(Me.GVRelatedEquipmentDetail)
            PopulateRelatedEquipmentDetailGrid()

            Me.PopulateControlFromBOProperty(Me.moManufacturerDrop, .ManufacturerId)
            PopulateMasterModelDropDown()
            Me.PopulateControlFromBOProperty(Me.moIsMasterModelDrop, .IsMasterEquipment)
            Me.PopulateControlFromBOProperty(Me.moModelText, .Model)
            Me.PopulateControlFromBOProperty(Me.moDescriptionText, .Description)
            Me.PopulateControlFromBOProperty(Me.moMasterEquipmentDrop, .MasterEquipmentId)
            Me.PopulateControlFromBOProperty(Me.moManufacturerWarrentyText, .ManufacturerWarrenty)
            Me.PopulateControlFromBOProperty(Me.moEquipmentClassDrop, .EquipmentClassId)
            Me.PopulateControlFromBOProperty(Me.moEquipmentTypeDrop, .EquipmentTypeId)
            Me.PopulateControlFromBOProperty(Me.moRepairableDrop, .RepairableId)
            
            BindSelectItem(.Color, me.moColor)
            BindSelectItem(.Memory, me.moMemory)
            BindSelectItem(.Carrier, me.moCarrier)

            'Me.PopulateControlFromBOProperty(Me.moColor, .Color)
            'Me.PopulateControlFromBOProperty(Me.moMemory, .Memory)
            'Me.PopulateControlFromBOProperty(Me.moCarrier, .Carrier)

        End With

    End Sub

    Sub PopulateCommentDetailGrid()
        'This is a temporary Binding Logic. BEGIN        
        Dim dv As Equipment.EquipmentCommentSelectionView = Me.State.MyBO.GetCommentSelectionView()
        dv.Sort = Me.State.SortExpressionCommentDetailGrid

        Me.DataGridCommentDetail.Columns(Me.GRID_COL_COMMENT).SortExpression = Equipment.EquipmentCommentSelectionView.COL_NAME_COMMENT
        Me.SetGridItemStyleColor(Me.DataGridCommentDetail)

        SetPageAndSelectedIndexFromGuid(dv, Me.State.CommentSelectedChildId, Me.DataGridCommentDetail, Me.State.CommentDetailPageIndex)
        Me.State.CommentDetailPageIndex = Me.DataGridCommentDetail.CurrentPageIndex

        Me.DataGridCommentDetail.DataSource = dv
        Me.DataGridCommentDetail.AutoGenerateColumns = False
        Me.DataGridCommentDetail.DataBind()
        'This is a temporary Binding Logic. END
    End Sub

    Sub PopulateImageDetailGrid()
        'This is a temporary Binding Logic. BEGIN        
        Dim dv As Equipment.EquipmentImageSelectionView = Me.State.MyBO.GetImageSelectionView()
        dv.Sort = Me.State.SortExpressionImageDetailGrid

        Me.DataGridImageDetail.Columns(Me.GRID_COL_CODE).SortExpression = Equipment.EquipmentImageSelectionView.COL_NAME_CODE
        Me.DataGridImageDetail.Columns(Me.GRID_COL_DESCRIPTION).SortExpression = Equipment.EquipmentImageSelectionView.COL_NAME_DESCRIPTION
        Me.DataGridImageDetail.Columns(Me.GRID_COL_IMAGE_TYPE).SortExpression = Equipment.EquipmentImageSelectionView.COL_NAME_IMAGE_TYPE
        Me.SetGridItemStyleColor(Me.DataGridImageDetail)

        SetPageAndSelectedIndexFromGuid(dv, Me.State.ImageSelectedChildId, Me.DataGridImageDetail, Me.State.ImageDetailPageIndex)
        Me.State.ImageDetailPageIndex = Me.DataGridImageDetail.CurrentPageIndex

        Me.DataGridImageDetail.DataSource = dv
        Me.DataGridImageDetail.AutoGenerateColumns = False
        Me.DataGridImageDetail.DataBind()
        'This is a temporary Binding Logic. END
    End Sub

    Sub PopulateRelatedEquipmentDetailGrid()
        'This is a temporary Binding Logic . BEGIN
        Dim dv As Equipment.RelatedEquipmentSelectionView = Me.State.MyBO.GetEquipmentSelectionView()
        dv.Sort = Me.State.SortExpressionRelatedEquipmentDetailGrid
        'also set the related equipment id in the grid
        Me.GVRelatedEquipmentDetail.Columns(Me.GRID_COL_EQUIPMENT_TYPE).SortExpression = Equipment.RelatedEquipmentSelectionView.COL_NAME_EQUIPMENT_TYPE
        Me.GVRelatedEquipmentDetail.Columns(Me.GRID_COL_EQUIPMENT_DESCRIPTION).SortExpression = Equipment.RelatedEquipmentSelectionView.COL_NAME_DESCRIPTION
        Me.GVRelatedEquipmentDetail.Columns(Me.GRID_COL_MANUFACTURER).SortExpression = Equipment.RelatedEquipmentSelectionView.COL_NAME_MANUFACTURER
        Me.GVRelatedEquipmentDetail.Columns(Me.GRID_COL_MODEL).SortExpression = Equipment.RelatedEquipmentSelectionView.COL_NAME_MODEL
        Me.GVRelatedEquipmentDetail.Columns(Me.GRID_COL_IN_OEM_BOX).SortExpression = Equipment.RelatedEquipmentSelectionView.COL_NAME_IN_OEM_BOX
        Me.GVRelatedEquipmentDetail.Columns(Me.GRID_COL_IS_COVERED).SortExpression = Equipment.RelatedEquipmentSelectionView.COL_NAME_IS_COVERED
        Me.SetGridItemStyleColor(Me.GVRelatedEquipmentDetail)

        If Me.State.IsRelatedEquipmentEditing Then
            Me.SetPageAndSelectedIndexFromGuid(dv, Me.State.RelatedEquipmentSelectedChildId, GVRelatedEquipmentDetail,
                                    GVRelatedEquipmentDetail.PageIndex, True)
        Else
            SetPageAndSelectedIndexFromGuid(dv, Me.State.RelatedEquipmentSelectedChildId, Me.GVRelatedEquipmentDetail, Me.State.RelatedEquipmentDetailPageIndex)
            Me.State.RelatedEquipmentDetailPageIndex = Me.GVRelatedEquipmentDetail.PageIndex
        End If

        If dv.Count > 0 Then
            Me.GVRelatedEquipmentDetail.DataSource = dv
            Me.GVRelatedEquipmentDetail.AutoGenerateColumns = False
            Me.GVRelatedEquipmentDetail.DataBind()
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
        With Me.State.MyBO
            Me.PopulateBOProperty(Me.State.MyBO, "ManufacturerId", Me.moManufacturerDrop)
            Me.PopulateBOProperty(Me.State.MyBO, "IsMasterEquipment", Me.moIsMasterModelDrop)
            Me.PopulateBOProperty(Me.State.MyBO, "Model", Me.moModelText)
            Me.PopulateBOProperty(Me.State.MyBO, "Description", Me.moDescriptionText)
            Me.PopulateBOProperty(Me.State.MyBO, "MasterEquipmentId", Me.moMasterEquipmentDrop)
            Me.PopulateBOProperty(Me.State.MyBO, "EquipmentClassId", Me.moEquipmentClassDrop)
            Me.PopulateBOProperty(Me.State.MyBO, "EquipmentTypeId", Me.moEquipmentTypeDrop)
            Me.PopulateBOProperty(Me.State.MyBO, "RepairableId", Me.moRepairableDrop)
            Me.PopulateBOProperty(Me.State.MyBO, "ManufacturerWarrenty", Me.moManufacturerWarrentyText)
            
            Me.PopulateBOProperty(Me.State.MyBO, "Color", Me.moColor,False,True)
            Me.PopulateBOProperty(Me.State.MyBO, "Memory", Me.moMemory,False,True)                
            Me.PopulateBOProperty(Me.State.MyBO, "Carrier", moCarrier ,False,True)
            
        End With
        If Me.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub

    Protected Sub CreateNew()
        Me.State.ScreenSnapShotBO = Nothing 'Reset the backup copy
        Me.State.MyBO = New Equipment
        Me.PopulateFormFromBOs()
        Me.EnableDisableFields()
    End Sub

    Protected Sub CreateNewWithCopy()
        Dim newObj As New Equipment
        newObj.Copy(Me.State.MyBO)

        Me.State.MyBO = newObj
        Me.PopulateFormFromBOs()
        Me.EnableDisableFields()

        'create the backup copy
        Me.State.ScreenSnapShotBO = New Equipment
        Me.State.ScreenSnapShotBO.Copy(Me.State.MyBO)
    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
        If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
            If Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr AndAlso Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.Accept Then
                Me.State.MyBO.Save()
            End If
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    Me.CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    Me.CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    Me.ReturnToCallingPage(New ReturnType(Me.State.ActionInProgress, Me.State.MyBO, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.Accept
                    If (Me.State.IsCommentEditing) Then
                        Me.PopulateCommentChildBOFromDetail()
                        Me.State.MyCommentChildBO.Save()
                        Me.State.MyCommentChildBO.EndEdit()
                        Me.State.IsCommentEditing = False
                    ElseIf (Me.State.IsImageEditing) Then
                        Me.PopulateImageChildBOFromDetail()
                        Me.State.MyImageChildBO.Save()
                        Me.State.MyImageChildBO.EndEdit()
                        Me.State.IsImageEditing = False
                    ElseIf (Me.State.IsRelatedEquipmentEditing) Then
                        Me.PopulateRelatedEquipmentChildBOFromDetail()
                        Me.State.MyRelatedEquipmentChildBO.Save()
                        Me.State.MyRelatedEquipmentChildBO.EndEdit()
                    End If
                    Me.EnableDisableFields()
                    Me.PopulateCommentDetailGrid()
                    Me.PopulateImageDetailGrid()
                    AttributeValues.DataBind()
                    Me.PopulateRelatedEquipmentDetailGrid()
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
                    Me.HandleErrors(New Exception(Me.State.LastErrMsg), Me.MasterPage.MessageController)

                Case ElitaPlusPage.DetailPageCommand.Accept
                    If (Me.State.IsCommentEditing) Then
                        Me.State.MyCommentChildBO.cancelEdit()
                        If Me.State.MyCommentChildBO.IsSaveNew Then
                            Me.State.MyCommentChildBO.Delete()
                            Me.State.MyCommentChildBO.Save()
                        End If
                        Me.State.IsCommentEditing = False
                    ElseIf (Me.State.IsImageEditing) Then
                        Me.State.MyImageChildBO.cancelEdit()
                        If Me.State.MyCommentChildBO.IsSaveNew Then
                            Me.State.MyImageChildBO.Delete()
                            Me.State.MyImageChildBO.Save()
                        End If
                        Me.State.IsImageEditing = False
                    ElseIf (Me.State.IsRelatedEquipmentEditing) Then
                        Me.State.MyRelatedEquipmentChildBO.cancelEdit()
                        If Me.State.MyRelatedEquipmentChildBO.IsSaveNew Then
                            Me.State.MyRelatedEquipmentChildBO.Delete()
                            Me.State.MyRelatedEquipmentChildBO.Save()
                        End If
                        Me.State.IsRelatedEquipmentEditing = False
                    End If
                    Me.EnableDisableFields()
                    Me.PopulateCommentDetailGrid()
                    Me.PopulateImageDetailGrid()
                    AttributeValues.DataBind()
                    Me.PopulateRelatedEquipmentDetailGrid()
            End Select
        End If
        'Clean after consuming the action
        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        Me.HiddenSaveChangesPromptResponse.Value = ""
    End Sub

    Sub BeginCommentChildEdit()
        Me.State.IsCommentEditing = True
        Me.EnableDisableFields()
        With Me.State
            If Not .CommentSelectedChildId.Equals(Guid.Empty) Then
                .MyCommentChildBO = .MyBO.GetCommentChild(.CommentSelectedChildId)
            Else
                .MyCommentChildBO = .MyBO.GetNewCommentChild
            End If
            .MyCommentChildBO.BeginEdit()
        End With
        Me.PopulateDetailFromCommentChildBO()
    End Sub

    Sub BeginImageChildEdit()
        Me.State.IsImageEditing = True
        Me.EnableDisableFields()
        With Me.State
            If Not .ImageSelectedChildId.Equals(Guid.Empty) Then
                .MyImageChildBO = .MyBO.GetImageChild(.ImageSelectedChildId)
            Else
                .MyImageChildBO = .MyBO.GetNewImageChild
            End If
            .MyImageChildBO.BeginEdit()
        End With
        Me.PopulateDetailFromImageChildBO()
    End Sub

    Sub EndCommentChildEdit(ByVal lastop As ElitaPlusPage.DetailPageCommand)
        Try
            With Me.State
                Select Case lastop
                    Case ElitaPlusPage.DetailPageCommand.OK
                        Me.PopulateCommentChildBOFromDetail()
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
            Me.State.IsCommentEditing = False
            Me.EnableDisableFields()
            Me.PopulateCommentDetailGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Sub EndImageChildEdit(ByVal lastop As ElitaPlusPage.DetailPageCommand)
        Try
            With Me.State
                Select Case lastop
                    Case ElitaPlusPage.DetailPageCommand.OK
                        Me.PopulateImageChildBOFromDetail()
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
            Me.State.IsImageEditing = False
            Me.EnableDisableFields()
            Me.PopulateImageDetailGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Sub EndRelatedEquipmentChildEdit(ByVal lastop As ElitaPlusPage.DetailPageCommand)
        Try
            With Me.State
                Select Case lastop
                    Case ElitaPlusPage.DetailPageCommand.OK
                        Me.PopulateRelatedEquipmentChildBOFromDetail()
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
            Me.State.IsRelatedEquipmentEditing = False
            Me.EnableDisableFields()
            Me.PopulateRelatedEquipmentDetailGrid()
            EnableDisableRelatedEquipmentControl(False)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Sub PopulateDetailFromCommentChildBO()
        With Me.State.MyCommentChildBO
            Me.PopulateControlFromBOProperty(Me.moCommentTextBox, .Comment)
        End With
    End Sub

    Sub PopulateDetailFromImageChildBO()
        With Me.State.MyImageChildBO
            'Throw New NotImplementedException()
        End With
    End Sub

    Sub PopulateImageChildBOFromDetail()
        With Me.State.MyImageChildBO
            'Throw New NotImplementedException 
        End With
        If Me.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub

    Sub PopulateCommentChildBOFromDetail()
        With Me.State.MyCommentChildBO
            Me.PopulateBOProperty(Me.State.MyCommentChildBO, "Comment", Me.moCommentTextBox)
        End With
        If Me.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub

    Protected Sub BindCommentsDetailBoPropertiesToLabels()
        With Me.State
            Me.BindBOPropertyToLabel(.MyCommentChildBO, "Comment", Me.moCommentLabel)
        End With
        Me.ClearGridHeadersAndLabelsErrSign()
    End Sub

    Protected Sub BindImagesDetailBoPropertiesToLabels()
        With Me.State
            'Throw New NotImplementedException 
        End With
        Me.ClearGridHeadersAndLabelsErrSign()
    End Sub

    Protected Sub BindRelatedEquipmentDetailBoPropertiesToLabels()
        With Me.State
            'Throw New NotImplementedException 
        End With
        Me.ClearGridHeadersAndLabelsErrSign()
    End Sub

    Sub BeginRelatedEquipmentChildEdit()
        Me.State.IsRelatedEquipmentEditing = True
        ''Me.EnableDisableFields()
        With Me.State
            If Not .RelatedEquipmentSelectedChildId.Equals(Guid.Empty) Then
                .MyRelatedEquipmentChildBO = .MyBO.GetRelatedEquipmentChild(.RelatedEquipmentSelectedChildId)
            Else
                .MyRelatedEquipmentChildBO = .MyBO.GetNewRelatedEquipmentChild
            End If
            .MyRelatedEquipmentChildBO.BeginEdit()
        End With
    End Sub

    Sub PopulateDetailFromRelatedEquipmentChildBO(ByVal gRow As GridViewRow)

        'fill the drop downs
        Dim moInOemBoxDrop As DropDownList = CType(gRow.Cells(Me.GRID_COL_IN_OEM_BOX).FindControl(Me.GRID_CONTROL_IS_IN_OEM_BOX), DropDownList)
        Dim moIsCoveredDrop As DropDownList = CType(gRow.Cells(Me.GRID_COL_IS_COVERED).FindControl(Me.GRID_CONTROL_IS_COVERED), DropDownList)
        Dim languageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId

        With Me.State.MyRelatedEquipmentChildBO
            If Not moInOemBoxDrop Is Nothing Then
                Me.PopulateControlFromBOProperty(moInOemBoxDrop, .IsInOemBoxId)
            End If
            If Not moIsCoveredDrop Is Nothing Then
                Me.PopulateControlFromBOProperty(moIsCoveredDrop, .IsCoveredId)
            End If
        End With
    End Sub

    Sub PopulateRelatedEquipmentChildBOFromDetail()
        With Me.State.MyRelatedEquipmentChildBO
            Dim moInOemBoxDrop As DropDownList = CType(Me.GVRelatedEquipmentDetail.Rows(Me.GVRelatedEquipmentDetail.EditIndex).Cells(Me.GRID_COL_IN_OEM_BOX).FindControl(Me.GRID_CONTROL_IS_IN_OEM_BOX), DropDownList)
            If Not moInOemBoxDrop Is Nothing Then
                Me.PopulateBOProperty(Me.State.MyRelatedEquipmentChildBO, "IsInOemBoxId", New Guid(moInOemBoxDrop.SelectedValue))
            End If
            Dim moIsCoveredDrop As DropDownList = CType(Me.GVRelatedEquipmentDetail.Rows(Me.GVRelatedEquipmentDetail.EditIndex).Cells(Me.GRID_COL_IS_COVERED).FindControl(Me.GRID_CONTROL_IS_COVERED), DropDownList)
            If Not moIsCoveredDrop Is Nothing Then
                Me.PopulateBOProperty(Me.State.MyRelatedEquipmentChildBO, "IsCoveredId", New Guid(moIsCoveredDrop.SelectedValue))
            End If
        End With
        If Me.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub

#End Region

#Region "Button Clicks"

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
            Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
        End Try
    End Sub

    Private Sub btnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            Me.PopulateBOsFormFrom()
            If Me.State.MyBO.IsDirty OrElse Me.State.MyBO.IsFamilyDirty Then
                Me.State.MyBO.Save()
                Me.State.HasDataChanged = True
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
                Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
            Else
                Me.DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnUndo_Write_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_Write.Click
        Try
            If Not Me.State.MyBO.IsNew Then
                'Reload from the DB
                Me.State.MyBO = New Equipment(Me.State.MyBO.Id)
            ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                'It was a new with copy
                Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
            Else
                Me.State.MyBO = New Equipment
            End If
            Me.PopulateFormFromBOs()
            Me.EnableDisableFields()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnDelete_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click
        Try
            Me.State.MyBO.Delete()
            Me.State.MyBO.Save()
            Me.State.HasDataChanged = True
            Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, Me.State.MyBO, Me.State.HasDataChanged))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            'undo the delete
            Me.State.MyBO.RejectChanges()
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnNew_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            Me.PopulateBOsFormFrom()
            If Me.State.MyBO.IsDirty Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                Me.CreateNew()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnCopy_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy_WRITE.Click
        Try
            Me.PopulateBOsFormFrom()
            If Me.State.MyBO.IsDirty Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            Else
                Me.CreateNewWithCopy()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnRelatedEquipmentSelectChild_Write_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRelatedEquipmentSelectChild_Write.Click
        Try
            ShowUserControl_on_RelatedEquipment(True)
            With UserControlSearchAvailableEquipment
                .dvSelectedEquipment = Me.State.MyBO.GetRelatedEquipmentDV()
                .BindSelected(.dvSelectedEquipment)
            End With
            EnableDisableParentControls(False)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub


#End Region

#Region "Detail Grid Events"

    Public Sub ItemCreated(ByVal sender As System.Object, ByVal e As DataGridItemEventArgs)
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub


#Region "Images Grid"
    Private Sub DataGridImageDetail_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridImageDetail.ItemCreated
        Try
            MyBase.BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Comment Grid"

    Private Sub DataGridCommentDetail_ItemCreated(ByVal sender As Object, ByVal e As DataGridItemEventArgs) Handles DataGridCommentDetail.ItemCreated
        Try
            MyBase.BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Public Sub DataGridCommentDetail_ItemDataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs) Handles DataGridCommentDetail.ItemDataBound
        Try
            Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                e.Item.Cells(Me.GRID_COL_EQUIPMENT_COMMENT_ID).Text = New Guid(CType(dvRow(Equipment.EquipmentCommentSelectionView.COL_NAME_EQUIPMENT_COMMENT_ID), Byte())).ToString
                e.Item.Cells(Me.GRID_COL_COMMENT).Text = dvRow(Equipment.EquipmentCommentSelectionView.COL_NAME_COMMENT).ToString
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub DataGridCommentDetail_SortCommand(ByVal source As Object, ByVal e As DataGridSortCommandEventArgs) Handles DataGridCommentDetail.SortCommand
        Try
            If Me.State.SortExpressionCommentDetailGrid.StartsWith(e.SortExpression) Then
                If Me.State.SortExpressionCommentDetailGrid.StartsWith(e.SortExpression & " DESC") Then
                    Me.State.SortExpressionCommentDetailGrid = e.SortExpression
                Else
                    Me.State.SortExpressionCommentDetailGrid = e.SortExpression & " DESC"
                End If
            Else
                Me.State.SortExpressionCommentDetailGrid = e.SortExpression
            End If
            Me.PopulateCommentDetailGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub DataGridCommentDetail_ItemCommand(ByVal source As Object, ByVal e As DataGridCommandEventArgs) Handles DataGridCommentDetail.ItemCommand
        Try
            If e.CommandName = "ViewRecord" Then
                Me.State.IsCommentEditing = True
                Me.State.CommentSelectedChildId = New Guid(e.Item.Cells(Me.GRID_COL_EQUIPMENT_COMMENT_ID).Text)
                Me.BeginCommentChildEdit()
                Me.EnableDisableFields()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub DataGridCommentDetail_PageIndexChanged(ByVal source As Object, ByVal e As DataGridPageChangedEventArgs) Handles DataGridCommentDetail.PageIndexChanged
        Try
            Me.State.CommentDetailPageIndex = e.NewPageIndex
            Me.State.CommentSelectedChildId = Guid.Empty
            Me.PopulateCommentDetailGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Related Equipment"
    Protected Sub GVRelatedEquipmentDetail_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GVRelatedEquipmentDetail.PageIndexChanging
        Try
            Me.State.RelatedEquipmentDetailPageIndex = e.NewPageIndex
            Me.State.RelatedEquipmentSelectedChildId = Guid.Empty
            Me.PopulateRelatedEquipmentDetailGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub GVRelatedEquipmentDetail_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Try
            Dim nIndex As Integer
            If e.CommandName = EDIT_COMMAND_NAME Then
                nIndex = CInt(e.CommandArgument)
                Me.State.RelatedEquipmentSelectedChildId = New Guid(CType(Me.GVRelatedEquipmentDetail.Rows(nIndex).Cells(Me.GRID_COL_RELATED_EQUIPMENT_ID).Controls(1), Label).Text)
                Me.State.IsRelatedEquipmentEditing = True
                Me.BeginRelatedEquipmentChildEdit()
                PopulateRelatedEquipmentDetailGrid()
                Me.FillDropdownList(GVRelatedEquipmentDetail.Rows(nIndex))
                Me.PopulateDetailFromRelatedEquipmentChildBO(GVRelatedEquipmentDetail.Rows(nIndex))
                Me.SetGridControls(GVRelatedEquipmentDetail, False)
                Me.EnableDisableFields()
            ElseIf e.CommandName = DELETE_COMMAND_NAME Then
                nIndex = CInt(e.CommandArgument)
                Me.State.RelatedEquipmentSelectedChildId = New Guid(CType(Me.GVRelatedEquipmentDetail.Rows(nIndex).Cells(Me.GRID_COL_RELATED_EQUIPMENT_ID).Controls(1), Label).Text)
                Me.State.IsRelatedEquipmentEditing = True
                BeginRelatedEquipmentChildEdit()
                EndRelatedEquipmentChildEdit(ElitaPlusPage.DetailPageCommand.Delete)
                PopulateRelatedEquipmentDetailGrid()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub


    Protected Sub GVRelatedEquipmentDetail_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        Try
            MyBase.BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub GVRelatedEquipmentDetail_Sorting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles GVRelatedEquipmentDetail.Sorting
        Try
            If Me.State.SortExpressionRelatedEquipmentDetailGrid.StartsWith(e.SortExpression) Then
                If Me.State.SortExpressionRelatedEquipmentDetailGrid.StartsWith(e.SortExpression & " DESC") Then
                    Me.State.SortExpressionRelatedEquipmentDetailGrid = e.SortExpression
                Else
                    Me.State.SortExpressionRelatedEquipmentDetailGrid = e.SortExpression & " DESC"
                End If
            Else
                Me.State.SortExpressionRelatedEquipmentDetailGrid = e.SortExpression
            End If
            Me.PopulateRelatedEquipmentDetailGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub ShowUserControl_on_RelatedEquipment(ByVal attrVisible As Boolean)
        'show the user control
        RelatedEquipmentScroller.Visible = Not attrVisible

        'hide the Grid
        divSearchAvailableSelected.Visible = attrVisible

    End Sub

    Private Sub FillDropdownList(ByVal gRow As GridViewRow)

        'fill the drop downs
        Dim moInOemBoxDrop As DropDownList = DirectCast(gRow.Cells(Me.GRID_COL_IN_OEM_BOX).FindControl(Me.GRID_CONTROL_IS_IN_OEM_BOX), DropDownList)
        Dim moIsCoveredDrop As DropDownList = DirectCast(gRow.Cells(Me.GRID_COL_IS_COVERED).FindControl(Me.GRID_CONTROL_IS_COVERED), DropDownList)
        Dim languageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId

        With Me.State.MyRelatedEquipmentChildBO
            If Not moInOemBoxDrop Is Nothing Then
                '  Me.BindListControlToDataView(moInOemBoxDrop, LookupListNew.GetYesNoLookupList(languageId), , , False)
                moInOemBoxDrop.Populate(CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions())
            End If
            If Not moIsCoveredDrop Is Nothing Then
                'Me.BindListControlToDataView(moIsCoveredDrop, LookupListNew.GetYesNoLookupList(languageId), , , False)
                moIsCoveredDrop.Populate(CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions())
            End If
        End With
    End Sub

#End Region

#End Region

#Region "Detail Clicks"

#Region "Comments"

    Private Sub btnAddNewCommentFromGrid_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddNewCommentFromGrid_WRITE.Click
        Try
            Me.State.CommentSelectedChildId = Guid.Empty
            Me.BeginCommentChildEdit()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnCommentBackChild_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCommentBackChild.Click
        Try
            Me.PopulateCommentChildBOFromDetail()
            If Me.State.MyCommentChildBO.IsDirty Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Accept
            Else
                Me.EndCommentChildEdit(ElitaPlusPage.DetailPageCommand.Back)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
            Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
        End Try
    End Sub

    Private Sub btnCommentCancelChild_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCommentCancelChild.Click
        Try
            Me.EndCommentChildEdit(ElitaPlusPage.DetailPageCommand.Cancel)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub btnCommentOkChild_Write_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCommentOkChild_Write.Click
        Try
            Me.EndCommentChildEdit(ElitaPlusPage.DetailPageCommand.OK)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnCommentDeleteChild_Write_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCommentDeleteChild_Write.Click
        Try
            Me.EndCommentChildEdit(ElitaPlusPage.DetailPageCommand.Delete)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Related Equipment"
    Private Sub btnRelatedEquipmentCancelChild_Write_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRelatedEquipmentCancelChild_Write.Click

        Try
            Me.SetGridControls(GVRelatedEquipmentDetail, False)
            Me.EndRelatedEquipmentChildEdit(ElitaPlusPage.DetailPageCommand.Cancel)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub btnRelatedEquipmentOkChild_Write_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRelatedEquipmentOkChild_Write.Click
        Try
            Me.EndRelatedEquipmentChildEdit(ElitaPlusPage.DetailPageCommand.OK)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnSaveRelatedEquipmentChild_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Me.State.RelatedEquipmentSelectedChildId = Guid.Empty
            Me.BeginRelatedEquipmentChildEdit()
            Me.SetGridControls(GVRelatedEquipmentDetail, False)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#End Region

#Region "Handle-Drop"

    Private Sub moIsMasterModelDrop_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moIsMasterModelDrop.SelectedIndexChanged
        Try
            EnableDisableParentControls(True)
            PopulateMasterModelDropDown()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

    Private Sub moManufacturerDrop_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles moManufacturerDrop.SelectedIndexChanged
        Try
            PopulateMasterModelDropDown()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#Region "User Control Event Handler"
    Protected Sub ExecuteSearchFilter(ByVal sender As Object, ByVal args As SearchAvailableSelectedEventArgs) Handles UserControlSearchAvailableEquipment.ExecuteSearchFilter
        Dim equip As New Equipment
        Try
            args.dvAvailableEquipment = equip.ExecuteEquipmentListFilter(args.ManufactorerID, args.EquipmentClass, args.EquipmentType, args.Model, args.Description, Me.State.MyBO.EquipmentTypeId)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub SaveClicked(ByVal sender As Object, ByVal args As SearchAvailableSelectedEventArgs) Handles UserControlSearchAvailableEquipment.EventSaveEquipmentListDetail
        Try
            ShowUserControl_on_RelatedEquipment(False)
            EnableDisableParentControls(True)
            RefreshEquipmentSelection(args.listSelectedEquipment)
            PopulateRelatedEquipmentDetailGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub CancelButtonClicked(ByVal sender As Object, ByVal args As SearchAvailableSelectedEventArgs) Handles UserControlSearchAvailableEquipment.EventCancelButtonClicked
        Try
            ShowUserControl_on_RelatedEquipment(False)
            EnableDisableParentControls(True)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub CustomPopulateDropDown(ByVal sender As Object, ByVal args As SearchAvailableSelectedEventArgs) Handles UserControlSearchAvailableEquipment.EventCustomPopulateDropDown
        Try
            If Not sender Is Nothing Then
                Dim cboEquipmenttype As DropDownList = CType(sender, DropDownList)
                args.dvmakeList = RelatedEquipment.GetEquipmentType(Me.State.MyBO.EquipmentTypeId)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub


#End Region

    Private Function IsRelatedEquipmentEditing() As Boolean
        Throw New NotImplementedException
    End Function

    Private Sub RefreshEquipmentSelection(ByVal listSelectedEquipment As ArrayList)
        Dim NoId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "N")
        'add newly added equipment children
        For Each obj As Object In listSelectedEquipment
            Dim childEquipmentId As String = DirectCast(obj, String)
            Dim Childfound As Boolean = False
            For Each REChild As RelatedEquipment In Me.State.MyBO.RelatedEquipmentChildren
                If REChild.ChildEquipmentId = New Guid(childEquipmentId) Then
                    Childfound = True
                End If
            Next
            If Not Childfound Then
                Me.State.RelatedEquipmentSelectedChildId = Guid.Empty
                BeginRelatedEquipmentChildEdit()
                With Me.State.MyRelatedEquipmentChildBO
                    .ChildEquipmentId = New Guid(childEquipmentId)
                    .IsInOemBoxId = NoId
                    .IsCoveredId = NoId
                    .Save()
                    .EndEdit()
                End With
            End If
        Next
        'remove that got deselected 
        For Each REChild As RelatedEquipment In Me.State.MyBO.RelatedEquipmentChildren
            Dim Childfound As Boolean = False
            For Each obj As Object In listSelectedEquipment
                Dim childEquipmentId As String = DirectCast(obj, String)
                If REChild.ChildEquipmentId = New Guid(childEquipmentId) Then
                    Childfound = True
                End If
            Next
            If Not Childfound Then
                Me.State.RelatedEquipmentSelectedChildId = REChild.Id
                BeginRelatedEquipmentChildEdit()
                EndRelatedEquipmentChildEdit(ElitaPlusPage.DetailPageCommand.Delete)
            End If
        Next
        Me.State.IsRelatedEquipmentEditing = False
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
    Private Sub EnableTab(ByVal blnFlag As Boolean)
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
