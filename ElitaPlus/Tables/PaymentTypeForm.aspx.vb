Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Collections.Generic
Imports System.Web.Services
Imports System.Threading
Imports Assurant.Elita.Web.Forms
Imports System.Web.Script.Services
Imports Assurant.ElitaPlus.Security
Imports System.Web.Script.Serialization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Namespace Tables
    Partial Public Class PaymentTypeForm
        Inherits ElitaPlusSearchPage

#Region "Bread Crum"

        Private Sub UpdateBreadCrum()
            If (State IsNot Nothing) Then
                If (State IsNot Nothing) Then
                    MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("PAYMENT_TYPE")
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("PAYMENT_TYPE")
                End If
            End If
        End Sub
#End Region

#Region "Constants"
        Public Const URL As String = "Tables/PaymentTypeForm.aspx"
        Public Const PAGETITLE As String = "PAYMENT_TYPE"
        Public Const PAGETAB As String = "TABLES"
        Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
        Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

        Private Const GRID_COL_PAYMENT_TYPE_IDX As Integer = 0
        Private Const GRID_COL_COLLECTION_METHOD_IDX As Integer = 1
        Private Const GRID_COL_PAYMENT_INSTRUMENT_IDX As Integer = 2
        Private Const GRID_COL_EDIT_IDX As Integer = 3
        Private Const GRID_COL_DELETE_IDX As Integer = 4


        Private Const GRID_CTRL_NAME_LABLE_PAYMENT_TYPE_ID As String = "IdLabel"
        Private Const GRID_CTRL_NAME_LABLE_COLLECTION_METHOD As String = "Collection_MethodLabel"
        Private Const GRID_CTRL_NAME_LABEL_PAYMENT_INSTRUMENT As String = "Payment_InstrumentLabel"


        Private Const GRID_CTRL_NAME_EDIT_COLLECTION_METHOD As String = "cboCollection_MethodInGrid"
        Private Const GRID_CTRL_NAME_EDIT_PAYMENT_INSTRUMENT As String = "cboPayment_InstrumentInGrid"


        Private Const EDIT_COMMAND As String = "SelectAction"
        Private Const DELETE_COMMAND As String = "DeleteRecord"
        Private Const SORT_COMMAND As String = "Sort"

        Private Const NO_ROW_SELECTED_INDEX As Integer = -1
#End Region

#Region "Page State"

        Class MyState
            Public MyBO As PaymentType
            Public newPaymentTypeId As Guid
            Public PageIndex As Integer = 0
            Public PageSize As Integer = DEFAULT_NEW_UI_PAGE_SIZE
            Public searchDV As PaymentType.PaymentTypeSearchDV = Nothing
            Public Id As Guid
            Public HasDataChanged As Boolean
            Public IsGridAddNew As Boolean = False
            Public IsEditMode As Boolean = False
            Public IsGridVisible As Boolean
            Public IsAfterSave As Boolean
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public SortExpression As String = PaymentType.PaymentTypeSearchDV.COL_COLLECTION_METHOD

        End Class

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property

        Public ReadOnly Property IsGridInEditMode() As Boolean
            Get
                Return Grid.EditIndex > NO_ITEM_SELECTED_INDEX
            End Get
        End Property

        Public Property SortDirection() As String
            Get
                If ViewState("SortDirection") IsNot Nothing Then
                    Return ViewState("SortDirection").ToString
                Else
                    Return String.Empty
                End If

            End Get
            Set(value As String)
                ViewState("SortDirection") = value
            End Set
        End Property
#End Region

#Region "Page Events"
        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            MasterPage.MessageController.Clear()
            Try
                If Not IsPostBack Then
                    UpdateBreadCrum()

                    SetControlState()
                    State.PageIndex = 0
                    TranslateGridHeader(Grid)
                    TranslateGridControls(Grid)
                    PopulatePaymentTypeGrid()
                Else
                    CheckIfComingFromDeleteConfirm()
                    BindBoPropertiesToGridHeaders()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub
#End Region

#Region "Control Handler"
        Protected Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click

            Try
                State.IsEditMode = True
                State.IsGridVisible = True
                State.IsGridAddNew = True
                AddNew()
                SetControlState()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub AddNew()
            If State.MyBO Is Nothing OrElse State.MyBO.IsNew = False Then
                State.MyBO = New PaymentType
                State.MyBO.AddNewRowToSearchDV(State.searchDV, State.MyBO)
            End If
            State.Id = State.MyBO.Id
            State.IsGridAddNew = True
            PopulatePaymentTypeGrid()
            'Set focus on the Code TextBox for the EditItemIndex row
            SetPageAndSelectedIndexFromGuid(State.searchDV, State.Id, Grid, State.PageIndex, State.IsEditMode)
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
            SetGridControls(Grid, False)
        End Sub

        Protected Sub btnSave_Click(sender As Object, e As EventArgs)

            Try
                PopulateBOFromForm()
                If (State.MyBO.IsDirty) Then
                    State.MyBO.Save()
                    State.IsAfterSave = True
                    State.IsGridAddNew = False
                    MasterPage.MessageController.AddSuccess(MSG_RECORD_SAVED_OK, True)
                    State.searchDV = Nothing
                    ReturnFromEditing()
                Else
                    MasterPage.MessageController.AddWarning(MSG_RECORD_NOT_SAVED, True)
                    ReturnFromEditing()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub
        Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
            Try
                With State
                    If .IsGridAddNew Then
                        RemoveNewRowFromSearchDV()
                        .IsGridAddNew = False
                        Grid.PageIndex = .PageIndex
                    End If
                    .Id = Guid.Empty
                    State.MyBO = Nothing
                    .IsEditMode = False
                End With
                Grid.EditIndex = NO_ITEM_SELECTED_INDEX
                'Me.State.searchDV = Nothing
                PopulatePaymentTypeGrid()
                SetControlState()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region
#Region "Helper functions"
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
            'Do the delete here
            State.ActionInProgress = DetailPageCommand.Nothing_
            'Save the RiskTypeId in the Session

            Dim obj As PaymentType = New PaymentType(State.Id)

            obj.Delete()

            'Call the Save() method in the Role Business Object here

            obj.Save()

            MasterPage.MessageController.AddSuccess(MSG_RECORD_DELETED_OK, True)

            State.PageIndex = Grid.PageIndex

            'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
            State.IsAfterSave = True
            State.searchDV = Nothing
            PopulatePaymentTypeGrid()
            State.PageIndex = Grid.PageIndex
            State.IsEditMode = False
            SetControlState()
        End Sub
        Private Sub SetControlState()
            If (State.IsEditMode) Then
                ControlMgr.SetVisibleControl(Me, btnNew, False)
                MenuEnabled = False
                If (cboPageSize.Enabled) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, False)
                End If
            Else
                ControlMgr.SetVisibleControl(Me, btnNew, True)
                MenuEnabled = True
                If Not (cboPageSize.Enabled) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, True)
                End If
            End If
        End Sub
        Private Function GetDV() As DataView

            Dim dv As DataView

            dv = GetDataView()
            dv.Sort = Grid.DataMember()
            Grid.DataSource = dv

            Return (dv)

        End Function

        Private Function GetDataView() As DataView

            Return PaymentType.getList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)

        End Function

        Protected Sub BindBoPropertiesToGridHeaders()
            BindBOPropertyToGridHeader(State.MyBO, "CollectionMethodId", Grid.Columns(GRID_COL_COLLECTION_METHOD_IDX))
            BindBOPropertyToGridHeader(State.MyBO, "PaymentInstrumentId", Grid.Columns(GRID_COL_PAYMENT_INSTRUMENT_IDX))
            ClearGridViewHeadersAndLabelsErrSign()
        End Sub
        Private Sub RemoveNewRowFromSearchDV()
            Dim rowind As Integer = NO_ITEM_SELECTED_INDEX
            With State
                If .searchDV IsNot Nothing Then
                    rowind = FindSelectedRowIndexFromGuid(.searchDV, .Id)
                End If
            End With
            If rowind <> NO_ITEM_SELECTED_INDEX Then State.searchDV.Delete(rowind)
        End Sub

        Private Function PopulateBOFromForm() As Boolean
            Dim cboCollectionMethod As DropDownList
            Dim cboPaymentInstrument As DropDownList
            With State.MyBO
                .CompanyGroupId() = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                cboCollectionMethod = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_COLLECTION_METHOD_IDX).FindControl(GRID_CTRL_NAME_EDIT_COLLECTION_METHOD), DropDownList)
                PopulateBOProperty(State.MyBO, "CollectionMethodId", cboCollectionMethod)

                cboPaymentInstrument = CType(Grid.Rows((Grid.EditIndex)).Cells(GRID_COL_PAYMENT_INSTRUMENT_IDX).FindControl(GRID_CTRL_NAME_EDIT_PAYMENT_INSTRUMENT), DropDownList)
                PopulateBOProperty(State.MyBO, "PaymentInstrumentId", cboPaymentInstrument)
                .Code = LookupListNew.GetCodeFromId(LookupListCache.LK_COLLECTION_METHODS, .CollectionMethodId)
                .Description = cboCollectionMethod.SelectedItem.ToString
            End With
            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Function

        Private Sub ReturnFromEditing()

            Grid.EditIndex = NO_ROW_SELECTED_INDEX

            If (Grid.PageCount = 0) Then
                'if returning to the "1st time in" screen
                ControlMgr.SetVisibleControl(Me, Grid, False)
            Else
                ControlMgr.SetVisibleControl(Me, Grid, True)
            End If

            State.IsEditMode = False
            PopulatePaymentTypeGrid()
            State.PageIndex = Grid.PageIndex
            SetControlState()
        End Sub
#End Region
#Region "Grid related"
        Private Sub PopulatePaymentTypeGrid()

            Dim oDataView As DataView
            Try
                If State.searchDV Is Nothing Then
                    State.searchDV = GetDV()
                End If
                State.searchDV.Sort = SortDirection
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

                Grid.Columns(GRID_COL_COLLECTION_METHOD_IDX).SortExpression = PaymentType.PaymentTypeSearchDV.COL_COLLECTION_METHOD
                Grid.Columns(GRID_COL_PAYMENT_INSTRUMENT_IDX).SortExpression = PaymentType.PaymentTypeSearchDV.COL_PAYMENT_INSTRUMENT

                SortAndBindGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub


        Private Sub SortAndBindGrid()
            TranslateGridControls(Grid)

            If (State.searchDV.Count = 0) Then
                State.searchDV = Nothing
                State.MyBO = New PaymentType
                PaymentType.AddNewRowToSearchDV(State.searchDV, State.MyBO)
                Grid.DataSource = State.searchDV
                Grid.DataBind()
                Grid.Rows(0).Visible = False
                State.IsGridAddNew = True
                State.IsGridVisible = False

            Else
                Grid.Enabled = True
                Grid.PageSize = State.PageSize
                Grid.DataSource = State.searchDV
                State.IsGridVisible = True
                HighLightSortColumn(Grid, SortDirection)
                Grid.DataBind()
                Grid.PagerSettings.Visible = True
                If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True
            End If


            ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)

            Session("recCount") = State.searchDV.Count

            If Grid.Visible Then
                If (State.IsGridAddNew) Then
                    lblRecordCount.Text = (State.searchDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                Else
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)

        End Sub

        Private Sub Grid_PageIndexChanged(sender As Object, e As EventArgs) Handles Grid.PageIndexChanged
            Try
                If (Not (State.IsEditMode)) Then
                    State.PageIndex = Grid.PageIndex
                    State.Id = Guid.Empty
                    PopulatePaymentTypeGrid()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Sub Grid_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                Grid.PageIndex = e.NewPageIndex
                State.PageIndex = Grid.PageIndex
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_Sorting(sender As Object, e As GridViewSortEventArgs) Handles Grid.Sorting
            Try
                Dim spaceIndex As Integer = SortDirection.LastIndexOf(" ")


                If spaceIndex > 0 AndAlso SortDirection.Substring(0, spaceIndex).Equals(e.SortExpression) Then
                    If SortDirection.EndsWith(" ASC") Then
                        SortDirection = e.SortExpression + " DESC"
                    Else
                        SortDirection = e.SortExpression + " ASC"
                    End If
                Else
                    SortDirection = e.SortExpression + " ASC"
                End If

                State.PageIndex = 0
                PopulatePaymentTypeGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Protected Sub cboPageSize_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                State.PageSize = CType(cboPageSize.SelectedValue, Integer)
                State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.PageSize)
                Grid.PageIndex = State.PageIndex
                PopulatePaymentTypeGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                Dim strID As String

                If dvRow IsNot Nothing Then
                    strID = GetGuidStringFromByteArray(CType(dvRow(PaymentType.PaymentTypeSearchDV.COL_PAYMENT_TYPE_ID), Byte()))
                    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                        CType(e.Row.Cells(GRID_COL_PAYMENT_TYPE_IDX).FindControl(GRID_CTRL_NAME_LABLE_PAYMENT_TYPE_ID), Label).Text = strID

                        If (State.IsEditMode = True AndAlso State.Id.ToString.Equals(strID)) Then
                            Dim objDDL As DropDownList
                            Dim dv As DataView
                            Dim guidVal As Guid

                            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId


                            'populate Collection Method dropdown
                            objDDL = CType(e.Row.Cells(GRID_COL_COLLECTION_METHOD_IDX).FindControl(GRID_CTRL_NAME_EDIT_COLLECTION_METHOD), DropDownList)
                            'dv = LookupListNew.DropdownLookupList(LookupListNew.LK_COLLECTION_METHODS, langId, True)
                            '  Me.BindListControlToDataView(objDDL, dv, , , True)
                            Dim collectionMethodLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("COLLMETHOD", Thread.CurrentPrincipal.GetLanguageCode())
                            objDDL.Populate(collectionMethodLkl, New PopulateOptions() With
                             {
                                       .AddBlankItem = True
                             })
                            guidVal = New Guid(CType(dvRow(PaymentType.PaymentTypeSearchDV.COL_COLLECTION_METHOD_ID), Byte()))
                            SetSelectedItem(objDDL, guidVal)

                            'populate Payment Instrument  dropdown
                            objDDL = CType(e.Row.Cells(GRID_COL_PAYMENT_INSTRUMENT_IDX).FindControl(GRID_CTRL_NAME_EDIT_PAYMENT_INSTRUMENT), DropDownList)
                            'dv = LookupListNew.DropdownLookupList(LookupListNew.LK_PAYMENT_INSTRUMENT, langId, True)
                            'Me.BindListControlToDataView(objDDL, dv, , , True)
                            Dim paymentIntrumentLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("PMTINSTR", Thread.CurrentPrincipal.GetLanguageCode())
                            objDDL.Populate(paymentIntrumentLkl, New PopulateOptions() With
                             {
                                       .AddBlankItem = True
                             })
                            guidVal = New Guid(CType(dvRow(PaymentType.PaymentTypeSearchDV.COL_PAYMENT_INSTRUMENT_ID), Byte()))
                            SetSelectedItem(objDDL, guidVal)
                        Else
                            CType(e.Row.Cells(GRID_COL_COLLECTION_METHOD_IDX).FindControl(GRID_CTRL_NAME_LABLE_COLLECTION_METHOD), Label).Text = dvRow(PaymentType.PaymentTypeSearchDV.COL_COLLECTION_METHOD).ToString
                            CType(e.Row.Cells(GRID_COL_PAYMENT_INSTRUMENT_IDX).FindControl(GRID_CTRL_NAME_LABEL_PAYMENT_INSTRUMENT), Label).Text = dvRow(PaymentType.PaymentTypeSearchDV.COL_PAYMENT_INSTRUMENT).ToString

                        End If
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub
        Public Sub RowCommand(source As System.Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)

            Try
                Dim index As Integer
                If (e.CommandName = EDIT_COMMAND) Then
                    index = CInt(e.CommandArgument)
                    'Do the Edit here

                    'Set the IsEditMode flag to TRUE
                    State.IsEditMode = True

                    State.Id = New Guid(CType(Grid.Rows(index).Cells(GRID_COL_PAYMENT_TYPE_IDX).FindControl(GRID_CTRL_NAME_LABLE_PAYMENT_TYPE_ID), Label).Text)
                    State.MyBO = New PaymentType(State.Id)

                    PopulatePaymentTypeGrid()

                    State.PageIndex = Grid.PageIndex

                    SetControlState()

                ElseIf (e.CommandName = DELETE_COMMAND) Then
                    index = CInt(e.CommandArgument)
                    State.Id = New Guid(CType(Grid.Rows(index).Cells(GRID_COL_PAYMENT_TYPE_IDX).FindControl(GRID_CTRL_NAME_LABLE_PAYMENT_TYPE_ID), Label).Text)
                    DisplayMessage(Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenDeletePromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete

                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Public Sub RowCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region
    End Class

End Namespace