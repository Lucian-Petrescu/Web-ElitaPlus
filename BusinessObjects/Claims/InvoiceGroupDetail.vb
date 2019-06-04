'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (4/6/2013)  ********************

Public Class InvoiceGroupDetail
    Inherits BusinessObjectBase

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
            Dim dal As New InvoiceGroupDetailDAL
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
            Dim dal As New InvoiceGroupDetailDAL
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
    Public ReadOnly Property Id() As Guid
        Get
            If row(InvoiceGroupDetailDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(InvoiceGroupDetailDAL.COL_NAME_INVOICE_GROUP_DETAIL_ID), Byte()))
            End If
        End Get
    End Property


    Public Property InvoiceGroupId() As Guid
        Get
            CheckDeleted()
            If row(InvoiceGroupDetailDAL.COL_NAME_INVOICE_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(InvoiceGroupDetailDAL.COL_NAME_INVOICE_GROUP_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(InvoiceGroupDetailDAL.COL_NAME_INVOICE_GROUP_ID, Value)
        End Set
    End Property



    Public Property InvoiceReconciliationId() As Guid
        Get
            CheckDeleted()
            If row(InvoiceGroupDetailDAL.COL_NAME_INVOICE_RECONCILIATION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(InvoiceGroupDetailDAL.COL_NAME_INVOICE_RECONCILIATION_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(InvoiceGroupDetailDAL.COL_NAME_INVOICE_RECONCILIATION_ID, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New InvoiceGroupDetailDAL
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
    Public Shared Function getInvoicegroupdetailList(ByVal invgrpid As Guid) As InvoiceGroupDetailSearchDV
        Try
            Dim languageid As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId

            Dim dal As New InvoiceGroupDetailDAL
            Dim ds As New DataSet, dv As DataView
            ds = dal.LoadList(invgrpid, languageid)
            Return New InvoiceGroupDetailSearchDV(ds.Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function getReconciledInvoicesList(ByVal servicecenterid As Guid, ByVal Invnumber As String, ByVal Invamount As String, _
                                   ByVal Invoicestatusid As Guid, ByVal InvoiceDate As String) As InvoiceGroupDetailSearchDV

        Try
            Dim dal As New InvoiceGroupDetailDAL
            Dim compIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies

            Dim languageid As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        

            Dim errors() As ValidationError = {New ValidationError(ElitaPlus.Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR, GetType(InvoiceGroup), Nothing, "Search", Nothing)}

          
         
            Return New InvoiceGroupDetailSearchDV(dal.LoadReconciledInvoiceslist(compIds, servicecenterid, Invnumber, Invamount, InvoiceDate, Invoicestatusid, languageid).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function



    Public Shared Function getLineItemsList(ByVal Invoiceid As Guid) As InvoiceLineItemsDV
        Try
            Dim dal As New InvoiceGroupDetailDAL
            Dim languageid As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim ds As New DataSet, dv As DataView
            ds = dal.LoadlineitemRecords(Invoiceid, languageid)
            Return New InvoiceLineItemsDV(ds.Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function


    Public Shared Function GetInvoicegroupnumber() As DBHelper.DBHelperParameter()
        Try
            Dim dal As New InvoiceGroupDetailDAL
            Dim companygroupid As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            Return dal.getinvoicegrpnumber(companygroupid)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetNewDataViewRow(ByVal dv As DataView, ByVal bo As InvoiceItem) As DataView

        Dim dt As DataTable
        dt = dv.Table
        Dim certBO As Certificate = New Certificate
        Dim CertitemBO As CertItem = New CertItem

        If bo.IsNew Then
            Dim row As DataRow = dt.NewRow

            row(InvoiceGroupDetail.InvoiceLineItemsDV.COL_LINE_ITEM_TYPE) = bo.ServiceClassId 'String.Empty
            row(InvoiceGroupDetail.InvoiceLineItemsDV.COL_LINE_ITEM_DESCRIPTION) = bo.ServiceTypeId 'String.Empty
            row(InvoiceGroupDetail.InvoiceLineItemsDV.COL_LINE_ITEM_AMOUNT) = DBNull.Value
            row(InvoiceGroupDetail.InvoiceLineItemsDV.COL_CLAIM_NUMBER) = String.Empty
            row(InvoiceGroupDetail.InvoiceLineItemsDV.COL_AUTHORIZATION_NUMBER) = String.Empty
            row(InvoiceGroupDetail.InvoiceLineItemsDV.COL_VENDOR_SKU) = String.Empty
            row(InvoiceGroupDetail.InvoiceLineItemsDV.COL_VENDOR_SKU_DESC) = String.Empty

            row(InvoiceGroupDetail.InvoiceLineItemsDV.COL_LINE_ITEM_ID) = bo.Id.ToByteArray

            dt.Rows.Add(row)
        End If

        Return (dv)

    End Function

    Public Shared Function Addstandardlineitems() As DataSet
        Try
            Dim dal As New InvoiceGroupDetailDAL
            Dim languageid As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim ds As New DataSet
            ds = dal.GetStandardlineitems(languageid)
            Return (ds)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function Getlineiteminsertvalues(ByVal servicecentercode As String) As DataView
        Try
            Dim dal As New InvoiceGroupDetailDAL

            Dim ds As New DataSet
            ds = dal.getlineitemvalues(servicecentercode)
            Return New DataView(ds.Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    
    Public Shared Function Getmaxlineitemnumber(ByVal invoiceid As Guid) As DataView
        Try
            Dim dal As New InvoiceGroupDetailDAL
            Dim ds As New DataSet
            ds = dal.getmaxlineitemnumber(invoiceid)
            Return New DataView(ds.Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function Getinvoicereconids(ByVal invoiceid As Guid) As DataView
        Try
            Dim dal As New InvoiceGroupDetailDAL
            Dim ds As New DataSet
            ds = dal.getinvoicereconids(invoiceid)
            Return New DataView(ds.Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#End Region

    Public Class InvoiceGroupDetailSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_INVOICE_ID As String = "invoice_id"
        Public Const COL_SERVICE_CENTER As String = "Service_center_description"
        Public Const COL_INVOICE_NUMBER As String = "invoice_number"
        Public Const COL_INVOICE_AMOUNT As String = "invoice_amount"
        Public Const COL_INVOICE_DATE As String = "invoice_date"
        Public Const COL_LINE_ITEM_AMOUNT As String = "line_item_amount"
        Public Const COL_INVOICE_STATUS As String = "invoice_status"
        Public Const COL_INV_RECON_ID As String = "invoice_reconciliation_id"
        Public Const COL_INV_GRP_DETAIL_ID As String = "invoice_group_detail_id"

#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

    Public Class InvoiceLineItemsDV
        Inherits DataView
#Region "Constants"
        Public Const COL_LINE_ITEM_ID As String = "invoice_item_id"
        Public Const COL_LINE_ITEM_TYPE As String = "line_item_type"
        Public Const COL_LINE_ITEM_DESCRIPTION As String = "line_item_description"
        Public Const COL_LINE_ITEM_AMOUNT As String = "line_item_amount"
        Public Const COL_CLAIM_NUMBER As String = "claim_number"
        Public Const COL_AUTHORIZATION_NUMBER As String = "authorization_number"
        Public Const COL_VENDOR_SKU As String = "vendor_sku"
        Public Const COL_VENDOR_SKU_DESC As String = "vendor_sku_description"
        Public Const COL_SERIAL_NUMBER As String = "serial_number"
        Public Const COL_CERT_ID As String = "cert_id"
        Public Const COl_CERT_ITEM_ID As String = "cert_item_id"
        Public Const COL_CLAIM_AUTHORIZATION_ID As String = "claim_authorization_id"
#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
End Class



