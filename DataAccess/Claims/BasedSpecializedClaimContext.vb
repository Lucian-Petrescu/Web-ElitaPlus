Imports Oracle.ManagedDataAccess.Client
Imports Oracle.ManagedDataAccess.Types

Public MustInherit Class BasedSpecializedClaimContext
    Inherits BaseDbContext
    Public Sub New()
        MyBase.New("Claims.ClaimDataModel")
    End Sub

    Private Sub CheckDBConnection()
        If Me.Database.Connection.State = ConnectionState.Closed Then
            Database.Connection.Open()
        End If
    End Sub

    Protected MustOverride ReadOnly Property CustomizationName() As String

    Public Overridable Function GetCertificateClaimInfo(ByVal pCompanyCode As String,
                                                       ByVal pCertificateNumber As String,
                                                       ByVal pSerialNumber As String,
                                                       ByVal pPhoneNumber As String,
                                                       ByVal pUserId As Guid,
                                                       ByVal pLanguageId As Guid,
                                                       ByRef pErrorCode As String,
                                                       ByRef pErrorMessage As String) As DataSet
        Dim dsCertList As New DataSet

        Dim dbCommand As OracleCommand = DirectCast(Database.Connection.CreateCommand(), OracleCommand)
        dbCommand.CommandType = CommandType.StoredProcedure
        'elita.elp_ws_sps_timb_certclaim.GetCertClaimInfo
        Dim commandTxt As String = String.Concat("elp_ws_sps_",
                                                    CustomizationName,
                                                    "_certclaim.GetCertClaimInfo")

        dbCommand.CommandText = commandTxt

        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_company_code", .OracleDbType = OracleDbType.Varchar2, .Value = pCompanyCode})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_cert_number", .OracleDbType = OracleDbType.Varchar2, .Value = pCertificateNumber})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_serial_number", .OracleDbType = OracleDbType.Varchar2, .Value = pSerialNumber})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_phone_number", .OracleDbType = OracleDbType.Varchar2, .Value = pPhoneNumber})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_user_id", .OracleDbType = OracleDbType.Blob, .Value = pUserId.ToByteArray})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "pi_language_id", .OracleDbType = OracleDbType.Blob, .Value = pLanguageId.ToByteArray})

        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "po_certificate_info", .OracleDbType = OracleDbType.RefCursor, .Direction = ParameterDirection.Output})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "po_claim_info", .OracleDbType = OracleDbType.RefCursor, .Direction = ParameterDirection.Output})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "po_error_code", .OracleDbType = OracleDbType.Varchar2, .Direction = ParameterDirection.Output, .Size = 200})
        dbCommand.Parameters.Add(New OracleParameter() With {.ParameterName = "po_error_message", .OracleDbType = OracleDbType.Varchar2, .Direction = ParameterDirection.Output, .Size = 200})
        Dim dbAdapter As OracleDataAdapter = New OracleDataAdapter(dbCommand)

        Try
            dbAdapter.Fill(dsCertList)

            Dim error_code As OracleString = dbCommand.Parameters("po_error_code").Value

            If (error_code.IsNull OrElse error_code.ToString = String.Empty) Then

                dsCertList.Tables(0).TableName = "CertificateInfo"
                dsCertList.Tables(1).TableName = "ClaimInfo"
            Else
                Dim error_message As OracleString = dbCommand.Parameters("po_error_message").Value

                pErrorCode = error_code.ToString
                pErrorMessage = error_message.ToString
            End If

            Return dsCertList
        Catch ex As Exception
            Throw New DataException("Error trying to access the Database", ex)
        End Try





        'Dim ds As New DataSet

        'Try
        '    DBHelper.FetchSp(selectStmt, inParameters, outParameters, ds, "CertClaimInfoResponse")

        '    If String.IsNullOrEmpty(outParameters(2).Value) Then
        '        ds.Tables(0).TableName = "CertificateInfo"
        '        ds.Tables(1).TableName = "ClaimInfo"
        '    Else
        '        ErrorCode = outParameters(2).Value
        '        ErrorMessage = outParameters(3).Value
        '    End If
        '    Return ds
        'Catch ex As Exception
        '    Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        'End Try
    End Function
End Class
