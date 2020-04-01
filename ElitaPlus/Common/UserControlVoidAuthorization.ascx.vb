Public Class UserControlVoidAuthorization
    Inherits System.Web.UI.UserControl

    Sub InitializeComponent()
        Try
            
            SetTranslations()

        Catch ex As Exception
            HandleLocalException(ex)
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    #Region "Properties"
    Public Property TranslationFunc As Func(Of String, String)
    Public Property HostMessageController As IMessageController
    #End Region

    Sub HandleLocalException(ex As Exception)
        Dim errorMessage As String = $"{ex.Message} {ex.StackTrace}"
        If HostMessageController IsNot Nothing Then
            HostMessageController.AddError(errorMessage, True)
        End If
    End Sub

    Private Sub SetTranslations()
        If TranslationFunc Is Nothing Then
            Throw New ArgumentException("The Translation lambda function not initialized")
        End If

        lblReasonClosed.Text = TranslationFunc("Reason_Closed")
        moVoidClaimAuthorizationLabel.Text = TranslationFunc("VOID_AUTHORIZATION")
        lblVoidComment.Text = TranslationFunc("AUTH_VOID_COMMENT")
        chkCloseClaim.Text = TranslationFunc("CLOSE_CLAIM")
       
    End Sub

End Class