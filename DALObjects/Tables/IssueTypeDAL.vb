'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (5/11/2012)********************


Public Class IssueTypeDAL
    Inherits DALBase

    Public MyDropDownParentId As Guid
    Public MyDropDownListItemId As Guid
    Public MyDropDownNewItemCode As String
    Public MyDropDownNewItemDesc As String
    Public MyDropDownUser As String
    Public MyDropDownAction As String

#Region "Constants"

    Public Const TABLE_NAME As String = "ELP_ISSUE_TYPE"
    Public Const TABLE_LIST As String = "ELP_LIST"
    Public Const TABLE_LIST_ITEM As String = "ELP_LIST_ITEM"

    Public Const TABLE_KEY_NAME As String = "issue_type_id"
    Public Const COL_NAME_ISSUE_TYPE_ID As String = "issue_type_id"
    Public Const COL_NAME_CODE As String = "code"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_IS_SYSTEM_GENERATED_DESC As String = "is_system_generated_desc"
    Public Const COL_NAME_IS_SELF_CLEANING_DESC As String = "is_self_cleaning_desc"
    Public Const COL_NAME_IS_SYSTEM_GENERATED_ID As String = "is_system_generated"
    Public Const COL_NAME_IS_SELF_CLEANING_ID As String = "is_self_cleaning"

    Public Const COL_NAME_LIST_ID = "list_id"
    Public Const COL_NAME_LIST_ITEM_ID = "list_item_id"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("issue_type_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub


    Public Function LoadList(ByVal code As String, _
                                         ByVal description As String, _
                                         ByVal languageId As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = String.Empty
        Dim ds As New DataSet

        If ((Not (code Is Nothing)) AndAlso (Me.FormatSearchMask(code))) Then
            whereClauseConditions &= " WHERE " & Environment.NewLine & "UPPER(" & Me.COL_NAME_CODE & ")" & code.ToUpper
        End If

        If ((Not (description Is Nothing)) AndAlso (Me.FormatSearchMask(description))) Then
            If (Not (whereClauseConditions = String.Empty)) Then
                whereClauseConditions &= " AND "
            Else
                whereClauseConditions &= " WHERE "
            End If
            whereClauseConditions &= Environment.NewLine & "UPPER(" & Me.COL_NAME_DESCRIPTION & ")" & description.ToUpper
        End If

        selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)

        Try
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

#End Region

#Region "CRUD Methods"
    
    Public Function GetDropdownId(ByVal listCode As String) As Guid
        Dim selectStmt As String = Me.Config("/SQL/DROPDOWN_ID")
        Dim ds As New DataSet
        Dim listCodeParam As DBHelper.DBHelperParameter
        Dim id As Byte()
        Try
            listCodeParam = New DBHelper.DBHelperParameter(Me.COL_NAME_CODE, listCode)
            id = DBHelper.Fetch(ds, selectStmt, Me.TABLE_LIST, New DBHelper.DBHelperParameter() {listCodeParam}).Tables(Me.TABLE_LIST).Rows(0)(COL_NAME_LIST_ID)
            If Not id Is Nothing Then
                Return New Guid(id)
            Else
                Return Guid.Empty
            End If

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetDropdownItemId(ByVal dropdownId As Guid, ByVal itemCode As String) As Guid
        Dim selectStmt As String = Me.Config("/SQL/DROPDOWN_ITEM_ID")
        Dim ds As New DataSet
        Dim itemCodeParam As DBHelper.DBHelperParameter
        Dim dropdownIdParam As DBHelper.DBHelperParameter
        Dim tempDS As DataSet
        Dim id As Byte()
        Try
            itemCodeParam = New DBHelper.DBHelperParameter(Me.COL_NAME_CODE, itemCode)
            dropdownIdParam = New DBHelper.DBHelperParameter(Me.COL_NAME_LIST_ID, DropdownId.ToByteArray)
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_LIST_ITEM, New DBHelper.DBHelperParameter() {itemCodeParam, dropdownIdParam})
            If ds.Tables(Me.TABLE_LIST_ITEM).Rows.Count > 0 Then
                id = ds.Tables(Me.TABLE_LIST_ITEM).Rows(0)(COL_NAME_LIST_ITEM_ID)
                Return New Guid(id)
            Else
                Return Guid.Empty
            End If

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


    Public Function AddDropdownItem(ByVal code As String, ByVal maintainable_by_user As String, ByVal display_to_user As String, ByVal list_id As Guid, ByVal englishTranslation As String, ByVal userId As String, ByVal tr As IDbTransaction) As Integer
        Dim selectStmt As String = Me.Config("/SQL/ADD_DROPDOWN_ITEM")

        Dim inputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                            New DBHelper.DBHelperParameter("p_code", code), _
                            New DBHelper.DBHelperParameter("p_maintainable_by_user", maintainable_by_user), _
                            New DBHelper.DBHelperParameter("p_display_to_user", display_to_user), _
                            New DBHelper.DBHelperParameter("p_list_id", list_id), _
                            New DBHelper.DBHelperParameter("p_english_translation", englishTranslation), _
                            New DBHelper.DBHelperParameter("p_user", userId)}
        Dim outputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                            New DBHelper.DBHelperParameter("p_return_code", GetType(Integer))}

        DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameters, tr)

        Dim retVal As Integer
        retVal = CType(outputParameters(0).Value, Integer)

        Return retVal
    End Function

    Public Function UpdateDropdownItem(ByVal listItemId As Guid, ByVal code As String, ByVal maintainable_by_user As String, ByVal display_to_user As String, ByVal englishTranslation As String, ByVal userId As String, ByVal Transaction As IDbTransaction) As Integer
        Dim selectStmt As String = Me.Config("/SQL/UPDATE_DROPDOWN_ITEM")

        Dim inputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                            New DBHelper.DBHelperParameter("p_list_item_id", listItemId.ToByteArray), _
                            New DBHelper.DBHelperParameter("p_code", code), _
                            New DBHelper.DBHelperParameter("p_maintainable_by_user", maintainable_by_user), _
                            New DBHelper.DBHelperParameter("p_display_to_user", display_to_user), _
                            New DBHelper.DBHelperParameter("p_english_translation", englishTranslation), _
                            New DBHelper.DBHelperParameter("p_user", userId)}
        Dim outputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                            New DBHelper.DBHelperParameter("p_return_code", GetType(Integer))}

        DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameters, Transaction)

        Dim retVal As Integer
        retVal = CType(outputParameters(0).Value, Integer)

        Return retVal
    End Function

    Public Function DeleteDropdownItem(ByVal listItemId As Guid) As Integer
        Dim selectStmt As String = Me.Config("/SQL/DELETE_DROPDOWN_ITEM")

        Dim inputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                            New DBHelper.DBHelperParameter("p_list_item_id", listItemId.ToByteArray)}
        Dim outputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                            New DBHelper.DBHelperParameter("p_return_code", GetType(Integer))}

        DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameters)

        Dim retVal As Integer
        retVal = CType(outputParameters(0).Value, Integer)

        Return retVal
    End Function

#End Region

#Region "Overloaded Methods"

    Public Sub Update(ByVal ds As DataSet, ByRef Transaction As IDbTransaction, Optional ByVal changesFilter As DataRowState = Nothing)
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
        Dim retVal As Integer = 0

        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If

        Try
            If Me.MyDropDownAction = "Add" Then
                MyBase.Update(familyDataset.Tables(Me.TABLE_NAME), tr, DataRowState.Added)
                retVal = Me.AddDropdownItem(Me.MyDropDownNewItemCode, "Y", "Y", Me.MyDropDownParentId, Me.MyDropDownNewItemDesc, Me.MyDropDownUser, tr)
            ElseIf Me.MyDropDownAction = "Update" Then
                MyBase.Update(familyDataset.Tables(Me.TABLE_NAME), tr, DataRowState.Modified)
                retVal = Me.UpdateDropdownItem(Me.MyDropDownListItemId, Me.MyDropDownNewItemCode, "Y", "Y", Me.MyDropDownNewItemDesc, Me.MyDropDownUser, tr)
            Else
                Me.Update(familyDataset, tr, DataRowState.Deleted)
                retVal = Me.DeleteDropdownItem(Me.MyDropDownListItemId)
            End If

            If retVal <> 0 Then
                DBHelper.RollBack(tr)
                Throw New DatabaseException("Error saving data")
            Else
                If Transaction Is Nothing Then
                    DBHelper.Commit(tr)
                    familyDataset.AcceptChanges()
                End If
            End If

        Catch ex As Exception
            If Transaction Is Nothing Then
                DBHelper.RollBack(tr)
            End If
            Throw
        End Try
    End Sub

#End Region

End Class


