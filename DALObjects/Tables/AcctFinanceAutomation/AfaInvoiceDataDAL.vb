'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (1/6/2015)********************


Public Class AfaInvoiceDataDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_AFA_INVOICE_DATA"
    Public Const TABLE_KEY_NAME As String = "afa_invoice_data_id"

    Public Const COL_NAME_AFA_INVOICE_DATA_ID As String = "afa_invoice_data_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_INVOICE_MONTH As String = "invoice_month"
    Public Const COL_NAME_INVOICE_XML_DATA As String = "invoice_xml_data"
    Public Const COL_NAME_DELETED As String = "deleted"
    Public Const COL_NAME_INVOICE_HTML As String = "invoice_html"
    Public Const COL_NAME_INVOICE_CSV As String = "invoice_csv"
    Public Const COL_NAME_FILENAME As String = "filename"
    Public Const COL_NAME_DIRECTORY_NAME As String = "directory_name"
#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.new()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(familyDS As DataSet, id As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("afa_invoice_data_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    
    Public Function LoadInvoiceData(dealerID As Guid, invoiceMonth As String) As String
        Dim selectStmt As String = Config("/SQL/LOAD_DEALER_INVOICE_DATA")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("dealer_id", dealerID.ToByteArray), _
                                                                                           New DBHelper.DBHelperParameter("invoice_month", invoiceMonth)}

        Return DBHelper.ReadOracleXmlTypeData(selectStmt, parameters)
    End Function

    Public Function LoadActiveInvoice(familyDS As DataSet, dealerID As Guid, invoiceMonth As String) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_ACTIVE_INVOICE")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("dealer_id", dealerID.ToByteArray), _
                                                                                           New DBHelper.DBHelperParameter("invoice_month", invoiceMonth)}
        'Dim ds As New DataSet
        'DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
        'Return ds
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Sub SaveInvoiceHTML(id As Guid, strHTML As String)
        Try
            Dim connection As New OracleConnection(DBHelper.ConnectString)

            Dim strSQL As String = "UPDATE AFA.ELP_AFA_INVOICE_DATA SET invoice_html = :invoice_html where afa_invoice_data_id = :afa_invoice_data_id"

            Dim paramData As New OracleParameter
            paramData.Direction = ParameterDirection.Input
            paramData.OracleDbType = OracleDbType.Clob
            paramData.ParameterName = "invoice_html"
            paramData.Value = strHTML

            Dim paramID As New OracleParameter
            paramID.Direction = ParameterDirection.Input
            paramID.OracleDbType = OracleDbType.Raw
            paramID.ParameterName = "afa_invoice_data_id"
            paramID.Value = id.ToByteArray

            Dim cmd As New OracleCommand
            cmd.Connection = connection
            cmd.Parameters.Add(paramData)
            cmd.Parameters.Add(paramID)
            cmd.CommandText = strSQL
            connection.Open()
            cmd.ExecuteNonQuery()

            paramData = Nothing
            paramID = Nothing
            cmd = Nothing
            connection.Close()
            connection = Nothing
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub


    Public Sub SaveInvoiceCSV(id As Guid, strCSV As String)
        Try
            Dim connection As New OracleConnection(DBHelper.ConnectString)

            Dim strSQL As String = "UPDATE AFA.ELP_AFA_INVOICE_DATA SET invoice_csv = :invoice_csv where afa_invoice_data_id = :afa_invoice_data_id"

            Dim paramData As New OracleParameter
            paramData.Direction = ParameterDirection.Input
            paramData.OracleDbType = OracleDbType.Clob
            paramData.ParameterName = "invoice_csv"
            paramData.Value = strCSV

            Dim paramID As New OracleParameter
            paramID.Direction = ParameterDirection.Input
            paramID.OracleDbType = OracleDbType.Raw
            paramID.ParameterName = "afa_invoice_data_id"
            paramID.Value = id.ToByteArray

            Dim cmd As New OracleCommand
            cmd.Connection = connection
            cmd.Parameters.Add(paramData)
            cmd.Parameters.Add(paramID)
            cmd.CommandText = strSQL
            connection.Open()
            cmd.ExecuteNonQuery()

            paramData = Nothing
            paramID = Nothing
            cmd = Nothing
            connection.Close()
            connection = Nothing
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub

#End Region


End Class


