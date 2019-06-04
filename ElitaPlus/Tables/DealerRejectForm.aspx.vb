Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Imports Microsoft.VisualBasic

Public Class DealerRejectForm
    Inherits ElitaPlusSearchPage
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button
    Protected WithEvents Button2 As System.Web.UI.WebControls.Button
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label

    Protected csFamilyBO As DealerRejectCode

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

#Region "Constants "
    Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
    Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
    Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"
    Private Const GRID_CTL_DEALER_REJECT_CODE_ID As String = "lblDealerRejectCodeId"
    Private Const GRID_CTL_SELECTED_CHKBOX As String = "CheckBoxItemSel"
    Private Const GRID_CTL_REJECT_CODE As String = "lblDealerRejCode"
    Private Const GRID_CTL_REJECT_REASON As String = "lblDealerRejReason"
    Private Const GRID_CTL_DEALER_ID As String = "lblDealerId"
    Private Const GRID_CTL_MSG_CODE_ID As String = "lblMsgCodeId"
    Private Const GRID_CTL_RECORD_TYPE_ID As String = "lblRecordTypeId"

    Private Const GRID_COL_SELECTED_IDX As Integer = 0
    Private Const GRID_COL_SELECTED_CHK_IDX As Integer = 1
    Private Const GRID_COL_REJECT_CODE_IDX As Integer = 2
    Private Const GRID_COL_REJECT_REASON_IDX As Integer = 3
    Private Const GRID_COL_DEALER_ID_IDX As Integer = 4
    Private Const GRID_COL_MSG_CODE_ID_IDX As Integer = 5
    Private Const GRID_COL_RECORD_TYPE_ID_IDX As Integer = 6


    Private Const LABEL_SELECT_DEALERCODE As String = "DEALER"

    Private IsReturningFromChild As Boolean = False

    Public Const SELECT_ACTION_COMMAND As String = "SelectAction"
    Public Const URL As String = "~/Tables/DealerRejectForm.aspx"

    Public Const MSG_CODE_DLR_REJECT As String = "DLREJECT"
    Public Const MSG_CODE_DLR_PYMT_REJECT As String = "DLPAYREJECT"

    Private Const NO_ROW_SELECTED_INDEX As Integer = -1

    Dim permType As FormAuthorization.enumPermissionType
#End Region

#Region "Page State"
    Class MyState
        Public DealerRejectCodeBO As DealerRejectCode
        Public IsAfterSave As Boolean
        Public IsDisabled As Boolean = False
        Public IsEditMode As Boolean = False

        Public PageIndex As Integer = 0
        Public PageSize As Integer = DEFAULT_NEW_UI_PAGE_SIZE

        Public selectedMsgCodeId As Guid = Guid.Empty
        Public RejectCodeMask As String
        Public RejectReasonMask As String
        Public DealerId As Guid = Guid.Empty
        Public RecordTypeId As Guid = Guid.Empty
        Public MsgCodeId As Guid = Guid.Empty
        Public RejectMsgTypeId As Guid = Guid.Empty
        Public IsGridVisible As Boolean
        Public searchDV As DealerRejectCode.DealerRejectCodeSearchDV = Nothing
        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        Public HasDataChanged As Boolean
        Public bnoRow As Boolean = False
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
    End Class

    Public Sub New()
        MyBase.New(New MyState())
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            Return CType(MyBase.State, MyState)
        End Get
    End Property
#End Region

#Region "Properties"

    Public ReadOnly Property DealerMultipleDrop() As MultipleColumnDDLabelControl
        Get
            If moDealerMultipleDrop Is Nothing Then
                moDealerMultipleDrop = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
            End If
            Return moDealerMultipleDrop
        End Get
    End Property

#End Region

#Region "Page Events"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Try
            Me.UpdateBreadCrum()
            If Not Me.IsPostBack Then
                ClearGridHeaders(Me.DataGridRejectCode)
                'Me.ShowMissingTranslations(ErrorControl)
                Me.SetGridItemStyleColor(Me.DataGridRejectCode)
                Me.PopulateDropdown()
                Me.SetControlState()
                cboPageSize.SelectedValue = Me.State.PageSize.ToString()
            End If

            Me.btnAdd_WRITE.Visible = False
            Me.btnBack.Visible = False

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub UpdateBreadCrum()
        Me.MasterPage.UsePageTabTitleInBreadCrum = False
        Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("DEALER") & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("DEALER_REJECT_CODE")
        Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("DEALER_REJECT_CODE")
        Me.MasterPage.MessageController.Clear()
        Me.MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage("TABLES") & ElitaBase.Sperator & Me.MasterPage.PageTab
    End Sub

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                '' ''Me.State.claimId = CType(CallingPar, Guid)
                '' ''Me.State.ClaimBO = New Claim(Me.State.claimId)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub


    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        Try
            Me.MenuEnabled = True
            Me.IsReturningFromChild = True
            Dim retObj As DealerRejectForm.ReturnType = CType(ReturnPar, DealerRejectForm.ReturnType)
            If Not retObj Is Nothing AndAlso retObj.BoChanged Then
                Me.State.searchDV = Nothing
            End If
            Select Case retObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Back
                    If Not retObj Is Nothing Then
                        If Not retObj.EditingBo.IsNew Then
                            Me.State.selectedMsgCodeId = retObj.EditingBo.Id
                        End If
                        Me.State.IsGridVisible = True
                    End If
            End Select
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub


#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As DealerRejectCode
        Public BoChanged As Boolean = False
        Public HasDataChanged As Boolean
        Public dealerId As Guid = Guid.Empty

        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As DealerRejectCode, Optional ByVal boChanged As Boolean = False)
            Me.LastOperation = LastOp
            Me.EditingBo = curEditingBo
            Me.BoChanged = boChanged
        End Sub

    End Class
#End Region

#End Region

#Region "Datagrid Related "
    Private Sub SetControlState()

        If (Me.State.IsEditMode) Then
            ControlMgr.SetEnableControl(Me, btnCancel, True)
            ControlMgr.SetEnableControl(Me, btnSave, True)
            ControlMgr.SetEnableControl(Me, mobtnSearch, False)
            ControlMgr.SetEnableControl(Me, mobtnClearSearch, False)
            Me.MenuEnabled = False
            If Me.DataGridRejectCode.PageCount > 1 Then
                Me.DataGridRejectCode.PagerStyle.Visible = False
            End If
        Else
            ControlMgr.SetEnableControl(Me, btnCancel, False)
            ControlMgr.SetEnableControl(Me, btnSave, False)
            ControlMgr.SetEnableControl(Me, mobtnSearch, True)
            ControlMgr.SetEnableControl(Me, mobtnClearSearch, True)
            Me.MenuEnabled = True
            If Me.DataGridRejectCode.PageCount > 1 Then
                Me.DataGridRejectCode.PagerStyle.Visible = True
            End If
        End If

    End Sub

    Public Sub OnCheckedChangeEvent(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.State.IsEditMode = True
        Me.SetControlState()
    End Sub

    Private Sub DataGridRejectCode_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridRejectCode.ItemCreated
        Try
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                AddHandler CType(e.Item.Cells(GRID_COL_SELECTED_CHK_IDX).FindControl(GRID_CTL_SELECTED_CHKBOX), CheckBox).CheckedChanged, AddressOf OnCheckedChangeEvent
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub ItemBound(ByVal source As Object, ByVal e As DataGridItemEventArgs) Handles DataGridRejectCode.ItemDataBound
        Try
            BaseItemBound(source, e)

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.EditItem Then
                Dim drv As DataRowView = CType(e.Item.DataItem, DataRowView)
                Dim i As Integer
                Dim disableRow As Boolean = False

                If Not drv(DALObjects.DealerRejectCodeDAL.COL_NAME_DEALER_REJECT_CODE_ID) Is System.DBNull.Value Then
                    CType(e.Item.Cells(GRID_COL_SELECTED_CHK_IDX).FindControl(GRID_CTL_SELECTED_CHKBOX), CheckBox).Checked = True
                Else
                    CType(e.Item.Cells(GRID_COL_SELECTED_CHK_IDX).FindControl(GRID_CTL_SELECTED_CHKBOX), CheckBox).Checked = False
                End If
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub


    Public Sub DataGridRejectCode_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGridRejectCode.ItemCommand
        Try
            If e.CommandName = SELECT_ACTION_COMMAND Then
                If Not e.CommandArgument.ToString().Equals(String.Empty) Then
                    Me.State.selectedMsgCodeId = New Guid(e.CommandArgument.ToString())
                    Me.callPage(DealerRejectForm.URL, Me.State.selectedMsgCodeId)
                End If
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            If (TypeOf ex Is System.Reflection.TargetInvocationException) AndAlso
           (TypeOf ex.InnerException Is Threading.ThreadAbortException) Then Return
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub ItemCreated(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub DataGridRejectCode_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridRejectCode.PageIndexChanged
        Try
            Me.DataGridRejectCode.CurrentPageIndex = e.NewPageIndex
            Me.State.PageIndex = Me.DataGridRejectCode.CurrentPageIndex
            PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub EnableDisableControl(ByVal ctl As System.Web.UI.Control, Optional ByVal disabled As Boolean = False)
        'If Not (permType = FormAuthorization.enumPermissionType.EDIT) Or disabled Then
        '    ControlMgr.SetEnableControl(Me, CType(ctl, System.Web.UI.WebControls.WebControl), False)
        'Else
        '    ControlMgr.SetEnableControl(Me, CType(ctl, System.Web.UI.WebControls.WebControl), True)
        'End If
        ControlMgr.SetEnableControl(Me, CType(ctl, System.Web.UI.WebControls.WebControl), disabled)
    End Sub

#End Region

#Region "Button Clicks"
    Protected Sub SaveChanges()
        Dim i, j, k As Integer
        Dim DataChanged As Boolean = False
        'Dim retVal As Integer
        Dim isFirstBO As Boolean = True
        Dim selectedCount As Integer = 0
        Dim emptyDealerId As Boolean = False
        'Dim count As Integer


        'ErrorControl.Clear_Hide()
        ClearGridHeaders(Me.DataGridRejectCode)

        Try

            For i = 0 To Me.DataGridRejectCode.Items.Count - 1
                Dim selected As Boolean = False
                Dim isNew As String = ""

                selected = CType(DataGridRejectCode.Items(i).Cells(GRID_COL_SELECTED_CHK_IDX).FindControl(GRID_CTL_SELECTED_CHKBOX), CheckBox).Checked()

                Dim dealerRejectCodeId As Guid = Guid.Empty
                If Not CType(DataGridRejectCode.Items(i).Cells(GRID_COL_SELECTED_IDX).FindControl(GRID_CTL_DEALER_REJECT_CODE_ID), Label).Text = "" Then
                    dealerRejectCodeId = New Guid(CType(DataGridRejectCode.Items(i).Cells(GRID_COL_SELECTED_IDX).FindControl(GRID_CTL_DEALER_REJECT_CODE_ID), Label).Text)
                End If

                If (Not dealerRejectCodeId.Equals(Guid.Empty)) Then
                    isNew = "N"
                Else
                    isNew = "Y"
                End If

                Dim dealerId As Guid = Guid.Empty
                If Not CType(DataGridRejectCode.Items(i).Cells(GRID_COL_SELECTED_IDX).FindControl(GRID_CTL_DEALER_ID), Label).Text = "" Then
                    dealerId = New Guid(CType(DataGridRejectCode.Items(i).Cells(GRID_COL_SELECTED_IDX).FindControl(GRID_CTL_DEALER_ID), Label).Text)
                End If

                Dim msgCodeId As Guid = Guid.Empty
                If Not CType(DataGridRejectCode.Items(i).Cells(GRID_COL_SELECTED_IDX).FindControl(GRID_CTL_MSG_CODE_ID), Label).Text = "" Then
                    msgCodeId = New Guid(CType(DataGridRejectCode.Items(i).Cells(GRID_COL_SELECTED_IDX).FindControl(GRID_CTL_MSG_CODE_ID), Label).Text)
                End If

                Dim recordTypeId As Guid = Guid.Empty
                If Not CType(DataGridRejectCode.Items(i).Cells(GRID_COL_SELECTED_IDX).FindControl(GRID_CTL_RECORD_TYPE_ID), Label).Text = "" Then
                    recordTypeId = New Guid(CType(DataGridRejectCode.Items(i).Cells(GRID_COL_SELECTED_IDX).FindControl(GRID_CTL_RECORD_TYPE_ID), Label).Text)
                End If


                If selected Then
                    ' Checkbox selected
                    selectedCount = selectedCount + 1

                    If (isNew = "Y") Then
                        ' Record needs to be inserted
                        Dim curBO As DealerRejectCode
                        curBO = GetDealerRejectCodeBO(isFirstBO, Guid.Empty)

                        curBO.DealerId = Me.State.DealerId
                        curBO.MsgCodeId = msgCodeId
                        curBO.RecordTypeId = recordTypeId

                        curBO.Validate()
                        DataChanged = True
                        isFirstBO = False
                    End If
                Else
                    ' Checkbox deselected
                    If (isNew = "N") Then
                        ' Record exist for deletion

                        Dim curBO As DealerRejectCode

                        If (Not dealerRejectCodeId.Equals(Guid.Empty)) Then
                            curBO = GetDealerRejectCodeBO(isFirstBO, dealerRejectCodeId)
                        Else
                            curBO = GetDealerRejectCodeBO(isFirstBO, Guid.Empty)
                        End If

                        curBO.Validate()
                        curBO.Delete()

                        DataChanged = True
                        isFirstBO = False
                    End If
                End If
            Next

            If DataChanged Then
                csFamilyBO.Save()
                Me.State.searchDV = Nothing
                Me.State.IsEditMode = False
                'EnableDisableControl(Me.btnBack, False)
                ' EnableDisableControl(Me.NewButton_WRITE, False) 'DEF-1333
                Me.SetGridItemStyleColor(Me.DataGridRejectCode)

                PopulateGrid()
                Me.AddInfoMsg(MSG_RECORD_SAVED_OK)
            Else
                Me.AddInfoMsg(MSG_RECORD_NOT_SAVED)
            End If

            csFamilyBO = Nothing

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Private Function GetDealerRejectCodeBO(ByVal isFirstBO As Boolean, ByVal dealerRejectCodeId As Guid) As DealerRejectCode

        If isFirstBO Then
            If dealerRejectCodeId.Equals(Guid.Empty) Then
                csFamilyBO = New DealerRejectCode
            Else
                csFamilyBO = New DealerRejectCode(dealerRejectCodeId)
            End If

            Return csFamilyBO
        Else

            Return csFamilyBO.AddDealerRejectCode(dealerRejectCodeId)
        End If

    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            'Me.ErrorControl.Clear_Hide()
            Me.SaveChanges()
            'Me.ErrorControl.Show()
            Me.State.IsEditMode = False
            Me.SetControlState()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            'Me.ErrorControl.Clear_Hide()
            Me.State.IsEditMode = False
            Me.SetControlState()
            PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub moBtnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mobtnSearch.Click
        Try
            If (Not Me.State.DealerRejectCodeBO Is Nothing AndAlso Me.State.DealerRejectCodeBO.IsDirty) Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Undo
            Else
                'Me.ErrorControl.Clear_Hide()
                Me.SetStateProperties()

                Me.State.PageIndex = 0
                Me.State.selectedMsgCodeId = Guid.Empty
                Me.State.IsGridVisible = True
                Me.State.searchDV = Nothing
                Me.State.HasDataChanged = False
                Me.State.IsEditMode = False
                Me.SetControlState()
                cboPageSize.SelectedValue = CType(State.PageSize, String)
                Me.PopulateGrid()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub moBtnClearSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mobtnClearSearch.Click
        'Me.ErrorControl.Clear_Hide()
        ClearSearchCriteria()
        Me.State.IsEditMode = False
        Me.SetControlState()
    End Sub

    Private Sub ClearSearchCriteria()

        Try
            Me.ClearStateValues()
            ' Clear all search options typed or selected by the user
            Me.ClearAllSearchOptions()

            ' Update the Bo state properties with the new value
            Me.SetStateProperties()

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub SetStateProperties()

        Try
            If Me.State Is Nothing Then
                Trace(Me, "Restoring State")
                Me.RestoreState(New MyState)
            End If

            Me.ClearStateValues()

            Me.State.DealerId = Me.DealerMultipleDrop.SelectedGuid()
            Me.State.RecordTypeId = GetSelectedItem(Me.ddlRecordType)
            Me.State.RejectCodeMask = Me.TextboxRejectCode.Text.ToUpper.Trim
            Me.State.RejectReasonMask = Me.TextboxRejectReason.Text.ToUpper.Trim
            Me.State.RejectMsgTypeId = GetSelectedItem(ddlRectionMsgType)

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub ClearStateValues()
        Try
            'clear State

            Me.State.DealerId = Guid.Empty
            Me.State.RecordTypeId = Guid.Empty
            Me.State.RejectCodeMask = String.Empty
            Me.State.RejectReasonMask = String.Empty
            Me.State.RejectMsgTypeId = Guid.Empty

            Me.State.IsGridVisible = True
            Me.State.searchDV = Nothing

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Protected Sub ClearAllSearchOptions()

        Me.DealerMultipleDrop.SelectedIndex = BLANK_ITEM_SELECTED

        Me.TextboxRejectCode.Text = String.Empty
        Me.TextboxRejectReason.Text = String.Empty

        ddlRecordType.SelectedIndex = NO_ITEM_SELECTED_INDEX
        ddlRectionMsgType.SelectedIndex = NO_ITEM_SELECTED_INDEX

        Me.State.PageIndex = Me.DataGridRejectCode.CurrentPageIndex
        Me.DataGridRejectCode.DataSource = Me.State.searchDV
        Me.DataGridRejectCode.DataBind()

    End Sub

    Public Function CheckNull(ByVal val As Object) As String
        Dim retStr As String = ""

        If Not val Is DBNull.Value Then
            If val.GetType Is GetType(Byte()) Then
                retStr = GetGuidStringFromByteArray(CType(val, Byte()))
            ElseIf val.GetType Is GetType(BooleanType) Then
                If CType(val, BooleanType).Value Then
                    retStr = "Y"
                Else
                    retStr = "N"
                End If
            ElseIf val.GetType Is GetType(Guid) Then
                If Not val.Equals(Guid.Empty) Then
                    retStr = GetGuidStringFromByteArray(CType(val, Guid).ToByteArray)
                End If
            ElseIf val.GetType Is GetType(DateType) Then
                retStr = CType(val.ToString, DateTime).ToString
            End If
        End If

        Return retStr
    End Function

#End Region

#Region "Controlling Logic"

    Private Sub PopulateDropdown()
        DealerMultipleDrop.Caption = "* " + TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALERCODE)
        DealerMultipleDrop.NothingSelected = True

        DealerMultipleDrop.BindData(LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies))
        DealerMultipleDrop.AutoPostBackDD = True

        'If LookupListNew.GetCodeFromId(LookupListNew.LK_MSG_TYPE, Me.State.RejectMsgTypeId) = MSG_CODE_DLR_REJECT Then
        '    BindListControlToDataView(Me.ddlRecordType, LookupListNew.GetRecordTypeLookupList(Authentication.LangId, True))
        'ElseIf (LookupListNew.GetCodeFromId(LookupListNew.LK_MSG_TYPE, Me.State.RejectMsgTypeId) = MSG_CODE_DLR_PYMT_REJECT) Then
        '    ddlRecordType.Items.Add("A")
        '    ddlRecordType.Items.Add("C")
        '    ddlRecordType.Items.Add("D")
        '    ddlRecordType.Items.Add("U")
        'End If

        'BindListControlToDataView(ddlRectionMsgType, LookupListNew.GetRejectionMsgTypesLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId))
        ddlRectionMsgType.Populate(CommonConfigManager.Current.ListManager.GetList("MSGTYPE", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
            {
                .AddBlankItem = True
            })

        ''Setting the selected value for Record Type Drop Down to FC record. Cannot be modified by the user.
        'SetSelectedItem(Me.ddlRecordType, LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList("RECTYP", ElitaPlusIdentity.Current.ActiveUser.LanguageId), "FC"))
        'Me.EnableDisableControl(Me.ddlRecordType, False)
    End Sub

    Protected Sub PopulateGrid()

        If (ddlRectionMsgType.Items.Count > 0 AndAlso ddlRectionMsgType.SelectedIndex <= 0) Then
            Me.State.searchDV = Nothing
            DataGridRejectCode.DataSource = Nothing
            DataGridRejectCode.DataBind()
            Throw New GUIException(Message.MSG_INVALID_SERVICE_CENTER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_REJ_MESSAGE_TYPE_MUST_BE_SELECTED_ERR)
        End If

        If (ddlRecordType.Items.Count > 0 AndAlso ddlRecordType.SelectedIndex <= 0) Then
            Me.State.searchDV = Nothing
            DataGridRejectCode.DataSource = Nothing
            DataGridRejectCode.DataBind()
            Throw New GUIException(Message.MSG_INVALID_SERVICE_CENTER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_RECORD_TYPE_MUST_BE_SELECTED_ERR)
        End If

        If ((Me.State.searchDV Is Nothing) OrElse (Me.State.HasDataChanged)) Then
            Me.State.searchDV = DealerRejectCode.GetList(ElitaPlusIdentity.Current.ActiveUser.LanguageId, Me.State.RecordTypeId, Me.State.DealerId, Me.State.RejectCodeMask, Me.State.RejectReasonMask, Me.State.RejectMsgTypeId)
        End If


        Me.DataGridRejectCode.AutoGenerateColumns = False
        Me.DataGridRejectCode.PageSize = State.PageSize


        If (Me.State.IsEditMode) Then
            Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.selectedMsgCodeId, Me.DataGridRejectCode, Me.State.PageIndex, Me.State.IsEditMode)
        Else
            Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.selectedMsgCodeId, Me.DataGridRejectCode, Me.State.PageIndex)
        End If

        Me.DataGridRejectCode.DataSource = Me.State.searchDV
        Me.State.PageIndex = Me.DataGridRejectCode.CurrentPageIndex
        Me.DataGridRejectCode.DataBind()

        If State.searchDV.Count = 0 Then
            Me.btnSave.Enabled = False
            Me.btnCancel.Enabled = False
        End If

        Session("recCount") = Me.State.searchDV.Count
        Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
    End Sub

    Private Sub ddlRectionMsgType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlRectionMsgType.SelectedIndexChanged
        Try
            ddlRecordType.Items.Clear()
            If (ddlRectionMsgType.SelectedIndex <> NO_ITEM_SELECTED_INDEX) Then
                Me.State.RejectMsgTypeId = GetSelectedItem(ddlRectionMsgType)

                If LookupListNew.GetCodeFromId(LookupListNew.LK_MSG_TYPE, Me.State.RejectMsgTypeId) = MSG_CODE_DLR_REJECT Then

                    'Dim dv As DataView = LookupListNew.GetRecordTypeLookupList(Authentication.LangId, True) 'RECTYP

                    'If (dv.Count > 0) Then
                    '    Dim condition As String = " code not in ('XX','NX','AX','AS','SU','NO')"
                    '    dv.RowFilter = dv.RowFilter & " and " & condition
                    'End If

                    'BindListControlToDataView(Me.ddlRecordType, dv) 'RECTYP

                    Dim recordTypeLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("RECTYP", Thread.CurrentPrincipal.GetLanguageCode())
                    Dim notStringList() As String = {"XX", "NX", "AX", "AS", "SU", "NO"}
                    Dim FilteredRecord As ListItem() = (From x In recordTypeLkl
                                                        Where Not notStringList.Contains(x.Code)
                                                        Select x).ToArray()

                    Me.ddlRecordType.Populate(FilteredRecord, New PopulateOptions() With
                        {
                            .AddBlankItem = True,
                            .SortFunc = AddressOf PopulateOptions.GetDescription
                        })

                    ddlRecordType.SelectedIndex = BLANK_ITEM_SELECTED
                    Me.EnableDisableControl(Me.ddlRecordType, True)

                ElseIf (LookupListNew.GetCodeFromId(LookupListNew.LK_MSG_TYPE, Me.State.RejectMsgTypeId) = MSG_CODE_DLR_PYMT_REJECT) Then
                    Dim dv As DataView = LookupListNew.GetPaymentRecordTypeLookupList(Authentication.LangId, True)
                    If (dv.Count > 0) Then
                        Dim condition As String = "CODE <> 'X'"
                        dv.RowFilter = dv.RowFilter & " and " & condition
                    End If
                    'BindListControlToDataView(Me.ddlRecordType, dv) 'PYMTRECTYP
                    Dim recordTypeLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("PYMTRECTYP", Thread.CurrentPrincipal.GetLanguageCode())
                    Dim FilteredRecord As ListItem() = (From x In recordTypeLkl
                                                        Where Not x.Code = "X"
                                                        Select x).ToArray()
                    Me.ddlRecordType.Populate(FilteredRecord, New PopulateOptions() With
                                              {
                                              .AddBlankItem = True,
                                              .SortFunc = AddressOf PopulateOptions.GetDescription
                                              })
                    ddlRecordType.SelectedIndex = BLANK_ITEM_SELECTED
                    Me.EnableDisableControl(Me.ddlRecordType, True)
                End If
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    'Private Sub ddlRecordType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlRecordType.SelectedIndexChanged
    '    Try
    '        'clear the grid
    '        DataGridRejectCode.DataSource = Nothing
    '        DataGridRejectCode.DataBind()

    '    Catch ex As Exception
    '        Me.HandleErrors(ex, Me.MasterPage.MessageController)
    '    End Try
    'End Sub

    Private Sub cboPageSize_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Me.State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            Me.State.selectedPageSize = Me.State.PageSize
            Me.State.PageIndex = NewCurrentPageIndex(DataGridRejectCode, State.searchDV.Count, State.PageSize)
            DataGridRejectCode.CurrentPageIndex = NewCurrentPageIndex(DataGridRejectCode, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            Me.State.selectedPageSize = DataGridRejectCode.PageSize
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
        Dim lastAction As ElitaPlusPage.DetailPageCommand = Me.State.ActionInProgress
        If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
            Select Case lastAction
                Case ElitaPlusPage.DetailPageCommand.Undo
                    Me.SetStateProperties()
                    Me.State.PageIndex = 0
                    Me.State.selectedMsgCodeId = Guid.Empty
                    Me.State.IsGridVisible = True
                    Me.State.searchDV = Nothing
                    Me.State.HasDataChanged = False
                    Me.State.IsEditMode = False
                    Me.SetControlState()
                    cboPageSize.SelectedValue = CType(State.PageSize, String)
                    Me.PopulateGrid()
            End Select


        End If
    End Sub

    Private Sub CleanPopupInput()
        Try
            If Not Me.State Is Nothing Then
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                'Me.State.LastErrMsg = ""
                Me.HiddenSaveChangesPromptResponse.Value = ""
            End If
        Catch ex As Exception
            '  Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub
End Class
