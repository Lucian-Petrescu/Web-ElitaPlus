Public Class OcMessageAttemptsList
    Inherits BusinessObjectListBase

    Public Sub New(ByVal parent As OcMessage, Optional ByVal clean As Boolean = False)
        MyBase.New(LoadTable(parent,clean), GetType(OcMessageAttempts), parent)
    End Sub

    Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
        Return CType(bo, OcMessageAttempts).OcMessageId.Equals(CType(Parent, OcMessage).Id)
    End Function

    Public Function Find(ByVal templateParamsId As Guid) As OcMessageAttempts
        Dim bo As OcMessageAttempts
        For Each bo In Me
            If bo.Id.Equals(templateParamsId) Then Return bo
        Next
        Return Nothing
    End Function

#Region "Class Methods"
    Private Shared Function LoadTable(ByVal parent As OcMessage,Optional ByVal clean As Boolean = False) As DataTable
        Try
            If (clean) Then
                parent.Dataset.Tables.Remove("ELP_OC_MESSAGE_ATTEMPS")
                Dim dal As New OcMessageAttemptsDAL
                dal.LoadList(parent.Dataset, parent.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                parent.AddChildrenCollection(GetType(OcMessageAttemptsList))
            
            ElseIf Not parent.IsChildrenCollectionLoaded(GetType(OcMessageAttemptsList)) Then
            
                Dim dal As New OcMessageAttemptsDAL
                dal.LoadList(parent.Dataset, parent.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                parent.AddChildrenCollection(GetType(OcMessageAttemptsList))
            End If

            Return parent.Dataset.Tables(OcMessageAttemptsDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region

End Class
