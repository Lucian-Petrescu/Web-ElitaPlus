Imports System.Text
#Region "FormAuthorizationData"

Public Class FormAuthorizationData

    Public code, network_id As String
    Public language_id, company_id As Guid
    Public companies_count As Integer


End Class

#End Region

Public Class FormAuthorizationDAL
    Inherits DALBase

#Region "Constants"
    Public Const TABLE_NAME As String = "FORM_AUTHORIZATION"
    Public Const TABLE_NAME_TAB As String = "TAB_AUTHORIZATION"

    Public Const COL_NAME_CODE As String = "CODE"
    Public Const COL_NAME_TAB_CODE As String = "TAB_CODE"
    Public Const COL_NAME_PARENT_TAB_CODE As String = "PARENT_TAB"
    Public Const COL_NAME_CATEGORY As String = "CATEGORY"
    Public Const COL_NAME_NETWORK_ID As String = "NETWORK_ID"
    Public Const COL_NAME_LANGUAGE_ID As String = "LANGUAGE_ID"
    Public Const COL_NAME_COMPANY_ID As String = "COMPANY_ID"
    Public Const COL_NAME_NUM_COMPANIES As String = "NUM_COMPANIES"

#End Region

#Region "DATA ACCESS ROUTINES"

    '-------------------------------------
    'Name:Load
    'Purpose:Retrieve the sql for the authorized forms based on a tab_id.
    'SQL Statement name:GET_FORM_AUTH
    ' Load the parameters with the session information., return the links for a tab.
    'Input Values:
    'Uses:
    '-------------------------------------
    Public Function Load(oFormAuthorizationData As FormAuthorizationData) As DataSet
        Dim selectStmt As String = Config("/SQL/GET_FORM_AUTH_BY_USER")
        Dim ds As New DataSet
        With oFormAuthorizationData
            Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                        {New DBHelper.DBHelperParameter(COL_NAME_CODE, .code.ToUpper), _
                         New DBHelper.DBHelperParameter(COL_NAME_NETWORK_ID, .network_id), _
                         New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, .language_id.ToByteArray), _
                         New DBHelper.DBHelperParameter(COL_NAME_COMPANY_ID, .company_id.ToByteArray) _
                        }
            Try
                DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Catch ex As Exception
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End With
        Return ds
    End Function

    Public Function LoadMultipleCompanies(compIds As ArrayList, oFormAuthorizationData As FormAuthorizationData) As DataSet
        Dim whereClauseConditions As String = ""
        Dim selectStmt As String = Config("/SQL/GET_FORM_AUTH_BY_USER_MULTIPLE_COMPANIES")
        Dim ds As New DataSet

        whereClauseConditions &= MiscUtil.BuildListForSql(COL_NAME_COMPANY_ID, compIds)
        selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        With oFormAuthorizationData
            Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                        {New DBHelper.DBHelperParameter(COL_NAME_CODE, .code.ToUpper), _
                         New DBHelper.DBHelperParameter(COL_NAME_NETWORK_ID, .network_id), _
                         New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, .language_id.ToByteArray), _
                         New DBHelper.DBHelperParameter(COL_NAME_CODE, .code.ToUpper), _
                         New DBHelper.DBHelperParameter(COL_NAME_NUM_COMPANIES, .companies_count) _
                        }
            Try
                DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Catch ex As Exception
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End With
        Return ds
    End Function


    Public Function LoadSubMenu(oFormAuthorizationData As FormAuthorizationData) As DataSet
        Dim selectStmt As String = Config("/SQL/GET_FORM_AUTH_BY_USER_ALL_TABS")
        Dim ds As New DataSet
        With oFormAuthorizationData
            Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                        {New DBHelper.DBHelperParameter(COL_NAME_NETWORK_ID, .network_id), _
                         New DBHelper.DBHelperParameter(COL_NAME_NETWORK_ID, .network_id), _
                         New DBHelper.DBHelperParameter(COL_NAME_NETWORK_ID, .network_id), _
                         New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, .language_id.ToByteArray), _
                         New DBHelper.DBHelperParameter(COL_NAME_COMPANY_ID, .company_id.ToByteArray), _
                         New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, .language_id.ToByteArray), _
                          New DBHelper.DBHelperParameter(COL_NAME_NETWORK_ID, .network_id), _
                         New DBHelper.DBHelperParameter(COL_NAME_NETWORK_ID, .network_id) _
                       }
            Try
                DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Catch ex As Exception
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End With
        Return ds
    End Function

    Public Function LoadSubMenuMultipleCompanies(compIds As ArrayList, oFormAuthorizationData As FormAuthorizationData) As DataSet
        Dim whereClauseConditions As String = ""
        Dim selectStmt As String = Config("/SQL/GET_FORM_AUTH_BY_USER_MULTIPLE_COMPANIES_ALL_TABS")
        Dim ds As New DataSet

        whereClauseConditions &= MiscUtil.BuildListForSql(COL_NAME_COMPANY_ID, compIds, True)
        selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        With oFormAuthorizationData
            Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                        {New DBHelper.DBHelperParameter(COL_NAME_NETWORK_ID, .network_id), _
                         New DBHelper.DBHelperParameter(COL_NAME_NETWORK_ID, .network_id), _
                         New DBHelper.DBHelperParameter(COL_NAME_NETWORK_ID, .network_id), _
                         New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, .language_id.ToByteArray), _
                         New DBHelper.DBHelperParameter(COL_NAME_NUM_COMPANIES, .companies_count), _
                         New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, .language_id.ToByteArray), _
                         New DBHelper.DBHelperParameter(COL_NAME_NETWORK_ID, .network_id), _
                         New DBHelper.DBHelperParameter(COL_NAME_NETWORK_ID, .network_id) _
                        }
            Try
                DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Catch ex As Exception
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End With
        Return ds
    End Function

    Public Function LoadNavigation(oFormAuthorizationData As FormAuthorizationData) As DataSet
        Dim selectStmt As String = Config("/SQL/GET_FORM_NAV")
        Dim ds As New DataSet
        With oFormAuthorizationData
            Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                        {New DBHelper.DBHelperParameter(COL_NAME_CODE, .code.ToUpper) _
                        }
            Try
                DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Catch ex As Exception
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End With
        Return ds
    End Function

    
    Public Function FormPermissions(oFormAuthorizationData As FormAuthorizationData) As DataSet
        Dim selectStmt As String = Config("/SQL/GET_FORM_AUTH_BY_SINGLE_FORM")
        Dim ds As New DataSet
        With oFormAuthorizationData
            Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                        {New DBHelper.DBHelperParameter(COL_NAME_NETWORK_ID, .network_id), _
                         New DBHelper.DBHelperParameter(COL_NAME_CODE, .code.ToUpper), _
                         New DBHelper.DBHelperParameter(COL_NAME_COMPANY_ID, .company_id.ToByteArray) _
                        }
            Try
                DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Catch ex As Exception
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End With
        Return ds

    End Function

    Public Function FormPermissionsMultipleCompanies(compIds As ArrayList, oFormAuthorizationData As FormAuthorizationData) As DataSet
        Dim whereClauseConditions As String = ""
        Dim selectStmt As String = Config("/SQL/GET_FORM_AUTH_BY_SINGLE_FORM_MULTIPLE_COMPANIES")
        Dim ds As New DataSet

        whereClauseConditions &= MiscUtil.BuildListForSql(COL_NAME_COMPANY_ID, compIds)
        selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        With oFormAuthorizationData
            Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                        {New DBHelper.DBHelperParameter(COL_NAME_NETWORK_ID, .network_id), _
                         New DBHelper.DBHelperParameter(COL_NAME_CODE, .code.ToUpper), _
                         New DBHelper.DBHelperParameter(COL_NAME_CODE, .code.ToUpper), _
                         New DBHelper.DBHelperParameter(COL_NAME_NUM_COMPANIES, .companies_count) _
                        }
            Try
                DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Catch ex As Exception
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End With
        Return ds
    End Function



#End Region

#Region "Control Authorization"

    Public Function GetControlAuthorization(oFormAuthorizationData As FormAuthorizationData) As DataSet
        'Dim selectStmt As String = Me.Config("/SQL/GET_CONTROL_AUTHORIZATION")
        Dim selectStmt As StringBuilder = New StringBuilder(Config("/SQL/GET_CONTROL_AUTHORIZATION"))
        Dim ds As New DataSet
        With oFormAuthorizationData
            selectStmt.Replace("#USER_NETWORK_ID", .network_id.ToUpper).Replace("#FORM_CODE", .code.ToUpper)
            'Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
            '            {New DBHelper.DBHelperParameter(COL_NAME_NETWORK_ID, .network_id.ToUpper), _
            '             New DBHelper.DBHelperParameter(COL_NAME_CODE, .code.ToUpper), _
            '             New DBHelper.DBHelperParameter(COL_NAME_NETWORK_ID, .network_id.ToUpper) _
            '            }
            Try
                ds = DBHelper.Fetch(selectStmt.ToString, TABLE_NAME)
            Catch ex As Exception
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End With

        Return ds

    End Function

#End Region

#Region "Tab Authorization"

    Public Function GetTabAuthorization(oFormAuthorizationData As FormAuthorizationData) As DataSet
        Dim selectStmt As String = Config("/SQL/GET_USER_TAB_AUTH")
        Dim ds As New DataSet
        With oFormAuthorizationData
            Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                        {New DBHelper.DBHelperParameter(COL_NAME_NETWORK_ID, .network_id), _
                         New DBHelper.DBHelperParameter(COL_NAME_NETWORK_ID, .network_id), _
                         New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, .language_id.ToByteArray) _
                        }
            Try
                DBHelper.Fetch(ds, selectStmt, TABLE_NAME_TAB, parameters)
            Catch ex As Exception
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End With
        Return ds

    End Function

#End Region

End Class
