'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (4/11/2007)********************


Public Class VSCRateVersionDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_VSC_RATE_VERSION"
    Public Const TABLE_KEY_NAME As String = "vsc_rate_version_id"

    Public Const COL_NAME_VSC_RATE_VERSION_ID As String = "vsc_rate_version_id"
    Public Const COL_NAME_VSC_PLAN_ID As String = "vsc_plan_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_DEALER_GROUP_ID As String = "dealer_group_id"
    Public Const COL_NAME_VERSION_NUMBER As String = "version_number"
    Public Const COL_NAME_EFFECTIVE_DATE As String = "effective_date"
    Public Const COL_NAME_EXPIRATION_DATE As String = "expiration_date"

    Public Enum SearchByType
        Dealer
        DealerGroup
        CompanyGroup
    End Enum

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("vsc_rate_version_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(ByVal SearchBy As SearchByType, ByVal CompanyGroupID As Guid, _
                            ByVal PlanID As Guid, _
                            ByVal Code As String, _
                            ByVal Name As String, _
                            ByVal EffectiveDate As Date, _
                            Optional ByVal HighestVersionOnly As Boolean = True, _
                            Optional ByVal iVersionNumber As Integer = 0) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")

        Dim whereClauseConditions As String = ""

        If Not PlanID.Equals(Guid.Empty) Then
            whereClauseConditions &= Environment.NewLine & " and rv.vsc_plan_id = " & MiscUtil.GetDbStringFromGuid(PlanID)
        End If

        If SearchBy = SearchByType.Dealer Then
            whereClauseConditions &= Environment.NewLine & " AND dg.dealer_group_id is null and d.dealer_id is not null"
            If Me.FormatSearchMask(Code) Then
                whereClauseConditions &= Environment.NewLine & " AND " & "UPPER(d.dealer) " & Code.ToUpper
            End If
            If Me.FormatSearchMask(Name) Then
                whereClauseConditions &= Environment.NewLine & " AND " & "UPPER(d.Dealer_Name) " & Name.ToUpper
            End If
        ElseIf SearchBy = SearchByType.DealerGroup Then
            whereClauseConditions &= Environment.NewLine & " AND dg.dealer_group_id is not null and d.dealer_id is null"
            If Me.FormatSearchMask(Code) Then
                whereClauseConditions &= Environment.NewLine & " AND " & "UPPER(dg.CODE) " & Code.ToUpper
            End If
            If Me.FormatSearchMask(Name) Then
                whereClauseConditions &= Environment.NewLine & " AND " & "UPPER(dg.DESCRIPTION) " & Name.ToUpper
            End If
        ElseIf SearchBy = SearchByType.CompanyGroup Then
            whereClauseConditions &= Environment.NewLine & " AND dg.dealer_group_id is null and d.dealer_id is null"
        End If

        If EffectiveDate > Date.MinValue Then
            whereClauseConditions &= Environment.NewLine & " AND trunc(rv.EFFECTIVE_DATE) = TO_DATE('" & EffectiveDate.ToString("MM/dd/yyyy") & "','mm/dd/yyyy')"
        End If

        If iVersionNumber <> 0 Then
            whereClauseConditions &= Environment.NewLine & " AND rv.VERSION_NUMBER = " & iVersionNumber
        End If

        If HighestVersionOnly Then
            whereClauseConditions &= Environment.NewLine & " and not exists(select null from ELP_VSC_RATE_VERSION where vsc_plan_id = rv.vsc_plan_id and version_number > rv.version_number and nvl(dealer_group_id, '0') =  nvl(rv.dealer_group_id, '0') and nvl(dealer_id, '0') = nvl(rv.dealer_id, '0'))"
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("company_group_id", CompanyGroupID.ToByteArray)}
        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function LoadListByDealerGroup(ByVal CompanyGroupID As Guid, _
                                            ByVal PlanID As Guid, _
                                            ByVal DealerGroupID As Guid, _
                                            Optional ByVal HighestVersionOnly As Boolean = True) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")

        Dim whereClauseConditions As String = ""

        If Not PlanID.Equals(Guid.Empty) Then
            whereClauseConditions &= Environment.NewLine & " and rv.vsc_plan_id = " & MiscUtil.GetDbStringFromGuid(PlanID)
        End If
        If Not DealerGroupID.Equals(Guid.Empty) Then
            whereClauseConditions &= Environment.NewLine & " and dg.DEALER_GROUP_ID = " & MiscUtil.GetDbStringFromGuid(DealerGroupID)
        End If
        If HighestVersionOnly Then
            whereClauseConditions &= Environment.NewLine & " and not exists(select null from ELP_VSC_RATE_VERSION where vsc_plan_id = rv.vsc_plan_id and version_number > rv.version_number and nvl(dealer_group_id, '0') =  nvl(rv.dealer_group_id, '0') and nvl(dealer_id, '0') = nvl(rv.dealer_id, '0'))"
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("company_group_id", CompanyGroupID.ToByteArray)}
        Try
            Dim ds As New DataSet
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try


    End Function

    Public Function validateEffectiveDate(RateVersionId As Guid, EffectiveDate As Date) As String
        Dim sqlStmt As String
        sqlStmt = Me.Config("/SQL/VALIDATE_VSC_PLAN_RATE")

        Try
            Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                            New DBHelper.DBHelperParameter("po_errorCode", GetType(String), 500)}

            Dim inParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {
                            New DBHelper.DBHelperParameter("pi_vsc_rate_version_id", RateVersionId.ToByteArray),
                            New DBHelper.DBHelperParameter("pi_NewEffectiveDate", EffectiveDate)}

            DBHelper.ExecuteSpParamBindByName(sqlStmt, inParameters, outParameters)

            If outParameters(0).Value IsNot Nothing Then
                Return outParameters(0).Value.ToString()
            End If

            Return ""
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


