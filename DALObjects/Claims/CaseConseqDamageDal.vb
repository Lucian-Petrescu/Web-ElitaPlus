'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (11/15/2017)********************


Public Class CaseConseqDamageDal
    Inherits DALBase


#Region "Constants"
    Public Const TableName As String = "ELP_CASE_CONSEQ_DAMAGE"
    Public Const TableKeyName As String = "case_conseq_damage_id"

    Public Const ColNameCaseConseqDamageId As String = "case_conseq_damage_id"
    Public Const ColNameCaseId As String = "case_id"
    Public Const ColNameClaimId As String = "claim_id"
    Public Const ColNameConseqDamageXcd As String = "conseq_damage_xcd"
    Public Const ColNameCoverageConseqDamageId As String = "coverage_conseq_damage_id"
    Public Const ColNameStatusXcd As String = "status_xcd"
    Public Const ColNameRequestedAmount As String = "requested_amount"
    Public Const ColNameApprovedAmount As String = "approved_amount"

#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.New()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(familyDs As DataSet, id As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("case_conseq_damage_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDs, selectStmt, TableName, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList() As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Return DBHelper.Fetch(selectStmt, TableName)
    End Function
    Public Function LoadListConsequentialDamage(claimId As Guid, languageId As Guid) As DataSet
        Try
            Dim selectStmt As String = Config("/SQL/LOAD_CONSEQUENTIAL_DAMAGE")
            Dim ds As New DataSet
            Dim whereClauseConditions As String = String.Empty
            Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                                                                {New DBHelper.DBHelperParameter("langid", languageId.ToByteArray),
                                                                New DBHelper.DBHelperParameter("langid", languageId.ToByteArray),
                                                                New DBHelper.DBHelperParameter(ColNameClaimId, claimId.ToByteArray)}

            DBHelper.Fetch(ds, selectStmt, TableName, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ds As DataSet, Optional ByVal transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(TableName) Is Nothing Then
            Update(ds.Tables(TableName), transaction, changesFilter)
        End If
    End Sub
#End Region


End Class


