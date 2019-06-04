Imports System.Runtime.Serialization
Imports Assurant.ElitaPlus.Business
Imports Assurant.ElitaPlus.DataEntities

Namespace SpecializedServices.GW
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/GwPilService/GetCertificate", Name:="DealerInfo")>
    Public Class DealerInfo
        Private Property CommonManager As ICommonManager

        Public Sub New(pCert As Certificate, ByVal pDealer As Dealer, pCommonManager As CommonManager,
                       ByVal pAddress As Address, ByVal pCountryManager As ICountryManager)
            Dim oContract As Contract
            oContract = pDealer.Contracts.Where(Function(c) pCert.WarrantySalesDate >= c.Effective AndAlso pCert.WarrantySalesDate <= c.Expiration).FirstOrDefault
            Me.DealerName = pDealer.DealerName
            Me.DealerNumber = pDealer.DealerCode
            Me.OriginalDealerNumber = String.Empty
            Me.DealerType = pDealer.DealerTypeId.ToDescription(pCommonManager, ListCodes.DealerType, LanguageCodes.USEnglish)
            Me.NumOfClaims = oContract.NumOfClaims
            Me.NumOfRepairClaims = oContract.NumOfRepairClaims
            Me.NumOfReplacementClaims = oContract.NumOfReplacementClaims
            Me.DaysToReportClaim = oContract.DaysToReportClaim
            Me.ClaimWaitingPeriod = oContract.WAITING_PERIOD
            Me.ClaimLimitBasedOnCode = oContract.ClaimLimitBasedOnId.ToCode(pCommonManager, ListCodes.ClaimLimitBasedOn, LanguageCodes.USEnglish)
            Me.ClaimLimitBasedOnDesc = oContract.ClaimLimitBasedOnId.ToDescription(pCommonManager, ListCodes.ClaimLimitBasedOn, LanguageCodes.USEnglish)
            Me.DealerTaxId = pDealer.TAX_ID_NUMBER
            If Not pAddress Is Nothing Then
                Me.DealerAddress = New AddressInfo(pAddress, pCountryManager)
            End If
        End Sub

        Public Sub New()

        End Sub

        <DataMember(IsRequired:=True, Name:="DealerNumber", Order:=3)>
        Public Property DealerNumber As String

        <DataMember(IsRequired:=False, Name:="OriginalDealerNumber", Order:=12)>
        Public Property OriginalDealerNumber As String

        <DataMember(IsRequired:=True, Name:="DealerName", Order:=48)>
        Public Property DealerName As String

        <DataMember(IsRequired:=True, Name:="BusinessLine")>
        Public Property DealerType As String

        <DataMember(IsRequired:=False, Name:="NumOfClaims", EmitDefaultValue:=False)>
        Public Property NumOfClaims As Nullable(Of Integer)

        <DataMember(IsRequired:=False, Name:="NumOfRepairClaims", EmitDefaultValue:=False)>
        Public Property NumOfRepairClaims As Nullable(Of Integer)

        <DataMember(IsRequired:=False, Name:="NumOfReplacementClaims", EmitDefaultValue:=False)>
        Public Property NumOfReplacementClaims As Nullable(Of Integer)

        <DataMember(IsRequired:=False, Name:="DaysToReportClaim", EmitDefaultValue:=False)>
        Public Property DaysToReportClaim As Nullable(Of Integer)

        <DataMember(IsRequired:=False, Name:="ClaimLimitBasedOnCode", EmitDefaultValue:=False)>
        Public Property ClaimLimitBasedOnCode As String

        <DataMember(IsRequired:=False, Name:="ClaimLimitBasedOnDesc", EmitDefaultValue:=False)>
        Public Property ClaimLimitBasedOnDesc As String

        <DataMember(IsRequired:=False, Name:="ClaimWaitingPeriod", EmitDefaultValue:=False)>
        Public Property ClaimWaitingPeriod As Integer

        <DataMember(IsRequired:=False, Name:="DealerGroupName", EmitDefaultValue:=False)>
        Public Property DealerGroupName As String

        <DataMember(IsRequired:=False, Name:="DealerTaxId", EmitDefaultValue:=False)>
        Public Property DealerTaxId As String

        <DataMember(IsRequired:=False, Name:="DealerAddress", EmitDefaultValue:=False)>
        Public Property DealerAddress As AddressInfo
    End Class
End Namespace