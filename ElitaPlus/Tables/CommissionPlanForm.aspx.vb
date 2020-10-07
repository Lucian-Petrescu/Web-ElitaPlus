Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.DALObjects
Imports System.Globalization
Imports Assurant.ElitaPlus.Business

Namespace Tables

    Partial Class CommissionPlanForm
        Inherits ElitaPlusSearchPage
        ' Inherits System.Web.UI.Page
#Region "Page State"

#Region "MyState"
        Class MyState
            Public MyBo As CommPlan
            Public MyBoDist As CommPlanDistribution
            Public moDistributionList() As CommPlanDistribution
            Public moCommPlanId As Guid = Guid.Empty
            Public moCommPlanDistId As Guid = Guid.Empty
            Public moCommPlanDistPlanId As Guid = Guid.Empty
            Public LastErrMsg As String
            Public IsCommPlanDistNew As Boolean = False
            Public IsComingFromSavePlan As Boolean = False
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public boChanged As Boolean = False
            Public IsNewCloneCopyClicked As Boolean = False
            Public IsCommPerGreaterThanHundred As Boolean
            Public IsPmComCombination As Boolean
            Public IsAmountAndPercentBothPresent As Boolean
            Public IsDiffSelectedTwice As Boolean
            Public IsDiffNotSelectedOnce As Boolean
            Public IsBucketIncomingSelected As Boolean
            Public IsDealerConfiguredForSourceXcd As Boolean = False
            Public IsCompanyConfiguredForSourceXcd As Boolean = False
            Public IsIgnorePremiumSetYesForContract As Boolean = False
            Public IsComingFromPlanCodeDuplicate As Boolean = False
            Public IsComingFromDateOverLap As Boolean = False
            Public PlaListDV As DataView = Nothing
            Public IsDealerExistForSelectedPlan As Boolean = True
            Public IsPlanNew As Boolean = False
            Public IsNewWithCopy As Boolean = False
            Public IsUndo As Boolean = False
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
            State.moCommPlanId = CType(CallingParameters, Guid)

            If State.moCommPlanId.Equals(Guid.Empty) Then
                State.IsCommPlanDistNew = True
                ClearPlan()
                SetPlanButtonsState(True)
                PopulatePlanFields()
                PopulateDistributionList()
                TheDealerControl.ChangeEnabledControlProperty(True)
                EnableNewDistributionButtons(False)
            Else
                State.IsCommPlanDistNew = False
                SetPlanButtonsState(False)
                PopulatePlanFields()
                PopulateDistributionList()
                TheDealerControl.ChangeEnabledControlProperty(False)
                EnableNewDistributionButtons(True)
            End If
            If Not TheDealerControl.SelectedGuid.Equals(Guid.Empty) Then
            End If
        End Sub
#End Region


#Region "Constants- Plan"

        Public Const URL As String = "CommissionPlanForm.aspx"

        ' Property Name
        Public Const NOTHING_SELECTED As Integer = 0
        Public Const DEALER_ID_PROPERTY As String = "DealerId"
        Public Const EFFECTIVE_DATE_PROPERTY As String = "EffectiveDate"
        Public Const EXPIRATION_DATE_PROPERTY As String = "ExpirationDate"
        Public Const REFERENCE_SOURCE_PROPERTY As String = "ReferenceSource"
        Public Const CODE_PROPERTY As String = "Code"
        Public Const DESCRIPTION_PROPERTY As String = "Description"
        Public Const PAGETITLE As String = "COMMISSION_PLAN"
        Public Const PAGETAB As String = "TABLES"
        Public Const Payee_Type_Dealer_Group As String = "1"
        Public Const Payee_Type_Dealer As String = "2"
        Public Const Payee_Type_Comm_Entity As String = "4"

        Private Const LABEL_DEALER As String = "DEALER"

#End Region

#Region "Constants-Distribution"

        Private Const COL_COMMISSION_PLAN_DIST_ID_IDX As Integer = 2
        Private Const COL_COMMISSION_PLAN_ID_IDX As Integer = 3
        Private Const COL_PAYEE_TYPE_XCD_IDX As Integer = 4
        Private Const COL_ENTITY_ID_IDX As Integer = 5
        Private Const COL_COMMISSION_AMOUNT_IDX As Integer = 6
        Private Const COL_COMMISSION_PERCENTAGE_IDX As Integer = 7
        Private Const COL_COMMISSIONS_SOURCE_XCD_IDX As Integer = 8
        Private Const COL_ACT_ENT_SOURCE_XCD_IDX As Integer = 9
        Private Const COL_POSITION_IDX As Integer = 10

        Private Const PROPERTY_PAYEE_TYPE_XCD As String = "PayeeTypeXcd"
        Private Const PROPERTY_ENTITY_ID As String = "EntityId"
        Private Const PROPERTY_COMM_AMT As String = "CommissionAmount"
        Private Const PROPERTY_COMM_PER As String = "CommissionPercent"
        Private Const PROPERTY_COMMISSIONS_XCD As String = "CommissionsPercentSourceXcd"
        Private Const PROPERTY_ACT_ENT_XCD As String = "ActEntitySourceXcd"
        Private Const PROPERTY_POSITION As String = "Position"

        'Actions
        Private Const ACTION_NONE As String = "ACTION_NONE"
        Private Const ACTION_SAVE As String = "ACTION_SAVE"
        Private Const ACTION_CANCEL_DELETE As String = "ACTION_CANCEL_DELETE"
        Private Const ACTION_EDIT As String = "ACTION_EDIT"
        Private Const ACTION_NEW As String = "ACTION_NEW"

        ' DataView Elements
        Private Const DB_COMM_PLAN_DIST_ID As Integer = 0
        Private Const DB_COMM_PLAN_ID As Integer = 1
        Private Const DB_PAYEE_TYPE_XCD As Integer = 2
        Private Const DB_ENTITY_ID As Integer = 3
        Private Const DB_COMM_AMT As Integer = 4
        Private Const DB_COMM_PER As Integer = 5
        Private Const DB_COMM_SRC_XCD As Integer = 6
        Private Const DB_AMT_ENTITY_SRC_XCD As Integer = 7
        Private Const DB_POSITION As Integer = 8

#End Region

#Region "Variables"
        Private moComPlanExpirationData As CommPlanDataExp
        Private moExpirationData As CommPlanData
        Private moDistribution As CommPlanDistribution
        Private mbIsNewRate As Boolean

#End Region

#Region "Properties"

        Private ReadOnly Property TheCommPlan() As CommPlan
            Get
                If State.MyBo Is Nothing Then
                    If State.IsCommPlanDistNew = True Then
                        ' For creating, inserting
                        State.MyBo = New CommPlan
                        State.moCommPlanId = State.MyBo.Id
                    Else
                        ' For updating, deleting
                        State.MyBo = New CommPlan(State.moCommPlanId)
                    End If
                End If
                BindBoPropertiesToLabels(State.MyBo)
                Return State.MyBo
            End Get
        End Property

        Private ReadOnly Property TheCommPlanDist() As CommPlanDistribution
            Get
                If State.MyBoDist Is Nothing Then
                    If State.IsCommPlanDistNew = True Then
                        ' For creating, inserting
                        State.MyBoDist = New CommPlanDistribution
                        State.moCommPlanDistId = State.MyBoDist.Id
                        moDistribution = New CommPlanDistribution
                        DistributionId = moDistribution.Id.ToString
                    Else
                        ' For updating, deleting
                        State.MyBoDist = New CommPlanDistribution(State.moCommPlanDistId)
                        If DistributionId = "" Then
                            DistributionId = Guid.Empty.ToString
                        End If

                        moDistribution = New CommPlanDistribution(State.moCommPlanDistId)
                    End If
                End If
                Return State.MyBoDist
            End Get
        End Property

        Private ReadOnly Property ExpirationCount() As Integer
            Get
                If moExpirationData Is Nothing Then
                    moExpirationData = New CommPlanData
                    moExpirationData.dealerId = TheDealerControl.SelectedGuid 'Me.GetSelectedItem(moDealerDrop_WRITE)
                End If
                Return TheCommPlan.ExpirationCount(moExpirationData)
            End Get
        End Property

        Private ReadOnly Property ExpirationOverlapping() As Integer
            Get
                If moComPlanExpirationData Is Nothing Then
                    moComPlanExpirationData = New CommPlanDataExp
                    moComPlanExpirationData.dealerId = TheDealerControl.SelectedGuid 'Me.GetSelectedItem(moDealerDrop_WRITE)
                    moComPlanExpirationData.expirationDate = Convert.ToDateTime(moExpirationText_WRITE.Text)
                    '     moComPlanExpirationData.expirationDate = 
                End If
                Return TheCommPlan.ExpirationOverlapping(moComPlanExpirationData)
            End Get
        End Property

        Private ReadOnly Property MaxExpiration() As Date
            Get
                If moExpirationData Is Nothing Then
                    moExpirationData = New CommPlanData
                    moExpirationData.dealerId = TheDealerControl.SelectedGuid
                End If
                Return TheCommPlan.MaxExpiration(moExpirationData)
            End Get
        End Property

        Public ReadOnly Property TheDealerControl() As MultipleColumnDDLabelControl
            Get
                If multipleDropControl Is Nothing Then
                    multipleDropControl = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
                End If
                Return multipleDropControl
            End Get
        End Property

        Private Property DistributionId() As String
            Get
                Return moCoverageRateIdLabel.Text
            End Get
            Set(Value As String)
                moCoverageRateIdLabel.Text = Value
            End Set
        End Property

#End Region

#Region "Handlers"

        Protected WithEvents moCoverageEditPanel As System.Web.UI.WebControls.Panel
#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        'Protected WithEvents moErrorController As ErrorController
        'Protected WithEvents moErrorControllerGrid As ErrorController

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Handlers-Init"

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                MasterPage.MessageController.Clear_Hide()
                ClearLabelsErrSign()
                ClearGridHeaders(moGridView)

                If Not Page.IsPostBack Then
                    State.IsNewCloneCopyClicked = False
                    MasterPage.MessageController.Clear()
                    MasterPage.UsePageTabTitleInBreadCrum = False
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")
                    UpdateBreadCrum()
                    SetFormTab(PAGETAB)
                    SetGridItemStyleColor(moGridView)
                    TranslateGridHeader(moGridView)
                    SetStateProperties()

                    'US-521672
                    moGridView.Visible = True

                    'US-521672
                    SetGridSourceXcdLabelFromBo()

                    AddControlMsg(btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO,
                                     MSG_TYPE_CONFIRM, True)
                    AddCalendar(BtnEffectiveDate_WRITE, moEffectiveText_WRITE)
                    AddCalendar(BtnExpirationDate_WRITE, moExpirationText_WRITE)
                    AddCalendarNewWithDisableBeforeDate(BtnExpirationDate_WRITE, moExpirationText_WRITE, "", "Y", System.DateTime.Today)
                Else
                    moGridView.Visible = True
                    'US-521672
                    SetGridSourceXcdLabelFromBo()
                    CheckIfComingFromConfirm()
                End If

                ControlMgr.SetVisibleControl(Me, btnBack, True)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub GoBack()
            Dim retType As New CommissionPlanSearchForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.moCommPlanId, State.boChanged)
            ReturnToCallingPage(retType)
        End Sub

        Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
            Try
                If IsDirtyPlanBO() = True Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM,
                                            HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    GoBack()
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                GoBack()
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub SavePlanChanges()
            If ApplyPlanChanges() = True Then
                State.boChanged = True
                PopulatePlanFields()
                State.IsCommPlanDistNew = False
                SetPlanButtonsState(False)
            End If
        End Sub

        Private Sub btnSave_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnSave_WRITE.Click
            Try
                SavePlanChanges()
                RePopulateDistributionListForPlan()
                SetGridSourceXcdLabelFromBo()
                'ControlMgr.SetEnableControl(Me, moEffectiveText_WRITE, False)

                If (String.IsNullOrWhiteSpace(TextBoxCode.Text) Or String.IsNullOrWhiteSpace(TextBoxDescription.Text)) Then
                    EnableNewDistributionButtons(False)
                Else
                    EnableNewDistributionButtons(True)
                End If


                If State.IsComingFromPlanCodeDuplicate = True Or State.IsComingFromDateOverLap = True Then
                    EnableNewDistributionButtons(False)
                    State.IsComingFromPlanCodeDuplicate = False
                    State.IsComingFromDateOverLap = False
                End If

            Catch ex As Exception
                RePopulateDistributionListForPlan()
                SetGridSourceXcdLabelFromBo()
                SetGridControls(moGridView, True)
                EnableDisableControls(moCoverageEditPanel, True)
                SetGridSourceXcdLabelFromBo()
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try
                'RebindPlanMyBo()
                PopulatePlanFields()
                'ClearPlanCodeDescription()
                RePopulateDistributionListForPlan()
                SetGridSourceXcdLabelFromBo()
                If Not moGridView.Rows.Count > 0 Then
                    EnableNewDistributionButtons(False)
                End If

                If State.IsNewCloneCopyClicked = True Then
                    State.moCommPlanId = CType(CallingParameters, Guid)

                    If State.moCommPlanId.Equals(Guid.Empty) Then
                        State.IsCommPlanDistNew = True
                    Else
                        State.IsCommPlanDistNew = False
                    End If

                    TheDealerControl.ChangeEnabledControlProperty(True)
                    moGridView.Visible = True
                    btnBack.Visible = True
                    EnableDisableControls(moCoverageEditPanel, False)
                    EnableDateFields()
                    EnablePlanCodeDescFields(True)
                    EnableExpiration(True)
                    State.IsNewCloneCopyClicked = False
                End If

            Catch ex As Exception
                SetGridSourceXcdLabelFromBo()
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnDelete_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete_WRITE.Click
            Try
                If DeletePlan() = True Then
                    State.boChanged = True
                    GoBack()
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Sub RebindPlanMyBo()
            If State.IsCommPlanDistNew = True Then
                ' For creating, inserting
                State.MyBo = New CommPlan
                State.moCommPlanId = State.MyBo.Id
            Else
                ' For updating, deleting
                State.MyBo = New CommPlan(State.moCommPlanId)
            End If
        End Sub
        Private Sub CreateNew()
            State.MyBo = Nothing
            State.moCommPlanId = Guid.Empty
            State.IsCommPlanDistNew = True
            ClearPlan()
            SetPlanButtonsState(True)
            PopulatePlanFields()
            TheDealerControl.ChangeEnabledControlProperty(True)
        End Sub

        Private Sub btnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNew_WRITE.Click
            Try
                If IsDirtyPlanBO() = True Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
                Else
                    CreateNew()
                End If
                EnableNewDistributionButtons(False)
                EnableExpiration(True)
                EnablePlanCodeDescFields(True)
            Catch ex As Exception
                SetGridSourceXcdLabelFromBo()
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CreateNewCopy()
            Dim newObj As New CommPlan
            SetGridControls(moGridView, True)
            State.MyBo = Nothing
            newObj.Copy(TheCommPlan)
            State.IsCommPlanDistNew = True
            State.MyBo = newObj

            EnableDateFields()
            SetPlanButtonsState(True)
            ClearDistributionGrid()
            State.IsNewCloneCopyClicked = True
        End Sub

        Private Sub btnCopy_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnCopy_WRITE.Click
            Try
                If IsDirtyPlanBO() = True Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
                Else
                    CreateNewCopy()
                End If
            Catch ex As Exception
                SetGridSourceXcdLabelFromBo()
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Handlers-DropDowns"

        Private Sub OnFromDrop_Changed(fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
           Handles multipleDropControl.SelectedDropChanged
            Try
                EnableDateFields()
                moGridView.Visible = True
                btnBack.Visible = True
                EnablePlanCodeDescFields(True)
                EnableExpiration(True)
                EnableDisableControls(moCoverageEditPanel, False)

                If CheckDealerExistForCommissionPlan() = "N" Then
                    State.IsDealerExistForSelectedPlan = False
                    EnableEffective(True)
                Else
                    State.IsDealerExistForSelectedPlan = True
                    EnableEffective(False)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Handlers-Labels"
        Protected Sub BindBoPropertiesToLabels(oPlan As CommPlan)
            BindBOPropertyToLabel(oPlan, DEALER_ID_PROPERTY, TheDealerControl.CaptionLabel)
            BindBOPropertyToLabel(oPlan, EFFECTIVE_DATE_PROPERTY, moEffectiveLabel)
            BindBOPropertyToLabel(oPlan, EXPIRATION_DATE_PROPERTY, moExpirationLabel)
            BindBOPropertyToLabel(oPlan, CODE_PROPERTY, LabelCode)
            BindBOPropertyToLabel(oPlan, DESCRIPTION_PROPERTY, LabelDescription)
        End Sub

        Protected Sub BindBoPropertiesToLabels(oPlan As CommPlanDistribution)
            BindBOPropertyToLabel(oPlan, DEALER_ID_PROPERTY, TheDealerControl.CaptionLabel)
            BindBOPropertyToLabel(oPlan, EFFECTIVE_DATE_PROPERTY, moEffectiveLabel)
            BindBOPropertyToLabel(oPlan, EXPIRATION_DATE_PROPERTY, moExpirationLabel)
            BindBOPropertyToLabel(oPlan, CODE_PROPERTY, LabelCode)
            BindBOPropertyToLabel(oPlan, DESCRIPTION_PROPERTY, LabelDescription)
        End Sub

        Public Sub ClearLabelsErrSign()
            ClearLabelErrSign(TheDealerControl.CaptionLabel)
            ClearLabelErrSign(moEffectiveLabel)
            ClearLabelErrSign(moExpirationLabel)
            ClearLabelErrSign(LabelCode)
            ClearLabelErrSign(LabelDescription)
        End Sub
#End Region

#End Region

#Region "Button-Management"

        Private Sub SetPlanButtonsState(bIsNew As Boolean)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, Not bIsNew)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, Not bIsNew)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, Not bIsNew)
        End Sub

        Private Sub EnableEffective(bIsEnable As Boolean)
            'ControlMgr.SetVisibleControl(Me, moEffectiveText_WRITE, bIsEnable)
            ControlMgr.SetEnableControl(Me, moEffectiveText_WRITE, bIsEnable)

            ControlMgr.SetVisibleControl(Me, BtnEffectiveDate_WRITE, bIsEnable)
            ControlMgr.SetEnableControl(Me, BtnEffectiveDate_WRITE, bIsEnable)
        End Sub

        Private Sub EnableExpiration(bIsEnable As Boolean)
            ControlMgr.SetEnableControl(Me, moExpirationText_WRITE, bIsEnable)
            ControlMgr.SetVisibleControl(Me, BtnExpirationDate_WRITE, bIsEnable)
        End Sub

        Private Sub EnablePlanCodeDescFields(IsEnable As Boolean)
            ControlMgr.SetEnableControl(Me, TextBoxCode, IsEnable)
            ControlMgr.SetEnableControl(Me, TextBoxDescription, IsEnable)
        End Sub

        Private Sub EnableDateFields()
            Select Case ExpirationCount
                Case 0  ' New Record
                    'EnableEffective(True)
                    EnableExpiration(True)
                    'Yesterday
                    moEffectiveText_WRITE.Text = Date.Now().AddDays(-1).ToString("dd-MMM-yyyy", CultureInfo.CurrentCulture)
                    ' Next Year
                    moExpirationText_WRITE.Text = Date.Now().AddYears(1).ToString("dd-MMM-yyyy", CultureInfo.CurrentCulture)
                Case 1
                    If State.IsCommPlanDistNew = True Then
                        'New Record
                        ' Next Day MaxExpiration
                        moEffectiveText_WRITE.Text = MaxExpiration.AddDays(1).ToString("dd-MMM-yyyy", CultureInfo.CurrentCulture)
                        ' Next Year after MaxExpiration 
                        moExpirationText_WRITE.Text = MaxExpiration.AddYears(1).ToString("dd-MMM-yyyy", CultureInfo.CurrentCulture)
                        'EnableEffective(False)
                    Else
                        ' Modify the only record
                        'EnableEffective(True)
                    End If
                    ControlMgr.SetEnableControl(Me, BtnExpirationDate_WRITE, True)
                Case Else   ' There is more than one record
                    EnableExpiration(True)
                    If State.IsCommPlanDistNew = True Then
                        'New Record
                        ' Next Day MaxExpiration
                        moEffectiveText_WRITE.Text = MaxExpiration.AddDays(1).ToString("dd-MMM-yyyy", CultureInfo.CurrentCulture)
                        ' Next Year after MaxExpiration 
                        moExpirationText_WRITE.Text = MaxExpiration.AddYears(1).ToString("dd-MMM-yyyy", CultureInfo.CurrentCulture)
                    Else
                        Dim oMaxExpiration As String = GetDateFormattedString(MaxExpiration)
                        If moExpirationText_WRITE.Text <> oMaxExpiration Then
                            ' It is not the last Record
                            EnableExpiration(False)
                            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
                        End If
                    End If
                    'EnableEffective(False)
            End Select
        End Sub

#End Region

#Region "Populate"

        Private Sub PopulateDealer()

            Dim oDataView As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
            TheDealerControl.Caption = "* " + TranslationBase.TranslateLabelOrMessage(LABEL_DEALER)
            TheDealerControl.NothingSelected = True
            TheDealerControl.BindData(oDataView)
            TheDealerControl.AutoPostBackDD = True

            If State.IsCommPlanDistNew = True Then
                TheDealerControl.NothingSelected = True
            Else
                TheDealerControl.SelectedGuid = TheCommPlan.DealerId
            End If

        End Sub

        Private Sub PopulateDatesFromBO()
            PopulateControlFromBOProperty(moEffectiveText_WRITE, TheCommPlan.EffectiveDate)
            PopulateControlFromBOProperty(moExpirationText_WRITE, TheCommPlan.ExpirationDate)
            PopulateControlFromBOProperty(moExpirationText_WRITE, TheCommPlan.ExpirationDate)
        End Sub

        Private Sub PupulateCodeDescFromBO()
            PopulateControlFromBOProperty(TextBoxCode, TheCommPlan.Code)
            PopulateControlFromBOProperty(TextBoxDescription, TheCommPlan.Description)
        End Sub

        Private Sub ClearPlanCodeDescription()
            TextBoxCode.Text = String.Empty
            TextBoxDescription.Text = String.Empty
        End Sub
        Private Sub PopulatePlanFields()
            Try
                PopulateDealer()
                PopulateDatesFromBO()
                EnableDateFields()
                PupulateCodeDescFromBO()
                EnablePlanCodeDescFields(True)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

#End Region

#Region "Clear"

        Private Sub ClearDealer()
            If State.IsCommPlanDistNew = True Then
                TheDealerControl.SelectedIndex = 0
            Else
                TheDealerControl.SelectedGuid = TheCommPlan.DealerId
            End If

        End Sub

        Private Sub ClearPlan()
            ClearDealer()
            ClearDistributionGrid()
        End Sub

#End Region

#Region "Business Part"
        Private Sub UpdateBreadCrum()
            If (State IsNot Nothing) Then
                If (State IsNot Nothing) Then
                    MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(PAGETITLE)
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
                End If
            End If
        End Sub

        Private Sub PopulatePlanBOFromForm(oPlan As CommPlan)
            With oPlan
                ' DropDowns
                .DealerId = TheDealerControl.SelectedGuid 'Me.GetSelectedItem(moDealerDrop_WRITE)
                PopulateBOProperty(oPlan, REFERENCE_SOURCE_PROPERTY, "ELP_DEALER")

                If Not String.IsNullOrWhiteSpace(TextBoxCode.Text) Then
                    TextBoxCode.Text = TextBoxCode.Text.ToUpper()
                End If

                PopulateBOProperty(oPlan, CODE_PROPERTY, TextBoxCode)
                PopulateBOProperty(oPlan, DESCRIPTION_PROPERTY, TextBoxDescription)
                ' Texts
                PopulateBOProperty(oPlan, EFFECTIVE_DATE_PROPERTY, moEffectiveText_WRITE)
                PopulateBOProperty(oPlan, EXPIRATION_DATE_PROPERTY, moExpirationText_WRITE)

            End With

            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Private Function IsDirtyPlanBO() As Boolean
            Dim bIsDirty As Boolean = True
            Dim oPlan As CommPlan

            oPlan = TheCommPlan
            With oPlan
                PopulatePlanBOFromForm(State.MyBo)
                bIsDirty = .IsDirty
            End With
            Return bIsDirty
        End Function

        Private Function ApplyPlanChanges() As Boolean
            Dim bIsOk As Boolean = True
            Dim oPlan As CommPlan
            Dim datesOverlapFlag As String
            Try
                If IsDirtyPlanBO() = True Then
                    oPlan = TheCommPlan
                    datesOverlapFlag = oPlan.CheckDatesOverLap(oPlan.DealerId, oPlan.ExpirationDate, oPlan.Id)
                    If datesOverlapFlag = "N" Then
                        oPlan.Save()
                        If TextBoxCode.Text.ToUpper().Equals(oPlan.Code.ToUpper()) Then
                            MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
                        Else
                            'Me.MasterPage.MessageController.AddError(Message.MSG_DUPLICATE_PLAN_CODE_NOT_ALLOWED, True)
                            State.IsComingFromPlanCodeDuplicate = True
                            Throw New DataNotFoundException
                        End If
                        EnableNewDistributionButtons(True)
                    Else
                        State.IsComingFromDateOverLap = True
                        Throw New GUIException(Message.MSG_EXPIRATION_DATE_IS_OVERLAPPING_WITH_OTHER_PLAN, Assurant.ElitaPlus.Common.ErrorCodes.MSG_EXPIRATION_DATE_IS_OVERLAPPING)
                    End If
                Else
                    MasterPage.MessageController.AddError(Message.MSG_RECORD_NOT_SAVED, True)
                End If
                State.IsCommPlanDistNew = False
                SetPlanButtonsState(False)
            Catch ex As Exception
                If ex.Message.ToUpper = "ERR_BO_DATA_NOT_FOUND" Then ' ErrorTypes.ERROR_BO Then
                    Try
                        State.IsComingFromPlanCodeDuplicate = True
                        Throw New GUIException(Message.MSG_DUPLICATE_PLAN_CODE_NOT_ALLOWED, Assurant.ElitaPlus.Common.ErrorCodes.MSG_DUPLICATE_PLAN_CODE_NOT_ALLOWED)
                    Catch ex1 As Exception
                        HandleErrors(ex1, MasterPage.MessageController)
                        RebindPlanMyBo()
                        bIsOk = False
                    End Try
                Else
                    HandleErrors(ex, MasterPage.MessageController)
                    RebindPlanMyBo()

                    If State.IsDealerExistForSelectedPlan = False Then
                        EnableEffective(True)
                    Else
                        EnableEffective(False)
                    End If
                    bIsOk = False
                End If

            End Try
            Return bIsOk
        End Function

        Private Function CheckDealerExistForCommissionPlan() As String
            Dim oPlan As CommPlan
            Dim dealerExistFlag As String = "Y"
            Try
                oPlan = TheCommPlan
                'dealerExistFlag = oPlan.CheckDealerExistForPlan(multipleDropControl.SelectedGuid)
                State.PlaListDV = CommPlan.getList(GetSearchParmDealerPlanList)

                If State.PlaListDV IsNot Nothing Then
                    If State.PlaListDV.Count = 0 Then
                        dealerExistFlag = "N"
                    End If
                End If
            Catch ex As Exception
                dealerExistFlag = "Y"
                HandleErrors(ex, MasterPage.MessageController)
            End Try

            Return dealerExistFlag
        End Function

        Private Function DeletePlan() As Boolean
            Dim bIsOk As Boolean = True
            Dim oPlan As CommPlan
            Dim commPaymentExists As String

            Try
                oPlan = TheCommPlan
                commPaymentExists = oPlan.CommPaymentExist(oPlan.Id)
                If commPaymentExists = "N" Then
                    Throw New GUIException(Message.MSG_EXTRACT_LINKED_WITH_PLAN_DELETE_NOT_ALLOWED, Assurant.ElitaPlus.Common.ErrorCodes.MSG_EXTRACT_LINKED_WITH_OTHER_PLAN_DELETE_NOT_ALLOWED)
                Else
                    With oPlan
                        .Delete()
                        .Save()
                    End With
                End If
            Catch ex As Exception
                'Me.LoadDistributionList()
                RePopulateDistributionListForPlan()
                SetGridSourceXcdLabelFromBo()
                HandleErrors(ex, MasterPage.MessageController)
                bIsOk = False
            End Try
            Return bIsOk
        End Function

#End Region

#Region "Handlers-Grid"

        Public Sub RowCreated(sender As Object, e As GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

#End Region

        Private Sub ChangeControlEnabledProperty(ctrl As Control, enabled As Boolean)
            Try
                If ((ctrl.GetType) Is GetType(WebControls.TextBox)) Then
                    If enabled = False Then 'change to readonly always allowed
                        CType(ctrl, WebControls.TextBox).ReadOnly = Not (enabled)
                        CType(ctrl, WebControls.TextBox).CssClass = "FLATTEXTBOX"
                    Else
                        If CanEnableControl(CType(ctrl, WebControl)) Then ' check whether change is allowed
                            CType(ctrl, WebControls.TextBox).ReadOnly = Not (enabled)
                            CType(ctrl, WebControls.TextBox).CssClass = "FLATTEXTBOX"
                        End If
                    End If
                End If
            Catch ex As Exception
            End Try
        End Sub

        Private Sub ClearDistributionGrid()
            moGridView.DataSource = Nothing
            moGridView.DataBind()
        End Sub

#Region "State-Management"

#Region "Plan State-Management"

        Protected Sub ComingFromSaveDistribution100PerAlert()
            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                ' Return from the Back Button

                Select Case confResponse
                    Case MSG_VALUE_YES
                        SaveDistributionChanges()
                        SetGridSourceXcdLabelFromBo()
                    Case MSG_VALUE_NO
                        'Do not save data
                        SetGridControls(moGridView, True)
                        FillSourceXcdDropdownList()
                        FillActEntSourceXcdDropdownList()
                        FillEntityDropDownList()
                        FillPayeeTypeDropDownList()
                        SetGridSourceXcdDropdownFromBo()
                        SetGridSourceXcdLabelFromBo()
                End Select
            End If

        End Sub

        Protected Sub ComingFromBack()
            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                ' Return from the Back Button

                Select Case confResponse
                    Case MSG_VALUE_YES
                        ' Save and go back to Search Page
                        If ApplyPlanChanges() = True Then
                            State.boChanged = True
                            GoBack()
                        End If
                    Case MSG_VALUE_NO
                        ' Go back to Search Page
                        GoBack()
                End Select
            End If

        End Sub

        Protected Sub ComingFromNew()
            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                ' Return from the New Button

                Select Case confResponse
                    Case MSG_VALUE_YES
                        ' Save and create a new BO
                        If ApplyPlanChanges() = True Then
                            CreateNew()
                        End If
                    Case MSG_VALUE_NO
                        ' create a new BO
                        CreateNew()
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
                        If ApplyPlanChanges() = True Then
                            State.boChanged = True
                            CreateNewCopy()
                        End If
                    Case MSG_VALUE_NO
                        ' create a new BO
                        CreateNewCopy()
                End Select
            End If

        End Sub

        Protected Sub ComingFromNewDistribution()
            Try
                Dim confResponse As String = HiddenSaveChangesPromptResponse.Value

                If Not confResponse = String.Empty Then
                    ' Return from the Back Button

                    Select Case confResponse
                        Case MSG_BTN_OK
                            State.IsCommPlanDistNew = True
                            DistributionId = Guid.Empty.ToString
                            PopulateDistributionList(ACTION_NEW)
                            FillSourceXcdDropdownList()
                            FillActEntSourceXcdDropdownList()
                            FillEntityDropDownList()
                            FillPayeeTypeDropDownList()
                            SetGridSourceXcdDropdownFromBo()
                            SetGridSourceXcdLabelFromBo()
                            SetGridControls(moGridView, False)
                            EnableDisableControls(moCoverageEditPanel, True)
                            setbuttons(True)
                            btnBack.Visible = True
                    End Select
                End If
            Catch ex As Exception
                FillSourceXcdDropdownList()
                FillEntityDropDownList()
                FillActEntSourceXcdDropdownList()
                FillPayeeTypeDropDownList()
                SetGridSourceXcdDropdownFromBo()
                SetGridSourceXcdLabelFromBo()
                SetGridControls(moGridView, True)
                EnableDisableControls(moCoverageEditPanel, True)
                setbuttons(True)
                btnBack.Visible = True
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub ComingFromNewDistributionForEitherAmtPer()
            Try
                Dim confResponse As String = HiddenSaveChangesPromptResponse.Value

                If Not confResponse = String.Empty Then
                    ' Return from the Back Button

                    Select Case confResponse
                        Case MSG_BTN_OK
                            SetGridControls(moGridView, True)
                            FillSourceXcdDropdownList()
                            FillActEntSourceXcdDropdownList()
                            FillEntityDropDownList()
                            FillPayeeTypeDropDownList()
                            SetGridSourceXcdDropdownFromBo()
                            SetGridSourceXcdLabelFromBo()
                            SetGridControls(moGridView, True)
                            EnableDisableControls(moCoverageEditPanel, True)
                            setbuttons(True)
                            btnBack.Visible = True
                    End Select
                End If
            Catch ex As Exception
                FillSourceXcdDropdownList()
                FillActEntSourceXcdDropdownList()
                FillEntityDropDownList()
                FillPayeeTypeDropDownList()
                SetGridSourceXcdDropdownFromBo()
                SetGridSourceXcdLabelFromBo()
                SetGridControls(moGridView, True)
                EnableDisableControls(moCoverageEditPanel, True)
                setbuttons(True)
                btnBack.Visible = True
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Breadkdown State-Management"

#End Region

        Protected Sub CheckIfComingFromConfirm()
            Try
                Select Case State.ActionInProgress
                    ' Distribution
                    Case ElitaPlusPage.DetailPageCommand.OK
                        If State.IsAmountAndPercentBothPresent Then
                            ComingFromNewDistributionForEitherAmtPer()
                        Else
                            ComingFromNewDistribution()
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ComingFromBack()
                    Case ElitaPlusPage.DetailPageCommand.New_
                        ComingFromNewCopy()
                        EnableNewDistributionButtons(False)
                        EnableExpiration(True)
                        EnablePlanCodeDescFields(True)
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        ComingFromNewCopy()


                    'Case ElitaPlusPage.DetailPageCommand.Redirect_
                    Case ElitaPlusPage.DetailPageCommand.Accept
                        'Alert for accepting greater than 100% commission percentage
                        ComingFromSaveDistribution100PerAlert()

                        TheDealerControl.ChangeEnabledControlProperty(False)
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        MasterPage.MessageController.AddErrorAndShow(State.LastErrMsg)
                End Select

                'Clean after consuming the action
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                HiddenSaveChangesPromptResponse.Value = String.Empty
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Acct Source Xcd Option Bucket Logic"
        Private Sub ValidatePmCertSourceLogic()
            'Avoid Price Metrics and Cert Commission combination
            ValidatePmCertCommSourceXcd()

            If State.IsPmComCombination Then
                Throw New GUIException(Message.MSG_PRICEMETRICS_CERTCALC_NOT_ALLOWED, Assurant.ElitaPlus.Common.ErrorCodes.MSG_PRICE_METRICS_AND_CERT_COMM_NOT_ALLOWED)
            End If

        End Sub

        Private Sub ValidatePmCertCommSourceXcd()
            Dim countPM As Integer = 0
            Dim countCertComm As Integer = 0
            For pageIndexk As Integer = 0 To moGridView.PageCount - 1
                moGridView.PageIndex = pageIndexk
                Dim rowNum As Integer = moGridView.Rows.Count
                For i As Integer = 0 To rowNum - 1
                    Dim gRow As GridViewRow = moGridView.Rows(i)
                    If gRow.RowType = DataControlRowType.DataRow Then
                        Dim mocboCommPercentSourceXcd As DropDownList = DirectCast(gRow.Cells(COL_COMMISSIONS_SOURCE_XCD_IDX).FindControl("cboCommPercentSourceXcd"), DropDownList)
                        Dim mollblCommPercentSourceXcd As Label = DirectCast(gRow.Cells(COL_COMMISSIONS_SOURCE_XCD_IDX).FindControl("lblCommPercentSourceXcd"), Label)
                        Dim mollblCommPercentSourceXcdCode As Label = DirectCast(gRow.Cells(COL_COMMISSIONS_SOURCE_XCD_IDX).FindControl("lblCommPercentSourceXcdCode"), Label)

                        If mocboCommPercentSourceXcd IsNot Nothing Then
                            If mocboCommPercentSourceXcd.Items.Count > 0 Then
                                If mocboCommPercentSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_COMMBRKDOWN_OPTION_CERTCOMM) Then
                                    countPM = countPM + 1
                                End If

                                If mocboCommPercentSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_COMMBRKDOWN_OPTION_PRICEMATRIX) Then
                                    countCertComm = countCertComm + 1
                                End If
                            End If
                        End If

                        If mollblCommPercentSourceXcdCode IsNot Nothing Then
                            If Not String.IsNullOrWhiteSpace(mollblCommPercentSourceXcdCode.Text) Then

                                If mollblCommPercentSourceXcdCode.Text.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_COMMBRKDOWN_OPTION_CERTCOMM) Then
                                    countPM = countPM + 1
                                End If

                                If mollblCommPercentSourceXcdCode.Text.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_COMMBRKDOWN_OPTION_PRICEMATRIX) Then
                                    countCertComm = countCertComm + 1
                                End If
                            End If
                        End If

                    End If
                Next
            Next

            If ((countPM = 1 Or countPM >
                1) And (countCertComm = 1 Or countCertComm > 1)) Then
                State.IsPmComCombination = True
            Else
                State.IsPmComCombination = False
            End If
        End Sub

        Private Function IsCommPerGreaterThanHundred() As Boolean
            Dim countCommPer As Decimal = 0
            Dim isCommPerMoreThanHundred As Boolean
            For pageIndexk As Integer = 0 To moGridView.PageCount - 1
                moGridView.PageIndex = pageIndexk
                Dim rowNum As Integer = moGridView.Rows.Count
                For i As Integer = 0 To rowNum - 1
                    Dim gRow As GridViewRow = moGridView.Rows(i)
                    If gRow.RowType = DataControlRowType.DataRow Then
                        Dim molblCommPer As Label = DirectCast(gRow.Cells(COL_COMMISSIONS_SOURCE_XCD_IDX).FindControl("moCommission_PercentLabel"), Label)
                        Dim motextBoxCommPer As TextBox = DirectCast(gRow.Cells(COL_COMMISSIONS_SOURCE_XCD_IDX).FindControl("moCommission_PercentText"), TextBox)

                        If motextBoxCommPer IsNot Nothing Then
                            If Not String.IsNullOrWhiteSpace(motextBoxCommPer.Text) Then
                                Dim cPer As Decimal = Convert.ToDecimal(motextBoxCommPer.Text)
                                countCommPer = countCommPer + cPer
                            End If
                        End If

                        If molblCommPer IsNot Nothing Then
                            If Not String.IsNullOrWhiteSpace(molblCommPer.Text) Then
                                Dim cPer As Decimal = Convert.ToDecimal(molblCommPer.Text)
                                countCommPer = countCommPer + cPer
                            End If
                        End If

                    End If
                Next
            Next

            If (countCommPer > 100) Then
                isCommPerMoreThanHundred = True
            Else
                isCommPerMoreThanHundred = False
            End If

            Return isCommPerMoreThanHundred
        End Function

        Private Function IsAmountAndPercentBothPresent() As Boolean
            Dim countCommPer As Int16 = 0
            Dim countAmt As Int16 = 0

            For pageIndexk As Integer = 0 To moGridView.PageCount - 1
                moGridView.PageIndex = pageIndexk
                Dim rowNum As Integer = moGridView.Rows.Count
                For i As Integer = 0 To rowNum - 1
                    Dim gRow As GridViewRow = moGridView.Rows(i)
                    If gRow.RowType = DataControlRowType.DataRow Then
                        Dim molblCommPer As Label = DirectCast(gRow.Cells(COL_COMMISSIONS_SOURCE_XCD_IDX).FindControl("moCommission_PercentLabel"), Label)
                        Dim motextBoxCommPer As TextBox = DirectCast(gRow.Cells(COL_COMMISSIONS_SOURCE_XCD_IDX).FindControl("moCommission_PercentText"), TextBox)

                        Dim molblAmt As Label = DirectCast(gRow.Cells(COL_COMMISSION_AMOUNT_IDX).FindControl("moLowPriceLabel"), Label)
                        Dim motextBoxAmt As TextBox = DirectCast(gRow.Cells(COL_COMMISSION_AMOUNT_IDX).FindControl("moLowPriceText"), TextBox)

                        If motextBoxCommPer IsNot Nothing Then
                            If Not String.IsNullOrWhiteSpace(motextBoxCommPer.Text) Then
                                Dim cPer As Decimal = Convert.ToDecimal(motextBoxCommPer.Text)
                                If cPer > 0 Then
                                    countCommPer = countCommPer + 1
                                End If
                            End If
                        End If

                        If molblCommPer IsNot Nothing Then
                            If Not String.IsNullOrWhiteSpace(molblCommPer.Text) Then
                                Dim cPer As Decimal = Convert.ToDecimal(molblCommPer.Text)
                                countCommPer = countCommPer + 1
                            End If
                        End If


                        If motextBoxAmt IsNot Nothing Then
                            If Not String.IsNullOrWhiteSpace(motextBoxAmt.Text) Then
                                Dim cAmt As Decimal = Convert.ToDecimal(motextBoxAmt.Text)
                                If cAmt > 0 Then
                                    countAmt = countAmt + 1
                                End If
                            End If
                        End If

                        If molblAmt IsNot Nothing Then
                            If Not String.IsNullOrWhiteSpace(molblAmt.Text) Then
                                Dim cAmt As Decimal = Convert.ToDecimal(molblAmt.Text)
                                If cAmt > 0 Then
                                    countAmt = countAmt + 1
                                End If
                            End If
                        End If

                    End If
                Next
            Next

            If ((countCommPer = 1 Or countCommPer > 1) And (countAmt = 1 Or countAmt > 1)) Then
                State.IsAmountAndPercentBothPresent = True
            Else
                State.IsAmountAndPercentBothPresent = False
            End If

            Return State.IsAmountAndPercentBothPresent
        End Function

        Private Sub SetGridSourceXcdLabelFromBo()
            'If moGridView.EditIndex = -1 Then Exit Sub
            For pageIndexk As Integer = 0 To moGridView.PageCount - 1
                moGridView.PageIndex = pageIndexk
                Dim rowNum As Integer = moGridView.Rows.Count
                For i As Integer = 0 To rowNum - 1
                    Dim gRow As GridViewRow = moGridView.Rows(i)
                    If gRow.RowType = DataControlRowType.DataRow Then
                        Dim mollblCommPercentSourceXcd As Label = DirectCast(gRow.Cells(COL_COMMISSIONS_SOURCE_XCD_IDX).FindControl("lblCommPercentSourceXcd"), Label)

                        Dim molblActEntSourceXcd As Label = DirectCast(gRow.Cells(COL_ACT_ENT_SOURCE_XCD_IDX).FindControl("lblActEntitySourceXcd"), Label)

                        Dim molblPayeeType As Label = DirectCast(gRow.Cells(COL_PAYEE_TYPE_XCD_IDX).FindControl("lblPayeeType"), Label)
                        Dim molblEntityType As Label = DirectCast(gRow.Cells(COL_ENTITY_ID_IDX).FindControl("lblEntityType"), Label)

                        If molblPayeeType IsNot Nothing Then
                            If molblPayeeType.Visible Then
                                If (molblPayeeType.Text IsNot Nothing And Not String.IsNullOrWhiteSpace(molblPayeeType.Text)) Then
                                    molblPayeeType.Text = GetDescOfExtCodePayeeTypeXcdOption(molblPayeeType.Text)
                                End If
                            End If
                        End If

                        If mollblCommPercentSourceXcd IsNot Nothing Then
                            If mollblCommPercentSourceXcd.Visible Then
                                If (mollblCommPercentSourceXcd.Text IsNot Nothing And Not String.IsNullOrWhiteSpace(mollblCommPercentSourceXcd.Text)) Then
                                    mollblCommPercentSourceXcd.Text = GetDescFromExtCode(mollblCommPercentSourceXcd.Text)
                                End If
                            End If
                        End If

                        If molblActEntSourceXcd IsNot Nothing Then
                            If molblActEntSourceXcd.Visible Then
                                If (molblActEntSourceXcd.Text IsNot Nothing And Not String.IsNullOrWhiteSpace(molblActEntSourceXcd.Text)) Then
                                    molblActEntSourceXcd.Text = GetDescFromExtCodeForActEnt(molblActEntSourceXcd.Text)
                                End If
                            End If
                        End If

                        If molblEntityType IsNot Nothing Then
                            If molblEntityType.Visible Then
                                If (molblEntityType.Text IsNot Nothing And Not String.IsNullOrWhiteSpace(molblEntityType.Text)) Then
                                    molblEntityType.Text = GetDescOfIDFromEntityTypeOption(molblEntityType.Text)
                                End If
                            End If
                        End If

                    End If
                Next
            Next
        End Sub

        Private Sub FillSourceXcdDropdownList()

            'fill the drop downs
            If moGridView.EditIndex = -1 Then Exit Sub

            Dim gRow As GridViewRow = moGridView.Rows(moGridView.EditIndex)
            Dim mocboCommPercentSourceXcd As DropDownList = DirectCast(gRow.Cells(COL_COMMISSIONS_SOURCE_XCD_IDX).FindControl("cboCommPercentSourceXcd"), DropDownList)

            If mocboCommPercentSourceXcd IsNot Nothing Then
                PoupulateSourceOptionDropdownlist(mocboCommPercentSourceXcd)
            End If
        End Sub

        Private Sub FillActEntSourceXcdDropdownList()

            'fill the drop downs
            If moGridView.EditIndex = -1 Then Exit Sub

            Dim gRow As GridViewRow = moGridView.Rows(moGridView.EditIndex)
            Dim mocboActEntitySourceXcd As DropDownList = DirectCast(gRow.Cells(COL_ACT_ENT_SOURCE_XCD_IDX).FindControl("cboActEntitySourceXcd"), DropDownList)

            If mocboActEntitySourceXcd IsNot Nothing Then
                PoupulateActEntSourceOptionDropdownlist(mocboActEntitySourceXcd)
            End If
        End Sub

        Private Sub FillEntityDropDownList()
            If moGridView.EditIndex = -1 Then Exit Sub
            Dim gRow As GridViewRow = moGridView.Rows(moGridView.EditIndex)
            Dim mocboEntityType As DropDownList = DirectCast(gRow.Cells(COL_ENTITY_ID_IDX).FindControl("cboEntityType"), DropDownList)

            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            Dim CommEntityList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="CommEntityByCompanyGroup", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=listcontext)

            mocboEntityType.Populate(CommEntityList, New PopulateOptions() With
            {
                .AddBlankItem = True,
                .TextFunc = AddressOf PopulateOptions.GetDescription,
                .ValueFunc = AddressOf PopulateOptions.GetListItemId
            })
        End Sub

        Private Sub FillPayeeTypeDropDownList()
            If moGridView.EditIndex = -1 Then Exit Sub
            Dim gRow As GridViewRow = moGridView.Rows(moGridView.EditIndex)
            Dim mocboPayeeType As DropDownList = DirectCast(gRow.Cells(COL_ENTITY_ID_IDX).FindControl("cboPayeeType"), DropDownList)

            Dim PayeeTypeList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="PYTYPE", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
            Dim FilteredPayeeTypeList As DataElements.ListItem()
            Dim FilteredPayeeType1List As DataElements.ListItem()
            Dim yesId As Guid = (From lst In CommonConfigManager.Current.ListManager.GetList(listCode:="YESNO", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
                                 Where lst.Code = "Y"
                                 Select lst.ListItemId).FirstOrDefault()

            Dim dealerId As Guid = TheDealerControl.SelectedGuid
            Dim oDealer As New Dealer(dealerId)

            If Not oDealer.DealerGroupId.Equals(Guid.Empty) Then
                Dim oDealerGrp As New DealerGroup(oDealer.DealerGroupId)
                If oDealerGrp.AcctingByGroupId.Equals(yesId) Then
                    FilteredPayeeTypeList = (From lst In PayeeTypeList
                                             Where lst.Code <> Payee_Type_Dealer
                                             Select lst).ToArray()
                Else
                    FilteredPayeeTypeList = (From lst In PayeeTypeList
                                             Where lst.Code <> Payee_Type_Dealer_Group
                                             Select lst).ToArray()
                End If
            Else
                FilteredPayeeTypeList = (From lst In PayeeTypeList
                                         Where lst.Code <> Payee_Type_Dealer_Group
                                         Select lst).ToArray()
            End If

            mocboPayeeType.Populate(FilteredPayeeTypeList, New PopulateOptions() With
            {
                .AddBlankItem = False,
                .TextFunc = AddressOf PopulateOptions.GetDescription,
                .ValueFunc = AddressOf PopulateOptions.GetExtendedCode
            })

            FilteredPayeeType1List = (From lst In PayeeTypeList
                                      Where lst.Code = Payee_Type_Comm_Entity
                                      Select lst).ToArray()

            Dim FirstPayeeType As String
            FirstPayeeType = FilteredPayeeType1List.First().ListItemId.ToString()
            BindSelectItem(FirstPayeeType, mocboPayeeType)
        End Sub

        Private Sub SetGridSourceXcdDropdownFromBo()
            If moGridView.EditIndex = -1 Then Exit Sub
            Dim gRow As GridViewRow = moGridView.Rows(moGridView.EditIndex)
            Dim mocboCommPercentSourceXcd As DropDownList = DirectCast(gRow.Cells(COL_COMMISSIONS_SOURCE_XCD_IDX).FindControl("cboCommPercentSourceXcd"), DropDownList)
            Dim moTextCommission_PercentText As TextBox = DirectCast(gRow.Cells(COL_COMMISSION_PERCENTAGE_IDX).FindControl("moCommission_PercentText"), TextBox)
            Dim diffValue As Decimal = 0.0000

            If State.IsNewWithCopy = False Then
                With TheCommPlanDist
                    If mocboCommPercentSourceXcd.Visible Then
                        If .CommissionsPercentSourceXcd IsNot Nothing And mocboCommPercentSourceXcd.Items.Count > 0 Then
                            SetSelectedItem(mocboCommPercentSourceXcd, .CommissionsPercentSourceXcd)

                            If mocboCommPercentSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_COMMBRKDOWN_OPTION_DIFFERENCE) Then
                                PopulateControlFromBOProperty(moTextCommission_PercentText, diffValue, PERCENT_FORMAT)
                                moTextCommission_PercentText.Enabled = False
                            Else
                                moTextCommission_PercentText.Enabled = True
                            End If
                        End If
                    End If
                End With
            End If
        End Sub
        Private Sub SetControlValuesFromBo()
            If moGridView.EditIndex = -1 Then Exit Sub
            Dim gRow As GridViewRow = moGridView.Rows(moGridView.EditIndex)
            Dim cboPayeeType As DropDownList = DirectCast(gRow.Cells(COL_COMMISSIONS_SOURCE_XCD_IDX).FindControl("cboPayeeType"), DropDownList)
            Dim cboActEntitySourceXcd As DropDownList = DirectCast(gRow.Cells(COL_COMMISSIONS_SOURCE_XCD_IDX).FindControl("cboActEntitySourceXcd"), DropDownList)
            Dim cboEntityType As DropDownList = DirectCast(gRow.Cells(COL_COMMISSIONS_SOURCE_XCD_IDX).FindControl("cboEntityType"), DropDownList)
            Dim moTextCommission_PercentText As TextBox = DirectCast(gRow.Cells(COL_COMMISSION_PERCENTAGE_IDX).FindControl("moCommission_PercentText"), TextBox)
            Dim moLowPriceText As TextBox = DirectCast(gRow.Cells(COL_COMMISSION_PERCENTAGE_IDX).FindControl("moLowPriceText"), TextBox)
            Dim textBoxPosition As TextBox = DirectCast(gRow.Cells(COL_COMMISSION_PERCENTAGE_IDX).FindControl("textBoxPosition"), TextBox)

            If State.IsNewWithCopy = False Then
                With TheCommPlanDist

                    If cboActEntitySourceXcd.Visible Then
                        If .ActEntitySourceXcd IsNot Nothing And cboActEntitySourceXcd.Items.Count > 0 Then
                            SetSelectedItem(cboActEntitySourceXcd, .ActEntitySourceXcd)
                        End If
                    End If

                    If cboPayeeType.Visible Then
                        If .PayeeTypeXcd IsNot Nothing And cboPayeeType.Items.Count > 0 Then
                            SetSelectedItem(cboPayeeType, .PayeeTypeXcd)
                        End If
                    End If

                    If cboEntityType.Visible Then
                        If cboEntityType.Items.Count > 0 Then
                            SetSelectedItem(cboEntityType, .EntityId)
                        End If
                    End If

                    If .CommissionPercent IsNot Nothing And moTextCommission_PercentText IsNot Nothing Then
                        PopulateControlFromBOProperty(moTextCommission_PercentText, .CommissionPercent, PERCENT_FORMAT)
                    End If

                    If .CommissionAmount IsNot Nothing And moLowPriceText IsNot Nothing Then
                        PopulateControlFromBOProperty(moLowPriceText, .CommissionAmount, DECIMAL_FORMAT)
                    End If

                    If .Position IsNot Nothing And textBoxPosition IsNot Nothing Then
                        PopulateControlFromBOProperty(textBoxPosition, .Position)
                    End If
                End With
            End If
        End Sub

        Private Sub PoupulateSourceOptionDropdownlist(oDropDownList As DropDownList)
            Dim oAcctBucketsSourceOption As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="ACCTBUCKETSOURCE_COMMBRKDOWN")

            oDropDownList.Populate(oAcctBucketsSourceOption, New PopulateOptions() With
                                {
                                .AddBlankItem = False,
                                .TextFunc = AddressOf PopulateOptions.GetDescription,
                                .ValueFunc = AddressOf PopulateOptions.GetExtendedCode
                                })
        End Sub

        Private Sub PoupulateActEntSourceOptionDropdownlist(oDropDownList As DropDownList)
            Dim AcctFieldTypeList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="ACCTFIELDTYP", languageCode:=ElitaPlusIdentity.Current.ActiveUser.LanguageCode)

            Dim FilteredAcctFieldTypeList As DataElements.ListItem() = (From lst In AcctFieldTypeList
                                                                        Where lst.Code.ToUpper.StartsWith("COMMISSIONS")
                                                                        Select lst).ToArray()

            oDropDownList.Populate(FilteredAcctFieldTypeList, New PopulateOptions() With
                                {
                                .AddBlankItem = False,
                                .TextFunc = AddressOf PopulateOptions.GetDescription,
                                .ValueFunc = AddressOf PopulateOptions.GetExtendedCode
                                })
        End Sub

        Public Function GetDescFromExtCode(desc As String) As String
            Dim sGetCodeSourceOptionDesc As String
            Try
                sGetCodeSourceOptionDesc = String.Empty
                If desc IsNot Nothing And Not String.IsNullOrWhiteSpace(desc) Then
                    sGetCodeSourceOptionDesc = LookupListNew.GetDescriptionFromExtCode("ACCTBUCKETSOURCE_COMMBRKDOWN", ElitaPlusIdentity.Current.ActiveUser.LanguageId, desc)
                End If
                Return sGetCodeSourceOptionDesc
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Function

        Public Function GetDescFromExtCodeForActEnt(desc As String) As String
            Dim sGetCodeSourceOptionDesc As String
            Try
                sGetCodeSourceOptionDesc = String.Empty
                If desc IsNot Nothing And Not String.IsNullOrWhiteSpace(desc) Then
                    sGetCodeSourceOptionDesc = LookupListNew.GetDescriptionFromExtCode("ACCTFIELDTYP", ElitaPlusIdentity.Current.ActiveUser.LanguageId, desc)
                End If
                Return sGetCodeSourceOptionDesc
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Function

        Public Function GetDescOfExtCodePayeeTypeXcdOption(desc As String) As String
            Dim sGetDescOfExtCodeFromPayeeTypeXcd As String
            Try
                sGetDescOfExtCodeFromPayeeTypeXcd = String.Empty
                If desc IsNot Nothing And Not String.IsNullOrWhiteSpace(desc) Then
                    sGetDescOfExtCodeFromPayeeTypeXcd = LookupListNew.GetDescriptionFromExtCode("PYTYPE", ElitaPlusIdentity.Current.ActiveUser.LanguageId, desc)
                End If
                Return sGetDescOfExtCodeFromPayeeTypeXcd
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Function

        Public Function GetDescOfIDFromEntityTypeOption(desc As String) As String
            Dim sGetDescOfExtCodeFromPayeeTypeXcd As String
            Try
                sGetDescOfExtCodeFromPayeeTypeXcd = String.Empty
                Dim listcontext As ListContext = New ListContext()
                listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                Dim CommEntityList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="CommEntityByCompanyGroup", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=listcontext)

                If desc IsNot Nothing And Not String.IsNullOrWhiteSpace(desc) Then
                    If (desc.Length > 16) Then
                        sGetDescOfExtCodeFromPayeeTypeXcd = (From lst In CommEntityList
                                                             Where lst.ListItemId = GetGuidFromString(desc)
                                                             Select lst.Translation).FirstOrDefault
                    End If
                End If
                Return sGetDescOfExtCodeFromPayeeTypeXcd
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Function

        Private Sub LoadDistributionList()
            If moGridView.Rows.Count > 0 Then
                Dim i As Integer = 0
                Dim oDistribution(moGridView.Rows.Count - 1) As CommPlanDistribution

                For i = 0 To moGridView.Rows.Count - 1
                    oDistribution(i) = New CommPlanDistribution
                    oDistribution(i).CommissionPlanId = State.moCommPlanId 'TheCommPlanDist.CommissionPlanId
                    If moGridView.Rows(i).Cells(COL_PAYEE_TYPE_XCD_IDX).Controls(1).GetType().ToString = "System.Web.UI.WebControls.Label" Then
                        PopulateBOProperty(oDistribution(i), PROPERTY_PAYEE_TYPE_XCD, CType(moGridView.Rows(i).Cells(COL_PAYEE_TYPE_XCD_IDX).Controls(1), Label).Text)
                    Else
                        PopulateBOProperty(oDistribution(i), PROPERTY_PAYEE_TYPE_XCD, CType(moGridView.Rows(i).Cells(COL_PAYEE_TYPE_XCD_IDX).Controls(1), DropDownList).Text)
                    End If

                    If moGridView.Rows(i).Cells(COL_ENTITY_ID_IDX).Controls(0).GetType().ToString = "System.Web.UI.WebControls.Label" Then
                        PopulateBOProperty(oDistribution(i), PROPERTY_ENTITY_ID, CType(moGridView.Rows(i).Cells(COL_ENTITY_ID_IDX).Controls(0), Label).Text)
                    Else
                        PopulateBOProperty(oDistribution(i), PROPERTY_ENTITY_ID, CType(moGridView.Rows(i).Cells(COL_ENTITY_ID_IDX).Controls(1), DropDownList).SelectedValue)
                    End If

                    If moGridView.Rows(i).Cells(COL_COMMISSION_AMOUNT_IDX).Controls(1).GetType().ToString = "System.Web.UI.WebControls.Label" Then
                        PopulateBOProperty(oDistribution(i), PROPERTY_COMM_AMT, CType(moGridView.Rows(i).Cells(COL_COMMISSION_AMOUNT_IDX).Controls(1), Label).Text)
                    Else
                        PopulateBOProperty(oDistribution(i), PROPERTY_COMM_AMT, CType(moGridView.Rows(i).Cells(COL_COMMISSION_AMOUNT_IDX).Controls(1), TextBox).Text)
                    End If

                    If moGridView.Rows(i).Cells(COL_COMMISSION_PERCENTAGE_IDX).Controls(1).GetType().ToString = "System.Web.UI.WebControls.Label" Then
                        PopulateBOProperty(oDistribution(i), PROPERTY_COMM_PER, CType(moGridView.Rows(i).Cells(COL_COMMISSION_PERCENTAGE_IDX).Controls(1), Label).Text)
                    Else
                        PopulateBOProperty(oDistribution(i), PROPERTY_COMM_PER, CType(moGridView.Rows(i).Cells(COL_COMMISSION_PERCENTAGE_IDX).Controls(1), TextBox).Text)
                    End If

                    If moGridView.Rows(i).Cells(COL_COMMISSIONS_SOURCE_XCD_IDX).Controls(0).GetType().ToString = "System.Web.UI.WebControls.Label" Then
                        PopulateBOProperty(oDistribution(i), PROPERTY_COMMISSIONS_XCD, CType(moGridView.Rows(i).Cells(COL_COMMISSIONS_SOURCE_XCD_IDX).Controls(0), Label).Text)
                    Else
                        PopulateBOProperty(oDistribution(i), PROPERTY_COMMISSIONS_XCD, CType(moGridView.Rows(i).Cells(COL_COMMISSIONS_SOURCE_XCD_IDX).Controls(1), DropDownList).SelectedValue)
                    End If

                    If moGridView.Rows(i).Cells(COL_ACT_ENT_SOURCE_XCD_IDX).Controls(0).GetType().ToString = "System.Web.UI.WebControls.Label" Then
                        PopulateBOProperty(oDistribution(i), PROPERTY_ACT_ENT_XCD, CType(moGridView.Rows(i).Cells(COL_ACT_ENT_SOURCE_XCD_IDX).Controls(0), Label).Text)
                    Else
                        PopulateBOProperty(oDistribution(i), PROPERTY_ACT_ENT_XCD, CType(moGridView.Rows(i).Cells(COL_ACT_ENT_SOURCE_XCD_IDX).Controls(1), DropDownList).SelectedValue)
                    End If

                    If moGridView.Rows(i).Cells(COL_POSITION_IDX).Controls(1).GetType().ToString = "System.Web.UI.WebControls.Label" Then
                        PopulateBOProperty(oDistribution(i), PROPERTY_POSITION, CType(moGridView.Rows(i).Cells(COL_POSITION_IDX).Controls(1), Label).Text)
                    Else
                        PopulateBOProperty(oDistribution(i), PROPERTY_POSITION, CType(moGridView.Rows(i).Cells(COL_POSITION_IDX).Controls(1), TextBox).Text)
                    End If
                Next

                State.moDistributionList = oDistribution
            End If
        End Sub

        Public Function SaveDistributionList() As Boolean
            Dim i As Integer = 0
            Try
                If State.moDistributionList IsNot Nothing Then
                    For i = 0 To State.moDistributionList.Length - 1
                        State.moDistributionList(i).CommissionPlanId = State.moCommPlanId 'TheCommPlanDist.CommissionPlanId
                        State.moDistributionList(i).Save()
                    Next
                End If
            Catch ex As Exception
                Dim j As Integer
                'REPLACE THIS LOOP BY A DB ROLLBACK
                For j = 0 To i - 1
                    State.moDistributionList(j).Delete()
                    State.moDistributionList(j).Save()
                Next
                'Me.HandleErrors(ex, moMsgControllerRate)
                Return False
            End Try
            Return True
        End Function
        Private Sub BindBoPropertiesToGridHeader()
            BindBOPropertyToGridHeader(TheCommPlanDist, PROPERTY_PAYEE_TYPE_XCD, moGridView.Columns(COL_PAYEE_TYPE_XCD_IDX))
            BindBOPropertyToGridHeader(TheCommPlanDist, PROPERTY_ENTITY_ID, moGridView.Columns(COL_ENTITY_ID_IDX))
            BindBOPropertyToGridHeader(TheCommPlanDist, PROPERTY_COMM_AMT, moGridView.Columns(COL_COMMISSION_AMOUNT_IDX))
            BindBOPropertyToGridHeader(TheCommPlanDist, PROPERTY_COMM_PER, moGridView.Columns(COL_COMMISSION_PERCENTAGE_IDX))
            BindBOPropertyToGridHeader(TheCommPlanDist, PROPERTY_COMMISSIONS_XCD, moGridView.Columns(COL_COMMISSIONS_SOURCE_XCD_IDX))
            BindBOPropertyToGridHeader(TheCommPlanDist, PROPERTY_ACT_ENT_XCD, moGridView.Columns(COL_ACT_ENT_SOURCE_XCD_IDX))
            BindBOPropertyToGridHeader(TheCommPlanDist, PROPERTY_POSITION, moGridView.Columns(COL_POSITION_IDX))
        End Sub

#Region "Handlers-Distribution-DataGrid"

        Public Sub ItemCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moGridView_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles moGridView.PageIndexChanging
            Try
                ResetIndexes()
                moGridView.PageIndex = e.NewPageIndex
                PopulateDistributionList(ACTION_CANCEL_DELETE)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub ItemCommand(source As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles moGridView.RowCommand
            Dim nIndex As Integer = CInt(e.CommandArgument)

            Try
                If e.CommandName = EDIT_COMMAND_NAME Then
                    nIndex = CInt(e.CommandArgument)
                    moGridView.EditIndex = nIndex
                    moGridView.SelectedIndex = nIndex
                    PopulateDistributionList(ACTION_EDIT)
                    FillSourceXcdDropdownList()
                    FillActEntSourceXcdDropdownList()
                    FillEntityDropDownList()
                    FillPayeeTypeDropDownList()
                    SetGridSourceXcdDropdownFromBo()
                    SetGridSourceXcdLabelFromBo()
                    SetControlValuesFromBo()
                    SetGridControls(moGridView, False)
                    EnableDisableControls(moCoverageEditPanel, True)
                ElseIf (e.CommandName = DELETE_COMMAND_NAME) Then
                    moGridView.EditIndex = nIndex
                    moGridView.SelectedIndex = nIndex
                    DistributionId = GetGridText(moGridView, nIndex, COL_COMMISSION_PLAN_DIST_ID_IDX)

                    State.moCommPlanDistPlanId = GetGuidFromString(GetGridText(moGridView, nIndex, COL_COMMISSION_PLAN_ID_IDX))
                    State.moCommPlanDistId = GetGuidFromString(DistributionId)
                    State.MyBoDist = New CommPlanDistribution(State.moCommPlanDistId)
                    If DeleteSelectedDistribution(nIndex) = True Then
                        PopulateDistributionList(ACTION_CANCEL_DELETE)
                    End If
                    SetGridSourceXcdLabelFromBo()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub ResetIndexes()
            moGridView.EditIndex = NO_ITEM_SELECTED_INDEX
            moGridView.SelectedIndex = NO_ITEM_SELECTED_INDEX
        End Sub

        Private Sub BtnSaveRate_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles BtnSaveRate_WRITE.Click
            Try
                ValidatePmCertSourceLogic()

                ValidatePositionNo()

                ValidateCommAmountAndPercent()

                If State.IsPmComCombination Then
                    Throw New GUIException(Message.MSG_PRICEMETRICS_CERTCALC_NOT_ALLOWED, Assurant.ElitaPlus.Common.ErrorCodes.MSG_PRICE_METRICS_AND_CERT_COMM_NOT_ALLOWED)
                End If

                If IsAmountAndPercentBothPresent() Then
                    State.IsAmountAndPercentBothPresent = True
                    Throw New GUIException(Message.MSG_EITHER_PERCENTAGE_OR_AMOUNT_ALLOWED, Assurant.ElitaPlus.Common.ErrorCodes.MSG_ONLY_EITHER_PERCENTAGE_OR_AMOUNT_ALLOWED)
                End If

                If IsCommPerGreaterThanHundred() Then
                    State.ActionInProgress = DetailPageCommand.Accept
                    DisplayMessage(Message.MSG_COMMISSION_PERCENTAGE_IS_GREATER_THAN_HUNDRED, "", MSG_BTN_YES_NO, MSG_TYPE_INFO, HiddenSaveChangesPromptResponse)
                Else
                    SaveDistributionChanges()
                    SetGridSourceXcdLabelFromBo()
                    SetGridControls(moGridView, True)
                End If

                EnablePlanCodeDescFields(True)
            Catch ex As Exception
                State.IsCommPlanDistNew = False
                SetGridSourceXcdLabelFromBo()
                TheDealerControl.ChangeEnabledControlProperty(False)
                setbuttons(True)
                btnBack.Visible = True
                btnSave_WRITE.Visible = False
                BtnSaveRate_WRITE.Visible = True
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub BtnCancelRate_Click(sender As System.Object, e As System.EventArgs) Handles BtnCancelRate.Click
            'Pencil button in not in edit mode
            Try
                State.IsCommPlanDistNew = False
                EnableForEditRateButtons(False)
                PopulateDistributionList(ACTION_CANCEL_DELETE)
                SetGridSourceXcdLabelFromBo()
                TheDealerControl.ChangeEnabledControlProperty(False)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub BtnNewRate_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles BtnNewRate_WRITE.Click
            Try
                If moGridView.Rows.Count >= 5 Then
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.OK
                    DisplayMessage(Message.MSG_DISTRIBUTION_RECORD_LIMITED_FOR_EXTRACT_REPORT, "", MSG_BTN_OK, MSG_TYPE_INFO, HiddenSaveChangesPromptResponse)
                Else
                    State.IsCommPlanDistNew = True
                    DistributionId = Guid.Empty.ToString
                    PopulateDistributionList(ACTION_NEW)
                    FillSourceXcdDropdownList()
                    FillActEntSourceXcdDropdownList()
                    FillEntityDropDownList()
                    FillPayeeTypeDropDownList()
                    SetGridSourceXcdDropdownFromBo()
                    SetGridSourceXcdLabelFromBo()
                    SetGridControls(moGridView, False)
                    EnableDisableControls(moCoverageEditPanel, True)
                    setbuttons(True)
                    btnBack.Visible = True
                End If

            Catch ex As Exception
                SetGridControls(moGridView, True)
                FillSourceXcdDropdownList()
                FillActEntSourceXcdDropdownList()
                FillEntityDropDownList()
                FillPayeeTypeDropDownList()
                SetGridSourceXcdDropdownFromBo()
                SetGridSourceXcdLabelFromBo()
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub setbuttons(enable As Boolean)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, enable)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, enable)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, enable)
            ControlMgr.SetEnableControl(Me, btnUndo_WRITE, enable)
        End Sub

        Private Sub SaveDistributionChanges()
            If ApplyDistributionChanges() = True Then
                If State.IsCommPlanDistNew = True Then
                    State.IsCommPlanDistNew = False
                End If
                PopulateDistributionList(ACTION_SAVE)
            End If
        End Sub
        Private Function ApplyDistributionChanges() As Boolean
            Dim bIsOk As Boolean = True
            Dim bIsDirty As Boolean
            Dim oPlanDist As CommPlanDistribution
            Dim commPaymentExists As String

            If moGridView.EditIndex < 0 Then Return False ' Distribution is not in edit mode
            If State.IsNewWithCopy Then
                LoadDistributionList()
                State.moDistributionList(moGridView.SelectedIndex).Validate()
                Return bIsOk
            End If
            If State.IsCommPlanDistNew = False Then
                DistributionId = GetSelectedGridText(moGridView, COL_COMMISSION_PLAN_DIST_ID_IDX)
            End If
            BindBoPropertiesToGridHeader()

            With TheCommPlanDist
                PopulateRateBOFromForm()
                commPaymentExists = oPlanDist.CommPaymentExist(.CommissionPlanId)
                If commPaymentExists = "Y" Then
                    State.moCommPlanDistPlanId = Guid.Empty
                    Throw New GUIException(Message.MSG_EXTRACT_LINKED_WITH_PLAN_SAVE_NOT_ALLOWED, Assurant.ElitaPlus.Common.ErrorCodes.MSG_EXTRACT_LINKED_WITH_OTHER_PLAN_SAVE_NOT_ALLOWED)
                Else
                    bIsDirty = .IsDirty
                    .Save()
                    EnableForEditRateButtons(False)
                End If
            End With

            If (bIsOk = True) Then
                If bIsDirty = True Then
                    MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
                Else
                    MasterPage.MessageController.AddError(Message.MSG_RECORD_NOT_SAVED, True)
                End If
            End If
            Return bIsOk
        End Function

        Private Sub PopulateRateBOFromForm()
            With TheCommPlanDist
                .CommissionPlanId = State.moCommPlanId 'TheCommPlanDist.CommissionPlanId
                CommonSourceOptionLogic()
            End With
            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub
        Private Sub CommonSourceOptionLogic()

            If moGridView.EditIndex = -1 Then Exit Sub
            Dim gRow As GridViewRow = moGridView.Rows(moGridView.EditIndex)
            Dim mocboPayeeType As DropDownList = DirectCast(gRow.Cells(COL_PAYEE_TYPE_XCD_IDX).FindControl("cboPayeeType"), DropDownList)
            Dim mocboEntityType As DropDownList = DirectCast(gRow.Cells(COL_ENTITY_ID_IDX).FindControl("cboEntityType"), DropDownList)
            Dim mocboCommPercentSourceXcd As DropDownList = DirectCast(gRow.Cells(COL_COMMISSIONS_SOURCE_XCD_IDX).FindControl("cboCommPercentSourceXcd"), DropDownList)
            Dim mocboActEntitySourceXcd As DropDownList = DirectCast(gRow.Cells(COL_ACT_ENT_SOURCE_XCD_IDX).FindControl("cboActEntitySourceXcd"), DropDownList)
            Dim moTextmoLowPriceText As TextBox = DirectCast(gRow.Cells(COL_COMMISSION_AMOUNT_IDX).FindControl("moLowPriceText"), TextBox)
            Dim moTextmoCommission_PercentText As TextBox = DirectCast(gRow.Cells(COL_COMMISSION_PERCENTAGE_IDX).FindControl("moCommission_PercentText"), TextBox)
            Dim textboxPosition As TextBox = DirectCast(gRow.Cells(COL_POSITION_IDX).FindControl("textBoxPosition"), TextBox)

            If (mocboCommPercentSourceXcd.Items.Count > 0) Then
                If mocboCommPercentSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_COMMBRKDOWN_OPTION_DIFFERENCE) Then
                    moTextmoCommission_PercentText.Text = "0.0000"
                End If
            End If

            PopulateBOProperty(TheCommPlanDist, PROPERTY_COMM_AMT, moTextmoLowPriceText)

            PopulateBOProperty(TheCommPlanDist, PROPERTY_COMM_PER, moTextmoCommission_PercentText)

            If (Not String.IsNullOrWhiteSpace(textboxPosition.Text)) Then
                PopulateBOProperty(TheCommPlanDist, PROPERTY_POSITION, textboxPosition)
            End If

            If mocboEntityType.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                PopulateBOProperty(TheCommPlanDist, PROPERTY_ENTITY_ID, mocboEntityType, True, False)
            End If

            If mocboCommPercentSourceXcd.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                PopulateBOProperty(TheCommPlanDist, PROPERTY_COMMISSIONS_XCD, mocboCommPercentSourceXcd, False, True)
            End If

            If mocboActEntitySourceXcd.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                PopulateBOProperty(TheCommPlanDist, PROPERTY_ACT_ENT_XCD, mocboActEntitySourceXcd, False, True)
            End If

            If mocboPayeeType.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                Dim tempPayeeTextbox As TextBox = New TextBox
                tempPayeeTextbox.Text = mocboPayeeType.SelectedValue
                PopulateBOProperty(TheCommPlanDist, PROPERTY_PAYEE_TYPE_XCD, tempPayeeTextbox)
            End If
        End Sub
        Private Function GetDropDownControlFromGrid(oDataGrid As GridView, cellPosition As Integer) As Control
            Dim oItem As GridViewRow = oDataGrid.Rows(oDataGrid.SelectedIndex)
            Dim oControl As Control

            For Each gridControl As Control In oItem.Cells(cellPosition).Controls

                If gridControl.GetType().FullName.Equals("System.Web.UI.WebControls.DropDownList") Then
                    oControl = gridControl
                End If
            Next

            Return oControl
        End Function
        Private Function DeleteSelectedDistribution(nIndex As Integer) As Boolean
            Dim bIsOk As Boolean = True
            Dim oPlanDist As CommPlanDistribution
            Dim commPaymentExists As String
            Try
                If State.IsNewWithCopy Then
                    If State.moDistributionList Is Nothing Then
                        LoadDistributionList()
                    End If
                    State.moDistributionList(nIndex) = Nothing
                Else
                    commPaymentExists = oPlanDist.CommPaymentExist(State.moCommPlanDistPlanId)

                    If commPaymentExists = "Y" Then
                        State.moCommPlanDistPlanId = Guid.Empty
                        Throw New GUIException(Message.MSG_EXTRACT_LINKED_WITH_PLAN_DELETE_NOT_ALLOWED, Assurant.ElitaPlus.Common.ErrorCodes.MSG_EXTRACT_LINKED_WITH_OTHER_PLAN_DELETE_NOT_ALLOWED)
                    Else
                        With TheCommPlanDist()
                            .Delete()
                            .Save()
                            State.moCommPlanDistPlanId = Guid.Empty
                            MasterPage.MessageController.AddSuccess(Message.DELETE_RECORD_CONFIRMATION, True)
                        End With
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
                bIsOk = False
            End Try
            Return bIsOk
        End Function

        Private Sub PopulateDistributionList(Optional ByVal oAction As String = ACTION_NONE)
            Dim oDistribution As CommPlanDistribution
            Dim oDataView As DataView

            If State.IsPlanNew = True And Not State.IsNewWithCopy Then
                Return ' We can not have Distribution if the plan is new
            End If

            Try
                If State.IsNewWithCopy Then
                    oDataView = oDistribution.getPlanList(Guid.Empty)
                    If Not oAction = ACTION_CANCEL_DELETE Then
                        LoadDistributionList()
                        'RePopulateDistributionListForPlan()
                    End If
                    If State.moDistributionList IsNot Nothing Then
                        oDataView = getDVFromArray(State.moDistributionList, oDataView.Table)
                    End If
                Else
                    If State.IsNewCloneCopyClicked = True Then
                        If TheCommPlan IsNot Nothing Then
                            State.moCommPlanId = TheCommPlan.Id
                            State.IsNewCloneCopyClicked = False
                        End If
                    End If

                    oDataView = oDistribution.getPlanList(State.moCommPlanId) 'TheCommPlanDist.Id)

                End If

                Select Case oAction
                    Case ACTION_NONE
                        SetPageAndSelectedIndexFromGuid(oDataView, Guid.Empty, moGridView, 0)
                        EnableForEditRateButtons(False)
                    Case ACTION_SAVE
                        SetPageAndSelectedIndexFromGuid(oDataView, GetGuidFromString(DistributionId), moGridView,
                                    moGridView.PageIndex)
                        EnableForEditRateButtons(False)
                    Case ACTION_CANCEL_DELETE
                        SetPageAndSelectedIndexFromGuid(oDataView, Guid.Empty, moGridView,
                                    moGridView.PageIndex)
                        EnableForEditRateButtons(False)
                    Case ACTION_EDIT
                        If State.IsNewWithCopy Then
                            DistributionId = State.moDistributionList(moGridView.SelectedIndex).Id.ToString
                            State.moCommPlanDistId = State.moDistributionList(moGridView.SelectedIndex).Id
                        Else
                            DistributionId = GetSelectedGridText(moGridView, COL_COMMISSION_PLAN_DIST_ID_IDX)
                            State.moCommPlanDistId = GetGuidFromString(DistributionId) ' Me.State.moDistributionList(moGridView.SelectedIndex).Id

                            State.MyBoDist = New CommPlanDistribution(State.moCommPlanDistId)

                        End If
                        SetPageAndSelectedIndexFromGuid(oDataView, GetGuidFromString(DistributionId), moGridView,
                                    moGridView.PageIndex, True)
                        EnableForEditRateButtons(True)
                    Case ACTION_NEW
                        If State.IsNewWithCopy Then oDataView.Table.DefaultView.Sort() = Nothing ' Clear sort, so that the new line shows up at the bottom

                        State.IsCommPlanDistNew = True
                        State.MyBoDist = New CommPlanDistribution()
                        Dim oRow As DataRow = oDataView.Table.NewRow
                        With TheCommPlanDist
                            DistributionId = .Id.ToString
                            oRow(DB_COMM_PLAN_DIST_ID) = .Id.ToByteArray
                            oRow(DB_COMM_PLAN_ID) = .CommissionPlanId.ToByteArray
                            oRow(DB_PAYEE_TYPE_XCD) = "PAYTP"
                            oRow(DB_ENTITY_ID) = .EntityId.ToByteArray
                            oRow(DB_COMM_AMT) = 0
                            oRow(DB_COMM_PER) = 0
                            oRow(DB_COMM_SRC_XCD) = "PM"
                            oRow(DB_POSITION) = 0
                        End With
                        oDataView.Table.Rows.Add(oRow)

                        State.moCommPlanDistId = GetGuidFromString(DistributionId)

                        SetPageAndSelectedIndexFromGuid(oDataView, GetGuidFromString(DistributionId), moGridView,
                                    moGridView.PageIndex, True)
                        EnableForEditRateButtons(True)

                End Select

                moGridView.DataSource = oDataView
                moGridView.DataBind()
                ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, moGridView)

            Catch ex As Exception
            End Try
        End Sub

        Private Sub RePopulateDistributionList(Optional ByVal oAction As String = ACTION_NONE)
            Dim oDistribution As CommPlanDistribution
            Dim oDataView As DataView

            If State.IsPlanNew = True And Not State.IsNewWithCopy Then
                Return ' We can not have Distribution if the plan is new
            End If

            Try
                If State.IsNewWithCopy Then
                    oDataView = oDistribution.getPlanList(Guid.Empty)
                    If Not oAction = ACTION_CANCEL_DELETE Then
                        LoadDistributionList()
                        'RePopulateDistributionListForPlan()
                    End If
                    If State.moDistributionList IsNot Nothing Then
                        oDataView = getDVFromArray(State.moDistributionList, oDataView.Table)
                    End If
                Else
                    oDataView = oDistribution.getPlanList(State.moCommPlanId) 'TheCommPlanDist.Id)
                End If

                SetPageAndSelectedIndexFromGuid(oDataView, GetGuidFromString(DistributionId), moGridView,
                            moGridView.PageIndex)
                EnableForEditRateButtons(False)

                moGridView.DataSource = oDataView
                moGridView.DataBind()
                ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, moGridView)

            Catch ex As Exception
            End Try
        End Sub

        Private Sub RePopulateDistributionListForPlan(Optional ByVal oAction As String = ACTION_NONE)
            Dim oDistribution As CommPlanDistribution
            Dim oDataView As DataView

            If State.IsPlanNew = True And Not State.IsNewWithCopy Then
                Return ' We can not have Distribution if the plan is new
            End If

            Try
                If State.IsNewWithCopy Then

                    oDataView = oDistribution.getPlanList(Guid.Empty)

                    If Not oAction = ACTION_CANCEL_DELETE Then
                        LoadDistributionList()
                    End If

                    If State.moDistributionList IsNot Nothing Then
                        oDataView = getDVFromArray(State.moDistributionList, oDataView.Table)
                    End If
                Else
                    If moGridView.Visible And moGridView.Rows.Count > 0 Then
                        oDataView = oDistribution.getPlanList(State.moCommPlanId) 'TheCommPlanDist.Id)
                    End If
                End If

                If moGridView.Visible And moGridView.Rows.Count > 0 Then
                    If Not String.IsNullOrWhiteSpace(DistributionId) Then
                        SetPageAndSelectedIndexFromGuid(oDataView, GetGuidFromString(DistributionId), moGridView, moGridView.PageIndex)
                    End If
                End If
                EnableForEditRateButtons(False)

                moGridView.DataSource = oDataView
                moGridView.DataBind()
                ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, moGridView)

            Catch ex As Exception
            End Try
        End Sub

        Private Function getDVFromArray(oArray() As CommPlanDistribution, oDtable As DataTable) As DataView
            Dim oRow As DataRow
            Dim oDistribution As CommPlanDistribution
            For Each oDistribution In oArray
                If oDistribution IsNot Nothing Then
                    oRow = oDtable.NewRow
                    oRow("COMMISSION_PLAN_ID") = oDistribution.CommissionPlanId.ToByteArray
                    oRow("PAYEE_TYPE_XCD") = oDistribution.PayeeTypeXcd
                    oRow("ENTITY_ID") = oDistribution.EntityId.ToByteArray
                    oRow("COMMISSION_AMOUNT") = oDistribution.CommissionAmount.Value
                    oRow("COMMISSION_PERCENTAGE") = oDistribution.CommissionPercent.Value
                    oRow("COMMISSIONS_SOURCE_XCD") = oDistribution.CommissionsPercentSourceXcd
                    oRow("POSITION") = oDistribution.Position.Value

                    oDtable.Rows.Add(oRow)
                End If
            Next
            'oDtable.DefaultView.Sort() = "POSITION"
            Return oDtable.DefaultView

        End Function

        Private Sub EnableEditDistributionButtons(bIsReadWrite As Boolean)
            ControlMgr.SetEnableControl(Me, BtnSaveRate_WRITE, bIsReadWrite)
            ControlMgr.SetEnableControl(Me, BtnCancelRate, bIsReadWrite)
        End Sub

        Private Sub EnableNewDistributionButtons(bIsReadWrite As Boolean)
            ControlMgr.SetEnableControl(Me, BtnNewRate_WRITE, bIsReadWrite)
        End Sub

        Private Sub EnableForEditRateButtons(bIsReadWrite As Boolean)
            EnableNewDistributionButtons(Not bIsReadWrite)
            EnableEditDistributionButtons(bIsReadWrite)
        End Sub

        Private Sub PopulateDistribution()
            If State.IsNewWithCopy Then
                With State.moDistributionList(moGridView.SelectedIndex)
                    SetSelectedGridText(moGridView, COL_PAYEE_TYPE_XCD_IDX, .PayeeTypeXcd.ToString)
                    'Me.SetSelectedGridText(moGridView, COL_ENTITY_ID_IDX, .EntityId)
                    SetSelectedGridText(moGridView, COL_COMMISSION_AMOUNT_IDX, .CommissionAmount.ToString)
                    SetSelectedGridText(moGridView, COL_COMMISSION_PERCENTAGE_IDX, .CommissionPercent.ToString)
                    SetSelectedGridText(moGridView, COL_COMMISSIONS_SOURCE_XCD_IDX, .CommissionsPercentSourceXcd.ToString)
                    SetSelectedGridText(moGridView, COL_ACT_ENT_SOURCE_XCD_IDX, .ActEntitySourceXcd.ToString)
                    SetSelectedGridText(moGridView, COL_POSITION_IDX, .Position.ToString)
                End With
            Else
                With TheCommPlanDist
                    SetSelectedGridText(moGridView, COL_PAYEE_TYPE_XCD_IDX, .PayeeTypeXcd.ToString)
                    'Me.SetSelectedGridText(moGridView, COL_ENTITY_ID_IDX, .EntityId)
                    SetSelectedGridText(moGridView, COL_COMMISSION_AMOUNT_IDX, .CommissionAmount.ToString)
                    SetSelectedGridText(moGridView, COL_COMMISSION_PERCENTAGE_IDX, .CommissionPercent.ToString)
                    SetSelectedGridText(moGridView, COL_COMMISSIONS_SOURCE_XCD_IDX, .CommissionsPercentSourceXcd.ToString)
                    SetSelectedGridText(moGridView, COL_ACT_ENT_SOURCE_XCD_IDX, .ActEntitySourceXcd.ToString)
                    SetSelectedGridText(moGridView, COL_POSITION_IDX, .Position.ToString)
                End With
            End If
        End Sub

        Private Function GetSearchParmDealerPlanList() As CommPlanData
            Dim oCommPlanData As CommPlanData = New CommPlanData

            With oCommPlanData
                .companyIds = ElitaPlusIdentity.Current.ActiveUser.Companies
                .dealerId = multipleDropControl.SelectedGuid
            End With

            Return oCommPlanData

        End Function

        Protected Function CheckNull(objGrid As Object, objParam2 As String) As String
            If Object.ReferenceEquals(objGrid, DBNull.Value) Then
                Return String.Empty
            ElseIf TypeOf objGrid Is Byte() Then
                Return GetGuidStringFromByteArray(objGrid)
            Else
                If objGrid.ToString().Equals(Guid.Empty.ToString()) Then
                    Return String.Empty
                End If

                If Not String.IsNullOrWhiteSpace(objParam2) Then
                    If objParam2.Equals("COMMISSION_AMOUNT") Then
                        Return GetAmountFormattedToVariableString(objGrid)
                    End If
                End If

                If Not String.IsNullOrWhiteSpace(objParam2) Then
                    If objParam2.Equals("COMMISSION_PERCENTAGE") Then
                        Return GetAmountFormattedDoubleString(objGrid, "N4")
                    End If
                End If

                Return objGrid.ToString()
            End If
        End Function

        Private Sub ValidatePositionNo()
            If moGridView.EditIndex = -1 Then Exit Sub
            Dim gRow As GridViewRow = moGridView.Rows(moGridView.EditIndex)
            Dim textBoxPosition As TextBox = DirectCast(gRow.Cells(COL_POSITION_IDX).FindControl("textBoxPosition"), TextBox)

            If moGridView.Rows.Count = 0 Then
                If textBoxPosition IsNot Nothing Then
                    If Not String.IsNullOrWhiteSpace(textBoxPosition.Text) Then
                        If Convert.ToDecimal(textBoxPosition.Text) = 0 Then
                            'Can not be zero and null
                            Throw New GUIException(Message.MSG_POSITION_SHOULD_NOT_BE_ZERO_NULL, Assurant.ElitaPlus.Common.ErrorCodes.MSG_POSITION_VALUE_ZERO_NULL_NOT_ALLOWED)
                        End If
                    Else
                        ' Can not be null
                        Throw New GUIException(Message.MSG_POSITION_SHOULD_NOT_BE_ZERO_NULL, Assurant.ElitaPlus.Common.ErrorCodes.MSG_POSITION_VALUE_ZERO_NULL_NOT_ALLOWED)
                    End If
                End If
            ElseIf moGridView.Rows.Count > 0 Then
                If textBoxPosition IsNot Nothing Then
                    If Not String.IsNullOrWhiteSpace(textBoxPosition.Text) Then
                        If Convert.ToDecimal(textBoxPosition.Text) = 0 Then
                            'Can not be zero
                            Throw New GUIException(Message.MSG_POSITION_SHOULD_NOT_BE_ZERO_NULL, Assurant.ElitaPlus.Common.ErrorCodes.MSG_POSITION_VALUE_ZERO_NULL_NOT_ALLOWED)
                        Else
                            If moGridView.Rows.Count > 1 Then
                                Dim oPlanDist As CommPlanDistribution
                                Dim positionExistsFlag As String

                                'Call procedure to validate repeatative position number
                                positionExistsFlag = oPlanDist.CheckPositionExists(Convert.ToInt16(textBoxPosition.Text), State.moCommPlanDistId, State.moCommPlanId)

                                If positionExistsFlag = "Y" Then
                                    Throw New GUIException(Message.MSG_POSITION_VALUE_CAN_NOT_BE_REPEATED, Assurant.ElitaPlus.Common.ErrorCodes.MSG_DUPLICATE_POSITION_VALUE_NOT_ALLOWED)
                                End If

                            End If
                        End If
                    Else
                        ' Can not be null
                        Throw New GUIException(Message.MSG_POSITION_SHOULD_NOT_BE_ZERO_NULL, Assurant.ElitaPlus.Common.ErrorCodes.MSG_POSITION_VALUE_ZERO_NULL_NOT_ALLOWED)
                    End If
                End If
            End If
        End Sub

        Private Sub ValidateCommAmountAndPercent()
            If moGridView.EditIndex = -1 Then Exit Sub

            Dim gRow As GridViewRow = moGridView.Rows(moGridView.EditIndex)
            Dim textBoxAmt As TextBox = DirectCast(gRow.Cells(COL_COMMISSION_AMOUNT_IDX).FindControl("moLowPriceText"), TextBox)
            Dim textBoxAmtPercent As TextBox = DirectCast(gRow.Cells(COL_COMMISSION_PERCENTAGE_IDX).FindControl("moCommission_PercentText"), TextBox)

            If textBoxAmt IsNot Nothing And textBoxAmtPercent IsNot Nothing Then
                If String.IsNullOrWhiteSpace(textBoxAmt.Text) And String.IsNullOrWhiteSpace(textBoxAmtPercent.Text) Then
                    ' Either one should be present
                    Throw New GUIException(Message.MSG_EITHER_PERCENTAGE_OR_AMOUNT_NEEDED, Assurant.ElitaPlus.Common.ErrorCodes.MSG_EITHER_PERCENTAGE_OR_AMOUNT_REQUIRED)
                End If

            End If
        End Sub
#End Region
#End Region
    End Class
End Namespace
