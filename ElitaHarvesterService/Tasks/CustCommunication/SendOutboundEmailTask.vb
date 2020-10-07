Imports System.Configuration
Imports Assurant.ElitaPlus.Common
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports ElitaHarvesterService.OutboundCommunication
Imports System.IO
Imports System.Security.Cryptography
Imports System.Text

Public Class SendOutboundEmailTask
    Inherits TaskBase
#Region "Constructors"
    Public Sub New(machineName As String, processThreadName As String)
        MyBase.New(machineName, processThreadName)
    End Sub
#End Region


    '' Start of Encryption Fix. Remove this code in permanent fix

    Private Const AESCryptographyPassword = "AES_CRYPTOGRAPHY_PASSWORD"
    Private Const AESCryptographySalt = "AES_CRYPTOGRAPHY_SALT"
    Private Const AESCryptographyIterations = "AES_CRYPTOGRAPHY_ITERATIONS"

    Public Function AES_Encrypt(input As String) As String

        Dim bytesToBeEncrypted As Byte() = Nothing
        Dim encryptedBytes As Byte() = Nothing
        Dim passwordBytes As Byte() = Nothing
        Dim encryptedString As String = String.Empty
        Dim passwordString As String = String.Empty
        Dim saltString As String = String.Empty
        Dim encryptIterations As Integer = 0
        Dim saltBytes As Byte() = Nothing


        passwordString = ConfigurationManager.AppSettings(AESCryptographyPassword)
        saltString = ConfigurationManager.AppSettings(AESCryptographySalt)
        encryptIterations = CInt(ConfigurationManager.AppSettings(AESCryptographyIterations))

        If (Not String.IsNullOrEmpty(input)) Then
            bytesToBeEncrypted = Encoding.UTF8.GetBytes(input)
            passwordBytes = Encoding.UTF8.GetBytes(passwordString)
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes)

            ' Set your salt here, change it to meet your flavor:
            saltBytes = Encoding.UTF8.GetBytes(saltString)

            Using ms As New MemoryStream()
                Using AES As Aes = AES.Create()

                    Dim key = New Rfc2898DeriveBytes(passwordBytes, saltBytes, encryptIterations) '//"http://stackoverflow.com/questions/21145982/rfc2898derivebytes-iterationcount-purpose-and-best-practices"

                    AES.Key = key.GetBytes(AES.KeySize / 8)
                    AES.IV = key.GetBytes(AES.BlockSize / 8)

                    AES.Mode = CipherMode.CBC

                    Using cs = New CryptoStream(ms, AES.CreateEncryptor(AES.Key, AES.IV), CryptoStreamMode.Write)
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length)
                        cs.Close()
                    End Using
                    encryptedBytes = ms.ToArray()
                    encryptedString = Convert.ToBase64String(encryptedBytes)
                End Using
            End Using

        End If

        Return encryptedString

    End Function
    '' End of Encryption Fix. Remove this code in permanent fix

#Region "Protected Methods"
    Protected Friend Overrides Sub Execute()
        Logger.AddDebugLogEnter()

        Dim strComments As String
        dim intErrCode as Integer, strErrMsg as string
        dim guidMsgId as Guid, strTemplateCode as String
        dim strTemplateUserName As String, strTemplatePassword as String
        dim strWhiteList As string
        Dim listParamValues As new Dictionary(Of String, string)
        Dim lstRecipients as new List(Of String)
        Dim strStatus As String
        Dim strXml As New StringWriter
        Dim strCommReferenceId as string, blnBlocked as Boolean
        dim intUpdateResult As Integer
                    
        Try
            'MyBase.PublishedTasK.Id
            'to do, call elita.ELP_OUTBOUND_COMMUNICATION.GetMessageDetailsByTask to get all parameters
            'guidTaskId = MyBase.PublishedTask.Id
            'debug
            PublishedTask.GetOutBoundMessageDetails(PublishedTasK.Id, intErrCode, strErrMsg, guidMsgId, strTemplateCode, strWhiteList,
                                                    strTemplateUserName, strTemplatePassword, lstRecipients,listParamValues)


            If intErrCode = 0 Then
                Dim obc As OutboundCommunication.CommunicationClient
                Dim oWebPasswd As WebPasswd

                oWebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE_CUST_COMM_SERVICE), True)

                Dim strUrl As String
                strUrl = oWebPasswd.Url
                obc = New OutboundCommunication.CommunicationClient("CustomBinding_ICommunication", strUrl)

                obc.ClientCredentials.UserName.UserName = oWebPasswd.UserId
                obc.ClientCredentials.UserName.Password = oWebPasswd.Password

                For Each strRecipient As String In lstRecipients
                    blnBlocked = False
                    If EnvironmentContext.Current.Environment <> Assurant.ElitaPlus.Common.Environments.Production Then
                        If String.IsNullOrWhiteSpace(strWhiteList) OrElse strWhiteList.IndexOf(strRecipient, StringComparison.InvariantCulture) < 0 Then
                            'not in white list, don't send the email
                            'update the attemp as success
                            strCommReferenceId = guid.Empty.ToString()
                            strStatus = "SUCCESS"
                            strComments = "Email Block, not white listed in " + EnvironmentContext.Current.EnvironmentShortName
                            blnBlocked = True
                        End If
                    End If

                    If blnBlocked = False Then
                        Dim request = New OutboundCommunication.ExactTargetRequest()
                        request.ExactTargetPassword = strTemplatePassword
                        request.ExactTargetUserName = strTemplateUserName
                        request.FromEmail = String.Empty
                        request.FromName = String.Empty
                        request.TriggerKey = strTemplateCode
                        request.Email = strRecipient

                        Dim addEncryptedCertificate As Boolean = False
                        Dim encryptedTriggerKeys() As String =
                                {"Liberty_Purchase_Confirm", "QA_Liberty_Purchase_Confirm",
                                 "Liberty_Purchase_Confirm_Downgrade", "QA_Liberty_Purchase_Confirm_Downgrad",
                                 "Liberty_Purchase_Confirm_Upgrade", "QA_Liberty_Purchase_Confirm_Upgrade",
                                 "Liberty_Reactivation_wGap", "QA_Liberty_Reactivation_wGap"}

                        If (listParamValues.ContainsKey("CertificateNo") AndAlso encryptedTriggerKeys.Contains(strTemplateCode)) Then
                            addEncryptedCertificate = True
                        End If

                        request.Attributes = New NameValue(If(addEncryptedCertificate, listParamValues.Count, listParamValues.Count - 1)) {}

                        Dim strValue As String, i As Integer = 0
                        For each strKey as String in listParamValues.Keys
                            strValue = listParamValues(strKey)

                            ' Start of Encryption Fix. Remove this code in permanent fix
                            If (addEncryptedCertificate AndAlso strKey = "CertificateNo") Then
                                request.Attributes.SetValue(New NameValue() With {.Name = strKey & "Enc", .Value = If(String.IsNullOrEmpty(strValue), String.Empty, AES_Encrypt(strValue))}, i)
                                i = i + 1
                            End If
                            ' End of Encryption Fix. Remove this code in permanent fix

                            request.Attributes.SetValue(New NameValue() With {.Name = strKey, .Value = If(String.IsNullOrEmpty(strValue), String.Empty, strValue)}, i)
                            i = i + 1
                        Next

                        Try                        
                            Dim response = obc.SendExactTarget(request)
                       
                            strCommReferenceId = response.Id.ToString()

                            If response.Id <> Guid.Empty Then
                                If response.SendSuccessful = True Then
                                    strStatus = "SUCCESS"
                                Else
                                    strStatus = "FAILURE"
                                    Dim eb As New System.Xml.Serialization.XmlSerializer(response.GetType)
                                    eb.Serialize(strXML, response)
                                End If                       
                            End If
                        Catch ex As Exception
                            strStatus = "FAILURE"
                            strXml.WriteLine("Exception message:")
                            strXml.WriteLine(ex.Message)
                            strXml.WriteLine("Stack Trace:")
                            strXml.WriteLine(ex.StackTrace)
                        End Try
                    End If
                    
                    'Update Message process results    
                    intUpdateResult = PublishedTask.UpdateOutBoundMessageStatus(guidMsgId, strRecipient, strStatus, strCommReferenceId, strXml.ToString(), strComments)                     
                Next
            Else 'fail task, error got email detail 
                Throw New Exception("Get Message Err " + intErrCode.ToString() + " - " + strErrMsg)
            End If            
        Catch ex As Exception
            Logger.AddError(ex)
            Throw
        End Try
        Logger.AddDebugLogExit()
    End Sub
#End Region

#Region "Private Methods"
    Public Shared Function SerializeObj(obj As Object) As String
        Dim xs As New System.Xml.Serialization.XmlSerializer(obj.GetType)
        Dim w As New IO.StringWriter
        xs.Serialize(w, obj)
        Return w.ToString
    End Function

#End Region

End Class
