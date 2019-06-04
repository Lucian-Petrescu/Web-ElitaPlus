
Public MustInherit Class SearchCriteriaType
    Private _searchType As SearchTypeEnum
    Private ReadOnly _searchDataType As SearchDataType

    Public ReadOnly Property DataType As SearchDataType
        Get
            Return _searchDataType
        End Get
    End Property

    Public Property SearchType() As SearchTypeEnum
        Get
            Return _searchType
        End Get
        Set(ByVal value As SearchTypeEnum)
            _searchType = value
        End Set
    End Property

    Public MustOverride Function IsValid() As Boolean
    Public MustOverride Function IsEmpty() As Boolean

    Protected Sub New(ByVal pSearchDataType As SearchDataType)
        Me._searchDataType = pSearchDataType
    End Sub
End Class

Public MustInherit Class SearchCriteriaType(Of TDataType)
    Inherits SearchCriteriaType

    Private _fromValue As TDataType
    Private _toValue As TDataType

    Public Property FromValue() As TDataType
        Get
            Return _fromValue
        End Get
        Set(ByVal value As TDataType)
            _fromValue = value
        End Set
    End Property

    Public Property ToValue() As TDataType
        Get
            If (SearchType = SearchTypeEnum.Between) Then
                Return _toValue
            Else
                Return Nothing
            End If
        End Get
        Set(ByVal value As TDataType)
            _toValue = value
        End Set
    End Property

    Public Overrides Function IsValid() As Boolean
        Dim returnValue As Boolean = True
        Dim firstParameterSpecified As Boolean = False
        Dim secondParameterSpecified As Boolean = False
        ' Check if First Parameter is specified
        If (Not (Me.FromValue Is Nothing OrElse Me.FromValue.ToString().Trim() = String.Empty)) Then
            firstParameterSpecified = True
        End If

        ' Check if Second Parameter is specified
        If (Me.SearchType = SearchType.Between) AndAlso (Not (Me.ToValue Is Nothing OrElse Me.ToValue.ToString().Trim() = String.Empty)) Then
            secondParameterSpecified = True
        End If

        ' Check if SearchType is Between then either both parameters are specified else none is specified
        If (Me.SearchType = SearchType.Between) Then
            returnValue = (firstParameterSpecified = secondParameterSpecified)
        End If

        Return returnValue
    End Function

    Protected Sub New(ByVal pSearchDataType As SearchDataType)
        MyBase.New(pSearchDataType)
    End Sub
End Class

Public NotInheritable Class SearchCriteriaStringType
    Inherits SearchCriteriaType(Of String)
    Public Overrides Function IsEmpty() As Boolean
        Return ((FromValue Is Nothing OrElse FromValue.Trim() = String.Empty) AndAlso (ToValue Is Nothing OrElse ToValue.Trim() = String.Empty))
    End Function

    Public Sub New()
        MyBase.New(SearchDataType.String)
    End Sub
End Class

Public NotInheritable Class SearchCriteriaStructType(Of TType As {Structure, IEquatable(Of TType)})
    Inherits SearchCriteriaType(Of Nullable(Of TType))
    Public Overrides Function IsEmpty() As Boolean
        Return (FromValue Is Nothing AndAlso ToValue Is Nothing)
    End Function

    Public Sub New(ByVal pSearchDataType As SearchDataType)
        MyBase.New(pSearchDataType)
    End Sub
End Class

#Region "Enum"

Public Enum SearchDataType
    [Date]
    [DateTime]
    [Amount]
    [Number]
    [String]
End Enum


Public Enum SearchTypeEnum
    Equals
    LessThan
    GreaterThan
    Between
    [Like]
End Enum
#End Region
