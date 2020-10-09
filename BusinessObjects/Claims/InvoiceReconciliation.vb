'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (2/21/2013)  ********************

Public NotInheritable Class InvoiceReconciliation
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(id As Guid)
        MyBase.New()
        Dataset = New DataSet
        Load(id)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Dataset = New DataSet
        Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(id As Guid, familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load()
    End Sub

    Public Sub New(row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New InvoiceReconciliationDAL
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
            Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(id As Guid)
        Try
            Dim dal As New InvoiceReconciliationDAL
            If _isDSCreator Then
                If Row IsNot Nothing Then
                    Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Row)
                End If
            End If
            Row = Nothing
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Row = FindRow(id, dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.Load(Dataset, id)
                Row = FindRow(id, dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then
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
    Public ReadOnly Property Id As Guid
        Get
            If Row(InvoiceReconciliationDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(InvoiceReconciliationDAL.COL_NAME_INVOICE_RECONCILIATION_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property ClaimAuthItemId As Guid
        Get
            CheckDeleted()
            If Row(InvoiceReconciliationDAL.COL_NAME_CLAIM_AUTH_ITEM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(InvoiceReconciliationDAL.COL_NAME_CLAIM_AUTH_ITEM_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(InvoiceReconciliationDAL.COL_NAME_CLAIM_AUTH_ITEM_ID, Value)
            ClaimAuthorizationItem = Nothing
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property InvoiceItemId As Guid
        Get
            CheckDeleted()
            If Row(InvoiceReconciliationDAL.COL_NAME_INVOICE_ITEM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(InvoiceReconciliationDAL.COL_NAME_INVOICE_ITEM_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(InvoiceReconciliationDAL.COL_NAME_INVOICE_ITEM_ID, Value)
            InvoiceItem = Nothing
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ReconciliationStatusId As Guid
        Get
            CheckDeleted()
            If Row(InvoiceReconciliationDAL.COL_NAME_RECONCILIATION_STATUS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(InvoiceReconciliationDAL.COL_NAME_RECONCILIATION_STATUS_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(InvoiceReconciliationDAL.COL_NAME_RECONCILIATION_STATUS_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ReconciledAmount As DecimalType
        Get
            CheckDeleted()
            If Row(InvoiceReconciliationDAL.COL_NAME_RECONCILED_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(InvoiceReconciliationDAL.COL_NAME_RECONCILED_AMOUNT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(InvoiceReconciliationDAL.COL_NAME_RECONCILED_AMOUNT, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New InvoiceReconciliationDAL
                dal.Update(Row)
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Id
                    Dataset = New DataSet
                    Row = Nothing
                    Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
#End Region

#Region "Lazy Initialize Fields"
    Private _claimAuthItem As ClaimAuthItem = Nothing
    Private _invoiceItem As InvoiceItem = Nothing
#End Region

#Region "Lazy Initialize Properties"
    Public Property ClaimAuthorizationItem As ClaimAuthItem
        Get
            If (_claimAuthItem Is Nothing) Then
                If Not ClaimAuthItemId.Equals(Guid.Empty) Then
                    Me.ClaimAuthorizationItem = New ClaimAuthItem(ClaimAuthItemId, Dataset)
                End If
            End If
            Return _claimAuthItem
        End Get
        Private Set
            If (value Is Nothing OrElse _claimAuthItem Is Nothing OrElse Not _claimAuthItem.Equals(value)) Then
                _claimAuthItem = value
            End If
        End Set
    End Property

    Public Property InvoiceItem As InvoiceItem
        Get
            If (_invoiceItem Is Nothing) Then
                If Not InvoiceItemId.Equals(Guid.Empty) Then
                    Me.InvoiceItem = New InvoiceItem(InvoiceItemId, Dataset)
                End If
            End If
            Return _invoiceItem
        End Get
        Private Set
            If (value Is Nothing OrElse _invoiceItem Is Nothing OrElse Not _invoiceItem.Equals(value)) Then
                _invoiceItem = value
            End If
        End Set
    End Property
#End Region

#Region "DataView Retrieveing Methods"

#End Region

End Class


