Namespace Generic
    Partial  Class SrcDstListChooser
        Inherits System.Web.UI.UserControl
        
#Region "Constants"
        Protected Const EXCEPTION_TEXT As String = "SrcDstListChooser can not access Data Source -- "
#End Region

#Region "Attributes"

        Public Enum EventArg
            ADD
            ADD_ALL
            REMOVE
            REMOVE_ALL
        End Enum

#End Region

#Region "Properties"

        Public Property Title() As String
            Get
                Return moTitle.Text
            End Get
            Set(Value As String)
                moTitle.Text = Value
            End Set
        End Property

        Public ReadOnly Property AvailableList() As ArrayList
            Get
                Dim oAvailableArray As ArrayList = New ArrayList()
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
                Dim oSelectedArray As ArrayList = New ArrayList()
                Dim oItem As ListItem
                oSelectedArray.Clear()
                For Each oItem In moSelectedList.Items
                    oSelectedArray.Add(oItem.Value)
                Next
                Return oSelectedArray
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

#End Region

#Region "Handlers"
        Public Event SelectedListChanged(aSrc As SrcDstListChooser, aReason As EventArg)

       

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here

        End Sub

        Private Sub BtnAdd_Click(sender As System.Object, e As System.EventArgs) Handles BtnAdd.Click
            BtnAdd_Action()
            UpdateAddEnable()
            RemoveSelection(moSelectedList)
        End Sub

        Private Sub BtnAddAll_Click(sender As System.Object, e As System.EventArgs) Handles BtnAddAll.Click
            BtnAddAll_Action()
            UpdateAddEnable()
            RemoveSelection(moSelectedList)
        End Sub

        Private Sub BtnRemove_Click(sender As System.Object, e As System.EventArgs) Handles BtnRemove.Click
            BtnRemove_Action()
            UpdateRemoveEnable()
            RemoveSelection(moAvailableList)
        End Sub

        Private Sub BtnRemoveAll_Click(sender As System.Object, e As System.EventArgs) Handles BtnRemoveAll.Click
            BtnRemoveAll_Action()
            UpdateRemoveEnable()
            RemoveSelection(moAvailableList)
        End Sub

        Private Sub moAvailableList_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles moAvailableList.SelectedIndexChanged
            BtnAdd.Enabled = True
            BtnRemove.Enabled = False
        End Sub

        Private Sub moSelectedList_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles moSelectedList.SelectedIndexChanged
            BtnRemove.Enabled = True
            BtnAdd.Enabled = False
        End Sub
#End Region

#Region "Button Management"

        Private Sub RemoveSelection(aListBox As ListBox)
            Dim oItem As ListItem
            For Each oItem In aListBox.Items
                oItem.Selected = False
            Next
        End Sub

        Private Sub RemoveFromList(BoxToRemove As ListBox, BoxThatContains As ListBox)
            Dim oItem As ListItem
            For Each oItem In BoxThatContains.Items
                BoxToRemove.Items.Remove(oItem)
            Next
        End Sub

        Private Sub AddToListBox(BoxToAdd As ListBox, BoxThatContains As ListBox)
            Dim oItem As ListItem
            For Each oItem In BoxThatContains.Items
                If oItem.Selected = True Then
                    BoxToAdd.Items.Add(oItem)
                End If
            Next
        End Sub

        Private Sub AddAllList(BoxToAdd As ListBox, BoxThatContains As ListBox)
            Dim oItem As ListItem
            For Each oItem In BoxThatContains.Items
                BoxToAdd.Items.Add(oItem)
            Next
        End Sub
        Public Sub BtnAdd_Action()
            AddToListBox(moSelectedList, moAvailableList)
            Sort(moSelectedList)
            RemoveFromList(moAvailableList, moSelectedList)
            RaiseEvent SelectedListChanged(Me, EventArg.ADD)
        End Sub

        Private Sub BtnAddAll_Action()
            AddAllList(moSelectedList, moAvailableList)
            Sort(moSelectedList)
            RemoveFromList(moAvailableList, moSelectedList)
            RaiseEvent SelectedListChanged(Me, EventArg.ADD_ALL)
        End Sub

        Private Sub BtnRemove_Action()
            AddToListBox(moAvailableList, moSelectedList)
            Sort(moAvailableList)
            RemoveFromList(moSelectedList, moAvailableList)
            RaiseEvent SelectedListChanged(Me, EventArg.REMOVE)
        End Sub

        Private Sub BtnRemoveAll_Action()
            AddAllList(moAvailableList, moSelectedList)
            Sort(moAvailableList)
            RemoveFromList(moSelectedList, moAvailableList)
            RaiseEvent SelectedListChanged(Me, EventArg.REMOVE_ALL)
        End Sub

        Private Sub UpdateAddEnable()
            BtnAdd.Enabled = False
            BtnRemoveAll.Enabled = True
            If moAvailableList.Items.Count = 0 Then
                BtnAddAll.Enabled = False
            End If
        End Sub

        Private Sub UpdateRemoveEnable()
            BtnRemove.Enabled = False
            BtnAddAll.Enabled = True
            If moSelectedList.Items.Count = 0 Then
                BtnRemoveAll.Enabled = False
            End If
        End Sub

#End Region

#Region "Set List Data"


        Private Sub SetData(aListBox As ListBox, aData As Object, aText As String, aValue As String)
            Try
                With aListBox
                    .DataSource = aData
                    .DataTextField = aText
                    .DataValueField = aValue
                    .DataBind()
                End With

            Catch ex As Exception
                '    Throw New Exception(EXCEPTION_TEXT & ex.Message)
                Throw New DataNotFoundException(EXCEPTION_TEXT & ex.Message)
            End Try
        End Sub

        Public Sub SetAvailableData(aData As Object, aText As String, aValue As String)
            SetData(moAvailableList, aData, aText, aValue)
            BtnAdd.Enabled = False
            BtnAddAll.Enabled = True
            BtnRemove.Enabled = False
            BtnRemoveAll.Enabled = False
        End Sub

        Public Sub SetSelectedData(aData As Object, aText As String, aValue As String)
            SetData(moSelectedList, aData, aText, aValue)
        End Sub

        Private Sub ClearData(aList As ListBox)
            Dim nCount As Integer = aList.Items.Count
            Dim nIndex As Integer

            For nIndex = 0 To nCount - 1
                aList.Items.RemoveAt(0)
            Next

        End Sub

        Public Sub ClearAvailableData()
            ClearData(moAvailableList)
        End Sub

        Public Sub ClearSelectedData()
            ClearData(moSelectedList)
        End Sub

#End Region

#Region "Sort"
        Private Class TheComparer
            Implements System.Collections.IComparer

            Function Compare(x As Object, y As Object) As Integer _
                  Implements System.Collections.IComparer.Compare
                Dim oXItem As ListItem = CType(x, ListItem)
                Dim oYItem As ListItem = CType(y, ListItem)

                Return New CaseInsensitiveComparer().Compare(oXItem.Text, oYItem.Text)

            End Function
        End Class

        Private Sub Sort(aList As ListBox)
            Dim oArray As ArrayList = New ArrayList()
            Dim oItem As ListItem
            Dim oComparer As IComparer = New TheComparer()

            For Each oItem In aList.Items
                oArray.Add(oItem)
            Next
            oArray.Sort(oComparer)
            ClearData(aList)

            For Each oItem In oArray
                aList.Items.Add(oItem)
            Next

        End Sub

#End Region
#Region "Manual Test"
        Public Sub SelectAvailableItems(indices As ArrayList)
            Dim oItem As Integer
            For Each oItem In indices
                If moAvailableList.Items.Count > oItem Then
                    moAvailableList.Items(oItem).Selected = True
                End If
            Next
        End Sub
#End Region

    End Class
End Namespace
