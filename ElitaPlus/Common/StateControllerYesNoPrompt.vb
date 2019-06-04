Public Class StateControllerYesNoPrompt
    Implements IStateController

#Region "Constants"
    Public HIDDENFIELDNAME As String = "__YesNoResponse__"
    Public HIDDENFIELDNAME_UNIQUEID As String = "ctl00$__YesNoResponse__"
#End Region
#Region "Private Atributes"
    Private NavController As INavigationController
    Private CallingPage As ElitaPlusPage
#End Region

#Region "Internal State Managment"
    Enum ProcessingStage
        AddingYesNoPrompt
        CheckingForUserSelection
    End Enum

    Class Parameters
        Public PromptMessage As String
        Public TranslateMessage As Boolean = True
        Public UseDefault As Boolean = False
        Public IsDefaultValueYes As Boolean = False

        Public Sub New(ByVal strMsg As String, Optional ByVal translate As Boolean = True, _
                       Optional ByVal useDefatultValue As Boolean = False, _
                       Optional ByVal defaultYesValue As Boolean = False)
            Me.PromptMessage = strMsg
            Me.TranslateMessage = translate
            Me.UseDefault = useDefatultValue
            Me.IsDefaultValueYes = defaultYesValue
        End Sub
    End Class

    Class MyState
        Public PromptMessage As String
        Public TranslateMessage As Boolean = True
        Public Stage As ProcessingStage = ProcessingStage.AddingYesNoPrompt
        Public UseDefault As Boolean = False
        Public IsDefaultValueYes As Boolean = False
    End Class

    Public ReadOnly Property State() As MyState
        Get
            If Me.NavController.State Is Nothing Then
                Me.NavController.State = New MyState

                '    Me.State.PromptMessage = CType(Me.NavController.ParametersPassed, String)
                If Not Me.NavController.ParametersPassed Is Nothing Then
                    Dim params As Parameters = CType(Me.NavController.ParametersPassed, Parameters)
                    Me.State.PromptMessage = params.PromptMessage
                    Me.State.TranslateMessage = params.TranslateMessage
                    Me.State.UseDefault = params.UseDefault
                    Me.State.IsDefaultValueYes = params.IsDefaultValueYes
                Else
                    Me.State.PromptMessage = ""
                    Me.State.TranslateMessage = False
                    Me.State.UseDefault = False
                    Me.State.IsDefaultValueYes = False
                End If
                'End If
            End If
            Return CType(Me.NavController.State, MyState)
        End Get
    End Property
#End Region
    Public Sub Process(ByVal callingPage As System.Web.UI.Page, ByVal navCtrl As INavigationController) Implements IStateController.Process
        Me.NavController = navCtrl
        Me.CallingPage = CType(callingPage, ElitaPlusPage)
        Select Case Me.State.Stage
            Case ProcessingStage.AddingYesNoPrompt
                If (Me.State.UseDefault) Then
                    Me.State.Stage = ProcessingStage.CheckingForUserSelection
                    Me.Process(callingPage, navCtrl)
                    Exit Sub
                Else
                    Me.AddPrompt()
                    Me.State.Stage = ProcessingStage.CheckingForUserSelection
                End If
            Case ProcessingStage.CheckingForUserSelection
                Dim response As String
                If (Me.State.UseDefault) Then
                    If (Me.State.IsDefaultValueYes) Then
                        response = ElitaPlusPage.MSG_VALUE_YES
                    Else
                        response = ElitaPlusPage.MSG_VALUE_NO
                    End If
                Else
                    response = Me.GetPromptResponse
                End If
                If response = ElitaPlusPage.MSG_VALUE_YES Then
                    Me.NavController.Navigate(Me.CallingPage, FlowEvents.EVENT_YES)
                ElseIf response = ElitaPlusPage.MSG_VALUE_NO Then

                    'REQ-346 - Added check to see if we're in the Customer_email prompt.  If so, and user selects "N", needs to navigate to sentemail if service order email has been chosen.
                    If Me.NavController.CurrentNavState.Name = "CUSTOMER_EMAIL_YES_NO_PROMPT" _
                        AndAlso Not Me.NavController.FlowSession(FlowSessionKeys.SESSION_EMAIL_SENT) Is Nothing _
                        AndAlso Me.NavController.FlowSession(FlowSessionKeys.SESSION_EMAIL_SENT).ToString = "Y" Then
                        Me.NavController.Navigate(Me.CallingPage, "sent_email", Message.MSG_EMAIL_SENT)

                    Else
                        Me.NavController.Navigate(Me.CallingPage, FlowEvents.EVENT_NO)
                    End If
                Else
                    Me.AddPrompt()
                    'Throw New GUIException("Unexpected YesNo Prompt Response", Assurant.ElitaPlus.Common.ErrorCodes.GUI_UNEXPECTED_PROMPT_RESPONSE)
                End If
        End Select
    End Sub

    Sub AddPrompt()

        Dim hiddenField As New HtmlControls.HtmlInputHidden
        hiddenField.ID = HIDDENFIELDNAME
        If Me.CallingPage.IsNewUI Then
            Me.CallingPage.MasterPage.FindControl("Form1").Controls.Add(hiddenField)
        Else
            Me.CallingPage.FindControl("Form1").Controls.Add(hiddenField)
        End If
        hiddenField.Name = HIDDENFIELDNAME
        hiddenField.EnableViewState = True
        hiddenField.Value = "HHHHH"
        CType(Me.CallingPage, ElitaPlusPage).DisplayMessage(Me.State.PromptMessage, "", ElitaPlusPage.MSG_BTN_YES_NO, ElitaPlusPage.MSG_TYPE_CONFIRM, hiddenField, Me.State.TranslateMessage)
    End Sub

    ReadOnly Property GetPromptResponse() As String
        Get
            If Me.CallingPage.IsNewUI Then
                Return Me.CallingPage.MasterPage.Request.Form.Item(Me.HIDDENFIELDNAME_UNIQUEID)
            Else
                Return Me.CallingPage.Request.Form.Item(Me.HIDDENFIELDNAME)
            End If
        End Get
    End Property



End Class
