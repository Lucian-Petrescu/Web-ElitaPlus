Imports System.IO
Imports System.Net
Imports System.Security.Cryptography.X509Certificates
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms

Namespace business
    Partial Class MaintainUser
        Inherits ElitaPlusPage

#Region "Constants"

        Private Const MAINTAIN_USER_FORM001 As String = "MAINTAIN_USER_FORM001" ' Maintain User Field Exception
        Private Const MAINTAIN_USER_FORM002 As String = "MAINTAIN_USER_FORM002" ' Maintain User Update Exception
        Private Const MAINTAIN_USER_FORM003 As String = "MAINTAIN_USER_FORM003" ' Maintain User-Roles List Exception
        Private Const MAINTAIN_USER_FORM004 As String = "MAINTAIN_USER_FORM004" ' Maintain User-Roles Update Exception
        Public Const URL As String = "MaintainUser.aspx"

        Private Const MAX_AUTHORIZATION_LIMIT As Double = 9999999999
        Private Const MIN_AUTHORIZATION_LIMIT As Double = 0

        Private Const AUTHORIZATION_FORMAT_INCORRECT As String = "AUTHORIZATION_FORMAT_INCORRECT"
        Private Const USER_NOT_FOUND_IN_SESSION As String = "USER_NOT_FOUND_IN_SESSION"
        Private Const USER_DELETE As String = "USER_DELETE_MESSAGE"

        'constant name of the session variable used to hold the user object in session.
        Private Const CURRENT_USER_OBJECT As String = "CurrentUserObject"

        Private Const DATA_HAS_CHANGED As String = "DATA_HAS_CHANGED"
        Private Const DUPLICATE_NETWORK_ID As String = "ORA-00001"

        Private Const NETWORK_ID_ALREADY_EXISTS As String = "NETWORK_ID_ALREADY_EXISTS"
        Private Const CONST_ZERO As String = "0"
        Private Const MAINTAIN_USER_LIST As String = "MaintainUserList.aspx"
        Private Const COL_DESCRIPTION_NAME As String = "description"
        Private Const COL_ID_NAME As String = "id"

        'AA648971
        Private Const IHQ_SECURITY As String = "IHQSE"
        Private Const IHQ_SUPPORT As String = "IHQSU"
        Private Const IHQ_ACTUARIAL As String = "IHQAC"
        Private Const IHQ_VIEW As String = "IHQVI"

        Private Const DELETE_ACTION As String = "DeleteAction"
        Private Const EDIT_ACTION As String = "EditAction"
        Private Const SAVE_ACTION As String = "SaveAction"
        Private Const CANCEL_ACTION As String = "CancelAction"

        Private Const EDIT_COMPANY_BUTTON As String = "btnView"
        Private Const DELETE_COMPANY_BUTTON As String = "btnDelete"
        Private Const AUTHORIZATION_LIMIT_TEXT As String = "moAuthorizationLimitText"
        Private Const PAYMENT_LIMIT_TEXT As String = "moPaymentLimitText"
        Private Const AUTHORIZATION_LIMIT_LABEL As String = "moAuthorizationLimitLabel"
        Private Const PAYMENT_LIMIT_LABEL As String = "moPaymentLimitLabel"
        Private Const LIABILITY_OVERRIDE_LIMIT_LABEL As String = "moLiabilityOverrideLimitLabel"
        Private Const LIABILITY_OVERRIDE_LIMIT_TEXT As String = "moLiabilityOverrideLimitText"

        Private Const GRID_COL_EDIT_BUTTON_IDX As Integer = 0
        Private Const GRID_COL_DELETE_BUTTON_IDX As Integer = 1
        Private Const GRID_COL_USER_ID_IDX As Integer = 2
        Private Const GRID_COL_COMPANY_ID_IDX As Integer = 3
        Private Const GRID_COL_COMPANY_NAME_IDX As Integer = 4
        Private Const GRID_COL_AUTHORIZATION_LIMIT As Integer = 5
        Private Const GRID_COL_PAYMENT_LIMIT As Integer = 6
        Private Const GRID_COL_LIABILITY_OVERRIDE_LIMIT As Integer = 7


        Private Const EDIT_SP_CLAIM_BUTTON As String = "EditButton"
        Private Const USER_SECURITY_GRID_COL_EDIT_BUTTON_IDX As Integer = 0
        Private Const USER_SECURITY_GRID_COL_SP_USER_CLAIMS_ID_IDX As Integer = 1
        Private Const USER_SECURITY_GRID_COL_SP_CLAIM_TYPE_ID_IDX As Integer = 2
        Private Const USER_SECURITY_GRID_COL_SP_CLAIM_CODE_DESCRIPTION_IDX As Integer = 3
        Private Const USER_SECURITY_GRID_COL_SP_CLAIM_VALUE_IDX As Integer = 4
        Private Const USER_SECURITY_GRID_COL_SP_CLAIM_VALID_FROM_IDX As Integer = 5
        Private Const USER_SECURITY_GRID_COL_SP_CLAIM_VALID_TO_IDX As Integer = 6

        Private Const SP_USER_CLAIMS_ID_LABEL As String = "SpUserClaimsIdLabel"
        Private Const SP_CLAIM_TYPE_ID_LABEL As String = "SpClaimTypeIdLabel"
        Private Const SP_CLAIM_CODE_DESCRIPTION_LABEL As String = "SpClaimCodeDescriptionLabel"
        Private Const SP_CLAIM_VALUE_LABEL As String = "SpClaimValueLabel"
        Private Const SP_CLAIM_EFFECTIVEDATE_LABEL As String = "SpClaimEffectiveDateLabel"
        Private Const SP_CLAIM_EXPIRATIONDATE_LABEL As String = "SpClaimExpirationDateLabel"
        Private Const SP_CLAIM_EXPIRATIONDATE_TEXT As String = "SpClaimExpirationDateText"
        Private Const SP_CLAIM_EXPIRATIONDATE_IMAGEBUTTON_NAME As String = "SpClaimExpirationDateImageButton"

        Private Const SP_CLAIM_CODE As String = "SP_CLAIM_CODE"
        Private Const SP_CLAIM_X509_CERTIFICATE_CODE As String = "X509_CERTIFICATE"
        Private Const SP_CLAIM_CLIENT_IP_ADDRESS As String = "CLIENT_IP_ADDRESS"
        Private Const SP_CLAIM_SERVICE_CENTER As String = "SERVICE_CENTER"
        Private Const SP_CLAIM_DEALER As String = "DEALER"
        Private Const SP_CLAIM_DEALER_GROUP As String = "DEALER_GROUP"
        Private Const SP_CLAIM_COMPANY_GROUP As String = "COMPANY_GROUP"

#End Region

#Region "PROPERTIES"

        Public Property AccountStatus() As Boolean
            Get
                Return rdoActive.Checked
            End Get
            Set(Value As Boolean)
                If Value = True Then
                    rdoActive.Checked = True
                Else
                    rdoInActive.Checked = True
                End If
            End Set
        End Property

        Public Property IsExternal() As Boolean
            Get
                Return rdoExternal.Checked
            End Get
            Set(Value As Boolean)
                rdoInternal.Checked = Not Value
                rdoExternal.Checked = Value
            End Set
        End Property

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
            Public MyRoleChildBO As UserRole
            Public MyCompanyChildBO As UserCompanyAssigned
            Public MyUserCompanyChildBO As UserCompany
            Public MyCompanyId As Guid = Guid.Empty
            Public MyCompanyGroupId As Guid = Guid.Empty
            Public MyCurrentSelectedList As New ListBox
            Public HasDataChanged As Boolean
            Public UserCompanyAssigned As UserCompanyAssigned.UserCompanyAssignedDV = Nothing
            Public IsEditing As Boolean = False
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public LastErrMsg As String
            Public DataChanged As Boolean = False
            Public UserSecurityDataChanged As Boolean = False
            Public EnabledUserSecurityEditButton As Boolean = True
            Public SpUserClaim As SpUserClaims
            Public SpUserClaimDv As SpUserClaims.SpUserClaimsDV = Nothing
            Public ViewOnly As Boolean = True
            Public usercomAssignedLkl As ListItem()
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


        Protected WithEvents moErrorController As ErrorController

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
            'Put user code to initialize the page here
            MasterPage.MessageController.Clear_Hide()
            Try
                If Not IsPostBack Then
                    MasterPage.MessageController.Clear()
                    MasterPage.UsePageTabTitleInBreadCrum = False
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")
                    UpdateBreadCrum()

                    If (Thread.CurrentPrincipal.CanManageUsers())
                        State.ViewOnly = False
                    else
                        MasterPage.MessageController.AddWarning(ElitaPlus.Common.ErrorCodes.GuiErrorMissingLdapPermission, True)
                    End If

                    AddControlMsg(btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, True)
                    If State.MyBO Is Nothing Then
                        State.MyBO = New User
                    End If
                    'Me.LoadUserGroup()
                    TranslateGridHeader(GridViewUserSecurity)
                    PopulateSpClaimCodeDropdowns()
                    PopulateFormFromBOs()
                    EnableDisableFields()
                    AddCalendar(ValidFromImageButton, ValidFromText)
                    AddCalendar(ValidToImageButton, ValidToText)
                End If
                BindBoPropertiesToLabels()
                CheckIfComingFromSaveConfirm()
                If Not IsPostBack Then
                    AddLabelDecorations(State.MyBO)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                HiddenSaveChangesPromptResponse.Value = ""
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)

        End Sub
#End Region

#Region "Handlers-Buttons"

        Private Sub btnSaveCompany_Click(sender As Object, e As EventArgs) Handles BtnSaveCompany.Click
            Try
                If (Not State.IsEditing) Then Return
                Dim oCompanyId As String
                Dim dr As DataRow
                Dim index As Integer = Grid.EditIndex
                State.DataChanged = True
                oCompanyId = Grid.Rows(index).Cells(GRID_COL_COMPANY_ID_IDX).Text
                For Each dr In State.UserCompanyAssigned.Table.Rows
                    If (GetGuidStringFromByteArray(CType(dr(UserCompanyAssigned.COL_COMPANY_ID), Byte())) = oCompanyId) Then

                        Dim result As Decimal

                        If (Not (Decimal.TryParse(CType(Grid.Rows(index).Cells(GRID_COL_PAYMENT_LIMIT).FindControl(PAYMENT_LIMIT_TEXT), TextBox).Text, result))) Then
                            Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_PAYMENT_AMOUNT_ERR, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_PAYMENT_AMOUNT_ERR)
                        End If

                        If (Not (Decimal.TryParse(CType(Grid.Rows(index).Cells(GRID_COL_AUTHORIZATION_LIMIT).FindControl(AUTHORIZATION_LIMIT_TEXT), TextBox).Text, result))) Then
                            Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_AUTHORIZED_AMOUNT_ERR, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_AUTHORIZED_AMOUNT_ERR)
                        End If

                        If (Not (Decimal.TryParse(CType(Grid.Rows(index).Cells(GRID_COL_LIABILITY_OVERRIDE_LIMIT).FindControl(LIABILITY_OVERRIDE_LIMIT_TEXT), TextBox).Text, result))) Then
                            Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_LIABILITY_OVERRIDE_AMOUNT_ERR, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_LIABILITY_OVERRIDE_AMOUNT_ERR)
                        End If

                        dr(UserCompanyAssigned.COL_PAYMENT_LIMIT) =
                            CType(Grid.Rows(index).Cells(GRID_COL_PAYMENT_LIMIT).FindControl(PAYMENT_LIMIT_TEXT), TextBox).Text
                        dr(UserCompanyAssigned.COL_AUTHORIZATION_LIMIT) =
                            CType(Grid.Rows(index).Cells(GRID_COL_AUTHORIZATION_LIMIT).FindControl(AUTHORIZATION_LIMIT_TEXT), TextBox).Text
                        dr(UserCompanyAssigned.COL_LIABILITY_OVERRIDE_LIMIT) =
                            CType(Grid.Rows(index).Cells(GRID_COL_LIABILITY_OVERRIDE_LIMIT).FindControl(LIABILITY_OVERRIDE_LIMIT_TEXT), TextBox).Text
                        Exit For
                    End If
                Next

                Grid.EditIndex = -1
                State.IsEditing = False
                SetButtons(True)
                RefreshAssignedCompany()
                DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub UpdateSpClaimButton_Click(sender As Object, e As EventArgs) Handles UpdateSpClaimButton.Click
            Try
                If (Not State.IsEditing) Then Return

                Dim SelectedSpUserClaimID As String
                Dim dr As DataRow
                Dim index As Integer = GridViewUserSecurity.EditIndex
                Dim ExpirationDateresult As Date
                Dim EffectiveDateresult As Date
                Dim ExpirationDate As String
                Dim IsValidSPClaim As Boolean = False
                Dim SPClaimTypeId As String

                SelectedSpUserClaimID = CType(GridViewUserSecurity.Rows(index).Cells(USER_SECURITY_GRID_COL_SP_USER_CLAIMS_ID_IDX).FindControl(SP_USER_CLAIMS_ID_LABEL), Label).Text

                For Each dr In State.SpUserClaimDv.Table.Rows
                    If (GetGuidStringFromByteArray(CType(dr(SpUserClaims.COL_NAME_SP_USER_CLAIMS_ID), Byte())) = SelectedSpUserClaimID) Then

                        ExpirationDate = CType(GridViewUserSecurity.Rows(index).Cells(USER_SECURITY_GRID_COL_SP_CLAIM_VALID_TO_IDX).FindControl(SP_CLAIM_EXPIRATIONDATE_TEXT), TextBox).Text
                        EffectiveDateresult = CType(CType(GridViewUserSecurity.Rows(index).Cells(USER_SECURITY_GRID_COL_SP_CLAIM_VALID_FROM_IDX).FindControl(SP_CLAIM_EFFECTIVEDATE_LABEL), Label).Text, Date)

                        If (Not (Date.TryParse(ExpirationDate, ExpirationDateresult))) Then
                            Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_DATE_ERR, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_DATE_ERR)
                        ElseIf (EffectiveDateresult > ExpirationDateresult) Then
                            Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.GUI_VALID_FROM_DATE_GREATER_THAN_ERR, Assurant.ElitaPlus.Common.ErrorCodes.GUI_VALID_FROM_DATE_GREATER_THAN_ERR)
                        End If

                        SPClaimTypeId = GetGuidStringFromByteArray(CType(dr(SpUserClaims.COL_NAME_SP_CLAIM_TYPE_ID), Byte()))
                        IsValidSPClaim = ValidateSpClaim(SPClaimTypeId, CType(dr(SpUserClaims.COL_NAME_SP_CLAIM_VALUE), String), EffectiveDateresult, ExpirationDateresult, False)

                        If Not IsValidSPClaim Then
                            Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_OVERLAPPING_SP_CLAIM, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_OVERLAPPING_SP_CLAIM)
                        End If

                        dr(SpUserClaims.COL_NAME_EXPIRATION_DATE) = ExpirationDate
                        Exit For
                    End If
                Next

                GridViewUserSecurity.EditIndex = -1
                State.IsEditing = False
                SetUserSecurityButtons(True)
                State.EnabledUserSecurityEditButton = True
                State.UserSecurityDataChanged = True
                RefreshSpUserClaims()

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Sub CancelUpdateSpClaimButton_Click(sender As Object, e As EventArgs) Handles CancelUpdateSpClaimButton.Click
            SetUserSecurityButtons(True)
            State.IsEditing = False
            GridViewUserSecurity.EditIndex = -1
            State.EnabledUserSecurityEditButton = True
            RefreshSpUserClaims()
        End Sub

        Private Sub btnCancelCompany_Click(sender As Object, e As EventArgs) Handles BtnCancelCompany.Click
            SetButtons(True)
            State.IsEditing = False
            Grid.EditIndex = -1
            RefreshAssignedCompany()
        End Sub
        Private Sub btnApply_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnApply_WRITE.Click
            Try
                State.HasDataChanged = False
                PopulateBOsFormFrom()
                If State.MyBO.IsFamilyDirty Then
                    State.HasDataChanged = True
                    State.MyBO.ValidateUserRolesAssigned()
                    State.MyBO.Save()
                    PopulateFormFromBOs()
                    EnableDisableFields()
                    ' User Security - SP Claims
                    If State.UserSecurityDataChanged = True Then
                        State.EnabledUserSecurityEditButton = True
                        RefreshSpUserClaims()
                        State.UserSecurityDataChanged = False
                    End If

                    MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                    '  ElitaPlusPage.BindListControlToDataView(Me.State.MyCurrentSelectedList, Me.State.UserCompanyAssigned, COL_DESCRIPTION_NAME, UserCompanyAssigned.COL_COMPANY_ID, False)
                    Dim listcontext As ListContext = New ListContext()
                    listcontext.UserId = State.MyBO.Id
                    Dim comLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("SelectedUserCompanyAssigned", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                    State.MyCurrentSelectedList.Populate(comLkl, New PopulateOptions())
                Else
                    MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
            Try
                PopulateBOsFormFrom()

                If State.MyBO.IsFamilyDirty And IsDataChanged() And btnSave_WRITE.Enabled = True Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
                DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                State.LastErrMsg = MasterPage.MessageController.Text
            End Try
        End Sub

        Private Sub btnDelete_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete_WRITE.Click
            Try
                State.MyBO.UserPermission.RevokeAll()
                DeleteAllRoles()
                State.UserCompanyAssigned.Table.Rows.Clear()
                RefreshAssignedCompany()

                ' User Security - SP Claims
                ExpirySecurityPermissionClaim()
                State.SpUserClaimDv.Table.Rows.Clear()
                RefreshSpUserClaims()

                DeleteAllCompanies()
                State.MyBO.Delete()
                State.MyBO.Save()
                State.HasDataChanged = True
                ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, State.MyBO, State.HasDataChanged))
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                'undo the delete
                State.MyBO.RejectChanges()
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub btnAddPermissionToSelected_Click(sender As System.Object, e As System.EventArgs) Handles btnAddPermissionToSelected.Click
            Try
                If lstAvailablePermission.SelectedItem Is Nothing Then Exit Sub
                State.MyBO.UserPermission.Grant(New Guid(lstAvailablePermission.SelectedItem.Value))
                LoadUserPermissions()
                LoadAvailablePermissions()
                State.DataChanged = True
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnAddToSelected_Click(sender As System.Object, e As System.EventArgs) Handles btnAddToSelected.Click
            Dim oListItem As New System.Web.UI.WebControls.ListItem
            Dim oArrayList As New ArrayList
            Try
                If lstAvailable.SelectedItem Is Nothing Then Exit Sub
                With oListItem
                    .Text = lstAvailable.SelectedItem.Text
                    .Value = lstAvailable.SelectedItem.Value
                End With
                oArrayList.Add(lstAvailable.SelectedItem.Value)
                State.MyBO.AttachUserRoles(oArrayList)
                UpdateView(lstAvailable, lstSelected)
                'AA648971
                CheckUserRoles()
                State.DataChanged = True
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnRemovePermission_Click(sender As System.Object, e As System.EventArgs) Handles btnRemovePermission.Click
            Try
                If lstSelectedPermission.SelectedItem Is Nothing Then Exit Sub
                State.MyBO.UserPermission.Revoke(New Guid(lstSelectedPermission.SelectedItem.Value))
                LoadUserPermissions()
                LoadAvailablePermissions()
                State.DataChanged = True
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnRemove_Click(sender As System.Object, e As System.EventArgs) Handles btnRemove.Click
            Try
                If lstSelected.SelectedItem Is Nothing Then Exit Sub
                Dim oListItem As New System.Web.UI.WebControls.ListItem
                Dim oArrayList As New ArrayList
                With oListItem
                    .Text = lstSelected.SelectedItem.Text
                    .Value = lstSelected.SelectedItem.Value
                End With
                oArrayList.Add(lstSelected.SelectedItem.Value)
                State.MyBO.DetachUserRoles(oArrayList)
                UpdateView(lstSelected, lstAvailable)
                'AA648971
                CheckUserRoles()
                State.DataChanged = True
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnCopy_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnCopy_WRITE.Click
            Try
                PopulateBOsFormFrom()
                If State.MyBO.IsFamilyDirty And IsDataChanged() Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
                Else
                    CreateNewWithCopy()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Protected Sub CreateNewWithCopy()
            Dim objOldBO As User = State.MyBO
            State.MyBO = New Assurant.ElitaPlus.BusinessObjectsNew.User
            txtNetworkID.Text = ""
            txtUserName.Text = ""

            'Copy the user roles from existing BO
            Dim selectedDv As DataView = State.MyBO.GetUserRoles(objOldBO.Id)
            Dim dr As DataRowView, roleID As Guid
            For Each dr In selectedDv
                roleID = New Guid(CType(dr.Row(COL_ID_NAME), Byte()))
                State.MyBO.AddRoleChild(roleID)
            Next


            State.SpUserClaimDv = State.MyBO.GetSpUserClaims(objOldBO.Id, objOldBO.LanguageId, SP_CLAIM_CODE)
            State.SpUserClaimDv.Table.DefaultView.Sort = SpUserClaims.COL_NAME_SP_CLAIM_TYPE_ID & " ASC," & SpUserClaims.COL_NAME_SP_CLAIM_VALUE & " ASC," & SpUserClaims.COL_NAME_EFFECTIVE_DATE & " DESC," & SpUserClaims.COL_NAME_EXPIRATION_DATE & " DESC"
            CloneSecurityPermissionClaim()
            RefreshSpUserClaims()

            EnableDisableFields()
        End Sub
        Private Sub btnSave_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnSave_WRITE.Click
            State.HasDataChanged = False
            Try
                PopulateBOsFormFrom()
                If State.MyBO.IsFamilyDirty Then
                    State.HasDataChanged = True
                    State.MyBO.ValidateUserRolesAssigned()
                    State.MyBO.Save()
                    PopulateFormFromBOs()

                    ' User Security - SP Claims
                    If State.UserSecurityDataChanged = True Then
                        State.EnabledUserSecurityEditButton = True
                        RefreshSpUserClaims()
                        State.UserSecurityDataChanged = False
                    End If

                    MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub btnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNew_WRITE.Click
            Try
                PopulateBOsFormFrom()
                If State.MyBO.IsFamilyDirty And IsDataChanged() Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
                Else
                    CreateNew()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Protected Sub CreateNew()
            State.MyBO = New Assurant.ElitaPlus.BusinessObjectsNew.User
            PopulateFormFromBOs()
            EnableDisableFields()
        End Sub

        Private Sub btnAddCompany_Click(sender As System.Object, e As System.EventArgs) Handles btnAddCompany.Click
            Try
                If cboCompanyAvailable.SelectedItem Is Nothing Then Exit Sub

                Dim dr As DataRow
                dr = State.UserCompanyAssigned.Table.NewRow()
                dr(UserCompanyAssigned.COL_AUTHORIZATION_LIMIT) = 0
                dr(UserCompanyAssigned.COL_PAYMENT_LIMIT) = 0
                dr(UserCompanyAssigned.COL_LIABILITY_OVERRIDE_LIMIT) = 0
                dr(UserCompanyAssigned.COL_USER_ID) = State.MyBO.Id.ToByteArray()
                dr(UserCompanyAssigned.COL_COMPANY_ID) = (New Guid(cboCompanyAvailable.SelectedItem.Value)).ToByteArray()
                dr(UserCompanyAssigned.COL_DESCRIPTION) = cboCompanyAvailable.SelectedItem.Text
                dr(UserCompanyAssigned.COL_IS_LOADED) = "F"
                State.UserCompanyAssigned.Table.Rows.Add(dr)
                RefreshAssignedCompany()
                State.DataChanged = True
                If cboCompanyAvailable.SelectedItem Is Nothing Then Exit Sub
                cboCompanyAvailable.Items.Remove(cboCompanyAvailable.SelectedItem)

                LoadCompanyGrpDropDownLists()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub RefreshAssignedCompany()
            Grid.DataSource = State.UserCompanyAssigned
            Grid.DataBind()
        End Sub


        Protected Sub CheckIfComingFromSaveConfirm()
            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value

            If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
                If State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                    If State.MyBO.IsFamilyDirty Then
                        State.HasDataChanged = True
                        State.MyBO.ValidateUserRolesAssigned()
                        State.MyBO.Save()
                    End If
                End If
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                    Case ElitaPlusPage.DetailPageCommand.New_
                        AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                        CreateNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                        CreateNewWithCopy()
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        ReturnToCallingPage(New ReturnType(State.ActionInProgress, State.MyBO, State.HasDataChanged))
                End Select
            ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                    Case ElitaPlusPage.DetailPageCommand.New_
                        CreateNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        CreateNewWithCopy()
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        MasterPage.MessageController.AddErrorAndShow(State.LastErrMsg)
                End Select
            End If
            'Clean after consuming the action
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            HiddenSaveChangesPromptResponse.Value = ""
        End Sub
#End Region

#Region "Handlers-DropDowns"

        'Private Sub cboCompanyGroupId_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCompanyGroupId.SelectedIndexChanged

        '    Try
        '        Me.State.MyCompanyGroupId = Me.GetSelectedItem(cboCompanyGroupId)
        '        Me.LoadAvailableCompany()
        '        Me.LoadUserCompanys()
        '    Catch ex As Exception
        '        Me.HandleErrors(ex, Me.MasterPage.MessageController)
        '    End Try
        'End Sub

        Private Sub rdoExternal_CheckedChanged(sender As Object, e As System.EventArgs) Handles rdoExternal.CheckedChanged
            Try
                ControlMgr.SetVisibleControl(Me, tblExternalUserDetails, True)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub rdoInternal_CheckedChanged(sender As Object, e As System.EventArgs) Handles rdoInternal.CheckedChanged
            Try
                ControlMgr.SetVisibleControl(Me, tblExternalUserDetails, False)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#End Region

#Region "Enable-Disable"

        Private Sub EnableDisableFields()
            ControlMgr.SetVisibleControl(Me, tblExternalUserDetails, rdoExternal.Checked)
            HideControlsForSpClaims()
        End Sub

        Private Sub HideControlsForSpClaims()
            ControlMgr.SetVisibleControl(Me, SpClaimCountry, False)
            ControlMgr.SetVisibleControl(Me, SpClaimDealer, False)
            ControlMgr.SetVisibleControl(Me, SpClaimValue, False)
            ControlMgr.SetVisibleControl(Me, SpClaimEffectiveDate, False)
            ControlMgr.SetVisibleControl(Me, SpClaimExpirationDate, False)
        End Sub

        Protected Sub BindBoPropertiesToLabels()

            BindBOPropertyToLabel(State.MyBO, "LanguageId", moLanguageLabel)
            BindBOPropertyToLabel(State.MyBO, "NetworkId", moNetworkLabel)
            BindBOPropertyToLabel(State.MyBO, "UserName", moUserLabel)
            ClearGridHeadersAndLabelsErrSign()
        End Sub

#End Region

#Region "Load And Clear"

        Private Sub LoadUserDetails()
            LoadUserRoles()
            LoadUserPermissions()
            LoadUserCompanys()
            LoadAvailableRoles()
            LoadAvailablePermissions()
            LoadAvailableCompany()
            LoadCompanyInfo()
            LoadLanguageInfo()
            LoadCompanyAssignedInfo()
            LoadCompanyGrpDropDownLists()
            LoadSpUserClaims()  ' Load SP User Claim Grid
        End Sub



        Private Function SetSelectedValue(sCurrentValue As String, oListControl As ListControl) As Integer
            oListControl.SelectedItem.Selected = False
            Dim oItem As System.Web.UI.WebControls.ListItem

            For Each oItem In oListControl.Items
                If oItem.Value = sCurrentValue Then
                    oItem.Selected = True
                End If
            Next
        End Function

        Private Sub LoadCompanyAssignedInfo()
            State.UserCompanyAssigned = State.MyBO.GetSelectedAssignedCompanies(State.MyBO.Id)
            '     ElitaPlusPage.BindListControlToDataView(Me.State.MyCurrentSelectedList, Me.State.UserCompanyAssigned, COL_DESCRIPTION_NAME, UserCompanyAssigned.COL_COMPANY_ID, False)
            Dim listcontext As ListContext = New ListContext()
            listcontext.UserId = State.MyBO.Id
            State.usercomAssignedLkl = CommonConfigManager.Current.ListManager.GetList("SelectedUserCompanyAssigned", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            State.MyCurrentSelectedList.Populate(State.usercomAssignedLkl, New PopulateOptions())
            RefreshAssignedCompany()
        End Sub

        Private Sub LoadLanguageInfo()
            ' Me.BindListControlToDataView(cboLanguagesId, LookupListNew.GetLanguageLookupList(False), , , False)
            Dim langLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.LanguageList, Thread.CurrentPrincipal.GetLanguageCode())
            cboLanguagesId.Populate(langLkl, New PopulateOptions())
            BindSelectItem(State.MyBO.LanguageId.ToString, cboLanguagesId)
        End Sub

        Private Sub LoadCompanyInfo()

            'Me.BindListControlToDataView(Me.cboCompanyGroupId, LookupListNew.DataView(LookupListNew.LK_COMPANY_GROUP))
            'Me.SetSelectedItem(Me.cboCompanyGroupId, Me.State.MyCompanyGroupId)
        End Sub


        Private Sub ClearTexts()
            txtUserName.Text = Nothing
            txtNetworkID.Text = Nothing
        End Sub

        Private Function CopyListBox(source As ListControl) As ArrayList
            Dim oItem As System.Web.UI.WebControls.ListItem
            Dim target As New ArrayList

            For Each oItem In source.Items
                target.Add(oItem)
            Next
            Return target
        End Function

        Private Sub RemoveSelectedRoles()
            Dim oItem As System.Web.UI.WebControls.ListItem
            Dim tempSelected As ArrayList = CopyListBox(lstSelected)
            Dim oArrayList As New ArrayList

            For Each oItem In tempSelected
                Dim oListItem As New System.Web.UI.WebControls.ListItem
                With oListItem
                    .Text = oItem.Text
                    .Value = oItem.Value
                    lstSelected.SelectedValue = .Value
                End With
                oArrayList.Add(lstSelected.SelectedItem.Value)
                State.MyBO.DetachUserRoles(oArrayList)
                UpdateView(lstSelected, lstAvailable)
            Next
        End Sub

        Private Sub RemoveSelectedCompanies()
            Dim oItem As System.Web.UI.WebControls.ListItem
            Dim tempSelected As ArrayList = CopyListBox(cboCompanyAvailable)

            For Each oItem In tempSelected
                Dim oListItem As New System.Web.UI.WebControls.ListItem
                With oListItem
                    .Text = oItem.Text
                    .Value = oItem.Value
                    lstSelected.SelectedValue = .Value
                End With
                UpdateView(cboCompanyAvailable, cboCompanyAvailable)
            Next
        End Sub

        Private Sub UpdateView(oSourceList As ListControl, oTargetList As ListControl)
            Dim CurrentListItem As New System.Web.UI.WebControls.ListItem

            If oSourceList.SelectedItem Is Nothing Then Exit Sub
            CurrentListItem.Text = oSourceList.SelectedItem.Text
            CurrentListItem.Value = oSourceList.SelectedItem.Value
            oSourceList.Items.Remove(CurrentListItem)
            oTargetList.Items.Add(CurrentListItem)
        End Sub

        Private Sub ClearPermissions()
            ClearList(lstAvailablePermission)
            ClearList(lstSelectedPermission)
        End Sub

        Private Sub ClearRoles()
            ClearList(lstAvailable)
            ClearList(lstSelected)
        End Sub

        Private Sub ClearLists()
            ClearPermissions()
            ClearRoles()
            ClearCompanies()
            ClearList(cboLanguagesId)
        End Sub

        Private Sub ClearAll()
            ClearTexts()
            ClearLists()
            rdoActive.Checked = False
            rdoInActive.Checked = False
        End Sub

        Private Sub SetButtons(enable As Boolean)
            Dim actionEnabled As Boolean = enable And Not State.ViewOnly
            Dim actionNotEnabled As Boolean = Not enable And Not State.ViewOnly
            ControlMgr.SetEnableControl(Me, btnSave_WRITE, actionEnabled)
            ControlMgr.SetEnableControl(Me, btnApply_WRITE, True)
            ControlMgr.SetEnableControl(Me, btnBack, enable)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, actionEnabled)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, actionEnabled)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, actionEnabled)
            ControlMgr.SetEnableControl(Me, BtnSaveCompany, actionNotEnabled)
            ControlMgr.SetEnableControl(Me, BtnCancelCompany, actionNotEnabled)
        End Sub
        'AA648971
        Private Sub CheckUserRoles()
            Dim IHQRolesDV As DataView = CType(Session("IHQRoles"), DataView)
            Dim blnIHQ_Role As Boolean = False
            Dim indexA, indexB As Integer
            For indexA = 0 To lstSelected.Items.Count - 1
                For indexB = 0 To IHQRolesDV.Count - 1
                    If lstSelected.Items(indexA).Value.Equals(New Guid(CType(IHQRolesDV.Item(indexB).Item(0), Byte())).ToString) Then
                        blnIHQ_Role = True
                        Exit For
                    End If
                Next
                If blnIHQ_Role Then Exit For
            Next
            If blnIHQ_Role Then
                IsExternal = False
                rdoExternal.Checked = False
                rdoInternal.Checked = True
                rdoExternal.Enabled = False
                rdoInternal.Enabled = False
                ControlMgr.SetVisibleControl(Me, tblExternalUserDetails, False)
            Else
                rdoExternal.Enabled = True
                rdoInternal.Enabled = True
            End If

        End Sub

        Private Sub LoadIHQRoles()
            Dim IHQRolesDV As DataView = LookupListNew.GetIHQRolesLookupList()
            Session.Add("IHQRoles", IHQRolesDV)
        End Sub

#End Region


#Region "Business"
        Private Sub UpdateBreadCrum()
            If (State IsNot Nothing) Then
                If (State IsNot Nothing) Then
                    MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("User")
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("User")
                End If
            End If
        End Sub

        Private Function IsDataChanged() As Boolean
            If State.MyBO.IsDirty Or State.DataChanged = True Or State.UserSecurityDataChanged = True Then
                Return True
            Else
                Return False
            End If
        End Function

        'Private Sub LoadUserGroup()
        '    Try
        '        Dim oUserCompany As UserCompany = New UserCompany(Me.State.MyBO.Id, "N")
        '        Dim oCompany As Assurant.ElitaPlus.BusinessObjectsNew.Company = New Assurant.ElitaPlus.BusinessObjectsNew.Company(oUserCompany.CompanyId)
        '        Me.State.MyCompanyGroupId = oCompany.CompanyGroupId
        '    Catch ex As Exception
        '        Me.State.MyCompanyGroupId = Guid.Empty
        '    End Try
        'End Sub

        Private Sub PopulateFormFromBOs()
            With State.MyBO
                AccountStatus = .Active = "Y"
                IsExternal = .External = "Y"
                PopulateControlFromBOProperty(txtNetworkID, .NetworkId)
                PopulateControlFromBOProperty(txtUserName, .UserName)

            End With
            LoadUserDetails()
            'AA648971
            LoadIHQRoles()
            CheckUserRoles()
            LoadExternalUserDetail()
            SetButtons(True)
            SetUserSecurityButtons(True)
        End Sub

        Private Sub PopulateBOsFormFrom()

            If State.UserCompanyAssigned.Count = 0 Then
                Throw New GUIException(Message.MSG_COMPANY_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_REQUIRED)
            End If

            ' ApplyAllRoleChanges()
            ApplyAllCompanyChanges()
            If State.UserSecurityDataChanged = True Then
                AddSecurityPermissionClaim()
            End If

            With State.MyBO
                If AccountStatus Then
                    .Active = "Y"
                Else
                    .Active = "N"
                End If

                PopulateBOProperty(State.MyBO, "LanguageId", cboLanguagesId)
                PopulateBOProperty(State.MyBO, "NetworkId", txtNetworkID)
                PopulateBOProperty(State.MyBO, "UserName", txtUserName)

                If IsExternal Then
                    .External = "Y"
                    If Not GetSelectedItem(cboDealerGroupId).Equals(Guid.Empty) Then
                        .ExternalTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_EXTERNAL_USER_TYPES, Codes.EXTERNAL_USER_TYPE__DEALER_GROUP)
                        .ScDealerId = GetSelectedItem(cboDealerGroupId)
                    ElseIf Not GetSelectedItem(cboDealerId).Equals(Guid.Empty) Then
                        .ExternalTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_EXTERNAL_USER_TYPES, Codes.EXTERNAL_USER_TYPE__DEALER)
                        .ScDealerId = GetSelectedItem(cboDealerId)
                    ElseIf Not GetSelectedItem(cboServiceCenterId).Equals(Guid.Empty) Then
                        .ExternalTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_EXTERNAL_USER_TYPES, Codes.EXTERNAL_USER_TYPE__SERVICE_CENTER)
                        .ScDealerId = GetSelectedItem(cboServiceCenterId)
                    ElseIf chkOther.Checked Then
                        .ExternalTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_EXTERNAL_USER_TYPES, Codes.EXTERNAL_USER_TYPE__OTHER)
                        .ScDealerId = Nothing
                    Else
                        ElitaPlusPage.SetLabelError(moDealerGroupLabel)
                        ElitaPlusPage.SetLabelError(moDealerLabel)
                        ElitaPlusPage.SetLabelError(LabelSearchServiceCenter)
                        ElitaPlusPage.SetLabelError(lblOther)
                        Throw New GUIException(Message.MSG_EXTERNAL_USER_DEALER_OR_SC_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.GUI_EXTERNAL_USER_DEALER_OR_SC_REQUIRED)
                    End If
                Else
                    .External = "N"
                    .ExternalTypeId = Nothing
                    .ScDealerId = Nothing
                End If
            End With
            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If

        End Sub



        ' Must chek befor continue
        Private Sub LoadCompanyGrpDropDownLists()
            Dim selectedCompanies, selectedCountries As ArrayList

            If State.UserCompanyAssigned.Count > 0 Then
                selectedCompanies = GetSelectedCompanies()
                selectedCountries = Country.GetCountries(selectedCompanies) 'dg.code || ' - ' || dg.description
                ' Me.BindListControlToDataView(Me.cboDealerGroupId, LookupListNew.GetDealersGroupsByCompanyLookupList(selectedCompanies), , , True) 'GetDealerGroupListByCompanyForUser changes
                Dim dealLkl = GetDealerGroupListByCompanyForUser()
                Dim textFun As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                                                                            Return li.Code + "-" + li.Translation
                                                                        End Function
                cboDealerGroupId.Populate(dealLkl, New PopulateOptions() With
                                           {
                                            .TextFunc = textFun,
                                            .SortFunc = textFun,
                                            .AddBlankItem = True
                                           })
                ' Me.BindListControlToDataView(Me.cboDealerId, LookupListNew.GetDealerLookupList(selectedCompanies), , , True)
                Dim oDealerList = GetDealerListByCompanyForUser()
                Dim dealerTextFunc As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                                                                                   Return li.ExtendedCode + "-" + li.Translation
                                                                               End Function
                cboDealerId.Populate(oDealerList, New PopulateOptions() With
                                           {
                                            .TextFunc = dealerTextFunc,
                                            .SortFunc = dealerTextFunc,
                                            .AddBlankItem = True
                                           })
                ' Me.BindListControlToDataView(Me.cboServiceCenterId, LookupListNew.GetServiceCenterLookupList(selectedCountries), , , True)
                Dim SvcListLkl = GetSVCList()
                cboServiceCenterId.Populate(SvcListLkl, New PopulateOptions() With
                                           {
                                            .AddBlankItem = True
                                           })

            End If
        End Sub
        Private Function GetDealerListByCompanyForUser() As Assurant.Elita.CommonConfiguration.DataElements.ListItem()
            Dim selectedCompanies As ArrayList
            selectedCompanies = GetSelectedCompanies()
            Dim Index As Integer
            Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext

            Dim UserCompanies As ArrayList = selectedCompanies

            Dim oDealerList As New Collections.Generic.List(Of Assurant.Elita.CommonConfiguration.DataElements.ListItem)

            For Index = 0 To UserCompanies.Count - 1
                'UserCompanyList &= ",'" & GuidControl.GuidToHexString(UserCompanyies(Index))
                oListContext.CompanyId = UserCompanies(Index)
                Dim oDealerListForCompany As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerListByCompany", context:=oListContext)
                If oDealerListForCompany.Count > 0 Then
                    If oDealerList IsNot Nothing Then
                        oDealerList.AddRange(oDealerListForCompany)
                    Else
                        oDealerList = oDealerListForCompany.Clone()
                    End If

                End If
            Next

            Return oDealerList.ToArray()

        End Function
        Private Function GetDealerGroupListByCompanyForUser() As Assurant.Elita.CommonConfiguration.DataElements.ListItem()
            Dim selectedCompanies As ArrayList
            selectedCompanies = GetSelectedCompanies()
            Dim Index As Integer
            Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext

            Dim UserCompanies As ArrayList = selectedCompanies

            Dim oDealerList As New Collections.Generic.List(Of Assurant.Elita.CommonConfiguration.DataElements.ListItem)

            For Index = 0 To UserCompanies.Count - 1
                'UserCompanyList &= ",'" & GuidControl.GuidToHexString(UserCompanyies(Index))
                oListContext.CompanyId = UserCompanies(Index)
                Dim oDealerListForCompany As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerGroupByCompany", context:=oListContext)
                If oDealerListForCompany.Count > 0 Then
                    If oDealerList IsNot Nothing Then
                        oDealerList.AddRange(oDealerListForCompany)
                    Else
                        oDealerList = oDealerListForCompany.Clone()
                    End If

                End If
            Next

            Return oDealerList.ToArray()

        End Function
        Private Function GetSVCList() As Assurant.Elita.CommonConfiguration.DataElements.ListItem()
            Dim selectedCountries As ArrayList
            Dim selectedCompanies As ArrayList
            selectedCompanies = GetSelectedCompanies()
            selectedCountries = Country.GetCountries(selectedCompanies)
            Dim Index As Integer
            Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext

            Dim UserCountries As ArrayList = selectedCountries

            Dim oSVCList As New Collections.Generic.List(Of Assurant.Elita.CommonConfiguration.DataElements.ListItem)

            For Index = 0 To UserCountries.Count - 1
                'UserCompanyList &= ",'" & GuidControl.GuidToHexString(UserCompanyies(Index))
                oListContext.CountryId = UserCountries(Index)
                Dim oSvcListByCountry As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.ServiceCenterListByCountry, context:=oListContext)
                If oSvcListByCountry.Count > 0 Then
                    If oSVCList IsNot Nothing Then
                        oSVCList.AddRange(oSvcListByCountry)
                    Else
                        oSVCList = oSvcListByCountry.Clone()
                    End If

                End If
            Next

            Return oSVCList.ToArray()

        End Function

#Region "Permissions"
        Private Sub LoadAvailablePermissions()

            Dim availableDv As DataView = State.MyBO.UserPermission.GetAvailablePermissions()
            lstAvailablePermission.Items.Clear()
            ElitaPlusPage.BindListControlToDataView(lstAvailablePermission, availableDv, COL_DESCRIPTION_NAME, COL_ID_NAME, False)

        End Sub

        Private Sub LoadUserPermissions()
            Try
                Dim selectedDv As DataView = State.MyBO.UserPermission.GetSelectedPermissions()
                lstSelectedPermission.Items.Clear()
                ElitaPlusPage.BindListControlToDataView(lstSelectedPermission, selectedDv, COL_DESCRIPTION_NAME, COL_ID_NAME, False)

            Catch ex As Exception
                MasterPage.MessageController.AddError(MAINTAIN_USER_FORM003)
                MasterPage.MessageController.AddError(ex.Message)
                MasterPage.MessageController.Show()

            End Try

        End Sub
#End Region

#Region "Roles"

        Private Sub LoadAvailableRoles()

            '  Dim availableDv As DataView = Me.State.MyBO.GetAvailableRoles(Me.State.MyBO.Id) 'UserRoles
            lstAvailable.Items.Clear()
            '  ElitaPlusPage.BindListControlToDataView(Me.lstAvailable, availableDv, COL_DESCRIPTION_NAME, COL_ID_NAME, False)
            Dim listcontext As ListContext = New ListContext()
            listcontext.UserId = State.MyBO.Id
            Dim availRoleLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("AvailableRolesByUser", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)

            lstAvailable.Populate(availRoleLkl, New PopulateOptions())

        End Sub

        Private Sub LoadUserRoles()
            Try
                '  Dim selectedDv As DataView = Me.State.MyBO.GetUserRoles(Me.State.MyBO.Id)
                lstSelected.Items.Clear()
                '  ElitaPlusPage.BindListControlToDataView(Me.lstSelected, selectedDv, COL_DESCRIPTION_NAME, COL_ID_NAME, False)
                Dim listcontext As ListContext = New ListContext()
                listcontext.UserId = State.MyBO.Id
                Dim roleLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.UserRoles, Thread.CurrentPrincipal.GetLanguageCode(), listcontext)

                lstSelected.Populate(roleLkl, New PopulateOptions())
            Catch ex As Exception
                MasterPage.MessageController.AddError(MAINTAIN_USER_FORM003)
                MasterPage.MessageController.AddError(ex.Message)
                MasterPage.MessageController.Show()

            End Try

        End Sub

        Private Sub LoadExternalUserDetail()
            If IsExternal Then
                If State.MyBO.ExternalTypeId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_EXTERNAL_USER_TYPES, Codes.EXTERNAL_USER_TYPE__DEALER_GROUP)) Then
                    'User is dealer group
                    BindSelectItem(State.MyBO.ScDealerId.ToString, cboDealerGroupId)
                ElseIf State.MyBO.ExternalTypeId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_EXTERNAL_USER_TYPES, Codes.EXTERNAL_USER_TYPE__DEALER)) Then
                    'User is dealer
                    BindSelectItem(State.MyBO.ScDealerId.ToString, cboDealerId)
                ElseIf State.MyBO.ExternalTypeId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_EXTERNAL_USER_TYPES, Codes.EXTERNAL_USER_TYPE__SERVICE_CENTER)) Then
                    'User is Service Center
                    BindSelectItem(State.MyBO.ScDealerId.ToString, cboServiceCenterId)
                ElseIf State.MyBO.ExternalTypeId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_EXTERNAL_USER_TYPES, Codes.EXTERNAL_USER_TYPE__OTHER)) Then
                    'User is Other
                    chkOther.Checked = True
                End If
            End If

        End Sub

        'Sub EndChildEdit(ByVal lastop As ElitaPlusPage.DetailPageCommand)
        '    Try
        '        With Me.State
        '            Select Case lastop
        '                Case ElitaPlusPage.DetailPageCommand.OK
        '                    .MyRoleChildBO.Save()
        '                Case ElitaPlusPage.DetailPageCommand.Cancel
        '                    .MyRoleChildBO.cancelEdit()
        '                    If .MyRoleChildBO.IsSaveNew Then
        '                        .MyRoleChildBO.Delete()
        '                        .MyRoleChildBO.Save()
        '                    End If
        '                Case ElitaPlusPage.DetailPageCommand.Delete
        '                    .MyRoleChildBO.Delete()
        '                    .MyRoleChildBO.Save()
        '            End Select
        '        End With
        '    Catch ex As Exception
        '        Me.HandleErrors(ex, Me.MasterPage.MessageController)
        '    End Try
        'End Sub

        'Private Sub ApplyRoleChange(ByVal CurrentListItem As ListItem)
        '    Dim oRoleId As New Guid(CurrentListItem.Value)
        '    Me.State.MyRoleChildBO = Me.State.MyBO.AddUserRoleChild(oRoleId)
        '    Me.EndChildEdit(ElitaPlusPage.DetailPageCommand.OK)
        'End Sub

        'Private Sub DeleteRole(ByVal CurrentListItem As ListItem)
        '    Dim oRoleId As New Guid(CurrentListItem.Value)
        '    Me.State.MyRoleChildBO = Me.State.MyBO.GetUserRoleChild(oRoleId)
        '    Me.EndChildEdit(ElitaPlusPage.DetailPageCommand.Delete)
        'End Sub

        Private Sub DeleteAllRoles()
            RemoveSelectedRoles()
            'ApplyAllRoleChanges()
        End Sub


#End Region

#Region "Companies"

        Private Sub LoadAvailableCompany()

            cboCompanyAvailable.Items.Clear()
            'Dim availableDv As DataView = Me.State.MyBO.GetAvailableAssignedCompanies(Me.State.MyBO.Id)
            cboCompanyAvailable.Items.Clear()
            ' ElitaPlusPage.BindListControlToDataView(Me.cboCompanyAvailable, availableDv, COL_DESCRIPTION_NAME, COL_ID_NAME, False)
            Dim listcontext As ListContext = New ListContext()
            listcontext.UserId = State.MyBO.Id
            Dim comUesrLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("SelectedUserCompanyAssigned", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            Dim comLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.Company, Thread.CurrentPrincipal.GetLanguageCode())
            Dim FilteredRecord As ListItem() = (From x In comLkl
                                                Where Not (comUesrLkl).Contains(x)
                                                Select x).ToArray()
            cboCompanyAvailable.Populate(FilteredRecord, New PopulateOptions())

        End Sub

        Private Sub LoadUserCompanys()

            Try
                Grid.DataSource = State.UserCompanyAssigned
                Grid.DataBind()

            Catch ex As Exception
                MasterPage.MessageController.AddError(MAINTAIN_USER_FORM003)
                MasterPage.MessageController.AddError(ex.Message)
                MasterPage.MessageController.Show()
            End Try

        End Sub

        Sub EndCompanyChildEdit(lastop As ElitaPlusPage.DetailPageCommand)
            Try
                With State
                    Select Case lastop
                        Case ElitaPlusPage.DetailPageCommand.OK
                            .MyCompanyChildBO.Save()
                        Case ElitaPlusPage.DetailPageCommand.Cancel
                            .MyCompanyChildBO.cancelEdit()
                            If .MyCompanyChildBO.IsSaveNew Then
                                .MyCompanyChildBO.Delete()
                                .MyCompanyChildBO.Save()
                            End If
                        Case ElitaPlusPage.DetailPageCommand.Delete
                            .MyCompanyChildBO.Delete()
                            .MyCompanyChildBO.Save()
                    End Select
                End With
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Sub EndUserCompanyChildEdit(lastop As ElitaPlusPage.DetailPageCommand)
            Try
                With State
                    Select Case lastop
                        Case ElitaPlusPage.DetailPageCommand.OK
                            .MyUserCompanyChildBO.Save()
                        Case ElitaPlusPage.DetailPageCommand.Cancel
                            .MyUserCompanyChildBO.cancelEdit()
                            If .MyUserCompanyChildBO.IsSaveNew Then
                                .MyUserCompanyChildBO.Delete()
                                .MyUserCompanyChildBO.Save()
                            End If
                        Case ElitaPlusPage.DetailPageCommand.Delete
                            .MyUserCompanyChildBO.Delete()
                            .MyUserCompanyChildBO.Save()
                    End Select
                End With
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub


        Private Sub ApplyCompanyChange(oCompanyId As Guid, oAuthorizationLimit As Decimal, oPaymentLimit As Decimal, oLiabilityOverrideLimit As Decimal)
            State.MyCompanyChildBO = State.MyBO.AddCompanyGrpChild(oCompanyId, oAuthorizationLimit, oPaymentLimit, oLiabilityOverrideLimit)
            EndCompanyChildEdit(ElitaPlusPage.DetailPageCommand.OK)
        End Sub

        Private Sub DeleteCompany(CurrentListItem As System.Web.UI.WebControls.ListItem)
            Dim oCompanyId As New Guid(CurrentListItem.Value)
            State.MyCompanyChildBO = State.MyBO.GetCompanyGrpChild(oCompanyId)
            EndCompanyChildEdit(ElitaPlusPage.DetailPageCommand.Delete)
        End Sub

        Private Sub DeleteAllCompanies()
            RemoveSelectedCompanies()
            ApplyAllCompanyChanges()
        End Sub

        'add the child companies to the elpuser object.
        Private Sub ApplyAllCompanyChanges()
            Dim oListItem As New System.Web.UI.WebControls.ListItem
            Dim oFirstCompanyAssignedId As Guid = Guid.Empty
            Dim oFirstCompnayAssignedName As String = String.Empty

            State.MyBO.InitCompanyGrpTable()

            ' Delete companies that are in the DB 
            For Each oListItem In State.MyCurrentSelectedList.Items
                Try
                    DeleteCompany(oListItem)
                Catch ex As Exception
                End Try
            Next

            ' Add Selected Companies
            Dim dr As DataRow
            For Each dr In State.UserCompanyAssigned.Table.Rows
                Try
                    Dim oCurrentCompanyId As Guid = New Guid(CType(dr(Assurant.ElitaPlus.DALObjects.UserDAL.COL_NAME_COMPANY_ID), Byte()))
                    ApplyCompanyChange(oCurrentCompanyId,
                                       CType(dr(UserCompanyAssigned.COL_AUTHORIZATION_LIMIT), Decimal),
                                       CType(dr(UserCompanyAssigned.COL_PAYMENT_LIMIT), Decimal),
                                       CType(dr(UserCompanyAssigned.COL_LIABILITY_OVERRIDE_LIMIT), Decimal))
                    If oFirstCompanyAssignedId = Guid.Empty Then
                        oFirstCompanyAssignedId = oCurrentCompanyId
                        oFirstCompnayAssignedName = dr(Assurant.ElitaPlus.DALObjects.UserDAL.COL_NAME_DESCRIPTION).ToString()
                    Else
                        If oFirstCompnayAssignedName.Trim.ToUpper > dr(Assurant.ElitaPlus.DALObjects.UserDAL.COL_NAME_DESCRIPTION).ToString() Then
                            oFirstCompnayAssignedName = dr(Assurant.ElitaPlus.DALObjects.UserDAL.COL_NAME_DESCRIPTION).ToString()
                            oFirstCompanyAssignedId = oCurrentCompanyId
                        End If
                    End If
                Catch ex As Exception
                    HandleErrors(ex, MasterPage.MessageController)
                End Try
            Next


            'If Not State.MyBO.IsIHQRole Then
            ' Delete user companies that are in the DB 
            Dim oCompanyID As Guid
            Dim flag As Boolean = False
            For Each oCompanyID In State.MyBO.Companies
                Try
                    If BusinessObjectBase.FindRow(oCompanyID, UserCompanyAssigned.COL_COMPANY_ID, State.UserCompanyAssigned.Table) Is Nothing Then
                        State.MyUserCompanyChildBO = State.MyBO.GetUserCompanyGrpChild(oCompanyID)
                        EndUserCompanyChildEdit(ElitaPlusPage.DetailPageCommand.Delete)
                    Else
                        flag = True
                    End If

                Catch ex As Exception
                End Try
            Next

            'If there is not a single assigned company add the first one of Usr Assigned Companies
            If Not flag Then
                'Added condition to check If UserCompanyAssigned has no value then skip adding User Assigned Companies. Def-24047.
                If (State.UserCompanyAssigned.Table IsNot Nothing And State.UserCompanyAssigned.Table.Rows.Count > 0) Then
                    State.MyUserCompanyChildBO = State.MyBO.AddUserCompanyGrpChild(New Guid(CType(State.UserCompanyAssigned.Table.Rows(0)(Assurant.ElitaPlus.DALObjects.UserDAL.COL_NAME_COMPANY_ID), Byte())))
                    EndUserCompanyChildEdit(ElitaPlusPage.DetailPageCommand.OK)
                End If
            End If
        End Sub

        Private Sub ClearCompanies()
            ClearList(cboCompanyAvailable)
            State.UserCompanyAssigned.Table.Rows.Clear()
            RefreshAssignedCompany()
        End Sub

        Private Function GetSelectedCompanies() As ArrayList
            Dim dr As DataRow
            Dim selectedCompanyArray As New ArrayList

            For Each dr In State.UserCompanyAssigned.Table.Rows
                selectedCompanyArray.Add(GetGuidFromString(GetGuidStringFromByteArray(CType(dr(UserCompanyAssigned.COL_COMPANY_ID), Byte()))))
            Next
            Return selectedCompanyArray
        End Function

#End Region

#Region "User Grid Releated"
        'The Binding LOgic is here
        Public Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try

                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                If dvRow IsNot Nothing Then
                    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                        If State.IsEditing OrElse State.ViewOnly Then
                            e.Row.Cells(GRID_COL_EDIT_BUTTON_IDX).FindControl(EDIT_COMPANY_BUTTON).Visible = False
                            e.Row.Cells(GRID_COL_EDIT_BUTTON_IDX).FindControl(DELETE_COMPANY_BUTTON).Visible = False
                        End If

                        e.Row.Cells(GRID_COL_USER_ID_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(UserCompanyAssigned.COL_USER_ID), Byte()))
                        e.Row.Cells(GRID_COL_COMPANY_ID_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(UserCompanyAssigned.COL_COMPANY_ID), Byte()))
                        e.Row.Cells(GRID_COL_COMPANY_NAME_IDX).Text = dvRow(UserCompanyAssigned.COL_DESCRIPTION).ToString

                        If (e.Row.DataItemIndex = Grid.EditIndex) Then
                            CType(e.Row.Cells(GRID_COL_AUTHORIZATION_LIMIT).FindControl(AUTHORIZATION_LIMIT_TEXT), TextBox).Text = dvRow(UserCompanyAssigned.COL_AUTHORIZATION_LIMIT).ToString
                            CType(e.Row.Cells(GRID_COL_PAYMENT_LIMIT).FindControl(PAYMENT_LIMIT_TEXT), TextBox).Text = dvRow(UserCompanyAssigned.COL_PAYMENT_LIMIT).ToString
                            CType(e.Row.Cells(GRID_COL_LIABILITY_OVERRIDE_LIMIT).FindControl(LIABILITY_OVERRIDE_LIMIT_TEXT), TextBox).Text = dvRow(UserCompanyAssigned.COL_LIABILITY_OVERRIDE_LIMIT).ToString
                        Else
                            CType(e.Row.Cells(GRID_COL_AUTHORIZATION_LIMIT).FindControl(AUTHORIZATION_LIMIT_LABEL), Label).Text = dvRow(UserCompanyAssigned.COL_AUTHORIZATION_LIMIT).ToString
                            CType(e.Row.Cells(GRID_COL_PAYMENT_LIMIT).FindControl(PAYMENT_LIMIT_LABEL), Label).Text = dvRow(UserCompanyAssigned.COL_PAYMENT_LIMIT).ToString
                            CType(e.Row.Cells(GRID_COL_LIABILITY_OVERRIDE_LIMIT).FindControl(LIABILITY_OVERRIDE_LIMIT_LABEL), Label).Text = dvRow(UserCompanyAssigned.COL_LIABILITY_OVERRIDE_LIMIT).ToString
                        End If
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub


        Protected Sub Grid_ItemCommand(source As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
            Try
                If e.CommandName = DELETE_ACTION Then
                    Dim index As Integer = CInt(e.CommandArgument)

                    'REQ 861
                    'Check if the Company you are trying to remove has any work queue assigned for this user
                    'if yes then don't allow to remove/delete that company from assigned company
                    Dim Comp_id As Guid = New Guid(Grid.Rows(index).Cells(GRID_COL_COMPANY_ID_IDX).Text)
                    If Not State.MyBO.CheckWorkQueueAssigned(Comp_id) Then
                        Throw New GUIException(Message.WRkQueue_COMPANY_USER_EXISTS, Assurant.ElitaPlus.Common.ErrorCodes.WORKQUEUE_COMPANY_EXISTS)
                    End If

                    Dim oCompanyListItem As New System.Web.UI.WebControls.ListItem
                    With oCompanyListItem
                        .Text = Grid.Rows(index).Cells(GRID_COL_COMPANY_NAME_IDX).Text
                        .Value = Grid.Rows(index).Cells(GRID_COL_COMPANY_ID_IDX).Text
                    End With
                    cboCompanyAvailable.Items.Add(oCompanyListItem)
                    Dim dr As DataRow
                    For Each dr In State.UserCompanyAssigned.Table.Rows
                        If (GetGuidStringFromByteArray(CType(dr(UserCompanyAssigned.COL_COMPANY_ID), Byte())) = oCompanyListItem.Value) Then
                            State.UserCompanyAssigned.Table.Rows.Remove(dr)
                            Exit For
                        End If
                    Next
                    RefreshAssignedCompany()
                    LoadCompanyGrpDropDownLists()
                    Grid.EditIndex = -1
                    State.IsEditing = False
                    State.DataChanged = True
                ElseIf e.CommandName = EDIT_ACTION Then
                    Dim index As Integer = CInt(e.CommandArgument)
                    Grid.EditIndex = index
                    Grid.SelectedIndex = index
                    SetButtons(False)
                    State.IsEditing = True
                    RefreshAssignedCompany()
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "User Security - Grid related events"
        Public Sub GridViewUserSecurity_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridViewUserSecurity.RowDataBound
            Try

                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                If dvRow IsNot Nothing Then
                    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                        If State.IsEditing Then
                            e.Row.Cells(USER_SECURITY_GRID_COL_EDIT_BUTTON_IDX).FindControl(EDIT_SP_CLAIM_BUTTON).Visible = False
                        End If

                        Dim SpUserClaimsID As String
                        If dvRow(SpUserClaims.COL_NAME_SP_USER_CLAIMS_ID).ToString = String.Empty Then
                            SpUserClaimsID = GetGuidStringFromByteArray(dvRow(SpUserClaims.COL_NAME_SP_USER_CLAIMS_ID), "true")
                        Else
                            SpUserClaimsID = GetGuidStringFromByteArray(CType(dvRow(SpUserClaims.COL_NAME_SP_USER_CLAIMS_ID), Byte()), "true")
                        End If
                        CType(e.Row.Cells(USER_SECURITY_GRID_COL_SP_USER_CLAIMS_ID_IDX).FindControl(SP_USER_CLAIMS_ID_LABEL), Label).Text = SpUserClaimsID
                        CType(e.Row.Cells(USER_SECURITY_GRID_COL_SP_CLAIM_TYPE_ID_IDX).FindControl(SP_CLAIM_TYPE_ID_LABEL), Label).Text = GetGuidStringFromByteArray(CType(dvRow(SpUserClaims.COL_NAME_SP_CLAIM_TYPE_ID), Byte()))
                        CType(e.Row.Cells(USER_SECURITY_GRID_COL_SP_CLAIM_CODE_DESCRIPTION_IDX).FindControl(SP_CLAIM_CODE_DESCRIPTION_LABEL), Label).Text = dvRow(SpUserClaims.COL_NAME_SP_CLAIM_CODE_DESCRIPTION).ToString
                        CType(e.Row.Cells(USER_SECURITY_GRID_COL_SP_CLAIM_VALUE_IDX).FindControl(SP_CLAIM_VALUE_LABEL), Label).Text = dvRow(SpUserClaims.COL_NAME_SP_CLAIM_VALUE).ToString

                        'If (dvRow(SpUserClaims.COL_NAME_SP_CLAIM_CODE_DESCRIPTION).ToString = "Client IP Address") Then
                        CType(e.Row.Cells(USER_SECURITY_GRID_COL_SP_CLAIM_VALID_FROM_IDX).FindControl(SP_CLAIM_EFFECTIVEDATE_LABEL), Label).Text = GetDateFormattedStringNullable(dvRow(SpUserClaims.COL_NAME_EFFECTIVE_DATE))

                        If (e.Row.DataItemIndex = GridViewUserSecurity.EditIndex) Then
                            CType(e.Row.Cells(USER_SECURITY_GRID_COL_SP_CLAIM_VALID_TO_IDX).FindControl(SP_CLAIM_EXPIRATIONDATE_TEXT), TextBox).Text = GetDateFormattedStringNullable(dvRow(SpUserClaims.COL_NAME_EXPIRATION_DATE))
                        Else
                            CType(e.Row.Cells(USER_SECURITY_GRID_COL_SP_CLAIM_VALID_TO_IDX).FindControl(SP_CLAIM_EXPIRATIONDATE_LABEL), Label).Text = GetDateFormattedStringNullable(dvRow(SpUserClaims.COL_NAME_EXPIRATION_DATE))
                        End If
                        'Else
                        '    CType(e.Row.Cells(Me.USER_SECURITY_GRID_COL_SP_CLAIM_VALID_FROM_IDX).FindControl(Me.SP_CLAIM_EFFECTIVEDATE_LABEL), Label).Text = String.Empty
                        '    CType(e.Row.Cells(Me.USER_SECURITY_GRID_COL_SP_CLAIM_VALID_TO_IDX).FindControl(Me.SP_CLAIM_EXPIRATIONDATE_LABEL), Label).Text = String.Empty
                        'End If
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Protected Sub GridViewUserSecurity_ItemCommand(source As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridViewUserSecurity.RowCommand
            Dim oGridViewrow As GridViewRow
            Dim effectiveDateImageButton As ImageButton
            Dim effectiveDateText As TextBox
            Try
                If e.CommandName = EDIT_ACTION Then
                    Dim index As Integer = CInt(e.CommandArgument)
                    GridViewUserSecurity.EditIndex = index
                    GridViewUserSecurity.SelectedIndex = index
                    SetUserSecurityButtons(False)
                    State.IsEditing = True
                    State.EnabledUserSecurityEditButton = False
                    RefreshSpUserClaims()

                    'Date Calendars
                    oGridViewrow = GridViewUserSecurity.Rows(index)
                    effectiveDateImageButton = CType(oGridViewrow.FindControl(SP_CLAIM_EXPIRATIONDATE_IMAGEBUTTON_NAME), ImageButton)
                    effectiveDateText = CType(oGridViewrow.FindControl(SP_CLAIM_EXPIRATIONDATE_TEXT), TextBox)
                    AddCalendar_New(effectiveDateImageButton, effectiveDateText)

                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "User Security - SP Claim Related Events"
        Protected Sub SpClaimCodeDropDown_SelectedIndexChanged(sender As Object, e As EventArgs) Handles SpClaimCodeDropDown.SelectedIndexChanged
            If (SpClaimCodeDropDown.SelectedItem.Value = "") Then
                HideControlsForSpClaims()
            ElseIf (SpClaimCodeDropDown.SelectedItem.Value = SP_CLAIM_X509_CERTIFICATE_CODE) Then
                CertificateFile.Visible = True
                SpClaimValue.Visible = False
                SpClaimEffectiveDate.Visible = False
                SpClaimExpirationDate.Visible = False
                SpClaimCountry.Visible = False
                SpClaimDealer.Visible = False
            ElseIf (SpClaimCodeDropDown.SelectedItem.Value = SP_CLAIM_CLIENT_IP_ADDRESS) Then
                CertificateFile.Visible = False
                SpClaimValue.Visible = True
                SpClaimEffectiveDate.Visible = True
                SpClaimExpirationDate.Visible = True
                SpClaimCountry.Visible = False
                SpClaimDealer.Visible = False
                SpClaimValueLabel.Text = TranslationBase.TranslateLabelOrMessage(SpClaimCodeDropDown.SelectedItem.Value)
            ElseIf (SpClaimCodeDropDown.SelectedItem.Value = SP_CLAIM_DEALER) Then
                SpClaimDealer.Visible = True
                CertificateFile.Visible = False
                SpClaimValue.Visible = False
                SpClaimEffectiveDate.Visible = True
                SpClaimExpirationDate.Visible = True
                SpClaimCountry.Visible = False
            Else
                CertificateFile.Visible = False
                SpClaimValue.Visible = True
                SpClaimEffectiveDate.Visible = True
                SpClaimExpirationDate.Visible = True
                SpClaimCountry.Visible = False
                SpClaimDealer.Visible = False
                SpClaimValueLabel.Text = TranslationBase.TranslateLabelOrMessage(SpClaimCodeDropDown.SelectedItem.Value)

                If (SpClaimCodeDropDown.SelectedItem.Value = SP_CLAIM_SERVICE_CENTER) Then
                    SpClaimCountry.Visible = True
                End If

            End If
        End Sub

        Protected Sub AddSpClaimButton_Click(sender As Object, e As EventArgs) Handles AddSpClaimButton.Click
            Try
                If SpClaimCodeDropDown.SelectedItem Is Nothing Then Exit Sub

                Dim IsSPClaimValid As Boolean = False
                Dim EffectiveDateresult As Date
                Dim ExpirationDateresult As Date

                Dim dr As DataRow
                dr = State.SpUserClaimDv.Table.NewRow()

                Dim oSpClaimType As New SpClaimTypes(SpClaimCodeDropDown.SelectedItem.Value.ToString())

                If (SpClaimCodeDropDown.SelectedItem.Value = SP_CLAIM_X509_CERTIFICATE_CODE) Then
                    If (CertificateFileUpload.Value Is Nothing) OrElse
                        (CertificateFileUpload.PostedFile.ContentLength = 0) Then
                        ' OrElse (Me.CertificateFileUpload.PostedFile.ContentType <> "application/x-x509-ca-cert") Then
                        Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_CERTIFICATE_FILE_ERR, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_CERTIFICATE_FILE_ERR)
                    End If
                    Try

                        Dim x509 As New X509Certificate2()

                        'Create X509Certificate2 object from .cer file.
                        Dim reader As BinaryReader = New BinaryReader(CertificateFileUpload.PostedFile.InputStream)
                        Dim rawData() As Byte = reader.ReadBytes(CertificateFileUpload.PostedFile.ContentLength)
                        x509.Import(rawData)

                        dr(SpUserClaims.COL_NAME_SP_CLAIM_VALUE) = x509.Thumbprint
                        dr(SpUserClaims.COL_NAME_EFFECTIVE_DATE) = GetDateFormattedStringNullable(CType(x509.GetEffectiveDateString(), Date))
                        dr(SpUserClaims.COL_NAME_EXPIRATION_DATE) = GetDateFormattedStringNullable(CType(x509.GetExpirationDateString(), Date))

                    Catch ex As Exception
                        Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_CERTIFICATE_FILE_ERR, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_CERTIFICATE_FILE_ERR)
                    End Try

                ElseIf (SpClaimCodeDropDown.SelectedItem.Value = SP_CLAIM_CLIENT_IP_ADDRESS) Then
                    Dim ValidIPaddress As IPAddress
                    If IPAddress.TryParse(SpClaimValueTextBox.Text, ValidIPaddress) Then
                        dr(SpUserClaims.COL_NAME_SP_CLAIM_VALUE) = SpClaimValueTextBox.Text
                    Else
                        Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_IP_ADDRESS_ERR, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_IP_ADDRESS_ERR)
                    End If                    

                ElseIf (SpClaimCodeDropDown.SelectedItem.Value = SP_CLAIM_DEALER) Then
                    dr(SpUserClaims.COL_NAME_SP_CLAIM_VALUE) = txtDealerCode.Text.ToUpper()                   

                Else
                    If (SpClaimCodeDropDown.SelectedItem.Value = SP_CLAIM_SERVICE_CENTER) Then
                        dr(SpUserClaims.COL_NAME_SP_CLAIM_VALUE) = SpClaimCountryTextBox.Text.ToUpper() + "#" + SpClaimValueTextBox.Text.ToUpper()
                    Else
                        dr(SpUserClaims.COL_NAME_SP_CLAIM_VALUE) = SpClaimValueTextBox.Text.ToUpper()
                    End If
                   
                    ''''check for newly added claims whether user is allowed to access
                    'If (SpClaimValueTextBox.Text <> String.Empty AndAlso Not ValidateSpClaimValuesForUser()) Then
                    '    Throw New GUIException(ElitaPlus.Common.ErrorCodes.GUI_INVALID_SELECTION, ElitaPlus.Common.ErrorCodes.GUI_INVALID_SELECTION)
                    'End If
                End If

                ''''Date Validations
                If (SpClaimCodeDropDown.SelectedItem.Value <> SP_CLAIM_X509_CERTIFICATE_CODE) Then
                    If Date.TryParse(ValidFromText.Text, EffectiveDateresult) Then
                        dr(SpUserClaims.COL_NAME_EFFECTIVE_DATE) = GetDateFormattedStringNullable(EffectiveDateresult)
                    Else
                        Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.GUI_VALID_FROM_DATE_REQUIRED_ERR, Assurant.ElitaPlus.Common.ErrorCodes.GUI_VALID_FROM_DATE_REQUIRED_ERR)
                    End If

                    If Date.TryParse(ValidToText.Text, ExpirationDateresult) Then
                        dr(SpUserClaims.COL_NAME_EXPIRATION_DATE) = GetDateFormattedStringNullable(ExpirationDateresult)
                    Else
                        Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.GUI_VALID_TO_DATE_REQUIRED_ERR, Assurant.ElitaPlus.Common.ErrorCodes.GUI_VALID_TO_DATE_REQUIRED_ERR)
                    End If

                    If EffectiveDateresult > ExpirationDateresult Then
                        Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.GUI_VALID_FROM_DATE_GREATER_THAN_ERR, Assurant.ElitaPlus.Common.ErrorCodes.GUI_VALID_FROM_DATE_GREATER_THAN_ERR)
                    End If

                    ''check for Dates overlapping
                    IsSPClaimValid = ValidateSpClaim(oSpClaimType.Id.ToString(), dr(SpUserClaims.COL_NAME_SP_CLAIM_VALUE).ToString(), dr(SpUserClaims.COL_NAME_EFFECTIVE_DATE), dr(SpUserClaims.COL_NAME_EXPIRATION_DATE))
                    If Not IsSPClaimValid Then
                        Throw New GUIException(ElitaPlus.Common.ErrorCodes.GUI_INVALID_OVERLAPPING_SP_CLAIM, ElitaPlus.Common.ErrorCodes.GUI_INVALID_OVERLAPPING_SP_CLAIM)
                    End If
                End If
                

                dr(SpUserClaims.COL_NAME_SP_CLAIM_TYPE_ID) = oSpClaimType.Id.ToByteArray()
                dr(SpUserClaims.COL_NAME_SP_CLAIM_CODE_DESCRIPTION) = SpClaimCodeDropDown.SelectedItem.Text
                dr(SpUserClaims.COL_NAME_USER_ID) = State.MyBO.Id.ToByteArray()

                State.SpUserClaimDv.Table.Rows.Add(dr)
                State.EnabledUserSecurityEditButton = False
                State.UserSecurityDataChanged = True
                RefreshSpUserClaims()
                ClearSpClaimForm()

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Function ValidateSpClaim(NewSpClaimTypeId As String, NewSpClaimValue As String, NewClaimEffectiveDate As Date, NewClaimExpirationDate As Date, Optional ByVal isNewSpClaim As Boolean = True) As Boolean

            Dim SPClaimTypeId As String
            Dim SPClaimValue As String
            Dim EffectiveDate As Date
            Dim ExpirationDate As Date
            Dim SPClaimDV As DataView = State.SpUserClaimDv.Table.DefaultView
            Dim dr As DataRow

            For Each dr In SPClaimDV.Table.Rows
                Try
                    SPClaimTypeId = GetGuidStringFromByteArray(CType(dr(SpUserClaims.COL_NAME_SP_CLAIM_TYPE_ID), Byte()))
                    SPClaimValue = CType(dr(SpUserClaims.COL_NAME_SP_CLAIM_VALUE), String)
                    EffectiveDate = dr(SpUserClaims.COL_NAME_EFFECTIVE_DATE)
                    ExpirationDate = dr(SpUserClaims.COL_NAME_EXPIRATION_DATE)

                    If NewSpClaimTypeId.Equals(SPClaimTypeId) AndAlso NewSpClaimValue.Equals(SPClaimValue) Then
                        If NewClaimEffectiveDate = EffectiveDate AndAlso isNewSpClaim Then
                            Return False
                        End If
                        If NewClaimEffectiveDate < EffectiveDate AndAlso NewClaimExpirationDate >= EffectiveDate Then 'AndAlso NewClaimExpirationDate.Equals(ExpirationDate) Then
                            Return False
                        End If
                        If NewClaimEffectiveDate > EffectiveDate AndAlso NewClaimEffectiveDate <= ExpirationDate Then 'AndAlso NewClaimExpirationDate.Equals(ExpirationDate) Then
                            Return False
                        End If
                    End If

                Catch ex As Exception
                    HandleErrors(ex, MasterPage.MessageController)
                End Try
            Next
            Return True
        End Function
        Protected Sub ClearButton_Click(sender As Object, e As EventArgs) Handles ClearButton.Click
            Try
                ClearSpClaimForm()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "User Security - Others"
        Private Sub ClearSpClaimForm()
            SpClaimValueTextBox.Text = String.Empty
            ValidFromText.Text = String.Empty
            ValidToText.Text = String.Empty
            SpClaimCountryTextBox.Text = String.Empty
            txtDealerCode.Text = String.Empty
        End Sub
        Private Sub LoadSpUserClaims()
            State.SpUserClaimDv = State.MyBO.GetSpUserClaims(State.MyBO.Id, State.MyBO.LanguageId, SP_CLAIM_CODE)
            State.SpUserClaimDv.Table.DefaultView.Sort = SpUserClaims.COL_NAME_SP_CLAIM_TYPE_ID & " ASC," & SpUserClaims.COL_NAME_SP_CLAIM_VALUE & " ASC," & SpUserClaims.COL_NAME_EFFECTIVE_DATE & " DESC," & SpUserClaims.COL_NAME_EXPIRATION_DATE & " DESC"
            RefreshSpUserClaims()
        End Sub
        Private Sub RefreshSpUserClaims()
            If State.EnabledUserSecurityEditButton = True Then
                GridViewUserSecurity.Columns(USER_SECURITY_GRID_COL_EDIT_BUTTON_IDX).Visible = True
            Else
                GridViewUserSecurity.Columns(USER_SECURITY_GRID_COL_EDIT_BUTTON_IDX).Visible = False
            End If
            GridViewUserSecurity.DataSource = State.SpUserClaimDv.Table.DefaultView
            GridViewUserSecurity.DataBind()
        End Sub
        Private Sub SetUserSecurityButtons(enable As Boolean)
            SetButtons(enable)

            Dim actionEnabled As Boolean = enable And Not State.ViewOnly
            Dim actionNotEnabled As Boolean = Not enable And Not State.ViewOnly

            ControlMgr.SetEnableControl(Me, ClearButton, actionEnabled)
            ControlMgr.SetEnableControl(Me, AddSpClaimButton, True)

            ControlMgr.SetEnableControl(Me, UpdateSpClaimButton, actionNotEnabled)
            ControlMgr.SetEnableControl(Me, CancelUpdateSpClaimButton, actionNotEnabled)
        End Sub
        Protected Sub PopulateSpClaimCodeDropdowns()
            'Me.SpClaimCodeDropDown.PopulateOld("SP_CLAIM_CODE", ListValueType.Description, ListValueType.Code, PopulateBehavior.AddBlankListItem, String.Empty, ListValueType.Description)
            SpClaimCodeDropDown.Populate(CommonConfigManager.Current.ListManager.GetList("SP_CLAIM_CODE", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
               {
                 .ValueFunc = AddressOf .GetCode,
                 .AddBlankItem = True,
                 .BlankItemValue = String.Empty
               })
        End Sub
        Private Sub UserSecurityBoAction(LastAction As ElitaPlusPage.DetailPageCommand)
            Try
                With State
                    Select Case LastAction
                        Case ElitaPlusPage.DetailPageCommand.OK
                            .SpUserClaim.Save()
                        Case ElitaPlusPage.DetailPageCommand.Save
                            .SpUserClaim.Touch()
                            .SpUserClaim.Save()
                        Case ElitaPlusPage.DetailPageCommand.Cancel
                            .SpUserClaim.cancelEdit()
                            If .SpUserClaim.IsSaveNew Then
                                .SpUserClaim.Delete()
                                .SpUserClaim.Save()
                            End If
                        Case ElitaPlusPage.DetailPageCommand.Delete
                            .SpUserClaim.Delete()
                            .SpUserClaim.Save()
                    End Select
                End With
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub AddSecurityPermissionClaim()
            Dim dr As DataRow
            Dim oCurrentSpClaimsId As Guid
            For Each dr In State.SpUserClaimDv.Table.Rows
                Try
                    If dr(SpUserClaims.COL_NAME_SP_USER_CLAIMS_ID).ToString = String.Empty Then
                        oCurrentSpClaimsId = Guid.Empty
                    Else
                        oCurrentSpClaimsId = New Guid(CType(dr(SpUserClaims.COL_NAME_SP_USER_CLAIMS_ID), Byte()))
                    End If

                    If oCurrentSpClaimsId.Equals(Guid.Empty) Then
                        Dim oCurrentSpClaimTypeId As Guid = New Guid(CType(dr(SpUserClaims.COL_NAME_SP_CLAIM_TYPE_ID), Byte()))
                        State.SpUserClaim = State.MyBO.AddSecurityClaim(oCurrentSpClaimTypeId, CType(dr(SpUserClaims.COL_NAME_SP_CLAIM_VALUE), String), CType(dr(SpUserClaims.COL_NAME_EFFECTIVE_DATE), DateTime), CType(dr(SpUserClaims.COL_NAME_EXPIRATION_DATE), DateTime))
                        UserSecurityBoAction(ElitaPlusPage.DetailPageCommand.OK)
                    Else
                        State.SpUserClaim = State.MyBO.GetSecurityClaim(oCurrentSpClaimsId)
                        If CType(dr(SpUserClaims.COL_NAME_EXPIRATION_DATE), DateTime) <> State.SpUserClaim.ExpirationDate Then
                            State.SpUserClaim.ExpirationDate = CType(dr(SpUserClaims.COL_NAME_EXPIRATION_DATE), DateTime)
                            UserSecurityBoAction(ElitaPlusPage.DetailPageCommand.Save)
                        End If
                    End If
                Catch ex As Exception
                    HandleErrors(ex, MasterPage.MessageController)
                End Try
            Next
        End Sub
        Private Sub CloneSecurityPermissionClaim()
            Dim dr As DataRow
            For Each dr In State.SpUserClaimDv.Table.Rows
                Try
                    dr(SpUserClaims.COL_NAME_SP_USER_CLAIMS_ID) = Guid.Empty.ToByteArray()
                Catch ex As Exception
                    HandleErrors(ex, MasterPage.MessageController)
                End Try
            Next
            State.UserSecurityDataChanged = True
            State.EnabledUserSecurityEditButton = False

        End Sub
        Private Sub ExpirySecurityPermissionClaim()
            Dim dr As DataRow
            For Each dr In State.SpUserClaimDv.Table.Rows
                Try
                    If dr(SpUserClaims.COL_NAME_SP_USER_CLAIMS_ID).ToString <> String.Empty Then
                        Dim oCurrentSpClaimsId As Guid = New Guid(CType(dr(SpUserClaims.COL_NAME_SP_USER_CLAIMS_ID), Byte()))
                        State.SpUserClaim = State.MyBO.GetSecurityClaim(oCurrentSpClaimsId)
                        UserSecurityBoAction(ElitaPlusPage.DetailPageCommand.Delete)
                    End If
                Catch ex As Exception
                    HandleErrors(ex, MasterPage.MessageController)
                End Try
            Next
        End Sub
#End Region
#End Region

#Region "Validations"

        Private Function ValidateSpClaimValuesForUser() As Boolean

            Dim spClaimType As String = SpClaimCodeDropDown.SelectedItem.Value.ToUpper()
            Dim spClaimValue As String = SpClaimValueTextBox.Text.ToUpper()

            If (Not String.IsNullOrWhiteSpace(spClaimType) AndAlso Not String.IsNullOrWhiteSpace(spClaimValue)) Then
                If (spClaimType.ToUpper() = SP_CLAIM_DEALER) Then
                    Return isDealerCodeValid(spClaimValue)
                ElseIf (spClaimType.ToUpper() = SP_CLAIM_DEALER_GROUP) Then
                    Return isDealerGroupValid(spClaimValue)
                ElseIf (spClaimType.ToUpper() = SP_CLAIM_COMPANY_GROUP) Then
                    Return isCompanyGroupValid(spClaimValue)
                ElseIf (spClaimType.ToUpper() = SP_CLAIM_SERVICE_CENTER) Then
                    Return isServiceCenterCodeValid(spClaimValue, SpClaimCountryTextBox.Text.ToUpper())
                End If
            End If
        End Function
        Private Function isDealerCodeValid(dealerCode As String) As Boolean
            Try
                Return BusinessObjectsNew.User.IsDealerValidForUserClaim(State.MyBO.Id, dealerCode)
            Catch ex As Exception

            End Try
        End Function

        Private Function isDealerGroupValid(dealerGroupCode As String) As Boolean
            Return BusinessObjectsNew.User.IsDealerGroupValidForUserClaim(State.MyBO.Id, dealerGroupCode)
        End Function

        Private Function isCompanyGroupValid(companyGroupCode As String) As Boolean
            Return BusinessObjectsNew.User.IsCompanyGroupValidForUserClaim(State.MyBO.Id, companyGroupCode)
        End Function

        Private Function isServiceCenterCodeValid(serviceCenterCode As String, countryCode As String) As Boolean
            Return BusinessObjectsNew.User.IsServiceCenterValidForUserClaim(State.MyBO.Id, serviceCenterCode, countryCode)
        End Function



#End Region

    End Class
End Namespace
