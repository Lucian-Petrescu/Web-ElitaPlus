Imports Assurant.ElitaPlus.ElitaPlusWebApp.Generic
Imports SysWebUICtls = System.Web.UI.WebControls

Namespace Security


    Partial Class MaintainControlExclusionForm
        ' Inherits System.Web.UI.Page
        Inherits ElitaPlusPage

#Region "Constants"

        Private Const MAINTAIN_CONTROL_EXCLUSION_FORM002 As String = "MAINTAIN_CONTROL_EXCLUSION_FORM002" ' Control Exception
        Private Const MAINTAIN_CONTROL_EXCLUSION_FORM003 As String = "MAINTAIN_CONTROL_EXCLUSION_FORM003" ' Update Exception

        'Permission Type
        Private Const PERMISSION_TYPE_INVISIBLE As String = "I"
        Private Const PERMISSION_TYPE_VIEWONLY As String = "V"
        Private Const PERMISSION_TYPE_EDITABLE As String = "E"

        'Existing Rec
        Private Const EXISTING_RECORD_YES As String = "1"
        Private Const EXISTING_RECORD_NO As String = "0"

        'Form Title
        Public Const PAGETITLE As String = "MAINTAIN CONTROL EXCLUSIONS"
        Public Const PAGETAB As String = "ADMIN"

        Private Const PATH_TO_ICONS As String = "/Navigation/images/icons/treevw_"

        'Result table columns
        Private Const COL_ACTION As String = "Action"
        Private Const COL_FORMID As String = "FormID"
        Private Const COL_CTLNAME As String = "CtlName"
        Private Const COL_ROLEID As String = "RoleID"
        Private Const COL_PERMTYPE As String = "PermType"
#End Region

#Region "Properties"
        Private _dvTabFormList As DataView
        Private _dtChanges As DataTable

        Private ReadOnly Property TabFormList() As DataView
            Get
                If _dvTabFormList Is Nothing Then
                    _dvTabFormList = RoleAuthCtrlExclusion.PopulateList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                End If
                Return _dvTabFormList
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

#Region "Handlers"
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Me.ErrControllerMaster.Clear_Hide()
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
                ErrControllerMaster.AddErrorAndShow(MAINTAIN_CONTROL_EXCLUSION_FORM003)
            End Try
        End Sub

        Private Sub BtnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReset.Click
            tvFormList.Nodes.Clear()
            populateTabs()
        End Sub

#End Region

#Region "TreeView"
        Private Sub populateFormNotes(ByVal tabNote As SysWebUICtls.TreeNode)
            TabFormList.RowFilter = "TAB_CODE='" & tabNote.Value & "'"
            Dim strTemp As String, i As Integer
            Dim drv As DataRowView
            For i = 0 To TabFormList.Count - 1
                drv = TabFormList.Item(i)
                Dim newNode As SysWebUICtls.TreeNode = New SysWebUICtls.TreeNode(drv("FORM_NAME").ToString,
                                                        drv("FORM_CODE").ToString & "|" & GuidControl.ByteArrayToGuid(drv("FORM_ID")).ToString,
                                                        Request.ApplicationPath & PATH_TO_ICONS & drv("TAB_ICON_IMG").ToString)
                newNode.PopulateOnDemand = True
                newNode.Expanded = False
                tabNote.ChildNodes.Add(newNode)
            Next
            TabFormList.RowFilter = ""
        End Sub

        Private Sub populateControlNotes(ByVal formNode As SysWebUICtls.TreeNode)
            Dim strFormCode As String() = formNode.Value.Split("|"c)
            Dim lsCtlName As Collections.Generic.List(Of String) = AssemblyInformation.GetControlsByFormName(strFormCode(0))
            If Not lsCtlName Is Nothing Then
                For Each ctl As String In lsCtlName
                    Dim newNode As SysWebUICtls.TreeNode = New SysWebUICtls.TreeNode(ctl, strFormCode(0))
                    newNode.PopulateOnDemand = True
                    newNode.Expanded = False
                    formNode.ChildNodes.Add(newNode)
                Next
            End If
        End Sub

        Private Sub populateRolePermissions(ByVal ctlNote As SysWebUICtls.TreeNode)
            Dim dv As DataView = RoleAuthCtrlExclusion.GetControlPermissionList(ctlNote.Value, ctlNote.Text)
            Dim strFormPerm As String, strCtlPerm As String, i As Integer
            Dim drv As DataRowView
            Dim roleNode As SysWebUICtls.TreeNode, optNode As SysWebUICtls.TreeNode

            For i = 0 To dv.Count - 1
                drv = dv.Item(i)
                strFormPerm = drv("FORM_PERMISSION").ToString
                strCtlPerm = drv("CTL_PERMISSION").ToString
                roleNode = New SysWebUICtls.TreeNode(drv("ROLE_NAME").ToString, GuidControl.ByteArrayToGuid(drv("ROLE_ID")).ToString & "|" & strFormPerm & "|" & strCtlPerm & "|" & drv("ExistingCtrlEx").ToString)
                roleNode.Expanded = True

                optNode = New SysWebUICtls.TreeNode("Invisible", PERMISSION_TYPE_INVISIBLE)
                If strFormPerm <> PERMISSION_TYPE_INVISIBLE Then
                    optNode.ShowCheckBox = True
                    If strCtlPerm = PERMISSION_TYPE_INVISIBLE Then optNode.Checked = True
                End If
                roleNode.ChildNodes.Add(optNode)

                If strFormPerm <> PERMISSION_TYPE_INVISIBLE Then
                    optNode = New SysWebUICtls.TreeNode("View Only", PERMISSION_TYPE_VIEWONLY)
                    optNode.ShowCheckBox = True
                    If strCtlPerm = PERMISSION_TYPE_VIEWONLY Then optNode.Checked = True
                    roleNode.ChildNodes.Add(optNode)
                End If

                If strFormPerm <> PERMISSION_TYPE_INVISIBLE Then
                    optNode = New SysWebUICtls.TreeNode("Editable", PERMISSION_TYPE_EDITABLE)
                    optNode.ShowCheckBox = True
                    If strCtlPerm = PERMISSION_TYPE_EDITABLE Then optNode.Checked = True
                    roleNode.ChildNodes.Add(optNode)
                End If

                ctlNote.ChildNodes.Add(roleNode)
            Next
        End Sub

        Private Sub tvFormList_TreeNodePopulate(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.TreeNodeEventArgs) Handles tvFormList.TreeNodePopulate
            Try
                Select Case e.Node.Depth
                    Case 0
                        populateFormNotes(e.Node)
                    Case 1
                        populateControlNotes(e.Node)
                    Case 2
                        populateRolePermissions(e.Node)
                End Select
            Catch ex As Exception
                ErrControllerMaster.AddErrorAndShow(MAINTAIN_CONTROL_EXCLUSION_FORM002)
            End Try
        End Sub

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

                    Me.tvFormList.Nodes.Add(newNode)
                End If
            Next
        End Sub
#End Region

#Region "Business Part"
        Sub ReloadControlPermissions(ByVal changedNotes As SysWebUICtls.TreeNodeCollection)
            For Each objNote As SysWebUICtls.TreeNode In changedNotes
                For Each tabNote As SysWebUICtls.TreeNode In tvFormList.Nodes
                    For Each formNote As SysWebUICtls.TreeNode In tabNote.ChildNodes
                        For Each ctlNote As SysWebUICtls.TreeNode In formNote.ChildNodes
                            If objNote.Text = ctlNote.Text AndAlso objNote.Value = ctlNote.Value Then
                                ctlNote.ChildNodes.Clear()
                                populateRolePermissions(ctlNote)
                            End If
                        Next
                    Next
                Next
            Next
        End Sub

        Private Sub AddChanges(ByVal strAction As String, ByVal strFormID As String,
                               ByVal strCtlName As String, ByVal strRoleID As String, ByVal strPermType As String)
            If _dtChanges Is Nothing Then
                _dtChanges = New DataTable
                _dtChanges.Columns.Add(COL_ACTION, GetType(String))
                _dtChanges.Columns.Add(COL_FORMID, GetType(String))
                _dtChanges.Columns.Add(COL_CTLNAME, GetType(String))
                _dtChanges.Columns.Add(COL_ROLEID, GetType(String))
                _dtChanges.Columns.Add(COL_PERMTYPE, GetType(String))
            End If

            Dim strFilter As String = String.Format("FormID='{0}' and CtlName='{1}' and RoleID='{2}'", strFormID, strCtlName, strRoleID)
            If _dtChanges.Select(strFilter).Count = 0 Then
                Dim dr As DataRow = _dtChanges.NewRow
                dr(COL_ACTION) = strAction
                dr(COL_FORMID) = strFormID
                dr(COL_CTLNAME) = strCtlName
                dr(COL_ROLEID) = strRoleID
                dr(COL_PERMTYPE) = strPermType
                _dtChanges.Rows.Add(dr)
            End If
        End Sub

        Private Sub SaveTheChanges()
            Dim strAction As String, strTemp As String
            Dim objCtlEx As RoleAuthCtrlExclusion
            If Not _dtChanges Is Nothing AndAlso _dtChanges.Rows.Count > 0 Then
                For Each dr As DataRow In _dtChanges.Rows
                    strAction = dr(COL_ACTION).ToString
                    Select Case strAction
                        Case "A"
                            objCtlEx = New RoleAuthCtrlExclusion
                            With objCtlEx
                                .FormId = New Guid(dr(COL_FORMID).ToString)
                                .RoleId = New Guid(dr(COL_ROLEID).ToString)
                                .ControlName = dr(COL_CTLNAME).ToString
                                .PermissionType = dr(COL_PERMTYPE).ToString
                            End With
                        Case "D"
                            objCtlEx = New RoleAuthCtrlExclusion(New Guid(dr(COL_FORMID).ToString), New Guid(dr(COL_ROLEID).ToString), dr(COL_CTLNAME).ToString)
                            objCtlEx.Delete()
                        Case "U"
                            objCtlEx = New RoleAuthCtrlExclusion(New Guid(dr(COL_FORMID).ToString), New Guid(dr(COL_ROLEID).ToString), dr(COL_CTLNAME).ToString)
                            objCtlEx.PermissionType = dr(COL_PERMTYPE).ToString
                    End Select
                    objCtlEx.Save()
                Next
            End If
            _dtChanges = Nothing
        End Sub

        Private Sub ProcessChanges()
            Dim chkNotes As SysWebUICtls.TreeNodeCollection = tvFormList.CheckedNodes
            Dim strCtlName As String, strFormID As String, strRoleID As String
            Dim strTemp As String, strList As String(), strPermType As String, strChangeType As String
            Dim strFormPerm As String, strCtlPerm As String, strExistingRec As String
            Dim changedNotes As New SysWebUICtls.TreeNodeCollection

            If Not chkNotes Is Nothing Then
                For Each objNote As SysWebUICtls.TreeNode In chkNotes
                    strPermType = objNote.Value
                    strList = objNote.Parent.Value.Split("|"c)
                    strRoleID = strList(0)
                    strFormPerm = strList(1)
                    strCtlPerm = strList(2)
                    strExistingRec = strList(3)
                    If strPermType <> strCtlPerm Then 'there is changes
                        If strPermType <> PERMISSION_TYPE_EDITABLE Then
                            For Each objPT As SysWebUICtls.TreeNode In objNote.Parent.ChildNodes
                                If objPT.Checked Then
                                    If objPT.Value = PERMISSION_TYPE_EDITABLE Then
                                        strPermType = PERMISSION_TYPE_EDITABLE
                                    ElseIf objPT.Value = PERMISSION_TYPE_VIEWONLY AndAlso strPermType = PERMISSION_TYPE_INVISIBLE Then
                                        strPermType = PERMISSION_TYPE_VIEWONLY
                                    End If
                                End If
                            Next
                        End If
                        If strPermType <> strCtlPerm Then 'real change, save it
                            strChangeType = String.Empty
                            If strExistingRec = EXISTING_RECORD_YES Then 'existing record in control exclusion table
                                If strPermType = strFormPerm Then
                                    'control permission same as form permission, delete the record
                                    strChangeType = "D"
                                Else
                                    'update the record
                                    strChangeType = "U"
                                End If
                            Else
                                If strPermType <> strFormPerm Then 'new control exclusion record
                                    'add the record
                                    strChangeType = "A"
                                End If
                            End If
                            If strChangeType <> String.Empty Then
                                strFormID = objNote.Parent.Parent.Parent.Value.Split("|"c)(1)
                                strCtlName = objNote.Parent.Parent.Text
                                AddChanges(strChangeType, strFormID, strCtlName, strRoleID, strPermType)
                                changedNotes.Add(New SysWebUICtls.TreeNode(objNote.Parent.Parent.Text, objNote.Parent.Parent.Value))
                            End If
                        End If
                    End If
                Next
                SaveTheChanges()
                ReloadControlPermissions(changedNotes)
            End If
        End Sub

#End Region

    End Class

End Namespace


