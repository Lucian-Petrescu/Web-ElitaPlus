Imports ElitaHarvesterService.Antivirus.ActivateProduct
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports System.Text
Imports Assurant.ElitaPlus.Common

Public NotInheritable Class ActivateProductTask
    Inherits AntivirusProductTaskBase


#Region "Constructors"
    Public Sub New(machineName As String, processThreadName As String)
        MyBase.New(machineName, processThreadName)
    End Sub
#End Region

#Region "Private Members"
    Private _productType As ActivateProductRequestProductInfoProductType
    Private _vendorName As ActivateProductRequestProductInfoVendorName
    Private syncRoot As Object = New Object()
    Private oActivateProductClient As ActivateProductServiceClient

    Private ReadOnly Property ProductType As ActivateProductRequestProductInfoProductType
        Get
            'Logic for extracting product type from Dealer 
            _productType = ActivateProductRequestProductInfoProductType.Antivirus
            Return _productType
        End Get
    End Property

    Private ReadOnly Property VendorName As ActivateProductRequestProductInfoVendorName
        Get
            'Logic for extracting Vendor Name from Dealer 
            _vendorName = ActivateProductRequestProductInfoVendorName.McAfee
            Return _vendorName
        End Get
    End Property

    Protected Overloads ReadOnly Property SubscriberId As String
        Get
            Return String.Format("{0}-{1}", CustRegistration.TaxId, CustomerItem.ImeiNumber)
        End Get
    End Property
#End Region

#Region "Protected Methods"

    Protected Friend Overrides Sub Execute()
        Logger.AddDebugLogEnter()
        Dim activateProductRequest As ActivateProductRequest = New ActivateProductRequest
        Dim activateProductResponse As ActivateProductResponse = New ActivateProductResponse

        Try
            ' 1. Find Device Information based on Argument
            ' 2. Check if License was ever generated, if not then only generate a License
            If (IsValidRequest()) Then
                oActivateProductClient = CreateActivateProductClient()

                activateProductRequest.UserAuthorization = GetUserAuthorization()
                activateProductRequest.Carrier = GetCarrierInfo()
                activateProductRequest.DeviceInfo = GetDeviceInfo()
                activateProductRequest.ProductInfo = GetProductInfo()
                activateProductRequest.SubscriberInfo = GetSubsciberInfo()
                activateProductRequest.Transaction = GetTransactionInfo()

                activateProductResponse = oActivateProductClient.ActivateProduct(activateProductRequest)

                'Save the activation code in DB
                If (activateProductResponse.ActivationCode <> String.Empty) Then
                    ' 3. Update Product Key, Order Reference Number (Customer Context ID) and Fullfilment Date in Device Table
                    SaveActivationLicense(activateProductResponse.ActivationCode)
                End If
            Else
                Throw New Exception(FailReason)
            End If
        Catch ex As Exception
            Logger.AddError(ex)
            Throw
        End Try
        Logger.AddDebugLogExit()
    End Sub
#End Region

#Region "Private Methods"

    Private Function CreateActivateProductClient() As Antivirus.ActivateProduct.ActivateProductServiceClient
        Dim activateProductClient As Antivirus.ActivateProduct.ActivateProductServiceClient
        Dim oWebPasswd As WebPasswd
        oWebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE__ACTIVATE_AV_PRODUCT), True)
        activateProductClient = New Antivirus.ActivateProduct.ActivateProductServiceClient("CustomBinding_IActivateProduct", oWebPasswd.Url)
        UserName = oWebPasswd.UserId
        Password = oWebPasswd.Password
        Return activateProductClient
    End Function

    Private Function GetSubsciberInfo() As ActivateProductRequestSubscriberInfo
        Dim apRequestSubscriberInfo As ActivateProductRequestSubscriberInfo = New ActivateProductRequestSubscriberInfo()

        apRequestSubscriberInfo.SubscriberId = SubscriberId
        apRequestSubscriberInfo.FirstName = CustRegistration.ContactInfo.FirstName
        apRequestSubscriberInfo.LastName = CustRegistration.ContactInfo.LastName
        apRequestSubscriberInfo.PhoneNumber = PhoneNumber
        apRequestSubscriberInfo.PostalCode = CustRegistration.ContactInfo.Address.PostalCode
        apRequestSubscriberInfo.PreferredLanguage = PrefferedLanguage
        apRequestSubscriberInfo.Region = Region
        apRequestSubscriberInfo.Street = CustRegistration.ContactInfo.Address.Address1
        apRequestSubscriberInfo.CountryCode = CountryCode
        apRequestSubscriberInfo.EmailAddress = CustRegistration.ContactInfo.Email
        apRequestSubscriberInfo.RequestedPassword = Codes.MCAFEE_REQ_PASS
        Return apRequestSubscriberInfo
    End Function

    Private Function GetCarrierInfo() As ActivateProductRequestCarrier
        Dim acprCarrier As ActivateProductRequestCarrier = Nothing
        Select Case VendorName
            Case ActivateProductRequestProductInfoVendorName.McAfee
                'Do nothing for McAfee
            Case ActivateProductRequestProductInfoVendorName.AVG
                'Do nothing for McAfee
            Case ActivateProductRequestProductInfoVendorName.Kaspersky
                'Do nothing for McAfee
        End Select

        Return acprCarrier
    End Function

    Private Function GetDeviceInfo() As ActivateProductRequestDeviceInfo
        Dim acprDevice As ActivateProductRequestDeviceInfo = Nothing
        Select Case VendorName
            Case ActivateProductRequestProductInfoVendorName.McAfee
                acprDevice = New ActivateProductRequestDeviceInfo()
                acprDevice.DeviceType = DeviceType
            Case ActivateProductRequestProductInfoVendorName.AVG
                'Do nothing for AVG
            Case ActivateProductRequestProductInfoVendorName.Kaspersky
                'Do nothing for Kaspersky
        End Select

        Return acprDevice
    End Function

    Private Function GetUserAuthorization() As ActivateProductRequestUserAuthorization
        Dim apUserAuth As ActivateProductRequestUserAuthorization = New ActivateProductRequestUserAuthorization()
        apUserAuth.UserId = UserName
        apUserAuth.Password = Password

        Return apUserAuth
    End Function

    Private Function GetProductInfo() As ActivateProductRequestProductInfo
        Dim apRequestProductInfo As ActivateProductRequestProductInfo = New ActivateProductRequestProductInfo()
        apRequestProductInfo.ProductId = ProductCode
        apRequestProductInfo.ProductType = ProductType
        apRequestProductInfo.VendorName = VendorName
        apRequestProductInfo.VendorNameSpecified = True

        Return apRequestProductInfo
    End Function

    Private Function GetTransactionInfo() As ActivateProductRequestTransaction
        Dim apRequestTransaction As ActivateProductRequestTransaction = New ActivateProductRequestTransaction()
        apRequestTransaction.TransactionId = Guid.NewGuid().ToString()
        Return apRequestTransaction
    End Function

    Private Sub SaveActivationLicense(activationCode As String)
        CustomerItem.ProductKey = activationCode
        CustomerItem.ProductProcurementDate = DateTime.Now
        CustomerItem.OrderRefNum = SubscriberId
        Try
            CustomerItem.Save()
        Catch ex As Exception
            Logger.AddError(String.Format("Exception while saving Product Key for Customer|Tax Id:{0},ProductKey:{1}|", CustRegistration.TaxId, CustomerItem.ProductKey), ex)
            Throw
        End Try

    End Sub

    Private Function IsValidRequest() As Boolean
        Dim flag As Boolean = True
        Dim sb As StringBuilder = New StringBuilder()
        If (Not String.IsNullOrEmpty(CustomerItem.ProductKey)) Then
            flag = False
            sb.AppendLine(String.Format("Activation Key already exists for cutomer registration item id {0}", GuidControl.GuidToHexString(CustomerItem.Id)))
        End If
        If (EquipmentType <> Codes.EQUIPMENT_TYPE__TABLET AndAlso EquipmentType <> Codes.EQUIPMENT_TYPE__SMARTPHONE) Then
            flag = False
            sb.AppendLine(String.Format("Activation cannot be done for equipement type {0} for cutomer registration item id {1}", EquipmentType, GuidControl.GuidToHexString(CustomerItem.Id)))
        End If


        If (Not flag) Then
            Logger.AddError(sb.ToString())
            FailReason = sb.ToString()
        End If

        Return flag
    End Function
#End Region

End Class
