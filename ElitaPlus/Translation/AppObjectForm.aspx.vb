Imports System.Text.RegularExpressions

Namespace Translation
    Partial Class AppObjectForm
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
        Public Const URL As String = "Tables/FormCategoryForm.aspx"
        Public Const PAGETITLE As String = "Add New Application Objects"
        Public Const PAGETAB As String = "ADMIN"

        Protected Const GOOD_RETURN As Int64 = 0
        Protected Const DELETE_TEXT As String = "DELETE"
        Protected Const APPOBJECTFORM001 As String = "APPOBJECTFORM001"
        Protected Const APPOBJECTFORM002 As String = "APPOBJECTFORM002"
        Protected Const APPOBJECTFORM003 As String = "APPOBJECTFORM003"
        Protected Const APPOBJECTFORM004 As String = "APPOBJECTFORM004"
        Protected Const LINEBREAK As String = " "

        'TODO Insert in Dictionary Table
        Protected Const CONFIRM_MSG As String = "Are you sure you want to clear the current tables?"

        Private Const GRID_COL_EDIT_IDX As Integer = 0
        Private Const GRID_COL_DELETE_IDX As Integer = 1
        Private Const GRID_COL_NEW_FORM_ID_IDX As Integer = 2
        Private Const GRID_COL_TAB_IDX As Integer = 3
        Private Const GRID_COL_CODE_IDX As Integer = 4
        Private Const GRID_COL_ENGLISH_IDX As Integer = 5
        Private Const GRID_COL_URL_IDX As Integer = 6
        Private Const GRID_COL_NAV_ALLOWED_IDX As Integer = 7
        Private Const GRID_COL_FORM_CATEGORY_IDX As Integer = 8
        Private Const GRID_COL_QUERY_STRING_IDX As Integer = 8

        Private Const GRID_CTRL_NAME_NEW_FORM_ID As String = "lblNewFormID"
        Private Const GRID_CTRL_NAME_TAB As String = "ddlTab"
        Private Const GRID_CTRL_NAME_TAB_Label As String = "lblTab"
        Private Const GRID_CTRL_NAME_FORM_CODE As String = "txtFormCode"
        Private Const GRID_CTRL_NAME_FORM_CODE_Label As String = "lblFormCode"
        Private Const GRID_CTRL_NAME_ENGLISH As String = "txtEnglish"
        Private Const GRID_CTRL_NAME_ENGLISH_Label As String = "lblEnglish"
        Private Const GRID_CTRL_NAME_URL As String = "txtRelativeURL"
        Private Const GRID_CTRL_NAME_URL_Label As String = "lblRelativeURL"
        Private Const GRID_CTRL_NAME_NAV_ALLOWED As String = "ddlNavAllowed"
        Private Const GRID_CTRL_NAME_NAV_ALLOWED_Label As String = "lblNavAllowed"
        Private Const GRID_CTRL_NAME_FORM_CATEGORY As String = "ddlFormCategory"
        Private Const GRID_CTRL_NAME_FORM_CATEGORY_Label As String = "lblFormCategory"
        Private Const GRID_CTRL_NAME_QUERY_STRING As String = "txtQueryString"
        Private Const GRID_CTRL_NAME_QUERY_STRING_Label As String = "lblQueryString"
#End Region

#Region "Properties and members"
        Private moTabs As DataView
        Private moForms As DataView
        Private moProc As TabForm
        Private _TabList As DataView
        Private _FormCatList As DataView

        Private ReadOnly Property Tabs() As DataView
            Get
                If moTabs Is Nothing Then
                    moTabs = TabForm.GetNewTabList()
                End If
                Return moTabs
            End Get
        End Property

        Private ReadOnly Property Forms() As DataView
            Get
                If moForms Is Nothing Then
                    moForms = TabForm.GetNewFormList()
                End If
                Return moForms
            End Get
        End Property

        Private ReadOnly Property Proc() As TabForm
            Get
                If moProc Is Nothing Then
                    moProc = New TabForm
                End If
                Return moProc
            End Get
        End Property

        Public ReadOnly Property IsGridFormInEditMode() As Boolean
            Get
                Return Me.GridForm.EditIndex > Me.NO_ITEM_SELECTED_INDEX
            End Get
        End Property

        Public ReadOnly Property FORMCATLIST() As DataView
            Get
                If _FormCatList Is Nothing Then
                    _FormCatList = FormCategory.getFormCategoryList()
                End If
                Return _FormCatList
            End Get
        End Property

        Public ReadOnly Property TABLIST() As DataView
            Get
                If _TabList Is Nothing Then
                    _TabList = FormCategory.getTabList()
                End If
                Return _TabList
            End Get
        End Property
#End Region

#Region "Page State"
        ' This class keeps the current state for the search page.
        Class MyState
            Public IsGridAddNew As Boolean = False
            Public IsGridVisible As Boolean
            Public GridFormEditIndex As Integer = NO_ITEM_SELECTED_INDEX
            Public IsFormValueChanged As Boolean = False
            'Public searchDV As FormCategory.FormCategorySearchDV = Nothing
            Public dvForms As DataView
            Public NewFormID As Guid
            Public NewFormTab As String = ""
            Public NewFormEnglish As String = ""
            Public NewFormCode As String = ""
            Public NewFormURL As String = ""
            Public NewFormNavAllowed As String = ""
            Public NewFormFormCat As String = ""
            Public QueryString As String = ""
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


#Region "Page event"
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Me.ErrControllerMaster.Clear_Hide()
            ShowAllStatistics(False)
            If Not Page.IsPostBack Then
                Me.SetFormTitle(PAGETITLE)
                Me.SetFormTab(PAGETAB)
                'populateSearchControls()
                TranslateGridHeader(GridTab)
                TranslateGridHeader(GridForm)

                Me.btnClearTables.Attributes.Add("onclick", "javascript:return window.confirm('" & Me.CONFIRM_MSG & "');")
                PopulateTabs()
                PopulateForms()
                ShowAllStatistics(False)
                Me.btnImport.Text = "Import New Objects to " & EnvironmentContext.Current.EnvironmentName
            End If
            'Me.ShowMissingTranslations(Me.ErrControllerMaster)
        End Sub

        Private Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
            Try
                SetControlState()
                Dim blnIsDev As Boolean = True
                If EnvironmentContext.Current.Environment <> Environments.Development Then
                    blnIsDev = False
                    For Each gvRow As GridViewRow In GridForm.Rows
                        If gvRow.Visible = True Then
                            SetGridControls(GridForm, False)
                            Exit For
                        End If
                    Next
                End If
                btnClearTables.Visible = (btnClearTables.Visible AndAlso blnIsDev)
                If blnIsDev AndAlso ((Authentication.CurrentUser.NetworkId = "OS0B6M") OrElse (Authentication.CurrentUser.NetworkId = "OS02JD") _
                    OrElse (Authentication.CurrentUser.NetworkId = "AI0549")) Then
                    ' Enable Clear
                    btnClearTables.Enabled = True
                Else
                    btnClearTables.Enabled = False
                End If

                btnNew.Visible = (btnNew.Visible AndAlso blnIsDev)
                btnCancel.Visible = (btnCancel.Visible AndAlso blnIsDev)
                btnSave.Visible = (btnSave.Visible AndAlso blnIsDev)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub
#End Region

#Region "Grid Related"
        Private Sub GridForm_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridForm.RowCommand
            Try
                'ignore other commands
                If e.CommandName = "SelectAction" OrElse e.CommandName = "DeleteAction" Then
                    Dim lblCtrl As Label
                    Dim row As GridViewRow = CType(CType(e.CommandSource, Control).Parent.Parent, GridViewRow)
                    Dim RowInd As Integer = row.RowIndex
                    lblCtrl = CType(GridForm.Rows(RowInd).Cells(GRID_COL_NEW_FORM_ID_IDX).FindControl(GRID_CTRL_NAME_NEW_FORM_ID), Label)
                    State.NewFormID = New Guid(lblCtrl.Text)
                    lblCtrl = Nothing
                    If e.CommandName = "SelectAction" Then
                        GridForm.EditIndex = RowInd
                        PopulateForms()
                        'Disable all Edit and Delete icon buttons on the Grid
                        SetGridControls(Me.GridForm, False)
                    ElseIf e.CommandName = "DeleteAction" Then
                        Dim intErrCode As Integer, strErrMsg As String
                        TabForm.DeleteNewForm(intErrCode, strErrMsg, State.NewFormID)
                        State.dvForms = Nothing
                        PopulateForms()
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub


        Private Sub GridForm_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridForm.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                Dim lblTemp As Label, ddl As DropDownList, txt As TextBox

                If (itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem) AndAlso e.Row.RowIndex <> -1 Then
                    With e.Row
                        If .RowIndex = GridForm.EditIndex Then

                            txt = CType(e.Row.FindControl(GRID_CTRL_NAME_FORM_CODE), TextBox)
                            If Not txt Is Nothing Then
                                If State.IsGridAddNew Then txt.Text = State.NewFormCode
                                txt = Nothing
                            End If

                            txt = CType(e.Row.FindControl(GRID_CTRL_NAME_ENGLISH), TextBox)
                            If Not txt Is Nothing Then
                                If State.IsGridAddNew Then txt.Text = State.NewFormEnglish
                                txt = Nothing
                            End If

                            txt = CType(e.Row.FindControl(GRID_CTRL_NAME_URL), TextBox)
                            If Not txt Is Nothing Then
                                If State.IsGridAddNew Then
                                    txt.Text = State.NewFormURL
                                End If
                                txt = Nothing
                            End If

                            ddl = CType(e.Row.FindControl(GRID_CTRL_NAME_NAV_ALLOWED), DropDownList)
                            If Not ddl Is Nothing Then
                                Try
                                    If State.IsGridAddNew Then
                                        Me.SetSelectedItem(ddl, State.NewFormNavAllowed)
                                    Else
                                        Me.SetSelectedItem(ddl, dvRow("NAV_ALWAYS_ALLOWED").ToString())
                                    End If
                                Catch ex As Exception
                                End Try
                                ddl = Nothing
                            End If

                            ddl = CType(e.Row.FindControl(GRID_CTRL_NAME_FORM_CATEGORY), DropDownList)
                            Dim strFormCatDDL As String = ""
                            If Not ddl Is Nothing Then
                                Try
                                    strFormCatDDL = ddl.ClientID
                                    Dim dv As DataView = FORMCATLIST
                                    If State.IsGridAddNew Then
                                        If State.NewFormTab = "" Then
                                            dv.RowFilter = "1=2"
                                            Me.BindDDLToDataView(ddl, dv, "Description", "Code", True)
                                            ddl.Enabled = False
                                        Else
                                            dv.RowFilter = "Tab_Code='" & State.NewFormTab & "'"
                                            Me.BindDDLToDataView(ddl, dv, "Description", "Code", True)
                                            Me.SetSelectedItem(ddl, State.NewFormFormCat)
                                        End If
                                    Else
                                        dv.RowFilter = "Tab_Code='" & dvRow("Tab").ToString() & "'"
                                        Me.BindDDLToDataView(ddl, dv, "Description", "Code", True)
                                        Me.SetSelectedItem(ddl, dvRow("FORM_CATEGORY_CODE").ToString())
                                    End If
                                    dv.RowFilter = ""
                                Catch ex As Exception
                                End Try
                                hiddenFormCatSelect.Value = ddl.SelectedValue
                                ddl.Attributes.Add("onchange", "SetFormCatSelection(document.getElementById('" & hiddenFormCatSelect.ClientID & "'));")
                                ddl = Nothing
                            End If

                            ddl = CType(e.Row.FindControl(GRID_CTRL_NAME_TAB), DropDownList)
                            If Not ddl Is Nothing Then
                                Try
                                    Me.BindDDLToDataView(ddl, TABLIST, "Tab_DESC", "Tab_Code", True)
                                    If State.IsGridAddNew Then
                                        Me.SetSelectedItem(ddl, State.NewFormTab)
                                    Else
                                        Me.SetSelectedItem(ddl, dvRow("Tab").ToString())
                                    End If
                                Catch ex As Exception
                                End Try
                                If Not State.IsGridAddNew Then ddl.Enabled = False
                                ddl.Attributes.Add("onchange", "SetFormCatList(document.getElementById('" & strFormCatDDL & "'));")
                                ddl = Nothing
                            End If

                            txt = CType(e.Row.FindControl(GRID_CTRL_NAME_QUERY_STRING), TextBox)
                            If Not txt Is Nothing Then
                                If State.IsGridAddNew Then txt.Text = State.QueryString
                                txt = Nothing
                            End If
                        End If
                    End With
                End If
                BaseItemBound(sender, e)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub
#End Region

#Region "Helper functions"
        Private Sub PopulateTabs()
            Try
                Dim isDVEmpty As Boolean = False
                Dim dv As DataView = Tabs
                If dv.Count = 0 Then
                    isDVEmpty = True
                    AddNewRowToDV(dv)
                End If
                GridTab.DataSource = dv
                GridTab.DataBind()
                If isDVEmpty Then
                    For Each gvRow As GridViewRow In GridTab.Rows
                        gvRow.Visible = False
                        gvRow.Controls.Clear()
                    Next
                End If
            Catch oEx As Exception
                ELPWebConstants.ShowTranslatedMessageAsPopup(APPOBJECTFORM002, ElitaPlusIdentity.Current.ActiveUser.LanguageId, Me.Page, oEx)
            End Try
        End Sub

        Private Sub PopulateForms()
            Try
                Dim isDVEmpty As Boolean = False
                Dim dv As DataView
                If State.dvForms Is Nothing Then State.dvForms = Forms
                dv = State.dvForms

                If dv.Count = 0 Then
                    isDVEmpty = True
                    AddNewRowToDV(dv, "New_Application_Form_Id")
                End If
                GridForm.DataSource = dv

                If (IsGridFormInEditMode OrElse State.IsGridAddNew) Then
                    State.GridFormEditIndex = FindSelectedRowIndexFromGuid(dv, State.NewFormID)
                    GridForm.EditIndex = State.GridFormEditIndex
                End If
                GridForm.DataBind()

                If isDVEmpty Then
                    For Each gvRow As GridViewRow In GridForm.Rows
                        gvRow.Visible = False
                        gvRow.Controls.Clear()
                    Next
                End If
            Catch oEx As Exception
                ELPWebConstants.ShowTranslatedMessageAsPopup(APPOBJECTFORM003, ElitaPlusIdentity.Current.ActiveUser.LanguageId, Me.Page, oEx)
            End Try

        End Sub

        Private Sub Import_Objects()
            Dim errMsg As New Collections.Generic.List(Of String)

            ' Hide The Statistic
            ShowAllStatistics(False)
            Try
                ' Load Tabs
                Proc.LoadTabs(ElitaPlusIdentity.Current.ActiveUser.NetworkId)
                Me.txtNewTabs.Text = Proc.outNew.ToString
                Me.txtAddedTabs.Text = Proc.Added.ToString
                ShowTabStatistics(True)
                ' Something went wrong
                If Proc.ReturnCode <> GOOD_RETURN Then
                    errMsg.Add(Proc.English)
                End If

                ' Load the Forms
                Proc.LoadForms(ElitaPlusIdentity.Current.ActiveUser.NetworkId)
                Me.txtNewForms.Text = Proc.outNew.ToString
                Me.txtAddedForms.Text = Proc.Added.ToString
                ShowFormStatistics(True)
                ' Something went wrong
                If Proc.ReturnCode <> GOOD_RETURN Then
                    errMsg.Add(Proc.English)
                End If
                If errMsg.Count > 0 Then '
                    Me.ErrControllerMaster.AddErrorAndShow(errMsg.ToArray, False)
                End If
            Catch oEx As Exception
                ELPWebConstants.ShowTranslatedMessageAsPopup(APPOBJECTFORM004, ElitaPlusIdentity.Current.ActiveUser.LanguageId, Me.Page, oEx)
            End Try
        End Sub

        Private Sub ShowAllStatistics(ByVal Show As Boolean)
            ShowFormStatistics(Show)
            ShowTabStatistics(Show)
        End Sub

        Private Sub ShowTabStatistics(ByVal Show As Boolean)
            lblNewTabs.Visible = Show
            txtNewTabs.Visible = Show
            lblAddedTabs.Visible = Show
            txtAddedTabs.Visible = Show
        End Sub
        Private Sub ShowFormStatistics(ByVal Show As Boolean)
            lblNewForms.Visible = Show
            txtNewForms.Visible = Show
            lblAddedForms.Visible = Show
            txtAddedForms.Visible = Show
        End Sub

        Private Sub WipeoutTables()
            ShowAllStatistics(False)
            Try
                ' Development Environment is the only Environment where this action is allowed
                If EnvironmentContext.Current.Environment = Environments.Development Then
                    ' Delete all Entries in New TAB table
                    Proc.ClearTabs()
                    ' Delete all Entries in New FORM table
                    Proc.ClearForms()
                End If
                PopulateTabs()
                PopulateForms()
                Me.btnImport.Enabled = False

            Catch oEx As Exception
                ELPWebConstants.ShowTranslatedMessageAsPopup(APPOBJECTFORM001, ElitaPlusIdentity.Current.ActiveUser.LanguageId, Me.Page, oEx)
            End Try
        End Sub

        Public Shared Function GetGuidStringFromByteArrayNullable(ByVal value As Byte()) As String
            If value Is Nothing Then
                Return Guid.Empty.ToString
            Else
                Return GetGuidStringFromByteArray(value)
            End If
        End Function

        Private Sub AddNewRowToDV(ByRef dv As DataView, Optional ByVal strID As String = "")
            Dim dt As DataTable, row As DataRow
            dt = dv.Table.Clone
            row = dt.NewRow
            If strID.Trim <> "" Then row(strID) = Guid.Empty.ToByteArray
            dt.Rows.Add(row)
            dv = dt.DefaultView
        End Sub

        Private Sub AddNewFormToDV(ByVal NewFormID As Guid)
            Dim dt As DataTable, row As DataRow
            dt = State.dvForms.Table
            row = dt.NewRow
            row("New_Application_Form_Id") = NewFormID.ToByteArray
            dt.Rows.Add(row)
        End Sub

        Private Sub BindDDLToDataView(ByVal lstControl As ListControl, ByVal Data As DataView, ByVal TextColumnName As String,
                                      ByVal ValueColumnName As String, Optional ByVal AddNothingSelected As Boolean = True)
            Dim i As Integer
            lstControl.Items.Clear()
            If AddNothingSelected Then
                lstControl.Items.Add(New ListItem("", ""))
            End If
            If Not Data Is Nothing Then
                For i = 0 To Data.Count - 1
                    lstControl.Items.Add(New ListItem(Data(i)(TextColumnName).ToString, Data(i)(ValueColumnName).ToString))
                Next
            End If
        End Sub

        Private Function PopulateBOFromForm(ByRef errMsg As Collections.Generic.List(Of String)) As Boolean
            Dim blnSuccess As Boolean = True, strTemp As String
            Dim ind As Integer = GridForm.EditIndex
            With Me.State
                Dim ddl As DropDownList = CType(GridForm.Rows(ind).Cells(GRID_COL_TAB_IDX).FindControl(GRID_CTRL_NAME_TAB), DropDownList)
                strTemp = ddl.SelectedValue
                If strTemp <> .NewFormTab Then .IsFormValueChanged = True
                .NewFormTab = strTemp
                If .NewFormTab = "" Then
                    blnSuccess = False
                    errMsg.Add(TranslationBase.TranslateLabelOrMessage("TAB") & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR))
                End If
                ddl = Nothing

                strTemp = ""
                strTemp = CType(GridForm.Rows(ind).Cells(GRID_COL_CODE_IDX).FindControl(GRID_CTRL_NAME_FORM_CODE), TextBox).Text.Trim.ToUpper()
                If strTemp <> .NewFormCode Then .IsFormValueChanged = True
                .NewFormCode = strTemp
                If .NewFormCode = String.Empty Then
                    blnSuccess = False
                    errMsg.Add(TranslationBase.TranslateLabelOrMessage("CODE") & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR))
                End If

                strTemp = ""
                strTemp = CType(GridForm.Rows(ind).Cells(GRID_COL_ENGLISH_IDX).FindControl(GRID_CTRL_NAME_ENGLISH), TextBox).Text.Trim
                If strTemp <> .NewFormEnglish Then .IsFormValueChanged = True
                .NewFormEnglish = strTemp
                If .NewFormEnglish = String.Empty Then
                    blnSuccess = False
                    errMsg.Add(TranslationBase.TranslateLabelOrMessage("ENGLISH") & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR))
                End If

                strTemp = ""
                strTemp = CType(GridForm.Rows(ind).Cells(GRID_COL_URL_IDX).FindControl(GRID_CTRL_NAME_URL), TextBox).Text.Trim
                If strTemp <> .NewFormURL Then .IsFormValueChanged = True
                .NewFormURL = strTemp
                If .NewFormURL = String.Empty Then
                    blnSuccess = False
                    errMsg.Add(TranslationBase.TranslateLabelOrMessage("RELATIVE_URL") & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR))
                End If

                ddl = CType(GridForm.Rows(ind).Cells(GRID_COL_NAV_ALLOWED_IDX).FindControl(GRID_CTRL_NAME_NAV_ALLOWED), DropDownList)
                strTemp = ddl.SelectedValue
                If strTemp <> .NewFormNavAllowed Then .IsFormValueChanged = True
                .NewFormNavAllowed = strTemp
                If .NewFormNavAllowed = "" Then
                    blnSuccess = False
                    errMsg.Add(TranslationBase.TranslateLabelOrMessage("NAV_ALWAYS_ALLOWED") & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR))
                End If
                ddl = Nothing

                strTemp = ""
                strTemp = CType(GridForm.Rows(ind).Cells(GRID_COL_QUERY_STRING_IDX).FindControl(GRID_CTRL_NAME_QUERY_STRING), TextBox).Text.Trim
                If strTemp <> .QueryString Then .IsFormValueChanged = True
                .QueryString = strTemp
                Dim oRegExValidator As New Regex(RegExConstants.QUERY_STRING_REGEX)
                If Not oRegExValidator.IsMatch(.QueryString) Then
                    blnSuccess = False
                    errMsg.Add(TranslationBase.TranslateLabelOrMessage("QUERY_STRING") & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_INVALID_VALUE_FORMAT))
                End If

                strTemp = hiddenFormCatSelect.Value
                If strTemp <> .NewFormFormCat Then .IsFormValueChanged = True
                .NewFormFormCat = strTemp

            End With
            Return blnSuccess
        End Function

        Private Sub ClearStateProperties()
            With State
                .NewFormID = Guid.Empty
                .NewFormCode = ""
                .NewFormEnglish = ""
                .NewFormFormCat = ""
                .NewFormTab = ""
                .NewFormNavAllowed = ""
                .NewFormTab = ""
                .NewFormURL = ""
                .QueryString = ""
            End With
        End Sub

        Private Sub SetControlState()
            If (IsGridFormInEditMode) Then
                ControlMgr.SetVisibleControl(Me, btnNew, False)
                ControlMgr.SetVisibleControl(Me, btnCancel, True)
                ControlMgr.SetVisibleControl(Me, btnSave, True)
                ControlMgr.SetVisibleControl(Me, btnImport, False)
                ControlMgr.SetVisibleControl(Me, btnClearTables, False)
                Me.MenuEnabled = False
                If litScriptArray.Text.Trim = String.Empty Then WriteFormCategoryArray()
                If State.IsGridAddNew Then
                    Dim objCtrl As WebControl = CType(GridForm.Rows(GridForm.EditIndex).Cells(GRID_COL_CODE_IDX).FindControl(GRID_CTRL_NAME_FORM_CODE), WebControl)
                    If Not objCtrl Is Nothing Then
                        objCtrl.Enabled = True
                    End If
                End If
            Else
                ControlMgr.SetVisibleControl(Me, btnNew, True)
                ControlMgr.SetVisibleControl(Me, btnCancel, False)
                ControlMgr.SetVisibleControl(Me, btnSave, False)
                ControlMgr.SetVisibleControl(Me, btnImport, True)
                ControlMgr.SetVisibleControl(Me, btnClearTables, True)
                Me.MenuEnabled = True
            End If
        End Sub

        Private Sub WriteFormCategoryArray()
            Dim dv As DataView = FORMCATLIST
            If dv.Count = 0 Then Exit Sub

            dv.Sort = "Tab_Code, DESCRIPTION"
            Dim sbArray As New System.Text.StringBuilder, strTab As String, intCount As Integer = 0, strTemp As String
            Dim sbTab As New System.Text.StringBuilder, sbCat As New System.Text.StringBuilder
            Dim dvCat As DataView
            sbTab.Append("var TabList = ['")

            For i As Integer = 0 To (dv.Count - 1)
                strTemp = dv.Item(i)("Tab_Code").ToString
                If strTemp <> strTab Then 'new tab
                    intCount += 1
                    strTab = strTemp
                    If intCount > 1 Then
                        sbTab.Append(",'")
                    End If
                    sbTab.Append(strTab)
                    sbTab.Append("'")

                    dvCat = dv
                    dvCat.RowFilter = "Tab_Code='" & strTab & "'"
                    sbCat.Append("var Item")
                    sbCat.Append(intCount.ToString)
                    sbCat.Append(" = ['")
                    For j As Integer = 0 To (dvCat.Count - 1)
                        If j > 0 Then
                            sbCat.Append(",'")
                        End If
                        sbCat.Append(dvCat(j)("code").ToString)
                        sbCat.Append("|")
                        sbCat.Append(dvCat(j)("DESCRIPTION").ToString)
                        sbCat.Append("'")
                    Next
                    sbCat.Append("];")
                    sbCat.Append(Environment.NewLine)
                End If
                dv.RowFilter = ""
            Next
            sbTab.Append("];")
            sbTab.Append(Environment.NewLine)

            sbArray.Append("<script language=""javascript"" type=""text/javascript"">")
            sbArray.Append(Environment.NewLine)
            sbArray.Append(sbTab.ToString)
            sbArray.Append(sbCat.ToString)
            sbArray.Append("var TabItems = [")
            For i As Integer = 1 To intCount
                If i > 1 Then sbArray.Append(",")
                sbArray.Append("Item")
                sbArray.Append(i.ToString)
            Next
            sbArray.Append("];")
            sbArray.Append(Environment.NewLine)
            sbArray.Append("</script>")
            Me.litScriptArray.Text = sbArray.ToString
        End Sub
#End Region

#Region "button event handlers"
        Private Sub btnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImport.Click
            Import_Objects()
        End Sub

        Private Sub btnClearTables_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearTables.Click
            Try
                WipeoutTables()
            Catch oex As Exception
                ELPWebConstants.ShowTranslatedMessageAsPopup(APPOBJECTFORM001, ElitaPlusIdentity.Current.ActiveUser.LanguageId, Me.Page, oex)
            End Try
        End Sub

        Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNew.Click
            Try
                State.IsGridVisible = True
                Me.State.NewFormID = Guid.NewGuid
                AddNewFormToDV(State.NewFormID)
                State.IsGridAddNew = True
                PopulateForms()
                'Disable all Edit and Delete icon buttons on the Grid
                SetGridControls(Me.GridForm, False)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
            Try
                Dim ErrMsg As New Collections.Generic.List(Of String)
                Dim intErrCode As Integer, strErrMsg As String
                If PopulateBOFromForm(ErrMsg) Then
                    With State
                        If (.IsFormValueChanged) Then
                            TabForm.SaveNewForm(intErrCode, strErrMsg, .NewFormID, .NewFormTab, .NewFormCode, .NewFormEnglish,
                                                 .NewFormURL, .NewFormNavAllowed, String.Empty, .NewFormFormCat, .QueryString)
                            If intErrCode = 0 Then
                                ClearStateProperties()
                                Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                                State.dvForms = Nothing
                            Else
                                ErrMsg.Add(strErrMsg)
                                Me.ErrControllerMaster.AddErrorAndShow(ErrMsg.ToArray, False)
                            End If

                        Else
                            Me.AddInfoMsg(Message.MSG_RECORD_NOT_SAVED)
                        End If
                        GridForm.EditIndex = Me.NO_ITEM_SELECTED_INDEX
                        .IsGridAddNew = False
                    End With
                Else
                    Me.ErrControllerMaster.AddErrorAndShow(ErrMsg.ToArray, False)
                End If
                Me.PopulateForms()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancel.Click
            Try
                With State
                    If .IsGridAddNew Then
                        .dvForms.Delete(.GridFormEditIndex)
                    End If
                    ClearStateProperties()
                    .IsGridAddNew = False
                End With
                GridForm.EditIndex = NO_ITEM_SELECTED_INDEX
                PopulateForms()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub
#End Region




    End Class


End Namespace

