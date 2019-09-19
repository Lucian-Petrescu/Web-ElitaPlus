Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Security

    Partial Public Class WebServiceFunctionsListForm
        Inherits ElitaPlusSearchPage
#Region "Constants"
        Public Const URL As String = "WebServiceFunctionsListForm.aspx"
        Public Const PAGETITLE As String = "WEB_SERVICE_FUNCTIONS"
        Public Const PAGETAB As String = "ADMIN"

        Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
        Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"
        Private Const MSG_OPERATION_NOT_AVAILABLE As String = "OPERATION_NOT_READY_IN_THIS_VERSION"

        Public Const GRID_COL_WEB_SERVICE_FUNCTION_ID_IDX As Integer = 0
        Public Const GRID_COL_IMAGE_BUTTON_EDIT_IDX As Integer = 1
        Public Const GRID_COL_IMAGE_BUTTON_DELETE_IDX As Integer = 2
        Public Const GRID_COL_STOP_START_BUTTON_IDX As Integer = 3
        Public Const GRID_COL_ON_LINE_STATUS_IDX As Integer = 4
        Public Const GRID_COL_WEB_SERVICE_FUNCTION_NAME_IDX As Integer = 5
        Public Const GRID_COL_LAST_OPERATION_DATE_IDX As Integer = 6
        Public Const GRID_COL_USER_IDX As Integer = 7
        Public Const GRID_COL_DEFAULT_ON_LINE_MESSAGE_IDX As Integer = 8


        Public Const ACTION_BUTTON_CONTROL_START_NAME As String = "btnActionStart"
        Public Const ACTION_BUTTON_CONTROL_STOP_NAME As String = "btnActionStop"
        Public Const ACTION_BUTTON_CONTROL_ITEMS_NAME As String = "btnItems"
        Public Const ACTION_IMAGE_BUTTON_CONTROL_EDIT_NAME As String = "btnEdit"
        Public Const ACTION_IMAGE_BUTTON_CONTROL_DELETE_NAME As String = "DeleteButton_WRITE"

        Public Const GRID_CTRL_WEBSERVICE_FUNCTION_ID As String = "lblWebservice_function_id"
        Public Const GRID_CTRL_LAST_OPERATION_DATE_LABLE_NAME As String = "last_operation_dateLabel"

        Public Const GRID_CTRL_WEB_SERVICE_FUNCTION_NAME As String = "cboweb_service_function_name"
        Public Const GRID_CTRL_ON_LINE_STATUS_NAME As String = "cboOn_Line_Status"
        Public Const GRID_CTRL_OFF_LINE_MESSAGE_NAME As String = "Off_Line_MessageTextBox"

        Public Const GRID_CTRL_WEB_SERVICE_FUNCTION_LABEL_NAME As String = "web_service_function_nameLabel"
        Public Const GRID_CTRL_ON_LINE_STATUS_LABEL_NAME As String = "On_Line_StatusLabel"
        Public Const GRID_CTRL_WEB_SERVICE_FUNCTION_LABEL_EDIT_NAME As String = "web_service_function_nameLabelEdit"
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
            Public boWebservice As BusinessObjectsNew.Webservices
            Public boWebServiceFunction As BusinessObjectsNew.WsFunctions
            Public WebServiceFunctionId As Guid
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
            Public SortExpression As String = WsFunctions.COL_NAME_WEB_SERVICE_FUNCTION_NAME
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

        Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
            Try
                If Not Me.CallingParameters Is Nothing Then
                    'Get the id from the parent
                    Me.State.boWebservice = New Webservices(CType(Me.CallingParameters, Guid))

                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub
#End Region

#Region "Page Return Type"
        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public EditingBo As Webservices
            Public HasDataChanged As Boolean
            Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As Webservices, ByVal hasDataChanged As Boolean)
                Me.LastOperation = LastOp
                Me.EditingBo = curEditingBo
                Me.HasDataChanged = hasDataChanged
            End Sub
        End Class

#End Region

#Region "Properties"

        Public ReadOnly Property IsEditing() As Boolean
            Get
                IsEditing = (Me.Grid.EditIndex > NO_ROW_SELECTED_INDEX)
            End Get
        End Property

#End Region

#Region "Page event handlers"
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Me.ErrControllerMaster.Clear_Hide()
            'Me.Form.DefaultButton = SearchButton.UniqueID
            Try
                If Not Me.IsPostBack Then
                    Me.SortDirection = Me.State.SortExpression
                    Me.SetFormTitle(PAGETITLE)
                    Me.SetFormTab(PAGETAB)
                    TranslateGridHeader(Grid)
                    Me.TranslateStatuses()
                    'Me.SetDefaultButton(Me.txtWebServiceName, SearchButton)
                    PopulateDropdowns()
                    'GetStateProperties()
                    SetButtonsState()
                    PopulateWebServiceInfo()

                    SetFocus(Me.txtWebServiceName)
                End If
                'Me.DisplayProgressBarOnClick(Me.SearchButton, "Loading_WebServices")
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
            Me.ShowMissingTranslations(Me.ErrControllerMaster)
        End Sub

        Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
            Try
                Me.MenuEnabled = True
                Me.IsReturningFromChild = True
                Dim retObj As WebServiceFunctionsListForm.ReturnType = CType(ReturnPar, WebServiceFunctionsListForm.ReturnType)
                If Not retObj Is Nothing AndAlso retObj.HasDataChanged Then
                    Me.State.searchDV = Nothing
                End If
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If Not retObj Is Nothing Then
                            If Not retObj.EditingBo.IsNew Then
                                Me.State.selectedWebServiceId = retObj.EditingBo.Id
                            End If
                            Me.State.IsGridVisible = True
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        Me.AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
                End Select
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub PopulateWebServiceInfo()

            Try
                With Me.State.boWebservice
                    Me.txtWebServiceName.Text = .WebServiceName
                    If LookupListNew.GetCodeFromId(LookupListCache.LK_YESNO, .OnLineId) = Codes.YESNO_Y Then
                        Me.txtOnLineStatus.Text = Me.State.On_line_Translated
                    Else
                        Me.txtOnLineStatus.Text = Me.State.Off_line_Translated
                    End If
                End With
                LoadGridData()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Private Sub PopulateBOFromForm()

            Try
                Dim txtDefaultOffLineMsg As TextBox = CType(Me.Grid.Rows(Me.Grid.EditIndex).Cells(Me.GRID_COL_DEFAULT_ON_LINE_MESSAGE_IDX).FindControl(Me.GRID_CTRL_OFF_LINE_MESSAGE_NAME), TextBox)

                If Me.State.AddingNewRow Then
                    Dim cboWebServiceFunctionsName As DropDownList = CType(Me.Grid.Rows(Me.Grid.EditIndex).Cells(Me.GRID_COL_WEB_SERVICE_FUNCTION_NAME_IDX).FindControl(Me.GRID_CTRL_WEB_SERVICE_FUNCTION_NAME), DropDownList)
                    Dim cboOnLineStatus As DropDownList = CType(Me.Grid.Rows(Me.Grid.EditIndex).Cells(Me.GRID_COL_ON_LINE_STATUS_IDX).FindControl(Me.GRID_CTRL_ON_LINE_STATUS_NAME), DropDownList)
                    Me.State.boWebServiceFunction.FunctionName = cboWebServiceFunctionsName.SelectedValue
                    Me.PopulateBOProperty(Me.State.boWebServiceFunction, "OffLineMessage", txtDefaultOffLineMsg)
                    Me.PopulateBOProperty(Me.State.boWebServiceFunction, "OnLineId", cboOnLineStatus)
                    Me.State.boWebServiceFunction.WebserviceId = Me.State.boWebservice.Id
                Else
                    Me.PopulateBOProperty(Me.State.boWebServiceFunction, "OffLineMessage", txtDefaultOffLineMsg)
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub
        Private Sub TranslateStatuses()
            Me.State.On_line_Translated = TranslationBase.TranslateLabelOrMessage("ON_LINE")
            Me.State.Off_line_Translated = TranslationBase.TranslateLabelOrMessage("OFF_LINE")
        End Sub
#End Region

#Region "Button event handlers"
        Protected Sub LoadGridData()
            Try
                Me.SetStateProperties()
                Me.State.PageIndex = 0
                Me.State.selectedWebServiceId = Guid.Empty
                Me.State.IsGridVisible = True
                Me.State.searchClick = True
                Me.State.searchDV = Nothing
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub NewButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewButton_WRITE.Click

            Try
                Me.State.IsEditMode = True
                Me.State.IsGridVisible = True
                Me.State.AddingNewRow = True
                AddNewWebServiceFunction()
                SetButtonsState()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try


                Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.boWebservice, False))
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub SaveButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton_WRITE.Click

            Try
                PopulateBOFromForm()
                If (Me.State.boWebServiceFunction.IsDirty) Then
                    Me.State.boWebServiceFunction.Save()
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

#Region "Helper functions"
        Private Sub SetButtonsState()

            If (Me.State.IsEditMode) Then
                ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, True)
                ControlMgr.SetVisibleControl(Me, CancelButton, True)
                ControlMgr.SetVisibleControl(Me, NewButton_WRITE, False)
                ControlMgr.SetVisibleControl(Me, Me.btnBack, False)

                Me.MenuEnabled = False
                If (Me.cboPageSize.Visible) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, False)
                End If
                'Linkbutton_panel.Enabled = False
            Else
                ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, False)
                ControlMgr.SetVisibleControl(Me, CancelButton, False)
                ControlMgr.SetVisibleControl(Me, NewButton_WRITE, True)
                ControlMgr.SetVisibleControl(Me, btnBack, True)
                Me.MenuEnabled = True
                If (Me.cboPageSize.Visible) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, True)
                End If
                'Linkbutton_panel.Enabled = True
            End If
        End Sub

        Private Sub ReturnFromEditing()

            Grid.EditIndex = NO_ROW_SELECTED_INDEX

            If Me.Grid.PageCount = 0 Then
                'if returning to the "1st time in" screen
                ControlMgr.SetVisibleControl(Me, Grid, False)
            Else
                ControlMgr.SetVisibleControl(Me, Grid, True)
            End If

            Me.State.IsEditMode = False
            Me.PopulateGrid()
            Me.State.PageIndex = Grid.PageIndex
            SetButtonsState()

        End Sub

        Protected Sub PopulateDropdowns()
            Dim YESNOList As DataElements.ListItem() =
                        CommonConfigManager.Current.ListManager.GetList(listCode:="YESNO",
                                                                languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
            Me.State.Yes_Id = (From lst In YESNOList
                               Where lst.Code = Codes.YESNO_Y
                               Select lst.ListItemId).FirstOrDefault()

            Me.State.No_Id = (From lst In YESNOList
                              Where lst.Code = Codes.YESNO_N
                              Select lst.ListItemId).FirstOrDefault()

            'Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            'Dim yesNoLkL As DataView = LookupListNew.DropdownLookupList("YESNO", langId, True)
            'Me.State.Yes_Id = LookupListNew.GetIdFromCode(yesNoLkL, Codes.YESNO_Y)
            'Me.State.No_Id = LookupListNew.GetIdFromCode(yesNoLkL, Codes.YESNO_N)
        End Sub

        'Private Sub GetStateProperties()
        '    Try
        '        'Me.txtWebServiceName.Text = Me.State.moWebServiceName
        '    Catch ex As Exception
        '        Me.HandleErrors(ex, Me.ErrControllerMaster)
        '    End Try
        'End Sub

        Private Sub SetStateProperties()
            Try
                If Me.State Is Nothing Then
                    Me.RestoreState(New MyState)
                End If
                'Me.State.moWebServiceName = Me.txtWebServiceName.Text
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub PopulateGrid()
            Try
                Dim oDataView As DataView
                Dim sortBy As String = "function_name"
                If (Me.State.searchDV Is Nothing) Then
                    Me.State.searchDV = WsFunctions.GetWsFunctions(Me.State.boWebservice.Id)
                    If Me.State.searchClick Then
                        Me.ValidSearchResultCount(Me.State.searchDV.Count, True)
                        Me.State.searchClick = False
                    End If
                End If

                If Not (Me.State.searchDV Is Nothing) Then
                    Me.State.searchDV.Sort = Me.SortDirection
                End If

                If (Me.State.IsAfterSave) Then
                    Me.State.IsAfterSave = False
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.WebServiceFunctionId, Me.Grid, Me.State.PageIndex)
                ElseIf (Me.State.IsEditMode) Then
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.WebServiceFunctionId, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)
                Else
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Guid.Empty, Me.Grid, Me.State.PageIndex)
                End If

                Grid.PageSize = State.PageSize

                Me.SortAndBindGrid()

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub


        Private Function GetGridDataView() As DataView
            If (Me.State.searchDV Is Nothing) Then
                Me.State.searchDV = WsFunctions.GetWsFunctions(Me.State.boWebservice.Id)
            End If

            Return Me.State.searchDV
        End Function

        Private Sub SortAndBindGrid()
            'Me.State.PageIndex = Me.Grid.CurrentPageIndex
            'Me.Grid.DataSource = Me.State.searchDV
            'HighLightSortColumn(Grid, Me.State.SortExpression)

            Me.State.PageIndex = Me.Grid.PageIndex
            If (Me.State.searchDV.Count = 0) Then

                Me.State.bnoRow = True
                'Me.Grid.DataSource = Nothing
                CreateHeaderForEmptyGrid(Grid, Me.SortDirection)
            Else
                Me.State.bnoRow = False
                Me.Grid.Enabled = True
                Me.Grid.DataSource = Me.State.searchDV
                HighLightSortColumn(Grid, Me.SortDirection)
                Me.Grid.DataBind()
            End If

            If Not Grid.BottomPagerRow Is Nothing AndAlso Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

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

        Private Sub AddNewWebServiceFunction()

            Dim dv As DataView

            Me.State.searchDV = GetGridDataView()

            Me.State.boWebServiceFunction = New Assurant.ElitaPlus.BusinessObjectsNew.WsFunctions
            Me.State.WebServiceFunctionId = Me.State.boWebServiceFunction.Id

            Me.State.searchDV = Me.State.boWebServiceFunction.GetNewDataViewRow(Me.State.searchDV, Me.State.WebserviceId, Me.State.boWebServiceFunction.Id)

            Grid.DataSource = Me.State.searchDV

            Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.WebServiceFunctionId, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)

            Grid.DataBind()

            Me.State.PageIndex = Grid.PageIndex

            SetGridControls(Me.Grid, False)

            'Set focus on the Description TextBox for the EditItemIndex row
            Me.SetFocusOnEditableDropDownFieldInGrid(Me.Grid, Me.GRID_COL_ON_LINE_STATUS_IDX, Me.GRID_CTRL_ON_LINE_STATUS_NAME, Me.Grid.EditIndex)

            Me.PopulateFormFromBO(True)

            'Me.TranslateGridControls(Grid)
            Me.SetButtonsState()
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
        End Sub

        Private Sub PopulateFormFromBO(Optional ByVal blnAddingNew As Boolean = False)

            Dim gridRowIdx As Integer = Me.Grid.EditIndex

            Dim txtOffLineMessage As TextBox = CType(Me.Grid.Rows(gridRowIdx).Cells(Me.GRID_COL_DEFAULT_ON_LINE_MESSAGE_IDX).FindControl(Me.GRID_CTRL_OFF_LINE_MESSAGE_NAME), TextBox)
            Dim cboOnLineStatus As DropDownList = CType(Me.Grid.Rows(Me.Grid.EditIndex).Cells(Me.GRID_COL_ON_LINE_STATUS_IDX).FindControl(Me.GRID_CTRL_ON_LINE_STATUS_NAME), DropDownList)

            Dim cboWebServiceNames As DropDownList = CType(Me.Grid.Rows(gridRowIdx).Cells(Me.GRID_COL_WEB_SERVICE_FUNCTION_NAME_IDX).FindControl(Me.GRID_CTRL_WEB_SERVICE_FUNCTION_NAME), DropDownList)


            Dim btnActionStart As Button = CType(Me.Grid.Rows(gridRowIdx).Cells(Me.GRID_COL_STOP_START_BUTTON_IDX).FindControl(Me.ACTION_BUTTON_CONTROL_START_NAME), Button)
            Dim btnActionStop As Button = CType(Me.Grid.Rows(gridRowIdx).Cells(Me.GRID_COL_STOP_START_BUTTON_IDX).FindControl(Me.ACTION_BUTTON_CONTROL_STOP_NAME), Button)
            Dim imgImageButton As ImageButton = CType(Me.Grid.Rows(gridRowIdx).Cells(Me.GRID_COL_IMAGE_BUTTON_EDIT_IDX).FindControl(Me.ACTION_IMAGE_BUTTON_CONTROL_EDIT_NAME), ImageButton)
            btnActionStart.Visible = False
            btnActionStop.Visible = False
            imgImageButton.Visible = False

            txtWebServiceName.Width = Unit.Pixel(250)
            txtOffLineMessage.Width = Unit.Pixel(250)
            cboOnLineStatus.Width = Unit.Pixel(50)
            txtOffLineMessage.Text = txtDefaultMsgFromLastRow
            txtOffLineMessage.Width = Unit.Pixel(250)


            Try
                If blnAddingNew Then
                    Dim webServiceFunctionsNames As DataView = XMLHelper.GetWebServiceFunctionsNames(Me.State.boWebservice.WebServiceName.ToUpper).Tables(0).DefaultView
                    Dim i As Integer
                    cboWebServiceNames.Items.Clear()
                    cboWebServiceNames.Items.Add(New ListItem("", ""))

                    If Not webServiceFunctionsNames Is Nothing Then
                        For i = 0 To webServiceFunctionsNames.Count - 1
                            cboWebServiceNames.Items.Add(New ListItem(webServiceFunctionsNames(i)("DESCRIPTION").ToString, webServiceFunctionsNames(i)("CODE").ToString))
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
                    Dim lblOnLineStatusCtrl As Label = CType(Me.Grid.Rows(gridRowIdx).Cells(GRID_COL_ON_LINE_STATUS_IDX).FindControl(Me.GRID_CTRL_ON_LINE_STATUS_LABEL_EDIT_NAME), Label)
                    If lblOnLineStatusCtrl.Text.Equals("Y") Then
                        lblOnLineStatusCtrl.Text = Me.State.On_line_Translated
                    ElseIf lblOnLineStatusCtrl.Text.Equals("N") Then
                        lblOnLineStatusCtrl.Text = Me.State.Off_line_Translated
                    End If
                    cboWebServiceNames.Visible = False
                    cboOnLineStatus.Visible = False
                    SetFocus(txtOffLineMessage)
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Public Sub SaveBo(ByVal strStartOrStopID As Guid)
            Try
                Me.State.boWebServiceFunction.OnLineId = strStartOrStopID
                Me.State.boWebServiceFunction.LastOperationDate = Date.Now
                If (Me.State.boWebServiceFunction.IsDirty) Then
                    Me.State.boWebServiceFunction.Save()

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
#End Region

#Region "Grid related"

        Private Sub Grid_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.PageIndexChanged
            Try
                Me.State.PageIndex = Grid.PageIndex
                Me.State.selectedWebServiceId = Guid.Empty
                PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                Grid.PageIndex = e.NewPageIndex
                State.PageIndex = Grid.PageIndex
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Public Sub RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand

            Try
                Dim index As Integer

                If (e.CommandName = Me.EDIT_COMMAND) Then
                    index = CInt(e.CommandArgument)
                    'Set the IsEditMode flag to TRUE
                    Me.State.IsEditMode = True
                    Me.State.WebServiceFunctionId = New Guid(CType(Grid.Rows(index).Cells(GRID_COL_WEB_SERVICE_FUNCTION_ID_IDX).FindControl(Me.GRID_CTRL_WEBSERVICE_FUNCTION_ID), Label).Text)
                    Me.State.boWebServiceFunction = New Assurant.ElitaPlus.BusinessObjectsNew.WsFunctions(Me.State.WebServiceFunctionId)
                    Me.PopulateGrid()

                    Me.State.PageIndex = Grid.PageIndex
                    'Disable all Edit and Delete icon buttons on the Grid
                    SetGridControls(Me.Grid, False)
                    'Set focus on the Description TextBox for the EditItemIndex row
                    Me.SetFocusOnEditableFieldInGrid(Me.Grid, Me.GRID_COL_DEFAULT_ON_LINE_MESSAGE_IDX, Me.GRID_CTRL_OFF_LINE_MESSAGE_NAME, index)
                    Me.PopulateFormFromBO()
                    Me.SetButtonsState()

                ElseIf e.CommandName = Me.STOP_COMMAND Then
                    index = CInt(e.CommandArgument)
                    Me.State.IsEditMode = True
                    Me.State.WebServiceFunctionId = New Guid(CType(Grid.Rows(index).Cells(GRID_COL_WEB_SERVICE_FUNCTION_ID_IDX).FindControl(Me.GRID_CTRL_WEBSERVICE_FUNCTION_ID), Label).Text)
                    Me.State.boWebServiceFunction = New Assurant.ElitaPlus.BusinessObjectsNew.WsFunctions(Me.State.WebServiceFunctionId)

                    Me.State.PageIndex = Grid.PageIndex
                    Me.SaveBo(Me.State.No_Id)

                ElseIf e.CommandName = Me.START_COMMAND Then
                    index = CInt(e.CommandArgument)
                    Me.State.IsEditMode = True
                    Me.State.WebServiceFunctionId = New Guid(CType(Grid.Rows(index).Cells(GRID_COL_WEB_SERVICE_FUNCTION_ID_IDX).FindControl(Me.GRID_CTRL_WEBSERVICE_FUNCTION_ID), Label).Text)
                    Me.State.boWebServiceFunction = New Assurant.ElitaPlus.BusinessObjectsNew.WsFunctions(Me.State.WebServiceFunctionId)
                    Me.State.PageIndex = Grid.PageIndex
                    Me.SaveBo(Me.State.Yes_Id)

                ElseIf (e.CommandName = Me.DELETE_COMMAND) Then
                    index = CInt(e.CommandArgument)
                    'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                    Grid.SelectedIndex = Me.NO_ROW_SELECTED_INDEX

                    'Save the Id in the Session

                    Me.State.WebServiceFunctionId = New Guid(CType(Grid.Rows(index).Cells(GRID_COL_WEB_SERVICE_FUNCTION_ID_IDX).FindControl(Me.GRID_CTRL_WEBSERVICE_FUNCTION_ID), Label).Text)
                    Me.State.boWebServiceFunction = New Assurant.ElitaPlus.BusinessObjectsNew.WsFunctions(Me.State.WebServiceFunctionId)

                    Try
                        Me.State.boWebServiceFunction.Delete()
                        'Call the Save() method in the Manufacturer Business Object here
                        Me.State.boWebServiceFunction.Save()
                        Me.State.IsAfterSave = True
                        Me.AddInfoMsg(Me.MSG_RECORD_DELETED_OK)
                        Me.State.searchDV = Nothing
                        Me.ReturnFromEditing()
                        Me.State.PageIndex = Grid.PageIndex
                    Catch ex As Exception
                        Me.State.boWebServiceFunction.RejectChanges()
                        Throw ex
                    End Try

                    Me.State.PageIndex = Grid.PageIndex

                ElseIf ((e.CommandName = Me.SORT_COMMAND) AndAlso Not (Me.IsEditing)) Then

                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Try
                If Not dvRow Is Nothing And Me.State.searchDV.Count > 0 Then
                    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                        CType(e.Row.Cells(Me.GRID_COL_WEB_SERVICE_FUNCTION_ID_IDX).FindControl(Me.GRID_CTRL_WEBSERVICE_FUNCTION_ID), Label).Text = GetGuidStringFromByteArray(CType(dvRow("ws_function_id"), Byte()))

                        If Not dvRow("last_operation_date").Equals(DBNull.Value) Then
                            Dim lastOperationDate As Date = CType(dvRow(Me.State.boWebServiceFunction.WsFunctionsSearchDV.COL_LAST_OPERATION_DATE), Date)
                            'Sridhar CType(e.Row.Cells(Me.GRID_COL_LAST_OPERATION_DATE_IDX).FindControl(Me.GRID_CTRL_LAST_OPERATION_DATE_LABLE_NAME), Label).Text = Me.GetLongDateFormattedString(lastOperationDate)
                            CType(e.Row.Cells(Me.GRID_COL_LAST_OPERATION_DATE_IDX).FindControl(Me.GRID_CTRL_LAST_OPERATION_DATE_LABLE_NAME), Label).Text = GetLongDateFormattedString(lastOperationDate)
                        End If

                        CType(e.Row.Cells(Me.GRID_COL_USER_IDX).FindControl(GRID_CTRL_LAST_CHANGE_BY_LABEL_NAME), Label).Text = dvRow("last_change_by").ToString

                        If (Me.State.IsEditMode = True AndAlso Me.State.WebServiceFunctionId.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow("ws_function_id"), Byte())))) Then
                            CType(e.Row.Cells(Me.GRID_COL_DEFAULT_ON_LINE_MESSAGE_IDX).FindControl(Me.GRID_CTRL_OFF_LINE_MESSAGE_NAME), TextBox).Text = dvRow("off_line_message").ToString
                            If Not Me.State.AddingNewRow Then txtDefaultMsgFromLastRow = dvRow("off_line_message").ToString
                            CType(e.Row.Cells(GRID_COL_WEB_SERVICE_FUNCTION_NAME_IDX).FindControl(Me.GRID_CTRL_WEB_SERVICE_FUNCTION_LABEL_EDIT_NAME), Label).Text = dvRow("function_name").ToString
                            CType(e.Row.Cells(Me.GRID_COL_ON_LINE_STATUS_IDX).FindControl(Me.GRID_CTRL_ON_LINE_STATUS_LABEL_EDIT_NAME), Label).Text = dvRow("on_line").ToString
                        Else
                            CType(e.Row.Cells(Me.GRID_COL_ON_LINE_STATUS_IDX).FindControl(Me.GRID_CTRL_ON_LINE_STATUS_LABEL_NAME), Label).Text = dvRow("on_line").ToString
                            CType(e.Row.Cells(GRID_COL_WEB_SERVICE_FUNCTION_NAME_IDX).FindControl(Me.GRID_CTRL_WEB_SERVICE_FUNCTION_LABEL_NAME), Label).Text = dvRow("function_name").ToString
                            CType(e.Row.Cells(Me.GRID_COL_DEFAULT_ON_LINE_MESSAGE_IDX).FindControl(GRID_CTRL_OFF_LINE_MESSAGE_LABEL_NAME), Label).Text = dvRow("off_line_message").ToString
                            Dim btnActionStart As Button = CType(e.Row.Cells(Me.GRID_COL_STOP_START_BUTTON_IDX).FindControl(Me.ACTION_BUTTON_CONTROL_START_NAME), Button)
                            Dim btnActionStop As Button = CType(e.Row.Cells(Me.GRID_COL_STOP_START_BUTTON_IDX).FindControl(Me.ACTION_BUTTON_CONTROL_STOP_NAME), Button)
                            Dim imgImageButton As ImageButton = CType(e.Row.Cells(Me.GRID_COL_IMAGE_BUTTON_EDIT_IDX).FindControl(Me.ACTION_IMAGE_BUTTON_CONTROL_EDIT_NAME), ImageButton)
                            Dim imgDeleteImageButton As ImageButton = CType(e.Row.Cells(Me.GRID_COL_IMAGE_BUTTON_EDIT_IDX).FindControl(Me.ACTION_IMAGE_BUTTON_CONTROL_DELETE_NAME), ImageButton)

                            Dim lblOnLineStatusCtrl As Label = CType(e.Row.Cells(GRID_COL_ON_LINE_STATUS_IDX).FindControl(Me.GRID_CTRL_ON_LINE_STATUS_LABEL_NAME), Label)
                            Dim lblWebServiceNameCtrl As Label = CType(e.Row.Cells(GRID_COL_WEB_SERVICE_FUNCTION_NAME_IDX).FindControl(Me.GRID_CTRL_WEB_SERVICE_FUNCTION_LABEL_NAME), Label)

                            If Me.State.AddingNewRow Then
                                btnActionStart.Visible = False
                                btnActionStop.Visible = False
                                imgImageButton.Visible = False
                                imgDeleteImageButton.Visible = False

                                If e.Row.RowIndex = Me.State.searchDV.Count - 2 Then txtDefaultMsgFromLastRow = CType(e.Row.Cells(Me.GRID_COL_DEFAULT_ON_LINE_MESSAGE_IDX).FindControl(Me.GRID_CTRL_OFF_LINE_MESSAGE_LABEL_NAME), Label).Text
                                If e.Row.RowIndex < Me.State.searchDV.Count - 1 Then e.Row.Enabled = False
                            Else
                                If lblOnLineStatusCtrl.Text.Equals("Y") Then
                                    btnActionStart.Visible = False
                                    btnActionStop.Visible = True
                                    lblOnLineStatusCtrl.Text = Me.State.On_line_Translated
                                ElseIf lblOnLineStatusCtrl.Text.Equals("N") Then
                                    btnActionStart.Visible = True
                                    btnActionStop.Visible = False
                                    lblOnLineStatusCtrl.Text = Me.State.Off_line_Translated

                                End If
                            End If
                        End If
                    End If
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(ByVal grid As GridView, ByVal cellPosition As Integer, ByVal controlName As String, ByVal itemIndex As Integer)
            'Set focus on the Description TextBox for the EditItemIndex row
            Dim txtDefaultMsg As TextBox = CType(grid.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
            SetFocus(txtDefaultMsg)
        End Sub
        Private Sub SetFocusOnEditableDropDownFieldInGrid(ByVal grid As GridView, ByVal cellPosition As Integer, ByVal controlName As String, ByVal itemIndex As Integer)
            'Set focus on the Description TextBox for the EditItemIndex row
            Dim cboDefaultMsg As DropDownList = CType(grid.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), DropDownList)
            SetFocus(cboDefaultMsg)
        End Sub

        Public Sub RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
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
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub cboPageSize_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                State.PageSize = CType(cboPageSize.SelectedValue, Integer)
                Me.State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.PageSize)
                Me.Grid.PageIndex = Me.State.PageIndex
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Public Property SortDirection() As String
            Get
                Return ViewState("SortDirection").ToString
            End Get
            Set(ByVal value As String)
                ViewState("SortDirection") = value
            End Set
        End Property

#End Region


    End Class
End Namespace