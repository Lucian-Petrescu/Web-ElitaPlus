'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (10/30/2008)  ********************
Imports Assurant.ElitaPlus.Common
Imports Assurant.Common
Public Class CertInstallment
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(id As Guid, Optional ByVal useCertId As Boolean = False)
        MyBase.New()
        Dataset = New DataSet
        Load(id, useCertId)
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
            Dim dal As New CertInstallmentDAL
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

    Protected Sub Load(id As Guid, Optional ByVal useCertId As Boolean = False)
        Try
            Dim dal As New CertInstallmentDAL
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

                dal.Load(Dataset, id, useCertId)
                If useCertId Then
                    Row = FindRow(id, dal.COL_NAME_CERT_ID, Dataset.Tables(dal.TABLE_NAME))
                Else
                    Row = FindRow(id, dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_NAME))
                End If
            End If
            If Row Is Nothing Then
                Throw New DataNotFoundException
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub SP_ChangeOfBillingStatus(statusId As Guid)
        Dim dal As New BillingDetailDAL

        _newPaymentDueDate = dal.ExecuteSP(statusId, CertId)
        'If Not oErrMess Is Nothing Then
        '    Throw New ApplicationException("SP_ChangeOfBillingStatus falieu")
        'End If
    End Sub

    'REQ-5761
    Public Sub SP_ChngOfBillingStatus(statusId As Guid)
        Dim dal As New BillingPayDetailDAL
        _newPaymentDueDate = dal.ExecuteSP(statusId, CertId)
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
            If row(CertInstallmentDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CertInstallmentDAL.COL_NAME_CERT_INSTALLMENT_ID), Byte()))
            End If
        End Get
    End Property


    Public Property BillingFrequencyId As Guid
        Get
            CheckDeleted()
            If row(CertInstallmentDAL.COL_NAME_BILLING_FREQUENCY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CertInstallmentDAL.COL_NAME_BILLING_FREQUENCY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertInstallmentDAL.COL_NAME_BILLING_FREQUENCY_ID, Value)
        End Set
    End Property



    Public Property BillingStatusId As Guid
        Get
            CheckDeleted()
            If row(CertInstallmentDAL.COL_NAME_BILLING_STATUS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CertInstallmentDAL.COL_NAME_BILLING_STATUS_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertInstallmentDAL.COL_NAME_BILLING_STATUS_ID, Value)
        End Set
    End Property



    Public Property BankInfoId As Guid
        Get
            CheckDeleted()
            If row(CertInstallmentDAL.COL_NAME_BANK_INFO_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CertInstallmentDAL.COL_NAME_BANK_INFO_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertInstallmentDAL.COL_NAME_BANK_INFO_ID, Value)
        End Set
    End Property



    Public Property NumberOfInstallments As LongType
        Get
            CheckDeleted()
            If row(CertInstallmentDAL.COL_NAME_NUMBER_OF_INSTALLMENTS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(CertInstallmentDAL.COL_NAME_NUMBER_OF_INSTALLMENTS), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertInstallmentDAL.COL_NAME_NUMBER_OF_INSTALLMENTS, Value)
        End Set
    End Property



    Public Property InstallmentAmount As DecimalType
        Get
            CheckDeleted()
            If row(CertInstallmentDAL.COL_NAME_INSTALLMENT_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(CertInstallmentDAL.COL_NAME_INSTALLMENT_AMOUNT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertInstallmentDAL.COL_NAME_INSTALLMENT_AMOUNT, Value)
        End Set
    End Property



    Public Property PaymentDueDate As DateType
        Get
            CheckDeleted()
            If row(CertInstallmentDAL.COL_NAME_PAYMENT_DUE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(CertInstallmentDAL.COL_NAME_PAYMENT_DUE_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertInstallmentDAL.COL_NAME_PAYMENT_DUE_DATE, Value)
        End Set
    End Property



    Public Property DateLetterSent As DateType
        Get
            CheckDeleted()
            If row(CertInstallmentDAL.COL_NAME_DATE_LETTER_SENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(CertInstallmentDAL.COL_NAME_DATE_LETTER_SENT), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertInstallmentDAL.COL_NAME_DATE_LETTER_SENT, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property CertId As Guid
        Get
            CheckDeleted()
            If row(CertInstallmentDAL.COL_NAME_CERT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CertInstallmentDAL.COL_NAME_CERT_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertInstallmentDAL.COL_NAME_CERT_ID, Value)
        End Set
    End Property

    Public Property SendLetterId As Guid
        Get
            CheckDeleted()
            If Row(CertInstallmentDAL.COL_NAME_SEND_LETTER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertInstallmentDAL.COL_NAME_SEND_LETTER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertInstallmentDAL.COL_NAME_SEND_LETTER_ID, Value)
        End Set
    End Property

    Public Property CancellationDueDate As DateType
        Get
            CheckDeleted()
            If Row(CertInstallmentDAL.COL_NAME_CANCELLATION_DUE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(CertInstallmentDAL.COL_NAME_CANCELLATION_DUE_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertInstallmentDAL.COL_NAME_CANCELLATION_DUE_DATE, Value)
        End Set
    End Property

    Public Property CreditCardInfoId As Guid
        Get
            CheckDeleted()
            If row(CertInstallmentDAL.COL_NAME_CREDIT_CARD_INFO_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CertInstallmentDAL.COL_NAME_CREDIT_CARD_INFO_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertInstallmentDAL.COL_NAME_CREDIT_CARD_INFO_ID, Value)
        End Set
    End Property


    'Req - 1016 Start
    Public Property NextBillingDate As DateType
        Get
            CheckDeleted()
            If Row(CertInstallmentDAL.COL_NAME_NEXT_BILLING_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(CertInstallmentDAL.COL_NAME_NEXT_BILLING_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertInstallmentDAL.COL_NAME_NEXT_BILLING_DATE, Value)
        End Set
    End Property
    'Req - 1016 End


    Private _bankinfo As BankInfo = Nothing
    Private _creditCardInfo As CreditCardInfo = Nothing

    Public ReadOnly Property BankInfo As BankInfo
        Get
            _bankinfo = New BankInfo(BankInfoId, Dataset)
            Return _bankinfo

        End Get
    End Property

    Public ReadOnly Property CreditCardInfo As CreditCardInfo
        Get
            If _creditCardInfo Is Nothing Then _creditCardInfo = New CreditCardInfo(CreditCardInfoId, Dataset)
            Return _creditCardInfo

        End Get
    End Property

    Private _currentBillingDetail As BillingDetail = Nothing
    Public ReadOnly Property CurrentBillingDetail As BillingDetail
        Get
            If _currentBillingDetail Is Nothing Then _currentBillingDetail = New BillingDetail(CertId, Dataset, True)
            Return _currentBillingDetail

        End Get
    End Property

    'REQ-5761
    Private _currentBillingPayDetail As BillingPayDetail = Nothing

    Public ReadOnly Property CurrentBillingPaydetail As BillingPayDetail
        Get
            If _currentBillingPayDetail Is Nothing Then _currentBillingPayDetail = New BillingPayDetail(CertId, Dataset, True)
            Return _currentBillingPayDetail
        End Get
    End Property

    Private _newPaymentDueDate As DateType
    Public Property newPaymentDueDate As DateType
        Get
            Return _newPaymentDueDate
        End Get
        Set
            _newPaymentDueDate = Value
        End Set
    End Property


#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsFamilyDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New CertInstallmentDAL
                dal.UpdateFamily(Dataset)
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

#Region "Children"
    Public Function AddCreditCardInfo(objCreditCardInfoID As Guid) As CreditCardInfo
        Dim objCreditCardInfo As CreditCardInfo
        CreditCardInfo.DeleteNewChildCreditCardInfo(Me)
        If objCreditCardInfoID.Equals(Guid.Empty) Then
            objCreditCardInfo = New CreditCardInfo(Dataset)
        Else
            objCreditCardInfo = New CreditCardInfo(objCreditCardInfoID, Dataset)
        End If

        Return objCreditCardInfo
    End Function

    Public Function AddBankInfo(objBankInfoID As Guid) As BankInfo
        Dim objBankInfo As BankInfo
        BankInfo.DeleteNewChildBankInfo(Me)
        If objBankInfoID.Equals(Guid.Empty) Then
            objBankInfo = New BankInfo(Dataset)
        Else
            objBankInfo = New BankInfo(objBankInfoID, Dataset)
        End If

        Return objBankInfo
    End Function
#End Region

#Region "DataView Retrieveing Methods"


#End Region


    End Class


