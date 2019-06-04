Imports Assurant.ElitaPlus.DataAccessInterface
Imports Assurant.ElitaPlus.DataEntities

Public NotInheritable Class DealerRepository(Of TType As {BaseEntity, IRecordCreateModifyInfo, IDealerEntity})
    Inherits Repository(Of TType, DealerContext)
    Implements IDealerRepository(Of TType)

    Public Sub New()
        MyBase.New(New Lazy(Of DealerContext)())
    End Sub

    Private Property m_dealerContext As DealerContext

    Public Function ComputeTax(ByVal pAmount As Decimal,
                               ByVal pDealerId As Nullable(Of Guid),
                               ByVal pCountryId As Nullable(Of Guid),
                               ByVal pCompanyTypeId As Nullable(Of Guid),
                               ByVal pTaxTypeId As Nullable(Of Guid),
                               ByVal pRegionId As Nullable(Of Guid),
                               ByVal pExpectedPremiumIsWpId As Nullable(Of Guid),
                               ByVal pProductTaxTypeId As Nullable(Of Guid),
                               ByVal pSalesDate As Date) As String Implements IDealerRepository(Of TType).ComputeTax
        Return MyBase.Context.ComputeTax(pAmount,
                                          pDealerId,
                                          pCountryId,
                                          pCompanyTypeId,
                                          pTaxTypeId,
                                          pRegionId,
                                          pExpectedPremiumIsWpId,
                                          pProductTaxTypeId,
                                          pSalesDate)
    End Function
End Class
