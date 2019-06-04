Imports Assurant.ElitaPlus.DALObjects
Imports System.Globalization
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common

Namespace Tables

    Partial Class CompensationPlanForm
        Inherits ElitaPlusSearchPage
        ' Inherits System.Web.UI.Page
#Region "Page State"

#Region "MyState"

        Class MyState
            Public MyBo As CompensationPlan
            Public CommissionPlanId As Guid = Guid.Empty
            Public CommPlanExtId As Guid = Guid.Empty
            Public moInError As Boolean = False
            Public LastErrMsg As String
            Public IsPlanNew As Boolean = False
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public HasDataChanged As Boolean = False
            Public searchDV As DataView = Nothing
            Public searchcpDV As CommPlanExtract.CommPlanExtractSearchDV = Nothing

            Public IsGridVisible As Boolean
            Public PageIndex As Integer = 0
            Private mnPageIndex As Integer = 0
            Private msPageSort As String
            Private mnPageSize As Integer = DEFAULT_PAGE_SIZE
            Public SortExpression As String = "code"

#Region "State-Properties"

            Public Property PageSize() As Integer
                Get
                    Return mnPageSize
                End Get
                Set(ByVal Value As Integer)
                    mnPageSize = Value
                End Set
            End Property

            Public Property PageSort() As String
                Get
                    Return msPageSort
                End Get
                Set(ByVal Value As String)
                    msPageSort = Value
                End Set
            End Property

            Public Property SearchCommPlanExtDataView() As CommPlanExtract.CommPlanExtractSearchDV
                Get
                    Return searchcpDV
                End Get
                Set(ByVal Value As CommPlanExtract.CommPlanExtractSearchDV)
                    searchcpDV = Value
                End Set
            End Property
#End Region

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


#Region "Page Return"

        Private IsReturningFromChild As Boolean = False

        Public Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
            Try
                Me.MenuEnabled = True
                Me.IsReturningFromChild = True
                Dim retObj As CommPlanExtractForm.ReturnType = CType(ReturnPar, CommPlanExtractForm.ReturnType)
                Me.State.HasDataChanged = retObj.HasDataChanged
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If Not retObj Is Nothing Then
                            If Not retObj.moCommissPlanId.Equals(Guid.Empty) Then
                                Me.State.CommissionPlanId = retObj.moCommissPlanId
                            Else
                                Me.State.CommissionPlanId = Session(SessionCommPlanId)
                            End If
                            Me.State.IsGridVisible = True
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        Me.AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
                        Me.State.CommissionPlanId = Guid.Empty
                    Case Else
                        Me.State.CommissionPlanId = Guid.Empty
                End Select
                moDataGrid.CurrentPageIndex = Me.State.PageIndex
                cboPageSize.SelectedValue = CType(Me.State.PageSize, String)
                moDataGrid.PageSize = Me.State.PageSize
                ControlMgr.SetVisibleControl(Me, trPageSize, moDataGrid.Visible)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public moComissionPlanId As Guid
            Public BoChanged As Boolean
            Public Sub New(ByVal LastOp As DetailPageCommand, ByVal oCommiPlanId As Guid, ByVal boChanged As Boolean)
                Me.LastOperation = LastOp
                Me.moComissionPlanId = oCommiPlanId
                Me.BoChanged = boChanged
            End Sub
        End Class

#End Region

        Private Sub SetStateProperties()

            If (Me.State.CommissionPlanId.Equals(Guid.Empty)) Then
                Me.State.CommissionPlanId = CType(Me.CallingParameters, Guid)
                Session(SessionCommPlanId) = Me.State.CommissionPlanId
            End If

            If Me.State.CommissionPlanId.Equals(Guid.Empty) Then
                Me.State.IsPlanNew = True
                ClearPlan()
                SetPlanButtonsState(True)
                PopulatePlan()
                TheDealerControl.ChangeEnabledControlProperty(True)
            Else
                Me.State.IsPlanNew = False
                SetPlanButtonsState(False)
                PopulatePlan()
                TheDealerControl.ChangeEnabledControlProperty(False)
            End If
        End Sub

#End Region

#Region "Constants"
        Private Const COMPENSATION_FORM001 As String = "COMPENSATION_FORM001" ' Compensation Plan Exception
        Private Const COMPENSATION_CODE_FORM002 As String = "COMPENSATION_CODE_FORM002" ' Compensation Plan Code Field Exception
        Public Const URL As String = "CompensationPlanForm.aspx"
        Public Const NOTHING_SELECTED As Integer = 0
        Public Const COMMISSION_PLAN_ID_PROPERTY As String = "CompensationPlanId"
        Public Const DEALER_ID_PROPERTY As String = "DealerId"
        Public Const EFFECTIVE_DATE_PROPERTY As String = "EffectiveDate"
        Public Const EXPIRATION_DATE_PROPERTY As String = "ExpirationDate"
        Public Const CODE_PROPERTY As String = "Code"
        Public Const DESCRIPTION_PROPERTY As String = "Description"
        Private Const LABEL_DEALER As String = "DEALER"

        Private Const SessionCommPlanId As String = "SessionCommPlanId"

#End Region

#Region "Variables"

        'Private moPlan As Commpensationplan
        Private moExpirationData As CompensationPlanData
        Private moMatchCount As CompensationPlanData

#End Region

#Region "Properties"

        Private ReadOnly Property ThePlan() As CompensationPlan
            Get
                If Me.State.MyBo Is Nothing Then
                    If Me.State.IsPlanNew = True Then
                        ' For creating, inserting
                        Me.State.MyBo = New CompensationPlan
                        Me.State.CommissionPlanId = Me.State.MyBo.Id
                    Else
                        ' For updating, deleting
                        Me.State.MyBo = New CompensationPlan(Me.State.CommissionPlanId)
                    End If
                End If
                BindBoPropertiesToLabels(Me.State.MyBo)
                Return Me.State.MyBo
            End Get
        End Property

        Private ReadOnly Property ExpirationCount() As Integer
            Get
                If moExpirationData Is Nothing Then
                    moExpirationData = New CompensationPlanData
                    moExpirationData.dealerId = TheDealerControl.SelectedGuid 'Me.GetSelectedItem(moDealerDrop_WRITE)
                End If
                Return ThePlan.ExpirationCount(moExpirationData)
            End Get
        End Property

        Private ReadOnly Property MaxExpiration() As Date
            Get
                If moExpirationData Is Nothing Then
                    moExpirationData = New CompensationPlanData
                    moExpirationData.dealerId = TheDealerControl.SelectedGuid
                End If
                Return ThePlan.MaxExpiration(moExpirationData)
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

        Protected WithEvents moErrorController As ErrorController

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
                    Me.SetStateProperties()
                    Me.SetGridItemStyleColor(moDataGrid)
                    Me.AddControlMsg(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO,
                                                                            Me.MSG_TYPE_CONFIRM, True)
                    Me.AddCalendar(Me.BtnEffectiveDate_WRITE, Me.moEffectiveText_WRITE)
                    Me.AddCalendar(Me.BtnExpirationDate_WRITE, Me.moExpirationText_WRITE)

                    If Me.IsReturningFromChild Then
                        ControlMgr.SetVisibleControl(Me, moDataGrid, Me.State.IsGridVisible)
                        ControlMgr.SetVisibleControl(Me, trPageSize, Me.State.IsGridVisible)
                        If Me.State.IsGridVisible Then
                            Me.PopulateGrid(Me.POPULATE_ACTION_SAVE)
                        End If
                    End If

                Else
                    CheckIfComingFromConfirm()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)
        End Sub

#End Region

#Region "Handlers-Grid"

        Public Property SortDirection() As String
            Get
                Return ViewState("SortDirection").ToString
            End Get
            Set(ByVal value As String)
                ViewState("SortDirection") = value
            End Set
        End Property

        Private Sub moDataGrid_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles moDataGrid.PageIndexChanged
            Try
                moDataGrid.CurrentPageIndex = e.NewPageIndex
                PopulateGrid(POPULATE_ACTION_NO_EDIT)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Public Sub ItemCreated(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Sub ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
            Dim sCommPlanExtId As String
            Dim sCommPlanId As String
            Try
                If e.CommandSource.GetType.Equals(GetType(ImageButton)) Then
                    'this only runs when they click the pencil button for editing.
                    sCommPlanExtId = CType(e.Item.FindControl("comm_plan_extract_id"), Label).Text
                    sCommPlanId = CType(e.Item.FindControl("commission_plan_id"), Label).Text
                    Me.State.CommPlanExtId = Me.GetGuidFromString(sCommPlanExtId)
                    Me.State.CommissionPlanId = Me.GetGuidFromString(sCommPlanId)
                    SetSession()
                    Me.callPage(CommPlanExtractForm.URL, New CommPlanExtractForm.Parameters(Me.State.CommissionPlanId, Me.State.CommPlanExtId))
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moDataGrid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles moDataGrid.SortCommand
            Try
                If Me.State.SortExpression.StartsWith(e.SortExpression) Then
                    If Me.State.SortExpression.EndsWith(" DESC") Then
                        Me.State.SortExpression = e.SortExpression
                    Else
                        Me.State.SortExpression &= " DESC"
                    End If
                Else
                    Me.State.SortExpression = e.SortExpression
                End If
                Me.moDataGrid.CurrentPageIndex = 0
                Me.moDataGrid.SelectedIndex = -1
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub GoBack()
            Dim retType As New CompensationPlanSearchForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back,
                                                                                        Me.State.CommissionPlanId, Me.State.HasDataChanged)
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
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub SavePlanChanges()
            If ApplyPlanChanges() = True Then
                Me.State.HasDataChanged = True
                PopulatePlan()
                Me.State.IsPlanNew = False
                SetPlanButtonsState(False)
            End If
        End Sub

        Private Sub btnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
            Try
                SavePlanChanges()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try
                ClearPlan()
                PopulatePlan()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)

                '    If Not Me.State.IsPlanNew Then
                '        'Reload from the DB
                '        Me.State.MyBo = New CompensationPlan(Me.State.MyBo.Id)
                '    ElseIf Not Me.State.MyBo Is Nothing Then
                '        'It was a new with copy
                '        Me.State.MyBo.Clone(Me.State.MyBo)
                '    Else
                '        CreateNew()
                '        End If
                '        PopulatePlan()
                '        PopulatePlanBOFromForm(Me.State.MyBo)
                '    Me.SetPlanButtonsState(Me.State.IsPlanNew)
                '    EnableDateFields()
            End Try
        End Sub

        Private Sub btnDelete_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click
            Try
                If DeletePlan() = True Then
                    Me.State.HasDataChanged = True
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
            Me.State.CommissionPlanId = Guid.Empty
            Me.State.IsPlanNew = True
            ClearPlan()
            SetPlanButtonsState(True)
            PopulatePlan()
            TheDealerControl.ChangeEnabledControlProperty(True)
            'ControlMgr.SetVisibleControl(Me, moPeriodPanel_WRITE, True)
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
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnNew_CommPlanExtract_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddCommiPlanExt.Click
            Try
                SetSession()
                Me.State.CommPlanExtId = Guid.Empty
                Me.callPage(CommPlanExtractForm.URL, New CommPlanExtractForm.Parameters(Me.State.CommissionPlanId, Me.State.CommPlanExtId))
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CreateNewCopy()
            Dim newObj As New CompensationPlan
            Me.State.MyBo = Nothing
            newObj.Copy(ThePlan)
            Me.State.IsPlanNew = True
            Me.State.MyBo = newObj
            EnableDateFields()
            SetPlanButtonsState(True)
            SetPlanButtonsState2(True)

        End Sub

        Private Sub btnCopy_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy_WRITE.Click
            Try
                If IsDirtyPlanBO() = True Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
                Else
                    CreateNewCopy()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Handlers-Labels"

        Protected Sub BindBoPropertiesToLabels(ByVal oPlan As CompensationPlan)
            Me.BindBOPropertyToLabel(oPlan, DEALER_ID_PROPERTY, Me.TheDealerControl.CaptionLabel)
            Me.BindBOPropertyToLabel(oPlan, EFFECTIVE_DATE_PROPERTY, moEffectiveLabel)
            Me.BindBOPropertyToLabel(oPlan, CODE_PROPERTY, moCodeLabel)
            Me.BindBOPropertyToLabel(oPlan, DESCRIPTION_PROPERTY, moDescriptionLabel)
            Me.BindBOPropertyToLabel(oPlan, EFFECTIVE_DATE_PROPERTY, moEffectiveLabel)
            Me.BindBOPropertyToLabel(oPlan, EXPIRATION_DATE_PROPERTY, moExpirationLabel)

        End Sub

        Public Sub ClearLabelsErrSign()
            Me.ClearLabelErrSign(Me.TheDealerControl.CaptionLabel)
            Me.ClearLabelErrSign(moEffectiveLabel)
            Me.ClearLabelErrSign(moExpirationLabel)
            Me.ClearLabelErrSign(moCodeLabel)
            Me.ClearLabelErrSign(moDescriptionLabel)

        End Sub
#End Region

#Region "Button-Management"

        Private Sub SetPlanButtonsState(ByVal bIsNew As Boolean)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, Not bIsNew)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, Not bIsNew)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, Not bIsNew)
            ControlMgr.SetVisibleControl(Me, CommPlanExtPanel, Not bIsNew)
        End Sub
        'This is for cloned data
        Private Sub SetPlanButtonsState2(ByVal bIsCopy As Boolean)
            ControlMgr.SetEnableControl(Me, btnUndo_WRITE, Not bIsCopy)

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
                    moEffectiveText_WRITE.Text = Date.Now().AddDays(-1).ToString("dd-MMM-yyyy", CultureInfo.CurrentCulture)
                    ' Next Year
                    moExpirationText_WRITE.Text = Date.Now().AddYears(1).ToString("dd-MMM-yyyy", CultureInfo.CurrentCulture)
                Case 1
                    If Me.State.IsPlanNew = True Then
                        'New Record
                        ' Next Day MaxExpiration
                        moEffectiveText_WRITE.Text = MaxExpiration.AddDays(1).ToString("dd-MMM-yyyy", CultureInfo.CurrentCulture)
                        ' Next Year after MaxExpiration 
                        moExpirationText_WRITE.Text = MaxExpiration.AddYears(1).ToString("dd-MMM-yyyy", CultureInfo.CurrentCulture)
                        EnableEffective(False)
                    Else
                        EnableEffective(False)
                        EnableExpiration(False)
                    End If
                    ControlMgr.SetEnableControl(Me, BtnExpirationDate_WRITE, True)
                Case Else   ' There is more than one record
                    EnableExpiration(True)
                    If Me.State.IsPlanNew = True Then
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
            Dim oCompanyList As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Try
                Dim dv As DataView = LookupListNew.GetDealerLookupList(oCompanyList, True)
                TheDealerControl.SetControl(True, TheDealerControl.MODES.NEW_MODE, True, dv, "*" + TranslationBase.TranslateLabelOrMessage(LABEL_DEALER), True, True)
                If Me.State.IsPlanNew = True Then
                    TheDealerControl.SelectedGuid = Guid.Empty
                    TheDealerControl.ChangeEnabledControlProperty(True)
                Else
                    TheDealerControl.ChangeEnabledControlProperty(False)

                    TheDealerControl.SelectedGuid = ThePlan.DealerId
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.MasterPage.MessageController.AddError(COMPENSATION_FORM001)
                Me.MasterPage.MessageController.AddError(ex.Message, False)
                Me.MasterPage.MessageController.Show()
            End Try
        End Sub

        Private Sub PopulateDates()
            Me.PopulateControlFromBOProperty(moEffectiveText_WRITE, ThePlan.EffectiveDate)
            Me.PopulateControlFromBOProperty(moExpirationText_WRITE, ThePlan.ExpirationDate)
        End Sub

        Private Sub PopulateCode()
            Me.PopulateControlFromBOProperty(moCodeText, ThePlan.Code)
        End Sub

        Private Sub PopulateDescription()
            Me.PopulateControlFromBOProperty(moDescription, ThePlan.Description)
        End Sub

        Private Sub PopulatePlan()
            Try
                PopulateDealer()
                PopulateDates()
                PopulateCode()
                PopulateDescription()
                EnableDateFields()
                PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Sub BindDataGrid(ByVal oDataView As DataView)
            moDataGrid.DataSource = oDataView
            moDataGrid.DataBind()
        End Sub

        Private Sub PopulateGrid(Optional ByVal oAction As String = POPULATE_ACTION_NONE)

            Try
                If (Me.State.CommissionPlanId.Equals(Guid.Empty)) Then
                    Me.State.CommissionPlanId = CType(Me.CallingParameters, Guid)
                End If

                If ((Me.State.searchcpDV Is Nothing) OrElse (Me.State.HasDataChanged)) Then
                    Me.State.searchcpDV = CommPlanExtract.getList(Me.State.CommissionPlanId)
                End If
                Me.State.searchcpDV.Sort = Me.State.SortExpression
                moDataGrid.AutoGenerateColumns = False
                HighLightSortColumn(moDataGrid, Me.State.SortExpression)
                'BasePopulateGrid(moDataGrid, Me.State.searchcpDV, Me.State.CommissionPlanId, oAction)
                If Me.IsReturningFromChild Then
                    BasePopulateGrid(moDataGrid, Me.State.searchcpDV, Me.State.CommissionPlanId, POPULATE_ACTION_SAVE)
                Else
                    BasePopulateGrid(moDataGrid, Me.State.searchcpDV, Me.State.CommissionPlanId, oAction)
                End If

                ControlMgr.SetVisibleControl(Me, trPageSize, moDataGrid.Visible)

                Session("recCount") = Me.State.searchcpDV.Count

                If Me.moDataGrid.Visible Then
                    Me.lblRecordCount.Text = Me.State.searchcpDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Me.moDataGrid.CurrentPageIndex = NewCurrentPageIndex(moDataGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try
        End Sub


#End Region

#Region "Clear"

        Private Sub ClearDealer()
            If Me.State.IsPlanNew = True Then
                TheDealerControl.SelectedIndex = 0
            Else
                TheDealerControl.SelectedGuid = ThePlan.DealerId

            End If

        End Sub

        Private Sub ClearTexts()
            moCodeText.Text = Nothing
            moDescription.Text = Nothing
        End Sub

        Private Sub ClearPlan()
            ClearDealer()
            ClearTexts()
        End Sub

#End Region

#Region "Business Part"
        Private Sub UpdateBreadCrum()
            If (Not Me.State Is Nothing) Then
                If (Not Me.State Is Nothing) Then
                    Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("COMPENSATION_PLAN")
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("COMPENSATION_PLAN")
                End If
            End If
        End Sub

        Private Sub PopulatePlanBOFromForm(ByVal oPlan As CompensationPlan)
            With oPlan
                ' DropDowns
                .DealerId = TheDealerControl.SelectedGuid
                Me.PopulateBOProperty(oPlan, EFFECTIVE_DATE_PROPERTY, moEffectiveText_WRITE)
                Me.PopulateBOProperty(oPlan, EXPIRATION_DATE_PROPERTY, moExpirationText_WRITE)
                Me.PopulateBOProperty(oPlan, CODE_PROPERTY, moCodeText)
                Me.PopulateBOProperty(oPlan, DESCRIPTION_PROPERTY, moDescription)
            End With


            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Public Sub ValidateDates()

            If moExpirationText_WRITE.Text = Nothing And moEffectiveText_WRITE.Text <> String.Empty Then
                ElitaPlusPage.SetLabelError(moExpirationLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.EXPERITAION_DATE_ERR1)

            ElseIf moExpirationText_WRITE.Text <> String.Empty And moEffectiveText_WRITE.Text = Nothing Then
                ElitaPlusPage.SetLabelError(moEffectiveLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.EFFECTIVE_DATE_ERR1)
            ElseIf moExpirationText_WRITE.Text = Nothing And moEffectiveText_WRITE.Text = Nothing Then
                ElitaPlusPage.SetLabelError(moEffectiveLabel)
                ElitaPlusPage.SetLabelError(moExpirationLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.EXPERITAION_DATE_ERR1)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.EFFECTIVE_DATE_ERR1)
            End If
        End Sub

        Private Function IsDirtyPlanBO() As Boolean
            Dim bIsDirty As Boolean = True
            Dim oPlan As CompensationPlan

            oPlan = ThePlan
            With oPlan
                PopulatePlanBOFromForm(Me.State.MyBo)
                bIsDirty = .IsDirty
            End With
            Return bIsDirty
        End Function


        Private Function ApplyPlanChanges() As Boolean
            Dim bIsOk As Boolean = True
            Dim oPlan As CompensationPlan

            Try

                If IsDirtyPlanBO() = True Then
                    oPlan = ThePlan
                    ValidateDates()
                    'BindBoPropertiesToLabels(Me.State.MyBo)
                    oPlan.Save()
                    'Me.State.MyBo = Nothing
                    Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                Else
                    Me.MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
                End If
                Me.State.IsPlanNew = False
                SetPlanButtonsState(False)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
                bIsOk = False
            End Try
            Return bIsOk
        End Function

        Private Function DeletePlan() As Boolean
            Dim bIsOk As Boolean = True
            Dim oPlan As CompensationPlan

            Try
                oPlan = ThePlan
                With oPlan
                    .Delete()
                    .Save()
                End With
            Catch ex As Exception
                Me.MasterPage.MessageController.AddError(COMPENSATION_CODE_FORM002)
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
                bIsOk = False
            End Try
            Return bIsOk
        End Function

        Public Shared Sub SetLabelColor(ByVal lbl As Label)
            lbl.ForeColor = Color.Black
        End Sub
#End Region

#Region "State-Management"

#Region "Period State-Management"

        Protected Sub ComingFromBack()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
            Try
                If Not confResponse = String.Empty Then
                    ' Return from the Back Button

                    Select Case confResponse
                        Case Me.MSG_VALUE_YES
                            ' Save and go back to Search Page
                            If ApplyPlanChanges() = True Then
                                Me.State.HasDataChanged = True
                                GoBack()
                            End If
                        Case Me.MSG_VALUE_NO
                            ' Go back to Search Page
                            GoBack()
                    End Select
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub ComingFromNew()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                ' Return from the New Button

                Select Case confResponse
                    Case MSG_VALUE_YES
                        ' Save and create a new BO
                        If ApplyPlanChanges() = True Then
                            Me.State.HasDataChanged = True
                            CreateNew()
                        End If
                    Case MSG_VALUE_NO
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
                    Case MSG_VALUE_YES
                        ' Save and create a new Copy BO
                        If ApplyPlanChanges() = True Then
                            Me.State.HasDataChanged = True
                            CreateNewCopy()
                        End If
                    Case MSG_VALUE_NO
                        ' create a new BO
                        CreateNewCopy()
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

        Private Sub SetSession()
            With Me.State

                .CommissionPlanId = Me.State.CommissionPlanId 'Me.GetSelectedItem(moDealerDrop)
                .PageIndex = moDataGrid.CurrentPageIndex
                .PageSize = moDataGrid.PageSize
                .PageSort = Me.State.SortExpression
                .SearchCommPlanExtDataView = Me.State.searchcpDV
            End With
        End Sub

#End Region

#End Region

    End Class
End Namespace