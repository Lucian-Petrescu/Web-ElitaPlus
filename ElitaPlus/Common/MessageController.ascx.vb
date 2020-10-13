Public Class MessageController
    Inherits UserControl
    Implements IErrorController, IMessageController

    Private m_MessageType As IMessageController.MessageType

#Region "Constants"
    Public Const SUCCESS_CSS As String = "successMsg"
    Public Const WARNING_CSS As String = "warningMsg"
    Public Const INFORMATION_CSS As String = "infoMsg"
    Public Const ERROR_CSS As String = "errorMsg"
#End Region

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

#Region "Generic"
    Public Sub AddMessage(message() As String, Optional ByVal translate As Boolean = True, _
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
            If Not Text.Contains(translatedMessage & "<br/>") Then
                Text &= translatedMessage
                If Not Text.Length = 0 Then
                    Text &= "<br/>"
                End If
            End If
        Next

        If (messageType <> IMessageController.MessageType.None) Then
            Type = messageType
        End If
    End Sub

    Public Sub AddMessage(message As String, Optional ByVal translate As Boolean = True, _
        Optional ByVal messageType As IMessageController.MessageType = IMessageController.MessageType.None) _
        Implements IMessageController.AddMessage
        Dim translatedMessage As String
        Dim oPage As ElitaPlusPage
        Dim oTranslationProcess As TranslationProcess
        Dim nPrevCount As Integer

        If translate Then
            oPage = CType(Page, ElitaPlusPage)
            oTranslationProcess = oPage.GetTranslationProcessReference()
            nPrevCount = oPage.MissingTranslationsCount
            translatedMessage = TranslationBase.TranslateLabelOrMessage(message)
            If oPage.MissingTranslationsCount > nPrevCount Then
                translatedMessage = message & ": is not translated"
            End If
        Else
            translatedMessage = message
        End If

        If Not Text.Contains(translatedMessage & "<br/>") Then
            Text &= translatedMessage
            If Not Text.Length = 0 Then
                Text &= "<br/>"
            End If
        End If

        If (messageType <> IMessageController.MessageType.None) Then
            Type = messageType
        End If
    End Sub
#End Region

#Region "Error"
    Public Sub AddError(errorMessage As String, Optional ByVal translate As Boolean = True) _
        Implements IErrorController.AddError, IMessageController.AddError
        AddMessage(errorMessage, translate, IMessageController.MessageType.Error)
    End Sub

    Public Sub AddError(errorMessages() As String, Optional ByVal translate As Boolean = True) _
        Implements IErrorController.AddError, IMessageController.AddError
        AddMessage(errorMessages, translate, IMessageController.MessageType.Error)
    End Sub

    <Obsolete("Prefer to use method AddError over AddErrorAndShow")> _
    Public Sub AddErrorAndShow(errorMessage As String, Optional ByVal translate As Boolean = True) _
        Implements IErrorController.AddErrorAndShow
        AddError(errorMessage, translate)
    End Sub

    <Obsolete("Prefer to use method AddError over AddErrorAndShow")> _
    Public Sub AddErrorAndShow(errorMessages() As String, Optional ByVal translate As Boolean = True) _
        Implements IErrorController.AddErrorAndShow
        AddError(errorMessages, translate)
    End Sub
#End Region

#Region "Information"
    Public Sub AddInformation(infoMessage As String, Optional ByVal translate As Boolean = True) _
        Implements IMessageController.AddInformation
        AddMessage(infoMessage, translate, IMessageController.MessageType.Information)
    End Sub

    Public Sub AddInformation(infoMessages() As String, Optional ByVal translate As Boolean = True) _
        Implements IMessageController.AddInformation
        AddMessage(infoMessages, translate, IMessageController.MessageType.Information)
    End Sub
#End Region

#Region "Success"
    Public Sub AddSuccess(successMessage As String, Optional ByVal translate As Boolean = True) _
        Implements IMessageController.AddSuccess
        AddMessage(successMessage, translate, IMessageController.MessageType.Success)
    End Sub

    Public Sub AddSuccess(successMessages() As String, Optional ByVal translate As Boolean = True) _
        Implements IMessageController.AddSuccess
        AddMessage(successMessages, translate, IMessageController.MessageType.Success)
    End Sub
#End Region

#Region "Warning"
    Public Sub AddWarning(warningMessage As String, Optional ByVal translate As Boolean = True) _
        Implements IMessageController.AddWarning
        AddMessage(warningMessage, translate, IMessageController.MessageType.Warning)
    End Sub

    Public Sub AddWarning(warningMessages() As String, Optional ByVal translate As Boolean = True) _
        Implements IMessageController.AddWarning
        AddMessage(warningMessages, translate, IMessageController.MessageType.Warning)
    End Sub
#End Region

    Public Sub Clear() _
        Implements IErrorController.Clear, IMessageController.Clear
        Text = String.Empty
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
            Return MessageLiteral.Text
        End Get
        Set(value As String)
            MessageLiteral.Text = value
            ShowHideMessage()
        End Set
    End Property

    Public Property Type As IMessageController.MessageType _
        Implements IMessageController.Type
        Get
            Return m_MessageType
        End Get
        Set(value As IMessageController.MessageType)
            m_MessageType = value
            ChangeMessageCSS()
        End Set
    End Property

    Private Sub ChangeMessageCSS()
        Dim cssClassName As String
        Dim imagePath As String
        Select Case Type
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
        MessageBox.Attributes("class") = cssClassName
    End Sub

    Private Sub ShowHideMessage()
        Visible = Not String.IsNullOrEmpty(Text)
    End Sub
End Class