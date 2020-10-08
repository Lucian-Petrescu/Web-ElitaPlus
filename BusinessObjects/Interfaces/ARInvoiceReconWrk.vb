Public Class ARInvoiceReconWrk
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_AR_INVOICE_INTERFACE"
    Public Const TABLE_KEY_NAME As String = COL_NAME_INVOICE_INTERFACE_ID

    Public Const COL_NAME_INVOICE_INTERFACE_ID As String = "invoice_interface_id"
    Public Const COL_NAME_DEALERFILE_PROCESSED_ID As String = "dealerfile_processed_id"
    Public Const COL_NAME_RECORD_TYPE As String = "record_type"
    Public Const COL_NAME_CERTIFICATE As String = "certificate"
    Public Const COL_NAME_INVOICE_NUMBER As String = "invoice_number"
    Public Const COL_NAME_INVOICE_PERIOD_START_DATE As String = "invoice_period_start_date"
    Public Const COL_NAME_INVOICE_PERIOD_END_DATE As String = "invoice_period_end_date"
    Public Const COL_NAME_INVOICE_DATE As String = "invoice_date"
    Public Const COL_NAME_INVOCE_DUE_DATE As String = "invoce_due_date"
    Public Const COL_NAME_BILL_TO_ADDRESS_ID As String = "bill_to_address_id"
    Public Const COL_NAME_SHIP_TO_ADDRESS_ID As String = "ship_to_address_id"
    Public Const COL_NAME_CURRENCY_CODE As String = "currency_code"
    Public Const COL_NAME_EXCHANGE_RATE As String = "exchange_rate"
    Public Const COL_NAME_LINE_TYPE As String = "line_type"
    Public Const COL_NAME_ITEM_CODE As String = "item_code"
    Public Const COL_NAME_EARNING_PARTER As String = "earning_parter"
    Public Const COL_NAME_SOURCE As String = "source"
    Public Const COL_NAME_REFERENCE As String = "reference"
    Public Const COL_NAME_REFERENCE_ID As String = "reference_id"
    Public Const COL_NAME_INSTALLMENT_NUMBER As String = "installment_number"
    Public Const COL_NAME_AMOUNT As String = "amount"
    Public Const COL_NAME_PARENT_LINE_NUMBER As String = "parent_line_number"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_INVOICE_HEADER_ID As String = "invoice_header_id"
    Public Const COL_NAME_INVOICE_LINE_ID As String = "invoice_line_id"
    Public Const COL_NAME_ENTIRE_RECORD As String = "entire_record"
    Public Const COL_NAME_INVOICE_LOADED As String = "invoice_loaded"
    Public Const COL_NAME_REJECT_CODE As String = "reject_code"
    Public Const COL_NAME_REJECT_REASON As String = "reject_reason"
    Public Const COL_NAME_REJECT_MSG_PARMS As String = "reject_msg_parms"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_UI_PROD_CODE As String = "UI_PROG_CODE"
    Public Const COL_REJECT_MSG_PARMS As String = "REJECT_MSG_PARMS"
    Public Const COL_MSG_PARAMETER_COUNT As String = "MSG_PARAMETER_COUNT"
    Public Const COL_REJECT_REASON As String = "reject_reason"
    Public Const COL_TRANSLATED_MSG As String = "Translated_MSG"

#End Region

#Region "Constructors"

    'Exiting BO
    Public Sub New(id As Guid)
            MyBase.New()
            Dataset = New DataSet
            Load(id)
        End Sub

        'Exiting BO
        Public Sub New(id As Guid, sModifiedDate As String)
            MyBase.New()
            Dataset = New DataSet
            Load(id)
        'Me.VerifyConcurrency(sModifiedDate)
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

        Protected Sub Load()
            Try
            Dim dal As New ARInvoiceReconWrkDAL
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
            Dim dal As New ARInvoiceReconWrkDAL
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
            If Row(ARInvoiceReconWrkDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ARInvoiceReconWrkDAL.COL_NAME_INVOICE_INTERFACE_ID), Byte()))
            End If
            End Get
        End Property

    <ValueMandatory("")>
    Public Property DealerfileProcessedId As Guid
        Get
            CheckDeleted()
            If Row(ARInvoiceReconWrkDAL.COL_NAME_DEALERFILE_PROCESSED_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ARInvoiceReconWrkDAL.COL_NAME_DEALERFILE_PROCESSED_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARInvoiceReconWrkDAL.COL_NAME_DEALERFILE_PROCESSED_ID, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=2)>
    Public Property RecordType As String
        Get
            CheckDeleted()
            If Row(DealerReconWrkDAL.COL_NAME_RECORD_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ARInvoiceReconWrkDAL.COL_NAME_RECORD_TYPE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARInvoiceReconWrkDAL.COL_NAME_RECORD_TYPE, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=20)>
    Public Property Certificate As String
        Get
            CheckDeleted()
            If Row(ARInvoiceReconWrkDAL.COL_NAME_CERTIFICATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ARInvoiceReconWrkDAL.COL_NAME_CERTIFICATE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARInvoiceReconWrkDAL.COL_NAME_CERTIFICATE, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=3)>
        Public Property RejectCode As String
            Get
                CheckDeleted()
                If Row(DealerReconWrkDAL.COL_NAME_REJECT_CODE) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return CType(Row(DealerReconWrkDAL.COL_NAME_REJECT_CODE), String)
                End If
            End Get
            Set
                CheckDeleted()
                SetValue(DealerReconWrkDAL.COL_NAME_REJECT_CODE, Value)
            End Set
        End Property

    Public Property BillToAddressId As Guid
        Get
            CheckDeleted()
            If Row(ARInvoiceReconWrkDAL.COL_NAME_BILL_TO_ADDRESS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ARInvoiceReconWrkDAL.COL_NAME_BILL_TO_ADDRESS_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARInvoiceReconWrkDAL.COL_NAME_BILL_TO_ADDRESS_ID, Value)
        End Set
    End Property

    Public Property ShipToAddressId As Guid
        Get
            CheckDeleted()
            If Row(ARInvoiceReconWrkDAL.COL_NAME_SHIP_TO_ADDRESS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ARInvoiceReconWrkDAL.COL_NAME_SHIP_TO_ADDRESS_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARInvoiceReconWrkDAL.COL_NAME_SHIP_TO_ADDRESS_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=3)>
    Public Property CurrencyCode As String
        Get
            CheckDeleted()
            If Row(ARInvoiceReconWrkDAL.COL_NAME_CURRENCY_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ARInvoiceReconWrkDAL.COL_NAME_CURRENCY_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARInvoiceReconWrkDAL.COL_NAME_CURRENCY_CODE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=100)>
        Public Property RejectReason As String
            Get
                CheckDeleted()
                If Row(DealerReconWrkDAL.COL_NAME_REJECT_REASON) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return CType(Row(DealerReconWrkDAL.COL_NAME_REJECT_REASON), String)
                End If
            End Get
            Set
                CheckDeleted()
                SetValue(DealerReconWrkDAL.COL_NAME_REJECT_REASON, Value)
            End Set
        End Property
    <ValidStringLength("", Max:=24)>
    Public Property ExchangeRate As String
        Get
            CheckDeleted()
            If Row(ARInvoiceReconWrkDAL.COL_NAME_EXCHANGE_RATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ARInvoiceReconWrkDAL.COL_NAME_EXCHANGE_RATE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARInvoiceReconWrkDAL.COL_NAME_EXCHANGE_RATE, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=10)>
    Public Property LineType As String
        Get
            CheckDeleted()
            If Row(ARInvoiceReconWrkDAL.COL_NAME_LINE_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ARInvoiceReconWrkDAL.COL_NAME_LINE_TYPE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARInvoiceReconWrkDAL.COL_NAME_LINE_TYPE, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=100)>
    Public Property RejectMsgParams As String
        Get
            CheckDeleted()
            If Row(ARInvoiceReconWrkDAL.COL_NAME_REJECT_MSG_PARMS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ARInvoiceReconWrkDAL.COL_NAME_REJECT_MSG_PARMS), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARInvoiceReconWrkDAL.COL_NAME_REJECT_MSG_PARMS, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=30)>
    Public Property InvoiceNumber As String
        Get
            CheckDeleted()
            If Row(ARInvoiceReconWrkDAL.COL_NAME_INVOICE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ARInvoiceReconWrkDAL.COL_NAME_INVOICE_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARInvoiceReconWrkDAL.COL_NAME_INVOICE_NUMBER, Value)
        End Set
    End Property

    Public Property InvoicePeriodStartDate As DateType
        Get
            CheckDeleted()
            If Row(ARInvoiceReconWrkDAL.COL_NAME_INVOICE_PERIOD_START_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ARInvoiceReconWrkDAL.COL_NAME_INVOICE_PERIOD_START_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARInvoiceReconWrkDAL.COL_NAME_INVOICE_PERIOD_START_DATE, Value)
        End Set
    End Property

    Public Property InvoicePeriodEndDate As DateType
        Get
            CheckDeleted()
            If Row(ARInvoiceReconWrkDAL.COL_NAME_INVOICE_PERIOD_END_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ARInvoiceReconWrkDAL.COL_NAME_INVOICE_PERIOD_END_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARInvoiceReconWrkDAL.COL_NAME_INVOICE_PERIOD_END_DATE, Value)
        End Set
    End Property

    Public Property InvoiceDate As DateType
        Get
            CheckDeleted()
            If Row(ARInvoiceReconWrkDAL.COL_NAME_INVOICE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ARInvoiceReconWrkDAL.COL_NAME_INVOICE_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARInvoiceReconWrkDAL.COL_NAME_INVOICE_DATE, Value)
        End Set
    End Property
    Public Property InvoiceDueDate As DateType
        Get
            CheckDeleted()
            If Row(ARInvoiceReconWrkDAL.COL_NAME_INVOCE_DUE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ARInvoiceReconWrkDAL.COL_NAME_INVOCE_DUE_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARInvoiceReconWrkDAL.COL_NAME_INVOCE_DUE_DATE, Value)
        End Set
    End Property

    '<ValidStringLength("", Max:=50)>
    '    Public Property CustomerName() As String
    '        Get
    '            CheckDeleted()
    '            If Row(DealerReconWrkDAL.COL_NAME_CUSTOMER_NAME) Is DBNull.Value Then
    '                Return Nothing
    '            Else
    '                Return CType(Row(DealerReconWrkDAL.COL_NAME_CUSTOMER_NAME), String)
    '            End If
    '        End Get
    '        Set(ByVal Value As String)
    '            CheckDeleted()
    '            Me.SetValue(DealerReconWrkDAL.COL_NAME_CUSTOMER_NAME, Value)
    '        End Set
    '    End Property

    <ValidStringLength("", Max:=10)>
    Public Property ItemCode As String
        Get
            CheckDeleted()
            If Row(ARInvoiceReconWrkDAL.COL_NAME_ITEM_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ARInvoiceReconWrkDAL.COL_NAME_ITEM_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARInvoiceReconWrkDAL.COL_NAME_ITEM_CODE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=10)>
    Public Property EarningPartner As String
        Get
            CheckDeleted()
            If Row(ARInvoiceReconWrkDAL.COL_NAME_EARNING_PARTER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ARInvoiceReconWrkDAL.COL_NAME_EARNING_PARTER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARInvoiceReconWrkDAL.COL_NAME_EARNING_PARTER, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=50)>
    Public Property Source As String
        Get
            CheckDeleted()
            If Row(ARInvoiceReconWrkDAL.COL_NAME_SOURCE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ARInvoiceReconWrkDAL.COL_NAME_SOURCE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARInvoiceReconWrkDAL.COL_NAME_SOURCE, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=10)>
    Public Property Reference As String
        Get
            CheckDeleted()
            If Row(ARInvoiceReconWrkDAL.COL_NAME_REFERENCE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ARInvoiceReconWrkDAL.COL_NAME_REFERENCE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARInvoiceReconWrkDAL.COL_NAME_REFERENCE, Value)
        End Set
    End Property

    Public Property ReferenceId As Guid
        Get
            If Row(ARInvoiceReconWrkDAL.COL_NAME_REFERENCE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ARInvoiceReconWrkDAL.COL_NAME_REFERENCE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARInvoiceReconWrkDAL.COL_NAME_REFERENCE_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=99)>
    Public Property InstallmentNumber As String
        Get
            CheckDeleted()
            If Row(ARInvoiceReconWrkDAL.COL_NAME_INSTALLMENT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ARInvoiceReconWrkDAL.COL_NAME_INSTALLMENT_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARInvoiceReconWrkDAL.COL_NAME_INSTALLMENT_NUMBER, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=13)>
    Public Property Amount As String
        Get
            CheckDeleted()
            If Row(ARInvoiceReconWrkDAL.COL_NAME_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ARInvoiceReconWrkDAL.COL_NAME_AMOUNT), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARInvoiceReconWrkDAL.COL_NAME_AMOUNT, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=10)>
    Public Property ParentLineNumber As String
        Get
            CheckDeleted()
            If Row(ARInvoiceReconWrkDAL.COL_NAME_PARENT_LINE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ARInvoiceReconWrkDAL.COL_NAME_PARENT_LINE_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARInvoiceReconWrkDAL.COL_NAME_PARENT_LINE_NUMBER, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=200)>
    Public Property Description As String
        Get
            CheckDeleted()
            If Row(ARInvoiceReconWrkDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ARInvoiceReconWrkDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARInvoiceReconWrkDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property
    Public Property InvoiceHeaderId As String
        Get
            CheckDeleted()
            If Row(ARInvoiceReconWrkDAL.COL_NAME_INVOICE_HEADER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ARInvoiceReconWrkDAL.COL_NAME_INVOICE_HEADER_ID), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARInvoiceReconWrkDAL.COL_NAME_INVOICE_HEADER_ID, Value)
        End Set
    End Property
    Public Property InvoiceLineId As String
        Get
            CheckDeleted()
            If Row(ARInvoiceReconWrkDAL.COL_NAME_INVOICE_LINE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ARInvoiceReconWrkDAL.COL_NAME_INVOICE_LINE_ID), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARInvoiceReconWrkDAL.COL_NAME_INVOICE_LINE_ID, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=3000)>
    Public Property EntireRecord As String
        Get
            CheckDeleted()
            If Row(ARInvoiceReconWrkDAL.COL_NAME_ENTIRE_RECORD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ARInvoiceReconWrkDAL.COL_NAME_ENTIRE_RECORD), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARInvoiceReconWrkDAL.COL_NAME_ENTIRE_RECORD, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=1)>
    Public Property InvoiceLoaded As String
        Get
            CheckDeleted()
            If Row(ARInvoiceReconWrkDAL.COL_NAME_INVOICE_LOADED) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ARInvoiceReconWrkDAL.COL_NAME_INVOICE_LOADED), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARInvoiceReconWrkDAL.COL_NAME_INVOICE_LOADED, Value)
        End Set
    End Property



#End Region

#Region "Custom Validation"

#End Region

#Region "External Properties"

    Shared ReadOnly Property CompanyId(DealerfileProcessedId As Guid) As Guid
            Get
                Dim oDealerfileProcessed As New DealerFileProcessed(DealerfileProcessedId)
                Dim oDealer As New Dealer(oDealerfileProcessed.DealerId)
                Dim oCompanyId As Guid = oDealer.CompanyId
                Return oCompanyId
            End Get
        End Property

#End Region

#Region "Public Members"
    'Public Shared Sub UpdateHeaderCount(ByVal dealerFileProcessedId As Guid)
    'Dim dal As New ARInvoiceReconWrkDAL
    'dal.UpdateHeaderCount(dealerFileProcessedId)
    'End Sub


    Public Overrides Sub Save()
            Try
                MyBase.Save()
                If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ARInvoiceReconWrkDAL
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

    Public Shared Function LoadList(dealerfileProcessedID As Guid,
                                    recordMode As String,
                                    recordType As String,
                                    rejectCode As String,
                                    rejectReason As String,
                                    parentFile As String,
                                    PageIndex As Integer,
                                    Pagesize As Integer,
                                    SortExpression As String) As DataView
        Try
            Dim dal As New ARInvoiceReconWrkDAL
            Dim ds As DataSet

            ds = dal.LoadList(dealerfileProcessedID, ElitaPlusIdentity.Current.ActiveUser.LanguageId,
                              recordMode, recordType, rejectCode, rejectReason, parentFile, PageIndex, Pagesize, SortExpression)

            Return (ds.Tables(ARInvoiceReconWrkDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function LoadRejectList(dealerfileProcessedID As Guid) As DataView
        Try
            Dim dal As New ARInvoiceReconWrkDAL
            Dim ds As DataSet

            ds = dal.LoadRejectList(dealerfileProcessedID)
            Return (ds.Tables(ARInvoiceReconWrkDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function
#End Region


End Class
