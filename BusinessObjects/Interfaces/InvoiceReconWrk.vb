'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (2/22/2013)  ********************

Public Class InvoiceReconWrk
    Inherits BusinessObjectBase
    Implements IFileLoadReconWork
#Region "Constants"
    Public Const COL_NAME_INVOICE_RECON_WRK_ID As String = "invoice_recon_wrk_id"
    Public Const COL_NAME_CLAIMLOAD_FILE_PROCESSED_ID As String = "claimload_file_processed_id"
    Public Const COL_NAME_RECORD_TYPE As String = "record_type"
    Public Const COL_NAME_REJECT_CODE As String = "reject_code"
    Public Const COL_NAME_REJECT_REASON As String = "reject_reason"
    Public Const COL_NAME_LOADED As String = "loaded"
    Public Const COL_NAME_COMPANY_CODE As String = "company_code"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_INVOICE_NUMBER As String = "invoice_number"
    Public Const COL_NAME_INVOICE_DATE As String = "invoice_date"
    Public Const COL_NAME_INVOICE_ID As String = "invoice_id"
    Public Const COL_NAME_REPAIR_DATE As String = "repair_date"
    Public Const COL_NAME_ATTRIBUTES As String = "attributes"
    Public Const COL_NAME_CLAIM_NUMBER As String = "claim_number"
    Public Const COL_NAME_CLAIM_ID As String = "claim_id"
    Public Const COL_NAME_AUTHORIZATION_NUMBER As String = "authorization_number"
    Public Const COL_NAME_AUTHORIZATION_ID As String = "authorization_id"
    Public Const COL_NAME_LINE_ITEM_NUMBER As String = "line_item_number"
    Public Const COL_NAME_vendor_SKU As String = "vendor_SKU"
    Public Const COL_NAME_vendor_SKU_DESCRIPTION As String = "vendor_sku_description"
    Public Const COL_NAME_SERVICE_CENTER_ID As String = "service_center_id"
    Public Const COL_NAME_SERVICE_CENTER_CODE As String = "service_center_code"
    Public Const COL_NAME_SERVICE_CLASS_ID As String = "service_class_id"
    Public Const COL_NAME_SERVICE_TYPE_ID As String = "service_type_id"
    Public Const COL_NAME_AMOUNT As String = "amount"
    Public Const COL_NAME_ENTIRE_RECORD As String = "entire_record"
    Public Const COL_NAME_DUE_DATE As String = "due_date"
    Public Const COL_NAME_SERVICE_LEVEL As String = "service_level"
    Public Const COL_NAME_SERVICE_LEVEL_ID As String = "service_level_id"
    Public Const COL_NAME_MSG_PARAMETER_COUNT As String = "msg_parameter_count"
    Public Const COL_NAME_Translated_MSG As String = "translated_msg"
#End Region

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load(id)
    End Sub

    Public Sub New(ByVal id As Guid, ByVal sModifiedDate As String)
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load(id)
        Me.VerifyConcurrency(sModifiedDate)
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
            Dim dal As New InvoiceReconWrkDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            setvalue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New InvoiceReconWrkDAL
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
    End Sub
#End Region


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid Implements IFileLoadReconWork.Id
        Get
            If Row(InvoiceReconWrkDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(InvoiceReconWrkDAL.COL_NAME_INVOICE_RECON_WRK_ID), Byte()))
            End If
        End Get
    End Property

    Public ReadOnly Property ParentId As Guid Implements IFileLoadReconWork.ParentId
        Get
            Return Me.ClaimloadFileProcessedId
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property ClaimloadFileProcessedId() As Guid
        Get
            CheckDeleted()
            If Row(InvoiceReconWrkDAL.COL_NAME_CLAIMLOAD_FILE_PROCESSED_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(InvoiceReconWrkDAL.COL_NAME_CLAIMLOAD_FILE_PROCESSED_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(InvoiceReconWrkDAL.COL_NAME_CLAIMLOAD_FILE_PROCESSED_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=8)> _
    Public Property RecordType() As String Implements IFileLoadReconWork.RecordType
        Get
            CheckDeleted()
            If Row(InvoiceReconWrkDAL.COL_NAME_RECORD_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(InvoiceReconWrkDAL.COL_NAME_RECORD_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(InvoiceReconWrkDAL.COL_NAME_RECORD_TYPE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=20)> _
    Public Property RejectCode() As String Implements IFileLoadReconWork.RejectCode
        Get
            CheckDeleted()
            If Row(InvoiceReconWrkDAL.COL_NAME_REJECT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(InvoiceReconWrkDAL.COL_NAME_REJECT_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(InvoiceReconWrkDAL.COL_NAME_REJECT_CODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=240)> _
    Public Property RejectReason() As String Implements IFileLoadReconWork.RejectReason
        Get
            CheckDeleted()
            If Row(InvoiceReconWrkDAL.COL_NAME_REJECT_REASON) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(InvoiceReconWrkDAL.COL_NAME_REJECT_REASON), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(InvoiceReconWrkDAL.COL_NAME_REJECT_REASON, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=4)> _
    Public Property Loaded() As String Implements IFileLoadReconWork.Loaded
        Get
            CheckDeleted()
            If Row(InvoiceReconWrkDAL.COL_NAME_LOADED) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(InvoiceReconWrkDAL.COL_NAME_LOADED), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(InvoiceReconWrkDAL.COL_NAME_LOADED, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=20)> _
    Public Property CompanyCode() As String
        Get
            CheckDeleted()
            If row(InvoiceReconWrkDAL.COL_NAME_COMPANY_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(InvoiceReconWrkDAL.COL_NAME_COMPANY_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(InvoiceReconWrkDAL.COL_NAME_COMPANY_CODE, Value)
        End Set
    End Property



    Public Property CompanyId() As Guid
        Get
            CheckDeleted()
            If row(InvoiceReconWrkDAL.COL_NAME_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(InvoiceReconWrkDAL.COL_NAME_COMPANY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(InvoiceReconWrkDAL.COL_NAME_COMPANY_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=40)> _
    Public Property InvoiceNumber() As String
        Get
            CheckDeleted()
            If row(InvoiceReconWrkDAL.COL_NAME_INVOICE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(InvoiceReconWrkDAL.COL_NAME_INVOICE_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(InvoiceReconWrkDAL.COL_NAME_INVOICE_NUMBER, Value)
        End Set
    End Property

    Public Property InvoiceDate() As DateType
        Get
            CheckDeleted()
            If row(InvoiceReconWrkDAL.COL_NAME_INVOICE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(InvoiceReconWrkDAL.COL_NAME_INVOICE_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(InvoiceReconWrkDAL.COL_NAME_INVOICE_DATE, Value)
        End Set
    End Property



    Public Property InvoiceId() As Guid
        Get
            CheckDeleted()
            If row(InvoiceReconWrkDAL.COL_NAME_INVOICE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(InvoiceReconWrkDAL.COL_NAME_INVOICE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(InvoiceReconWrkDAL.COL_NAME_INVOICE_ID, Value)
        End Set
    End Property



    Public Property RepairDate() As DateType
        Get
            CheckDeleted()
            If row(InvoiceReconWrkDAL.COL_NAME_REPAIR_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(InvoiceReconWrkDAL.COL_NAME_REPAIR_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(InvoiceReconWrkDAL.COL_NAME_REPAIR_DATE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=4000)> _
    Public Property Attributes() As String
        Get
            CheckDeleted()
            If row(InvoiceReconWrkDAL.COL_NAME_ATTRIBUTES) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(InvoiceReconWrkDAL.COL_NAME_ATTRIBUTES), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(InvoiceReconWrkDAL.COL_NAME_ATTRIBUTES, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=40)> _
    Public Property ClaimNumber() As String
        Get
            CheckDeleted()
            If row(InvoiceReconWrkDAL.COL_NAME_CLAIM_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(InvoiceReconWrkDAL.COL_NAME_CLAIM_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(InvoiceReconWrkDAL.COL_NAME_CLAIM_NUMBER, Value)
        End Set
    End Property



    Public Property ClaimId() As Guid
        Get
            CheckDeleted()
            If row(InvoiceReconWrkDAL.COL_NAME_CLAIM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(InvoiceReconWrkDAL.COL_NAME_CLAIM_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(InvoiceReconWrkDAL.COL_NAME_CLAIM_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=40)> _
    Public Property AuthorizationNumber() As String
        Get
            CheckDeleted()
            If row(InvoiceReconWrkDAL.COL_NAME_AUTHORIZATION_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(InvoiceReconWrkDAL.COL_NAME_AUTHORIZATION_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(InvoiceReconWrkDAL.COL_NAME_AUTHORIZATION_NUMBER, Value)
        End Set
    End Property



    Public Property AuthorizationId() As Guid
        Get
            CheckDeleted()
            If row(InvoiceReconWrkDAL.COL_NAME_AUTHORIZATION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(InvoiceReconWrkDAL.COL_NAME_AUTHORIZATION_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(InvoiceReconWrkDAL.COL_NAME_AUTHORIZATION_ID, Value)
        End Set
    End Property



    Public Property LineItemNumber() As LongType
        Get
            CheckDeleted()
            If row(InvoiceReconWrkDAL.COL_NAME_LINE_ITEM_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(InvoiceReconWrkDAL.COL_NAME_LINE_ITEM_NUMBER), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(InvoiceReconWrkDAL.COL_NAME_LINE_ITEM_NUMBER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200)> _
    Public Property VendorSku() As String
        Get
            CheckDeleted()
            If Row(InvoiceReconWrkDAL.COL_NAME_VENDOR_SKU) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(InvoiceReconWrkDAL.COL_NAME_VENDOR_SKU), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(InvoiceReconWrkDAL.COL_NAME_VENDOR_SKU, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=800)> _
    Public Property VendorSkuDescription() As String
        Get
            CheckDeleted()
            If Row(InvoiceReconWrkDAL.COL_NAME_VENDOR_SKU_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(InvoiceReconWrkDAL.COL_NAME_VENDOR_SKU_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(InvoiceReconWrkDAL.COL_NAME_VENDOR_SKU_DESCRIPTION, Value)
        End Set
    End Property



    Public Property ServiceCenterId() As Guid
        Get
            CheckDeleted()
            If row(InvoiceReconWrkDAL.COL_NAME_SERVICE_CENTER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(InvoiceReconWrkDAL.COL_NAME_SERVICE_CENTER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(InvoiceReconWrkDAL.COL_NAME_SERVICE_CENTER_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=40)> _
    Public Property ServiceCenterCode() As String
        Get
            CheckDeleted()
            If row(InvoiceReconWrkDAL.COL_NAME_SERVICE_CENTER_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(InvoiceReconWrkDAL.COL_NAME_SERVICE_CENTER_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(InvoiceReconWrkDAL.COL_NAME_SERVICE_CENTER_CODE, Value)
        End Set
    End Property



    Public Property ServiceClassId() As Guid
        Get
            CheckDeleted()
            If row(InvoiceReconWrkDAL.COL_NAME_SERVICE_CLASS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(InvoiceReconWrkDAL.COL_NAME_SERVICE_CLASS_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(InvoiceReconWrkDAL.COL_NAME_SERVICE_CLASS_ID, Value)
        End Set
    End Property



    Public Property ServiceTypeId() As Guid
        Get
            CheckDeleted()
            If row(InvoiceReconWrkDAL.COL_NAME_SERVICE_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(InvoiceReconWrkDAL.COL_NAME_SERVICE_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(InvoiceReconWrkDAL.COL_NAME_SERVICE_TYPE_ID, Value)
        End Set
    End Property



    Public Property Amount() As DecimalType
        Get
            CheckDeleted()
            If row(InvoiceReconWrkDAL.COL_NAME_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(InvoiceReconWrkDAL.COL_NAME_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(InvoiceReconWrkDAL.COL_NAME_AMOUNT, Value)
        End Set
    End Property



    Public Property DueDate() As DateType
        Get
            CheckDeleted()
            If row(InvoiceReconWrkDAL.COL_NAME_DUE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(InvoiceReconWrkDAL.COL_NAME_DUE_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(InvoiceReconWrkDAL.COL_NAME_DUE_DATE, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=20)> _
    Public Property ServiceLevel() As String
        Get
            CheckDeleted()
            If Row(InvoiceReconWrkDAL.COL_NAME_SERVICE_LEVEL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(InvoiceReconWrkDAL.COL_NAME_SERVICE_LEVEL), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(InvoiceReconWrkDAL.COL_NAME_SERVICE_LEVEL, Value)
        End Set
    End Property
    Public Property ServiceLevelId() As Guid
        Get
            CheckDeleted()
            If Row(InvoiceReconWrkDAL.COL_NAME_SERVICE_LEVEL_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(InvoiceReconWrkDAL.COL_NAME_SERVICE_LEVEL_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(InvoiceReconWrkDAL.COL_NAME_SERVICE_LEVEL_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=4000)> _
    Public Property EntireRecord() As String
        Get
            CheckDeleted()
            If row(InvoiceReconWrkDAL.COL_NAME_ENTIRE_RECORD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(InvoiceReconWrkDAL.COL_NAME_ENTIRE_RECORD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(InvoiceReconWrkDAL.COL_NAME_ENTIRE_RECORD, Value)
        End Set
    End Property

    Public ReadOnly Property FamilyDataSet As DataSet Implements IFileLoadReconWork.FamilyDataSet
        Get
            Return Me.Dataset
        End Get
    End Property


#End Region

#Region "Public Members"
    Public Overrides Sub Save() Implements IFileLoadReconWork.Save
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New InvoiceReconWrkDAL
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
#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function LoadList(ByVal ClaimLoadFileId As Guid) As DataView
        Dim dal As New InvoiceReconWrkDAL
        Dim ds As DataSet = New DataSet()
        dal.LoadList(ds, ClaimLoadFileId, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
        Return ds.Tables(InvoiceReconWrkDAL.TABLE_NAME).DefaultView
    End Function
#End Region

End Class

Public Class InvoiceReconWrkList
    Inherits BusinessObjectListEnumerableBase(Of ClaimFileProcessed, InvoiceReconWrk)

    Public Sub New(ByVal parent As ClaimFileProcessed)
        MyBase.New(LoadTable(parent), parent)
    End Sub

    Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
        Return CType(bo, InvoiceReconWrk).ClaimloadFileProcessedId.Equals(CType(Parent, ClaimFileProcessed).Id)
    End Function

    Private Shared Function LoadTable(ByVal parent As ClaimFileProcessed) As DataTable
        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(InvoiceReconWrkList)) Then
                Dim dal As New InvoiceReconWrkDAL
                dal.LoadList(parent.Dataset, parent.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                parent.AddChildrenCollection(GetType(InvoiceReconWrkList))
            End If
            Return parent.Dataset.Tables(InvoiceReconWrkDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
End Class
