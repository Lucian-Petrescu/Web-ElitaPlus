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
            If (Not Me.State Is Nothing) Then
                If (Not Me.State Is Nothing) Then
                    Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("PAYMENT_TYPE")
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("PAYMENT_TYPE")
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
                Return Me.Grid.EditIndex > Me.NO_ITEM_SELECTED_INDEX
            End Get
        End Property

        Public Property SortDirection() As String
            Get
                If Not ViewState("SortDirection") Is Nothing Then
                    Return ViewState("SortDirection").ToString
                Else
                    Return String.Empty
                End If

            End Get
            Set(ByVal value As String)
                ViewState("SortDirection") = value
            End Set
        End Property
#End Region

#Region "Page Events"
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Me.MasterPage.MessageController.Clear()
            Try
                If Not Me.IsPostBack Then
                    UpdateBreadCrum()

                    SetControlState()
                    Me.State.PageIndex = 0
                    Me.TranslateGridHeader(Grid)
                    Me.TranslateGridControls(Grid)
                    PopulatePaymentTypeGrid()
                Else
                    CheckIfComingFromDeleteConfirm()
                    BindBoPropertiesToGridHeaders()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)
        End Sub
#End Region

#Region "Control Handler"
        Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNew.Click

            Try
                Me.State.IsEditMode = True
                Me.State.IsGridVisible = True
                Me.State.IsGridAddNew = True
                AddNew()
                Me.SetControlState()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub AddNew()
            If Me.State.MyBO Is Nothing OrElse Me.State.MyBO.IsNew = False Then
                Me.State.MyBO = New PaymentType
                State.MyBO.AddNewRowToSearchDV(Me.State.searchDV, Me.State.MyBO)
            End If
            Me.State.Id = Me.State.MyBO.Id
            State.IsGridAddNew = True
            PopulatePaymentTypeGrid()
            'Set focus on the Code TextBox for the EditItemIndex row
            Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.Id, Me.Grid, Me.State.PageIndex, Me.State.IsEditMode)
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
            SetGridControls(Me.Grid, False)
        End Sub

        Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs)

            Try
                PopulateBOFromForm()
                If (Me.State.MyBO.IsDirty) Then
                    Me.State.MyBO.Save()
                    Me.State.IsAfterSave = True
                    Me.State.IsGridAddNew = False
                    Me.MasterPage.MessageController.AddSuccess(Me.MSG_RECORD_SAVED_OK, True)
                    Me.State.searchDV = Nothing
                    Me.ReturnFromEditing()
                Else
                    Me.MasterPage.MessageController.AddWarning(Me.MSG_RECORD_NOT_SAVED, True)
                    Me.ReturnFromEditing()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub
        Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs)
            Try
                With State
                    If .IsGridAddNew Then
                        RemoveNewRowFromSearchDV()
                        .IsGridAddNew = False
                        Grid.PageIndex = .PageIndex
                    End If
                    .Id = Guid.Empty
                    Me.State.MyBO = Nothing
                    .IsEditMode = False
                End With
                Grid.EditIndex = NO_ITEM_SELECTED_INDEX
                'Me.State.searchDV = Nothing
                PopulatePaymentTypeGrid()
                SetControlState()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
#End Region
#Region "Helper functions"
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
            'Do the delete here
            Me.State.ActionInProgress = DetailPageCommand.Nothing_
            'Save the RiskTypeId in the Session

            Dim obj As PaymentType = New PaymentType(Me.State.Id)

            obj.Delete()

            'Call the Save() method in the Role Business Object here

            obj.Save()

            Me.MasterPage.MessageController.AddSuccess(Me.MSG_RECORD_DELETED_OK, True)

            Me.State.PageIndex = Grid.PageIndex

            'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
            Me.State.IsAfterSave = True
            Me.State.searchDV = Nothing
            PopulatePaymentTypeGrid()
            Me.State.PageIndex = Grid.PageIndex
            Me.State.IsEditMode = False
            SetControlState()
        End Sub
        Private Sub SetControlState()
            If (Me.State.IsEditMode) Then
                ControlMgr.SetVisibleControl(Me, btnNew, False)
                Me.MenuEnabled = False
                If (Me.cboPageSize.Enabled) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, False)
                End If
            Else
                ControlMgr.SetVisibleControl(Me, btnNew, True)
                Me.MenuEnabled = True
                If Not (Me.cboPageSize.Enabled) Then
                    ControlMgr.SetEnableControl(Me, Me.cboPageSize, True)
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
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "CollectionMethodId", Me.Grid.Columns(Me.GRID_COL_COLLECTION_METHOD_IDX))
            Me.BindBOPropertyToGridHeader(Me.State.MyBO, "PaymentInstrumentId", Me.Grid.Columns(Me.GRID_COL_PAYMENT_INSTRUMENT_IDX))
            Me.ClearGridViewHeadersAndLabelsErrSign()
        End Sub
        Private Sub RemoveNewRowFromSearchDV()
            Dim rowind As Integer = NO_ITEM_SELECTED_INDEX
            With State
                If Not .searchDV Is Nothing Then
                    rowind = FindSelectedRowIndexFromGuid(.searchDV, .Id)
                End If
            End With
            If rowind <> NO_ITEM_SELECTED_INDEX Then State.searchDV.Delete(rowind)
        End Sub

        Private Function PopulateBOFromForm() As Boolean
            Dim cboCollectionMethod As DropDownList
            Dim cboPaymentInstrument As DropDownList
            With Me.State.MyBO
                .CompanyGroupId() = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                cboCollectionMethod = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_COLLECTION_METHOD_IDX).FindControl(GRID_CTRL_NAME_EDIT_COLLECTION_METHOD), DropDownList)
                PopulateBOProperty(Me.State.MyBO, "CollectionMethodId", cboCollectionMethod)

                cboPaymentInstrument = CType(Grid.Rows((Me.Grid.EditIndex)).Cells(GRID_COL_PAYMENT_INSTRUMENT_IDX).FindControl(GRID_CTRL_NAME_EDIT_PAYMENT_INSTRUMENT), DropDownList)
                PopulateBOProperty(Me.State.MyBO, "PaymentInstrumentId", cboPaymentInstrument)
                .Code = LookupListNew.GetCodeFromId(LookupListCache.LK_COLLECTION_METHODS, .CollectionMethodId)
                .Description = cboCollectionMethod.SelectedItem.ToString
            End With
            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Function

        Private Sub ReturnFromEditing()

            Grid.EditIndex = NO_ROW_SELECTED_INDEX

            If (Me.Grid.PageCount = 0) Then
                'if returning to the "1st time in" screen
                ControlMgr.SetVisibleControl(Me, Grid, False)
            Else
                ControlMgr.SetVisibleControl(Me, Grid, True)
            End If

            Me.State.IsEditMode = False
            Me.PopulatePaymentTypeGrid()
            Me.State.PageIndex = Grid.PageIndex
            SetControlState()
        End Sub
#End Region
#Region "Grid related"
        Private Sub PopulatePaymentTypeGrid()

            Dim oDataView As DataView
            Try
                If Me.State.searchDV Is Nothing Then
                    Me.State.searchDV = GetDV()
                End If
                Me.State.searchDV.Sort = Me.SortDirection
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

                Me.Grid.Columns(Me.GRID_COL_COLLECTION_METHOD_IDX).SortExpression = PaymentType.PaymentTypeSearchDV.COL_COLLECTION_METHOD
                Me.Grid.Columns(Me.GRID_COL_PAYMENT_INSTRUMENT_IDX).SortExpression = PaymentType.PaymentTypeSearchDV.COL_PAYMENT_INSTRUMENT

                SortAndBindGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub


        Private Sub SortAndBindGrid()
            Me.TranslateGridControls(Grid)

            If (Me.State.searchDV.Count = 0) Then
                Me.State.searchDV = Nothing
                Me.State.MyBO = New PaymentType
                PaymentType.AddNewRowToSearchDV(Me.State.searchDV, Me.State.MyBO)
                Me.Grid.DataSource = Me.State.searchDV
                Me.Grid.DataBind()
                Me.Grid.Rows(0).Visible = False
                Me.State.IsGridAddNew = True
                Me.State.IsGridVisible = False

            Else
                Me.Grid.Enabled = True
                Me.Grid.PageSize = Me.State.PageSize
                Me.Grid.DataSource = Me.State.searchDV
                Me.State.IsGridVisible = True
                HighLightSortColumn(Grid, Me.SortDirection)
                Me.Grid.DataBind()
                Grid.PagerSettings.Visible = True
                If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True
            End If


            ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)

            Session("recCount") = Me.State.searchDV.Count

            If Me.Grid.Visible Then
                If (Me.State.IsGridAddNew) Then
                    Me.lblRecordCount.Text = (Me.State.searchDV.Count - 1) & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                Else
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
            ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)

        End Sub

        Private Sub Grid_PageIndexChanged(sender As Object, e As EventArgs) Handles Grid.PageIndexChanged
            Try
                If (Not (Me.State.IsEditMode)) Then
                    Me.State.PageIndex = Grid.PageIndex
                    Me.State.Id = Guid.Empty
                    Me.PopulatePaymentTypeGrid()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
        Private Sub Grid_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                Grid.PageIndex = e.NewPageIndex
                State.PageIndex = Grid.PageIndex
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_Sorting(sender As Object, e As GridViewSortEventArgs) Handles Grid.Sorting
            Try
                Dim spaceIndex As Integer = Me.SortDirection.LastIndexOf(" ")


                If spaceIndex > 0 AndAlso Me.SortDirection.Substring(0, spaceIndex).Equals(e.SortExpression) Then
                    If Me.SortDirection.EndsWith(" ASC") Then
                        Me.SortDirection = e.SortExpression + " DESC"
                    Else
                        Me.SortDirection = e.SortExpression + " ASC"
                    End If
                Else
                    Me.SortDirection = e.SortExpression + " ASC"
                End If

                Me.State.PageIndex = 0
                Me.PopulatePaymentTypeGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
        Protected Sub cboPageSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                State.PageSize = CType(cboPageSize.SelectedValue, Integer)
                Me.State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.PageSize)
                Me.Grid.PageIndex = Me.State.PageIndex
                Me.PopulatePaymentTypeGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
        Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                Dim strID As String

                If Not dvRow Is Nothing Then
                    strID = GetGuidStringFromByteArray(CType(dvRow(PaymentType.PaymentTypeSearchDV.COL_PAYMENT_TYPE_ID), Byte()))
                    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                        CType(e.Row.Cells(Me.GRID_COL_PAYMENT_TYPE_IDX).FindControl(Me.GRID_CTRL_NAME_LABLE_PAYMENT_TYPE_ID), Label).Text = strID

                        If (Me.State.IsEditMode = True AndAlso Me.State.Id.ToString.Equals(strID)) Then
                            Dim objDDL As DropDownList
                            Dim dv As DataView
                            Dim guidVal As Guid

                            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId


                            'populate Collection Method dropdown
                            objDDL = CType(e.Row.Cells(Me.GRID_COL_COLLECTION_METHOD_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_COLLECTION_METHOD), DropDownList)
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
                            objDDL = CType(e.Row.Cells(Me.GRID_COL_PAYMENT_INSTRUMENT_IDX).FindControl(Me.GRID_CTRL_NAME_EDIT_PAYMENT_INSTRUMENT), DropDownList)
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
                            CType(e.Row.Cells(Me.GRID_COL_COLLECTION_METHOD_IDX).FindControl(Me.GRID_CTRL_NAME_LABLE_COLLECTION_METHOD), Label).Text = dvRow(PaymentType.PaymentTypeSearchDV.COL_COLLECTION_METHOD).ToString
                            CType(e.Row.Cells(Me.GRID_COL_PAYMENT_INSTRUMENT_IDX).FindControl(Me.GRID_CTRL_NAME_LABEL_PAYMENT_INSTRUMENT), Label).Text = dvRow(PaymentType.PaymentTypeSearchDV.COL_PAYMENT_INSTRUMENT).ToString

                        End If
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub
        Public Sub RowCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

            Try
                Dim index As Integer
                If (e.CommandName = Me.EDIT_COMMAND) Then
                    index = CInt(e.CommandArgument)
                    'Do the Edit here

                    'Set the IsEditMode flag to TRUE
                    Me.State.IsEditMode = True

                    Me.State.Id = New Guid(CType(Me.Grid.Rows(index).Cells(Me.GRID_COL_PAYMENT_TYPE_IDX).FindControl(Me.GRID_CTRL_NAME_LABLE_PAYMENT_TYPE_ID), Label).Text)
                    Me.State.MyBO = New PaymentType(Me.State.Id)

                    Me.PopulatePaymentTypeGrid()

                    Me.State.PageIndex = Grid.PageIndex

                    Me.SetControlState()

                ElseIf (e.CommandName = Me.DELETE_COMMAND) Then
                    index = CInt(e.CommandArgument)
                    Me.State.Id = New Guid(CType(Me.Grid.Rows(index).Cells(Me.GRID_COL_PAYMENT_TYPE_IDX).FindControl(Me.GRID_CTRL_NAME_LABLE_PAYMENT_TYPE_ID), Label).Text)
                    Me.DisplayMessage(Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenDeletePromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete

                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Public Sub RowCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region
    End Class

End Namespace