Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports ElitaHarvesterService.Antivirus.UpdateProduct
Imports System.Text
Imports Assurant.ElitaPlus.Common

Public NotInheritable Class UpdateProductTask
    Inherits AntivirusProductTaskBase
#Region "Constructors"

    Public Sub New(machineName As String, processThreadName As String)
        MyBase.New(machineName, processThreadName)
    End Sub

#End Region

#Region "Private Members"
    Private _productType As UpdateProductRequestProductInfoProductType
    Private _vendorName As UpdateProductRequestProductInfoVendorName
    Private syncRoot As Object = New Object()
    Private oUpdateProductClient As UpdateProductClient

    Private ReadOnly Property ProductType As UpdateProductRequestProductInfoProductType
        Get
            'Logic for extracting product type from Dealer 
            _productType = UpdateProductRequestProductInfoProductType.Antivirus
            Return _productType
        End Get
    End Property

    Private ReadOnly Property VendorName As UpdateProductRequestProductInfoVendorName
        Get
            'Logic for extracting Vendor Name from Dealer 
            _vendorName = UpdateProductRequestProductInfoVendorName.McAfee
            Return _vendorName
        End Get
    End Property

#End Region

#Region "Protected Methods"

    Protected Friend Overrides Sub Execute()
        Logger.AddDebugLogEnter()
        Dim updateProductRequest As UpdateProductRequest = New UpdateProductRequest
        Dim updateProductResponse As UpdateProductResponse = New UpdateProductResponse

        Try
            If (IsValidRequest()) Then

                oUpdateProductClient = CreateUpdateProductClient()
                updateProductRequest.UserAuthorization = GetUserAuthorization()
                updateProductRequest.Carrier = GetCarrierInfo()
                updateProductRequest.DeviceInfo = GetDeviceInfo()
                updateProductRequest.ProductInfo = GetProductInfo()
                updateProductRequest.SubscriberInfo = GetSubsciberInfo()
                updateProductRequest.Transaction = GetTransactionInfo()

                updateProductResponse = oUpdateProductClient.UpdateProduct(updateProductRequest)
                UpdatePhoneNumber()
            Else
                Throw New Exception(FailReason)
            End If
            Logger.AddDebugLogExit()
        Catch ex As Exception
            Throw
        End Try

    End Sub
#End Region

#Region "Private Methods"

    Public Function CreateUpdateProductClient() As Antivirus.UpdateProduct.UpdateProductClient
        Dim updateProductClient As Antivirus.UpdateProduct.UpdateProductClient
        Dim oWebPasswd As WebPasswd
        oWebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE__UPDATE_AV_PRODUCT), True)
        updateProductClient = New Antivirus.UpdateProduct.UpdateProductClient("CustomBinding_IUpdateProduct", oWebPasswd.Url)
        UserName = oWebPasswd.UserId
        Password = oWebPasswd.Password
        Return updateProductClient
    End Function

    Private Function GetSubsciberInfo() As UpdateProductRequestSubscriberInfo
        Dim upRequestSubscriberInfo As UpdateProductRequestSubscriberInfo = New UpdateProductRequestSubscriberInfo()
        upRequestSubscriberInfo.SubscriberId = SubscriberId
        upRequestSubscriberInfo.EmailAddress = CustRegistration.ContactInfo.Email
        upRequestSubscriberInfo.PhoneNumber = AppendCountryCode(GetNewPhoneNumber())
        upRequestSubscriberInfo.OldPhoneNumber = PhoneNumber
        upRequestSubscriberInfo.CountryCode = CountryCode

        Return upRequestSubscriberInfo
    End Function

    Private Function GetCarrierInfo() As UpdateProductRequestCarrier
        Dim uprCarrier As UpdateProductRequestCarrier = Nothing
        Select Case VendorName
            Case UpdateProductRequestProductInfoVendorName.McAfee
                'Do nothing for McAfee
            Case UpdateProductRequestProductInfoVendorName.AVG
                'Do nothing for McAfee
            Case UpdateProductRequestProductInfoVendorName.Kaspersky
                'Do nothing for McAfee
        End Select

        Return uprCarrier
    End Function

    Private Function GetDeviceInfo() As UpdateProductRequestDeviceInfo
        Dim uprDevice As UpdateProductRequestDeviceInfo = Nothing
        Select Case VendorName
            Case UpdateProductRequestProductInfoVendorName.McAfee
                uprDevice = New UpdateProductRequestDeviceInfo()
                uprDevice.DeviceType = DeviceType
            Case UpdateProductRequestProductInfoVendorName.AVG
                'Do nothing for AVG
            Case UpdateProductRequestProductInfoVendorName.Kaspersky
                'Do nothing for Kaspersky
        End Select

        Return uprDevice
    End Function

    Private Function GetUserAuthorization() As UpdateProductRequestUserAuthorization
        Dim upUserAuth As UpdateProductRequestUserAuthorization = New UpdateProductRequestUserAuthorization()
        upUserAuth.UserId = UserName
        upUserAuth.Password = Password

        Return upUserAuth
    End Function

    Private Function GetProductInfo() As UpdateProductRequestProductInfo
        Dim upRequestProductInfo As UpdateProductRequestProductInfo = New UpdateProductRequestProductInfo()
        upRequestProductInfo.ProductId = ProductCode
        upRequestProductInfo.ActivationCode = CustomerItem.ProductKey
        upRequestProductInfo.ProductType = ProductType
        upRequestProductInfo.VendorName = VendorName
        upRequestProductInfo.VendorNameSpecified = True

        Return upRequestProductInfo
    End Function

    Private Function GetTransactionInfo() As UpdateProductRequestTransaction
        Dim upRequestTransaction As UpdateProductRequestTransaction = New UpdateProductRequestTransaction()
        upRequestTransaction.TransactionId = Guid.NewGuid().ToString()
        Return upRequestTransaction
    End Function

    Private Function IsValidRequest() As Boolean
        Dim flag As Boolean = True
        Dim sb As StringBuilder = New StringBuilder()
        Select Case DeviceType
            Case DEVICE_TYPE__PHONE
                If (String.IsNullOrEmpty(CustomerItem.CellPhone)) Then
                    flag = False
                    sb.Append(String.Format("Old Phone number missing for Customer Registration Item Id {0}", GuidControl.GuidToHexString(CustomerItem.Id)))
                End If
                Dim workPhone As String = GetNewPhoneNumber()
                If (String.IsNullOrEmpty(workPhone)) Then
                    flag = False
                    sb.Append(String.Format("Work Phone number missing for Certificate Id {0}", GuidControl.ByteArrayToGuid(GuidControl.HexToByteArray(MyBase.PublishedTask(PublishedTask.CERTIFICATE_ID)))))
                Else
                    If (workPhone.Equals(CustomerItem.CellPhone)) Then
                        flag = False
                        sb.Append(String.Format("Old Phone Number is same as New Phone Number for Customer Registration Item Id {0} ,Certificate Id {1}", GuidControl.GuidToHexString(CustomerItem.Id), GuidControl.ByteArrayToGuid(GuidControl.HexToByteArray(MyBase.PublishedTask(PublishedTask.CERTIFICATE_ID)))))
                    End If
                End If
                If (String.IsNullOrEmpty(CustomerItem.ProductKey)) Then
                    flag = False
                    sb.Append(String.Format("Activation Key is missing for Customer Registration Item Id {0}", GuidControl.GuidToHexString(CustomerItem.Id)))
                End If
                If ((flag) AndAlso (String.IsNullOrEmpty(CustomerItem.OrderRefNum))) Then
                    flag = False
                    sb.Append(String.Format("Order Reference Number (Subscriber ID) is missing for Customer Registration Id {0}", GuidControl.GuidToHexString(CustRegistration.Id)))
                    Logger.AddError(sb.ToString())
                    FailReason = sb.ToString()
                End If
            Case DEVICE_TYPE__TABLET
                flag = False
                sb.Append(String.Format("Update request is not valid for Device Type Tablet for Customer Registration Item Id {0}", GuidControl.GuidToHexString(CustomerItem.Id)))
        End Select
        If (Not flag) Then
            Logger.AddError(sb.ToString())
            FailReason = sb.ToString()
        End If

        Return flag
    End Function

    Private Function GetNewPhoneNumber() As String
        Dim newPhoneNumber As String = String.Empty
        Dim certificateId As Guid
        Dim certificate As Certificate

        If (Not String.IsNullOrEmpty(MyBase.PublishedTask(PublishedTask.CERTIFICATE_ID))) Then
            certificateId = GuidControl.ByteArrayToGuid(GuidControl.HexToByteArray(MyBase.PublishedTask(PublishedTask.CERTIFICATE_ID)))
        Else
            Throw New ArgumentException("No Certificate Id found for the task ")
        End If

        certificate = New Certificate(certificateId)
        newPhoneNumber = certificate.WorkPhone
        Return newPhoneNumber
    End Function

    Private Sub UpdatePhoneNumber()
        Try
            CustomerItem.CellPhone = GetNewPhoneNumber()
            CustomerItem.Save()
        Catch ex As Exception
            Logger.AddError(String.Format("Exception while updating Phone number in DB for Customer Registration Item Id ", GuidControl.GuidToHexString(CustomerItem.Id)), ex)
            Throw
        End Try
        
    End Sub

#End Region
End Class
