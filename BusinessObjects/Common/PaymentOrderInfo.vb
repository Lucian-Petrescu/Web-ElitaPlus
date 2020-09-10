Public Class PaymentOrderInfo
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
        Me.Dataset = New Dataset
        Me.Load(id)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Me.Dataset = New Dataset
        Me.Load()
    End Sub

    'Existing BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDS As Dataset)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load(id)
        'Me._userObj = userObj
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As Dataset)
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
            setvalue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            'Initialize()
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
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

#End Region

#Region "Private Members"
    'Private mCustomerCountryID As Guid
    Private _sourceCountryID As Guid
    Private _PaymentMethodId As Guid
    Private _domesticTransfer As Boolean
    Private _InternationalEUTransfer As Boolean
    Private _InternationalTransfer As Boolean
    Private _BRValidation As Boolean
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
    <ValueMandatory("")> _
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

    <ValueMandatory(""), ValidStringLength("", Max:=50)> _
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


    <ValueMandatory(""), ValidBankIDLengthFromCountry("")> _
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
    
    <ValueMandatory(""), ValidStringLength("", Max:=50)> _
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

    Public Property PaymentMethodId() As Guid
        Get
            Return _PaymentMethodId
        End Get
        Set(ByVal Value As Guid)
            _PaymentMethodId = Value
        End Set
    End Property
#End Region

#Region "Custom Validation"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidBankIDLengthFromCountry
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_BANKID_LENGTH) 'INVALID_ACCTNO_LENGTH
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As PaymentOrderInfo = CType(objectToValidate, PaymentOrderInfo)
            Dim i As Integer

            If obj.CountryID.Equals(Guid.Empty) Then
                Return False
            End If
            Dim objCountry As New Country(obj.CountryID)
            If obj.Bank_Id Is Nothing Then            
                Return False
            Else
                obj.Bank_Id = obj.Bank_Id.ToString.Trim
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
                            'BankId(i) = CInt(Mid(obj.Bank_Id.ToString, i + 1, 1))
                            BankId(i) = CInt(Mid(obj.Bank_Id.ToString, i + 1, 1)) * CInt(Mid(strConstant, i + 1, 1))
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
            Return True
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidateBankNameForbrasil
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_BRANCHDIGIT_LENGTH) 'INVALID_ACCTNO_LENGTH
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As PaymentOrderInfo = CType(objectToValidate, PaymentOrderInfo)
            Dim i As Integer

            If obj.CountryID.Equals(Guid.Empty) Then
                Return False
            End If

            Dim objCountry As New Country(obj.CountryID)
            If LookupListNew.GetCodeFromId(LookupListNew.LK_VALIDATE_BANK_INFO, objCountry.ValidateBankInfoId) = Codes.Country_Code_Brasil _
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

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidateBankIdForbrasil
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_BRANCHDIGIT_LENGTH) 'INVALID_ACCTNO_LENGTH
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As PaymentOrderInfo = CType(objectToValidate, PaymentOrderInfo)
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

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try           
            'If Not (Me.IsDeleted) Then SetPaymentReasonID()

            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New BankInfoDAL
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

    Private Function IsEmptyString(ByVal value As String)
        Return (value Is Nothing OrElse value.Trim.Length = 0)
    End Function

#End Region

#Region "Shared Methods"

    Public Shared Sub SetProcessCancellationData(ByVal oBankInfoData As BankInfoData, _
                                              ByVal oPaymentOrderInfo As PaymentOrderInfo)
        With oBankInfoData
            .bankinfoId = oPaymentOrderInfo.Id
            If Not oPaymentOrderInfo.Account_Name Is Nothing Then
                .AccountName = oPaymentOrderInfo.Account_Name
            End If            
            If Not oPaymentOrderInfo.Bank_Id Is Nothing Then
                .BankID = oPaymentOrderInfo.Bank_Id
            Else
                .BankID = Nothing
            End If                        
            If Not IsNothing(oPaymentOrderInfo.CountryID) Then
                .CountryId = oPaymentOrderInfo.CountryID
            Else
                Dim ocountry As Country = ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID)
                .CountryId = ocountry.Id
            End If            
            If Not oPaymentOrderInfo.BankName Is Nothing Then
                .BankName = oPaymentOrderInfo.BankName
            End If            
        End With
    End Sub

#End Region

#Region "DataView Retrieveing Methods"

#End Region

End Class

