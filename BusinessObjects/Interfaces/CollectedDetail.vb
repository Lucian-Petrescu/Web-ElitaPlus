Public Class CollectedDetail
             Inherits BusinessObjectBase


 Public Shared Function getCollectedTotals(ByVal certId As Guid) As CollectedTotals
        Try
            Dim dal As New PaymentDetailDAL
            Return New CollectedTotals(dal.LoadCollectedTotals(certId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
 End Function 

    Public Shared Function getCollectedHistoryList(ByVal certId As Guid, ByVal Sortby As String) As CollectedHistorySearchDV
        Try
            Dim dal As New PaymentDetailDAL
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Return New CollectedHistorySearchDV(dal.LoadCollectedHistList(certId, Sortby).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function


#Region "CollectedHistorySearchDV"

    Public Class CollectedHistorySearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_NAME_COLLECTED_AMOUNT As String = PaymentDetailDAL.COL_NAME_COLLECTED_AMOUNT
        Public Const COL_NAME_COLLECTED_DATE As String = PaymentDetailDAL.COL_NAME_COLLECTED_DATE
        Public Const COL_NAME_BILLING_START_DATE As String = PaymentDetailDAL.COL_NAME_BILLING_START_DATE
        Public Const COL_NAME_INCOMING_AMOUNT As String = PaymentDetailDAL.COL_NAME_INCOMING_AMOUNT
        Public Const COL_NAME_INSTALLMENT_NUM As String = PaymentDetailDAL.COL_NAME_INSTALLMENT_NUM
        Public Const COL_NAME_CREATED_DATE As String = PaymentDetailDAL.COL_NAME_Created_date

#End Region

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property CollectedAmount(ByVal row) As String
            Get
                Return row(COL_NAME_COLLECTED_AMOUNT).ToString()
            End Get
        End Property

        Public Shared ReadOnly Property CollectedDate(ByVal row) As DateType
            Get
                Return row(COL_NAME_COLLECTED_DATE).ToString()
            End Get
        End Property

        Public Shared ReadOnly Property BillingStartDate(ByVal row) As DateType
            Get
                Return row(COL_NAME_BILLING_START_DATE).ToString()
            End Get
        End Property
        Public Shared ReadOnly Property CREATED_DATE(ByVal row) As DateType
            Get
                Return row(COL_NAME_CREATED_DATE).ToString()
            End Get
        End Property

        Public Shared ReadOnly Property IncomingAmount(ByVal row) As String
            Get
                Return row(COL_NAME_INCOMING_AMOUNT).ToString()
            End Get
        End Property

    End Class

#End Region

#Region "CollectedTotal"
    Public Class CollectedTotals
        Inherits DataView

#Region "Constants"
        Public Const COL_DETAIL_COUNT As String = "collected_count"
        Public Const COL_DETAIL_COLLECTED_AMOUNT_TOTAL As String = "collected_amount_total"
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
