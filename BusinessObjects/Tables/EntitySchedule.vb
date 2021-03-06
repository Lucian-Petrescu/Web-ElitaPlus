﻿'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (10/15/2012)  ********************

Public Class EntitySchedule
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const COL_NAME_ENTITY_SCHEDULE_ID As String = EntityScheduleDAL.COL_NAME_ENTITY_SCHEDULE_ID
    Public Const COL_NAME_ENTITY As String = EntityScheduleDAL.COL_NAME_ENTITY
    Public Const COL_NAME_ENTITY_ID As String = EntityScheduleDAL.COL_NAME_ENTITY_ID
    Public Const COL_NAME_SCHEDULE_ID As String = EntityScheduleDAL.COL_NAME_SCHEDULE_ID
    Public Const COL_NAME_EFFECTIVE As String = EntityScheduleDAL.COL_NAME_EFFECTIVE
    Public Const COL_NAME_EXPIRATION As String = EntityScheduleDAL.COL_NAME_EXPIRATION
    Public Const COL_NAME_SCHEDULE_CODE As String = ScheduleDAL.COL_NAME_CODE
    Public Const COL_NAME_DESCRIPTION As String = ScheduleDAL.COL_NAME_DESCRIPTION
    Public Const TABLE_NAME_WORKQUEUE As String = "ELP_WORKQUEUE"
#End Region

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid, ByVal entity As IEffecttiveExpiration)
        MyBase.New()
        Me.EntityObject = entity
        Me.Dataset = New DataSet
        Me.Load(id)
    End Sub

    'New BO
    Public Sub New(ByVal entity As IEffecttiveExpiration)
        MyBase.New()
        Me.EntityObject = entity
        Me.Dataset = New DataSet
        Me.Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDS As DataSet, ByVal entity As IEffecttiveExpiration)
        MyBase.New(False)
        Me.EntityObject = entity
        Me.Dataset = familyDS
        Me.Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As DataSet, ByVal entity As IEffecttiveExpiration)
        MyBase.New(False)
        Me.EntityObject = entity
        Me.Dataset = familyDS
        Me.Load()
    End Sub

    Public Sub New(ByVal row As DataRow, ByVal entity As IEffecttiveExpiration)
        MyBase.New(False)
        Me.EntityObject = entity
        Me.Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New EntityScheduleDAL
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
            Dim dal As New EntityScheduleDAL
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
        If (Me.EntityObject.GetType().Equals(GetType(WorkQueue))) Then
            Dim workQueue As WorkQueue = DirectCast(Me.EntityObject, WorkQueue)
            If (workQueue.WorkQueue.ActiveOn > DateTime.UtcNow) Then
                Me.Effective = workQueue.ConvertTimeFromUtc(workQueue.WorkQueue.ActiveOn)
            Else
                Me.Effective = workQueue.ConvertTimeFromUtc(DateTime.UtcNow)
            End If
            Me.Expiration = workQueue.ConvertTimeFromUtc(workQueue.DEFAULT_EXPIRATION_DATE)
        Else
            Me.Effective = Now
            Me.Expiration = WorkQueue.DEFAULT_EXPIRATION_DATE
        End If

    End Sub

    Private _entityObject As IEffecttiveExpiration
#End Region


#Region "Properties"

    Public Property EntityObject As IEffecttiveExpiration
        Get
            Return _entityObject
        End Get
        Private Set(ByVal value As IEffecttiveExpiration)
            _entityObject = value
        End Set
    End Property

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If Row(EntityScheduleDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(EntityScheduleDAL.COL_NAME_ENTITY_SCHEDULE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=1020)> _
    Public Property Entity() As String
        Get
            CheckDeleted()
            If Row(EntityScheduleDAL.COL_NAME_ENTITY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(EntityScheduleDAL.COL_NAME_ENTITY), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(EntityScheduleDAL.COL_NAME_ENTITY, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property EntityId() As Guid
        Get
            CheckDeleted()
            If Row(EntityScheduleDAL.COL_NAME_ENTITY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(EntityScheduleDAL.COL_NAME_ENTITY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(EntityScheduleDAL.COL_NAME_ENTITY_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ScheduleId() As Guid
        Get
            CheckDeleted()
            If Row(EntityScheduleDAL.COL_NAME_SCHEDULE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(EntityScheduleDAL.COL_NAME_SCHEDULE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(EntityScheduleDAL.COL_NAME_SCHEDULE_ID, Value)
        End Set
    End Property

    Public ReadOnly Property OriginalEffective As Date
        Get
            CheckDeleted()
            If Row(EntityScheduleDAL.COL_NAME_EFFECTIVE, DataRowVersion.Original) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateTimeType(CType(Row(EntityScheduleDAL.COL_NAME_EFFECTIVE, DataRowVersion.Original), Date))
            End If
        End Get
    End Property

    <ValueMandatory(""), DateCompareValidatorAttribute("", Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_EFFECTIVE_HIGHER_EXPIRATION_DATE, _
        "Expiration", DateCompareValidatorAttribute.CompareType.LessThan, DefaultCompareToValue:=DateCompareValidatorAttribute.DefaultType.MaxDate, _
        DefaultCompareValue:=DateCompareValidatorAttribute.DefaultType.MinDate), _
    DateCompareValidator("", Assurant.ElitaPlus.Common.ErrorCodes.BO_ERROR_WQ_SCHEDULE_EFF_LESSER_THAN_WQ_EFF, _
        "EntityEffective", DateCompareValidatorAttribute.CompareType.GreaterThanOrEqual, DefaultCompareToValue:=DateCompareValidatorAttribute.DefaultType.MinDate, _
        DefaultCompareValue:=DateCompareValidatorAttribute.DefaultType.MinDate)> _
    Public Property Effective() As DateTimeType
        Get
            CheckDeleted()
            If Row(EntityScheduleDAL.COL_NAME_EFFECTIVE) Is DBNull.Value Then
                Return Nothing
            Else
                If (Me.EntityObject.GetType().Equals(GetType(WorkQueue))) Then
                    Return New DateTimeType(DirectCast(Me.EntityObject, WorkQueue).ConvertTimeFromUtc(CType(Row(EntityScheduleDAL.COL_NAME_EFFECTIVE), Date)))
                Else
                    Return New DateTimeType(CType(Row(EntityScheduleDAL.COL_NAME_EFFECTIVE), Date))
                End If
            End If
        End Get
        Set(ByVal Value As DateTimeType)
            CheckDeleted()
            If (Not Value Is Nothing) Then
                If (Me.EntityObject.GetType().Equals(GetType(WorkQueue))) Then
                    Value = New DateTimeType(DirectCast(Me.EntityObject, WorkQueue).ConvertTimeToUtc(Value.Value))
                End If
                Value = New DateTimeType(New Date(Value.Value.Year, Value.Value.Month, Value.Value.Day, Value.Value.Hour, Value.Value.Minute, Value.Value.Second))
            End If
            Me.SetValue(EntityScheduleDAL.COL_NAME_EFFECTIVE, Value)
        End Set
    End Property

    <DateCompareValidator("", Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_EFFECTIVE_DATE_SMALLER_THAN_SYSDATE, "", _
            DateCompareValidatorAttribute.CompareType.GreaterThan, CheckWhenNew:=True, CompareToType:=DateCompareValidatorAttribute.CompareToPropertyType.Nothing, _
            DefaultCompareToValue:=DateCompareValidatorAttribute.DefaultType.UtcToday)> _
    Public ReadOnly Property EffectiveUtc() As DateTimeType
        Get
            If Row(EntityScheduleDAL.COL_NAME_EFFECTIVE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateTimeType(CType(Row(EntityScheduleDAL.COL_NAME_EFFECTIVE), Date))
            End If
        End Get
    End Property

    Public ReadOnly Property ExpirationUtc() As DateTimeType
        Get
            If Row(EntityScheduleDAL.COL_NAME_EXPIRATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateTimeType(CType(Row(EntityScheduleDAL.COL_NAME_EXPIRATION), Date))
            End If
        End Get
    End Property

    Public ReadOnly Property EntityEffective As DateTimeType
        Get
            CheckDeleted()
            Return Me.EntityObject.Effective
        End Get
    End Property

    Public ReadOnly Property OriginalExpiration As Date
        Get
            CheckDeleted()
            If Row(EntityScheduleDAL.COL_NAME_EXPIRATION, DataRowVersion.Original) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateTimeType(CType(Row(EntityScheduleDAL.COL_NAME_EXPIRATION, DataRowVersion.Original), Date))
            End If
        End Get
    End Property

    <DateCompareValidatorAttribute("", Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_EFFECTIVE_HIGHER_EXPIRATION_DATE, _
        "Effective", DateCompareValidatorAttribute.CompareType.GreaterThan, DefaultCompareToValue:=DateCompareValidatorAttribute.DefaultType.MinDate, _
        DefaultCompareValue:=DateCompareValidatorAttribute.DefaultType.MaxDate), _
    OverlapValidator("", DataRowPropertyName:="DataRow", DataTablePropertyName:="DataTable", EffectiveDateColumnName:=EntityScheduleDAL.COL_NAME_EFFECTIVE, ExpirationDateColumnName:=EntityScheduleDAL.COL_NAME_EXPIRATION), _
    DateCompareValidatorAttribute("", Assurant.ElitaPlus.Common.ErrorCodes.BO_ERROR_WQ_SCHEDULE_EXP_GREATER_THAN_WQ_EXP, _
        "EntityExpiration", DateCompareValidatorAttribute.CompareType.LessThanOrEqual, DefaultCompareToValue:=DateCompareValidatorAttribute.DefaultType.MaxDate, _
        DefaultCompareValue:=DateCompareValidatorAttribute.DefaultType.MaxDate)> _
    Public Property Expiration() As DateTimeType
        Get
            CheckDeleted()
            If Row(EntityScheduleDAL.COL_NAME_EXPIRATION) Is DBNull.Value Then
                Return Nothing
            Else
                If (Me.EntityObject.GetType().Equals(GetType(WorkQueue))) Then
                    Return New DateTimeType(DirectCast(Me.EntityObject, WorkQueue).ConvertTimeFromUtc(CType(Row(EntityScheduleDAL.COL_NAME_EXPIRATION), Date)))
                Else
                    Return New DateTimeType(CType(Row(EntityScheduleDAL.COL_NAME_EXPIRATION), Date))
                End If
            End If
        End Get
        Set(ByVal Value As DateTimeType)
            CheckDeleted()
            If (Not Value Is Nothing) Then
                If (Me.EntityObject.GetType().Equals(GetType(WorkQueue))) Then
                    Value = New DateTimeType(DirectCast(Me.EntityObject, WorkQueue).ConvertTimeToUtc(Value.Value))
                End If
                Value = New DateTimeType(New Date(Value.Value.Year, Value.Value.Month, Value.Value.Day, Value.Value.Hour, Value.Value.Minute, Value.Value.Second))
            End If
            Me.SetValue(EntityScheduleDAL.COL_NAME_EXPIRATION, Value)
        End Set
    End Property

    Public ReadOnly Property EntityExpiration As DateTimeType
        Get
            CheckDeleted()
            Return Me.EntityObject.Expiration
        End Get
    End Property


    <ValueMandatory("")> _
    Public Property ScheduleCode() As String
        Get
            CheckDeleted()
            If Row(ScheduleDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(EntityScheduleDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(EntityScheduleDAL.COL_NAME_CODE, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property ScheduleDescription() As String
        Get
            CheckDeleted()
            If Row(ScheduleDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(EntityScheduleDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(EntityScheduleDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property

    Public ReadOnly Property DataTable As DataTable
        Get
            Return Me.Row.Table
        End Get
    End Property

    Public ReadOnly Property DataRow As DataRow
        Get
            Return Me.Row
        End Get
    End Property

#End Region

#Region "Public Members"
    Public Shared Function GetList(ByVal scheduleId As Guid, ByVal entityName As String) As DataTable
        Try
            Dim dal As New EntityScheduleDAL
            Dim ds As New DataSet
            dal.LoadList(ds, scheduleId, entityName)
            Return ds.Tables(EntityScheduleDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Function

    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New EntityScheduleDAL
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

    Public Sub Copy(ByVal original As EntitySchedule)
        If Not Me.IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Object.")
        End If
        MyBase.CopyFrom(original)
    End Sub
#End Region

#Region "DataView Retrieveing Methods"

#End Region

    Public Class ScheduleSelectionView
        Inherits DataView
        Public Const COL_NAME_ENTITY_SCHEDULE_ID As String = EntityScheduleDAL.COL_NAME_ENTITY_SCHEDULE_ID
        Public Const COL_NAME_SCHEDULE_CODE As String = ScheduleDAL.COL_NAME_CODE
        Public Const COL_NAME_SCHEDULE_DESCRIPTION As String = ScheduleDAL.COL_NAME_DESCRIPTION
        Public Const COL_NAME_EFFECTIVE As String = EntityScheduleDAL.COL_NAME_EFFECTIVE
        Public Const COL_NAME_EXPIRATION As String = EntityScheduleDAL.COL_NAME_EXPIRATION
        Public Const COL_NAME_SCHEDULE_ID As String = EntityScheduleDAL.COL_NAME_SCHEDULE_ID
        Public Const COL_NAME_IS_NEW As String = "IS_NEW"

        Public Sub New(ByVal Table As DataTable)
            MyBase.New(Table)
        End Sub

        Public Shared Function CreateTable() As DataTable
            Dim t As New DataTable
            t.Columns.Add(COL_NAME_ENTITY_SCHEDULE_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_SCHEDULE_CODE, GetType(String))
            t.Columns.Add(COL_NAME_SCHEDULE_DESCRIPTION, GetType(String))
            t.Columns.Add(COL_NAME_EFFECTIVE, GetType(Date))
            t.Columns.Add(COL_NAME_EXPIRATION, GetType(Date))
            t.Columns.Add(COL_NAME_SCHEDULE_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_IS_NEW, GetType(Boolean))
            Return t
        End Function
    End Class

End Class

Public Class EntityScheduleList
    Inherits BusinessObjectListBase

    Private parentId As Guid
    Private tableName As String

    Public Sub New(ByVal parent As BusinessObjectBase)
        MyBase.New(LoadTable(parent), GetType(EntitySchedule), parent)
        If (parent.GetType().Equals(GetType(WorkQueue))) Then
            tableName = EntitySchedule.TABLE_NAME_WORKQUEUE
            parentId = DirectCast(parent, WorkQueue).Id
        Else
            tableName = String.Empty
            parentId = Guid.Empty
        End If
    End Sub

    Friend Overrides Function GetChild(ByVal row As DataRow) As BusinessObjectBase
        Dim bo As BusinessObjectBase = BOType.GetConstructor(New Type() {GetType(DataRow), GetType(IEffecttiveExpiration)}).Invoke(New Object() {row, CType(Parent, IEffecttiveExpiration)})
        Return bo
    End Function

    Public Overrides Function GetNewChild() As BusinessObjectBase
        Dim bo As BusinessObjectBase = BOType.GetConstructor(New Type() {GetType(DataSet), GetType(IEffecttiveExpiration)}).Invoke(New Object() {Me.Table.DataSet, CType(Parent, IEffecttiveExpiration)})
        Return bo
    End Function

    Public Overrides Function GetChild(ByVal childId As System.Guid) As BusinessObjectBase
        Dim bo As BusinessObjectBase
        Try
            bo = MyBase.BOType.GetConstructor(New Type() {GetType(Guid), GetType(DataSet), GetType(IEffecttiveExpiration)}).Invoke(New Object() {childId, Me.Table.DataSet, CType(Parent, IEffecttiveExpiration)})
        Catch ex As Exception
            If Not ex.InnerException Is Nothing AndAlso ex.InnerException.GetType Is GetType(DataNotFoundException) Then
                Throw ex.InnerException
            Else
                Throw ex
            End If
        End Try
        Return bo
    End Function

    Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
        Return CType(bo, EntitySchedule).EntityId.Equals(parentId)
    End Function

    Private Shared Function LoadTable(ByVal parent As BusinessObjectBase) As DataTable
        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(EntityScheduleList)) Then
                Dim dal As New EntityScheduleDAL
                Dim tableName As String
                Dim parentId As Guid
                If (TypeOf (parent) Is WorkQueue) Then
                    tableName = "ELP_WORKQUEUE"
                    parentId = DirectCast(parent, WorkQueue).Id
                Else
                    tableName = String.Empty
                    parentId = Guid.Empty
                End If
                dal.LoadList(parent.Dataset, tableName, parentId)
                parent.AddChildrenCollection(GetType(EntityScheduleList))
            End If
            Return parent.Dataset.Tables(EntityScheduleDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function AsSelectionView() As EntitySchedule.ScheduleSelectionView
        Dim t As DataTable = EntitySchedule.ScheduleSelectionView.CreateTable
        Dim detail As EntitySchedule

        For Each detail In Me
            Dim row As DataRow = t.NewRow
            row(EntitySchedule.ScheduleSelectionView.COL_NAME_ENTITY_SCHEDULE_ID) = detail.Id.ToByteArray()
            row(EntitySchedule.ScheduleSelectionView.COL_NAME_SCHEDULE_CODE) = detail.ScheduleCode
            row(EntitySchedule.ScheduleSelectionView.COL_NAME_SCHEDULE_DESCRIPTION) = detail.ScheduleDescription
            row(EntitySchedule.ScheduleSelectionView.COL_NAME_EFFECTIVE) = detail.Effective.Value
            row(EntitySchedule.ScheduleSelectionView.COL_NAME_SCHEDULE_ID) = detail.ScheduleId.ToByteArray()
            If (detail.Expiration Is Nothing) Then
                row(EntitySchedule.ScheduleSelectionView.COL_NAME_EXPIRATION) = DBNull.Value
            Else
                row(EntitySchedule.ScheduleSelectionView.COL_NAME_EXPIRATION) = detail.Expiration.Value
            End If
            row(EntitySchedule.ScheduleSelectionView.COL_NAME_IS_NEW) = detail.IsNew
            t.Rows.Add(row)
        Next
        Return New EntitySchedule.ScheduleSelectionView(t)
    End Function
End Class
