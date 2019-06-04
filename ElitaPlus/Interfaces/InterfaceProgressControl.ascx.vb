Namespace Interfaces

Partial Class InterfaceProgressControl
    Inherits System.Web.UI.UserControl

#Region "Constants"

        Public Const SESSION_LOCALSTATE_KEY As String = "INTERFACE_PROGRESSCONTROL_SESSION_LOCALSTATE_KEY"

#End Region

#Region "State"

        Public Class MyState
            Public baseController As String

            Public Sub New()
                ' Default Values
                baseController = String.Empty
            End Sub

        End Class

#End Region

#Region "Variables"

        Private moState As MyState

#End Region

#Region "Properties"

        Private Shadows ReadOnly Property ThePage() As ElitaPlusPage
            Get
                Return CType(MyBase.Page, ElitaPlusPage)
            End Get
        End Property

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
        End Sub

#End Region

#Region "Handlers-Button"

        Private Sub btnViewHidden_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewHidden.Click
            ThePage.SetProgressTimeOutScript(TheState.baseController)
        End Sub

        Private Sub btnErrorHidden_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnErrorHidden.Click
            Dim oContainer As Control
            Dim oErrorController As IErrorController

            oContainer = Me.BindingContainer
            If ThePage.MasterPage Is Nothing Then
                oErrorController = ThePage.ErrControllerMaster
                If oErrorController Is Nothing Then
                    If oContainer.NamingContainer Is Nothing Then
                        oErrorController = CType(Me.BindingContainer.FindControl("ErrorCtrl"), ErrorController)
                    Else
                        oErrorController = CType(Me.BindingContainer.BindingContainer.FindControl("ErrorCtrl"), ErrorController)
                    End If
                End If
            Else
                oErrorController = ThePage.MasterPage.MessageController
            End If

            oErrorController.AddError(moInterfaceStatus.Value, False)
            oErrorController.AddError(moInterfaceErrorMsg.Value, False)
            oErrorController.Show()
            AppConfig.Log(New Exception(moInterfaceStatus.Value & " " & moInterfaceErrorMsg.Value))
            moInterfaceStatus.Value = String.Empty
        End Sub

#End Region

#End Region

#Region "Public Methods"

        ' First Routine to be called
        Public Sub EnableInterfaceProgress(Optional ByVal baseController As String = "")
            '   Dim oPage As ElitaPlusPage = CType(Me.BindingContainer.Page(), ElitaPlusPage)
            ' ThePage.SetProgressTimeOutScript(baseController)
            TheState.baseController = baseController
            InstallInterfaceProgress()
        End Sub

#End Region

#Region "JavaScripts"

        Public Sub InstallInterfaceProgress()
            Dim sJavaScript As String

            sJavaScript = "<SCRIPT>" & Environment.NewLine
            sJavaScript &= "EnableInterfaceProgress('" & TheState.baseController & "');" & Environment.NewLine
            sJavaScript &= "</SCRIPT>" & Environment.NewLine
            ThePage.RegisterStartupScript("EnableInterfaceProgress", sJavaScript)
        End Sub

        'Public Sub CloseProgressBar(ByVal statusMsg As String, ByVal errorMsg As String)
        '    Dim sJavaScript As String
        '    Dim transMsg As String = ThePage.TranslateLabelOrMessage(statusMsg)

        '    sJavaScript = "<SCRIPT>" & Environment.NewLine
        '    sJavaScript &= "document.all('" & LocalState.baseController & "moInterfaceProgressControl_moInterfaceStatus').value = '" & transMsg & "';" & Environment.NewLine
        '    sJavaScript &= "document.all('" & LocalState.baseController & "moInterfaceProgressControl_moInterfaceErrorMsg').value = '" & errorMsg & "';" & Environment.NewLine
        '    sJavaScript &= "</SCRIPT>" & Environment.NewLine
        '    ThePage.RegisterStartupScript("CloseProgressBar", sJavaScript)
        'End Sub

        'Public Sub ContinueWaiting()
        '    Dim sJavaScript As String

        '    sJavaScript = "<SCRIPT>" & Environment.NewLine
        '    sJavaScript &= "buttonContinueClick();" & Environment.NewLine
        '    sJavaScript &= "</SCRIPT>" & Environment.NewLine
        '    ThePage.RegisterStartupScript("ContinueWaiting", sJavaScript)
        'End Sub
#End Region


        
    End Class

End Namespace