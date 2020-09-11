Imports Microsoft.VisualBasic
Imports System.Globalization
Imports System.Threading
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.Web.Forms
Imports Assurant.ElitaPlus.DALObjects

Namespace Tables
    Partial Class CompanyCreditCardForm
        Inherits ElitaPlusSearchPage
        'Inherits System.Web.UI.Page


#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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

        Public Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
            Try
                Me.IsReturningFromChild = True
                Dim retObj As ReturnType = CType(ReturnPar, ReturnType)
                If Not retObj Is Nothing AndAlso retObj.BoChanged Then
                    Me.State.searchDV = Nothing
                End If

                If Not retObj Is Nothing Then
                    Select Case retObj.LastOperation
                        Case ElitaPlusPage.DetailPageCommand.Back
                            Me.State.CompanyCreditCardId = retObj.moCompanyCreditCardId
                        Case Else
                            Me.State.CompanyCreditCardId = Guid.Empty
                    End Select
                    Me.Grid.PageIndex = Me.State.PageIndex
                    Me.Grid.PageSize = Me.State.PageSize
                    cboPageSize.SelectedValue = CType(Me.State.PageSize, String)
                    Me.Grid.PageSize = Me.State.PageSize
                    ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Public Class ReturnType
            Public LastOperation As ElitaPlusPage.DetailPageCommand
            Public moCompanyCreditCardId As Guid
            Public BoChanged As Boolean = False

            Public Sub New(ByVal LastOp As ElitaPlusPage.DetailPageCommand, ByVal omoCompanyCreditCardId As Guid, Optional ByVal boChanged As Boolean = False)
                Me.LastOperation = LastOp
                Me.moCompanyCreditCardId = omoCompanyCreditCardId
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

        Private Const COMPANY_CREDIT_CARD_ID As Integer = 2
        Private Const CREDIT_CARD_ID As Integer = 3
        Private Const COMPANY_ID As Integer = 4
        Private Const COMPANY As Integer = 5
        Private Const CREDIT_CARD As Integer = 6
        Private Const BILLING_DATE As Integer = 7
        Private Const BILLING_SCHEDULE As Integer = 8

        Private Const COMPANY_CONTROL_NAME As String = "cboCompanyInGrid"
        Private Const ID_CONTROL_NAME As String = "IdLabel"
        Private Const COMPANY_LABEL_CONTROL_NAME As String = "CompanyLabel"
        Private Const CREDIT_CARD_CONTROL_NAME As String = "cboCreditCardInGrid"
        Private Const CREDIT_CARD_LABEL_CONTROL_NAME As String = "Credit_CardLabel"
        Private Const CREDIT_CARD_FORMAT_LABEL_CONTROL_NAME As String = "CreditCardFormatLabel"
        Private Const BILLING_DATE_CONTROL_NAME As String = "BillingDateTextBox"
        Private Const BILLING_DATE_LABEL_CONTROL_NAME As String = "BillingDateLabel"

        Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
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

        Private Property IsNewCompanyCreditCard() As Boolean
            Get
                Return Me.State.IsNew
            End Get
            Set(ByVal Value As Boolean)
                Me.State.IsNew = Value
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

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                moErrorController.Clear_Hide()
                moCompanyCreditCardId = Guid.Empty.ToString
                If Not Page.IsPostBack Then
                    Me.SetGridItemStyleColor(Grid)
                    IsNewCompanyCreditCard = False
                    If Me.State.MyBO Is Nothing Then
                        Me.State.MyBO = New CompanyCreditCard
                    End If
                    Me.State.PageIndex = 0
                    SetButtonsState(False)
                    Me.TranslateGridHeader(Me.Grid)
                    Me.TranslateGridControls(Me.Grid)
                    PopulateCompanyCreditCardGrid()
                Else
                    CheckIfComingFromDeleteConfirm()
                End If
                BindBoPropertiesToGridHeaders()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try
            Me.ShowMissingTranslations(moErrorController)
        End Sub

        Protected Sub CheckIfComingFromDeleteConfirm()
            Dim confResponse As String = Me.HiddenDeletePromptResponse.Value
            If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
                If Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete Then
                    DoDelete()
                End If
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Delete
                End Select
            ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Delete
                End Select
            End If
            'Clean after consuming the action
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            Me.HiddenDeletePromptResponse.Value = ""
        End Sub

        Private Sub DoDelete()
            Me.State.MyBO = New CompanyCreditCard(Me.State.Id)
            Try
                Me.State.MyBO.Delete()
                'Call the Save() method in the DealerGroup Business Object here
                Me.State.MyBO.Save()

            Catch ex As Exception
                Me.State.MyBO.RejectChanges()
                Throw ex
            End Try

            Me.State.PageIndex = Grid.PageIndex

            'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
            Me.State.IsAfterSave = True
            Me.State.searchDV = Nothing
            PopulateCompanyCreditCardGrid(ACTION_NO_EDIT)
            Me.State.PageIndex = Grid.PageIndex

        End Sub

#End Region

#Region "Handlers_DropDowns"

        Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                Me.PopulateCompanyCreditCardGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try
        End Sub

#End Region

#Region "Handlers_buttons"

        Private Sub BtnNew_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew_WRITE.Click
            Try
                Me.State.IsEditMode = True
                Me.State.searchDV = Nothing
                AddNew()
                SetButtonsState(True)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try
        End Sub

        Private Sub BtnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancel.Click
            Try
                IsNewCompanyCreditCard = True
                SetGridControls(Me.Grid, True)
                If (IsNewCompanyCreditCard) Then
                    Me.State.searchDV = Nothing
                End If
                ReturnFromEditing()
                IsNewCompanyCreditCard = False
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try
        End Sub
        Private Sub BtnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave_WRITE.Click
            Try
                PopulateBOFromForm()
                If (Me.State.MyBO.IsDirty) Then
                    Me.State.MyBO.Save()
                    Me.State.IsAfterSave = True
                    Me.State.AddingNewRow = False
                    Me.AddInfoMsg(Me.MSG_RECORD_SAVED_OK)
                    Me.State.searchDV = Nothing
                    Me.ReturnFromEditing()
                Else
                    Me.AddInfoMsg(Me.MSG_RECORD_NOT_SAVED)
                    Me.ReturnFromEditing()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try
        End Sub
#End Region

#Region "Handlers_Grid"

        Protected Sub RowCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
            Dim nIndex As Integer
            Dim paramList As New ArrayList
            Dim creditCardType As String
            Dim companyName As String
            Dim isEditing As Boolean = Me.State.IsEditMode

            Try
                If e.CommandName = Me.EDIT_COMMAND_NAME Then
                    nIndex = CInt(e.CommandArgument)
                    Grid.EditIndex = nIndex

                    Grid.SelectedIndex = nIndex
                    Me.State.IsEditMode = True
                    Me.State.Id = New Guid(CType(Me.Grid.Rows(nIndex).Cells(Me.COMPANY_CREDIT_CARD_ID).FindControl(Me.ID_CONTROL_NAME), Label).Text)
                    Me.State.MyBO = New CompanyCreditCard(Me.State.Id)
                    PopulateCompanyCreditCardGrid(ACTION_EDIT)

                    'Disable all Edit and Delete icon buttons on the Grid
                    SetGridControls(Me.Grid, False)
                    Me.State.PageIndex = Grid.PageIndex
                    PopulateFormFromBO(nIndex)
                    SetButtonsState(True)
                ElseIf (e.CommandName = Me.DELETE_COMMAND_NAME) Then

                    nIndex = CInt(e.CommandArgument)
                    Grid.SelectedIndex = Me.NO_ROW_SELECTED_INDEX

                    Me.State.Id = New Guid(CType(Me.Grid.Rows(nIndex).Cells(Me.COMPANY_CREDIT_CARD_ID).FindControl(Me.ID_CONTROL_NAME), Label).Text)

                    Me.DisplayMessage(Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenDeletePromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
                ElseIf (e.CommandName = Me.SELECT_COMMAND_NAME) Then

                    nIndex = CInt(e.CommandArgument)
                    Grid.SelectedIndex = Me.NO_ROW_SELECTED_INDEX

                    If Not Me.State.IsEditMode Then
                        Me.State.Id = New Guid(CType(Me.Grid.Rows(nIndex).Cells(Me.COMPANY_CREDIT_CARD_ID).FindControl(Me.ID_CONTROL_NAME), Label).Text)
                        creditCardType = CType(Me.Grid.Rows(nIndex).Cells(Me.CREDIT_CARD).FindControl(Me.CREDIT_CARD_LABEL_CONTROL_NAME), Label).Text.ToString
                        companyName = CType(Me.Grid.Rows(nIndex).Cells(Me.COMPANY).FindControl(Me.COMPANY_LABEL_CONTROL_NAME), Label).Text.ToString
                        paramList.Add(Me.State.Id)
                        paramList.Add(creditCardType)
                        paramList.Add(companyName)
                        Me.callPage(CompanyCreditCardScheduleForm.URL, paramList)
                    End If
                End If


            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try

        End Sub

        Protected Sub RowCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub Grid_Sorting(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting

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
                Me.Grid.PageIndex = 0
                Me.Grid.SelectedIndex = -1
                Me.PopulateCompanyCreditCardGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try

        End Sub

        Private Sub Grid_PageIndexChanging(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                If (Not (Me.State.IsEditMode)) Then
                    Me.State.PageIndex = e.NewPageIndex
                    Me.Grid.PageIndex = Me.State.PageIndex
                    Me.PopulateCompanyCreditCardGrid(ACTION_NO_EDIT)
                    Me.Grid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try

        End Sub

        Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

                If Not dvRow Is Nothing And Not State.searchDV.Count > 0 Then
                    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                        CType(e.Row.Cells(Me.COMPANY_CREDIT_CARD_ID).FindControl(Me.ID_CONTROL_NAME), Label).Text = GetGuidStringFromByteArray(CType(dvRow(CompanyCreditCard.CompanyCreditCardSearchDV.COL_COMPANY_CREDIT_CARD_ID), Byte()))
                        If (Me.State.IsEditMode = True AndAlso Me.State.Id.ToString.Equals(GetGuidStringFromByteArray(CType(dvRow(CompanyCreditCard.CompanyCreditCardSearchDV.COL_COMPANY_CREDIT_CARD_ID), Byte())))) Then
                            Dim companyLst As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="Company")
                            CType(e.Row.Cells(Me.COMPANY).FindControl(Me.COMPANY_CONTROL_NAME), DropDownList).Populate(companyLst, New PopulateOptions() With
                                {
                                    .AddBlankItem = True
                                })
                            'Me.BindListControlToDataView(CType(e.Row.Cells(Me.COMPANY).FindControl(Me.COMPANY_CONTROL_NAME), DropDownList), LookupListNew.GetCompanyLookupList())
                            Me.SetSelectedItem(CType(e.Row.Cells(Me.COMPANY).FindControl(Me.COMPANY_CONTROL_NAME), DropDownList), Me.State.MyBO.CompanyId)

                            Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
                            oListContext.LanguageId = Thread.CurrentPrincipal.GetLanguageId()
                            Dim creditcardFormatLst As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="CreditCardFormat", context:=oListContext, languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
                            CType(e.Row.Cells(Me.CREDIT_CARD).FindControl(Me.CREDIT_CARD_CONTROL_NAME), DropDownList).Populate(creditcardFormatLst, New PopulateOptions() With
                                {
                                    .AddBlankItem = True
                                })
                            'Me.BindListControlToDataView(CType(e.Row.Cells(Me.CREDIT_CARD).FindControl(Me.CREDIT_CARD_CONTROL_NAME), DropDownList), LookupListNew.GetCompanyCreditCardsFormatLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                            Me.SetSelectedItem(CType(e.Row.Cells(Me.CREDIT_CARD).FindControl(Me.CREDIT_CARD_CONTROL_NAME), DropDownList), Me.State.MyBO.Id)
                        Else
                            CType(e.Row.Cells(Me.COMPANY).FindControl(COMPANY_LABEL_CONTROL_NAME), Label).Text = dvRow(CompanyCreditCard.CompanyCreditCardSearchDV.COL_COMPANY_CODE).ToString
                            CType(e.Row.Cells(Me.CREDIT_CARD).FindControl(CREDIT_CARD_LABEL_CONTROL_NAME), Label).Text = dvRow(CompanyCreditCard.CompanyCreditCardSearchDV.COL_CREDIT_CARD_TYPE).ToString
                            CType(e.Row.Cells(Me.BILLING_DATE).FindControl(BILLING_DATE_LABEL_CONTROL_NAME), Label).Text = dvRow(CompanyCreditCard.CompanyCreditCardSearchDV.COL_BILLING_DATE).ToString
                        End If
                    End If
                End If
                If Not dvRow Is Nothing And State.searchDV.Count > 0 Then
                    If CType(dvRow(CompanyCreditCard.CompanyCreditCardSearchDV.COL_BILLING_DATE), Long) = 0 Then
                        ControlMgr.SetEnableControl(Me, CType(e.Row.Cells(Me.BILLING_SCHEDULE).FindControl(Me.BILLING_SCHEDULE_CONTROL_NAME), ImageButton), True)

                    Else
                        ControlMgr.SetEnableControl(Me, CType(e.Row.Cells(Me.BILLING_SCHEDULE).FindControl(Me.BILLING_SCHEDULE_CONTROL_NAME), ImageButton), False)
                    End If
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try
        End Sub

#End Region

#End Region

#Region "Buttons-Management"

        Private Sub SetButtonsState(ByVal bIsEdit As Boolean)

            If (bIsEdit) Then
                ControlMgr.SetVisibleControl(Me, BtnSave_WRITE, True)
                ControlMgr.SetVisibleControl(Me, BtnCancel, True)
                ControlMgr.SetVisibleControl(Me, BtnNew_WRITE, False)
                Me.MenuEnabled = False

            Else
                ControlMgr.SetVisibleControl(Me, BtnSave_WRITE, False)
                ControlMgr.SetVisibleControl(Me, BtnCancel, False)
                ControlMgr.SetVisibleControl(Me, BtnNew_WRITE, True)
                Me.MenuEnabled = True

            End If

        End Sub
#End Region

#Region "Populate"

        Private Sub PopulateCompanyCreditCardGrid(Optional ByVal oAction As String = ACTION_NONE)

            Dim oDataView As DataView
            Try
                If Me.State.searchDV Is Nothing Then
                    Me.State.searchDV = GetDV()
                End If
                Dim dv As CompanyCreditCard.CompanyCreditCardSearchDV

                If State.searchDV.Count = 0 Then
                    dv = State.searchDV.AddNewRowToEmptyDV
                    SetPageAndSelectedIndexFromGuid(dv, Me.State.Id, Me.Grid, Me.State.PageIndex)
                    Me.Grid.DataSource = dv
                Else
                    SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.Id, Me.Grid, Me.State.PageIndex)
                    Me.Grid.DataSource = Me.State.searchDV
                End If

                Me.State.searchDV.Sort = Me.State.SortExpression
                If (Me.State.IsAfterSave) Then
                    Me.State.IsAfterSave = False
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.Id, Me.Grid, Me.Grid.PageIndex)
                ElseIf (Me.State.IsEditMode) Then
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.Id, Me.Grid, Me.Grid.PageIndex, Me.State.IsEditMode)
                Else
                    'In a Delete scenario...
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Guid.Empty, Me.Grid, Me.Grid.PageIndex, Me.State.IsEditMode)
                End If

                Me.Grid.AutoGenerateColumns = False

                If State.searchDV.Count = 0 Then
                    SortAndBindGrid(dv)
                Else
                    SortAndBindGrid(Me.State.searchDV)
                End If

                If State.searchDV.Count = 0 Then
                    For Each gvRow As GridViewRow In Grid.Rows
                        gvRow.Visible = False
                        gvRow.Controls.Clear()
                    Next
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try

        End Sub
        Private Sub SortAndBindGridxx()
            Me.State.PageIndex = Me.Grid.PageIndex
            If (Me.State.searchDV.Count > 0) Then
                Me.Grid.DataSource = Me.State.searchDV
                HighLightSortColumn(Grid, Me.State.SortExpression)
                Me.Grid.DataBind()
                Grid.PagerSettings.Visible = True
                ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)
                If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True
            End If

            Session("recCount") = Me.State.searchDV.Count

            If Me.Grid.Visible Then
                If (Me.State.AddingNewRow) Then
                    Me.lblRecordCount.Text = (Me.State.searchDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                Else
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
        End Sub

        Private Sub SortAndBindGrid(ByVal dvBinding As DataView, Optional ByVal blnEmptyList As Boolean = False)
            Me.Grid.DataSource = dvBinding
            HighLightSortColumn(Grid, Me.State.SortExpression)
            Me.Grid.DataBind()
            If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True
            If blnEmptyList Then
                For Each gvRow As GridViewRow In Grid.Rows
                    'gvRow.Visible = False
                    gvRow.Controls.Clear()
                Next
            End If
        End Sub

        Private Sub AddNew()
            Me.State.searchDV = GetDV()


            Me.State.MyBO = New CompanyCreditCard
            Me.State.Id = Me.State.MyBO.Id

            Me.State.searchDV = Me.State.MyBO.GetNewDataViewRow(Me.State.searchDV, Me.State.Id, Me.State.MyBO)
            'Me.State.searchDV = Me.State.searchDV.AddNewRowToEmptyDV
            Grid.DataSource = Me.State.searchDV

            Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.Id, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)

            Me.Grid.AutoGenerateColumns = False

            SortAndBindGrid(Me.State.searchDV)

            SetGridControls(Me.Grid, False)

            PopulateFormFromBO()
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(ByVal grid As GridView, ByVal cellPosition As Integer, ByVal controlName As String, ByVal itemIndex As Integer)
            'Set focus on the Description TextBox for the EditItemIndex row
            Dim code As TextBox = CType(grid.Rows(itemIndex).Cells(cellPosition).FindControl(controlName), TextBox)
            SetFocus(code)
        End Sub
        Public Overrides Sub BaseSetButtonsState(ByVal bIsEdit As Boolean)
            SetButtonsState(bIsEdit)
        End Sub

        Protected Sub BindBoPropertiesToGridHeaders()
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "CompanyId", Me.Grid.Columns(Me.COMPANY_ID))
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "CreditCardFormatId", Me.Grid.Columns(Me.CREDIT_CARD))
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "BillingDate", Me.Grid.Columns(Me.BILLING_DATE))
            Me.ClearGridViewHeadersAndLabelsErrSign()
        End Sub

#End Region

#Region "Business Part"

        Private Sub PopulateBOFromForm()
            Try

                Dim cboCompany As DropDownList = CType(Me.Grid.Rows(Me.Grid.EditIndex).Cells(Me.COMPANY_ID).FindControl(Me.COMPANY_CONTROL_NAME), DropDownList)
                Dim cboCreditCardFormat As DropDownList = CType(Me.Grid.Rows(Me.Grid.EditIndex).Cells(Me.CREDIT_CARD).FindControl(Me.CREDIT_CARD_CONTROL_NAME), DropDownList)
                Dim txtBilling As TextBox = CType(Me.Grid.Rows(Me.Grid.EditIndex).Cells(Me.BILLING_DATE).FindControl(Me.BILLING_DATE_CONTROL_NAME), TextBox)

                Me.PopulateBOProperty(Me.State.MyBO, "CompanyId", cboCompany)
                Me.PopulateBOProperty(Me.State.MyBO, "CreditCardFormatId", cboCreditCardFormat)
                Me.PopulateBOProperty(Me.State.MyBO, "BillingDate", txtBilling)

            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try

        End Sub

        Private Sub PopulateFormFromBO(Optional ByVal gridRowIdx As Integer = Nothing)

            If gridRowIdx.Equals(Nothing) Then gridRowIdx = Me.Grid.EditIndex

            Dim companyList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="Company")
            Dim filteredCompanyList As DataElements.ListItem() = (From x In companyList
                                                                  Where ElitaPlusIdentity.Current.ActiveUser.Companies.Contains(x.ListItemId)
                                                                  Select x).ToArray()
            CType(Me.Grid.Rows(gridRowIdx).Cells(Me.COMPANY).FindControl(Me.COMPANY_CONTROL_NAME), DropDownList).Populate(filteredCompanyList, New PopulateOptions() With
                {
                    .AddBlankItem = True
                })

            'Me.BindListControlToDataView(CType(Me.Grid.Rows(gridRowIdx).Cells(Me.COMPANY).FindControl(Me.COMPANY_CONTROL_NAME), DropDownList), LookupListNew.GetUserCompaniesLookupList())

            Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
            oListContext.LanguageId = Thread.CurrentPrincipal.GetLanguageId()
            Dim creditcardFormatLst As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="CreditCardFormat", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=oListContext)
            CType(Me.Grid.Rows(gridRowIdx).Cells(Me.CREDIT_CARD).FindControl(Me.CREDIT_CARD_CONTROL_NAME), DropDownList).Populate(creditcardFormatLst, New PopulateOptions() With
                {
                    .AddBlankItem = True
                })
            'Me.BindListControlToDataView(CType(Me.Grid.Rows(gridRowIdx).Cells(Me.CREDIT_CARD).FindControl(Me.CREDIT_CARD_CONTROL_NAME), DropDownList), LookupListNew.GetCompanyCreditCardsFormatLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId))

            Try
                With Me.State.MyBO

                    If (Not .CompanyId.Equals(Guid.Empty)) Then
                        Dim cboCompany As DropDownList = CType(Me.Grid.Rows(gridRowIdx).Cells(Me.COMPANY).FindControl(COMPANY_CONTROL_NAME), DropDownList)
                        Me.PopulateControlFromBOProperty(cboCompany, .CompanyId)
                    End If
                    If (Not .CreditCardFormatId.Equals(Guid.Empty)) Then
                        Dim cboCreditCardFormat As DropDownList = CType(Me.Grid.Rows(gridRowIdx).Cells(Me.CREDIT_CARD).FindControl(CREDIT_CARD_CONTROL_NAME), DropDownList)
                        Me.PopulateControlFromBOProperty(cboCreditCardFormat, .CreditCardFormatId)
                    End If

                    Dim txtBilling As TextBox = CType(Me.Grid.Rows(gridRowIdx).Cells(Me.BILLING_DATE).FindControl(BILLING_DATE_CONTROL_NAME), TextBox)
                    Me.PopulateControlFromBOProperty(txtBilling, .BillingDate)

                    CType(Me.Grid.Rows(gridRowIdx).Cells(Me.COMPANY_CREDIT_CARD_ID).FindControl(Me.ID_CONTROL_NAME), Label).Text = .Id.ToString
                    CType(Me.Grid.Rows(gridRowIdx).Cells(Me.COMPANY_ID).FindControl(Me.COMPANY_LABEL_CONTROL_NAME), Label).Text = .CompanyId.ToString
                    CType(Me.Grid.Rows(gridRowIdx).Cells(Me.CREDIT_CARD_ID).FindControl(Me.CREDIT_CARD_FORMAT_LABEL_CONTROL_NAME), Label).Text = .CreditCardFormatId.ToString


                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try

        End Sub

        Private Sub ReturnFromEditing()

            Grid.EditIndex = NO_ROW_SELECTED_INDEX

            If Me.Grid.PageCount = 0 Then
                'if returning to the "1st time in" screen
                ControlMgr.SetVisibleControl(Me, Grid, False)
            Else
                ControlMgr.SetVisibleControl(Me, Grid, True)
            End If

            SetGridControls(Grid, True)
            Me.State.IsEditMode = False
            Me.PopulateCompanyCreditCardGrid(ACTION_SAVE)
            Me.State.PageIndex = Grid.PageIndex
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


