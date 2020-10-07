'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (4/20/2007)  ********************

Public Class CommissionEntity
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
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
    Public Sub New(ByVal id As Guid, ByVal familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load()
    End Sub

    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New CommissionEntityDAL
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

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New CommissionEntityDAL
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
    Private _isBankInfoNeedDeletion As Boolean
    Private _isNewBankInfo As Boolean
    Private _isNewWithCopy As Boolean
    Private _isDelete As Boolean
    Private _constrVoilation As Boolean
    Private _lastPaymentMethodId As Guid = Guid.Empty
#End Region


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If Row(CommissionEntityDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CommissionEntityDAL.COL_NAME_ENTITY_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=800)> _
    Public Property EntityName() As String
        Get
            CheckDeleted()
            If Row(CommissionEntityDAL.COL_NAME_ENTITY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CommissionEntityDAL.COL_NAME_ENTITY_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(CommissionEntityDAL.COL_NAME_ENTITY_NAME, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=240)> _
    Public Property Phone() As String
        Get
            CheckDeleted()
            If Row(CommissionEntityDAL.COL_NAME_PHONE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CommissionEntityDAL.COL_NAME_PHONE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(CommissionEntityDAL.COL_NAME_PHONE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=800), EmailAddress("")> _
    Public Property Email() As String
        Get
            CheckDeleted()
            If Row(CommissionEntityDAL.COL_NAME_EMAIL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CommissionEntityDAL.COL_NAME_EMAIL), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(CommissionEntityDAL.COL_NAME_EMAIL, Value)
        End Set
    End Property
    <ValueMandatory("")> _
       Public Property PaymentMethodId() As Guid
        Get
            CheckDeleted()
            If Row(CommissionEntityDAL.COL_NAME_PAYMENT_METHOD_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CommissionEntityDAL.COL_NAME_PAYMENT_METHOD_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(CommissionEntityDAL.COL_NAME_PAYMENT_METHOD_ID, Value)
        End Set
    End Property

    Public Property isBankInfoNeedDeletion() As Boolean
        Get
            Return _isBankInfoNeedDeletion
        End Get
        Set(ByVal Value As Boolean)
            _isBankInfoNeedDeletion = Value
        End Set
    End Property
    Public Property IsNewBankInfo() As Boolean
        Get
            Return _isNewBankInfo
        End Get
        Set(ByVal Value As Boolean)
            _isNewBankInfo = Value
        End Set
    End Property
    Public Property IsNewWithCopy() As Boolean
        Get
            Return _isNewWithCopy
        End Get
        Set(ByVal Value As Boolean)
            _isNewWithCopy = Value
        End Set
    End Property

    Public Property IsDelete() As Boolean
        Get
            Return _isDelete
        End Get
        Set(ByVal Value As Boolean)
            _isDelete = Value
        End Set
    End Property

    Public Property BankInfoId() As Guid
        Get
            CheckDeleted()
            If Row(CommissionEntityDAL.COL_NAME_BANK_INFO_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CommissionEntityDAL.COL_NAME_BANK_INFO_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(CommissionEntityDAL.COL_NAME_BANK_INFO_ID, Value)
        End Set
    End Property

    Public Property CompanyGroupId() As Guid
        Get
            CheckDeleted()
            If Row(CommissionEntityDAL.COL_NAME_COMPANY_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CommissionEntityDAL.COL_NAME_COMPANY_GROUP_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(CommissionEntityDAL.COL_NAME_COMPANY_GROUP_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=800)> _
    Public Property Address1() As String
        Get
            CheckDeleted()
            If Row(CommissionEntityDAL.COL_NAME_ADDRESS1) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CommissionEntityDAL.COL_NAME_ADDRESS1), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(CommissionEntityDAL.COL_NAME_ADDRESS1, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=800)> _
    Public Property Address2() As String
        Get
            CheckDeleted()
            If Row(CommissionEntityDAL.COL_NAME_ADDRESS2) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CommissionEntityDAL.COL_NAME_ADDRESS2), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(CommissionEntityDAL.COL_NAME_ADDRESS2, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=800)> _
    Public Property City() As String
        Get
            CheckDeleted()
            If Row(CommissionEntityDAL.COL_NAME_CITY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CommissionEntityDAL.COL_NAME_CITY), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(CommissionEntityDAL.COL_NAME_CITY, Value)
        End Set
    End Property

    Public Property RegionId() As Guid
        Get
            CheckDeleted()
            If Row(CommissionEntityDAL.COL_NAME_REGION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CommissionEntityDAL.COL_NAME_REGION_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(CommissionEntityDAL.COL_NAME_REGION_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=160)> _
    Public Property PostalCode() As String
        Get
            CheckDeleted()
            If Row(CommissionEntityDAL.COL_NAME_POSTAL_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CommissionEntityDAL.COL_NAME_POSTAL_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(CommissionEntityDAL.COL_NAME_POSTAL_CODE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property CountryId() As Guid
        Get
            CheckDeleted()
            If Row(CommissionEntityDAL.COL_NAME_COUNTRY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CommissionEntityDAL.COL_NAME_COUNTRY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(CommissionEntityDAL.COL_NAME_COUNTRY_ID, Value)
        End Set
    End Property

    Private Property LastPaymentMethodId() As Guid
        Get
            Return _lastPaymentMethodId
        End Get
        Set(ByVal Value As Guid)
            _lastPaymentMethodId = Value
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property DisplayId() As Guid
        Get
            CheckDeleted()
            If Row(CommissionEntityDAL.COL_NAME_DISPLAY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CommissionEntityDAL.COL_NAME_DISPLAY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(CommissionEntityDAL.COL_NAME_DISPLAY_ID, Value)
        End Set
    End Property

    Public Property ConstrVoilation() As Boolean
        Get
            Return _constrVoilation
        End Get
        Set(ByVal Value As Boolean)
            _constrVoilation = Value
        End Set
    End Property
    <ValidStringLength("", Max:=15)>
    Public Property TaxId() As String
        Get
            CheckDeleted()
            If Row(CommissionEntityDAL.COL_NAME_TAX_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CommissionEntityDAL.COL_NAME_TAX_ID), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(CommissionEntityDAL.COL_NAME_TAX_ID, Value)
        End Set
    End Property

    Public Property CommissionEntityTypeid() As Guid
        Get
            CheckDeleted()
            If Row(CommissionEntityDAL.COL_NAME_COMMISSION_ENTITY_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CommissionEntityDAL.COL_NAME_COMMISSION_ENTITY_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(CommissionEntityDAL.COL_NAME_COMMISSION_ENTITY_TYPE_ID, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Dim blnBankInfoSave As Boolean
        Try
            MyBase.Save()
            If Not IsDeleted Then LastPaymentMethodId = PaymentMethodId
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New CommissionEntityDAL '
                If LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENTMETHOD, LastPaymentMethodId) = Codes.PAYMENT_METHOD__BANK_TRANSFER Then
                    blnBankInfoSave = True
                    isBankInfoNeedDeletion = True
                Else
                    blnBankInfoSave = False
                    If Not IsNew AndAlso isBankInfoNeedDeletion Then
                        CurrentBankInfo.BeginEdit()
                        CurrentBankInfo.Delete()
                    End If
                End If
                dal.UpdateFamily(Dataset, , blnBankInfoSave) 'New Code Added Manually
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Id
                    Dataset = New DataSet
                    Row = Nothing
                    Load(objId)
                    _bankinfo = Nothing
                End If
            End If

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            CurrentBankInfo.cancelEdit()
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    Public Overrides ReadOnly Property IsDirty() As Boolean
        Get
            Return MyBase.IsDirty OrElse IsChildrenDirty OrElse _
            (Not CurrentBankInfo Is Nothing AndAlso (Not CurrentBankInfo.IsNew And CurrentBankInfo.IsDirty)) OrElse _
            (Not CurrentBankInfo Is Nothing AndAlso (CurrentBankInfo.IsNew And Not CurrentBankInfo.IsEmpty))
        End Get
    End Property

    Public Sub DeleteAndSave()
        CheckDeleted()
        Dim binfo As BankInfo = CurrentBankInfo
        BeginEdit()
        If Not binfo Is Nothing Then binfo.BeginEdit()

        Try
            LastPaymentMethodId = PaymentMethodId

            Delete()
            If Not binfo Is Nothing Then binfo.Delete()
            Save()

        Catch ex As Exception
            If ex.Message = "Integrity Constraint Violation" Then
                ConstrVoilation = True
            End If
            cancelEdit()
            If Not binfo Is Nothing Then binfo.cancelEdit()
            Throw ex
        End Try
    End Sub

    Public Sub Copy(ByVal original As CommissionEntity)
        If Not IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing commission entity")
        End If
        'Copy myself
        CopyFrom(original)
    End Sub
#End Region

#Region "DataView Retrieveing Methods"
#Region "CommissionEntitySearchDV"
    Public Class CommissionEntitySearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_COMMISSION_ENTITY_ID As String = "entity_id"
        Public Const COL_COMMISSION_ENTITY_NAME As String = "entity_name"
        Public Const COL_COMMISSION_ENTITY As String = "commission_entity"
        Public Const COL_COMMISSION_ENTITY_PHONE As String = "phone"
#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region

    Public Shared Function getList(ByVal descriptionMask As String, ByVal phoneMask As String, ByVal CompanyGroupIdMask As Guid) As CommissionEntitySearchDV
        Try
            Dim dal As New CommissionEntityDAL
            Dim compIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Return New CommissionEntitySearchDV(dal.LoadList(descriptionMask, phoneMask, CompanyGroupIdMask).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

#Region "BankInfo"

    Private _bankinfo As BankInfo = Nothing

    Public ReadOnly Property CurrentBankInfo() As BankInfo
        Get
            Return _bankinfo
        End Get
    End Property

    Public ReadOnly Property NewBankInfo() As BankInfo
        Get
            Return _bankinfo
        End Get
    End Property
    Public Function Add_BankInfo() As BankInfo

        If BankInfoId.Equals(Guid.Empty) Then
            _bankinfo = New BankInfo(Dataset)
        Else
            _bankinfo = New BankInfo(BankInfoId, Dataset)
        End If
        Return _bankinfo
    End Function

#End Region

#Region "Custom Validation"
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
Public NotInheritable Class EmailAddress
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_EMAIL_IS_INVALID_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As CommissionEntity = CType(objectToValidate, CommissionEntity)

            If obj.Email Is Nothing Then
                Return True
            End If

            Return MiscUtil.EmailAddressValidation(obj.Email)

        End Function

    End Class
#End Region
End Class



