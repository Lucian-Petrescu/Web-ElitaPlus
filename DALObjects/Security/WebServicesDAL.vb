'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (9/24/2009)********************
Option Strict On
Imports Assurant.ElitaPlus.DALObjects.DBHelper

Public Class WebUserLogData

    Public Web_UserLog_id As Guid
    Public Url, WebServiceFunctionName, Network_id, Environment, Hub_Region As String
    Public Input_xml As String
    Public Created_date As Date

End Class

Public Class WebServicesDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_WEBSERVICES"
    Public Const TABLE_KEY_NAME As String = "webservice_id"

    Public Const COL_NAME_WEBSERVICE_ID As String = "webservice_id"
    Public Const COL_NAME_WEB_SERVICE_NAME As String = "web_service_name"
    Public Const COL_NAME_ON_LINE_ID As String = "on_line_id"
    Public Const COL_NAME_OFF_LINE_MESSAGE As String = "off_line_message"
    Public Const COL_NAME_LAST_OPERATION_DATE As String = "last_operation_date"

    Public Const PAR_NAME_Web_UserLog_id As String = "p_Web_UserLog_id"
    Public Const PAR_NAME_WebServiceName As String = "p_WebServiceName"
    Public Const PAR_NAME_WebServiceFunctionName As String = "p_WebServiceFunctionName"
    Public Const PAR_NAME_Network_id As String = "p_Network_id"
    Public Const PAR_NAME_Environment As String = "p_Environment"
    Public Const PAR_NAME_Hub_Region As String = "p_Hub_Region"
    Public Const PAR_NAME_Input_xml As String = "p_Input_xml"
    Public Const PAR_NAME_Created_date As String = "p_Created_date"

#End Region

#Region "Signatures"

    Public Delegate Sub AsyncCaller(oWebUserLogData As WebUserLogData, selectStmt As String)

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("webservice_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(web_service_name As String, on_line_id As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")

        Dim whereClauseConditions As String

        If (web_service_name IsNot Nothing) AndAlso (Not web_service_name.Equals(String.Empty) AndAlso (FormatSearchMask(web_service_name))) Then
            whereClauseConditions &= Environment.NewLine & "WHERE UPPER(web_service_name) " & web_service_name.ToUpper
        End If


        If Not on_line_id.Equals(Guid.Empty) Then
            If (whereClauseConditions IsNot Nothing) AndAlso (Not whereClauseConditions.Equals(String.Empty)) Then
                whereClauseConditions &= Environment.NewLine & "AND on_line_id = " & MiscUtil.GetDbStringFromGuid(on_line_id, True)
            Else
                whereClauseConditions &= Environment.NewLine & "WHERE on_line_id = " & MiscUtil.GetDbStringFromGuid(on_line_id, True)
            End If
        End If

        selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)

        Try

            Return DBHelper.Fetch(selectStmt, TABLE_NAME)

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function


    Public Sub WebUserLog(Web_UserLog_id As Guid, Url As String, functionToProcess As String, _
                                      networkId As String, Environment As String, Hub As String, _
                                      _xml As String, Created_date As Date)

        Dim selectStmt As String = Config("/SQL/WebUserLog")

        Dim oWebUserLogData As New WebUserLogData

        With oWebUserLogData
            .Web_UserLog_id = Web_UserLog_id
            .Url = Url
            .WebServiceFunctionName = functionToProcess
            .Network_id = networkId
            .Environment = Environment
            .Hub_Region = Hub
            .Input_xml = _xml
            .Created_date = Created_date

        End With

        Dim aSyncHandler As New AsyncCaller(AddressOf AsyncExecuteSP)
        aSyncHandler.BeginInvoke(oWebUserLogData, selectStmt, Nothing, Nothing)


    End Sub

#End Region

    Private Sub AsyncExecuteSP(oWebUserLogData As WebUserLogData, selectStmt As String)

        Try
            Using cmd As OracleCommand = OracleDbHelper.CreateCommand(selectStmt, CommandType.StoredProcedure, OracleDbHelper.CreateConnection())
                cmd.AddParameter(PAR_NAME_Web_UserLog_id, OracleDbType.Raw, oWebUserLogData.Web_UserLog_id.ToByteArray)
                cmd.AddParameter(PAR_NAME_WebServiceName, OracleDbType.Varchar2, oWebUserLogData.Url)
                cmd.AddParameter(PAR_NAME_WebServiceFunctionName, OracleDbType.Varchar2, oWebUserLogData.WebServiceFunctionName)
                cmd.AddParameter(PAR_NAME_Network_id, OracleDbType.Varchar2, oWebUserLogData.Network_id)
                cmd.AddParameter(PAR_NAME_Environment, OracleDbType.Varchar2, oWebUserLogData.Environment)
                cmd.AddParameter(PAR_NAME_Hub_Region, OracleDbType.Varchar2, oWebUserLogData.Hub_Region)
                cmd.AddParameter(PAR_NAME_Input_xml, OracleDbType.Clob, oWebUserLogData.Input_xml)
                cmd.AddParameter(PAR_NAME_Created_date, OracleDbType.Date, oWebUserLogData.Created_date)
                cmd.ExecuteNonQuery()
            End Using
        Catch ex As Exception
            ex = Nothing
        End Try

    End Sub

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