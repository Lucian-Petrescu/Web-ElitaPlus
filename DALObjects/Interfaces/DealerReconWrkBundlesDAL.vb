Public Class DealerReconWrkBundlesDAL
    Inherits DALBase


#Region "Constants"
    Private Const DSNAME As String = "LIST"

    Public Const TABLE_NAME As String = "ELP_DEALER_RECON_WRK_BUNDLES"
    'Public Const TABLE_KEY_NAME As String = "dealer_recon_wrk_bundles_id"
    Public Const DEALER_RECON_WRK_TABLE_NAME As String = "ELP_DEALER_RECON_WRK"

    'Public Const COL_NAME_DEALER_RECON_WRK_BUNDLES_ID As String = "dealer_recon_wrk_bundles_id"
    Public Const COL_NAME_DEALERFILE_PROCESSED_ID As String = "dealerfile_processed_id"
    Public Const COL_DEALER_RECON_WRK_ID As String = "dealer_recon_wrk_id"
    Public Const COL_ITEM_NUMBER As String = "ITEM_NUMBER"
    Public Const COL_ITEM_MANUFACTURER As String = "ITEM_MANUFACTURER"
    Public Const COL_ITEM_MODEL As String = "ITEM_MODEL"
    Public Const COL_ITEM_SERIAL_NUMBER As String = "ITEM_SERIAL_NUMBER"
    Public Const COL_ITEM_DESCRIPTION As String = "ITEM_DESCRIPTION"
    Public Const COL_ITEM_PRICE As String = "ITEM_PRICE"
    Public Const COL_ITEM_BUNDLE_VAL As String = "ITEM_BUNDLE_VAL"
    Public Const COL_ITEM_MAN_WARRANTY As String = "ITEM_MAN_WARRANTY"

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
    Public Sub LoadSchema(ByVal ds As DataSet, ByVal dealerFileProcessedID As Guid)
        Load(ds, dealerFileProcessedID)
    End Sub

    Public Sub Load(ByVal familyDS As DataSet, ByVal dealerFileProcessedID As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_DEALERFILE_PROCESSED_ID, dealerFileProcessedID.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(ByVal dealerReconWrkId As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_DEALER_RECON_WRK_ID, dealerReconWrkId.ToByteArray)}
        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
    End Sub
#End Region

#Region "Bundles Save Method"
    Public Function SaveBundles(ByVal ds As DataSet) As Integer
        Dim selectStmt As String = Me.Config("/SQL/UPDATE_BUNDLES")
        Dim inParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("BUNDLES", ds.GetXml, GetType(Xml.XmlDocument))}
        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(Me.P_RETURN, GetType(Integer))}

        Try

            DBHelper.ExecuteSp(selectStmt, inParameters, outParameters)
            Return outParameters(0).Value

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Function
#End Region


End Class





