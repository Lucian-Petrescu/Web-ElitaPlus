'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (9/5/2008)********************


Public Class NewDictionaryItemDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_NEW_DICTIONARY_ITEM"
    Public Const TABLE_KEY_NAME As String = "new_dict_item_id"

    Public Const COL_NAME_NEW_DICT_ITEM_ID As String = "new_dict_item_id"
    Public Const COL_NAME_UI_PROG_CODE As String = "ui_prog_code"
    Public Const COL_NAME_ENGLISH_TRANSLATION As String = "english_translation"
    Public Const COL_NAME_APPROVED As String = "approved"
    Public Const COL_NAME_DICT_ITEM_ID As String = "dict_item_id"
    Public Const COL_NAME_IMPORTED As String = "imported"

    Public Const COL_NAME_MSG_CODE As String = "msg_code"
    Public Const COL_NAME_MSG_TYPE As String = "msg_type"
    Public Const COL_NAME_MSG_PARAMETER_COUNT As String = "msg_parameter_count"

#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.new()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ByVal ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(ByVal familyDS As DataSet, ByVal id As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("new_dict_item_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList() As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
    End Function


#End Region

#Region "Overloaded Methods"
    'Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
    '    If ds Is Nothing Then
    '        Return
    '    End If
    '    If Not ds.Tables(Me.TABLE_NAME) Is Nothing Then
    '        MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
    '    End If
    'End Sub
    Public Overloads Sub UpdateFamily(ByVal familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)

        Dim LabelDal As New LabelDAL
        Dim DictItem As New DictionaryItemDAL
        Dim DictItemTrans As New DictItemTranslationDAL

        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try

            'First Pass updates Deletions
            If familyDataset.Tables.Count > 1 Then
                DictItemTrans.Update(familyDataset.GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)
                LabelDal.Update(familyDataset.GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)
                DictItem.Update(familyDataset.GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)
            End If

            MyBase.Update(familyDataset.Tables(Me.TABLE_NAME).GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)

            'Second Pass updates additions and changes
            Update(familyDataset.Tables(Me.TABLE_NAME).GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)

            If familyDataset.Tables.Count > 1 Then
                DictItem.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
                LabelDal.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
                DictItemTrans.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
            End If

            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                DBHelper.Commit(tr)
                familyDataset.AcceptChanges()
            End If
        Catch ex As Exception
            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                DBHelper.RollBack(tr)
            End If
            Throw ex
        End Try
    End Sub

    Public Overloads Sub DeleteAll()
        Dim selectStmt As String = Me.Config("/SQL/DELETE_ALL")
        Try
            DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

#End Region


End Class


