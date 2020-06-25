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
            Public moCommTolerance As CommissionTolerance
            Public moCoverageRateList() As CommPlanDistribution
            Public moCommPlanDistList As CommPlanDistribution.CommPlanDistList
            Public moCommPlanDist As CommPlanDistribution
            Public moAssocComm As CommPlanDistribution
            Public moIsAssocCommDirty As Boolean = False
            Public moCommissionToleranceId As Guid = Guid.Empty
            Public moCommPlanId As Guid = Guid.Empty
            Public moCommPlanDistId As Guid = Guid.Empty
            Public moInError As Boolean = False
            Public LastErrMsg As String
            Public IsCommPlanDistNew As Boolean = False
            Public IsToleranceNew As Boolean = False
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public boChanged As Boolean = False
            Public moAssocCommSearch As AssociateCommissions.SearchDV = Nothing
            Public moCommPlanDistSearch As CommPlanDistribution.SearchDV = Nothing
            Public searchDV As DataView = Nothing

            Public IsPmComCombination As Boolean
            Public IsDiffSelectedTwice As Boolean
            Public IsDiffNotSelectedOnce As Boolean
            Public IsBucketIncomingSelected As Boolean
            Public IsDealerConfiguredForSourceXcd As Boolean = False
            Public IsCompanyConfiguredForSourceXcd As Boolean = False
            Public IsIgnorePremiumSetYesForContract As Boolean = False

            Public IsCoverageNew As Boolean = False
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
                ClearPeriod()
                SetPeriodButtonsState(True)
                PopulatePeriod()
                PopulateCoverageRateList()
                TheDealerControl.ChangeEnabledControlProperty(True)
            Else
                Me.State.IsCommPlanDistNew = False
                SetPeriodButtonsState(False)
                PopulatePeriod()
                PopulateCoverageRateList()
                TheDealerControl.ChangeEnabledControlProperty(False)
            End If
            If Not TheDealerControl.SelectedGuid.Equals(Guid.Empty) Then
            End If
        End Sub

#End Region


#Region "Constants"

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

#Region "Constants-Tolerance"

        Private Const COMMISSION_TOLERANCE_ID_COL As Integer = 2
        Private Const ALLOWED_MARKUP_COL As Integer = 3
        Private Const TOLERANCE_COL As Integer = 4
        Private Const DEALER_MARKUP_COL As Integer = 5
        Private Const BROKER_MARKUP_COL As Integer = 6
        Private Const BROKER2_MARKUP_COL As Integer = 7
        Private Const BROKER3_MARKUP_COL As Integer = 8
        Private Const BROKER4_MARKUP_COL As Integer = 9
        Private Const DEALER_COMM_COL As Integer = 10
        Private Const BROKER_COMM_COL As Integer = 11
        Private Const BROKER2_COMM_COL As Integer = 12
        Private Const BROKER3_COMM_COL As Integer = 13
        Private Const BROKER4_COMM_COL As Integer = 14
        Private Const COMMISSION_PERCENT1_SOURCE_COL As Integer = 15
        Private Const COMMISSION_PERCENT2_SOURCE_COL As Integer = 16
        Private Const COMMISSION_PERCENT3_SOURCE_COL As Integer = 17
        Private Const COMMISSION_PERCENT4_SOURCE_COL As Integer = 18
        Private Const COMMISSION_PERCENT5_SOURCE_COL As Integer = 19


        ' Property Name
        Public Const ALLOWED_MARKUP_PCT_PROPERTY As String = "AllowedMarkupPct"
        Public Const TOLERANCE_PROPERTY As String = "Tolerance"
        Public Const BROKER_MARKUP_PCT_PROPERTY As String = "BrokerMarkupPct"
        Public Const BROKER_COMM_PCT_PROPERTY As String = "BrokerCommPct"
        Public Const MARKUP_TOTAL As String = "MarkupTotal"
        Public Const COMMISSION_TOTAL As String = "CommTotal"
        Public Const COL_NAME_MARKUP_PERCENT1 As String = "MARKUP_PERCENT1"
        Public Const COL_NAME_MARKUP_PERCENT2 As String = "MARKUP_PERCENT2"
        Public Const COL_NAME_MARKUP_PERCENT3 As String = "MARKUP_PERCENT3"
        Public Const COL_NAME_MARKUP_PERCENT4 As String = "MARKUP_PERCENT4"
        Public Const COL_NAME_MARKUP_PERCENT5 As String = "MARKUP_PERCENT5"
        Public Const COL_NAME_COMMISSION_PERCENT1 As String = "COMMISSION_PERCENT1"
        Public Const COL_NAME_COMMISSION_PERCENT2 As String = "COMMISSION_PERCENT2"
        Public Const COL_NAME_COMMISSION_PERCENT3 As String = "COMMISSION_PERCENT3"
        Public Const COL_NAME_COMMISSION_PERCENT4 As String = "COMMISSION_PERCENT4"
        Public Const COL_NAME_COMMISSION_PERCENT5 As String = "COMMISSION_PERCENT5"


        'Control Names 
        Private Const ID_LABEL_CONTROL_NAME As String = "moCommissionToleranceId"
        Private Const ALLOWED_MARKUP_PCT_LABEL_CONTROL_NAME As String = "moAllowedMarkupPctLabel"
        Private Const ALLOWED_MARKUP_PCT_CONTROL_NAME As String = "moAllowedMarkupPctText"
        Private Const TOLERANCE_CONTROL_NAME As String = "moToleranceText"
        Private Const TOLERANCE_LABEL_CONTROL_NAME As String = "moToleranceLabel"
        Private Const DEALERMARKUPPCT1_CONTROL_NAME As String = "moDealerMarkupPctText1"
        Private Const DEALERMARKUPPCT1_LABEL_CONTROL_NAME As String = "moDealerMarkupPctLabel1"
        Private Const DEALERMARKUPPCT2_CONTROL_NAME As String = "moDealerMarkupPctText2"
        Private Const DEALERMARKUPPCT2_LABEL_CONTROL_NAME As String = "moDealerMarkupPctLabel2"
        Private Const DEALERMARKUPPCT3_CONTROL_NAME As String = "moDealerMarkupPctText3"
        Private Const DEALERMARKUPPCT3_LABEL_CONTROL_NAME As String = "moDealerMarkupPctLabel3"
        Private Const DEALERMARKUPPCT4_CONTROL_NAME As String = "moDealerMarkupPctText4"
        Private Const DEALERMARKUPPCT4_LABEL_CONTROL_NAME As String = "moDealerMarkupPctLabel4"
        Private Const DEALERMARKUPPCT5_CONTROL_NAME As String = "moDealerMarkupPctText5"
        Private Const DEALERMARKUPPCT5_LABEL_CONTROL_NAME As String = "moDealerMarkupPctLabel5"

        Private Const BROKERMARKUPPCT1_CONTROL_NAME As String = "moBrokerMarkupPctText1"
        Private Const BROKERMARKUPPCT1_LABEL_CONTROL_NAME As String = "moBrokerMarkupPctLabel1"
        Private Const BROKERMARKUPPCT2_CONTROL_NAME As String = "moBrokerMarkupPctText2"
        Private Const BROKERMARKUPPCT2_LABEL_CONTROL_NAME As String = "moBrokerMarkupPctLabel2"
        Private Const BROKERMARKUPPCT3_CONTROL_NAME As String = "moBrokerMarkupPctText3"
        Private Const BROKERMARKUPPCT3_LABEL_CONTROL_NAME As String = "moBrokerMarkupPctLabel3"
        Private Const BROKERMARKUPPCT4_CONTROL_NAME As String = "moBrokerMarkupPctText4"
        Private Const BROKERMARKUPPCT4_LABEL_CONTROL_NAME As String = "moBrokerMarkupPctLabel4"
        Private Const BROKERMARKUPPCT5_CONTROL_NAME As String = "moBrokerMarkupPctText5"
        Private Const BROKERMARKUPPCT5_LABEL_CONTROL_NAME As String = "moBrokerMarkupPctLabel5"

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

#End Region

#Region "Variables"

        Private moExpirationData As CommPlanData
        Private moCoverageRate As CommPlanDistribution
        Private mbIsNewRate As Boolean


#Region "Variables-Tolerance"


#End Region

#End Region

#Region "Properties"

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
                        moCoverageRate = New CommPlanDistribution
                        CoverageRateId = moCoverageRate.Id.ToString
                    Else
                        ' For updating, deleting
                        Me.State.MyBoDist = New CommPlanDistribution(Me.State.moCommPlanDistId)
                        If CoverageRateId = "" Then
                            CoverageRateId = Guid.Empty.ToString
                        End If

                        moCoverageRate = New CommPlanDistribution(Me.State.moCommPlanDistId)
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

        Public ReadOnly Property HasDealerConfigeredForSourceXcd() As Boolean
            Get
                Dim isDealerConfiguredForSourceXcd As Boolean
                isDealerConfiguredForSourceXcd = False
                If Not Me.State.MyBo Is Nothing Then
                    If (Me.State.MyBo.DealerId <> Guid.Empty) Then
                        Dim oDealer As New Dealer(Me.State.MyBo.DealerId)

                        If Not oDealer.AcctBucketsWithSourceXcd Is Nothing Then
                            If oDealer.AcctBucketsWithSourceXcd.Equals(Codes.EXT_YESNO_Y) Then
                                isDealerConfiguredForSourceXcd = True
                            Else
                                isDealerConfiguredForSourceXcd = False
                            End If
                        Else
                            isDealerConfiguredForSourceXcd = False
                        End If
                    Else
                        isDealerConfiguredForSourceXcd = False
                    End If
                Else
                    isDealerConfiguredForSourceXcd = False
                End If
                Return isDealerConfiguredForSourceXcd
            End Get
        End Property

        Public ReadOnly Property HasCompanyConfigeredForSourceXcd() As Boolean
            Get
                Dim isCompanyConfiguredForSourceXcd As Boolean
                isCompanyConfiguredForSourceXcd = False
                If Not TheDealerControl Is Nothing Then
                    If (TheDealerControl.SelectedGuid <> Guid.Empty) Then
                        Dim oDealer As New Dealer(TheDealerControl.SelectedGuid)
                        Dim oCompany As New Company(oDealer.CompanyId)
                        If Not oCompany.AttributeValues Is Nothing Then
                            If oCompany.AttributeValues.Contains("NEW_COMMISSION_MODULE_CONFIGURED") Then
                                If oCompany.AttributeValues.Value(Codes.NEW_COMMISSION_MODULE_CONFIGURED) = Codes.YESNO_Y Then
                                    isCompanyConfiguredForSourceXcd = True
                                Else
                                    isCompanyConfiguredForSourceXcd = False
                                End If
                            Else
                                isCompanyConfiguredForSourceXcd = False
                            End If
                        Else
                            isCompanyConfiguredForSourceXcd = False
                        End If
                    Else
                        isCompanyConfiguredForSourceXcd = False
                    End If
                Else
                    isCompanyConfiguredForSourceXcd = False
                End If
                Return isCompanyConfiguredForSourceXcd
            End Get
        End Property
        Private Property CoverageRateId1() As String
            Get
                Return GetGuidStringFromByteArray(Me.State.moCommPlanId.ToByteArray)
            End Get
            Set(ByVal Value As String)
                Me.State.moCommPlanId = GetGuidFromString(Value)
            End Set
        End Property
        Private Property CoverageRateId() As String
            Get
                Return moCoverageRateIdLabel.Text
            End Get
            Set(ByVal Value As String)
                moCoverageRateIdLabel.Text = Value
            End Set
        End Property

#Region "Properties-Tolerance"

        Private ReadOnly Property TheComTolerance() As CommissionTolerance
            Get
                If Me.State.moCommissionToleranceId = Guid.Empty Then
                    ' For creating, inserting
                    Me.State.moCommTolerance = Me.State.MyBo.AddCommTolerance(Nothing)
                    Me.State.moCommissionToleranceId = Me.State.moCommTolerance.Id
                Else
                    ' For updating, deleting
                    Me.State.moCommTolerance = Me.State.MyBo.AddCommTolerance(Me.State.moCommissionToleranceId)
                End If

                Return Me.State.moCommTolerance
            End Get
        End Property

#End Region

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
                    'US-521672
                    Me.moGridView.Visible = True
                    'US-521672
                    SetGridSourceXcdLabelFromBo()

                    Me.AddControlMsg(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO,
                                     Me.MSG_TYPE_CONFIRM, True)
                    Me.AddCalendar(Me.BtnEffectiveDate_WRITE, Me.moEffectiveText_WRITE)
                    Me.AddCalendar(Me.BtnExpirationDate_WRITE, Me.moExpirationText_WRITE)
                Else
                    Me.moGridView.Visible = True
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
            Dim retType As New CommissionPeriodSearchForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.moCommPlanId, Me.State.boChanged)
            Me.ReturnToCallingPage(retType)
        End Sub

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try
                If IsDirtyPeriodBO() = True Then
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

        Private Sub SavePeriodChanges()
            If ApplyPeriodChanges() = True Then
                Me.State.boChanged = True
                ClearTolerance()
                PopulatePeriod()
                Me.State.IsCommPlanDistNew = False
                SetPeriodButtonsState(False)
            End If
        End Sub

        Private Sub btnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
            Try
                SavePeriodChanges()
                SetGridSourceXcdLabelFromBo()
            Catch ex As Exception
                SetGridSourceXcdLabelFromBo()
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try
                ClearPeriod()
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
            ClearPeriod()
            SetPeriodButtonsState(True)
            PopulatePeriod()
            TheDealerControl.ChangeEnabledControlProperty(True)
        End Sub

        Private Sub btnNew_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
            Try
                If IsDirtyPeriodBO() = True Then
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
            ClearTolerance()
        End Sub

        Private Sub btnCopy_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy_WRITE.Click
            Try
                If IsDirtyPeriodBO() = True Then
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
                'Me.State.IsCommPlanDistNew = True
                'ClearPeriod()
                'SetPeriodButtonsState(True)
                'PopulatePeriod()
                'PopulateCoverageRateList()
                'TheDealerControl.ChangeEnabledControlProperty(True)

                EnableDateFields()
                'SetGridControls(moGridView, True)
                'FillSourceXcdDropdownList()
                'FillEntityDropDownList()
                'FillPayeeTypeDropDownList()
                'SetGridSourceXcdDropdownFromBo()
                'SetGridSourceXcdLabelFromBo()
                Me.moGridView.Visible = True
                BtnNewRate_WRITE.Visible = True
                btnBack.Visible = True
                'Me.DisplayMessage(Message.MSG_COMPANY_NOT_CONFIGURED_FOR_DEALER, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO, Me.HiddenSaveChangesPromptResponse)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Handlers-Labels"
        Protected Sub BindBoPropertiesToLabels(ByVal oPeriod As CommPlan)
            Me.BindBOPropertyToLabel(oPeriod, DEALER_ID_PROPERTY, Me.TheDealerControl.CaptionLabel)
            Me.BindBOPropertyToLabel(oPeriod, EFFECTIVE_DATE_PROPERTY, moEffectiveLabel)
            Me.BindBOPropertyToLabel(oPeriod, EXPIRATION_DATE_PROPERTY, moExpirationLabel)
            Me.BindBOPropertyToLabel(oPeriod, CODE_PROPERTY, Me.LabelCode)
            Me.BindBOPropertyToLabel(oPeriod, DESCRIPTION_PROPERTY, Me.LabelDescription)
        End Sub

        Protected Sub BindBoPropertiesToLabels(ByVal oPeriod As CommPlanDistribution)
            Me.BindBOPropertyToLabel(oPeriod, DEALER_ID_PROPERTY, Me.TheDealerControl.CaptionLabel)
            Me.BindBOPropertyToLabel(oPeriod, EFFECTIVE_DATE_PROPERTY, moEffectiveLabel)
            Me.BindBOPropertyToLabel(oPeriod, EXPIRATION_DATE_PROPERTY, moExpirationLabel)
            Me.BindBOPropertyToLabel(oPeriod, CODE_PROPERTY, Me.LabelCode)
            Me.BindBOPropertyToLabel(oPeriod, DESCRIPTION_PROPERTY, Me.LabelDescription)
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
                PopulateTolerance()
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

        Private Sub ClearPeriod()
            ClearDealer()
            ClearTolerance()
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

        Private Sub PopulatePeriodBOFromForm(ByVal oPeriod As CommPlan)
            With oPeriod
                ' DropDowns
                .DealerId = TheDealerControl.SelectedGuid 'Me.GetSelectedItem(moDealerDrop_WRITE)
                Me.PopulateBOProperty(oPeriod, REFERENCE_SOURCE_PROPERTY, "ELP_DEALER")
                Me.PopulateBOProperty(oPeriod, CODE_PROPERTY, Me.TextBoxCode)
                Me.PopulateBOProperty(oPeriod, DESCRIPTION_PROPERTY, Me.TextBoxDescription)
                ' Texts
                Me.PopulateBOProperty(oPeriod, EFFECTIVE_DATE_PROPERTY, moEffectiveText_WRITE)
                Me.PopulateBOProperty(oPeriod, EXPIRATION_DATE_PROPERTY, moExpirationText_WRITE)

            End With

            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub
        Private Sub PopulatePeriodBOFromForm(ByVal oPeriod As CommPlanDistribution)
            With oPeriod
                Me.PopulateBOProperty(oPeriod, REFERENCE_SOURCE_PROPERTY, "ELP_DEALER")
                Me.PopulateBOProperty(oPeriod, CODE_PROPERTY, Me.TextBoxCode)
                Me.PopulateBOProperty(oPeriod, DESCRIPTION_PROPERTY, Me.TextBoxDescription)
                ' Texts
                Me.PopulateBOProperty(oPeriod, EFFECTIVE_DATE_PROPERTY, moEffectiveText_WRITE)
                Me.PopulateBOProperty(oPeriod, EXPIRATION_DATE_PROPERTY, moExpirationText_WRITE)

            End With

            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub
        Private Function IsDirtyPeriodBO() As Boolean
            Dim bIsDirty As Boolean = True
            Dim oPeriod As CommPlan

            oPeriod = TheCommPlan
            With oPeriod
                PopulatePeriodBOFromForm(Me.State.MyBo)
                bIsDirty = .IsDirty
            End With
            If Not bIsDirty Then
                If Not Me.State.moCommTolerance Is Nothing Then
                    bIsDirty = IsDirtyToleranceBO()
                End If
            End If
            Return bIsDirty
        End Function

        Private Sub ValidatePayeeType()

        End Sub

        Private Function ApplyPeriodChanges() As Boolean
            Dim bIsOk As Boolean = True
            Dim oPeriod As CommPlan

            Try
                If IsDirtyPeriodBO() = True Then
                    If Not Me.State.moCommTolerance Is Nothing Then
                        PopulateToleranceBOFromForm()
                    End If
                    oPeriod = TheCommPlan
                    oPeriod.Save()
                    Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
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
            Dim oPeriod As CommPlan

            Try
                oPeriod = TheCommPlan
                With oPeriod
                    .Delete()
                    .Save()
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
                bIsOk = False
            End Try
            Return bIsOk
        End Function

#End Region


#Region "Tolerance"

#Region "Tolerance Handlers"

#Region "Tolerance Handlers-Buttons"

        Private Sub NewTolerance()
            Me.State.moCommissionToleranceId = Guid.Empty
            Me.State.moCommTolerance = New CommissionTolerance
            Me.State.IsToleranceNew = True
            ControlMgr.SetVisibleControl(Me, moPeriodButtonPanel, False)
            Me.EnableDisableControls(moCoverageEditPanel, True)
            EnableToleranceGrid(False)
            InitializeFormTolerance()
            PopulateFormFromPeriodEntityBO()
        End Sub

        Private Sub BtnNewGrid_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNewGrid_WRITE.Click
            Try
                If IsDirtyPeriodBO() = True Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM,
                                            Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Accept
                Else
                    NewTolerance()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub BtnUndoGrid_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnUndoGrid_WRITE.Click
            EditTolerance()
        End Sub

        Private Sub SaveDistributionChanges()
            If ApplyToleranceChanges() = True Then

                Me.State.MyBo = New CommPlan(Me.State.MyBo.Id)
                Me.State.MyBoDist = New CommPlanDistribution(Me.State.MyBoDist.Id)

                PopulateFormFromPeriodEntityBO()
                PopulateTolerance(Me.POPULATE_ACTION_SAVE)
                ControlMgr.SetVisibleControl(Me, moPeriodButtonPanel, True)
                PopulatePeriodEntity()
                TheDealerControl.ChangeEnabledControlProperty(False)
                ControlMgr.SetEnableControl(Me, TextBoxCode, True)
                ControlMgr.SetEnableControl(Me, TextBoxDescription, True)
            End If
        End Sub

        Private Sub BtnSaveGrid_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSaveGrid_WRITE.Click
            Try
                SaveDistributionChanges()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub BtnCancelGrid_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancelGrid.Click
            Try
                If IsDirtyPeriodBO() = True Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM,
                                      Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    Me.State.IsToleranceNew = False
                    PopulateTolerance(Me.POPULATE_ACTION_NO_EDIT)
                    ControlMgr.SetVisibleControl(Me, moPeriodButtonPanel, True)
                    Me.EnableDisableControls(moCoverageEditPanel, False)
                    TheDealerControl.ChangeEnabledControlProperty(False)
                    ControlMgr.SetVisibleControl(Me, Me.btnEntityBack, False)
                End If
                'US-521672
                SetGridSourceXcdLabelFromBo()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region


#Region "Tolerance Handlers-Grid"

        Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                Me.Grid.PageIndex = e.NewPageIndex
                PopulateTolerance(Me.POPULATE_ACTION_NO_EDIT)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Public Sub RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub EditTolerance()
            Me.State.IsToleranceNew = False
            EnableToleranceGrid(False)
            PopulateFormFromToleranceBO()
            ControlMgr.SetVisibleControl(Me, moPeriodButtonPanel, False)
            Me.EnableDisableControls(moCoverageEditPanel, True)
        End Sub

        Private Sub RowCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
            Try
                If (e.CommandName = Me.EDIT_COMMAND_NAME) Then
                    Dim index As Integer = CInt(e.CommandArgument)
                    Me.State.moCommissionToleranceId = Me.GetGuidFromString(
                                    Me.GetGridText(Me.Grid, index, COMMISSION_TOLERANCE_ID_COL))

                    ControlMgr.SetVisibleControl(Me, btnEntityBack, True)
                    Me.State.moCommTolerance = New CommissionTolerance(Me.State.moCommissionToleranceId)
                    EditTolerance()
                    PopulatePeriodEntity()
                ElseIf (e.CommandName = Me.DELETE_COMMAND_NAME) Then
                    Dim index As Integer = CInt(e.CommandArgument)
                    Me.State.moCommissionToleranceId = Me.GetGuidFromString(
                                    Me.GetGridText(Me.Grid, index, COMMISSION_TOLERANCE_ID_COL))

                    If DeleteTolerance() = True Then
                        PopulateTolerance(Me.POPULATE_ACTION_NO_EDIT)
                        PopulatePeriodEntity()
                    End If
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub BindBoPropertiesToGridHeaders(ByVal oTolerance As CommissionTolerance)
            Me.BindBOPropertyToGridHeader(oTolerance, ALLOWED_MARKUP_PCT_PROPERTY,
                                                            Me.Grid.Columns(ALLOWED_MARKUP_COL))
            Me.BindBOPropertyToGridHeader(oTolerance, TOLERANCE_PROPERTY,
                                                            Me.Grid.Columns(TOLERANCE_COL))
        End Sub

#End Region

#End Region

#Region "Tolerance Button-Management"

        Public Overrides Sub BaseSetButtonsState(ByVal bIsEdit As Boolean)
            SetToleranceButtonsState(bIsEdit)
        End Sub

        Private Sub SetToleranceButtonsState(ByVal bIsEdit As Boolean)
            If Me.State.IsCommPlanDistNew = False Then
                ControlMgr.SetVisibleControl(Me, BtnNewGrid_WRITE, Not bIsEdit)
            End If
            ControlMgr.SetVisibleControl(Me, BtnSaveGrid_WRITE, bIsEdit)
            ControlMgr.SetVisibleControl(Me, BtnCancelGrid, bIsEdit)
            ControlMgr.SetVisibleControl(Me, BtnUndoGrid_WRITE, bIsEdit)
        End Sub

        Private Sub DisableToleranceButtons()
            ControlMgr.SetVisibleControl(Me, BtnNewGrid_WRITE, False)
            SetToleranceButtonsState(False)
        End Sub

        Private Sub EnableRestrictMarkup(ByVal bIsReadWrite As Boolean, ByVal nRecCount As Integer)
            Me.Grid.Columns(ALLOWED_MARKUP_COL).Visible = bIsReadWrite
            Me.Grid.Columns(TOLERANCE_COL).Visible = bIsReadWrite
            If (bIsReadWrite = False) AndAlso (nRecCount = 1) Then
                ' Restrict Markup = 'N' then, period can have at most one Tolerance
                ControlMgr.SetVisibleControl(Me, BtnNewGrid_WRITE, False)
            End If
        End Sub

        Private Sub EnableRestrictMarkupDetail(ByVal bIsReadWrite As Boolean)
            ControlMgr.SetVisibleControl(Me, moRestrictDetailPanel, bIsReadWrite)
        End Sub

        Private Sub EnableToleranceGrid(ByVal bIsEnable As Boolean)
            ControlMgr.SetVisibleControl(Me, moGridPanel, bIsEnable)
            ControlMgr.SetVisibleControl(Me, BtnNewGrid_WRITE, bIsEnable)
            ControlMgr.SetVisibleControl(Me, moDetailPanel_WRITE, Not bIsEnable)
            If Not bIsEnable Then
                Me.MasterPage.MessageController.Clear_Hide()
                BindBoPropertiesToLabels()
            End If

            ControlMgr.SetVisibleControl(Me, BtnSaveGrid_WRITE, Not bIsEnable)
            ControlMgr.SetVisibleControl(Me, BtnCancelGrid, Not bIsEnable)
            ControlMgr.SetVisibleControl(Me, BtnUndoGrid_WRITE, Not bIsEnable)

        End Sub

#End Region

#Region "Tolerance Populate"
        Protected Sub BindBoPropertiesToLabels()
            Me.BindBOPropertyToLabel(Me.State.moCommTolerance, ALLOWED_MARKUP_PCT_PROPERTY, Me.moAllowedMarkupPctDetailLabel)
            Me.BindBOPropertyToLabel(Me.State.moCommTolerance, TOLERANCE_PROPERTY, Me.moToleranceDetailLabel)
            Me.ClearGridHeadersAndLabelsErrSign()
        End Sub

        Protected Sub BindBoAssocCommToLabels(ByVal obj As CommPlanDistribution)
            Me.BindBOPropertyToLabel(obj, "CommissionAmount", Me.lblMarkup)
            Me.BindBOPropertyToLabel(obj, "CommissionPercent", Me.lblComm)
        End Sub

        Private Sub PopulateToleranceFormItem(ByVal oControl As TextBox, ByVal oPropertyValue As Object)
            Me.PopulateControlFromBOProperty(oControl, oPropertyValue)
        End Sub

        Private Sub PopulateFormFromToleranceBO()
            Try
                EnableRestrictMarkupDetail(GetRestrictMarkup())
                With TheComTolerance
                    PopulateToleranceFormItem(moAllowedMarkupPctDetailText, .AllowedMarkupPct)
                    PopulateToleranceFormItem(moToleranceDetailText, .Tolerance)
                End With

                PopulateFormFromPeriodEntityBO()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Sub PopulateFormFromPeriodEntityBO()
        End Sub

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

        Private Sub InitializeFormTolerance()
            Dim nInitValue As New DecimalType(0)

            EnableRestrictMarkupDetail(GetRestrictMarkup())
            PopulateToleranceFormItem(moAllowedMarkupPctDetailText, nInitValue)
            PopulateToleranceFormItem(moToleranceDetailText, nInitValue)

        End Sub

        Private Sub PopulateToleranceBOItem(ByVal oComTolerance As CommissionTolerance, ByVal oPropertyName As String,
                                                ByVal oControl As TextBox)
            Me.PopulateBOProperty(oComTolerance, oPropertyName, oControl)
        End Sub

        Private Sub PopulateToleranceBOFromForm()

            With Me.State.moCommTolerance
                BindBoPropertiesToLabels()
                .CommissionPeriodId = Me.State.MyBo.Id
                If GetRestrictMarkup() = True Then
                    Me.PopulateToleranceBOItem(Me.State.moCommTolerance, ALLOWED_MARKUP_PCT_PROPERTY, moAllowedMarkupPctDetailText)
                    Me.PopulateToleranceBOItem(Me.State.moCommTolerance, TOLERANCE_PROPERTY, moToleranceDetailText)
                Else
                    If Me.State.IsToleranceNew Then
                        .AllowedMarkupPct = New DecimalType(0)
                        .Tolerance = New DecimalType(0)
                    End If
                End If
                If .IsDirty Then
                    '.Validate()
                End If
            End With

            PopulateAssocCommBOFromForm()
            PopulateCommPlanDistBOFromForm()

            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If

        End Sub

        Private Function GetToleranceDataView() As DataView
            Dim oCommissionTolerance As CommissionToleraneData = New CommissionToleraneData
            Dim commToleranceView As DataView = Me.State.moCommTolerance.getList(Me.State.MyBo.Id)
            Return commToleranceView

        End Function

        Public Overrides Sub AddNewBoRow(ByVal dv As DataView)
            Dim oId As Guid = Guid.NewGuid

            Me.BaseAddNewGridRow(Me.Grid, dv, oId)
            InitializeFormTolerance()
        End Sub

        Private Sub PopulateTolerance(Optional ByVal oAction As String = POPULATE_ACTION_NONE)
            Dim oDataView As DataView

            Try
                If Me.State.IsCommPlanDistNew = True Then Return ' We can not have Tolerances if the Period is new

                EnableToleranceGrid(True)
                oDataView = GetToleranceDataView()
                Me.State.searchDV = oDataView
                BasePopulateGrid(Me.Grid, Me.State.searchDV, Me.State.moCommissionToleranceId, oAction)
                'SetGridSourceXcdLabelFromBo()
                EnableRestrictMarkup(GetRestrictMarkup(), oDataView.Count)
                ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Me.Grid)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
                If Not Me.State.IsDealerConfiguredForSourceXcd Then
                    Me.State.IsDealerConfiguredForSourceXcd = HasDealerConfigeredForSourceXcd()
                End If
                SetGridSourceXcdLabelFromBo()
            End Try

        End Sub
        Private Sub PopulatePayeeType()
        End Sub
        Private Sub PopulatePeriodEntity()
        End Sub

#End Region

#Region "Tolerance Clear"

        Private Sub ClearTolerance()
            DisableToleranceButtons()
            Me.Grid.DataSource = Nothing
            Me.Grid.DataBind()
        End Sub

#End Region

#Region "Tolerance-Business Part"

        Private Function GetRestrictMarkup() As Boolean
            Dim oPeriodData As New CommissionPeriodData

            oPeriodData.dealerId = TheDealerControl.SelectedGuid ' Me.GetSelectedItem(moDealerDrop_WRITE)
            Return CommissionPeriod.GetRestrictMarkup(oPeriodData)

        End Function

        Private Function IsDirtyToleranceBO() As Boolean
            Dim bIsDirty As Boolean = True
            Try
                With Me.State.moCommTolerance
                    PopulateToleranceBOFromForm()
                    bIsDirty = .IsDirty
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Return bIsDirty
        End Function

        Private Function ApplyToleranceChanges() As Boolean
            Dim bIsOk As Boolean = True
            Dim bIsDirty As Boolean
            Dim oComTolerance As CommissionTolerance
            Try
                oComTolerance = TheComTolerance
                With Me.State.MyBo
                    PopulateToleranceBOFromForm()
                    bIsDirty = .IsDirty Or oComTolerance.IsDirty Or Me.State.moIsAssocCommDirty
                    .Save()
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
                bIsOk = False

            End Try
            If bIsOk Then
                If bIsDirty Then
                    Me.State.IsToleranceNew = False
                    Me.State.moIsAssocCommDirty = False
                    BaseSetButtonsState(False)
                    Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                Else
                    Me.DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    bIsOk = False
                End If
            End If

            Return bIsOk
        End Function


        Private Function DeleteTolerance() As Boolean
            Dim bIsOk As Boolean = True
            Me.State.moCommTolerance = New CommissionTolerance(Me.State.moCommissionToleranceId)
            Try
                With Me.State.moCommTolerance
                    .Delete()
                    .Save()
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
                bIsOk = False
            End Try
            Me.State.moCommTolerance = Nothing
            Return bIsOk
        End Function

#End Region

#End Region

#Region "State-Management"

#Region "Period State-Management"

        Protected Sub ComingFromBack()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                ' Return from the Back Button

                Select Case confResponse
                    Case Me.MSG_VALUE_YES
                        ' Save and go back to Search Page
                        If ApplyPeriodChanges() = True Then
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
                        If ApplyPeriodChanges() = True Then
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
                        If ApplyPeriodChanges() = True Then
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
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                ' Return from the Back Button

                Select Case confResponse
                    Case Me.MSG_BTN_OK
                        SetStateProperties()
                        SetGridControls(moGridView, True)
                        FillSourceXcdDropdownList()
                        FillEntityDropDownList()
                        FillPayeeTypeDropDownList()
                        SetGridSourceXcdDropdownFromBo()
                        SetGridSourceXcdLabelFromBo()
                End Select
            End If

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
                        If ApplyPeriodChanges() = True Then
                            Me.State.boChanged = True
                            EditTolerance()
                        End If
                    Case Me.MSG_VALUE_NO

                End Select
            End If

        End Sub

        Protected Sub ComingFromNewTolerance()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                ' Return from the Back Button

                Select Case confResponse
                    Case Me.MSG_VALUE_YES
                        ' Save and go back to Search Page
                        If ApplyPeriodChanges() = True Then
                            Me.State.boChanged = True
                            NewTolerance()
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
                        ComingFromNewDistribution()
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ComingFromBack()
                    Case ElitaPlusPage.DetailPageCommand.New_
                        ComingFromNewCopy()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        ComingFromNewCopy()

                    ' Breadkdown
                    Case ElitaPlusPage.DetailPageCommand.Redirect_
                        ComingFromEditTolerance()
                    Case ElitaPlusPage.DetailPageCommand.Accept
                        ComingFromNewTolerance()
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

        Private Sub btnEntySave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEntitySave.Click

            ApplyPeriodChanges()

        End Sub
        Private Sub UpdateEntityTable()
        End Sub
        Private Sub PopulateAssocCommBOFromForm()
        End Sub

        Private Sub PopulateCommPlanDistBOFromForm()
        End Sub

        Private Sub btnEntityUndo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEntityUndo.Click
            Dim dealerId As Guid = TheDealerControl.SelectedGuid
            If dealerId.Equals(Guid.Empty) Then
                Exit Sub
            End If
            PopulatePayeeType()
            PopulatePeriodEntity()
            TheDealerControl.ChangeEnabledControlProperty(False)

        End Sub

#Region "Acct Source Xcd Option Bucket Logic"
        Private Sub ValidatePmCertSourceLogic()
            'Avoid Price Metrics and Cert Commission combination
            ValidatePmCertCommSourceXcd()

            If Me.State.IsPmComCombination Then
                ElitaPlusPage.SetLabelError(Me.lblAcctSourceBucket)
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
                        'Dim gRow As GridViewRow = moGridView.Rows(moGridView.EditIndex)
                        Dim mocboCommPercentSourceXcd As DropDownList = DirectCast(gRow.Cells(COL_COMMISSIONS_SOURCE_XCD_IDX).FindControl("cboCommPercentSourceXcd"), DropDownList)
                        Dim mollblCommPercentSourceXcd As Label = DirectCast(gRow.Cells(COL_COMMISSIONS_SOURCE_XCD_IDX).FindControl("lblCommPercentSourceXcd"), Label)
                        Me.State.IsDiffSelectedTwice = False
                        Me.State.IsDiffNotSelectedOnce = False

                        'ACCT_BUCKETS_SOURCE_COMMBRKDOWN_OPTION_PRICEMATRIX
                        If mocboCommPercentSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_COMMBRKDOWN_OPTION_CERTCOMM) Then
                            countPM = countPM + 1
                        End If

                        If mocboCommPercentSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_COMMBRKDOWN_OPTION_PRICEMATRIX) Then
                            countCertComm = countCertComm + 1
                        End If
                    End If
                Next
            Next

            If ((countPM = 1 Or countPM > 1) And (countCertComm = 1 Or countCertComm > 1)) Then
                Me.State.IsPmComCombination = True
            Else
                Me.State.IsPmComCombination = False
            End If
        End Sub

        Private Sub CheckPMCertCombonation()
            If moGridView.Rows.Count > 0 Then
                Dim i As Integer = 0
                Dim oCoverageRate(moGridView.Rows.Count - 1) As CommPlanDistribution

                For i = 0 To moGridView.Rows.Count - 1
                    oCoverageRate(i) = New CommPlanDistribution
                    dim str As String
                    
                    If moGridView.Rows(i).Cells(COL_PAYEE_TYPE_XCD_IDX).Controls(1).GetType().ToString = "System.Web.UI.WebControls.Label" Then
                       str   = CType(moGridView.Rows(i).Cells(COL_PAYEE_TYPE_XCD_IDX).Controls(1), Label).Text
                    Else
                       str = CType(moGridView.Rows(i).Cells(COL_PAYEE_TYPE_XCD_IDX).Controls(1), DropDownList).Text
                    End If
                Next

                'Me.State.moCoverageRateList = oCoverageRate
            End If
        End Sub

        Private Sub SetGridSourceXcdLabelFromBo()
            'If moGridView.EditIndex = -1 Then Exit Sub
            For pageIndexk As Integer = 0 To Me.moGridView.PageCount - 1
                Me.moGridView.PageIndex = pageIndexk
                Dim rowNum As Integer = Me.moGridView.Rows.Count
                For i As Integer = 0 To rowNum - 1
                    Dim gRow As GridViewRow = moGridView.Rows(i)
                    If gRow.RowType = DataControlRowType.DataRow Then
                        Dim mollblCommPercentSourceXcd As Label = DirectCast(gRow.Cells(COL_COMMISSIONS_SOURCE_XCD_IDX).FindControl("lblCommPercentSourceXcd"), Label)
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
                                    mollblCommPercentSourceXcd.Text = GetCodeAmtSourceOption(mollblCommPercentSourceXcd.Text)
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

            'Me.BindListControlToDataView(cboPeriodEntity1, oDataView, , , True)
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
                'Dim lkYesNo As DataView = LookupListNew.DropdownLookupList("YESNO", Authentication.LangId)
                'lkYesNo.RowFilter += " and code='Y'"
                'Dim yesId As Guid = New Guid(CType(lkYesNo.Item(0).Item("ID"), Byte()))
                If oDealerGrp.AcctingByGroupId.Equals(yesId) Then
                    'dvPayeeType.RowFilter += "and code <>'" & Payee_Type_Dealer & "'"
                    FilteredPayeeTypeList = (From lst In PayeeTypeList
                                             Where lst.Code <> Payee_Type_Dealer
                                             Select lst).ToArray()
                Else
                    'dvPayeeType.RowFilter += "and code <>'" & Payee_Type_Dealer_Group & "'"
                    FilteredPayeeTypeList = (From lst In PayeeTypeList
                                             Where lst.Code <> Payee_Type_Dealer_Group
                                             Select lst).ToArray()
                End If
            Else
                'dvPayeeType.RowFilter += "and code <>'" & Payee_Type_Dealer_Group & "'"
                FilteredPayeeTypeList = (From lst In PayeeTypeList
                                         Where lst.Code <> Payee_Type_Dealer_Group
                                         Select lst).ToArray()
            End If

            'Me.BindListControlToDataView(Me.cboPayeeType1, dvPayeeType, , , False)
            mocboPayeeType.Populate(FilteredPayeeTypeList, New PopulateOptions() With
            {
                .AddBlankItem = False,
                .TextFunc = AddressOf PopulateOptions.GetDescription,
                .ValueFunc = AddressOf PopulateOptions.GetExtendedCode
            })

            FilteredPayeeType1List = (From lst In PayeeTypeList
                                      Where lst.Code = Payee_Type_Comm_Entity
                                      Select lst).ToArray()

            'If dvPayeeType1.Count = 1 Then
            '    Me.litScriptVars.Text += "var commEntity = '" + GuidControl.ByteArrayToGuid(dvPayeeType1.Item(0)(LookupListNew.COL_ID_NAME)).ToString + "';"
            'End If

            'If FilteredPayeeType1List.Count = 1 Then
            '    Me.litScriptVars.Text += "var commEntity = '" + FilteredPayeeType1List.First().ListItemId.ToString + "';"
            'End If

            'If Me.State.IsCommPlanDistNew = True Then
            Dim FirstPayeeType As String
            FirstPayeeType = FilteredPayeeType1List.First().ListItemId.ToString()
            'BindSelectItem(GuidControl.ByteArrayToGuid(dvPayeeType1.Item(0)(LookupListNew.COL_ID_NAME)).ToString, cboPayeeType1)
            'BindSelectItem(GuidControl.ByteArrayToGuid(dvPayeeType1.Item(0)(LookupListNew.COL_ID_NAME)).ToString, cboPayeeType2)
            'BindSelectItem(GuidControl.ByteArrayToGuid(dvPayeeType1.Item(0)(LookupListNew.COL_ID_NAME)).ToString, cboPayeeType3)
            'BindSelectItem(GuidControl.ByteArrayToGuid(dvPayeeType1.Item(0)(LookupListNew.COL_ID_NAME)).ToString, cboPayeeType4)
            'BindSelectItem(GuidControl.ByteArrayToGuid(dvPayeeType1.Item(0)(LookupListNew.COL_ID_NAME)).ToString, cboPayeeType5)
            BindSelectItem(FirstPayeeType, mocboPayeeType)
            'BindSelectItem(FirstPayeeType, cboPayeeType2)
            'BindSelectItem(FirstPayeeType, cboPayeeType3)
            'BindSelectItem(FirstPayeeType, cboPayeeType4)
            'BindSelectItem(FirstPayeeType, cboPayeeType5)
            'End If

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
            DisplaySourceXcdFields()

            Dim oAcctBucketsSourceOption As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="ACCTBUCKETSOURCE_COMMBRKDOWN")

            oDropDownList.Populate(oAcctBucketsSourceOption, New PopulateOptions() With
                                {
                                .AddBlankItem = False,
                                .TextFunc = AddressOf PopulateOptions.GetDescription,
                                .ValueFunc = AddressOf PopulateOptions.GetExtendedCode
                                })
            HideSourceScdFields()
        End Sub

        Private Sub DisplaySourceXcdFields()
        End Sub

        Private Sub HideSourceScdFields()
        End Sub

        Private Sub DisableMarkUpPercentage()
        End Sub

        Private Sub DisableCommPercentage()
        End Sub

        Public Function GetCodeAmtSourceOption(ByVal desc As String) As String
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
                    'sGetDescOfExtCodeFromPayeeTypeXcd = LookupListNew.GetCodeFromDescription("PYTYPE", ElitaPlusIdentity.Current.ActiveUser.LanguageId, desc)
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

        Private Sub LoadCoverageRateList()
            If moGridView.Rows.Count > 0 Then
                Dim i As Integer = 0
                Dim oCoverageRate(moGridView.Rows.Count - 1) As CommPlanDistribution

                For i = 0 To moGridView.Rows.Count - 1
                    oCoverageRate(i) = New CommPlanDistribution
                    'oCoverageRate(i).CommissionPlanId = TheCommPlanDist.CommissionPlanId
                    oCoverageRate(i).CommissionPlanId = Me.State.moCommPlanId 'TheCommPlanDist.CommissionPlanId
                    If moGridView.Rows(i).Cells(COL_PAYEE_TYPE_XCD_IDX).Controls(1).GetType().ToString = "System.Web.UI.WebControls.Label" Then
                        Me.PopulateBOProperty(oCoverageRate(i), PROPERTY_PAYEE_TYPE_XCD, CType(moGridView.Rows(i).Cells(COL_PAYEE_TYPE_XCD_IDX).Controls(1), Label).Text)
                    Else
                        Me.PopulateBOProperty(oCoverageRate(i), PROPERTY_PAYEE_TYPE_XCD, CType(moGridView.Rows(i).Cells(COL_PAYEE_TYPE_XCD_IDX).Controls(1), DropDownList).Text)
                    End If

                    If moGridView.Rows(i).Cells(COL_ENTITY_ID_IDX).Controls(0).GetType().ToString = "System.Web.UI.WebControls.Label" Then
                        Me.PopulateBOProperty(oCoverageRate(i), PROPERTY_ENTITY_ID, CType(moGridView.Rows(i).Cells(COL_ENTITY_ID_IDX).Controls(0), Label).Text)
                    Else
                        Me.PopulateBOProperty(oCoverageRate(i), PROPERTY_ENTITY_ID, CType(moGridView.Rows(i).Cells(COL_ENTITY_ID_IDX).Controls(1), DropDownList).SelectedValue)
                    End If

                    If moGridView.Rows(i).Cells(COL_COMMISSION_AMOUNT_IDX).Controls(1).GetType().ToString = "System.Web.UI.WebControls.Label" Then
                        Me.PopulateBOProperty(oCoverageRate(i), PROPERTY_COMM_AMT, CType(moGridView.Rows(i).Cells(COL_COMMISSION_AMOUNT_IDX).Controls(1), Label).Text)
                    Else
                        Me.PopulateBOProperty(oCoverageRate(i), PROPERTY_COMM_AMT, CType(moGridView.Rows(i).Cells(COL_COMMISSION_AMOUNT_IDX).Controls(1), TextBox).Text)
                    End If

                    If moGridView.Rows(i).Cells(COL_COMMISSION_PERCENTAGE_IDX).Controls(1).GetType().ToString = "System.Web.UI.WebControls.Label" Then
                        Me.PopulateBOProperty(oCoverageRate(i), PROPERTY_COMM_PER, CType(moGridView.Rows(i).Cells(COL_COMMISSION_PERCENTAGE_IDX).Controls(1), Label).Text)
                    Else
                        Me.PopulateBOProperty(oCoverageRate(i), PROPERTY_COMM_PER, CType(moGridView.Rows(i).Cells(COL_COMMISSION_PERCENTAGE_IDX).Controls(1), TextBox).Text)
                    End If

                    If moGridView.Rows(i).Cells(COL_COMMISSIONS_SOURCE_XCD_IDX).Controls(0).GetType().ToString = "System.Web.UI.WebControls.Label" Then
                        Me.PopulateBOProperty(oCoverageRate(i), PROPERTY_COMMISSIONS_XCD, CType(moGridView.Rows(i).Cells(COL_COMMISSIONS_SOURCE_XCD_IDX).Controls(0), Label).Text)
                    Else
                        Me.PopulateBOProperty(oCoverageRate(i), PROPERTY_COMMISSIONS_XCD, CType(moGridView.Rows(i).Cells(COL_COMMISSIONS_SOURCE_XCD_IDX).Controls(1), DropDownList).SelectedValue)
                    End If

                    If moGridView.Rows(i).Cells(COL_POSITION_IDX).Controls(1).GetType().ToString = "System.Web.UI.WebControls.Label" Then
                        Me.PopulateBOProperty(oCoverageRate(i), PROPERTY_POSITION, CType(moGridView.Rows(i).Cells(COL_POSITION_IDX).Controls(1), Label).Text)
                    Else
                        Me.PopulateBOProperty(oCoverageRate(i), PROPERTY_POSITION, CType(moGridView.Rows(i).Cells(COL_POSITION_IDX).Controls(1), TextBox).Text)
                    End If
                Next

                Me.State.moCoverageRateList = oCoverageRate
            End If
        End Sub

        Public Function SaveCoverageRateList() As Boolean
            Dim i As Integer = 0
            Try
                'If Me.State.IsNewWithCopy = True And Not Me.State.moCoverageRateList Is Nothing Then
                If Not Me.State.moCoverageRateList Is Nothing Then
                    'Associate each detail record to the newly created coverage record
                    'and Save each detail (Coverage Rate) Record
                    For i = 0 To Me.State.moCoverageRateList.Length - 1
                        Me.State.moCoverageRateList(i).CommissionPlanId = Me.State.moCommPlanId 'TheCommPlanDist.CommissionPlanId
                        Me.State.moCoverageRateList(i).Save()
                    Next
                End If
            Catch ex As Exception
                Dim j As Integer
                'REPLACE THIS LOOP BY A DB ROLLBACK
                For j = 0 To i - 1
                    Me.State.moCoverageRateList(j).Delete()
                    Me.State.moCoverageRateList(j).Save()
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

#Region "Handlers-CoverageRate-DataGrid"

        ' Coverage-Rate DataGrid
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
                PopulateCoverageRateList(ACTION_CANCEL_DELETE)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        'The pencil was clicked
        Protected Sub ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles moGridView.RowCommand
            Dim nIndex As Integer = CInt(e.CommandArgument)

            Try
                If e.CommandName = EDIT_COMMAND_NAME Then
                    nIndex = CInt(e.CommandArgument)
                    moGridView.EditIndex = nIndex
                    moGridView.SelectedIndex = nIndex
                    PopulateCoverageRateList(ACTION_EDIT)

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
                    CoverageRateId = Me.GetGridText(moGridView, nIndex, COL_COMMISSION_PLAN_DIST_ID_IDX)
                    Me.State.moCommPlanDistId = GetGuidFromString(CoverageRateId)
                    Me.State.MyBoDist = New CommPlanDistribution(Me.State.moCommPlanDistId)
                    If DeleteSelectedCoverageRate(nIndex) = True Then
                        PopulateCoverageRateList(ACTION_CANCEL_DELETE)
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
                'CheckPMCertCombonation
                'ValidatePmCertSourceLogic()
                
                SaveRateChanges()
                SetGridSourceXcdLabelFromBo()

                TheDealerControl.ChangeEnabledControlProperty(False)
            Catch ex As Exception
                SetGridSourceXcdLabelFromBo()
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub BtnCancelRate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancelRate.Click
            'Pencil button in not in edit mode
            Try
                Me.State.IsCommPlanDistNew = False
                EnableForEditRateButtons(False)
                PopulateCoverageRateList(ACTION_CANCEL_DELETE)
                SetGridSourceXcdLabelFromBo()
                TheDealerControl.ChangeEnabledControlProperty(False)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub BtnNewRate_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNewRate_WRITE.Click
            Try
                If moGridView.Rows.Count = 5 Then
                    'Throw New GUIException(Message.MSG_DISTRIBUTION_RECORD_LIMITED_FOR_EXTRACT_REPORT, Assurant.ElitaPlus.Common.ErrorCodes.MSG_COMMISSION_DISTRIBUTION_RECORD_LIMITED_FOR_EXTRACT_REPORT)
                    Me.DisplayMessage(Message.MSG_DISTRIBUTION_RECORD_LIMITED_FOR_EXTRACT_REPORT, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO, Me.HiddenSaveChangesPromptResponse)
                End If
                Me.State.IsCommPlanDistNew = True
                CoverageRateId = Guid.Empty.ToString
                PopulateCoverageRateList(ACTION_NEW)
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
            Catch ex As Exception
                SetStateProperties()
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

        Private Sub SaveRateChanges()
            If ApplyRateChanges() = True Then
                If Me.State.IsCommPlanDistNew = True Then
                    Me.State.IsCommPlanDistNew = False
                End If
                PopulateCoverageRateList(ACTION_SAVE)
            End If
        End Sub
        Private Function ApplyRateChanges() As Boolean
            Dim bIsOk As Boolean = True
            Dim bIsDirty As Boolean
            If moGridView.EditIndex < 0 Then Return False ' Coverage Rate is not in edit mode
            If Me.State.IsNewWithCopy Then
                Me.LoadCoverageRateList()
                Me.State.moCoverageRateList(moGridView.SelectedIndex).Validate()
                Return bIsOk
            End If
            If Me.State.IsCommPlanDistNew = False Then
                CoverageRateId = Me.GetSelectedGridText(moGridView, COL_COMMISSION_PLAN_DIST_ID_IDX)
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
        Private Sub ClearCoverageRate()
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
        Private Function DeleteSelectedCoverageRate(ByVal nIndex As Integer) As Boolean
            Dim bIsOk As Boolean = True
            Try
                If Me.State.IsNewWithCopy Then
                    If Me.State.moCoverageRateList Is Nothing Then Me.LoadCoverageRateList()
                    Me.State.moCoverageRateList(nIndex) = Nothing
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

        Private Sub PopulateCoverageRateList(Optional ByVal oAction As String = ACTION_NONE)
            'Dim oCoverageRates As CoverageRate
            Dim oCoverageRates As CommPlanDistribution
            Dim oDataView As DataView

            If Me.State.IsCoverageNew = True And Not Me.State.IsNewWithCopy Then
                Return ' We can not have CoverageRates if the coverage is new
            End If

            Try
                'If CoveragePricingCode = NO_COVERAGE_PRICING Then
                '    EnableCoveragePricing(False)
                'Else
                '    EnableCoveragePricing(True)
                'End If

                If Me.State.IsNewWithCopy Then
                    oDataView = oCoverageRates.getPlanList(Guid.Empty)
                    If Not oAction = ACTION_CANCEL_DELETE Then
                        Me.LoadCoverageRateList()
                    End If
                    If Not Me.State.moCoverageRateList Is Nothing Then
                        oDataView = getDVFromArray(Me.State.moCoverageRateList, oDataView.Table)
                    End If
                Else
                    oDataView = oCoverageRates.getPlanList(Me.State.moCommPlanId) 'TheCommPlanDist.Id)
                End If

                Select Case oAction
                    Case ACTION_NONE
                        Me.SetPageAndSelectedIndexFromGuid(oDataView, Guid.Empty, moGridView, 0)
                        EnableForEditRateButtons(False)
                    Case ACTION_SAVE
                        Me.SetPageAndSelectedIndexFromGuid(oDataView, GetGuidFromString(CoverageRateId), moGridView,
                                    moGridView.PageIndex)
                        EnableForEditRateButtons(False)
                    Case ACTION_CANCEL_DELETE
                        Me.SetPageAndSelectedIndexFromGuid(oDataView, Guid.Empty, moGridView,
                                    moGridView.PageIndex)
                        EnableForEditRateButtons(False)
                    Case ACTION_EDIT
                        If Me.State.IsNewWithCopy Then
                            CoverageRateId = Me.State.moCoverageRateList(moGridView.SelectedIndex).Id.ToString
                            Me.State.moCommPlanDistId = Me.State.moCoverageRateList(moGridView.SelectedIndex).Id
                        Else
                            CoverageRateId = Me.GetSelectedGridText(moGridView, COL_COMMISSION_PLAN_DIST_ID_IDX)
                            Me.State.moCommPlanDistId = GetGuidFromString(CoverageRateId) ' Me.State.moCoverageRateList(moGridView.SelectedIndex).Id

                            Me.State.MyBoDist = New CommPlanDistribution(Me.State.moCommPlanDistId)

                        End If
                        Me.SetPageAndSelectedIndexFromGuid(oDataView, GetGuidFromString(CoverageRateId), moGridView,
                                    moGridView.PageIndex, True)
                        EnableForEditRateButtons(True)
                    Case ACTION_NEW
                        If Me.State.IsNewWithCopy Then oDataView.Table.DefaultView.Sort() = Nothing ' Clear sort, so that the new line shows up at the bottom

                        'IsNewRate = True
                        Me.State.IsCommPlanDistNew = True
                        Me.State.MyBoDist = New CommPlanDistribution()
                        Dim oRow As DataRow = oDataView.Table.NewRow
                        With TheCommPlanDist
                            CoverageRateId = .Id.ToString
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

                        Me.State.moCommPlanDistId = GetGuidFromString(CoverageRateId)

                        Me.SetPageAndSelectedIndexFromGuid(oDataView, GetGuidFromString(CoverageRateId), moGridView,
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
            Dim oCoverageRate As CommPlanDistribution
            For Each oCoverageRate In oArray
                If Not oCoverageRate Is Nothing Then
                    oRow = oDtable.NewRow
                    oRow("COMMISSION_PLAN_ID") = oCoverageRate.CommissionPlanId.ToByteArray
                    oRow("PAYEE_TYPE_XCD") = oCoverageRate.PayeeTypeXcd
                    oRow("ENTITY_ID") = oCoverageRate.EntityId.ToByteArray
                    oRow("COMMISSION_AMOUNT") = oCoverageRate.CommissionAmount.Value
                    oRow("COMMISSION_PERCENTAGE") = oCoverageRate.CommissionPercent.Value
                    oRow("COMMISSIONS_SOURCE_XCD") = oCoverageRate.CommissionsPercentSourceXcd
                    oRow("POSITION") = oCoverageRate.Position.Value

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

        Private Sub PopulateCoverageRate()
            If Me.State.IsNewWithCopy Then
                With Me.State.moCoverageRateList(moGridView.SelectedIndex)
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
