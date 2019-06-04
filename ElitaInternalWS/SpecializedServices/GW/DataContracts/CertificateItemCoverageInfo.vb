Imports System.Runtime.Serialization
Imports Assurant.ElitaPlus.Business
Imports Assurant.ElitaPlus.DataEntities
Imports System.Collections.Generic

Namespace SpecializedServices.GW
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/GwPilService/GetCertificate", Name:="CertificateItemCoverageInfo")>
    Public Class CertificateItemCoverageInfo
        <DataMember(IsRequired:=True, Name:="CoverageName")>
        Public Property CoverageName As String

        <DataMember(IsRequired:=True, Name:="EffectiveStartingOn")>
        Public Property BeginDate As Date

        <DataMember(IsRequired:=True, Name:="EffectiveEndingOn")>
        Public Property EndDate As Date

        <DataMember(IsRequired:=True, Name:="CoverageTypeCode")>
        Public Property CoverageTypeCode As String

        <DataMember(IsRequired:=True, Name:="LiabilityLimits")>
        Public Property LiabilityLimits As String

        <DataMember(IsRequired:=False, Name:="DeductibleAmount", EmitDefaultValue:=False)>
        Public Property DeductibleAmount As Nullable(Of Decimal)

        <DataMember(IsRequired:=False, Name:="DeductiblePercent", EmitDefaultValue:=False)>
        Public Property DeductiblePercent As Nullable(Of Decimal)

        <DataMember(IsRequired:=False, Name:="LossCost", EmitDefaultValue:=False)>
        Public Property LossCost As Nullable(Of Decimal)

        <DataMember(IsRequired:=False, Name:="DealerDiscountAmount", EmitDefaultValue:=False)>
        Public Property DealerDiscountAmount As Nullable(Of Decimal)

        <DataMember(IsRequired:=False, Name:="DealerDiscountPercent", EmitDefaultValue:=False)>
        Public Property DealerDiscountPercent As Nullable(Of Decimal)

        <DataMember(IsRequired:=False, Name:="IsClaimAllowed", EmitDefaultValue:=False)>
        Public Property IsClaimAllowed As String

        <DataMember(IsRequired:=False, Name:="IsDiscount", EmitDefaultValue:=False)>
        Public Property IsDiscount As String

        <DataMember(IsRequired:=False, Name:="SalesTax", EmitDefaultValue:=False)>
        Public Property SalesTax As Nullable(Of Decimal)

        <DataMember(IsRequired:=False, Name:="Tax1", EmitDefaultValue:=False)>
        Public Property Tax1 As Nullable(Of Decimal)

        <DataMember(IsRequired:=False, Name:="Tax2", EmitDefaultValue:=False)>
        Public Property Tax2 As Nullable(Of Decimal)

        <DataMember(IsRequired:=False, Name:="Tax3", EmitDefaultValue:=False)>
        Public Property Tax3 As Nullable(Of Decimal)

        <DataMember(IsRequired:=False, Name:="Tax4", EmitDefaultValue:=False)>
        Public Property Tax4 As Nullable(Of Decimal)

        <DataMember(IsRequired:=False, Name:="Tax5", EmitDefaultValue:=False)>
        Public Property Tax5 As Nullable(Of Decimal)

        <DataMember(IsRequired:=False, Name:="Tax6", EmitDefaultValue:=False)>
        Public Property Tax6 As Nullable(Of Decimal)

        <DataMember(IsRequired:=False, Name:="RepairDiscountPercent", EmitDefaultValue:=False)>
        Public Property RepairDiscountPercent As Nullable(Of Decimal)

        <DataMember(IsRequired:=False, Name:="ReplacementDiscountPercent", EmitDefaultValue:=False)>
        Public Property ReplacementDiscountPercent As Nullable(Of Decimal)

        <DataMember(IsRequired:=False, Name:="TaxesPaidByCustomer", EmitDefaultValue:=False)>
        Public Property TaxesPaidByCustomer As Nullable(Of Decimal)

        <DataMember(IsRequired:=False, Name:="DeductibleBasedOn", EmitDefaultValue:=False)>
        Public Property DeductibleBasedOn As String

        <DataMember(IsRequired:=False, Name:="LiabilityLimitPercent", EmitDefaultValue:=False)>
        Public Property LiabilityLimitPercent As Nullable(Of Decimal)

        <DataMember(IsRequired:=False, Name:="CoverageLiabilityLimit", EmitDefaultValue:=False)>
        Public Property CoverageLiabilityLimit As Nullable(Of Decimal)

        <DataMember(IsRequired:=False, Name:="CoverageLiabilityLimitPercent", EmitDefaultValue:=False)>
        Public Property CoverageLiabilityLimitPercent As Nullable(Of Decimal)

        <DataMember(IsRequired:=False, Name:="NoOfRenewals", EmitDefaultValue:=False)>
        Public Property NoOfRenewals As Nullable(Of Integer)

        <DataMember(IsRequired:=False, Name:="RenewalDate", EmitDefaultValue:=False)>
        Public Property RenewalDate As Nullable(Of Date)

        <DataMember(IsRequired:=True, Name:="MethodOfRepairCode")>
        Public Property MethodOfRepairCode As String

        <DataMember(IsRequired:=True, Name:="MethodOfRepairDesc")>
        Public Property MethodOfRepairDesc As String

        <DataMember(IsRequired:=False, Name:="CertificateItemCoverageDeductibleInfo", EmitDefaultValue:=False)>
        Public Property ItemCoverageDeductibles As IEnumerable(Of CertificateItemCoverageDeductibleInfo)

        Public Sub New()

        End Sub

        Public Sub New(ByVal pCertficateItemCoverage As CertificateItemCoverage,
                       ByVal pCommonManager As CommonManager,
                       ByVal pProduct As Product,
                       ByVal pLangauge As String)
            ' Copy properties from Certificate Item to current instance
            Dim oCoverage As Coverage
            If pProduct.Items.Count >= 0 And Not pProduct.Items.Where(Function(i) i.ItemNumber = pCertficateItemCoverage.Item.ItemNumber).FirstOrDefault Is Nothing Then
                oCoverage = pProduct.Items.Where(Function(i) i.ItemNumber = pCertficateItemCoverage.Item.ItemNumber).FirstOrDefault.Coverages.Where(Function(c) c.CoverageTypeId = pCertficateItemCoverage.CoverageTypeId).FirstOrDefault
            End If

            With Me
                    .CoverageName = pCertficateItemCoverage.CoverageTypeId.ToDescription(pCommonManager, ListCodes.CoverageType, pLangauge)
                    .CoverageTypeCode = pCertficateItemCoverage.CoverageTypeId.ToCode(pCommonManager, ListCodes.CoverageType, pLangauge)
                    .BeginDate = pCertficateItemCoverage.BeginDate
                    .EndDate = pCertficateItemCoverage.EndDate
                    .LiabilityLimits = pCertficateItemCoverage.LiabilityLimits
                    .DeductibleAmount = pCertficateItemCoverage.Deductible
                    .DeductiblePercent = pCertficateItemCoverage.DeductiblePercent
                    .LossCost = pCertficateItemCoverage.LossCost
                    .DealerDiscountAmount = pCertficateItemCoverage.DealerDiscountAmt
                    .DealerDiscountPercent = pCertficateItemCoverage.DealerDiscountPercent
                    .IsClaimAllowed = If(Not pCertficateItemCoverage.IsClaimAllowed Is Nothing,
                                pCertficateItemCoverage.IsClaimAllowed.ToDescription(pCommonManager, ListCodes.YesNo, pLangauge), Nothing)
                    .IsDiscount = If(Not pCertficateItemCoverage.IsDiscount Is Nothing,
                            pCertficateItemCoverage.IsDiscount.ToDescription(pCommonManager, ListCodes.YesNo, pLangauge), Nothing)
                    .SalesTax = pCertficateItemCoverage.SalesTax
                    .Tax1 = pCertficateItemCoverage.Tax1
                    .Tax2 = pCertficateItemCoverage.Tax2
                    .Tax3 = pCertficateItemCoverage.Tax3
                    .Tax4 = pCertficateItemCoverage.Tax4
                    .Tax5 = pCertficateItemCoverage.Tax5
                    .Tax6 = pCertficateItemCoverage.Tax6
                    .RepairDiscountPercent = pCertficateItemCoverage.RepairDiscountPCt
                    .ReplacementDiscountPercent = pCertficateItemCoverage.ReplacementDiscountPct
                    .TaxesPaidByCustomer = pCertficateItemCoverage.TaxesPaidByCustomer
                    .DeductibleBasedOn = pCertficateItemCoverage.DeductibleBasedOnId.ToDescription(pCommonManager, ListCodes.DeductibleBasedOn, pLangauge)
                    '''''from Elp_Coverage table
                    .LiabilityLimitPercent = If(Not oCoverage Is Nothing, oCoverage.LiabilityLimitPercent, Nothing)
                    .CoverageLiabilityLimit = If(Not oCoverage Is Nothing, oCoverage.CovLiabilityLimit, Nothing)
                    .CoverageLiabilityLimitPercent = If(Not oCoverage Is Nothing, oCoverage.CovLiaibilityLimitPercent, Nothing)
                    '.LiabilityLimitPercent = pCertficateItemCoverage.LiabilityLimits
                    '.CoverageLiabilityLimit = pCertficateItemCoverage.CoverageLiabilityLimit
                    '''.CoverageLiabilityLimitPercent = pCertficateItemCoverage.cov 
                    .NoOfRenewals = pCertficateItemCoverage.NoOfRenewals
                    .RenewalDate = pCertficateItemCoverage.RenewalDate
                    .MethodOfRepairCode = If(Not pCertficateItemCoverage.MethodOfRepairId Is Nothing,
                                    pCertficateItemCoverage.MethodOfRepairId.ToCode(pCommonManager, ListCodes.MethodOfRepair, pLangauge),
                                    pCertficateItemCoverage.Certificate.MethodOfRepairId.ToCode(pCommonManager, ListCodes.MethodOfRepair, pLangauge))
                    .MethodOfRepairDesc = If(Not pCertficateItemCoverage.MethodOfRepairId Is Nothing,
                                    pCertficateItemCoverage.MethodOfRepairId.ToDescription(pCommonManager, ListCodes.MethodOfRepair, pLangauge),
                                    pCertficateItemCoverage.Certificate.MethodOfRepairId.ToDescription(pCommonManager, ListCodes.MethodOfRepair, pLangauge))

                    If pCertficateItemCoverage.ItemCoverageDeductibles.Count > 0 Then
                        Me.ItemCoverageDeductibles = New List(Of CertificateItemCoverageDeductibleInfo)

                        For Each cicd As CertificateItemCoverageDeductible In pCertficateItemCoverage.ItemCoverageDeductibles
                            DirectCast(Me.ItemCoverageDeductibles, IList(Of CertificateItemCoverageDeductibleInfo)).Add(New CertificateItemCoverageDeductibleInfo(cicd, pCommonManager, pLangauge))
                        Next
                    End If

                End With
        End Sub
    End Class
End Namespace