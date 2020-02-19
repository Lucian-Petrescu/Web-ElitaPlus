Imports System.Text
Imports System.Collections.Generic

Public Class InvoiceTrans
    Inherits BusinessObjectBase

#Region "Constants"
    Private Const INVOICE_NUM_ALREADY_EXISTS As String = "INVOICE_NUM_ALREADY_EXISTS"
#End Region

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

    Public Sub New(ByVal ClaimInvoiceBO As ClaimInvoice)
        MyBase.New(False)
        Me.Dataset = ClaimInvoiceBO.Dataset
        Me.Load()
    End Sub

    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Me.Dataset = row.Table.DataSet
        Me.Row = row
    End Sub


    Protected Sub Load()
        Try
            Dim dal As New InvoiceTransDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New InvoiceTransDAL(ElitaPlusIdentity.Current.ActiveUser.Id)
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
                Throw New DataNotFoundException(Assurant.ElitaPlus.Common.ErrorTypes.ERROR_BO)
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
#End Region

#Region "Private Members"
    Private _invoiceTrans As InvoiceTrans = Nothing

    'Initialization code for new objects
    Private Sub Initialize()
    End Sub
#End Region

#Region "Properties"

    Public UserId As Guid

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If Row(InvoiceTransDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(InvoiceTransDAL.COL_NAME_INVOICE_TRANS_ID), Byte()))
            End If
        End Get
    End Property
    <ValueMandatory("")> _
    Public Property ServiceCenterId() As Guid
        Get
            CheckDeleted()
            If Row(InvoiceTransDAL.COL_NAME_SERVICE_CENTER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(InvoiceTransDAL.COL_NAME_SERVICE_CENTER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(InvoiceTransDAL.COL_NAME_SERVICE_CENTER_ID, Value)
        End Set
    End Property

    Public Property ServiceCenterName() As String
        Get
            CheckDeleted()
            If Row(InvoiceTransDAL.COL_NAME_SERVICE_CENTER_NAME) Is DBNull.Value Then
                Return String.Empty
            Else
                Return CType(Row(InvoiceTransDAL.COL_NAME_SERVICE_CENTER_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(InvoiceTransDAL.COL_NAME_SERVICE_CENTER_NAME, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property InvoiceTypeId() As Guid
        Get
            CheckDeleted()
            If Row(InvoiceTransDAL.COL_NAME_INVOICE_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(InvoiceTransDAL.COL_NAME_INVOICE_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(InvoiceTransDAL.COL_NAME_INVOICE_TYPE_ID, Value)
        End Set
    End Property


    Public Property InvoiceTypeName() As String
        Get
            CheckDeleted()
            If Row(InvoiceTransDAL.COL_NAME_INVOICE_TYPE_NAME) Is DBNull.Value Then
                Return String.Empty
            Else
                Return CType(Row(InvoiceTransDAL.COL_NAME_INVOICE_TYPE_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(InvoiceTransDAL.COL_NAME_INVOICE_TYPE_NAME, Value)
        End Set
    End Property

    Public Property InvoiceStatusId() As Guid
        Get
            CheckDeleted()
            If Row(InvoiceTransDAL.COL_NAME_INVOICE_STATUS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(InvoiceTransDAL.COL_NAME_INVOICE_STATUS_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(InvoiceTransDAL.COL_NAME_INVOICE_STATUS_ID, Value)
        End Set
    End Property

    Public Property InvoiceStatusName() As String
        Get
            CheckDeleted()
            If Row(InvoiceTransDAL.COL_NAME_INVOICE_STATUS_NAME) Is DBNull.Value Then
                Return String.Empty
            Else
                Return CType(Row(InvoiceTransDAL.COL_NAME_INVOICE_STATUS_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(InvoiceTransDAL.COL_NAME_INVOICE_STATUS_NAME, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=20)> _
    Public Property SvcControlNumber() As String
        Get
            CheckDeleted()
            If Row(InvoiceTransDAL.COL_NAME_SVC_CONTROL_NUMBER) Is DBNull.Value Then
                Return String.Empty
            Else
                Return CType(Row(InvoiceTransDAL.COL_NAME_SVC_CONTROL_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(InvoiceTransDAL.COL_NAME_SVC_CONTROL_NUMBER, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property SvcControlAmount() As Double
        Get
            CheckDeleted()
            If Row(InvoiceTransDAL.COL_NAME_SVC_CONTROL_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(InvoiceTransDAL.COL_NAME_SVC_CONTROL_AMOUNT), Double)
            End If
        End Get
        Set(ByVal Value As Double)
            CheckDeleted()
            Me.SetValue(InvoiceTransDAL.COL_NAME_SVC_CONTROL_AMOUNT, Math.Round(Value, 2))
        End Set
    End Property

    Public ReadOnly Property BatchStatus() As String
        Get
            CheckDeleted()
            If Row(InvoiceTransDAL.COL_NAME_BATCH_STATUS) Is DBNull.Value Then
                Return String.Empty
            Else
                Return CType(Row(InvoiceTransDAL.COL_NAME_BATCH_STATUS), String)
            End If
        End Get
    End Property

    Public ReadOnly Property BatchStartTime() As Date
        Get
            CheckDeleted()
            If Row(InvoiceTransDAL.COL_NAME_BATCH_START_TIME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(InvoiceTransDAL.COL_NAME_BATCH_START_TIME), String)
            End If
        End Get
    End Property

    Public ReadOnly Property BatchProcessTime() As Double
        Get
            CheckDeleted()
            If Row(InvoiceTransDAL.COL_NAME_BATCH_PROCESS_TIME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(InvoiceTransDAL.COL_NAME_BATCH_PROCESS_TIME), Double)
            End If
        End Get
    End Property
    <ValidStringLength("", Max:=15)>
    Public Property BatchNumber() As String
        Get
            CheckDeleted()
            If Row(InvoiceTransDAL.COL_NAME_BATCH_NUMBER) Is DBNull.Value Then
                Return String.Empty
            Else
                Return CType(Row(InvoiceTransDAL.COL_NAME_BATCH_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(InvoiceTransDAL.COL_NAME_BATCH_NUMBER, Value)
        End Set
    End Property

    Public Property RegionId() As Guid
        Get
            CheckDeleted()
            If Row(InvoiceTransDAL.COL_NAME_REGION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(InvoiceTransDAL.COL_NAME_REGION_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(InvoiceTransDAL.COL_NAME_REGION_ID, Value)
        End Set
    End Property


    Public Property Tax1Amount() As Double
        Get
            CheckDeleted()
            If Row(InvoiceTransDAL.COL_NAME_TAX1_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(InvoiceTransDAL.COL_NAME_TAX1_AMOUNT), Double)
            End If
        End Get
        Set(ByVal Value As Double)
            CheckDeleted()
            Me.SetValue(InvoiceTransDAL.COL_NAME_TAX1_AMOUNT, Math.Round(Value, 2))
        End Set
    End Property

    Public Property Tax2Amount() As Double
        Get
            CheckDeleted()
            If Row(InvoiceTransDAL.COL_NAME_TAX2_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(InvoiceTransDAL.COL_NAME_TAX2_AMOUNT), Double)
            End If
        End Get
        Set(ByVal Value As Double)
            CheckDeleted()
            Me.SetValue(InvoiceTransDAL.COL_NAME_TAX2_AMOUNT, Math.Round(Value, 2))
        End Set
    End Property
    <ValueMandatoryConditionallyForInvDate(""), ValidInvoiceDate("")> _
    Public Property InvoiceDate() As DateType
        Get
            CheckDeleted()
            If Row(InvoiceTransDAL.COL_NAME_INVOICE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(InvoiceTransDAL.COL_NAME_INVOICE_DATE).ToString()))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(InvoiceTransDAL.COL_NAME_INVOICE_DATE, Value)
        End Set
    End Property
    <ValidInvoiceDate("")> _
    Public Property InvoiceReceivedDate() As DateType
        Get
            CheckDeleted()
            If Row(InvoiceTransDAL.COL_NAME_INVOICE_RECEIVED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(InvoiceTransDAL.COL_NAME_INVOICE_RECEIVED_DATE).ToString()))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(InvoiceTransDAL.COL_NAME_INVOICE_RECEIVED_DATE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=100)> _
    Public Property InvoiceComments() As String
        Get
            CheckDeleted()
            If Row(InvoiceTransDAL.COL_NAME_INVOICE_COMMENTS) Is DBNull.Value Then
                Return String.Empty
            Else
                Return CType(Row(InvoiceTransDAL.COL_NAME_INVOICE_COMMENTS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(InvoiceTransDAL.COL_NAME_INVOICE_COMMENTS, Value)
        End Set
    End Property

    Public ReadOnly Property InvoiceCreatedDate() As Date
        Get
            CheckDeleted()
            If Row(InvoiceTransDAL.COL_NAME_INVOICE_CREATED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Dim dt As DateTime
                dt = DateHelper.GetDateValue(Row(InvoiceTransDAL.COL_NAME_INVOICE_CREATED_DATE).ToString())
                Return dt
            End If
        End Get
    End Property
#End Region

#Region "Custom Validation"
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValueMandatoryConditionallyForInvDate
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVOICE_DATE_REQUIRED_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As InvoiceTrans = CType(objectToValidate, InvoiceTrans)
            Dim oCompaniesDv As DataView
            oCompaniesDv = User.GetUserCompanies(ElitaPlusIdentity.Current.ActiveUser.Id)
            oCompaniesDv.RowFilter = "code in ('ABA','VBA','SBA')"
            If oCompaniesDv.Count > 0 Then
                If obj.InvoiceDate Is Nothing Then
                    Return False
                Else
                    Return True
                End If
            Else
                Return True
            End If
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidInvoiceDate
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_INVOICE_DATE_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As InvoiceTrans = CType(objectToValidate, InvoiceTrans)

            If Not obj.InvoiceDate Is Nothing Then

                If obj.InvoiceDate.Value <= System.DateTime.Today Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return True
            End If

        End Function
    End Class
#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New InvoiceTransDAL(Me.UserId)
                Dim objId As Guid
                If Me.IsNew Then
                    objId = dal.CreateBatch(Me.ServiceCenterId, Me.SvcControlNumber, Me.SvcControlAmount, Me.BatchNumber, Me.UserId, Me.InvoiceDate, Me.InvoiceStatusId, Me.InvoiceReceivedDate, Me.InvoiceTypeId)

                    If objId = Guid.Empty Then
                        Throw New DataNotFoundException(INVOICE_NUM_ALREADY_EXISTS)
                    End If
                Else
                    If dal.UpdateBatch(Me.Id, Me.ServiceCenterId, Me.SvcControlNumber, Me.SvcControlAmount, Me.BatchNumber, Me.UserId, Me.InvoiceDate, Me.InvoiceStatusId, Me.InvoiceReceivedDate, Me.InvoiceTypeId) Then
                        objId = Me.Id
                    Else
                        Throw New DatabaseException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, New Exception(ElitaPlus.Common.ErrorCodes.DAL_ERROR))
                    End If
                End If
                'Reload the Data from the DB
                If Me.Row.RowState <> DataRowState.Detached Then
                    Me.Dataset = New DataSet
                    Me.Row = Nothing
                    Me.Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    'Returns a list of invoice batch records.
    Public Shared Function GetList(ByVal svcControlNumber As String, ByVal serviceCenterId As Guid, ByVal userId As Guid, ByVal BatchNumber As String,
                                   ByVal svcControlAmount As String, ByVal InvoiceDate As String, ByVal invoiceTypeId As Guid, ByVal invoiceStatusId As Guid, ByVal invoiceReceivedDate As String) As InvoiceTransSearchDV

        Try
            Dim dal As New InvoiceTransDAL
            Return New InvoiceTransSearchDV(dal.LoadList(Guid.Empty, serviceCenterId, svcControlNumber, userId, BatchNumber, svcControlAmount, InvoiceDate, invoiceTypeId, invoiceStatusId, invoiceReceivedDate).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetInvoiceComments(ByVal invoice_trans_id As String) As String

        Dim retVal As String
        Dim dal As New InvoiceTransDAL
        Dim dataset As DataSet = dal.GetInvoiceComments(invoice_trans_id)
        Dim dataTable As DataTable = dataset.Tables(0)

        If dataTable.Rows(0).Item(0) Is DBNull.Value Then
            retVal = "N/A"
        Else
            retVal = dataTable.Rows(0).Item(0)
        End If

        Return retVal

    End Function

    Public Function UpdateRejectReason(ByVal invoiceTransId As Guid, ByVal InvoiceComments As String) As Boolean
        Try

            Dim dal As New InvoiceTransDAL
            Return dal.UpdateRejectReason(invoiceTransId, InvoiceComments)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Function

    'Returns a specific invocie batch record
    Public Shared Function GetInvoiceTrans(ByVal id As Guid) As InvoiceTransSearchDV

        Try
            Dim dal As New InvoiceTransDAL
            Return New InvoiceTransSearchDV(dal.Load(id).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetInvoiceTransDetail(ByVal invoiceTransId As Guid) As InvoiceTransDetailDV

        Try
            Dim dal As New InvoiceTransDAL
            Return New InvoiceTransDetailDV(dal.LoadbatchDetail(invoiceTransId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    'Deletes the batch header and all of the related detail records
    Public Function DeleteBatch(ByVal id As Guid) As Boolean

        Try
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New InvoiceTransDAL
                Return dal.DeleteBatch(id)
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try

    End Function

    Public Sub SaveBatch(ByVal ClaimSet As BatchClaimInvoice, ByVal id As Guid)
        Try
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New InvoiceTransDAL
                dal.SaveClaims(ClaimSet, id)
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
    Public Function SaveBatchTax(ByVal id As Guid, ByVal tax1_Amt As Double, ByVal Tax2_amt As Double, ByVal batch_number As String _
                                , ByVal region_id As Guid) As Boolean
        Try
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New InvoiceTransDAL
                Return dal.SaveBatchTax(id, region_id, batch_number, tax1_Amt, Tax2_amt, Me.UserId)
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Function

    Public Function ProcessBatch(ByVal id As Guid, ByVal InvoiceTaxTypeId As Guid) As Boolean
        Try
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New InvoiceTransDAL
                Return dal.ProcessBatch(id, InvoiceTaxTypeId)
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Function

    'REQ 1150 added another parameter - dealerId
    Public Function CheckInvoiceTaxType(ByVal companyId As Guid, ByVal taxtypeID As Guid, ByVal dealerId As Guid) As DataSet
        Try
            Dim countryID As Guid
            Dim dal As New InvoiceTransDAL
            Dim companyBO As Company = New Company(companyId)
            countryID = companyBO.BusinessCountryId
            'REQ 1150
            Return dal.GetInvoiceTaxTypeDetails(taxtypeID, countryID, dealerId)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Friend Shared Function CheckInvoiceTaxTypeByCountry(ByVal countryID As Guid, ByVal taxtypeID As Guid, ByVal dealerId As Guid) As DataSet
        Try
            Dim dal As New InvoiceTransDAL
            Return dal.GetInvoiceTaxTypeDetails(taxtypeID, countryID, dealerId)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Function UpdateExcludeDeductibleFlag(ByVal strExcludeDeductible As String, ByVal invoiceTransDetailId As Guid) As Boolean
        Try

            Dim dal As New InvoiceTransDAL

            Return dal.UpdateExcludeDeductible(strExcludeDeductible, invoiceTransDetailId)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Function UpdatePaymentAmount(ByVal invoiceTransDetailId As Guid) As Boolean
        Try

            Dim dal As New InvoiceTransDAL

            Return dal.UpdatePaymentAmount(invoiceTransDetailId)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    'REQ 5565 
    Public Function CheckForPreInvoice(ByVal BatchNumber As String) As DataSet
        Try
            Dim dal As New InvoiceTransDAL

            Return dal.CheckforPreInvoice(BatchNumber)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function


    Public Shared Function GetBatchClosedClaims(ByVal serviceCenterId As Guid, ByVal batchNumber As String, ByVal InvoiceTransId As Guid) As String

        Try
            Dim dal As New InvoiceTransDAL
            Dim dt As DataTable
            Dim lstr As New List(Of String)

            dt = dal.GetBatchClosedClaims(serviceCenterId, batchNumber, InvoiceTransId, ElitaPlusIdentity.Current.ActiveUser.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0)

            For Each row As DataRow In dt.Rows
                lstr.Add(row("claim_number"))
            Next
            Return String.Join(",", lstr.ToArray)


        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function
#End Region

#Region "DataView Retrieveing Methods"
    Public Class InvoiceTransSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_INVOICE_TRANS_ID As String = InvoiceTransDAL.COL_NAME_INVOICE_TRANS_ID
        Public Const COL_SERVICE_CENTER_ID As String = InvoiceTransDAL.COL_NAME_SERVICE_CENTER_ID
        Public Const COL_SERVICE_CENTER_NAME As String = InvoiceTransDAL.COL_NAME_SERVICE_CENTER_NAME
        Public Const COL_SVC_CONTROL_NUMBER As String = InvoiceTransDAL.COL_NAME_SVC_CONTROL_NUMBER
        Public Const COL_SVC_CONTROL_AMOUNT As String = InvoiceTransDAL.COL_NAME_SVC_CONTROL_AMOUNT
        Public Const COL_BATCH_STATUS As String = InvoiceTransDAL.COL_NAME_BATCH_STATUS
        Public Const COL_BATCH_START_TIME As String = InvoiceTransDAL.COL_NAME_BATCH_START_TIME
        Public Const COL_BATCH_PROCESS_TIME As String = InvoiceTransDAL.COL_NAME_BATCH_PROCESS_TIME
        Public Const COL_BATCH_NUMBER As String = InvoiceTransDAL.COL_NAME_BATCH_NUMBER
        Public Const COL_INVOICE_DATE As String = InvoiceTransDAL.COL_NAME_INVOICE_DATE
        Public Const COL_INVOICE_STATUS_ID As String = InvoiceTransDAL.COL_NAME_INVOICE_STATUS_ID
        Public Const COL_INVOICE_STATUS_NAME As String = InvoiceTransDAL.COL_NAME_INVOICE_STATUS_NAME
        Public Const COL_INVOICE_COMMENTS As String = InvoiceTransDAL.COL_NAME_INVOICE_COMMENTS
        Public Const COL_TOTAL_BONUS_PAID As String = "TotalBonusPaid"

#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

    Public Class InvoiceTransDetailDV
        Inherits DataView

#Region "Constants"


        Public Const COL_INVOICE_TRANS_DETAIL_ID As String = "invoice_trans_detail_id"
        Public Const COL_INVOICE_TRANS_ID As String = InvoiceTransDAL.COL_NAME_INVOICE_TRANS_ID
        Public Const COL_CLAIM_ID As String = "claim_id"
        Public Const COL_CLAIM_NUMBER As String = "claim_number"
        Public Const COL_CLAIM_MODIFIED_DATE As String = "claim_modified_date"
        Public Const COL_RESERVE_AMOUNT As String = "reserve_amount"
        Public Const COL_PAYMENT_AMOUNT As String = "payment_amount"
        Public Const COL_REPAIR_DATE As String = "repair_date"
        Public Const COL_PICKUP_DATE As String = "pickup_date"
        Public Const COL_SPARE_PARTS As String = "spare_parts"
        Public Const COL_CLOSE_CLAIM As String = "close_claim"
        Public Const COL_CONTACT_NAME As String = "contact_name"
        Public Const COL_STATUS As String = "claim_status"
        Public Const COL_LOSS_DATE As String = "loss_date"
        Public Const COL_SERVICE_CENTER As String = "description"
        Public Const COL_NAME_AUTHORIZATION_NUMBER As String = ClaimDAL.COL_NAME_AUTHORIZATION_NUMBER
        Public Const COL_SALVAGE_AMOUNT As String = "salvage_amount"
        Public Const COL_DEDUCTIBLE As String = "deductible"
        Public Const COL_EXCLUDE_DEDUCTIBLE As String = "exclude_deductible"
        Public Const COL_PRE_INVOICE_STATUS As String = "pre_invoice_status"
        Public Const COL_Total_BONUS As String = "TotalBonus"
        Public Const COL_ISPreInvoiceClaim As String = "isPreInvoiceClaim"
        Public Const COL_Payment_Amount_Total As String = "payment_amount_Total"

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
