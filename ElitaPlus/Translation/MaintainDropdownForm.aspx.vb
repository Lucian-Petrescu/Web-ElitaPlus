Namespace Translation
    Partial Class MaintainDropdownForm
        Inherits ElitaPlusPage
        Protected WithEvents Label1 As System.Web.UI.WebControls.Label
        Protected WithEvents Button1 As System.Web.UI.WebControls.Button
        Protected WithEvents Button2 As System.Web.UI.WebControls.Button
        Protected WithEvents Label2 As System.Web.UI.WebControls.Label
        Protected WithEvents ErrorController As ErrorController

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
#Region "Parameters"

        Public Class Parameters
            Public moLookupId As Guid
            Public moCompanyId As Guid

            Public Sub New(ByVal oCompanyId As Guid, ByVal oLookupId As Guid)
                moCompanyId = oCompanyId
                moLookupId = oLookupId
            End Sub

        End Class

#End Region
#Region " Constants "
        Protected Const CONFIRM_MSG As String = "Are you sure you want to delete the selected dropdowns?"

        Private Const NEW_TRANS_VALUE_CIDX As Integer = 2
        Private Const OLD_TRANS_VALUE_CIDX As Integer = 5
        Private Const DICT_ITEM_GUID_ID_CIDX As Integer = 6
        Private Const DROPDOWN_ID_CIDX As Integer = 0
        Private Const LABEL_COMPANY As String = "COMPANY"
        Private Const ITEMS_CIDX As Integer = 3
        Private Const COL_NAME_LANGUAGE As String = "LANG_TRANSLATION"

#End Region

#Region "Page State"
        Class MyState
            Public moParms As Parameter
            Public UserCompanyLanguageId As Guid
            Public moDropdownGuid As Guid
            Public PageIndex As Integer = 0
            Public searchDV As DataView = Nothing
            Public ListId As Guid = Guid.Empty
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
                    Me.ShowMissingTranslations(ErrorController)
                    Me.MenuEnabled = False
                    Me.LoadData()
                    Me.PopulateGrid()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorController)
            End Try
        End Sub

        Protected Sub LoadData()

            PopulateDropdowns()

        End Sub
        Protected Sub PopulateDropdowns()


            Try
                moCompanyMultipleDrop.SetControl(True, moCompanyMultipleDrop.MODES.NEW_MODE, True, LookupListNew.GetUserCompaniesLookupList(), TranslationBase.TranslateLabelOrMessage(LABEL_COMPANY), True)
                
            Catch ex As Exception
                Me.ErrControllerMaster.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.DB_ERROR)
            End Try

            
        End Sub
        Protected Sub PopulateGrid()

            'Dim moCompanyLangId As Guid
            Dim MyCompanyLang As Company

            If Not moCompanyMultipleDrop.SelectedGuid.Equals(Guid.Empty) Then
                MyCompanyLang = New Company(moCompanyMultipleDrop.SelectedGuid)
                Me.State.UserCompanyLanguageId = MyCompanyLang.LanguageId
            End If

            If Not Me.State.moDropdownGuid.Equals(Guid.Empty) Then
                moCompanyMultipleDrop.SelectedGuid = Me.State.moDropdownGuid
            End If

            Me.State.searchDV = Dropdown.AdminLoadList(Me.State.UserCompanyLanguageId)
            Me.State.searchDV.RowFilter = "MAINTAINABLE_BY_USER='Y'"
            Me.DataGridDropdowns.DataSource = Me.State.searchDV
            Me.DataGridDropdowns.AutoGenerateColumns = False
            If Not Me.State.UserCompanyLanguageId.Equals(Guid.Empty) Then
                Me.DataGridDropdowns.Columns(2).HeaderText = LookupListNew.GetDescriptionFromId(LookupListNew.LK_LANGUAGES, Me.State.UserCompanyLanguageId)
            End If
            Me.DataGridDropdowns.CurrentPageIndex = Me.State.PageIndex
            Me.DataGridDropdowns.DataBind()
            ControlMgr.DisableAllGridControlsIfNotEditAuth(Me, DataGridDropdowns)
        End Sub

        Protected Sub SaveChanges()
            Dim DataChanged As Boolean = False
            Dim i As Integer
            Dim dropdownBO As New Dropdown
            Dim retVal As Integer
            For i = 0 To Me.DataGridDropdowns.Items.Count - 1
                Dim newLangTransValue As String = CType(Me.DataGridDropdowns.Items(i).Cells(Me.NEW_TRANS_VALUE_CIDX).FindControl("TextBoxLangTrans"), TextBox).Text
                'comparing to the original values saved in hidden columns
                Dim isDirty As Boolean = False
                isDirty = (newLangTransValue.Trim.ToUpper <> Me.DataGridDropdowns.Items(i).Cells(Me.OLD_TRANS_VALUE_CIDX).Text.Trim.ToUpper)
                If isDirty Then
                    Dim DropdownId As New Guid(CType(Me.DataGridDropdowns.Items(i).Cells(Me.DROPDOWN_ID_CIDX).FindControl("lblListId"), Label).Text)
                    Dim nDictItemTranslationID As New Guid(CType(Me.DataGridDropdowns.Items(i).Cells(Me.DICT_ITEM_GUID_ID_CIDX).FindControl("lblDictItemTransId"), Label).Text)
                    Try
                        retVal = dropdownBO.UpdateTranslation(nDictItemTranslationID, newLangTransValue.Trim, ElitaPlusIdentity.Current.ActiveUser.NetworkId)
                        If retVal = 0 Then
                            Me.MenuEnabled = True
                            DataChanged = True
                        Else
                            Me.ErrorController.AddError(Message.ERR_SAVING_DATA)
                        End If
                    Catch ex As Exception
                        Me.ErrorController.AddError(Message.ERR_SAVING_DATA)
                    End Try
                End If
            Next
            If DataChanged Then
                Me.PopulateGrid()
            End If
        End Sub

        Private Sub btnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
            Try
                Me.ErrorController.Clear_Hide()
                Me.SaveChanges()
                Me.ErrorController.Show()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorController)
            End Try
        End Sub

        Private Sub btnCancel_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel_WRITE.Click
            Try
                Me.Response.Redirect(NavigationHistory.LastPage)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorController)
            End Try
        End Sub

        Public Sub DataGridDropdowns_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGridDropdowns.ItemCommand
            Try
                If e.CommandName = "ItemsCMD" Then
                    Dim DropdownId As New Guid(CType(Me.DataGridDropdowns.Items(e.Item.ItemIndex).Cells(Me.DROPDOWN_ID_CIDX).FindControl("lblListId"), Label).Text)
                    
                    Me.callPage(Translation.MaintainDropdownItemForm.URL, New Translation.MaintainDropdownItemForm.Parameters(DropdownId, Me.State.UserCompanyLanguageId))
                    'Me.callPage(MaintainDropdownItemForm.PAGE_NAME, DropdownId)
                End If
            Catch ex As Exception

                Me.HandleErrors(ex, Me.ErrorController)
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
                If elemType = ListItemType.AlternatingItem Then
                    Dim objButton As Button

                    objButton = DirectCast(e.Item.Cells(Me.ITEMS_CIDX).Controls(0), Button)
                    objButton.Style.Add("background-color", "#dee3e7")
                    objButton.Style.Add("cursor", "hand")
                    objButton.CssClass = "FLATBUTTON"
                    'ItemStyle-CssClass="FLATBUTTON"

                ElseIf elemType = ListItemType.Item Then
                    Dim objButton As Button

                    objButton = DirectCast(e.Item.Cells(Me.ITEMS_CIDX).Controls(0), Button)
                    objButton.Style.Add("background-color", "#dee3e7")
                    objButton.Style.Add("cursor", "hand")
                    objButton.CssClass = "FLATBUTTON"

                End If
            End If
        End Sub

        Private Sub DataGridDropdowns_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridDropdowns.PageIndexChanged
            Try
                Me.DataGridDropdowns.CurrentPageIndex = e.NewPageIndex
                Me.State.PageIndex = Me.DataGridDropdowns.CurrentPageIndex
                PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorController)
            End Try
        End Sub


        Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
            'Enable the Menu Navigation Back after returning from the child
            Try
                Me.MenuEnabled = True
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorController)
            End Try
        End Sub

        Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try
                Me.ReturnToTabHomePage()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorController)
            End Try
        End Sub



        Private Sub moCompanyMultipleDrop_SelectedDropChanged(ByVal aSrc As Common.MultipleColumnDDLabelControl) Handles moCompanyMultipleDrop.SelectedDropChanged
            Me.State.moDropdownGuid = moCompanyMultipleDrop.SelectedGuid
            PopulateGrid()

        End Sub
    End Class

End Namespace

