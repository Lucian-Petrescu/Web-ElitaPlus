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
        State.InputParameters = CType(Me.CallingParameters, Parameters)

        If State.InputParameters IsNot Nothing Then
            Me.State.moIsNewCommPlanExt = State.InputParameters.CommPlanExtId
        End If

        If Me.State.moIsNewCommPlanExt.Equals(Guid.Empty) Then
            Me.State.IsCommPlanExtNew = True
            BindBoPropertiesToLabels()
            Me.AddLabelDecorations(TheCommPlanExt)
            ClearAll()
            PopulateAll()
        Else
            Me.State.IsCommPlanExtNew = False
            BindBoPropertiesToLabels()
            Me.AddLabelDecorations(TheCommPlanExt)
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
        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal oCommissPlanId As Guid, ByVal hasDataChanged As Boolean)
            Me.LastOperation = LastOp
            Me.moCommissPlanId = oCommissPlanId
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class

#End Region

#Region "Parameters"
    Public Class Parameters
        Public CommPlanId As Guid
        Public CommPlanExtId As Guid
        Public Sub New(ByVal commplanid As Guid, ByVal commplanextid As Guid)
            Me.CommPlanId = commplanid
            Me.CommPlanExtId = commplanextid
        End Sub
    End Class
#End Region

#Region "Properties"
    Private ReadOnly Property TheCommPlanExt As CommPlanExtract

        Get
            If Me.State.MyBO Is Nothing Then
                If Me.State.IsCommPlanExtNew = True Then
                    ' For creating, inserting
                    Me.State.MyBO = New CommPlanExtract
                    Me.State.moIsNewCommPlanExt = Me.State.MyBO.Id
                Else
                    ' For updating, deleting
                    Me.State.MyBO = New CommPlanExtract(Me.State.moIsNewCommPlanExt)
                End If
            End If

            Return Me.State.MyBO
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

                If Me.State.IsCommPlanExtNew = True Then
                    CreateNew()
                End If
                PopulateFormFromBOs()
                Me.AddCalendar(Me.BtnCommiEffectDate, Me.txtCommiEffectDate)
                Me.AddCalendar(Me.BtnCommiExpDate, Me.txtCommiExpDate)
            End If

            EnableDisableCommPerAmt()
            BindBoPropertiesToLabels()
            CheckIfComingFromConfirm()
            If Not Me.IsPostBack Then
                Me.AddLabelDecorations(TheCommPlanExt)
            End If
            DisableCommPerAmtTxt()

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
                State.InputParameters = CType(Me.CallingParameters, Parameters)
                State.MyBO = New CommPlanExtract(Me.State.InputParameters.CommPlanExtId)
            Else
                Me.State.IsCommPlanExtNew = True
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
    End Sub

    Private Sub GoBack()
        Dim retType As New CommPlanExtractForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO.CommissionPlanId, Me.State.boChanged)
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
            If Not Me.State.IsCommPlanExtNew Then
                'Reload from the DB
                Me.State.MyBO = New CommPlanExtract(Me.State.MyBO.Id)
            ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                'It was a new with copy
                Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
            Else
                CreateNew()
            End If
            PopulateAll()
            PopulateFormFromBOs()
            Me.SetButtonsState(Me.State.IsCommPlanExtNew)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub CreateNew()
        Me.State.ScreenSnapShotBO = Nothing
        Me.State.IsCommPlanExtNew = True
        Me.State.MyBO = New CommPlanExtract
        ClearAll()
        Me.SetButtonsState(True)
        Me.PopulateAll()
        Me.PopulateBOsFromForm()
        PopulateFormFromBOs()
        EnableDisableCommPerAmt()
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

        Dim newObj As New CommPlanExtract
        newObj.Copy(TheCommPlanExt)

        Me.State.MyBO = newObj
        Me.State.moIsNewCommPlanExt = Guid.Empty
        Me.State.IsCommPlanExtNew = True

        Me.SetButtonsState(True)
        PopulateFormFromBOs()
        EnableDisableCommPerAmt()

        'create the backup copy
        Me.State.ScreenSnapShotBO = New CommPlanExtract
        Me.State.ScreenSnapShotBO.Copy(TheCommPlanExt)

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
            Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, Me.State.MyBO.CommissionPlanId, Me.State.HasDataChanged))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
        If (Not Me.State Is Nothing) Then
            If (Not Me.State Is Nothing) Then
                Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("COMM_PLAN_EXTRACT")
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("COMM_PLAN_EXTRACT")
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
            Me.MasterPage.MessageController.AddError(COMMPLANEXTRACT_LIST_FORM001)
            Me.MasterPage.MessageController.AddError(ex.Message, False)
            Me.MasterPage.MessageController.Show()
        End Try
    End Sub

    Private Sub PopulateFormFromBOs()
        Try
            With TheCommPlanExt
                BindSelectItem(.AmountSourceXcd, ddlAmtXcd)
                BindSelectItem(.CommExtractPackageId.ToString, ddlCommiExtType)
                BindSelectItem(.CommAtRateXcd, ddlCommRateXcd)
                Me.PopulateControlFromBOProperty(Me.txtCommPlanCode, .Code)
                Me.PopulateControlFromBOProperty(Me.txtCommPlanDesc, .Description)
                Me.PopulateControlFromBOProperty(Me.txtCommiEffectDate, .EffectiveDate)
                Me.PopulateControlFromBOProperty(Me.txtCommiExpDate, .ExpirationDate)
                Me.PopulateControlFromBOProperty(Me.txtCyclRunDay, .CycleRunDay)
                Me.PopulateControlFromBOProperty(Me.txtCyclCutOffDay, .CycleCutOffDay)
                Me.PopulateControlFromBOProperty(Me.txtlblSeqNumber, .SequenceNumber)
                Me.PopulateControlFromBOProperty(Me.txtCommiPerct, .CommissionPercentage)
                Me.PopulateControlFromBOProperty(Me.txtCommiAmt, .CommissionAmount)
                Me.PopulateControlFromBOProperty(Me.txtCyclFreXcd, .CycleFrequencyXcd)
                Me.PopulateControlFromBOProperty(Me.txtCyclSrcXcd, .CycleCutOffSourceXcd)
            End With
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub PopulateAll()
        PopulateDropDowns()
    End Sub

    Protected Sub PopulateBOsFromForm()

        With Me.TheCommPlanExt
            Me.PopulateBOProperty(Me.State.MyBO, "Code", Me.txtCommPlanCode)
            Me.PopulateBOProperty(Me.State.MyBO, "Description", Me.txtCommPlanDesc)
            Me.PopulateBOProperty(Me.State.MyBO, "EffectiveDate", Me.txtCommiEffectDate)
            Me.PopulateBOProperty(Me.State.MyBO, "ExpirationDate", Me.txtCommiExpDate)
            Me.PopulateBOProperty(Me.State.MyBO, "CycleRunDay", Me.txtCyclRunDay)
            Me.PopulateBOProperty(Me.State.MyBO, "CycleCutOffDay", Me.txtCyclCutOffDay)
            Me.PopulateBOProperty(Me.State.MyBO, "SequenceNumber", Me.txtlblSeqNumber)
            Me.PopulateBOProperty(Me.State.MyBO, "CommissionPercentage", Me.txtCommiPerct)
            Me.PopulateBOProperty(Me.State.MyBO, "CommissionAmount", Me.txtCommiAmt)
            Me.PopulateBOProperty(Me.State.MyBO, "AmountSourceXcd", Me.ddlAmtXcd, False, True)
            Me.PopulateBOProperty(Me.State.MyBO, "CommExtractPackageId", Me.ddlCommiExtType)
            Me.PopulateBOProperty(Me.State.MyBO, "CommTitleXcd", GetSelectedDescription(ddlCommiExtType))
            Me.PopulateBOProperty(Me.State.MyBO, "CommAtRateXcd", Me.ddlCommRateXcd, False, True)

        End With
        If Me.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If

    End Sub
#End Region

#Region "Gui-Validation"

    Private Sub SetButtonsState(ByVal bIsNew As Boolean)
        ControlMgr.SetEnableControl(Me, btnNew_WRITE, Not bIsNew)
        ControlMgr.SetEnableControl(Me, btnCopy_WRITE, Not bIsNew)
        ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
    End Sub

    Private Sub EnableDisableCommPerAmt()
        If Me.ddlCommRateXcd.SelectedValue.Equals("YESNO-Y") Then

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
        If (txtCommiPerct.Text.Equals(String.Empty) And txtCommiAmt.Text.Equals(String.Empty) AndAlso Me.ddlCommRateXcd.SelectedValue.Equals("YESNO-N")) Then
            ElitaPlusPage.SetLabelError(lblCommiPerct)
            ElitaPlusPage.SetLabelError(lblCommiAmt)
            Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_COMM_PERC_AMT_ERR)
        End If
    End Sub

    Private Sub DisableCommPerAmtTxt()
        If (txtCommiAmt.Text.Equals(String.Empty) And txtCommiPerct.Text.Equals(String.Empty)) Then
            txtCommiAmt.Enabled = True
            txtCommiPerct.Enabled = True
        ElseIf (txtCommiPerct.Text.Equals(String.Empty) And Me.ddlCommRateXcd.SelectedValue.Equals("YESNO-N")) Then
            txtCommiPerct.Enabled = False
        ElseIf (txtCommiAmt.Text.Equals(String.Empty) And Me.ddlCommRateXcd.SelectedValue.Equals("YESNO-N")) Then
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
            Me.MasterPage.MessageController.AddError(COMMPLANEXTRACT_LIST_FORM001)
            Me.MasterPage.MessageController.AddError(ex.Message, False)
            Me.MasterPage.MessageController.Show()
        End Try
        Return bIsDirty
    End Function

    Private Function ApplyChanges() As Boolean

        Try

            Me.PopulateBOsFromForm()
            If TheCommPlanExt.IsDirty() Then
                ValidateDates()
                ValidateCommPerandAmt()
                Me.State.MyBO.CommissionPlanId = State.InputParameters.CommPlanId
                Me.State.MyBO.Save()
                Me.State.boChanged = True
                If Me.State.IsCommPlanExtNew = True Then
                    Me.State.IsCommPlanExtNew = False
                End If
                PopulateAll()
                PopulateFormFromBOs()
                EnableDisableCommPerAmt()
                Me.SetButtonsState(Me.State.IsCommPlanExtNew)
                Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
            Else
                Me.MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Function


    Public Sub ValidateDates()

        If txtCommiExpDate.Text.Trim() <> String.Empty And txtCommiEffectDate.Text.Trim() <> String.Empty Then
            If CDate(txtCommiEffectDate.Text) >= CDate(txtCommiExpDate.Text) Then
                ElitaPlusPage.SetLabelError(lblCommiEffectDate)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_EFFEC_DATE_ERR)
            End If
        End If
    End Sub

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
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Code", Me.lblCommPlanCode)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Description", Me.lblCommPlanDescr)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "EffectiveDate", Me.lblCommiEffectDate)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ExpirationDate", Me.lblCommiExpDate)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "CycleFrequencyXcd", Me.lblCyclFreqXcd)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "CycleRunDay", Me.lblCyclRunDay)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "CycleCutOffDay", Me.lblCyclCutOffDay)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "CycleCutOffSourceXcd", Me.lblCyclSrcXcd)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "AmountSourceXcd", Me.lblAmtXcd)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "CommTitleXcd", Me.lblCommiExtType)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "CommAtRateXcd", Me.lblCommRateXcd)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "SequenceNumber", Me.lblSeqNumber)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "CommissionPercentage", Me.lblCommiPerct)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "CommissionAmount", Me.lblCommiAmt)
    End Sub

    Private Sub ClearLabelsErrSign()
        Me.ClearLabelErrSign(lblCommPlanCode)
        Me.ClearLabelErrSign(lblCommPlanDescr)
        Me.ClearLabelErrSign(lblCommiEffectDate)
        Me.ClearLabelErrSign(lblCommiExpDate)
        Me.ClearLabelErrSign(lblCyclFreqXcd)
        Me.ClearLabelErrSign(lblCyclRunDay)
        Me.ClearLabelErrSign(lblCyclCutOffDay)
        Me.ClearLabelErrSign(lblCyclSrcXcd)
        Me.ClearLabelErrSign(lblAmtXcd)
        Me.ClearLabelErrSign(lblCommiExtType)
        Me.ClearLabelErrSign(lblSeqNumber)
        Me.ClearLabelErrSign(lblCommiPerct)
        Me.ClearLabelErrSign(lblCommiAmt)
    End Sub

    Public Shared Sub SetLabelColor(ByVal lbl As Label)
        lbl.ForeColor = Color.Black
    End Sub

#End Region

#End Region

End Class