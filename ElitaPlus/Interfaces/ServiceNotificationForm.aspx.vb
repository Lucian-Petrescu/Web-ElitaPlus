Imports Assurant.ElitaPlus.ElitaPlusWebApp.Reports
Imports Assurant.ElitaPlus.DALObjects

Namespace Interfaces

    Partial Class ServiceNotificationForm
        Inherits ElitaPlusSearchPage

#Region "Page State"

        Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn

            TheServiceNotificationController.SetErrorController(ErrorCtrl)
            TheServiceNotificationController.Page_PageReturn(ReturnFromUrl, ReturnPar)
            mbIsPageReturn = True
        End Sub

#End Region


#Region "Variables"

        Private mbIsPageReturn As Boolean = False

#End Region


#Region "Properties"

        Public ReadOnly Property TheServiceNotificationController() As SvrNotificationFileProcessedController
            Get
                If moSvrNotificationController Is Nothing Then
                    moSvrNotificationController = CType(FindControl("moSvrNotificationController"), SvrNotificationFileProcessedController)
                End If
                Return moSvrNotificationController
            End Get
        End Property

#End Region

#Region "Handlers"

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Protected WithEvents ErrorCtrl As ErrorController
        Protected WithEvents moSvrNotificationController As SvrNotificationFileProcessedController


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
            Me.ErrorCtrl.Clear_Hide()
            TheServiceNotificationController.SetErrorController(ErrorCtrl)
            Try
                If Not Me.IsPostBack Then
                    If mbIsPageReturn = False Then
                        TheServiceNotificationController.InitController(ServiceNotificationReconWrkForm.URL, PrintClaimLoadRejectForm.URL, _
                                                                   ServiceNotificationFileProcessedData.InterfaceTypeCode.CLOSE_NOTIFICATION)
                    End If

                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
            Me.ShowMissingTranslations(Me.ErrorCtrl)
            TheServiceNotificationController.InstallInterfaceProgressBar()
        End Sub

#End Region

#End Region

    End Class

End Namespace