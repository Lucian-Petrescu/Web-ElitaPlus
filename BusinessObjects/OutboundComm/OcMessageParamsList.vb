Public Class OcMessageParamsList
    Inherits BusinessObjectListBase

    Public Sub New(ByVal parent As OcMessage)
        MyBase.New(LoadTable(parent), GetType(OcMessageParams), parent)
    End Sub

    Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
        Return CType(bo, OcMessageParams).OcMessageId.Equals(CType(Parent, OcMessage).Id)
    End Function

    Public Function Find(ByVal messageParamsId As Guid) As OcMessageParams
        Dim bo As OcMessageParams
        For Each bo In Me
            If bo.Id.Equals(messageParamsId) Then Return bo
        Next
        Return Nothing
    End Function

#Region "Class Methods"
    Private Shared Function LoadTable(ByVal parent As OcMessage) As DataTable
        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(OcMessageParamsList)) Then
                Dim dal As New OcMessageParamsDAL
                dal.LoadList(parent.Dataset, parent.Id)
                parent.AddChildrenCollection(GetType(OcMessageParamsList))
            End If
            Return parent.Dataset.Tables(OcMessageParamsDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region

End Class
