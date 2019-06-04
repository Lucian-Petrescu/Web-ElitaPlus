Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports ElitaHarvesterService.Antivirus.CancelProduct
Imports System.Text
Imports Assurant.ElitaPlus.Common

Public NotInheritable Class CancelProductTask
    Inherits AntivirusProductTaskBase

    Public Sub New(ByVal machineName As String, ByVal processThreadName As String)
        MyBase.New(machineName, processThreadName)
    End Sub

#Region "Private Members"
    Private _productType As CancelProductRequestProductInfoProductType
    Private _vendorName As CancelProductRequestProductInfoVendorName
    Private syncRoot As Object = New Object()
    Private oCancelProductClient As CancelProductClient

    Private ReadOnly Property ProductType As CancelProductRequestProductInfoProductType
        Get
            'Logic for extracting product type from Dealer 
            _productType = CancelProductRequestProductInfoProductType.Antivirus
            Return _productType
        End Get
    End Property

    Private ReadOnly Property VendorName As CancelProductRequestProductInfoVendorName
        Get
            'Logic for extracting Vendor Name from Dealer 
            _vendorName = CancelProductRequestProductInfoVendorName.McAfee
            Return _vendorName
        End Get
    End Property

#End Region

#Region "Protected Methods"

    Protected Friend Overrides Sub Execute()
        Logger.AddDebugLogEnter()
        Dim cancelProductRequest As CancelProductRequest = New CancelProductRequest
        Dim cancelProductResponse As CancelProductResponse = New CancelProductResponse

        Try
            If (IsValidRequest()) Then
                oCancelProductClient = CreateCancelProductClient()
                cancelProductRequest.UserAuthorization = GetUserAuthorization()
                cancelProductRequest.Carrier = GetCarrierInfo()
                cancelProductRequest.DeviceInfo = GetDeviceInfo()
                cancelProductRequest.ProductInfo = GetProductInfo()
                cancelProductRequest.SubscriberInfo = GetSubsciberInfo()
                cancelProductRequest.Transaction = GetTransactionInfo()

                cancelProductResponse = oCancelProductClient.CancelProduct(cancelProductRequest)

                UpdateCustomerItem()
            Else
                Throw New Exception(Me.FailReason)
            End If
            Logger.AddDebugLogExit()
        Catch ex As Exception
            Throw
        End Try

    End Sub
#End Region

#Region "Private Methods"
    Public Function CreateCancelProductClient() As Antivirus.CancelProduct.CancelProductClient
        Dim cancelProductClient As Antivirus.CancelProduct.CancelProductClient
        Dim oWebPasswd As WebPasswd
        oWebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE__CANCEL_AV_PRODUCT), True)
        cancelProductClient = New Antivirus.CancelProduct.CancelProductClient("CustomBinding_ICancelProduct", oWebPasswd.Url)
        Me.UserName = oWebPasswd.UserId
        Me.Password = oWebPasswd.Password
        Return cancelProductClient
    End Function


    Private Function GetSubsciberInfo() As CancelProductRequestSubscriberInfo
        Dim cpRequestSubscriberInfo As CancelProductRequestSubscriberInfo = New CancelProductRequestSubscriberInfo()
        cpRequestSubscriberInfo.SubscriberId = SubscriberId
        cpRequestSubscriberInfo.PhoneNumber = PhoneNumber

        Return cpRequestSubscriberInfo
    End Function

    Private Function GetCarrierInfo() As CancelProductRequestCarrier
        Dim cprCarrier As CancelProductRequestCarrier = Nothing
        Select Case VendorName
            Case CancelProductRequestProductInfoVendorName.McAfee
                'Do nothing for McAfee
            Case CancelProductRequestProductInfoVendorName.AVG
                'Do nothing for McAfee
            Case CancelProductRequestProductInfoVendorName.Kaspersky
                'Do nothing for McAfee
        End Select

        Return cprCarrier
    End Function

    Private Function GetDeviceInfo() As CancelProductRequestDeviceInfo
        Dim cprDevice As CancelProductRequestDeviceInfo = Nothing
        Select Case VendorName
            Case CancelProductRequestProductInfoVendorName.McAfee
                cprDevice = New CancelProductRequestDeviceInfo()
                cprDevice.DeviceType = Me.DeviceType
            Case CancelProductRequestProductInfoVendorName.AVG
                'Do nothing for AVG
            Case CancelProductRequestProductInfoVendorName.Kaspersky
                'Do nothing for Kaspersky
        End Select

        Return cprDevice
    End Function

    Private Function GetUserAuthorization() As CancelProductRequestUserAuthorization
        Dim cpUserAuth As CancelProductRequestUserAuthorization = New CancelProductRequestUserAuthorization()
        cpUserAuth.UserId = Me.UserName
        cpUserAuth.Password = Me.Password

        Return cpUserAuth
    End Function

    Private Function GetProductInfo() As CancelProductRequestProductInfo
        Dim cpRequestProductInfo As CancelProductRequestProductInfo = New CancelProductRequestProductInfo()
        cpRequestProductInfo.ProductId = ProductCode
        cpRequestProductInfo.ActivationCode = CustomerItem.ProductKey
        cpRequestProductInfo.ProductType = ProductType
        cpRequestProductInfo.VendorName = VendorName
        cpRequestProductInfo.VendorNameSpecified = True

        Return cpRequestProductInfo
    End Function

    Private Function GetTransactionInfo() As CancelProductRequestTransaction
        Dim cpRequestTransaction As CancelProductRequestTransaction = New CancelProductRequestTransaction()
        cpRequestTransaction.TransactionId = Guid.NewGuid().ToString()
        Return cpRequestTransaction
    End Function

    Private Sub UpdateCustomerItem()
        CustomerItem.ProductKey = String.Empty
        CustomerItem.OrderRefNum = String.Empty
        Try
            CustomerItem.Save()
        Catch ex As Exception
            Logger.AddError(String.Format("Exception while deleting Product Key for Customer|Tax Id:{0},ProductKey:{1}|", CustRegistration.TaxId, CustomerItem.ProductKey), ex)
            Throw
        End Try

    End Sub

    Private Function IsValidRequest() As Boolean
        Dim flag As Boolean = True
        Dim sb As StringBuilder = New StringBuilder()
        If (String.IsNullOrEmpty(CustomerItem.ProductKey)) Then
            flag = False
            sb.Append(String.Format("Activation Key is missing for Customer Registration Id {0}", GuidControl.GuidToHexString(CustRegistration.Id)))
            Logger.AddError(sb.ToString())
            Me.FailReason = sb.ToString()
        End If

        If ((flag) AndAlso (String.IsNullOrEmpty(CustomerItem.OrderRefNum))) Then
            flag = False
            sb.Append(String.Format("Order Reference Number (Subscriber ID) is missing for Customer Registration Id {0}", GuidControl.GuidToHexString(CustRegistration.Id)))
            Logger.AddError(sb.ToString())
            Me.FailReason = sb.ToString()
        End If
        Return flag
    End Function

#End Region

End Class
