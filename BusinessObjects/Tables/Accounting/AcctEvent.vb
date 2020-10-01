'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (4/19/2007)  ********************

Public Class AcctEvent
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
            Dim dal As New AcctEventDAL
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
            Dim dal As New AcctEventDAL
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
            If Row(AcctEventDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AcctEventDAL.COL_NAME_ACCT_EVENT_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")>
    Public Property AcctCompanyId() As Guid
        Get
            CheckDeleted()
            If Row(AcctEventDAL.COL_NAME_ACCT_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AcctEventDAL.COL_NAME_ACCT_COMPANY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(AcctEventDAL.COL_NAME_ACCT_COMPANY_ID, Value)
        End Set
    End Property



    Public Property DealerId() As Guid
        Get
            CheckDeleted()
            If Row(AcctEventDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AcctEventDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(AcctEventDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property



    Public Property ServiceCenterId() As Guid
        Get
            CheckDeleted()
            If Row(AcctEventDAL.COL_NAME_SERVICE_CENTER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AcctEventDAL.COL_NAME_SERVICE_CENTER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(AcctEventDAL.COL_NAME_SERVICE_CENTER_ID, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property AcctEventTypeId() As Guid
        Get
            CheckDeleted()
            If Row(AcctEventDAL.COL_NAME_ACCT_EVENT_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AcctEventDAL.COL_NAME_ACCT_EVENT_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(AcctEventDAL.COL_NAME_ACCT_EVENT_TYPE_ID, Value)
        End Set
    End Property

    Public Property EventCondition() As Object
        Get
            CheckDeleted()
            If Row(AcctEventDAL.COL_NAME_EVENT_CONDITION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctEventDAL.COL_NAME_EVENT_CONDITION), Object)
            End If
        End Get
        Set(ByVal Value As Object)
            CheckDeleted()
            Me.SetValue(AcctEventDAL.COL_NAME_EVENT_CONDITION, Value)
        End Set
    End Property

    Public Property LastRunDate() As DateType
        Get
            CheckDeleted()
            If Row(AcctEventDAL.COL_NAME_LAST_RUN_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(AcctEventDAL.COL_NAME_LAST_RUN_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(AcctEventDAL.COL_NAME_LAST_RUN_DATE, Value)
        End Set
    End Property



    Public Property LastCompleteDate() As DateType
        Get
            CheckDeleted()
            If Row(AcctEventDAL.COL_NAME_LAST_COMPLETE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(AcctEventDAL.COL_NAME_LAST_COMPLETE_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(AcctEventDAL.COL_NAME_LAST_COMPLETE_DATE, Value)
        End Set
    End Property



    Public Property DynSql() As Object
        Get
            CheckDeleted()
            If Row(AcctEventDAL.COL_NAME_DYN_SQL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctEventDAL.COL_NAME_DYN_SQL), Object)
            End If
        End Get
        Set(ByVal Value As Object)
            CheckDeleted()
            Me.SetValue(AcctEventDAL.COL_NAME_DYN_SQL, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=1)>
    Public Property AllowBalTran() As String
        Get
            CheckDeleted()
            If Row(AcctEventDAL.COL_NAME_ALLOW_BAL_TRAN) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctEventDAL.COL_NAME_ALLOW_BAL_TRAN), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctEventDAL.COL_NAME_ALLOW_BAL_TRAN, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1)>
    Public Property AllowOverBudget() As String
        Get
            CheckDeleted()
            If Row(AcctEventDAL.COL_NAME_ALLOW_OVER_BUDGET) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctEventDAL.COL_NAME_ALLOW_OVER_BUDGET), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctEventDAL.COL_NAME_ALLOW_OVER_BUDGET, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1)>
    Public Property AllowPostToSuspended() As String
        Get
            CheckDeleted()
            If Row(AcctEventDAL.COL_NAME_ALLOW_POST_TO_SUSPENDED) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctEventDAL.COL_NAME_ALLOW_POST_TO_SUSPENDED), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctEventDAL.COL_NAME_ALLOW_POST_TO_SUSPENDED, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=2)>
    Public Property BalancingOptions() As String
        Get
            CheckDeleted()
            If Row(AcctEventDAL.COL_NAME_BALANCING_OPTIONS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctEventDAL.COL_NAME_BALANCING_OPTIONS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctEventDAL.COL_NAME_BALANCING_OPTIONS, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=5)>
    Public Property JournalType() As String
        Get
            CheckDeleted()
            If Row(AcctEventDAL.COL_NAME_JOURNAL_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctEventDAL.COL_NAME_JOURNAL_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctEventDAL.COL_NAME_JOURNAL_TYPE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1)>
    Public Property LoadOnly() As String
        Get
            CheckDeleted()
            If Row(AcctEventDAL.COL_NAME_LOAD_ONLY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctEventDAL.COL_NAME_LOAD_ONLY), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctEventDAL.COL_NAME_LOAD_ONLY, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1)>
    Public Property PostingType() As String
        Get
            CheckDeleted()
            If Row(AcctEventDAL.COL_NAME_POSTING_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctEventDAL.COL_NAME_POSTING_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctEventDAL.COL_NAME_POSTING_TYPE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1)>
    Public Property PostProvisional() As String
        Get
            CheckDeleted()
            If Row(AcctEventDAL.COL_NAME_POST_PROVISIONAL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctEventDAL.COL_NAME_POST_PROVISIONAL), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctEventDAL.COL_NAME_POST_PROVISIONAL, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1)>
    Public Property PostToHold() As String
        Get
            CheckDeleted()
            If Row(AcctEventDAL.COL_NAME_POST_TO_HOLD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctEventDAL.COL_NAME_POST_TO_HOLD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctEventDAL.COL_NAME_POST_TO_HOLD, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=15)>
    Public Property ReportingAccount() As String
        Get
            CheckDeleted()
            If Row(AcctEventDAL.COL_NAME_REPORTING_ACCOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctEventDAL.COL_NAME_REPORTING_ACCOUNT), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctEventDAL.COL_NAME_REPORTING_ACCOUNT, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1)>
    Public Property SuppressSubstitutedMessages() As String
        Get
            CheckDeleted()
            If Row(AcctEventDAL.COL_NAME_SUPPRESS_SUBSTITUTED_MESSAGES) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctEventDAL.COL_NAME_SUPPRESS_SUBSTITUTED_MESSAGES), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctEventDAL.COL_NAME_SUPPRESS_SUBSTITUTED_MESSAGES, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=15)>
    Public Property SuspenseAccount() As String
        Get
            CheckDeleted()
            If Row(AcctEventDAL.COL_NAME_SUSPENSE_ACCOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctEventDAL.COL_NAME_SUSPENSE_ACCOUNT), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctEventDAL.COL_NAME_SUSPENSE_ACCOUNT, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=15)>
    Public Property TransactionAmountAccount() As String
        Get
            CheckDeleted()
            If Row(AcctEventDAL.COL_NAME_TRANSACTION_AMOUNT_ACCOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctEventDAL.COL_NAME_TRANSACTION_AMOUNT_ACCOUNT), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctEventDAL.COL_NAME_TRANSACTION_AMOUNT_ACCOUNT, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=5)>
    Public Property LayoutCode() As String
        Get
            CheckDeleted()
            If Row(AcctEventDAL.COL_NAME_LAYOUT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctEventDAL.COL_NAME_LAYOUT_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctEventDAL.COL_NAME_LAYOUT_CODE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=100)>
    Public Property EventName() As String
        Get
            CheckDeleted()
            If Row(AcctEventDAL.COL_NAME_EVENT_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctEventDAL.COL_NAME_EVENT_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctEventDAL.COL_NAME_EVENT_NAME, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=20)>
    Public Property EventDescription() As String
        Get
            CheckDeleted()
            If Row(AcctEventDAL.COL_NAME_EVENT_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctEventDAL.COL_NAME_EVENT_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctEventDAL.COL_NAME_EVENT_DESCRIPTION, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=100)>
    Public Property JournalLevel() As String
        Get
            CheckDeleted()
            If Row(AcctEventDAL.COL_NAME_JOURNAL_LEVEL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctEventDAL.COL_NAME_JOURNAL_LEVEL), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctEventDAL.COL_NAME_JOURNAL_LEVEL, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New AcctEventDAL
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
#Region "AcctEventSearchDV"
    Public Class AcctEventSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_ACCT_COMPANY_ID As String = "acct_company_id"
        Public Const COL_EVENT_NAME As String = "event_name"
        Public Const COL_EVENT_ID As String = "acct_event_id"
        Public Const COL_EVENT_TYPE As String = "event_type"
        Public Const COL_EVENT_CODE As String = "event_code"
        Public Const COL_ACCT_EVENT_TYPE_ID As String = "acct_event_type_id"

#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region

    Public Shared Function getList(ByVal EventTypeMask As Guid, ByVal AcctCompanyMask As Guid) As AcctEventSearchDV
        Try
            Dim dal As New AcctEventDAL
            Return New AcctEventSearchDV(dal.LoadList(EventTypeMask, AcctCompanyMask, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

End Class


