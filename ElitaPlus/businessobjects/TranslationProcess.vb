Imports System.Text
Imports System.Reflection

'-------------------------------------
'Name:Class: TranslationProcess
'Purpose:Run a select and use the key of a translation to retrieve the value of the translation,
' for a single language. Loop thru the form and create 3 groups of controls;grids,dropdowns and labels.
'then concatenate the text in the objects to build the where clause. Retrieve the translation from the DB.
'Finally, pass in the dictionary key to get the translated value of the item.
''-------------------------------------

Public Class TranslationProcess
    Inherits TranslationBase

#Region "CONSTRUCTOR"


    Public Sub New()

    End Sub


#End Region


#Region "CONSTANTS"

    Const TEXT As String = "Text"
    Const NONE As Int16 = 0
    Const PAGEDATAGRID As String = "System.Web.UI.WebControls.DataGrid"
    Const PAGEGRIDBUTTON As String = "Button"
    Const PAGETOOLTIP As String = "ToolTip"
    Const PAGELISTCONTROL As String = "System.Web.UI.WebControls.ListControl"
    Const PAGEBUTTONCOLUMN As String = "system.Web.UI.WebControls.ButtonColumn"

    Const DATAGRIDSTYLE As String = "DATAGRID"
    Const DATAGRID_NOWRAP_STYLE As String = "DATAGRID_NOWRAP"

    Const TRANSLATION_EXCLUSION_MARK As String = "_NO_TRANSLATE"


#End Region



#Region "PRIVATE VARIABLES"

    Private TextFromControlsList As New ArrayList
    '  Private mTranslationMissingList As New ArrayList
    Private mControlsInGrid As New SingleValueControlArray
    Private oSingleValueControlArray As New SingleValueControlArray
    Private mlstTooltips As New ArrayList
    Private mlstTranslatableGridControls As New ArrayList


#End Region



#Region "PRIVATE FUNCTIONS AND SUBROUTINES"



    '-------------------------------------
    'Name:GetTranslation
    'Purpose:Filters the dataview using find method to return a single value according to the keyword passed in.
    'Input Values:
    'Uses:
    '-------------------------------------
    'Private Function Translate(ByVal sUIProgCode As String, ByVal oView As DataView) As String

    '    Dim oCurrentMissingTranslations As ArrayList

    '    If sUIProgCode.Trim = String.Empty Then

    '        'exit the function by returning the blank string.
    '        Return String.Empty

    '    End If



    '    oView.RowFilter = "UI_PROG_CODE = '" & sUIProgCode & "'"

    '    If oView.Count = 0 Then

    '        'add the missing translation item to the missing list.
    '        TranslationMissingList.Add(sUIProgCode)

    '        Return sUIProgCode


    '    Else

    '        Return oView.Item(0).Row("TRANSLATION").ToString

    '    End If

    'End Function


    Public Sub TranslateBasicControls(ByVal SingleValueControlArray As SingleValueControlArray, ByVal InClause As String, ByVal nLanguageID As Guid)


        Dim oSingleValueControl As SingleValueControl

        Dim sUIProgCode As String
        Dim oView As New DataView
        Dim oDS As DataSet

        'Load the translation data from the database.
        oDS = TranslationBase.GetTranslations(InClause, nLanguageID)

        'set the view to use for the translation.
        oView.Table = oDS.Tables(0)

        For Each oSingleValueControl In SingleValueControlArray

            Dim oType As Type = oSingleValueControl.ControlObject.GetType
            Dim oPropInfo As PropertyInfo = oType.GetProperty(TEXT)

            If Not oPropInfo Is Nothing Then

                'get the keyword from the object which holds the original text.
                sUIProgCode = CType(oPropInfo.GetValue(oSingleValueControl.ControlObject, Nothing), String).ToUpper

                ' get the translation by passing the original text and setting the "new" text
                ' to the value returned by the filtering of the dataview.
                oPropInfo.SetValue(oSingleValueControl.ControlObject, Translate(sUIProgCode, oView), Nothing)


            End If



        Next

    End Sub




#End Region


#Region "PUBLIC METHODS AND PROPERTIES"

    Public ReadOnly Property TranslationMissingCount() As Integer
        Get
            If Not mTranslationMissingList Is Nothing Then
                Return mTranslationMissingList.Count
            Else
                Return NONE
            End If

        End Get

    End Property


    'Public Property TranslationMissingList() As ArrayList
    '    Get

    '        Return mTranslationMissingList

    '    End Get

    '    Set(ByVal Value As ArrayList)

    '        mTranslationMissingList = Value

    '    End Set

    'End Property


#End Region

    '-------------------------------------
    'Name:Translate
    'Purpose:'separate the control types into 3 collections. Then translate each group.
    'Input Values:oCurrentPage- the main container for the page.
    '-------------------------------------
    Public Sub TranslateThePage(ByVal oCurrentObject As Control, ByVal nLanguageID As Guid)

        Dim sResult As String
        CollectTheTextFromTheControls(oCurrentObject, nLanguageID)

        sResult = BuildStringFromArray(TextFromControlsList) & ")"

        'if there are no controls then exit
        If sResult.Length <= 2 Then

            Exit Sub

        Else

            TranslateBasicControls(oSingleValueControlArray, sResult, nLanguageID)

            TranslateToolTips(oCurrentObject, nLanguageID)

        End If




    End Sub

    Sub TranslateToolTips(ByVal oCurrentObject As Control, ByVal nLanguageID As Guid)

        Dim sResultString As String
        Dim oDVToolTips As DataView

        If mlstTooltips.Count = 0 Then

            Exit Sub

        Else

            sResultString = BuildStringFromArray(mlstTooltips) & ")"
            oDVToolTips = GetToolTipDBTranslations(sResultString, nLanguageID)

            SetToolTipText(oCurrentObject, nLanguageID, oDVToolTips)

        End If

    End Sub

    Private Function GetToolTipDBTranslations(ByVal sResultString As String, ByVal nLanguageID As Guid) As DataView

        Dim oDS As DataSet
        Dim oDV As New DataView


        'Load the translation data from the database.
        oDS = TranslationBase.GetTranslations(sResultString, nLanguageID)

        'set the view to use for the translation.
        oDV.Table = oDS.Tables(0)

        Return oDV

    End Function

#Region "TOOLTIP ROUTINES"

    Sub SetToolTipText(ByVal oCurrentObject As Control, ByVal nLanguageID As Guid, ByVal oDVToolTips As DataView)

        Dim sResultString As String
        Dim oDS As DataSet
        Dim oItem As String
        Dim sCurrentText As String
        Dim oControl As Control
        Dim oType As Type
        Dim oPropToolTip As PropertyInfo

        If mlstTooltips.Count = 0 Then Exit Sub


        For Each oControl In oCurrentObject.Controls

            oType = Nothing
            oType = oControl.GetType

            If oControl.HasControls = True Then

                SetToolTipText(oControl, nLanguageID, oDVToolTips)

            End If
            oPropToolTip = oType.GetProperty(PAGETOOLTIP)

            If oPropToolTip Is Nothing Then

            Else
                sCurrentText = oPropToolTip.GetValue(oControl, Nothing).ToString
            End If

            If sCurrentText = String.Empty Then

            Else

                oPropToolTip.SetValue(oControl, Translate(sCurrentText, oDVToolTips), Nothing)

            End If

            sCurrentText = String.Empty
        Next

    End Sub


    Sub GetToolTipText(ByVal oCurrentObject As Control, ByVal nLanguageID As Guid)

        Dim sText As String = String.Empty
        Dim oType As Type = oCurrentObject.GetType

        Dim oPropTooTip As PropertyInfo = oType.GetProperty(TEXT)

        If Not oPropTooTip Is Nothing Then

            'get the keyword from the object which holds the original text.
            sText = CType(oPropTooTip.GetValue(oCurrentObject, Nothing), String)
            mlstTooltips.Add(sText)

        End If

    End Sub

#End Region

#Region "LABEL AND BUTTON ROUTINES"



    Private Sub AddControlToSingleValueArray(ByVal oControl As Control)


        Dim oSingleValueControl As New SingleValueControl

        With oSingleValueControl

            .ControlID = oControl.ID
            .ControlObject = oControl

        End With

        oSingleValueControlArray.Add(oSingleValueControl)


    End Sub






    Private Sub CollectTheTextFromTheControls(ByVal oCurrentObject As Control, ByVal nLanguageID As Guid)

        '  Dim oDB As New DbStruct
        Dim oDS As DataSet
        Dim oPageControl As Control
        Dim oStringBuilder As New StringBuilder
        Dim sCurrentText As String = String.Empty
        Dim sCurrentToolTipText As String = String.Empty


        For Each oPageControl In oCurrentObject.Controls

            'If oPageControl.ID <> String.Empty AndAlso (Not oPageControl.ID.ToUpper.EndsWith(TRANSLATION_EXCLUSION_MARK)) Then
            If oPageControl.ID Is Nothing OrElse Not oPageControl.ID.ToUpper.EndsWith(TRANSLATION_EXCLUSION_MARK) Then

                If oPageControl.HasControls Then

                    'jump into the next lower level
                    CollectTheTextFromTheControls(oPageControl, nLanguageID)

                End If

                If oPageControl.GetType.IsSubclassOf(GetType(System.Web.UI.WebControls.WebControl)) AndAlso Not oPageControl.ID Is Nothing AndAlso Not oPageControl.GetType.IsSubclassOf(GetType(System.Web.UI.WebControls.ListControl)) Then
                    sCurrentText = GetTextFromControl(oPageControl, TEXT)

                    'now get the tool tip text for translation.
                    sCurrentToolTipText = GetTextFromControl(oPageControl, PAGETOOLTIP)
                    If sCurrentToolTipText <> String.Empty Then
                        mlstTooltips.Add(sCurrentToolTipText)
                    End If


                    'load the control and original text into a an arry for reuse.
                    AddControlToSingleValueArray(oPageControl)

                    If oPageControl.GetType.ToString = PAGEDATAGRID Then

                        TranslateGridHeader(CType(oPageControl, DataGrid))
                        TranslateGridButtonColumns(CType(oPageControl, DataGrid))

                        If (Not CType(oPageControl.Page, ElitaPlusPage).IsNewUI) Then
                            'ALR - 9/18/2007 - Set the CSS Style for the datagrid 
                            If Not CType(oPageControl, DataGrid).CssClass = Me.DATAGRID_NOWRAP_STYLE OrElse CType(oPageControl, DataGrid).CssClass.Trim.Equals(String.Empty) Then
                                CType(oPageControl, DataGrid).CssClass = DATAGRIDSTYLE
                            End If
                        End If
                    End If

                End If


                TranslateRadioAndCheckBoxList(oPageControl)
                TranslateTabStrip(oPageControl)

            End If

            If sCurrentText.Trim <> String.Empty Then

                'add the text to build the sql.
                TextFromControlsList.Add(sCurrentText)

            End If

            'now clear out the text to avoid putting it in twice
            sCurrentText = String.Empty
            sCurrentToolTipText = String.Empty

        Next


    End Sub



    Private Sub TranslateRadioAndCheckBoxList(ByVal oPageControl As Control)

        '   If oPageControl.GetType.BaseType Is GetType(System.Web.UI.WebControls.ListControl) Then

        If oPageControl.GetType Is GetType(System.Web.UI.WebControls.CheckBoxList) _
 Or oPageControl.GetType Is GetType(System.Web.UI.WebControls.RadioButtonList) Then

            TranslateSingleListControl(CType(oPageControl, ListControl))
        End If
    End Sub

    Private Sub TranslateSingleListControl(ByVal oControl As ListControl)

        Dim oItem As ListItem
        Dim oTranslationItem As TranslationItem
        Dim oTranslationItemArray As New TranslationItemArray

        'load the array with translation items 
        For Each oItem In oControl.Items

            oTranslationItem = New TranslationItem

            With oTranslationItem

                .TextToTranslate = oItem.Text.ToUpper


            End With

            oTranslationItemArray.Add(oTranslationItem)

        Next

        'do the translation
        If (Not oTranslationItemArray Is Nothing And oTranslationItemArray.Items.Count > 0) Then
            Me.TranslateList(oTranslationItemArray, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
        End If

        'go thru the array and set the text of the control to it's matching translation.
        For Each oItem In oControl.Items

            If oTranslationItemArray.Contains(oItem.Text.ToUpper) Then

                oTranslationItem = oTranslationItemArray.Item(oItem.Text.ToUpper)
                oItem.Text = oTranslationItem.Translation

            End If

        Next

    End Sub

    Private Sub TranslateTabStrip(ByVal oPageControl As Control)

        If oPageControl.GetType Is GetType(Microsoft.Web.UI.WebControls.TabStrip) Then

            TranslateSingleTabStripControl(CType(oPageControl, Microsoft.Web.UI.WebControls.TabStrip))

        End If
    End Sub

    Private Sub TranslateSingleTabStripControl(ByVal oControl As Microsoft.Web.UI.WebControls.TabStrip)

        Dim oItem As Microsoft.Web.UI.WebControls.TabItem
        Dim oTranslationItem As TranslationItem
        Dim oTranslationItemArray As New TranslationItemArray

        'load the array with translation items 
        For Each oItem In oControl.Items

            oTranslationItem = New TranslationItem

            With oTranslationItem

                .TextToTranslate = oItem.Text.ToUpper


            End With

            oTranslationItemArray.Add(oTranslationItem)

        Next

        'do the translation
        Me.TranslateList(oTranslationItemArray, ElitaPlusIdentity.Current.ActiveUser.LanguageId)

        'go thru the array and set the text of the control to it's matching translation.
        For Each oItem In oControl.Items

            If oTranslationItemArray.Contains(oItem.Text.ToUpper) Then

                oTranslationItem = oTranslationItemArray.Item(oItem.Text.ToUpper)
                oItem.Text = oTranslationItem.Translation

            End If

        Next

    End Sub

    Private Shared Function GetTextFromControl(ByVal oControl As Object, ByVal sPropertyName As String) As String

        Dim sText As String = String.Empty
        Dim oType As Type = oControl.GetType
        Dim oPropInfo As PropertyInfo = oType.GetProperty(sPropertyName)

        If Not oPropInfo Is Nothing Then

            'get the keyword from the object which holds the original text.
            sText = CType(oPropInfo.GetValue(oControl, Nothing), String)

        End If

        Return sText.ToUpper


    End Function

    'Public Function TranslateList(ByVal aryItemsToTranslate As TranslationItemArray, ByVal CurrentLanguageID As Guid) As ArrayList


    '    Dim oDS As DataSet
    '    Dim oItem As TranslationItem
    '    Dim oView As New DataView
    '    Dim sResultString As String


    '    'build the in clause of the sql statement from the values in the grid header.
    '    sResultString = BuildStringFromTranslationArray(aryItemsToTranslate) & ")"

    '    'Load the translation data from the database.
    '    oDS = TranslationBase.GetTranslations(sResultString, CurrentLanguageID)

    '    'set the view to use for the translation.
    '    oView.Table = oDS.Tables(0)

    '    For Each oItem In aryItemsToTranslate.Items

    '        oItem.Translation = Translate(oItem.TextToTranslate, oView)

    '    Next

    '    '  Return Me.GetCurrentMissingTranslations
    '    Return Me.TranslationMissingList()

    'End Function

    'Public Function TranslateList(ByVal aryItemsToTranslate As ArrayList) As ArrayList

    '    Dim oDB As New DbStruct
    '    Dim oDS As DataSet
    '    Dim oControl As Control
    '    Dim oStringBuilder As New StringBuilder
    '    Dim sCurrentText As String = String.Empty
    '    Dim oItem As TranslationItem
    '    Dim oView As New DataView
    '    Dim aryList As New ArrayList

    '    Dim sResultString As String
    '    Dim oLanguageID As Guid

    '    For Each oItem In aryItemsToTranslate

    '        If oItem.TextToTranslate.ToString.Trim <> String.Empty Then
    '            aryList.Add(oItem.TextToTranslate.ToString.Trim.ToUpper)
    '        End If
    '    Next

    '    'build the in clause of the sql statement from the values in the grid header.
    '    sResultString = BuildStringFromArray(aryList) & ")"


    '    'Load the translation data from the database.
    '    oDS = TranslationBase.GetTranslations(sResultString, ElitaPlusIdentity.Current.ActiveUser.LanguageId)

    '    'set the view to use for the translation.
    '    oView.Table = oDS.Tables(0)

    '    For Each oItem In aryItemsToTranslate

    '        oItem.Translation = Translate(oItem.TextToTranslate, oView)
    '    Next

    '    '  Return Me.GetCurrentMissingTranslations
    '    Return Me.TranslationMissingList

    'End Function


    'Private Function BuildStringFromTranslationArray(ByVal aryList As TranslationItemArray, Optional ByVal sCharToJoin As String = ", ") As String

    '    Dim sItem As String
    '    Dim oWorkingArray(aryList.CurrentCount - 1) As String
    '    Dim nIndex As Integer = 0
    '    Dim sResult As String = String.Empty

    '    Dim oTranslationItem As TranslationItem
    '    Dim oDictionaryEntry As DictionaryEntry

    '    For Each oTranslationItem In aryList.Items

    '        oWorkingArray(nIndex) = "'" & oTranslationItem.TextToTranslate & "'"
    '        nIndex += 1

    '    Next

    '    sResult = String.Join(sCharToJoin, oWorkingArray)
    '    Return sResult

    'End Function

    'Private Shared Function BuildStringFromArray(ByVal aryList As ArrayList, Optional ByVal sCharToJoin As String = ", ") As String

    '    Dim sItem As String
    '    Dim oWorkingArray(aryList.Count - 1) As String
    '    Dim nIndex As Integer = 0
    '    Dim sResult As String = String.Empty

    '    For Each sItem In aryList
    '        oWorkingArray(nIndex) = "'" & sItem.Replace("'", " ") & "'"
    '        nIndex += 1
    '    Next

    '    sResult = String.Join(sCharToJoin, oWorkingArray)
    '    Return sResult
    'End Function


#End Region

#Region "GRID RELATED FUNCTIONS"

    Private Sub LoadTranslationTypesForGrid()

        mlstTranslatableGridControls.Add(PAGEGRIDBUTTON)
        mlstTranslatableGridControls.Add(PAGEBUTTONCOLUMN)

    End Sub

    Public Overloads Function TranslateControlsInGrid(ByVal oGrid As DataGrid) As ArrayList

        LoadTranslationTypesForGrid()



        Dim oGridRow As DataGridItem
        Dim aryList As New ArrayList

        TranslateGridButtonColumns(oGrid)

        For Each oGridRow In oGrid.Items
            aryList.AddRange(GetTextFromControlsInGrid(oGridRow))
        Next


        If aryList.Count = 0 Then
            'nothing found in the arraylist of sql so don't bother translating.
        Else

            TranslateListOfControlsInGrid(aryList)

        End If


        ' Return Me.GetCurrentMissingTranslations
        Return Me.TranslationMissingList

    End Function

    Public Overloads Function TranslateControlsInGrid(ByVal oGrid As GridView) As ArrayList

        LoadTranslationTypesForGrid()



        Dim oGridRow As GridViewRow
        Dim aryList As New ArrayList

        TranslateGridButtonColumns(oGrid)

        For Each oGridRow In oGrid.Rows
            aryList.AddRange(GetTextFromControlsInGrid(oGridRow))
        Next


        If aryList.Count = 0 Then
            'nothing found in the arraylist of sql so don't bother translating.
        Else

            TranslateListOfControlsInGrid(aryList)

        End If


        ' Return Me.GetCurrentMissingTranslations
        Return Me.TranslationMissingList

    End Function

    '-------------------------------------
    'Name:GetTextFromControlsInGrid
    'Purpose:Retrieve an array of text from all the cells in a single grid row.
    'Input Values:oGridRow - a single grid item= a row.
    'Uses:GetControlFromSingleCell
    '-------------------------------------

    Private Overloads Function GetTextFromControlsInGrid(ByVal oGridRow As DataGridItem) As ArrayList


        Dim oItem As DataGridItem
        Dim aryTextFromControls As New ArrayList
        Dim nCount As Integer
        Dim nColumnCount As Integer = oGridRow.Cells.Count

        Dim oCurrentControl As Control
        Dim sTextInControl As String



        For nCount = 0 To nColumnCount - 1
            Dim oSingleValueControl As New SingleValueControl
            If oGridRow.Cells(nCount).HasControls Then


                With oSingleValueControl
                    'get a reference to the control in a single cell.
                    oCurrentControl = GetControlFromSingleCell(oGridRow.Cells(nCount))

                    'use this reference to retrieve the text, type and id.
                    If Not oCurrentControl Is Nothing Then

                        .ControlType = oCurrentControl.GetType.ToString

                        If oCurrentControl.ID <> String.Empty Then

                            .ControlID = oCurrentControl.ID

                            'add the text of the control to be used for translation.
                            sTextInControl = GetTextFromControl(oCurrentControl, TEXT)

                            If sTextInControl = String.Empty Then
                                'the control has no text to translate so don't add it.
                            Else

                                aryTextFromControls.Add(sTextInControl)
                                .ControlObject = oCurrentControl

                                'add the control to the array  so after the aryList has been used to build the sql,
                                'the mcontrolsingrid arraylist can be used to set the values.

                                mControlsInGrid.Add(oSingleValueControl)

                            End If

                        End If

                    End If

                End With
            End If

        Next

        Return aryTextFromControls

    End Function


    '-------------------------------------
    'Name:GetTextFromControlsInGrid
    'Purpose:Retrieve an array of text from all the cells in a single grid row.
    'Input Values:oGridRow - a single grid item= a row.
    'Uses:GetControlFromSingleCell
    '-------------------------------------

    Private Overloads Function GetTextFromControlsInGrid(ByVal oGridRow As GridViewRow) As ArrayList


        Dim oItem As GridViewRow
        Dim aryTextFromControls As New ArrayList
        Dim nCount As Integer
        Dim nColumnCount As Integer = oGridRow.Cells.Count

        Dim oCurrentControl As Control
        Dim sTextInControl As String



        For nCount = 0 To nColumnCount - 1
            Dim oSingleValueControl As New SingleValueControl
            If oGridRow.Cells(nCount).HasControls Then


                With oSingleValueControl
                    'get a reference to the control in a single cell.
                    oCurrentControl = GetControlFromSingleCell(oGridRow.Cells(nCount))

                    'use this reference to retrieve the text, type and id.
                    If Not oCurrentControl Is Nothing Then

                        .ControlType = oCurrentControl.GetType.ToString

                        If oCurrentControl.ID <> String.Empty Then

                            .ControlID = oCurrentControl.ID

                            'add the text of the control to be used for translation.
                            sTextInControl = GetTextFromControl(oCurrentControl, TEXT)

                            If sTextInControl = String.Empty Then
                                'the control has no text to translate so don't add it.
                            Else

                                aryTextFromControls.Add(sTextInControl)
                                .ControlObject = oCurrentControl

                                'add the control to the array  so after the aryList has been used to build the sql,
                                'the mcontrolsingrid arraylist can be used to set the values.

                                mControlsInGrid.Add(oSingleValueControl)

                            End If

                        End If

                    End If

                End With
            End If

        Next

        Return aryTextFromControls

    End Function

    Private Function GetControlFromSingleCell(ByVal oCell As TableCell) As Control

        Dim oControl As Control

        Dim sTypeName As String
        Dim ControlCounter As Integer

        Dim nCount As Integer
        For nCount = 0 To oCell.Controls.Count - 1

            'this code needs to check which types of controls to retrieve.
            'make sure you aren't adding the default literal control and that it has an id.
            sTypeName = oCell.Controls(nCount).GetType.Name
            If sTypeName <> GetType(System.Web.UI.LiteralControl).Name _
            And oCell.Controls(nCount).ID <> String.Empty Then

                If mlstTranslatableGridControls.IndexOf(sTypeName) <> -1 Then

                    oControl = oCell.Controls(nCount)

                    'return the first control that is of a type that is allowed to be translated within a grid.

                    Exit For

                End If

            End If


        Next

        Return oControl

    End Function



    Private Sub TranslateListOfControlsInGrid(ByVal aryGridTextFromControls As ArrayList)

        Dim aryList As New ArrayList
        Dim sResultString As String
        Dim ods As DataSet
        Dim oView As DataView

        'build the in clause of the sql statement from the values in the grid header.
        sResultString = BuildStringFromArray(aryGridTextFromControls) & ")"

        'make sure the sql statement is real.
        If sResultString.Length < 1 Then Exit Sub

        'use the basic pattern to translate the controls values.
        TranslateBasicControls(mControlsInGrid, sResultString, ElitaPlusIdentity.Current.ActiveUser.LanguageId)

    End Sub

    Private Overloads Sub TranslateGridButtonColumns(ByVal oGrid As DataGrid)
        Dim ods As DataSet
        Dim oView As New DataView

        Dim aryList As ArrayList
        Dim sResultString As String

        'nothing in the headers to translate. no columns found.

        aryList = GetGridButtonColumnText(oGrid)

        If aryList Is Nothing Then

            'nothing in the buttoncolumns to translate. no columns found.
            Exit Sub
        Else

            'build the in clause of the sql statement from the values in the grid header.
            sResultString = BuildStringFromArray(aryList) & ")"

            'make sure the sql statement is real.
            If sResultString.Length <= 2 Then Exit Sub

            'Load the translation data from the database.
            ods = TranslationBase.GetTranslations(sResultString, ElitaPlusIdentity.Current.ActiveUser.LanguageId)

            'set the view to use for the translation.
            oView.Table = ods.Tables(0)

            'loop thru the headers and set the values according to their translated values.
            SetGridButtonColumnText(oGrid, oView)

        End If



    End Sub

    Private Overloads Sub TranslateGridButtonColumns(ByVal oGrid As GridView)
        Dim ods As DataSet
        Dim oView As New DataView

        Dim aryList As ArrayList
        Dim sResultString As String

        'nothing in the headers to translate. no columns found.

        aryList = GetGridButtonColumnText(oGrid)

        If aryList Is Nothing Then

            'nothing in the buttoncolumns to translate. no columns found.
            Exit Sub
        Else

            'build the in clause of the sql statement from the values in the grid header.
            sResultString = BuildStringFromArray(aryList) & ")"

            'make sure the sql statement is real.
            If sResultString.Length <= 2 Then Exit Sub

            'Load the translation data from the database.
            ods = TranslationBase.GetTranslations(sResultString, ElitaPlusIdentity.Current.ActiveUser.LanguageId)

            'set the view to use for the translation.
            oView.Table = ods.Tables(0)

            'loop thru the headers and set the values according to their translated values.
            SetGridButtonColumnText(oGrid, oView)

        End If



    End Sub
    Private Overloads Sub SetGridButtonColumnText(ByVal oGrid As DataGrid, ByVal oView As DataView)

        Dim nCounter As Integer
        Dim nColumnCount As Integer = oGrid.Columns.Count
        Dim oColumnType As DataGridColumn
        Dim oButtonColumn As ButtonColumn
        Dim sText As String

        'loop thru the columns of the grid and check for button columns
        For nCounter = 0 To nColumnCount - 1

            oColumnType = oGrid.Columns(nCounter)

            If oColumnType.GetType Is GetType(ButtonColumn) Then

                oButtonColumn = CType(oGrid.Columns(nCounter), ButtonColumn)

                sText = oButtonColumn.Text

                If sText <> String.Empty Then
                    'set the translation value of the button column text in the grid
                    oButtonColumn.Text = Translate(sText, oView)
                End If
            Else

                'not a button column so do nothing.

            End If

        Next

    End Sub

    Private Overloads Sub SetGridButtonColumnText(ByVal oGrid As GridView, ByVal oView As DataView)

        Dim nCounter As Integer
        Dim nColumnCount As Integer = oGrid.Columns.Count
        Dim oColumnType As DataControlField
        Dim oButtonColumn As CommandField
        Dim sText As String
        Dim dText As String
        Dim iText As String
        Dim cText As String
        Dim eText As String

        'loop thru the columns of the grid and check for button columns
        For nCounter = 0 To nColumnCount - 1

            oColumnType = oGrid.Columns(nCounter)

            If oColumnType.GetType Is GetType(CommandField) Then

                oButtonColumn = CType(oGrid.Columns(nCounter), CommandField)

                If oButtonColumn.ShowSelectButton Then
                    sText = oButtonColumn.SelectText
                End If

                If oButtonColumn.ShowDeleteButton Then
                    dText = oButtonColumn.DeleteText
                End If

                If oButtonColumn.ShowInsertButton Then
                    iText = oButtonColumn.InsertText
                End If

                If oButtonColumn.ShowCancelButton Then
                    cText = oButtonColumn.CancelText
                End If

                If oButtonColumn.ShowEditButton Then
                    eText = oButtonColumn.EditText
                End If

                If sText <> String.Empty Then
                    oButtonColumn.SelectText = Translate(sText, oView)
                End If

                If dText <> String.Empty Then
                    oButtonColumn.DeleteText = Translate(dText, oView)
                End If

                If iText <> String.Empty Then
                    oButtonColumn.InsertText = Translate(iText, oView)
                End If

                If cText <> String.Empty Then
                    oButtonColumn.CancelText = Translate(cText, oView)
                End If

                If eText <> String.Empty Then
                    oButtonColumn.EditText = Translate(eText, oView)
                End If
            Else

                'not a button column so do nothing.

            End If

        Next

    End Sub

    Public Overloads Function TranslateGridHeader(ByVal oGrid As System.Web.UI.WebControls.DataGrid) As ArrayList
        Dim ods As DataSet
        Dim oView As New DataView

        Dim aryList As ArrayList
        Dim sResultString As String


        aryList = GetGridHeaderText(oGrid)
        If aryList Is Nothing Then
            'nothing in the headers to translate. no columns found.
            Exit Function
        Else

            'build the in clause of the sql statement from the values in the grid header.
            sResultString = BuildStringFromArray(aryList) & ")"

            'make sure the sql statement is real.
            If sResultString.Length < 1 Then Exit Function

            'Load the translation data from the database.
            ods = TranslationBase.GetTranslations(sResultString, ElitaPlusIdentity.Current.ActiveUser.LanguageId)

            'set the view to use for the translation.
            oView.Table = ods.Tables(0)

            'loop thru the headers and set the values according to their translated values.
            SetGridHeaderText(oGrid, oView)

        End If


        'Return Me.GetCurrentMissingTranslations
        Return Me.TranslationMissingList


    End Function

    Public Overloads Function TranslateGridHeader(ByVal oGrid As System.Web.UI.WebControls.GridView) As ArrayList
        Dim ods As DataSet
        Dim oView As New DataView

        Dim aryList As ArrayList
        Dim sResultString As String


        aryList = GetGridHeaderText(oGrid)
        If aryList Is Nothing Then
            'nothing in the headers to translate. no columns found.
            Exit Function
        Else

            'build the in clause of the sql statement from the values in the grid header.
            sResultString = BuildStringFromArray(aryList) & ")"

            'make sure the sql statement is real.
            If sResultString.Length < 1 Then Exit Function

            'Load the translation data from the database.
            ods = TranslationBase.GetTranslations(sResultString, ElitaPlusIdentity.Current.ActiveUser.LanguageId)

            'set the view to use for the translation.
            oView.Table = ods.Tables(0)

            'loop thru the headers and set the values according to their translated values.
            SetGridHeaderText(oGrid, oView)

        End If


        'Return Me.GetCurrentMissingTranslations
        Return Me.TranslationMissingList


    End Function

    Private Overloads Sub SetGridHeaderText(ByVal oGrid As System.Web.UI.WebControls.DataGrid, ByVal oView As DataView)

        Dim nCounter As Integer
        Dim nColumnCount As Integer = oGrid.Columns.Count
        Dim sUIProgCode As String

        For nCounter = 0 To nColumnCount - 1
            sUIProgCode = oGrid.Columns(nCounter).HeaderText.ToUpper

            'set the translation value of the header text in the grid
            oGrid.Columns(nCounter).HeaderText = Translate(sUIProgCode, oView)
        Next

    End Sub

    Private Overloads Sub SetGridHeaderText(ByVal oGrid As System.Web.UI.WebControls.GridView, ByVal oView As DataView)

        Dim nCounter As Integer
        Dim nColumnCount As Integer = oGrid.Columns.Count
        Dim sUIProgCode As String

        For nCounter = 0 To nColumnCount - 1
            sUIProgCode = oGrid.Columns(nCounter).HeaderText.ToUpper

            'set the translation value of the header text in the grid
            oGrid.Columns(nCounter).HeaderText = Translate(sUIProgCode, oView)
        Next

    End Sub

    Private Overloads Function GetGridButtonColumnText(ByVal oGrid As System.Web.UI.WebControls.DataGrid) As ArrayList

        Dim nCounter As Integer
        Dim nColumnCount As Integer = oGrid.Columns.Count
        Dim aryList As New ArrayList
        Dim oButtonColumn As ButtonColumn
        Dim oColumnType As DataGridColumn
        Dim sText As String

        If nColumnCount = 0 Then Exit Function

        'loop thru the columns of the grid and check for button columns
        For nCounter = 0 To nColumnCount - 1

            oColumnType = oGrid.Columns(nCounter)

            If oColumnType.GetType Is GetType(ButtonColumn) Then

                oButtonColumn = CType(oGrid.Columns(nCounter), ButtonColumn)

                sText = oButtonColumn.Text

                If sText <> String.Empty Then

                    aryList.Add(sText.ToUpper)

                End If

            Else
                'not a button column so do nothing.
            End If


        Next

        Return aryList

    End Function

    Private Overloads Function GetGridButtonColumnText(ByVal oGrid As System.Web.UI.WebControls.GridView) As ArrayList

        Dim nCounter As Integer
        Dim nColumnCount As Integer = oGrid.Columns.Count
        Dim aryList As New ArrayList
        Dim oButtonColumn As CommandField
        Dim sText As String
        Dim dText As String
        Dim iText As String
        Dim cText As String
        Dim eText As String

        If nColumnCount = 0 Then Exit Function

        'loop thru the columns of the grid and check for button columns
        For nCounter = 0 To nColumnCount - 1


            If oGrid.Columns(nCounter).GetType Is GetType(CommandField) Then

                oButtonColumn = CType(oGrid.Columns(nCounter), CommandField)

                If oButtonColumn.ShowSelectButton Then
                    sText = oButtonColumn.SelectText
                End If

                If oButtonColumn.ShowDeleteButton Then
                    dText = oButtonColumn.DeleteText
                End If

                If oButtonColumn.ShowInsertButton Then
                    iText = oButtonColumn.InsertText
                End If

                If oButtonColumn.ShowCancelButton Then
                    cText = oButtonColumn.CancelText
                End If

                If oButtonColumn.ShowEditButton Then
                    eText = oButtonColumn.EditText
                End If

                If sText <> String.Empty Then

                    aryList.Add(sText.ToUpper)

                End If

                If dText <> String.Empty Then

                    aryList.Add(dText.ToUpper)

                End If

                If iText <> String.Empty Then

                    aryList.Add(iText.ToUpper)

                End If

                If cText <> String.Empty Then

                    aryList.Add(cText.ToUpper)

                End If

                If eText <> String.Empty Then

                    aryList.Add(eText.ToUpper)

                End If

            Else
                'not a button column so do nothing.
            End If


        Next

        Return aryList

    End Function


    Private Overloads Function GetGridHeaderText(ByVal oGrid As System.Web.UI.WebControls.DataGrid) As ArrayList

        Dim nCounter As Integer
        Dim nColumnCount As Integer = oGrid.Columns.Count
        Dim aryList As New ArrayList

        For nCounter = 0 To nColumnCount - 1

            aryList.Add((oGrid.Columns(nCounter).HeaderText.ToUpper))

        Next

        Return aryList

    End Function

    Private Overloads Function GetGridHeaderText(ByVal oGrid As System.Web.UI.WebControls.GridView) As ArrayList

        Dim nCounter As Integer
        Dim nColumnCount As Integer = oGrid.Columns.Count
        Dim aryList As New ArrayList

        For nCounter = 0 To nColumnCount - 1

            aryList.Add((oGrid.Columns(nCounter).HeaderText.ToUpper))

        Next

        Return aryList

    End Function

#End Region


End Class

