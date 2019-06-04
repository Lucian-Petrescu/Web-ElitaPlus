Public Interface IErrorController
    Sub AddError(ByVal errorMessage As String, Optional ByVal translate As Boolean = True)
    Sub AddError(ByVal errorMessages() As String, Optional ByVal translate As Boolean = True)
    Sub AddErrorAndShow(ByVal errorMessage As String, Optional ByVal translate As Boolean = True)
    Sub AddErrorAndShow(ByVal errorMessages() As String, Optional ByVal translate As Boolean = True)
    Sub Clear()
    Sub Show()
    Sub Hide()
    Sub Clear_Hide()
    Property Text() As String
End Interface

Public Interface IMessageController
    Inherits IErrorController
    Sub AddMessage(ByVal message As String, Optional ByVal translate As Boolean = True, Optional ByVal messageType As IMessageController.MessageType = IMessageController.MessageType.None)
    Sub AddMessage(ByVal message() As String, Optional ByVal translate As Boolean = True, Optional ByVal messageType As IMessageController.MessageType = IMessageController.MessageType.None)
    Overloads Sub AddError(ByVal errorMessage As String, Optional ByVal translate As Boolean = True)
    Overloads Sub AddError(ByVal errorMessages() As String, Optional ByVal translate As Boolean = True)
    Sub AddInformation(ByVal informationMessage As String, Optional ByVal translate As Boolean = True)
    Sub AddInformation(ByVal informationMessages() As String, Optional ByVal translate As Boolean = True)
    Sub AddSuccess(ByVal successMessage As String, Optional ByVal translate As Boolean = True)
    Sub AddSuccess(ByVal successMessages() As String, Optional ByVal translate As Boolean = True)
    Sub AddWarning(ByVal warningMessage As String, Optional ByVal translate As Boolean = True)
    Sub AddWarning(ByVal warningMessages() As String, Optional ByVal translate As Boolean = True)
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


