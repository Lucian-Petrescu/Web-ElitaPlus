'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (1/7/2015)  ********************

Public Class Reconciliation
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
            Dim dal As New ReconciliationDAL
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
            Dim dal As New ReconciliationDAL
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

#Region "ReconciliationDV"

    Public Class ReconciliationDV
        Inherits DataView

#Region "Constants"
        'Public Const COL_NAME_AFA_ENROLL_RECON_DISCREP_ID As String = "afa_enroll_recon_discrep_id"
        Public Const COL_NAME_DEALER_ID As String = "dealer_id"
        Public Const COL_NAME_BILLING_DATE As String = "billing_date"
        Public Const COL_NAME_SOC_TYPE As String = "soc_type"
        Public Const COL_NAME_ACCOUNT_STATUS As String = "account_status"
        Public Const COL_NAME_BILLABLE_COUNT As String = "billable_count"
        Public Const COL_NAME_CARRIER_COUNT As String = "carrier_count"
        Public Const COL_NAME_DISCREPANCY As String = "discrepancy"

        'Public Const COL_NAME_ASSURANT_JUMP2_COUNT As String = "assurant_JUMP2"
        'Public Const COL_NAME_CARRIER_JUMP2_COUNT As String = "carrier_JUMP2"
        'Public Const COL_NAME_ASSURANT_WAR_NY_COUNT As String = "assurant_WAR_NY"
        'Public Const COL_NAME_CARRIER_WAR_NY_COUNT As String = "carrier_WAR_NY"
        'Public Const COL_NAME_ASSURANT_BUND_NY_COUNT As String = "assurant_BUND_NY"
        'Public Const COL_NAME_CARRIER_BUND_NY_COUNT As String = "carrier_BUND_NY"

#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

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
            If Row(ReconciliationDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ReconciliationDAL.COL_NAME_AFA_ENROLL_RECON_DISCREP_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property DealerId() As Guid
        Get
            CheckDeleted()
            If Row(ReconciliationDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ReconciliationDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ReconciliationDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property BillingDate() As DateType
        Get
            CheckDeleted()
            If Row(ReconciliationDAL.COL_NAME_BILLING_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ReconciliationDAL.COL_NAME_BILLING_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(ReconciliationDAL.COL_NAME_BILLING_DATE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=50)> _
    Public Property SocType() As String
        Get
            CheckDeleted()
            If Row(ReconciliationDAL.COL_NAME_SOC_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ReconciliationDAL.COL_NAME_SOC_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ReconciliationDAL.COL_NAME_SOC_TYPE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)> _
    Public Property AccountStatus() As String
        Get
            CheckDeleted()
            If Row(ReconciliationDAL.COL_NAME_ACCOUNT_STATUS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ReconciliationDAL.COL_NAME_ACCOUNT_STATUS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ReconciliationDAL.COL_NAME_ACCOUNT_STATUS, Value)
        End Set
    End Property



    Public Property BillableCount() As LongType
        Get
            CheckDeleted()
            If Row(ReconciliationDAL.COL_NAME_BILLABLE_COUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ReconciliationDAL.COL_NAME_BILLABLE_COUNT), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(ReconciliationDAL.COL_NAME_BILLABLE_COUNT, Value)
        End Set
    End Property



    Public Property CarrierCount() As LongType
        Get
            CheckDeleted()
            If Row(ReconciliationDAL.COL_NAME_CARRIER_COUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ReconciliationDAL.COL_NAME_CARRIER_COUNT), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(ReconciliationDAL.COL_NAME_CARRIER_COUNT, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1)> _
    Public Property Discrepancy() As String
        Get
            CheckDeleted()
            If Row(ReconciliationDAL.COL_NAME_DISCREPANCY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ReconciliationDAL.COL_NAME_DISCREPANCY), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ReconciliationDAL.COL_NAME_DISCREPANCY, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New ReconciliationDAL
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

    Public Shared Function GetPHPReconData(ByVal dealerId As Guid, ByVal firstDayOfMonth As String, ByVal lastDayOfMonth As String, ByVal showOnlyDiscrep As Boolean) As ReconciliationDV
        Try

            Dim dal As New ReconciliationDAL
            Return New ReconciliationDV(dal.GetPHPReconData(dealerId, firstDayOfMonth, lastDayOfMonth, showOnlyDiscrep).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetMHPReconData(ByVal dealerId As Guid, ByVal firstDayOfMonth As String, ByVal lastDayOfMonth As String, ByVal showOnlyDiscrep As Boolean) As ReconciliationDV
        Try

            Dim dal As New ReconciliationDAL
            Return New ReconciliationDV(dal.GetMHPReconData(dealerId, firstDayOfMonth, lastDayOfMonth, showOnlyDiscrep).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function OverRideReconciliation(ByVal dealerId As Guid, ByVal firstDayOfMonth As String, _
                                                  ByVal lastDayOfMonth As String, ByVal userName As String) As Boolean
        Try

            Dim dal As New ReconciliationDAL
            Return dal.OverRideReconciliation(dealerId, firstDayOfMonth, lastDayOfMonth, userName)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function ReloadReconcillation(ByVal dealerId As Guid, ByVal firstDayOfMonth As String, ByVal userName As String) As Boolean
        Try

            Dim dal As New ReconciliationDAL
            Return dal.ReloadReconcillation(dealerId, firstDayOfMonth, userName)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

End Class



