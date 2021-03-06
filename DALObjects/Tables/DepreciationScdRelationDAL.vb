﻿'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (10/9/2017)********************


Public Class DepreciationScdRelationDal
    Inherits DALBase


#Region "Constants"
    Public Const TableName As String = "ELP_DEPRECIATION_SCD_RELATION"
    Public Const TableKeyName As String = "depreciation_scd_relation_id"

    Public Const ColNameDepreciationScdRelationId As String = "depreciation_scd_relation_id"
    Public Const ColNameDepreciationScheduleId As String = "depreciation_schedule_id"
    Public Const ColNameDepreciationScheduleCode As String = "DepreciationScheduleCode"
    Public Const ColNameTableReference As String = "table_reference"
    Public Const ColNameTableReferenceId As String = "table_reference_id"
    Public Const ColNameEffectiveDate As String = "effective_date"
    Public Const ColNameExpirationDate As String = "expiration_date"
    Public Const ColNameDepreciationSchUsageXcd As String = "depreciation_sch_usage_xcd"
    Public Const ColNameDepreciationSchUsage As String = "depreciation_sch_usage"

#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.New()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ByVal ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(ByVal familyDs As DataSet, ByVal id As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("depreciation_scd_relation_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDs, selectStmt, TableName, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(ByVal referenceId As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim ds As New DataSet
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(ColNameTableReferenceId, referenceId.ToByteArray)}
        Try
            DBHelper.Fetch(ds, selectStmt, TableName, parameters)
            Return (ds)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function
    Public Sub LoadList(ByVal referenceId As Guid, ByVal familyDs As DataSet)
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(ColNameTableReferenceId, referenceId.ToByteArray)}
        Try
            DBHelper.Fetch(familyDs, selectStmt, TableName, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub


#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(TableName) Is Nothing Then
            Update(ds.Tables(TableName), transaction, changesFilter)
        End If
    End Sub
#End Region


End Class


