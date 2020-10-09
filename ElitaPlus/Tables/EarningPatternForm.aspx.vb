Imports Assurant.ElitaPlus.Common.GuidControl
Imports System.Globalization
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports System.Threading

Namespace Tables

    Partial Class EarningPatternForm
        Inherits ElitaPlusSearchPage

#Region "Page State"

#Region "MyState"

        Class MyState
            Public moEarningPatternId As Guid = Guid.Empty
            Public moOldEarningPatternId As Guid = Guid.Empty
            Public moEarningCodeId As Guid = Guid.Empty
            Public IsEarningPatternNew As Boolean = False
            Public IsNewWithCopy As Boolean = False
            Public IsUndo As Boolean = False
            Public moPercentList() As EarningPercent
            Public selectedEarningPercentId As Guid = Guid.Empty
            Public selectedDescription As String
            Public selectedCode As String
            Public selectedEffective As String
            Public selectedExpiration As String
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public boChanged As Boolean = False
            Public bnoRow As Boolean = False
            Public IsEditMode As Boolean = False
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
            State.moEarningPatternId = CType(CallingParameters, Guid)
            State.moOldEarningPatternId = CType(CallingParameters, Guid)
            If State.moEarningPatternId.Equals(Guid.Empty) Then
                State.IsEarningPatternNew = True
                BindBoPropertiesToLabels()
                AddLabelDecorations(TheEarningPattern)
                ClearAll()
                EnableButtons(False)
                PopulateAll()
                EnableFields(True)
            Else
                State.IsEarningPatternNew = False
                BindBoPropertiesToLabels()
                AddLabelDecorations(TheEarningPattern)
                EnableButtons(True)
                PopulateAll()
                EnableFields(False)
            End If
        End Sub

#End Region

#Region "Constants"

        Public Const DESCRIPTION_PROPERTY As String = "Description"
        Public Const CODE_PROPERTY As String = "Code"
        Public Const EFECTIVE_PROPERTY As String = "Effective"
        Public Const EXPIRATION_PROPERTY As String = "Expiration"
        Public Const EARNING_CODE_ID_PROPERTY As String = "EarningCodeId"
        Public Const EARNING_PATTERN_START_ON_PROPERTY As String = "EarningPatternStartsOnId"
        Private Const EARNINGPATTERN_LIST As String = "EarningPatternListForm.aspx"
        Public Const URL As String = "EarningPatternForm.aspx"
        Public Const COL_EARNING_TERM As String = "EarningTermConstant"
        Public Const COL_EARNING_PERCENT As String = "EarningPercentConstant"
        Private Const INCORRECT_FORMAT As String = "Input string was not in a correct format."

#End Region

#Region "Earning Percentage Constants"

        Private Const EarningPercentIdConstant As Integer = 2
        Private Const EarningTermConstant As Integer = 3
        Private Const EarningPercentConstant As Integer = 4

        ' DataView Elements
        Private Const DBEARNING_PERCENT_ID As Integer = 0
        Private Const DBEARNING_TERM As Integer = 1
        Private Const DBEARNING_PERCENT As Integer = 2
        ' Property Name


        Private Const EARNING_TERM_PROPERTY As String = "EarningTerm"
        Private Const EARNING_PERCENT_PROPERTY As String = "EarningPercent"
        
        'Actions
        Private Const ACTION_NONE As String = "ACTION_NONE"
        Private Const ACTION_SAVE As String = "ACTION_SAVE"
        Private Const ACTION_CANCEL_DELETE As String = "ACTION_CANCEL_DELETE"
        Private Const ACTION_EDIT As String = "ACTION_EDIT"
        Private Const ACTION_NEW As String = "ACTION_NEW"

#End Region

#Region "Attributes"
        Private msCommand As String
        Private moEarningPattern As EarningPattern
        Private moEarningPercent As EarningPercent
        Private mbIsNewPercent As Boolean
#End Region

#Region "Properties"

        Private ReadOnly Property TheEarningPattern() As EarningPattern
            Get

                If moEarningPattern Is Nothing Then
                    If State.IsEarningPatternNew = True Then
                        ' For creating, inserting
                        moEarningPattern = New EarningPattern
                        State.moEarningPatternId = moEarningPattern.Id
                    Else
                        ' For updating, deleting
                        moEarningPattern = New EarningPattern(State.moEarningPatternId)
                    End If
                End If

                Return moEarningPattern
            End Get
        End Property

#End Region

#Region "Earning Percent Properties"

        Private ReadOnly Property TheEarningPercent() As EarningPercent
            Get

                If moEarningPercent Is Nothing Then
                    If IsNewPercent = True Then
                        ' For creating, inserting
                        moEarningPercent = New EarningPercent
                        EarningPercentID = moEarningPercent.Id.ToString
                    Else
                        ' For updating, deleting
                        If EarningPercentID = "" Then
                            EarningPercentID = Guid.Empty.ToString
                        End If
                        moEarningPercent = New EarningPercent(GetGuidFromString(EarningPercentID))
                    End If
                End If

                Return moEarningPercent
            End Get

        End Property

        Private ReadOnly Property TheEarningPercentNWC() As EarningPercent
            Get

                If moEarningPercent Is Nothing Then
                    ' For creating, inserting
                    moEarningPercent = New EarningPercent
                    EarningPercentID = moEarningPercent.Id.ToString
                End If

                Return moEarningPercent
            End Get

        End Property
        Private Property EarningPercentID() As String
            Get
                Return moEarningPercentIDLabel.Text
            End Get
            Set(Value As String)
                moEarningPercentIDLabel.Text = Value
            End Set
        End Property

        Private Property IsNewPercent() As Boolean
            Get
                Return Convert.ToBoolean(moIsNewPercentLabel.Text)
            End Get
            Set(Value As Boolean)
                moIsNewPercentLabel.Text = Value.ToString
            End Set
        End Property


#End Region

#Region "Handlers"

#Region "Handlers-Init"

        Protected WithEvents moErrorController As ErrorController
        Protected WithEvents moErrorControllerPercent As ErrorController

        Protected WithEvents moPanel As System.Web.UI.WebControls.Panel
        Protected WithEvents moPercentLabel As System.Web.UI.WebControls.Label
        Protected WithEvents BtnDeletePercent As System.Web.UI.WebControls.Button
        Protected WithEvents EditPanel_WRITE As System.Web.UI.WebControls.Panel
        Protected WithEvents Button1 As System.Web.UI.WebControls.Button
        Protected WithEvents moEarningPatternEditPanel As System.Web.UI.WebControls.Panel
        Protected WithEvents Textbox1 As System.Web.UI.WebControls.TextBox
        Protected WithEvents Textbox2 As System.Web.UI.WebControls.TextBox

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

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                'Preserve values during postbacks
                PreserveValues()
                moErrorControllerPercent.Clear_Hide()
                moErrorController.Clear_Hide()
                ClearLabelsErrSign()
                ClearGridHeaders(moDataGrid)
                If Not Page.IsPostBack Then
                    TranslateGridHeader(moDataGrid)
                    SetGridItemStyleColor(moDataGrid)
                    SetStateProperties()
                    AddControlMsg(btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, _
                                                                       MSG_TYPE_CONFIRM, True)
                    AddCalendar(BtnEffectiveDate, moEffectiveText)
                    AddCalendar(BtnExpirationDate, moExpirationText)
                End If
                BindBoPropertiesToLabels()
                CheckIfComingFromConfirm()
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
            ShowMissingTranslations(moErrorController)
        End Sub

#End Region

#Region "Handlers-DropDown"

        Private Sub PreserveValues()
            State.selectedDescription = moDescriptionText.Text
            State.selectedEffective = moEffectiveText.Text
            State.selectedExpiration = moExpirationText.Text
            State.selectedCode = moCodeText.Text
        End Sub

        Private Sub moCodeDrop_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles moCodeDrop.SelectedIndexChanged
            TheEarningPattern.EarningCodeId = GetSelectedItem(moCodeDrop)
            PopulateEarningCode()
        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub SaveChanges()
            If ApplyChanges() = True Then
                State.boChanged = True
                ClearEarningPercent()
                If State.IsEarningPatternNew = True Then
                    State.IsEarningPatternNew = False
                End If
                If State.IsNewWithCopy Then
                    State.IsNewWithCopy = False
                End If
                PopulateAll()
                EnableFields(False)
            End If
        End Sub

        Private Sub btnApply_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnApply_WRITE.Click
            Try
                SaveChanges()
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub GoBack()
            Dim retType As New EarningPatternListForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back, _
                                                                                State.moEarningPatternId, State.boChanged)
            ReturnToCallingPage(retType)
        End Sub

        Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
            Try
                If IsDirtyBO() = True Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, _
                                                HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    GoBack()
                End If
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try
                State.IsUndo = True
                PopulateAll()

                If State.IsNewWithCopy AndAlso State.IsUndo Then
                    State.IsUndo = False
                    State.IsNewWithCopy = False
                    State.IsEarningPatternNew = False
                    EnableButtons(True)
                    EnableFields(False)
                End If
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub CreateNew()
            ClearForNew()
            ClearAll()
            EnableButtons(False)
            PopulateAll()
            EnableFields(True)
        End Sub

        Private Sub ClearForNew()
            State.moEarningPatternId = Guid.Empty
            State.IsEarningPatternNew = True
            moEarningPattern = Nothing
            State.selectedDescription = Nothing
            State.selectedCode = Nothing
            State.selectedEffective = Nothing
            State.selectedExpiration = Nothing
        End Sub

        Private Sub btnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNew_WRITE.Click
            Try
                If IsDirtyBO() = True Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
                Else
                    CreateNew()
                End If
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub CreateNewCopy()

            'Clear Earning Pattern BO
            State.moEarningPatternId = Guid.Empty
            State.IsEarningPatternNew = True
            EnableFields(True)
            State.IsNewWithCopy = True
            SetGridControls(moDataGrid, True)
            EnableButtons(False)
            If State.IsNewWithCopy Then
                EnableNewPercentButtons(False)
            End If
        End Sub

        Private Sub LoadPercentList()
            If State.IsNewWithCopy = False Then
                If moDataGrid.Rows.Count > 0 Then
                    Dim i As Integer = 0
                    Dim oEarningPercent(moDataGrid.Rows.Count - 1) As EarningPercent

                    For i = 0 To moDataGrid.Rows.Count - 1
                        oEarningPercent(i) = New EarningPercent
                        oEarningPercent(i).EarningPatternId = TheEarningPattern.Id

                        If moDataGrid.Rows(i).Cells(EarningTermConstant).Controls(1).GetType().ToString = "System.Web.UI.WebControls.Label" Then
                            PopulateBOProperty(oEarningPercent(i), "EarningTerm", CType(moDataGrid.Rows(i).Cells(EarningTermConstant).Controls(1), Label).Text)
                        Else
                            PopulateBOProperty(oEarningPercent(i), "EarningTerm", CType(moDataGrid.Rows(i).Cells(EarningTermConstant).Controls(1), TextBox).Text)
                        End If
                        If moDataGrid.Rows(i).Cells(EarningPercentConstant).Controls(1).GetType().ToString = "System.Web.UI.WebControls.Label" Then
                            PopulateBOProperty(oEarningPercent(i), "EarningPercent", CType(moDataGrid.Rows(i).Cells(EarningPercentConstant).Controls(1), Label).Text)
                        Else
                            PopulateBOProperty(oEarningPercent(i), "EarningPercent", CType(moDataGrid.Rows(i).Cells(EarningPercentConstant).Controls(1), TextBox).Text)
                        End If
                    Next
                    State.moPercentList = oEarningPercent
                End If
            Else
                Dim oEarnPrct As EarningPercent
                Dim oDataViewOld As DataView, j As Integer

                oDataViewOld = oEarnPrct.GetList(State.moOldEarningPatternId) 'Get data from existing pattern
                If oDataViewOld.Count > 0 Then
                    Dim oEarningPrct(oDataViewOld.Count - 1) As EarningPercent
                    For j = 0 To oDataViewOld.Count - 1
                        oEarningPrct(j) = New EarningPercent
                        oEarningPrct(j).EarningPatternId = TheEarningPattern.Id
                        PopulateBOProperty(oEarningPrct(j), "EarningTerm", oDataViewOld(j)(COL_EARNING_TERM).ToString)
                        PopulateBOProperty(oEarningPrct(j), "EarningPercent", oDataViewOld(j)(COL_EARNING_PERCENT).ToString)
                    Next
                    State.moPercentList = oEarningPrct
                End If
            End If
        End Sub

        Public Function SavePercentList() As Boolean
            Dim i As Integer = 0
            Try
                If State.IsNewWithCopy = True AndAlso State.moPercentList IsNot Nothing Then
                    'Associate each detail record to the newly created pattern record
                    'and Save each detail Record
                    For i = 0 To State.moPercentList.Length - 1
                        State.moPercentList(i).EarningPatternId = TheEarningPattern.Id
                        State.moPercentList(i).Save()
                    Next
                End If
            Catch ex As Exception
                Dim j As Integer
                'REPLACE THIS LOOP BY A DB ROLLBACK
                For j = 0 To i - 1
                    State.moPercentList(j).Delete()
                    State.moPercentList(j).Save()
                Next
                HandleErrors(ex, moErrorControllerPercent)
                Return False
            End Try
            Return True
        End Function

        Private Sub btnCopy_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnCopy_WRITE.Click
            Try
                If IsDirtyBO() = True Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
                Else
                    CreateNewCopy()
                End If
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub btnDelete_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete_WRITE.Click
            Try
                If DeleteEarningPattern() = True Then
                    State.boChanged = True
                    Dim retType As New EarningPatternListForm.ReturnType(ElitaPlusPage.DetailPageCommand.Delete, _
                                    State.moEarningPatternId)
                    retType.BoChanged = True
                    ReturnToCallingPage(retType)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

#End Region

#Region "Handlers-Labels"

        Private Sub BindBoPropertiesToLabels()
            BindBOPropertyToLabel(TheEarningPattern, DESCRIPTION_PROPERTY, moDescriptionLabel)
            BindBOPropertyToLabel(TheEarningPattern, CODE_PROPERTY, moCodeLabel)
            BindBOPropertyToLabel(TheEarningPattern, EFECTIVE_PROPERTY, moEffectiveLabel)
            BindBOPropertyToLabel(TheEarningPattern, EXPIRATION_PROPERTY, moExpirationLabel)
            BindBOPropertyToLabel(TheEarningPattern, EARNING_CODE_ID_PROPERTY, moCodeLabel)
            BindBOPropertyToLabel(TheEarningPattern, EARNING_PATTERN_START_ON_PROPERTY, EPStartsOnLabel)
        End Sub

        Private Sub ClearLabelsErrSign()
            ClearLabelErrSign(moDescriptionLabel)
            ClearLabelErrSign(moCodeLabel)
            ClearLabelErrSign(moEffectiveLabel)
            ClearLabelErrSign(moExpirationLabel)
        End Sub

        Private Sub BindBoPropertiesToGridHeader()
            BindBOPropertyToGridHeader(TheEarningPercent, EARNING_TERM_PROPERTY, moDataGrid.Columns(EarningTermConstant))
            BindBOPropertyToGridHeader(TheEarningPercent, EARNING_PERCENT_PROPERTY, moDataGrid.Columns(EarningPercentConstant))
        End Sub

#End Region

#End Region

#Region "Handlers-Earning-Percentage"

#Region "Handlers-Earning-Percentage-DataGrid"

        Public Property SortDirection() As String
            Get
                Return ViewState("SortDirection").ToString
            End Get
            Set(value As String)
                ViewState("SortDirection") = value
            End Set
        End Property

        ' Earning-Percent DataGrid
        Public Sub ItemCreated(sender As Object, e As GridViewRowEventArgs)
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub moDataGrid_PageIndexChanged(source As System.Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles moDataGrid.PageIndexChanging
            Try
                ResetIndexes()
                moDataGrid.PageIndex = e.NewPageIndex
                PopulatePercentList(ACTION_CANCEL_DELETE)
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

        'The pencil was clicked
        Sub ItemCommand(source As System.Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)
            Dim nIndex As Integer
            Try
                If State.IsNewWithCopy = True Then
                    Exit Sub
                End If

                If e.CommandName = EDIT_COMMAND_NAME Then
                    State.IsEditMode = True
                    nIndex = CInt(e.CommandArgument)
                    moDataGrid.EditIndex = nIndex
                    moDataGrid.SelectedIndex = nIndex
                    Dim gd As String = CType(moDataGrid.Rows(nIndex).Cells(DBEARNING_PERCENT_ID).FindControl("moEARNING_PERCENT_ID"), Label).Text
                    State.selectedEarningPercentId = New Guid(gd)
                    '      EnableForEditRateButtons(True)
                    PopulatePercentList(ACTION_EDIT)
                    PopulateEarningPercent()
                    SetGridControls(moDataGrid, False)
                    SetFocusInGrid(moDataGrid, nIndex, EarningPercentConstant)
                    EnableDisableControls(moEarningPatternEditPanel, True)
                    setbuttons(False)
                ElseIf (e.CommandName = DELETE_COMMAND_NAME) Then
                    nIndex = CInt(e.CommandArgument)
                    EarningPercentID = GetGridText(moDataGrid, nIndex, EarningPercentIdConstant)
                    If DeleteSelectedPercent(nIndex) = True Then
                        PopulatePercentList(ACTION_CANCEL_DELETE)
                    End If

                End If
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub
        'The Binding Logic is here
        Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles moDataGrid.RowDataBound
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

            If dvRow IsNot Nothing AndAlso Not State.bnoRow Then

                If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                    CType(e.Row.Cells(DBEARNING_PERCENT_ID).FindControl("moEARNING_PERCENT_ID"), Label).Text = GetGuidStringFromByteArray(CType(dvRow(DBEARNING_PERCENT_ID), Byte()))
                    If (State.IsEditMode = True _
                            AndAlso State.selectedEarningPercentId.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow(DBEARNING_PERCENT_ID), Byte())))) Then
                        CType(e.Row.Cells(DBEARNING_TERM).FindControl("moEarningTermLabel"), Label).Text = dvRow(DBEARNING_TERM).ToString
                        CType(e.Row.Cells(DBEARNING_PERCENT).FindControl("moEarningPercentText"), TextBox).Text = dvRow(DBEARNING_PERCENT).ToString
                    Else
                        CType(e.Row.Cells(DBEARNING_TERM).FindControl("moEarningTermLabel"), Label).Text = dvRow(DBEARNING_TERM).ToString
                        CType(e.Row.Cells(DBEARNING_PERCENT).FindControl("moEarningPercentLabel"), Label).Text = dvRow(DBEARNING_PERCENT).ToString

                    End If
                    'e.Row.Cells(Me.ACCT_COMPANY_DESCRIPTION_COL).Text = dvRow(AcctBusinessUnit.AcctBusinessUnitSearchDV.COL_ACCT_COMPANY_DESCRIPTION).ToString
                End If
            End If
        End Sub
        Protected Sub ItemBound(source As Object, e As GridViewRowEventArgs) Handles moDataGrid.RowDataBound
            Try
                BaseItemBound(source, e)
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try

        End Sub

        Private Sub ResetIndexes()
            moDataGrid.EditIndex = NO_ITEM_SELECTED_INDEX
            moDataGrid.SelectedIndex = NO_ITEM_SELECTED_INDEX
        End Sub

#End Region

#Region "Handlers-Percentage-Buttons"

        Private Sub setbuttons(enable As Boolean)
            ControlMgr.SetEnableControl(Me, btnBack, enable)
            ControlMgr.SetEnableControl(Me, btnApply_WRITE, enable)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, enable)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, enable)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, enable)
            ControlMgr.SetEnableControl(Me, btnUndo_WRITE, enable)
            ControlMgr.SetVisibleControl(Me, BtnEffectiveDate, enable)
            ControlMgr.SetVisibleControl(Me, BtnExpirationDate, enable)
        End Sub

        Private Sub SavePercentChanges()
            If ApplyPercentChanges() = True Then
                If IsNewPercent = True Then
                    IsNewPercent = False
                End If
                PopulatePercentList(ACTION_SAVE)
                EnableDisableControls(moEarningPatternEditPanel, False)
                setbuttons(True)
            End If
        End Sub

        Private Sub BtnSavePercent_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles BtnSavePercent_WRITE.Click
            Try
                SavePercentChanges()
            Catch ex As Exception
                If (ex.InnerException IsNot Nothing) AndAlso (ex.InnerException.Message = INCORRECT_FORMAT) Then
                    moErrorControllerPercent.AddError(Assurant.ElitaPlus.Common.ErrorCodes.GUI_PERCENT_MUST_BE_NUMERIC_ERR)
                    moErrorControllerPercent.Show()
                Else
                    HandleErrors(ex, moErrorControllerPercent)
                End If
            End Try
        End Sub

        Private Sub BtnCancelPercent_Click(sender As System.Object, e As System.EventArgs) Handles BtnCancelPercent.Click
            'Pencil button in not in edit mode
            Try
                IsNewPercent = False
                EnableForEditPercentButtons(False)
                PopulatePercentList(ACTION_CANCEL_DELETE)
                EnableDisableControls(moEarningPatternEditPanel, False)
                setbuttons(True)
            Catch ex As Exception
                HandleErrors(ex, moErrorControllerPercent)
            End Try
        End Sub

        Private Sub BtnNewPercent_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNewPercent_WRITE.Click
            Try
                IsNewPercent = True
                EarningPercentID = Guid.Empty.ToString
                PopulatePercentList(ACTION_NEW)
                SetGridControls(moDataGrid, False)
                SetFocusInGrid(moDataGrid, moDataGrid.SelectedIndex, EarningPercentConstant)
                EnableDisableControls(moEarningPatternEditPanel, True)
                setbuttons(False)
            Catch ex As Exception
                HandleErrors(ex, moErrorControllerPercent)
            End Try
        End Sub

#End Region

#End Region

#Region "Button Management"

        Private Sub EnableEffective(bIsEnable As Boolean)
            ControlMgr.SetEnableControl(Me, moEffectiveText, bIsEnable)
            ControlMgr.SetVisibleControl(Me, BtnEffectiveDate, bIsEnable)
        End Sub

        Private Sub EnableExpiration(bIsEnable As Boolean)
            ControlMgr.SetEnableControl(Me, moExpirationText, bIsEnable)
            ControlMgr.SetVisibleControl(Me, BtnExpirationDate, bIsEnable)
        End Sub

        Private Sub EnableFields(bIsEnable As Boolean)
            ControlMgr.SetEnableControl(Me, moCodeDrop, bIsEnable)
            EnableEffective(True)
            EnableExpiration(True)
        End Sub

        Private Sub EnableButtons(bIsReadWrite As Boolean)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, bIsReadWrite)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, bIsReadWrite)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, bIsReadWrite)
        End Sub

#End Region

#Region "Clear"

        Private Sub ClearEarningPercent()
            If Not State.IsNewWithCopy Then
                EnablePercentButtons(False)
                'Pencil button in not in edit mode
                '   ResetIndexes()
                moDataGrid.DataSource = Nothing
                moDataGrid.DataBind()
            End If
        End Sub

        Private Sub ClearFields()
            If Not State.IsNewWithCopy Then
                ClearList(moCodeDrop)
                ClearList(moEPStartsOnDrop)
                ClearEarningPercent()
                moCodeText.Text = Nothing
                moDescriptionText.Text = Nothing
                moEffectiveText.Text = Nothing
                moExpirationText.Text = Nothing
            End If
        End Sub

        Private Sub ClearAll()
            ClearFields()
        End Sub

#End Region

#Region "Populate"

        Private Sub PopulateAll()
            EnablePercentButtons(False)
            PopulateEarningCode()
            PopulateFields()
            IsNewPercent = False
            PopulatePercentList()
        End Sub


        Private Sub PopulateEarningCode()
            'Dim oCompanyGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            'Dim oEarningCodeDV As DataView = LookupListNew.GetEarningCodeLookupList(oCompanyGroupId)
            'Dim oEPSODV As DataView = LookupListNew.GetEarningPatternStartsOnLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
            oListContext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            Dim oEarningCode As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="EarningCodesByCompanyGroup", context:=oListContext)
            Dim earningPatterns As ListItem() = CommonConfigManager.Current.ListManager.GetList("EPSO", Thread.CurrentPrincipal.GetLanguageCode())

            If State.IsNewWithCopy AndAlso State.IsUndo Then
                State.moEarningPatternId = State.moOldEarningPatternId
                moEarningPattern = New EarningPattern(State.moEarningPatternId)
            End If

            moCodeDrop.Populate(oEarningCode, New PopulateOptions() With
                {
                    .AddBlankItem = True,
                    .TextFunc = AddressOf .GetCode,
                    .SortFunc = AddressOf .GetCode
                })

            'oEarningCodeDV.Sort = Me.CODE_PROPERTY
            'Me.BindListControlToDataView(moCodeDrop, oEarningCodeDV, "CODE", , True)

            PopulateControlFromBOProperty(moCodeDrop, TheEarningPattern.EarningCodeId)

            If moCodeDrop.SelectedIndex > 0 Then
                moDescriptionText.Text = oEarningCode.ElementAt(moCodeDrop.SelectedIndex - 1).Translation
                'moDescriptionText.Text = oEarningCodeDV.Item(moCodeDrop.SelectedIndex - 1).Item("DESCRIPTION").ToString
            Else
                moDescriptionText.Text = Nothing
            End If

            'oEPSODV.Sort = Me.CODE_PROPERTY
            'Me.BindListControlToDataView(moEPStartsOnDrop, oEPSODV, "DESCRIPTION", , True)
            moEPStartsOnDrop.Populate(earningPatterns, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True,
                                                        .SortFunc = AddressOf .GetCode
                                                       })

            PopulateControlFromBOProperty(moEPStartsOnDrop, TheEarningPattern.EarningPatternStartsOnId)
        End Sub

        Private Sub PopulateFields()
            Try
                If IsPostBack AndAlso Not State.IsUndo Then
                    ' JLR - Restore Presviously Selected Values
                    moDescriptionText.Text = State.selectedDescription
                    moCodeText.Text = State.selectedCode
                    moEffectiveText.Text = State.selectedEffective
                    moExpirationText.Text = State.selectedExpiration
                    State.IsUndo = False
                Else
                    ' JLR - Otherwise load values from BO unless it is new with copy
                    ' In that case, BO has been cleared but we want to preserve the values 
                    ' already in the screen
                    If Not State.IsNewWithCopy Then
                        'JLR==> PopulateControlFromBOProperty(moDescriptionText, TheEarningPattern.Description)
                        'JLR==> PopulateControlFromBOProperty(moCodeText, TheEarningPattern.Code)
                        If TheEarningPattern.Effective Is Nothing Then
                            PopulateControlFromBOProperty(moEffectiveText, TheEarningPattern.Effective)
                        Else
                            PopulateControlFromBOProperty(moEffectiveText, TheEarningPattern.Effective.Value.ToString("dd-MMM-yyyy", CultureInfo.CurrentCulture))
                        End If
                        If TheEarningPattern.Expiration Is Nothing Then
                            PopulateControlFromBOProperty(moExpirationText, TheEarningPattern.Expiration)
                        Else
                            PopulateControlFromBOProperty(moExpirationText, TheEarningPattern.Expiration.Value.ToString("dd-MMM-yyyy", CultureInfo.CurrentCulture))
                        End If
                    Else
                        If State.IsUndo = True Then
                            If TheEarningPattern.Effective Is Nothing Then
                                PopulateControlFromBOProperty(moEffectiveText, TheEarningPattern.Effective)
                            Else
                                PopulateControlFromBOProperty(moEffectiveText, TheEarningPattern.Effective.Value.ToString("dd-MMM-yyyy", CultureInfo.CurrentCulture))
                            End If
                            If TheEarningPattern.Expiration Is Nothing Then
                                PopulateControlFromBOProperty(moExpirationText, TheEarningPattern.Expiration)
                            Else
                                PopulateControlFromBOProperty(moExpirationText, TheEarningPattern.Expiration.Value.ToString("dd-MMM-yyyy", CultureInfo.CurrentCulture))
                            End If
                        End If
                    End If
                End If
            Catch ex As Exception
                moErrorController.AddError(ex.Message)
                moErrorController.Show()
            End Try
        End Sub

#End Region

#Region "Business Part"

        Private Sub PopulateBOsFromForm()

            ' If using the Textbox instead of the Dropdown
            ' Me.PopulateBOProperty(TheEarningPattern, "Code", moCodeText)
            ' Me.State.moEarningCodeId = LookupListNew.GetIdFromCode(LookupListNew.LK_EARNING_CODES, moCodeText.Text)
            ' TheEarningPattern.EarningCodeId = Me.State.moEarningCodeId

            PopulateBOProperty(TheEarningPattern, "EarningCodeId", moCodeDrop)
            If Microsoft.VisualBasic.IsDate(moEffectiveText.Text) Then PopulateBOProperty(TheEarningPattern, "Effective", CType(moEffectiveText.Text, Date).ToString) Else PopulateBOProperty(TheEarningPattern, "Effective", "")
            If Microsoft.VisualBasic.IsDate(moExpirationText.Text) Then PopulateBOProperty(TheEarningPattern, "Expiration", CType(moExpirationText.Text, Date).ToString) Else PopulateBOProperty(TheEarningPattern, "Expiration", "")
            PopulateBOProperty(TheEarningPattern, "EarningPatternStartsOnId", moEPStartsOnDrop)
        End Sub

        Private Function IsDirtyBO() As Boolean
            Dim bIsDirty As Boolean = True
            Try
                '** TheEarningPattern.UniqueFieldsChanged = Me.UniqueFieldsChanged()
                With TheEarningPattern
                    PopulateBOsFromForm()
                    bIsDirty = .IsDirty
                    If bIsDirty = False Then bIsDirty = IsDirtyPercentBO()
                End With
            Catch ex As Exception
                moErrorController.AddError(ex.Message, False)
                moErrorController.Show()
            End Try
            Return bIsDirty
        End Function

        Private Function ApplyChanges() As Boolean
            Dim bIsOk As Boolean = True
            Dim bIsDirty As Boolean = False

            With TheEarningPattern
                bIsDirty = IsDirtyBO()
                .Save()
                LoadPercentList()
                If SavePercentList() Then
                    EnableButtons(True)
                Else
                    'REPLACE THIS WITH A DB ROLLBACK
                    .Delete()
                    .Save()
                    bIsOk = False
                End If
            End With
            If (bIsOk = True) Then
                If bIsDirty = True Then
                    AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                Else
                    AddInfoMsg(Message.MSG_RECORD_NOT_SAVED)
                End If
            End If
            Return bIsOk
        End Function

        Private Function DeleteEarningPattern() As Boolean
            Dim bIsOk As Boolean = True

            Try
                With TheEarningPattern
                    PopulateBOsFromForm()
                    If .IsLastPattern() = False AndAlso .IsFirstPattern = False Then
                        moErrorController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.EARNING_PATTERN_CANNOT_BE_DELETED)
                        moErrorController.Show()
                        .cancelEdit()
                        bIsOk = False
                    ElseIf DeleteAllPercent() = False Then
                        ' Error in Percent
                        .cancelEdit()
                        bIsOk = False
                    Else
                        .Delete()
                        .Save()
                    End If
                End With
            Catch ex As Exception
                moErrorController.AddError(ex.Message, False)
                moErrorController.Show()
                bIsOk = False
            End Try
            Return bIsOk
        End Function

        Private Function UniqueFieldsChanged() As Boolean
            Dim moEffectiveDate As Date
            Dim moExpirationDate As Date
            If Microsoft.VisualBasic.IsDate(moEffectiveText.Text) Then moEffectiveDate = CType(moEffectiveText.Text, Date)
            If Microsoft.VisualBasic.IsDate(moExpirationText.Text) Then moExpirationDate = CType(moExpirationText.Text, Date)

            With TheEarningPattern
                If State.IsEarningPatternNew Then
                    Return False
                Else
                    If CType(.Effective.ToString, Date) <> moEffectiveDate _
                    OrElse CType(.Expiration.ToString, Date) <> moExpirationDate Then
                        Return True
                    Else
                        Return False
                    End If
                End If
            End With
        End Function

#End Region

#Region "Earning-Percent"

#Region "Earning-Percent Button Management"

        Private Sub EnableEditPercentButtons(bIsReadWrite As Boolean)
            ControlMgr.SetEnableControl(Me, BtnSavePercent_WRITE, bIsReadWrite)
            ControlMgr.SetEnableControl(Me, BtnCancelPercent, bIsReadWrite)
        End Sub

        Private Sub EnableNewPercentButtons(bIsReadWrite As Boolean)
            ControlMgr.SetEnableControl(Me, btnNewPercent_WRITE, bIsReadWrite)
        End Sub

        Private Sub EnablePercentButtons(bIsReadWrite As Boolean)
            EnableNewPercentButtons(bIsReadWrite)
            EnableEditPercentButtons(bIsReadWrite)
        End Sub

        Private Sub EnableForEditPercentButtons(bIsReadWrite As Boolean)
            EnableNewPercentButtons(Not bIsReadWrite)
            EnableEditPercentButtons(bIsReadWrite)
        End Sub

#End Region

#Region "Populate"

        Private Sub PopulatePercentList(Optional ByVal oAction As String = ACTION_NONE)
            Dim oEarningPercent As EarningPercent
            Dim oDataView As DataView, oDataViewOld As DataView

            If State.IsEarningPatternNew = True AndAlso Not State.IsNewWithCopy Then Return

            Try

                If State.IsNewWithCopy Then
                    If State.IsUndo = True Then
                        oDataView = oEarningPercent.GetList(TheEarningPattern.Id)
                    Else
                        oDataViewOld = oEarningPercent.GetList(State.moOldEarningPatternId)
                        oDataView = oEarningPercent.GetList(Guid.Empty)
                        If Not oAction = ACTION_CANCEL_DELETE Then
                            LoadPercentList()
                        End If
                        oDataView = getDataFromExistingPattern(oDataViewOld, oDataView.Table) 'getDVFromArray(Me.State.moPercentList, oDataView.Table)
                    End If
                Else
                        oDataView = oEarningPercent.GetList(TheEarningPattern.Id)
                End If

                Select Case oAction
                    Case ACTION_NONE
                        SetPageAndSelectedIndexFromGuid(oDataView, Guid.Empty, moDataGrid, 0)
                        EnableForEditPercentButtons(False)
                    Case ACTION_SAVE
                        SetPageAndSelectedIndexFromGuid(oDataView, GetGuidFromString(EarningPercentID), moDataGrid, _
                                    moDataGrid.PageIndex)
                        EnableForEditPercentButtons(False)
                        State.IsEditMode = False
                    Case ACTION_CANCEL_DELETE
                        State.IsEditMode = False
                        SetPageAndSelectedIndexFromGuid(oDataView, Guid.Empty, moDataGrid, _
                                    moDataGrid.PageIndex)
                        EnableForEditPercentButtons(False)
                        If State.IsNewWithCopy Then
                            EnableNewPercentButtons(False)
                        End If

                    Case ACTION_EDIT
                        If State.IsNewWithCopy Then
                            EarningPercentID = State.moPercentList(moDataGrid.SelectedIndex).Id.ToString
                        Else
                            EarningPercentID = GetGridText(moDataGrid, moDataGrid.SelectedIndex, EarningPercentIdConstant)
                        End If
                        SetPageAndSelectedIndexFromGuid(oDataView, GetGuidFromString(EarningPercentID), moDataGrid, _
                                    moDataGrid.PageIndex, True)
                        EnableForEditPercentButtons(True)
                    Case ACTION_NEW
                        If State.IsNewWithCopy Then oDataView.Table.DefaultView.Sort() = Nothing ' Clear sort, so that the new line shows up at the bottom
                        Dim oRow As DataRow = oDataView.Table.NewRow
                        oRow(DBEARNING_PERCENT_ID) = TheEarningPercent.Id.ToByteArray
                        oRow(DBEARNING_TERM) = oDataView.Count + 1
                        State.IsEditMode = True
                        State.selectedEarningPercentId = GetGuidFromString(GetGuidStringFromByteArray(CType(oRow(DBEARNING_PERCENT_ID), Byte())))
                        oDataView.Table.Rows.Add(oRow)
                        SetPageAndSelectedIndexFromGuid(oDataView, GetGuidFromString(EarningPercentID), moDataGrid, _
                                    moDataGrid.PageIndex, True)
                        EnableForEditPercentButtons(True)

                End Select

                ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, moDataGrid)

                moDataGrid.DataSource = oDataView
                moDataGrid.DataBind()


            Catch ex As Exception
                moErrorControllerPercent.AddError(ex.Message, False)
                moErrorControllerPercent.Show()
            End Try
        End Sub

        Private Function getDVFromArray(oArray() As EarningPercent, oDtable As DataTable) As DataView

            Dim oRow As DataRow
            Dim oEarningPercent As EarningPercent
            For Each oEarningPercent In oArray
                If oEarningPercent IsNot Nothing Then
                    oRow = oDtable.NewRow
                    oRow(COL_EARNING_TERM) = oEarningPercent.EarningTerm.Value
                    oRow(COL_EARNING_PERCENT) = oEarningPercent.EarningPercent.Value
                    oDtable.Rows.Add(oRow)
                End If
            Next
            oDtable.DefaultView.Sort() = COL_EARNING_TERM
            Return oDtable.DefaultView

        End Function

        Private Function getDataFromExistingPattern(dv As DataView, oDtable As DataTable) As DataView
            Dim i As Integer
            Dim oRow As DataRow
            If dv.Count = 0 Then
                Exit Function
            End If

            For i = 0 To dv.Count - 1
                oRow = oDtable.NewRow
                oRow(DBEARNING_PERCENT_ID) = TheEarningPercentNWC.Id.ToByteArray
                oRow(DBEARNING_TERM) = dv(i)(COL_EARNING_TERM)
                oRow(DBEARNING_PERCENT) = dv(i)(COL_EARNING_PERCENT)
                oDtable.Rows.Add(oRow)
            Next
            oDtable.DefaultView.Sort() = COL_EARNING_TERM
            Return oDtable.DefaultView
        End Function

        Private Sub ModifyGridHeader()

            moDataGrid.Columns(EarningTermConstant).HeaderText = moDataGrid.Columns(EarningTermConstant).HeaderText.Replace("%", "<br>%")
            moDataGrid.Columns(EarningPercentConstant).HeaderText = moDataGrid.Columns(EarningPercentConstant).HeaderText.Replace("%", "<br>%")

        End Sub

        Private Sub PopulateEarningPercent()
            If State.IsNewWithCopy Then
                With State.moPercentList(moDataGrid.SelectedIndex)
                    SetSelectedGridText(moDataGrid, EarningTermConstant, .EarningTerm.ToString)
                    SetSelectedGridText(moDataGrid, EarningPercentConstant, .EarningPercent.ToString)
                End With
            Else
                With TheEarningPercent
                    SetSelectedGridText(moDataGrid, EarningTermConstant, .EarningTerm.ToString)
                    SetSelectedGridText(moDataGrid, EarningPercentConstant, .EarningPercent.Value.ToString)
                End With
            End If

        End Sub

#End Region

#Region "Business Part"

        Private Sub PopulatePercentBOFromForm()
            With TheEarningPercent
                .EarningPatternId = TheEarningPattern.Id
                PopulateBOProperty(TheEarningPercent, "EarningTerm", GetGridText(moDataGrid, moDataGrid.SelectedIndex, EarningTermConstant))
                PopulateBOProperty(TheEarningPercent, "EarningPercent", GetGridText(moDataGrid, moDataGrid.SelectedIndex, EarningPercentConstant))
            End With
        End Sub

        Private Function IsDirtyPercentBO() As Boolean
            Dim bIsDirty As Boolean = True
            If moDataGrid.EditIndex = NO_ITEM_SELECTED_INDEX Then Return False
            Dim sEarningPercentID As String = GetGridText(moDataGrid, moDataGrid.SelectedIndex, EarningPercentIdConstant)
            Try
                With TheEarningPercent
                    PopulatePercentBOFromForm()
                    bIsDirty = .IsDirty
                End With
            Catch ex As Exception
                moErrorControllerPercent.AddError(ex.Message, False)
                moErrorControllerPercent.Show()
            End Try
            Return bIsDirty
        End Function

        Private Function ApplyPercentChanges() As Boolean
            Dim bIsOk As Boolean = True
            Dim bIsDirty As Boolean
            If moDataGrid.EditIndex < 0 Then Return False
            If State.IsNewWithCopy Then
                LoadPercentList()
                State.moPercentList(moDataGrid.SelectedIndex).Validate()
                Return bIsOk
            End If
            If IsNewPercent = False Then
                EarningPercentID = GetGridText(moDataGrid, moDataGrid.SelectedIndex, EarningPercentIdConstant)
            End If
            BindBoPropertiesToGridHeader()
            With TheEarningPercent
                PopulatePercentBOFromForm()
                bIsDirty = .IsDirty
                .Save()
                EnableForEditPercentButtons(False)
            End With
            If (bIsOk = True) Then
                If bIsDirty = True Then
                    AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                Else
                    AddInfoMsg(Message.MSG_RECORD_NOT_SAVED)
                End If
            End If
            Return bIsOk
        End Function

        ' The user selected a specific Percentage to Delete
        Private Function DeleteSelectedPercent(nIndex As Integer) As Boolean
            Dim bIsOk As Boolean = True
            Try
                If State.IsNewWithCopy Then
                    If State.moPercentList Is Nothing Then LoadPercentList()
                    State.moPercentList(nIndex) = Nothing
                Else
                    With TheEarningPercent()
                        If .IsLastterm() = False Then
                            moErrorController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.EARNING_PERCENT_CANNOT_BE_DELETED)
                            moErrorController.Show()
                            .cancelEdit()
                            bIsOk = False
                        Else
                            .Delete()
                            .Save()
                        End If

                    End With
                End If

            Catch ex As Exception
                moErrorControllerPercent.AddError(ex.Message, False)
                moErrorControllerPercent.Show()
                bIsOk = False
            End Try
            Return bIsOk
        End Function

        ' Delete a Percentage from a DataView Row
        Private Function DeleteAPercent(oRow As DataRow) As Boolean
            Dim bIsOk As Boolean = True
            Try
                Dim oEarningPercent As EarningPercent = New EarningPercent(New Guid(CType(oRow(DBEARNING_PERCENT_ID), Byte())))
                oEarningPercent.Delete()
                oEarningPercent.Save()
            Catch ex As Exception
                moErrorControllerPercent.AddError(ex.Message)
                moErrorControllerPercent.Show()
                bIsOk = False
            End Try
            Return bIsOk
        End Function

        Private Function DeleteAllPercent() As Boolean
            Dim bIsOk As Boolean = True
            Dim oDataView As DataView
            Dim oRows As DataRowCollection
            Dim oRow As DataRow

            Try
                oRows = TheEarningPercent.GetList(TheEarningPattern.Id).Table.Rows
                For Each oRow In oRows
                    If DeleteAPercent(oRow) = False Then Return False
                Next
            Catch ex As Exception
                moErrorControllerPercent.AddError(ex.Message)
                moErrorControllerPercent.Show()
                bIsOk = False
            End Try
            Return bIsOk
        End Function
#End Region

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
                        ' Go back to Search Page
                        GoBack()
                End Select
            End If

        End Sub


        Protected Sub CheckIfComingFromConfirm()
            ComingFromBack()
            'Clean after consuming the action
            HiddenSaveChangesPromptResponse.Value = String.Empty
        End Sub

#End Region

    End Class
End Namespace