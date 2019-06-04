Imports Assurant.Elita.Fulfillment.BusinessRulesEngineInterface
Imports Assurant.Elita.Fulfillment
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports ElitaInternalWS.SpecializedServices.Ascn
Imports System.Collections.Generic
Imports Assurant.Elita.Fulfillment.ElitaRulesEngine.Clients
Imports Assurant.Elita.Fulfillment.ElitaRulesEngine
Imports System.Threading.Tasks

Public Class FulfillmentRulesHelper

    Public Shared Function ExecuteClaimsAuthorizedAmountRules(ClaimBO As Claim,
                                                                objClaimAuthDetail As ClaimAuthDetail,
                                                                partlistInfo As List(Of PartsInfo),
                                                                fulfillmentRuleEngine As IFulfillmentRulesClientProvider) As Contracts.ClaimAuthorizedAmountResponse
        'Getting all claimsfor claim's certificate
        Dim allClaimsDV As Certificate.CertificateClaimsDV = ClaimBO.Certificate.ClaimsForCertificate(ClaimBO.Certificate.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)

        Dim rulesRequest As New Contracts.ClaimAuthorizedAmountRequest

        With rulesRequest
            .Workspace.CompanyCode = ClaimBO.Company.Code
            .Workspace.DealerCode = ClaimBO.DealerCode
            .Workspace.CountryCode = New Country(ClaimBO.Company.CountryId).Code

            .PolicySummary = New Contracts.PolicyInfo()
            .PolicySummary.SalesPrice = ClaimBO.Certificate.SalesPrice
            If (allClaimsDV IsNot Nothing AndAlso allClaimsDV.Count > 0) Then
                Dim currentClaim As Contracts.ClaimInfo
                Dim row As DataRowView
                For claimIndex As Integer = 0 To allClaimsDV.Count - 1
                    currentClaim = New Contracts.ClaimInfo()
                    row = allClaimsDV(claimIndex)

                    With currentClaim
                        .ClaimId = New Guid(CType(row(Certificate.CertificateClaimsDV.COL_CLAIM_ID), Byte()))
                        .ClaimNumber = row(Certificate.CertificateClaimsDV.COL_CLAIM_NUMBER).ToString()
                        .PaymentAmount = If(row(Certificate.CertificateClaimsDV.COL_TOTAL_PAID) Is DBNull.Value, 0, CType(row(Certificate.CertificateClaimsDV.COL_TOTAL_PAID), Decimal))
                        .StatusCode = row(Certificate.CertificateClaimsDV.COL_STATUS_CODE).ToString()
                    End With

                    .PolicySummary.ClaimSummary.Add(currentClaim)
                Next
                'Dim claimsRequest = allCaims.
            End If


            If Not objClaimAuthDetail.ShippingAmount = Nothing Then
                .ClaimAuthorizedAmountsSummary.ShippingAmount = objClaimAuthDetail.ShippingAmount
            End If

            If Not objClaimAuthDetail.LaborAmount = Nothing Then
                .ClaimAuthorizedAmountsSummary.LaborAmount = objClaimAuthDetail.LaborAmount
            End If

            If Not objClaimAuthDetail.ServiceCharge = Nothing Then
                .ClaimAuthorizedAmountsSummary.ServiceCharge = objClaimAuthDetail.ServiceCharge
            End If

            If Not objClaimAuthDetail.TripAmount = Nothing Then
                .ClaimAuthorizedAmountsSummary.TripAmount = objClaimAuthDetail.TripAmount
            End If

            If Not objClaimAuthDetail.OtherAmount = Nothing Then
                .ClaimAuthorizedAmountsSummary.OtherAmount = objClaimAuthDetail.OtherAmount
            End If

            Dim partsTotal As Decimal
            Dim partsInfoDS As DataSet

            'Dim parts As List(Of Guid) = New List(Of Guid)()

            'If Part List Info provided in request (Not Nothing) is because user wants to update claim's part -- otherwise take claim's parts
            If partlistInfo Is Nothing OrElse Not partlistInfo.Any Then
                Dim partsInfoDV As PartsInfo.PartsInfoDV = PartsInfo.getSelectedList(ClaimBO.Id)

                If (partsInfoDV IsNot Nothing AndAlso partsInfoDV.Count > 0) Then
                    Dim partsRow As DataRowView
                    Dim partCost As Decimal
                    Dim i As Integer

                    For i = 0 To partsInfoDV.Count - 1
                        partsRow = partsInfoDV(i)
                        partCost = If(partsRow(PartsInfo.PartsInfoDV.COL_NAME_COST) Is DBNull.Value, 0, CType(partsRow(PartsInfo.PartsInfoDV.COL_NAME_COST), Decimal))
                        partsTotal += partCost
                    Next

                    partsInfoDS = New PriceListDetail().GetPriceListForParts(ClaimBO.Id, ClaimBO.CreatedDate.Value, partsInfoDV)

                End If
            Else
                partsTotal = partlistInfo.Sum(Function(part As PartsInfo) part.Cost.Value)

                partsInfoDS = New PriceListDetail().GetPriceListForParts(ClaimBO.Id, ClaimBO.CreatedDate.Value, partlistInfo)
            End If

            ''.PriceListSummary.PartsMaxCost = 10000

            If (partsInfoDS IsNot Nothing AndAlso partsInfoDS.Tables.Count = 1 AndAlso partsInfoDS.Tables(0).Rows.Count > 0) Then

                .PriceListSummary.PartsMaxCost = partsInfoDS.Tables(0).Rows.Cast(Of DataRow)().Sum(
                                                                                            Function(row As DataRow)
                                                                                                Return If(row("max_price") Is Nothing, 0, CType(row("max_price"), Decimal))
                                                                                            End Function
                                                                                          )
            End If


            .ClaimAuthorizedAmountsSummary.PartsAmount = partsTotal
        End With


        Dim ruleEngine As New BusinessRules.FulfillmentBusinessRulesEngine(fulfillmentRuleEngine)

        Dim t As Task(Of Contracts.ClaimAuthorizedAmountResponse)
        Try
            t = ruleEngine.ExecuteClaimAuthorizedAmountRules(rulesRequest)

            t.Wait()
        Catch aggregateEx As AggregateException
            Throw aggregateEx.InnerException
        Catch ex As Exception
            Throw ex
        End Try


        Return t.Result
    End Function


End Class
