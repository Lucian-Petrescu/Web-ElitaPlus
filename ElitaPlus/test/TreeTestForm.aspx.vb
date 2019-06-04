' $log$
' $Id: TreeTestForm.aspx.vb, 129283+1 2014/04/14 18:05:02 co0799 $    

Imports Assurant.ElitaPlus.BusinessObjects.Common
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Generic
Imports Microsoft.Web.UI.WebControls

Partial Class TreeTestForm
    Inherits System.Web.UI.Page
    '  Inherits ElitaPlusPage    

#Region "Constants"

    ' Table that populates TAB  tree
    Private Const NONICON As Integer = -1
    Private Const TABID As Integer = 0
    Private Const TABNAME As Integer = 1
    Private Const TABICON As Integer = 2

#End Region

#Region "Attributes"

    '   Private moControlExclusions As ControlExclusions
    '  Private moApplicationUser As ApplicationUser

#End Region

#Region "Properties"

    Private ReadOnly Property Tree() As TreeController
        Get
            If moTree Is Nothing Then
                moTree = CType(FindControl("moTree"), TreeController)
            End If
            Return moTree
        End Get
    End Property

#End Region

#Region "Handlers"

    ' User Controls
    Protected WithEvents moTree As TreeController

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
            PopulateTree()
        End If
    End Sub

    Private Sub OnSelectedIndex_Changed(ByVal oTreeController As TreeController, _
                        ByVal e As TreeController.TreeControllerEventArgs) _
                        Handles moTree.SelectedIndexChanged
        Dim oNode As TreeNode = e.Node
        Dim nLevel As Integer = Tree.NodeLevel(oNode)
        'If (nLevel = FORMS_TREE_LEVEL) AndAlso (oNode.Nodes.Count = 0) Then
        '    PopulateControls(oNode)
        'End If

    End Sub

    Private Sub BtnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAdd.Click
        PopulateControl2(Tree.GetSelectedNode)
    End Sub

    Private Sub BtnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        Dim oId As Guid
        Dim oNode As TreeNode

        oNode = Tree.GetSelectedNode
        oId = New Guid(oNode.NodeData)
    End Sub

    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        Tree.DeleteSelectedNode()
    End Sub

#End Region

#Region "Populate"

    Private Function GetDataView(ByVal oId As Guid, ByVal sName As String, Optional ByVal sIcon As String = "") As DataView
        Dim oDataView As DataView
        Dim oTable As New DataTable("Tabs")
        Dim oColumn As DataColumn
        Dim oRow As DataRow

        ' Columns
        ' Soft Questions
        oColumn = New DataColumn("TAB_ID", GetType(Guid))
        oTable.Columns.Add(oColumn)
        oColumn = New DataColumn("TAB_NAME")
        oTable.Columns.Add(oColumn)
        oColumn = New DataColumn("TAB_ICON")
        oTable.Columns.Add(oColumn)

        ' Rows
        oRow = oTable.NewRow
        oRow(TABID) = oId.ToByteArray
        oRow(TABNAME) = sName
        oRow(TABICON) = sIcon

        oTable.Rows.Add(oRow)
        oDataView = New DataView(oTable)
        Return oDataView
    End Function

    Private Sub AddTreeLevel(ByVal sName As String, ByVal nIdPos As Integer, ByVal nDescPos As Integer, ByVal nIconPos As Integer)
        Dim oTreeLevel As New TreeController.TreeLevel

        With oTreeLevel
            .name = sName
            .idPos = nIdPos
            .descPos = nDescPos
            .iconPos = nIconPos

        End With
        Tree.AddData(oTreeLevel)
    End Sub

    Private Sub PopulateTab()
        Dim oDataView As DataView


        Try
            oDataView = GetDataView(Guid.NewGuid, "SoftQuestions", "home_icon.gif")
            Tree.BindData(oDataView)
            '   Tree.IsAutoPostBack = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PopulateForm1()
        Dim oDataView As DataView
        Dim oTreeParent As TreeNode

        '  Tree.ClearTreeLevels()
        ' AddTreeLevel("Forms", TABID, TABNAME, TABICON)
        Try
            oDataView = GetDataView(Guid.NewGuid, "FormA", "interfaces_icon.gif")

            oTreeParent = Tree.ObtainNodeFromIndex("0")
            Tree.BindSubLevel(oTreeParent, oDataView)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PopulateForm2()
        Dim oDataView As DataView
        Dim oTreeParent As TreeNode

        Try
            oDataView = GetDataView(Guid.NewGuid, "FormB", "interfaces_icon.gif")
            oTreeParent = Tree.ObtainNodeFromIndex("0")
            Tree.BindSubLevel(oTreeParent, oDataView)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PopulateControl1()
        Dim oDataView As DataView
        Dim oTreeParent As TreeNode


        Try
            oDataView = GetDataView(Guid.NewGuid, "ControlA")

            oTreeParent = Tree.ObtainNodeFromIndex("0.1")   'FormB
            Tree.BindSubLevel(oTreeParent, oDataView)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PopulateControl2(ByVal oTreeParent As TreeNode)
        Dim oDataView As DataView
        '  Dim oTreeParent As TreeNode

        Tree.ClearTreeLevels()
        AddTreeLevel("Controls", TABID, TABNAME, -1)
        Try
            oDataView = GetDataView(Guid.NewGuid, "ControlB")

            '   oTreeParent = Tree.ObtainNodeFromIndex("0.1")   'FormB
            Tree.BindSubLevel(oTreeParent, oDataView)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub PopulateTree()
        AddTreeLevel("Tabs", TABID, TABNAME, TABICON)
        PopulateTab()
        PopulateForm1()
        PopulateForm2()
        Tree.ClearTreeLevels()
        AddTreeLevel("Controls", TABID, TABNAME, NONICON)
        PopulateControl1()
        ' PopulateControl2()
    End Sub
#End Region


End Class
