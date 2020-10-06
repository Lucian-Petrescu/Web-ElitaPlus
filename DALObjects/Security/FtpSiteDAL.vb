'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (6/26/2009)********************


Public Class FtpSiteDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_FTP_SITE"
    Public Const TABLE_KEY_NAME As String = "ftp_site_id"

    Public Const COL_NAME_FTP_SITE_ID As String = "ftp_site_id"
    Public Const COL_NAME_CODE As String = "code"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_HOST As String = "host"
    Public Const COL_NAME_PORT As String = "port"
    Public Const COL_NAME_USER_NAME As String = "user_name"
    Public Const COL_NAME_PASSWORD As String = "password"
    Public Const COL_NAME_ACCOUNT As String = "account"
    Public Const COL_NAME_DIRECTORY As String = "directory"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("ftp_site_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(code As String, description As String) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        '  Dim inClausecondition As String = ""
        Dim whereClauseConditions As String = ""

        code = code.Trim()
        If (Not code.Equals(String.Empty) Or Not code = "") AndAlso (FormatSearchMask(code)) Then
            whereClauseConditions &= Environment.NewLine & "Upper(ftp.code)" & code.ToUpper
        End If

        description = description.Trim()
        If (Not description.Equals(String.Empty) Or Not description = "") AndAlso (FormatSearchMask(description)) Then
            If Not whereClauseConditions = "" Then
                whereClauseConditions &= " AND "
            End If
            whereClauseConditions &= Environment.NewLine & " Upper(ftp.description)" & description.ToUpper
        End If

        'If Not inClausecondition = "" Then
        '    selectStmt = selectStmt.Replace(Me.DYNAMIC_IN_CLAUSE_PLACE_HOLDER, inClausecondition)
        'Else
        '    selectStmt = selectStmt.Replace(Me.DYNAMIC_IN_CLAUSE_PLACE_HOLDER, "")
        'End If

        If Not whereClauseConditions = "" Then
            whereClauseConditions = "WHERE " & whereClauseConditions
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Dim ds As New DataSet
        Try

            ds = DBHelper.Fetch(selectStmt, TABLE_NAME)
            Return ds
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
        If Not ds.Tables(TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region


End Class



