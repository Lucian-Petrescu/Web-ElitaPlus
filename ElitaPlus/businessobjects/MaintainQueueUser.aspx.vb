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
            Public Sub New(LastOp As DetailPageCommand, curEditingBo As Assurant.ElitaPlus.BusinessObjectsNew.User, hasDataChanged As Boolean)
                LastOperation = LastOp
                EditingBo = curEditingBo
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

        Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
            Try
                If CallingParameters IsNot Nothing Then
                    'Get the id from the parent
                    State.MyBO = New Assurant.ElitaPlus.BusinessObjectsNew.User(CType(CallingParameters, Guid))
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
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

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Handlers-Init"


        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            MasterPage.MessageController.Clear()
            Try
                If Not IsPostBack Then
                    PopulateFormFromBOs()
                    UpdateBreadCrum()
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub
#End Region

#Region "Handlers-Buttons"

        Private Sub btnCancel_Click(sender As System.Object, e As System.EventArgs) Handles btnCancel.Click
            Try
                ReturnToCallingPage(New business.MaintainUser.ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnSave_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnSave_WRITE.Click
            State.HasDataChanged = False
            Try
                State.MyBO.SaveWorkQueueUser(UC_AvaSel_Rule.SelectedList)
                If State.MyBO.IsChildrenDirty Then
                    State.MyBO.UpdateWorkQueueUserAssign()
                    State.HasDataChanged = True
                    ReturnToCallingPage(New business.MaintainUser.ReturnType(ElitaPlusPage.DetailPageCommand.Save, State.MyBO, State.HasDataChanged))
                Else
                    DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", MSG_BTN_OK, MSG_TYPE_INFO)
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

#End Region

#Region "Handlers-DropDowns"

        Private Sub ddlCompanyList_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlCompanyList.SelectedIndexChanged
            Try
                If Not New Guid(ddlCompanyList.SelectedValue) = Guid.Empty Then
                    LoadUserControl()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
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

            ddlCompanyList.Populate(UserCompanies.ToArray(),
                                    New PopulateOptions() With
                                    {
                                        .AddBlankItem = True
                                    })

        End Sub

        Private Sub LoadUserSummaryInfo()
            With State.MyBO
                lblNewrokId.Text = .NetworkId
                lblUsername.Text = .UserName
                lblUserStatus.Text = LookupListNew.GetDescriptionFromCode(LookupListNew.LK_YESNO, .Active.Trim)
                lblUserLanguage.Text = LookupListNew.GetDescriptionFromId(LookupListNew.LK_LANGUAGES, .LanguageId)
            End With
        End Sub

        Private Sub LoadUserControl()
            State.MyBO.SelectedCompanyId = New Guid(ddlCompanyList.SelectedValue)
            Dim AvailableList As User.WorkQueueAssignSelectionView = State.MyBO.GetAvailableWorkQueueAssign(State.MyBO.SelectedCompanyId)
            UC_AvaSel_Rule.SetAvailableData(AvailableList, "DESCRIPTION", "WORKQUEUE_ID")

            Dim SelectedList As User.WorkQueueAssignSelectionView = State.MyBO.GetWQAssignSelectionView(State.MyBO.SelectedCompanyId)
            UC_AvaSel_Rule.SetSelectedData(SelectedList, "DESCRIPTION", "WORKQUEUE_ID")
            UC_AvaSel_Rule.RemoveSelectedFromAvailable()
        End Sub

#End Region
#Region "Bread Crum"
        Private Sub UpdateBreadCrum()
            MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("Admin")
            MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage("TABLES") & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("WORK_QUEUE")
            MasterPage.BreadCrum = MasterPage.BreadCrum & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("QUEUE_USER")
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("QUEUE_USER")
            MasterPage.UsePageTabTitleInBreadCrum = False
        End Sub
#End Region

    End Class
End Namespace
