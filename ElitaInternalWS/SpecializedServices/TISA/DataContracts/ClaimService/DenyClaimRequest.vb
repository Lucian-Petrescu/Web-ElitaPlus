Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.Runtime.Serialization
Imports ElitaInternalWS.Claims

Namespace SpecializedServices.Tisa

    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/ClaimService/UpdateClaim", Name:="DenyClaimRequest"),
        ValidateUpdateDenyClaimActionAttribute>
    Public Class DenyClaimRequest
        Inherits UpdateClaimRequest



    End Class

    Public Class ValidateUpdateDenyClaimActionAttribute
        Inherits ValidationAttribute

        Protected Overrides Function IsValid(value As Object, validationContext As ValidationContext) As ValidationResult
            With DirectCast(validationContext.ObjectInstance, DenyClaimRequest)
                If (.UpdateAction <> UpdateActionType.UnderMFW AndAlso
                    .UpdateAction <> UpdateActionType.NoProblemFound) Then
                    Return New ValidationResult("Invalid Update Action for DenyClaim Operation",
                                                New List(Of String)({"UpdateAction"}))

                Else
                    Return ValidationResult.Success
                End If
            End With
        End Function

    End Class

End Namespace

