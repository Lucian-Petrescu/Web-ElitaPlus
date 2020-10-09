'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (10/2/2019)  ********************

Public Class ApInvoiceLines
    Inherits BusinessObjectBase

#Region "Constant"
    Public Const AP_LINE_ID As String = "ap_invoice_lines_id"
    Public Const LINE_NUMBER_COL As String = "Line_Number"
    Public Const LINE_TYPE_COL As String = "Line_type"
    Public Const ITEM_CODE_COL As String = "Vendor_Item_Code"
    Public Const DESCRIPTION_COL As String = "Description"
    Public Const QUANTITY_COL As String = "Quantity"
    Public Const UNIT_PRICE_COL As String = "Unit_Price"
    Public Const TOTAL_PRICE_COL As String = "Total_Price"
    Public Const UNIT_OF_MEASUREMENT_COL As String = "UOM_XCD"
    Public Const PO_NUMBER_COL As String = "po_number"


#End Region

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
            Dim dal As New ApInvoiceLinesDAL
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
            Dim dal As New ApInvoiceLinesDAL            
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
            If row(ApInvoiceLinesDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ApInvoiceLinesDAL.COL_NAME_AP_INVOICE_LINES_ID), Byte()))
            End If
        End Get
    End Property
	
    <ValueMandatory("")> _
    Public Property ApInvoiceHeaderId As Guid
        Get
            CheckDeleted()
            If row(ApInvoiceLinesDAL.COL_NAME_AP_INVOICE_HEADER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ApInvoiceLinesDAL.COL_NAME_AP_INVOICE_HEADER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ApInvoiceLinesDAL.COL_NAME_AP_INVOICE_HEADER_ID, Value)
        End Set
    End Property
	
	
    <ValueMandatory("Line Number")> _
    Public Property LineNumber As LongType
        Get
            CheckDeleted()
            If row(ApInvoiceLinesDAL.COL_NAME_LINE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(ApInvoiceLinesDAL.COL_NAME_LINE_NUMBER), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ApInvoiceLinesDAL.COL_NAME_LINE_NUMBER, Value)
        End Set
    End Property
	
	
    <ValueMandatory("Line Type"),ValidStringLength("", Max:=400)> _
    Public Property LineType As String
        Get
            CheckDeleted()
            If row(ApInvoiceLinesDAL.COL_NAME_LINE_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ApInvoiceLinesDAL.COL_NAME_LINE_TYPE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ApInvoiceLinesDAL.COL_NAME_LINE_TYPE, Value)
        End Set
    End Property
	
	
    <ValueMandatoryVendorItem("Vendor Item Code"),ValidStringLength("", Max:=400)> _
    Public Property VendorItemCode As String
        Get
            CheckDeleted()
            If row(ApInvoiceLinesDAL.COL_NAME_VENDOR_ITEM_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ApInvoiceLinesDAL.COL_NAME_VENDOR_ITEM_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ApInvoiceLinesDAL.COL_NAME_VENDOR_ITEM_CODE, Value)
        End Set
    End Property
	
	
    <ValueMandatoryVendorItemDescription("Description"),ValidStringLength("", Max:=1000)> _
    Public Property Description As String
        Get
            CheckDeleted()
            If row(ApInvoiceLinesDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ApInvoiceLinesDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ApInvoiceLinesDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property
	
	
    <ValueMandatoryquantity("Quantity")> _
    Public Property Quantity As DecimalType
        Get
            CheckDeleted()
            If row(ApInvoiceLinesDAL.COL_NAME_QUANTITY) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(ApInvoiceLinesDAL.COL_NAME_QUANTITY), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ApInvoiceLinesDAL.COL_NAME_QUANTITY, Value)
        End Set
    End Property
	
	
    <ValueMandatoryUom("Unit of Measurement"),ValidStringLength("", Max:=400)> _
    Public Property UomXcd As String
        Get
            CheckDeleted()
            If row(ApInvoiceLinesDAL.COL_NAME_UOM_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ApInvoiceLinesDAL.COL_NAME_UOM_XCD), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ApInvoiceLinesDAL.COL_NAME_UOM_XCD, Value)
        End Set
    End Property
	
	
    <ValueMandatory("")> _
    Public Property MatchedQuantity As DecimalType
        Get
            CheckDeleted()
            If row(ApInvoiceLinesDAL.COL_NAME_MATCHED_QUANTITY) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(ApInvoiceLinesDAL.COL_NAME_MATCHED_QUANTITY), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ApInvoiceLinesDAL.COL_NAME_MATCHED_QUANTITY, Value)
        End Set
    End Property
	
	
    <ValueMandatory("")> _
    Public Property PaidQuantity As DecimalType
        Get
            CheckDeleted()
            If row(ApInvoiceLinesDAL.COL_NAME_PAID_QUANTITY) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(ApInvoiceLinesDAL.COL_NAME_PAID_QUANTITY), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ApInvoiceLinesDAL.COL_NAME_PAID_QUANTITY, Value)
        End Set
    End Property
	
	
    <ValueMandatoryUnitPrice("Unit Price")> _
    Public Property UnitPrice As DecimalType
        Get
            CheckDeleted()
            If row(ApInvoiceLinesDAL.COL_NAME_UNIT_PRICE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(ApInvoiceLinesDAL.COL_NAME_UNIT_PRICE), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ApInvoiceLinesDAL.COL_NAME_UNIT_PRICE, Value)
        End Set
    End Property
	
	
    <ValueMandatoryTotalPrice("")> _
    Public Property TotalPrice As DecimalType
        Get
            CheckDeleted()
            If row(ApInvoiceLinesDAL.COL_NAME_TOTAL_PRICE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(ApInvoiceLinesDAL.COL_NAME_TOTAL_PRICE), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ApInvoiceLinesDAL.COL_NAME_TOTAL_PRICE, Value)
        End Set
    End Property
	
	
    
    Public Property ParentLineNumber As LongType
        Get
            CheckDeleted()
            If row(ApInvoiceLinesDAL.COL_NAME_PARENT_LINE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(ApInvoiceLinesDAL.COL_NAME_PARENT_LINE_NUMBER), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ApInvoiceLinesDAL.COL_NAME_PARENT_LINE_NUMBER, Value)
        End Set
    End Property
	
	
    <ValidStringLength("", Max:=400)> _
    Public Property PoNumber As String
        Get
            CheckDeleted()
            If row(ApInvoiceLinesDAL.COL_NAME_PO_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ApInvoiceLinesDAL.COL_NAME_PO_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ApInvoiceLinesDAL.COL_NAME_PO_NUMBER, Value)
        End Set
    End Property
	
	
    
    Public Property PoDate As DateType
        Get
            CheckDeleted()
            If row(ApInvoiceLinesDAL.COL_NAME_PO_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(ApInvoiceLinesDAL.COL_NAME_PO_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ApInvoiceLinesDAL.COL_NAME_PO_DATE, Value)
        End Set
    End Property
	
	
    
    Public Property BillingPeriodStartDate As DateType
        Get
            CheckDeleted()
            If row(ApInvoiceLinesDAL.COL_NAME_BILLING_PERIOD_START_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(ApInvoiceLinesDAL.COL_NAME_BILLING_PERIOD_START_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ApInvoiceLinesDAL.COL_NAME_BILLING_PERIOD_START_DATE, Value)
        End Set
    End Property
	
	
    
    Public Property BillingPeriodEndDate As DateType
        Get
            CheckDeleted()
            If row(ApInvoiceLinesDAL.COL_NAME_BILLING_PERIOD_END_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(ApInvoiceLinesDAL.COL_NAME_BILLING_PERIOD_END_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ApInvoiceLinesDAL.COL_NAME_BILLING_PERIOD_END_DATE, Value)
        End Set
    End Property
	
	
    <ValidStringLength("", Max:=400)> _
    Public Property ReferenceNumber As String
        Get
            CheckDeleted()
            If row(ApInvoiceLinesDAL.COL_NAME_REFERENCE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ApInvoiceLinesDAL.COL_NAME_REFERENCE_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ApInvoiceLinesDAL.COL_NAME_REFERENCE_NUMBER, Value)
        End Set
    End Property
	
	
    <ValidStringLength("", Max:=1000)> _
    Public Property VendorTransactionType As String
        Get
            CheckDeleted()
            If row(ApInvoiceLinesDAL.COL_NAME_VENDOR_TRANSACTION_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ApInvoiceLinesDAL.COL_NAME_VENDOR_TRANSACTION_TYPE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ApInvoiceLinesDAL.COL_NAME_VENDOR_TRANSACTION_TYPE, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ApInvoiceLinesDAL
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
    Public Sub DeleteInvoiceLine()
        Dim dal As New ApInvoiceLinesDAL
        dal.DeleteInvoiceLine(Row)
    End Sub

#End Region

#Region "DataView Retrieveing Methods"
    Public Function GetApInvoiceLines(accountPayableInvoiceHeaderId As Guid) As DataView
        Try
            Dim dal As New ApInvoiceLinesDAL
            Dim ds As DataSet = New DataSet

            ds = dal.GetApInvoiceLines(ds, accountPayableInvoiceHeaderId)
            Return ds.Tables(ApInvoiceLinesDAL.TABLE_NAME).DefaultView

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    public Function GetAuthorization(serviceCenterId As Guid , claimNumber As String, authorizationNumber As string) As Dataview
        Try
            Dim dal As New ApInvoiceLinesDAL
            Dim ds As DataSet = New DataSet

            ds = dal.GetAuthorization(serviceCenterId, claimNumber,authorizationNumber)
            Return ds.Tables(ApInvoiceLinesDAL.TABLE_NAME).DefaultView

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Public function GetPoLines(authorizationIds As Generic.List(Of Guid)) As Dataview
        Try
            Dim dal As New ApInvoiceLinesDAL
            Dim ds As DataSet = New DataSet

            ds = dal.GetPoLines( authorizationIds)
            Return ds.Tables(ApInvoiceLinesDAL.TABLE_NAME).DefaultView

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
        Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End function

#End Region


#Region "APInvoiceLinesDV Dataview"
    Public Class APInvoiceLinesDV
        Inherits DataView

        Public Const COL_INVOICE_lines_ID As String = "ap_invoice_lines_id"
        Public Const COL_LINE_NUMBER As String = "line_number"
        Public Const COL_LINE_TYPE As String = "line_type"
        Public Const COL_ITEM_CODE As String = "item_code"
        Public Const COL_DESCRIPTION As String = "description"
        Public Const COL_QUANTITY As String = "quantity"
        Public Const COL_UNIT_PRICE As String = "unit_price"
        Public Const COL_TOTAL_PRICE As String = "total_price"
        Public Const COL_MATCHED_QUANTITY As String = "matched_quantity"
        Public Const COL_PAID_QUANTITY As String = "paid_quantity"
        Public Const COL_PO_NUMBER As String = "po_number"
        Public Const COL_PO_QUANTITY As String = "po_line_quantity"
        Public Const COL_PAYMENT_STATUS As String = "payment_status"
        Public Const COL_PAYMENT_SOURCE As String = "payment_source"
        Public Const COL_PAYMENT_DATE As String = "payment_date"
        Public Const COL_UNIT_OF_MEASUREMENT As String = "UOM_XCD"
        Public Const MSG_THE_VALUE_REQUIRED_ITEM_CODE As String = "MSG_THE_VALUE_REQUIRED_ITEM_CODE"
        Public Const MSG_THE_VALUE_REQUIRED_DESCRIPTION As String = "MSG_THE_VALUE_REQUIRED_DESCRIPTION"
        Public Const MSG_THE_VALUE_REQUIRED_UNIT_PRICE As String = "MSG_THE_VALUE_REQUIRED_UNIT_PRICE"
        Public Const MSG_THE_VALUE_REQUIRED_QTY As String = "MSG_THE_VALUE_REQUIRED_QTY"
        Public Const MSG_THE_VALUE_REQUIRED_UOM As String = "MSG_THE_VALUE_REQUIRED_UOM"
        Public Const MSG_THE_VALUE_REQUIRED_TOTAL_PRICE As String = "MSG_THE_VALUE_REQUIRED_TOTAL_PRICE"

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region

#Region "Custom Validation"
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable class ValueMandatoryVendorItem
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, APInvoiceLinesDV.MSG_THE_VALUE_REQUIRED_ITEM_CODE)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj = CType(objectToValidate, ApInvoiceLines)
            If obj.IsNew AndAlso valueToCheck Is Nothing Then
                Return False
            ElseIf valueToCheck.Equals(String.Empty) Then
                Return False
            End If

            Return True
        End Function
    End Class
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable class ValueMandatoryVendorItemDescription
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, APInvoiceLinesDV.MSG_THE_VALUE_REQUIRED_DESCRIPTION)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj = CType(objectToValidate, ApInvoiceLines)
            If obj.IsNew AndAlso valueToCheck Is Nothing Then
                Return False
            ElseIf valueToCheck.Equals(String.Empty) Then
                Return False
            End If

            Return True
        End Function
    End Class
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable class ValueMandatoryQuantity
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, APInvoiceLinesDV.MSG_THE_VALUE_REQUIRED_QTY)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj = CType(objectToValidate, ApInvoiceLines)
            If obj.IsNew AndAlso valueToCheck Is Nothing Then
                Return False
            ElseIf valueToCheck.Equals(String.Empty) Then
                Return False
            End If

            Return True
        End Function
    End Class
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable class ValueMandatoryUnitPrice
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, APInvoiceLinesDV.MSG_THE_VALUE_REQUIRED_UNIT_PRICE)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj = CType(objectToValidate, ApInvoiceLines)
            If obj.IsNew AndAlso valueToCheck Is Nothing Then
                Return False
            ElseIf valueToCheck.Equals(String.Empty) Then
                Return False
            End If

            Return True
        End Function
    End Class
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable class ValueMandatoryTotalPrice
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, APInvoiceLinesDV.MSG_THE_VALUE_REQUIRED_TOTAL_PRICE)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj = CType(objectToValidate, ApInvoiceLines)
            If obj.IsNew AndAlso valueToCheck Is Nothing Then
                Return False
            ElseIf valueToCheck.Equals(String.Empty) Then
                Return False
            End If

            Return True
        End Function
    End Class
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable class ValueMandatoryUom
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, APInvoiceLinesDV.MSG_THE_VALUE_REQUIRED_UOM)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj = CType(objectToValidate, ApInvoiceLines)
            If obj.IsNew AndAlso valueToCheck Is Nothing Then
                Return False
            ElseIf valueToCheck.Equals(String.Empty) Then
                Return False
            End If

            Return True
        End Function
    End Class
#End Region

End Class


