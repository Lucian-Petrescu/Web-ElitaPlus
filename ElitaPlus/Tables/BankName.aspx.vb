
Option Strict On
Option Explicit On
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Partial Class BankNameForm
    Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "

    Protected WithEvents ErrorCtrl As ErrorController

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

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As Company
        Public HasDataChanged As Boolean
        Public Sub New(LastOp As DetailPageCommand, curEditingBo As Company, hasDataChanged As Boolean)
            LastOperation = LastOp
            EditingBo = curEditingBo
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class
#End Region


#Region "Constants"

    Public Const GRID_COL_ID_IDX As Integer = 0
    Public Const GRID_COL_DESCRIPTION_IDX As Integer = 1
    Public Const GRID_COL_CODE_IDX As Integer = 2

    Public Const GRID_CONTROL_BANK_NAME_ID_LABEL As String = "lblBankNameID"
    Public Const GRID_CONTROL_BANK_NAME_LABEL As String = "lblBankName"
    Public Const GRID_CONTROL_BANK_NAME_TEXTBOX As String = "txtBankName"
    Public Const GRID_CONTROL_BANK_CODE_LABEL As String = "lblBankCode"
    Public Const GRID_CONTROL_BANK_CODE_TEXTBOX As String = "txtBankCode"

    Public Const GRID_TOTAL_COLUMNS As Integer = 4
    Private Const NOTHING_SELECTED As String = "00000000-0000-0000-0000-000000000000"
    Private Const BANKNAMEFORM As String = "BankName.aspx"
    Private Const BANK_NAME_TEXTBOX_MAX_LENGTH As Integer = 100
    Private Const BANK_CODE_TEXTBOX_MAX_LENGTH As Integer = 10

#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    ' This class keeps the current state for the search page.
    Class MyState
        Public MyBO As BankName
        Public PageIndex As Integer = 0
        Public CodeMask As String
        Public BankName As String
        Public CountryID As Guid = Guid.Empty
        Public BankCodeMaxLength As Long = 10
        Public IsGridVisible As Boolean
        Public searchDV As BankName.BankNameSearchDV = Nothing
        Public selectedPageSize As Integer = DEFAULT_NEW_UI_PAGE_SIZE
        Public SortExpression As String = Company.CompanySearchDV.COL_DESCRIPTION
        Public HasDataChanged As Boolean
        Public bnoRow As Boolean = False
        Public BankNameID As Guid = Guid.Empty
        Public IsRowBeingEdited As Boolean = False

        Sub New()

        End Sub

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

#Region "Page_Events"

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        MasterPage.MessageController.Clear_Hide()
        SetStateProperties()

        Try
            If Not IsPostBack Then

                MasterPage.MessageController.Clear()
                MasterPage.UsePageTabTitleInBreadCrum = False
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")
                UpdateBreadCrum()
                TranslateGridHeader(moBankNamesGridView)
                PopulateBankNamesDropDown()
                SortDirection = State.SortExpression
                SetDefaultButton(ddlBankNameDropDown, btnSearch)
                SetDefaultButton(txtSearchBankCodeTextBox, btnSearch)
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                If Not (State.selectedPageSize = DEFAULT_PAGE_SIZE) Then
                    cboPageSize.SelectedValue = CType(State.selectedPageSize, String)
                    moBankNamesGridView.PageSize = State.selectedPageSize
                End If
                SetGridItemStyleColor(moBankNamesGridView)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub

    Private Sub UpdateBreadCrum()
        If (State IsNot Nothing) Then
            If (State IsNot Nothing) Then
                MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("BANK_NAME")
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("BANK_NAME")
            End If
        End If
    End Sub

    Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
        MenuEnabled = True
        IsReturningFromChild = True
        Dim retObj As BankNameForm.ReturnType = CType(ReturnPar, BankNameForm.ReturnType)
        State.HasDataChanged = retObj.HasDataChanged
        Select Case retObj.LastOperation
            Case ElitaPlusPage.DetailPageCommand.Delete
                AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
        End Select
    End Sub

#End Region

#Region "Controlling Logic"

    Public Sub PopulateGrid()
        If ((State.searchDV Is Nothing) OrElse (State.HasDataChanged)) Then
            State.searchDV = BankName.getList(State.BankName, State.CodeMask, State.CountryID)
        End If
        State.searchDV.Sort = SortDirection
        moBankNamesGridView.AutoGenerateColumns = False

        SetPageAndSelectedIndexFromGuid(State.searchDV, State.BankNameID, moBankNamesGridView, State.PageIndex, State.IsRowBeingEdited)
        SortAndBindGrid()
        State.HasDataChanged = False
    End Sub

    Private Sub SortAndBindGrid()
        State.PageIndex = moBankNamesGridView.PageIndex

        If (State.searchDV Is Nothing OrElse State.searchDV.Count = 0) Then
            Session("recCount") = 0
            State.searchDV.AddNew()
            State.searchDV(0)(0) = Guid.Empty.ToByteArray
            moBankNamesGridView.DataSource = State.searchDV
            moBankNamesGridView.DataBind()
            moBankNamesGridView.Rows(0).Visible = False
            moBankNamesGridView.Rows(0).Controls.Clear()
            State.bnoRow = True
        Else
            Session("recCount") = State.searchDV.Count
            State.bnoRow = False
            moBankNamesGridView.Enabled = True
            moBankNamesGridView.DataSource = State.searchDV
            HighLightSortColumn(moBankNamesGridView, SortDirection)
            moBankNamesGridView.DataBind()
        End If
        If Not moBankNamesGridView.BottomPagerRow.Visible Then moBankNamesGridView.BottomPagerRow.Visible = True

        ControlMgr.SetVisibleControl(Me, moBankNamesGridView, State.IsGridVisible)

        ControlMgr.SetVisibleControl(Me, trPageSize, moBankNamesGridView.Visible)

        If State.searchDV.Count > 0 Then

            If moBankNamesGridView.Visible Then
                lblRecordCount.Text = Session("recCount").ToString & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        Else
            If moBankNamesGridView.Visible Then
                lblRecordCount.Text = Session("recCount").ToString & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        End If
    End Sub

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
    'The Binding Logic is here
    Public Sub moBankNamesGridView_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles moBankNamesGridView.RowDataBound
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim CurrentBankNameID As Guid = New Guid(CType(dvRow.Row(BankName.BankNameSearchDV.COL_BANK_ID), Byte()))

            e.Row.Cells(GRID_COL_ID_IDX).Text = CurrentBankNameID.ToString

            If ((e.Row.RowState And DataControlRowState.Edit) > 0) Then
                If dvRow.Row(BankName.BankNameSearchDV.COL_DESCRIPTION) IsNot Nothing Then
                    Dim txtBankName As TextBox = CType(e.Row.FindControl(GRID_CONTROL_BANK_NAME_TEXTBOX), TextBox)
                    txtBankName.Text = dvRow.Row(BankName.BankNameSearchDV.COL_DESCRIPTION).ToString
                    txtBankName.MaxLength = BANK_NAME_TEXTBOX_MAX_LENGTH
                End If

                If dvRow.Row(BankName.BankNameSearchDV.COL_CODE) IsNot Nothing Then
                    Dim txtBankCode As TextBox = CType(e.Row.FindControl(GRID_CONTROL_BANK_CODE_TEXTBOX), TextBox)
                    txtBankCode.Text = dvRow.Row(BankName.BankNameSearchDV.COL_CODE).ToString
                    txtBankCode.MaxLength = BANK_CODE_TEXTBOX_MAX_LENGTH
                End If
                SetGridViewButtonsVisibility(e.Row, False, False, True, True)
            Else
                If dvRow.Row(BankName.BankNameSearchDV.COL_DESCRIPTION) IsNot Nothing Then
                    Dim lblBankName As Label = CType(e.Row.FindControl(GRID_CONTROL_BANK_NAME_LABEL), Label)
                    lblBankName.Text = dvRow.Row(BankName.BankNameSearchDV.COL_DESCRIPTION).ToString
                End If

                If dvRow.Row(BankName.BankNameSearchDV.COL_CODE) IsNot Nothing Then
                    Dim lblBankCode As Label = CType(e.Row.FindControl(GRID_CONTROL_BANK_CODE_LABEL), Label)
                    lblBankCode.Text = dvRow.Row(BankName.BankNameSearchDV.COL_CODE).ToString
                End If

                If State.IsRowBeingEdited Then
                    SetGridViewButtonsVisibility(e.Row, False, False, False, False)
                Else
                    SetGridViewButtonsVisibility(e.Row, True, True, False, False)
                End If

            End If
        End If

    End Sub

    Public Sub moBankNamesGridView_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Try
            Dim nIndex As Integer
            If e.CommandName = ElitaPlusSearchPage.EDIT_COMMAND_NAME Then
                nIndex = CInt(e.CommandArgument)
                moBankNamesGridView.SelectedIndex = nIndex
                moBankNamesGridView.EditIndex = nIndex
                State.BankNameID = New Guid(moBankNamesGridView.Rows(nIndex).Cells(GRID_COL_ID_IDX).Text)
                BeginEdit(State.BankNameID)
            ElseIf e.CommandName = ElitaPlusSearchPage.DELETE_COMMAND_NAME Then
                nIndex = CInt(e.CommandArgument)
                State.BankNameID = New Guid(moBankNamesGridView.Rows(nIndex).Cells(GRID_COL_ID_IDX).Text)
                BeginEdit(State.BankNameID)
                EndEdit(ElitaPlusPage.DetailPageCommand.Delete)
            ElseIf e.CommandName = ElitaPlusSearchPage.SAVE_COMMAND_NAME Then
                nIndex = CInt(e.CommandArgument)
                PopulateBoFromControls(moBankNamesGridView.Rows(nIndex))
                'Check if bank name / code is duplicated
                ValidateBankNameRecords()
                EndEdit(ElitaPlusPage.DetailPageCommand.OK)
            ElseIf e.CommandName = ElitaPlusSearchPage.CANCEL_COMMAND_NAME Then
                moBankNamesGridView.EditIndex = -1
                EndEdit(ElitaPlusPage.DetailPageCommand.Cancel)
            End If
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Public Sub moBankNamesGridView_RowCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles moBankNamesGridView.RowCreated
        BaseItemCreated(sender, e)
    End Sub

    Public Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            moBankNamesGridView.PageIndex = NewCurrentPageIndex(moBankNamesGridView, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Public Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles moBankNamesGridView.Sorting
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
            State.SortExpression = SortDirection
            State.PageIndex = 0
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Public Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles moBankNamesGridView.PageIndexChanging

        Try
            State.PageIndex = e.NewPageIndex
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub SetGridViewButtonsVisibility(gridrow As GridViewRow, bEditButtonVisible As Boolean, bDeleteButtonVisible As Boolean, bSaveButtonVisible As Boolean, bCancelButtonVisible As Boolean)
        Try
            Dim EditButton As ImageButton = CType(gridrow.Cells(EDIT_COL).FindControl(EDIT_CONTROL_NAME), ImageButton)
            If (EditButton IsNot Nothing) Then
                EditButton.Enabled = bEditButtonVisible
                EditButton.Visible = bEditButtonVisible
            End If

            Dim DeleteButton As ImageButton = CType(gridrow.Cells(EDIT_COL).FindControl(DELETE_CONTROL_NAME), ImageButton)
            If (DeleteButton IsNot Nothing) Then
                DeleteButton.Enabled = bDeleteButtonVisible
                DeleteButton.Visible = bDeleteButtonVisible
            End If

            Dim SaveButton As Button = CType(gridrow.Cells(EDIT_COL).FindControl(SAVE_CONTROL_NAME), Button)
            If (SaveButton IsNot Nothing) Then
                SaveButton.Enabled = bSaveButtonVisible
                SaveButton.Visible = bSaveButtonVisible
            End If

            Dim CancelButton As Button = CType(gridrow.Cells(EDIT_COL).FindControl(CANCEL_CONTROL_NAME), Button)
            If (CancelButton IsNot Nothing) Then
                CancelButton.Enabled = bCancelButtonVisible
                CancelButton.Visible = bCancelButtonVisible
            End If


        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub BeginEdit(CurrentBankNameID As Guid)
        Try
            State.IsRowBeingEdited = True
            With State
                If Not CurrentBankNameID.Equals(Guid.Empty) Then
                    .MyBO = New BankName(CurrentBankNameID)
                Else
                    .MyBO = New BankName()
                    .BankNameID = State.MyBO.Id
                    If .searchDV Is Nothing Then
                        .searchDV = .MyBO.GetNewDataViewRow(.MyBO.getList(" ", " ", State.MyBO.CountryID), .BankNameID, .CountryID)
                    Else
                        .searchDV = .MyBO.GetNewDataViewRow(.searchDV, .BankNameID, .CountryID)
                    End If
                End If
            End With
            ChangeEnabledProperty(btnAdd_WRITE, False)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region " Button Clicks "
    Private Sub SetStateProperties()

        Try
            State.BankName = ddlBankNameDropDown.SelectedItem.Text
        Catch ex As Exception
            State.BankName = String.Empty
        End Try

        State.CodeMask = txtSearchBankCodeTextBox.Text.Trim()
        Dim CurrentCountry As Country = ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID)
        State.CountryID = CurrentCountry.Id
        If (CurrentCountry.BankIDLength IsNot Nothing AndAlso CurrentCountry.BankIDLength.Value <> 0) Then
            State.BankCodeMaxLength = CurrentCountry.BankIDLength.Value
        End If
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
        Try
            State.PageIndex = 0
            State.BankNameID = Guid.Empty
            State.IsGridVisible = True
            State.searchDV = Nothing
            State.HasDataChanged = False
            State.IsRowBeingEdited = False
            PopulateGrid()
            ChangeEnabledProperty(btnAdd_WRITE, True)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnAdd_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnAdd_WRITE.Click
        State.IsGridVisible = True
        State.BankNameID = Guid.Empty
        BeginEdit(Guid.Empty)
        PopulateGrid()
    End Sub

    Private Sub btnClearSearch_Click1(sender As Object, e As System.EventArgs) Handles btnClearSearch.Click
        ClearSearchCriteria()
    End Sub
    Private Sub ClearSearchCriteria()

        Try
            ddlBankNameDropDown.SelectedIndex = 0
            txtSearchBankCodeTextBox.Text = String.Empty
            'Update Page State
            With State
                .BankName = String.Empty
                .CodeMask = String.Empty
                .searchDV = Nothing
                .BankNameID = Guid.Empty
                .IsRowBeingEdited = False
                .MyBO = Nothing
            End With
            ChangeEnabledProperty(btnAdd_WRITE, True)
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Private Sub PopulateBankNamesDropDown()
        Try
            Dim BankNameBO As New BankName
            'Dim dtBankNamesList As DataTable = BankNameBO.LoadBankNameByCountry(Me.State.CountryID)
            'If Not dtBankNamesList Is Nothing Then
            'Me.BindListTextToDataView(ddlBankNameDropDown, dtBankNamesList.DefaultView(), "DESCRIPTION", "DESCRIPTION", True, True)
            Dim BankName As DataElements.ListItem() =
                    CommonConfigManager.Current.ListManager.GetList(listCode:="BankNameByCountry",
                                                        context:=New ListContext() With
                                                        {
                                                          .CountryId = State.CountryID
                                                        })

                ddlBankNameDropDown.Populate(BankName.ToArray(),
                        New PopulateOptions() With
                        {
                            .AddBlankItem = True,
                            .ValueFunc = AddressOf .GetDescription
                        })
                Try
                    SetSelectedItemByText(ddlBankNameDropDown, State.BankName)
                Catch ex As Exception
                    ddlBankNameDropDown.SelectedIndex = 0
                    State.BankName = String.Empty
                End Try
            'End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub EndEdit(detailPageCommand As DetailPageCommand)
        Try
            With State
                Select Case detailPageCommand
                    Case ElitaPlusPage.DetailPageCommand.OK
                        .MyBO.Save()
                        .MyBO.EndEdit()
                        MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        .MyBO.Delete()
                        .MyBO.Save()
                        MasterPage.MessageController.AddSuccess(Message.DELETE_RECORD_CONFIRMATION, True)
                End Select
                .MyBO = Nothing
                .BankNameID = Guid.Empty
                .HasDataChanged = True
                .IsRowBeingEdited = False
                .searchDV = Nothing
                ChangeEnabledProperty(btnAdd_WRITE, True)
            End With
            PopulateBankNamesDropDown()
            'Me.PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Error Handling"


#End Region

    Private Sub PopulateBoFromControls(gridViewRow As GridViewRow)
        Try
            With State
                Dim txtBankName As TextBox = CType(gridViewRow.FindControl(GRID_CONTROL_BANK_NAME_TEXTBOX), TextBox)
                PopulateBOProperty(.MyBO, "Description", txtBankName.Text.Trim())

                Dim txtBankCode As TextBox = CType(gridViewRow.FindControl(GRID_CONTROL_BANK_CODE_TEXTBOX), TextBox)
                PopulateBOProperty(.MyBO, "Code", txtBankCode.Text.Trim())

            End With
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Function ValidateBankNameRecords() As Boolean

        If (String.IsNullOrEmpty(State.MyBO.Description)) Then
            Throw New GUIException(Message.MSG_BANK_NAME_VALUE_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.MSG_BANK_NAME_VALUE_REQUIRED)
        End If

        If (String.IsNullOrEmpty(State.MyBO.Code)) Then
            Throw New GUIException(Message.MSG_BANK_CODE_VALUE_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.MSG_BANK_CODE_VALUE_REQUIRED)
        End If

        If (Not String.IsNullOrEmpty(State.MyBO.Code) AndAlso State.MyBO.Code.Length > State.BankCodeMaxLength) Then
            Dim errors() As ValidationError = {New ValidationError(String.Format(TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_BANK_CODE_MAX_LENGTH_EXCEEDED), State.BankCodeMaxLength.ToString), GetType(BankName), Nothing, "Code", Nothing)}
            Throw New BOValidationException(errors, GetType(BankName).FullName)
        End If


        If Not String.IsNullOrEmpty(State.MyBO.Description) OrElse Not String.IsNullOrEmpty(State.MyBO.Code) Then
            Dim dvBankNameDV As BankName.BankNameSearchDV = State.MyBO.getList(String.Empty, String.Empty, State.MyBO.CountryID)

            If dvBankNameDV IsNot Nothing AndAlso dvBankNameDV.Count > 0 Then

                For Each BankNameDataRow As DataRow In dvBankNameDV.Table.Rows
                    Dim CurrentBankNameID As Guid = New Guid(CType(BankNameDataRow(BankName.BankNameSearchDV.COL_BANK_ID), Byte()))
                    If (Not CurrentBankNameID.Equals(State.MyBO.Id)) Then

                        'validate the bank name and code duplication
                        If Not String.IsNullOrEmpty(State.MyBO.Description) AndAlso _
                            BankNameDataRow(BankName.BankNameSearchDV.COL_DESCRIPTION).ToString().ToUpper() = State.MyBO.Description.ToUpper() AndAlso _
                            Not String.IsNullOrEmpty(State.MyBO.Code) AndAlso _
                            BankNameDataRow(BankName.BankNameSearchDV.COL_CODE).ToString().ToUpper() = State.MyBO.Code.ToUpper() Then

                            Throw New GUIException(Message.MSG_DUPLICATE_BANK_NAME_CODE, Assurant.ElitaPlus.Common.ErrorCodes.MSG_DUPLICATE_BANK_NAME_CODE)
                        ElseIf Not String.IsNullOrEmpty(State.MyBO.Description) AndAlso _
                             BankNameDataRow(BankName.BankNameSearchDV.COL_DESCRIPTION).ToString().ToUpper() = State.MyBO.Description.ToUpper() Then
                            Throw New GUIException(Message.MSG_DUPLICATE_BANK_NAME, Assurant.ElitaPlus.Common.ErrorCodes.MSG_DUPLICATE_BANK_NAME)
                        ElseIf Not String.IsNullOrEmpty(State.MyBO.Code) AndAlso _
                             BankNameDataRow(BankName.BankNameSearchDV.COL_CODE).ToString().ToUpper() = State.MyBO.Code.ToUpper() Then
                            Throw New GUIException(Message.MSG_DUPLICATE_BANK_CODE, Assurant.ElitaPlus.Common.ErrorCodes.MSG_DUPLICATE_BANK_CODE)
                        End If
                    End If

                Next
            End If

        End If
    End Function

End Class
