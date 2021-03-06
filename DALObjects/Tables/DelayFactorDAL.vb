﻿'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (5/7/2008)********************


Public Class DelayFactorDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_DELAY_FACTOR"
    Public Const TABLE_KEY_NAME As String = "delay_factor_id"

    Public Const COL_NAME_DELAY_FACTOR_ID As String = "delay_factor_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_EFFECTIVE_DATE As String = "effective_date"
    Public Const COL_NAME_EXPIRATION_DATE As String = "expiration_date"
    Public Const COL_NAME_LOW_NUMBER_OF_DAYS As String = "low_number_of_days"
    Public Const COL_NAME_HIGH_NUMBER_OF_DAYS As String = "high_number_of_days"
    Public Const COL_NAME_FACTOR As String = "factor"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("delay_factor_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList() As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
    End Function

    Public Function LoadList(ByVal dealerId As Guid, ByVal effective As Date, ByVal expiration As Date) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim parameters() As OracleParameter

        parameters = New OracleParameter() _
                                    {New OracleParameter(COL_NAME_DEALER_ID, dealerId.ToByteArray), _
                                     New OracleParameter(COL_NAME_EFFECTIVE_DATE, effective), _
                                     New OracleParameter(COL_NAME_EXPIRATION_DATE, expiration)}

        Try
            Return (DBHelper.Fetch(selectStmt, Me.TABLE_NAME, Me.TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadListByDealer(ByVal dealerId As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST_BY_DEALER")
        Dim parameters() As OracleParameter

        parameters = New OracleParameter() {New OracleParameter(COL_NAME_DEALER_ID, dealerId.ToByteArray)}

        Try
            Return (DBHelper.Fetch(selectStmt, Me.TABLE_NAME, Me.TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetDelayFactorByDealer(ByVal dealerId As Guid, ByVal compId As ArrayList) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_DELAY_FACTOR_BY_DEALER")
        Dim whereClauseConditions As String = ""

        Try
            Dim ds As New DataSet
            Dim parameter As DBHelper.DBHelperParameter

            If Not dealerId.Equals(Guid.Empty) Then
                whereClauseConditions &= Environment.NewLine & "AND " & "f.dealer_id = " & MiscUtil.GetDbStringFromGuid(dealerId)
            End If

            whereClauseConditions &= Environment.NewLine & " AND " & Environment.NewLine & MiscUtil.BuildListForSql(DealerDAL.COL_NAME_COMPANY_ID, compId)

            If Not whereClauseConditions = "" Then
                selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
            Else
                selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
            End If

            selectStmt = selectStmt.Replace(Me.DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, _
                                    Environment.NewLine & "ORDER BY " & Environment.NewLine & DelayFactorDAL.COL_NAME_EFFECTIVE_DATE & " DESC")

            ds = DBHelper.Fetch(selectStmt, Me.TABLE_NAME)

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
#End Region


End Class


