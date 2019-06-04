'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (10/30/2008)  ********************
Imports Assurant.ElitaPlus.Common
Imports Assurant.Common
Public Class CertInstallment
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid, Optional ByVal useCertId As Boolean = False)
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load(id, useCertId)
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
            Dim dal As New CertInstallmentDAL
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

    Protected Sub Load(ByVal id As Guid, Optional ByVal useCertId As Boolean = False)
        Try
            Dim dal As New CertInstallmentDAL
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

                dal.Load(Me.Dataset, id, useCertId)
                If useCertId Then
                    Me.Row = Me.FindRow(id, dal.COL_NAME_CERT_ID, Me.Dataset.Tables(dal.TABLE_NAME))
                Else
                    Me.Row = Me.FindRow(id, dal.TABLE_KEY_NAME, Me.Dataset.Tables(dal.TABLE_NAME))
                End If
            End If
            If Me.Row Is Nothing Then
                Throw New DataNotFoundException
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub SP_ChangeOfBillingStatus(ByVal statusId As Guid)
        Dim dal As New BillingDetailDAL

        _newPaymentDueDate = dal.ExecuteSP(statusId, Me.CertId)
        'If Not oErrMess Is Nothing Then
        '    Throw New ApplicationException("SP_ChangeOfBillingStatus falieu")
        'End If
    End Sub

    'REQ-5761
    Public Sub SP_ChngOfBillingStatus(ByVal statusId As Guid)
        Dim dal As New BillingPayDetailDAL
        _newPaymentDueDate = dal.ExecuteSP(statusId, Me.CertId)
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
            If row(CertInstallmentDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CertInstallmentDAL.COL_NAME_CERT_INSTALLMENT_ID), Byte()))
            End If
        End Get
    End Property


    Public Property BillingFrequencyId() As Guid
        Get
            CheckDeleted()
            If row(CertInstallmentDAL.COL_NAME_BILLING_FREQUENCY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CertInstallmentDAL.COL_NAME_BILLING_FREQUENCY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertInstallmentDAL.COL_NAME_BILLING_FREQUENCY_ID, Value)
        End Set
    End Property



    Public Property BillingStatusId() As Guid
        Get
            CheckDeleted()
            If row(CertInstallmentDAL.COL_NAME_BILLING_STATUS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CertInstallmentDAL.COL_NAME_BILLING_STATUS_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertInstallmentDAL.COL_NAME_BILLING_STATUS_ID, Value)
        End Set
    End Property



    Public Property BankInfoId() As Guid
        Get
            CheckDeleted()
            If row(CertInstallmentDAL.COL_NAME_BANK_INFO_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CertInstallmentDAL.COL_NAME_BANK_INFO_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertInstallmentDAL.COL_NAME_BANK_INFO_ID, Value)
        End Set
    End Property



    Public Property NumberOfInstallments() As LongType
        Get
            CheckDeleted()
            If row(CertInstallmentDAL.COL_NAME_NUMBER_OF_INSTALLMENTS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(CertInstallmentDAL.COL_NAME_NUMBER_OF_INSTALLMENTS), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(CertInstallmentDAL.COL_NAME_NUMBER_OF_INSTALLMENTS, Value)
        End Set
    End Property



    Public Property InstallmentAmount() As DecimalType
        Get
            CheckDeleted()
            If row(CertInstallmentDAL.COL_NAME_INSTALLMENT_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(CertInstallmentDAL.COL_NAME_INSTALLMENT_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertInstallmentDAL.COL_NAME_INSTALLMENT_AMOUNT, Value)
        End Set
    End Property



    Public Property PaymentDueDate() As DateType
        Get
            CheckDeleted()
            If row(CertInstallmentDAL.COL_NAME_PAYMENT_DUE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(CertInstallmentDAL.COL_NAME_PAYMENT_DUE_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(CertInstallmentDAL.COL_NAME_PAYMENT_DUE_DATE, Value)
        End Set
    End Property



    Public Property DateLetterSent() As DateType
        Get
            CheckDeleted()
            If row(CertInstallmentDAL.COL_NAME_DATE_LETTER_SENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(CertInstallmentDAL.COL_NAME_DATE_LETTER_SENT), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(CertInstallmentDAL.COL_NAME_DATE_LETTER_SENT, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property CertId() As Guid
        Get
            CheckDeleted()
            If row(CertInstallmentDAL.COL_NAME_CERT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CertInstallmentDAL.COL_NAME_CERT_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertInstallmentDAL.COL_NAME_CERT_ID, Value)
        End Set
    End Property

    Public Property SendLetterId() As Guid
        Get
            CheckDeleted()
            If Row(CertInstallmentDAL.COL_NAME_SEND_LETTER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertInstallmentDAL.COL_NAME_SEND_LETTER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertInstallmentDAL.COL_NAME_SEND_LETTER_ID, Value)
        End Set
    End Property

    Public Property CancellationDueDate() As DateType
        Get
            CheckDeleted()
            If Row(CertInstallmentDAL.COL_NAME_CANCELLATION_DUE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(CertInstallmentDAL.COL_NAME_CANCELLATION_DUE_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(CertInstallmentDAL.COL_NAME_CANCELLATION_DUE_DATE, Value)
        End Set
    End Property

    Public Property CreditCardInfoId() As Guid
        Get
            CheckDeleted()
            If row(CertInstallmentDAL.COL_NAME_CREDIT_CARD_INFO_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CertInstallmentDAL.COL_NAME_CREDIT_CARD_INFO_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertInstallmentDAL.COL_NAME_CREDIT_CARD_INFO_ID, Value)
        End Set
    End Property


    'Req - 1016 Start
    Public Property NextBillingDate() As DateType
        Get
            CheckDeleted()
            If Row(CertInstallmentDAL.COL_NAME_NEXT_BILLING_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(CertInstallmentDAL.COL_NAME_NEXT_BILLING_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(CertInstallmentDAL.COL_NAME_NEXT_BILLING_DATE, Value)
        End Set
    End Property
    'Req - 1016 End


    Private _bankinfo As BankInfo = Nothing
    Private _creditCardInfo As CreditCardInfo = Nothing

    Public ReadOnly Property BankInfo() As BankInfo
        Get
            Me._bankinfo = New BankInfo(Me.BankInfoId, Me.Dataset)
            Return Me._bankinfo

        End Get
    End Property

    Public ReadOnly Property CreditCardInfo() As CreditCardInfo
        Get
            If Me._creditCardInfo Is Nothing Then Me._creditCardInfo = New CreditCardInfo(Me.CreditCardInfoId, Me.Dataset)
            Return Me._creditCardInfo

        End Get
    End Property

    Private _currentBillingDetail As BillingDetail = Nothing
    Public ReadOnly Property CurrentBillingDetail() As BillingDetail
        Get
            If Me._currentBillingDetail Is Nothing Then Me._currentBillingDetail = New BillingDetail(Me.CertId, Me.Dataset, True)
            Return Me._currentBillingDetail

        End Get
    End Property

    'REQ-5761
    Private _currentBillingPayDetail As BillingPayDetail = Nothing

    Public ReadOnly Property CurrentBillingPaydetail() As BillingPayDetail
        Get
            If Me._currentBillingPayDetail Is Nothing Then Me._currentBillingPayDetail = New BillingPayDetail(Me.CertId, Me.Dataset, True)
            Return Me._currentBillingPayDetail
        End Get
    End Property

    Private _newPaymentDueDate As DateType
    Public Property newPaymentDueDate() As DateType
        Get
            Return _newPaymentDueDate
        End Get
        Set(ByVal Value As DateType)
            _newPaymentDueDate = Value
        End Set
    End Property


#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsFamilyDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New CertInstallmentDAL
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
#End Region

#Region "Children"
    Public Function AddCreditCardInfo(ByVal objCreditCardInfoID As Guid) As CreditCardInfo
        Dim objCreditCardInfo As CreditCardInfo
        CreditCardInfo.DeleteNewChildCreditCardInfo(Me)
        If objCreditCardInfoID.Equals(Guid.Empty) Then
            objCreditCardInfo = New CreditCardInfo(Me.Dataset)
        Else
            objCreditCardInfo = New CreditCardInfo(objCreditCardInfoID, Me.Dataset)
        End If

        Return objCreditCardInfo
    End Function

    Public Function AddBankInfo(ByVal objBankInfoID As Guid) As BankInfo
        Dim objBankInfo As BankInfo
        BankInfo.DeleteNewChildBankInfo(Me)
        If objBankInfoID.Equals(Guid.Empty) Then
            objBankInfo = New BankInfo(Me.Dataset)
        Else
            objBankInfo = New BankInfo(objBankInfoID, Me.Dataset)
        End If

        Return objBankInfo
    End Function
#End Region

#Region "DataView Retrieveing Methods"


#End Region


    End Class


