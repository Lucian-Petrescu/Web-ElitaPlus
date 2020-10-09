'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (10/2/2019)  ********************

Public Class ApInvoiceMatching
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
            Dim dal As New ApInvoiceMatchingDAL
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
            Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            setvalue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize() 
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(id As Guid)               
        Try
            Dim dal As New ApInvoiceMatchingDAL            
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
            If row(ApInvoiceMatchingDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ApInvoiceMatchingDAL.COL_NAME_INVOICE_MATCHING_ID), Byte()))
            End If
        End Get
    End Property
	
    
    Public Property PoLineId As Guid
        Get
            CheckDeleted()
            If row(ApInvoiceMatchingDAL.COL_NAME_PO_LINE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ApInvoiceMatchingDAL.COL_NAME_PO_LINE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ApInvoiceMatchingDAL.COL_NAME_PO_LINE_ID, Value)
        End Set
    End Property
	
	
    <ValueMandatory("")> _
    Public Property InvoiceLineId As Guid
        Get
            CheckDeleted()
            If row(ApInvoiceMatchingDAL.COL_NAME_INVOICE_LINE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ApInvoiceMatchingDAL.COL_NAME_INVOICE_LINE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ApInvoiceMatchingDAL.COL_NAME_INVOICE_LINE_ID, Value)
        End Set
    End Property
	
	
    <ValueMandatory("")> _
    Public Property Qty As DecimalType
        Get
            CheckDeleted()
            If row(ApInvoiceMatchingDAL.COL_NAME_QTY) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(ApInvoiceMatchingDAL.COL_NAME_QTY), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ApInvoiceMatchingDAL.COL_NAME_QTY, Value)
        End Set
    End Property
	
	
    <ValueMandatory("")> _
    Public Property Amount As DecimalType
        Get
            CheckDeleted()
            If row(ApInvoiceMatchingDAL.COL_NAME_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(ApInvoiceMatchingDAL.COL_NAME_AMOUNT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ApInvoiceMatchingDAL.COL_NAME_AMOUNT, Value)
        End Set
    End Property
	
	
    
    Public Property ExtendedQty As DecimalType
        Get
            CheckDeleted()
            If row(ApInvoiceMatchingDAL.COL_NAME_EXTENDED_QTY) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(ApInvoiceMatchingDAL.COL_NAME_EXTENDED_QTY), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ApInvoiceMatchingDAL.COL_NAME_EXTENDED_QTY, Value)
        End Set
    End Property
	
	
    <ValidStringLength("", Max:=120)> _
    Public Property ExtendedUom As String
        Get
            CheckDeleted()
            If row(ApInvoiceMatchingDAL.COL_NAME_EXTENDED_UOM) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ApInvoiceMatchingDAL.COL_NAME_EXTENDED_UOM), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ApInvoiceMatchingDAL.COL_NAME_EXTENDED_UOM, Value)
        End Set
    End Property
	
	
    
    Public Property ExtendedUnitPrice As DecimalType
        Get
            CheckDeleted()
            If row(ApInvoiceMatchingDAL.COL_NAME_EXTENDED_UNIT_PRICE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(ApInvoiceMatchingDAL.COL_NAME_EXTENDED_UNIT_PRICE), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ApInvoiceMatchingDAL.COL_NAME_EXTENDED_UNIT_PRICE, Value)
        End Set
    End Property
	
	
    
    Public Property ApPaymentBatchId As Guid
        Get
            CheckDeleted()
            If row(ApInvoiceMatchingDAL.COL_NAME_AP_PAYMENT_BATCH_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ApInvoiceMatchingDAL.COL_NAME_AP_PAYMENT_BATCH_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ApInvoiceMatchingDAL.COL_NAME_AP_PAYMENT_BATCH_ID, Value)
        End Set
    End Property
	
	
    <ValidStringLength("", Max:=400)> _
    Public Property MatchTypeXcd As String
        Get
            CheckDeleted()
            If row(ApInvoiceMatchingDAL.COL_NAME_MATCH_TYPE_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ApInvoiceMatchingDAL.COL_NAME_MATCH_TYPE_XCD), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ApInvoiceMatchingDAL.COL_NAME_MATCH_TYPE_XCD, Value)
        End Set
    End Property
	
	
   

#End Region

#Region "Public Members"
    Public Overrides Sub Save()         
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ApInvoiceMatchingDAL
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

End Class


