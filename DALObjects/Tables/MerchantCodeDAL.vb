'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (7/13/2010)********************


Public Class MerchantCodeDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_MERCHANT_CODE"
    Public Const TABLE_KEY_NAME As String = "merchant_code_id"

    Public Const COL_NAME_MERCHANT_CODE_ID As String = "merchant_code_id"
    Public Const COL_NAME_COMPANY_CREDIT_CARD_ID As String = "company_credit_card_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_MERCHANT_CODE As String = "merchant_code"

    Private Const DSNAME As String = "LIST"
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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("merchant_code_id", id.ToByteArray)}
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
        If Not ds.Tables(TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region

#Region "CRUD Methods"

    Public Function LoadList(languageId As Guid, dealerId As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_LIST")

        Dim parameters() As OracleParameter
        parameters = New OracleParameter() {New OracleParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray), _
                                            New OracleParameter(COL_NAME_DEALER_ID, dealerId.ToByteArray)}

        Try
            Return (DBHelper.Fetch(selectStmt, DSNAME, TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    'Public Function VerifyDuplicateCreditCardType(ByVal creditCardId As Guid, ByVal contractId As Guid) As DataSet

    '    Dim selectStmt As String = Me.Config("/SQL/VERIFY_DUPLICATE_CREDIT_CARD_TYPE")

    '    Dim parameters() As OracleParameter
    '    parameters = New OracleParameter() {New OracleParameter(COL_NAME_COMPANY_CREDIT_CARD_ID, creditCardId.ToByteArray), _
    '                                        New OracleParameter(COL_NAME_CONTRACT_ID, contractId.ToByteArray)}

    '    Try
    '        Return (DBHelper.Fetch(selectStmt, DSNAME, Me.TABLE_NAME, parameters))
    '    Catch ex As Exception
    '        Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
    '    End Try

    'End Function

#End Region
End Class


