Imports System.DirectoryServices
Imports RMEncryption
Imports System.Collections.Generic
'Imports Assurant.Elita.Configuration

Public Class Authentication

#Region "Constants"

    '  Private Const LDAP_SERVER As String = "LDAP://directory.assurant.com:389"
    '  Private Const LDAP_SERVER As String = "LDAP://172.31.3.118" Atlanta
    '  Private Const LDAP_SERVER As String = "LDAP://172.30.3.118" Miami
    Private Const LDAP_ROOT As String = "dc=assurant,dc=com"
    Private Const LDAP_BASEDN As String = "ou=AssurNet,o=Applications," & LDAP_ROOT
    Public Const LDAP_ELITAPLUS_GROUP As String = "ElitaPlus"
    Public Const LDAP_EUROPE_GROUP As String = "Europe"
   
    Private _PrivacyUserType As AppConfig.DataProtectionPrivacyLevel
#End Region


#Region "Logged In User Info Shortcuts"

    Public Shared ReadOnly Property CurrentUser() As User
        Get
            Return ElitaPlusIdentity.Current.ActiveUser
        End Get
    End Property

    Public Shared ReadOnly Property LangId() As Guid
        Get
            Return ElitaPlusIdentity.Current.ActiveUser.LanguageId
        End Get
    End Property

    Public Shared ReadOnly Property CompId() As Guid
        Get
            Return ElitaPlusIdentity.Current.ActiveUser.CompanyId
        End Get
    End Property

    Public Shared ReadOnly Property CompIds() As ArrayList
        Get
            Return ElitaPlusIdentity.Current.ActiveUser.Companies
        End Get
    End Property

    Public Shared ReadOnly Property CountryIds() As ArrayList
        Get
            Return ElitaPlusIdentity.Current.ActiveUser.Countries
        End Get
    End Property

    Public Shared ReadOnly Property CompanyGroupId() As Guid
        Get
            Return ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
        End Get
    End Property

    Public Shared ReadOnly Property CompanyGroupCode() As String
        Get
            Return ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Code
        End Get
    End Property


#End Region

#Region "LDAP User-Group"

    Public Shared Function GetLdapServer(ByVal path As String, ByVal userId As String,
    ByVal password As String, ByVal authenticationType As AuthenticationTypes) As DirectoryEntry
        Const LDAP_SERVER_PREFIX As String = "LDAP://"
        Dim entry As DirectoryEntry
        Dim fPath As String
        Dim name As String

        ' Try
        fPath = LDAP_SERVER_PREFIX & AppConfig.Ldap.LdapIp & path
        ' fPath = LDAP_SERVER_PREFIX & "directorydev.assurant.com" & path
        entry = New DirectoryEntry(fPath, userId, password, authenticationType)
        name = entry.Name
        '  Catch ex As Exception
        ' It will try Data Recovery
        'fPath = LDAP_SERVER_PREFIX & AppConfig.Ldap.LdapDrIp & path
        'entry = New DirectoryEntry(fPath, userId, password, authenticationType)
        '  End Try

        Return entry

    End Function

    Public Shared Function IsLdapUserInGroup(ByVal group As String, ByVal networkId As String, ByVal entry As DirectoryEntry) As Boolean
        Dim oSearcher As New DirectorySearcher
        Dim oResult As SearchResult
        Dim ResultFields() As String = {"member", "cn"}
        Dim isUserInGroup As Boolean = False
        Dim oUserCapitalCase As String

        Dim i As Integer
        Try
            With oSearcher
                .SearchRoot = entry
                .PropertiesToLoad.AddRange(ResultFields)
                ' .Filter = "(& (cn=" & group & ")(member=uid=" & networkId & ",*) )"
                If entry.Username Is String.Empty Then
                    .Filter = "(& (cn=" & group & ")(member=*) )"
                Else
                    .Filter = "(& (cn=" & group & ")(member=" & entry.Username & ") )"
                End If

                .SearchScope = SearchScope.Subtree
                oResult = .FindOne()
            End With

            If (Not oResult Is Nothing) Then
                If (entry.Username Is String.Empty) Then
                    oUserCapitalCase = ("uid=" & networkId & ",").ToUpper
                    For Each sUserName As String In oResult.Properties("member")
                        If sUserName.ToUpper.StartsWith(oUserCapitalCase) Then
                            isUserInGroup = True
                            Exit For
                        End If
                    Next
                Else
                    isUserInGroup = True
                End If

                ' User Found in the specified Group
                '  isUserInGroup = True
            End If

        Catch e As Exception
            ' User Not Found in the specified Group
            isUserInGroup = False
        End Try

        Return isUserInGroup
    End Function

    Public Shared Function IsLdapUserInGroup(ByVal group As String, ByVal networkId As String) As Boolean
        Dim entry As DirectoryEntry
        Dim isUserInGroup As Boolean
        Dim path As String = "/cn=" & group & "," & LDAP_BASEDN

        Try
            entry = GetLdapServer(path, "", "", AuthenticationTypes.Anonymous)
            isUserInGroup = IsLdapUserInGroup(group, networkId, entry)
        Catch e As Exception
            ' The User is not Authorized or It does not belong to the specified group
            isUserInGroup = False
        End Try

        Return isUserInGroup
    End Function


    Public Shared Function GetLdapUser(ByVal group As String, ByVal networkId As String, ByVal password As String) As DirectoryEntry
        Dim path As String = "/cn=" & LDAP_ELITAPLUS_GROUP & "," & LDAP_BASEDN
        Dim entry As DirectoryEntry
        Dim ldapUserId, userType As String
        Dim isUserInGroup As Boolean


        userType = group
        ldapUserId = "uid=" & networkId & ",o=" & userType & ",dc=assurant,dc=com"
        Try
            entry = GetLdapServer(path, ldapUserId, password, AuthenticationTypes.FastBind)
            '   entry = GetLdapServer(path, "", "", AuthenticationTypes.Anonymous)
            isUserInGroup = IsLdapUserInGroup(LDAP_ELITAPLUS_GROUP, networkId, entry)
            If isUserInGroup = False Then
                ' The User is not Authorized or It does not belong to the specified group
                entry = Nothing
            End If
        Catch e As Exception
            AppConfig.Debug("LDAP Error " & e.Message)
            ' The User is not Authorized
            entry = Nothing
        End Try

        Return entry

    End Function


    Public Shared Function IsLdapUser(ByVal group As String, ByVal networkId As String, ByVal password As String) As Boolean
        Dim bValidUser As Boolean = True
        Dim userEntry As DirectoryEntry

        userEntry = GetLdapUser(group, networkId, password)
        If userEntry Is Nothing Then
            bValidUser = False
        End If

        Return bValidUser
    End Function

#End Region

#Region "LDAP-Read Properties"

    Public Shared Function GetLdapFields(ByVal networkId As String, ByVal resultFields As String()) As IDictionary(Of String, Object)
        Dim searchResult As SearchResult
        Dim returnValue As New Dictionary(Of String, Object)

        Dim directoryEntry As New DirectoryEntry("LDAP://" + AppConfig.Ldap.LdapIp + "/dc=Assurant,dc=com", String.Empty, String.Empty, AuthenticationTypes.Anonymous)
        Try
            Using searcher As New DirectorySearcher()
                searcher.SearchRoot = directoryEntry
                searcher.PropertiesToLoad.AddRange(resultFields)

                searcher.Filter = String.Format("(uid={0})", networkId)

                searcher.SearchScope = SearchScope.Subtree
                searchResult = searcher.FindOne()
            End Using

            If (searchResult IsNot Nothing) Then
                For Each resultField As String In resultFields
                    If (searchResult.Properties.Contains(resultField)) Then
                        Dim result As ResultPropertyValueCollection = searchResult.Properties(resultField)
                        If (result.Count = 1) Then
                            returnValue.Add(resultField, result(0))
                        Else
                            returnValue.Add(resultField, result)
                        End If
                    End If
                Next
            End If

        Catch generatedExceptionName As Exception
            ' User Not Found or Mail not configured
        End Try

        Return returnValue
    End Function

#End Region

#Region "Application Initialization"

    'Public ReadOnly Property Hub() As String
    '    Get
    '        Dim oQuery As String = String.Empty
    '        Dim pos As Integer
    '        Dim oHub As String = String.Empty

    '        ' REQ-6082 - Use the hub config from the config file if available
    '        oHub = ElitaConfig.Current.General.Hub
    '        If Not String.IsNullOrWhiteSpace(oHub) Then
    '            Return oHub
    '        End If

    '        'If Not System.Web.HttpContext.Current Is Nothing Then
    '        '    oQuery = System.Web.HttpContext.Current.Request.Url.Query
    '        'ElseIf Not System.ServiceModel.OperationContext.Current Is Nothing Then
    '        '    ' WCF
    '        '    oQuery = System.ServiceModel.OperationContext.Current.RequestContext.RequestMessage.Headers.To.Query
    '        'End If
    '        'If (oQuery.Length > 5) Then
    '        '    pos = oQuery.ToUpper.IndexOf("HUB=")
    '        '    If (pos > -1) Then
    '        '        oHub = oQuery.Substring(pos + 4, 2).ToUpper
    '        '    End If
    '        'End If
    '        Return oHub
    '    End Get
    'End Property

    Public ReadOnly Property ApplicationHost() As String
        Get
            Dim oHost As String = String.Empty
            If Not System.Web.HttpContext.Current Is Nothing Then
                oHost = System.Web.HttpContext.Current.Request.Url.Host
            ElseIf Not System.ServiceModel.OperationContext.Current Is Nothing Then
                ' WCF
                oHost = System.ServiceModel.OperationContext.Current.Channel.LocalAddress.Uri.Host
            ElseIf Not System.Net.Dns.GetHostName Is Nothing Then
                oHost = System.Net.Dns.GetHostName
            End If
            Return oHost
        End Get
    End Property

    Private Function GetMachineDomain() As String
        Dim sMachineDomain As String

        sMachineDomain = AppConfig.Config.GetSettingValue(AppConfig.MANUAL_PREFIX & "DOMAIN")
        If sMachineDomain = String.Empty Then
            ' Dynamic
            If EnvironmentContext.Current.Environment <> Environments.Development Then
                '  No Development
                sMachineDomain = Environment.MachineName.ToUpper.Substring(0, 4)
            Else
                ' Development
                sMachineDomain = AppConfig.DEFAULT_MACHINE_DOMAIN
            End If
        End If
        Return sMachineDomain
    End Function

    ' It returns like EU, SA the HubRegion. NO means Development, No Region
    'Private Function GetConnectionType(ByVal connHost As String) As String
    Private Function GetConnectionType() As String
        'Dim connHost As String = oAuthentication.ApplicationHost.ToUpper
        Dim connType, connPrefix, connMiddle, connSuffix As String
        Dim startIndex, subLength As Integer

        '   Session("ELITAPLUS_HOSTNAME") = "=" & connHost & "="

        connPrefix = Nothing
        connMiddle = Nothing
        connSuffix = Nothing
        connType = AppConfig.Config.GetSettingValue(
                     AppConfig.MANUAL_PREFIX & "HUB")
        If connType = String.Empty Then
            ' Get the hub from config file
            connType = Assurant.Elita.Configuration.ElitaConfig.Current.General.Hub
        End If
        Return connType
    End Function

    Private Sub StartLog()
        Dim isDebug As String = System.Configuration.ConfigurationSettings.AppSettings("DEBUG_LOG")
        If isDebug Is Nothing Then
            isDebug = "FALSE"
        End If
        isDebug = isDebug.ToUpper
        If isDebug = "TRUE" Then
            AppConfig.SetModeLog(AppConfig.DB_LOG, True)
        Else
            AppConfig.SetModeLog(AppConfig.DB_LOG, False)
        End If
    End Sub

    Private Function IsUserPrivacyGroup(ByVal networkId As String, ByVal privacyLevel As String) As Boolean
        Dim isInLdap As Boolean = False
        If privacyLevel = AppConfig.DB_PRIVACY_ADV Then
            isInLdap = IsLdapUserInGroup(Assurant.Elita.Configuration.ElitaConfig.Current.Security.DataProtectionGroup, networkId)
            If isInLdap Then
                _PrivacyUserType = AppConfig.DataProtectionPrivacyLevel.Privacy_DataProtection
            Else
                isInLdap = IsLdapUserInGroup(LDAP_EUROPE_GROUP, networkId)
                If isInLdap Then
                    _PrivacyUserType = AppConfig.DataProtectionPrivacyLevel.Privacy_Basic
                Else
                    _PrivacyUserType = AppConfig.DataProtectionPrivacyLevel.Privacy_General
                End If
            End If
        ElseIf privacyLevel = AppConfig.DB_PRIVACY_BASIC Then
            ' Verify if the user belongs to Europe group in LDAP 
            isInLdap = IsLdapUserInGroup(LDAP_EUROPE_GROUP, networkId)
            If isInLdap Then
                _PrivacyUserType = AppConfig.DataProtectionPrivacyLevel.Privacy_Basic
            Else
                _PrivacyUserType = AppConfig.DataProtectionPrivacyLevel.Privacy_General

            End If
        Else
            _PrivacyUserType = AppConfig.DataProtectionPrivacyLevel.Privacy_General
        End If
    End Function

    Private Function getDataProtectionPrivacyLevel(ByVal privacyLevel As String, ByVal userPrivacyGroups As List(Of String)) As AppConfig.DataProtectionPrivacyLevel
        Dim isInDataProtectionGroup As Boolean = False
        Dim isInSecureGroup As Boolean = False

        isInSecureGroup = userPrivacyGroups.Contains(Elita.Configuration.ElitaConfig.Current.Security.SecureGroup)
        isInDataProtectionGroup = userPrivacyGroups.Contains(Elita.Configuration.ElitaConfig.Current.Security.DataProtectionGroup)

        If privacyLevel = AppConfig.DB_PRIVACY_ADV Then
            If isInDataProtectionGroup Then
                Return AppConfig.DataProtectionPrivacyLevel.Privacy_DataProtection
            ElseIf isInSecureGroup Then
                Return AppConfig.DataProtectionPrivacyLevel.Privacy_Basic
            End If
        ElseIf privacyLevel = AppConfig.DB_PRIVACY_BASIC And isInSecureGroup Then
            Return AppConfig.DataProtectionPrivacyLevel.Privacy_Basic
        End If

        Return AppConfig.DataProtectionPrivacyLevel.Privacy_General
    End Function

    Public Function CreateCorePrincipal(ByVal networkID As String, ByVal connType As String, ByVal machineDomain As String,
                                        Optional ByVal webServiceName As String = Nothing,
                                        Optional ByVal webServiceFunctionName As String = Nothing,
                                        Optional ByVal userPrivacyGroups As List(Of String) = Nothing) As ElitaPlusPrincipal
        Dim sHostname As String = ApplicationHost.ToUpper
        'Dim appUserSuffix As String = String.Empty
        Dim oServers As Servers
        Dim bIsDebugLogin As Boolean = False
        Dim isDebugLogin As String = System.Configuration.ConfigurationSettings.AppSettings("DEBUG_LOGIN")
        Dim isTraceOn As String = System.Configuration.ConfigurationSettings.AppSettings("TRACE_ON")
        'Now set the Principal
        Dim identity As New ElitaPlusIdentity("ASSURNET")
        With identity
            .ConnectionType = connType
            .AppIV = AppConfig.DataBase.AppIV()
            .AppUserId = AppConfig.DataBase.UserId( AppConfig.DataProtectionPrivacyLevel.Privacy_General)
            .AppPassword = AppConfig.DataBase.Password( AppConfig.DataProtectionPrivacyLevel.Privacy_General)
        End With

        Dim principal As New ElitaPlusPrincipal(identity)
        System.Threading.Thread.CurrentPrincipal = DirectCast(principal, ElitaPlusPrincipal)
        StartLog()
        If Not isDebugLogin Is Nothing Then
            isDebugLogin = isDebugLogin.ToUpper
            If isDebugLogin = "TRUE" Then
                bIsDebugLogin = True
                AppConfig.Debug(GenericConstants.ELITA_PLUS_VERSION & ", " &
                   sHostname & ", Region =" & connType & ", Prefix =" & machineDomain & ", " &
                  AppConfig.DataBase.Server & ", networkID=" & networkID)
            End If
        End If

        If Not isTraceOn Is Nothing Then
            isTraceOn = isTraceOn.ToUpper
            If isTraceOn = "TRUE" Then
                identity.TraceOn = True
            Else
                identity.TraceOn = False
            End If
        Else
            identity.TraceOn = False
        End If

        ' First Access to DB that is not for Log
        oServers = New Servers(connType, machineDomain, webServiceName, webServiceFunctionName)

        identity.LdapIp = oServers.LdapIp

        ' Privacy groups
        If Not userPrivacyGroups Is Nothing Then
            ' We already have the user privacy groups
            _PrivacyUserType = getDataProtectionPrivacyLevel(oServers.PrivacyLevelXCD, userPrivacyGroups) 'PrivacyLeveXCD property will be tri-state 
            'Trace userprivacygroup issue
            Dim logEntry As String
            If (Not principal.ActiveUser Is Nothing) Then
                logEntry = " Authentication Class: _privaceygroup is not nothing; UserID=" & principal.ActiveUser.NetworkId & "; PrivacyUserType=" & _PrivacyUserType & "; Time=" & Now.ToString
            Else
                logEntry = " Authentication Class: _privaceygroup is not nothing; UserID= & principal.ActiveUser is still nothing & ; PrivacyUserType=" & _PrivacyUserType & "; Time=" & Now.ToString
            End If

            AppConfig.DebugLog(logEntry)
        Else
            ' looking for the user privacy groups using LDAP
            IsUserPrivacyGroup(networkID, oServers.PrivacyLevelXCD) 'PrivacyLeveXCD property will be tri-state 
            'Trace userprivacygroup issue
            Dim logEntry As String
            If (Not principal.ActiveUser Is Nothing) Then
                logEntry = " Authentication Class: _privaceygroup is nothing and calling LDAP; UserID=" & principal.ActiveUser.NetworkId & "; PrivacyUserType=" & _PrivacyUserType & "; Time=" & Now.ToString
            Else
                logEntry = " Authentication Class: _privaceygroup is nothing and calling LDAP; UserID= & principal.ActiveUser is still nothing & ; PrivacyUserType=" & _PrivacyUserType & "; Time=" & Now.ToString
            End If
            AppConfig.DebugLog(logEntry)
        End If

        With identity
            .FtpHostname = oServers.FtpHostname
            .FtpHostPath = oServers.FtpHostPath
            .FtpTriggerExtension = oServers.FtpTriggerExtension
            .FtpSplitPath = oServers.FtpSplitPath
            .CeDrSdk = AppConfig.CE_NO_DR
            .CeDrViewer = AppConfig.CE_NO_DR
            .FelitaFtpHostname = oServers.FelitaFtpHostname
            .LdapIp = oServers.LdapIp
            .AppIV = AppConfig.DataBase.AppIV()
            .AppUserId = AppConfig.DataBase.UserId(_PrivacyUserType)
            .AppPassword = AppConfig.DataBase.Password(_PrivacyUserType)
            .ServiceOrderImageHostName = oServers.ServiceOrderImageHost
            .SmartStreamFtpHostname = oServers.SmartStreamHostName
            .SmartStreamAPUpload = oServers.SmartStreamAPUpload
            .SmartStreamGLStatus = oServers.SmartStreamGLStatus
            .SmartStreamGLUpload = oServers.SmartStreamGLUpload
            .AcctBalanceHostName = oServers.AcctBalanceHostname
            .PrivacyUserType = _PrivacyUserType
            .DBPrivacyUserType = oServers.PrivacyLevelXCD
            .CreateUser(networkID)
        End With
 
        If bIsDebugLogin = True Then
            Dim trace As String
            If (Not userPrivacyGroups Is Nothing) Then
                trace = String.Join(",", userPrivacyGroups.ToArray())
            End If

            AppConfig.Debug("CompanyGroup=" & Authentication.CompanyGroupCode &
               "@ Language =" & GuidControl.GuidToHexString(Authentication.LangId) &
               "@Groups" & trace)
        End If



        principal.WebServiceOffLineMessage = oServers.WebServiceOffLineMessage
        principal.WebServiceFunctionOffLineMessage = oServers.WebServiceFunctionOffLineMessage


        Return principal


    End Function

    Public Function CreatePrincipalForServices(ByVal networkID As String, ByVal connTypeNTSvc As String, ByVal machineDomainSvc As String) As ElitaPlusPrincipal

        Dim connType As String
        Dim machineDomain As String

        connType = connTypeNTSvc
        machineDomain = machineDomainSvc

        Return CreateCorePrincipal(networkID, connType, machineDomain, Nothing)

    End Function
    Public Function CreatePrincipalForServices(ByVal networkID As String, ByVal connTypeNTSvc As String, ByVal machineDomainSvc As String, ByVal webServiceName As String) As ElitaPlusPrincipal

        Dim connType As String
        Dim machineDomain As String

        connType = connTypeNTSvc
        machineDomain = machineDomainSvc

        Return CreateCorePrincipal(networkID, connType, machineDomain, webServiceName)

    End Function
    Public Function CreatePrincipal(ByVal networkID As String, Optional ByVal webServiceName As String = Nothing,
                                    Optional ByVal webServiceFunctionName As String = Nothing,
                                    Optional ByVal hubRegion As String = Nothing,
                                    Optional ByVal machineDomainName As String = Nothing) As ElitaPlusPrincipal
        Dim sHostname As String = ApplicationHost.ToUpper

        Dim connType As String
        Dim machineDomain As String

        If (hubRegion Is Nothing) Then
            connType = GetConnectionType()
        Else
            connType = GetHubRegion(hubRegion)
        End If

        If (String.IsNullOrWhiteSpace(machineDomainName)) Then
            machineDomain = GetMachineDomain()
        Else
            If String.IsNullOrWhiteSpace(Assurant.Elita.Configuration.ElitaConfig.Current.General.MachineDomain) = False Then
                If machineDomainName = Assurant.Elita.Configuration.ElitaConfig.Current.General.MachineDomain Then
                    machineDomain = machineDomainName
                Else
                    machineDomain = GetMachineDomain()
                End If
            Else
                machineDomain = machineDomainName 'machine domaain not configured in config file, use the value as it is
            End If
        End If

        Return CreateCorePrincipal(networkID, connType, machineDomain, webServiceName, webServiceFunctionName)
    End Function

    Public Function CreatePrincipalBasedOnExternalGroups(ByVal networkID As String,
                                            ByVal userPrivacyGroups As List(Of String)) As ElitaPlusPrincipal
        Dim sHostname As String = ApplicationHost.ToUpper

        Dim connType As String = GetConnectionType()
        Dim machineDomain As String = GetMachineDomain()

        Return CreateCorePrincipal(networkID, connType, machineDomain, Nothing, Nothing, userPrivacyGroups)
    End Function

    Public Function GetHubRegion(ByVal hubRegion As String, Optional ByVal hostName As String = Nothing) As String

        If String.IsNullOrWhiteSpace(hubRegion) Then
            GetHubRegion = GetConnectionType()
        Else
            GetHubRegion = hubRegion.ToUpperInvariant()
        End If
    End Function

    Public Shared Sub SetCulture()
        If CType(System.Threading.Thread.CurrentPrincipal, Object).GetType Is GetType(ElitaPlusPrincipal) Then
            'Once the user is logged in we will be refreshing the culture with the user's language culture
            ' Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            '  Dim langId As Guid = langId
            Dim cultureName As String = LookupListNew.GetCodeFromId(LookupListNew.LK_LANGUAGE_CULTURES, LangId)
            System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo(cultureName)
        End If
    End Sub

#End Region

#Region "WebService"

    Public Shared Function GetWSComplexUserName() As String
        Return AppConfig.WebService.ComplexName(CurrentUser.NetworkId)
    End Function

    Public Shared Function CreateWSToken(ByVal networkId As String) As String
        Dim source, token As String

        source = EnvironmentContext.Current.EnvironmentShortName & AppConfig.FIELD_SEPARATOR &
                AppConfig.ConnType & AppConfig.FIELD_SEPARATOR &
                networkId & AppConfig.FIELD_SEPARATOR &
                System.DateTime.Now.Ticks.ToString

        token = AppConfig.AppEncrypt(source, ElitaPlusParameters.CurrentParameters.AppIV)


        '        source = networkId & AppConfig.FIELD_SEPARATOR & System.DateTime.Now.Ticks.ToString
        '       token = RMEncryptor.Encrypt(source)
        Return token
    End Function

    Public Shared Function VerifyWSToken(ByVal token As String, ByRef networkId As String, ByRef Env As String, ByRef Hub As String) As Boolean
        Dim isValid As Boolean = False
        Dim source, sDate As String
        Dim tokenDate As Long
        Dim sepPos As Integer

        source = AppConfig.AppDecrypt(token, AppConfig.DataBase.AppIV())

        'source = RMEncryptor.Decrypt(token)

        sepPos = source.IndexOf(AppConfig.FIELD_SEPARATOR)

        If sepPos > 0 Then
            Env = source.Substring(0, sepPos)

            source = source.Substring(sepPos + 1)
            sepPos = source.IndexOf(AppConfig.FIELD_SEPARATOR)

            If sepPos > 0 Then
                Hub = source.Substring(0, sepPos)

                source = source.Substring(sepPos + 1)
                sepPos = source.IndexOf(AppConfig.FIELD_SEPARATOR)

                If sepPos > 0 Then
                    networkId = source.Substring(0, sepPos)
                    sDate = source.Substring(sepPos + 1)

                    tokenDate = sDate

                    '  tokenDate = DateTime.Parse(sDate)
                    If System.DateTime.Now.AddDays(-1).Ticks < tokenDate Then
                        isValid = True
                    End If
                End If
            End If
        End If



        Return isValid
    End Function

#End Region
End Class
