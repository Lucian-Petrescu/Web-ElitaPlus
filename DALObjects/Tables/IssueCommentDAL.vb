﻿'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (5/30/2012)********************


Public Class IssueCommentDAL
    Inherits DALBase

#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_ISSUE_COMMENT"
    Public Const TABLE_KEY_NAME As String = "issue_comment_id"

    Public Const COL_NAME_ISSUE_COMMENT_ID As String = "issue_comment_id"
    Public Const COL_NAME_ISSUE_ID As String = "issue_id"
    Public Const COL_NAME_ISSUE_COMMENT_TYPE_ID As String = "issue_comment_type_id"
    Public Const COL_NAME_CODE As String = "code"
    Public Const COL_NAME_TEXT As String = "text"
    Public Const COL_NAME_DISPLAY_ON_WEB As String = "display_on_web"

#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.new()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ByVal ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(ByVal familyDS As DataSet, ByVal id As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("issue_comment_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(ByVal familyDS As DataSet, ByVal id As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("issue_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadList(ByVal IssueId As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_COMMENT_LIST")
        Dim whereClauseConditions As String = String.Empty
        Dim ds As New DataSet
        Dim IssueIdParam As DBHelper.DBHelperParameter

        IssueIdParam = New DBHelper.DBHelperParameter(Me.COL_NAME_ISSUE_ID, IssueId.ToByteArray)

        Try
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {IssueIdParam})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(Me.TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region

#Region "Public Methods"
    Public Function IsChild(ByVal IssueCommentId As Guid, ByVal IssueId As Guid, ByVal companyIds As ArrayList, ByVal languageId As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/IS_CHILD")
        Dim whereClauseConditions As String = String.Empty
        Dim ds As New DataSet
        Dim IssueCommentIdParam As DBHelper.DBHelperParameter
        Dim IssueIdParam As DBHelper.DBHelperParameter

        Try
            Dim params() As DBHelper.DBHelperParameter = {New DBHelper.DBHelperParameter(Me.COL_NAME_ISSUE_COMMENT_ID, IssueCommentId.ToByteArray), _
                                                          New DBHelper.DBHelperParameter(Me.COL_NAME_ISSUE_ID, IssueId.ToByteArray)}
            Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, params)

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetDropdownCodeToUpdate(ByVal IssueCommentID As Guid) As DataSet
        Dim ds As New DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_ISSUE_COMMENT_CODE")

        Dim params() As DBHelper.DBHelperParameter = {New DBHelper.DBHelperParameter(COL_NAME_ISSUE_COMMENT_ID, IssueCommentID.ToByteArray)}

        Try
            Return DBHelper.Fetch(ds, selectStmt, IssueDAL.TABLE_NAME, params)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

#End Region

End Class


