Option Strict On
Option Explicit On

Imports System.Security.Cryptography.X509Certificates
Imports Assurant.ElitaPlus.DALObjects
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Tables

    Partial Class MfgCoverageForm
        Inherits ElitaPlusSearchPage

        Private Class PageStatus

            Public Sub New()
                pageIndex = 0
                pageCount = 0
            End Sub

        End Class


#Region "Member Variables"

        Private Shared pageIndex As Integer
        Private Shared pageCount As Integer
        Private IsReturningFromChild As Boolean = False

#End Region

#Region "Properties"

        Public ReadOnly Property IsEditing() As Boolean
            Get
                IsEditing = (Grid.EditItemIndex > NO_ROW_SELECTED_INDEX)
            End Get
        End Property

#End Region

#Region "Page State"
        Class MyState
            Public PageIndex As Integer = 0
            Public myBO As MfgCoverage
            Public DescriptionMask As String
            Public ManufacturerSearchId As Guid
            Public RiskTypeSearchId As Guid
            Public Model As String
            Public CompanyId As Guid
            Public CompanyGroupId As Guid
            Public Id As Guid
            Public IsGridVisible As Boolean
            Public IsAfterSave As Boolean
            Public IsEditMode As Boolean
            Public searchDV As DataView = Nothing
            Public SortExpression As String = MfgCoverage.MfgCoverageSearchDV.COL_MANUFACTURER_NAME
            Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
            Public AddingNewRow As Boolean
            Public Canceling As Boolean
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

#Region "Constants"

        Private Const ID_COL_IDX As Integer = 2
        Private Const MANUFACTURER_COL_IDX As Integer = 3
        Private Const RISK_TYPE_COL_IDX As Integer = 4
        Private Const MODEL_COL_IDX As Integer = 5
        Private Const MFG_WARRANTY_COL_IDX As Integer = 6
        Private Const EQUIPMENT_TYPE_COL_IDX As Integer = 7
        Private Const EQUIPMENT_COL_IDX As Integer = 8
        Private Const MFG_MAIN_PARTS_WARRANTY_COL_IDX As Integer = 9
        Private Const EXT_WARRANTY_COL_IDX As Integer = 10


        Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
        Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

        Private Const EDIT_COMMAND As String = "EditRecord"
        Private Const DELETE_COMMAND As String = "DeleteRecord"
        Private Const SORT_COMMAND As String = "Sort"
        Private Const EXT_WARRANTY_COMMAND As String = "ExtWarranty"

        Private Const MODEL_IN_GRID_CONTROL_NAME As String = "TextBoxGridModel"
        Private Const RISK_TYPE_LIST_IN_GRID_CONTROL_NAME As String = "cboRiskTypeInGrid"
        Private Const EQUIPMENT_TYPE_LIST_IN_GRID_CONTROL_NAME As String = "cboEquipmentType"
        Private Const EQUIPMENT_LIST_IN_GRID_CONTROL_NAME As String = "cboEquipment"
        Private Const MFG_WARRANTY_IN_GRID_CONTROL_NAME As String = "TextBoxGridMfgWarranty"
        Private Const MANUFACTURER_LIST_IN_GRID_CONTROL_NAME As String = "cboManufacturerInGrid"
        Private Const MFG_MAIN_PARTS_WARRANTY_IN_GRID_CONTROL_NAME As String = "TextBoxGridMfgMainPartsWarranty"

        Private Const MANUFACTURER_DESCRIPTION_COL_NAME As String = "CODE"
        Private Const RISK_TYPE_DESCRIPTION_COL_NAME As String = "description"

        Private Const DEALER_CELL_STYLE_WIDTH As String = "45px" '199
        Private Const MANUFACTURER_CELL_STYLE_WIDTH As String = "15px" '199
        Private Const RISK_TYPE_CELL_STYLE_WIDTH As String = "30px" '175
        Private Const MODEL_CELL_STYLE_WIDTH As String = "10px" '175
        Private Const MFG_WARRANTY_CELL_STYLE_WIDTH As String = "5px" '175
        Private Const DEDUCTIBLE_CELL_STYLE_WIDTH As String = "45px" '175
        Private Const MFG_MAIN_PARTS_WARRANTY_CELL_STYLE_WIDTH As String = "5px" '175

        Private Const NO_ROW_SELECTED_INDEX As Integer = -1

        Private Const LABEL_DEALER As String = "DEALER"

        Public Const PAGETITLE As String = "MFG_COVERAGE"
        Public Const PAGETAB As String = "TABLES"

#End Region

#Region "Button Click Handlers"

        Private Sub SearchButton_Click(sender As System.Object, e As System.EventArgs) Handles SearchButton.Click

            Search()

        End Sub

        Private Sub Search()
            Try
                State.PageIndex = 0
                State.Id = Guid.Empty
                State.IsGridVisible = True
                State.searchDV = Nothing
                PopulateGrid()
                State.PageIndex = Grid.CurrentPageIndex
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Private Sub ClearButton_Click(sender As System.Object, e As System.EventArgs) Handles ClearButton.Click

            ClearSearchCriteria()

        End Sub

        Private Sub ClearSearchCriteria()

            Try
                cboManufacturer.SelectedIndex = 0
                cboRiskType.SelectedIndex = 0
                txtModel.Text = ""
                'Update Page State
                With State
                    .ManufacturerSearchId = Guid.Empty
                    .RiskTypeSearchId = Guid.Empty
                    .Model = ""
                End With
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Private Sub NewButton_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles NewButton_WRITE.Click

            Try
                State.IsEditMode = True
                State.IsGridVisible = True
                State.AddingNewRow = True
                AddNew()
                SetButtonsState()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Private Sub SaveButton_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles SaveButton_WRITE.Click

            Try
                PopulateBOFromForm()
                If (State.myBO.IsDirty) Then
                    State.myBO.Save()
                    State.IsAfterSave = True
                    State.AddingNewRow = False
                    AddInfoMsg(MSG_RECORD_SAVED_OK)
                    State.searchDV = Nothing
                    ReturnFromEditing()
                Else
                    AddInfoMsg(MSG_RECORD_NOT_SAVED)
                    ReturnFromEditing()
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Private Sub CancelButton_Click(sender As System.Object, e As System.EventArgs) Handles CancelButton.Click

            Try
                Grid.SelectedIndex = NO_ITEM_SELECTED_INDEX
                State.Canceling = True
                If (State.AddingNewRow) Then
                    State.AddingNewRow = False
                    State.searchDV = Nothing
                End If
                ReturnFromEditing()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

#End Region

#Region "Private Methods"

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

            'Put user code to initialize the page here
            Try
                ErrControllerMaster.Clear_Hide()
                SetStateProperties()
                If Not Page.IsPostBack Then
                    SetFormTitle(PAGETITLE)
                    SetFormTab(PAGETAB)
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    SetGridItemStyleColor(Grid)
                    If State.myBO Is Nothing Then
                        State.myBO = New MfgCoverage
                    End If
                    PopulateDropdowns()
                    State.PageIndex = 0
                    SetButtonsState()
                End If
                BindBoPropertiesToGridHeaders()
                If (State.IsEditMode) Then
                    PopulateManufacturerDropDown(Grid.Items(Grid.EditItemIndex))
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
            ShowMissingTranslations(ErrControllerMaster)

            If IsReturningFromChild Then
                Try
                    txtModel.Text = State.Model
                    SetSelectedItem(cboRiskType, State.RiskTypeSearchId)
                    SetSelectedItem(cboManufacturer, State.ManufacturerSearchId)
                    Search()
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.myBO.Id, Grid, State.PageIndex, State.IsEditMode)

                Catch ex As Exception
                    HandleErrors(ex, ErrControllerMaster)
                End Try
            End If
        End Sub

        Private Sub PopulateManufacturerDropDown(dgi As DataGridItem)
            Try
                Dim mfgrDropDown As DropDownList
                Dim equipmentTypeDropDown As DropDownList
                Dim equipementDropDown As DropDownList
                mfgrDropDown = CType(dgi.Cells(MANUFACTURER_COL_IDX).FindControl(MANUFACTURER_LIST_IN_GRID_CONTROL_NAME), DropDownList)
                equipmentTypeDropDown = CType(dgi.Cells(MANUFACTURER_COL_IDX).FindControl(EQUIPMENT_TYPE_LIST_IN_GRID_CONTROL_NAME), DropDownList)
                equipementDropDown = CType(dgi.Cells(MANUFACTURER_COL_IDX).FindControl(EQUIPMENT_LIST_IN_GRID_CONTROL_NAME), DropDownList)

                Dim dt As New DataTable
                dt.Columns.Add("DESCRIPTION")
                dt.Columns.Add("ID")
                Dim dv As New DataView(dt)
                If ((New Guid(mfgrDropDown.SelectedValue) = Guid.Empty) OrElse _
                    (New Guid(equipmentTypeDropDown.SelectedValue) = Guid.Empty)) Then
                    ' BindListControlToDataView(equipementDropDown, dv, "DESCRIPTION", "ID")
                    Dim listcontext As ListContext = New ListContext()
                    listcontext.CompanyGroupId = Guid.Empty
                    listcontext.ManufacturerId = Guid.Empty
                    listcontext.EquipmentTypeId = Guid.Empty
                    Dim equiLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.EquipmentByCompanyGroupEquipmentType, Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                    equipementDropDown.Populate(equiLkl, New PopulateOptions() With
                         {
                        .AddBlankItem = True,
                        .TextFunc = AddressOf .GetCode
                         })
                Else
                    'dv = LookupListNew.GetEquipmentLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, mfgrDropDown.SelectedItem.Value, False, equipmentTypeDropDown.SelectedItem.Value)
                    'BindListControlToDataView(equipementDropDown, dv, "DESCRIPTION", "ID")
                    Dim listcontext As ListContext = New ListContext()
                    listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                    listcontext.ManufacturerId = GetSelectedItem(mfgrDropDown)
                    listcontext.EquipmentTypeId = GetSelectedItem(equipmentTypeDropDown)
                    Dim equiLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.EquipmentByCompanyGroupEquipmentType, Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                    equipementDropDown.Populate(equiLkl, New PopulateOptions() With
                         {
                        .AddBlankItem = True,
                        .TextFunc = AddressOf .GetCode
                         })
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub PopulateGrid()

            Try
                'Refresh the DataView and Call SetPageAndSelectedIndexFromGuid() to go to the Page 
                'where the most recently saved Record exists in the DataView
                If (State.searchDV Is Nothing) Then
                    State.searchDV = GetGridDataView()
                End If
                'Ticket # 748479 - Search grids in Tables tab should not show pop-up message when number of retrieved record is over 1,000
                State.searchDV.Sort = State.SortExpression
                If (State.IsAfterSave) Then
                    State.IsAfterSave = False
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.Id, Grid, State.PageIndex)
                ElseIf (State.IsEditMode) Then
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.Id, Grid, State.PageIndex, State.IsEditMode)
                Else
                    'In a Delete scenario...
                    SetPageAndSelectedIndexFromGuid(State.searchDV, Guid.Empty, Grid, State.PageIndex, State.IsEditMode)
                End If

                Grid.AutoGenerateColumns = False
                Grid.Columns(MANUFACTURER_COL_IDX).SortExpression = MfgCoverage.MfgCoverageSearchDV.COL_MANUFACTURER_NAME
                Grid.Columns(RISK_TYPE_COL_IDX).SortExpression = MfgCoverage.MfgCoverageSearchDV.COL_RISK_TYPE_ENGLISH

                SortAndBindGrid()

            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Private Function GetGridDataView() As DataView

            With State
                Return (MfgCoverage.getList(.ManufacturerSearchId, .CompanyGroupId, .RiskTypeSearchId, .Model))
            End With

        End Function

        Private Sub SetStateProperties()

            If Not IsReturningFromChild Then
                If (cboManufacturer.SelectedItem IsNot Nothing AndAlso cboManufacturer.SelectedItem.Value <> NOTHING_SELECTED_TEXT) Then
                    State.ManufacturerSearchId = GetGuidFromString(cboManufacturer.SelectedItem.Value)
                Else
                    State.ManufacturerSearchId = Guid.Empty
                End If
                If (cboRiskType.SelectedItem IsNot Nothing AndAlso cboRiskType.SelectedItem.Value <> NOTHING_SELECTED_TEXT) Then
                    State.RiskTypeSearchId = GetGuidFromString(cboRiskType.SelectedItem.Value)
                Else
                    State.RiskTypeSearchId = Guid.Empty
                End If
                State.Model = txtModel.Text.Trim
            End If

            State.CompanyId = ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID
            State.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id

        End Sub

        'Used to populate the dropdowns within the Search panel
        Private Sub PopulateDropdowns()

            Dim oDataView As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)

            ' Me.BindListControlToDataView(Me.cboManufacturer, LookupListNew.GetManufacturerLookupList(Me.State.CompanyGroupId), MANUFACTURER_DESCRIPTION_COL_NAME, , True)
            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyGroupId = State.CompanyGroupId
            Dim manufacturerLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ManufacturerByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            cboManufacturer.Populate(manufacturerLkl, New PopulateOptions() With
                            {
                             .AddBlankItem = True,
                             .TextFunc = AddressOf .GetCode
                           })
            ' Me.BindListControlToDataView(Me.cboRiskType, LookupListNew.GetRiskTypeLookupList(Me.State.CompanyGroupId), RISK_TYPE_DESCRIPTION_COL_NAME, , True)
            cboRiskType.Populate(CommonConfigManager.Current.ListManager.GetList("RiskTypeByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext), New PopulateOptions() With
                            {
                             .AddBlankItem = True
                           })
            SetSelectedItem(cboManufacturer, State.ManufacturerSearchId)
            SetSelectedItem(cboRiskType, State.RiskTypeSearchId)

        End Sub

        Private Sub AddNew()

            Dim dv As DataView

            State.searchDV = GetGridDataView()

            State.myBO = New MfgCoverage
            State.Id = State.myBO.Id

            State.searchDV = State.myBO.GetNewDataViewRow(State.searchDV, State.Id)

            Grid.DataSource = State.searchDV

            SetPageAndSelectedIndexFromGuid(State.searchDV, State.Id, Grid, State.PageIndex, State.IsEditMode)

            Grid.DataBind()

            State.PageIndex = Grid.CurrentPageIndex

            SetGridControls(Grid, False)
            Grid.Columns(EXT_WARRANTY_COL_IDX).Visible = False

            'Set focus on the Description TextBox for the EditItemIndex row
            'Me.SetFocusOnEditableFieldInGrid(Me.Grid, Me.DEALER_COL_IDX, Me.Grid.EditItemIndex)

            SetButtonsState()
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
        End Sub

        Private Sub SortAndBindGrid()
            State.PageIndex = Grid.CurrentPageIndex
            Grid.DataSource = State.searchDV
            HighLightSortColumn(Grid, State.SortExpression)
            Grid.DataBind()

            ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)

            ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

            Session("recCount") = State.searchDV.Count

            If Grid.Visible Then
                If (State.AddingNewRow) Then
                    lblRecordCount.Text = (State.searchDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                Else
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
        End Sub

        Private Sub PopulateBOFromForm()

            With State.myBO
                Dim txtModelControl As TextBox = CType(Grid.Items(Grid.EditItemIndex).Cells(MODEL_COL_IDX).FindControl(MODEL_IN_GRID_CONTROL_NAME), TextBox)
                Dim txtMfgWarrantyControl As TextBox = CType(Grid.Items(Grid.EditItemIndex).Cells(MFG_WARRANTY_COL_IDX).FindControl(MFG_WARRANTY_IN_GRID_CONTROL_NAME), TextBox)
                Dim cboManufacturerControl As DropDownList = CType(Grid.Items(Grid.EditItemIndex).Cells(MANUFACTURER_COL_IDX).FindControl(MANUFACTURER_LIST_IN_GRID_CONTROL_NAME), DropDownList)
                Dim cboRiskTypeControl As DropDownList = CType(Grid.Items(Grid.EditItemIndex).Cells(RISK_TYPE_COL_IDX).FindControl(RISK_TYPE_LIST_IN_GRID_CONTROL_NAME), DropDownList)
                Dim cboEquipmentType As DropDownList = CType(Grid.Items(Grid.EditItemIndex).Cells(EQUIPMENT_TYPE_COL_IDX).FindControl(EQUIPMENT_TYPE_LIST_IN_GRID_CONTROL_NAME), DropDownList)
                Dim cboEquipment As DropDownList = CType(Grid.Items(Grid.EditItemIndex).Cells(EQUIPMENT_COL_IDX).FindControl(EQUIPMENT_LIST_IN_GRID_CONTROL_NAME), DropDownList)
                Dim txtMainPartsWarrantyControl As TextBox = CType(Grid.Items(Grid.EditItemIndex).Cells(MFG_MAIN_PARTS_WARRANTY_COL_IDX).FindControl(MFG_MAIN_PARTS_WARRANTY_IN_GRID_CONTROL_NAME), TextBox)

                PopulateBOProperty(State.myBO, "Model", txtModelControl)
                PopulateBOProperty(State.myBO, "MfgWarranty", txtMfgWarrantyControl)
                PopulateBOProperty(State.myBO, "ManufacturerId", cboManufacturerControl)
                PopulateBOProperty(State.myBO, "RiskTypeId", cboRiskTypeControl)
                PopulateBOProperty(State.myBO, "CompanyGroupId", State.CompanyGroupId)
                PopulateBOProperty(State.myBO, "EquipmentTypeId", cboEquipmentType)
                PopulateBOProperty(State.myBO, "EquipmentId", New Guid(Request.Form(cboEquipment.UniqueID)))
                PopulateBOProperty(State.myBO, "MfgMainPartsWarranty", txtMainPartsWarrantyControl)
                If ErrCollection.Count > 0 Then
                    Throw New PopulateBOErrorException
                End If
            End With
        End Sub

        Private Sub ReturnFromEditing()

            Grid.EditItemIndex = NO_ROW_SELECTED_INDEX

            If Grid.PageCount = 0 Then
                'if returning to the "1st time in" screen
                ControlMgr.SetVisibleControl(Me, Grid, False)
            Else
                ControlMgr.SetVisibleControl(Me, Grid, True)
            End If
            SetGridControls(Grid, True)
            Grid.Columns(EXT_WARRANTY_COL_IDX).Visible = True

            State.IsEditMode = False
            PopulateGrid()
            State.PageIndex = Grid.CurrentPageIndex
            SetButtonsState()

        End Sub

        Private Sub SetButtonsState()

            If (State.IsEditMode) Then
                ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, True)
                ControlMgr.SetVisibleControl(Me, CancelButton, True)
                ControlMgr.SetVisibleControl(Me, NewButton_WRITE, False)
                ControlMgr.SetEnableControl(Me, SearchButton, False)
                ControlMgr.SetEnableControl(Me, ClearButton, False)
                MenuEnabled = False
                If (cboPageSize.Visible) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, False)
                End If
            Else
                ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, False)
                ControlMgr.SetVisibleControl(Me, CancelButton, False)
                ControlMgr.SetVisibleControl(Me, NewButton_WRITE, True)
                ControlMgr.SetEnableControl(Me, SearchButton, True)
                ControlMgr.SetEnableControl(Me, ClearButton, True)
                MenuEnabled = True
                If (cboPageSize.Visible) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, True)
                End If
            End If

        End Sub

        Protected Sub ReturnFromCalledPage(url As String, returnPar As Object) Handles MyBase.PageReturn

            If returnPar IsNot Nothing Then
                State.myBO = New MfgCoverage(CType(returnPar, MfgCoverageExtForm.ReturnType).mfgCoverageId)
                State.Model = CType(returnPar, MfgCoverageExtForm.ReturnType).Model
                State.RiskTypeSearchId = New Guid(CType(returnPar, MfgCoverageExtForm.ReturnType).RiskType)
                State.ManufacturerSearchId = New Guid(CType(returnPar, MfgCoverageExtForm.ReturnType).Manufacturer)
                IsReturningFromChild = True

            End If
        End Sub
#End Region

#Region " Datagrid Related "

        'The Binding Logic is here
        Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
            Try
                Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

                If (itemType = ListItemType.Item OrElse
                    itemType = ListItemType.AlternatingItem OrElse
                    itemType = ListItemType.SelectedItem) Then

                    e.Item.Cells(ID_COL_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(MfgCoverage.MfgCoverageSearchDV.COL_MFG_COVERAGE_ID), Byte()))
                    e.Item.Cells(MODEL_COL_IDX).Text = dvRow(MfgCoverage.MfgCoverageSearchDV.COL_MODEL).ToString
                    e.Item.Cells(MFG_WARRANTY_COL_IDX).Text = dvRow(MfgCoverage.MfgCoverageSearchDV.COL_MFG_WARRANTY).ToString
                    e.Item.Cells(MANUFACTURER_COL_IDX).Text = dvRow(MfgCoverage.MfgCoverageSearchDV.COL_MANUFACTURER_NAME).ToString
                    e.Item.Cells(RISK_TYPE_COL_IDX).Text = dvRow(MfgCoverage.MfgCoverageSearchDV.COL_RISK_TYPE_DESCRIPTION).ToString
                    e.Item.Cells(EQUIPMENT_TYPE_COL_IDX).Text = dvRow(MfgCoverage.MfgCoverageSearchDV.COL_EQUIPMENT_TYPE_DESCRIPTION).ToString
                    e.Item.Cells(EQUIPMENT_COL_IDX).Text = dvRow(MfgCoverage.MfgCoverageSearchDV.COL_EQUIPMENT_DESCRIPTION).ToString
                    e.Item.Cells(MFG_MAIN_PARTS_WARRANTY_COL_IDX).Text = dvRow(MfgCoverage.MfgCoverageSearchDV.COL_MFG_MAIN_PARTS_WARRANTY).ToString
                ElseIf (itemType = ListItemType.EditItem) Then
                    e.Item.Cells(ID_COL_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(MfgCoverage.MfgCoverageSearchDV.COL_MFG_COVERAGE_ID), Byte()))
                    CType(e.Item.Cells(MODEL_COL_IDX).FindControl(MODEL_IN_GRID_CONTROL_NAME), TextBox).Text = dvRow(MfgCoverage.MfgCoverageSearchDV.COL_MODEL).ToString
                    CType(e.Item.Cells(MFG_WARRANTY_COL_IDX).FindControl(MFG_WARRANTY_IN_GRID_CONTROL_NAME), TextBox).Text = dvRow(MfgCoverage.MfgCoverageSearchDV.COL_MFG_WARRANTY).ToString
                    ' Me.BindListControlToDataView(CType(e.Item.Cells(Me.MANUFACTURER_COL_IDX).FindControl(MANUFACTURER_LIST_IN_GRID_CONTROL_NAME), DropDownList), LookupListNew.GetManufacturerLookupList(Me.State.CompanyGroupId), MANUFACTURER_DESCRIPTION_COL_NAME, , True)
                    Dim listcontext As ListContext = New ListContext()
                    listcontext.CompanyGroupId = State.CompanyGroupId
                    Dim manufacturerLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ManufacturerByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                    CType(e.Item.Cells(MANUFACTURER_COL_IDX).FindControl(MANUFACTURER_LIST_IN_GRID_CONTROL_NAME), DropDownList).Populate(manufacturerLkl, New PopulateOptions() With
                            {
                             .AddBlankItem = True,
                             .TextFunc = AddressOf .GetCode
                           })
                    SetSelectedItem(CType(e.Item.Cells(MANUFACTURER_COL_IDX).FindControl(MANUFACTURER_LIST_IN_GRID_CONTROL_NAME), DropDownList), State.myBO.ManufacturerId)
                    AddHandler CType(e.Item.Cells(MANUFACTURER_COL_IDX).FindControl(MANUFACTURER_LIST_IN_GRID_CONTROL_NAME), DropDownList).SelectedIndexChanged, AddressOf ManufacturerEquipmentTypeChanged
                    ' Me.BindListControlToDataView(CType(e.Item.Cells(Me.RISK_TYPE_COL_IDX).FindControl(RISK_TYPE_LIST_IN_GRID_CONTROL_NAME), DropDownList), LookupListNew.GetRiskTypeLookupList(Me.State.CompanyGroupId), RISK_TYPE_DESCRIPTION_COL_NAME, , True)
                    CType(e.Item.Cells(RISK_TYPE_COL_IDX).FindControl(RISK_TYPE_LIST_IN_GRID_CONTROL_NAME), DropDownList).Populate(CommonConfigManager.Current.ListManager.GetList("RiskTypeByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext), New PopulateOptions() With
                            {
                             .AddBlankItem = True
                           })
                    SetSelectedItem(CType(e.Item.Cells(RISK_TYPE_COL_IDX).FindControl(RISK_TYPE_LIST_IN_GRID_CONTROL_NAME), DropDownList), State.myBO.RiskTypeId)
                    'Me.BindListControlToDataView(CType(e.Item.Cells(Me.EQUIPMENT_TYPE_COL_IDX).FindControl(EQUIPMENT_TYPE_LIST_IN_GRID_CONTROL_NAME), DropDownList), LookupListNew.GetEquipmentTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)) 'EQPTYPE
                    CType(e.Item.Cells(EQUIPMENT_TYPE_COL_IDX).FindControl(EQUIPMENT_TYPE_LIST_IN_GRID_CONTROL_NAME), DropDownList).Populate(CommonConfigManager.Current.ListManager.GetList("EQPTYPE", Thread.CurrentPrincipal.GetLanguageCode(), listcontext), New PopulateOptions() With
                            {
                             .AddBlankItem = True
                           })
                    SetSelectedItem(CType(e.Item.Cells(EQUIPMENT_TYPE_COL_IDX).FindControl(EQUIPMENT_TYPE_LIST_IN_GRID_CONTROL_NAME), DropDownList), State.myBO.EquipmentTypeId)
                    PopulateManufacturerDropDown(e.Item)
                    SetSelectedItem(CType(e.Item.Cells(EQUIPMENT_COL_IDX).FindControl(EQUIPMENT_LIST_IN_GRID_CONTROL_NAME), DropDownList), State.myBO.EquipmentId)
                    CType(e.Item.Cells(MFG_MAIN_PARTS_WARRANTY_COL_IDX).FindControl(MFG_MAIN_PARTS_WARRANTY_IN_GRID_CONTROL_NAME), TextBox).Text = dvRow(MfgCoverage.MfgCoverageSearchDV.COL_MFG_MAIN_PARTS_WARRANTY).ToString
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Public Sub ManufacturerEquipmentTypeChanged(sender As Object, e As EventArgs)

        End Sub

        Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged

            Try
                If (Not (State.IsEditMode)) Then
                    State.PageIndex = e.NewPageIndex
                    Grid.CurrentPageIndex = State.PageIndex
                    PopulateGrid()
                    Grid.SelectedIndex = NO_ITEM_SELECTED_INDEX
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Protected Sub ItemCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs)

            Try
                Dim index As Integer = e.Item.ItemIndex

                If (e.CommandName = EDIT_COMMAND) Then
                    'Do the Edit here

                    'Set the IsEditMode flag to TRUE
                    State.IsEditMode = True

                    State.Id = New Guid(Grid.Items(e.Item.ItemIndex).Cells(ID_COL_IDX).Text)

                    State.myBO = New MfgCoverage(State.Id)

                    PopulateGrid()

                    State.PageIndex = Grid.CurrentPageIndex

                    'Disable all Edit and Delete icon buttons on the Grid
                    SetGridControls(Grid, False)
                    Grid.Columns(EXT_WARRANTY_COL_IDX).Visible = False

                    SetButtonsState()

                ElseIf (e.CommandName = DELETE_COMMAND) Then
                    'Do the delete here

                    'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                    Grid.SelectedIndex = NO_ROW_SELECTED_INDEX

                    'Save the Id in the Session
                    State.Id = New Guid(Grid.Items(e.Item.ItemIndex).Cells(ID_COL_IDX).Text)
                    State.myBO = New MfgCoverage(State.Id)

                    Try
                        State.myBO.Delete()
                        'Call the Save() method in the Business Object here
                        State.myBO.Save()
                    Catch ex As Exception
                        State.myBO.RejectChanges()
                        Throw ex
                    End Try

                    State.PageIndex = Grid.CurrentPageIndex

                    'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
                    State.IsAfterSave = True

                    State.searchDV = Nothing
                    PopulateGrid()
                    State.PageIndex = Grid.CurrentPageIndex
                ElseIf (e.CommandName = EXT_WARRANTY_COMMAND) Then

                    State.Id = New Guid(Grid.Items(e.Item.ItemIndex).Cells(ID_COL_IDX).Text)

                    callPage(MfgCoverageExtForm.URL, New MfgCoverageExtForm.ReturnType(State.Id, cboRiskType.SelectedValue, cboManufacturer.SelectedValue, txtModel.Text))

                End If

            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Protected Sub ItemBound(source As Object, e As DataGridItemEventArgs) Handles Grid.ItemDataBound
            BaseItemBound(source, e)
        End Sub

        Protected Sub ItemCreated(sender As Object, e As DataGridItemEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.CurrentPageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles Grid.SortCommand

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
                'To handle the requirement of always going to the FIRST page on the Grid whenever the user switches the sorting criterion
                'Set the Me.State.selectedClaimId = Guid.Empty and set Me.State.PageIndex = 0
                State.Id = Guid.Empty
                State.PageIndex = 0

                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Protected Sub BindBoPropertiesToGridHeaders()
            BindBOPropertyToGridHeader(State.myBO, "ManufacturerId", Grid.Columns(MANUFACTURER_COL_IDX))
            BindBOPropertyToGridHeader(State.myBO, "Model", Grid.Columns(MODEL_COL_IDX))
            BindBOPropertyToGridHeader(State.myBO, "MfgWarranty", Grid.Columns(MFG_WARRANTY_COL_IDX))
            BindBOPropertyToGridHeader(State.myBO, "RiskTypeId", Grid.Columns(RISK_TYPE_COL_IDX))
            BindBOPropertyToGridHeader(State.myBO, "MfgMainPartsWarranty", Grid.Columns(MFG_MAIN_PARTS_WARRANTY_COL_IDX))
            ClearGridHeadersAndLabelsErrSign()
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(grid As DataGrid, cellPosition As Integer, itemIndex As Integer)
            Dim ctrlManufacturer As DropDownList = CType(grid.Items(itemIndex).Cells(cellPosition + 1).FindControl(MANUFACTURER_LIST_IN_GRID_CONTROL_NAME), DropDownList)
            Dim ctrlRiskType As DropDownList = CType(grid.Items(itemIndex).Cells(cellPosition + 2).FindControl(RISK_TYPE_LIST_IN_GRID_CONTROL_NAME), DropDownList)
            Dim ctrlModel As TextBox = CType(grid.Items(itemIndex).Cells(cellPosition + 2).FindControl(MODEL_IN_GRID_CONTROL_NAME), TextBox)
            Dim ctrlMfgWarranty As TextBox = CType(grid.Items(itemIndex).Cells(cellPosition + 2).FindControl(MFG_WARRANTY_IN_GRID_CONTROL_NAME), TextBox)
            Dim ctrlMfgMainPartsWarranty As TextBox = CType(grid.Items(itemIndex).Cells(cellPosition + 2).FindControl(MFG_MAIN_PARTS_WARRANTY_IN_GRID_CONTROL_NAME), TextBox)
        End Sub


#End Region


    End Class

End Namespace
