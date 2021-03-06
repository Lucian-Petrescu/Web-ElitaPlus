﻿'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (9/17/2008)  ********************

Public Class ProductGroupPrc
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
    Public Sub New(ByVal familyDS As DataSet, ByVal Id As Guid, ByVal ServCentId As Guid)
        MyBase.New(False)
        Me.Dataset = familyDS
        LoadByProductGroupIdProductCodeId(Id, ServCentId)
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
            Dim dal As New ProductGroupPrcDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New ProductGroupPrcDAL
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


    Protected Sub LoadByProductGroupIdProductCodeId(ByVal productGroupId As Guid, ByVal productCodeId As Guid)
        Try
            Dim dal As New ProductGroupPrcDAL

            If Me._isDSCreator Then
                If Not Me.Row Is Nothing Then
                    Me.Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Me.Row)
                End If
            End If
            Me.Row = Nothing
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Me.Row = Me.FindRow(productGroupId, dal.COL_NAME_PRODUCT_GROUP_ID, Me.Dataset.Tables(dal.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.LoadByProductGroupIdProductCodeId(Me.Dataset, productGroupId, productCodeId)
                Me.Row = Me.FindRow(productCodeId, dal.COL_NAME_PRODUCT_CODE_ID, Me.Dataset.Tables(dal.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then
                Throw New DataNotFoundException
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

#End Region

#Region "Variables"
    Private moProductCodeIDs As ArrayList
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
            If Row(ProductGroupPrcDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductGroupPrcDAL.COL_NAME_PRODUCT_GROUP_DETAIL_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property ProductGroupId() As Guid
        Get
            CheckDeleted()
            If Row(ProductGroupPrcDAL.COL_NAME_PRODUCT_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductGroupPrcDAL.COL_NAME_PRODUCT_GROUP_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ProductGroupPrcDAL.COL_NAME_PRODUCT_GROUP_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ProductCodeId() As Guid
        Get
            CheckDeleted()
            If Row(ProductGroupPrcDAL.COL_NAME_PRODUCT_CODE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductGroupPrcDAL.COL_NAME_PRODUCT_CODE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ProductGroupPrcDAL.COL_NAME_PRODUCT_CODE_ID, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New ProductGroupPrcDAL
                dal.Update(Me.Row)
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
#End Region

#Region "DataView Retrieveing Methods"

#End Region
#Region "Extended Functionality"

    Public Shared Function GetProductGroupIDs(ByVal oProductGroupId As Guid) As DataView
        Dim dal As New ProductGroupPrcDAL
        Dim ds As DataSet

        ds = dal.LoadGroupProductCodeIDs(oProductGroupId)
        Return ds.Tables(dal.TABLE_NAME).DefaultView
    End Function

    Public Shared Function GetAllProductGroupIDs(ByVal dealerID As Guid) As DataView
        Dim dal As New ProductGroupPrcDAL
        Dim ds As DataSet

        ds = dal.LoadAllGroupProductCodeIDs(dealerID)
        Return ds.Tables(dal.TABLE_NAME).DefaultView
    End Function

    Public Function ProductCodeIDs(ByVal isNetwork As Boolean, ByVal dealerID As Guid) As ArrayList
        Dim oPrcDv As DataView
        If moProductCodeIDs Is Nothing Then
            If isNetwork Then
                oPrcDv = GetProductGroupIDs(Me.ProductGroupId)
            Else
                oPrcDv = GetAllProductGroupIDs(dealerID)
            End If

            moProductCodeIDs = New ArrayList

            If oPrcDv.Table.Rows.Count > 0 Then
                Dim index As Integer
                ' Create Array
                For index = 0 To oPrcDv.Table.Rows.Count - 1
                    If Not oPrcDv.Table.Rows(index)(ProductGroupPrcDAL.COL_NAME_PRODUCT_CODE_ID) Is System.DBNull.Value Then
                        moProductCodeIDs.Add(New Guid(CType(oPrcDv.Table.Rows(index)(ProductGroupPrcDAL.COL_NAME_PRODUCT_CODE_ID), Byte())))
                    End If
                Next
            End If
        End If
        Return moProductCodeIDs
    End Function
    '----------

#End Region
End Class






