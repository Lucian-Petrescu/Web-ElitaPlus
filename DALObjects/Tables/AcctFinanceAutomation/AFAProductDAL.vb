'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (1/15/2015)********************


Public Class AFAProductDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_AFA_PRODUCT"
    Public Const TABLE_KEY_NAME As String = "afa_product_id"

    Public Const COL_NAME_AFA_PRODUCT_ID As String = "afa_product_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_CODE As String = "code"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_PRODUCT_TYPE As String = "product_type"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("afa_product_id", id.ToByteArray)}
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

    Public Function LoadList(dealerId As Guid, productCode As String) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = ""

        If FormatSearchMask(productCode) Then
            whereClauseConditions &= Environment.NewLine & " and UPPER(p.code) " & productCode.ToUpper.Trim
        End If

        selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("dealer_id", dealerId.ToByteArray)}

        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
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


    Public Function IsProdCodeUnique(dealerId As Guid, Code As String, prodCodeId As Guid) As Boolean
        Dim selectStmt As String = Config("/SQL/GET_DUPLICATE_PRODUCT_COUNT")
        Dim parameters() As DBHelper.DBHelperParameter = _
            New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID, dealerId.ToByteArray), _
                                              New DBHelper.DBHelperParameter(COL_NAME_CODE, Code.ToUpper), _
                                              New DBHelper.DBHelperParameter(COL_NAME_AFA_PRODUCT_ID, prodCodeId.ToByteArray)}

        Dim count As Integer
        Try
            count = DBHelper.ExecuteScalar(selectStmt, parameters)
            If count > 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function


End Class