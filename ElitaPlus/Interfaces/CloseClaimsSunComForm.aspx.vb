Imports Assurant.ElitaPlus.ElitaPlusWebApp.Reports
Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common

Namespace Interfaces

    Partial Class CloseClaimsSunComForm
        Inherits ElitaPlusSearchPage

#Region "Page State"

        Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn

            TheClaimController.SetErrorController(ErrorCtrl)
            TheClaimController.Page_PageReturn(ReturnFromUrl, ReturnPar)
            mbIsPageReturn = True
        End Sub

#End Region


#Region "Variables"

        Private mbIsPageReturn As Boolean = False

#End Region


#Region "Properties"

        Public ReadOnly Property TheClaimController() As ClaimFileProcessedController
            Get
                If moClaimController Is Nothing Then
                    moClaimController = CType(FindControl("moClaimController"), ClaimFileProcessedController)
                End If
                Return moClaimController
            End Get
        End Property

#End Region

#Region "Handlers"

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Protected WithEvents ErrorCtrl As ErrorController
        Protected WithEvents moClaimController As ClaimFileProcessedController
        Protected WithEvents moDealerMultipleDrop As MultipleColumnDDLabelControl


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
            ErrorCtrl.Clear_Hide()
            TheClaimController.SetErrorController(ErrorCtrl)

            If moDealerMultipleDrop Is Nothing Then
                moDealerMultipleDrop = CType(TheClaimController.FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
            End If
            Try
                If Not IsPostBack Then
                    If mbIsPageReturn = False Then
                        TheClaimController.InitController(CloseClaimSunComReconWrkForm.URL, PrintClaimLoadRejectForm.URL, _
                                                                    ClaimFileProcessedData.InterfaceTypeCode.CLOSE_CLAIM_SUNCOM)
                    End If

                End If
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
            If mbIsPageReturn = False Then
                ShowMissingTranslations(ErrorCtrl)
            End If
            TheClaimController.InstallInterfaceProgressBar()
        End Sub

#End Region

#Region "Events-Handlers"

        Private Sub OnFromDrop_Changed(fromMultipleDrop As MultipleColumnDDLabelControl) _
                Handles moDealerMultipleDrop.SelectedDropChanged
            Try
                moClaimController.PopulateClaimInterfaceForADealer()
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

#End Region

#End Region

    End Class

End Namespace