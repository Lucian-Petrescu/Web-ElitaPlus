Public Class CertEndorseCovDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_CERT_ENDORSE_COV"
    Public Const TABLE_KEY_NAME As String = "cert_endorse_cov_id"

    Public Const COL_NAME_CERT_ENDORSE_COV_ID As String = "cert_endorse_cov_id"
    Public Const COL_NAME_CERT_ENDORSE_ID As String = "cert_endorse_id"
    Public Const COL_NAME_COVERAGE_TYPE_ID As String = "coverage_type_id"
    Public Const COL_NAME_BEGIN_DATE_PRE As String = "begin_date_pre"
    Public Const COL_NAME_BEGIN_DATE_POST As String = "begin_date_post"
    Public Const COL_NAME_END_DATE_PRE As String = "end_date_pre"
    Public Const COL_NAME_END_DATE_POST As String = "end_date_post"
    Public Const COL_NAME_LANGUAGE_ID As String = "language_id"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("cert_endorse_cov_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub LoadList(ByVal familyDs As DataSet, ByVal certEndorseId As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("cert_endorse_id", certEndorseId)}
        Try
            DBHelper.Fetch(familyDs, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(ByVal certEndorsementId As Guid, ByVal languageId As Guid) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST_FOR_ENDORSEMENT")
        parameters = New OracleParameter() _
                                    {New OracleParameter(COL_NAME_CERT_ENDORSE_ID, certEndorsementId.ToByteArray), _
                                     New OracleParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray)}

        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)

    End Function

    Public Sub IsCertEndorsable(ByVal ds As DataSet, ByVal certId As Guid, ByVal beginDatePost As Date, ByVal endDatePost As Date)
        Dim selectStmt As String = Me.Config("/SQL/IS_CERT_ENDORSABLE")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                                {New DBHelper.DBHelperParameter("cert_id", certId.ToByteArray), _
                                    New DBHelper.DBHelperParameter("begin_date_post", beginDatePost), _
                                    New DBHelper.DBHelperParameter("end_date_post", endDatePost)}
        Try
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub


#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If Not ds.Tables(Me.TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region


End Class



