
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

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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
        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As Company, ByVal hasDataChanged As Boolean)
            Me.LastOperation = LastOp
            Me.EditingBo = curEditingBo
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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.MasterPage.MessageController.Clear_Hide()
        Me.SetStateProperties()

        Try
            If Not Me.IsPostBack Then

                Me.MasterPage.MessageController.Clear()
                Me.MasterPage.UsePageTabTitleInBreadCrum = False
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")
                UpdateBreadCrum()
                TranslateGridHeader(moBankNamesGridView)
                Me.PopulateBankNamesDropDown()
                Me.SortDirection = Me.State.SortExpression
                Me.SetDefaultButton(Me.ddlBankNameDropDown, btnSearch)
                Me.SetDefaultButton(Me.txtSearchBankCodeTextBox, btnSearch)
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                If Not (Me.State.selectedPageSize = DEFAULT_PAGE_SIZE) Then
                    cboPageSize.SelectedValue = CType(Me.State.selectedPageSize, String)
                    moBankNamesGridView.PageSize = Me.State.selectedPageSize
                End If
                Me.SetGridItemStyleColor(Me.moBankNamesGridView)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub

    Private Sub UpdateBreadCrum()
        If (Not Me.State Is Nothing) Then
            If (Not Me.State Is Nothing) Then
                Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("BANK_NAME")
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("BANK_NAME")
            End If
        End If
    End Sub

    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        Me.MenuEnabled = True
        Me.IsReturningFromChild = True
        Dim retObj As BankNameForm.ReturnType = CType(ReturnPar, BankNameForm.ReturnType)
        Me.State.HasDataChanged = retObj.HasDataChanged
        Select Case retObj.LastOperation
            Case ElitaPlusPage.DetailPageCommand.Delete
                Me.AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
        End Select
    End Sub

#End Region

#Region "Controlling Logic"

    Public Sub PopulateGrid()
        If ((Me.State.searchDV Is Nothing) OrElse (Me.State.HasDataChanged)) Then
            Me.State.searchDV = BankName.getList(Me.State.BankName, Me.State.CodeMask, Me.State.CountryID)
        End If
        Me.State.searchDV.Sort = Me.SortDirection
        Me.moBankNamesGridView.AutoGenerateColumns = False

        SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.BankNameID, Me.moBankNamesGridView, Me.State.PageIndex, Me.State.IsRowBeingEdited)
        Me.SortAndBindGrid()
        Me.State.HasDataChanged = False
    End Sub

    Private Sub SortAndBindGrid()
        Me.State.PageIndex = Me.moBankNamesGridView.PageIndex

        If (Me.State.searchDV Is Nothing OrElse Me.State.searchDV.Count = 0) Then
            Session("recCount") = 0
            Me.State.searchDV.AddNew()
            Me.State.searchDV(0)(0) = Guid.Empty.ToByteArray
            moBankNamesGridView.DataSource = Me.State.searchDV
            moBankNamesGridView.DataBind()
            moBankNamesGridView.Rows(0).Visible = False
            moBankNamesGridView.Rows(0).Controls.Clear()
            Me.State.bnoRow = True
        Else
            Session("recCount") = Me.State.searchDV.Count
            Me.State.bnoRow = False
            Me.moBankNamesGridView.Enabled = True
            Me.moBankNamesGridView.DataSource = Me.State.searchDV
            HighLightSortColumn(moBankNamesGridView, Me.SortDirection)
            Me.moBankNamesGridView.DataBind()
        End If
        If Not moBankNamesGridView.BottomPagerRow.Visible Then moBankNamesGridView.BottomPagerRow.Visible = True

        ControlMgr.SetVisibleControl(Me, moBankNamesGridView, Me.State.IsGridVisible)

        ControlMgr.SetVisibleControl(Me, trPageSize, Me.moBankNamesGridView.Visible)

        If Me.State.searchDV.Count > 0 Then

            If Me.moBankNamesGridView.Visible Then
                Me.lblRecordCount.Text = Session("recCount").ToString & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        Else
            If Me.moBankNamesGridView.Visible Then
                Me.lblRecordCount.Text = Session("recCount").ToString & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        End If
    End Sub

#End Region

#Region " Datagrid Related "
    Public Property SortDirection() As String
        Get
            Return ViewState("SortDirection").ToString
        End Get
        Set(ByVal value As String)
            ViewState("SortDirection") = value
        End Set
    End Property
    'The Binding Logic is here
    Public Sub moBankNamesGridView_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles moBankNamesGridView.RowDataBound
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim CurrentBankNameID As Guid = New Guid(CType(dvRow.Row(BankName.BankNameSearchDV.COL_BANK_ID), Byte()))

            e.Row.Cells(Me.GRID_COL_ID_IDX).Text = CurrentBankNameID.ToString

            If ((e.Row.RowState And DataControlRowState.Edit) > 0) Then
                If Not dvRow.Row(BankName.BankNameSearchDV.COL_DESCRIPTION) Is Nothing Then
                    Dim txtBankName As TextBox = CType(e.Row.FindControl(GRID_CONTROL_BANK_NAME_TEXTBOX), TextBox)
                    txtBankName.Text = dvRow.Row(BankName.BankNameSearchDV.COL_DESCRIPTION).ToString
                    txtBankName.MaxLength = BANK_NAME_TEXTBOX_MAX_LENGTH
                End If

                If Not dvRow.Row(BankName.BankNameSearchDV.COL_CODE) Is Nothing Then
                    Dim txtBankCode As TextBox = CType(e.Row.FindControl(GRID_CONTROL_BANK_CODE_TEXTBOX), TextBox)
                    txtBankCode.Text = dvRow.Row(BankName.BankNameSearchDV.COL_CODE).ToString
                    txtBankCode.MaxLength = BANK_CODE_TEXTBOX_MAX_LENGTH
                End If
                SetGridViewButtonsVisibility(e.Row, False, False, True, True)
            Else
                If Not dvRow.Row(BankName.BankNameSearchDV.COL_DESCRIPTION) Is Nothing Then
                    Dim lblBankName As Label = CType(e.Row.FindControl(GRID_CONTROL_BANK_NAME_LABEL), Label)
                    lblBankName.Text = dvRow.Row(BankName.BankNameSearchDV.COL_DESCRIPTION).ToString
                End If

                If Not dvRow.Row(BankName.BankNameSearchDV.COL_CODE) Is Nothing Then
                    Dim lblBankCode As Label = CType(e.Row.FindControl(GRID_CONTROL_BANK_CODE_LABEL), Label)
                    lblBankCode.Text = dvRow.Row(BankName.BankNameSearchDV.COL_CODE).ToString
                End If

                If Me.State.IsRowBeingEdited Then
                    SetGridViewButtonsVisibility(e.Row, False, False, False, False)
                Else
                    SetGridViewButtonsVisibility(e.Row, True, True, False, False)
                End If

            End If
        End If

    End Sub

    Public Sub moBankNamesGridView_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Try
            Dim nIndex As Integer
            If e.CommandName = ElitaPlusSearchPage.EDIT_COMMAND_NAME Then
                nIndex = CInt(e.CommandArgument)
                Me.moBankNamesGridView.SelectedIndex = nIndex
                Me.moBankNamesGridView.EditIndex = nIndex
                Me.State.BankNameID = New Guid(moBankNamesGridView.Rows(nIndex).Cells(Me.GRID_COL_ID_IDX).Text)
                Me.BeginEdit(Me.State.BankNameID)
            ElseIf e.CommandName = ElitaPlusSearchPage.DELETE_COMMAND_NAME Then
                nIndex = CInt(e.CommandArgument)
                Me.State.BankNameID = New Guid(moBankNamesGridView.Rows(nIndex).Cells(Me.GRID_COL_ID_IDX).Text)
                Me.BeginEdit(Me.State.BankNameID)
                Me.EndEdit(ElitaPlusPage.DetailPageCommand.Delete)
            ElseIf e.CommandName = ElitaPlusSearchPage.SAVE_COMMAND_NAME Then
                nIndex = CInt(e.CommandArgument)
                Me.PopulateBoFromControls(moBankNamesGridView.Rows(nIndex))
                'Check if bank name / code is duplicated
                ValidateBankNameRecords()
                Me.EndEdit(ElitaPlusPage.DetailPageCommand.OK)
            ElseIf e.CommandName = ElitaPlusSearchPage.CANCEL_COMMAND_NAME Then
                Me.moBankNamesGridView.EditIndex = -1
                Me.EndEdit(ElitaPlusPage.DetailPageCommand.Cancel)
            End If
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Public Sub moBankNamesGridView_RowCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles moBankNamesGridView.RowCreated
        BaseItemCreated(sender, e)
    End Sub

    Public Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            moBankNamesGridView.PageIndex = NewCurrentPageIndex(moBankNamesGridView, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Public Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles moBankNamesGridView.Sorting
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
            Me.State.SortExpression = Me.SortDirection
            Me.State.PageIndex = 0
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Public Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles moBankNamesGridView.PageIndexChanging

        Try
            Me.State.PageIndex = e.NewPageIndex
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub SetGridViewButtonsVisibility(ByVal gridrow As GridViewRow, ByVal bEditButtonVisible As Boolean, ByVal bDeleteButtonVisible As Boolean, ByVal bSaveButtonVisible As Boolean, ByVal bCancelButtonVisible As Boolean)
        Try
            Dim EditButton As ImageButton = CType(gridrow.Cells(EDIT_COL).FindControl(EDIT_CONTROL_NAME), ImageButton)
            If (Not EditButton Is Nothing) Then
                EditButton.Enabled = bEditButtonVisible
                EditButton.Visible = bEditButtonVisible
            End If

            Dim DeleteButton As ImageButton = CType(gridrow.Cells(EDIT_COL).FindControl(DELETE_CONTROL_NAME), ImageButton)
            If (Not DeleteButton Is Nothing) Then
                DeleteButton.Enabled = bDeleteButtonVisible
                DeleteButton.Visible = bDeleteButtonVisible
            End If

            Dim SaveButton As Button = CType(gridrow.Cells(EDIT_COL).FindControl(SAVE_CONTROL_NAME), Button)
            If (Not SaveButton Is Nothing) Then
                SaveButton.Enabled = bSaveButtonVisible
                SaveButton.Visible = bSaveButtonVisible
            End If

            Dim CancelButton As Button = CType(gridrow.Cells(EDIT_COL).FindControl(CANCEL_CONTROL_NAME), Button)
            If (Not CancelButton Is Nothing) Then
                CancelButton.Enabled = bCancelButtonVisible
                CancelButton.Visible = bCancelButtonVisible
            End If


        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub BeginEdit(ByVal CurrentBankNameID As Guid)
        Try
            Me.State.IsRowBeingEdited = True
            With Me.State
                If Not CurrentBankNameID.Equals(Guid.Empty) Then
                    .MyBO = New BankName(CurrentBankNameID)
                Else
                    .MyBO = New BankName()
                    .BankNameID = Me.State.MyBO.Id
                    If .searchDV Is Nothing Then
                        .searchDV = .MyBO.GetNewDataViewRow(.MyBO.getList(" ", " ", Me.State.MyBO.CountryID), .BankNameID, .CountryID)
                    Else
                        .searchDV = .MyBO.GetNewDataViewRow(.searchDV, .BankNameID, .CountryID)
                    End If
                End If
            End With
            Me.ChangeEnabledProperty(Me.btnAdd_WRITE, False)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region " Button Clicks "
    Private Sub SetStateProperties()

        Try
            Me.State.BankName = ddlBankNameDropDown.SelectedItem.Text
        Catch ex As Exception
            Me.State.BankName = String.Empty
        End Try

        Me.State.CodeMask = txtSearchBankCodeTextBox.Text.Trim()
        Dim CurrentCountry As Country = ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID)
        Me.State.CountryID = CurrentCountry.Id
        If (Not CurrentCountry.BankIDLength Is Nothing AndAlso CurrentCountry.BankIDLength.Value <> 0) Then
            Me.State.BankCodeMaxLength = CurrentCountry.BankIDLength.Value
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Me.State.PageIndex = 0
            Me.State.BankNameID = Guid.Empty
            Me.State.IsGridVisible = True
            Me.State.searchDV = Nothing
            Me.State.HasDataChanged = False
            Me.State.IsRowBeingEdited = False
            Me.PopulateGrid()
            Me.ChangeEnabledProperty(Me.btnAdd_WRITE, True)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnAdd_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd_WRITE.Click
        Me.State.IsGridVisible = True
        Me.State.BankNameID = Guid.Empty
        Me.BeginEdit(Guid.Empty)
        PopulateGrid()
    End Sub

    Private Sub btnClearSearch_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
        ClearSearchCriteria()
    End Sub
    Private Sub ClearSearchCriteria()

        Try
            ddlBankNameDropDown.SelectedIndex = 0
            txtSearchBankCodeTextBox.Text = String.Empty
            'Update Page State
            With Me.State
                .BankName = String.Empty
                .CodeMask = String.Empty
                .searchDV = Nothing
                .BankNameID = Guid.Empty
                .IsRowBeingEdited = False
                .MyBO = Nothing
            End With
            Me.ChangeEnabledProperty(Me.btnAdd_WRITE, True)
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
                                                          .CountryId = Me.State.CountryID
                                                        })

                Me.ddlBankNameDropDown.Populate(BankName.ToArray(),
                        New PopulateOptions() With
                        {
                            .AddBlankItem = True,
                            .ValueFunc = AddressOf .GetDescription
                        })
                Try
                    Me.SetSelectedItemByText(ddlBankNameDropDown, Me.State.BankName)
                Catch ex As Exception
                    Me.ddlBankNameDropDown.SelectedIndex = 0
                    Me.State.BankName = String.Empty
                End Try
            'End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub EndEdit(ByVal detailPageCommand As DetailPageCommand)
        Try
            With Me.State
                Select Case detailPageCommand
                    Case ElitaPlusPage.DetailPageCommand.OK
                        .MyBO.Save()
                        .MyBO.EndEdit()
                        Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        .MyBO.Delete()
                        .MyBO.Save()
                        Me.MasterPage.MessageController.AddSuccess(Message.DELETE_RECORD_CONFIRMATION, True)
                End Select
                .MyBO = Nothing
                .BankNameID = Guid.Empty
                .HasDataChanged = True
                .IsRowBeingEdited = False
                .searchDV = Nothing
                Me.ChangeEnabledProperty(Me.btnAdd_WRITE, True)
            End With
            PopulateBankNamesDropDown()
            'Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Error Handling"


#End Region

    Private Sub PopulateBoFromControls(ByVal gridViewRow As GridViewRow)
        Try
            With Me.State
                Dim txtBankName As TextBox = CType(gridViewRow.FindControl(GRID_CONTROL_BANK_NAME_TEXTBOX), TextBox)
                Me.PopulateBOProperty(.MyBO, "Description", txtBankName.Text.Trim())

                Dim txtBankCode As TextBox = CType(gridViewRow.FindControl(GRID_CONTROL_BANK_CODE_TEXTBOX), TextBox)
                Me.PopulateBOProperty(.MyBO, "Code", txtBankCode.Text.Trim())

            End With
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Function ValidateBankNameRecords() As Boolean

        If (String.IsNullOrEmpty(Me.State.MyBO.Description)) Then
            Throw New GUIException(Message.MSG_BANK_NAME_VALUE_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.MSG_BANK_NAME_VALUE_REQUIRED)
        End If

        If (String.IsNullOrEmpty(Me.State.MyBO.Code)) Then
            Throw New GUIException(Message.MSG_BANK_CODE_VALUE_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.MSG_BANK_CODE_VALUE_REQUIRED)
        End If

        If (Not String.IsNullOrEmpty(Me.State.MyBO.Code) AndAlso Me.State.MyBO.Code.Length > Me.State.BankCodeMaxLength) Then
            Dim errors() As ValidationError = {New ValidationError(String.Format(TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_BANK_CODE_MAX_LENGTH_EXCEEDED), Me.State.BankCodeMaxLength.ToString), GetType(BankName), Nothing, "Code", Nothing)}
            Throw New BOValidationException(errors, GetType(BankName).FullName)
        End If


        If Not String.IsNullOrEmpty(Me.State.MyBO.Description) OrElse Not String.IsNullOrEmpty(Me.State.MyBO.Code) Then
            Dim dvBankNameDV As BankName.BankNameSearchDV = Me.State.MyBO.getList(String.Empty, String.Empty, Me.State.MyBO.CountryID)

            If Not dvBankNameDV Is Nothing AndAlso dvBankNameDV.Count > 0 Then

                For Each BankNameDataRow As DataRow In dvBankNameDV.Table.Rows
                    Dim CurrentBankNameID As Guid = New Guid(CType(BankNameDataRow(BankName.BankNameSearchDV.COL_BANK_ID), Byte()))
                    If (Not CurrentBankNameID.Equals(Me.State.MyBO.Id)) Then

                        'validate the bank name and code duplication
                        If Not String.IsNullOrEmpty(Me.State.MyBO.Description) AndAlso _
                            BankNameDataRow(BankName.BankNameSearchDV.COL_DESCRIPTION).ToString().ToUpper() = Me.State.MyBO.Description.ToUpper() AndAlso _
                            Not String.IsNullOrEmpty(Me.State.MyBO.Code) AndAlso _
                            BankNameDataRow(BankName.BankNameSearchDV.COL_CODE).ToString().ToUpper() = Me.State.MyBO.Code.ToUpper() Then

                            Throw New GUIException(Message.MSG_DUPLICATE_BANK_NAME_CODE, Assurant.ElitaPlus.Common.ErrorCodes.MSG_DUPLICATE_BANK_NAME_CODE)
                        ElseIf Not String.IsNullOrEmpty(Me.State.MyBO.Description) AndAlso _
                             BankNameDataRow(BankName.BankNameSearchDV.COL_DESCRIPTION).ToString().ToUpper() = Me.State.MyBO.Description.ToUpper() Then
                            Throw New GUIException(Message.MSG_DUPLICATE_BANK_NAME, Assurant.ElitaPlus.Common.ErrorCodes.MSG_DUPLICATE_BANK_NAME)
                        ElseIf Not String.IsNullOrEmpty(Me.State.MyBO.Code) AndAlso _
                             BankNameDataRow(BankName.BankNameSearchDV.COL_CODE).ToString().ToUpper() = Me.State.MyBO.Code.ToUpper() Then
                            Throw New GUIException(Message.MSG_DUPLICATE_BANK_CODE, Assurant.ElitaPlus.Common.ErrorCodes.MSG_DUPLICATE_BANK_CODE)
                        End If
                    End If

                Next
            End If

        End If
    End Function

End Class
