'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (1/31/2013)********************

Imports Assurant.ElitaPlus.DALObjects.DBHelper
Public Class DailyObdFileDetailTempDAL
    Inherits DALBase


#Region "Constants"
   Public Const TABLE_NAME As String = "ELP_DAILY_OBD_FILE_DETAIL_TEMP"
    Public Const TABLE_KEY_NAME As String = "file_detail_temp_id"

    Public Const COL_NAME_FILE_DETAIL_TEMP_ID As String = "file_detail_temp_id"
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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("file_detail_temp_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList() As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Return DBHelper.Fetch(selectStmt, TABLE_NAME)
    End Function

    Public Function Load(FromDate As String, ToDate As String, CertNumber As String, SelectOnNewEnrollmnt As String, _
                               SelectOnCancel As String, selectOnBilling As String) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        'Dim whereclausecondition As String = ""


        ''since in our SQL we already have where clause..
        selectStmt &= Environment.NewLine
        ''selectStmt &= Environment.NewLine & "inv.company_id = '" & Me.GuidToSQLString(compIds) & "'"
        'selectStmt &= Environment.NewLine & MiscUtil.BuildListForSql("inv." & Me.COL_NAME_COMPANY_ID, compIds, False)

        'If ((Not (FromDate = "")) AndAlso (Not (ToDate = ""))) Then
        '    selectStmt &= Environment.NewLine & " AND fromdate >= '" & FromDate & "'" & "AND" & Environment.NewLine & " todate <='" & ToDate & "'"

        'End If


        If ((Not (CertNumber Is Nothing)) AndAlso (FormatSearchMask(CertNumber))) Then

            selectStmt &= Environment.NewLine & "AND c.cert_number " & CertNumber

        End If

        'If SelectOnNewEnrollmnt = "Y" Then
        '    selectStmt &= Environment.NewLine & "AND  selection_on_newbusiness ='Y' "
        'End If
        'If SelectOnCancel = "Y" Then
        '    selectStmt &= Environment.NewLine & "AND  selection_on_cancel ='Y' "
        'End If
        'If selectOnBilling = "Y" Then
        '    selectStmt &= Environment.NewLine & "AND   selection_on_billing ='Y' "
        'End If

        Try
            Return (DBHelper.Fetch(selectStmt, TABLE_NAME))
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
        If ds.Tables(TABLE_NAME) IsNot Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region

    Public Sub getrecordslist(CompanyCode As String, Dealercode As String, CertNumber As String, _
                            selectonNewEnrollment As String, selectoncancel As String, selectonbilling As String, _
                                        fromdate As Date, todate As Date, callfrom As String, _
                                            Optional ByVal processeddate As Date = Nothing, Optional ByVal selectioncertificate As String = "N")
        Dim sqlStmt As String
        sqlStmt = Config("/SQL/Detail_Records_List")
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
    Public Sub deletetemprecord(file_detail_temp_id As Guid)
        Dim sqlStmt As String
        sqlStmt = Config("/SQL/DELETE")
        Dim ds As DataSet
        Try
            'If file_detail_Id.Count > 0 Then
            '    For i As Integer = 0 To file_detail_Id.Count
            Dim inParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                        New DBHelper.DBHelperParameter("file_detail_temp_id", GuidToSQLString(file_detail_temp_id))}

            DBHelper.Execute(sqlStmt, inParameters)
            '    Next
            'End If

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try


    End Sub
    'New DBHelper.DBHelperParameter("ProcessedDate", processeddate), _
    'New DBHelper.DBHelperParameter("SelectionOnCertificate", selectioncertificate), _
    'New DBHelper.DBHelperParameter("CalledFrom", callfrom) _
End Class



