'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (7/13/2010)********************
Imports Assurant.ElitaPlus.DALObjects.DBHelper

Public Class DealerTmkReconWrkDAL
    Inherits DALBase


#Region "Constants"
    Private Const DSNAME As String = "LIST"

    Public Const TABLE_NAME As String = "ELP_DEALER_TMK_RECON_WRK"
    Public Const TABLE_KEY_NAME As String = "dealer_tmk_recon_wrk_id"

    Public Const COL_NAME_DEALER_TMK_RECON_WRK_ID As String = "dealer_tmk_recon_wrk_id"
    Public Const COL_NAME_DEALERFILE_PROCESSED_ID As String = "dealerfile_processed_id"
    Public Const COL_NAME_RECORD_TYPE As String = "record_type"
    Public Const COL_NAME_REJECT_CODE As String = "reject_code"
    Public Const COL_NAME_REJECT_REASON As String = "reject_reason"
    Public Const COL_NAME_TMK_LOADED As String = "tmk_loaded"
    Public Const COL_NAME_CERTIFICATE As String = "certificate"
    Public Const COL_NAME_DEALERCODE As String = "dealercode"
    Public Const COL_NAME_FIRSTNAME As String = "firstname"
    Public Const COL_NAME_LASTNAME As String = "lastname"
    Public Const COL_NAME_SALESDATE As String = "salesdate"
    Public Const COL_NAME_CAMPAIGN_NUMBER As String = "campaign_number"
    Public Const COL_NAME_LAYOUT As String = "layout"
    Public Const COL_NAME_TMK_LOADED_DESC As String = "tmk_loaded_desc"

    'for reject message translation
    Public Const COL_UI_PROD_CODE As String = "UI_PROG_CODE"
    Public Const COL_REJECT_MSG_PARMS As String = "REJECT_MSG_PARMS"
    Public Const COL_MSG_PARAMETER_COUNT As String = "MSG_PARAMETER_COUNT"
    Public Const COL_REJECT_REASON As String = "reject_reason"
    Public Const COL_TRANSLATED_MSG As String = "Translated_MSG"

    Public Const PARAM_FILENAME = 0
    Public Const PARAM_LAYOUT = 1
    Public Const PARAM_INTERFACE_STATUS_ID = 2
    Public Const TOTAL_PARAM_SP = 2

#End Region

#Region "TeleMrktFileProcessedData class definition"
    Public Class TeleMrktFileProcessedData
        Public interfaceStatus_id As Guid
        Public filename As String
        Public layout As String = "gen_tm"
    End Class
#End Region

#Region "Delegate Signatures"
    Public Delegate Sub AsyncCaller(oFileProcessedData As TeleMrktFileProcessedData, selectStmt As String)
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
        Dim parameters() As DBHelperParameter = New DBHelperParameter() {New DBHelperParameter("dealer_tmk_recon_wrk_id", id.ToByteArray)}
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
    Private Function IsThereALikeClause(certNumberMask As String, campaignNumberMask As String, statusCodeMask As String) As Boolean
        Dim bIsLikeClause As Boolean

        bIsLikeClause = IsLikeClause(certNumberMask) OrElse IsLikeClause(campaignNumberMask) OrElse IsLikeClause(statusCodeMask)
        Return bIsLikeClause
    End Function

    Public Function LoadList(dealerfileProcessedID As Guid, languageID As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim parameters() As OracleParameter
        parameters = New OracleParameter() {New OracleParameter("language_id", languageID.ToByteArray), _
                                            New OracleParameter(COL_NAME_DEALERFILE_PROCESSED_ID, dealerfileProcessedID.ToByteArray)}
        Try
            Return (DBHelper.Fetch(selectStmt, DSNAME, TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadList(dealerfileProcessedID As Guid, certNumberMask As String, campaignNumberMask As String, statusCodeMask As String, languageID As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim parameters() As OracleParameter
        Dim bIsLikeClause As Boolean = False
        Dim whereClauseConditions As String = ""
        Dim ds As New DataSet

        bIsLikeClause = IsThereALikeClause(certNumberMask, campaignNumberMask, statusCodeMask)

        If ((Not (certNumberMask Is Nothing)) AndAlso (FormatSearchMask(certNumberMask))) Then
            whereClauseConditions &= Environment.NewLine & "AND UPPER(dtrw.certificate)" & certNumberMask.ToUpper & ""
        End If
        If ((Not (campaignNumberMask Is Nothing)) AndAlso (FormatSearchMask(campaignNumberMask))) Then
            whereClauseConditions &= Environment.NewLine & "AND UPPER(dtrw.campaign_number)" & campaignNumberMask.ToUpper & ""
        End If
        If ((Not (statusCodeMask Is Nothing)) AndAlso (FormatSearchMask(statusCodeMask))) Then
            whereClauseConditions &= Environment.NewLine & "AND UPPER(dtrw.tmk_loaded)" & statusCodeMask.ToUpper & ""
        End If
        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If
        parameters = New OracleParameter() {New OracleParameter("language_id", languageID.ToByteArray), _
                                            New OracleParameter(COL_NAME_DEALERFILE_PROCESSED_ID, dealerfileProcessedID.ToByteArray)}
        Try

            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return (ds)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Private Sub AsyncExecuteSP(oFileProcessedData As TeleMrktFileProcessedData, selectStmt As String)
        Dim inputParameters(TOTAL_PARAM_SP) As DBHelperParameter
        Dim outputParameter(0) As DBHelperParameter

        With oFileProcessedData
            inputParameters(PARAM_FILENAME) = New DBHelperParameter("p_filename", .filename)
            If .layout Is Nothing Then
                inputParameters(PARAM_LAYOUT) = New DBHelperParameter("p_layout", "")
            Else
                inputParameters(PARAM_LAYOUT) = New DBHelperParameter("p_layout", .layout)
            End If
            inputParameters(PARAM_INTERFACE_STATUS_ID) = New DBHelperParameter("p_interface_status_id", .interfaceStatus_id.ToByteArray)
            outputParameter(0) = New DBHelperParameter("p_return", GetType(Integer))
        End With
        ' Call DBHelper Store Procedure
        ExecuteSpParamBindByName(selectStmt, inputParameters, outputParameter)
        If outputParameter(0).Value <> 0 Then
            Dim e As New ApplicationException("Return Value = " & outputParameter(0).Value)
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, e)
        End If
    End Sub

    Private Sub ExecuteSP(oFileProcessedData As TeleMrktFileProcessedData, selectStmt As String)
        Dim aSyncHandler As New AsyncCaller(AddressOf AsyncExecuteSP)
        aSyncHandler.BeginInvoke(oFileProcessedData, selectStmt, Nothing, Nothing)
    End Sub

    Public Sub ValidateFile(oData As TeleMrktFileProcessedData)
        Dim selectStmt As String
        selectStmt = Config("/SQL/VALIDATE_TMK")
        Try
            ExecuteSP(oData, selectStmt)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub ProcessFile(oData As TeleMrktFileProcessedData)
        Dim selectStmt As String
        selectStmt = Config("/SQL/PROCESS_TMK")
        Try
            ExecuteSP(oData, selectStmt)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub DeleteFile(oData As TeleMrktFileProcessedData)
        Dim selectStmt As String
        selectStmt = Config("/SQL/DELETE_TMK")
        Try
            ExecuteSP(oData, selectStmt)
        Catch ex As Exception
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


