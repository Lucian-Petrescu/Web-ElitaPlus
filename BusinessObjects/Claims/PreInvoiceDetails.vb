'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (2/17/2015)  ********************

Public Class PreInvoiceDetails
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
            Dim dal As New PreInvoiceDetailsDAL
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
            Dim dal As New PreInvoiceDetailsDAL            
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
            If row(PreInvoiceDetailsDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(PreInvoiceDetailsDAL.COL_NAME_PRE_INVOICE_DETAILS_ID), Byte()))
            End If
        End Get
    End Property
	
    <ValueMandatory("")> _
    Public Property PreInvoiceId() As Guid
        Get
            CheckDeleted()
            If row(PreInvoiceDetailsDAL.COL_NAME_PRE_INVOICE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(PreInvoiceDetailsDAL.COL_NAME_PRE_INVOICE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(PreInvoiceDetailsDAL.COL_NAME_PRE_INVOICE_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property ClaimId() As Guid
        Get
            CheckDeleted()
            If row(PreInvoiceDetailsDAL.COL_NAME_CLAIM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(PreInvoiceDetailsDAL.COL_NAME_CLAIM_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(PreInvoiceDetailsDAL.COL_NAME_CLAIM_ID, Value)
        End Set
    End Property

    'Public Property BatchNumber() As String
    '    Get
    '        CheckDeleted()
    '        If Row(PreInvoiceDetailsDAL.COL_BATCH_NUMBER) Is DBNull.Value Then
    '            Return Nothing
    '        Else
    '            Return CType(Row(PreInvoiceDetailsDAL.COL_BATCH_NUMBER), String)
    '        End If
    '    End Get
    '    Set(ByVal Value As String)
    '        CheckDeleted()
    '        Me.SetValue(PreInvoiceDetailsDAL.COL_BATCH_NUMBER, Value)
    '    End Set
    'End Property

    'Public Property Status() As String
    '    Get
    '        CheckDeleted()
    '        If Row(PreInvoiceDetailsDAL.COL_STATUS) Is DBNull.Value Then
    '            Return Nothing
    '        Else
    '            Return CType(Row(PreInvoiceDetailsDAL.COL_STATUS), String)
    '        End If
    '    End Get
    '    Set(ByVal Value As String)
    '        CheckDeleted()
    '        Me.SetValue(PreInvoiceDAL.COL_NAME_BATCH_NUMBER, Value)
    '    End Set
    'End Property

    'Public Property TotalAmount() As DecimalType
    '    Get
    '        CheckDeleted()
    '        If Row(PreInvoiceDetailsDAL.COL_TOTAL_AMOUNT) Is DBNull.Value Then
    '            Return Nothing
    '        Else
    '            Return New DecimalType(CType(Row(PreInvoiceDetailsDAL.COL_TOTAL_AMOUNT), Decimal))
    '        End If
    '    End Get
    '    Set(ByVal Value As DecimalType)
    '        CheckDeleted()
    '        Me.SetValue(PreInvoiceDetailsDAL.COL_TOTAL_AMOUNT, Value)
    '    End Set
    'End Property


    '<ValueMandatory("")> _
    'Public Property TotalClaims() As DecimalType
    '    Get
    '        CheckDeleted()
    '        If Row(PreInvoiceDetailsDAL.COL_CLAIMS) Is DBNull.Value Then
    '            Return Nothing
    '        Else
    '            Return New DecimalType(CType(Row(PreInvoiceDetailsDAL.COL_CLAIMS), Decimal))
    '        End If
    '    End Get
    '    Set(ByVal Value As DecimalType)
    '        CheckDeleted()
    '        Me.SetValue(PreInvoiceDetailsDAL.COL_CLAIMS, Value)
    '    End Set
    'End Property
	
	
   

#End Region

#Region "Public Members"
    Public Overrides Sub Save()         
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New PreInvoiceDetailsDAL
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

    Public Shared Function LoadPreInvoiceClaims(ByVal preinvoiceId As Guid, ByVal serviceCenterId As Guid, ByVal MasterCenterId As Guid, ByVal languageId As Guid) As PreInvoiceDetailSearchDV

        Dim dal As New PreInvoiceDetailsDAL

        Return New PreInvoiceDetailSearchDV(dal.LoadPreInvoiceProcessClaims(preinvoiceId, serviceCenterId, MasterCenterId, languageId).Tables(0))

    End Function

    Public Shared Function ApprovePreInvoiceClaims(ByVal company_id As Guid, ByVal pre_invoice_id As Guid) As DBHelper.DBHelperParameter()
        Try
            Dim dal As New PreInvoiceDetailsDAL
            Return dal.ApprovePreInvoice(company_id, pre_invoice_id)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function RejectPreInvoiceClaims(ByVal company_id As Guid, ByVal claimIds As String, ByVal pre_invoice_id As Guid, ByVal Comments As String) As DBHelper.DBHelperParameter()
        Try
            Dim dal As New PreInvoiceDetailsDAL
            Return dal.RejectPreInvoiceClaims(company_id, claimIds, pre_invoice_id, Comments)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function CheckClaimInPreInvoice(ByVal ClaimID As Guid) As Integer

        Dim dal As New PreInvoiceDetailsDAL

        Return dal.CheckClaimInPreInvoice(ClaimID)

    End Function

    Public Shared Function UpdatePreInvoiceTotal(ByVal PreInvoiceId As Guid, ByVal TotalAmount As Decimal) As Integer

        Dim dal As New PreInvoiceDetailsDAL

        Return dal.UpdatePreInvoiceTotal(PreInvoiceId, TotalAmount)

    End Function
#End Region

#Region "DataView Retrieveing Methods"
    Public Class PreInvoiceDetailSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_PRE_INVOICE_ID As String = "pre_invoice_id"
        Public Const COL_PRE_INV_DETAIL_ID As String = "pre_invoice_details_id"
        Public Const COL_CLAIM_ID As String = "claim_id"
        Public Const COL_CLAIM_NUMBER As String = "claim_number"
        Public Const COL_MASTER_CENTER_NAME As String = "master_center_name"
        Public Const COL_SERVICE_CENTER_NAME As String = "service_center_name"
        Public Const COL_CLAIM_TYPE As String = "claim_type"
        Public Const COL_AUTH_AMOUNT As String = "authorization_amount"

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


