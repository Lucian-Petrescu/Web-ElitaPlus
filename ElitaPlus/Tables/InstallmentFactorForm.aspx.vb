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
            IsEditing = (Me.moDataGrid.EditItemIndex > NO_ROW_SELECTED_INDEX)
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

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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

        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal DealerBO As Dealer, ByVal hasDataChanged As Boolean)
            Me.LastOperation = LastOp
            Me.DealerBO = DealerBO
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class
#End Region

#Region "Page Parameters"
    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        Try
            Me.MenuEnabled = True
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
    End Sub

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                Me.State.DealerId = CType(CType(CallingPar, ArrayList)(0), Guid)
                If CType(CallingPar, ArrayList)(1) Is Nothing Or CType(CallingPar, ArrayList)(1).Equals(String.Empty) Then
                    Me.State.effective = Date.MinValue
                Else
                    Me.State.effective = CType(CType(CallingPar, ArrayList)(1), Date)
                End If
                If CType(CallingPar, ArrayList)(2) Is Nothing Or CType(CallingPar, ArrayList)(2).Equals(String.Empty) Then
                    Me.State.expiration = Date.MinValue
                Else
                    Me.State.expiration = CType(CType(CallingPar, ArrayList)(2), Date)
                End If

                If Me.State.DealerId.Equals(Guid.Empty) Then
                    CMD.Value = Me.INIT_LOAD
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub
#End Region

#Region "Private Methods"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Put user code to initialize the page here
        Try
            ErrController.Clear_Hide()
            ErrorControllerDS.Clear_Hide()
            CopyDealerId.Value = Request.Params("CopyDealerId")
            If CMD.Value Is Nothing Or CMD.Value = "" Then
                CMD.Value = Request.Params("CMD")
            End If

            If Not Page.IsPostBack Then
                If Me.State.MyBO Is Nothing Then
                    Me.State.MyBO = New InstallmentFactor
                End If
                Me.AddCalendar(Me.ImageButtonStartDate, Me.TextboxEffective)
                Me.AddCalendar(Me.ImageButtonEndDate, Me.TextboxExpiration)
                PopulateHeader()
                'PopulateContract()
                Me.SetGridItemStyleColor(moDataGrid)
                Me.State.PageIndex = 0
                SetButtonsState()
                SetLowerButtonsState()
                'SetFieldsState()
                PopulateGrid()
            End If
            'PopulateHeader()
            BindBoPropertiesToGridHeaders()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
        Me.ShowMissingTranslations(ErrController)
    End Sub

    Private Sub PopulateGrid()
        Dim dv As DataView
        Dim maxHighDay As Long

        Try
            If (Me.State.searchDV Is Nothing) Then
                Me.State.searchDV = GetDV()
            End If

            If (Me.State.IsAfterSave) Then
                Me.State.IsAfterSave = False
                Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.Id, Me.moDataGrid, Me.State.PageIndex)
            ElseIf (Me.State.IsEditMode) Then
                Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.Id, Me.moDataGrid, Me.State.PageIndex, Me.State.IsEditMode)
            Else
                Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Guid.Empty, Me.moDataGrid, Me.State.PageIndex)
            End If

            Me.moDataGrid.AutoGenerateColumns = False
            Me.moDataGrid.Columns(Me.LOW_PAYMENT_COL).SortExpression = InstallmentFactor.InstallmentFactorSearchDV.COL_LOW_NUMBER_OF_PAYMENTS
            Me.moDataGrid.Columns(Me.HIGH_PAYMENT_COL).SortExpression = InstallmentFactor.InstallmentFactorSearchDV.COL_HIGH_NUMBER_OF_PAYMENTS
            Me.moDataGrid.Columns(Me.FACTOR_COL).SortExpression = InstallmentFactor.InstallmentFactorSearchDV.COL_FACTOR
            Me.SortAndBindGrid()

            dv = Me.State.searchDV
            dv.Sort = InstallmentFactor.InstallmentFactorSearchDV.COL_HIGH_NUMBER_OF_PAYMENTS + " ASC"

            If (dv.Count > 0) Then
                If Not dv(dv.Count - 1)(InstallmentFactor.InstallmentFactorSearchDV.COL_HIGH_NUMBER_OF_PAYMENTS).Equals(DBNull.Value) Then
                    maxHighDay = CType(dv(dv.Count - 1)(InstallmentFactor.InstallmentFactorSearchDV.COL_HIGH_NUMBER_OF_PAYMENTS), Long)
                    DisableDelControl(moDataGrid, maxHighDay)
                End If
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
    End Sub

    Private Sub PopulateHeader()

        Dim oDealerview As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
        Dim oDataView As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)

        TheDealerControl.Caption = "* " + TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALERCODE)
        TheDealerControl.NothingSelected = True
        TheDealerControl.BindData(oDataView)
        TheDealerControl.SelectedGuid = Me.State.DealerId
        If (CMD Is Nothing Or CMD.Value <> COPY_INSTALLMENT_FACTOR) Then
            TheDealerControl.AutoPostBackDD = True
        End If

        If Me.State.effective.Equals(Date.MinValue) Then
            Me.PopulateControlFromBOProperty(Me.TextboxEffective, Nothing)
        Else
            Me.PopulateControlFromBOProperty(Me.TextboxEffective, Me.State.effective)
        End If

        If Me.State.expiration.Equals(Date.MinValue) Then
            Me.PopulateControlFromBOProperty(Me.TextboxExpiration, Nothing)
        Else
            Me.PopulateControlFromBOProperty(Me.TextboxExpiration, Me.State.expiration)
        End If

    End Sub

    Private Sub OnFromDrop_Changed(ByVal fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
            Handles multipleDropControl.SelectedDropChanged
        Try
            Me.State.DealerId = TheDealerControl.SelectedGuid
            If CMD.Value <> Me.COPY_INSTALLMENT_FACTOR Then
                CMD.Value = Me.INIT_LOAD
                Me.State.searchDV = Nothing
                PopulateGrid()
                SetButtonsState()
                SetLowerButtonsState()
            End If
        Catch ex As Exception
            HandleErrors(ex, Me.ErrController)
        End Try
    End Sub

    Public Sub DisableDelControl(ByVal grid As DataGrid, ByVal maxHighDay As Long)
        Dim i As Integer
        Dim del As ImageButton
        Dim highDay As Label

        For i = 0 To (grid.Items.Count - 1)
            del = CType(grid.Items(i).Cells(DELETE_COL).FindControl(DELETE_CONTROL_NAME), ImageButton)
            highDay = CType(grid.Items(i).Cells(HIGH_PAYMENT_COL).FindControl("moHighPaymentLabel"), Label)
            If Not del Is Nothing And Not highDay Is Nothing Then
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

        If Me.State.effective = Date.MinValue AndAlso Not Me.State.MyBO Is Nothing AndAlso Not Me.State.MyBO.EffectiveDate Is Nothing Then
            Me.State.effective = CType(Me.State.MyBO.EffectiveDate, Date)
        End If
        If Me.State.expiration = Date.MinValue AndAlso Not Me.State.MyBO Is Nothing AndAlso Not Me.State.MyBO.ExpirationDate Is Nothing Then
            Me.State.expiration = CType(Me.State.MyBO.ExpirationDate, Date)
        End If

        Me.State.searchDV = GetGridDataView()
        Me.State.searchDV.Sort = moDataGrid.DataMember()

        Return (Me.State.searchDV)

    End Function

    Private Function GetGridDataView() As DataView
        Return (InstallmentFactor.LoadList(Me.State.DealerId, Me.State.effective, Me.State.expiration))
    End Function

    Private Sub SetStateProperties()
    End Sub

    Private Sub AddNew()

        Dim dv As DataView

        Me.State.searchDV = GetGridDataView()
        Me.State.MyBO = New InstallmentFactor
        Me.State.Id = Me.State.MyBO.Id

        Me.State.searchDV = Me.State.MyBO.GetNewDataViewRow(Me.State.searchDV, Me.State.Id, Me.State.MyBO)

        moDataGrid.DataSource = Me.State.searchDV

        Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.Id, Me.moDataGrid, Me.State.PageIndex, Me.State.IsEditMode)

        moDataGrid.DataBind()

        Me.State.PageIndex = moDataGrid.CurrentPageIndex

        SetGridControls(Me.moDataGrid, False)

        'Set focus on the Low Month TextBox for the EditItemIndex row
        Me.SetFocusOnEditableFieldInGrid(Me.moDataGrid, Me.LOW_PAYMENT_COL, Me.LOW_PAYMENT_CONTROL_NAME, Me.moDataGrid.EditItemIndex)

        Me.AssignSelectedRecordFromBO()

        'Me.TranslateGridControls(moDataGrid)
        Me.SetButtonsState()
        ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, moDataGrid)


    End Sub

    Private Sub SortAndBindGrid()
        Me.State.PageIndex = Me.moDataGrid.CurrentPageIndex
        Me.moDataGrid.DataSource = Me.State.searchDV
        Me.moDataGrid.DataBind()

        ControlMgr.SetVisibleControl(Me, moDataGrid, True)
        Session("recCount") = Me.State.searchDV.Count

        ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, moDataGrid)
    End Sub

    Private Sub AssignBOFromSelectedRecord()

        If Me.State.MyBO.EffectiveDate Is Nothing Then
            Me.PopulateBOProperty(Me.State.MyBO, "EffectiveDate", Me.TextboxEffective)
        End If
        If Me.State.MyBO.ExpirationDate Is Nothing Then
            Me.PopulateBOProperty(Me.State.MyBO, "ExpirationDate", Me.TextboxExpiration)
        End If
        If Me.State.MyBO.DealerId.Equals(Guid.Empty) Then
            Me.PopulateBOProperty(Me.State.MyBO, "DealerId", Me.State.DealerId)
        End If

        Me.PopulateBOProperty(Me.State.MyBO, "LowNumberOfPayments", CType(Me.GetSelectedGridControl(moDataGrid, LOW_PAYMENT_COL), TextBox))
        Me.PopulateBOProperty(Me.State.MyBO, "HighNumberOfPayments", CType(Me.GetSelectedGridControl(moDataGrid, HIGH_PAYMENT_COL), TextBox))
        Me.PopulateBOProperty(Me.State.MyBO, "Factor", CType(Me.GetSelectedGridControl(moDataGrid, FACTOR_COL), TextBox))

        If Me.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If

    End Sub

    Private Sub AssignSelectedRecordFromBO()

        Dim gridRowIdx As Integer = Me.moDataGrid.EditItemIndex
        Try
            With Me.State.MyBO
                If Not .LowNumberOfPayments Is Nothing Then
                    CType(Me.moDataGrid.Items(gridRowIdx).Cells(Me.LOW_PAYMENT_COL).FindControl(Me.LOW_PAYMENT_CONTROL_NAME), TextBox).Text = CType(.LowNumberOfPayments, String)
                End If
                If Not .HighNumberOfPayments Is Nothing Then
                    CType(Me.moDataGrid.Items(gridRowIdx).Cells(Me.HIGH_PAYMENT_COL).FindControl(Me.HIGH_DAY_CONTROL_NAME), TextBox).Text = CType(.HighNumberOfPayments, String)
                End If
                If Not .Factor Is Nothing Then
                    CType(Me.moDataGrid.Items(gridRowIdx).Cells(Me.FACTOR_COL).FindControl(Me.FACTOR_CONTROL_NAME), TextBox).Text = CType(.Factor, String)
                End If
                If Not .DealerId.Equals(Guid.Empty) Then
                    CType(Me.moDataGrid.Items(gridRowIdx).Cells(Me.DEALER_ID_COL).FindControl(Me.DEALER_CONTROL_NAME), Label).Text = GuidControl.GuidToHexString(.DealerId)
                ElseIf Not Me.State.DealerId.Equals(Guid.Empty) Then
                    CType(Me.moDataGrid.Items(gridRowIdx).Cells(Me.DEALER_ID_COL).FindControl(Me.DEALER_CONTROL_NAME), Label).Text = GuidControl.GuidToHexString(Me.State.DealerId)
                End If

                CType(Me.moDataGrid.Items(gridRowIdx).Cells(Me.ID_COL).FindControl(Me.ID_CONTROL_NAME), Label).Text = .Id.ToString
            End With
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub

    Private Sub ReturnFromEditing()

        moDataGrid.EditItemIndex = NO_ROW_SELECTED_INDEX

        If Me.moDataGrid.PageCount = 0 Then
            'if returning to the "1st time in" screen
            ControlMgr.SetVisibleControl(Me, moDataGrid, False)
        Else
            ControlMgr.SetVisibleControl(Me, moDataGrid, True)
        End If

        Me.State.IsEditMode = False
        Me.PopulateGrid()
        Me.State.PageIndex = moDataGrid.CurrentPageIndex
        SetButtonsState()

    End Sub

    Private Sub SetButtonsState()

        If (Me.State.IsEditMode) Then
            ControlMgr.SetEnableControl(Me, BtnSave_WRITE, True)
            ControlMgr.SetEnableControl(Me, BtnCancel, True)
            ControlMgr.SetEnableControl(Me, BtnNew_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnBack, False)
            Me.MenuEnabled = False
        Else
            ControlMgr.SetEnableControl(Me, BtnSave_WRITE, False)
            ControlMgr.SetEnableControl(Me, BtnCancel, False)

            If CMD.Value <> Me.COPY_INSTALLMENT_FACTOR Then
                ControlMgr.SetEnableControl(Me, BtnNew_WRITE, True)
            Else
                ControlMgr.SetEnableControl(Me, BtnNew_WRITE, False)
            End If

            ControlMgr.SetEnableControl(Me, btnBack, True)
            Me.MenuEnabled = True
        End If

    End Sub

    Private Sub SetLowerButtonsState()
        If Me.State.IsEditMode = True Then
            btnBack.Enabled = False
            btnApply_WRITE.Enabled = False
            btnButtomNew_WRITE.Enabled = False
            btnCopy_WRITE.Enabled = False
        Else
            If CMD.Value = Me.NEW_INSTALLMENT_FACTOR Then
                btnBack.Enabled = True
                btnApply_WRITE.Enabled = False
                btnButtomNew_WRITE.Enabled = True
                btnCopy_WRITE.Enabled = True
                TheDealerControl.ChangeEnabledControlProperty(True)
                Me.TextboxEffective.Enabled = True
                Me.TextboxExpiration.Enabled = True
                ControlMgr.SetEnableControl(Me, ImageButtonStartDate, True)
                ControlMgr.SetEnableControl(Me, ImageButtonEndDate, True)
            ElseIf CMD.Value = Me.COPY_INSTALLMENT_FACTOR Then
                btnBack.Enabled = True
                btnApply_WRITE.Enabled = True
                btnButtomNew_WRITE.Enabled = False
                btnCopy_WRITE.Enabled = False
                TheDealerControl.ChangeEnabledControlProperty(True)
                Me.TextboxEffective.Enabled = True
                Me.TextboxExpiration.Enabled = True
                ControlMgr.SetEnableControl(Me, ImageButtonStartDate, True)
                ControlMgr.SetEnableControl(Me, ImageButtonEndDate, True)
            ElseIf CMD.Value = Me.INIT_LOAD Then
                btnBack.Enabled = True
                btnApply_WRITE.Enabled = False
                btnButtomNew_WRITE.Enabled = False
                btnCopy_WRITE.Enabled = False
                TheDealerControl.ChangeEnabledControlProperty(True)
                Me.TextboxEffective.Enabled = True
                Me.TextboxExpiration.Enabled = True
                ControlMgr.SetEnableControl(Me, ImageButtonStartDate, True)
                ControlMgr.SetEnableControl(Me, ImageButtonEndDate, True)
            Else
                btnBack.Enabled = True
                btnApply_WRITE.Enabled = False
                btnButtomNew_WRITE.Enabled = True
                btnCopy_WRITE.Enabled = True
                TheDealerControl.ChangeEnabledControlProperty(False)
                Me.TextboxEffective.Enabled = False
                Me.TextboxExpiration.Enabled = False
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

    Private Sub NewButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew_WRITE.Click

        Try
            Me.State.IsEditMode = True
            Me.State.AddingNewRow = True
            AddNew()
            SetButtonsState()
            SetLowerButtonsState()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub

    Private Sub SaveButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave_WRITE.Click

        Try
            AssignBOFromSelectedRecord()

            If (Me.State.MyBO.IsDirty) Then
                Me.State.MyBO.Save()
                Me.State.IsAfterSave = True
                Me.State.AddingNewRow = False
                Me.AddInfoMsg(Me.MSG_RECORD_SAVED_OK)
                Me.State.searchDV = Nothing
                CMD.Value = ""
                Me.ReturnFromEditing()
            Else
                Me.AddInfoMsg(Me.MSG_RECORD_NOT_SAVED)
                Me.ReturnFromEditing()
            End If
            SetLowerButtonsState()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorControllerDS)
        End Try

    End Sub

    Private Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancel.Click

        Try
            Me.moDataGrid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
            Me.State.Canceling = True
            If (Me.State.AddingNewRow) Then
                Me.State.AddingNewRow = False
                Me.State.searchDV = Nothing
            End If
            ReturnFromEditing()
            SetLowerButtonsState()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub

    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.Back(ElitaPlusPage.DetailPageCommand.Back)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
            Dim dealerBO As New Dealer
            Me.ReturnToCallingPage(New ReturnType(Me.State.ActionInProgress, dealerBO, False))
        End Try
    End Sub

    Protected Sub Back(ByVal cmd As ElitaPlusPage.DetailPageCommand)
        Dim DealerBO As New Dealer(Me.State.DealerId)
        Dim retObj As ReturnType = New ReturnType(cmd, DealerBO, False)
        Me.NavController = Nothing
        Me.ReturnToCallingPage(retObj)
    End Sub

    Private Sub btnButtomNew_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnButtomNew_WRITE.Click
        Try
            CMD.Value = Me.NEW_INSTALLMENT_FACTOR
            Me.State.DealerId = Guid.Empty
            'Me.State.ContractId = Guid.Empty
            Me.State.IsEditMode = False
            Me.State.AddingNewRow = False
            'AddNew()
            SetButtonsState()
            SetLowerButtonsState()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
    End Sub

    Private Sub btnCopy_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy_WRITE.Click
        Try
            CMD.Value = Me.COPY_INSTALLMENT_FACTOR
            CopyDealerId.Value = GuidControl.GuidToHexString(Me.State.DealerId)
            Me.State.DealerId = Guid.Empty
            PopulateHeader()
            SetLowerButtonsState()
        Catch ex As Exception
            Me.HandleErrors(ex, ErrController)
        End Try
    End Sub

    Private Sub btnApply_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply_WRITE.Click
        Try
            Dim dv As DataView = InstallmentFactor.LoadList(New Guid(GuidControl.HexToByteArray(CopyDealerId.Value)), Me.State.effective, Me.State.expiration)
            Dim dt As DataTable = dv.Table

            For Each row As DataRow In dt.Rows
                Me.State.MyBO = New InstallmentFactor
                Me.State.Id = Me.State.MyBO.Id
                Me.State.MyBO.DealerId = Me.State.DealerId
                Me.State.MyBO.LowNumberOfPayments = CType(row(InstallmentFactor.InstallmentFactorSearchDV.COL_LOW_NUMBER_OF_PAYMENTS), Long)
                Me.State.MyBO.HighNumberOfPayments = CType(row(InstallmentFactor.InstallmentFactorSearchDV.COL_HIGH_NUMBER_OF_PAYMENTS), Long)
                Me.State.MyBO.EffectiveDate = Me.State.effective
                Me.State.MyBO.ExpirationDate = Me.State.expiration

                If (row(InstallmentFactor.InstallmentFactorSearchDV.COL_FACTOR).Equals(DBNull.Value)) Then
                    Me.State.MyBO.Factor = Nothing
                Else
                    Me.State.MyBO.Factor = CType(CType(row(InstallmentFactor.InstallmentFactorSearchDV.COL_FACTOR), Decimal), DecimalType)
                End If

                Me.State.MyBO.Save()
            Next

            CMD.Value = ""
            CopyDealerId.Value = ""
            SetLowerButtonsState()
            Me.State.IsEditMode = False
            Me.State.IsAfterSave = True
            Me.AddInfoMsg(Me.MSG_RECORD_COPIED_OK)
            Me.State.searchDV = Nothing
            Me.ReturnFromEditing()
        Catch ex As Exception
            Me.HandleErrors(ex, ErrController)
        End Try
    End Sub

#End Region

#Region " Datagrid Related "

    Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles moDataGrid.PageIndexChanged

        Try
            If (Not (Me.State.IsEditMode)) Then
                Me.State.PageIndex = e.NewPageIndex
                Me.moDataGrid.CurrentPageIndex = Me.State.PageIndex
                Me.PopulateGrid()
                Me.moDataGrid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub

    Protected Sub ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)

        Try
            Dim index As Integer = e.Item.ItemIndex

            If (e.CommandName = Me.EDIT_COMMAND) Then
                'Do the Edit here

                'Set the IsEditMode flag to TRUE
                Me.State.IsEditMode = True

                Me.State.Id = New Guid(CType(Me.moDataGrid.Items(e.Item.ItemIndex).Cells(Me.ID_COL).FindControl(Me.ID_CONTROL_NAME), Label).Text)
                Me.State.MyBO = New InstallmentFactor(Me.State.Id)

                Me.PopulateGrid()

                Me.State.PageIndex = moDataGrid.CurrentPageIndex

                'Disable all Edit and Delete icon buttons on the moDataGrid
                SetGridControls(Me.moDataGrid, False)

                'Set focus on the Description TextBox for the EditItemIndex row
                Me.SetFocusOnEditableFieldInGrid(Me.moDataGrid, Me.LOW_PAYMENT_COL, Me.LOW_PAYMENT_CONTROL_NAME, index)

                Me.AssignSelectedRecordFromBO()
                Me.SetButtonsState()

            ElseIf (e.CommandName = Me.DELETE_COMMAND) Then
                'Do the delete here

                'Clear the SelectedItemStyle to remove the highlight from the previously saved row
                moDataGrid.SelectedIndex = Me.NO_ROW_SELECTED_INDEX

                'Save the Id in the Session

                Me.State.Id = New Guid(CType(Me.moDataGrid.Items(e.Item.ItemIndex).Cells(Me.ID_COL).FindControl(Me.ID_CONTROL_NAME), Label).Text)
                Me.State.MyBO = New InstallmentFactor(Me.State.Id)

                Try
                    Me.State.MyBO.Delete()
                    Me.State.MyBO.Save()
                Catch ex As Exception
                    Me.State.MyBO.RejectChanges()
                    Throw ex
                End Try

                Me.State.PageIndex = moDataGrid.CurrentPageIndex

                'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
                Me.State.IsAfterSave = True
                Me.State.searchDV = Nothing
                PopulateGrid()
                Me.State.PageIndex = moDataGrid.CurrentPageIndex

            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub

    Protected Sub ItemBound(ByVal source As Object, ByVal e As DataGridItemEventArgs) Handles moDataGrid.ItemDataBound
        Try
            BaseItemBound(source, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try
    End Sub

    Protected Sub ItemCreated(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles moDataGrid.SortCommand

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
            Me.State.Id = Guid.Empty
            Me.State.PageIndex = 0

            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrController)
        End Try

    End Sub

    Protected Sub BindBoPropertiesToGridHeaders()
        Me.BindBOPropertyToGridHeader(Me.State.MyBO, "LowNumberOfPayments", Me.moDataGrid.Columns(Me.LOW_PAYMENT_COL))
        Me.BindBOPropertyToGridHeader(Me.State.MyBO, "HighNumberOfPayments", Me.moDataGrid.Columns(Me.HIGH_PAYMENT_COL))
        Me.BindBOPropertyToGridHeader(Me.State.MyBO, "Factor", Me.moDataGrid.Columns(Me.FACTOR_COL))
        Me.ClearGridHeadersAndLabelsErrSign()
    End Sub

    Private Sub SetFocusOnEditableFieldInGrid(ByVal moDataGrid As DataGrid, ByVal cellPosition As Integer, ByVal controlName As String, ByVal itemIndex As Integer)
        'Set focus on the Low Month TextBox for the EditItemIndex row
        Dim lowDay As TextBox = CType(moDataGrid.Items(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
        SetFocus(lowDay)
    End Sub

#End Region

End Class



