'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (2/21/2013)********************


Public Class InvoiceDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_INVOICE"
    Public Const TABLE_KEY_NAME As String = "invoice_id"

    Public Const MV_INVOICE_NAME As String = "ELP_INVOICE_EXTENDED_MV"

    Public Const COL_NAME_INVOICE_ID As String = "invoice_id"
    Public Const COL_NAME_SERVICE_CENTER_ID As String = "service_center_id"
    Public Const COL_NAME_INVOICE_NUMBER As String = "invoice_number"
    Public Const COL_NAME_INVOICE_DATE As String = "invoice_date"
    Public Const COL_NAME_INVOICE_AMOUNT As String = "invoice_amount"
    Public Const COL_NAME_INVOICE_STATUS_ID As String = "invoice_status_id"
    Public Const COL_NAME_SOURCE As String = "source"
    Public Const COL_NAME_DUE_DATE As String = "due_date"
    Public Const COL_NAME_IS_COMPLETE_ID As String = "is_complete_id"
    Public Const COL_NAME_SERVICE_CENTER_DESCRIPTION As String = "service_center_description"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("invoice_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(invoiceNumber As SearchCriteriaStringType,
                            invoiceAmount As SearchCriteriaStructType(Of Double),
                            invoiceDate As SearchCriteriaStructType(Of Date),
                            serviceCenterName As String,
                            batchNumber As SearchCriteriaStringType,
                            claimNumber As SearchCriteriaStringType,
                            dateCreated As SearchCriteriaStructType(Of Date),
                            authorizationNumber As SearchCriteriaStringType,
                            userId As Guid,
                            languageId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String
        Dim parameters() As DBHelper.DBHelperParameter =
            New DBHelper.DBHelperParameter() _
            {
                New DBHelper.DBHelperParameter(UserDAL.COL_NAME_USER_ID, userId.ToByteArray()),
                New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray())
            }
        whereClauseConditions &= invoiceNumber.ToSqlString(MV_INVOICE_NAME, COL_NAME_INVOICE_NUMBER, parameters)
        whereClauseConditions &= invoiceAmount.ToSqlString(MV_INVOICE_NAME, COL_NAME_INVOICE_AMOUNT, parameters)
        whereClauseConditions &= invoiceDate.ToSqlString(MV_INVOICE_NAME, COL_NAME_INVOICE_DATE, parameters)
        If ((Not (serviceCenterName Is Nothing)) AndAlso (FormatSearchMask(serviceCenterName))) Then
            whereClauseConditions &= Environment.NewLine & " AND UPPER(" & COL_NAME_SERVICE_CENTER_DESCRIPTION & ")" & serviceCenterName.ToUpper
        End If
        whereClauseConditions &= batchNumber.ToSqlString(MV_INVOICE_NAME, ClaimAuthorizationDAL.COL_NAME_BATCH_NUMBER, parameters)
        whereClauseConditions &= claimNumber.ToSqlString(MV_INVOICE_NAME, ClaimDAL.COL_NAME_CLAIM_NUMBER, parameters)
        whereClauseConditions &= dateCreated.ToSqlString(MV_INVOICE_NAME, COL_NAME_CREATED_DATE, parameters)
        whereClauseConditions &= authorizationNumber.ToSqlString(MV_INVOICE_NAME, ClaimAuthorizationDAL.COL_NAME_AUTHORIZATION_NUMBER, parameters)

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Dim ds As New DataSet
        Try
            ds = DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadAuthorizationList(invoiceId As Guid, languageId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_AUTHORIZATION_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = _
            New DBHelper.DBHelperParameter() _
            { _
                New DBHelper.DBHelperParameter(COL_NAME_INVOICE_ID, invoiceId.ToByteArray()), _
                New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray()) _
            }

        Dim ds As New DataSet
        Try
            ds = DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadAuthorizationList(serviceCenterId As Guid, batchNumber As String) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_AUTHORIZATION_FOR_SELECTION")
        Dim parameters() As DBHelper.DBHelperParameter = _
            New DBHelper.DBHelperParameter() _
            { _
                New DBHelper.DBHelperParameter(COL_NAME_SERVICE_CENTER_ID, serviceCenterId.ToByteArray()), _
                New DBHelper.DBHelperParameter(COL_NAME_SERVICE_CENTER_ID, serviceCenterId.ToByteArray()), _
                New DBHelper.DBHelperParameter(ClaimAuthorizationDAL.COL_NAME_BATCH_NUMBER & "1", batchNumber), _
                New DBHelper.DBHelperParameter(ClaimAuthorizationDAL.COL_NAME_BATCH_NUMBER & "2", batchNumber) _
            }

        Dim ds As New DataSet
        Try
            ds = DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region

#Region "Overloaded Methods"
    Public Overloads Sub UpdateFamily(familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)
        Dim oInvoiceItemDAL As New InvoiceItemDAL
        Dim oClaimAuthorizationDAL As New ClaimAuthorizationDAL
        Dim oClaimAuthItemDAL As New ClaimAuthItemDAL
        Dim oInvoiceReconciliationDAL As New InvoiceReconciliationDAL
        Dim oAttributeValueDAL As New AttributeValueDAL
        Dim oClaimDAL As New ClaimDAL

        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            'First Pass updates Deletions
            oInvoiceReconciliationDAL.Update(familyDataset, tr, DataRowState.Deleted)
            oClaimAuthItemDAL.Update(familyDataset, tr, DataRowState.Deleted)
            oInvoiceItemDAL.Update(familyDataset, tr, DataRowState.Deleted)
            oAttributeValueDAL.Update(familyDataset, tr, DataRowState.Deleted)

            MyBase.Update(familyDataset.Tables(TABLE_NAME), tr, DataRowState.Deleted)

            'Second Pass updates additions and changes
            Update(familyDataset.Tables(TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified)
            oAttributeValueDAL.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)
            oInvoiceItemDAL.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)
            oClaimAuthorizationDAL.Update(familyDataset, tr, DataRowState.Modified)
            oClaimAuthItemDAL.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)
            oInvoiceReconciliationDAL.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)
            oClaimDAL.Update(familyDataset, tr, DataRowState.Modified)

            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                DBHelper.Commit(tr)
            End If
        Catch ex As Exception
            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                DBHelper.RollBack(tr)
            End If
            Throw ex
        End Try
    End Sub

    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If ds.Tables(TABLE_NAME) IsNot Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region

End Class


