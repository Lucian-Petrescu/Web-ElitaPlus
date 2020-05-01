Imports Assurant.ElitaPlus.BusinessObjectsNew.CertItem

Friend Interface ICalculateFinancialBalance

    Function Calculate(ByVal pCertificate As Certificate) As Decimal

    Property SerialNumber As String

End Interface

Friend Class DefaultCalculateFinancialBalance
    Implements ICalculateFinancialBalance


    Public Property SerialNumber As String Implements ICalculateFinancialBalance.SerialNumber

    Public Function Calculate(pCertificate As Certificate) As Decimal Implements ICalculateFinancialBalance.Calculate
        Return 0D
    End Function
End Class

Friend Class IncomingAmountFinancialBalance
    Implements ICalculateFinancialBalance


    Public Property SerialNumber As String Implements ICalculateFinancialBalance.SerialNumber

    Public Function Calculate(pCertificate As Certificate) As Decimal Implements ICalculateFinancialBalance.Calculate
        Dim dal As New CertEndorseDAL
        Return pCertificate.OutstandingBalanceAmount
    End Function
End Class

Friend NotInheritable Class PRCalculateFinancialBalance
    Implements ICalculateFinancialBalance

    Public Property SerialNumber As String Implements ICalculateFinancialBalance.SerialNumber

    Private Function Calculate(ByVal pCertificate As Certificate) As Decimal Implements ICalculateFinancialBalance.Calculate

        Dim dal As New CertItemDAL
        Dim ds As DataSet
        Dim val As Decimal

        Dim originalRetailPrice As Nullable(Of Decimal)

        'Dim dv As CertItemSearchDV = pCertificate.CertItems

        If Not pCertificate.Items.Where(Function(i) i.SerialNumber = Me.SerialNumber).OrderByDescending(Function(i) i.EffectiveDate).First().OriginalRetailPrice Is Nothing Then
            originalRetailPrice = pCertificate.Items.Where(Function(i) i.SerialNumber = Me.SerialNumber).OrderByDescending(Function(i) i.EffectiveDate).First().OriginalRetailPrice
        Else
            originalRetailPrice = 0.0
        End If





        'originalRetailPrice = dv.GetOriginalRetailPrice(Me.SerialNumber)
        If originalRetailPrice.HasValue And Not pCertificate.FinanceDate Is Nothing Then

            Dim noOfMonthsPassed As Double

            noOfMonthsPassed = (DateTime.Now.Month - pCertificate.FinanceDate.Value.Month) + 12 * (DateTime.Now.Year - pCertificate.FinanceDate.Value.Year)
            Dim downPayment As DecimalType = IIf(IsDBNull(pCertificate.DownPayment), 0, pCertificate.DownPayment)

            val = (originalRetailPrice - CType(downPayment, Decimal) - CType(pCertificate.AdvancePayment, Decimal) - (noOfMonthsPassed * pCertificate.Financed_installment_Amount.Value))

            If val < 0.0 Then
                val = 0.0
            End If

            Return val
        Else
            Return 0.0
        End If
    End Function
End Class

Friend NotInheritable Class BRCalculateFinancialBalance
    Implements ICalculateFinancialBalance

    Public Property SerialNumber As String Implements ICalculateFinancialBalance.SerialNumber

    Private Function Calculate(pCertificate As Certificate) As Decimal Implements ICalculateFinancialBalance.Calculate


        Dim val As Double

        Dim noOfMonthsRemaining As Double
        noOfMonthsRemaining = 24 - ((DateTime.Now.Month - pCertificate.WarrantySalesDate.Value.Month) + 12 * (DateTime.Now.Year - pCertificate.WarrantySalesDate.Value.Year))
        Dim finInstallAmount As Decimal
        If Not pCertificate.Finance_Installment_Amount Is Nothing Then
            finInstallAmount = pCertificate.Financed_installment_Amount.Value
        End If

        Dim grossamt As Decimal
        grossamt = pCertificate.GetMonthlyGrossAmount(pCertificate.Id)
        val = (noOfMonthsRemaining * pCertificate.Financed_installment_Amount.Value) - (noOfMonthsRemaining * pCertificate.GetMonthlyGrossAmount(pCertificate.Id))


        Return val

    End Function


End Class

Friend NotInheritable Class CHCalculateCustPoints
    Implements ICalculateFinancialBalance

    Public Property SerialNumber As String Implements ICalculateFinancialBalance.SerialNumber

    Private Function Calculate(pCertificate As Certificate) As Decimal Implements ICalculateFinancialBalance.Calculate

        Dim val As Double
        val = pCertificate.DealerRewardPoints

        Return val

    End Function
End Class

Friend NotInheritable Class CalculateFinancialBalanceFactory

    Friend Shared Function GetCalculator(ByVal pCalculationMethod As String) As ICalculateFinancialBalance
        Select Case pCalculationMethod
            Case Codes.UPG_FINANCE_BAL_COMP_METH__PR
                Return New PRCalculateFinancialBalance()
            Case Codes.UPG_FINANCE_BAL_COMP_METH__BR
                Return New BRCalculateFinancialBalance()
            Case Codes.UPG_FINANCE_BAL_COMP_METH__H3GIT
                Return New H3GITCalculateFinancialBalance()
            Case Codes.UPG_MOVISTAR_CUST_REWARD_POINTS__CH
                Return New CHCalculateCustPoints()
            Case Codes.UPG_FINANCE_BAL_COMP_METH_IA
                Return New IncomingAmountFinancialBalance
            Case Else
                Return New DefaultCalculateFinancialBalance()
        End Select
    End Function

End Class

Friend NotInheritable Class H3GITCalculateFinancialBalance
    Implements ICalculateFinancialBalance

    Public Property SerialNumber As String Implements ICalculateFinancialBalance.SerialNumber

    Private Function Calculate(pCertificate As Certificate) As Decimal Implements ICalculateFinancialBalance.Calculate


        Dim val As Double = 0
        Dim noOfMonthsPassed As Double
        Dim pymtActDate As Date
        Dim paymentShift As Long
        Dim finInstallAmount As Decimal

        pymtActDate = pCertificate.WarrantySalesDate.Value.AddMonths(1)
        pymtActDate = New DateTime(pymtActDate.Year, pymtActDate.Month, 1)

        noOfMonthsPassed = pCertificate.GetMonthsPassedForH3GI(pymtActDate) '(DateTime.Now.Month - pymtActDate.Month) + 12 * (DateTime.Now.Year - pymtActDate.Year)
        If Not pCertificate.PaymentShiftNumber Is Nothing Then
            paymentShift = pCertificate.PaymentShiftNumber.Value
        Else
            paymentShift = 0
        End If

        If Not pCertificate.Finance_Installment_Amount Is Nothing Then
            finInstallAmount = pCertificate.Financed_installment_Amount.Value
        End If

        val = CType(pCertificate.Finance_Tab_Amount, Decimal) - (finInstallAmount * (noOfMonthsPassed - paymentShift))

        Return val

    End Function


End Class
