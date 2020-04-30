Imports System.Threading
Imports System.Security.Principal
Imports System.Web
'Imports Assurant.ProjectAdministrationSystem.DALObjects.Security



<Serializable()>
Public NotInheritable Class ElitaPlusPrincipal
    Implements IPrincipal


#Region " Member Variables "


    Private _identity As ElitaPlusIdentity
    Private _roles As String()
    Private _unitCodesManaged As String()
    Private _unitCodesSurrogated As String()
    Private _webServiceOffLineMessage As String
    Private _webServiceFunctionOffLineMessage As String
    Private _idToken As String


#End Region



#Region " Constructors "


    Public Sub New(ByVal identity As ElitaPlusIdentity)
        Me.Initialize()

        If (Not identity Is Nothing) Then
            Me._identity = identity
        End If

    End Sub


#End Region



#Region " Private Members "


    Private Sub Initialize()
        Me._identity = Nothing
        Me._roles = Nothing
        Me._unitCodesManaged = Nothing
    End Sub



#End Region



#Region " Public Properties "


    Public ReadOnly Property Identity() As System.Security.Principal.IIdentity Implements System.Security.Principal.IPrincipal.Identity
        Get
            Return Me._identity
        End Get
    End Property
    Public Property WebServiceOffLineMessage() As String
        Get
            Return _webServiceOffLineMessage
        End Get
        Set(ByVal value As String)
            _webServiceOffLineMessage = value
        End Set
    End Property

    Public Property WebServiceFunctionOffLineMessage() As String
        Get
            Return _webServiceFunctionOffLineMessage
        End Get
        Set(ByVal value As String)
            _webServiceFunctionOffLineMessage = value
        End Set
    End Property
    Public Property IdToken() As String
        Get
            Return Me._idToken
        End Get
        Set(ByVal value As String)
            _idToken = value
        End Set
    End Property

#End Region



#Region " Public Methods "

    <Obsolete("Create/Use one of extension method from Assurant.Elita.Security.IdentityExtensions")>
    Public Function ActiveUser() As User
        Return CType(Me.Identity, ElitaPlusIdentity).ActiveUser
    End Function

    Public Function IsInRole(ByVal role As String) As Boolean Implements System.Security.Principal.IPrincipal.IsInRole
        Return CType(Me.Identity, ElitaPlusIdentity).ActiveUser.isInRole(role)
    End Function

#End Region



#Region "Static Methods"



    Public Shared ReadOnly Property Current() As ElitaPlusPrincipal
        Get
            If (TypeOf System.Threading.Thread.CurrentPrincipal Is ElitaPlusPrincipal) Then
                Return CType(System.Threading.Thread.CurrentPrincipal, ElitaPlusPrincipal)
            Else
                If (TypeOf HttpContext.Current.User Is ElitaPlusPrincipal) Then
                    System.Threading.Thread.CurrentPrincipal = HttpContext.Current.User
                    Authentication.SetCulture()
                Else
                    If (TypeOf HttpContext.Current.Session("PRINCIPAL_SESSION_KEY") Is ElitaPlusPrincipal) Then
                        System.Threading.Thread.CurrentPrincipal = DirectCast(HttpContext.Current.Session("PRINCIPAL_SESSION_KEY"), ElitaPlusPrincipal)
                        HttpContext.Current.User = System.Threading.Thread.CurrentPrincipal
                        Authentication.SetCulture()
                    End If
                End If

                Return CType(System.Threading.Thread.CurrentPrincipal, ElitaPlusPrincipal)
            End If
        End Get
    End Property



#End Region



End Class





