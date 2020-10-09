Public Class PriceListSearch
    Private _action As IPriceListSearch = Nothing

    Public Property action As IPriceListSearch
        Get
            Return _action
        End Get
        Set
            _action = value
        End Set
    End Property

    Public Function GetPriceList(oPriceListSearch As PriceListSearchDC) As DataSet
        Return action.GetPriceList(oPriceListSearch)
    End Function

End Class
