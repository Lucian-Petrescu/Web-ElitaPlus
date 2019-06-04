Imports System.Runtime.Serialization
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports System.ServiceModel
Imports System.ComponentModel.DataAnnotations

<DataContract(Name:="UpdateBatchNumber", Namespace:="http://elita.assurant.com/DataContracts/Claim")> _
Public Class UpdateBatchNumber
    Inherits ClaimOperation

    <DataMember(Name:="BatchNumber", IsRequired:=True),
    StringLength(10, ErrorMessage:="BatchNumber value exceeded maximum length of 10"),
    Required(ErrorMessage:="BatchNumber is Required")> _
    Public Property BatchNumber As String

    Public Overrides Sub Execute(ByVal oClaim As ClaimBase)
        If (oClaim.ClaimAuthorizationType <> ClaimAuthorizationType.Single) Then
            Throw New FaultException(Of ClaimOperationNotSupportedFault)(New ClaimOperationNotSupportedFault() With {.Message = "Multi-Auth Claims not supported", .OperationName = "UpdateBatchNumber"})
        End If

        Dim oSingleAuthClaim As Claim = DirectCast(oClaim, Claim)
        oSingleAuthClaim.BatchNumber = Me.BatchNumber

    End Sub

End Class