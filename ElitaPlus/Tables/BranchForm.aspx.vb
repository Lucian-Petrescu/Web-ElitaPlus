Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.Security
Partial Class BranchForm
    Inherits ElitaPlusPage

#Region "Page State"

#Region "MyState"

    Class MyState

        Public moIsNewBranchLabel As Guid = Guid.Empty
        Public IsNewBranchNew As Boolean = False
        Public IsNewWithCopy As Boolean = False
        Public IsUndo As Boolean = False
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public LastOperation As DetailPageCommand = DetailPageCommand.Nothing_
        Public boChanged As Boolean = False
        Public LastErrMsg As String
        Public HasDataChanged As Boolean
        Public MyBO As Branch
        Public ScreenSnapShotBO As Branch


    End Class
#End Region

    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            Return CType(MyBase.State, MyState)
        End Get
    End Property

    Private Sub SetStateProperties()
        State.moIsNewBranchLabel = CType(CallingParameters, Guid)
        If State.moIsNewBranchLabel.Equals(Guid.Empty) Then
            State.IsNewBranchNew = True
            BindBoPropertiesToLabels()
            AddLabelDecorations(TheBranch)
            ClearAll()
            PopulateAll()
        Else
            State.IsNewBranchNew = False
            BindBoPropertiesToLabels()
            AddLabelDecorations(TheBranch)
            PopulateAll()
        End If
    End Sub

#End Region

#Region "Constants"

    Public Shared URL As String = "BranchForm.aspx"
    Public Const DEALERID_PROPERTY As String = "DealerId"
    Public Const BRANCHCODE_PROPERTY As String = "BranchCode"
    Public Const BRANCHNAME_PROPERTY As String = "BranchName"
    Public Const CONTACTPHONE_PROPERTY As String = "ContactPhone"
    Public Const CONTACTEXT_PROPERTY As String = "ContactExt"
    Public Const CONTACTFAX_PROPERTY As String = "ContactFax"
    Public Const CONTACTEMAIL_PROPERTY As String = "ContactEmail"
    Public Const MARKET_PROPERTY As String = "Market"
    Public Const AddressInfoStartIndex As Int16 = 4
    Public Const BRANCH_DIRTY_COLUMNS_COUNT As Integer = 1
    Private Const LABEL_SELECT_DEALERCODE As String = "DEALER"
    Private Const LABEL_DEALER As String = "DEALER_NAME"
    Private Const BRANCH_LIST_FORM001 As String = "BRANCH_LIST_FORM001" ' Maintain Branch List Exception

#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public moBranchId As Guid
        Public HasDataChanged As Boolean
        Public Sub New(LastOp As DetailPageCommand, oBranchId As Guid, hasDataChanged As Boolean)
            LastOperation = LastOp
            moBranchId = oBranchId
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class

#End Region

#Region "Properties"
    Private ReadOnly Property TheBranch As Branch

        Get
            If State.MyBO Is Nothing Then
                If State.IsNewBranchNew = True Then
                    ' For creating, inserting
                    State.MyBO = New Branch
                    State.moIsNewBranchLabel = State.MyBO.Id
                Else
                    ' For updating, deleting
                    State.MyBO = New Branch(State.moIsNewBranchLabel)
                End If
            End If

            Return State.MyBO
        End Get
    End Property

    Public ReadOnly Property AddressCtr() As UserControlAddress_New
        Get
            Return moAddressController
        End Get
    End Property

    Public ReadOnly Property TheDealerControl() As MultipleColumnDDLabelControl_New
        Get
            If multipleDropControl Is Nothing Then
                multipleDropControl = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl_New)
            End If
            Return multipleDropControl
        End Get
    End Property

#End Region

#Region "Handlers"

#Region "Handlers-Init"
    Protected WithEvents moErrorController As ErrorController

    Protected WithEvents moPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents EditPanel_WRITE As System.Web.UI.WebControls.Panel
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button
    Protected WithEvents moAddressController As UserControlAddress_New
    Protected WithEvents moDealerMultipleDrop As MultipleColumnDDLabelControl

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

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'Put user code to initialize the page here
        Try
            MasterPage.MessageController.Clear_Hide()
            ClearLabelsErrSign()
            If Not Page.IsPostBack Then
                MasterPage.MessageController.Clear()
                MasterPage.UsePageTabTitleInBreadCrum = False
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")
                UpdateBreadCrum()
                SetStateProperties()
                AddControlMsg(btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO,
                MSG_TYPE_CONFIRM, True)

                If State.IsNewBranchNew = True Then
                    CreateNew()
                End If
                PopulateFormFromBOs()
                moAddressController.ReAssignTabIndex(AddressInfoStartIndex)
                AddressCtr.EnableControls(False, True)
                AddCalendar(BtnOpenDate, txtOpendate)
                AddCalendar(BtnCloseDate, txtClosedate)
            End If

            BindBoPropertiesToLabels()
            CheckIfComingFromConfirm()
            If Not IsPostBack Then
                AddLabelDecorations(TheBranch)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)

        End Try
        If Me.State.LastOperation = DetailPageCommand.Redirect_ Then
            MasterPage.MessageController.Clear_Hide()
            'ClearLabelsErrSign()
            State.LastOperation = DetailPageCommand.Nothing_
        Else
            ShowMissingTranslations(MasterPage.MessageController)
        End If
    End Sub

    Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall

        Try
            If CallingParameters IsNot Nothing Then
                'Get the id from the parent
                State.MyBO = New Branch(CType(CallingParameters, Guid))
            Else
                State.IsNewBranchNew = True
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

#End Region

#Region "Handlers-Buttons"

    Private Sub btnApply_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnApply_WRITE.Click
        ApplyChanges()
        AddressCtr.EnableControls(False, True)
    End Sub

    Private Sub GoBack()
        Dim retType As New BranchForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back,
                                                                State.moIsNewBranchLabel, State.boChanged)
        ReturnToCallingPage(retType)
    End Sub

    Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
        Try
            If IsDirtyBO() = True Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM,
                                                HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                GoBack()
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnUndo_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_WRITE.Click
        Try
            If Not State.IsNewBranchNew Then
                'Reload from the DB
                State.MyBO = New Branch(State.MyBO.Id)
            ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                'It was a new with copy
                State.MyBO.Clone(State.ScreenSnapShotBO)
            Else
                CreateNew()
            End If
            PopulateAll()
            PopulateFormFromBOs()
            SetButtonsState(State.IsNewBranchNew)
            AddressCtr.EnableControls(False, True)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub CreateNew()
        State.ScreenSnapShotBO = Nothing
        State.IsNewBranchNew = True
        State.MyBO = New Branch
        ClearAll()
        SetButtonsState(True)
        PopulateAll()
        PopulateBOsFromForm()
        PopulateFormFromBOs()
        TheDealerControl.ChangeEnabledControlProperty(True)
        'SetOpenDate(True)
    End Sub

    Private Sub btnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            If IsDirtyBO() = True Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                CreateNew()
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub CreateNewCopy()

        PopulateBOsFromForm()

        Dim newObj As New Branch
        newObj.Copy(TheBranch)

        State.MyBO = newObj
        State.moIsNewBranchLabel = Guid.Empty
        State.IsNewBranchNew = True
        AddressCtr.EnableControls(False, True)

        With TheBranch
            .BranchCode = Nothing
            .BranchName = Nothing
        End With

        SetButtonsState(True)
        PopulateFormFromBOs()
        TheDealerControl.ChangeEnabledControlProperty(True)
        'SetOpenDate(True)

        'create the backup copy
        State.ScreenSnapShotBO = New Branch
        State.ScreenSnapShotBO.Copy(TheBranch)

    End Sub

    Private Sub btnCopy_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnCopy_WRITE.Click
        Try
            If IsDirtyBO() = True Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            Else
                CreateNewCopy()
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
            DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
        End Try
    End Sub

    Private Sub btnDelete_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete_WRITE.Click
        Try
            State.MyBO.DeleteAndSave()
            State.HasDataChanged = True
            ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, State.moIsNewBranchLabel, State.HasDataChanged))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Clear"

    Private Sub ClearTexts()
        txtBranchCode.Text = Nothing
        txtBranchName.Text = Nothing
        txtStoreManager.Text = Nothing
        txtContactPhone.Text = Nothing
        txtContactExt.Text = Nothing
        txtContactFax.Text = Nothing
        txtContactEmail.Text = Nothing
        txtMarket.Text = Nothing
        txtMarketingRegion.Text = Nothing
        txtOpendate.Text = Nothing
        txtClosedate.Text = Nothing
    End Sub

    Private Sub ClearAll()
        ClearTexts()
        TheDealerControl.ClearMultipleDrop()
        ClearList(ddlBranchType)
    End Sub

#End Region

#Region "Populate"

    Private Sub UpdateBreadCrum()
        If (State IsNot Nothing) Then
            If (State IsNot Nothing) Then
                MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("BRANCH_FORM")
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("BRANCH_FORM")
            End If
        End If
    End Sub

    Private Sub PopulateDealer()
        Dim oCompanyList As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
        Try
            Dim dv As DataView = LookupListNew.GetDealerLookupList(oCompanyList, True)
            TheDealerControl.SetControl(True, TheDealerControl.MODES.NEW_MODE, True, dv, "*" + TranslationBase.TranslateLabelOrMessage(LABEL_DEALER), True, True)
            If State.IsNewBranchNew = True Then
                TheDealerControl.SelectedGuid = Guid.Empty
                TheDealerControl.ChangeEnabledControlProperty(True)
                'SetOpenDate(True)
            Else
                TheDealerControl.ChangeEnabledControlProperty(False)
                If State.MyBO.OpenDate IsNot Nothing Then
                    'SetOpenDate(False)
                End If
                TheDealerControl.SelectedGuid = TheBranch.DealerId
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            MasterPage.MessageController.AddError(BRANCH_LIST_FORM001)
            MasterPage.MessageController.AddError(ex.Message, False)
            MasterPage.MessageController.Show()
        End Try
    End Sub

    Private Sub PopulateDropDowns()
        Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        Try
            Dim branchType As ListItem() = CommonConfigManager.Current.ListManager.GetList("BRTYPE", Thread.CurrentPrincipal.GetLanguageCode())
            ddlBranchType.Populate(branchType, New PopulateOptions() With
                                                {
                                                    .AddBlankItem = True
                                                })

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            MasterPage.MessageController.AddError(BRANCH_LIST_FORM001)
            MasterPage.MessageController.AddError(ex.Message, False)
            MasterPage.MessageController.Show()
        End Try
    End Sub

    Private Sub PopulateFormFromBOs()
        Try
            With TheBranch
                BindSelectItem(.BranchTypeId.ToString, ddlBranchType)
                PopulateControlFromBOProperty(txtBranchCode, .BranchCode)
                PopulateControlFromBOProperty(txtBranchName, .BranchName)
                PopulateControlFromBOProperty(txtContactEmail, .ContactEmail)
                PopulateControlFromBOProperty(txtContactExt, .ContactExt)
                PopulateControlFromBOProperty(txtContactFax, .ContactFax)
                PopulateControlFromBOProperty(txtContactPhone, .ContactPhone)
                PopulateControlFromBOProperty(txtMarket, .Market)
                PopulateControlFromBOProperty(txtMarketingRegion, .MarketingRegion)
                PopulateControlFromBOProperty(txtStoreManager, .StoreManager)
                PopulateControlFromBOProperty(txtOpendate, .OpenDate)
                PopulateControlFromBOProperty(txtClosedate, .CloseDate)
                AddressCtr.Bind(State.MyBO)
            End With
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub PopulateAll()
        PopulateDropDowns()
        PopulateDealer()
    End Sub

    Protected Sub PopulateBOsFromForm()

        With TheBranch
            .DealerId = TheDealerControl.SelectedGuid
            PopulateBOProperty(State.MyBO, "BranchCode", txtBranchCode)
            PopulateBOProperty(State.MyBO, "BranchName", txtBranchName)
            PopulateBOProperty(State.MyBO, "ContactEmail", txtContactEmail)
            PopulateBOProperty(State.MyBO, "ContactExt", txtContactExt)
            PopulateBOProperty(State.MyBO, "ContactFax", txtContactFax)
            PopulateBOProperty(State.MyBO, "ContactPhone", txtContactPhone)
            PopulateBOProperty(State.MyBO, "Market", txtMarket)
            PopulateBOProperty(State.MyBO, "MarketingRegion", txtMarketingRegion)
            PopulateBOProperty(State.MyBO, "BranchTypeId", ddlBranchType)
            PopulateBOProperty(State.MyBO, "StoreManager", txtStoreManager)
            PopulateBOProperty(State.MyBO, "OpenDate", txtOpendate)
            PopulateBOProperty(State.MyBO, "CloseDate", txtClosedate)
            AddressCtr.PopulateBOFromControl(True)

        End With
        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If

    End Sub

    Public Sub ValidateDates()

        If txtClosedate.Text.Trim() <> String.Empty AndAlso txtOpendate.Text.Trim() <> String.Empty Then
            If CDate(txtOpendate.Text) >= CDate(txtClosedate.Text) Then
                ElitaPlusPage.SetLabelError(lblOpenDate)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.BEGIN_DATE_ERR1)
            End If
        ElseIf txtClosedate.Text.Trim() <> String.Empty AndAlso txtOpendate.Text.Trim() = String.Empty Then
            ElitaPlusPage.SetLabelError(lblOpenDate)
            Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_BEGIN_DATE)
        End If
    End Sub
#End Region

#Region "Gui-Validation"

    Private Sub SetButtonsState(bIsNew As Boolean)
        ControlMgr.SetEnableControl(Me, btnNew_WRITE, Not bIsNew)
        ControlMgr.SetEnableControl(Me, btnCopy_WRITE, Not bIsNew)
        ControlMgr.SetEnableControl(Me, btnDelete_WRITE, Not bIsNew)
        TheDealerControl.ChangeEnabledControlProperty(bIsNew)
        ControlMgr.SetEnableControl(Me, lblBranchCode, bIsNew)
    End Sub

    'Private Sub SetOpenDate(ByVal bIsEnable As Boolean)
    '    ControlMgr.SetEnableControl(Me, txtOpendate, bIsEnable)
    '    ControlMgr.SetEnableControl(Me, BtnOpenDate, bIsEnable)
    'End Sub

#End Region

#Region "Business Part"

    Private Function IsDirtyBO() As Boolean
        Dim bIsDirty As Boolean = True

        Try
            With TheBranch
                PopulateBOsFromForm()
                bIsDirty = .IsDirty
            End With
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            MasterPage.MessageController.AddError(BRANCH_LIST_FORM001)
            MasterPage.MessageController.AddError(ex.Message, False)
            MasterPage.MessageController.Show()
        End Try
        Return bIsDirty
    End Function

    Private Function ApplyChanges() As Boolean

        Try

            PopulateBOsFromForm()

            'Dim errors() As ValidationError = {New ValidationError("Branch Code is required", GetType(Branch), Nothing, "BranchID", Nothing)}
            'If ((Me.State.MyBO.BranchCode = Nothing)) Then
            '    Throw New BOValidationException(errors, GetType(Branch).FullName)
            'End If

            If TheBranch.IsDirty() Then
                ValidateDates()
                State.MyBO.Save()
                State.boChanged = True
                If State.IsNewBranchNew = True Then
                    State.IsNewBranchNew = False
                End If
                PopulateAll()
                PopulateFormFromBOs()
                SetButtonsState(State.IsNewBranchNew)
                MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
            Else
                MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Function

#End Region

#Region "State-Management"

    Protected Sub ComingFromBack()
        Dim confResponse As String = HiddenSaveChangesPromptResponse.Value

        If Not confResponse = String.Empty Then
            ' Return from the Back Button

            Select Case confResponse
                Case MSG_VALUE_YES
                    ' Save and go back to Search Page
                    If ApplyChanges() = True Then
                        State.boChanged = True
                        GoBack()
                    End If
                Case MSG_VALUE_NO
                    GoBack()
            End Select
        End If

    End Sub

    Protected Sub ComingFromNewCopy()
        Dim confResponse As String = HiddenSaveChangesPromptResponse.Value

        If Not confResponse = String.Empty Then
            ' Return from the New Copy Button

            Select Case confResponse
                Case MSG_VALUE_YES
                    ' Save and create a new Copy BO
                    If ApplyChanges() = True Then
                        State.boChanged = True
                        CreateNewCopy()
                    End If
                Case MSG_VALUE_NO
                    ' create a new BO
                    CreateNewCopy()
            End Select
        End If

    End Sub
    Protected Sub ComingFromNew()
        Dim confResponse As String = HiddenSaveChangesPromptResponse.Value

        If Not confResponse = String.Empty Then
            ' Return from the New Copy Button

            Select Case confResponse
                Case MSG_VALUE_YES
                    ' Save and create a new Copy BO
                    If ApplyChanges() = True Then
                        State.boChanged = True
                        CreateNew()
                    End If
                Case MSG_VALUE_NO
                    ' create a new BO
                    CreateNew()
            End Select
        End If

    End Sub


    Protected Sub CheckIfComingFromConfirm()
        Try
            Select Case State.ActionInProgress
                    ' Period
                Case ElitaPlusPage.DetailPageCommand.Back
                    ComingFromBack()
                Case ElitaPlusPage.DetailPageCommand.New_
                    ComingFromNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    ComingFromNewCopy()
            End Select

            'Clean after consuming the action
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            HiddenSaveChangesPromptResponse.Value = String.Empty
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Handlers-Labels"

    Private Sub BindBoPropertiesToLabels()
        BindBOPropertyToLabel(State.MyBO, DEALERID_PROPERTY, TheDealerControl.CaptionLabel)
        BindBOPropertyToLabel(State.MyBO, BRANCHCODE_PROPERTY, lblBranchCode)
        BindBOPropertyToLabel(State.MyBO, BRANCHNAME_PROPERTY, lblBranchName)
        BindBOPropertyToLabel(State.MyBO, CONTACTEMAIL_PROPERTY, lblContactEmail)
        BindBOPropertyToLabel(State.MyBO, CONTACTEXT_PROPERTY, lblContactExt)
        BindBOPropertyToLabel(State.MyBO, CONTACTFAX_PROPERTY, lblContactFax)
        BindBOPropertyToLabel(State.MyBO, CONTACTPHONE_PROPERTY, lblContactPhone)
        BindBOPropertyToLabel(State.MyBO, MARKET_PROPERTY, lblMarket)
        BindBOPropertyToLabel(State.MyBO, "MarketingRegion", lblMarketingRegion)
        BindBOPropertyToLabel(State.MyBO, "BranchTypeId", lblBranchType)
        BindBOPropertyToLabel(State.MyBO, "StoreManager", lblStoreMgr)
        BindBOPropertyToLabel(State.MyBO, "OpenDate", lblOpenDate)
        BindBOPropertyToLabel(State.MyBO, "CloseDate", lblCloseDate)
    End Sub

    Private Sub ClearLabelsErrSign()
        ClearLabelErrSign(lblBranchCode)
        ClearLabelErrSign(TheDealerControl.CaptionLabel)
        ClearLabelErrSign(lblBranchName)
        ClearLabelErrSign(lblBranchType)
        ClearLabelErrSign(lblStoreMgr)
        ClearLabelErrSign(lblContactPhone)
        ClearLabelErrSign(lblContactExt)
        ClearLabelErrSign(lblContactFax)
        ClearLabelErrSign(lblContactEmail)
        ClearLabelErrSign(lblMarket)
        ClearLabelErrSign(lblMarketingRegion)
        ClearLabelErrSign(lblOpenDate)
        ClearLabelErrSign(lblCloseDate)
    End Sub

    Public Shared Sub SetLabelColor(lbl As Label)
        lbl.ForeColor = Color.Black
    End Sub

#End Region

#End Region

End Class