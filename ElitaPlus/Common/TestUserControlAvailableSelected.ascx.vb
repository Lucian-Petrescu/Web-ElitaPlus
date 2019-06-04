Namespace Generic
    Partial Class TestUserControlAvailableSelected
        Inherits System.Web.UI.UserControl

#Region "Constants"
        Protected Const EXCEPTION_TEXT As String = "SrcDstListChooser can not access Data Source -- "
#End Region

#Region "Events"
        Public Event SelectedListChanged(ByVal aSrc As TestUserControlAvailableSelected, ByVal aReason As EventArg)
        Public Event Attach(ByVal aSrc As TestUserControlAvailableSelected, ByVal attachedList As ArrayList)
        Public Event Detach(ByVal aSrc As TestUserControlAvailableSelected, ByVal detachedList As ArrayList)

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

        Public Property AvailableDesc() As String
            Get

            End Get
            Set(ByVal Value As String)
                Me.moAvailableTitle.Text = Value
            End Set
        End Property
        Public Property SelectedDesc() As String
            Get

            End Get
            Set(ByVal Value As String)
                Me.moSelectedTitle.Text = Value
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

        Public Property BackColor() As String
            Get
                Return (Me.moOutTable.BgColor)
            End Get
            Set(ByVal Value As String)
                Me.moOutTable.BgColor = Value
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
                Return Me.moSelectedList
            End Get

        End Property

#End Region

#Region "Handlers"




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

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here

        End Sub

        Private Sub BtnAdd_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAdd_WRITE.Click
            BtnAdd_Action()
            Me.EnableDisableButtons()
            RemoveSelection(moSelectedList)
        End Sub

        Private Sub BtnAddAll_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAddAll_WRITE.Click
            BtnAddAll_Action()
            Me.EnableDisableButtons()
            RemoveSelection(moSelectedList)
        End Sub

        Private Sub BtnRemove_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRemove_WRITE.Click
            BtnRemove_Action()
            Me.EnableDisableButtons()
            RemoveSelection(moAvailableList)
        End Sub

        Private Sub BtnRemoveAll_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRemoveAll_WRITE.Click
            BtnRemoveAll_Action()
            Me.EnableDisableButtons()
            RemoveSelection(moAvailableList)
        End Sub

#End Region

#Region "Button Management"

        Private Sub RemoveSelection(ByVal aListBox As ListBox)
            Dim oItem As ListItem
            For Each oItem In aListBox.Items
                oItem.Selected = False
            Next
        End Sub

        Private Sub RemoveFromList(ByVal BoxToRemove As ListBox, ByVal BoxThatContains As ListBox)
            Dim oItem As ListItem
            For Each oItem In BoxThatContains.Items
                BoxToRemove.Items.Remove(oItem)
            Next
        End Sub

        Private Sub AddToListBox(ByVal BoxToAdd As ListBox, ByVal BoxThatContains As ListBox)
            Dim oItem As ListItem
            For Each oItem In BoxThatContains.Items
                If oItem.Selected = True Then
                    BoxToAdd.Items.Add(oItem)
                End If
            Next
        End Sub

        Private Sub AddAllList(ByVal BoxToAdd As ListBox, ByVal BoxThatContains As ListBox)
            Dim oItem As ListItem
            For Each oItem In BoxThatContains.Items
                BoxToAdd.Items.Add(oItem)
            Next
        End Sub
        Public Sub BtnAdd_Action()
            Dim selectedItemValues As ArrayList = Me.GetSelectedValueListCollection(Me.moAvailableList)
            AddToListBox(moSelectedList, moAvailableList)
            Sort(moSelectedList)
            'SelectedListListBox = moSelectedList
            RemoveFromList(moAvailableList, moSelectedList)
            RaiseEvent Attach(Me, selectedItemValues)
            RaiseEvent SelectedListChanged(Me, EventArg.ADD)
        End Sub

        Private Sub BtnAddAll_Action()
            Dim itemValues As ArrayList = Me.GetAllValueListCollection(Me.moAvailableList)
            AddAllList(moSelectedList, moAvailableList)
            Sort(moSelectedList)
            'SelectedListListBox = moSelectedList
            RemoveFromList(moAvailableList, moSelectedList)
            RaiseEvent Attach(Me, itemValues)
            RaiseEvent SelectedListChanged(Me, EventArg.ADD_ALL)
        End Sub

        Private Sub BtnRemove_Action()
            Dim selectedItemValues As ArrayList = Me.GetSelectedValueListCollection(Me.moSelectedList)
            AddToListBox(moAvailableList, moSelectedList)
            Sort(moAvailableList)
            RemoveFromList(moSelectedList, moAvailableList)
            RaiseEvent Detach(Me, selectedItemValues)
            RaiseEvent SelectedListChanged(Me, EventArg.REMOVE)
        End Sub

        Private Sub BtnRemoveAll_Action()
            Dim itemValues As ArrayList = Me.GetAllValueListCollection(Me.moSelectedList)
            AddAllList(moAvailableList, moSelectedList)
            Sort(moAvailableList)
            RemoveFromList(moSelectedList, moAvailableList)
            RaiseEvent Detach(Me, itemValues)
            RaiseEvent SelectedListChanged(Me, EventArg.REMOVE_ALL)
        End Sub

        Private Sub EnableDisableButtons()
            ControlMgr.SetEnableControl(Page, moAvailableList, True)
            ControlMgr.SetEnableControl(Page, moSelectedList, True)
            ControlMgr.SetEnableControl(Page, BtnAdd_WRITE, True)
            ControlMgr.SetEnableControl(Page, BtnAddAll_WRITE, True)
            ControlMgr.SetEnableControl(Page, BtnRemove_WRITE, True)
            ControlMgr.SetEnableControl(Page, BtnRemoveAll_WRITE, True)
            If moAvailableList.Items.Count = 0 Then
                ControlMgr.SetEnableControl(Page, BtnAdd_WRITE, False)
                ControlMgr.SetEnableControl(Page, BtnAddAll_WRITE, False)
            End If
            If Me.moSelectedList.Items.Count = 0 Then
                ControlMgr.SetEnableControl(Page, BtnRemove_WRITE, False)
                ControlMgr.SetEnableControl(Page, BtnRemoveAll_WRITE, False)
            End If
        End Sub


        Private Function GetSelectedValueListCollection(ByVal listCtr As ListControl) As ArrayList
            Dim item As ListItem
            Dim result As New ArrayList
            For Each item In listCtr.Items
                If item.Selected Then
                    result.Add(item.Value)
                End If
            Next
            Return result
        End Function

        Private Function GetAllValueListCollection(ByVal listCtr As ListControl) As ArrayList
            Dim item As ListItem
            Dim result As New ArrayList
            For Each item In listCtr.Items
                result.Add(item.Value)
            Next
            Return result
        End Function

#End Region

#Region "Set List Data"


        Public Sub SetAvailableData(ByVal dv As DataView, ByVal textColumnName As String, ByVal guidValueColumnName As String)
            ElitaPlusPage.BindListControlToDataView(moAvailableList, dv, textColumnName, guidValueColumnName, False)
            Me.EnableDisableButtons()
        End Sub

        Public Sub SetSelectedData(ByVal dv As DataView, ByVal textColumnName As String, ByVal guidValueColumnName As String)
            ElitaPlusPage.BindListControlToDataView(moSelectedList, dv, textColumnName, guidValueColumnName, False)
            Me.EnableDisableButtons()
        End Sub


#End Region

#Region "Sort"
        Private Class TheComparer
            Implements System.Collections.IComparer

            Function Compare(ByVal x As Object, ByVal y As Object) As Integer _
                  Implements System.Collections.IComparer.Compare
                Dim oXItem As ListItem = CType(x, ListItem)
                Dim oYItem As ListItem = CType(y, ListItem)

                Return New CaseInsensitiveComparer().Compare(oXItem.Text, oYItem.Text)

            End Function
        End Class

        Private Sub Sort(ByVal aList As ListBox)
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






    End Class
End Namespace
