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
            Public LastErrMsg As String
            Public IsCommPlanDistNew As Boolean = False
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public boChanged As Boolean = False

            Public IsCommPerGreaterThanHundred As Boolean
            Public IsPmComCombination As Boolean
            Public IsAmountAndPercentBothPresent As Boolean
            Public IsDiffSelectedTwice As Boolean
            Public IsDiffNotSelectedOnce As Boolean
            Public IsBucketIncomingSelected As Boolean
            Public IsDealerConfiguredForSourceXcd As Boolean = False
            Public IsCompanyConfiguredForSourceXcd As Boolean = False
            Public IsIgnorePremiumSetYesForContract As Boolean = False

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
            Me.State.moCommPlanId = CType(Me.CallingParameters, Guid)

            If Me.State.moCommPlanId.Equals(Guid.Empty) Then
                Me.State.IsCommPlanDistNew = True
                ClearPlan()
                SetPeriodButtonsState(True)
                PopulatePeriod()
                PopulateDistributionList()
                TheDealerControl.ChangeEnabledControlProperty(True)
            Else
                Me.State.IsCommPlanDistNew = False
                SetPeriodButtonsState(False)
                PopulatePeriod()
                PopulateDistributionList()
                TheDealerControl.ChangeEnabledControlProperty(False)
            End If
            If Not TheDealerControl.SelectedGuid.Equals(Guid.Empty) Then
            End If
        End Sub

#End Region


#Region "Constants- Plan"

        Public Const URL As String = "CommissionPlanForm.aspx"

        ' Property Name
        Public Const NOTHING_SELECTED As Integer = 0
        Public Const COMMISSION_PERIOD_ID_PROPERTY As String = "CommissionPeriodId"
        Public Const DEALER_ID_PROPERTY As String = "DealerId"
        Public Const EFFECTIVE_DATE_PROPERTY As String = "EffectiveDate"
        Public Const EXPIRATION_DATE_PROPERTY As String = "ExpirationDate"
        Public Const REFERENCE_SOURCE_PROPERTY As String = "ReferenceSource"
        Public Const CODE_PROPERTY As String = "Code"
        Public Const DESCRIPTION_PROPERTY As String = "Description"
        Public Const PAGETITLE As String = "COMMISSION_BREAKDOWN"
        Public Const PAGETAB As String = "TABLES"
        Public Const Payee_Type_Dealer_Group As String = "1"
        Public Const Payee_Type_Dealer As String = "2"
        Public Const Payee_Type_Comm_Entity As String = "4"

        Public Const COMPUTE_METHOD_COMPUTE_ON_NET As String = "N"
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
        Private Const COL_POSITION_IDX As Integer = 9

        Private Const PROPERTY_PAYEE_TYPE_XCD As String = "PayeeTypeXcd"
        Private Const PROPERTY_ENTITY_ID As String = "EntityId"
        Private Const PROPERTY_COMM_AMT As String = "CommissionAmount"
        Private Const PROPERTY_COMM_PER As String = "CommissionPercent"
        Private Const PROPERTY_COMMISSIONS_XCD As String = "CommissionsPercentSourceXcd"
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
        Private Const DB_POSITION As Integer = 7

#End Region

#Region "Variables"
        Private moComPlanExpirationData As CommPlanDataExp
        Private moExpirationData As CommPlanData
        Private moDistribution As CommPlanDistribution
        Private mbIsNewRate As Boolean

#End Region

#Region "Properties"
        Public ReadOnly Property HasCompanyConfigeredForSourceXcd() As Boolean
            Get
                Dim isCompanyConfiguredForSourceXcd As Boolean
                isCompanyConfiguredForSourceXcd = False
                Dim oCompany As Company
                Dim oDataView As DataView = LookupListNew.GetUserCompanyLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, ElitaPlusIdentity.Current.ActiveUser.Id)
                For Each row As DataRowView In oDataView
                    Dim oCompany_id As Guid = New Guid(CType(row.Item("id"), Byte()))
                    oCompany = New Company(oCompany_id)
                    isCompanyConfiguredForSourceXcd = False
                    If Not oCompany.AttributeValues Is Nothing Then
                        If oCompany.AttributeValues.Contains("NEW_COMMISSION_MODULE_CONFIGURED") Then
                            'If oCompany.AttributeValues.Value(Codes.NEW_COMMISSION_MODULE_CONFIGURED) = Codes.EXT_YESNO_Y Then
                            If oCompany.AttributeValues.Value(Codes.NEW_COMMISSION_MODULE_CONFIGURED) = Codes.YESNO_Y Then
                                isCompanyConfiguredForSourceXcd = True
                                Exit For
                            End If
                        End If
                    End If
                Next

                Return isCompanyConfiguredForSourceXcd

            End Get
        End Property

        Private ReadOnly Property TheCommPlan() As CommPlan
            Get
                If Me.State.MyBo Is Nothing Then
                    If Me.State.IsCommPlanDistNew = True Then
                        ' For creating, inserting
                        Me.State.MyBo = New CommPlan
                        Me.State.moCommPlanId = Me.State.MyBo.Id
                    Else
                        ' For updating, deleting
                        Me.State.MyBo = New CommPlan(Me.State.moCommPlanId)
                    End If
                End If
                BindBoPropertiesToLabels(Me.State.MyBo)
                Return Me.State.MyBo
            End Get
        End Property

        Private ReadOnly Property TheCommPlanDist() As CommPlanDistribution
            Get
                If Me.State.MyBoDist Is Nothing Then
                    If Me.State.IsCommPlanDistNew = True Then
                        ' For creating, inserting
                        Me.State.MyBoDist = New CommPlanDistribution
                        Me.State.moCommPlanDistId = Me.State.MyBoDist.Id
                        moDistribution = New CommPlanDistribution
                        DistributionId = moDistribution.Id.ToString
                    Else
                        ' For updating, deleting
                        Me.State.MyBoDist = New CommPlanDistribution(Me.State.moCommPlanDistId)
                        If DistributionId = "" Then
                            DistributionId = Guid.Empty.ToString
                        End If

                        moDistribution = New CommPlanDistribution(Me.State.moCommPlanDistId)
                    End If
                End If
                Return Me.State.MyBoDist
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
            Set(ByVal Value As String)
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

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Handlers-Init"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                Me.MasterPage.MessageController.Clear_Hide()
                ClearLabelsErrSign()
                ClearGridHeaders(moGridView)
                If Not Page.IsPostBack Then
                    Me.MasterPage.MessageController.Clear()
                    Me.MasterPage.UsePageTabTitleInBreadCrum = False
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")
                    UpdateBreadCrum()
                    Me.SetFormTab(PAGETAB)
                    Me.SetGridItemStyleColor(moGridView)
                    Me.TranslateGridHeader(Me.moGridView)
                    Me.TranslateGridControls(moGridView)
                    Me.SetStateProperties()

                    If HasCompanyConfigeredForSourceXcd() Then
                        'US-521672
                        Me.moGridView.Visible = True
                    End If

                    'US-521672
                    SetGridSourceXcdLabelFromBo()

                    Me.AddControlMsg(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO,
                                     Me.MSG_TYPE_CONFIRM, True)
                    Me.AddCalendar(Me.BtnEffectiveDate_WRITE, Me.moEffectiveText_WRITE)
                    Me.AddCalendar(Me.BtnExpirationDate_WRITE, Me.moExpirationText_WRITE)
                Else
                    If HasCompanyConfigeredForSourceXcd() Then
                        Me.moGridView.Visible = True
                    End If
                    'US-521672
                    SetGridSourceXcdLabelFromBo()
                    CheckIfComingFromConfirm()
                End If
                btnBack.Visible = True
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)
        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub GoBack()
            Dim retType As New CommissionPlanSearchForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.moCommPlanId, Me.State.boChanged)
            Me.ReturnToCallingPage(retType)
        End Sub

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try
                If IsDirtyPlanBO() = True Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM,
                                            Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    GoBack()
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                GoBack()
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub SavePlanChanges()
            If ApplyPlanChanges() = True Then
                Me.State.boChanged = True
                ClearDistributionGrid()
                PopulatePeriod()
                Me.State.IsCommPlanDistNew = False
                SetPeriodButtonsState(False)
            End If
        End Sub

        Private Sub btnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
            Try
                'Dim ExpirationOverlapCount As Integer = ExpirationOverlapping()
                'If ExpirationOverlapCount = 0 Then
                SavePlanChanges()
                SetGridSourceXcdLabelFromBo()
                'Else
                '    Throw New GUIException(Message.MSG_EXPIRATION_DATE_IS_OVERLAPPING_WITH_OTHER_PLAN, Assurant.ElitaPlus.Common.ErrorCodes.MSG_EXPIRATION_DATE_IS_OVERLAPPING)
                'End If

            Catch ex As Exception
                SetGridControls(moGridView, True)
                PopulateDistributionList(ACTION_CANCEL_DELETE)
                SetGridSourceXcdLabelFromBo()
                SetGridControls(moGridView, True)
                EnableDisableControls(Me.moCoverageEditPanel, True)
                SetGridSourceXcdLabelFromBo()
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try
                ClearPlan()
                PopulatePeriod()
            Catch ex As Exception
                SetGridSourceXcdLabelFromBo()
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnDelete_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click
            Try
                If DeletePeriod() = True Then
                    Me.State.boChanged = True
                    GoBack()
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CreateNew()
            Me.State.MyBo = Nothing
            Me.State.moCommPlanId = Guid.Empty
            Me.State.IsCommPlanDistNew = True
            ClearPlan()
            SetPeriodButtonsState(True)
            PopulatePeriod()
            TheDealerControl.ChangeEnabledControlProperty(True)
        End Sub

        Private Sub btnNew_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
            Try
                If IsDirtyPlanBO() = True Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
                Else
                    CreateNew()
                End If
            Catch ex As Exception
                SetGridSourceXcdLabelFromBo()
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CreateNewCopy()
            Dim newObj As New CommPlan
            SetGridControls(moGridView, True)
            Me.State.MyBo = Nothing
            newObj.Copy(TheCommPlan)
            Me.State.IsCommPlanDistNew = True
            Me.State.MyBo = newObj

            EnableDateFields()
            SetPeriodButtonsState(True)
            ClearDistributionGrid()
        End Sub

        Private Sub btnCopy_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy_WRITE.Click
            Try
                If IsDirtyPlanBO() = True Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
                Else
                    CreateNewCopy()
                End If
            Catch ex As Exception
                SetGridSourceXcdLabelFromBo()
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Handlers-DropDowns"

        Private Sub OnFromDrop_Changed(ByVal fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
           Handles multipleDropControl.SelectedDropChanged
            Try
                If HasCompanyConfigeredForSourceXcd() Then
                    EnableDateFields()
                    Me.moGridView.Visible = True
                    BtnNewRate_WRITE.Visible = True
                Else
                    'EnableDateFields()
                    Me.moGridView.Visible = False
                    BtnNewRate_WRITE.Visible = False

                End If
                btnBack.Visible = True
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Handlers-Labels"
        Protected Sub BindBoPropertiesToLabels(ByVal oPlan As CommPlan)
            Me.BindBOPropertyToLabel(oPlan, DEALER_ID_PROPERTY, Me.TheDealerControl.CaptionLabel)
            Me.BindBOPropertyToLabel(oPlan, EFFECTIVE_DATE_PROPERTY, moEffectiveLabel)
            Me.BindBOPropertyToLabel(oPlan, EXPIRATION_DATE_PROPERTY, moExpirationLabel)
            Me.BindBOPropertyToLabel(oPlan, CODE_PROPERTY, Me.LabelCode)
            Me.BindBOPropertyToLabel(oPlan, DESCRIPTION_PROPERTY, Me.LabelDescription)
        End Sub

        Protected Sub BindBoPropertiesToLabels(ByVal oPlan As CommPlanDistribution)
            Me.BindBOPropertyToLabel(oPlan, DEALER_ID_PROPERTY, Me.TheDealerControl.CaptionLabel)
            Me.BindBOPropertyToLabel(oPlan, EFFECTIVE_DATE_PROPERTY, moEffectiveLabel)
            Me.BindBOPropertyToLabel(oPlan, EXPIRATION_DATE_PROPERTY, moExpirationLabel)
            Me.BindBOPropertyToLabel(oPlan, CODE_PROPERTY, Me.LabelCode)
            Me.BindBOPropertyToLabel(oPlan, DESCRIPTION_PROPERTY, Me.LabelDescription)
        End Sub

        Public Sub ClearLabelsErrSign()
            Me.ClearLabelErrSign(Me.TheDealerControl.CaptionLabel)
            Me.ClearLabelErrSign(moEffectiveLabel)
            Me.ClearLabelErrSign(moExpirationLabel)
            Me.ClearLabelErrSign(LabelCode)
            Me.ClearLabelErrSign(LabelDescription)
        End Sub
#End Region

#End Region

#Region "Button-Management"

        Private Sub SetPeriodButtonsState(ByVal bIsNew As Boolean)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, Not bIsNew)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, Not bIsNew)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, Not bIsNew)
        End Sub

        Private Sub EnableEffective(ByVal bIsEnable As Boolean)
            ControlMgr.SetEnableControl(Me, moEffectiveText_WRITE, bIsEnable)
            ControlMgr.SetVisibleControl(Me, BtnEffectiveDate_WRITE, bIsEnable)
        End Sub

        Private Sub EnableExpiration(ByVal bIsEnable As Boolean)
            ControlMgr.SetEnableControl(Me, moExpirationText_WRITE, bIsEnable)
            ControlMgr.SetVisibleControl(Me, BtnExpirationDate_WRITE, bIsEnable)
        End Sub

        Private Sub EnableDateFields()
            Select Case ExpirationCount
                Case 0  ' New Record
                    EnableEffective(True)
                    EnableExpiration(True)
                    'Yesterday
                    moEffectiveText_WRITE.Text = Date.Now().AddDays(-1).ToString("dd-MMM-yyyy", CultureInfo.CurrentCulture)
                    ' Next Year
                    moExpirationText_WRITE.Text = Date.Now().AddYears(1).ToString("dd-MMM-yyyy", CultureInfo.CurrentCulture)
                Case 1
                    If Me.State.IsCommPlanDistNew = True Then
                        'New Record
                        ' Next Day MaxExpiration
                        moEffectiveText_WRITE.Text = MaxExpiration.AddDays(1).ToString("dd-MMM-yyyy", CultureInfo.CurrentCulture)
                        ' Next Year after MaxExpiration 
                        moExpirationText_WRITE.Text = MaxExpiration.AddYears(1).ToString("dd-MMM-yyyy", CultureInfo.CurrentCulture)
                        EnableEffective(False)
                    Else
                        ' Modify the only record
                        EnableEffective(True)
                    End If
                    ControlMgr.SetEnableControl(Me, BtnExpirationDate_WRITE, True)
                Case Else   ' There is more than one record
                    EnableExpiration(True)
                    If Me.State.IsCommPlanDistNew = True Then
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
                    EnableEffective(False)
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

            If Me.State.IsCommPlanDistNew = True Then
                TheDealerControl.NothingSelected = True
            Else
                TheDealerControl.SelectedGuid = TheCommPlan.DealerId
            End If

        End Sub

        Private Sub PopulateDatesFromBO()
            Me.PopulateControlFromBOProperty(moEffectiveText_WRITE, TheCommPlan.EffectiveDate)
            Me.PopulateControlFromBOProperty(moExpirationText_WRITE, TheCommPlan.ExpirationDate)
            Me.PopulateControlFromBOProperty(moExpirationText_WRITE, TheCommPlan.ExpirationDate)
        End Sub

        Private Sub PupulateCodeDescFromBO()
            Me.PopulateControlFromBOProperty(Me.TextBoxCode, TheCommPlan.Code)
            Me.PopulateControlFromBOProperty(Me.TextBoxDescription, TheCommPlan.Description)
        End Sub

        Private Sub PopulatePeriod()
            Try
                PopulateDealer()
                PopulateDatesFromBO()
                EnableDateFields()
                'PopulateTolerance()
                PupulateCodeDescFromBO()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

#End Region

#Region "Clear"

        Private Sub ClearDealer()
            If Me.State.IsCommPlanDistNew = True Then
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
            If (Not Me.State Is Nothing) Then
                If (Not Me.State Is Nothing) Then
                    Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(PAGETITLE)
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
                End If
            End If
        End Sub

        Private Sub PopulatePeriodBOFromForm(ByVal oPlan As CommPlan)
            With oPlan
                ' DropDowns
                .DealerId = TheDealerControl.SelectedGuid 'Me.GetSelectedItem(moDealerDrop_WRITE)
                Me.PopulateBOProperty(oPlan, REFERENCE_SOURCE_PROPERTY, "ELP_DEALER")
                Me.PopulateBOProperty(oPlan, CODE_PROPERTY, Me.TextBoxCode)
                Me.PopulateBOProperty(oPlan, DESCRIPTION_PROPERTY, Me.TextBoxDescription)
                ' Texts
                Me.PopulateBOProperty(oPlan, EFFECTIVE_DATE_PROPERTY, moEffectiveText_WRITE)
                Me.PopulateBOProperty(oPlan, EXPIRATION_DATE_PROPERTY, moExpirationText_WRITE)

            End With

            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub
        Private Sub PopulatePeriodBOFromForm(ByVal oPlan As CommPlanDistribution)
            With oPlan
                Me.PopulateBOProperty(oPlan, REFERENCE_SOURCE_PROPERTY, "ELP_DEALER")
                Me.PopulateBOProperty(oPlan, CODE_PROPERTY, Me.TextBoxCode)
                Me.PopulateBOProperty(oPlan, DESCRIPTION_PROPERTY, Me.TextBoxDescription)
                ' Texts
                Me.PopulateBOProperty(oPlan, EFFECTIVE_DATE_PROPERTY, moEffectiveText_WRITE)
                Me.PopulateBOProperty(oPlan, EXPIRATION_DATE_PROPERTY, moExpirationText_WRITE)

            End With

            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub
        Private Function IsDirtyPlanBO() As Boolean
            Dim bIsDirty As Boolean = True
            Dim oPlan As CommPlan

            oPlan = TheCommPlan
            With oPlan
                PopulatePeriodBOFromForm(Me.State.MyBo)
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
                    datesOverlapFlag = oPlan.CheckDatesOverLap(oPlan.DealerId, oPlan.ExpirationDate)
                    If datesOverlapFlag = "N" Then
                        oPlan.Save()
                        Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    Else
                        Throw New GUIException(Message.MSG_EXPIRATION_DATE_IS_OVERLAPPING_WITH_OTHER_PLAN, Assurant.ElitaPlus.Common.ErrorCodes.MSG_EXPIRATION_DATE_IS_OVERLAPPING)
                    End If
                Else
                    Me.DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                End If
                Me.State.IsCommPlanDistNew = False
                SetPeriodButtonsState(False)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
                bIsOk = False
            End Try
            'SaveDistributionChanges()
            Return bIsOk
        End Function

        Private Function DeletePeriod() As Boolean
            Dim bIsOk As Boolean = True
            Dim oPlan As CommPlan
            Dim commPaymentExists As String

            Try
                oPlan = TheCommPlan
                commPaymentExists = oPlan.CommPaymentExist(oPlan.Id)
                If commPaymentExists = "N" Then
                    ErrControllerMaster.AddErrorAndShow("DATES_OVERLAPPING")
                Else
                    With oPlan
                        .Delete()
                        .Save()
                    End With
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
                bIsOk = False
            End Try
            Return bIsOk
        End Function

#End Region

#Region "Handlers-Grid"

        Public Sub RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

#End Region

        Private Sub ChangeControlEnabledProperty(ByVal ctrl As Control, ByVal enabled As Boolean)
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
            Me.moGridView.DataSource = Nothing
            Me.moGridView.DataBind()
        End Sub

#Region "State-Management"

#Region "Period State-Management"

        Protected Sub ComingFromSaveDistribution100PerAlert()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                ' Return from the Back Button

                Select Case confResponse
                    Case Me.MSG_VALUE_YES
                        SaveDistributionChanges()
                        SetGridSourceXcdLabelFromBo()
                    Case Me.MSG_VALUE_NO
                        'Do not save data
                        SetGridControls(moGridView, True)
                        FillSourceXcdDropdownList()
                        FillEntityDropDownList()
                        FillPayeeTypeDropDownList()
                        SetGridSourceXcdDropdownFromBo()
                        SetGridSourceXcdLabelFromBo()
                End Select
            End If

        End Sub

        Protected Sub ComingFromBack()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                ' Return from the Back Button

                Select Case confResponse
                    Case Me.MSG_VALUE_YES
                        ' Save and go back to Search Page
                        If ApplyPlanChanges() = True Then
                            Me.State.boChanged = True
                            GoBack()
                        End If
                    Case Me.MSG_VALUE_NO
                        ' Go back to Search Page
                        GoBack()
                End Select
            End If

        End Sub

        Protected Sub ComingFromNew()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                ' Return from the New Button

                Select Case confResponse
                    Case Me.MSG_VALUE_YES
                        ' Save and create a new BO
                        If ApplyPlanChanges() = True Then
                            CreateNew()
                        End If
                    Case Me.MSG_VALUE_NO
                        ' create a new BO
                        CreateNew()
                End Select
            End If

        End Sub

        Protected Sub ComingFromNewCopy()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                ' Return from the New Copy Button

                Select Case confResponse
                    Case Me.MSG_VALUE_YES
                        ' Save and create a new Copy BO
                        If ApplyPlanChanges() = True Then
                            Me.State.boChanged = True
                            CreateNewCopy()
                        End If
                    Case Me.MSG_VALUE_NO
                        ' create a new BO
                        CreateNewCopy()
                End Select
            End If

        End Sub

        Protected Sub ComingFromNewDistribution()
            Try
                Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value

                If Not confResponse = String.Empty Then
                    ' Return from the Back Button

                    Select Case confResponse
                        Case Me.MSG_BTN_OK
                            Me.State.IsCommPlanDistNew = True
                            DistributionId = Guid.Empty.ToString
                            PopulateDistributionList(ACTION_NEW)
                            'SetGridControls(moGridView, True)
                            FillSourceXcdDropdownList()
                            FillEntityDropDownList()
                            FillPayeeTypeDropDownList()
                            SetGridSourceXcdDropdownFromBo()
                            SetGridSourceXcdLabelFromBo()
                            SetGridControls(moGridView, True)
                            EnableDisableControls(Me.moCoverageEditPanel, True)
                            setbuttons(False)
                            btnBack.Visible = True
                    End Select
                End If
            Catch ex As Exception
                ' SetStateProperties()
                FillSourceXcdDropdownList()
                FillEntityDropDownList()
                FillPayeeTypeDropDownList()
                SetGridSourceXcdDropdownFromBo()
                SetGridSourceXcdLabelFromBo()
                SetGridControls(moGridView, True)
                EnableDisableControls(Me.moCoverageEditPanel, True)
                setbuttons(False)
                btnBack.Visible = True
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub ComingFromNewDistributionForEitherAmtPer()
            Try
                Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value

                If Not confResponse = String.Empty Then
                    ' Return from the Back Button

                    Select Case confResponse
                        Case Me.MSG_BTN_OK
                            SetGridControls(moGridView, True)
                            FillSourceXcdDropdownList()
                            FillEntityDropDownList()
                            FillPayeeTypeDropDownList()
                            SetGridSourceXcdDropdownFromBo()
                            SetGridSourceXcdLabelFromBo()
                            SetGridControls(moGridView, True)
                            EnableDisableControls(Me.moCoverageEditPanel, True)
                            setbuttons(False)
                            btnBack.Visible = True
                    End Select
                End If
            Catch ex As Exception
                ' SetStateProperties()
                FillSourceXcdDropdownList()
                FillEntityDropDownList()
                FillPayeeTypeDropDownList()
                SetGridSourceXcdDropdownFromBo()
                SetGridSourceXcdLabelFromBo()
                SetGridControls(moGridView, True)
                EnableDisableControls(Me.moCoverageEditPanel, True)
                setbuttons(False)
                btnBack.Visible = True
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Breadkdown State-Management"

        Protected Sub ComingFromEditTolerance()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                ' Return from the Back Button

                Select Case confResponse
                    Case Me.MSG_VALUE_YES
                        ' Save and go back to Search Page
                        If ApplyPlanChanges() = True Then
                            Me.State.boChanged = True
                            'EditTolerance()
                        End If
                    Case Me.MSG_VALUE_NO

                End Select
            End If

        End Sub

#End Region

        Protected Sub CheckIfComingFromConfirm()
            Try
                Select Case Me.State.ActionInProgress
                    ' Period
                    Case ElitaPlusPage.DetailPageCommand.OK
                        If Me.State.IsAmountAndPercentBothPresent Then
                            ComingFromNewDistributionForEitherAmtPer()
                        Else
                            ComingFromNewDistribution()
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ComingFromBack()
                    Case ElitaPlusPage.DetailPageCommand.New_
                        ComingFromNewCopy()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        ComingFromNewCopy()


                    Case ElitaPlusPage.DetailPageCommand.Redirect_
                        ComingFromEditTolerance()
                    Case ElitaPlusPage.DetailPageCommand.Accept
                        'Alert for accepting greater than 100% commission percentage
                        ComingFromSaveDistribution100PerAlert()

                        TheDealerControl.ChangeEnabledControlProperty(False)
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        Me.MasterPage.MessageController.AddErrorAndShow(Me.State.LastErrMsg)
                End Select

                'Clean after consuming the action
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                Me.HiddenSaveChangesPromptResponse.Value = String.Empty
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Acct Source Xcd Option Bucket Logic"
        Private Sub ValidatePmCertSourceLogic()
            'Avoid Price Metrics and Cert Commission combination
            ValidatePmCertCommSourceXcd()

            If Me.State.IsPmComCombination Then
                Throw New GUIException(Message.MSG_PRICEMETRICS_CERTCALC_NOT_ALLOWED, Assurant.ElitaPlus.Common.ErrorCodes.MSG_PRICE_METRICS_AND_CERT_COMM_NOT_ALLOWED)
            End If

        End Sub

        Private Sub ValidatePmCertCommSourceXcd()
            Dim countPM As Integer = 0
            Dim countCertComm As Integer = 0
            For pageIndexk As Integer = 0 To Me.moGridView.PageCount - 1
                Me.moGridView.PageIndex = pageIndexk
                Dim rowNum As Integer = Me.moGridView.Rows.Count
                For i As Integer = 0 To rowNum - 1
                    Dim gRow As GridViewRow = moGridView.Rows(i)
                    If gRow.RowType = DataControlRowType.DataRow Then
                        'If moGridView.EditIndex = -1 Then Exit Sub
                        Dim mocboCommPercentSourceXcd As DropDownList = DirectCast(gRow.Cells(COL_COMMISSIONS_SOURCE_XCD_IDX).FindControl("cboCommPercentSourceXcd"), DropDownList)
                        Dim mollblCommPercentSourceXcd As Label = DirectCast(gRow.Cells(COL_COMMISSIONS_SOURCE_XCD_IDX).FindControl("lblCommPercentSourceXcd"), Label)
                        Dim mollblCommPercentSourceXcdCode As Label = DirectCast(gRow.Cells(COL_COMMISSIONS_SOURCE_XCD_IDX).FindControl("lblCommPercentSourceXcdCode"), Label)

                        If Not mocboCommPercentSourceXcd Is Nothing Then
                            If mocboCommPercentSourceXcd.Items.Count > 0 Then
                                If mocboCommPercentSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_COMMBRKDOWN_OPTION_CERTCOMM) Then
                                    countPM = countPM + 1
                                End If

                                If mocboCommPercentSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_COMMBRKDOWN_OPTION_PRICEMATRIX) Then
                                    countCertComm = countCertComm + 1
                                End If
                            End If
                        End If

                        If Not mollblCommPercentSourceXcdCode Is Nothing Then
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
                Me.State.IsPmComCombination = True
            Else
                Me.State.IsPmComCombination = False
            End If
        End Sub

        Private Function IsCommPerGreaterThanHundred() As Boolean
            Dim countCommPer As Decimal = 0
            Dim isCommPerMoreThanHundred As Boolean
            For pageIndexk As Integer = 0 To Me.moGridView.PageCount - 1
                Me.moGridView.PageIndex = pageIndexk
                Dim rowNum As Integer = Me.moGridView.Rows.Count
                For i As Integer = 0 To rowNum - 1
                    Dim gRow As GridViewRow = moGridView.Rows(i)
                    If gRow.RowType = DataControlRowType.DataRow Then
                        Dim molblCommPer As Label = DirectCast(gRow.Cells(COL_COMMISSIONS_SOURCE_XCD_IDX).FindControl("moCommission_PercentLabel"), Label)
                        Dim motextBoxCommPer As TextBox = DirectCast(gRow.Cells(COL_COMMISSIONS_SOURCE_XCD_IDX).FindControl("moCommission_PercentText"), TextBox)

                        If Not motextBoxCommPer Is Nothing Then
                            If Not String.IsNullOrWhiteSpace(motextBoxCommPer.Text) Then
                                Dim cPer As Decimal = Convert.ToDecimal(motextBoxCommPer.Text)
                                countCommPer = countCommPer + cPer
                            End If
                        End If

                        If Not molblCommPer Is Nothing Then
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

            For pageIndexk As Integer = 0 To Me.moGridView.PageCount - 1
                Me.moGridView.PageIndex = pageIndexk
                Dim rowNum As Integer = Me.moGridView.Rows.Count
                For i As Integer = 0 To rowNum - 1
                    Dim gRow As GridViewRow = moGridView.Rows(i)
                    If gRow.RowType = DataControlRowType.DataRow Then
                        Dim molblCommPer As Label = DirectCast(gRow.Cells(COL_COMMISSIONS_SOURCE_XCD_IDX).FindControl("moCommission_PercentLabel"), Label)
                        Dim motextBoxCommPer As TextBox = DirectCast(gRow.Cells(COL_COMMISSIONS_SOURCE_XCD_IDX).FindControl("moCommission_PercentText"), TextBox)

                        Dim molblAmt As Label = DirectCast(gRow.Cells(COL_COMMISSIONS_SOURCE_XCD_IDX).FindControl("moLowPriceLabel"), Label)
                        Dim motextBoxAmt As TextBox = DirectCast(gRow.Cells(COL_COMMISSIONS_SOURCE_XCD_IDX).FindControl("moLowPriceText"), TextBox)

                        If Not motextBoxCommPer Is Nothing Then
                            If Not String.IsNullOrWhiteSpace(motextBoxCommPer.Text) Then
                                Dim cPer As Decimal = Convert.ToDecimal(motextBoxCommPer.Text)
                                If cPer > 0 Then
                                    countCommPer = countCommPer + 1
                                End If
                            End If
                        End If

                        If Not molblCommPer Is Nothing Then
                            If Not String.IsNullOrWhiteSpace(molblCommPer.Text) Then
                                Dim cPer As Decimal = Convert.ToDecimal(molblCommPer.Text)
                                countCommPer = countCommPer + 1
                            End If
                        End If


                        If Not motextBoxAmt Is Nothing Then
                            If Not String.IsNullOrWhiteSpace(motextBoxAmt.Text) Then
                                Dim cAmt As Decimal = Convert.ToDecimal(motextBoxAmt.Text)
                                If cAmt > 0 Then
                                    countAmt = countAmt + 1
                                End If
                            End If
                        End If

                        If Not molblAmt Is Nothing Then
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
                Me.State.IsAmountAndPercentBothPresent = True
            Else
                Me.State.IsAmountAndPercentBothPresent = False
            End If

            Return Me.State.IsAmountAndPercentBothPresent
        End Function

        Private Sub SetGridSourceXcdLabelFromBo()
            'If moGridView.EditIndex = -1 Then Exit Sub
            For pageIndexk As Integer = 0 To Me.moGridView.PageCount - 1
                Me.moGridView.PageIndex = pageIndexk
                Dim rowNum As Integer = Me.moGridView.Rows.Count
                For i As Integer = 0 To rowNum - 1
                    Dim gRow As GridViewRow = moGridView.Rows(i)
                    If gRow.RowType = DataControlRowType.DataRow Then
                        Dim mollblCommPercentSourceXcd As Label = DirectCast(gRow.Cells(COL_COMMISSIONS_SOURCE_XCD_IDX).FindControl("lblCommPercentSourceXcd"), Label)
                        Dim mollblCommPercentSourceXcdCode As Label = DirectCast(gRow.Cells(COL_COMMISSIONS_SOURCE_XCD_IDX).FindControl("lblCommPercentSourceXcdCode"), Label)
                        Dim molblPayeeType As Label = DirectCast(gRow.Cells(COL_COMMISSIONS_SOURCE_XCD_IDX).FindControl("lblPayeeType"), Label)
                        Dim molblEntityType As Label = DirectCast(gRow.Cells(COL_COMMISSIONS_SOURCE_XCD_IDX).FindControl("lblEntityType"), Label)

                        If Not molblPayeeType Is Nothing Then
                            If molblPayeeType.Visible Then
                                If (Not molblPayeeType.Text Is Nothing And Not String.IsNullOrWhiteSpace(molblPayeeType.Text)) Then
                                    molblPayeeType.Text = GetDescOfExtCodePayeeTypeXcdOption(molblPayeeType.Text)
                                End If
                            End If
                        End If

                        If Not mollblCommPercentSourceXcd Is Nothing Then
                            If mollblCommPercentSourceXcd.Visible Then
                                If (Not mollblCommPercentSourceXcd.Text Is Nothing And Not String.IsNullOrWhiteSpace(mollblCommPercentSourceXcd.Text)) Then
                                    mollblCommPercentSourceXcd.Text = GetDescFromExtCode(mollblCommPercentSourceXcd.Text)
                                End If
                            End If
                        End If

                        If Not molblEntityType Is Nothing Then
                            If molblEntityType.Visible Then
                                If (Not molblEntityType.Text Is Nothing And Not String.IsNullOrWhiteSpace(molblEntityType.Text)) Then
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

            If Not mocboCommPercentSourceXcd Is Nothing Then
                PoupulateSourceOptionDropdownlist(mocboCommPercentSourceXcd)
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
                .AddBlankItem = False,
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

            If Me.State.IsNewWithCopy = False Then
                With TheCommPlanDist
                    If mocboCommPercentSourceXcd.Visible Then
                        If Not .CommissionsPercentSourceXcd Is Nothing And mocboCommPercentSourceXcd.Items.Count > 0 Then
                            Me.SetSelectedItem(mocboCommPercentSourceXcd, .CommissionsPercentSourceXcd)

                            If mocboCommPercentSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_COMMBRKDOWN_OPTION_DIFFERENCE) Then
                                Me.PopulateControlFromBOProperty(moTextCommission_PercentText, diffValue, Me.PERCENT_FORMAT)
                                moTextCommission_PercentText.Enabled = False
                            Else
                                moTextCommission_PercentText.Enabled = True
                            End If
                        End If
                    End If
                End With
            End If
        End Sub

        Private Sub PoupulateSourceOptionDropdownlist(ByVal oDropDownList As DropDownList)
            Dim oAcctBucketsSourceOption As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="ACCTBUCKETSOURCE_COMMBRKDOWN")

            oDropDownList.Populate(oAcctBucketsSourceOption, New PopulateOptions() With
                                {
                                .AddBlankItem = False,
                                .TextFunc = AddressOf PopulateOptions.GetDescription,
                                .ValueFunc = AddressOf PopulateOptions.GetExtendedCode
                                })
        End Sub

        Public Function GetDescFromExtCode(ByVal desc As String) As String
            Dim sGetCodeSourceOptionDesc As String
            Try
                sGetCodeSourceOptionDesc = String.Empty
                If Not desc Is Nothing And Not String.IsNullOrWhiteSpace(desc) Then
                    sGetCodeSourceOptionDesc = LookupListNew.GetDescriptionFromExtCode("ACCTBUCKETSOURCE_COMMBRKDOWN", ElitaPlusIdentity.Current.ActiveUser.LanguageId, desc)
                End If
                Return sGetCodeSourceOptionDesc
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Function
        Public Function GetDescOfExtCodePayeeTypeXcdOption(ByVal desc As String) As String
            Dim sGetDescOfExtCodeFromPayeeTypeXcd As String
            Try
                sGetDescOfExtCodeFromPayeeTypeXcd = String.Empty
                If Not desc Is Nothing And Not String.IsNullOrWhiteSpace(desc) Then
                    sGetDescOfExtCodeFromPayeeTypeXcd = LookupListNew.GetDescriptionFromExtCode("PYTYPE", ElitaPlusIdentity.Current.ActiveUser.LanguageId, desc)
                End If
                Return sGetDescOfExtCodeFromPayeeTypeXcd
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Function

        Public Function GetDescOfIDFromEntityTypeOption(ByVal desc As String) As String
            Dim sGetDescOfExtCodeFromPayeeTypeXcd As String
            Try
                sGetDescOfExtCodeFromPayeeTypeXcd = String.Empty
                Dim listcontext As ListContext = New ListContext()
                listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                Dim CommEntityList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="CommEntityByCompanyGroup", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=listcontext)

                If Not desc Is Nothing And Not String.IsNullOrWhiteSpace(desc) Then
                    If (desc.Length > 16) Then
                        sGetDescOfExtCodeFromPayeeTypeXcd = (From lst In CommEntityList
                                                             Where lst.ListItemId = GetGuidFromString(desc)
                                                             Select lst.Translation).FirstOrDefault
                    End If
                End If
                Return sGetDescOfExtCodeFromPayeeTypeXcd
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Function

        Private Sub LoadDistributionList()
            If moGridView.Rows.Count > 0 Then
                Dim i As Integer = 0
                Dim oDistribution(moGridView.Rows.Count - 1) As CommPlanDistribution

                For i = 0 To moGridView.Rows.Count - 1
                    oDistribution(i) = New CommPlanDistribution
                    'oDistribution(i).CommissionPlanId = TheCommPlanDist.CommissionPlanId
                    oDistribution(i).CommissionPlanId = Me.State.moCommPlanId 'TheCommPlanDist.CommissionPlanId
                    If moGridView.Rows(i).Cells(COL_PAYEE_TYPE_XCD_IDX).Controls(1).GetType().ToString = "System.Web.UI.WebControls.Label" Then
                        Me.PopulateBOProperty(oDistribution(i), PROPERTY_PAYEE_TYPE_XCD, CType(moGridView.Rows(i).Cells(COL_PAYEE_TYPE_XCD_IDX).Controls(1), Label).Text)
                    Else
                        Me.PopulateBOProperty(oDistribution(i), PROPERTY_PAYEE_TYPE_XCD, CType(moGridView.Rows(i).Cells(COL_PAYEE_TYPE_XCD_IDX).Controls(1), DropDownList).Text)
                    End If

                    If moGridView.Rows(i).Cells(COL_ENTITY_ID_IDX).Controls(0).GetType().ToString = "System.Web.UI.WebControls.Label" Then
                        Me.PopulateBOProperty(oDistribution(i), PROPERTY_ENTITY_ID, CType(moGridView.Rows(i).Cells(COL_ENTITY_ID_IDX).Controls(0), Label).Text)
                    Else
                        Me.PopulateBOProperty(oDistribution(i), PROPERTY_ENTITY_ID, CType(moGridView.Rows(i).Cells(COL_ENTITY_ID_IDX).Controls(1), DropDownList).SelectedValue)
                    End If

                    If moGridView.Rows(i).Cells(COL_COMMISSION_AMOUNT_IDX).Controls(1).GetType().ToString = "System.Web.UI.WebControls.Label" Then
                        Me.PopulateBOProperty(oDistribution(i), PROPERTY_COMM_AMT, CType(moGridView.Rows(i).Cells(COL_COMMISSION_AMOUNT_IDX).Controls(1), Label).Text)
                    Else
                        Me.PopulateBOProperty(oDistribution(i), PROPERTY_COMM_AMT, CType(moGridView.Rows(i).Cells(COL_COMMISSION_AMOUNT_IDX).Controls(1), TextBox).Text)
                    End If

                    If moGridView.Rows(i).Cells(COL_COMMISSION_PERCENTAGE_IDX).Controls(1).GetType().ToString = "System.Web.UI.WebControls.Label" Then
                        Me.PopulateBOProperty(oDistribution(i), PROPERTY_COMM_PER, CType(moGridView.Rows(i).Cells(COL_COMMISSION_PERCENTAGE_IDX).Controls(1), Label).Text)
                    Else
                        Me.PopulateBOProperty(oDistribution(i), PROPERTY_COMM_PER, CType(moGridView.Rows(i).Cells(COL_COMMISSION_PERCENTAGE_IDX).Controls(1), TextBox).Text)
                    End If

                    If moGridView.Rows(i).Cells(COL_COMMISSIONS_SOURCE_XCD_IDX).Controls(0).GetType().ToString = "System.Web.UI.WebControls.Label" Then
                        Me.PopulateBOProperty(oDistribution(i), PROPERTY_COMMISSIONS_XCD, CType(moGridView.Rows(i).Cells(COL_COMMISSIONS_SOURCE_XCD_IDX).Controls(0), Label).Text)
                    Else
                        Me.PopulateBOProperty(oDistribution(i), PROPERTY_COMMISSIONS_XCD, CType(moGridView.Rows(i).Cells(COL_COMMISSIONS_SOURCE_XCD_IDX).Controls(1), DropDownList).SelectedValue)
                    End If

                    If moGridView.Rows(i).Cells(COL_POSITION_IDX).Controls(1).GetType().ToString = "System.Web.UI.WebControls.Label" Then
                        Me.PopulateBOProperty(oDistribution(i), PROPERTY_POSITION, CType(moGridView.Rows(i).Cells(COL_POSITION_IDX).Controls(1), Label).Text)
                    Else
                        Me.PopulateBOProperty(oDistribution(i), PROPERTY_POSITION, CType(moGridView.Rows(i).Cells(COL_POSITION_IDX).Controls(1), TextBox).Text)
                    End If
                Next

                Me.State.moDistributionList = oDistribution
            End If
        End Sub

        Public Function SaveDistributionList() As Boolean
            Dim i As Integer = 0
            Try
                If Not Me.State.moDistributionList Is Nothing Then
                    For i = 0 To Me.State.moDistributionList.Length - 1
                        Me.State.moDistributionList(i).CommissionPlanId = Me.State.moCommPlanId 'TheCommPlanDist.CommissionPlanId
                        Me.State.moDistributionList(i).Save()
                    Next
                End If
            Catch ex As Exception
                Dim j As Integer
                'REPLACE THIS LOOP BY A DB ROLLBACK
                For j = 0 To i - 1
                    Me.State.moDistributionList(j).Delete()
                    Me.State.moDistributionList(j).Save()
                Next
                'Me.HandleErrors(ex, moMsgControllerRate)
                Return False
            End Try
            Return True
        End Function
        Private Sub BindBoPropertiesToGridHeader()
            Me.BindBOPropertyToGridHeader(TheCommPlanDist, PROPERTY_PAYEE_TYPE_XCD, moGridView.Columns(COL_PAYEE_TYPE_XCD_IDX))
            Me.BindBOPropertyToGridHeader(TheCommPlanDist, PROPERTY_ENTITY_ID, moGridView.Columns(COL_ENTITY_ID_IDX))
            Me.BindBOPropertyToGridHeader(TheCommPlanDist, PROPERTY_COMM_AMT, moGridView.Columns(COL_COMMISSION_AMOUNT_IDX))
            Me.BindBOPropertyToGridHeader(TheCommPlanDist, PROPERTY_COMM_PER, moGridView.Columns(COL_COMMISSION_PERCENTAGE_IDX))
            Me.BindBOPropertyToGridHeader(TheCommPlanDist, PROPERTY_COMMISSIONS_XCD, moGridView.Columns(COL_COMMISSIONS_SOURCE_XCD_IDX))
            Me.BindBOPropertyToGridHeader(TheCommPlanDist, PROPERTY_POSITION, moGridView.Columns(COL_POSITION_IDX))
        End Sub

#Region "Handlers-Distribution-DataGrid"

        Public Sub ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moGridView_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles moGridView.PageIndexChanging
            Try
                ResetIndexes()
                moGridView.PageIndex = e.NewPageIndex
                PopulateDistributionList(ACTION_CANCEL_DELETE)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles moGridView.RowCommand
            Dim nIndex As Integer = CInt(e.CommandArgument)

            Try
                If e.CommandName = EDIT_COMMAND_NAME Then
                    nIndex = CInt(e.CommandArgument)
                    moGridView.EditIndex = nIndex
                    moGridView.SelectedIndex = nIndex
                    PopulateDistributionList(ACTION_EDIT)

                    Me.FillSourceXcdDropdownList()
                    FillEntityDropDownList()
                    FillPayeeTypeDropDownList()
                    SetGridSourceXcdDropdownFromBo()
                    SetGridSourceXcdLabelFromBo()

                    SetGridControls(moGridView, False)
                    EnableDisableControls(Me.moCoverageEditPanel, True)
                ElseIf (e.CommandName = DELETE_COMMAND_NAME) Then
                    moGridView.EditIndex = nIndex
                    moGridView.SelectedIndex = nIndex
                    DistributionId = Me.GetGridText(moGridView, nIndex, COL_COMMISSION_PLAN_DIST_ID_IDX)
                    Me.State.moCommPlanDistId = GetGuidFromString(DistributionId)
                    Me.State.MyBoDist = New CommPlanDistribution(Me.State.moCommPlanDistId)
                    If DeleteSelectedDistribution(nIndex) = True Then
                        PopulateDistributionList(ACTION_CANCEL_DELETE)
                    End If
                    SetGridSourceXcdLabelFromBo()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub ResetIndexes()
            moGridView.EditIndex = NO_ITEM_SELECTED_INDEX
            moGridView.SelectedIndex = NO_ITEM_SELECTED_INDEX
        End Sub

        Private Sub BtnSaveRate_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSaveRate_WRITE.Click
            Try
                ValidatePmCertSourceLogic()

                If IsAmountAndPercentBothPresent() Then
                    Me.State.IsAmountAndPercentBothPresent = True
                    'Me.State.ActionInProgress = DetailPageCommand.OK
                    'Me.DisplayMessage(Message.MSG_COMMISSION_PERCENTAGE_IS_GREATER_THAN_HUNDRED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_ALERT, Me.HiddenSaveChangesPromptResponse)
                    Throw New GUIException(Message.MSG_EITHER_PERCENTAGE_OR_AMOUNT_ALLOWED, Assurant.ElitaPlus.Common.ErrorCodes.MSG_ONLY_EITHER_PERCENTAGE_OR_AMOUNT_ALLOWED)
                End If

                If IsCommPerGreaterThanHundred() Then
                    Me.State.ActionInProgress = DetailPageCommand.Accept
                    Me.DisplayMessage(Message.MSG_COMMISSION_PERCENTAGE_IS_GREATER_THAN_HUNDRED, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_INFO, Me.HiddenSaveChangesPromptResponse)
                Else
                    SaveDistributionChanges()
                    SetGridSourceXcdLabelFromBo()

                    TheDealerControl.ChangeEnabledControlProperty(False)
                End If

            Catch ex As Exception
                If Me.State.IsAmountAndPercentBothPresent = True Then
                    SetGridControls(moGridView, True)
                    PopulateDistributionList(ACTION_CANCEL_DELETE)
                    'FillSourceXcdDropdownList()
                    'FillEntityDropDownList()
                    'FillPayeeTypeDropDownList()
                    'SetGridSourceXcdDropdownFromBo()
                    SetGridSourceXcdLabelFromBo()
                    SetGridControls(moGridView, True)
                    EnableDisableControls(Me.moCoverageEditPanel, True)
                Else
                    Me.State.IsCommPlanDistNew = False
                    EnableForEditRateButtons(False)
                    'Below method reloads the data in the grid
                    PopulateDistributionList(ACTION_CANCEL_DELETE)
                    SetGridSourceXcdLabelFromBo()
                    TheDealerControl.ChangeEnabledControlProperty(False)
                End If

                setbuttons(False)
                btnBack.Visible = True
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub BtnCancelRate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancelRate.Click
            'Pencil button in not in edit mode
            Try
                Me.State.IsCommPlanDistNew = False
                EnableForEditRateButtons(False)
                PopulateDistributionList(ACTION_CANCEL_DELETE)
                SetGridSourceXcdLabelFromBo()
                TheDealerControl.ChangeEnabledControlProperty(False)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub BtnNewRate_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNewRate_WRITE.Click
            Try
                If moGridView.Rows.Count >= 5 Then
                    'Throw New GUIException(Message.MSG_DISTRIBUTION_RECORD_LIMITED_FOR_EXTRACT_REPORT, Assurant.ElitaPlus.Common.ErrorCodes.MSG_COMMISSION_DISTRIBUTION_RECORD_LIMITED_FOR_EXTRACT_REPORT)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.OK
                    Me.DisplayMessage(Message.MSG_DISTRIBUTION_RECORD_LIMITED_FOR_EXTRACT_REPORT, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO, Me.HiddenSaveChangesPromptResponse)
                Else
                    Me.State.IsCommPlanDistNew = True
                    DistributionId = Guid.Empty.ToString
                    PopulateDistributionList(ACTION_NEW)
                    'SetGridControls(moGridView, True)
                    FillSourceXcdDropdownList()
                    FillEntityDropDownList()
                    FillPayeeTypeDropDownList()
                    SetGridSourceXcdDropdownFromBo()
                    SetGridSourceXcdLabelFromBo()
                    SetGridControls(moGridView, True)
                    EnableDisableControls(Me.moCoverageEditPanel, True)
                    setbuttons(False)
                    btnBack.Visible = True
                End If

            Catch ex As Exception
                ' SetStateProperties()
                SetGridControls(moGridView, True)
                FillSourceXcdDropdownList()
                FillEntityDropDownList()
                FillPayeeTypeDropDownList()
                SetGridSourceXcdDropdownFromBo()
                SetGridSourceXcdLabelFromBo()
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub setbuttons(ByVal enable As Boolean)
            'ControlMgr.SetEnableControl(Me, btnBack, enable)
            'ControlMgr.SetEnableControl(Me, btnApply_WRITE, enable)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, enable)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, enable)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, enable)
            ControlMgr.SetEnableControl(Me, btnUndo_WRITE, enable)
            'ControlMgr.SetVisibleControl(Me, BtnEffectiveDate, enable)
            'ControlMgr.SetVisibleControl(Me, BtnExpirationDate, enable)
        End Sub

        Private Sub SaveDistributionChanges()
            If ApplyDistributionChanges() = True Then
                If Me.State.IsCommPlanDistNew = True Then
                    Me.State.IsCommPlanDistNew = False
                End If
                PopulateDistributionList(ACTION_SAVE)
            End If
        End Sub
        Private Function ApplyDistributionChanges() As Boolean
            Dim bIsOk As Boolean = True
            Dim bIsDirty As Boolean
            If moGridView.EditIndex < 0 Then Return False ' Distribution is not in edit mode
            If Me.State.IsNewWithCopy Then
                Me.LoadDistributionList()
                Me.State.moDistributionList(moGridView.SelectedIndex).Validate()
                Return bIsOk
            End If
            If Me.State.IsCommPlanDistNew = False Then
                DistributionId = Me.GetSelectedGridText(moGridView, COL_COMMISSION_PLAN_DIST_ID_IDX)
            End If
            BindBoPropertiesToGridHeader()
            With TheCommPlanDist
                PopulateRateBOFromForm()
                bIsDirty = .IsDirty
                .Save()
                EnableForEditRateButtons(False)
            End With
            If (bIsOk = True) Then
                If bIsDirty = True Then
                    Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
                Else
                    Me.MasterPage.MessageController.AddError(Message.MSG_RECORD_NOT_SAVED, True)
                End If
            End If
            Return bIsOk
        End Function
        Private Sub ClearDistribution()
            If Not Me.State.IsNewWithCopy Then
                EnableRateButtons(False)
                moGridView.DataBind()
            End If
        End Sub

        Private Sub PopulateRateBOFromForm()
            With TheCommPlanDist
                .CommissionPlanId = Me.State.moCommPlanId 'TheCommPlanDist.CommissionPlanId
                CommonSourceOptionLogic()
            End With
            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub
        Private Sub CommonSourceOptionLogic()

            If moGridView.EditIndex = -1 Then Exit Sub
            Dim gRow As GridViewRow = moGridView.Rows(moGridView.EditIndex)
            Dim mocboPayeeType As DropDownList = DirectCast(gRow.Cells(COL_PAYEE_TYPE_XCD_IDX).FindControl("cboPayeeType"), DropDownList)
            Dim mocboEntityType As DropDownList = DirectCast(gRow.Cells(COL_ENTITY_ID_IDX).FindControl("cboEntityType"), DropDownList)
            Dim mocboCommPercentSourceXcd As DropDownList = DirectCast(gRow.Cells(COL_COMMISSIONS_SOURCE_XCD_IDX).FindControl("cboCommPercentSourceXcd"), DropDownList)
            Dim moTextmoLowPriceText As TextBox = DirectCast(gRow.Cells(COL_COMMISSION_AMOUNT_IDX).FindControl("moLowPriceText"), TextBox)
            Dim moTextmoCommission_PercentText As TextBox = DirectCast(gRow.Cells(COL_COMMISSION_PERCENTAGE_IDX).FindControl("moCommission_PercentText"), TextBox)
            Dim moTextmoRenewal_NumberText As TextBox = DirectCast(gRow.Cells(COL_COMMISSIONS_SOURCE_XCD_IDX).FindControl("moRenewal_NumberText"), TextBox)

            If (mocboCommPercentSourceXcd.Items.Count > 0) Then
                If mocboCommPercentSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_COMMBRKDOWN_OPTION_DIFFERENCE) Then
                    moTextmoCommission_PercentText.Text = "0.0000"
                End If
            End If

            If (String.IsNullOrWhiteSpace(moTextmoLowPriceText.Text)) Then
                moTextmoLowPriceText.Text = "0.00"
            End If

            If (String.IsNullOrWhiteSpace(moTextmoCommission_PercentText.Text)) Then
                moTextmoCommission_PercentText.Text = "0.0000"
            End If

            If (String.IsNullOrWhiteSpace(moTextmoRenewal_NumberText.Text)) Then
                moTextmoRenewal_NumberText.Text = "1"
            End If

            Me.PopulateBOProperty(TheCommPlanDist, PROPERTY_COMM_AMT, moTextmoLowPriceText)
            Me.PopulateBOProperty(TheCommPlanDist, PROPERTY_COMM_PER, moTextmoCommission_PercentText)
            Me.PopulateBOProperty(TheCommPlanDist, PROPERTY_POSITION, moTextmoRenewal_NumberText)

            If mocboEntityType.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                Me.PopulateBOProperty(TheCommPlanDist, PROPERTY_ENTITY_ID, mocboEntityType, True, False)
            End If

            If mocboCommPercentSourceXcd.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                Me.PopulateBOProperty(TheCommPlanDist, PROPERTY_COMMISSIONS_XCD, mocboCommPercentSourceXcd, False, True)
            End If

            If mocboPayeeType.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                moTextmoRenewal_NumberText.Text = mocboPayeeType.SelectedValue   'PayeeTypeCode 
                Me.PopulateBOProperty(TheCommPlanDist, PROPERTY_PAYEE_TYPE_XCD, moTextmoRenewal_NumberText)
            End If
        End Sub
        Private Function GetDropDownControlFromGrid(ByVal oDataGrid As GridView, ByVal cellPosition As Integer) As Control
            Dim oItem As GridViewRow = oDataGrid.Rows(oDataGrid.SelectedIndex)
            Dim oControl As Control

            For Each gridControl As Control In oItem.Cells(cellPosition).Controls

                If gridControl.GetType().FullName.Equals("System.Web.UI.WebControls.DropDownList") Then
                    oControl = gridControl
                End If
            Next

            Return oControl
        End Function
        Private Function DeleteSelectedDistribution(ByVal nIndex As Integer) As Boolean
            Dim bIsOk As Boolean = True
            Try
                If Me.State.IsNewWithCopy Then
                    If Me.State.moDistributionList Is Nothing Then Me.LoadDistributionList()
                    Me.State.moDistributionList(nIndex) = Nothing
                Else
                    With TheCommPlanDist()
                        .Delete()
                        .Save()
                    End With
                End If

            Catch ex As Exception
                'moMsgControllerRate.AddError(COVERAGE_FORM005)
                'moMsgControllerRate.AddError(ex.Message, False)
                'moMsgControllerRate.Show()
                bIsOk = False
            End Try
            Return bIsOk
        End Function

        Private Sub PopulateDistributionList(Optional ByVal oAction As String = ACTION_NONE)
            Dim oDistribution As CommPlanDistribution
            Dim oDataView As DataView

            If Me.State.IsPlanNew = True And Not Me.State.IsNewWithCopy Then
                Return ' We can not have Distribution if the plan is new
            End If

            Try
                If Me.State.IsNewWithCopy Then
                    oDataView = oDistribution.getPlanList(Guid.Empty)
                    If Not oAction = ACTION_CANCEL_DELETE Then
                        Me.LoadDistributionList()
                    End If
                    If Not Me.State.moDistributionList Is Nothing Then
                        oDataView = getDVFromArray(Me.State.moDistributionList, oDataView.Table)
                    End If
                Else
                    oDataView = oDistribution.getPlanList(Me.State.moCommPlanId) 'TheCommPlanDist.Id)
                End If

                Select Case oAction
                    Case ACTION_NONE
                        Me.SetPageAndSelectedIndexFromGuid(oDataView, Guid.Empty, moGridView, 0)
                        EnableForEditRateButtons(False)
                    Case ACTION_SAVE
                        Me.SetPageAndSelectedIndexFromGuid(oDataView, GetGuidFromString(DistributionId), moGridView,
                                    moGridView.PageIndex)
                        EnableForEditRateButtons(False)
                    Case ACTION_CANCEL_DELETE
                        Me.SetPageAndSelectedIndexFromGuid(oDataView, Guid.Empty, moGridView,
                                    moGridView.PageIndex)
                        EnableForEditRateButtons(False)
                    Case ACTION_EDIT
                        If Me.State.IsNewWithCopy Then
                            DistributionId = Me.State.moDistributionList(moGridView.SelectedIndex).Id.ToString
                            Me.State.moCommPlanDistId = Me.State.moDistributionList(moGridView.SelectedIndex).Id
                        Else
                            DistributionId = Me.GetSelectedGridText(moGridView, COL_COMMISSION_PLAN_DIST_ID_IDX)
                            Me.State.moCommPlanDistId = GetGuidFromString(DistributionId) ' Me.State.moDistributionList(moGridView.SelectedIndex).Id

                            Me.State.MyBoDist = New CommPlanDistribution(Me.State.moCommPlanDistId)

                        End If
                        Me.SetPageAndSelectedIndexFromGuid(oDataView, GetGuidFromString(DistributionId), moGridView,
                                    moGridView.PageIndex, True)
                        EnableForEditRateButtons(True)
                    Case ACTION_NEW
                        If Me.State.IsNewWithCopy Then oDataView.Table.DefaultView.Sort() = Nothing ' Clear sort, so that the new line shows up at the bottom

                        Me.State.IsCommPlanDistNew = True
                        Me.State.MyBoDist = New CommPlanDistribution()
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

                        Me.State.moCommPlanDistId = GetGuidFromString(DistributionId)

                        Me.SetPageAndSelectedIndexFromGuid(oDataView, GetGuidFromString(DistributionId), moGridView,
                                    moGridView.PageIndex, True)
                        EnableForEditRateButtons(True)

                End Select

                moGridView.DataSource = oDataView
                moGridView.DataBind()
                ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, moGridView)

            Catch ex As Exception
                'moMsgControllerRate.AddError(COVERAGE_FORM004)
                'moMsgControllerRate.AddError(ex.Message, False)
                'moMsgControllerRate.Show()
            End Try
        End Sub

        Private Function getDVFromArray(ByVal oArray() As CommPlanDistribution, ByVal oDtable As DataTable) As DataView
            Dim oRow As DataRow
            Dim oDistribution As CommPlanDistribution
            For Each oDistribution In oArray
                If Not oDistribution Is Nothing Then
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

        Private Sub EnableEditRateButtons(ByVal bIsReadWrite As Boolean)
            ControlMgr.SetEnableControl(Me, BtnSaveRate_WRITE, bIsReadWrite)
            ControlMgr.SetEnableControl(Me, BtnCancelRate, bIsReadWrite)
        End Sub

        Private Sub EnableNewRateButtons(ByVal bIsReadWrite As Boolean)
            ControlMgr.SetEnableControl(Me, BtnNewRate_WRITE, bIsReadWrite)
        End Sub

        Private Sub EnableRateButtons(ByVal bIsReadWrite As Boolean)
            EnableNewRateButtons(bIsReadWrite)
            EnableEditRateButtons(bIsReadWrite)
        End Sub

        Private Sub EnableForEditRateButtons(ByVal bIsReadWrite As Boolean)
            EnableNewRateButtons(Not bIsReadWrite)
            EnableEditRateButtons(bIsReadWrite)
        End Sub

        Private Sub PopulateDistribution()
            If Me.State.IsNewWithCopy Then
                With Me.State.moDistributionList(moGridView.SelectedIndex)
                    Me.SetSelectedGridText(moGridView, COL_PAYEE_TYPE_XCD_IDX, .PayeeTypeXcd.ToString)
                    'Me.SetSelectedGridText(moGridView, COL_ENTITY_ID_IDX, .EntityId)
                    Me.SetSelectedGridText(moGridView, COL_COMMISSION_AMOUNT_IDX, .CommissionAmount.ToString)
                    Me.SetSelectedGridText(moGridView, COL_COMMISSION_PERCENTAGE_IDX, .CommissionPercent.ToString)
                    Me.SetSelectedGridText(moGridView, COL_COMMISSIONS_SOURCE_XCD_IDX, .CommissionsPercentSourceXcd.ToString)
                    Me.SetSelectedGridText(moGridView, COL_POSITION_IDX, .Position.ToString)
                End With
            Else
                With TheCommPlanDist
                    Me.SetSelectedGridText(moGridView, COL_PAYEE_TYPE_XCD_IDX, .PayeeTypeXcd.ToString)
                    'Me.SetSelectedGridText(moGridView, COL_ENTITY_ID_IDX, .EntityId)
                    Me.SetSelectedGridText(moGridView, COL_COMMISSION_AMOUNT_IDX, .CommissionAmount.ToString)
                    Me.SetSelectedGridText(moGridView, COL_COMMISSION_PERCENTAGE_IDX, .CommissionPercent.ToString)
                    Me.SetSelectedGridText(moGridView, COL_COMMISSIONS_SOURCE_XCD_IDX, .CommissionsPercentSourceXcd.ToString)
                    Me.SetSelectedGridText(moGridView, COL_POSITION_IDX, .Position.ToString)
                End With
            End If
        End Sub
#End Region
#End Region
    End Class
End Namespace
