'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (3/7/2008)********************


Public Class MfgCoverageDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_MFG_COVERAGE"
    Public Const TABLE_KEY_NAME As String = "mfg_coverage_id"

    Public Const COL_NAME_MFG_COVERAGE_ID As String = "mfg_coverage_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_MANUFACTURER_ID As String = "manufacturer_id"
    Public Const COL_NAME_MODEL As String = "model"
    Public Const COL_NAME_RISK_TYPE_ID As String = "risk_type_id"
    Public Const COL_NAME_MFG_WARRANTY As String = "mfg_warranty"
    Public Const COL_NAME_COMPANY_GROUP_ID = "company_group_id"
    Public Const COL_NAME_DEALER_NAME = "dealer_name"
    Public Const COL_DEDUCTIBLE = "Deductible"
    Public Const COL_EQUIPMENT_TYPE_ID = "equipment_type_id"
    Public Const COL_EQUIPMENT_ID = "equipment_id"
    Public Const COL_NAME_MFG_MAIN_PARTS_WARRANTY = "mfg_main_parts_warranty"

    Public Const COL_NAME_LANGUAGE_ID As String = "language_id"

    Private Const DSNAME As String = "LIST"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("mfg_coverage_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub LoadByEquipmentId(ByVal familyDS As DataSet, ByVal equipmentId As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD_BY_EQUIPMENT_ID")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_EQUIPMENT_ID, equipmentId.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(ByVal manufacturerId As Guid, ByVal CompanyGroupId As Guid, ByVal riskType As Guid, ByVal model As String, ByVal languageId As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = ""


        Dim parameters() As OracleParameter

        If Not Me.IsNothing(manufacturerId) Then
            whereClauseConditions &= " AND mfgc.manufacturer_id = '" & Me.GuidToSQLString(manufacturerId) & "'"
        End If

        If Not Me.IsNothing(riskType) Then
            whereClauseConditions &= " AND mfgc.risk_type_Id = '" & Me.GuidToSQLString(riskType) & "'"
        End If



        If Not Me.IsNothing(model) AndAlso Me.FormatSearchMask(model) Then
            whereClauseConditions &= Environment.NewLine & " AND " & "UPPER(mfgc.model)" & model.ToUpper
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        parameters = New OracleParameter() {New OracleParameter(COL_NAME_LANGUAGE_ID, languageId.ToByteArray()), New OracleParameter(COL_NAME_COMPANY_GROUP_ID, CompanyGroupId.ToByteArray)}

        Try
            Return (DBHelper.Fetch(selectStmt, DSNAME, Me.TABLE_NAME, parameters))
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
#End Region


End Class


