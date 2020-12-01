
Imports System.Collections.Generic
Imports Assurant.ElitaPlus.BusinessObjectsNew.ClaimFulfillmentWebAppGatewayService

Public Interface IFullfillable

    Function GetFulfillmentDetails(ByVal claimNumber As String, ByVal companyCode As String) As FulfillmentDetails
    Function SaveLogisticStages(claimNumber As String, companyCode As String, logisticStages As List(Of SelectedLogisticStage)) As UpdatedLogisticStagesResponse
End Interface
