Public Class NavPage
    Inherits ServerViewStatePage
    'Inherits System.Web.UI.Page

#Region "Contants"
    Protected Const NAV_OBJ_SESSION_KEY As String = "NAV_OBJ_SESSION_KEY"
#End Region

#Region "Events"

    Public Event PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object)
    Public Event PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object)
    Public Event PageRedirect(ByVal RedirectFromUrl As String, ByVal CallingPar As Object)
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

    Public Sub New(ByVal State As Object)
        mobjPage = Me
        mobjState = State
    End Sub

    Public Sub RestoreState(ByVal State As Object)
        mobjPage = Me
        mobjState = State
    End Sub

    Public Sub New(ByVal State As Object, ByVal p As NavPage)
        mobjPage = p
        mobjState = State
    End Sub

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Me.Navigator.NavAction = WebNavigator.NAVIGATION_ACTION.NAV_FLOW Then
            Me.Navigator.NavReset() 'To reset NavAction to Nothing after it is consumed
            Return
        End If
        If Not Me.IsPostBack Then
            If Me.useExistingState Then
                mobjState = Me.Navigator.PageState
            Else
                If Me.Navigator.NavAction = WebNavigator.NAVIGATION_ACTION.NAV_RETURN Then
                    mobjState = Me.Navigator.PageState
                    If Not Me.Navigator.PageReturnPointMethodName Is Nothing Then
                        CallReturnHandler(Me.Navigator.PageReturnPointMethodName, Me.NavOriginURL, Me.ReturnedValues)
                    Else
                        RaiseEvent PageReturn(Me.NavOriginURL, Me.ReturnedValues)
                    End If
                Else
                    Me.Navigator.SetCurrentPage(mobjPage, mobjState)
                End If
            End If
        Else
            mobjState = Me.Navigator.PageState
        End If
        If Not Me.useExistingState Then
            If Me.IsPostBack Then
                RaiseEvent PagePostBack()
            Else
                Select Case Me.Navigator.NavAction
                    Case WebNavigator.NAVIGATION_ACTION.NAV_CALL
                        Me.Navigator.NavReset() 'To reset NavAction to Nothing after it is consumed
                        RaiseEvent PageCall(Me.NavOriginURL, Me.CallingParameters)
                    Case WebNavigator.NAVIGATION_ACTION.NAV_REDIRECT
                        Me.Navigator.NavReset() 'To reset NavAction to Nothing after it is consumed
                        RaiseEvent PageRedirect(Me.NavOriginURL, Me.CallingParameters)
                    Case WebNavigator.NAVIGATION_ACTION.NAV_NOTHING
                        Me.Navigator.Reset() 'To reset the navigation stack when an invalid navigation transition may have ocurred
                        RaiseEvent WebRedirect()
                    Case WebNavigator.NAVIGATION_ACTION.NAV_RETURN
                        Me.Navigator.NavReset() 'To reset NavAction to Nothing after it is consumed
                End Select
            End If
        End If
    End Sub

    Protected ReadOnly Property State() As Object
        Get
            If (Not mobjState Is Nothing) AndAlso (Me.GetType.BaseType.FullName <> mobjState.GetType.ReflectedType.FullName) Then
                Response.Redirect(ELPWebConstants.APPLICATION_PATH & "/Common/LogOutForm.aspx")
            End If

            Return mobjState
        End Get
    End Property


    Protected ReadOnly Property Navigator() As WebNavigator
        Get
            Try
                If Me.mobjNavigator Is Nothing Then
                    Me.mobjNavigator = CType(Session(NAV_OBJ_SESSION_KEY), WebNavigator)
                    If Me.mobjNavigator Is Nothing Then
                        Me.mobjNavigator = New WebNavigator
                        Session(NAV_OBJ_SESSION_KEY) = Me.mobjNavigator
                    End If
                End If
                Return Me.mobjNavigator
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
    Public Sub callPage(ByVal url As String, Optional ByVal parameters As Object = Nothing, Optional ByVal ReturnPointMethodName As String = Nothing)
        Navigator.callPage(Me.mobjPage, url, Me.State, parameters, ReturnPointMethodName)
    End Sub

    Public Sub ReturnToCallingPage(Optional ByVal returnValues As Object = Nothing, Optional ByVal returnStep As Integer = 1)
        Navigator.ReturnToCallingPage(Me.mobjPage, returnValues, returnStep)
    End Sub

    Public Sub ReturnToMaxCallingPage(Optional ByVal returnValues As Object = Nothing)
        Navigator.ReturnToMaxCallingPage(Me.mobjPage, returnValues)
    End Sub

    Public Sub SetPageOutOfNavigation()
        Navigator.RemovePageFromNavigation()
    End Sub

    Public Sub Redirect(ByVal url As String, Optional ByVal parameters As Object = Nothing)
        Navigator.Redirect(Me.mobjPage, url, parameters)
    End Sub

    Public Sub Flow(ByVal url As String)
        Navigator.Flow(Me.mobjPage, url)
    End Sub

    Public ReadOnly Property CallingParameters() As Object
        Get
            If Not Navigator Is Nothing Then
                Return Navigator.CallingParameters
            Else
                Return Nothing
            End If
        End Get
    End Property

    Public ReadOnly Property CalledUrl() As String
        Get
            Return Me.Navigator.CalledUrl
        End Get
    End Property

    Public ReadOnly Property ReturnedValues() As Object
        Get
            If Not Navigator Is Nothing Then
                Return Me.Navigator.ReturnedValues
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
            If Not Navigator Is Nothing Then
                Return Me.Navigator.RetStep
            Else
                Return Nothing
            End If
        End Get
    End Property

    Public ReadOnly Property NavOriginURL() As String
        Get
            If Not Navigator Is Nothing Then
                Return Me.Navigator.NavOriginURL
            Else
                Return Nothing
            End If
        End Get
    End Property

    Public ReadOnly Property PageState() As Object
        Get
            If Not Navigator Is Nothing Then
                Return Me.Navigator.PageState
            Else
                Return Nothing
            End If
        End Get
    End Property

    Public ReadOnly Property StateSession() As Hashtable
        Get
            If Not Navigator Is Nothing Then
                Return Me.Navigator.StateSession
            Else
                Return Nothing
            End If
        End Get
    End Property


    Private Sub CallReturnHandler(ByVal MethodName As String, ByVal FromURL As String, ByVal ReturnValuesObj As Object)
        Me.GetType.GetMethod(MethodName).Invoke(Me, New Object() {FromURL, ReturnValuesObj})
    End Sub
End Class
