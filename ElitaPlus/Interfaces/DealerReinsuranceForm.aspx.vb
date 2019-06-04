
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Reports
Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Namespace Interfaces

    Public Class DealerReinsuranceForm
        Inherits ElitaPlusSearchPage

#Region "Page State"

        Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn

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

        Protected WithEvents moDealerFileRadio As RadioButton

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
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            'Put user code to initialize the page here
            GetDealerUserControlElements()

            Me.UserControlMessageController.Clear_Hide()
            TheDealerController.SetErrorController(UserControlMessageController)

            moDealerGroupMultipleDrop.Visible = False

            moDealerFileRadio.Visible = True

            Try
                If Not Me.IsPostBack Then
                    If mbIsPageReturn = False Then
                        If Not moDealerFileRadio Is Nothing Then
                            moDealerFileRadio.Checked = True
                        End If

                    End If

                    Me.MasterPage.MessageController.Clear()
                    Me.MasterPage.UsePageTabTitleInBreadCrum = False
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Interfaces")
                    UpdateBreadCrum()

                    If mbIsPageReturn = False Then

                        TheDealerController.InitController(DealerReinsuranceReconWrkForm.URL, PrintDealerLoadRejectForm.URL,
                                                DealerFileProcessedData.InterfaceTypeCode.RINS)
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            If mbIsPageReturn = False Then Me.ShowMissingTranslations(Me.MasterPage.MessageController)
            TheDealerController.InstallInterfaceProgressBar()
        End Sub

#End Region

#Region "Events-Handlers"

        Private Sub OnFromDrop_Changed(ByVal fromMultipleDrop As MultipleColumnDDLabelControl_New) _
        Handles moDealerMultipleDrop.SelectedDropChanged
            Try
                TheDealerController.ClearDealerGroupSelection()
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
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
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

        End Sub

        Private Sub UpdateBreadCrum()
            Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("DEALER_REINSURANCE")
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("DEALER_REINSURANCE")
        End Sub

#End Region

#End Region
    End Class
End Namespace
