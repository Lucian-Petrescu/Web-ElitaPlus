Imports System.Diagnostics
Imports Microsoft.Web.UI.WebControls
Imports Microsoft.VisualBasic

Namespace Generic

    Partial  Class TreeController
        Inherits System.Web.UI.UserControl


#Region "Constants"

        Protected Const EXCEPTION_TEXT As String = "TreeController can not access Data Source -- "

        Private Const ID_COLUMN_ORDINAL0 As Integer = 0
        Private Const ID_COLUMN_ORDINAL1 As Integer = 1

        Private Const PATH_TO_ICONS As String = "/Navigation/images/icons/"
#End Region

#Region "Types"

        Public Class TreeLevel
            Public name As String
            Public idPos As Integer
            Public descPos As Integer
            Public iconPos As Integer
        End Class

        Public Class TreeControllerEventArgs
            Inherits EventArgs

#Region "TreeControllerEventArgs Types"
            Public Enum EventEnum
                LEAF
                SELECTED_INDEX_CHANGED
            End Enum
#End Region
#Region "TreeControllerEventArgs Attributes"
            Private moNode As TreeNode
            Private moRow As DataRow
            Private moEventEnum As EventEnum
#End Region

#Region "TreeControllerEventArgs Properties"
            Public ReadOnly Property Node() As TreeNode
                Get
                    Return moNode
                End Get

            End Property

            Public ReadOnly Property Row() As DataRow
                Get
                    Return moRow
                End Get

            End Property

            Public ReadOnly Property TheEventEnum() As EventEnum
                Get
                    Return moEventEnum
                End Get

            End Property
#End Region



            Public Sub New(oLeaf As TreeNode, oRow As DataRow, oEventEnum As EventEnum)
                moNode = oLeaf
                moRow = oRow
                moEventEnum = oEventEnum
            End Sub

            Public Sub New(oNode As TreeNode, oEventEnum As EventEnum)
                moNode = oNode
                moEventEnum = oEventEnum
            End Sub
        End Class


        Public Class ChildEnumerator
            Implements IEnumerator

#Region "ChildEnumerator Attributes"
            Private mnLevelNumber As Integer
            Private moCurrentNode As TreeNode
            Private moChildTree As TreeView
            Private moNodeEnum As IEnumerator
            Private moEnumStack As ArrayList
#End Region

#Region "ChildEnumerator Properties"
            Public Overridable Overloads ReadOnly Property Current() As Object _
                Implements IEnumerator.Current
                Get
                    Return moCurrentNode
                End Get

            End Property

            Public Property LevelNumber() As Integer
                Get
                    Return mnLevelNumber
                End Get
                Set(Value As Integer)
                    mnLevelNumber = Value
                End Set
            End Property

#End Region

            Public Sub New(oTree As TreeView)
                moChildTree = oTree
            End Sub

            Private Function GoUp() As Boolean
                Dim bContinue As Boolean = True
                While (moNodeEnum.MoveNext = False) AndAlso bContinue
                    If moEnumStack.Count > 0 Then
                        ' Go up one level
                        moNodeEnum = CType(moEnumStack(moEnumStack.Count - 1), IEnumerator)
                        moEnumStack.RemoveAt(moEnumStack.Count - 1)
                    Else
                        bContinue = False
                    End If
                End While

                Return bContinue
            End Function

            Public Function GetNextChild() As Boolean
                moCurrentNode = CType(moNodeEnum.Current, TreeNode)
                If moEnumStack.Count < LevelNumber() Then
                    If moCurrentNode.Nodes.Count > 0 Then
                        '     Look Down for the correct level
                        moEnumStack.Add(moNodeEnum)
                        moNodeEnum = moCurrentNode.Nodes.GetEnumerator
                        moNodeEnum.Reset()
                    End If
                    If GoUp() = False Then Return False
                    Return GetNextChild()
                End If
                Return True

            End Function

            Public Overridable Overloads Function MoveNext() As Boolean _
                    Implements IEnumerator.MoveNext
                If moCurrentNode Is Nothing Then
                    moEnumStack = New ArrayList()
                    If moChildTree.Nodes.Count = 0 Then Return False
                    moNodeEnum = moChildTree.Nodes.GetEnumerator
                    moNodeEnum.Reset()
                End If
                If GoUp() = False Then Return False
                Return GetNextChild()
                Return True
            End Function

            Public Overridable Overloads Sub Reset() _
                    Implements IEnumerator.Reset
                moCurrentNode = Nothing
            End Sub


        End Class
#End Region

#Region "Attributes"

        Private moTreeLevels As ArrayList
        Private moDataView As DataView
        Private moRowEnum As IEnumerator
        Private moChildEnum As ChildEnumerator
        Private moExpanded As Boolean
        Private moFontWeight As String = ""

#End Region

#Region "Properties"

        Public Property IsAutoPostBack() As Boolean
            Get
                Return moTree.AutoPostBack
            End Get
            Set(Value As Boolean)
                moTree.AutoPostBack = Value
            End Set
        End Property

        Public Property SelectedNodeIndex() As String
            Get
                Return moTree.SelectedNodeIndex
            End Get
            Set(Value As String)
                moTree.SelectedNodeIndex = Value
            End Set
        End Property

        Public Property Expanded() As Boolean
            Get
                Return moExpanded
            End Get
            Set(Value As Boolean)
                moExpanded = Value
            End Set
        End Property

        Public Property FontWeight() As String
            Get
                Return moFontWeight
            End Get
            Set(Value As String)
                moFontWeight = Value
            End Set
        End Property

#End Region

#Region "Handlers"

        Public Event LeafReached(sender As TreeController, e As TreeControllerEventArgs)
        Public Event SelectedIndexChanged(sender As TreeController, e As TreeControllerEventArgs)


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
        End Sub

        Private Sub moTree_SelectedIndexChange(sender As System.Object, _
                    e As TreeViewSelectEventArgs) Handles moTree.SelectedIndexChange

            Dim oTreeView As TreeView = CType(sender, TreeView)
            Dim sIndex As String = oTreeView.SelectedNodeIndex

            If sIndex <> e.NewNode Then Return
            Dim oNode As TreeNode = moTree.GetNodeFromIndex(e.NewNode)

            RaiseEvent SelectedIndexChanged(Me, New TreeControllerEventArgs(oNode, TreeControllerEventArgs.EventEnum.SELECTED_INDEX_CHANGED))

        End Sub

#End Region

#Region "Creates a Tree Based on DataView"
        'Public Sub ExpandAll()
        '    moTree.ExpandLevel = moTreeLevels.Count
        'End Sub

        Public Sub AddData(oTreeLevel As TreeLevel)
            If moTreeLevels Is Nothing Then
                moTreeLevels = New ArrayList
            End If
            If oTreeLevel IsNot Nothing Then
                moTreeLevels.Add(oTreeLevel)
            End If
        End Sub

        Private Function DifferentAncestor(sPrevAncestor As String, nLevelIndex As Integer) As Boolean
            Dim bDifferent As Boolean = True
            Dim sCurrAncestor As String = ""
            Dim oRow As DataRow
            Dim nIndex As Integer
            Dim oLevel As TreeLevel
            Dim sId As String

            oRow = CType(moRowEnum.Current, DataRow)
            For nIndex = 0 To nLevelIndex - 1
                oLevel = CType(moTreeLevels(nIndex), TreeLevel)
                If (oRow.Item(oLevel.idPos)).GetType Is GetType(Byte()) Then
                    sId = GuidControl.ByteArrayToGuid(oRow.Item(oLevel.idPos)).ToString
                Else
                    sId = oRow.Item(oLevel.idPos).ToString
                End If
                'sCurrAncestor &= "." & oRow.Item(oLevel.idPos).ToString()
                sCurrAncestor &= "." & sId
            Next
            If sPrevAncestor = sCurrAncestor Then bDifferent = False

            Return bDifferent
        End Function

        Private Function CreateLevel(oParentNodes As TreeNodeCollection, nLevelIndex As Integer, sAncestor As String) As Boolean
            Dim oLevel As TreeLevel
            Dim oNode As TreeNode
            Dim oRow As DataRow
            Dim bContinue As Boolean = True
            Dim bBreak As Boolean = False
            Dim sId, sIcon As String

            oLevel = CType(moTreeLevels(nLevelIndex), TreeLevel)
            Do
                oRow = CType(moRowEnum.Current, DataRow)

                ' This Level
                oNode = New TreeNode
                oNode.Text = oRow.Item(oLevel.descPos).ToString
                ' id
                If (oRow(oLevel.idPos)).GetType Is GetType(Byte()) Then
                    sId = (New Guid(CType(oRow(oLevel.idPos), Byte()))).ToString
                Else
                    sId = oRow.Item(oLevel.idPos).ToString
                End If
                oNode.NodeData = sId

                If oLevel.iconPos > -1 Then
                    sIcon = oRow.Item(oLevel.iconPos).ToString
                    oNode.ImageUrl = Request.ApplicationPath & PATH_TO_ICONS & "treevw_" & sIcon
                End If
                If FontWeight.Trim().Length > 0 Then
                    oNode.DefaultStyle.Item("font-weight") = FontWeight()
                End If
                oParentNodes.Add(oNode)

                'If Expanded Then
                '    If moTree.ExpandLevel < NodeLevel(oNode) Then
                '        moTree.ExpandLevel = NodeLevel(oNode)
                '    End If
                'End If

                If Expanded Then
                    If (NodeLevel(oNode) = 1) Then
                        moTree.ExpandLevel = NodeLevel(oNode)
                    End If
                End If

                If (nLevelIndex + 1) = moTreeLevels.Count Then
                    ' Finished all Levels for this Node
                    RaiseEvent LeafReached(Me, New TreeControllerEventArgs(oNode, oRow, TreeControllerEventArgs.EventEnum.LEAF))
                    ' Next Row
                    If (moRowEnum.MoveNext = False) Then
                        bContinue = False
                    Else
                        ' Finished this Level
                        If DifferentAncestor(sAncestor, nLevelIndex) Then bBreak = True
                    End If
                Else
                    ' Next Level
                    Dim oNextLevel As TreeLevel
                    Dim oNextParentNode As TreeNode

                    oNextLevel = CType(moTreeLevels(nLevelIndex + 1), TreeLevel)
                    oNextParentNode = oNode
                    bContinue = CreateLevel(oNextParentNode.Nodes, nLevelIndex + 1, sAncestor & "." & sId)
                    ' Finished this Level
                    If bContinue Then If DifferentAncestor(sAncestor, nLevelIndex) Then bBreak = True

                End If


            Loop While Not bBreak AndAlso bContinue
            Return bContinue

        End Function

        ' DataView Format: ID, NAME
        'TABS_FORMS: Tab_ID, Tab_NAME,
        '               FORM_ID,  FORM_NAME,
        '                   TAB_ICON, FORM_CODE

        ' FORMS_PERMISSIONS: Tab_ID, Tab_NAME,
        '                       FORM_ID,  FORM_NAME,
        '                            PERMISSION, TAB_ICON_IMG
        Private Sub BindData(oNodes As TreeNodeCollection, oDataView As DataView)
            Try
                Dim nLevelIndex As Integer = 0
                Dim oLevel As TreeLevel

                moDataView = oDataView
                moRowEnum = moDataView.Table.Rows.GetEnumerator()
                moRowEnum.Reset()

                If (moTreeLevels.Count = nLevelIndex) Then Return
                If (moRowEnum.MoveNext = False) Then Return
                oLevel = CType(moTreeLevels(nLevelIndex), TreeLevel)

                ' Root
                CreateLevel(oNodes, nLevelIndex, "")
            Catch ex As Exception
                Throw New DataNotFoundException(EXCEPTION_TEXT & ex.Message)
            End Try
        End Sub

        Public Sub BindData(oDataView As DataView)
            BindData(moTree.Nodes, oDataView)
        End Sub

        Public Sub BindSubLevel(oParent As TreeNode, oDataView As DataView)
            BindData(oParent.Nodes, oDataView)
        End Sub


        Public Sub ClearTreeLevels()
            moTreeLevels = Nothing
        End Sub

        Public Sub ClearData()
            moTreeLevels = Nothing
            moTree.Nodes.Clear()
        End Sub

        ' The Level is the number of dots 
        Public Function NodeLevel(oNode As TreeNode) As Integer
            Dim sNodePath As String = oNode.GetNodeIndex
            Dim oArray As String() = Split(sNodePath, ".")
            Return oArray.Length - 1
        End Function

        Public Function ObtainNodeFromIndex(oIndex As String) As TreeNode
            Return moTree.GetNodeFromIndex(oIndex)
        End Function

        Public Function GetSelectedNode() As TreeNode
            Return ObtainNodeFromIndex(moTree.SelectedNodeIndex)
        End Function

        Public Sub DeleteSelectedNode()
            Dim oNode As TreeNode

            oNode = GetSelectedNode()
            If oNode.Nodes.Count = 0 Then
                oNode.Remove()
            End If


        End Sub

#End Region

#Region "Check Enumerator"
        ' Obtains the nodes that have a CheckBox property
        ' Based on a Level or all the leaves

        Public Function GetChildEnumerator() As ChildEnumerator
            If moChildEnum Is Nothing Then
                moChildEnum = New ChildEnumerator(moTree)
            End If

            Return moChildEnum
        End Function



#End Region


    End Class

End Namespace
