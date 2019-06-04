Imports System.Text
Imports RestSharp
Imports Newtonsoft.Json.Linq
Imports Assurant.ElitaPlus.Common
Imports System.Security.Cryptography
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports RestSharp.Extensions.MonoHttp
Imports System.Configuration

Public Class NotifyDigitalServiceTask
    Inherits TaskBase

#Region "Fields"
    Private _certificateId As Guid
    Private _oCertificate As Certificate

#End Region

#Region "Constructors"

    Public Sub New(ByVal machineName As String, ByVal processThreadName As String)
        MyBase.New(machineName, processThreadName)
    End Sub

#End Region

#Region "Properties"
    Private Property CertificateId As Guid
        Get
            Return _certificateId
        End Get
        Set(ByVal value As Guid)
            _certificateId = value
        End Set
    End Property

    Private Property oCertificate As Certificate
        Get
            Return _oCertificate
        End Get
        Set(ByVal value As Certificate)
            _oCertificate = value
        End Set
    End Property


    Private Const DigitalSchema = "DIGITAL_SCHEMA"
    Private Const DigitalToken = "DIGITAL_TOKEN"
    Private Const DigitalKey = "DIGITAL_KEY"
    Private Const DigitalUrl = "DIGITAL_URL"

#End Region

#Region "Protected Methods"

    Protected Friend Overrides Sub Execute()

        Logger.AddDebugLogEnter()

        Dim oDigitalServiceClient As RestClient
        Dim DigitalServiceRequest As RestRequest
        Dim DigitalServiceResponse As IRestResponse
        Dim Schema As String 
        Dim oauth_token As String
        Dim oauth_token_secret As String
        Dim resource_url As String

        Try
            If (Not String.IsNullOrEmpty(MyBase.PublishedTask(PublishedTask.CERTIFICATE_ID))) Then
                CertificateId = GuidControl.ByteArrayToGuid(GuidControl.HexToByteArray(MyBase.PublishedTask(PublishedTask.CERTIFICATE_ID)))
                If (Not CertificateId.Equals(Guid.Empty)) Then
                    oCertificate = New Certificate(CertificateId)

                    Schema = ConfigurationManager.AppSettings(DigitalSchema)
                    oauth_token = ConfigurationManager.AppSettings(DigitalToken)
                    oauth_token_secret = ConfigurationManager.AppSettings(DigitalKey)
                    resource_url = ConfigurationManager.AppSettings(DigitalUrl)

                    Dim epochStart As New DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                    Dim timeSpan As TimeSpan = DateTime.UtcNow - epochStart
                    Dim oauth_timestamp As String = Convert.ToUInt64(timeSpan.TotalSeconds).ToString()
                    Dim authHeader As String = String.Empty
                    Dim requestContentBase64String As String = String.Empty
                    Dim resource_urlvalidate As String = resource_url + "api/PolicyEnroll"

                    Dim requestUri As String = HttpUtility.UrlEncode(resource_urlvalidate.ToLower())
                    Dim nonce As String = Guid.NewGuid().ToString("N")

                    Dim signatureRawData As String = String.Format("{0}{1}{2}{3}{4}", oauth_token, "POST", requestUri, oauth_timestamp, nonce)

                    Dim secretKeyByteArray = Convert.FromBase64String(oauth_token_secret)

                    Dim signature As Byte() = Encoding.UTF8.GetBytes(signatureRawData)

                    Using hmac As New HMACSHA256(secretKeyByteArray)
                        Dim signatureBytes As Byte() = hmac.ComputeHash(signature)
                        Dim requestSignatureBase64String As String = Convert.ToBase64String(signatureBytes)
                        authHeader = String.Format("{0} {1}:{2}:{3}:{4}", Schema, oauth_token, requestSignatureBase64String, nonce, oauth_timestamp)
                    End Using

                    oDigitalServiceClient = New RestClient(resource_url)
                    DigitalServiceRequest = New RestRequest(Method.POST)
                    DigitalServiceRequest.AddHeader("Authorization", authHeader)
                    DigitalServiceRequest.AddHeader("Content-type", "application/json")
                    DigitalServiceRequest.Resource = "api/PolicyEnroll"
                    DigitalServiceRequest.RequestFormat = DataFormat.Json
                    DigitalServiceRequest.AddBody(New With {Key .MiddlewareIDNumber = oCertificate.TaxIDNumb,
                                                            Key .CompanyCode = oCertificate.Dealer.Company.Code,
                                                            Key .DealerCode = oCertificate.Dealer.Dealer,
                                                            Key .CertificateNumber = oCertificate.CertNumber})

                    DigitalServiceResponse = oDigitalServiceClient.Execute(DigitalServiceRequest)

                End If
            End If
            Logger.AddDebugLogExit()
        Catch ex As Exception
            Logger.AddError(ex)
            Throw
        End Try
    End Sub
#End Region


#Region "Private Methods"

    Private Function IsValidRequest() As Boolean
        Dim flag As Boolean = True
        Dim sb As StringBuilder = New StringBuilder()
        If (Not String.IsNullOrEmpty(Me.oCertificate.CertNumber)) Then
            flag = False
            sb.AppendLine(String.Format("CertNumber already exists  {0}", GuidControl.GuidToHexString(oCertificate.Id)))
        End If

        If (Not flag) Then
            Logger.AddError(sb.ToString())
            Me.FailReason = sb.ToString()
        End If

        Return flag
    End Function
#End Region
End Class
