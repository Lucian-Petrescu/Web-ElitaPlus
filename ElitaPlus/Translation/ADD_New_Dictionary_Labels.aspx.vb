
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

            Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As NewDictionaryItem, ByVal hasDataChanged As Boolean)
                Me.LastOperation = LastOp
                Me.EditingBo = curEditingBo
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

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Handlers-Init"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            'Put user code to initialize the page here
            ''Me.ErrControllerMaster.Clear_Hide()
            Me.MasterPage.MessageController.Clear()
            If Not Page.IsPostBack Then
                ''Def-25721:Added UpdateBreadCrum to display page header
                UpdateBreadCrum()
                Me.MenuEnabled = False
                If EnvironmentContext.Current.Environment = Environments.Development OrElse EnvironmentContext.Current.Environment <> Environments.Test Then
                    Me.State.isTestEnvironment = True
                End If
                If Not Me.State.isTestEnvironment Then
                    Me.btnWipeout.Visible = False
                End If
                Me.SetFormTitle(PAGETITLE)
                Me.SetFormTab(PAGETAB)
                If ((Me.State.searchDV Is Nothing) OrElse (Me.State.HasDataChanged)) Then
                    Me.State.Mybo = New NewDictionaryItem
                End If
                Me.PopulateGrid()

                If (ElitaPlusIdentity.Current.ActiveUser.NetworkId = "OS0807") OrElse _
                (ElitaPlusIdentity.Current.ActiveUser.NetworkId = "OS02JD") OrElse _
                (ElitaPlusIdentity.Current.ActiveUser.NetworkId = "AI0549") Then
                    ControlMgr.SetEnableControl(Me, Me.btnWipeout, True)
                Else
                    ControlMgr.SetEnableControl(Me, Me.btnWipeout, False)
                End If
            Else
                CheckIfComingFromSaveConfirm()
            End If
        End Sub

#End Region

#Region "Controlling Logic"

        Private Function PopulateBOFromForm(ByVal index As Integer, ByRef errMsg As Collections.Generic.List(Of String)) As Boolean

            Dim hasErr As Boolean = False, strUICode As String, strEngText As String


            grdLabels.EditItemIndex = index
            Dim oguid As String = "00000000-0000-0000-0000-000000000001"

            With Me.State.Mybo
                strUICode = CType(grdLabels.Items(index).Cells(Me.GRID_COL_UI_PROG_CODE).FindControl(Me.UI_PROG_CODE_CONTROL_NAME), TextBox).Text.ToUpper
                strEngText = CType(grdLabels.Items(index).Cells(Me.GRID_COL_ENG_TRANSLATION).FindControl(Me.ENG_TRANSLATION_CONTROL_NAME), TextBox).Text.Replace("_", " ")


                Dim strMSGCode As String, strMsgType As String, strTemp As String, lngTemp As Long
                strMSGCode = CType(grdLabels.Items(index).Cells(Me.GRID_COL_MSG_CODE).FindControl(Me.GRID_CONTROL_MSG_CODE), TextBox).Text.Trim.ToUpper
                If strMSGCode <> String.Empty Then 'New message code
                    strMsgType = CType(grdLabels.Items(index).Cells(Me.GRID_COL_MSG_TYPE).FindControl(Me.GRID_CONTROL_MSG_TYPE), DropDownList).SelectedValue.Trim
                    strTemp = CType(grdLabels.Items(index).Cells(Me.GRID_COL_MSG_PARAM_CNT).FindControl(Me.GRID_CONTROL_MSG_PARAM_COUNT), DropDownList).SelectedValue.Trim

                    If .IsNew OrElse .Imported = "N" Then
                        Me.PopulateBOProperty(Me.State.Mybo, "UiProgCode", strUICode)
                        Me.PopulateBOProperty(Me.State.Mybo, "EnglishTranslation", strEngText)
                        Me.PopulateBOProperty(State.Mybo, "MsgCode", strMSGCode)
                        If Long.TryParse(strTemp, lngTemp) Then
                            .MsgParameterCount = lngTemp
                        End If
                        If strMsgType = String.Empty Then
                            hasErr = True
                            errMsg.Add(grdLabels.Columns(Me.GRID_COL_MSG_TYPE).HeaderText & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR))
                        Else
                            Me.PopulateBOProperty(Me.State.Mybo, "MsgType", strMsgType)
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
                    Me.PopulateBOProperty(Me.State.Mybo, "UiProgCode", strUICode)
                    Me.PopulateBOProperty(Me.State.Mybo, "EnglishTranslation", strEngText)
                    If .MsgCode <> String.Empty Then .MsgCode = String.Empty
                    If .MsgType <> String.Empty Then .MsgType = String.Empty
                    If Not .MsgParameterCount Is Nothing And .MsgParameterCount <> 0 Then .MsgParameterCount = 0
                End If
                If .IsNew AndAlso .IsDirty Then
                    Me.PopulateBOProperty(Me.State.Mybo, "DictItemId", New Guid(oguid))
                    Me.PopulateBOProperty(Me.State.Mybo, "Imported", "N")
                End If

            End With

            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If

            If Me.State.Mybo.IsDirty Then
                Me.State.isDirty = True
            End If

            Return Not hasErr
        End Function
        Protected Sub CheckIfComingFromSaveConfirm()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
            If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        Me.State.Mybo.Delete()
                        Me.State.Mybo.Save()
                        Me.DisplayMessage(Message.DELETE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    Case ElitaPlusPage.DetailPageCommand.Back
                        SaveNewDictItem()
                        Me.ReturnToTabHomePage()
                    Case ElitaPlusPage.DetailPageCommand.Accept
                        SaveNewDictItem()
                        Import()
                    Case ElitaPlusPage.DetailPageCommand.New_
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        Me.ReturnToTabHomePage()
                End Select
            ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        'Me.DisplayMessage(Message.DELETE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    Case ElitaPlusPage.DetailPageCommand.Back
                        Me.ReturnToTabHomePage()
                    Case ElitaPlusPage.DetailPageCommand.New_
                        'Me.CreateNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        'Me.CreateNewWithCopy()
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        'Me.ReturnToTabHomePage() '(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.Mybo, Me.State.HasDataChanged))

                End Select
            End If
            'Clean after consuming the action
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            Me.HiddenSaveChangesPromptResponse.Value = ""
        End Sub

        Public Sub PopulateGrid()
            Dim i As Integer
            'Dim edt As ImageButton
            Dim del As ImageButton
            Dim txtCtl As TextBox
            Dim oVisible As Boolean = False
            Dim oImpotedCtl As String

            If (Me.State.searchDV Is Nothing) Then 'OrElse (Me.State.HasDataChanged)) Then
                'Me.State.Mybo = New NewDictionaryItem
                Me.State.searchDV = Me.State.Mybo.getList()
            End If

            'Me.State.searchDV.Sort = Me.State.SortExpression

            Me.grdLabels.AutoGenerateColumns = False
            SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.SelectedEXDNId, Me.grdLabels, Me.State.PageIndex)
            Me.SortAndBindGrid()

            If grdLabels.Items.Count < 1 Then
                ControlMgr.SetVisibleControl(Me, Me.btnAppend, False)
                ControlMgr.SetVisibleControl(Me, Me.moBtnSave_WRITE, False)
                ControlMgr.SetVisibleControl(Me, Me.moBtnCancel, False)
            Else
                ControlMgr.SetVisibleControl(Me, Me.btnAppend, True)
                ControlMgr.SetVisibleControl(Me, Me.moBtnSave_WRITE, True)
                ControlMgr.SetVisibleControl(Me, Me.moBtnCancel, True)
            End If

            If Not Me.State.isTestEnvironment Then
                oVisible = True
                ControlMgr.SetVisibleControl(Me, Me.moBtnSave_WRITE, False)
                ControlMgr.SetVisibleControl(Me, Me.moBtnCancel, False)
                ControlMgr.SetVisibleControl(Me, Me.btnNew_WRITE, False)
            End If

            grdLabels.Columns(GRID_COL_CREATED_DATE).Visible = oVisible
            grdLabels.Columns(GRID_COL_MODIFIED_DATE).Visible = oVisible
            grdLabels.Columns(GRID_COL_CREATED_BY).Visible = oVisible
            grdLabels.Columns(GRID_COL_MODIFIED_BY).Visible = oVisible

            ControlMgr.SetEnableControl(Me, Me.btnAppend, False)

            For i = 0 To (grdLabels.Items.Count - 1)
                grdLabels.SelectedIndex = i
                oImpotedCtl = grdLabels.Items(i).Cells(Me.GRID_COL_IMPORTED).Text
                del = CType(grdLabels.Items(i).Cells(Me.GRID_COL_DELETE_TRANSLATION).FindControl(Me.DELETE_CONTROL_NAME), ImageButton)
                If oVisible Then
                    ControlMgr.SetVisibleControl(Me, del, False)
                    ControlMgr.SetEnableControl(Me, Me.btnAppend, True)
                    ControlMgr.SetEnableControl(Me, Me.grdLabels, False)
                ElseIf Me.State.isNew Then
                    txtCtl = CType(grdLabels.Items(i).Cells(Me.GRID_COL_UI_PROG_CODE).FindControl(Me.UI_PROG_CODE_CONTROL_NAME), TextBox)
                    del = CType(grdLabels.Items(i).Cells(Me.GRID_COL_DELETE_TRANSLATION).FindControl(Me.DELETE_CONTROL_NAME), ImageButton)
                    ControlMgr.SetVisibleControl(Me, del, False)
                    If Not txtCtl Is Nothing AndAlso Not oImpotedCtl = "" Then
                        ControlMgr.SetEnableControl(Me, txtCtl, False)
                    End If
                    txtCtl = CType(grdLabels.Items(i).Cells(Me.GRID_COL_ENG_TRANSLATION).FindControl(Me.ENG_TRANSLATION_CONTROL_NAME), TextBox)
                    If Not txtCtl Is Nothing AndAlso Not oImpotedCtl = "" Then
                        ControlMgr.SetEnableControl(Me, txtCtl, False)
                    End If
                Else
                    If oImpotedCtl = "" Then
                        If Not del Is Nothing Then
                            ControlMgr.SetVisibleControl(Me, del, False)
                        End If
                    Else
                        ControlMgr.SetEnableControl(Me, Me.btnAppend, True)
                        ControlMgr.SetVisibleControl(Me, del, True)
                    End If
                End If
            Next


        End Sub
        Private Sub SortAndBindGrid()
            Me.State.PageIndex = Me.grdLabels.CurrentPageIndex
            Me.grdLabels.DataSource = Me.State.searchDV
            HighLightSortColumn(grdLabels, Me.State.SortExpression)
            Me.grdLabels.DataBind()
        End Sub

#End Region

#Region " Datagrid Related "

        Public Sub ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
            Try
                Me.State.Mybo = New NewDictionaryItem(New Guid(e.Item.Cells(Me.GRID_COL_ID).Text))
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
                    Me.State.Mybo.RemoveLabels(Me.State.Mybo.DictItemId)
                    Me.State.Mybo.Delete()
                    Me.State.Mybo.Save()
                    Me.State.isNew = False
                    Me.State.isDirty = False
                    Me.State.searchDV = Nothing
                    Me.PopulateGrid()
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                'Me.HandleErrors(ex, Me.ErrControllerMaster)
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Public Sub ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub grdLabels_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdLabels.ItemDataBound
            Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

            If (Not dvRow Is Nothing) AndAlso dvRow.Row.RowState = DataRowState.Added Then
                grdLabels.EditItemIndex = e.Item.ItemIndex
            End If
            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                Try
                    Dim ddl As DropDownList = CType(e.Item.Cells(Me.GRID_COL_MSG_TYPE).FindControl(GRID_CONTROL_MSG_TYPE), DropDownList)
                    If Not ddl Is Nothing Then
                        Dim dvList As DataView = LookupListNew.DropdownLookupList(LookupListNew.LK_MSG_TYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
                        Dim i As Integer
                        ddl.Items.Clear()
                        ddl.Items.Add(New ListItem("", ""))
                        If Not dvList Is Nothing Then
                            For i = 0 To dvList.Count - 1
                                ddl.Items.Add(New ListItem(dvList(i)("DESCRIPTION").ToString, dvList(i)("CODE").ToString))
                            Next
                        End If
                        ddl.SelectedValue = dvRow(NewDictionaryItem.NewDictItemSearchDV.COL_NAME_MSG_TYPE).ToString
                    End If

                    ddl = CType(e.Item.Cells(Me.GRID_COL_MSG_PARAM_CNT).FindControl(GRID_CONTROL_MSG_PARAM_COUNT), DropDownList)
                    If Not ddl Is Nothing Then
                        'PopulateControlFromBOProperty(ddl, dvRow(NewDictionaryItem.NewDictItemSearchDV.COL_NAME_MSG_PARAM_COUNT))
                        ddl.SelectedValue = dvRow(NewDictionaryItem.NewDictItemSearchDV.COL_NAME_MSG_PARAM_COUNT).ToString
                    End If
                Catch ex As Exception
                End Try

                Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_ID), dvRow(NewDictionaryItem.NewDictItemSearchDV.COL_NAME_NEW_DICT_ITEM_ID))
                Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_IMPORTED), dvRow(NewDictionaryItem.NewDictItemSearchDV.COL_NAME_IMPORTED))
            End If
        End Sub

        Private Sub Grid_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles grdLabels.SortCommand
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
                Me.State.PageIndex = 0
                Me.PopulateGrid()
            Catch ex As Exception
                ''Me.HandleErrors(ex, Me.ErrControllerMaster)
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Import()
            Dim oImpotedCtl As String, labelID As Guid

            For i As Integer = 0 To (grdLabels.Items.Count - 1)
                Try
                    grdLabels.SelectedIndex = i
                    oImpotedCtl = grdLabels.Items(i).Cells(Me.GRID_COL_IMPORTED).Text
                    If Not oImpotedCtl = "" Then
                        Me.State.Mybo = New NewDictionaryItem(New Guid(Me.grdLabels.Items(i).Cells(Me.GRID_COL_ID).Text))

                        If oImpotedCtl = "N" Then
                            Me.State.Mybo.GenerateNewLabels(labelID)
                            If (Me.State.Mybo.IsFamilyDirty) Then
                                Me.State.Mybo.Save()
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
                                Me.State.oLabelCount += 1
                            End If
                        ElseIf oImpotedCtl = "Y" Then
                            Me.State.Mybo.ChangeLabels(Me.State.Mybo.DictItemId)
                            If (Me.State.Mybo.IsFamilyDirty) Then
                                Me.State.Mybo.Save()
                            End If
                        End If
                    End If
                Catch ex As Exception
                End Try
            Next

            ControlMgr.SetVisibleControl(Me, Me.pnlLabelCount, True)
            'End If
            Me.PopulateControlFromBOProperty(Me.txtMessageCount, Me.State.oLabelCount)
            Me.State.oLabelCount = 0
            Me.State.searchDV = NewDictionaryItem.getList()
            Me.PopulateGrid()

        End Sub

        Private Function IsGridDirty() As Boolean
            Dim errMsg As Collections.Generic.List(Of String) = New Collections.Generic.List(Of String)
            Dim oImpotedCtl As String
            Me.State.isDirty = False

            For i As Integer = 0 To (grdLabels.Items.Count - 1)
                grdLabels.SelectedIndex = i
                oImpotedCtl = grdLabels.Items(i).Cells(Me.GRID_COL_IMPORTED).Text
                If Not oImpotedCtl Is Nothing Then
                    Select Case oImpotedCtl
                        Case ""
                            Me.State.Mybo = New NewDictionaryItem
                        Case Else
                            Me.State.Mybo = New NewDictionaryItem(New Guid(Me.grdLabels.Items(i).Cells(Me.GRID_COL_ID).Text))
                    End Select
                End If
                PopulateBOFromForm(i, errMsg)
            Next

            Return Me.State.isDirty

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
                    oImpotedCtl = grdLabels.Items(i).Cells(Me.GRID_COL_IMPORTED).Text
                    If Not oImpotedCtl Is Nothing Then
                        Select Case oImpotedCtl
                            Case ""
                                Me.State.Mybo = New NewDictionaryItem
                            Case Else
                                Me.State.Mybo = New NewDictionaryItem(New Guid(Me.grdLabels.Items(i).Cells(Me.GRID_COL_ID).Text))
                        End Select
                        If PopulateBOFromForm(i, errMsg) Then
                            If Me.State.Mybo.IsDirty Then
                                Me.State.Mybo.Save()
                                Me.State.HasDataChanged = True
                            End If
                        Else
                            popHasErr = True
                        End If
                    End If
                Catch ex As Exception
                    oError = True
                    ''Me.HandleErrors(ex, Me.ErrControllerMaster)
                    Me.HandleErrors(ex, Me.MasterPage.MessageController)
                End Try
            Next

            If popHasErr Then
                'Me.ErrControllerMaster.AddErrorAndShow(errMsg.ToArray, False)
                Me.MasterPage.MessageController.AddErrorAndShow(errMsg.ToArray, False)
            End If

            If Not oError Then
                Me.State.isNew = False
                Me.State.isDirty = False
                Me.State.searchDV = Nothing
                ControlMgr.SetEnableControl(Me, Me.btnNew_WRITE, True)
            End If
            oError = False
        End Sub
        ''Def-25721: Added function to display page label
        Private Sub UpdateBreadCrum()
            If (Not Me.State Is Nothing) Then
                If (Not Me.State Is Nothing) Then
                    Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator &
                        TranslationBase.TranslateLabelOrMessage("Add New Dictionary Labels")
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Add New Dictionary Labels")
                End If
            End If
        End Sub
#End Region

#Region "Button Management"

        Private Sub btnNew_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
            Try
                Me.State.Mybo = New NewDictionaryItem
                Me.State.searchDV = State.Mybo.GetNewDataViewRow(Me.State.searchDV, State.Mybo)
                Me.State.isNew = True
                ControlMgr.SetEnableControl(Me, Me.btnNew_WRITE, False)
                PopulateGrid()
            Catch ex As Exception
                ''Me.HandleErrors(ex, Me.ErrControllerMaster)
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            
        End Sub

        Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click

            Dim oImpotedCtl As String
            Me.State.isDirty = False

            Try
                If IsGridDirty() Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    Me.ReturnToTabHomePage() '(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.Mybo, Me.State.HasDataChanged))
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                ''Me.HandleErrors(ex, Me.ErrControllerMaster)
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
                Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                ''Me.State.LastErrMsg = Me.ErrControllerMaster.Text
                Me.State.LastErrMsg = Me.MasterPage.MessageController.Text
            End Try
        End Sub

        Private Sub moBtnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles moBtnCancel.Click
            Try
                Me.State.isNew = False
                Me.State.isDirty = False
                Me.State.searchDV = Nothing
                ControlMgr.SetEnableControl(Me, Me.btnNew_WRITE, True)
                PopulateGrid()
            Catch ex As Exception
                ''Me.HandleErrors(ex, Me.ErrControllerMaster)
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moBtnSave_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles moBtnSave_WRITE.Click
            Try
                If State.isNew Then
                    Dim errMsg As Collections.Generic.List(Of String) = New Collections.Generic.List(Of String)
                    If PopulateBOFromForm(grdLabels.EditItemIndex, errMsg) Then
                        If (Me.State.Mybo.IsDirty) Then
                            If State.Mybo.MsgCode = String.Empty OrElse MessageCode.IsNewMsgCode(State.Mybo.MsgCode, State.Mybo.MsgType, State.Mybo.UiProgCode) Then
                                Me.State.Mybo.Save()
                                Me.State.searchDV = Nothing
                                Me.State.isDirty = False
                                Me.State.isNew = False
                                PopulateGrid()
                                ControlMgr.SetEnableControl(Me, Me.btnNew_WRITE, True)
                            Else
                                Me.DisplayMessage(MessageCode.Err_MSG_CODE_EXISTS, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                            End If
                        End If
                    Else
                        '' Me.ErrControllerMaster.AddErrorAndShow(errMsg.ToArray, False)
                        Me.MasterPage.MessageController.AddErrorAndShow(errMsg.ToArray, False)

                    End If
                Else
                    SaveNewDictItem()
                    Me.PopulateGrid()
                End If
            Catch ex As Exception
                Me.PopulateGrid()
                ''Me.HandleErrors(ex, Me.ErrControllerMaster)
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Sub btnAppend_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAppend.Click
            If IsGridDirty() Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Accept
            Else
                Import()
            End If

        End Sub

        Private Sub btnWipeout_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnWipeout.Click
            Try
                Me.State.Mybo.DeleteAll()
                Me.State.searchDV = NewDictionaryItem.getList()
                Me.PopulateGrid()
            Catch ex As Exception
                ''Me.HandleErrors(ex, Me.ErrControllerMaster)
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
#End Region
    End Class

End Namespace
