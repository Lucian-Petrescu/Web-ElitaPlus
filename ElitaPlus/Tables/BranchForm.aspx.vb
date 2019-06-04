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
        Me.State.moIsNewBranchLabel = CType(Me.CallingParameters, Guid)
        If Me.State.moIsNewBranchLabel.Equals(Guid.Empty) Then
            Me.State.IsNewBranchNew = True
            BindBoPropertiesToLabels()
            Me.AddLabelDecorations(TheBranch)
            ClearAll()
            PopulateAll()
        Else
            Me.State.IsNewBranchNew = False
            BindBoPropertiesToLabels()
            Me.AddLabelDecorations(TheBranch)
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
        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal oBranchId As Guid, ByVal hasDataChanged As Boolean)
            Me.LastOperation = LastOp
            Me.moBranchId = oBranchId
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class

#End Region

#Region "Properties"
    Private ReadOnly Property TheBranch As Branch

        Get
            If Me.State.MyBO Is Nothing Then
                If Me.State.IsNewBranchNew = True Then
                    ' For creating, inserting
                    Me.State.MyBO = New Branch
                    Me.State.moIsNewBranchLabel = Me.State.MyBO.Id
                Else
                    ' For updating, deleting
                    Me.State.MyBO = New Branch(Me.State.moIsNewBranchLabel)
                End If
            End If

            Return Me.State.MyBO
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

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Put user code to initialize the page here
        Try
            Me.MasterPage.MessageController.Clear_Hide()
            ClearLabelsErrSign()
            If Not Page.IsPostBack Then
                Me.MasterPage.MessageController.Clear()
                Me.MasterPage.UsePageTabTitleInBreadCrum = False
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")
                UpdateBreadCrum()
                Me.SetStateProperties()
                Me.AddControlMsg(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO,
                MSG_TYPE_CONFIRM, True)

                If Me.State.IsNewBranchNew = True Then
                    CreateNew()
                End If
                PopulateFormFromBOs()
                Me.moAddressController.ReAssignTabIndex(AddressInfoStartIndex)
                AddressCtr.EnableControls(False, True)
                Me.AddCalendar(Me.BtnOpenDate, Me.txtOpendate)
                Me.AddCalendar(Me.BtnCloseDate, Me.txtClosedate)
            End If

            BindBoPropertiesToLabels()
            CheckIfComingFromConfirm()
            If Not Me.IsPostBack Then
                Me.AddLabelDecorations(TheBranch)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)

        End Try
        If Me.State.LastOperation = DetailPageCommand.Redirect_ Then
            Me.MasterPage.MessageController.Clear_Hide()
            'ClearLabelsErrSign()
            Me.State.LastOperation = DetailPageCommand.Nothing_
        Else
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)
        End If
    End Sub

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall

        Try
            If Not Me.CallingParameters Is Nothing Then
                'Get the id from the parent
                Me.State.MyBO = New Branch(CType(Me.CallingParameters, Guid))
            Else
                Me.State.IsNewBranchNew = True
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

#End Region

#Region "Handlers-Buttons"

    Private Sub btnApply_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply_WRITE.Click
        ApplyChanges()
        AddressCtr.EnableControls(False, True)
    End Sub

    Private Sub GoBack()
        Dim retType As New BranchForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back,
                                                                Me.State.moIsNewBranchLabel, Me.State.boChanged)
        Me.ReturnToCallingPage(retType)
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            If IsDirtyBO() = True Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM,
                                                Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                GoBack()
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnUndo_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_WRITE.Click
        Try
            If Not Me.State.IsNewBranchNew Then
                'Reload from the DB
                Me.State.MyBO = New Branch(Me.State.MyBO.Id)
            ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                'It was a new with copy
                Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
            Else
                CreateNew()
            End If
            PopulateAll()
            PopulateFormFromBOs()
            Me.SetButtonsState(Me.State.IsNewBranchNew)
            AddressCtr.EnableControls(False, True)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub CreateNew()
        Me.State.ScreenSnapShotBO = Nothing
        Me.State.IsNewBranchNew = True
        Me.State.MyBO = New Branch
        ClearAll()
        Me.SetButtonsState(True)
        Me.PopulateAll()
        Me.PopulateBOsFromForm()
        PopulateFormFromBOs()
        TheDealerControl.ChangeEnabledControlProperty(True)
        'SetOpenDate(True)
    End Sub

    Private Sub btnNew_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            If IsDirtyBO() = True Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                CreateNew()
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub CreateNewCopy()

        Me.PopulateBOsFromForm()

        Dim newObj As New Branch
        newObj.Copy(TheBranch)

        Me.State.MyBO = newObj
        Me.State.moIsNewBranchLabel = Guid.Empty
        Me.State.IsNewBranchNew = True
        AddressCtr.EnableControls(False, True)

        With TheBranch
            .BranchCode = Nothing
            .BranchName = Nothing
        End With

        Me.SetButtonsState(True)
        PopulateFormFromBOs()
        TheDealerControl.ChangeEnabledControlProperty(True)
        'SetOpenDate(True)

        'create the backup copy
        Me.State.ScreenSnapShotBO = New Branch
        Me.State.ScreenSnapShotBO.Copy(TheBranch)

    End Sub

    Private Sub btnCopy_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy_WRITE.Click
        Try
            If IsDirtyBO() = True Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            Else
                CreateNewCopy()
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
            Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
        End Try
    End Sub

    Private Sub btnDelete_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click
        Try
            Me.State.MyBO.DeleteAndSave()
            Me.State.HasDataChanged = True
            Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, Me.State.moIsNewBranchLabel, Me.State.HasDataChanged))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
        If (Not Me.State Is Nothing) Then
            If (Not Me.State Is Nothing) Then
                Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("BRANCH_FORM")
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("BRANCH_FORM")
            End If
        End If
    End Sub

    Private Sub PopulateDealer()
        Dim oCompanyList As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
        Try
            Dim dv As DataView = LookupListNew.GetDealerLookupList(oCompanyList, True)
            TheDealerControl.SetControl(True, TheDealerControl.MODES.NEW_MODE, True, dv, "*" + TranslationBase.TranslateLabelOrMessage(LABEL_DEALER), True, True)
            If Me.State.IsNewBranchNew = True Then
                TheDealerControl.SelectedGuid = Guid.Empty
                TheDealerControl.ChangeEnabledControlProperty(True)
                'SetOpenDate(True)
            Else
                TheDealerControl.ChangeEnabledControlProperty(False)
                If Not State.MyBO.OpenDate Is Nothing Then
                    'SetOpenDate(False)
                End If
                TheDealerControl.SelectedGuid = TheBranch.DealerId
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.MasterPage.MessageController.AddError(BRANCH_LIST_FORM001)
            Me.MasterPage.MessageController.AddError(ex.Message, False)
            Me.MasterPage.MessageController.Show()
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
            Me.MasterPage.MessageController.AddError(BRANCH_LIST_FORM001)
            Me.MasterPage.MessageController.AddError(ex.Message, False)
            Me.MasterPage.MessageController.Show()
        End Try
    End Sub

    Private Sub PopulateFormFromBOs()
        Try
            With TheBranch
                BindSelectItem(.BranchTypeId.ToString, ddlBranchType)
                Me.PopulateControlFromBOProperty(Me.txtBranchCode, .BranchCode)
                Me.PopulateControlFromBOProperty(Me.txtBranchName, .BranchName)
                Me.PopulateControlFromBOProperty(Me.txtContactEmail, .ContactEmail)
                Me.PopulateControlFromBOProperty(Me.txtContactExt, .ContactExt)
                Me.PopulateControlFromBOProperty(Me.txtContactFax, .ContactFax)
                Me.PopulateControlFromBOProperty(Me.txtContactPhone, .ContactPhone)
                Me.PopulateControlFromBOProperty(Me.txtMarket, .Market)
                Me.PopulateControlFromBOProperty(Me.txtMarketingRegion, .MarketingRegion)
                Me.PopulateControlFromBOProperty(Me.txtStoreManager, .StoreManager)
                Me.PopulateControlFromBOProperty(Me.txtOpendate, .OpenDate)
                Me.PopulateControlFromBOProperty(Me.txtClosedate, .CloseDate)
                AddressCtr.Bind(Me.State.MyBO)
            End With
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub PopulateAll()
        PopulateDropDowns()
        PopulateDealer()
    End Sub

    Protected Sub PopulateBOsFromForm()

        With Me.TheBranch
            .DealerId = TheDealerControl.SelectedGuid
            Me.PopulateBOProperty(Me.State.MyBO, "BranchCode", Me.txtBranchCode)
            Me.PopulateBOProperty(Me.State.MyBO, "BranchName", Me.txtBranchName)
            Me.PopulateBOProperty(Me.State.MyBO, "ContactEmail", Me.txtContactEmail)
            Me.PopulateBOProperty(Me.State.MyBO, "ContactExt", Me.txtContactExt)
            Me.PopulateBOProperty(Me.State.MyBO, "ContactFax", Me.txtContactFax)
            Me.PopulateBOProperty(Me.State.MyBO, "ContactPhone", Me.txtContactPhone)
            Me.PopulateBOProperty(Me.State.MyBO, "Market", Me.txtMarket)
            Me.PopulateBOProperty(Me.State.MyBO, "MarketingRegion", Me.txtMarketingRegion)
            Me.PopulateBOProperty(Me.State.MyBO, "BranchTypeId", Me.ddlBranchType)
            Me.PopulateBOProperty(Me.State.MyBO, "StoreManager", Me.txtStoreManager)
            Me.PopulateBOProperty(Me.State.MyBO, "OpenDate", Me.txtOpendate)
            Me.PopulateBOProperty(Me.State.MyBO, "CloseDate", Me.txtClosedate)
            Me.AddressCtr.PopulateBOFromControl(True)

        End With
        If Me.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If

    End Sub

    Public Sub ValidateDates()

        If txtClosedate.Text.Trim() <> String.Empty And txtOpendate.Text.Trim() <> String.Empty Then
            If CDate(txtOpendate.Text) >= CDate(txtClosedate.Text) Then
                ElitaPlusPage.SetLabelError(lblOpenDate)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.BEGIN_DATE_ERR1)
            End If
        ElseIf txtClosedate.Text.Trim() <> String.Empty And txtOpendate.Text.Trim() = String.Empty Then
            ElitaPlusPage.SetLabelError(lblOpenDate)
            Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_BEGIN_DATE)
        End If
    End Sub
#End Region

#Region "Gui-Validation"

    Private Sub SetButtonsState(ByVal bIsNew As Boolean)
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
            Me.MasterPage.MessageController.AddError(BRANCH_LIST_FORM001)
            Me.MasterPage.MessageController.AddError(ex.Message, False)
            Me.MasterPage.MessageController.Show()
        End Try
        Return bIsDirty
    End Function

    Private Function ApplyChanges() As Boolean

        Try

            Me.PopulateBOsFromForm()

            'Dim errors() As ValidationError = {New ValidationError("Branch Code is required", GetType(Branch), Nothing, "BranchID", Nothing)}
            'If ((Me.State.MyBO.BranchCode = Nothing)) Then
            '    Throw New BOValidationException(errors, GetType(Branch).FullName)
            'End If

            If TheBranch.IsDirty() Then
                ValidateDates()
                Me.State.MyBO.Save()
                Me.State.boChanged = True
                If Me.State.IsNewBranchNew = True Then
                    Me.State.IsNewBranchNew = False
                End If
                PopulateAll()
                PopulateFormFromBOs()
                Me.SetButtonsState(Me.State.IsNewBranchNew)
                Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
            Else
                Me.MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Function

#End Region

#Region "State-Management"

    Protected Sub ComingFromBack()
        Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value

        If Not confResponse = String.Empty Then
            ' Return from the Back Button

            Select Case confResponse
                Case MSG_VALUE_YES
                    ' Save and go back to Search Page
                    If ApplyChanges() = True Then
                        Me.State.boChanged = True
                        GoBack()
                    End If
                Case MSG_VALUE_NO
                    GoBack()
            End Select
        End If

    End Sub

    Protected Sub ComingFromNewCopy()
        Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value

        If Not confResponse = String.Empty Then
            ' Return from the New Copy Button

            Select Case confResponse
                Case MSG_VALUE_YES
                    ' Save and create a new Copy BO
                    If ApplyChanges() = True Then
                        Me.State.boChanged = True
                        CreateNewCopy()
                    End If
                Case MSG_VALUE_NO
                    ' create a new BO
                    CreateNewCopy()
            End Select
        End If

    End Sub
    Protected Sub ComingFromNew()
        Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value

        If Not confResponse = String.Empty Then
            ' Return from the New Copy Button

            Select Case confResponse
                Case MSG_VALUE_YES
                    ' Save and create a new Copy BO
                    If ApplyChanges() = True Then
                        Me.State.boChanged = True
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
            Select Case Me.State.ActionInProgress
                    ' Period
                Case ElitaPlusPage.DetailPageCommand.Back
                    ComingFromBack()
                Case ElitaPlusPage.DetailPageCommand.New_
                    ComingFromNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    ComingFromNewCopy()
            End Select

            'Clean after consuming the action
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            Me.HiddenSaveChangesPromptResponse.Value = String.Empty
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Handlers-Labels"

    Private Sub BindBoPropertiesToLabels()
        Me.BindBOPropertyToLabel(Me.State.MyBO, DEALERID_PROPERTY, Me.TheDealerControl.CaptionLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO, BRANCHCODE_PROPERTY, Me.lblBranchCode)
        Me.BindBOPropertyToLabel(Me.State.MyBO, BRANCHNAME_PROPERTY, Me.lblBranchName)
        Me.BindBOPropertyToLabel(Me.State.MyBO, CONTACTEMAIL_PROPERTY, Me.lblContactEmail)
        Me.BindBOPropertyToLabel(Me.State.MyBO, CONTACTEXT_PROPERTY, Me.lblContactExt)
        Me.BindBOPropertyToLabel(Me.State.MyBO, CONTACTFAX_PROPERTY, Me.lblContactFax)
        Me.BindBOPropertyToLabel(Me.State.MyBO, CONTACTPHONE_PROPERTY, Me.lblContactPhone)
        Me.BindBOPropertyToLabel(Me.State.MyBO, MARKET_PROPERTY, Me.lblMarket)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "MarketingRegion", Me.lblMarketingRegion)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "BranchTypeId", Me.lblBranchType)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "StoreManager", Me.lblStoreMgr)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "OpenDate", Me.lblOpenDate)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "CloseDate", Me.lblCloseDate)
    End Sub

    Private Sub ClearLabelsErrSign()
        Me.ClearLabelErrSign(lblBranchCode)
        Me.ClearLabelErrSign(TheDealerControl.CaptionLabel)
        Me.ClearLabelErrSign(lblBranchName)
        Me.ClearLabelErrSign(lblBranchType)
        Me.ClearLabelErrSign(lblStoreMgr)
        Me.ClearLabelErrSign(lblContactPhone)
        Me.ClearLabelErrSign(lblContactExt)
        Me.ClearLabelErrSign(lblContactFax)
        Me.ClearLabelErrSign(lblContactEmail)
        Me.ClearLabelErrSign(lblMarket)
        Me.ClearLabelErrSign(lblMarketingRegion)
        Me.ClearLabelErrSign(lblOpenDate)
        Me.ClearLabelErrSign(lblCloseDate)
    End Sub

    Public Shared Sub SetLabelColor(ByVal lbl As Label)
        lbl.ForeColor = Color.Black
    End Sub

#End Region

#End Region

End Class