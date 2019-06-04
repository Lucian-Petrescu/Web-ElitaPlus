'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (4/20/2007)  ********************

Public Class CommissionEntity
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
            Dim dal As New CommissionEntityDAL
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
            Dim dal As New CommissionEntityDAL
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
            Me.SetValue(CommissionEntityDAL.COL_NAME_ENTITY_NAME, Value)
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
            Me.SetValue(CommissionEntityDAL.COL_NAME_PHONE, Value)
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
            Me.SetValue(CommissionEntityDAL.COL_NAME_EMAIL, Value)
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
            Me.SetValue(CommissionEntityDAL.COL_NAME_PAYMENT_METHOD_ID, Value)
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
            Me.SetValue(CommissionEntityDAL.COL_NAME_BANK_INFO_ID, Value)
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
            Me.SetValue(CommissionEntityDAL.COL_NAME_COMPANY_GROUP_ID, Value)
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
            Me.SetValue(CommissionEntityDAL.COL_NAME_ADDRESS1, Value)
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
            Me.SetValue(CommissionEntityDAL.COL_NAME_ADDRESS2, Value)
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
            Me.SetValue(CommissionEntityDAL.COL_NAME_CITY, Value)
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
            Me.SetValue(CommissionEntityDAL.COL_NAME_REGION_ID, Value)
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
            Me.SetValue(CommissionEntityDAL.COL_NAME_POSTAL_CODE, Value)
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
            Me.SetValue(CommissionEntityDAL.COL_NAME_COUNTRY_ID, Value)
        End Set
    End Property

    Private Property LastPaymentMethodId() As Guid
        Get
            Return _lastPaymentMethodId
        End Get
        Set(ByVal Value As Guid)
            Me._lastPaymentMethodId = Value
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
            Me.SetValue(CommissionEntityDAL.COL_NAME_DISPLAY_ID, Value)
        End Set
    End Property

    Public Property ConstrVoilation() As Boolean
        Get
            Return _constrVoilation
        End Get
        Set(ByVal Value As Boolean)
            Me._constrVoilation = Value
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
            Me.SetValue(CommissionEntityDAL.COL_NAME_TAX_ID, Value)
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
            Me.SetValue(CommissionEntityDAL.COL_NAME_COMMISSION_ENTITY_TYPE_ID, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Dim blnBankInfoSave As Boolean
        Try
            MyBase.Save()
            If Not Me.IsDeleted Then Me.LastPaymentMethodId = Me.PaymentMethodId
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New CommissionEntityDAL '
                If LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENTMETHOD, Me.LastPaymentMethodId) = Codes.PAYMENT_METHOD__BANK_TRANSFER Then
                    blnBankInfoSave = True
                    Me.isBankInfoNeedDeletion = True
                Else
                    blnBankInfoSave = False
                    If Not Me.IsNew AndAlso Me.isBankInfoNeedDeletion Then
                        Me.CurrentBankInfo.BeginEdit()
                        Me.CurrentBankInfo.Delete()
                    End If
                End If
                dal.UpdateFamily(Me.Dataset, , blnBankInfoSave) 'New Code Added Manually
                'Reload the Data from the DB
                If Me.Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Me.Id
                    Me.Dataset = New DataSet
                    Me.Row = Nothing
                    Me.Load(objId)
                    Me._bankinfo = Nothing
                End If
            End If

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Me.CurrentBankInfo.cancelEdit()
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    Public Overrides ReadOnly Property IsDirty() As Boolean
        Get
            Return MyBase.IsDirty OrElse Me.IsChildrenDirty OrElse _
            (Not Me.CurrentBankInfo Is Nothing AndAlso (Not Me.CurrentBankInfo.IsNew And Me.CurrentBankInfo.IsDirty)) OrElse _
            (Not Me.CurrentBankInfo Is Nothing AndAlso (Me.CurrentBankInfo.IsNew And Not Me.CurrentBankInfo.IsEmpty))
        End Get
    End Property

    Public Sub DeleteAndSave()
        Me.CheckDeleted()
        Dim binfo As BankInfo = Me.CurrentBankInfo
        Me.BeginEdit()
        If Not binfo Is Nothing Then binfo.BeginEdit()

        Try
            Me.LastPaymentMethodId = Me.PaymentMethodId

            Me.Delete()
            If Not binfo Is Nothing Then binfo.Delete()
            Me.Save()

        Catch ex As Exception
            If ex.Message = "Integrity Constraint Violation" Then
                Me.ConstrVoilation = True
            End If
            Me.cancelEdit()
            If Not binfo Is Nothing Then binfo.cancelEdit()
            Throw ex
        End Try
    End Sub

    Public Sub Copy(ByVal original As CommissionEntity)
        If Not Me.IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing commission entity")
        End If
        'Copy myself
        Me.CopyFrom(original)
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

        If Me.BankInfoId.Equals(Guid.Empty) Then
            _bankinfo = New BankInfo(Me.Dataset)
        Else
            _bankinfo = New BankInfo(Me.BankInfoId, Me.Dataset)
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



