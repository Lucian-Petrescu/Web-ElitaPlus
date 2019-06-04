'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (12/20/2006)********************


Public Class CoverageLossDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_COVERAGE_LOSS"
    Public Const TABLE_KEY_NAME As String = "coverage_loss_id"

    Public Const COL_NAME_COVERAGE_LOSS_ID As String = "coverage_loss_id"
    Public Const COL_NAME_COMPANY_GROUP_ID As String = "company_group_id"
    Public Const COL_NAME_COVERAGE_TYPE_ID As String = "coverage_type_id"
    Public Const COL_NAME_CAUSE_OF_LOSS_ID As String = "cause_of_loss_id"
    Public Const COL_NAME_LANGUAGE_ID As String = "language_id"
    Public Const COL_NAME_DEFAULT_FLAG As String = "default_flag"
    Public Const COL_NAME_ACTIVE As String = "active"
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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("coverage_loss_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub Load(ByVal familyDS As DataSet, ByVal oCompanyGroupId As Guid, ByVal causeOfLossId As Guid, ByVal coverageTypeId As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD_NOT_AVALABLE")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_CAUSE_OF_LOSS_ID, causeOfLossId.ToByteArray), _
                                                         New DBHelper.DBHelperParameter(COL_NAME_COVERAGE_TYPE_ID, coverageTypeId.ToByteArray), _
                                                         New DBHelper.DBHelperParameter(COL_NAME_COMPANY_GROUP_ID, oCompanyGroupId.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(ByVal CompanyGroupId As Guid, ByVal coverageTypeId As Guid, ByVal languageid As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim parameters() As OracleParameter

        If Not coverageTypeId.Equals(Guid.Empty) Then
            parameters = New OracleParameter() _
                                                {New OracleParameter(COL_NAME_COMPANY_GROUP_ID, CompanyGroupId.ToByteArray), _
                                                New OracleParameter(COL_NAME_COVERAGE_TYPE_ID, coverageTypeId.ToByteArray), _
                                                New OracleParameter(COL_NAME_LANGUAGE_ID, languageid.ToByteArray)}
        Else
            parameters = New OracleParameter() _
                                                {New OracleParameter(COL_NAME_COMPANY_GROUP_ID, CompanyGroupId.ToByteArray), _
                                                 New OracleParameter(COL_NAME_COVERAGE_TYPE_ID, "%%"), _
                                                 New OracleParameter(COL_NAME_LANGUAGE_ID, languageid.ToByteArray)}
        End If
        Try
            Return (DBHelper.Fetch(selectStmt, DSNAME, Me.TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Sub LoadList(ByVal coverageTypeId As Guid, ByVal CompanyGroupId As Guid, ByVal familyDataset As DataSet)
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/LOAD_ALL_COV_LOSS")
        parameters = New OracleParameter() {New OracleParameter(Me.COL_NAME_COVERAGE_TYPE_ID, coverageTypeId.ToByteArray), _
                                           New OracleParameter(Me.COL_NAME_COMPANY_GROUP_ID, CompanyGroupId.ToByteArray)}
        DBHelper.Fetch(familyDataset, selectStmt, Me.TABLE_NAME, parameters)
    End Sub

    Public Sub LoadList(ByVal coveragelossid As Guid, ByVal familyDataset As DataSet)
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/LOAD")
        parameters = New OracleParameter() {New OracleParameter(Me.COL_NAME_COVERAGE_LOSS_ID, coveragelossid.ToByteArray)}
        DBHelper.Fetch(familyDataset, selectStmt, Me.TABLE_NAME, parameters)
    End Sub

    Public Function LoadAvailableCausesOfLoss(ByVal grpID As Guid, ByVal languageid As Guid, ByVal coverageTypeId As Guid) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/GET_AVAILABLE_CAUSES_OF_LOSS")

        parameters = New OracleParameter() {New OracleParameter(COL_NAME_LANGUAGE_ID, languageid.ToByteArray), _
                                            New OracleParameter(COL_NAME_COMPANY_GROUP_ID, grpID.ToByteArray), _
                                            New OracleParameter(COL_NAME_COVERAGE_TYPE_ID, coverageTypeId.ToByteArray)}

        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
    End Function

    Public Function LoadSelectedCausesOfLoss(ByVal grpId As Guid, ByVal coverageTypeId As Guid, ByVal languageid As Guid) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/GET_SELECTED_CAUSES_OF_LOSS")

        parameters = New OracleParameter() {New OracleParameter(COL_NAME_COMPANY_GROUP_ID, grpId.ToByteArray), _
                                            New OracleParameter(COL_NAME_COVERAGE_TYPE_ID, coverageTypeId.ToByteArray), _
                                            New OracleParameter(COL_NAME_LANGUAGE_ID, languageid.ToByteArray)}
        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
    End Function
    Public Function GetCauseOfLossInUse(ByVal causeOfLossIds As ArrayList, ByVal compIds As ArrayList) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/GET_CAUSE_OF_LOSS_IN_USE")

        Dim inClauseCondition As String
        inClauseCondition &= MiscUtil.BuildListForSql("cl." & "company_id", compIds, True)
        selectStmt = selectStmt.Replace("--dynamic_in_clause_1", inClauseCondition)
        inClauseCondition = ""
        inClauseCondition &= MiscUtil.BuildListForSql("and cl." & Me.COL_NAME_CAUSE_OF_LOSS_ID, causeOfLossIds, True)
        selectStmt = selectStmt.Replace("--dynamic_in_clause_2", inClauseCondition)

        Try
            Dim ds As New DataSet
            Return (DBHelper.Fetch(selectStmt, Me.TABLE_NAME))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadSelectedCovLossFromCovandCauseOfLoss(ByVal causeOfLossId As Guid, ByVal coverageTypeId As Guid, ByVal grpId As Guid) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/LOAD_COVLOSS_FROM_COV_AND_CAUSE_OF_LOSS")

        parameters = New OracleParameter() {New OracleParameter(COL_NAME_CAUSE_OF_LOSS_ID, causeOfLossId.ToByteArray), _
                                            New OracleParameter(COL_NAME_COVERAGE_TYPE_ID, coverageTypeId.ToByteArray), _
                                            New OracleParameter(COL_NAME_COMPANY_GROUP_ID, grpId.ToByteArray)}
        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
    End Function

    Public Function LoadCauseOfLossByCov(ByVal coverageTypeId As Guid, ByVal grpId As Guid, ByVal language_id As Guid) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/LOAD_CAUSE_OF_LOSS_BY_COVERAGE")

        parameters = New OracleParameter() {New OracleParameter(COL_NAME_COVERAGE_TYPE_ID, coverageTypeId.ToByteArray), _
                                            New OracleParameter(COL_NAME_COMPANY_GROUP_ID, grpId.ToByteArray), _
                                            New OracleParameter(COL_NAME_LANGUAGE_ID, language_id.ToByteArray)}
        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
    End Function

    Public Function LoadDefaultCauseOfLossByCov(ByVal coverageTypeId As Guid, ByVal grpId As Guid, ByVal language_id As Guid) As DataSet
        Dim ds As New DataSet
        Dim parameters() As OracleParameter
        Dim selectStmt As String = Me.Config("/SQL/LOAD_DEFAULT_CAUSE_OF_LOSS_BY_COVERAGE")

        parameters = New OracleParameter() {New OracleParameter(COL_NAME_COVERAGE_TYPE_ID, coverageTypeId.ToByteArray), _
                                            New OracleParameter(COL_NAME_COMPANY_GROUP_ID, grpId.ToByteArray), _
                                            New OracleParameter(COL_NAME_LANGUAGE_ID, language_id.ToByteArray)}
        Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
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

#Region "Method for Web Service"
    Public Function getCoverageLossforSpecialService(ByVal splsvcCode As String, ByVal DealerId As Guid, ByVal CompanyGroupID As Guid) As DataSet
        Dim parameters() As DBHelper.DBHelperParameter
        Dim ds As New DataSet
        Dim selectStmt As String = Me.Config("/SQL/GetCoverageLossForSpecialService")
        Try
            parameters = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(DealerDAL.COL_NAME_DEALER_ID.ToUpper, DealerId.ToByteArray), _
                    New DBHelper.DBHelperParameter(COL_NAME_CODE.ToUpper.ToUpper, splsvcCode), _
                    New DBHelper.DBHelperParameter(COL_NAME_COMPANY_GROUP_ID.ToUpper, CompanyGroupID.ToByteArray)}

            Return DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region
End Class


