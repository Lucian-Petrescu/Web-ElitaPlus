﻿'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (9/25/2012)********************


Public Class CompanyWorkQueueIssueDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_COMPANY_WRKQUE_ISSUE"
    Public Const TABLE_KEY_NAME As String = "company_wrkque_issue_id"

    Public Const COL_NAME_COMPANY_WRKQUE_ISSUE_ID As String = "company_wrkque_issue_id"
    Public Const COL_NAME_WORKQUEUE_ID As String = "workqueue_id"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_ISSUE_ID As String = "issue_id"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("company_wrkque_issue_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub LoadlistByIssue(familyDS As DataSet, Parentid As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD_WQ_BY_ISSUE")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("ISSUE_ID", Parentid.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function GetWorkQueueIdByIssueCompany(issueId As Guid, companyId As Guid) As Guid
        Dim selectStmt As String = Config("/SQL/LOAD_WQ_BY_ISSUE_COMPANY")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_ISSUE_ID, issueId.ToByteArray), New DBHelper.DBHelperParameter(COL_NAME_COMPANY_ID, companyId.ToByteArray)}
        Try
            Dim obj As Object
            obj = DBHelper.ExecuteScalar(selectStmt, parameters)
            If (obj IsNot Nothing) Then
                Return New Guid(CType(obj, Byte()))
            End If

            Return Guid.Empty

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
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


End Class


