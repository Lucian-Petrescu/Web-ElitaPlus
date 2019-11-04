'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (10/2/2019)  ********************

Public Class ApInvoiceMatching
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
            Dim dal As New ApInvoiceMatchingDAL
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
            Dim dal As New ApInvoiceMatchingDAL            
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
            If row(ApInvoiceMatchingDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ApInvoiceMatchingDAL.COL_NAME_INVOICE_MATCHING_ID), Byte()))
            End If
        End Get
    End Property
	
    
    Public Property PoLineId() As Guid
        Get
            CheckDeleted()
            If row(ApInvoiceMatchingDAL.COL_NAME_PO_LINE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ApInvoiceMatchingDAL.COL_NAME_PO_LINE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ApInvoiceMatchingDAL.COL_NAME_PO_LINE_ID, Value)
        End Set
    End Property
	
	
    <ValueMandatory("")> _
    Public Property InvoiceLineId() As Guid
        Get
            CheckDeleted()
            If row(ApInvoiceMatchingDAL.COL_NAME_INVOICE_LINE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ApInvoiceMatchingDAL.COL_NAME_INVOICE_LINE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ApInvoiceMatchingDAL.COL_NAME_INVOICE_LINE_ID, Value)
        End Set
    End Property
	
	
    <ValueMandatory("")> _
    Public Property Qty() As DecimalType
        Get
            CheckDeleted()
            If row(ApInvoiceMatchingDAL.COL_NAME_QTY) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(ApInvoiceMatchingDAL.COL_NAME_QTY), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ApInvoiceMatchingDAL.COL_NAME_QTY, Value)
        End Set
    End Property
	
	
    <ValueMandatory("")> _
    Public Property Amount() As DecimalType
        Get
            CheckDeleted()
            If row(ApInvoiceMatchingDAL.COL_NAME_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(ApInvoiceMatchingDAL.COL_NAME_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ApInvoiceMatchingDAL.COL_NAME_AMOUNT, Value)
        End Set
    End Property
	
	
    
    Public Property ExtendedQty() As DecimalType
        Get
            CheckDeleted()
            If row(ApInvoiceMatchingDAL.COL_NAME_EXTENDED_QTY) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(ApInvoiceMatchingDAL.COL_NAME_EXTENDED_QTY), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ApInvoiceMatchingDAL.COL_NAME_EXTENDED_QTY, Value)
        End Set
    End Property
	
	
    <ValidStringLength("", Max:=120)> _
    Public Property ExtendedUom() As String
        Get
            CheckDeleted()
            If row(ApInvoiceMatchingDAL.COL_NAME_EXTENDED_UOM) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ApInvoiceMatchingDAL.COL_NAME_EXTENDED_UOM), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ApInvoiceMatchingDAL.COL_NAME_EXTENDED_UOM, Value)
        End Set
    End Property
	
	
    
    Public Property ExtendedUnitPrice() As DecimalType
        Get
            CheckDeleted()
            If row(ApInvoiceMatchingDAL.COL_NAME_EXTENDED_UNIT_PRICE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(ApInvoiceMatchingDAL.COL_NAME_EXTENDED_UNIT_PRICE), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ApInvoiceMatchingDAL.COL_NAME_EXTENDED_UNIT_PRICE, Value)
        End Set
    End Property
	
	
    
    Public Property ApPaymentBatchId() As Guid
        Get
            CheckDeleted()
            If row(ApInvoiceMatchingDAL.COL_NAME_AP_PAYMENT_BATCH_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ApInvoiceMatchingDAL.COL_NAME_AP_PAYMENT_BATCH_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ApInvoiceMatchingDAL.COL_NAME_AP_PAYMENT_BATCH_ID, Value)
        End Set
    End Property
	
	
    <ValidStringLength("", Max:=400)> _
    Public Property MatchTypeXcd() As String
        Get
            CheckDeleted()
            If row(ApInvoiceMatchingDAL.COL_NAME_MATCH_TYPE_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ApInvoiceMatchingDAL.COL_NAME_MATCH_TYPE_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ApInvoiceMatchingDAL.COL_NAME_MATCH_TYPE_XCD, Value)
        End Set
    End Property
	
	
   

#End Region

#Region "Public Members"
    Public Overrides Sub Save()         
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New ApInvoiceMatchingDAL
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

End Class


