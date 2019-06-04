Imports System.Runtime.Serialization



<DataContract(Name:="CoverageTypes", Namespace:="http://elita.assurant.com/DataContracts/Claims")>
Public Enum CoverageTypes

    <EnumMember> Other = 0
    <EnumMember> Theft = 1
    <EnumMember> Accidental = 2
    <EnumMember> MechanicalBreakdown = 3
    <EnumMember> Manufacturer = 4
    <EnumMember> Upgrade = 5

End Enum



