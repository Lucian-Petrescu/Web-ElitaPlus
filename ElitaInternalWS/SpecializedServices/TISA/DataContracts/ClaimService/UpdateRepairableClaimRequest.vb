Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.Runtime.Serialization

Namespace SpecializedServices.Tisa

    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/ClaimService/UpdateClaim", Name:="UpdateRepairableClaimRequest"),
     ValidateUpdateRepairableClaimActionAttribute>
    Public Class UpdateRepairableClaimRequest
        Inherits UpdateClaimRequest

        <DataMember(IsRequired:=False, Name:="AuthorizationNumber"), StringLength(10)>
        Public Property AuthorizationNumber As String

        <DataMember(Name:="AuthorizedAmount", IsRequired:=False), AuthorizedAmountMandatoryAttribute>
        Public Property AuthorizedAmount As Nullable(Of Decimal)

        <DataMember(IsRequired:=False, Name:="CoverageTypeCode"), StringLength(5)>
        Public Property CoverageTypeCode As String

        <DataMember(IsRequired:=False, Name:="ProblemDescription"), StringLength(500)>
        Public Property ProblemDescription As String

        <DataMember(IsRequired:=False, Name:="RepairDate")>
        Public Property RepairDate As Nullable(Of Date)

        <DataMember(IsRequired:=False, Name:="TechnicalReport"), StringLength(500)>
        Public Property TechnicalReport As String

        <DataMember(IsRequired:=False, Name:="SpecialInstructions"), StringLength(500)>
        Public Property SpecialInstructions As String

        <DataMember(IsRequired:=False, Name:="ServiceLevel"), StringLength(2, MinimumLength:=1)>
        Public Property ServiceLevel As String

        <DataMember(IsRequired:=True, Name:="Model"),
               Required(), StringLength(100, MinimumLength:=1)>
        Public Property Model As String

        <DataMember(IsRequired:=True, Name:="Manufacturer"),
               Required(), StringLength(255, MinimumLength:=1)>
        Public Property Manufacturer As String

        <DataMember(IsRequired:=True, Name:="SerialNumber"),
          Required(), StringLength(30, MinimumLength:=1)>
        Public Property SerialNumber As String
    End Class


    Public Class AuthorizedAmountMandatoryAttribute
        Inherits ValidationAttribute

        Protected Overrides Function IsValid(value As Object, validationContext As ValidationContext) As ValidationResult

            If validationContext.ObjectType = GetType(UpdateClaimReplacedWithRefubishedRequest) Then
                With DirectCast(validationContext.ObjectInstance, UpdateClaimReplacedWithRefubishedRequest)
                    If ((.Condition = DeviceConditionEnum.Refurbished) AndAlso ((Not .AuthorizedAmount.HasValue) OrElse (.AuthorizedAmount = 0))) Then
                        Return New ValidationResult("Authorized Amount is mandatory for Refurbished Devices",
                                                    New List(Of String)({validationContext.MemberName}))

                    Else
                        Return ValidationResult.Success
                    End If
                End With
            ElseIf validationContext.ObjectType = GetType(UpdateRepairableClaimRequest)

            End If


        End Function
    End Class

    Public Class ValidateUpdateRepairableClaimActionAttribute
        Inherits ValidationAttribute

        Protected Overrides Function IsValid(value As Object, validationContext As ValidationContext) As ValidationResult

            If validationContext.ObjectType = GetType(UpdateRepairableClaimRequest) Then
                With DirectCast(validationContext.ObjectInstance, UpdateRepairableClaimRequest)
                    If (.UpdateAction <> UpdateActionType.Repaired AndAlso
                        .UpdateAction <> UpdateActionType.RepairedBoardSwap AndAlso
                        .UpdateAction <> UpdateActionType.RepairedWithRefurbParts AndAlso
                        .UpdateAction <> UpdateActionType.WaitingForSpareParts) Then
                        Return New ValidationResult("Invalid Update Action for UpdateRepairableClaim Operation",
                                                    New List(Of String)({"UpdateAction"}))

                    Else
                        Return ValidationResult.Success
                    End If
                End With
            End If

        End Function

    End Class


End Namespace

