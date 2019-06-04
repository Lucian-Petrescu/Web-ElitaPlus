'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (2/27/2007)  ********************

Public Class PoliceStation
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load(id)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDS As DataSet)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As DataSet)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load()
    End Sub

    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Me.Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New PoliceStationDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            setvalue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New PoliceStationDAL
            If Me._isDSCreator Then
                If Not Me.Row Is Nothing Then
                    Me.Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Me.Row)
                End If
            End If
            Me.Row = Nothing
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Me.Row = Me.FindRow(id, dal.TABLE_KEY_NAME, Me.Dataset.Tables(dal.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.Load(Me.Dataset, id)
                Me.Row = Me.FindRow(id, dal.TABLE_KEY_NAME, Me.Dataset.Tables(dal.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then
                Throw New DataNotFoundException
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
#End Region

#Region "Private Members"
    'Initialization code for new objects
    Private Sub Initialize()
    End Sub
#End Region

#Region "CONSTANTS"

    Private Const POLICE_STATION_NAME_REQUIRED As String = "POLICE_STATION_NAME_REQUIRED"
    Private Const MIN_POLICE_STATION_NAME_LENGTH As String = "1"
    Private Const MAX_POLICE_STATION_NAME_LENGTH As String = "200"

    Private Const MIN_POLICE_STATION_CODE_LENGTH As String = "1"
    Private Const MAX_POLICE_STATION_CODE_LENGTH As String = "5"
    Private Const POLICE_STATION_CODE_REQUIRED As String = "POLICE_STATION_CODE_REQUIRED"

    Public Const COL_POLICE_STATION_ID As String = "POLICE_STATION_ID"
    Public Const COL_POLICE_STATION_CODE As String = "POLICE_STATION_CODE"
    Public Const COL_POLICE_STATION_NAME As String = "POLICE_STATION_NAME"
    Public Const WILDCARD_CHAR As Char = "%"
    Public Const ASTERISK As Char = "*"
    Private Const DSNAME As String = "LIST"

#End Region

#Region "Variables"

#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If Row(PoliceStationDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PoliceStationDAL.COL_NAME_POLICE_STATION_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CountryId() As Guid
        Get
            CheckDeleted()
            If Row(PoliceStationDAL.COL_NAME_COUNTRY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PoliceStationDAL.COL_NAME_COUNTRY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(PoliceStationDAL.COL_NAME_COUNTRY_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=15)> _
    Public Property PoliceStationCode() As String
        Get
            CheckDeleted()
            If Row(PoliceStationDAL.COL_NAME_POLICE_STATION_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(PoliceStationDAL.COL_NAME_POLICE_STATION_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(PoliceStationDAL.COL_NAME_POLICE_STATION_CODE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=200)> _
    Public Property PoliceStationName() As String
        Get
            CheckDeleted()
            If Row(PoliceStationDAL.COL_NAME_POLICE_STATION_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(PoliceStationDAL.COL_NAME_POLICE_STATION_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(PoliceStationDAL.COL_NAME_POLICE_STATION_NAME, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200)> _
    Public Property Address1() As String
        Get
            CheckDeleted()
            If Row(PoliceStationDAL.COL_NAME_ADDRESS1) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(PoliceStationDAL.COL_NAME_ADDRESS1), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(PoliceStationDAL.COL_NAME_ADDRESS1, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200)> _
    Public Property Address2() As String
        Get
            CheckDeleted()
            If Row(PoliceStationDAL.COL_NAME_ADDRESS2) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(PoliceStationDAL.COL_NAME_ADDRESS2), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(PoliceStationDAL.COL_NAME_ADDRESS2, Value)
        End Set
    End Property

    'Added for Def-1598
    <ValidStringLength("", Max:=200)> _
    Public Property Address3() As String
        Get
            CheckDeleted()
            If Row(PoliceStationDAL.COL_NAME_ADDRESS3) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(PoliceStationDAL.COL_NAME_ADDRESS3), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(PoliceStationDAL.COL_NAME_ADDRESS3, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=200)> _
    Public Property City() As String
        Get
            CheckDeleted()
            If Row(PoliceStationDAL.COL_NAME_CITY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(PoliceStationDAL.COL_NAME_CITY), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(PoliceStationDAL.COL_NAME_CITY, Value)
        End Set
    End Property

    Public Property RegionId() As Guid
        Get
            CheckDeleted()
            If Row(PoliceStationDAL.COL_NAME_REGION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PoliceStationDAL.COL_NAME_REGION_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(PoliceStationDAL.COL_NAME_REGION_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=40)> _
    Public Property PostalCode() As String
        Get
            CheckDeleted()
            If Row(PoliceStationDAL.COL_NAME_POSTAL_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(PoliceStationDAL.COL_NAME_POSTAL_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(PoliceStationDAL.COL_NAME_POSTAL_CODE, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New PoliceStationDAL
                dal.Update(Me.Row)
                'Reload the Data from the DB
                If Me.Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Me.Id
                    Me.Dataset = New Dataset
                    Me.Row = Nothing
                    Me.Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    Public Overrides ReadOnly Property IsDirty() As Boolean
        Get
            Return MyBase.IsDirty
        End Get
    End Property

    Public Sub DeleteAndSave()
        Me.CheckDeleted()
        Me.BeginEdit()
        Try
            Me.Delete()
            Me.Save()
        Catch ex As Exception
            Me.cancelEdit()
            Throw ex
        End Try
    End Sub

    Public Sub Copy(ByVal original As PoliceStation)
        If Not Me.IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing police station")
        End If
        'Copy myself
        Me.CopyFrom(original)

        'copy the children       

        'Me.AddressId = Guid.Empty
        'Me.Address.CopyFrom(original.Address)

    End Sub
#End Region

#Region "DataView Retrieveing Methods"
#Region "PoliceStationSearchDV"
    Public Class PoliceStationSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_POLICE_STATION_ID As String = "police_station_id"
        Public Const COL_POLICE_STATION_CODE As String = "police_station_code"
        Public Const COL_POLICE_STATION_NAME As String = "police_station_name"
        Public Const COL_POLICE_STATION As String = "police_station"
        Public Const COL_COUNTRY_ID As String = "country_id"
#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region

    Public Shared Function getList(ByVal descriptionMask As String, ByVal codeMask As String, ByVal CountryMask As Guid) As PoliceStationSearchDV
        Try
            Dim dal As New PoliceStationDAL
            Dim compIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Return New PoliceStationSearchDV(dal.LoadList(descriptionMask, codeMask, CountryMask).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#End Region
End Class







