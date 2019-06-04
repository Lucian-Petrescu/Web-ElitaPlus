'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (6/1/2012)********************

Public Class ProcessDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_PROCESS"
    Public Const TABLE_KEY_NAME As String = "process_id"

    Public Const COL_NAME_PROCESS_ID As String = "process_id"
    Public Const COL_NAME_CODE As String = "code"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_EFFECTIVE As String = "effective"
    Public Const COL_NAME_EXPIRATION As String = "expiration"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("process_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(ByVal code As String, _
                                      ByVal description As String, ByVal activeOn As String, _
                                      ByVal companyIds As ArrayList, ByVal languageId As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = String.Empty
        Dim ds As New DataSet
        Dim languageIdParam As DBHelper.DBHelperParameter
        Dim bIsLikeClause As Boolean = False
        Dim bIsWhereClause As Boolean = False

        bIsLikeClause = IsThereALikeClause(description, code)

        If ((Not (code Is Nothing)) AndAlso (Me.FormatSearchMask(code))) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(EI." & Me.COL_NAME_CODE & ")" & code.ToUpper
            bIsWhereClause = True
        End If
        If ((Not (description Is Nothing)) AndAlso (Me.FormatSearchMask(description))) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(EI." & Me.COL_NAME_DESCRIPTION & ")" & description.ToUpper
            bIsWhereClause = True
        End If
        If (Not (activeOn Is String.Empty)) Then
            whereClauseConditions &= " AND " & Environment.NewLine & " trunc(to_date('" & DateHelper.GetDateValue(activeOn).ToString("MM/dd/yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss')) BETWEEN EI." & Me.COL_NAME_EFFECTIVE & " AND EI." & Me.COL_NAME_EXPIRATION & ""
            bIsWhereClause = True
        End If

        If bIsWhereClause Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        End If

        Try
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Private Function IsThereALikeClause(ByVal description As String, ByVal code As String) As Boolean
        Dim bIsLikeClause As Boolean

        bIsLikeClause = Me.IsLikeClause(description) OrElse Me.IsLikeClause(code)

        Return bIsLikeClause
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


End Class
