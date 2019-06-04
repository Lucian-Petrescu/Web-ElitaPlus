'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (11/18/2004)********************

#Region "CompensationPlanData"

Public Class CompensationPlanData
    Public dealerId As Guid
    Public companyIds As ArrayList
    Public dbCode As String

End Class

#End Region

Public Class CompensationPlanDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_COMMISSION_PLAN"
    Public Const TABLE_KEY_NAME As String = "commission_plan_id"
    '  Public Const DSNAME As String = "LIST"

    Public Const COL_NAME_COMMISSION_PLAN_ID As String = "commission_plan_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_COMPANY_CODE As String = "company_code"
    Public Const COL_NAME_CODE As String = "code"
    Public Const COL_NAME_DEALER_NAME As String = "dealer_name"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_EFFECTIVE_DATE As String = "effective_date"
    Public Const COL_NAME_EXPIRATION_DATE As String = "expiration_date"
    Public Const COL_NAME_EFFECTIVE As String = "effective"
    Public Const COL_NAME_EXPIRATION As String = "expiration"
    Public Const COL_NAME_REFERENCE As String = "Reference"
    Public Const COL_NAME_COMPANY_ID = "company_id"
    Public Const COL_NAME_DEALER_ID0 As String = "reference_id0"
    Public Const COL_NAME_DEALER_ID1 As String = "reference_id1"
    Public Const DEALER_ID = 0
    Public Const CODE_ID = 1
    Public Const DEALER_ID0 = 0
    Public Const DEALER_ID1 = 1
    Public Const TOTAL_PARAM = 0
    Public Const TOTAL_PARAM_A = 1
    ' Expiration
    Public Const COL_NAME_MAX_EXPIRATION As String = "expiration"
    Public Const COL_NAME_EXPIRATION_COUNT As String = "expiration_count"

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

    Public Sub Load(ByVal familyDS As DataSet, ByVal id As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("commission_Plan_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function CodeExistsForOtherDealers(ByVal dealerId As Guid, ByVal CommissionCode As String) As Integer

        Dim selectStmt As String = Me.Config("/SQL/CODE_MATCH")
        Dim parameters(TOTAL_PARAM_A) As DBHelper.DBHelperParameter
        Dim result As Object
        Dim inCausecondition As String

        parameters(DEALER_ID) = New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID, dealerId.ToByteArray)
        parameters(CODE_ID) = New DBHelper.DBHelperParameter(COL_NAME_CODE, CommissionCode)

        result = DBHelper.ExecuteScalar(selectStmt, parameters)

        Return Convert.ToInt32(result)

    End Function

    Public Function LoadList(ByVal oCompensationPlanData As CompensationPlanData) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim parameters(TOTAL_PARAM) As DBHelper.DBHelperParameter
        Dim inCausecondition As String

        With oCompensationPlanData
            inCausecondition &= MiscUtil.BuildListForSql("AND D." & Me.COL_NAME_COMPANY_ID, oCompensationPlanData.companyIds, True)

            If .dealerId.Equals(Guid.Empty) Then
                parameters(DEALER_ID) = New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID, GenericConstants.WILDCARD)
            Else
                parameters(DEALER_ID) = New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID, .dealerId.ToByteArray)
            End If
            selectStmt = selectStmt.Replace(Me.DYNAMIC_IN_CLAUSE_PLACE_HOLDER, inCausecondition)
        End With

        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


    Public Function LoadExpiration(ByVal oCompenstionPlanData As CompensationPlanData) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/MAX_EXPIRATION")
        Dim parameters(TOTAL_PARAM_A) As DBHelper.DBHelperParameter

        With oCompenstionPlanData
            parameters(DEALER_ID0) = New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID0, .dealerId.ToByteArray)
            parameters(DEALER_ID1) = New DBHelper.DBHelperParameter(COL_NAME_DEALER_ID1, .dealerId.ToByteArray)
        End With

        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


#End Region


    Public Overloads Sub update(ByVal ds As DataSet, Optional ByVal transaction As IDbTransaction = Nothing, Optional ByVal changesfilter As DataRowState = Nothing)
        If Not ds.Tables(Me.TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(Me.TABLE_NAME), transaction, changesfilter)
        End If
    End Sub





End Class



