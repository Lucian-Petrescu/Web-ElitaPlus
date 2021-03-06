'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (9/5/2007)  ********************

Public Class CommissionPeriodEntity
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
            Dim dal As New CommissionPeriodEntityDAL
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
            Dim dal As New CommissionToleranceDAL
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
            If Me.Row Is Nothing Then
                Throw New DataNotFoundException
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub


    'Protected Sub Load(ByVal periodId As Guid, ByVal EntityId As Guid)
    '    Try
    '        Dim dal As New CommissionPeriodEntityDAL

    '        If Me._isDSCreator Then
    '            If Not Me.Row Is Nothing Then
    '                Me.Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Me.Row)
    '            End If
    '        End If
    '        Me.Row = Nothing
    '        If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
    '            Me.Row = Me.FindRow(EntityId, dal.COL_NAME_ENTITY_ID, Me.Dataset.Tables(dal.TABLE_NAME))
    '        End If
    '        If Me.Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
    '            dal.Load(Me.Dataset, periodId, EntityId)
    '            Me.Row = Me.FindRow(periodId, dal.COL_NAME_COMMISSION_PERIOD_ID, Me.Dataset.Tables(dal.TABLE_NAME))
    '        End If
    '        If Me.Row Is Nothing Then
    '            Throw New DataNotFoundException
    '        End If
    '    Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
    '        Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
    '    End Try
    'End Sub
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
            If row(CommissionPeriodEntityDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CommissionPeriodEntityDAL.COL_NAME_COMMISSION_PERIOD_ENTITY_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CommissionPeriodId() As Guid
        Get
            CheckDeleted()
            If row(CommissionPeriodEntityDAL.COL_NAME_COMMISSION_PERIOD_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CommissionPeriodEntityDAL.COL_NAME_COMMISSION_PERIOD_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CommissionPeriodEntityDAL.COL_NAME_COMMISSION_PERIOD_ID, Value)
        End Set
    End Property


    Public Property EntityId() As Guid
        Get
            CheckDeleted()
            If Row(CommissionPeriodEntityDAL.COL_NAME_ENTITY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CommissionPeriodEntityDAL.COL_NAME_ENTITY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CommissionPeriodEntityDAL.COL_NAME_ENTITY_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property Position() As LongType
        Get
            CheckDeleted()
            If row(CommissionPeriodEntityDAL.COL_NAME_POSITION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(CommissionPeriodEntityDAL.COL_NAME_POSITION), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(CommissionPeriodEntityDAL.COL_NAME_POSITION, Value)
        End Set
    End Property

    Public Property PayeeTypeId() As Guid
        Get
            CheckDeleted()
            If Row(CommissionPeriodEntityDAL.COL_NAME_PAYEE_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CommissionPeriodEntityDAL.COL_NAME_PAYEE_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CommissionPeriodEntityDAL.COL_NAME_PAYEE_TYPE_ID, Value)
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
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New CommissionPeriodEntityDAL
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

    Private Shared Function GetCommPeriodEntityList(ByVal parent As CommissionPeriod) As DataTable

        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(PeriodEntityList)) Then
                Dim dal As New CommissionPeriodEntityDAL
                dal.LoadList(parent.Id, parent.Dataset)
                parent.AddChildrenCollection(GetType(PeriodEntityList))
            End If
            'Return New CovLossList(parent)
            Return parent.Dataset.Tables(CommissionPeriodEntityDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Shared Function getList(ByVal commissionPeriodId As Guid) As SearchDV
        Try
            Dim dal As New CommissionPeriodEntityDAL

            Return New SearchDV(dal.LoadList(commissionPeriodId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#End Region
#Region "SearchDV"

    Public Class SearchDV
        Inherits DataView

#Region "Constants"

        Public Const COL_NAME_COMMISSION_PERIOD_ENTITY_ID As String = CommissionPeriodEntityDAL.COL_NAME_COMMISSION_PERIOD_ENTITY_ID
        Public Const COL_NAME_COMMISSION_PERIOD_ID As String = CommissionPeriodEntityDAL.COL_NAME_COMMISSION_PERIOD_ID
        Public Const COL_NAME_ENTITY_ID As String = CommissionPeriodEntityDAL.COL_NAME_ENTITY_ID
        Public Const COL_NAME_POSITION As String = CommissionPeriodEntityDAL.COL_NAME_POSITION
        Public Const COL_NAME_COMM_ENTITY_NAME As String = CommissionPeriodEntityDAL.COL_NAME_ENTITY_NAME

#End Region

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property CommissionPeriodEntityId(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_COMMISSION_PERIOD_ENTITY_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property CommissionPeriodId(ByVal row As DataRow) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_COMMISSION_PERIOD_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property EntityId(ByVal row As DataRow) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_ENTITY_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property Position(ByVal row As DataRow) As LongType
            Get
                Return CType(row(COL_NAME_POSITION), LongType)
            End Get
        End Property

        Public Shared ReadOnly Property EntityName(ByVal row As DataRow) As String
            Get
                Return CType(row(COL_NAME_COMM_ENTITY_NAME), String)
            End Get
        End Property

    End Class

#End Region
#Region "List Methods"
    Public Class PeriodEntityList
        Inherits BusinessObjectListBase
        Public Sub New(ByVal parent As CommissionPeriod)
            MyBase.New(GetCommPeriodEntityList(parent), GetType(CommissionPeriodEntity), parent)
        End Sub

        Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
            Return True
        End Function

        Public Function FindById(ByVal commPeriodEntityId As Guid) As CommissionPeriodEntity
            Dim bo As CommissionPeriodEntity
            For Each bo In Me
                If bo.Id.Equals(commPeriodEntityId) Then Return bo
            Next
            Return Nothing
        End Function

        Public Function FindByPosition(ByVal position As LongType) As CommissionPeriodEntity
            Dim bo As CommissionPeriodEntity
            For Each bo In Me
                If bo.Position.Equals(position) Then Return bo
            Next
            Return Nothing
        End Function

    End Class

#End Region
End Class



