Public Class ClaimReconWrkPartsDAL
    Inherits DALBase


#Region "Constants"
    Private Const DSNAME As String = "LIST"

    Public Const TABLE_NAME As String = "ELP_CLAIMLOAD_RECON_WRK_PARTS"
    'Public Const TABLE_KEY_NAME As String = "dealer_recon_wrk_bundles_id"
    Public Const DEALER_RECON_WRK_TABLE_NAME As String = "ELP_CLAIMLOAD_RECON_WRK"

    'Public Const COL_NAME_DEALER_RECON_WRK_BUNDLES_ID As String = "dealer_recon_wrk_bundles_id"
    Public Const COL_NAME_CLAIMLOAD_FILE_PROCESSED_ID As String = "claimload_file_processed_id"
    Public Const COL_CLAIMLOAD_RECON_WRK_ID As String = "claimload_recon_wrk_id"
    Public Const COL_PART_SKU As String = "part_sku"
    Public Const COL_PART_DESCRIPTION As String = "PART_DESCRIPTION"


    'Parameter
    Private Const P_RETURN As String = "V_RETURN"
    Private Const P_XMLSET As String = "V_XMLSET"

#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.new()
    End Sub

#End Region

#Region "Load Methods"
    Public Sub LoadSchema(ds As DataSet, dealerFileProcessedID As Guid)
        Load(ds, dealerFileProcessedID)
    End Sub

    Public Sub Load(familyDS As DataSet, dealerFileProcessedID As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_CLAIMLOAD_FILE_PROCESSED_ID, dealerFileProcessedID.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(claimReconWrkId As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_CLAIMLOAD_RECON_WRK_ID, claimReconWrkId.ToByteArray)}
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
        MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
    End Sub
#End Region

#Region "Bundles Save Method"
    Public Function SaveParts(ds As DataSet) As Integer
        Dim selectStmt As String = Config("/SQL/UPDATE_PARTS")
        Dim inParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("PARTS", ds.GetXml, GetType(Xml.XmlDocument))}
        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(P_RETURN, GetType(Integer))}

        Try

            DBHelper.ExecuteSp(selectStmt, inParameters, outParameters)
            Return outParameters(0).Value

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Function
#End Region


End Class





