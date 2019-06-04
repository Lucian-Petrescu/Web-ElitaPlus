Imports Assurant.ElitaPlus.ElitaPlusWebApp.Reports
Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Namespace Interfaces

    Partial Class DealerFileForm
        Inherits ElitaPlusSearchPage

#Region "Page State"

        Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
           
            TheDealerController.SetErrorController(UserControlMessageController)
            TheDealerController.Page_PageReturn(ReturnFromUrl, ReturnPar)
            mbIsPageReturn = True
        End Sub

#End Region


        '#Region "Constants"

        '            Public Shared URL As String = ELPWebConstants.APPLICATION_PATH & "/Interfaces/DealerFileForm.aspx"
        '#End Region

#Region "Variables"

        Private mbIsPageReturn As Boolean = False
#End Region

#Region "Properties"

        Public ReadOnly Property TheDealerController() As DealerFileProcessedController_New
            Get
                If moDealerController Is Nothing Then
                    moDealerController = CType(FindControl("moDealerController"), DealerFileProcessedController_New)
                End If
                Return moDealerController
            End Get
        End Property

        Public ReadOnly Property UserControlMessageController() As MessageController
            Get
                If MessageController Is Nothing Then
                    MessageController = DirectCast(Me.MasterPage.MessageController, MessageController)
                End If
                Return MessageController
            End Get
        End Property

#End Region

#Region "Handlers"

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        'Protected WithEvents ErrorCtrl As ErrorController
        Protected WithEvents MessageController As MessageController
        Protected WithEvents moDealerController As DealerFileProcessedController_New

        Protected WithEvents moDealerMultipleDrop As MultipleColumnDDLabelControl_New
        Protected WithEvents moDealerGroupMultipleDrop As MultipleColumnDDLabelControl_New
        Protected WithEvents moParentFileRadio As RadioButton
        Protected WithEvents moDealerFileRadio As RadioButton
        Protected WithEvents moParentFileLabel As Label

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
            Me.UserControlMessageController.Clear_Hide()
            TheDealerController.SetErrorController(UserControlMessageController)

            GetDealerUserControlElements()

            Try
                If Not Me.IsPostBack Then
                    If mbIsPageReturn = False Then
                        If Not moDealerFileRadio Is Nothing Then
                            moDealerFileRadio.Checked = True
                        End If
                        If Not moParentFileRadio Is Nothing Then
                            moParentFileRadio.Checked = False
                        End If
                    End If
                    Me.MasterPage.MessageController.Clear()
                    Me.MasterPage.UsePageTabTitleInBreadCrum = False
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Interfaces")
                    UpdateBreadCrum()

                    If mbIsPageReturn = False Then
                        TheDealerController.InitController(DealerReconWrkForm.URL, PrintDealerLoadRejectForm.URL, _
                                                                    DealerFileProcessedData.InterfaceTypeCode.CERT)
                    End If
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            If mbIsPageReturn = False Then Me.ShowMissingTranslations(Me.MessageController)
            TheDealerController.InstallInterfaceProgressBar()
        End Sub
#End Region

#Region "Events-Handlers"

        Private Sub OnFromDrop_Changed(ByVal fromMultipleDrop As MultipleColumnDDLabelControl_New) _
                Handles moDealerMultipleDrop.SelectedDropChanged
            Try
                TheDealerController.ClearDealerGroupSelection()
                TheDealerController.PopulateDealerInterface()
                'Dim objDealer As New Dealer(moDealerMultipleDrop.SelectedGuid)
                'If Not objDealer Is Nothing AndAlso objDealer.DealerTypeDesc = "VSC" Then
                '    TheDealerController.InitController(DealerVSCReconWrkForm.URL, PrintDealerLoadRejectForm.URL, DealerFileProcessedData.InterfaceTypeCode.CERT)
                'End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
        Private Sub OnParentFileRadio_Checked(ByVal dlrfile As DealerFileProcessedController_New) Handles moDealerController.CheckedChanged
            Try
                TheDealerController.ClearDealerGroupSelection()
                TheDealerController.ClearDealerSelection()
                TheDealerController.PopulateDealerInterface()

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
     
        Private Sub OnDealerGrpDrop_Changed(ByVal fromMultipleDrop As MultipleColumnDDLabelControl_New) _
                Handles moDealerGroupMultipleDrop.SelectedDropChanged
            Try
                TheDealerController.ClearDealerSelection()
                TheDealerController.PopulateDealerInterface()
                'Dim objDealer As New Dealer(moDealerMultipleDrop.SelectedGuid)
                'If Not objDealer Is Nothing AndAlso objDealer.DealerTypeDesc = "VSC" Then
                '    TheDealerController.InitController(DealerVSCReconWrkForm.URL, PrintDealerLoadRejectForm.URL, DealerFileProcessedData.InterfaceTypeCode.CERT)
                'End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub


#End Region

#Region "ConrollingLogic"
        Private Sub UpdateBreadCrum()
            '  If (Not Me.State Is Nothing) Then
            'If (Not Me.State Is Nothing) Then
            Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("DEALER_FILE")
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("DEALER_FILE")
            'End If
            'End If
        End Sub
        Private Sub GetDealerUserControlElements()
            If moDealerMultipleDrop Is Nothing Then
                moDealerMultipleDrop = CType(TheDealerController.FindControl("multipleDropControl"), MultipleColumnDDLabelControl_New)
            End If

            If moDealerGroupMultipleDrop Is Nothing Then
                moDealerGroupMultipleDrop = CType(TheDealerController.FindControl("multipleDealerGrpDropControl"), MultipleColumnDDLabelControl_New)
            End If

            If moDealerFileRadio Is Nothing Then
                moDealerFileRadio = CType(TheDealerController.FindControl("rdDealerFile"), RadioButton)
            End If

            If moParentFileRadio Is Nothing Then
                moParentFileRadio = CType(TheDealerController.FindControl("rdParentFile"), RadioButton)
            End If

            If moParentFileLabel Is Nothing Then
                moParentFileLabel = CType(TheDealerController.FindControl("lblParentFile"), Label)
            End If
        End Sub
#End Region
#End Region


    End Class

End Namespace