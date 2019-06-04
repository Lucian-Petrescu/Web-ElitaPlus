
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

        Public Sub New(ByVal pContract As Contract, ByVal pCommonManager As ICommonManager, ByVal pLanguage As String, ByVal pCountryManager As ICountryManager, ByVal contractProducer As BO.Producer, ByVal producerAddress As Address)
            Me.EffectiveDate = pContract.Effective
            Me.ExpirationDate = pContract.Expiration
            Me.PolicyNumber = pContract.POLICY
            Me.PeriodicBilling = pContract.RECURRING_PREMIUM_ID.ToDescription(pCommonManager, ListCodes.PeriodicBilling, pLanguage)
            If Not pContract.LineOfBusinessId Is Nothing Then
                Dim countryLOB As New BO.CountryLineOfBusiness(pContract.LineOfBusinessId)
                Me.LineOfBusiness = countryLOB.Description
            End If

            If Not contractProducer Is Nothing Then
                Me.ProducerName = contractProducer.Description
                Me.ProducerTaxId = contractProducer.TaxIdNumber
                Me.RegulatorRegistrationId = contractProducer.RegulatorRegistrationId

                If Not producerAddress Is Nothing Then
                    Me.ProducerAddress = New AddressInfo(producerAddress, pCountryManager)
                End If

            End If
        End Sub
    End Class
End Namespace
