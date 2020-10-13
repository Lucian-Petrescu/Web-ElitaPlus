'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (10/30/2008)********************


Public Class CertInstallmentDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_CERT_INSTALLMENT"
    Public Const TABLE_KEY_NAME As String = "cert_installment_id"

    Public Const COL_NAME_CERT_INSTALLMENT_ID As String = "cert_installment_id"
    Public Const COL_NAME_BILLING_FREQUENCY_ID As String = "billing_frequency_id"
    Public Const COL_NAME_BILLING_STATUS_ID As String = "billing_status_id"
    Public Const COL_NAME_BANK_INFO_ID As String = "bank_info_id"
    Public Const COL_NAME_NUMBER_OF_INSTALLMENTS As String = "number_of_installments"
    Public Const COL_NAME_INSTALLMENT_AMOUNT As String = "installment_amount"
    Public Const COL_NAME_PAYMENT_DUE_DATE As String = "payment_due_date"
    Public Const COL_NAME_DATE_LETTER_SENT As String = "date_letter_sent"
    Public Const COL_NAME_SEND_LETTER_ID As String = "send_letter_id"
    Public Const COL_NAME_CERT_ID As String = "cert_id"
    Public Const COL_NAME_CANCELLATION_DUE_DATE As String = "cancellation_due_date"
    Public Const COL_NAME_CREDIT_CARD_INFO_ID As String = "credit_card_info_id"
    'Req-1016 Start
    Public Const COL_NAME_NEXT_BILLING_DATE As String = "next_billing_date"
    'Req-1016 End


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

    Public Sub Load(familyDS As DataSet, id As Guid, Optional ByVal useCertId As Boolean = False)
        Dim selectStmt As String = Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter

        If useCertId Then
            selectStmt = Config("/SQL/LOAD_BY_CERT_ID")
            parameters = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("cert_id", id.ToByteArray)}
        Else
            parameters = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("cert_installment_id", id.ToByteArray)}
        End If

        'Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("cert_installment_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub


    Public Sub LoadByCertId(familyDS As DataSet, certId As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD_BY_CERT_ID")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("cert_id", certId.ToByteArray)}
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

    Public Overloads Sub UpdateFamily(familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)

        Dim tr As IDbTransaction = Transaction

        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            'First Pass updates Deletions
            MyBase.Update(familyDataset.Tables(TABLE_NAME).GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)

            If familyDataset.Tables(CreditCardInfoDAL.TABLE_NAME) IsNot Nothing AndAlso familyDataset.Tables(CreditCardInfoDAL.TABLE_NAME).Rows.Count > 0 Then
                Dim cciDal As New CreditCardInfoDAL
                cciDal.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
            End If

            If familyDataset.Tables(BankInfoDAL.TABLE_NAME) IsNot Nothing AndAlso familyDataset.Tables(BankInfoDAL.TABLE_NAME).Rows.Count > 0 Then
                Dim biDal As New BankInfoDAL
                biDal.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
            End If

            'Second Pass updates additions and changes
            Update(familyDataset.Tables(TABLE_NAME).GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)

            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                DBHelper.Commit(tr)
                familyDataset.AcceptChanges()
            End If
        Catch ex As Exception
            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                DBHelper.RollBack(tr)
            End If
            Throw ex
        End Try
    End Sub
#End Region

End Class


