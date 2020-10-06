'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (12/29/2005)********************
Imports Assurant.ElitaPlus.DALObjects.DBHelper

Public Class SplitReconWrkDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_SPLIT_RECON_WRK"
    Public Const TABLE_KEY_NAME As String = "split_recon_wrk_id"

    Public Const COL_NAME_SPLIT_RECON_WRK_ID As String = "split_recon_wrk_id"
    Public Const COL_NAME_SPLITFILE_PROCESSED_ID As String = "splitfile_processed_id"
    Public Const COL_NAME_RECORD_TYPE As String = "record_type"
    Public Const COL_NAME_REST As String = "rest"
    Public Const COL_NAME_SPLIT_PROCESSED As String = "split_processed"
    Public Const COL_NAME_OUTFILE_NAME As String = "outfile_name"

    'Search Parameters
    Public Const SPLIT_FILE_PROCESSED_ID = 0

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("split_recon_wrk_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(SplitfileProcessedId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim parameters(0) As DBHelperParameter
        Dim sFileTypeCode As String

        If SplitfileProcessedId.Equals(Guid.Empty) Then
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr)
        Else
            parameters(SPLIT_FILE_PROCESSED_ID) = New DBHelperParameter(COL_NAME_SPLITFILE_PROCESSED_ID, SplitfileProcessedId.ToByteArray)
        End If

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
        If Not ds.Tables(TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region


End Class
