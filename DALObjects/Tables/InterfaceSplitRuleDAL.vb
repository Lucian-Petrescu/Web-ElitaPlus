Public Class InterfaceSplitRuleDAL
    Inherits DALBase

#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_INTERFACE_SPLIT_RULES"
    Public Const TABLE_KEY_NAME As String = "interface_split_rule_id"

    Public Const COL_NAME_INTERFACE_SPLIT_RULE_ID As String = "interface_split_rule_id"
    Public Const COL_NAME_SOURCE As String = "source"
    Public Const COL_NAME_SOURCE_CODE As String = "source_code"
    Public Const COL_NAME_NEW_SOURCE_CODE As String = "new_source_code"
    Public Const COL_NAME_ACTIVE As String = "active"
    Public Const COL_NAME_FIELD_NAME As String = "field_name"
    Public Const COL_NAME_FIELD_OPERATOR As String = "field_operator"
    Public Const COL_NAME_FIELD_VALUE As String = "field_value"

#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.new()
    End Sub
#End Region

#Region "Load Methods"
    Public Sub LoadSchema(ByRef ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(ByVal familyDS As DataSet, ByVal id As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_INTERFACE_SPLIT_RULE_ID, id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList() As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
    End Function

    Public Function LoadList(ByVal source As String, ByVal sourceCode As String) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = ""
        Dim ds As New DataSet

        If ((Not (source Is Nothing)) AndAlso (Me.FormatSearchMask(source))) Then
            whereClauseConditions &= Environment.NewLine & " AND UPPER(" & Me.COL_NAME_SOURCE & ")" & source.ToUpper
        End If

        If ((Not (sourceCode Is Nothing)) AndAlso (Me.FormatSearchMask(sourceCode))) Then
            whereClauseConditions &= Environment.NewLine & " AND UPPER(" & Me.COL_NAME_SOURCE_CODE & ")" & sourceCode.ToUpper
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            Return (DBHelper.Fetch(selectStmt, Me.TABLE_NAME))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)

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
