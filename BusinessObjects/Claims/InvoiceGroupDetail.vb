'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (4/6/2013)  ********************

Public Class InvoiceGroupDetail
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
            Dim dal As New InvoiceGroupDetailDAL
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
            Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            setvalue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(id As Guid)
        Try
            Dim dal As New InvoiceGroupDetailDAL
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
    Private Sub Initialize()
    End Sub
#End Region


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid
        Get
            If row(InvoiceGroupDetailDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(InvoiceGroupDetailDAL.COL_NAME_INVOICE_GROUP_DETAIL_ID), Byte()))
            End If
        End Get
    End Property


    Public Property InvoiceGroupId As Guid
        Get
            CheckDeleted()
            If row(InvoiceGroupDetailDAL.COL_NAME_INVOICE_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(InvoiceGroupDetailDAL.COL_NAME_INVOICE_GROUP_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(InvoiceGroupDetailDAL.COL_NAME_INVOICE_GROUP_ID, Value)
        End Set
    End Property



    Public Property InvoiceReconciliationId As Guid
        Get
            CheckDeleted()
            If row(InvoiceGroupDetailDAL.COL_NAME_INVOICE_RECONCILIATION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(InvoiceGroupDetailDAL.COL_NAME_INVOICE_RECONCILIATION_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(InvoiceGroupDetailDAL.COL_NAME_INVOICE_RECONCILIATION_ID, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New InvoiceGroupDetailDAL
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
    Public Shared Function getInvoicegroupdetailList(invgrpid As Guid) As InvoiceGroupDetailSearchDV
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

    Public Shared Function getReconciledInvoicesList(servicecenterid As Guid, Invnumber As String, Invamount As String, _
                                   Invoicestatusid As Guid, InvoiceDate As String) As InvoiceGroupDetailSearchDV

        Try
            Dim dal As New InvoiceGroupDetailDAL
            Dim compIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies

            Dim languageid As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        

            Dim errors() As ValidationError = {New ValidationError(Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR, GetType(InvoiceGroup), Nothing, "Search", Nothing)}

          
         
            Return New InvoiceGroupDetailSearchDV(dal.LoadReconciledInvoiceslist(compIds, servicecenterid, Invnumber, Invamount, InvoiceDate, Invoicestatusid, languageid).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function



    Public Shared Function getLineItemsList(Invoiceid As Guid) As InvoiceLineItemsDV
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

    Public Shared Function GetNewDataViewRow(dv As DataView, bo As InvoiceItem) As DataView

        Dim dt As DataTable
        dt = dv.Table
        Dim certBO As Certificate = New Certificate
        Dim CertitemBO As CertItem = New CertItem

        If bo.IsNew Then
            Dim row As DataRow = dt.NewRow

            row(InvoiceLineItemsDV.COL_LINE_ITEM_TYPE) = bo.ServiceClassId 'String.Empty
            row(InvoiceLineItemsDV.COL_LINE_ITEM_DESCRIPTION) = bo.ServiceTypeId 'String.Empty
            row(InvoiceLineItemsDV.COL_LINE_ITEM_AMOUNT) = DBNull.Value
            row(InvoiceLineItemsDV.COL_CLAIM_NUMBER) = String.Empty
            row(InvoiceLineItemsDV.COL_AUTHORIZATION_NUMBER) = String.Empty
            row(InvoiceLineItemsDV.COL_VENDOR_SKU) = String.Empty
            row(InvoiceLineItemsDV.COL_VENDOR_SKU_DESC) = String.Empty

            row(InvoiceLineItemsDV.COL_LINE_ITEM_ID) = bo.Id.ToByteArray

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

    Public Shared Function Getlineiteminsertvalues(servicecentercode As String) As DataView
        Try
            Dim dal As New InvoiceGroupDetailDAL

            Dim ds As New DataSet
            ds = dal.getlineitemvalues(servicecentercode)
            Return New DataView(ds.Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    
    Public Shared Function Getmaxlineitemnumber(invoiceid As Guid) As DataView
        Try
            Dim dal As New InvoiceGroupDetailDAL
            Dim ds As New DataSet
            ds = dal.getmaxlineitemnumber(invoiceid)
            Return New DataView(ds.Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function Getinvoicereconids(invoiceid As Guid) As DataView
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

        Public Sub New(table As DataTable)
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

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
End Class



