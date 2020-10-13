'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (2/27/2007)  ********************
Public Class PoliceStation
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(id As Guid)
        MyBase.New()
        Dataset = New DataSet
        Load(id)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Dataset = New DataSet
        Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(id As Guid, familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load()
    End Sub

    Public Sub New(row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New PoliceStationDAL
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
            Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(id As Guid)
        Try
            Dim dal As New PoliceStationDAL
            If _isDSCreator Then
                If Row IsNot Nothing Then
                    Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Row)
                End If
            End If
            Row = Nothing
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Row = FindRow(id, dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.Load(Dataset, id)
                Row = FindRow(id, dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then
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
    Public Const COL_POLICE_STATION_DISTRICT_CODE As String = "POLICE_STATION_DISTRICT_CODE"
    Public Const COL_POLICE_STATION_DISTRICT_NAME As String = "POLICE_STATION_DISTRICT_NAME"
    Public Const WILDCARD_CHAR As Char = "%"
    Public Const ASTERISK As Char = "*"
    Private Const DSNAME As String = "LIST"

#End Region

#Region "Variables"

#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid
        Get
            If Row(PoliceStationDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PoliceStationDAL.COL_NAME_POLICE_STATION_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")>
    Public Property CountryId As Guid
        Get
            CheckDeleted()
            If Row(PoliceStationDAL.COL_NAME_COUNTRY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PoliceStationDAL.COL_NAME_COUNTRY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PoliceStationDAL.COL_NAME_COUNTRY_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=15)>
    Public Property PoliceStationCode As String
        Get
            CheckDeleted()
            If Row(PoliceStationDAL.COL_NAME_POLICE_STATION_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(PoliceStationDAL.COL_NAME_POLICE_STATION_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PoliceStationDAL.COL_NAME_POLICE_STATION_CODE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=200)>
    Public Property PoliceStationName As String
        Get
            CheckDeleted()
            If Row(PoliceStationDAL.COL_NAME_POLICE_STATION_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(PoliceStationDAL.COL_NAME_POLICE_STATION_NAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PoliceStationDAL.COL_NAME_POLICE_STATION_NAME, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=15), DistrictCodeValidation("")>
    Public Property PoliceStationDistrictCode As String
        Get
            CheckDeleted()
            If Row(PoliceStationDAL.COL_NAME_POLICE_STATION_DISTRICT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(PoliceStationDAL.COL_NAME_POLICE_STATION_DISTRICT_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PoliceStationDAL.COL_NAME_POLICE_STATION_DISTRICT_CODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200), DistrictNameValidation("")>
    Public Property PoliceStationDistrictName As String
        Get
            CheckDeleted()
            If Row(PoliceStationDAL.COL_NAME_POLICE_STATION_DISTRICT_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(PoliceStationDAL.COL_NAME_POLICE_STATION_DISTRICT_NAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PoliceStationDAL.COL_NAME_POLICE_STATION_DISTRICT_NAME, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=200)>
    Public Property Address1 As String
        Get
            CheckDeleted()
            If Row(PoliceStationDAL.COL_NAME_ADDRESS1) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(PoliceStationDAL.COL_NAME_ADDRESS1), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PoliceStationDAL.COL_NAME_ADDRESS1, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200)>
    Public Property Address2 As String
        Get
            CheckDeleted()
            If Row(PoliceStationDAL.COL_NAME_ADDRESS2) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(PoliceStationDAL.COL_NAME_ADDRESS2), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PoliceStationDAL.COL_NAME_ADDRESS2, Value)
        End Set
    End Property

    'Added for Def-1598
    <ValidStringLength("", Max:=200)>
    Public Property Address3 As String
        Get
            CheckDeleted()
            If Row(PoliceStationDAL.COL_NAME_ADDRESS3) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(PoliceStationDAL.COL_NAME_ADDRESS3), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PoliceStationDAL.COL_NAME_ADDRESS3, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=200)>
    Public Property City As String
        Get
            CheckDeleted()
            If Row(PoliceStationDAL.COL_NAME_CITY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(PoliceStationDAL.COL_NAME_CITY), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PoliceStationDAL.COL_NAME_CITY, Value)
        End Set
    End Property

    Public Property RegionId As Guid
        Get
            CheckDeleted()
            If Row(PoliceStationDAL.COL_NAME_REGION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PoliceStationDAL.COL_NAME_REGION_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PoliceStationDAL.COL_NAME_REGION_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=40)>
    Public Property PostalCode As String
        Get
            CheckDeleted()
            If Row(PoliceStationDAL.COL_NAME_POSTAL_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(PoliceStationDAL.COL_NAME_POSTAL_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PoliceStationDAL.COL_NAME_POSTAL_CODE, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New PoliceStationDAL
                dal.Update(Row)
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Id
                    Dataset = New DataSet
                    Row = Nothing
                    Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    Public Overrides ReadOnly Property IsDirty As Boolean
        Get
            Return MyBase.IsDirty
        End Get
    End Property

    Public Sub DeleteAndSave()
        CheckDeleted()
        BeginEdit()
        Try
            Delete()
            Save()
        Catch ex As Exception
            cancelEdit()
            Throw ex
        End Try

    End Sub

    Public Sub Copy(original As PoliceStation)
        If Not IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing police station")
        End If
        'Copy myself
        CopyFrom(original)

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
        Public Const COL_POLICE_STATION_DISTRICT_CODE As String = "police_station_district_code"
        Public Const COL_POLICE_STATION_DISTRICT_NAME As String = "police_station_district_name"
        Public Const COL_POLICE_STATION As String = "police_station"
        Public Const COL_COUNTRY_ID As String = "country_id"
#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region

    Public Shared Function getList(descriptionMask As String, codeMask As String, CountryMask As Guid) As PoliceStationSearchDV
        Try
            Dim dal As New PoliceStationDAL
            Dim compIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Return New PoliceStationSearchDV(dal.LoadList(descriptionMask, codeMask, CountryMask).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#End Region

#Region "Custom Validation"
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class DistrictCodeValidation
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Messages.VALUE_MANDATORY_ERR)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As PoliceStation = CType(objectToValidate, PoliceStation)
            If (String.IsNullOrEmpty(obj.PoliceStationDistrictCode) AndAlso Not String.IsNullOrEmpty(obj.PoliceStationDistrictName)) OrElse (Not String.IsNullOrEmpty(obj.PoliceStationDistrictCode) AndAlso String.IsNullOrEmpty(obj.PoliceStationDistrictName)) Then
                If String.IsNullOrEmpty(obj.PoliceStationDistrictCode) Then
                    Message = "Please enter District Code."
                    Return False
                End If
            End If
            Return True

        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class DistrictNameValidation
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Messages.VALUE_MANDATORY_ERR)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As PoliceStation = CType(objectToValidate, PoliceStation)
            If (String.IsNullOrEmpty(obj.PoliceStationDistrictCode) AndAlso Not String.IsNullOrEmpty(obj.PoliceStationDistrictName)) OrElse (Not String.IsNullOrEmpty(obj.PoliceStationDistrictCode) AndAlso String.IsNullOrEmpty(obj.PoliceStationDistrictName)) Then
                If String.IsNullOrEmpty(obj.PoliceStationDistrictName) Then
                    Message = "Please enter District Name."
                    Return False
                End If
            End If
            Return True
        End Function
    End Class
#End Region
End Class







