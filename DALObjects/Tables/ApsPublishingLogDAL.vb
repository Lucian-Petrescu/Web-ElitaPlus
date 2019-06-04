'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (10/9/2013)********************


Public Class ApsPublishingLogDAL
    Inherits DALBase


#Region "Constants"
    'Public Const TABLE_NAME As String = "APS_PUBLISHING_LOG"
	Public Const TABLE_KEY_NAME As String = "id"
	
	Public Const COL_NAME_ID As String = "id"
	Public Const COL_NAME_HEADER As String = "header"
	Public Const COL_NAME_TYPE As String = "type"
	Public Const COL_NAME_CODE As String = "code"
	Public Const COL_NAME_MACHINE_NAME As String = "machine_name"
	Public Const COL_NAME_APPLICATION_NAME As String = "application_name"
	Public Const COL_NAME_USER_NAME As String = "user_name"
	Public Const COL_NAME_EXTENDED_CONTENT As String = "extended_content"
	Public Const COL_NAME_GENERATION_DATE_TIME As String = "generation_date_time"
	Public Const COL_NAME_RECORDED_DATE_TIME As String = "recorded_date_time"
    Public Const COL_NAME_EXTENDED_CONTENT2 As String = "extended_content2"

    Public ReadOnly TABLE_NAME As String

#End Region

#Region "Constructors"

    Public Sub New()

    End Sub
    Public Sub New(ByVal TableName As String)
        MyBase.new()
        TABLE_NAME = TableName
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ByVal ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(ByVal familyDS As DataSet, ByVal id As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(ByVal Header As String, _
                             ByVal Code As String, _
                             ByVal MachineName As String, _
                             ByVal UserName As String, _
                             ByVal TypeName As String, _
                             ByVal generationDate As SearchCriteriaStructType(Of Date), _
                             ByVal languageid As Guid) As DataSet

        Dim selectStmt As String = String.Empty
        selectStmt = Me.Config("/SQL/LOAD_LIST_APS_ORACLE")


        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {}

        Dim whereClauseConditions As String = String.Empty

        'If ((Not (Header Is Nothing)) AndAlso (Me.FormatSearchMask(Header))) Then
        '    whereClauseConditions &= Environment.NewLine & " UPPER(" & Me.COL_NAME_HEADER & ") " & Header.ToUpper & " AND"
        'End If

        If (Not (TypeName Is Nothing)) Then
            whereClauseConditions &= Environment.NewLine & " UPPER(" & Me.COL_NAME_TYPE & ") = '" & TypeName.ToUpper & "' AND"
        End If
        If ((Not (Header Is Nothing)) AndAlso (Me.FormatSearchMask(Header))) Then
            whereClauseConditions &= Environment.NewLine & " UPPER(" & Me.COL_NAME_HEADER & ") " & Header.ToUpper & " AND"
        End If
        If ((Not (Code Is Nothing)) AndAlso (Me.FormatSearchMask(Code))) Then
            whereClauseConditions &= Environment.NewLine & " UPPER(" & Me.COL_NAME_CODE & ") " & Code.ToUpper & " AND "
        End If

        If ((Not (MachineName Is Nothing)) AndAlso (Me.FormatSearchMask(MachineName))) Then
            whereClauseConditions &= Environment.NewLine & " UPPER(" & Me.COL_NAME_MACHINE_NAME & ") " & MachineName.ToUpper & " AND "
        End If

        If ((Not (UserName Is Nothing)) AndAlso (Me.FormatSearchMask(UserName))) Then
            whereClauseConditions &= Environment.NewLine & " UPPER(" & Me.COL_NAME_USER_NAME & ") " & UserName.ToUpper & " AND "
        End If

        whereClauseConditions &= generationDate.ToSqlString(Me.TABLE_NAME, Me.COL_NAME_GENERATION_DATE_TIME, parameters).Remove(0, 4) 'remove the first and appened by ToSqlString extension method

        ''''set the table name dynamically
        selectStmt = selectStmt.Replace(Me.DYNAMIC_FROM_CLAUSE_PLACE_HOLDER, TABLE_NAME)

        If Not whereClauseConditions.Equals(String.Empty) Then
            whereClauseConditions = " WHERE " & whereClauseConditions
            'whereClauseConditions = whereClauseConditions.Remove(whereClauseConditions.Length - 4)
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If


        Dim ds As New DataSet
        Try
            Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
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


End Class


