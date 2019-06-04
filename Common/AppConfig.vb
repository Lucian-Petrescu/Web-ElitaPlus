Imports RMEncryption
Imports System.Text

Public Class AppConfig

#Region "Constants"


    Public Const HUB_REGION_ENVIRONMENT As String = "AssurNetHubRegion"
    Public Const DEFAULT_HUB_REGION As String = "NO"
    Public Const EUROPE_HUB_REGION As String = "EU"
    Public Const DEFAULT_MACHINE_DOMAIN As String = "NONE"
    Public Const DEV_ENV_USER_ID_VARIABLE_NAME As String = "ELITA_USER"
    Public Const DEFAULT_USER_ID As String = ""
    Public Const CE_NO_DR As String = "NONE"
    Public Const SECTION_SETTING As String = "settingsSection"
    Public Const SECTION_EXTERNAL As String = "externalSection"
    Public Const SERVER_PREFIX As String = "SERVER_"
    Public Const MANUAL_PREFIX As String = "MANUAL_"
    Public Const FIELD_SEPARATOR As Char = "@"c
    'Constant represents new privacy states
    Public Const DB_PRIVACY_GENERAL As String = "N" 'General Authentication
    Public Const DB_PRIVACY_BASIC As String = "Y" 'Basic/Current Authentication
    Public Const DB_PRIVACY_ADV As String = "D" 'GDPR Advanced Authentication


#End Region

#Region "ENUM:Privacy Levels"
    Public Enum DataProtectionPrivacyLevel
        Privacy_Basic = 1
        Privacy_General = 2
        Privacy_DataProtection = 3
    End Enum
#End Region

#Region "Common"
    Public Shared _AppIV As Byte()

    Public Shared ReadOnly Property ConnType() As String
        Get
            Return ElitaPlusParameters.CurrentParameters.ConnectionType
        End Get
    End Property
    '*********************************************************************************************************************
    '*** This is the defaul value that RMEncryption uses. This should be use to set RMEncryption back to default IV
    '*********************************************************************************************************************
    Public Shared Function AppDefaultIV() As Byte()
        Return New Byte() {10, 61, 235, 120, 122, 120, 80, 248, 13, 182, 196, 212, 176, 46, 23, 85}
    End Function

  
    Public Shared Function AppEncrypt(ByVal sValue As String) As String
        Dim sEncryptStr As String
        Try
            RMEncryptor.ivb = AppDefaultIV()
            sEncryptStr = RMEncryptor.Encrypt(sValue)
        Catch ex As Exception
            sEncryptStr = ""
        End Try

        Return sEncryptStr
    End Function
    Public Shared Function AppEncrypt(ByVal sValue As String, ByVal bIV() As Byte) As String
        Dim sEncryptStr As String
        Try
            RMEncryptor.ivb = bIV
            sEncryptStr = RMEncryptor.Encrypt(sValue)
            RMEncryptor.ivb = AppDefaultIV()
        Catch ex As Exception
            sEncryptStr = ""
        End Try

        Return sEncryptStr
    End Function


    Public Shared Function AppDecrypt(ByVal sEncrypted As String) As String
        Dim sDecrypt As String
        Try
            RMEncryptor.ivb = AppDefaultIV()
            sDecrypt = RMEncryptor.Decrypt(sEncrypted)
        Catch ex As Exception
            sDecrypt = ""
        End Try

        Return sDecrypt
    End Function

    Public Shared Function AppDecrypt(ByVal sEncrypted As String, ByVal bIV() As Byte) As String
        Dim sDecrypt As String

        Try
            RMEncryptor.ivb = bIV
            'RMEncryptor.ivb = AppDefaultIV()
            sDecrypt = RMEncryptor.Decrypt(sEncrypted)
            RMEncryptor.ivb = AppDefaultIV()
        Catch ex As Exception
            sDecrypt = ""
        End Try

        Return sDecrypt
    End Function

#End Region

#Region "Database Config"
    Public Class DataBase

        Public Shared ReadOnly Property ConnectionString() As String
            Get
                Dim sStrConnect As String

                sStrConnect = "Validate Connection=true;" & "User ID=" & UserId & ";Password=" & Password & ";Data Source=" & Server
                ' sStrConnect = "Validate Connection=true;" & "User ID=elp_app_user" & ";Password=elp1int" & ";Data Source=" & Server
                Return sStrConnect
            End Get
        End Property

        Public Shared ReadOnly Property Server() As String
            Get
                Dim sStrConnect As String = AppConfig.Config.GetSettingValue(
                    AppConfig.MANUAL_PREFIX & "DB_CONNECTION")

                If String.IsNullOrWhiteSpace(sStrConnect) Then
                    sStrConnect = Assurant.Elita.Configuration.ElitaConfig.Current.Database.DataSourceName
                End If

                If String.IsNullOrWhiteSpace(sStrConnect) Then
                    ' Dynamic, data source is not supplied by the configuration file, support windows service that connect to different hubs
                    sStrConnect = "elitaplus_" & EnvironmentContext.Current.EnvironmentShortName
                    If ConnType <> DEFAULT_HUB_REGION Then
                        sStrConnect &= "_" & ConnType.ToLower
                    End If
                End If

                Return sStrConnect
                ' If you need to modify the Connection string for testing, go to Settings.xml

            End Get
        End Property

        Public Shared ReadOnly Property AppIV() As Byte()
            Get
                Try
                    _AppIV = ElitaPlusParameters.CurrentParameters.AppIV
                Catch ex As Exception
                    ' The IV is encrypted with the default IV of RMEncryptor. 
                    ' That is a hardcoded IV in RMencryptor and is = to AppDefaultIV()
                    Dim sStrIV As String = AppConfig.Config.GetSettingValue("ELITA_INITVECTOR")

                    If sStrIV = String.Empty Then
                        _AppIV = AppDefaultIV()
                    Else
                        RMEncryptor.ivb = AppDefaultIV()

                        Dim sEncrypted_IV As String = RMEncryptor.Encrypt(sStrIV) & "1234567890123456"
                        _AppIV = Encoding.ASCII.GetBytes(sEncrypted_IV.Substring(1, 16))
                    End If
                End Try

                Return _AppIV
            End Get
        End Property

        Public Shared ReadOnly Property UserId() As String
            Get
                Return ElitaPlusParameters.CurrentParameters.AppUserId
            End Get
        End Property

        Public Shared ReadOnly Property UserId(ByVal privacyUserType As AppConfig.DataProtectionPrivacyLevel) As String
            Get
                Dim sStrConnect As String = AppConfig.Config.GetSettingValue(AppConfig.MANUAL_PREFIX & "DB_USERID")
                If String.IsNullOrWhiteSpace(sStrConnect) Then
                    If privacyUserType = AppConfig.DataProtectionPrivacyLevel.Privacy_DataProtection Then
                        'site need privacy control and user are part of DP group
                        sStrConnect = Elita.Configuration.ElitaConfig.Current.Database.DataProtectionUserName

                    ElseIf privacyUserType = AppConfig.DataProtectionPrivacyLevel.Privacy_Basic Then
                        'site need privacy control and user are part of Secure group
                        sStrConnect = Assurant.Elita.Configuration.ElitaConfig.Current.Database.SecuredUserName
                    Else
                        sStrConnect = Assurant.Elita.Configuration.ElitaConfig.Current.Database.UserName
                    End If

                End If

                Return sStrConnect
            End Get
        End Property
        Public Shared ReadOnly Property Password() As String
            Get
                Return ElitaPlusParameters.CurrentParameters.AppPassword
            End Get
        End Property

        Public Shared ReadOnly Property Password(ByVal privacyUserType As AppConfig.DataProtectionPrivacyLevel) As String
            Get
                Dim sStrConnect As String = AppConfig.Config.GetSettingValue(AppConfig.MANUAL_PREFIX & "DB_PASSWORD")

                If String.IsNullOrWhiteSpace(sStrConnect) Then
                    If privacyUserType = AppConfig.DataProtectionPrivacyLevel.Privacy_DataProtection Then
                        'site need privacy control and user are part of DP group
                        sStrConnect = Assurant.Elita.Configuration.ElitaConfig.Current.Database.DataProtectionPassword
                    ElseIf privacyUserType = AppConfig.DataProtectionPrivacyLevel.Privacy_Basic Then
                        'site need privacy control and user are part of Secure group
                        sStrConnect = Assurant.Elita.Configuration.ElitaConfig.Current.Database.SecuredPassword
                    Else
                        sStrConnect = Assurant.Elita.Configuration.ElitaConfig.Current.Database.Password
                    End If

                End If

                Return sStrConnect
            End Get
        End Property
      
    End Class
#End Region

#Region "UNIX Server"

    Public Class UnixServer

        Public Shared ReadOnly Property HostName() As String
            Get
                Return ElitaPlusParameters.CurrentParameters.FtpHostname
            End Get
        End Property

        Public Shared ReadOnly Property UserId() As String
            Get
                Dim sStrConnect As String

                sStrConnect = AppConfig.Config.GetSettingValue("FTP_USERID")

                If String.IsNullOrWhiteSpace(sStrConnect) Then
                    sStrConnect = Assurant.Elita.Configuration.ElitaConfig.Current.UnixServer.UserId
                End If

                Return sStrConnect
            End Get
        End Property

        Public Shared ReadOnly Property Password() As String
            Get
                Dim sStrConnect As String

                sStrConnect = AppConfig.Config.GetSettingValue("FTP_PASSWORD")

                If String.IsNullOrWhiteSpace(sStrConnect) Then
                    sStrConnect = Assurant.Elita.Configuration.ElitaConfig.Current.UnixServer.Password
                End If

                Return sStrConnect
            End Get
        End Property

        Public Shared ReadOnly Property FtpDirectory() As String
            Get
                Return ElitaPlusParameters.CurrentParameters.FtpHostPath
            End Get
        End Property

        Public Shared ReadOnly Property FtpDirectorySplit() As String
            Get
                Return ElitaPlusParameters.CurrentParameters.FtpSplitPath
            End Get
        End Property

        Public Shared ReadOnly Property InterfaceDirectory() As String
            Get
                Dim sStrConnect As String

                sStrConnect = AppConfig.Config.GetSettingValue("FTP_DIRECTORY")

                If String.IsNullOrWhiteSpace(sStrConnect) Then
                    sStrConnect = Assurant.Elita.Configuration.ElitaConfig.Current.UnixServer.InterfaceDirectory
                End If

                Return sStrConnect
            End Get
        End Property

        Public Shared ReadOnly Property FtpTriggerExtension() As String
            Get
                Return ElitaPlusParameters.CurrentParameters.FtpTriggerExtension
            End Get
        End Property

    End Class
#End Region

#Region "FELITA UNIX Server"

    Public Class Felita

        Public Shared ReadOnly Property FTPHostName() As String
            Get
                Return ElitaPlusParameters.CurrentParameters.FelitaFtpHostname
            End Get
        End Property

        Public Shared ReadOnly Property ReconUserId() As String
            Get
                Return Assurant.Elita.Configuration.ElitaConfig.Current.Felita.ReconServerUserId
            End Get
        End Property

        Public Shared ReadOnly Property ReconPassword() As String
            Get
                Return Assurant.Elita.Configuration.ElitaConfig.Current.Felita.ReconServerPassword
            End Get
        End Property

        Public Shared ReadOnly Property UserId() As String
            Get
                Return Assurant.Elita.Configuration.ElitaConfig.Current.Felita.FelitaServerUserId
            End Get
        End Property

        Public Shared ReadOnly Property Password() As String
            Get
                Return Assurant.Elita.Configuration.ElitaConfig.Current.Felita.FelitaServerPassword
            End Get
        End Property

        Public Shared ReadOnly Property WCFUploadEndpoint() As String
            Get
                Return Assurant.Elita.Configuration.ElitaConfig.Current.Felita.WcfUploadEndPoint
            End Get
        End Property


    End Class
#End Region

#Region "SMARTSTREAM CONFIGURATION"

    Public Class SmartStream

        Public Shared ReadOnly Property FTPHostName() As String
            Get
                Return ElitaPlusParameters.CurrentParameters.SmartStreamFtpHostname
            End Get
        End Property

        Public Shared ReadOnly Property FTPUserId() As String
            Get
                Return Assurant.Elita.Configuration.ElitaConfig.Current.SmartStream.FtpUserId
            End Get
        End Property

        Public Shared ReadOnly Property FTPPassword() As String
            Get
                Return Assurant.Elita.Configuration.ElitaConfig.Current.SmartStream.FtpPassword
            End Get
        End Property

        Public Shared ReadOnly Property WCFGLUserId() As String
            Get
                Return Assurant.Elita.Configuration.ElitaConfig.Current.SmartStream.WcfGLUserId
            End Get
        End Property

        Public Shared ReadOnly Property WCFGLPassword() As String
            Get
                Return Assurant.Elita.Configuration.ElitaConfig.Current.SmartStream.WcfGLPassword
            End Get
        End Property

        Public Shared ReadOnly Property WCFGLUploadEndpoint() As String
            Get
                Return ElitaPlusParameters.CurrentParameters.SmartStreamGLUpload
            End Get
        End Property

        Public Shared ReadOnly Property WCFGLStatusEndpoint() As String
            Get
                Return ElitaPlusParameters.CurrentParameters.SmartStreamGLStatus
            End Get
        End Property

        Public Shared ReadOnly Property WCFAPUploadEndpoint() As String
            Get
                Return ElitaPlusParameters.CurrentParameters.SmartStreamAPUpload
            End Get
        End Property

        Public Shared ReadOnly Property WCFGLCertificateName() As String
            Get
                Return Assurant.Elita.Configuration.ElitaConfig.Current.SmartStream.WcfGLCertName
            End Get
        End Property

        Public Shared ReadOnly Property WCFAPCertificateName() As String
            Get
                Return Assurant.Elita.Configuration.ElitaConfig.Current.SmartStream.WcfAPCertName
            End Get
        End Property

        Public Shared ReadOnly Property WCFAPUserId() As String
            Get
                Return Assurant.Elita.Configuration.ElitaConfig.Current.SmartStream.WcfAPUserId
            End Get
        End Property

        Public Shared ReadOnly Property WCFAPPassword() As String
            Get
                Return Assurant.Elita.Configuration.ElitaConfig.Current.SmartStream.WcfAPPassword
            End Get
        End Property



    End Class
#End Region

#Region "ServiceOrderEmail"

    Public Class ServiceOrderEmail


        Public Shared ReadOnly Property AttachDir() As String
            Get
                Return Assurant.Elita.Configuration.ElitaConfig.Current.General.ServiceOrderEmailAttachDirectory
            End Get
        End Property

        Public Shared ReadOnly Property SmtpServer() As String
            Get
                Return Assurant.Elita.Configuration.ElitaConfig.Current.General.ServiceOrderEmailSMTPServer
            End Get
        End Property

    End Class

#End Region

#Region "Caching"

    Private Shared _commonCache As Cache = Nothing
    Public Shared ReadOnly Property CommonCache() As Cache
        Get
            If _commonCache Is Nothing Then
                _commonCache = New Cache(CommonCacheEntryMaxAgeInMin())
            End If
            Return _commonCache
        End Get
    End Property

    Public Shared ReadOnly Property CommonCacheEntryMaxAgeInMin() As Long
        Get
            Return Long.Parse(Assurant.Elita.Configuration.ElitaConfig.Current.General.CacheMaxAgeInMinutes)
        End Get
    End Property

    Public Shared Sub ClearCache()
        CommonCache.InvalidateAllEntries()
    End Sub

#End Region

#Region "Environment"

    Public Shared ReadOnly Property HubRegion() As String
        Get
            ' Dim sHubRegion As String = DEFAULT_HUB_REGION
            Dim sHubRegion As String = String.Empty
            Dim sHubRegionEnv As String = System.Environment.GetEnvironmentVariable(HUB_REGION_ENVIRONMENT)

            If sHubRegionEnv Is Nothing Then
                If ((EnvironmentContext.Current.Environment = Environments.Development) OrElse (EnvironmentContext.Current.Environment = Environments.Test)) Then
                    sHubRegion = DEFAULT_HUB_REGION
                End If
            Else
                sHubRegion = sHubRegionEnv.ToUpper
            End If

            Return sHubRegion
        End Get
    End Property

    Public Shared ReadOnly Property DevEnvUserId() As String
        Get
            Dim envVar As String = System.Environment.GetEnvironmentVariable(DEV_ENV_USER_ID_VARIABLE_NAME)
            If envVar Is Nothing Then
                envVar = DEFAULT_USER_ID
            End If
            envVar = envVar.ToUpper
            Return envVar
        End Get
    End Property
#End Region


#Region "Publishing, Logging and Debugging"
    Public Const DEBUG_MESSAGE_TYPE As String = "DEBUG"
    'Public Const EVENT_LOG As String = "Assurant.Common.MessagePublishing.WindowsEventLogPublisher"
    'Public Const EMAIL_LOG As String = "Assurant.Common.MessagePublishing.EMailMessagePublisher"
    'Public Const FILE_LOG As String = "Assurant.Common.MessagePublishing.FileMessagePublisher"
    'Public Const DB_LOG As String = "Assurant.Common.MessagePublishing.DBMessagePublisher"
    Public Const EVENT_LOG As Integer = 0
    '  Public Const EMAIL_LOG As Integer = 1
    '  Public Const FILE_LOG As Integer = 2
    Public Const DB_LOG As Integer = 1

    'Private Shared publishingConfig As PublishingConfiguration

    'Public Shared ReadOnly Property CurrentPublishingConfiguration() As PublishingConfiguration
    '    Get
    '        If publishingConfig Is Nothing Then

    '            'load and parse the configuration
    '            Dim configHandler As New PublishingConfigurationSectionHandler
    '            Dim oXml As Xml.XmlNode = ConfigReader.GetNode(GetType(AppConfig), "/CONFIG/LOGGING/publishingConfiguration")
    '            ' DBMessagePublisher
    '            Try
    '                ConfigReader.AddNodeAttribute(GetType(AppConfig), oXml.ChildNodes(DB_LOG), _
    '                                         "DBConnString", AppConfig.DataBase.ConnectionString)
    '                '    publishingConfig = configHandler.Create(Nothing, Nothing, oXml)
    '            Catch ex As Exception
    '            Finally
    '                publishingConfig = configHandler.Create(Nothing, Nothing, oXml)
    '            End Try

    '            'if we have a config already, check to be sure we have a dbconnstring.  if not, add it.
    '        ElseIf CType(publishingConfig.Publishers(DB_LOG), Assurant.Common.MessagePublishing.PublisherSettings).SpecificAttributes("DBConnString") Is Nothing Then

    '            Try
    '                CType(publishingConfig.Publishers(DB_LOG), Assurant.Common.MessagePublishing.PublisherSettings).SpecificAttributes.Add("DBConnString", AppConfig.DataBase.ConnectionString)
    '            Catch ex As Exception
    '                'Do nothing because we don't have a connect string
    '            End Try
    '        End If

    '        Return publishingConfig

    '    End Get
    'End Property

    Public Shared ReadOnly Property CurrentPublishingConfiguration() As PublishingConfiguration
        Get
            Dim publishingConfig As PublishingConfiguration

            'load and parse the configuration
            Dim configHandler As New PublishingConfigurationSectionHandler
            Dim oXml As Xml.XmlNode = ConfigReader.GetNode(GetType(AppConfig), "/CONFIG/LOGGING/publishingConfiguration").Clone

            ' DBMessagePublisher
            Try
                ConfigReader.AddNodeAttribute(GetType(AppConfig), oXml.ChildNodes(DB_LOG),
                                         "DBConnString", AppConfig.DataBase.ConnectionString)
            Catch ex As Exception
            Finally
                publishingConfig = configHandler.Create(Nothing, Nothing, oXml)
            End Try

            Return publishingConfig

        End Get
    End Property


    Public Shared Sub SetModeLog(ByVal logType As Integer, ByVal mode As Boolean)
        'Dim pubList As ArrayList = CurrentPublishingConfiguration.Publishers
        'Dim pub As PublisherSettings
        'Dim index As Integer = 0

        'For Each pub In pubList
        '    If index = logType Then
        '        If mode = True Then
        '            pub.Mode = PublishingMode.On
        '        Else
        '            pub.Mode = PublishingMode.Off
        '        End If
        '        Exit For
        '    End If
        '    index += 1
        'Next


    End Sub

    Public Shared Sub Log(ByVal message As IPublishableMessage)
        PublishingManager.Publish(message, CurrentPublishingConfiguration)
    End Sub

    Public Shared Sub Log(ByVal ex As Exception)
        PublishingManager.Publish(ex, CurrentPublishingConfiguration)
    End Sub

    Public Shared Sub Log(ByVal message As String)
        PublishingManager.Publish(message, CurrentPublishingConfiguration)
    End Sub

    Public Shared Sub DebugLog(ByVal message As String)
        Log(New DebugMessage(message))
    End Sub

    Public Shared Sub Debug(ByVal msg As String)
        Try
            AppConfig.DebugLog(msg)
        Catch ex1 As Exception
        End Try
    End Sub

    Public Class DebugMessage
        Inherits BasePublishableMessage

        Public Sub New(ByVal message As String)
            MyBase.New(message, DEBUG_MESSAGE_TYPE)
        End Sub

        Public Shared ReadOnly Property TraceOn() As Boolean
            Get
                Try
                    If Not ElitaPlusParameters.CurrentParameters Is Nothing Then
                        Return ElitaPlusParameters.CurrentParameters.TraceOn
                    Else
                        Return Boolean.Parse(System.Configuration.ConfigurationSettings.AppSettings("TRACE_ON"))
                    End If
                Catch ex As Exception
                    Return Boolean.Parse(System.Configuration.ConfigurationSettings.AppSettings("TRACE_ON"))
                End Try

            End Get
        End Property

        Public Shared Sub Trace(ByVal id As String, ByVal pageName As String, ByVal msg As String)
            If TraceOn = True Then
                Dim fullMsg As String = "Tid=" & id & "@ Pname=" & pageName & "@ " & msg
                Debug(fullMsg)
            End If
        End Sub

    End Class



#End Region

#Region "File Management"

    Public Shared ReadOnly Property FileClientDirectory() As String
        Get
            Return Assurant.Elita.Configuration.ElitaConfig.Current.General.FileClientDirectory
        End Get
    End Property


#End Region

#Region "SSRS"

    Public Class SS

        Public Shared ReadOnly Property MachineName() As String
            Get
                Return ElitaPlusParameters.CurrentParameters.CeSdk
            End Get
        End Property

        Public Shared ReadOnly Property MachineNameDr() As String
            Get
                Return ElitaPlusParameters.CurrentParameters.CeDrSdk
            End Get
        End Property

        Public Shared ReadOnly Property ViewerMachineName() As String
            Get
                Return ElitaPlusParameters.CurrentParameters.CeViewer
            End Get
        End Property

        Public Shared ReadOnly Property ViewerMachineNameDr() As String
            Get
                Return ElitaPlusParameters.CurrentParameters.CeDrViewer
            End Get
        End Property

        Public Shared ReadOnly Property RootDir() As String
            Get
                Dim sStrConnect As String
                'sStrConnect = "ElitaPlus1" 
                sStrConnect = Assurant.Elita.Configuration.ElitaConfig.Current.SSRSReport.RootDirectory
                Return sStrConnect
            End Get
        End Property

        Public Shared ReadOnly Property UserId() As String
            Get
                Dim sStrConnect As String
                'sStrConnect = "os08rp" 'Assurant.Elita.Configuration.ElitaConfig.Current.SSRSReport.UserId
                sStrConnect = Assurant.Elita.Configuration.ElitaConfig.Current.SSRSReport.UserId
                Return sStrConnect
            End Get
        End Property

        Public Shared ReadOnly Property Password() As String
            Get
                Dim sStrConnect As String
                'sStrConnect = "Taaz2017" 'Assurant.Elita.Configuration.ElitaConfig.Current.SSRSReport.Password
                sStrConnect = Assurant.Elita.Configuration.ElitaConfig.Current.SSRSReport.Password
                Return sStrConnect
            End Get
        End Property

        Public Shared ReadOnly Property ViewerDir() As String
            Get
                Dim sStrConnect As String
                sStrConnect = "TBD" 'Assurant.Elita.Configuration.ElitaConfig.Current.SSRSReport.ViewerDirectory
                Return sStrConnect
            End Get
        End Property
        Public Shared ReadOnly Property DestinationDir() As String
            Get
                Dim sStrConnect As String
                sStrConnect = "TBD" 'Assurant.Elita.Configuration.ElitaConfig.Current.SSRSReport.DestinationDirectory
                Return sStrConnect
            End Get
        End Property

        Public Shared ReadOnly Property ReportPath() As String
            Get
                Dim sStrConnect As String
                sStrConnect = "Reports" 'Assurant.Elita.Configuration.ElitaConfig.Current.SSRSReport.ReportPath
                sStrConnect = Assurant.Elita.Configuration.ElitaConfig.Current.SSRSReport.ReportPath
                Return sStrConnect
            End Get
        End Property

        Public Shared ReadOnly Property ReportServerUrl() As String
            Get
                Dim sStrConnect As String
                'sStrConnect = "http://atl0wsrsd020.cead.prd/ReportServer" 'Assurant.Elita.Configuration.ElitaConfig.Current.SSRSReport.ReportServerUrl
                sStrConnect = Assurant.Elita.Configuration.ElitaConfig.Current.SSRSReport.ReportServerUrl
                Return sStrConnect
            End Get
        End Property
        Public Shared ReadOnly Property Domain() As String
            Get
                Dim sStrConnect As String
                sStrConnect = "prodcead" 'Assurant.Elita.Configuration.ElitaConfig.Current.SSRSReport.Domain
                sStrConnect = Assurant.Elita.Configuration.ElitaConfig.Current.SSRSReport.Domain
                Return sStrConnect
            End Get
        End Property
    End Class
#End Region

#Region "Application"
    Public Class Application

        Public Shared ReadOnly Property CeTimeout() As String
            Get
                Return Assurant.Elita.Configuration.ElitaConfig.Current.General.CEMinTimeOut
            End Get
        End Property

    End Class
#End Region

#Region "Retrieve Config"

    Public Class Config

        Public Shared Function GetCustomConfigValue(ByVal sectionName As String, ByVal keyName As String) As String
            Dim value As String
            Dim data As System.Collections.Specialized.NameValueCollection
            data = System.Configuration.ConfigurationSettings.GetConfig(sectionName)
            value = data.Get(keyName)
            Return value
        End Function

        Public Shared Function GetSettingValue(ByVal keyName As String) As String
            Return GetCustomConfigValue(AppConfig.SECTION_SETTING, keyName)
        End Function

        Public Shared Function GetExternalValue(ByVal keyName As String) As String
            Return GetCustomConfigValue(AppConfig.SECTION_EXTERNAL, keyName)
        End Function

    End Class


#End Region

#Region "WebService"
    Public Class WebService

        ' ApplicationId ¦ UserId ¦ LDAP Group
        Public Shared ReadOnly Property ComplexName(ByVal networkId As String) As String
            Get
                Dim sStrConnect As String = AppId() & FIELD_SEPARATOR & networkId & FIELD_SEPARATOR & Group()

                Return sStrConnect
            End Get
        End Property

        '' ApplicationId ¦ UserId ¦ LDAP Group
        '' In this case ApplicationId = UserId
        'Public Shared ReadOnly Property ComplexName(ByVal networkId As String, ByVal ldapGroup As String) As String
        '    Get
        '        Dim sStrConnect As String = networkId & FIELD_SEPARATOR & networkId & FIELD_SEPARATOR & ldapGroup

        '        Return sStrConnect
        '    End Get
        'End Property

        Public Shared ReadOnly Property AppId() As String
            Get
                Dim sStrConnect As String
                sStrConnect = Assurant.Elita.Configuration.ElitaConfig.Current.WebService.UserId
                Return sStrConnect
            End Get
        End Property

        ' ApplicationId ¦ UserId ¦ LDAP Group
        Public Shared ReadOnly Property AppId(ByVal complexName As String) As String
            Get
                Dim appUsername As String
                Dim sepPos As Integer = complexName.IndexOf(FIELD_SEPARATOR)

                appUsername = complexName.Substring(0, sepPos)

                Return appUsername
            End Get
        End Property

        ' ApplicationId ¦ UserId ¦ LDAP Group
        Public Shared ReadOnly Property UserId(ByVal complexName As String) As String
            Get
                Dim username As String
                Dim sepPos As Integer = complexName.IndexOf(FIELD_SEPARATOR)
                Dim lastSepPos As Integer = complexName.LastIndexOf(FIELD_SEPARATOR)

                username = complexName.Substring(sepPos + 1, lastSepPos - sepPos - 1)

                Return username
            End Get
        End Property

        Public Shared ReadOnly Property Password() As String
            Get
                Dim sStrConnect As String
                sStrConnect = Assurant.Elita.Configuration.ElitaConfig.Current.WebService.Password
                Return sStrConnect
            End Get
        End Property

        Public Shared ReadOnly Property Group() As String
            Get
                Dim sStrConnect As String
                sStrConnect = Assurant.Elita.Configuration.ElitaConfig.Current.WebService.Group
                Return sStrConnect
            End Get
        End Property

        ' ApplicationId ¦ UserId ¦ LDAP Group
        Public Shared ReadOnly Property Group(ByVal complexName As String) As String
            Get
                Dim ldapGroup As String
                Dim lastSepPos As Integer = complexName.LastIndexOf(FIELD_SEPARATOR)

                ldapGroup = complexName.Substring(lastSepPos + 1)

                Return ldapGroup
            End Get
        End Property

    End Class
#End Region

#Region "Ldap"

    Public Class Ldap

        Public Shared ReadOnly Property LdapIp() As String
            Get
                Return ElitaPlusParameters.CurrentParameters.LdapIp
            End Get
        End Property

    End Class
#End Region

End Class
