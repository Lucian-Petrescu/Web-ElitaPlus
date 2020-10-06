Public Class CertItemCoverageList
    Inherits BusinessObjectListBase

    Public Sub New(parent As Certificate)
        MyBase.New(LoadTable(parent), GetType(CertItemCoverage), parent)
    End Sub


    Public Overrides Function Belong(bo As BusinessObjectBase) As Boolean
        Return CType(bo, CertItemCoverage).CertItemId.Equals(CType(Parent, Certificate).Id)
    End Function

    Public Function Find(itemCoverageId As Guid) As CertItemCoverage
        Dim bo As CertItemCoverage
        For Each bo In Me
            If bo.Id.Equals(itemCoverageId) Then Return bo
        Next
        Return Nothing
    End Function


#Region "Class Methods"
    Private Shared Function LoadTable(parent As Certificate) As DataTable
        'Try
        '    If Not parent.IsChildrenCollectionLoaded(GetType(CertItemCoverageList)) Then
        '        Dim dal As New CertItemCoverageDAL
        '        dal.LoadList(parent.Dataset, parent.Id)
        '        parent.AddChildrenCollection(GetType(ServiceGroupRiskTypeList))
        '    End If
        '    Return parent.Dataset.Tables(ServiceGroupRiskTypeDAL.TABLE_NAME)
        'Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
        '    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        'End Try
    End Function
#End Region

#Region "Public Methods"

    Public Function GetPremiumTotals() As CertItemCoverage
        Dim bo As New CertItemCoverage
        Dim ototals As New CertItemCoverage
        Dim totalGrossAmtReceived As Decimal = 0
        Dim totalPremWritten As Decimal = 0
        Dim totalOrigPrem As Decimal = 0
        Dim totalLossCost As Decimal = 0
        Dim totalComissions As Decimal = 0
        Dim totalAdminExp As Decimal = 0
        Dim totalMarketingExp As Decimal = 0
        Dim totalOther As Decimal = 0
        Dim totalSalesTax As Decimal = 0
        Dim totalMTDPayments As Decimal = 0
        Dim totalYTDPayments As Decimal = 0

        ototals.GrossAmtReceived = New DecimalType(0)

        For Each bo In Me
            totalGrossAmtReceived += bo.GrossAmtReceived.Value
            totalPremWritten += bo.PremiumWritten.Value
            totalOrigPrem += bo.OriginalPremium.Value
            totalLossCost += bo.LossCost.Value
            totalComissions += bo.Commission.Value
            totalAdminExp += bo.AdminExpense.Value
            totalMarketingExp += bo.MarketingExpense.Value
            totalOther += bo.Other.Value
            totalSalesTax += bo.SalesTax.Value
            totalMTDPayments += bo.MtdPayments.Value
            totalYTDPayments += bo.YtdPayments.Value
        Next

        ototals.GrossAmtReceived = New DecimalType(totalGrossAmtReceived)
        ototals.PremiumWritten = New DecimalType(totalPremWritten)
        ototals.OriginalPremium = New DecimalType(totalOrigPrem)
        ototals.LossCost = New DecimalType(totalLossCost)
        ototals.Commission = New DecimalType(totalComissions)
        ototals.AdminExpense = New DecimalType(totalAdminExp)
        ototals.MarketingExpense = New DecimalType(totalMarketingExp)
        ototals.Other = New DecimalType(totalOther)
        ototals.SalesTax = New DecimalType(totalSalesTax)
        ototals.MtdPayments = New DecimalType(totalMTDPayments)
        ototals.YtdPayments = New DecimalType(totalYTDPayments)

        Return ototals
    End Function

#End Region

End Class
