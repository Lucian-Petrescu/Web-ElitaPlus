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

    Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
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
        Public Sub New(ClaimId As Guid, Optional ByVal bIsClaimFormCalling As Boolean = False)
            Me.ClaimId = ClaimId
            mbIsClaimFormCalling = bIsClaimFormCalling
        End Sub
    End Class
#End Region

#Region "Page Return Type"

    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As PoliceReport
        Public Sub New(LastOp As DetailPageCommand, curEditingBo As PoliceReport)
            LastOperation = LastOp
            EditingBo = curEditingBo
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
            If NavController.State Is Nothing Then
                NavController.State = New MyState
                InitializeFromFlowSession()
            End If
            Return CType(NavController.State, MyState)
        End Get
    End Property
    Protected Sub InitializeFromFlowSession()
        Try
            State.InputParameters = CType(NavController.ParametersPassed, Parameters)
            'If (Me.State.InputParameters Is Nothing) OrElse (Me.State.InputParameters.ClaimId.Equals(Guid.Empty)) Then
            '    Throw New DataNotFoundException
            'End If
            State.MyBO = New PoliceReport(State.InputParameters.ClaimId, True)
            'Me.State.IsComingFromClaimDetail = True

            'Me.State.ScreenSnapShotBO = New PoliceReport
            'Me.State.ScreenSnapShotBO.Clone(Me.State.MyBO)
        Catch dnfex As BN.DataNotFoundException
            ' its a valid scenario, there may or may NOT be a police report data for that claim id,
            ' so do NOT throw exception, just get a new object !
            State.MyBO = New PoliceReport
            'Me.State.ScreenSnapShotBO = New PoliceReport
            'Me.State.ScreenSnapShotBO.Clone(Me.State.MyBO)
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
        Try
            If CallingParameters IsNot Nothing Then
                'Get the id from the parent
                Dim pageParameters As Parameters = CType(CallingParameters, Parameters)
                Try
                    State.MyBO = New PoliceReport(pageParameters.ClaimId, True)

                    'Me.State.ScreenSnapShotBO = New PoliceReport
                    'Me.State.ScreenSnapShotBO.Clone(Me.State.MyBO)
                Catch dnfex As BN.DataNotFoundException
                    ' its a valid scenario, there may or may NOT be a police report data for that claim id,
                    ' so do NOT throw exception, just get a new object !
                    State.MyBO = New PoliceReport

                    'Me.State.ScreenSnapShotBO = New PoliceReport
                    'Me.State.ScreenSnapShotBO.Clone(Me.State.MyBO)
                    'Me.UserCtrPoliceReport.Bind(Me.State.MyBO, Me.ErrorCtrl)
                End Try
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
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

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        ErrorCtrl.Clear_Hide()
        'If moPoliceMultipleDrop Is Nothing Then
        '    moPoliceMultipleDrop = CType(UserCtrPoliceReport.FindControl("mPoliceMultipleColumnDropControl"), MultipleColumnDDLabelControl)
        'End If
        Try
            If Not IsPostBack Then
                If State.MyBO Is Nothing Then
                    State.MyBO = New PoliceReport
                End If
                Trace(Me, "PolRep Id = " & GuidControl.GuidToHexString(State.MyBO.Id))
                PopulateFormFromBO()
                EnableDisableFields()
            End If
            CheckIfComingFromSaveConfirm()
            'If Not Me.IsPostBack Then
            '    Me.AddLabelDecorations(Me.State.MyBO)

            'End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
        ShowMissingTranslations(ErrorCtrl)
    End Sub

#End Region

#Region "Controlling Logic"

    Protected Sub EnableDisableFields()

        'Me.SetEnabledForControlFamily(Me.LabelClaimNumber, False, True)   'Make it non-editable ALWAYS
        SetEnabledForControlFamily(TextboxClaimNumber, False, True)    'Make it non-editable ALWAYS
        SetEnabledForControlFamily(TextboxCertNumber, False, True)    'Make it non-editable ALWAYS
        SetEnabledForControlFamily(TextboxDealer, False, True)     'Make it non-editable ALWAYS
        If (State.IsEditMode) Then

            'Enable any Editable fields
            'When in Edit Mode:
            'Hide the "Edit" button; Make the "Save" button Visible; Enable all the fields
            'When NOT in Edit Mode:
            'Hide the "Save" button; Make the "Edit" button Visible; Disable all the fields

            ControlMgr.SetVisibleForControlFamily(Me, btnEdit_WRITE, False, True)
            ControlMgr.SetVisibleForControlFamily(Me, btnSave_WRITE, True, True)

            SetEnabledForControlFamily(UserCtrPoliceReport, True, True)  'Make it editable            
        Else
            ControlMgr.SetVisibleForControlFamily(Me, btnEdit_WRITE, True, True)
            ControlMgr.SetVisibleForControlFamily(Me, btnSave_WRITE, False, True)
            SetEnabledForControlFamily(UserCtrPoliceReport, False, True)  'Make it non-editable
        End If

    End Sub

    Protected Sub PopulateFormFromBO(Optional ByVal bypassdualdropinitialization As Boolean = False)

        Try
            PopulateControlFromBOProperty(TextboxClaimNumber, State.MyBO.ClaimNumber(State.InputParameters.ClaimId))
            PopulateControlFromBOProperty(TextboxCertNumber, State.MyBO.CertificateNumber(State.InputParameters.ClaimId))
            PopulateControlFromBOProperty(TextboxDealer, State.MyBO.DealerName(State.InputParameters.ClaimId))
            'PopulateClaimPart()
            With State
                'if star is needed to show besides the dual dropdown label - so send "true" for the parameter
                UserCtrPoliceReport.Bind(.MyBO, ErrorCtrl, True, bypassdualdropinitialization)
            End With
            UserCtrPoliceReport.SetTheRequiredFields()
            'Me.UserCtrPoliceReport.ChangeEnabledControlProperty(False)
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Protected Sub PopulatePoliceReportBOFromUserCtr()
        With State.MyBO
            'Dim myParameters As Parameters = CType(Me.CallingParameters, Parameters)
            .ClaimId = State.InputParameters.ClaimId
            ' here validate all the properties of police report BO
            UserCtrPoliceReport.PopulateBOFromControl()
        End With
        'If Me.ErrCollection.Count > 0 Then
        '    Throw New PopulateBOErrorException
        'End If
    End Sub

    Protected Sub PopulateBOFromForm()
        PopulatePoliceReportBOFromUserCtr()
    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = HiddenSaveChangesPromptResponse.Value

        If State.LastState = InternalStates.ConfirmBackDuplicateRptNumber Then
            If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then 'save the changes
                Try
                    State.MyBO.Save()
                    State.LastState = InternalStates.Regular
                    HiddenSaveChangesPromptResponse.Value = ""
                    NavController.Navigate(Me, FlowEvents.EVENT_POLICEREPORT_UPDATED, Message.MSG_POLICE_REPORT_UPDATED)
                Catch ex As Exception
                    HandleErrors(ex, ErrorCtrl)
                End Try
            End If
            Exit Sub
        End If

        If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then

            If State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then 'AndAlso Me.State.IsComingFromClaimDetail 
                State.MyBO.Save()
            End If
            NavController.Navigate(Me, "back")
        ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
            If State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then 'AndAlso Me.State.IsComingFromClaimDetail Then
                NavController.Navigate(Me, "back")
            End If
        End If

        'Clean after consuming the action
        State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        HiddenSaveChangesPromptResponse.Value = ""

    End Sub

#End Region


#Region "Button Clicks"

    Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click

        Try
            PopulateBOFromForm()
            If (State.MyBO.IsDirty) Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                NavController.Navigate(Me, "back")
            End If
            'Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Nothing))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
            DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
            State.LastState = InternalStates.ConfirmBackOnError
            State.LastErrMsg = ErrorCtrl.Text
        End Try
    End Sub
    Private Sub btnEdit_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnEdit_WRITE.Click

        'Introduce the logic to Enable/Disable the Editable fields here
        'Also make the relevant buttons Disabled/Invisible when in Edit mode - set a flag carried in the State

        'Set the Me.State.IsEditMode flag to "True" 
        Try
            'Me.MenuEnabled = False
            State.IsEditMode = True
            'Me.PopulateFormFromBO()
            EnableDisableFields()

        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnSave_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnSave_WRITE.Click

        Try
            Dim strOrigRptNumber As String = State.MyBO.ReportNumber, guidOrigStationID As Guid = State.MyBO.PoliceStationId
            PopulateBOFromForm()
            If (State.MyBO.IsDirty) Then
                If State.MyBO.ReportNumber <> strOrigRptNumber OrElse guidOrigStationID <> State.MyBO.PoliceStationId Then
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
                        DisplayMessage(sbMsg.ToString, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse, False)
                    Else 'Save the record
                        State.MyBO.Save()
                        NavController.Navigate(Me, FlowEvents.EVENT_POLICEREPORT_UPDATED, Message.MSG_POLICE_REPORT_UPDATED)
                    End If
                Else
                    State.MyBO.Save()
                    NavController.Navigate(Me, FlowEvents.EVENT_POLICEREPORT_UPDATED, Message.MSG_POLICE_REPORT_UPDATED)
                End If
            Else
                DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", MSG_BTN_OK, MSG_TYPE_INFO)
            End If

        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try

    End Sub

    Private Sub btnUndo_Write_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_Write.Click
        Try
            If Not State.MyBO.IsNew Then 'AndAlso Not Me.State.ScreenSnapShotBO Is Nothing Then
                'Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
                State.MyBO = New PoliceReport(State.InputParameters.ClaimId, True)
            End If
            PopulateFormFromBO()
        Catch dnfex As BN.DataNotFoundException
            ' its a valid scenario, there may or may NOT be a police report data for that claim id,
            ' so do NOT throw exception, just get a new object !
            State.MyBO = New PoliceReport
            PopulateFormFromBO()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

#End Region


#Region "Error Handling"

#End Region


End Class
