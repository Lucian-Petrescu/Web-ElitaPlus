'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (4/11/2007)********************


Public Class VSCQuoteItemDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_VSC_QUOTE_ITEM"
    Public Const TABLE_KEY_NAME As String = "quote_item_id"

    Public Const COL_NAME_QUOTE_ITEM_ID As String = "quote_item_id"
    Public Const COL_NAME_QUOTE_ID As String = "quote_id"
    Public Const COL_NAME_VSC_COVERAGE_RATE_ID As String = "vsc_coverage_rate_id"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("quote_item_id", id.ToByteArray)}
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
#End Region


End Class


