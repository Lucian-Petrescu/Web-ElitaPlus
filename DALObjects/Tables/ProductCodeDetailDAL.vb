Imports Assurant.ElitaPlus.DALObjects
Public Class ProductCodeDetailDAL
    Inherits DALBase

#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_PRODUCT_CODE_DETAIL"
    Public Const TABLE_KEY_NAME As String = "product_code_detail_id"
    Public Const COL_NAME_PRODUCT_CODE_DETAIL_ID As String = "product_code_detail_id"
    Public Const COL_NAME_PRODUCT_CODE_PARENT_ID As String = "product_code_parent_id"
    Public Const COL_NAME_PRODUCT_CODE_ID As String = "product_code_id"
    Public Const COL_NAME_EFFECTIVE As String = "effective"
    Public Const COL_NAME_EXPIRATION As String = "expiration"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_COMPANY_ID As String = "Company_id"
    Public Const COL_NAME_DEALER_CODE As String = "dealer"
    Public Const COL_NAME_COMPANY_CODE As String = "code"


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



    Public Function LoadList(DealerId As Guid, ProductId As Guid) As DataSet

        Dim selectstmt As String = Config("/SQL/LOAD_LIST")
        Dim parameters() As DBHelper.DBHelperParameter
        Dim whereClauseConditions As String = ""
        Dim ds As New DataSet
        parameters = New DBHelper.DBHelperParameter() _
                                    {New DBHelper.DBHelperParameter(COL_NAME_PRODUCT_CODE_ID, ProductId.ToByteArray),
                                    New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID, DealerId.ToByteArray)}

        Try
            DBHelper.Fetch(ds, selectstmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try


    End Function
#End Region
    Public Function LoadList(ParentProductCodeId As Guid, familyDS As DataSet)

        Dim selectStmt As String = Config("/SQL/ChildListByParentID")
        ' Dim ds As DataSet
        Dim dcPk As DataColumnCollection

        Dim parameters() As OracleParameter
        parameters = New OracleParameter() {New OracleParameter(COL_NAME_PRODUCT_CODE_ID, ParentProductCodeId.ToByteArray)}

        Try

            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
    Public Function ChildListByParentID(ParentProductCodeId As Guid) As DataSet

        Dim selectstmt As String = Config("/SQL/ChildListByParentID")
        Dim parameters() As DBHelper.DBHelperParameter
        Dim whereClauseConditions As String = ""
        Dim ds As New DataSet
        parameters = New DBHelper.DBHelperParameter() _
                                    {New DBHelper.DBHelperParameter(COL_NAME_PRODUCT_CODE_PARENT_ID, ParentProductCodeId.ToByteArray)}

        Try
            DBHelper.Fetch(ds, selectstmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try


    End Function



End Class
