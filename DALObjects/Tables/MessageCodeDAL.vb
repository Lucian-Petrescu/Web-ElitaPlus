'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (10/29/2008)********************


Public Class MessageCodeDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_MESSAGE_CODE"
    Public Const TABLE_KEY_NAME As String = "msg_code_id"

    Public Const COL_NAME_MSG_CODE_ID As String = "msg_code_id"
    Public Const COL_NAME_MSG_TYPE As String = "msg_type"
    Public Const COL_NAME_MSG_CODE As String = "msg_code"
    Public Const COL_NAME_LABEL_ID As String = "label_id"
    Public Const COL_NAME_MSG_PARAMETER_COUNT As String = "msg_parameter_count"

    Public Const COL_NAME_UI_PROG_CODE As String = "UI_PROG_CODE"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("msg_code_id", id.ToByteArray)}
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

#Region "Public methods"
    Public Function LoadList(guidMsgType As Guid, strMsgCode As String, strUIProgCode As String) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = ""

        If guidMsgType <> Guid.Empty Then
            whereClauseConditions &= Environment.NewLine & "AND " & COL_NAME_MSG_TYPE & "= HEXTORAW('" & GuidToSQLString(guidMsgType) & "')"
        End If

        If FormatSearchMask(strMsgCode) Then
            whereClauseConditions &= Environment.NewLine & "AND UPPER(" & COL_NAME_MSG_CODE & ") " & strMsgCode.ToUpper
        End If

        If FormatSearchMask(strUIProgCode) Then
            whereClauseConditions &= Environment.NewLine & "AND UPPER(" & COL_NAME_UI_PROG_CODE & ") " & strUIProgCode.ToUpper
        End If

        selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)

        Try
            Return (DBHelper.Fetch(selectStmt, TABLE_NAME))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        Return DBHelper.Fetch(selectStmt, TABLE_NAME)
    End Function

    Public Function getExistingMSGCode(strMSGCode As String, strMSGType As String, strUIProgCode As String) As DataSet
        Dim selectStmt As String = Config("/SQL/GET_EXISTING_MSG_CODE")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                                                   New DBHelper.DBHelperParameter("CODE", strMSGType), _
                                                   New DBHelper.DBHelperParameter(COL_NAME_MSG_CODE, strMSGCode), _
                                                   New DBHelper.DBHelperParameter(COL_NAME_UI_PROG_CODE, strUIProgCode)}
        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function loadMsgIdFromMsgCode(strMsgCode As String, strMsgType As String) As DataSet
        Dim selectStmt As String = Config("/SQL/GET_ID_FROM_MSGCODE")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                                                   New DBHelper.DBHelperParameter("CODE", strMsgType), _
                                                   New DBHelper.DBHelperParameter(COL_NAME_MSG_CODE, strMsgCode)}
        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region
End Class


