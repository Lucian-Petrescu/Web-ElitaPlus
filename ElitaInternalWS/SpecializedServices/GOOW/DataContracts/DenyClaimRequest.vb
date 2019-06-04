Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.Runtime.Serialization
Imports ElitaInternalWS.Claims

Namespace SpecializedServices.Goow
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Goow/DenyClaim", Name:="DenyClaimRequest")>
    Public Class DenyClaimRequest
        Inherits BaseClaimRequest
    End Class


    'Public Class ValidateUpdateDenyClaimActionAttribute
    '    Inherits ValidationAttribute

    '    Protected Overrides Function IsValid(value As Object, validationContext As ValidationContext) As ValidationResult
    '        With DirectCast(validationContext.ObjectInstance, DenyClaimRequest)
    '            If (
    '               .UpdateAction = UpdateActionType.Repaired) Then
    '                Return New ValidationResult("Invalid Update Action for DenyClaim Operation",
    '                                            New List(Of String)({"UpdateAction"}))
    '            Else
    '                Return ValidationResult.Success
    '            End If
    '        End With
    '    End Function

    'End Class
End Namespace