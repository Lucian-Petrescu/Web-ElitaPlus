'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (3/14/2007)********************


Public Class VSCModelDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_VSC_MODEL"
    Public Const TABLE_KEY_NAME As String = "model_id"

    Public Const COL_NAME_COMPANY_GROUP_ID As String = "company_group_id"
    Public Const COL_NAME_MODEL_ID As String = "model_id"
    Public Const COL_NAME_MANUFACTURER_ID As String = "manufacturer_id"
    Public Const COL_NAME_MAKE As String = "make"
    Public Const COL_NAME_CAR_CODE As String = "car_code"
    Public Const COL_NAME_MODEL As String = "model"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_ENGINE_VERSION As String = "engine_version"
    Public Const COL_NAME_MODEL_YEAR As String = "model_year"
    Public Const COL_NAME_NEW_CLASS_CODE_ID As String = "new_class_code_id"
    Public Const COL_NAME_USED_CLASS_CODE_ID As String = "used_class_code_id"
    Public Const COL_NAME_NEW_CLASS_CODE As String = "new_class_code"
    Public Const COL_NAME_USED_CLASS_CODE As String = "used_class_code"
    Public Const COL_NAME_ACTIVE_NEW_ID As String = "active_new_id"
    Public Const COL_NAME_ACTIVE_USED_ID As String = "active_used_id"
    Public Const COL_NAME_ACTIVE_NEW As String = "active_new"
    Public Const COL_NAME_ACTIVE_USED As String = "active_used"
    Public Const COL_NAME_ENGINE_MONTHS_KM_MI As String = "engine_months_km_mi"
    Public Const COL_NAME_COVERAGE_LIMIT_ID As String = "vsc_coverage_limit_id"
    Public Const COL_NAME_COVERAGE_LIMIT_CODE As String = "coverage_limit_code"
    'REQ-1142
    Public Const COL_NAME_EXTERNAL_CAR_CODE As String = "external_car_code"

    Public Const VAL_NEW As String = "NEW"
    Public Const VAL_USED As String = "USED"
    Private Const DSNAME As String = "GetVSCModels"


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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("model_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
    Public Sub Load(ByVal familyDS As DataSet, _
                    ByVal make As String, _
                    ByVal model As String, _
                    ByVal trim As String, _
                    ByVal year As Integer)
        Dim selectStmt As String = Me.Config("/SQL/LOAD_BY_MDY")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_MANUFACTURER_ID, make), _
                                                                                           New DBHelper.DBHelperParameter(COL_NAME_MODEL, model), _
                                                                                           New DBHelper.DBHelperParameter(COL_NAME_MODEL_ID, trim), _
                                                                                           New DBHelper.DBHelperParameter(COL_NAME_MODEL_YEAR, year)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(ByVal companyGroupId As Guid, ByVal make As String, _
                    ByVal model As String, _
                    ByVal trim As String, _
                    ByVal year As String) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")

        Dim dynamic_where_clause As String

        dynamic_where_clause &= Environment.NewLine & "AND MANF." & Me.COL_NAME_COMPANY_GROUP_ID & " =  '" & Me.GuidToSQLString(companyGroupId) & "'"

        If Not make Is Nothing AndAlso Not make.Equals(String.Empty) Then
            dynamic_where_clause &= Environment.NewLine & "And M." & COL_NAME_MANUFACTURER_ID & " ='" & make & "'"
        End If

        If Not model Is Nothing AndAlso Not model.Equals(String.Empty) Then
            dynamic_where_clause &= Environment.NewLine & "And M." & COL_NAME_MODEL & " ='" & model & "'"
        End If

        If Not trim Is Nothing AndAlso Not trim.Equals(String.Empty) Then
            dynamic_where_clause &= Environment.NewLine & "And M." & COL_NAME_DESCRIPTION & " ='" & trim & "'"
        End If
        If Not year Is Nothing AndAlso Not year.Equals(String.Empty) Then
            dynamic_where_clause &= Environment.NewLine & "And M." & COL_NAME_MODEL_YEAR & " =" & year
        End If

        dynamic_where_clause &= Environment.NewLine & "AND CL." & Me.COL_NAME_COMPANY_GROUP_ID & " =  '" & Me.GuidToSQLString(companyGroupId) & "'"

        selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, dynamic_where_clause)

        selectStmt = selectStmt.Replace(Me.DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, _
                                            Environment.NewLine & "ORDER BY " & Environment.NewLine & Me.COL_NAME_MANUFACTURER_ID & ", " & Me.COL_NAME_MODEL & ", " & COL_NAME_MODEL_YEAR)

        Try
            Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadDistinctList(ByVal companyGroupId As Guid, ByVal make As String, _
                ByVal model As String, _
                ByVal trim As String, _
                ByVal year As String, _
                ByVal requestedfield As String) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_DISTINCTLIST")

        Dim dynamic_field_selector As String
        Dim dynamic_where_clause As String

        dynamic_field_selector = requestedfield

        dynamic_where_clause &= Environment.NewLine & "AND MANF." & Me.COL_NAME_COMPANY_GROUP_ID & " =  '" & Me.GuidToSQLString(companyGroupId) & "'"

        If Not make Is Nothing AndAlso Not make.Equals(String.Empty) Then
            dynamic_where_clause &= Environment.NewLine & "And M." & COL_NAME_MANUFACTURER_ID & " ='" & make & "'"
        End If

        If Not model Is Nothing AndAlso Not model.Equals(String.Empty) Then
            dynamic_where_clause &= Environment.NewLine & "And M." & COL_NAME_MODEL & " ='" & model & "'"
        End If

        If Not trim Is Nothing AndAlso Not trim.Equals(String.Empty) Then
            dynamic_where_clause &= Environment.NewLine & "And M." & COL_NAME_DESCRIPTION & " ='" & trim & "'"
        End If
        If Not year Is Nothing AndAlso Not year.Equals(String.Empty) Then
            dynamic_where_clause &= Environment.NewLine & "And M." & COL_NAME_MODEL_YEAR & " =" & year
        End If

        selectStmt = selectStmt.Replace(Me.DYNAMIC_FIELD_SELECTOR_PLACE_HOLDER, dynamic_field_selector)

        selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, dynamic_where_clause)

        selectStmt = selectStmt.Replace(Me.DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, _
                                            Environment.NewLine & "ORDER BY " & Environment.NewLine & Me.COL_NAME_MANUFACTURER_ID & ", " & Me.COL_NAME_MODEL & ", " & COL_NAME_MODEL_YEAR)

        Try
            Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function
    Public Function LoadVSCModels(ByVal companyGroupId As Guid, ByVal make As String) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_VSC_MODELS")

        Dim parameters() As OracleParameter = New OracleParameter() _
                                     {New OracleParameter(COL_NAME_COMPANY_GROUP_ID, companyGroupId.ToByteArray), _
                                      New OracleParameter(COL_NAME_DESCRIPTION, make.ToUpper)}
        Try
            Return (DBHelper.Fetch(selectStmt, DSNAME, Me.TABLE_NAME, parameters))

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadExternalCardCode(ByVal companyGroupId As Guid, ByVal externalCarCode As String, ByVal manufacturerId As Guid, ByVal model As String, ByVal version As String) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_VSC_EXTERNALCARCODE")

        Dim parameters() As OracleParameter = New OracleParameter() _
                                     {New OracleParameter(COL_NAME_COMPANY_GROUP_ID, companyGroupId.ToByteArray), _
                                      New OracleParameter(COL_NAME_DESCRIPTION, manufacturerId.ToByteArray), _
                                      New OracleParameter(COL_NAME_MODEL, model.ToUpper), _
                                      New OracleParameter(COL_NAME_ENGINE_VERSION, version.ToUpper), _
                                      New OracleParameter(COL_NAME_EXTERNAL_CAR_CODE, externalCarCode.ToUpper)}
        Try
            Return (DBHelper.Fetch(selectStmt, DSNAME, Me.TABLE_NAME, parameters))

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function
    Public Function LoadVSCEngineVersions(ByVal companyGroupId As Guid, ByVal make As String, ByVal model As String) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_VSC_ENGINE_VERSIONS")

        Dim parameters() As OracleParameter = New OracleParameter() _
                                     {New OracleParameter(COL_NAME_COMPANY_GROUP_ID, companyGroupId.ToByteArray), _
                                      New OracleParameter(COL_NAME_DESCRIPTION, make.ToUpper), _
                                      New OracleParameter(COL_NAME_MODEL, model.ToUpper)}
        Try
            Return (DBHelper.Fetch(selectStmt, DSNAME, Me.TABLE_NAME, parameters))

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function
    Public Function LoadVSCYears(ByVal companyGroupId As Guid, ByVal make As String, ByVal model As String, ByVal engineVersion As String) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_VSC_YEARS")

        Dim parameters() As OracleParameter = New OracleParameter() _
                                     {New OracleParameter(COL_NAME_COMPANY_GROUP_ID, companyGroupId.ToByteArray), _
                                      New OracleParameter(COL_NAME_DESCRIPTION, make.ToUpper), _
                                      New OracleParameter(COL_NAME_MODEL, model.ToUpper), _
                                      New OracleParameter(COL_NAME_ENGINE_VERSION, engineVersion.ToUpper)}
        Try
            Return (DBHelper.Fetch(selectStmt, DSNAME, Me.TABLE_NAME, parameters))

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


