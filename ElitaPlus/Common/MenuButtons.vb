

Public Class ButtonTitle
    Implements IComparer
    Public ButtonText As String
    Public ButtonIndex As Integer
    Public Sub New(ByVal objButtonText As String, ByVal objButtonIndex As Integer)
        ButtonText = objButtonText
        ButtonIndex = objButtonIndex
    End Sub
    Public Enum CompareTypes
        ByButtonText
        ByButtonIndex
    End Enum
    Public CompareType As CompareTypes = CompareTypes.ByButtonText
    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements System.Collections.IComparer.Compare
        Dim empx As ButtonTitle = DirectCast(x, ButtonTitle)
        Dim empy As ButtonTitle = DirectCast(y, ButtonTitle)


        Select Case CompareType
            Case CompareTypes.ByButtonText
                Return empx.ButtonText.CompareTo(empy.ButtonText)
            Case CompareTypes.ByButtonIndex
                Return empx.ButtonIndex.CompareTo(empy.ButtonIndex)
        End Select

    End Function
End Class


Public Class MenuButtons

    Public Shared Sub MenuButtonsInit(ByRef MenuX As TextBox, ByRef MenuY As TextBox)
        '-MR-
        'Remarks MenuX and MenuY are empty at the starting of the page (because of the translation error)...
        MenuX.Text = "-188px"
        MenuY.Text = "-330px"

    End Sub
    Public Shared Sub MenuButtonsProc(ByVal oPage As ElitaPlusPage, ByRef objPanButtons As Panel, ByRef objPanButtonsHidden As Panel, ByRef objPanButtonsHeading As Panel, ByRef objMenuX As TextBox, ByRef objMenuY As TextBox, ByRef objPanelWiring As Panel, ByRef objcloseIcon As System.Web.UI.WebControls.Image, ByRef objbtnEdit_WRITE As Button, ByRef objbtnSave_WRITE As Button, ByRef objbtnUndo_Write As Button, ByRef objViewPanel_READ As Panel)


        Dim childControl As Control
        ' Dim myDynamicLabel = New Label



        Dim sJavaScript As String

        objPanButtons.BorderStyle = BorderStyle.Solid
        objPanButtons.BorderWidth = 1
        objPanButtons.BorderColor = Drawing.Color.Gray



        Dim i As Integer = objPanButtonsHidden.Controls.Count

        Dim ArrButtons(i) As Button
        Dim flagHideOriginalButtons As Integer = 0


        Dim ii As Integer = 0


        'First Pass create array of original buttons...
        For Each childControl In objPanButtonsHidden.Controls
            If (childControl.GetType.ToString = "System.Web.UI.WebControls.Button") Then
                'ChangeAttribute(CType(childControl, Button), objMenuX, objMenuY, objPanButtons, objPanelWiring)

                If (childControl.ID.IndexOf("Clone") <> -1) Then
                    flagHideOriginalButtons = 1
                End If

                If (childControl.Visible = True) Then
                    ArrButtons(ii) = CType(childControl, Button)
                    ii = ii + 1
                End If


            End If



        Next


        'Remarks: the button collection item (classification in the collection location) is read only !

        Dim mycontrol As Button
        Dim ArrButtonsTemp(ii) As Button
        Dim ArrButtonsSorted(ii) As Button


        Dim counterAdd As Integer = 0
        Dim counterIndex As Integer = 0
        Dim flagexists As Integer = 0
        Dim flagLoop As Integer = 0



        For i = 0 To ii - 1

            'clone the buttons

            mycontrol = New Button
            mycontrol.ID = ArrButtons(counterIndex).ID + "Clone"
            mycontrol.Text = ArrButtons(counterIndex).Text
            mycontrol.Width = ArrButtons(counterIndex).Width
            mycontrol.CssClass = ArrButtons(counterIndex).CssClass
            ' mycontrol.Visible = ArrButtons(counterIndex).Visible

            counterIndex += 1

            For Each childControl In objPanButtons.Controls
                If (childControl.GetType.ToString = "System.Web.UI.WebControls.Button") Then
                    If (childControl.ID = mycontrol.ID) Then
                        flagexists = 1

                        Exit For

                    End If

                End If
            Next

            If (flagexists = 0) Then
                ArrButtonsTemp(counterAdd) = mycontrol
                counterAdd += 1
            Else
                Exit For
            End If

        Next i

        '---------------------------------------------------------------------------
        'Before adding the buttons, it has to be sorted..
        '---------------------------------------------------------------------------
        If (flagexists = 0) Then

            Dim m_ButtonTitle(ii - 1) As ButtonTitle


            For i = 0 To ii - 1
                m_ButtonTitle(i) = New ButtonTitle(ArrButtonsTemp(i).Text.ToString, i)
            Next i


            Dim myComparing As New ButtonTitle(ButtonTitle.CompareTypes.ByButtonText.ToString, ButtonTitle.CompareTypes.ByButtonIndex)
            Array.Sort(m_ButtonTitle, myComparing)



            For i = 0 To ii - 1
                ArrButtonsSorted(i) = ArrButtonsTemp(m_ButtonTitle(i).ButtonIndex)
            Next i
        End If

        '-----------------------------------------------------------------------------

        For i = 0 To ii - 1

            If (flagexists = 0) Then
                objPanButtons.Controls.Add(ArrButtonsSorted(i))

                ChangeAttribute(CType(ArrButtonsSorted(i), Button), objMenuX, objMenuY, objPanButtons, objPanelWiring)
            Else
                Exit For
            End If

        Next i

        '------------------------------------------------------------------------------------

        'we need this two lines in order to keep the panelmenubuttons visible after clicking a button
        'Resize menu panel and table after reload or clicking buttons...


        sJavaScript = "<script type ='text/jscript' language='JavaScript'>document.getElementById('" + objPanButtons.ID.ToString + "').style.top = " + objMenuY.Text.Replace("px", "") + "; document.getElementById('" + objPanButtons.ID.ToString + "').style.left = " + objMenuX.Text.Replace("px", "") + ";document.getElementById('" + objPanButtonsHeading.ID.ToString + "').style.width = parseInt(document.getElementById('menubuttonstable').offsetWidth);document.getElementById('" + objPanButtons.ID.ToString + "').style.height = parseInt(document.getElementById('menubuttonstable').offsetHeight);</script>"
        oPage.ClientScript.RegisterStartupScript(oPage.GetType(), "MyScript", sJavaScript, False)


        Dim PanelMenuButtonsLocationY As String

        'PanelMenuButtonsLocationX = "267"
        PanelMenuButtonsLocationY = "485" '  
        objPanelWiring.Width = 89

        Dim AdjMenuPanelTop As Integer = 148

        Dim AdjustIframe As String = "oobj.style.left = document.getElementById('" + objPanButtons.ID.ToString + "').offsetLeft;"
        AdjustIframe += "oobj.style.top = document.getElementById('" + objPanButtons.ID.ToString + "').offsetTop;"
        AdjustIframe += "oobj.style.width = document.getElementById('" + objPanButtons.ID.ToString + "').offsetWidth;"
        AdjustIframe += "oobj.style.height= document.getElementById('" + objPanButtons.ID.ToString + "').offsetHeight;"

        objPanelWiring.Attributes.Add("onmouseover", " document.body.style.cursor = 'hand'; document.getElementById('" + objMenuX.ID.ToString + "').value = document.getElementById('menubuttonstable').offsetLeft + document.getElementById('" + objPanelWiring.ID.ToString + "').offsetWidth + " + AdjMenuPanelTop.ToString + ";document.getElementById('" + objMenuY.ID.ToString + "').value = " + PanelMenuButtonsLocationY + ";document.getElementById('" + objPanButtonsHeading.ID.ToString + "').style.width = parseInt(document.getElementById('menubuttonstable').offsetWidth);  AdjustMenuLocation();" + AdjustIframe + "AdjustBottonPage();")

        objcloseIcon.Attributes.Add("onclick", "document.body.style.cursor = 'hand'; document.getElementById('" + objMenuY.ID.ToString + "').value = -188; document.getElementById('" + objMenuY.ID.ToString + "').value = -530;AdjustMenuLocation();document.getElementById('myframe').style.left =-200;getElementById('" + objMenuY.ID.ToString + "').value = onclick.pageY;getElementById('" + objMenuX.ID.ToString + "').value = onclick.pageX;")
        objcloseIcon.Attributes.Add("onmouseover", "document.body.style.cursor = 'hand';")

        objPanButtons.Attributes.Add("onmousedown", "Adjust(); document.body.style.cursor = 'hand'; BeginDrag('" + objPanButtons.ID.ToString + "');")
        objPanButtons.Attributes.Add("onmouseover", "document.getElementById('" + objPanButtonsHeading.ID.ToString + "').style.width = parseInt(document.getElementById('menubuttonstable').offsetWidth);")


        objbtnEdit_WRITE.Attributes.Add("onclick", "document.getElementById('" + objMenuX.ID.ToString + "').value = -188; document.getElementById('myframe').style.left =-200;document.getElementById('" + objPanButtons.ID.ToString + "').style.left =-300;AdjustMenuLocation();" + AdjustIframe)
        objbtnSave_WRITE.Attributes.Add("onclick", "document.getElementById('" + objMenuX.ID.ToString + "').value = -188; document.getElementById('myframe').style.left =-200;document.getElementById('" + objPanButtons.ID.ToString + "').style.left =-300;AdjustMenuLocation();" + AdjustIframe)
        objbtnUndo_Write.Attributes.Add("onclick", "document.getElementById('" + objMenuX.ID.ToString + "').value = -188; document.getElementById('myframe').style.left =-200;document.getElementById('" + objPanButtons.ID.ToString + "').style.left =-300;AdjustMenuLocation();" + AdjustIframe)


        'objViewPanel_READ.Attributes.Add("onResize", "document.getElementById('" + objPanButtons.ID.ToString + "').style.left = document.getElementById('menubuttonstable').offsetLeft + document.getElementById('" + objPanelWiring.ID.ToString + "').offsetWidth + 26;" + AdjustIframe)
        objViewPanel_READ.Attributes.Add("onResize", "AdjustBottonPage();")


        'objPanelWiring.Attributes.Add("onclick", "CallClaimHistoryForm();")


    End Sub
    Public Shared Sub ChangeAttribute(ByRef myButton As Button, ByVal objMenuX As TextBox, ByRef objMenuY As TextBox, ByRef objPanButtons As Panel, ByRef objPanelWiring As Panel)
        myButton.Attributes.Add("onmouseover", "document.getElementById( '" + myButton.ID.ToString + "').style.filter='alpha(style=0, opacity=60)';document.getElementById( '" + myButton.ID.ToString + "').style.color = '#000099';")
        myButton.Attributes.Add("onmouseout", "document.getElementById( '" + myButton.ID.ToString + "').style.filter='alpha(style=0, opacity=100)';document.getElementById( '" + myButton.ID.ToString + "').style.color = 'black';")
        Dim myClickingButton As String = myButton.ID.ToString.Replace("Clone", "")
        myButton.Attributes.Add("onmousedown", "document.getElementById('" + objMenuX.ID.ToString + "').value = -200; document.getElementById('myframe').style.left =-200;AdjustMenuLocation(); document.getElementById('" + myClickingButton + "').click(onmousedown);onmousedown.event=0; ")

    End Sub

End Class



