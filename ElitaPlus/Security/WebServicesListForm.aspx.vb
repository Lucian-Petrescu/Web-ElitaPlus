Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Security

    Partial Public Class WebServicesListForm
        Inherits ElitaPlusSearchPage
#Region "Constants"
        Public Const URL As String = "Security/WebServicesListForm.aspx"
        Public Const PAGETITLE As String = "WEBSERVICES"
        Public Const PAGETAB As String = "ADMIN"

        Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
        Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"
        Private Const MSG_OPERATION_NOT_AVAILABLE As String = "OPERATION_NOT_READY_IN_THIS_VERSION"

        Public Const GRID_COL_WEB_SERVICE_ID_IDX As Integer = 0
        Public Const GRID_COL_IMAGE_BUTTON_EDIT_IDX As Integer = 1
        Public Const GRID_COL_IMAGE_BUTTON_DELETE_IDX As Integer = 2
        Public Const GRID_COL_STOP_START_BUTTON_IDX As Integer = 3
        Public Const GRID_COL_ON_LINE_STATUS_IDX As Integer = 4
        Public Const GRID_COL_WEB_SERVICE_NAME_IDX As Integer = 5
        Public Const GRID_COL_LAST_OPERATION_DATE_IDX As Integer = 6
        Public Const GRID_COL_USER_IDX As Integer = 7
        Public Const GRID_COL_DEFAULT_ON_LINE_MESSAGE_IDX As Integer = 8
        Public Const GRID_COL_ITEMS_BUTTON_IDX As Integer = 9

        Public Const ACTION_BUTTON_CONTROL_START_NAME As String = "btnActionStart"
        Public Const ACTION_BUTTON_CONTROL_STOP_NAME As String = "btnActionStop"
        Public Const ACTION_BUTTON_CONTROL_ITEMS_NAME As String = "btnItems"
        Public Const ACTION_IMAGE_BUTTON_CONTROL_EDIT_NAME As String = "btnEdit"
        Public Const ACTION_IMAGE_BUTTON_CONTROL_DELETE_NAME As String = "DeleteButton_WRITE"

        Public Const GRID_CTRL_WEBSERVICE_ID As String = "lblWebservice_id"
        Public Const GRID_CTRL_LAST_OPERATION_DATE_LABLE_NAME As String = "last_operation_dateLabel"

        Public Const GRID_CTRL_WEB_SERVICE_NAME As String = "cboweb_service_name"
        Public Const GRID_CTRL_ON_LINE_STATUS_NAME As String = "cboOn_Line_Status"
        Public Const GRID_CTRL_OFF_LINE_MESSAGE_NAME As String = "Off_Line_MessageTextBox"

        Public Const GRID_CTRL_WEB_SERVICE_LABEL_NAME As String = "web_service_nameLabel"
        Public Const GRID_CTRL_ON_LINE_STATUS_LABEL_NAME As String = "On_Line_StatusLabel"
        Public Const GRID_CTRL_WEB_SERVICE_LABEL_EDIT_NAME As String = "web_service_nameLabelEdit"
        Public Const GRID_CTRL_ON_LINE_STATUS_LABEL_EDIT_NAME As String = "On_Line_StatusLabelEdit"
        Public Const GRID_CTRL_LAST_CHANGE_BY_LABEL_NAME As String = "last_change_byLabel"
        Public Const GRID_CTRL_OFF_LINE_MESSAGE_LABEL_NAME As String = "Off_Line_MessageLabel"


        Private Const EDIT_COMMAND As String = "EditRecord"
        Private Const DELETE_COMMAND As String = "DeleteRecord"
        Private Const ITEMS_COMMAND As String = "ItemsCMD"
        Private Const START_COMMAND As String = "Start"
        Private Const STOP_COMMAND As String = "Stop"
        Private Const SORT_COMMAND As String = "Sort"

        Private Const NO_ROW_SELECTED_INDEX As Integer = -1

        Private txtDefaultMsgFromLastRow As String
#End Region

#Region "Page State"
        Private IsReturningFromChild As Boolean = False

        Class MyState
            Public moWebServiceName As String = Nothing
            Public boWebservice As BusinessObjectsNew.Webservices
            Public WebserviceId As Guid
            Public moOnLineId As Guid = Guid.Empty
            Public PageIndex As Integer = 0
            Public PageSize As Integer = 10
            Public searchDV As DataView = Nothing
            Public searchClick As Boolean = False
            Public serverFoundMSG As String
            Public selectedWebServiceId As Guid = Guid.Empty
            Public IsGridVisible As Boolean = False
            Public IsAfterSave As Boolean
            Public bnoRow As Boolean = False
            Public IsEditMode As Boolean
            Public AddingNewRow As Boolean
            Public Canceling As Boolean
            Public Yes_Id As Guid
            Public No_Id As Guid
            Public On_line_Translated As String
            Public Off_line_Translated As String
            Public SortExpression As String = Webservices.COL_NAME_WEB_SERVICE_NAME
            Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE


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

#Region "Properties"

        Public ReadOnly Property IsEditing() As Boolean
            Get
                IsEditing = (Grid.EditIndex > NO_ROW_SELECTED_INDEX)
            End Get
        End Property

#End Region

#Region "Page event handlers"
        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            ErrControllerMaster.Clear_Hide()
            Form.DefaultButton = SearchButton.UniqueID
            Try
                If Not IsPostBack Then
                    SortDirection = State.SortExpression
                    SetFormTitle(PAGETITLE)
                    SetFormTab(PAGETAB)
                    TranslateGridHeader(Grid)
                    TranslateStatuses()
                    SetDefaultButton(txtWebServiceName, SearchButton)
                    PopulateDropdowns()
                    GetStateProperties()
                    SetButtonsState()
                    If Not IsReturningFromChild Then
                        ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    Else
                        ' It is returning from detail
                        PopulateGrid()
                    End If
                    SetFocus(txtWebServiceName)
                End If
                DisplayProgressBarOnClick(SearchButton, "Loading_WebServices")
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
            ShowMissingTranslations(ErrControllerMaster)
        End Sub

        Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
            Try
                MenuEnabled = True
                IsReturningFromChild = True
                Dim retObj As WebServiceFunctionsListForm.ReturnType = CType(ReturnPar, WebServiceFunctionsListForm.ReturnType)
                If retObj IsNot Nothing AndAlso retObj.HasDataChanged Then
                    State.searchDV = Nothing
                End If
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If retObj IsNot Nothing Then
                            If Not retObj.EditingBo.IsNew Then
                                State.selectedWebServiceId = retObj.EditingBo.Id
                            End If
                            State.IsGridVisible = True
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
                End Select
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub
        Private Sub PopulateBOFromForm()

            Try
                Dim txtDefaultOffLineMsg As TextBox = CType(Grid.Rows(Grid.EditIndex).Cells(GRID_COL_DEFAULT_ON_LINE_MESSAGE_IDX).FindControl(GRID_CTRL_OFF_LINE_MESSAGE_NAME), TextBox)

                If State.AddingNewRow Then
                    Dim cboWebServiceName As DropDownList = CType(Grid.Rows(Grid.EditIndex).Cells(GRID_COL_WEB_SERVICE_NAME_IDX).FindControl(GRID_CTRL_WEB_SERVICE_NAME), DropDownList)
                    Dim cboOnLineStatus As DropDownList = CType(Grid.Rows(Grid.EditIndex).Cells(GRID_COL_ON_LINE_STATUS_IDX).FindControl(GRID_CTRL_ON_LINE_STATUS_NAME), DropDownList)
                    State.boWebservice.WebServiceName = cboWebServiceName.SelectedValue
                    'Me.PopulateBOProperty(Me.State.boWebservice, "WebServiceName", cboWebServiceName)
                    PopulateBOProperty(State.boWebservice, "OffLineMessage", txtDefaultOffLineMsg)
                    PopulateBOProperty(State.boWebservice, "OnLineId", cboOnLineStatus)
                    'Me.State.boWebservice.OnLineId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)
                Else
                    PopulateBOProperty(State.boWebservice, "OffLineMessage", txtDefaultOffLineMsg)
                End If

            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub
        Private Sub TranslateStatuses()
            State.On_line_Translated = TranslationBase.TranslateLabelOrMessage("ON_LINE")
            State.Off_line_Translated = TranslationBase.TranslateLabelOrMessage("OFF_LINE")
        End Sub
#End Region

#Region "Button event handlers"
        Protected Sub SearchButton_Click(sender As Object, e As EventArgs) Handles SearchButton.Click
            Try
                SetStateProperties()
                State.PageIndex = 0
                State.selectedWebServiceId = Guid.Empty
                State.IsGridVisible = True
                State.searchClick = True
                State.searchDV = Nothing
                State.moOnLineId = ElitaPlusPage.GetSelectedItem(cboOn_Line_Status)
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Protected Sub ClearButton_Click(sender As Object, e As EventArgs) Handles ClearButton.Click
            Try
                txtWebServiceName.Text = String.Empty
                cboOn_Line_Status.SelectedIndex = -1

                'Update Page State
                With State
                    .moWebServiceName = txtWebServiceName.Text
                    .moOnLineId = GetSelectedItem(cboOn_Line_Status)
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
                AddNewWebService()
                SetButtonsState()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Private Sub SaveButton_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles SaveButton_WRITE.Click

            Try
                PopulateBOFromForm()
                If (State.boWebservice.IsDirty) Then
                    State.boWebservice.Save()
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

#Region "Helper functions"
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

        Private Sub ReturnFromEditing()

            Grid.EditIndex = NO_ROW_SELECTED_INDEX

            If Grid.PageCount = 0 Then
                'if returning to the "1st time in" screen
                ControlMgr.SetVisibleControl(Me, Grid, False)
            Else
                ControlMgr.SetVisibleControl(Me, Grid, True)
            End If

            State.IsEditMode = False
            PopulateGrid()
            State.PageIndex = Grid.PageIndex
            SetButtonsState()

        End Sub

        Protected Sub PopulateDropdowns()

            'Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            'Dim yesNoLkL As DataView = LookupListNew.DropdownLookupList("YESNO", langId, True)
            'Me.BindListControlToDataView(Me.cboOn_Line_Status, yesNoLkL, , , True)

            Dim YESNOList As DataElements.ListItem() =
                        CommonConfigManager.Current.ListManager.GetList(listCode:="YESNO",
                                                                languageCode:=Thread.CurrentPrincipal.GetLanguageCode())

            cboOn_Line_Status.Populate(YESNOList.ToArray(),
                                New PopulateOptions() With
                                {
                                    .AddBlankItem = True
                                })


            State.Yes_Id = (From YESNO In YESNOList
                               Where YESNO.Code = Codes.YESNO_Y
                               Select YESNO).FirstOrDefault().ListItemId

            State.No_Id = (From YESNO In YESNOList
                              Where YESNO.Code = Codes.YESNO_N
                              Select YESNO).FirstOrDefault().ListItemId

        End Sub

        Private Sub GetStateProperties()
            Try
                txtWebServiceName.Text = State.moWebServiceName

            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub SetStateProperties()

            Try
                If State Is Nothing Then
                    RestoreState(New MyState)
                End If
                State.moWebServiceName = txtWebServiceName.Text

            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub PopulateGrid()
            Try
                Dim oDataView As DataView
                Dim sortBy As String = "web_service_name"
                If (State.searchDV Is Nothing) Then
                    State.searchDV = Webservices.GetWebServices(State.moWebServiceName, State.moOnLineId)
                    If State.searchClick Then
                        ValidSearchResultCount(State.searchDV.Count, True)
                        State.searchClick = False
                    End If
                End If

                If Not (State.searchDV Is Nothing) Then
                    State.searchDV.Sort = SortDirection
                End If

                If (State.IsAfterSave) Then
                    State.IsAfterSave = False
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.WebserviceId, Grid, State.PageIndex)
                ElseIf (State.IsEditMode) Then
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.WebserviceId, Grid, State.PageIndex, State.IsEditMode)
                ElseIf IsReturningFromChild Then
                    IsReturningFromChild = False
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.WebserviceId, Grid, State.PageIndex)
                Else
                    SetPageAndSelectedIndexFromGuid(State.searchDV, Guid.Empty, Grid, State.PageIndex)
                End If

                Grid.PageSize = State.PageSize

                SortAndBindGrid()

            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub


        Private Function GetGridDataView() As DataView
            If (State.searchDV Is Nothing) Then
                State.searchDV = Webservices.GetWebServices(State.moWebServiceName, State.moOnLineId)
            End If
            Return State.searchDV
        End Function

        Private Sub SortAndBindGrid()
            'Me.State.PageIndex = Me.Grid.CurrentPageIndex
            'Me.Grid.DataSource = Me.State.searchDV
            'HighLightSortColumn(Grid, Me.State.SortExpression)

            State.PageIndex = Grid.PageIndex
            If (State.searchDV.Count = 0) Then

                State.bnoRow = True
                'Me.Grid.DataSource = Nothing
                CreateHeaderForEmptyGrid(Grid, SortDirection)
            Else
                State.bnoRow = False
                Grid.Enabled = True
                Grid.DataSource = State.searchDV
                HighLightSortColumn(Grid, SortDirection)
                Grid.DataBind()
            End If

            If Grid.BottomPagerRow IsNot Nothing AndAlso Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

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

        Private Sub AddNewWebService()

            Dim dv As DataView

            State.searchDV = GetGridDataView()

            State.boWebservice = New Assurant.ElitaPlus.BusinessObjectsNew.Webservices
            State.WebserviceId = State.boWebservice.Id

            State.searchDV = State.boWebservice.GetNewDataViewRow(State.searchDV, State.WebserviceId)

            Grid.DataSource = State.searchDV

            SetPageAndSelectedIndexFromGuid(State.searchDV, State.WebserviceId, Grid, State.PageIndex, State.IsEditMode)

            Grid.DataBind()

            State.PageIndex = Grid.PageIndex

            SetGridControls(Grid, False)

            'Set focus on the Description TextBox for the EditItemIndex row
            SetFocusOnEditableDropDownFieldInGrid(Grid, GRID_COL_ON_LINE_STATUS_IDX, GRID_CTRL_ON_LINE_STATUS_NAME, Grid.EditIndex)

            PopulateFormFromBO(True)

            'Me.TranslateGridControls(Grid)
            SetButtonsState()
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
        End Sub

        Private Sub PopulateFormFromBO(Optional ByVal blnAddingNew As Boolean = False)

            Dim gridRowIdx As Integer = Grid.EditIndex

            Dim txtOffLineMessage As TextBox = CType(Grid.Rows(gridRowIdx).Cells(GRID_COL_DEFAULT_ON_LINE_MESSAGE_IDX).FindControl(GRID_CTRL_OFF_LINE_MESSAGE_NAME), TextBox)
            Dim cboOnLineStatus As DropDownList = CType(Grid.Rows(Grid.EditIndex).Cells(GRID_COL_ON_LINE_STATUS_IDX).FindControl(GRID_CTRL_ON_LINE_STATUS_NAME), DropDownList)

            Dim cboWebServiceNames As DropDownList = CType(Grid.Rows(gridRowIdx).Cells(GRID_COL_WEB_SERVICE_NAME_IDX).FindControl(GRID_CTRL_WEB_SERVICE_NAME), DropDownList)


            Dim btnActionStart As Button = CType(Grid.Rows(gridRowIdx).Cells(GRID_COL_STOP_START_BUTTON_IDX).FindControl(ACTION_BUTTON_CONTROL_START_NAME), Button)
            Dim btnActionStop As Button = CType(Grid.Rows(gridRowIdx).Cells(GRID_COL_STOP_START_BUTTON_IDX).FindControl(ACTION_BUTTON_CONTROL_STOP_NAME), Button)
            Dim imgImageButton As ImageButton = CType(Grid.Rows(gridRowIdx).Cells(GRID_COL_IMAGE_BUTTON_EDIT_IDX).FindControl(ACTION_IMAGE_BUTTON_CONTROL_EDIT_NAME), ImageButton)
            Dim btnItemsButton As Button = CType(Grid.Rows(gridRowIdx).Cells(GRID_COL_ITEMS_BUTTON_IDX).FindControl(ACTION_BUTTON_CONTROL_ITEMS_NAME), Button)
            btnActionStart.Visible = False
            btnActionStop.Visible = False
            imgImageButton.Visible = False
            btnItemsButton.Visible = False

            txtWebServiceName.Width = Unit.Pixel(250)
            txtOffLineMessage.Width = Unit.Pixel(250)
            cboOnLineStatus.Width = Unit.Pixel(50)
            txtOffLineMessage.Text = txtDefaultMsgFromLastRow
            txtOffLineMessage.Width = Unit.Pixel(250)


            Try
                If blnAddingNew Then
                    Dim webServiceNames As DataView = XMLHelper.GetWebServiceNames.Tables(0).DefaultView
                    Dim i As Integer
                    cboWebServiceNames.Items.Clear()
                    cboWebServiceNames.Items.Add(New ListItem("", ""))

                    If webServiceNames IsNot Nothing Then
                        For i = 0 To webServiceNames.Count - 1
                            cboWebServiceNames.Items.Add(New ListItem(webServiceNames(i)("DESCRIPTION").ToString, webServiceNames(i)("CODE").ToString))
                        Next
                    End If

                    'Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                    'Dim yesNoLkL As DataView = LookupListNew.DropdownLookupList("YESNO", langId, True)
                    'Me.BindListControlToDataView(cboOnLineStatus, yesNoLkL, , , True)
                    Dim YESNOList As DataElements.ListItem() =
                        CommonConfigManager.Current.ListManager.GetList(listCode:="YESNO",
                                                                languageCode:=Thread.CurrentPrincipal.GetLanguageCode())

                    cboOnLineStatus.Populate(YESNOList.ToArray(),
                                New PopulateOptions() With
                                {
                                    .AddBlankItem = True
                                })

                    SetFocus(cboOnLineStatus)

                Else
                    Dim lblOnLineStatusCtrl As Label = CType(Grid.Rows(gridRowIdx).Cells(GRID_COL_ON_LINE_STATUS_IDX).FindControl(GRID_CTRL_ON_LINE_STATUS_LABEL_EDIT_NAME), Label)
                    If lblOnLineStatusCtrl.Text.Equals("Y") Then
                        lblOnLineStatusCtrl.Text = State.On_line_Translated
                    ElseIf lblOnLineStatusCtrl.Text.Equals("N") Then
                        lblOnLineStatusCtrl.Text = State.Off_line_Translated
                    End If
                    cboWebServiceNames.Visible = False
                    cboOnLineStatus.Visible = False
                    SetFocus(txtOffLineMessage)
                End If

            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Public Sub SaveBo(strStartOrStopID As Guid)
            Try
                State.boWebservice.OnLineId = strStartOrStopID
                State.boWebservice.LastOperationDate = Date.Now
                If (State.boWebservice.IsDirty) Then
                    State.boWebservice.Save()
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
#End Region

#Region "Grid related"

        Private Sub Grid_PageIndexChanged(sender As Object, e As System.EventArgs) Handles Grid.PageIndexChanged
            Try
                State.PageIndex = Grid.PageIndex
                State.selectedWebServiceId = Guid.Empty
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                Grid.PageIndex = e.NewPageIndex
                State.PageIndex = Grid.PageIndex
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Public Sub RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand

            Try
                Dim index As Integer

                If (e.CommandName = EDIT_COMMAND) Then
                    index = CInt(e.CommandArgument)
                    'Set the IsEditMode flag to TRUE
                    State.IsEditMode = True
                    State.WebserviceId = New Guid(CType(Grid.Rows(index).Cells(GRID_COL_WEB_SERVICE_ID_IDX).FindControl(GRID_CTRL_WEBSERVICE_ID), Label).Text)
                    State.boWebservice = New Assurant.ElitaPlus.BusinessObjectsNew.Webservices(State.WebserviceId)
                    PopulateGrid()
                    State.PageIndex = Grid.PageIndex
                    'Disable all Edit and Delete icon buttons on the Grid
                    SetGridControls(Grid, False)
                    'Set focus on the Description TextBox for the EditItemIndex row
                    SetFocusOnEditableFieldInGrid(Grid, GRID_COL_DEFAULT_ON_LINE_MESSAGE_IDX, GRID_CTRL_OFF_LINE_MESSAGE_NAME, index)
                    PopulateFormFromBO()
                    SetButtonsState()
                ElseIf e.CommandName = ITEMS_COMMAND Then

                    index = CInt(e.CommandArgument)
                    State.selectedWebServiceId = New Guid(CType(Grid.Rows(index).Cells(GRID_COL_WEB_SERVICE_ID_IDX).FindControl(GRID_CTRL_WEBSERVICE_ID), Label).Text)
                    callPage(WebServiceFunctionsListForm.URL, State.selectedWebServiceId)
                    'Me.AddInfoMsg(Me.MSG_OPERATION_NOT_AVAILABLE)

                ElseIf e.CommandName = STOP_COMMAND Then
                    index = CInt(e.CommandArgument)
                    State.IsEditMode = True
                    State.WebserviceId = New Guid(CType(Grid.Rows(index).Cells(GRID_COL_WEB_SERVICE_ID_IDX).FindControl(GRID_CTRL_WEBSERVICE_ID), Label).Text)
                    State.boWebservice = New Assurant.ElitaPlus.BusinessObjectsNew.Webservices(State.WebserviceId)

                    State.PageIndex = Grid.PageIndex
                    SaveBo(State.No_Id)

                ElseIf e.CommandName = START_COMMAND Then
                    index = CInt(e.CommandArgument)
                    State.IsEditMode = True
                    State.WebserviceId = New Guid(CType(Grid.Rows(index).Cells(GRID_COL_WEB_SERVICE_ID_IDX).FindControl(GRID_CTRL_WEBSERVICE_ID), Label).Text)
                    State.boWebservice = New Assurant.ElitaPlus.BusinessObjectsNew.Webservices(State.WebserviceId)
                    State.PageIndex = Grid.PageIndex
                    SaveBo(State.Yes_Id)

                ElseIf (e.CommandName = DELETE_COMMAND) Then
                    index = CInt(e.CommandArgument)
                    'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                    Grid.SelectedIndex = NO_ROW_SELECTED_INDEX

                    'Save the Id in the Session

                    State.WebserviceId = New Guid(CType(Grid.Rows(index).Cells(GRID_COL_WEB_SERVICE_ID_IDX).FindControl(GRID_CTRL_WEBSERVICE_ID), Label).Text) 'Me.State.selectedWebServiceId

                    State.boWebservice = New Assurant.ElitaPlus.BusinessObjectsNew.Webservices(State.WebserviceId)
                    Try
                        State.boWebservice.Delete()
                        'Call the Save() method in the Manufacturer Business Object here
                        State.boWebservice.Save()
                        State.IsAfterSave = True
                        AddInfoMsg(MSG_RECORD_DELETED_OK)
                        State.searchDV = Nothing
                        ReturnFromEditing()
                        State.PageIndex = Grid.PageIndex
                    Catch ex As Exception
                        State.boWebservice.RejectChanges()
                        Throw ex
                    End Try

                    State.PageIndex = Grid.PageIndex

                ElseIf ((e.CommandName = SORT_COMMAND) AndAlso Not (IsEditing)) Then

                End If

            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Try
                If dvRow IsNot Nothing AndAlso State.searchDV.Count > 0 Then
                    If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                        CType(e.Row.Cells(GRID_COL_WEB_SERVICE_ID_IDX).FindControl(GRID_CTRL_WEBSERVICE_ID), Label).Text = GetGuidStringFromByteArray(CType(dvRow("webservice_id"), Byte()))

                        If Not dvRow("last_operation_date").Equals(DBNull.Value) Then
                            Dim lastOperationDate As Date = CType(dvRow(State.boWebservice.WebServicesSearchDV.COL_LAST_OPERATION_DATE), Date)
                            CType(e.Row.Cells(GRID_COL_LAST_OPERATION_DATE_IDX).FindControl(GRID_CTRL_LAST_OPERATION_DATE_LABLE_NAME), Label).Text = GetLongDateFormattedString(lastOperationDate)
                        End If

                        CType(e.Row.Cells(GRID_COL_USER_IDX).FindControl(GRID_CTRL_LAST_CHANGE_BY_LABEL_NAME), Label).Text = dvRow("last_change_by").ToString

                        If (State.IsEditMode = True AndAlso State.WebserviceId.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow("webservice_id"), Byte())))) Then
                            CType(e.Row.Cells(GRID_COL_DEFAULT_ON_LINE_MESSAGE_IDX).FindControl(GRID_CTRL_OFF_LINE_MESSAGE_NAME), TextBox).Text = dvRow("off_line_message").ToString
                            If Not State.AddingNewRow Then txtDefaultMsgFromLastRow = dvRow("off_line_message").ToString
                            CType(e.Row.Cells(GRID_COL_WEB_SERVICE_NAME_IDX).FindControl(GRID_CTRL_WEB_SERVICE_LABEL_EDIT_NAME), Label).Text = dvRow("web_service_name").ToString
                            CType(e.Row.Cells(GRID_COL_ON_LINE_STATUS_IDX).FindControl(GRID_CTRL_ON_LINE_STATUS_LABEL_EDIT_NAME), Label).Text = dvRow("on_line").ToString
                            CType(e.Row.Cells(GRID_COL_ON_LINE_STATUS_IDX).FindControl(GRID_CTRL_ON_LINE_STATUS_LABEL_EDIT_NAME), Label).Text = dvRow("on_line").ToString
                            CType(e.Row.Cells(GRID_COL_WEB_SERVICE_NAME_IDX).FindControl(GRID_CTRL_WEB_SERVICE_LABEL_EDIT_NAME), Label).Text = dvRow("web_service_name").ToString

                        Else

                            CType(e.Row.Cells(GRID_COL_ON_LINE_STATUS_IDX).FindControl(GRID_CTRL_ON_LINE_STATUS_LABEL_NAME), Label).Text = dvRow("on_line").ToString
                            CType(e.Row.Cells(GRID_COL_WEB_SERVICE_NAME_IDX).FindControl(GRID_CTRL_WEB_SERVICE_LABEL_NAME), Label).Text = dvRow("web_service_name").ToString
                            CType(e.Row.Cells(GRID_COL_DEFAULT_ON_LINE_MESSAGE_IDX).FindControl(GRID_CTRL_OFF_LINE_MESSAGE_LABEL_NAME), Label).Text = dvRow("off_line_message").ToString
                            Dim btnActionStart As Button = CType(e.Row.Cells(GRID_COL_STOP_START_BUTTON_IDX).FindControl(ACTION_BUTTON_CONTROL_START_NAME), Button)
                            Dim btnActionStop As Button = CType(e.Row.Cells(GRID_COL_STOP_START_BUTTON_IDX).FindControl(ACTION_BUTTON_CONTROL_STOP_NAME), Button)
                            Dim imgImageButton As ImageButton = CType(e.Row.Cells(GRID_COL_IMAGE_BUTTON_EDIT_IDX).FindControl(ACTION_IMAGE_BUTTON_CONTROL_EDIT_NAME), ImageButton)
                            Dim imgDeleteImageButton As ImageButton = CType(e.Row.Cells(GRID_COL_IMAGE_BUTTON_EDIT_IDX).FindControl(ACTION_IMAGE_BUTTON_CONTROL_DELETE_NAME), ImageButton)
                            Dim btnItemsButton As Button = CType(e.Row.Cells(GRID_COL_ITEMS_BUTTON_IDX).FindControl(ACTION_BUTTON_CONTROL_ITEMS_NAME), Button)
                            Dim lblOnLineStatusCtrl As Label = CType(e.Row.Cells(GRID_COL_ON_LINE_STATUS_IDX).FindControl(GRID_CTRL_ON_LINE_STATUS_LABEL_NAME), Label)
                            Dim lblWebServiceNameCtrl As Label = CType(e.Row.Cells(GRID_COL_WEB_SERVICE_NAME_IDX).FindControl(GRID_CTRL_WEB_SERVICE_LABEL_NAME), Label)

                            If State.AddingNewRow Then
                                btnActionStart.Visible = False
                                btnActionStop.Visible = False
                                imgImageButton.Visible = False
                                imgDeleteImageButton.Visible = False
                                btnItemsButton.Visible = False
                                If e.Row.RowIndex = State.searchDV.Count - 2 Then txtDefaultMsgFromLastRow = CType(e.Row.Cells(GRID_COL_DEFAULT_ON_LINE_MESSAGE_IDX).FindControl(GRID_CTRL_OFF_LINE_MESSAGE_LABEL_NAME), Label).Text
                                If e.Row.RowIndex < State.searchDV.Count - 1 Then e.Row.Enabled = False
                            Else
                                If lblOnLineStatusCtrl.Text.Equals("Y") Then
                                    btnActionStart.Visible = False
                                    btnActionStop.Visible = True
                                    lblOnLineStatusCtrl.Text = State.On_line_Translated
                                ElseIf lblOnLineStatusCtrl.Text.Equals("N") Then
                                    btnActionStart.Visible = True
                                    btnActionStop.Visible = False
                                    lblOnLineStatusCtrl.Text = State.Off_line_Translated
                                End If
                            End If
                        End If
                    End If
                End If

            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(grid As GridView, cellPosition As Integer, controlName As String, itemIndex As Integer)
            'Set focus on the Description TextBox for the EditItemIndex row
            Dim txtDefaultMsg As TextBox = CType(grid.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
            SetFocus(txtDefaultMsg)
        End Sub
        Private Sub SetFocusOnEditableDropDownFieldInGrid(grid As GridView, cellPosition As Integer, controlName As String, itemIndex As Integer)
            'Set focus on the Description TextBox for the EditItemIndex row
            Dim cboDefaultMsg As DropDownList = CType(grid.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), DropDownList)
            SetFocus(cboDefaultMsg)
        End Sub

        Public Sub RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
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
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub cboPageSize_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                State.PageSize = CType(cboPageSize.SelectedValue, Integer)
                State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.PageSize)
                Grid.PageIndex = State.PageIndex
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Public Property SortDirection() As String
            Get
                Return ViewState("SortDirection").ToString
            End Get
            Set(value As String)
                ViewState("SortDirection") = value
            End Set
        End Property

#End Region


    End Class
End Namespace