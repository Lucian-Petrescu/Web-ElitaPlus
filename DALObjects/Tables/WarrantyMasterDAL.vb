'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (8/4/2008)********************


Public Class WarrantyMasterDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_WARRANTY_MASTER"
    Public Const TABLE_KEY_NAME As String = "warranty_master_id"

    Public Const COL_NAME_WARRANTY_MASTER_ID As String = "warranty_master_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_SKU_NUMBER As String = "sku_number"
    Public Const COL_NAME_SKU_DESCRIPTION As String = "sku_description"
    Public Const COL_NAME_MANUFACTURER_ID As String = "manufacturer_id"
    Public Const COL_NAME_MANUFACTURER_NAME As String = "manufacturer_name"
    Public Const COL_NAME_WARRANTY_TYPE As String = "warranty_type"
    Public Const COL_NAME_WARRANTY_DESCRIPTION As String = "warranty_description"
    Public Const COL_NAME_MODEL_NUMBER As String = "model_number"
    Public Const COL_NAME_WARRANTY_DURATION_PARTS As String = "warranty_duration_parts"
    Public Const COL_NAME_WARRANTY_DURATION_LABOR As String = "warranty_duration_labor"
    Public Const COL_NAME_IS_DELETED As String = "is_deleted"
    Public Const COL_NAME_COMPANY_ID = "company_id"
    Public Const COL_NAME_RISK_TYPE_ID As String = "risk_type_id"

    Public Const WILDCARD As Char = "%"
    Public Const ELITA_WILDCARD As Char = "*"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("warranty_master_id", id.ToByteArray)}
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

    Public Function LoadList(compIds As ArrayList, dealerId As Guid, skuNumber As String, manufacturerName As String, modelNumber As String, warrantyType As String) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim parameters() As OracleParameter
        Dim inClausecondition As String = ""
        Dim whereClauseConditions As String = ""

        inClausecondition &= "And edealer." & MiscUtil.BuildListForSql(COL_NAME_COMPANY_ID, compIds, False)

        If Not dealerId.Equals(Guid.Empty) Then
            whereClauseConditions &= Environment.NewLine & "AND " & "edealer.DEALER_ID = " & MiscUtil.GetDbStringFromGuid(dealerId)
        End If

        If warrantyType.Trim <> String.Empty Then
            whereClauseConditions &= Environment.NewLine & " AND " & " WARRANTY_TYPE = '" & warrantyType.Trim & "'"
        End If

        selectStmt = selectStmt.Replace(DYNAMIC_IN_CLAUSE_PLACE_HOLDER, inClausecondition)
        selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)

        If skuNumber.Equals("") Then skuNumber = WILDCARD
        If skuNumber.Contains(ELITA_WILDCARD) Then skuNumber = skuNumber.Replace(ELITA_WILDCARD, WILDCARD)
        If manufacturerName.Equals("") Then manufacturerName = WILDCARD
        If manufacturerName.Contains(ELITA_WILDCARD) Then manufacturerName = manufacturerName.Replace(ELITA_WILDCARD, WILDCARD)
        If modelNumber.Equals("") Then modelNumber = WILDCARD
        If modelNumber.Contains(ELITA_WILDCARD) Then modelNumber = modelNumber.Replace(ELITA_WILDCARD, WILDCARD)

        Dim ds As New DataSet
        Try
            Dim parameter As DBHelper.DBHelperParameter
            parameters = New OracleParameter() _
                                    {New OracleParameter(WarrantyMasterDAL.COL_NAME_SKU_NUMBER, skuNumber), _
                                     New OracleParameter(WarrantyMasterDAL.COL_NAME_MANUFACTURER_NAME, manufacturerName), _
                                     New OracleParameter(WarrantyMasterDAL.COL_NAME_MODEL_NUMBER, modelNumber)}
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetMakeAndModelForDealerFromWM(SkuNumber As String, DealerID As Guid) As DataSet

        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Config("/SQL/WS_GET_MAKE_MODEL_FROM_WARRANTY_MASTER_FOR_DEALER")

        parameters = New OracleParameter() {New OracleParameter(COL_NAME_SKU_NUMBER, SkuNumber), _
                                            New OracleParameter(COL_NAME_DEALER_ID, DealerID.ToByteArray)}
        Return DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
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

