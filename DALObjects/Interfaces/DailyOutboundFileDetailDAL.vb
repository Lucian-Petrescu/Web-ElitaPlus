'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (1/31/2013)********************


Public Class DailyOutboundFileDetailDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_DAILY_OUTBOUND_FILE_DETAIL"
    Public Const TABLE_KEY_NAME As String = "file_detail_id"

    Public Const COL_NAME_FILE_DETAIL_ID As String = "file_detail_id"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_CERT_ID As String = "cert_id"
    Public Const COL_NAME_CERT_NUMBER As String = "cert_number"
    Public Const COL_NAME_CERT_CREATED_DATE As String = "cert_created_date"
    Public Const COL_NAME_RECORD_TYPE As String = "record_type"
    Public Const COL_NAME_REC_CANCEL As String = "rec_cancel"
    Public Const COL_NAME_REC_NEW_BUSINESS As String = "rec_new_business"
    Public Const COL_NAME_REC_BILLING As String = "rec_billing"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("file_detail_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList() As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Try
            Return DBHelper.Fetch(selectStmt, TABLE_NAME)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function


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

#Region "Insert Detail Records"
    Public Sub insertdetailrecords(Company_id As Guid, Dealer_id As Guid, cert_id As Guid, CertNumber As String, certCreatedDate As Date, _
                                           selectonNewEnrollment As String, selectoncancel As String, selectonbilling As String, _
                                           recordType As String, createdDate As Date, createdBy As String, billing_detail_id As Guid, _
                                            Optional ByVal selectioncertificate As String = "")
        Dim sqlStmt As String
        sqlStmt = Config("/SQL/INSERT")
        Dim ds As DataSet
        Try
            Dim file_detail_id As Guid = Guid.NewGuid()

            Dim inParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
            New DBHelper.DBHelperParameter("file_detail_id", GuidToSQLString(file_detail_id)), _
            New DBHelper.DBHelperParameter("billing_detail_id", GuidToSQLString(billing_detail_id)), _
            New DBHelper.DBHelperParameter("company_id", GuidToSQLString(Company_id)), _
            New DBHelper.DBHelperParameter("dealer_id", GuidToSQLString(Dealer_id)), _
            New DBHelper.DBHelperParameter("cert_id", cert_id), _
            New DBHelper.DBHelperParameter("cert_number", CertNumber), _
            New DBHelper.DBHelperParameter("cert_created_date", certCreatedDate), _
            New DBHelper.DBHelperParameter("rec_cancel", selectoncancel), _
            New DBHelper.DBHelperParameter("rec_new_business", selectonNewEnrollment), _
            New DBHelper.DBHelperParameter("rec_billing", selectonbilling), _
            New DBHelper.DBHelperParameter("record_type", recordType), _
            New DBHelper.DBHelperParameter("created_date", createdDate), _
            New DBHelper.DBHelperParameter("created_by", createdBy)}

            DBHelper.Execute(sqlStmt, inParameters)
            'ExecuteSPCreateInvoice(DealerID, userNetworkId, sqlStmt)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try


    End Sub

    Public Sub getrecordsviewlist(CompanyCode As String, Dealercode As String, CertNumber As String, _
                            selectonNewEnrollment As String, selectoncancel As String, selectonbilling As String, _
                                        fromdate As Date, todate As Date, callfrom As String, _
                                            Optional ByVal processeddate As Date = Nothing, Optional ByVal selectioncertificate As String = "N")
        Dim sqlStmt As String
        sqlStmt = Config("/SQL/Detail_Records_View_List")
        Dim ds As DataSet
        Try

            Dim inParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
    New DBHelper.DBHelperParameter("CompanyCode", CompanyCode), _
    New DBHelper.DBHelperParameter("DealerCode", Dealercode), _
    New DBHelper.DBHelperParameter("SelectionOnCancel", selectoncancel), _
    New DBHelper.DBHelperParameter("SelectionOnBilling", selectonbilling), _
    New DBHelper.DBHelperParameter("SelectionOnNewBusiness", selectonNewEnrollment), _
    New DBHelper.DBHelperParameter("CertificateNumber", CertNumber), _
    New DBHelper.DBHelperParameter("DateRangeFrom", fromdate), _
    New DBHelper.DBHelperParameter("DateRangeTo", todate)}


            'New DBHelper.DBHelperParameter("SelectionOnNewBusiness", selectonbilling), _
            'New DBHelper.DBHelperParameter("CertificateNumber", Convert.ToDateTime(processeddate)), _
            Dim outParameters() As DBHelper.DBHelperParameter
            DBHelper.ExecuteSp(sqlStmt, inParameters, outParameters)
            'ExecuteSPCreateInvoice(DealerID, userNetworkId, sqlStmt)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
#End Region
    Public Sub deletedetailrecord(file_detail_Id As Guid)
        Dim sqlStmt As String
        sqlStmt = Config("/SQL/DELETE")
        Dim ds As DataSet
        Try
            'If file_detail_Id.Count > 0 Then
            '    For i As Integer = 0 To file_detail_Id.Count
            Dim inParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                        New DBHelper.DBHelperParameter("file_detail_id", GuidToSQLString(file_detail_Id))}

            DBHelper.Execute(sqlStmt, inParameters)
            '    Next
            'End If

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try


    End Sub

End Class



