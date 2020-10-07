'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (7/29/2008)********************
Imports System.Xml

Public Class PickupListDetailDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_PICKUP_LIST_DETAIL"
    Public Const TABLE_KEY_NAME As String = "detail_id"

    Public Const COL_NAME_DETAIL_ID As String = "detail_id"
    Public Const COL_NAME_HEADER_ID As String = "header_id"
    Public Const COL_NAME_CLAIM_STATUS_ID As String = "claim_status_id"
    Public Const COL_NAME_CLAIM_ID As String = "claim_id"
    Public Const COL_NAME_IS_EXCEPTION As String = "is_exception"
    Public Const COL_NAME_RESOLUTION_CLAIM_STATUS_ID As String = "resolution_claim_status_id"

    Public Const PARAM_USER_ID As String = "p_user_id"
    Public Const PARAM_PICKUP_EXCEPTIONS_XMLDOC As String = "p_PICKUP_EXCEPTIONS_FILE"
    Public Const PARAM_RETURN As String = "p_return"
    Public Const PARAM_EXCEPTION_MSG As String = "p_exception_msg"
    Public Const P_RETURN As Integer = 0
    Public Const P_EXCEPTION_MSG As Integer = 1

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("detail_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
   
    Public Function LoadByClaimId(id As Guid) As DataSet
        Dim ds As New DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_BY_CLAIM")

        Dim parameters = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("claim_id", id.ToByteArray)}

        Return DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)

    End Function

    Public Function LoadList() As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Return DBHelper.Fetch(selectStmt, TABLE_NAME)
    End Function


    'Calls the stored procedure UPDATE_PICKUP_EXCEPTION
    Public Sub UpdatePickupExceptions(PickupExceptions As DataSet, userId As Guid)

        Dim selectStmt As String = Config("/SQL/UPDATE_PICKUP_EXCEPTION")
        Dim outputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                                                                {New DBHelper.DBHelperParameter(PARAM_RETURN, GetType(Integer)), _
                                                                 New DBHelper.DBHelperParameter(PARAM_EXCEPTION_MSG, GetType(String), 200)}

        Dim inParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                {New DBHelper.DBHelperParameter(PARAM_USER_ID, userId.ToByteArray), _
                 New DBHelper.DBHelperParameter(PARAM_PICKUP_EXCEPTIONS_XMLDOC, PickupExceptions.GetXml, GetType(XmlDocument))}
        Dim ds As New DataSet

        Try
            DBHelper.ExecuteSp(selectStmt, inParameters, outputParameters)

            If outputParameters(P_RETURN).Value <> 0 Then _
                 Throw New StoredProcedureGeneratedException("Update Pickup Exceptions Error: ", outputParameters(P_EXCEPTION_MSG).Value)

        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Sub

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


