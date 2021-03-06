﻿'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (3/13/2014)  ********************

Public Class ExcludeCancReasonByRole
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
            Dim dal As New ExcludeCancreasonByRoleDAL
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
            Dim dal As New ExcludeCancreasonByRoleDAL
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
            If Row(ExcludeCancreasonByRoleDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ExcludeCancreasonByRoleDAL.COL_NAME_EXCLUDE_CANCREASON_ROLE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CancellationReasonId() As Guid
        Get
            CheckDeleted()
            If Row(ExcludeCancreasonByRoleDAL.COL_NAME_CANCELLATION_REASON_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ExcludeCancreasonByRoleDAL.COL_NAME_CANCELLATION_REASON_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ExcludeCancreasonByRoleDAL.COL_NAME_CANCELLATION_REASON_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property RoleId() As Guid
        Get
            CheckDeleted()
            If Row(ExcludeCancreasonByRoleDAL.COL_NAME_ROLE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ExcludeCancreasonByRoleDAL.COL_NAME_ROLE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ExcludeCancreasonByRoleDAL.COL_NAME_ROLE_ID, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New ExcludeCancreasonByRoleDAL
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


    Public Class ExcludeCancReasonByRoleList
        Inherits BusinessObjectListBase

        Public Sub New(ByVal parent As CancellationReason)
            MyBase.New(LoadTable(parent), GetType(ExcludeCancReasonByRole), parent)
        End Sub


        Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
            Return CType(bo, ExcludeCancReasonByRole).CancellationReasonId.Equals(CType(Parent, CancellationReason).Id)
        End Function

        Public Function Find(ByVal RoleId As Guid) As ExcludeCancReasonByRole
            Dim bo As ExcludeCancReasonByRole
            For Each bo In Me
                If bo.RoleId.Equals(RoleId) Then Return bo
            Next
            Return Nothing
        End Function


#Region "Class Methods"
        Private Shared Function LoadTable(ByVal parent As CancellationReason) As DataTable
            Try
                If Not parent.IsChildrenCollectionLoaded(GetType(ExcludeCancReasonByRoleList)) Then
                    Dim dal As New ExcludeCancReasonByRoleDAL
                    dal.LoadList(parent.Dataset, parent.Id)
                    parent.AddChildrenCollection(GetType(ExcludeCancReasonByRoleList))
                End If
                Return parent.Dataset.Tables(ExcludeCancReasonByRoleDAL.TABLE_NAME)
            Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End Function


#End Region


    End Class


End Class


