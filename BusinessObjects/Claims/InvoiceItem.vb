'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (2/21/2013)  ********************

Public NotInheritable Class InvoiceItem
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
            Dim dal As New InvoiceItemDAL
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
            Dim dal As New InvoiceItemDAL
            If _isDSCreator Then
                If Not Row Is Nothing Then
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
            If Row(InvoiceItemDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(InvoiceItemDAL.COL_NAME_INVOICE_ITEM_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property InvoiceId As Guid
        Get
            CheckDeleted()
            If Row(InvoiceItemDAL.COL_NAME_INVOICE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(InvoiceItemDAL.COL_NAME_INVOICE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(InvoiceItemDAL.COL_NAME_INVOICE_ID, Value)
            Invoice = Nothing
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property ClaimAuthorizationId As Guid
        Get
            CheckDeleted()
            If Row(InvoiceItemDAL.COL_NAME_CLAIM_AUTHORIZATION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(InvoiceItemDAL.COL_NAME_CLAIM_AUTHORIZATION_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(InvoiceItemDAL.COL_NAME_CLAIM_AUTHORIZATION_ID, Value)
            ClaimAuthorization = Nothing
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property LineItemNumber As LongType
        Get
            CheckDeleted()
            If Row(InvoiceItemDAL.COL_NAME_LINE_ITEM_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(InvoiceItemDAL.COL_NAME_LINE_ITEM_NUMBER), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(InvoiceItemDAL.COL_NAME_LINE_ITEM_NUMBER, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=200)> _
    Public Property VendorSku As String
        Get
            CheckDeleted()
            If Row(InvoiceItemDAL.COL_NAME_VENDOR_SKU) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(InvoiceItemDAL.COL_NAME_VENDOR_SKU), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(InvoiceItemDAL.COL_NAME_VENDOR_SKU, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=800)> _
    Public Property VendorSkuDescription As String
        Get
            CheckDeleted()
            If Row(InvoiceItemDAL.COL_NAME_VENDOR_SKU_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(InvoiceItemDAL.COL_NAME_VENDOR_SKU_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(InvoiceItemDAL.COL_NAME_VENDOR_SKU_DESCRIPTION, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property Amount As DecimalType
        Get
            CheckDeleted()
            If Row(InvoiceItemDAL.COL_NAME_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(InvoiceItemDAL.COL_NAME_AMOUNT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(InvoiceItemDAL.COL_NAME_AMOUNT, Value)
        End Set
    End Property



    Public Property AdjustmentReasonId As Guid
        Get
            CheckDeleted()
            If Row(InvoiceItemDAL.COL_NAME_ADJUSTMENT_REASON_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(InvoiceItemDAL.COL_NAME_ADJUSTMENT_REASON_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(InvoiceItemDAL.COL_NAME_ADJUSTMENT_REASON_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ServiceClassId As Guid
        Get
            CheckDeleted()
            If Row(InvoiceItemDAL.COL_NAME_SERVICE_CLASS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(InvoiceItemDAL.COL_NAME_SERVICE_CLASS_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(InvoiceItemDAL.COL_NAME_SERVICE_CLASS_ID, Value)
        End Set
    End Property



    Public Property ServiceTypeId As Guid
        Get
            CheckDeleted()
            If Row(InvoiceItemDAL.COL_NAME_SERVICE_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(InvoiceItemDAL.COL_NAME_SERVICE_TYPE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(InvoiceItemDAL.COL_NAME_SERVICE_TYPE_ID, Value)
        End Set
    End Property



    Public Property ServiceLevelId As Guid
        Get
            CheckDeleted()
            If Row(InvoiceItemDAL.COL_NAME_SERVICE_LEVEL_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(InvoiceItemDAL.COL_NAME_SERVICE_LEVEL_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(InvoiceItemDAL.COL_NAME_SERVICE_LEVEL_ID, Value)
        End Set
    End Property




#End Region

#Region "Derived Properties"
    Public ReadOnly Property InvoiceReconciliationId As Guid
        Get
            Dim row As DataRow
            Dim dal As New InvoiceReconciliationDAL
            If Dataset.Tables.IndexOf(InvoiceReconciliationDAL.TABLE_NAME) >= 0 Then
                row = FindRow(Id, InvoiceItemDAL.COL_NAME_INVOICE_ITEM_ID, Dataset.Tables(InvoiceReconciliationDAL.TABLE_NAME))
            End If
            If row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.LoadByInvoiceItemId(Dataset, Id)
                row = FindRow(Id, InvoiceItemDAL.COL_NAME_INVOICE_ITEM_ID, Dataset.Tables(InvoiceReconciliationDAL.TABLE_NAME))
            End If
            If (row Is Nothing) Then
                Return Guid.Empty
            Else
                Return New InvoiceReconciliation(row).Id
            End If

        End Get
    End Property
#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New InvoiceItemDAL
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

#Region "DataView Retrieveing Methods"

#End Region

#Region "Lazy Initialize Fields"
    Private _invoice As Invoice = Nothing
    Private _claimAuthorization As ClaimAuthorization
    Private _invoiceReconciliation As InvoiceReconciliation
#End Region

#Region "Lazy Initialize Properties"
    Public Property Invoice As Invoice
        Get
            If (_invoice Is Nothing) Then
                If Not InvoiceId.Equals(Guid.Empty) Then
                    Me.Invoice = New Invoice(InvoiceId, Dataset)
                End If
            End If
            Return _invoice
        End Get
        Private Set
            If (_invoice Is Nothing OrElse value Is Nothing OrElse Not _invoice.Equals(value)) Then
                _invoice = value
            End If
        End Set
    End Property

    Public Property ClaimAuthorization As ClaimAuthorization
        Get
            If (_claimAuthorization Is Nothing) Then
                If Not ClaimAuthorizationId.Equals(Guid.Empty) Then
                    Me.ClaimAuthorization = New ClaimAuthorization(ClaimAuthorizationId, Dataset)
                End If
            End If
            Return _claimAuthorization
        End Get
        Private Set
            If (_claimAuthorization Is Nothing OrElse value Is Nothing OrElse Not _claimAuthorization.Equals(value)) Then
                _claimAuthorization = value
            End If
        End Set
    End Property

    Public Property InvoiceReconciliation As InvoiceReconciliation
        Get
            If (_invoiceReconciliation Is Nothing) Then
                If Not InvoiceReconciliationId.Equals(Guid.Empty) Then
                    Me.InvoiceReconciliation = New InvoiceReconciliation(InvoiceReconciliationId, Dataset)
                End If
            End If
            Return _invoiceReconciliation
        End Get
        Set
            If (_invoiceReconciliation Is Nothing OrElse value Is Nothing OrElse Not _invoiceReconciliation.Equals(value)) Then
                _invoiceReconciliation = value
            End If
        End Set
    End Property
#End Region

End Class

Public Class InvoiceItemList
    Inherits BusinessObjectListEnumerableBase(Of Invoice, InvoiceItem)

    Public Sub New(parent As Invoice)
        MyBase.New(LoadTable(parent), parent)
    End Sub

    Public Overrides Function Belong(bo As BusinessObjectBase) As Boolean
        Return CType(bo, InvoiceItem).InvoiceId.Equals(CType(Parent, Invoice).Id)
    End Function

    Private Shared Function LoadTable(parent As Invoice) As DataTable
        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(InvoiceItemList)) Then
                Dim dal As New InvoiceItemDAL
                dal.LoadList(parent.Dataset, parent.Id)
                parent.AddChildrenCollection(GetType(InvoiceItemList))
            End If
            Return parent.Dataset.Tables(InvoiceItemDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

End Class