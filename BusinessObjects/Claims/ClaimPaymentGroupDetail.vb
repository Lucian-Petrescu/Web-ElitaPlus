'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (3/19/2013)  ********************

Public Class ClaimPaymentGroupDetail
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
            Dim dal As New ClaimPaymentGroupDetailDAL
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
            Dim dal As New ClaimPaymentGroupDetailDAL
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

#Region "Private Members"
    'Initialization code for new objects
    Private Sub Initialize()
    End Sub
#End Region

#Region "PaymentGroupSearchDV"

    Public Class PaymentClaimAuthLineItemsDV
        Inherits DataView

#Region "Constants"
        Public Const COL_NAME_SVC_CLASS As String = "SVCCLASS"
        Public Const COL_NAME_SVC_TYPE As String = "SVCTYPE"
        Public Const COL_NAME_LINE_ITEM_NUMBER As String = "line_item_number"
        Public Const COL_NAME_AUTH_AMOUNT As String = "AuthAmount"
        Public Const COL_NAME_INVOICE_AMOUNT As String = "InvoiceAmount"
#End Region
    End Class


    Public Class PaymentGroupDetailSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_NAME_PAYMENT_GROUP_ID As String = "payment_group_id"
        Public Const COL_NAME_PAYMENT_GROUP_DETAIL_ID As String = "payment_group_detail_id"
        Public Const COL_NAME_SERVICE_CENTER_CODE As String = "service_center_code"
        Public Const COL_NAME_CLAIM_NUMBER As String = "claim_number"
        Public Const COL_NAME_CLAIM_SELECTED As String = "selected"
        Public Const COL_NAME_AUTHORIZATION_NUMBER As String = "authorization_number"
        Public Const COL_NAME_AUTHORIZATION_ID As String = "claim_authorization_id"
        Public Const COL_NAME_EXCLUDE_DEDUCTIBLE As String = "exclude_deductible"
        Public Const COL_NAME_INVOICE_NUMBER As String = "invoice_number"
        Public Const COL_NAME_INVOICE_ID As String = "invoice_id"
        Public Const COL_NAME_INVOICE_DATE As String = "invoice_date"
        Public Const COL_NAME_RECONCILED_AMOUNT As String = "reconciled_amount"
        Public Const COL_NAME_DUE_DATE As String = "due_date"
        Public Const COL_NAME_COUNT As String = "count"
#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property SvcCode(row As DataRow) As String
            Get
                If row(COL_NAME_SERVICE_CENTER_CODE) Is DBNull.Value Then Return Nothing
                Return row(COL_NAME_SERVICE_CENTER_CODE).ToString
            End Get
        End Property

        Public Shared ReadOnly Property PaymentGrpDetailId(row) As Guid
            Get
                If row(COL_NAME_PAYMENT_GROUP_DETAIL_ID) Is DBNull.Value Then Return Nothing
                Return New Guid(CType(row(COL_NAME_PAYMENT_GROUP_DETAIL_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property ClaimNumber(row As DataRow) As String
            Get
                If row(COL_NAME_CLAIM_NUMBER) Is DBNull.Value Then Return Nothing
                Return row(COL_NAME_CLAIM_NUMBER).ToString
            End Get
        End Property

        Public Shared ReadOnly Property AuthorizationNumber(row As DataRow) As String
            Get
                If row(COL_NAME_AUTHORIZATION_NUMBER) Is DBNull.Value Then Return Nothing
                Return row(COL_NAME_AUTHORIZATION_NUMBER).ToString
            End Get
        End Property

        Public Shared ReadOnly Property InvoiceNumber(row As DataRow) As String
            Get
                If row(COL_NAME_INVOICE_NUMBER) Is DBNull.Value Then Return Nothing
                Return row(COL_NAME_INVOICE_NUMBER).ToString
            End Get
        End Property

        Public Shared ReadOnly Property InvoiceDate(row As DataRow) As DateType
            Get
                If row(COL_NAME_INVOICE_DATE) Is DBNull.Value Then Return Nothing
                Return New DateType(CType(row(COL_NAME_INVOICE_DATE), Date))
            End Get
        End Property

        Public Shared ReadOnly Property InvoiceReconciledAmount(row As DataRow) As DecimalType
            Get
                If row(COL_NAME_RECONCILED_AMOUNT) Is DBNull.Value Then Return Nothing
                Return New DecimalType(CType(row(COL_NAME_RECONCILED_AMOUNT), Decimal))
            End Get
        End Property

        Public Shared ReadOnly Property DueDate(row As DataRow) As DateType
            Get
                If row(COL_NAME_DUE_DATE) Is DBNull.Value Then Return Nothing
                Return New DateType(CType(row(COL_NAME_DUE_DATE), Date))
            End Get
        End Property

        Public Shared ReadOnly Property Count(row As DataRow) As String
            Get
                If row(COL_NAME_COUNT) Is DBNull.Value Then Return Nothing
                Return CType(row(COL_NAME_COUNT), Integer).ToString
            End Get
        End Property

    End Class

#End Region


#Region "DataView Retrieveing Methods"

    Public Shared Function GetPaymentGroupDetail(pymntGroupId As Guid) As PaymentGroupDetailSearchDV
        Try
            Dim dal As New ClaimPaymentGroupDetailDAL
            Return New PaymentGroupDetailSearchDV(dal.GetPaymentGroupDetail(pymntGroupId).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetClaimAuthorizationsToBePaid(pymntGroupId As Guid) As DataView
        Try
            Dim dal As New ClaimPaymentGroupDetailDAL
            Return New DataView(dal.GetClaimAuthorizationsToBePaid(pymntGroupId).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function


    Public Shared Function SelectPayables(claimNumber As String, InvGrpNumber As String, InvNumber As String, _
                                          mobileNumber As String, _
                                          invoiceDateRange As SearchCriteriaStructType(Of Date), _
                                          accountNumber As String, _
                                          serviceCenterName As String, authorizationNumber As String, _
                                          Optional ByVal sortBy As String = PaymentGroupDetailSearchDV.COL_NAME_SERVICE_CENTER_CODE) As PaymentGroupDetailSearchDV

        Try
            Dim dal As New ClaimPaymentGroupDetailDAL
            Dim compIds As ArrayList = New ArrayList
            With ElitaPlusIdentity.Current.ActiveUser
                compIds = .Companies 'compIds.Add(.GetSelectedAssignedCompanies(ElitaPlusIdentity.Current.ActiveUser.Id))
            End With

            Dim errors() As ValidationError = {New ValidationError(ElitaPlus.Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR, _
                                                                   GetType(ClaimPaymentGroupDetailDAL), Nothing, "Search", Nothing)}

            'Convert the Claim Number to UPPER Case
            If (Not (claimNumber.Equals(String.Empty))) Then
                claimNumber = claimNumber.ToUpper
            End If

            'Convert the Service Center Name to UPPER Case
            If (Not (serviceCenterName Is Nothing)) AndAlso (Not (serviceCenterName.Equals(String.Empty))) Then
                serviceCenterName = serviceCenterName.ToUpper
            End If
            'Check if the user has entered any search criteria... if NOT, then display an error
            If (claimNumber.Equals(String.Empty) AndAlso InvGrpNumber.Equals(String.Empty) AndAlso _
                InvNumber.Equals(String.Empty) AndAlso mobileNumber.Equals(String.Empty) AndAlso _
                (invoiceDateRange Is Nothing OrElse invoiceDateRange.IsEmpty) AndAlso _
                accountNumber.Equals(String.Empty) AndAlso serviceCenterName.Equals(String.Empty) AndAlso authorizationNumber.Equals(String.Empty)) Then
                Throw New BOValidationException(errors, GetType(ClaimPaymentGroupDetailDAL).FullName)
            End If

            Return New PaymentGroupDetailSearchDV(dal.SelectPayables(compIds, claimNumber, InvGrpNumber, InvNumber, mobileNumber, invoiceDateRange, _
                                                                     accountNumber, serviceCenterName, authorizationNumber, sortBy).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function


#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If Row(ClaimPaymentGroupDetailDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimPaymentGroupDetailDAL.COL_NAME_PAYMENT_GROUP_DETAIL_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property PaymentGroupId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimPaymentGroupDetailDAL.COL_NAME_PAYMENT_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimPaymentGroupDetailDAL.COL_NAME_PAYMENT_GROUP_ID), Byte()))
            End If
        End Get
        Set(Value As Guid)
            CheckDeleted()
            SetValue(ClaimPaymentGroupDetailDAL.COL_NAME_PAYMENT_GROUP_ID, Value)
        End Set
    End Property


    '<ValueMandatory("")> _
    'Public Property InvoiceReconciliationId() As Guid
    '    Get
    '        CheckDeleted()
    '        If Row(ClaimPaymentGroupDetailDAL.COL_NAME_INVOICE_RECONCILIATION_ID) Is DBNull.Value Then
    '            Return Nothing
    '        Else
    '            Return New Guid(CType(Row(ClaimPaymentGroupDetailDAL.COL_NAME_INVOICE_RECONCILIATION_ID), Byte()))
    '        End If
    '    End Get
    '    Set(ByVal Value As Guid)
    '        CheckDeleted()
    '        Me.SetValue(ClaimPaymentGroupDetailDAL.COL_NAME_INVOICE_RECONCILIATION_ID, Value)
    '    End Set
    'End Property

    <ValidStringLength("", Max:=1)> _
    Public Property ExcludeDeductible() As String
        Get
            CheckDeleted()
            If Row(ClaimPaymentGroupDetailDAL.COL_NAME_EXCLUDE_DEDUCTIBLE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimPaymentGroupDetailDAL.COL_NAME_EXCLUDE_DEDUCTIBLE), String)
            End If
        End Get
        Set(Value As String)
            CheckDeleted()
            SetValue(ClaimPaymentGroupDetailDAL.COL_NAME_EXCLUDE_DEDUCTIBLE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ClaimAuthorizationId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimPaymentGroupDetailDAL.COL_NAME_AUTHORIZATION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimPaymentGroupDetailDAL.COL_NAME_AUTHORIZATION_ID), Byte()))
            End If
        End Get
        Set(Value As Guid)
            CheckDeleted()
            SetValue(ClaimPaymentGroupDetailDAL.COL_NAME_AUTHORIZATION_ID, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ClaimPaymentGroupDetailDAL
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

#End Region

    Public Shared Function GetClaimAuthLineItems(claimAuthId As Guid) As DataView
        Try
            Dim dal As New ClaimPaymentGroupDetailDAL
            Return New DataView(dal.GetClaimAuthLineItems(claimAuthId).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Class PymntGrpDetailList
        Inherits BusinessObjectListBase

        Public Sub New(parent As ClaimPaymentGroup)
            MyBase.New(LoadTable(parent), GetType(ClaimPaymentGroupDetail), parent)
        End Sub

        Public Overrides Function Belong(bo As BusinessObjectBase) As Boolean
            Return CType(bo, ClaimPaymentGroupDetail).PaymentGroupId.Equals(CType(Parent, ClaimPaymentGroup).Id)
        End Function

        Private Shared Function LoadTable(parent As ClaimPaymentGroup) As DataTable
            Try
                If Not parent.IsChildrenCollectionLoaded(GetType(PymntGrpDetailList)) Then
                    Dim dal As New ClaimPaymentGroupDetailDAL
                    dal.GetPaymentGroupDetail(parent.Dataset, parent.Id)
                    parent.AddChildrenCollection(GetType(PymntGrpDetailList))
                End If
                Return parent.Dataset.Tables(ClaimPaymentGroupDetailDAL.TABLE_NAME)
            Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End Function

    End Class

End Class