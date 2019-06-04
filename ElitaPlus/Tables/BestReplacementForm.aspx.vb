Option Strict On

Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Public Class BestReplacementForm
    Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Constants"
    Public Const URL As String = "BestReplacementForm.aspx"

    Public Const GRID_COL_BEST_REPLACEMENT_ID As Integer = 0
    Public Const GRID_COL_MANUFACTURER As Integer = 2
    Public Const GRID_COL_MODEL As Integer = 3
    Public Const GRID_COL_REPLACEMENT_MANUFACTURER As Integer = 4
    Public Const GRID_COL_REPLACEMENT_MODEL As Integer = 5
    Public Const GRID_COL_PRIORITY As Integer = 6

#End Region

#Region "Page State"

    Class MyState
        Public MyBO As BestReplacementGroup
        Public MyChildBO As BestReplacement
        Public ScreenSnapShotBO As BestReplacementGroup
        Public ScreenSnapShotChildBO As BestReplacement
        Public IsACopy As Boolean
        Public IsChildEditing As Boolean = False
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public LastErrMsg As String
        Public HasDataChanged As Boolean
        Public SelectedChildId As Guid = Guid.Empty
        Public DetailPageIndex As Integer = 0
        Public SortExpressionDetailGrid As String = BestReplacementGroup.BestReplacementSelectionView.COL_NAME_MANUFACTURER &
            ", " & BestReplacementGroup.BestReplacementSelectionView.COL_NAME_MODEL & ", " & BestReplacementGroup.BestReplacementSelectionView.COL_NAME_PRIORITY
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

#Region "Page Events"
    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                'Get the id from the parent
                Me.State.MyBO = New BestReplacementGroup(CType(Me.CallingParameters, Guid))
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try

    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'Put user code to initialize the page here
            Me.ErrorCtrl.Clear_Hide()

            If Not Me.IsPostBack Then
                Me.MenuEnabled = False
                Me.AddConfirmation(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT)
                Me.AddControlMsg(Me.btnDeleteChild_Write, Message.DELETE_RECORD_PROMPT, "", ElitaPlusPage.MSG_BTN_YES_NO, ElitaPlusPage.MSG_TYPE_CONFIRM, True)
                If Me.State.MyBO Is Nothing Then
                    Me.State.MyBO = New BestReplacementGroup
                End If
                Me.PopulateDropdowns()
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
            End If
            BindDetailBoPropertiesToLabels()
            BindBoPropertiesToLabels()
            CheckIfComingFromSaveConfirm()
            If Not Me.IsPostBack Then
                Me.AddLabelDecorations(Me.State.MyBO)
                Me.AddLabelDecorations(New BestReplacement())
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
        Me.ShowMissingTranslations(Me.ErrorCtrl)
    End Sub
#End Region

#Region "Button Clicks"
    Private Sub btnUndo_Write_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_Write.Click
        Try
            If Not Me.State.MyBO.IsNew Then
                'Reload from the DB
                Me.State.MyBO = New BestReplacementGroup(Me.State.MyBO.Id)
            ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                'It was a new with copy
                Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
            Else
                Me.State.MyBO = New BestReplacementGroup
            End If
            Me.PopulateFormFromBOs()
            Me.EnableDisableFields()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
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
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnNew_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            Me.PopulateBOsFormFrom()
            If Me.State.MyBO.IsDirty Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", ElitaPlusPage.MSG_BTN_YES_NO_CANCEL, ElitaPlusPage.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                Me.CreateNew()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            Me.PopulateBOsFormFrom()
            If Me.State.MyBO.IsDirty Then
                Me.State.MyBO.Save()
                Me.State.HasDataChanged = True
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
                Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", ElitaPlusPage.MSG_BTN_OK, ElitaPlusPage.MSG_TYPE_INFO)
            Else
                Me.DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", ElitaPlusPage.MSG_BTN_OK, ElitaPlusPage.MSG_TYPE_INFO)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.PopulateBOsFormFrom()
            If Me.State.MyBO.IsDirty Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", ElitaPlusPage.MSG_BTN_YES_NO_CANCEL, ElitaPlusPage.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
            Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", ElitaPlusPage.MSG_BTN_YES_NO_CANCEL, ElitaPlusPage.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            Me.State.LastErrMsg = Me.ErrorCtrl.Text
        End Try
    End Sub

    Private Sub btnCopy_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy_WRITE.Click
        Try
            Me.PopulateBOsFormFrom()
            If Me.State.MyBO.IsDirty Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", ElitaPlusPage.MSG_BTN_YES_NO_CANCEL, ElitaPlusPage.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            Else
                Me.CreateNewWithCopy()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub
#End Region

#Region "Child Panel Post Backs"
    Private Sub moMakeDropdown_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles moMakeDropdown.SelectedIndexChanged
        Try
            If (moMakeDropdown.SelectedIndex = NO_ITEM_SELECTED_INDEX) Then
                moModelDropdown.Items.Clear()
            Else
                Dim manufacturerId As Guid = New Guid(moMakeDropdown.SelectedValue)
                '  ElitaPlusPage.BindListControlToDataView(Me.moModelDropdown, LookupListNew.GetEquipmentLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, manufacturerId.ToString(), False), "CODE", "ID", True, True)
                Dim listcontext As ListContext = New ListContext()
                listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                listcontext.ManufacturerId = manufacturerId
                listcontext.EquipmentTypeId = Guid.Empty
                Dim prodLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.EquipmentByCompanyGroupEquipmentType, ElitaPlusIdentity.Current.ActiveUser.LanguageCode, listcontext)
                Me.moModelDropdown.Populate(prodLkl, New PopulateOptions() With
                 {
                .AddBlankItem = True,
                .TextFunc = AddressOf .GetCode
                 })
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub moReplacementMakeDropDown_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles moReplacementMakeDropDown.SelectedIndexChanged
        Try
            If (moReplacementMakeDropDown.SelectedIndex = NO_ITEM_SELECTED_INDEX) Then
                moReplacementModelDropDown.Items.Clear()
            Else
                Dim manufacturerId As Guid = New Guid(moReplacementMakeDropDown.SelectedValue)
                ' ElitaPlusPage.BindListControlToDataView(Me.moReplacementModelDropDown, LookupListNew.GetEquipmentLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, manufacturerId.ToString(), False), "CODE", "ID", True, True)
                Dim listcontext As ListContext = New ListContext()
                listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                listcontext.ManufacturerId = manufacturerId
                listcontext.EquipmentTypeId = Guid.Empty
                Dim prodLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.EquipmentByCompanyGroupEquipmentType, ElitaPlusIdentity.Current.ActiveUser.LanguageCode, listcontext)
                Me.moReplacementModelDropDown.Populate(prodLkl, New PopulateOptions() With
                 {
                .AddBlankItem = True,
                .TextFunc = AddressOf .GetCode
                 })
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub
#End Region

#Region "Controlling Logic"

    Protected Sub CreateNewWithCopy()
        Me.State.IsACopy = True
        Me.PopulateBOsFormFrom()

        Dim newObj As New BestReplacementGroup
        newObj.Copy(Me.State.MyBO)

        Me.State.MyBO = newObj
        Me.State.MyBO.Code = Nothing
        Me.State.MyBO.Description = Nothing

        Me.PopulateFormFromBOs()
        Me.EnableDisableFields()

        'create the backup copy
        Me.State.ScreenSnapShotBO = New BestReplacementGroup
        Me.State.ScreenSnapShotBO.Clone(Me.State.MyBO)
        Me.State.IsACopy = False

    End Sub

    Protected Sub CreateNew()
        Me.State.ScreenSnapShotBO = Nothing 'Reset the backup copy

        Me.State.MyBO = New BestReplacementGroup()
        Me.PopulateFormFromBOs()
        Me.EnableDisableFields()
    End Sub

    Private Sub PopulateDropdowns()
        ' Dim companyGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
        '  Dim makeDataView As DataView = LookupListNew.GetManufacturerLookupList(companyGroupId)
        Dim listcontext As ListContext = New ListContext()
        listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
        Dim manufacturerLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ManufacturerByCompanyGroup", ElitaPlusIdentity.Current.ActiveUser.LanguageCode, listcontext)
        Me.moMakeDropdown.Populate(manufacturerLkl, New PopulateOptions() With
                            {
                             .AddBlankItem = True
                           })
        ' ElitaPlusPage.BindListControlToDataView(Me.moMakeDropdown, makeDataView, , , True, True)
        'ElitaPlusPage.BindListControlToDataView(Me.moReplacementMakeDropDown, makeDataView, , , True, True)
        Me.moReplacementMakeDropDown.Populate(manufacturerLkl, New PopulateOptions() With
                           {
                            .AddBlankItem = True
                          })
    End Sub

    Protected Sub EnableDisableFields()
        If Me.State.IsChildEditing Then
            ControlMgr.SetVisibleControl(Me, PanelAllEditDetail, True)
            EnableDisableParentControls(False)
        Else
            ControlMgr.SetVisibleControl(Me, PanelAllEditDetail, False)
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
    End Sub

    Sub EnableDisableParentControls(ByVal enableToggle As Boolean)
        ControlMgr.SetEnableControl(Me, btnBack, enableToggle)
        ControlMgr.SetEnableControl(Me, btnCopy_WRITE, enableToggle)
        ControlMgr.SetEnableControl(Me, btnDelete_WRITE, enableToggle)
        ControlMgr.SetEnableControl(Me, btnSave_WRITE, enableToggle)
        ControlMgr.SetEnableControl(Me, btnUndo_Write, enableToggle)

        ControlMgr.SetEnableControl(Me, moCodeText_WRITE, enableToggle)
        ControlMgr.SetEnableControl(Me, moDescriptionText_WRITE, enableToggle)
        ControlMgr.SetVisibleControl(Me, moEquipmentDataGrid, enableToggle)
        ControlMgr.SetVisibleControl(Me, btnAddNewChildFromGrid_WRITE, enableToggle)
    End Sub

    Protected Sub BindBoPropertiesToLabels()
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Description", Me.moDescriptionLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Code", Me.moCodeLabel)

        Me.ClearGridViewHeadersAndLabelsErrorSign()
    End Sub

    Protected Sub PopulateFormFromBOs()
        With Me.State.MyBO
            PopulateDetailGrid()

            Me.PopulateControlFromBOProperty(Me.moDescriptionText_WRITE, .Description)
            Me.PopulateControlFromBOProperty(Me.moCodeText_WRITE, .Code)
        End With
    End Sub

    Protected Sub PopulateBOsFormFrom()
        With Me.State.MyBO
            Me.PopulateBOProperty(Me.State.MyBO, "Code", Me.moCodeText_WRITE)
            Me.PopulateBOProperty(Me.State.MyBO, "Description", Me.moDescriptionText_WRITE)
        End With
        If Me.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub

    Sub PopulateDetailGrid()
        'This is a temporary Binding Logic. BEGIN        
        Dim dv As BestReplacementGroup.BestReplacementSelectionView = Me.State.MyBO.GetDetailSelectionView()
        dv.Sort = Me.State.SortExpressionDetailGrid

        Me.moEquipmentDataGrid.Columns(BestReplacementForm.GRID_COL_MANUFACTURER).SortExpression = BestReplacementGroup.BestReplacementSelectionView.COL_NAME_MANUFACTURER
        Me.moEquipmentDataGrid.Columns(BestReplacementForm.GRID_COL_MODEL).SortExpression = BestReplacementGroup.BestReplacementSelectionView.COL_NAME_MODEL
        Me.moEquipmentDataGrid.Columns(BestReplacementForm.GRID_COL_REPLACEMENT_MANUFACTURER).SortExpression = BestReplacementGroup.BestReplacementSelectionView.COL_NAME_REPLACEMENT_EQUIPMENT_MANUFACTURER
        Me.moEquipmentDataGrid.Columns(BestReplacementForm.GRID_COL_REPLACEMENT_MODEL).SortExpression = BestReplacementGroup.BestReplacementSelectionView.COL_NAME_REPLACEMENT_MODEL
        Me.moEquipmentDataGrid.Columns(BestReplacementForm.GRID_COL_PRIORITY).SortExpression = BestReplacementGroup.BestReplacementSelectionView.COL_NAME_PRIORITY
        Me.SetGridItemStyleColor(Me.moEquipmentDataGrid)

        SetPageAndSelectedIndexFromGuid(dv, Me.State.SelectedChildId, Me.moEquipmentDataGrid, Me.State.DetailPageIndex)
        Me.State.DetailPageIndex = Me.moEquipmentDataGrid.CurrentPageIndex

        Me.moEquipmentDataGrid.DataSource = dv
        Me.moEquipmentDataGrid.AutoGenerateColumns = False
        Me.moEquipmentDataGrid.DataBind()
        'This is a temporary Binding Logic. END
    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
        If Not confResponse Is Nothing AndAlso confResponse = ElitaPlusPage.MSG_VALUE_YES Then
            If Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr AndAlso Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.Accept Then
                Me.State.MyBO.Save()
            End If
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", ElitaPlusPage.MSG_BTN_OK, ElitaPlusPage.MSG_TYPE_INFO)
                    Me.CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", ElitaPlusPage.MSG_BTN_OK, ElitaPlusPage.MSG_TYPE_INFO)
                    Me.CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    Me.ReturnToCallingPage(New ReturnType(Me.State.ActionInProgress, Me.State.MyBO, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.Accept
                    Me.PopulateChildBOFromDetail()
                    Me.State.MyChildBO.Save()
                    Me.State.MyChildBO.EndEdit()
                    Me.State.MyBO.Save()

                    Me.State.IsChildEditing = False
                    Me.EnableDisableFields()
                    Me.PopulateDetailGrid()
            End Select
        ElseIf Not confResponse Is Nothing AndAlso confResponse = ElitaPlusPage.MSG_VALUE_NO Then
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    Me.CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    Me.CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    Me.ErrorCtrl.AddErrorAndShow(Me.State.LastErrMsg)
                Case ElitaPlusPage.DetailPageCommand.Accept
                    Me.State.MyChildBO.cancelEdit()
                    If Me.State.MyChildBO.IsSaveNew Then
                        Me.State.MyChildBO.Delete()
                        Me.State.MyChildBO.Save()
                    End If

                    Me.State.IsChildEditing = False
                    Me.EnableDisableFields()
                    Me.PopulateDetailGrid()
            End Select
        End If
        'Clean after consuming the action
        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        Me.HiddenSaveChangesPromptResponse.Value = ""
    End Sub

    Sub BeginChildEdit()
        Me.State.IsChildEditing = True
        Me.EnableDisableFields()
        With Me.State
            If Not .SelectedChildId.Equals(Guid.Empty) Then
                .MyChildBO = .MyBO.GetChild(.SelectedChildId)
            Else
                .MyChildBO = .MyBO.GetNewChild
            End If
            .MyChildBO.BeginEdit()
        End With
        Me.PopulateDetailFromChildBO()
    End Sub

    Sub PopulateDetailFromChildBO()
        With Me.State.MyChildBO
            ElitaPlusPage.SetSelectedItem(Me.moMakeDropdown, .EquipmentManufacturerId)
            'ElitaPlusPage.BindListControlToDataView(Me.moModelDropdown, LookupListNew.GetEquipmentLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, .EquipmentManufacturerId.ToString(), False), "CODE", "ID", True, True)
            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            listcontext.ManufacturerId = .EquipmentManufacturerId
            listcontext.EquipmentTypeId = Guid.Empty
            Dim prodLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.EquipmentByCompanyGroupEquipmentType, ElitaPlusIdentity.Current.ActiveUser.LanguageCode, listcontext)
            Me.moModelDropdown.Populate(prodLkl, New PopulateOptions() With
                 {
                .AddBlankItem = True,
                .TextFunc = AddressOf .GetCode
                 })
            ElitaPlusPage.SetSelectedItem(Me.moModelDropdown, .EquipmentId)
            ElitaPlusPage.SetSelectedItem(Me.moReplacementMakeDropDown, .ReplacementEquipmentManufacturerId)
            'ElitaPlusPage.BindListControlToDataView(Me.moReplacementModelDropDown, LookupListNew.GetEquipmentLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, .ReplacementEquipmentManufacturerId.ToString(), False), "CODE", "ID", True, True)
            Dim listcontext1 As ListContext = New ListContext()
            listcontext1.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            listcontext1.ManufacturerId = .EquipmentManufacturerId
            listcontext1.EquipmentTypeId = Guid.Empty
            Dim equiLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.EquipmentByCompanyGroupEquipmentType, ElitaPlusIdentity.Current.ActiveUser.LanguageCode, listcontext1)
            Me.moReplacementModelDropDown.Populate(equiLkl, New PopulateOptions() With
                 {
                .AddBlankItem = True,
                .TextFunc = AddressOf .GetCode
                 })
            ElitaPlusPage.SetSelectedItem(Me.moReplacementModelDropDown, .ReplacementEquipmentId)
            Me.PopulateControlFromBOProperty(Me.moPriorityText, .Priority)
        End With
    End Sub

    Sub PopulateChildBOFromDetail()
        With Me.State.MyChildBO
            Me.PopulateBOProperty(Me.State.MyChildBO, "EquipmentManufacturerId", Me.moMakeDropdown)
            Me.PopulateBOProperty(Me.State.MyChildBO, "EquipmentId", Me.moModelDropdown)
            Me.PopulateBOProperty(Me.State.MyChildBO, "ReplacementEquipmentManufacturerId", Me.moReplacementMakeDropDown)
            Me.PopulateBOProperty(Me.State.MyChildBO, "ReplacementEquipmentId", Me.moReplacementModelDropDown)
            Me.PopulateBOProperty(Me.State.MyChildBO, "Priority", Me.moPriorityText)
        End With
        If Me.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub

    Protected Sub BindDetailBoPropertiesToLabels()
        With Me.State
            Me.BindBOPropertyToLabel(.MyChildBO, "EquipmentManufacturerId", Me.moMakeLabel)
            Me.BindBOPropertyToLabel(.MyChildBO, "EquipmentId", Me.moModelLabel)
            Me.BindBOPropertyToLabel(.MyChildBO, "ReplacementEquipmentManufacturerId", Me.moReplacementMakeLabel)
            Me.BindBOPropertyToLabel(.MyChildBO, "ReplacementEquipmentId", Me.moReplacementModelLabel)
            Me.BindBOPropertyToLabel(.MyChildBO, "Priority", Me.moPriorityLabel)
        End With
        Me.ClearGridViewHeadersAndLabelsErrorSign()
    End Sub

    Sub EndChildEdit(ByVal lastop As ElitaPlusPage.DetailPageCommand)
        Try
            With Me.State
                Select Case lastop
                    Case ElitaPlusPage.DetailPageCommand.OK
                        Me.PopulateChildBOFromDetail()
                        .MyChildBO.Save()
                        .MyChildBO.EndEdit()
                    Case ElitaPlusPage.DetailPageCommand.Cancel
                        .MyChildBO.cancelEdit()
                        If .MyChildBO.IsSaveNew Then
                            .MyChildBO.Delete()
                            .MyChildBO.Save()
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Back
                        .MyChildBO.cancelEdit()
                        If .MyChildBO.IsSaveNew Then
                            .MyChildBO.Delete()
                            .MyChildBO.Save()
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        .MyChildBO.Delete()
                        .MyChildBO.Save()
                        .MyChildBO.EndEdit()
                        .SelectedChildId = Guid.Empty
                End Select
            End With
            Me.State.IsChildEditing = False
            Me.EnableDisableFields()
            Me.PopulateDetailGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub
#End Region

#Region "Equipment Grid Events"
    Protected Sub moEquipmentDataGrid_ItemCreated(ByVal sender As Object, ByVal e As DataGridItemEventArgs) Handles moEquipmentDataGrid.ItemCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Public Sub moEquipmentDataGrid_ItemDataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs) Handles moEquipmentDataGrid.ItemDataBound
        Try
            Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                e.Item.Cells(BestReplacementForm.GRID_COL_MANUFACTURER).Text = dvRow(BestReplacementGroup.BestReplacementSelectionView.COL_NAME_MANUFACTURER).ToString
                e.Item.Cells(BestReplacementForm.GRID_COL_MODEL).Text = dvRow(BestReplacementGroup.BestReplacementSelectionView.COL_NAME_MODEL).ToString
                e.Item.Cells(BestReplacementForm.GRID_COL_REPLACEMENT_MANUFACTURER).Text = dvRow(BestReplacementGroup.BestReplacementSelectionView.COL_NAME_REPLACEMENT_EQUIPMENT_MANUFACTURER).ToString
                e.Item.Cells(BestReplacementForm.GRID_COL_REPLACEMENT_MODEL).Text = dvRow(BestReplacementGroup.BestReplacementSelectionView.COL_NAME_REPLACEMENT_MODEL).ToString
                e.Item.Cells(BestReplacementForm.GRID_COL_PRIORITY).Text = dvRow(BestReplacementGroup.BestReplacementSelectionView.COL_NAME_PRIORITY).ToString
                e.Item.Cells(BestReplacementForm.GRID_COL_BEST_REPLACEMENT_ID).Text = New Guid(CType(dvRow(BestReplacementGroup.BestReplacementSelectionView.COL_NAME_BEST_REPLACEMENT_ID), Byte())).ToString()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Public Sub moEquipmentDataGrid_ItemCommand(ByVal sender As Object, ByVal e As DataGridCommandEventArgs) Handles moEquipmentDataGrid.ItemCommand
        Try
            If e.CommandName = "ViewRecord" Then
                Me.State.IsChildEditing = True
                Me.State.SelectedChildId = New Guid(e.Item.Cells(BestReplacementForm.GRID_COL_BEST_REPLACEMENT_ID).Text)
                Me.BeginChildEdit()
                Me.EnableDisableFields()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub moEquipmentDataGrid_PageIndexChanged(ByVal source As Object, ByVal e As DataGridPageChangedEventArgs) Handles moEquipmentDataGrid.PageIndexChanged
        Try
            Me.State.DetailPageIndex = e.NewPageIndex
            Me.State.SelectedChildId = Guid.Empty
            Me.PopulateDetailGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub moEquipmentDataGrid_SortCommand(ByVal source As Object, ByVal e As DataGridSortCommandEventArgs) Handles moEquipmentDataGrid.SortCommand
        Try
            If Me.State.SortExpressionDetailGrid.StartsWith(e.SortExpression) Then
                If Me.State.SortExpressionDetailGrid.StartsWith(e.SortExpression & " DESC") Then
                    Me.State.SortExpressionDetailGrid = e.SortExpression
                Else
                    Me.State.SortExpressionDetailGrid = e.SortExpression & " DESC"
                End If
            Else
                Me.State.SortExpressionDetailGrid = e.SortExpression
            End If
            If Me.State.SortExpressionDetailGrid.StartsWith(BestReplacementGroup.BestReplacementSelectionView.COL_NAME_MANUFACTURER) Then
                Me.State.SortExpressionDetailGrid &= ", " & BestReplacementGroup.BestReplacementSelectionView.COL_NAME_MODEL
            End If
            If Me.State.SortExpressionDetailGrid.StartsWith(BestReplacementGroup.BestReplacementSelectionView.COL_NAME_REPLACEMENT_EQUIPMENT_MANUFACTURER) Then
                Me.State.SortExpressionDetailGrid &= ", " & BestReplacementGroup.BestReplacementSelectionView.COL_NAME_REPLACEMENT_MODEL
            End If
            Me.PopulateDetailGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub
#End Region

#Region "Detail Clicks"
    Private Sub btnAddNewChildFromGrid_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddNewChildFromGrid_WRITE.Click
        Try
            Me.State.SelectedChildId = Guid.Empty
            Me.BeginChildEdit()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnAddNewChild_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddNewChild_Write.Click
        Try
            Me.State.MyChildBO.cancelEdit()
            If Me.State.MyChildBO.IsSaveNew Then
                Me.State.MyChildBO.Delete()
            End If
            Me.State.MyChildBO = Me.State.MyBO.GetNewChild
            Me.State.MyChildBO.BeginEdit()
            Me.PopulateDetailFromChildBO()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnOkChild_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOkChild_Write.Click
        Try
            Me.EndChildEdit(ElitaPlusPage.DetailPageCommand.OK)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnDeleteChild_Write_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteChild_Write.Click
        Try
            Me.EndChildEdit(ElitaPlusPage.DetailPageCommand.Delete)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnAddChildWithCopy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddChildWithCopy_Write.Click
        Try
            With Me.State
                Me.State.MyChildBO.cancelEdit()
                If Me.State.MyChildBO.IsSaveNew Then
                    Me.State.MyChildBO.Delete()
                End If
                Me.State.MyChildBO = .MyBO.GetNewChild
                Me.State.MyChildBO.BeginEdit()
                Me.PopulateChildBOFromDetail()
            End With
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnBackChild_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackChild.Click
        Try
            Me.PopulateChildBOFromDetail()
            If Me.State.MyChildBO.IsDirty Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", ElitaPlusPage.MSG_BTN_YES_NO_CANCEL, ElitaPlusPage.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Accept
            Else
                Me.EndChildEdit(ElitaPlusPage.DetailPageCommand.Back)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
            Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", ElitaPlusPage.MSG_BTN_YES_NO_CANCEL, ElitaPlusPage.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            Me.State.LastErrMsg = Me.ErrorCtrl.Text
        End Try
    End Sub

    Private Sub btnCancelChild_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelChild.Click
        Try
            If Not Me.State.MyChildBO.IsNew Then
                'Reload from the DB
                Me.State.MyChildBO = Me.State.MyBO.GetChild(Me.State.SelectedChildId)
            ElseIf Not Me.State.ScreenSnapShotChildBO Is Nothing Then
                'It was a new with copy
                Me.State.MyChildBO.Clone(Me.State.ScreenSnapShotChildBO)
            Else
                Me.State.MyChildBO = Me.State.MyBO.GetNewChild
            End If
            Me.PopulateDetailFromChildBO()
            Me.EnableDisableFields()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub
#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As BestReplacementGroup
        Public HasDataChanged As Boolean
        Public Sub New(ByVal LastOperation As DetailPageCommand, ByVal currentEditingBo As BestReplacementGroup, ByVal hasDataChanged As Boolean)
            Me.LastOperation = LastOperation
            Me.EditingBo = currentEditingBo
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class
#End Region

End Class