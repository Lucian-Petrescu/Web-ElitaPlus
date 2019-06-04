Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.Runtime.Serialization

Namespace SpecializedServices.Tisa

    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/ClaimService/UpdateClaim", Name:="UpdateClaimReplacedWithRefubishedRequest"),
        ValidateUpdateClaimReplacedWithRefubishedActionAttribute>
    Public Class UpdateClaimReplacedWithRefubishedRequest
        Inherits UpdateRepairableClaimRequest

        <DataMember(IsRequired:=True, Name:="Condition")>
        Public Property Condition As DeviceConditionEnum

        <DataMember(IsRequired:=True, Name:="LossType")>
        Public Property LossType As LossTypeEnum

        <DataMember(IsRequired:=False, Name:="SimCardAmount")>
        Public Property SimCardAmount As Nullable(Of Decimal)

    End Class

    Public Class ValidateUpdateClaimReplacedWithRefubishedActionAttribute
        Inherits ValidationAttribute

        Protected Overrides Function IsValid(value As Object, validationContext As ValidationContext) As ValidationResult

            If validationContext.ObjectType = GetType(UpdateClaimReplacedWithRefubishedRequest) Then
                With DirectCast(validationContext.ObjectInstance, UpdateClaimReplacedWithRefubishedRequest)
                    If (.UpdateAction <> UpdateActionType.ReplacedRefurbished AndAlso
                         .UpdateAction <> UpdateActionType.NotRepairedMissingParts AndAlso
                         .UpdateAction <> UpdateActionType.Irreparable AndAlso
                        .UpdateAction <> UpdateActionType.DeviceDelivered) Then
                        Return New ValidationResult("Invalid Update Action for UpdateClaimReplacedWithRefubished Operation",
                                                    New List(Of String)({"UpdateAction"}))

                    Else
                        Return ValidationResult.Success
                    End If
                End With
            End If


        End Function

    End Class
End Namespace
