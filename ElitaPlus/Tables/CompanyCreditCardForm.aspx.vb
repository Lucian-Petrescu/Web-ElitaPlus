Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.Web.Forms
Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.Security

Namespace Tables
    Partial Class CompanyCreditCardForm
        Inherits ElitaPlusSearchPage


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

#Region "Page State"
        Class MyState
            Public IsNew As Boolean
            Public AddingNewRow As Boolean
            Public searchDV As CompanyCreditCard.CompanyCreditCardSearchDV = Nothing
            Public newCompanyCreditCardId As Guid
            Public SortExpression As String = CompanyCreditCardDAL.COL_NAME_CREDIT_CARD_TYPE
            Public IsEditMode As Boolean
            Public IsAfterSave As Boolean
            Public PageSize As Integer = DEFAULT_PAGE_SIZE
            Public Id As Guid
            Public MyBO As CompanyCreditCard
            Public CompanyCreditCardId As Guid
            Public PageIndex As Integer = 0
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

#Region "Page Return"

        Public Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
            Try
                IsReturningFromChild = True
                Dim retObj As ReturnType = CType(ReturnPar, ReturnType)
                If retObj IsNot Nothing AndAlso retObj.BoChanged Then
                    State.searchDV = Nothing
                End If

                If retObj IsNot Nothing Then
                    Select Case retObj.LastOperation
                        Case ElitaPlusPage.DetailPageCommand.Back
                            State.CompanyCreditCardId = retObj.moCompanyCreditCardId
                        Case Else
                            State.CompanyCreditCardId = Guid.Empty
                    End Select
                    Grid.PageIndex = State.PageIndex
                    Grid.PageSize = State.PageSize
                    cboPageSize.SelectedValue = CType(State.PageSize, String)
                    Grid.PageSize = State.PageSize
                    ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Public Class ReturnType
            Public LastOperation As ElitaPlusPage.DetailPageCommand
            Public moCompanyCreditCardId As Guid
            Public BoChanged As Boolean = False

            Public Sub New(LastOp As ElitaPlusPage.DetailPageCommand, omoCompanyCreditCardId As Guid, Optional ByVal boChanged As Boolean = False)
                LastOperation = LastOp
                moCompanyCreditCardId = omoCompanyCreditCardId
                Me.BoChanged = boChanged
            End Sub
        End Class

#End Region

#Region " Constants "
        'Actions
        Private Const ACTION_NONE As String = "ACTION_NONE"
        Private Const ACTION_SAVE As String = "ACTION_SAVE"
        Private Const ACTION_NO_EDIT As String = "ACTION_NO_EDIT"
        Private Const ACTION_EDIT As String = "ACTION_EDIT"
        Private Const ACTION_NEW As String = "ACTION_NEW"

        Private Const CompanyCreditCardIdConstant As Integer = 2
        Private Const CreditCardIdConstant As Integer = 3
        Private Const CompanyIdConstant As Integer = 4
        Private Const CompanyConstant As Integer = 5
        Private Const CreditCardConstant As Integer = 6
        Private Const BillingDateConstant As Integer = 7
        Private Const BillingScheduleConstant As Integer = 8

        Private Const COMPANY_CONTROL_NAME As String = "cboCompanyInGrid"
        Private Const ID_CONTROL_NAME As String = "IdLabel"
        Private Const COMPANY_LABEL_CONTROL_NAME As String = "CompanyLabel"
        Private Const CREDIT_CARD_CONTROL_NAME As String = "cboCreditCardInGrid"
        Private Const CREDIT_CARD_LABEL_CONTROL_NAME As String = "Credit_CardLabel"
        Private Const CREDIT_CARD_FORMAT_LABEL_CONTROL_NAME As String = "CreditCardFormatLabel"
        Private Const BILLING_DATE_CONTROL_NAME As String = "BillingDateTextBox"
        Private Const BILLING_DATE_LABEL_CONTROL_NAME As String = "BillingDateLabel"

        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"
        Private Const NO_ROW_SELECTED_INDEX As Integer = -1

        Private Const BILLING_SCHEDULE_CONTROL_NAME As String = "btnScheduleInGrid"

#End Region

#Region "Variables"
        Private msCommand As String
        Private moCompanyCreditCard As CompanyCreditCard
        Private moCompanyCreditCardId As String
        Protected WithEvents moTitleLabel As System.Web.UI.WebControls.Label
        Protected WithEvents moIsNewRecord As System.Web.UI.WebControls.Label
        Protected WithEvents multipleDropControl As MultipleColumnDDLabelControl
        Private IsReturningFromChild As Boolean = False
        Private _CreditCardTypeList As DataView

        Private mbIsDelete As Boolean
#End Region

#Region "Properties"

        Private ReadOnly Property TheCompanyCreditCard() As CompanyCreditCard
            Get
                If IsNewCompanyCreditCard() = True Then
                    ' For creating, inserting
                    moCompanyCreditCard = New CompanyCreditCard
                    CompanyCreditCardId = moCompanyCreditCard.Id.ToString
                Else
                    ' For updating, deleting
                    Dim oCompanyCreditCardId As Guid = GetGuidFromString(CompanyCreditCardId)
                    moCompanyCreditCard = New CompanyCreditCard(oCompanyCreditCardId)
                End If

                Return moCompanyCreditCard
            End Get
        End Property

        Private Property CompanyCreditCardId() As String
            Get
                If Grid.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                    moCompanyCreditCardId = GetSelectedGridText(Grid, CompanyCreditCardIdConstant)
                End If
                Return moCompanyCreditCardId
            End Get
            Set(Value As String)
                If Grid.SelectedIndex > NO_ITEM_SELECTED_INDEX Then
                    SetSelectedGridText(Grid, CompanyCreditCardIdConstant, Value)
                End If
                moCompanyCreditCardId = Value
            End Set
        End Property

        Private Property IsNewCompanyCreditCard() As Boolean
            Get
                Return State.IsNew
            End Get
            Set(Value As Boolean)
                State.IsNew = Value
            End Set
        End Property

#End Region

#Region "Handlers"
        Protected WithEvents moErrorController As ErrorController
        Protected WithEvents moCoxxde As System.Web.UI.WebControls.Label
        Protected WithEvents moxxDescription As System.Web.UI.WebControls.Label
        Protected WithEvents EditPanel_WRITE As System.Web.UI.WebControls.Panel
        Protected WithEvents doPostBack As System.Web.UI.WebControls.Button

#Region "Handlers_Init"

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                moErrorController.Clear_Hide()
                moCompanyCreditCardId = Guid.Empty.ToString
                If Not Page.IsPostBack Then
                    SetGridItemStyleColor(Grid)
                    IsNewCompanyCreditCard = False
                    If State.MyBO Is Nothing Then
                        State.MyBO = New CompanyCreditCard
                    End If
                    State.PageIndex = 0
                    SetButtonsState(False)
                    TranslateGridHeader(Grid)
                    TranslateGridControls(Grid)
                    PopulateCompanyCreditCardGrid()
                Else
                    CheckIfComingFromDeleteConfirm()
                End If
                BindBoPropertiesToGridHeaders()
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
            ShowMissingTranslations(moErrorController)
        End Sub

        Protected Sub CheckIfComingFromDeleteConfirm()
            Dim confResponse As String = HiddenDeletePromptResponse.Value
            If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
                If Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete Then
                    DoDelete()
                End If
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Delete
                End Select
            ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Delete
                End Select
            End If
            'Clean after consuming the action
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            HiddenDeletePromptResponse.Value = ""
        End Sub

        Private Sub DoDelete()
            State.MyBO = New CompanyCreditCard(State.Id)
            Try
                State.MyBO.Delete()
                'Call the Save() method in the DealerGroup Business Object here
                State.MyBO.Save()

            Catch ex As Exception
                State.MyBO.RejectChanges()
                Throw ex
            End Try

            State.PageIndex = Grid.PageIndex

            'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
            State.IsAfterSave = True
            State.searchDV = Nothing
            PopulateCompanyCreditCardGrid(ACTION_NO_EDIT)
            State.PageIndex = Grid.PageIndex

        End Sub

#End Region

#Region "Handlers_DropDowns"

        Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                PopulateCompanyCreditCardGrid()
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

#End Region

#Region "Handlers_buttons"

        Private Sub BtnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles BtnNew_WRITE.Click
            Try
                State.IsEditMode = True
                State.searchDV = Nothing
                AddNew()
                SetButtonsState(True)
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub BtnCancel_Click(sender As System.Object, e As System.EventArgs) Handles BtnCancel.Click
            Try
                IsNewCompanyCreditCard = True
                SetGridControls(Grid, True)
                If (IsNewCompanyCreditCard) Then
                    State.searchDV = Nothing
                End If
                ReturnFromEditing()
                IsNewCompanyCreditCard = False
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub
        Private Sub BtnSave_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles BtnSave_WRITE.Click
            Try
                PopulateBOFromForm()
                If (State.MyBO.IsDirty) Then
                    State.MyBO.Save()
                    State.IsAfterSave = True
                    State.AddingNewRow = False
                    AddInfoMsg(MSG_RECORD_SAVED_OK)
                    State.searchDV = Nothing
                    ReturnFromEditing()
                Else
                    AddInfoMsg(MSG_RECORD_NOT_SAVED)
                    ReturnFromEditing()
                End If
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub
#End Region

#Region "Handlers_Grid"

        Protected Sub RowCommand(source As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)
            Dim nIndex As Integer
            Dim paramList As New ArrayList
            Dim creditCardType As String
            Dim companyName As String
            Dim isEditing As Boolean = State.IsEditMode

            Try
                If e.CommandName = EDIT_COMMAND_NAME Then
                    nIndex = CInt(e.CommandArgument)
                    Grid.EditIndex = nIndex

                    Grid.SelectedIndex = nIndex
                    State.IsEditMode = True
                    State.Id = New Guid(CType(Grid.Rows(nIndex).Cells(CompanyCreditCardIdConstant).FindControl(ID_CONTROL_NAME), Label).Text)
                    State.MyBO = New CompanyCreditCard(State.Id)
                    PopulateCompanyCreditCardGrid(ACTION_EDIT)

                    'Disable all Edit and Delete icon buttons on the Grid
                    SetGridControls(Grid, False)
                    State.PageIndex = Grid.PageIndex
                    PopulateFormFromBO(nIndex)
                    SetButtonsState(True)
                ElseIf (e.CommandName = DELETE_COMMAND_NAME) Then

                    nIndex = CInt(e.CommandArgument)
                    Grid.SelectedIndex = NO_ROW_SELECTED_INDEX

                    State.Id = New Guid(CType(Grid.Rows(nIndex).Cells(CompanyCreditCardIdConstant).FindControl(ID_CONTROL_NAME), Label).Text)

                    DisplayMessage(Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenDeletePromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
                ElseIf (e.CommandName = SELECT_COMMAND_NAME) Then

                    nIndex = CInt(e.CommandArgument)
                    Grid.SelectedIndex = NO_ROW_SELECTED_INDEX

                    If Not State.IsEditMode Then
                        State.Id = New Guid(CType(Grid.Rows(nIndex).Cells(CompanyCreditCardIdConstant).FindControl(ID_CONTROL_NAME), Label).Text)
                        creditCardType = CType(Grid.Rows(nIndex).Cells(CreditCardConstant).FindControl(CREDIT_CARD_LABEL_CONTROL_NAME), Label).Text.ToString
                        companyName = CType(Grid.Rows(nIndex).Cells(CompanyConstant).FindControl(COMPANY_LABEL_CONTROL_NAME), Label).Text.ToString
                        paramList.Add(State.Id)
                        paramList.Add(creditCardType)
                        paramList.Add(companyName)
                        callPage(CompanyCreditCardScheduleForm.URL, paramList)
                    End If
                End If


            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try

        End Sub

        Protected Sub RowCreated(sender As Object, e As GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub Grid_Sorting(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting

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
                Grid.PageIndex = 0
                Grid.SelectedIndex = -1
                PopulateCompanyCreditCardGrid()
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try

        End Sub

        Private Sub Grid_PageIndexChanging(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                If (Not (State.IsEditMode)) Then
                    State.PageIndex = e.NewPageIndex
                    Grid.PageIndex = State.PageIndex
                    PopulateCompanyCreditCardGrid(ACTION_NO_EDIT)
                    Grid.SelectedIndex = NO_ITEM_SELECTED_INDEX
                End If
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try

        End Sub

        Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

                If dvRow IsNot Nothing AndAlso Not State.searchDV.Count > 0 Then
                    If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                        CType(e.Row.Cells(CompanyCreditCardIdConstant).FindControl(ID_CONTROL_NAME), Label).Text = GetGuidStringFromByteArray(CType(dvRow(CompanyCreditCard.CompanyCreditCardSearchDV.COL_COMPANY_CREDIT_CARD_ID), Byte()))
                        If (State.IsEditMode = True AndAlso State.Id.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow(CompanyCreditCard.CompanyCreditCardSearchDV.COL_COMPANY_CREDIT_CARD_ID), Byte())))) Then
                            Dim companyLst As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="Company")
                            CType(e.Row.Cells(CompanyConstant).FindControl(COMPANY_CONTROL_NAME), DropDownList).Populate(companyLst, New PopulateOptions() With
                                {
                                    .AddBlankItem = True
                                })
                            'Me.BindListControlToDataView(CType(e.Row.Cells(Me.CompanyConstant).FindControl(Me.COMPANY_CONTROL_NAME), DropDownList), LookupListNew.GetCompanyLookupList())
                            SetSelectedItem(CType(e.Row.Cells(CompanyConstant).FindControl(COMPANY_CONTROL_NAME), DropDownList), State.MyBO.CompanyId)

                            Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
                            oListContext.LanguageId = Thread.CurrentPrincipal.GetLanguageId()
                            Dim creditcardFormatLst As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="CreditCardFormat", context:=oListContext, languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
                            CType(e.Row.Cells(CreditCardConstant).FindControl(CREDIT_CARD_CONTROL_NAME), DropDownList).Populate(creditcardFormatLst, New PopulateOptions() With
                                {
                                    .AddBlankItem = True
                                })
                            'Me.BindListControlToDataView(CType(e.Row.Cells(Me.CreditCardConstant).FindControl(Me.CREDIT_CARD_CONTROL_NAME), DropDownList), LookupListNew.GetCompanyCreditCardsFormatLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                            SetSelectedItem(CType(e.Row.Cells(CreditCardConstant).FindControl(CREDIT_CARD_CONTROL_NAME), DropDownList), State.MyBO.Id)
                        Else
                            CType(e.Row.Cells(CompanyConstant).FindControl(COMPANY_LABEL_CONTROL_NAME), Label).Text = dvRow(CompanyCreditCard.CompanyCreditCardSearchDV.COL_COMPANY_CODE).ToString
                            CType(e.Row.Cells(CreditCardConstant).FindControl(CREDIT_CARD_LABEL_CONTROL_NAME), Label).Text = dvRow(CompanyCreditCard.CompanyCreditCardSearchDV.COL_CREDIT_CARD_TYPE).ToString
                            CType(e.Row.Cells(BillingDateConstant).FindControl(BILLING_DATE_LABEL_CONTROL_NAME), Label).Text = dvRow(CompanyCreditCard.CompanyCreditCardSearchDV.COL_BILLING_DATE).ToString
                        End If
                    End If
                End If
                If dvRow IsNot Nothing AndAlso State.searchDV.Count > 0 Then
                    If CType(dvRow(CompanyCreditCard.CompanyCreditCardSearchDV.COL_BILLING_DATE), Long) = 0 Then
                        ControlMgr.SetEnableControl(Me, CType(e.Row.Cells(BillingScheduleConstant).FindControl(BILLING_SCHEDULE_CONTROL_NAME), ImageButton), True)

                    Else
                        ControlMgr.SetEnableControl(Me, CType(e.Row.Cells(BillingScheduleConstant).FindControl(BILLING_SCHEDULE_CONTROL_NAME), ImageButton), False)
                    End If
                End If

            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

#End Region

#End Region

#Region "Buttons-Management"

        Private Sub SetButtonsState(bIsEdit As Boolean)

            If (bIsEdit) Then
                ControlMgr.SetVisibleControl(Me, BtnSave_WRITE, True)
                ControlMgr.SetVisibleControl(Me, BtnCancel, True)
                ControlMgr.SetVisibleControl(Me, BtnNew_WRITE, False)
                MenuEnabled = False

            Else
                ControlMgr.SetVisibleControl(Me, BtnSave_WRITE, False)
                ControlMgr.SetVisibleControl(Me, BtnCancel, False)
                ControlMgr.SetVisibleControl(Me, BtnNew_WRITE, True)
                MenuEnabled = True

            End If

        End Sub
#End Region

#Region "Populate"

        Private Sub PopulateCompanyCreditCardGrid(Optional ByVal oAction As String = ACTION_NONE)

            Dim oDataView As DataView
            Try
                If State.searchDV Is Nothing Then
                    State.searchDV = GetDV()
                End If
                Dim dv As CompanyCreditCard.CompanyCreditCardSearchDV

                If State.searchDV.Count = 0 Then
                    dv = State.searchDV.AddNewRowToEmptyDV
                    SetPageAndSelectedIndexFromGuid(dv, State.Id, Grid, State.PageIndex)
                    Grid.DataSource = dv
                Else
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.Id, Grid, State.PageIndex)
                    Grid.DataSource = State.searchDV
                End If

                State.searchDV.Sort = State.SortExpression
                If (State.IsAfterSave) Then
                    State.IsAfterSave = False
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.Id, Grid, Grid.PageIndex)
                ElseIf (State.IsEditMode) Then
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.Id, Grid, Grid.PageIndex, State.IsEditMode)
                Else
                    'In a Delete scenario...
                    SetPageAndSelectedIndexFromGuid(State.searchDV, Guid.Empty, Grid, Grid.PageIndex, State.IsEditMode)
                End If

                Grid.AutoGenerateColumns = False

                If State.searchDV.Count = 0 Then
                    SortAndBindGrid(dv)
                Else
                    SortAndBindGrid(State.searchDV)
                End If

                If State.searchDV.Count = 0 Then
                    For Each gvRow As GridViewRow In Grid.Rows
                        gvRow.Visible = False
                        gvRow.Controls.Clear()
                    Next
                End If

            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try

        End Sub
        Private Sub SortAndBindGridxx()
            State.PageIndex = Grid.PageIndex
            If (State.searchDV.Count > 0) Then
                Grid.DataSource = State.searchDV
                HighLightSortColumn(Grid, State.SortExpression)
                Grid.DataBind()
                Grid.PagerSettings.Visible = True
                ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
                If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True
            End If

            Session("recCount") = State.searchDV.Count

            If Grid.Visible Then
                If (State.AddingNewRow) Then
                    lblRecordCount.Text = (State.searchDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                Else
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
        End Sub

        Private Sub SortAndBindGrid(dvBinding As DataView, Optional ByVal blnEmptyList As Boolean = False)
            Grid.DataSource = dvBinding
            HighLightSortColumn(Grid, State.SortExpression)
            Grid.DataBind()
            If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True
            If blnEmptyList Then
                For Each gvRow As GridViewRow In Grid.Rows
                    'gvRow.Visible = False
                    gvRow.Controls.Clear()
                Next
            End If
        End Sub

        Private Sub AddNew()
            State.searchDV = GetDV()


            State.MyBO = New CompanyCreditCard
            State.Id = State.MyBO.Id

            State.searchDV = State.MyBO.GetNewDataViewRow(State.searchDV, State.Id, State.MyBO)
            'Me.State.searchDV = Me.State.searchDV.AddNewRowToEmptyDV
            Grid.DataSource = State.searchDV

            SetPageAndSelectedIndexFromGuid(State.searchDV, State.Id, Grid, State.PageIndex, State.IsEditMode)

            Grid.AutoGenerateColumns = False

            SortAndBindGrid(State.searchDV)

            SetGridControls(Grid, False)

            PopulateFormFromBO()
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(grid As GridView, cellPosition As Integer, controlName As String, itemIndex As Integer)
            'Set focus on the Description TextBox for the EditItemIndex row
            Dim code As TextBox = CType(grid.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
            SetFocus(code)
        End Sub
        Public Overrides Sub BaseSetButtonsState(bIsEdit As Boolean)
            SetButtonsState(bIsEdit)
        End Sub

        Protected Sub BindBoPropertiesToGridHeaders()
            BindBOPropertyToGridHeader(State.MyBO, "CompanyId", Grid.Columns(CompanyIdConstant))
            BindBOPropertyToGridHeader(State.MyBO, "CreditCardFormatId", Grid.Columns(CreditCardConstant))
            BindBOPropertyToGridHeader(State.MyBO, "BillingDate", Grid.Columns(BillingDateConstant))
            ClearGridViewHeadersAndLabelsErrSign()
        End Sub

#End Region

#Region "Business Part"

        Private Sub PopulateBOFromForm()
            Try

                Dim cboCompany As DropDownList = CType(Grid.Rows(Grid.EditIndex).Cells(CompanyIdConstant).FindControl(COMPANY_CONTROL_NAME), DropDownList)
                Dim cboCreditCardFormat As DropDownList = CType(Grid.Rows(Grid.EditIndex).Cells(CreditCardConstant).FindControl(CREDIT_CARD_CONTROL_NAME), DropDownList)
                Dim txtBilling As TextBox = CType(Grid.Rows(Grid.EditIndex).Cells(BillingDateConstant).FindControl(BILLING_DATE_CONTROL_NAME), TextBox)

                PopulateBOProperty(State.MyBO, "CompanyId", cboCompany)
                PopulateBOProperty(State.MyBO, "CreditCardFormatId", cboCreditCardFormat)
                PopulateBOProperty(State.MyBO, "BillingDate", txtBilling)

            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try

        End Sub

        Private Sub PopulateFormFromBO(Optional ByVal gridRowIdx As Integer = Nothing)

            If gridRowIdx.Equals(Nothing) Then gridRowIdx = Grid.EditIndex

            Dim companyList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="Company")
            Dim filteredCompanyList As DataElements.ListItem() = (From x In companyList
                                                                  Where ElitaPlusIdentity.Current.ActiveUser.Companies.Contains(x.ListItemId)
                                                                  Select x).ToArray()
            CType(Grid.Rows(gridRowIdx).Cells(CompanyConstant).FindControl(COMPANY_CONTROL_NAME), DropDownList).Populate(filteredCompanyList, New PopulateOptions() With
                {
                    .AddBlankItem = True
                })

            'Me.BindListControlToDataView(CType(Me.Grid.Rows(gridRowIdx).Cells(Me.CompanyConstant).FindControl(Me.COMPANY_CONTROL_NAME), DropDownList), LookupListNew.GetUserCompaniesLookupList())

            Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
            oListContext.LanguageId = Thread.CurrentPrincipal.GetLanguageId()
            Dim creditcardFormatLst As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="CreditCardFormat", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=oListContext)
            CType(Grid.Rows(gridRowIdx).Cells(CreditCardConstant).FindControl(CREDIT_CARD_CONTROL_NAME), DropDownList).Populate(creditcardFormatLst, New PopulateOptions() With
                {
                    .AddBlankItem = True
                })
            'Me.BindListControlToDataView(CType(Me.Grid.Rows(gridRowIdx).Cells(Me.CreditCardConstant).FindControl(Me.CREDIT_CARD_CONTROL_NAME), DropDownList), LookupListNew.GetCompanyCreditCardsFormatLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId))

            Try
                With State.MyBO

                    If (Not .CompanyId.Equals(Guid.Empty)) Then
                        Dim cboCompany As DropDownList = CType(Grid.Rows(gridRowIdx).Cells(CompanyConstant).FindControl(COMPANY_CONTROL_NAME), DropDownList)
                        PopulateControlFromBOProperty(cboCompany, .CompanyId)
                    End If
                    If (Not .CreditCardFormatId.Equals(Guid.Empty)) Then
                        Dim cboCreditCardFormat As DropDownList = CType(Grid.Rows(gridRowIdx).Cells(CreditCardConstant).FindControl(CREDIT_CARD_CONTROL_NAME), DropDownList)
                        PopulateControlFromBOProperty(cboCreditCardFormat, .CreditCardFormatId)
                    End If

                    Dim txtBilling As TextBox = CType(Grid.Rows(gridRowIdx).Cells(BillingDateConstant).FindControl(BILLING_DATE_CONTROL_NAME), TextBox)
                    PopulateControlFromBOProperty(txtBilling, .BillingDate)

                    CType(Grid.Rows(gridRowIdx).Cells(CompanyCreditCardIdConstant).FindControl(ID_CONTROL_NAME), Label).Text = .Id.ToString
                    CType(Grid.Rows(gridRowIdx).Cells(CompanyIdConstant).FindControl(COMPANY_LABEL_CONTROL_NAME), Label).Text = .CompanyId.ToString
                    CType(Grid.Rows(gridRowIdx).Cells(CreditCardIdConstant).FindControl(CREDIT_CARD_FORMAT_LABEL_CONTROL_NAME), Label).Text = .CreditCardFormatId.ToString


                End With
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try

        End Sub

        Private Sub ReturnFromEditing()

            Grid.EditIndex = NO_ROW_SELECTED_INDEX

            If Grid.PageCount = 0 Then
                'if returning to the "1st time in" screen
                ControlMgr.SetVisibleControl(Me, Grid, False)
            Else
                ControlMgr.SetVisibleControl(Me, Grid, True)
            End If

            SetGridControls(Grid, True)
            State.IsEditMode = False
            PopulateCompanyCreditCardGrid(ACTION_SAVE)
            State.PageIndex = Grid.PageIndex
            SetButtonsState(False)

        End Sub

        Private Function GetDV() As CompanyCreditCard.CompanyCreditCardSearchDV

            Dim dv As CompanyCreditCard.CompanyCreditCardSearchDV

            dv = GetDataView()
            dv.Sort = Grid.DataMember()
            Grid.DataSource = dv

            Return (dv)

        End Function

        Private Function GetDataView() As CompanyCreditCard.CompanyCreditCardSearchDV

            Return CompanyCreditCard.LoadList()

        End Function

#End Region

    End Class
End Namespace


