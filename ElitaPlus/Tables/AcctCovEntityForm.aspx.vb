﻿
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Tables
    Partial Class AcctCovEntityForm
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

#End Region

#Region "Properties"

        Public ReadOnly Property IsEditing() As Boolean
            Get
                IsEditing = (grdView.EditIndex > NO_ROW_SELECTED_INDEX)
            End Get
        End Property

#End Region

#Region "Page State"
        Class MyState
            Public PageIndex As Integer = 0
            'Public MyBO As AcctCompany = New AcctCompany
            Public MyBO As AcctCovEntity
            Public MyAcctCompany As ArrayList = New ArrayList
            Public Id As Guid
            Public IsGridVisible As Boolean
            Public IsAfterSave As Boolean
            Public IsEditMode As Boolean
            Public AddingNewRow As Boolean
            Public Canceling As Boolean
            Public searchDV As DataView = Nothing
            Public SortExpression As String = AcctCompany.AcctCompanyDV.COL_DESCRIPTION
            Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
            Public bnoRow As Boolean = False
            Public YESNOdv As DataView
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public dvRegion As DataView = Nothing
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

        Private Const COL_ID As Integer = 2
        Private Const COL_COVERAGETYPE As Integer = 3
        Private Const COL_BUSINESS_ENTITY As Integer = 4
        Private Const COL_BUSINESS_UNIT As Integer = 5
        Private Const COL_REGION As Integer = 7
        Private Const COL_COVERAGETYPECODE As Integer = 6

        Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
        Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"


        Private Const ID_CONTROL_NAME As String = "IdLabel"
        Private Const COVERAGE_TYPE_CONTROL_NAME As String = "CoverageTypeDropDown"
        Private Const COVERAGE_TYPE_LABEL_CONTROL_NAME As String = "CoverageTypeLabel"
        Private Const BUSINESS_ENTITY_CONTROL_NAME As String = "BusinessEntityDropDown"
        Private Const BUSINESS_ENTITY_LABEL_CONTROL_NAME As String = "BusinessEntityLabel"
        Private Const BUSINESS_UNIT_CONTROL_NAME As String = "BusinessUnitDropDown"
        Private Const BUSINESS_UNIT_LABEL_CONTROL_NAME As String = "BusinessUnitLabel"
        Private Const REGION_LABEL_CONTROL_NAME As String = "RegionLabel"
        Private Const REGION_CONTROL_NAME As String = "RegionDropDown"
        Private Const ACCT_COVERAGE_TYPE_CONTROL_NAME As String = "AcctCoverageTypeText"
        Private Const ACCT_COVERAGE_TYPE_LABEL_CONTROL_NAME As String = "AcctCoverageTypeLabel"

        Private Const EDIT_COMMAND As String = "EditRecord"
        Private Const DELETE_COMMAND As String = "DeleteRecord"
        Private Const SORT_COMMAND As String = "Sort"

        Private Const YESNO As String = "YESNO"
        Private Const YES_STRING As String = "Y"

        Private Const NO_ROW_SELECTED_INDEX As Integer = -1

        Public Const PAGETITLE As String = "COVERAGE_ENTITY"
        Public Const PAGETAB As String = "TABLES"

#End Region

#Region "Button Click Handlers"

        Private Sub SearchButton_Click(sender As System.Object, e As System.EventArgs) Handles SearchButton.Click
            Try
                If SearchAcctCompanyDropdownList.SelectedIndex = 0 AndAlso SearchAcctCompanyDropdownList.Items.Count > 1 Then
                    ErrControllerMaster.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_SELECTION)
                    Exit Sub
                End If
                State.IsGridVisible = True
                State.searchDV = Nothing
                PopulateGrid()
                'Me.State.PageIndex = Me.grdView.CurrentPageIndex
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Private Sub ClearButton_Click(sender As System.Object, e As System.EventArgs) Handles ClearButton.Click

            Try
                ClearSearchCriteria()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Private Sub ClearSearchCriteria()

            Try
                SearchAcctCompanyDropdownList.ClearSelection()

            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Private Sub NewButton_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles NewButton_WRITE.Click

            Try

                If SearchAcctCompanyDropdownList.SelectedIndex = 0 AndAlso SearchAcctCompanyDropdownList.Items.Count > 1 Then
                    ErrControllerMaster.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_SELECTION)
                    Exit Sub
                End If

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

                AssignBOFromSelectedRecord()
                If (State.MyBO.IsDirty) Then
                    State.MyBO.Save()
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
                grdView.SelectedIndex = NO_ITEM_SELECTED_INDEX
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

                SetFormTitle(PAGETITLE)
                SetFormTab(PAGETAB)

                SetStateProperties()
                If Not Page.IsPostBack Then
                    SortDirection = AcctCovEntity.COL_COVERAGE_TYPE
                    SetDefaultButton(SearchAcctCompanyDropdownList, SearchButton)
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    SetGridItemStyleColor(grdView)
                    State.PageIndex = 0
                    If State.MyBO Is Nothing Then
                        State.MyBO = New AcctCovEntity
                    End If
                    SetButtonsState()
                    TranslateGridHeader(grdView)
                    TranslateGridControls(grdView)
                    PopulateAll()
                Else
                    CheckIfComingFromDeleteConfirm()
                End If
                BindBoPropertiesToGridHeaders()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
            ShowMissingTranslations(ErrControllerMaster)
        End Sub

        Private Sub PopulateAll()

            PopulateAcctCompanyDropdown(SearchAcctCompanyDropdownList)


        End Sub

        Private Sub PopulateAcctCompanyDropdown(listCtl As System.Web.UI.WebControls.DropDownList)

            Dim numCos As Integer = ElitaPlusIdentity.Current.ActiveUser.AccountingCompanies.Length

            If numCos > 0 Then
                If numCos > 1 Then
                    listCtl.Items.Add(New System.Web.UI.WebControls.ListItem("", Guid.Empty.ToString))
                    State.MyAcctCompany.Add(Guid.Empty)
                Else
                    listCtl.Enabled = False

                End If

                For Each _acctCo As AcctCompany In ElitaPlusIdentity.Current.ActiveUser.AccountingCompanies
                    If Not _acctCo.IsNew Then
                        listCtl.Items.Add(New System.Web.UI.WebControls.ListItem(_acctCo.Description, _acctCo.Id.ToString))
                        State.MyAcctCompany.Add(_acctCo.Id)
                    Else
                        ErrControllerMaster.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.GUI_ACCOUNTING_COMPANIES_NOT_FOUND_ERR)
                        Exit Sub
                    End If
                Next

                SetSelectedItem(listCtl, listCtl.Items(0).Value)
            End If
        End Sub

        Private Sub PopulateGrid()

            Dim dv As DataView
            Dim oAcctCompany As AcctCompany

            Try

                If (State.searchDV Is Nothing) Then
                    State.searchDV = GetDV()
                End If
                State.searchDV.Sort = SortDirection

                oAcctCompany = New AcctCompany(GetSelectedItem(SearchAcctCompanyDropdownList))
                If oAcctCompany IsNot Nothing Then
                    Dim dvYESNO As DataView = LookupListNew.GetYesNoLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                    If LookupListNew.GetIdFromCode(dvYESNO, YES_STRING).Equals(oAcctCompany.CoverageEntityByRegion) Then
                        grdView.Columns(COL_REGION).Visible = True
                    Else
                        grdView.Columns(COL_REGION).Visible = False
                    End If
                End If


                If (State.IsAfterSave) Then
                    State.IsAfterSave = False
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.Id, grdView, State.PageIndex)
                ElseIf (State.IsEditMode) Then
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.Id, grdView, State.PageIndex, State.IsEditMode)
                Else
                    SetPageAndSelectedIndexFromGuid(State.searchDV, Guid.Empty, grdView, State.PageIndex)
                End If

                'TODO
                grdView.AutoGenerateColumns = False
                'Me.grdView.Columns(Me.DESCRIPTION_COL).SortExpression = AcctCompany.AcctCompanyDV.COL_DESCRIPTION
                'Me.grdView.Columns(Me.CODE_COL).SortExpression = AcctCompany.AcctCompanyDV.COL_CODE
                'Me.grdView.Columns(Me.USE_ACCOUNTING_COL).SortExpression = AcctCompany.AcctCompanyDV.COL_USE_ACCOUNTING
                'Me.grdView.Columns(Me.RPT_COMMISSION_COL).SortExpression = AcctCompany.AcctCompanyDV.COL_RPT_COMMISSION_BREAKDOWN
                'Me.grdView.Columns(Me.USE_ELITA_BANK_INFO_COL).SortExpression = AcctCompany.AcctCompanyDV.COL_USE_ELITA_BANK_INFO
                'Me.grdView.Columns(Me.COV_ENTITY_BY_REGION_COL).SortExpression = AcctCompany.AcctCompanyDV.COL_COV_ENTITY_BY_REGION
                'Me.grdView.Columns(Me.ACCT_SYSTEM_ID_COL).SortExpression = AcctCompany.AcctCompanyDV.COL_ACCT_SYSTEM_ID
                SortAndBindGrid()

            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Protected Sub CheckIfComingFromDeleteConfirm()
            Dim confResponse As String = HiddenDeletePromptResponse.Value
            If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
                If Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete Then
                    DoDelete()
                End If
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Delete
                End Select
            ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Delete
                End Select
            End If
            'Clean after consuming the action
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            HiddenDeletePromptResponse.Value = ""
        End Sub

        Private Sub DoDelete()
            State.MyBO = New AcctCovEntity(State.Id)

            Try
                State.MyBO.Delete()
                State.MyBO.Save()
            Catch ex As Exception
                State.MyBO.RejectChanges()
                Throw ex
            End Try

            State.PageIndex = grdView.PageIndex

            'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
            State.IsAfterSave = True
            State.searchDV = Nothing
            PopulateGrid()
            State.PageIndex = grdView.PageIndex
        End Sub

        Private Function GetDV() As DataView

            Dim dv As DataView

            State.searchDV = GetGridDataView()
            State.searchDV.Sort = grdView.DataMember()

            Return (State.searchDV)

        End Function

        Private Function GetGridDataView() As DataView

            With State
                Return (AcctCovEntity.LoadList(GetSelectedItem(SearchAcctCompanyDropdownList), Guid.Empty, Guid.Empty, Guid.Empty))
            End With

        End Function

        Private Sub SetStateProperties()

            'TODO -- set filters
            ' Me.State.DescriptionMask = SearchDescriptionTextBox.Text

        End Sub

        Private Sub AddNew()

            Dim dv As DataView

            State.searchDV = GetGridDataView()

            State.MyBO = New AcctCovEntity
            State.Id = State.MyBO.Id

            State.searchDV = State.MyBO.GetNewDataViewRow(State.searchDV, State.Id, State.MyBO)
            grdView.DataSource = State.searchDV

            SetPageAndSelectedIndexFromGuid(State.searchDV, State.Id, grdView, State.PageIndex, State.IsEditMode)

            grdView.DataBind()

            State.PageIndex = grdView.PageIndex

            SetGridControls(grdView, False)

            'Set focus on the Business Entity Dropdown for the EditItemIndex row
            SetFocusOnEditableFieldInGrid(grdView, COL_BUSINESS_ENTITY, BUSINESS_ENTITY_CONTROL_NAME, grdView.EditIndex)

            AssignSelectedRecordFromBO()

            'Me.TranslateGridControls(Grid)
            SetButtonsState()
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, grdView)
        End Sub

        Private Sub SortAndBindGrid()

            State.PageIndex = grdView.PageIndex
            If (State.searchDV.Count = 0) Then

                State.bnoRow = True
                CreateHeaderForEmptyGrid(grdView, SortDirection)
            Else
                State.bnoRow = False
                grdView.Enabled = True
                grdView.DataSource = State.searchDV
                HighLightSortColumn(grdView, SortDirection)
                grdView.DataBind()
            End If

            If Not grdView.BottomPagerRow.Visible Then grdView.BottomPagerRow.Visible = True

            ControlMgr.SetVisibleControl(Me, grdView, State.IsGridVisible)

            ControlMgr.SetVisibleControl(Me, trPageSize, grdView.Visible)

            Session("recCount") = State.searchDV.Count

            If grdView.Visible Then
                If (State.AddingNewRow) Then
                    lblRecordCount.Text = (State.searchDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                Else
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, grdView)
        End Sub

        Private Sub AssignBOFromSelectedRecord()
            Try
                With State.MyBO
                    .AcctBusinessUnitId = GetSelectedItem(CType(grdView.Rows(grdView.EditIndex).Cells(COL_BUSINESS_UNIT).FindControl(BUSINESS_UNIT_CONTROL_NAME), DropDownList))
                    .BusinessEntityId = GetSelectedItem(CType(grdView.Rows(grdView.EditIndex).Cells(COL_BUSINESS_ENTITY).FindControl(BUSINESS_ENTITY_CONTROL_NAME), DropDownList))
                    .CoverageTypeId = GetSelectedItem(CType(grdView.Rows(grdView.EditIndex).Cells(COL_COVERAGETYPE).FindControl(COVERAGE_TYPE_CONTROL_NAME), DropDownList))
                    .RegionId = GetSelectedItem(CType(grdView.Rows(grdView.EditIndex).Cells(COL_REGION).FindControl(REGION_CONTROL_NAME), DropDownList))
                    .AcctCoverageTypeCode = CType(grdView.Rows(grdView.EditIndex).Cells(COL_COVERAGETYPECODE).FindControl(ACCT_COVERAGE_TYPE_CONTROL_NAME), TextBox).Text
                End With
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Private Sub AssignSelectedRecordFromBO()

            Dim gridRowIdx As Integer = grdView.EditIndex
            Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            'TODO

            Try
                With State.MyBO

                    CType(grdView.Rows(gridRowIdx).Cells(COL_ID).FindControl(ID_CONTROL_NAME), Label).Text = .Id.ToString

                    'BindListControlToDataView(CType(Me.grdView.Rows(gridRowIdx).Cells(Me.COL_BUSINESS_UNIT).FindControl(Me.BUSINESS_UNIT_CONTROL_NAME), DropDownList), AcctBusinessUnit.getList(GetSelectedItem(Me.SearchAcctCompanyDropdownList), Nothing), AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_BUSINESS_UNIT, AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_ACCT_BUSINESS_UNIT_ID, False)
                    Dim listcontext1 As ListContext = New ListContext()
                    listcontext1.AccountingCompanyId = GetSelectedItem(SearchAcctCompanyDropdownList)
                    Dim bussLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.BusinessUnitByAcctCompany, ElitaPlusIdentity.Current.ActiveUser.LanguageCode, listcontext1)
                    CType(grdView.Rows(gridRowIdx).Cells(COL_BUSINESS_UNIT).FindControl(BUSINESS_UNIT_CONTROL_NAME), DropDownList).Populate(bussLkl, New PopulateOptions() With
                    {
                    .TextFunc = AddressOf .GetCode
                    })

                    If Not .AcctBusinessUnitId.Equals(Guid.Empty) Then
                        SetSelectedItem(CType(grdView.Rows(gridRowIdx).Cells(COL_BUSINESS_UNIT).FindControl(BUSINESS_UNIT_CONTROL_NAME), DropDownList), .AcctBusinessUnitId)
                    End If

                    'BusinessObjectsNew.LookupListNew.DropdownLookupList(BusinessObjectsNew.LookupListNew.LK_BUSINESS_ENTITY, ElitaPlusIdentity.Current.ActiveUser.LanguageId)

                    ' BindListControlToDataView(CType(Me.grdView.Rows(gridRowIdx).Cells(Me.COL_BUSINESS_ENTITY).FindControl(Me.BUSINESS_ENTITY_CONTROL_NAME), DropDownList), LookupListNew.DropdownLookupList(LookupListNew.LK_BUSINESS_ENTITY, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True), , , False) '"BENT"
                    CType(grdView.Rows(gridRowIdx).Cells(COL_BUSINESS_ENTITY).FindControl(BUSINESS_ENTITY_CONTROL_NAME), DropDownList).Populate(CommonConfigManager.Current.ListManager.GetList("BENT", ElitaPlusIdentity.Current.ActiveUser.LanguageCode), New PopulateOptions())
                    If Not .BusinessEntityId.Equals(Guid.Empty) Then
                        SetSelectedItem(CType(grdView.Rows(gridRowIdx).Cells(COL_BUSINESS_ENTITY).FindControl(BUSINESS_ENTITY_CONTROL_NAME), DropDownList), .BusinessEntityId)
                    End If

                    'BindListControlToDataView(CType(Me.grdView.Rows(gridRowIdx).Cells(Me.COL_COVERAGETYPE).FindControl(Me.COVERAGE_TYPE_CONTROL_NAME), DropDownList), LookupListNew.GetCoverageTypeByCompanyGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id), , , False) 'CoverageTypeByCompanyGroup
                    Dim listcontext As ListContext = New ListContext()
                    listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                    CType(grdView.Rows(gridRowIdx).Cells(COL_COVERAGETYPE).FindControl(COVERAGE_TYPE_CONTROL_NAME), DropDownList).Populate(CommonConfigManager.Current.ListManager.GetList("CoverageTypeByCompanyGroup", ElitaPlusIdentity.Current.ActiveUser.LanguageCode, listcontext), New PopulateOptions())
                    If Not .CoverageTypeId.Equals(Guid.Empty) Then
                        SetSelectedItem(CType(grdView.Rows(gridRowIdx).Cells(COL_COVERAGETYPE).FindControl(COVERAGE_TYPE_CONTROL_NAME), DropDownList), .CoverageTypeId)
                    End If

                    '  BindListControlToDataView(CType(Me.grdView.Rows(gridRowIdx).Cells(Me.COL_REGION).FindControl(Me.REGION_CONTROL_NAME), DropDownList), GetRegions, , , True)
                    Dim RegionLkl As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = GetRegion()

                    CType(grdView.Rows(gridRowIdx).Cells(COL_REGION).FindControl(REGION_CONTROL_NAME), DropDownList).Populate(RegionLkl, New PopulateOptions() With
                                                       {
                                                        .AddBlankItem = True
                                                       })


                    If Not .RegionId.Equals(Guid.Empty) Then
                        SetSelectedItem(CType(grdView.Rows(gridRowIdx).Cells(COL_REGION).FindControl(REGION_CONTROL_NAME), DropDownList), .RegionId)
                    End If
                    If .AcctCoverageTypeCode IsNot Nothing Then
                        CType(grdView.Rows(gridRowIdx).Cells(COL_COVERAGETYPECODE).FindControl(ACCT_COVERAGE_TYPE_CONTROL_NAME), TextBox).Text = .AcctCoverageTypeCode.ToString
                    End If
                End With
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub
        Private Function GetRegion() As Assurant.Elita.CommonConfiguration.DataElements.ListItem()
            Dim Index As Integer
            Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext

            Dim UserCompanies As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Dim userCountries As ArrayList = Country.GetCountries(UserCompanies)
            Dim oRegionLkl As New Collections.Generic.List(Of Assurant.Elita.CommonConfiguration.DataElements.ListItem)

            For Each country_id As Guid In userCountries
                'UserCompanyList &= ",'" & GuidControl.GuidToHexString(UserCompanyies(Index))
                oListContext.CountryId = country_id
                Dim oRegionList As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="RegionsByCountry", context:=oListContext)
                If oRegionList.Count > 0 Then
                    If oRegionLkl IsNot Nothing Then
                        oRegionLkl.AddRange(oRegionList)
                    Else

                        oRegionLkl =oRegionList.Clone()
                    End If
                End If
            Next

            Return oRegionLkl.ToArray()

        End Function

        Private Sub ReturnFromEditing()

            grdView.EditIndex = NO_ROW_SELECTED_INDEX

            If grdView.PageCount = 0 Then
                'if returning to the "1st time in" screen
                ControlMgr.SetVisibleControl(Me, grdView, False)
            Else
                ControlMgr.SetVisibleControl(Me, grdView, True)
            End If

            State.IsEditMode = False
            PopulateGrid()
            State.PageIndex = grdView.PageIndex
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
                'Linkbutton_panel.Enabled = False
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
                'Linkbutton_panel.Enabled = True
            End If

        End Sub

        Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                grdView.PageIndex = NewCurrentPageIndex(grdView, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Function GetRegions() As DataView

            Try

                Dim dv As DataView
                If State.dvRegion Is Nothing Then
                    State.dvRegion = LookupListNew.GetRegionLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
                End If
                dv = State.dvRegion

                Return dv
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try


        End Function


#End Region

#Region " Datagrid Related "

        Public Property SortDirection() As String
            Get
                Return ViewState("SortDirection").ToString
            End Get
            Set(value As String)
                ViewState("SortDirection") = value
            End Set
        End Property

        Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdView.PageIndexChanging

            Try
                If (Not (State.IsEditMode)) Then
                    State.PageIndex = e.NewPageIndex
                    grdView.PageIndex = State.PageIndex
                    PopulateGrid()
                    grdView.SelectedIndex = NO_ITEM_SELECTED_INDEX
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Protected Sub ItemCommand(source As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)

            Try
                Dim index As Integer

                If (e.CommandName = EDIT_COMMAND) Then
                    index = CInt(e.CommandArgument)
                    State.IsEditMode = True
                    State.Id = New Guid(CType(grdView.Rows(index).Cells(COL_ID).FindControl(ID_CONTROL_NAME), Label).Text)
                    State.MyBO = New AcctCovEntity(State.Id)

                    PopulateGrid()

                    State.PageIndex = grdView.PageIndex

                    'Disable all Edit and Delete icon buttons on the Grid
                    SetGridControls(grdView, False)

                    'TODO - comment out?
                    'Set focus on the COverage Type dropdown for the EditItemIndex row
                    SetFocusOnEditableFieldInGrid(grdView, COL_BUSINESS_ENTITY, BUSINESS_ENTITY_CONTROL_NAME, index)

                    AssignSelectedRecordFromBO()

                    SetButtonsState()

                ElseIf (e.CommandName = DELETE_COMMAND) Then
                    index = CInt(e.CommandArgument)

                    'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                    grdView.SelectedIndex = NO_ROW_SELECTED_INDEX

                    State.Id = New Guid(CType(grdView.Rows(index).Cells(COL_ID).FindControl(ID_CONTROL_NAME), Label).Text)
                    DisplayMessage(Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenDeletePromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
                End If

            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        'The Binding Logic is here
        Private Sub ItemDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdView.RowDataBound
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

            If dvRow IsNot Nothing AndAlso Not State.bnoRow Then

                If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then

                    CType(e.Row.Cells(COL_ID).FindControl(ID_CONTROL_NAME), Label).Text = GetGuidStringFromByteArray(CType(dvRow(DALObjects.AcctCovEntityDAL.COL_NAME_ACCT_COV_ENTITY_ID), Byte()))

                    If (State.IsEditMode = True _
                            AndAlso State.Id.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow(DALObjects.AcctCovEntityDAL.COL_NAME_ACCT_COV_ENTITY_ID), Byte())))) Then

                        'NADA

                    Else

                        Dim dRow() As DataRow

                        If (e.Row.Cells(COL_BUSINESS_ENTITY).FindControl(BUSINESS_ENTITY_LABEL_CONTROL_NAME) IsNot Nothing) AndAlso (dvRow IsNot Nothing) Then
                            CType(e.Row.Cells(COL_BUSINESS_ENTITY).FindControl(BUSINESS_ENTITY_LABEL_CONTROL_NAME), Label).Text = LookupListNew.GetDescriptionFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_BUSINESS_ENTITY, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True), GuidControl.ByteArrayToGuid(dvRow(DALObjects.AcctCovEntityDAL.COL_NAME_BUSINESS_ENTITY_ID)))
                        End If

                        If (e.Row.Cells(COL_BUSINESS_UNIT).FindControl(BUSINESS_UNIT_LABEL_CONTROL_NAME) IsNot Nothing) AndAlso (dvRow IsNot Nothing) Then
                            CType(e.Row.Cells(COL_BUSINESS_UNIT).FindControl(BUSINESS_UNIT_LABEL_CONTROL_NAME), Label).Text = dvRow(AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_BUSINESS_UNIT).ToString
                        End If

                        If (e.Row.Cells(COL_COVERAGETYPE).FindControl(COVERAGE_TYPE_LABEL_CONTROL_NAME) IsNot Nothing) AndAlso (dvRow IsNot Nothing) Then
                            CType(e.Row.Cells(COL_COVERAGETYPE).FindControl(COVERAGE_TYPE_LABEL_CONTROL_NAME), Label).Text = LookupListNew.GetDescriptionFromId(LookupListNew.GetCoverageTypeByCompanyGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id), GuidControl.ByteArrayToGuid(dvRow(DALObjects.AcctCovEntityDAL.COL_NAME_COVERAGE_TYPE_ID)))
                        End If

                        If (e.Row.Cells(COL_REGION).FindControl(REGION_LABEL_CONTROL_NAME) IsNot Nothing) AndAlso (dvRow IsNot Nothing) AndAlso Not dvRow(DALObjects.AcctCovEntityDAL.COL_NAME_REGION_ID).Equals(DBNull.Value) Then
                            CType(e.Row.Cells(COL_REGION).FindControl(REGION_LABEL_CONTROL_NAME), Label).Text = LookupListNew.GetDescriptionFromId(GetRegions, GuidControl.ByteArrayToGuid(dvRow(DALObjects.AcctCovEntityDAL.COL_NAME_REGION_ID)))
                        End If

                        If (e.Row.Cells(COL_COVERAGETYPECODE).FindControl(ACCT_COVERAGE_TYPE_LABEL_CONTROL_NAME) IsNot Nothing) AndAlso (dvRow IsNot Nothing) AndAlso Not dvRow(DALObjects.AcctCovEntityDAL.COL_NAME_ACCT_COVERAGE_TYPE_CODE).Equals(DBNull.Value) Then
                            CType(e.Row.Cells(COL_COVERAGETYPECODE).FindControl(ACCT_COVERAGE_TYPE_LABEL_CONTROL_NAME), Label).Text = dvRow(DALObjects.AcctCovEntityDAL.COL_NAME_ACCT_COVERAGE_TYPE_CODE).ToString
                        End If

                    End If
                End If
            End If
        End Sub

        Protected Sub ItemBound(source As Object, e As GridViewRowEventArgs) Handles grdView.RowDataBound

            Try
                If Not State.bnoRow Then
                    BaseItemBound(source, e)
                End If

            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Protected Sub ItemCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles grdView.Sorting
            Try
                Dim spaceIndex As Integer = SortDirection.LastIndexOf(" ")

                If spaceIndex > 0 AndAlso SortDirection.Substring(0, spaceIndex).Equals(e.SortExpression) Then
                    If SortDirection.EndsWith(" ASC") Then
                        SortDirection = e.SortExpression + " DESC"
                    Else
                        SortDirection = e.SortExpression + " ASC"
                    End If
                Else
                    SortDirection = e.SortExpression + " ASC"
                End If

                State.PageIndex = 0
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Protected Sub BindBoPropertiesToGridHeaders()
            BindBOPropertyToGridHeader(State.MyBO, "BusinessEntityId", grdView.Columns(COL_BUSINESS_ENTITY))
            BindBOPropertyToGridHeader(State.MyBO, "AcctBusinessUnitId", grdView.Columns(COL_BUSINESS_UNIT))
            BindBOPropertyToGridHeader(State.MyBO, "CoverageTypeId", grdView.Columns(COL_COVERAGETYPE))
            BindBOPropertyToGridHeader(State.MyBO, "RegionId", grdView.Columns(COL_REGION))
            BindBOPropertyToGridHeader(State.MyBO, "AcctCoverageTypeCode", grdView.Columns(COL_COVERAGETYPECODE))
            ClearGridViewHeadersAndLabelsErrSign()
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(grid As GridView, cellPosition As Integer, controlName As String, itemIndex As Integer)
            'Set focus on the Coverage Type dropdown for the EditItemIndex row
            If itemIndex >= 0 Then
                Dim ctl As DropDownList = CType(grid.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), DropDownList)
                SetFocus(ctl)
            End If
        End Sub

#End Region
    End Class

End Namespace
