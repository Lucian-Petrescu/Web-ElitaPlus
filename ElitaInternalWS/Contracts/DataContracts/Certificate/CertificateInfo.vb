Imports System.Runtime.Serialization
Imports System.Collections.Generic
Imports Assurant.ElitaPlus.BusinessObjectsNew

<DataContract(Name:="CertificateInfo", Namespace:="http://elita.assurant.com/DataContracts/Certificate")>
Public Class CertificateInfo

    <DataMember(Name:="CertificateNumber", IsRequired:=True)>
    Public Property CertificateNumber As String

    <DataMember(Name:="ProductCode", IsRequired:=True)>
    Public Property ProductCode As String

    <DataMember(Name:="WorkPhone", IsRequired:=True)>
    Public Property WorkPhone As String

    <DataMember(Name:="CertificateStatus", IsRequired:=True)>
    Public Property CertificateStatus As String

    <DataMember(Name:="WarrantySalesDate", IsRequired:=True)>
    Public Property WarrantySalesDate As Date

    <DataMember(Name:="UpgradeTerm", IsRequired:=False)>
    Public Property UpgradeFixedTerm As Nullable(Of Long)

    <DataMember(Name:="UpgradeProduct", IsRequired:=False)>
    Public Property UpgradeProduct As String

    <DataMember(Name:="UpgradeTermUOM", IsRequired:=False)>
    Public Property UpgradeTermUOM As String

    <DataMember(Name:="UpgradeTermFrom", IsRequired:=False)>
    Public Property UpgradeTermFrom As Nullable(Of Long)

    <DataMember(Name:="UpgradeTermTo", IsRequired:=False)>
    Public Property UpgradeTermTo As Nullable(Of Long)

    <DataMember(Name:="LoanCode", IsRequired:=False)>
    Public Property LoanCode As String

    <DataMember(Name:="PenaltyFee", IsRequired:=False)>
    Public Property PenaltyFee As String

    <DataMember(Name:="Items", IsRequired:=False)>
    Public Property Items As IEnumerable(Of ItemInfo)

    <DataMember(Name:="CertificateFinanceInfo", IsRequired:=False)>
    Public Property Finance As FinanceInfo

    <DataMember(Name:="PremiumAmount", IsRequired:=False)>
    Public Property PremiumAmount As Decimal

    <DataMember(Name:="AppleCareFee", IsRequired:=False)>
    Public Property AppleCareFee As Decimal

    <DataMember(Name:="SalesPrice", IsRequired:=False)>
    Public Property SalesPrice As Decimal


    Public Sub New()

    End Sub

    Friend Sub New(ByVal pCertificate As Certificate, UpgradeFlag As String, UpgradeDate As Date)
        Me.CertificateNumber = pCertificate.CertNumber
        Me.ProductCode = pCertificate.ProductCode
        Me.WorkPhone = pCertificate.WorkPhone
        If UpgradeFlag = Codes.YESNO_N Then
            Me.CertificateStatus = pCertificate.StatusCode
        ElseIf UpgradeFlag = Codes.YESNO_Y Then
            If pCertificate.StatusCode = Codes.CERTIFICATE_STATUS__CANCELLED Then
                If UpgradeDate > pCertificate.getCertCancellationDate() Then
                    Me.CertificateStatus = pCertificate.StatusCode
                Else
                    Me.CertificateStatus = Codes.CERTIFICATE_STATUS__ACTIVE
                End If
            Else
                Me.CertificateStatus = pCertificate.StatusCode
            End If
        End If
        Me.WarrantySalesDate = pCertificate.WarrantySalesDate
        If Not pCertificate.UpgradeFixedTerm Is Nothing Then
            Me.UpgradeFixedTerm = pCertificate.UpgradeFixedTerm.Value
        End If
        Me.UpgradeProduct = pCertificate.getProdUpgradeProgramCode
        Me.UpgradeTermUOM = pCertificate.getUpgradeTermUOMCode
        If Not pCertificate.UpgradeTermFrom Is Nothing Then
            Me.UpgradeTermFrom = pCertificate.UpgradeTermFrom.Value
        End If
        If Not pCertificate.UpgradeTermTo Is Nothing Then
            Me.UpgradeTermTo = pCertificate.UpgradeTermTo.Value
        End If
        Me.LoanCode = pCertificate.LoanCode
        Me.PenaltyFee = pCertificate.PenaltyFee
        Me.PremiumAmount = pCertificate.PremiumAmount
        Me.AppleCareFee = pCertificate.AppleCareFee
        Me.SalesPrice = pCertificate.SalesPrice

    End Sub


End Class
