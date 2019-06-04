Imports System.Collections.Generic

Public Class SearchFieldHelper

    Private _fieldManager As List(Of SearchField)

    Public Sub New()
        _fieldManager = New List(Of SearchField)

    End Sub

    Public ReadOnly Property Fields As List(Of SearchField)
        Get
            Return _fieldManager
        End Get
    End Property

    Public ReadOnly Property Item(ByVal fieldName As String) As SearchField
        Get
            Dim el = (From n In _fieldManager
                      Where n.FieldName.Equals(fieldName)
                      Select n).FirstOrDefault
            Return el
        End Get
    End Property

    Public Function HasCriterias() As Boolean

        For Each field As SearchField In _fieldManager
            If Not (TypeOf field Is SortBySearchField) AndAlso Not field.IsEmpty Then
                Return True
            End If
        Next

        Return False
    End Function

End Class


Public MustInherit Class SearchField

    Protected _fieldValue As Object
    Protected _fieldName As String


    Public Sub New(fieldName As String)
        _fieldName = fieldName
    End Sub

    Public ReadOnly Property FieldName As String
        Get
            Return _fieldName
        End Get
    End Property

    Public Overridable ReadOnly Property ValueAsString() As String
        Get
            Throw New NotImplementedException()
        End Get
    End Property
    Public Overridable ReadOnly Property ValueAsInt32() As Int32
        Get
            Throw New NotImplementedException()
        End Get
    End Property
    Public Overridable ReadOnly Property ValueAsGuid() As Guid
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Public Sub SetValue(value As Object)
        _fieldValue = value
    End Sub


    Public Overridable ReadOnly Property IsEmpty As Boolean
        Get
            Return _fieldValue Is Nothing
        End Get
    End Property

    Public Sub Clear()
        _fieldValue = Nothing
    End Sub

    Public Overrides Function Equals(obj As Object) As Boolean

        If obj Is Nothing Then
            Return False
        ElseIf TypeOf obj Is SearchField Then
            Return _fieldName.Equals(obj.FieldName)
        Else
            Return False

        End If
    End Function


End Class

Public Class SortBySearchField
    Inherits SearchField

    Public Sub New(fieldName As String)
        MyBase.New(fieldName)
    End Sub
End Class
Public Class StringSearchField
    Inherits SearchField

    Public Sub New(fieldName As String)
        MyBase.New(fieldName)
    End Sub
End Class

Public Class IntegerSearchField
    Inherits SearchField

    Public Sub New(fieldName As String)
        MyBase.New(fieldName)
    End Sub
    Public Property FieldValue As Int32
        Get
            Return _fieldValue
        End Get

        Set(value As Int32)
            _fieldValue = value
        End Set
    End Property

End Class

Public Class GuidSearchField
    Inherits SearchField
    Public Sub New(fieldName As String)
        MyBase.New(fieldName)
    End Sub
    Public Property FieldValue As Guid
        Get
            Return _fieldValue
        End Get

        Set(value As Guid)
            _fieldValue = value
        End Set
    End Property

    Public Overrides ReadOnly Property IsEmpty As Boolean
        Get
            If _fieldValue Is Nothing Then
                Return True
            Else
                Dim el As Guid = CType(_fieldValue, Guid)
                Return el = Guid.Empty
            End If
        End Get
    End Property

End Class