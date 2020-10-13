Public Class EquipmentDAL
    Inherits DALBase

#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_EQUIPMENT"
    Public Const TABLE_KEY_NAME As String = "equipment_id"

    Public Const COL_NAME_EQUIPMENT_ID As String = "equipment_id"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_MODEL As String = "model"
    Public Const COL_NAME_MASTER_EQUIPMENT_ID As String = "master_equipment_id"
    Public Const COL_NAME_REPAIRABLE_ID As String = "repairable_id"
    Public Const COL_NAME_MANUFACTURER_ID As String = "manufacturer_id"
    Public Const COL_NAME_EQUIPMENT_CLASS_ID As String = "equipment_class_id"
    Public Const COL_NAME_EQUIPMENT_TYPE_ID As String = "equipment_type_id"
    Public Const COL_NAME_EFFECTIVE As String = "effective"
    Public Const COL_NAME_EXPIRATION As String = "expiration"
    Public Const COL_NAME_IS_MASTER_EQUIPMENT As String = "is_master_equipment"

    Public Const COL_NAME_MANUFACTURER As String = "manufacturer"
    Public Const COL_NAME_EQUIPMENT_CLASS As String = "equipment_class"
    Public Const COL_NAME_EQUIPMENT_TYPE As String = "equipment_type"
    Public Const COL_NAME_SKU As String = "sku"


    Public Const COL_NAME_COLOR_XCD_ID As String = "color_xcd"
    Public Const COL_NAME_MEMORY_XCD_ID As String = "memory_xcd"
    Public Const COL_NAME_CARRIER_XCD_ID As String = "carrier_xcd"
   


    Public Const PAR_NAME_LOOKUP_DATE As String = "lookup_date"
    Public Const PAR_NAME_DEALER As String = "dealer"
    Public Const PAR_NAME_MANUFACTURER As String = "manufacturer"
    Public Const PAR_NAME_COMPANY_GROUP_ID As String = "company_group_id"
    Public Const PAR_NAME_EQUIPMENT_LIST_CODE As String = "equipment_list_code"
    Public Const PAR_NAME_EFFECTIVE_DATE As String = "effective_date"
    Public Const PAR_NAME_MANUFACTURER_ID As String = "manufacturerid"
    Public Const PAR_NAME_MODEL As String = "model"

    Private Const const_AND As String = " AND "

    Public Const COL_NAME_COMPANY_GROUP_ID = "company_group_id"
    Private Const DSNAME As String = "GetMakesAndModelResponse"
    Public Const TABLE_NAME_MAKES_AND_MODEL_INFO As String = "MakesAndModelInformation"
    Public Const COL_NAME_EQUIPMENT_CODE As String = "equipment_code"
    Public Const DATASET_NAME__GET_MAKE_AND_MODEL_RESPONSE As String = "GetMakesAndModelResponse"

#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.New()
    End Sub
#End Region

#Region "Load Methods"
    Public Sub LoadSchema(ByRef ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(familyDS As DataSet, id As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_EQUIPMENT_ID, id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(description As String, model As String,
                                        manufacturerName As String, equipmentClassName As String,
                                        equipmentTypeName As String,
                                        companyIds As ArrayList, languageId As Guid, sku As String, Optional ByVal EquipmentListCode As String = "",
                                        Optional ByVal Effective_On_Date As Date = Nothing) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        If Not String.IsNullOrEmpty(EquipmentListCode) Then
            selectStmt = Config("/SQL/LOAD_LIST_BY_EQUIPMENT_LIST")
        End If

        Dim whereClauseConditions As String = ""
        Dim ds As New DataSet
        Dim languageIdParam As DBHelper.DBHelperParameter
        Dim bIsLikeClause As Boolean = False

        bIsLikeClause = IsThereALikeClause(description, model,
                                manufacturerName, sku)

        If ((Not (description Is Nothing)) AndAlso (FormatSearchMask(description))) Then
            whereClauseConditions &= Environment.NewLine & "UPPER(E." & COL_NAME_DESCRIPTION & ")" & description.ToUpper & " AND"
        End If

        If ((Not (manufacturerName Is Nothing)) AndAlso (FormatSearchMask(manufacturerName))) Then
            whereClauseConditions &= Environment.NewLine & "UPPER(M." & ManufacturerDAL.COL_NAME_DESCRIPTION & ")" & manufacturerName.ToUpper & " AND"
        End If

        If ((Not (model Is Nothing)) AndAlso (FormatSearchMask(model))) Then
            If (model.Contains(EQUALS_CLAUSE)) Then
                model = EQUALS_CLAUSE & " GetTextForComparison(" & model.Replace(EQUALS_CLAUSE, String.Empty) & ")"
            End If
            whereClauseConditions &= Environment.NewLine & "GetTextForComparison(E." & COL_NAME_MODEL & ")" & model.ToUpper & " AND"
        End If

        If ((Not (equipmentClassName Is Nothing)) AndAlso (FormatSearchMask(equipmentClassName))) Then
            whereClauseConditions &= Environment.NewLine & "UPPER(DITEC.TRANSLATION)" & equipmentClassName.ToUpper & " AND"
        End If

        If ((Not (equipmentTypeName Is Nothing)) AndAlso (FormatSearchMask(equipmentTypeName))) Then
            whereClauseConditions &= Environment.NewLine & "UPPER(DITET.TRANSLATION)" & equipmentTypeName.ToUpper & " AND"
        End If

        If ((Not (sku Is Nothing)) AndAlso (FormatSearchMask(sku))) Then
            whereClauseConditions &= Environment.NewLine & "UPPER(EWM.SKU_NUMBER)" & sku.ToUpper & " AND"
        End If

        whereClauseConditions &= Environment.NewLine & MiscUtil.BuildListForSql("M." & ManufacturerDAL.COL_NAME_COMPANY_GROUP_ID, companyIds, True) & " AND"

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            If String.IsNullOrEmpty(EquipmentListCode) Then
                DBHelper.Fetch(ds, selectStmt, TABLE_NAME,
                                New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray())})
            Else
                DBHelper.Fetch(ds, selectStmt, TABLE_NAME,
                                New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray()),
                                                                  New DBHelper.DBHelperParameter("EQUIP_LIST_CODE", EquipmentListCode),
                                                                  New DBHelper.DBHelperParameter("EFF_DATE", Effective_On_Date)})
            End If
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
    Public Function LoadEquipmentForBenefitsList(companyIds As ArrayList, Effective_On_Date As Date, languageId As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_EQUIPMENT_FOR_BENEFITS_LIST")

        Dim whereClauseConditions As String = ""
        Dim ds As New DataSet
        Dim languageIdParam As DBHelper.DBHelperParameter
        Dim bIsLikeClause As Boolean = False


        whereClauseConditions &= Environment.NewLine & MiscUtil.BuildListForSql("M." & ManufacturerDAL.COL_NAME_COMPANY_GROUP_ID, companyIds, True) & " AND"

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME,
                            New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("EFF_DATE", Effective_On_Date)})

            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
    Private Function IsThereALikeClause(description As String, model As String,
                                manufacturerName As String, SKU As String) As Boolean
        Dim bIsLikeClause As Boolean

        bIsLikeClause = IsLikeClause(description) OrElse IsLikeClause(model) OrElse
                            IsLikeClause(manufacturerName) OrElse IsLikeClause(SKU)
        Return bIsLikeClause
    End Function

    Public Function LoadEquipmentListForWS(EquipmentListCode As String, companyGroupId As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_EQUIPMENT_LIST_FOR_WS")
        Dim parameters() As OracleParameter

        parameters = New OracleParameter() _
                                    {New OracleParameter(COL_NAME_EQUIPMENT_CODE, EquipmentListCode)}
        Try
            Return (DBHelper.Fetch(selectStmt, DATASET_NAME__GET_MAKE_AND_MODEL_RESPONSE, TABLE_NAME_MAKES_AND_MODEL_INFO, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function
#End Region

#Region "Overloaded Methods"
    'This method was added manually to accommodate BO families Save
    Public Overloads Sub UpdateFamily(familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)
        Dim commentDAL As New EquipmentCommentDAL
        Dim imageDAL As New EquipmentImageDAL
        Dim attrbValDAL As New AttributeValueDAL
        Dim mfgCvgDAL As New MfgCoverageDAL
        Dim RelequipDAL As New RelatedEquipmentDAL

        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            'First Pass updates Deletions
            commentDAL.Update(familyDataset.GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)
            attrbValDAL.Update(familyDataset.GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)
            imageDAL.Update(familyDataset.GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)
            mfgCvgDAL.Update(familyDataset.GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)
            RelequipDAL.Update(familyDataset.GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)
            MyBase.Update(familyDataset.Tables(TABLE_NAME).GetChanges(DataRowState.Deleted), tr, DataRowState.Deleted)

            'Second Pass updates additions and changes
            Update(familyDataset.Tables(TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified)
            commentDAL.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)
            attrbValDAL.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)
            imageDAL.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)
            mfgCvgDAL.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)
            RelequipDAL.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)

            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                DBHelper.Commit(tr)
            End If
        Catch ex As Exception
            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                DBHelper.RollBack(tr)
            End If
            Throw ex
        End Try
    End Sub

    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds.Tables(TABLE_NAME) IsNot Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region

#Region "Public Methods"

    Public Function FindEquipment(dealer As String, manufacturer As String, model As String, lookupDate As Date) As Guid
        Dim selectStmt As String = Config("/SQL/FIND_EQUIPMENT_BY_MAKE_MODEL_DEALER")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                                                         {New DBHelper.DBHelperParameter(PAR_NAME_LOOKUP_DATE, lookupDate, GetType(Date)),
                                                          New DBHelper.DBHelperParameter(PAR_NAME_DEALER, dealer, GetType(String)),
                                                         New DBHelper.DBHelperParameter(PAR_NAME_MANUFACTURER, manufacturer, GetType(String)),
                                                         New DBHelper.DBHelperParameter(PAR_NAME_MANUFACTURER, manufacturer, GetType(String)),
                                                         New DBHelper.DBHelperParameter(COL_NAME_MODEL, model, GetType(String))}
        Try
            Dim obj As Object
            obj = DBHelper.ExecuteScalar(selectStmt, parameters)
            If (obj IsNot Nothing) Then
                Return New Guid(CType(obj, Byte()))
            End If

            Return Guid.Empty

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetEquipmentClassIdByEquipmentId(equipmentId As Guid) As Guid
        Dim selectStmt As String = Config("/SQL/GET_EQUIPMENT_CLASS_ID_BY_EQUIPMENT_ID")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                                                         {New DBHelper.DBHelperParameter(TABLE_KEY_NAME, equipmentId, GetType(Guid))}
        Try
            Dim classId As Object
            classId = DBHelper.ExecuteScalar(selectStmt, parameters)
            If (classId IsNot Nothing) Then
                Return New Guid(CType(classId, Byte()))
            End If

            Return Guid.Empty

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function ExecuteEquipmentFilter(CompGrpid As Guid, makeid As Guid, equipmentClass As Guid, equipmentType As Guid, Model As String, Description As String, parentEquipmenttype As Guid) As DataSet

        Dim ds As New DataSet
        Dim selectStmt As String = Config("/SQL/SEARCH_Equipment_List_detail")
        Dim dynamic_Where_Clause As String = String.Empty
        Dim COL_PARENT_EQUIPMENT_TYPE As String = "PARENT_EQUIPMENT_TYPE"
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(ManufacturerDAL.COL_NAME_COMPANY_GROUP_ID, CompGrpid.ToByteArray),
                                                                                            New DBHelper.DBHelperParameter(COL_PARENT_EQUIPMENT_TYPE, parentEquipmenttype.ToByteArray)}

        If Not makeid.Equals(Guid.Empty) Then
            dynamic_Where_Clause &= const_AND & "EM."
            dynamic_Where_Clause &= COL_NAME_MANUFACTURER_ID.ToUpper & " =  :" & COL_NAME_MANUFACTURER_ID
            'DEF-3186
            ReDim Preserve parameters(parameters.Count)
            'End of DEF-3186
            parameters(parameters.Count - 1) = New DBHelper.DBHelperParameter(COL_NAME_MASTER_EQUIPMENT_ID, makeid.ToByteArray)
        End If
        If Not equipmentClass.Equals(Guid.Empty) Then
            dynamic_Where_Clause &= const_AND & "EQ."
            dynamic_Where_Clause &= COL_NAME_EQUIPMENT_CLASS_ID.ToUpper & " = " & MiscUtil.GetDbStringFromGuid(equipmentClass)
        End If
        If Not equipmentType.Equals(Guid.Empty) Then
            dynamic_Where_Clause &= const_AND & "EQ."
            dynamic_Where_Clause &= COL_NAME_EQUIPMENT_TYPE_ID.ToUpper & " = " & MiscUtil.GetDbStringFromGuid(equipmentType)
        End If
        If Not String.IsNullOrEmpty(Model) Then
            dynamic_Where_Clause &= const_AND & "EQ."
            dynamic_Where_Clause &= COL_NAME_MODEL.ToUpper & " LIKE '" & Model & "'"
        End If
        If Not String.IsNullOrEmpty(Description) Then
            dynamic_Where_Clause &= const_AND & "EQ."
            dynamic_Where_Clause &= COL_NAME_DESCRIPTION.ToUpper & " LIKE '" & Description & "'"
        End If

        selectStmt &= dynamic_Where_Clause

        Try

            Return DBHelper.Fetch(ds, selectStmt, EquipmentDAL.TABLE_NAME, parameters)

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetEquipmentIdByEquipmentList(equipmentListCode As String,
                                                  effectiveDate As DateTime, manufacturer_id As Guid, model As String) As Guid

        Dim selectStmt As String = Config("/SQL/GET_EQUIPMENT_ID_BY_EQUIPMENTLIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                                                         {New DBHelper.DBHelperParameter(PAR_NAME_EQUIPMENT_LIST_CODE, equipmentListCode, GetType(String)),
                                                          New DBHelper.DBHelperParameter(PAR_NAME_EFFECTIVE_DATE, effectiveDate, GetType(DateTime)),
                                                          New DBHelper.DBHelperParameter(PAR_NAME_EFFECTIVE_DATE, effectiveDate, GetType(DateTime)),
                                                          New DBHelper.DBHelperParameter(PAR_NAME_EFFECTIVE_DATE, effectiveDate, GetType(DateTime)),
                                                          New DBHelper.DBHelperParameter(PAR_NAME_MANUFACTURER_ID, manufacturer_id, GetType(Guid)),
                                                          New DBHelper.DBHelperParameter(COL_NAME_MODEL, model, GetType(String))}
        Try
            Dim obj As Object
            obj = DBHelper.ExecuteScalar(selectStmt, parameters)
            If (obj IsNot Nothing) Then
                Return New Guid(CType(obj, Byte()))
            End If

            Return Guid.Empty

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region

End Class
