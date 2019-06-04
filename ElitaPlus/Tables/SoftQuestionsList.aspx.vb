Imports Assurant.ElitaPlus.ElitaPlusWebApp.Generic
Imports Microsoft.Web.UI.WebControls
Imports SysWebUICtls = System.Web.UI.WebControls
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Partial Class SoftQuestionsList
    Inherits ElitaPlusPage

#Region "Constants"
    Private Const ACTION_EDIT As String = "E"
    Private Const ACTION_NEW As String = "N"

    Public Const URL As String = "/ElitaPlus/Tables/SoftQuestionsList.aspx"

#End Region

#Region "Page Controls"
    Protected WithEvents ErrorCtrl As ErrorController
    Protected WithEvents NodeModifyPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents lbSoftQuestionGroup As System.Web.UI.WebControls.Label
    Protected WithEvents moCertificateInfoController As Certificates.UserControlCertificateInfo

#End Region

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub


    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object
    Private displaySoftQForm As String = "display:'none'"

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Properties"

    Public ReadOnly Property UserCertificateCtr() As Certificates.UserControlCertificateInfo
        Get
            If moCertificateInfoController Is Nothing Then
                moCertificateInfoController = CType(FindControl("moCertificateInfoController"), Certificates.UserControlCertificateInfo)
            End If
            Return moCertificateInfoController
        End Get
    End Property

    Public ReadOnly Property GetCompanyCode() As String
        Get
            Dim companyBO As Company = New Company(Me.State.CertificateCompanyID)

            Return companyBO.Code
        End Get

    End Property

#End Region

#Region "Page Parameters"
    Public Class Parameters
        Public RiskTypeId As Guid
        Public CertificateID As Guid
        Public CertificateCompanyId As Guid
        Public ShowAcceptButton As Boolean = True
        Public Sub New(ByVal RiskTypeId As Guid, ByVal CertificateID As Guid, ByVal CertificateCompanyId As Guid)
            Me.RiskTypeId = RiskTypeId
            Me.CertificateID = CertificateID
            Me.CertificateCompanyId = CertificateCompanyId
        End Sub
    End Class
#End Region

#Region "Page State"
    Class MyState
        Public softQuestionAction As String
        Public SoftQuestionBO As SoftQuestion
        Public SoftQuestionId As Guid
        Public RiskTypeID As Guid
        Public CertificateID As Guid
        Public CertificateCompanyID As Guid
        Public SelectedNodeValue As String
        Public OriginalState As Object
    End Class

    Public Sub New()
        'MyBase.New(New MyState)
        MyBase.New(True)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            If Not Me.NavController Is Nothing Then
                If Me.NavController.State Is Nothing Then
                    Me.NavController.State = New MyState
                    If Not Me.NavController.ParametersPassed Is Nothing Then
                        Dim pageParameters As Parameters = CType(Me.NavController.ParametersPassed, Parameters)
                        Me.State.RiskTypeID = pageParameters.RiskTypeId
                        Me.State.CertificateID = pageParameters.CertificateID
                        Me.State.CertificateCompanyID = pageParameters.CertificateCompanyId
                    Else
                        Me.State.RiskTypeID = Guid.Empty
                        Me.State.CertificateID = Guid.Empty
                        Me.State.CertificateCompanyID = Guid.Empty
                    End If
                End If
                Return CType(Me.NavController.State, MyState)
            Else
                Return CType(MyBase.State, MyState)
            End If
        End Get
    End Property

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                Dim pageParameters As Parameters = CType(Me.CallingParameters, Parameters)
                Me.State.RiskTypeID = pageParameters.RiskTypeId
                Me.State.CertificateID = pageParameters.CertificateID
            Else
                Me.State.RiskTypeID = Guid.Empty
                Me.State.CertificateID = Guid.Empty
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try

    End Sub

#End Region

#Region "PageEvents"

    Function isNavStateSet() As Boolean
        If Not Me.NavController Is Nothing Then
            If Not Me.NavController.State Is Nothing Then
                If NavController.State.GetType.FullName.Contains("SoftQuestionsList") = True Then
                    Return True
                End If
            End If
        End If
        Return False
    End Function

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Try
            ControlMgr.SetVisibleControl(Me, moCertificateInfoController, False)
            ControlMgr.SetVisibleControl(Me, CertRow, False)
            ErrorCtrl.Hide()
            ControlMgr.SetVisibleControl(Me, ErrorRow, False)

            btnNew_WRITE.Attributes.Add("onclick", "javascript:return setAction('E');")
            btnModify_WRITE.Attributes.Add("onclick", "javascript:return setAction('E');")

            Me.MenuEnabled = Not (isSoftQuestionFormVisible.Value = "E")

            If Not Page.IsPostBack Then 'fist time calling
                'AGL
                If Request.QueryString.Count > 0 Then
                    If isNavStateSet() = False Then
                        Dim localState As New MyState
                        localState.OriginalState = Me.NavController.State
                        Me.NavController.State = localState
                    End If
                    Dim pageParameters As Parameters = New Parameters(New Guid(Request.QueryString("RiskTypeID").ToString()), New Guid(Request.QueryString("CertificateID").ToString()), New Guid(Request.QueryString("CertificateCompanyID").ToString()))
                    Me.State.RiskTypeID = pageParameters.RiskTypeId
                    Me.State.CertificateID = pageParameters.CertificateID
                    Me.State.CertificateCompanyID = pageParameters.CertificateCompanyId
                End If
                Me.AddConfirmation(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT)
                Me.btnPanel.Enabled = False
                'Session("NodeID") = "0"
                PopulateTree()
            Else
                If NextAction.Value = "RefreshTree" Then
                    PopulateTree()
                    NextAction.Value = ""
                End If
            End If
            If Not Me.State.CertificateID.Equals(Guid.Empty) Then
                ControlMgr.SetVisibleControl(Me, CertRow, True)
                ControlMgr.SetVisibleControl(Me, moCertificateInfoController, True)
                moCertificateInfoController = Me.UserCertificateCtr
                moCertificateInfoController.InitController(Me.State.CertificateID, , GetCompanyCode)
            End If
            isSoftQuestionFormVisible.Value = ""
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub
    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
        Me.HiddenSaveChangesPromptResponse.Value = ""
    End Sub
#End Region

#Region "Button Handlers"

    Private Sub GetGroupNode(ByVal SelectedNode As SysWebUICtls.TreeNode, ByRef blnIsRoot As Boolean, ByRef GroupNode As SysWebUICtls.TreeNode)
        Dim rootNode As SysWebUICtls.TreeNode = tvQuestion.Nodes(0)
        If SelectedNode.Value = rootNode.Value Then
            blnIsRoot = True
        Else
            blnIsRoot = False
        End If
        If Not blnIsRoot Then
            Dim objNode As SysWebUICtls.TreeNode
            GroupNode = SelectedNode
            objNode = SelectedNode.Parent
            While objNode.Value <> rootNode.Value
                GroupNode = objNode
                objNode = objNode.Parent
            End While
        End If
    End Sub

    Private Sub LoadQuestionGroup()
        Dim availableGroup As DataView = SoftQuestion.getAvailableSoftQuestionGroup(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id) 'soft que
        cboSoftQuestionGroup.Items.Clear()
        ' BindListControlToDataView(cboSoftQuestionGroup, availableGroup, "DESCRIPTION", "SOFT_QUESTION_GROUP_ID", False)
        Dim listcontext As ListContext = New ListContext()
        listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
        Dim quesLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.SoftQuestionByCompanyGroup, Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
        cboSoftQuestionGroup.Populate(quesLkl, New PopulateOptions())
        ControlMgr.SetEnableControl(Me, cboSoftQuestionGroup, True)
    End Sub

    Private Function FindSelectedNode(ByVal objStart As SysWebUICtls.TreeNode, ByRef objSelectedNode As SysWebUICtls.TreeNode) As Boolean
        Dim objNode As SysWebUICtls.TreeNode, blnFound As Boolean = False
        If Not State.SoftQuestionBO Is Nothing Then
            For Each objNode In objStart.ChildNodes
                If objNode.Value = State.SelectedNodeValue Then
                    blnFound = True
                    objSelectedNode = objNode
                End If
                If Not blnFound Then blnFound = FindSelectedNode(objNode, objSelectedNode)
                If blnFound Then Exit For
            Next
        End If
        Return blnFound
    End Function
    Private Sub btnNew_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            Dim parentNode As SysWebUICtls.TreeNode = tvQuestion.SelectedNode
            Dim groupNode As SysWebUICtls.TreeNode, blnIsRoot As Boolean
            GetGroupNode(parentNode, blnIsRoot, groupNode)

            Dim groupid As Guid
            If blnIsRoot Then
                LoadQuestionGroup()
            Else
                groupid = New Guid(groupNode.Value.Split("|"c)(1))
                cboSoftQuestionGroup.Items.Clear()
                cboSoftQuestionGroup.Items.Add(New System.Web.UI.WebControls.ListItem(groupNode.Text, groupid.ToString()))
                ControlMgr.SetEnableControl(Me, cboSoftQuestionGroup, False)
            End If
            txtSoftQuestion.Text = ""

            Dim parentID As Guid = New Guid(parentNode.Value.Split("|"c)(0))
            State.SoftQuestionBO = New SoftQuestion()
            State.SoftQuestionId = State.SoftQuestionBO.Id
            If parentID <> Guid.Empty Then
                State.SoftQuestionBO.ParentId = parentID
            End If
            ControlMgr.SetEnableControl(Me, tvQuestion, False)
            Me.softQuestionTreePanel.Enabled = False
            Me.PanelSoftQEdit.Visible = True
            Me.btnPanel.Visible = False
            Me.MenuEnabled = False
            State.softQuestionAction = ACTION_NEW
            State.SelectedNodeValue = parentNode.Value
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnModify_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModify_WRITE.Click
        Try
            cboSoftQuestionGroup.Items.Clear()
            ControlMgr.SetEnableControl(Me, cboSoftQuestionGroup, False)
            ControlMgr.SetEnableControl(Me, tvQuestion, False)

            Dim objNode As SysWebUICtls.TreeNode = tvQuestion.SelectedNode
            Dim blnEditable As Boolean = True, strRootValue As String = tvQuestion.Nodes(0).Value
            If objNode.Value = strRootValue OrElse objNode.Parent.Value = strRootValue Then
                blnEditable = False
            End If
            If blnEditable Then
                Dim groupNode As SysWebUICtls.TreeNode, blnIsRoot As Boolean
                GetGroupNode(objNode, blnIsRoot, groupNode)
                Dim groupid As Guid
                If Not blnIsRoot Then
                    groupid = New Guid(groupNode.Value.Split("|"c)(1))
                    cboSoftQuestionGroup.Items.Add(New System.Web.UI.WebControls.ListItem(groupNode.Text, groupid.ToString()))
                End If
                State.SoftQuestionBO = New SoftQuestion(New Guid(objNode.Value.Split("|"c)(0)))
                State.SoftQuestionId = Me.State.SoftQuestionBO.Id
                txtSoftQuestion.Text = objNode.Text
                ControlMgr.SetEnableControl(Me, txtSoftQuestion, True)
                ControlMgr.SetEnableControl(Me, btnSave, True)
            Else
                txtSoftQuestion.Text = ""
                ControlMgr.SetEnableControl(Me, txtSoftQuestion, False)
                ControlMgr.SetEnableControl(Me, btnSave, False)
                Me.DisplayMessage(Message.MSG_CANNOTMODIFY_SOFTQUESTION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_ALERT)
            End If

            Me.softQuestionTreePanel.Enabled = False
            Me.PanelSoftQEdit.Visible = True
            Me.btnPanel.Visible = False
            State.softQuestionAction = ACTION_EDIT
            State.SelectedNodeValue = objNode.Value
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim objNode As SysWebUICtls.TreeNode
            Dim strSoftQuestion As String = txtSoftQuestion.Text.Trim
            If strSoftQuestion.Equals("") Then
                If State.SoftQuestionBO.ParentId <> Guid.Empty Then
                    strSoftQuestion = "Unknown Description"
                Else
                    strSoftQuestion = cboSoftQuestionGroup.SelectedItem.Text
                End If

            End If
            State.SoftQuestionBO.Description = strSoftQuestion

            If State.softQuestionAction = ACTION_EDIT Then
                State.SoftQuestionBO.Save()
                FindSelectedNode(tvQuestion.Nodes(0), objNode)
                If Not objNode Is Nothing Then
                    objNode.Text = strSoftQuestion
                    objNode.Selected = True
                End If
            ElseIf State.softQuestionAction = ACTION_NEW Then
                Me.PopulateBOProperty(State.SoftQuestionBO, "SoftQuestionGroupId", cboSoftQuestionGroup)
                Dim childOrder As Long = 0
                Dim guidParent As Guid = New Guid(State.SelectedNodeValue.Split("|"c)(0))
                If guidParent <> Guid.Empty Then childOrder = SoftQuestion.getMaxChildOrder(guidParent) + 1
                State.SoftQuestionBO.ChildOrder = childOrder
                State.SoftQuestionBO.Save()
                'add the node to treeview
                If State.SoftQuestionBO.ParentId = Guid.Empty Then
                    objNode = tvQuestion.Nodes(0)
                Else
                    FindSelectedNode(tvQuestion.Nodes(0), objNode)
                End If
                Dim obj As SysWebUICtls.TreeNode = New SysWebUICtls.TreeNode(strSoftQuestion, State.SoftQuestionBO.Id.ToString & "|" & State.SoftQuestionBO.SoftQuestionGroupId.ToString & "|0")
                obj.SelectAction = TreeNodeSelectAction.Select
                obj.Selected = True
                objNode.ChildNodes.Add(obj)
                objNode.Selected = False
                objNode.Expanded = True
                Dim strParentValue() As String = objNode.Value.Split("|"c)
                If strParentValue.Count = 3 Then
                    Dim intChildCnt As Integer = 0
                    If Integer.TryParse(strParentValue(2), intChildCnt) Then
                        intChildCnt += 1
                        objNode.Value = strParentValue(0) & "|" & strParentValue(1) & "|" & intChildCnt
                    End If
                End If
            End If
            Me.PanelSoftQEdit.Visible = False
            Me.btnPanel.Visible = True
            Me.btnPanel.Enabled = True
            Me.softQuestionTreePanel.Enabled = True
            ControlMgr.SetEnableControl(Me, tvQuestion, True)

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            Me.PanelSoftQEdit.Visible = False
            Me.btnPanel.Visible = True
            Me.btnPanel.Enabled = True
            Me.softQuestionTreePanel.Enabled = True
            ControlMgr.SetEnableControl(Me, tvQuestion, True)
            ControlMgr.SetEnableControl(Me, txtSoftQuestion, True)
            ControlMgr.SetEnableControl(Me, btnSave, True)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnDelete_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click
        Try
            Dim objNode As SysWebUICtls.TreeNode = tvQuestion.SelectedNode
            If objNode.Value <> tvQuestion.Nodes(0).Value Then
                Dim intChildCnt As Integer, strTemp As String = objNode.Value.Split("|"c)(2)
                If Not Integer.TryParse(strTemp, intChildCnt) Then intChildCnt = -1
                If intChildCnt = 0 Then
                    Dim objSQ As SoftQuestion = New SoftQuestion(New Guid(objNode.Value.Split("|"c)(0)))
                    objSQ.Delete()
                    objSQ.Save()
                    Dim parentNode As SysWebUICtls.TreeNode = objNode.Parent
                    parentNode.Selected = True
                    parentNode.ChildNodes.Remove(objNode)
                    'todo- reduce parent's child count
                    Dim strParentValue() As String = parentNode.Value.Split("|"c)
                    If strParentValue.Count = 3 Then
                        intChildCnt = 0
                        If Integer.TryParse(strParentValue(2), intChildCnt) Then
                            If intChildCnt > 0 Then intChildCnt -= 1
                            parentNode.Value = strParentValue(0) & "|" & strParentValue(1) & "|" & intChildCnt
                        End If
                    End If
                Else
                    DisplayMessage(Message.MSG_CANNOTDELETE_SOFTQUESTION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_ALERT)
                End If
            Else
                DisplayMessage(Message.MSG_CANNOTDELETE_SOFTQUESTION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_ALERT)
            End If
        Catch ex As Exception
            ControlMgr.SetVisibleControl(Me, ErrorRow, True)
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Try
            If Me.State.OriginalState Is Nothing Then
                If Not Me.NavController Is Nothing Then
                    Me.NavController.FlowSession(FlowSessionKeys.SESSION_SOFTQUESTION_CERT_ID) = Me.State.CertificateID
                    Me.NavController.FlowSession(FlowSessionKeys.SESSION_SOFTQUESTION_COMMENTADDED) = ""
                    Me.NavController.Navigate(Me, FlowEvents.EVENT_BACK, New StateControllerYesNoPrompt.Parameters(Message.MSG_PROMPT_FOR_SOFTQUESTION_COMMENT))
                Else
                    Me.ReturnToCallingPage()
                End If
            Else
                Me.NavController.State = Me.State.OriginalState
                Dim sJavaScript As String = "<script type=""text/javascript""> window.parent.document.getElementById('divSoftQuestions').style.display = 'none'; </script>"
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "CloseSoftQuestions", sJavaScript)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

#End Region

#Region "TreeView functions"
    Private Sub PopulateQuestionGroups()
        Dim rootNode As SysWebUICtls.TreeNode = New SysWebUICtls.TreeNode(TranslationBase.TranslateLabelOrMessage("Soft Questions"), Guid.Empty.ToString)

        Dim softQuestDV As SoftQuestion.SoftQuestionDV, blnFromCert As Boolean = False
        If Me.State.RiskTypeID.Equals(Guid.Empty) Then
            softQuestDV = SoftQuestion.getSoftQuestionGroups(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
        Else
            softQuestDV = SoftQuestion.getSoftQuestionGroupForRiskType(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, Me.State.RiskTypeID)
            blnFromCert = True
        End If

        Dim intChildCnt As Integer
        'Dim drv As DataRowView
        For Each drv As DataRowView In softQuestDV
            intChildCnt = 0
            Integer.TryParse(drv(SoftQuestion.SoftQuestionDV.COL_NAME_CHILD_COUNT).ToString, intChildCnt)
            Dim newNode As SysWebUICtls.TreeNode = New SysWebUICtls.TreeNode(drv(SoftQuestion.SoftQuestionDV.COL_NAME_DESCRIPTION).ToString, GuidControl.ByteArrayToGuid(drv(SoftQuestion.SoftQuestionDV.COL_NAME_SOFT_QUESTION_ID)).ToString & "|" & GuidControl.ByteArrayToGuid(drv(SoftQuestion.SoftQuestionDV.COL_NAME_SOFT_QUESTION_GROUP_ID)).ToString & "|" & intChildCnt)
            If intChildCnt > 0 Then
                With newNode
                    .PopulateOnDemand = True
                    .Expanded = False
                End With
            End If
            If blnFromCert Then
                newNode.SelectAction = TreeNodeSelectAction.None
                newNode.NavigateUrl = "javascript:void(0);"
            Else
                newNode.SelectAction = TreeNodeSelectAction.Select
            End If
            rootNode.ChildNodes.Add(newNode)
        Next

        If blnFromCert Then
            rootNode.SelectAction = TreeNodeSelectAction.None
        Else
            rootNode.SelectAction = TreeNodeSelectAction.Select
            rootNode.Selected = True
        End If
        Me.tvQuestion.Nodes.Add(rootNode)
    End Sub

    Private Sub tvQuestion_TreeNodePopulate(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.TreeNodeEventArgs) Handles tvQuestion.TreeNodePopulate
        Dim guidParent As Guid = New Guid(e.Node.Value.Split("|"c)(0))
        Dim softQuestDV As SoftQuestion.SoftQuestionDV = SoftQuestion.getChildren(guidParent)
        Dim intChildCnt As Integer, blnFromCert As Boolean = False
        If Not State.RiskTypeID.Equals(Guid.Empty) Then blnFromCert = True

        'Dim drv As DataRowView
        For Each drv As DataRowView In softQuestDV
            intChildCnt = 0
            Integer.TryParse(drv(SoftQuestion.SoftQuestionDV.COL_NAME_CHILD_COUNT).ToString, intChildCnt)
            Dim newNode As SysWebUICtls.TreeNode = New SysWebUICtls.TreeNode(drv(SoftQuestion.SoftQuestionDV.COL_NAME_DESCRIPTION).ToString, GuidControl.ByteArrayToGuid(drv(SoftQuestion.SoftQuestionDV.COL_NAME_SOFT_QUESTION_ID)).ToString & "||" & intChildCnt)
            If intChildCnt > 0 Then
                With newNode
                    .PopulateOnDemand = True
                    .Expanded = False
                End With
            End If
            If blnFromCert Then
                newNode.SelectAction = TreeNodeSelectAction.None
                newNode.NavigateUrl = "javascript:void(0);"
            Else
                newNode.SelectAction = TreeNodeSelectAction.Select
            End If
            e.Node.ChildNodes.Add(newNode)
        Next
    End Sub

    Private Sub PopulateTree()

        PopulateQuestionGroups()
        EnableDisableButtons()
    End Sub

    Private Sub EnableDisableButtons()
        If Me.State.RiskTypeID.Equals(Guid.Empty) Then
            ControlMgr.SetVisibleControl(Me, btnDelete_WRITE, True)
            ControlMgr.SetVisibleControl(Me, btnModify_WRITE, True)
            ControlMgr.SetVisibleControl(Me, btnNew_WRITE, True)
            btnPanel.Enabled = True
            ControlMgr.SetVisibleControl(Me, btnClose, False)
        Else
            ControlMgr.SetVisibleControl(Me, btnDelete_WRITE, False)
            ControlMgr.SetVisibleControl(Me, btnModify_WRITE, False)
            ControlMgr.SetVisibleControl(Me, btnNew_WRITE, False)
            btnPanel.Enabled = True
            ControlMgr.SetVisibleControl(Me, btnClose, True)
        End If

    End Sub

#End Region

#Region "SoftQuestionComments"

    Public Class SoftQuestionComments
        Implements IStateController

        Private NavController As INavigationController
        Private CallingPage As ElitaPlusPage

        Public Sub Process(ByVal callingPage As System.Web.UI.Page, ByVal navCtrl As INavigationController) Implements IStateController.Process
            Me.NavController = navCtrl
            Me.CallingPage = CType(callingPage, ElitaPlusPage)

            Dim certID As Guid = CType(Me.NavController.FlowSession(FlowSessionKeys.SESSION_SOFTQUESTION_CERT_ID), Guid)
            Dim certBO As Certificate = New Certificate(certID)

            Dim commentBO As Comment = New Comment
            commentBO.CertId = certID
            commentBO.CallerName = certBO.CustomerName

            commentBO.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, "SQR")

            Dim dv As DataView
            dv = LookupListNew.GetCommentTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            commentBO.Comments = LookupListNew.GetDescriptionFromId(dv, commentBO.CommentTypeId)
            commentBO.Save()

            commentBO.Comments = ""

            Me.NavController.FlowSession(FlowSessionKeys.SESSION_SOFTQUESTION_CERT_ID) = Nothing
            Me.NavController.Navigate(Me.CallingPage, FlowEvents.EVENT_COMMENTS, New CommentForm.Parameters(commentBO))
        End Sub
    End Class
#End Region


End Class
