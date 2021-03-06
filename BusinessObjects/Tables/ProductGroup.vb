﻿'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (09/16/2008)  ********************
'Namespace Table
Public Class ProductGroup
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
    Public Sub New(ByVal id As Guid, ByVal familyDS As Dataset)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As Dataset)
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
            Dim dal As New ProductGroupDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_PRODUCT_GROUP_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_PRODUCT_GROUP_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_PRODUCT_GROUP_NAME).Rows.Add(newRow)
            Me.Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New ProductGroupDAL
            If Me._isDSCreator Then
                If Not Me.Row Is Nothing Then
                    Me.Dataset.Tables(dal.TABLE_PRODUCT_GROUP_NAME).Rows.Remove(Me.Row)
                End If
            End If
            Me.Row = Nothing
            If Me.Dataset.Tables.IndexOf(dal.TABLE_PRODUCT_GROUP_NAME) >= 0 Then
                Me.Row = Me.FindRow(id, dal.TABLE_KEY_NAME, Me.Dataset.Tables(dal.TABLE_PRODUCT_GROUP_NAME))
            End If
            If Me.Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.Load(Me.Dataset, id)
                Me.Row = Me.FindRow(id, dal.TABLE_KEY_NAME, Me.Dataset.Tables(dal.TABLE_PRODUCT_GROUP_NAME))
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
            If Row(ProductGroupDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductGroupDAL.COL_NAME_PRODUCT_GROUP_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property DealerId() As Guid
        Get
            CheckDeleted()
            If Row(ProductGroupDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductGroupDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ProductGroupDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=200)> _
    Public Property Description() As String
        Get
            CheckDeleted()
            If Row(ProductGroupDAL.COL_NAME_PRODUCT_GROUP_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ProductGroupDAL.COL_NAME_PRODUCT_GROUP_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ProductGroupDAL.COL_NAME_PRODUCT_GROUP_NAME, Value)
        End Set
    End Property

    Dim _Route As Route
    Public ReadOnly Property moRoute() As Route
        Get
            If (_Route Is Nothing) Then
                _Route = New Route(Me.Id, Nothing)
            End If

            Return (_Route)
        End Get

    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New ProductGroupDAL
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

    Public Overrides ReadOnly Property IsDirty() As Boolean
        Get
            Return MyBase.IsFamilyDirty
        End Get
    End Property

    Public Sub Copy(ByVal original As ProductGroup)
        If Not Me.IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Product Group")
        End If
        'Copy myself
        Me.CopyFrom(original)

    End Sub

#End Region

#Region "Children Related"

    Public Sub AttachProductcodes(ByVal selectedProductCodeStrCollection As ArrayList)
        Dim pgPcIdStr As String
        For Each pgPcIdStr In selectedProductCodeStrCollection
            Dim pgPcBO As ProductGroupPrc = New ProductGroupPrc(Me.Dataset)
            If Not pgPcIdStr Is Nothing Then
                pgPcBO.ProductGroupId = Me.Id
                pgPcBO.ProductCodeId = New Guid(pgPcIdStr)
                pgPcBO.Save()
            End If
        Next
    End Sub

    Public Sub DetachProductCodes(ByVal selectedProductCodeGuidStrCollection As ArrayList)
        Dim pgPcIdStr As String
        For Each pgPcIdStr In selectedProductCodeGuidStrCollection
            Dim pgPcBO As ProductGroupPrc = New ProductGroupPrc(Me.Dataset, Me.Id, New Guid(pgPcIdStr))
            If Not pgPcBO Is Nothing Then
                pgPcBO.Delete()
                pgPcBO.Save()
            End If
        Next
    End Sub

    Public Shared Function GetAvailableProductCodes(ByVal dealerId As Guid, Optional ByVal ds As DataSet = Nothing) As DataSet
        If ds Is Nothing Then
            ds = New DataSet
        End If
        Dim pgDAL As ProductGroupDAL = New ProductGroupDAL
        pgDAL.LoadAvailableProductCodes(ds, dealerId)
        Return ds
    End Function


    Public Shared Function GetSelectedProductCodes(ByVal dealerId As Guid, Optional ByVal ds As DataSet = Nothing) As DataSet
        If ds Is Nothing Then
            ds = New DataSet
        End If
        Dim pgDAL As ProductGroupDAL = New ProductGroupDAL
        pgDAL.LoadSelectedProductCodes(ds, dealerId)
        Return ds
    End Function

    Public Sub DeleteAndSave()
        Me.CheckDeleted()
        Me.BeginEdit()
        Try
            Me.Delete()
            Me.Save()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Me.cancelEdit()
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Sub

#End Region

#Region "DataView Retrieving Methods"

    Public Shared Function getList(ByVal groupName As String, ByVal dealerID As Guid, ByVal productCodeId As String, ByVal riskTypeId As String) As ProductGroupSearchDV
        Try
            Dim dal As New ProductGroupDAL
            Return New ProductGroupSearchDV(dal.LoadList(Authentication.CompIds, dealerID, groupName, productCodeId, riskTypeId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function


    Public Class ProductGroupSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_NAME_DESCRIPTION As String = ProductGroupDAL.COL_NAME_PRODUCT_GROUP_NAME
        Public Const COL_NAME_DEALER As String = ProductGroupDAL.COL_NAME_DEALER
        Public Const COL_NAME_DEALER_NAME As String = ProductGroupDAL.COL_NAME_DEALER_NAME
        Public Const COL_NAME_PRODUCT_GROUP_ID As String = ProductGroupDAL.COL_NAME_PRODUCT_GROUP_ID
#End Region

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property ProductGroupId(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_PRODUCT_GROUP_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property Description(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_DESCRIPTION).ToString
            End Get
        End Property

        Public Shared ReadOnly Property DealerCodeAndName(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_DEALER).ToString + "-" + row(COL_NAME_DEALER_NAME)
            End Get
        End Property


    End Class
#End Region

End Class
'End Namespace



