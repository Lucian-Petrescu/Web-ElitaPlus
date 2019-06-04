'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (11/3/2004)  ********************

Public Class CertCancellation
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
            Dim dal As New CertCancellationDAL
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
            Dim dal As New CertCancellationDAL
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

#Region "Attributes"
    Dim cancellationReasonDesc As String
    Dim cancellationReasonCode As String
#End Region

#Region "Private Members"
    'Initialization code for new objects
    Private Sub Initialize()
    End Sub
#End Region

#Region "BankInfo"

    Private _bankinfo As bankinfo = Nothing
    Public ReadOnly Property bankinfo() As bankinfo
        Get
            If Me._bankinfo Is Nothing Then
                If Me.BankInfoId.Equals(Guid.Empty) Then
                    Me._bankinfo = New bankinfo(Me.Dataset)
                    Me.BankInfoId = Me._bankinfo.Id
                Else
                    Me._bankinfo = New bankinfo(Me.BankInfoId, Me.Dataset)
                End If
            End If
            Return Me._bankinfo
        End Get
    End Property

#End Region

#Region "PaymentOrderInfo"

    Private _PaymentOrderinfo As PaymentOrderInfo = Nothing
    Public ReadOnly Property PmtOrderinfo(Optional ByVal userBankinfo As Boolean = False) As PaymentOrderInfo
        Get
            If userBankinfo = True Then
                If Me._bankinfo Is Nothing Then
                    If Me.BankInfoId.Equals(Guid.Empty) Then
                        Me._PaymentOrderinfo = New PaymentOrderInfo(Me.Dataset)
                        Me._paymentOrderId = Me._PaymentOrderinfo.Id
                    Else
                        Me._PaymentOrderinfo = New PaymentOrderInfo(Me.BankInfoId, Me.Dataset)
                    End If
                End If
                Return Me._PaymentOrderinfo
            Else
                If Me._PaymentOrderinfo Is Nothing Then
                    If Me._paymentOrderId.Equals(Guid.Empty) Then
                        Me._PaymentOrderinfo = New PaymentOrderInfo(Me.Dataset)
                        Me._paymentOrderId = Me._PaymentOrderinfo.Id
                    Else
                        Me._PaymentOrderinfo = New PaymentOrderInfo(Me._paymentOrderId, Me.Dataset)
                    End If
                End If
                Return Me._PaymentOrderinfo
            End If
        End Get
    End Property

#End Region


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If Row(CertCancellationDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertCancellationDAL.COL_NAME_CERT_CANCELLATION_ID), Byte()))
            End If
        End Get
    End Property
    ' Ani -- this property is needed for bank info saving while cancelling a certificate
    <ValueMandatory("")> _
        Public Property BankInfoId() As Guid
        Get
            CheckDeleted()
            If Row(CertCancellationDAL.COL_NAME_BANKINFO_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertCancellationDAL.COL_NAME_BANKINFO_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertCancellationDAL.COL_NAME_BANKINFO_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
   Public Property CompanyId() As Guid
        Get
            CheckDeleted()
            If Row(CertCancellationDAL.COL_NAME_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertCancellationDAL.COL_NAME_COMPANY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertCancellationDAL.COL_NAME_COMPANY_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property CertId() As Guid
        Get
            CheckDeleted()
            If Row(CertCancellationDAL.COL_NAME_CERT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertCancellationDAL.COL_NAME_CERT_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertCancellationDAL.COL_NAME_CERT_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property CancellationReasonId() As Guid
        Get
            CheckDeleted()
            If Row(CertCancellationDAL.COL_NAME_CANCELLATION_REASON_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertCancellationDAL.COL_NAME_CANCELLATION_REASON_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertCancellationDAL.COL_NAME_CANCELLATION_REASON_ID, Value)
        End Set
    End Property



    Public Property CommissionBreakdownId() As Guid
        Get
            CheckDeleted()
            If Row(CertCancellationDAL.COL_NAME_COMMISSION_BREAKDOWN_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertCancellationDAL.COL_NAME_COMMISSION_BREAKDOWN_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertCancellationDAL.COL_NAME_COMMISSION_BREAKDOWN_ID, Value)
        End Set
    End Property



    Public Property OriginalRegionId() As Guid
        Get
            CheckDeleted()
            If Row(CertCancellationDAL.COL_NAME_ORIGINAL_REGION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertCancellationDAL.COL_NAME_ORIGINAL_REGION_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertCancellationDAL.COL_NAME_ORIGINAL_REGION_ID, Value)
        End Set
    End Property



    Public Property RefundDestId() As Guid
        Get
            CheckDeleted()
            If Row(CertCancellationDAL.COL_NAME_REFUND_DEST_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertCancellationDAL.COL_NAME_REFUND_DEST_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertCancellationDAL.COL_NAME_REFUND_DEST_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property CancellationDate() As DateType
        Get
            CheckDeleted()
            If Row(CertCancellationDAL.COL_NAME_CANCELLATION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(CertCancellationDAL.COL_NAME_CANCELLATION_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(CertCancellationDAL.COL_NAME_CANCELLATION_DATE, Value)
        End Set
    End Property

    Dim _cancellationRequestedDate As Nullable(Of Date) = Nothing
    Public Property CancellationRequestedDate() As Nullable(Of Date)
        Get
            Return _cancellationRequestedDate
        End Get
        Set(ByVal Value As Nullable(Of Date))
            _cancellationRequestedDate = Value
        End Set
    End Property
    <ValidStringLength("", Max:=50)> _
    Public Property Source() As String
        Get
            CheckDeleted()
            If Row(CertCancellationDAL.COL_NAME_SOURCE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertCancellationDAL.COL_NAME_SOURCE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertCancellationDAL.COL_NAME_SOURCE, Value)
        End Set
    End Property



    Public Property ProcessedDate() As DateType
        Get
            CheckDeleted()
            If Row(CertCancellationDAL.COL_NAME_PROCESSED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(CertCancellationDAL.COL_NAME_PROCESSED_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(CertCancellationDAL.COL_NAME_PROCESSED_DATE, Value)
        End Set
    End Property



    Public Property GrossAmtReceived() As DecimalType
        Get
            CheckDeleted()
            If Row(CertCancellationDAL.COL_NAME_GROSS_AMT_RECEIVED) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertCancellationDAL.COL_NAME_GROSS_AMT_RECEIVED), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertCancellationDAL.COL_NAME_GROSS_AMT_RECEIVED, Value)
        End Set
    End Property



    Public Property PremiumWritten() As DecimalType
        Get
            CheckDeleted()
            If Row(CertCancellationDAL.COL_NAME_PREMIUM_WRITTEN) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertCancellationDAL.COL_NAME_PREMIUM_WRITTEN), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertCancellationDAL.COL_NAME_PREMIUM_WRITTEN, Value)
        End Set
    End Property



    Public Property OriginalPremium() As DecimalType
        Get
            CheckDeleted()
            If Row(CertCancellationDAL.COL_NAME_ORIGINAL_PREMIUM) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertCancellationDAL.COL_NAME_ORIGINAL_PREMIUM), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertCancellationDAL.COL_NAME_ORIGINAL_PREMIUM, Value)
        End Set
    End Property



    Public Property LossCost() As DecimalType
        Get
            CheckDeleted()
            If Row(CertCancellationDAL.COL_NAME_LOSS_COST) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertCancellationDAL.COL_NAME_LOSS_COST), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertCancellationDAL.COL_NAME_LOSS_COST, Value)
        End Set
    End Property



    Public Property Commission() As DecimalType
        Get
            CheckDeleted()
            If Row(CertCancellationDAL.COL_NAME_COMMISSION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertCancellationDAL.COL_NAME_COMMISSION), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertCancellationDAL.COL_NAME_COMMISSION, Value)
        End Set
    End Property



    Public Property AdminExpense() As DecimalType
        Get
            CheckDeleted()
            If Row(CertCancellationDAL.COL_NAME_ADMIN_EXPENSE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertCancellationDAL.COL_NAME_ADMIN_EXPENSE), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertCancellationDAL.COL_NAME_ADMIN_EXPENSE, Value)
        End Set
    End Property



    Public Property MarketingExpense() As DecimalType
        Get
            CheckDeleted()
            If Row(CertCancellationDAL.COL_NAME_MARKETING_EXPENSE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertCancellationDAL.COL_NAME_MARKETING_EXPENSE), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertCancellationDAL.COL_NAME_MARKETING_EXPENSE, Value)
        End Set
    End Property



    Public Property Other() As DecimalType
        Get
            CheckDeleted()
            If Row(CertCancellationDAL.COL_NAME_OTHER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertCancellationDAL.COL_NAME_OTHER), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertCancellationDAL.COL_NAME_OTHER, Value)
        End Set
    End Property



    Public Property SalesTax() As DecimalType
        Get
            CheckDeleted()
            If Row(CertCancellationDAL.COL_NAME_SALES_TAX) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertCancellationDAL.COL_NAME_SALES_TAX), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertCancellationDAL.COL_NAME_SALES_TAX, Value)
        End Set
    End Property



    Public Property Tax1() As DecimalType
        Get
            CheckDeleted()
            If Row(CertCancellationDAL.COL_NAME_TAX1) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertCancellationDAL.COL_NAME_TAX1), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertCancellationDAL.COL_NAME_TAX1, Value)
        End Set
    End Property



    Public Property Tax2() As DecimalType
        Get
            CheckDeleted()
            If Row(CertCancellationDAL.COL_NAME_TAX2) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertCancellationDAL.COL_NAME_TAX2), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertCancellationDAL.COL_NAME_TAX2, Value)
        End Set
    End Property



    Public Property Tax3() As DecimalType
        Get
            CheckDeleted()
            If Row(CertCancellationDAL.COL_NAME_TAX3) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertCancellationDAL.COL_NAME_TAX3), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertCancellationDAL.COL_NAME_TAX3, Value)
        End Set
    End Property



    Public Property Tax4() As DecimalType
        Get
            CheckDeleted()
            If Row(CertCancellationDAL.COL_NAME_TAX4) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertCancellationDAL.COL_NAME_TAX4), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertCancellationDAL.COL_NAME_TAX4, Value)
        End Set
    End Property



    Public Property Tax5() As DecimalType
        Get
            CheckDeleted()
            If Row(CertCancellationDAL.COL_NAME_TAX5) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertCancellationDAL.COL_NAME_TAX5), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertCancellationDAL.COL_NAME_TAX5, Value)
        End Set
    End Property



    Public Property Tax6() As DecimalType
        Get
            CheckDeleted()
            If Row(CertCancellationDAL.COL_NAME_TAX6) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertCancellationDAL.COL_NAME_TAX6), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertCancellationDAL.COL_NAME_TAX6, Value)
        End Set
    End Property



    Public Property ComputedRefund() As DecimalType
        Get
            CheckDeleted()
            If Row(CertCancellationDAL.COL_NAME_COMPUTED_REFUND) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertCancellationDAL.COL_NAME_COMPUTED_REFUND), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertCancellationDAL.COL_NAME_COMPUTED_REFUND, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1)> _
    Public Property CreditIssuedFlag() As String
        Get
            CheckDeleted()
            If Row(CertCancellationDAL.COL_NAME_CREDIT_ISSUED_FLAG) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertCancellationDAL.COL_NAME_CREDIT_ISSUED_FLAG), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertCancellationDAL.COL_NAME_CREDIT_ISSUED_FLAG, Value)
        End Set
    End Property



    Public Property CustomerPaid() As DecimalType
        Get
            CheckDeleted()
            If Row(CertCancellationDAL.COL_NAME_CUSTOMER_PAID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertCancellationDAL.COL_NAME_CUSTOMER_PAID), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertCancellationDAL.COL_NAME_CUSTOMER_PAID, Value)
        End Set
    End Property



    Public Property PrincipalPaid() As DecimalType
        Get
            CheckDeleted()
            If Row(CertCancellationDAL.COL_NAME_PRINCIPAL_PAID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertCancellationDAL.COL_NAME_PRINCIPAL_PAID), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertCancellationDAL.COL_NAME_PRINCIPAL_PAID, Value)
        End Set
    End Property



    Public Property RefundAmt() As DecimalType
        Get
            CheckDeleted()
            If Row(CertCancellationDAL.COL_NAME_REFUND_AMT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertCancellationDAL.COL_NAME_REFUND_AMT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertCancellationDAL.COL_NAME_REFUND_AMT, Value)
        End Set
    End Property



    Public Property AssurantGwp() As DecimalType
        Get
            CheckDeleted()
            If Row(CertCancellationDAL.COL_NAME_ASSURANT_GWP) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertCancellationDAL.COL_NAME_ASSURANT_GWP), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertCancellationDAL.COL_NAME_ASSURANT_GWP, Value)
        End Set
    End Property
    Public Property MarkupCommission() As DecimalType
        Get
            CheckDeleted()
            If Row(CertCancellationDAL.COL_NAME_MARKUP_COMMISSION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertCancellationDAL.COL_NAME_MARKUP_COMMISSION), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertCancellationDAL.COL_NAME_MARKUP_COMMISSION, Value)
        End Set
    End Property

    Public Property PaymentMethodId() As Guid
        Get
            CheckDeleted()
            If Row(CertCancellationDAL.COL_NAME_PAYMENT_METHOD_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertCancellationDAL.COL_NAME_PAYMENT_METHOD_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertCancellationDAL.COL_NAME_PAYMENT_METHOD_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=20)> _
    Public Property TrackingNumber() As String
        Get
            CheckDeleted()
            If Row(CertCancellationDAL.COL_NAME_TRACKING_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertCancellationDAL.COL_NAME_TRACKING_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertCancellationDAL.COL_NAME_TRACKING_NUMBER, Value)
        End Set
    End Property

    'need to be mandatory..............
    <ValueMandatory("")> _
    Public Property StatusId() As Guid
        Get
            CheckDeleted()
            If Row(CertCancellationDAL.COL_NAME_STATUS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertCancellationDAL.COL_NAME_STATUS_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertCancellationDAL.COL_NAME_STATUS_ID, Value)
        End Set
    End Property

    ' needs to be mandatory ................
    <ValueMandatory("")> _
    Public Property StatusDate() As DateType
        Get
            CheckDeleted()
            If Row(CertCancellationDAL.COL_NAME_STATUS_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(CertCancellationDAL.COL_NAME_STATUS_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(CertCancellationDAL.COL_NAME_STATUS_DATE, Value)
        End Set
    End Property
    Public Property PayRejectCode() As String
        Get
            CheckDeleted()
            If Row(CertCancellationDAL.COL_NAME_PAY_REJECT_CODE_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertCancellationDAL.COL_NAME_PAY_REJECT_CODE_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertCancellationDAL.COL_NAME_PAY_REJECT_CODE_XCD, Value)
        End Set
    End Property
    Public Property RefundStatus() As String
        Get
            CheckDeleted()
            If Row(CertCancellationDAL.COL_NAME_REFUND_STATUS_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertCancellationDAL.COL_NAME_REFUND_STATUS_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertCancellationDAL.COL_NAME_REFUND_STATUS_XCD, Value)
        End Set
    End Property
    Public Property RefundMethod() As String
        Get
            CheckDeleted()
            If Row(CertCancellationDAL.COL_NAME_REFUND_METHOD_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertCancellationDAL.COL_NAME_REFUND_METHOD_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertCancellationDAL.COL_NAME_REFUND_METHOD_XCD, Value)
        End Set
    End Property
    Public Property MarkupCommissionVat() As DecimalType
        Get
            CheckDeleted()
            If Row(CertCancellationDAL.COL_NAME_MARKUP_COMMISSION_VAT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertCancellationDAL.COL_NAME_MARKUP_COMMISSION_VAT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertCancellationDAL.COL_NAME_MARKUP_COMMISSION_VAT, Value)
        End Set
    End Property
    Private _ErrorExists As Boolean
    Private _ErrorCode As String
    Private _ErrorMsg As String
    Public Property ErrorExists() As Boolean
        Get
            Return _ErrorExists
        End Get
        Set(ByVal value As Boolean)
            _ErrorExists = value
        End Set
    End Property
    Public Property ErrorCode() As String
        Get
            Return _ErrorCode
        End Get
        Set(ByVal value As String)
            _ErrorCode = value
        End Set
    End Property
    Public Property ErrorMsg() As String
        Get
            Return _ErrorMsg
        End Get
        Set(ByVal value As String)
            _ErrorMsg = value
        End Set
    End Property
    Private _InstallmentsPaid As Long
    Public Property InstallmentsPaid() As Long
        Get
            Return _InstallmentsPaid
        End Get
        Set(ByVal value As Long)
            _InstallmentsPaid = value
        End Set
    End Property
    Private _CCAuthorizationNumber As String
    Public Property CCAuthorizationNumber() As String
        Get
            Return _CCAuthorizationNumber
        End Get
        Set(ByVal value As String)
            _CCAuthorizationNumber = value
        End Set
    End Property
    Private _paymentOrderId As Guid
    Public Property paymentOrderId() As Guid
        Get
            Return _paymentOrderId
        End Get
        Set(ByVal value As Guid)
            _paymentOrderId = value
        End Set
    End Property

    'Public Property PaymentReasonId() As Guid
    '    Get
    '        CheckDeleted()
    '        If Row(CertCancellationDAL.COL_NAME_PAYMENT_REASON_ID) Is DBNull.Value Then
    '            Return Nothing
    '        Else
    '            Return New Guid(CType(Row(CertCancellationDAL.COL_NAME_PAYMENT_REASON_ID), Byte()))
    '        End If
    '    End Get
    '    Set(ByVal Value As Guid)
    '        CheckDeleted()
    '        Me.SetValue(CertCancellationDAL.COL_NAME_PAYMENT_REASON_ID, Value)
    '    End Set
    'End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New CertCancellationDAL
                dal.Update(Me.Row)
                'Reload the Data from the DB
                If Me.Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Me.Id
                    Me.Dataset = New Dataset
                    Me.Row = Nothing
                    Me.Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

#End Region

#Region "Shared Methods"

    Public Shared Sub SetProcessCancellationData(ByVal oCertCancData As CertCancellationData, _
                                              ByVal oCert As Certificate, ByVal oCertCanc As CertCancellation)
        Dim oCancellatioReason As New Assurant.ElitaPlus.BusinessObjectsNew.CancellationReason(oCertCanc.CancellationReasonId)

        With oCertCancData
            .companyId = oCert.CompanyId
            .dealerId = oCert.DealerId
            .certificate = oCert.CertNumber
            .source = ElitaPlusIdentity.Current.ActiveUser.NetworkId
            .cancellationDate = oCertCanc.CancellationDate.Value
            .cancellationCode = oCancellatioReason.Code
            .quote = "N"
            .payment_method_Id = oCertCanc.PaymentMethodId
            If Not oCertCanc.ComputedRefund Is Nothing Then
                .refundAmountRcvd = oCertCanc.ComputedRefund
            End If
            If Not IsDBNull(oCertCanc.InstallmentsPaid) Then
                .InstallmentsPaid = oCertCanc.InstallmentsPaid
            End If
            
        End With
    End Sub

#End Region

#Region "DataView Retrieveing Methods"

    Public ReadOnly Property getCancellationReasonDescription() As String

        Get
            Dim dv As DataView = LookupListNew.GetCancellationReasonLookupList(Me.CompanyId)
            cancellationReasonDesc = LookupListNew.GetDescriptionFromId(dv, Me.CancellationReasonId)
            Return cancellationReasonDesc
        End Get

    End Property

    Public ReadOnly Property getCancellationReasonCode() As String

        Get
            Dim dv As DataView = LookupListNew.GetCancellationReasonLookupList(Me.CompanyId)
            cancellationReasonCode = LookupListNew.GetCodeFromId(dv, Me.CancellationReasonId)
            Return cancellationReasonCode
        End Get

    End Property
    Public ReadOnly Property getCancellationReasonId(ByVal CancellationReasonCode As String) As Guid

        Get
            Dim dv As DataView = LookupListNew.GetCancellationReasonLookupList(Me.CompanyId)
            CancellationReasonId = LookupListNew.GetIdFromCode(dv, CancellationReasonCode)
            Return CancellationReasonId
        End Get

    End Property

    Public Function getRefundComputeMethodId() As Guid
        Dim dal As New CertCancellationDAL
        Dim ds As Dataset
        Dim dv As DataView

        ds = dal.getRefundComputeMethodId(Me.CancellationReasonId)
        dv = ds.Tables(CertCancellationDAL.TABLE_REFUND_COMPUTE_METHOD).DefaultView
        Return New Guid(CType(dv(0)(0), Byte()))
    End Function

    Public Shared Function getCertificateCancellationId(ByVal certId As Guid) As Guid
        Dim dal As New CertCancellationDAL
        Dim ds As Dataset
        Dim dv As DataView

        ds = dal.getCertCancellationId(certId)
        dv = ds.Tables(CertCancellationDAL.TABLE_KEY_NAME).DefaultView
        Return New Guid(CType(dv(0)(0), Byte()))
    End Function

    Public ReadOnly Property getRegionDescription() As String

        Get
            Dim dv As DataView = LookupListNew.GetRegionLookupList()
            cancellationReasonDesc = LookupListNew.GetDescriptionFromId(dv, Me.OriginalRegionId)
            Return cancellationReasonDesc
        End Get

    End Property

    Public Function getPolicyCost() As Decimal
        Dim dal As New CertCancellationDAL
        Dim ds As Dataset
        Dim dv As DataView

        ds = dal.getPolicyCost(Me.CertId)
        dv = ds.Tables(CertCancellationDAL.TABLE_POLICY_COST).DefaultView
        Return (CType(dv(0)(CertCancellationDAL.COL_POLICY_COST), Decimal))
    End Function


#End Region

#Region "StoreProcedures Control"
    Public Shared Function SetFutureCancelCertificate(ByVal oCancelCertificateData As CertCancellationData,
                                             Optional ByVal oBankInfoData As BankInfoData = Nothing,
                                             Optional ByVal oCommentData As CommentData = Nothing,
                                             Optional ByVal cancellationRequestedDate As Date = Nothing) As CertCancellationData
        Try
            'MyBase.Save()

            'Dim oCancelCertificateData As CertCancellationData = CType(oCancelData, CertCancellationData)
            Dim dal As New CertCancellationDAL

            dal.ExecuteSetFutureCancelSP(oCancelCertificateData, oBankInfoData, oCommentData, cancellationRequestedDate)
            Return oCancelCertificateData

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Public Shared Function CancelCertificate(ByVal oCancelCertificateData As CertCancellationData, _
                                             Optional ByVal oBankInfoData As BankInfoData = Nothing, _
                                             Optional ByVal oCommentData As CommentData = Nothing) As CertCancellationData
        Try
            'MyBase.Save()

            'Dim oCancelCertificateData As CertCancellationData = CType(oCancelData, CertCancellationData)
            Dim dal As New CertCancellationDAL

            dal.ExecuteCancelSP(oCancelCertificateData, oBankInfoData, oCommentData)
            Return oCancelCertificateData

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Sub UpdateBankInfoForRejectsSP(ByVal oUpdateBankInfoForRejectsData As UpdateBankInfoForRejectsData)
        Try
            Dim dal As New CertCancellationDAL

            dal.ExecuteUpdateBankInfoForRejectsSP(oUpdateBankInfoForRejectsData)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Sub
    Public Shared Sub ReverseCancellation(ByVal oReverseCancelData As ReverseCancellationData)
        Try
            'Dim oReverseCancelData As ReverseCancellationData = CType(oReverseData, ReverseCancellationData)
            Dim dal As New CertCancellationDAL

            dal.ExecuteReverseCancelSP(oReverseCancelData)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Sub


    ' This Method will be used by the cancel cert web service off-line. any errors will be sent by email by store procedure
    Public Function CancelCertificatesByInvocieNumber(ByVal dealerId As Guid, ByVal companyId As Guid, ByVal certNumber As String, ByVal invoiceNumber As String, ByVal cancellation_code As String, _
                                                      ByVal Source As String, ByVal CancellationDate As DateType) As Boolean

        Try
            'Dim oReverseCancelData As ReverseCancellationData = CType(oReverseData, ReverseCancellationData)
            Dim dal As New CertCancellationDAL

            dal.CancelCertificatesByInvocieNumber(dealerId, companyId, certNumber, invoiceNumber, cancellation_code, Source, CancellationDate)

            Return True

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Function CancelCoverages(ByVal dealerId As Guid, ByVal certNumber As String, ByVal CancellationDate As DateType) As Boolean

        Try
            'Dim oReverseCancelData As ReverseCancellationData = CType(oReverseData, ReverseCancellationData)
            Dim dal As New CertCancellationDAL

            dal.CancelCoverages(dealerId, certNumber, CancellationDate)

            Return True

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function
#End Region

End Class



