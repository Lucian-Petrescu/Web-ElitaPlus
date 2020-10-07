Public Interface IErrorController
    Sub AddError(errorMessage As String, Optional ByVal translate As Boolean = True)
    Sub AddError(errorMessages() As String, Optional ByVal translate As Boolean = True)
    Sub AddErrorAndShow(errorMessage As String, Optional ByVal translate As Boolean = True)
    Sub AddErrorAndShow(errorMessages() As String, Optional ByVal translate As Boolean = True)
    Sub Clear()
    Sub Show()
    Sub Hide()
    Sub Clear_Hide()
    Property Text() As String
End Interface

Public Interface IMessageController
    Inherits IErrorController
    Sub AddMessage(message As String, Optional ByVal translate As Boolean = True, Optional ByVal messageType As IMessageController.MessageType = IMessageController.MessageType.None)
    Sub AddMessage(message() As String, Optional ByVal translate As Boolean = True, Optional ByVal messageType As IMessageController.MessageType = IMessageController.MessageType.None)
    Overloads Sub AddError(errorMessage As String, Optional ByVal translate As Boolean = True)
    Overloads Sub AddError(errorMessages() As String, Optional ByVal translate As Boolean = True)
    Sub AddInformation(informationMessage As String, Optional ByVal translate As Boolean = True)
    Sub AddInformation(informationMessages() As String, Optional ByVal translate As Boolean = True)
    Sub AddSuccess(successMessage As String, Optional ByVal translate As Boolean = True)
    Sub AddSuccess(successMessages() As String, Optional ByVal translate As Boolean = True)
    Sub AddWarning(warningMessage As String, Optional ByVal translate As Boolean = True)
    Sub AddWarning(warningMessages() As String, Optional ByVal translate As Boolean = True)
    Overloads Sub Clear()
    Overloads Property Text() As String
    Property Type() As MessageType

#Region "Enumerations"
    Enum MessageType
        None = 0
        [Error] = 1
        Information = 2
        Success = 3
        Warning = 4
    End Enum
#End Region

End Interface


