﻿Public Class CommPCodeEntityList
    Inherits BusinessObjectBase

#Region "Constructors"

    'New BO
    Public Sub New()
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load()
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As DataSet)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load()
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal isNew As Boolean, ByVal id As Guid, ByVal familyDS As DataSet)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load(isNew, id)
    End Sub

    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Me.Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    'Protected Sub Load()
    '    Try
    '        Dim dal As New CommPCodeEntityListDAL
    '        If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
    '            dal.LoadSchema(Me.Dataset)
    '        End If
    '        Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
    '        Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
    '        Me.Row = newRow
    '        SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
    '        Initialize()
    '    Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
    '        Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
    '    End Try
    'End Sub

    Protected Sub Load()
        Try
            Dim dal As New CommPCodeEntityListDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            'Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            'Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            'Me.Row = newRow
            'SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            'Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal isNew As Boolean, ByVal id As Guid)
        Try
            Dim dal As New CommPCodeEntityListDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            If isNew = True Then
                SetValue(dal.TABLE_KEY_NAME, id)
            Else
                SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            End If

            Initialize()
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
            If Row(CommPCodeEntityListDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CommPCodeEntityListDAL.COL_NAME_COMM_P_CODE_ENTITY_ID), Byte()))
            End If
        End Get
    End Property


    <ValueMandatory("")> _
    Public Property CommPCodeId() As Guid
        Get
            CheckDeleted()
            If Row(CommPCodeEntityListDAL.COL_NAME_COMM_P_CODE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CommPCodeEntityListDAL.COL_NAME_COMM_P_CODE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CommPCodeEntityListDAL.COL_NAME_COMM_P_CODE_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property PayeeTypeId() As Guid
        Get
            CheckDeleted()
            If Row(CommPCodeEntityListDAL.COL_NAME_PAYEE_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CommPCodeEntityListDAL.COL_NAME_PAYEE_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CommPCodeEntityListDAL.COL_NAME_PAYEE_TYPE_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
Public Property CommScheduleId() As Guid
        Get
            CheckDeleted()
            If Row(CommPCodeEntityDAL.COL_NAME_COMM_SCHEDULE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CommPCodeEntityDAL.COL_NAME_COMM_SCHEDULE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CommPCodeEntityDAL.COL_NAME_COMM_SCHEDULE_ID, Value)
        End Set
    End Property

    Public Property CommSchedule() As String
        Get
            CheckDeleted()
            If Row(CommPCodeEntityListDAL.COL_NAME_COMM_SCHEDULE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CommPCodeEntityListDAL.COL_NAME_COMM_SCHEDULE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CommPCodeEntityListDAL.COL_NAME_COMM_SCHEDULE, Value)
        End Set
    End Property

    Public Property PayeeType() As String
        Get
            CheckDeleted()
            If Row(CommPCodeEntityListDAL.COL_NAME_PAYEE_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CommPCodeEntityListDAL.COL_NAME_PAYEE_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CommPCodeEntityListDAL.COL_NAME_PAYEE_TYPE, Value)
        End Set
    End Property

    Public Property EntityId() As Guid
        Get
            CheckDeleted()
            If Row(CommPCodeEntityListDAL.COL_NAME_ENTITY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CommPCodeEntityListDAL.COL_NAME_ENTITY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CommPCodeEntityListDAL.COL_NAME_ENTITY_ID, Value)
        End Set
    End Property

    Public Property Entity() As String
        Get
            CheckDeleted()
            If Row(CommPCodeEntityListDAL.COL_NAME_ENTITY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CommPCodeEntityListDAL.COL_NAME_ENTITY), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CommPCodeEntityListDAL.COL_NAME_ENTITY, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property IsCommFixedId() As Guid
        Get
            CheckDeleted()
            If Row(CommPCodeEntityListDAL.COL_NAME_IS_COMM_FIXED_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CommPCodeEntityListDAL.COL_NAME_IS_COMM_FIXED_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CommPCodeEntityListDAL.COL_NAME_IS_COMM_FIXED_ID, Value)
        End Set
    End Property

    Public Property IsCommFixedCode() As String
        Get
            CheckDeleted()
            If Row(CommPCodeEntityListDAL.COL_NAME_IS_COMM_FIXED_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CommPCodeEntityListDAL.COL_NAME_IS_COMM_FIXED_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CommPCodeEntityListDAL.COL_NAME_IS_COMM_FIXED_CODE, Value)
        End Set
    End Property

    Public Property IsCommFixed() As String
        Get
            CheckDeleted()
            If Row(CommPCodeEntityListDAL.COL_NAME_IS_COMM_FIXED) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CommPCodeEntityListDAL.COL_NAME_IS_COMM_FIXED), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CommPCodeEntityListDAL.COL_NAME_IS_COMM_FIXED, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property CommissionAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(CommPCodeEntityListDAL.COL_NAME_COMMISSION_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CommPCodeEntityListDAL.COL_NAME_COMMISSION_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CommPCodeEntityListDAL.COL_NAME_COMMISSION_AMOUNT, Value)
        End Set
    End Property


    '<ValueMandatory("")> _
    'Public Property IsMarkupFixedId() As Guid
    '    Get
    '        CheckDeleted()
    '        If Row(CommPCodeEntityListDAL.COL_NAME_IS_MARKUP_FIXED_ID) Is DBNull.Value Then
    '            Return Nothing
    '        Else
    '            Return New Guid(CType(Row(CommPCodeEntityListDAL.COL_NAME_IS_MARKUP_FIXED_ID), Byte()))
    '        End If
    '    End Get
    '    Set(ByVal Value As Guid)
    '        CheckDeleted()
    '        Me.SetValue(CommPCodeEntityListDAL.COL_NAME_IS_MARKUP_FIXED_ID, Value)
    '    End Set
    'End Property

    'Public Property IsMarkupFixedCode() As String
    '    Get
    '        CheckDeleted()
    '        If Row(CommPCodeEntityListDAL.COL_NAME_IS_MARKUP_FIXED_CODE) Is DBNull.Value Then
    '            Return Nothing
    '        Else
    '            Return CType(Row(CommPCodeEntityListDAL.COL_NAME_IS_MARKUP_FIXED_CODE), String)
    '        End If
    '    End Get
    '    Set(ByVal Value As String)
    '        CheckDeleted()
    '        Me.SetValue(CommPCodeEntityListDAL.COL_NAME_IS_MARKUP_FIXED_CODE, Value)
    '    End Set
    'End Property

    'Public Property IsMarkupFixed() As String
    '    Get
    '        CheckDeleted()
    '        If Row(CommPCodeEntityListDAL.COL_NAME_IS_MARKUP_FIXED) Is DBNull.Value Then
    '            Return Nothing
    '        Else
    '            Return CType(Row(CommPCodeEntityListDAL.COL_NAME_IS_MARKUP_FIXED), String)
    '        End If
    '    End Get
    '    Set(ByVal Value As String)
    '        CheckDeleted()
    '        Me.SetValue(CommPCodeEntityListDAL.COL_NAME_IS_MARKUP_FIXED, Value)
    '    End Set
    'End Property



    <ValueMandatory("")> _
    Public Property MarkupAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(CommPCodeEntityListDAL.COL_NAME_MARKUP_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CommPCodeEntityListDAL.COL_NAME_MARKUP_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CommPCodeEntityListDAL.COL_NAME_MARKUP_AMOUNT, Value)
        End Set
    End Property

    ''REQ-976 
    <ValidNumericRange(" ", MAX:=9999)> _
    Public Property DaysToClawback() As LongType
        Get
            CheckDeleted()
            If Row(CommPCodeEntityListDAL.COL_NAME_DAYS_TO_Clawback) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(CommPCodeEntityListDAL.COL_NAME_DAYS_TO_Clawback), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(CommPCodeEntityListDAL.COL_NAME_DAYS_TO_Clawback, Value)
        End Set
    End Property
    Public Property BranchId() As Guid
        Get
            CheckDeleted()
            If Row(CommPCodeEntityListDAL.COL_NAME_BRANCH_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CommPCodeEntityListDAL.COL_NAME_BRANCH_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CommPCodeEntityListDAL.COL_NAME_BRANCH_ID, Value)
        End Set
    End Property
    Public Property BranchName() As String
        Get
            CheckDeleted()
            If Row(BranchDAL.COL_NAME_BRANCH_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(BranchDAL.COL_NAME_BRANCH_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(BranchDAL.COL_NAME_BRANCH_NAME, Value)
        End Set
    End Property

    ''End Req-976

#End Region

    '#Region "DataView Retrieveing Methods"


    '    Public Shared Function getList(ByVal commPCodeId As Guid) As DataView
    '        Try
    '            Dim dal As New CommPCodeEntityDAL

    '            Return New DataView(dal.LoadList(commPCodeId, Authentication.LangId).Tables(0))
    '        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
    '            Throw New DataBaseAccessException(ex.ErrorType, ex)
    '        End Try
    '    End Function

    '    Public Shared Function getEntities(ByVal oDataSet As DataSet, ByVal commPCodeId As Guid) As DataTable
    '        Try
    '            Dim dal As New CommPCodeEntityDAL

    '            Return dal.LoadEntities(oDataSet, commPCodeId).Tables(CommPCodeEntityDAL.TABLE_NAME)
    '        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
    '            Throw New DataBaseAccessException(ex.ErrorType, ex)
    '        End Try
    '    End Function

    '#End Region


End Class
