Public Class StateControllerConfirmationPrompt
    Implements IStateController

#Region "Private Atributes"
    Private NavController As INavigationController
    Private CallingPage As ElitaPlusPage
#End Region

#Region "Internal State Management"
    Enum ProcessingStage
        AddingOKPrompt
        CheckingForUserAction
    End Enum

    Class MyState
        Public Stage As ProcessingStage = ProcessingStage.AddingOKPrompt
    End Class

    Public ReadOnly Property State() As MyState
        Get
            If Me.NavController.State Is Nothing Then
                Me.NavController.State = New MyState
            End If
            Return CType(Me.NavController.State, MyState)
        End Get
    End Property
#End Region
    Public Sub Process(ByVal callingPage As System.Web.UI.Page, ByVal navCtrl As INavigationController) Implements IStateController.Process
        Me.NavController = navCtrl
        Me.CallingPage = CType(callingPage, ElitaPlusPage)
        Select Case Me.State.Stage
            Case ProcessingStage.AddingOKPrompt
                Me.AddPrompt()
                Me.State.Stage = ProcessingStage.CheckingForUserAction
                'navCtrl.Navigate(Me.CallingPage, FlowEvents.EVENT_NEXT)
            Case ProcessingStage.CheckingForUserAction
                navCtrl.Navigate(Me.CallingPage, FlowEvents.EVENT_NEXT)
        End Select
    End Sub

    Sub AddPrompt()
        CType(CallingPage, ElitaPlusPage).DisplayMessageWithSubmit(CType(Me.NavController.ParametersPassed, String), "", ElitaPlusPage.MSG_BTN_OK, ElitaPlusPage.MSG_TYPE_INFO)
    End Sub
End Class
