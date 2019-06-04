'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (2/27/2007)********************


Public Class PoliceStationDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_POLICE_STATION"
    Public Const TABLE_KEY_NAME As String = "police_station_id"

    Public Const COL_NAME_POLICE_STATION_ID As String = "police_station_id"
    Public Const COL_NAME_COUNTRY_ID As String = "country_id"
    Public Const COL_NAME_POLICE_STATION_CODE As String = "police_station_code"
    Public Const COL_NAME_POLICE_STATION_NAME As String = "police_station_name"
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
        MyBase.new()
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


