
Public Class PaymentDetail
    Inherits BusinessObjectBase

    Public Shared Function getPaymentTotals(certId As Guid) As PaymentTotals
        Try
            Dim dal As New PaymentDetailDAL
            Return New PaymentTotals(dal.LoadPaymentTotals(certId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function getHistoryList(certId As Guid, Sortby As String) As PaymentHistorySearchDV
        Try
            Dim dal As New PaymentDetailDAL
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Return New PaymentHistorySearchDV(dal.LoadPaymentHistList(certId, Sortby).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#Region "PaymentHistorySearchDV"
    Public Class PaymentHistorySearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_NAME_PAYMENT_AMOUNT As String = PaymentDetailDAL.COL_NAME_PAYMENT_AMOUNT
        Public Const COL_NAME_SERIAL_NUMBER As String = PaymentDetailDAL.COL_NAME_SERIAL_NUMBER
        Public Const COL_NAME_COVERAGE_SEQ As String = PaymentDetailDAL.COL_NAME_COVERAGE_SEQ
        Public Const COL_NAME_DATE_PAID_FROM As String = PaymentDetailDAL.COL_NAME_DATE_PAID_FROM
        Public Const COL_NAME_DATE_PAID_FOR As String = PaymentDetailDAL.COL_NAME_DATE_PAID_FOR
        Public Const COL_NAME_DATE_OF_PAYMENT As String = PaymentDetailDAL.COL_NAME_DATE_OF_PAYMENT
        Public Const COL_NAME_DATE_PROCESSED As String = PaymentDetailDAL.COL_NAME_DATE_PROCESSED
        Public Const COL_NAME_SOURCE As String = PaymentDetailDAL.COL_NAME_SOURCE
        Public Const COL_NAME_INCOMING_AMOUNT As String = PaymentDetailDAL.COL_NAME_INCOMING_AMOUNT
        'Req - 1016 - Start
        Public Const COL_NAME_PAYMENT_DUE_DATE As String = PaymentDetailDAL.COL_NAME_PAYMENT_DUE_DATE
        'Req - 1016 - End
        Public Const COL_NAME_INSTALLMENT_NUM As String = PaymentDetailDAL.COL_NAME_INSTALLMENT_NUM
        Public Const COL_NAME_PAYMENT_INFO As String = PaymentDetailDAL.COL_NAME_PAYMENT_INFO
        Public Const COL_NAME_PAYMENT_REFERENCE_NUMBER As String = PaymentDetailDAL.COL_NAME_PAYMENT_REFERENCE_NUMBER
#End Region

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property SerialNumber(row) As String
            Get
                Return row(COL_NAME_SERIAL_NUMBER).ToString()
            End Get
        End Property

        Public Shared ReadOnly Property PaymentAmount(row) As String
            Get
                Return row(COL_NAME_PAYMENT_AMOUNT).ToString()
            End Get
        End Property


        Public Shared ReadOnly Property CoverageSequence(row) As Integer
            Get
                Return row(COL_NAME_COVERAGE_SEQ).ToString()
            End Get
        End Property

        Public Shared ReadOnly Property DatePaidFrom(row) As DateType
            Get
                Return row(COL_NAME_DATE_PAID_FROM).ToString()
            End Get
        End Property


        Public Shared ReadOnly Property DatePaidFor(row) As DateType
            Get
                Return row(COL_NAME_DATE_PAID_FOR).ToString()
            End Get
        End Property

        Public Shared ReadOnly Property DateOfPayment(row) As DateType
            Get
                Return row(COL_NAME_DATE_OF_PAYMENT).ToString()
            End Get
        End Property

        Public Shared ReadOnly Property DateProcessed(row) As DateType
            Get
                Return row(COL_NAME_DATE_PROCESSED).ToString()
            End Get
        End Property

        Public Shared ReadOnly Property IncomingAmount(row) As String
            Get
                Return row(COL_NAME_INCOMING_AMOUNT).ToString()
            End Get
        End Property

        'Req - 1016 Start
        Public Shared ReadOnly Property PaymentDueDate(row) As DateType
            Get
                Return row(COL_NAME_PAYMENT_DUE_DATE).ToString()
            End Get
        End Property
        'Req - 1016 End
        Public Shared ReadOnly Property PaymentInfo(row) As String
            Get
                Return row(COL_NAME_PAYMENT_INFO).ToString()
            End Get
        End Property

        Public Shared ReadOnly Property PaymentReferenceNumber(row) As String
        Get
            Return row(COL_NAME_PAYMENT_REFERENCE_NUMBER).ToString()
        End Get
    End Property
End Class


#End Region

#Region "PaymentTotal"
    Public Class PaymentTotals
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
