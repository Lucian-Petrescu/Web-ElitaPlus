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

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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
        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As PremiumAdjustmentSettings, Optional ByVal boChanged As Boolean = False)
            Me.LastOperation = LastOp
            Me.EditingBo = curEditingBo
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

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                'Get the id from the parent
                Me.State.MyBO = New PremiumAdjustmentSettings(CType(Me.CallingParameters, Guid))
                Me.State.AdjustmentSettingChanged = False
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try

    End Sub

#End Region

#Region "Page Events"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Me.ErrorCtrl.Clear_Hide()
        Try
            cboFinancialAdjustmentBy.Attributes.Add("onChange", "fabDDOnChange(this);")

            Dim oEffectiveDateImage As ImageButton = CType(FindControl("moEffectiveDateImage"), ImageButton)
            If (Not oEffectiveDateImage Is Nothing) Then
                Me.AddCalendar(oEffectiveDateImage, CType(FindControl("TextBoxEffectiveDate"), TextBox))
            End If

            Dim oExpirationDateImage As ImageButton = CType(FindControl("moExpirationDateImage"), ImageButton)
            If (Not oExpirationDateImage Is Nothing) Then
                Me.AddCalendar(oExpirationDateImage, CType(FindControl("TextBoxExpirationDate"), TextBox))
            End If

            If Not Me.IsPostBack Then
                Me.MenuEnabled = False
                Me.AddConfirmation(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT)
                If Me.State.MyBO Is Nothing Then
                    Me.State.MyBO = New PremiumAdjustmentSettings
                End If
                PopulateDropdowns()
                InitializeDealerDropDowns()
                Me.PopulateFormFromBOs()
                'Me.EnableDisableFields()
            End If
            BindBoPropertiesToLabels()
            CheckIfComingFromSaveConfirm()
            Me.EnableDisableFields()
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
        Me.ShowMissingTranslations(Me.ErrorCtrl)
    End Sub
#End Region

#Region "Controlling Logic"

    Private Sub InitializeDealerDropDowns()

        DealerMultipleDrop.Caption = TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALERCODE)
        DealerMultipleDrop.NothingSelected = True

        DealerMultipleDrop.BindData(LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies))
        DealerMultipleDrop.AutoPostBackDD = True

        If Not Me.State.MyBO Is Nothing Then
            Me.DealerMultipleDrop.SelectedGuid = Me.State.MyBO.DealerId
        End If


    End Sub




    Protected Sub EnableDisableFields()

        'Enabled by Default
        ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnCopy_WRITE, True)

        If (GetGuidFromString(Me.cboFinancialAdjustmentBy.SelectedValue) = Guid.Empty) Then
            Me.LabelAdjustmentPercentage.Style.Add("display", "none")
            Me.TextBoxAdjustmentPercentage.Style.Add("display", "none")
            Me.LabelAdjustmentAmount.Style.Add("display", "none")
            Me.TextBoxAdjustmentAmount.Style.Add("display", "none")
            Me.LabelAdjustmentBasedOn.Style.Add("display", "none")
            Me.cboAdjustmentBasedOn.Style.Add("display", "none")
        ElseIf (LookupListNew.GetCodeFromId("FIN_ADJ_BY", GetGuidFromString(Me.cboFinancialAdjustmentBy.SelectedValue)) = FIN_ADJ_BY_PERCENTAGE) Then
            Me.LabelAdjustmentAmount.Style.Add("display", "none")
            Me.TextBoxAdjustmentAmount.Style.Add("display", "none")

            Me.LabelAdjustmentPercentage.Style.Add("display", "inline")
            Me.TextBoxAdjustmentPercentage.Style.Add("display", "inline")

            Me.LabelAdjustmentBasedOn.Style.Add("display", "none")
            Me.cboAdjustmentBasedOn.Style.Add("display", "none")
        ElseIf (LookupListNew.GetCodeFromId("FIN_ADJ_BY", GetGuidFromString(Me.cboFinancialAdjustmentBy.SelectedValue)) = FIN_ADJ_BY_AMOUNT) Then
            Me.LabelAdjustmentPercentage.Style.Add("display", "none")
            Me.TextBoxAdjustmentPercentage.Style.Add("display", "none")

            Me.LabelAdjustmentAmount.Style.Add("display", "inline")
            Me.TextBoxAdjustmentAmount.Style.Add("display", "inline")

            Me.LabelAdjustmentBasedOn.Style.Add("display", "inline")
            Me.cboAdjustmentBasedOn.Style.Add("display", "inline")
        End If
        'New With Copy Button
        If Me.State.MyBO.IsNew Then
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
        End If
    End Sub

    Protected Sub BindBoPropertiesToLabels()
        Me.BindBOPropertyToLabel(Me.State.MyBO, "AdjustmentBy", Me.LabeFinancialAdjustmentBy)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "AdjustmentBasedOn", Me.LabelAdjustmentBasedOn)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "AdjustmentPercentage", Me.LabelAdjustmentPercentage)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "AdjustmentAmount", Me.LabelAdjustmentAmount)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "EffectiveDate", Me.LabelEffectiveDate)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ExpirationDate", Me.LabelExpirationDate)
        Me.ClearGridHeadersAndLabelsErrSign()
    End Sub

    Protected Sub PopulateDropdowns()
        'Me.BindListControlToDataView(Me.cboAdjustmentBasedOn, LookupListNew.DropdownLookupList("ABO", langId, True))
        Me.cboAdjustmentBasedOn.Populate(CommonConfigManager.Current.ListManager.GetList("ABO", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
            {
                .AddBlankItem = True
            })

        'Me.BindListControlToDataView(Me.cboFinancialAdjustmentBy, LookupListNew.DropdownLookupList("FAB", langId, True))
        Me.cboFinancialAdjustmentBy.Populate(CommonConfigManager.Current.ListManager.GetList("FAB", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
            {
                .AddBlankItem = True
            })
    End Sub

    Protected Sub PopulateFormFromBOs()
        With Me.State.MyBO
            Me.PopulateControlFromBOProperty(Me.TextBoxAdjustmentAmount, .AdjustmentAmount)
            Me.PopulateControlFromBOProperty(Me.TextBoxAdjustmentPercentage, .AdjustmentPercentage)
            Me.PopulateControlFromBOProperty(Me.TextBoxEffectiveDate, .EffectiveDate)
            Me.PopulateControlFromBOProperty(Me.TextBoxExpirationDate, .ExpirationDate)

            Me.SetSelectedItem(Me.cboFinancialAdjustmentBy, .AdjustmentBy)
            Me.SetSelectedItem(Me.cboAdjustmentBasedOn, .AdjustmentBasedOn)

            If Not LookupListNew.GetIdFromCode("FIN_ADJ_BY", FIN_ADJ_BY_AMOUNT) = Guid.Empty Then
                hdFinAdjustByAmt.Value = LookupListNew.GetIdFromCode("FIN_ADJ_BY", FIN_ADJ_BY_AMOUNT).ToString
            End If

            If Not LookupListNew.GetIdFromCode("FIN_ADJ_BY", FIN_ADJ_BY_PERCENTAGE) = Guid.Empty Then
                hdFinAdjustByPct.Value = LookupListNew.GetIdFromCode("FIN_ADJ_BY", FIN_ADJ_BY_PERCENTAGE).ToString
            End If

        End With

    End Sub

    Protected Sub PopulateBOsFormFrom()
        With Me.State.MyBO
            Me.PopulateBOProperty(Me.State.MyBO, "AdjustmentBy", Me.cboFinancialAdjustmentBy)
            Me.PopulateBOProperty(Me.State.MyBO, "AdjustmentBasedOn", Me.cboAdjustmentBasedOn)
            Me.PopulateBOProperty(Me.State.MyBO, "AdjustmentPercentage", Me.TextBoxAdjustmentPercentage)
            Me.PopulateBOProperty(Me.State.MyBO, "AdjustmentAmount", Me.TextBoxAdjustmentAmount)
            Me.PopulateBOProperty(Me.State.MyBO, "EffectiveDate", Me.TextBoxEffectiveDate)
            Me.PopulateBOProperty(Me.State.MyBO, "ExpirationDate", Me.TextBoxExpirationDate)
            If Me.State.MyBO.IsNew And ElitaPlusIdentity.Current.ActiveUser.Companies.Count > 1 Then
                Me.State.MyBO.DealerId = Me.DealerMultipleDrop.SelectedGuid
            End If
        End With
        If Me.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub


    Protected Sub CreateNew()
        Me.State.ScreenSnapShotBO = Nothing 'Reset the backup copy

        Me.State.MyBO = New PremiumAdjustmentSettings
        Me.PopulateFormFromBOs()
        Me.EnableDisableFields()
    End Sub

    Protected Sub CreateNewWithCopy()
        Me.State.MyBO = New PremiumAdjustmentSettings
        Me.PopulateBOsFormFrom()
        Me.EnableDisableFields()

        'create the backup copy for undo
        Me.State.ScreenSnapShotBO = New PremiumAdjustmentSettings
        Me.State.ScreenSnapShotBO.Clone(Me.State.MyBO)
    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value

        'If Not confResponse Is Nothing AndAlso confResponse = Me.CONFIRM_MESSAGE_OK Then
        If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
            Select Case Me.State.actionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Me.State.MyBO.Save()
                    Me.State.AdjustmentSettingChanged = True
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.AdjustmentSettingChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    Me.State.MyBO.Save()
                    Me.State.AdjustmentSettingChanged = True
                    Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                    Me.CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    Me.State.MyBO.Save()
                    Me.State.AdjustmentSettingChanged = True
                    Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                    Me.CreateNewWithCopy()
            End Select
            'ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.CONFIRM_MESSAGE_CANCEL Then
        ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
            Select Case Me.State.actionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.AdjustmentSettingChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    Me.CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    Me.CreateNewWithCopy()
            End Select
        End If

        'Clean after consuming the action
        Me.State.actionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        Me.HiddenSaveChangesPromptResponse.Value = ""
    End Sub


#End Region

#Region "Button Clicks"

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.PopulateBOsFormFrom()
            If Me.State.MyBO.IsDirty Then
                'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.actionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.AdjustmentSettingChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            Dim errors() As ValidationError = {New ValidationError("Adjustment Based On is required", GetType(PremiumAdjustmentSettings), Nothing, "AdjustmentBasedOn", Nothing)}
            Me.PopulateBOsFormFrom()
            If ((Me.State.MyBO.AdjustmentBasedOn.ToString = Guid.Empty.ToString) And (Me.State.MyBO.AdjustmentBy.ToString = LookupListNew.GetIdFromCode("FIN_ADJ_BY", FIN_ADJ_BY_AMOUNT).ToString)) Then
                Throw New BOValidationException(errors, GetType(PremiumAdjustmentSettings).FullName)
            End If

            If Me.State.MyBO.IsDirty Then
                If Me.State.MyBO.IsNew Then
                    Me.State.MyBO.DealerId = Me.DealerMultipleDrop.SelectedGuid
                End If
                Me.State.MyBO.Save()
                Me.State.AdjustmentSettingChanged = True
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
                Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
            Else
                Me.AddInfoMsg(Message.MSG_RECORD_NOT_SAVED)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnUndo_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_WRITE.Click
        Try
            If Not Me.State.MyBO.IsNew Then
                'Reload from the DB
                Me.State.MyBO = New PremiumAdjustmentSettings(Me.State.MyBO.Id)
            ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                'It was a new with copy
                Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
            Else
                Me.State.MyBO = New PremiumAdjustmentSettings
            End If
            Me.PopulateFormFromBOs()
            Me.EnableDisableFields()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnDelete_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click
        Try
            Me.State.MyBO.Delete()
            Me.State.MyBO.Save()
            Me.State.AdjustmentSettingChanged = True
            Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, Me.State.MyBO, Me.State.AdjustmentSettingChanged))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.State.MyBO.RejectChanges()
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnNew_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            Me.PopulateBOsFormFrom()
            If Me.State.MyBO.IsDirty Then
                Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                Me.State.actionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                Me.CreateNew()
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub



    Private Sub btnCopy_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy_WRITE.Click
        Try
            Me.PopulateBOsFormFrom()
            If Me.State.MyBO.IsDirty Then
                Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                Me.State.actionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            Else
                Me.CreateNewWithCopy()

            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub


#End Region



End Class