Imports System.Runtime.Serialization
Imports Assurant.ElitaPlus.Common



<Serializable> Public NotInheritable Class BOValidationException
    Inherits ElitaPlusException

    Private errorList() As ValidationError
    Private boName As String
    Private sUniqueId As String



#Region " Constructors "


    Public Sub New(ByVal validationErrors() As ValidationError, ByVal businessObjectName As String, _
    Optional ByVal sUniqueId As String = "")
        MyBase.New("Validation Errors Found at BO: " & businessObjectName, ErrorCodes.BO_INVALID_DATA)
        Dim err As Assurant.Common.Validation.ValidationError
        Dim sProperties As String = String.Empty

        UniqueId = sUniqueId
        For Each err In validationErrors
            sProperties &= " Property: " & err.PropertyName()
            If Not err.OffendingValue Is Nothing Then
                sProperties &= " Value =" & err.OffendingValue.ToString & "="
            End If
        Next
        Header &= sProperties
        errorList = validationErrors
        boName = businessObjectName

    End Sub


    Public Sub New()
        Me.New("Validation Errors Found in BO")
    End Sub


    Public Sub New(ByVal message As String, Optional ByVal innerException As Exception = Nothing)
        MyBase.New("Validation Errors Found at BO", ErrorCodes.BO_INVALID_DATA, innerException)
    End Sub


    Protected Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
        MyBase.New(info, context)
    End Sub

    Public Sub New(ByVal message As String, ByVal errorCode As String, Optional ByVal innerException As Exception = Nothing)
        MyBase.New(message, errorCode, innerException)
    End Sub


#End Region



    Public ReadOnly Property BusinessObjectName As String
        Get
            Return boName
        End Get
    End Property


    Public Property UniqueId As String
        Get
            Return sUniqueId
        End Get
        Set
            sUniqueId = Value
        End Set
    End Property

    Public Function ValidationErrorList() As ValidationError()
        If errorList Is Nothing Then
            Return Nothing
        Else
            Return DirectCast(errorList.Clone(), ValidationError())
        End If
    End Function


End Class
