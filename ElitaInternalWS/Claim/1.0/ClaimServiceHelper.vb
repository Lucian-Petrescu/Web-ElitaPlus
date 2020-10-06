Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports System.ServiceModel

Friend Module ClaimServiceHelper

    Friend Function GetClaim(claimLookupRequest As ClaimLookup) As ClaimBase
        Dim oClaim As ClaimBase = Nothing

        Try
            If (claimLookupRequest.GetType() Is GetType(ClaimSerialNumberLookup)) Then
                Dim req As ClaimSerialNumberLookup = DirectCast(claimLookupRequest, ClaimSerialNumberLookup)
                oClaim = ClaimFacade.Instance.GetClaimBySerialNumber(Of ClaimBase)(req.DealerCode, req.SerialNumber)
            ElseIf (claimLookupRequest.GetType() Is GetType(ClaimNumberLookup)) Then
                Dim req As ClaimNumberLookup = DirectCast(claimLookupRequest, ClaimNumberLookup)
                oClaim = ClaimFacade.Instance.GetClaimByClaimNumber(Of ClaimBase)(req.CompanyCode, req.ClaimNumber)
            Else
                Throw New NotSupportedException()
            End If

        Catch ex As StoredProcedureGeneratedException
            Throw New FaultException(Of ClaimNotFoundFault)(New ClaimNotFoundFault() With {.ClaimSearch = claimLookupRequest})
        Catch ex As DataNotFoundException
            Throw New FaultException(Of ClaimNotFoundFault)(New ClaimNotFoundFault() With {.ClaimSearch = claimLookupRequest})
        Catch ex As Exception
            Throw New FaultException(Of ClaimNotFoundFault)(New ClaimNotFoundFault() With {.ClaimSearch = claimLookupRequest})
        End Try

        If (oClaim Is Nothing) Then
            Throw New FaultException(Of ClaimNotFoundFault)(New ClaimNotFoundFault() With {.ClaimSearch = claimLookupRequest})
        End If

        Return oClaim

    End Function


End Module
