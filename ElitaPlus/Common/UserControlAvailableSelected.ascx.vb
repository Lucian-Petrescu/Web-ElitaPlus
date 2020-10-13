Imports System.Diagnostics

Namespace Generic
    Partial Class UserControlAvailableSelected
        Inherits UserControl

#Region "Constants"
        Public Const FIELD_SEPARATOR As Char = "¦"c

        Protected Const EXCEPTION_TEXT As String = "SrcDstListChooser can not access Data Source -- "
#End Region

#Region "Types"

        Public Enum EventTypes
            ATTACH
            DETACH
        End Enum

#End Region

#Region "Events"
        '   Public Event SelectedListChanged(ByVal aSrc As UserControlAvailableSelected, ByVal aReason As EventArg)
        Public Event Attach(aSrc As UserControlAvailableSelected, attachedList As ArrayList)
        Public Event Detach(aSrc As UserControlAvailableSelected, detachedList As ArrayList)

#End Region

#Region "Attributes"

        Public Enum EventArg
            ADD
            ADD_ALL
            REMOVE
            REMOVE_ALL
        End Enum

#End Region

#Region "Variables"
        Private _ShowUpButton As Boolean
        Private _ShowDownButton As Boolean
#End Region

#Region "Properties"

        Public Property AvailableDesc() As String
            Get

            End Get
            Set(Value As String)
                moAvailableTitle.Text = Value
            End Set
        End Property
        Public Property SelectedDesc() As String
            Get

            End Get
            Set(Value As String)
                moSelectedTitle.Text = Value
            End Set
        End Property


        Public ReadOnly Property AvailableList() As ArrayList
            Get
                Dim oAvailableArray As ArrayList = New ArrayList
                Dim oItem As ListItem
                oAvailableArray.Clear()
                For Each oItem In moAvailableList.Items
                    oAvailableArray.Add(oItem.Value)
                Next
                Return oAvailableArray
            End Get

        End Property

        Public ReadOnly Property SelectedList() As ArrayList
            Get
                Dim oSelectedArray As ArrayList = New ArrayList
                Dim oItem As ListItem
                oSelectedArray.Clear()
                For Each oItem In moSelectedList.Items
                    oSelectedArray.Add(oItem.Value)
                Next
                Return oSelectedArray
            End Get

        End Property

        Public ReadOnly Property SelectedListCodes() As ArrayList
            Get
                Dim oSelectedArray As ArrayList = New ArrayList
                Dim oItem As ListItem
                oSelectedArray.Clear()
                For Each oItem In moSelectedList.Items
                    oSelectedArray.Add(oItem.Text)
                Next
                Return oSelectedArray
            End Get

        End Property

        Public ReadOnly Property DetachList() As ArrayList
            Get
                Dim oDetachArray As ArrayList

                If (moAvailableValues.Value IsNot String.Empty) Then
                    ' oDetachArray = moAvailableValues.Value.Split(AppConfig.FIELD_SEPARATOR)
                    oDetachArray = New ArrayList(moAvailableValues.Value.Split(FIELD_SEPARATOR))
                End If

                Return oDetachArray
            End Get

        End Property

        Public ReadOnly Property AttachList() As ArrayList
            Get
                Dim oAttachArray As ArrayList

                If (moSelectedValues.Value IsNot String.Empty) Then
                    ' oDetachArray = moAvailableValues.Value.Split(AppConfig.FIELD_SEPARATOR)
                    oAttachArray = New ArrayList(moSelectedValues.Value.Split(FIELD_SEPARATOR))
                End If

                Return oAttachArray
            End Get

        End Property

        Public Property BackColor() As String
            Get
                Return (moOutTable.BgColor)
            End Get
            Set(Value As String)
                moOutTable.BgColor = Value
            End Set
        End Property

        Private Shadows ReadOnly Property Page() As ElitaPlusPage
            Get
                Return CType(MyBase.Page, ElitaPlusPage)
            End Get
        End Property

        Public ReadOnly Property SelectedTitleLabel() As Label
            Get
                Return CType(FindControl("moSelectedTitle"), Label)
            End Get
        End Property

        Public ReadOnly Property SelectedListListBox() As ListBox
            Get
                Return moSelectedList
            End Get
        End Property

        Public Property ShowUpButton() As Boolean
            Get
                Return _ShowUpButton
            End Get
            Set(value As Boolean)
                _ShowUpButton = value
            End Set
        End Property

        Public Property ShowDownButton() As Boolean
            Get
                Return _ShowDownButton
            End Get
            Set(value As Boolean)
                _ShowDownButton = value
            End Set
        End Property

#End Region

#Region "Handlers"

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

        Private Sub Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            If IsPostBack Then
                ReadValuesFromClient()
            End If
            btnMoveUp.Visible = ShowUpButton
            btnMoveDown.Visible = ShowDownButton
            RegisterClientScript()
        End Sub

        'Private Sub BtnAdd_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAdd_WRITE.Click
        '    BtnAdd_Action()
        '    Me.EnableDisableButtons()
        '    RemoveSelection(moSelectedList)
        'End Sub

        'Private Sub BtnAddAll_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAddAll_WRITE.Click
        '    BtnAddAll_Action()
        '    Me.EnableDisableButtons()
        '    RemoveSelection(moSelectedList)
        'End Sub

        'Private Sub BtnRemove_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRemove_WRITE.Click
        '    BtnRemove_Action()
        '    Me.EnableDisableButtons()
        '    RemoveSelection(moAvailableList)
        'End Sub

        'Private Sub BtnRemoveAll_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRemoveAll_WRITE.Click
        '    BtnRemoveAll_Action()
        '    Me.EnableDisableButtons()
        '    RemoveSelection(moAvailableList)
        'End Sub

        Private Sub btnMoveUp_Click(sender As Object, e As EventArgs) Handles btnMoveUp.Click
            If AreMultipleRowsSelected(moSelectedList) Then
                Dim iSelectedIndex As Integer = moSelectedList.SelectedIndex
                If iSelectedIndex > 0 Then 'not the first one
                    Dim tempItem As ListItem = moSelectedList.SelectedItem
                    moSelectedList.Items.Remove(tempItem)
                    moSelectedList.Items.Insert(iSelectedIndex - 1, tempItem)
                End If
            End If
        End Sub

        Private Sub btnMoveDown_Click(sender As Object, e As EventArgs) Handles btnMoveDown.Click
            If AreMultipleRowsSelected(moSelectedList) Then
                Dim iSelectedIndex As Integer = moSelectedList.SelectedIndex
                If iSelectedIndex <> moSelectedList.Items.Count - 1 Then 'Not the last one
                    Dim tempItem As ListItem = moSelectedList.SelectedItem
                    moSelectedList.Items.Remove(tempItem)
                    moSelectedList.Items.Insert(iSelectedIndex + 1, tempItem)
                End If
            End If
        End Sub

        Private Function AreMultipleRowsSelected(sender As ListBox) As Boolean
            Dim iCounter As Integer()
            iCounter = sender.GetSelectedIndices()
            If iCounter.Count = 1 Then
                Return True
            End If
            Return False
        End Function

#End Region

        '#Region "Button Management"

        '        Private Sub RemoveSelection(ByVal aListBox As ListBox)
        '            Dim oItem As ListItem
        '            For Each oItem In aListBox.Items
        '                oItem.Selected = False
        '            Next
        '        End Sub

        '        Private Sub RemoveFromList(ByVal BoxToRemove As ListBox, ByVal BoxThatContains As ListBox)
        '            Dim oItem As ListItem
        '            For Each oItem In BoxThatContains.Items
        '                BoxToRemove.Items.Remove(oItem)
        '            Next
        '        End Sub

        '        Private Sub AddToListBox(ByVal BoxToAdd As ListBox, ByVal BoxThatContains As ListBox)
        '            Dim oItem As ListItem
        '            For Each oItem In BoxThatContains.Items
        '                If oItem.Selected = True Then
        '                    BoxToAdd.Items.Add(oItem)
        '                End If
        '            Next
        '        End Sub

        '        Private Sub AddAllList(ByVal BoxToAdd As ListBox, ByVal BoxThatContains As ListBox)
        '            Dim oItem As ListItem
        '            For Each oItem In BoxThatContains.Items
        '                BoxToAdd.Items.Add(oItem)
        '            Next
        '        End Sub

        'Private Sub SetClientInput(ByVal serverList As ListBox, _
        '        ByVal clientTexts As HtmlInputHidden, ByVal clientValues As HtmlInputHidden)
        '    Dim totItems As Integer = serverList.Items.Count
        '    Dim i As Integer

        '    clientTexts.Value = String.Empty
        '    clientValues.Value = String.Empty
        '    If totItems > 0 Then
        '        clientTexts.Value = serverList.Items(0).Text
        '        clientValues.Value = serverList.Items(0).Value
        '    End If
        '    totItems -= 1
        '    For i = 1 To totItems
        '        clientTexts.Value &= AppConfig.FIELD_SEPARATOR & serverList.Items(i).Text
        '        clientValues.Value &= AppConfig.FIELD_SEPARATOR & serverList.Items(i).Value
        '    Next
        'End Sub

        'Public Sub BtnAdd_Action()
        '    Dim selectedItemValues As ArrayList = Me.GetSelectedValueListCollection(Me.moAvailableList)
        '    AddToListBox(moSelectedList, moAvailableList)
        '    Sort(moSelectedList)
        '    'SelectedListListBox = moSelectedList
        '    RemoveFromList(moAvailableList, moSelectedList)
        '    RaiseEvent Attach(Me, selectedItemValues)
        '    RaiseEvent SelectedListChanged(Me, EventArg.ADD)
        'End Sub

        'Private Sub BtnAddAll_Action()
        '    Dim itemValues As ArrayList = Me.GetAllValueListCollection(Me.moAvailableList)
        '    AddAllList(moSelectedList, moAvailableList)
        '    Sort(moSelectedList)
        '    'SelectedListListBox = moSelectedList
        '    RemoveFromList(moAvailableList, moSelectedList)
        '    RaiseEvent Attach(Me, itemValues)
        '    RaiseEvent SelectedListChanged(Me, EventArg.ADD_ALL)
        'End Sub

        'Private Sub BtnRemove_Action()
        '    Dim selectedItemValues As ArrayList = Me.GetSelectedValueListCollection(Me.moSelectedList)
        '    AddToListBox(moAvailableList, moSelectedList)
        '    Sort(moAvailableList)
        '    RemoveFromList(moSelectedList, moAvailableList)
        '    RaiseEvent Detach(Me, selectedItemValues)
        '    RaiseEvent SelectedListChanged(Me, EventArg.REMOVE)
        'End Sub

        'Private Sub BtnRemoveAll_Action()
        '    Dim itemValues As ArrayList = Me.GetAllValueListCollection(Me.moSelectedList)
        '    AddAllList(moAvailableList, moSelectedList)
        '    Sort(moAvailableList)
        '    RemoveFromList(moSelectedList, moAvailableList)
        '    RaiseEvent Detach(Me, itemValues)
        '    RaiseEvent SelectedListChanged(Me, EventArg.REMOVE_ALL)
        'End Sub

        'Private Sub EnableDisableButtons()
        '    ControlMgr.SetEnableControl(Page, moAvailableList, True)
        '    ControlMgr.SetEnableControl(Page, moSelectedList, True)
        '    ControlMgr.SetEnableControl(Page, BtnAdd_WRITE, True)
        '    ControlMgr.SetEnableControl(Page, BtnAddAll_WRITE, True)
        '    ControlMgr.SetEnableControl(Page, BtnRemove_WRITE, True)
        '    ControlMgr.SetEnableControl(Page, BtnRemoveAll_WRITE, True)
        '    If moAvailableList.Items.Count = 0 Then
        '        ControlMgr.SetEnableControl(Page, BtnAdd_WRITE, False)
        '        ControlMgr.SetEnableControl(Page, BtnAddAll_WRITE, False)
        '    End If
        '    If Me.moSelectedList.Items.Count = 0 Then
        '        ControlMgr.SetEnableControl(Page, BtnRemove_WRITE, False)
        '        ControlMgr.SetEnableControl(Page, BtnRemoveAll_WRITE, False)
        '    End If
        'End Sub


        'Private Function GetSelectedValueListCollection(ByVal listCtr As ListControl) As ArrayList
        '    Dim item As ListItem
        '    Dim result As New ArrayList
        '    For Each item In listCtr.Items
        '        If item.Selected Then
        '            result.Add(item.Value)
        '        End If
        '    Next
        '    Return result
        'End Function

        'Private Function GetAllValueListCollection(ByVal listCtr As ListControl) As ArrayList
        '    Dim item As ListItem
        '    Dim result As New ArrayList
        '    For Each item In listCtr.Items
        '        result.Add(item.Value)
        '    Next
        '    Return result
        'End Function

        '#End Region

#Region "Set List Data"


        Public Sub SetAvailableData(dv As DataView, textColumnName As String, guidValueColumnName As String)
            ElitaPlusPage.BindListControlToDataView(moAvailableList, dv, textColumnName, guidValueColumnName, False)
            '  SetClientInput(moAvailableList, moAvailableTexts, moAvailableValues)
            '  Me.EnableDisableButtons()
        End Sub

        Public Sub SetSelectedData(dv As DataView, textColumnName As String, guidValueColumnName As String, _
                                   Optional ByVal AddNothingSelected As Boolean = True, Optional ByVal SortByTextColumn As Boolean = True)
            ElitaPlusPage.BindListControlToDataView(moSelectedList, dv, textColumnName, guidValueColumnName, False, SortByTextColumn)
            '  SetClientInput(moSelectedList, moSelectedTexts, moSelectedValues)
            '  Me.EnableDisableButtons()
        End Sub

        Public Sub RemoveSelectedFromAvailable()
            For Each lst As ListItem In moSelectedList.Items
                Dim lst1 As ListItem = moAvailableList.Items.FindByText(lst.Text)
                If lst1 IsNot Nothing Then
                    moAvailableList.Items.Remove(lst1)
                End If
            Next
        End Sub

#End Region

#Region "Sort"
        Private Class TheComparer
            Implements IComparer

            Function Compare(x As Object, y As Object) As Integer _
                  Implements IComparer.Compare
                Dim oXItem As ListItem = CType(x, ListItem)
                Dim oYItem As ListItem = CType(y, ListItem)

                Return New CaseInsensitiveComparer().Compare(oXItem.Text, oYItem.Text)

            End Function
        End Class

        Private Sub Sort(aList As ListBox)
            Dim oArray As ArrayList = New ArrayList
            Dim oItem As ListItem
            Dim oComparer As IComparer = New TheComparer

            For Each oItem In aList.Items
                oArray.Add(oItem)
            Next
            oArray.Sort(oComparer)
            aList.Items.Clear()
            For Each oItem In oArray
                aList.Items.Add(oItem)
            Next

        End Sub

#End Region

#Region "Client & JavaScript"

        ' Insert elements into source and delete from target
        Private Sub SetServerList(oEvent As EventTypes, sourceList As ListBox, _
                       clientTexts As HtmlInputHidden, _
                       clientValues As HtmlInputHidden, targetList As ListBox)
            Dim textArray(), valueArray() As String
            Dim oListItem As ListItem
            Dim index As Integer

            If (clientTexts.Value IsNot String.Empty) Then
                textArray = clientTexts.Value.Split(FIELD_SEPARATOR)
                valueArray = clientValues.Value.Split(FIELD_SEPARATOR)
                For index = 0 To (textArray.Length - 1)
                    oListItem = New ListItem(textArray(index), valueArray(index))
                    sourceList.Items.Add(oListItem)     ' Insert elements into source
                    targetList.Items.Remove(oListItem)  ' Delete elements from target
                Next
                If (oEvent = EventTypes.DETACH) Then
                    RaiseEvent Detach(Me, DetachList)
                ElseIf (oEvent = EventTypes.ATTACH) Then
                    RaiseEvent Attach(Me, AttachList)
                End If
                clientTexts.Value = String.Empty
                clientValues.Value = String.Empty
            End If

        End Sub

        Private Sub ReadValuesFromClient()
            SetServerList(EventTypes.DETACH, moAvailableList, moAvailableTexts, moAvailableValues, moSelectedList)
            SetServerList(EventTypes.ATTACH, moSelectedList, moSelectedTexts, moSelectedValues, moAvailableList)
        End Sub

        Protected Sub RegisterClientScript()
            Dim sJavaScript As String

            sJavaScript = "<SCRIPT language='JavaScript' src='../Navigation/Scripts/AvailableSelected.js'>" & Environment.NewLine
            sJavaScript &= "</SCRIPT>" & Environment.NewLine
            MyBase.Page.RegisterStartupScript("AvailableSelectedScript", sJavaScript)
        End Sub
#End Region

    End Class
End Namespace
