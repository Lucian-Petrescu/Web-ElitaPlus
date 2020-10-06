Imports System.Runtime.Serialization

<Serializable()> Public Class DataBaseAccessException
    Inherits DatabaseException

#Region "Enums"
    Public Enum DatabaseAccessErrorType
        ReadErr
        WriteErr
        BusinessErr
    End Enum
#End Region

#Region "Private Members"
    Private _errType As DatabaseAccessErrorType
#End Region

#Region "Constructors"
    Public Sub New(errType As DatabaseAccessErrorType, Optional ByVal innerException As Exception = Nothing, _
    Optional ByVal userMsg As String = Nothing)
        MyBase.New("Error trying to access the Database", innerException)

        _errType = errType
        If userMsg Is Nothing Then
            Select Case _errType
                Case DatabaseAccessErrorType.ReadErr
                    Code = ErrorCodes.DB_READ_ERROR
                Case DatabaseAccessErrorType.WriteErr
                    If (Not innerException Is Nothing) Then
                        If (innerException.ToString.Contains("ORA-20999")) Then
                            Code = ErrorCodes.DB_ERROR_POSTAL_CODE_FORMAT_NOT_RIGHT   '"Postal Code format not correct"
                            'Throw New ElitaPlusException(Me.Code, Me.Code)
                            ' Throw New Exception(ErrorCodes.DB_ERROR_POSTAL_CODE_FORMAT_NOT_RIGHT)
                        ElseIf (innerException.ToString.Contains("ORA-20998")) Then
                            Code = ErrorCodes.DB_ERROR_COMUNA_NOT_FOUND   '"Comuna Entered NOt Found. Please enter the correct Comuna"
                            'Throw New ElitaPlusException(Me.Code, Me.Code)
                            'Throw New Exception(ErrorCodes.DB_ERROR_COMUNA_NOT_FOUND)
                        ElseIf (innerException.ToString.Contains("ORA-06512")) And (innerException.ToString.Contains("ELITA.GETPOSTALCODE")) Then
                            Code = ErrorCodes.DB_ERROR_POSTAL_CODE_FORMAT_NOT_RIGHT
                        Else
                            Code = ErrorCodes.DB_WRITE_ERROR
                        End If
                    Else
                        Code = ErrorCodes.DB_WRITE_ERROR
                    End If

            End Select
        Else
            Code = userMsg
        End If

    End Sub

    Protected Sub New(info As SerializationInfo, context As StreamingContext)
        MyBase.New(info, context)
    End Sub

#End Region

#Region "Properties"
    Public ReadOnly Property ErrorType() As DatabaseAccessErrorType
        Get
            Return _errType
        End Get
    End Property
#End Region




End Class
