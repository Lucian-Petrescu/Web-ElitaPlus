Imports System.Runtime.Serialization



<DataContract(Name:="CertificateDetailTypes", Namespace:="http://elita.assurant.com/DataContracts/Certificate"), Flags()> _
Public Enum CertificateDetailTypes
    None = 0

    ''' <summary>
    ''' Includes Finance Info in response
    ''' </summary>
    ''' <remarks></remarks>
    <EnumMember> FinanceInfo = 1

    ''' <summary>
    ''' Gets Items
    ''' </summary>
    ''' <remarks></remarks>
    <EnumMember> Items = 2

    ''' <summary>
    ''' For Internal Use Only, used to determine if Coverage Option is selected. We can not have Coverages w/o Item so this
    ''' option should not be exposed to Clients
    ''' </summary>
    ''' <remarks></remarks>
    CoveragesOnly = 4

    ''' <summary>
    ''' Gets Items and Coverages
    ''' </summary>
    ''' <remarks></remarks>
    <EnumMember> Coverages = 6

    <EnumMember> UpgradeCert = 7
    
End Enum


