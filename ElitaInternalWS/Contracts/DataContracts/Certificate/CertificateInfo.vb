﻿Imports System.Runtime.Serialization
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

    Friend Sub New(pCertificate As Certificate, UpgradeFlag As String, UpgradeDate As Date)
        CertificateNumber = pCertificate.CertNumber
        ProductCode = pCertificate.ProductCode
        WorkPhone = pCertificate.WorkPhone
        If UpgradeFlag = Codes.YESNO_N Then
            CertificateStatus = pCertificate.StatusCode
        ElseIf UpgradeFlag = Codes.YESNO_Y Then
            If pCertificate.StatusCode = Codes.CERTIFICATE_STATUS__CANCELLED Then
                If UpgradeDate > pCertificate.getCertCancellationDate() Then
                    CertificateStatus = pCertificate.StatusCode
                Else
                    CertificateStatus = Codes.CERTIFICATE_STATUS__ACTIVE
                End If
            Else
                CertificateStatus = pCertificate.StatusCode
            End If
        End If
        WarrantySalesDate = pCertificate.WarrantySalesDate
        If pCertificate.UpgradeFixedTerm IsNot Nothing Then
            UpgradeFixedTerm = pCertificate.UpgradeFixedTerm.Value
        End If
        UpgradeProduct = pCertificate.getProdUpgradeProgramCode
        UpgradeTermUOM = pCertificate.getUpgradeTermUOMCode
        If pCertificate.UpgradeTermFrom IsNot Nothing Then
            UpgradeTermFrom = pCertificate.UpgradeTermFrom.Value
        End If
        If pCertificate.UpgradeTermTo IsNot Nothing Then
            UpgradeTermTo = pCertificate.UpgradeTermTo.Value
        End If
        LoanCode = pCertificate.LoanCode
        PenaltyFee = pCertificate.PenaltyFee
        PremiumAmount = pCertificate.PremiumAmount
        AppleCareFee = pCertificate.AppleCareFee
        SalesPrice = pCertificate.SalesPrice

    End Sub


End Class
