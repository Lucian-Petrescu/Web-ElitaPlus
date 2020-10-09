''' <summary>
''' Validates for an entity if there are any overlapping records with same code and/or same parent
''' with effective date and expiration date
''' </summary>
''' <remarks></remarks>
Public Class OverlapValidationVisitorDAL
    Inherits DALBase

    Public Function LoadList(id As Guid, TypeName As String, _
                            Code As String, Effective As DateTimeType, Expiration As DateTimeType, _
                                  ParentID As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/" & TypeName)
        Dim whereClauseConditions As String = ""

        Dim parameters() As OracleParameter = _
            New OracleParameter() {New OracleParameter("id", OracleDbType.Raw, 16), _
                                   New OracleParameter("Code", OracleDbType.Varchar2), _
                                   New OracleParameter("Effective", OracleDbType.TimeStamp), _
                                   New OracleParameter("Expiration", OracleDbType.TimeStamp)}

        Dim ds As New DataSet

        parameters(0).Value = id.ToByteArray            'populate the ID
        parameters(1).Value = Code.Trim                 'populate the code
        parameters(2).Value = Effective.Value           'populate the effective date
        parameters(3).Value = Expiration.Value          'populate the expiration date

        If Not ParentID = Guid.Empty Then
            ReDim Preserve parameters(parameters.Length)
            parameters(parameters.GetUpperBound(0)) = New OracleParameter("parent_id", OracleDbType.Raw, 16)
            parameters(parameters.GetUpperBound(0)).Value = ParentID.ToByteArray
        End If

        Try
            Return DBHelper.Fetch(ds, selectStmt, TypeName, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

End Class
