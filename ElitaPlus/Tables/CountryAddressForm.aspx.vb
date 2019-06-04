Partial Class CountryAddressForm
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
            PreviewList.Items.Add("")
            LoadMailAddrFormatList()
            RefreshPreview()
            'Session("AddAtEnd") = True
        Else
            LoadMailAddrFormatList(False)
        End If

        chkOptional.Attributes.Add("OnClick", "SetSelectedItemOptional(document.getElementById('" & Me.MailAddrFormatList.ClientID & "'))")
        btnSave_WRITE.Attributes.Add("OnClick", "SaveFormat(document.getElementById('" & Me.MailAddrFormatList.ClientID & "'));return false;")
        MailAddrFormatList.Attributes.Add("OnChange", "SetOptionalFlag(document.getElementById('" & Me.chkOptional.ClientID & "'))")
    End Sub

    Private Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        Dim strAddressFormat As String = ""

        For Each tempItem As ListItem In MailAddrFormatList.Items
            strAddressFormat = strAddressFormat + tempItem.Text
        Next
        strAddressFormat = strAddressFormat.Replace("[\n]", "[\\n]")
        Me.literalJS.Text = "<SCRIPT>document.getElementById(""hiddenAddrFormat"").value = """ & strAddressFormat & """;</SCRIPT>"
    End Sub

    Private Sub LoadMailAddrFormatList(Optional ByVal blnInit As Boolean = True)
        'todo ..get the actual values from the BO
        Dim mailAddrFormat As String = "", intSelected As Integer
        If Not blnInit Then
            If Not (Page.Request.Form("hiddenAddrFormat") Is Nothing) Then
                mailAddrFormat = Page.Request.Form("hiddenAddrFormat")
                intSelected = MailAddrFormatList.SelectedIndex
            End If
        Else
            If Not (Session("MailAddrFormat") Is Nothing) Then
                mailAddrFormat = CType(Session("MailAddrFormat"), String)
            End If
        End If
        ListItemValue = 0
        MailAddrFormatList.Items.Clear()

        'Dim mailAddrFormatArray As String() = mailAddrFormat.Split(CType("]", Char))
        'For Each mailAddrToken As String In mailAddrFormatArray
        '    If mailAddrToken.Trim().Length > 0 Then
        '        MailAddrFormatList.Items.Add(New ListItem(mailAddrToken + "]", ListItemValue.ToString))
        '        ListItemValue = ListItemValue + 1
        '    End If
        'Next
        Dim colAddrTokens As Collections.Generic.List(Of Address.MailAddressItem) = New Collections.Generic.List(Of Address.MailAddressItem)
        Address.SplitMailingAddressFormatString(mailAddrFormat, colAddrTokens)
        For Each AToken As Address.MailAddressItem In colAddrTokens
            If AToken.ItemName.Length > 0 Then
                MailAddrFormatList.Items.Add(New ListItem(AToken.ItemName, ListItemValue.ToString))
                ListItemValue = ListItemValue + 1
            End If
        Next
        If blnInit Then
            MailAddrFormatList.SelectedIndex = MailAddrFormatList.Items.Count - 1
        Else
            MailAddrFormatList.SelectedIndex = intSelected
        End If

    End Sub
    Public Sub SpecialCharChanged(ByVal sender As System.Object, ByVal e As EventArgs)
        Dim cSpecialCharTextBox As String = SpecialChar.Text.Trim()
        If cSpecialCharTextBox.Length <> 0 Then
            CreateTokenFormat("[" & cSpecialCharTextBox & "]") ', CType(Session("AddAtEnd"), Boolean))
            SpecialChar.Text = ""
            SpecialChar.Visible = False
            SpecialCharLabel.Visible = False
        End If
    End Sub


    Private Sub CreateTokenFormat(ByVal tokenValue As String, Optional ByVal insertAtEnd As Boolean = True)
        'TokenBox.Text = TokenBox.Text & tokenValue
        If insertAtEnd Then
            MailAddrFormatList.Items.Add(New ListItem(tokenValue, ListItemValue.ToString))
            ListItemValue = ListItemValue + 1
            MailAddrFormatList.SelectedIndex = MailAddrFormatList.Items.Count - 1
        Else
            ' first four are to be fixed.
            If (MailAddrFormatList.SelectedIndex > 3) Then
                MailAddrFormatList.Items.Insert(MailAddrFormatList.SelectedIndex, New ListItem(tokenValue, ListItemValue.ToString))
                ListItemValue = ListItemValue + 1
            End If
        End If
        RefreshPreview()
    End Sub

    Private Sub AddValueToDisplay(ByVal displayValue As String, ByVal tokenValue As String)
        If tokenValue = "[Space]" Then
            displayValue = " "
        End If
        If tokenValue <> "[\n]" Then
            TextBox2.Text = TextBox2.Text & displayValue
            PreviewList.Items(PreviewList.Items.Count - 1).Text = TextBox2.Text
        Else
            PreviewList.Items.Add("")
            TextBox2.Text = ""
        End If
    End Sub


    Private Sub ClearButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearButton.Click
        'TokenBox.Text = ""
        MailAddrFormatList.Items.Clear()
        ListItemValue = 0
        AvailList.SelectedIndex = 0
        RefreshPreview()
    End Sub

    Private Sub RefreshButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshButton.Click
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
        TextBox2.Text = ""
        PreviewList.Items.Clear()
        PreviewList.Items.Add("")

        If MailAddrFormatList.Items.IndexOf(MailAddrFormatList.Items.FindByText("[ADR1]")) = -1 Then
            MailAddrFormatList.Items.Insert(0, New ListItem("[ADR1]", ListItemValue.ToString))
            ListItemValue = ListItemValue + 1
            MailAddrFormatList.Items.Insert(1, New ListItem("[\n]", ListItemValue.ToString))
            ListItemValue = ListItemValue + 1
        End If
        If MailAddrFormatList.Items.IndexOf(MailAddrFormatList.Items.FindByText("[ADR2]")) = -1 Then
            MailAddrFormatList.Items.Insert(2, New ListItem("[ADR2]", ListItemValue.ToString))
            ListItemValue = ListItemValue + 1
            MailAddrFormatList.Items.Insert(3, New ListItem("[\n]", ListItemValue.ToString))
            ListItemValue = ListItemValue + 1
        End If

        'Dim addrFormat As AddressFormat = New AddressFormat(TokenBox.Text.Trim())
        'For i As Integer = 0 To addrFormat.TokenCount - 1
        For i As Integer = 0 To MailAddrFormatList.Items.Count - 1
            'Dim strToken As String = addrFormat.GetNextToken()
            Dim strToken As String = StripTokenFlag(MailAddrFormatList.Items(i).Text)
            If strToken = "[ADR1]" Then
                AddValueToDisplay("Address1", "ADR1]")
            ElseIf strToken = "[ADR2]" Then
                AddValueToDisplay("Address2", "ADR2]")
            Else
                Dim addrListItem As ListItem = AvailList.Items.FindByValue(strToken)
                If addrListItem Is Nothing Then
                    Dim specialChar As String = strToken.Replace("[", "").Replace("]", "")
                    If specialChar.Length <> 1 Then
                        TextBox2.Text = ""
                        PreviewList.Items.Clear()
                        PreviewList.Items.Add("")
                        AddValueToDisplay("Invalid Token " & strToken, strToken)
                        Exit For
                    Else
                        AddValueToDisplay(specialChar, strToken)
                    End If
                Else
                    Dim selectedValue As String = addrListItem.Text
                    Dim selectedValuesArray As String() = selectedValue.Split(CType(",", Char))
                    AddValueToDisplay(selectedValuesArray(0), addrListItem.Value)
                End If
            End If
        Next i
    End Sub
    Private Sub AddToFormatList()
        If AvailList.SelectedIndex = -1 Then
            Exit Sub
        End If
        Dim bShowSpecialCharTextBox As Boolean = (AvailList.Items(AvailList.SelectedIndex).Value.Trim() = "[*]")
        SpecialChar.Visible = bShowSpecialCharTextBox
        SpecialCharLabel.Visible = bShowSpecialCharTextBox
        If Not bShowSpecialCharTextBox Then
            CreateTokenFormat(AvailList.Items(AvailList.SelectedIndex).Value)
        End If
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        AddToFormatList() '(True)
    End Sub

    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        'If MailAddrFormatList.Items.Count > 4 Then
        '    If MailAddrFormatList.SelectedIndex > 3 Then
        '        MailAddrFormatList.Items.Remove(MailAddrFormatList.SelectedItem)
        '        RefreshPreview()
        '    End If
        'End If
        MailAddrFormatList.Items.Remove(MailAddrFormatList.SelectedItem)
        RefreshPreview()
    End Sub

    Private Sub btnMoveUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMoveUp.Click
        'If MailAddrFormatList.Items.Count > 4 Then
        '    Dim iSelectedIndex As Integer = MailAddrFormatList.SelectedIndex
        '    If (iSelectedIndex > 4) Then
        '        Dim tempItem As ListItem = MailAddrFormatList.SelectedItem
        '        MailAddrFormatList.Items.Remove(tempItem)
        '        MailAddrFormatList.Items.Insert(iSelectedIndex - 1, tempItem)
        '        RefreshPreview()
        '    End If
        'End If
        Dim iSelectedIndex As Integer = MailAddrFormatList.SelectedIndex
        If iSelectedIndex > 0 Then 'not the first one
            Dim tempItem As ListItem = MailAddrFormatList.SelectedItem
            MailAddrFormatList.Items.Remove(tempItem)
            MailAddrFormatList.Items.Insert(iSelectedIndex - 1, tempItem)
            RefreshPreview()
        End If
    End Sub

    Private Sub btnMoveDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMoveDown.Click
        'If MailAddrFormatList.Items.Count > 4 Then
        '    Dim iSelectedIndex As Integer = MailAddrFormatList.SelectedIndex
        '    If (iSelectedIndex > 3) And (iSelectedIndex <> MailAddrFormatList.Items.Count - 1) Then
        '        Dim tempItem As ListItem = MailAddrFormatList.SelectedItem
        '        MailAddrFormatList.Items.Remove(tempItem)
        '        MailAddrFormatList.Items.Insert(iSelectedIndex + 1, tempItem)
        '        RefreshPreview()
        '    End If
        'End If
        Dim iSelectedIndex As Integer = MailAddrFormatList.SelectedIndex
        If iSelectedIndex <> MailAddrFormatList.Items.Count - 1 Then 'Not the last one
            Dim tempItem As ListItem = MailAddrFormatList.SelectedItem
            MailAddrFormatList.Items.Remove(tempItem)
            MailAddrFormatList.Items.Insert(iSelectedIndex + 1, tempItem)
            RefreshPreview()
        End If
    End Sub


    Private Sub btnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
        'If Not Page.IsStartupScriptRegistered("SaveCountryAddress") Then
        Dim addrFormatStr As String = ""

        For Each tempItem As ListItem In MailAddrFormatList.Items
            addrFormatStr = addrFormatStr + tempItem.Text
        Next

        addrFormatStr = addrFormatStr.Replace("[\n]", "[\\n]")

        Dim sJavaScript As String
        sJavaScript = "<SCRIPT>" & Environment.NewLine
        sJavaScript &= "parent.SaveCountryAddress('" & addrFormatStr & "');" & Environment.NewLine
        sJavaScript &= "</SCRIPT>" & Environment.NewLine
        Me.RegisterStartupScript("SaveCountryAddress", sJavaScript)
        'End If
    End Sub

End Class
