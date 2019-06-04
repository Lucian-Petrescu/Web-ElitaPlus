Imports SysWebUICtls = System.Web.UI.WebControls


Namespace Security

    Partial Class CompanyFormExclusions
        '  Inherits System.Web.UI.Page
        Inherits ElitaPlusPage

#Region "Constants"

        Private Const MAINTAIN_FORM_EXCLUSION_FORM001 As String = "MAINTAIN_FORM_EXCLUSION_FORM001" ' Form Exception
        Private Const MAINTAIN_FORM_EXCLUSION_FORM002 As String = "MAINTAIN_FORM_EXCLUSION_FORM002" ' Update Exception

        'Form Title
        Public Const PAGETITLE As String = "COMPANY_FORM_EXCLUSIONS"
        Public Const PAGETAB As String = "ADMIN"

        Private Const PATH_TO_ICONS As String = "/Navigation/images/icons/treevw_"
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
            Dim TabList As DataView = RoleAuthTabsExclusion.GetTabList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
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

        Private Sub populateCompanyPermission(ByVal formNode As SysWebUICtls.TreeNode)
            Dim dv As DataView = CompanyFormExclusion.GetCompanyPermissionByFormID(New Guid(formNode.Value))

            Dim strTabExcluded As String
            Dim drv As DataRowView, i As Integer
            Dim companyNode As TreeNode, optNode As TreeNode

            For i = 0 To dv.Count - 1
                drv = dv.Item(i)
                strTabExcluded = drv("Excluded").ToString
                companyNode = New SysWebUICtls.TreeNode(drv("COMPANY_CODE").ToString, GuidControl.ByteArrayToGuid(drv("company_id")).ToString & "|" & strTabExcluded)
                companyNode.ShowCheckBox = True
                If strTabExcluded = "N" Then companyNode.Checked = True
                formNode.ChildNodes.Add(companyNode)
            Next
        End Sub

        Private Sub populateForms(ByVal tabNode As SysWebUICtls.TreeNode)
            Dim dv As DataView = CompanyFormExclusion.GetFormListByTabID(New Guid(tabNode.Value), ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            'tabNode.ChildNodes.Add(New TreeNode(
            Dim drv As DataRowView, i As Integer

            For i = 0 To dv.Count - 1
                drv = dv.Item(i)
                Dim newNode As TreeNode = New TreeNode(drv("FORM_NAME").ToString, GuidControl.ByteArrayToGuid(drv("FORM_ID")).ToString, tabNode.ImageUrl)
                newNode.PopulateOnDemand = True
                newNode.Expanded = False
                tabNode.ChildNodes.Add(newNode)
            Next
        End Sub

        Private Sub tvFormList_TreeNodePopulate(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.TreeNodeEventArgs) Handles tvFormList.TreeNodePopulate
            Try
                Select Case e.Node.Depth
                    Case 0
                        populateForms(e.Node)
                    Case 1
                        populateCompanyPermission(e.Node)
                End Select
            Catch ex As Exception
                ErrControllerMaster.AddErrorAndShow(MAINTAIN_FORM_EXCLUSION_FORM001)
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
                ErrControllerMaster.AddErrorAndShow(MAINTAIN_FORM_EXCLUSION_FORM002)
            End Try
        End Sub

        Private Sub BtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReset.Click
            tvFormList.Nodes.Clear()
            populateTabs()
        End Sub

#End Region


#Region "Business Part"
        Private Sub ProcessChanges()
            Dim strFormID As String, strExcluded As String, strCompanyID As String
            Dim oForm As CompanyFormExclusion
            Dim strCompConfig() As String
            Dim changedNotes As New SysWebUICtls.TreeNodeCollection, blnChanged As Boolean

            For Each nodeTab As SysWebUICtls.TreeNode In tvFormList.Nodes
                For Each nodeForm As TreeNode In nodeTab.ChildNodes
                    blnChanged = False
                    For Each nodeComp As TreeNode In nodeForm.ChildNodes
                        strCompConfig = nodeComp.Value.Split("|"c)
                        If nodeComp.Checked Then
                            strExcluded = "N"
                        Else
                            strExcluded = "Y"
                        End If

                        If strExcluded = "Y" Then
                            If strCompConfig(1) = "N" Then 'add new 
                                oForm = New CompanyFormExclusion
                                With oForm
                                    .FormId = New Guid(nodeForm.Value)
                                    .CompanyId = New Guid(strCompConfig(0))
                                    .Save()
                                End With
                                blnChanged = True
                            End If
                        Else
                            If strCompConfig(1) = "Y" Then 'delete
                                oForm = New CompanyFormExclusion(New Guid(nodeForm.Value), New Guid(strCompConfig(0)))
                                oForm.Delete()
                                oForm.Save()
                                blnChanged = True
                            End If
                        End If
                    Next
                    If blnChanged Then 'reload form configure
                        changedNotes.Add(New TreeNode(nodeForm.Text, nodeForm.Value))
                    End If
                Next
            Next
            ReloadCompanyPermissions(changedNotes)
        End Sub

        Sub ReloadCompanyPermissions(ByVal changedNodes As SysWebUICtls.TreeNodeCollection)
            For Each tabNode As SysWebUICtls.TreeNode In tvFormList.Nodes
                For Each formNode As TreeNode In tabNode.ChildNodes
                    If IsFormPermissionChanged(changedNodes, formNode) Then
                        formNode.ChildNodes.Clear()
                        populateCompanyPermission(formNode)
                    End If
                Next
            Next
        End Sub

        Function IsFormPermissionChanged(ByVal changedNodes As SysWebUICtls.TreeNodeCollection, ByVal formNode As SysWebUICtls.TreeNode) As Boolean
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

