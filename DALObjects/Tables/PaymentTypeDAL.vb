'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (11/6/2006)********************


Public Class PaymentTypeDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_PAYMENT_TYPE"
    Public Const TABLE_KEY_NAME As String = "payment_type_id"

    Public Const COL_NAME_PAYMENT_TYPE_ID As String = "payment_type_id"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_CODE As String = "code"
    Public Const COL_NAME_COLLECTION_METHOD_ID As String = "collection_method_id"
    Public Const COL_NAME_PAYMENT_INSTRUMENT_ID As String = "payment_instrument_id"
    Public Const COL_NAME_COLLECTION_METHOD As String = "collection_method"
    Public Const COL_NAME_PAYMENT_INSTRUMENT As String = "payment_instrument"
    Public Const COL_NAME_COMPANY_GROUP_ID As String = "company_group_id"

    Public Const TABLE_NAME_COLLECTIONS As String = "COLLECTION_METHODS"
    Public Const TABLE_NAME_PAYMENT_INSTRUMENTS As String = "PAYMENT_INSTRUMENTS"
    Public Const TABLE_NAME_PAYMENT_TYPES As String = "PAYMENT_TYPES"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("payment_type_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(familyDS As DataSet, company_group_id As Guid, language_id As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("language_id1", language_id.ToByteArray), _
                            New DBHelper.DBHelperParameter("language_id2", language_id.ToByteArray), _
                            New DBHelper.DBHelperParameter("Company_Group_ID", company_group_id.ToByteArray)}


        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadListForQouteEngine(familyDS As DataSet, company_group_id As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("Company_Group_ID", company_group_id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadCollectionMethodsList(familyDS As DataSet, company_group_id As Guid, language_id As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_COLLECTION_METHODS_LIST_FOR_VSC_QOUTE")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("language_id", language_id.ToByteArray), _
                            New DBHelper.DBHelperParameter("Company_Group_ID", company_group_id.ToByteArray)}


        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME_COLLECTIONS, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadPaymentInstrumentsList(familyDS As DataSet, company_group_id As Guid, language_id As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_PAYMENT_INSTRUMENTS_LIST_FOR_VSC_QOUTE")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("language_id", language_id.ToByteArray), _
                            New DBHelper.DBHelperParameter("Company_Group_ID", company_group_id.ToByteArray)}


        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME_PAYMENT_INSTRUMENTS, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
    Public Function LoadPaymentTypesList(familyDS As DataSet, company_group_id As Guid, language_id As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_PAYMENT_TYPES_LIST_FOR_VSC_QOUTE")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("language_id1", language_id.ToByteArray), _
                            New DBHelper.DBHelperParameter("language_id2", language_id.ToByteArray), _
                            New DBHelper.DBHelperParameter("Company_Group_ID", company_group_id.ToByteArray)}


        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME_PAYMENT_TYPES, parameters)
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


End Class



