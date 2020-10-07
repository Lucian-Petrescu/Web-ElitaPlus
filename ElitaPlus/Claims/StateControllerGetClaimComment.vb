Public Class StateControllerGetClaimComment
    Implements IStateController


    Public Sub Process(callingPage As System.Web.UI.Page, navCtrl As INavigationController) Implements IStateController.Process
        Dim newClaim As ClaimBase = CType(navCtrl.FlowSession(FlowSessionKeys.SESSION_CLAIM), ClaimBase)
        navCtrl.Navigate(callingPage, FlowEvents.EVENT_NEXT, New CommentForm.Parameters(Comment.GetLatestComment(newClaim)))
    End Sub
End Class
