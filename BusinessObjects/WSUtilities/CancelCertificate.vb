Imports Assurant.ElitaPlus.BusinessObjects.Common
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.BusinessObjects.Tables
Imports Assurant.ElitaPlus.DALObjects
Imports System.Threading
Imports System.Xml

Public Class CancelCertificate
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const DATA_COL_NAME_CERT_NUMBER As String = "CERTIFICATE_NO"
    Public Const DATA_COL_NAME_SOURCE As String = "SOURCE"
    Public Const DATA_COL_PAYMENT_METHOD_CODE As String = "PAYMENT_METHOD_CODE"
    Public Const DATA_COL_ACCOUNT_NAME As String = "ACCOUNT_NAME"
    Public Const DATA_COL_BANK_ID As String = "BANK_ID"
    Public Const DATA_COL_ACCOUNT_NUMBER As String = "ACCOUNT_NUMBER"
    Public Const DATA_COL_SWIFT_CODE As String = "SWIFT_CODE"
    Public Const DATA_COL_IBAN_NUMBER As String = "IBAN_NUMBER"
    Public Const DATA_COL_ACCOUNT_TYPE_CODE As String = "ACCOUNT_TYPE_CODE"
    Public Const DATA_COL_PAYMENT_REASON_CODE As String = "PAYMENT_REASON_CODE"
    Public Const DATA_COL_NAME_DEALER_CODE As String = "DEALER_CODE"
    Public Const DATA_COL_NAME_CANCELLATION_DATE As String = "CANCELLATION_DATE"
    Public Const DATA_COL_NAME_REFUND_AMOUNT As String = "REFUND_AMOUNT"
    Public Const DATA_COL_NAME_INVOICE_NUMBER As String = "INVOICE_NUMBER"
    Public Const DATA_COL_NAME_CANCEL_ALL_BY_INVOICE As String = "CANCEL_ALL_BY_INVOICE"
    Public Const DATA_COL_NAME_CANCELLATION_REASON_CODE As String = "CANCELLATION_REASON_CODE"    
    Public Const DATA_COL_NAME_COMMENT_TYPE_CODE As String = "COMMENT_TYPE_CODE"
    Public Const DATA_COL_NAME_COMMENTS As String = "COMMENTS"
    Public Const DATA_COL_NAME_ACTION As String = "ACTION"
    Public Const DATA_COL_NAME_ACCOUNT_DIGIT As String = "ACCOUNT_DIGIT"
    Public Const DATA_COL_NAME_BRANCH_DIGIT As String = "BRANCH_DIGIT"
    Public Const DATA_COL_NAME_BRANCH_NUMBER As String = "BRANCH_NUMBER"
    Public Const DATA_COL_NAME_INST_PAID As String = "INSTALLMENTS_PAID"


    Private Const TABLE_NAME As String = "CancelCertificate"
    Private Const DATASET_NAME As String = "CancelCertificate"
    Private Const DATA_COL_NAME_CERT_ID As String = "cert_id"
    Public Const CODE As String = "Code"
    Public Const DESCRIPTION As String = "Description"
    Public Const FIRST_ROW As Integer = 0

    Private Const CERTIFICATE_NOT_FOUND As String = "ERR_CERTIFICATE_NOT_FOUND"
    Private Const CERTIFICATE_INVOICE_NUM_NOT_FOUND As String = "CERTIFICATE_INVOICE_NUM_NOT_FOUND"
    Private Const NO_CANCELLATION_ALLOWED As String = "NO_CANCELLATION_ALLOWED_FOR_CERTIFICATE"

    Private Const CERTIFICATE_ALREADY_CLOSED As String = "ERR_CERTIFICATE_ALREADY_CLOSED"
    Private Const ACTIVE_CLAIMS_EXIST As String = "ERR_ACTIVE_CLAIMS_EXIST"
    Public Const MSG_INVALID_CANCELLATION_REASON_FOR_CERTIFICATE As String = "MSG_INVALID_CANCELLATION_REASON_FOR_CERTIFICATE"
    Public Const MSG_CERT_CANCELDATE_CANNOT_LOWER_THAN_CLAIM_LOSSDATE As String = "MSG_CERT_CANCELDATE_CANNOT_LOWER_THAN_CLAIM_LOSSDATE"
    Public Const MSG_CERT_CANCEL_CANNOT_HAVE_CLAIMS As String = "MSG_CERT_CANCEL_CANNOT_HAVE_CLAIMS"
    Public Const MSG_INVALID_CANCELLATION_DATE As String = "MSG_INVALID_CANCELLATION_DATE"

    Public Const Success_Code = 0
    Public Const Error_Code = -1
#End Region

#Region "Constructors"

    Public Sub New(ByVal ds As CancelCertificateDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

    End Sub

#End Region

#Region "Private Members"
    Private _certId As Guid = Guid.Empty
    Private _dealerId As Guid = Guid.Empty
    Private _companyId As Guid = Guid.Empty
    Private _bankInfoBO As BankInfo
    Private _CommentBO As Comment
    Private _cancellation_code As String = String.Empty

    Private Sub MapDataSet(ByVal ds As CancelCertificateDs)

        Dim schema As String = ds.GetXmlSchema

        Dim t As Integer
        Dim i As Integer

        For t = 0 To ds.Tables.Count - 1
            For i = 0 To ds.Tables(t).Columns.Count - 1
                ds.Tables(t).Columns(i).ColumnName = ds.Tables(t).Columns(i).ColumnName.ToUpper
            Next
        Next

        Me.Dataset = New DataSet
        Me.Dataset.ReadXmlSchema(XMLHelper.GetXMLStream(schema))

    End Sub

    'Initialization code for new objects
    Private Sub Initialize()
    End Sub

    Private Sub Load(ByVal ds As CancelCertificateDs)
        Try
            Initialize()
            Dim newRow As DataRow = Me.Dataset.Tables(TABLE_NAME).NewRow
            Me.Row = newRow
            PopulateBOFromWebService(ds)
            Me.Dataset.Tables(TABLE_NAME).Rows.Add(newRow)

        Catch ex As BOValidationException
            Throw ex
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("CancelCertificate Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ByVal ds As CancelCertificateDs)
        Try
            If ds.CancelCertificate.Count = 0 Then Exit Sub
            With ds.CancelCertificate.Item(0)
                Me.CertNumber = ds.CancelCertificate.Item(0).CERTIFICATE_NO
                Me.DealerCode = ds.CancelCertificate.Item(0).DEALER_CODE
                Me.Source = ds.CancelCertificate.Item(0).SOURCE
                Me.CancellationDate = ds.CancelCertificate.Item(0).CANCELLATION_DATE


                If Not .IsPAYMENT_METHOD_CODENull Then Me.PaymentMethodCode = ds.CancelCertificate.Item(0).PAYMENT_METHOD_CODE
                If Not .IsACCOUNT_NAMENull Then Me.AccountName = ds.CancelCertificate.Item(0).ACCOUNT_NAME
                If Not .IsBANK_IDNull Then Me.BankId = ds.CancelCertificate.Item(0).BANK_ID
                If Not .IsACCOUNT_NUMBERNull Then Me.AccountNumber = ds.CancelCertificate.Item(0).ACCOUNT_NUMBER
                If Not .IsSWIFT_CODENull Then Me.SwiftCode = ds.CancelCertificate.Item(0).SWIFT_CODE
                If Not .IsIBAN_NUMBERNull Then Me.IBANNumber = ds.CancelCertificate.Item(0).IBAN_NUMBER
                If Not .IsACCOUNT_TYPE_CODENull Then Me.AccountTypeCode = ds.CancelCertificate.Item(0).ACCOUNT_TYPE_CODE
                If Not .IsPAYMENT_REASON_CODENull Then Me.PaymentReasonCode = ds.CancelCertificate.Item(0).PAYMENT_REASON_CODE
                If Not .IsPAYMENT_METHOD_CODENull AndAlso Not .IsACCOUNT_NAMENull AndAlso Not .IsBANK_IDNull AndAlso _
                    Not .IsACCOUNT_NUMBERNull AndAlso Not .IsSWIFT_CODENull AndAlso Not .IsIBAN_NUMBERNull AndAlso Not .IsACCOUNT_TYPE_CODENull _
                    AndAlso .IsACCOUNT_DIGITNull AndAlso .IsBRANCH_DIGITNull Then
                    Me._bankInfoBO = New BankInfo
                    _bankInfoBO.Account_Name = .ACCOUNT_NAME
                    _bankInfoBO.Account_Number = .ACCOUNT_NUMBER
                    _bankInfoBO.AccountTypeId = Me.GetAccountTypeID(.ACCOUNT_TYPE_CODE)
                    _bankInfoBO.Bank_Id = .BANK_ID
                    _bankInfoBO.BankName = .ACCOUNT_NAME
                    _bankInfoBO.SwiftCode = .SWIFT_CODE
                    _bankInfoBO.IbanNumber = .IBAN_NUMBER
                ElseIf Not .IsPAYMENT_METHOD_CODENull AndAlso Not .IsACCOUNT_NAMENull AndAlso Not .IsBANK_IDNull AndAlso _
                     Not .IsACCOUNT_NUMBERNull AndAlso Not .IsACCOUNT_TYPE_CODENull AndAlso Not .IsACCOUNT_DIGITNull AndAlso Not .IsBRANCH_DIGITNull _
                     AndAlso Not .IsBRANCH_NUMBERNull AndAlso Not .IsBANK_NAMENull AndAlso .IsSWIFT_CODENull AndAlso .IsIBAN_NUMBERNull Then
                    Me._bankInfoBO = New BankInfo
                    _bankInfoBO.Account_Name = .ACCOUNT_NAME
                    _bankInfoBO.Account_Number = .ACCOUNT_NUMBER
                    _bankInfoBO.AccountTypeId = Me.GetAccountTypeID(.ACCOUNT_TYPE_CODE)
                    _bankInfoBO.Bank_Id = .BANK_ID
                    _bankInfoBO.BankName = .BANK_NAME
                    _bankInfoBO.AccountDigit = .ACCOUNT_DIGIT
                    _bankInfoBO.BranchDigit = .BRANCH_DIGIT
                    _bankInfoBO.BranchNumber = .BRANCH_NUMBER
                End If
                If Not .IsCANCEL_ALL_BY_INVOICENull Then Me.CancelAllByInvoice = (ds.CancelCertificate.Item(0).CANCEL_ALL_BY_INVOICE).ToUpper
                If Not .IsINVOICE_NUMBERNull Then Me.InvoiceNumber = ds.CancelCertificate.Item(0).INVOICE_NUMBER
                If Not .IsCANCELLATION_REASON_CODENull Then Me.CancellationReasonCode = ds.CancelCertificate.Item(0).CANCELLATION_REASON_CODE
                If Not .IsREFUND_AMOUNTNull Then Me.RefundAmount = ds.CancelCertificate.Item(0).REFUND_AMOUNT
                If Not .IsCOMMENT_TYPE_CODENull Then Me.CommentTypeCode = ds.CancelCertificate.Item(0).COMMENT_TYPE_CODE
                If Not .IsCOMMENTSNull Then Me.Comments = ds.CancelCertificate.Item(0).COMMENTS
                If Not .IsACTIONNull Then Me.Action = ds.CancelCertificate.Item(0).ACTION
                If Not .IsINSTALLMENTS_PAIDNull Then Me.InstallmentsPaid = ds.CancelCertificate.Item(0).INSTALLMENTS_PAID

                If Not .IsCOMMENTSNull AndAlso Not .IsCOMMENT_TYPE_CODENull Then
                    Me._CommentBO = New Comment
                    _CommentBO.CommentTypeId = Me.GetCommentTypeID(.COMMENT_TYPE_CODE)
                    _CommentBO.Comments = .COMMENTS
                End If
            End With

        Catch ex As BOValidationException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("CancelCertificate Invalid Parameters Error", Common.ErrorCodes.BO_INVALID_DATA, ex)
        End Try
    End Sub

    Protected Shadows Sub CheckDeleted()
    End Sub

    Private Sub CancelCertificatesByInvocieNumber()

        Dim objCertCancellation As New CertCancellation
        objCertCancellation.CancelCertificatesByInvocieNumber(Me.DealerId, Me.CompanyId, Me.CertNumber, Me.InvoiceNumber, Me.CancellationCode, Me.Source, Me.CancellationDate.Value)

    End Sub
#End Region

#Region "Properties"

    <ValueMandatory("")> _
    Public Property CertNumber() As String
        Get
            If Row(Me.DATA_COL_NAME_CERT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_CERT_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_CERT_NUMBER, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property DealerCode() As String
        Get
            If Row(Me.DATA_COL_NAME_DEALER_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_DEALER_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_DEALER_CODE, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property CancellationDate() As DateType
        Get
            CheckDeleted()
            If Row(DATA_COL_NAME_CANCELLATION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_CANCELLATION_DATE), DateTime)
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(DATA_COL_NAME_CANCELLATION_DATE, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property Source() As String
        Get
            If Row(Me.DATA_COL_NAME_SOURCE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_SOURCE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_SOURCE, Value)
        End Set
    End Property

    Public Property PaymentMethodCode() As String
        Get
            If Row(Me.DATA_COL_PAYMENT_METHOD_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_PAYMENT_METHOD_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_PAYMENT_METHOD_CODE, Value)
        End Set
    End Property

    Public Property AccountName() As String
        Get
            If Row(Me.DATA_COL_ACCOUNT_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_ACCOUNT_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_ACCOUNT_NAME, Value)
        End Set
    End Property

    Public Property BankId() As String
        Get
            If Row(Me.DATA_COL_BANK_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_BANK_ID), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_BANK_ID, Value)
        End Set
    End Property

    Public Property AccountNumber() As String
        Get
            If Row(Me.DATA_COL_ACCOUNT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_ACCOUNT_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_ACCOUNT_NUMBER, Value)
        End Set
    End Property

    Public Property SwiftCode() As String
        Get
            If Row(Me.DATA_COL_SWIFT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_SWIFT_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_SWIFT_CODE, Value)
        End Set
    End Property

    Public Property IBANNumber() As String
        Get
            If Row(Me.DATA_COL_IBAN_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_IBAN_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_IBAN_NUMBER, Value)
        End Set
    End Property

    Public Property AccountTypeCode() As String
        Get
            If Row(Me.DATA_COL_ACCOUNT_TYPE_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_ACCOUNT_TYPE_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_ACCOUNT_TYPE_CODE, Value)
        End Set
    End Property

    Public Property PaymentReasonCode() As String
        Get
            If Row(Me.DATA_COL_PAYMENT_REASON_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_PAYMENT_REASON_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_PAYMENT_REASON_CODE, Value)
        End Set
    End Property

    Public Property InvoiceNumber() As String
        Get
            If Row(Me.DATA_COL_NAME_INVOICE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_INVOICE_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_INVOICE_NUMBER, Value)
        End Set
    End Property

    Public Property CancelAllByInvoice() As String
        Get
            If Row(Me.DATA_COL_NAME_CANCEL_ALL_BY_INVOICE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_CANCEL_ALL_BY_INVOICE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_CANCEL_ALL_BY_INVOICE, Value)
        End Set
    End Property


    Public Property CancellationReasonCode() As String
        Get
            If Row(Me.DATA_COL_NAME_CANCELLATION_REASON_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_CANCELLATION_REASON_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_CANCELLATION_REASON_CODE, Value)
        End Set
    End Property

    Public Property RefundAmount() As Decimal
        Get
            If Row(Me.DATA_COL_NAME_REFUND_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_REFUND_AMOUNT), Decimal)
            End If
        End Get
        Set(ByVal Value As Decimal)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_REFUND_AMOUNT, Value)
        End Set
    End Property


    Public Property CommentTypeCode() As String
        Get
            If Row(Me.DATA_COL_NAME_COMMENT_TYPE_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_COMMENT_TYPE_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_COMMENT_TYPE_CODE, Value)
        End Set
    End Property


    Public Property Comments() As String
        Get
            If Row(Me.DATA_COL_NAME_COMMENTS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_COMMENTS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_COMMENTS, Value)
        End Set
    End Property
    Public Property Action() As String
        Get
            If Row(Me.DATA_COL_NAME_ACTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_ACTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_ACTION, Value)
        End Set
    End Property

    Public Property ACCOUNT_DIGIT() As Long
        Get
            If Row(Me.DATA_COL_NAME_ACCOUNT_DIGIT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_ACCOUNT_DIGIT), Long)
            End If
        End Get
        Set(ByVal Value As Long)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_ACCOUNT_DIGIT, Value)
        End Set
    End Property
    Public Property BRANCH_DIGIT() As Long
        Get
            If Row(Me.DATA_COL_NAME_BRANCH_DIGIT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_BRANCH_DIGIT), Long)
            End If
        End Get
        Set(ByVal value As Long)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_BRANCH_DIGIT, Value)
        End Set
    End Property

    Public Property BRANCH_NUMBER() As Long
        Get
            If Row(Me.DATA_COL_NAME_BRANCH_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_BRANCH_NUMBER), Long)
            End If
        End Get
        Set(ByVal Value As Long)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_BRANCH_NUMBER, Value)
        End Set
    End Property

    Private ReadOnly Property CertID() As Guid
        Get
            If _certId.Equals(Guid.Empty) Then
                Dim dsCert As DataSet = Certificate.ClaimLogisticsGetCert(Me.CertNumber, Me.DealerCode)

                If Not dsCert Is Nothing AndAlso dsCert.Tables.Count > 0 AndAlso dsCert.Tables(0).Rows.Count = 1 Then
                    If dsCert.Tables(0).Rows(0).Item(Me.DATA_COL_NAME_CERT_ID) Is DBNull.Value Then
                        Throw New BOValidationException("CancelCertificate Error: ", CERTIFICATE_NOT_FOUND)
                    Else
                        _certId = New Guid(CType(dsCert.Tables(0).Rows(0).Item(Me.DATA_COL_NAME_CERT_ID), Byte()))

                        If _certId.Equals(Guid.Empty) Then
                            Throw New BOValidationException("CancelCertificate Error: ", CERTIFICATE_NOT_FOUND)
                        End If
                    End If
                Else
                    Throw New BOValidationException("CancelCertificate Error: ", CERTIFICATE_NOT_FOUND)
                End If
            End If

            Return Me._certId
        End Get
    End Property

    Public Function GetAccountTypeID(ByVal AccountType As String) As Guid
        Dim accountTypeID As Guid = Guid.Empty
        Dim list As DataView = LookupListNew.GetAccountTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)

        accountTypeID = LookupListNew.GetIdFromCode(list, AccountType)

        Return accountTypeID
    End Function

    Public Function GetCommentTypeID(ByVal CommentTypeCode As String) As Guid
        Dim CommentTypeID As Guid = Guid.Empty
        Dim list As DataView = LookupListNew.GetCommentTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)

        CommentTypeID = LookupListNew.GetIdFromCode(list, CommentTypeCode)

        Return CommentTypeID
    End Function

    Public Property InstallmentsPaid() As Long
        Get
            If Row(Me.DATA_COL_NAME_INST_PAID) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_INST_PAID), Long)
            End If
        End Get
        Set(ByVal Value As Long)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_INST_PAID, Value)
        End Set
    End Property

#End Region

#Region "Public Members"


    Public Overrides Function ProcessWSRequest() As String
        Try
            _certId = Guid.Empty
            Me.Validate()

            Dim oCancelCertificateData As New CertCancellationData
            Dim oCancelCertRstData As New CertCancellationData
            Dim oCert As Certificate
            Dim oCertsDataSet As DataSet

            If Not Me.CancelAllByInvoice Is Nothing AndAlso Me.CancelAllByInvoice.Equals("Y") Then
                If Not Me.InvoiceNumber Is Nothing AndAlso Not Me.InvoiceNumber.Equals(String.Empty) Then
                    'List of certificates BY Invocie number
                    oCertsDataSet = Certificate.GetCertListByInvoiceNumber(Me.InvoiceNumber)
                    If Not oCertsDataSet Is Nothing AndAlso oCertsDataSet.Tables.Count > 0 AndAlso oCertsDataSet.Tables(0).Rows.Count > 0 Then
                        If oCertsDataSet.Tables(0).Rows(0).Item(Me.DATA_COL_NAME_CERT_ID) Is DBNull.Value Then
                            Throw New BOValidationException("CancelCertificate Error: ", CERTIFICATE_NOT_FOUND)
                        Else
                            _certId = New Guid(CType(oCertsDataSet.Tables(0).Rows(0).Item(Me.DATA_COL_NAME_CERT_ID), Byte()))
                            If _certId.Equals(Guid.Empty) Then
                                Throw New BOValidationException("CancelCertificate Error: ", CERTIFICATE_NOT_FOUND)
                            End If
                            oCert = New Certificate(_certId)
                        End If
                    Else
                        Throw New BOValidationException("CancelCertificate Error: ", CERTIFICATE_NOT_FOUND)
                    End If

                Else
                    'List of certificates BY Certificate number                    
                    oCert = New Certificate(Me.CertID)
                    If oCert.InvoiceNumber Is Nothing OrElse oCert.InvoiceNumber.Equals(String.Empty) Then
                        Throw New BOValidationException("CancelCertificate Error: ", Me.CERTIFICATE_INVOICE_NUM_NOT_FOUND)
                    End If
                End If
            Else

                oCert = New Certificate(Me.CertID)
            End If

            If Me.CancelAllByInvoice Is Nothing OrElse Me.CancelAllByInvoice.Equals("N") Then
                ' Check if the certificate is already closed
                If oCert.StatusCode = "C" Then
                    Throw New BOValidationException("CancelCertificate Error: ", CERTIFICATE_ALREADY_CLOSED)
                End If

                ' Check if there is any claims that are not closed
                If (Dealer.IsSkipActiveClaim(Me.DealerId)) Then
                    ' check if there is any claim in A or P status and Loss Date > cancellation date 
                    If (oCert.ActiveClaimExist(Me.DealerId, oCert.CertNumber, Me.CancellationDate)) Then
                        Throw New BOValidationException("CancelCertificate Error: ", Me.ACTIVE_CLAIMS_EXIST)
                    End If
                Else
                    If oCert.TotalClaimsNotClosedForCert(Me.DealerId, oCert.CertNumber) Then
                        Throw New BOValidationException("CancelCertificate Error: ", Me.ACTIVE_CLAIMS_EXIST)
                    End If
                End If
            End If

            If oCert.IsChildCertificate Then
                Throw New BOValidationException("CancelCertificate Error: ", Me.NO_CANCELLATION_ALLOWED)
            End If

            ' Get cancellation reason code from Contract record
            Dim oContract As Contract
            oContract = Contract.GetContract(Me.DealerId, oCert.WarrantySalesDate.Value)
            If oContract Is Nothing Then
                oContract = Contract.GetMaxExpirationContract(Me.DealerId)
                If oContract Is Nothing Then
                    Throw New BOValidationException("CancelCertificate Error: ", Common.ErrorCodes.NO_CONTRACT_FOUND)
                End If
            End If

            ' create a cancellation BO
            Dim certCancellationBO As New CertCancellation
            Dim oCancellationReason As Assurant.ElitaPlus.BusinessObjectsNew.CancellationReason
            Dim refundMDv As DataView
            refundMDv = LookupListNew.GetRefundComputeMethodLookupList(Authentication.LangId)

            If Me.CancellationReasonCode Is Nothing Then
                certCancellationBO.CancellationReasonId = oContract.CancellationReasonId
            Else
                certCancellationBO.CompanyId = oCert.CompanyId
                certCancellationBO.CancellationReasonId = certCancellationBO.getCancellationReasonId(Me.CancellationReasonCode)
            End If
            If certCancellationBO.CancellationReasonId.Equals(Guid.Empty) Then
                Throw New BOValidationException("CancelCertificate Error: ", Common.ErrorCodes.MSG_INVALID_CANCELLATION_REASON_FOR_CERTIFICATE)
            End If
            oCancellationReason = New CancellationReason(certCancellationBO.CancellationReasonId)

            certCancellationBO.CancellationDate = Me.CancellationDate
            certCancellationBO.CertId = oCert.Id
            'Call QuoteCancellation

            If oCancellationReason.RefundComputeMethodId.Equals(LookupListNew.GetIdFromCode(refundMDv, Codes.REFUND_COMPUTE_METHOD__22)) Then
                If (Me.Action <> "Q" And Me.Action <> "C") Then
                    Throw New BOValidationException("CancelCertificate Error: ", Common.ErrorCodes.ERR_MSG_INVALID_ACTION_REQUESTED)
                End If

                If (Me.Action = "C" And IsDBNull(Me.RefundAmount)) Then
                    Throw New BOValidationException("CancelCertificate Error: ", Common.ErrorCodes.ERR_MSG_REFUND_AMT_REQUIRED)
                End If
            End If

            ' Check if the cancellation date is within the allowed period
            ' If Me.Source.Equals("OLITA") AndAlso Me.CancellationDate > oCert.WarrantySalesDate.Value.AddDays(oContract.FullRefundDays) Then
            If Me.CancellationDate > oCert.WarrantySalesDate.Value.AddDays(oContract.FullRefundDays) Then
                Throw New BOValidationException("CancelCertificate Error: ", Me.MSG_INVALID_CANCELLATION_DATE)
            End If

            If Me.Action = "Q" Then
                oCert.QuoteCancellation(certCancellationBO, oCancelCertificateData, oContract)
                If Not oCancelCertificateData.errorExist Then
                    If oCancelCertificateData.errorExist2 = False Then
                        'Dim ds As New DataSet
                        'Dim dcRedundAmt As DataColumn
                        'Dim drrow As DataRow
                        'ds.Tables.Add(CertCancellationDAL.TABLE_REFUND_AMOUNT)
                        'dcRedundAmt = New DataColumn(Me.DATA_COL_NAME_REFUND_AMOUNT)
                        'ds.Tables(CertCancellationDAL.TABLE_REFUND_AMOUNT).Columns.Add(dcRedundAmt)
                        'drrow = ds.Tables(CertCancellationDAL.TABLE_REFUND_AMOUNT).NewRow()
                        'drrow(0)(Me.DATA_COL_NAME_REFUND_AMOUNT) = oCancelCertificateData.refundAmount
                        'ds.Tables(CertCancellationDAL.TABLE_REFUND_AMOUNT).Rows.Add(drrow)
                        'Return XMLHelper.FromDatasetToXML(ds)

                        Return GetResponseXML(oCancelCertificateData.errorCode, oCancelCertificateData.ErrorMsg, oCancelCertificateData.refundAmount, oCancelCertificateData.InstallmentsPaid, oCancelCertificateData.AuthNumber)
                    Else
                        Throw New BOValidationException("CancelCertificate Error: ", oCancelCertificateData.errorCode)
                    End If
                Else
                    Throw New BOValidationException("CancelCertificate Error: ", oCancelCertificateData.errorCode)
                End If
            ElseIf Me.Action = "C" Then

                certCancellationBO.ComputedRefund = Me.RefundAmount
                certCancellationBO.InstallmentsPaid = Me.InstallmentsPaid
                If Not _CommentBO Is Nothing Then
                    _CommentBO.CallerName = oCert.CustomerName
                End If
                oCert.ProcessCancellation(certCancellationBO, Me._bankInfoBO, oContract, _CommentBO, oCancelCertRstData)
                ' Set the acknoledge OK response
                Return GetResponseXML(certCancellationBO.ErrorCode, certCancellationBO.ErrorMsg, certCancellationBO.ComputedRefund, certCancellationBO.InstallmentsPaid, certCancellationBO.CCAuthorizationNumber)
            Else
                oCert.QuoteCancellation(certCancellationBO, oCancelCertificateData)

                If Not oCancelCertificateData.errorExist Then
                    If oCancelCertificateData.errorExist2 = False Then
                        If Not Me.CancelAllByInvoice Is Nothing AndAlso Me.CancelAllByInvoice.Equals("Y") Then
                            ' Cancel Certificates by invoice number off-line
                            Dim objCancellationReason = New CancellationReason(oContract.CancellationReasonId)
                            Me.CancellationCode = objCancellationReason.Code
                            Dim t As Thread = New Thread(AddressOf Me.CancelCertificatesByInvocieNumber)
                            t.Start()

                            ' Set the acknoledge OK response
                            Return XMLHelper.GetXML_OK_Response
                        Else
                            'Call certificate Cancellation
                            oCert.ProcessCancellation(certCancellationBO, Me._bankInfoBO, oContract)
                            ' Set the acknoledge OK response
                            Return XMLHelper.GetXML_OK_Response
                        End If
                    Else
                        Throw New BOValidationException("CancelCertificate Error: ", oCancelCertificateData.errorCode)
                    End If
                Else
                    Throw New BOValidationException("CancelCertificate Error: ", oCancelCertificateData.errorCode)
                End If

            End If

            'Check the outcome

        Catch ex As BOValidationException
            Throw ex
        Catch ex As StoredProcedureGeneratedException
            Throw ex
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try

    End Function


#End Region


#Region "Extended Properties"

    Private ReadOnly Property DealerId() As Guid
        Get
            If Me._dealerId.Equals(Guid.Empty) Then

                Dim list As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
                If list Is Nothing Then
                    Throw New BOValidationException("CancelCertificate Error: ", Common.ErrorCodes.WS_ERROR_ACCESSING_DATABASE)
                End If
                Me._dealerId = LookupListNew.GetIdFromCode(list, Me.DealerCode)
                If _dealerId.Equals(Guid.Empty) Then
                    Throw New BOValidationException("CancelCertificate Error: ", Common.ErrorCodes.WS_DEALER_NOT_FOUND)
                End If
                list = Nothing
            End If

            Return Me._dealerId
        End Get
    End Property

    Private ReadOnly Property CompanyId() As Guid
        Get
            If Me._companyId.Equals(Guid.Empty) Then
                Dim objDealer As New Dealer(Me.DealerId)
                Me._companyId = objDealer.CompanyId
            End If

            Return Me._companyId
        End Get
    End Property


    Private Property CancellationCode() As String
        Get
            Return _cancellation_code
        End Get
        Set(ByVal value As String)
            _cancellation_code = value
        End Set
    End Property
#End Region


    Private Shared Function GetResponseXML(ByVal strcode As String, ByVal strMessage As String, ByVal RefundAmt As Decimal, _
                                           ByVal InstallmentsPaid As Integer, ByVal AuthNumber As String) As String

        Dim objDoc As New Xml.XmlDocument
        Dim objRoot As Xml.XmlElement
        Dim objE As XmlElement

        Dim objDecl As XmlDeclaration = objDoc.CreateXmlDeclaration("1.0", "utf-8", Nothing)

        objRoot = objDoc.CreateElement("QuoteCancelResult")
        objDoc.InsertBefore(objDecl, objDoc.DocumentElement)
        objDoc.AppendChild(objRoot)

        objE = objDoc.CreateElement("Code")
        objRoot.AppendChild(objE)
        objE.InnerText = strcode

        objE = objDoc.CreateElement("Message")
        objRoot.AppendChild(objE)
        objE.InnerText = strMessage

        objE = objDoc.CreateElement("ComputedRefundAmount")
        objRoot.AppendChild(objE)
        objE.InnerText = RefundAmt

        objE = objDoc.CreateElement("InstallmentsPaid")
        objRoot.AppendChild(objE)
        objE.InnerText = InstallmentsPaid

        objE = objDoc.CreateElement("CCAuthNumber")
        objRoot.AppendChild(objE)
        objE.InnerText = AuthNumber

        Return objDoc.OuterXml


    End Function

End Class




