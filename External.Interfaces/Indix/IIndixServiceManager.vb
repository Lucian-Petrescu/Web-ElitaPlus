Public Interface IIndixServiceManager
    Function GetProductDetails(request As ProductDetailsRequest) As ProductDetailsResponse

    'REQ-6230
    Function GetProducts(request As ProductSearchRequest, ByRef totalNumberOfRecords As Integer) As IEnumerable(Of ProductDetail)
End Interface
