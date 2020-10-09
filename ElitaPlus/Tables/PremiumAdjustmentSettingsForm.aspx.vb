Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Public Class PremiumAdjustmentSettingsForm
    Inherits ElitaPlusPage

    Protected WithEvents ErrorCtrl As ErrorController

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents Panel1 As System.Web.UI.WebControls.Panel
    Protected WithEvents CodeValidator As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents RegularExpressionValidator1 As System.Web.UI.WebControls.RegularExpressionValidator
    Protected WithEvents multipleDropControl As MultipleColumnDDLabelControl


    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Constants"
    Public Const URL As String = "PremiumAdjustmentSettingsForm.aspx"
    Private Const ONE_ITEM As Integer = 1
    Private Const LABEL_SELECT_DEALERCODE As String = "DEALER"
    Private Const FIN_ADJ_BY_PERCENTAGE As String = "PCT"
    Private Const FIN_ADJ_BY_AMOUNT As String = "FAMT"

#End Region

#Region "Properties"
    Public ReadOnly Property DealerMultipleDrop() As MultipleColumnDDLabelControl
        Get
            If multipleDropControl Is Nothing Then
                multipleDropControl = CType(FindControl("MultipleColumnDDLabelControl"), MultipleColumnDDLabelControl)
            End If
            Return multipleDropControl
        End Get
    End Property
#End Region

#Region "Page Return Type"
    ' the information here is used in the search page
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As PremiumAdjustmentSettings
        Public BoChanged As Boolean = False
        Public Sub New(LastOp As DetailPageCommand, curEditingBo As PremiumAdjustmentSettings, Optional ByVal boChanged As Boolean = False)
            LastOperation = LastOp
            EditingBo = curEditingBo
            Me.BoChanged = boChanged
        End Sub
    End Class
#End Region

#Region "Page State"


    Class MyState
        Public MyBO As PremiumAdjustmentSettings
        Public ScreenSnapShotBO As PremiumAdjustmentSettings
        Public AdjustmentSettingChanged As Boolean = False
        Public DealerId As Guid

        Public actionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
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
                State.MyBO = New PremiumAdjustmentSettings(CType(CallingParameters, Guid))
                State.AdjustmentSettingChanged = False
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try

    End Sub

#End Region

#Region "Page Events"
    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        ErrorCtrl.Clear_Hide()
        Try
            cboFinancialAdjustmentBy.Attributes.Add("onChange", "fabDDOnChange(this);")

            Dim oEffectiveDateImage As ImageButton = CType(FindControl("moEffectiveDateImage"), ImageButton)
            If (oEffectiveDateImage IsNot Nothing) Then
                AddCalendar(oEffectiveDateImage, CType(FindControl("TextBoxEffectiveDate"), TextBox))
            End If

            Dim oExpirationDateImage As ImageButton = CType(FindControl("moExpirationDateImage"), ImageButton)
            If (oExpirationDateImage IsNot Nothing) Then
                AddCalendar(oExpirationDateImage, CType(FindControl("TextBoxExpirationDate"), TextBox))
            End If

            If Not IsPostBack Then
                MenuEnabled = False
                AddConfirmation(btnDelete_WRITE, Message.DELETE_RECORD_PROMPT)
                If State.MyBO Is Nothing Then
                    State.MyBO = New PremiumAdjustmentSettings
                End If
                PopulateDropdowns()
                InitializeDealerDropDowns()
                PopulateFormFromBOs()
                'Me.EnableDisableFields()
            End If
            BindBoPropertiesToLabels()
            CheckIfComingFromSaveConfirm()
            EnableDisableFields()
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
        ShowMissingTranslations(ErrorCtrl)
    End Sub
#End Region

#Region "Controlling Logic"

    Private Sub InitializeDealerDropDowns()

        DealerMultipleDrop.Caption = TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALERCODE)
        DealerMultipleDrop.NothingSelected = True

        DealerMultipleDrop.BindData(LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies))
        DealerMultipleDrop.AutoPostBackDD = True

        If State.MyBO IsNot Nothing Then
            DealerMultipleDrop.SelectedGuid = State.MyBO.DealerId
        End If


    End Sub




    Protected Sub EnableDisableFields()

        'Enabled by Default
        ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnCopy_WRITE, True)

        If (GetGuidFromString(cboFinancialAdjustmentBy.SelectedValue) = Guid.Empty) Then
            LabelAdjustmentPercentage.Style.Add("display", "none")
            TextBoxAdjustmentPercentage.Style.Add("display", "none")
            LabelAdjustmentAmount.Style.Add("display", "none")
            TextBoxAdjustmentAmount.Style.Add("display", "none")
            LabelAdjustmentBasedOn.Style.Add("display", "none")
            cboAdjustmentBasedOn.Style.Add("display", "none")
        ElseIf (LookupListNew.GetCodeFromId("FIN_ADJ_BY", GetGuidFromString(cboFinancialAdjustmentBy.SelectedValue)) = FIN_ADJ_BY_PERCENTAGE) Then
            LabelAdjustmentAmount.Style.Add("display", "none")
            TextBoxAdjustmentAmount.Style.Add("display", "none")

            LabelAdjustmentPercentage.Style.Add("display", "inline")
            TextBoxAdjustmentPercentage.Style.Add("display", "inline")

            LabelAdjustmentBasedOn.Style.Add("display", "none")
            cboAdjustmentBasedOn.Style.Add("display", "none")
        ElseIf (LookupListNew.GetCodeFromId("FIN_ADJ_BY", GetGuidFromString(cboFinancialAdjustmentBy.SelectedValue)) = FIN_ADJ_BY_AMOUNT) Then
            LabelAdjustmentPercentage.Style.Add("display", "none")
            TextBoxAdjustmentPercentage.Style.Add("display", "none")

            LabelAdjustmentAmount.Style.Add("display", "inline")
            TextBoxAdjustmentAmount.Style.Add("display", "inline")

            LabelAdjustmentBasedOn.Style.Add("display", "inline")
            cboAdjustmentBasedOn.Style.Add("display", "inline")
        End If
        'New With Copy Button
        If State.MyBO.IsNew Then
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
        End If
    End Sub

    Protected Sub BindBoPropertiesToLabels()
        BindBOPropertyToLabel(State.MyBO, "AdjustmentBy", LabeFinancialAdjustmentBy)
        BindBOPropertyToLabel(State.MyBO, "AdjustmentBasedOn", LabelAdjustmentBasedOn)
        BindBOPropertyToLabel(State.MyBO, "AdjustmentPercentage", LabelAdjustmentPercentage)
        BindBOPropertyToLabel(State.MyBO, "AdjustmentAmount", LabelAdjustmentAmount)
        BindBOPropertyToLabel(State.MyBO, "EffectiveDate", LabelEffectiveDate)
        BindBOPropertyToLabel(State.MyBO, "ExpirationDate", LabelExpirationDate)
        ClearGridHeadersAndLabelsErrSign()
    End Sub

    Protected Sub PopulateDropdowns()
        'Me.BindListControlToDataView(Me.cboAdjustmentBasedOn, LookupListNew.DropdownLookupList("ABO", langId, True))
        cboAdjustmentBasedOn.Populate(CommonConfigManager.Current.ListManager.GetList("ABO", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
            {
                .AddBlankItem = True
            })

        'Me.BindListControlToDataView(Me.cboFinancialAdjustmentBy, LookupListNew.DropdownLookupList("FAB", langId, True))
        cboFinancialAdjustmentBy.Populate(CommonConfigManager.Current.ListManager.GetList("FAB", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
            {
                .AddBlankItem = True
            })
    End Sub

    Protected Sub PopulateFormFromBOs()
        With State.MyBO
            PopulateControlFromBOProperty(TextBoxAdjustmentAmount, .AdjustmentAmount)
            PopulateControlFromBOProperty(TextBoxAdjustmentPercentage, .AdjustmentPercentage)
            PopulateControlFromBOProperty(TextBoxEffectiveDate, .EffectiveDate)
            PopulateControlFromBOProperty(TextBoxExpirationDate, .ExpirationDate)

            SetSelectedItem(cboFinancialAdjustmentBy, .AdjustmentBy)
            SetSelectedItem(cboAdjustmentBasedOn, .AdjustmentBasedOn)

            If Not LookupListNew.GetIdFromCode("FIN_ADJ_BY", FIN_ADJ_BY_AMOUNT) = Guid.Empty Then
                hdFinAdjustByAmt.Value = LookupListNew.GetIdFromCode("FIN_ADJ_BY", FIN_ADJ_BY_AMOUNT).ToString
            End If

            If Not LookupListNew.GetIdFromCode("FIN_ADJ_BY", FIN_ADJ_BY_PERCENTAGE) = Guid.Empty Then
                hdFinAdjustByPct.Value = LookupListNew.GetIdFromCode("FIN_ADJ_BY", FIN_ADJ_BY_PERCENTAGE).ToString
            End If

        End With

    End Sub

    Protected Sub PopulateBOsFormFrom()
        With State.MyBO
            PopulateBOProperty(State.MyBO, "AdjustmentBy", cboFinancialAdjustmentBy)
            PopulateBOProperty(State.MyBO, "AdjustmentBasedOn", cboAdjustmentBasedOn)
            PopulateBOProperty(State.MyBO, "AdjustmentPercentage", TextBoxAdjustmentPercentage)
            PopulateBOProperty(State.MyBO, "AdjustmentAmount", TextBoxAdjustmentAmount)
            PopulateBOProperty(State.MyBO, "EffectiveDate", TextBoxEffectiveDate)
            PopulateBOProperty(State.MyBO, "ExpirationDate", TextBoxExpirationDate)
            If State.MyBO.IsNew AndAlso ElitaPlusIdentity.Current.ActiveUser.Companies.Count > 1 Then
                State.MyBO.DealerId = DealerMultipleDrop.SelectedGuid
            End If
        End With
        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub


    Protected Sub CreateNew()
        State.ScreenSnapShotBO = Nothing 'Reset the backup copy

        State.MyBO = New PremiumAdjustmentSettings
        PopulateFormFromBOs()
        EnableDisableFields()
    End Sub

    Protected Sub CreateNewWithCopy()
        State.MyBO = New PremiumAdjustmentSettings
        PopulateBOsFormFrom()
        EnableDisableFields()

        'create the backup copy for undo
        State.ScreenSnapShotBO = New PremiumAdjustmentSettings
        State.ScreenSnapShotBO.Clone(State.MyBO)
    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = HiddenSaveChangesPromptResponse.Value

        'If Not confResponse Is Nothing AndAlso confResponse = Me.CONFIRM_MESSAGE_OK Then
        If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
            Select Case State.actionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    State.MyBO.Save()
                    State.AdjustmentSettingChanged = True
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.AdjustmentSettingChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    State.MyBO.Save()
                    State.AdjustmentSettingChanged = True
                    AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                    CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    State.MyBO.Save()
                    State.AdjustmentSettingChanged = True
                    AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                    CreateNewWithCopy()
            End Select
            'ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.CONFIRM_MESSAGE_CANCEL Then
        ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
            Select Case State.actionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.AdjustmentSettingChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    CreateNewWithCopy()
            End Select
        End If

        'Clean after consuming the action
        State.actionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        HiddenSaveChangesPromptResponse.Value = ""
    End Sub


#End Region

#Region "Button Clicks"

    Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
        Try
            PopulateBOsFormFrom()
            If State.MyBO.IsDirty Then
                'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.actionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.AdjustmentSettingChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnSave_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            Dim errors() As ValidationError = {New ValidationError("Adjustment Based On is required", GetType(PremiumAdjustmentSettings), Nothing, "AdjustmentBasedOn", Nothing)}
            PopulateBOsFormFrom()
            If ((State.MyBO.AdjustmentBasedOn.ToString = Guid.Empty.ToString) AndAlso (State.MyBO.AdjustmentBy.ToString = LookupListNew.GetIdFromCode("FIN_ADJ_BY", FIN_ADJ_BY_AMOUNT).ToString)) Then
                Throw New BOValidationException(errors, GetType(PremiumAdjustmentSettings).FullName)
            End If

            If State.MyBO.IsDirty Then
                If State.MyBO.IsNew Then
                    State.MyBO.DealerId = DealerMultipleDrop.SelectedGuid
                End If
                State.MyBO.Save()
                State.AdjustmentSettingChanged = True
                PopulateFormFromBOs()
                EnableDisableFields()
                AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
            Else
                AddInfoMsg(Message.MSG_RECORD_NOT_SAVED)
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnUndo_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_WRITE.Click
        Try
            If Not State.MyBO.IsNew Then
                'Reload from the DB
                State.MyBO = New PremiumAdjustmentSettings(State.MyBO.Id)
            ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                'It was a new with copy
                State.MyBO.Clone(State.ScreenSnapShotBO)
            Else
                State.MyBO = New PremiumAdjustmentSettings
            End If
            PopulateFormFromBOs()
            EnableDisableFields()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnDelete_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete_WRITE.Click
        Try
            State.MyBO.Delete()
            State.MyBO.Save()
            State.AdjustmentSettingChanged = True
            ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, State.MyBO, State.AdjustmentSettingChanged))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            State.MyBO.RejectChanges()
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            PopulateBOsFormFrom()
            If State.MyBO.IsDirty Then
                AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, HiddenSaveChangesPromptResponse)
                State.actionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                CreateNew()
            End If

        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub



    Private Sub btnCopy_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnCopy_WRITE.Click
        Try
            PopulateBOsFormFrom()
            If State.MyBO.IsDirty Then
                AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, HiddenSaveChangesPromptResponse)
                State.actionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            Else
                CreateNewWithCopy()

            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub


#End Region



End Class