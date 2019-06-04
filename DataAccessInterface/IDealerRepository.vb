Imports Assurant.ElitaPlus.DataEntities

Public Interface IDealerRepository(Of TEntity As {BaseEntity, IDealerEntity})
    Inherits IRepository(Of TEntity)

    Function ComputeTax(ByVal pAmount As Decimal,
                               ByVal pDealerId As Nullable(Of Guid),
                               ByVal pCountryId As Nullable(Of Guid),
                               ByVal pCompanyTypeId As Nullable(Of Guid),
                               ByVal pTaxTypeId As Nullable(Of Guid),
                               ByVal pRegionId As Nullable(Of Guid),
                               ByVal pExpectedPremiumIsWpId As Nullable(Of Guid),
                               ByVal pProductTaxTypeId As Nullable(Of Guid),
                               ByVal pSalesDate As Date) As String
End Interface
