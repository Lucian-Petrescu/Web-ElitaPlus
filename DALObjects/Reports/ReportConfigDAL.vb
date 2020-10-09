'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (5/27/2009)********************


Public Class ReportConfigDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_REPORT_CONFIG"
    Public Const TABLE_KEY_NAME As String = "report_config_id"

    Public Const COL_NAME_REPORT_CONFIG_ID As String = "report_config_id"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_REPORT_CE_NAME As String = "report_ce_name"
    Public Const COL_NAME_FORM_ID As String = "form_id"
    Public Const COL_NAME_LARGE_REPORT As String = "large_report"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("report_config_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(compIds As ArrayList, report As String, reportCe As String, _
                             languageId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim inClausecondition As String = ""
        Dim whereClauseConditions As String = ""

        inClausecondition &= " AND rep." & MiscUtil.BuildListForSql(DALObjects.CompanyDAL.COL_NAME_COMPANY_ID, compIds, False)

        reportCe = reportCe.Trim()
        If (Not reportCe.Equals(String.Empty) Or Not reportCe = "") AndAlso (FormatSearchMask(reportCe)) Then
            whereClauseConditions &= Environment.NewLine & "AND " & "Upper(rep.report_ce_name)" & reportCe.ToUpper
        End If

        report = report.Trim()
        If (Not report.Equals(String.Empty) Or Not report = "") AndAlso (FormatSearchMask(report)) Then
            whereClauseConditions &= Environment.NewLine & "AND " & "Upper(trans.translation)" & report.ToUpper
        End If

        If Not languageId.Equals(Guid.Empty) Then
            whereClauseConditions &= Environment.NewLine & " AND trans.language_id = " & MiscUtil.GetDbStringFromGuid(languageId)
        End If

        If Not inClausecondition = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_IN_CLAUSE_PLACE_HOLDER, inClausecondition)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_IN_CLAUSE_PLACE_HOLDER, "")
        End If

        If Not whereClauseConditions = "" Then
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


