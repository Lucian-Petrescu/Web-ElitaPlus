Public Structure PriceListDetailRecord
    Public Property ServiceClassId As Guid
    Public Property ServiceClassCode As String
    Public Property ServiceTypeId As Nullable(Of Guid)
    Public Property ServiceTypeCode As String
    Public Property ServiceLevelId As Nullable(Of Guid)
    Public Property ServiceLevelCode As String
    Public Property Price As Decimal
    Public Property VendorSku As String
    Public Property VendorSkuDescription As String
    Public Property PriceListId As Guid
    Public Property IsDeductibleId As Nullable(Of Guid)
    Public Property IsDeductibleCode As String
    Public Property IsStandardId As Nullable(Of Guid)
    Public Property IsStandardCode As String
    Public Property ContainsDeductibleId As Nullable(Of Guid)
    Public Property ContainsDeductibleCode As String
    Public Property Priority As String
    Public Property CurrencyId As Nullable(Of Guid)
    Public Property CurrencyCode As String
    Public Property Make As String
    Public Property Model As String

End Structure
