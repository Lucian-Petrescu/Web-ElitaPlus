
Imports Assurant.ElitaPlus.BusinessObjectsNew.ClaimFulfillmentWebAppGatewayService

Public Interface IFullfillable

    Function GetFulfillmentDetails(ByVal claimNumber As String, ByVal companyCode As String) As FulfillmentDetails
End Interface
