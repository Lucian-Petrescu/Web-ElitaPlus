
Imports System.Xml

Public Class ClaimSuspenseDAL
    Inherits DALBase

#Region "Constants"
    Private Const DSNAME As String = "LIST"

    Public Const TABLE_NAME As String = "ELP_CLAIM_RECON_WRK"
    Public Const TABLE_KEY_NAME As String = "claim_recon_wrk_id"

    Public Const COL_NAME_CLAIM_RECON_WRK_ID As String = "claim_recon_wrk_id"
    Public Const COL_NAME_CLAIMFILE_PROCESSED_ID As String = "claimfile_processed_id"
    Public Const COL_NAME_FILENAME As String = "filename"
    Public Const COL_NAME_INTERFACE_CODE As String = "interface_code"
    Public Const COL_NAME_CLAIM_ACTION As String = "claim_action"
    Public Const COL_NAME_PROCESS_ORDER As String = "process_order"
    Public Const COL_NAME_REJECT_REASON As String = "reject_reason"
    Public Const COL_NAME_CLAIM_LOADED As String = "claim_loaded"
    Public Const COL_NAME_DEALER_CODE As String = "dealer_code"
    Public Const COL_NAME_CERTIFICATE As String = "certificate"
    Public Const COL_NAME_CERTIFICATE_SALES_DATE As String = "certificate_sales_date"
    Public Const COL_NAME_AUTHORIZATION_NUMBER As String = "authorization_number"
    Public Const COL_NAME_AUTHORIZATION_CREATION_DATE As String = "authorization_creation_date"
    Public Const COL_NAME_AUTHORIZATION_CODE As String = "authorization_code"
    Public Const COL_NAME_PRODUCT_CODE As String = "product_code"
    Public Const COL_NAME_ADDITIONAL_PRODUCT_CODE As String = "additional_product_code"
    Public Const COL_NAME_MANUFACTURER As String = "manufacturer"
    Public Const COL_NAME_MODEL As String = "model"
    Public Const COL_NAME_SERIAL_NUMBER As String = "serial_number"
    Public Const COL_NAME_SERVICE_CENTER_CODE As String = "service_center_code"
    Public Const COL_NAME_AMOUNT As String = "amount"
    Public Const COL_NAME_DO_NOT_PROCESS As String = "do_not_process"
    Public Const COL_NAME_DATE_CLAIM_CLOSED As String = "date_claim_closed"
    Public Const COL_NAME_STATUS_CODE As String = "status_code"
    Public Const COL_NAME_CLAIM_NUMBER As String = "claim_number"
    Public Const COL_NAME_REPLACEMENT_DATE As String = "replacement_date"
    Public Const COL_NAME_PROBLEM_DESCRIPTION As String = "problem_description"

    Public Shadows Const MAX_NUMBER_OF_ROWS As Int32 = 501

    'Parameter
    Private Const P_RETURN As String = "V_RETURN"
    Private Const P_XMLSET As String = "V_XMLSET"
    Private Const P_CERTIFICATE As String = "V_CERTIFICATE_NUMBER"
    Private Const P_AUTHORIZATION As String = "V_AUTHORIZATION_NUMBER"
    Private Const P_FILENAME As String = "V_FILENAME"
    Private Const P_MAX_ROWS As String = "V_MAX_ROWS"
    Private Const P_USER_ID As String = "V_USER_ID"
    Private Const P_USER_NAME As String = "V_USER_NAME"
    Private Const P_CLAIM_RECON_LIST As String = "V_CLAIM_RECON_LIST"


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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("claim_recon_wrk_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(certificateNumber As String, authorizationNumber As String, fileName As String, userId As Guid, Optional ByVal MaxRows As Integer = MAX_NUMBER_OF_ROWS) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim inParameters As New ArrayList
        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(P_CLAIM_RECON_LIST, GetType(DataSet))}
        Dim whereClauseConditions As String
        Dim _param As DBHelper.DBHelperParameter
        Dim ds As New DataSet("CLAIM_SUSPENSE")

        Try

            _param = New DBHelper.DBHelperParameter(P_USER_ID, userId.ToByteArray)
            inParameters.Add(_param)

            _param = New DBHelper.DBHelperParameter(P_CERTIFICATE, certificateNumber.ToUpper.Replace("*", "%"))
            inParameters.Add(_param)

            _param = New DBHelper.DBHelperParameter(P_AUTHORIZATION, authorizationNumber.ToUpper.Replace("*", "%"))
            inParameters.Add(_param)

            _param = New DBHelper.DBHelperParameter(P_FILENAME, fileName.ToUpper.Replace("*", "%"))
            inParameters.Add(_param)

            _param = New DBHelper.DBHelperParameter(P_MAX_ROWS, MaxRows)
            inParameters.Add(_param)

            DBHelper.FetchSp(selectStmt, inParameters.ToArray(GetType(DBHelper.DBHelperParameter)), outParameters, ds, TABLE_NAME)
            Return ds

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

#End Region

#Region "Public Methods"

    Public Function Process(ds As DataSet, UserName As String) As Integer

        Dim selectStmt As String = Config("/SQL/PROCESS")

        'Ticket # 1054112 - Remove the problem descriptio ncolumn from the dataset prior to passing back to oracle.
        If Not ds.Tables(0).Columns(COL_NAME_PROBLEM_DESCRIPTION) Is Nothing Then
            ds.Tables(0).Columns.Remove(COL_NAME_PROBLEM_DESCRIPTION)
        End If

        Dim inParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(P_XMLSET, ds.GetXml, GetType(XmlDocument)), New DBHelper.DBHelperParameter(P_USER_NAME, UserName)}
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
