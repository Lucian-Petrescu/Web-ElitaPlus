Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.DALObjects
Imports System.Globalization
Imports Assurant.ElitaPlus.Business
Imports Microsoft.Web.Services3.Referral

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
            'Public moCommEntity As CommissionEntity
            Public moCoverageRateList() As CommPlanDistribution
            Public moCommPlanDistList As CommPlanDistribution.CommPlanDistList
            Public moCommPlanDist As CommPlanDistribution
            Public moAssocComm As CommPlanDistribution
            'Public moAssocCommList As AssociateCommissions.AssocCommList
            'Public moAssocComm As AssociateCommissions            
            Public moIsAssocCommDirty As Boolean = False
            'Public moCommPeriodEntity As CommissionPeriodEntity
            'Public moCommissionPeriodId As Guid = Guid.Empty
            Public moCommissionToleranceId As Guid = Guid.Empty
            Public moCommPlanId As Guid = Guid.Empty
            Public moCommPlanDistId As Guid = Guid.Empty
            Public moInError As Boolean = False
            Public LastErrMsg As String
            'Public moTolerance As CommissionTolerance = Nothing
            Public IsCommPlanDistNew As Boolean = False
            Public IsToleranceNew As Boolean = False
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public boChanged As Boolean = False
            Public moAssocCommSearch As AssociateCommissions.SearchDV = Nothing
            Public moCommPlanDistSearch As CommPlanDistribution.SearchDV = Nothing
            Public searchDV As DataView = Nothing

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
            'Me.State.moCommissionPeriodId = CType(Me.CallingParameters, Guid)
            Me.State.moCommPlanId = CType(Me.CallingParameters, Guid)

            If Me.State.moCommPlanId.Equals(Guid.Empty) Then
                Me.State.IsCommPlanDistNew = True
                ClearPeriod()
                SetPeriodButtonsState(True)
                PopulatePeriod()
                PopulateCoverageRateList()
                'PopulateCoverageRate()
                TheDealerControl.ChangeEnabledControlProperty(True)
            Else
                Me.State.IsCommPlanDistNew = False
                SetPeriodButtonsState(False)
                PopulatePeriod()
                PopulateCoverageRateList()
                TheDealerControl.ChangeEnabledControlProperty(False)
            End If
            If Not TheDealerControl.SelectedGuid.Equals(Guid.Empty) Then
                ' ControlMgr.SetVisibleControl(Me, moRestrictDetailPanel2, GetRestrictMarkup())
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

        'Private moPeriod As CommissionPeriod
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
                        'Me.State.moCommissionPeriodId = Me.State.MyBo.Id
                        Me.State.moCommPlanId = Me.State.MyBo.Id
                    Else
                        ' For updating, deleting
                        'Me.State.MyBo = New CommPlan(Me.State.moCommissionPeriodId)
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
                        'If IsNewRate = True Then
                        ' For creating, inserting
                        Me.State.MyBoDist = New CommPlanDistribution
                        'Me.State.moCommissionPeriodId = Me.State.MyBo.Id
                        Me.State.moCommPlanDistId = Me.State.MyBoDist.Id
                        'CoverageRateId = Me.State.MyBoDist.Id.ToString
                        moCoverageRate = New CommPlanDistribution
                        CoverageRateId = moCoverageRate.Id.ToString
                    Else
                        ' For updating, deleting
                        'Me.State.MyBo = New CommPlan(Me.State.moCommissionPeriodId)
                        Me.State.MyBoDist = New CommPlanDistribution(Me.State.moCommPlanDistId)
                        If CoverageRateId = "" Then
                            CoverageRateId = Guid.Empty.ToString
                        End If

                        moCoverageRate = New CommPlanDistribution(Me.State.moCommPlanDistId)
                    End If
                End If
                'BindBoPropertiesToLabels(Me.State.MyBoDist)
                Return Me.State.MyBoDist
            End Get
        End Property

        'Private ReadOnly Property TheCoverageRate() As CommPlanDistribution
        '    Get
        '        If moCoverageRate Is Nothing Then
        '            'If IsNewRate = True Then
        '            ' For creating, inserting
        '            moCoverageRate = New CommPlanDistribution
        '            CoverageRateId = moCoverageRate.Id.ToString
        '        Else
        '            ' For updating, deleting
        '            If CoverageRateId = "" Then
        '                CoverageRateId = Guid.Empty.ToString
        '            End If

        '            moCoverageRate = New CommPlanDistribution(Me.State.moCommPlanDistId)
        '            'moCoverageRate = New CommPlanDistribution(GetGuidFromString(CoverageRateId))
        '            'moCoverageRate = New CommPlanDistribution(Me.State.moCommPlanDistId)
        '        End If

        '        Return moCoverageRate
        '    End Get

        'End Property

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
                If Not Me.State.MyBo Is Nothing Then
                    If (Me.State.MyBo.DealerId <> Guid.Empty) Then
                        Dim oDealer As New Dealer(Me.State.MyBo.DealerId)
                        Dim oCompany As New Company(oDealer.CompanyId)
                        If Not oCompany.AttributeValues Is Nothing Then
                            If oCompany.AttributeValues.Contains("NEW_COMMISSION_MODULE_CONFIGURED") Then
                                'If oCompany.AttributeValues.Value(Codes.NEW_COMMISSION_MODULE_CONFIGURED) = Codes.EXT_YESNO_Y Then
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
                    'Me.SetFormTitle(PAGETITLE)
                    Me.SetFormTab(PAGETAB)
                    'Me.TranslateGridHeader(Grid)
                    'Me.TranslateGridControls(Grid)
                    'Me.SetGridItemStyleColor(Me.Grid)
                    Me.SetGridItemStyleColor(moGridView)
                    Me.TranslateGridHeader(Me.moGridView)
                    Me.TranslateGridControls(moGridView)
                    Me.SetStateProperties()
                    'US-521672
                    Me.State.IsDealerConfiguredForSourceXcd = HasDealerConfigeredForSourceXcd()
                    Me.State.IsCompanyConfiguredForSourceXcd = HasCompanyConfigeredForSourceXcd()
                    If Not Me.State.IsCompanyConfiguredForSourceXcd Then
                        Me.moGridView.Visible = False
                    Else
                        Me.moGridView.Visible = True
                        'US-521672
                        SetGridSourceXcdLabelFromBo()
                    End If

                    'BindSourceOptionDropdownlist()
                    'ExecuteEvents()

                    Me.AddControlMsg(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO,
                                     Me.MSG_TYPE_CONFIRM, True)
                    Me.AddCalendar(Me.BtnEffectiveDate_WRITE, Me.moEffectiveText_WRITE)
                    Me.AddCalendar(Me.BtnExpirationDate_WRITE, Me.moExpirationText_WRITE)
                    'HideGridSourceXcdColumn()
                Else
                    Me.State.IsCompanyConfiguredForSourceXcd = HasCompanyConfigeredForSourceXcd()
                    If Not Me.State.IsCompanyConfiguredForSourceXcd Then
                        'Me.DisplayMessage(Message.MSG_COMPANY_NOT_CONFIGURED_FOR_DEALER, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO, Me.HiddenSaveChangesPromptResponse)
                        Me.moGridView.Visible = False
                    Else
                        Me.moGridView.Visible = True
                        'US-521672
                        SetGridSourceXcdLabelFromBo()
                        CheckIfComingFromConfirm()
                    End If
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)
        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub GoBack()
            'Dim retType As New CommissionPeriodSearchForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.moCommissionPeriodId, Me.State.boChanged)
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
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try
                ClearPeriod()
                PopulatePeriod()
            Catch ex As Exception
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
            'Me.State.moCommissionPeriodId = Guid.Empty
            Me.State.moCommPlanId = Guid.Empty
            Me.State.IsCommPlanDistNew = True
            ClearPeriod()
            SetPeriodButtonsState(True)
            PopulatePeriod()
            TheDealerControl.ChangeEnabledControlProperty(True)
            'ControlMgr.SetVisibleControl(Me, moRestrictDetailPanel2, True)
            'TheDealerControl.ChangeEnabledControlProperty(True)
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
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CreateNewCopy()
            Dim newObj As New CommPlan
            'Me.State.moCommissionPeriodId = Guid.Empty
            SetGridControls(moGridView, True)
            Me.State.MyBo = Nothing
            newObj.Copy(TheCommPlan)
            Me.State.IsCommPlanDistNew = True
            Me.State.MyBo = newObj

            EnableDateFields()
            SetPeriodButtonsState(True)
            ClearTolerance()
            'TheDealerControl.ChangeEnabledControlProperty(True)
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
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Handlers-DropDowns"


        Private Sub OnFromDrop_Changed(ByVal fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
           Handles multipleDropControl.SelectedDropChanged
            Try
                EnableDateFields()
                'If Me.State.IsCommPlanDistNew = True Then
                '    'PopulatePayeeType()
                'End If
                Me.State.IsCompanyConfiguredForSourceXcd = HasCompanyConfigeredForSourceXcd()
                If Me.State.IsCompanyConfiguredForSourceXcd Then
                    SetGridControls(moGridView, True)
                    FillSourceXcdDropdownList()
                    FillEntityDropDownList()
                    FillPayeeTypeDropDownList()
                    SetGridSourceXcdDropdownFromBo()
                    SetGridSourceXcdLabelFromBo()
                    'SetGridSourceXcdTextboxForNewCoverage()
                    Me.moGridView.Visible = False
                    BtnNewRate_WRITE.Visible = False
                Else
                    Me.DisplayMessage(Message.MSG_COMPANY_NOT_CONFIGURED_FOR_DEALER, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO, Me.HiddenSaveChangesPromptResponse)
                    Me.moGridView.Visible = False
                    BtnNewRate_WRITE.Visible = False
                End If
                'ControlMgr.SetVisibleControl(Me, moRestrictDetailPanel2, GetRestrictMarkup())
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
        'Private Sub moDealerDrop_WRITE_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moDealerDrop_WRITE.SelectedIndexChanged
        '    EnableDateFields()
        'End Sub
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
            'Me.ClearLabelErrSign(moComputeMethodLabel)
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
                If Me.State.IsCommPlanDistNew = False Then
                    PopulatePayeeType()
                End If
                PopulatePeriodEntity()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

#End Region

#Region "Client Attributes"

        'Private Sub ExecuteEvents()
        '    'cboPayeeType1.Attributes("onChange") = String.Format("return EnableControl('{0}','{1}');", cboPayeeType1.ClientID, cboPeriodEntity1.ClientID)
        '    'cboPayeeType2.Attributes("onChange") = String.Format("return EnableControl('{0}','{1}');", cboPayeeType2.ClientID, cboPeriodEntity2.ClientID)
        '    'cboPayeeType3.Attributes("onChange") = String.Format("return EnableControl('{0}','{1}');", cboPayeeType3.ClientID, cboPeriodEntity3.ClientID)
        '    'cboPayeeType4.Attributes("onChange") = String.Format("return EnableControl('{0}','{1}');", cboPayeeType4.ClientID, cboPeriodEntity4.ClientID)
        '    'cboPayeeType5.Attributes("onChange") = String.Format("return EnableControl('{0}','{1}');", cboPayeeType5.ClientID, cboPeriodEntity5.ClientID)
        'End Sub
#End Region

#Region "Clear"

        Private Sub ClearDealer()
            If Me.State.IsCommPlanDistNew = True Then
                'moDealerDrop_WRITE.SelectedIndex = 0
                TheDealerControl.SelectedIndex = 0
            Else
                'Me.SetSelectedItem(moDealerDrop_WRITE, TheCommPlan.DealerId)
                TheDealerControl.SelectedGuid = TheCommPlan.DealerId

            End If

        End Sub

        Private Sub ClearPeriod()
            ClearDealer()
            '        EnableDateFields()
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
                ' DropDowns
                '.DealerId = TheDealerControl.SelectedGuid 'Me.GetSelectedItem(moDealerDrop_WRITE)
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
            'Dim PayeeTypeCode As String
            'PayeeTypeCode = LookupListNew.GetCodeFromId(LookupListNew.LK_PAYEE_TYPE, Me.GetSelectedItem(cboPayeeType1))
            'If PayeeTypeCode = Payee_Type_Comm_Entity Then
            '    If Me.GetSelectedItem(cboPeriodEntity1).Equals(Guid.Empty) Then
            '        ElitaPlusPage.SetLabelError(Me.LabelCommEntity)
            '        ControlMgr.SetEnableControl(Me, cboPeriodEntity1, True)
            '        Me.cboPeriodEntity1.Focus()
            '        Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.MISSING_COMM_ENTITY_ERR)
            '    End If
            'End If
            'PayeeTypeCode = LookupListNew.GetCodeFromId(LookupListNew.LK_PAYEE_TYPE, Me.GetSelectedItem(cboPayeeType2))
            'If PayeeTypeCode = Payee_Type_Comm_Entity Then
            '    If Me.GetSelectedItem(cboPeriodEntity2).Equals(Guid.Empty) Then
            '        ElitaPlusPage.SetLabelError(Me.LabelCommEntity)
            '        ControlMgr.SetEnableControl(Me, cboPeriodEntity2, True)
            '        Me.cboPeriodEntity2.Focus()
            '        Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.MISSING_COMM_ENTITY_ERR)
            '    End If
            'End If
            'PayeeTypeCode = LookupListNew.GetCodeFromId(LookupListNew.LK_PAYEE_TYPE, Me.GetSelectedItem(cboPayeeType3))
            'If PayeeTypeCode = Payee_Type_Comm_Entity Then
            '    If Me.GetSelectedItem(cboPeriodEntity3).Equals(Guid.Empty) Then
            '        ElitaPlusPage.SetLabelError(Me.LabelCommEntity)
            '        ControlMgr.SetEnableControl(Me, cboPeriodEntity3, True)
            '        Me.cboPeriodEntity3.Focus()
            '        Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.MISSING_COMM_ENTITY_ERR)
            '    End If
            'End If
            'PayeeTypeCode = LookupListNew.GetCodeFromId(LookupListNew.LK_PAYEE_TYPE, Me.GetSelectedItem(cboPayeeType4))
            'If PayeeTypeCode = Payee_Type_Comm_Entity Then
            '    If Me.GetSelectedItem(cboPeriodEntity4).Equals(Guid.Empty) Then
            '        ElitaPlusPage.SetLabelError(Me.LabelCommEntity)
            '        ControlMgr.SetEnableControl(Me, cboPeriodEntity4, True)
            '        Me.cboPeriodEntity4.Focus()
            '        Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.MISSING_COMM_ENTITY_ERR)
            '    End If
            'End If
            'PayeeTypeCode = LookupListNew.GetCodeFromId(LookupListNew.LK_PAYEE_TYPE, Me.GetSelectedItem(cboPayeeType5))
            'If PayeeTypeCode = Payee_Type_Comm_Entity Then
            '    If Me.GetSelectedItem(cboPeriodEntity5).Equals(Guid.Empty) Then
            '        ElitaPlusPage.SetLabelError(Me.LabelCommEntity)
            '        ControlMgr.SetEnableControl(Me, cboPeriodEntity5, True)
            '        Me.cboPeriodEntity5.Focus()
            '        Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.MISSING_COMM_ENTITY_ERR)
            '    End If
            'End If

        End Sub

        Private Function ApplyPeriodChanges() As Boolean
            Dim bIsOk As Boolean = True
            Dim oPeriod As CommPlan

            Try
                'ValidatePayeeType()
                'SetLabelColor(Me.LabelCommEntity)
                'UpdateEntityTable()
                'SaveDistributionChanges()
                If IsDirtyPeriodBO() = True Then
                    If Not Me.State.moCommTolerance Is Nothing Then
                        PopulateToleranceBOFromForm()
                    End If
                    oPeriod = TheCommPlan
                    'BindBoPropertiesToLabels(Me.State.MyBo)
                    oPeriod.Save()
                    'Me.State.MyBo = Nothing
                    'oPeriod = TheCommPlan
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
            SaveDistributionChanges()
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

        Public Shared Sub SetLabelColor(ByVal lbl As Label)
            lbl.ForeColor = Color.Black
        End Sub
#End Region


#Region "Tolerance"

#Region "Tolerance Handlers"

#Region "Tolerance Handlers-Buttons"

        Private Sub NewTolerance()
            Me.State.moCommissionToleranceId = Guid.Empty
            Me.State.moCommTolerance = New CommissionTolerance
            Me.State.IsToleranceNew = True
            ControlMgr.SetVisibleControl(Me, moPeriodButtonPanel, False)
            'EnableDisablePeriodEntity(False)
            Me.EnableDisableControls(moPeriodPanel_WRITE, True)
            EnableToleranceGrid(False)
            InitializeFormTolerance()
            PopulateFormFromPeriodEntityBO()
            'SetGridSourceXcdLabelFromBo()
        End Sub

        Private Sub BtnNewGrid_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNewGrid_WRITE.Click
            Try
                If IsDirtyPeriodBO() = True Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM,
                                            Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Accept
                Else
                    'If GetRestrictMarkup() = False Then
                    'Exit Sub
                    'End If
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
                'EnableDisablePeriodEntity(True)
                PopulatePeriodEntity()
                TheDealerControl.ChangeEnabledControlProperty(False)
                'ControlMgr.SetEnableControl(Me, moComputeMethodDropDown, True)
                ControlMgr.SetEnableControl(Me, TextBoxCode, True)
                ControlMgr.SetEnableControl(Me, TextBoxDescription, True)
                'Me.EnableDisableControls(moPeriodPanel_WRITE, False)
                'SetGridSourceXcdLabelFromBo()
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
                    'Me.ClearGridHeaders(Me.Grid)
                    Me.State.IsToleranceNew = False
                    PopulateTolerance(Me.POPULATE_ACTION_NO_EDIT)
                    ControlMgr.SetVisibleControl(Me, moPeriodButtonPanel, True)
                    'EnableDisablePeriodEntity(True)
                    Me.EnableDisableControls(moPeriodPanel_WRITE, False)
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
            'EnableDisablePeriodEntity(False)
            Me.EnableDisableControls(moPeriodPanel_WRITE, True)
        End Sub

        Private Sub RowCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
            Try
                If (e.CommandName = Me.EDIT_COMMAND_NAME) Then
                    Dim index As Integer = CInt(e.CommandArgument)
                    Me.State.moCommissionToleranceId = Me.GetGuidFromString(
                                    Me.GetGridText(Me.Grid, index, COMMISSION_TOLERANCE_ID_COL))

                    'Me.State.moCommissionToleranceId = New Guid(Me.Grid.Rows(index).Cells(Me.COMMISSION_TOLERANCE_ID_COL).Text)

                    ControlMgr.SetVisibleControl(Me, btnEntityBack, True)
                    Me.State.moCommTolerance = New CommissionTolerance(Me.State.moCommissionToleranceId)
                    'If GetRestrictMarkup() = False Then
                    'Exit Sub
                    'End If
                    EditTolerance()
                    PopulatePeriodEntity()
                    'End If
                ElseIf (e.CommandName = Me.DELETE_COMMAND_NAME) Then
                    Dim index As Integer = CInt(e.CommandArgument)
                    'nIndex = e.Item.ItemIndex
                    Me.State.moCommissionToleranceId = Me.GetGuidFromString(
                                    Me.GetGridText(Me.Grid, index, COMMISSION_TOLERANCE_ID_COL))
                    'Me.State.moCommissionPeriodId = New Guid(Me.Grid.Rows(index).Cells(Me.COMMISSION_TOLERANCE_ID_COL).Text)

                    If DeleteTolerance() = True Then
                        PopulateTolerance(Me.POPULATE_ACTION_NO_EDIT)
                        PopulatePeriodEntity()
                        '  If Me.Grid.Rows.Count = 0 Then
                        'GoBack()
                        'End If
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
            'ControlMgr.SetVisibleControl(Me, moRestrictDetailPanel2, bIsReadWrite)
            'moAllowedMarkupPctDetailLabel.Visible = bIsReadWrite
            'moAllowedMarkupPctDetailText.Visible = bIsReadWrite
            'moToleranceDetailLabel.Visible = bIsReadWrite
            'moToleranceDetailText.Visible = bIsReadWrite
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
            'Dim oComTolerance As CommissionTolerance = TheComTolerance

            Me.BindBOPropertyToLabel(Me.State.moCommTolerance, ALLOWED_MARKUP_PCT_PROPERTY, Me.moAllowedMarkupPctDetailLabel)
            Me.BindBOPropertyToLabel(Me.State.moCommTolerance, TOLERANCE_PROPERTY, Me.moToleranceDetailLabel)
            Me.ClearGridHeadersAndLabelsErrSign()
        End Sub

        Protected Sub BindBoAssocCommToLabels(ByVal obj As CommPlanDistribution)
            Me.BindBOPropertyToLabel(obj, "CommissionAmount", Me.lblMarkup)
            Me.BindBOPropertyToLabel(obj, "CommissionPercent", Me.lblComm)
            'Me.BindBOPropertyToLabel(obj, MARKUP_TOTAL, Me.lblTotal)
            'Me.BindBOPropertyToLabel(obj, COMMISSION_TOTAL, Me.lblTotal)
            'Me.ClearGridHeadersAndLabelsErrSign()
        End Sub

        Private Sub PopulateToleranceFormItem(ByVal oControl As TextBox, ByVal oPropertyValue As Object)
            Me.PopulateControlFromBOProperty(oControl, oPropertyValue)
        End Sub

        Private Sub PopulateFormFromToleranceBO()
            'Dim oComTolerance As CommissionTolerance = TheComTolerance

            'Dim oAssocComm As AssociateCommissions
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
            'Dim nInitValue As New DecimalType(0)
            'Dim i As Integer
            'Dim oPeriodEntity As CommissionPeriodEntity
            'Dim oAssocCommission As AssociateCommissions
            'Dim oCommEntity As CommissionEntity

            'Dim oEntityView As DataView
            'Dim oDataView As AssociateCommissions.SearchDV = AssociateCommissions.getList(Me.State.moCommTolerance.Id)
            'Dim oMakupTotal As Decimal = 0
            'Dim oBrokerComm As Decimal = 0
            'Dim strPayeeType As String
            'Dim strPositioncheck As String = String.Empty
            'Dim diffValue As Decimal = 0.0000

            'If (Me.State.searchDV Is Nothing) Then
            '    Me.State.searchDV = oDataView
            'End If

            ''Me.State.moAssocCommSearch = AssociateCommissions.getList(TheComTolerance.Id)
            'If Me.State.IsToleranceNew Then
            '    For Each oPeriodEntity In Me.State.MyBo.AssociatedCommPeriodEntity
            '        Select Case oPeriodEntity.Position
            '            Case 1
            '                If Not oPeriodEntity.EntityId.Equals(Guid.Empty) Then
            '                    oCommEntity = New CommissionEntity(oPeriodEntity.EntityId)
            '                    PopulateToleranceFormItem(Me.txtBrokerMakupEntity, oCommEntity.EntityName)
            '                Else
            '                    strPayeeType = LookupListNew.GetDescriptionFromId(LookupListNew.LK_PAYEE_TYPE, oPeriodEntity.PayeeTypeId)
            '                    PopulateToleranceFormItem(Me.txtBrokerMakupEntity, strPayeeType)
            '                End If
            '                PopulateToleranceFormItem(Me.txtBrokerMakupPct, nInitValue)
            '                PopulateToleranceFormItem(Me.txtBrokerCommPct, nInitValue)

            '                If Me.cboBrokerCommPctSourceXcd.Visible Then
            '                    Me.txtBrokerMakupPct.Text = "0.00"
            '                    ControlMgr.SetEnableControl(Me, Me.txtBrokerMakupPct, False)
            '                End If
            '            Case 2
            '                If Not oPeriodEntity.EntityId.Equals(Guid.Empty) Then
            '                    oCommEntity = New CommissionEntity(oPeriodEntity.EntityId)
            '                    PopulateToleranceFormItem(Me.txtBrokerMakupEntity2, oCommEntity.EntityName)
            '                Else
            '                    strPayeeType = LookupListNew.GetDescriptionFromId(LookupListNew.LK_PAYEE_TYPE, oPeriodEntity.PayeeTypeId)
            '                    PopulateToleranceFormItem(Me.txtBrokerMakupEntity2, strPayeeType)
            '                End If
            '                PopulateToleranceFormItem(Me.txtBrokerMakupPct2, nInitValue)
            '                PopulateToleranceFormItem(Me.txtBrokerCommPct2, nInitValue)

            '                If Me.cboBrokerCommPct2SourceXcd.Visible Then
            '                    Me.txtBrokerMakupPct2.Text = "0.00"
            '                    ControlMgr.SetEnableControl(Me, Me.txtBrokerMakupPct2, False)
            '                End If
            '            Case 3
            '                If Not oPeriodEntity.EntityId.Equals(Guid.Empty) Then
            '                    oCommEntity = New CommissionEntity(oPeriodEntity.EntityId)
            '                    PopulateToleranceFormItem(Me.txtBrokerMakupEntity3, oCommEntity.EntityName)
            '                Else
            '                    strPayeeType = LookupListNew.GetDescriptionFromId(LookupListNew.LK_PAYEE_TYPE, oPeriodEntity.PayeeTypeId)
            '                    PopulateToleranceFormItem(Me.txtBrokerMakupEntity3, strPayeeType)
            '                End If

            '                PopulateToleranceFormItem(Me.txtBrokerMakupPct3, nInitValue)
            '                PopulateToleranceFormItem(Me.txtBrokerCommPct3, nInitValue)

            '                If Me.cboBrokerCommPct3SourceXcd.Visible Then
            '                    Me.txtBrokerMakupPct3.Text = "0.00"
            '                    ControlMgr.SetEnableControl(Me, Me.txtBrokerMakupPct3, False)
            '                End If
            '            Case 4
            '                If Not oPeriodEntity.EntityId.Equals(Guid.Empty) Then
            '                    oCommEntity = New CommissionEntity(oPeriodEntity.EntityId)
            '                    PopulateToleranceFormItem(Me.txtBrokerMakupEntity4, oCommEntity.EntityName)
            '                Else
            '                    strPayeeType = LookupListNew.GetDescriptionFromId(LookupListNew.LK_PAYEE_TYPE, oPeriodEntity.PayeeTypeId)
            '                    PopulateToleranceFormItem(Me.txtBrokerMakupEntity4, strPayeeType)
            '                End If
            '                PopulateToleranceFormItem(Me.txtBrokerMakupPct4, nInitValue)
            '                PopulateToleranceFormItem(Me.txtBrokerCommPct4, nInitValue)

            '                If Me.cboBrokerCommPct4SourceXcd.Visible Then
            '                    Me.txtBrokerMakupPct4.Text = "0.00"
            '                    ControlMgr.SetEnableControl(Me, Me.txtBrokerMakupPct4, False)
            '                End If
            '            Case 5
            '                If Not oPeriodEntity.EntityId.Equals(Guid.Empty) Then
            '                    oCommEntity = New CommissionEntity(oPeriodEntity.EntityId)
            '                    PopulateToleranceFormItem(Me.txtBrokerMakupEntity5, oCommEntity.EntityName)
            '                Else
            '                    strPayeeType = LookupListNew.GetDescriptionFromId(LookupListNew.LK_PAYEE_TYPE, oPeriodEntity.PayeeTypeId)
            '                    PopulateToleranceFormItem(Me.txtBrokerMakupEntity5, strPayeeType)
            '                End If
            '                PopulateToleranceFormItem(Me.txtBrokerMakupPct5, nInitValue)
            '                PopulateToleranceFormItem(Me.txtBrokerCommPct5, nInitValue)

            '                If Me.cboBrokerCommPct5SourceXcd.Visible Then
            '                    Me.txtBrokerMakupPct5.Text = "0.00"
            '                    ControlMgr.SetEnableControl(Me, Me.txtBrokerMakupPct5, False)
            '                End If
            '        End Select
            '    Next
            'Else
            '    For i = 0 To oDataView.Count - 1
            '        If (strPositioncheck.IndexOf(oDataView(i).Item(4).ToString) < 0) Then
            '            Select Case oDataView(i).Item(4).ToString
            '                Case "1"
            '                    strPositioncheck = strPositioncheck & ",1"
            '                    'REQ-5773
            '                    Me.txtBrokerMakupPct.Text = Me.GetAmountFormattedPercentString(oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_MARKUP_PERCENT))
            '                    Me.txtBrokerCommPct.Text = Me.GetAmountFormattedPercentString(oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMMISSION_PERCENT))
            '                    If oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMM_ENTITY_NAME).ToString <> "" Then
            '                        PopulateToleranceFormItem(Me.txtBrokerMakupEntity, oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMM_ENTITY_NAME))
            '                    Else
            '                        If Not oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_PAYEE_TYPE_ID) Is DBNull.Value Then
            '                            strPayeeType = LookupListNew.GetDescriptionFromId(LookupListNew.LK_PAYEE_TYPE, GuidControl.ByteArrayToGuid(oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_PAYEE_TYPE_ID)))
            '                            PopulateToleranceFormItem(Me.txtBrokerMakupEntity, strPayeeType)
            '                        End If
            '                    End If
            '                    PopulateToleranceFormItem(Me.AsCommId1, oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_ASSOCIATE_COMMISSIONS_ID))
            '                    oMakupTotal += CType(oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_MARKUP_PERCENT), Decimal)
            '                    oBrokerComm += CType(oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMMISSION_PERCENT), Decimal)

            '                    If Me.cboBrokerCommPctSourceXcd.Visible Then
            '                        oMakupTotal = 0.00
            '                        Me.txtBrokerMakupPct.Text = "0.00"
            '                        ControlMgr.SetEnableControl(Me, Me.txtBrokerMakupPct, False)

            '                        If Me.cboBrokerCommPctSourceXcd.Items.Count > 0 And Not oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMMISSION_PERCENT_SOURCE_XCD) Is Nothing Then
            '                            If Not oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMMISSION_PERCENT_SOURCE_XCD) Is DBNull.Value Then
            '                                Me.SetSelectedItem(Me.cboBrokerCommPctSourceXcd, oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMMISSION_PERCENT_SOURCE_XCD))
            '                            End If

            '                            If cboBrokerCommPctSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_COMMBRKDOWN_OPTION_DIFFERENCE) Then
            '                                Me.PopulateControlFromBOProperty(txtBrokerCommPct, diffValue, Me.PERCENT_FORMAT)
            '                                ControlMgr.SetEnableControl(Me, Me.txtBrokerCommPct, False)
            '                            Else
            '                                ControlMgr.SetEnableControl(Me, Me.txtBrokerCommPct, True)
            '                            End If
            '                        End If
            '                    End If
            '                Case "2"
            '                    strPositioncheck = strPositioncheck & ",2"
            '                    Me.txtBrokerMakupPct2.Text = Me.GetAmountFormattedPercentString(oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_MARKUP_PERCENT))
            '                    Me.txtBrokerCommPct2.Text = Me.GetAmountFormattedPercentString(oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMMISSION_PERCENT))
            '                    If oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMM_ENTITY_NAME).ToString <> "" Then
            '                        PopulateToleranceFormItem(Me.txtBrokerMakupEntity2, oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMM_ENTITY_NAME))
            '                    Else
            '                        If Not oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_PAYEE_TYPE_ID) Is DBNull.Value Then
            '                            strPayeeType = LookupListNew.GetDescriptionFromId(LookupListNew.LK_PAYEE_TYPE, GuidControl.ByteArrayToGuid(oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_PAYEE_TYPE_ID)))
            '                            PopulateToleranceFormItem(Me.txtBrokerMakupEntity2, strPayeeType)
            '                        End If
            '                    End If
            '                    PopulateToleranceFormItem(Me.AsCommId2, oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_ASSOCIATE_COMMISSIONS_ID))
            '                    oMakupTotal += CType(oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_MARKUP_PERCENT), Decimal)
            '                    oBrokerComm += CType(oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMMISSION_PERCENT), Decimal)

            '                    If Me.cboBrokerCommPct2SourceXcd.Visible Then
            '                        oMakupTotal = 0.00
            '                        Me.txtBrokerMakupPct2.Text = "0.00"
            '                        ControlMgr.SetEnableControl(Me, Me.txtBrokerMakupPct2, False)

            '                        If Me.cboBrokerCommPct2SourceXcd.Items.Count > 0 And Not oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMMISSION_PERCENT_SOURCE_XCD) Is Nothing Then
            '                            If Not oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMMISSION_PERCENT_SOURCE_XCD) Is DBNull.Value Then
            '                                Me.SetSelectedItem(Me.cboBrokerCommPct2SourceXcd, oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMMISSION_PERCENT_SOURCE_XCD))
            '                            End If

            '                            If cboBrokerCommPct2SourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_COMMBRKDOWN_OPTION_DIFFERENCE) Then
            '                                Me.PopulateControlFromBOProperty(txtBrokerCommPct2, diffValue, Me.PERCENT_FORMAT)
            '                                ControlMgr.SetEnableControl(Me, Me.txtBrokerCommPct2, False)
            '                            Else
            '                                ControlMgr.SetEnableControl(Me, Me.txtBrokerCommPct2, True)
            '                            End If
            '                        End If
            '                    End If
            '                Case "3"
            '                    strPositioncheck = strPositioncheck & ",3"
            '                    Me.txtBrokerMakupPct3.Text = Me.GetAmountFormattedPercentString(oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_MARKUP_PERCENT))
            '                    Me.txtBrokerCommPct3.Text = Me.GetAmountFormattedPercentString(oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMMISSION_PERCENT))
            '                    If oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMM_ENTITY_NAME).ToString <> "" Then
            '                        PopulateToleranceFormItem(Me.txtBrokerMakupEntity3, oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMM_ENTITY_NAME))
            '                    Else
            '                        If Not oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_PAYEE_TYPE_ID) Is DBNull.Value Then
            '                            strPayeeType = LookupListNew.GetDescriptionFromId(LookupListNew.LK_PAYEE_TYPE, GuidControl.ByteArrayToGuid(oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_PAYEE_TYPE_ID)))
            '                            PopulateToleranceFormItem(Me.txtBrokerMakupEntity3, strPayeeType)
            '                        End If
            '                    End If
            '                    PopulateToleranceFormItem(Me.AsCommId3, oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_ASSOCIATE_COMMISSIONS_ID))
            '                    oMakupTotal += CType(oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_MARKUP_PERCENT), Decimal)
            '                    oBrokerComm += CType(oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMMISSION_PERCENT), Decimal)

            '                    If Me.cboBrokerCommPct3SourceXcd.Visible Then
            '                        oMakupTotal = 0.00
            '                        Me.txtBrokerMakupPct3.Text = "0.00"
            '                        ControlMgr.SetEnableControl(Me, Me.txtBrokerMakupPct3, False)

            '                        If Me.cboBrokerCommPct3SourceXcd.Items.Count > 0 And Not oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMMISSION_PERCENT_SOURCE_XCD) Is Nothing Then
            '                            If Not oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMMISSION_PERCENT_SOURCE_XCD) Is DBNull.Value Then
            '                                Me.SetSelectedItem(Me.cboBrokerCommPct3SourceXcd, oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMMISSION_PERCENT_SOURCE_XCD))
            '                            End If

            '                            If cboBrokerCommPct3SourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_COMMBRKDOWN_OPTION_DIFFERENCE) Then
            '                                Me.PopulateControlFromBOProperty(txtBrokerCommPct3, diffValue, Me.PERCENT_FORMAT)
            '                                ControlMgr.SetEnableControl(Me, Me.txtBrokerCommPct3, False)
            '                            Else
            '                                ControlMgr.SetEnableControl(Me, Me.txtBrokerCommPct3, True)
            '                            End If
            '                        End If
            '                    End If
            '                Case "4"
            '                    strPositioncheck = strPositioncheck & ",4"
            '                    Me.txtBrokerMakupPct4.Text = Me.GetAmountFormattedPercentString(oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_MARKUP_PERCENT))
            '                    Me.txtBrokerCommPct4.Text = Me.GetAmountFormattedPercentString(oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMMISSION_PERCENT))
            '                    If oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMM_ENTITY_NAME).ToString <> "" Then
            '                        PopulateToleranceFormItem(Me.txtBrokerMakupEntity4, oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMM_ENTITY_NAME))
            '                    Else
            '                        If Not oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_PAYEE_TYPE_ID) Is DBNull.Value Then
            '                            strPayeeType = LookupListNew.GetDescriptionFromId(LookupListNew.LK_PAYEE_TYPE, GuidControl.ByteArrayToGuid(oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_PAYEE_TYPE_ID)))
            '                            PopulateToleranceFormItem(Me.txtBrokerMakupEntity4, strPayeeType)
            '                        End If
            '                    End If
            '                    PopulateToleranceFormItem(Me.AsCommId4, oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_ASSOCIATE_COMMISSIONS_ID))
            '                    oMakupTotal += CType(oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_MARKUP_PERCENT), Decimal)
            '                    oBrokerComm += CType(oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMMISSION_PERCENT), Decimal)

            '                    If Me.cboBrokerCommPct4SourceXcd.Visible Then
            '                        oMakupTotal = 0.00
            '                        Me.txtBrokerMakupPct4.Text = "0.00"
            '                        ControlMgr.SetEnableControl(Me, Me.txtBrokerMakupPct4, False)

            '                        If Me.cboBrokerCommPct4SourceXcd.Items.Count > 0 And Not oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMMISSION_PERCENT_SOURCE_XCD) Is Nothing Then
            '                            If Not oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMMISSION_PERCENT_SOURCE_XCD) Is DBNull.Value Then
            '                                Me.SetSelectedItem(Me.cboBrokerCommPct4SourceXcd, oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMMISSION_PERCENT_SOURCE_XCD))
            '                            End If

            '                            If cboBrokerCommPct4SourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_COMMBRKDOWN_OPTION_DIFFERENCE) Then
            '                                Me.PopulateControlFromBOProperty(txtBrokerCommPct4, diffValue, Me.PERCENT_FORMAT)
            '                                ControlMgr.SetEnableControl(Me, Me.txtBrokerCommPct4, False)
            '                            Else
            '                                ControlMgr.SetEnableControl(Me, Me.txtBrokerCommPct4, True)
            '                            End If
            '                        End If
            '                    End If
            '                Case "5"
            '                    strPositioncheck = strPositioncheck & ",5"
            '                    Me.txtBrokerMakupPct5.Text = Me.GetAmountFormattedPercentString(oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_MARKUP_PERCENT))
            '                    Me.txtBrokerCommPct5.Text = Me.GetAmountFormattedPercentString(oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMMISSION_PERCENT))
            '                    If oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMM_ENTITY_NAME).ToString <> "" Then
            '                        PopulateToleranceFormItem(Me.txtBrokerMakupEntity5, oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMM_ENTITY_NAME))
            '                    Else
            '                        If Not oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_PAYEE_TYPE_ID) Is DBNull.Value Then
            '                            strPayeeType = LookupListNew.GetDescriptionFromId(LookupListNew.LK_PAYEE_TYPE, GuidControl.ByteArrayToGuid(oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_PAYEE_TYPE_ID)))
            '                            PopulateToleranceFormItem(Me.txtBrokerMakupEntity5, strPayeeType)
            '                        End If
            '                    End If
            '                    PopulateToleranceFormItem(Me.AsCommId5, oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_ASSOCIATE_COMMISSIONS_ID))
            '                    oMakupTotal += CType(oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_MARKUP_PERCENT), Decimal)
            '                    oBrokerComm += CType(oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMMISSION_PERCENT), Decimal)

            '                    If Me.cboBrokerCommPct5SourceXcd.Visible Then
            '                        oMakupTotal = 0.00
            '                        Me.txtBrokerMakupPct5.Text = "0.00"
            '                        ControlMgr.SetEnableControl(Me, Me.txtBrokerMakupPct5, False)

            '                        If Me.cboBrokerCommPct5SourceXcd.Items.Count > 0 And Not oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMMISSION_PERCENT_SOURCE_XCD) Is Nothing Then
            '                            If Not oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMMISSION_PERCENT_SOURCE_XCD) Is DBNull.Value Then
            '                                Me.SetSelectedItem(Me.cboBrokerCommPct5SourceXcd, oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMMISSION_PERCENT_SOURCE_XCD))
            '                            End If

            '                            If cboBrokerCommPct5SourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_COMMBRKDOWN_OPTION_DIFFERENCE) Then
            '                                Me.PopulateControlFromBOProperty(txtBrokerCommPct5, diffValue, Me.PERCENT_FORMAT)
            '                                ControlMgr.SetEnableControl(Me, Me.txtBrokerCommPct5, False)
            '                            Else
            '                                ControlMgr.SetEnableControl(Me, Me.txtBrokerCommPct5, True)
            '                            End If
            '                        End If
            '                    End If
            '            End Select
            '        End If
            '    Next
            'End If

            ''Me.BindBOPropertyToLabel(oAssocCommission, MARKUP_TOTAL, lblTotal)
            ''Me.BindBOPropertyToLabel(oAssocCommission, COMMISSION_TOTAL, lblTotal)
            'PopulateToleranceFormItem(Me.txtCommPctTotal, oBrokerComm)
            'PopulateToleranceFormItem(Me.txtBrokerPctTotal, oMakupTotal)
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
            'Dim i As Integer
            'Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            'Dim dvPayeeType As DataView = LookupListNew.GetPayeeTypeLookupList(langId)
            'Dim dvPayeeType1 As DataView = dvPayeeType
            'Dim PayeeTypeList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="PYTYPE", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
            'Dim FilteredPayeeTypeList As DataElements.ListItem()
            'Dim FilteredPayeeType1List As DataElements.ListItem()
            'Dim yesId As Guid = (From lst In CommonConfigManager.Current.ListManager.GetList(listCode:="YESNO", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
            '                     Where lst.Code = "Y"
            '                     Select lst.ListItemId).FirstOrDefault()

            'Dim dealerId As Guid = TheDealerControl.SelectedGuid
            'Dim oDealer As New Dealer(dealerId)

            'If Not oDealer.DealerGroupId.Equals(Guid.Empty) Then
            '    Dim oDealerGrp As New DealerGroup(oDealer.DealerGroupId)
            '    'Dim lkYesNo As DataView = LookupListNew.DropdownLookupList("YESNO", Authentication.LangId)
            '    'lkYesNo.RowFilter += " and code='Y'"
            '    'Dim yesId As Guid = New Guid(CType(lkYesNo.Item(0).Item("ID"), Byte()))
            '    If oDealerGrp.AcctingByGroupId.Equals(yesId) Then
            '        'dvPayeeType.RowFilter += "and code <>'" & Payee_Type_Dealer & "'"
            '        FilteredPayeeTypeList = (From lst In PayeeTypeList
            '                                 Where lst.Code <> Payee_Type_Dealer
            '                                 Select lst).ToArray()
            '    Else
            '        'dvPayeeType.RowFilter += "and code <>'" & Payee_Type_Dealer_Group & "'"
            '        FilteredPayeeTypeList = (From lst In PayeeTypeList
            '                                 Where lst.Code <> Payee_Type_Dealer_Group
            '                                 Select lst).ToArray()
            '    End If
            'Else
            '    'dvPayeeType.RowFilter += "and code <>'" & Payee_Type_Dealer_Group & "'"
            '    FilteredPayeeTypeList = (From lst In PayeeTypeList
            '                             Where lst.Code <> Payee_Type_Dealer_Group
            '                             Select lst).ToArray()
            'End If

            ''Me.BindListControlToDataView(Me.cboPayeeType1, dvPayeeType, , , False)
            'Me.cboPayeeType1.Populate(FilteredPayeeTypeList, New PopulateOptions() With
            '{
            '    .AddBlankItem = False
            '})

            ''Me.BindListControlToDataView(Me.cboPayeeType2, dvPayeeType, , , False)
            'Me.cboPayeeType2.Populate(FilteredPayeeTypeList, New PopulateOptions() With
            '{
            '    .AddBlankItem = False
            '})

            ''Me.BindListControlToDataView(Me.cboPayeeType3, dvPayeeType, , , False)
            'Me.cboPayeeType3.Populate(FilteredPayeeTypeList, New PopulateOptions() With
            '{
            '    .AddBlankItem = False
            '})

            ''Me.BindListControlToDataView(Me.cboPayeeType4, dvPayeeType, , , False)
            'Me.cboPayeeType4.Populate(FilteredPayeeTypeList, New PopulateOptions() With
            '{
            '    .AddBlankItem = False
            '})

            ''Me.BindListControlToDataView(Me.cboPayeeType5, dvPayeeType, , , False)
            'Me.cboPayeeType5.Populate(FilteredPayeeTypeList, New PopulateOptions() With
            '{
            '    .AddBlankItem = False
            '})

            ''dvPayeeType1.RowFilter += "and code='" & Payee_Type_Comm_Entity & "'"
            'FilteredPayeeType1List = (From lst In PayeeTypeList
            '                          Where lst.Code = Payee_Type_Comm_Entity
            '                          Select lst).ToArray()

            ''If dvPayeeType1.Count = 1 Then
            ''    Me.litScriptVars.Text += "var commEntity = '" + GuidControl.ByteArrayToGuid(dvPayeeType1.Item(0)(LookupListNew.COL_ID_NAME)).ToString + "';"
            ''End If

            ''If FilteredPayeeType1List.Count = 1 Then
            ''    Me.litScriptVars.Text += "var commEntity = '" + FilteredPayeeType1List.First().ListItemId.ToString + "';"
            ''End If

            'If Me.State.IsCommPlanDistNew = True Then
            '    Dim FirstPayeeType As String
            '    FirstPayeeType = FilteredPayeeType1List.First().ListItemId.ToString()
            '    'BindSelectItem(GuidControl.ByteArrayToGuid(dvPayeeType1.Item(0)(LookupListNew.COL_ID_NAME)).ToString, cboPayeeType1)
            '    'BindSelectItem(GuidControl.ByteArrayToGuid(dvPayeeType1.Item(0)(LookupListNew.COL_ID_NAME)).ToString, cboPayeeType2)
            '    'BindSelectItem(GuidControl.ByteArrayToGuid(dvPayeeType1.Item(0)(LookupListNew.COL_ID_NAME)).ToString, cboPayeeType3)
            '    'BindSelectItem(GuidControl.ByteArrayToGuid(dvPayeeType1.Item(0)(LookupListNew.COL_ID_NAME)).ToString, cboPayeeType4)
            '    'BindSelectItem(GuidControl.ByteArrayToGuid(dvPayeeType1.Item(0)(LookupListNew.COL_ID_NAME)).ToString, cboPayeeType5)
            '    BindSelectItem(FirstPayeeType, cboPayeeType1)
            '    BindSelectItem(FirstPayeeType, cboPayeeType2)
            '    BindSelectItem(FirstPayeeType, cboPayeeType3)
            '    BindSelectItem(FirstPayeeType, cboPayeeType4)
            '    BindSelectItem(FirstPayeeType, cboPayeeType5)
            'End If
        End Sub
        Private Sub PopulatePeriodEntity()
            'Dim i As Integer
            'Dim oDataView As DataView
            'Dim oPeriodEntity As CommissionPeriodEntity
            'oDataView = LookupListNew.GetCommEntityLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

            'Dim listcontext As ListContext = New ListContext()
            'listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            'Dim CommEntityList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="CommEntityByCompanyGroup", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=listcontext)

            ''Me.BindListControlToDataView(cboPeriodEntity1, oDataView, , , True)
            'Me.cboPeriodEntity1.Populate(CommEntityList, New PopulateOptions() With
            '{
            '    .AddBlankItem = True
            '})

            ''Me.BindListControlToDataView(cboPeriodEntity2, oDataView, , , True)
            'Me.cboPeriodEntity2.Populate(CommEntityList, New PopulateOptions() With
            '{
            '    .AddBlankItem = True
            '})

            ''Me.BindListControlToDataView(cboPeriodEntity3, oDataView, , , True)
            'Me.cboPeriodEntity3.Populate(CommEntityList, New PopulateOptions() With
            '{
            '    .AddBlankItem = True
            '})

            ''Me.BindListControlToDataView(cboPeriodEntity4, oDataView, , , True)
            'Me.cboPeriodEntity4.Populate(CommEntityList, New PopulateOptions() With
            '{
            '    .AddBlankItem = True
            '})

            ''Me.BindListControlToDataView(cboPeriodEntity5, oDataView, , , True)
            'Me.cboPeriodEntity5.Populate(CommEntityList, New PopulateOptions() With
            '{
            '    .AddBlankItem = True
            '})

            'If Me.State.IsCommPlanDistNew = True Then
            '    'If oDataView.Count > 0 Then
            '    If CommEntityList.Count > 0 Then
            '        Me.State.MyBo.AttachPeriodEntity(Me.GetSelectedItem(cboPeriodEntity1), 1, Me.GetSelectedItem(cboPayeeType1))
            '        Me.State.MyBo.AttachPeriodEntity(Me.GetSelectedItem(cboPeriodEntity2), 2, Me.GetSelectedItem(cboPayeeType2))
            '        Me.State.MyBo.AttachPeriodEntity(Me.GetSelectedItem(cboPeriodEntity3), 3, Me.GetSelectedItem(cboPayeeType3))
            '        Me.State.MyBo.AttachPeriodEntity(Me.GetSelectedItem(cboPeriodEntity4), 4, Me.GetSelectedItem(cboPayeeType4))
            '        Me.State.MyBo.AttachPeriodEntity(Me.GetSelectedItem(cboPeriodEntity5), 5, Me.GetSelectedItem(cboPayeeType5))
            '    End If
            'Else
            '    Dim PayeeTypeCode As String
            'For Each oPeriodEntity As CommissionPeriodEntity In Me.State.MyBo.AssociatedCommPeriodEntity
            '    Select Case oPeriodEntity.Position
            '        Case 1
            '            BindSelectItem(oPeriodEntity.EntityId.ToString, cboPeriodEntity1)
            '            BindSelectItem(oPeriodEntity.PayeeTypeId.ToString, cboPayeeType1)

            '            PayeeTypeCode = LookupListNew.GetCodeFromId(LookupListNew.LK_PAYEE_TYPE, Me.GetSelectedItem(cboPayeeType1))
            '            If PayeeTypeCode <> Payee_Type_Comm_Entity Then
            '                ControlMgr.SetEnableControl(Me, cboPeriodEntity1, False)
            '            Else
            '                ControlMgr.SetEnableControl(Me, cboPeriodEntity1, True)
            '            End If
            '        Case 2
            '            BindSelectItem(oPeriodEntity.EntityId.ToString, cboPeriodEntity2)
            '            BindSelectItem(oPeriodEntity.PayeeTypeId.ToString, cboPayeeType2)
            '            PayeeTypeCode = LookupListNew.GetCodeFromId(LookupListNew.LK_PAYEE_TYPE, Me.GetSelectedItem(cboPayeeType2))
            '            If PayeeTypeCode <> Payee_Type_Comm_Entity Then
            '                ControlMgr.SetEnableControl(Me, cboPeriodEntity2, False)
            '            Else
            '                ControlMgr.SetEnableControl(Me, cboPeriodEntity2, True)
            '            End If
            '        Case 3
            '            BindSelectItem(oPeriodEntity.EntityId.ToString, cboPeriodEntity3)
            '            BindSelectItem(oPeriodEntity.PayeeTypeId.ToString, cboPayeeType3)
            '            PayeeTypeCode = LookupListNew.GetCodeFromId(LookupListNew.LK_PAYEE_TYPE, Me.GetSelectedItem(cboPayeeType3))
            '            If PayeeTypeCode <> Payee_Type_Comm_Entity Then
            '                ControlMgr.SetEnableControl(Me, cboPeriodEntity3, False)
            '            Else
            '                ControlMgr.SetEnableControl(Me, cboPeriodEntity3, True)
            '            End If
            '        Case 4
            '            BindSelectItem(oPeriodEntity.EntityId.ToString, cboPeriodEntity4)
            '            BindSelectItem(oPeriodEntity.PayeeTypeId.ToString, cboPayeeType4)
            '            PayeeTypeCode = LookupListNew.GetCodeFromId(LookupListNew.LK_PAYEE_TYPE, Me.GetSelectedItem(cboPayeeType4))
            '            If PayeeTypeCode <> Payee_Type_Comm_Entity Then
            '                ControlMgr.SetEnableControl(Me, cboPeriodEntity4, False)
            '            Else
            '                ControlMgr.SetEnableControl(Me, cboPeriodEntity4, True)
            '            End If
            '        Case 5
            '            BindSelectItem(oPeriodEntity.EntityId.ToString, cboPeriodEntity5)
            '            BindSelectItem(oPeriodEntity.PayeeTypeId.ToString, cboPayeeType5)
            '            PayeeTypeCode = LookupListNew.GetCodeFromId(LookupListNew.LK_PAYEE_TYPE, Me.GetSelectedItem(cboPayeeType5))
            '            If PayeeTypeCode <> Payee_Type_Comm_Entity Then
            '                ControlMgr.SetEnableControl(Me, cboPeriodEntity5, False)
            '            Else
            '                ControlMgr.SetEnableControl(Me, cboPeriodEntity5, True)
            '            End If
            '    End Select
            'Next
            'End If
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
            'If me.Grid.EditItemIndex = Me.NO_ITEM_SELECTED_INDEX Then Return False ' Grid is not in edit mode
            'Dim oComTolerance As CommissionTolerance
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
                '  BindBoPropertiesToGridHeaders(oTolerance)
                With Me.State.MyBo
                    PopulateToleranceBOFromForm()
                    bIsDirty = .IsDirty Or oComTolerance.IsDirty Or Me.State.moIsAssocCommDirty
                    .Save()
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
                bIsOk = False

                'SetSourceBucketValues()
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
                        'SetGridSourceXcdTextboxForNewCoverage()

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
            'Dim commPeriodEntity As CommissionPeriodEntity
            'For Each commPeriodEntity In Me.State.MyBo.AssociatedCommPeriodEntity
            '    Select Case commPeriodEntity.Position.ToString
            '        Case "1"
            '            If commPeriodEntity.EntityId <> Me.GetSelectedItem(cboPeriodEntity1) OrElse commPeriodEntity.PayeeTypeId <> Me.GetSelectedItem(cboPayeeType1) Then
            '                commPeriodEntity.EntityId = Me.GetSelectedItem(cboPeriodEntity1)
            '                commPeriodEntity.PayeeTypeId = Me.GetSelectedItem(cboPayeeType1)
            '            End If
            '        Case "2"
            '            If commPeriodEntity.EntityId <> Me.GetSelectedItem(cboPeriodEntity2) OrElse commPeriodEntity.PayeeTypeId <> Me.GetSelectedItem(cboPayeeType2) Then
            '                commPeriodEntity.EntityId = Me.GetSelectedItem(cboPeriodEntity2)
            '                commPeriodEntity.PayeeTypeId = Me.GetSelectedItem(cboPayeeType2)
            '            End If
            '        Case "3"
            '            If commPeriodEntity.EntityId <> Me.GetSelectedItem(cboPeriodEntity3) OrElse commPeriodEntity.PayeeTypeId <> Me.GetSelectedItem(cboPayeeType3) Then
            '                commPeriodEntity.EntityId = Me.GetSelectedItem(cboPeriodEntity3)
            '                commPeriodEntity.PayeeTypeId = Me.GetSelectedItem(cboPayeeType3)
            '            End If
            '        Case "4"
            '            If commPeriodEntity.EntityId <> Me.GetSelectedItem(cboPeriodEntity4) OrElse commPeriodEntity.PayeeTypeId <> Me.GetSelectedItem(cboPayeeType4) Then
            '                commPeriodEntity.EntityId = Me.GetSelectedItem(cboPeriodEntity4)
            '                commPeriodEntity.PayeeTypeId = Me.GetSelectedItem(cboPayeeType4)
            '            End If
            '        Case "5"
            '            If commPeriodEntity.EntityId <> Me.GetSelectedItem(cboPeriodEntity5) OrElse commPeriodEntity.PayeeTypeId <> Me.GetSelectedItem(cboPayeeType5) Then
            '                commPeriodEntity.EntityId = Me.GetSelectedItem(cboPeriodEntity5)
            '                commPeriodEntity.PayeeTypeId = Me.GetSelectedItem(cboPayeeType5)
            '            End If
            '    End Select
            'Next
        End Sub
        Private Sub PopulateAssocCommBOFromForm()
            'Dim oPeriodEntity As CommissionPeriodEntity
            'Dim oCommEntity As CommissionEntity
            'Dim oAssocComm As AssociateCommissions
            'Dim nInitValue As New DecimalType(0)
            'Dim totalMarkup As Decimal = 0
            'Dim totalComm As Decimal = 0
            'Dim oCount As Integer = 0
            'Dim strPayeeType As String
            ''Me.BindBOPropertyToLabel(oAssocComm, "MarkupPercent", Me.lblMarkup)
            'Dim strPositionChecked As String = String.Empty

            'For Each oPeriodEntity In Me.State.MyBo.AssociatedCommPeriodEntity
            '    oCount = oCount + 1
            '    If (strPositionChecked.IndexOf(oPeriodEntity.Position.ToString) < 0) Then
            '        Select Case oPeriodEntity.Position
            '            Case 1
            '                strPositionChecked = strPositionChecked & ",1"
            '                If Me.State.IsToleranceNew Then
            '                    If Not oPeriodEntity.EntityId.Equals(Guid.Empty) Then
            '                        oCommEntity = New CommissionEntity(oPeriodEntity.EntityId)
            '                        PopulateToleranceFormItem(Me.txtBrokerMakupEntity, oCommEntity.EntityName)
            '                    Else
            '                        strPayeeType = LookupListNew.GetDescriptionFromId(LookupListNew.LK_PAYEE_TYPE, oPeriodEntity.PayeeTypeId)
            '                        PopulateToleranceFormItem(Me.txtBrokerMakupEntity, strPayeeType)
            '                    End If
            '                    oAssocComm = Me.State.MyBo.AddAssocComm(Guid.Empty)
            '                    BindBoAssocCommToLabels(oAssocComm)
            '                    oAssocComm.CommissionToleranceId = Me.State.moCommissionToleranceId
            '                    Me.PopulateBOProperty(oAssocComm, "MarkupPercent", Me.txtBrokerMakupPct)
            '                    Me.PopulateBOProperty(oAssocComm, "CommissionPercent", Me.txtBrokerCommPct)
            '                    oAssocComm.Position = 1
            '                    If Me.txtBrokerMakupPct.Text <> "" Then
            '                        totalMarkup += oAssocComm.MarkupPercent.Value
            '                    End If
            '                    If Me.txtBrokerCommPct.Text <> "" Then
            '                        totalComm += oAssocComm.CommissionPercent.Value
            '                    End If
            '                    Me.PopulateBOProperty(oAssocComm, COMMISSION_TOTAL, totalComm.ToString)
            '                    Me.PopulateBOProperty(oAssocComm, MARKUP_TOTAL, totalMarkup.ToString)
            '                    'oAssocComm.Validate()
            '                    If Me.State.IsDealerConfiguredForSourceXcd Then
            '                        Me.txtBrokerMakupPct.Text = "0.00"
            '                        ControlMgr.SetEnableControl(Me, txtBrokerMakupPct, False)
            '                        If Me.cboBrokerCommPctSourceXcd.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
            '                            Me.PopulateBOProperty(oAssocComm, "CommissionsPercentSourceXcd", Me.cboBrokerCommPctSourceXcd, False, True)
            '                        End If
            '                    End If
            '                Else
            '                    If Me.AsCommId1.Text <> "" Then
            '                        oAssocComm = Me.State.MyBo.AddAssocComm(New Guid(Me.AsCommId1.Text))
            '                        BindBoAssocCommToLabels(oAssocComm)
            '                        Me.PopulateBOProperty(oAssocComm, "MarkupPercent", Me.txtBrokerMakupPct)

            '                        If Me.State.IsDealerConfiguredForSourceXcd Then
            '                            Me.txtBrokerMakupPct.Text = "0.00"
            '                            ControlMgr.SetEnableControl(Me, txtBrokerMakupPct, False)

            '                            If String.IsNullOrWhiteSpace(Me.txtBrokerCommPct.Text) Then
            '                                Me.txtBrokerCommPct.Text = "0.00"
            '                                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "onchange", "UpdateBroker();", False)
            '                            End If

            '                            Me.PopulateBOProperty(oAssocComm, "CommissionPercent", Me.txtBrokerCommPct)

            '                            If Me.cboBrokerCommPctSourceXcd.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
            '                                If cboBrokerCommPctSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_COMMBRKDOWN_OPTION_DIFFERENCE) Then
            '                                    ControlMgr.SetEnableControl(Me, txtBrokerCommPct, False)
            '                                Else
            '                                    ControlMgr.SetEnableControl(Me, txtBrokerCommPct, True)
            '                                End If

            '                                Me.PopulateBOProperty(oAssocComm, "CommissionsPercentSourceXcd", Me.cboBrokerCommPctSourceXcd, False, True)
            '                            End If
            '                        Else
            '                            Me.PopulateBOProperty(oAssocComm, "CommissionPercent", Me.txtBrokerCommPct)
            '                        End If

            '                        totalMarkup += oAssocComm.MarkupPercent.Value
            '                        totalComm += oAssocComm.CommissionPercent.Value
            '                        Me.PopulateBOProperty(oAssocComm, COMMISSION_TOTAL, totalComm.ToString)
            '                        Me.PopulateBOProperty(oAssocComm, MARKUP_TOTAL, totalMarkup.ToString)

            '                        'oAssocComm.Validate()
            '                    Else
            '                        Me.ChangeControlEnabledProperty(Me.txtBrokerMakupPct, False)
            '                        Me.ChangeControlEnabledProperty(Me.txtBrokerCommPct, False)
            '                    End If
            '                End If
            '            Case 2
            '                strPositionChecked = strPositionChecked & ",2"
            '                If Me.State.IsToleranceNew Then
            '                    If Not oPeriodEntity.EntityId.Equals(Guid.Empty) Then
            '                        oCommEntity = New CommissionEntity(oPeriodEntity.EntityId)
            '                        PopulateToleranceFormItem(Me.txtBrokerMakupEntity2, oCommEntity.EntityName)
            '                    Else
            '                        strPayeeType = LookupListNew.GetDescriptionFromId(LookupListNew.LK_PAYEE_TYPE, oPeriodEntity.PayeeTypeId)
            '                        PopulateToleranceFormItem(Me.txtBrokerMakupEntity2, strPayeeType)
            '                    End If
            '                    oAssocComm = Me.State.MyBo.AddAssocComm(Guid.Empty)
            '                    BindBoAssocCommToLabels(oAssocComm)
            '                    oAssocComm.CommissionToleranceId = Me.State.moCommissionToleranceId
            '                    Me.PopulateBOProperty(oAssocComm, "MarkupPercent", Me.txtBrokerMakupPct2)
            '                    Me.PopulateBOProperty(oAssocComm, "CommissionPercent", Me.txtBrokerCommPct2)
            '                    oAssocComm.Position = 2
            '                    If Me.txtBrokerMakupPct2.Text <> "" Then
            '                        totalMarkup += oAssocComm.MarkupPercent.Value
            '                    End If
            '                    If Me.txtBrokerCommPct2.Text <> "" Then
            '                        totalComm += oAssocComm.CommissionPercent.Value
            '                    End If
            '                    Me.PopulateBOProperty(oAssocComm, COMMISSION_TOTAL, totalComm.ToString)
            '                    Me.PopulateBOProperty(oAssocComm, MARKUP_TOTAL, totalMarkup.ToString)
            '                    'oAssocComm.Validate()
            '                    If Me.State.IsDealerConfiguredForSourceXcd Then
            '                        Me.txtBrokerMakupPct2.Text = "0.00"
            '                        ControlMgr.SetEnableControl(Me, txtBrokerMakupPct2, False)

            '                        If Me.cboBrokerCommPct2SourceXcd.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
            '                            Me.PopulateBOProperty(oAssocComm, "CommissionsPercentSourceXcd", Me.cboBrokerCommPct2SourceXcd, False, True)
            '                        End If
            '                    End If
            '                Else
            '                    If Me.AsCommId2.Text <> "" Then
            '                        oAssocComm = Me.State.MyBo.AddAssocComm(New Guid(Me.AsCommId2.Text))
            '                        BindBoAssocCommToLabels(oAssocComm)
            '                        Me.PopulateBOProperty(oAssocComm, "MarkupPercent", Me.txtBrokerMakupPct2)

            '                        If Me.State.IsDealerConfiguredForSourceXcd Then
            '                            Me.txtBrokerMakupPct2.Text = "0.00"
            '                            ControlMgr.SetEnableControl(Me, txtBrokerMakupPct2, False)

            '                            If String.IsNullOrWhiteSpace(Me.txtBrokerCommPct2.Text) Then
            '                                Me.txtBrokerCommPct2.Text = "0.0000"
            '                            End If

            '                            Me.PopulateBOProperty(oAssocComm, "CommissionPercent", Me.txtBrokerCommPct2)

            '                            If Me.cboBrokerCommPct2SourceXcd.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
            '                                If cboBrokerCommPct2SourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_COMMBRKDOWN_OPTION_DIFFERENCE) Then
            '                                    ControlMgr.SetEnableControl(Me, txtBrokerCommPct2, False)
            '                                Else
            '                                    ControlMgr.SetEnableControl(Me, txtBrokerCommPct2, True)
            '                                End If

            '                                Me.PopulateBOProperty(oAssocComm, "CommissionsPercentSourceXcd", Me.cboBrokerCommPct2SourceXcd, False, True)
            '                            End If
            '                        Else
            '                            Me.PopulateBOProperty(oAssocComm, "CommissionPercent", Me.txtBrokerCommPct2)
            '                        End If

            '                        totalMarkup += oAssocComm.MarkupPercent.Value
            '                        totalComm += oAssocComm.CommissionPercent.Value
            '                        Me.PopulateBOProperty(oAssocComm, COMMISSION_TOTAL, totalComm.ToString)
            '                        Me.PopulateBOProperty(oAssocComm, MARKUP_TOTAL, totalMarkup.ToString)
            '                        'oAssocComm.Validate()                                    
            '                    Else
            '                        Me.ChangeControlEnabledProperty(Me.txtBrokerMakupPct2, False)
            '                        Me.ChangeControlEnabledProperty(Me.txtBrokerCommPct2, False)
            '                    End If

            '                End If
            '            Case 3
            '                strPositionChecked = strPositionChecked & ",3"
            '                If Me.State.IsToleranceNew Then
            '                    If Not oPeriodEntity.EntityId.Equals(Guid.Empty) Then
            '                        oCommEntity = New CommissionEntity(oPeriodEntity.EntityId)
            '                        PopulateToleranceFormItem(Me.txtBrokerMakupEntity3, oCommEntity.EntityName)
            '                    Else
            '                        strPayeeType = LookupListNew.GetDescriptionFromId(LookupListNew.LK_PAYEE_TYPE, oPeriodEntity.PayeeTypeId)
            '                        PopulateToleranceFormItem(Me.txtBrokerMakupEntity3, strPayeeType)
            '                    End If
            '                    oAssocComm = Me.State.MyBo.AddAssocComm(Guid.Empty)
            '                    BindBoAssocCommToLabels(oAssocComm)
            '                    oAssocComm.CommissionToleranceId = Me.State.moCommissionToleranceId
            '                    Me.PopulateBOProperty(oAssocComm, "MarkupPercent", Me.txtBrokerMakupPct3)
            '                    Me.PopulateBOProperty(oAssocComm, "CommissionPercent", Me.txtBrokerCommPct3)
            '                    oAssocComm.Position = 3
            '                    If Me.txtBrokerMakupPct3.Text <> "" Then
            '                        totalMarkup += oAssocComm.MarkupPercent.Value
            '                    End If
            '                    If Me.txtBrokerCommPct3.Text <> "" Then
            '                        totalComm += oAssocComm.CommissionPercent.Value
            '                    End If
            '                    Me.PopulateBOProperty(oAssocComm, COMMISSION_TOTAL, totalComm.ToString)
            '                    Me.PopulateBOProperty(oAssocComm, MARKUP_TOTAL, totalMarkup.ToString)
            '                    'oAssocComm.Validate()
            '                    If Me.State.IsDealerConfiguredForSourceXcd Then
            '                        Me.txtBrokerMakupPct3.Text = "0.00"
            '                        ControlMgr.SetEnableControl(Me, txtBrokerMakupPct3, False)

            '                        If Me.cboBrokerCommPct3SourceXcd.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
            '                            Me.PopulateBOProperty(oAssocComm, "CommissionsPercentSourceXcd", Me.cboBrokerCommPct3SourceXcd, False, True)
            '                        End If
            '                    End If
            '                Else
            '                    If Me.AsCommId3.Text <> "" Then
            '                        oAssocComm = Me.State.MyBo.AddAssocComm(New Guid(Me.AsCommId3.Text))
            '                        BindBoAssocCommToLabels(oAssocComm)
            '                        Me.PopulateBOProperty(oAssocComm, "MarkupPercent", Me.txtBrokerMakupPct3)

            '                        If Me.State.IsDealerConfiguredForSourceXcd Then
            '                            Me.txtBrokerMakupPct3.Text = "0.00"
            '                            ControlMgr.SetEnableControl(Me, txtBrokerMakupPct3, False)

            '                            If String.IsNullOrWhiteSpace(Me.txtBrokerCommPct3.Text) Then
            '                                Me.txtBrokerCommPct3.Text = "0.0000"
            '                            End If

            '                            Me.PopulateBOProperty(oAssocComm, "CommissionPercent", Me.txtBrokerCommPct3)

            '                            If Me.cboBrokerCommPct3SourceXcd.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
            '                                If cboBrokerCommPct3SourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_COMMBRKDOWN_OPTION_DIFFERENCE) Then
            '                                    ControlMgr.SetEnableControl(Me, txtBrokerCommPct3, False)
            '                                Else
            '                                    ControlMgr.SetEnableControl(Me, txtBrokerCommPct3, True)
            '                                End If

            '                                Me.PopulateBOProperty(oAssocComm, "CommissionsPercentSourceXcd", Me.cboBrokerCommPct3SourceXcd, False, True)
            '                            End If
            '                        Else
            '                            Me.PopulateBOProperty(oAssocComm, "CommissionPercent", Me.txtBrokerCommPct3)
            '                        End If

            '                        totalMarkup += oAssocComm.MarkupPercent.Value
            '                        totalComm += oAssocComm.CommissionPercent.Value
            '                        Me.PopulateBOProperty(oAssocComm, COMMISSION_TOTAL, totalComm.ToString)
            '                        Me.PopulateBOProperty(oAssocComm, MARKUP_TOTAL, totalMarkup.ToString)
            '                        'oAssocComm.Validate()
            '                    Else
            '                        Me.ChangeControlEnabledProperty(Me.txtBrokerMakupPct3, False)
            '                        Me.ChangeControlEnabledProperty(Me.txtBrokerCommPct3, False)
            '                    End If
            '                End If
            '            Case 4
            '                strPositionChecked = strPositionChecked & ",4"
            '                If Me.State.IsToleranceNew Then
            '                    If Not oPeriodEntity.EntityId.Equals(Guid.Empty) Then
            '                        oCommEntity = New CommissionEntity(oPeriodEntity.EntityId)
            '                        PopulateToleranceFormItem(Me.txtBrokerMakupEntity4, oCommEntity.EntityName)
            '                    Else
            '                        strPayeeType = LookupListNew.GetDescriptionFromId(LookupListNew.LK_PAYEE_TYPE, oPeriodEntity.PayeeTypeId)
            '                        PopulateToleranceFormItem(Me.txtBrokerMakupEntity4, strPayeeType)
            '                    End If
            '                    oAssocComm = Me.State.MyBo.AddAssocComm(Guid.Empty)
            '                    BindBoAssocCommToLabels(oAssocComm)
            '                    oAssocComm.CommissionToleranceId = Me.State.moCommissionToleranceId
            '                    Me.PopulateBOProperty(oAssocComm, "MarkupPercent", Me.txtBrokerMakupPct4)
            '                    Me.PopulateBOProperty(oAssocComm, "CommissionPercent", Me.txtBrokerCommPct4)
            '                    oAssocComm.Position = 4
            '                    If Me.txtBrokerMakupPct4.Text <> "" Then
            '                        totalMarkup += oAssocComm.MarkupPercent.Value
            '                    End If
            '                    If Me.txtBrokerCommPct4.Text <> "" Then
            '                        totalComm += oAssocComm.CommissionPercent.Value
            '                    End If
            '                    Me.PopulateBOProperty(oAssocComm, COMMISSION_TOTAL, totalComm.ToString)
            '                    Me.PopulateBOProperty(oAssocComm, MARKUP_TOTAL, totalMarkup.ToString)
            '                    'oAssocComm.Validate()
            '                    If Me.State.IsDealerConfiguredForSourceXcd Then
            '                        Me.txtBrokerMakupPct4.Text = "0.00"
            '                        ControlMgr.SetEnableControl(Me, txtBrokerMakupPct4, False)

            '                        If Me.cboBrokerCommPct4SourceXcd.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
            '                            Me.PopulateBOProperty(oAssocComm, "CommissionsPercentSourceXcd", Me.cboBrokerCommPct4SourceXcd, False, True)
            '                        End If
            '                    End If
            '                Else
            '                    If Me.AsCommId4.Text <> "" Then
            '                        oAssocComm = Me.State.MyBo.AddAssocComm(New Guid(Me.AsCommId4.Text))
            '                        BindBoAssocCommToLabels(oAssocComm)
            '                        Me.PopulateBOProperty(oAssocComm, "MarkupPercent", Me.txtBrokerMakupPct4)

            '                        If Me.State.IsDealerConfiguredForSourceXcd Then
            '                            Me.txtBrokerMakupPct4.Text = "0.00"
            '                            ControlMgr.SetEnableControl(Me, txtBrokerMakupPct4, False)

            '                            If String.IsNullOrWhiteSpace(Me.txtBrokerCommPct4.Text) Then
            '                                Me.txtBrokerCommPct4.Text = "0.0000"
            '                            End If

            '                            Me.PopulateBOProperty(oAssocComm, "CommissionPercent", Me.txtBrokerCommPct4)

            '                            If Me.cboBrokerCommPct4SourceXcd.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
            '                                If cboBrokerCommPct4SourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_COMMBRKDOWN_OPTION_DIFFERENCE) Then
            '                                    ControlMgr.SetEnableControl(Me, txtBrokerCommPct4, False)
            '                                Else
            '                                    ControlMgr.SetEnableControl(Me, txtBrokerCommPct4, True)
            '                                End If

            '                                Me.PopulateBOProperty(oAssocComm, "CommissionsPercentSourceXcd", Me.cboBrokerCommPct4SourceXcd, False, True)
            '                            End If
            '                        Else
            '                            Me.PopulateBOProperty(oAssocComm, "CommissionPercent", Me.txtBrokerCommPct4)
            '                        End If

            '                        totalMarkup += oAssocComm.MarkupPercent.Value
            '                        totalComm += oAssocComm.CommissionPercent.Value
            '                        Me.PopulateBOProperty(oAssocComm, COMMISSION_TOTAL, totalComm.ToString)
            '                        Me.PopulateBOProperty(oAssocComm, MARKUP_TOTAL, totalMarkup.ToString)
            '                        'oAssocComm.Validate()                                    
            '                    Else
            '                        Me.ChangeControlEnabledProperty(Me.txtBrokerMakupPct4, False)
            '                        Me.ChangeControlEnabledProperty(Me.txtBrokerCommPct4, False)
            '                    End If
            '                End If
            '            Case 5
            '                strPositionChecked = strPositionChecked & ",5"
            '                If Me.State.IsToleranceNew Then
            '                    If Not oPeriodEntity.EntityId.Equals(Guid.Empty) Then
            '                        oCommEntity = New CommissionEntity(oPeriodEntity.EntityId)
            '                        PopulateToleranceFormItem(Me.txtBrokerMakupEntity5, oCommEntity.EntityName)
            '                    Else
            '                        strPayeeType = LookupListNew.GetDescriptionFromId(LookupListNew.LK_PAYEE_TYPE, oPeriodEntity.PayeeTypeId)
            '                        PopulateToleranceFormItem(Me.txtBrokerMakupEntity5, strPayeeType)
            '                    End If
            '                    oAssocComm = Me.State.MyBo.AddAssocComm(Guid.Empty)
            '                    BindBoAssocCommToLabels(oAssocComm)
            '                    oAssocComm.CommissionToleranceId = Me.State.moCommissionToleranceId
            '                    Me.PopulateBOProperty(oAssocComm, "MarkupPercent", Me.txtBrokerMakupPct5)
            '                    Me.PopulateBOProperty(oAssocComm, "CommissionPercent", Me.txtBrokerCommPct5)
            '                    oAssocComm.Position = 5
            '                    If Me.txtBrokerMakupPct5.Text <> "" Then
            '                        totalMarkup += oAssocComm.MarkupPercent.Value
            '                    End If
            '                    If Me.txtBrokerCommPct5.Text <> "" Then
            '                        totalComm += oAssocComm.CommissionPercent.Value
            '                    End If
            '                    Me.PopulateBOProperty(oAssocComm, COMMISSION_TOTAL, totalComm.ToString)
            '                    Me.PopulateBOProperty(oAssocComm, MARKUP_TOTAL, totalMarkup.ToString)
            '                    'oAssocComm.Validate()
            '                    If Me.State.IsDealerConfiguredForSourceXcd Then
            '                        Me.txtBrokerMakupPct5.Text = "0.00"
            '                        ControlMgr.SetEnableControl(Me, txtBrokerMakupPct5, False)

            '                        If Me.cboBrokerCommPct5SourceXcd.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
            '                            Me.PopulateBOProperty(oAssocComm, "CommissionsPercentSourceXcd", Me.cboBrokerCommPct5SourceXcd, False, True)
            '                        End If
            '                    End If
            '                Else
            '                    If Me.AsCommId5.Text <> "" Then
            '                        oAssocComm = Me.State.MyBo.AddAssocComm(New Guid(Me.AsCommId5.Text))
            '                        BindBoAssocCommToLabels(oAssocComm)
            '                        Me.PopulateBOProperty(oAssocComm, "MarkupPercent", Me.txtBrokerMakupPct5)

            '                        If Me.State.IsDealerConfiguredForSourceXcd Then
            '                            Me.txtBrokerMakupPct5.Text = "0.00"
            '                            ControlMgr.SetEnableControl(Me, txtBrokerMakupPct5, False)

            '                            If String.IsNullOrWhiteSpace(Me.txtBrokerCommPct5.Text) Then
            '                                Me.txtBrokerCommPct5.Text = "0.0000"
            '                            End If

            '                            Me.PopulateBOProperty(oAssocComm, "CommissionPercent", Me.txtBrokerCommPct5)

            '                            If Me.cboBrokerCommPct5SourceXcd.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
            '                                If cboBrokerCommPct5SourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_COMMBRKDOWN_OPTION_DIFFERENCE) Then
            '                                    ControlMgr.SetEnableControl(Me, txtBrokerCommPct5, False)
            '                                Else
            '                                    ControlMgr.SetEnableControl(Me, txtBrokerCommPct5, True)
            '                                End If

            '                                Me.PopulateBOProperty(oAssocComm, "CommissionsPercentSourceXcd", Me.cboBrokerCommPct5SourceXcd, False, True)
            '                            End If
            '                        Else
            '                            Me.PopulateBOProperty(oAssocComm, "CommissionPercent", Me.txtBrokerCommPct5)
            '                        End If

            '                        totalMarkup += oAssocComm.MarkupPercent.Value
            '                        totalComm += oAssocComm.CommissionPercent.Value
            '                        Me.PopulateBOProperty(oAssocComm, COMMISSION_TOTAL, totalComm.ToString)
            '                        Me.PopulateBOProperty(oAssocComm, MARKUP_TOTAL, totalMarkup.ToString)
            '                        'oAssocComm.Validate()                                    
            '                    Else
            '                        Me.ChangeControlEnabledProperty(Me.txtBrokerMakupPct5, False)
            '                        Me.ChangeControlEnabledProperty(Me.txtBrokerCommPct5, False)
            '                    End If
            '                End If
            '        End Select
            '    End If
            '    If Me.ErrCollection.Count > 0 Then
            '        Throw New PopulateBOErrorException
            '    ElseIf oCount > 4 Then
            '        PopulateToleranceFormItem(Me.txtCommPctTotal, totalComm)
            '        PopulateToleranceFormItem(Me.txtBrokerPctTotal, totalMarkup)
            '        If Not oAssocComm Is Nothing Then
            '            Me.State.moAssocComm = oAssocComm
            '            If Not Me.HasDealerConfigeredForSourceXcd Then
            '                oAssocComm.Validate()
            '            End If
            '        End If
            '    End If
            '    If Not oAssocComm Is Nothing Then
            '        Me.State.moIsAssocCommDirty = Me.State.moIsAssocCommDirty Or oAssocComm.IsDirty
            '    End If
            'Next
            'ValidateDiffSourceLogic()
        End Sub

        Private Sub PopulateCommPlanDistBOFromForm()
            'Dim oPeriodEntity As CommissionPeriodEntity
            'Dim oCommEntity As CommissionEntity
            ''Dim oAssocComm As AssociateCommissions
            'Dim oAssocComm As CommPlanDistribution
            'Dim nInitValue As New DecimalType(0)
            'Dim totalMarkup As Decimal = 0
            'Dim totalComm As Decimal = 0
            'Dim oCount As Integer = 0
            'Dim strPayeeType As String
            ''Me.BindBOPropertyToLabel(oAssocComm, "MarkupPercent", Me.lblMarkup)
            'Dim strPositionChecked As String = String.Empty

            ''For Each oPeriodEntity In Me.State.moCommPlanDistList 'Me.State.MyBo.AssociatedCommPeriodEntity
            ''For Each oAssocComm In Me.State.moCommPlanDist.CommPlanDistList 'Me.State.MyBo.AssociatedCommPeriodEntity
            'oCount = oCount + 1
            ''If (strPositionChecked.IndexOf(oPeriodEntity.Position.ToString) < 0) Then    
            ''If (strPositionChecked.IndexOf(oAssocComm.Position.ToString) < 0) Then
            ''Select Case oPeriodEntity.Position
            ''   Case 1
            'strPositionChecked = strPositionChecked & ",1"
            'If Me.State.IsToleranceNew Then
            '    'If Not oPeriodEntity.EntityId.Equals(Guid.Empty) Then
            '    '    oCommEntity = New CommissionEntity(oPeriodEntity.EntityId)
            '    '    PopulateToleranceFormItem(Me.txtBrokerMakupEntity, oCommEntity.EntityName)
            '    'Else
            '    '    strPayeeType = LookupListNew.GetDescriptionFromId(LookupListNew.LK_PAYEE_TYPE, oPeriodEntity.PayeeTypeId)
            '    '    PopulateToleranceFormItem(Me.txtBrokerMakupEntity, strPayeeType)
            '    'End If
            '    oAssocComm = Me.State.MyBo.AddAssocComm(Guid.Empty)
            '    BindBoAssocCommToLabels(oAssocComm)
            '    'oAssocComm.CommissionToleranceId = Me.State.moCommissionToleranceId
            '    oAssocComm.CommissionPlanId = Me.State.moCommPlanId 'Me.State.moCommissionToleranceId 
            '    Me.PopulateBOProperty(oAssocComm, "CommissionAmount", Me.txtBrokerMakupPct)
            '    Me.PopulateBOProperty(oAssocComm, "CommissionPercent", Me.txtBrokerCommPct)
            '    oAssocComm.Position = 1
            '    If Me.txtBrokerMakupPct.Text <> "" Then
            '        totalMarkup += oAssocComm.CommissionAmount.Value
            '    End If
            '    If Me.txtBrokerCommPct.Text <> "" Then
            '        totalComm += oAssocComm.CommissionPercent.Value
            '    End If
            '    Me.PopulateBOProperty(oAssocComm, COMMISSION_TOTAL, totalComm.ToString)
            '    Me.PopulateBOProperty(oAssocComm, MARKUP_TOTAL, totalMarkup.ToString)
            '    'oAssocComm.Validate()
            '    If Me.State.IsDealerConfiguredForSourceXcd Then
            '        Me.txtBrokerMakupPct.Text = "0.00"
            '        ControlMgr.SetEnableControl(Me, txtBrokerMakupPct, False)
            '        If Me.cboBrokerCommPctSourceXcd.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
            '            Me.PopulateBOProperty(oAssocComm, "CommissionsPercentSourceXcd", Me.cboBrokerCommPctSourceXcd, False, True)
            '        End If
            '    End If
            'Else
            '    If Me.AsCommId1.Text <> "" Then
            '        oAssocComm = Me.State.MyBo.AddAssocComm(New Guid(Me.AsCommId1.Text))
            '        BindBoAssocCommToLabels(oAssocComm)
            '        Me.PopulateBOProperty(oAssocComm, "CommissionAmount", Me.txtBrokerMakupPct)

            '        If Me.State.IsDealerConfiguredForSourceXcd Then
            '            Me.txtBrokerMakupPct.Text = "0.00"
            '            ControlMgr.SetEnableControl(Me, txtBrokerMakupPct, False)

            '            If String.IsNullOrWhiteSpace(Me.txtBrokerCommPct.Text) Then
            '                Me.txtBrokerCommPct.Text = "0.00"
            '                'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "onchange", "UpdateBroker();", False)
            '            End If

            '            Me.PopulateBOProperty(oAssocComm, "CommissionPercent", Me.txtBrokerCommPct)

            '            If Me.cboBrokerCommPctSourceXcd.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
            '                If cboBrokerCommPctSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_COMMBRKDOWN_OPTION_DIFFERENCE) Then
            '                    ControlMgr.SetEnableControl(Me, txtBrokerCommPct, False)
            '                Else
            '                    ControlMgr.SetEnableControl(Me, txtBrokerCommPct, True)
            '                End If

            '                Me.PopulateBOProperty(oAssocComm, "CommissionsPercentSourceXcd", Me.cboBrokerCommPctSourceXcd, False, True)
            '            End If
            '        Else
            '            Me.PopulateBOProperty(oAssocComm, "CommissionPercent", Me.txtBrokerCommPct)
            '        End If

            '        totalMarkup += oAssocComm.CommissionAmount.Value
            '        totalComm += oAssocComm.CommissionPercent.Value
            '        Me.PopulateBOProperty(oAssocComm, COMMISSION_TOTAL, totalComm.ToString)
            '        Me.PopulateBOProperty(oAssocComm, MARKUP_TOTAL, totalMarkup.ToString)

            '        'oAssocComm.Validate()
            '    Else
            '        Me.ChangeControlEnabledProperty(Me.txtBrokerMakupPct, False)
            '        Me.ChangeControlEnabledProperty(Me.txtBrokerCommPct, False)
            '    End If
            'End If
            ''   Case 2
            'strPositionChecked = strPositionChecked & ",2"
            'If Me.State.IsToleranceNew Then
            '    'If Not oPeriodEntity.EntityId.Equals(Guid.Empty) Then
            '    '    oCommEntity = New CommissionEntity(oPeriodEntity.EntityId)
            '    '    PopulateToleranceFormItem(Me.txtBrokerMakupEntity2, oCommEntity.EntityName)
            '    'Else
            '    '    strPayeeType = LookupListNew.GetDescriptionFromId(LookupListNew.LK_PAYEE_TYPE, oPeriodEntity.PayeeTypeId)
            '    '    PopulateToleranceFormItem(Me.txtBrokerMakupEntity2, strPayeeType)
            '    'End If
            '    oAssocComm = Me.State.MyBo.AddAssocComm(Guid.Empty)
            '    BindBoAssocCommToLabels(oAssocComm)
            '    oAssocComm.CommissionPlanId = Me.State.moCommPlanId 'Me.State.moCommissionToleranceId
            '    Me.PopulateBOProperty(oAssocComm, "CommissionAmount", Me.txtBrokerMakupPct2)
            '    Me.PopulateBOProperty(oAssocComm, "CommissionPercent", Me.txtBrokerCommPct2)
            '    oAssocComm.Position = 2
            '    If Me.txtBrokerMakupPct2.Text <> "" Then
            '        totalMarkup += oAssocComm.CommissionAmount.Value
            '    End If
            '    If Me.txtBrokerCommPct2.Text <> "" Then
            '        totalComm += oAssocComm.CommissionPercent.Value
            '    End If
            '    Me.PopulateBOProperty(oAssocComm, COMMISSION_TOTAL, totalComm.ToString)
            '    Me.PopulateBOProperty(oAssocComm, MARKUP_TOTAL, totalMarkup.ToString)
            '    'oAssocComm.Validate()
            '    If Me.State.IsDealerConfiguredForSourceXcd Then
            '        Me.txtBrokerMakupPct2.Text = "0.00"
            '        ControlMgr.SetEnableControl(Me, txtBrokerMakupPct2, False)

            '        If Me.cboBrokerCommPct2SourceXcd.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
            '            Me.PopulateBOProperty(oAssocComm, "CommissionsPercentSourceXcd", Me.cboBrokerCommPct2SourceXcd, False, True)
            '        End If
            '    End If
            'Else
            '    If Me.AsCommId2.Text <> "" Then
            '        oAssocComm = Me.State.MyBo.AddAssocComm(New Guid(Me.AsCommId2.Text))
            '        BindBoAssocCommToLabels(oAssocComm)
            '        Me.PopulateBOProperty(oAssocComm, "CommissionAmount", Me.txtBrokerMakupPct2)

            '        If Me.State.IsDealerConfiguredForSourceXcd Then
            '            Me.txtBrokerMakupPct2.Text = "0.00"
            '            ControlMgr.SetEnableControl(Me, txtBrokerMakupPct2, False)

            '            If String.IsNullOrWhiteSpace(Me.txtBrokerCommPct2.Text) Then
            '                Me.txtBrokerCommPct2.Text = "0.0000"
            '            End If

            '            Me.PopulateBOProperty(oAssocComm, "CommissionPercent", Me.txtBrokerCommPct2)

            '            If Me.cboBrokerCommPct2SourceXcd.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
            '                If cboBrokerCommPct2SourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_COMMBRKDOWN_OPTION_DIFFERENCE) Then
            '                    ControlMgr.SetEnableControl(Me, txtBrokerCommPct2, False)
            '                Else
            '                    ControlMgr.SetEnableControl(Me, txtBrokerCommPct2, True)
            '                End If

            '                Me.PopulateBOProperty(oAssocComm, "CommissionsPercentSourceXcd", Me.cboBrokerCommPct2SourceXcd, False, True)
            '            End If
            '        Else
            '            Me.PopulateBOProperty(oAssocComm, "CommissionPercent", Me.txtBrokerCommPct2)
            '        End If

            '        totalMarkup += oAssocComm.CommissionAmount.Value
            '        totalComm += oAssocComm.CommissionPercent.Value
            '        Me.PopulateBOProperty(oAssocComm, COMMISSION_TOTAL, totalComm.ToString)
            '        Me.PopulateBOProperty(oAssocComm, MARKUP_TOTAL, totalMarkup.ToString)
            '        'oAssocComm.Validate()                                    
            '    Else
            '        Me.ChangeControlEnabledProperty(Me.txtBrokerMakupPct2, False)
            '        Me.ChangeControlEnabledProperty(Me.txtBrokerCommPct2, False)
            '    End If

            'End If
            ''   Case 3
            'strPositionChecked = strPositionChecked & ",3"
            'If Me.State.IsToleranceNew Then
            '    'If Not oPeriodEntity.EntityId.Equals(Guid.Empty) Then
            '    '    oCommEntity = New CommissionEntity(oPeriodEntity.EntityId)
            '    '    PopulateToleranceFormItem(Me.txtBrokerMakupEntity3, oCommEntity.EntityName)
            '    'Else
            '    '    strPayeeType = LookupListNew.GetDescriptionFromId(LookupListNew.LK_PAYEE_TYPE, oPeriodEntity.PayeeTypeId)
            '    '    PopulateToleranceFormItem(Me.txtBrokerMakupEntity3, strPayeeType)
            '    'End If
            '    oAssocComm = Me.State.MyBo.AddAssocComm(Guid.Empty)
            '    BindBoAssocCommToLabels(oAssocComm)
            '    oAssocComm.CommissionPlanId = Me.State.moCommPlanId 'Me.State.moCommissionToleranceId
            '    Me.PopulateBOProperty(oAssocComm, "CommissionAmount", Me.txtBrokerMakupPct3)
            '    Me.PopulateBOProperty(oAssocComm, "CommissionPercent", Me.txtBrokerCommPct3)
            '    oAssocComm.Position = 3
            '    If Me.txtBrokerMakupPct3.Text <> "" Then
            '        totalMarkup += oAssocComm.CommissionAmount.Value
            '    End If
            '    If Me.txtBrokerCommPct3.Text <> "" Then
            '        totalComm += oAssocComm.CommissionPercent.Value
            '    End If
            '    Me.PopulateBOProperty(oAssocComm, COMMISSION_TOTAL, totalComm.ToString)
            '    Me.PopulateBOProperty(oAssocComm, MARKUP_TOTAL, totalMarkup.ToString)
            '    'oAssocComm.Validate()
            '    If Me.State.IsDealerConfiguredForSourceXcd Then
            '        Me.txtBrokerMakupPct3.Text = "0.00"
            '        ControlMgr.SetEnableControl(Me, txtBrokerMakupPct3, False)

            '        If Me.cboBrokerCommPct3SourceXcd.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
            '            Me.PopulateBOProperty(oAssocComm, "CommissionsPercentSourceXcd", Me.cboBrokerCommPct3SourceXcd, False, True)
            '        End If
            '    End If
            'Else
            '    If Me.AsCommId3.Text <> "" Then
            '        oAssocComm = Me.State.MyBo.AddAssocComm(New Guid(Me.AsCommId3.Text))
            '        BindBoAssocCommToLabels(oAssocComm)
            '        Me.PopulateBOProperty(oAssocComm, "CommissionAmount", Me.txtBrokerMakupPct3)

            '        If Me.State.IsDealerConfiguredForSourceXcd Then
            '            Me.txtBrokerMakupPct3.Text = "0.00"
            '            ControlMgr.SetEnableControl(Me, txtBrokerMakupPct3, False)

            '            If String.IsNullOrWhiteSpace(Me.txtBrokerCommPct3.Text) Then
            '                Me.txtBrokerCommPct3.Text = "0.0000"
            '            End If

            '            Me.PopulateBOProperty(oAssocComm, "CommissionPercent", Me.txtBrokerCommPct3)

            '            If Me.cboBrokerCommPct3SourceXcd.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
            '                If cboBrokerCommPct3SourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_COMMBRKDOWN_OPTION_DIFFERENCE) Then
            '                    ControlMgr.SetEnableControl(Me, txtBrokerCommPct3, False)
            '                Else
            '                    ControlMgr.SetEnableControl(Me, txtBrokerCommPct3, True)
            '                End If

            '                Me.PopulateBOProperty(oAssocComm, "CommissionsPercentSourceXcd", Me.cboBrokerCommPct3SourceXcd, False, True)
            '            End If
            '        Else
            '            Me.PopulateBOProperty(oAssocComm, "CommissionPercent", Me.txtBrokerCommPct3)
            '        End If

            '        totalMarkup += oAssocComm.CommissionAmount.Value
            '        totalComm += oAssocComm.CommissionPercent.Value
            '        Me.PopulateBOProperty(oAssocComm, COMMISSION_TOTAL, totalComm.ToString)
            '        Me.PopulateBOProperty(oAssocComm, MARKUP_TOTAL, totalMarkup.ToString)
            '        'oAssocComm.Validate()
            '    Else
            '        Me.ChangeControlEnabledProperty(Me.txtBrokerMakupPct3, False)
            '        Me.ChangeControlEnabledProperty(Me.txtBrokerCommPct3, False)
            '    End If
            'End If
            ''   Case 4
            'strPositionChecked = strPositionChecked & ",4"
            'If Me.State.IsToleranceNew Then
            '    'If Not oPeriodEntity.EntityId.Equals(Guid.Empty) Then
            '    '    oCommEntity = New CommissionEntity(oPeriodEntity.EntityId)
            '    '    PopulateToleranceFormItem(Me.txtBrokerMakupEntity4, oCommEntity.EntityName)
            '    'Else
            '    '    strPayeeType = LookupListNew.GetDescriptionFromId(LookupListNew.LK_PAYEE_TYPE, oPeriodEntity.PayeeTypeId)
            '    '    PopulateToleranceFormItem(Me.txtBrokerMakupEntity4, strPayeeType)
            '    'End If
            '    oAssocComm = Me.State.MyBo.AddAssocComm(Guid.Empty)
            '    BindBoAssocCommToLabels(oAssocComm)
            '    oAssocComm.CommissionPlanId = Me.State.moCommPlanId 'Me.State.moCommissionToleranceId
            '    Me.PopulateBOProperty(oAssocComm, "CommissionAmount", Me.txtBrokerMakupPct4)
            '    Me.PopulateBOProperty(oAssocComm, "CommissionPercent", Me.txtBrokerCommPct4)
            '    oAssocComm.Position = 4
            '    If Me.txtBrokerMakupPct4.Text <> "" Then
            '        totalMarkup += oAssocComm.CommissionAmount.Value
            '    End If
            '    If Me.txtBrokerCommPct4.Text <> "" Then
            '        totalComm += oAssocComm.CommissionPercent.Value
            '    End If
            '    Me.PopulateBOProperty(oAssocComm, COMMISSION_TOTAL, totalComm.ToString)
            '    Me.PopulateBOProperty(oAssocComm, MARKUP_TOTAL, totalMarkup.ToString)
            '    'oAssocComm.Validate()
            '    If Me.State.IsDealerConfiguredForSourceXcd Then
            '        Me.txtBrokerMakupPct4.Text = "0.00"
            '        ControlMgr.SetEnableControl(Me, txtBrokerMakupPct4, False)

            '        If Me.cboBrokerCommPct4SourceXcd.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
            '            Me.PopulateBOProperty(oAssocComm, "CommissionsPercentSourceXcd", Me.cboBrokerCommPct4SourceXcd, False, True)
            '        End If
            '    End If
            'Else
            '    If Me.AsCommId4.Text <> "" Then
            '        oAssocComm = Me.State.MyBo.AddAssocComm(New Guid(Me.AsCommId4.Text))
            '        BindBoAssocCommToLabels(oAssocComm)
            '        Me.PopulateBOProperty(oAssocComm, "CommissionAmount", Me.txtBrokerMakupPct4)

            '        If Me.State.IsDealerConfiguredForSourceXcd Then
            '            Me.txtBrokerMakupPct4.Text = "0.00"
            '            ControlMgr.SetEnableControl(Me, txtBrokerMakupPct4, False)

            '            If String.IsNullOrWhiteSpace(Me.txtBrokerCommPct4.Text) Then
            '                Me.txtBrokerCommPct4.Text = "0.0000"
            '            End If

            '            Me.PopulateBOProperty(oAssocComm, "CommissionPercent", Me.txtBrokerCommPct4)

            '            If Me.cboBrokerCommPct4SourceXcd.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
            '                If cboBrokerCommPct4SourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_COMMBRKDOWN_OPTION_DIFFERENCE) Then
            '                    ControlMgr.SetEnableControl(Me, txtBrokerCommPct4, False)
            '                Else
            '                    ControlMgr.SetEnableControl(Me, txtBrokerCommPct4, True)
            '                End If

            '                Me.PopulateBOProperty(oAssocComm, "CommissionsPercentSourceXcd", Me.cboBrokerCommPct4SourceXcd, False, True)
            '            End If
            '        Else
            '            Me.PopulateBOProperty(oAssocComm, "CommissionPercent", Me.txtBrokerCommPct4)
            '        End If

            '        totalMarkup += oAssocComm.CommissionAmount.Value
            '        totalComm += oAssocComm.CommissionPercent.Value
            '        Me.PopulateBOProperty(oAssocComm, COMMISSION_TOTAL, totalComm.ToString)
            '        Me.PopulateBOProperty(oAssocComm, MARKUP_TOTAL, totalMarkup.ToString)
            '        'oAssocComm.Validate()                                    
            '    Else
            '        Me.ChangeControlEnabledProperty(Me.txtBrokerMakupPct4, False)
            '        Me.ChangeControlEnabledProperty(Me.txtBrokerCommPct4, False)
            '    End If
            'End If
            ''   Case 5
            'strPositionChecked = strPositionChecked & ",5"
            'If Me.State.IsToleranceNew Then
            '    'If Not oPeriodEntity.EntityId.Equals(Guid.Empty) Then
            '    '    oCommEntity = New CommissionEntity(oPeriodEntity.EntityId)
            '    '    PopulateToleranceFormItem(Me.txtBrokerMakupEntity5, oCommEntity.EntityName)
            '    'Else
            '    '    strPayeeType = LookupListNew.GetDescriptionFromId(LookupListNew.LK_PAYEE_TYPE, oPeriodEntity.PayeeTypeId)
            '    '    PopulateToleranceFormItem(Me.txtBrokerMakupEntity5, strPayeeType)
            '    'End If
            '    oAssocComm = Me.State.MyBo.AddAssocComm(Guid.Empty)
            '    BindBoAssocCommToLabels(oAssocComm)
            '    oAssocComm.CommissionPlanId = Me.State.moCommPlanId 'Me.State.moCommissionToleranceId
            '    Me.PopulateBOProperty(oAssocComm, "CommissionAmount", Me.txtBrokerMakupPct5)
            '    Me.PopulateBOProperty(oAssocComm, "CommissionPercent", Me.txtBrokerCommPct5)
            '    oAssocComm.Position = 5
            '    If Me.txtBrokerMakupPct5.Text <> "" Then
            '        totalMarkup += oAssocComm.CommissionAmount.Value
            '    End If
            '    If Me.txtBrokerCommPct5.Text <> "" Then
            '        totalComm += oAssocComm.CommissionPercent.Value
            '    End If
            '    Me.PopulateBOProperty(oAssocComm, COMMISSION_TOTAL, totalComm.ToString)
            '    Me.PopulateBOProperty(oAssocComm, MARKUP_TOTAL, totalMarkup.ToString)
            '    'oAssocComm.Validate()
            '    If Me.State.IsDealerConfiguredForSourceXcd Then
            '        Me.txtBrokerMakupPct5.Text = "0.00"
            '        ControlMgr.SetEnableControl(Me, txtBrokerMakupPct5, False)

            '        If Me.cboBrokerCommPct5SourceXcd.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
            '            Me.PopulateBOProperty(oAssocComm, "CommissionsPercentSourceXcd", Me.cboBrokerCommPct5SourceXcd, False, True)
            '        End If
            '    End If
            'Else
            '    If Me.AsCommId5.Text <> "" Then
            '        oAssocComm = Me.State.MyBo.AddAssocComm(New Guid(Me.AsCommId5.Text))
            '        BindBoAssocCommToLabels(oAssocComm)
            '        Me.PopulateBOProperty(oAssocComm, "CommissionAmount", Me.txtBrokerMakupPct5)

            '        If Me.State.IsDealerConfiguredForSourceXcd Then
            '            Me.txtBrokerMakupPct5.Text = "0.00"
            '            ControlMgr.SetEnableControl(Me, txtBrokerMakupPct5, False)

            '            If String.IsNullOrWhiteSpace(Me.txtBrokerCommPct5.Text) Then
            '                Me.txtBrokerCommPct5.Text = "0.0000"
            '            End If

            '            Me.PopulateBOProperty(oAssocComm, "CommissionPercent", Me.txtBrokerCommPct5)

            '            If Me.cboBrokerCommPct5SourceXcd.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
            '                If cboBrokerCommPct5SourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_COMMBRKDOWN_OPTION_DIFFERENCE) Then
            '                    ControlMgr.SetEnableControl(Me, txtBrokerCommPct5, False)
            '                Else
            '                    ControlMgr.SetEnableControl(Me, txtBrokerCommPct5, True)
            '                End If

            '                Me.PopulateBOProperty(oAssocComm, "CommissionsPercentSourceXcd", Me.cboBrokerCommPct5SourceXcd, False, True)
            '            End If
            '        Else
            '            Me.PopulateBOProperty(oAssocComm, "CommissionPercent", Me.txtBrokerCommPct5)
            '        End If

            '        totalMarkup += oAssocComm.CommissionAmount.Value
            '        totalComm += oAssocComm.CommissionPercent.Value
            '        Me.PopulateBOProperty(oAssocComm, COMMISSION_TOTAL, totalComm.ToString)
            '        Me.PopulateBOProperty(oAssocComm, MARKUP_TOTAL, totalMarkup.ToString)
            '        'oAssocComm.Validate()                                    
            '    Else
            '        Me.ChangeControlEnabledProperty(Me.txtBrokerMakupPct5, False)
            '        Me.ChangeControlEnabledProperty(Me.txtBrokerCommPct5, False)
            '    End If
            'End If
            ''End Select
            ''End If
            'If Me.ErrCollection.Count > 0 Then
            '    Throw New PopulateBOErrorException
            'ElseIf oCount > 4 Then
            '    PopulateToleranceFormItem(Me.txtCommPctTotal, totalComm)
            '    PopulateToleranceFormItem(Me.txtBrokerPctTotal, totalMarkup)
            '    If Not oAssocComm Is Nothing Then
            '        Me.State.moAssocComm = oAssocComm
            '        If Not Me.HasDealerConfigeredForSourceXcd Then
            '            oAssocComm.Validate()
            '        End If
            '    End If
            'End If
            'If Not oAssocComm Is Nothing Then
            '    Me.State.moIsAssocCommDirty = Me.State.moIsAssocCommDirty Or oAssocComm.IsDirty
            'End If
            ''Next
            'ValidateDiffSourceLogic()
        End Sub

        'Private Sub EnableDisablePeriodEntity(ByVal oEnable As Boolean)

        '    'ControlMgr.SetVisibleControl(Me, btnEntitySave, oEnable)
        '    'ControlMgr.SetVisibleControl(Me, btnEntityUndo, oEnable)
        '    Me.ChangeControlEnabledProperty(Me.btnEntitySave, oEnable)
        '    Me.ChangeControlEnabledProperty(Me.btnEntityUndo, oEnable)

        '    Me.ChangeControlEnabledProperty(Me.cboPeriodEntity1, oEnable)
        '    Me.ChangeControlEnabledProperty(Me.cboPeriodEntity2, oEnable)
        '    Me.ChangeControlEnabledProperty(Me.cboPeriodEntity3, oEnable)
        '    Me.ChangeControlEnabledProperty(Me.cboPeriodEntity4, oEnable)
        '    Me.ChangeControlEnabledProperty(Me.cboPeriodEntity5, oEnable)

        'End Sub

        Private Sub btnEntityUndo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEntityUndo.Click
            Dim dealerId As Guid = TheDealerControl.SelectedGuid
            If dealerId.Equals(Guid.Empty) Then
                Exit Sub
            End If
            PopulatePayeeType()
            PopulatePeriodEntity()
            TheDealerControl.ChangeEnabledControlProperty(False)

        End Sub

        'Public Sub RegisterClientServerIds()

        '    Dim onloadScript As New System.Text.StringBuilder()
        '    onloadScript.Append("<script type='text/javascript'>")
        '    onloadScript.Append(Environment.NewLine)
        '    onloadScript.Append("var BrokerMakupID = '" + Me.txtBrokerMakupPct.ClientID + "';")
        '    onloadScript.Append("var BrokerMakup2ID = '" + Me.txtBrokerMakupPct2.ClientID + "';")
        '    onloadScript.Append("var BrokerMakup3ID = '" + Me.txtBrokerMakupPct3.ClientID + "';")
        '    onloadScript.Append("var BrokerMakup4ID = '" + Me.txtBrokerMakupPct4.ClientID + "';")
        '    onloadScript.Append("var BrokerMakup5ID = '" + Me.txtBrokerMakupPct5.ClientID + "';")

        '    onloadScript.Append("var markup = '" + Me.txtBrokerMakupPct2.ClientID + "';")
        '    onloadScript.Append("var markup2 = '" + Me.txtBrokerMakupPct2.ClientID + "';")
        '    onloadScript.Append("var markup3 = '" + Me.txtBrokerMakupPct3.ClientID + "';")
        '    onloadScript.Append("var markup4 = '" + Me.txtBrokerMakupPct4.ClientID + "';")
        '    onloadScript.Append("var markup5 = '" + Me.txtBrokerMakupPct5.ClientID + "';")

        '    onloadScript.Append("var BrokerCommID = '" + Me.txtBrokerCommPct.ClientID + "';")
        '    onloadScript.Append("var BrokerComm2ID = '" + Me.txtBrokerCommPct2.ClientID + "';")
        '    onloadScript.Append("var BrokerComm3ID = '" + Me.txtBrokerCommPct3.ClientID + "';")
        '    onloadScript.Append("var BrokerComm4ID = '" + Me.txtBrokerCommPct4.ClientID + "';")
        '    onloadScript.Append("var BrokerComm5ID = '" + Me.txtBrokerCommPct5.ClientID + "';")

        '    onloadScript.Append("var Comm = '" + Me.txtBrokerCommPct.ClientID + "';")
        '    onloadScript.Append("var Comm2 = '" + Me.txtBrokerCommPct2.ClientID + "';")
        '    onloadScript.Append("var Comm3 = '" + Me.txtBrokerCommPct3.ClientID + "';")
        '    onloadScript.Append("var Comm4 = '" + Me.txtBrokerCommPct4.ClientID + "';")
        '    onloadScript.Append("var Comm5 = '" + Me.txtBrokerCommPct5.ClientID + "';")

        '    onloadScript.Append("var markupTotalID = '" + Me.txtBrokerPctTotal.ClientID + "';")
        '    onloadScript.Append("var CommTotalID = '" + Me.txtCommPctTotal.ClientID + "';")

        '    onloadScript.Append(Environment.NewLine)
        '    onloadScript.Append("</script>")
        '    ' Register script with page 
        '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "onLoadCall", onloadScript.ToString())

        'End Sub

        'Private Sub cboPeriodEntity1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPeriodEntity1.SelectedIndexChanged
        '    UpdateEntityTable()
        'End Sub

        'Private Sub cboPeriodEntity2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPeriodEntity2.SelectedIndexChanged
        '    UpdateEntityTable()
        'End Sub

        'Private Sub cboPeriodEntity3_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPeriodEntity3.SelectedIndexChanged
        '    UpdateEntityTable()
        'End Sub

        'Private Sub cboPeriodEntity4_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPeriodEntity4.SelectedIndexChanged
        '    UpdateEntityTable()
        'End Sub

        'Private Sub cboPeriodEntity5_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPeriodEntity5.SelectedIndexChanged
        '    UpdateEntityTable()
        'End Sub

#Region "Acct Source Xcd Option Bucket Logic"
        Private Sub ValidateDiffSourceLogic()
            If Me.State.IsCompanyConfiguredForSourceXcd Then

                ValidateDifferenceSourceXcd()

                If Me.State.IsDiffSelectedTwice Then
                    ElitaPlusPage.SetLabelError(Me.lblAcctSourceBucket)
                    Throw New GUIException(Message.MSG_DIFFERENCE_OPTION_ALLOWED_ONLY_ONCE, Assurant.ElitaPlus.Common.ErrorCodes.MSG_DIFFERENCE_SOURCE_ALLOWED_ONLY_FOR_ONE_BUCKET)
                ElseIf Me.State.IsDiffNotSelectedOnce Then
                    ElitaPlusPage.SetLabelError(Me.lblAcctSourceBucket)
                    Throw New GUIException(Message.MSG_DIFFERENCE_OPTION_ATLEAST_ONE, Assurant.ElitaPlus.Common.ErrorCodes.MSG_DIFFERENCE_OPTION_SHOULD_PRESENT_ATLEAST_FOR_ONE_BUCKET)
                End If

            End If
        End Sub

        Private Sub ValidateDifferenceSourceXcd()

            Me.State.IsDiffSelectedTwice = False
            Me.State.IsDiffNotSelectedOnce = False
            Dim countDiff As Integer = 0

            If cboBrokerCommPctSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_COMMBRKDOWN_OPTION_DIFFERENCE) Then
                countDiff = countDiff + 1
            End If

            If cboBrokerCommPct2SourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_COMMBRKDOWN_OPTION_DIFFERENCE) Then
                countDiff = countDiff + 1
            End If

            If cboBrokerCommPct3SourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_COMMBRKDOWN_OPTION_DIFFERENCE) Then
                countDiff = countDiff + 1
            End If

            If cboBrokerCommPct4SourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_COMMBRKDOWN_OPTION_DIFFERENCE) Then
                countDiff = countDiff + 1
            End If

            If cboBrokerCommPct5SourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_COMMBRKDOWN_OPTION_DIFFERENCE) Then
                countDiff = countDiff + 1
            End If

            If countDiff > 1 Then
                Me.State.IsDiffSelectedTwice = True
            ElseIf countDiff < 1 Then
                Me.State.IsDiffNotSelectedOnce = True
            End If

        End Sub

        'Private Sub HideGridSourceXcdColumn()
        '    If Not Me.State.IsCompanyConfiguredForSourceXcd Then
        '        Me.Grid.Columns(COMMISSION_PERCENT1_SOURCE_COL).Visible = False
        '        Me.Grid.Columns(COMMISSION_PERCENT2_SOURCE_COL).Visible = False
        '        Me.Grid.Columns(COMMISSION_PERCENT3_SOURCE_COL).Visible = False
        '        Me.Grid.Columns(COMMISSION_PERCENT4_SOURCE_COL).Visible = False
        '        Me.Grid.Columns(COMMISSION_PERCENT5_SOURCE_COL).Visible = False
        '    End If
        'End Sub

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

        Private Sub SetGridSourceXcdLabelFromBoforNew()
            'If moGridView.EditIndex = -1 Then Exit Sub
            For pageIndexk As Integer = 0 To Me.moGridView.PageCount - 1
                Me.moGridView.PageIndex = pageIndexk
                Dim rowNum As Integer = Me.moGridView.Rows.Count
                For i As Integer = 0 To rowNum - 1
                    Dim gRow As GridViewRow = moGridView.Rows(i)
                    If gRow.RowType = DataControlRowType.DataRow Then
                        Dim molblCommPercentSourceXcd As Label = DirectCast(gRow.Cells(COL_COMMISSIONS_SOURCE_XCD_IDX).FindControl("lblCommPercentSourceXcd"), Label)
                        Dim molblPayeeType As Label = DirectCast(gRow.Cells(COL_COMMISSIONS_SOURCE_XCD_IDX).FindControl("lblPayeeType"), Label)
                        Dim molblEntityType As Label = DirectCast(gRow.Cells(COL_COMMISSIONS_SOURCE_XCD_IDX).FindControl("lblEntityType"), Label)

                        If Not molblCommPercentSourceXcd Is Nothing Then
                            If molblCommPercentSourceXcd.Visible Then
                                If (Not molblCommPercentSourceXcd.Text Is Nothing And Not String.IsNullOrWhiteSpace(molblCommPercentSourceXcd.Text)) Then
                                    molblCommPercentSourceXcd.Text = GetCodeAmtSourceOption(molblCommPercentSourceXcd.Text)
                                End If
                            End If
                        End If

                        If Not molblPayeeType Is Nothing Then
                            If molblPayeeType.Visible Then
                                If (Not molblPayeeType.Text Is Nothing And Not String.IsNullOrWhiteSpace(molblPayeeType.Text)) Then
                                    molblPayeeType.Text = GetDescOfExtCodePayeeTypeXcdOption(molblPayeeType.Text)
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

        'Private Sub SetSourceBucketValues()
        '    If Me.State.IsCompanyConfiguredForSourceXcd Then

        '        DisableMarkUpPercentage()

        '        If Me.cboBrokerCommPctSourceXcd.Visible And Me.cboBrokerCommPctSourceXcd.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
        '            If cboBrokerCommPctSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_COMMBRKDOWN_OPTION_DIFFERENCE) Then
        '                Me.txtBrokerCommPct.Text = "0.0000"
        '                ControlMgr.SetEnableControl(Me, txtBrokerCommPct, False)
        '            Else
        '                ControlMgr.SetEnableControl(Me, txtBrokerCommPct, True)
        '            End If
        '        End If

        '        If Me.cboBrokerCommPct2SourceXcd.Visible And Me.cboBrokerCommPct2SourceXcd.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
        '            If cboBrokerCommPct2SourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_COMMBRKDOWN_OPTION_DIFFERENCE) Then
        '                Me.txtBrokerCommPct2.Text = "0.0000"
        '                ControlMgr.SetEnableControl(Me, txtBrokerCommPct2, False)
        '            Else
        '                ControlMgr.SetEnableControl(Me, txtBrokerCommPct2, True)
        '            End If
        '        End If

        '        If Me.cboBrokerCommPct3SourceXcd.Visible And Me.cboBrokerCommPct3SourceXcd.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
        '            If cboBrokerCommPct3SourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_COMMBRKDOWN_OPTION_DIFFERENCE) Then
        '                Me.txtBrokerCommPct3.Text = "0.0000"
        '                ControlMgr.SetEnableControl(Me, txtBrokerCommPct3, False)
        '            Else
        '                ControlMgr.SetEnableControl(Me, txtBrokerCommPct3, True)
        '            End If
        '        End If

        '        If Me.cboBrokerCommPct4SourceXcd.Visible And Me.cboBrokerCommPct4SourceXcd.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
        '            If cboBrokerCommPct4SourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_COMMBRKDOWN_OPTION_DIFFERENCE) Then
        '                Me.txtBrokerCommPct4.Text = "0.0000"
        '                ControlMgr.SetEnableControl(Me, txtBrokerCommPct4, False)
        '            Else
        '                ControlMgr.SetEnableControl(Me, txtBrokerCommPct4, True)
        '            End If
        '        End If

        '        If Me.cboBrokerCommPct5SourceXcd.Visible And Me.cboBrokerCommPct5SourceXcd.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
        '            If cboBrokerCommPct5SourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_COMMBRKDOWN_OPTION_DIFFERENCE) Then
        '                Me.txtBrokerCommPct5.Text = "0.0000"
        '                ControlMgr.SetEnableControl(Me, txtBrokerCommPct5, False)
        '            Else
        '                ControlMgr.SetEnableControl(Me, txtBrokerCommPct5, True)
        '            End If
        '        End If

        '        DisableCommPercentage()

        '    End If
        'End Sub

        'Private Sub BindSourceOptionDropdownlist()
        '    If Me.State.IsCompanyConfiguredForSourceXcd Then
        '        DisplaySourceXcdFields()

        '        Dim oAcctBucketsSourceOption As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="ACCTBUCKETSOURCE_COMMBRKDOWN")

        '        cboBrokerCommPctSourceXcd.Populate(oAcctBucketsSourceOption, New PopulateOptions() With
        '                             {
        '                             .AddBlankItem = False,
        '                             .TextFunc = AddressOf PopulateOptions.GetDescription,
        '                             .ValueFunc = AddressOf PopulateOptions.GetExtendedCode
        '                             })

        '        cboBrokerCommPct2SourceXcd.Populate(oAcctBucketsSourceOption, New PopulateOptions() With
        '                            {
        '                            .AddBlankItem = False,
        '                            .TextFunc = AddressOf PopulateOptions.GetDescription,
        '                            .ValueFunc = AddressOf PopulateOptions.GetExtendedCode
        '                            })

        '        cboBrokerCommPct3SourceXcd.Populate(oAcctBucketsSourceOption, New PopulateOptions() With
        '                            {
        '                            .AddBlankItem = False,
        '                            .TextFunc = AddressOf PopulateOptions.GetDescription,
        '                            .ValueFunc = AddressOf PopulateOptions.GetExtendedCode
        '                            })

        '        cboBrokerCommPct4SourceXcd.Populate(oAcctBucketsSourceOption, New PopulateOptions() With
        '                            {
        '                            .AddBlankItem = False,
        '                            .TextFunc = AddressOf PopulateOptions.GetDescription,
        '                            .ValueFunc = AddressOf PopulateOptions.GetExtendedCode
        '                            })

        '        cboBrokerCommPct5SourceXcd.Populate(oAcctBucketsSourceOption, New PopulateOptions() With
        '                            {
        '                            .AddBlankItem = False,
        '                            .TextFunc = AddressOf PopulateOptions.GetDescription,
        '                            .ValueFunc = AddressOf PopulateOptions.GetExtendedCode
        '                            })
        '    Else
        '        HideSourceScdFields()
        '    End If

        'End Sub

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
            ControlMgr.SetVisibleControl(Me, cboBrokerCommPctSourceXcd, True)
            ControlMgr.SetVisibleControl(Me, cboBrokerCommPct2SourceXcd, True)
            ControlMgr.SetVisibleControl(Me, cboBrokerCommPct3SourceXcd, True)
            ControlMgr.SetVisibleControl(Me, cboBrokerCommPct4SourceXcd, True)
            ControlMgr.SetVisibleControl(Me, cboBrokerCommPct5SourceXcd, True)

            ControlMgr.SetVisibleControl(Me, lblAcctSourceBucket, True)
        End Sub

        Private Sub HideSourceScdFields()
            ControlMgr.SetVisibleControl(Me, cboBrokerCommPctSourceXcd, False)
            ControlMgr.SetVisibleControl(Me, cboBrokerCommPct2SourceXcd, False)
            ControlMgr.SetVisibleControl(Me, cboBrokerCommPct3SourceXcd, False)
            ControlMgr.SetVisibleControl(Me, cboBrokerCommPct4SourceXcd, False)
            ControlMgr.SetVisibleControl(Me, cboBrokerCommPct5SourceXcd, False)

            ControlMgr.SetVisibleControl(Me, lblAcctSourceBucket, False)
        End Sub

        Private Sub DisableMarkUpPercentage()
            Me.txtBrokerMakupPct.Text = "0.0000"
            ControlMgr.SetEnableControl(Me, txtBrokerMakupPct, False)

            Me.txtBrokerMakupPct2.Text = "0.0000"
            ControlMgr.SetEnableControl(Me, txtBrokerMakupPct2, False)

            Me.txtBrokerMakupPct3.Text = "0.0000"
            ControlMgr.SetEnableControl(Me, txtBrokerMakupPct3, False)

            Me.txtBrokerMakupPct4.Text = "0.0000"
            ControlMgr.SetEnableControl(Me, txtBrokerMakupPct4, False)

            Me.txtBrokerMakupPct5.Text = "0.0000"
            ControlMgr.SetEnableControl(Me, txtBrokerMakupPct5, False)
        End Sub

        Private Sub DisableCommPercentage()
            If Me.txtBrokerCommPct.Visible And String.IsNullOrWhiteSpace(Me.txtBrokerCommPct.Text) Then
                Me.txtBrokerCommPct.Text = "0.0000"
            End If

            If Me.txtBrokerCommPct2.Visible And String.IsNullOrWhiteSpace(Me.txtBrokerCommPct2.Text) Then
                Me.txtBrokerCommPct2.Text = "0.0000"
            End If

            If Me.txtBrokerCommPct3.Visible And String.IsNullOrWhiteSpace(Me.txtBrokerCommPct3.Text) Then
                Me.txtBrokerCommPct3.Text = "0.0000"
            End If

            If Me.txtBrokerCommPct4.Visible And String.IsNullOrWhiteSpace(Me.txtBrokerCommPct4.Text) Then
                Me.txtBrokerCommPct4.Text = "0.0000"
            End If

            If Me.txtBrokerCommPct5.Visible And String.IsNullOrWhiteSpace(Me.txtBrokerCommPct5.Text) Then
                Me.txtBrokerCommPct5.Text = "0.0000"
            End If
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
                    'nIndex = e.Item.ItemIndex
                    moGridView.EditIndex = nIndex
                    moGridView.SelectedIndex = nIndex
                    '      EnableForEditRateButtons(True)
                    PopulateCoverageRateList(ACTION_EDIT)
                    'Me.FillDropdownList()
                    'PopulateCoverageRate()

                    Me.FillSourceXcdDropdownList()
                    FillEntityDropDownList()
                    FillPayeeTypeDropDownList()
                    SetGridSourceXcdDropdownFromBo()
                    SetGridSourceXcdLabelFromBo()

                    SetGridControls(moGridView, False)
                    'Me.SetFocusInGrid(moGridView, nIndex, LOW_PRICE)
                    'EnableDisableControls(Me.moCoverageEditPanel, True)
                    'setbuttons(False)
                ElseIf (e.CommandName = DELETE_COMMAND_NAME) Then
                    'nIndex = e.Item.ItemIndex
                    moGridView.EditIndex = nIndex
                    moGridView.SelectedIndex = nIndex
                    CoverageRateId = Me.GetGridText(moGridView, nIndex, COL_COMMISSION_PLAN_DIST_ID_IDX)
                    Me.State.moCommPlanDistId = GetGuidFromString(CoverageRateId)
                    ' Me.State.moCoverageRateList(moGridView.SelectedIndex).Id
                    Me.State.MyBoDist = New CommPlanDistribution(Me.State.moCommPlanDistId)
                    If DeleteSelectedCoverageRate(nIndex) = True Then
                        PopulateCoverageRateList(ACTION_CANCEL_DELETE)
                    End If

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
                SaveRateChanges()
                SetGridSourceXcdLabelFromBo()

                TheDealerControl.ChangeEnabledControlProperty(False)
                'ControlMgr.SetEnableControl(Me, moProductDrop, False)
                'ControlMgr.SetEnableControl(Me, moRiskDrop, False)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub BtnCancelRate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancelRate.Click
            'Pencil button in not in edit mode
            Try
                'IsNewRate = False
                Me.State.IsCommPlanDistNew = False
                EnableForEditRateButtons(False)
                PopulateCoverageRateList(ACTION_CANCEL_DELETE)
                'US-521697
                SetGridSourceXcdLabelFromBo()
                'EnableDisableControls(Me.moCoverageEditPanel, False)
                'setbuttons(True)
                TheDealerControl.ChangeEnabledControlProperty(False)
                'ControlMgr.SetEnableControl(Me, moProductDrop, False)
                'ControlMgr.SetEnableControl(Me, moRiskDrop, False)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub BtnNewRate_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNewRate_WRITE.Click
            Try
                'IsNewRate = True
                If moGridView.Rows.Count = 5 Then
                    'Me.DisplayMessage(Message.MSG_DISTRIBUTION_RECORD_EXCEEDED_LIMIT, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO, Me.HiddenSaveChangesPromptResponse)
                    'Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.OK                    
                    Throw New GUIException(Message.MSG_DISTRIBUTION_RECORD_EXCEEDED_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.MSG_COMMISSION_DISTRIBUTION_RECORD_EXCEEDS_LIMIT)
                Else
                    Me.State.IsCommPlanDistNew = True
                    CoverageRateId = Guid.Empty.ToString
                    PopulateCoverageRateList(ACTION_NEW)
                End If

                'FillDropdownList()
                'SetGridControls(moGridView, True)
                FillSourceXcdDropdownList()
                FillEntityDropDownList()
                FillPayeeTypeDropDownList()
                SetGridSourceXcdDropdownFromBo()
                SetGridSourceXcdLabelFromBo()
                'SetGridSourceXcdTextboxForNewCoverage()

                'EnableDisableControls(Me.moCoverageEditPanel, True)
                'setbuttons(False)
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
        Private Sub SaveRateChanges()
            If ApplyRateChanges() = True Then
                'If IsNewRate = True Then
                '    IsNewRate = False
                'End If
                If Me.State.IsCommPlanDistNew = True Then
                    Me.State.IsCommPlanDistNew = False
                End If
                PopulateCoverageRateList(ACTION_SAVE)
                'EnableDisableControls(Me.moCoverageEditPanel, False)
                'setbuttons(True)
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
            'If IsNewRate = False Then
            '    CoverageRateId = Me.GetSelectedGridText(moGridView, COL_COMMISSION_PLAN_DIST_ID_IDX)
            'End If
            BindBoPropertiesToGridHeader()
            'With TheCommPlanDist
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
                'Me.PopulateBOProperty(TheCommPlanDist, PROPERTY_PAYEE_TYPE_XCD, CType(Me.GetDropDownControlFromGrid(moGridView, COL_PAYEE_TYPE_XCD_IDX), DropDownList))
                'Me.PopulateBOProperty(TheCommPlanDist, PROPERTY_ENTITY_ID, CType(Me.GetDropDownControlFromGrid(moGridView, COL_ENTITY_ID_IDX), DropDownList))                
                'Me.PopulateBOProperty(TheCommPlanDist, PROPERTY_COMM_AMT, CType(Me.GetSelectedGridControl(moGridView, COL_COMMISSION_AMOUNT_IDX), TextBox))
                'Me.PopulateBOProperty(TheCommPlanDist, PROPERTY_COMM_PER, CType(Me.GetSelectedGridControl(moGridView, COL_COMMISSION_PERCENTAGE_IDX), TextBox))
                'Me.PopulateBOProperty(TheCommPlanDist, PROPERTY_PAYEE_TYPE_XCD, CType(Me.GetDropDownControlFromGrid(moGridView, COL_COMMISSIONS_SOURCE_XCD_IDX), DropDownList))
                'Me.PopulateBOProperty(TheCommPlanDist, PROPERTY_POSITION, CType(Me.GetSelectedGridControl(moGridView, COL_POSITION_IDX), TextBox))

                CommonSourceOptionLogic()
            End With

            'ValidateCoverage()

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

            'If Me.State.IsDealerConfiguredForSourceXcd Then

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

            'If Me.State.IsIgnorePremiumSetYesForContract Then

            '    ValidateIncomingSourceXcd()

            '    If Me.State.IsBucketIncomingSelected Then
            '        Throw New GUIException(Message.MSG_INCOMING_OPTION_NOT_ALLOWED, Assurant.ElitaPlus.Common.ErrorCodes.MSG_INCOMING_OPTION_NOT_ALLOWED_WHEN_IGNORE_PREMIUM_IS_YES)
            '    End If
            'End If

            'ValidateDifferenceSourceXcd()

            'If Me.State.IsDiffSelectedTwice Then
            '    Throw New GUIException(Message.MSG_DIFFERENCE_OPTION_ALLOWED_ONLY_ONCE, Assurant.ElitaPlus.Common.ErrorCodes.MSG_DIFFERENCE_SOURCE_ALLOWED_ONLY_FOR_ONE_BUCKET)
            'ElseIf Me.State.IsDiffNotSelectedOnce Then
            '    Throw New GUIException(Message.MSG_DIFFERENCE_OPTION_ATLEAST_ONE, Assurant.ElitaPlus.Common.ErrorCodes.MSG_DIFFERENCE_OPTION_SHOULD_PRESENT_ATLEAST_FOR_ONE_BUCKET)
            'End If
            'End If
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
        'Private Sub SetGridSourceXcdTextboxForNewCoverage()
        '    If moGridView.EditIndex = -1 Then Exit Sub
        '    With TheCommPlanDist
        '        If Me.State.IsCompanyConfiguredForSourceXcd Then
        '            Dim gRow As GridViewRow = moGridView.Rows(moGridView.EditIndex)
        '            Dim mocboCommPercentSourceXcd As DropDownList = DirectCast(gRow.Cells(COL_COMMISSIONS_SOURCE_XCD_IDX).FindControl("cboCommPercentSourceXcd"), DropDownList)
        '            Dim moTextCommission_PercentText As TextBox = DirectCast(gRow.Cells(COL_COMMISSION_PERCENTAGE_IDX).FindControl("moCommission_PercentText"), TextBox)

        '            Dim diffValue As Decimal = 0.0000

        '            If mocboCommPercentSourceXcd.Visible Then
        '                If mocboCommPercentSourceXcd.Items.Count > 0 Then
        '                    If mocboCommPercentSourceXcd.SelectedItem.Value.ToUpper.Equals(Codes.ACCT_BUCKETS_SOURCE_COMMBRKDOWN_OPTION_DIFFERENCE) Then
        '                        Me.PopulateControlFromBOProperty(moTextCommission_PercentText, diffValue, Me.PERCENT_FORMAT)
        '                        moTextCommission_PercentText.Enabled = False
        '                    End If
        '                End If
        '            End If

        '        End If
        '    End With

        'End Sub
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
            oDtable.DefaultView.Sort() = "POSITION"
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
