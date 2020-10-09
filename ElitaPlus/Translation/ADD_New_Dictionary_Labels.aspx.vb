
Imports Assurant.Assurnet.Services

Namespace Translation
    Partial Class ADD_New_Dictionary_Labels
        Inherits ElitaPlusSearchPage

        '  Private CFG_ENVIRONMENT As String = ConfigurationMgr.ConfigValue("Environment")
       
#Region "Page Return Type"
        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public EditingBo As NewDictionaryItem
            Public HasDataChanged As Boolean

            Public Sub New(LastOp As DetailPageCommand, curEditingBo As NewDictionaryItem, hasDataChanged As Boolean)
                LastOperation = LastOp
                EditingBo = curEditingBo
                Me.HasDataChanged = hasDataChanged
            End Sub
        End Class
#End Region
#Region "Page State"
        Private IsReturningFromChild As Boolean = False

        Class MyState
            Public Mybo As NewDictionaryItem
            Public MyMSGCodeBO As MessageCode
            Public PageIndex As Integer = 0
            Public SortExpression As String = NewDictionaryItem.NewDictItemSearchDV.COL_NAME_UI_PROG_CODE
            Public SelectedEXDNId As Guid = Guid.Empty
            Public IsGridVisible As Boolean
            Public SearchCode As String
            Public SearchDescription As String
            Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
            Public searchDV As DataView = Nothing
            Public HasDataChanged As Boolean
            Public LastErrMsg As String
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public isNew As Boolean = False
            Public isDirty As Boolean = False
            Public oLabelCount As Integer = 0
            Public isTestEnvironment As Boolean = False

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

#Region "Constants"
        Public Const PAGETITLE As String = "ADD_New_Dictionary_Labels"
        Public Const PAGETAB As String = "ADMIN"

        Public Const GRID_COL_DELETE_TRANSLATION As Integer = 0
        Public Const GRID_COL_ID As Integer = 1
        Public Const GRID_COL_UI_PROG_CODE As Integer = 2
        Public Const GRID_COL_ENG_TRANSLATION As Integer = 3
        Public Const GRID_COL_MSG_CODE As Integer = 4
        Public Const GRID_COL_MSG_TYPE As Integer = 5
        Public Const GRID_COL_MSG_PARAM_CNT As Integer = 6
        Public Const GRID_COL_CREATED_DATE As Integer = 7
        Public Const GRID_COL_MODIFIED_DATE As Integer = 8
        Public Const GRID_COL_CREATED_BY As Integer = 9
        Public Const GRID_COL_MODIFIED_BY As Integer = 10
        Public Const GRID_COL_IMPORTED As Integer = 11
        Public Const GRID_COL_DICT_ITEM_ID As Integer = 12

        Public Const UI_PROG_CODE_CONTROL_NAME As String = "moUiProgCode"
        Public Const ENG_TRANSLATION_CONTROL_NAME As String = "moTranslation"
        Public Const CREATED_DATE_CONTROL_NAME As String = "moCreatedDate"
        Public Const MODIFIED_DATE_CONTROL_NAME As String = "moCreateDate"
        Public Const CREATED_BY_CONTROL_NAME As String = "moCreatedBy"
        Public Const MODIFIED_BY_CONTROL_NAME As String = "moModifiedBy"
        Public Const IMPORTED_CONTROL_NAME As String = "moImported"
        Public Const GRID_CONTROL_MSG_PARAM_COUNT As String = "ddlMSGParamCnt"
        Public Const GRID_CONTROL_MSG_TYPE As String = "ddlMSGType"
        Public Const GRID_CONTROL_MSG_CODE As String = "txtMSGCODE"

        Public Const MSG_CHANGE_NOT_ALLOWED As String = "CHANGE_NOT_ALLOWED"
#End Region

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

#Region "Handlers-Init"

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

            'Put user code to initialize the page here
            ''Me.ErrControllerMaster.Clear_Hide()
            MasterPage.MessageController.Clear()
            If Not Page.IsPostBack Then
                ''Def-25721:Added UpdateBreadCrum to display page header
                UpdateBreadCrum()
                MenuEnabled = False
                If EnvironmentContext.Current.Environment = Environments.Development OrElse EnvironmentContext.Current.Environment <> Environments.Test Then
                    State.isTestEnvironment = True
                End If
                If Not State.isTestEnvironment Then
                    btnWipeout.Visible = False
                End If
                SetFormTitle(PAGETITLE)
                SetFormTab(PAGETAB)
                If ((State.searchDV Is Nothing) OrElse (State.HasDataChanged)) Then
                    State.Mybo = New NewDictionaryItem
                End If
                PopulateGrid()

                If (ElitaPlusIdentity.Current.ActiveUser.NetworkId = "OS0807") OrElse _
                (ElitaPlusIdentity.Current.ActiveUser.NetworkId = "OS02JD") OrElse _
                (ElitaPlusIdentity.Current.ActiveUser.NetworkId = "AI0549") Then
                    ControlMgr.SetEnableControl(Me, btnWipeout, True)
                Else
                    ControlMgr.SetEnableControl(Me, btnWipeout, False)
                End If
            Else
                CheckIfComingFromSaveConfirm()
            End If
        End Sub

#End Region

#Region "Controlling Logic"

        Private Function PopulateBOFromForm(index As Integer, ByRef errMsg As Collections.Generic.List(Of String)) As Boolean

            Dim hasErr As Boolean = False, strUICode As String, strEngText As String


            grdLabels.EditItemIndex = index
            Dim oguid As String = "00000000-0000-0000-0000-000000000001"

            With State.Mybo
                strUICode = CType(grdLabels.Items(index).Cells(GRID_COL_UI_PROG_CODE).FindControl(UI_PROG_CODE_CONTROL_NAME), TextBox).Text.ToUpper
                strEngText = CType(grdLabels.Items(index).Cells(GRID_COL_ENG_TRANSLATION).FindControl(ENG_TRANSLATION_CONTROL_NAME), TextBox).Text.Replace("_", " ")


                Dim strMSGCode As String, strMsgType As String, strTemp As String, lngTemp As Long
                strMSGCode = CType(grdLabels.Items(index).Cells(GRID_COL_MSG_CODE).FindControl(GRID_CONTROL_MSG_CODE), TextBox).Text.Trim.ToUpper
                If strMSGCode <> String.Empty Then 'New message code
                    strMsgType = CType(grdLabels.Items(index).Cells(GRID_COL_MSG_TYPE).FindControl(GRID_CONTROL_MSG_TYPE), DropDownList).SelectedValue.Trim
                    strTemp = CType(grdLabels.Items(index).Cells(GRID_COL_MSG_PARAM_CNT).FindControl(GRID_CONTROL_MSG_PARAM_COUNT), DropDownList).SelectedValue.Trim

                    If .IsNew OrElse .Imported = "N" Then
                        PopulateBOProperty(State.Mybo, "UiProgCode", strUICode)
                        PopulateBOProperty(State.Mybo, "EnglishTranslation", strEngText)
                        PopulateBOProperty(State.Mybo, "MsgCode", strMSGCode)
                        If Long.TryParse(strTemp, lngTemp) Then
                            .MsgParameterCount = lngTemp
                        End If
                        If strMsgType = String.Empty Then
                            hasErr = True
                            errMsg.Add(grdLabels.Columns(GRID_COL_MSG_TYPE).HeaderText & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR))
                        Else
                            PopulateBOProperty(State.Mybo, "MsgType", strMsgType)
                        End If
                    Else 'No change allowed if 
                        If .MsgCode <> strMSGCode OrElse .MsgType <> strMsgType Then
                            hasErr = True
                            errMsg.Add(TranslationBase.TranslateLabelOrMessage("MSG_CODE") & " " & .MsgCode & ":" & TranslationBase.TranslateLabelOrMessage(MSG_CHANGE_NOT_ALLOWED))
                        End If
                    End If
                ElseIf .MsgCode <> String.Empty Then 'Existing MSG code is erased
                    hasErr = True
                    errMsg.Add(TranslationBase.TranslateLabelOrMessage("MSG_CODE") & " " & .MsgCode & ":" & TranslationBase.TranslateLabelOrMessage(MSG_CHANGE_NOT_ALLOWED))
                Else
                    PopulateBOProperty(State.Mybo, "UiProgCode", strUICode)
                    PopulateBOProperty(State.Mybo, "EnglishTranslation", strEngText)
                    If .MsgCode <> String.Empty Then .MsgCode = String.Empty
                    If .MsgType <> String.Empty Then .MsgType = String.Empty
                    If .MsgParameterCount IsNot Nothing AndAlso .MsgParameterCount <> 0 Then .MsgParameterCount = 0
                End If
                If .IsNew AndAlso .IsDirty Then
                    PopulateBOProperty(State.Mybo, "DictItemId", New Guid(oguid))
                    PopulateBOProperty(State.Mybo, "Imported", "N")
                End If

            End With

            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If

            If State.Mybo.IsDirty Then
                State.isDirty = True
            End If

            Return Not hasErr
        End Function
        Protected Sub CheckIfComingFromSaveConfirm()
            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
            If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        State.Mybo.Delete()
                        State.Mybo.Save()
                        DisplayMessage(Message.DELETE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                    Case ElitaPlusPage.DetailPageCommand.Back
                        SaveNewDictItem()
                        ReturnToTabHomePage()
                    Case ElitaPlusPage.DetailPageCommand.Accept
                        SaveNewDictItem()
                        Import()
                    Case ElitaPlusPage.DetailPageCommand.New_
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        ReturnToTabHomePage()
                End Select
            ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        'Me.DisplayMessage(Message.DELETE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ReturnToTabHomePage()
                    Case ElitaPlusPage.DetailPageCommand.New_
                        'Me.CreateNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        'Me.CreateNewWithCopy()
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        'Me.ReturnToTabHomePage() '(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.Mybo, Me.State.HasDataChanged))

                End Select
            End If
            'Clean after consuming the action
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            HiddenSaveChangesPromptResponse.Value = ""
        End Sub

        Public Sub PopulateGrid()
            Dim i As Integer
            'Dim edt As ImageButton
            Dim del As ImageButton
            Dim txtCtl As TextBox
            Dim oVisible As Boolean = False
            Dim oImpotedCtl As String

            If (State.searchDV Is Nothing) Then 'OrElse (Me.State.HasDataChanged)) Then
                'Me.State.Mybo = New NewDictionaryItem
                State.searchDV = State.Mybo.getList()
            End If

            'Me.State.searchDV.Sort = Me.State.SortExpression

            grdLabels.AutoGenerateColumns = False
            SetPageAndSelectedIndexFromGuid(State.searchDV, State.SelectedEXDNId, grdLabels, State.PageIndex)
            SortAndBindGrid()

            If grdLabels.Items.Count < 1 Then
                ControlMgr.SetVisibleControl(Me, btnAppend, False)
                ControlMgr.SetVisibleControl(Me, moBtnSave_WRITE, False)
                ControlMgr.SetVisibleControl(Me, moBtnCancel, False)
            Else
                ControlMgr.SetVisibleControl(Me, btnAppend, True)
                ControlMgr.SetVisibleControl(Me, moBtnSave_WRITE, True)
                ControlMgr.SetVisibleControl(Me, moBtnCancel, True)
            End If

            If Not State.isTestEnvironment Then
                oVisible = True
                ControlMgr.SetVisibleControl(Me, moBtnSave_WRITE, False)
                ControlMgr.SetVisibleControl(Me, moBtnCancel, False)
                ControlMgr.SetVisibleControl(Me, btnNew_WRITE, False)
            End If

            grdLabels.Columns(GRID_COL_CREATED_DATE).Visible = oVisible
            grdLabels.Columns(GRID_COL_MODIFIED_DATE).Visible = oVisible
            grdLabels.Columns(GRID_COL_CREATED_BY).Visible = oVisible
            grdLabels.Columns(GRID_COL_MODIFIED_BY).Visible = oVisible

            ControlMgr.SetEnableControl(Me, btnAppend, False)

            For i = 0 To (grdLabels.Items.Count - 1)
                grdLabels.SelectedIndex = i
                oImpotedCtl = grdLabels.Items(i).Cells(GRID_COL_IMPORTED).Text
                del = CType(grdLabels.Items(i).Cells(GRID_COL_DELETE_TRANSLATION).FindControl(DELETE_CONTROL_NAME), ImageButton)
                If oVisible Then
                    ControlMgr.SetVisibleControl(Me, del, False)
                    ControlMgr.SetEnableControl(Me, btnAppend, True)
                    ControlMgr.SetEnableControl(Me, grdLabels, False)
                ElseIf State.isNew Then
                    txtCtl = CType(grdLabels.Items(i).Cells(GRID_COL_UI_PROG_CODE).FindControl(UI_PROG_CODE_CONTROL_NAME), TextBox)
                    del = CType(grdLabels.Items(i).Cells(GRID_COL_DELETE_TRANSLATION).FindControl(DELETE_CONTROL_NAME), ImageButton)
                    ControlMgr.SetVisibleControl(Me, del, False)
                    If txtCtl IsNot Nothing AndAlso Not oImpotedCtl = "" Then
                        ControlMgr.SetEnableControl(Me, txtCtl, False)
                    End If
                    txtCtl = CType(grdLabels.Items(i).Cells(GRID_COL_ENG_TRANSLATION).FindControl(ENG_TRANSLATION_CONTROL_NAME), TextBox)
                    If txtCtl IsNot Nothing AndAlso Not oImpotedCtl = "" Then
                        ControlMgr.SetEnableControl(Me, txtCtl, False)
                    End If
                Else
                    If oImpotedCtl = "" Then
                        If del IsNot Nothing Then
                            ControlMgr.SetVisibleControl(Me, del, False)
                        End If
                    Else
                        ControlMgr.SetEnableControl(Me, btnAppend, True)
                        ControlMgr.SetVisibleControl(Me, del, True)
                    End If
                End If
            Next


        End Sub
        Private Sub SortAndBindGrid()
            State.PageIndex = grdLabels.CurrentPageIndex
            grdLabels.DataSource = State.searchDV
            HighLightSortColumn(grdLabels, State.SortExpression)
            grdLabels.DataBind()
        End Sub

#End Region

#Region " Datagrid Related "

        Public Sub ItemCommand(source As System.Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs)
            Try
                State.Mybo = New NewDictionaryItem(New Guid(e.Item.Cells(GRID_COL_ID).Text))
                If e.CommandName = "DeleteRecord" Then
                    With State.Mybo
                        If .MsgCode <> String.Empty Then
                            Dim MSGCodeID As Guid = MessageCode.GetMsgIdFromMsgCode(.MsgCode, .MsgType)
                            If MSGCodeID <> Guid.Empty Then
                                Dim objMSGCode As MessageCode = New MessageCode(MSGCodeID)
                                objMSGCode.Delete()
                                objMSGCode.Save()
                            End If
                        End If
                    End With
                    State.Mybo.RemoveLabels(State.Mybo.DictItemId)
                    State.Mybo.Delete()
                    State.Mybo.Save()
                    State.isNew = False
                    State.isDirty = False
                    State.searchDV = Nothing
                    PopulateGrid()
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                'Me.HandleErrors(ex, Me.ErrControllerMaster)
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Public Sub ItemCreated(sender As System.Object, e As System.Web.UI.WebControls.DataGridItemEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub grdLabels_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdLabels.ItemDataBound
            Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

            If (dvRow IsNot Nothing) AndAlso dvRow.Row.RowState = DataRowState.Added Then
                grdLabels.EditItemIndex = e.Item.ItemIndex
            End If
            If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                Try
                    Dim ddl As DropDownList = CType(e.Item.Cells(GRID_COL_MSG_TYPE).FindControl(GRID_CONTROL_MSG_TYPE), DropDownList)
                    If ddl IsNot Nothing Then
                        Dim dvList As DataView = LookupListNew.DropdownLookupList(LookupListNew.LK_MSG_TYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
                        Dim i As Integer
                        ddl.Items.Clear()
                        ddl.Items.Add(New ListItem("", ""))
                        If dvList IsNot Nothing Then
                            For i = 0 To dvList.Count - 1
                                ddl.Items.Add(New ListItem(dvList(i)("DESCRIPTION").ToString, dvList(i)("CODE").ToString))
                            Next
                        End If
                        ddl.SelectedValue = dvRow(NewDictionaryItem.NewDictItemSearchDV.COL_NAME_MSG_TYPE).ToString
                    End If

                    ddl = CType(e.Item.Cells(GRID_COL_MSG_PARAM_CNT).FindControl(GRID_CONTROL_MSG_PARAM_COUNT), DropDownList)
                    If ddl IsNot Nothing Then
                        'PopulateControlFromBOProperty(ddl, dvRow(NewDictionaryItem.NewDictItemSearchDV.COL_NAME_MSG_PARAM_COUNT))
                        ddl.SelectedValue = dvRow(NewDictionaryItem.NewDictItemSearchDV.COL_NAME_MSG_PARAM_COUNT).ToString
                    End If
                Catch ex As Exception
                End Try

                PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_ID), dvRow(NewDictionaryItem.NewDictItemSearchDV.COL_NAME_NEW_DICT_ITEM_ID))
                PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_IMPORTED), dvRow(NewDictionaryItem.NewDictItemSearchDV.COL_NAME_IMPORTED))
            End If
        End Sub

        Private Sub Grid_SortCommand(source As System.Object, e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles grdLabels.SortCommand
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
                State.PageIndex = 0
                PopulateGrid()
            Catch ex As Exception
                ''Me.HandleErrors(ex, Me.ErrControllerMaster)
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Import()
            Dim oImpotedCtl As String, labelID As Guid

            For i As Integer = 0 To (grdLabels.Items.Count - 1)
                Try
                    grdLabels.SelectedIndex = i
                    oImpotedCtl = grdLabels.Items(i).Cells(GRID_COL_IMPORTED).Text
                    If Not oImpotedCtl = "" Then
                        State.Mybo = New NewDictionaryItem(New Guid(grdLabels.Items(i).Cells(GRID_COL_ID).Text))

                        If oImpotedCtl = "N" Then
                            State.Mybo.GenerateNewLabels(labelID)
                            If (State.Mybo.IsFamilyDirty) Then
                                State.Mybo.Save()
                                'load the message code if any
                                If State.Mybo.MsgCode <> String.Empty Then
                                    Dim objMsgCode As New MessageCode
                                    With objMsgCode
                                        .MsgCode = State.Mybo.MsgCode
                                        .MsgParameterCount = State.Mybo.MsgParameterCount
                                        .MsgType = LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_MSG_TYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId), State.Mybo.MsgType)
                                        .LabelId = labelID
                                        .Save()
                                    End With
                                End If
                                State.oLabelCount += 1
                            End If
                        ElseIf oImpotedCtl = "Y" Then
                            State.Mybo.ChangeLabels(State.Mybo.DictItemId)
                            If (State.Mybo.IsFamilyDirty) Then
                                State.Mybo.Save()
                            End If
                        End If
                    End If
                Catch ex As Exception
                End Try
            Next

            ControlMgr.SetVisibleControl(Me, pnlLabelCount, True)
            'End If
            PopulateControlFromBOProperty(txtMessageCount, State.oLabelCount)
            State.oLabelCount = 0
            State.searchDV = NewDictionaryItem.getList()
            PopulateGrid()

        End Sub

        Private Function IsGridDirty() As Boolean
            Dim errMsg As Collections.Generic.List(Of String) = New Collections.Generic.List(Of String)
            Dim oImpotedCtl As String
            State.isDirty = False

            For i As Integer = 0 To (grdLabels.Items.Count - 1)
                grdLabels.SelectedIndex = i
                oImpotedCtl = grdLabels.Items(i).Cells(GRID_COL_IMPORTED).Text
                If oImpotedCtl IsNot Nothing Then
                    Select Case oImpotedCtl
                        Case ""
                            State.Mybo = New NewDictionaryItem
                        Case Else
                            State.Mybo = New NewDictionaryItem(New Guid(grdLabels.Items(i).Cells(GRID_COL_ID).Text))
                    End Select
                End If
                PopulateBOFromForm(i, errMsg)
            Next

            Return State.isDirty

        End Function

        Public Sub SaveNewDictItem()
            Dim oError As Boolean = False
            Dim i As Integer
            Dim txtCtl As TextBox
            Dim oImpotedCtl As String
            Dim errMsg As Collections.Generic.List(Of String) = New Collections.Generic.List(Of String)
            Dim popHasErr As Boolean = False

            For i = 0 To (grdLabels.Items.Count - 1)
                Try
                    grdLabels.EditItemIndex = i
                    oImpotedCtl = grdLabels.Items(i).Cells(GRID_COL_IMPORTED).Text
                    If oImpotedCtl IsNot Nothing Then
                        Select Case oImpotedCtl
                            Case ""
                                State.Mybo = New NewDictionaryItem
                            Case Else
                                State.Mybo = New NewDictionaryItem(New Guid(grdLabels.Items(i).Cells(GRID_COL_ID).Text))
                        End Select
                        If PopulateBOFromForm(i, errMsg) Then
                            If State.Mybo.IsDirty Then
                                State.Mybo.Save()
                                State.HasDataChanged = True
                            End If
                        Else
                            popHasErr = True
                        End If
                    End If
                Catch ex As Exception
                    oError = True
                    ''Me.HandleErrors(ex, Me.ErrControllerMaster)
                    HandleErrors(ex, MasterPage.MessageController)
                End Try
            Next

            If popHasErr Then
                'Me.ErrControllerMaster.AddErrorAndShow(errMsg.ToArray, False)
                MasterPage.MessageController.AddErrorAndShow(errMsg.ToArray, False)
            End If

            If Not oError Then
                State.isNew = False
                State.isDirty = False
                State.searchDV = Nothing
                ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)
            End If
            oError = False
        End Sub
        ''Def-25721: Added function to display page label
        Private Sub UpdateBreadCrum()
            If (State IsNot Nothing) Then
                If (State IsNot Nothing) Then
                    MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator &
                        TranslationBase.TranslateLabelOrMessage("Add New Dictionary Labels")
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Add New Dictionary Labels")
                End If
            End If
        End Sub
#End Region

#Region "Button Management"

        Private Sub btnNew_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnNew_WRITE.Click
            Try
                State.Mybo = New NewDictionaryItem
                State.searchDV = State.Mybo.GetNewDataViewRow(State.searchDV, State.Mybo)
                State.isNew = True
                ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
                PopulateGrid()
            Catch ex As Exception
                ''Me.HandleErrors(ex, Me.ErrControllerMaster)
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            
        End Sub

        Private Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click

            Dim oImpotedCtl As String
            State.isDirty = False

            Try
                If IsGridDirty() Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    ReturnToTabHomePage() '(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.Mybo, Me.State.HasDataChanged))
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                ''Me.HandleErrors(ex, Me.ErrControllerMaster)
                HandleErrors(ex, MasterPage.MessageController)
                DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                ''Me.State.LastErrMsg = Me.ErrControllerMaster.Text
                State.LastErrMsg = MasterPage.MessageController.Text
            End Try
        End Sub

        Private Sub moBtnCancel_Click(sender As Object, e As System.EventArgs) Handles moBtnCancel.Click
            Try
                State.isNew = False
                State.isDirty = False
                State.searchDV = Nothing
                ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)
                PopulateGrid()
            Catch ex As Exception
                ''Me.HandleErrors(ex, Me.ErrControllerMaster)
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moBtnSave_WRITE_Click(sender As Object, e As System.EventArgs) Handles moBtnSave_WRITE.Click
            Try
                If State.isNew Then
                    Dim errMsg As Collections.Generic.List(Of String) = New Collections.Generic.List(Of String)
                    If PopulateBOFromForm(grdLabels.EditItemIndex, errMsg) Then
                        If (State.Mybo.IsDirty) Then
                            If State.Mybo.MsgCode = String.Empty OrElse MessageCode.IsNewMsgCode(State.Mybo.MsgCode, State.Mybo.MsgType, State.Mybo.UiProgCode) Then
                                State.Mybo.Save()
                                State.searchDV = Nothing
                                State.isDirty = False
                                State.isNew = False
                                PopulateGrid()
                                ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)
                            Else
                                DisplayMessage(MessageCode.Err_MSG_CODE_EXISTS, "", MSG_BTN_OK, MSG_TYPE_INFO)
                            End If
                        End If
                    Else
                        '' Me.ErrControllerMaster.AddErrorAndShow(errMsg.ToArray, False)
                        MasterPage.MessageController.AddErrorAndShow(errMsg.ToArray, False)

                    End If
                Else
                    SaveNewDictItem()
                    PopulateGrid()
                End If
            Catch ex As Exception
                PopulateGrid()
                ''Me.HandleErrors(ex, Me.ErrControllerMaster)
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub btnAppend_Click(sender As Object, e As System.EventArgs) Handles btnAppend.Click
            If IsGridDirty() Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Accept
            Else
                Import()
            End If

        End Sub

        Private Sub btnWipeout_Click(sender As Object, e As System.EventArgs) Handles btnWipeout.Click
            Try
                State.Mybo.DeleteAll()
                State.searchDV = NewDictionaryItem.getList()
                PopulateGrid()
            Catch ex As Exception
                ''Me.HandleErrors(ex, Me.ErrControllerMaster)
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region
    End Class

End Namespace
