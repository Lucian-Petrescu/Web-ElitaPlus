Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common


Partial Class ChangeCompany
    Inherits ElitaPlusPage
    Protected WithEvents moErrorController As ErrorController
    Protected WithEvents dlstChangeCompany As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblCompanyTitle As System.Web.UI.WebControls.Label
    Protected WithEvents lblChangeMyCompany As System.Web.UI.WebControls.Label
    Protected WithEvents moGroupCompanyMultipleDrop As MultipleColumnDDLabelControl
    Protected WithEvents UserControlAvailableSelectedCompanies As Assurant.ElitaPlus.ElitaPlusWebApp.Generic.UserControlAvailableSelected

#Region " Web Form Designer Generated Code "

    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
        InitializeComponent()
    End Sub

#End Region

#Region "Page State"


    Class MyState
        Public MyBO As User
        Public MyCompanyChildBO As UserCompany
        Public MyCompanyId As Guid = Guid.Empty
        Public MyCompanyGroupId As Guid = Guid.Empty
        Public MyCurrentSelectedList As New DataView
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

#End Region

#Region "CONSTANTS"

    Private Const NOTHING_SELECTED As String = "00000000-0000-0000-0000-000000000000"
    Private Const COL_DESCRIPTION_NAME As String = "description"
    Private Const COL_ID_NAME As String = "id"
    Public Const AVAILABLE_COMPANIES As String = "AVAILABLE_COMPANIES"
    Public Const SELECTED_COMPANIES As String = "SELECTED_COMPANIES"
    Private Const LABEL_COMPANY_GROUP As String = "COMPANY_GROUP"

#End Region

#Region "Properties"

    Public ReadOnly Property GroupCompanyMultipleDrop() As MultipleColumnDDLabelControl
        Get
            If moGroupCompanyMultipleDrop Is Nothing Then
                moGroupCompanyMultipleDrop = CType(FindControl("moGroupCompanyMultipleDrop"), MultipleColumnDDLabelControl)
            End If
            Return moGroupCompanyMultipleDrop
        End Get
    End Property

#End Region

#Region "Handlers-DropDown"

    Private Sub OnFromDrop_Changed(fromMultipleDrop As MultipleColumnDDLabelControl) _
                    Handles moGroupCompanyMultipleDrop.SelectedDropChanged
        Try
            PopulateUserControlAvailableSelectedCompanies()
        Catch ex As Exception
            HandleErrors(ex, moErrorController)
        End Try
    End Sub

#End Region

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        moErrorController.Clear_Hide()
        ClearLabelsErrSign()
        Try
            If Not Page.IsPostBack Then
                State.MyBO = ElitaPlusIdentity.Current.ActiveUser

                PopulateDropdown()
                PopulateUserControlAvailableSelectedCompanies()
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, moErrorController)
        End Try
        ShowMissingTranslations(moErrorController)

    End Sub

    Public Sub ClearLabelsErrSign()
        Try
            ClearLabelErrSign(GroupCompanyMultipleDrop.CaptionLabel)
        Catch ex As Exception
            HandleErrors(ex, moErrorController)
        End Try
    End Sub

    Sub PopulateDropdown()
        GroupCompanyMultipleDrop.NothingSelected = True
        Dim dv As DataView = LookupListNew.GetCompanyGroupLookupList()
        If Not ElitaPlusIdentity.Current.ActiveUser.IsIHQRole Then
            'filter out the company group by user company assigned
            Dim dvAvailCompanyGrp As DataView = BusinessObjectsNew.User.GetAvailableCompanyGroup(State.MyBO.Id)
            Dim blnAssigned As Boolean, guidTemp1 As Guid, guidTemp2 As Guid
            For i As Integer = (dv.Count - 1) To 0 Step -1
                blnAssigned = False
                guidTemp1 = New Guid(CType(dv(i)("id"), Byte()))
                For j As Integer = 0 To dvAvailCompanyGrp.Count - 1
                    guidTemp2 = New Guid(CType(dvAvailCompanyGrp(j)("company_group_id"), Byte()))
                    If guidTemp1.Equals(guidTemp2) Then
                        blnAssigned = True
                        Exit For
                    End If
                Next
                If Not blnAssigned Then
                    dv.Delete(i)
                End If
            Next
        End If
        GroupCompanyMultipleDrop.SetControl(True, GroupCompanyMultipleDrop.MODES.NEW_MODE, True, dv, TranslationBase.TranslateLabelOrMessage(LABEL_COMPANY_GROUP), True)
        GroupCompanyMultipleDrop.SelectedGuid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
    End Sub

    Private Sub btnSave_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            UpdateUserCompany()
        Catch ex As Exception
            HandleErrors(ex, moErrorController)
        End Try
    End Sub

    Sub UpdateUserCompany()
        Dim nNewCompanyID As Guid
        Dim nNewCompanyIDs As New ArrayList



        Dim userCompIdStr As String
        For Each userCompIdStr In UserControlAvailableSelectedCompanies.SelectedList
            nNewCompanyIDs.Add(New Guid(userCompIdStr))
        Next

        If GroupCompanyMultipleDrop.SelectedGuid.Equals(Guid.Empty) Then
            ElitaPlusPage.SetLabelError(GroupCompanyMultipleDrop.CaptionLabel)
            Throw New GUIException(Message.MSG_COMPANY_GROUP_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_GROUP_MUST_BE_SELECTED_ERR)
        End If

        If UserControlAvailableSelectedCompanies.SelectedList.Count = 0 Then
            'ElitaPlusPage.SetLabelError(UserControlAvailableSelectedCompanies.SelectedTitleLabel)
            Throw New GUIException(Message.MSG_COMPANY_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_REQUIRED)
        End If
        State.MyBO.UpdateUserCompanies(nNewCompanyIDs)
        State.MyBO.AccountingCompaniesClearCache()

        ReloadNavigationMenu()

    End Sub

    Sub ReloadNavigationMenu()

        ReloadHeader()

    End Sub

    Sub PopulateUserControlAvailableSelectedCompanies()
        Dim oCompanyGroupID As Guid = GroupCompanyMultipleDrop.SelectedGuid
        Dim availableDv As DataView = State.MyBO.GetAvailableCompanies(oCompanyGroupID, State.MyBO.Id)
        Dim selectedDv As DataView = State.MyBO.GetSelectedCompanies(oCompanyGroupID, State.MyBO.Id)
        UserControlAvailableSelectedCompanies.SelectedList.Clear()
        UserControlAvailableSelectedCompanies.AvailableList.Clear()
        UserControlAvailableSelectedCompanies.SetSelectedData(selectedDv, COL_DESCRIPTION_NAME, COL_ID_NAME)
        UserControlAvailableSelectedCompanies.SetAvailableData(availableDv, COL_DESCRIPTION_NAME, COL_ID_NAME)
        UserControlAvailableSelectedCompanies.AvailableDesc = TranslationBase.TranslateLabelOrMessage(AVAILABLE_COMPANIES)
        UserControlAvailableSelectedCompanies.SelectedDesc = TranslationBase.TranslateLabelOrMessage(SELECTED_COMPANIES)
    End Sub

End Class
