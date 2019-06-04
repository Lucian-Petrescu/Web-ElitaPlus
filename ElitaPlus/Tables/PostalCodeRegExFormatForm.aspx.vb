Imports System.Text.RegularExpressions

Partial Class PostalCodeRegExFormatForm
    Inherits System.Web.UI.Page
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

    'Private Shared PostalCodeRegEx As New Collection
    Private Shared ListItemValue As Integer

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not Page.IsPostBack Then
            lblPreview.Text = ""
            DetailPanel.Visible = False
            ListPanel.Enabled = True
            ListItemValue = 0
            LoadSegmentList()
            GenerateRegEx()
        End If
        EnableDisableValues()
    End Sub

    Private Sub InitializeEditValues()
        chkOptional.Checked = False
        MinLen.Text = "0"
        MaxLen.Text = "0"
        CaseList.SelectedValue = "Upper"
        DisallowedValues.Text = ""
    End Sub


    Private Sub LoadSegmentList()
        SegmentList.Items.Clear()
        ListItemValue = 0
        Dim regExString As String = ""
        If Not (Session("PostalCodeRegEx") Is Nothing) Then
            regExString = CType(Session("PostalCodeRegEx"), String)
            ' remove the ^ and $ from the regex
            If regExString.Trim().Length > 2 Then ' to account for ^ and $
                regExString = regExString.Substring(1, regExString.Trim().Length - 2)
            End If
        End If

        Dim previewStr As String = ""
        If regExString.Trim().Length > 0 Then
            For Each tempStr As String In regExString.Trim().Split("}".Chars(0))
                If tempStr.Trim().Length > 0 Then
                    tempStr = tempStr + "}"
                    Dim tempRegEx As GenericRegExFactory = New GenericRegExFactory(tempStr)
                    Dim regExTypeName As String = ""
                    Select Case tempRegEx.RegularExp.RegularExType
                        Case RegExTypeValues.SpaceRegEx
                            regExTypeName = "Space"
                            previewStr = New String(" ".Chars(0), tempRegEx.RegularExp.MaximumLength)
                        Case RegExTypeValues.SpecialCharRegEx
                            regExTypeName = "SpecialChar"
                            previewStr = New String(CType(tempRegEx.RegularExp, SpecialCharRegEx).SpecialChar.Chars(0), tempRegEx.RegularExp.MaximumLength)
                        Case RegExTypeValues.NumericRegEx
                            regExTypeName = "Numeric"
                            previewStr = New String("N".Chars(0), tempRegEx.RegularExp.MaximumLength)
                        Case RegExTypeValues.AlphaNumericRegEx
                            regExTypeName = "AlphaNumeric"
                            previewStr = New String("X".Chars(0), tempRegEx.RegularExp.MaximumLength)
                        Case RegExTypeValues.AlphaRegEx
                            regExTypeName = "Alpha"
                            previewStr = New String("A".Chars(0), tempRegEx.RegularExp.MaximumLength)
                    End Select
                    SegmentList.Items.Add(New ListItem(tempStr, regExTypeName & "," & ListItemValue & "," & previewStr))
                    ListItemValue = ListItemValue + 1
                End If
            Next
            If SegmentList.Items.Count > 0 Then
                SegmentList.SelectedIndex = 0
            End If
        End If
    End Sub

    Private Sub chkOptional_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkOptional.CheckedChanged
        MinLen.Text = "0"
        EnableDisableValues()
    End Sub

    Private Sub RegExTypeList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RegExTypeList.SelectedIndexChanged
        chkOptional.Checked = False
        MinLen.Text = "0"
        MaxLen.Text = "0"
        CaseList.SelectedValue = "Upper"
        DisallowedValues.Text = ""

        If RegExTypeList.SelectedValue = "SpecialChar" Then
            DisallowLabel.Text = "Special Char(s)"
        Else
            DisallowLabel.Text = "Disallowed Values"
        End If
        EnableDisableValues()
    End Sub

    Private Sub EnableDisableValues()
        MinLen.Enabled = Not chkOptional.Checked
        CaseList.Visible = RegExTypeList.SelectedValue.StartsWith("Alpha")
        CaseLabel.Visible = RegExTypeList.SelectedValue.StartsWith("Alpha")
        DisallowedValues.Visible = Not (RegExTypeList.SelectedValue = "Space") 'Or RegExTypeList.SelectedValue = "SpecialChar")
        DisallowLabel.Visible = Not (RegExTypeList.SelectedValue = "Space")
        ExampleLabel.Visible = Not (RegExTypeList.SelectedValue = "Space" Or RegExTypeList.SelectedValue = "SpecialChar")
    End Sub

    Private Sub btnGenerate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerate.Click
        ErrorLabel.Visible = False
        Dim tempCaseValue As CaseValues
        Select Case CaseList.SelectedValue
            Case "Both"
                tempCaseValue = CaseValues.Both
            Case "Upper"
                tempCaseValue = CaseValues.UpperCase
            Case "Lower"
                tempCaseValue = CaseValues.LowerCase
        End Select

        Try
            Dim previewStr As String = ""
            ' we enforce only fixed length
            MinLen.Text = MaxLen.Text
            Dim tempRegEx As GenericRegEx
            Select Case RegExTypeList.SelectedValue
                Case "Numeric"
                    tempRegEx = New NumericRegEx(chkOptional.Checked, CType(MinLen.Text, Integer), CType(MaxLen.Text, Integer), DisallowedValues.Text)
                    previewStr = New String("N".Chars(0), CType(MaxLen.Text, Integer))
                Case "Alpha"
                    tempRegEx = New AlphaRegEx(chkOptional.Checked, CType(MinLen.Text, Integer), CType(MaxLen.Text, Integer), tempCaseValue, DisallowedValues.Text)
                    previewStr = New String("A".Chars(0), CType(MaxLen.Text, Integer))
                Case "AlphaNumeric"
                    tempRegEx = New AlphaNumericRegEx(chkOptional.Checked, CType(MinLen.Text, Integer), CType(MaxLen.Text, Integer), tempCaseValue, DisallowedValues.Text)
                    previewStr = New String("X".Chars(0), CType(MaxLen.Text, Integer))
                Case "Space"
                    tempRegEx = New SpaceRegEx(chkOptional.Checked, CType(MinLen.Text, Integer), CType(MaxLen.Text, Integer))
                    previewStr = New String(" ".Chars(0), CType(MaxLen.Text, Integer))
                Case "SpecialChar"
                    tempRegEx = New SpecialCharRegEx(chkOptional.Checked, CType(MinLen.Text, Integer), CType(MaxLen.Text, Integer), DisallowedValues.Text)
                    If DisallowedValues.Text.Length > 0 Then
                        previewStr = New String(DisallowedValues.Text.Chars(0), CType(MaxLen.Text, Integer))
                    Else
                        previewStr = New String(" ".Chars(0), CType(MaxLen.Text, Integer))
                    End If
            End Select
            'PostalCodeRegEx.Add(tempRegEx)
            Dim regExString As String = tempRegEx.RegExString()
            If EditPanelTitle.Text.StartsWith("Edit") Then
                Dim editListItem As ListItem = SegmentList.SelectedItem
                Dim selectedIndex As Integer = SegmentList.SelectedIndex
                SegmentList.Items.Remove(editListItem)
                SegmentList.Items.Insert(selectedIndex, New ListItem(regExString, RegExTypeList.SelectedValue & "," & ListItemValue & "," & previewStr))
                SegmentList.SelectedIndex = selectedIndex
            Else
                SegmentList.Items.Add(New ListItem(regExString, RegExTypeList.SelectedValue & "," & ListItemValue & "," & previewStr))
                SegmentList.SelectedIndex = SegmentList.Items.Count - 1
            End If
            ListItemValue = ListItemValue + 1
            GenerateRegEx()
            DetailPanel.Visible = False
            ListPanel.Enabled = True
            SegmentList.Enabled = True
        Catch ex As Exception
            'PostalCodeRegEx.Remove(PostalCodeRegEx.Count)
            ErrorLabel.Visible = True
            ErrorLabel.Text = ex.Message()
        End Try
    End Sub

    Private Sub GenerateRegEx()
        lblPreview.Text = ""
        RegExBox.Text = ""
        For Each tempItem As ListItem In SegmentList.Items
            Dim tempStr As String() = tempItem.Value.Split(",".Chars(0))
            If Not (tempStr Is Nothing) Then
                If (tempStr.Length = 3) Then
                    lblPreview.Text = lblPreview.Text + tempStr(2)
                End If
            End If
            RegExBox.Text = RegExBox.Text + tempItem.Text
        Next tempItem
        If RegExBox.Text.Trim().Length > 0 Then
            RegExBox.Text = "^" & RegExBox.Text & "$"
        End If

        'RegExBox.Text = "^"
        'For Each tempRegEx As GenericRegEx In PostalCodeRegEx
        '    RegExBox.Text = RegExBox.Text + tempRegEx.RegExString()
        'Next tempRegEx
        'RegExBox.Text = RegExBox.Text + "$"
    End Sub

    Private Sub btnTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTest.Click
        lblTest.Visible = True
        If RegExBox.Text.Trim().Length = 0 Then
            lblTest.ForeColor = Color.Red
            lblTest.Text = "No RegEx to match"
        ElseIf TestInput.Text.Trim().Length = 0 Then
            lblTest.ForeColor = Color.Red
            lblTest.Text = "No value to match"
        Else
            Dim tempRegEx As Regex = New Regex(RegExBox.Text.Trim())
            Dim M As Match = tempRegEx.Match(TestInput.Text.Trim())

            If M.Success Then
                lblTest.ForeColor = Color.Green
                lblTest.Text = "Match"
            Else
                lblTest.ForeColor = Color.Red
                lblTest.Text = "No Match"
            End If

        End If
    End Sub

    Private Sub ClearRegEx_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearRegEx.Click
        RegExBox.Text = ""
        lblPreview.Text = ""
        'For iRegExCount As Integer = 1 To PostalCodeRegEx.Count
        '    PostalCodeRegEx.Remove(1)
        'Next iRegExCount
        lblTest.Text = ""
    End Sub

    Private Sub AddRegex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddRegex.Click
        InitializeEditValues()
        SegmentList.Enabled = False
        DetailPanel.Visible = True
        ListPanel.Enabled = False
        EditPanelTitle.Text = "Add New RegEx Segment"
        lblTest.Visible = False
        RegExTypeList.SelectedIndex = 0
        RegExTypeList_SelectedIndexChanged(sender, e)
    End Sub

    Private Sub EditRegex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditRegex.Click
        If SegmentList.SelectedIndex > -1 Then
            SegmentList.Enabled = False
            DetailPanel.Visible = True
            ListPanel.Enabled = False
            lblTest.Visible = False
            EditPanelTitle.Text = "Edit RegEx Segment"
            RegExTypeList.SelectedIndex = RegExTypeList.Items.IndexOf(RegExTypeList.Items.FindByValue(SegmentList.SelectedItem.Value.Substring(0, SegmentList.SelectedItem.Value.IndexOf(","))))
            RegExTypeList_SelectedIndexChanged(sender, e)
            ' pass the regex in constructor and calculate other fields
            Dim tempRegEx As GenericRegExFactory = New GenericRegExFactory(SegmentList.SelectedItem.Text)
            MinLen.Text = tempRegEx.RegularExp.MinimumLength.ToString()
            MaxLen.Text = tempRegEx.RegularExp.MaximumLength.ToString()
            DisallowedValues.Text = tempRegEx.RegularExp.DisallowedValues
            If tempRegEx.RegularExp.RegularExType = RegExTypeValues.SpecialCharRegEx Then
                DisallowedValues.Text = CType(tempRegEx.RegularExp, SpecialCharRegEx).SpecialChar
            ElseIf (tempRegEx.RegularExp.RegularExType = RegExTypeValues.AlphaRegEx) Then
                Select Case CType(tempRegEx.RegularExp, AlphaRegEx).CaseType
                    Case 1
                        CaseList.SelectedIndex = CaseList.Items.IndexOf(CaseList.Items.FindByValue("Upper"))
                    Case 2
                        CaseList.SelectedIndex = CaseList.Items.IndexOf(CaseList.Items.FindByValue("Lower"))
                    Case 3
                        CaseList.SelectedIndex = CaseList.Items.IndexOf(CaseList.Items.FindByValue("Both"))
                End Select

            ElseIf (tempRegEx.RegularExp.RegularExType = RegExTypeValues.AlphaNumericRegEx) Then
                Select Case CType(tempRegEx.RegularExp, AlphaNumericRegEx).CaseType
                    Case 1
                        CaseList.SelectedIndex = CaseList.Items.IndexOf(CaseList.Items.FindByValue("Upper"))
                    Case 2
                        CaseList.SelectedIndex = CaseList.Items.IndexOf(CaseList.Items.FindByValue("Lower"))
                    Case 3
                        CaseList.SelectedIndex = CaseList.Items.IndexOf(CaseList.Items.FindByValue("Both"))
                End Select
            End If
        End If
    End Sub


    Private Sub DeleteRegex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteRegex.Click
        lblTest.Visible = False
        If SegmentList.SelectedIndex > -1 Then
            SegmentList.Items.Remove(SegmentList.SelectedItem)
            Dim selIndex As Integer = SegmentList.SelectedIndex
            If selIndex - 1 >= 0 Then
                SegmentList.SelectedIndex = selIndex - 1
            ElseIf SegmentList.Items.Count > 0 Then
                SegmentList.SelectedIndex = 0
            End If
            GenerateRegEx()
        End If
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        GenerateRegEx()
        SegmentList.Enabled = True
        DetailPanel.Visible = False
        ListPanel.Enabled = True
        ErrorLabel.Visible = False
    End Sub

    
    Private Sub btnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
        Dim sJavaScript As String
        sJavaScript = "<SCRIPT>" & Environment.NewLine
        sJavaScript &= "parent.SavePostalCodeFormat('" & RegExBox.Text.Replace("\", "\\") & "','" & lblPreview.Text.Trim() & "');" & Environment.NewLine
        sJavaScript &= "</SCRIPT>" & Environment.NewLine
        Me.RegisterStartupScript("SavePostalCodeFormat", sJavaScript)
    End Sub
End Class
