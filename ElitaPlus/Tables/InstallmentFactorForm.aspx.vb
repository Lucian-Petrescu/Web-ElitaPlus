Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common

Partial Class InstallmentFactorForm
    Inherits ElitaPlusSearchPage

    Private Class PageStatus

        Public Sub New()
            pageIndex = 0
            pageCount = 0
        End Sub

    End Class

#Region "Member Variables"
    Private Shared pageIndex As Integer
    Private Shared pageCount As Integer
    'Private DealerId As Guid
    'Private ContractId As Guid

    Protected WithEvents multipleDropControl As MultipleColumnDDLabelControl
    Protected WithEvents CMD As System.Web.UI.HtmlControls.HtmlInputHidden

#End Region

#Region "Properties"

    Public ReadOnly Property IsEditing() As Boolean
        Get
            IsEditing = (moDataGrid.EditItemIndex > NO_ROW_SELECTED_INDEX)
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

#Region "Page State"
    Class MyState
        Public MyBO As InstallmentFactor
        Public PageIndex As Integer = 0
        Public effective As Date
        Public expiration As Date
        Public DealerId As Guid = Guid.Empty
        Public LowPayment As Integer
        Public HighPayment As Integer
        Public Factor As Double
        Public Id As Guid
        Public IsAfterSave As Boolean
        Public IsEditMode As Boolean
        Public AddingNewRow As Boolean
        Public Canceling As Boolean
        Public searchDV As DataView = Nothing
        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        Public SortExpression As String = InstallmentFactor.InstallmentFactorSearchDV.COL_LOW_NUMBER_OF_PAYMENTS
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
    End Class

    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            Return CType(MyBase.State, MyState)
        End Get
    End Property

#End Region


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

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

    Public Const URL As String = "~/Tables/InstallmentFactorForm.aspx"

    Private Const ID_COL As Integer = 2
    Private Const LOW_PAYMENT_COL As Integer = 3
    Private Const HIGH_PAYMENT_COL As Integer = 4
    Private Const FACTOR_COL As Integer = 5
    Private Const DEALER_ID_COL As Integer = 6

    Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
    Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
    Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
    Private Const MSG_RECORD_COPIED_OK As String = "MSG_COPY_WAS_COMPLETED_SUCCESSFULLY"
    Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

    Private Const LOW_PAYMENT_CONTROL_NAME As String = "moLowPaymentText"
    Private Const HIGH_DAY_CONTROL_NAME As String = "moHighPaymentText"
    Private Const ID_CONTROL_NAME As String = "moInstallmentFactor_ID"
    Private Const FACTOR_CONTROL_NAME As String = "moFactorText"
    Private Const DEALER_CONTROL_NAME As String = "moDealer_ID"

    Private Const EDIT_COMMAND As String = "EditRecord"
    Private Const DELETE_COMMAND As String = "DeleteRecord"
    Private Const SORT_COMMAND As String = "Sort"

    Private Const NO_ROW_SELECTED_INDEX As Integer = -1
    Private Const LABEL_SELECT_DEALERCODE As String = "DEALER"


    Private COPY_INSTALLMENT_FACTOR As String = "COPY_INSTALLMENT_FACTOR"
    Private NEW_INSTALLMENT_FACTOR As String = "NEW_INSTALLMENT_FACTOR"
    Private INIT_LOAD As String = "INIT_LOAD"

#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public DealerBO As Dealer
        Public HasDataChanged As Boolean

        Public Sub New(LastOp As DetailPageCommand, DealerBO As Dealer, hasDataChanged As Boolean)
            LastOperation = LastOp
            Me.DealerBO = DealerBO
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class
#End Region

#Region "Page Parameters"
    Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
        Try
            MenuEnabled = True
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub

    Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
        Try
            If CallingParameters IsNot Nothing Then
                State.DealerId = CType(CType(CallingPar, ArrayList)(0), Guid)
                If CType(CallingPar, ArrayList)(1) Is Nothing OrElse CType(CallingPar, ArrayList)(1).Equals(String.Empty) Then
                    State.effective = Date.MinValue
                Else
                    State.effective = CType(CType(CallingPar, ArrayList)(1), Date)
                End If
                If CType(CallingPar, ArrayList)(2) Is Nothing OrElse CType(CallingPar, ArrayList)(2).Equals(String.Empty) Then
                    State.expiration = Date.MinValue
                Else
                    State.expiration = CType(CType(CallingPar, ArrayList)(2), Date)
                End If

                If State.DealerId.Equals(Guid.Empty) Then
                    CMD.Value = INIT_LOAD
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try

    End Sub
#End Region

#Region "Private Methods"

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        'Put user code to initialize the page here
        Try
            ErrController.Clear_Hide()
            ErrorControllerDS.Clear_Hide()
            CopyDealerId.Value = Request.Params("CopyDealerId")
            If CMD.Value Is Nothing OrElse CMD.Value = "" Then
                CMD.Value = Request.Params("CMD")
            End If

            If Not Page.IsPostBack Then
                If State.MyBO Is Nothing Then
                    State.MyBO = New InstallmentFactor
                End If
                AddCalendar(ImageButtonStartDate, TextboxEffective)
                AddCalendar(ImageButtonEndDate, TextboxExpiration)
                PopulateHeader()
                'PopulateContract()
                SetGridItemStyleColor(moDataGrid)
                State.PageIndex = 0
                SetButtonsState()
                SetLowerButtonsState()
                'SetFieldsState()
                PopulateGrid()
            End If
            'PopulateHeader()
            BindBoPropertiesToGridHeaders()
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
        ShowMissingTranslations(ErrController)
    End Sub

    Private Sub PopulateGrid()
        Dim dv As DataView
        Dim maxHighDay As Long

        Try
            If (State.searchDV Is Nothing) Then
                State.searchDV = GetDV()
            End If

            If (State.IsAfterSave) Then
                State.IsAfterSave = False
                SetPageAndSelectedIndexFromGuid(State.searchDV, State.Id, moDataGrid, State.PageIndex)
            ElseIf (State.IsEditMode) Then
                SetPageAndSelectedIndexFromGuid(State.searchDV, State.Id, moDataGrid, State.PageIndex, State.IsEditMode)
            Else
                SetPageAndSelectedIndexFromGuid(State.searchDV, Guid.Empty, moDataGrid, State.PageIndex)
            End If

            moDataGrid.AutoGenerateColumns = False
            moDataGrid.Columns(LOW_PAYMENT_COL).SortExpression = InstallmentFactor.InstallmentFactorSearchDV.COL_LOW_NUMBER_OF_PAYMENTS
            moDataGrid.Columns(HIGH_PAYMENT_COL).SortExpression = InstallmentFactor.InstallmentFactorSearchDV.COL_HIGH_NUMBER_OF_PAYMENTS
            moDataGrid.Columns(FACTOR_COL).SortExpression = InstallmentFactor.InstallmentFactorSearchDV.COL_FACTOR
            SortAndBindGrid()

            dv = State.searchDV
            dv.Sort = InstallmentFactor.InstallmentFactorSearchDV.COL_HIGH_NUMBER_OF_PAYMENTS + " ASC"

            If (dv.Count > 0) Then
                If Not dv(dv.Count - 1)(InstallmentFactor.InstallmentFactorSearchDV.COL_HIGH_NUMBER_OF_PAYMENTS).Equals(DBNull.Value) Then
                    maxHighDay = CType(dv(dv.Count - 1)(InstallmentFactor.InstallmentFactorSearchDV.COL_HIGH_NUMBER_OF_PAYMENTS), Long)
                    DisableDelControl(moDataGrid, maxHighDay)
                End If
            End If

        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub

    Private Sub PopulateHeader()

        Dim oDealerview As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
        Dim oDataView As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)

        TheDealerControl.Caption = "* " + TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALERCODE)
        TheDealerControl.NothingSelected = True
        TheDealerControl.BindData(oDataView)
        TheDealerControl.SelectedGuid = State.DealerId
        If (CMD Is Nothing OrElse CMD.Value <> COPY_INSTALLMENT_FACTOR) Then
            TheDealerControl.AutoPostBackDD = True
        End If

        If State.effective.Equals(Date.MinValue) Then
            PopulateControlFromBOProperty(TextboxEffective, Nothing)
        Else
            PopulateControlFromBOProperty(TextboxEffective, State.effective)
        End If

        If State.expiration.Equals(Date.MinValue) Then
            PopulateControlFromBOProperty(TextboxExpiration, Nothing)
        Else
            PopulateControlFromBOProperty(TextboxExpiration, State.expiration)
        End If

    End Sub

    Private Sub OnFromDrop_Changed(fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
            Handles multipleDropControl.SelectedDropChanged
        Try
            State.DealerId = TheDealerControl.SelectedGuid
            If CMD.Value <> COPY_INSTALLMENT_FACTOR Then
                CMD.Value = INIT_LOAD
                State.searchDV = Nothing
                PopulateGrid()
                SetButtonsState()
                SetLowerButtonsState()
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub

    Public Sub DisableDelControl(grid As DataGrid, maxHighDay As Long)
        Dim i As Integer
        Dim del As ImageButton
        Dim highDay As Label

        For i = 0 To (grid.Items.Count - 1)
            del = CType(grid.Items(i).Cells(DELETE_COL).FindControl(DELETE_CONTROL_NAME), ImageButton)
            highDay = CType(grid.Items(i).Cells(HIGH_PAYMENT_COL).FindControl("moHighPaymentLabel"), Label)
            If del IsNot Nothing AndAlso highDay IsNot Nothing Then
                If CType(highDay.Text, Long) <> maxHighDay Then
                    del.Enabled = False
                    del.Visible = False
                Else
                    del.Enabled = True
                    del.Visible = True
                End If
            End If
        Next

    End Sub

    Private Function GetDV() As DataView

        Dim dv As DataView

        If State.effective = Date.MinValue AndAlso State.MyBO IsNot Nothing AndAlso State.MyBO.EffectiveDate IsNot Nothing Then
            State.effective = CType(State.MyBO.EffectiveDate, Date)
        End If
        If State.expiration = Date.MinValue AndAlso State.MyBO IsNot Nothing AndAlso State.MyBO.ExpirationDate IsNot Nothing Then
            State.expiration = CType(State.MyBO.ExpirationDate, Date)
        End If

        State.searchDV = GetGridDataView()
        State.searchDV.Sort = moDataGrid.DataMember()

        Return (State.searchDV)

    End Function

    Private Function GetGridDataView() As DataView
        Return (InstallmentFactor.LoadList(State.DealerId, State.effective, State.expiration))
    End Function

    Private Sub SetStateProperties()
    End Sub

    Private Sub AddNew()

        Dim dv As DataView

        State.searchDV = GetGridDataView()
        State.MyBO = New InstallmentFactor
        State.Id = State.MyBO.Id

        State.searchDV = State.MyBO.GetNewDataViewRow(State.searchDV, State.Id, State.MyBO)

        moDataGrid.DataSource = State.searchDV

        SetPageAndSelectedIndexFromGuid(State.searchDV, State.Id, moDataGrid, State.PageIndex, State.IsEditMode)

        moDataGrid.DataBind()

        State.PageIndex = moDataGrid.CurrentPageIndex

        SetGridControls(moDataGrid, False)

        'Set focus on the Low Month TextBox for the EditItemIndex row
        SetFocusOnEditableFieldInGrid(moDataGrid, LOW_PAYMENT_COL, LOW_PAYMENT_CONTROL_NAME, moDataGrid.EditItemIndex)

        AssignSelectedRecordFromBO()

        'Me.TranslateGridControls(moDataGrid)
        SetButtonsState()
        ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, moDataGrid)


    End Sub

    Private Sub SortAndBindGrid()
        State.PageIndex = moDataGrid.CurrentPageIndex
        moDataGrid.DataSource = State.searchDV
        moDataGrid.DataBind()

        ControlMgr.SetVisibleControl(Me, moDataGrid, True)
        Session("recCount") = State.searchDV.Count

        ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, moDataGrid)
    End Sub

    Private Sub AssignBOFromSelectedRecord()

        If State.MyBO.EffectiveDate Is Nothing Then
            PopulateBOProperty(State.MyBO, "EffectiveDate", TextboxEffective)
        End If
        If State.MyBO.ExpirationDate Is Nothing Then
            PopulateBOProperty(State.MyBO, "ExpirationDate", TextboxExpiration)
        End If
        If State.MyBO.DealerId.Equals(Guid.Empty) Then
            PopulateBOProperty(State.MyBO, "DealerId", State.DealerId)
        End If

        PopulateBOProperty(State.MyBO, "LowNumberOfPayments", CType(GetSelectedGridControl(moDataGrid, LOW_PAYMENT_COL), TextBox))
        PopulateBOProperty(State.MyBO, "HighNumberOfPayments", CType(GetSelectedGridControl(moDataGrid, HIGH_PAYMENT_COL), TextBox))
        PopulateBOProperty(State.MyBO, "Factor", CType(GetSelectedGridControl(moDataGrid, FACTOR_COL), TextBox))

        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If

    End Sub

    Private Sub AssignSelectedRecordFromBO()

        Dim gridRowIdx As Integer = moDataGrid.EditItemIndex
        Try
            With State.MyBO
                If .LowNumberOfPayments IsNot Nothing Then
                    CType(moDataGrid.Items(gridRowIdx).Cells(LOW_PAYMENT_COL).FindControl(LOW_PAYMENT_CONTROL_NAME), TextBox).Text = CType(.LowNumberOfPayments, String)
                End If
                If .HighNumberOfPayments IsNot Nothing Then
                    CType(moDataGrid.Items(gridRowIdx).Cells(HIGH_PAYMENT_COL).FindControl(HIGH_DAY_CONTROL_NAME), TextBox).Text = CType(.HighNumberOfPayments, String)
                End If
                If .Factor IsNot Nothing Then
                    CType(moDataGrid.Items(gridRowIdx).Cells(FACTOR_COL).FindControl(FACTOR_CONTROL_NAME), TextBox).Text = CType(.Factor, String)
                End If
                If Not .DealerId.Equals(Guid.Empty) Then
                    CType(moDataGrid.Items(gridRowIdx).Cells(DEALER_ID_COL).FindControl(DEALER_CONTROL_NAME), Label).Text = GuidControl.GuidToHexString(.DealerId)
                ElseIf Not State.DealerId.Equals(Guid.Empty) Then
                    CType(moDataGrid.Items(gridRowIdx).Cells(DEALER_ID_COL).FindControl(DEALER_CONTROL_NAME), Label).Text = GuidControl.GuidToHexString(State.DealerId)
                End If

                CType(moDataGrid.Items(gridRowIdx).Cells(ID_COL).FindControl(ID_CONTROL_NAME), Label).Text = .Id.ToString
            End With
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try

    End Sub

    Private Sub ReturnFromEditing()

        moDataGrid.EditItemIndex = NO_ROW_SELECTED_INDEX

        If moDataGrid.PageCount = 0 Then
            'if returning to the "1st time in" screen
            ControlMgr.SetVisibleControl(Me, moDataGrid, False)
        Else
            ControlMgr.SetVisibleControl(Me, moDataGrid, True)
        End If

        State.IsEditMode = False
        PopulateGrid()
        State.PageIndex = moDataGrid.CurrentPageIndex
        SetButtonsState()

    End Sub

    Private Sub SetButtonsState()

        If (State.IsEditMode) Then
            ControlMgr.SetEnableControl(Me, BtnSave_WRITE, True)
            ControlMgr.SetEnableControl(Me, BtnCancel, True)
            ControlMgr.SetEnableControl(Me, BtnNew_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnBack, False)
            MenuEnabled = False
        Else
            ControlMgr.SetEnableControl(Me, BtnSave_WRITE, False)
            ControlMgr.SetEnableControl(Me, BtnCancel, False)

            If CMD.Value <> COPY_INSTALLMENT_FACTOR Then
                ControlMgr.SetEnableControl(Me, BtnNew_WRITE, True)
            Else
                ControlMgr.SetEnableControl(Me, BtnNew_WRITE, False)
            End If

            ControlMgr.SetEnableControl(Me, btnBack, True)
            MenuEnabled = True
        End If

    End Sub

    Private Sub SetLowerButtonsState()
        If State.IsEditMode = True Then
            btnBack.Enabled = False
            btnApply_WRITE.Enabled = False
            btnButtomNew_WRITE.Enabled = False
            btnCopy_WRITE.Enabled = False
        Else
            If CMD.Value = NEW_INSTALLMENT_FACTOR Then
                btnBack.Enabled = True
                btnApply_WRITE.Enabled = False
                btnButtomNew_WRITE.Enabled = True
                btnCopy_WRITE.Enabled = True
                TheDealerControl.ChangeEnabledControlProperty(True)
                TextboxEffective.Enabled = True
                TextboxExpiration.Enabled = True
                ControlMgr.SetEnableControl(Me, ImageButtonStartDate, True)
                ControlMgr.SetEnableControl(Me, ImageButtonEndDate, True)
            ElseIf CMD.Value = COPY_INSTALLMENT_FACTOR Then
                btnBack.Enabled = True
                btnApply_WRITE.Enabled = True
                btnButtomNew_WRITE.Enabled = False
                btnCopy_WRITE.Enabled = False
                TheDealerControl.ChangeEnabledControlProperty(True)
                TextboxEffective.Enabled = True
                TextboxExpiration.Enabled = True
                ControlMgr.SetEnableControl(Me, ImageButtonStartDate, True)
                ControlMgr.SetEnableControl(Me, ImageButtonEndDate, True)
            ElseIf CMD.Value = INIT_LOAD Then
                btnBack.Enabled = True
                btnApply_WRITE.Enabled = False
                btnButtomNew_WRITE.Enabled = False
                btnCopy_WRITE.Enabled = False
                TheDealerControl.ChangeEnabledControlProperty(True)
                TextboxEffective.Enabled = True
                TextboxExpiration.Enabled = True
                ControlMgr.SetEnableControl(Me, ImageButtonStartDate, True)
                ControlMgr.SetEnableControl(Me, ImageButtonEndDate, True)
            Else
                btnBack.Enabled = True
                btnApply_WRITE.Enabled = False
                btnButtomNew_WRITE.Enabled = True
                btnCopy_WRITE.Enabled = True
                TheDealerControl.ChangeEnabledControlProperty(False)
                TextboxEffective.Enabled = False
                TextboxExpiration.Enabled = False
                ControlMgr.SetEnableControl(Me, ImageButtonStartDate, False)
                ControlMgr.SetEnableControl(Me, ImageButtonEndDate, False)
            End If
        End If
    End Sub


    'Private Sub SetFieldsState()
    '    'Me.moStartDateText.Enabled = False
    '    'Me.moEndDateText.Enabled = False
    '    'Me.cboContract.Enabled = False
    'End Sub


#End Region

#Region "Button Click Handlers"

    Private Sub NewButton_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles BtnNew_WRITE.Click

        Try
            State.IsEditMode = True
            State.AddingNewRow = True
            AddNew()
            SetButtonsState()
            SetLowerButtonsState()
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try

    End Sub

    Private Sub SaveButton_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles BtnSave_WRITE.Click

        Try
            AssignBOFromSelectedRecord()

            If (State.MyBO.IsDirty) Then
                State.MyBO.Save()
                State.IsAfterSave = True
                State.AddingNewRow = False
                AddInfoMsg(MSG_RECORD_SAVED_OK)
                State.searchDV = Nothing
                CMD.Value = ""
                ReturnFromEditing()
            Else
                AddInfoMsg(MSG_RECORD_NOT_SAVED)
                ReturnFromEditing()
            End If
            SetLowerButtonsState()
        Catch ex As Exception
            HandleErrors(ex, ErrorControllerDS)
        End Try

    End Sub

    Private Sub CancelButton_Click(sender As System.Object, e As System.EventArgs) Handles BtnCancel.Click

        Try
            moDataGrid.SelectedIndex = NO_ITEM_SELECTED_INDEX
            State.Canceling = True
            If (State.AddingNewRow) Then
                State.AddingNewRow = False
                State.searchDV = Nothing
            End If
            ReturnFromEditing()
            SetLowerButtonsState()
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try

    End Sub

    Private Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
        Try
            Back(ElitaPlusPage.DetailPageCommand.Back)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrController)
            Dim dealerBO As New Dealer
            ReturnToCallingPage(New ReturnType(State.ActionInProgress, dealerBO, False))
        End Try
    End Sub

    Protected Sub Back(cmd As ElitaPlusPage.DetailPageCommand)
        Dim DealerBO As New Dealer(State.DealerId)
        Dim retObj As ReturnType = New ReturnType(cmd, DealerBO, False)
        NavController = Nothing
        ReturnToCallingPage(retObj)
    End Sub

    Private Sub btnButtomNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnButtomNew_WRITE.Click
        Try
            CMD.Value = NEW_INSTALLMENT_FACTOR
            State.DealerId = Guid.Empty
            'Me.State.ContractId = Guid.Empty
            State.IsEditMode = False
            State.AddingNewRow = False
            'AddNew()
            SetButtonsState()
            SetLowerButtonsState()
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub

    Private Sub btnCopy_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnCopy_WRITE.Click
        Try
            CMD.Value = COPY_INSTALLMENT_FACTOR
            CopyDealerId.Value = GuidControl.GuidToHexString(State.DealerId)
            State.DealerId = Guid.Empty
            PopulateHeader()
            SetLowerButtonsState()
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub

    Private Sub btnApply_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnApply_WRITE.Click
        Try
            Dim dv As DataView = InstallmentFactor.LoadList(New Guid(GuidControl.HexToByteArray(CopyDealerId.Value)), State.effective, State.expiration)
            Dim dt As DataTable = dv.Table

            For Each row As DataRow In dt.Rows
                State.MyBO = New InstallmentFactor
                State.Id = State.MyBO.Id
                State.MyBO.DealerId = State.DealerId
                State.MyBO.LowNumberOfPayments = CType(row(InstallmentFactor.InstallmentFactorSearchDV.COL_LOW_NUMBER_OF_PAYMENTS), Long)
                State.MyBO.HighNumberOfPayments = CType(row(InstallmentFactor.InstallmentFactorSearchDV.COL_HIGH_NUMBER_OF_PAYMENTS), Long)
                State.MyBO.EffectiveDate = State.effective
                State.MyBO.ExpirationDate = State.expiration

                If (row(InstallmentFactor.InstallmentFactorSearchDV.COL_FACTOR).Equals(DBNull.Value)) Then
                    State.MyBO.Factor = Nothing
                Else
                    State.MyBO.Factor = CType(CType(row(InstallmentFactor.InstallmentFactorSearchDV.COL_FACTOR), Decimal), DecimalType)
                End If

                State.MyBO.Save()
            Next

            CMD.Value = ""
            CopyDealerId.Value = ""
            SetLowerButtonsState()
            State.IsEditMode = False
            State.IsAfterSave = True
            AddInfoMsg(MSG_RECORD_COPIED_OK)
            State.searchDV = Nothing
            ReturnFromEditing()
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub

#End Region

#Region " Datagrid Related "

    Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles moDataGrid.PageIndexChanged

        Try
            If (Not (State.IsEditMode)) Then
                State.PageIndex = e.NewPageIndex
                moDataGrid.CurrentPageIndex = State.PageIndex
                PopulateGrid()
                moDataGrid.SelectedIndex = NO_ITEM_SELECTED_INDEX
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try

    End Sub

    Protected Sub ItemCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs)

        Try
            Dim index As Integer = e.Item.ItemIndex

            If (e.CommandName = EDIT_COMMAND) Then
                'Do the Edit here

                'Set the IsEditMode flag to TRUE
                State.IsEditMode = True

                State.Id = New Guid(CType(moDataGrid.Items(e.Item.ItemIndex).Cells(ID_COL).FindControl(ID_CONTROL_NAME), Label).Text)
                State.MyBO = New InstallmentFactor(State.Id)

                PopulateGrid()

                State.PageIndex = moDataGrid.CurrentPageIndex

                'Disable all Edit and Delete icon buttons on the moDataGrid
                SetGridControls(moDataGrid, False)

                'Set focus on the Description TextBox for the EditItemIndex row
                SetFocusOnEditableFieldInGrid(moDataGrid, LOW_PAYMENT_COL, LOW_PAYMENT_CONTROL_NAME, index)

                AssignSelectedRecordFromBO()
                SetButtonsState()

            ElseIf (e.CommandName = DELETE_COMMAND) Then
                'Do the delete here

                'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                moDataGrid.SelectedIndex = NO_ROW_SELECTED_INDEX

                'Save the Id in the Session

                State.Id = New Guid(CType(moDataGrid.Items(e.Item.ItemIndex).Cells(ID_COL).FindControl(ID_CONTROL_NAME), Label).Text)
                State.MyBO = New InstallmentFactor(State.Id)

                Try
                    State.MyBO.Delete()
                    State.MyBO.Save()
                Catch ex As Exception
                    State.MyBO.RejectChanges()
                    Throw ex
                End Try

                State.PageIndex = moDataGrid.CurrentPageIndex

                'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
                State.IsAfterSave = True
                State.searchDV = Nothing
                PopulateGrid()
                State.PageIndex = moDataGrid.CurrentPageIndex

            End If

        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try

    End Sub

    Protected Sub ItemBound(source As Object, e As DataGridItemEventArgs) Handles moDataGrid.ItemDataBound
        Try
            BaseItemBound(source, e)
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub

    Protected Sub ItemCreated(sender As Object, e As DataGridItemEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles moDataGrid.SortCommand

        Try
            If State.SortExpression.StartsWith(e.SortExpression) Then
                If State.SortExpression.EndsWith(" DESC") Then
                    State.SortExpression = e.SortExpression
                Else
                    State.SortExpression &= " DESC"
                End If
            Else
                State.SortExpression = e.SortExpression
            End If
            State.Id = Guid.Empty
            State.PageIndex = 0

            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try

    End Sub

    Protected Sub BindBoPropertiesToGridHeaders()
        BindBOPropertyToGridHeader(State.MyBO, "LowNumberOfPayments", moDataGrid.Columns(LOW_PAYMENT_COL))
        BindBOPropertyToGridHeader(State.MyBO, "HighNumberOfPayments", moDataGrid.Columns(HIGH_PAYMENT_COL))
        BindBOPropertyToGridHeader(State.MyBO, "Factor", moDataGrid.Columns(FACTOR_COL))
        ClearGridHeadersAndLabelsErrSign()
    End Sub

    Private Sub SetFocusOnEditableFieldInGrid(moDataGrid As DataGrid, cellPosition As Integer, controlName As String, itemIndex As Integer)
        'Set focus on the Low Month TextBox for the EditItemIndex row
        Dim lowDay As TextBox = CType(moDataGrid.Items(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
        SetFocus(lowDay)
    End Sub

#End Region

End Class



