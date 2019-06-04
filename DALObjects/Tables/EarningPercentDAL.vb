'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (12/11/2006)********************


Public Class EarningPercentDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_EARNING_PERCENT"
    Public Const TABLE_KEY_NAME As String = "earning_percent_id"

    Public Const COL_NAME_EARNING_PERCENT_ID As String = "earning_percent_id"
    Public Const COL_NAME_EARNING_PATTERN_ID As String = "earning_pattern_id"
    Public Const COL_NAME_EARNING_TERM As String = "earning_term"
    Public Const COL_NAME_EARNING_PERCENT As String = "earning_percent"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("earning_percent_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(ByVal EarningPatternId) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim ds As New DataSet
        Dim covergeParam As New DBHelper.DBHelperParameter(Me.COL_NAME_EARNING_PATTERN_ID, EarningPatternId.ToByteArray)
        DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {covergeParam})
        Return ds
    End Function

    Public Function TermCount(ByVal EarningTerm As Integer, ByVal EarningPatternId As Guid, ByVal EarningPercentId As Guid) As Integer

        Dim selectStmt As String = Me.Config("/SQL/TERM_COUNT")
        Dim parameters() As DBHelper.DBHelperParameter
        Dim ds As New DataSet
        Dim termCnt As Integer
        parameters = New DBHelper.DBHelperParameter() _
                    {New DBHelper.DBHelperParameter(Me.COL_NAME_EARNING_PATTERN_ID, EarningPatternId.ToByteArray), _
                     New DBHelper.DBHelperParameter(Me.COL_NAME_EARNING_TERM, EarningTerm), _
                     New DBHelper.DBHelperParameter(Me.COL_NAME_EARNING_PERCENT_ID, EarningPercentId.ToByteArray)}
        DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
        termCnt = ds.Tables(0).Rows(0).Item(0)
        Return termCnt
    End Function

    Public Function TermPercent(ByVal EarningTerm As Integer, ByVal EarningPatternId As Guid) As Double

        Dim selectStmt As String = Me.Config("/SQL/TERM_PERCENT")
        Dim parameters() As DBHelper.DBHelperParameter
        Dim ds As New DataSet
        Dim termPct As Double
        parameters = New DBHelper.DBHelperParameter() _
                    {New DBHelper.DBHelperParameter(Me.COL_NAME_EARNING_PATTERN_ID, EarningPatternId.ToByteArray), _
                     New DBHelper.DBHelperParameter(Me.COL_NAME_EARNING_TERM, EarningTerm)}
        DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)

        If ds.Tables(0).Rows.Count > 0 Then
            termPct = ds.Tables(0).Rows(0).Item(0)
        Else
            termPct = -1
        End If
        Return termPct
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


