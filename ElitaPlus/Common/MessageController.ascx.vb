Public Class MessageController
    Inherits System.Web.UI.UserControl
    Implements IErrorController, IMessageController

    Private m_MessageType As IMessageController.MessageType

#Region "Constants"
    Public Const SUCCESS_CSS As String = "successMsg"
    Public Const WARNING_CSS As String = "warningMsg"
    Public Const INFORMATION_CSS As String = "infoMsg"
    Public Const ERROR_CSS As String = "errorMsg"
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

#Region "Generic"
    Public Sub AddMessage(ByVal message() As String, Optional ByVal translate As Boolean = True, _
        Optional ByVal messageType As IMessageController.MessageType = IMessageController.MessageType.None) _
        Implements IMessageController.AddMessage
        Dim i As Integer

        if message Is Nothing Then Return

        For i = 0 To message.Length - 1
            Dim translatedMessage As String
            If translate Then
                translatedMessage = TranslationBase.TranslateLabelOrMessage(message(i))
            Else
                translatedMessage = message(i)
            End If
            If Not Me.Text.Contains(translatedMessage & "<br/>") Then
                Me.Text &= translatedMessage
                If Not Me.Text.Length = 0 Then
                    Me.Text &= "<br/>"
                End If
            End If
        Next

        If (messageType <> IMessageController.MessageType.None) Then
            Me.Type = messageType
        End If
    End Sub

    Public Sub AddMessage(ByVal message As String, Optional ByVal translate As Boolean = True, _
        Optional ByVal messageType As IMessageController.MessageType = IMessageController.MessageType.None) _
        Implements IMessageController.AddMessage
        Dim translatedMessage As String
        Dim oPage As ElitaPlusPage
        Dim oTranslationProcess As TranslationProcess
        Dim nPrevCount As Integer

        If translate Then
            oPage = CType(Me.Page, ElitaPlusPage)
            oTranslationProcess = oPage.GetTranslationProcessReference()
            nPrevCount = oPage.MissingTranslationsCount
            translatedMessage = TranslationBase.TranslateLabelOrMessage(message)
            If oPage.MissingTranslationsCount > nPrevCount Then
                translatedMessage = message & ": is not translated"
            End If
        Else
            translatedMessage = message
        End If

        If Not Me.Text.Contains(translatedMessage & "<br/>") Then
            Me.Text &= translatedMessage
            If Not Me.Text.Length = 0 Then
                Me.Text &= "<br/>"
            End If
        End If

        If (messageType <> IMessageController.MessageType.None) Then
            Me.Type = messageType
        End If
    End Sub
#End Region

#Region "Error"
    Public Sub AddError(ByVal errorMessage As String, Optional ByVal translate As Boolean = True) _
        Implements IErrorController.AddError, IMessageController.AddError
        Me.AddMessage(errorMessage, translate, IMessageController.MessageType.Error)
    End Sub

    Public Sub AddError(ByVal errorMessages() As String, Optional ByVal translate As Boolean = True) _
        Implements IErrorController.AddError, IMessageController.AddError
        Me.AddMessage(errorMessages, translate, IMessageController.MessageType.Error)
    End Sub

    <Obsolete("Prefer to use method AddError over AddErrorAndShow")> _
    Public Sub AddErrorAndShow(ByVal errorMessage As String, Optional ByVal translate As Boolean = True) _
        Implements IErrorController.AddErrorAndShow
        Me.AddError(errorMessage, translate)
    End Sub

    <Obsolete("Prefer to use method AddError over AddErrorAndShow")> _
    Public Sub AddErrorAndShow(ByVal errorMessages() As String, Optional ByVal translate As Boolean = True) _
        Implements IErrorController.AddErrorAndShow
        Me.AddError(errorMessages, translate)
    End Sub
#End Region

#Region "Information"
    Public Sub AddInformation(ByVal infoMessage As String, Optional ByVal translate As Boolean = True) _
        Implements IMessageController.AddInformation
        Me.AddMessage(infoMessage, translate, IMessageController.MessageType.Information)
    End Sub

    Public Sub AddInformation(ByVal infoMessages() As String, Optional ByVal translate As Boolean = True) _
        Implements IMessageController.AddInformation
        Me.AddMessage(infoMessages, translate, IMessageController.MessageType.Information)
    End Sub
#End Region

#Region "Success"
    Public Sub AddSuccess(ByVal successMessage As String, Optional ByVal translate As Boolean = True) _
        Implements IMessageController.AddSuccess
        Me.AddMessage(successMessage, translate, IMessageController.MessageType.Success)
    End Sub

    Public Sub AddSuccess(ByVal successMessages() As String, Optional ByVal translate As Boolean = True) _
        Implements IMessageController.AddSuccess
        Me.AddMessage(successMessages, translate, IMessageController.MessageType.Success)
    End Sub
#End Region

#Region "Warning"
    Public Sub AddWarning(ByVal warningMessage As String, Optional ByVal translate As Boolean = True) _
        Implements IMessageController.AddWarning
        Me.AddMessage(warningMessage, translate, IMessageController.MessageType.Warning)
    End Sub

    Public Sub AddWarning(ByVal warningMessages() As String, Optional ByVal translate As Boolean = True) _
        Implements IMessageController.AddWarning
        Me.AddMessage(warningMessages, translate, IMessageController.MessageType.Warning)
    End Sub
#End Region

    Public Sub Clear() _
        Implements IErrorController.Clear, IMessageController.Clear
        Me.Text = String.Empty
    End Sub

    <Obsolete("This call is not necessary, this has been taken care automatically.")> _
    Public Sub Show() _
        Implements IErrorController.Show
        ShowHideMessage()
    End Sub

    <Obsolete("This call is not necessary, this has been taken care automatically.")> _
    Public Sub Hide() _
        Implements IErrorController.Hide
        ShowHideMessage()
    End Sub

    <Obsolete("This call is not necessary, this has been taken care automatically with the call to Clear method.")> _
    Public Sub Clear_Hide() _
        Implements IErrorController.Clear_Hide
        Clear()
    End Sub

    Public Property Text As String _
        Implements IErrorController.Text, IMessageController.Text
        Get
            Return Me.MessageLiteral.Text
        End Get
        Set(ByVal value As String)
            Me.MessageLiteral.Text = value
            ShowHideMessage()
        End Set
    End Property

    Public Property Type As IMessageController.MessageType _
        Implements IMessageController.Type
        Get
            Return Me.m_MessageType
        End Get
        Set(ByVal value As IMessageController.MessageType)
            Me.m_MessageType = value
            ChangeMessageCSS()
        End Set
    End Property

    Private Sub ChangeMessageCSS()
        Dim cssClassName As String
        Dim imagePath As String
        Select Case Me.Type
            Case IMessageController.MessageType.Error
                cssClassName = ERROR_CSS
                imagePath = "icon_error.png"
            Case (IMessageController.MessageType.Information)
                cssClassName = INFORMATION_CSS
                imagePath = "icon_info.png"
            Case IMessageController.MessageType.Success
                cssClassName = SUCCESS_CSS
                imagePath = "icon_success.png"
            Case IMessageController.MessageType.Warning
                cssClassName = WARNING_CSS
                imagePath = "icon_warning.png"
            Case Else
                cssClassName = ERROR_CSS
                imagePath = "icon_error.png"
        End Select
        moIconImage.Src = String.Format("{0}\App_Themes\{1}\Images\{2}", Request.ApplicationPath, Page.Theme, imagePath)
        Me.MessageBox.Attributes("class") = cssClassName
    End Sub

    Private Sub ShowHideMessage()
        Me.Visible = Not String.IsNullOrEmpty(Me.Text)
    End Sub
End Class