Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.DALObjects
Imports System.Globalization

Namespace Tables

    Partial Class CommissionPeriodForm
        Inherits ElitaPlusSearchPage
        ' Inherits System.Web.UI.Page
#Region "Page State"

#Region "MyState"

        Class MyState
            Public MyBo As CommissionPeriod
            Public moCommTolerance As CommissionTolerance
            Public moCommEntity As CommissionEntity
            Public moAssocCommList As AssociateCommissions.AssocCommList
            Public moAssocComm As AssociateCommissions
            Public moIsAssocCommDirty As Boolean = False
            Public moCommPeriodEntity As CommissionPeriodEntity
            Public moCommissionPeriodId As Guid = Guid.Empty
            Public moCommissionToleranceId As Guid = Guid.Empty
            Public moInError As Boolean = False
            Public LastErrMsg As String
            'Public moTolerance As CommissionTolerance = Nothing
            Public IsPeriodNew As Boolean = False
            Public IsToleranceNew As Boolean = False
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public boChanged As Boolean = False
            Public moAssocCommSearch As AssociateCommissions.SearchDV = Nothing
            Public searchDV As DataView = Nothing
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
            Me.State.moCommissionPeriodId = CType(Me.CallingParameters, Guid)
            If Me.State.moCommissionPeriodId.Equals(Guid.Empty) Then
                Me.State.IsPeriodNew = True
                ClearPeriod()
                SetPeriodButtonsState(True)
                PopulatePeriod()
                TheDealerControl.ChangeEnabledControlProperty(True)
            Else
                Me.State.IsPeriodNew = False
                SetPeriodButtonsState(False)
                PopulatePeriod()
                TheDealerControl.ChangeEnabledControlProperty(False)
            End If
            If Not TheDealerControl.SelectedGuid.Equals(Guid.Empty) Then
                ControlMgr.SetVisibleControl(Me, moRestrictDetailPanel2, GetRestrictMarkup())
            End If
        End Sub

#End Region


#Region "Constants"

        Public Const URL As String = "CommissionPeriodForm.aspx"

        ' Property Name
        Public Const NOTHING_SELECTED As Integer = 0
        Public Const COMMISSION_PERIOD_ID_PROPERTY As String = "CommissionPeriodId"
        Public Const DEALER_ID_PROPERTY As String = "DealerId"
        Public Const EFFECTIVE_DATE_PROPERTY As String = "EffectiveDate"
        Public Const EXPIRATION_DATE_PROPERTY As String = "ExpirationDate"
        Public Const COMPUTE_METHOD_ID_PROPERTY As String = "ComputeMethodId"
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
#End Region

#End Region

#Region "Variables"

        'Private moPeriod As CommissionPeriod
        Private moExpirationData As CommissionPeriodData


#Region "Variables-Tolerance"


#End Region

#End Region

#Region "Properties"

        Private ReadOnly Property ThePeriod() As CommissionPeriod
            Get
                If Me.State.MyBo Is Nothing Then
                    If Me.State.IsPeriodNew = True Then
                        ' For creating, inserting
                        Me.State.MyBo = New CommissionPeriod
                        Me.State.moCommissionPeriodId = Me.State.MyBo.Id
                    Else
                        ' For updating, deleting
                        Me.State.MyBo = New CommissionPeriod(Me.State.moCommissionPeriodId)
                    End If
                End If
                BindBoPropertiesToLabels(Me.State.MyBo)
                Return Me.State.MyBo
            End Get
        End Property

        Private ReadOnly Property ExpirationCount() As Integer
            Get
                If moExpirationData Is Nothing Then
                    moExpirationData = New CommissionPeriodData
                    moExpirationData.dealerId = TheDealerControl.SelectedGuid 'Me.GetSelectedItem(moDealerDrop_WRITE)
                End If
                Return ThePeriod.ExpirationCount(moExpirationData)
            End Get
        End Property

        Private ReadOnly Property MaxExpiration() As Date
            Get
                If moExpirationData Is Nothing Then
                    moExpirationData = New CommissionPeriodData
                    moExpirationData.dealerId = TheDealerControl.SelectedGuid
                End If
                Return ThePeriod.MaxExpiration(moExpirationData)
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

#Region "Properties-Tolerance"

        Private ReadOnly Property TheComTolerance() As CommissionTolerance
            Get
                'If Me.State.IsToleranceNew = True Then
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

                If Not Page.IsPostBack Then
                    Me.MasterPage.MessageController.Clear()
                    Me.MasterPage.UsePageTabTitleInBreadCrum = False
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")
                    UpdateBreadCrum()
                    'Me.SetFormTitle(PAGETITLE)
                    Me.SetFormTab(PAGETAB)
                    Me.TranslateGridHeader(Grid)
                    Me.TranslateGridControls(Grid)
                    Me.SetGridItemStyleColor(Me.Grid)
                    Me.SetStateProperties()
                    ExecuteEvents()

                    Me.AddControlMsg(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO,
                                                                        Me.MSG_TYPE_CONFIRM, True)
                    Me.AddCalendar(Me.BtnEffectiveDate_WRITE, Me.moEffectiveText_WRITE)
                    Me.AddCalendar(Me.BtnExpirationDate_WRITE, Me.moExpirationText_WRITE)
                Else
                    CheckIfComingFromConfirm()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)
        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub GoBack()
            Dim retType As New CommissionPeriodSearchForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back,
                                                                                Me.State.moCommissionPeriodId, Me.State.boChanged)
            Me.ReturnToCallingPage(retType)
        End Sub

        Private Sub btnEntityBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEntityBack.Click
            Try
                If IsDirtyPeriodBO() = True Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM,
                                            Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    Me.ClearGridHeaders(Me.Grid)
                    Me.State.IsToleranceNew = False
                    PopulateTolerance(Me.POPULATE_ACTION_NO_EDIT)
                    ControlMgr.SetVisibleControl(Me, moPeriodButtonPanel, True)
                    EnableDisablePeriodEntity(True)
                    Me.EnableDisableControls(moPeriodPanel_WRITE, False)
                    hdnSelectedTab.Value = 0
                    ControlMgr.SetVisibleControl(Me, Me.btnEntityBack, False)
                    TheDealerControl.ChangeEnabledControlProperty(False)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
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
                Me.State.IsPeriodNew = False
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
            Me.State.moCommissionPeriodId = Guid.Empty
            Me.State.IsPeriodNew = True
            ClearPeriod()
            SetPeriodButtonsState(True)
            PopulatePeriod()
            TheDealerControl.ChangeEnabledControlProperty(True)
            ControlMgr.SetVisibleControl(Me, moRestrictDetailPanel2, True)
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
            Dim newObj As New CommissionPeriod
            'Me.State.moCommissionPeriodId = Guid.Empty
            Me.State.MyBo = Nothing
            newObj.Copy(ThePeriod)
            Me.State.IsPeriodNew = True
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
                If Me.State.IsPeriodNew = True Then
                    PopulatePayeeType()
                End If
                ControlMgr.SetVisibleControl(Me, moRestrictDetailPanel2, GetRestrictMarkup())
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
        'Private Sub moDealerDrop_WRITE_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moDealerDrop_WRITE.SelectedIndexChanged
        '    EnableDateFields()
        'End Sub
#End Region

#Region "Handlers-Labels"

        Protected Sub BindBoPropertiesToLabels(ByVal oPeriod As CommissionPeriod)
            Me.BindBOPropertyToLabel(oPeriod, DEALER_ID_PROPERTY, Me.TheDealerControl.CaptionLabel)
            Me.BindBOPropertyToLabel(oPeriod, EFFECTIVE_DATE_PROPERTY, moEffectiveLabel)
            Me.BindBOPropertyToLabel(oPeriod, EXPIRATION_DATE_PROPERTY, moExpirationLabel)
            Me.BindBOPropertyToLabel(oPeriod, COMPUTE_METHOD_ID_PROPERTY, Me.moComputeMethodLabel)
        End Sub

        Public Sub ClearLabelsErrSign()
            Me.ClearLabelErrSign(Me.TheDealerControl.CaptionLabel)
            Me.ClearLabelErrSign(moEffectiveLabel)
            Me.ClearLabelErrSign(moExpirationLabel)
            Me.ClearLabelErrSign(moComputeMethodLabel)
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
                    If Me.State.IsPeriodNew = True Then
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
                    If Me.State.IsPeriodNew = True Then
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

            If Me.State.IsPeriodNew = True Then
                TheDealerControl.NothingSelected = True
            Else
                TheDealerControl.SelectedGuid = ThePeriod.DealerId
            End If

        End Sub

        Private Sub PopulateDates()
            Me.PopulateControlFromBOProperty(moEffectiveText_WRITE, ThePeriod.EffectiveDate)
            Me.PopulateControlFromBOProperty(moExpirationText_WRITE, ThePeriod.ExpirationDate)
        End Sub

        Private Sub PupulateComputeMethod()
            'Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            'Dim dvComputeMethod As DataView = LookupListNew.DropdownLookupList(LookupListNew.LK_COMPUTE_METHOD_CODE, langId, True)
            'Me.BindListControlToDataView(Me.moComputeMethodDropDown, dvComputeMethod)

            Dim ComputeMethodList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="MCM", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
            Me.moComputeMethodDropDown.Populate(ComputeMethodList, New PopulateOptions() With
            {
                .AddBlankItem = True
            })

            If Me.State.IsPeriodNew = True Then
                ThePeriod.ComputeMethodId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMPUTE_METHOD, COMPUTE_METHOD_COMPUTE_ON_NET)
            End If
            Me.PopulateControlFromBOProperty(Me.moComputeMethodDropDown, ThePeriod.ComputeMethodId)
        End Sub

        Private Sub PopulatePeriod()
            Try
                PopulateDealer()
                PopulateDates()
                EnableDateFields()
                PopulateTolerance()
                PupulateComputeMethod()
                If Me.State.IsPeriodNew = False Then
                    PopulatePayeeType()
                End If
                PopulatePeriodEntity()

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

#End Region

#Region "Client Attributes"

        Private Sub ExecuteEvents()
            cboPayeeType1.Attributes("onChange") = String.Format("return EnableControl('{0}','{1}');", cboPayeeType1.ClientID, cboPeriodEntity1.ClientID)
            cboPayeeType2.Attributes("onChange") = String.Format("return EnableControl('{0}','{1}');", cboPayeeType2.ClientID, cboPeriodEntity2.ClientID)
            cboPayeeType3.Attributes("onChange") = String.Format("return EnableControl('{0}','{1}');", cboPayeeType3.ClientID, cboPeriodEntity3.ClientID)
            cboPayeeType4.Attributes("onChange") = String.Format("return EnableControl('{0}','{1}');", cboPayeeType4.ClientID, cboPeriodEntity4.ClientID)
            cboPayeeType5.Attributes("onChange") = String.Format("return EnableControl('{0}','{1}');", cboPayeeType5.ClientID, cboPeriodEntity5.ClientID)
        End Sub
#End Region

#Region "Clear"

        Private Sub ClearDealer()
            If Me.State.IsPeriodNew = True Then
                'moDealerDrop_WRITE.SelectedIndex = 0
                TheDealerControl.SelectedIndex = 0
            Else
                'Me.SetSelectedItem(moDealerDrop_WRITE, ThePeriod.DealerId)
                TheDealerControl.SelectedGuid = ThePeriod.DealerId

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

        Private Sub PopulatePeriodBOFromForm(ByVal oPeriod As CommissionPeriod)
            With oPeriod
                ' DropDowns
                .DealerId = TheDealerControl.SelectedGuid 'Me.GetSelectedItem(moDealerDrop_WRITE)
                Me.PopulateBOProperty(oPeriod, COMPUTE_METHOD_ID_PROPERTY, Me.moComputeMethodDropDown)

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
            Dim oPeriod As CommissionPeriod

            oPeriod = ThePeriod
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
            Dim PayeeTypeCode As String
            PayeeTypeCode = LookupListNew.GetCodeFromId(LookupListNew.LK_PAYEE_TYPE, Me.GetSelectedItem(cboPayeeType1))
            If PayeeTypeCode = Payee_Type_Comm_Entity Then
                If Me.GetSelectedItem(cboPeriodEntity1).Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(Me.LabelCommEntity)
                    ControlMgr.SetEnableControl(Me, cboPeriodEntity1, True)
                    Me.cboPeriodEntity1.Focus()
                    Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.MISSING_COMM_ENTITY_ERR)
                End If
            End If
            PayeeTypeCode = LookupListNew.GetCodeFromId(LookupListNew.LK_PAYEE_TYPE, Me.GetSelectedItem(cboPayeeType2))
            If PayeeTypeCode = Payee_Type_Comm_Entity Then
                If Me.GetSelectedItem(cboPeriodEntity2).Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(Me.LabelCommEntity)
                    ControlMgr.SetEnableControl(Me, cboPeriodEntity2, True)
                    Me.cboPeriodEntity2.Focus()
                    Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.MISSING_COMM_ENTITY_ERR)
                End If
            End If
            PayeeTypeCode = LookupListNew.GetCodeFromId(LookupListNew.LK_PAYEE_TYPE, Me.GetSelectedItem(cboPayeeType3))
            If PayeeTypeCode = Payee_Type_Comm_Entity Then
                If Me.GetSelectedItem(cboPeriodEntity3).Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(Me.LabelCommEntity)
                    ControlMgr.SetEnableControl(Me, cboPeriodEntity3, True)
                    Me.cboPeriodEntity3.Focus()
                    Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.MISSING_COMM_ENTITY_ERR)
                End If
            End If
            PayeeTypeCode = LookupListNew.GetCodeFromId(LookupListNew.LK_PAYEE_TYPE, Me.GetSelectedItem(cboPayeeType4))
            If PayeeTypeCode = Payee_Type_Comm_Entity Then
                If Me.GetSelectedItem(cboPeriodEntity4).Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(Me.LabelCommEntity)
                    ControlMgr.SetEnableControl(Me, cboPeriodEntity4, True)
                    Me.cboPeriodEntity4.Focus()
                    Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.MISSING_COMM_ENTITY_ERR)
                End If
            End If
            PayeeTypeCode = LookupListNew.GetCodeFromId(LookupListNew.LK_PAYEE_TYPE, Me.GetSelectedItem(cboPayeeType5))
            If PayeeTypeCode = Payee_Type_Comm_Entity Then
                If Me.GetSelectedItem(cboPeriodEntity5).Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(Me.LabelCommEntity)
                    ControlMgr.SetEnableControl(Me, cboPeriodEntity5, True)
                    Me.cboPeriodEntity5.Focus()
                    Throw New GUIException(Message.MSG_INVALID_LIABILITY_LIMIT, Assurant.ElitaPlus.Common.ErrorCodes.MISSING_COMM_ENTITY_ERR)
                End If
            End If

        End Sub

        Private Function ApplyPeriodChanges() As Boolean
            Dim bIsOk As Boolean = True
            Dim oPeriod As CommissionPeriod

            Try
                ValidatePayeeType()
                SetLabelColor(Me.LabelCommEntity)
                UpdateEntityTable()
                If IsDirtyPeriodBO() = True Then
                    If Not Me.State.moCommTolerance Is Nothing Then
                        PopulateToleranceBOFromForm()
                    End If
                    oPeriod = ThePeriod
                    'BindBoPropertiesToLabels(Me.State.MyBo)
                    oPeriod.Save()
                    'Me.State.MyBo = Nothing
                    'oPeriod = ThePeriod
                    Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                Else
                    Me.DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                End If
                Me.State.IsPeriodNew = False
                SetPeriodButtonsState(False)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
                bIsOk = False
            End Try
            Return bIsOk
        End Function

        Private Function DeletePeriod() As Boolean
            Dim bIsOk As Boolean = True
            Dim oPeriod As CommissionPeriod

            Try
                oPeriod = ThePeriod
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
            EnableDisablePeriodEntity(False)
            Me.EnableDisableControls(moPeriodPanel_WRITE, True)
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

        Private Sub SaveToleranceChanges()
            If ApplyToleranceChanges() = True Then
                Me.State.MyBo = New CommissionPeriod(Me.State.MyBo.Id)
                PopulateFormFromPeriodEntityBO()
                PopulateTolerance(Me.POPULATE_ACTION_SAVE)
                ControlMgr.SetVisibleControl(Me, moPeriodButtonPanel, True)
                EnableDisablePeriodEntity(True)
                PopulatePeriodEntity()
                TheDealerControl.ChangeEnabledControlProperty(False)
                ControlMgr.SetEnableControl(Me, moComputeMethodDropDown, True)
                'Me.EnableDisableControls(moPeriodPanel_WRITE, False)
            End If
        End Sub

        Private Sub BtnSaveGrid_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSaveGrid_WRITE.Click
            Try
                SaveToleranceChanges()
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
                    Me.ClearGridHeaders(Me.Grid)
                    Me.State.IsToleranceNew = False
                    PopulateTolerance(Me.POPULATE_ACTION_NO_EDIT)
                    ControlMgr.SetVisibleControl(Me, moPeriodButtonPanel, True)
                    EnableDisablePeriodEntity(True)
                    Me.EnableDisableControls(moPeriodPanel_WRITE, False)
                    TheDealerControl.ChangeEnabledControlProperty(False)
                    ControlMgr.SetVisibleControl(Me, Me.btnEntityBack, False)
                End If
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
            EnableDisablePeriodEntity(False)
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
            If Me.State.IsPeriodNew = False Then
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
            ControlMgr.SetVisibleControl(Me, moRestrictDetailPanel2, bIsReadWrite)
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

        Protected Sub BindBoAssocCommToLabels(ByVal obj As AssociateCommissions)
            Me.BindBOPropertyToLabel(obj, "MarkupPercent", Me.lblMarkup)
            Me.BindBOPropertyToLabel(obj, "CommissionPercent", Me.lblComm)
            Me.BindBOPropertyToLabel(obj, MARKUP_TOTAL, Me.lblTotal)
            Me.BindBOPropertyToLabel(obj, COMMISSION_TOTAL, Me.lblTotal)
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
            Dim nInitValue As New DecimalType(0)
            Dim i As Integer
            Dim oPeriodEntity As CommissionPeriodEntity
            Dim oAssocCommission As AssociateCommissions
            Dim oCommEntity As CommissionEntity

            Dim oEntityView As DataView
            Dim oDataView As AssociateCommissions.SearchDV = AssociateCommissions.getList(Me.State.moCommTolerance.Id)
            Dim oMakupTotal As Decimal = 0
            Dim oBrokerComm As Decimal = 0
            Dim strPayeeType As String
            Dim strPositioncheck As String = String.Empty

            If (Me.State.searchDV Is Nothing) Then
                Me.State.searchDV = oDataView
            End If

            'Me.State.moAssocCommSearch = AssociateCommissions.getList(TheComTolerance.Id)
            If Me.State.IsToleranceNew Then
                For Each oPeriodEntity In Me.State.MyBo.AssociatedCommPeriodEntity
                    Select Case oPeriodEntity.Position
                        Case 1
                            If Not oPeriodEntity.EntityId.Equals(Guid.Empty) Then
                                oCommEntity = New CommissionEntity(oPeriodEntity.EntityId)
                                PopulateToleranceFormItem(Me.txtBrokerMakupEntity, oCommEntity.EntityName)
                            Else
                                strPayeeType = LookupListNew.GetDescriptionFromId(LookupListNew.LK_PAYEE_TYPE, oPeriodEntity.PayeeTypeId)
                                PopulateToleranceFormItem(Me.txtBrokerMakupEntity, strPayeeType)
                            End If
                            PopulateToleranceFormItem(Me.txtBrokerMakupPct, nInitValue)
                            PopulateToleranceFormItem(Me.txtBrokerCommPct, nInitValue)
                        Case 2
                            If Not oPeriodEntity.EntityId.Equals(Guid.Empty) Then
                                oCommEntity = New CommissionEntity(oPeriodEntity.EntityId)
                                PopulateToleranceFormItem(Me.txtBrokerMakupEntity2, oCommEntity.EntityName)
                            Else
                                strPayeeType = LookupListNew.GetDescriptionFromId(LookupListNew.LK_PAYEE_TYPE, oPeriodEntity.PayeeTypeId)
                                PopulateToleranceFormItem(Me.txtBrokerMakupEntity2, strPayeeType)
                            End If
                            PopulateToleranceFormItem(Me.txtBrokerMakupPct2, nInitValue)
                            PopulateToleranceFormItem(Me.txtBrokerCommPct2, nInitValue)
                        Case 3
                            If Not oPeriodEntity.EntityId.Equals(Guid.Empty) Then
                                oCommEntity = New CommissionEntity(oPeriodEntity.EntityId)
                                PopulateToleranceFormItem(Me.txtBrokerMakupEntity3, oCommEntity.EntityName)
                            Else
                                strPayeeType = LookupListNew.GetDescriptionFromId(LookupListNew.LK_PAYEE_TYPE, oPeriodEntity.PayeeTypeId)
                                PopulateToleranceFormItem(Me.txtBrokerMakupEntity3, strPayeeType)
                            End If

                            PopulateToleranceFormItem(Me.txtBrokerMakupPct3, nInitValue)
                            PopulateToleranceFormItem(Me.txtBrokerCommPct3, nInitValue)
                        Case 4
                            If Not oPeriodEntity.EntityId.Equals(Guid.Empty) Then
                                oCommEntity = New CommissionEntity(oPeriodEntity.EntityId)
                                PopulateToleranceFormItem(Me.txtBrokerMakupEntity4, oCommEntity.EntityName)
                            Else
                                strPayeeType = LookupListNew.GetDescriptionFromId(LookupListNew.LK_PAYEE_TYPE, oPeriodEntity.PayeeTypeId)
                                PopulateToleranceFormItem(Me.txtBrokerMakupEntity4, strPayeeType)
                            End If
                            PopulateToleranceFormItem(Me.txtBrokerMakupPct4, nInitValue)
                            PopulateToleranceFormItem(Me.txtBrokerCommPct4, nInitValue)
                        Case 5
                            If Not oPeriodEntity.EntityId.Equals(Guid.Empty) Then
                                oCommEntity = New CommissionEntity(oPeriodEntity.EntityId)
                                PopulateToleranceFormItem(Me.txtBrokerMakupEntity5, oCommEntity.EntityName)
                            Else
                                strPayeeType = LookupListNew.GetDescriptionFromId(LookupListNew.LK_PAYEE_TYPE, oPeriodEntity.PayeeTypeId)
                                PopulateToleranceFormItem(Me.txtBrokerMakupEntity5, strPayeeType)
                            End If
                            PopulateToleranceFormItem(Me.txtBrokerMakupPct5, nInitValue)
                            PopulateToleranceFormItem(Me.txtBrokerCommPct5, nInitValue)
                    End Select
                Next
            Else
                For i = 0 To oDataView.Count - 1
                    If (strPositioncheck.IndexOf(oDataView(i).Item(4).ToString) < 0) Then
                        Select Case oDataView(i).Item(4).ToString
                            Case "1"
                                strPositioncheck = strPositioncheck & ",1"
                                'REQ-5773
                                Me.txtBrokerMakupPct.Text = Me.GetAmountFormattedPercentString(oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_MARKUP_PERCENT))
                                Me.txtBrokerCommPct.Text = Me.GetAmountFormattedPercentString(oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMMISSION_PERCENT))
                                If oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMM_ENTITY_NAME).ToString <> "" Then
                                    PopulateToleranceFormItem(Me.txtBrokerMakupEntity, oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMM_ENTITY_NAME))
                                Else
                                    If Not oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_PAYEE_TYPE_ID) Is DBNull.Value Then
                                        strPayeeType = LookupListNew.GetDescriptionFromId(LookupListNew.LK_PAYEE_TYPE, GuidControl.ByteArrayToGuid(oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_PAYEE_TYPE_ID)))
                                        PopulateToleranceFormItem(Me.txtBrokerMakupEntity, strPayeeType)
                                    End If
                                End If
                                PopulateToleranceFormItem(Me.AsCommId1, oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_ASSOCIATE_COMMISSIONS_ID))
                                oMakupTotal += CType(oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_MARKUP_PERCENT), Decimal)
                                oBrokerComm += CType(oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMMISSION_PERCENT), Decimal)
                            Case "2"
                                strPositioncheck = strPositioncheck & ",2"
                                Me.txtBrokerMakupPct2.Text = Me.GetAmountFormattedPercentString(oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_MARKUP_PERCENT))
                                Me.txtBrokerCommPct2.Text = Me.GetAmountFormattedPercentString(oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMMISSION_PERCENT))
                                If oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMM_ENTITY_NAME).ToString <> "" Then
                                    PopulateToleranceFormItem(Me.txtBrokerMakupEntity2, oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMM_ENTITY_NAME))
                                Else
                                    If Not oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_PAYEE_TYPE_ID) Is DBNull.Value Then
                                        strPayeeType = LookupListNew.GetDescriptionFromId(LookupListNew.LK_PAYEE_TYPE, GuidControl.ByteArrayToGuid(oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_PAYEE_TYPE_ID)))
                                        PopulateToleranceFormItem(Me.txtBrokerMakupEntity2, strPayeeType)
                                    End If
                                End If
                                PopulateToleranceFormItem(Me.AsCommId2, oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_ASSOCIATE_COMMISSIONS_ID))
                                oMakupTotal += CType(oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_MARKUP_PERCENT), Decimal)
                                oBrokerComm += CType(oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMMISSION_PERCENT), Decimal)
                            Case "3"
                                strPositioncheck = strPositioncheck & ",3"
                                Me.txtBrokerMakupPct3.Text = Me.GetAmountFormattedPercentString(oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_MARKUP_PERCENT))
                                Me.txtBrokerCommPct3.Text = Me.GetAmountFormattedPercentString(oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMMISSION_PERCENT))
                                If oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMM_ENTITY_NAME).ToString <> "" Then
                                    PopulateToleranceFormItem(Me.txtBrokerMakupEntity3, oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMM_ENTITY_NAME))
                                Else
                                    If Not oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_PAYEE_TYPE_ID) Is DBNull.Value Then
                                        strPayeeType = LookupListNew.GetDescriptionFromId(LookupListNew.LK_PAYEE_TYPE, GuidControl.ByteArrayToGuid(oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_PAYEE_TYPE_ID)))
                                        PopulateToleranceFormItem(Me.txtBrokerMakupEntity3, strPayeeType)
                                    End If
                                End If
                                PopulateToleranceFormItem(Me.AsCommId3, oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_ASSOCIATE_COMMISSIONS_ID))
                                oMakupTotal += CType(oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_MARKUP_PERCENT), Decimal)
                                oBrokerComm += CType(oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMMISSION_PERCENT), Decimal)
                            Case "4"
                                strPositioncheck = strPositioncheck & ",4"
                                Me.txtBrokerMakupPct4.Text = Me.GetAmountFormattedPercentString(oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_MARKUP_PERCENT))
                                Me.txtBrokerCommPct4.Text = Me.GetAmountFormattedPercentString(oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMMISSION_PERCENT))
                                If oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMM_ENTITY_NAME).ToString <> "" Then
                                    PopulateToleranceFormItem(Me.txtBrokerMakupEntity4, oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMM_ENTITY_NAME))
                                Else
                                    If Not oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_PAYEE_TYPE_ID) Is DBNull.Value Then
                                        strPayeeType = LookupListNew.GetDescriptionFromId(LookupListNew.LK_PAYEE_TYPE, GuidControl.ByteArrayToGuid(oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_PAYEE_TYPE_ID)))
                                        PopulateToleranceFormItem(Me.txtBrokerMakupEntity4, strPayeeType)
                                    End If
                                End If
                                PopulateToleranceFormItem(Me.AsCommId4, oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_ASSOCIATE_COMMISSIONS_ID))
                                oMakupTotal += CType(oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_MARKUP_PERCENT), Decimal)
                                oBrokerComm += CType(oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMMISSION_PERCENT), Decimal)
                            Case "5"
                                strPositioncheck = strPositioncheck & ",5"
                                Me.txtBrokerMakupPct5.Text = Me.GetAmountFormattedPercentString(oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_MARKUP_PERCENT))
                                Me.txtBrokerCommPct5.Text = Me.GetAmountFormattedPercentString(oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMMISSION_PERCENT))
                                If oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMM_ENTITY_NAME).ToString <> "" Then
                                    PopulateToleranceFormItem(Me.txtBrokerMakupEntity5, oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMM_ENTITY_NAME))
                                Else
                                    If Not oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_PAYEE_TYPE_ID) Is DBNull.Value Then
                                        strPayeeType = LookupListNew.GetDescriptionFromId(LookupListNew.LK_PAYEE_TYPE, GuidControl.ByteArrayToGuid(oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_PAYEE_TYPE_ID)))
                                        PopulateToleranceFormItem(Me.txtBrokerMakupEntity5, strPayeeType)
                                    End If
                                End If
                                PopulateToleranceFormItem(Me.AsCommId5, oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_ASSOCIATE_COMMISSIONS_ID))
                                oMakupTotal += CType(oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_MARKUP_PERCENT), Decimal)
                                oBrokerComm += CType(oDataView(i)(AssociateCommissions.SearchDV.COL_NAME_COMMISSION_PERCENT), Decimal)
                        End Select
                    End If
                Next
            End If

            'Me.BindBOPropertyToLabel(oAssocCommission, MARKUP_TOTAL, lblTotal)
            'Me.BindBOPropertyToLabel(oAssocCommission, COMMISSION_TOTAL, lblTotal)
            PopulateToleranceFormItem(Me.txtCommPctTotal, oBrokerComm)
            PopulateToleranceFormItem(Me.txtBrokerPctTotal, oMakupTotal)
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
                    .Validate()
                End If
            End With

            PopulateAssocCommBOFromForm()
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
                If Me.State.IsPeriodNew = True Then Return ' We can not have Tolerances if the Period is new

                EnableToleranceGrid(True)
                oDataView = GetToleranceDataView()
                Me.State.searchDV = oDataView
                BasePopulateGrid(Me.Grid, Me.State.searchDV, Me.State.moCommissionToleranceId, oAction)
                EnableRestrictMarkup(GetRestrictMarkup(), oDataView.Count)
                ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Me.Grid)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub
        Private Sub PopulatePayeeType()
            'Dim i As Integer
            'Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            'Dim dvPayeeType As DataView = LookupListNew.GetPayeeTypeLookupList(langId)
            'Dim dvPayeeType1 As DataView = dvPayeeType
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
            Me.cboPayeeType1.Populate(FilteredPayeeTypeList, New PopulateOptions() With
            {
                .AddBlankItem = False
            })

            'Me.BindListControlToDataView(Me.cboPayeeType2, dvPayeeType, , , False)
            Me.cboPayeeType2.Populate(FilteredPayeeTypeList, New PopulateOptions() With
            {
                .AddBlankItem = False
            })

            'Me.BindListControlToDataView(Me.cboPayeeType3, dvPayeeType, , , False)
            Me.cboPayeeType3.Populate(FilteredPayeeTypeList, New PopulateOptions() With
            {
                .AddBlankItem = False
            })

            'Me.BindListControlToDataView(Me.cboPayeeType4, dvPayeeType, , , False)
            Me.cboPayeeType4.Populate(FilteredPayeeTypeList, New PopulateOptions() With
            {
                .AddBlankItem = False
            })

            'Me.BindListControlToDataView(Me.cboPayeeType5, dvPayeeType, , , False)
            Me.cboPayeeType5.Populate(FilteredPayeeTypeList, New PopulateOptions() With
            {
                .AddBlankItem = False
            })

            'dvPayeeType1.RowFilter += "and code='" & Payee_Type_Comm_Entity & "'"
            FilteredPayeeType1List = (From lst In PayeeTypeList
                                      Where lst.Code = Payee_Type_Comm_Entity
                                      Select lst).ToArray()

            'If dvPayeeType1.Count = 1 Then
            '    Me.litScriptVars.Text += "var commEntity = '" + GuidControl.ByteArrayToGuid(dvPayeeType1.Item(0)(LookupListNew.COL_ID_NAME)).ToString + "';"
            'End If

            If FilteredPayeeType1List.Count = 1 Then
                Me.litScriptVars.Text += "var commEntity = '" + FilteredPayeeType1List.First().ListItemId.ToString + "';"
            End If

            If Me.State.IsPeriodNew = True Then
                Dim FirstPayeeType As String
                FirstPayeeType = FilteredPayeeType1List.First().ListItemId.ToString()
                'BindSelectItem(GuidControl.ByteArrayToGuid(dvPayeeType1.Item(0)(LookupListNew.COL_ID_NAME)).ToString, cboPayeeType1)
                'BindSelectItem(GuidControl.ByteArrayToGuid(dvPayeeType1.Item(0)(LookupListNew.COL_ID_NAME)).ToString, cboPayeeType2)
                'BindSelectItem(GuidControl.ByteArrayToGuid(dvPayeeType1.Item(0)(LookupListNew.COL_ID_NAME)).ToString, cboPayeeType3)
                'BindSelectItem(GuidControl.ByteArrayToGuid(dvPayeeType1.Item(0)(LookupListNew.COL_ID_NAME)).ToString, cboPayeeType4)
                'BindSelectItem(GuidControl.ByteArrayToGuid(dvPayeeType1.Item(0)(LookupListNew.COL_ID_NAME)).ToString, cboPayeeType5)
                BindSelectItem(FirstPayeeType, cboPayeeType1)
                BindSelectItem(FirstPayeeType, cboPayeeType2)
                BindSelectItem(FirstPayeeType, cboPayeeType3)
                BindSelectItem(FirstPayeeType, cboPayeeType4)
                BindSelectItem(FirstPayeeType, cboPayeeType5)
            End If
        End Sub
        Private Sub PopulatePeriodEntity()
            'Dim i As Integer
            'Dim oDataView As DataView
            'Dim oPeriodEntity As CommissionPeriodEntity
            'oDataView = LookupListNew.GetCommEntityLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            Dim CommEntityList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="CommEntityByCompanyGroup", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=listcontext)

            'Me.BindListControlToDataView(cboPeriodEntity1, oDataView, , , True)
            Me.cboPeriodEntity1.Populate(CommEntityList, New PopulateOptions() With
            {
                .AddBlankItem = True
            })

            'Me.BindListControlToDataView(cboPeriodEntity2, oDataView, , , True)
            Me.cboPeriodEntity2.Populate(CommEntityList, New PopulateOptions() With
            {
                .AddBlankItem = True
            })

            'Me.BindListControlToDataView(cboPeriodEntity3, oDataView, , , True)
            Me.cboPeriodEntity3.Populate(CommEntityList, New PopulateOptions() With
            {
                .AddBlankItem = True
            })

            'Me.BindListControlToDataView(cboPeriodEntity4, oDataView, , , True)
            Me.cboPeriodEntity4.Populate(CommEntityList, New PopulateOptions() With
            {
                .AddBlankItem = True
            })

            'Me.BindListControlToDataView(cboPeriodEntity5, oDataView, , , True)
            Me.cboPeriodEntity5.Populate(CommEntityList, New PopulateOptions() With
            {
                .AddBlankItem = True
            })

            If Me.State.IsPeriodNew = True Then
                'If oDataView.Count > 0 Then
                If CommEntityList.Count > 0 Then
                    Me.State.MyBo.AttachPeriodEntity(Me.GetSelectedItem(cboPeriodEntity1), 1, Me.GetSelectedItem(cboPayeeType1))
                    Me.State.MyBo.AttachPeriodEntity(Me.GetSelectedItem(cboPeriodEntity2), 2, Me.GetSelectedItem(cboPayeeType2))
                    Me.State.MyBo.AttachPeriodEntity(Me.GetSelectedItem(cboPeriodEntity3), 3, Me.GetSelectedItem(cboPayeeType3))
                    Me.State.MyBo.AttachPeriodEntity(Me.GetSelectedItem(cboPeriodEntity4), 4, Me.GetSelectedItem(cboPayeeType4))
                    Me.State.MyBo.AttachPeriodEntity(Me.GetSelectedItem(cboPeriodEntity5), 5, Me.GetSelectedItem(cboPayeeType5))
                End If
            Else
                Dim PayeeTypeCode As String
                For Each oPeriodEntity As CommissionPeriodEntity In Me.State.MyBo.AssociatedCommPeriodEntity
                    Select Case oPeriodEntity.Position
                        Case 1
                            BindSelectItem(oPeriodEntity.EntityId.ToString, cboPeriodEntity1)
                            BindSelectItem(oPeriodEntity.PayeeTypeId.ToString, cboPayeeType1)

                            PayeeTypeCode = LookupListNew.GetCodeFromId(LookupListNew.LK_PAYEE_TYPE, Me.GetSelectedItem(cboPayeeType1))
                            If PayeeTypeCode <> Payee_Type_Comm_Entity Then
                                ControlMgr.SetEnableControl(Me, cboPeriodEntity1, False)
                            Else
                                ControlMgr.SetEnableControl(Me, cboPeriodEntity1, True)
                            End If
                        Case 2
                            BindSelectItem(oPeriodEntity.EntityId.ToString, cboPeriodEntity2)
                            BindSelectItem(oPeriodEntity.PayeeTypeId.ToString, cboPayeeType2)
                            PayeeTypeCode = LookupListNew.GetCodeFromId(LookupListNew.LK_PAYEE_TYPE, Me.GetSelectedItem(cboPayeeType2))
                            If PayeeTypeCode <> Payee_Type_Comm_Entity Then
                                ControlMgr.SetEnableControl(Me, cboPeriodEntity2, False)
                            Else
                                ControlMgr.SetEnableControl(Me, cboPeriodEntity2, True)
                            End If
                        Case 3
                            BindSelectItem(oPeriodEntity.EntityId.ToString, cboPeriodEntity3)
                            BindSelectItem(oPeriodEntity.PayeeTypeId.ToString, cboPayeeType3)
                            PayeeTypeCode = LookupListNew.GetCodeFromId(LookupListNew.LK_PAYEE_TYPE, Me.GetSelectedItem(cboPayeeType3))
                            If PayeeTypeCode <> Payee_Type_Comm_Entity Then
                                ControlMgr.SetEnableControl(Me, cboPeriodEntity3, False)
                            Else
                                ControlMgr.SetEnableControl(Me, cboPeriodEntity3, True)
                            End If
                        Case 4
                            BindSelectItem(oPeriodEntity.EntityId.ToString, cboPeriodEntity4)
                            BindSelectItem(oPeriodEntity.PayeeTypeId.ToString, cboPayeeType4)
                            PayeeTypeCode = LookupListNew.GetCodeFromId(LookupListNew.LK_PAYEE_TYPE, Me.GetSelectedItem(cboPayeeType4))
                            If PayeeTypeCode <> Payee_Type_Comm_Entity Then
                                ControlMgr.SetEnableControl(Me, cboPeriodEntity4, False)
                            Else
                                ControlMgr.SetEnableControl(Me, cboPeriodEntity4, True)
                            End If
                        Case 5
                            BindSelectItem(oPeriodEntity.EntityId.ToString, cboPeriodEntity5)
                            BindSelectItem(oPeriodEntity.PayeeTypeId.ToString, cboPayeeType5)
                            PayeeTypeCode = LookupListNew.GetCodeFromId(LookupListNew.LK_PAYEE_TYPE, Me.GetSelectedItem(cboPayeeType5))
                            If PayeeTypeCode <> Payee_Type_Comm_Entity Then
                                ControlMgr.SetEnableControl(Me, cboPeriodEntity5, False)
                            Else
                                ControlMgr.SetEnableControl(Me, cboPeriodEntity5, True)
                            End If
                    End Select
                Next
            End If
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
            Dim commPeriodEntity As CommissionPeriodEntity
            For Each commPeriodEntity In Me.State.MyBo.AssociatedCommPeriodEntity
                Select Case commPeriodEntity.Position.ToString
                    Case "1"
                        If commPeriodEntity.EntityId <> Me.GetSelectedItem(cboPeriodEntity1) OrElse commPeriodEntity.PayeeTypeId <> Me.GetSelectedItem(cboPayeeType1) Then
                            commPeriodEntity.EntityId = Me.GetSelectedItem(cboPeriodEntity1)
                            commPeriodEntity.PayeeTypeId = Me.GetSelectedItem(cboPayeeType1)
                        End If
                    Case "2"
                        If commPeriodEntity.EntityId <> Me.GetSelectedItem(cboPeriodEntity2) OrElse commPeriodEntity.PayeeTypeId <> Me.GetSelectedItem(cboPayeeType2) Then
                            commPeriodEntity.EntityId = Me.GetSelectedItem(cboPeriodEntity2)
                            commPeriodEntity.PayeeTypeId = Me.GetSelectedItem(cboPayeeType2)
                        End If
                    Case "3"
                        If commPeriodEntity.EntityId <> Me.GetSelectedItem(cboPeriodEntity3) OrElse commPeriodEntity.PayeeTypeId <> Me.GetSelectedItem(cboPayeeType3) Then
                            commPeriodEntity.EntityId = Me.GetSelectedItem(cboPeriodEntity3)
                            commPeriodEntity.PayeeTypeId = Me.GetSelectedItem(cboPayeeType3)
                        End If
                    Case "4"
                        If commPeriodEntity.EntityId <> Me.GetSelectedItem(cboPeriodEntity4) OrElse commPeriodEntity.PayeeTypeId <> Me.GetSelectedItem(cboPayeeType4) Then
                            commPeriodEntity.EntityId = Me.GetSelectedItem(cboPeriodEntity4)
                            commPeriodEntity.PayeeTypeId = Me.GetSelectedItem(cboPayeeType4)
                        End If
                    Case "5"
                        If commPeriodEntity.EntityId <> Me.GetSelectedItem(cboPeriodEntity5) OrElse commPeriodEntity.PayeeTypeId <> Me.GetSelectedItem(cboPayeeType5) Then
                            commPeriodEntity.EntityId = Me.GetSelectedItem(cboPeriodEntity5)
                            commPeriodEntity.PayeeTypeId = Me.GetSelectedItem(cboPayeeType5)
                        End If
                End Select
            Next
        End Sub
        Private Sub PopulateAssocCommBOFromForm()
            Dim oPeriodEntity As CommissionPeriodEntity
            Dim oCommEntity As CommissionEntity
            Dim oAssocComm As AssociateCommissions
            Dim nInitValue As New DecimalType(0)
            Dim totalMarkup As Decimal = 0
            Dim totalComm As Decimal = 0
            Dim oCount As Integer = 0
            Dim strPayeeType As String
            'Me.BindBOPropertyToLabel(oAssocComm, "MarkupPercent", Me.lblMarkup)
            Dim strPositionChecked As String = String.Empty

            For Each oPeriodEntity In Me.State.MyBo.AssociatedCommPeriodEntity
                oCount = oCount + 1
                If (strPositionChecked.IndexOf(oPeriodEntity.Position.ToString) < 0) Then
                    Select Case oPeriodEntity.Position
                        Case 1
                            strPositionChecked = strPositionChecked & ",1"
                            If Me.State.IsToleranceNew Then
                                If Not oPeriodEntity.EntityId.Equals(Guid.Empty) Then
                                    oCommEntity = New CommissionEntity(oPeriodEntity.EntityId)
                                    PopulateToleranceFormItem(Me.txtBrokerMakupEntity, oCommEntity.EntityName)
                                Else
                                    strPayeeType = LookupListNew.GetDescriptionFromId(LookupListNew.LK_PAYEE_TYPE, oPeriodEntity.PayeeTypeId)
                                    PopulateToleranceFormItem(Me.txtBrokerMakupEntity, strPayeeType)
                                End If
                                oAssocComm = Me.State.MyBo.AddAssocComm(Guid.Empty)
                                BindBoAssocCommToLabels(oAssocComm)
                                oAssocComm.CommissionToleranceId = Me.State.moCommissionToleranceId
                                Me.PopulateBOProperty(oAssocComm, "MarkupPercent", Me.txtBrokerMakupPct)
                                Me.PopulateBOProperty(oAssocComm, "CommissionPercent", Me.txtBrokerCommPct)
                                oAssocComm.Position = 1
                                If Me.txtBrokerMakupPct.Text <> "" Then
                                    totalMarkup += oAssocComm.MarkupPercent.Value
                                End If
                                If Me.txtBrokerCommPct.Text <> "" Then
                                    totalComm += oAssocComm.CommissionPercent.Value
                                End If
                                Me.PopulateBOProperty(oAssocComm, COMMISSION_TOTAL, totalComm.ToString)
                                Me.PopulateBOProperty(oAssocComm, MARKUP_TOTAL, totalMarkup.ToString)
                                'oAssocComm.Validate()

                            Else
                                If Me.AsCommId1.Text <> "" Then
                                    oAssocComm = Me.State.MyBo.AddAssocComm(New Guid(Me.AsCommId1.Text))
                                    BindBoAssocCommToLabels(oAssocComm)
                                    Me.PopulateBOProperty(oAssocComm, "MarkupPercent", Me.txtBrokerMakupPct)
                                    Me.PopulateBOProperty(oAssocComm, "CommissionPercent", Me.txtBrokerCommPct)
                                    totalMarkup += oAssocComm.MarkupPercent.Value
                                    totalComm += oAssocComm.CommissionPercent.Value
                                    Me.PopulateBOProperty(oAssocComm, COMMISSION_TOTAL, totalComm.ToString)
                                    Me.PopulateBOProperty(oAssocComm, MARKUP_TOTAL, totalMarkup.ToString)
                                    'oAssocComm.Validate()
                                Else
                                    Me.ChangeControlEnabledProperty(Me.txtBrokerMakupPct, False)
                                    Me.ChangeControlEnabledProperty(Me.txtBrokerCommPct, False)
                                End If
                            End If
                        Case 2
                            strPositionChecked = strPositionChecked & ",2"
                            If Me.State.IsToleranceNew Then
                                If Not oPeriodEntity.EntityId.Equals(Guid.Empty) Then
                                    oCommEntity = New CommissionEntity(oPeriodEntity.EntityId)
                                    PopulateToleranceFormItem(Me.txtBrokerMakupEntity2, oCommEntity.EntityName)
                                Else
                                    strPayeeType = LookupListNew.GetDescriptionFromId(LookupListNew.LK_PAYEE_TYPE, oPeriodEntity.PayeeTypeId)
                                    PopulateToleranceFormItem(Me.txtBrokerMakupEntity2, strPayeeType)
                                End If
                                oAssocComm = Me.State.MyBo.AddAssocComm(Guid.Empty)
                                BindBoAssocCommToLabels(oAssocComm)
                                oAssocComm.CommissionToleranceId = Me.State.moCommissionToleranceId
                                Me.PopulateBOProperty(oAssocComm, "MarkupPercent", Me.txtBrokerMakupPct2)
                                Me.PopulateBOProperty(oAssocComm, "CommissionPercent", Me.txtBrokerCommPct2)
                                oAssocComm.Position = 2
                                If Me.txtBrokerMakupPct2.Text <> "" Then
                                    totalMarkup += oAssocComm.MarkupPercent.Value
                                End If
                                If Me.txtBrokerCommPct2.Text <> "" Then
                                    totalComm += oAssocComm.CommissionPercent.Value
                                End If
                                Me.PopulateBOProperty(oAssocComm, COMMISSION_TOTAL, totalComm.ToString)
                                Me.PopulateBOProperty(oAssocComm, MARKUP_TOTAL, totalMarkup.ToString)
                                'oAssocComm.Validate()

                            Else
                                If Me.AsCommId2.Text <> "" Then
                                    oAssocComm = Me.State.MyBo.AddAssocComm(New Guid(Me.AsCommId2.Text))
                                    BindBoAssocCommToLabels(oAssocComm)
                                    Me.PopulateBOProperty(oAssocComm, "MarkupPercent", Me.txtBrokerMakupPct2)
                                    Me.PopulateBOProperty(oAssocComm, "CommissionPercent", Me.txtBrokerCommPct2)
                                    totalMarkup += oAssocComm.MarkupPercent.Value
                                    totalComm += oAssocComm.CommissionPercent.Value
                                    Me.PopulateBOProperty(oAssocComm, COMMISSION_TOTAL, totalComm.ToString)
                                    Me.PopulateBOProperty(oAssocComm, MARKUP_TOTAL, totalMarkup.ToString)
                                    'oAssocComm.Validate()
                                Else
                                    Me.ChangeControlEnabledProperty(Me.txtBrokerMakupPct2, False)
                                    Me.ChangeControlEnabledProperty(Me.txtBrokerCommPct2, False)
                                End If

                            End If
                        Case 3
                            strPositionChecked = strPositionChecked & ",3"
                            If Me.State.IsToleranceNew Then
                                If Not oPeriodEntity.EntityId.Equals(Guid.Empty) Then
                                    oCommEntity = New CommissionEntity(oPeriodEntity.EntityId)
                                    PopulateToleranceFormItem(Me.txtBrokerMakupEntity3, oCommEntity.EntityName)
                                Else
                                    strPayeeType = LookupListNew.GetDescriptionFromId(LookupListNew.LK_PAYEE_TYPE, oPeriodEntity.PayeeTypeId)
                                    PopulateToleranceFormItem(Me.txtBrokerMakupEntity3, strPayeeType)
                                End If
                                oAssocComm = Me.State.MyBo.AddAssocComm(Guid.Empty)
                                BindBoAssocCommToLabels(oAssocComm)
                                oAssocComm.CommissionToleranceId = Me.State.moCommissionToleranceId
                                Me.PopulateBOProperty(oAssocComm, "MarkupPercent", Me.txtBrokerMakupPct3)
                                Me.PopulateBOProperty(oAssocComm, "CommissionPercent", Me.txtBrokerCommPct3)
                                oAssocComm.Position = 3
                                If Me.txtBrokerMakupPct3.Text <> "" Then
                                    totalMarkup += oAssocComm.MarkupPercent.Value
                                End If
                                If Me.txtBrokerCommPct3.Text <> "" Then
                                    totalComm += oAssocComm.CommissionPercent.Value
                                End If
                                Me.PopulateBOProperty(oAssocComm, COMMISSION_TOTAL, totalComm.ToString)
                                Me.PopulateBOProperty(oAssocComm, MARKUP_TOTAL, totalMarkup.ToString)
                                'oAssocComm.Validate()

                            Else
                                If Me.AsCommId3.Text <> "" Then
                                    oAssocComm = Me.State.MyBo.AddAssocComm(New Guid(Me.AsCommId3.Text))
                                    BindBoAssocCommToLabels(oAssocComm)
                                    Me.PopulateBOProperty(oAssocComm, "MarkupPercent", Me.txtBrokerMakupPct3)
                                    Me.PopulateBOProperty(oAssocComm, "CommissionPercent", Me.txtBrokerCommPct3)
                                    totalMarkup += oAssocComm.MarkupPercent.Value
                                    totalComm += oAssocComm.CommissionPercent.Value
                                    Me.PopulateBOProperty(oAssocComm, COMMISSION_TOTAL, totalComm.ToString)
                                    Me.PopulateBOProperty(oAssocComm, MARKUP_TOTAL, totalMarkup.ToString)
                                    'oAssocComm.Validate()
                                Else
                                    Me.ChangeControlEnabledProperty(Me.txtBrokerMakupPct3, False)
                                    Me.ChangeControlEnabledProperty(Me.txtBrokerCommPct3, False)
                                End If
                            End If
                        Case 4
                            strPositionChecked = strPositionChecked & ",4"
                            If Me.State.IsToleranceNew Then
                                If Not oPeriodEntity.EntityId.Equals(Guid.Empty) Then
                                    oCommEntity = New CommissionEntity(oPeriodEntity.EntityId)
                                    PopulateToleranceFormItem(Me.txtBrokerMakupEntity4, oCommEntity.EntityName)
                                Else
                                    strPayeeType = LookupListNew.GetDescriptionFromId(LookupListNew.LK_PAYEE_TYPE, oPeriodEntity.PayeeTypeId)
                                    PopulateToleranceFormItem(Me.txtBrokerMakupEntity4, strPayeeType)
                                End If
                                oAssocComm = Me.State.MyBo.AddAssocComm(Guid.Empty)
                                BindBoAssocCommToLabels(oAssocComm)
                                oAssocComm.CommissionToleranceId = Me.State.moCommissionToleranceId
                                Me.PopulateBOProperty(oAssocComm, "MarkupPercent", Me.txtBrokerMakupPct4)
                                Me.PopulateBOProperty(oAssocComm, "CommissionPercent", Me.txtBrokerCommPct4)
                                oAssocComm.Position = 4
                                If Me.txtBrokerMakupPct4.Text <> "" Then
                                    totalMarkup += oAssocComm.MarkupPercent.Value
                                End If
                                If Me.txtBrokerCommPct4.Text <> "" Then
                                    totalComm += oAssocComm.CommissionPercent.Value
                                End If
                                Me.PopulateBOProperty(oAssocComm, COMMISSION_TOTAL, totalComm.ToString)
                                Me.PopulateBOProperty(oAssocComm, MARKUP_TOTAL, totalMarkup.ToString)
                                'oAssocComm.Validate()

                            Else
                                If Me.AsCommId4.Text <> "" Then
                                    oAssocComm = Me.State.MyBo.AddAssocComm(New Guid(Me.AsCommId4.Text))
                                    BindBoAssocCommToLabels(oAssocComm)
                                    Me.PopulateBOProperty(oAssocComm, "MarkupPercent", Me.txtBrokerMakupPct4)
                                    Me.PopulateBOProperty(oAssocComm, "CommissionPercent", Me.txtBrokerCommPct4)
                                    totalMarkup += oAssocComm.MarkupPercent.Value
                                    totalComm += oAssocComm.CommissionPercent.Value
                                    Me.PopulateBOProperty(oAssocComm, COMMISSION_TOTAL, totalComm.ToString)
                                    Me.PopulateBOProperty(oAssocComm, MARKUP_TOTAL, totalMarkup.ToString)
                                    'oAssocComm.Validate()
                                Else
                                    Me.ChangeControlEnabledProperty(Me.txtBrokerMakupPct4, False)
                                    Me.ChangeControlEnabledProperty(Me.txtBrokerCommPct4, False)
                                End If
                            End If
                        Case 5
                            strPositionChecked = strPositionChecked & ",5"
                            If Me.State.IsToleranceNew Then
                                If Not oPeriodEntity.EntityId.Equals(Guid.Empty) Then
                                    oCommEntity = New CommissionEntity(oPeriodEntity.EntityId)
                                    PopulateToleranceFormItem(Me.txtBrokerMakupEntity5, oCommEntity.EntityName)
                                Else
                                    strPayeeType = LookupListNew.GetDescriptionFromId(LookupListNew.LK_PAYEE_TYPE, oPeriodEntity.PayeeTypeId)
                                    PopulateToleranceFormItem(Me.txtBrokerMakupEntity5, strPayeeType)
                                End If
                                oAssocComm = Me.State.MyBo.AddAssocComm(Guid.Empty)
                                BindBoAssocCommToLabels(oAssocComm)
                                oAssocComm.CommissionToleranceId = Me.State.moCommissionToleranceId
                                Me.PopulateBOProperty(oAssocComm, "MarkupPercent", Me.txtBrokerMakupPct5)
                                Me.PopulateBOProperty(oAssocComm, "CommissionPercent", Me.txtBrokerCommPct5)
                                oAssocComm.Position = 5
                                If Me.txtBrokerMakupPct5.Text <> "" Then
                                    totalMarkup += oAssocComm.MarkupPercent.Value
                                End If
                                If Me.txtBrokerCommPct5.Text <> "" Then
                                    totalComm += oAssocComm.CommissionPercent.Value
                                End If
                                Me.PopulateBOProperty(oAssocComm, COMMISSION_TOTAL, totalComm.ToString)
                                Me.PopulateBOProperty(oAssocComm, MARKUP_TOTAL, totalMarkup.ToString)
                                'oAssocComm.Validate()

                            Else
                                If Me.AsCommId5.Text <> "" Then
                                    oAssocComm = Me.State.MyBo.AddAssocComm(New Guid(Me.AsCommId5.Text))
                                    BindBoAssocCommToLabels(oAssocComm)
                                    Me.PopulateBOProperty(oAssocComm, "MarkupPercent", Me.txtBrokerMakupPct5)
                                    Me.PopulateBOProperty(oAssocComm, "CommissionPercent", Me.txtBrokerCommPct5)
                                    totalMarkup += oAssocComm.MarkupPercent.Value
                                    totalComm += oAssocComm.CommissionPercent.Value
                                    Me.PopulateBOProperty(oAssocComm, COMMISSION_TOTAL, totalComm.ToString)
                                    Me.PopulateBOProperty(oAssocComm, MARKUP_TOTAL, totalMarkup.ToString)
                                    'oAssocComm.Validate()
                                Else
                                    Me.ChangeControlEnabledProperty(Me.txtBrokerMakupPct5, False)
                                    Me.ChangeControlEnabledProperty(Me.txtBrokerCommPct5, False)
                                End If
                            End If
                    End Select
                End If
                If Me.ErrCollection.Count > 0 Then
                    Throw New PopulateBOErrorException
                ElseIf oCount > 4 Then
                    PopulateToleranceFormItem(Me.txtCommPctTotal, totalComm)
                    PopulateToleranceFormItem(Me.txtBrokerPctTotal, totalMarkup)
                    If Not oAssocComm Is Nothing Then
                        Me.State.moAssocComm = oAssocComm
                        oAssocComm.Validate()
                    End If
                End If
                If Not oAssocComm Is Nothing Then
                    Me.State.moIsAssocCommDirty = Me.State.moIsAssocCommDirty Or oAssocComm.IsDirty
                End If
            Next

        End Sub

        Private Sub EnableDisablePeriodEntity(ByVal oEnable As Boolean)

            'ControlMgr.SetVisibleControl(Me, btnEntitySave, oEnable)
            'ControlMgr.SetVisibleControl(Me, btnEntityUndo, oEnable)
            Me.ChangeControlEnabledProperty(Me.btnEntitySave, oEnable)
            Me.ChangeControlEnabledProperty(Me.btnEntityUndo, oEnable)

            Me.ChangeControlEnabledProperty(Me.cboPeriodEntity1, oEnable)
            Me.ChangeControlEnabledProperty(Me.cboPeriodEntity2, oEnable)
            Me.ChangeControlEnabledProperty(Me.cboPeriodEntity3, oEnable)
            Me.ChangeControlEnabledProperty(Me.cboPeriodEntity4, oEnable)
            Me.ChangeControlEnabledProperty(Me.cboPeriodEntity5, oEnable)

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

    End Class
End Namespace
