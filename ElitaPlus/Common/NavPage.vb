Public Class NavPage
    Inherits ServerViewStatePage
    'Inherits System.Web.UI.Page

#Region "Contants"
    Protected Const NAV_OBJ_SESSION_KEY As String = "NAV_OBJ_SESSION_KEY"
#End Region

#Region "Events"

    Public Event PageReturn(ReturnFromUrl As String, ReturnPar As Object)
    Public Event PageCall(CallFromUrl As String, CallingPar As Object)
    Public Event PageRedirect(RedirectFromUrl As String, CallingPar As Object)
    Public Event PagePostBack()
    Public Event WebRedirect()

#End Region

    Protected mobjNavigator As WebNavigator = Nothing
    Protected mobjPage As NavPage
    Protected mobjState As Object
    Protected useExistingState As Boolean = False


    Public Sub New(Optional ByVal useExistingState As Boolean = False)
        mobjPage = Me
        mobjState = Nothing
        Me.useExistingState = useExistingState
    End Sub

    Public Sub New(State As Object)
        mobjPage = Me
        mobjState = State
    End Sub

    Public Sub RestoreState(State As Object)
        mobjPage = Me
        mobjState = State
    End Sub

    Public Sub New(State As Object, p As NavPage)
        mobjPage = p
        mobjState = State
    End Sub

    Private Sub Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Me.Navigator.NavAction = WebNavigator.NAVIGATION_ACTION.NAV_FLOW Then
            Navigator.NavReset() 'To reset NavAction to Nothing after it is consumed
            Return
        End If
        If Not IsPostBack Then
            If useExistingState Then
                mobjState = Navigator.PageState
            Else
                If Me.Navigator.NavAction = WebNavigator.NAVIGATION_ACTION.NAV_RETURN Then
                    mobjState = Navigator.PageState
                    If Navigator.PageReturnPointMethodName IsNot Nothing Then
                        CallReturnHandler(Navigator.PageReturnPointMethodName, NavOriginURL, ReturnedValues)
                    Else
                        RaiseEvent PageReturn(NavOriginURL, ReturnedValues)
                    End If
                Else
                    Navigator.SetCurrentPage(mobjPage, mobjState)
                End If
            End If
        Else
            mobjState = Navigator.PageState
        End If
        If Not useExistingState Then
            If IsPostBack Then
                RaiseEvent PagePostBack()
            Else
                Select Case Navigator.NavAction
                    Case WebNavigator.NAVIGATION_ACTION.NAV_CALL
                        Navigator.NavReset() 'To reset NavAction to Nothing after it is consumed
                        RaiseEvent PageCall(NavOriginURL, CallingParameters)
                    Case WebNavigator.NAVIGATION_ACTION.NAV_REDIRECT
                        Navigator.NavReset() 'To reset NavAction to Nothing after it is consumed
                        RaiseEvent PageRedirect(NavOriginURL, CallingParameters)
                    Case WebNavigator.NAVIGATION_ACTION.NAV_NOTHING
                        Navigator.Reset() 'To reset the navigation stack when an invalid navigation transition may have ocurred
                        RaiseEvent WebRedirect()
                    Case WebNavigator.NAVIGATION_ACTION.NAV_RETURN
                        Navigator.NavReset() 'To reset NavAction to Nothing after it is consumed
                End Select
            End If
        End If
    End Sub

    Protected ReadOnly Property State() As Object
        Get
            If (mobjState IsNot Nothing) AndAlso ([GetType].BaseType.FullName <> mobjState.GetType.ReflectedType.FullName) Then
                Response.Redirect(ELPWebConstants.APPLICATION_PATH & "/Common/LogOutForm.aspx")
            End If

            Return mobjState
        End Get
    End Property


    Protected ReadOnly Property Navigator() As WebNavigator
        Get
            Try
                If mobjNavigator Is Nothing Then
                    mobjNavigator = CType(Session(NAV_OBJ_SESSION_KEY), WebNavigator)
                    If mobjNavigator Is Nothing Then
                        mobjNavigator = New WebNavigator
                        Session(NAV_OBJ_SESSION_KEY) = mobjNavigator
                    End If
                End If
                Return mobjNavigator
            Catch ex As Exception
                'When we are in design mode there is no session object
                Return Nothing
            End Try
        End Get
    End Property

#Region "Client"

    Protected Shared ReadOnly Property ClientNavigator() As WebNavigator
        Get
            Try
                Dim oNavigator As WebNavigator
                oNavigator = CType(HttpContext.Current.Session(NAV_OBJ_SESSION_KEY), WebNavigator)

                Return oNavigator
            Catch ex As Exception
                'When we are in design mode there is no session object
                Return Nothing
            End Try
        End Get
    End Property

#End Region

    'ReturnPointMethodName is the name of a method that wil be called on return from the page
    'The method must have the following signature: Public Sub <ReturnPointMethodName>(ReturnFromUrl as String, ReturnValues as Object)
    Public Sub callPage(url As String, Optional ByVal parameters As Object = Nothing, Optional ByVal ReturnPointMethodName As String = Nothing)
        Navigator.callPage(mobjPage, url, State, parameters, ReturnPointMethodName)
    End Sub

    Public Sub ReturnToCallingPage(Optional ByVal returnValues As Object = Nothing, Optional ByVal returnStep As Integer = 1)
        Navigator.ReturnToCallingPage(mobjPage, returnValues, returnStep)
    End Sub

    Public Sub ReturnToMaxCallingPage(Optional ByVal returnValues As Object = Nothing)
        Navigator.ReturnToMaxCallingPage(mobjPage, returnValues)
    End Sub

    Public Sub SetPageOutOfNavigation()
        Navigator.RemovePageFromNavigation()
    End Sub

    Public Sub Redirect(url As String, Optional ByVal parameters As Object = Nothing)
        Navigator.Redirect(mobjPage, url, parameters)
    End Sub

    Public Sub Flow(url As String)
        Navigator.Flow(mobjPage, url)
    End Sub

    Public ReadOnly Property CallingParameters() As Object
        Get
            If Navigator IsNot Nothing Then
                Return Navigator.CallingParameters
            Else
                Return Nothing
            End If
        End Get
    End Property

    Public ReadOnly Property CalledUrl() As String
        Get
            Return Navigator.CalledUrl
        End Get
    End Property

    Public ReadOnly Property ReturnedValues() As Object
        Get
            If Navigator IsNot Nothing Then
                Return Navigator.ReturnedValues
            Else
                Return Nothing
            End If
        End Get
    End Property


    'Public ReadOnly Property LastNavAction() As WebNavigator.NAVIGATION_ACTION
    '    Get
    '        If Not Navigator Is Nothing Then
    '            Return Me.Navigator.NavAction
    '        Else
    '            Return WebNavigator.NAVIGATION_ACTION.NAV_NOTHING
    '        End If
    '    End Get
    'End Property

    Public ReadOnly Property RetStep() As Integer
        Get
            If Navigator IsNot Nothing Then
                Return Navigator.RetStep
            Else
                Return Nothing
            End If
        End Get
    End Property

    Public ReadOnly Property NavOriginURL() As String
        Get
            If Navigator IsNot Nothing Then
                Return Navigator.NavOriginURL
            Else
                Return Nothing
            End If
        End Get
    End Property

    Public ReadOnly Property PageState() As Object
        Get
            If Navigator IsNot Nothing Then
                Return Navigator.PageState
            Else
                Return Nothing
            End If
        End Get
    End Property

    Public ReadOnly Property StateSession() As Hashtable
        Get
            If Navigator IsNot Nothing Then
                Return Navigator.StateSession
            Else
                Return Nothing
            End If
        End Get
    End Property


    Private Sub CallReturnHandler(MethodName As String, FromURL As String, ReturnValuesObj As Object)
        [GetType].GetMethod(MethodName).Invoke(Me, New Object() {FromURL, ReturnValuesObj})
    End Sub
End Class
