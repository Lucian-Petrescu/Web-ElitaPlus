Namespace Interfaces

Partial Class InterfaceBaseForm
        Inherits ElitaPlusPage

        

#Region "Constants"

        Public Shared URL As String = ELPWebConstants.APPLICATION_PATH & "/Interfaces/InterfaceBaseForm.aspx"
        Public Const SESSION_PARAMETERS_KEY As String = "INTERFACE_BASE_SESSION_PARAMETERS_KEY"
        Public Const SESSION_LOCALSTATE_KEY As String = "INTERFACE_BASE_SESSION_LOCALSTATE_KEY"

#End Region

#Region "Page Parameters"

        Public Class Params
            Public intStatusId As Guid
            Public baseController As String

            Public Sub New()
                ' Default Values
                intStatusId = Guid.Empty
                baseController = String.Empty
            End Sub

        End Class

#End Region

#Region "Page State"

        Public Sub New()
            'MyBase.New(New MyState)
            MyBase.New(True)
        End Sub

        Public Class MyState
            Public moParams As Params
            Public errorStatus As InterfaceStatusWrk.IntError


            Public Sub New()
                ' Default Values
            End Sub

        End Class


        Protected Sub SetStateProperties()
            Me.moState = Nothing
            TheState.moParams = CType(Session(SESSION_PARAMETERS_KEY), Params)

        End Sub

#End Region

#Region "Variables"

        Private moState As MyState

#End Region

#Region "Properties"

        Protected ReadOnly Property TheState() As MyState
            Get
                Try
                    If Me.moState Is Nothing Then
                        Me.moState = CType(Session(SESSION_LOCALSTATE_KEY), MyState)
                        If Me.moState Is Nothing Then
                            Me.moState = New MyState
                            Session(SESSION_LOCALSTATE_KEY) = Me.moState
                        End If
                    End If
                    Return Me.moState
                Catch ex As Exception
                    'When we are in design mode there is no session object
                    Return Nothing
                End Try
            End Get
        End Property

#End Region

#Region "Handlers"


#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Handlers-Init"


        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            If Not Page.IsPostBack Then
                SetStateProperties()
                ContinueWaiting()
            End If

        End Sub

#End Region

#Region "Handlers-Button"

        Private Sub btnHtmlHidden_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHtmlHidden.Click

        End Sub

        Private Sub btnContinue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnContinue.Click
            ContinueWaiting()
        End Sub

#End Region

#End Region

#Region "Progress Bar"

        Private Sub ContinueWaiting()
            Dim intStatus As InterfaceStatusWrk

            Try
                '  SetStateProperties()
                intStatus = New InterfaceStatusWrk(TheState.moParams.intStatusId)
                TheState.errorStatus = intStatus.WaitTilDone()

            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                TheState.errorStatus.status = InterfaceStatusWrk.IntStatus.INTERFACE_UNKNOWN_PROBLEM
            Finally
                If TheState.errorStatus.status = InterfaceStatusWrk.IntStatus.PENDING Then
                    ContinueWaitingForInterface()
                Else
                    CloseProgressBar( _
                        InterfaceStatusWrk.IntStatus.GetName(GetType(InterfaceStatusWrk.IntStatus), _
                                                            TheState.errorStatus.status), _
                        TheState.errorStatus.msg)
                    Session(SESSION_LOCALSTATE_KEY) = Nothing
                    Session(SESSION_PARAMETERS_KEY) = Nothing
                End If
            End Try


        End Sub

#End Region

#Region "JavaScripts"

        Public Sub CloseProgressBar(ByVal statusMsg As String, ByVal errorMsg As String)

            If errorMsg Is Nothing Then
                errorMsg = String.Empty
            End If

            If ((TheState Is Nothing) Or (TheState.moParams Is Nothing)) Then
                SetStateProperties()
            End If

            Dim sJavaScript As String
            Dim transMsg As String = String.Empty

            Try
                transMsg = TranslationBase.TranslateLabelOrMessage(statusMsg)
            Catch ex As Exception

            End Try

            Dim statusControlId As String = TheState.moParams.baseController & "moInterfaceProgressControl_moInterfaceStatus"
            Dim errmsgControlId As String = TheState.moParams.baseController & "moInterfaceProgressControl_moInterfaceErrorMsg"
            Dim status As String = transMsg
            Dim errMsg As String = errorMsg
            sJavaScript = "<SCRIPT>" & Environment.NewLine
            sJavaScript &= "updateStatus('" & statusControlId & "','" & errmsgControlId & "','" & status & "','" & errMsg & "');" & Environment.NewLine
            sJavaScript &= "</SCRIPT>" & Environment.NewLine

            'sJavaScript = "<SCRIPT>" & Environment.NewLine
            'sJavaScript &= "parent.parent.document.all('" & TheState.moParams.baseController & "moInterfaceProgressControl_moInterfaceStatus').value = '" & transMsg & "';" & Environment.NewLine
            'sJavaScript &= "parent.parent.document.all('" & TheState.moParams.baseController & "moInterfaceProgressControl_moInterfaceErrorMsg').value = '" & errorMsg & "';" & Environment.NewLine
            'sJavaScript &= "</SCRIPT>" & Environment.NewLine

            Me.RegisterStartupScript("CloseProgressBar", sJavaScript)
        End Sub


        Public Sub ContinueWaitingForInterface()
            Dim sJavaScript As String

            sJavaScript = "<SCRIPT>" & Environment.NewLine
            sJavaScript &= "buttonContinueClick();" & Environment.NewLine
            sJavaScript &= "</SCRIPT>" & Environment.NewLine
            Me.RegisterStartupScript("ContinueWaiting", sJavaScript)
        End Sub

#End Region

    End Class



End Namespace
