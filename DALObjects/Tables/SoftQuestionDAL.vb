'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (11/11/2004)********************


Public Class SoftQuestionDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_SOFT_QUESTION"
    Public Const TABLE_KEY_NAME As String = "soft_question_id"

    Public Const COL_NAME_SOFT_QUESTION_ID As String = "soft_question_id"
    Public Const COL_NAME_SOFT_QUESTION_GROUP_ID As String = "soft_question_group_id"
    Public Const COL_NAME_PARENT_ID As String = "parent_id"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_RISKTYPE_ID As String = "risk_type_id"
    Public Const COL_NAME_CHILD_ORDER As String = "child_order"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COMPANY_GROUP As String = "company_group_id"

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

    Public Sub Load(familyDS As DataSet, id As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("soft_question_id", id.ToByteArray)}
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

    Public Function GetMaxChildOrder(parentID As Guid) As Long
        Dim selectStmt As String = Config("/SQL/LOAD_MAX_CHILD_ORDER")
        Dim ds As DataSet = New DataSet
        Try
            Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("parent_id", parentID.ToByteArray)}
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            If ds.Tables.Count > 0 AndAlso ds.Tables(TABLE_NAME).Rows.Count > 0 Then
                Return ds.Tables(TABLE_NAME).Rows(0)(COL_NAME_CHILD_ORDER)
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Return 0
            'Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetSoftQuestionId(parentID As Guid, ChildOrder As Long) As Guid
        Dim selectStmt As String = Config("/SQL/LOAD_SOFTQUESTIONID")
        Dim ds As DataSet = New DataSet
        Try
            Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("soft_question_id", ChildOrder), _
                                                                                               New DBHelper.DBHelperParameter("parent_id", parentID.ToByteArray)}
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            If ds.Tables.Count > 0 AndAlso ds.Tables(TABLE_NAME).Rows.Count > 0 Then
                Return New Guid(CType(ds.Tables(TABLE_NAME).Rows(0)(COL_NAME_SOFT_QUESTION_ID), Byte()))
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function


    Public Function LoadList(softQuestionGrpId As Guid, companyGroupId As Guid) As DataSet
        Dim selectStmt As String
        Dim parameters() As DBHelper.DBHelperParameter

        If softQuestionGrpId.Equals(Guid.Empty) Then
            selectStmt = Config("/SQL/LOAD_ALL_LIST")
            parameters = New DBHelper.DBHelperParameter() _
                                        {New DBHelper.DBHelperParameter(COMPANY_GROUP, companyGroupId.ToByteArray())}
        Else
            selectStmt = Config("/SQL/LOAD_LIST")
            parameters = New DBHelper.DBHelperParameter() _
                                        {New DBHelper.DBHelperParameter(COL_NAME_SOFT_QUESTION_GROUP_ID, softQuestionGrpId.ToByteArray())}
        End If

        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadSoftQuestionGroups(companyGroupId As Guid) As DataSet
        Dim selectStmt As String
        Dim parameters() As DBHelper.DBHelperParameter

        selectStmt = Config("/SQL/LOAD_GROUPS")
        parameters = New DBHelper.DBHelperParameter() _
                                        {New DBHelper.DBHelperParameter(COMPANY_GROUP, companyGroupId.ToByteArray())}

        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function
    Public Function LoadSoftQuestionGroupForRiskType(companyGroupId As Guid, riskTypeId As Guid) As DataSet
        Dim selectStmt As String
        Dim parameters() As DBHelper.DBHelperParameter

        selectStmt = Config("/SQL/LOAD_GROUP_FOR_RISKTYPE")
        parameters = New DBHelper.DBHelperParameter() _
                                        {New DBHelper.DBHelperParameter(COMPANY_GROUP, companyGroupId.ToByteArray()), _
                                         New DBHelper.DBHelperParameter(COL_NAME_RISKTYPE_ID, riskTypeId.ToByteArray())}

        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function
    Public Function LoadAvailableSoftQuestionGroups(companyGroupId As Guid) As DataSet
        Dim selectStmt As String
        Dim parameters() As DBHelper.DBHelperParameter

        selectStmt = Config("/SQL/LOAD_AVAIL_GROUPS")
        parameters = New DBHelper.DBHelperParameter() _
                                        {New DBHelper.DBHelperParameter(COMPANY_GROUP, companyGroupId.ToByteArray())}

        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadChildren(parentID As Guid) As DataSet
        Dim selectStmt As String
        Dim parameters() As DBHelper.DBHelperParameter

        selectStmt = Config("/SQL/LOAD_CHILDREN")
        parameters = New DBHelper.DBHelperParameter() _
                                        {New DBHelper.DBHelperParameter(COL_NAME_PARENT_ID, parentID.ToByteArray())}

        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function
    '<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>
    Public Function LoadReOrderGroup(companyGroupId As Guid) As DataSet
        Dim selectStmt As String
        Dim parameters() As DBHelper.DBHelperParameter

        selectStmt = Config("/SQL/LOAD_Reorder_GROUPS")
        parameters = New DBHelper.DBHelperParameter() _
                                        {New DBHelper.DBHelperParameter(COMPANY_GROUP, companyGroupId.ToByteArray())}

        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadReOrderQuestion(SoftQuestionGroupId As String) As DataSet
        Dim selectStmt As String
        Dim parameters() As DBHelper.DBHelperParameter

        selectStmt = Config("/SQL/LOAD_Reorder_Question")
        parameters = New DBHelper.DBHelperParameter() _
                                        {New DBHelper.DBHelperParameter(COL_NAME_SOFT_QUESTION_GROUP_ID, SoftQuestionGroupId)}

        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
    Public Function LoadReOrderQuestion2(SoftQuestionId As String) As DataSet
        Dim selectStmt As String
        Dim parameters() As DBHelper.DBHelperParameter

        selectStmt = Config("/SQL/LOAD_Reorder_Question2")
        parameters = New DBHelper.DBHelperParameter() _
                                        {New DBHelper.DBHelperParameter(COL_NAME_SOFT_QUESTION_ID, SoftQuestionId)}

        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
    Public Function LoadLastRow(soft_question_group_id As String, PARENT_ID As String, CHILD_ORDER As String, DESCRIPTION As String) As DataSet
        Dim selectStmt As String
        Dim parameters() As DBHelper.DBHelperParameter

        selectStmt = Config("/SQL/LOAD_LAST_ROW")
        parameters = New DBHelper.DBHelperParameter() _
                                        {New DBHelper.DBHelperParameter(COL_NAME_SOFT_QUESTION_GROUP_ID, soft_question_group_id), _
                                    New DBHelper.DBHelperParameter(COL_NAME_PARENT_ID, PARENT_ID), _
                                    New DBHelper.DBHelperParameter(COL_NAME_CHILD_ORDER, CHILD_ORDER), _
                                    New DBHelper.DBHelperParameter(COL_NAME_DESCRIPTION, DESCRIPTION)}

        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
    Public Function LoadReOrderQuestion3(soft_question_group_id As String) As DataSet
        Dim selectStmt As String
        Dim parameters() As DBHelper.DBHelperParameter

        selectStmt = Config("/SQL/LOAD_Reorder_Question3")
        parameters = New DBHelper.DBHelperParameter() _
                                        {New DBHelper.DBHelperParameter(COL_NAME_SOFT_QUESTION_GROUP_ID, soft_question_group_id)}

        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
    Public Function LoadNullParentQuestion(SoftQuestionId As String) As DataSet
        Dim selectStmt As String
        Dim parameters() As DBHelper.DBHelperParameter

        selectStmt = Config("/SQL/LOAD_Select_Null_ParentID")
        parameters = New DBHelper.DBHelperParameter() _
                                        {New DBHelper.DBHelperParameter(COL_NAME_SOFT_QUESTION_ID, SoftQuestionId)}

        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function InsertSoftQuestions(soft_question_group_id As String, parent_id As String, child_order As Integer, description As String)
        Dim selectStmt As String
        Dim parameters() As DBHelper.DBHelperParameter

        selectStmt = Config("/SQL/INSERT_ReOrder")
        parameters = New DBHelper.DBHelperParameter() _
                                    {New DBHelper.DBHelperParameter(COL_NAME_SOFT_QUESTION_GROUP_ID, soft_question_group_id), _
                                    New DBHelper.DBHelperParameter(COL_NAME_PARENT_ID, parent_id), _
                                    New DBHelper.DBHelperParameter(COL_NAME_CHILD_ORDER, child_order), _
                                    New DBHelper.DBHelperParameter(COL_NAME_DESCRIPTION, description)}

        Try
            'Dim ds As New DataSet
            DBHelper.Execute(selectStmt, parameters)
            'Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function DeleteOrderQuestion(SoftQuestionGroupId As String)
        Dim selectStmt As String
        Dim parameters() As DBHelper.DBHelperParameter

        selectStmt = Config("/SQL/Delete_ReOrder")
        parameters = New DBHelper.DBHelperParameter() _
                                        {New DBHelper.DBHelperParameter(COL_NAME_SOFT_QUESTION_GROUP_ID, SoftQuestionGroupId)}

        Try
            'Dim ds As New DataSet
            DBHelper.Execute(selectStmt, parameters)
            'Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
    '<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>'<0>
#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds.Tables(TABLE_NAME) IsNot Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region


End Class


