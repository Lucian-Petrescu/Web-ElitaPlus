Imports Assurant.ElitaPlus.DataEntities
Imports Oracle.ManagedDataAccess.Client

Public Class CertificateContext

    Public Sub New()
        MyBase.New("Certificates.CertificateDataModel")
    End Sub

    Private Sub CheckDBConnection()
        If Me.Database.Connection.State = ConnectionState.Closed Then
            Database.Connection.Open()
        End If
    End Sub

    Friend Function SearchCertificate(ByVal pCompanyCode As String,
                                 ByVal pDealerCode As String,
                                 ByVal pCertificateNumber As String,
                                 ByVal pPhoneNumber As String,
                                 ByVal pSerialNumber As String,
                                 ByVal pIdentificationNumber As String,
                                 ByVal pServiceLineNumber As String) As DataSet

        Dim dbCommand As OracleCommand = DirectCast(Database.Connection.CreateCommand(), OracleCommand)
        dbCommand.CommandType = CommandType.StoredProcedure

        dbCommand.CommandText = "elp_ws_getCertificate.Search_Certificate"


        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_company_code", .OracleDbType = OracleDbType.Varchar2, .Value = pCompanyCode})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_dealer_code", .OracleDbType = OracleDbType.Varchar2, .Value = pDealerCode})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_cert_number", .OracleDbType = OracleDbType.Varchar2, .Value = pCertificateNumber})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_serial_number", .OracleDbType = OracleDbType.Varchar2, .Value = pSerialNumber})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_identification_number", .OracleDbType = OracleDbType.Varchar2, .Value = pIdentificationNumber})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_service_line_number", .OracleDbType = OracleDbType.Varchar2, .Value = pServiceLineNumber})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_phone_number", .OracleDbType = OracleDbType.Varchar2, .Value = pPhoneNumber})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "po_cert_table", .OracleDbType = OracleDbType.RefCursor, .Direction = ParameterDirection.Output})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "po_return_code", .OracleDbType = OracleDbType.Int64, .Direction = ParameterDirection.Output})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "po_exception_msg", .OracleDbType = OracleDbType.Int64, .Direction = ParameterDirection.Output})
        Dim dbAdapter As OracleDataAdapter = New OracleDataAdapter(dbCommand)
        Dim dsCertList As New DataSet
        dbAdapter.Fill(dsCertList)
        Return dsCertList

    End Function

    Friend Function SearchCertificateByTaxId(ByVal pCountryCode As String,
                                 ByVal pIdentificationNumber As String,
                                 ByVal pPhoneNumber As String,
                                 ByVal numberOfRecords As Integer,
                                 ByRef totalRecordFound As Long) As DataSet

        Dim dbCommand As OracleCommand = DirectCast(Database.Connection.CreateCommand(), OracleCommand)
        dbCommand.CommandType = CommandType.StoredProcedure

        dbCommand.CommandText = "elp_ws_getCertificate.Search_CertByTaxId_GWPIL"

        dbCommand.BindByName = True
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_country_code", .OracleDbType = OracleDbType.Varchar2, .Value = pCountryCode.Trim.ToUpper, .Size = 20})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_identification_number", .OracleDbType = OracleDbType.Varchar2, .Value = pIdentificationNumber.Trim, .Size = 100})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_phone_number", .OracleDbType = OracleDbType.Varchar2, .Value = pPhoneNumber.Trim, .Size = 100})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_row_count_return", .OracleDbType = OracleDbType.Long, .Value = numberOfRecords})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "po_cert_table", .OracleDbType = OracleDbType.RefCursor, .Direction = ParameterDirection.Output})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "po_total_rec_found", .OracleDbType = OracleDbType.Int16, .Direction = ParameterDirection.Output, .DbType = DbType.Int16})

        Dim dbAdapter As OracleDataAdapter = New OracleDataAdapter(dbCommand)
        Dim dsCertList As New DataSet
        Try
            CheckDBConnection()
            dbAdapter.Fill(dsCertList)
        Catch ex As Exception
            Throw ex
        End Try


        If (dbCommand.Parameters("po_total_rec_found") IsNot DBNull.Value) Then
            totalRecordFound = DirectCast(dbCommand.Parameters("po_total_rec_found").Value, Int16)
        Else
            totalRecordFound = 0
        End If

        Return dsCertList
    End Function

    Friend Function GWSearchCertificateByCertNumber(ByVal pDealerCode As String,
                                 ByVal pCertNumber As String) As DataSet

        Dim dbCommand As OracleCommand = DirectCast(Database.Connection.CreateCommand(), OracleCommand)
        dbCommand.CommandType = CommandType.StoredProcedure

        dbCommand.CommandText = "elp_ws_getCertificate.Search_CertByCertNbr_GWPIL"

        dbCommand.BindByName = True
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_dealer_code", .OracleDbType = OracleDbType.Varchar2, .Value = pDealerCode.Trim.ToUpper, .Size = 5})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_cert_number", .OracleDbType = OracleDbType.Varchar2, .Value = pCertNumber.Trim, .Size = 20})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "po_cert_table", .OracleDbType = OracleDbType.RefCursor, .Direction = ParameterDirection.Output})

        Dim dbAdapter As OracleDataAdapter = New OracleDataAdapter(dbCommand)
        Dim dsCertList As New DataSet
        Try
            CheckDBConnection()
            dbAdapter.Fill(dsCertList)
        Catch ex As Exception
            Throw ex
        End Try
        Return dsCertList
    End Function

    Friend Sub GetCertificateCoverageRate(ByVal pCertId As Guid,
                                 ByVal pCoverageDate As Date,
                                 ByRef poGWP As Decimal,
                                 ByRef poSalexTax As Decimal)

        Dim dbCommand As OracleCommand = DirectCast(Database.Connection.CreateCommand(), OracleCommand)
        dbCommand.CommandType = CommandType.StoredProcedure
        dbCommand.CommandText = "elita.ELP_UTL_RATES.get_GWP_by_Cert_id"

        dbCommand.BindByName = True

        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_certid", .OracleDbType = OracleDbType.Raw, .Size = 16, .Value = pCertId, .Direction = ParameterDirection.Input})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_date_of_payment", .OracleDbType = OracleDbType.Date, .Value = pCoverageDate, .Direction = ParameterDirection.Input})

        'output parameters
        Dim paramGWP As OracleParameter
        paramGWP = New OracleParameter() With {.ParameterName = "po_GWP", .DbType = DbType.Decimal, .Direction = ParameterDirection.Output, .Size = 16, .Precision = 5}
        dbCommand.Parameters.Add(paramGWP)
        Dim paramSalesTax As OracleParameter
        paramSalesTax = New OracleParameter() With {.ParameterName = "po_SalesTax", .DbType = DbType.Decimal, .Direction = ParameterDirection.Output, .Size = 16, .Precision = 5}
        dbCommand.Parameters.Add(paramSalesTax)


        Try
            CheckDBConnection()
            dbCommand.ExecuteNonQuery()
            poGWP = paramGWP.Value
            poSalexTax = paramSalesTax.Value
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Friend Sub GetFirstCertEndorseDates(ByVal pCertId As Guid,
                                 ByRef poEndorseEffectiveDate As String,
                                 ByRef poEndorseExpirationDate As String)

        Dim dbCommand As OracleCommand = DirectCast(Database.Connection.CreateCommand(), OracleCommand)
        dbCommand.CommandType = CommandType.StoredProcedure
        dbCommand.CommandText = "elita.elp_ws_getCertificate.get_first_cert_endorse_dates"

        dbCommand.BindByName = True

        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_cert_id", .OracleDbType = OracleDbType.Raw, .Size = 16, .Value = pCertId, .Direction = ParameterDirection.Input})

        'output parameters
        Dim paramEndorseEffectiveDate As OracleParameter
        paramEndorseEffectiveDate = New OracleParameter() With {.ParameterName = "po_effective_date", .DbType = DbType.String, .Size = 30, .Direction = ParameterDirection.Output}
        dbCommand.Parameters.Add(paramEndorseEffectiveDate)
        Dim paramEndorseExpirationDate As OracleParameter
        paramEndorseExpirationDate = New OracleParameter() With {.ParameterName = "po_expiration_date", .DbType = DbType.String, .Size = 30, .Direction = ParameterDirection.Output}
        dbCommand.Parameters.Add(paramEndorseExpirationDate)


        Try
            CheckDBConnection()
            dbCommand.ExecuteNonQuery()
            If paramEndorseEffectiveDate.Value.ToString <> String.Empty Then
                poEndorseEffectiveDate = paramEndorseEffectiveDate.Value.ToString
            End If
            If paramEndorseExpirationDate.Value.ToString <> String.Empty Then
                poEndorseExpirationDate = paramEndorseExpirationDate.Value.ToString
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Friend Sub GetPremiumFromProduct(ByVal pCertId As Guid,
                                    ByRef pCurrencyCode As String,
                                    ByRef pGrossAmt As Decimal)
        Dim dbCommand As OracleCommand = DirectCast(Database.Connection.CreateCommand(), OracleCommand)

        dbCommand.CommandType = CommandType.StoredProcedure
        dbCommand.CommandText = "elp_ws_getcertificate.get_coverage_rate_details "


        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_cert_id", .OracleDbType = OracleDbType.Raw, .Size = 16, .Direction = ParameterDirection.Input, .Value = pCertId})

        'output parameters
        Dim paramCurrencyCode As OracleParameter
        paramCurrencyCode = New OracleParameter() With {.ParameterName = "po_currency_code", .OracleDbType = OracleDbType.Varchar2, .Direction = ParameterDirection.Output, .Size = 20}
        dbCommand.Parameters.Add(paramCurrencyCode)
        Dim paramGrossAmt As OracleParameter
        paramGrossAmt = New OracleParameter() With {.ParameterName = "po_gross_amt", .OracleDbType = OracleDbType.Decimal, .Direction = ParameterDirection.Output, .Size = 16}
        dbCommand.Parameters.Add(paramGrossAmt)

        CheckDBConnection()
        dbCommand.ExecuteNonQuery()


        pCurrencyCode = paramCurrencyCode.Value.ToString()
        pGrossAmt = Convert.ToDecimal(paramGrossAmt.Value.ToString())
        Database.Connection.Close()

    End Sub


    Friend Function SearchCertificateBYCustomerInfo(ByVal pCompanyCode As String, ByVal pDealerCode As String, ByVal pDealerGrp As String, ByVal pCustomerFirstName As String, ByVal pCustomerLastName As String,
                                                ByVal pWorkPhone As String, ByVal pEmail As String, ByVal pPostalCode As String, ByVal pIdentificationNumber As String) As DataSet

        Dim dbCommand As OracleCommand = DirectCast(Database.Connection.CreateCommand(), OracleCommand)
        dbCommand.CommandType = CommandType.StoredProcedure

        dbCommand.CommandText = "elp_ws_getCertificate.SearchPolicyByCustomerInfo"

        dbCommand.BindByName = True
       dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_company_code", .OracleDbType = OracleDbType.Varchar2, .Value = pCompanyCode})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_dealer_code", .OracleDbType = OracleDbType.Varchar2, .Value = pDealerCode})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_dealer_group", .OracleDbType = OracleDbType.Varchar2, .Value = pDealerGrp})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_first_name", .OracleDbType = OracleDbType.Varchar2, .Value = pCustomerFirstName})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_last_name", .OracleDbType = OracleDbType.Varchar2, .Value = pCustomerLastName})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_email", .OracleDbType = OracleDbType.Varchar2, .Value = pEmail})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_phone_number", .OracleDbType = OracleDbType.Varchar2, .Value = pWorkPhone})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_postal_code", .OracleDbType = OracleDbType.Varchar2, .Value = pPostalCode})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_identification_number", .OracleDbType = OracleDbType.Varchar2, .Value = pIdentificationNumber})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "po_cert_table", .OracleDbType = OracleDbType.RefCursor, .Direction = ParameterDirection.Output})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "po_return_code", .OracleDbType = OracleDbType.Int64, .Direction = ParameterDirection.Output})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "po_exception_msg", .OracleDbType = OracleDbType.Int64, .Direction = ParameterDirection.Output})

        Dim dbAdapter As OracleDataAdapter = New OracleDataAdapter(dbCommand)
        Dim dsCertList As New DataSet
        Try
            CheckDBConnection()
            dbAdapter.Fill(dsCertList)

            Catch ex As Exception
            'Throw ex
        End Try
                  
       
        Return dsCertList
    
    End Function

   
End Class
