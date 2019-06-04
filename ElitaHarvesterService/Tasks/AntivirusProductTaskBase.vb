Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports Assurant.ElitaPlus.Common
Imports Assurant.ElitaPlus.DALObjects

Public MustInherit Class AntivirusProductTaskBase
    Inherits TaskBase

#Region "Constants"
    Protected Const DEVICE_TYPE__TABLET As String = "TABLET"
    Protected Const DEVICE_TYPE__PHONE As String = "PHONE"
#End Region

#Region "Constructors"
    Public Sub New(ByVal machineName As String, ByVal processThreadName As String)
        MyBase.New(machineName, processThreadName)
    End Sub
#End Region

#Region "Private Methods"

    Private Function GetProductCode() As String
        Dim dealerCode As String = Me.Dealer.Dealer
        Dim productCode As String = String.Empty

        'TODO more generic implementation to find Product codes.
        Select Case dealerCode
            Case "TIMS"
                Select Case Me.EquipmentType
                    Case Codes.EQUIPMENT_TYPE__SMARTPHONE
                        productCode = Codes.MCAFEE_PROD_CODE_PHONE
                    Case Codes.EQUIPMENT_TYPE__TABLET
                        productCode = Codes.MCAFEE_PROD_CODE_TABLET
                    Case Else
                        Logger.AddError(String.Format("McAfee Product Code not configured for Equipment Type : {0}", Me.EquipmentType))
                        Me.FailReason = String.Format("McAfee Product Code not configured for Equipment Type : {0}", Me.EquipmentType)
                        Throw New NotImplementedException()
                End Select
            Case Else
                Logger.AddError(String.Format("Task not configured for dealer code  : {0}", dealerCode))
                Me.FailReason = String.Format("McAfee Product Code not configured for Equipment Type : {0}", Me.EquipmentType)
                Throw New NotImplementedException()
        End Select

        Return productCode

    End Function

    Private Function GetDeviceType() As String
        Dim equipmentId As Guid
        Dim eqp As Equipment
        equipmentId = Equipment.FindEquipment(Me.Dealer.Dealer, Me.CustomerItem.Make, Me.CustomerItem.Model, Me.CustomerItem.RegistrationDate.Value)
        If (equipmentId = Nothing) Then
            Dim str As String = String.Format("Unable to find Equipment Id for Make:{0}, Model:{1},LookUpDate:{2}  for Customer Registration Item Id : {3}", Me.CustomerItem.Make, Me.CustomerItem.Model, Me.CustomerItem.RegistrationDate, GuidControl.GuidToHexString(Me.CustomerItem.Id))
            Logger.AddError(str)
            Me.FailReason = str
            Throw New ArgumentException(str)
        End If
        eqp = New Equipment(equipmentId)
        Return LookupListNew.GetCodeFromId(LookupListNew.LK_EQUIPMENT_TYPE, eqp.EquipmentTypeId)
    End Function

    Private Sub InitializeRegistrationInformation()
        If _isInitialized Then Exit Sub
        Dim custRegistrationId As Guid
        Dim custRegistrationItemId As Guid
        Dim certificateId As Guid
        ' Check if Registration ID is supplied
        If (Not String.IsNullOrEmpty(MyBase.PublishedTask(PublishedTask.REGISTRATION_ID))) Then
            custRegistrationId = GuidControl.ByteArrayToGuid(GuidControl.HexToByteArray(MyBase.PublishedTask(PublishedTask.REGISTRATION_ID)))
            If (Not custRegistrationId.Equals(Guid.Empty)) Then
                Me.CustRegistration = New CustRegistration(custRegistrationId)
            End If
        End If

        ' Check if Registration Item ID is supplied
        If (Not String.IsNullOrEmpty(MyBase.PublishedTask(PublishedTask.REGISTRATION_ITEM_ID))) Then
            custRegistrationItemId = GuidControl.ByteArrayToGuid(GuidControl.HexToByteArray(MyBase.PublishedTask(PublishedTask.REGISTRATION_ITEM_ID)))
            If (Not custRegistrationItemId.Equals(Guid.Empty)) Then
                Me.CustomerItem = New CustItem(custRegistrationItemId)
            End If
        End If

        ' If Registration is not initialized then check if Certificate Number is supplied
        If (Me._custRegistration Is Nothing OrElse Me._custRegistration Is Nothing) Then
            If (Not String.IsNullOrEmpty(MyBase.PublishedTask(PublishedTask.CERTIFICATE_ID))) Then
                certificateId = GuidControl.ByteArrayToGuid(GuidControl.HexToByteArray(MyBase.PublishedTask(PublishedTask.CERTIFICATE_ID)))
                Dim registrationDetails As CustItemDAL.RegistrationDetails
                registrationDetails = CustItem.FindRegistration(certificateId)
                If (Not String.IsNullOrEmpty(registrationDetails.RegistrationId)) Then
                    Me.CustRegistration = New CustRegistration(GuidControl.ByteArrayToGuid(GuidControl.HexToByteArray(registrationDetails.RegistrationId)))
                End If
                If (Not String.IsNullOrEmpty(registrationDetails.RegistrationItemId)) Then
                    Me.CustomerItem = New CustItem(GuidControl.ByteArrayToGuid(GuidControl.HexToByteArray(registrationDetails.RegistrationItemId)))
                End If

                If (Me._custRegistration Is Nothing OrElse Me._customerItem Is Nothing) Then
                    Logger.AddError("Invalid Argument Certificate ID, can not resolve RegistrationId and RegistrationItemId")
                    Me.FailReason = "Invalid Argument Certificate ID, can not resolve RegistrationId and RegistrationItemId"
                    Throw New ArgumentException("Invalid Argument Certificate ID, can not resolve RegistrationId and RegistrationItemId")
                End If

            Else
                Logger.AddError("Missing Arguments, expecting CertificateId Or (RegistrationId and RegistrationItemId)")
                Me.FailReason = "Missing Arguments, expecting CertificateId Or (RegistrationId and RegistrationItemId)"
                Throw New ArgumentException("Missing Arguments, expecting CertificateId Or (RegistrationId and RegistrationItemId)")
            End If
        End If
        _isInitialized = True
    End Sub

    

#End Region

#Region "Protected Members"

    Private _isInitialized As Boolean = False
    Private _customerItem As CustItem

    Protected Property CustomerItem() As CustItem
        Get
            InitializeRegistrationInformation()
            Return _customerItem
        End Get
        Private Set(ByVal value As CustItem)
            _customerItem = value
        End Set
    End Property

    Private _custRegistration As CustRegistration
    Protected Property CustRegistration() As CustRegistration
        Get
            InitializeRegistrationInformation()
            Return _custRegistration
        End Get
        Private Set(ByVal value As CustRegistration)
            _custRegistration = value
        End Set
    End Property

    Private _dealer As Dealer
    Protected ReadOnly Property Dealer() As Dealer
        Get
            If (_dealer Is Nothing) Then
                _dealer = New Dealer(Me.CustRegistration.DealerId)
            End If
            Return _dealer
        End Get
    End Property

    Private _productCode As String
    Protected ReadOnly Property ProductCode As String
        Get
            If (String.IsNullOrEmpty(_productCode)) Then
                _productCode = GetProductCode()
            End If
            Return _productCode
        End Get
    End Property

    Private _equipmentType As String
    Protected Property EquipmentType As String
        Get
            If (String.IsNullOrEmpty(_equipmentType)) Then
                _equipmentType = GetDeviceType()
            End If
            Return _equipmentType
        End Get
        Private Set(ByVal value As String)
            _equipmentType = value
        End Set
    End Property

    Public ReadOnly Property DeviceType As String
        Get
            Select Case Me.EquipmentType
                Case Codes.EQUIPMENT_TYPE__SMARTPHONE
                    Return AntivirusProductTaskBase.DEVICE_TYPE__PHONE
                Case Codes.EQUIPMENT_TYPE__TABLET
                    Return AntivirusProductTaskBase.DEVICE_TYPE__TABLET
                Case Else
                    Return String.Empty
            End Select
        End Get
    End Property

    Private _region As String = String.Empty
    Protected ReadOnly Property Region As String
        Get
            If (CustRegistration.ContactInfo.Address.RegionId <> Nothing) Then
                _region = New Region(CustRegistration.ContactInfo.Address.RegionId).Description
            End If
            Return _region
        End Get
    End Property

    Private _countryCode As String = String.Empty
    Protected ReadOnly Property CountryCode As String
        Get
            _countryCode = "IT"
            Return _countryCode
        End Get
    End Property

    Private _prefferedLanguage As String = String.Empty
    Protected ReadOnly Property PrefferedLanguage As String
        Get
            _prefferedLanguage = "it"
            Return _prefferedLanguage
        End Get
    End Property

    Private _userName As String = String.Empty
    Protected Property UserName As String
        Get
            Return _userName
        End Get
        Set(ByVal value As String)
            _userName = value
        End Set
    End Property

    Private _password As String = String.Empty
    Protected Property Password As String
        Get
            Return _password
        End Get
        Set(ByVal value As String)
            _password = value
        End Set
    End Property

    Protected ReadOnly Property SubscriberId As String
        Get
            Return Me.CustomerItem.OrderRefNum
        End Get
    End Property

    Private _phoneNumber As String = String.Empty
    Protected ReadOnly Property PhoneNumber As String
        Get
            Select Case DeviceType
                Case DEVICE_TYPE__PHONE
                    If (Me.CustomerItem.CellPhone.Trim = String.Empty) Then
                        Me.FailReason = String.Format("Phone number is required for Phone for customer registration Item Id {0} ", GuidControl.GuidToHexString(Me.CustomerItem.Id))
                        Throw New Exception(Me.FailReason)
                    Else
                        _phoneNumber = AppendCountryCode(Me.CustomerItem.CellPhone)
                    End If
                Case DEVICE_TYPE__TABLET
                    _phoneNumber = If(Me.CustomerItem.CellPhone = String.Empty, Me.CustomerItem.CellPhone, AppendCountryCode(Me.CustomerItem.CellPhone))
            End Select

            Return _phoneNumber
        End Get
    End Property


    Protected Function AppendCountryCode(ByVal value As String) As String
        Select Case CountryCode
            Case "IT"
                value = String.Format("39{0}", value)
        End Select

        Return value

    End Function
#End Region
End Class
