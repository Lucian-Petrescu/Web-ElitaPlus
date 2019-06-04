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
        Private Const GRID_COL_EFFECTIVE_IDX As Integer = 4
        Private Const GRID_COL_EXPIRATION_IDX As Integer = 5

        Private Const GRID_COL_PRICE_LIST_ID_IDX As Integer = 6

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

        Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
            Try
                Me.MenuEnabled = True
                Me.IsReturningFromChild = True
                Dim retObj As PriceListDetailForm.ReturnType = CType(ReturnPar, PriceListDetailForm.ReturnType)
                Me.State.HasDataChanged = retObj.HasDataChanged
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If Not retObj Is Nothing Then
                            If Not retObj.EditingBo.IsNew Then
                                Me.State.SelectedPriceListId = retObj.EditingBo.Id
                            End If
                            Me.State.IsGridVisible = True
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        Me.DisplayMessage(Message.DELETE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    Case ElitaPlusPage.DetailPageCommand.Expire
                        Me.DisplayMessage(Message.EXPIRE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                End Select
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub SaveGuiState()
            With Me.State
                .Code = Me.txtCode.Text
                .Description = Me.txtDesription.Text
                '   Me.SetSelectedItem(Me.ddlSvcType, .ServiceType)
                .ServiceType = New Guid(Me.ddlSvcType.SelectedValue.ToString())
                .Country = New Guid(Me.ddlCountry.SelectedValue.ToString())
                '  Me.SetSelectedItem(Me.ddlCountry, .Country)
                .ServiceCenter = Me.txtSvcCntrName.Text
                If (Me.txtActiveOnDate.Text <> "") Then
                    .ActiveOnDate = DateHelper.GetDateValue(Me.txtActiveOnDate.Text)
                End If
            End With
        End Sub

        Private Sub RestoreGuiState()
            With Me.State
                PopulateControlFromBOProperty(Me.txtCode, .Code)
                PopulateControlFromBOProperty(Me.txtDesription, .Description)
                PopulateControlFromBOProperty(Me.txtActiveOnDate, .ActiveOnDate)
                PopulateControlFromBOProperty(Me.txtSvcCntrName, .ServiceCenter)
                PopulateControlFromBOProperty(Me.ddlCountry, .Country)
                PopulateControlFromBOProperty(Me.ddlSvcType, .ServiceType)
            End With
        End Sub


#End Region

#Region "Page_Events"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Try
                Me.UpdateBreadCrum()
                Me.MasterPage.MessageController.Clear()
                Me.Form.DefaultButton = btnSearch.UniqueID

                If Not Me.IsPostBack Then
                    Me.AddCalendar_New(Me.ImageButtonActiveOnDate, Me.txtActiveOnDate)
                    'Me.SetDefaultButton(Me.txtCode, btnSearch)
                    'Me.SetDefaultButton(Me.txtDesription, btnSearch)

                    ' Me.BindListControlToDataView(Me.ddlCountry, LookupListNew.GetUserCountriesLookupList())
                    Dim countryList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="Country")

                    Dim filteredCountryList As DataElements.ListItem() = (From x In countryList
                                                                          Where ElitaPlusIdentity.Current.ActiveUser.Countries.Contains(x.ListItemId)
                                                                          Select x).ToArray()

                    Me.ddlCountry.Populate(filteredCountryList, New PopulateOptions() With
                        {
                            .AddBlankItem = True
                        })

                    'Me.BindListControlToDataView(Me.ddlSvcType, LookupListNew.GetServiceTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                    'Me.BindListControlToDataView(Me.ddlSvcType, LookupListNew.GetNewServiceTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)) 'SVCTYP
                    Me.ddlSvcType.Populate(CommonConfigManager.Current.ListManager.GetList("SVCTYP", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                        {
                            .AddBlankItem = True
                        })

                    If txtActiveOnDate.Text = String.Empty Then
                        Me.txtActiveOnDate.Text = DateTime.Now.ToString()
                    End If

                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    Me.RestoreGuiState()
                    Me.TranslateGridHeader(Grid)

                    '''Set the country to the User's country
                    If ddlCountry.Items.Count = 2 Then
                        'set the country as default selected
                        ddlCountry.SelectedIndex = 1
                        Me.State.Country = New Guid(ddlCountry.SelectedValue.ToString())
                    End If
                    '''' 
                    If Me.State.IsGridVisible Then
                        'If Not (Me.State.SelectedPageSize = DEFAULT_PAGE_SIZE) Then
                        '    cboPageSize.SelectedValue = CType(Me.State.SelectedPageSize, String)
                        '    If (Me.State.SelectedPageSize = 0) Then
                        '        Me.State.SelectedPageSize = DEFAULT_PAGE_SIZE
                        '    End If
                        '    Me.Grid.PageSize = Me.State.SelectedPageSize
                        'End If
                        Me.PopulateGrid()
                    End If
                    'Set page size
                    cboPageSize.SelectedValue = Me.State.PageSize.ToString()
                    SetFocus(Me.txtCode)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)

        End Sub

        Private Sub UpdateBreadCrum()
            Me.MasterPage.UsePageTabTitleInBreadCrum = False
            Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("SERVICE_NETWORK") & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("PRICE_LIST")
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("PRICE_LIST_SEARCH")
            Me.MasterPage.MessageController.Clear()
            Me.MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage("TABLES") & ElitaBase.Sperator & Me.MasterPage.PageTab
        End Sub

#End Region

#Region "Controlling Logic"

        Public Sub ValidateDates()

            If Not txtActiveOnDate.Text Is String.Empty Then
                If (DateHelper.IsDate(txtActiveOnDate.Text.ToString()) = False) Then
                    ElitaPlusPage.SetLabelError(Me.lblActiveOnDate)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Message.MSG_INVALID_DATE)
                End If
            End If

        End Sub

        Public Sub PopulateGrid()
            If ((Me.State.SearchDV Is Nothing) OrElse (Me.State.HasDataChanged) OrElse Me.IsReturningFromChild) Then
                Me.State.Code = txtCode.Text.Trim()
                Me.State.Description = txtDesription.Text
                Me.State.ServiceType = New Guid(ddlSvcType.SelectedValue)
                Me.State.Country = New Guid(ddlCountry.SelectedValue)
                Me.State.ServiceCenter = txtSvcCntrName.Text
                If txtActiveOnDate.Text.Trim = String.Empty Then
                    Me.State.ActiveOnDate = Nothing
                Else
                    Me.State.ActiveOnDate = DateHelper.GetDateValue(Me.txtActiveOnDate.Text)
                End If

                '''''Get the search list for the country(s)
                ''''create a string of the country(s) list.
                'Dim stbCountries As New System.Text.StringBuilder
                Dim lstCountries As New Collections.Generic.List(Of String)
                Dim strCountriesList As String

                If Guid.Equals(Me.State.Country, Guid.Empty) Then
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
                Me.State.SearchDV = PriceList.GetList(Me.State.Code,
                                                      Me.State.Description,
                                                      Me.State.ServiceType,
                                                      strCountriesList,
                                                      Me.State.ServiceCenter,
                                                      Me.State.ActiveOnDate)
            End If
            Me.State.SearchDV.Sort = Me.State.SortExpression
            Me.Grid.AutoGenerateColumns = False
            Me.Grid.PageSize = Me.State.PageSize

            SetPageAndSelectedIndexFromGuid(Me.State.SearchDV, Me.State.SelectedPriceListId, Me.Grid, Me.State.PageIndex)
            If Me.State.SearchClick Then
                Me.ValidSearchResultCountNew(Me.State.SearchDV.Count, True)
                Me.State.SearchClick = False
            End If
            Me.SortAndBindGrid()
        End Sub

        Private Sub SortAndBindGrid()
            Me.State.PageIndex = Me.Grid.PageIndex
            Me.Grid.DataSource = Me.State.SearchDV
            HighLightSortColumn(Grid, Me.State.SortExpression)
            Me.Grid.DataBind()

            ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)

            Session("recCount") = Me.State.SearchDV.Count
            Me.lblRecordCount.Text = Me.State.SearchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
        End Sub
#End Region

#Region " GridView Related "

        Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
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
                Me.State.PageIndex = 0
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Sub cboPageSize_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Me.State.PageSize = CType(cboPageSize.SelectedValue, Integer)
                Me.State.SelectedPageSize = Me.State.PageSize
                Me.State.PageIndex = NewCurrentPageIndex(Grid, State.SearchDV.Count, State.PageSize)
                Me.Grid.PageIndex = Me.State.PageIndex
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub


        Private Sub Grid_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.PageIndexChanged
            Try
                Me.State.PageIndex = Grid.PageIndex
                Me.State.SelectedPriceListId = Guid.Empty
                PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                Grid.PageIndex = e.NewPageIndex
                State.PageIndex = Grid.PageIndex
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                If e.Row.RowType = DataControlRowType.DataRow Then

                    'e.Row.Cells(Me.GRID_COL_COUNTRY).Text = LookupListNew.GetDescriptionFromId(LookupListNew.LK_COUNTRIES, _
                    ' New Guid(CType(dvRow(PriceList.PriceListSearchDV.COL_NAME_COUNTRY_ID), Byte())))
                    e.Row.Cells(Me.GRID_COL_EFFECTIVE_IDX).Text = Me.GetDateFormattedString(DateHelper.GetDateValue(dvRow(PriceList.PriceListSearchDV.COL_NAME_EFFECTIVE)))
                    e.Row.Cells(Me.GRID_COL_EXPIRATION_IDX).Text = Me.GetDateFormattedString(DateHelper.GetDateValue(dvRow(PriceList.PriceListSearchDV.COL_NAME_EXPIRATION)))
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
            Try
                If e.CommandName = "Select" Then
                    Dim lblCtrl As Label
                    Dim row As GridViewRow = CType(CType(e.CommandSource, Control).Parent.Parent, GridViewRow)
                    Dim RowInd As Integer = row.RowIndex
                    lblCtrl = CType(Grid.Rows(RowInd).Cells(GRID_COL_PRICE_LIST_ID_IDX).FindControl(Me.GRID_CTRL_PRICE_LIST_ID), Label)
                    Me.State.SelectedPriceListId = New Guid(lblCtrl.Text)
                    Me.callPage(PriceListDetailForm.URL, Me.State.SelectedPriceListId)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Button Clicks "

        Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            Try
                Me.ValidateDates()
                Me.SaveGuiState()
                Me.State.PageIndex = 0
                Me.State.SelectedPriceListId = Guid.Empty
                Me.State.IsGridVisible = True
                Me.State.SearchDV = Nothing
                Me.State.HasDataChanged = False
                Me.State.SearchClick = True
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub btnAdd_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
            Try

                Me.callPage(PriceListDetailForm.URL)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub


        Protected Sub btnClearSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
            Try
                'Clear State
                Me.txtCode.Text = String.Empty
                Me.txtDesription.Text = String.Empty
                Me.txtActiveOnDate.Text = String.Empty
                Me.txtSvcCntrName.Text = String.Empty
                Me.ddlCountry.SelectedIndex = 0
                Me.ddlSvcType.SelectedIndex = 0

                Me.State.Code = String.Empty
                Me.State.Description = String.Empty
                Me.State.Country = Nothing
                Me.State.ServiceType = Nothing
                Me.State.ActiveOnDate = DateTime.Now

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region
    End Class

End Namespace