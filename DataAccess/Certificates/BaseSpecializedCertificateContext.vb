Imports System.Data.Entity
Imports Oracle.ManagedDataAccess.Client

Public MustInherit Class BaseSpecializedCertificateContext
    Inherits BaseDbContext
    Public Sub New()
        MyBase.New("Certificates.CertificateDataModel")
    End Sub

    Private Sub CheckDBConnection()
        If Me.Database.Connection.State = ConnectionState.Closed Then
            Database.Connection.Open()
        End If
    End Sub

    Protected MustOverride ReadOnly Property CustomizationName() As String

    Friend MustOverride Function SearchCertificateByTaxId(ByVal pCountryCode As String,
                                 ByVal pIdentificationNumber As String,
                                 ByVal pPhoneNumber As String,
                                 ByVal numberOfRecords As Integer,
                                 ByRef totalRecordFound As Long) As DataSet

    Friend MustOverride Sub GetCertificateCoverageRate(ByVal pCertId As Guid,
                                 ByVal pCoverageDate As Date,
                                 ByRef poGWP As Decimal,
                                 ByRef poSalexTax As Decimal)

    Friend MustOverride Sub GetPremiumFromProduct(ByVal pCertId As Guid,
                                    ByRef pCurrencyCode As String,
                                    ByRef pGrossAmt As Decimal)


    Friend MustOverride Function SearchCertificateBYCustomerInfo(ByVal pCompanyCode As String, ByVal pDealerCode As String, ByVal pDealerGrp As String, ByVal pCustomerFirstName As String, ByVal pCustomerLastName As String,
                                                ByVal pWorkPhone As String, ByVal pEmail As String, ByVal pPostalCode As String, ByVal pIdentificationNumber As String) As DataSet

    'US 203684
    Friend Overridable Function SearchCertificate(ByVal pCompanyCode As String,
                                                 ByVal pDealerCode As String,
                                                 ByVal pCertificateNumber As String,
                                                 ByVal pPhoneNumber As String,
                                                 ByVal pSerialNumber As String,
                                                 ByVal pIdentificationNumber As String,
                                                 ByVal pServiceLineNumber As String) As DataSet

        Dim dbCommand As OracleCommand = DirectCast(Database.Connection.CreateCommand(), OracleCommand)
        dbCommand.CommandType = CommandType.StoredProcedure
        Dim commandTxt As String = If(String.IsNullOrEmpty(CustomizationName),
                                                    "elp_ws_getCertificate.Search_Certificate",
                                                    String.Concat("elp_ws_sps_",
                                                                    CustomizationName,
                                                                    "_certcustomer.Search_Certificate"))

        dbCommand.CommandText = commandTxt


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

End Class
