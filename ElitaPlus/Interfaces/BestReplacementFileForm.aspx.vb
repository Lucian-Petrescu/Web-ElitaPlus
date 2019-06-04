Imports Assurant.ElitaPlus.ElitaPlusWebApp.Reports
Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Namespace Interfaces
    Public Class BestReplacementFileForm
        Inherits ElitaPlusSearchPage

#Region "Constant"
        Private Const BEST_REPLACEMENT As String = "BEST_REPLACEMENT_FILE"
#End Region

        Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn

            TheFileController.SetErrorController(ErrorCtrl)
            TheFileController.Page_PageReturn(ReturnFromUrl, ReturnPar)
            mbIsPageReturn = True
        End Sub

        Private Sub moFileController_PopulateReferenceDataView(ByVal sender As Object, ByVal e As FileProcessedController.PopulateReferenceEventArgs) _
            Handles moFileController.PopulateReferenceDataView
            e.ReferenceDV = LookupListNew.GetBestReplacementLookupList(e.CompanyGroupId)
        End Sub

        Private Sub moFileController_SetExpectedFileName(ByVal sender As Object, ByVal e As FileProcessedController.SetExpectedFileNameEventArgs) _
            Handles moFileController.SetExpectedFileName
            e.FileName = e.CompanyGroupCode & "-" & e.ReferenceCode & "-"
        End Sub

        Private Sub moFileController_OnValidate(ByVal sender As Object, ByVal e As FileProcessedController.ExecuteActionEventArgs) _
            Handles moFileController.OnValidate
            e.InterfaceStatusId = BestReplacementRecon.ValidateFile(e.FileProcessedId)
        End Sub

        Private Sub moFileController_OnProcess(ByVal sender As Object, ByVal e As FileProcessedController.ExecuteActionEventArgs) _
            Handles moFileController.OnProcess
            e.InterfaceStatusId = BestReplacementRecon.ProcessFile(e.FileProcessedId)
        End Sub

        Private Sub moFileController_OnDelete(ByVal sender As Object, ByVal e As FileProcessedController.ExecuteActionEventArgs) _
            Handles moFileController.OnDelete
            e.InterfaceStatusId = BestReplacementRecon.DeleteFile(e.FileProcessedId)
        End Sub
#Region "Page State"
        Class MyState
            Public SearchedComanyGroupID As Guid
        End Class

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property
#End Region
#Region "Constants"
        Public Shared URL As String = ELPWebConstants.APPLICATION_PATH & "/Interfaces/BestReplacementFileForm.aspx"
#End Region

#Region "Variables"

        Private mbIsPageReturn As Boolean = False
#End Region

#Region "Properties"

        Public ReadOnly Property TheFileController() As FileProcessedController
            Get
                If moFileController Is Nothing Then
                    moFileController = CType(FindControl("moFileController"), FileProcessedController)
                End If
                Return moFileController
            End Get
        End Property
#End Region


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
            Me.ErrorCtrl.Clear_Hide()
            moFileController.SetErrorController(ErrorCtrl)
            Me.MenuEnabled = True
            'If moDealerMultiDrop Is Nothing Then
            '    moDealerMultiDrop = CType(TheFileController.FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
            'End If
            Try
                If Not Me.IsPostBack Then
                    If mbIsPageReturn = False Then
                        TheFileController.InitController(BestReplacementReconWrkForm.URL)
                    End If
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
            If mbIsPageReturn = False Then Me.ShowMissingTranslations(Me.ErrorCtrl)
            TheFileController.InstallInterfaceProgressBar()
        End Sub
#End Region

    End Class
End Namespace
