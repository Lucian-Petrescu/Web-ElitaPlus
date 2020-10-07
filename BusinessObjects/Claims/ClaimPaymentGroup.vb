'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (2/21/2013)  ********************

Public Class ClaimPaymentGroup
    Inherits BusinessObjectBase

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
            Dim dal As New ClaimPaymentGroupDAL
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
            Dim dal As New ClaimPaymentGroupDAL
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

#Region "PaymentGroupSearchDV"

    Public Class PaymentGroupSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_NAME_PAYMENT_GROUP_ID As String = "payment_group_id"
        Public Const COL_NAME_SERVICE_CENTER_ID As String = "service_center_id"
        Public Const COL_NAME_PAYMENT_GROUP_NUMBER As String = "payment_group_number"
        Public Const COL_NAME_PAYMENT_GROUP_STATUS As String = "payment_group_status"
        Public Const COL_NAME_PAYMENT_GROUP_DATE As String = "payment_group_date"
        Public Const COL_NAME_PAYMENT_GROUP_TOTAL As String = "payment_group_total"
#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

#End Region

#Region "DataView Retrieveing Methods"

    Public Shared Function GetPaymentGroup(svcCenterId As Guid, PymntGrpNumber As String, _
                                           PymntGrpStatusId As Guid, _
                                           PaymentGroupDateRange As SearchCriteriaStructType(Of Date), _
                                           InvoiceNumber As String, _
                                           InvoiceGrpNumber As String) As PaymentGroupSearchDV
        Try

            Dim dal As New ClaimPaymentGroupDAL
            Return New PaymentGroupSearchDV(dal.GetPaymentGroup(svcCenterId, PymntGrpNumber, PymntGrpStatusId, _
                                                                PaymentGroupDateRange, InvoiceNumber, InvoiceGrpNumber).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

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
            If Row(ClaimPaymentGroupDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimPaymentGroupDAL.COL_NAME_PAYMENT_GROUP_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=40)> _
    Public Property PaymentGroupNumber As String
        Get
            CheckDeleted()
            If Row(ClaimPaymentGroupDAL.COL_NAME_PAYMENT_GROUP_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimPaymentGroupDAL.COL_NAME_PAYMENT_GROUP_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimPaymentGroupDAL.COL_NAME_PAYMENT_GROUP_NUMBER, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property PaymentGroupStatusId As Guid
        Get
            CheckDeleted()
            If Row(ClaimPaymentGroupDAL.COL_NAME_PAYMENT_GROUP_STATUS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimPaymentGroupDAL.COL_NAME_PAYMENT_GROUP_STATUS_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimPaymentGroupDAL.COL_NAME_PAYMENT_GROUP_STATUS_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property PaymentGroupDate As DateType
        Get
            CheckDeleted()
            If Row(ClaimPaymentGroupDAL.COL_NAME_PAYMENT_GROUP_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ClaimPaymentGroupDAL.COL_NAME_PAYMENT_GROUP_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimPaymentGroupDAL.COL_NAME_PAYMENT_GROUP_DATE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property PaymentGroupTotal As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimPaymentGroupDAL.COL_NAME_PAYMENT_GROUP_TOTAL) Is DBNull.Value Then
                Return New DecimalType(0D)
            Else
                Return New DecimalType(CType(Row(ClaimPaymentGroupDAL.COL_NAME_PAYMENT_GROUP_TOTAL), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimPaymentGroupDAL.COL_NAME_PAYMENT_GROUP_TOTAL, Value)
        End Set
    End Property

    Dim _companyId As Guid
    Public Property CompanyId As Guid
        Get
            Return _companyId
        End Get
        Set
            _companyId = value
        End Set
    End Property


#End Region

    Public ReadOnly Property PymntGrpDetailChildren As ClaimPaymentGroupDetail.PymntGrpDetailList
        Get
            Return New ClaimPaymentGroupDetail.PymntGrpDetailList(Me)
        End Get
    End Property

#Region "Public Members"

    Public Function GetPymntGroupDetailChild(childId As Guid) As ClaimPaymentGroupDetail
        Return CType(PymntGrpDetailChildren.GetChild(childId), ClaimPaymentGroupDetail)
    End Function

    Public Function GetNewPymntGroupDetailChild() As ClaimPaymentGroupDetail
        Dim newPymntGrpDetail As ClaimPaymentGroupDetail = CType(PymntGrpDetailChildren.GetNewChild, ClaimPaymentGroupDetail)
        newPymntGrpDetail.PaymentGroupId = Id
        Return newPymntGrpDetail
    End Function

    Public Sub Save(Optional ByVal Transaction As IDbTransaction = Nothing)
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then

                Dim dal As New ClaimPaymentGroupDAL
                dal.UpdateFamily(Dataset, CompanyId, Transaction)

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

    Public Sub processClaimAuthorizations()
        Dim claimAuthId As Guid
        Dim invoiceId As Guid
        Dim invReconId As Guid
        Dim invoiceNum As String
        Dim excludeDed As Boolean
        Dim oInvRecon As InvoiceReconciliation
        Dim oClaimAuth As ClaimAuthorization
        Dim oInvoice As Invoice
        Dim createPaymentDV As DataView = Nothing
        Dim reconciledAmount As DecimalType
        Dim Transaction As IDbTransaction = DBHelper.GetNewTransaction

        Try
            createPaymentDV = ClaimPaymentGroupDetail.GetClaimAuthorizationsToBePaid(Id)

            For Each dr As DataRow In createPaymentDV.Table.Rows
                claimAuthId = New Guid(CType(dr(ClaimPaymentGroupDetail.PaymentGroupDetailSearchDV.COL_NAME_AUTHORIZATION_ID), Byte()))
                invoiceId = New Guid(CType(dr(ClaimPaymentGroupDetail.PaymentGroupDetailSearchDV.COL_NAME_INVOICE_ID), Byte()))
                reconciledAmount = New DecimalType(CType(dr(ClaimPaymentGroupDetail.PaymentGroupDetailSearchDV.COL_NAME_RECONCILED_AMOUNT), Decimal))
                invoiceNum = CType(dr(ClaimPaymentGroupDetail.PaymentGroupDetailSearchDV.COL_NAME_INVOICE_NUMBER), String)
                excludeDed = CType(dr(ClaimPaymentGroupDetail.PaymentGroupDetailSearchDV.COL_NAME_EXCLUDE_DEDUCTIBLE), Boolean)

                oClaimAuth = New ClaimAuthorization(claimAuthId, Dataset)
                oInvoice = New Invoice(invoiceId, Dataset)

                'Create claim Invoice and Dibursement entry for the Accounting.
                CreateAndSavePayment(oClaimAuth, reconciledAmount, oInvoice, invoiceNum, excludeDed, Transaction)

                'Get all the Invoice Recons for the Claim Authorization and Mark them as Selected for Payment
                For Each ClaimAuthItem As ClaimAuthItem In oClaimAuth.ClaimAuthorizationItemChildren
                    If Not ClaimAuthItem.InvoiceReconciliationId = Guid.Empty Then
                        oInvRecon = New InvoiceReconciliation(ClaimAuthItem.InvoiceReconciliationId, Dataset)
                        oInvRecon.ReconciliationStatusId = LookupListNew.GetIdFromCode(LookupListNew.LK_INV_RECON_STAT, _
                                                                                       Codes.INVOICE_RECON_STATUS_PAID)
                        oInvRecon.Save()
                    End If
                Next

                'Mark the Claim Authorization as Paid
                oClaimAuth.ClaimAuthStatus = ClaimAuthorizationStatus.Paid
                oClaimAuth.Save()
            Next

            'Change the Status of the Claim Payment Group as Paid and Save
            PaymentGroupStatusId = LookupListNew.GetIdFromCode(LookupListNew.LK_PAYMENT_GRP_STAT, Codes.PYMNT_GRP_STATUS_APPROVED_FOR_PAYMENT)

            'Invoice Recon/Claim Auth should Save in the same Transaction as that of Claim and Claim Invoice/Disbursement.
            Save(Transaction)
            'We are the creator of the transaction we should commit it  and close the connection
            DBHelper.Commit(Transaction)
            Load(Id) 'Reload the Payment Group again after commiting the Batch

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            DBHelper.RollBack(Transaction)
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Sub

    Private Sub CreateAndSavePayment(oClaimAuth As ClaimAuthorization, reconciledAmount As DecimalType, _
                                     oInvoice As Invoice, invoiceNumber As String, _
                                     excludeDed As Boolean, Transaction As IDbTransaction)
        ''ByVal invoiceId As Guid, ByVal invoiceNumber As String, _
        Dim ClaimInvoiceBO As ClaimInvoice
        Dim DisbursementBO As Disbursement
        ClaimInvoiceBO = New ClaimInvoice(Dataset) 'Do Not create Claim Invoice as a Family DataSet but Control the Commit with a Transation.
        DisbursementBO = ClaimInvoiceBO.AddNewDisbursement()

        ClaimInvoiceBO.excludeDeductible = excludeDed
        ClaimInvoiceBO.ReconciledAmount = reconciledAmount
        ClaimInvoiceBO.SvcControlNumber = invoiceNumber
        ClaimInvoiceBO.Invoice = oInvoice
        ClaimInvoiceBO.InvoiceDate = oInvoice.InvoiceDate
        ClaimInvoiceBO.PrepopulateClaimInvoice(oClaimAuth)
        'Claim Invoice Should always be populated before Disbursement.
        ClaimInvoiceBO.PrepopulateDisbursment()

        'default values
        ClaimInvoiceBO.BeginEdit()
        If Not String.IsNullOrEmpty(oClaimAuth.BatchNumber) Then
            ClaimInvoiceBO.BatchNumber = oClaimAuth.BatchNumber
        Else
            ClaimInvoiceBO.BatchNumber = "1"
        End If
        ClaimInvoiceBO.LaborTax = New DecimalType(0D)
        ClaimInvoiceBO.PartTax = New DecimalType(0D)
        ClaimInvoiceBO.RecordCount = New LongType(1)
        ClaimInvoiceBO.Source = Nothing

        'Save the BO and Process the Claims
        ClaimInvoiceBO.Save(Transaction, True)
        ClaimInvoiceBO.EndEdit()
    End Sub

    Public Sub CreatePaymentGroupDetail(claimAuthId As Guid, excludeDed As String, _
                                        claimAuthReconAmount As DecimalType, ByRef paymentAmount As DecimalType)
        Dim MyPymntGrpDetailChildBO As ClaimPaymentGroupDetail
        Dim oClaimAuthBO As ClaimAuthorization
        Dim oInvRecon As InvoiceReconciliation

        MyPymntGrpDetailChildBO = GetNewPymntGroupDetailChild()
        MyPymntGrpDetailChildBO.BeginEdit()

        MyPymntGrpDetailChildBO.ClaimAuthorizationId = claimAuthId

        MyPymntGrpDetailChildBO.ExcludeDeductible = excludeDed

        'ClaimAuth saves as a Family
        oClaimAuthBO = New ClaimAuthorization(claimAuthId, Dataset)

        'Add the Reconciled Amount at Claim Auth level to the Payment Group Detail
        paymentAmount = paymentAmount.Value + claimAuthReconAmount.Value

        'Get all the Invoice Recons for the Claim Authorization and Mark them as Selected for Payment
        For Each ClaimAuthItem As ClaimAuthItem In oClaimAuthBO.ClaimAuthorizationItemChildren
            If Not ClaimAuthItem.InvoiceReconciliationId = Guid.Empty Then
                oInvRecon = New InvoiceReconciliation(ClaimAuthItem.InvoiceReconciliationId, Dataset)
                oInvRecon.ReconciliationStatusId = LookupListNew.GetIdFromCode(LookupListNew.LK_INV_RECON_STAT, _
                                                                               Codes.INVOICE_RECON_STATUS_TO_BE_PAID)
                oInvRecon.Save()
            End If
        Next

        'Mark the Claim Authorization as Selected for Payment 
        oClaimAuthBO.ClaimAuthStatus = ClaimAuthorizationStatus.ToBePaid
        oClaimAuthBO.Save()

        MyPymntGrpDetailChildBO.Save()
        MyPymntGrpDetailChildBO.EndEdit()
    End Sub

#End Region




End Class
