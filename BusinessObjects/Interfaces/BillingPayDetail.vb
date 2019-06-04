Public Class BillingPayDetail
    Inherits BusinessObjectBase

#Region "Constructors"

    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load(id)
    End Sub

    Public Sub New()
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load()
    End Sub

    Public Sub New(ByVal id As Guid, ByVal familyDS As DataSet, Optional ByVal useCertId As Boolean = False)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load(id, useCertId)
    End Sub

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
            Dim dal As New BillingPayDetailDAL
            If Me.Dataset.Tables.IndexOf(dal.BILLPAY_TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.BILLPAY_TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.BILLPAY_TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            SetValue(dal.BILLPAYTBL_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid, Optional ByVal useCertId As Boolean = False)
        Try
            Dim dal As New BillingPayDetailDAL
            If Me._isDSCreator Then
                If Not Me.Row Is Nothing Then
                    Me.Dataset.Tables(dal.BILLPAY_TABLE_NAME).Rows.Remove(Me.Row)
                End If
            End If
            Me.Row = Nothing
            If Me.Dataset.Tables.IndexOf(dal.BILLPAY_TABLE_NAME) >= 0 Then
                Me.Row = Me.FindRow(id, dal.BILLPAYTBL_KEY_NAME, Me.Dataset.Tables(dal.BILLPAY_TABLE_NAME))
            End If
            If Me.Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.LoadBillPay(Me.Dataset, id, useCertId)
                If useCertId Then
                    Me.Row = Me.FindRow(id, dal.COL_NAME_CERT_ID, Me.Dataset.Tables(dal.BILLPAY_TABLE_NAME))
                Else
                    Me.Row = Me.FindRow(id, dal.BILLPAYTBL_KEY_NAME, Me.Dataset.Tables(dal.BILLPAY_TABLE_NAME))
                End If
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
            If Row(BillingPayDetailDAL.BILLPAYTBL_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(BillingPayDetailDAL.COL_NAME_BILLING_DETAIL_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")>
    Public Property InstallmentNumber() As LongType
        Get
            CheckDeleted()
            If Row(BillingPayDetailDAL.COL_NAME_INSTALLMENT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(BillingPayDetailDAL.COL_NAME_INSTALLMENT_NUMBER), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(BillingPayDetailDAL.COL_NAME_INSTALLMENT_NUMBER, Value)
        End Set
    End Property

    Public Property BilledAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(BillingPayDetailDAL.COL_NAME_BILLED_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(BillingPayDetailDAL.COL_NAME_BILLED_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(BillingPayDetailDAL.COL_NAME_BILLED_AMOUNT, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property CertId() As Guid
        Get
            CheckDeleted()
            If Row(BillingPayDetailDAL.COL_NAME_CERT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(BillingPayDetailDAL.COL_NAME_CERT_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(BillingPayDetailDAL.COL_NAME_CERT_ID, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New BillingPayDetailDAL
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


    Public Shared Function CreateBillPayForRejOrAct(ByVal NewStatusId As Guid, ByVal CertId As Guid, ByVal InstalNo As Integer, ByVal RejectCodeId As Guid, ByVal SelectBillHistId As Guid) As Integer
        Dim dal As New BillingPayDetailDAL, RetVal As Integer

        RetVal = dal.CreateBillPayForRejOrAct(NewStatusId, CertId, InstalNo, RejectCodeId, SelectBillHistId)
        Return RetVal
    End Function
#End Region


#Region "DataView Retrieveing Methods"

    Public Shared Function getloadBillpayList(ByVal BillingHeaderId As Guid) As DataView
        Dim dal As New BillingPayDetailDAL
        Dim ds As DataSet
        Try
            ds = dal.loadBillpayList(BillingHeaderId)

            If Not ds Is Nothing AndAlso ds.Tables.Count > 0 Then
                Return ds.Tables(0).DefaultView
            Else
                Return New DataView
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Public Shared Function getloadBillpayListForNonBillingDealer(ByVal BillingHeaderId As Guid) As DataView
        Dim dal As New BillingPayDetailDAL
        Dim ds As DataSet
        Try
            ds = dal.loadBillpayListForNonBillingDealer(BillingHeaderId, Authentication.CurrentUser.LanguageId)

            If Not ds Is Nothing AndAlso ds.Tables.Count > 0 Then
                Return ds.Tables(0).DefaultView
            Else
                Return New DataView
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function


    Public Shared Function getLoadBillHistList(ByVal certId As Guid, ByVal Sortby As String) As BillPayHistorySearchDV
        Try
            Dim dal As New BillingPayDetailDAL
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Return New BillPayHistorySearchDV(dal.LoadBillHistList(langId, certId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function getBillPayTotals(ByVal certId As Guid) As BillPayTotals
        Try
            Dim dal As New BillingPayDetailDAL
            Return New BillPayTotals(dal.LoadTotals(certId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Public Shared Function getBillPayTotalsNew(ByVal certId As Guid) As BillPayTotals
        Try
            Dim dal As New BillingPayDetailDAL
            Return New BillPayTotals(dal.LoadTotalsNew(certId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function getBillingLaterRow(ByVal certId As Guid) As BillPayLaterRow
        Dim dv As DataView
        Try
            Dim dal As New BillingPayDetailDAL
            Return New BillPayLaterRow(dal.LoadLaterBillPayRow(certId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetMaxActiveInstNoForCert(ByVal CertId As Guid) As Integer
        Dim dal As New BillingPayDetailDAL
        Dim dv As New DataView
        Try
            dv = New BillPayLaterRow(dal.GetMaxActiveInstNoForCert(CertId).Tables(0))
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
        Dim dal As New BillingPayDetailDAL
        Dim dv As New DataView
        Try
            dv = New BillPayLaterRow(dal.GetLatestRejInstNoForCert(CertId).Tables(0))
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
        Dim dal As New BillingPayDetailDAL
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
#Region "BillPayHistorySearchDV"
    Public Class BillPayHistorySearchDV
        Inherits DataView

#Region "Constants"

        Public Const COL_NAME_BILLING_DETAIL_ID As String = BillingPayDetailDAL.COL_NAME_BILLING_DETAIL_ID
        Public Const COL_NAME_INSTALLMENT_NUMBER As String = BillingPayDetailDAL.COL_NAME_INSTALLMENT_NUMBER
        Public Const COL_NAME_BILLED_AMOUNT As String = BillingPayDetailDAL.COL_NAME_BILLED_AMOUNT
        Public Const COL_NAME_REJECTED_ID As String = BillingPayDetailDAL.COL_NAME_REJECTED_ID
        Public Const COL_NAME_PAID As String = BillingPayDetailDAL.COL_NAME_PAID
        Public Const COL_NAME_COVERAGE_SEQ As String = BillingPayDetailDAL.COL_NAME_COVERAGE_SEQ
        Public Const COL_NAME_FROM_DATE As String = BillingPayDetailDAL.COL_NAME_FROM_DATE
        Public Const COL_NAME_TO_DATE As String = BillingPayDetailDAL.COL_NAME_TO_DATE
        Public Const COL_NAME_BILLING_DATE As String = BillingPayDetailDAL.COL_NAME_BILLING_DATE
        Public Const COL_NAME_OPEN_AMOUNT As String = BillingPayDetailDAL.COL_NAME_OPEN_AMOUNT
        Public Const COL_NAME_SOURCE As String = BillingPayDetailDAL.COL_NAME_SOURCE
        Public Const COL_NAME_INCOMING_AMOUNT As String = BillingPayDetailDAL.COL_NAME_INCOMING_AMOUNT
        Public Const COL_NAME_BILLING_STATUS As String = BillingPayDetailDAL.COL_NAME_BILLING_STATUS
        Public Const COL_NAME_REJECT_CODE As String = BillingPayDetailDAL.COL_NAME_REJECT_CODE
        Public Const COL_NAME_INVOICE_NUMBER As String = BillingPayDetailDAL.COL_NAME_INVOICE_NUMBER


        Public Const COL_NAME_DATE_PROCESSED As String = "process_date"
        Public Const COL_NAME_CREATED_DATE1 As String = "created_date1"

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

        'Public Shared ReadOnly Property CreatedDate(ByVal row As DataRow) As DateType
        '    Get
        '        Return row(COL_NAME_CREATED_DATE).ToString
        '    End Get
        'End Property

        Public Shared ReadOnly Property CoverageSequence(ByVal row) As Integer
            Get
                Return row(COL_NAME_COVERAGE_SEQ).ToString()
            End Get
        End Property

        Public Shared ReadOnly Property FromDate(ByVal row) As DateType
            Get
                Return row(COL_NAME_FROM_DATE).ToString()
            End Get
        End Property


        Public Shared ReadOnly Property ToDate(ByVal row) As DateType
            Get
                Return row(COL_NAME_TO_DATE).ToString()
            End Get
        End Property

        Public Shared ReadOnly Property BillingDatet(ByVal row) As DateType
            Get
                Return row(COL_NAME_BILLING_DATE).ToString()
            End Get
        End Property

        Public Shared ReadOnly Property OpenAmount(ByVal row) As DateType
            Get
                Return row(COL_NAME_OPEN_AMOUNT).ToString()
            End Get
        End Property

        Public Shared ReadOnly Property DateProcessed(ByVal row) As DateType
            Get
                Return row(COL_NAME_DATE_PROCESSED).ToString()
            End Get
        End Property

        Public Shared ReadOnly Property IncomingAmount(ByVal row) As String
            Get
                Return row(COL_NAME_INCOMING_AMOUNT).ToString()
            End Get
        End Property

    End Class
#End Region

#Region "BillPayTotal"
    Public Class BillPayTotals
        Inherits DataView

#Region "Constants"
        Public Const COL_DETAIL_COUNT As String = "billing_count"
        Public Const COL_BILLPAY_AMOUNT_TOTAL As String = "billed_amount_total"
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
    Public Class BillPayLaterRow
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
