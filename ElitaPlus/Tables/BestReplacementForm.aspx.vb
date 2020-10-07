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

    Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
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
    Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
        Try
            If CallingParameters IsNot Nothing Then
                'Get the id from the parent
                State.MyBO = New BestReplacementGroup(CType(CallingParameters, Guid))
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try

    End Sub

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Try
            'Put user code to initialize the page here
            ErrorCtrl.Clear_Hide()

            If Not IsPostBack Then
                MenuEnabled = False
                AddConfirmation(btnDelete_WRITE, Message.DELETE_RECORD_PROMPT)
                AddControlMsg(btnDeleteChild_Write, Message.DELETE_RECORD_PROMPT, "", ElitaPlusPage.MSG_BTN_YES_NO, ElitaPlusPage.MSG_TYPE_CONFIRM, True)
                If State.MyBO Is Nothing Then
                    State.MyBO = New BestReplacementGroup
                End If
                PopulateDropdowns()
                PopulateFormFromBOs()
                EnableDisableFields()
            End If
            BindDetailBoPropertiesToLabels()
            BindBoPropertiesToLabels()
            CheckIfComingFromSaveConfirm()
            If Not IsPostBack Then
                AddLabelDecorations(State.MyBO)
                AddLabelDecorations(New BestReplacement())
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
        ShowMissingTranslations(ErrorCtrl)
    End Sub
#End Region

#Region "Button Clicks"
    Private Sub btnUndo_Write_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_Write.Click
        Try
            If Not State.MyBO.IsNew Then
                'Reload from the DB
                State.MyBO = New BestReplacementGroup(State.MyBO.Id)
            ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                'It was a new with copy
                State.MyBO.Clone(State.ScreenSnapShotBO)
            Else
                State.MyBO = New BestReplacementGroup
            End If
            PopulateFormFromBOs()
            EnableDisableFields()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
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
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            PopulateBOsFormFrom()
            If State.MyBO.IsDirty Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", ElitaPlusPage.MSG_BTN_YES_NO_CANCEL, ElitaPlusPage.MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                CreateNew()
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnSave_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            PopulateBOsFormFrom()
            If State.MyBO.IsDirty Then
                State.MyBO.Save()
                State.HasDataChanged = True
                PopulateFormFromBOs()
                EnableDisableFields()
                DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", ElitaPlusPage.MSG_BTN_OK, ElitaPlusPage.MSG_TYPE_INFO)
            Else
                DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", ElitaPlusPage.MSG_BTN_OK, ElitaPlusPage.MSG_TYPE_INFO)
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
        Try
            PopulateBOsFormFrom()
            If State.MyBO.IsDirty Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", ElitaPlusPage.MSG_BTN_YES_NO_CANCEL, ElitaPlusPage.MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
            DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", ElitaPlusPage.MSG_BTN_YES_NO_CANCEL, ElitaPlusPage.MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            State.LastErrMsg = ErrorCtrl.Text
        End Try
    End Sub

    Private Sub btnCopy_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnCopy_WRITE.Click
        Try
            PopulateBOsFormFrom()
            If State.MyBO.IsDirty Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", ElitaPlusPage.MSG_BTN_YES_NO_CANCEL, ElitaPlusPage.MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            Else
                CreateNewWithCopy()
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub
#End Region

#Region "Child Panel Post Backs"
    Private Sub moMakeDropdown_SelectedIndexChanged(sender As Object, e As EventArgs) Handles moMakeDropdown.SelectedIndexChanged
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
                moModelDropdown.Populate(prodLkl, New PopulateOptions() With
                 {
                .AddBlankItem = True,
                .TextFunc = AddressOf .GetCode
                 })
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub moReplacementMakeDropDown_SelectedIndexChanged(sender As Object, e As EventArgs) Handles moReplacementMakeDropDown.SelectedIndexChanged
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
                moReplacementModelDropDown.Populate(prodLkl, New PopulateOptions() With
                 {
                .AddBlankItem = True,
                .TextFunc = AddressOf .GetCode
                 })
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub
#End Region

#Region "Controlling Logic"

    Protected Sub CreateNewWithCopy()
        State.IsACopy = True
        PopulateBOsFormFrom()

        Dim newObj As New BestReplacementGroup
        newObj.Copy(State.MyBO)

        State.MyBO = newObj
        State.MyBO.Code = Nothing
        State.MyBO.Description = Nothing

        PopulateFormFromBOs()
        EnableDisableFields()

        'create the backup copy
        State.ScreenSnapShotBO = New BestReplacementGroup
        State.ScreenSnapShotBO.Clone(State.MyBO)
        State.IsACopy = False

    End Sub

    Protected Sub CreateNew()
        State.ScreenSnapShotBO = Nothing 'Reset the backup copy

        State.MyBO = New BestReplacementGroup()
        PopulateFormFromBOs()
        EnableDisableFields()
    End Sub

    Private Sub PopulateDropdowns()
        ' Dim companyGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
        '  Dim makeDataView As DataView = LookupListNew.GetManufacturerLookupList(companyGroupId)
        Dim listcontext As ListContext = New ListContext()
        listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
        Dim manufacturerLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ManufacturerByCompanyGroup", ElitaPlusIdentity.Current.ActiveUser.LanguageCode, listcontext)
        moMakeDropdown.Populate(manufacturerLkl, New PopulateOptions() With
                            {
                             .AddBlankItem = True
                           })
        ' ElitaPlusPage.BindListControlToDataView(Me.moMakeDropdown, makeDataView, , , True, True)
        'ElitaPlusPage.BindListControlToDataView(Me.moReplacementMakeDropDown, makeDataView, , , True, True)
        moReplacementMakeDropDown.Populate(manufacturerLkl, New PopulateOptions() With
                           {
                            .AddBlankItem = True
                          })
    End Sub

    Protected Sub EnableDisableFields()
        If State.IsChildEditing Then
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
        If State.MyBO.IsNew Then
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
        End If
    End Sub

    Sub EnableDisableParentControls(enableToggle As Boolean)
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
        BindBOPropertyToLabel(State.MyBO, "Description", moDescriptionLabel)
        BindBOPropertyToLabel(State.MyBO, "Code", moCodeLabel)

        ClearGridViewHeadersAndLabelsErrorSign()
    End Sub

    Protected Sub PopulateFormFromBOs()
        With State.MyBO
            PopulateDetailGrid()

            PopulateControlFromBOProperty(moDescriptionText_WRITE, .Description)
            PopulateControlFromBOProperty(moCodeText_WRITE, .Code)
        End With
    End Sub

    Protected Sub PopulateBOsFormFrom()
        With State.MyBO
            PopulateBOProperty(State.MyBO, "Code", moCodeText_WRITE)
            PopulateBOProperty(State.MyBO, "Description", moDescriptionText_WRITE)
        End With
        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub

    Sub PopulateDetailGrid()
        'This is a temporary Binding Logic. BEGIN        
        Dim dv As BestReplacementGroup.BestReplacementSelectionView = State.MyBO.GetDetailSelectionView()
        dv.Sort = State.SortExpressionDetailGrid

        moEquipmentDataGrid.Columns(BestReplacementForm.GRID_COL_MANUFACTURER).SortExpression = BestReplacementGroup.BestReplacementSelectionView.COL_NAME_MANUFACTURER
        moEquipmentDataGrid.Columns(BestReplacementForm.GRID_COL_MODEL).SortExpression = BestReplacementGroup.BestReplacementSelectionView.COL_NAME_MODEL
        moEquipmentDataGrid.Columns(BestReplacementForm.GRID_COL_REPLACEMENT_MANUFACTURER).SortExpression = BestReplacementGroup.BestReplacementSelectionView.COL_NAME_REPLACEMENT_EQUIPMENT_MANUFACTURER
        moEquipmentDataGrid.Columns(BestReplacementForm.GRID_COL_REPLACEMENT_MODEL).SortExpression = BestReplacementGroup.BestReplacementSelectionView.COL_NAME_REPLACEMENT_MODEL
        moEquipmentDataGrid.Columns(BestReplacementForm.GRID_COL_PRIORITY).SortExpression = BestReplacementGroup.BestReplacementSelectionView.COL_NAME_PRIORITY
        SetGridItemStyleColor(moEquipmentDataGrid)

        SetPageAndSelectedIndexFromGuid(dv, State.SelectedChildId, moEquipmentDataGrid, State.DetailPageIndex)
        State.DetailPageIndex = moEquipmentDataGrid.CurrentPageIndex

        moEquipmentDataGrid.DataSource = dv
        moEquipmentDataGrid.AutoGenerateColumns = False
        moEquipmentDataGrid.DataBind()
        'This is a temporary Binding Logic. END
    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
        If confResponse IsNot Nothing AndAlso confResponse = ElitaPlusPage.MSG_VALUE_YES Then
            If State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr AndAlso State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.Accept Then
                State.MyBO.Save()
            End If
            Select Case State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", ElitaPlusPage.MSG_BTN_OK, ElitaPlusPage.MSG_TYPE_INFO)
                    CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", ElitaPlusPage.MSG_BTN_OK, ElitaPlusPage.MSG_TYPE_INFO)
                    CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    ReturnToCallingPage(New ReturnType(State.ActionInProgress, State.MyBO, State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.Accept
                    PopulateChildBOFromDetail()
                    State.MyChildBO.Save()
                    State.MyChildBO.EndEdit()
                    State.MyBO.Save()

                    State.IsChildEditing = False
                    EnableDisableFields()
                    PopulateDetailGrid()
            End Select
        ElseIf confResponse IsNot Nothing AndAlso confResponse = ElitaPlusPage.MSG_VALUE_NO Then
            Select Case State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    ErrorCtrl.AddErrorAndShow(State.LastErrMsg)
                Case ElitaPlusPage.DetailPageCommand.Accept
                    State.MyChildBO.cancelEdit()
                    If State.MyChildBO.IsSaveNew Then
                        State.MyChildBO.Delete()
                        State.MyChildBO.Save()
                    End If

                    State.IsChildEditing = False
                    EnableDisableFields()
                    PopulateDetailGrid()
            End Select
        End If
        'Clean after consuming the action
        State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        HiddenSaveChangesPromptResponse.Value = ""
    End Sub

    Sub BeginChildEdit()
        State.IsChildEditing = True
        EnableDisableFields()
        With State
            If Not .SelectedChildId.Equals(Guid.Empty) Then
                .MyChildBO = .MyBO.GetChild(.SelectedChildId)
            Else
                .MyChildBO = .MyBO.GetNewChild
            End If
            .MyChildBO.BeginEdit()
        End With
        PopulateDetailFromChildBO()
    End Sub

    Sub PopulateDetailFromChildBO()
        With State.MyChildBO
            ElitaPlusPage.SetSelectedItem(moMakeDropdown, .EquipmentManufacturerId)
            'ElitaPlusPage.BindListControlToDataView(Me.moModelDropdown, LookupListNew.GetEquipmentLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, .EquipmentManufacturerId.ToString(), False), "CODE", "ID", True, True)
            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            listcontext.ManufacturerId = .EquipmentManufacturerId
            listcontext.EquipmentTypeId = Guid.Empty
            Dim prodLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.EquipmentByCompanyGroupEquipmentType, ElitaPlusIdentity.Current.ActiveUser.LanguageCode, listcontext)
            moModelDropdown.Populate(prodLkl, New PopulateOptions() With
                 {
                .AddBlankItem = True,
                .TextFunc = AddressOf .GetCode
                 })
            ElitaPlusPage.SetSelectedItem(moModelDropdown, .EquipmentId)
            ElitaPlusPage.SetSelectedItem(moReplacementMakeDropDown, .ReplacementEquipmentManufacturerId)
            'ElitaPlusPage.BindListControlToDataView(Me.moReplacementModelDropDown, LookupListNew.GetEquipmentLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, .ReplacementEquipmentManufacturerId.ToString(), False), "CODE", "ID", True, True)
            Dim listcontext1 As ListContext = New ListContext()
            listcontext1.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            listcontext1.ManufacturerId = .EquipmentManufacturerId
            listcontext1.EquipmentTypeId = Guid.Empty
            Dim equiLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.EquipmentByCompanyGroupEquipmentType, ElitaPlusIdentity.Current.ActiveUser.LanguageCode, listcontext1)
            moReplacementModelDropDown.Populate(equiLkl, New PopulateOptions() With
                 {
                .AddBlankItem = True,
                .TextFunc = AddressOf .GetCode
                 })
            ElitaPlusPage.SetSelectedItem(moReplacementModelDropDown, .ReplacementEquipmentId)
            PopulateControlFromBOProperty(moPriorityText, .Priority)
        End With
    End Sub

    Sub PopulateChildBOFromDetail()
        With State.MyChildBO
            PopulateBOProperty(State.MyChildBO, "EquipmentManufacturerId", moMakeDropdown)
            PopulateBOProperty(State.MyChildBO, "EquipmentId", moModelDropdown)
            PopulateBOProperty(State.MyChildBO, "ReplacementEquipmentManufacturerId", moReplacementMakeDropDown)
            PopulateBOProperty(State.MyChildBO, "ReplacementEquipmentId", moReplacementModelDropDown)
            PopulateBOProperty(State.MyChildBO, "Priority", moPriorityText)
        End With
        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub

    Protected Sub BindDetailBoPropertiesToLabels()
        With State
            BindBOPropertyToLabel(.MyChildBO, "EquipmentManufacturerId", moMakeLabel)
            BindBOPropertyToLabel(.MyChildBO, "EquipmentId", moModelLabel)
            BindBOPropertyToLabel(.MyChildBO, "ReplacementEquipmentManufacturerId", moReplacementMakeLabel)
            BindBOPropertyToLabel(.MyChildBO, "ReplacementEquipmentId", moReplacementModelLabel)
            BindBOPropertyToLabel(.MyChildBO, "Priority", moPriorityLabel)
        End With
        ClearGridViewHeadersAndLabelsErrorSign()
    End Sub

    Sub EndChildEdit(lastop As ElitaPlusPage.DetailPageCommand)
        Try
            With State
                Select Case lastop
                    Case ElitaPlusPage.DetailPageCommand.OK
                        PopulateChildBOFromDetail()
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
            State.IsChildEditing = False
            EnableDisableFields()
            PopulateDetailGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub
#End Region

#Region "Equipment Grid Events"
    Protected Sub moEquipmentDataGrid_ItemCreated(sender As Object, e As DataGridItemEventArgs) Handles moEquipmentDataGrid.ItemCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Public Sub moEquipmentDataGrid_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles moEquipmentDataGrid.ItemDataBound
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
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Public Sub moEquipmentDataGrid_ItemCommand(sender As Object, e As DataGridCommandEventArgs) Handles moEquipmentDataGrid.ItemCommand
        Try
            If e.CommandName = "ViewRecord" Then
                State.IsChildEditing = True
                State.SelectedChildId = New Guid(e.Item.Cells(BestReplacementForm.GRID_COL_BEST_REPLACEMENT_ID).Text)
                BeginChildEdit()
                EnableDisableFields()
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub moEquipmentDataGrid_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles moEquipmentDataGrid.PageIndexChanged
        Try
            State.DetailPageIndex = e.NewPageIndex
            State.SelectedChildId = Guid.Empty
            PopulateDetailGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub moEquipmentDataGrid_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles moEquipmentDataGrid.SortCommand
        Try
            If State.SortExpressionDetailGrid.StartsWith(e.SortExpression) Then
                If State.SortExpressionDetailGrid.StartsWith(e.SortExpression & " DESC") Then
                    State.SortExpressionDetailGrid = e.SortExpression
                Else
                    State.SortExpressionDetailGrid = e.SortExpression & " DESC"
                End If
            Else
                State.SortExpressionDetailGrid = e.SortExpression
            End If
            If State.SortExpressionDetailGrid.StartsWith(BestReplacementGroup.BestReplacementSelectionView.COL_NAME_MANUFACTURER) Then
                State.SortExpressionDetailGrid &= ", " & BestReplacementGroup.BestReplacementSelectionView.COL_NAME_MODEL
            End If
            If State.SortExpressionDetailGrid.StartsWith(BestReplacementGroup.BestReplacementSelectionView.COL_NAME_REPLACEMENT_EQUIPMENT_MANUFACTURER) Then
                State.SortExpressionDetailGrid &= ", " & BestReplacementGroup.BestReplacementSelectionView.COL_NAME_REPLACEMENT_MODEL
            End If
            PopulateDetailGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub
#End Region

#Region "Detail Clicks"
    Private Sub btnAddNewChildFromGrid_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnAddNewChildFromGrid_WRITE.Click
        Try
            State.SelectedChildId = Guid.Empty
            BeginChildEdit()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnAddNewChild_Click(sender As Object, e As System.EventArgs) Handles btnAddNewChild_Write.Click
        Try
            State.MyChildBO.cancelEdit()
            If State.MyChildBO.IsSaveNew Then
                State.MyChildBO.Delete()
            End If
            State.MyChildBO = State.MyBO.GetNewChild
            State.MyChildBO.BeginEdit()
            PopulateDetailFromChildBO()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnOkChild_Click(sender As Object, e As System.EventArgs) Handles btnOkChild_Write.Click
        Try
            EndChildEdit(ElitaPlusPage.DetailPageCommand.OK)
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnDeleteChild_Write_Click(sender As Object, e As System.EventArgs) Handles btnDeleteChild_Write.Click
        Try
            EndChildEdit(ElitaPlusPage.DetailPageCommand.Delete)
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnAddChildWithCopy_Click(sender As Object, e As System.EventArgs) Handles btnAddChildWithCopy_Write.Click
        Try
            With State
                State.MyChildBO.cancelEdit()
                If State.MyChildBO.IsSaveNew Then
                    State.MyChildBO.Delete()
                End If
                State.MyChildBO = .MyBO.GetNewChild
                State.MyChildBO.BeginEdit()
                PopulateChildBOFromDetail()
            End With
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnBackChild_Click(sender As System.Object, e As System.EventArgs) Handles btnBackChild.Click
        Try
            PopulateChildBOFromDetail()
            If State.MyChildBO.IsDirty Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", ElitaPlusPage.MSG_BTN_YES_NO_CANCEL, ElitaPlusPage.MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Accept
            Else
                EndChildEdit(ElitaPlusPage.DetailPageCommand.Back)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
            DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", ElitaPlusPage.MSG_BTN_YES_NO_CANCEL, ElitaPlusPage.MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            State.LastErrMsg = ErrorCtrl.Text
        End Try
    End Sub

    Private Sub btnCancelChild_Click(sender As Object, e As System.EventArgs) Handles btnCancelChild.Click
        Try
            If Not State.MyChildBO.IsNew Then
                'Reload from the DB
                State.MyChildBO = State.MyBO.GetChild(State.SelectedChildId)
            ElseIf State.ScreenSnapShotChildBO IsNot Nothing Then
                'It was a new with copy
                State.MyChildBO.Clone(State.ScreenSnapShotChildBO)
            Else
                State.MyChildBO = State.MyBO.GetNewChild
            End If
            PopulateDetailFromChildBO()
            EnableDisableFields()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub
#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As BestReplacementGroup
        Public HasDataChanged As Boolean
        Public Sub New(LastOperation As DetailPageCommand, currentEditingBo As BestReplacementGroup, hasDataChanged As Boolean)
            Me.LastOperation = LastOperation
            EditingBo = currentEditingBo
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class
#End Region

End Class