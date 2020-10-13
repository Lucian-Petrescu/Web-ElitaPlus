'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (4/4/2008)********************


Public Class LabelDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_LABEL"
    Public Const TABLE_KEY_NAME As String = "label_id"

    Public Const COL_NAME_LABEL_ID As String = "label_id"
    Public Const COL_NAME_UI_PROG_CODE As String = "ui_prog_code"
    Public Const COL_NAME_IN_USE As String = "in_use"
    Public Const COL_NAME_DICT_ITEM_ID As String = "dict_item_id"
    Public Const COL_DICT_ITEM_ENGLISH As String = "english"
    Public Const COL_NAME_LANGUAGE_ID As String = "language_id"

#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.new()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(familyDS As DataSet, id As Guid, Optional ByVal useFamilyId As Boolean = False)
        Dim selectStmt As String = Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_LABEL_ID, id.ToByteArray)}
        If useFamilyId Then
            selectStmt = Config("/SQL/LOAD_USING_DICT_ITEM_ID")
            parameters = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_DICT_ITEM_ID, id.ToByteArray)}
        End If

        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList() As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Return DBHelper.Fetch(selectStmt, TABLE_NAME)
    End Function

    Public Function LoadList(Language_Id As Guid, SearchMask As String, OrderByTrans As Boolean) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_UI_PROG_CODE_LIST")
        Dim ds As New DataSet
        'Dim da As DictItemTranslationDAL
        Dim whereClauseConditions As String = ""
        Dim bIsLikeClause As Boolean

        Dim parameters() As OracleParameter
        If ((Not (SearchMask Is Nothing)) AndAlso (DictFormatSearchMask(SearchMask))) Then
            If OrderByTrans Then
                whereClauseConditions &= Environment.NewLine & "and upper(" & COL_NAME_UI_PROG_CODE & ")" & SearchMask.ToUpper
            Else
                whereClauseConditions &= Environment.NewLine & "and upper(TRANS.TRANSLATION)" & SearchMask.ToUpper
            End If
        End If
        selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)

        If Not OrderByTrans Then
            selectStmt = selectStmt.Replace(DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, _
                                            Environment.NewLine & "ORDER BY upper(TRANS.TRANSLATION)")
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, _
                                             Environment.NewLine & "ORDER BY upper(" & COL_NAME_UI_PROG_CODE & ")")
        End If

        parameters = New OracleParameter() _
                                    {New OracleParameter(COL_NAME_LANGUAGE_ID, Language_Id.ToByteArray)}

        Try
            Return (DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function



#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If ds.Tables(TABLE_NAME) IsNot Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region


End Class


