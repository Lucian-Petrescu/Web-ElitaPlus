Namespace Translation
    Partial Class AdminDropdownTranslationForm
        Inherits ElitaPlusSearchPage

#Region "Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents ErrorControl As ErrorController
        Public Const PAGE_NAME As String = "~/Translation/AdminDropdownTranslationForm.aspx"
        'Protected WithEvents TitleName As System.Web.UI.WebControls.Label

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Page State"
        Class MyState
            Public isADropdown As Boolean
            Public searchDV As DataView = Nothing
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

#Region "Constants "
        Private Const DG_TRANS_TRANSLATION As Integer = 0
        Private Const DG_DICT_ITEM_GUID_ID As Integer = 3
        Private Const DG_OLD_TRANSLATION As Integer = 6
        Private Const CANCEL_BUTTON As String = "Cancel"
        Protected Const CONFIRM_MSG As String = "MGS_CONFIRM_PROMPT" '"Are you sure you want to delete the selected dropdowns?"
        Private Const ANY_CHANGE_WIIL_BE_LOST As String = "Any change will be lost"
        Private Const CURRENT_CONTROL As String = "CURRENT_CONTROL"
        Private Const IS_OK As String = "Ok"
        Private Const DROPDOWN_ID As String = "DROPDOWN_ID"
        Private Const DROPDOWN_ITEM_NAME As String = "Dropdown Item Name"
        Private Const DROPDOWN_NAME As String = "Dropdown Name"
        Private Const COL_NAME_CODE As String = "CODE"
#End Region

#Region "Member Variables "
        Private mbSkipApplyEdit As Boolean = False
#End Region


#Region "Page Events"
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                Me.ErrorControl.Clear_Hide()

                If Not Page.IsPostBack Then
                    Me.ShowMissingTranslations(ErrorControl)

                    If Not Session(DROPDOWN_ID) Is Nothing Then
                        Me.State.isADropdown = True
                        Session(DROPDOWN_ID) = Nothing
                    End If
                    PopulateGrid()
                Else
                    CheckIfComingFromCancelConfirm()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorControl)
            End Try
        End Sub
#End Region

#Region "Datagrid Related "

        Public Sub ItemCreated(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
            '-------------------------------------
            'Name:ReasorbTranslation
            'Purpose:Translate any message to be display
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

        Public Function GetTransItemId(ByVal DICT_ITEM_TRANSLATION_ID As Byte()) As String

            Return GuidConversion.ConvertToString(DICT_ITEM_TRANSLATION_ID)

        End Function

#End Region

#Region "Button Clicks"
        Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

            Try
                If IsDirty() Then
                    ConfirmMessage(ANY_CHANGE_WIIL_BE_LOST)
                    Exit Sub
                End If
                ConfirmedCancel()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorControl)
            End Try
        End Sub

        Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
            '-------------------------------------
            'Name:btnSave
            'Purpose:Save any modification to the selected Label and/or to its translations and 
            '             return to the Labels grid 
            '-------------------------------------
            Try
                Me.ErrorControl.Clear_Hide()
                SaveChanges()
                Me.ErrorControl.Show()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorControl)
            End Try
        End Sub

        Private Sub SaveChanges()
            Dim sTranslation As String
            Dim nDictItemTranslationID As Guid
            Dim oLabel As Label
            Dim dropdownBO As New Dropdown
            Dim retVal As Integer
            Dim i As Integer
            Dim DataChanged As Boolean

            For i = 0 To Me.grdTranslation.Items.Count - 1
                sTranslation = CType(Me.grdTranslation.Items(i).Cells(DG_TRANS_TRANSLATION).FindControl("txtNewTranslation"), TextBox).Text
                oLabel = CType(Me.grdTranslation.Items(i).Cells(DG_DICT_ITEM_GUID_ID).FindControl("TransItemIDGuid"), Label)
                nDictItemTranslationID = New Guid(oLabel.Text)
                Dim isDirty As Boolean = False
                isDirty = isDirty Or (sTranslation.Trim <> Me.grdTranslation.Items(i).Cells(DG_OLD_TRANSLATION).Text.Trim)
                If isDirty Then
                    Try
                        retVal = dropdownBO.UpdateTranslation(nDictItemTranslationID, sTranslation.Trim, ElitaPlusIdentity.Current.ActiveUser.NetworkId)
                        If retVal = 0 Then
                            Me.MenuEnabled = True
                            DataChanged = True
                        Else
                            Me.ErrorControl.AddError(Message.ERR_SAVING_DATA)
                        End If
                    Catch ex As Exception
                        Me.ErrorControl.AddError(Message.ERR_SAVING_DATA)
                    End Try
                End If
            Next
            If DataChanged = True Then
                PopulateGrid()
            End If
        End Sub
#End Region

#Region "Controlling Logic"

        Private Sub ConfirmedCancel()
            ErrorControl.Clear_Hide()
            NavigationHistory.LastPage() 'does a pop of the last page
            Me.ReturnToCallingPage()
        End Sub

        Protected Sub LoadData()
            If Me.State.isADropdown = True Then
                'Load List data
                Me.State.searchDV = Dropdown.AdminLoadListTranslation(CType(Me.CallingParameters, Guid))
            Else
                'Load List Item data
                Me.State.searchDV = DropdownItem.AdminLoadListItemTranslation(CType(Me.CallingParameters, Guid))
            End If
        End Sub

        Private Sub PopulateGrid()
            '-------------------------------------
            'Name:PopulateGrid
            'Purpose:Load translations in the grid for the selected Label 
            '-------------------------------------
            Me.ErrorControl.Clear_Hide()
            LoadData()
            If Me.State.isADropdown Then
                grdTranslation.DataSource = Me.State.searchDV
                Me.TitleName.Text = DROPDOWN_NAME
                If Me.State.searchDV.Count > 0 Then
                    Me.lblDropdownItemNam.Text = Me.State.searchDV.Item(0)(COL_NAME_CODE).ToString
                End If
            Else
                grdTranslation.DataSource = Me.State.searchDV
                Me.TitleName.Text = DROPDOWN_ITEM_NAME
                If Me.State.searchDV.Count > 0 Then
                    Me.lblDropdownItemNam.Text = Me.State.searchDV.Item(0)(COL_NAME_CODE).ToString
                End If
            End If

            grdTranslation.DataBind()

        End Sub

        Protected Sub CheckIfComingFromCancelConfirm()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
            If Not confResponse Is Nothing AndAlso confResponse = Me.CONFIRM_MESSAGE_OK Then
                ConfirmedCancel()
            End If
            Me.HiddenSaveChangesPromptResponse.Value = ""
        End Sub

        Private Function IsDirty() As Boolean
            Dim sTranslation As String
            Dim objItem As DataGridItem
            Dim i As Integer
            For i = 0 To Me.grdTranslation.Items.Count - 1
                sTranslation = CType(Me.grdTranslation.Items(i).Cells(DG_TRANS_TRANSLATION).FindControl("txtNewTranslation"), TextBox).Text
                Dim isPageDirty As Boolean = False
                isPageDirty = isPageDirty Or (sTranslation.Trim.ToUpper <> Me.grdTranslation.Items(i).Cells(DG_OLD_TRANSLATION).Text.Trim.ToUpper)
                If isPageDirty Then
                    Return True
                End If
            Next
        End Function

        Private Sub ConfirmMessage(ByVal Message As String)
            Me.AddConfirmMsg(Message, HiddenSaveChangesPromptResponse)
            Me.DisplayMessage(Message, "", Me.MSG_BTN_OK_CANCEL, Me.MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
            Me.ErrorControl.Show()
        End Sub
#End Region

    End Class
End Namespace