Imports BN = Assurant.ElitaPlus.BusinessObjectsNew
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common

Partial Class PoliceReportForm
    Inherits ElitaPlusPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents ErrorCtrl As ErrorController
    Protected WithEvents moUserControlPoliceReport As UserControlPoliceReport
    Protected WithEvents moPoliceMultipleDrop As MultipleColumnDDLabelControl

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Constants"

    Public Const URL As String = "~/Claims/PoliceReportForm.aspx"
    'Public Const SERVICE_CENTER_ID_SESSION_KEY As String = "SERVICE_CENTER_ID_SESSION_KEY"

#End Region

#Region "Page Parameters"
    Public Class Parameters
        Public ClaimId As Guid
        Public mbIsClaimFormCalling As Boolean
        Public Sub New(ByVal ClaimId As Guid, Optional ByVal bIsClaimFormCalling As Boolean = False)
            Me.ClaimId = ClaimId
            mbIsClaimFormCalling = bIsClaimFormCalling
        End Sub
    End Class
#End Region

#Region "Page Return Type"

    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As PoliceReport
        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As PoliceReport)
            Me.LastOperation = LastOp
            Me.EditingBo = curEditingBo
        End Sub
    End Class

#End Region

    Public Enum InternalStates
        Regular
        ConfirmCreateWithAuthorizationRequired
        ConfirmAcknowledgementForClaimAdded
        ConfirmBackOnError
        ConfirmBackDuplicateRptNumber
    End Enum

#Region "Page State"

    Class MyState
        Public MyBO, ScreenSnapShotBO As PoliceReport
        Public InputParameters As Parameters
        Public IsEditMode As Boolean = False
        Public LastState As InternalStates = InternalStates.Regular
        Public LastErrMsg As String
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
    End Class

    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            'Return CType(MyBase.State, MyState)
            If Me.NavController.State Is Nothing Then
                Me.NavController.State = New MyState
                InitializeFromFlowSession()
            End If
            Return CType(Me.NavController.State, MyState)
        End Get
    End Property
    Protected Sub InitializeFromFlowSession()
        Try
            Me.State.InputParameters = CType(Me.NavController.ParametersPassed, Parameters)
            'If (Me.State.InputParameters Is Nothing) OrElse (Me.State.InputParameters.ClaimId.Equals(Guid.Empty)) Then
            '    Throw New DataNotFoundException
            'End If
            Me.State.MyBO = New PoliceReport(Me.State.InputParameters.ClaimId, True)
            'Me.State.IsComingFromClaimDetail = True

            'Me.State.ScreenSnapShotBO = New PoliceReport
            'Me.State.ScreenSnapShotBO.Clone(Me.State.MyBO)
        Catch dnfex As BN.DataNotFoundException
            ' its a valid scenario, there may or may NOT be a police report data for that claim id,
            ' so do NOT throw exception, just get a new object !
            Me.State.MyBO = New PoliceReport
            'Me.State.ScreenSnapShotBO = New PoliceReport
            'Me.State.ScreenSnapShotBO.Clone(Me.State.MyBO)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                'Get the id from the parent
                Dim pageParameters As Parameters = CType(Me.CallingParameters, Parameters)
                Try
                    Me.State.MyBO = New PoliceReport(pageParameters.ClaimId, True)

                    'Me.State.ScreenSnapShotBO = New PoliceReport
                    'Me.State.ScreenSnapShotBO.Clone(Me.State.MyBO)
                Catch dnfex As BN.DataNotFoundException
                    ' its a valid scenario, there may or may NOT be a police report data for that claim id,
                    ' so do NOT throw exception, just get a new object !
                    Me.State.MyBO = New PoliceReport

                    'Me.State.ScreenSnapShotBO = New PoliceReport
                    'Me.State.ScreenSnapShotBO.Clone(Me.State.MyBO)
                    'Me.UserCtrPoliceReport.Bind(Me.State.MyBO, Me.ErrorCtrl)
                End Try
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try

    End Sub

#End Region

#Region "Member Variables"

    'Private moReplacementData As ReplacementData
    'Private PoliceReportId As Guid

#End Region

#Region "Properties"
    Public ReadOnly Property UserCtrPoliceReport() As UserControlPoliceReport
        Get
            If moUserControlPoliceReport Is Nothing Then
                moUserControlPoliceReport = CType(FindControl("mcUserControlPoliceReport"), UserControlPoliceReport)
            End If
            Return moUserControlPoliceReport
        End Get
    End Property

    'Private ReadOnly Property ClaimInfo() As ReplacementData
    '    Get
    '        If moReplacementData Is Nothing Then
    '            moReplacementData = New ReplacementData(Me.State.InputParameters.ClaimId)
    '            Replacement.GetClaimInfo(moReplacementData)
    '        End If

    '        Return moReplacementData
    '    End Get
    'End Property
#End Region

#Region "Page Events"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Me.ErrorCtrl.Clear_Hide()
        'If moPoliceMultipleDrop Is Nothing Then
        '    moPoliceMultipleDrop = CType(UserCtrPoliceReport.FindControl("mPoliceMultipleColumnDropControl"), MultipleColumnDDLabelControl)
        'End If
        Try
            If Not Me.IsPostBack Then
                If Me.State.MyBO Is Nothing Then
                    Me.State.MyBO = New PoliceReport
                End If
                Trace(Me, "PolRep Id = " & GuidControl.GuidToHexString(Me.State.MyBO.Id))
                Me.PopulateFormFromBO()
                Me.EnableDisableFields()
            End If
            CheckIfComingFromSaveConfirm()
            'If Not Me.IsPostBack Then
            '    Me.AddLabelDecorations(Me.State.MyBO)

            'End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
        Me.ShowMissingTranslations(Me.ErrorCtrl)
    End Sub

#End Region

#Region "Controlling Logic"

    Protected Sub EnableDisableFields()

        'Me.SetEnabledForControlFamily(Me.LabelClaimNumber, False, True)   'Make it non-editable ALWAYS
        Me.SetEnabledForControlFamily(Me.TextboxClaimNumber, False, True)    'Make it non-editable ALWAYS
        Me.SetEnabledForControlFamily(Me.TextboxCertNumber, False, True)    'Make it non-editable ALWAYS
        Me.SetEnabledForControlFamily(Me.TextboxDealer, False, True)     'Make it non-editable ALWAYS
        If (Me.State.IsEditMode) Then

            'Enable any Editable fields
            'When in Edit Mode:
            'Hide the "Edit" button; Make the "Save" button Visible; Enable all the fields
            'When NOT in Edit Mode:
            'Hide the "Save" button; Make the "Edit" button Visible; Disable all the fields

            ControlMgr.SetVisibleForControlFamily(Me, Me.btnEdit_WRITE, False, True)
            ControlMgr.SetVisibleForControlFamily(Me, Me.btnSave_WRITE, True, True)

            Me.SetEnabledForControlFamily(Me.UserCtrPoliceReport, True, True)  'Make it editable            
        Else
            ControlMgr.SetVisibleForControlFamily(Me, Me.btnEdit_WRITE, True, True)
            ControlMgr.SetVisibleForControlFamily(Me, Me.btnSave_WRITE, False, True)
            Me.SetEnabledForControlFamily(Me.UserCtrPoliceReport, False, True)  'Make it non-editable
        End If

    End Sub

    Protected Sub PopulateFormFromBO(Optional ByVal bypassdualdropinitialization As Boolean = False)

        Try
            Me.PopulateControlFromBOProperty(Me.TextboxClaimNumber, Me.State.MyBO.ClaimNumber(Me.State.InputParameters.ClaimId))
            Me.PopulateControlFromBOProperty(Me.TextboxCertNumber, Me.State.MyBO.CertificateNumber(Me.State.InputParameters.ClaimId))
            Me.PopulateControlFromBOProperty(Me.TextboxDealer, Me.State.MyBO.DealerName(Me.State.InputParameters.ClaimId))
            'PopulateClaimPart()
            With Me.State
                'if star is needed to show besides the dual dropdown label - so send "true" for the parameter
                Me.UserCtrPoliceReport.Bind(.MyBO, Me.ErrorCtrl, True, bypassdualdropinitialization)
            End With
            Me.UserCtrPoliceReport.SetTheRequiredFields()
            'Me.UserCtrPoliceReport.ChangeEnabledControlProperty(False)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Protected Sub PopulatePoliceReportBOFromUserCtr()
        With Me.State.MyBO
            'Dim myParameters As Parameters = CType(Me.CallingParameters, Parameters)
            .ClaimId = Me.State.InputParameters.ClaimId
            ' here validate all the properties of police report BO
            Me.UserCtrPoliceReport.PopulateBOFromControl()
        End With
        'If Me.ErrCollection.Count > 0 Then
        '    Throw New PopulateBOErrorException
        'End If
    End Sub

    Protected Sub PopulateBOFromForm()
        Me.PopulatePoliceReportBOFromUserCtr()
    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value

        If State.LastState = InternalStates.ConfirmBackDuplicateRptNumber Then
            If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then 'save the changes
                Try
                    Me.State.MyBO.Save()
                    Me.State.LastState = InternalStates.Regular
                    Me.HiddenSaveChangesPromptResponse.Value = ""
                    Me.NavController.Navigate(Me, FlowEvents.EVENT_POLICEREPORT_UPDATED, Message.MSG_POLICE_REPORT_UPDATED)
                Catch ex As Exception
                    Me.HandleErrors(ex, Me.ErrorCtrl)
                End Try
            End If
            Exit Sub
        End If

        If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then

            If Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then 'AndAlso Me.State.IsComingFromClaimDetail 
                Me.State.MyBO.Save()
            End If
            Me.NavController.Navigate(Me, "back")
        ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
            If Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then 'AndAlso Me.State.IsComingFromClaimDetail Then
                Me.NavController.Navigate(Me, "back")
            End If
        End If

        'Clean after consuming the action
        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        Me.HiddenSaveChangesPromptResponse.Value = ""

    End Sub

#End Region


#Region "Button Clicks"

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click

        Try
            Me.PopulateBOFromForm()
            If (Me.State.MyBO.IsDirty) Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                Me.NavController.Navigate(Me, "back")
            End If
            'Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Nothing))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
            Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            Me.State.LastState = InternalStates.ConfirmBackOnError
            Me.State.LastErrMsg = Me.ErrorCtrl.Text
        End Try
    End Sub
    Private Sub btnEdit_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit_WRITE.Click

        'Introduce the logic to Enable/Disable the Editable fields here
        'Also make the relevant buttons Disabled/Invisible when in Edit mode - set a flag carried in the State

        'Set the Me.State.IsEditMode flag to "True" 
        Try
            'Me.MenuEnabled = False
            Me.State.IsEditMode = True
            'Me.PopulateFormFromBO()
            Me.EnableDisableFields()

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click

        Try
            Dim strOrigRptNumber As String = State.MyBO.ReportNumber, guidOrigStationID As Guid = State.MyBO.PoliceStationId
            Me.PopulateBOFromForm()
            If (Me.State.MyBO.IsDirty) Then
                If Me.State.MyBO.ReportNumber <> strOrigRptNumber OrElse guidOrigStationID <> State.MyBO.PoliceStationId Then
                    Dim lstClaim As Collections.Generic.List(Of String)
                    If State.MyBO.IsReportNumberInUser(lstClaim) Then
                        Dim sbMsg As New System.Text.StringBuilder
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
                        sbMsg.Append(TranslationBase.TranslateLabelOrMessage(Message.MSG_CONTINUE))
                        'save the state so that we know where we are when coming back from messagebox
                        State.LastState = InternalStates.ConfirmBackDuplicateRptNumber
                        Me.DisplayMessage(sbMsg.ToString, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse, False)
                    Else 'Save the record
                        Me.State.MyBO.Save()
                        Me.NavController.Navigate(Me, FlowEvents.EVENT_POLICEREPORT_UPDATED, Message.MSG_POLICE_REPORT_UPDATED)
                    End If
                Else
                    Me.State.MyBO.Save()
                    Me.NavController.Navigate(Me, FlowEvents.EVENT_POLICEREPORT_UPDATED, Message.MSG_POLICE_REPORT_UPDATED)
                End If
            Else
                Me.DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try

    End Sub

    Private Sub btnUndo_Write_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_Write.Click
        Try
            If Not Me.State.MyBO.IsNew Then 'AndAlso Not Me.State.ScreenSnapShotBO Is Nothing Then
                'Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
                Me.State.MyBO = New PoliceReport(Me.State.InputParameters.ClaimId, True)
            End If
            Me.PopulateFormFromBO()
        Catch dnfex As BN.DataNotFoundException
            ' its a valid scenario, there may or may NOT be a police report data for that claim id,
            ' so do NOT throw exception, just get a new object !
            Me.State.MyBO = New PoliceReport
            Me.PopulateFormFromBO()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

#End Region


#Region "Error Handling"

#End Region


End Class
