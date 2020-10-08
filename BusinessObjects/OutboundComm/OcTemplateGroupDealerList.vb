Public Class OcTemplateGroupDealerList
    Inherits BusinessObjectListBase

    Public Sub New(parent As OcTemplateGroup)
        MyBase.New(LoadTable(parent), GetType(OcTemplateGroupDealer), parent)
    End Sub

    Public Overrides Function Belong(bo As BusinessObjectBase) As Boolean
        Return CType(bo, OcTemplateGroupDealer).OcTemplateGroupId.Equals(CType(Parent, OcTemplateGroup).Id)
    End Function

    Public Function Find(dealerId As Guid) As OcTemplateGroupDealer
        Dim bo As OcTemplateGroupDealer
        For Each bo In Me
            If bo.DealerId.Equals(dealerId) Then Return bo
        Next
        Return Nothing
    End Function

#Region "Class Methods"
    Private Shared Function LoadTable(parent As OcTemplateGroup) As DataTable
        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(OcTemplateGroupDealerList)) Then
                Dim dal As New OcTemplateGroupDealerDAL
                dal.LoadList(parent.Dataset, parent.Id)
                parent.AddChildrenCollection(GetType(OcTemplateGroupDealerList))
            End If
            Return parent.Dataset.Tables(OcTemplateGroupDealerDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region
End Class
