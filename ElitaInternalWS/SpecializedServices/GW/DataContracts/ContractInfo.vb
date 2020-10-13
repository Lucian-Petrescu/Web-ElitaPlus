
Imports System.Runtime.Serialization
Imports Assurant.ElitaPlus.DataEntities
Imports Assurant.ElitaPlus.Business
Imports BO = Assurant.ElitaPlus.BusinessObjectsNew

Namespace SpecializedServices.GW
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/GwPilService/GetCertificate", Name:="Contract")>
    Public Class ContractInfo
        <DataMember(IsRequired:=True, Name:="EffectiveDate", Order:=1)>
        Public Property EffectiveDate As Date

        <DataMember(IsRequired:=True, Name:="ExpirationDate", Order:=2)>
        Public Property ExpirationDate As Date

        <DataMember(IsRequired:=False, Name:="PolicyNumber", Order:=3)>
        Public Property PolicyNumber As String

        <DataMember(IsRequired:=False, Name:="PeriodicBilling", Order:=4)>
        Public Property PeriodicBilling As String

        <DataMember(IsRequired:=False, Name:="LineOfBusiness", Order:=5)>
        Public Property LineOfBusiness As String

        <DataMember(IsRequired:=False, Name:="ProducerName", Order:=6)>
        Public Property ProducerName As String

        <DataMember(IsRequired:=False, Name:="ProducerAddress", Order:=7)>
        Public Property ProducerAddress As AddressInfo

        <DataMember(IsRequired:=False, Name:="ProducerTaxId", Order:=8)>
        Public Property ProducerTaxId As String

        <DataMember(IsRequired:=False, Name:="RegulatorRegistrationId", Order:=9)>
        Public Property RegulatorRegistrationId As String
        Private Property AddressManager As IAddressManager

        Public Sub New()
        End Sub

        Public Sub New(pContract As Contract, pCommonManager As ICommonManager, pLanguage As String, pCountryManager As ICountryManager, contractProducer As BO.Producer, producerAddress As Address)
            EffectiveDate = pContract.Effective
            ExpirationDate = pContract.Expiration
            PolicyNumber = pContract.POLICY
            PeriodicBilling = pContract.RECURRING_PREMIUM_ID.ToDescription(pCommonManager, ListCodes.PeriodicBilling, pLanguage)
            If pContract.LineOfBusinessId IsNot Nothing Then
                Dim countryLOB As New BO.CountryLineOfBusiness(pContract.LineOfBusinessId)
                LineOfBusiness = countryLOB.Description
            End If

            If contractProducer IsNot Nothing Then
                ProducerName = contractProducer.Description
                ProducerTaxId = contractProducer.TaxIdNumber
                RegulatorRegistrationId = contractProducer.RegulatorRegistrationId

                If producerAddress IsNot Nothing Then
                    Me.ProducerAddress = New AddressInfo(producerAddress, pCountryManager)
                End If

            End If
        End Sub
    End Class
End Namespace
