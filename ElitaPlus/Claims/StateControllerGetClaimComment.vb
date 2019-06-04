Public Class StateControllerGetClaimComment
    Implements IStateController


    Public Sub Process(ByVal callingPage As System.Web.UI.Page, ByVal navCtrl As INavigationController) Implements IStateController.Process
        Dim newClaim As ClaimBase = CType(navCtrl.FlowSession(FlowSessionKeys.SESSION_CLAIM), ClaimBase)
        navCtrl.Navigate(callingPage, FlowEvents.EVENT_NEXT, New CommentForm.Parameters(Comment.GetLatestComment(newClaim)))
    End Sub
End Class
