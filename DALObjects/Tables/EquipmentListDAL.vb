
Public Class EquipmentListDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_EQUIPMENT_LIST"
    Public Const TABLE_DEALER As String = "ELP_DEALER"
    Public Const TABLE_NAME_COMPANY_COUNTRY As String = "Countries"
    Public Const TABLE_KEY_NAME As String = "equipment_list_id"
    Public Const TABLE_NAME_GROUP_COMPANIES As String = "group_companies"

    Public Const COL_NAME_DEALER_EQUIPMENT_LIST_CODE = "equipment_list_code"
    Public Const COL_NAME_EQUIPMENT_LIST_ID = "equipment_list_id"
    Public Const COL_NAME_CODE = "code"
    Public Const COL_NAME_DESCRIPTION = "description"

    Public Const COL_NAME_COMMENTS = "comments"
    Public Const COL_NAME_EFFECTIVE = "effective"
    Public Const COL_NAME_EXPIRATION = "expiration"

    Private Const DSNAME As String = "LIST"
    Public Const WILDCARD As Char = "%"
#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.new()
    End Sub

#End Region

#Region "CRUD Methods"

    Public Sub LoadSchema(ByVal ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(ByVal familyDS As DataSet, ByVal id As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD")
        Dim parameters() As OracleParameter = New OracleParameter() {New OracleParameter("equipment_list_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Sub

    Public Function LoadList(ByVal code As String, _
                                           ByVal description As String, ByVal effective As String, _
                                           ByVal expiration As String, _
                                           ByVal companyIds As ArrayList, ByVal languageId As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = String.Empty
        Dim ds As New DataSet
        Dim languageIdParam As DBHelper.DBHelperParameter
        Dim bIsLikeClause As Boolean = False
        Dim bIsWhereClause As Boolean = False

        bIsLikeClause = IsThereALikeClause(description, code)

        If ((Not (code Is Nothing)) AndAlso (Me.FormatSearchMask(code))) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(" & Me.COL_NAME_CODE & ")" & code.ToUpper
            bIsWhereClause = True
        End If
        If ((Not (description Is Nothing)) AndAlso (Me.FormatSearchMask(description))) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(" & Me.COL_NAME_DESCRIPTION & ")" & description.ToUpper
            bIsWhereClause = True
        End If
        If (Not (effective Is String.Empty)) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "(" & Me.COL_NAME_EFFECTIVE & ")" & " =  to_date('" & DateHelper.GetDateValue(effective).ToString("MM/dd/yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss')"
            bIsWhereClause = True
        End If
        If (Not (expiration Is String.Empty)) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "(" & Me.COL_NAME_EXPIRATION & ")" & " =  to_date('" & DateHelper.GetDateValue(expiration).ToString("MM/dd/yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss')"
            bIsWhereClause = True
        End If

        If bIsWhereClause Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        End If

        Try
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Sub LoadEQ(ByVal DS As DataSet, ByVal id As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD_EQ_LIST")
        Dim parameters() As OracleParameter = New OracleParameter() {New OracleParameter("equipment_list_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(DS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Sub

    Public Function CheckOverlap(ByVal code As String, _
                                           ByVal effective As String, _
                                           ByVal expiration As String, _
                                           ByVal companyIds As ArrayList, ByVal languageId As Guid, ByVal listId As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/CHECK_OVERLAP")
        Dim whereClauseConditions As String = String.Empty
        Dim ds As New DataSet
        Dim languageIdParam As DBHelper.DBHelperParameter
        Dim EquipmentListIdParam As DBHelper.DBHelperParameter
        Dim bIsWhereClause As Boolean = False

        If ((Not (code Is Nothing)) AndAlso (Me.FormatSearchMask(code))) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(" & Me.COL_NAME_CODE & ")" & code.ToUpper
            bIsWhereClause = True
        End If
        If (Not String.IsNullOrEmpty(effective) And Not String.IsNullOrEmpty(expiration)) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "TO_DATE('" & DateHelper.GetDateValue(expiration).ToString("MM/dd/yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss') BETWEEN " & Me.COL_NAME_EFFECTIVE & " AND " & Me.COL_NAME_EXPIRATION
            bIsWhereClause = True
        End If
        If (Not (effective Is String.Empty)) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "TO_DATE('" & DateHelper.GetDateValue(effective).ToString("MM/dd/yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss')" & " >=  (SELECT MAX(EFFECTIVE) FROM ELP_EQUIPMENT_LIST WHERE UPPER(" & Me.COL_NAME_CODE & ")" & code.ToUpper & ")"
            bIsWhereClause = True
        End If
        If bIsWhereClause Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        EquipmentListIdParam = New DBHelper.DBHelperParameter(Me.COL_NAME_EQUIPMENT_LIST_ID, listId.ToByteArray)

        Try
            Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {EquipmentListIdParam})
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function CheckOverlapToExpire(ByVal code As String, _
                                           ByVal effective As DateType, _
                                           ByVal expiration As DateType, _
                                           ByVal companyIds As ArrayList, ByVal languageId As Guid, ByVal listId As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/CHECK_LIST_OVERLAP_TO_EXPIRE")
        Dim whereClauseConditions As String = String.Empty
        Dim ds As New DataSet
        Dim languageIdParam As DBHelper.DBHelperParameter
        Dim RowNumParam As DBHelper.DBHelperParameter
        Dim EquipmentListIdParam As DBHelper.DBHelperParameter
        Dim EquipmentCodeParam As DBHelper.DBHelperParameter
        Dim EquipmentEffectiveParam As DBHelper.DBHelperParameter
        Dim EquipmentExpirationParam As DBHelper.DBHelperParameter
        Dim bIsWhereClause As Boolean = False

        RowNumParam = New DBHelper.DBHelperParameter(PAR_NAME_ROW_NUMBER, 101, GetType(Integer))
        EquipmentCodeParam = New DBHelper.DBHelperParameter(Me.COL_NAME_CODE, code.ToUpper)
        EquipmentEffectiveParam = New DBHelper.DBHelperParameter(Me.COL_NAME_EFFECTIVE, effective)
        EquipmentListIdParam = New DBHelper.DBHelperParameter(Me.COL_NAME_EQUIPMENT_LIST_ID, listId.ToByteArray)

        Try
             If ((Not (code Is Nothing)) AndAlso (Me.FormatSearchMask(code))) Then
                whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(" & Me.COL_NAME_CODE & ")" & code.ToUpper
                bIsWhereClause = True
            End If
            If (Not String.IsNullOrEmpty(effective) And Not String.IsNullOrEmpty(expiration)) Then
                whereClauseConditions &= " AND " & Environment.NewLine & "TO_DATE('" & DateTime.Parse(expiration).ToString("MM/dd/yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss') BETWEEN " & Me.COL_NAME_EFFECTIVE & " AND " & Me.COL_NAME_EXPIRATION
                bIsWhereClause = True
            End If
            If (Not (effective Is String.Empty)) Then
                whereClauseConditions &= " AND " & Environment.NewLine & "TO_DATE('" & DateTime.Parse(effective).ToString("MM/dd/yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss')" & " >=  (SELECT MAX(EFFECTIVE) FROM ELP_EQUIPMENT_LIST WHERE UPPER(" & Me.COL_NAME_CODE & ")" & code.ToUpper & ")"
                bIsWhereClause = True
            End If
            If bIsWhereClause Then
                selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
            Else
                selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
            End If

            EquipmentListIdParam = New DBHelper.DBHelperParameter(Me.COL_NAME_EQUIPMENT_LIST_ID, listId.ToByteArray)
            Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {EquipmentListIdParam})

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function CheckDurationOverlap(ByVal code As String, _
                                           ByVal effective As String, _
                                           ByVal expiration As String, _
                                           ByVal companyIds As ArrayList, ByVal languageId As Guid, ByVal listId As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/CHECK_OVERLAP_DURATION")
        Dim whereClauseConditions As String = String.Empty
        Dim ds As New DataSet
        Dim languageIdParam As DBHelper.DBHelperParameter
        Dim equipmentListIdParam As DBHelper.DBHelperParameter
        Dim bIsWhereClause As Boolean = False

        If ((Not (code Is Nothing)) AndAlso (Me.FormatSearchMask(code))) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(" & Me.COL_NAME_CODE & ")" & code.ToUpper
            bIsWhereClause = True
        End If
        If (Not String.IsNullOrEmpty(effective)) Then
            whereClauseConditions &= " AND ((" & Environment.NewLine & " to_date('" & DateHelper.GetDateValue(effective).ToString("MM/dd/yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss') BETWEEN " & Me.COL_NAME_EFFECTIVE & " AND " & Me.COL_NAME_EXPIRATION
            bIsWhereClause = True
        End If
        If (Not String.IsNullOrEmpty(expiration)) Then
            whereClauseConditions &= ") OR (" & Environment.NewLine & " to_date('" & DateHelper.GetDateValue(expiration).ToString("MM/dd/yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss') BETWEEN " & Me.COL_NAME_EFFECTIVE & " AND " & Me.COL_NAME_EXPIRATION & "))"
            bIsWhereClause = True
        End If

        If bIsWhereClause Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        equipmentListIdParam = New DBHelper.DBHelperParameter(Me.COL_NAME_EQUIPMENT_LIST_ID, listId.ToByteArray)

        Try
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {equipmentListIdParam})
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function ExpireList(ByVal PrevListDS As DataSet, ByVal effective As String) As Boolean

        Dim selectStmt As String = Me.Config("/SQL/EXPIRE_LIST")
        Dim ds As New DataSet
        Dim EquipmentListIdParam As DBHelper.DBHelperParameter
        Dim EquipmentExpiration As DBHelper.DBHelperParameter
        Dim index As Integer
        Try
            For index = 0 To PrevListDS.Tables(0).Rows.Count - 1
                EquipmentListIdParam = New DBHelper.DBHelperParameter(Me.COL_NAME_EQUIPMENT_LIST_ID, PrevListDS.Tables(0).Rows(index)(Me.COL_NAME_EQUIPMENT_LIST_ID))
                EquipmentExpiration = New DBHelper.DBHelperParameter(Me.COL_NAME_EXPIRATION, DateHelper.GetDateValue(effective).AddMinutes(-1))
                DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {EquipmentExpiration, EquipmentListIdParam})
                ExpireListEquipments(PrevListDS.Tables(0).Rows(index)(Me.COL_NAME_EQUIPMENT_LIST_ID), DateHelper.GetDateValue(effective).AddMinutes(-1).ToString)
            Next
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        Return False
    End Function

    Public Function ExpireListEquipments(ByVal equipmentListId As Byte(), ByVal expiration As String) As Boolean

        Dim selectStmt As String = Me.Config("/SQL/EXPIRE_EQUIPMENTS")
        Dim ds As New DataSet
        Dim EquipmentListIdParam As DBHelper.DBHelperParameter
        Dim whereClauseConditions As String = String.Empty
        Dim dynamicFieldClauseConditions As String = String.Empty
        Dim bIsWhereClause As Boolean = False
        Dim bIsFieldClause As Boolean = False

        If (Not (expiration Is String.Empty)) Then
            dynamicFieldClauseConditions &= Environment.NewLine & "(" & Me.COL_NAME_EXPIRATION & ")" & " =  to_date('" & DateHelper.GetDateValue(expiration).ToString("MM/dd/yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss')"
            bIsFieldClause = True
        End If

        If (Not (expiration Is String.Empty)) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "(" & Me.COL_NAME_EXPIRATION & ")" & " >  to_date('" & DateHelper.GetDateValue(expiration).ToString("MM/dd/yyyy HH:mm:ss") & "', 'mm-dd-yyyy hh24:mi:ss')"
            bIsWhereClause = True
        End If

        If bIsFieldClause Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_FIELD_SELECTOR_PLACE_HOLDER, dynamicFieldClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_FIELD_SELECTOR_PLACE_HOLDER, "")
        End If

        If bIsWhereClause Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            EquipmentListIdParam = New DBHelper.DBHelperParameter(Me.COL_NAME_EQUIPMENT_LIST_ID, equipmentListId)
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {EquipmentListIdParam})
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        Return False
    End Function

    Public Function IsEquipmentInList(ByVal equipmentId As Guid, ByVal equipmentListId As Guid, ByVal companyIds As ArrayList, ByVal languageId As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/IS_EQUIPMENT")
        Dim ds As New DataSet
        Dim EquipmentIdParam As DBHelper.DBHelperParameter
        Dim EquipmentListIdParam As DBHelper.DBHelperParameter

        Try
            EquipmentIdParam = New DBHelper.DBHelperParameter("EQUIPMENT_ID", equipmentId)
            EquipmentListIdParam = New DBHelper.DBHelperParameter(Me.COL_NAME_EQUIPMENT_LIST_ID, equipmentListId)
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {EquipmentIdParam, EquipmentListIdParam})
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        Return ds
    End Function

    Public Function InsertEquipment(ByVal equipmentId As Guid, ByVal companyIds As ArrayList, ByVal languageId As Guid) As Boolean

        Dim selectStmt As String = Me.Config("/SQL/INSERT_EQUIPMENT")
        Dim ds As New DataSet
        Dim EquipmentIdParam As DBHelper.DBHelperParameter

        Try
            EquipmentIdParam = New DBHelper.DBHelperParameter(Me.COL_NAME_EQUIPMENT_LIST_ID, equipmentId)
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {EquipmentIdParam})
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        Return False
    End Function

    Private Function IsThereALikeClause(ByVal description As String, ByVal code As String) As Boolean
        Dim bIsLikeClause As Boolean

        bIsLikeClause = Me.IsLikeClause(description) OrElse Me.IsLikeClause(code)

        Return bIsLikeClause
    End Function

    Public Function IsListToDealer(ByVal ListCode As String, ByVal EquipmentListID As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/IS_LIST_ASSIGNED_TO_DEALER")
        Dim EquipmentListDAL As New EquipmentListDAL
        Dim EquipmentListIdParam As DBHelper.DBHelperParameter
        Dim ds As New DataSet

        Try
            EquipmentListIdParam = New DBHelper.DBHelperParameter(COL_NAME_EQUIPMENT_LIST_ID, EquipmentListID.ToByteArray)
            selectStmt &= " AND UPPER(" & COL_NAME_DEALER_EQUIPMENT_LIST_CODE & ") = '" & ListCode.ToUpper & "'"
            Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_DEALER, New DBHelper.DBHelperParameter() {EquipmentListIdParam})
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetCurrentDateTime() As DateTime

        Dim selectStmt As String = Me.Config("/SQL/CURRENT_TIME_STAMP")
        Dim whereClauseConditions As String = String.Empty
        Dim ds As New DataSet
        Dim EquipmentIdParam As DBHelper.DBHelperParameter

        Try
            Return (DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, New DBHelper.DBHelperParameter() {}).Tables(0).Rows(0)("SYSDATE"))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region

#Region "Overloaded Methods"

    'This method was added manually to accommodate BO families Save
    Public Overloads Sub UpdateFamily(ByVal familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)
        Dim EquipmentListDAL As New EquipmentListDAL
        Dim EquipmentListDetailDAL As New EquipmentListDetailDAL
        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            'First Pass - Delete
            EquipmentListDetailDAL.Update(familyDataset.GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)
            MyBase.Update(familyDataset.Tables(Me.TABLE_NAME).GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)

            'Second Pass - Add new List
            Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)
            EquipmentListDetailDAL.Update(familyDataset.GetChanges(DataRowState.Added Or DataRowState.Modified), tr, DataRowState.Added Or DataRowState.Modified)

            'At the end delete the Address
            If Transaction Is Nothing Then
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

    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
    End Sub
#End Region

#Region "Public Method"
    Public Function GetSelectedEquipmentList(ByVal EquipmentListID As Guid) As DataSet
        Dim ds As New DataSet
        Dim selectStmt As String = Me.Config("/SQL/GetSelectedEquipmentList")

        Dim params() As DBHelper.DBHelperParameter = {New DBHelper.DBHelperParameter(COL_NAME_EQUIPMENT_LIST_ID, EquipmentListID.ToByteArray)}

        Try
            Return DBHelper.Fetch(ds, selectStmt, EquipmentDAL.TABLE_NAME, params)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function
#End Region

End Class


