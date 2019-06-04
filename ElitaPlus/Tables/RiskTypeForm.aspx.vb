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
                IsEditing = (Me.RiskGrid.EditIndex > NO_ROW_SELECTED_INDEX)
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

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Handler-Init"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            'Put user code to initialize the page here
            Try
                riskTypeErrorController.Clear_Hide()
                Me.SetStateProperties()
                If Not Page.IsPostBack Then
                    Me.SortDirection = RiskType.DESCRIPTION_COL
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    Me.SetDefaultButton(Me.RiskTypeTextBox, Me.SearchButton)
                    Me.SetDefaultButton(Me.RiskTypeEnglishTextBox, Me.SearchButton)
                    Me.SetGridItemStyleColor(Me.RiskGrid)
                    If Me.State.RiskTypeBO Is Nothing Then
                        Me.State.RiskTypeBO = New RiskType
                    End If
                    Me.PopulateRiskGroupDropdown(Me.RiskGroupList, Not (Me.IsEditing))
                    Me.RiskGroupList.SelectedIndex = 0
                    SetButtonsState()
                    Me.State.PageIndex = 0
                    Me.TranslateGridHeader(RiskGrid)
                    Me.TranslateGridControls(RiskGrid)
                Else
                    CheckIfComingFromDeleteConfirm()
                End If
                BindBoPropertiesToGridHeaders()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.riskTypeErrorController)
            End Try
            Me.ShowMissingTranslations(riskTypeErrorController)
        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub SearchButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SearchButton.Click

            Try
                Me.State.IsGridVisible = True
                Me.State.searchDV = Nothing
                PopulateRiskGrid()
                Me.State.PageIndex = RiskGrid.PageIndex
            Catch ex As Exception
                Me.HandleErrors(ex, Me.riskTypeErrorController)
            End Try

        End Sub

        Private Sub ClearButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearButton.Click

            ClearSearchCriteria()

        End Sub

        Private Sub ClearSearchCriteria()

            Try
                RiskTypeTextBox.Text = String.Empty
                RiskTypeEnglishTextBox.Text = String.Empty
                'Set the 1st Item in the RiskGroupDrowpdown List to ""
                RiskGroupList.SelectedIndex = 0

                'Update Page State
                With Me.State
                    .DescriptionMask = RiskTypeTextBox.Text
                    .RiskTypeEnglishMask = RiskTypeEnglishTextBox.Text
                    .RiskGroupIdSearch = Nothing
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.riskTypeErrorController)
            End Try

        End Sub

        Private Sub NewButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewButton_WRITE.Click

            Try
                Me.State.IsEditMode = True
                Me.State.IsGridVisible = True
                Me.State.AddingNewRow = True
                Me.AddNewRiskType()
                Me.SetButtonsState()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.riskTypeErrorController)
            End Try

        End Sub

        Private Sub SaveButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton_WRITE.Click

            Try
                PopulateBOFromForm()
                If (Me.State.RiskTypeBO.IsDirty) Then
                    Me.State.RiskTypeBO.Save()
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
                Me.HandleErrors(ex, Me.riskTypeErrorController)
            End Try

        End Sub

        Private Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelButton.Click

            Try
                Me.RiskGrid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
                Me.State.Canceling = True
                If (Me.State.AddingNewRow) Then
                    Me.State.AddingNewRow = False
                    Me.State.searchDV = Nothing
                End If
                ReturnFromEditing()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.riskTypeErrorController)
            End Try

        End Sub

#End Region

#Region "Handlers-Grid"

        Public Property SortDirection() As String
            Get
                Return ViewState("SortDirection").ToString
            End Get
            Set(ByVal value As String)
                ViewState("SortDirection") = value
            End Set
        End Property


        Private Sub SortAndBindGrid()
            Me.TranslateGridControls(RiskGrid)

            'RiskGrid.DataSource = Me.State.searchDV
            'HighLightSortColumn(RiskGrid, Me.State.SortExpression)
            'Me.RiskGrid.DataBind()
            If (Me.State.searchDV.Count = 0) Then

                Me.State.bnoRow = True
                CreateHeaderForEmptyGrid(RiskGrid, Me.SortDirection)
            Else
                Me.State.bnoRow = False
                Me.RiskGrid.Enabled = True
                Me.RiskGrid.DataSource = Me.State.searchDV
                HighLightSortColumn(RiskGrid, Me.SortDirection)
                Me.RiskGrid.DataBind()
                If RiskType.Is_TaxByProductType_Yes = True Then
                    RiskGrid.Columns(Me.PRODUCT_TAX_TYPE_ID_COL).Visible = True
                Else
                    RiskGrid.Columns(Me.PRODUCT_TAX_TYPE_ID_COL).Visible = False
                End If
            End If
            If Not RiskGrid.BottomPagerRow.Visible Then RiskGrid.BottomPagerRow.Visible = True


            ControlMgr.SetVisibleControl(Me, RiskGrid, Me.State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, trPageSize, Me.RiskGrid.Visible)

            Session("recCount") = Me.State.searchDV.Count

            If Me.RiskGrid.Visible Then
                If (Me.State.AddingNewRow) Then
                    Me.lblRecordCount.Text = (Me.State.searchDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                Else
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, RiskGrid)
        End Sub

        Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                RiskGrid.PageIndex = NewCurrentPageIndex(RiskGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                Me.State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                Me.PopulateRiskGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.riskTypeErrorController)
            End Try
        End Sub

        Private Sub RiskGrid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles RiskGrid.PageIndexChanging

            Try
                If (Not (Me.State.IsEditMode)) Then
                    Me.State.PageIndex = e.NewPageIndex
                    Me.RiskGrid.PageIndex = Me.State.PageIndex
                    Me.PopulateRiskGrid()
                    Me.RiskGrid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.riskTypeErrorController)
            End Try

        End Sub

        Protected Sub ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

            Try
                Dim index As Integer

                If (e.CommandName = Me.EDIT_COMMAND) Then
                    index = CInt(e.CommandArgument)
                    'Do the Edit here

                    'Set the IsEditMode flag to TRUE
                    Me.State.IsEditMode = True

                    Me.State.RiskTypeId = New Guid(CType(Me.RiskGrid.Rows(index).Cells(Me.RISK_TYPE_ID_COL).FindControl(Me.RISK_TYPE_ID_CONTROL_NAME), Label).Text)
                    Me.State.RiskTypeBO = New RiskType(Me.State.RiskTypeId)

                    Me.PopulateRiskGrid()

                    Me.State.PageIndex = RiskGrid.PageIndex

                    Me.SetGridControls(Me.RiskGrid, False)

                    'Set focus on the Description TextBox for the EditItemIndex row
                    Me.SetFocusOnEditableFieldInGrid(Me.RiskGrid, Me.DESCRIPTION_COL, Me.DESCRIPTION_CONTROL_NAME, index)

                    Dim productTaxTypeList As DropDownList =
                        CType(RiskGrid.Rows(index).Cells(Me.PRODUCT_TAX_TYPE_ID_COL).FindControl(
                                Me.PRODUCT_TAX_TYPE_ID_CONTROL_NAME), DropDownList)
                    PopulateProductTaxTypeDropdown(productTaxTypeList, Not (Me.IsEditing))
                    Me.SetSelectedItem(productTaxTypeList, Me.State.RiskTypeBO.ProductTaxTypeId)

                    Dim riskGroupList As DropDownList = CType(RiskGrid.Rows(index).Cells(Me.RISK_GROUP_COL).FindControl(Me.RISK_GROUP_CONTROL_NAME), DropDownList)
                    'Invoke the RiskGroupFactoryLookup with NotingSelected = True
                    PopulateRiskGroupDropdown(riskGroupList, Not (Me.IsEditing))
                    Me.SetSelectedItem(riskGroupList, Me.State.RiskTypeBO.RiskGroupId)

                    Me.PopulateFormFromBO()

                    Me.SetButtonsState()

                ElseIf (e.CommandName = Me.DELETE_COMMAND) Then
                    index = CInt(e.CommandArgument)
                    Me.State.RiskTypeId = New Guid(CType(Me.RiskGrid.Rows(index).Cells(Me.RISK_TYPE_ID_COL).FindControl(Me.RISK_TYPE_ID_CONTROL_NAME), Label).Text)
                    Me.DisplayMessage(Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenDeletePromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete

                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.riskTypeErrorController)
            End Try

        End Sub

        Private Sub DoDelete()
            'Do the delete here

            'Save the RiskTypeId in the Session

            Dim riskTypeBO As RiskType = New RiskType(Me.State.RiskTypeId)

            riskTypeBO.Delete()

            'Call the Save() method in the RiskType Business Object here

            riskTypeBO.Save()

            Me.State.PageIndex = RiskGrid.PageIndex

            'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
            Me.State.IsAfterSave = True
            Me.State.searchDV = Nothing
            PopulateRiskGrid()
            Me.State.PageIndex = RiskGrid.PageIndex
        End Sub
        Private Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles RiskGrid.RowDataBound
            Try
                'BaseItemBound(source, e)
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

                If Not dvRow Is Nothing And Not Me.State.bnoRow Then

                    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                        CType(e.Row.Cells(Me.RISK_TYPE_ID_COL).FindControl(Me.RISK_TYPE_ID_CONTROL_NAME), Label).Text = GetGuidStringFromByteArray(CType(dvRow(RiskType.RISK_TYPE_ID_COL), Byte()))

                        If (Me.State.IsEditMode = True _
                                AndAlso Me.State.RiskTypeId.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow(RiskType.RISK_TYPE_ID_COL), Byte())))) Then
                            CType(e.Row.Cells(Me.DESCRIPTION_COL).FindControl(Me.DESCRIPTION_CONTROL_NAME), TextBox).Text = dvRow(RiskType.DESCRIPTION_COL).ToString
                            CType(e.Row.Cells(Me.RISK_TYPE_ENGLISH_COL).FindControl(Me.RISK_TYPE_ENGLISH_CONTROL_NAME), TextBox).Text = dvRow(RiskType.RISK_TYPE_ENGLISH_COL).ToString
                            CType(e.Row.Cells(Me.PRODUCT_TAX_TYPE_ID_COL).FindControl(
                                Me.PRODUCT_TAX_TYPE_ID_CONTROL_NAME), DropDownList).Text =
                                                dvRow(RiskType.PRODUCT_TAX_TYPE).ToString
                            CType(e.Row.Cells(Me.RISK_GROUP_COL).FindControl(Me.RISK_GROUP_CONTROL_NAME), DropDownList).Text = dvRow(RiskType.RISK_GROUP).ToString
                        Else
                            CType(e.Row.Cells(Me.DESCRIPTION_COL).FindControl(Me.DESCRIPTION_CONTROL_LABEL), Label).Text = dvRow(RiskType.DESCRIPTION_COL).ToString
                            CType(e.Row.Cells(Me.RISK_TYPE_ENGLISH_COL).FindControl(Me.RISK_TYPE_ENGLISH_LABEL), Label).Text = dvRow(RiskType.RISK_TYPE_ENGLISH_COL).ToString
                            CType(e.Row.Cells(Me.PRODUCT_TAX_TYPE_ID_COL).FindControl(
                                Me.PRODUCT_TAX_TYPE_ID_DD_LABEL), Label).Text =
                                    dvRow(RiskType.PRODUCT_TAX_TYPE).ToString
                            CType(e.Row.Cells(Me.RISK_GROUP_COL).FindControl(Me.RISK_GROUP_DD_LABEL), Label).Text = dvRow(RiskType.RISK_GROUP).ToString
                        End If
                        'e.Row.Cells(Me.ACCT_COMPANY_DESCRIPTION_COL).Text = dvRow(AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_ACCT_COMPANY_DESCRIPTION).ToString
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.riskTypeErrorController)
            End Try
        End Sub
        Protected Sub ItemBound(ByVal source As Object, ByVal e As GridViewRowEventArgs) Handles RiskGrid.RowDataBound

            Try
                If Not Me.State.bnoRow Then
                    BaseItemBound(source, e)
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.riskTypeErrorController)
            End Try
        End Sub

        Protected Sub ItemCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.riskTypeErrorController)
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

        Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles RiskGrid.Sorting
            Try
                Dim spaceIndex As Integer = Me.SortDirection.LastIndexOf(" ")


                If spaceIndex > 0 AndAlso Me.SortDirection.Substring(0, spaceIndex).Equals(e.SortExpression) Then
                    If Me.SortDirection.EndsWith(" ASC") Then
                        Me.SortDirection = e.SortExpression + " DESC"
                    Else
                        Me.SortDirection = e.SortExpression + " ASC"
                    End If
                Else
                    Me.SortDirection = e.SortExpression + " ASC"
                End If

                Me.State.PageIndex = 0
                Me.PopulateRiskGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.riskTypeErrorController)
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
                If (Me.State.searchDV Is Nothing) Then
                    Me.State.searchDV = GetDV()
                End If
                Me.State.searchDV.Sort = Me.SortDirection
                If (Me.State.IsAfterSave) Then
                    Me.State.IsAfterSave = False
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.RiskTypeId, Me.RiskGrid, Me.State.PageIndex)
                ElseIf (Me.State.IsEditMode) Then
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.RiskTypeId, Me.RiskGrid, Me.State.PageIndex, Me.State.IsEditMode)
                Else
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Guid.Empty, Me.RiskGrid, Me.State.PageIndex)
                End If

                Me.RiskGrid.AutoGenerateColumns = False
                Me.RiskGrid.Columns(Me.DESCRIPTION_COL).SortExpression = RiskType.DESCRIPTION_COL
                Me.RiskGrid.Columns(Me.RISK_TYPE_ENGLISH_COL).SortExpression = RiskType.RISK_TYPE_ENGLISH_COL
                Me.RiskGrid.Columns(Me.PRODUCT_TAX_TYPE_ID_COL).SortExpression = RiskType.PRODUCT_TAX_TYPE
                Me.RiskGrid.Columns(Me.RISK_GROUP_COL).SortExpression = RiskType.RISK_GROUP

                SortAndBindGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.riskTypeErrorController)
            End Try

        End Sub

        Private Function GetDV() As DataView

            Dim dv As DataView

            Me.State.searchDV = GetRiskGridDataView()
            Me.State.searchDV.Sort = RiskGrid.DataMember()
            RiskGrid.DataSource = Me.State.searchDV

            Return (Me.State.searchDV)

        End Function

        Private Function GetRiskGridDataView() As DataView

            With State
                Return (RiskType.GetRiskTypeList(.DescriptionMask, .RiskTypeEnglishMask, .RiskGroupIdSearch, .LangId, .CompanyGroupId))
            End With

        End Function



        Private Sub PopulateProductTaxTypeDropdown(ByVal productTaxTypeDropDownList As DropDownList, ByVal nothingSelected As Boolean)
            Try

                Dim productTaxTypeListLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("PTT", Thread.CurrentPrincipal.GetLanguageCode())
                productTaxTypeDropDownList.Populate(productTaxTypeListLkl, New PopulateOptions() With
                                                              {
                                                                .AddBlankItem = nothingSelected
                                                               })
            Catch ex As Exception
                Me.HandleErrors(ex, Me.riskTypeErrorController)
            End Try

        End Sub

        Private Sub PopulateRiskGroupDropdown(ByVal riskGroupDropDownList As DropDownList, ByVal nothingSelected As Boolean)
            Try

                Dim riskGroupLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("RGRP", Thread.CurrentPrincipal.GetLanguageCode())
                riskGroupDropDownList.Populate(riskGroupLkl, New PopulateOptions() With
                                                              {
                                                                .AddBlankItem = nothingSelected
                                                               })
            Catch ex As Exception
                Me.HandleErrors(ex, Me.riskTypeErrorController)
            End Try

        End Sub

        Private Sub AddNewRiskType()

            Dim dv As DataView

            Me.State.searchDV = GetRiskGridDataView()

            Me.State.RiskTypeBO = New RiskType
            Me.State.RiskTypeId = Me.State.RiskTypeBO.RiskTypeId
            Me.State.RiskTypeBO.CompanyGroupId = Me.State.CompanyGroupId

            Me.State.searchDV = Me.State.RiskTypeBO.GetNewDataViewRow(Me.State.searchDV, Me.State.RiskTypeId)

            RiskGrid.DataSource = Me.State.searchDV

            Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.RiskTypeId, Me.RiskGrid,
                                               Me.State.PageIndex, Me.State.IsEditMode)

            Me.RiskGrid.AutoGenerateColumns = False
            Me.RiskGrid.Columns(Me.DESCRIPTION_COL).SortExpression = RiskType.DESCRIPTION_COL
            Me.RiskGrid.Columns(Me.RISK_TYPE_ENGLISH_COL).SortExpression = RiskType.RISK_TYPE_ENGLISH_COL
            Me.RiskGrid.Columns(Me.PRODUCT_TAX_TYPE_ID_COL).SortExpression = RiskType.PRODUCT_TAX_TYPE
            Me.RiskGrid.Columns(Me.RISK_GROUP_COL).SortExpression = RiskType.RISK_GROUP
            HighLightSortColumn(RiskGrid, Me.SortDirection)
            RiskGrid.DataBind()

            If RiskType.Is_TaxByProductType_Yes = True Then
                RiskGrid.Columns(Me.PRODUCT_TAX_TYPE_ID_COL).Visible = True
            Else
                RiskGrid.Columns(Me.PRODUCT_TAX_TYPE_ID_COL).Visible = False
            End If

            Me.State.PageIndex = RiskGrid.PageIndex

            SetGridControls(Me.RiskGrid, False)

            ''Set focus on the Description TextBox for the EditItemIndex row
            Me.SetFocusOnEditableFieldInGrid(Me.RiskGrid, Me.DESCRIPTION_COL, Me.DESCRIPTION_CONTROL_NAME, RiskGrid.EditIndex)
            Dim productTaxTypeList As DropDownList = CType(
                RiskGrid.Rows(RiskGrid.EditIndex).Cells(Me.PRODUCT_TAX_TYPE_ID_COL).FindControl(
                                Me.PRODUCT_TAX_TYPE_ID_CONTROL_NAME), DropDownList)
            PopulateProductTaxTypeDropdown(productTaxTypeList, False)

            Dim riskGroupList As DropDownList = CType(RiskGrid.Rows(RiskGrid.EditIndex).Cells(Me.RISK_GROUP_COL).FindControl(Me.RISK_GROUP_CONTROL_NAME), DropDownList)
            'Invoke the RiskGroupFactoryLookup with NotingSelected = False
            PopulateRiskGroupDropdown(riskGroupList, False)
            Me.PopulateFormFromBO()

            Me.TranslateGridControls(RiskGrid)

            Me.SetButtonsState()
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, RiskGrid)
        End Sub

        Private Sub PopulateBOFromForm()
            Try
                With Me.State.RiskTypeBO
                    .Description = CType(Me.RiskGrid.Rows(Me.RiskGrid.EditIndex).Cells(Me.DESCRIPTION_COL).FindControl(Me.DESCRIPTION_CONTROL_NAME), TextBox).Text
                    .RiskTypeEnglish = CType(Me.RiskGrid.Rows(Me.RiskGrid.EditIndex).Cells(Me.RISK_TYPE_ENGLISH_COL).FindControl(Me.RISK_TYPE_ENGLISH_CONTROL_NAME), TextBox).Text
                    .ProductTaxTypeId = Me.GetSelectedItem(CType(
                            RiskGrid.Rows(RiskGrid.EditIndex).Cells(
                            Me.PRODUCT_TAX_TYPE_ID_COL).FindControl(Me.PRODUCT_TAX_TYPE_ID_CONTROL_NAME), DropDownList))
                    .RiskGroupId = Me.GetSelectedItem(CType(RiskGrid.Rows(RiskGrid.EditIndex).Cells(Me.RISK_GROUP_COL).FindControl(Me.RISK_GROUP_CONTROL_NAME), DropDownList))
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.riskTypeErrorController)
            End Try

        End Sub

        Private Sub PopulateFormFromBO()

            Dim gridRowIdx As Integer = Me.RiskGrid.EditIndex
            Try
                With Me.State.RiskTypeBO
                    If (Not .Description Is Nothing) Then
                        CType(Me.RiskGrid.Rows(gridRowIdx).Cells(Me.DESCRIPTION_COL).FindControl(Me.DESCRIPTION_CONTROL_NAME), TextBox).Text = .Description
                    End If
                    If (Not .RiskTypeEnglish Is Nothing) Then
                        CType(Me.RiskGrid.Rows(gridRowIdx).Cells(Me.RISK_TYPE_ENGLISH_COL).FindControl(Me.RISK_TYPE_ENGLISH_CONTROL_NAME), TextBox).Text = .RiskTypeEnglish
                    End If

                    If (.ProductTaxTypeId.Equals(Guid.Empty)) Then
                        Me.SetSelectedItem(CType(RiskGrid.Rows(gridRowIdx).Cells(
                                Me.RISK_GROUP_COL).FindControl(
                                Me.PRODUCT_TAX_TYPE_ID_CONTROL_NAME), DropDownList),
                            LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(
                            Codes.PRODUCT_TAX_TYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId), Codes.PRODUCT_TAX_TYPE__ALL))
                    Else
                        Me.SetSelectedItem(CType(RiskGrid.Rows(gridRowIdx).Cells(
                                Me.RISK_GROUP_COL).FindControl(
                                Me.PRODUCT_TAX_TYPE_ID_CONTROL_NAME), DropDownList), .ProductTaxTypeId)
                    End If
                    If (Not .RiskGroupId.Equals(Guid.Empty)) Then
                        Me.SetSelectedItem(CType(RiskGrid.Rows(gridRowIdx).Cells(Me.RISK_GROUP_COL).FindControl(Me.RISK_GROUP_CONTROL_NAME), DropDownList), .RiskGroupId)
                    End If
                    CType(Me.RiskGrid.Rows(gridRowIdx).Cells(Me.RISK_TYPE_ID_COL).FindControl(Me.RISK_TYPE_ID_CONTROL_NAME), Label).Text = .RiskTypeId.ToString
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.riskTypeErrorController)
            End Try

        End Sub



#End Region

#Region "Control"

        Private Sub ReturnFromEditing()

            RiskGrid.EditIndex = NO_ROW_SELECTED_INDEX

            If (Me.RiskGrid.PageCount = 0) Then
                'if returning to the "1st time in" screen
                ControlMgr.SetVisibleControl(Me, RiskGrid, False)
            Else
                ControlMgr.SetVisibleControl(Me, RiskGrid, True)
            End If

            Me.State.IsEditMode = False
            Me.PopulateRiskGrid()
            Me.State.PageIndex = RiskGrid.PageIndex
            SetButtonsState()

        End Sub

        Private Sub SetButtonsState()

            If (Me.State.IsEditMode) Then
                ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, True)
                ControlMgr.SetVisibleControl(Me, CancelButton, True)
                ControlMgr.SetVisibleControl(Me, NewButton_WRITE, False)
                ControlMgr.SetEnableControl(Me, SearchButton, False)
                ControlMgr.SetEnableControl(Me, ClearButton, False)
                RiskGroupList.Enabled = False
                Me.MenuEnabled = False
            Else
                ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, False)
                ControlMgr.SetVisibleControl(Me, CancelButton, False)
                ControlMgr.SetVisibleControl(Me, NewButton_WRITE, True)
                ControlMgr.SetEnableControl(Me, SearchButton, True)
                ControlMgr.SetEnableControl(Me, ClearButton, True)
                ControlMgr.SetEnableControl(Me, RiskGroupList, True)
                Me.MenuEnabled = True
            End If

        End Sub

        Protected Sub BindBoPropertiesToGridHeaders()
            Me.BindBOPropertyToGridHeader(Me.State.RiskTypeBO, "Description", Me.RiskGrid.Columns(Me.DESCRIPTION_COL))
            Me.BindBOPropertyToGridHeader(Me.State.RiskTypeBO, "RiskTypeEnglish", Me.RiskGrid.Columns(Me.RISK_TYPE_ENGLISH_COL))
            Me.ClearGridViewHeadersAndLabelsErrSign()
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(ByVal grid As GridView, ByVal cellPosition As Integer, ByVal controlName As String, ByVal itemIndex As Integer)
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

            Me.State.DescriptionMask = RiskTypeTextBox.Text
            Me.State.RiskTypeEnglishMask = RiskTypeEnglishTextBox.Text
            If (Not RiskGroupList.SelectedItem Is Nothing AndAlso RiskGroupList.SelectedItem.Value <> Me.NOTHING_SELECTED_TEXT) Then
                Me.State.RiskGroupIdSearch = Me.GetGuidFromString(RiskGroupList.SelectedItem.Value)
            Else
                Me.State.RiskGroupIdSearch = Nothing
            End If
            Me.State.LangId = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Me.State.CompanyId = ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID
            Me.State.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id

        End Sub

        Protected Sub CheckIfComingFromDeleteConfirm()
            Dim confResponse As String = Me.HiddenDeletePromptResponse.Value
            If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
                If Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete Then
                    DoDelete()
                End If
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Delete
                End Select
            ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Delete
                End Select
            End If
            'Clean after consuming the action
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            Me.HiddenDeletePromptResponse.Value = ""
        End Sub


#End Region

    End Class

End Namespace


