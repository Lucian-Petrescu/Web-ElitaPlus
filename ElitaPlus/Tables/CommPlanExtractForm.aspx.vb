Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.Security


Partial Class CommPlanExtractForm
    Inherits ElitaPlusPage

#Region "Page State"

#Region "MyState"

    Class MyState

        Public moIsNewCommPlanExt As Guid = Guid.Empty
        Public IsCommPlanExtNew As Boolean = False
        Public IsNewWithCopy As Boolean = False
        Public IsUndo As Boolean = False
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public LastOperation As DetailPageCommand = DetailPageCommand.Nothing_
        Public boChanged As Boolean = False
        Public LastErrMsg As String
        Public HasDataChanged As Boolean
        Public MyBO As CommPlanExtract
        Public ScreenSnapShotBO As CommPlanExtract
        Public InputParameters As Parameters


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
        State.InputParameters = CType(CallingParameters, Parameters)

        If State.InputParameters IsNot Nothing Then
            State.moIsNewCommPlanExt = State.InputParameters.CommPlanExtId
        End If

        If State.moIsNewCommPlanExt.Equals(Guid.Empty) Then
            State.IsCommPlanExtNew = True
            BindBoPropertiesToLabels()
            AddLabelDecorations(TheCommPlanExt)
            ClearAll()
            PopulateAll()
        Else
            State.IsCommPlanExtNew = False
            BindBoPropertiesToLabels()
            AddLabelDecorations(TheCommPlanExt)
            PopulateAll()
        End If
    End Sub

#End Region

#Region "Constants"

    Public Shared URL As String = "CommPlanExtractForm.aspx"
    Private Const COMMPLANEXTRACT_LIST_FORM001 As String = "COMM_PLAN_FORM001" ' Maintain Exception

#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public moCommissPlanId As Guid
        Public HasDataChanged As Boolean
        Public Sub New(LastOp As DetailPageCommand, oCommissPlanId As Guid, hasDataChanged As Boolean)
            LastOperation = LastOp
            moCommissPlanId = oCommissPlanId
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class

#End Region

#Region "Parameters"
    Public Class Parameters
        Public CommPlanId As Guid
        Public CommPlanExtId As Guid
        Public Sub New(commplanid As Guid, commplanextid As Guid)
            Me.CommPlanId = commplanid
            Me.CommPlanExtId = commplanextid
        End Sub
    End Class
#End Region

#Region "Properties"
    Private ReadOnly Property TheCommPlanExt As CommPlanExtract

        Get
            If State.MyBO Is Nothing Then
                If State.IsCommPlanExtNew = True Then
                    ' For creating, inserting
                    State.MyBO = New CommPlanExtract
                    State.moIsNewCommPlanExt = State.MyBO.Id
                Else
                    ' For updating, deleting
                    State.MyBO = New CommPlanExtract(State.moIsNewCommPlanExt)
                End If
            End If

            Return State.MyBO
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

                If State.IsCommPlanExtNew = True Then
                    CreateNew()
                End If
                PopulateFormFromBOs()
                AddCalendar(BtnCommiEffectDate, txtCommiEffectDate)
                AddCalendar(BtnCommiExpDate, txtCommiExpDate)
            End If

            EnableDisableCommPerAmt()
            BindBoPropertiesToLabels()
            CheckIfComingFromConfirm()
            If Not IsPostBack Then
                AddLabelDecorations(TheCommPlanExt)
            End If
            DisableCommPerAmtTxt()

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
                State.InputParameters = CType(CallingParameters, Parameters)
                State.MyBO = New CommPlanExtract(State.InputParameters.CommPlanExtId)
            Else
                State.IsCommPlanExtNew = True
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
    End Sub

    Private Sub GoBack()
        Dim retType As New CommPlanExtractForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO.CommissionPlanId, State.boChanged)
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
            If Not State.IsCommPlanExtNew Then
                'Reload from the DB
                State.MyBO = New CommPlanExtract(State.MyBO.Id)
            ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                'It was a new with copy
                State.MyBO.Clone(State.ScreenSnapShotBO)
            Else
                CreateNew()
            End If
            PopulateAll()
            PopulateFormFromBOs()
            SetButtonsState(State.IsCommPlanExtNew)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub CreateNew()
        State.ScreenSnapShotBO = Nothing
        State.IsCommPlanExtNew = True
        State.MyBO = New CommPlanExtract
        ClearAll()
        SetButtonsState(True)
        PopulateAll()
        PopulateBOsFromForm()
        PopulateFormFromBOs()
        EnableDisableCommPerAmt()
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

        Dim newObj As New CommPlanExtract
        newObj.Copy(TheCommPlanExt)

        State.MyBO = newObj
        State.moIsNewCommPlanExt = Guid.Empty
        State.IsCommPlanExtNew = True

        SetButtonsState(True)
        PopulateFormFromBOs()
        EnableDisableCommPerAmt()

        'create the backup copy
        State.ScreenSnapShotBO = New CommPlanExtract
        State.ScreenSnapShotBO.Copy(TheCommPlanExt)

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
            ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, State.MyBO.CommissionPlanId, State.HasDataChanged))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Clear"

    Private Sub ClearTexts()
        txtCommPlanCode.Text = Nothing
        txtCommPlanDesc.Text = Nothing
        txtCommiEffectDate.Text = Nothing
        txtCommiExpDate.Text = Nothing
        txtCyclRunDay.Text = Nothing
        txtCyclCutOffDay.Text = Nothing
        txtlblSeqNumber.Text = Nothing
        txtCommiPerct.Text = Nothing
        txtCommiAmt.Text = Nothing
    End Sub

    Private Sub ClearAll()
        ClearTexts()
        ClearList(ddlAmtXcd)
        ClearList(ddlCommiExtType)
        ClearList(ddlCommRateXcd)
    End Sub

#End Region

#Region "Populate"

    Private Sub UpdateBreadCrum()
        If (State IsNot Nothing) Then
            If (State IsNot Nothing) Then
                MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("COMM_PLAN_EXTRACT")
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("COMM_PLAN_EXTRACT")
            End If
        End If
    End Sub
    Private Sub PopulateDropDowns()
        Try
            Dim commRate As ListItem() = CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode())
            commRate.OrderBy("Description", LinqExtentions.SortDirection.Ascending)
            ddlCommRateXcd.Populate(commRate, New PopulateOptions() With
                                              {
                                                .AddBlankItem = False
                                               })

            ' Me.ddlAmtXcd.PopulateOld("COMM_AMOUNT_SOURCE", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.None, String.Empty, ListValueType.Description)
            Dim amountSource As ListItem() = CommonConfigManager.Current.ListManager.GetList("COMM_AMOUNT_SOURCE", Thread.CurrentPrincipal.GetLanguageCode())
            ddlAmtXcd.Populate(amountSource, New PopulateOptions() With
                                              {
                                                .ValueFunc = AddressOf PopulateOptions.GetExtendedCode
                                               })

            Dim extracttypes As ListItem() = CommonConfigManager.Current.ListManager.GetList("ExtractTypeList", Thread.CurrentPrincipal.GetLanguageCode())
            ddlCommiExtType.Populate(extracttypes, New PopulateOptions() With
                                                {
                                                    .AddBlankItem = False
                                                })

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            MasterPage.MessageController.AddError(COMMPLANEXTRACT_LIST_FORM001)
            MasterPage.MessageController.AddError(ex.Message, False)
            MasterPage.MessageController.Show()
        End Try
    End Sub

    Private Sub PopulateFormFromBOs()
        Try
            With TheCommPlanExt
                BindSelectItem(.AmountSourceXcd, ddlAmtXcd)
                BindSelectItem(.CommExtractPackageId.ToString, ddlCommiExtType)
                BindSelectItem(.CommAtRateXcd, ddlCommRateXcd)
                PopulateControlFromBOProperty(txtCommPlanCode, .Code)
                PopulateControlFromBOProperty(txtCommPlanDesc, .Description)
                PopulateControlFromBOProperty(txtCommiEffectDate, .EffectiveDate)
                PopulateControlFromBOProperty(txtCommiExpDate, .ExpirationDate)
                PopulateControlFromBOProperty(txtCyclRunDay, .CycleRunDay)
                PopulateControlFromBOProperty(txtCyclCutOffDay, .CycleCutOffDay)
                PopulateControlFromBOProperty(txtlblSeqNumber, .SequenceNumber)
                PopulateControlFromBOProperty(txtCommiPerct, .CommissionPercentage)
                PopulateControlFromBOProperty(txtCommiAmt, .CommissionAmount)
                PopulateControlFromBOProperty(txtCyclFreXcd, .CycleFrequencyXcd)
                PopulateControlFromBOProperty(txtCyclSrcXcd, .CycleCutOffSourceXcd)
            End With
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub PopulateAll()
        PopulateDropDowns()
    End Sub

    Protected Sub PopulateBOsFromForm()

        With TheCommPlanExt
            PopulateBOProperty(State.MyBO, "Code", txtCommPlanCode)
            PopulateBOProperty(State.MyBO, "Description", txtCommPlanDesc)
            PopulateBOProperty(State.MyBO, "EffectiveDate", txtCommiEffectDate)
            PopulateBOProperty(State.MyBO, "ExpirationDate", txtCommiExpDate)
            PopulateBOProperty(State.MyBO, "CycleRunDay", txtCyclRunDay)
            PopulateBOProperty(State.MyBO, "CycleCutOffDay", txtCyclCutOffDay)
            PopulateBOProperty(State.MyBO, "SequenceNumber", txtlblSeqNumber)
            PopulateBOProperty(State.MyBO, "CommissionPercentage", txtCommiPerct)
            PopulateBOProperty(State.MyBO, "CommissionAmount", txtCommiAmt)
            PopulateBOProperty(State.MyBO, "AmountSourceXcd", ddlAmtXcd, False, True)
            PopulateBOProperty(State.MyBO, "CommExtractPackageId", ddlCommiExtType)
            PopulateBOProperty(State.MyBO, "CommTitleXcd", GetSelectedDescription(ddlCommiExtType))
            PopulateBOProperty(State.MyBO, "CommAtRateXcd", ddlCommRateXcd, False, True)

        End With
        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If

    End Sub
#End Region

#Region "Gui-Validation"

    Private Sub SetButtonsState(bIsNew As Boolean)
        ControlMgr.SetEnableControl(Me, btnNew_WRITE, Not bIsNew)
        ControlMgr.SetEnableControl(Me, btnCopy_WRITE, Not bIsNew)
        ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
    End Sub

    Private Sub EnableDisableCommPerAmt()
        If ddlCommRateXcd.SelectedValue.Equals("YESNO-Y") Then

            lblCommiAmt.Visible = False
            txtCommiAmt.Visible = False
            lblCommiPerct.Visible = False
            txtCommiPerct.Visible = False
        Else
            lblCommiAmt.Visible = True
            txtCommiAmt.Visible = True
            lblCommiPerct.Visible = True
            txtCommiPerct.Visible = True

        End If
    End Sub

    Public Sub ValidateCommPerandAmt()
        If (txtCommiPerct.Text.Equals(String.Empty) AndAlso txtCommiAmt.Text.Equals(String.Empty) AndAlso ddlCommRateXcd.SelectedValue.Equals("YESNO-N")) Then
            ElitaPlusPage.SetLabelError(lblCommiPerct)
            ElitaPlusPage.SetLabelError(lblCommiAmt)
            Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_COMM_PERC_AMT_ERR)
        End If
    End Sub

    Private Sub DisableCommPerAmtTxt()
        If (txtCommiAmt.Text.Equals(String.Empty) AndAlso txtCommiPerct.Text.Equals(String.Empty)) Then
            txtCommiAmt.Enabled = True
            txtCommiPerct.Enabled = True
        ElseIf (txtCommiPerct.Text.Equals(String.Empty) AndAlso ddlCommRateXcd.SelectedValue.Equals("YESNO-N")) Then
            txtCommiPerct.Enabled = False
        ElseIf (txtCommiAmt.Text.Equals(String.Empty) AndAlso ddlCommRateXcd.SelectedValue.Equals("YESNO-N")) Then
            txtCommiAmt.Enabled = False
        End If
    End Sub


#End Region

#Region "Business Part"

    Private Function IsDirtyBO() As Boolean
        Dim bIsDirty As Boolean = True

        Try
            With TheCommPlanExt
                PopulateBOsFromForm()
                bIsDirty = .IsDirty
            End With
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            MasterPage.MessageController.AddError(COMMPLANEXTRACT_LIST_FORM001)
            MasterPage.MessageController.AddError(ex.Message, False)
            MasterPage.MessageController.Show()
        End Try
        Return bIsDirty
    End Function

    Private Function ApplyChanges() As Boolean

        Try

            PopulateBOsFromForm()
            If TheCommPlanExt.IsDirty() Then
                ValidateDates()
                ValidateCommPerandAmt()
                State.MyBO.CommissionPlanId = State.InputParameters.CommPlanId
                State.MyBO.Save()
                State.boChanged = True
                If State.IsCommPlanExtNew = True Then
                    State.IsCommPlanExtNew = False
                End If
                PopulateAll()
                PopulateFormFromBOs()
                EnableDisableCommPerAmt()
                SetButtonsState(State.IsCommPlanExtNew)
                MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
            Else
                MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Function


    Public Sub ValidateDates()

        If txtCommiExpDate.Text.Trim() <> String.Empty AndAlso txtCommiEffectDate.Text.Trim() <> String.Empty Then
            If CDate(txtCommiEffectDate.Text) >= CDate(txtCommiExpDate.Text) Then
                ElitaPlusPage.SetLabelError(lblCommiEffectDate)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_EFFEC_DATE_ERR)
            End If
        End If
    End Sub

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
        BindBOPropertyToLabel(State.MyBO, "Code", lblCommPlanCode)
        BindBOPropertyToLabel(State.MyBO, "Description", lblCommPlanDescr)
        BindBOPropertyToLabel(State.MyBO, "EffectiveDate", lblCommiEffectDate)
        BindBOPropertyToLabel(State.MyBO, "ExpirationDate", lblCommiExpDate)
        BindBOPropertyToLabel(State.MyBO, "CycleFrequencyXcd", lblCyclFreqXcd)
        BindBOPropertyToLabel(State.MyBO, "CycleRunDay", lblCyclRunDay)
        BindBOPropertyToLabel(State.MyBO, "CycleCutOffDay", lblCyclCutOffDay)
        BindBOPropertyToLabel(State.MyBO, "CycleCutOffSourceXcd", lblCyclSrcXcd)
        BindBOPropertyToLabel(State.MyBO, "AmountSourceXcd", lblAmtXcd)
        BindBOPropertyToLabel(State.MyBO, "CommTitleXcd", lblCommiExtType)
        BindBOPropertyToLabel(State.MyBO, "CommAtRateXcd", lblCommRateXcd)
        BindBOPropertyToLabel(State.MyBO, "SequenceNumber", lblSeqNumber)
        BindBOPropertyToLabel(State.MyBO, "CommissionPercentage", lblCommiPerct)
        BindBOPropertyToLabel(State.MyBO, "CommissionAmount", lblCommiAmt)
    End Sub

    Private Sub ClearLabelsErrSign()
        ClearLabelErrSign(lblCommPlanCode)
        ClearLabelErrSign(lblCommPlanDescr)
        ClearLabelErrSign(lblCommiEffectDate)
        ClearLabelErrSign(lblCommiExpDate)
        ClearLabelErrSign(lblCyclFreqXcd)
        ClearLabelErrSign(lblCyclRunDay)
        ClearLabelErrSign(lblCyclCutOffDay)
        ClearLabelErrSign(lblCyclSrcXcd)
        ClearLabelErrSign(lblAmtXcd)
        ClearLabelErrSign(lblCommiExtType)
        ClearLabelErrSign(lblSeqNumber)
        ClearLabelErrSign(lblCommiPerct)
        ClearLabelErrSign(lblCommiAmt)
    End Sub

    Public Shared Sub SetLabelColor(lbl As Label)
        lbl.ForeColor = Color.Black
    End Sub

#End Region

#End Region

End Class