Imports SysWebUICtls = System.Web.UI.WebControls


Namespace Security

    Partial Class MaintainFormInclusionForm
        Inherits ElitaPlusPage

#Region "Constants"

        Private Const MAINTAIN_FORM_EXCLUSION_FORM001 As String = "MAINTAIN_FORM_EXCLUSION_FORM001" ' Form Exception
        Private Const MAINTAIN_FORM_EXCLUSION_FORM002 As String = "MAINTAIN_FORM_EXCLUSION_FORM002" ' Update Exception

        Private Const PATH_TO_ICONS As String = "/Navigation/images/icons/treevw_"

        'Permission Type
        Private Const PERMISSION_TYPE_INVISIBLE As String = "N"
        Private Const PERMISSION_TYPE_VIEWONLY As String = "V"
        Private Const PERMISSION_TYPE_EDITABLE As String = "E"
#End Region


#Region "Properties"

        Private _dvTabFormList As DataView

        Private ReadOnly Property TabFormList() As DataView
            Get
                If _dvTabFormList Is Nothing Then
                    _dvTabFormList = RoleAuthFormInclusion.GetTabFormList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                End If
                Return _dvTabFormList
            End Get
        End Property

#End Region



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

#Region "Tree control population"
        Private Sub populateTabs()
            Dim strTemp As String, i As Integer, strLastTab As String = ""
            Dim drv As DataRowView
            For i = 0 To TabFormList.Count - 1
                drv = TabFormList.Item(i)
                strTemp = drv("TAB_NAME").ToString
                If strLastTab <> strTemp Then
                    strLastTab = strTemp
                    Dim newNode As SysWebUICtls.TreeNode = New SysWebUICtls.TreeNode(strLastTab, drv("TAB_CODE").ToString,
                                            Request.ApplicationPath & PATH_TO_ICONS & drv("TAB_ICON_IMG").ToString)
                    newNode.PopulateOnDemand = True
                    newNode.Expanded = False

                    tvFormList.Nodes.Add(newNode)
                End If
            Next
        End Sub

        Private Sub populateFormNotes(tabNode As SysWebUICtls.TreeNode)
            TabFormList.RowFilter = "TAB_CODE='" & tabNode.Value & "'"
            Dim strTemp As String, i As Integer
            Dim drv As DataRowView
            For i = 0 To TabFormList.Count - 1
                drv = TabFormList.Item(i)
                Dim newNode As SysWebUICtls.TreeNode = New SysWebUICtls.TreeNode(drv("FORM_NAME").ToString,
                                                        drv("FORM_CODE").ToString & "|" & GuidControl.ByteArrayToGuid(drv("FORM_ID")).ToString,
                                                        Request.ApplicationPath & PATH_TO_ICONS & drv("TAB_ICON_IMG").ToString)
                newNode.PopulateOnDemand = True
                newNode.Expanded = False
                tabNode.ChildNodes.Add(newNode)
            Next
            TabFormList.RowFilter = ""
        End Sub

        Private Sub populateRolePermission(formNode As SysWebUICtls.TreeNode)
            Dim strFormID As String = formNode.Value.Split("|"c)(1)
            Dim dv As DataView = RoleAuthFormInclusion.GetPermissionByFormID(New Guid(strFormID))

            Dim strFormPerm As String
            Dim drv As DataRowView, i As Integer
            Dim roleNode As SysWebUICtls.TreeNode, optNode As SysWebUICtls.TreeNode

            For i = 0 To dv.Count - 1
                drv = dv.Item(i)
                strFormPerm = drv("PERMISSION_TYPE").ToString
                roleNode = New SysWebUICtls.TreeNode(drv("ROLE_DESC").ToString, GuidControl.ByteArrayToGuid(drv("ROLE_ID")).ToString & "|" & strFormPerm & "|" & drv("ExistingRec").ToString)
                roleNode.Expanded = True

                optNode = New SysWebUICtls.TreeNode("View Only", PERMISSION_TYPE_VIEWONLY)
                optNode.ShowCheckBox = True
                If strFormPerm = PERMISSION_TYPE_VIEWONLY Then optNode.Checked = True
                roleNode.ChildNodes.Add(optNode)

                optNode = New SysWebUICtls.TreeNode("Editable", PERMISSION_TYPE_EDITABLE)
                optNode.ShowCheckBox = True
                If strFormPerm = PERMISSION_TYPE_EDITABLE Then optNode.Checked = True
                roleNode.ChildNodes.Add(optNode)

                formNode.ChildNodes.Add(roleNode)
            Next
        End Sub

        Private Sub tvFormList_TreeNodePopulate(sender As Object, e As System.Web.UI.WebControls.TreeNodeEventArgs) Handles tvFormList.TreeNodePopulate
            Try
                Select Case e.Node.Depth
                    Case 0
                        populateFormNotes(e.Node)
                    Case 1
                        populateRolePermission(e.Node)
                End Select
            Catch ex As Exception
                ErrControllerMaster.AddErrorAndShow(MAINTAIN_FORM_EXCLUSION_FORM001)
            End Try
        End Sub
#End Region

#Region "Handlers"
        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            If Not Page.IsPostBack Then
                populateTabs()
            End If
        End Sub

        Private Sub BtnSave_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles BtnSave_WRITE.Click
            Try
                ProcessChanges()
            Catch ex As Exception
                ErrControllerMaster.AddErrorAndShow(MAINTAIN_FORM_EXCLUSION_FORM002)
            End Try
        End Sub

        Private Sub BtnReset_Click(sender As System.Object, e As System.EventArgs) Handles BtnReset.Click
            tvFormList.Nodes.Clear()
            populateTabs()
        End Sub

#End Region


#Region "Business Part"
        Private Sub ProcessChanges()
            Dim strFormID As String, strPermission As String
            Dim oForm As RoleAuthFormInclusion
            Dim changedNotes As New SysWebUICtls.TreeNodeCollection, blnChanged As Boolean

            For Each nodeTab As SysWebUICtls.TreeNode In tvFormList.Nodes
                For Each nodeForm As SysWebUICtls.TreeNode In nodeTab.ChildNodes
                    strFormID = nodeForm.Value.Split("|"c)(1)
                    blnChanged = False
                    For Each nodeRole As SysWebUICtls.TreeNode In nodeForm.ChildNodes
                        Dim strRoleConfig() As String = nodeRole.Value.Split("|"c)
                        strPermission = "N"
                        For Each nodePerm As SysWebUICtls.TreeNode In nodeRole.ChildNodes
                            If nodePerm.Checked Then
                                Select Case nodePerm.Value
                                    Case "V"
                                        If strPermission <> "E" Then strPermission = nodePerm.Value
                                    Case "E"
                                        strPermission = nodePerm.Value
                                End Select
                            End If
                        Next
                        If strPermission = "N" Then
                            If strRoleConfig(2) = "Y" Then 'existing included form, delete
                                oForm = New RoleAuthFormInclusion(New Guid(strFormID), New Guid(strRoleConfig(0)))
                                oForm.Delete()
                                oForm.Save()
                                blnChanged = True
                            End If
                        Else
                            If strRoleConfig(2) = "N" Then 'New row, add
                                oForm = New RoleAuthFormInclusion
                                With oForm
                                    .FormId = New Guid(strFormID)
                                    .RoleId = New Guid(strRoleConfig(0))
                                    .PermissionType = strPermission
                                    .Save()
                                    blnChanged = True
                                End With
                            Else 'existiting row, update if changed
                                If strPermission <> strRoleConfig(1) Then
                                    oForm = New RoleAuthFormInclusion(New Guid(strFormID), New Guid(strRoleConfig(0)))
                                    With oForm
                                        .PermissionType = strPermission
                                        .Save()
                                    End With
                                    blnChanged = True
                                End If
                            End If
                        End If

                    Next
                    If blnChanged Then 'reload form configure
                        changedNotes.Add(New SysWebUICtls.TreeNode(nodeForm.Text, nodeForm.Value))
                    End If
                Next
            Next
            ReloadControlPermissions(changedNotes)
        End Sub

        Sub ReloadControlPermissions(changedNodes As SysWebUICtls.TreeNodeCollection)
            Dim strFormID As String, strRoleID As String
            For Each tabNode As SysWebUICtls.TreeNode In tvFormList.Nodes
                For Each formNode As SysWebUICtls.TreeNode In tabNode.ChildNodes
                    If IsFormPermissionChanged(changedNodes, formNode) Then
                        formNode.ChildNodes.Clear()
                        populateRolePermission(formNode)
                    End If
                Next
            Next
        End Sub

        Function IsFormPermissionChanged(changedNodes As SysWebUICtls.TreeNodeCollection, formNode As SysWebUICtls.TreeNode) As Boolean
            Dim blnChanged As Boolean = False
            For Each objNode As TreeNode In changedNodes
                If objNode.Value = formNode.Value Then
                    blnChanged = True
                    Exit For
                End If
            Next
            Return blnChanged
        End Function
#End Region



    End Class

End Namespace

