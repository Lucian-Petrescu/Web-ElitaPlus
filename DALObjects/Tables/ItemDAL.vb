'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (8/3/2006)********************


Public Class ItemDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_ITEM"
    Public Const TABLE_KEY_NAME As String = "item_id"

    Public Const COL_NAME_ITEM_ID As String = "item_id"
    Public Const COL_NAME_PRODUCT_CODE_ID As String = "product_code_id"
    Public Const COL_NAME_RISK_TYPE_ID As String = "risk_type_id"
    Public Const COL_NAME_MAX_REPLACEMENT_COST As String = "max_replacement_cost"
    Public Const COL_NAME_ITEM_NUMBER As String = "item_number"
    Public Const COL_NAME_OPTIONAL_ITEM As String = "optional_item"
    Public Const COL_NAME_OPTIONAL_ITEM_CODE As String = "optional_item_code"

    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_DEALER_NAME As String = "dealer_name"
    Public Const COL_NAME_PRODUCT_CODE As String = "product_code"
    Public Const COL_NAME_RISK_TYPE As String = "risk_type"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_INUSEFLAG As String = "inuseflag"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("item_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(ByVal compIds As ArrayList, ByVal dealerId As Guid, _
                         ByVal productCodeId As Guid, ByVal riskTypeId As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim parameters() As DBHelper.DBHelperParameter
        Dim inClauseCondition As String
        Dim whereClauseConditions As String = ""
        Dim ds As New DataSet

        inClauseCondition &= " AND edealer." & MiscUtil.BuildListForSql(Me.COL_NAME_COMPANY_ID, compIds, True)

        If Not dealerId.Equals(Guid.Empty) Then
            whereClauseConditions &= Environment.NewLine & "AND " & "EDEALER.DEALER_ID = " & MiscUtil.GetDbStringFromGuid(dealerId)
        End If

        If Not productCodeId.Equals(Guid.Empty) Then
            whereClauseConditions &= Environment.NewLine & "AND " & "PC.PRODUCT_CODE_ID = " & MiscUtil.GetDbStringFromGuid(productCodeId)
        End If

        If Not riskTypeId.Equals(Guid.Empty) Then
            whereClauseConditions &= Environment.NewLine & "AND " & "RISK.RISK_TYPE_ID = " & MiscUtil.GetDbStringFromGuid(riskTypeId)
        End If

        Try

            If Not inClauseCondition = "" Then
                selectStmt = selectStmt.Replace(Me.DYNAMIC_IN_CLAUSE_PLACE_HOLDER, inClauseCondition)
            Else
                selectStmt = selectStmt.Replace(Me.DYNAMIC_IN_CLAUSE_PLACE_HOLDER, "")
            End If

            If Not whereClauseConditions = "" Then
                selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
            Else
                selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")

            End If
            selectStmt = selectStmt.Replace(Me.DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, _
                                    Environment.NewLine & "ORDER BY " & Environment.NewLine & _
                                    "UPPER(" & Me.COL_NAME_DEALER_NAME & "), UPPER(" & _
                                    Me.COL_NAME_PRODUCT_CODE & "), " & Me.COL_NAME_ITEM_NUMBER & _
                                    ", UPPER(" & Me.COL_NAME_RISK_TYPE & ") DESC")
            '			ORDER BY UPPER(DEALER_NAME), UPPER(PRODUCT_CODE), ITEM_NUMBER, UPPER(RISK_TYPE)

            parameters = New DBHelper.DBHelperParameter() _
                                        {New DBHelper.DBHelperParameter(Me.PAR_NAME_ROW_NUMBER, Me.MAX_NUMBER_OF_ROWS)}

            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function getDealerId(ByVal id As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/GET_DEALER_ID")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("item_id", id.ToByteArray)}
        Dim ds As New DataSet
        Try
            Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadDealerItemsInfo(ByRef ds As DataSet, ByVal dealerId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_DEALER_ITMES_INFO_FOR_WS")
        Dim parameters() As DBHelper.DBHelperParameter
        Dim whereClauseConditions As String = ""
        Dim OrderByClause As String = ""

        parameters = New DBHelper.DBHelperParameter() _
                    {New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID, dealerId.ToByteArray)}

        If ds Is Nothing Then ds = New DataSet
        Try
            DBHelper.Fetch(ds, selectStmt, "ITEMS", parameters)

            Return ds
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

    Public Function ProductCodeExists(ByVal product_code_id As Guid, ByVal risk_type_id As Guid, ByVal item_number As Long) As Boolean
        Dim selectStmt As String = Me.Config("/SQL/ITEM_UNIQUE")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                                    New DBHelper.DBHelperParameter("product_code_id", product_code_id.ToByteArray), _
                                    New DBHelper.DBHelperParameter("risk_type_id", risk_type_id.ToByteArray), _
                                    New DBHelper.DBHelperParameter("item_number", item_number)}
        Dim ds As New DataSet
        Try
            Dim bExists As Boolean = True
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            If CType(ds.Tables(0).Rows(0).Item(0), Integer) = 0 Then
                bExists = False
            End If
            Return bExists
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function OptionalItemCodeExists(ByVal product_code_id As Guid, ByVal item_number As Long, ByVal OptionalItemCode As String) As Boolean
        Dim selectStmt As String = Me.Config("/SQL/OPTIONAL_ITEM_UNIQUE")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                                    New DBHelper.DBHelperParameter("product_code_id", product_code_id.ToByteArray), _
                                    New DBHelper.DBHelperParameter("item_number", item_number), _
                                    New DBHelper.DBHelperParameter("optional_item_code", OptionalItemCode)}
        Dim ds As New DataSet
        Try
            Dim bExists As Boolean = True
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            If CType(ds.Tables(0).Rows(0).Item(0), Integer) = 0 Then
                bExists = False
            End If
            Return bExists
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function


#End Region

End Class


