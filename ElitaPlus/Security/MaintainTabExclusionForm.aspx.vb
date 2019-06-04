Imports SysWebUICtls = System.Web.UI.WebControls

Namespace Security

    Partial Class MaintainTabExclusionForm
        ' Inherits System.Web.UI.Page
        Inherits ElitaPlusPage

#Region "Constants"


        Private Const MAINTAIN_TAB_EXCLUSION_FORM001 As String = "MAINTAIN_TAB_EXCLUSION_FORM001" ' Tab Exception
        Private Const MAINTAIN_TAB_EXCLUSION_FORM002 As String = "MAINTAIN_TAB_EXCLUSION_FORM002" ' Update Exception

        'Form Title
        Public Const PAGETITLE As String = "MAINTAIN TAB EXCLUSIONS"
        Public Const PAGETAB As String = "ADMIN"

        Private Const PATH_TO_ICONS As String = "/Navigation/images/icons/treevw_"

#End Region

#Region "Properties"
        'Private baseUrl As String
        Private _dvTabList As DataView

        Private ReadOnly Property TabList() As DataView
            Get
                If _dvTabList Is Nothing Then
                    _dvTabList = RoleAuthTabsExclusion.GetTabList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                End If
                Return _dvTabList
            End Get
        End Property
#End Region

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

#Region "Treeview related functions"
        Private Sub populateTabs()
            Dim i As Integer
            Dim drv As DataRowView
            For i = 0 To TabList.Count - 1
                drv = TabList.Item(i)
                Dim newNode As SysWebUICtls.TreeNode = New SysWebUICtls.TreeNode(drv("TAB_NAME").ToString, GuidControl.ByteArrayToGuid(drv("TAB_ID")).ToString,
                                            Request.ApplicationPath & PATH_TO_ICONS & drv("TAB_ICON_IMG").ToString)
                newNode.PopulateOnDemand = True
                newNode.Expanded = False
                Me.tvFormList.Nodes.Add(newNode)
            Next
        End Sub

        Private Sub populateRolePermission(ByVal tabNode As SysWebUICtls.TreeNode)
            Dim dv As DataView = RoleAuthTabsExclusion.GetPermissionsByTabID(New Guid(tabNode.Value))

            Dim strTabExcluded As String
            Dim drv As DataRowView, i As Integer
            Dim roleNode As SysWebUICtls.TreeNode, optNode As SysWebUICtls.TreeNode

            For i = 0 To dv.Count - 1
                drv = dv.Item(i)
                strTabExcluded = drv("Excluded").ToString
                roleNode = New SysWebUICtls.TreeNode(drv("ROLE_DESC").ToString, GuidControl.ByteArrayToGuid(drv("ROLE_ID")).ToString & "|" & strTabExcluded)
                roleNode.ShowCheckBox = True
                If strTabExcluded = "N" Then roleNode.Checked = True
                tabNode.ChildNodes.Add(roleNode)
            Next
        End Sub

        Private Sub tvFormList_TreeNodePopulate(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.TreeNodeEventArgs) Handles tvFormList.TreeNodePopulate
            Try
                Select Case e.Node.Depth
                    Case 0
                        populateRolePermission(e.Node)
                End Select
            Catch ex As Exception
                ErrControllerMaster.AddErrorAndShow(MAINTAIN_TAB_EXCLUSION_FORM001)
            End Try
        End Sub
#End Region

#Region "Handlers"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            'Put user code to initialize the page here
            Me.ErrControllerMaster.Clear_Hide()
            'Put user code to initialize the page here
            If Not Page.IsPostBack Then
                Me.SetFormTab(Me.PAGETAB)
                Me.SetFormTitle(Me.PAGETITLE)
                populateTabs()
            End If
        End Sub

        Private Sub BtnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave_WRITE.Click
            Try
                ProcessChanges()
            Catch ex As Exception
                ErrControllerMaster.AddErrorAndShow(MAINTAIN_TAB_EXCLUSION_FORM002)
            End Try
        End Sub

        Private Sub BtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReset.Click
            tvFormList.Nodes.Clear()
            populateTabs()
        End Sub

#End Region

#Region "Business Part"
        Private Sub ProcessChanges()
            Dim strTabID As String, strExcluded As String
            Dim oTab As RoleAuthTabsExclusion
            Dim changedNotes As New SysWebUICtls.TreeNodeCollection, blnChanged As Boolean

            For Each nodeTab As SysWebUICtls.TreeNode In tvFormList.Nodes
                strTabID = nodeTab.Value
                blnChanged = False
                For Each nodeRole As SysWebUICtls.TreeNode In nodeTab.ChildNodes
                    Dim strRoleConfig() As String = nodeRole.Value.Split("|"c)
                    If nodeRole.Checked Then
                        strExcluded = "N"
                    Else
                        strExcluded = "Y"
                    End If

                    If strExcluded = "Y" Then
                        If strRoleConfig(1) = "N" Then 'add new 
                            oTab = New RoleAuthTabsExclusion
                            With oTab
                                .TabId = New Guid(strTabID)
                                .RoleId = New Guid(strRoleConfig(0))
                                .Save()
                            End With
                            blnChanged = True
                        End If
                    Else
                        If strRoleConfig(1) = "Y" Then 'delete
                            oTab = New RoleAuthTabsExclusion(New Guid(strTabID), New Guid(strRoleConfig(0)))
                            oTab.Delete()
                            oTab.Save()
                            blnChanged = True
                        End If
                    End If
                    If blnChanged Then 'reload form configure
                        changedNotes.Add(New SysWebUICtls.TreeNode(nodeTab.Text, nodeTab.Value))
                    End If
                Next
            Next
            ReloadControlPermissions(changedNotes)
        End Sub

        Sub ReloadControlPermissions(ByVal changedNodes As SysWebUICtls.TreeNodeCollection)
            For Each tabNode As SysWebUICtls.TreeNode In tvFormList.Nodes
                If IsFormPermissionChanged(changedNodes, tabNode) Then
                    tabNode.ChildNodes.Clear()
                    populateRolePermission(tabNode)
                End If
            Next
        End Sub

        Function IsFormPermissionChanged(ByVal changedNodes As SysWebUICtls.TreeNodeCollection, ByVal tabNode As SysWebUICtls.TreeNode) As Boolean
            Dim blnChanged As Boolean = False
            For Each objNode As TreeNode In changedNodes
                If objNode.Value = tabNode.Value Then
                    blnChanged = True
                    Exit For
                End If
            Next
            Return blnChanged
        End Function

#End Region
    End Class

End Namespace
