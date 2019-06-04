Public Class BankInfo
    Inherits BusinessObjectBase

    'Public Interface IBankInfo
    '    Property BankInfoId() As Guid
    'End Interface

    '#Region "Private Attributes"
    '    Private _userObj As IBankInfo = Nothing
    '#End Region


#Region "Constructors"

    'Existing BO
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

    'Existing BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDS As DataSet)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load(id)
        'Me._userObj = userObj
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As DataSet)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load()
        'Me._userObj = userObj
    End Sub

    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Me.Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New BankInfoDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            'Initialize()
            SafeOriginalCopy()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New BankInfoDAL
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

            SafeOriginalCopy()

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Private Sub SafeOriginalCopy()

        ' we want keep the original IBAN number to validate the changes
        If Row(BankInfoDAL.COL_NAME_IBAN_NUMBER) Is DBNull.Value Then
            _OriginalIbanNumber = String.Empty
        Else
            _OriginalIbanNumber = CType(Row(BankInfoDAL.COL_NAME_IBAN_NUMBER), String)
        End If
    End Sub
#End Region

#Region "Private Members"
    'Private mCustomerCountryID As Guid
    Private _sourceCountryID As Guid
    Private _PaymentMethodId As Guid
    Private _PayeeId As Guid
    Private _domesticTransfer As Boolean
    Private _InternationalEUTransfer As Boolean
    Private _sepaEUBankTransfer As Boolean
    Private _InternationalTransfer As Boolean
    Private _BRValidation As Boolean
    Private _FRValidation As Boolean
    Private _OriginalIbanNumber As String = String.Empty
    Private _validateBankFields As Boolean = True
#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If Row(BankInfoDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(BankInfoDAL.COL_NAME_BANKINFO_ID), Byte()))
            End If
        End Get
    End Property
    <ValueMandatory("")>
    Public Property CountryID() As Guid
        Get
            CheckDeleted()
            If Row(BankInfoDAL.COL_NAME_COUNTRY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(BankInfoDAL.COL_NAME_COUNTRY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(BankInfoDAL.COL_NAME_COUNTRY_ID, Value)
        End Set
    End Property

    <ValidateAccountNameForCountries(""), ValidStringLength("", Max:=50)>
    Public Property Account_Name() As String
        Get
            CheckDeleted()
            If Row(BankInfoDAL.COL_NAME_ACCOUNT_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(BankInfoDAL.COL_NAME_ACCOUNT_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(BankInfoDAL.COL_NAME_ACCOUNT_NAME, Value)
        End Set
    End Property
    Public ReadOnly Property Account_Name_Obfuscated() As String
        Get
            If Row(BankInfoDAL.COL_NAME_ACCOUNT_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Dim accountName = CType(Row(BankInfoDAL.COL_NAME_ACCOUNT_NAME), String)

                If Not String.IsNullOrEmpty(accountName) AndAlso accountName.Trim.Length > 4 Then
                    Dim nAstericks = accountName.Trim.Length - 4
                    Dim filler = New String("*", nAstericks)
                    REM Return $"{accountName.Substring(0, 2)}{filler}{accountName.Trim.Substring(accountName.Trim.Length - 2)}"
                    Return String.Format("{0}{1}{2}", accountName.Substring(0, 2), filler, accountName.Trim.Substring(accountName.Trim.Length - 2))
                ElseIf Not String.IsNullOrEmpty(accountName) AndAlso accountName.Trim.Length > 1 Then
                    Return "****"
                End If

            End If
        End Get
    End Property

    <ValidBankIDLengthFromCountry("")>
    Public Property Bank_Id() As String
        Get
            CheckDeleted()
            If Row(BankInfoDAL.COL_NAME_BANK_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(BankInfoDAL.COL_NAME_BANK_ID), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(BankInfoDAL.COL_NAME_BANK_ID, Value)
        End Set
    End Property


    <ValidAcctNOLengthFromCountry(""), ValidStringLength("", Max:=29)>
    Public Property Account_Number() As String
        Get
            CheckDeleted()
            If Row(BankInfoDAL.COL_NAME_ACCOUNT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(BankInfoDAL.COL_NAME_ACCOUNT_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(BankInfoDAL.COL_NAME_ACCOUNT_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=40), ValidSwiftCode("")>
    Public Property SwiftCode() As String
        Get
            CheckDeleted()
            If Row(BankInfoDAL.COL_NAME_SWIFT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(BankInfoDAL.COL_NAME_SWIFT_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(BankInfoDAL.COL_NAME_SWIFT_CODE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=40), ValidIBAN_Number("")>
    Public Property IbanNumber() As String
        Get
            CheckDeleted()
            If Row(BankInfoDAL.COL_NAME_IBAN_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(BankInfoDAL.COL_NAME_IBAN_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(BankInfoDAL.COL_NAME_IBAN_NUMBER, Value)
        End Set
    End Property
    Public ReadOnly Property OriginalIbanNumber() As String
        Get
            Return Me._OriginalIbanNumber
        End Get
    End Property

    Public Sub CopyOriginalIbanNumber(value As String)
        Me._OriginalIbanNumber = value
    End Sub

    <ValidateAccountTypeForbrasil("")>
    Public Property AccountTypeId() As Guid
        Get
            CheckDeleted()
            If Row(BankInfoDAL.COL_NAME_ACCOUNT_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(BankInfoDAL.COL_NAME_ACCOUNT_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(BankInfoDAL.COL_NAME_ACCOUNT_TYPE_ID, Value)
        End Set
    End Property

    '*ANI
    Public Property PaymentReasonID() As Guid
        Get
            CheckDeleted()
            If Row(BankInfoDAL.COL_NAME_PAYMENT_REASON_ID) Is DBNull.Value Then

                Dim dv As DataView = LookupListNew.GetPaymentReasonLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                If Not dv Is Nothing AndAlso dv.Count > 0 Then
                    Return New Guid(CType(dv(0)(LookupListNew.COL_ID_NAME), Byte()))
                Else
                    Return Nothing
                End If

            Else
                Return New Guid(CType(Row(BankInfoDAL.COL_NAME_PAYMENT_REASON_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(BankInfoDAL.COL_NAME_PAYMENT_REASON_ID, Value)
        End Set
    End Property '*ANI

    <ValidStringLength("", Max:=200)>
    Public Property BranchName() As String
        Get
            CheckDeleted()
            If Row(BankInfoDAL.COL_NAME_BRANCH_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(BankInfoDAL.COL_NAME_BRANCH_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(BankInfoDAL.COL_NAME_BRANCH_NAME, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50), ValidateBankNameForbrasil("")>
    Public Property BankName() As String
        Get
            CheckDeleted()
            If Row(BankInfoDAL.COL_NAME_BANK_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(BankInfoDAL.COL_NAME_BANK_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(BankInfoDAL.COL_NAME_BANK_NAME, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=15)>
    Public Property BankSortCode() As String
        Get
            CheckDeleted()
            If Row(BankInfoDAL.COL_NAME_BANK_SORT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(BankInfoDAL.COL_NAME_BANK_SORT_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(BankInfoDAL.COL_NAME_BANK_SORT_CODE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=5)>
    Public Property BankSubCode() As String
        Get
            CheckDeleted()
            If Row(BankInfoDAL.COL_NAME_BANK_SUB_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(BankInfoDAL.COL_NAME_BANK_SUB_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(BankInfoDAL.COL_NAME_BANK_SUB_CODE, Value)
        End Set
    End Property

    <ValidAmount("")>
    Public Property TransactionLimit() As DecimalType
        Get
            CheckDeleted()
            If Row(BankInfoDAL.COL_NAME_TRANSACTION_LIMIT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(BankInfoDAL.COL_NAME_TRANSACTION_LIMIT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(BankInfoDAL.COL_NAME_TRANSACTION_LIMIT, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=15)>
    Public Property BankLookupCode() As String
        Get
            CheckDeleted()
            If Row(BankInfoDAL.COL_NAME_BANK_LOOKUP_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(BankInfoDAL.COL_NAME_BANK_LOOKUP_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(BankInfoDAL.COL_NAME_BANK_LOOKUP_CODE, Value)
        End Set
    End Property

    Public Property SourceCountryID() As Guid
        Get
            Return _sourceCountryID
        End Get
        Set(ByVal Value As Guid)
            _sourceCountryID = Value
        End Set
    End Property
    Public Property ValidateFieldsforBR() As Boolean
        Get
            Return _BRValidation
        End Get
        Set(ByVal Value As Boolean)
            _BRValidation = Value
        End Set
    End Property

    Public Property ValidateFieldsforFR() As Boolean
        Get
            Return _FRValidation
        End Get
        Set(ByVal Value As Boolean)
            _FRValidation = Value
        End Set
    End Property
    Public Property ValidateBankFields() As Boolean
        Get
            Return _validateBankFields
        End Get
        Set(ByVal Value As Boolean)
            _validateBankFields = Value
        End Set
    End Property

    <ValidDomesticTransfer("")>
    Public Property DomesticTransfer() As Boolean
        Get
            Return _domesticTransfer
        End Get
        Set(ByVal Value As Boolean)
            _domesticTransfer = Value
        End Set
    End Property

    <ValidInternationalTransfer("")>
    Public Property InternationalTransfer() As Boolean
        Get
            Return _InternationalTransfer
        End Get
        Set(ByVal Value As Boolean)
            _InternationalTransfer = Value
        End Set
    End Property

    <ValidInternationalEUTransfer("")>
    Public Property InternationalEUTransfer() As Boolean
        Get
            Return _InternationalEUTransfer
        End Get
        Set(ByVal Value As Boolean)
            _InternationalEUTransfer = Value
        End Set
    End Property

    <ValidInternationalEUTransfer("")>
    Public Property SepaEUBankTransfer() As Boolean
        Get
            Return _sepaEUBankTransfer
        End Get
        Set(ByVal Value As Boolean)
            _sepaEUBankTransfer = Value
        End Set
    End Property

    <ValidateBranchDigitForbrasil("")>
    Public Property BranchDigit As LongType
        Get
            CheckDeleted()
            If Row(BankInfoDAL.COL_NAME_BRANCH_DIGIT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(BankInfoDAL.COL_NAME_BRANCH_DIGIT), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(BankInfoDAL.COL_NAME_BRANCH_DIGIT, Value)
        End Set
    End Property


    <ValidateAccountDigitForbrasil("")>
    Public Property AccountDigit() As LongType
        Get
            CheckDeleted()
            If Row(BankInfoDAL.COL_NAME_ACCOUNT_DIGIT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(BankInfoDAL.COL_NAME_ACCOUNT_DIGIT), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(BankInfoDAL.COL_NAME_ACCOUNT_DIGIT, Value)
        End Set
    End Property

    <ValidateBranchNumberForbrasil("")>
    Public Property BranchNumber() As LongType
        Get
            CheckDeleted()
            If Row(BankInfoDAL.COL_NAME_BRANCH_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(BankInfoDAL.COL_NAME_BRANCH_NUMBER), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(BankInfoDAL.COL_NAME_BRANCH_NUMBER, Value)
        End Set
    End Property

    Public Property PaymentMethodId() As Guid
        Get
            Return _PaymentMethodId
        End Get
        Set(ByVal Value As Guid)
            _PaymentMethodId = Value
        End Set
    End Property

    Public Property PayeeId() As Guid
        Get
            Return _PayeeId
        End Get
        Set(ByVal Value As Guid)
            _PayeeId = Value
        End Set
    End Property

    <ValidStringLength("", Max:=15)>
    Public Property TaxId() As String
        Get
            CheckDeleted()
            If Row(BankInfoDAL.COL_NAME_TAX_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(BankInfoDAL.COL_NAME_TAX_ID), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(BankInfoDAL.COL_NAME_TAX_ID, Value)
        End Set
    End Property

    Public Property AddressId() As Guid
        Get
            CheckDeleted()
            If Row(BankInfoDAL.COL_NAME_ADDRESS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(BankInfoDAL.COL_NAME_ADDRESS_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(BankInfoDAL.COL_NAME_ADDRESS_ID, Value)
        End Set
    End Property


    Private _Address As Address = Nothing

    Public ReadOnly Property BankInfoAddress() As Address
        Get
            If Me._Address Is Nothing Then
                If Me.AddressId.Equals(Guid.Empty) Then
                    'If Me.IsNew Then
                    Me._Address = New Address(Me.Dataset, Nothing)
                    _Address.CountryId = Me.CountryID
                    '   Me.AddressId = Me._Address.Id
                    'End If
                Else
                    Me._Address = New Address(Me.AddressId, Me.Dataset, Nothing)
                End If
            End If
            Return Me._Address

        End Get
    End Property


    ' This field is a compute field, so It must be just for READONLY purpose
    Public Property IbanNumberLast4Digits() As String
        Get
            CheckDeleted()
            If Row(BankInfoDAL.COL_NAME_IBAN_NUMBER_LAST4DIGITS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(BankInfoDAL.COL_NAME_IBAN_NUMBER_LAST4DIGITS), String)
            End If
        End Get
        Private Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(BankInfoDAL.COL_NAME_IBAN_NUMBER_LAST4DIGITS, Value)
        End Set
    End Property

    Public Property Account_Number_Last4Digits() As String
        Get
            CheckDeleted()
            If Row(BankInfoDAL.COL_NAME_ACCOUNT_NUMBER_LAST4DIGITS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(BankInfoDAL.COL_NAME_ACCOUNT_NUMBER_LAST4DIGITS), String)
            End If
        End Get
        Private Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(BankInfoDAL.COL_NAME_ACCOUNT_NUMBER_LAST4DIGITS, Value)
        End Set
    End Property

#End Region

#Region "Custom Validation"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidBankIDLengthFromCountry
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_BANKID_LENGTH) 'INVALID_ACCTNO_LENGTH
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As BankInfo = CType(objectToValidate, BankInfo)
            Dim i As Integer

            If obj.CountryID.Equals(Guid.Empty) Then
                Return False
            End If

            Dim objCountry As New Country(obj.CountryID)

            If Not obj.SourceCountryID.Equals(obj.CountryID) Then
                If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, objCountry.EuropeanCountryId) = Codes.YESNO_Y Then
                    'international transfer --Bank ID is optional
                    Return True
                End If
            End If

            'MyBase.Message = Common.ErrorCodes.INVALID_BANKID_LENGTH
            
            'For i = 0 To obj.Bank_Id.Length - 1
            '    If Not IsNumeric(obj.Bank_Id.Chars(i)) Then
            '        MyBase.Message = Common.ErrorCodes.INVALID_BANKID
            '        Return False
            '    End If
            'Next
            If (LookupListNew.GetCodeFromId(LookupListNew.LK_VALIDATE_BANK_INFO, objCountry.ValidateBankInfoId) <> Codes.Country_Code_France) and obj.ValidateBankFields = True Then
                If obj.Bank_Id Is Nothing Then
                    MyBase.Message = Common.ErrorCodes.INVALID_BANKID_REQD
                    Return False
                Else
                    Dim _result As Integer = 0
                    If Int32.TryParse(obj.Bank_Id, _result) Then
                        obj.Bank_Id = obj.Bank_Id.ToString.Trim
                    Else
                        MyBase.Message = Common.ErrorCodes.INVALID_BANKID
                        Return False
                    End If
                    'obj.Bank_Id = obj.Bank_Id.ToString.Trim
                End If

                If (Not objCountry.BankIDLength Is Nothing) AndAlso objCountry.BankIDLength.Value > 0 Then
                    'ToString.Length > 0 Then
                    If (obj.Bank_Id.ToString.Trim.Length <> objCountry.BankIDLength.Value) Then
                        MyBase.Message = Common.ErrorCodes.INVALID_BANKID_LENGTH
                        Return False
                    Else
                        If LookupListNew.GetCodeFromId(LookupListNew.LK_VALIDATE_BANK_INFO, objCountry.ValidateBankInfoId) = Codes.Country_Code_Argentina Then
                            Dim BankId(6) As Integer, strConstant As String = "7139713"
                            Dim Total As Integer = 0, checkDigit As Integer
                            For i = 0 To 6
                                BankId(i) = CInt(Mid(obj.Bank_Id.ToString, i + 1, 1))
                                BankId(i) = BankId(i) * CInt(Mid(strConstant, i + 1, 1))
                                Total = Total + BankId(i)
                            Next
                            checkDigit = 10 - CInt(Total.ToString.Substring(Total.ToString.Length - 1, 1))
                            If checkDigit = 10 Then
                                checkDigit = 0
                            End If
                            If checkDigit <> CInt(obj.Bank_Id.ToString.Substring(obj.Bank_Id.ToString.Length - 1, 1)) Then
                                MyBase.Message = Common.ErrorCodes.POPULATE_BO_ERR
                                Return False
                            End If
                        End If
                    End If
                Else
                    If obj.Bank_Id.ToString.Trim.Length > 10 Then
                        MyBase.Message = Common.ErrorCodes.INVALID_BANKID_LENGTH
                        Return False
                    End If
                End If
            End If
            Return True
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidAcctNOLengthFromCountry
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_ACCTNO_LENGTH)
            'MyBase.Message = Common.ErrorCodes.INVALID_ACCTNO_LENGTH
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As BankInfo = CType(objectToValidate, BankInfo)
            Dim i As Integer
            If obj.CountryID.Equals(Guid.Empty) Then
                Return False
            End If

            Dim objCountry As New Country(obj.CountryID)

            If Not obj.SourceCountryID.Equals(obj.CountryID) Then

                If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, objCountry.EuropeanCountryId) = Codes.YESNO_Y Then
                    'international transfer --Bank Account # is optional
                    Return True
                End If
            End If

            If (LookupListNew.GetCodeFromId(LookupListNew.LK_VALIDATE_BANK_INFO, objCountry.ValidateBankInfoId) <> Codes.Country_Code_France) And obj.ValidateBankFields = True Then
                If obj.Account_Number Is Nothing Then
                    MyBase.Message = Common.ErrorCodes.INVALID_BANKACCNO_REQD
                    Return False
                Else
                    obj.Account_Number = obj.Account_Number.ToString.Trim
                End If

                'For i = 0 To obj.Account_Number.Length - 1
                '    If Not IsNumeric(obj.Account_Number.Chars(i)) Then
                '        MyBase.Message = Common.ErrorCodes.INVALID_ACCOUNT_NUMBER
                '        Return False
                '    End If
                'Next

                If (Not objCountry.BankAcctNoLength.ToString Is Nothing) AndAlso objCountry.BankAcctNoLength > 0 Then
                    'ToString.Length > 0 Then

                    If LookupListNew.GetCodeFromId(LookupListNew.LK_COUNTRIES, objCountry.Id) = Codes.Country_Code_Argentina Then
                        If (obj.Account_Number.ToString.Trim.Length <> objCountry.BankAcctNoLength) Then
                            MyBase.Message = Common.ErrorCodes.INVALID_ACCTNO_LENGTH
                            Return False
                        Else
                            If LookupListNew.GetCodeFromId(LookupListNew.LK_VALIDATE_BANK_INFO, objCountry.ValidateBankInfoId) = Codes.Country_Code_Argentina Then
                                Dim AccNumber(12) As Integer, strConstant As String = "39713971397139713"
                                Dim Total As Integer = 0, checkDigit As Integer
                                If System.Text.RegularExpressions.Regex.IsMatch(obj.Account_Number, "\D") Then
                                    MyBase.Message = Common.ErrorCodes.POPULATE_BO_ERR
                                    Return False
                                End If
                                For i = 0 To 12
                                    AccNumber(i) = CInt(Mid(obj.Account_Number.ToString, i + 1, 1))
                                    AccNumber(i) = AccNumber(i) * CInt(Mid(strConstant, i + 1, 1))
                                    Total = Total + AccNumber(i)
                                Next
                                checkDigit = 10 - CInt(Total.ToString.Substring(Total.ToString.Length - 1, 1))
                                If checkDigit = 10 Then
                                    checkDigit = 0
                                End If
                                If checkDigit <> CInt(obj.Account_Number.ToString.Substring(obj.Account_Number.ToString.Length - 1, 1)) Then
                                    MyBase.Message = Common.ErrorCodes.POPULATE_BO_ERR
                                    Return False
                                End If
                            End If
                        End If
                    Else
                        If (obj.Account_Number.ToString.Trim.Length > objCountry.BankAcctNoLength) Then
                            MyBase.Message = Common.ErrorCodes.INVALID_ACCTNO_LENGTH
                            Return False
                        End If
                    End If
                Else
                    If obj.Account_Number.ToString.Trim.Length > 29 Then
                        MyBase.Message = Common.ErrorCodes.INVALID_ACCTNO_LENGTH
                        Return False
                    End If
                End If
            End If

            Return True
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidSwiftCode
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_BANKSWIFTCODE_REQD)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As BankInfo = CType(objectToValidate, BankInfo)
            If obj.CountryID.Equals(Guid.Empty) Then
                Return True
            End If

            Dim objCountry As New Country(obj.CountryID)

            If obj.SourceCountryID.Equals(Guid.Empty) Then
                'Source unknown --Bank swift code is optional
                Return True
            End If

            If obj.SourceCountryID.Equals(obj.CountryID) Then
                'domestic transfer --Bank swift code is optional
                Return True
            End If

            If obj.SwiftCode Is Nothing Then
                MyBase.Message = Common.ErrorCodes.INVALID_BANKSWIFTCODE_REQD
                Return False
            End If

            Return True
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidDomesticTransfer
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_BANK_INFO)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As BankInfo = CType(objectToValidate, BankInfo)
            Dim retVal As Boolean = True
            Dim objCountry As New Country(obj.CountryID)
            If Not LookupListNew.GetCodeFromId(LookupListNew.LK_VALIDATE_BANK_INFO, objCountry.ValidateBankInfoId) = Codes.Country_Code_France Then
                If obj.DomesticTransfer Then
                    If (Not obj.SwiftCode Is Nothing Or obj.SwiftCode <> "") Or (Not obj.IbanNumber Is Nothing Or obj.IbanNumber <> "") Then
                        retVal = False
                    End If
                Else
                    If (Not obj.SourceCountryID.Equals(Guid.Empty)) And (Not obj.CountryID.Equals(Guid.Empty)) Then
                        If obj.SourceCountryID.Equals(obj.CountryID) Then
                            'Domestic transfer
                            If (Not obj.SwiftCode Is Nothing Or obj.SwiftCode <> "") Or (Not obj.IbanNumber Is Nothing Or obj.IbanNumber <> "") Then
                                retVal = False
                            End If
                        End If
                    Else
                        'Domestic transfer
                        If (Not obj.SwiftCode Is Nothing Or obj.SwiftCode <> "") Or (Not obj.IbanNumber Is Nothing Or obj.IbanNumber <> "") Then
                            retVal = False
                        End If
                    End If
                End If
            End If
            Return retVal

        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidInternationalTransfer
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_BANK_INFO)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As BankInfo = CType(objectToValidate, BankInfo)
            Dim retVal As Boolean = True

            If obj.InternationalTransfer Then
                If (Not obj.IbanNumber Is Nothing Or obj.IbanNumber <> "") Then
                    retVal = False
                End If
            Else
                If (Not obj.SourceCountryID.Equals(Guid.Empty)) And (Not obj.CountryID.Equals(Guid.Empty)) Then
                    If Not obj.SourceCountryID.Equals(obj.CountryID) Then
                        Dim objCountry As New Country(obj.CountryID)
                        If Not (LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, objCountry.EuropeanCountryId) = Codes.YESNO_Y) Then
                            'International transfer & Destination is not a European country
                            If (Not obj.IbanNumber Is Nothing Or obj.IbanNumber <> "") Then
                                retVal = False
                            End If
                        End If
                    End If
                End If
            End If

            Return retVal

        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidInternationalEUTransfer
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_BANK_INFO)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As BankInfo = CType(objectToValidate, BankInfo)
            Dim retVal As Boolean = True

            If obj.DomesticTransfer Then
            ElseIf obj.InternationalEUTransfer Then
                If (Not obj.Bank_Id Is Nothing) Or (Not obj.Account_Number Is Nothing) Then
                    retVal = False
                End If
            Else
                If (Not obj.SourceCountryID.Equals(Guid.Empty)) And (Not obj.CountryID.Equals(Guid.Empty)) Then
                    If Not obj.SourceCountryID.Equals(obj.CountryID) Then
                        Dim objCountry As New Country(obj.CountryID)
                        If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, objCountry.EuropeanCountryId) = Codes.YESNO_Y Then
                            'International transfer & Destination is European country
                            If (Not obj.Bank_Id Is Nothing) Or (Not obj.Account_Number Is Nothing) Then
                                retVal = False
                            End If
                        End If
                    End If
                End If
            End If

            Return retVal

        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidIBAN_Number
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_BANKIBANNO_REQD)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As BankInfo = CType(objectToValidate, BankInfo)
            If obj.CountryID.Equals(Guid.Empty) Then
                Return True
            End If

            Dim objCountry As New Country(obj.CountryID)

            If Not LookupListNew.GetCodeFromId(LookupListNew.LK_VALIDATE_BANK_INFO, objCountry.ValidateBankInfoId) = Codes.Country_Code_France Then
                If obj.SourceCountryID.Equals(obj.CountryID) Then
                    'domestic transfer --Bank swift code is optional
                    Return True
                Else
                    If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, objCountry.EuropeanCountryId) = Codes.YESNO_N Then
                        Return True
                    End If
                End If

                If obj.IbanNumber Is Nothing Then
                    MyBase.Message = Common.ErrorCodes.INVALID_BANKIBANNO_REQD
                    Return False
                End If
            Else

                If LookupListNew.GetCodeFromId(LookupListNew.LK_VALIDATE_BANK_INFO, objCountry.ValidateBankInfoId) = Codes.Country_Code_France Then

                    ' if the Iban number changed
                    If Not obj.OriginalIbanNumber.Equals(obj.IbanNumber) Then

                        If String.IsNullOrEmpty(obj.IbanNumber) Then
                            MyBase.Message = Common.ErrorCodes.INVALID_BANKIBANNO_INVALID
                            Return False
                        End If

                        Dim bankInfoDAL As BankInfoDAL = New BankInfoDAL
                        If Not bankInfoDAL.IbanIsValid(obj.IbanNumber, objCountry.Code) Then
                            MyBase.Message = Common.ErrorCodes.INVALID_BANKIBANNO_INVALID
                            Return False
                        End If
                    End If

                End If

                If (LookupListNew.GetCodeFromId(LookupListNew.LK_VALIDATE_BANK_INFO, objCountry.ValidateBankInfoId) = Codes.Country_Code_France And
                    LookupListNew.GetCodeFromId(LookupListNew.LK_PAYEE, obj.PayeeId) = ClaimInvoice.PAYEE_OPTION_CUSTOMER And
                    LookupListNew.GetCodeFromId(LookupListNew.LK_PAYMENTMETHOD, obj.PaymentMethodId) = Codes.PAYMENT_METHOD__BANK_TRANSFER) Then
                    If obj.IbanNumber Is Nothing Then
                        MyBase.Message = Common.ErrorCodes.INVALID_BANKIBANNO_REQD
                        Return False
                    End If
                End If
            End If
            Return True
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidAmount
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_TRANSACTION_LIMIT_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As BankInfo = CType(objectToValidate, BankInfo)
            If (Not obj.TransactionLimit Is Nothing) Then
                If obj.TransactionLimit.Value < 0 OrElse obj.TransactionLimit.Value > 999999999999999.99D Then
                    Return False
                End If
            End If
            Return True
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidateBranchDigitForbrasil
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_BRANCHDIGIT_LENGTH) 'INVALID_ACCTNO_LENGTH
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As BankInfo = CType(objectToValidate, BankInfo)
            Dim i As Integer

            If obj.CountryID.Equals(Guid.Empty) Then
                Return False
            End If

            Dim objCountry As New Country(obj.CountryID)
            If LookupListNew.GetCodeFromId(LookupListNew.LK_VALIDATE_BANK_INFO, objCountry.ValidateBankInfoId) = Codes.Country_Code_Brasil _
                And obj.ValidateFieldsforBR = True Then

                'Branch Digit
                If Not obj.IsEmptyNumber(obj.BranchDigit) Then

                    If obj.BranchDigit.ToString.Length > 1 Then
                        MyBase.Message = Common.ErrorCodes.INVALID_BRANCHDIGIT_LENGTH
                        Return False
                    End If

                    If Not IsNumeric(obj.BranchDigit.Value) Then
                        MyBase.Message = Common.ErrorCodes.GUI_BRANCHDIGIT_ONLY_NUMERIC_ALLOWED
                        Return False
                    End If
                Else

                    MyBase.Message = Common.ErrorCodes.ERR_BRANCHDIGIT_IS_REQUIRED
                    Return False
                End If

            End If

            Return True
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidateAccountDigitForbrasil
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_BRANCHDIGIT_LENGTH) 'INVALID_ACCTNO_LENGTH
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As BankInfo = CType(objectToValidate, BankInfo)
            Dim i As Integer

            If obj.CountryID.Equals(Guid.Empty) Then
                Return False
            End If

            Dim objCountry As New Country(obj.CountryID)
            If LookupListNew.GetCodeFromId(LookupListNew.LK_VALIDATE_BANK_INFO, objCountry.ValidateBankInfoId) = Codes.Country_Code_Brasil _
                 And obj.ValidateFieldsforBR = True Then

                'Account Digit
                If Not obj.IsEmptyNumber(obj.AccountDigit) Then

                    If obj.AccountDigit.ToString.Length > 1 Then
                        MyBase.Message = Common.ErrorCodes.INVALID_ACCOUNTDIGIT_LENGTH
                        Return False
                    End If

                    If Not IsNumeric(obj.AccountDigit.Value) Then
                        MyBase.Message = Common.ErrorCodes.GUI_ACCOUNTDIGIT_ONLY_NUMERIC_ALLOWED
                        Return False
                    End If
                Else
                    MyBase.Message = Common.ErrorCodes.ERR_ACCOUNTDIGIT_IS_REQUIRED
                    Return False
                End If

            End If

            Return True
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidateBranchNumberForbrasil
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_BRANCHDIGIT_LENGTH) 'INVALID_ACCTNO_LENGTH
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As BankInfo = CType(objectToValidate, BankInfo)
            Dim i As Integer

            If obj.CountryID.Equals(Guid.Empty) Then
                Return False
            End If

            Dim objCountry As New Country(obj.CountryID)
            If LookupListNew.GetCodeFromId(LookupListNew.LK_VALIDATE_BANK_INFO, objCountry.ValidateBankInfoId) = Codes.Country_Code_Brasil _
                 And obj.ValidateFieldsforBR = True Then

                'Branch Number
                If Not obj.IsEmptyNumber(obj.BranchNumber) Then

                    If obj.BranchNumber.ToString.Length > 4 Then
                        MyBase.Message = Common.ErrorCodes.INVALID_BRANCHNUMBER_LENGTH
                        Return False
                    End If

                    If Not IsNumeric(obj.BranchNumber.Value) Then
                        MyBase.Message = Common.ErrorCodes.GUI_BRANCHNUMBER_ONLY_NUMERIC_ALLOWED
                        Return False
                    End If
                Else
                    MyBase.Message = Common.ErrorCodes.ERR_BRANCHNUMBER_IS_REQUIRED
                    Return False
                End If
            End If


            Return True
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidateBankNameForbrasil
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_BRANCHDIGIT_LENGTH) 'INVALID_ACCTNO_LENGTH
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As BankInfo = CType(objectToValidate, BankInfo)
            Dim i As Integer

            If obj.CountryID.Equals(Guid.Empty) Then
                Return False
            End If

            Dim objCountry As New Country(obj.CountryID)
            If LookupListNew.GetCodeFromId(LookupListNew.LK_VALIDATE_BANK_INFO, objCountry.ValidateBankInfoId) = Codes.Country_Code_Brasil And obj.ValidateBankFields = True _
                 And obj.ValidateFieldsforBR = True Then

                'Bank Number
                If obj.IsEmptyString(obj.BankName) Then
                    MyBase.Message = Common.ErrorCodes.ERR_BANKNAME_IS_REQUIRED
                    Return False
                End If

            End If


            Return True
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidateBankIdForbrasil
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_BRANCHDIGIT_LENGTH) 'INVALID_ACCTNO_LENGTH
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As BankInfo = CType(objectToValidate, BankInfo)
            Dim i As Integer

            If obj.CountryID.Equals(Guid.Empty) Then
                Return False
            End If

            Dim objCountry As New Country(obj.CountryID)
            If LookupListNew.GetCodeFromId(LookupListNew.LK_VALIDATE_BANK_INFO, objCountry.ValidateBankInfoId) = Codes.Country_Code_Brasil _
                 And obj.ValidateFieldsforBR = True Then

                'Bank Id
                If obj.IsEmptyString(obj.Bank_Id) Then
                    MyBase.Message = Common.ErrorCodes.ERR_BANKID_IS_REQUIRED
                    Return False
                End If

                If obj.Bank_Id.ToString.Length > 3 Then
                    MyBase.Message = Common.ErrorCodes.INVALID_BANKID_LENGTH
                    Return False
                End If

            End If

            Return True
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidateAccountNumberForbrasil
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_BRANCHDIGIT_LENGTH) 'INVALID_ACCTNO_LENGTH
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As BankInfo = CType(objectToValidate, BankInfo)
            Dim i As Integer

            If obj.CountryID.Equals(Guid.Empty) Then
                Return False
            End If

            Dim objCountry As New Country(obj.CountryID)
            If LookupListNew.GetCodeFromId(LookupListNew.LK_VALIDATE_BANK_INFO, objCountry.ValidateBankInfoId) = Codes.Country_Code_Brasil _
                 And obj.ValidateFieldsforBR = True Then

                'Accout Number
                If obj.IsEmptyString(obj.Account_Number) Then
                    MyBase.Message = Common.ErrorCodes.ERR_MSG_ACCOUNT_NUMBER_IS_REQUIRED
                    Return False
                End If

                If obj.Account_Number.ToString.Length > 10 Then
                    MyBase.Message = Common.ErrorCodes.INVALID_ACCOUNT_NUMBER_LENGTH
                    Return False
                End If

            End If


            Return True
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidateAccountTypeForbrasil
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_BRANCHDIGIT_LENGTH) 'INVALID_ACCTNO_LENGTH
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As BankInfo = CType(objectToValidate, BankInfo)
            Dim i As Integer

            If obj.CountryID.Equals(Guid.Empty) Then
                Return False
            End If

            Dim objCountry As New Country(obj.CountryID)
            If LookupListNew.GetCodeFromId(LookupListNew.LK_VALIDATE_BANK_INFO, objCountry.ValidateBankInfoId) = Codes.Country_Code_Brasil _
                 And obj.ValidateFieldsforBR = True Then

                'Account Type
                If obj.AccountTypeId.Equals(Guid.Empty) Then
                    MyBase.Message = Common.ErrorCodes.ERR_ACCOUNTTYPE_IS_REQUIRED
                    Return False
                End If
            End If


            Return True
        End Function
    End Class



    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidateAccountNameForCountries
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_BRANCHDIGIT_LENGTH) 'INVALID_ACCTNO_LENGTH
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As BankInfo = CType(objectToValidate, BankInfo)
            Dim i As Integer

            If obj.CountryID.Equals(Guid.Empty) Then
                Return False
            End If

            Dim objCountry As New Country(obj.CountryID)
            If LookupListNew.GetCodeFromId(LookupListNew.LK_VALIDATE_BANK_INFO, objCountry.ValidateBankInfoId) = Codes.Country_Code_France Or obj.ValidateBankFields = False Then
                ' No run validation "Account Name" for French
                Return True
            Else
                If obj.Account_Name Is Nothing AndAlso obj.SepaEUBankTransfer = False Then
                    MyBase.Message = Common.ErrorCodes.INVALID_BANKACCNAME_REQD
                    Return False
                End If
            End If

            Return True
        End Function
    End Class



#End Region

#Region "Public Members"
    Public Overrides Sub Save()

        Dim addressObj As Address = Me.BankInfoAddress
        Try
            'If Me.IsDirty Then
            '    If Not Me._userObj Is Nothing Then
            '        If Me.IsNew And Me.IsEmpty Then
            '            Me._userObj.BankInfoId = Guid.Empty
            '            Me.Delete()
            '        End If
            '    End If
            'End If

            ' Move here from UserControlBankInfo.ascx.vb 02/08/2008
            If Not (Me.IsDeleted) Then SetPaymentReasonID()

            If Not addressObj Is Nothing Then
                addressObj.SourceId = Me.Id
            End If

            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New BankInfoDAL
                dal.Update(Me.Row)
                'Update Address for BankInfo
                dal.UpdateAddress(Me.Dataset)
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

    Public Overrides ReadOnly Property IsDirty() As Boolean
        Get
            Return MyBase.IsDirty OrElse Me.IsChildrenDirty OrElse
            (Not Me.BankInfoAddress.IsNew And Me.BankInfoAddress.IsDirty) OrElse
            (Me.BankInfoAddress.IsNew And Not Me.BankInfoAddress.IsEmpty)
        End Get
    End Property

    Public Sub SetPaymentReasonID()

        If Me.PaymentReasonID.Equals(System.Guid.Empty) Then
            Dim dv As DataView = LookupListNew.GetPaymentReasonLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            If Not dv Is Nothing AndAlso dv.Count > 0 Then
                Me.PaymentReasonID = New Guid(CType(dv(0)(LookupListNew.COL_ID_NAME), Byte()))
            End If
        End If

    End Sub

    Public ReadOnly Property IsEmpty() As Boolean
        Get
            If (Not IsEmptyString(Me.Account_Name)) OrElse (Not IsEmptyNumber(Me.Account_Number)) OrElse
            (Not IsEmptyNumber(Me.Bank_Id)) Then
                Return False
            End If
            Return True
        End Get
    End Property

    Private Function IsEmptyString(ByVal value As String)
        Return (value Is Nothing OrElse value.Trim.Length = 0)
    End Function

    Private Function IsEmptyNumber(ByVal value As LongType)
        Return (value Is Nothing OrElse value.ToString.Length = 0)
    End Function

#End Region

#Region "Shared Methods"

    Public Shared Sub SetProcessCancellationData(ByVal oBankInfoData As BankInfoData,
                                                 ByVal oBankInfo As BankInfo)
        With oBankInfoData
            .bankinfoId = oBankInfo.Id
            If Not oBankInfo.Account_Name Is Nothing Then
                .AccountName = oBankInfo.Account_Name
            End If
            If Not oBankInfo.Account_Number Is Nothing Then
                .AccountNumber = oBankInfo.Account_Number
            Else
                .AccountNumber = Nothing
            End If
            If Not oBankInfo.Bank_Id Is Nothing Then
                .BankID = oBankInfo.Bank_Id
            Else
                .BankID = Nothing
            End If
            If Not oBankInfo.SwiftCode Is Nothing Then
                .SwiftCode = oBankInfo.SwiftCode
            End If
            If Not oBankInfo.IbanNumber Is Nothing Then
                .IBAN_Number = oBankInfo.IbanNumber
            End If
            If Not IsNothing(oBankInfo.CountryID) Then
                .CountryId = oBankInfo.CountryID
            Else
                Dim ocountry As Country = ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID)
                .CountryId = ocountry.Id
            End If
            If Not IsNothing(oBankInfo.PaymentReasonID) Then
                .PaymentReasonId = oBankInfo.PaymentReasonID
            Else
                oBankInfo.SetPaymentReasonID()
                .PaymentReasonId = oBankInfo.PaymentReasonID
            End If
            If Not IsNothing(oBankInfo.AccountTypeId) Then
                .AccountTypeId = oBankInfo.AccountTypeId
            End If
            If Not oBankInfo.BankName Is Nothing Then
                .BankName = oBankInfo.BankName
            End If
            If Not oBankInfo.BranchDigit Is Nothing Then
                .BranchDigit = oBankInfo.BranchDigit
            End If
            If Not oBankInfo.AccountDigit Is Nothing Then
                .AccountDigit = oBankInfo.AccountDigit
            End If
            If Not oBankInfo.BranchNumber Is Nothing Then
                .BranchNumber = oBankInfo.BranchNumber
            End If
            '.AccountDigit = oBankInfo.AccountDigit
            '.BranchNumber = oBankInfo.BranchNumber
        End With
    End Sub

    Public Shared Sub DeleteNewChildBankInfo(ByVal parentCertInstallment As CertInstallment)
        Dim row As DataRow
        If parentCertInstallment.Dataset.Tables.IndexOf(BankInfoDAL.TABLE_NAME) >= 0 Then
            Dim rowIndex As Integer
            For rowIndex = 0 To parentCertInstallment.Dataset.Tables(BankInfoDAL.TABLE_NAME).Rows.Count - 1
                row = parentCertInstallment.Dataset.Tables(BankInfoDAL.TABLE_NAME).Rows.Item(rowIndex)
                If Not (row.RowState = DataRowState.Deleted) Or (row.RowState = DataRowState.Detached) Then
                    Dim bi As BankInfo = New BankInfo(row)
                    If parentCertInstallment.BankInfoId.Equals(bi.Id) And bi.IsNew Then
                        bi.Delete()
                    End If
                End If
            Next

        End If
    End Sub
#End Region

#Region "DataView Retrieveing Methods"

#End Region



End Class

