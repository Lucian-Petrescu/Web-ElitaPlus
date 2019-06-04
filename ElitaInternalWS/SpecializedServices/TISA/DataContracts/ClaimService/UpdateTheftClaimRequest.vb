Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.Runtime.Serialization

Namespace SpecializedServices.Tisa
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/ClaimService/UpdateClaim", Name:="UpdateTheftClaimRequest"),
        ValidateUpdateTheftClaimActionAttribute>
    Public Class UpdateTheftClaimRequest
        Inherits UpdateRepairableClaimRequest

        <DataMember(IsRequired:=True, Name:="Condition"), ValidateUpdateIrRepairableClaimActionAttribute>
        Public Property Condition As DeviceConditionEnum

        <DataMember(IsRequired:=True, Name:="LossType")>
        Public Property LossType As LossTypeEnum

        <DataMember(IsRequired:=False, Name:="SimCardAmount")>
        Public Property SimCardAmount As Nullable(Of Decimal)

    End Class

    Public Class ValidateUpdateTheftClaimActionAttribute
        Inherits ValidationAttribute

        Protected Overrides Function IsValid(value As Object, validationContext As ValidationContext) As ValidationResult

            If (validationContext.ObjectType = GetType(UpdateTheftClaimRequest)) Then
                With DirectCast(validationContext.ObjectInstance, UpdateTheftClaimRequest)
                    If (.UpdateAction <> UpdateActionType.DeviceDelivered) Then
                        Return New ValidationResult("Invalid Update Action for UpdateRepairableClaim Operation", New List(Of String)({"UpdateAction"}))
                    Else
                        Return ValidationResult.Success
                    End If
                End With
            End If

        End Function

    End Class

End Namespace