Public Class RoleReportDAL
    Inherits DALBase

#Region "Constants"
    Public Const TABLE_NAME As String = "ROLE_REPORT"

    Public Const COL_NAME_LANGUAGE_ID As String = "LANGUAGE_ID"

#End Region

#Region "ROLES"

    Public Function GetDataset(oLanguageID As Guid, sRolesIds As String, sTabsIds As String) As DataSet
        Dim selectStmt As String = Config("/SQL/GET_MENUS_SELECT")
        Dim ds As New DataSet

        selectStmt &= " " & GetMiddleSql(sRolesIds, sTabsIds) & " " & Config("/SQL/GET_MENUS_ORDER")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                    {New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, oLanguageID.ToByteArray), _
                     New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, oLanguageID.ToByteArray) _
                    }
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        Return ds
    End Function

    Public Function GetMiddleSql(sRolesIds As String, sTabsIds As String) As String
        Dim selectStmt As String
        Dim ds As New DataSet

        If (sRolesIds Is Nothing) Then Return Nothing
        selectStmt = Config("/SQL/GET_ROLEID_NAME") & " " & sRolesIds
        If (sTabsIds Is Nothing) = False Then
            selectStmt &= " " & Config("/SQL/GET_TABID_NAME") & " " & sTabsIds
        End If

        Return selectStmt
    End Function


#End Region

#Region "TABS"

    Public Function PopulateAvailableList(oLanguageId As Guid, aRoleList As ArrayList) As DataSet
        Dim selectStmt As String = Config("/SQL/GET_TABS_SELECT")
        Dim ds As New DataSet

        Dim sInList, sItem As String
        Dim oRoleArray(aRoleList.Count - 1) As String
        Dim nIndex As Integer = 0

        aRoleList.CopyTo(oRoleArray)
        For Each sItem In aRoleList
            oRoleArray(nIndex) = "'" & sItem & "'"
            nIndex += 1
        Next
        sInList = "(" & String.Join(", ", oRoleArray) & ")"
        If (sInList Is Nothing) Then Return Nothing

        selectStmt &= " " & sInList & " " & Config("/SQL/GET_TABS_ORDER")

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                            {New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, oLanguageId.ToByteArray) _
                            }
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        Return ds
    End Function

#End Region

End Class
