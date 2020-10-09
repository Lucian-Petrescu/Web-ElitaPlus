Imports System.Threading
Imports System.Security.Principal
Imports System.Web
'Imports Assurant.ProjectAdministrationSystem.DALObjects.Security



<Serializable>
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


    Public Sub New(identity As ElitaPlusIdentity)
        Initialize()

        If (identity IsNot Nothing) Then
            _identity = identity
        End If

    End Sub


#End Region



#Region " Private Members "


    Private Sub Initialize()
        _identity = Nothing
        _roles = Nothing
        _unitCodesManaged = Nothing
    End Sub



#End Region



#Region " Public Properties "


    Public ReadOnly Property Identity As System.Security.Principal.IIdentity Implements System.Security.Principal.IPrincipal.Identity
        Get
            Return _identity
        End Get
    End Property
    Public Property WebServiceOffLineMessage As String
        Get
            Return _webServiceOffLineMessage
        End Get
        Set
            _webServiceOffLineMessage = value
        End Set
    End Property

    Public Property WebServiceFunctionOffLineMessage As String
        Get
            Return _webServiceFunctionOffLineMessage
        End Get
        Set
            _webServiceFunctionOffLineMessage = value
        End Set
    End Property
    Public Property IdToken As String
        Get
            Return _idToken
        End Get
        Set
            _idToken = value
        End Set
    End Property

#End Region



#Region " Public Methods "

    <Obsolete("Create/Use one of extension method from Assurant.Elita.Security.IdentityExtensions")>
    Public Function ActiveUser() As User
        Return CType(Identity, ElitaPlusIdentity).ActiveUser
    End Function

    Public Function IsInRole(role As String) As Boolean Implements System.Security.Principal.IPrincipal.IsInRole
        Dim identity1 As ElitaPlusIdentity = CType(Identity, ElitaPlusIdentity)
        Return identity1.isInRole(role)
    End Function

#End Region



#Region "Static Methods"



    Public Shared ReadOnly Property Current As ElitaPlusPrincipal
        Get
            Dim threadPrincipal As IPrincipal = Thread.CurrentPrincipal

            If (TypeOf threadPrincipal Is ElitaPlusPrincipal) Then
                Return CType(threadPrincipal, ElitaPlusPrincipal)
            Else
                If (TypeOf HttpContext.Current.User Is ElitaPlusPrincipal) Then
                    threadPrincipal = HttpContext.Current.User
                    Authentication.SetCulture()
                Else
                    If (TypeOf HttpContext.Current.Session("PRINCIPAL_SESSION_KEY") Is ElitaPlusPrincipal) Then
                        threadPrincipal = DirectCast(HttpContext.Current.Session("PRINCIPAL_SESSION_KEY"), ElitaPlusPrincipal)
                        HttpContext.Current.User = threadPrincipal
                        Authentication.SetCulture()
                    End If
                End If

                Return CType(threadPrincipal, ElitaPlusPrincipal)
            End If
        End Get
    End Property



#End Region



End Class





