'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (11/16/2007)  ********************

Public Class AcctPremInvoice
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
            Dim dal As New AcctPremInvoiceDAL
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
            Dim dal As New AcctPremInvoiceDAL
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
            If row(AcctPremInvoiceDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AcctPremInvoiceDAL.COL_NAME_ACCT_PREM_INVOICE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CompanyId() As Guid
        Get
            CheckDeleted()
            If row(AcctPremInvoiceDAL.COL_NAME_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AcctPremInvoiceDAL.COL_NAME_COMPANY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(AcctPremInvoiceDAL.COL_NAME_COMPANY_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property DealerId() As Guid
        Get
            CheckDeleted()
            If row(AcctPremInvoiceDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AcctPremInvoiceDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(AcctPremInvoiceDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property



    Public Property BranchId() As Guid
        Get
            CheckDeleted()
            If row(AcctPremInvoiceDAL.COL_NAME_BRANCH_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AcctPremInvoiceDAL.COL_NAME_BRANCH_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(AcctPremInvoiceDAL.COL_NAME_BRANCH_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=60)> _
    Public Property InvoiceNumber() As String
        Get
            CheckDeleted()
            If row(AcctPremInvoiceDAL.COL_NAME_INVOICE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(AcctPremInvoiceDAL.COL_NAME_INVOICE_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctPremInvoiceDAL.COL_NAME_INVOICE_NUMBER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=60)> _
    Public Property CreditNoteNumber() As String
        Get
            CheckDeleted()
            If row(AcctPremInvoiceDAL.COL_NAME_CREDIT_NOTE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(AcctPremInvoiceDAL.COL_NAME_CREDIT_NOTE_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctPremInvoiceDAL.COL_NAME_CREDIT_NOTE_NUMBER, Value)
        End Set
    End Property



    Public Property PreviousInvoiceDate() As DateType
        Get
            CheckDeleted()
            If row(AcctPremInvoiceDAL.COL_NAME_PREVIOUS_INVOICE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(AcctPremInvoiceDAL.COL_NAME_PREVIOUS_INVOICE_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(AcctPremInvoiceDAL.COL_NAME_PREVIOUS_INVOICE_DATE, Value)
        End Set
    End Property



    Public Property NewTotalCert() As LongType
        Get
            CheckDeleted()
            If row(AcctPremInvoiceDAL.COL_NAME_NEW_TOTAL_CERT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(AcctPremInvoiceDAL.COL_NAME_NEW_TOTAL_CERT), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(AcctPremInvoiceDAL.COL_NAME_NEW_TOTAL_CERT, Value)
        End Set
    End Property



    Public Property NewGrossAmtRecvd() As DecimalType
        Get
            CheckDeleted()
            If row(AcctPremInvoiceDAL.COL_NAME_NEW_GROSS_AMT_RECVD) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(AcctPremInvoiceDAL.COL_NAME_NEW_GROSS_AMT_RECVD), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(AcctPremInvoiceDAL.COL_NAME_NEW_GROSS_AMT_RECVD, Value)
        End Set
    End Property



    Public Property NewPremiumWritten() As DecimalType
        Get
            CheckDeleted()
            If row(AcctPremInvoiceDAL.COL_NAME_NEW_PREMIUM_WRITTEN) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(AcctPremInvoiceDAL.COL_NAME_NEW_PREMIUM_WRITTEN), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(AcctPremInvoiceDAL.COL_NAME_NEW_PREMIUM_WRITTEN, Value)
        End Set
    End Property



    Public Property NewCommission() As DecimalType
        Get
            CheckDeleted()
            If row(AcctPremInvoiceDAL.COL_NAME_NEW_COMMISSION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(AcctPremInvoiceDAL.COL_NAME_NEW_COMMISSION), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(AcctPremInvoiceDAL.COL_NAME_NEW_COMMISSION, Value)
        End Set
    End Property



    Public Property NewTax1() As DecimalType
        Get
            CheckDeleted()
            If row(AcctPremInvoiceDAL.COL_NAME_NEW_TAX1) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(AcctPremInvoiceDAL.COL_NAME_NEW_TAX1), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(AcctPremInvoiceDAL.COL_NAME_NEW_TAX1, Value)
        End Set
    End Property



    Public Property NewTax2() As DecimalType
        Get
            CheckDeleted()
            If row(AcctPremInvoiceDAL.COL_NAME_NEW_TAX2) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(AcctPremInvoiceDAL.COL_NAME_NEW_TAX2), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(AcctPremInvoiceDAL.COL_NAME_NEW_TAX2, Value)
        End Set
    End Property



    Public Property NewTax3() As DecimalType
        Get
            CheckDeleted()
            If row(AcctPremInvoiceDAL.COL_NAME_NEW_TAX3) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(AcctPremInvoiceDAL.COL_NAME_NEW_TAX3), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(AcctPremInvoiceDAL.COL_NAME_NEW_TAX3, Value)
        End Set
    End Property



    Public Property NewTax4() As DecimalType
        Get
            CheckDeleted()
            If row(AcctPremInvoiceDAL.COL_NAME_NEW_TAX4) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(AcctPremInvoiceDAL.COL_NAME_NEW_TAX4), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(AcctPremInvoiceDAL.COL_NAME_NEW_TAX4, Value)
        End Set
    End Property



    Public Property NewTax5() As DecimalType
        Get
            CheckDeleted()
            If row(AcctPremInvoiceDAL.COL_NAME_NEW_TAX5) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(AcctPremInvoiceDAL.COL_NAME_NEW_TAX5), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(AcctPremInvoiceDAL.COL_NAME_NEW_TAX5, Value)
        End Set
    End Property



    Public Property NewTax6() As DecimalType
        Get
            CheckDeleted()
            If row(AcctPremInvoiceDAL.COL_NAME_NEW_TAX6) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(AcctPremInvoiceDAL.COL_NAME_NEW_TAX6), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(AcctPremInvoiceDAL.COL_NAME_NEW_TAX6, Value)
        End Set
    End Property



    Public Property NewPremiumTotal() As DecimalType
        Get
            CheckDeleted()
            If row(AcctPremInvoiceDAL.COL_NAME_NEW_PREMIUM_TOTAL) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(AcctPremInvoiceDAL.COL_NAME_NEW_PREMIUM_TOTAL), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(AcctPremInvoiceDAL.COL_NAME_NEW_PREMIUM_TOTAL, Value)
        End Set
    End Property



    Public Property CancelTotalCert() As LongType
        Get
            CheckDeleted()
            If row(AcctPremInvoiceDAL.COL_NAME_CANCEL_TOTAL_CERT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(AcctPremInvoiceDAL.COL_NAME_CANCEL_TOTAL_CERT), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(AcctPremInvoiceDAL.COL_NAME_CANCEL_TOTAL_CERT, Value)
        End Set
    End Property



    Public Property CancelGrossAmtRecvd() As DecimalType
        Get
            CheckDeleted()
            If row(AcctPremInvoiceDAL.COL_NAME_CANCEL_GROSS_AMT_RECVD) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(AcctPremInvoiceDAL.COL_NAME_CANCEL_GROSS_AMT_RECVD), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(AcctPremInvoiceDAL.COL_NAME_CANCEL_GROSS_AMT_RECVD, Value)
        End Set
    End Property



    Public Property CancelPremiumWritten() As DecimalType
        Get
            CheckDeleted()
            If row(AcctPremInvoiceDAL.COL_NAME_CANCEL_PREMIUM_WRITTEN) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(AcctPremInvoiceDAL.COL_NAME_CANCEL_PREMIUM_WRITTEN), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(AcctPremInvoiceDAL.COL_NAME_CANCEL_PREMIUM_WRITTEN, Value)
        End Set
    End Property



    Public Property CancelCommission() As DecimalType
        Get
            CheckDeleted()
            If row(AcctPremInvoiceDAL.COL_NAME_CANCEL_COMMISSION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(AcctPremInvoiceDAL.COL_NAME_CANCEL_COMMISSION), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(AcctPremInvoiceDAL.COL_NAME_CANCEL_COMMISSION, Value)
        End Set
    End Property



    Public Property CancelTax1() As DecimalType
        Get
            CheckDeleted()
            If row(AcctPremInvoiceDAL.COL_NAME_CANCEL_TAX1) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(AcctPremInvoiceDAL.COL_NAME_CANCEL_TAX1), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(AcctPremInvoiceDAL.COL_NAME_CANCEL_TAX1, Value)
        End Set
    End Property



    Public Property CancelTax2() As DecimalType
        Get
            CheckDeleted()
            If row(AcctPremInvoiceDAL.COL_NAME_CANCEL_TAX2) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(AcctPremInvoiceDAL.COL_NAME_CANCEL_TAX2), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(AcctPremInvoiceDAL.COL_NAME_CANCEL_TAX2, Value)
        End Set
    End Property



    Public Property CancelTax3() As DecimalType
        Get
            CheckDeleted()
            If row(AcctPremInvoiceDAL.COL_NAME_CANCEL_TAX3) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(AcctPremInvoiceDAL.COL_NAME_CANCEL_TAX3), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(AcctPremInvoiceDAL.COL_NAME_CANCEL_TAX3, Value)
        End Set
    End Property



    Public Property CancelTax4() As DecimalType
        Get
            CheckDeleted()
            If row(AcctPremInvoiceDAL.COL_NAME_CANCEL_TAX4) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(AcctPremInvoiceDAL.COL_NAME_CANCEL_TAX4), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(AcctPremInvoiceDAL.COL_NAME_CANCEL_TAX4, Value)
        End Set
    End Property



    Public Property CancelTax5() As DecimalType
        Get
            CheckDeleted()
            If row(AcctPremInvoiceDAL.COL_NAME_CANCEL_TAX5) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(AcctPremInvoiceDAL.COL_NAME_CANCEL_TAX5), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(AcctPremInvoiceDAL.COL_NAME_CANCEL_TAX5, Value)
        End Set
    End Property



    Public Property CancelTax6() As DecimalType
        Get
            CheckDeleted()
            If row(AcctPremInvoiceDAL.COL_NAME_CANCEL_TAX6) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(AcctPremInvoiceDAL.COL_NAME_CANCEL_TAX6), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(AcctPremInvoiceDAL.COL_NAME_CANCEL_TAX6, Value)
        End Set
    End Property



    Public Property CancelPremiumTotal() As DecimalType
        Get
            CheckDeleted()
            If row(AcctPremInvoiceDAL.COL_NAME_CANCEL_PREMIUM_TOTAL) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(AcctPremInvoiceDAL.COL_NAME_CANCEL_PREMIUM_TOTAL), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(AcctPremInvoiceDAL.COL_NAME_CANCEL_PREMIUM_TOTAL, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New AcctPremInvoiceDAL
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
    Public Shared Function SearchInvoices(ByVal CompanyIds As ArrayList, ByVal DealerID As Guid, ByVal InvNum As String, _
                                    ByVal BeginDate As Date, ByVal EndDate As Date) As DataView
        Dim dal As New AcctPremInvoiceDAL
        Dim ds As DataSet

        Try
            ds = dal.LoadList(CompanyIds, DealerID, InvNum, BeginDate, EndDate)
            If Not ds Is Nothing AndAlso ds.Tables.Count > 0 Then
                Return ds.Tables(0).DefaultView
            Else
                Return New DataView
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        End Try

    End Function
#End Region

#Region "Create Invoice"
    Public Shared Sub CreateInvoice(ByVal DealerID As Guid, ByVal UserNewWorkID As String)
        Try
            Dim dal As New AcctPremInvoiceDAL
            dal.CreateInvoice(DealerID, UserNewWorkID)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Sub
#End Region

End Class