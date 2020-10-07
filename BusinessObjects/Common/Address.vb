'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (10/28/2004)  ********************

Public Class Address
    Inherits BusinessObjectBase

    Public Interface IAddressUser
        Property AddressId As Guid
    End Interface

#Region "Private Attributes"
    Private _userObj As IAddressUser = Nothing
    Private Const PropName_Region As String = "RegionId"
    Private Const PropName_Zip As String = "PostalCode"
    Private _profile_code As String
#End Region


#Region "Constructors"
    Private _dataSet As Data.DataSet
    Private _addressID As Guid

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
    Public Sub New(ByVal id As Guid, ByVal familyDS As DataSet, ByVal userObj As IAddressUser)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
        _userObj = userObj
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDS As DataSet, ByVal userObj As IAddressUser, ByVal Flg As Boolean)
        MyBase.New(Flg)
        Dataset = familyDS
        Load(id)
        _userObj = userObj
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As DataSet, ByVal userObj As IAddressUser)
        MyBase.New(False)
        Dataset = familyDS
        Load()
        _userObj = userObj
    End Sub

    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Sub New(ByVal familyDS As Data.DataSet)
        ' TODO: Complete member initialization 
        '_dataSet = dataSet
        MyBase.New(False)
        Dataset = familyDS
        Load()
    End Sub

    Sub New(ByVal addressid As Guid, ByVal dataset As Data.DataSet)
        ' todo: complete member initialization 

        MyBase.New(False)
        Me.Dataset = dataset
        Load(addressid)


        _addressID = addressid
        _dataSet = dataset
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New AddressDAL
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
            Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            'Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New AddressDAL
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
    'Private Sub Initialize()
    '    Me.SetValue(AddressDAL.COL_NAME_COUNTRY_ID, ElitaPlusIdentity.Current.ActiveUser.Company.BusinessCountryId)
    'End Sub
    Private _countryBO As Country
    Private _inforceFieldValidation As Boolean = False

    Public ReadOnly Property countryBO As Country
        Get
            If _countryBO Is Nothing Then
                If Not CountryId.Equals(Guid.Empty) Then
                    _countryBO = New Country(CountryId)
                Else
                    _countryBO = ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID)
                End If
            End If
            Return _countryBO
        End Get
    End Property

    Private Function GetMailingAddressLabel() As String
        Dim mailAddrLabel As String = ""

        'Dim countryBO As Country = New Country(Me.CountryId)
        Dim mailAddrFormat As String = countryBO.MailAddrFormat

        mailAddrLabel = mailAddrFormat
        mailAddrLabel = mailAddrLabel.Replace("[ADR1]", Address1)
        mailAddrLabel = mailAddrLabel.Replace("[ADR2]", Address2)
        mailAddrLabel = mailAddrLabel.Replace("[ADR3]", Address3)
        mailAddrLabel = mailAddrLabel.Replace("[CITY]", City)
        mailAddrLabel = mailAddrLabel.Replace("[COU]", countryBO.Description)
        mailAddrLabel = mailAddrLabel.Replace("[ZIP]", PostalCode)
        mailAddrLabel = mailAddrLabel.Replace("[Space]", " ")

        If Not RegionId.Equals(Guid.Empty) Then
            Dim regionBO As Region = New Region(RegionId)
            mailAddrLabel = mailAddrLabel.Replace("[RGNAME]", regionBO.Description)
            mailAddrLabel = mailAddrLabel.Replace("[RGCODE]", regionBO.ShortDesc)
        Else
            mailAddrLabel = mailAddrLabel.Replace("[RGNAME]", "")
            mailAddrLabel = mailAddrLabel.Replace("[RGCODE]", "")
        End If
        'remove all consecutive new line characters
        While mailAddrLabel.IndexOf("[\n][\n]") <> -1
            mailAddrLabel = mailAddrLabel.Replace("[\n][\n]", "[\n]")
        End While
        mailAddrLabel = mailAddrLabel.Replace("[\n]", Environment.NewLine)
        mailAddrLabel = mailAddrLabel.Replace("[", "")
        mailAddrLabel = mailAddrLabel.Replace("]", "")

        Return mailAddrLabel
    End Function
#End Region


#Region "Properties"



    Public Property ProfileCode As String
        Get
            Return _profile_code
        End Get

        Set
            _profile_code = value
        End Set
    End Property
    'Key Property
    Public ReadOnly Property Id As Guid
        Get
            If Row(AddressDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AddressDAL.COL_NAME_ADDRESS_ID), Byte()))
            End If
        End Get
    End Property

    <ValidStringLength("", Max:=100), MandatoryForVscAttribute(""), MandatoryForServCenterAttribute(""), MandatoryForDartyGiftCardAttribute("Address1"), RequiredFieldBySetting("", Nothing, "[ADR1]")>
    Public Property Address1 As String
        Get
            CheckDeleted()
            If Row(AddressDAL.COL_NAME_ADDRESS1) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AddressDAL.COL_NAME_ADDRESS1), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AddressDAL.COL_NAME_ADDRESS1, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=100), RequiredFieldBySetting("", Nothing, "[ADR2]")> _
    Public Property Address2 As String
        Get
            CheckDeleted()
            If Row(AddressDAL.COL_NAME_ADDRESS2) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AddressDAL.COL_NAME_ADDRESS2), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AddressDAL.COL_NAME_ADDRESS2, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=100), RequiredFieldBySetting("", Nothing, "[ADR3]")> _
    Public Property Address3 As String
        Get
            CheckDeleted()
            If Row(AddressDAL.COL_NAME_ADDRESS3) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AddressDAL.COL_NAME_ADDRESS3), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AddressDAL.COL_NAME_ADDRESS3, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50), MandatoryForVscAttribute(""), MandatoryForServCenterAttribute(""), MandatoryForDartyGiftCardAttribute("City"), RequiredFieldBySetting("", Nothing, "[CITY]")>
    Public Property City As String
        Get
            CheckDeleted()
            If Row(AddressDAL.COL_NAME_CITY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AddressDAL.COL_NAME_CITY), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AddressDAL.COL_NAME_CITY, Value)
        End Set
    End Property
    <MandatoryForVscAttribute(""), MandatoryCountryAddressFormatAttribute("RegionId"), RequiredFieldBySetting("", Nothing, "[REGION]")> _
    Public Property RegionId As Guid
        Get
            CheckDeleted()
            If Row(AddressDAL.COL_NAME_REGION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AddressDAL.COL_NAME_REGION_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AddressDAL.COL_NAME_REGION_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=25), MandatoryForVscAttribute(""), MandatoryCountryAddressFormatAttribute("PostalCode"), MandatoryForDartyGiftCardAttribute("PostalCode"), RequiredFieldBySetting("", Nothing, "[ZIP]")>
    Public Property PostalCode As String
        Get
            CheckDeleted()
            If Row(AddressDAL.COL_NAME_POSTAL_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AddressDAL.COL_NAME_POSTAL_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AddressDAL.COL_NAME_POSTAL_CODE, MiscUtil.ConvertToUpper(Value))
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property CountryId As Guid
        Get
            CheckDeleted()
            If Row(AddressDAL.COL_NAME_COUNTRY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AddressDAL.COL_NAME_COUNTRY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AddressDAL.COL_NAME_COUNTRY_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=25)> _
    Public Property ZipLocator As String
        Get
            CheckDeleted()
            If Row(AddressDAL.COL_NAME_ZIP_LOCATOR) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AddressDAL.COL_NAME_ZIP_LOCATOR), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AddressDAL.COL_NAME_ZIP_LOCATOR, Value)
        End Set
    End Property


    Public ReadOnly Property MailingAddressLabel As String
        Get
            Return GetMailingAddressLabel()
        End Get
    End Property


    Private _AddressIsRequire As Boolean = False
    Private _AddressRequiredServCenter As Boolean = False
    Private _PaymentMethodId As Guid
    Private _PayeeId As Guid

    Public Property AddressIsRequire As Boolean
        Get
            Return _AddressIsRequire
        End Get
        Set
            _AddressIsRequire = Value
        End Set
    End Property
    Public Property AddressRequiredServCenter As Boolean
        Get
            Return _AddressRequiredServCenter
        End Get
        Set
            _AddressRequiredServCenter = Value
        End Set
    End Property


    Public Property InforceFieldValidation As Boolean
        Get
            Return _inforceFieldValidation
        End Get
        Set
            _inforceFieldValidation = Value
        End Set
    End Property

    Public Property PaymentMethodId As Guid
        Get
            Return _PaymentMethodId
        End Get
        Set
            _PaymentMethodId = Value
        End Set
    End Property

    Public Property PayeeId As Guid
        Get
            Return _payeeId
        End Get
        Set
            _payeeId = Value
        End Set
    End Property

    <ValidStringLength("", Max:=10)>
    Public Property Source As String
        Get
            CheckDeleted()
            If Row(AddressDAL.COL_NAME_SOURCE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AddressDAL.COL_NAME_SOURCE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AddressDAL.COL_NAME_SOURCE, Value)
        End Set
    End Property

    Public Property SourceId As Guid
        Get
            CheckDeleted()
            If Row(AddressDAL.COL_NAME_SOURCE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AddressDAL.COL_NAME_SOURCE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AddressDAL.COL_NAME_SOURCE_ID, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            If IsDirty Then
                'Me.SetPostalCodeLocator()
                If Not _userObj Is Nothing Then
                    If IsNew And IsEmpty Then
                        _userObj.AddressId = Guid.Empty
                        Delete()
                    End If
                End If
            End If
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New AddressDAL
                dal.Update(Row)
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Id
                    Dataset = New Dataset
                    Row = Nothing
                    Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    'Public Sub SetPostalCodeLocator()
    '    If PostalCode = String.Empty Then Return
    '    Dim testValidator As PostalCodeValidator = New PostalCodeValidator(Me.CountryId, Me.PostalCode)
    '    Dim formatResult As PostalCodeFormatResult = testValidator.IsValid()
    '    If formatResult.IsValid Then
    '        Dim retStr As String
    '        If formatResult.ComunaEnabled.Trim = "N" Then
    '            retStr = formatResult.PostalCode.Substring(formatResult.LocatorStart - 1, formatResult.LocatorLength)
    '        Else
    '            retStr = formatResult.PostalCode
    '        End If
    '        ZipLocator = retStr
    '    Else
    '        Dim err As New ValidationError(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_POSTALCODEFORMAT_ERR, Me.GetType, Nothing, "PostalCode", Nothing)
    '        Throw New BOValidationException(New ValidationError() {err}, "Address", Me.UniqueId)
    '    End If
    'End Sub

    Public ReadOnly Property IsEmpty As Boolean
        Get
            If (Not IsEmptyString(Address1)) OrElse (Not IsEmptyString(Address2)) OrElse (Not IsEmptyString(Address3)) OrElse _
            (Not IsEmptyString(City)) OrElse (Not IsEmptyString(PostalCode)) OrElse _
            (Not RegionId.Equals(Guid.Empty)) Then
                Return False
            End If
            Return True
        End Get
    End Property

    Private Function IsEmptyString(ByVal value As String)
        Return (value Is Nothing OrElse value.Trim.Length = 0)
    End Function
#End Region

#Region "DataView Retrieveing Methods"

#End Region

#Region "Custom Validation"
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
Public NotInheritable Class MandatoryForVscAttribute
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Assurant.Common.Validation.Messages.VALUE_MANDATORY_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Address = CType(objectToValidate, Address)

            If obj.AddressIsRequire Then
                If TypeOf valueToCheck Is Guid Then
                    If valueToCheck.Equals(Guid.Empty) Then
                        Return False
                    End If
                Else
                    If valueToCheck Is Nothing Or valueToCheck Is String.Empty Then
                        Return False
                    End If
                End If
            End If

            Return True

        End Function
    End Class
#End Region


#Region "Custom Validation"
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
Public NotInheritable Class MandatoryForServCenterAttribute
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Assurant.Common.Validation.Messages.VALUE_MANDATORY_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Address = CType(objectToValidate, Address)

            If obj.AddressRequiredServCenter Then
                If TypeOf valueToCheck Is Guid Then
                    If valueToCheck.Equals(Guid.Empty) Then
                        Return False
                    End If
                Else
                    If valueToCheck Is Nothing Or valueToCheck Is String.Empty Then
                        Return False
                    End If
                End If
            End If

            Return True

        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class MandatoryCountryAddressFormatAttribute
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Assurant.Common.Validation.Messages.VALUE_MANDATORY_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Address = CType(objectToValidate, Address)

            If obj.AddressRequiredServCenter Then
                'If (Not TypeOf valueToCheck Is Guid) OrElse (valueToCheck.Equals(Guid.Empty)) Then
                '    Return True
                'End If

                If obj.CountryId = Guid.Empty Then Return True

                'Country is not empty, check required zip and region based on address format string
                Dim strAddFmt As String = New Country(obj.CountryId).MailAddrFormat.ToUpper

                'zip required
                'If PropName_Zip.ToUpper = Me.DisplayName.ToUpper AndAlso strAddFmt.IndexOf("[ZIP]") > -1 Then
                If PropName_Zip.ToUpper = DisplayName.ToUpper AndAlso IsAddressComponentRequired(strAddFmt, "ZIP") Then
                    If obj.PostalCode Is Nothing OrElse obj.PostalCode.Trim = String.Empty Then
                        Return False
                    End If
                End If
                'region required
                'If PropName_Region.ToUpper = Me.DisplayName.ToUpper AndAlso (strAddFmt.IndexOf("[RGCODE]") > -1 OrElse strAddFmt.IndexOf("[RGNAME]") > -1) Then
                If PropName_Region.ToUpper = DisplayName.ToUpper AndAlso (IsAddressComponentRequired(strAddFmt, "RGCODE") OrElse IsAddressComponentRequired(strAddFmt, "RGNAME")) Then
                    If obj.RegionId = Guid.Empty Then
                        Return False
                    End If
                End If
            End If
            Return True
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidUserCountry
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_USER_COUNTRY)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Address = CType(objectToValidate, Address)
            Dim valid As Boolean = False

            'ALR - REQ-400 - Check for mismatch between CountryID and Region.countryId.  If mismatched, set country to the region's country
            If Not obj.CountryId.Equals(Guid.Empty) AndAlso Not obj.RegionId.Equals(Guid.Empty) Then
                Dim oReg As New Region(obj.RegionId)
                If Not oReg Is Nothing Then
                    If Not oReg.CountryId.Equals(obj.CountryId) Then
                        obj.CountryId = oReg.CountryId
                    End If
                End If
            End If

            If obj.CountryId = Guid.Empty Then Return True

            Dim CompanyGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            Dim oCountryList As DataView

            oCountryList = LookupListNew.GetCompanyGroupCountryLookupList(CompanyGroupId)
            Dim index As Integer
            For index = 0 To oCountryList.Count - 1
                If (New Guid(CType(oCountryList(index)("id"), Byte()))) = (obj.CountryId) Then
                    Return True
                End If
            Next

            Return False
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class RequiredFieldBySetting
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String, ByVal x As String, ByVal shortName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR)
            propertyShortName = shortName
        End Sub

        Private _propertyShortName As String = Nothing
        Private Property propertyShortName As String
            Get
                Return _propertyShortName
            End Get
            Set
                _propertyShortName = Value
            End Set
        End Property

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Address = CType(objectToValidate, Address)

            If obj.InforceFieldValidation = False Then Return True

            If InStr(obj.countryBO.ContactInfoReqFields, propertyShortName) > 0 Then
                If valueToCheck Is Nothing OrElse valueToCheck.Equals(String.Empty) Then
                    Return False
                Else
                    Return True
                End If
            Else
                Return True
            End If

        End Function
    End Class
#End Region


#Region "Custom Validation"
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class MandatoryForDartyGiftCardAttribute
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Assurant.Common.Validation.Messages.VALUE_MANDATORY_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Address = CType(objectToValidate, Address)

            If (Not obj.PayeeId.Equals(Guid.Empty) And Not obj.PaymentMethodId.Equals(Guid.Empty)) Then
                If TypeOf valueToCheck Is Guid Then
                    If valueToCheck.Equals(Guid.Empty) Then
                        Return False
                    End If
                Else
                    If valueToCheck Is Nothing Or valueToCheck Is String.Empty Then
                        Return False
                    End If
                End If

            End If


            Return True

        End Function
    End Class
#End Region


#Region "Handle mailing address format"
    Public Class MailAddressItem
        Private _ItemName As String
        Private _Required As Boolean
        Public Sub New()
            _ItemName = String.Empty
            _Required = True
        End Sub
        Public Sub New(ByVal ItemName As String, Optional ByVal Required As Boolean = True)
            _ItemName = ItemName
            _Required = Required
        End Sub
        Public Property ItemName As String
            Get
                Return _ItemName
            End Get
            Set
                _ItemName = Value
            End Set
        End Property
        Public Property Required As Boolean
            Get
                Return _Required
            End Get
            Set
                Required = Value
            End Set
        End Property
    End Class
    Public Shared Sub SplitMailingAddressFormatString(ByVal MailAddrFmtStr As String, ByRef AddressComponents As Collections.Generic.List(Of MailAddressItem))
        MailAddrFmtStr = MailAddrFmtStr.Trim
        'MailAddrFmtStr = "[ADR1][-][\n][ADR2][\n][ZIP][Space][CITY][Space][COU][\n][RGNAME]*[,][Space][RGCODE]"
        If MailAddrFmtStr.Trim <> "" Then
            AddressComponents = New Collections.Generic.List(Of MailAddressItem)(15)
            Dim RegExp As Text.RegularExpressions.Regex, blnRequired As Boolean
            RegExp = New Text.RegularExpressions.Regex("\[(.)+?\](\*)*")
            Dim m As Text.RegularExpressions.Match = RegExp.Match(MailAddrFmtStr)
            While (m.Success)
                blnRequired = True
                If m.Value.Trim.EndsWith("]*") Then blnRequired = False
                AddressComponents.Add(New MailAddressItem(m.Value.Trim, blnRequired))
                m = m.NextMatch()
            End While
        End If
    End Sub
    Public Shared Function IsAddressComponentRequired(ByVal MailAddrFmtStr As String, ByVal strComponent As String) As Boolean
        Dim blnRequired As Boolean = False
        MailAddrFmtStr = MailAddrFmtStr.Trim
        If MailAddrFmtStr.Trim <> "" Then
            Dim RegExp As Text.RegularExpressions.Regex = New Text.RegularExpressions.Regex("\[(.+?)\](\*)*")
            Dim m As Text.RegularExpressions.Match = RegExp.Match(MailAddrFmtStr)
            While (m.Success)
                If m.Groups(1).Value.Trim = strComponent.Trim AndAlso (Not m.Value.Trim.EndsWith("]*")) Then
                    blnRequired = True
                    Exit While
                End If
                m = m.NextMatch()
            End While
        End If
        Return blnRequired
    End Function
#End Region


End Class


