Public Class ErrorResponse
    Public Property ErrorCode() As String
        Get
            Return m_ErrorCode
        End Get
        Set
            m_ErrorCode = Value
        End Set
    End Property
    Private m_ErrorCode As String
    Public Property ErrorMessage() As String
        Get
            Return m_ErrorMessage
        End Get
        Set
            m_ErrorMessage = Value
        End Set
    End Property
    Private m_ErrorMessage As String

End Class
