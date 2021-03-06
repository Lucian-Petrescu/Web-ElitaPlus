﻿'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (7/16/2008)  ********************

Public Class Route
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
            Dim dal As New RouteDAL
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
            Dim dal As New RouteDAL
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

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If row(RouteDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(RouteDAL.COL_NAME_ROUTE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=2000)> _
    Public Property Description() As String
        Get
            CheckDeleted()
            If row(RouteDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(RouteDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(RouteDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=200)> _
    Public Property Code() As String
        Get
            CheckDeleted()
            If row(RouteDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(RouteDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(RouteDAL.COL_NAME_CODE, Value)
        End Set
    End Property


    '<ValueMandatory("")> _
    'Public Property CountryId() As Guid
    '    Get
    '        CheckDeleted()
    '        If row(RouteDAL.COL_NAME_COUNTRY_ID) Is DBNull.Value Then
    '            Return Nothing
    '        Else
    '            Return New Guid(CType(row(RouteDAL.COL_NAME_COUNTRY_ID), Byte()))
    '        End If
    '    End Get
    '    Set(ByVal Value As Guid)
    '        CheckDeleted()
    '        Me.SetValue(RouteDAL.COL_NAME_COUNTRY_ID, Value)
    '    End Set
    'End Property



    '<ValueMandatory("")> _
    Public Property ServiceNetworkId() As Guid
        Get
            CheckDeleted()
            If Row(RouteDAL.COL_NAME_SERVICE_NETWORK_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(RouteDAL.COL_NAME_SERVICE_NETWORK_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(RouteDAL.COL_NAME_SERVICE_NETWORK_ID, Value)
        End Set
    End Property

    Public Overrides ReadOnly Property IsDirty() As Boolean
        Get
            Return MyBase.IsDirty OrElse Me.IsChildrenDirty
        End Get
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsFamilyDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New RouteDAL
                dal.UpdateFamily(Me.Dataset) 'New Code Added Manually
                'Reload the Data from the DB
                If Me.Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Me.Id
                    Me.Dataset = New DataSet
                    Me.Row = Nothing
                    Me.Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    'Public Sub Copy(ByVal original As Route)
    '    If Not Me.IsNew Then
    '        Throw New BOInvalidOperationException("You cannot copy into an existing Service Center")
    '    End If
    '    'Copy myself
    '    Me.CopyFrom(original)

    '    'copy the children       
    '    'Manufacturers
    '    Dim selSvrDv As DataView = original.GetSelectedServiceCenters
    '    Dim selSvrList As New ArrayList
    '    Dim i As Integer = 0
    '    For i = 0 To selSvrDv.Count - 1
    '        selSvrList.Add(New Guid(CType(selSvrDv(i)(LookupListNew.COL_ID_NAME), Byte())).ToString)
    '    Next
    '    Me.AttachServiceCenters(selSvrList)

    'End Sub

#End Region

#Region "Children Related"

    'Public ReadOnly Property ServiceNetworkSvcChildren() As ServiceNetworkSVCList
    '    Get
    '        Return New ServiceNetworkSVCList(Me)
    '    End Get
    'End Property

    Public Sub AttachServiceCenters(ByVal selectedServiceCenterGuidStrCollection As ArrayList)
        Dim routeSrvIdStr As String
        For Each routeSrvIdStr In selectedServiceCenterGuidStrCollection
            Dim routeSrvBO As ServiceCenter = New ServiceCenter(New Guid(routeSrvIdStr), Me.Dataset)
            routeSrvBO.RouteId = Me.Id
            routeSrvBO.Save()
        Next
    End Sub

    Public Sub DetachServiceCenters(ByVal selectedServiceCenterGuidStrCollection As ArrayList)
        Dim routeSrvIdStr As String
        For Each routeSrvIdStr In selectedServiceCenterGuidStrCollection
            Dim routeSrvBO As ServiceCenter = New ServiceCenter(New Guid(routeSrvIdStr), Me.Dataset)
            routeSrvBO.RouteId = Guid.Empty
            routeSrvBO.Save()
        Next
    End Sub

    Public Shared Function GetAvailableSCs(ByVal scNetworkId As Guid, Optional ByVal ds As DataSet = Nothing) As DataSet
        If ds Is Nothing Then
            ds = New DataSet
        End If
        Dim cpDAL As RouteDAL = New RouteDAL
        cpDAL.LoadAvailableSCs(ds, scNetworkId)
        Return ds
    End Function


    Public Shared Function GetSelectedSCs(ByVal routeId As Guid, Optional ByVal ds As DataSet = Nothing) As DataSet
        If ds Is Nothing Then
            ds = New DataSet
        End If
        Dim cpDAL As RouteDAL = New RouteDAL
        cpDAL.LoadSelectedSCs(ds, routeId)
        Return ds
    End Function



#End Region

#Region "DataView Retrieveing Methods"

    Public Shared Function getList(ByVal RouteId As Guid, ByVal serviceNetworkId As Guid) As RouteSearchDV
        Try
            Dim dal As New RouteDAL
            'Dim oCompany As New ElitaPlus.BusinessObjectsNew.Company(ElitaPlusIdentity.Current.ActiveUser.CompanyId)

            Return New RouteSearchDV(dal.LoadList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, RouteId, serviceNetworkId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function LoadList(ByVal scNetworkId As Guid) As DataSet
        Try
            Dim dal As New RouteDAL
            Return dal.LoadList(scNetworkId)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetRouteByCode(ByVal routeCode As String) As DataSet

        Try
            Dim dal As New RouteDAL
            Return dal.GetRouteByCode(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, routeCode)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

#End Region

#Region "RouteSearchDV"

    Public Class RouteSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_NAME_DESCRIPTION As String = RouteDAL.COL_NAME_DESCRIPTION
        Public Const COL_NAME_SHORT_DESC As String = RouteDAL.COL_NAME_SHORT_DESC
        Public Const COL_NAME_ROUTE_ID As String = RouteDAL.COL_NAME_ROUTE_ID
        Public Const COL_NAME_SERVICE_NETWORK_CODE = RouteDAL.COL_NAME_SERVICE_NETWORK_CODE
#End Region

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property routeId(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_ROUTE_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property Description(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_DESCRIPTION).ToString
            End Get
        End Property

        Public Shared ReadOnly Property ShortDescription(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_SHORT_DESC).ToString
            End Get
        End Property


    End Class
#End Region
End Class


