﻿Imports System.Collections.Generic
Imports Assurant.Elita.ClientIntegration
Imports Assurant.ElitaPlus.BusinessObjectsNew.LegacyBridgeService
Imports Assurant.ElitaPlus.ElitaPlusWebApp.ClaimLegacyBridgeService
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Imports Assurant.Elita.CommonConfiguration.DataElements

Partial Class ClaimIssueDetailForm
    Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "
    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Constants"
    Private Const NO_DATA As String = " - "
    Public Const URL As String = "~\Claims\ClaimIssueDetailForm.aspx"
    Public Const MULTDDL_CONTROL As String = "~/Common/MultipleColumnDDLabelControl.ascx"
    'Constants for Questions Grid'
    Public Const Q_GRID_COL_ISSUE_ID_IDX As Integer = 0
    Public Const Q_GRID_COL_SOFT_QUESTION_ID_IDX As Integer = 1
    Public Const Q_GRID_COL_QUESTION_DESC_IDX As Integer = 2
    Public Const Q_GRID_COL_ENTITY_ATTRIBUTE_VALUE_IDX As Integer = 3
    Public Const Q_GRID_COL_ANSWER_IDX As Integer = 4

    Public Const ISSUE_ID As String = "lblIssueID"
    Public Const SOFT_QUESTION_ID As String = "lblSoftQuestionID"
    Public Const QUESTION_DESC As String = "lblQuestionDesc"
    Public Const ENTITY_VALUE As String = "lblEntityValue"
    Public Const QUESTION_ANSWER As String = "lblAnswer"

    'Constants for Process History Grid'
    Public Const Q_GRID_COL_CLAIM_ISSUE_ID_IDX As Integer = 0
    Public Const Q_GRID_COL_CLAIM_ISSUE_STATUS_ID_IDX As Integer = 1
    Public Const Q_GRID_COL_CLAIM_ISSUE_STATUS_DESC_IDX As Integer = 2
    Public Const Q_GRID_COL_COMMENT_IDX As Integer = 3
    Public Const Q_GRID_COL_PROCESSED_BY_IDX As Integer = 4
    Public Const Q_GRID_COL_PROCESSED_DATE_IDX As Integer = 5

    Public Const CLAIM_ISSUE_ID As String = "lblClaimIssueID"
    Public Const CLAIM_ISSUE_STATUS_ID As String = "lblClaimIssueStatusID"
    Public Const CLAIM_ISSUE_STATUS_DESC As String = "lblIssueStatusDesc"
    Public Const CLAIM_ISSUE_COMMENT As String = "lblComment"

    Public Const CLAIMISSUESDETAIL As String = "CLAIMISSUESDETAIL"

    'Claim Issue Statuses'
    Public Const STATUS_WAIVED As String = "WAIVED"
    Public Const STATUS_RESOLVED As String = "RESOLVED"
    Public Const STATUS_REJECTED As String = "REJECTED"
    Public Const STATUS_PENDING As String = "PENDING"
    Public Const STATUS_OPEN As String = "OPEN"
    Public Const STATUS_CLOSED As String = "CLOSED"

    'Claim Issue Response Constants'
    Public Const COL_NAME_CLAIM_ISSUE_RESPONSE_ID As String = "claim_issue_response_id"
    Public Const COL_NAME_ANSWER_ID As String = "answer_id"
    Public Const COL_NAME_ANSWER_VALUE As String = "answer_value"
    Public Const COL_NAME_SUPPORTS_CLAIM_ID As String = "supports_claim_id"
    Public Const COL_NAME_CLAIM_ISSUE_ID As String = "claim_issue_id"

    Public Const NEW_CLAIM_FORM As String = "newclaimform"
    Public Const CLAIM_ISSUE_FORM As String = "claimissueform"

    Private Const SP_CLAIM_CODE As String = "SP_CLAIM_CODE"
    Private Const SP_CLAIM_CODE_ISSUE_PROCESS As String = "ISSUE_PROCESS"
    Private Const SP_CLAIM_TYPE_ISSUE_PROCESS As String = "SP_CLAIM_CODE-ISSUE_PROCESS"

#End Region

#Region "Variables"

    Private mbIsFirstPass As Boolean = True
    Private bEdit As Boolean = False

#End Region

#Region "Page Parameters"
    Public Class Parameters
        Public ClaimBO As ClaimBase
        Public ClaimIssueId As Guid
        Public IssueType As String

        Public Sub New(claimBO As ClaimBase, claimIssueId As Guid, Optional ByVal issueType As String = "NONE")
            Me.ClaimBO = claimBO
            Me.ClaimIssueId = claimIssueId
            Me.IssueType = issueType
        End Sub
    End Class
#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As ClaimBase
        Public BoChanged As Boolean = False
        Public Sub New(LastOp As DetailPageCommand, curEditingBo As ClaimBase, Optional ByVal boChanged As Boolean = False)
            LastOperation = LastOp
            EditingBo = curEditingBo
            Me.BoChanged = boChanged
        End Sub
        Public Sub New(LastOp As DetailPageCommand)
            LastOperation = LastOp
        End Sub
    End Class
#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    Class MyState
        Public ClaimIssueId As Guid = Guid.Empty
        Public MyBO As ClaimIssue
        Public MyPrevBO As ClaimIssue
        Public MyClaimBO As ClaimBase
        Public MyCertificateBO As Certificate
        Public MyUserBO As User
        Public InputParameters As Parameters

        Public ClaimNumber As String
        Public IssueCode As String
        Public IssueDescription As String
        Public CreatedBy As String
        Public CreatedDate As DateTime
        Public ProcessedBy As String
        Public ProcessedDate As DateTime
        Public IssueStatus As String

        Public SelectedClaimIssueResponseId As Guid
        Public EditClaimIssueResponse As Boolean

        Public aryCountryId As ArrayList
        Public checkIssueProcessClaimExists As String
        Public isIssueProcessClaimExists As Boolean = False

    End Class

    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            If (NavController Is Nothing) Then
                Return CType(MyBase.State, MyState)
            Else
                If NavController.State Is Nothing Then
                    NavController.State = New MyState
                    InitializeFromFlowSession()
                End If
                Return CType(NavController.State, MyState)
            End If

        End Get
    End Property

    Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
        Try
            If CallingParameters IsNot Nothing Then
                State.InputParameters = CType(CallingPar, Parameters)
                State.ClaimIssueId = State.InputParameters.ClaimIssueId

                State.MyClaimBO = State.InputParameters.ClaimBO

                ' Need to save the claim issue on this screen itself inplace of sending back to parent for saving it along the claim
                ' This is valid for callPage - currently called from claim wizard form
                State.MyBO = New ClaimIssue(State.ClaimIssueId)

                State.MyPrevBO = State.MyBO
                State.MyCertificateBO = New Certificate(State.MyClaimBO.CertificateId)
                State.aryCountryId = New ArrayList
                State.aryCountryId.Add(State.MyCertificateBO.AddressChild.CountryId)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Sub SaveGUIState()
        State.ClaimNumber = Label_ClaimNumber.Text
        State.IssueCode = Label_IssueCode.Text
        State.IssueDescription = Label_IssueDescription.Text
        State.CreatedBy = Label_CreatedBy.Text
        State.CreatedDate = DateHelper.GetDateValue(Label_CreatedDate.Text)
        State.ProcessedBy = Label_ProcessedBy.Text
        State.ProcessedDate = DateHelper.GetDateValue(Label_ProcessedDate.Text)
        State.IssueStatus = Label_IssueStatus.Text
    End Sub

    Sub RestoreGUIState()
        Label_ClaimNumber.Text = State.MyClaimBO.ClaimNumber
        Label_IssueCode.Text = State.MyBO.IssueCode
        Label_IssueDescription.Text = State.MyBO.IssueDescription
        Label_CreatedBy.Text = State.MyBO.CreatedBy
        Label_CreatedDate.Text = State.MyBO.CreatedDate.ToString()
        Label_ProcessedBy.Text = State.ProcessedBy
        Label_ProcessedDate.Text = State.ProcessedDate.ToString()
        Label_IssueStatus.Text = State.IssueStatus
    End Sub

    Protected Sub InitializeFromFlowSession()

        State.InputParameters = CType(NavController.ParametersPassed, Parameters)

        Try
            If State.InputParameters.ClaimBO IsNot Nothing Then
                State.ClaimIssueId = State.InputParameters.ClaimIssueId

                State.MyClaimBO = State.InputParameters.ClaimBO

                ' This is valid for when navigation is used(not callpage) - currently called from new claim form/ claim detail
                If State.InputParameters.IssueType = Codes.ISSUE_TYPE_CONSEQUENTIAL_DAMAGE Then
                    ' if the issue type is CD, then we need to save it
                    State.MyClaimBO = ClaimFacade.Instance.GetClaim(Of ClaimBase)(CType(State.InputParameters.ClaimBO.Id, Guid))
                    State.MyBO = New ClaimIssue(State.ClaimIssueId)
                Else
                    State.MyClaimBO = State.InputParameters.ClaimBO
                    State.MyBO = CType(State.MyClaimBO.ClaimIssuesList.GetChild(State.ClaimIssueId), ClaimIssue)
                End If

                State.MyPrevBO = State.MyBO
                State.MyCertificateBO = New Certificate(State.MyClaimBO.CertificateId)
                State.aryCountryId = New ArrayList
                State.aryCountryId.Add(State.MyCertificateBO.AddressChild.CountryId)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub


#End Region

#Region "Properties"

    Private ReadOnly Property isUserHasAccessToProcessIssue As Boolean
        Get
            If State.checkIssueProcessClaimExists Is Nothing Then
                State.isIssueProcessClaimExists = GetSpUserClaimsIssueProcess()
                State.checkIssueProcessClaimExists = State.isIssueProcessClaimExists.ToString
            End If
            Return State.isIssueProcessClaimExists
        End Get
    End Property

#End Region

#Region "Page_Events"

    Private Sub Page_load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If mbIsFirstPass = True Then
            mbIsFirstPass = False
        Else
            ' Do not load again the Page that was already loaded
            Return
        End If

        MasterPage.UsePageTabTitleInBreadCrum = False
        MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("CLAIM_ISSUE_SUMMARY")
        MasterPage.PageTitle = String.Format("{0} ", TranslationBase.TranslateLabelOrMessage("CLAIM_ISSUE_SUMMARY"))
        MessageLiteral.Text = TranslationBase.TranslateLabelOrMessage("MSG_SELECT_REASON")
        msgSelectwaiveReason.Text = TranslationBase.TranslateLabelOrMessage("MSG_SELECT_REASON")
        btnReopenContinue.Attributes.Add("onclick", String.Format("return validate('{0}','{1}','{2}');", rdbtlstIssueReopen.ClientID, txtComments.ClientID, modalMessageBoxReopen.ClientID))
        btnWaiveContinue.Attributes.Add("onclick", String.Format("return validate('{0}','{1}','{2}');", rdbtRsnWaiveIss.ClientID, txtWaiveComments.ClientID, modalMessageBoxWaive.ClientID))
        btnWaiveCancel.Attributes.Add("onclick", String.Format("HideErrorAndModal('{0}','{1}','{2}','{3}');", "ModalWaiveIssue", rdbtRsnWaiveIss.ClientID, modalMessageBoxWaive.ClientID, txtWaiveComments.ClientID))
        btnReopenCancel.Attributes.Add("onclick", String.Format("HideErrorAndModal('{0}','{1}','{2}','{3}');", "ModalReopenIssue", rdbtlstIssueReopen.ClientID, modalMessageBoxReopen.ClientID, txtComments.ClientID))
        UpdateBreadCrum()

        MasterPage.MessageController.Clear()

        PopulateLists()
        PopulateFormFromBOs()
    End Sub
    Sub EnableDisableFields()
        ControlMgr.SetVisibleControl(Me, Label_ClaimNumber, True)
        ControlMgr.SetVisibleControl(Me, Label_IssueCode, True)
        ControlMgr.SetVisibleControl(Me, Label_IssueDescription, True)
        ControlMgr.SetVisibleControl(Me, Label_CreatedBy, True)
        ControlMgr.SetVisibleControl(Me, Label_CreatedDate, True)
        ControlMgr.SetVisibleControl(Me, Label_ProcessedBy, True)
        ControlMgr.SetVisibleControl(Me, Label_ProcessedDate, True)
        'Issue Status?        
    End Sub

    Private Sub UpdateBreadCrum()
        If (State IsNot Nothing) Then
            If (State.MyBO IsNot Nothing) Then
                MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & "Claim Issues"
            End If
        End If
    End Sub

#End Region

#Region "Buttons Clicks "

    Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
        If (NavController IsNot Nothing) Then
            Select Case NavController.PrevNavState.Name
                Case "CLAIM_DETAIL"
                    NavController.Navigate(Me, "claimForm", New ClaimForm.Parameters(State.MyClaimBO.Id))
                Case Else
                    NavController.Navigate(Me, "back")
            End Select
        Else
            Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyClaimBO)
            MyBase.ReturnToCallingPage(retObj)
        End If

    End Sub
    Private Sub RegisterJavaScriptCode()
        Dim sJavaScript As String
        sJavaScript = "<SCRIPT>" & Environment.NewLine
        sJavaScript &= "function ToggleSelection(ctlCodeDropDown, ctlDescDropDown, change_Desc_Or_Code, lblCaption)" & Environment.NewLine
        sJavaScript &= "{" & Environment.NewLine & "var objCodeDropDown = document.getElementById(ctlCodeDropDown);" & Environment.NewLine
        sJavaScript &= "var objDescDropDown = document.getElementById(ctlDescDropDown);" & Environment.NewLine
        'sJavaScript &= " alert( 'Code name = ' + ctlCodeDropDown + ' obj =' + objCodeDropDown + '\n  Desc name = ' + ctlDescDropDown + ' obj =' + objDescDropDown + '\n  Caption name = ' + lblCaption + ' obj =' + document.getElementById(lblCaption));" & Environment.NewLine
        sJavaScript &= "if (change_Desc_Or_Code=='C'){" & Environment.NewLine & "objCodeDropDown.value = objDescDropDown.options[objDescDropDown.selectedIndex].value;}" & Environment.NewLine
        sJavaScript &= "else { objDescDropDown.value = objCodeDropDown.options[objCodeDropDown.selectedIndex].value;}" & Environment.NewLine
        sJavaScript &= "if (lblCaption != '') {document.all.item(lblCaption).style.color = '';}}" & Environment.NewLine
        sJavaScript &= "</SCRIPT>" & Environment.NewLine
        Page.RegisterStartupScript("ToggleDropDown", sJavaScript)
    End Sub
#End Region

#Region "Controlling Logic"
    Private Function GetSpUserClaimsIssueProcess() As Boolean
        Dim dvSpClaims As DataView = Authentication.CurrentUser.GetSpUserClaims(Authentication.CurrentUser.Id, Authentication.LangId, SP_CLAIM_CODE)
        Dim effectiveDate As Date
        Dim expirationDate As Date
        Dim todayDate As Date = DateTime.Now
        Dim IsIssueProcessClaimExists As Boolean = False
        For Each row As DataRow In dvSpClaims.Table.Rows
            If row.Item("code") = SP_CLAIM_CODE_ISSUE_PROCESS AndAlso row("sp_claim_value") = State.MyBO.IssueCode Then
                effectiveDate = DirectCast(row("effective_date"), Date)
                expirationDate = DirectCast(row("expiration_date"), Date)
                If effectiveDate <= todayDate AndAlso expirationDate >= todayDate Then
                    IsIssueProcessClaimExists = True
                    Exit For
                End If
            End If
        Next
        Return IsIssueProcessClaimExists
    End Function
    Protected Sub PopulateFormFromBOs()
        Dim cssClassName As String
        Dim userDv As DataView
        PopulateControlFromBOProperty(Label_ClaimNumber, State.MyClaimBO.ClaimNumber)
        With State.MyBO
            PopulateControlFromBOProperty(Label_IssueCode, .IssueCode)
            PopulateControlFromBOProperty(Label_IssueDescription, .IssueDescription)
            PopulateControlFromBOProperty(Label_CreatedBy, .CreatedBy)
            PopulateControlFromBOProperty(Label_CreatedDate, .CreatedDate)
            PopulateControlFromBOProperty(Label_ProcessedBy, .ProcessedBy)
            PopulateControlFromBOProperty(Label_ProcessedDate, .ProcessedDate)
            PopulateControlFromBOProperty(Label_IssueStatus, LookupListNew.GetDescriptionFromCode(LookupListCache.LK_CLAIM_ISSUE_STATUS, .StatusCode))

            If (State.MyBO.StatusCode = Codes.CLAIMISSUE_STATUS__RESOLVED OrElse State.MyBO.StatusCode = Codes.CLAIMISSUE_STATUS__WAIVED) Then
                cssClassName = "StatActive"
            Else
                cssClassName = "StatClosed"
            End If

            tdLabelIssueStatus.Attributes.Item("Class") = cssClassName

        End With

        EnableDisableFields()
        If ((State.MyBO.StatusCode = STATUS_OPEN OrElse State.MyBO.StatusCode = STATUS_PENDING) AndAlso (State.MyBO.Claim.EvaluatePreConditions(State.MyBO.Issue.PreConditionList))) Then
            ControlMgr.SetEnableControl(Me, btnProcess, True)
            ControlMgr.SetEnableControl(Me, btnWaive, True)
            ControlMgr.SetEnableControl(Me, btnUndo, True)
            btnReopen.Visible = False
            bEdit = True
            If State.MyBO.Issue.SPClaimType = SP_CLAIM_TYPE_ISSUE_PROCESS AndAlso Not isUserHasAccessToProcessIssue Then
                ControlMgr.SetEnableControl(Me, btnProcess, False)
                ControlMgr.SetEnableControl(Me, btnWaive, False)
                ControlMgr.SetEnableControl(Me, btnUndo, False)
                MasterPage.MessageController.AddWarning("NO_SECURITY_CLAIM_AUTHORIZATION", True)
            End If
        Else
            bEdit = False
            ControlMgr.SetEnableControl(Me, btnProcess, False)
            ControlMgr.SetEnableControl(Me, btnWaive, False)
            ControlMgr.SetEnableControl(Me, btnUndo, False)
            If State.MyClaimBO.CanIssuesReopen() Then
                btnReopen.Visible = True
            End If
        End If

        Try
            BindBoPropertiesToLabels()
            AddLabelDecorations(State.MyBO)

            Dim dvQuestions As DataView = State.MyBO.ClaimIssueQuestionListByDealer(State.MyCertificateBO.DealerId).Table.DefaultView
            If dvQuestions.Count > 0 Then
                grdQuestions.DataSource = dvQuestions
                grdQuestions.DataBind()
            End If

            Dim dvCLaimIssueStatus As DataView = State.MyBO.ClaimIssueStatusList.Table.DefaultView
            If dvCLaimIssueStatus.Count > 0 Then
                dvCLaimIssueStatus.Sort = "created_date desc"
                grdProcessHistory.DataSource = dvCLaimIssueStatus
                grdProcessHistory.DataBind()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Protected Sub BindBoPropertiesToLabels()
        Try
            BindBOPropertyToLabel(State.MyBO, "lblIssueCode", lblIssueCode)
            BindBOPropertyToLabel(State.MyBO, "lblIssueDescription", lblIssueDescription)
            BindBOPropertyToLabel(State.MyBO, "lblCreatedBy", lblCreatedBy)
            BindBOPropertyToLabel(State.MyBO, "lblCreatedDate", lblCreatedDate)
            BindBOPropertyToLabel(State.MyBO, "lblProcessedBy", lblProcessedBy)
            BindBOPropertyToLabel(State.MyBO, "lblProcessedDate", lblProcessedDate)
            BindBOPropertyToLabel(State.MyBO, "lblIssueStatus", lblIssueStatus)

            ClearGridHeadersAndLabelsErrSign()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub PopulateLists()
        Dim IssueReopenReasonListLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ISSREOPNRSN", Thread.CurrentPrincipal.GetLanguageCode())
        rdbtlstIssueReopen.Populate(IssueReopenReasonListLkl, New PopulateOptions() With
            {
              .AddBlankItem = False
                })
        'BindListControlToDataView(rdbtRsnWaiveIss, LookupListNew.GetIssueWaiveReasonLookupList(Authentication.LangId), "DESCRIPTION", "ID", False, True, False)
        Dim IssueWaiveReasonListLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ISSWAIVERSN", Thread.CurrentPrincipal.GetLanguageCode())
        rdbtRsnWaiveIss.Populate(IssueWaiveReasonListLkl, New PopulateOptions() With
            {
              .AddBlankItem = False
                })
    End Sub
#End Region

#Region "Error Handling"


#End Region


    Private Sub grdQuestions_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdQuestions.RowDataBound
        Try
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim bContainsEntityAttribute As Boolean = False

            If (e.Row.RowType = DataControlRowType.DataRow) Then
                'Populate Question Description'
                Dim gSoftQuestionID As New Guid(CType(e.Row.Cells(Q_GRID_COL_SOFT_QUESTION_ID_IDX).FindControl(SOFT_QUESTION_ID), Label).Text)
                Dim oQuestion As New Question(gSoftQuestionID)

                CType(e.Row.Cells(Q_GRID_COL_QUESTION_DESC_IDX).FindControl(QUESTION_DESC), Label).Text = oQuestion.TranslatedDescription 'oQuestion.Description

                Dim oEntityAttribute As EntityAttribute

                If Not oQuestion.EntityAttributeId = Guid.Empty Then
                    oEntityAttribute = New EntityAttribute(oQuestion.EntityAttributeId)
                    If State.InputParameters.IssueType <> Codes.ISSUE_TYPE_CONSEQUENTIAL_DAMAGE AndAlso oEntityAttribute.Entity <> Codes.ENTITY_TYPE_CONSEQUENTIAL_DAMAGE AndAlso oEntityAttribute.Entity <> Codes.ENTITY_TYPE_BENEFIT_CHECK Then
                        bContainsEntityAttribute = True
                    End If
                End If

                'Populate Answer based on Answer Type
                Dim oAnswerType As New AnswerType(oQuestion.AnswerTypeId)
                Select Case oAnswerType.Code
                    Case "YES_NO"
                        Dim ddl As New DropDownList
                        ddl.ID = "ddlYesNo"
                        Dim dv As DataView = oQuestion.AnswerChildren.Table.AsDataView()
                        'Dim dv As DataView = LookupListNew.GetYesNoLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                        'Me.BindListControlToDataView(ddl, dv, "DESCRIPTION", "ID", True)

                        BindListControlToDataView(ddl, dv, "ANSWER_VALUE", "LIST_ITEM_ID", True, False)
                        ddl.SelectedValue = GetAnswerSelectedId(gSoftQuestionID).ToString()
                        ddl.Enabled = bEdit
                        e.Row.Cells(Q_GRID_COL_ANSWER_IDX).Controls.Add(ddl)
                        If bContainsEntityAttribute Then
                            e.Row.Cells(Q_GRID_COL_ENTITY_ATTRIBUTE_VALUE_IDX).Controls.Add(GetEntityValue(oEntityAttribute.Entity, oEntityAttribute.Attribute, ddl))
                        End If
                    Case "PSD"
                        Dim ddlMult As MultipleColumnDDLabelControl
                        ddlMult = CType(LoadControl(MULTDDL_CONTROL), MultipleColumnDDLabelControl)
                        ddlMult.ID = "MultiDrop"
                        AddHandler ddlMult.SelectedDropChanged, AddressOf ddlMult_OnSelectedDropChanged

                        Dim dv As DataView = LookupListNew.GetPoliceLookupList(State.aryCountryId)
                        ddlMult.SetControl(True,
                                          ddlMult.MODES.NEW_MODE,
                                          True,
                                          dv,
                                          "Police Station",
                                          True)

                        Dim sSelected As String
                        Dim gSelected As Guid

                        gSelected = GetAnswerSelectedId(gSoftQuestionID)

                        ddlMult.SelectedGuid = gSelected

                        'ddlMult.CodeDropDown.SelectedValue = sSelected
                        ddlMult.CodeDropDown.Enabled = bEdit

                        'ddlMult.DescDropDown.SelectedValue = sSelected
                        ddlMult.DescDropDown.Enabled = bEdit

                        e.Row.Cells(Q_GRID_COL_ANSWER_IDX).Controls.Add(ddlMult)


                        If bContainsEntityAttribute Then
                            e.Row.Cells(Q_GRID_COL_ENTITY_ATTRIBUTE_VALUE_IDX).Controls.Add(GetEntityValue(oEntityAttribute.Entity, oEntityAttribute.Attribute, ddlMult.CodeDropDown))
                        End If
                    Case "NUMBER", "NAME", "DESCRIPTION"
                        Dim txt As New TextBox
                        txt.ID = "txt"
                        txt.Text = GetAnswerTextValue(gSoftQuestionID)
                        txt.Enabled = bEdit
                        e.Row.Cells(Q_GRID_COL_ANSWER_IDX).Controls.Add(txt)
                        If bContainsEntityAttribute Then
                            e.Row.Cells(Q_GRID_COL_ENTITY_ATTRIBUTE_VALUE_IDX).Controls.Add(GetEntityValue(oEntityAttribute.Entity, oEntityAttribute.Attribute))
                        End If
                    Case "DATE"
                        Dim txtDate As New TextBox
                        txtDate.ID = "txtDate"
                        txtDate.Text = GetAnswerTextValue(gSoftQuestionID)
                        Dim btnCalendar As New ImageButton
                        btnCalendar.ID = "btnCalendar"
                        btnCalendar.ImageUrl = "~/App_Themes/Default/Images/calendar.png"
                        txtDate.Enabled = bEdit
                        btnCalendar.Enabled = bEdit
                        e.Row.Cells(Q_GRID_COL_ANSWER_IDX).Controls.Add(txtDate)
                        e.Row.Cells(Q_GRID_COL_ANSWER_IDX).Controls.Add(btnCalendar)

                        txtDate.ID = txtDate.ClientID
                        AddCalendar_New(btnCalendar, txtDate)
                        If bContainsEntityAttribute Then
                            e.Row.Cells(Q_GRID_COL_ENTITY_ATTRIBUTE_VALUE_IDX).Controls.Add(GetEntityValue(oEntityAttribute.Entity, oEntityAttribute.Attribute))
                        End If
                    Case "DATE_TIME"
                        Dim txtDateTime As New TextBox
                        txtDateTime.ID = "txtDateTime"
                        txtDateTime.Text = GetAnswerTextValue(gSoftQuestionID)
                        Dim btnCalendar As New ImageButton
                        btnCalendar.ID = "btnCalendar"
                        btnCalendar.ImageUrl = "~/App_Themes/Default/Images/calendar.png"
                        txtDateTime.Enabled = bEdit
                        btnCalendar.Enabled = bEdit
                        e.Row.Cells(Q_GRID_COL_ANSWER_IDX).Controls.Add(txtDateTime)
                        e.Row.Cells(Q_GRID_COL_ANSWER_IDX).Controls.Add(btnCalendar)

                        txtDateTime.ID = txtDateTime.ClientID
                        AddCalendarwithTime_New(btnCalendar, txtDateTime)
                        If bContainsEntityAttribute Then
                            e.Row.Cells(Q_GRID_COL_ENTITY_ATTRIBUTE_VALUE_IDX).Controls.Add(GetEntityValue(oEntityAttribute.Entity, oEntityAttribute.Attribute))
                        End If
                End Select
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub grdProcessHistory_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdProcessHistory.RowDataBound
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim gClaimIssueStatusID As New Guid(CType(e.Row.Cells(Q_GRID_COL_CLAIM_ISSUE_STATUS_ID_IDX).FindControl(CLAIM_ISSUE_STATUS_ID), Label).Text)
            CType(e.Row.Cells(Q_GRID_COL_CLAIM_ISSUE_STATUS_DESC_IDX).FindControl(CLAIM_ISSUE_STATUS_DESC), Label).Text = LookupListNew.GetDescriptionFromId(LookupListCache.LK_CLAIM_ISSUE_STATUS, gClaimIssueStatusID).ToString()

            Dim strProcessedDate As String = Convert.ToString(e.Row.Cells(Q_GRID_COL_PROCESSED_DATE_IDX).Text)
            strProcessedDate = strProcessedDate.Replace("&nbsp;", "")
            If String.IsNullOrWhiteSpace(strProcessedDate) = False Then
                Dim tempProcessedDate = Convert.ToDateTime(e.Row.Cells(Q_GRID_COL_PROCESSED_DATE_IDX).Text.Trim())
                Dim formattedProcessedDate = GetLongDate12FormattedString(tempProcessedDate)
                e.Row.Cells(Q_GRID_COL_PROCESSED_DATE_IDX).Text = Convert.ToString(formattedProcessedDate)
            End If
        End If
    End Sub

    Private Sub btnProcess_Click(sender As Object, e As EventArgs) Handles btnProcess.Click, btnWaiveContinue.Click
        'DEF-2862
        Dim policeReportNumber As String = String.Empty
        Dim policeStationId As Guid
        'DEF-2862

        'For Consequential Damage Issues
        Dim isEntityReimbursementAmount As Boolean = False
        Dim reimbursementAmt As String = String.Empty

        'For Entity Attribute - IMEI Number
        Dim isEntityAttributeBenefitCheckIMEInumber As Boolean = False
        Dim ImeiNumber As String = String.Empty
        Dim BenefitCheckNoAction As Guid = Guid.Empty

        Dim bWaived As Boolean = False
        If CType(sender, Button).ID = btnWaiveContinue.ID Then
            bWaived = True
        End If

        For Each row As GridViewRow In grdQuestions.Rows
            If row.RowType = DataControlRowType.DataRow Then
                Dim gSoftQuestionID As New Guid(CType(row.Cells(Q_GRID_COL_SOFT_QUESTION_ID_IDX).FindControl(SOFT_QUESTION_ID), Label).Text)
                Dim oQuestion As New Question(gSoftQuestionID)
                Dim oAnswerType As New AnswerType(oQuestion.AnswerTypeId)
                Dim oClaimIssueResponse As ClaimIssueResponse
                If Not GetClaimIssueResponseId(gSoftQuestionID) = Guid.Empty Then
                    oClaimIssueResponse = State.MyBO.GetClaimIssueResponseChild(GetClaimIssueResponseId(gSoftQuestionID))
                Else
                    oClaimIssueResponse = State.MyBO.GetNewClaimIssueResponseChild()
                End If

                'DEF-2862
                Dim oEntityAttribute As EntityAttribute

                If Not oQuestion.EntityAttributeId = Guid.Empty Then
                    oEntityAttribute = New EntityAttribute(oQuestion.EntityAttributeId)
                End If
                'DEF-2862

                With oClaimIssueResponse
                    .ClaimIssueId = State.MyBO.ClaimIssueId
                    Dim c As TableCell = row.Cells(Q_GRID_COL_ANSWER_IDX)

                    For Each ctrl As Control In c.Controls
                        If ctrl.Visible = True Then
                            Select Case ctrl.GetType().ToString()
                                Case "System.Web.UI.WebControls.TextBox"
                                    If CType(ctrl, TextBox).Text.Length > 0 Then
                                        'TODO Begin: replace this with a more abstract solution in the future
                                        ' Date validation
                                        If oAnswerType.Code = "DATE" Then
                                            If Not IsValidDate(DateHelper.GetDateValue(CType(ctrl, TextBox).Text), DateHelper.GetDateValue(DateTime.Today.ToString()), "GreaterThanOrEqual") Then
                                                MasterPage.MessageController.AddErrorAndShow(TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIM_ISSUES_INVALID_DATE))
                                                If (oEntityAttribute IsNot Nothing) Then
                                                    SetEntityValue(oEntityAttribute.Entity, oEntityAttribute.Attribute, Nothing)
                                                End If
                                                .Delete()
                                                Exit Sub
                                            End If
                                            If Not IsValidDate(DateHelper.GetDateValue(CType(ctrl, TextBox).Text), State.MyClaimBO.LossDate, "LessThan") Then
                                                MasterPage.MessageController.AddErrorAndShow(TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIM_ISSUES_DATE_PRIOR_TO_DATE_OF_LOSS))
                                                If (oEntityAttribute IsNot Nothing) Then
                                                    SetEntityValue(oEntityAttribute.Entity, oEntityAttribute.Attribute, Nothing)
                                                End If
                                                .Delete()
                                                Exit Sub
                                            End If
                                        End If

                                        ' DateTime validation
                                        If oAnswerType.Code = "DATE_TIME" Then
                                            If Not IsValidDate(DateHelper.GetDateValue(CType(ctrl, TextBox).Text), DateTime.Now, "GreaterThanOrEqual") Then
                                                MasterPage.MessageController.AddErrorAndShow(TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIM_ISSUES_INVALID_DATE))
                                                If (oEntityAttribute IsNot Nothing) Then
                                                    SetEntityValue(oEntityAttribute.Entity, oEntityAttribute.Attribute, Nothing)
                                                End If
                                                .Delete()
                                                Exit Sub
                                            End If
                                            If Not IsValidDate(DateHelper.GetDateValue(CType(ctrl, TextBox).Text), CType(State.MyClaimBO.LossDate, DateTime), "LessThan") Then
                                                MasterPage.MessageController.AddErrorAndShow(TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIM_ISSUES_DATE_PRIOR_TO_DATE_OF_LOSS))
                                                If (oEntityAttribute IsNot Nothing) Then
                                                    SetEntityValue(oEntityAttribute.Entity, oEntityAttribute.Attribute, Nothing)
                                                End If
                                                .Delete()
                                                Exit Sub
                                            End If
                                        End If

                                        'DEF-2862
                                        If oEntityAttribute IsNot Nothing AndAlso oEntityAttribute.Attribute = Codes.ENTITY_ATTRIBUTE_REPORT_NUMBER Then
                                            policeReportNumber = CType(ctrl, TextBox).Text
                                        End If
                                        'DEF-2862

                                        'End: replace this with a more abstract solution in the future
                                        .AnswerId = CType(oQuestion.AnswerChildren(0), Answer).Id
                                        .SupportsClaimId = CType(oQuestion.AnswerChildren(0), Answer).SupportsClaimId
                                        .AnswerDescription = CType(oQuestion.AnswerChildren(0), Answer).Description
                                        .AnswerValue = CType(ctrl, TextBox).Text

                                        If oEntityAttribute IsNot Nothing AndAlso oEntityAttribute.Attribute = Codes.ENTITY_ATTRIBUTE_REIMBURSEMENT_AMOUNT Then
                                            isEntityReimbursementAmount = True
                                            reimbursementAmt = CType(ctrl, TextBox).Text
                                        End If

                                        If oEntityAttribute IsNot Nothing _
                                            AndAlso (oEntityAttribute.Attribute = Codes.ENTITY_ATTRIBUTE_BENEFIT_CHECK_IMEI_NUMBER) Then
                                            isEntityAttributeBenefitCheckIMEInumber = True
                                            ImeiNumber = CType(ctrl, TextBox).Text
                                        End If

                                        If (oEntityAttribute IsNot Nothing) AndAlso (oAnswerType.Code = "DATE") Then
                                            SetEntityValue(oEntityAttribute.Entity, oEntityAttribute.Attribute, DateHelper.GetDateValue(.AnswerValue))
                                        End If

                                        If (oEntityAttribute IsNot Nothing) AndAlso (oAnswerType.Code = "DATE_TIME") Then
                                            SetEntityValue(oEntityAttribute.Entity, oEntityAttribute.Attribute, DateHelper.GetDateValue(.AnswerValue))
                                        End If

                                        .Save()

                                    Else
                                        .Delete()
                                    End If
                                Case "System.Web.UI.WebControls.DropDownList"
                                    If CType(ctrl, DropDownList).SelectedIndex > 0 Then
                                        Dim gSelectedId As Guid = New Guid(CType(ctrl, DropDownList).SelectedItem.Value)
                                        If oQuestion.AnswerChildren.Count > 0 Then
                                            For Each ans As Answer In oQuestion.AnswerChildren
                                                If ans.ListItemId = gSelectedId Then
                                                    .AnswerId = ans.Id
                                                    .SupportsClaimId = ans.SupportsClaimId
                                                    .AnswerDescription = ans.Description
                                                    .AnswerValue = GuidControl.GuidToHexString(New Guid(CType(ctrl, DropDownList).SelectedItem.Value))
                                                    Exit For
                                                End If
                                            Next

                                            If oEntityAttribute IsNot Nothing AndAlso oEntityAttribute.Attribute = Codes.ENTITY_ATTRIBUTE_BENEFIT_CHECK_NO_ACTION_IMEI Then
                                                BenefitCheckNoAction = gSelectedId
                                            End If

                                            .Save()
                                        End If
                                    Else
                                        .Delete()
                                    End If
                                Case "ASP.common_multiplecolumnddlabelcontrol_ascx"
                                    Dim gSelectedId As Guid
                                    Dim KeySelected As String
                                    Dim FormKey As Boolean

                                    Dim ddl As MultipleColumnDDLabelControl = CType(ctrl, MultipleColumnDDLabelControl)
                                    'TODO Add this functionality to MultiDropDown control itself
                                    For Each key As String In Request.Form.AllKeys
                                        FormKey = key.EndsWith("moMultipleColumnDrop")
                                        If FormKey Then
                                            Dim strVal As String = Request.Form.GetValues(key)(0)
                                            gSelectedId = New Guid(strVal.ToString())
                                            Exit For
                                        End If
                                    Next

                                    If FormKey Then

                                        ddl.SelectedGuid = gSelectedId

                                        'DEF-2862
                                        If oEntityAttribute IsNot Nothing AndAlso oEntityAttribute.Attribute = "PoliceStationId" Then
                                            policeStationId = gSelectedId
                                        End If
                                        'DEF-2862

                                        If oQuestion.AnswerChildren.Count > 0 Then
                                            For Each ans As Answer In oQuestion.AnswerChildren
                                                .AnswerId = ans.Id
                                                .SupportsClaimId = ans.SupportsClaimId
                                                .AnswerDescription = ans.Description
                                                .AnswerValue = GuidControl.GuidToHexString(New Guid(CType(ctrl, MultipleColumnDDLabelControl).CodeDropDown.SelectedValue.ToString()))
                                                If ans.ListItemId = gSelectedId Then
                                                    Exit For
                                                End If
                                            Next
                                            .Save()
                                        End If
                                    Else
                                        .Delete()
                                    End If
                                    'If CType(ctrl, MultipleColumnDDLabelControl).CodeDropDown.SelectedIndex > 0 Then
                                    '    Dim gSelectedId As Guid = New Guid(CType(ctrl, MultipleColumnDDLabelControl).CodeDropDown.SelectedValue.ToString())
                                    '    If oQuestion.AnswerChildren.Count > 0 Then
                                    '        For Each ans As Answer In oQuestion.AnswerChildren
                                    '            .AnswerId = ans.Id
                                    '            .SupportsClaimId = ans.SupportsClaimId
                                    '            .AnswerDescription = ans.Description
                                    '            .AnswerValue = GuidControl.GuidToHexString(New Guid(CType(ctrl, MultipleColumnDDLabelControl).CodeDropDown.SelectedValue.ToString()))
                                    '            If ans.ListItemId = gSelectedId Then
                                    '                Exit For
                                    '            End If
                                    '        Next
                                    '        .Save()
                                    '    End If
                                    'Else
                                    '    .Delete()
                                    'End If
                            End Select
                        End If
                    Next
                End With
            End If
        Next

        'DEF-2862
        If (policeReportNumber.Trim <> String.Empty AndAlso policeStationId <> Nothing AndAlso
              Not policeReportNumber.Trim.ToUpper().Equals("N/A") AndAlso
              Not policeReportNumber.Trim.ToUpper().Equals("N-A")) Then

            Dim lstClaim As Collections.Generic.List(Of String)
            If PoliceReport.IsReportNumberInUse(lstClaim, policeReportNumber, policeStationId) Then
                Dim sbMsg As New System.Text.StringBuilder
                'sbMsg.Append("This Police Report# Is Already Used By The Claim#:")
                sbMsg.Append(TranslationBase.TranslateLabelOrMessage(Message.MSG_DUPLICATE_POLICE_REPORT_NUMBER))
                sbMsg.Append(" ")
                For int As Integer = 0 To (lstClaim.Count - 1)
                    sbMsg.Append(lstClaim.Item(int))
                    If int < (lstClaim.Count - 1) Then
                        sbMsg.Append(",")
                    Else
                        sbMsg.Append(". ")
                    End If
                Next
                MasterPage.MessageController.AddError(sbMsg.ToString)
                Exit Sub 'don't create the claim yet

            End If
        End If
        'DEF-2862

        ' Consequential Damage ReimbursementAmount
        If State.InputParameters.IssueType = Codes.ISSUE_TYPE_CONSEQUENTIAL_DAMAGE _
            AndAlso isEntityReimbursementAmount Then
            ' If Issue is Consequential Damage and one of the question is Reimbursement Entity attribute 
            Dim isCdReimAmtSet As Boolean = False
            isCdReimAmtSet = ConsequentialDamageReimbursementAmount(reimbursementAmt)
            If Not isCdReimAmtSet Then
                'if Reimbursement Amount is not set then don't resolve the issue yet
                Exit Sub
            End If
        End If

        ' Benefit Check IMEI Number
        If Not bWaived AndAlso isEntityAttributeBenefitCheckIMEInumber Then
            ' IMEI number updated in the Elita for the claim equipment record
            Dim isIMEIsaved As Boolean = False
            isIMEIsaved = UpdateClaimedEquip(ImeiNumber)
            If isIMEIsaved Then
                ' Call the web service
                SendSelectDeviceData(isEntityAttributeBenefitCheckIMEInumber, BenefitCheckNoAction)
            Else
                'if IMEI number is not saved then don't resolve the issue yet
                Exit Sub
            End If
        End If

        'resolve the issue "Previously Opened Claim(s) In Progress", check all the previous opened claims is closed or denied
        If Not bWaived AndAlso State.MyBO.IssueCode = "TIC_IP" Then
            Dim intclaimcnt = Claim.GetPreviousInProgressClaimCount(State.MyBO.ClaimId)
            If intclaimcnt > 0 Then
                MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage("PRE_CLAIM_IN_PROGRESS")) 'Previously Opened Claim(s) Still In Progress
                Exit Sub 'don't resolve the issue
            End If
        End If

        Dim errorCode As String = String.Empty
        Dim claimIssueStatus As String = SetClaimIssueStatus(bWaived)
        If claimIssueStatus = Codes.CLAIMISSUE_STATUS__REJECTED Then
            Select Case State.MyBO.IssueCode
                Case Codes.ISSUE_CODE__FMI_R1,
                     Codes.ISSUE_CODE__FMI_R2,
                     Codes.ISSUE_CODE__FMI_R3,
                     Codes.ISSUE_CODE__FMI_R4,
                     Codes.ISSUE_CODE__FMI_R5,
                     Codes.ISSUE_CODE__FMI_R6,
                     Codes.ISSUE_CODE__FMI_R7
                    errorCode = ClaimIssue.ProcessFraudMonitoringIndicatorRule(State.MyBO.Claim.Id,
                                                                               State.MyBO.Claim.Certificate.Id,
                                                                               State.MyBO.IssueCode)
            End Select
        End If
        If Not String.IsNullOrWhiteSpace(errorCode) Then
            MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage(errorCode))
            Exit Sub 'don't resolve the issue
        End If


        Dim oClaimIssueStatus As ClaimIssueStatus = State.MyBO.GetNewClaimIssueStatusChild()
        With oClaimIssueStatus
            .ClaimIssueId = State.MyBO.ClaimIssueId
            .ProcessedDate = DateTime.Now()
            .ProcessedBy = ElitaPlusIdentity.Current.ActiveUser.UserName
            .ClaimIssueStatusCId = LookupListNew.GetIdFromCode(LookupListCache.LK_CLAIM_ISSUE_STATUS, claimIssueStatus)
            .Comments = CheckComment(claimIssueStatus)
            If (bWaived) Then
                .IssueProcessReasonId = New Guid(hdnSelectedReason.Value)
                .Comments = txtWaiveComments.Text
            End If

            .Save()
        End With

        State.MyBO.StatusCode = claimIssueStatus
        State.MyBO.ProcessedDate = DateTime.Now()
        State.MyBO.ProcessedBy = ElitaPlusIdentity.Current.ActiveUser.UserName
        State.MyBO.Save()


        Try
            If (NavController IsNot Nothing) Then
                Select Case NavController.PrevNavState.Name
                    Case "CLAIM_ISSUES_INFORMATION"
                        Select Case claimIssueStatus
                            Case Codes.CLAIMISSUE_STATUS__OPEN, Codes.CLAIMISSUE_STATUS__PENDING
                                State.MyClaimBO.StatusCode = Codes.CLAIM_STATUS__PENDING
                                Dim c As Comment = State.MyClaimBO.AddNewComment()
                                c.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_SET_TO_PENDING)
                                c.Comments = TranslationBase.TranslateLabelOrMessage("MSG_CLAIM_PENDING_ISSUE_OPEN")
                            Case Codes.CLAIMISSUE_STATUS__REJECTED, Codes.CLAIMISSUE_STATUS__RESOLVED
                                State.MyClaimBO.StatusCode = Codes.CLAIM_STATUS__DENIED
                                Dim oEntityIssue As EntityIssue = New EntityIssue(oClaimIssueStatus.ClaimIssueId)
                                If Not oEntityIssue.IssueId = Guid.Empty Then
                                    Dim oIssue As Issue = New Issue(oEntityIssue.IssueId)
                                    If oIssue.DeniedReason IsNot Nothing Then
                                        State.MyClaimBO.DeniedReasonId = LookupListNew.GetIdFromExtCode(LookupListNew.LK_DENIED_REASON, oIssue.DeniedReason)
                                    Else
                                        State.MyClaimBO.DeniedReasonId = LookupListNew.GetIdFromCode(LookupListNew.LK_DENIED_REASON, Codes.REASON_DENIED_CLAIM_ISSUE_REJECTED)
                                    End If
                                Else
                                    State.MyClaimBO.DeniedReasonId = LookupListNew.GetIdFromCode(LookupListNew.LK_DENIED_REASON, Codes.REASON_DENIED_CLAIM_ISSUE_REJECTED)
                                End If
                                Dim c As Comment = State.MyClaimBO.AddNewComment()
                                c.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_DENIED)
                                c.Comments = TranslationBase.TranslateLabelOrMessage("MSG_CLAIM_DENIED_ISSUE_REJECTED")
                        End Select
                        State.MyClaimBO.Save()
                End Select
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        PopulateFormFromBOs()
        NavigateNext()
    End Sub

    Private Sub btnUndo_Click(sender As Object, e As EventArgs) Handles btnUndo.Click
        State.MyBO = State.MyPrevBO
        PopulateFormFromBOs()
    End Sub
    Private Function ConsequentialDamageReimbursementAmount(reimbursementValue As String) As Boolean
        Dim isReimbursementAmtUpdated As Boolean = False
        Dim reimbursementAmt As Decimal = New Decimal(0)
        Try
            reimbursementAmt = Decimal.Parse(reimbursementValue)
        Catch ex As Exception
            MasterPage.MessageController.AddError(ElitaPlus.Common.ErrorCodes.GUI_INVALID_REIMBURSEMENT_AMOUNT, True)
            Return isReimbursementAmtUpdated
        End Try

        If reimbursementAmt <= 0 Then
            MasterPage.MessageController.AddError(ElitaPlus.Common.ErrorCodes.GUI_INVALID_REIMBURSEMENT_AMOUNT, True)
            Return isReimbursementAmtUpdated
        End If

        Dim oCaseConseqDamage As CaseConseqDamage = New CaseConseqDamage(State.MyBO.ClaimId)

        If oCaseConseqDamage IsNot Nothing _
            AndAlso Not oCaseConseqDamage.CoverageConseqDamageId.Equals(Guid.Empty) Then
            Dim oCoverageConseqDamage As CoverageConseqDamage = New CoverageConseqDamage(oCaseConseqDamage.CoverageConseqDamageId)

            If oCoverageConseqDamage.LiabilityLimitPerIncident IsNot Nothing Then
                If oCoverageConseqDamage.LiabilityLimitPerIncident.Value >= reimbursementAmt Then
                    oCaseConseqDamage.RequestedAmount = reimbursementAmt
                    oCaseConseqDamage.ApprovedAmount = reimbursementAmt
                    oCaseConseqDamage.Save()
                    isReimbursementAmtUpdated = True
                Else
                    MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.ISSUE_CONSEQUENTIAL_DAMAGE_REIMBURSEMENT_AMOUNT_INCORRECT) _
                                                          & " " & oCoverageConseqDamage.LiabilityLimitPerIncident.Value.ToString(), False)
                    isReimbursementAmtUpdated = False
                End If
            Else
                MasterPage.MessageController.AddError(ElitaPlus.Common.ErrorCodes.COVERAGE_CONSEQUENTIAL_DAMAGE_LIABILITY_NOT_FOUND, True)
                isReimbursementAmtUpdated = False
            End If
        Else
            MasterPage.MessageController.AddError(ElitaPlus.Common.ErrorCodes.COVERAGE_CONSEQUENTIAL_DAMAGE_LIABILITY_NOT_FOUND, True)
            isReimbursementAmtUpdated = False
        End If
        Return isReimbursementAmtUpdated
    End Function
    Private Function UpdateClaimedEquip(imeiNumber As String) As Boolean
        Dim blnUpdateSuccess As Boolean = False

        If String.IsNullOrWhiteSpace(imeiNumber) Then
            MasterPage.MessageController.AddError(Message.MSG_IMEI_NUMBER_MANDATORY, True)
            blnUpdateSuccess = False
        ElseIf State.MyClaimBO.ClaimedEquipment Is Nothing Then
            MasterPage.MessageController.AddError(Message.MSG_CLAIM_EQUIPMENT_RECORD_NOT_FOUND, True)
            blnUpdateSuccess = False
        Else
            Dim objClaimedEquipment As New ClaimEquipment(State.MyClaimBO.ClaimedEquipment.Id)
            objClaimedEquipment.IMEINumber = imeiNumber
            objClaimedEquipment.Save()
            blnUpdateSuccess = True
        End If

        Return blnUpdateSuccess
    End Function
    Private Sub SendSelectDeviceData(checkBenefit As Boolean, benefitChkNoAction As Guid)
        Try
            Dim wsResponse As String
            Dim HasBenefitFalseAction As HasBenefitActionEnum = HasBenefitActionEnum.None
            If benefitChkNoAction.Equals(Guid.Empty) Then
                HasBenefitFalseAction = HasBenefitActionEnum.None
            ElseIf LookupListNew.GetCodeFromId(LookupListCache.LK_LANG_INDEPENDENT_YES_NO, benefitChkNoAction) = "Y" Then
                HasBenefitFalseAction = HasBenefitActionEnum.No
            Else
                HasBenefitFalseAction = HasBenefitActionEnum.Yes
            End If
            Dim client As LegacyBridgeServiceClient = Claim.GetLegacyBridgeServiceClient()
            wsResponse = WcfClientHelper.Execute(Of LegacyBridgeServiceClient, ILegacyBridgeService, Boolean)(
                                                                            client,
                                                                            New List(Of Object) From {New Headers.InteractiveUserHeader() With {.LanId = Authentication.CurrentUser.NetworkId}},
                                                                            Function(c As LegacyBridgeServiceClient)
                                                                                Return c.ExecuteClaimRecordingRules(State.MyClaimBO.Id.ToString, checkBenefit, HasBenefitFalseAction)
                                                                            End Function)
        Catch ex As Exception
            MasterPage.MessageController.AddError(ElitaPlus.Common.ErrorCodes.GUI_CLAIM_LEGACY_BRIDGE_SERVICE_ERR, True)
            Throw
        End Try
    End Sub
    Private Function CheckComment(statusCode As String, Optional ByVal commentText As String = "") As String
        Dim issueComment As String
        Dim issue As New Issue(State.MyBO.IssueId)
        For Each Item As IssueComment In issue.IssueNotesChildren
            If Item.Code.Trim() = statusCode.Trim() Then
                issueComment = Item.Text
                Exit For
            End If
        Next
        Return issueComment
    End Function

    Private Function UpdateComment(issuecomment As IssueComment, Optional ByVal commentText As String = "") As Guid
        Dim gCommentId As Guid = Guid.Empty

        Dim comment As New Comment()
        comment.CertId = State.MyCertificateBO.Id
        comment.ClaimId = State.MyBO.ClaimId
        comment.CallerName = State.MyBO.CreatedBy
        comment.CommentTypeId = issuecomment.IssueCommentTypeId
        If commentText.Length > 0 Then
            comment.Comments = commentText
        Else
            comment.Comments = issuecomment.Text
        End If
        comment.Save()
        gCommentId = comment.Id
        Return gCommentId
    End Function

    Private Function GetAnswerSelectedId(gSoftQuestionId As Guid) As Guid
        Dim gSelectedId As Guid = Guid.Empty
        For i As Integer = 0 To State.MyBO.ClaimIssueResponseList.Table.Rows.Count - 1
            'DEF-4069
            If Not Me.State.MyBO.ClaimIssueResponseList.Table.Rows(i).RowState = DataRowState.Deleted Then
                Dim gAnswerId As New Guid(CType(State.MyBO.ClaimIssueResponseList.Table.Rows(i)(COL_NAME_ANSWER_ID), Byte()))
                Dim oAnswer As New Answer(gAnswerId)
                If oAnswer.QuestionId = gSoftQuestionId Then
                    'If Not oAnswer.ListItemId = Guid.Empty Then
                    '    gSelectedId = oAnswer.ListItemId
                    'Else
                    Dim b As Byte() = GuidControl.HexToByteArray(State.MyBO.ClaimIssueResponseList.Table.Rows(i)(COL_NAME_ANSWER_VALUE).ToString())
                    gSelectedId = New Guid(b)
                    'End If
                    Exit For
                End If
            End If
        Next
        Return gSelectedId
    End Function

    Private Function GetAnswerTextValue(gSoftQuestionId As Guid) As String
        Dim sTextValue As String = String.Empty
        For i As Integer = 0 To State.MyBO.ClaimIssueResponseList.Table.Rows.Count - 1
            'DEF-4069
            If Not (Me.State.MyBO.ClaimIssueResponseList.Table.Rows(i).RowState = DataRowState.Deleted) Then
                Dim gAnswerId As New Guid(CType(State.MyBO.ClaimIssueResponseList.Table.Rows(i)(COL_NAME_ANSWER_ID), Byte()))
                Dim oAnswer As New Answer(gAnswerId)
                If oAnswer.QuestionId = gSoftQuestionId Then
                    sTextValue = State.MyBO.ClaimIssueResponseList.Table.Rows(i)(COL_NAME_ANSWER_VALUE).ToString()
                    Exit For
                End If
            End If
        Next
        Return sTextValue
    End Function

    Private Function GetClaimIssueResponseId(gSoftQuestionId As Guid) As Guid
        Dim gClaimIssueResponseId As Guid = Guid.Empty
        Dim gAnswerId As Guid = Guid.Empty
        For i As Integer = 0 To State.MyBO.ClaimIssueResponseList.Table.Rows.Count - 1
            'DEF-4069
            If Not (Me.State.MyBO.ClaimIssueResponseList.Table.Rows(i).RowState = DataRowState.Deleted) Then
                If State.MyBO.ClaimIssueResponseList.Table.Rows(i)(COL_NAME_ANSWER_ID) IsNot System.DBNull.Value Then
                    gAnswerId = New Guid(CType(State.MyBO.ClaimIssueResponseList.Table.Rows(i)(COL_NAME_ANSWER_ID), Byte()))
                    Dim oAnswer As New Answer(gAnswerId)
                    If oAnswer.QuestionId = gSoftQuestionId Then
                        gClaimIssueResponseId = New Guid(CType(State.MyBO.ClaimIssueResponseList.Table.Rows(i)(COL_NAME_CLAIM_ISSUE_RESPONSE_ID), Byte()))
                        Exit For
                    End If
                End If
            End If
        Next
        Return gClaimIssueResponseId
    End Function

    Private Function SetClaimIssueStatus(Optional ByVal bWaived As Boolean = False) As String
        Dim sStatus As String = STATUS_PENDING
        Dim sOutcome As String = ""
        Dim gSupportsClaimId As Guid
        Dim sSupportsClaim As String = ""
        Dim iQuestionCount As Integer = 0

        If bWaived = True Then
            sStatus = STATUS_WAIVED
        Else
            For i As Integer = 0 To State.MyBO.ClaimIssueResponseList.Table.Rows.Count - 1
                'DEF-4069
                If Not Me.State.MyBO.ClaimIssueResponseList.Table.Rows(i).RowState = DataRowState.Deleted Then
                    If State.ClaimIssueId.Equals(New Guid(CType(State.MyBO.ClaimIssueResponseList.Table.Rows(i)(COL_NAME_CLAIM_ISSUE_ID), Byte()))) Then
                        If State.MyBO.ClaimIssueResponseList.Table.Rows(i)(COL_NAME_SUPPORTS_CLAIM_ID) IsNot System.DBNull.Value Then
                            iQuestionCount = iQuestionCount + 1
                            gSupportsClaimId = New Guid(CType(State.MyBO.ClaimIssueResponseList.Table.Rows(i)(COL_NAME_SUPPORTS_CLAIM_ID), Byte()))
                            sSupportsClaim = LookupListNew.GetCodeFromId(LookupListCache.LK_LANG_INDEPENDENT_YES_NO, gSupportsClaimId).ToString()
                            Select Case sSupportsClaim
                                Case "Y"
                                    sOutcome += "Y"
                                Case "N"
                                    sOutcome += "N"
                                Case Else
                                    sOutcome += "P"
                            End Select
                        End If
                    End If
                End If
            Next
            If sOutcome.Contains("Y") AndAlso iQuestionCount = grdQuestions.Rows.Count Then
                sStatus = STATUS_RESOLVED
            End If
            If sOutcome.Contains("P") Then
                sStatus = STATUS_PENDING
            End If
            If sOutcome.Contains("N") Then
                sStatus = STATUS_REJECTED
            End If
        End If
        Return sStatus 'LookupListNew.GetIdFromCode(LookupListCache.LK_CLAIM_ISSUE_STATUS, sStatus)
    End Function

    Private Function SetEntityValue(sEntity As String, sAttribute As String, value As Object) As Label
        Dim oObject As Object
        Dim _typeObj As Type
        If (sEntity = EntityAttribute.EntityClaim) Then
            oObject = State.MyClaimBO
            _typeObj = State.MyClaimBO.GetType()
            If (value Is Nothing) Then
                _typeObj.GetProperty(sAttribute).SetValue(oObject, value, Nothing)
            ElseIf _typeObj.GetProperty(sAttribute).PropertyType = GetType(DateType) Then
                Dim dateValue As DateType = New DateType(DirectCast(value, Date))
                _typeObj.GetProperty(sAttribute).SetValue(oObject, dateValue, Nothing)
            ElseIf _typeObj.GetProperty(sAttribute).PropertyType = GetType(DateTime) Then
                Dim dateTime As DateTime = New DateTime(value.ToString())
                _typeObj.GetProperty(sAttribute).SetValue(oObject, dateTime, Nothing)
            Else
                _typeObj.GetProperty(sAttribute).SetValue(oObject, value, Nothing)
            End If
        End If

    End Function

    Private Function GetEntityValue(sEntity As String, sAttribute As String, Optional ByVal ddl As DropDownList = Nothing) As Label
        Dim oObject As Object
        Dim _typeObj As Type
        If (sEntity = EntityAttribute.EntityClaim) Then
            oObject = State.MyClaimBO
            _typeObj = State.MyClaimBO.GetType()
        Else
            Dim sAssembly As String = "Assurant.ElitaPlus.BusinessObjectsNew." + sEntity + ", Assurant.ElitaPlus.BusinessObjectsNew"
            _typeObj = Type.GetType(sAssembly)
            oObject = Activator.CreateInstance(_typeObj, State.MyClaimBO.Id, True)
        End If
        Dim sValue As String = ""
        Dim lblCompareValue As New Label
        'If Not _typeObj.GetProperty(sAttribute).GetValue(oObject, Nothing) Is Nothing Then
        If (_typeObj.GetProperty(sAttribute) IsNot Nothing) AndAlso (_typeObj.GetProperty(sAttribute).GetValue(oObject, Nothing) IsNot Nothing) Then
            lblCompareValue.ID = "lblCompareValue"
            If ddl IsNot Nothing Then
                If ddl.Items.Count > 0 Then
                    Dim listItem As System.Web.UI.WebControls.ListItem
                    listItem = ddl.Items.FindByValue(CType(_typeObj.GetProperty(sAttribute).GetValue(oObject, Nothing), Guid).ToString())
                    If listItem IsNot Nothing Then
                        sValue = listItem.Text
                    End If
                End If
            Else
                sValue = _typeObj.GetProperty(sAttribute).GetValue(oObject, Nothing).ToString()
            End If
        End If
        lblCompareValue.Text = sValue
        Return lblCompareValue
    End Function

    Private Sub NavigateNext()
        Try
            If (NavController IsNot Nothing) Then
                Select Case NavController.PrevNavState.Name
                    Case "CREATE_NEW_CLAIM"
                        NavController.Navigate(Me, "next", New NewClaimForm.Parameters(CType(State.MyClaimBO, Claim), Nothing, Nothing))
                    Case "NEW_CLAIM_DETAIL"
                        NavController.Navigate(Me, "claimDetail", New NewClaimForm.Parameters(CType(State.MyClaimBO, Claim), Nothing, Nothing))
                    Case "CLAIM_ISSUES_INFORMATION"
                        NavController.Navigate(Me, "claimIssues", New ClaimIssueForm.Parameters(CType(State.MyClaimBO, MultiAuthClaim)))
                    Case "CLAIM_DETAIL"
                        NavController.Navigate(Me, "claimForm", New ClaimForm.Parameters(State.MyClaimBO.Id))
                End Select
            Else
                ReturnToCallingPage()
                'Me.callPage(ClaimWizardForm.URL, New ClaimWizardForm.Parameters(ClaimWizardForm.ClaimWizardSteps.Step3, Nothing, Nothing, Me.State.MyClaimBO, True, False))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnReopen_Click(sender As Object, e As EventArgs) Handles btnReopenContinue.Click

        State.MyBO.StatusCode = Codes.CLAIMISSUE_STATUS__OPEN
        Dim oClaimIssueStatus As ClaimIssueStatus = State.MyBO.GetNewClaimIssueStatusChild()
        With oClaimIssueStatus
            .ClaimIssueId = State.MyBO.ClaimIssueId
            .ProcessedDate = DateTime.Now()
            .ProcessedBy = ElitaPlusIdentity.Current.ActiveUser.UserName
            .ClaimIssueStatusCId = LookupListNew.GetIdFromCode(LookupListCache.LK_CLAIM_ISSUE_STATUS, STATUS_OPEN)
            .Comments = txtComments.Text
            .IssueProcessReasonId = New Guid(hdnSelectedReason.Value)
            .Save()
        End With
        State.MyBO.Save()
        Try
            If (NavController IsNot Nothing) Then
                Select Case NavController.PrevNavState.Name
                    Case "CLAIM_ISSUES_INFORMATION"
                        Select Case LookupListNew.GetCodeFromId(LookupListCache.LK_CLAIM_ISSUE_STATUS, oClaimIssueStatus.ClaimIssueStatusCId)
                            Case Codes.CLAIMISSUE_STATUS__OPEN, Codes.CLAIMISSUE_STATUS__PENDING
                                State.MyClaimBO.StatusCode = Codes.CLAIM_STATUS__PENDING
                                Dim c As Comment = State.MyClaimBO.AddNewComment()
                                c.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_SET_TO_PENDING)
                                c.Comments = TranslationBase.TranslateLabelOrMessage("MSG_CLAIM_PENDING_ISSUE_OPEN")
                            Case Codes.CLAIMISSUE_STATUS__REJECTED, Codes.CLAIMISSUE_STATUS__RESOLVED
                                State.MyClaimBO.StatusCode = Codes.CLAIM_STATUS__DENIED
                                Dim oEntityIssue As EntityIssue = New EntityIssue(oClaimIssueStatus.ClaimIssueId)
                                If Not oEntityIssue.IssueId = Guid.Empty Then
                                    Dim oIssue As Issue = New Issue(oEntityIssue.IssueId)
                                    If oIssue.DeniedReason IsNot Nothing Then
                                        State.MyClaimBO.DeniedReasonId = LookupListNew.GetIdFromExtCode(LookupListNew.LK_DENIED_REASON, oIssue.DeniedReason)
                                    Else
                                        State.MyClaimBO.DeniedReasonId = LookupListNew.GetIdFromCode(LookupListNew.LK_DENIED_REASON, Codes.REASON_DENIED_CLAIM_ISSUE_REJECTED)
                                    End If
                                Else
                                    State.MyClaimBO.DeniedReasonId = LookupListNew.GetIdFromCode(LookupListNew.LK_DENIED_REASON, Codes.REASON_DENIED_CLAIM_ISSUE_REJECTED)
                                End If
                                Dim c As Comment = State.MyClaimBO.AddNewComment()
                                c.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_DENIED)
                                c.Comments = TranslationBase.TranslateLabelOrMessage("MSG_CLAIM_DENIED_ISSUE_REJECTED")
                        End Select
                        State.MyClaimBO.Save()
                        State.MyBO = CType(State.MyClaimBO.ClaimIssuesList.GetChild(State.ClaimIssueId), ClaimIssue)
                End Select
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        PopulateFormFromBOs()
      
    End Sub

    Private Function IsValidDate(DateToCheck As DateTime, DateToCompare As DateTime, sOperation As String) As Boolean
        Dim bReturn As Boolean = True
        Select Case sOperation
            Case "GreaterThanOrEqual"
                If DateToCheck >= DateToCompare Then
                    bReturn = False
                End If
            Case "LessThan"
                If DateToCheck < DateToCompare Then
                    bReturn = False
                End If
        End Select
        Return bReturn
    End Function

    Private Function IsValidDate(DateToCheck As DateType, DateToCompare As DateType, sOperation As String) As Boolean
        Dim bReturn As Boolean = True
        Select Case sOperation
            Case "GreaterThanOrEqual"
                If DateToCheck.Value >= DateToCompare.Value Then
                    bReturn = False
                End If
            Case "LessThan"
                If DateToCheck.Value < DateToCompare.Value Then
                    bReturn = False
                End If
        End Select
        Return bReturn
    End Function

    Private Sub ddlMult_OnSelectedDropChanged(aSrc As MultipleColumnDDLabelControl)

    End Sub

End Class
