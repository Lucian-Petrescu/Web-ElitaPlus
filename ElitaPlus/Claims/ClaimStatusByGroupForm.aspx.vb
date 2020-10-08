Imports System.Diagnostics
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports System.Threading
Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Claims

Partial Class ClaimStatusByGroupForm
    Inherits ElitaPlusSearchPage
    Protected WithEvents Label1 As Label
    Protected WithEvents Button1 As Button
    Protected WithEvents Button2 As Button
    Protected WithEvents Label2 As Label
    Protected WithEvents Label7 As Label
    'Protected WithEvents CMD As System.Web.UI.HtmlControls.HtmlInputHidden

    Protected csbgFamilyBO As ClaimStatusByGroup

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(sender As Object, e As EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Constants "
    Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
    Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
    Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"
    Public Const ERR_STATUS_ORDER_EXIST As String = "ERR_STATUS_ORDER_EXIST"
    Private Const MSG_NO_RECORD_SELECTED As String = "MSG_NO_RECORD_SELECTED"
    Private Const GRID_CTL_SELECTED_CHKBOX As String = "CheckBoxItemSel"
    Private Const GRID_CTL_SELECTED_LISTITEMID As String = "lblListItemId"
    Private Const GRID_CTL_EXTENDED_CLAIM_STATUS As String = "lblExtendedClaimStatus"
    Private Const GRID_CTL_OWNER As String = "dropDownOwner"
    Private Const GRID_CTL_OWNER_ID As String = "lblOwnerId"
    Private Const GRID_CTL_SKIPPING_ALLOWED As String = "dropDownSkippingAllowed"
    Private Const GRID_CTL_STATUS_ORDER As String = "txtStatusOrder"
    Private Const GRID_CTL_ACTIVE As String = "dropDownActive"
    Private Const GRID_CTL_ACTIVE_ID As String = "lblActiveId"
    Private Const GRID_CTL_CLAIM_STATUS_GROUP_ID As String = "lblClaimStatusGroupId"
    Private Const GRID_CTL_ISNEW As String = "lblIsNew"
    Private Const GRID_CTL_GROUP_NUMBER As String = "txtGroupNumber"
    Private Const GRID_CTL_TURNAROUND_DAYS As String = "txtTurnaroundTimeDays"
    Private Const GRID_CTL_TAT_REMINDER_HOURS As String = "txtTatReminderHours"

    Private Const GRID_COL_SELECTED_IDX As Integer = 0
    Private Const GRID_COL_SELECTED_CHK_IDX As Integer = 1
    Private Const GRID_COL_EXTENDED_CLAIM_STATUS_IDX As Integer = 2
    Private Const GRID_COL_OWNER_IDX As Integer = 3
    Private Const GRID_COL_SKIPPING_ALLOWED_IDX As Integer = 4
    Private Const GRID_COL_STATUS_ORDER_IDX As Integer = 5
    Private Const GRID_COL_ACTIVE_IDX As Integer = 6
    Private Const GRID_COL_GROUP_NUMBER_IDX As Integer = 7
    Private Const GRID_COL_ISNEW_IDX As Integer = 8
    Private Const GRID_COL_TURNAROUND_DAYS_IDX As Integer = 9
    Private Const GRID_COL_TAT_REMINDER_HOURS_IDX As Integer = 10

    Private COPY_CLAIM_STATUS_GROUP As String = "COPY_CLAIM_STATUS_GROUP"
    Private NEW_CLAIM_STATUS_GROUP As String = "NEW_CLAIM_STATUS_GROUP"
    Private INIT_LOAD As String = "INIT_LOAD"

    Private Const LABEL_SELECT_DEALERCODE As String = "DEALER"
    Private Const BRANCH_LIST_FORM001 As String = "BRANCH_LIST_FORM001" ' Maintain Branch List Exception
    Private Const ERR_INVALID_STATUS_ORDER As String = "ERR_INVALID_STATUS_ORDER"
    Private Const ERR_CLAIM_STATUS_GROUP_IN_USED As String = "ERR_CLAIM_STATUS_GROUP_IN_USED"
    Private Const ERR_CLAIM_STATUS_GROUP_NOT_CHECKED As String = "ERR_CLAIM_STATUS_GROUP_NOT_CHECKED"

    Private IsReturningFromChild As Boolean = False
    Private Const YESNO As String = "YESNO"
    Private Const EXTOWN As String = "EXTOWN"

    Public Const PAGETITLE As String = "EXTENDED_CLAIM_STATUS"
    Public Const PAGETAB As String = "TABLES"

    Dim permType As FormAuthorization.enumPermissionType

    Public Enum SearchByType
        Dealer
        CompanyGroup
    End Enum

    Public ReadOnly Property TheDealerControl() As MultipleColumnDDLabelControl
        Get
            If multipleDropControl Is Nothing Then
                multipleDropControl = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
            End If
            Return multipleDropControl
        End Get
    End Property

#End Region

#Region "Page State"
    Class MyState
        Public PageIndex As Integer = 0
        Public searchDV As DataView = Nothing
        Public dealerId As Guid = Guid.Empty
        Public searchBy As Integer = 0
        Public isNew As String = "N"
        Public YESNOdv As DataView
        Public OWNERdv As DataView
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
    Private Sub Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Try
            If CMD.Value Is Nothing OrElse CMD.Value = "" Then
                CMD.Value = Request.Params("CMD")
            End If

            permType = FormAuthorization.GetPermissions("CLAIMSTATUSBYGROUPFORM")

            If Not IsPostBack Then
                ClearGridHeaders(DataGridDropdowns)
                ClearLabelErrSign(TheDealerControl.CaptionLabel)
                ShowMissingTranslations(ErrorControl)
                MenuEnabled = False
                SetGridItemStyleColor(DataGridDropdowns)
                SetHeaderControlStatus()
                SetButtonsState()
                PopulateAll()
                PopulateGrid()
            End If

            EnableDisableControl(btnSave)
            EnableDisableControl(btnCancel)
            EnableDisableControl(btnButtomNew_WRITE)
            EnableDisableControl(btnCopy_WRITE)

        Catch ex As Exception
            HandleErrors(ex, ErrorControl)
        End Try
    End Sub

    Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
        Try
            If CallingParameters IsNot Nothing Then
                State.searchBy = CType(CType(CallingPar, ArrayList)(0), Integer)
                State.dealerId = CType(CType(CallingPar, ArrayList)(1), Guid)
                State.isNew = CType(CType(CallingPar, ArrayList)(2), String)
                If State.isNew = "Y" Then
                    CMD.Value = INIT_LOAD
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorControl)
        End Try

    End Sub

    Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
        Try
            MenuEnabled = True
            IsReturningFromChild = True
            State.searchDV = Nothing
        Catch ex As Exception
            HandleErrors(ex, ErrorControl)
        End Try
    End Sub

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public HasDataChanged As Boolean
        Public dealerId As Guid = Guid.Empty
        Public ObjectType As TargetType

        Public Sub New(LastOp As DetailPageCommand, dealerId As Guid, objectType As TargetType, hasDataChanged As Boolean)
            LastOperation = LastOp
            Me.HasDataChanged = hasDataChanged
            Me.dealerId = dealerId
            Me.ObjectType = objectType
        End Sub

        Public Enum TargetType
            Dealer
            CompanyGroup
        End Enum

    End Class
#End Region

#End Region

#Region "Datagrid Related "

    Protected Sub ItemBound(source As Object, e As DataGridItemEventArgs) Handles DataGridDropdowns.ItemDataBound

        Try
            BaseItemBound(source, e)

            If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
                If (e.Item.Cells(GRID_COL_ACTIVE_IDX).FindControl(GRID_CTL_ACTIVE) IsNot Nothing) Then
                    Dim YESNOdv As DataView = State.YESNOdv
                    Dim dropdownActive As DropDownList = CType(e.Item.Cells(GRID_COL_ACTIVE_IDX).FindControl(GRID_CTL_ACTIVE), DropDownList)
                    'Me.BindListControlToDataView(dropdownActive, YESNOdv)
                    Dim YESNOList As ListItem() = CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode())
                    dropdownActive.Populate(YESNOList, New PopulateOptions() With
                                          {
                                            .AddBlankItem = True
                                           })

                    Dim drv As DataRowView = CType(e.Item.DataItem, DataRowView)
                    If drv(ClaimStatusByGroupDAL.COL_NAME_ACTIVE_ID) IsNot DBNull.Value Then
                        dropdownActive.SelectedValue = GuidControl.ByteArrayToGuid(CType(drv(ClaimStatusByGroupDAL.COL_NAME_ACTIVE_ID), Byte())).ToString()
                    End If

                    EnableDisableControl(e.Item.Cells(GRID_COL_ACTIVE_IDX).FindControl(GRID_CTL_ACTIVE))
                End If

                If (e.Item.Cells(GRID_COL_OWNER_IDX).FindControl(GRID_CTL_OWNER) IsNot Nothing) Then
                    Dim OWNERdv As DataView = State.OWNERdv
                    Dim dropdownOwner As DropDownList = CType(e.Item.Cells(GRID_COL_OWNER_IDX).FindControl(GRID_CTL_OWNER), DropDownList)
                    'Me.BindListControlToDataView(dropdownOwner, OWNERdv)
                    Dim ownerList As ListItem() = CommonConfigManager.Current.ListManager.GetList("EXTOWN", Thread.CurrentPrincipal.GetLanguageCode())
                    dropdownOwner.Populate(ownerList, New PopulateOptions() With
                                          {
                                            .AddBlankItem = True
                                           })

                    Dim drv As DataRowView = CType(e.Item.DataItem, DataRowView)
                    If drv(ClaimStatusByGroupDAL.COL_NAME_OWNER_ID) IsNot DBNull.Value Then
                        dropdownOwner.SelectedValue = GuidControl.ByteArrayToGuid(CType(drv(ClaimStatusByGroupDAL.COL_NAME_OWNER_ID), Byte())).ToString()
                    End If

                    EnableDisableControl(e.Item.Cells(GRID_COL_OWNER_IDX).FindControl(GRID_CTL_OWNER))
                End If

                If (e.Item.Cells(GRID_COL_SKIPPING_ALLOWED_IDX).FindControl(GRID_CTL_SKIPPING_ALLOWED) IsNot Nothing) Then
                    Dim YESNOdv As DataView = State.YESNOdv
                    Dim dropdownSkippingAllowed As DropDownList = CType(e.Item.Cells(GRID_COL_SKIPPING_ALLOWED_IDX).FindControl(GRID_CTL_SKIPPING_ALLOWED), DropDownList)
                    ' Me.BindListControlToDataView(dropdownSkippingAllowed, YESNOdv)
                    Dim YESNOList As ListItem() = CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode())
                    dropdownSkippingAllowed.Populate(YESNOList, New PopulateOptions() With
                                          {
                                            .AddBlankItem = True
                                           })

                    Dim drv As DataRowView = CType(e.Item.DataItem, DataRowView)
                    If drv(ClaimStatusByGroupDAL.COL_NAME_SKIPPING_ALLOWED_ID) IsNot DBNull.Value Then
                        dropdownSkippingAllowed.SelectedValue = GuidControl.ByteArrayToGuid(CType(drv(ClaimStatusByGroupDAL.COL_NAME_SKIPPING_ALLOWED_ID), Byte())).ToString()
                    End If

                    EnableDisableControl(e.Item.Cells(GRID_COL_SKIPPING_ALLOWED_IDX).FindControl(GRID_CTL_SKIPPING_ALLOWED))
                End If

                If (e.Item.Cells(GRID_COL_STATUS_ORDER_IDX).FindControl(GRID_CTL_STATUS_ORDER) IsNot Nothing) Then
                    EnableDisableControl(e.Item.Cells(GRID_COL_STATUS_ORDER_IDX).FindControl(GRID_CTL_STATUS_ORDER))
                End If

                If (e.Item.Cells(GRID_COL_SELECTED_CHK_IDX).FindControl(GRID_CTL_SELECTED_CHKBOX) IsNot Nothing) Then
                    EnableDisableControl(e.Item.Cells(GRID_COL_SELECTED_CHK_IDX).FindControl(GRID_CTL_SELECTED_CHKBOX))
                End If

                If (e.Item.Cells(GRID_COL_GROUP_NUMBER_IDX).FindControl(GRID_CTL_GROUP_NUMBER) IsNot Nothing) Then
                    EnableDisableControl(e.Item.Cells(GRID_COL_GROUP_NUMBER_IDX).FindControl(GRID_CTL_GROUP_NUMBER))
                End If

                If (e.Item.Cells(GRID_COL_TURNAROUND_DAYS_IDX).FindControl(GRID_CTL_TURNAROUND_DAYS) IsNot Nothing) Then
                    EnableDisableControl(e.Item.Cells(GRID_COL_TURNAROUND_DAYS_IDX).FindControl(GRID_CTL_TURNAROUND_DAYS))
                End If

                If (e.Item.Cells(GRID_COL_TAT_REMINDER_HOURS_IDX).FindControl(GRID_CTL_TAT_REMINDER_HOURS) IsNot Nothing) Then
                    EnableDisableControl(e.Item.Cells(GRID_COL_TAT_REMINDER_HOURS_IDX).FindControl(GRID_CTL_TAT_REMINDER_HOURS))
                End If

            End If

        Catch ex As Exception
            HandleErrors(ex, ErrorControl)
        End Try
    End Sub

    Protected Sub EnableDisableControl(ctl As Control)
        If Not (permType = FormAuthorization.enumPermissionType.EDIT) Then
            ControlMgr.SetEnableControl(Me, CType(ctl, WebControl), False)
        End If
    End Sub

    Private Sub OnFromDrop_Changed(fromMultipleDrop As MultipleColumnDDLabelControl) _
        Handles multipleDropControl.SelectedDropChanged
        Try
            State.searchBy = SearchByType.Dealer
            State.dealerId = TheDealerControl.SelectedGuid

            If CMD.Value <> COPY_CLAIM_STATUS_GROUP Then
                CMD.Value = INIT_LOAD
                State.searchDV = Nothing
                PopulateGrid()
                SetButtonsState()
                State.isNew = "Y"
            End If

        Catch ex As Exception
            HandleErrors(ex, ErrorControl)
        End Try
    End Sub

    Public Sub DataGridDropdowns_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles DataGridDropdowns.ItemCommand
    End Sub

    Protected Sub ItemCreated(sender As Object, e As DataGridItemEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub DataGridDropdowns_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles DataGridDropdowns.PageIndexChanged
        Try
            DataGridDropdowns.CurrentPageIndex = e.NewPageIndex
            State.PageIndex = DataGridDropdowns.CurrentPageIndex
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorControl)
        End Try
    End Sub

#End Region

#Region "Button Clicks"
    Protected Sub SaveChanges()
        Dim i As Integer
        Dim DataChanged As Boolean = False
        Dim retVal As Integer
        Dim isFirstBO As Boolean = True
        Dim selectedCount As Integer = 0
        Dim emptyDealerId As Boolean = False
        Dim dtStatusOrder As New DataTable()
        dtStatusOrder.Columns.Add("StatusOrder")

        ErrorControl.Clear_Hide()
        ClearGridHeaders(DataGridDropdowns)
        ClearLabelErrSign(TheDealerControl.CaptionLabel)

        Try
            If State.searchBy = SearchByType.Dealer AndAlso State.dealerId.Equals(Guid.Empty) Then
                SetLabelError(TheDealerControl.CaptionLabel)
                emptyDealerId = True
                Throw New GUIException(Message.MSG_DEALER_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_DEALER_REQUIRED)
            End If

            For i = 0 To DataGridDropdowns.Items.Count - 1
                Dim statusOrderInt As Integer
                Dim groupNumberInt As Integer
                Dim statusOrder As String = CType(CType(DataGridDropdowns.Items(i).Cells(GRID_COL_STATUS_ORDER_IDX).FindControl(GRID_CTL_STATUS_ORDER), TextBox).Text, String)
                Dim listItemId As String = CType(DataGridDropdowns.Items(i).Cells(GRID_COL_SELECTED_IDX).FindControl(GRID_CTL_SELECTED_LISTITEMID), Label).Text
                Dim selected As Boolean = CType(DataGridDropdowns.Items(i).Cells(GRID_COL_SELECTED_CHK_IDX).FindControl(GRID_CTL_SELECTED_CHKBOX), CheckBox).Checked
                Dim ownerId As Guid = GetSelectedItem(CType(DataGridDropdowns.Items(i).Cells(GRID_COL_OWNER_IDX).FindControl(GRID_CTL_OWNER), DropDownList))
                Dim skippingAllowedId As Guid = GetSelectedItem(CType(DataGridDropdowns.Items(i).Cells(GRID_COL_SKIPPING_ALLOWED_IDX).FindControl(GRID_CTL_SKIPPING_ALLOWED), DropDownList))
                Dim activeId As Guid = GetSelectedItem(CType(DataGridDropdowns.Items(i).Cells(GRID_COL_ACTIVE_IDX).FindControl(GRID_CTL_ACTIVE), DropDownList))
                Dim isNew As String = CType(DataGridDropdowns.Items(i).Cells(GRID_COL_ISNEW_IDX).FindControl(GRID_CTL_ISNEW), Label).Text
                Dim groupNumber As String = CType(CType(DataGridDropdowns.Items(i).Cells(GRID_COL_GROUP_NUMBER_IDX).FindControl(GRID_CTL_GROUP_NUMBER), TextBox).Text, String)
                Dim turnaroundDays As String = CType(CType(DataGridDropdowns.Items(i).Cells(GRID_COL_TURNAROUND_DAYS_IDX).FindControl(GRID_CTL_TURNAROUND_DAYS), TextBox).Text, String)
                Dim tatReminderHours As String = CType(CType(DataGridDropdowns.Items(i).Cells(GRID_COL_TAT_REMINDER_HOURS_IDX).FindControl(GRID_CTL_TAT_REMINDER_HOURS), TextBox).Text, String)
               

                Dim drStatusOrder As DataRow = dtStatusOrder.NewRow()
                drStatusOrder(0) = statusOrder
                dtStatusOrder.Rows.Add(drStatusOrder)

                If selected Then
                    ' Checkbox selected
                    selectedCount = selectedCount + 1

                    If (isNew IsNot Nothing AndAlso isNew = "N") AndAlso CMD.Value <> COPY_CLAIM_STATUS_GROUP Then
                        ' Record exist for update

                        Dim dv As DataView = State.searchDV
                        Dim dr As DataRowView = GetDataRow(dv, listItemId)
                        Dim isDirty As Boolean = False

                        If dr(ClaimStatusByGroupDAL.COL_NAME_STATUS_ORDER) IsNot DBNull.Value Then
                            isDirty = isDirty OrElse (CType(dr(ClaimStatusByGroupDAL.COL_NAME_STATUS_ORDER), String) <> (CType(CType(DataGridDropdowns.Items(i).Cells(GRID_COL_STATUS_ORDER_IDX).FindControl(GRID_CTL_STATUS_ORDER), TextBox).Text, String)))
                        Else
                            isDirty = isDirty OrElse ("" <> (CType(CType(DataGridDropdowns.Items(i).Cells(GRID_COL_STATUS_ORDER_IDX).FindControl(GRID_CTL_STATUS_ORDER), TextBox).Text, String)))
                        End If

                        If dr(ClaimStatusByGroupDAL.COL_NAME_OWNER_ID) IsNot DBNull.Value Then
                            isDirty = isDirty OrElse Not ((New Guid(CType(dr(ClaimStatusByGroupDAL.COL_NAME_OWNER_ID), Byte()))).Equals(ownerId))
                        Else
                            isDirty = isDirty OrElse Not ((Guid.Empty).Equals(ownerId))
                        End If

                        If dr(ClaimStatusByGroupDAL.COL_NAME_SKIPPING_ALLOWED_ID) IsNot DBNull.Value Then
                            isDirty = isDirty OrElse Not ((New Guid(CType(dr(ClaimStatusByGroupDAL.COL_NAME_SKIPPING_ALLOWED_ID), Byte()))).Equals(skippingAllowedId))
                        Else
                            isDirty = isDirty OrElse Not ((Guid.Empty).Equals(skippingAllowedId))
                        End If

                        If dr(ClaimStatusByGroupDAL.COL_NAME_ACTIVE_ID) IsNot DBNull.Value Then
                            isDirty = isDirty OrElse Not ((New Guid(CType(dr(ClaimStatusByGroupDAL.COL_NAME_ACTIVE_ID), Byte()))).Equals(activeId))
                        Else
                            isDirty = isDirty OrElse Not ((Guid.Empty).Equals(activeId))
                        End If

                        If dr(ClaimStatusByGroupDAL.COL_NAME_GROUP_NUMBER) IsNot DBNull.Value Then
                            isDirty = isDirty OrElse (CType(dr(ClaimStatusByGroupDAL.COL_NAME_GROUP_NUMBER), String) <> (CType(CType(DataGridDropdowns.Items(i).Cells(GRID_COL_GROUP_NUMBER_IDX).FindControl(GRID_CTL_GROUP_NUMBER), TextBox).Text, String)))
                        Else
                            isDirty = isDirty OrElse ("" <> (CType(CType(DataGridDropdowns.Items(i).Cells(GRID_COL_GROUP_NUMBER_IDX).FindControl(GRID_CTL_GROUP_NUMBER), TextBox).Text, String)))
                        End If

                        If dr(ClaimStatusByGroupDAL.COL_NAME_TURNAROUND_DAYS) IsNot DBNull.Value Then
                            isDirty = isDirty OrElse (CType(dr(ClaimStatusByGroupDAL.COL_NAME_TURNAROUND_DAYS), String) <> (CType(CType(DataGridDropdowns.Items(i).Cells(GRID_COL_TURNAROUND_DAYS_IDX).FindControl(GRID_CTL_TURNAROUND_DAYS), TextBox).Text, String)))
                        Else
                            isDirty = isDirty OrElse ("" <> (CType(CType(DataGridDropdowns.Items(i).Cells(GRID_COL_TURNAROUND_DAYS_IDX).FindControl(GRID_CTL_TURNAROUND_DAYS), TextBox).Text, String)))
                        End If

                        If dr(ClaimStatusByGroupDAL.COL_NAME_TAT_REMINDER_HOURS) IsNot DBNull.Value Then
                            isDirty = isDirty OrElse (CType(dr(ClaimStatusByGroupDAL.COL_NAME_TAT_REMINDER_HOURS), String) <> (CType(CType(DataGridDropdowns.Items(i).Cells(GRID_COL_TAT_REMINDER_HOURS_IDX).FindControl(GRID_CTL_TAT_REMINDER_HOURS), TextBox).Text, String)))
                        Else
                            isDirty = isDirty OrElse ("" <> (CType(CType(DataGridDropdowns.Items(i).Cells(GRID_COL_TAT_REMINDER_HOURS_IDX).FindControl(GRID_CTL_TAT_REMINDER_HOURS), TextBox).Text, String)))
                        End If

                        If isDirty Then
                            Dim curBO As ClaimStatusByGroup

                            If dr(ClaimStatusByGroupDAL.COL_NAME_CLAIM_STATUS_BY_GROUP_ID) Is DBNull.Value Then
                                curBO = GetClaimStatusByGroupBO(isFirstBO, Guid.Empty)
                            Else
                                curBO = GetClaimStatusByGroupBO(isFirstBO, New Guid(CType(dr(ClaimStatusByGroupDAL.COL_NAME_CLAIM_STATUS_BY_GROUP_ID), Byte())))
                            End If

                            If Not Integer.TryParse(statusOrder, statusOrderInt) Then
                                'SetPageAndSelectedIndexFromGuid(Me.State.searchDV, New Guid(listItemId), Me.DataGridDropdowns, Me.State.PageIndex)
                                'Throw New GUIException(ERR_INVALID_STATUS_ORDER, ERR_INVALID_STATUS_ORDER)
                                statusOrderInt = 0
                            ElseIf (dtStatusOrder.Select("StatusOrder='" & statusOrder & "'").Length > 1) Then
                                statusOrderInt = 0
                            End If

                            If groupNumber IsNot String.Empty Then
                                Integer.TryParse(groupNumber, groupNumberInt)
                                curBO.GroupNumber = groupNumberInt
                            Else
                                curBO.GroupNumber = Nothing
                            End If

                            If Not String.IsNullOrEmpty(turnaroundDays)  Then
                                curBO.TurnaroundDays =  DecimalType.Parse(turnaroundDays)
                            End If

                            If Not String.IsNullOrEmpty(tatReminderHours)  Then
                                curBO.TurnaroundTimeReminderHours =  DecimalType.Parse(tatReminderHours)
                            End If

                            curBO.StatusOrder = statusOrderInt
                            curBO.OwnerId = ownerId
                            curBO.ActiveId = activeId
                            curBO.SkippingAllowedId = skippingAllowedId


                            BindBOPropertyToGridHeader(curBO, "OwnerId", DataGridDropdowns.Columns(GRID_COL_OWNER_IDX))
                            BindBOPropertyToGridHeader(curBO, "SkippingAllowedId", DataGridDropdowns.Columns(GRID_COL_SKIPPING_ALLOWED_IDX))
                            BindBOPropertyToGridHeader(curBO, "StatusOrder", DataGridDropdowns.Columns(GRID_COL_STATUS_ORDER_IDX))
                            BindBOPropertyToGridHeader(curBO, "ActiveId", DataGridDropdowns.Columns(GRID_COL_ACTIVE_IDX))
                            BindBOPropertyToGridHeader(curBO, "GroupNumber", DataGridDropdowns.Columns(GRID_COL_GROUP_NUMBER_IDX))
                            BindBOPropertyToGridHeader(curBO, "TurnaroundDays", DataGridDropdowns.Columns(GRID_COL_TURNAROUND_DAYS_IDX))
                            BindBOPropertyToGridHeader(curBO, "TurnaroundTimeReminderHours", DataGridDropdowns.Columns(GRID_COL_TAT_REMINDER_HOURS_IDX))

                            curBO.Validate()

                            DataChanged = True
                            isFirstBO = False

                        End If
                    Else
                        ' Record not exist for insert
                        Dim curBO As ClaimStatusByGroup
                        curBO = GetClaimStatusByGroupBO(isFirstBO, Guid.Empty)

                        If State.searchBy = ClaimStatusByGroupDAL.SearchByType.Dealer Then
                            curBO.DealerId = State.dealerId
                        ElseIf State.searchBy = ClaimStatusByGroupDAL.SearchByType.CompanyGroup Then
                            curBO.CompanyGroupId = Authentication.CurrentUser.CompanyGroup.Id
                        End If

                        If Not Integer.TryParse(statusOrder, statusOrderInt) Then
                            statusOrderInt = 0
                            'SetPageAndSelectedIndexFromGuid(Me.State.searchDV, New Guid(listItemId), Me.DataGridDropdowns, Me.State.PageIndex)
                            'Throw New GUIException(ERR_INVALID_STATUS_ORDER, ERR_INVALID_STATUS_ORDER)
                        ElseIf (dtStatusOrder.Select("StatusOrder='" & statusOrder & "'").Length > 1) Then
                            statusOrderInt = 0
                        End If

                        If groupNumber IsNot String.Empty Then
                            Integer.TryParse(groupNumber, groupNumberInt)
                        End If

                        If Not String.IsNullOrEmpty(turnaroundDays)  Then
                            curBO.TurnaroundDays =  DecimalType.Parse(turnaroundDays)
                        End If

                        If Not String.IsNullOrEmpty(tatReminderHours)  Then
                            curBO.TurnaroundTimeReminderHours =  DecimalType.Parse(tatReminderHours)
                        End If

                        curBO.StatusOrder = statusOrderInt
                        curBO.ListItemId = New Guid(listItemId)
                        curBO.OwnerId = ownerId
                        curBO.ActiveId = activeId
                        curBO.SkippingAllowedId = skippingAllowedId
                        curBO.GroupNumber = groupNumberInt

                        BindBOPropertyToGridHeader(curBO, "OwnerId", DataGridDropdowns.Columns(GRID_COL_OWNER_IDX))
                        BindBOPropertyToGridHeader(curBO, "SkippingAllowedId", DataGridDropdowns.Columns(GRID_COL_SKIPPING_ALLOWED_IDX))
                        BindBOPropertyToGridHeader(curBO, "StatusOrder", DataGridDropdowns.Columns(GRID_COL_STATUS_ORDER_IDX))
                        BindBOPropertyToGridHeader(curBO, "ActiveId", DataGridDropdowns.Columns(GRID_COL_ACTIVE_IDX))
                        BindBOPropertyToGridHeader(curBO, "GroupNumber", DataGridDropdowns.Columns(GRID_COL_GROUP_NUMBER_IDX))
                        BindBOPropertyToGridHeader(curBO, "TurnaroundDays", DataGridDropdowns.Columns(GRID_COL_TURNAROUND_DAYS_IDX))
                        BindBOPropertyToGridHeader(curBO, "TurnaroundTimeReminderHours", DataGridDropdowns.Columns(GRID_COL_TAT_REMINDER_HOURS_IDX))

                        curBO.Validate()
                        DataChanged = True
                        isFirstBO = False

                    End If

                Else
                    ' Checkbox deselected
                    If isNew IsNot Nothing AndAlso isNew = "N" Then
                        ' Record exist for deletion
                        If ClaimStatusByGroup.IsDeletable(listItemId, State.dealerId, Authentication.CurrentUser.CompanyGroup.Id, State.searchBy) Then
                            ' Delete record
                            Dim curBO As ClaimStatusByGroup
                            Dim dv As DataView = State.searchDV
                            Dim drLocal As DataRowView = GetDataRow(dv, listItemId)

                            If drLocal(ClaimStatusByGroupDAL.COL_NAME_CLAIM_STATUS_BY_GROUP_ID) Is DBNull.Value Then
                                curBO = GetClaimStatusByGroupBO(isFirstBO, Guid.Empty)
                            Else
                                curBO = GetClaimStatusByGroupBO(isFirstBO, New Guid(CType(drLocal(ClaimStatusByGroupDAL.COL_NAME_CLAIM_STATUS_BY_GROUP_ID), Byte())))
                            End If

                            BindBOPropertyToGridHeader(curBO, "OwnerId", DataGridDropdowns.Columns(GRID_COL_OWNER_IDX))
                            BindBOPropertyToGridHeader(curBO, "SkippingAllowedId", DataGridDropdowns.Columns(GRID_COL_SKIPPING_ALLOWED_IDX))
                            BindBOPropertyToGridHeader(curBO, "StatusOrder", DataGridDropdowns.Columns(GRID_COL_STATUS_ORDER_IDX))
                            BindBOPropertyToGridHeader(curBO, "ActiveId", DataGridDropdowns.Columns(GRID_COL_ACTIVE_IDX))
                            BindBOPropertyToGridHeader(curBO, "GroupNumber", DataGridDropdowns.Columns(GRID_COL_GROUP_NUMBER_IDX))
                            BindBOPropertyToGridHeader(curBO, "TurnaroundDays", DataGridDropdowns.Columns(GRID_COL_TURNAROUND_DAYS_IDX))
                            BindBOPropertyToGridHeader(curBO, "TurnaroundTimeReminderHours", DataGridDropdowns.Columns(GRID_COL_TAT_REMINDER_HOURS_IDX))

                            curBO.Validate()
                            curBO.Delete()

                            DataChanged = True
                            isFirstBO = False

                        Else
                            ' Show error
                            SetPageAndSelectedIndexFromGuid(State.searchDV, New Guid(listItemId), DataGridDropdowns, State.PageIndex)
                            Throw New GUIException(ERR_CLAIM_STATUS_GROUP_IN_USED, ERR_CLAIM_STATUS_GROUP_IN_USED)
                        End If
                    Else
                        If (statusOrder IsNot Nothing AndAlso statusOrder <> "") OrElse Not ownerId.Equals(Guid.Empty) OrElse Not skippingAllowedId.Equals(Guid.Empty) OrElse Not activeId.Equals(Guid.Empty) Then
                            ' Show error
                            SetPageAndSelectedIndexFromGuid(State.searchDV, New Guid(listItemId), DataGridDropdowns, State.PageIndex)
                            Throw New GUIException(ERR_CLAIM_STATUS_GROUP_NOT_CHECKED, ERR_CLAIM_STATUS_GROUP_NOT_CHECKED)
                        End If
                    End If
                End If
            Next

            If selectedCount = 0 AndAlso State.isNew = "Y" Then
                Throw New GUIException(MSG_NO_RECORD_SELECTED, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_NO_RECORD_SELECTED)
            Else
                If DataChanged Then
                    csbgFamilyBO.Save()
                    State.isNew = "N"
                    CMD.Value = ""
                    State.searchDV = Nothing
                    PopulateGrid()
                    AddInfoMsg(MSG_RECORD_SAVED_OK)
                Else
                    AddInfoMsg(MSG_RECORD_NOT_SAVED)
                End If
            End If

            csbgFamilyBO = Nothing

        Catch ex As Exception
            HandleErrors(ex, ErrorControl)
            If emptyDealerId Then
                PopulateDealer()
            End If
        End Try

    End Sub

    Private Function GetClaimStatusByGroupBO(isFirstBO As Boolean, claimStatusGroupId As Guid) As ClaimStatusByGroup

        If isFirstBO Then
            If claimStatusGroupId.Equals(Guid.Empty) Then
                csbgFamilyBO = New ClaimStatusByGroup
            Else
                csbgFamilyBO = New ClaimStatusByGroup(claimStatusGroupId)
            End If

            Return csbgFamilyBO
        Else

            Return csbgFamilyBO.AddClaimStatusByGroup(claimStatusGroupId)
        End If

    End Function

    Public Function GetDataRow(dv As DataView, listItemId As String) As DataRowView
        Dim dr As DataRowView = Nothing
        Dim retDr As DataRowView = Nothing
        Dim i As Integer = 0
        Dim guidStr As String = GuidControl.GuidToHexString(New Guid(listItemId))

        For Each dr In dv
            Dim drStr As String = GuidControl.GuidToHexString(New Guid(CType(dr(ClaimStatusByGroupDAL.COL_NAME_LIST_ITEM_ID), Byte())))
            If drStr = guidStr Then
                retDr = dr
            End If
        Next

        Return retDr
    End Function

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            ErrorControl.Clear_Hide()
            SaveChanges()
            ErrorControl.Show()
        Catch ex As Exception
            HandleErrors(ex, ErrorControl)
        End Try
    End Sub

    Private Sub btnButtomNew_WRITE_Click(sender As Object, e As EventArgs) Handles btnButtomNew_WRITE.Click
        Try
            State.searchBy = SearchByType.Dealer
            CMD.Value = NEW_CLAIM_STATUS_GROUP
            State.dealerId = Guid.Empty
            State.isNew = "Y"
            State.searchDV = Nothing
            SetHeaderControlStatus()
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorControl)
        End Try
    End Sub

    Private Sub btnCopy_WRITE_Click(sender As Object, e As EventArgs) Handles btnCopy_WRITE.Click
        Try
            CMD.Value = COPY_CLAIM_STATUS_GROUP
            CopyDealerId.Value = GuidControl.GuidToHexString(State.dealerId)
            State.isNew = "Y"
            State.dealerId = Guid.Empty
            SetHeaderControlStatus()
        Catch ex As Exception
            HandleErrors(ex, ErrorControl)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorControl)
        End Try
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            Back(DetailPageCommand.Back)
        Catch ex As ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrorControl)
        End Try
    End Sub

    Protected Sub Back(cmd As DetailPageCommand)
        If State.searchBy = SearchByType.CompanyGroup Then
            ReturnToCallingPage(New ReturnType(DetailPageCommand.Back, Guid.Empty, ReturnType.TargetType.CompanyGroup, False))
        Else
            ReturnToCallingPage(New ReturnType(DetailPageCommand.Back, State.dealerId, ReturnType.TargetType.Dealer, False))
        End If
    End Sub

#End Region

#Region "Controlling Logic"

    Protected Sub PopulateGrid()
        If State.searchDV Is Nothing Then
            State.searchDV = ClaimStatusByGroup.getListByCompanyGroupOrDealer(State.searchBy, Authentication.CurrentUser.CompanyGroup.Id, State.dealerId)
            'REQ-941
            Dim row As DataRow
            For Each row In State.searchDV.Table.Rows
                If row("SELECTED").ToString.Equals("Y") Then
                    btnActionSettings.Enabled = True
                    Exit For
                End If
            Next
        End If

        DataGridDropdowns.AutoGenerateColumns = False

        If State.searchBy = SearchByType.Dealer Then
            SetPageAndSelectedIndexFromGuid(State.searchDV, State.dealerId, DataGridDropdowns, State.PageIndex)
        Else
            SetPageAndSelectedIndexFromGuid(State.searchDV, Authentication.CurrentUser.CompanyGroup.Id, DataGridDropdowns, State.PageIndex)
        End If

        State.PageIndex = DataGridDropdowns.CurrentPageIndex
        DataGridDropdowns.DataSource = State.searchDV
        DataGridDropdowns.DataBind()
    End Sub

    Public Shared Sub SetLabelColor(lbl As Label)
        lbl.ForeColor = Color.Black
    End Sub

    Private Sub PopulateAll()
        Dim YESNOdv As DataView = LookupListNew.DropdownLookupList(YESNO, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
        State.YESNOdv = YESNOdv

        Dim OWNERdv As DataView = LookupListNew.DropdownLookupList(EXTOWN, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
        State.OWNERdv = OWNERdv

    End Sub

    Private Sub PopulateDealer()
        Try
            Dim oDealerview As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
            TheDealerControl.SetControl(True, _
                                        MultipleColumnDDLabelControl.MODES.NEW_MODE, _
                                        True, _
                                        oDealerview, _
                                        TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALERCODE), _
                                        True, True, _
                                        , _
                                        "multipleDropControl_moMultipleColumnDrop", _
                                        "multipleDropControl_moMultipleColumnDropDesc", _
                                        "multipleDropControl_lb_DropDown", _
                                        False, _
                                        0)

            TheDealerControl.SelectedGuid = State.dealerId
        Catch ex As Exception
            ErrorControl.AddError(BRANCH_LIST_FORM001)
            ErrorControl.AddError(ex.Message, False)
            ErrorControl.Show()
        End Try
    End Sub

    Private Sub SetHeaderControlStatus()
        If State.searchBy = SearchByType.Dealer Then
            ControlMgr.SetVisibleControl(Me, TheDealerControl, True)

            If State.isNew = "Y" Then
                TheDealerControl.ChangeEnabledControlProperty(True)
            Else
                TheDealerControl.ChangeEnabledControlProperty(False)
            End If

            ControlMgr.SetVisibleControl(Me, lblCompanyGroup, False)
            ControlMgr.SetVisibleControl(Me, txtCompanyGroup, False)

            PopulateDealer()
        Else
            ControlMgr.SetVisibleControl(Me, TheDealerControl, False)
            ControlMgr.SetVisibleControl(Me, lblCompanyGroup, True)
            ControlMgr.SetVisibleControl(Me, txtCompanyGroup, True)
            ControlMgr.SetEnableControl(Me, lblCompanyGroup, False)
            ControlMgr.SetEnableControl(Me, txtCompanyGroup, False)
            Dim arCompanyGroup As New ArrayList
            arCompanyGroup.Add(Authentication.CurrentUser.CompanyGroup.Id)
            txtCompanyGroup.Text = CType(LookupListNew.GetCompanyGroupNoptInUseLookupList(arCompanyGroup)(0)(CompanyGroupDAL.COL_NAME_DESCRIPTION), String)
        End If

    End Sub

    Private Sub SetButtonsState()

        If (State.searchBy = SearchByType.Dealer) Then
            ControlMgr.SetEnableControl(Me, btnButtomNew_WRITE, True)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, True)
            MenuEnabled = False
        Else
            ControlMgr.SetEnableControl(Me, btnButtomNew_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
            MenuEnabled = False
        End If

    End Sub

#End Region

    Protected Sub btnActionSettings_Click(sender As Object, e As EventArgs) Handles btnActionSettings.Click
        Try
            Dim params As New ArrayList

            params.Add(State.searchBy)
            params.Add(State.dealerId)
            params.Add(State.isNew)
            callPage(ClaimStatusAction.URL, params)
        Catch ex As ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Protected Sub btnDefaultStatuses_Click(sender As Object, e As EventArgs) Handles btnDefaultStatuses.Click
        Try
            Dim params As New ArrayList

            params.Add(State.searchBy)
            params.Add(State.dealerId)
            params.Add(State.isNew)
            callPage(DefaultClaimStatusForm.URL, params)
        Catch ex As ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub
End Class

