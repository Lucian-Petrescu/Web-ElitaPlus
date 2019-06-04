'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (4/19/2007)********************
Imports Assurant.Common.Zip
Imports System.IO

Public Class AcctTransmissionDAL
    Inherits DALBase

#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_ACCT_TRANSMISSION"
    Public Const TABLE_KEY_NAME As String = "acct_transmission_id"
    Public Const DATASET_NAME As String = "ds_acct_transmission"

    Public Const COL_NAME_ACCT_TRANSMISSION_ID As String = "acct_transmission_id"
    Public Const COL_NAME_FILE_NAME As String = "file_name"
    Public Const COL_NAME_FILE_TEXT As String = "file_text"
    Public Const COL_NAME_TRANSMISSION_DATE As String = "transmission_date"
    Public Const COL_NAME_TRANSMISSION_RECEIVED As String = "transmission_received"
    Public Const COL_NAME_TRANSMISSION_COUNT As String = "transmission_count"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_DEBIT_AMOUNT As String = "debit_amount"
    Public Const COL_NAME_CREDIT_AMOUNT As String = "credit_amount"
    Public Const COL_NAME_REJECT_REASON As String = "reject_reason"
    Public Const COL_NAME_FILE_TYPE_FLAG As String = "file_type_flag"
    Public Const COL_NAME_FILE_SUB_TYPE_FLAG As String = "file_sub_type_flag"
    Public Const COL_NAME_NUM_TRANSACTIONS_SENT As String = "num_transactions_sent"
    Public Const COL_NAME_DEBIT_AMOUNT_RECEIVED As String = "debit_amount_received"
    Public Const COL_NAME_CREDIT_AMOUNT_RECEIVED As String = "credit_amount_received"
    Public Const COL_NAME_NUM_TRANSACTIONS_RECEIVED As String = "num_transactions_received"
    Public Const COL_NAME_DATE_RECEIVED As String = "date_received"
    Public Const COL_NAME_REJECT_REASON_DETAIL As String = "reject_reason_detail"
    Public Const COL_NAME_BATCH_NUMBER As String = "batch_number_id"
    Public Const COL_NAME_STATUS_ID As String = "status_id"
    Public Const COL_NAME_JOURNAL_TYPE As String = "journal_type"
    Public Const COL_NAME_FILE_TEXT_COMPRESSED As String = "file_text_compressed"

    Public Const PAR_NAME_RETURN As String = "p_return"
    Public Const PAR_NAME_FILE_NAME As String = "p_file_name"
    Public Const PAR_NAME_FILE_TEXT As String = "p_file_text"
    Public Const PAR_NAME_MODIFIED_BY As String = "p_modified_by"


    Private Const MAX_REVERSAL_DAYS As Integer = 91

    Private Const JOURNAL_FILE_TYPE As Integer = 0

#End Region

#Region "Constructors"

    Public Sub New()
        MyBase.new()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ByVal ds As DataSet)
        Load(ds, Guid.Empty, True)
    End Sub

    Public Sub Load(ByVal familyDS As DataSet, ByVal id As Guid, ByVal OmitText As Boolean)
        Dim selectStmt As String
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("acct_transmission_id", id.ToByteArray)}

        If OmitText Then
            selectStmt = Me.Config("/SQL/LOAD_WITHOUT_TEXT")
        Else
            selectStmt = Me.Config("/SQL/LOAD")
        End If

        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
            If Not OmitText Then
                'Decompressing the FileTextCompressed column data and copying it to FileText column for further use
                If familyDS.Tables.Count > 0 AndAlso familyDS.Tables(0).Rows.Count > 0 Then
                    Dim bytearray() As Byte = CType(familyDS.Tables(0).Rows(0)(AcctTransmissionDAL.COL_NAME_FILE_TEXT_COMPRESSED), Byte())
                    Dim inputmemorystream As New MemoryStream(bytearray)
                    Dim compressionmethod As ICompressionProvider
                    compressionmethod = CompressionProviderFactory.Current.CreateInstance(CompressionProviderType.IonicZip)
                    Dim outputmemorystream As MemoryStream = New MemoryStream()
                    compressionmethod.Uncompress(inputmemorystream, outputmemorystream)
                    familyDS.Tables(0).Rows(0)(AcctTransmissionDAL.COL_NAME_FILE_TEXT) = outputmemorystream.MemoryStreamToString()
                End If
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Sub

    Public Function LoadList() As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
    End Function

    Public Function LoadList(ByVal CompanyId As Guid, ByVal StatusToInclude As ArrayList, ByVal LanguageId As Guid, Optional ByVal JournalOnly As Boolean = True) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = ""
        Dim parameters() As OracleParameter = New OracleParameter() {New OracleParameter("language_id", LanguageId.ToByteArray)}

        whereClauseConditions &= String.Format(" AND {0} =HEXTORAW('{1}')", Me.COL_NAME_COMPANY_ID, Me.GuidToSQLString(CompanyId))

        If JournalOnly Then
            whereClauseConditions &= String.Format(" AND {0} = {1}", Me.COL_NAME_FILE_TYPE_FLAG, JOURNAL_FILE_TYPE)
        End If

        If Not StatusToInclude Is Nothing AndAlso StatusToInclude.Count > 0 Then
            whereClauseConditions &= String.Format(" AND {0} ", MiscUtil.BuildListForSql(COL_NAME_STATUS_ID, StatusToInclude, True))
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        selectStmt = selectStmt.Replace(Me.DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, " Order By created_date desc")

        Try
            'Dim ds = New DataSet
            Return (DBHelper.Fetch(selectStmt, Me.DATASET_NAME, Me.TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadList(ByVal ParentFileName As String, ByVal ParentBatchNumber As String, ByVal RejectReason As String, ByVal StatusToOmit As Guid, ByVal LanguageId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = ""
        Dim parameters() As OracleParameter = New OracleParameter() {New OracleParameter("language_id", LanguageId.ToByteArray)}

        whereClauseConditions &= String.Format(" AND UPPER({0}) ='{1}'", Me.COL_NAME_BATCH_NUMBER, ParentBatchNumber)
        whereClauseConditions &= String.Format(" AND NOT UPPER({0}) = UPPER('{1}')", Me.COL_NAME_FILE_NAME, ParentFileName)

        If (Not (RejectReason Is Nothing)) AndAlso (RejectReason.Trim.Length > 0) Then
            whereClauseConditions &= String.Format(" AND UPPER({0}) = '{1}'", Me.COL_NAME_REJECT_REASON, RejectReason.ToUpper)
        End If

        If Not StatusToOmit.Equals(Guid.Empty) Then
            whereClauseConditions &= String.Format(" AND NOT {0} = HEXTORAW('{1}') ", Me.COL_NAME_STATUS_ID, Me.GuidToSQLString(StatusToOmit))
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        selectStmt = selectStmt.Replace(Me.DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, " Order By created_date desc")

        Try
            Return (DBHelper.Fetch(selectStmt, Me.DATASET_NAME, Me.TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadList(ByVal CompanyId As Guid, ByVal StartDate As Date, ByVal EndDate As Date, ByVal BatchNumber As String, ByVal StatusToOmit As Guid, ByVal LanguageId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = ""
        Dim parameters() As OracleParameter = New OracleParameter() {New OracleParameter("language_id", LanguageId.ToByteArray)}

        whereClauseConditions &= String.Format(" AND {0} =HEXTORAW('{1}')", Me.COL_NAME_COMPANY_ID, Me.GuidToSQLString(CompanyId))
        whereClauseConditions &= String.Format(" AND {0} is null ", Me.COL_NAME_REJECT_REASON)
        whereClauseConditions &= String.Format(" AND UPPER({0}) like '%JOURNAL%' ", Me.COL_NAME_FILE_NAME)
        whereClauseConditions &= String.Format(" AND ELP_ACCT_TRANSMISSION.{0} > SYSDATE - {1} ", Me.COL_NAME_CREATED_DATE, MAX_REVERSAL_DAYS)

        If (Not (BatchNumber Is Nothing)) AndAlso (BatchNumber.Trim.Length > 0) Then
            whereClauseConditions &= String.Format(" AND UPPER({0}) like '%{1}.XML'", Me.COL_NAME_FILE_NAME, BatchNumber.ToUpper)
        Else
            If Not StartDate.Equals(Date.MinValue) Then
                whereClauseConditions &= String.Format(" AND ELP_ACCT_TRANSMISSION.{0} > TO_DATE('{1}','MM/DD/YYYY')", Me.COL_NAME_CREATED_DATE, StartDate.ToString("MM/dd/yyyy"))
            End If
            If Not EndDate.Equals(Date.MinValue) Then
                whereClauseConditions &= String.Format(" AND ELP_ACCT_TRANSMISSION.{0} < TO_DATE('{1}','MM/DD/YYYY')", Me.COL_NAME_CREATED_DATE, EndDate.ToString("MM/dd/yyyy"))
            End If
        End If

        If Not StatusToOmit.Equals(Guid.Empty) Then
            whereClauseConditions &= String.Format(" AND NOT {0} = HEXTORAW('{1}') ", Me.COL_NAME_STATUS_ID, Me.GuidToSQLString(StatusToOmit))
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        selectStmt = selectStmt.Replace(Me.DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, " Order By ELP_ACCT_TRANSMISSION.created_date desc")

        Try
            Return (DBHelper.Fetch(selectStmt, Me.DATASET_NAME, Me.TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadList(ByVal CompanyId As Guid, ByVal FileName As String, ByVal AcctTransmissionId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_TRANSMISSION_LIST")
        Dim parameters() As OracleParameter = New OracleParameter() {New OracleParameter("company_id", CompanyId.ToByteArray),
                                                                     New OracleParameter("file_name", FileName),
                                                                     New OracleParameter("acct_transmission_id", AcctTransmissionId.ToByteArray)}

        selectStmt = selectStmt.Replace(Me.DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, " Order By ELP_ACCT_TRANSMISSION.created_date desc")

        Try
            Return (DBHelper.Fetch(selectStmt, Me.DATASET_NAME, Me.TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

#End Region

#Region "XML Migration Methods"
    Public Sub MigrateXML(ByVal rowcount As Integer, ByVal modified_by As String)

        Dim readStmt As String = Me.Config("/SQL/LOAD_UNCOMPRESSED_LIST")
        Dim updateStmt As String = Me.Config("/SQL/UPDATE_COMPRESSED_XML")

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
        {New DBHelper.DBHelperParameter("row_count", rowcount)}

        Try
            DBHelper.MigrateXML(readStmt, parameters, updateStmt, modified_by)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Sub

    Public Function TransmissionsToMigrate() As Integer
        Dim stmt As String = Me.Config("/SQL/TRANSMISSIONSTOMIGRATE")

        Try
            Return CType(DBHelper.ExecuteScalar(stmt), Integer)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(Me.TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region

#Region "Public Methods"

    Public Sub InsertCustom(ByVal CurrentRow As DataRow, ByVal CreatedBy As String)

        Dim conn As OracleConnection = DBHelper.GetConnection
        Dim tr As IDbTransaction = DBHelper.GetNewTransaction(conn)

        Dim selectStmt As String = Me.Config("/SQL/INSERT")

        'Compressing the data before copying it to ROW
        Dim compressionmethod As ICompressionProvider
        compressionmethod = CompressionProviderFactory.Current.CreateInstance(CompressionProviderType.IonicZip)
        Dim outputmemorystream As MemoryStream = New MemoryStream()
        compressionmethod.Compress(CurrentRow(Me.COL_NAME_FILE_TEXT).ToString(), outputmemorystream)

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                            {New DBHelper.DBHelperParameter(Me.COL_NAME_FILE_NAME, CurrentRow(Me.COL_NAME_FILE_NAME).ToString), _
                            New DBHelper.DBHelperParameter(Me.COL_NAME_FILE_TEXT_COMPRESSED, outputmemorystream.ToArray()), _
                            New DBHelper.DBHelperParameter(Me.COL_NAME_TRANSMISSION_DATE, If(IsDate(CurrentRow(Me.COL_NAME_TRANSMISSION_DATE)), CurrentRow(Me.COL_NAME_TRANSMISSION_DATE), Date.MinValue)), _
                            New DBHelper.DBHelperParameter(Me.COL_NAME_TRANSMISSION_COUNT, Integer.Parse(CurrentRow(Me.COL_NAME_TRANSMISSION_COUNT))), _
                            New DBHelper.DBHelperParameter(Me.COL_NAME_DEBIT_AMOUNT, If(IsDBNull(CurrentRow(Me.COL_NAME_DEBIT_AMOUNT)), 0, CurrentRow(Me.COL_NAME_DEBIT_AMOUNT))), _
                            New DBHelper.DBHelperParameter(Me.COL_NAME_CREDIT_AMOUNT, If(IsDBNull(CurrentRow(Me.COL_NAME_CREDIT_AMOUNT)), 0, CurrentRow(Me.COL_NAME_CREDIT_AMOUNT))), _
                            New DBHelper.DBHelperParameter(Me.COL_NAME_REJECT_REASON, CurrentRow(Me.COL_NAME_REJECT_REASON).ToString), _
                            New DBHelper.DBHelperParameter(Me.COL_NAME_COMPANY_ID, CType(CurrentRow(Me.COL_NAME_COMPANY_ID), Byte())), _
                            New DBHelper.DBHelperParameter(Me.COL_NAME_CREATED_BY, CreatedBy), _
                            New DBHelper.DBHelperParameter(Me.COL_NAME_FILE_TYPE_FLAG, Integer.Parse(CurrentRow(Me.COL_NAME_FILE_TYPE_FLAG))), _
                            New DBHelper.DBHelperParameter(Me.COL_NAME_FILE_SUB_TYPE_FLAG, Integer.Parse(CurrentRow(Me.COL_NAME_FILE_SUB_TYPE_FLAG))), _
                            New DBHelper.DBHelperParameter(Me.COL_NAME_NUM_TRANSACTIONS_SENT, If(IsDBNull(CurrentRow(Me.COL_NAME_NUM_TRANSACTIONS_SENT)), 0, Integer.Parse(CurrentRow(Me.COL_NAME_NUM_TRANSACTIONS_SENT)))), _
                            New DBHelper.DBHelperParameter(Me.COL_NAME_DEBIT_AMOUNT_RECEIVED, If(IsDBNull(CurrentRow(Me.COL_NAME_DEBIT_AMOUNT_RECEIVED)), 0, CurrentRow(Me.COL_NAME_DEBIT_AMOUNT_RECEIVED))), _
                            New DBHelper.DBHelperParameter(Me.COL_NAME_CREDIT_AMOUNT_RECEIVED, If(IsDBNull(CurrentRow(Me.COL_NAME_CREDIT_AMOUNT_RECEIVED)), 0, CurrentRow(Me.COL_NAME_CREDIT_AMOUNT_RECEIVED))), _
                            New DBHelper.DBHelperParameter(Me.COL_NAME_NUM_TRANSACTIONS_RECEIVED, If(IsDBNull(CurrentRow(Me.COL_NAME_NUM_TRANSACTIONS_RECEIVED)), 0, Integer.Parse(CurrentRow(Me.COL_NAME_NUM_TRANSACTIONS_RECEIVED)))), _
                            New DBHelper.DBHelperParameter(Me.COL_NAME_DATE_RECEIVED, If(IsDate(CurrentRow(Me.COL_NAME_DATE_RECEIVED)), CurrentRow(Me.COL_NAME_DATE_RECEIVED), Date.MinValue)), _
                            New DBHelper.DBHelperParameter(Me.COL_NAME_REJECT_REASON_DETAIL, CurrentRow(Me.COL_NAME_REJECT_REASON_DETAIL).ToString, GetType(System.Text.StringBuilder)), _
                            New DBHelper.DBHelperParameter(Me.COL_NAME_BATCH_NUMBER, If(IsDBNull(CurrentRow(Me.COL_NAME_BATCH_NUMBER)), "", CurrentRow(Me.COL_NAME_BATCH_NUMBER).ToString)), _
                            New DBHelper.DBHelperParameter(Me.COL_NAME_ACCT_TRANSMISSION_ID, CType(CurrentRow(Me.COL_NAME_ACCT_TRANSMISSION_ID), Byte())), _
                            New DBHelper.DBHelperParameter(Me.COL_NAME_STATUS_ID, CType(CurrentRow(Me.COL_NAME_STATUS_ID), Byte())), _
                            New DBHelper.DBHelperParameter(Me.COL_NAME_JOURNAL_TYPE, CurrentRow(Me.COL_NAME_JOURNAL_TYPE).ToString)}

        Try
            DBHelper.ExecuteWithParam(selectStmt, parameters, tr)
            If Not tr Is Nothing Then
                'In Case of sucess, commit the transaction
                DBHelper.Commit(tr)
            End If
        Catch ex As Exception
            If Not tr Is Nothing Then
                'In Case of error, rollback the transaction
                DBHelper.RollBack(tr)
            End If
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    'Override to the delete method as we don't want to delete records, only delete virtually.
    Public Sub DeleteCustom(ByVal AcctTransmissionId As Guid, ByVal DeletedStatusId As Guid)

        Dim selectStmt As String = Me.Config("/SQL/DELETE")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                           {New DBHelper.DBHelperParameter(Me.COL_NAME_STATUS_ID, DeletedStatusId.ToByteArray), _
                           New DBHelper.DBHelperParameter(Me.COL_NAME_ACCT_TRANSMISSION_ID, AcctTransmissionId.ToByteArray)}

        Try
            DBHelper.ExecuteWithParam(selectStmt, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Sub

#End Region

End Class


