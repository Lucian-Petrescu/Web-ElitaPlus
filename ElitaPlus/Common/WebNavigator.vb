Public Class WebNavigator

    Public Enum NAVIGATION_ACTION
        NAV_RETURN
        NAV_CALL
        NAV_REDIRECT
        NAV_FLOW
        NAV_NOTHING
    End Enum

#Region "Stack"

    'Dim stackLevel As Integer
    '    For stackLevel = 1 To returnStep
    '        Me.mobjCurPage = CType(Me.mobjNavStack.Pop(), CallerPage)
    '    Next
    '    Me.mintRetStep = returnStep
    '    Me.mobjCallingParameters = Nothing
    '    Me.mobjReturnedValues = returnValues
    '    msNavOriginURL = navPage.Request.Url.AbsolutePath
    '    Me.mePreviousInvokedAction = meLastInvokedAction
    '    Me.meLastInvokedAction = NAVIGATION_ACTION.NAV_RETURN
    '    navPage.Response.Redirect(Me.mobjCurPage.url)

    Private Class BackupStack
        Private mobjCurPage As CallerPage
        Protected mintRetStep As Integer
        Protected mobjCallingParameters As Object
        Protected mobjReturnedValues As Object
        Protected msNavOriginURL As String
        Protected mePreviousInvokedAction As NAVIGATION_ACTION
        Protected meLastInvokedAction As NAVIGATION_ACTION
    End Class

#End Region

    Protected MaxStackLevel As Integer = 10

    Protected mobjNavStack As System.Collections.Stack = New System.Collections.Stack(10)  'stack of CallerPage

    Protected mobjReturnedValues As Object

    Protected mobjCallingParameters As Object

    Protected meLastInvokedAction As NAVIGATION_ACTION = NAVIGATION_ACTION.NAV_NOTHING

    Protected mintRetStep As Integer

    Protected msNavOriginURL As String

    Protected mobjCurPage As CallerPage

    Protected mePreviousInvokedAction As NAVIGATION_ACTION = NAVIGATION_ACTION.NAV_NOTHING


    Public Sub New(Optional ByVal CallingStackMaxLevel As Integer = 10)
        Me.MaxStackLevel = CallingStackMaxLevel
    End Sub

    Public ReadOnly Property NavStackCount() As Integer
        Get
            Return mobjNavStack.Count
        End Get
    End Property

    'ReturnPointMethodName is the name of a method that wil be called on return from the page
    'The method must have the following signature: Public Sub <ReturnPointMethodName>(ReturnFromUrl as String, ReturnValues as Object)
    Public Sub callPage(ByVal navPage As System.Web.UI.Page, ByVal url As String, Optional ByVal currentState As Object = Nothing, Optional ByVal parameters As Object = Nothing, Optional ByVal ReturnPointMethodName As String = Nothing)
        'Dim caller As New CallerPage()
        Dim caller As CallerPage = Me.mobjCurPage
        Dim prevCaller As CallerPage

        If caller Is Nothing Then
            caller = New CallerPage
            caller.url = Me.GetCallingUrl(navPage)
            caller.state = currentState
        End If
        caller.calledUrl = url
        caller.ReturnPointMethodName = ReturnPointMethodName
        Me.mobjCallingParameters = parameters
        msNavOriginURL = navPage.Request.Url.AbsolutePath
        Me.mePreviousInvokedAction = meLastInvokedAction
        meLastInvokedAction = NAVIGATION_ACTION.NAV_CALL
        If Me.mobjNavStack.Count < Me.MaxStackLevel Then
            If Me.mobjNavStack.Count > 0 Then
                prevCaller = CType(Me.mobjNavStack.Peek(), CallerPage)
                If prevCaller.url = caller.url Then
                    ' The Last Caller can not be the same as the current caller
                    Me.mobjNavStack.Pop()
                    Return
                End If
            End If
            Me.mobjNavStack.Push(caller)
        End If
        '  System.Threading.Thread.Sleep(500)
        navPage.Response.Redirect(url)
        '  System.Threading.Thread.Sleep(500)
    End Sub

    Public Sub ReturnToCallingPage(ByVal navPage As System.Web.UI.Page, Optional ByVal returnValues As Object = Nothing, Optional ByVal returnStep As Integer = 1)
        Dim stackLevel As Integer
        For stackLevel = 1 To returnStep
            Me.mobjCurPage = CType(Me.mobjNavStack.Pop(), CallerPage)
        Next
        Me.mintRetStep = returnStep
        Me.mobjCallingParameters = Nothing
        Me.mobjReturnedValues = returnValues
        msNavOriginURL = navPage.Request.Url.AbsolutePath
        Me.mePreviousInvokedAction = meLastInvokedAction
        Me.meLastInvokedAction = NAVIGATION_ACTION.NAV_RETURN
        navPage.Response.Redirect(Me.mobjCurPage.url)
    End Sub

    Public Sub ReturnToMaxCallingPage(ByVal navPage As System.Web.UI.Page, Optional ByVal returnValues As Object = Nothing)
        Dim stackLevel As Integer
        For stackLevel = 1 To Me.mobjNavStack.Count
                Me.mobjCurPage = CType(Me.mobjNavStack.Pop(), CallerPage)
        Next
       
        Me.mintRetStep = Me.mobjNavStack.Count
        Me.mobjCallingParameters = Nothing
        Me.mobjReturnedValues = returnValues
        msNavOriginURL = navPage.Request.Url.AbsolutePath
        Me.mePreviousInvokedAction = meLastInvokedAction
        Me.meLastInvokedAction = NAVIGATION_ACTION.NAV_RETURN
        navPage.Response.Redirect(Me.mobjCurPage.url)
    End Sub

    Public Sub RemovePageFromNavigation()
        mobjNavStack.Pop()
    End Sub

    Public Sub Redirect(ByVal navPage As System.Web.UI.Page, ByVal url As String, Optional ByVal parameters As Object = Nothing)
        Me.mobjCurPage = Nothing
        Me.mobjNavStack.Clear()
        Me.mobjReturnedValues = Nothing
        Me.mobjCallingParameters = parameters
        msNavOriginURL = navPage.Request.Url.AbsolutePath
        Me.mePreviousInvokedAction = meLastInvokedAction
        Me.meLastInvokedAction = NAVIGATION_ACTION.NAV_REDIRECT
        navPage.Response.Redirect(url)
    End Sub

    Public Sub Flow(ByVal navPage As System.Web.UI.Page, ByVal url As String)
        msNavOriginURL = navPage.Request.Url.AbsolutePath
        Me.mePreviousInvokedAction = meLastInvokedAction
        Me.meLastInvokedAction = NAVIGATION_ACTION.NAV_FLOW
        navPage.Response.Redirect(url)
    End Sub

    Public Sub Reset()
        Me.mobjNavStack.Clear()
        Me.mobjReturnedValues = Nothing
        Me.mobjCallingParameters = Nothing
        msNavOriginURL = Nothing
        Me.mePreviousInvokedAction = NAVIGATION_ACTION.NAV_NOTHING
        Me.meLastInvokedAction = NAVIGATION_ACTION.NAV_NOTHING
    End Sub

    Public Sub NavReset()
        Me.mePreviousInvokedAction = meLastInvokedAction
        Me.meLastInvokedAction = NAVIGATION_ACTION.NAV_NOTHING
    End Sub

    Public ReadOnly Property CallingParameters() As Object
        Get
            Return Me.mobjCallingParameters
        End Get
    End Property

    Public ReadOnly Property CalledUrl() As String
        Get
            Return Me.mobjCurPage.calledUrl
        End Get
    End Property

    Public ReadOnly Property ReturnedValues() As Object
        Get
            Return Me.mobjReturnedValues
        End Get
    End Property


    Public ReadOnly Property NavAction() As NAVIGATION_ACTION
        Get
            Return Me.meLastInvokedAction
        End Get
    End Property

    Public ReadOnly Property PrevNavAction() As NAVIGATION_ACTION
        Get
            Return Me.mePreviousInvokedAction
        End Get
    End Property

    Public ReadOnly Property RetStep() As Integer
        Get
            Return Me.RetStep
        End Get
    End Property

    Public ReadOnly Property NavOriginURL() As String
        Get
            Return Me.msNavOriginURL
        End Get
    End Property

    Public ReadOnly Property PageState() As Object
        Get
            If Not Me.mobjCurPage Is Nothing Then
                Return Me.mobjCurPage.state
            Else
                Return Nothing
            End If
        End Get
    End Property

    Public ReadOnly Property PageReturnPointMethodName() As String
        Get
            If Not Me.mobjCurPage Is Nothing Then
                Return Me.mobjCurPage.ReturnPointMethodName
            Else
                Return Nothing
            End If
        End Get
    End Property

    Public Sub SetCurrentPage(ByVal NavPage As System.Web.UI.Page, ByVal CurState As Object)
        Me.mobjCurPage = New CallerPage
        Me.mobjCurPage.url = GetCallingUrl(NavPage)
        Me.mobjCurPage.state = CurState
    End Sub

    Function GetCallingUrl(ByVal NavPage As System.Web.UI.Page) As String
        Dim callingUrl As String
        callingUrl = NavPage.Request.Url.AbsolutePath
        If Not NavPage.Request.QueryString Is Nothing AndAlso NavPage.Request.QueryString.ToString <> "" Then
            callingUrl &= "?" & NavPage.Request.QueryString.ToString
        End If
        Return callingUrl
    End Function


    Protected Class CallerPage
        Public url As String
        Public calledUrl As String
        Public state As Object
        Public ReturnPointMethodName As String
        Public StateExtension As New Hashtable
    End Class

    Public ReadOnly Property StateSession() As Hashtable
        Get
            Return Me.mobjCurPage.StateExtension
        End Get
    End Property


End Class
