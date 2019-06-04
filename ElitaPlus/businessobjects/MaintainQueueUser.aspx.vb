Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace business
    Partial Class MaintainQueueUser
        Inherits ElitaPlusPage

#Region "Constants"
        Public Const URL As String = "MaintainQueueUser.aspx"

        Private Const AUTHORIZATION_FORMAT_INCORRECT As String = "AUTHORIZATION_FORMAT_INCORRECT"
        Private Const USER_NOT_FOUND_IN_SESSION As String = "USER_NOT_FOUND_IN_SESSION"
        Private Const USER_DELETE As String = "USER_DELETE_MESSAGE"
        Public Const NO_DATA As String = " - "
#End Region

#Region "Page Return Type"
        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public EditingBo As Assurant.ElitaPlus.BusinessObjectsNew.User
            Public HasDataChanged As Boolean
            Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As Assurant.ElitaPlus.BusinessObjectsNew.User, ByVal hasDataChanged As Boolean)
                Me.LastOperation = LastOp
                Me.EditingBo = curEditingBo
                Me.HasDataChanged = hasDataChanged
            End Sub
        End Class

#End Region

#Region "Page State"


        Class MyState
            Public MyBO As User
            Public MyCompanyId As Guid = Guid.Empty
            Public HasDataChanged As Boolean
        End Class

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property

        Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
            Try
                If Not Me.CallingParameters Is Nothing Then
                    'Get the id from the parent
                    Me.State.MyBO = New Assurant.ElitaPlus.BusinessObjectsNew.User(CType(Me.CallingParameters, Guid))
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

#End Region

#Region "Handlers"

#Region "Handlers-Variables"

        Protected WithEvents lblChangeMyCompany As System.Web.UI.WebControls.Label
        Protected WithEvents moUserIdLabel As System.Web.UI.WebControls.Label
        Protected WithEvents lblTitle As System.Web.UI.WebControls.Label
        Protected WithEvents moIsNewUserLabel As System.Web.UI.WebControls.Label
        Protected WithEvents dlstCompanyGroup As System.Web.UI.WebControls.DropDownList
#End Region

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Handlers-Init"


        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Me.MasterPage.MessageController.Clear()
            Try
                If Not Me.IsPostBack Then
                    Me.PopulateFormFromBOs()
                    UpdateBreadCrum()
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)
        End Sub
#End Region

#Region "Handlers-Buttons"

        Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
            Try
                Me.ReturnToCallingPage(New business.MaintainUser.ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
            Me.State.HasDataChanged = False
            Try
                Me.State.MyBO.SaveWorkQueueUser(UC_AvaSel_Rule.SelectedList)
                If Me.State.MyBO.IsChildrenDirty Then
                    Me.State.MyBO.UpdateWorkQueueUserAssign()
                    Me.State.HasDataChanged = True
                    Me.ReturnToCallingPage(New business.MaintainUser.ReturnType(ElitaPlusPage.DetailPageCommand.Save, Me.State.MyBO, Me.State.HasDataChanged))
                Else
                    Me.DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

#End Region

#Region "Handlers-DropDowns"

        Private Sub ddlCompanyList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCompanyList.SelectedIndexChanged
            Try
                If Not New Guid(ddlCompanyList.SelectedValue) = Guid.Empty Then
                    LoadUserControl()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#End Region


#Region "Load And Clear"

        Private Sub LoadIHQRoles()
            Dim IHQRolesDV As DataView = LookupListNew.GetIHQRolesLookupList()
            Session.Add("IHQRoles", IHQRolesDV)
        End Sub

#End Region


#Region "Business"
        Private Sub PopulateFormFromBOs()
            LoadCompanyDropDownLists()
            LoadUserSummaryInfo()
        End Sub

        ' Must chek befor continue
        Private Sub LoadCompanyDropDownLists()
            'Dim selectedCompanies, selectedCountries As ArrayList
            ' ElitaPlusIdentity.Current.ActiveUser.Companies()
            ' Me.BindListControlToDataView(Me.ddlCompanyList, Me.State.MyBO.GetSelectedAssignedCompanies(Me.State.MyBO.Id), "Description", "Company_Id", True)

            Dim CompanyList As DataElements.ListItem() =
                           CommonConfigManager.Current.ListManager.GetList(listCode:=ListCodes.Company)

            Dim UserCompanies As DataElements.ListItem() = (From Company In CompanyList
                                                            Where ElitaPlusIdentity.Current.ActiveUser.Companies.Contains(Company.ListItemId)
                                                            Select Company).ToArray()

            Me.ddlCompanyList.Populate(UserCompanies.ToArray(),
                                    New PopulateOptions() With
                                    {
                                        .AddBlankItem = True
                                    })

        End Sub

        Private Sub LoadUserSummaryInfo()
            With Me.State.MyBO
                lblNewrokId.Text = .NetworkId
                lblUsername.Text = .UserName
                lblUserStatus.Text = LookupListNew.GetDescriptionFromCode(LookupListNew.LK_YESNO, .Active.Trim)
                lblUserLanguage.Text = LookupListNew.GetDescriptionFromId(LookupListNew.LK_LANGUAGES, .LanguageId)
            End With
        End Sub

        Private Sub LoadUserControl()
            Me.State.MyBO.SelectedCompanyId = New Guid(ddlCompanyList.SelectedValue)
            Dim AvailableList As User.WorkQueueAssignSelectionView = Me.State.MyBO.GetAvailableWorkQueueAssign(Me.State.MyBO.SelectedCompanyId)
            UC_AvaSel_Rule.SetAvailableData(AvailableList, "DESCRIPTION", "WORKQUEUE_ID")

            Dim SelectedList As User.WorkQueueAssignSelectionView = Me.State.MyBO.GetWQAssignSelectionView(Me.State.MyBO.SelectedCompanyId)
            UC_AvaSel_Rule.SetSelectedData(SelectedList, "DESCRIPTION", "WORKQUEUE_ID")
            UC_AvaSel_Rule.RemoveSelectedFromAvailable()
        End Sub

#End Region
#Region "Bread Crum"
        Private Sub UpdateBreadCrum()
            Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("Admin")
            Me.MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage("TABLES") & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("WORK_QUEUE")
            Me.MasterPage.BreadCrum = Me.MasterPage.BreadCrum & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("QUEUE_USER")
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("QUEUE_USER")
            Me.MasterPage.UsePageTabTitleInBreadCrum = False
        End Sub
#End Region

    End Class
End Namespace
