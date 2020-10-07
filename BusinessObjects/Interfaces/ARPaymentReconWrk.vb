Public Class ARPaymentReconWrk
    Inherits BusinessObjectBase


#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Dataset = New DataSet
        Load(id)
    End Sub

    'Exiting BO
    Public Sub New(ByVal id As Guid, ByVal sModifiedDate As String)
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
    Public Sub New(ByVal id As Guid, ByVal familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load()
    End Sub

    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New ARPaymentReconWrkDAL
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

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New ARPaymentReconWrkDAL
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
            If Row(ARPaymentReconWrkDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ARPaymentReconWrkDAL.TABLE_KEY_NAME), Byte()))
            End If
        End Get
    End Property
    Public Property DealerId As Guid
        Get
            CheckDeleted()
            If Row(ARPaymentReconWrkDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ARPaymentReconWrkDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARPaymentReconWrkDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property DealerfileProcessedId As Guid
        Get
            CheckDeleted()
            If Row(ARPaymentReconWrkDAL.COL_NAME_DEALERFILE_PROCESSED_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ARPaymentReconWrkDAL.COL_NAME_DEALERFILE_PROCESSED_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARPaymentReconWrkDAL.COL_NAME_DEALERFILE_PROCESSED_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=100)>
    Public Property RejectReason As String
        Get
            CheckDeleted()
            If Row(ARPaymentReconWrkDAL.COL_NAME_REJECT_REASON) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ARPaymentReconWrkDAL.COL_NAME_REJECT_REASON), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARPaymentReconWrkDAL.COL_NAME_REJECT_REASON, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1)>
    Public Property PaymentLoaded As String
        Get
            CheckDeleted()
            If Row(ARPaymentReconWrkDAL.COL_NAME_PAYMENT_LOADED) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ARPaymentReconWrkDAL.COL_NAME_PAYMENT_LOADED), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARPaymentReconWrkDAL.COL_NAME_PAYMENT_LOADED, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=2)>
    Public Property RecordType As String
        Get
            CheckDeleted()
            If Row(ARPaymentReconWrkDAL.COL_NAME_RECORD_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ARPaymentReconWrkDAL.COL_NAME_RECORD_TYPE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARPaymentReconWrkDAL.COL_NAME_RECORD_TYPE, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=3)>
    Public Property RejectCode As String
        Get
            CheckDeleted()
            If Row(ARPaymentReconWrkDAL.COL_NAME_REJECT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ARPaymentReconWrkDAL.COL_NAME_REJECT_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARPaymentReconWrkDAL.COL_NAME_REJECT_CODE, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=4)>
    Public Property PostPrePaid As String
        Get
            CheckDeleted()
            If Row(ARPaymentReconWrkDAL.COL_NAME_POST_PRE_PAID) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ARPaymentReconWrkDAL.COL_NAME_POST_PRE_PAID), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARPaymentReconWrkDAL.COL_NAME_POST_PRE_PAID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=20)>
    Public Property Certificate As String
        Get
            CheckDeleted()
            If Row(ARPaymentReconWrkDAL.COL_NAME_CERTIFICATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ARPaymentReconWrkDAL.COL_NAME_CERTIFICATE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARPaymentReconWrkDAL.COL_NAME_CERTIFICATE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=10)>
    Public Property SubscriberNumber As String
        Get
            CheckDeleted()
            If Row(ARPaymentReconWrkDAL.COL_NAME_SUBSCRIBER_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ARPaymentReconWrkDAL.COL_NAME_SUBSCRIBER_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARPaymentReconWrkDAL.COL_NAME_SUBSCRIBER_NUMBER, Value)
        End Set
    End Property

    Public Property PaymentAmount As DecimalType
        Get
            CheckDeleted()
            If Row(ARPaymentReconWrkDAL.COL_NAME_PAYMENT_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ARPaymentReconWrkDAL.COL_NAME_PAYMENT_AMOUNT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARPaymentReconWrkDAL.COL_NAME_PAYMENT_AMOUNT, Value)
        End Set
    End Property



    Public Property PaymentDate As DateType
        Get
            CheckDeleted()
            If Row(ARPaymentReconWrkDAL.COL_NAME_PAYMENT_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ARPaymentReconWrkDAL.COL_NAME_PAYMENT_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARPaymentReconWrkDAL.COL_NAME_PAYMENT_DATE, Value)
        End Set
    End Property



    Public Property InvoiceDate As DateType
        Get
            CheckDeleted()
            If Row(ARPaymentReconWrkDAL.COL_NAME_INVOICE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ARPaymentReconWrkDAL.COL_NAME_INVOICE_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARPaymentReconWrkDAL.COL_NAME_INVOICE_DATE, Value)
        End Set
    End Property
    Public Property InvoicePeriodStartDate As DateType
        Get
            CheckDeleted()
            If Row(ARPaymentReconWrkDAL.COL_NAME_INVOICE_PERIOD_START_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ARPaymentReconWrkDAL.COL_NAME_INVOICE_PERIOD_START_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARPaymentReconWrkDAL.COL_NAME_INVOICE_PERIOD_START_DATE, Value)
        End Set
    End Property
    Public Property InvoicePeriodEndDate As DateType
        Get
            CheckDeleted()
            If Row(ARPaymentReconWrkDAL.COL_NAME_INVOICE_PERIOD_END_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ARPaymentReconWrkDAL.COL_NAME_INVOICE_PERIOD_END_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARPaymentReconWrkDAL.COL_NAME_INVOICE_PERIOD_END_DATE, Value)
        End Set
    End Property

    '<ValidStringLength("", Max:=20)>
    'Public Property RejectMsgParams() As String
    '    Get
    '        CheckDeleted()
    '        If Row(ARPaymentReconWrkDAL.COL_NAME_REJECT_MSG_PARAMS) Is DBNull.Value Then
    '            Return Nothing
    '        Else
    '            Return CType(Row(ARPaymentReconWrkDAL.COL_NAME_REJECT_MSG_PARAMS), String)
    '        End If
    '    End Get
    '    Set(ByVal Value As String)
    '        CheckDeleted()
    '        Me.SetValue(ARPaymentReconWrkDAL.COL_NAME_REJECT_MSG_PARAMS, Value)
    '    End Set
    'End Property
    <ValidStringLength("", Max:=10)>
    Public Property Reference As String
        Get
            CheckDeleted()
            If Row(ARPaymentReconWrkDAL.COL_NAME_REFERENCE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ARPaymentReconWrkDAL.COL_NAME_REFERENCE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARPaymentReconWrkDAL.COL_NAME_REFERENCE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)>
    Public Property Source As String
        Get
            CheckDeleted()
            If Row(ARPaymentReconWrkDAL.COL_NAME_SOURCE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ARPaymentReconWrkDAL.COL_NAME_SOURCE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARPaymentReconWrkDAL.COL_NAME_SOURCE, Value)
        End Set
    End Property

    '<ValidStringLength("", Max:=20)>
    'Public Property PaymentId() As String
    '    Get
    '        CheckDeleted()
    '        If Row(ARPaymentReconWrkDAL.COL_NAME_PAYMENT_ID) Is DBNull.Value Then
    '            Return Nothing
    '        Else
    '            Return CType(Row(ARPaymentReconWrkDAL.COL_NAME_PAYMENT_ID), String)
    '        End If
    '    End Get
    '    Set(ByVal Value As String)
    '        CheckDeleted()
    '        Me.SetValue(ARPaymentReconWrkDAL.COL_NAME_PAYMENT_ID, Value)
    '    End Set
    'End Property

    <ValidStringLength("", Max:=30)>
    Public Property InvoiceNumber As String
        Get
            CheckDeleted()
            If Row(ARPaymentReconWrkDAL.COL_NAME_INVOICE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ARPaymentReconWrkDAL.COL_NAME_INVOICE_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARPaymentReconWrkDAL.COL_NAME_INVOICE_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=30)>
    Public Property CreditCardNum As String
        Get
            CheckDeleted()
            If Row(ARPaymentReconWrkDAL.COL_NAME_CREDIT_CARD_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(ARPaymentReconWrkDAL.COL_NAME_CREDIT_CARD_NUMBER), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARPaymentReconWrkDAL.COL_NAME_CREDIT_CARD_NUMBER, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=3000)>
    Public Property EntireRecord As String
        Get
            CheckDeleted()
            If Row(ARPaymentReconWrkDAL.COL_NAME_ENTIRE_RECORD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ARPaymentReconWrkDAL.COL_NAME_ENTIRE_RECORD), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARPaymentReconWrkDAL.COL_NAME_ENTIRE_RECORD, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=30)>
    Public Property PaymentEntityCode As String
        Get
            CheckDeleted()
            If Row(ARPaymentReconWrkDAL.COL_NAME_PAYMENT_ENTITY_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ARPaymentReconWrkDAL.COL_NAME_PAYMENT_ENTITY_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARPaymentReconWrkDAL.COL_NAME_PAYMENT_ENTITY_CODE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=2)>
    Public Property CurrencyCode As String
        Get
            CheckDeleted()
            If Row(ARPaymentReconWrkDAL.COL_NAME_CURRENTCY_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ARPaymentReconWrkDAL.COL_NAME_CURRENTCY_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARPaymentReconWrkDAL.COL_NAME_CURRENTCY_CODE, Value)
        End Set
    End Property


    '<ValidStringLength("", Max:=5)>
    'Public Property ProductCode() As String
    '    Get
    '        CheckDeleted()
    '        If Row(ARPaymentReconWrkDAL.COL_NAME_PRODUCT_CODE) Is DBNull.Value Then
    '            Return Nothing
    '        Else
    '            Return CType(Row(ARPaymentReconWrkDAL.COL_NAME_PRODUCT_CODE), String)
    '        End If
    '    End Get
    '    Set(ByVal Value As String)
    '        CheckDeleted()
    '        Me.SetValue(ARPaymentReconWrkDAL.COL_NAME_PRODUCT_CODE, Value)
    '    End Set
    'End Property


    <ValidStringLength("", Max:=30)>
    Public Property PaymentMethod As String
        Get
            CheckDeleted()
            If Row(ARPaymentReconWrkDAL.COL_NAME_PAYMENT_METHOD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ARPaymentReconWrkDAL.COL_NAME_PAYMENT_METHOD), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARPaymentReconWrkDAL.COL_NAME_PAYMENT_METHOD, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=100)>
    Public Property Model As String
        Get
            CheckDeleted()
            If Row(ARPaymentReconWrkDAL.COL_NAME_MODEL) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(ARPaymentReconWrkDAL.COL_NAME_MODEL), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARPaymentReconWrkDAL.COL_NAME_MODEL, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=50)>
    Public Property Manufacturer As String
        Get
            CheckDeleted()
            If Row(ARPaymentReconWrkDAL.COL_NAME_MANUFACTURER) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(ARPaymentReconWrkDAL.COL_NAME_MANUFACTURER), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARPaymentReconWrkDAL.COL_NAME_MANUFACTURER, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=30)>
    Public Property SerialNumber As String
        Get
            CheckDeleted()
            If Row(ARPaymentReconWrkDAL.COL_NAME_SERIAL_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(ARPaymentReconWrkDAL.COL_NAME_SERIAL_NUMBER), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARPaymentReconWrkDAL.COL_NAME_SERIAL_NUMBER, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=20)>
    Public Property MobileNumber As String
        Get
            CheckDeleted()
            If Row(ARPaymentReconWrkDAL.COL_NAME_MOBILE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(ARPaymentReconWrkDAL.COL_NAME_MOBILE_NUMBER), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARPaymentReconWrkDAL.COL_NAME_MOBILE_NUMBER, Value)
        End Set
    End Property


    <ValidNumericRange("", Min:=0, Max:=2)>
    Public Property InstallmentNumber As LongType
        Get
            CheckDeleted()
            If Row(ARPaymentReconWrkDAL.COL_NAME_INSTALLMENT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ARPaymentReconWrkDAL.COL_NAME_INSTALLMENT_NUMBER), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARPaymentReconWrkDAL.COL_NAME_INSTALLMENT_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=30)>
    Public Property ApplicationMode As String
        Get
            CheckDeleted()
            If Row(ARPaymentReconWrkDAL.COL_NAME_APPLICATION_MODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ARPaymentReconWrkDAL.COL_NAME_APPLICATION_MODE), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ARPaymentReconWrkDAL.COL_NAME_APPLICATION_MODE, Value)
        End Set
    End Property
#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ARPaymentReconWrkDAL
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

    Public Shared Function LoadList(ByVal dealerfileProcessedID As Guid,
                                    ByVal recordMode As String,
                                    ByVal recordType As String,
                                    ByVal rejectCode As String,
                                    ByVal rejectReason As String,
                                    ByVal parentFile As String,
                                    ByVal PageIndex As Integer,
                                    ByVal Pagesize As Integer,
                                    ByVal SortExpression As String) As DataView
        Try
            Dim dal As New ARPaymentReconWrkDAL
            Dim ds As DataSet

            ds = dal.LoadList(dealerfileProcessedID, ElitaPlusIdentity.Current.ActiveUser.LanguageId,
                              recordMode, recordType, rejectCode, rejectReason, parentFile, PageIndex, Pagesize, SortExpression)

            Return (ds.Tables(ARPaymentReconWrkDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function LoadRejectList(ByVal dealerfileProcessedID As Guid) As DataView
        Try
            Dim dal As New ARPaymentReconWrkDAL
            Dim ds As DataSet

            ds = dal.LoadRejectList(dealerfileProcessedID)
            Return (ds.Tables(ARPaymentReconWrkDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

#End Region

End Class
