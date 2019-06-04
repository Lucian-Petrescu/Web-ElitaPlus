Public Class FormAuthorization


#Region "CONSTANTS"

    'Const FORM_AUTH_INFO As String = "Form_Auth_Info"
    'Const COMPANY_FORM_EXCLUSIONS As String = "CompanyFormExclusions"

#End Region


#Region "CONSTRUCTOR"

    Public Sub New()
       
    End Sub

#End Region


#Region "ENUMERATIONS"

    Public Enum enumPermissionType
        VIEWONLY = 1
        EDIT = 2
        NONE = 3
    End Enum

#End Region


#Region "DATA ACCESS ROUTINES"

    Public Shared Function Load(ByVal sCODE As String) As DataView
        Dim oDs As DataSet
        
        Try
            Dim oFormAuthorizationData As New FormAuthorizationData
            Dim dal As New FormAuthorizationDAL

            With oFormAuthorizationData
                .code = sCODE
                .network_id = ElitaPlusIdentity.Current.ActiveUser.NetworkId
                .language_id = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                .company_id = ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID
                .companies_count = ElitaPlusIdentity.Current.ActiveUser.Companies.Count
            End With
            If ElitaPlusIdentity.Current.ActiveUser.Companies.Count > 1 Then
                ' Multiple Companies
                oDs = dal.LoadMultipleCompanies(ElitaPlusIdentity.Current.ActiveUser.Companies, oFormAuthorizationData)
            Else
                ' Single Company
                oDs = dal.Load(oFormAuthorizationData)
            End If
            Return oDs.Tables(FormAuthorizationDAL.TABLE_NAME).DefaultView
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function LoadNavigation(ByVal sCODE As String) As DataView
       Dim oDs As DataSet

        Try
            Dim oFormAuthorizationData As New FormAuthorizationData
            Dim dal As New FormAuthorizationDAL

            With oFormAuthorizationData
                .code = sCODE
            End With
            
            oDs = dal.LoadNavigation(oFormAuthorizationData)

            Return oDs.Tables(FormAuthorizationDAL.TABLE_NAME).DefaultView
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function


    Public Shared Function AllowNavigation(ByVal sFormCode As String) As Boolean
        Dim oView As DataView
        Dim bAllow As Boolean = True
        Dim sNav As String

        oView = LoadNavigation(sFormCode)

        'there are no rows, so the user is DENIED Navigation.
        If oView.Table.Rows.Count = 1 Then
            sNav = (oView.Table.Rows(0)("NAV_ALWAYS_ALLOWED")).ToString
            If sNav = "N" Then bAllow = False
        End If

        Return bAllow

    End Function

    Public Shared Function GetPermissions(ByVal sFormName As String) As enumPermissionType
        Dim oView As DataView

        Dim oDS As DataSet = FormPermissions(sFormName)

        'there are no rows, so the user is DENIED PERMISSION.
        If oDS.Tables(0).Rows.Count = 0 Then
            Return enumPermissionType.NONE
        Else

            'they have permission, what type??
            oView = New DataView(oDS.Tables(0))
            oView.RowFilter = "PERMISSION_TYPE = 'E'"

            If oView.Count >= 1 Then
                Return enumPermissionType.EDIT
            Else
                Return enumPermissionType.VIEWONLY
            End If

        End If

    End Function

    Private Shared Function FormPermissions(ByVal sCODE As String) As DataSet
        Dim oDs As DataSet

        Try
            Dim oFormAuthorizationData As New FormAuthorizationData
            Dim dal As New FormAuthorizationDAL

            With oFormAuthorizationData
                .code = sCODE
                .network_id = ElitaPlusIdentity.Current.ActiveUser.NetworkId
                .company_id = ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID
                .companies_count = ElitaPlusIdentity.Current.ActiveUser.Companies.Count
            End With
            If ElitaPlusIdentity.Current.ActiveUser.Companies.Count > 1 Then
                ' Multiple Companies
                oDs = dal.FormPermissionsMultipleCompanies(ElitaPlusIdentity.Current.ActiveUser.Companies, oFormAuthorizationData)
            Else
                ' Single Company
                oDs = dal.FormPermissions(oFormAuthorizationData)
            End If
            Return oDs
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function


#End Region

#Region "Control Authorization"

    Public Shared Function GetControlAuthorization(ByVal sCODE As String, ByVal sNetworkID As String) As DataSet
        Dim oDs As DataSet

        Try
            Dim oFormAuthorizationData As New FormAuthorizationData
            Dim dal As New FormAuthorizationDAL

            With oFormAuthorizationData
                .code = sCODE
                .network_id = sNetworkID
            End With
            oDs = dal.GetControlAuthorization(oFormAuthorizationData)
            Return oDs
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

#End Region

#Region "Tab Authorization"
    '       After Oracle 10g
    'Public Shared Function GetTabAuthorization() As DataSet
    '    Dim oDs As DataSet

    '    Try
    '        Dim oFormAuthorizationData As New FormAuthorizationData
    '        Dim dal As New FormAuthorizationDAL

    '        With oFormAuthorizationData
    '            .network_id = ElitaPlusIdentity.Current.ActiveUser.NetworkId
    '            .language_id = ElitaPlusIdentity.Current.ActiveUser.LanguageId
    '        End With
    '        oDs = dal.GetTabAuthorization(oFormAuthorizationData)
    '        Return oDs
    '    Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
    '        Throw New DataBaseAccessException(ex.ErrorType, ex)
    '    End Try

    'End Function

    ' Before Oracle 10g
    Public Shared Function GetTabAuthorization() As DataSet
        Dim oDs As DataSet
        Dim oCompany As Company
        Dim oCompanyId As Guid

        Try
            Dim oFormAuthorizationData As New FormAuthorizationData
            Dim dal As New FormAuthorizationDAL

            With oFormAuthorizationData
                .network_id = ElitaPlusIdentity.Current.ActiveUser.NetworkId
                .language_id = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            End With
            oDs = dal.GetTabAuthorization(oFormAuthorizationData)

            Return oDs
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetMenus() As DataSet
        Dim oDs As DataSet
        Dim oCompany As Company
        Dim oCompanyId As Guid

        Try
            Dim oFormAuthorizationData As New FormAuthorizationData
            Dim dal As New FormAuthorizationDAL

            With oFormAuthorizationData
                .network_id = ElitaPlusIdentity.Current.ActiveUser.NetworkId
                .language_id = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                .company_id = ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID
                .companies_count = ElitaPlusIdentity.Current.ActiveUser.Companies.Count
            End With
            oDs = dal.GetTabAuthorization(oFormAuthorizationData)

            If ElitaPlusIdentity.Current.ActiveUser.Companies.Count > 1 Then
                ' Multiple Companies
                oDs.Tables.Add(dal.LoadSubMenuMultipleCompanies(ElitaPlusIdentity.Current.ActiveUser.Companies, oFormAuthorizationData).Tables(0).Copy)
            Else
                ' Single Company
                oDs.Tables.Add(dal.LoadSubMenu(oFormAuthorizationData).Tables(0).Copy)
            End If

            oDs.Relations.Add(New DataRelation("relTABS", oDs.Tables(dal.TABLE_NAME_TAB).Columns(dal.COL_NAME_CODE), oDs.Tables(dal.TABLE_NAME).Columns(dal.COL_NAME_PARENT_TAB_CODE)))
            oDs.Relations("relTABS").Nested = True

            oDs.Relations.Add((New DataRelation("relCategories", oDs.Tables(dal.TABLE_NAME).Columns(dal.COL_NAME_CODE), oDs.Tables(dal.TABLE_NAME).Columns(dal.COL_NAME_CATEGORY))))
            oDs.Relations("relCategories").Nested = True

            oDs.DataSetName = "SiteMap"

            Return oDs
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

#End Region



End Class
