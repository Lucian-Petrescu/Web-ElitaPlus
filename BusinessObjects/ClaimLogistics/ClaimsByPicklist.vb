﻿Imports System.Text.RegularExpressions

Public Class ClaimsByPicklist
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const DATA_COL_NAME_PICK_LIST_NUMBER As String = "pick_list_number"
    Public Const DATA_COL_NAME_STORE_NUMBER As String = "store_number"
    Public Const DATA_COL_NAME_SERVICE_CENTER_CODE As String = "service_center_code"
    Private Const TABLE_NAME As String = "ClaimsByPicklist"
    Private Const TABLE_RESULT As String = "RESULT"
    Private Const VALUE_OK As String = "OK"
    Private Const INVALID_PICKLIST_NUMBER As String = "INVALID_PICKLIST_NUMBER_ERR"
    Private Const INVALID_SERVICE_CENTER_CODE As String = "INVALID_SERVICE_CENTER_ERR"
    Private Const INVALID_STORE_NUMBER As String = "INVALID_STORE_NUMBER_ERR"
    Private Const DATASET_NAME As String = "ClaimsByPicklist"
#End Region

#Region "Constructors"

    Public Sub New(ByVal ds As ClaimsByPicklistDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

    End Sub

#End Region

#Region "Private Members"
    Private _serviceCenterId As Guid = Guid.Empty
    Private _storeServiceCenterId As Guid = Guid.Empty
    Private _headerId As Guid = Guid.Empty

    Private Sub MapDataSet(ByVal ds As ClaimsByPicklistDs)

        Dim schema As String = ds.GetXmlSchema

        Dim t As Integer
        Dim i As Integer

        For t = 0 To ds.Tables.Count - 1
            For i = 0 To ds.Tables(t).Columns.Count - 1
                ds.Tables(t).Columns(i).ColumnName = ds.Tables(t).Columns(i).ColumnName.ToUpper
            Next
        Next

        Me.Dataset = New DataSet
        Me.Dataset.ReadXmlSchema(XMLHelper.GetXMLStream(schema))

    End Sub

    'Initialization code for new objects
    Private Sub Initialize()
    End Sub

    Private Sub Load(ByVal ds As ClaimsByPicklistDs)
        Try
            Initialize()
            Dim newRow As DataRow = Me.Dataset.Tables(TABLE_NAME).NewRow
            Me.Row = newRow
            PopulateBOFromWebService(ds)
            Me.Dataset.Tables(TABLE_NAME).Rows.Add(newRow)
        Catch ex As BOValidationException
            Throw ex
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("ClaimsByPicklist Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ByVal ds As ClaimsByPicklistDs)
        Try
            If ds.ClaimsByPicklist.Count = 0 Then Exit Sub
            With ds.ClaimsByPicklist.Item(0)
                Me.PickListNumber = .PICK_LIST_NUMBER

                If Not .IsSTORE_NUMBERNull Then
                    Me.StoreNumber = .STORE_NUMBER
                End If

                If Not .IsSERVICE_CENTER_CODENull Then
                    Me.ServiceCenterCode = .SERVICE_CENTER_CODE
                End If

            End With
        Catch ex As BOValidationException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("ClaimsByPicklist Invalid Parameters Error", Common.ErrorCodes.BO_INVALID_DATA, ex)
        End Try
    End Sub

    Protected Shadows Sub CheckDeleted()
    End Sub

#End Region

#Region "Properties"

    Public Property PickListNumber() As String
        Get
            If Row(Me.DATA_COL_NAME_PICK_LIST_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(Me.DATA_COL_NAME_PICK_LIST_NUMBER), String))
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_PICK_LIST_NUMBER, Value)
        End Set
    End Property

    Public Property StoreNumber() As String
        Get
            If Row(Me.DATA_COL_NAME_STORE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(Me.DATA_COL_NAME_STORE_NUMBER), String))
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_STORE_NUMBER, Value)
        End Set
    End Property

    Public Property ServiceCenterCode() As String
        Get
            If Row(Me.DATA_COL_NAME_SERVICE_CENTER_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(Me.DATA_COL_NAME_SERVICE_CENTER_CODE), String))
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_SERVICE_CENTER_CODE, Value)
        End Set
    End Property

    Public ReadOnly Property HeaderID() As Guid
        Get
            If Me._headerId.Equals(Guid.Empty) Then

                Dim dvPicklist As DataView = LookupListNew.GetPicklistLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

                If Not dvPicklist Is Nothing AndAlso dvPicklist.Count > 0 Then
                    Me._headerId = LookupListNew.GetIdFromCode(dvPicklist, Me.PickListNumber)

                    If Me._headerId.Equals(Guid.Empty) Then
                        Throw New BOValidationException("ClaimsByPicklist Error: ", INVALID_PICKLIST_NUMBER)
                    End If
                Else
                    Throw New BOValidationException("ClaimsByPicklist Error: ", INVALID_PICKLIST_NUMBER)
                End If

            End If

            Return Me._headerId
        End Get
    End Property

    Public ReadOnly Property ServiceCenterID() As Guid
        Get
            If Me._serviceCenterId.Equals(Guid.Empty) AndAlso Not Me.ServiceCenterCode Is Nothing AndAlso Me.ServiceCenterCode <> "" Then

                Dim dvServiceCenter As DataView = LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Countries)

                If Not dvServiceCenter Is Nothing AndAlso dvServiceCenter.Count > 0 Then
                    Me._serviceCenterId = LookupListNew.GetIdFromCode(dvServiceCenter, Me.ServiceCenterCode)

                    If Me._serviceCenterId.Equals(Guid.Empty) Then
                        Throw New BOValidationException("ClaimsByPicklist Error: ", INVALID_SERVICE_CENTER_CODE)
                    End If
                Else
                    Throw New BOValidationException("ClaimsByPicklist Error: ", INVALID_SERVICE_CENTER_CODE)
                End If

            End If

            Return Me._serviceCenterId
        End Get
    End Property

    Public ReadOnly Property StoreServiceCenterID() As Guid
        Get
            If Me._storeServiceCenterId.Equals(Guid.Empty) AndAlso Not Me.StoreNumber Is Nothing AndAlso Me.StoreNumber <> "" Then

                Dim dvStore As DataView = LookupListNew.GetStoreLookupList(ElitaPlusIdentity.Current.ActiveUser.Countries)

                If Not dvStore Is Nothing AndAlso dvStore.Count > 0 Then
                    Me._storeServiceCenterId = LookupListNew.GetIdFromCode(dvStore, Me.StoreNumber)

                    If Me._storeServiceCenterId.Equals(Guid.Empty) Then
                        Throw New BOValidationException("ClaimsByPicklist Error: ", INVALID_STORE_NUMBER)
                    End If
                Else
                    Throw New BOValidationException("ClaimsByPicklist Error: ", INVALID_STORE_NUMBER)
                End If

            End If

            Return Me._storeServiceCenterId
        End Get
    End Property



#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String
        Try
            Me.Validate()

            Dim dsHeader As DataSet = PickupListHeader.GetClaimsByPickList(Me.HeaderID, Me.StoreServiceCenterID, Me.ServiceCenterID)
            dsHeader.DataSetName = Me.DATASET_NAME

            Dim excludeTags As ArrayList = New ArrayList()
            excludeTags.Add("/ClaimsByPicklist/PICKLIST/ROUTE_ID")
            excludeTags.Add("/ClaimsByPicklist/PICKLIST/STORE/ROUTE_ID")
            excludeTags.Add("/ClaimsByPicklist/PICKLIST/STORE/STORE_SERVICE_CENTER_ID")
            excludeTags.Add("/ClaimsByPicklist/PICKLIST/STORE/SERVICE_CENTER/SERVICE_CENTER_ID")
            excludeTags.Add("/ClaimsByPicklist/PICKLIST/STORE/SERVICE_CENTER/STORE_SERVICE_CENTER_ID")
            excludeTags.Add("/ClaimsByPicklist/PICKLIST/STORE/SERVICE_CENTER/CLAIM/SERVICE_CENTER_ID")
            excludeTags.Add("/ClaimsByPicklist/PICKLIST/STORE/SERVICE_CENTER/CLAIM/STORE_SERVICE_CENTER_ID")

            Return XMLHelper.FromDatasetToXML(dsHeader, excludeTags, True)

        Catch ex As BOValidationException
            Throw ex
        Catch ex As StoredProcedureGeneratedException
            Throw ex
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As Exception
            Throw ex
            'Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

#End Region

#Region "Extended Properties"

#End Region

End Class
