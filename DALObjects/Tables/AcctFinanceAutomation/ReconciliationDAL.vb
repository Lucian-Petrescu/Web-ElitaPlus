'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (1/7/2015)********************


Public Class ReconciliationDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_AFA_ENROLL_RECON_DISCREP"
    Public Const TABLE_KEY_NAME As String = "afa_enroll_recon_discrep_id"

    Public Const COL_NAME_AFA_ENROLL_RECON_DISCREP_ID As String = "afa_enroll_recon_discrep_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_BILLING_DATE As String = "billing_date"
    Public Const COL_NAME_SOC_TYPE As String = "soc_type"
    Public Const COL_NAME_ACCOUNT_STATUS As String = "account_status"
    Public Const COL_NAME_BILLABLE_COUNT As String = "billable_count"
    Public Const COL_NAME_CARRIER_COUNT As String = "carrier_count"
    Public Const COL_NAME_DISCREPANCY As String = "discrepancy"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("afa_enroll_recon_discrep_id", id.ToByteArray)}
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

    Public Function GetPHPReconData(ByVal dealerId As Guid, ByVal firstDayOfMonth As String, ByVal lastDayOfMonth As String, ByVal showOnlyDiscrep As Boolean) As DataSet
        Dim whereClauseConditions As String = ""

        Dim selectStmt As String = Me.Config("/SQL/LOAD_PHP_RECON_DATA")
        If showOnlyDiscrep Then
            selectStmt = Me.Config("/SQL/LOAD_PHP_RECON_DATA_DISCREP_ONLY")
        End If

        If Not firstDayOfMonth Is Nothing AndAlso Not lastDayOfMonth Is Nothing Then
            whereClauseConditions &= Environment.NewLine & " and billing_date >= to_date('" & firstDayOfMonth & "','mmddyyyy') and billing_date <= to_date('" & lastDayOfMonth & "','mmddyyyy')"
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("dealer_id", dealerId.ToByteArray)}

        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    ' Execute Store Procedure
    Public Function OverRideReconciliation(ByVal dealerId As Guid, ByVal firstDayOfMonth As String, ByVal lastDayOfMonth As String,
                                           ByVal userName As String) As Boolean

        Dim inputParameters(3) As DBHelper.DBHelperParameter
        Dim selectStmt As String = Me.Config("/SQL/OVERRIDE_RECON")

        If Not dealerId = Guid.Empty Then
            inputParameters(0) = New DBHelper.DBHelperParameter("pi_dealer_id", dealerId.ToByteArray)
        End If
        If Not firstDayOfMonth = String.Empty Then
            inputParameters(1) = New DBHelper.DBHelperParameter("pi_billingDtStart", firstDayOfMonth)
        End If
        If Not lastDayOfMonth = String.Empty Then
            inputParameters(2) = New DBHelper.DBHelperParameter("pi_billingDtEnd", lastDayOfMonth)
        End If
        If Not userName = String.Empty Then
            inputParameters(3) = New DBHelper.DBHelperParameter("pi_userName", userName)
        End If

        Dim outputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                            New DBHelper.DBHelperParameter("po_Result", GetType(String))}

        Try
            ' Call DBHelper Store Procedure
            DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameters)
            If CType(outputParameters(0).Value, String).Trim = "N" Then
                Return False
            ElseIf CType(outputParameters(0).Value, String).Trim = "Y" Then
                Return True
            End If

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    ' Execute Store Procedure
    Public Function ReloadReconcillation(ByVal dealerId As Guid, ByVal firstDayOfMonth As String, ByVal userName As String) As Boolean

        Dim inputParameters(2) As DBHelper.DBHelperParameter
        Dim selectStmt As String = Me.Config("/SQL/RELOAD_RECON")

        inputParameters(0) = New DBHelper.DBHelperParameter("pi_dealer_id", dealerId.ToByteArray)
        inputParameters(1) = New DBHelper.DBHelperParameter("pi_billingDtStart", firstDayOfMonth)
        inputParameters(2) = New DBHelper.DBHelperParameter("pi_userName", userName)

        Dim outputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                            New DBHelper.DBHelperParameter("po_Result", GetType(String))}

        Try
            ' Call DBHelper Store Procedure
            DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameters)
            If CType(outputParameters(0).Value, String).Trim = "N" Then
                Return False
            ElseIf CType(outputParameters(0).Value, String).Trim = "Y" Then
                Return True
            End If

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


    Public Function GetMHPReconData(ByVal dealerId As Guid, ByVal firstDayOfMonth As String, ByVal lastDayOfMonth As String, ByVal showOnlyDiscrep As Boolean) As DataSet
        Dim whereClauseConditions As String = ""

        Dim selectStmt As String = Me.Config("/SQL/LOAD_MHP_RECON_DATA")
        If showOnlyDiscrep Then
            selectStmt = Me.Config("/SQL/LOAD_MHP_RECON_DATA_DISCREP_ONLY")
        End If

        If Not firstDayOfMonth Is Nothing AndAlso Not lastDayOfMonth Is Nothing Then
            whereClauseConditions &= Environment.NewLine & " and billing_date >= to_date('" & firstDayOfMonth & "','mmddyyyy') and billing_date <= to_date('" & lastDayOfMonth & "','mmddyyyy')"
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("dealer_id", dealerId.ToByteArray)}

        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
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


