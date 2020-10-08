Public Class CollectedPayDetail
    Inherits BusinessObjectBase
    Public Shared Function getCollectPayTotals(certId As Guid) As CollectPayTotals
        Try
            Dim dal As New BillingPayDetailDAL
            Return New CollectPayTotals(dal.LoadCollectPayTotals(certId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function getCollectPayHistList(certId As Guid, Sortby As String) As CollectPayHistorySearchDV
        Try
            Dim dal As New BillingPayDetailDAL
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Return New CollectPayHistorySearchDV(dal.LoadCollectPayHistList(langId, certId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#Region "PaymentHistorySearchDV"
    Public Class CollectPayHistorySearchDV
        Inherits DataView

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

#Region "Constants"

        Public Const COL_NAME_COVERAGE_SEQ_PC As String = BillingPayDetailDAL.COL_NAME_COVERAGE_SEQUENCE_PC
        Public Const COL_NAME_COLLECTED_DATE As String = BillingPayDetailDAL.COL_NAME_COLLECTED_DATE
        Public Const COL_NAME_DATE_PROCESSED_PC As String = BillingPayDetailDAL.COL_NAME_DATE_PROCESSED_PC
        Public Const COL_NAME_COLLECTED_AMOUNT As String = BillingPayDetailDAL.COL_NAME_COLLECTED_AMOUNT
        Public Const COL_NAME_PAYMENT_AMOUNT As String = BillingPayDetailDAL.COL_NAME_PAYMENT_AMOUNT
        Public Const COL_NAME_INSTALLMENT_NUMB_PC As String = BillingPayDetailDAL.COL_NAME_INSTALLMENT_NUM
        'Public Const COL_NAME_CREDIT_CARD_TYPE As String = BillingPayDetailDAL.COL_NAME_CREDIT_CARD_TYPE
        'Public Const COL_NAME_CREDIT_CARD_NUMBER As String = BillingPayDetailDAL.COL_NAME_CREDIT_CARD_NUMBER
        Public Const COL_NAME_PAYMENT_METHOD As String = BillingPayDetailDAL.COL_NAME_PAYMENT_METHOD
        Public Const COL_NAME_PAYMENT_INSTRUMENT_NUMBER As String = BillingPayDetailDAL.COL_NAME_PAYMENT_INSTRUMENT_NUMBER
        Public Const COL_NAME_DATE_SEND As String = BillingPayDetailDAL.COL_NAME_DATE_SEND
        Public Const COL_NAME_REJECT_CODE_PC As String = BillingPayDetailDAL.COL_NAME_REJECT_CODE_PC
        Public Const COL_NAME_REJECT_REASON_PC As String = BillingPayDetailDAL.COL_NAME_REJECT_REASON_PC
        Public Const COL_NAME_REJECT_DATE_PC As String = BillingPayDetailDAL.COL_NAME_REJECT_DATE_PC
        Public Const COL_NAME_PAYMENT_STATUS As String = BillingPayDetailDAL.COL_NAME_PAYMENT_STATUS
        Public Const COL_NAME_PROCESSOR_REJECT_CODE As String = BillingDetailDAL.COL_NAME_PROCESSOR_REJECT_CODE
        Public Const COL_NAME_CERT_PAYMENT_ID As String = BillingPayDetailDAL.COL_NAME_CERT_PAYMENT_ID
        Public Const COL_NAME_PAYMENT_ID As String = BillingPayDetailDAL.COL_NAME_PAYMENT_ID
        ' Public Const COL_NAME_SOURCE As String = BillingPayDetailDAL.COL_NAME_SOURCE
        Public Const COL_NAME_PAYMENT_TYPE_XCD As String = BillingPayDetailDAL.COL_NAME_PAYMENT_TYPE_XCD
        'Req - 1016 - Start
        Public Const COL_NAME_PAYMENT_DUE_DATE As String = BillingPayDetailDAL.COL_NAME_PAYMENT_DUE_DATE
        'Req - 1016 - End


#End Region

        Public Shared ReadOnly Property CollectedAmount(row) As String
            Get
                Return row(COL_NAME_COLLECTED_AMOUNT).ToString()
            End Get
        End Property


        Public Shared ReadOnly Property CoverageSequence(row) As Integer
            Get
                Return row(COL_NAME_COVERAGE_SEQ_PC).ToString()
            End Get
        End Property

        Public Shared ReadOnly Property RejectDate(row) As DateType
            Get
                Return row(COL_NAME_REJECT_DATE_PC).ToString()
            End Get
        End Property

        Public Shared ReadOnly Property CreatedDate(row) As DateType
            Get
                Return row(COL_NAME_DATE_PROCESSED_PC).ToString()
            End Get
        End Property

        Public Shared ReadOnly Property CollectedDate(row) As DateType
            Get
                Return row(COL_NAME_COLLECTED_DATE).ToString()
            End Get
        End Property

        'Public Shared ReadOnly Property IncomingAmount(ByVal row) As String
        '    Get
        '        Return row(COL_NAME_INCOMING_AMOUNT).ToString()
        '    End Get
        'End Property

        Public Shared ReadOnly Property BillingStartDate(row) As DateType
            Get
                Return row(COL_NAME_DATE_SEND).ToString()
            End Get
        End Property

        'Public Shared ReadOnly Property PaymentStatus(ByVal row) As String
        '    Get
        '        Return row(COL_NAME_PAYMENT_STATUS).ToString()
        '    End Get
        'End Property

        'Req - 1016 Start
        Public Shared ReadOnly Property PaymentDueDate(row) As DateType
            Get
                Return row(COL_NAME_PAYMENT_DUE_DATE).ToString()
            End Get
        End Property
        'Req - 1016 End

    End Class
#End Region

#Region "CollectPayTotal"
    Public Class CollectPayTotals
        Inherits DataView

#Region "Constants"
        Public Const COL_DETAIL_COUNT As String = "payment_count"
        Public Const COL_DETAIL_PAYMENT_AMOUNT_TOTAL As String = "payment_amount_total"
#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region
End Class
