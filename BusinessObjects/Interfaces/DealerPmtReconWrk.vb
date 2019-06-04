'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (11/8/2004)  ********************

Public Class DealerPmtReconWrk
    Inherits BusinessObjectBase


#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Me.Dataset = New Dataset
        Me.Load(id)
    End Sub

    'Exiting BO
    Public Sub New(ByVal id As Guid, ByVal sModifiedDate As String)
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load(id)
        Me.VerifyConcurrency(sModifiedDate)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Me.Dataset = New Dataset
        Me.Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDS As Dataset)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As Dataset)
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
            Dim dal As New DealerPmtReconWrkDAL
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
            Dim dal As New DealerPmtReconWrkDAL
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
            If Row(DealerPmtReconWrkDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerPmtReconWrkDAL.COL_NAME_DEALER_PMT_RECON_WRK_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property DealerfileProcessedId() As Guid
        Get
            CheckDeleted()
            If Row(DealerPmtReconWrkDAL.COL_NAME_DEALERFILE_PROCESSED_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DealerPmtReconWrkDAL.COL_NAME_DEALERFILE_PROCESSED_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DealerPmtReconWrkDAL.COL_NAME_DEALERFILE_PROCESSED_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)> _
    Public Property RejectReason() As String
        Get
            CheckDeleted()
            If Row(DealerPmtReconWrkDAL.COL_NAME_REJECT_REASON) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerPmtReconWrkDAL.COL_NAME_REJECT_REASON), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerPmtReconWrkDAL.COL_NAME_REJECT_REASON, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1)> _
    Public Property PaymentLoaded() As String
        Get
            CheckDeleted()
            If Row(DealerPmtReconWrkDAL.COL_NAME_PAYMENT_LOADED) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerPmtReconWrkDAL.COL_NAME_PAYMENT_LOADED), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerPmtReconWrkDAL.COL_NAME_PAYMENT_LOADED, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1)> _
    Public Property RecordType() As String
        Get
            CheckDeleted()
            If Row(DealerPmtReconWrkDAL.COL_NAME_RECORD_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerPmtReconWrkDAL.COL_NAME_RECORD_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerPmtReconWrkDAL.COL_NAME_RECORD_TYPE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=20)> _
    Public Property Certificate() As String
        Get
            CheckDeleted()
            If Row(DealerPmtReconWrkDAL.COL_NAME_CERTIFICATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerPmtReconWrkDAL.COL_NAME_CERTIFICATE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerPmtReconWrkDAL.COL_NAME_CERTIFICATE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=30)> _
    Public Property SerialNumber() As String
        Get
            CheckDeleted()
            If Row(DealerPmtReconWrkDAL.COL_NAME_SERIAL_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerPmtReconWrkDAL.COL_NAME_SERIAL_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerPmtReconWrkDAL.COL_NAME_SERIAL_NUMBER, Value)
        End Set
    End Property



    Public Property PaymentAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(DealerPmtReconWrkDAL.COL_NAME_PAYMENT_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DealerPmtReconWrkDAL.COL_NAME_PAYMENT_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(DealerPmtReconWrkDAL.COL_NAME_PAYMENT_AMOUNT, Value)
        End Set
    End Property



    Public Property DateOfPayment() As DateType
        Get
            CheckDeleted()
            If Row(DealerPmtReconWrkDAL.COL_NAME_DATE_OF_PAYMENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(DealerPmtReconWrkDAL.COL_NAME_DATE_OF_PAYMENT), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(DealerPmtReconWrkDAL.COL_NAME_DATE_OF_PAYMENT, Value)
        End Set
    End Property



    Public Property DatePaidFor() As DateType
        Get
            CheckDeleted()
            If Row(DealerPmtReconWrkDAL.COL_NAME_DATE_PAID_FOR) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(DealerPmtReconWrkDAL.COL_NAME_DATE_PAID_FOR), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(DealerPmtReconWrkDAL.COL_NAME_DATE_PAID_FOR, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=20)> _
    Public Property CampaignNumber() As String
        Get
            CheckDeleted()
            If Row(DealerPmtReconWrkDAL.COL_NAME_CAMPAIGN_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerPmtReconWrkDAL.COL_NAME_CAMPAIGN_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerPmtReconWrkDAL.COL_NAME_CAMPAIGN_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=20)> _
    Public Property MembershipNumber() As String
        Get
            CheckDeleted()
            If Row(DealerPmtReconWrkDAL.COL_NAME_MEMBESHIP_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerPmtReconWrkDAL.COL_NAME_MEMBESHIP_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerPmtReconWrkDAL.COL_NAME_MEMBESHIP_NUMBER, Value)
        End Set
    End Property 

    <ValidStringLength("", Max:=20)> _
    Public Property ServiceLineNumber() As String
        Get
            CheckDeleted()
            If Row(DealerPmtReconWrkDAL.COL_NAME_SERVICE_LINE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerPmtReconWrkDAL.COL_NAME_SERVICE_LINE_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerPmtReconWrkDAL.COL_NAME_SERVICE_LINE_NUMBER, Value)
        End Set
    End Property 

    <ValidStringLength("", Max:=20)> _
    Public Property PaymentInvoiceNumber() As String
        Get
            CheckDeleted()
            If Row(DealerPmtReconWrkDAL.COL_NAME_PAYMENT_INVOICE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerPmtReconWrkDAL.COL_NAME_PAYMENT_INVOICE_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerPmtReconWrkDAL.COL_NAME_PAYMENT_INVOICE_NUMBER, Value)
        End Set
    End Property 

    Public Property CollectedAmount() As DecimalType
            Get
                CheckDeleted()
                If Row(DealerPmtReconWrkDAL.COL_NAME_COLLECTED_AMOUNT) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return New DecimalType(CType(Row(DealerPmtReconWrkDAL.COL_NAME_COLLECTED_AMOUNT), Decimal))
                End If
            End Get
            Set(ByVal Value As DecimalType)
                CheckDeleted()
                Me.SetValue(DealerPmtReconWrkDAL.COL_NAME_COLLECTED_AMOUNT, Value)
            End Set
        End Property

    <ValidStringLength("", Max:=20)> _
    Public Property Layout() As String
        Get
            CheckDeleted()
            If Row(DealerPmtReconWrkDAL.COL_NAME_LAYOUT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerPmtReconWrkDAL.COL_NAME_LAYOUT), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerPmtReconWrkDAL.COL_NAME_LAYOUT, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=5)> _
    Public Property ProductCode() As String
        Get
            CheckDeleted()
            If Row(DealerPmtReconWrkDAL.COL_NAME_PRODUCT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerPmtReconWrkDAL.COL_NAME_PRODUCT_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerPmtReconWrkDAL.COL_NAME_PRODUCT_CODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=20)> _
    Public Property NewProductCode() As String
        Get
            CheckDeleted()
            If Row(DealerPmtReconWrkDAL.COL_NAME_NEW_PRODUCT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DealerPmtReconWrkDAL.COL_NAME_NEW_PRODUCT_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DealerPmtReconWrkDAL.COL_NAME_NEW_PRODUCT_CODE, Value)
        End Set
    End Property

    Public Property AdjustmentAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(DealerPmtReconWrkDAL.COL_NAME_ADJUSTMENT_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DealerPmtReconWrkDAL.COL_NAME_ADJUSTMENT_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(DealerPmtReconWrkDAL.COL_NAME_ADJUSTMENT_AMOUNT, Value)
        End Set
    End Property

    Public Property InstallmentNum() As LongType
        Get
            CheckDeleted()
            If row(DealerPmtReconWrkDAL.COL_NAME_INSTALLMENT_NUM) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(DealerPmtReconWrkDAL.COL_NAME_INSTALLMENT_NUM), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(DealerPmtReconWrkDAL.COL_NAME_INSTALLMENT_NUM, Value)
        End Set
    End Property

    Public Property FeeIncome() As DecimalType
        Get
            CheckDeleted()
            If Row(DealerPmtReconWrkDAL.COL_NAME_FEE_INCOME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DealerPmtReconWrkDAL.COL_NAME_FEE_INCOME), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(DealerPmtReconWrkDAL.COL_NAME_FEE_INCOME, Value)
        End Set
    End Property
#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New DealerPmtReconWrkDAL
                dal.Update(Me.Row)
                'Reload the Data from the DB
                If Me.Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Me.Id
                    Me.Dataset = New Dataset
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

    Public Shared Function LoadList(ByVal dealerfileProcessedID As Guid, ByVal recordMode As String, ByVal parentFile As String) As DataView
        Try
            Dim dal As New DealerPmtReconWrkDAL
            Dim ds As DataSet

            ds = dal.LoadList(dealerfileProcessedID, ElitaPlusIdentity.Current.ActiveUser.LanguageId, recordMode, parentFile)
            Return (ds.Tables(DealerPmtReconWrkDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function LoadRejectList(ByVal dealerfileProcessedID As Guid) As DataView
        Try
            Dim dal As New DealerPmtReconWrkDAL
            Dim ds As DataSet

            ds = dal.LoadRejectList(dealerfileProcessedID)
            Return (ds.Tables(DealerPmtReconWrkDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

#End Region

End Class



