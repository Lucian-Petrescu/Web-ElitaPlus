Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Tables

    Partial Public Class PriceListSearchForm
        Inherits ElitaPlusSearchPage
#Region "Constants"

        Public Const GRID_CTRL_PRICE_LIST_ID As String = "lblListID"

        Private Const GRID_COL_PRICE_LIST_CODE_IDX As Integer = 0
        Private Const GRID_COL_DESCRIPTION_IDX As Integer = 1
        Private Const GRID_COL_COUNTRY As Integer = 2
        Private Const GRID_COL_VENDOR_COUNT_IDX As Integer = 3
        Private Const GRID_COL_STATUS_IDX As Integer = 4
        Private Const GRID_COL_EFFECTIVE_IDX As Integer = 5
        Private Const GRID_COL_EXPIRATION_IDX As Integer = 6

        Private Const GRID_COL_PRICE_LIST_ID_IDX As Integer = 7

#End Region

#Region "Page State"
        Private IsReturningFromChild As Boolean = False

        Class MyState

            Public SelectedPriceListId As Guid = Guid.Empty
            Public Code As String = String.Empty
            Public Description As String = String.Empty
            Public ServiceType As Guid = Guid.Empty
            Public Country As Guid = Guid.Empty
            Public ServiceCenter As String = String.Empty
            Public ActiveOnDate As DateType = Date.Today
            Public SortExpression As String = "Country"
            Public Status As String = String.Empty

            Public SearchDV As PriceList.PriceListSearchDV
            Public HasDataChanged As Boolean
            Public IsGridVisible As Boolean
            Public SelectedPageSize As Integer
            Public SearchClick As Boolean = False

            Public PageIndex As Integer = 0
            Public PageSize As Integer = 30


            Sub New()
            End Sub
        End Class

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property

        Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
            Try
                MenuEnabled = True
                IsReturningFromChild = True
                Dim retObj As PriceListDetailForm.ReturnType = CType(ReturnPar, PriceListDetailForm.ReturnType)
                State.HasDataChanged = retObj.HasDataChanged
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If retObj IsNot Nothing Then
                            If Not retObj.EditingBo.IsNew Then
                                State.SelectedPriceListId = retObj.EditingBo.Id
                            End If
                            State.IsGridVisible = True
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        DisplayMessage(Message.DELETE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                    Case ElitaPlusPage.DetailPageCommand.Expire
                        DisplayMessage(Message.EXPIRE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                End Select
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub SaveGuiState()
            With State
                .Code = txtCode.Text
                .Description = txtDesription.Text
                '   Me.SetSelectedItem(Me.ddlSvcType, .ServiceType)
                .ServiceType = New Guid(ddlSvcType.SelectedValue.ToString())
                .Country = New Guid(ddlCountry.SelectedValue.ToString())
                '  Me.SetSelectedItem(Me.ddlCountry, .Country)
                .ServiceCenter = txtSvcCntrName.Text
                If (txtActiveOnDate.Text <> "") Then
                    .ActiveOnDate = DateHelper.GetDateValue(txtActiveOnDate.Text)
                End If
            End With
        End Sub

        Private Sub RestoreGuiState()
            With State
                PopulateControlFromBOProperty(txtCode, .Code)
                PopulateControlFromBOProperty(txtDesription, .Description)
                PopulateControlFromBOProperty(txtActiveOnDate, .ActiveOnDate)
                PopulateControlFromBOProperty(txtSvcCntrName, .ServiceCenter)
                PopulateControlFromBOProperty(ddlCountry, .Country)
                PopulateControlFromBOProperty(ddlSvcType, .ServiceType)
            End With
        End Sub


#End Region

#Region "Page_Events"

        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            Try
                UpdateBreadCrum()
                MasterPage.MessageController.Clear()
                Form.DefaultButton = btnSearch.UniqueID

                If Not IsPostBack Then
                    AddCalendar_New(ImageButtonActiveOnDate, txtActiveOnDate)
                    'Me.SetDefaultButton(Me.txtCode, btnSearch)
                    'Me.SetDefaultButton(Me.txtDesription, btnSearch)

                    ' Me.BindListControlToDataView(Me.ddlCountry, LookupListNew.GetUserCountriesLookupList())
                    Dim countryList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="Country")

                    Dim filteredCountryList As DataElements.ListItem() = (From x In countryList
                                                                          Where ElitaPlusIdentity.Current.ActiveUser.Countries.Contains(x.ListItemId)
                                                                          Select x).ToArray()

                    ddlCountry.Populate(filteredCountryList, New PopulateOptions() With
                        {
                            .AddBlankItem = True
                        })

                    'Me.BindListControlToDataView(Me.ddlSvcType, LookupListNew.GetServiceTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                    'Me.BindListControlToDataView(Me.ddlSvcType, LookupListNew.GetNewServiceTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)) 'SVCTYP
                    ddlSvcType.Populate(CommonConfigManager.Current.ListManager.GetList("SVCTYP", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                        {
                            .AddBlankItem = True
                        })

                    If txtActiveOnDate.Text = String.Empty Then
                        txtActiveOnDate.Text = DateTime.Now.ToString()
                    End If

                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    RestoreGuiState()
                    TranslateGridHeader(Grid)

                    '''Set the country to the User's country
                    If ddlCountry.Items.Count = 2 Then
                        'set the country as default selected
                        ddlCountry.SelectedIndex = 1
                        State.Country = New Guid(ddlCountry.SelectedValue.ToString())
                    End If
                    '''' 
                    If State.IsGridVisible Then
                        'If Not (Me.State.SelectedPageSize = DEFAULT_PAGE_SIZE) Then
                        '    cboPageSize.SelectedValue = CType(Me.State.SelectedPageSize, String)
                        '    If (Me.State.SelectedPageSize = 0) Then
                        '        Me.State.SelectedPageSize = DEFAULT_PAGE_SIZE
                        '    End If
                        '    Me.Grid.PageSize = Me.State.SelectedPageSize
                        'End If
                        PopulateGrid()
                    End If
                    'Set page size
                    cboPageSize.SelectedValue = State.PageSize.ToString()
                    SetFocus(txtCode)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)

        End Sub

        Private Sub UpdateBreadCrum()
            MasterPage.UsePageTabTitleInBreadCrum = False
            MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("SERVICE_NETWORK") & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("PRICE_LIST")
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("PRICE_LIST_SEARCH")
            MasterPage.MessageController.Clear()
            MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage("TABLES") & ElitaBase.Sperator & MasterPage.PageTab
        End Sub

#End Region

#Region "Controlling Logic"

        Public Sub ValidateDates()

            If txtActiveOnDate.Text IsNot String.Empty Then
                If (DateHelper.IsDate(txtActiveOnDate.Text.ToString()) = False) Then
                    ElitaPlusPage.SetLabelError(lblActiveOnDate)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Message.MSG_INVALID_DATE)
                End If
            End If

        End Sub

        Public Sub PopulateGrid()
            If ((State.SearchDV Is Nothing) OrElse (State.HasDataChanged) OrElse IsReturningFromChild) Then
                State.Code = txtCode.Text.Trim()
                State.Description = txtDesription.Text
                State.ServiceType = New Guid(ddlSvcType.SelectedValue)
                State.Country = New Guid(ddlCountry.SelectedValue)
                State.ServiceCenter = txtSvcCntrName.Text
                If txtActiveOnDate.Text.Trim = String.Empty Then
                    State.ActiveOnDate = Nothing
                Else
                    State.ActiveOnDate = DateHelper.GetDateValue(txtActiveOnDate.Text)
                End If

                '''''Get the search list for the country(s)
                ''''create a string of the country(s) list.
                'Dim stbCountries As New System.Text.StringBuilder
                Dim lstCountries As New Collections.Generic.List(Of String)
                Dim strCountriesList As String

                If Guid.Equals(State.Country, Guid.Empty) Then
                    For Each lc As ListItem In ddlCountry.Items
                        lstCountries.Add(MiscUtil.GetDbStringFromGuid(New Guid(lc.Value)))
                        'stbCountries.Append(MiscUtil.GetDbStringFromGuid(New Guid(lc.Value))).Append(",")
                    Next
                    'create the comma separated countries list
                    strCountriesList = String.Join(",", lstCountries.ToArray())
                Else
                    strCountriesList = MiscUtil.GetDbStringFromGuid(New Guid(ddlCountry.SelectedValue))
                End If

                ''''
                State.SearchDV = PriceList.GetList(State.Code,
                                                      State.Description,
                                                      State.ServiceType,
                                                      strCountriesList,
                                                      State.ServiceCenter,
                                                      State.ActiveOnDate)
            End If
            State.SearchDV.Sort = State.SortExpression
            Grid.AutoGenerateColumns = False
            Grid.PageSize = State.PageSize

            SetPageAndSelectedIndexFromGuid(State.SearchDV, State.SelectedPriceListId, Grid, State.PageIndex)
            If State.SearchClick Then
                ValidSearchResultCountNew(State.SearchDV.Count, True)
                State.SearchClick = False
            End If
            SortAndBindGrid()
        End Sub

        Private Sub SortAndBindGrid()
            State.PageIndex = Grid.PageIndex
            Grid.DataSource = State.SearchDV
            HighLightSortColumn(Grid, State.SortExpression)
            Grid.DataBind()
            ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
            Session("recCount") = State.SearchDV.Count
            lblRecordCount.Text = State.SearchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
        End Sub
#End Region

#Region " GridView Related "

        Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
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
                State.PageIndex = 0
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub cboPageSize_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                State.PageSize = CType(cboPageSize.SelectedValue, Integer)
                State.SelectedPageSize = State.PageSize
                State.PageIndex = NewCurrentPageIndex(Grid, State.SearchDV.Count, State.PageSize)
                Grid.PageIndex = State.PageIndex
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub


        Private Sub Grid_PageIndexChanged(sender As Object, e As System.EventArgs) Handles Grid.PageIndexChanged
            Try
                State.PageIndex = Grid.PageIndex
                State.SelectedPriceListId = Guid.Empty
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                Grid.PageIndex = e.NewPageIndex
                State.PageIndex = Grid.PageIndex
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                If e.Row.RowType = DataControlRowType.DataRow Then

                    'e.Row.Cells(Me.GRID_COL_COUNTRY).Text = LookupListNew.GetDescriptionFromId(LookupListNew.LK_COUNTRIES, _
                    ' New Guid(CType(dvRow(PriceList.PriceListSearchDV.COL_NAME_COUNTRY_ID), Byte())))
                    e.Row.Cells(GRID_COL_STATUS_IDX).Text = dvRow(PriceList.PriceListSearchDV.COL_NAME_STATUS)
                    e.Row.Cells(GRID_COL_EFFECTIVE_IDX).Text = GetDateFormattedString(DateHelper.GetDateValue(dvRow(PriceList.PriceListSearchDV.COL_NAME_EFFECTIVE)))
                    e.Row.Cells(GRID_COL_EXPIRATION_IDX).Text = GetDateFormattedString(DateHelper.GetDateValue(dvRow(PriceList.PriceListSearchDV.COL_NAME_EXPIRATION)))
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
            Try
                If e.CommandName = "Select" Then
                    Dim lblCtrl As Label
                    Dim row As GridViewRow = CType(CType(e.CommandSource, Control).Parent.Parent, GridViewRow)
                    Dim RowInd As Integer = row.RowIndex
                    lblCtrl = CType(Grid.Rows(RowInd).Cells(GRID_COL_PRICE_LIST_ID_IDX).FindControl(GRID_CTRL_PRICE_LIST_ID), Label)
                    State.SelectedPriceListId = New Guid(lblCtrl.Text)
                    callPage(PriceListDetailForm.URL, State.SelectedPriceListId)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Button Clicks "

        Protected Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
            Try
                ValidateDates()
                SaveGuiState()
                State.PageIndex = 0
                State.SelectedPriceListId = Guid.Empty
                State.IsGridVisible = True
                State.SearchDV = Nothing
                State.HasDataChanged = False
                State.SearchClick = True
                PopulateGrid()

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub btnAdd_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNew.Click
            Try

                callPage(PriceListDetailForm.URL)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub


        Protected Sub btnClearSearch_Click(sender As Object, e As System.EventArgs) Handles btnClearSearch.Click
            Try
                'Clear State
                txtCode.Text = String.Empty
                txtDesription.Text = String.Empty
                txtActiveOnDate.Text = String.Empty
                txtSvcCntrName.Text = String.Empty
                ddlCountry.SelectedIndex = 0
                ddlSvcType.SelectedIndex = 0

                State.Code = String.Empty
                State.Description = String.Empty
                State.Country = Nothing
                State.ServiceType = Nothing
                State.ActiveOnDate = DateTime.Now

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region
    End Class

End Namespace