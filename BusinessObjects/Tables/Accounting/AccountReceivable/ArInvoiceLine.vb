'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (10/29/2020)  ********************

Public Class ArInvoiceLine
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
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
    Public Sub New(ByVal id As Guid, ByVal familyDs As DataSet)
        MyBase.New(False)
        Dataset = familyDs
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDs As DataSet)
        MyBase.New(False)
        Dataset = familyDs
        Load()
    End Sub
    
    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()             
        Try
            Dim dal As New ArInvoiceLineDal
            If Dataset.Tables.IndexOf(ArInvoiceLineDal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(ArInvoiceLineDal.TABLE_NAME).NewRow
            Dataset.Tables(ArInvoiceLineDal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            setvalue(ArInvoiceLineDal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize() 
        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal lineId As Guid)               
        Try
            Dim dal As New ArInvoiceLineDal            
            If _isDSCreator Then
                If Not Row Is Nothing Then
                    Dataset.Tables(ArInvoiceLineDal.TABLE_NAME).Rows.Remove(Row)
                End If
            End If
            Row = Nothing
            If Dataset.Tables.IndexOf(ArInvoiceLineDal.TABLE_NAME) >= 0 Then
                Row = FindRow(lineId, ArInvoiceLineDal.TABLE_KEY_NAME, Dataset.Tables(ArInvoiceLineDal.TABLE_NAME))
            End If
            If Row Is Nothing Then 'it is not in the data set, so will bring it from the db
                dal.Load(Dataset, lineId)
                Row = FindRow(lineId, ArInvoiceLineDal.TABLE_KEY_NAME, Dataset.Tables(ArInvoiceLineDal.TABLE_NAME))
            End If
            If Row Is Nothing Then
                Throw New DataNotFoundException
            End If
        Catch ex As DataBaseAccessException
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
            If row(ArInvoiceLineDal.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ArInvoiceLineDal.COL_NAME_INVOICE_LINE_ID), Byte()))
            End If
        End Get
    End Property
	
    <ValueMandatory("")> _
    Public Property InvoiceHeaderId() As Guid
        Get
            CheckDeleted()
            If row(ArInvoiceLineDal.COL_NAME_INVOICE_HEADER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ArInvoiceLineDal.COL_NAME_INVOICE_HEADER_ID), Byte()))
            End If
        End Get
        Set(ByVal value As Guid)
            CheckDeleted()
            SetValue(ArInvoiceLineDal.COL_NAME_INVOICE_HEADER_ID, value)
        End Set
    End Property
	
	
    <ValueMandatory(""),ValidStringLength("", Max:=40)> _
    Public Property LineType() As String
        Get
            CheckDeleted()
            If row(ArInvoiceLineDal.COL_NAME_LINE_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ArInvoiceLineDal.COL_NAME_LINE_TYPE), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            SetValue(ArInvoiceLineDal.COL_NAME_LINE_TYPE, value)
        End Set
    End Property
	
	
    <ValidStringLength("", Max:=40)> _
    Public Property ItemCode() As String
        Get
            CheckDeleted()
            If row(ArInvoiceLineDal.COL_NAME_ITEM_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ArInvoiceLineDal.COL_NAME_ITEM_CODE), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            SetValue(ArInvoiceLineDal.COL_NAME_ITEM_CODE, value)
        End Set
    End Property
	
	
    
    Public Property CertItemCoverageId() As Guid
        Get
            CheckDeleted()
            If row(ArInvoiceLineDal.COL_NAME_CERT_ITEM_COVERAGE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ArInvoiceLineDal.COL_NAME_CERT_ITEM_COVERAGE_ID), Byte()))
            End If
        End Get
        Set(ByVal value As Guid)
            CheckDeleted()
            SetValue(ArInvoiceLineDal.COL_NAME_CERT_ITEM_COVERAGE_ID, value)
        End Set
    End Property
	
	
    <ValueMandatory("")> _
    Public Property Amount() As DecimalType
        Get
            CheckDeleted()
            If row(ArInvoiceLineDal.COL_NAME_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(ArInvoiceLineDal.COL_NAME_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal value As DecimalType)
            CheckDeleted()
            SetValue(ArInvoiceLineDal.COL_NAME_AMOUNT, value)
        End Set
    End Property
	
	
    <ValueMandatory(""),ValidStringLength("", Max:=200)> _
    Public Property ErningParterXcd() As String
        Get
            CheckDeleted()
            If row(ArInvoiceLineDal.COL_NAME_ERNING_PARTER_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ArInvoiceLineDal.COL_NAME_ERNING_PARTER_XCD), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            SetValue(ArInvoiceLineDal.COL_NAME_ERNING_PARTER_XCD, value)
        End Set
    End Property
	
	
    <ValueMandatory("")> _
    Public Property ParentLineNumber() As LongType
        Get
            CheckDeleted()
            If row(ArInvoiceLineDal.COL_NAME_PARENT_LINE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(ArInvoiceLineDal.COL_NAME_PARENT_LINE_NUMBER), Long))
            End If
        End Get
        Set(ByVal value As LongType)
            CheckDeleted()
            SetValue(ArInvoiceLineDal.COL_NAME_PARENT_LINE_NUMBER, value)
        End Set
    End Property
	
	
    <ValidStringLength("", Max:=800)> _
    Public Property Description() As String
        Get
            CheckDeleted()
            If row(ArInvoiceLineDal.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ArInvoiceLineDal.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            SetValue(ArInvoiceLineDal.COL_NAME_DESCRIPTION, value)
        End Set
    End Property
	
	
    
    Public Property InvoicePeriodStartDate() As DateType
        Get
            CheckDeleted()
            If row(ArInvoiceLineDal.COL_NAME_INVOICE_PERIOD_START_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(ArInvoiceLineDal.COL_NAME_INVOICE_PERIOD_START_DATE), Date))
            End If
        End Get
        Set(ByVal value As DateType)
            CheckDeleted()
            SetValue(ArInvoiceLineDal.COL_NAME_INVOICE_PERIOD_START_DATE, value)
        End Set
    End Property
	
	
    
    Public Property InvoicePeriodEndDate() As DateType
        Get
            CheckDeleted()
            If row(ArInvoiceLineDal.COL_NAME_INVOICE_PERIOD_END_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(ArInvoiceLineDal.COL_NAME_INVOICE_PERIOD_END_DATE), Date))
            End If
        End Get
        Set(ByVal value As DateType)
            CheckDeleted()
            SetValue(ArInvoiceLineDal.COL_NAME_INVOICE_PERIOD_END_DATE, value)
        End Set
    End Property
	
	
    
    Public Property RefInvoiceLineId() As Guid
        Get
            CheckDeleted()
            If row(ArInvoiceLineDal.COL_NAME_REF_INVOICE_LINE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ArInvoiceLineDal.COL_NAME_REF_INVOICE_LINE_ID), Byte()))
            End If
        End Get
        Set(ByVal value As Guid)
            CheckDeleted()
            SetValue(ArInvoiceLineDal.COL_NAME_REF_INVOICE_LINE_ID, value)
        End Set
    End Property
	
	
    <ValueMandatory("")> _
    Public Property LineNumber() As LongType
        Get
            CheckDeleted()
            If row(ArInvoiceLineDal.COL_NAME_LINE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(ArInvoiceLineDal.COL_NAME_LINE_NUMBER), Long))
            End If
        End Get
        Set(ByVal value As LongType)
            CheckDeleted()
            SetValue(ArInvoiceLineDal.COL_NAME_LINE_NUMBER, value)
        End Set
    End Property
	
	
    
    Public Property IncomingAmount() As DecimalType
        Get
            CheckDeleted()
            If row(ArInvoiceLineDal.COL_NAME_INCOMING_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(ArInvoiceLineDal.COL_NAME_INCOMING_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal value As DecimalType)
            CheckDeleted()
            SetValue(ArInvoiceLineDal.COL_NAME_INCOMING_AMOUNT, value)
        End Set
    End Property
	
	
   

#End Region

#Region "Public Members"
    Public Overrides Sub Save()         
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ArInvoiceLineDal
                dal.Update(Row)
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Id
                    Dataset = New DataSet
                    Row = Nothing
                    Load(objId)
                End If
            End If
        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
#End Region

#Region "DataView Retrieveing Methods"
    
#End Region

End Class


