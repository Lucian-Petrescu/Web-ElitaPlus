Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Reports

Namespace Interfaces

    Partial Class VendorInventoryFileForm
        Inherits ElitaPlusSearchPage
#Region "Constants"
        Public Shared Url As String = ELPWebConstants.APPLICATION_PATH & "/Interfaces/VendorInventoryFileForm.aspx"
#End Region

#Region "Variables"
        Private _mbIsPageReturn As Boolean = False
        Protected WithEvents MessageController As MessageController
#End Region

#Region "Properties"
        Public ReadOnly Property TheFileController() As FileProcessedControllerNew
            Get
                If moFileController Is Nothing Then
                    moFileController = CType(FindControl("moFileController"), FileProcessedControllerNew)
                End If
                Return moFileController
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

#Region "Page_Events"
        Private Sub Page_PageReturn(ByVal returnFromUrl As String, ByVal returnPar As Object) Handles MyBase.PageReturn

            TheFileController.SetErrorController(UserControlMessageController)
            TheFileController.Page_PageReturn(returnFromUrl, returnPar)
            _mbIsPageReturn = True
        End Sub
        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            UserControlMessageController.Clear()
            moFileController.SetErrorController(UserControlMessageController)
            MenuEnabled = True
            Try
                If Not IsPostBack Then
                    MasterPage.MessageController.Clear()
                    UpdateBreadCrum()
                    If _mbIsPageReturn = False Then
                        TheFileController.InitController(VendorInventoryReconWrkForm.Url, ExportFileProcessedNewForm.URL,
                                                FileProcessedData.FileTypeCode.VendorInv)
                    End If
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            If _mbIsPageReturn = False Then ShowMissingTranslations(MessageController)
            TheFileController.InstallInterfaceProgressBar()
        End Sub
#End Region

#Region "Events-Handlers"
        Private Sub moFileController_PopulateReferenceDataView(ByVal sender As Object, ByVal e As FileProcessedControllerNew.PopulateReferenceEventArgs) _
            Handles moFileController.PopulateReferenceDataView
            e.ReferenceDv = LookupListNew.GetServiceCenterLookupList(e.CountryId)
        End Sub

        Private Sub moFileController_SetExpectedFileName(ByVal sender As Object, ByVal e As FileProcessedControllerNew.SetExpectedFileNameEventArgs) _
            Handles moFileController.SetExpectedFileName
            e.FileName = e.CompanyGroupCode & "-" & e.ReferenceCode & "-"
        End Sub

        Private Sub moFileController_OnValidate(ByVal sender As Object, ByVal e As FileProcessedControllerNew.ExecuteActionEventArgs) _
            Handles moFileController.OnValidate
            e.InterfaceStatusId = VendorloadInvReconWrk.ValidateFile(e.FileProcessedId)
        End Sub

        Private Sub moFileController_OnProcess(ByVal sender As Object, ByVal e As FileProcessedControllerNew.ExecuteActionEventArgs) _
            Handles moFileController.OnProcess
            e.InterfaceStatusId = VendorloadInvReconWrk.ProcessFile(e.FileProcessedId)
        End Sub

        Private Sub moFileController_OnDelete(ByVal sender As Object, ByVal e As FileProcessedControllerNew.ExecuteActionEventArgs) _
            Handles moFileController.OnDelete
            e.InterfaceStatusId = VendorloadInvReconWrk.DeleteFile(e.FileProcessedId)
        End Sub
#End Region

#Region "ControllingLogic"
        Private Sub UpdateBreadCrum()
            Dim pageTab As String
            Dim pageTitle As String
            Dim breadCrum As String

            pageTab = TranslationBase.TranslateLabelOrMessage("INTERFACES")
            pageTitle = TranslationBase.TranslateLabelOrMessage("INVENTORY_FILE")
            breadCrum = pageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("EQUIPMENT") _
                                    & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("EQUIPMENT_INVENTORY_FILE")

            MasterPage.UsePageTabTitleInBreadCrum = False
            MasterPage.PageTab = pageTab
            MasterPage.PageTitle = pageTitle
            MasterPage.BreadCrum = breadCrum
        End Sub
#End Region
    End Class

End Namespace