'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (4/30/2012)********************


Public Class AnswerDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_ANSWER"
    Public Const TABLE_KEY_NAME As String = "answer_id"

    Public Const COL_NAME_ANSWER_ID As String = "answer_id"
    Public Const COL_NAME_SOFT_QUESTION_ID As String = "soft_question_id"
    Public Const COL_NAME_CODE As String = "code"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_ANSWER_ORDER As String = "answer_order"
    Public Const COL_NAME_ANSWER_VALUE As String = "answer_value"
    Public Const COL_NAME_ANSWER_CODE As String = "answer_code"
    Public Const COL_NAME_SUPPORTS_CLAIM_ID As String = "supports_claim_id"
    Public Const COL_NAME_SCORE As String = "score"
    Public Const COL_NAME_EFFECTIVE As String = "effective"
    Public Const COL_NAME_EXPIRATION As String = "expiration"
    Public Const COL_NAME_LIST_ITEM_ID As String = "list_item_id"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("answer_id", id.ToByteArray)}
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

    Public Sub LoadList(familyDS As DataSet, SoftQuestionID As Guid, CurrentDate As DateTime)
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim parameters() As OracleParameter = New OracleParameter() _
                                             {New OracleParameter(COL_NAME_SOFT_QUESTION_ID, OracleDbType.Raw, 16), _
                                             New OracleParameter("current_date", OracleDbType.Date)}
        Try
            parameters(0).Value = SoftQuestionID.ToByteArray
            parameters(1).Value = CurrentDate
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
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

#Region "Public methods"
    Public Function GetAnswerList(SoftQuestionID As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_ANSWER_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_SOFT_QUESTION_ID, SoftQuestionID.ToByteArray)}
        Dim ds As New DataSet
        Try
            Return DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetNewAnswerCode() As String
        Dim selectStmt As String = Config("/SQL/GET_NEW_ANSWER_CODE")
        Try
            Dim DV As DataView = DBHelper.Fetch(selectStmt, TABLE_NAME).Tables(0).DefaultView
            Dim strCode As String = DV(0)(0).ToString
            '6 digits in answer code Prefixed by "A"
            strCode = "A" & strCode.PadLeft(6, "0")
            Return strCode
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetAnswerCodeByValue(AnswerValue As String) As String
        Dim selectStmt As String = Config("/SQL/GET_ANSWER_CODE_BY_VALUE")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                        {New DBHelper.DBHelperParameter(COL_NAME_ANSWER_VALUE.ToUpper, AnswerValue)}
        Dim ds As New DataSet
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            If Not ds.Tables(0).Rows.Count = 0 Then
                Return ds.Tables(0).Rows(0)(0).ToString
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
    Public Function GetAnswerDataByCode(AnswerCode As String) As DataSet
        Dim selectStmt As String = Config("/SQL/GET_ANSWER_DATA_BY_CODE")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                        {New DBHelper.DBHelperParameter(COL_NAME_ANSWER_CODE.ToUpper, AnswerCode)}
        Dim ds As New DataSet
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return  ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

#End Region



End Class


