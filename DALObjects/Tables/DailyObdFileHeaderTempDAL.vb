'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (1/9/2013)********************


Public Class DailyObdFileHeaderTempDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_DAILY_OBD_FILE_HEADER_TEMP"
    Public Const TABLE_KEY_NAME As String = "file_header_temp_id"

    Public Const COL_NAME_FILE_HEADER_TEMP_ID As String = "file_header_temp_id"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_CERT_NUMBER As String = "cert_number"
    Public Const COL_NAME_FROM_DATE As String = "from_date"
    Public Const COL_NAME_TO_DATE As String = "to_date"
    Public Const COL_NAME_SELECTION_ON_CREATED_DATE As String = "selection_on_created_date"
    Public Const COL_NAME_SELECTION_ON_MODIFIED_DATE As String = "selection_on_modified_date"
    Public Const COL_NAME_SELECTION_ON_CERT As String = "selection_on_cert"
    Public Const COL_NAME_SELECTION_ON_CANCEL As String = "selection_on_cancel"
    Public Const COL_NAME_SELECTION_ON_BILLING As String = "selection_on_billing"
    Public Const COL_NAME_SELECTION_ON_NEWBUSINESS As String = "selection_on_newbusiness"

#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.new()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ByVal ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(ByVal familyDS As DataSet, ByVal id As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("file_header_temp_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList() As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")

        selectStmt &= Environment.NewLine

        selectStmt &= "AND D.Record_Selected = 'Y'"
        Try
            Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function Load(ByVal FromDate As String, ByVal ToDate As String, ByVal certNumber As String, ByVal SelectOnCreatedDate As String, _
                           ByVal SelectOnModifiedDate As String, ByVal SelectOnNewEnrollmnt As String, _
                           ByVal SelectOnCancel As String, ByVal selectOnBilling As String) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        'Dim whereclausecondition As String = ""


        ''since in our SQL we already have where clause..
        selectStmt &= Environment.NewLine
        ''selectStmt &= Environment.NewLine & "inv.company_id = '" & Me.GuidToSQLString(compIds) & "'"
        'selectStmt &= Environment.NewLine & MiscUtil.BuildListForSql("inv." & Me.COL_NAME_COMPANY_ID, compIds, False)

        If ((Not (FromDate = "")) AndAlso (Not (ToDate = ""))) Then
            selectStmt &= Environment.NewLine & " AND fromdate >= '" & FromDate & "'" & "AND" & Environment.NewLine & " todate <='" & ToDate & "'"

        End If


        If ((Not (certNumber Is Nothing)) AndAlso (Me.FormatSearchMask(certNumber))) Then

            selectStmt &= Environment.NewLine & "AND D.cert_number " & certNumber

        End If

        'If ((Not (createdDate Is Nothing)) AndAlso (Me.FormatSearchMask(createdDate))) Then
        '    selectStmt &= Environment.NewLine & "AND to_char(inv.created_date,'mmddyyyy')" & createdDate
        'End If

        If SelectOnCreatedDate = "Y" Then
            selectStmt &= Environment.NewLine & "AND  selection_on_created_date ='Y' "
        End If

        If SelectOnModifiedDate = "Y" Then
            selectStmt &= Environment.NewLine & "AND  selection_on_modified_date ='Y' "
        End If
        If SelectOnNewEnrollmnt = "Y" Then
            selectStmt &= Environment.NewLine & "AND  selection_on_newbusiness ='Y' "
        End If
        If SelectOnCancel = "Y" Then
            selectStmt &= Environment.NewLine & "AND  selection_on_cancel ='Y' "
        End If
        If selectOnBilling = "Y" Then
            selectStmt &= Environment.NewLine & "AND   selection_on_billing ='Y' "
        End If

        Try
            Return (DBHelper.Fetch(selectStmt, Me.TABLE_NAME))
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

    Public Sub getrecordslist(ByVal CompanyCode As String, ByVal Dealercode As String, ByVal CertNumber As String, _
                                        ByVal selectoncreateddate As String, ByVal selectonmodifieddate As String, _
                                        ByVal selectonNewEnrollment As String, ByVal selectoncancel As String, ByVal selectonbilling As String, _
                                        ByVal fromdate As Date, ByVal todate As Date, ByVal callfrom As String, _
                                            Optional ByVal processeddate As Date = Nothing, Optional ByVal selectioncertificate As String = "N")
        Dim sqlStmt As String
        sqlStmt = Me.Config("/SQL/Header_Records_List")
        Dim ds As DataSet
        Try

            Dim inParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
    New DBHelper.DBHelperParameter("CompanyCode", "AES"), _
    New DBHelper.DBHelperParameter("DealerCode", "ELCI"), _
    New DBHelper.DBHelperParameter("ProcessedDate", processeddate), _
    New DBHelper.DBHelperParameter("SelectionOnCertificate", selectioncertificate), _
    New DBHelper.DBHelperParameter("SelectionOnCreatedDate", selectoncreateddate), _
    New DBHelper.DBHelperParameter("SelectionOnModifiedDate", selectonmodifieddate), _
    New DBHelper.DBHelperParameter("SelectionOnCancel", selectoncancel), _
    New DBHelper.DBHelperParameter("SelectionOnBilling", selectonbilling), _
    New DBHelper.DBHelperParameter("SelectionOnNewBusiness", selectonNewEnrollment), _
    New DBHelper.DBHelperParameter("CertificateNumber", CertNumber), _
    New DBHelper.DBHelperParameter("DateRangeFrom", fromdate), _
    New DBHelper.DBHelperParameter("DateRangeTo", todate), _
            New DBHelper.DBHelperParameter("CalledFrom", callfrom) _
}
            'New DBHelper.DBHelperParameter("SelectionOnNewBusiness", selectonbilling), _
            'New DBHelper.DBHelperParameter("CertificateNumber", Convert.ToDateTime(processeddate)), _
            Dim outParameters() As DBHelper.DBHelperParameter
            DBHelper.ExecuteSp(sqlStmt, inParameters, outParameters)
            'ExecuteSPCreateInvoice(DealerID, userNetworkId, sqlStmt)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

End Class



