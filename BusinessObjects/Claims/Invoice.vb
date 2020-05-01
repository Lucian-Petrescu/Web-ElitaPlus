'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (2/21/2013)  ********************
Imports System.Collections.Generic

Public NotInheritable Class Invoice
    Inherits BusinessObjectBase
    Implements IAttributable

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
            Dim dal As New InvoiceDAL
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
            Dim dal As New InvoiceDAL
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

#Region "Private Members"
    'Initialization code for new objects
    Private Sub Initialize()
        Me.InvoiceDate = Date.Today
        Me.InvoiceAmount = 0D
        Me.InvoiceStatusId = LookupListNew.GetIdFromCode(LookupListNew.LK_INVOICE_STATUS, Codes.INVOICE_STATUS__NEW)
        Me.IsComplete = False
    End Sub

    ''' <summary>
    ''' Validates if Due Date is Valid
    ''' </summary>
    ''' <returns>True if Due Date is valid, False otherwise.</returns>
    ''' <remarks>Check if Due Date is supplied, if not supplied then it's valid. If Due date is supplied and Invoice date is not OR Due date &lt; Invoice Date then it's not valid.</remarks>
    Private Function IsDueDateValid()
        If (Me.DueDate Is Nothing) Then
            Return True
        Else
            If (Me.InvoiceDate Is Nothing) Then
                Return False
            Else
                Return Me.DueDate.Value >= Me.InvoiceDate.Value
            End If
        End If
    End Function

    Private Function getDecimalValue(ByVal decimalObj As DecimalType, Optional ByVal decimalDigit As Integer = 2) As Decimal
        If decimalObj Is Nothing Then
            Return 0D
        Else
            Return Math.Round(decimalObj.Value, decimalDigit, MidpointRounding.AwayFromZero)
        End If
    End Function
#End Region

    Private _syncRoot As New Object

#Region "Constants"
    Private Const COL_TAX1_COMPUTE_METHOD As String = "tax1_compute_method"
    Private Const COL_TAX2_COMPUTE_METHOD As String = "tax2_compute_method"
    Private Const COL_TAX3_COMPUTE_METHOD As String = "tax3_compute_method"
    Private Const COL_TAX4_COMPUTE_METHOD As String = "tax4_compute_method"
    Private Const COL_TAX5_COMPUTE_METHOD As String = "tax5_compute_method"
    Private Const COL_TAX6_COMPUTE_METHOD As String = "tax6_compute_method"
    Private COMPUTE_TYPE_MANUALLY As String = "I"
    Private Const MAX_MANUALLY_ENTERED_TAXES As Integer = 2
#End Region

#Region "Derived Propertes"
    Public ReadOnly Property InvoiceStatusCode As String
        Get
            Return LookupListNew.GetCodeFromId(LookupListNew.LK_INVOICE_STATUS, Me.InvoiceStatusId)
        End Get
    End Property

    Public ReadOnly Property [ReadOnly] As Boolean
        Get
            If (Me.CanAddAuthorization) Then Return False
            For Each oClaimAuthorization As ClaimAuthorization In Me.ClaimAuthorizations
                If (oClaimAuthorization.ClaimAuthStatus = ClaimAuthorizationStatus.Fulfilled OrElse oClaimAuthorization.ClaimAuthStatus = ClaimAuthorizationStatus.Authorized) Then
                    Return False
                End If
            Next
            Return True
        End Get
    End Property

    ''' <summary>
    ''' Gets Difference between Invoice Amount and Sum of All Line Item Amounts excluding Lines Items added as part of Auto-Balance Process
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property DifferenceAmount As DecimalType
        Get
            Dim sumAmount As Decimal = 0D
            For Each item As InvoiceItem In Me.InvoiceItemChildren
                If (item.AdjustmentReasonId.Equals(Guid.Empty)) Then
                    sumAmount += item.Amount.Value
                End If
            Next
            Return Me.InvoiceAmount.Value - sumAmount - PerceptionIVA.Value - PerceptionIIBB.Value
        End Get
    End Property

    ''' <summary>
    ''' Gets Sum of All Line Item Amounts
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LineItemTotalAmount As DecimalType
        Get
            Dim sumAmount As Decimal = 0D
            For Each item As InvoiceItem In Me.InvoiceItemChildren
                sumAmount += item.Amount.Value
            Next
            Return sumAmount
        End Get
    End Property

    ''' <summary>
    ''' Checks whether an Authorization can be added to Invoice or not.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property CanAddAuthorization
        Get
            If (Me.IsNew) Then Return True
            If (Me.IsComplete) Then Return False
            Return True
        End Get
    End Property

    ''' <summary>
    ''' Invoice is Paid in full when 
    ''' 1. No More Authorizations Items can be added to Invoice
    ''' 2. All the Authorizations in Invoice have status as Paid
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property IsPaidFull
        Get
            If (Me.CanAddAuthorization) Then Return False
            For Each oClaimAuth As ClaimAuthorization In Me.ClaimAuthorizations
                If (oClaimAuth.ClaimAuthStatus <> ClaimAuthorizationStatus.Paid) Then
                    Return False
                End If
            Next
            Return True
        End Get
    End Property

    Private _isPerceptionTaxDefined As Nullable(Of Boolean)
    Public ReadOnly Property IsPerceptionTaxDefined As Boolean
        Get
            If (Not _isPerceptionTaxDefined.HasValue) Then
                SyncLock _syncRoot
                    If (Not _isPerceptionTaxDefined.HasValue) Then
                        If (Me.ServiceCenter Is Nothing) Then
                            _isPerceptionTaxDefined = False
                        Else
                            ' Read Taxes for Tax Type = 4 (Invoice Tax)
                            Dim taxTypeId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_TAX_TYPES, "4")
                            Dim ds As DataSet
                            ds = InvoiceTrans.CheckInvoiceTaxTypeByCountry(Me.ServiceCenter.CountryId, taxTypeId, Guid.Empty)

                            If ds.Tables(0).Rows.Count > 0 Then
                                Dim taxColumnCount As Integer
                                For Each dr As DataRow In ds.Tables(0).Rows

                                    taxColumnCount = CInt(IIf(dr(COL_TAX1_COMPUTE_METHOD).ToString = COMPUTE_TYPE_MANUALLY, taxColumnCount + 1, taxColumnCount + 0))
                                    taxColumnCount = CInt(IIf(dr(COL_TAX2_COMPUTE_METHOD).ToString = COMPUTE_TYPE_MANUALLY, taxColumnCount + 1, taxColumnCount + 0))
                                    taxColumnCount = CInt(IIf(dr(COL_TAX3_COMPUTE_METHOD).ToString = COMPUTE_TYPE_MANUALLY, taxColumnCount + 1, taxColumnCount + 0))
                                    taxColumnCount = CInt(IIf(dr(COL_TAX4_COMPUTE_METHOD).ToString = COMPUTE_TYPE_MANUALLY, taxColumnCount + 1, taxColumnCount + 0))
                                    taxColumnCount = CInt(IIf(dr(COL_TAX5_COMPUTE_METHOD).ToString = COMPUTE_TYPE_MANUALLY, taxColumnCount + 1, taxColumnCount + 0))
                                    taxColumnCount = CInt(IIf(dr(COL_TAX6_COMPUTE_METHOD).ToString = COMPUTE_TYPE_MANUALLY, taxColumnCount + 1, taxColumnCount + 0))

                                    If taxColumnCount <> MAX_MANUALLY_ENTERED_TAXES Then
                                        Throw New ElitaPlusException(TranslationBase.TranslateLabelOrMessage(Assurant.ElitaPlus.Common.ErrorCodes.ERR_TWO_TAXES_FOR_INVOICE_TAX_TYPE), Common.ErrorCodes.UNEXPECTED_ERROR)
                                    End If
                                    _isPerceptionTaxDefined = True

                                Next
                            Else
                                _isPerceptionTaxDefined = False
                            End If
                        End If
                    End If
                End SyncLock
            End If
            Return _isPerceptionTaxDefined.Value
        End Get
    End Property

    Public ReadOnly Property IsAnyClaimAuthorizationPaid As Boolean
        Get
            For Each auth As ClaimAuthorization In Me.ClaimAuthorizations
                If auth.ClaimAuthStatus = ClaimAuthorizationStatus.Paid Then
                    Return True
                End If
            Next
            Return False
        End Get
    End Property
#End Region

#Region "Properties"

    Private _AttributeValueList As AttributeValueList(Of IAttributable)

    Public ReadOnly Property AttributeValues As AttributeValueList(Of IAttributable) Implements IAttributable.AttributeValues
        Get
            If (_AttributeValueList Is Nothing) Then
                _AttributeValueList = New AttributeValueList(Of IAttributable)(Me.Dataset, Me)
            End If
            Return _AttributeValueList
        End Get
    End Property

    'Key Property
    <ValidateAtLeastOneItem(""), ValidateAuthorizationRepairDate(""), ValidateDuplicateServiceClassType("")> _
    Public ReadOnly Property Id() As Guid Implements IAttributable.Id
        Get
            If Row(InvoiceDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(InvoiceDAL.COL_NAME_INVOICE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property ServiceCenterId() As Guid
        Get
            CheckDeleted()
            If Row(InvoiceDAL.COL_NAME_SERVICE_CENTER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(InvoiceDAL.COL_NAME_SERVICE_CENTER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(InvoiceDAL.COL_NAME_SERVICE_CENTER_ID, Value)
            Me.ServiceCenter = Nothing
            _isPerceptionTaxDefined = Nothing
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=120), ValidateUniqueInvoiceNumber("")> _
    Public Property InvoiceNumber() As String
        Get
            CheckDeleted()
            If Row(InvoiceDAL.COL_NAME_INVOICE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(InvoiceDAL.COL_NAME_INVOICE_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(InvoiceDAL.COL_NAME_INVOICE_NUMBER, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidateInvoiceDate("")> _
    Public Property InvoiceDate() As DateType
        Get
            CheckDeleted()
            If Row(InvoiceDAL.COL_NAME_INVOICE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(InvoiceDAL.COL_NAME_INVOICE_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(InvoiceDAL.COL_NAME_INVOICE_DATE, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidNumericRange("", Message:=INVOICE_FORM002, Min:=0D, MinExclusive:=True)> _
    Public Property InvoiceAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(InvoiceDAL.COL_NAME_INVOICE_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(InvoiceDAL.COL_NAME_INVOICE_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(InvoiceDAL.COL_NAME_INVOICE_AMOUNT, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property InvoiceStatusId() As Guid
        Get
            CheckDeleted()
            If Row(InvoiceDAL.COL_NAME_INVOICE_STATUS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(InvoiceDAL.COL_NAME_INVOICE_STATUS_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(InvoiceDAL.COL_NAME_INVOICE_STATUS_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=200)> _
    Public Property Source() As String
        Get
            CheckDeleted()
            If Row(InvoiceDAL.COL_NAME_SOURCE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(InvoiceDAL.COL_NAME_SOURCE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(InvoiceDAL.COL_NAME_SOURCE, Value)
        End Set
    End Property

    <ValidateDueDateAfterInvoiceDate("")> _
    Public Property DueDate() As DateType
        Get
            CheckDeleted()
            If Row(InvoiceDAL.COL_NAME_DUE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(InvoiceDAL.COL_NAME_DUE_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(InvoiceDAL.COL_NAME_DUE_DATE, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property IsCompleteId() As Guid
        Get
            CheckDeleted()
            If Row(InvoiceDAL.COL_NAME_IS_COMPLETE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(InvoiceDAL.COL_NAME_IS_COMPLETE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(InvoiceDAL.COL_NAME_IS_COMPLETE_ID, Value)
        End Set
    End Property
#End Region

#Region "Attribute Properties"
    Public ReadOnly Property TableName As String Implements IAttributable.TableName
        Get
            Return InvoiceDAL.TABLE_NAME
        End Get
    End Property

    <ValidStringLength("", Max:=255)> _
    Public Property BatchNumber As String
        Get
            Return Me.AttributeValues.Value(Codes.ATTRIBUTE__BATCH_NUMBER)
        End Get
        Set(ByVal value As String)
            If (value Is Nothing OrElse value.Trim.Length = 0) Then
                Me.AttributeValues.Value(Codes.ATTRIBUTE__BATCH_NUMBER) = Nothing
            Else
                Me.AttributeValues.Value(Codes.ATTRIBUTE__BATCH_NUMBER) = value
            End If

        End Set
    End Property

    Public Property PerceptionIVA As DecimalType
        Get
            Return Me.AttributeValues.Value(Codes.ATTRIBUTE__PERCEPTION_IVA).ToDecimal(0D)
        End Get
        Set(ByVal value As DecimalType)
            If (value Is Nothing) Then
                Me.AttributeValues.Value(Codes.ATTRIBUTE__PERCEPTION_IVA) = Nothing
            Else
                Me.AttributeValues.Value(Codes.ATTRIBUTE__PERCEPTION_IVA) = value
            End If
        End Set
    End Property

    Public Property PerceptionIIBB As DecimalType
        Get
            Return Me.AttributeValues.Value(Codes.ATTRIBUTE__PERCEPTION_IIBB).ToDecimal(0D)
        End Get
        Set(ByVal value As DecimalType)
            If (value Is Nothing) Then
                Me.AttributeValues.Value(Codes.ATTRIBUTE__PERCEPTION_IIBB) = Nothing
            Else
                Me.AttributeValues.Value(Codes.ATTRIBUTE__PERCEPTION_IIBB) = value
            End If
        End Set
    End Property

    Public Property PerceptionIIBBRegion As Guid
        Get
            Return Me.AttributeValues.Value(Codes.ATTRIBUTE__PERCEPTION_IIBB_REGION_ID).ToGuid(Guid.Empty)
        End Get
        Set(ByVal value As Guid)
            If (value.Equals(Guid.Empty)) Then
                Me.AttributeValues.Value(Codes.ATTRIBUTE__PERCEPTION_IIBB_REGION_ID) = Nothing
            Else
                Me.AttributeValues.Value(Codes.ATTRIBUTE__PERCEPTION_IIBB_REGION_ID) = GuidControl.GuidToHexString(value)
            End If

        End Set
    End Property
#End Region

#Region "Derived Properties"
    Public Property IsComplete As Boolean
        Get
            Return Me.IsCompleteId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y))
        End Get
        Set(ByVal value As Boolean)
            If value Then
                Me.IsCompleteId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_Y)
            Else
                Me.IsCompleteId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_N)
            End If
        End Set
    End Property
#End Region

#Region "Constants"
    Private Const INVOICE_FORM001 As String = "INVOICE_FORM001" 'Due Date should be after Invoice Date
    Private Const INVOICE_FORM002 As String = "INVOICE_FORM002" 'Invoice Amount should be greater than 0
    Private Const INVOICE_FORM003 As String = "INVOICE_FORM003" 'Invoice should have at least one Line Item
    Private Const INVOICE_FORM004 As String = "INVOICE_FORM004" 'Repair Date is mandatory for all Authorizations selected in Invoice
#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            If (Not Me.IsDeleted) Then
                ' Clear Invoice Tax Attributes if Invoice Taxes are not Defined
                If (Not Me.IsPerceptionTaxDefined) Then
                    Me.PerceptionIIBB = Nothing
                    Me.PerceptionIVA = Nothing
                    Me.PerceptionIIBBRegion = Nothing
                    Me.BatchNumber = Nothing
                End If
                ' If Difference Amount is not Zero, Invoice is Incomplete. This is only applicable when Source is blank
                If ((Me.Source Is Nothing) OrElse (Me.Source.Trim() = String.Empty)) Then
                    Me.IsComplete = (Me.DifferenceAmount.Value = 0)
                End If
                ' Re-Calculate the Status of Invoice
                If (Me.InvoiceAmount.Value = Me.LineItemTotalAmount.Value + Me.PerceptionIVA.Value + Me.PerceptionIIBB.Value) Then
                    Me.InvoiceStatusId = LookupListNew.GetIdFromCode(LookupListNew.LK_INVOICE_STATUS, Codes.INVOICE_STATUS__BALANCED)
                ElseIf (Me.InvoiceAmount.Value < Me.LineItemTotalAmount.Value + Me.PerceptionIVA.Value + Me.PerceptionIIBB.Value) Then
                    Me.InvoiceStatusId = LookupListNew.GetIdFromCode(LookupListNew.LK_INVOICE_STATUS, Codes.INVOICE_STATUS__OVER)
                Else
                    Me.InvoiceStatusId = LookupListNew.GetIdFromCode(LookupListNew.LK_INVOICE_STATUS, Codes.INVOICE_STATUS__UNDER)
                End If

                ' Explicitly Save all Claim Authorizations so that Status will be Updated Automatically
                For Each oClaimAuthorization As ClaimAuthorization In Me.ClaimAuthorizations
                    oClaimAuthorization.Save()
                    ' This call is important, this recalculates Repair Date on Claim and Marks Claim Dirty
                    oClaimAuthorization.Claim.Save()
                Next
            End If
            If IsNew Then
                MyBase.Save()
            End If
            Me.SetModifiedAuditInfo()
            If Me._isDSCreator AndAlso Me.IsFamilyDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New InvoiceDAL
                dal.UpdateFamily(Me.Dataset)
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

    Public Sub RemoveAuthorization(ByVal claimAuthorizationId As Guid)
        Dim oClaimAuthorization As ClaimAuthorization
        oClaimAuthorization = Me.ClaimAuthorizations.Where(Function(item) item.Id = claimAuthorizationId).First()
        For Each invoiceItem As InvoiceItem In ( _
            From oInvoiceItem As InvoiceItem In Me.InvoiceItemChildren _
            Where oInvoiceItem.ClaimAuthorizationId = claimAuthorizationId _
            Select oInvoiceItem)
            invoiceItem.Delete()
        Next
        oClaimAuthorization.RejectInvoiceChanges()
    End Sub

    Public Sub AddAuthorization(ByVal claimAuthorizationId As Guid)
        Dim claimAuth As New ClaimAuthorization(claimAuthorizationId, Me.Dataset)
        Dim lineItemNumber As Integer = Me.InvoiceItemChildren.Count
        For Each detail As ClaimAuthItem In claimAuth.ClaimAuthorizationItemChildren
            Dim invItem As InvoiceItem = Me.GetNewInvoiceItemChild()
            invItem.ClaimAuthorizationId = detail.ClaimAuthorizationId
            lineItemNumber = lineItemNumber + 1
            invItem.LineItemNumber = lineItemNumber
            invItem.VendorSku = detail.VendorSku
            invItem.VendorSkuDescription = detail.VendorSkuDescription
            invItem.ServiceClassId = detail.ServiceClassId
            invItem.ServiceTypeId = detail.ServiceTypeId
            invItem.ServiceLevelId = claimAuth.ServiceLevelId
            invItem.Amount = detail.Amount
        Next
    End Sub

    Public ReadOnly Property Delete As IBusinessCommand
        Get
            Return New DeleteInvoiceCommand(Me)
        End Get
    End Property

    Public ReadOnly Property Balance As IBusinessCommand
        Get
            Return New BalanceCommand(Me)
        End Get
    End Property

    Public ReadOnly Property UndoBalance As IBusinessCommand
        Get
            Return New UndoBalanceCommand(Me)
        End Get
    End Property

    Private Sub BalanceAuthorization(ByVal claimAuthorization As ClaimAuthorization)
        Dim deductibleServiceClassId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_SERVICE_CLASS, Codes.SERVICE_CLASS__DEDUCTIBLE)
        Dim deductibleServiceTypeId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_SERVICE_TYPE_NEW, Codes.SERVICE_TYPE__DEDUCTIBLE)
        Dim payDeductibleServiceTypeId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_SERVICE_TYPE_NEW, Codes.SERVICE_TYPE__PAY_DEDUCTIBLE)

        Dim miscellaneousServiceClassId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_SERVICE_CLASS, Codes.SERVICE_CLASS__MISCELLANEOUS)
        Dim aboveLiabilityLimitServiceTypeId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_SERVICE_TYPE_NEW, Codes.SERVICE_TYPE__ABOVE_LIABILITY_LIMIT)

        Dim invoiceReconciliationStatusReconsiled As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_INV_RECON_STAT, Codes.INVOICE_RECON_STATUS_RECONCILED)

        Dim autoAdjustmentReasonDeductible As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_ADJUSTMENT_REASON, Codes.ADJUSTMENT_REASON__AA_DEDUCTIBLE)
        Dim autoAdjustmentReasonPayDeductible As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_ADJUSTMENT_REASON, Codes.ADJUSTMENT_REASON__AA_PAY_DEDUCTIBLE)
        Dim autoAdjustmentReasonAutoAdjusted As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_ADJUSTMENT_REASON, Codes.ADJUSTMENT_REASON__AA_AUTO_ADJUSTED)
        Dim autoAdjustmentReasonInvoiceAmountGreaterThanAuthorizedAmount = LookupListNew.GetIdFromCode(LookupListNew.LK_ADJUSTMENT_REASON, Codes.ADJUSTMENT_REASON__INV_AMT_GT_AUTH_AMT)
        Dim autoAdjustmentReasonNotIncludedInAuthorization As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_ADJUSTMENT_REASON, Codes.ADJUSTMENT_REASON__NOT_INCLUDED_ON_AUTHORIZATION)
        Dim autoAdjustmentReasonAboveLiabilityLimit As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_ADJUSTMENT_REASON, Codes.ADJUSTMENT_REASON__ABOVE_LIABILITY_LIMIT)

        Dim oClaimAuthorization As ClaimAuthorization = claimAuthorization
        Dim oInvoiceReconciliation As InvoiceReconciliation
        Dim oDeductibleClaimAuthItem As ClaimAuthItem

        Dim oClaimAuthItem As ClaimAuthItem
        Dim oAdjustmentClaimAuthItem As ClaimAuthItem
        Dim oInvoiceItem As InvoiceItem
        Dim oAdjustmentInvoiceItem As InvoiceItem

        Dim count As Integer

        If (oClaimAuthorization.ClaimAuthStatus <> ClaimAuthorizationStatus.Fulfilled) Then
            Throw New BOInvalidOperationException(String.Format(TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_CLAIM_AUTH_EXPECTED_IN_STATUS), oClaimAuthorization.AuthorizationNumber, oClaimAuthorization.ClaimNumber, LookupListNew.GetDescriptionFromCode(LookupListNew.LK_CLAIM_AUTHORIZATION_STATUS, Codes.CLAIM_AUTHORIZATION_STATUS__FULFILLED)))
        End If

        If (oClaimAuthorization.ContainsDeductible) Then
            ' Step #1 - If Authorization has a “CONTAINS_DEDUCTIBLE” = “Yes” and there is no line item for deductible in Authorization with 
            '           amount less than 0 (Deductible are negative amounts) then add a line item in Claim Authorization for deductible with 
            '           Adjustment Reason – “Auto-Adjusted-Deductible”, Service Class = Deductible and Service Type = Deductible
            count = (From item As ClaimAuthItem In oClaimAuthorization.ClaimAuthorizationItemChildren Where _
                item.Amount.Value < 0 AndAlso item.ServiceClassId = deductibleServiceClassId AndAlso _
                item.ServiceTypeId = deductibleServiceTypeId _
                Select item).Count()
            If (count = 0) Then
                ' Add Line Item to Authorization for Deductible
                oDeductibleClaimAuthItem = oClaimAuthorization.GetNewAuthorizationItemChild()
                With oDeductibleClaimAuthItem
                    .Amount = getDecimalValue(oClaimAuthorization.Deductible) * -1D
                    .ServiceClassId = deductibleServiceClassId
                    .ServiceTypeId = deductibleServiceTypeId
                    .VendorSku = Codes.VENDOR_SKU_DEDUCTIBLE
                    .VendorSkuDescription = Codes.VENDOR_SKU_DESC_DEDUCTIBLE
                    .AdjustmentReasonId = autoAdjustmentReasonAutoAdjusted
                    .Save()
                End With
            ElseIf (count = 1) Then
                oDeductibleClaimAuthItem = (From item As ClaimAuthItem In oClaimAuthorization.ClaimAuthorizationItemChildren Where _
                    item.Amount.Value < 0 AndAlso item.ServiceClassId = deductibleServiceClassId AndAlso _
                    item.ServiceTypeId = deductibleServiceTypeId _
                    Select item).First()
            Else
                Throw New BOInvalidOperationException(String.Format(TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_MULTIPLE_DEDUCTIBLE_LINES_IN_CLAIM_AUTH), oClaimAuthorization.AuthorizationNumber, oClaimAuthorization.ClaimNumber))
            End If

            ' Step #2 - If the authorization line item detail contains a deductible line item (Service Class = Deductible and Service Type = Deductible) 
            '           and invoice line item detail does not contain a deductible line item, then the system shall add a deductible line item type to 
            '           the invoice line item detail with the amount equal to “0.00” and the reconciled amount equal to the authorization line item _
            '           deductible amount with a adjustment reason of “Auto-adjusted-Deductible”
            count = (From item As InvoiceItem In Me.InvoiceItemChildren Where item.ClaimAuthorizationId = claimAuthorization.Id AndAlso _
                item.ServiceClassId = deductibleServiceClassId AndAlso item.ServiceTypeId = deductibleServiceTypeId).Count()
            If (count = 0) Then
                ' Add Line Item to Invoice
                oInvoiceItem = Me.GetNewInvoiceItemChild()
                With oInvoiceItem
                    .AdjustmentReasonId = autoAdjustmentReasonAutoAdjusted
                    .Amount = 0
                    .ClaimAuthorizationId = oClaimAuthorization.Id
                    .LineItemNumber = Me.InvoiceItemChildren.Count + 1
                    .ServiceClassId = deductibleServiceClassId
                    .ServiceTypeId = deductibleServiceTypeId
                    .VendorSku = Codes.VENDOR_SKU_DEDUCTIBLE
                    .VendorSkuDescription = Codes.VENDOR_SKU_DESC_DEDUCTIBLE
                    .Save()
                End With
            ElseIf (count = 1) Then
                oInvoiceItem = (From item As InvoiceItem In Me.InvoiceItemChildren Where item.ClaimAuthorizationId = claimAuthorization.Id AndAlso _
                    item.ServiceClassId = deductibleServiceClassId AndAlso item.ServiceTypeId = deductibleServiceTypeId).First()
            Else
                Throw New BOInvalidOperationException(String.Format(TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_MULTIPLE_DEDUCTIBLE_LINES_IN_INVOICE), Me.InvoiceNumber, oClaimAuthorization.AuthorizationNumber, oClaimAuthorization.ClaimNumber))
            End If

            ' Step #3 - When Invoice Deductible >= Pay Deductible Amount
            '           o	Add Reverse Entry to Authorization for Pay Deductible with Amount same as Pay Deductible Amount
            '           o	If Invoice contains Pay Deductible Line Item then Add Reverse Entry to Invoice for Pay Deductible with Amount same as Invoice 
            '               Pay Deductible Amount
            '           o	Add Reconciliation Record with 0 amount
            '           When Invoice Deductible < Claim Auth Pay Deductible
            '           o	Add Reverse Entry to Authorization for Pay Deductible for amount = Invoice Deductible
            '           o	Add Entry to Invoice for Pay Deductible with Amount = Claim Auth Pay Deductible – Invoice Deductible – Invoice Pay Deductible
            '           o	Add Reconciliation Record for Pay Deductible with Amount = Claim Auth Pay Deductible – Invoice Deductible
            count = (From item As ClaimAuthItem In oClaimAuthorization.ClaimAuthorizationItemChildren Where _
                item.ServiceClassId = deductibleServiceClassId AndAlso _
                item.ServiceTypeId = payDeductibleServiceTypeId _
                Select item).Count()
            If (count > 1) Then
                Throw New BOInvalidOperationException(String.Format(TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_MULTIPLE_PAY_DEDUCTIBLE_LINES_IN_CLAIM_AUTH), oClaimAuthorization.AuthorizationNumber, oClaimAuthorization.ClaimNumber))
            ElseIf (count = 1) Then
                oClaimAuthItem = (From item As ClaimAuthItem In oClaimAuthorization.ClaimAuthorizationItemChildren Where _
                    item.ServiceClassId = deductibleServiceClassId AndAlso _
                    item.ServiceTypeId = payDeductibleServiceTypeId _
                    Select item).First()

                count = (From item As InvoiceItem In Me.InvoiceItemChildren Where item.ClaimAuthorizationId = claimAuthorization.Id AndAlso _
                    item.ServiceClassId = deductibleServiceClassId AndAlso item.ServiceTypeId = payDeductibleServiceTypeId).Count()
                Dim invoicePayDeductibleLineItem As InvoiceItem
                If (count = 0) Then
                    invoicePayDeductibleLineItem = Me.GetNewInvoiceItemChild()
                    With invoicePayDeductibleLineItem
                        .AdjustmentReasonId = autoAdjustmentReasonAutoAdjusted
                        .Amount = 0
                        .ClaimAuthorizationId = oClaimAuthorization.Id
                        .LineItemNumber = Me.InvoiceItemChildren.Count + 1
                        .ServiceClassId = deductibleServiceClassId
                        .ServiceTypeId = payDeductibleServiceTypeId
                        .VendorSku = Codes.VENDOR_SKU_PAY_DEDUCTIBLE
                        .VendorSkuDescription = Codes.VENDOR_SKU_DESC_PAY_DEDUCTIBLE
                        .Save()
                    End With
                ElseIf (count = 1) Then
                    invoicePayDeductibleLineItem = (From item As InvoiceItem In Me.InvoiceItemChildren Where item.ClaimAuthorizationId = claimAuthorization.Id AndAlso _
                        item.ServiceClassId = deductibleServiceClassId AndAlso item.ServiceTypeId = payDeductibleServiceTypeId).First()
                Else
                    Throw New BOInvalidOperationException(String.Format(TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_MULTIPLE_PAY_DEDUCTIBLE_LINES_IN_INVOICE), Me.InvoiceNumber, oClaimAuthorization.AuthorizationNumber, oClaimAuthorization.ClaimNumber))
                End If

                If (oInvoiceItem.Amount.Value * -1D >= oClaimAuthItem.Amount.Value) Then
                    ' Add Reverse Entry to Authorization for Pay Deductible with Amount same as Pay Deductible Amount 
                    With oClaimAuthorization.GetNewAuthorizationItemChild()
                        .Amount = getDecimalValue(oClaimAuthItem.Amount) * -1D
                        .ServiceClassId = oClaimAuthItem.ServiceClassId
                        .ServiceTypeId = oClaimAuthItem.ServiceTypeId
                        .VendorSku = Codes.VENDOR_SKU_PAY_DEDUCTIBLE
                        .VendorSkuDescription = Codes.VENDOR_SKU_DESC_PAY_DEDUCTIBLE
                        .AdjustmentReasonId = autoAdjustmentReasonPayDeductible
                        .Save()
                    End With

                    ' If Invoice contains Pay Deductible Line Item then Add Reverse Entry to Invoice for Pay Deductible with Amount same 
                    ' as Invoice Pay Deductible Amount
                    If (invoicePayDeductibleLineItem.Amount.Value > 0) Then
                        With Me.GetNewInvoiceItemChild()
                            .AdjustmentReasonId = autoAdjustmentReasonPayDeductible
                            .Amount = invoicePayDeductibleLineItem.Amount.Value * -1D
                            .ClaimAuthorizationId = oClaimAuthorization.Id
                            .LineItemNumber = Me.InvoiceItemChildren.Count + 1
                            .ServiceClassId = deductibleServiceClassId
                            .ServiceTypeId = payDeductibleServiceTypeId
                            .VendorSku = Codes.VENDOR_SKU_PAY_DEDUCTIBLE
                            .VendorSkuDescription = Codes.VENDOR_SKU_DESC_PAY_DEDUCTIBLE
                            .Save()
                        End With
                    End If

                    ' Add Reconciliation Record
                    oInvoiceReconciliation = New InvoiceReconciliation(Me.Dataset)
                    With oInvoiceReconciliation
                        .InvoiceItemId = invoicePayDeductibleLineItem.Id
                        .ClaimAuthItemId = oClaimAuthItem.Id
                        .ReconciledAmount = 0
                        .ReconciliationStatusId = invoiceReconciliationStatusReconsiled
                        .Save()
                    End With
                Else
                    ' Add Reverse Entry to Authorization for Pay Deductible for Amount = Invoice Deductible
                    If (oInvoiceItem.Amount.Value <> 0) Then
                        With oClaimAuthorization.GetNewAuthorizationItemChild()
                            .Amount = oInvoiceItem.Amount.Value ' Deductible Amount are already Negative
                            .ServiceClassId = deductibleServiceClassId
                            .ServiceTypeId = payDeductibleServiceTypeId
                            .VendorSku = Codes.VENDOR_SKU_PAY_DEDUCTIBLE
                            .VendorSkuDescription = Codes.VENDOR_SKU_DESC_PAY_DEDUCTIBLE
                            .AdjustmentReasonId = autoAdjustmentReasonPayDeductible
                            .Save()
                        End With
                    End If

                    ' Add Entry to Invoice for Pay Deductible with Amount = Claim Auth Pay Deductible – Invoice Deductible – Invoice Pay Deductible
                    If (oClaimAuthItem.Amount.Value + oInvoiceItem.Amount.Value - invoicePayDeductibleLineItem.Amount.Value <> 0) Then
                        With Me.GetNewInvoiceItemChild()
                            .AdjustmentReasonId = autoAdjustmentReasonPayDeductible
                            .Amount = oClaimAuthItem.Amount.Value + oInvoiceItem.Amount.Value - invoicePayDeductibleLineItem.Amount.Value
                            .ClaimAuthorizationId = oClaimAuthorization.Id
                            .LineItemNumber = Me.InvoiceItemChildren.Count + 1
                            .ServiceClassId = deductibleServiceClassId
                            .ServiceTypeId = payDeductibleServiceTypeId
                            .VendorSku = Codes.VENDOR_SKU_PAY_DEDUCTIBLE
                            .VendorSkuDescription = Codes.VENDOR_SKU_DESC_PAY_DEDUCTIBLE
                            .Save()
                        End With
                    End If

                    ' Add Reconciliation Record for Pay Deductible with Amount = Claim Auth Pay Deductible – Invoice Deductible
                    oInvoiceReconciliation = New InvoiceReconciliation(Me.Dataset)
                    With oInvoiceReconciliation
                        .InvoiceItemId = invoicePayDeductibleLineItem.Id
                        .ClaimAuthItemId = oClaimAuthItem.Id
                        .ReconciledAmount = oClaimAuthItem.Amount.Value + oInvoiceItem.Amount.Value
                        .ReconciliationStatusId = invoiceReconciliationStatusReconsiled
                        .Save()
                    End With
                End If
            End If
        End If

        For Each oInvoiceItem In (From item In Me.InvoiceItemChildren Where item.ClaimAuthorizationId = claimAuthorization.Id Select item)
            ' Try to find Line Item from Authorization with same Service Class and Type
            oClaimAuthItem = (From item As ClaimAuthItem In oClaimAuthorization.ClaimAuthorizationItemChildren _
                             Where item.ServiceClassId = oInvoiceItem.ServiceClassId AndAlso item.ServiceTypeId = oInvoiceItem.ServiceTypeId _
                             Select item).FirstOrDefault()

            If (Not ((oInvoiceItem.ServiceClassId = deductibleServiceClassId AndAlso oInvoiceItem.ServiceTypeId = payDeductibleServiceTypeId) _
                     OrElse (Not oClaimAuthorization.ContainsDeductible AndAlso oInvoiceItem.ServiceClassId = deductibleServiceClassId AndAlso oInvoiceItem.ServiceTypeId = deductibleServiceTypeId))) Then
                If (Not oClaimAuthItem Is Nothing) Then
                    ' Step 4 - Compare the amount from each line item with service class/type where service class not equal to Deductible (Example: Service Warranty, 
                    '          Home Price, Carry-in Price, Send-in Price, Pick-up Price, Cleaning Price, Estimate Price etc) from the authorization to the invoice. 
                    '          •    If the invoice amount is less than the authorization amount: The system shall enter the difference in amount as a negative adjustment 
                    '               to the authorization line item adjustment amount with an adjustment reason of “Auto-adjusted” and invoice amount as reconciled amount 
                    '               in invoice reconciliation.
                    '          •    If the invoice amount is greater than the authorization amount: The system shall enter the difference in amount as a negative 
                    '               adjustment to the invoice line item adjustment amount with an adjustment reason of “Invoice amount is greater than the authorization 
                    '               amount.” and authorization amount as reconciled amount in invoice reconciliation.
                    ' Step 5 - Compare the amount from each line item deductible type (Service Class = Deductible, Service Type = Deductible) from the authorization to 
                    '          the invoice.  
                    '          •    If the invoice amount is greater than the authorization amount: The system shall enter the difference in amount, as an adjustment 
                    '               to the invoice line item adjustment amount with an adjustment reason of “Invoice amount is greater than the authorization amount.” 
                    '               (Example: If Authorization is -$50 and the Invoice are -$35 then the system will enter the difference into the invoice line item 
                    '               adjustment amount of -$15) and authorized amount as reconciled amount.
                    '          •	If the invoice amount is less than the authorization amount: The system shall add a new authorization line item type with a Service 
                    '               Class = Deductible and Service Type = Overage of Deductible Charged and adjustment reason of “Auto-adjusted-Deductible” and amount 
                    '               equal to (Invoice Amount – Authorization Amount). (Example: If Authorization is -$35 and the Invoice are -$50 then the system will 
                    '               enter the difference into the authorization line item type “DO” with an adjustment amount of -$15).
                    If (oInvoiceItem.Amount.Value < oClaimAuthItem.Amount.Value) Then
                        ' Add Negative Adjustment to Authorization
                        oAdjustmentClaimAuthItem = oClaimAuthorization.GetNewAuthorizationItemChild()
                        With oAdjustmentClaimAuthItem
                            .Amount = oInvoiceItem.Amount.Value - oClaimAuthItem.Amount.Value
                            .ServiceClassId = oClaimAuthItem.ServiceClassId
                            .ServiceTypeId = oClaimAuthItem.ServiceTypeId
                            .VendorSku = oClaimAuthItem.VendorSku
                            .VendorSkuDescription = oClaimAuthItem.VendorSkuDescription
                            If (.ServiceClassId = deductibleServiceClassId) Then
                                .AdjustmentReasonId = autoAdjustmentReasonAutoAdjusted
                            Else
                                .AdjustmentReasonId = autoAdjustmentReasonDeductible
                            End If
                            .Save()
                        End With
                    ElseIf (oInvoiceItem.Amount.Value > oClaimAuthItem.Amount.Value) Then
                        ' Add Negative Adjustment to Invoice
                        oAdjustmentInvoiceItem = Me.GetNewInvoiceItemChild()
                        With oAdjustmentInvoiceItem
                            .AdjustmentReasonId = autoAdjustmentReasonInvoiceAmountGreaterThanAuthorizedAmount
                            .Amount = oClaimAuthItem.Amount.Value - oInvoiceItem.Amount.Value
                            .ClaimAuthorizationId = oClaimAuthorization.Id
                            .LineItemNumber = Me.InvoiceItemChildren.Count + 1
                            .ServiceClassId = oInvoiceItem.ServiceClassId
                            .ServiceTypeId = oInvoiceItem.ServiceTypeId
                            .VendorSku = oInvoiceItem.VendorSku
                            .VendorSkuDescription = oInvoiceItem.VendorSkuDescription
                            .Save()
                        End With
                    End If
                    ' Add Reconciliation Record
                    oInvoiceReconciliation = New InvoiceReconciliation(Me.Dataset)
                    With oInvoiceReconciliation
                        .InvoiceItemId = oInvoiceItem.Id
                        .ClaimAuthItemId = oClaimAuthItem.Id
                        .ReconciledAmount = Math.Min(oInvoiceItem.Amount.Value, oClaimAuthItem.Amount.Value)
                        .ReconciliationStatusId = invoiceReconciliationStatusReconsiled
                        .Save()
                    End With
                    ' End If
                Else
                    ' Step 6 - When the invoice has a line item type that is not included on the authorization the system shall enter a negative adjustment equal to the 
                    '          charge for that invoice line item type in the invoice line item adjustment amount field with the reason of “Not Included on Authorization” 
                    '          in the invoice line item type adjustment reason.
                    ' Add Negative Adjustment to Invoice
                    oAdjustmentInvoiceItem = Me.GetNewInvoiceItemChild()
                    With oAdjustmentInvoiceItem
                        .AdjustmentReasonId = autoAdjustmentReasonNotIncludedInAuthorization
                        .Amount = oInvoiceItem.Amount.Value * -1
                        .ClaimAuthorizationId = oClaimAuthorization.Id
                        .LineItemNumber = Me.InvoiceItemChildren.Count + 1
                        .ServiceClassId = oInvoiceItem.ServiceClassId
                        .ServiceTypeId = oInvoiceItem.ServiceTypeId
                        .VendorSku = oInvoiceItem.VendorSku
                        .VendorSkuDescription = oInvoiceItem.VendorSkuDescription
                        .Save()
                    End With
                End If
            End If
        Next

        ' Step 9 - If Sum of all Claim Authorization Line Items except Pay Deductible Line Item is greater than Liability Limit then Add Adjustment Record to 
        '          Invoice, Claim Authorization and Invoice Reconciliation with following details
        '          a. Vendor SKU – ABOVE _LIABILITY_LIMIT
        '          b. Vendor SKU Description - “Above Liability Limit” 
        '          c. Service Class – Miscellaneous (“MISC”) 
        '          d. Service Type – Above Liability Limit (“ABOVE _LIABILITY_LIMIT”)
        '          e. Amount – (Sum of all Claim Authorization Line Items except Pay Deductible Line Item) – Liability Limit
        '          f. Adjustment Reason – “ABOVE_LIABILITY_LIMIT”
        Dim claimAuthorizationTotal As Decimal = 0D
        Dim LiabilityLimit As Decimal = oClaimAuthorization.Claim.LiabilityLimit.Value
        If (LiabilityLimit <> 0 Or ((CDec(oClaimAuthorization.Claim.Certificate.ProductLiabilityLimit.ToString) > 0 Or
                                     CDec(oClaimAuthorization.Claim.CertificateItemCoverage.CoverageLiabilityLimit.ToString) > 0) And
                                     LiabilityLimit = 0)) Then
            For Each oClaimAuthItem In oClaimAuthorization.ClaimAuthorizationItemChildren.Where(Function(item) item.ServiceClassId <> deductibleServiceClassId)
                claimAuthorizationTotal += oClaimAuthItem.Amount.Value
            Next
            If (claimAuthorizationTotal > LiabilityLimit) Then
                oAdjustmentInvoiceItem = Me.GetNewInvoiceItemChild()
                With oAdjustmentInvoiceItem
                    .AdjustmentReasonId = autoAdjustmentReasonAboveLiabilityLimit
                    .Amount = LiabilityLimit - claimAuthorizationTotal
                    .ClaimAuthorizationId = oClaimAuthorization.Id
                    .LineItemNumber = Me.InvoiceItemChildren.Count + 1
                    .ServiceClassId = miscellaneousServiceClassId
                    .ServiceTypeId = aboveLiabilityLimitServiceTypeId
                    .VendorSku = Codes.VENDOR_SKU_ABOVE_LIABILITY_LIMIT
                    .VendorSkuDescription = Codes.VENDOR_SKU_DESC_ABOVE_LIABILITY_LIMIT
                    .Save()
                End With
                oAdjustmentClaimAuthItem = oClaimAuthorization.GetNewAuthorizationItemChild()
                With oAdjustmentClaimAuthItem
                    .Amount = LiabilityLimit - claimAuthorizationTotal
                    .AdjustmentReasonId = autoAdjustmentReasonAboveLiabilityLimit
                    .ServiceClassId = miscellaneousServiceClassId
                    .ServiceTypeId = aboveLiabilityLimitServiceTypeId
                    .VendorSku = Codes.VENDOR_SKU_ABOVE_LIABILITY_LIMIT
                    .VendorSkuDescription = Codes.VENDOR_SKU_DESC_ABOVE_LIABILITY_LIMIT
                    .Save()
                End With
                oInvoiceReconciliation = New InvoiceReconciliation(Me.Dataset)
                With oInvoiceReconciliation
                    .InvoiceItemId = oAdjustmentInvoiceItem.Id
                    .ClaimAuthItemId = oAdjustmentClaimAuthItem.Id
                    .ReconciledAmount = LiabilityLimit - claimAuthorizationTotal
                    .ReconciliationStatusId = invoiceReconciliationStatusReconsiled
                    .Save()
                End With
            End If
        End If

        ' Check if Invoice is Balanced
        Dim invoiceAuthorizationTotal As Decimal = 0D
        claimAuthorizationTotal = 0D

        For Each oInvoiceItem In Me.InvoiceItemChildren
            If (oInvoiceItem.ClaimAuthorizationId = claimAuthorization.Id) Then
                invoiceAuthorizationTotal += oInvoiceItem.Amount.Value
            End If
        Next

        For Each oClaimAuthItem In oClaimAuthorization.ClaimAuthorizationItemChildren
            claimAuthorizationTotal += oClaimAuthItem.Amount.Value
        Next

        If (invoiceAuthorizationTotal = claimAuthorizationTotal) Then
            oClaimAuthorization.ClaimAuthStatus = ClaimAuthorizationStatus.Reconsiled
        Else
            For iCnt As Integer = Me.Dataset.Tables(InvoiceReconciliationDAL.TABLE_NAME).Rows().Count - 1 To 0 Step -1
                oInvoiceReconciliation = New InvoiceReconciliation(Me.Dataset.Tables(InvoiceReconciliationDAL.TABLE_NAME).Rows(iCnt))
                If (oInvoiceReconciliation.ClaimAuthorizationItem.ClaimAuthorizationId.Equals(claimAuthorization.Id)) Then
                    oInvoiceReconciliation.Delete()
                End If
            Next

            For Each oInvoiceItem In Me.InvoiceItemChildren
                If (Not oInvoiceItem.AdjustmentReasonId.Equals(Guid.Empty)) Then
                    oInvoiceItem.Delete()
                End If
            Next

            For Each oClaimAuthItem In oClaimAuthorization.ClaimAuthorizationItemChildren
                If (Not oClaimAuthItem.AdjustmentReasonId.Equals(Guid.Empty)) Then
                    oClaimAuthItem.Delete()
                End If
            Next
        End If
    End Sub

    Private Sub BalanceInvoice()
        Dim oClaimAuthorization As ClaimAuthorization

        For Each claimAuthId As Guid In (From item As InvoiceItem In Me.InvoiceItemChildren Select item.ClaimAuthorizationId).Distinct()
            oClaimAuthorization = New ClaimAuthorization(claimAuthId, Me.Dataset)
            ' Process Only Authorizations which are Fulfilled
            If (oClaimAuthorization.ClaimAuthStatus = ClaimAuthorizationStatus.Fulfilled) Then
                BalanceAuthorization(oClaimAuthorization)
            End If
        Next
        Me.Save()
    End Sub

    Private Shadows Sub DeleteInvoice()
        MyBase.Delete()
        Me.Save()
    End Sub

    Private Sub UndoBalanceAuthorization(ByVal claimAuthorization As ClaimAuthorization)
        For Each oInvoiceItem As InvoiceItem In Me.InvoiceItemChildren
            If (oInvoiceItem.ClaimAuthorizationId = claimAuthorization.Id) Then
                If (oInvoiceItem.InvoiceReconciliationId <> Guid.Empty) Then
                    oInvoiceItem.InvoiceReconciliation.Delete()
                End If
                If (oInvoiceItem.AdjustmentReasonId <> Guid.Empty) Then
                    oInvoiceItem.Delete()
                End If
            End If
        Next

        For Each oClaimAuthItem As ClaimAuthItem In claimAuthorization.ClaimAuthorizationItemChildren
            If (oClaimAuthItem.AdjustmentReasonId <> Guid.Empty) Then
                oClaimAuthItem.Delete()
            End If
        Next

        claimAuthorization.ClaimAuthStatus = ClaimAuthorizationStatus.Fulfilled
    End Sub

    Private Sub UndoBalanceInvoice()
        Dim oInvoiceReconciliation As New InvoiceReconciliationDAL
        Dim oClaimAuthorization As ClaimAuthorization
        Dim claimAuthorizationList As List(Of Guid)
        claimAuthorizationList = (From item As InvoiceItem In Me.InvoiceItemChildren Select item.ClaimAuthorizationId).Distinct().ToList()
        For Each claimAuthId As Guid In claimAuthorizationList
            oClaimAuthorization = New ClaimAuthorization(claimAuthId, Me.Dataset)
            ' Process Only Authorizations which are Reconsiled
            If (oClaimAuthorization.ClaimAuthStatus = ClaimAuthorizationStatus.Reconsiled) Then
                If (Not oClaimAuthorization.IsAddedToInvoiceGroup) Then
                    UndoBalanceAuthorization(oClaimAuthorization)
                End If
            End If
        Next
        Me.Save()
    End Sub

    Public Class DeleteInvoiceCommand
        Inherits BusinessCommandBase(Of Invoice)
        Implements IBusinessCommand

        Friend Sub New(ByVal invoice As Invoice)
            MyBase.New(invoice)
        End Sub

        ''' <summary>
        ''' Invoice can only be Deleted when All Authorizations in Invoice are in Fulfilled State and Invoice is not New
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property CanExecute As Boolean Implements IBusinessCommand.CanExecute
            Get
                If (Me.BusinessObject.InvoiceStatusCode = Codes.INVOICE_STATUS__NEW) Then Return False
                For Each oClaimAuthorization As ClaimAuthorization In Me.BusinessObject.ClaimAuthorizations
                    If (oClaimAuthorization.ClaimAuthStatus <> ClaimAuthorizationStatus.Fulfilled) Then
                        Return False
                    End If
                Next
                Return True
            End Get
        End Property

        Public Sub Execute() Implements IBusinessCommand.Execute
            MyBase.Execute()
            MyBase.BusinessObject.DeleteInvoice()
        End Sub
    End Class

    Public Class BalanceCommand
        Inherits BusinessCommandBase(Of Invoice)
        Implements IBusinessCommand

        Friend Sub New(ByVal invoice As Invoice)
            MyBase.New(invoice)
        End Sub

        ''' <summary>
        ''' Invoice can be Balanced when Status is NOT New and at least one Authorization exists with Status Fulfilled
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property CanExecute As Boolean Implements IBusinessCommand.CanExecute
            Get
                If (Me.BusinessObject.InvoiceStatusCode = Codes.INVOICE_STATUS__NEW) Then Return False
                For Each oClaimAuthorization As ClaimAuthorization In Me.BusinessObject.ClaimAuthorizations
                    If (oClaimAuthorization.ClaimAuthStatus = ClaimAuthorizationStatus.Fulfilled) Then
                        Return True
                    End If
                Next
                Return False
            End Get
        End Property

        Public Sub Execute() Implements IBusinessCommand.Execute
            MyBase.Execute()
            MyBase.BusinessObject.BalanceInvoice()
        End Sub
    End Class

    Public Class UndoBalanceCommand
        Inherits BusinessCommandBase(Of Invoice)
        Implements IBusinessCommand

        Friend Sub New(ByVal invoice As Invoice)
            MyBase.New(invoice)
        End Sub

        ''' <summary>
        ''' Invoice can be Balanced when Status is NOT New and at least one Authorization exists with Status Reconsiled
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property CanExecute As Boolean Implements IBusinessCommand.CanExecute
            Get
                If (Me.BusinessObject.InvoiceStatusCode = Codes.INVOICE_STATUS__NEW) Then Return False
                For Each oClaimAuthorization As ClaimAuthorization In Me.BusinessObject.ClaimAuthorizations
                    If (oClaimAuthorization.ClaimAuthStatus = ClaimAuthorizationStatus.Reconsiled) Then
                        If (Not oClaimAuthorization.IsAddedToInvoiceGroup) Then
                            Return True
                        End If
                    End If
                Next
                Return False
            End Get
        End Property

        Public Sub Execute() Implements IBusinessCommand.Execute
            MyBase.Execute()
            MyBase.BusinessObject.UndoBalanceInvoice()
        End Sub
    End Class

#End Region

#Region "DataView Retrieveing Methods"
    ''' <summary>
    ''' Gets List of all Invoices in System.
    ''' </summary>
    ''' <param name="invoiceNumber"></param>
    ''' <param name="invoiceAmount"></param>
    ''' <param name="invoiceDate"></param>
    ''' <param name="serviceCenterName"></param>
    ''' <param name="batchNumber"></param>
    ''' <param name="claimNumber"></param>
    ''' <param name="dateCreated"></param>
    ''' <param name="authorizationNumber"></param>
    ''' <returns>Data View of Type <see cref="Invoice.InvoiceSearchDV" /> with result set.</returns>
    ''' <remarks>Search results are always restricted by Companies selected by current user defined in <see cref="ElitaPlusIdentity"/></remarks>
    ''' <exception cref="BOValidationException">When all search criteria are empty then throws exception <see cref="BOValidationException" /> with message 'Enter At Least One Search Criterion'</exception>
    Public Shared Function GetList(ByVal invoiceNumber As SearchCriteriaStringType, _
                                   ByVal invoiceAmount As SearchCriteriaStructType(Of Double), _
                                   ByVal invoiceDate As SearchCriteriaStructType(Of Date), _
                                   ByVal serviceCenterName As String, _
                                   ByVal batchNumber As SearchCriteriaStringType, _
                                   ByVal claimNumber As SearchCriteriaStringType, _
                                   ByVal dateCreated As SearchCriteriaStructType(Of Date), _
                                   ByVal authorizationNumber As SearchCriteriaStringType) As Invoice.InvoiceSearchDV
        Try
            Dim dal As New InvoiceDAL

            ' Check if atleast one search criteria is specified
            If ((invoiceNumber Is Nothing OrElse invoiceNumber.IsEmpty) AndAlso _
                (invoiceAmount Is Nothing OrElse invoiceAmount.IsEmpty) AndAlso _
                (invoiceDate Is Nothing OrElse invoiceDate.IsEmpty) AndAlso _
                (batchNumber Is Nothing OrElse batchNumber.IsEmpty) AndAlso _
                (claimNumber Is Nothing OrElse claimNumber.IsEmpty) AndAlso _
                (dateCreated Is Nothing OrElse dateCreated.IsEmpty) AndAlso _
                (authorizationNumber Is Nothing OrElse authorizationNumber.IsEmpty) AndAlso _
                (serviceCenterName Is Nothing OrElse serviceCenterName.Trim() = String.Empty)) Then
                Dim errors() As ValidationError = {New ValidationError(ElitaPlus.Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR, GetType(Claim), Nothing, "Search", Nothing)}
                Throw New BOValidationException(errors, GetType(Invoice).FullName)
            End If

            Return New InvoiceSearchDV(dal.LoadList(invoiceNumber, invoiceAmount, invoiceDate, serviceCenterName, batchNumber, claimNumber, dateCreated, authorizationNumber, _
                                                    ElitaPlusIdentity.Current.ActiveUser.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetAuthorizationList(ByVal invoiceId As Guid) As InvoiceAuthorizationSearchDV
        Try
            Dim dal As New InvoiceDAL
            Return New InvoiceAuthorizationSearchDV(dal.LoadAuthorizationList(invoiceId, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#End Region

#Region "Lazy Initialize Fields"
    Private _serviceCenter As ServiceCenter = Nothing
#End Region

#Region "Lazy Initialize Properties"
    Public Property ServiceCenter() As ServiceCenter
        Get
            If (_serviceCenter Is Nothing) Then
                If Not Me.ServiceCenterId.Equals(Guid.Empty) Then
                    Me.ServiceCenter = New ServiceCenter(Me.ServiceCenterId, Me.Dataset)
                End If
            End If
            Return _serviceCenter
        End Get
        Private Set(ByVal value As ServiceCenter)
            If (value Is Nothing OrElse _serviceCenter Is Nothing OrElse Not _serviceCenter.Equals(value)) Then
                _serviceCenter = value
            End If
        End Set
    End Property
#End Region

#Region "Claim Authorization"
    Public ReadOnly Property ClaimAuthorizations() As List(Of ClaimAuthorization)
        Get
            Return (From claimAuthId As Guid In _
                    (From item As InvoiceItem In Me.InvoiceItemChildren Select item.ClaimAuthorizationId).Distinct() _
                    Select New ClaimAuthorization(claimAuthId, Me.Dataset)).ToList()
        End Get
    End Property

    Public ReadOnly Property CanModifyClaimAuthorization(ByVal claimAuthorizationId As Guid) As Boolean
        Get
            ' If Authorization has all the Reconciled Records with Reconciliation Status = 'Reconsiled' OR
            '   Authorization has no records in Reconciliation then Authorization can be Modified
            Dim oClaimAuthorization As ClaimAuthorization
            oClaimAuthorization = Me.ClaimAuthorizations.Where(Function(item) item.ClaimAuthorizationId = claimAuthorizationId).FirstOrDefault
            If (Not oClaimAuthorization Is Nothing) Then
                If (Not (oClaimAuthorization.ClaimAuthStatus = ClaimAuthorizationStatus.Authorized OrElse oClaimAuthorization.ClaimAuthStatus = ClaimAuthorizationStatus.Fulfilled)) Then
                    Return False
                End If
            End If
            Return True
        End Get
    End Property
#End Region

#Region "Invoice Item"
    Public ReadOnly Property InvoiceItemChildren() As InvoiceItemList
        Get
            Return New InvoiceItemList(Me)
        End Get
    End Property

    Public Function GetInvoiceItemChild(ByVal childId As Guid) As InvoiceItem
        Return CType(Me.InvoiceItemChildren.GetChild(childId), InvoiceItem)
    End Function

    Public Function GetNewInvoiceItemChild() As InvoiceItem
        Dim newInvoiceItem As InvoiceItem = CType(Me.InvoiceItemChildren.GetNewChild, InvoiceItem)
        newInvoiceItem.InvoiceId = Me.Id
        Return newInvoiceItem
    End Function

    Public Function GetInvoiceAuthorizationSelectionView() As InvoiceAuthorizationSelectionView
        Dim invoiceAuthorizationTable As DataTable = InvoiceAuthorizationSelectionView.CreateTable
        Dim detail As InvoiceItem
        Dim claimAuthorizationIds As New List(Of Guid)

        For Each detail In Me.InvoiceItemChildren
            If (Not claimAuthorizationIds.Contains(detail.ClaimAuthorization.Id)) Then
                Dim row As DataRow = invoiceAuthorizationTable.NewRow
                row(InvoiceAuthorizationSelectionView.COL_NAME_IS_SELECTED) = True
                row(InvoiceAuthorizationSelectionView.COL_NAME_INVOICE_ID) = detail.InvoiceId.ToByteArray()
                row(InvoiceAuthorizationSelectionView.COL_NAME_CLAIM_AUTHORIZATION_ID) = detail.ClaimAuthorization.Id.ToByteArray()
                row(InvoiceAuthorizationSelectionView.COL_NAME_CLAIM_NUMBER) = detail.ClaimAuthorization.Claim.ClaimNumber
                row(InvoiceAuthorizationSelectionView.COL_NAME_AUTHORIZATION_NUMBER) = detail.ClaimAuthorization.AuthorizationNumber
                row(InvoiceAuthorizationSelectionView.COL_NAME_BATCH_NUMBER) = detail.ClaimAuthorization.BatchNumber
                row(InvoiceAuthorizationSelectionView.COL_NAME_SVC_REFERENCE_NUMBER) = detail.ClaimAuthorization.ServiceCenterReferenceNumber
                row(InvoiceAuthorizationSelectionView.COL_NAME_CUSTOMER_NAME) = detail.ClaimAuthorization.Claim.Certificate.CustomerName
                row(InvoiceAuthorizationSelectionView.COL_NAME_RESERVE_AMOUNT) = detail.ClaimAuthorization.Claim.ReserveAmount.Value
                If (detail.ClaimAuthorization.Claim.Deductible Is Nothing) Then
                    row(InvoiceAuthorizationSelectionView.COL_NAME_DEDUCTIBLE) = 0
                Else
                    row(InvoiceAuthorizationSelectionView.COL_NAME_DEDUCTIBLE) = detail.ClaimAuthorization.Claim.Deductible.Value
                End If
                row(InvoiceAuthorizationSelectionView.COL_NAME_INVOICE_AUTH_AMOUNT) = (From item As InvoiceItem In Me.InvoiceItemChildren Where item.ClaimAuthorizationId = detail.ClaimAuthorizationId AndAlso item.AdjustmentReasonId.Equals(Guid.Empty) Select item.Amount.Value).Sum()
                If (Not detail.ClaimAuthorization.RepairDate Is Nothing) Then row(InvoiceAuthorizationSelectionView.COL_NAME_REPAIR_DATE) = detail.ClaimAuthorization.RepairDate.Value
                If (Not detail.ClaimAuthorization.PickUpDate Is Nothing) Then row(InvoiceAuthorizationSelectionView.COL_NAME_PICK_UP_DATE) = detail.ClaimAuthorization.PickUpDate.Value
                invoiceAuthorizationTable.Rows.Add(row)
                claimAuthorizationIds.Add(detail.ClaimAuthorization.Id)
            End If
        Next

        If (Me.CanAddAuthorization) Then
            Dim oInvoiceDal As New InvoiceDAL
            For Each detailRow As DataRow In oInvoiceDal.LoadAuthorizationList(Me.ServiceCenterId, Me.BatchNumber).Tables(InvoiceDAL.TABLE_NAME).Rows
                Dim claimAuthorizationId As Guid
                claimAuthorizationId = New Guid(CType(detailRow(InvoiceAuthorizationSelectionView.COL_NAME_CLAIM_AUTHORIZATION_ID), Byte()))
                If (Not claimAuthorizationIds.Contains(claimAuthorizationId)) Then
                    Dim row As DataRow = invoiceAuthorizationTable.NewRow
                    row(InvoiceAuthorizationSelectionView.COL_NAME_IS_SELECTED) = False
                    row(InvoiceAuthorizationSelectionView.COL_NAME_INVOICE_ID) = Me.Id.ToByteArray()
                    row(InvoiceAuthorizationSelectionView.COL_NAME_CLAIM_AUTHORIZATION_ID) = claimAuthorizationId.ToByteArray()
                    row(InvoiceAuthorizationSelectionView.COL_NAME_CLAIM_NUMBER) = detailRow(InvoiceAuthorizationSelectionView.COL_NAME_CLAIM_NUMBER).ToString()
                    row(InvoiceAuthorizationSelectionView.COL_NAME_AUTHORIZATION_NUMBER) = detailRow(InvoiceAuthorizationSelectionView.COL_NAME_AUTHORIZATION_NUMBER).ToString()
                    row(InvoiceAuthorizationSelectionView.COL_NAME_BATCH_NUMBER) = detailRow(InvoiceAuthorizationSelectionView.COL_NAME_BATCH_NUMBER).ToString()
                    row(InvoiceAuthorizationSelectionView.COL_NAME_SVC_REFERENCE_NUMBER) = detailRow(InvoiceAuthorizationSelectionView.COL_NAME_SVC_REFERENCE_NUMBER).ToString()
                    row(InvoiceAuthorizationSelectionView.COL_NAME_CUSTOMER_NAME) = detailRow(InvoiceAuthorizationSelectionView.COL_NAME_CUSTOMER_NAME).ToString()
                    row(InvoiceAuthorizationSelectionView.COL_NAME_RESERVE_AMOUNT) = detailRow(InvoiceAuthorizationSelectionView.COL_NAME_RESERVE_AMOUNT).ToString()
                    row(InvoiceAuthorizationSelectionView.COL_NAME_DEDUCTIBLE) = detailRow(InvoiceAuthorizationSelectionView.COL_NAME_DEDUCTIBLE).ToString()
                    row(InvoiceAuthorizationSelectionView.COL_NAME_INVOICE_AUTH_AMOUNT) = detailRow(InvoiceAuthorizationSelectionView.COL_NAME_INVOICE_AUTH_AMOUNT).ToString()
                    If (Not detailRow(InvoiceAuthorizationSelectionView.COL_NAME_REPAIR_DATE) Is DBNull.Value) Then row(InvoiceAuthorizationSelectionView.COL_NAME_REPAIR_DATE) = detailRow(InvoiceAuthorizationSelectionView.COL_NAME_REPAIR_DATE)
                    If (Not detailRow(InvoiceAuthorizationSelectionView.COL_NAME_PICK_UP_DATE) Is DBNull.Value) Then row(InvoiceAuthorizationSelectionView.COL_NAME_PICK_UP_DATE) = detailRow(InvoiceAuthorizationSelectionView.COL_NAME_PICK_UP_DATE)
                    invoiceAuthorizationTable.Rows.Add(row)
                End If
            Next
        End If

        Return New InvoiceAuthorizationSelectionView(invoiceAuthorizationTable)
    End Function

    Public Class InvoiceAuthorizationSelectionView
        Inherits DataView
        Public Const COL_NAME_IS_SELECTED As String = "IS_SELECTED"
        Public Const COL_NAME_INVOICE_ID As String = InvoiceDAL.COL_NAME_INVOICE_ID
        Public Const COL_NAME_CLAIM_AUTHORIZATION_ID As String = ClaimAuthorizationDAL.COL_NAME_CLAIM_AUTHORIZATION_ID
        Public Const COL_NAME_CLAIM_NUMBER As String = ClaimDAL.COL_NAME_CLAIM_NUMBER
        Public Const COL_NAME_AUTHORIZATION_NUMBER As String = ClaimAuthorizationDAL.COL_NAME_AUTHORIZATION_NUMBER
        Public Const COL_NAME_BATCH_NUMBER As String = ClaimAuthorizationDAL.COL_NAME_BATCH_NUMBER
        Public Const COL_NAME_SVC_REFERENCE_NUMBER As String = ClaimAuthorizationDAL.COL_NAME_SVC_REFERENCE_NUMBER
        Public Const COL_NAME_CUSTOMER_NAME As String = ClaimDAL.COL_NAME_CUSTOMER_NAME
        Public Const COL_NAME_RESERVE_AMOUNT As String = "RESERVE_AMOUNT"
        Public Const COL_NAME_DEDUCTIBLE As String = ClaimDAL.COL_NAME_DEDUCTIBLE
        Public Const COL_NAME_INVOICE_AUTH_AMOUNT As String = "INVOICE_AUTH_AMOUNT"
        Public Const COL_NAME_REPAIR_DATE As String = ClaimAuthorizationDAL.COL_NAME_REPAIR_DATE
        Public Const COL_NAME_PICK_UP_DATE As String = ClaimAuthorizationDAL.COL_NAME_PICK_UP_DATE


        Public Sub New(ByVal Table As DataTable)
            MyBase.New(Table)
        End Sub

        Public Shared Function CreateTable() As DataTable
            Dim t As New DataTable
            t.Columns.Add(COL_NAME_IS_SELECTED, GetType(Boolean))
            t.Columns.Add(COL_NAME_INVOICE_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_CLAIM_AUTHORIZATION_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_CLAIM_NUMBER, GetType(String))
            t.Columns.Add(COL_NAME_AUTHORIZATION_NUMBER, GetType(String))
            t.Columns.Add(COL_NAME_BATCH_NUMBER, GetType(String))
            t.Columns.Add(COL_NAME_SVC_REFERENCE_NUMBER, GetType(String))
            t.Columns.Add(COL_NAME_CUSTOMER_NAME, GetType(String))
            t.Columns.Add(COL_NAME_RESERVE_AMOUNT, GetType(Decimal))
            t.Columns.Add(COL_NAME_DEDUCTIBLE, GetType(Decimal))
            t.Columns.Add(COL_NAME_INVOICE_AUTH_AMOUNT, GetType(Decimal))
            t.Columns.Add(COL_NAME_REPAIR_DATE, GetType(DateTime))
            t.Columns.Add(COL_NAME_PICK_UP_DATE, GetType(DateTime))
            Return t
        End Function
    End Class

    Public Function GetInvoiceAuthorizationItemSelectionView(ByVal claimAuthorizationId) As InvoiceAuthorizationItemSelectionView
        Dim invoiceAuthorizationItemTable As DataTable = InvoiceAuthorizationItemSelectionView.CreateTable
        Dim detail As InvoiceItem
        Dim claimAuthItemIds As New List(Of Guid)


        For Each detail In (From item As InvoiceItem In Me.InvoiceItemChildren Where item.ClaimAuthorizationId = claimAuthorizationId Select item Order By item.LineItemNumber)
            Dim row As DataRow = invoiceAuthorizationItemTable.NewRow
            row(InvoiceAuthorizationItemSelectionView.COL_NAME_INVOICE_ITEM_ID) = GuidControl.GuidToHexString(detail.Id)
            row(InvoiceAuthorizationItemSelectionView.COL_NAME_LINE_ITEM_NUMBER) = detail.LineItemNumber
            row(InvoiceAuthorizationItemSelectionView.COL_NAME_VENDOR_SKU) = detail.VendorSku
            row(InvoiceAuthorizationItemSelectionView.COL_NAME_VENDOR_SKU_DESCRIPTION) = detail.VendorSkuDescription
            row(InvoiceAuthorizationItemSelectionView.COL_NAME_AMOUNT) = detail.Amount.Value
            row(InvoiceAuthorizationItemSelectionView.COL_NAME_SERVICE_CLASS_DESCRIPTION) = LookupListNew.GetDescriptionFromId(LookupListNew.LK_SERVICE_CLASS, detail.ServiceClassId)
            If (Me.InvoiceStatusCode <> Codes.INVOICE_STATUS__NEW) Then
                If ((Not detail.InvoiceReconciliation Is Nothing) AndAlso (Not detail.InvoiceReconciliation.ReconciledAmount Is Nothing)) Then
                    row(InvoiceAuthorizationItemSelectionView.COL_NAME_RECONCILED_AMOUNT) = detail.InvoiceReconciliation.ReconciledAmount.Value
                End If
                If ((Not detail.InvoiceReconciliation Is Nothing) AndAlso (Not detail.InvoiceReconciliation.ClaimAuthorizationItem.Amount Is Nothing)) Then
                    row(InvoiceAuthorizationItemSelectionView.COL_NAME_AUTHORIZED_AMOUNT) = detail.InvoiceReconciliation.ClaimAuthorizationItem.Amount.Value
                End If
            End If

            If (Not detail.ServiceTypeId.Equals(Guid.Empty)) Then row(InvoiceAuthorizationItemSelectionView.COL_NAME_SERVICE_TYPE_DESCRIPTION) = LookupListNew.GetDescriptionFromId(LookupListNew.LK_SERVICE_TYPE_NEW, detail.ServiceTypeId)
            invoiceAuthorizationItemTable.Rows.Add(row)
            If (Not detail.InvoiceReconciliation Is Nothing) Then
                claimAuthItemIds.Add(detail.InvoiceReconciliation.ClaimAuthItemId)
            End If
        Next

        Dim oClaimAuthorization As ClaimAuthorization
        oClaimAuthorization = (Me.ClaimAuthorizations.Where(Function(item) item.ClaimAuthorizationId = claimAuthorizationId)).FirstOrDefault()
        If (oClaimAuthorization.ClaimAuthStatus = ClaimAuthorizationStatus.Reconsiled OrElse oClaimAuthorization.ClaimAuthStatus = ClaimAuthorizationStatus.ToBePaid _
            OrElse oClaimAuthorization.ClaimAuthStatus = ClaimAuthorizationStatus.Paid) Then
            For Each oClaimAuthItem As ClaimAuthItem In oClaimAuthorization.ClaimAuthorizationItemChildren
                If (Not claimAuthItemIds.Contains(oClaimAuthItem.Id)) Then
                    Dim row As DataRow = invoiceAuthorizationItemTable.NewRow
                    row(InvoiceAuthorizationItemSelectionView.COL_NAME_INVOICE_ITEM_ID) = GuidControl.GuidToHexString(Guid.Empty)
                    row(InvoiceAuthorizationItemSelectionView.COL_NAME_LINE_ITEM_NUMBER) = Nothing
                    row(InvoiceAuthorizationItemSelectionView.COL_NAME_VENDOR_SKU) = oClaimAuthItem.VendorSku
                    row(InvoiceAuthorizationItemSelectionView.COL_NAME_VENDOR_SKU_DESCRIPTION) = oClaimAuthItem.VendorSkuDescription
                    row(InvoiceAuthorizationItemSelectionView.COL_NAME_SERVICE_CLASS_DESCRIPTION) = LookupListNew.GetDescriptionFromId(LookupListNew.LK_SERVICE_CLASS, oClaimAuthItem.ServiceClassId)
                    row(InvoiceAuthorizationItemSelectionView.COL_NAME_AUTHORIZED_AMOUNT) = oClaimAuthItem.Amount.Value
                    If (Not oClaimAuthItem.ServiceTypeId.Equals(Guid.Empty)) Then row(InvoiceAuthorizationItemSelectionView.COL_NAME_SERVICE_TYPE_DESCRIPTION) = LookupListNew.GetDescriptionFromId(LookupListNew.LK_SERVICE_TYPE_NEW, oClaimAuthItem.ServiceTypeId)
                    invoiceAuthorizationItemTable.Rows.Add(row)
                End If
            Next
        End If
        Return New InvoiceAuthorizationItemSelectionView(invoiceAuthorizationItemTable)
    End Function

    Public Class InvoiceAuthorizationItemSelectionView
        Inherits DataView
        Public Const COL_NAME_INVOICE_ITEM_ID As String = InvoiceItemDAL.COL_NAME_INVOICE_ITEM_ID
        Public Const COL_NAME_LINE_ITEM_NUMBER As String = InvoiceItemDAL.COL_NAME_LINE_ITEM_NUMBER
        Public Const COL_NAME_VENDOR_SKU As String = InvoiceItemDAL.COL_NAME_VENDOR_SKU
        Public Const COL_NAME_VENDOR_SKU_DESCRIPTION As String = InvoiceItemDAL.COL_NAME_VENDOR_SKU_DESCRIPTION
        Public Const COL_NAME_AMOUNT As String = InvoiceItemDAL.COL_NAME_AMOUNT
        Public Const COL_NAME_SERVICE_CLASS_DESCRIPTION As String = "SERVICE_CLASS_DESCRIPTION"
        Public Const COL_NAME_SERVICE_TYPE_DESCRIPTION As String = "SERVICE_TYPE_DESCRIPTION"
        Public Const COL_NAME_RECONCILED_AMOUNT As String = InvoiceReconciliationDAL.COL_NAME_RECONCILED_AMOUNT
        Public Const COL_NAME_AUTHORIZED_AMOUNT As String = "AUTHORIZED_AMOUNT"

        Public Sub New(ByVal Table As DataTable)
            MyBase.New(Table)
        End Sub

        Public Shared Function CreateTable() As DataTable
            Dim t As New DataTable
            t.Columns.Add(COL_NAME_INVOICE_ITEM_ID.ToUpperInvariant(), GetType(String))
            t.Columns.Add(COL_NAME_LINE_ITEM_NUMBER.ToUpperInvariant(), GetType(String))
            t.Columns.Add(COL_NAME_VENDOR_SKU.ToUpperInvariant(), GetType(String))
            t.Columns.Add(COL_NAME_VENDOR_SKU_DESCRIPTION.ToUpperInvariant(), GetType(String))
            t.Columns.Add(COL_NAME_AMOUNT.ToUpperInvariant(), GetType(Decimal))
            t.Columns.Add(COL_NAME_SERVICE_CLASS_DESCRIPTION.ToUpperInvariant(), GetType(String))
            t.Columns.Add(COL_NAME_SERVICE_TYPE_DESCRIPTION.ToUpperInvariant(), GetType(String))
            t.Columns.Add(COL_NAME_RECONCILED_AMOUNT.ToUpperInvariant(), GetType(Decimal))
            t.Columns.Add(COL_NAME_AUTHORIZED_AMOUNT.ToUpperInvariant(), GetType(Decimal))
            Return t
        End Function
    End Class

#End Region

#Region "Invoice Search Data View"
    Public Class InvoiceSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_NAME_INVOICE_ID As String = InvoiceDAL.COL_NAME_INVOICE_ID
        Public Const COL_NAME_INVOICE_NUMBER As String = InvoiceDAL.COL_NAME_INVOICE_NUMBER
        Public Const COL_NAME_INVOICE_DATE As String = InvoiceDAL.COL_NAME_INVOICE_DATE
        Public Const COL_NAME_SERVICE_CENTER_DESCRIPTION As String = "service_center_description"
        Public Const COL_NAME_CREATED_DATE As String = InvoiceDAL.COL_NAME_CREATED_DATE
        Public Const COL_NAME_INVOICE_AMOUNT As String = InvoiceDAL.COL_NAME_INVOICE_AMOUNT
        Public Const COL_NAME_INVOICE_STATUS As String = "invoice_status"
        Public Const COL_NAME_INVOICE_STATUS_CODE As String = "invoice_status_code"
#End Region

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property InvoiceId(ByVal row) As Guid
            Get
                If row(COL_NAME_INVOICE_ID) Is DBNull.Value Then Return Nothing
                Return New Guid(CType(row(COL_NAME_INVOICE_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property InvoiceNumber(ByVal row As DataRow) As String
            Get
                If row(COL_NAME_INVOICE_NUMBER) Is DBNull.Value Then Return Nothing
                Return row(COL_NAME_INVOICE_NUMBER).ToString
            End Get
        End Property

        Public Shared ReadOnly Property InvoiceStatusCode(ByVal row As DataRow) As String
            Get
                If row(COL_NAME_INVOICE_STATUS_CODE) Is DBNull.Value Then Return Nothing
                Return row(COL_NAME_INVOICE_STATUS_CODE).ToString
            End Get
        End Property

        Public Shared ReadOnly Property InvoiceDate(ByVal row As DataRow) As DateType
            Get
                If row(COL_NAME_INVOICE_DATE) Is DBNull.Value Then Return Nothing
                Return New DateType(CType(row(COL_NAME_INVOICE_DATE), Date))
            End Get
        End Property

        Public Shared ReadOnly Property ServiceCenterDescription(ByVal row As DataRow) As String
            Get
                If row(COL_NAME_SERVICE_CENTER_DESCRIPTION) Is DBNull.Value Then Return Nothing
                Return row(COL_NAME_SERVICE_CENTER_DESCRIPTION).ToString
            End Get
        End Property

        Public Shared ReadOnly Property DateCreated(ByVal row As DataRow) As String
            Get
                If row(COL_NAME_CREATED_DATE) Is DBNull.Value Then Return Nothing
                Return New DateType(CType(row(COL_NAME_CREATED_DATE), Date))
            End Get
        End Property

        Public Shared ReadOnly Property InvoiceStatus(ByVal row As DataRow) As String
            Get
                If row(COL_NAME_INVOICE_STATUS) Is DBNull.Value Then Return Nothing
                Return row(COL_NAME_INVOICE_STATUS).ToString
            End Get
        End Property

        Public Shared ReadOnly Property InvoiceAmount(ByVal row As DataRow) As DecimalType
            Get
                If row(COL_NAME_INVOICE_AMOUNT) Is DBNull.Value Then Return Nothing
                Return New DecimalType(CType(row(COL_NAME_INVOICE_AMOUNT), Decimal))
            End Get
        End Property
    End Class
#End Region

#Region "Invoice Authorization Search Data View"
    Public Class InvoiceAuthorizationSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_NAME_INVOICE_ID As String = InvoiceDAL.COL_NAME_INVOICE_ID
        Public Const COL_NAME_CLAIM_NUMBER As String = ClaimDAL.COL_NAME_CLAIM_NUMBER
        Public Const COL_NAME_AUTHORIZATION_NUMBER As String = ClaimAuthorizationDAL.COL_NAME_AUTHORIZATION_NUMBER
        Public Const COL_NAME_BATCH_NUMBER As String = ClaimAuthorizationDAL.COL_NAME_BATCH_NUMBER
        Public Const COL_NAME_INVOICE_AUTH_AMOUNT As String = "invoice_auth_amount"
        Public Const COL_NAME_AUTHORIZATION_STATUS As String = "authorization_status"
#End Region

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property InvoiceId(ByVal row) As Guid
            Get
                If row(COL_NAME_INVOICE_ID) Is DBNull.Value Then Return Nothing
                Return New Guid(CType(row(COL_NAME_INVOICE_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property ClaimNumber(ByVal row As DataRow) As String
            Get
                If row(COL_NAME_CLAIM_NUMBER) Is DBNull.Value Then Return Nothing
                Return row(COL_NAME_CLAIM_NUMBER).ToString
            End Get
        End Property

        Public Shared ReadOnly Property AuthorizationNumber(ByVal row As DataRow) As String
            Get
                If row(COL_NAME_AUTHORIZATION_NUMBER) Is DBNull.Value Then Return Nothing
                Return row(COL_NAME_AUTHORIZATION_NUMBER).ToString
            End Get
        End Property

        Public Shared ReadOnly Property BatchNumber(ByVal row As DataRow) As String
            Get
                If row(COL_NAME_BATCH_NUMBER) Is DBNull.Value Then Return Nothing
                Return row(COL_NAME_BATCH_NUMBER).ToString
            End Get
        End Property

        Public Shared ReadOnly Property InvoiceAuthorizationAmount(ByVal row As DataRow) As DecimalType
            Get
                If row(COL_NAME_INVOICE_AUTH_AMOUNT) Is DBNull.Value Then Return Nothing
                Return New DecimalType(CType(row(COL_NAME_INVOICE_AUTH_AMOUNT), Decimal))
            End Get
        End Property

        Public Shared ReadOnly Property AuthorizationStatus(ByVal row As DataRow) As String
            Get
                If row(COL_NAME_AUTHORIZATION_STATUS) Is DBNull.Value Then Return Nothing
                Return row(COL_NAME_AUTHORIZATION_STATUS).ToString
            End Get
        End Property
    End Class
#End Region

#Region "Custom Validations"
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidateDueDateAfterInvoiceDate
        Inherits ValidBaseAttribute
        Private _fieldDisplayName As String
        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, INVOICE_FORM001)
            _fieldDisplayName = fieldDisplayName
        End Sub

        Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Invoice = CType(objectToValidate, Invoice)
            Return obj.IsDueDateValid()
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidateAtLeastOneItem
        Inherits ValidBaseAttribute
        Private _fieldDisplayName As String
        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, INVOICE_FORM003)
            _fieldDisplayName = fieldDisplayName
        End Sub

        Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Invoice = CType(objectToValidate, Invoice)
            Return obj.InvoiceItemChildren.Count > 0
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidateAuthorizationRepairDate
        Inherits ValidBaseAttribute
        Private _fieldDisplayName As String
        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, INVOICE_FORM004)
            _fieldDisplayName = fieldDisplayName
        End Sub

        Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Invoice = CType(objectToValidate, Invoice)
            For Each oClaimAuthorization As ClaimAuthorization In obj.ClaimAuthorizations
                If (oClaimAuthorization.RepairDate Is Nothing) Then
                    Return False
                End If
            Next
            Return True
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidateUniqueInvoiceNumber
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVOICE_NUM_ALREADY_EXISTS)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Invoice = CType(objectToValidate, Invoice)
            Dim result As Invoice.InvoiceSearchDV
           
            If (Not obj.ServiceCenter Is Nothing) Then
                result = obj.GetList(New SearchCriteriaStringType() With {.SearchType = SearchTypeEnum.Equals, .FromValue = obj.InvoiceNumber}, _
                                     New SearchCriteriaStructType(Of Double)(SearchDataType.Amount) With {.SearchType = SearchTypeEnum.Equals}, _
                                     New SearchCriteriaStructType(Of Date)(SearchDataType.Date) With {.SearchType = SearchTypeEnum.Equals}, _
                                     obj.ServiceCenter.Description, _
                                     New SearchCriteriaStringType() With {.SearchType = SearchTypeEnum.Equals}, _
                                     New SearchCriteriaStringType() With {.SearchType = SearchTypeEnum.Equals}, _
                                     New SearchCriteriaStructType(Of Date)(SearchDataType.DateTime) With {.SearchType = SearchTypeEnum.Equals}, _
                                     New SearchCriteriaStringType() With {.SearchType = SearchTypeEnum.Equals})
            End If
            If (Not result Is Nothing AndAlso result.Count > 0) Then
                For Each oRow As DataRowView In result
                    If Invoice.InvoiceSearchDV.InvoiceNumber(oRow.Row).Equals(obj.InvoiceNumber) _
                        AndAlso Not (Invoice.InvoiceSearchDV.InvoiceId(oRow.Row).Equals(obj.Id)) Then
                        Return False
                    End If
                Next
            End If
            Return True
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidateInvoiceDate
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_INVOICE_DATE_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Invoice = CType(objectToValidate, Invoice)
            If (obj.InvoiceDate.Value.Date > DateTime.Now) Then
                Return False
            End If
            Return True
        End Function
    End Class

    Public NotInheritable Class ValidateDuplicateServiceClassType
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.DUPLICATE_SERVICE_CLASS_TYPE_IN_INVOICE)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Invoice = CType(objectToValidate, Invoice)
            For Each oClaimAuthorization As ClaimAuthorization In obj.ClaimAuthorizations
                For Each oInvoiceItem As InvoiceItem In obj.InvoiceItemChildren.Where(Function(item) oClaimAuthorization.ClaimAuthorizationId.Equals(item.ClaimAuthorizationId) AndAlso item.AdjustmentReasonId.Equals(Guid.Empty))
                    If (obj.InvoiceItemChildren.Where(Function(item) _
                                                      oClaimAuthorization.ClaimAuthorizationId.Equals(item.ClaimAuthorizationId) AndAlso
                                                      oInvoiceItem.ServiceClassId.Equals(item.ServiceClassId) AndAlso
                                                      oInvoiceItem.ServiceTypeId.Equals(item.ServiceTypeId) AndAlso
                                                      item.AdjustmentReasonId.Equals(Guid.Empty) AndAlso
                                                      oInvoiceItem.VendorSku.Equals(item.VendorSku) AndAlso
                                                      Not oInvoiceItem.Id.Equals(item.Id)).Count() > 0) Then
                        Return False
                    End If
                Next
            Next
            Return True
        End Function
    End Class
#End Region

End Class


