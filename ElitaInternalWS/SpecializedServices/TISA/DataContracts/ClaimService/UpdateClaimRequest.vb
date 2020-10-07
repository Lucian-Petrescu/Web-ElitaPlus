Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.Runtime.CompilerServices
Imports System.Runtime.Serialization

Namespace SpecializedServices.Tisa
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/ClaimService/UpdateClaim", Name:="UpdateClaimRequest")>
    Public Class UpdateClaimRequest

        <DataMember(IsRequired:=True, Name:="ClaimNumber"), Required(), StringLength(20, MinimumLength:=1)>
        Public Property ClaimNumber As String

        <DataMember(IsRequired:=False, Name:="ServiceCenterCode"), StringLength(10)>
        Public Property ServiceCenterCode As String

        <DataMember(IsRequired:=True, Name:="CompanyCode"), StringLength(5, MinimumLength:=1)>
        Public Property CompanyCode As String

        <DataMember(IsRequired:=True, Name:="UpdateAction")>
        Public Property UpdateAction As UpdateActionType


        <DataMember(IsRequired:=False, Name:="ExtendedStatuses")>
        Public Property ExtendedStatuses As List(Of ExtendedStatus)

    End Class

    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/ClaimService/UpdateClaim", Name:="ExtendedStatus")>
    Public Class ExtendedStatus
        <DataMember(IsRequired:=False, Name:="Code")>
        Public Property Code As String

        <DataMember(IsRequired:=True, Name:="StatusDate")>
        Public Property StatusDate As Nullable(Of Date)
    End Class



    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/ClaimService/UpdateClaim", Name:="UpdateActionType")>
    Public Enum UpdateActionType
        <EnumMember>
        UnderMFW = 1                'RUOEM 
        <EnumMember>
        NoProblemFound = 2          'NVF 
        <EnumMember>
        Repaired = 3                'REPRD
        <EnumMember>
        RepairedBoardSwap = 4       'REPBSWAP
        <EnumMember>
        RepairedWithRefurbParts = 5 'TRAFALREP
        <EnumMember>
        WaitingForSpareParts = 6    'EQDEM
        <EnumMember>
        ReplacedRefurbished = 7     'TRAIRR
        <EnumMember>
        Irreparable = 8             'CHLIRR
        <EnumMember>
        NotRepairedMissingParts = 9 'FALREP
        <EnumMember>
        DeviceDelivered = 10        'EQDE
    End Enum


    Friend Module UpdateClaimExtensions
        <Extension()>
        Friend Function GetExtendedStatusCode(pUpdateActionType As UpdateActionType) As String

            Select Case pUpdateActionType
                Case UpdateActionType.UnderMFW
                    Return "RUOEM"
                Case UpdateActionType.NoProblemFound
                    Return "NVF"
                Case UpdateActionType.Repaired
                    Return "REPRD"
                Case UpdateActionType.RepairedBoardSwap
                    Return "REPBSWAP"
                Case UpdateActionType.RepairedWithRefurbParts
                    Return "TRAFALREP"
                Case UpdateActionType.WaitingForSpareParts
                    Return "EQDEM"
                Case UpdateActionType.ReplacedRefurbished
                    Return "TRAIRR"
                Case UpdateActionType.Irreparable
                    Return "CHLIRR"
                Case UpdateActionType.NotRepairedMissingParts
                    Return "FALREP"
                Case UpdateActionType.DeviceDelivered
                    Return "EQDE"
                Case Else
                    Throw New NotSupportedException()
            End Select

        End Function
    End Module

End Namespace

