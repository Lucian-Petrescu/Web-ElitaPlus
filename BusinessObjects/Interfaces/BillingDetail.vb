'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (10/20/2008)  ********************

Public Class BillingDetail
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
    Public Sub New(ByVal id As Guid, ByVal familyDS As DataSet, Optional ByVal useCertId As Boolean = False)
        MyBase.New(False)
        Dataset = familyDS
        Load(id, useCertId)
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
            Dim dal As New BillingDetailDAL
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

    Protected Sub Load(ByVal id As Guid, Optional ByVal useCertId As Boolean = False)
        Try
            Dim dal As New BillingDetailDAL
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
                dal.Load(Dataset, id, useCertId)
                If useCertId Then
                    Row = FindRow(id, dal.COL_NAME_CERT_ID, Dataset.Tables(dal.TABLE_NAME))
                Else
                    Row = FindRow(id, dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_NAME))
                End If
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
            If row(BillingDetailDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(BillingDetailDAL.COL_NAME_BILLING_DETAIL_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property BillingHeaderId As Guid
        Get
            CheckDeleted()
            If row(BillingDetailDAL.COL_NAME_BILLING_HEADER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(BillingDetailDAL.COL_NAME_BILLING_HEADER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BillingDetailDAL.COL_NAME_BILLING_HEADER_ID, Value)
        End Set
    End Property

    Public Property BillingStatusId As Guid
        Get
            CheckDeleted()
            If Row(BillingDetailDAL.COL_NAME_BILLING_STATUS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(BillingDetailDAL.COL_NAME_BILLING_STATUS_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BillingDetailDAL.COL_NAME_BILLING_STATUS_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property InstallmentNumber As LongType
        Get
            CheckDeleted()
            If Row(BillingDetailDAL.COL_NAME_INSTALLMENT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(BillingDetailDAL.COL_NAME_INSTALLMENT_NUMBER), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BillingDetailDAL.COL_NAME_INSTALLMENT_NUMBER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)> _
    Public Property BankOwnerName As String
        Get
            CheckDeleted()
            If Row(BillingDetailDAL.COL_NAME_BANK_OWNER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(BillingDetailDAL.COL_NAME_BANK_OWNER_NAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BillingDetailDAL.COL_NAME_BANK_OWNER_NAME, Value)
        End Set
    End Property



    Public Property BankAcctNumber As String
        Get
            CheckDeleted()
            If Row(BillingDetailDAL.COL_NAME_BANK_ACCT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(BillingDetailDAL.COL_NAME_BANK_ACCT_NUMBER), Decimal)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BillingDetailDAL.COL_NAME_BANK_ACCT_NUMBER, Value)
        End Set
    End Property



    Public Property BankRtnNumber As String
        Get
            CheckDeleted()
            If Row(BillingDetailDAL.COL_NAME_BANK_RTN_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(BillingDetailDAL.COL_NAME_BANK_RTN_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BillingDetailDAL.COL_NAME_BANK_RTN_NUMBER, Value)
        End Set
    End Property


    Public Property BilledAmount As DecimalType
        Get
            CheckDeleted()
            If row(BillingDetailDAL.COL_NAME_BILLED_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(BillingDetailDAL.COL_NAME_BILLED_AMOUNT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BillingDetailDAL.COL_NAME_BILLED_AMOUNT, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200)> _
    Public Property Reason As String
        Get
            CheckDeleted()
            If Row(BillingDetailDAL.COL_NAME_REASON) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(BillingDetailDAL.COL_NAME_REASON), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BillingDetailDAL.COL_NAME_REASON, Value)
        End Set
    End Property

    <ValueMandatory("")> _
        Public Property CertId As Guid
        Get
            CheckDeleted()
            If Row(BillingDetailDAL.COL_NAME_CERT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(BillingDetailDAL.COL_NAME_CERT_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BillingDetailDAL.COL_NAME_CERT_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
        Public Property PaymentRunDate As DateType
        Get
            CheckDeleted()
            If Row(BillingDetailDAL.COL_NAME_PAYMENT_RUN_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(BillingDetailDAL.COL_NAME_PAYMENT_RUN_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BillingDetailDAL.COL_NAME_CERT_ID, Value)
        End Set
    End Property

    Public Property NightlyPaymentRunDate As DateType
        Get
            CheckDeleted()
            If row(BillingDetailDAL.COL_NAME_NIGHTLY_PAYMENT_RUN_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(BillingDetailDAL.COL_NAME_NIGHTLY_PAYMENT_RUN_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BillingDetailDAL.COL_NAME_NIGHTLY_PAYMENT_RUN_DATE, Value)
        End Set
    End Property



    Public Property ReAttemptCount As Integer
        Get
            CheckDeleted()
            If Row(BillingDetailDAL.COL_NAME_RE_ATTEMPT_COUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(BillingDetailDAL.COL_NAME_RE_ATTEMPT_COUNT), Integer)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BillingDetailDAL.COL_NAME_RE_ATTEMPT_COUNT, Value)
        End Set
    End Property



    Public Property CreditCardInfoId As Guid
        Get
            CheckDeleted()
            If row(BillingDetailDAL.COL_NAME_CREDIT_CARD_INFO_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(BillingDetailDAL.COL_NAME_CREDIT_CARD_INFO_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BillingDetailDAL.COL_NAME_CREDIT_CARD_INFO_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=80)> _
    Public Property AuthorizationNumber As String
        Get
            CheckDeleted()
            If row(BillingDetailDAL.COL_NAME_AUTHORIZATION_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(BillingDetailDAL.COL_NAME_AUTHORIZATION_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BillingDetailDAL.COL_NAME_AUTHORIZATION_NUMBER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=60)> _
    Public Property MerchantCode As String
        Get
            CheckDeleted()
            If row(BillingDetailDAL.COL_NAME_MERCHANT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(BillingDetailDAL.COL_NAME_MERCHANT_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BillingDetailDAL.COL_NAME_MERCHANT_CODE, Value)
        End Set
    End Property



    Public Property InstallmentDueDate As DateType
        Get
            CheckDeleted()
            If row(BillingDetailDAL.COL_NAME_INSTALLMENT_DUE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(BillingDetailDAL.COL_NAME_INSTALLMENT_DUE_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BillingDetailDAL.COL_NAME_INSTALLMENT_DUE_DATE, Value)
        End Set
    End Property
#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New BillingDetailDAL
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


    Public Shared Function CreateBillingHistForRejOrAct(ByVal NewStatusId As Guid, ByVal CertId As Guid, ByVal InstalNo As Integer, ByVal RejectCodeId As Guid, ByVal SelectBillHistId As Guid) As Integer
        Dim dal As New BillingDetailDAL, RetVal As Integer

        RetVal = dal.CreateBillingHistForRejOrAct(NewStatusId, CertId, InstalNo, RejectCodeId, SelectBillHistId)
        Return RetVal
    End Function
#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function getList(ByVal BillingHeaderId As Guid) As DataView
        Dim dal As New BillingDetailDAL
        Dim ds As DataSet
        Try
            ds = dal.LoadList(BillingHeaderId)

            If Not ds Is Nothing AndAlso ds.Tables.Count > 0 Then
                Return ds.Tables(0).DefaultView
            Else
                Return New DataView
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Public Shared Function getListForNonBillingByDealer(ByVal BillingHeaderId As Guid) As DataView
        Dim dal As New BillingDetailDAL
        Dim ds As DataSet
        Try
            ds = dal.LoadListForNonBillingByDealer(BillingHeaderId, Authentication.CurrentUser.LanguageId)

            If Not ds Is Nothing AndAlso ds.Tables.Count > 0 Then
                Return ds.Tables(0).DefaultView
            Else
                Return New DataView
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function


    Public Shared Function getHistoryList(ByVal certId As Guid) As BillingHistorySearchDV
        Try
            Dim dal As New BillingDetailDAL
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Return New BillingHistorySearchDV(dal.LoadBillHistList(langId, certId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function getBillingTotals(ByVal certId As Guid) As BillingTotals
        Try
            Dim dal As New BillingDetailDAL
            Return New BillingTotals(dal.LoadBillingTotals(certId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function getBillingTotalsNew(ByVal certId As Guid) As BillingTotals
        Try
            Dim dal As New BillingDetailDAL
            Return New BillingTotals(dal.LoadBillingTotalsNew(certId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function getBillingLaterRow(ByVal certId As Guid) As BillingLaterRow
        Dim dv As DataView
        Try
            Dim dal As New BillingDetailDAL
            Return New BillingLaterRow(dal.LoadLaterBillingRow(certId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetMaxActiveInstNoForCert(ByVal CertId As Guid) As Integer
        Dim dal As New BillingDetailDAL
        Dim dv As New DataView
        Try
            dv = New BillingLaterRow(dal.GetMaxActiveInstNoForCert(CertId).Tables(0))
            If dv.Count = 1 Then
                GetMaxActiveInstNoForCert = dv(0)(0)
            Else
                GetMaxActiveInstNoForCert = 0
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetLatestRejInstNoForCert(ByVal CertId As Guid) As Integer
        Dim dal As New BillingDetailDAL
        Dim dv As New DataView
        Try
            dv = New BillingLaterRow(dal.GetLatestRejInstNoForCert(CertId).Tables(0))
            If dv.Count = 1 Then
                GetLatestRejInstNoForCert = dv(0)(0)
            Else
                GetLatestRejInstNoForCert = 0
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetAllRejInstNoForCert(ByVal CertId As Guid) As DataView
        Dim dal As New BillingDetailDAL
        Dim ds As DataSet
        Try
            ds = dal.GetAllRejInstNoForCert(CertId)
            If Not ds Is Nothing AndAlso ds.Tables.Count > 0 Then
                Return ds.Tables(0).DefaultView
            Else
                Return New DataView
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function
#End Region
#Region "BillingHistorySearchDV"

    Public Class BillingHistorySearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_NAME_BILLING_DETAIL_ID As String = BillingDetailDAL.COL_NAME_BILLING_DETAIL_ID
        Public Const COL_NAME_INSTALLMENT_NUMBER As String = BillingDetailDAL.COL_NAME_INSTALLMENT_NUMBER
        Public Const COL_NAME_BILLED_AMOUNT As String = BillingDetailDAL.COL_NAME_BILLED_AMOUNT
        Public Const COL_NAME_INSTALLMENT_DUE_DATE As String = BillingDetailDAL.COL_NAME_INSTALLMENT_DUE_DATE
        Public Const COL_NAME_RE_ATTEMPT_COUNT As String = BillingDetailDAL.COL_NAME_RE_ATTEMPT_COUNT
        Public Const COL_NAME_REJECT_DATE As String = BillingDetailDAL.COL_NAME_REJECT_DATE
        Public Const COL_NAME_PAYMENT_RUN_DATE As String = BillingDetailDAL.COL_NAME_PAYMENT_RUN_DATE
        Public Const COL_NAME_REJECTED_ID As String = BillingDetailDAL.COL_NAME_REJECTED_ID
        Public Const COL_NAME_MAX_PAYMENT_RUN_DATE As String = BillingDetailDAL.COL_NAME_MAX_PAYMENT_RUN_DATE
        Public Const COL_NAME_BILLING_STATUS As String = BillingDetailDAL.COL_NAME_BILLING_STATUS
        Public Const COL_NAME_REJECT_CODE As String = BillingDetailDAL.COL_NAME_REJECT_CODE
        Public Const COL_NAME_PAID As String = BillingDetailDAL.COL_NAME_PAID
        Public Const COL_NAME_CREATED_DATE As String = "created_date"
        Public Const COL_NAME_CREATED_DATE1 As String = "created_date1"
        Public Const COL_NAME_PROCESSOR_REJECT_CODE As String = BillingDetailDAL.COL_NAME_PROCESSOR_REJECT_CODE

#End Region

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property BillingDetailId(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_BILLING_DETAIL_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property InstallmentNumber(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_INSTALLMENT_NUMBER).ToString
            End Get
        End Property

        Public Shared ReadOnly Property BilledAmount(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_BILLED_AMOUNT).ToString
            End Get
        End Property

        Public Shared ReadOnly Property PaymentRunDate(ByVal row As DataRow) As DateType
            Get
                Return row(COL_NAME_PAYMENT_RUN_DATE).ToString
            End Get
        End Property

        Public Shared ReadOnly Property CreatedDate(ByVal row As DataRow) As DateType
            Get
                Return row(COL_NAME_CREATED_DATE).ToString
            End Get
        End Property
    End Class
#End Region

#Region "BillingTotal"
    Public Class BillingTotals
        Inherits DataView

#Region "Constants"
        Public Const COL_DETAIL_COUNT As String = "billing_count"
        Public Const COL_BILLED_AMOUNT_TOTAL As String = "billed_amount_total"
#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region

#Region "Billing laters Row"
    Public Class BillingLaterRow
        Inherits DataView
#Region "Constants"
        Public Const COL_NAME_BILLING_DETAIL_ID As String = "billing_detail_id"
        Public Const COL_NAME_BILLING_HEADER_ID As String = "billing_header_id"
        Public Const COL_NAME_APLICATION_NAME As String = "aplication_name"
        Public Const COL_NAME_INSTALLMENT_NUMBER As String = "installment_number"
        Public Const COL_NAME_BANK_OWNER_NAME As String = "bank_owner_name"
        Public Const COL_NAME_BANK_ACCT_NUMBER As String = "bank_acct_number"
        Public Const COL_NAME_BANK_RTN_NUMBER As String = "bank_rtn_number"
        Public Const COL_NAME_BILLED_AMOUNT As String = "billed_amount"
        Public Const COL_NAME_REASON As String = "reason"
        Public Const COL_NAME_CERT_ID As String = "cert_id"
#End Region
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub
    End Class
#End Region
End Class


