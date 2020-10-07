Imports Assurant.ElitaPlus.ElitaPlusWebApp.Reports
Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Namespace Interfaces

    Partial Class DealerPmtForm
        Inherits ElitaPlusSearchPage

#Region "Page State"

        Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn

            TheDealerController.SetErrorController(UserControlMessageController)
            TheDealerController.Page_PageReturn(ReturnFromUrl, ReturnPar)
            mbIsPageReturn = True
        End Sub

#End Region

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
                    MessageController = DirectCast(MasterPage.MessageController, MessageController)
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

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Handlers-Init"

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            GetDealerUserControlElements()

            UserControlMessageController.Clear_Hide()
            TheDealerController.SetErrorController(UserControlMessageController)

            moDealerGroupMultipleDrop.Visible = False
            moParentFileRadio.Visible = True
            moParentFileLabel.Visible = True
            moDealerFileRadio.Visible = True

            Try
                If Not IsPostBack Then
                    If mbIsPageReturn = False Then
                        If moDealerFileRadio IsNot Nothing Then
                            moDealerFileRadio.Checked = True
                        End If
                        If moParentFileRadio IsNot Nothing Then
                            moParentFileRadio.Checked = False
                        End If
                    End If

                    MasterPage.MessageController.Clear()
                    MasterPage.UsePageTabTitleInBreadCrum = False
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Interfaces")
                    UpdateBreadCrum()

                    If mbIsPageReturn = False Then
                        ' ? or PrintDealerPmt...
                        TheDealerController.InitController(DealerPmtReconWrkForm.URL, PrintDealerLoadRejectForm.URL, _
                                                DealerFileProcessedData.InterfaceTypeCode.PAYM)
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            If mbIsPageReturn = False Then ShowMissingTranslations(MasterPage.MessageController)
            TheDealerController.InstallInterfaceProgressBar()
        End Sub
#End Region

#Region "Events-Handlers"

        Private Sub OnFromDrop_Changed(fromMultipleDrop As MultipleColumnDDLabelControl_New) _
        Handles moDealerMultipleDrop.SelectedDropChanged
            Try
                TheDealerController.ClearDealerGroupSelection()
                TheDealerController.PopulateDealerInterface()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub OnDealerGrpDrop_Changed(fromMultipleDrop As MultipleColumnDDLabelControl_New) _
        Handles moDealerGroupMultipleDrop.SelectedDropChanged
            Try
                TheDealerController.ClearDealerSelection()
                TheDealerController.PopulateDealerInterface()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub OnParentFileRadio_Checked(dlrfile As DealerFileProcessedController_New) Handles moDealerController.CheckedChanged
            Try
                TheDealerController.ClearDealerGroupSelection()
                TheDealerController.ClearDealerSelection()
                TheDealerController.PopulateDealerInterface()

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region
#Region "ConrollingLogic"
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

        Private Sub UpdateBreadCrum()
            MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("DEALER_PAYMENT")
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("DEALER_PAYMENT")
        End Sub

#End Region
#End Region

    End Class
End Namespace