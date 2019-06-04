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
                IsEditing = (Me.Grid.EditItemIndex > NO_ROW_SELECTED_INDEX)
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

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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

        Private Sub SearchButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchButton.Click

            Search()

        End Sub

        Private Sub Search()
            Try
                Me.State.PageIndex = 0
                Me.State.Id = Guid.Empty
                Me.State.IsGridVisible = True
                Me.State.searchDV = Nothing
                PopulateGrid()
                Me.State.PageIndex = Grid.CurrentPageIndex
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Private Sub ClearButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearButton.Click

            ClearSearchCriteria()

        End Sub

        Private Sub ClearSearchCriteria()

            Try
                cboManufacturer.SelectedIndex = 0
                cboRiskType.SelectedIndex = 0
                txtModel.Text = ""
                'Update Page State
                With Me.State
                    .ManufacturerSearchId = Guid.Empty
                    .RiskTypeSearchId = Guid.Empty
                    .Model = ""
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Private Sub NewButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewButton_WRITE.Click

            Try
                Me.State.IsEditMode = True
                Me.State.IsGridVisible = True
                Me.State.AddingNewRow = True
                AddNew()
                SetButtonsState()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Private Sub SaveButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton_WRITE.Click

            Try
                PopulateBOFromForm()
                If (Me.State.myBO.IsDirty) Then
                    Me.State.myBO.Save()
                    Me.State.IsAfterSave = True
                    Me.State.AddingNewRow = False
                    Me.AddInfoMsg(Me.MSG_RECORD_SAVED_OK)
                    Me.State.searchDV = Nothing
                    Me.ReturnFromEditing()
                Else
                    Me.AddInfoMsg(Me.MSG_RECORD_NOT_SAVED)
                    Me.ReturnFromEditing()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Private Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelButton.Click

            Try
                Me.Grid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
                Me.State.Canceling = True
                If (Me.State.AddingNewRow) Then
                    Me.State.AddingNewRow = False
                    Me.State.searchDV = Nothing
                End If
                ReturnFromEditing()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

#End Region

#Region "Private Methods"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            'Put user code to initialize the page here
            Try
                ErrControllerMaster.Clear_Hide()
                Me.SetStateProperties()
                If Not Page.IsPostBack Then
                    Me.SetFormTitle(PAGETITLE)
                    Me.SetFormTab(PAGETAB)
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    Me.SetGridItemStyleColor(Me.Grid)
                    If Me.State.myBO Is Nothing Then
                        Me.State.myBO = New MfgCoverage
                    End If
                    PopulateDropdowns()
                    Me.State.PageIndex = 0
                    SetButtonsState()
                End If
                BindBoPropertiesToGridHeaders()
                If (Me.State.IsEditMode) Then
                    PopulateManufacturerDropDown(Me.Grid.Items(Me.Grid.EditItemIndex))
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
            Me.ShowMissingTranslations(ErrControllerMaster)

            If Me.IsReturningFromChild Then
                Try
                    Me.txtModel.Text = Me.State.Model
                    Me.SetSelectedItem(Me.cboRiskType, Me.State.RiskTypeSearchId)
                    Me.SetSelectedItem(Me.cboManufacturer, Me.State.ManufacturerSearchId)
                    Search()
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.myBO.Id, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)

                Catch ex As Exception
                    Me.HandleErrors(ex, ErrControllerMaster)
                End Try
            End If
        End Sub

        Private Sub PopulateManufacturerDropDown(ByVal dgi As DataGridItem)
            Try
                Dim mfgrDropDown As DropDownList
                Dim equipmentTypeDropDown As DropDownList
                Dim equipementDropDown As DropDownList
                mfgrDropDown = CType(dgi.Cells(Me.MANUFACTURER_COL_IDX).FindControl(MANUFACTURER_LIST_IN_GRID_CONTROL_NAME), DropDownList)
                equipmentTypeDropDown = CType(dgi.Cells(Me.MANUFACTURER_COL_IDX).FindControl(EQUIPMENT_TYPE_LIST_IN_GRID_CONTROL_NAME), DropDownList)
                equipementDropDown = CType(dgi.Cells(Me.MANUFACTURER_COL_IDX).FindControl(EQUIPMENT_LIST_IN_GRID_CONTROL_NAME), DropDownList)

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
                Me.HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub PopulateGrid()

            Try
                'Refresh the DataView and Call SetPageAndSelectedIndexFromGuid() to go to the Page 
                'where the most recently saved Record exists in the DataView
                If (Me.State.searchDV Is Nothing) Then
                    Me.State.searchDV = GetGridDataView()
                End If
                'Ticket # 748479 - Search grids in Tables tab should not show pop-up message when number of retrieved record is over 1,000
                Me.State.searchDV.Sort = Me.State.SortExpression
                If (Me.State.IsAfterSave) Then
                    Me.State.IsAfterSave = False
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.Id, Me.Grid, Me.State.PageIndex)
                ElseIf (Me.State.IsEditMode) Then
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.Id, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)
                Else
                    'In a Delete scenario...
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Guid.Empty, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)
                End If

                Me.Grid.AutoGenerateColumns = False
                Me.Grid.Columns(Me.MANUFACTURER_COL_IDX).SortExpression = MfgCoverage.MfgCoverageSearchDV.COL_MANUFACTURER_NAME
                Me.Grid.Columns(Me.RISK_TYPE_COL_IDX).SortExpression = MfgCoverage.MfgCoverageSearchDV.COL_RISK_TYPE_ENGLISH

                Me.SortAndBindGrid()

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Private Function GetGridDataView() As DataView

            With State
                Return (MfgCoverage.getList(.ManufacturerSearchId, .CompanyGroupId, .RiskTypeSearchId, .Model))
            End With

        End Function

        Private Sub SetStateProperties()

            If Not Me.IsReturningFromChild Then
                If (Not cboManufacturer.SelectedItem Is Nothing AndAlso cboManufacturer.SelectedItem.Value <> Me.NOTHING_SELECTED_TEXT) Then
                    Me.State.ManufacturerSearchId = Me.GetGuidFromString(cboManufacturer.SelectedItem.Value)
                Else
                    Me.State.ManufacturerSearchId = Guid.Empty
                End If
                If (Not cboRiskType.SelectedItem Is Nothing AndAlso cboRiskType.SelectedItem.Value <> Me.NOTHING_SELECTED_TEXT) Then
                    Me.State.RiskTypeSearchId = Me.GetGuidFromString(cboRiskType.SelectedItem.Value)
                Else
                    Me.State.RiskTypeSearchId = Guid.Empty
                End If
                Me.State.Model = txtModel.Text.Trim
            End If

            Me.State.CompanyId = ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID
            Me.State.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id

        End Sub

        'Used to populate the dropdowns within the Search panel
        Private Sub PopulateDropdowns()

            Dim oDataView As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)

            ' Me.BindListControlToDataView(Me.cboManufacturer, LookupListNew.GetManufacturerLookupList(Me.State.CompanyGroupId), MANUFACTURER_DESCRIPTION_COL_NAME, , True)
            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyGroupId = Me.State.CompanyGroupId
            Dim manufacturerLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ManufacturerByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            Me.cboManufacturer.Populate(manufacturerLkl, New PopulateOptions() With
                            {
                             .AddBlankItem = True,
                             .TextFunc = AddressOf .GetCode
                           })
            ' Me.BindListControlToDataView(Me.cboRiskType, LookupListNew.GetRiskTypeLookupList(Me.State.CompanyGroupId), RISK_TYPE_DESCRIPTION_COL_NAME, , True)
            Me.cboRiskType.Populate(CommonConfigManager.Current.ListManager.GetList("RiskTypeByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext), New PopulateOptions() With
                            {
                             .AddBlankItem = True
                           })
            Me.SetSelectedItem(Me.cboManufacturer, Me.State.ManufacturerSearchId)
            Me.SetSelectedItem(Me.cboRiskType, Me.State.RiskTypeSearchId)

        End Sub

        Private Sub AddNew()

            Dim dv As DataView

            Me.State.searchDV = GetGridDataView()

            Me.State.myBO = New MfgCoverage
            Me.State.Id = Me.State.myBO.Id

            Me.State.searchDV = Me.State.myBO.GetNewDataViewRow(Me.State.searchDV, Me.State.Id)

            Grid.DataSource = Me.State.searchDV

            Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.Id, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)

            Grid.DataBind()

            Me.State.PageIndex = Grid.CurrentPageIndex

            SetGridControls(Me.Grid, False)
            Me.Grid.Columns(EXT_WARRANTY_COL_IDX).Visible = False

            'Set focus on the Description TextBox for the EditItemIndex row
            'Me.SetFocusOnEditableFieldInGrid(Me.Grid, Me.DEALER_COL_IDX, Me.Grid.EditItemIndex)

            Me.SetButtonsState()
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
        End Sub

        Private Sub SortAndBindGrid()
            Me.State.PageIndex = Me.Grid.CurrentPageIndex
            Me.Grid.DataSource = Me.State.searchDV
            HighLightSortColumn(Grid, Me.State.SortExpression)
            Me.Grid.DataBind()

            ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)

            ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)

            Session("recCount") = Me.State.searchDV.Count

            If Me.Grid.Visible Then
                If (Me.State.AddingNewRow) Then
                    Me.lblRecordCount.Text = (Me.State.searchDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                Else
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
        End Sub

        Private Sub PopulateBOFromForm()

            With Me.State.myBO
                Dim txtModelControl As TextBox = CType(Me.Grid.Items(Me.Grid.EditItemIndex).Cells(Me.MODEL_COL_IDX).FindControl(Me.MODEL_IN_GRID_CONTROL_NAME), TextBox)
                Dim txtMfgWarrantyControl As TextBox = CType(Me.Grid.Items(Me.Grid.EditItemIndex).Cells(Me.MFG_WARRANTY_COL_IDX).FindControl(Me.MFG_WARRANTY_IN_GRID_CONTROL_NAME), TextBox)
                Dim cboManufacturerControl As DropDownList = CType(Grid.Items(Grid.EditItemIndex).Cells(Me.MANUFACTURER_COL_IDX).FindControl(MANUFACTURER_LIST_IN_GRID_CONTROL_NAME), DropDownList)
                Dim cboRiskTypeControl As DropDownList = CType(Grid.Items(Grid.EditItemIndex).Cells(Me.RISK_TYPE_COL_IDX).FindControl(RISK_TYPE_LIST_IN_GRID_CONTROL_NAME), DropDownList)
                Dim cboEquipmentType As DropDownList = CType(Grid.Items(Grid.EditItemIndex).Cells(Me.EQUIPMENT_TYPE_COL_IDX).FindControl(EQUIPMENT_TYPE_LIST_IN_GRID_CONTROL_NAME), DropDownList)
                Dim cboEquipment As DropDownList = CType(Grid.Items(Grid.EditItemIndex).Cells(Me.EQUIPMENT_COL_IDX).FindControl(EQUIPMENT_LIST_IN_GRID_CONTROL_NAME), DropDownList)
                Dim txtMainPartsWarrantyControl As TextBox = CType(Me.Grid.Items(Me.Grid.EditItemIndex).Cells(Me.MFG_MAIN_PARTS_WARRANTY_COL_IDX).FindControl(Me.MFG_MAIN_PARTS_WARRANTY_IN_GRID_CONTROL_NAME), TextBox)

                Me.PopulateBOProperty(Me.State.myBO, "Model", txtModelControl)
                Me.PopulateBOProperty(Me.State.myBO, "MfgWarranty", txtMfgWarrantyControl)
                Me.PopulateBOProperty(Me.State.myBO, "ManufacturerId", cboManufacturerControl)
                Me.PopulateBOProperty(Me.State.myBO, "RiskTypeId", cboRiskTypeControl)
                Me.PopulateBOProperty(Me.State.myBO, "CompanyGroupId", Me.State.CompanyGroupId)
                Me.PopulateBOProperty(Me.State.myBO, "EquipmentTypeId", cboEquipmentType)
                Me.PopulateBOProperty(Me.State.myBO, "EquipmentId", New Guid(Request.Form(cboEquipment.UniqueID)))
                Me.PopulateBOProperty(Me.State.myBO, "MfgMainPartsWarranty", txtMainPartsWarrantyControl)
                If Me.ErrCollection.Count > 0 Then
                    Throw New PopulateBOErrorException
                End If
            End With
        End Sub

        Private Sub ReturnFromEditing()

            Grid.EditItemIndex = NO_ROW_SELECTED_INDEX

            If Me.Grid.PageCount = 0 Then
                'if returning to the "1st time in" screen
                ControlMgr.SetVisibleControl(Me, Grid, False)
            Else
                ControlMgr.SetVisibleControl(Me, Grid, True)
            End If
            SetGridControls(Grid, True)
            Me.Grid.Columns(EXT_WARRANTY_COL_IDX).Visible = True

            Me.State.IsEditMode = False
            Me.PopulateGrid()
            Me.State.PageIndex = Grid.CurrentPageIndex
            SetButtonsState()

        End Sub

        Private Sub SetButtonsState()

            If (Me.State.IsEditMode) Then
                ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, True)
                ControlMgr.SetVisibleControl(Me, CancelButton, True)
                ControlMgr.SetVisibleControl(Me, NewButton_WRITE, False)
                ControlMgr.SetEnableControl(Me, SearchButton, False)
                ControlMgr.SetEnableControl(Me, ClearButton, False)
                Me.MenuEnabled = False
                If (Me.cboPageSize.Visible) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, False)
                End If
            Else
                ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, False)
                ControlMgr.SetVisibleControl(Me, CancelButton, False)
                ControlMgr.SetVisibleControl(Me, NewButton_WRITE, True)
                ControlMgr.SetEnableControl(Me, SearchButton, True)
                ControlMgr.SetEnableControl(Me, ClearButton, True)
                Me.MenuEnabled = True
                If (Me.cboPageSize.Visible) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, True)
                End If
            End If

        End Sub

        Protected Sub ReturnFromCalledPage(ByVal url As String, ByVal returnPar As Object) Handles MyBase.PageReturn

            If Not returnPar Is Nothing Then
                Me.State.myBO = New MfgCoverage(CType(returnPar, MfgCoverageExtForm.ReturnType).mfgCoverageId)
                Me.State.Model = CType(returnPar, MfgCoverageExtForm.ReturnType).Model
                Me.State.RiskTypeSearchId = New Guid(CType(returnPar, MfgCoverageExtForm.ReturnType).RiskType)
                Me.State.ManufacturerSearchId = New Guid(CType(returnPar, MfgCoverageExtForm.ReturnType).Manufacturer)
                IsReturningFromChild = True

            End If
        End Sub
#End Region

#Region " Datagrid Related "

        'The Binding Logic is here
        Private Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
            Try
                Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

                If (itemType = ListItemType.Item OrElse
                    itemType = ListItemType.AlternatingItem OrElse
                    itemType = ListItemType.SelectedItem) Then

                    e.Item.Cells(Me.ID_COL_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(MfgCoverage.MfgCoverageSearchDV.COL_MFG_COVERAGE_ID), Byte()))
                    e.Item.Cells(Me.MODEL_COL_IDX).Text = dvRow(MfgCoverage.MfgCoverageSearchDV.COL_MODEL).ToString
                    e.Item.Cells(Me.MFG_WARRANTY_COL_IDX).Text = dvRow(MfgCoverage.MfgCoverageSearchDV.COL_MFG_WARRANTY).ToString
                    e.Item.Cells(Me.MANUFACTURER_COL_IDX).Text = dvRow(MfgCoverage.MfgCoverageSearchDV.COL_MANUFACTURER_NAME).ToString
                    e.Item.Cells(Me.RISK_TYPE_COL_IDX).Text = dvRow(MfgCoverage.MfgCoverageSearchDV.COL_RISK_TYPE_DESCRIPTION).ToString
                    e.Item.Cells(Me.EQUIPMENT_TYPE_COL_IDX).Text = dvRow(MfgCoverage.MfgCoverageSearchDV.COL_EQUIPMENT_TYPE_DESCRIPTION).ToString
                    e.Item.Cells(Me.EQUIPMENT_COL_IDX).Text = dvRow(MfgCoverage.MfgCoverageSearchDV.COL_EQUIPMENT_DESCRIPTION).ToString
                    e.Item.Cells(Me.MFG_MAIN_PARTS_WARRANTY_COL_IDX).Text = dvRow(MfgCoverage.MfgCoverageSearchDV.COL_MFG_MAIN_PARTS_WARRANTY).ToString
                ElseIf (itemType = ListItemType.EditItem) Then
                    e.Item.Cells(Me.ID_COL_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(MfgCoverage.MfgCoverageSearchDV.COL_MFG_COVERAGE_ID), Byte()))
                    CType(e.Item.Cells(Me.MODEL_COL_IDX).FindControl(MODEL_IN_GRID_CONTROL_NAME), TextBox).Text = dvRow(MfgCoverage.MfgCoverageSearchDV.COL_MODEL).ToString
                    CType(e.Item.Cells(Me.MFG_WARRANTY_COL_IDX).FindControl(MFG_WARRANTY_IN_GRID_CONTROL_NAME), TextBox).Text = dvRow(MfgCoverage.MfgCoverageSearchDV.COL_MFG_WARRANTY).ToString
                    ' Me.BindListControlToDataView(CType(e.Item.Cells(Me.MANUFACTURER_COL_IDX).FindControl(MANUFACTURER_LIST_IN_GRID_CONTROL_NAME), DropDownList), LookupListNew.GetManufacturerLookupList(Me.State.CompanyGroupId), MANUFACTURER_DESCRIPTION_COL_NAME, , True)
                    Dim listcontext As ListContext = New ListContext()
                    listcontext.CompanyGroupId = Me.State.CompanyGroupId
                    Dim manufacturerLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ManufacturerByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                    CType(e.Item.Cells(Me.MANUFACTURER_COL_IDX).FindControl(MANUFACTURER_LIST_IN_GRID_CONTROL_NAME), DropDownList).Populate(manufacturerLkl, New PopulateOptions() With
                            {
                             .AddBlankItem = True,
                             .TextFunc = AddressOf .GetCode
                           })
                    Me.SetSelectedItem(CType(e.Item.Cells(Me.MANUFACTURER_COL_IDX).FindControl(MANUFACTURER_LIST_IN_GRID_CONTROL_NAME), DropDownList), Me.State.myBO.ManufacturerId)
                    AddHandler CType(e.Item.Cells(Me.MANUFACTURER_COL_IDX).FindControl(MANUFACTURER_LIST_IN_GRID_CONTROL_NAME), DropDownList).SelectedIndexChanged, AddressOf ManufacturerEquipmentTypeChanged
                    ' Me.BindListControlToDataView(CType(e.Item.Cells(Me.RISK_TYPE_COL_IDX).FindControl(RISK_TYPE_LIST_IN_GRID_CONTROL_NAME), DropDownList), LookupListNew.GetRiskTypeLookupList(Me.State.CompanyGroupId), RISK_TYPE_DESCRIPTION_COL_NAME, , True)
                    CType(e.Item.Cells(Me.RISK_TYPE_COL_IDX).FindControl(RISK_TYPE_LIST_IN_GRID_CONTROL_NAME), DropDownList).Populate(CommonConfigManager.Current.ListManager.GetList("RiskTypeByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext), New PopulateOptions() With
                            {
                             .AddBlankItem = True
                           })
                    Me.SetSelectedItem(CType(e.Item.Cells(Me.RISK_TYPE_COL_IDX).FindControl(RISK_TYPE_LIST_IN_GRID_CONTROL_NAME), DropDownList), Me.State.myBO.RiskTypeId)
                    'Me.BindListControlToDataView(CType(e.Item.Cells(Me.EQUIPMENT_TYPE_COL_IDX).FindControl(EQUIPMENT_TYPE_LIST_IN_GRID_CONTROL_NAME), DropDownList), LookupListNew.GetEquipmentTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)) 'EQPTYPE
                    CType(e.Item.Cells(Me.EQUIPMENT_TYPE_COL_IDX).FindControl(EQUIPMENT_TYPE_LIST_IN_GRID_CONTROL_NAME), DropDownList).Populate(CommonConfigManager.Current.ListManager.GetList("EQPTYPE", Thread.CurrentPrincipal.GetLanguageCode(), listcontext), New PopulateOptions() With
                            {
                             .AddBlankItem = True
                           })
                    Me.SetSelectedItem(CType(e.Item.Cells(Me.EQUIPMENT_TYPE_COL_IDX).FindControl(EQUIPMENT_TYPE_LIST_IN_GRID_CONTROL_NAME), DropDownList), Me.State.myBO.EquipmentTypeId)
                    PopulateManufacturerDropDown(e.Item)
                    Me.SetSelectedItem(CType(e.Item.Cells(Me.EQUIPMENT_COL_IDX).FindControl(EQUIPMENT_LIST_IN_GRID_CONTROL_NAME), DropDownList), Me.State.myBO.EquipmentId)
                    CType(e.Item.Cells(Me.MFG_MAIN_PARTS_WARRANTY_COL_IDX).FindControl(MFG_MAIN_PARTS_WARRANTY_IN_GRID_CONTROL_NAME), TextBox).Text = dvRow(MfgCoverage.MfgCoverageSearchDV.COL_MFG_MAIN_PARTS_WARRANTY).ToString
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Public Sub ManufacturerEquipmentTypeChanged(ByVal sender As Object, ByVal e As EventArgs)

        End Sub

        Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged

            Try
                If (Not (Me.State.IsEditMode)) Then
                    Me.State.PageIndex = e.NewPageIndex
                    Me.Grid.CurrentPageIndex = Me.State.PageIndex
                    Me.PopulateGrid()
                    Me.Grid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Protected Sub ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)

            Try
                Dim index As Integer = e.Item.ItemIndex

                If (e.CommandName = Me.EDIT_COMMAND) Then
                    'Do the Edit here

                    'Set the IsEditMode flag to TRUE
                    Me.State.IsEditMode = True

                    Me.State.Id = New Guid(Me.Grid.Items(e.Item.ItemIndex).Cells(Me.ID_COL_IDX).Text)

                    Me.State.myBO = New MfgCoverage(Me.State.Id)

                    Me.PopulateGrid()

                    Me.State.PageIndex = Grid.CurrentPageIndex

                    'Disable all Edit and Delete icon buttons on the Grid
                    SetGridControls(Me.Grid, False)
                    Me.Grid.Columns(EXT_WARRANTY_COL_IDX).Visible = False

                    Me.SetButtonsState()

                ElseIf (e.CommandName = Me.DELETE_COMMAND) Then
                    'Do the delete here

                    'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                    Grid.SelectedIndex = Me.NO_ROW_SELECTED_INDEX

                    'Save the Id in the Session
                    Me.State.Id = New Guid(Me.Grid.Items(e.Item.ItemIndex).Cells(Me.ID_COL_IDX).Text)
                    Me.State.myBO = New MfgCoverage(Me.State.Id)

                    Try
                        Me.State.myBO.Delete()
                        'Call the Save() method in the Business Object here
                        Me.State.myBO.Save()
                    Catch ex As Exception
                        Me.State.myBO.RejectChanges()
                        Throw ex
                    End Try

                    Me.State.PageIndex = Grid.CurrentPageIndex

                    'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
                    Me.State.IsAfterSave = True

                    Me.State.searchDV = Nothing
                    PopulateGrid()
                    Me.State.PageIndex = Grid.CurrentPageIndex
                ElseIf (e.CommandName = Me.EXT_WARRANTY_COMMAND) Then

                    Me.State.Id = New Guid(Me.Grid.Items(e.Item.ItemIndex).Cells(Me.ID_COL_IDX).Text)

                    Me.callPage(MfgCoverageExtForm.URL, New MfgCoverageExtForm.ReturnType(Me.State.Id, Me.cboRiskType.SelectedValue, Me.cboManufacturer.SelectedValue, txtModel.Text))

                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Protected Sub ItemBound(ByVal source As Object, ByVal e As DataGridItemEventArgs) Handles Grid.ItemDataBound
            BaseItemBound(source, e)
        End Sub

        Protected Sub ItemCreated(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.CurrentPageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                Me.State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles Grid.SortCommand

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
                'To handle the requirement of always going to the FIRST page on the Grid whenever the user switches the sorting criterion
                'Set the Me.State.selectedClaimId = Guid.Empty and set Me.State.PageIndex = 0
                Me.State.Id = Guid.Empty
                Me.State.PageIndex = 0

                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Protected Sub BindBoPropertiesToGridHeaders()
            Me.BindBOPropertyToGridHeader(Me.State.myBO, "ManufacturerId", Me.Grid.Columns(Me.MANUFACTURER_COL_IDX))
            Me.BindBOPropertyToGridHeader(Me.State.myBO, "Model", Me.Grid.Columns(Me.MODEL_COL_IDX))
            Me.BindBOPropertyToGridHeader(Me.State.myBO, "MfgWarranty", Me.Grid.Columns(Me.MFG_WARRANTY_COL_IDX))
            Me.BindBOPropertyToGridHeader(Me.State.myBO, "RiskTypeId", Me.Grid.Columns(Me.RISK_TYPE_COL_IDX))
            Me.BindBOPropertyToGridHeader(Me.State.myBO, "MfgMainPartsWarranty", Me.Grid.Columns(Me.MFG_MAIN_PARTS_WARRANTY_COL_IDX))
            Me.ClearGridHeadersAndLabelsErrSign()
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(ByVal grid As DataGrid, ByVal cellPosition As Integer, ByVal itemIndex As Integer)
            Dim ctrlManufacturer As DropDownList = CType(grid.Items(itemIndex).Cells(cellPosition + 1).FindControl(MANUFACTURER_LIST_IN_GRID_CONTROL_NAME), DropDownList)
            Dim ctrlRiskType As DropDownList = CType(grid.Items(itemIndex).Cells(cellPosition + 2).FindControl(RISK_TYPE_LIST_IN_GRID_CONTROL_NAME), DropDownList)
            Dim ctrlModel As TextBox = CType(grid.Items(itemIndex).Cells(cellPosition + 2).FindControl(MODEL_IN_GRID_CONTROL_NAME), TextBox)
            Dim ctrlMfgWarranty As TextBox = CType(grid.Items(itemIndex).Cells(cellPosition + 2).FindControl(MFG_WARRANTY_IN_GRID_CONTROL_NAME), TextBox)
            Dim ctrlMfgMainPartsWarranty As TextBox = CType(grid.Items(itemIndex).Cells(cellPosition + 2).FindControl(MFG_MAIN_PARTS_WARRANTY_IN_GRID_CONTROL_NAME), TextBox)
        End Sub


#End Region


    End Class

End Namespace
