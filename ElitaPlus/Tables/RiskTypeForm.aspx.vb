Imports Microsoft.VisualBasic
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports System.Threading
Namespace Tables

    Partial Class RiskTypeForm
        Inherits ElitaPlusSearchPage

#Region "Constants"

        'Private Const EDIT_COL As Integer = 0
        'Private Const DELETE_COL As Integer = 1
        Private Const RISK_TYPE_ID_COL As Integer = 2
        Private Const DESCRIPTION_COL As Integer = 3
        Private Const RISK_TYPE_ENGLISH_COL As Integer = 4
        Private Const PRODUCT_TAX_TYPE_ID_COL As Integer = 5
        Private Const RISK_GROUP_COL As Integer = 6
        Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
        Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

        Private Const RISK_TYPE_ID_CONTROL_NAME As String = "RiskTypeIdLabel"

        Private Const DESCRIPTION_CONTROL_LABEL As String = "Label1"
        Private Const DESCRIPTION_CONTROL_NAME As String = "DescriptionTextBox"
        Private Const RISK_TYPE_ENGLISH_LABEL As String = "RiskTypeEngLabel"
        Private Const RISK_TYPE_ENGLISH_CONTROL_NAME As String = "RiskTypeEngTextBox"
        Private Const PRODUCT_TAX_TYPE_ID_DD_LABEL As String = "moProductTaxTypeLabel"
        Private Const PRODUCT_TAX_TYPE_ID_CONTROL_NAME As String = "ProductTaxTypeDropdown"
        Private Const RISK_GROUP_DD_LABEL As String = "Label2"
        Private Const RISK_GROUP_CONTROL_NAME As String = "RiskGroupDropdown"


        'Private Const EDIT_CONTROL_NAME As String = "EditButton"
        'Private Const DELETE_CONTROL_NAME As String = "DeleteButton"

        Private Const EDIT_COMMAND As String = "EditRecord"
        Private Const DELETE_COMMAND As String = "DeleteRecord"
        Private Const SORT_COMMAND As String = "Sort"

        'Private Const EDIT As String = "Edit"
        'Private Const DELETE As String = "Delete"

        Private Const NO_ROW_SELECTED_INDEX As Integer = -1

#End Region

#Region "Member Variables"

#End Region

#Region "Properties"

        Public ReadOnly Property IsEditing() As Boolean
            Get
                IsEditing = (RiskGrid.EditIndex > NO_ROW_SELECTED_INDEX)
            End Get
        End Property

#End Region

#Region "Page State"
        Class MyState
            Public PageIndex As Integer = 0
            Public RiskTypeBO As RiskType
            Public RiskTypeId As Guid
            Public LangId As Guid
            Public DescriptionMask As String
            Public RiskTypeEnglishMask As String
            Public RiskGroupIdSearch As Guid
            Public CompanyId As Guid
            Public CompanyGroupId As Guid
            Public IsAfterSave As Boolean
            Public IsEditMode As Boolean
            Public IsGridVisible As Boolean
            Public searchDV As DataView = Nothing
            'Public SortExpression As String = RiskType.DESCRIPTION_COL
            Public AddingNewRow As Boolean
            Public Canceling As Boolean
            Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public bnoRow As Boolean = False
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


#Region "Handlers"

#Region " Web Form Designer Generated Code "

        Protected WithEvents riskTypeErrorController As ErrorController
        Protected WithEvents TitleLabel As System.Web.UI.WebControls.Label
        Protected WithEvents RiskTypePanel As System.Web.UI.WebControls.Panel
        Protected WithEvents RiskTypeLabel As System.Web.UI.WebControls.Label
        Protected WithEvents RiskGroupDropdownListLabel As System.Web.UI.WebControls.Label

        ''NOTE: The following placeholder declaration is required by the Web Form Designer.
        ''Do not delete or move it.
        'Private designerPlaceholderDeclaration As System.Object

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Handler-Init"

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

            'Put user code to initialize the page here
            Try
                riskTypeErrorController.Clear_Hide()
                SetStateProperties()
                If Not Page.IsPostBack Then
                    SortDirection = RiskType.DESCRIPTION_COL
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    SetDefaultButton(RiskTypeTextBox, SearchButton)
                    SetDefaultButton(RiskTypeEnglishTextBox, SearchButton)
                    SetGridItemStyleColor(RiskGrid)
                    If State.RiskTypeBO Is Nothing Then
                        State.RiskTypeBO = New RiskType
                    End If
                    PopulateRiskGroupDropdown(RiskGroupList, Not (IsEditing))
                    RiskGroupList.SelectedIndex = 0
                    SetButtonsState()
                    State.PageIndex = 0
                    TranslateGridHeader(RiskGrid)
                    TranslateGridControls(RiskGrid)
                Else
                    CheckIfComingFromDeleteConfirm()
                End If
                BindBoPropertiesToGridHeaders()
            Catch ex As Exception
                HandleErrors(ex, riskTypeErrorController)
            End Try
            ShowMissingTranslations(riskTypeErrorController)
        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub SearchButton_Click(sender As System.Object, e As System.EventArgs) Handles SearchButton.Click

            Try
                State.IsGridVisible = True
                State.searchDV = Nothing
                PopulateRiskGrid()
                State.PageIndex = RiskGrid.PageIndex
            Catch ex As Exception
                HandleErrors(ex, riskTypeErrorController)
            End Try

        End Sub

        Private Sub ClearButton_Click(sender As System.Object, e As System.EventArgs) Handles ClearButton.Click

            ClearSearchCriteria()

        End Sub

        Private Sub ClearSearchCriteria()

            Try
                RiskTypeTextBox.Text = String.Empty
                RiskTypeEnglishTextBox.Text = String.Empty
                'Set the 1st Item in the RiskGroupDrowpdown List to ""
                RiskGroupList.SelectedIndex = 0

                'Update Page State
                With State
                    .DescriptionMask = RiskTypeTextBox.Text
                    .RiskTypeEnglishMask = RiskTypeEnglishTextBox.Text
                    .RiskGroupIdSearch = Nothing
                End With
            Catch ex As Exception
                HandleErrors(ex, riskTypeErrorController)
            End Try

        End Sub

        Private Sub NewButton_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles NewButton_WRITE.Click

            Try
                State.IsEditMode = True
                State.IsGridVisible = True
                State.AddingNewRow = True
                AddNewRiskType()
                SetButtonsState()
            Catch ex As Exception
                HandleErrors(ex, riskTypeErrorController)
            End Try

        End Sub

        Private Sub SaveButton_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles SaveButton_WRITE.Click

            Try
                PopulateBOFromForm()
                If (State.RiskTypeBO.IsDirty) Then
                    State.RiskTypeBO.Save()
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
                HandleErrors(ex, riskTypeErrorController)
            End Try

        End Sub

        Private Sub CancelButton_Click(sender As System.Object, e As System.EventArgs) Handles CancelButton.Click

            Try
                RiskGrid.SelectedIndex = NO_ITEM_SELECTED_INDEX
                State.Canceling = True
                If (State.AddingNewRow) Then
                    State.AddingNewRow = False
                    State.searchDV = Nothing
                End If
                ReturnFromEditing()
            Catch ex As Exception
                HandleErrors(ex, riskTypeErrorController)
            End Try

        End Sub

#End Region

#Region "Handlers-Grid"

        Public Property SortDirection() As String
            Get
                Return ViewState("SortDirection").ToString
            End Get
            Set(value As String)
                ViewState("SortDirection") = value
            End Set
        End Property


        Private Sub SortAndBindGrid()
            TranslateGridControls(RiskGrid)

            'RiskGrid.DataSource = Me.State.searchDV
            'HighLightSortColumn(RiskGrid, Me.State.SortExpression)
            'Me.RiskGrid.DataBind()
            If (State.searchDV.Count = 0) Then

                State.bnoRow = True
                CreateHeaderForEmptyGrid(RiskGrid, SortDirection)
            Else
                State.bnoRow = False
                RiskGrid.Enabled = True
                RiskGrid.DataSource = State.searchDV
                HighLightSortColumn(RiskGrid, SortDirection)
                RiskGrid.DataBind()
                If RiskType.Is_TaxByProductType_Yes = True Then
                    RiskGrid.Columns(PRODUCT_TAX_TYPE_ID_COL).Visible = True
                Else
                    RiskGrid.Columns(PRODUCT_TAX_TYPE_ID_COL).Visible = False
                End If
            End If
            If Not RiskGrid.BottomPagerRow.Visible Then RiskGrid.BottomPagerRow.Visible = True


            ControlMgr.SetVisibleControl(Me, RiskGrid, State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, trPageSize, RiskGrid.Visible)

            Session("recCount") = State.searchDV.Count

            If RiskGrid.Visible Then
                If (State.AddingNewRow) Then
                    lblRecordCount.Text = (State.searchDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                Else
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, RiskGrid)
        End Sub

        Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                RiskGrid.PageIndex = NewCurrentPageIndex(RiskGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                PopulateRiskGrid()
            Catch ex As Exception
                HandleErrors(ex, riskTypeErrorController)
            End Try
        End Sub

        Private Sub RiskGrid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles RiskGrid.PageIndexChanging

            Try
                If (Not (State.IsEditMode)) Then
                    State.PageIndex = e.NewPageIndex
                    RiskGrid.PageIndex = State.PageIndex
                    PopulateRiskGrid()
                    RiskGrid.SelectedIndex = NO_ITEM_SELECTED_INDEX
                End If
            Catch ex As Exception
                HandleErrors(ex, riskTypeErrorController)
            End Try

        End Sub

        Protected Sub ItemCommand(source As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)

            Try
                Dim index As Integer

                If (e.CommandName = EDIT_COMMAND) Then
                    index = CInt(e.CommandArgument)
                    'Do the Edit here

                    'Set the IsEditMode flag to TRUE
                    State.IsEditMode = True

                    State.RiskTypeId = New Guid(CType(RiskGrid.Rows(index).Cells(RISK_TYPE_ID_COL).FindControl(RISK_TYPE_ID_CONTROL_NAME), Label).Text)
                    State.RiskTypeBO = New RiskType(State.RiskTypeId)

                    PopulateRiskGrid()

                    State.PageIndex = RiskGrid.PageIndex

                    SetGridControls(RiskGrid, False)

                    'Set focus on the Description TextBox for the EditItemIndex row
                    SetFocusOnEditableFieldInGrid(RiskGrid, DESCRIPTION_COL, DESCRIPTION_CONTROL_NAME, index)

                    Dim productTaxTypeList As DropDownList =
                        CType(RiskGrid.Rows(index).Cells(PRODUCT_TAX_TYPE_ID_COL).FindControl(
                                PRODUCT_TAX_TYPE_ID_CONTROL_NAME), DropDownList)
                    PopulateProductTaxTypeDropdown(productTaxTypeList, Not (IsEditing))
                    SetSelectedItem(productTaxTypeList, State.RiskTypeBO.ProductTaxTypeId)

                    Dim riskGroupList As DropDownList = CType(RiskGrid.Rows(index).Cells(RISK_GROUP_COL).FindControl(RISK_GROUP_CONTROL_NAME), DropDownList)
                    'Invoke the RiskGroupFactoryLookup with NotingSelected = True
                    PopulateRiskGroupDropdown(riskGroupList, Not (IsEditing))
                    SetSelectedItem(riskGroupList, State.RiskTypeBO.RiskGroupId)

                    PopulateFormFromBO()

                    SetButtonsState()

                ElseIf (e.CommandName = DELETE_COMMAND) Then
                    index = CInt(e.CommandArgument)
                    State.RiskTypeId = New Guid(CType(RiskGrid.Rows(index).Cells(RISK_TYPE_ID_COL).FindControl(RISK_TYPE_ID_CONTROL_NAME), Label).Text)
                    DisplayMessage(Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenDeletePromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete

                End If
            Catch ex As Exception
                HandleErrors(ex, riskTypeErrorController)
            End Try

        End Sub

        Private Sub DoDelete()
            'Do the delete here

            'Save the RiskTypeId in the Session

            Dim riskTypeBO As RiskType = New RiskType(State.RiskTypeId)

            riskTypeBO.Delete()

            'Call the Save() method in the RiskType Business Object here

            riskTypeBO.Save()

            State.PageIndex = RiskGrid.PageIndex

            'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
            State.IsAfterSave = True
            State.searchDV = Nothing
            PopulateRiskGrid()
            State.PageIndex = RiskGrid.PageIndex
        End Sub
        Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles RiskGrid.RowDataBound
            Try
                'BaseItemBound(source, e)
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

                If dvRow IsNot Nothing And Not State.bnoRow Then

                    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                        CType(e.Row.Cells(RISK_TYPE_ID_COL).FindControl(RISK_TYPE_ID_CONTROL_NAME), Label).Text = GetGuidStringFromByteArray(CType(dvRow(RiskType.RISK_TYPE_ID_COL), Byte()))

                        If (State.IsEditMode = True _
                                AndAlso State.RiskTypeId.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow(RiskType.RISK_TYPE_ID_COL), Byte())))) Then
                            CType(e.Row.Cells(DESCRIPTION_COL).FindControl(DESCRIPTION_CONTROL_NAME), TextBox).Text = dvRow(RiskType.DESCRIPTION_COL).ToString
                            CType(e.Row.Cells(RISK_TYPE_ENGLISH_COL).FindControl(RISK_TYPE_ENGLISH_CONTROL_NAME), TextBox).Text = dvRow(RiskType.RISK_TYPE_ENGLISH_COL).ToString
                            CType(e.Row.Cells(PRODUCT_TAX_TYPE_ID_COL).FindControl(
                                PRODUCT_TAX_TYPE_ID_CONTROL_NAME), DropDownList).Text =
                                                dvRow(RiskType.PRODUCT_TAX_TYPE).ToString
                            CType(e.Row.Cells(RISK_GROUP_COL).FindControl(RISK_GROUP_CONTROL_NAME), DropDownList).Text = dvRow(RiskType.RISK_GROUP).ToString
                        Else
                            CType(e.Row.Cells(DESCRIPTION_COL).FindControl(DESCRIPTION_CONTROL_LABEL), Label).Text = dvRow(RiskType.DESCRIPTION_COL).ToString
                            CType(e.Row.Cells(RISK_TYPE_ENGLISH_COL).FindControl(RISK_TYPE_ENGLISH_LABEL), Label).Text = dvRow(RiskType.RISK_TYPE_ENGLISH_COL).ToString
                            CType(e.Row.Cells(PRODUCT_TAX_TYPE_ID_COL).FindControl(
                                PRODUCT_TAX_TYPE_ID_DD_LABEL), Label).Text =
                                    dvRow(RiskType.PRODUCT_TAX_TYPE).ToString
                            CType(e.Row.Cells(RISK_GROUP_COL).FindControl(RISK_GROUP_DD_LABEL), Label).Text = dvRow(RiskType.RISK_GROUP).ToString
                        End If
                        'e.Row.Cells(Me.ACCT_COMPANY_DESCRIPTION_COL).Text = dvRow(AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_ACCT_COMPANY_DESCRIPTION).ToString
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, riskTypeErrorController)
            End Try
        End Sub
        Protected Sub ItemBound(source As Object, e As GridViewRowEventArgs) Handles RiskGrid.RowDataBound

            Try
                If Not State.bnoRow Then
                    BaseItemBound(source, e)
                End If

            Catch ex As Exception
                HandleErrors(ex, riskTypeErrorController)
            End Try
        End Sub

        Protected Sub ItemCreated(sender As Object, e As GridViewRowEventArgs)
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                HandleErrors(ex, riskTypeErrorController)
            End Try
        End Sub

        'Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles RiskGrid.Sorting

        '    Try
        '        If Me.State.SortExpression.StartsWith(e.SortExpression) Then
        '            If Me.State.SortExpression.EndsWith(" DESC") Then
        '                Me.State.SortExpression = e.SortExpression
        '            Else
        '                Me.State.SortExpression &= " DESC"
        '            End If
        '        Else
        '            Me.State.SortExpression = e.SortExpression
        '        End If
        '        'To handle the requirement of always going to the FIRST page on the Grid whenever the user switches the sorting criterion
        '        'Set the Me.State.selectedClaimId = Guid.Empty and set Me.State.PageIndex = 0
        '        Me.State.RiskTypeId = Guid.Empty
        '        Me.State.PageIndex = 0

        '        Me.PopulateRiskGrid()
        '    Catch ex As Exception
        '        Me.HandleErrors(ex, Me.riskTypeErrorController)
        '    End Try

        'End Sub

        Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles RiskGrid.Sorting
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
                PopulateRiskGrid()
            Catch ex As Exception
                HandleErrors(ex, riskTypeErrorController)
            End Try
        End Sub

#End Region

#End Region


#Region "Populate"

        Private Sub PopulateRiskGrid()

            Dim dv As DataView
            Try
                'Refresh the DataView and Call SetPageAndSelectedIndexFromGuid() to go to the Page 
                'where the most recently saved Record exists in the DataView
                If (State.searchDV Is Nothing) Then
                    State.searchDV = GetDV()
                End If
                State.searchDV.Sort = SortDirection
                If (State.IsAfterSave) Then
                    State.IsAfterSave = False
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.RiskTypeId, RiskGrid, State.PageIndex)
                ElseIf (State.IsEditMode) Then
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.RiskTypeId, RiskGrid, State.PageIndex, State.IsEditMode)
                Else
                    SetPageAndSelectedIndexFromGuid(State.searchDV, Guid.Empty, RiskGrid, State.PageIndex)
                End If

                RiskGrid.AutoGenerateColumns = False
                RiskGrid.Columns(DESCRIPTION_COL).SortExpression = RiskType.DESCRIPTION_COL
                RiskGrid.Columns(RISK_TYPE_ENGLISH_COL).SortExpression = RiskType.RISK_TYPE_ENGLISH_COL
                RiskGrid.Columns(PRODUCT_TAX_TYPE_ID_COL).SortExpression = RiskType.PRODUCT_TAX_TYPE
                RiskGrid.Columns(RISK_GROUP_COL).SortExpression = RiskType.RISK_GROUP

                SortAndBindGrid()
            Catch ex As Exception
                HandleErrors(ex, riskTypeErrorController)
            End Try

        End Sub

        Private Function GetDV() As DataView

            Dim dv As DataView

            State.searchDV = GetRiskGridDataView()
            State.searchDV.Sort = RiskGrid.DataMember()
            RiskGrid.DataSource = State.searchDV

            Return (State.searchDV)

        End Function

        Private Function GetRiskGridDataView() As DataView

            With State
                Return (RiskType.GetRiskTypeList(.DescriptionMask, .RiskTypeEnglishMask, .RiskGroupIdSearch, .LangId, .CompanyGroupId))
            End With

        End Function



        Private Sub PopulateProductTaxTypeDropdown(productTaxTypeDropDownList As DropDownList, nothingSelected As Boolean)
            Try

                Dim productTaxTypeListLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("PTT", Thread.CurrentPrincipal.GetLanguageCode())
                productTaxTypeDropDownList.Populate(productTaxTypeListLkl, New PopulateOptions() With
                                                              {
                                                                .AddBlankItem = nothingSelected
                                                               })
            Catch ex As Exception
                HandleErrors(ex, riskTypeErrorController)
            End Try

        End Sub

        Private Sub PopulateRiskGroupDropdown(riskGroupDropDownList As DropDownList, nothingSelected As Boolean)
            Try

                Dim riskGroupLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("RGRP", Thread.CurrentPrincipal.GetLanguageCode())
                riskGroupDropDownList.Populate(riskGroupLkl, New PopulateOptions() With
                                                              {
                                                                .AddBlankItem = nothingSelected
                                                               })
            Catch ex As Exception
                HandleErrors(ex, riskTypeErrorController)
            End Try

        End Sub

        Private Sub AddNewRiskType()

            Dim dv As DataView

            State.searchDV = GetRiskGridDataView()

            State.RiskTypeBO = New RiskType
            State.RiskTypeId = State.RiskTypeBO.RiskTypeId
            State.RiskTypeBO.CompanyGroupId = State.CompanyGroupId

            State.searchDV = State.RiskTypeBO.GetNewDataViewRow(State.searchDV, State.RiskTypeId)

            RiskGrid.DataSource = State.searchDV

            SetPageAndSelectedIndexFromGuid(State.searchDV, State.RiskTypeId, RiskGrid,
                                               State.PageIndex, State.IsEditMode)

            RiskGrid.AutoGenerateColumns = False
            RiskGrid.Columns(DESCRIPTION_COL).SortExpression = RiskType.DESCRIPTION_COL
            RiskGrid.Columns(RISK_TYPE_ENGLISH_COL).SortExpression = RiskType.RISK_TYPE_ENGLISH_COL
            RiskGrid.Columns(PRODUCT_TAX_TYPE_ID_COL).SortExpression = RiskType.PRODUCT_TAX_TYPE
            RiskGrid.Columns(RISK_GROUP_COL).SortExpression = RiskType.RISK_GROUP
            HighLightSortColumn(RiskGrid, SortDirection)
            RiskGrid.DataBind()

            If RiskType.Is_TaxByProductType_Yes = True Then
                RiskGrid.Columns(PRODUCT_TAX_TYPE_ID_COL).Visible = True
            Else
                RiskGrid.Columns(PRODUCT_TAX_TYPE_ID_COL).Visible = False
            End If

            State.PageIndex = RiskGrid.PageIndex

            SetGridControls(RiskGrid, False)

            ''Set focus on the Description TextBox for the EditItemIndex row
            SetFocusOnEditableFieldInGrid(RiskGrid, DESCRIPTION_COL, DESCRIPTION_CONTROL_NAME, RiskGrid.EditIndex)
            Dim productTaxTypeList As DropDownList = CType(
                RiskGrid.Rows(RiskGrid.EditIndex).Cells(PRODUCT_TAX_TYPE_ID_COL).FindControl(
                                PRODUCT_TAX_TYPE_ID_CONTROL_NAME), DropDownList)
            PopulateProductTaxTypeDropdown(productTaxTypeList, False)

            Dim riskGroupList As DropDownList = CType(RiskGrid.Rows(RiskGrid.EditIndex).Cells(RISK_GROUP_COL).FindControl(RISK_GROUP_CONTROL_NAME), DropDownList)
            'Invoke the RiskGroupFactoryLookup with NotingSelected = False
            PopulateRiskGroupDropdown(riskGroupList, False)
            PopulateFormFromBO()

            TranslateGridControls(RiskGrid)

            SetButtonsState()
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, RiskGrid)
        End Sub

        Private Sub PopulateBOFromForm()
            Try
                With State.RiskTypeBO
                    .Description = CType(RiskGrid.Rows(RiskGrid.EditIndex).Cells(DESCRIPTION_COL).FindControl(DESCRIPTION_CONTROL_NAME), TextBox).Text
                    .RiskTypeEnglish = CType(RiskGrid.Rows(RiskGrid.EditIndex).Cells(RISK_TYPE_ENGLISH_COL).FindControl(RISK_TYPE_ENGLISH_CONTROL_NAME), TextBox).Text
                    .ProductTaxTypeId = GetSelectedItem(CType(
                            RiskGrid.Rows(RiskGrid.EditIndex).Cells(
                            PRODUCT_TAX_TYPE_ID_COL).FindControl(PRODUCT_TAX_TYPE_ID_CONTROL_NAME), DropDownList))
                    .RiskGroupId = GetSelectedItem(CType(RiskGrid.Rows(RiskGrid.EditIndex).Cells(RISK_GROUP_COL).FindControl(RISK_GROUP_CONTROL_NAME), DropDownList))
                End With
            Catch ex As Exception
                HandleErrors(ex, riskTypeErrorController)
            End Try

        End Sub

        Private Sub PopulateFormFromBO()

            Dim gridRowIdx As Integer = RiskGrid.EditIndex
            Try
                With State.RiskTypeBO
                    If (.Description IsNot Nothing) Then
                        CType(RiskGrid.Rows(gridRowIdx).Cells(DESCRIPTION_COL).FindControl(DESCRIPTION_CONTROL_NAME), TextBox).Text = .Description
                    End If
                    If (.RiskTypeEnglish IsNot Nothing) Then
                        CType(RiskGrid.Rows(gridRowIdx).Cells(RISK_TYPE_ENGLISH_COL).FindControl(RISK_TYPE_ENGLISH_CONTROL_NAME), TextBox).Text = .RiskTypeEnglish
                    End If

                    If (.ProductTaxTypeId.Equals(Guid.Empty)) Then
                        SetSelectedItem(CType(RiskGrid.Rows(gridRowIdx).Cells(
                                RISK_GROUP_COL).FindControl(
                                PRODUCT_TAX_TYPE_ID_CONTROL_NAME), DropDownList),
                            LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(
                            Codes.PRODUCT_TAX_TYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId), Codes.PRODUCT_TAX_TYPE__ALL))
                    Else
                        SetSelectedItem(CType(RiskGrid.Rows(gridRowIdx).Cells(
                                RISK_GROUP_COL).FindControl(
                                PRODUCT_TAX_TYPE_ID_CONTROL_NAME), DropDownList), .ProductTaxTypeId)
                    End If
                    If (Not .RiskGroupId.Equals(Guid.Empty)) Then
                        SetSelectedItem(CType(RiskGrid.Rows(gridRowIdx).Cells(RISK_GROUP_COL).FindControl(RISK_GROUP_CONTROL_NAME), DropDownList), .RiskGroupId)
                    End If
                    CType(RiskGrid.Rows(gridRowIdx).Cells(RISK_TYPE_ID_COL).FindControl(RISK_TYPE_ID_CONTROL_NAME), Label).Text = .RiskTypeId.ToString
                End With
            Catch ex As Exception
                HandleErrors(ex, riskTypeErrorController)
            End Try

        End Sub



#End Region

#Region "Control"

        Private Sub ReturnFromEditing()

            RiskGrid.EditIndex = NO_ROW_SELECTED_INDEX

            If (RiskGrid.PageCount = 0) Then
                'if returning to the "1st time in" screen
                ControlMgr.SetVisibleControl(Me, RiskGrid, False)
            Else
                ControlMgr.SetVisibleControl(Me, RiskGrid, True)
            End If

            State.IsEditMode = False
            PopulateRiskGrid()
            State.PageIndex = RiskGrid.PageIndex
            SetButtonsState()

        End Sub

        Private Sub SetButtonsState()

            If (State.IsEditMode) Then
                ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, True)
                ControlMgr.SetVisibleControl(Me, CancelButton, True)
                ControlMgr.SetVisibleControl(Me, NewButton_WRITE, False)
                ControlMgr.SetEnableControl(Me, SearchButton, False)
                ControlMgr.SetEnableControl(Me, ClearButton, False)
                RiskGroupList.Enabled = False
                MenuEnabled = False
            Else
                ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, False)
                ControlMgr.SetVisibleControl(Me, CancelButton, False)
                ControlMgr.SetVisibleControl(Me, NewButton_WRITE, True)
                ControlMgr.SetEnableControl(Me, SearchButton, True)
                ControlMgr.SetEnableControl(Me, ClearButton, True)
                ControlMgr.SetEnableControl(Me, RiskGroupList, True)
                MenuEnabled = True
            End If

        End Sub

        Protected Sub BindBoPropertiesToGridHeaders()
            BindBOPropertyToGridHeader(State.RiskTypeBO, "Description", RiskGrid.Columns(DESCRIPTION_COL))
            BindBOPropertyToGridHeader(State.RiskTypeBO, "RiskTypeEnglish", RiskGrid.Columns(RISK_TYPE_ENGLISH_COL))
            ClearGridViewHeadersAndLabelsErrSign()
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(grid As GridView, cellPosition As Integer, controlName As String, itemIndex As Integer)
            'Set focus on the Description TextBox for the EditItemIndex row
            Dim desc As TextBox = CType(grid.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
            SetFocus(desc)
        End Sub

        Private Sub HideEditAndDeleteButtons()
            For Each row As GridViewRow In RiskGrid.Rows
                row.Cells(0).Controls.RemoveAt(0)
                If row.RowIndex <> RiskGrid.EditIndex Then
                    row.Cells(1).Controls.RemoveAt(0)
                End If
            Next
        End Sub


#End Region

#Region "State Management"

        Private Sub SetStateProperties()

            State.DescriptionMask = RiskTypeTextBox.Text
            State.RiskTypeEnglishMask = RiskTypeEnglishTextBox.Text
            If (RiskGroupList.SelectedItem IsNot Nothing AndAlso RiskGroupList.SelectedItem.Value <> NOTHING_SELECTED_TEXT) Then
                State.RiskGroupIdSearch = GetGuidFromString(RiskGroupList.SelectedItem.Value)
            Else
                State.RiskGroupIdSearch = Nothing
            End If
            State.LangId = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            State.CompanyId = ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID
            State.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id

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


#End Region

    End Class

End Namespace


