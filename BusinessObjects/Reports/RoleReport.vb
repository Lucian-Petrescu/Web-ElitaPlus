Public Class RoleReport

#Region "Attributes"

    Private moDataList As DataView
    Private moDataSet As DataSet
    Private msRolesIds, msTabsIds As String
    Private moLanguageId As Guid

#End Region

#Region "Properties"

    Public ReadOnly Property DataList As DataView
        Get
            If moDataList Is Nothing Then
                moDataList = PopulateRoleMenus()
            End If
            Return moDataList
        End Get

    End Property

    Public ReadOnly Property TheDataSet As DataSet
        Get
            If moDataSet Is Nothing Then
                moDataSet = GetDataset()
            End If
            Return moDataSet
        End Get

    End Property

#End Region

#Region "Constructors"

    Public Sub New()

    End Sub

    Public Sub New(oLanguageId As Guid, rolesIds As String, tabsIds As String)
        msRolesIds = rolesIds
        msTabsIds = tabsIds
        moLanguageId = oLanguageId
    End Sub

#End Region

#Region "Roles"

    Private Function PopulateRoleMenus() As DataView
        Return GetDataset().Tables(RoleReportDAL.TABLE_NAME).DefaultView

    End Function

    Public Function GetDataset() As DataSet
        Dim oDs As DataSet

        Try
            Dim dal As New RoleReportDAL

            oDs = dal.GetDataset(moLanguageId, msRolesIds, msTabsIds)
            Return oDs
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#End Region

#Region "Tabs"

    Public Shared Function PopulateAvailableList(oLanguageId As Guid, aRoleList As ArrayList) As DataView
        Dim oDs As DataSet

        Try
            Dim dal As New RoleReportDAL

            oDs = dal.PopulateAvailableList(oLanguageId, aRoleList)
            Return oDs.Tables(RoleReportDAL.TABLE_NAME).DefaultView
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#End Region

End Class
