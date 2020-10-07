Imports Assurant.Common.Types
Public Class ValidateDuplicateCodeDAL
    Inherits DALBase
    Public Function ValidateDuplicateCode(objTypeName As String, effectivedate As DateTimeType, Code As String, iD As Guid) As DataView
        Dim selectStmt As String = Config("/SQL/" & objTypeName)
        Dim whereClauseConditions As String = ""

        Dim parameters() As OracleParameter = _
            New OracleParameter() {New OracleParameter("cODE", OracleDbType.Varchar2), _
                                   New OracleParameter("Effective", OracleDbType.Date), _
                                   New OracleParameter("entity_Id", OracleDbType.Raw, 16)}

        Dim ds As New DataSet

        parameters(0).Value = Code.Trim                     'populate the code
        parameters(1).Value = effectivedate.Value.Date      'populate the effective date
        parameters(2).Value = iD.ToByteArray                'populate the id of the entity

        Try
            Return DBHelper.Fetch(ds, selectStmt, objTypeName, parameters).Tables(0).DefaultView
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)

        End Try

    End Function


End Class
