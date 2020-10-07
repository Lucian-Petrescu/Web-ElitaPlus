#Region "BankInfoData"

Public Class BankInfoData

    Public bankinfoId, CountryId, PaymentReasonId, AccountTypeId As Guid
    Public AccountName, SwiftCode, IBAN_Number, BankName As String
    Public BankID, AccountNumber, BranchName As String
    Public AccountDigit, BranchDigit, BranchNumber As Long

End Class

#End Region

Public Class BankInfoDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_BANK_INFO"
    Public Const TABLE_KEY_NAME As String = "bank_info_id"

    Public Const COL_NAME_BANKINFO_ID As String = "bank_info_id"
    Public Const COL_NAME_ACCOUNT_NAME As String = "account_name"
    Public Const COL_NAME_BANK_ID As String = "bank_id"
    Public Const COL_NAME_ACCOUNT_NUMBER As String = "account_number"
    Public Const COL_NAME_ACCOUNT_NUMBER_LAST4DIGITS As String = "account_number_last4digits"
    Public Const COL_NAME_COUNTRY_ID As String = "country_id"
    Public Const COL_NAME_PAYMENT_REASON_ID As String = "payment_reason_id"
    Public Const COL_NAME_BANK_NAME As String = "bank_name"
    Public Const COL_NAME_BRANCH_NAME As String = "branch_name"
    Public Const COL_NAME_BANK_SORT_CODE As String = "bank_sort_code"
    Public Const COL_NAME_IBAN_NUMBER As String = "iban_number"
    Public Const COL_NAME_IBAN_NUMBER_LAST4DIGITS As String = "iban_number_last4digits"
    Public Const COL_NAME_SWIFT_CODE As String = "swift_code"
    Public Const COL_NAME_ACCOUNT_TYPE_ID As String = "account_type_id"

    Public Const COL_NAME_BANK_LOOKUP_CODE As String = "bank_lookup_code"
    Public Const COL_NAME_TRANSACTION_LIMIT As String = "transaction_limit"
    Public Const COL_NAME_BANK_SUB_CODE As String = "bank_sub_code"

    Public Const COL_NAME_BRANCH_DIGIT As String = "branch_digit"
    Public Const COL_NAME_ACCOUNT_DIGIT As String = "account_digit"
    Public Const COL_NAME_BRANCH_NUMBER As String = "branch_number"
    Public Const COL_NAME_TAX_ID As String = "tax_id"

    Public Const COL_NAME_ADDRESS_ID As String = "address_id"

#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.New()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(familyDS As DataSet, id As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("bank_info_id", id.ToByteArray)}
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


#Region "StoreProcedures Control"

    ' Execute Store Procedure
    Public Function IbanIsValid(ibanNumber As String, countryCode As String) As Boolean
        Dim inputParameters(1) As DBHelper.DBHelperParameter
        Dim sql As String = Config("/SQL/VALIDATE_IBAN_NUMBER")

        inputParameters(0) = New DBHelper.DBHelperParameter("p_iban", ibanNumber)
        inputParameters(1) = New DBHelper.DBHelperParameter("p_country_code", countryCode)

        ' Call DBHelper Store Procedure
        Dim returnValue As Int32 = DBHelper.ExecuteNonQuery(sql, inputParameters)

        Return returnValue


    End Function
#End Region


#Region "Overloaded Methods"
    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        Try
            If ds Is Nothing Then
                Return
            End If
            If Not ds.Tables(TABLE_NAME) Is Nothing Then
                MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
            End If
        Catch ex As DataBaseIntegrityConstraintViolation
            'DEF-1128 - ALR - Added the exception handler in case the bankinfo object belongs to another svc center, dealer, etc..
            Return
        End Try
    End Sub

    Public Overloads Sub UpdateAddress(familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)
        Dim addressDAL As New AddressDAL
        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            addressDAL.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)

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


#End Region

End Class
