Public Class OcTemplateRecipientList
    Inherits BusinessObjectListBase

    Public Sub New(ByVal parent As OcTemplate)
        MyBase.New(LoadTable(parent), GetType(OcTemplateRecipient), parent)
    End Sub

    Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
        Return CType(bo, OcTemplateRecipient).OcTemplateId.Equals(CType(Parent, OcTemplate).Id)
    End Function

    Public Function Find(ByVal templateRecipientId As Guid) As OcTemplateRecipient
        Dim bo As OcTemplateRecipient
        For Each bo In Me
            If bo.Id.Equals(templateRecipientId) Then Return bo
        Next
        Return Nothing
    End Function

    Public Function Delete(ByVal childId As Guid)
        Dim bo As OcTemplateRecipient
        Dim dr As DataRow

        dr = BusinessObjectBase.FindRow(childId, OcTemplateRecipient.TemplateRecipientsDV.COL_OC_TEMPLATE_RECIPIENT_ID, Parent.Dataset.Tables(OcTemplateRecipientDAL.TABLE_NAME))

        If Not (dr Is Nothing) Then
            Parent.Dataset.Tables(OcTemplateRecipientDAL.TABLE_NAME).Rows.Remove(dr)
        End If
    End Function

#Region "Class Methods"
    Private Shared Function LoadTable(ByVal parent As OcTemplate) As DataTable
        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(OcTemplateRecipientList)) Then
                Dim dal As New OcTemplateRecipientDAL
                dal.LoadList(parent.Dataset, parent.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                parent.AddChildrenCollection(GetType(OcTemplateRecipientList))
            End If
            Return parent.Dataset.Tables(OcTemplateRecipientDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region
End Class
