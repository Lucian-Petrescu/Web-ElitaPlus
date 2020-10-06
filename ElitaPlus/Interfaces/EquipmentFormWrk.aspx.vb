
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Reports
Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Namespace Interfaces
    Public Class EquipmentFormWrk
        Inherits ElitaPlusSearchPage
#Region "Page State"
        Class MyState

            Public SearchedComanyGroupID As Guid

        End Class
        Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn

            TheFileController.SetErrorController(ErrorCtrl)
            TheFileController.Page_PageReturn(ReturnFromUrl, ReturnPar)
            mbIsPageReturn = True
        End Sub

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property
#End Region

#Region "File Controller Handlers"
        Private Sub moFileController_BeforeGetDataView(sender As Object, e As FileProcessedController.FileProcessedDataEventArgs) _
            Handles moFileController.BeforeGetDataView
            e.FileProcessedData.ReferenceId = e.FileProcessedData.CompanyGroupId
        End Sub

        Private Sub moFileController_SetExpectedFileName(sender As Object, e As FileProcessedController.SetExpectedFileNameEventArgs) _
            Handles moFileController.SetExpectedFileName
            e.FileName = e.CompanyGroupCode & "-"
        End Sub

        Private Sub moFileController_OnValidate(sender As Object, e As FileProcessedController.ExecuteActionEventArgs) _
            Handles moFileController.OnValidate
            e.InterfaceStatusId = EquipmentReconWrk.ValidateFile(e.FileProcessedId)
        End Sub

        Private Sub moFileController_OnProcess(sender As Object, e As FileProcessedController.ExecuteActionEventArgs) _
            Handles moFileController.OnProcess
            e.InterfaceStatusId = EquipmentReconWrk.ProcessFile(e.FileProcessedId)
        End Sub

        Private Sub moFileController_OnDelete(sender As Object, e As FileProcessedController.ExecuteActionEventArgs) _
            Handles moFileController.OnDelete
            e.InterfaceStatusId = EquipmentReconWrk.DeleteFile(e.FileProcessedId)
        End Sub
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

#Region "Handlers"

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents moDealerMultipleDrop As MultipleColumnDDLabelControl
        Protected WithEvents moCompanyGroupMultiDrop As MultipleColumnDDLabelControl
        Protected WithEvents moCompanyMultiDrop As MultipleColumnDDLabelControl
        Protected WithEvents moReferenceMultipleDrop As MultipleColumnDDLabelControl

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
            ErrorCtrl.Clear_Hide()
            moFileController.SetErrorController(ErrorCtrl)
            MenuEnabled = True
            If moDealerMultipleDrop Is Nothing Then
                moDealerMultipleDrop = CType(TheFileController.FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
            End If
            If mocompanygroupMultiDrop Is Nothing Then
                mocompanygroupMultiDrop = CType(TheFileController.FindControl("moCompanyGroup"), MultipleColumnDDLabelControl)
            End If
            If mocompanyMultiDrop Is Nothing Then
                mocompanyMultiDrop = CType(TheFileController.FindControl("moCompany"), MultipleColumnDDLabelControl)
            End If
            'If moDealerMultiDrop Is Nothing Then
            '    moDealerMultiDrop = CType(TheFileController.FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
            'End If
            Try
                If Not IsPostBack Then
                    If mbIsPageReturn = False Then
                        TheFileController.InitController(EquipmentReconWrkForm.URL)
                    End If
                End If

            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
            If mbIsPageReturn = False Then ShowMissingTranslations(ErrorCtrl)
            TheFileController.InstallInterfaceProgressBar()
        End Sub
#End Region

#Region "Events-Handlers"

        Private Sub OnFromDrop_Changed(fromMultipleDrop As MultipleColumnDDLabelControl) _
                Handles moDealerMultipleDrop.SelectedDropChanged
            Try
                'TheFileController.PopulateRefernce()
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub
        Private Sub OnFromDrops_Changed(fromsMultipleDrop As MultipleColumnDDLabelControl) _
               Handles moCompanyGroupMultiDrop.SelectedDropChanged
            Try
                State.SearchedComanyGroupID = moCompanyGroupMultiDrop.SelectedGuid
                Dim alCompanies As New Guid
                alCompanies = State.SearchedComanyGroupID
                'TheFileController.PopulateUserControlAvailableCompanies(alCompanies)
                Dim objCompany As CompanyGroup = New CompanyGroup(State.SearchedComanyGroupID)
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Private Sub OnFromDropss_Changed(fromsMultipleDrop As MultipleColumnDDLabelControl) _
              Handles moCompanyMultiDrop.SelectedDropChanged
            Try
                'TheFileController.PopulateDealer()
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub
#End Region
#End Region
    End Class
End Namespace