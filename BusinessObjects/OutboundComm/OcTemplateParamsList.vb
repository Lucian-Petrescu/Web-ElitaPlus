Public Class OcTemplateParamsList
    Inherits BusinessObjectListBase

    Public Sub New(ByVal parent As OcTemplate)
        MyBase.New(LoadTable(parent), GetType(OcTemplateParams), parent)
    End Sub

    Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
        Return CType(bo, OcTemplateParams).OcTemplateId.Equals(CType(Parent, OcTemplate).Id)
    End Function

    Public Function Find(ByVal templateParamsId As Guid) As OcTemplateParams
        Dim bo As OcTemplateParams
        For Each bo In Me
            If bo.Id.Equals(templateParamsId) Then Return bo
        Next
        Return Nothing
    End Function

    Public Function Delete(ByVal childId As Guid)
        Dim bo As OcTemplateParams
        Dim dr As DataRow

        dr = BusinessObjectBase.FindRow(childId, OcTemplateParams.TemplateParamsDV.COL_OC_TEMPLATE_PARAMS_ID, Parent.Dataset.Tables(OcTemplateParamsDAL.TABLE_NAME))

        If Not (dr Is Nothing) Then
            Parent.Dataset.Tables(OcTemplateParamsDAL.TABLE_NAME).Rows.Remove(dr)
        End If
    End Function

#Region "Class Methods"
    Private Shared Function LoadTable(ByVal parent As OcTemplate) As DataTable
        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(OcTemplateParamsList)) Then
                Dim dal As New OcTemplateParamsDAL
                dal.LoadList(parent.Dataset, parent.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                parent.AddChildrenCollection(GetType(OcTemplateParamsList))
            End If
            Return parent.Dataset.Tables(OcTemplateParamsDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region
End Class
