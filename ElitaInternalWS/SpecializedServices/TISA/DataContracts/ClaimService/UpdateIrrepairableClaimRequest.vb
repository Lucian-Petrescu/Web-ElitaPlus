Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.Runtime.Serialization

Namespace SpecializedServices.Tisa

    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/ClaimService/UpdateClaim", Name:="UpdateIrrepairableClaimRequest")>
    Public Class UpdateIrrepairableClaimRequest
        Inherits UpdateRepairableClaimRequest

        '<DataMember(IsRequired:=True, Name:="Condition"), ValidateUpdateIrRepairableClaimActionAttribute>
        'Public Property Condition As DeviceConditionEnum

        '<DataMember(IsRequired:=True, Name:="LossType")>
        'Public Property LossType As LossTypeEnum

        '<DataMember(IsRequired:=True, Name:="SerialNumber"),
        '       Required(), StringLength(30, MinimumLength:=1)>
        'Public Property SerialNumber As String

        '<DataMember(IsRequired:=False, Name:="SimCardAmount")>
        'Public Property SimCardAmount As Nullable(Of Decimal)

    End Class

    Public Class ValidateUpdateIrRepairableClaimActionAttribute
        Inherits ValidationAttribute

        Protected Overrides Function IsValid(value As Object, validationContext As ValidationContext) As ValidationResult
            If validationContext.ObjectType = GetType(UpdateIrrepairableClaimRequest) Then
                With DirectCast(validationContext.ObjectInstance, UpdateIrrepairableClaimRequest)
                    If (.UpdateAction <> UpdateActionType.Irreparable AndAlso
                        .UpdateAction <> UpdateActionType.NotRepairedMissingParts) Then
                        Return New ValidationResult("Invalid Update Action for UpdateIrRepairableClaim Operation",
                                                    New List(Of String)({"UpdateAction"}))

                    Else
                        Return ValidationResult.Success
                    End If
                End With
            ElseIf validationContext.ObjectType = GetType(UpdateClaimReplacedWithRefubishedRequest)
                With DirectCast(validationContext.ObjectInstance, UpdateClaimReplacedWithRefubishedRequest)
                    If (.UpdateAction <> UpdateActionType.ReplacedRefurbished AndAlso
                        .UpdateAction <> UpdateActionType.Irreparable) Then
                        Return New ValidationResult("Invalid Update Action for UpdateIrRepairableClaim Operation",
                                                New List(Of String)({"UpdateAction"}))

                    Else
                        Return ValidationResult.Success
                End If
            End With
            End If

        End Function

    End Class

    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/ClaimService/UpdateClaim", Name:="DeviceConditionEnum")>
    Public Enum DeviceConditionEnum
        <EnumMember>
        [New] = 1
        <EnumMember>
        Refurbished = 2
    End Enum

    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/ClaimService/UpdateClaim", Name:="LossTypeEnum")>
    Public Enum LossTypeEnum
        <EnumMember>
        TotalLoss = 1
        <EnumMember>
        PartialLoss = 2
    End Enum

End Namespace

