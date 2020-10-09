Public Class ProductSearchRequest
    Private Const c_maxSearchTermLength As Integer = 200
    Private Const c_defaultPageSize As Integer = 50

    Public ReadOnly Property DefaultPageSize() As Integer
        Get
            Return c_defaultPageSize
        End Get

    End Property

    Public Property CountryCode As String

    Public Property SearchTerm As String

    Public Property CategoryId As List(Of Integer)


    Public Property StartPrice As String

    Public Property EndPrice As String

    Public Property SortBy As SortBy

    Public Property SortType As SortType

    Public TotalNumberOfRecords As Integer

    Private Sub New()

    End Sub

    Public Sub New(p_countryCode As String,
                    p_searchTerm As String,
                    p_categoryID As List(Of Integer),
                    p_startPrice As String,
                    p_endPrice As String,
                    p_sortBy As SortBy,
                    P_sortType As SortType,
                    p_totalNumberofRecords As Integer)


        CountryCode = p_countryCode
        SearchTerm = p_searchTerm
        CategoryId = p_categoryID
        StartPrice = p_startPrice
        EndPrice = p_endPrice
        SortBy = p_sortBy
        SortType = P_sortType
        TotalNumberOfRecords = p_totalNumberofRecords

    End Sub



    Public ReadOnly Property IsValid() As Boolean
        Get
            Return (Not String.IsNullOrEmpty(CountryCode) AndAlso Not String.IsNullOrEmpty(SearchTerm) AndAlso SearchTerm.Length < (c_maxSearchTermLength + 1))
        End Get

    End Property

End Class
