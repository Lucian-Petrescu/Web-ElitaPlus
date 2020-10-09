Public Class StateControllerPayClaim
    Implements IStateController


    Public Sub Process(callingPage As Page, navCtrl As INavigationController) Implements IStateController.Process
        Dim newClaim As Claim = CType(navCtrl.FlowSession(FlowSessionKeys.SESSION_CLAIM), Claim)
        navCtrl.Navigate(callingPage, FlowEvents.EVENT_NEXT, New PayClaimForm.Parameters(newClaim.Id, False))
    End Sub
End Class


Public Class StateControllerPayClaimPrompt
    Implements IStateController


    Public Sub Process(callingPage As Page, navCtrl As INavigationController) Implements IStateController.Process
        navCtrl.Navigate(callingPage, FlowEvents.EVENT_NEXT, New StateControllerYesNoPrompt.Parameters(Message.MSG_PROMPT_FOR_PAYING_CLAIM_FROM_NEW_CLAIM_FORM))
    End Sub
End Class