Partial Class CountryContactInfoForm
    Inherits System.Web.UI.Page



#Region "Members"

#Region "Controls"

    Private Shared ListItemValue As Integer
    Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel

#End Region

#End Region

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not Page.IsPostBack Then
            ListItemValue = 0            
            LoadContactInfoReqFieldsList()
            RefreshPreview()
            'Session("AddAtEnd") = True
        Else
            LoadContactInfoReqFieldsList(False)
        End If

        btnSave_WRITE.Attributes.Add("OnClick", "SaveFormat(document.getElementById('" & Me.ContactInfoReqFieldsList.ClientID & "'));return false;")        
    End Sub

    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Dim strContactInfoReqFields As String = ""

        For Each tempItem As ListItem In ContactInfoReqFieldsList.Items
            strContactInfoReqFields = strContactInfoReqFields + tempItem.Text
        Next
        strContactInfoReqFields = strContactInfoReqFields.Replace("[\n]", "[\\n]")
        Me.literalJS.Text = "<SCRIPT>document.getElementById(""hiddenContactInfoReqFields"").value = """ & strContactInfoReqFields & """;</SCRIPT>"
    End Sub

    Private Sub LoadContactInfoReqFieldsList(Optional ByVal blnInit As Boolean = True)
        'todo ..get the actual values from the BO
        Dim ContactInfoReqFields As String = "", intSelected As Integer
        If Not blnInit Then
            If Not (Page.Request.Form("hiddenContactInfoReqFields") Is Nothing) Then
                ContactInfoReqFields = Page.Request.Form("hiddenContactInfoReqFields")
                intSelected = ContactInfoReqFieldsList.SelectedIndex
            End If
        Else
            If Not (Session("ContactInfoReqFields") Is Nothing) Then
                ContactInfoReqFields = CType(Session("ContactInfoReqFields"), String)
            End If
        End If
        ListItemValue = 0
        ContactInfoReqFieldsList.Items.Clear()

        Dim colAddrTokens As Collections.Generic.List(Of ContactInfo.ContactInfoRequiredFieldsItem) = New Collections.Generic.List(Of ContactInfo.ContactInfoRequiredFieldsItem)
        ContactInfo.SplitContactInfoRequiredFieldsString(ContactInfoReqFields, colAddrTokens)
        For Each AToken As ContactInfo.ContactInfoRequiredFieldsItem In colAddrTokens
            If AToken.ItemName.Length > 0 Then
                ContactInfoReqFieldsList.Items.Add(New ListItem(AToken.ItemName, ListItemValue.ToString))
                ListItemValue = ListItemValue + 1
            End If
        Next
        If blnInit Then
            ContactInfoReqFieldsList.SelectedIndex = ContactInfoReqFieldsList.Items.Count - 1
        Else
            ContactInfoReqFieldsList.SelectedIndex = intSelected
        End If

    End Sub

    Private Sub CreateTokenFormat(ByVal tokenValue As String, Optional ByVal insertAtEnd As Boolean = True)
        'TokenBox.Text = TokenBox.Text & tokenValue
        If insertAtEnd Then
            ContactInfoReqFieldsList.Items.Add(New ListItem(tokenValue, ListItemValue.ToString))
            ListItemValue = ListItemValue + 1
            ContactInfoReqFieldsList.SelectedIndex = ContactInfoReqFieldsList.Items.Count - 1
        Else
            ' first four are to be fixed.
            If (ContactInfoReqFieldsList.SelectedIndex > 3) Then
                ContactInfoReqFieldsList.Items.Insert(ContactInfoReqFieldsList.SelectedIndex, New ListItem(tokenValue, ListItemValue.ToString))
                ListItemValue = ListItemValue + 1
            End If
        End If
        RefreshPreview()
    End Sub



    Private Function StripTokenFlag(ByVal strToken As String) As String
        If strToken.Trim.EndsWith("]*") Then
            Return strToken.Substring(0, strToken.Length - 1)
        Else
            Return strToken
        End If

    End Function
    Private Sub RefreshPreview()


        '' ''If ContactInfoReqFieldsList.Items.IndexOf(ContactInfoReqFieldsList.Items.FindByText("[ADR1]")) = -1 Then
        '' ''    ContactInfoReqFieldsList.Items.Insert(0, New ListItem("[ADR1]", ListItemValue.ToString))
        '' ''    ListItemValue = ListItemValue + 1
        '' ''    ContactInfoReqFieldsList.Items.Insert(1, New ListItem("[\n]", ListItemValue.ToString))
        '' ''    ListItemValue = ListItemValue + 1
        '' ''End If
        '' ''If ContactInfoReqFieldsList.Items.IndexOf(ContactInfoReqFieldsList.Items.FindByText("[ADR2]")) = -1 Then
        '' ''    ContactInfoReqFieldsList.Items.Insert(2, New ListItem("[ADR2]", ListItemValue.ToString))
        '' ''    ListItemValue = ListItemValue + 1
        '' ''    ContactInfoReqFieldsList.Items.Insert(3, New ListItem("[\n]", ListItemValue.ToString))
        '' ''    ListItemValue = ListItemValue + 1
        '' ''End If

        ' '' ''Dim addrFormat As AddressFormat = New AddressFormat(TokenBox.Text.Trim())
        ' '' ''For i As Integer = 0 To addrFormat.TokenCount - 1
        '' ''For i As Integer = 0 To ContactInfoReqFieldsList.Items.Count - 1
        '' ''    'Dim strToken As String = addrFormat.GetNextToken()
        '' ''    Dim strToken As String = StripTokenFlag(ContactInfoReqFieldsList.Items(i).Text)
        '' ''    If strToken = "[ADR1]" Then
        '' ''        AddValueToDisplay("Address1", "ADR1]")
        '' ''    ElseIf strToken = "[ADR2]" Then
        '' ''        AddValueToDisplay("Address2", "ADR2]")
        '' ''    Else
        '' ''        Dim addrListItem As ListItem = AvailList.Items.FindByValue(strToken)
        '' ''        If addrListItem Is Nothing Then
        '' ''            Dim specialChar As String = strToken.Replace("[", "").Replace("]", "")
        '' ''            If specialChar.Length <> 1 Then
        '' ''                TextBox2.Text = ""
        '' ''                PreviewList.Items.Clear()
        '' ''                PreviewList.Items.Add("")
        '' ''                AddValueToDisplay("Invalid Token " & strToken, strToken)
        '' ''                Exit For
        '' ''            Else
        '' ''                AddValueToDisplay(specialChar, strToken)
        '' ''            End If
        '' ''        Else
        '' ''            Dim selectedValue As String = addrListItem.Text
        '' ''            Dim selectedValuesArray As String() = selectedValue.Split(CType(",", Char))
        '' ''            AddValueToDisplay(selectedValuesArray(0), addrListItem.Value)
        '' ''        End If
        '' ''    End If
        '' ''Next i
    End Sub
    Private Sub AddToFormatList()
        If AvailList.SelectedIndex = -1 Then
            Exit Sub
        End If
        Dim bShowSpecialCharTextBox As Boolean = (AvailList.Items(AvailList.SelectedIndex).Value.Trim() = "[*]")

        If Not bShowSpecialCharTextBox Then
            CreateTokenFormat(AvailList.Items(AvailList.SelectedIndex).Value)
        End If
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        AddToFormatList()
    End Sub

    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.Click

        ContactInfoReqFieldsList.Items.Remove(ContactInfoReqFieldsList.SelectedItem)
        RefreshPreview()
    End Sub

    Private Sub btnMoveUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMoveUp.Click

        Dim iSelectedIndex As Integer = ContactInfoReqFieldsList.SelectedIndex
        If iSelectedIndex > 0 Then 'not the first one
            Dim tempItem As ListItem = ContactInfoReqFieldsList.SelectedItem
            ContactInfoReqFieldsList.Items.Remove(tempItem)
            ContactInfoReqFieldsList.Items.Insert(iSelectedIndex - 1, tempItem)
            RefreshPreview()
        End If
    End Sub

    Private Sub btnMoveDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMoveDown.Click

        Dim iSelectedIndex As Integer = ContactInfoReqFieldsList.SelectedIndex
        If iSelectedIndex <> ContactInfoReqFieldsList.Items.Count - 1 Then 'Not the last one
            Dim tempItem As ListItem = ContactInfoReqFieldsList.SelectedItem
            ContactInfoReqFieldsList.Items.Remove(tempItem)
            ContactInfoReqFieldsList.Items.Insert(iSelectedIndex + 1, tempItem)
            RefreshPreview()
        End If
    End Sub


    Private Sub btnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click

        Dim ContactInfoReqFieldsStr As String = ""

        For Each tempItem As ListItem In ContactInfoReqFieldsList.Items
            ContactInfoReqFieldsStr = ContactInfoReqFieldsStr + tempItem.Text
        Next

        ContactInfoReqFieldsStr = ContactInfoReqFieldsStr.Replace("[\n]", "[\\n]")

        Dim sJavaScript As String
        sJavaScript = "<SCRIPT>" & Environment.NewLine
        sJavaScript &= "parent.SaveCountryContactInfo('" & ContactInfoReqFieldsStr & "');" & Environment.NewLine
        sJavaScript &= "</SCRIPT>" & Environment.NewLine
        Me.RegisterStartupScript("SaveCountryContactInfo", sJavaScript)
        'End If
    End Sub

End Class
