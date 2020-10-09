'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (3/5/2012)********************


Public Class ServiceLevelDetailDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_SERVICE_LEVEL_DETAIL"
    Public Const TABLE_KEY_NAME As String = "service_level_detail_id"

    Public Const COL_NAME_SERVICE_LEVEL_DETAIL_ID As String = "service_level_detail_id"
    Public Const COL_NAME_SERVICE_LEVEL_GROUP_ID As String = "service_level_group_id"
    Public Const COL_NAME_CODE As String = "code"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_RISK_TYPE_ID As String = "risk_type_id"
    Public Const COL_NAME_COST_TYPE_ID As String = "cost_type_id"
    Public Const COL_NAME_SERVICE_LEVEL_COST As String = "service_level_cost"
    Public Const COL_NAME_EFFECTIVE_DATE As String = "effective_date"
    Public Const COL_NAME_EXPIRATION_DATE As String = "expiration_date"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("service_level_detail_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(ServiceLevelGroupId As Guid, svcLevelCodeMask As String, svcLevelDescMask As String, langId As Guid, sDate As String) As DataSet
        Dim ds As New DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("language_id", langId.ToByteArray)}
        Dim whereClauseConditions As String = ""

        If Not IsNothing(ServiceLevelGroupId) Then
            whereClauseConditions &= " where sld.service_level_group_id = '" & GuidToSQLString(ServiceLevelGroupId) & "'"
        End If

        If ((Not svcLevelCodeMask Is Nothing) AndAlso (FormatSearchMask(svcLevelCodeMask))) Then
            whereClauseConditions &= Environment.NewLine & "AND UPPER(sld.code)" & svcLevelCodeMask.ToUpper
        End If

        If ((Not svcLevelDescMask Is Nothing) AndAlso (FormatSearchMask(svcLevelDescMask))) Then
            whereClauseConditions &= Environment.NewLine & "AND UPPER(sld.description)" & svcLevelDescMask.ToUpper
        End If

        If (Not (sDate.Equals(String.Empty))) Then
            whereClauseConditions &= Environment.NewLine & "AND  to_Date(" & sDate & ", 'YYYYMMDD') >=  sld.effective_date" '  and sld.expiration_date"
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Return DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
    End Function

    Public Sub Delete(serviceLevelDetailId As Guid)
        Try
            Dim deleteStatement As String = Config("/SQL/DELETE")
            Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_SERVICE_LEVEL_DETAIL_ID, serviceLevelDetailId.ToByteArray)}
            DBHelper.Execute(deleteStatement, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function IsServiceLevelDetailValid(slgId As Guid, sCode As String, riskypeId As Guid, costTypeId As Guid, effectiveDate As Date) As DataSet
        Dim ds As New DataSet
        Dim selectStmt As String = Config("/SQL/VALIDATE_SLD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                                {New DBHelper.DBHelperParameter("service_level_group_id", slgId.ToByteArray), _
                                    New DBHelper.DBHelperParameter("code", sCode), _
                                    New DBHelper.DBHelperParameter("risk_type_id", riskypeId.ToByteArray), _
                                 New DBHelper.DBHelperParameter("cost_type_id", costTypeId.ToByteArray), _
                                  New DBHelper.DBHelperParameter("effective_date", effectiveDate)}
        Try
            Return DBHelper.Fetch(ds, selectStmt, "VALID_SLD", parameters)
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
        If Not ds.Tables(TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region


End Class


