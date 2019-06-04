Namespace Translation
    Partial Class MaintainDropdownItemForm
        Inherits ElitaPlusPage

        Protected WithEvents Button1 As System.Web.UI.WebControls.Button
        Protected WithEvents Button2 As System.Web.UI.WebControls.Button
        Protected WithEvents SelectDropDownLabel As System.Web.UI.WebControls.Label
        Protected WithEvents ErrorControl As ErrorController

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

#Region " Constants "
        Protected Const CONFIRM_MSG As String = "Are you sure you want to delete the selected dropdowns?"
        Public Const URL As String = "~/Translation/MaintainDropdownItemForm.aspx"
        Private Const NEW_TRANS_VALUE_CIDX As Integer = 2

        Private Const OLD_TRANS_VALUE_CIDX As Integer = 4
        Private Const DICT_ITEM_GUID_ID_CIDX As Integer = 5
        Private Const DROPDOWN_ITEM_ID_CIDX As Integer = 0

        Public Const PAGE_NAME As String = "~/Translation/MaintainDropdownItemForm.aspx"

#End Region
#Region "Parameters"

        Public Class Parameters
            Public moLookupId As Guid
            Public moCompanyId As Guid

            Public Sub New(ByVal oLookupId As Guid, ByVal oCompanyId As Guid)
                moCompanyId = oCompanyId
                moLookupId = oLookupId
            End Sub

        End Class

#End Region

#Region "Page State"
        Class MyState
            Public moParms As Parameters
            Public DropdownParentId As Guid
            Public UserCompanyLanguageId As Guid
            Public searchDV As DataView = Nothing
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


        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                If Not Me.IsPostBack Then
                    'Disable the Menu Navigation on this page to force the exit only by Cancel
                    Me.MenuEnabled = False
                    Me.LoadData()
                    Me.PopulateGrid()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorControl)
            End Try
        End Sub

        Protected Sub LoadData()
            'Me.State.DropdownParentId = CType(Me.CallingParameters, Guid)
            Me.State.moParms = CType(Me.CallingParameters, Parameters)
            'Me.State.UserCompanyLanguageId = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            'Me.State.UserCompanyLanguageId = ElitaPlusIdentity.Current.ActiveUser.Company.LanguageId
            Me.State.DropdownParentId = CType(Me.State.moParms.moLookupId, Guid)
            Me.State.UserCompanyLanguageId = CType(Me.State.moParms.moCompanyId, Guid)
        End Sub

        Protected Sub PopulateGrid()
            Me.State.searchDV = DropdownItem.AdminLoadListItems(Me.State.UserCompanyLanguageId, Me.State.DropdownParentId)
            If Not Me.State.DropdownParentId.Equals(Guid.Empty) Then
                Dim dropdownBO As New Dropdown(Me.State.DropdownParentId)
                Me.DropdownName.Text = dropdownBO.GetTranslation(Me.State.UserCompanyLanguageId, dropdownBO.DictItemId)
            End If
            Me.State.searchDV.RowFilter = "MAINTAINABLE_BY_USER='Y'"
            Me.DataGridDropdownItems.DataSource = Me.State.searchDV
            Me.DataGridDropdownItems.AutoGenerateColumns = False
            Me.DataGridDropdownItems.Columns(2).HeaderText = LookupListNew.GetDescriptionFromId(LookupListNew.LK_LANGUAGES, Me.State.UserCompanyLanguageId)
            Me.DataGridDropdownItems.DataBind()
            ControlMgr.DisableAllGridControlsIfNotEditAuth(Me, DataGridDropdownItems)
        End Sub

        Protected Sub SaveChanges()
            Dim DataChanged As Boolean = False
            Dim i As Integer
            Dim dropdownBO As New Dropdown
            Dim retVal As Integer
            For i = 0 To Me.DataGridDropdownItems.Items.Count - 1
                Dim newLangTransValue As String = CType(Me.DataGridDropdownItems.Items(i).Cells(Me.NEW_TRANS_VALUE_CIDX).FindControl("TextBoxLangTrans"), TextBox).Text
                'comparing to the original values saved in hidden columns
                Dim isDirty As Boolean = False
                isDirty = isDirty Or (newLangTransValue.Trim.ToUpper <> Me.DataGridDropdownItems.Items(i).Cells(Me.OLD_TRANS_VALUE_CIDX).Text.Trim.ToUpper)
                If isDirty Then
                    Dim DropdownItemId As New Guid(CType(Me.DataGridDropdownItems.Items(i).Cells(Me.DROPDOWN_ITEM_ID_CIDX).FindControl("lblListItemId"), Label).Text)
                    Dim nDictItemTranslationID As New Guid(CType(Me.DataGridDropdownItems.Items(i).Cells(Me.DICT_ITEM_GUID_ID_CIDX).FindControl("lblDictItemTransId"), Label).Text)
                    Try
                        retVal = dropdownBO.UpdateTranslation(nDictItemTranslationID, newLangTransValue.Trim, ElitaPlusIdentity.Current.ActiveUser.NetworkId)
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
            If DataChanged Then
                Me.PopulateGrid()
            End If
        End Sub

        Private Sub btnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
            Try
                Me.ErrorControl.Clear_Hide()
                Me.SaveChanges()
                Me.ErrorControl.Show()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorControl)
            End Try
        End Sub

        Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
            Try
                NavigationHistory.LastPage()
                Me.ReturnToCallingPage()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorControl)
            End Try
        End Sub

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
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorControl)
            End Try
        End Sub
    End Class
End Namespace

