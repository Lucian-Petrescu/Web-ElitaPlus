'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (4/4/2008)********************


Public Class DictItemTranslationDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_DICT_ITEM_TRANSLATION"
    Public Const TABLE_KEY_NAME As String = "dict_item_translation_id"

    Public Const COL_NAME_DICT_ITEM_TRANSLATION_ID As String = "dict_item_translation_id"
    Public Const COL_NAME_TRANSLATION As String = "translation"
    Public Const COL_NAME_LANGUAGE_ID As String = "language_id"
    Public Const COL_NAME_DICT_ITEM_ID As String = "dict_item_id"

    Public Const COL_DICT_ITEM_TRANSLATION As String = "translation"
    Public Const COL_DICT_ITEM_ENGLISH As String = "english"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_DICT_ITEM_ID, id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(ByVal Id As Guid, ByVal familyDataset As DataSet) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_TRANS_LIST")
        Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)

        Dim parameters() As OracleParameter
        parameters = New OracleParameter() {New OracleParameter(Me.COL_NAME_DICT_ITEM_ID, Id.ToByteArray)}
        DBHelper.Fetch(familyDataset, selectStmt, Me.TABLE_NAME, parameters)

    End Function
    Public Function LoadList(ByVal Language_Id As Guid, ByVal CompanyLangId As Guid, ByVal SearchMask As String, ByVal OrderByTrans As Boolean, Optional ByVal AdminUser As Boolean = False) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim ds As New DataSet
        'Dim da As DictItemTranslationDAL
        Dim whereClauseConditions As String = ""
        Dim bIsLikeClause As Boolean
        Dim parameters() As OracleParameter

        If ((Not (SearchMask Is Nothing)) AndAlso (Me.DictFormatSearchMask(SearchMask))) Then
            If OrderByTrans Then
                whereClauseConditions &= Environment.NewLine & "and upper(" & Me.COL_NAME_TRANSLATION & ")" & SearchMask.ToUpper
            Else
                whereClauseConditions &= Environment.NewLine & "and upper(" & Me.COL_DICT_ITEM_ENGLISH & ")" & SearchMask.ToUpper
            End If
        End If
        selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)

        If Not OrderByTrans Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, _
                                            Environment.NewLine & "ORDER BY upper(" & Me.COL_DICT_ITEM_ENGLISH & ")")
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, _
                                             Environment.NewLine & "ORDER BY upper(" & Me.COL_DICT_ITEM_TRANSLATION & ")")
        End If

        parameters = New OracleParameter() _
                                    {New OracleParameter(Me.COL_NAME_LANGUAGE_ID, CompanyLangId.ToByteArray), _
                                     New OracleParameter(Me.COL_NAME_LANGUAGE_ID, Language_Id.ToByteArray)}
        Try
            Return (DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadList(ByVal dictItemId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_TRANS_LIST")
        Dim ds As New DataSet
        'Dim da As DictItemTranslationDAL
        Dim whereClauseConditions As String = ""
        Dim bIsLikeClause As Boolean
        Dim parameters() As OracleParameter

        parameters = New OracleParameter() _
                                    {New OracleParameter(Me.COL_NAME_DICT_ITEM_ID, dictItemId.ToByteArray)}
        Try
            Return (DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(Me.TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub

    'This method was added manually to accommodate BO families Save
    Public Overloads Sub UpdateFamily(ByVal familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)

        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            'First Pass updates Deletions
            MyBase.Update(familyDataset.Tables(Me.TABLE_NAME).GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)

            'Second Pass updates additions and changes
            Update(familyDataset.Tables(Me.TABLE_NAME).GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
            
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


#End Region

End Class


