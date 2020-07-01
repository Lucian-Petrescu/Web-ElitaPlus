Option Strict On
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports System.Threading
Imports Microsoft.VisualBasic
Imports System.Web.Services

Partial Class ClaimStatusDetailForm
    Inherits ElitaPlusSearchPage

    Public Const URL As String = "~/Claims/ClaimStatusDetailForm.aspx"
    Protected csFamilyBO As ClaimStatus

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

    Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
    Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

    Private Const GRID_CTL_SELECTED_CHKBOX As String = "CheckBoxItemSel"
    Private Const GRID_CTL_SELECTED_LISTITEMID As String = "lblListItemId"
    Private Const GRID_CTL_EXTENDED_CLAIM_STATUS As String = "lblExtendedClaimStatus"
    Private Const GRID_CTL_EXTENDED_CLAIM_STATUS_DROPDOWNLIST As String = "ExtendedClaimStatusDropDownList"
    Private Const GRID_CTL_OWNER As String = "dropDownOwner"
    Private Const GRID_CTL_STATUS_COMMENT As String = "txtComment"
    Private Const GRID_CTL_ISNEW As String = "lblIsNew"
    Private Const GRID_CTL_STATUS_DATE As String = "txtStatusDate"
    Private Const GRID_CTL_CLAIM_STATUS_GROUP_ID As String = "lblClaimStatusGroupId"
    Private Const GRID_CTL_CLAIM_STATUS_DROPDOWNLIST As String = "ExtendedClaimStatusDropDownList"
    Private Const GRID_CTL_CLAIM_STATUS_ID As String = "lblClaimStatusId"
    Private Const GRID_CTL_IMG_STATUS_DATE As String = "imgStatusDate"
    Private Const GRID_CTL_GROUP_NUMBER As String = "lblGroupNumber"

    Private Const GRID_COL_SELECTED_IDX As Integer = 0  ' GRID_COL_CLAIM_STATUS_ID_IDX
    Private Const GRID_COL_SELECTED_CHK_IDX As Integer = 1
    Private Const GRID_COL_EXTENDED_CLAIM_STATUS_IDX As Integer = 2
    Private Const GRID_COL_STATUS_DATE_IDX As Integer = 3
    Private Const GRID_COL_COMMENT_IDX As Integer = 4
    Private Const GRID_COL_OWNER_IDX As Integer = 5
    Private Const GRID_COL_ISNEW_IDX As Integer = 6
    Private Const GRID_COL_CLAIM_STATUS_GROUP_ID_IDX As Integer = 7
    Private Const GRID_COL_LIST_ITEM_IDX As Integer = 8
    Private Const GRID_COL_GROUP_NUMBER_IDX As Integer = 9

    Private IsReturningFromChild As Boolean = False
    Private Const YESNO As String = "YESNO"
    Private Const EXTOWN As String = "EXTOWN"

    Public Const PAGETITLE As String = "EXTENDED_CLAIM_STATUS"
    Public Const CLAIM_TAB As String = "Claim"

    Private Const GRID_COL_STAGE_NAME As String = "STAGE_NAME"
    Private Const GRID_COL_AGING_START_STATUS As String = "AGING_START_STATUS"
    Private Const GRID_COL_AGING_START_DATETIME As String = "AGING_START_DATETIME"
    Private Const GRID_COL_AGING_END_STATUS As String = "AGING_END_STATUS"
    Private Const GRID_COL_AGING_END_DATETIME As String = "AGING_END_DATETIME"
    Private Const GRID_COL_STATUS_AGING_DAYS As String = "STATUS_AGING_DAYS"
    Private Const GRID_COL_STATUS_AGING As String = "STATUS_AGING"
    Private Const GRID_COL_STATUS_AGING_HOURS As String = "STATUS_AGING_HOURS"
    Private Const GRID_COL_AGING_SINCE_CLAIM_INCEPTION_DAYS As String = "AGING_SINCE_CLAIM_INCEPTION_DAYS"
    Private Const GRID_COL_AGING_SINCE_CLAIM_INCEPTION As String = "AGING_SINCE_CLAIM_INCEPTION"
    Private Const GRID_COL_AGING_SINCE_CLAIM_INCEPTION_HOURS As String = "AGING_SINCE_CLAIM_INCEPTION_HOURS"

    Dim permType As FormAuthorization.enumPermissionType

#End Region

#Region "Member Variables"
    Public selectedPageIndex As Integer = DEFAULT_PAGE_INDEX
#End Region

#Region "Page Parameters"
    Public Class Parameters
        Public ClaimBO As ClaimBase
        Public Sub New(ByVal claimBO As ClaimBase)
            Me.ClaimBO = claimBO
        End Sub
    End Class
#End Region

#Region "Page State"
    Class MyState
        Public searchDV As DataView = Nothing
        Public claimId As Guid = Guid.Empty
        Public ClaimBO As ClaimBase
        Public YESNOdv As DataView
        Public OWNERdv As DataView
        Public IsEditMode As Boolean
        Public ExtStatusEntry As String

        Public ClaimStatusBO As ClaimStatus
        Public StatusAdded As Boolean = False
        Public CompanyId As Guid
        Public ClaimStatusID As Guid
        Public IsAfterSave As Boolean
        Public IsDisabled As Boolean = False

        Public ActionInProgress As DetailPageCommand = ElitaPlusPage.DetailPageCommand.Nothing_
        Public PageIndex As Integer = 0
        Public selectedPageIndex As Integer = DEFAULT_PAGE_INDEX
        Public PageSize As Integer = DEFAULT_PAGE_SIZE
        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        Public BeginningPageIndex As Integer = 0
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

#Region "Page Events"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        permType = FormAuthorization.GetPermissions("CLAIMSTATUSDETAILFORM")

        'DEF-27836
        If Not (HdnFldFilterSize.Value = Request.Form(cboPageSize.UniqueID)) Then
            cboPageSize_SelectedIndexChanged(Me.cboPageSize, e)
        End If

        Try
            Me.MasterPage.MessageController.Clear_Hide()

            If CMD.Value Is Nothing Or CMD.Value = "" Then
                CMD.Value = Request.Params("CMD")
            End If

            If Not Me.IsPostBack Then

                ClearGridHeaders(Me.DataGridDropdowns)

                If (Me.MasterPage.MessageController.Text.Trim = "") Then
                    Me.MasterPage.MessageController.Clear()
                End If

                Me.SetFormTab(CLAIM_TAB)
                Me.SetFormTitle(PAGETITLE)

                Me.ShowMissingTranslations(Me.MasterPage.MessageController)
                Me.State.selectedPageSize = 10
                'Me.State.PageIndex = 0
                Me.MenuEnabled = False
                CheckifComingFromBackEndClaim()
                Me.CustomerNameTD.InnerText = Me.State.ClaimBO.CustomerName
                Me.ClaimNumberTD.InnerText = Me.State.ClaimBO.ClaimNumber
                Me.SetGridItemStyleColor(Me.DataGridDropdowns)
                Me.PopulateAll()
                Me.PopulateGrid()
                'Set page size
                cboPageSize.SelectedValue = Me.State.selectedPageSize.ToString()
            Else
                CheckIfComingFromSaveConfirm()


                'If (cboPageSize.SelectedValue <> Me.State.selectedPageSize.ToString()) Then
                '    Me.State.selectedPageSize = Integer.Parse(cboPageSize.SelectedValue)
                '    PopulateGrid()
                'End If

            End If

            EnableDisableControl(Me.SaveButton_WRITE)
            EnableDisableControl(Me.CancelButton_WRITE)

            If (Not Me.State.ClaimBO Is Nothing AndAlso Not Me.State.ExtStatusEntry Is Nothing AndAlso Me.State.ExtStatusEntry.Equals(Codes.CLAIM_EXTENDED_STATUS_ENTREY__PREDEFINED)) Then
                EnableDisableControl(Me.NewButton_WRITE, True)
            ElseIf Claim.CheckClaimPaymentInProgress(Me.State.claimId, Me.State.ClaimBO.Company.CompanyGroupId) Then
                EnableDisableControl(Me.SaveButton_WRITE, True)
                EnableDisableControl(Me.NewButton_WRITE, True)
            Else
                EnableDisableControl(Me.NewButton_WRITE, False)
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                Me.State.claimId = CType(CallingPar, Guid)
                Me.State.ClaimBO = ClaimFacade.Instance.GetClaim(Of ClaimBase)(Me.State.claimId)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub CheckifComingFromBackEndClaim()
        If Me.NavController Is Nothing Then
            Exit Sub
        End If
        If Me.NavController.CurrentNavState.Name = "EXTENDED_STATUS" Then
            Dim params As Parameters = CType(Me.NavController.ParametersPassed, Parameters)
            If Not params.ClaimBO Is Nothing Then
                Me.State.ClaimBO = params.ClaimBO
            End If
        End If
    End Sub

    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        Try
            Me.MenuEnabled = True
            Me.IsReturningFromChild = True
            Me.State.searchDV = Nothing
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public HasDataChanged As Boolean
        Public claimId As Guid = Guid.Empty

        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal claimId As Guid, ByVal hasDataChanged As Boolean)
            Me.LastOperation = LastOp
            Me.HasDataChanged = hasDataChanged
            Me.claimId = claimId
        End Sub
    End Class
#End Region

#End Region

#Region "Datagrid Related "

    Protected Sub ItemBound(ByVal source As Object, ByVal e As DataGridItemEventArgs) Handles DataGridDropdowns.ItemDataBound

        Dim clmStat As String = String.Empty
        Try
            BaseItemBound(source, e)

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.EditItem Then

                Dim drv As DataRowView = CType(e.Item.DataItem, DataRowView)
                Dim i As Integer
                Dim disableRow As Boolean = False

                'Disable Row belonging to a group number (with a status already selected from that group -commented out)
                If Not drv(DALObjects.ClaimStatusDAL.COL_NAME_GROUP_NUMBER) Is DBNull.Value Then
                    disableRow = True
                End If

                If Not drv(DALObjects.ClaimStatusDAL.COL_NAME_LIST_ITEM_ID) Is System.DBNull.Value Then
                    clmStat = LookupListNew.GetCodeFromId(LookupListNew.LK_EXTENDED_CLAIM_STATUSES, GuidControl.ByteArrayToGuid(drv(DALObjects.ClaimStatusDAL.COL_NAME_LIST_ITEM_ID)))
                End If

                If (Not e.Item.Cells(Me.GRID_COL_OWNER_IDX).FindControl(Me.GRID_CTL_OWNER) Is Nothing) Then
                    Dim OWNERdv As DataView = Me.State.OWNERdv
                    Dim dropdownOwner As DropDownList = CType(e.Item.Cells(Me.GRID_COL_OWNER_IDX).FindControl(Me.GRID_CTL_OWNER), DropDownList)

                    Dim ownerList As ListItem() = CommonConfigManager.Current.ListManager.GetList("EXTOWN", Thread.CurrentPrincipal.GetLanguageCode())
                    dropdownOwner.Populate(ownerList, New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = False
                                                   })

                    If Not drv(DALObjects.ClaimStatusByGroupDAL.COL_NAME_OWNER_ID) Is DBNull.Value Then
                        dropdownOwner.SelectedValue = GuidControl.ByteArrayToGuid(CType(drv(DALObjects.ClaimStatusByGroupDAL.COL_NAME_OWNER_ID), Byte())).ToString()
                    End If

                    If disableRow Or (Me.State.IsDisabled AndAlso clmStat <> Codes.CLAIM_EXTENDED_STATUS__PAYMENT_REVIEW_APPROVED) Then
                        EnableDisableControl(dropdownOwner, True)
                    Else
                        If drv("Enabled").ToString() = "Y" Or drv("Enabled").ToString() = "" Then
                            EnableDisableControl(dropdownOwner)
                        End If
                        dropdownOwner.Attributes.Add("onchange", "setDirty()")
                    End If
                End If

                If Not drv(DALObjects.ClaimStatusDAL.COL_NAME_STATUS_DATE_1) Is DBNull.Value Then
                    Me.PopulateControlFromBOProperty(e.Item.FindControl(GRID_CTL_STATUS_DATE), New DateTimeType(CType(drv(DALObjects.ClaimStatusDAL.COL_NAME_STATUS_DATE_1), Date)), DATE_TIME_FORMAT)
                End If

                Me.AddCalendar(CType(e.Item.FindControl(GRID_CTL_IMG_STATUS_DATE), ImageButton), CType(e.Item.FindControl(GRID_CTL_STATUS_DATE), TextBox), "", "Y")

                If Not drv(DALObjects.ClaimStatusDAL.COL_NAME_CLAIM_STATUS_ID) Is System.DBNull.Value Then
                    Me.PopulateControlFromBOProperty(e.Item.FindControl(GRID_CTL_CLAIM_STATUS_ID), drv(DALObjects.ClaimStatusDAL.COL_NAME_CLAIM_STATUS_ID))
                Else
                    Me.PopulateControlFromBOProperty(e.Item.FindControl(GRID_CTL_CLAIM_STATUS_ID), "")
                End If

                If (Not e.Item.Cells(Me.GRID_COL_SELECTED_CHK_IDX).FindControl(Me.GRID_CTL_SELECTED_CHKBOX) Is Nothing) Then
                    Dim checkboxSelect As CheckBox
                    checkboxSelect = CType(e.Item.Cells(Me.GRID_COL_SELECTED_CHK_IDX).FindControl(Me.GRID_CTL_SELECTED_CHKBOX), CheckBox)
                    If Me.State.IsEditMode Then
                        EnableDisableControl(checkboxSelect, True)
                    Else
                        If disableRow Or (Me.State.IsDisabled AndAlso clmStat <> Codes.CLAIM_EXTENDED_STATUS__PAYMENT_REVIEW_APPROVED) Then
                            EnableDisableControl(checkboxSelect, True)
                        Else
                            If drv("Enabled").ToString() = "Y" Or drv("Enabled").ToString() = "" Then
                                EnableDisableControl(checkboxSelect)
                            End If
                            checkboxSelect.Attributes.Add("onchange", "setDirty()")
                        End If
                    End If
                End If

                If (Not e.Item.Cells(Me.GRID_COL_STATUS_DATE_IDX).FindControl(Me.GRID_CTL_STATUS_DATE) Is Nothing) Then
                    Dim statusDateTextBox As TextBox
                    statusDateTextBox = CType(e.Item.Cells(Me.GRID_COL_STATUS_DATE_IDX).FindControl(Me.GRID_CTL_STATUS_DATE), TextBox)
                    If disableRow Or (Me.State.IsDisabled AndAlso clmStat <> Codes.CLAIM_EXTENDED_STATUS__PAYMENT_REVIEW_APPROVED) Then
                        EnableDisableControl(statusDateTextBox, True)
                    Else
                        If drv("Enabled").ToString() = "Y" Or drv("Enabled").ToString() = "" Then
                            EnableDisableControl(statusDateTextBox)
                        End If
                        statusDateTextBox.Attributes.Add("onchange", "setDirty()")
                    End If
                End If

                Dim commentTextBox As TextBox
                commentTextBox = CType(e.Item.Cells(Me.GRID_COL_COMMENT_IDX).FindControl(Me.GRID_CTL_STATUS_COMMENT), TextBox)
                If (Not commentTextBox Is Nothing) Then
                    If disableRow Or (Me.State.IsDisabled AndAlso clmStat <> Codes.CLAIM_EXTENDED_STATUS__PAYMENT_REVIEW_APPROVED) Then
                        EnableDisableControl(commentTextBox, True)
                    Else
                        If drv("Enabled").ToString() = "Y" Or drv("Enabled").ToString() = "" Then
                            EnableDisableControl(e.Item.Cells(Me.GRID_COL_COMMENT_IDX).FindControl(Me.GRID_CTL_STATUS_COMMENT))
                        End If
                        commentTextBox.Attributes.Add("onchange", "setDirty()")
                    End If
                End If

            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub EnableDisableControl(ByVal ctl As System.Web.UI.Control, Optional ByVal disabled As Boolean = False)
        If Not (permType = FormAuthorization.enumPermissionType.EDIT) Or disabled Then
            ControlMgr.SetEnableControl(Me, CType(ctl, System.Web.UI.WebControls.WebControl), False)
        Else
            ControlMgr.SetEnableControl(Me, CType(ctl, System.Web.UI.WebControls.WebControl), True)
        End If
    End Sub

    Protected Sub DataGridDropdowns_ItemCreated(ByVal sender As Object, ByVal e As DataGridItemEventArgs) Handles DataGridDropdowns.ItemCreated
        BaseItemCreated(sender, e)
    End Sub

    Private Sub cboPageSize_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            HdnFldFilterSize.Value = Request.Form(cboPageSize.UniqueID)

            If Integer.Parse(cboPageSize.SelectedValue) = Me.State.selectedPageSize Then
                Return
            End If

            If IsDataGPageDirty() Then
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.GridPageSize
                DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSavePagePromptResponse)
            Else
                Me.State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                Me.PopulateGrid()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

        'Try
        '    If IsDataGPageDirty() Then
        '        'cboPageSize.SelectedValue = Me.State.selectedPageSize.ToString()
        '        DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSavePagePromptResponse)
        '    Else
        '        If Me.State.selectedPageSize.ToString() <> cboPageSize.SelectedValue Then
        '            Me.State.PageIndex = 0
        '        End If
        '        Me.State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
        '        Me.PopulateGrid()
        '    End If
        'Catch ex As Exception
        '    Me.HandleErrors(ex, Me.MasterPage.MessageController)
        'End Try
    End Sub

    Private Sub DataGridDropdowns_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridDropdowns.PageIndexChanged
        Try
            Me.State.selectedPageIndex = e.NewPageIndex
            If IsDataGPageDirty() Then
                DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSavePagePromptResponse)
            Else
                Me.DataGridDropdowns.CurrentPageIndex = e.NewPageIndex
                PopulateGrid()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

        'Try
        '    If IsDataGPageDirty() Then
        '        DataGridDropdowns.CurrentPageIndex = Me.State.PageIndex
        '    Else
        '        Me.State.PageIndex = e.NewPageIndex
        '        PopulateGrid()
        '    End If
        'Catch ex As Exception
        '    Me.HandleErrors(ex, Me.MasterPage.MessageController)
        'End Try
    End Sub

    Function IsDataGPageDirty() As Boolean
        Dim Result As String = Me.HiddenIsPageDirty.Value
        Return Result.Equals("YES")
    End Function

    Private Sub SetFocusOnEditableFieldInGrid(ByVal grid As DataGrid, ByVal cellPosition As Integer, ByVal controlName As String, ByVal itemIndex As Integer, ByVal newRow As Boolean)
        'Set focus on the Description TextBox for the EditItemIndex row
        Dim desc As DropDownList = CType(grid.Items(itemIndex).Cells(cellPosition).FindControl(controlName), DropDownList)
        Dim listcontext As ListContext = New ListContext()
        listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id

        Dim extendedStatusList As ListItem() = CommonConfigManager.Current.ListManager.GetList("ExtendedStatusByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
        desc.Populate(extendedStatusList, New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = False
                                                   })
        SetFocus(desc)
    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = Me.HiddenSavePagePromptResponse.Value
        Try
            If Not confResponse.Equals(String.Empty) Then
                If confResponse = Me.MSG_VALUE_YES Then
                    SaveChanges()
                End If
                Me.HiddenSavePagePromptResponse.Value = String.Empty
                Me.HiddenIsPageDirty.Value = String.Empty

                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.claimId, False))
                    Case ElitaPlusPage.DetailPageCommand.GridPageSize
                        Me.State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                    Case Else
                        Me.DataGridDropdowns.CurrentPageIndex = Me.State.selectedPageIndex
                End Select

                PopulateGrid()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Button Clicks"

    Protected Sub SaveChanges()
        Dim i, j, k As Integer
        Dim DataChanged As Boolean = False
        Dim retVal As Integer
        Dim isFirstBO As Boolean = True
        Dim selectedCount As Integer = 0
        Dim emptyDealerId As Boolean = False
        Dim count As Integer
        Dim prfpmtClaimStatusId As String = String.Empty

        Me.MasterPage.MessageController.Clear_Hide()
        ClearGridHeaders(Me.DataGridDropdowns)

        Try
            Dim errors() As ValidationError = {New ValidationError("You cannot select more than one status from the same group", GetType(ClaimStatus), Nothing, "GroupNumber", Nothing)}
            'Validate if more than one status is selected belonging to the same group
            For j = 0 To Me.DataGridDropdowns.Items.Count - 1
                If CType(Me.DataGridDropdowns.Items(j).Cells(Me.GRID_COL_SELECTED_CHK_IDX).FindControl(Me.GRID_CTL_SELECTED_CHKBOX), CheckBox).Checked() _
                    AndAlso CType(Me.DataGridDropdowns.Items(j).Cells(Me.GRID_COL_GROUP_NUMBER_IDX).FindControl(Me.GRID_CTL_GROUP_NUMBER), Label).Text <> "" Then
                    count = 0
                    For k = 0 To Me.DataGridDropdowns.Items.Count - 1
                        If CType(Me.DataGridDropdowns.Items(k).Cells(Me.GRID_COL_SELECTED_CHK_IDX).FindControl(Me.GRID_CTL_SELECTED_CHKBOX), CheckBox).Checked() _
                            AndAlso CType(Me.DataGridDropdowns.Items(k).Cells(Me.GRID_COL_GROUP_NUMBER_IDX).FindControl(Me.GRID_CTL_GROUP_NUMBER), Label).Text <> "" _
                            AndAlso CType(Me.DataGridDropdowns.Items(j).Cells(Me.GRID_COL_GROUP_NUMBER_IDX).FindControl(Me.GRID_CTL_GROUP_NUMBER), Label).Text = CType(Me.DataGridDropdowns.Items(k).Cells(Me.GRID_COL_GROUP_NUMBER_IDX).FindControl(Me.GRID_CTL_GROUP_NUMBER), Label).Text Then

                            count = count + 1
                            If count > 1 Then
                                Throw New BOValidationException(errors, GetType(ClaimStatus).FullName)
                            End If

                        End If
                    Next
                End If
            Next

            For i = 0 To Me.DataGridDropdowns.Items.Count - 1
                Dim statusOrderInt As Integer
                Dim listItemId As String = CType(Me.DataGridDropdowns.Items(i).Cells(Me.GRID_COL_LIST_ITEM_IDX).FindControl(Me.GRID_CTL_SELECTED_LISTITEMID), Label).Text
                Dim selected As Boolean = False
                Dim claimStatusGroupId As Guid = Guid.Empty
                Dim isNew As String = ""

                If Me.State.IsEditMode AndAlso Me.DataGridDropdowns.Items(i).ItemType = ListItemType.EditItem Then
                    selected = True
                    claimStatusGroupId = Me.GetSelectedItem(CType(Me.DataGridDropdowns.Items(i).Cells(Me.GRID_COL_EXTENDED_CLAIM_STATUS_IDX).FindControl(Me.GRID_CTL_CLAIM_STATUS_DROPDOWNLIST), DropDownList))
                    isNew = "Y"
                Else
                    selected = CType(Me.DataGridDropdowns.Items(i).Cells(Me.GRID_COL_SELECTED_CHK_IDX).FindControl(Me.GRID_CTL_SELECTED_CHKBOX), CheckBox).Checked()
                    claimStatusGroupId = New Guid(CType(Me.DataGridDropdowns.Items(i).Cells(Me.GRID_COL_CLAIM_STATUS_GROUP_ID_IDX).FindControl(Me.GRID_CTL_CLAIM_STATUS_GROUP_ID), Label).Text)

                    ' if exists then Y else N
                    If CType(Me.DataGridDropdowns.Items(i).Cells(Me.GRID_COL_ISNEW_IDX).FindControl(Me.GRID_CTL_ISNEW), Label).Text = "N" Then
                        isNew = "Y"
                    Else
                        isNew = "N"
                    End If
                End If

                Dim claimStatusId As String = Nothing
                If Not CType(Me.DataGridDropdowns.Items(i).Cells(Me.GRID_COL_SELECTED_IDX).FindControl(Me.GRID_CTL_CLAIM_STATUS_ID), Label).Text = "" Then
                    claimStatusId = CType(Me.DataGridDropdowns.Items(i).Cells(Me.GRID_COL_SELECTED_IDX).FindControl(Me.GRID_CTL_CLAIM_STATUS_ID), Label).Text
                    If Not CType(Me.DataGridDropdowns.Items(i).Cells(Me.GRID_COL_LIST_ITEM_IDX).FindControl(Me.GRID_CTL_SELECTED_LISTITEMID), Label).Text = "" Then
                        If (LookupListNew.GetCodeFromId(LookupListNew.LK_EXTENDED_CLAIM_STATUSES, New Guid(CType(Me.DataGridDropdowns.Items(i).Cells(Me.GRID_COL_LIST_ITEM_IDX).FindControl(Me.GRID_CTL_SELECTED_LISTITEMID), Label).Text)) _
                            = Codes.CLAIM_EXTENDED_STATUS__PENDING_REVIEW_FOR_PAYMENT) Then
                            prfpmtClaimStatusId = claimStatusId
                        End If
                    End If
                End If

                Dim statusDate As Date = Date.MinValue
                If (DateHelper.IsDate(CType(CType(Me.DataGridDropdowns.Items(i).Cells(Me.GRID_COL_STATUS_DATE_IDX).FindControl(Me.GRID_CTL_STATUS_DATE), TextBox).Text, String))) Then
                    statusDate = DateHelper.GetDateValue(CType(Me.DataGridDropdowns.Items(i).Cells(Me.GRID_COL_STATUS_DATE_IDX).FindControl(Me.GRID_CTL_STATUS_DATE), TextBox).Text)
                End If

                Dim statusComment As String = CType(Me.DataGridDropdowns.Items(i).Cells(Me.GRID_COL_COMMENT_IDX).FindControl(Me.GRID_CTL_STATUS_COMMENT), TextBox).Text
                Dim ownerId As Guid = Me.GetSelectedItem(CType(Me.DataGridDropdowns.Items(i).Cells(Me.GRID_COL_OWNER_IDX).FindControl(Me.GRID_CTL_OWNER), DropDownList))

                If selected Then
                    ' Checkbox selected
                    selectedCount = selectedCount + 1

                    If (Not isNew Is Nothing AndAlso isNew = "N") Then
                        ' Record exist for update

                        Dim dv As DataView = Me.State.searchDV
                        Dim dr As DataRowView = GetDataRow(dv, claimStatusId)

                        If Not dr Is Nothing Then
                            Dim isDirty As Boolean = False

                            If Not dr(DALObjects.ClaimStatusDAL.COL_NAME_STATUS_COMMENTS) Is DBNull.Value Then
                                isDirty = isDirty Or (CType(dr(DALObjects.ClaimStatusDAL.COL_NAME_STATUS_COMMENTS), String) <> (CType(CType(Me.DataGridDropdowns.Items(i).Cells(Me.GRID_COL_COMMENT_IDX).FindControl(Me.GRID_CTL_STATUS_COMMENT), TextBox).Text, String)))
                            Else
                                isDirty = isDirty Or ("" <> (CType(CType(Me.DataGridDropdowns.Items(i).Cells(Me.GRID_COL_COMMENT_IDX).FindControl(Me.GRID_CTL_STATUS_COMMENT), TextBox).Text, String)))
                            End If

                            If Not dr(DALObjects.ClaimStatusDAL.COL_NAME_STATUS_DATE_1) Is DBNull.Value Then

                                isDirty = isDirty Or
                                          (
                                            DateHelper.GetDateValue(CType(dr(DALObjects.ClaimStatusDAL.COL_NAME_STATUS_DATE_1), String)) <>
                                            DateHelper.GetDateValue((CType(CType(Me.DataGridDropdowns.Items(i).Cells(Me.GRID_COL_STATUS_DATE_IDX).FindControl(Me.GRID_CTL_STATUS_DATE), TextBox).Text, String)))
                                          )
                            Else
                                isDirty = isDirty Or ("" <> (CType(CType(Me.DataGridDropdowns.Items(i).Cells(Me.GRID_COL_STATUS_DATE_IDX).FindControl(Me.GRID_CTL_STATUS_DATE), TextBox).Text, String)))
                            End If

                            If isDirty Then
                                Dim curBO As ClaimStatus

                                If dr(DALObjects.ClaimStatusDAL.COL_NAME_CLAIM_STATUS_ID) Is DBNull.Value Then
                                    curBO = GetClaimStatusBO(isFirstBO, Guid.Empty)
                                Else
                                    curBO = GetClaimStatusBO(isFirstBO, New Guid(CType(dr(DALObjects.ClaimStatusDAL.COL_NAME_CLAIM_STATUS_ID), Byte())))
                                End If

                                curBO.Comments = statusComment

                                If Not (statusDate = Date.MinValue) Then
                                    curBO.StatusDate = statusDate
                                End If

                                Me.BindBOPropertyToGridHeader(curBO, "Comments", Me.DataGridDropdowns.Columns(Me.GRID_COL_COMMENT_IDX))
                                Me.BindBOPropertyToGridHeader(curBO, "StatusDate", Me.DataGridDropdowns.Columns(Me.GRID_COL_STATUS_DATE_IDX))

                                curBO.Validate()

                                DataChanged = True
                                isFirstBO = False

                            End If
                        End If
                    Else
                        ' Record not exist for insert
                        Dim curBO As ClaimStatus
                        curBO = GetClaimStatusBO(isFirstBO, Guid.Empty)

                        curBO.ClaimId = Me.State.claimId
                        curBO.ClaimStatusByGroupId = claimStatusGroupId
                        curBO.Comments = statusComment

                        If Not (statusDate = Date.MinValue) Then
                            curBO.StatusDate = statusDate
                        End If

                        Me.BindBOPropertyToGridHeader(curBO, "Comments", Me.DataGridDropdowns.Columns(Me.GRID_COL_COMMENT_IDX))
                        Me.BindBOPropertyToGridHeader(curBO, "StatusDate", Me.DataGridDropdowns.Columns(Me.GRID_COL_STATUS_DATE_IDX))

                        curBO.Validate()
                        DataChanged = True
                        isFirstBO = False

                    End If

                Else
                    ' Checkbox deselected
                    If Not isNew Is Nothing AndAlso isNew = "N" Then
                        ' Record exist for deletion

                        Dim curBO, tempBO As ClaimStatus
                        Dim curStatus As String = String.Empty

                        If claimStatusId Is Nothing Or claimStatusId = "" Then
                            curBO = GetClaimStatusBO(isFirstBO, Guid.Empty)
                        Else
                            curBO = GetClaimStatusBO(isFirstBO, New Guid(claimStatusId))
                        End If

                        curStatus = curBO.StatusCode
                        curBO.Validate()
                        curBO.Delete()

                        'Check if  PRFPMT status exists and delete and insert the status date to today's date
                        If Not prfpmtClaimStatusId = String.Empty AndAlso curStatus = Codes.CLAIM_EXTENDED_STATUS__PAYMENT_REVIEW_APPROVED Then
                            curBO = GetClaimStatusBO(isFirstBO, New Guid(prfpmtClaimStatusId))
                            tempBO = GetClaimStatusBO(isFirstBO, Guid.Empty)
                            tempBO.CopyFrom(curBO)
                            tempBO.StatusDate = Date.Now
                            tempBO.Validate()
                            curBO.Delete()
                        End If

                        DataChanged = True
                        isFirstBO = False

                    End If
                End If
            Next

            If DataChanged Then
                csFamilyBO.Save()
                ClaimStatus.UpdateExtendedMV(Me.State.ClaimBO.Id)
                Me.CMD.Value = ""
                Me.State.searchDV = Nothing
                Me.State.IsEditMode = False
                Me.DataGridDropdowns.EditItemIndex = -1
                Me.DataGridDropdowns.SelectedIndex = -1

                EnableDisableControl(Me.BackButton_WRITE, False)
                Me.SetGridItemStyleColor(Me.DataGridDropdowns)
                'Me.State.searchDV = Nothing
                Me.HiddenIsPageDirty.Value = "NO"
                PopulateGrid(True)
                Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
                'Me.State.ClaimStatusBO = Nothing
            Else
                Me.MasterPage.MessageController.AddSuccess(Message.MSG_RECORD_NOT_SAVED, True)
            End If

            csFamilyBO = Nothing
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Private Function GetClaimStatusBO(ByVal isFirstBO As Boolean, ByVal claimStatusId As Guid) As ClaimStatus

        If isFirstBO Then
            If claimStatusId.Equals(Guid.Empty) Then
                csFamilyBO = New ClaimStatus
            Else
                csFamilyBO = New ClaimStatus(claimStatusId)
            End If

            Return csFamilyBO
        Else

            Return csFamilyBO.AddClaimStatus(claimStatusId)
        End If

    End Function

    Private Function GetDataRow(ByVal dv As DataView, ByVal claimStatusId As String) As DataRowView
        Dim dr As DataRowView = Nothing
        Dim retDr As DataRowView = Nothing
        Dim i As Integer = 0

        If Not claimStatusId Is Nothing AndAlso claimStatusId <> "" Then
            Dim guidStr As String = GuidControl.GuidToHexString(New Guid(claimStatusId))

            For Each dr In dv
                If Not dr(DALObjects.ClaimStatusDAL.COL_NAME_CLAIM_STATUS_ID) Is DBNull.Value Then
                    Dim drStr As String = GuidControl.GuidToHexString(New Guid(CType(dr(DALObjects.ClaimStatusDAL.COL_NAME_CLAIM_STATUS_ID), Byte())))
                    If drStr = guidStr Then
                        retDr = dr
                    End If
                End If
            Next
        End If

        Return retDr
    End Function

    Private Sub SaveButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton_WRITE.Click
        Try
            Me.MasterPage.MessageController.Clear_Hide()
            Me.SaveChanges()
            Me.MasterPage.MessageController.Show()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub CancelButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelButton_WRITE.Click
        Try
            Me.State.IsEditMode = False
            Me.DataGridDropdowns.EditItemIndex = -1
            PopulateGrid()
            EnableDisableControl(Me.BackButton_WRITE, False)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub BackButton_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BackButton_WRITE.Click
        Try
            Me.State.IsEditMode = False
            Me.DataGridDropdowns.EditItemIndex = -1
            EnableDisableControl(Me.BackButton_WRITE, False)
            Me.Back(ElitaPlusPage.DetailPageCommand.Back)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub Back(ByVal cmd As ElitaPlusPage.DetailPageCommand)
        Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.claimId, False))
    End Sub

    Private Sub NewButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewButton_WRITE.Click

        Try
            Dim desc As DropDownList = New DropDownList
            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id

            Dim extendedStatusList As ListItem() = CommonConfigManager.Current.ListManager.GetList("ExtendedStatusByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            desc.Populate(extendedStatusList, New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = False
                                                   })
            If desc.Items.Count > 0 Then
                Me.State.IsEditMode = True
                EnableDisableControl(Me.BackButton_WRITE, True)
                EnableDisableControl(Me.NewButton_WRITE, True)
                AddNewClaimStatusHistory()
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

#End Region

#Region "Controlling Logic"

    Protected Sub PopulateGrid(Optional ByVal _refresh As Boolean = False)
        Dim oClaimStatusBO As ClaimStatus = Nothing
        If Me.State.searchDV Is Nothing Or _refresh Then
            If Not Me.State.ClaimBO Is Nothing AndAlso Not Me.State.ExtStatusEntry Is Nothing AndAlso Me.State.ExtStatusEntry.Equals(Codes.CLAIM_EXTENDED_STATUS_ENTREY__PREDEFINED) Then
                Me.State.searchDV = ClaimStatus.GetClaimStatusByUserRole(Me.State.claimId)
            Else
                Me.State.searchDV = ClaimStatus.GetClaimStatusHistoryOnly(Me.State.claimId)
            End If
        End If

        oClaimStatusBO = ClaimStatus.GetLatestClaimStatus(Me.State.claimId)
        If Not oClaimStatusBO Is Nothing Then
            If oClaimStatusBO.StatusCode.Equals(Codes.CLAIM_EXTENDED_STATUS__PENDING_REVIEW_FOR_PAYMENT) Then
                Me.State.IsDisabled = True
            End If
        End If

        If Me.DataGridDropdowns.PageSize <> Me.State.selectedPageSize Then
            Me.DataGridDropdowns.PageSize = Me.State.selectedPageSize
            Me.DataGridDropdowns.CurrentPageIndex = DEFAULT_PAGE_INDEX
        End If

        Me.DataGridDropdowns.DataSource = Me.State.searchDV
        Me.DataGridDropdowns.DataBind()

        Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)

        Dim dtClaimId As DataTable = New DataTable()
        dtClaimId.Columns.Add("claimId", Type.GetType("System.Guid"))
        Dim dr As DataRow = dtClaimId.NewRow
        dr("claimId") = Me.State.claimId
        dtClaimId.Rows.Add(dr)
        Me.moClaimRepeater.DataSource = dtClaimId
        Me.moClaimRepeater.DataBind()
    End Sub

    Public Shared Sub SetLabelColor(ByVal lbl As Label)
        lbl.ForeColor = Color.Black
    End Sub

    Private Sub PopulateAll()
        Dim YESNOdv As DataView = LookupListNew.DropdownLookupList(YESNO, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
        Me.State.YESNOdv = YESNOdv

        Dim OWNERdv As DataView = LookupListNew.DropdownLookupList(EXTOWN, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
        Me.State.OWNERdv = OWNERdv

        Dim dealerObject As New Dealer(Me.State.ClaimBO.CompanyId, Me.State.ClaimBO.DealerCode)
        Me.State.ExtStatusEntry = LookupListNew.GetCodeFromId(LookupListNew.LK_CLAIM_EXTENDED_STATUS_ENTRY, dealerObject.ClaimExtendedStatusEntryId)
    End Sub

    Private Sub AddNewClaimStatusHistory()

        Dim dv As DataView

        Try
            dv = ClaimStatus.GetClaimStatusHistoryOnly(Me.State.claimId)
            Me.State.ClaimStatusBO = New ClaimStatus
            Me.State.ClaimStatusID = Me.State.ClaimStatusBO.Id
            Me.State.ClaimStatusBO.ClaimId = Me.State.ClaimBO.Id
            Me.State.ClaimStatusBO.StatusDate = DateTime.Now

            dv = Me.State.ClaimStatusBO.GetNewDataViewRow(dv, Me.State.ClaimStatusID, Me.State.ClaimBO.Id)

            DataGridDropdowns.PageSize = Convert.ToInt32(cboPageSize.SelectedItem.ToString)
            DataGridDropdowns.DataSource = dv

            Me.SetPageAndSelectedIndexFromGuid(dv, Me.State.ClaimStatusID, Me.DataGridDropdowns, Me.State.selectedPageIndex, Me.State.IsEditMode)

            DataGridDropdowns.DataBind()

            SetGridControls(Me.DataGridDropdowns, False)
            Me.lblRecordCount.Text = dv.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)

            'Set focus on the Description TextBox for the EditItemIndex row
            Me.SetFocusOnEditableFieldInGrid(Me.DataGridDropdowns, Me.GRID_COL_EXTENDED_CLAIM_STATUS_IDX, Me.GRID_CTL_EXTENDED_CLAIM_STATUS_DROPDOWNLIST, Me.DataGridDropdowns.EditItemIndex, True)
            Me.PopulateFormFromBO()
            Me.TranslateGridControls(DataGridDropdowns)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
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

    Private Sub PopulateFormFromBO()

        Dim gridRowIdx As Integer = Me.DataGridDropdowns.EditItemIndex
        Try
            With Me.State.ClaimStatusBO
                If Not .ClaimStatusByGroupId.Equals(Guid.Empty) Then
                    CType(Me.DataGridDropdowns.Items(gridRowIdx).Cells(Me.GRID_COL_EXTENDED_CLAIM_STATUS_IDX).FindControl(Me.GRID_CTL_EXTENDED_CLAIM_STATUS), DropDownList).SelectedValue = .ClaimStatusByGroupId.ToString()
                End If
                If Not .StatusDate Is Nothing Then
                    Me.PopulateControlFromBOProperty(CType(Me.DataGridDropdowns.Items(gridRowIdx).Cells(Me.GRID_COL_STATUS_DATE_IDX).FindControl(Me.GRID_CTL_STATUS_DATE), TextBox), .StatusDate, DATE_TIME_FORMAT)
                End If
                If Not .Comments Is Nothing Then
                    Me.PopulateControlFromBOProperty(CType(Me.DataGridDropdowns.Items(gridRowIdx).Cells(Me.GRID_COL_COMMENT_IDX).FindControl(Me.GRID_CTL_STATUS_COMMENT), TextBox), .Comments)
                End If
                CType(Me.DataGridDropdowns.Items(gridRowIdx).Cells(Me.GRID_COL_SELECTED_IDX).FindControl(Me.GRID_CTL_CLAIM_STATUS_ID), Label).Text = .Id.ToString
            End With
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

#End Region

#Region "Web Methods"
    <WebMethod(), Script.Services.ScriptMethod()> _
    Public Shared Function GetClaimAgingDetails(ByVal claimId As String) As String
        Try
            Dim claimAgingDetailsDV As ClaimAgingDetails.ClaimAgingDetailsDV
            Dim ds As New DataSet
            ds.DataSetName = "ClaimAgingDetailDs"

            'Adding Table Columns
            Dim dtHeaders As New DataTable("Headers")
            dtHeaders.Columns.Add(GRID_COL_STAGE_NAME, GetType(String))
            dtHeaders.Columns.Add(GRID_COL_AGING_START_STATUS, GetType(String))
            dtHeaders.Columns.Add(GRID_COL_AGING_START_DATETIME, GetType(String))
            dtHeaders.Columns.Add(GRID_COL_AGING_END_STATUS, GetType(String))
            dtHeaders.Columns.Add(GRID_COL_AGING_END_DATETIME, GetType(String))
            dtHeaders.Columns.Add(GRID_COL_STATUS_AGING_DAYS, GetType(String))
            dtHeaders.Columns.Add(GRID_COL_STATUS_AGING_HOURS, GetType(String))
            dtHeaders.Columns.Add(GRID_COL_AGING_SINCE_CLAIM_INCEPTION_DAYS, GetType(String))
            dtHeaders.Columns.Add(GRID_COL_AGING_SINCE_CLAIM_INCEPTION_HOURS, GetType(String))
            ds.Tables.Add(dtHeaders)

            'Adding grid Headers
            Dim dr As DataRow = dtHeaders.NewRow
            dr(GRID_COL_STAGE_NAME) = TranslationBase.TranslateLabelOrMessage(GRID_COL_STAGE_NAME) ' "Stage Name"
            dr(GRID_COL_AGING_START_STATUS) = TranslationBase.TranslateLabelOrMessage(GRID_COL_AGING_START_STATUS) ' "Aging Start Status"
            dr(GRID_COL_AGING_START_DATETIME) = TranslationBase.TranslateLabelOrMessage(GRID_COL_AGING_START_DATETIME) ' "Aging Start DateTime"
            dr(GRID_COL_AGING_END_STATUS) = TranslationBase.TranslateLabelOrMessage(GRID_COL_AGING_END_STATUS) ' "Aging End Status"
            dr(GRID_COL_AGING_END_DATETIME) = TranslationBase.TranslateLabelOrMessage(GRID_COL_AGING_END_DATETIME) ' "Aging End DateTime"
            dr(GRID_COL_STATUS_AGING_DAYS) = TranslationBase.TranslateLabelOrMessage(GRID_COL_STATUS_AGING) ' "Status Aging"
            dr(GRID_COL_STATUS_AGING_HOURS) = TranslationBase.TranslateLabelOrMessage(GRID_COL_STATUS_AGING_HOURS) ' "Status Aging"
            dr(GRID_COL_AGING_SINCE_CLAIM_INCEPTION_DAYS) = TranslationBase.TranslateLabelOrMessage(GRID_COL_AGING_SINCE_CLAIM_INCEPTION) ' "Aging Since Claim Inception"
            dr(GRID_COL_AGING_SINCE_CLAIM_INCEPTION_HOURS) = TranslationBase.TranslateLabelOrMessage(GRID_COL_AGING_SINCE_CLAIM_INCEPTION_HOURS) ' "Aging Since Claim Inception"
            dtHeaders.Rows.Add(dr)

            claimAgingDetailsDV = New ClaimAgingDetails().Load_List(Guid.Parse(claimId), Authentication.LangId)
            'claimAgingDetailsDV.Table.TableName = "ClaimAgingDetails"
            ds.Tables.Add(claimAgingDetailsDV.Table.Copy())
            Return ds.GetXml()
        Catch ex As Exception
            Return "Error!"
        End Try
    End Function

#End Region

End Class
