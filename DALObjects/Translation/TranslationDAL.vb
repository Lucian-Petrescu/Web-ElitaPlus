Public Class TranslationDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "TRANSLATION"

   Public Const COL_NAME_LANGUAGE_ID As String = "LANGUAGE_ID"

#End Region

#Region "DATA ACCESS ROUTINES"

    Public Function GetTranslations(ByVal InClause As String, ByVal oLanguageID As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_TRANSLATION_BASIC_CONTROLS")
        Dim ds As New DataSet

        selectStmt = selectStmt.Replace("ZZZ_UI_PROG_CODES", InClause)
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                    {New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, oLanguageID.ToByteArray) _
                    }
        Try
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        Return ds
    End Function

    Public Function Get_EnglishLanguageID() As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_ENLISH_LANGUAGES_ID")
        Dim ds As New DataSet

        Try
            ds = DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        Return ds
    End Function

#End Region


End Class
