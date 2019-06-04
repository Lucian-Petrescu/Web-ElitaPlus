'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (2/1/2010)  ********************

Public Class CommPCodeEntity
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

    Protected Sub Load()
        Try
            Dim dal As New CommPCodeEntityDAL
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
            Dim dal As New CommPCodeEntityDAL
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

    Protected Sub Load(ByVal isNew As Boolean, ByVal id As Guid)
        Try
            Dim dal As New CommPCodeEntityDAL
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

    'Public Sub SetIdValue(ByVal commPCodeEntityId As Guid)
    '    SetValue(CommPCodeEntityDAL.TABLE_KEY_NAME, commPCodeEntityId)
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
            If row(CommPCodeEntityDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CommPCodeEntityDAL.COL_NAME_COMM_P_CODE_ENTITY_ID), Byte()))
            End If
        End Get
    End Property

    
    <ValueMandatory("")> _
    Public Property CommPCodeId() As Guid
        Get
            CheckDeleted()
            If row(CommPCodeEntityDAL.COL_NAME_COMM_P_CODE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CommPCodeEntityDAL.COL_NAME_COMM_P_CODE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CommPCodeEntityDAL.COL_NAME_COMM_P_CODE_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property PayeeTypeId() As Guid
        Get
            CheckDeleted()
            If row(CommPCodeEntityDAL.COL_NAME_PAYEE_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CommPCodeEntityDAL.COL_NAME_PAYEE_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CommPCodeEntityDAL.COL_NAME_PAYEE_TYPE_ID, Value)
        End Set
    End Property



    Public Property EntityId() As Guid
        Get
            CheckDeleted()
            If row(CommPCodeEntityDAL.COL_NAME_ENTITY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CommPCodeEntityDAL.COL_NAME_ENTITY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CommPCodeEntityDAL.COL_NAME_ENTITY_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property IsCommFixedId() As Guid
        Get
            CheckDeleted()
            If row(CommPCodeEntityDAL.COL_NAME_IS_COMM_FIXED_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CommPCodeEntityDAL.COL_NAME_IS_COMM_FIXED_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CommPCodeEntityDAL.COL_NAME_IS_COMM_FIXED_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property CommissionAmount() As DecimalType
        Get
            CheckDeleted()
            If row(CommPCodeEntityDAL.COL_NAME_COMMISSION_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(CommPCodeEntityDAL.COL_NAME_COMMISSION_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CommPCodeEntityDAL.COL_NAME_COMMISSION_AMOUNT, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property MarkupAmount() As DecimalType
        Get
            CheckDeleted()
            If row(CommPCodeEntityDAL.COL_NAME_MARKUP_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(CommPCodeEntityDAL.COL_NAME_MARKUP_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CommPCodeEntityDAL.COL_NAME_MARKUP_AMOUNT, Value)
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
    'REQ-976 
    <ValidNumericRange(" ", MAX:=9999)> _
    Public Property DaysToClawback() As LongType
        Get
            CheckDeleted()
            If Row(CommPCodeEntityDAL.COL_NAME_DAYS_TO_CLAWBACK) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(CommPCodeEntityDAL.COL_NAME_DAYS_TO_CLAWBACK), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(CommPCodeEntityDAL.COL_NAME_DAYS_TO_CLAWBACK, Value)
        End Set
    End Property


    Public Property BranchId() As Guid
        Get
            CheckDeleted()
            If Row(CommPCodeEntityDAL.COL_NAME_BRANCH_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CommPCodeEntityDAL.COL_NAME_BRANCH_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CommPCodeEntityDAL.COL_NAME_BRANCH_ID, Value)
        End Set
    End Property
    'End Req-976

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New CommPCodeEntityDAL
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

    Public Sub Delete(ByVal singleEntry As Boolean)
        Dim oDataView As DataView
        Dim oRow As DataRow
        Try
            If singleEntry = True Then
                oDataView = getList(Me.CommPCodeId)
                oRow = Me.FindRow(Id, CommPCodeEntityDAL.TABLE_KEY_NAME, oDataView.Table)
                oRow.Delete()
                CommPCode.ValidateEntityChildren(oDataView.Table)
            End If

            MyBase.Delete()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
#End Region

#Region "DataView Retrieveing Methods"

    
    Public Shared Function getList(ByVal commPCodeId As Guid) As DataView
        Try
            Dim dal As New CommPCodeEntityDAL

            Return New DataView(dal.LoadList(commPCodeId, Authentication.LangId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function getEntities(ByVal oDataSet As DataSet, ByVal commPCodeId As Guid) As DataTable
        Try
            Dim dal As New CommPCodeEntityDAL

            Return dal.LoadEntities(oDataSet, commPCodeId).Tables(CommPCodeEntityDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#End Region

   
End Class




