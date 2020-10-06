Imports Oracle.ManagedDataAccess.Client

Public Class DealerContext
    Public Sub New()
        MyBase.New("Dealers.DealerDataModel")
    End Sub

    Private Sub CheckDBConnection()
        If Me.Database.Connection.State = ConnectionState.Closed Then
            Database.Connection.Open()
        End If
    End Sub

    Friend Function ComputeTax(ByVal pAmount As Decimal,
                               ByVal pDealerId As Nullable(Of Guid),
                               ByVal pCountryId As Nullable(Of Guid),
                               ByVal pCompanyTypeId As Nullable(Of Guid),
                               ByVal pTaxTypeId As Nullable(Of Guid),
                               ByVal pRegionId As Nullable(Of Guid),
                               ByVal pExpectedPremiumIsWpId As Nullable(Of Guid),
                               ByVal pProductTaxTypeId As Nullable(Of Guid),
                               ByVal pSalesDate As Date) As String
        Dim dbCommand As OracleCommand = DirectCast(Database.Connection.CreateCommand(), OracleCommand)
        dbCommand.CommandType = CommandType.StoredProcedure
        dbCommand.CommandText = "elita.elp_utl_tax.Get_Tax_Amount"

        Dim o_TaxAmount As OracleParameter = New OracleParameter("o_TaxAmount", OracleDbType.Int32)
        o_TaxAmount.Direction = ParameterDirection.ReturnValue
        dbCommand.Parameters.Add(o_TaxAmount)

        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_amount", .OracleDbType = OracleDbType.Decimal, .Size = 16, .Value = pAmount})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_dealer_id", .OracleDbType = OracleDbType.Raw, .Size = 16, .Value = pDealerId})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_country_id", .OracleDbType = OracleDbType.Raw, .Size = 16, .Value = pCountryId})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_company_type_id", .OracleDbType = OracleDbType.Raw, .Value = pCompanyTypeId})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_tax_type_id", .OracleDbType = OracleDbType.Raw, .Size = 16, .Value = pTaxTypeId})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_region_id", .OracleDbType = OracleDbType.Raw, .Size = 16, .Value = pRegionId})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_expected_premium_is_wp_id", .OracleDbType = OracleDbType.Raw, .Size = 16, .Value = pExpectedPremiumIsWpId})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_product_tax_type_id", .OracleDbType = OracleDbType.Raw, .Size = 16, .Value = pProductTaxTypeId})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_sales_date", .OracleDbType = OracleDbType.Date, .Size = 20, .Value = pSalesDate})

        Try
            CheckDBConnection()
            dbCommand.ExecuteNonQuery()
            Database.Connection.Close()

            Return Convert.ToDecimal(o_TaxAmount.Value.ToString())
        Catch ex As DataException
            Throw ex
        Catch ex As Exception
            Return Nothing
        End Try


    End Function
End Class
