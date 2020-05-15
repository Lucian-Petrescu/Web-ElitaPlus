'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (2/27/2007)********************


Public Class PoliceStationDAL1
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_POLICE_STATION"
    Public Const TABLE_KEY_NAME As String = "police_station_id"

    Public Const COL_NAME_POLICE_STATION_ID As String = "police_station_id"
    Public Const COL_NAME_COUNTRY_ID As String = "country_id"
    Public Const COL_NAME_POLICE_STATION_CODE As String = "police_station_code"
    Public Const COL_NAME_POLICE_STATION_NAME As String = "police_station_name"
    Public Const COL_NAME_POLICE_STATION_DISTRICT_CODE As String = "police_station_district_code"
    Public Const COL_NAME_POLICE_STATION_DISTRICT_NAME As String = "police_station_district_name"
    Public Const COL_NAME_ADDRESS1 As String = "address1"
    Public Const COL_NAME_ADDRESS2 As String = "address2"
    'Added for Def-1598
    Public Const COL_NAME_ADDRESS3 As String = "address3"

    Public Const COL_NAME_CITY As String = "city"
    Public Const COL_NAME_REGION_ID As String = "region_id"
    Public Const COL_NAME_POSTAL_CODE As String = "postal_code"
    Public Const PO_RETURN_MESSAGE As String = "po_return_message"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("police_station_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(ByVal descriptionMask As String, ByVal codeMask As String, ByVal CountryMask As Guid) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = ""
        Dim inCausecondition As String = ""
        Dim bIsLikeClause As Boolean = False

        bIsLikeClause = IsThereALikeClause(descriptionMask, codeMask)

        whereClauseConditions &= " WHERE UPPER(" & Me.COL_NAME_COUNTRY_ID & ") ='" & Me.GuidToSQLString(CountryMask) & "'"

        If ((Not (descriptionMask Is Nothing)) AndAlso (Me.FormatSearchMask(descriptionMask))) Then
            whereClauseConditions &= " AND UPPER(" & Me.COL_NAME_POLICE_STATION_NAME & ")" & descriptionMask.ToUpper
        End If

        If ((Not (codeMask Is Nothing)) AndAlso (Me.FormatSearchMask(codeMask))) Then
            whereClauseConditions &= Environment.NewLine & " AND UPPER(" & Me.COL_NAME_POLICE_STATION_CODE & ")" & codeMask.ToUpper
        End If

        selectStmt = selectStmt.Replace(Me.DYNAMIC_IN_CLAUSE_PLACE_HOLDER, inCausecondition)

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        selectStmt = selectStmt.Replace(Me.DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, "ORDER BY " & Me.COL_NAME_POLICE_STATION_NAME & ", " & Me.COL_NAME_POLICE_STATION_CODE)
        Try
            'Dim ds = New DataSet
            Return (DBHelper.Fetch(selectStmt, Me.TABLE_NAME))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try


        Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
    End Function

    Private Function IsThereALikeClause(ByVal descriptionMask As String, ByVal codeMask As String) As Boolean
        Dim bIsLikeClause As Boolean

        bIsLikeClause = Me.IsLikeClause(descriptionMask) OrElse Me.IsLikeClause(codeMask)
        Return bIsLikeClause
    End Function
#End Region
    Public Sub SavePoliceStation(ByVal row As DataRow, ByVal policeStationId As Guid)
        Dim selectStmt As String
        Dim rowState As DataRowState = row.RowState
        Dim inParameters As DBHelper.DBHelperParameter()
        Dim outParameters As DBHelper.DBHelperParameter()

        Select Case rowState
            Case DataRowState.Added
                selectStmt = Me.Config("/SQL/ADD_NEW_POLICE_STATION")
            Case DataRowState.Deleted
                selectStmt = Me.Config("/SQL/DELETE_POLICE_STATION")
            Case DataRowState.Modified
                selectStmt = Me.Config("/SQL/MODIFY_POLICE_STATION")
        End Select

        If row.RowState = DataRowState.Added Or row.RowState = DataRowState.Modified Then

            inParameters = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(Me.COL_NAME_COUNTRY_ID, New Guid(CType(row.Item(Me.COL_NAME_COUNTRY_ID), Byte())).ToByteArray()),
                                                New DBHelper.DBHelperParameter(Me.COL_NAME_POLICE_STATION_CODE, row.Item(Me.COL_NAME_POLICE_STATION_CODE).ToString()),
                                                New DBHelper.DBHelperParameter(Me.COL_NAME_POLICE_STATION_NAME, row.Item(Me.COL_NAME_POLICE_STATION_NAME).ToString()),
                                                New DBHelper.DBHelperParameter(Me.COL_NAME_ADDRESS1, row.Item(Me.COL_NAME_ADDRESS1).ToString()),
                                                New DBHelper.DBHelperParameter(Me.COL_NAME_ADDRESS2, row.Item(Me.COL_NAME_ADDRESS2).ToString()),
                                                New DBHelper.DBHelperParameter(Me.COL_NAME_ADDRESS3, row.Item(Me.COL_NAME_ADDRESS3).ToString()),
                                                New DBHelper.DBHelperParameter(Me.COL_NAME_CITY, row.Item(Me.COL_NAME_CITY).ToString()),
                                                New DBHelper.DBHelperParameter(Me.COL_NAME_REGION_ID, If(IsDBNull(row.Item(Me.COL_NAME_REGION_ID)), row.Item(Me.COL_NAME_REGION_ID), New Guid(CType(row.Item(Me.COL_NAME_REGION_ID), Byte())).ToByteArray())),
                                                New DBHelper.DBHelperParameter(Me.COL_NAME_POSTAL_CODE, row.Item(Me.COL_NAME_POSTAL_CODE).ToString()),
                                                New DBHelper.DBHelperParameter(Me.COL_NAME_CREATED_BY, row.Item(Me.COL_NAME_CREATED_BY).ToString()),
                                                New DBHelper.DBHelperParameter(Me.COL_NAME_POLICE_STATION_ID, New Guid(CType(row.Item(Me.COL_NAME_POLICE_STATION_ID), Byte())).ToByteArray()),
                                                New DBHelper.DBHelperParameter(Me.COL_NAME_POLICE_STATION_DISTRICT_CODE, row.Item(Me.COL_NAME_POLICE_STATION_DISTRICT_CODE).ToString()),
                                                New DBHelper.DBHelperParameter(Me.COL_NAME_POLICE_STATION_DISTRICT_NAME, row.Item(Me.COL_NAME_POLICE_STATION_DISTRICT_NAME).ToString())}
            outParameters = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(Me.PO_RETURN_MESSAGE, GetType(String))}

        ElseIf row.RowState = DataRowState.Deleted Then
            inParameters = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(Me.COL_NAME_POLICE_STATION_ID, policeStationId.ToByteArray())}
            outParameters = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(Me.PO_RETURN_MESSAGE, GetType(String))}

        End If

        Dim ds As New DataSet
        Dim tbl As String = Me.TABLE_NAME

        ' Call DBHelper Store Procedure
        DBHelper.FetchSp(selectStmt, inParameters, outParameters, ds, tbl)
        ' MyBase.UpdateFromSP()

        If outParameters(0).Value <> "Success" Then
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr)
        Else
            row.AcceptChanges()
        End If

    End Sub

#Region "Overloaded Methods"
    Public Overloads Sub UpdateFromSP(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(Me.TABLE_NAME) Is Nothing Then
            MyBase.UpdateFromSP(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub

#End Region

#Region "Override Methods"
    Protected Overrides Sub ConfigureUpdateCommand(ByRef command As OracleCommand, ByVal tableName As String)
        With command
            .BindByName = True
            .AddParameter(parameterName:="pi_country_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_COUNTRY_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_police_station_code", dbType:=OracleDbType.Varchar2, sourceColumn:=COL_NAME_POLICE_STATION_CODE, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_police_station_name", dbType:=OracleDbType.Varchar2, sourceColumn:=COL_NAME_POLICE_STATION_NAME, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_address1", dbType:=OracleDbType.Varchar2, sourceColumn:=COL_NAME_ADDRESS1, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_address2", dbType:=OracleDbType.Varchar2, sourceColumn:=COL_NAME_ADDRESS2, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_address3", dbType:=OracleDbType.Varchar2, sourceColumn:=COL_NAME_ADDRESS3, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_city", dbType:=OracleDbType.Varchar2, sourceColumn:=COL_NAME_CITY, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_region_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_REGION_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_postal_code", dbType:=OracleDbType.Varchar2, sourceColumn:=COL_NAME_POSTAL_CODE, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_modified_by", dbType:=OracleDbType.Varchar2, sourceColumn:=COL_NAME_MODIFIED_BY, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_police_station_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_POLICE_STATION_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_police_station_Dist_code", dbType:=OracleDbType.Varchar2, sourceColumn:=COL_NAME_POLICE_STATION_DISTRICT_CODE, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_police_station_Dist_Name", dbType:=OracleDbType.Varchar2, sourceColumn:=COL_NAME_POLICE_STATION_DISTRICT_NAME, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="po_return_message", dbType:=OracleDbType.Varchar2, sourceColumn:=PO_RETURN_MESSAGE, direction:=ParameterDirection.Input)

        End With
    End Sub

    'Protected Overrides Sub ConfigureDeleteCommand(ByRef command As OracleCommand, ByVal tableName As String)
    '    With command
    '        .BindByName = True
    '        .AddParameter(parameterName:="pi_event_config_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_EVENT_CONFIG_ID, direction:=ParameterDirection.Input)
    '    End With
    'End Sub

    'Protected Overrides Sub ConfigureInsertCommand(ByRef command As OracleCommand, ByVal tableName As String)
    '    With command
    '        .BindByName = True
    '        .AddParameter(parameterName:="pi_event_config_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_EVENT_CONFIG_ID, direction:=ParameterDirection.Input)
    '        .AddParameter(parameterName:="pi_company_group_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_COMPANY_GROUP_ID, direction:=ParameterDirection.Input)
    '        .AddParameter(parameterName:="pi_company_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_COMPANY_ID, direction:=ParameterDirection.Input)
    '        .AddParameter(parameterName:="pi_country_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_COUNTRY_ID, direction:=ParameterDirection.Input)
    '        .AddParameter(parameterName:="pi_dealer_group_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_DEALER_GROUP_ID, direction:=ParameterDirection.Input)
    '        .AddParameter(parameterName:="pi_dealer_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_DEALER_ID, direction:=ParameterDirection.Input)
    '        .AddParameter(parameterName:="pi_product_code", dbType:=OracleDbType.Varchar2, sourceColumn:=COL_NAME_PRODUCT_CODE, direction:=ParameterDirection.Input)
    '        .AddParameter(parameterName:="pi_event_type_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_EVENT_TYPE_ID, direction:=ParameterDirection.Input)
    '        .AddParameter(parameterName:="pi_coverage_type_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_COVERAGE_TYPE_ID, direction:=ParameterDirection.Input)
    '        .AddParameter(parameterName:="pi_event_argument_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_EVENT_ARGUMENT_ID, direction:=ParameterDirection.Input)
    '        .AddParameter(parameterName:="pi_created_by", dbType:=OracleDbType.Varchar2, sourceColumn:=COL_NAME_CREATED_BY, direction:=ParameterDirection.Input)
    '    End With
    'End Sub
#End Region

End Class


