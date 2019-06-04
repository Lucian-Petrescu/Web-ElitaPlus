
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Translation

Namespace Tables

    Partial Class DeviceItemsForm
        Inherits ElitaPlusSearchPage
        Protected WithEvents Button1 As System.Web.UI.WebControls.Button
        Protected WithEvents Button2 As System.Web.UI.WebControls.Button
        Protected WithEvents Label7 As System.Web.UI.WebControls.Label
        Protected WithEvents SelectDropDownLabel As System.Web.UI.WebControls.Label
        Protected WithEvents ErrorControl As ErrorController

        Public Const PAGE_NAME As String = "~/Tables/DeviceItemsForm.aspx"

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
        Protected Const CONFIRM_MSG As String = "MGS_CONFIRM_PROMPT" '"Are you sure you want to delete the selected dropdowns?"

        Private Const NEW_PROGCODE_CIDX As Integer = 1
        Private Const NEW_TRANS_VALUE_CIDX As Integer = 2
        Private Const NEW_MAINT_BY_USER_CIDX As Integer = 3
        Private Const NEW_DISP_TO_USER_CIDX As Integer = 4
        Private Const ITEMS_CIDX As Integer = 5
        Private Const OLD_PROGCODE_CIDX As Integer = 7
        Private Const OLD_TRANS_VALUE_CIDX As Integer = 10
        Private Const OLD_MAINT_BY_USER_CIDX As Integer = 8
        Private Const OLD_DISP_TO_USER_CIDX As Integer = 9

        Private Const DROPDOWN_ITEM_ID_CIDX As Integer = 6
        Private Const DROPDOWN_LIST_ID_CIDX As Integer = 11
        Private Const SELECTED_CIDX As Integer = 0

        Private Const DROPDOWN_PARENT_ID As String = "DROPDOWN_PARENT_ID"

#End Region


#Region "Page State"
        Class MyState
            Public PageIndex As Integer = 0
            Public DropdownParentId As Guid
            Public DropdownParentCode As String
            Public searchDV As DataView = Nothing
            Public ListItemId As Guid = Guid.Empty
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

#Region "Page Events"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                If Not Me.IsPostBack Then
                    'Me.ShowMissingTranslations(ErrorControl)
                    'Disable the Menu Navigation on this page to force the exit only by Cancel
                    Me.MenuEnabled = False
                    If Not String.IsNullOrEmpty(CallingParameters) Then
                        If Not CType(Me.CallingParameters, String).Equals(String.Empty) Then
                            Me.State.DropdownParentCode = CType(Me.CallingParameters, String)
                        End If
                    End If
                    Me.PopulateGrid()

                    Me.AddConfirmation(btnDelete, Me.CONFIRM_MSG)
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, ErrorControl)
            End Try
        End Sub

        Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
            'Enable the Menu Navigation Back after returning from the child
            Try
                ' CType(Me.CallingParameters, Guid)
                Me.MenuEnabled = True
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorControl)
            End Try
        End Sub
#End Region

#Region "Controlling Logic"

        Protected Sub PopulateGrid()
            Dim EngLangId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetLanguageLookupList(), Codes.ENGLISH_LANG_CODE)
            If Not Me.State.DropdownParentCode.Equals(String.Empty) Then
                'Dim dropdownBO As New Dropdown(Me.State.DropdownParentId)
                'Me.DropdownName.Text = Me.State.DropdownParentCode
            End If

            Me.State.searchDV = DropdownItem.DeviceLoadListItems(EngLangId, Me.State.DropdownParentCode)
            Me.DataGridDropdownItems.AutoGenerateColumns = False
            SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.ListItemId, Me.DataGridDropdownItems, Me.State.PageIndex)
            Me.State.PageIndex = Me.DataGridDropdownItems.CurrentPageIndex
            Me.DataGridDropdownItems.DataSource = Me.State.searchDV
            Me.DataGridDropdownItems.DataBind()

            Dim i As Integer
            For i = 0 To Me.DataGridDropdownItems.Items.Count - 1
                Dim DropdownListId As New Guid(CType(Me.DataGridDropdownItems.Items(i).Cells(Me.DROPDOWN_LIST_ID_CIDX).FindControl("lblListId"), Label).Text)
                Me.State.DropdownParentId = DropdownListId
            Next

            Me.ErrorControl.Show()
        End Sub

        Public Shared Sub SetLabelColor(ByVal lbl As Label)
            lbl.ForeColor = Color.Black
        End Sub
#End Region

#Region "Button Clicks"
        Protected Sub SaveChanges()
            Dim i As Integer
            Dim DataChanged As Boolean = False
            Dim strNewMaintByUserValue As String
            Dim strNewDispToUserValue As String
            Dim dropdownBO As New DropdownItem
            Dim retVal As Integer

            For i = 0 To Me.DataGridDropdownItems.Items.Count - 1
                Dim newProgCodeValue As String = CType(Me.DataGridDropdownItems.Items(i).Cells(Me.NEW_PROGCODE_CIDX).FindControl("TextBoxProgCode"), TextBox).Text
                Dim newEngTransValue As String = CType(Me.DataGridDropdownItems.Items(i).Cells(Me.NEW_TRANS_VALUE_CIDX).FindControl("TextBoxEngTrans"), TextBox).Text
                Dim newMaintByUserValue As Boolean = CType(Me.DataGridDropdownItems.Items(i).Cells(Me.NEW_MAINT_BY_USER_CIDX).FindControl("CheckBoxMaintainableByUser"), CheckBox).Checked
                Dim newDispToUserValue As Boolean = CType(Me.DataGridDropdownItems.Items(i).Cells(Me.NEW_DISP_TO_USER_CIDX).FindControl("CheckBoxDisplayToUser"), CheckBox).Checked
                If newMaintByUserValue = True Then
                    strNewMaintByUserValue = Codes.YESNO_Y
                Else
                    strNewMaintByUserValue = Codes.YESNO_N
                End If

                If newDispToUserValue = True Then
                    strNewDispToUserValue = Codes.YESNO_Y
                Else
                    strNewDispToUserValue = Codes.YESNO_N
                End If

                'comparing to the original values saved in hidden columns
                Dim isDirty As Boolean = False
                isDirty = isDirty Or (newProgCodeValue.Trim.ToUpper <> Me.DataGridDropdownItems.Items(i).Cells(Me.OLD_PROGCODE_CIDX).Text.Trim.ToUpper)
                isDirty = isDirty Or (newEngTransValue.Trim.ToUpper <> Me.DataGridDropdownItems.Items(i).Cells(Me.OLD_TRANS_VALUE_CIDX).Text.Trim.ToUpper)
                isDirty = isDirty Or (strNewMaintByUserValue <> Me.DataGridDropdownItems.Items(i).Cells(Me.OLD_MAINT_BY_USER_CIDX).Text.Trim.ToUpper)
                isDirty = isDirty Or (strNewDispToUserValue <> Me.DataGridDropdownItems.Items(i).Cells(Me.OLD_DISP_TO_USER_CIDX).Text.Trim.ToUpper)
                If isDirty Then
                    Dim DropdownItemId As New Guid(CType(Me.DataGridDropdownItems.Items(i).Cells(Me.DROPDOWN_ITEM_ID_CIDX).FindControl("lblListItemId"), Label).Text)
                    Try
                        retVal = dropdownBO.UpdateDropdownItem(DropdownItemId, newProgCodeValue.Trim.ToUpper, strNewMaintByUserValue, strNewDispToUserValue, newEngTransValue.Trim, ElitaPlusIdentity.Current.ActiveUser.NetworkId)
                        If retVal = 0 Then
                            DataChanged = True
                            'Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                        Else
                            Me.ErrorControl.AddError(Message.ERR_SAVING_DATA)
                        End If
                    Catch ex As Exception
                        Me.ErrorControl.AddError(Message.ERR_SAVING_DATA)
                    End Try
                End If
            Next
            If DataChanged Then
                Me.PopulateGrid()
            End If

        End Sub

        Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
            Try
                Me.ErrorControl.Clear_Hide()
                Me.SaveChanges()
                Me.ErrorControl.Show()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorControl)
            End Try
        End Sub

        Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
            Me.ErrorControl.Clear_Hide()
            Try
                Dim i As Integer
                Dim deleteCount As Integer = 0
                Dim totalChecked As Integer = 0
                Dim retVal As Integer
                Dim dropdownBO As New DropdownItem
                For i = 0 To Me.DataGridDropdownItems.Items.Count - 1
                    If CType(Me.DataGridDropdownItems.Items(i).Cells(Me.SELECTED_CIDX).FindControl("CheckBoxItemSel"), CheckBox).Checked Then
                        totalChecked += 1
                        Dim DropdownItemId As New Guid(CType(Me.DataGridDropdownItems.Items(i).Cells(Me.DROPDOWN_ITEM_ID_CIDX).FindControl("lblListItemId"), Label).Text)
                        'Me.State.DropdownParentId = DropdownItemId
                        Try
                            retVal = dropdownBO.DeleteDropdownItem(DropdownItemId)
                            If retVal = 0 Then
                                deleteCount += 1
                            Else
                                Me.ErrorControl.AddError(Message.ERR_DELETING_DATA)
                            End If
                        Catch ex As Exception
                            Me.ErrorControl.AddError(Message.ERR_DELETING_DATA)
                        End Try
                    End If
                Next
                Me.PopulateGrid()
                Dim ProcessingResultMsg As String = TranslationBase.TranslateLabelOrMessage(Message.BATCH_DELETE_PROCESS)
                ProcessingResultMsg = ProcessingResultMsg.Replace("?1", deleteCount.ToString)
                ProcessingResultMsg = ProcessingResultMsg.Replace("?2", totalChecked.ToString)
                Me.AddInfoMsg(ProcessingResultMsg, False)
                Me.ErrorControl.Show()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorControl)
            End Try
        End Sub

        Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
            Try
                NavigationHistory.LastPage() 'does a pop of the last page
                Me.ReturnToCallingPage()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorControl)
            End Try
        End Sub

        Private Sub bntAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles bntAdd.Click
            Me.ErrorControl.Clear_Hide()
            Try
                Dim dropdownBO As New DropdownItem
                Dim retVal As Integer
                If Me.TextBoxNewProgCode.Text.Trim = String.Empty Then
                    'display error
                    ElitaPlusPage.SetLabelError(Me.LabelNewProgCode)
                    Throw New GUIException(Message.MSG_INVALID_AUTHORIZED_AMOUNT_ERR, Assurant.ElitaPlus.Common.ErrorCodes.GUI_CODE_IS_REQUIED_ERR)
                End If

                If Me.TextBoxDescription.Text.Trim = String.Empty Then
                    'display error
                    ElitaPlusPage.SetLabelError(Me.LabelDescription)
                    Throw New GUIException(Message.MSG_INVALID_AUTHORIZED_AMOUNT_ERR, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DESCRIPTION_IS_REQUIED_ERR)
                End If

                If Me.State.DropdownParentId.Equals(Guid.Empty) Then
                    Me.State.DropdownParentId = LookupListNew.GetIdFromCode(LookupListNew.LK_DEVICE_GROUPS, Me.State.DropdownParentCode)
                End If

                retVal = dropdownBO.AddDropdownItem(Me.TextBoxNewProgCode.Text, Codes.YESNO_Y, Codes.YESNO_Y, Me.State.DropdownParentId, Me.TextBoxDescription.Text, ElitaPlusIdentity.Current.ActiveUser.NetworkId)
                If retVal = 0 Then
                    Me.AddInfoMsg(Message.RECORD_ADDED_OK)
                    PopulateGrid()
                    Me.TextBoxNewProgCode.Text = String.Empty
                    Me.TextBoxDescription.Text = String.Empty
                Else
                    Me.ErrorControl.AddError(Message.ERR_SAVING_DATA)
                End If
                SetLabelColor(Me.LabelNewProgCode)
                SetLabelColor(Me.LabelDescription)
                Me.ErrorControl.Show()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorControl)
            End Try
        End Sub
#End Region

#Region "Datagrid Related "
        Public Sub ItemCreated(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
            '-------------------------------------
            'Name:ReasorbTranslation
            'Purpose:Translate any message tobe display
            'Input Values: Message
            'Uses:
            '-------------------------------------
            ' get the type of item being created
            Dim elemType As ListItemType = e.Item.ItemType

            ' make sure it is the pager bar
            If elemType = ListItemType.Pager Then
                ' the pager bar as a whole has the follwoing layout
                ' <TR><TD colspan=x>.....links</TD></TR>
                ' item points to <TR>. The code below moves to <TD>
                Dim pager As TableCell = DirectCast(e.Item.Controls(0), TableCell)
                Dim i As Int32 = 0
                Dim bFound As Boolean = False
                For i = 0 To pager.Controls.Count
                    Dim obj As Object = pager.Controls(i)

                    If obj.GetType.ToString = "System.Web.UI.WebControls.DataGridLinkButton" Then

                        Dim h As LinkButton = CType(obj, LinkButton)
                        'h.Text = "[" & h.Text & "]"

                        If h.Text.Equals("...") Then
                            If Not bFound Then
                                'h.Text = "<"
                                'h.Text = ".        .."
                                h.ToolTip = "Previous set of pages"
                                h.Style.Add("BACKGROUND-REPEAT", "no-repeat")
                                h.Style.Add("COLOR", "#dee3e7")
                                h.Style.Add("BACKGROUND-IMAGE", "url(../Navigation/images/arrow_back.gif)")
                                bFound = True
                            Else
                                'h.Text = ">"
                                h.ToolTip = "Next set of pages"
                                h.Text = ".        .."
                                h.Style.Add("COLOR", "#dee3e7")
                                h.Style.Add("BACKGROUND-REPEAT", "no-repeat")
                                h.Style.Add("BACKGROUND-IMAGE", "url(../Navigation/images/arrow_foward.gif)")
                            End If

                        End If

                    Else
                        bFound = True
                        Dim l As System.Web.UI.WebControls.Label = CType(obj, System.Web.UI.WebControls.Label)
                        l.Text = "Page" & l.Text
                        'l.ForeColor = Color.Black
                        'l.Style.Add("FONT-WEIGHT", "BOLD")
                    End If

                    i += 1
                Next
            Else

            End If
        End Sub

        Private Sub DataGridDropdownItems_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridDropdownItems.PageIndexChanged
            Try
                Me.DataGridDropdownItems.CurrentPageIndex = e.NewPageIndex
                Me.State.PageIndex = Me.DataGridDropdownItems.CurrentPageIndex
                PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorControl)
            End Try
        End Sub

        Public Sub DataGridDropdownItems_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGridDropdownItems.ItemCommand
            Try
                If e.CommandName = "ItemsCMD" Then
                    Dim DropdownItemId As New Guid(CType(Me.DataGridDropdownItems.Items(e.Item.ItemIndex).Cells(Me.DROPDOWN_ITEM_ID_CIDX).FindControl("lblListItemId"), Label).Text)
                    Session(DROPDOWN_PARENT_ID) = Me.State.DropdownParentId
                    Me.callPage(AdminDropdownTranslationForm.PAGE_NAME, DropdownItemId)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorControl)
            End Try
        End Sub
#End Region
    End Class
End Namespace