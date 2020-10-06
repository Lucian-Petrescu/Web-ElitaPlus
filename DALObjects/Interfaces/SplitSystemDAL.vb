'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (12/14/2005)********************


Public Class SplitSystemDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_SPLIT_SYSTEM"
    Public Const TABLE_KEY_NAME As String = "split_system_id"

    Public Const COL_NAME_SPLIT_SYSTEM_ID As String = "split_system_id"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_SYSTEM_CODE As String = "system_code"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_LAYOUT As String = "layout"
    Public Const COL_NAME_INTERFACE_CODE As String = "interface_code"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("split_system_id", id.ToByteArray)}
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
        If Not ds.Tables(TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region


End Class


