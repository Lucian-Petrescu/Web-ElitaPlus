Public Class OcTemplateList
    Inherits BusinessObjectListBase

    Public Sub New(parent As OcTemplateGroup)
        MyBase.New(LoadTable(parent), GetType(OcTemplate), parent)
    End Sub

    Public Overrides Function Belong(bo As BusinessObjectBase) As Boolean
        Return CType(bo, OcTemplate).OcTemplateGroupId.Equals(CType(Parent, OcTemplateGroup).Id)
    End Function

    Public Function Find(templateId As Guid) As OcTemplate
        Dim bo As OcTemplate
        For Each bo In Me
            If bo.Id.Equals(templateId) Then Return bo
        Next
        Return Nothing
    End Function

#Region "Class Methods"
    Private Shared Function LoadTable(parent As OcTemplateGroup) As DataTable
        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(OcTemplateList)) Then
                Dim dal As New OcTemplateDAL
                dal.LoadList(parent.Dataset, parent.Id)
                parent.AddChildrenCollection(GetType(OcTemplateList))
            End If
            Return parent.Dataset.Tables(OcTemplateDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region
End Class
