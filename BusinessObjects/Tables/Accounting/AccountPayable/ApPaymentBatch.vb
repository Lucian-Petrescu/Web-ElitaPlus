'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (10/2/2019)  ********************

Public Class ApPaymentBatch
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
            Dim dal As New ApPaymentBatchDAL
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
            Dim dal As New ApPaymentBatchDAL            
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
            If row(ApPaymentBatchDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ApPaymentBatchDAL.COL_NAME_AP_PAYMENT_BATCH_ID), Byte()))
            End If
        End Get
    End Property
	
    <ValueMandatory(""),ValidStringLength("", Max:=400)> _
    Public Property BatchNumber As String
        Get
            CheckDeleted()
            If row(ApPaymentBatchDAL.COL_NAME_BATCH_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ApPaymentBatchDAL.COL_NAME_BATCH_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ApPaymentBatchDAL.COL_NAME_BATCH_NUMBER, Value)
        End Set
    End Property
	
	
    <ValueMandatory("")> _
    Public Property VendorId As Guid
        Get
            CheckDeleted()
            If row(ApPaymentBatchDAL.COL_NAME_VENDOR_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ApPaymentBatchDAL.COL_NAME_VENDOR_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ApPaymentBatchDAL.COL_NAME_VENDOR_ID, Value)
        End Set
    End Property
	
	
    
    Public Property VendorAddressId As Guid
        Get
            CheckDeleted()
            If row(ApPaymentBatchDAL.COL_NAME_VENDOR_ADDRESS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ApPaymentBatchDAL.COL_NAME_VENDOR_ADDRESS_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ApPaymentBatchDAL.COL_NAME_VENDOR_ADDRESS_ID, Value)
        End Set
    End Property
	
	
    <ValueMandatory("")> _
    Public Property Amount As DecimalType
        Get
            CheckDeleted()
            If row(ApPaymentBatchDAL.COL_NAME_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(ApPaymentBatchDAL.COL_NAME_AMOUNT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ApPaymentBatchDAL.COL_NAME_AMOUNT, Value)
        End Set
    End Property
	
	
    <ValueMandatory(""),ValidStringLength("", Max:=200)> _
    Public Property AccountingPeriod As String
        Get
            CheckDeleted()
            If row(ApPaymentBatchDAL.COL_NAME_ACCOUNTING_PERIOD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ApPaymentBatchDAL.COL_NAME_ACCOUNTING_PERIOD), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ApPaymentBatchDAL.COL_NAME_ACCOUNTING_PERIOD, Value)
        End Set
    End Property
	
	
    <ValueMandatory(""),ValidStringLength("", Max:=400)> _
    Public Property PaymentStatusXcd As String
        Get
            CheckDeleted()
            If row(ApPaymentBatchDAL.COL_NAME_PAYMENT_STATUS_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ApPaymentBatchDAL.COL_NAME_PAYMENT_STATUS_XCD), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ApPaymentBatchDAL.COL_NAME_PAYMENT_STATUS_XCD, Value)
        End Set
    End Property
	
	
    <ValueMandatory(""),ValidStringLength("", Max:=4)> _
    Public Property Distributed As String
        Get
            CheckDeleted()
            If row(ApPaymentBatchDAL.COL_NAME_DISTRIBUTED) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ApPaymentBatchDAL.COL_NAME_DISTRIBUTED), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ApPaymentBatchDAL.COL_NAME_DISTRIBUTED, Value)
        End Set
    End Property
	
	
    <ValueMandatory(""),ValidStringLength("", Max:=4)> _
    Public Property Posted As String
        Get
            CheckDeleted()
            If row(ApPaymentBatchDAL.COL_NAME_POSTED) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ApPaymentBatchDAL.COL_NAME_POSTED), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ApPaymentBatchDAL.COL_NAME_POSTED, Value)
        End Set
    End Property
	
	
    <ValidStringLength("", Max:=400)> _
    Public Property Paymentsource As String
        Get
            CheckDeleted()
            If row(ApPaymentBatchDAL.COL_NAME_PAYMENTSOURCE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ApPaymentBatchDAL.COL_NAME_PAYMENTSOURCE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ApPaymentBatchDAL.COL_NAME_PAYMENTSOURCE, Value)
        End Set
    End Property
	
	
   

#End Region

#Region "Public Members"
    Public Overrides Sub Save()         
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ApPaymentBatchDAL
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

    Public Shared sub ValidatePaymentBatch(vendorId As Guid, batchNumber As String, ByRef errCode As Integer, ByRef errMsg As String)
        Dim dal As New ApPaymentBatchDAL
        dal.ValidatePaymentBatch(vendorId, batchNumber, errCode, errMsg)
    End sub
#End Region

#Region "DataView Retrieveing Methods"
    
#End Region

End Class


