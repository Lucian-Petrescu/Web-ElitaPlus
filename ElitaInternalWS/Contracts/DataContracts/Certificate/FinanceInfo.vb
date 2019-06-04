Imports System.Runtime.Serialization
Imports Assurant.ElitaPlus.BusinessObjectsNew

<DataContract(Name:="FinanceInfo", Namespace:="http://elita.assurant.com/DataContracts/Certificate")> _
Public Class FinanceInfo

    <DataMember(Name:="FinancedTabAmount", IsRequired:=False)> _
    Public Property FinancedTabAmount As Nullable(Of Decimal)

    <DataMember(Name:="AssurantOutstandingFinancialBalance", IsRequired:=False)>
    Public Property AssurantOutstandingFinancialBalance As Nullable(Of Decimal)

    <DataMember(Name:="TotalPaidInstallments", IsRequired:=False)>
    Public Property TotalPaidInstallments As Integer

    <DataMember(Name:="TotalInstallments", IsRequired:=False)>
    Public Property TotalInstallments As Integer

    <DataMember(Name:="TotalNumberOverallPayments", IsRequired:=False)>
    Public Property TotalPayments As Integer

    Public Sub New()

    End Sub

    Friend Sub New(ByVal pCertificate As Certificate, ByVal serialNumber As String)

        Me.FinancedTabAmount = pCertificate.Finance_Tab_Amount

        Me.AssurantOutstandingFinancialBalance = pCertificate.GetFinancialAmount(serialNumber)

        Me.TotalPaidInstallments = pCertificate.NumOfConsecutivePayments

        Me.TotalInstallments = pCertificate.Finance_Term

        Me.TotalPayments = pCertificate.GetTotalOverallPayments()

    End Sub



End Class
