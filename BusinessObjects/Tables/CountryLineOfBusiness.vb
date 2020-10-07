'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (10/17/2018)  ********************

Imports System.Collections.Generic

Public Class CountryLineOfBusiness
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
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
    Public Sub New(ByVal id As Guid, ByVal familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load()
    End Sub

    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New CountryLineOfBusinessDAL
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

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New CountryLineOfBusinessDAL
            If _isDSCreator Then
                If Not Row Is Nothing Then
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
#Region "Constants"
    Private Const ContractTableName As String = ContractDAL.TABLE_NAME
#End Region

#Region "Private Members"
    'Initialization code for new objects
    Private Sub Initialize()
    End Sub
#End Region


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid
        Get
            If Row(CountryLineOfBusinessDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CountryLineOfBusinessDAL.COL_NAME_COUNTRY_LINE_OF_BUSINESS_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")>
    Public Property CountryId As Guid
        Get
            CheckDeleted()
            If Row(CountryLineOfBusinessDAL.COL_NAME_COUNTRY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CountryLineOfBusinessDAL.COL_NAME_COUNTRY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CountryLineOfBusinessDAL.COL_NAME_COUNTRY_ID, Value)
        End Set
    End Property

    <ValueMandatory("", Message:="Line of Business Code is required."), ValidStringLength("", Max:=15, Message:="Line of Business Code can be max 15 chars.")>
    Public Property Code As String
        Get
            CheckDeleted()
            If Row(CountryLineOfBusinessDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CountryLineOfBusinessDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CountryLineOfBusinessDAL.COL_NAME_CODE, Value)
        End Set
    End Property

    <ValueMandatory("", Message:="Line of Business Description is required."), ValidStringLength("", Max:=100, Message:="Description should be between 1 to 100 chars.")>
    Public Property Description As String
        Get
            CheckDeleted()
            If Row(CountryLineOfBusinessDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CountryLineOfBusinessDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CountryLineOfBusinessDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property
    <ValueMandatory("", Message:="Business Type is required.")>
    Public Property LineOfBusinessId As Guid
        Get
            CheckDeleted()
            If Row(CountryLineOfBusinessDAL.COL_NAME_LINE_OF_BUSINESS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CountryLineOfBusinessDAL.COL_NAME_LINE_OF_BUSINESS_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CountryLineOfBusinessDAL.COL_NAME_LINE_OF_BUSINESS_ID, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=1)>
    Public Property InUse As String
        Get
            CheckDeleted()
            If Row(CountryLineOfBusinessDAL.COL_NAME_IN_USE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CountryLineOfBusinessDAL.COL_NAME_IN_USE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CountryLineOfBusinessDAL.COL_NAME_IN_USE, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New CountryLineOfBusinessDAL
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
    Public Sub SaveWithoutCheckDsCreator()
        Try
            MyBase.Save()
            If IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New CountryLineOfBusinessDAL
                dal.Update(Row)
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Id
                    Dataset = New DataSet
                    Row = Nothing
                    Load(objId)
                End If
            End If
        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function GetCountryLineOfBusinessList(countryId As Guid) As List(Of CountryLineOfBusiness)
        Dim dal As New CountryLineOfBusinessDAL
        Dim ds As DataSet = dal.LoadList(countryId)
        Dim dsList As New List(Of CountryLineOfBusiness)
        For Each dr As DataRow In ds.Tables(0).Rows
            dsList.Add(New CountryLineOfBusiness(dr))
        Next
        Return dsList
    End Function
    Public Shared Function GetUsedLineOfBusinessInContracts(ByVal countryId As Guid) As List(Of Guid)

        Dim dal As New CountryLineOfBusinessDAL
        Dim dsList As New List(Of Guid)
        Dim ds As DataSet = dal.GetLineOfBusinessUsedInContracts(countryId)

        For Each dr As DataRow In ds.Tables(0).Rows
            dsList.Add(New Guid(CType(dr("line_of_business_id"), Byte())))
        Next

        Return dsList

    End Function
#End Region

End Class
