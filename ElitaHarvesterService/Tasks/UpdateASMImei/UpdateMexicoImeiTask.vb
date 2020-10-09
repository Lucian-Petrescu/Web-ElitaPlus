Imports System.Text
Imports Assurant.ElitaPlus.Common
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports ElitaHarvesterService.OutboundCommunication
Imports ElitaHarvesterService.MexicoIShopService
Imports ElitaHarvesterService.AssurantMexicoService
Imports ElitaHarvesterService.ASMMacStore
Imports System.IO
Imports System.Configuration

Public Class UpdateMexicoImeiTask
    Inherits TaskBase
    
#Region "Constructors"

    Public Sub New(machineName As String, processThreadName As String)
        MyBase.New(machineName, processThreadName)
    End Sub

#End Region


#Region "Fields"
    Private _certificateId As Guid
    Private _certItemId As Guid
    Private _oCertificate As Certificate
    Private _oCertItem As CertItem
#End Region
#Region "Properties"

    Private Const IshopUrl = "ISHOP_URL"
    Private Const AsmUrl = "ASM_URL"
    Private Const AsmUsername = "ASM_USERNAME"
    Private Const AsmPassword = "ASM_PASSWORD"
    Private Const MsUrl = "MS_URL"
    Private Const MsUsername = "MS_USERNAME"
    Private Const MsPassword = "MS_PASSWORD"

    Private Property CertificateId As Guid
        Get
            Return _certificateId
        End Get
        Set(value As Guid)
            _certificateId = value
        End Set
    End Property
    Private Property oCertificate As Certificate
        Get
            Return _oCertificate
        End Get
        Set(value As Certificate)
            _oCertificate = value
        End Set
    End Property


    Private Property CertItemId As Guid
        Get
            Return _certItemId
        End Get
        Set(value As Guid)
            _certItemId = value
        End Set
    End Property

    Private Property oCertItem As CertItem
        Get
            Return _oCertItem
        End Get
        Set(value As CertItem)
            _oCertItem = value
        End Set
    End Property



#End Region


#Region "Protected Methods"
    Protected Friend Overrides Sub Execute()

        Logger.AddDebugLogEnter()
        Dim mis As MexicoIShopService.SwitchUpAppleSoapClient
        Dim asm As AssurantMexicoService.AppleCareServiceClient
        Dim ms As ASMMacStore.ServiceSoapClient
        Dim MUsername As String
        Dim MPassword As String
        Dim omanufacturer As Manufacturer


        Try
            If (Not String.IsNullOrEmpty(MyBase.PublishedTask(PublishedTask.CERT_ITEM_ID))) Then
                CertItemId = GuidControl.ByteArrayToGuid(GuidControl.HexToByteArray(MyBase.PublishedTask(PublishedTask.CERT_ITEM_ID)))
                If (Not CertItemId.Equals(Guid.Empty)) Then
                    oCertItem = New CertItem(CertItemId)
                    oCertificate = New Certificate(oCertItem.CertId)
                    omanufacturer = New Manufacturer(oCertItem.ManufacturerId)
                    Select Case MyBase.PublishedTask(PublishedTask.DESTINATION)

                        Case "ASM"
                            Dim oWebPasswd As WebPasswd
                            oWebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE_ASM_MEXICO), True)
                            asm = New AssurantMexicoService.AppleCareServiceClient("BasicHttpBinding_IAppleCareService", oWebPasswd.Url)
                            Dim asmRequest = New AssurantMexicoService.CertificateInfoAppleCare

                            asmRequest.Brand = omanufacturer.Description
                            asmRequest.CertificateNumber = oCertificate.CertNumber
                            asmRequest.DealerCode = MyBase.PublishedTask(PublishedTask.DEALER_CODE)
                            asmRequest.IMEI = MyBase.PublishedTask(PublishedTask.IMEI_NO)
                            asmRequest.IdMotive = 5
                            asmRequest.Model = oCertItem.Model
                            asmRequest.Password = oWebPasswd.Password
                            asmRequest.ReplacementIMEI = MyBase.PublishedTask(PublishedTask.REP_IMEI_NO)
                            asmRequest.ReplacementSerialNumber = oCertificate.InvoiceNumber
                            asmRequest.SKU = oCertItem.SkuNumber
                            asmRequest.SerialNumber = oCertificate.InvoiceNumber
                            asmRequest.TransactionType = "M"
                            asmRequest.UserId = oWebPasswd.UserId

                            Dim asmResponse = asm.Update(asmRequest)


                        Case "iShop"
                            Dim oWebPasswd As WebPasswd
                            oWebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE_ISHOP_MEXICO), True)
                            mis = New MexicoIShopService.SwitchUpAppleSoapClient("SwitchUpAppleSoap", oWebPasswd.Url)
                            Dim ishopRequest = New MexicoIShopService.ActualizaDatosInpunt


                            ishopRequest.DealerCode = MyBase.PublishedTask(PublishedTask.DEALER_CODE)
                            ishopRequest.CertificateNumber = oCertificate.CertNumber
                            ishopRequest.SKU = oCertItem.SkuNumber
                            ishopRequest.Brand = omanufacturer.Description
                            ishopRequest.Model = oCertItem.Model
                            ishopRequest.SerialNumber = oCertificate.InvoiceNumber
                            ishopRequest.IMEI = MyBase.PublishedTask(PublishedTask.IMEI_NO)
                            ishopRequest.ReplacementModel = oCertItem.Model
                            ishopRequest.ReplacementSerialNumber = oCertificate.InvoiceNumber
                            ishopRequest.ReplacementIMEI = MyBase.PublishedTask(PublishedTask.REP_IMEI_NO)
                            ishopRequest.ReplacementSKU = oCertItem.SkuNumber
                            ishopRequest.TransactionType = "M"
                            ishopRequest.IdMotive = "5"

                            Dim ishopResponse = mis.ActualizaDatos(ishopRequest)

                        Case "MacStore"
                            Dim oWebPasswd As WebPasswd
                            oWebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE_MAC_STORE), True)
                            ms = New ASMMacStore.ServiceSoapClient("ServiceSoap12", oWebPasswd.Url)

                            MUsername = oWebPasswd.UserId
                            MPassword = oWebPasswd.Password
                            'MUsername = ConfigurationManager.AppSettings(MsUsername)
                            'MPassword = ConfigurationManager.AppSettings(MsPassword)
                            Dim DealerCode = MyBase.PublishedTask(PublishedTask.DEALER_CODE)

                            Dim SerialNumber = oCertificate.InvoiceNumber
                            Dim ReplacementIMEI = MyBase.PublishedTask(PublishedTask.REP_IMEI_NO)
                            Dim ReplacementSerialNumber = oCertificate.InvoiceNumber
                            Dim TransactionType = "M"
                            Dim IdMotive = "5"
                            ms.ActualizaInfoXCambioAC(MUsername, MPassword, DealerCode, oCertificate.CertNumber, oCertItem.SkuNumber, omanufacturer.Description, oCertItem.Model, oCertItem.SerialNumber, oCertItem.IMEINumber, ReplacementSerialNumber, ReplacementIMEI, TransactionType, IdMotive)
                    End Select
                End If
            End If

            Logger.AddDebugLogExit()
        Catch ex As Exception
            Logger.AddError(ex)
            Throw
        End Try
    End Sub
#End Region
    End Class