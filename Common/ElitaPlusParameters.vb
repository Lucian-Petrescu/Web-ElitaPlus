Imports System.Security.Principal

<Serializable()> _
Public Class ElitaPlusParameters
    Implements IIdentity

#Region " Constants "

    Private Const SITEMINDER_USER_NAME As String = "SM_USER"
    Private Const SITEMINDER_AUTHENTICATION_TYPE As String = "SiteMinder"
    Private Const NONE_AUTHENTICATION_TYPE As String = "None"
    Public Const UNKOWN_USER_NAME As String = "Unknown User"

#End Region

#Region " Member Variables "

    Private msAuthenticationType As String

#End Region

#Region " Constructors "

    Public Sub New()
        Me.msAuthenticationType = NONE_AUTHENTICATION_TYPE
    End Sub

#End Region

#Region " Public Members"

    Public ReadOnly Property AuthenticationType() As String Implements IIdentity.AuthenticationType
        Get
            Return Me.msAuthenticationType
        End Get
    End Property


    Public ReadOnly Property IsAuthenticated() As Boolean Implements IIdentity.IsAuthenticated
        Get
            Return True
        End Get
    End Property

    Public Overridable ReadOnly Property Name() As String Implements IIdentity.Name
        Get

        End Get
    End Property
    Public Property PrivacyUserType() As AppConfig.DataProtectionPrivacyLevel
    Public Property DBPrivacyUserType() As String

    Public Property ConnectionType() As String

    Public Property AppIV() As Byte()

    Public Property AppUserId() As String

    Public Property AppPassword() As String

    Public Property FtpHostname() As String

    Public Property FtpHostPath() As String

    Public Property FtpTriggerExtension() As String

    Public Property FtpSplitPath() As String

    Public Property ServiceOrderImageHostName() As String

    Public Property AcctBalanceHostName() As String

    Public Property CeSdk() As String

    Public Property CeDrSdk() As String

    Public Property CeViewer() As String

    Public Property CeDrViewer() As String

    Public Property FelitaFtpHostname() As String

    Public Property SmartStreamFtpHostname() As String

    Public Property SmartStreamAPUpload() As String

    Public Property SmartStreamGLUpload() As String

    Public Property SmartStreamGLStatus() As String

    Public Property LdapIp() As String

    Public Property TraceOn() As Boolean

#End Region

#Region "Static Methods"

    Public Shared ReadOnly Property CurrentParameters() As ElitaPlusParameters
        Get
            Return CType(System.Threading.Thread.CurrentPrincipal.Identity, ElitaPlusParameters)
        End Get
    End Property


#End Region
End Class
