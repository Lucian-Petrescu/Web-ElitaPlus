'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (2/21/2013)  ********************

Public NotInheritable Class ClaimAuthItem
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(id As Guid)
        MyBase.New()
        Dataset = New DataSet
        Load(id)
        _originalAmount = If(Amount Is Nothing, New Decimal(0D), Amount.Value)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Dataset = New DataSet
        Load()
        IsDeleted = False
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(id As Guid, familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
        _originalAmount = If(Amount Is Nothing, New Decimal(0D), Amount.Value)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load()
        IsDeleted = False
    End Sub

    'New BO attaching to a BO family
    Public Sub New(familyDS As DataSet, parentId As Guid)
        MyBase.New(False)
        Dataset = familyDS
        Load()
        Initialize(parentId)
        _originalAmount = If(Amount Is Nothing, New Decimal(0D), Amount.Value)
    End Sub

    Public Sub New(row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
        If Not IsNew Then
            _originalAmount = CType(row(ClaimAuthItemDAL.COL_NAME_AMOUNT, DataRowVersion.Original), Decimal)
        Else
            IsDeleted = False
        End If


    End Sub

    Protected Sub Load()
        Try
            Dim dal As New ClaimAuthItemDAL
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
            Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(id As Guid)
        Try
            Dim dal As New ClaimAuthItemDAL
            If _isDSCreator Then
                If Row IsNot Nothing Then
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
    Private Sub Initialize(parentId As Guid)
        ClaimAuthorizationId = parentId
        LineItemNumber = GetNewLineItemNumber()
        IsDeleted = False
    End Sub

    Friend Function GetNewLineItemNumber() As Integer
        Dim i As Integer = 1
        If (ClaimAuthorization IsNot Nothing) Then i = ClaimAuthorization.ClaimAuthorizationItemChildren.Count

        Return i
    End Function

    Private _originalAmount As Decimal = New Decimal(0D)
#End Region

#Region "Constants"
    Private Const MIN_DOUBLE As Double = 0.0
    Private Const MAX_DOUBLE As Double = 999999999.99
    Private Const MIM_DECIMAL_NUMBERS As Integer = 2
    Private Const AMOUNT_MIN_2_DIGITS_ALLOWED_AFTER_DECIMAL_POINT As String = "MIN_2_DIGITS_ALLOWED_AFTER_DECIMAL_POINT"
    Private Const AMOUNT_9_DIGITS_ALLOWED_BEFORE_DECIMAL_POINT As String = "AMOUNT_9_DIGITS_ALLOWED_BEFORE_DECIMAL_POINT"
#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid
        Get
            If Row(ClaimAuthItemDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimAuthItemDAL.COL_NAME_CLAIM_AUTH_ITEM_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property ClaimAuthorizationId As Guid
        Get
            CheckDeleted()
            If Row(ClaimAuthItemDAL.COL_NAME_CLAIM_AUTHORIZATION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimAuthItemDAL.COL_NAME_CLAIM_AUTHORIZATION_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            CheckIsOld()
            SetValue(ClaimAuthItemDAL.COL_NAME_CLAIM_AUTHORIZATION_ID, Value)
            ClaimAuthorization = Nothing
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property ServiceClassId As Guid
        Get
            CheckDeleted()
            If Row(ClaimAuthItemDAL.COL_NAME_SERVICE_CLASS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimAuthItemDAL.COL_NAME_SERVICE_CLASS_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            CheckIsOld()
            SetValue(ClaimAuthItemDAL.COL_NAME_SERVICE_CLASS_ID, Value)
        End Set
    End Property

    Public Property ServiceTypeId As Guid
        Get
            CheckDeleted()
            If Row(ClaimAuthItemDAL.COL_NAME_SERVICE_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimAuthItemDAL.COL_NAME_SERVICE_TYPE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            CheckIsOld()
            SetValue(ClaimAuthItemDAL.COL_NAME_SERVICE_TYPE_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property LineItemNumber As LongType
        Get
            CheckDeleted()
            If Row(ClaimAuthItemDAL.COL_NAME_LINE_ITEM_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ClaimAuthItemDAL.COL_NAME_LINE_ITEM_NUMBER), Long))
            End If
        End Get
        Set
            CheckDeleted()
            CheckIsOld()
            SetValue(ClaimAuthItemDAL.COL_NAME_LINE_ITEM_NUMBER, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=200)> _
    Public Property VendorSku As String
        Get
            CheckDeleted()
            If Row(ClaimAuthItemDAL.COL_NAME_VENDOR_SKU) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimAuthItemDAL.COL_NAME_VENDOR_SKU), String)
            End If
        End Get
        Set
            CheckDeleted()
            CheckIsOld()
            SetValue(ClaimAuthItemDAL.COL_NAME_VENDOR_SKU, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=800)> _
    Public Property VendorSkuDescription As String
        Get
            CheckDeleted()
            If Row(ClaimAuthItemDAL.COL_NAME_VENDOR_SKU_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimAuthItemDAL.COL_NAME_VENDOR_SKU_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            CheckIsOld()
            SetValue(ClaimAuthItemDAL.COL_NAME_VENDOR_SKU_DESCRIPTION, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidNumericRange("", MIN:=MAX_DOUBLE * -1, Max:=MAX_DOUBLE, Message:=AMOUNT_9_DIGITS_ALLOWED_BEFORE_DECIMAL_POINT)> _
    Public Property Amount As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimAuthItemDAL.COL_NAME_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ClaimAuthItemDAL.COL_NAME_AMOUNT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimAuthItemDAL.COL_NAME_AMOUNT, Value)
        End Set
    End Property

    Public Property AdjustmentReasonId As Guid
        Get
            CheckDeleted()
            If Row(ClaimAuthItemDAL.COL_NAME_ADJUSTMENT_REASON_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimAuthItemDAL.COL_NAME_ADJUSTMENT_REASON_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimAuthItemDAL.COL_NAME_ADJUSTMENT_REASON_ID, Value)
        End Set
    End Property
    Public Property RevAdjustmentReasonId As Guid
        Get
            CheckDeleted()
            If Row(ClaimAuthItemDAL.COL_NAME_ADJUSTMENT_REASON_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimAuthItemDAL.COL_NAME_ADJUSTMENT_REASON_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimAuthItemDAL.COL_NAME_ADJUSTMENT_REASON_ID, Value)
        End Set
    End Property


    Friend ReadOnly Property OrginalAmount As Decimal
        Get
            Return _originalAmount
        End Get
    End Property

    Public ReadOnly Property ServiceClassCode As String
        Get
            Return LookupListNew.GetCodeFromId(Codes.SERVICE_CLASS, ServiceClassId)
        End Get
    End Property

    Public ReadOnly Property ServiceTypeCode As String
        Get
            Return LookupListNew.GetCodeFromId(Codes.SERVICE_CLASS_TYPE, ServiceTypeId)
        End Get
    End Property

    Public Property IsDeleted As Boolean
        Get
            CheckDeleted()
            Return Convert.ToBoolean(CType(Row(ClaimAuthItemDAL.COL_NAME_IS_DELETED), Integer))
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimAuthItemDAL.COL_NAME_IS_DELETED, Convert.ToInt16(Value))
        End Set
    End Property

    Public Property PoAdjustmentReasonId As Guid
        Get
            CheckDeleted()
            If row(ClaimAuthItemDAL.COL_NAME_PO_ADJUSTMENT_REASON_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimAuthItemDAL.COL_NAME_PO_ADJUSTMENT_REASON_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimAuthItemDAL.COL_NAME_PO_ADJUSTMENT_REASON_ID, Value)
        End Set
    End Property
                
    
    Public Property AdjLineItemNumber As LongType
        Get
            CheckDeleted()
            If row(ClaimAuthItemDAL.COL_NAME_ADJ_LINE_ITEM_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(ClaimAuthItemDAL.COL_NAME_ADJ_LINE_ITEM_NUMBER), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimAuthItemDAL.COL_NAME_ADJ_LINE_ITEM_NUMBER, Value)
        End Set
    End Property


#End Region

#Region "Derived Properties"
    Public ReadOnly Property InvoiceReconciliationId As Guid
        Get
            Dim row As DataRow = Nothing
            Dim dal As New InvoiceReconciliationDAL
            If Dataset.Tables.IndexOf(InvoiceReconciliationDAL.TABLE_NAME) >= 0 Then
                row = FindRow(Id, ClaimAuthItemDAL.COL_NAME_CLAIM_AUTH_ITEM_ID, Dataset.Tables(InvoiceReconciliationDAL.TABLE_NAME))
            End If
            If row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.LoadByClaimAuthItemId(Dataset, Id)
                row = FindRow(Id, ClaimAuthItemDAL.COL_NAME_CLAIM_AUTH_ITEM_ID, Dataset.Tables(InvoiceReconciliationDAL.TABLE_NAME))
            End If
            If (row Is Nothing) Then
                Return Guid.Empty
            Else
                Return New InvoiceReconciliation(row).Id
            End If
        End Get
    End Property
#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            If Not MyBase.IsDeleted Then
                ClaimAuthorization.EvaluateContainsDeductible()
            End If
            MyBase.Save()

            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ClaimAuthItemDAL
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

#Region "DataView Retrieveing Methods"

#End Region

#Region "Lazy Initialize Fields"
    Private _claimAuthorization As ClaimAuthorization = Nothing
#End Region

#Region "Lazy Initialize Properties"
    Public Property ClaimAuthorization As ClaimAuthorization
        Get
            If (_claimAuthorization Is Nothing) Then
                If Not ClaimAuthorizationId.Equals(Guid.Empty) Then
                    Me.ClaimAuthorization = New ClaimAuthorization(ClaimAuthorizationId, Dataset)
                End If
            End If
            Return _claimAuthorization
        End Get
        Private Set
            _claimAuthorization = value
        End Set
    End Property
#End Region

End Class


Public Class ClaimAuthorizationItemList
    Inherits BusinessObjectListEnumerableBase(Of ClaimAuthorization, ClaimAuthItem)

    Public Sub New(parent As ClaimAuthorization)
        MyBase.New(LoadTable(parent), parent)
    End Sub

    Public Overrides Function Belong(bo As BusinessObjectBase) As Boolean
        Return CType(bo, ClaimAuthItem).ClaimAuthorizationId.Equals(CType(Parent, ClaimAuthorization).Id)
    End Function

    Private Shared Function LoadTable(parent As ClaimAuthorization) As DataTable
        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(ClaimAuthorizationItemList)) Then
                Dim dal As New ClaimAuthItemDAL
                dal.LoadList(parent.Dataset, parent.Id)
                parent.AddChildrenCollection(GetType(ClaimAuthorizationItemList))
            End If
            Return parent.Dataset.Tables(ClaimAuthItemDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public ReadOnly Property HasCollectionChanged As Boolean
        Get
            If Where(Function(item) item.IsDeleted OrElse (item.IsNew AndAlso item.AdjustmentReasonId.Equals(Guid.Empty))).Count > 0 Then Return True Else Return False
        End Get
    End Property

End Class
