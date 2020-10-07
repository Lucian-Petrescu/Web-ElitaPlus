'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (12/20/2012)********************


Public Class PoliceStationDAL
    Inherits OracleDALBase


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

    Public Sub Load(familyDS As DataSet, id As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("police_station_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(descriptionMask As String, codeMask As String, CountryMask As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = ""
        Dim inCausecondition As String = ""
        Dim bIsLikeClause As Boolean = False

        bIsLikeClause = IsThereALikeClause(descriptionMask, codeMask)

        whereClauseConditions &= " WHERE UPPER(" & COL_NAME_COUNTRY_ID & ") ='" & GuidToSQLString(CountryMask) & "'"

        If ((Not (descriptionMask Is Nothing)) AndAlso (FormatSearchMask(descriptionMask))) Then
            whereClauseConditions &= " AND UPPER(" & COL_NAME_POLICE_STATION_NAME & ")" & descriptionMask.ToUpper
        End If

        If ((Not (codeMask Is Nothing)) AndAlso (FormatSearchMask(codeMask))) Then
            whereClauseConditions &= Environment.NewLine & " AND UPPER(" & COL_NAME_POLICE_STATION_CODE & ")" & codeMask.ToUpper
        End If

        selectStmt = selectStmt.Replace(DYNAMIC_IN_CLAUSE_PLACE_HOLDER, inCausecondition)

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        selectStmt = selectStmt.Replace(DYNAMIC_ORDER_BY_CLAUSE_PLACE_HOLDER, "ORDER BY " & COL_NAME_POLICE_STATION_NAME & ", " & COL_NAME_POLICE_STATION_CODE)
        Try
            'Dim ds = New DataSet
            Return (DBHelper.Fetch(selectStmt, TABLE_NAME))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try


        Return DBHelper.Fetch(selectStmt, TABLE_NAME)
    End Function

    Private Function IsThereALikeClause(descriptionMask As String, codeMask As String) As Boolean
        Dim bIsLikeClause As Boolean

        bIsLikeClause = IsLikeClause(descriptionMask) OrElse IsLikeClause(codeMask)
        Return bIsLikeClause
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

    Protected Overrides Sub ConfigureUpdateCommand(ByRef command As OracleCommand, tableName As String)
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
        End With
    End Sub

    Protected Overrides Sub ConfigureDeleteCommand(ByRef command As OracleCommand, tableName As String)
        With command
            .BindByName = True
            .AddParameter(parameterName:="pi_police_station_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_POLICE_STATION_ID, direction:=ParameterDirection.Input)
        End With
    End Sub

    Protected Overrides Sub ConfigureInsertCommand(ByRef command As OracleCommand, tableName As String)
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
            .AddParameter(parameterName:="pi_created_by", dbType:=OracleDbType.Varchar2, sourceColumn:=COL_NAME_CREATED_BY, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_police_station_id", dbType:=OracleDbType.Raw, sourceColumn:=COL_NAME_POLICE_STATION_ID, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_police_station_Dist_code", dbType:=OracleDbType.Varchar2, sourceColumn:=COL_NAME_POLICE_STATION_DISTRICT_CODE, direction:=ParameterDirection.Input)
            .AddParameter(parameterName:="pi_police_station_Dist_Name", dbType:=OracleDbType.Varchar2, sourceColumn:=COL_NAME_POLICE_STATION_DISTRICT_NAME, direction:=ParameterDirection.Input)
        End With
    End Sub

#End Region

End Class