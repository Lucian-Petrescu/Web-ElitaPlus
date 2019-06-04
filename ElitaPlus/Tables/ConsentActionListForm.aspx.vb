Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Imports System.Collections.Generic

Namespace Tables

    Partial Public Class ConsentActionListForm
        Inherits ElitaPlusSearchPage
#Region "Constants"


        Private Const DEFAULT_ITEM As Integer = 0
        Public Const GRID_COL_CONSENT_ID_IDX As Integer = 0
        Public Const GRID_COL_REFERENCE_TYPE_IDX As Integer = 1
        Public Const GRID_COL_REFERENCE_VALUE_IDX As Integer = 2
        Public Const GRID_COL_CONSENT_TYPE_IDX As Integer = 3
        Public Const GRID_COL_CONSENT_FIELD_NAME_IDX As Integer = 4
        Public Const GRID_COL_CONSENT_FIELD_VALUE_IDX As Integer = 5
        Public Const GRID_COL_EFFECTIVE_IDX As Integer = 6
        Public Const GRID_COL_EXPIRATION_IDX As Integer = 7
        Public Const GRID_COL_CONSENT_CTRL As String = "btndelete"
        Private Const DELETE_COMMAND As String = "DeleteRecord"
        Public Const SELECT_ACTION_COMMAND As String = "SelectAction"
        Public Const URL As String = "~/Tables/ConsentActionListForm.aspx"
        Private Const DefaultSortColumn As String = "REFERENCE_TYPE"
        Private Const SearchLimit As Integer = 100 ' number of records to be return in case of search
        Private Const MSG_RECORD_DELETED_OK As String = "MSG_RECORD_DELETED_OK"
        Private Const GRID_CTRL_NAME_LABLE_CONSENT_ID As String = "moConsentId"
#End Region

#Region "Page State"
        Class MyState

            Public IsGridVisible As Boolean = False
            Public searchDV As ConsentActions.ConsentActionsSearchDV = Nothing
            Public searchBtnClicked As Boolean = False

            Public SortExpression As String = "consent_type_xcd"
            Public boChangedStr As String = "FALSE"


            Public mnPageIndex As Integer = 0
            Public msPageSort As String
            Public mnPageSize As Integer = DEFAULT_PAGE_SIZE

            Private moSearchDataView As DataView
            Public ReferenceType As String
            Public ReferenceValueId As Guid = Guid.Empty
            Public ConsentType As String
            Public ConsentField As String

            Public selectedConsentId As Guid = Guid.Empty
            Public selectedSortById As Guid = Guid.Empty
            Public ConsentActionsId As Guid
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_

            Public ConsentSearchData As IEnumerable(Of ConsentActions)
            Public SortDirection As WebControls.SortDirection = WebControls.SortDirection.Ascending

            Public TotalResultCountFound As Integer
            Public SortColumn As String = DefaultSortColumn

            Public searchClick As Boolean = False
            Public IsEditMode As Boolean = False
            Public IsAfterSave As Boolean



#Region "State-Properties"

            Public Property PageIndex() As Integer
                Get
                    Return mnPageIndex
                End Get
                Set(ByVal Value As Integer)
                    mnPageIndex = Value
                End Set
            End Property

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

            Public Property SearchDataView() As DataView
                Get
                    Return moSearchDataView
                End Get
                Set(ByVal Value As DataView)
                    moSearchDataView = Value
                End Set
            End Property
#End Region

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

#Region "Page Return"

#End Region

#End Region


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

#Region "page events"


        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Try
                Me.MasterPage.MessageController.Clear()
                Me.Form.DefaultButton = btnSearch.UniqueID
                If Not Me.IsPostBack Then
                    Me.MasterPage.MessageController.Clear()
                    Me.MasterPage.UsePageTabTitleInBreadCrum = False
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")
                    UpdateBreadCrum()
                    TranslateGridHeader(Grid)
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    PopulateReferenceTypes()
                    EnableDropDowns(False)
                Else
                    CheckIfComingFromDeleteConfirm()
                End If
                Me.DisplayNewProgressBarOnClick(Me.btnSearch, "LOADING_CONSENT_ACTIONS")
                Me.ShowMissingTranslations(Me.MasterPage.MessageController)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub


#End Region

#Region "Handlers-DropDown"
        Private Sub moReferencetypeDrop_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moReferenceTypeDrop.SelectedIndexChanged
            Try
                ClearForReferenceType()
                If moReferenceTypeDrop.SelectedIndex > 0 Then
                    PopulateControls()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moReferenceValueDrop_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moReferenceValueDrop.SelectedIndexChanged
            Try
                ClearForReferenceValue()
                If moReferenceValueDrop.SelectedIndex > 0 Then
                    PopulateConsentType()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moConsentTypeDrop_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moConsentTypeDrop.SelectedIndexChanged
            Try
                ClearForConsentType()
                If moConsentTypeDrop.SelectedIndex > 0 Then
                    PopulateConsentField()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub


        Private Sub cboPageSize_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                State.PageSize = CType(cboPageSize.SelectedValue, Integer)
                State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.PageSize)
                Grid.PageIndex = State.PageIndex
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region
#Region "Handlers-Buttons"


        Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
            Try
                Me.SetStateProperties()
                Me.State.PageIndex = 0
                Me.State.selectedConsentId = Guid.Empty
                Me.State.IsGridVisible = True
                Me.State.searchClick = True
                Me.State.searchDV = Nothing
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Protected Sub btnClearSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClearSearch.Click
            Try

                ' Clear all search options typed or selected by the user
                ClearAllSearchOptions()

                ' Update the state properties with the new value
                SetStateProperties()

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Populate"

        Private Sub PopulateReferenceTypes()
            Try

                Me.moReferenceTypeDrop.Populate(CommonConfigManager.Current.ListManager.GetList("REFERENCE_TYPE", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                                {
                                                 .AddBlankItem = True,
                                                 .BlankItemValue = String.Empty,
                                                 .ValueFunc = AddressOf PopulateOptions.GetCode
                                                })
                ' Me.State.ReferenceType = Me.GetSelectedValue(moReferenceTypeDrop)
                BindSelectItem(Me.State.ReferenceType, moReferenceTypeDrop)
                If Me.State.IsGridVisible Then
                    PopulateControls()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateControls()
            Try
                If moReferenceTypeDrop.SelectedIndex > 0 Then
                    Me.State.ReferenceType = Me.GetSelectedValue(moReferenceTypeDrop)
                    Select Case Me.State.ReferenceType
                        Case "ELP_COMPANY"
                            PopulateCompaniesDropdown()
                        Case "ELP_DEALER"
                            PopulateDealersDropdown()
                        Case "ELP_COUNTRY"
                            PopulateCountriesDropdown()
                    End Select
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateCompaniesDropdown()
            Try
                Dim companyList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="Company")

                Dim filteredCompanyList As DataElements.ListItem() = (From x In companyList
                                                                      Where ElitaPlusIdentity.Current.ActiveUser.Companies.Contains(x.ListItemId)
                                                                      Select x).ToArray()

                Dim companyTextFunc As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                                                                                    Return li.Translation + " " + "(" + li.Code + ")"
                                                                                End Function

                moReferenceValueDrop.Populate(filteredCompanyList, New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = False,
                                                    .TextFunc = companyTextFunc
                                                   })

                BindSelectItem(Me.State.ReferenceValueId.ToString, moReferenceValueDrop)
                ControlMgr.SetEnableControl(Me, moReferenceValueDrop, True)
                If Me.State.IsGridVisible AndAlso moReferenceValueDrop.SelectedIndex > 0 Then
                    ClearForConsentType()
                    Me.PopulateConsentType()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateDealersDropdown()
            Try
                Dim oDealerList = GetDealerListByCompanyForUser()
                Dim dealerTextFunc As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                                                                                   Return li.Translation + " " + "(" + li.Code + ")"
                                                                               End Function
                moReferenceValueDrop.Populate(oDealerList, New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = True,
                                                    .TextFunc = dealerTextFunc
                                                   })
                BindSelectItem(Me.State.ReferenceValueId.ToString, moReferenceValueDrop)
                ControlMgr.SetEnableControl(Me, moReferenceValueDrop, True)
                If Me.State.IsGridVisible AndAlso moReferenceValueDrop.SelectedIndex > 0 Then
                    ClearForConsentType()
                    Me.PopulateConsentType()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateCountriesDropdown()
            Try
                Dim oCountriesList = GetCountriesListByForUser()
                'Dim CountryTextFunc As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                '                                                                    Return li.Translation + " " + "(" + li.Code + ")"
                '                                                                End Function
                moReferenceValueDrop.Populate(oCountriesList, New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = True
                                                   })
                BindSelectItem(Me.State.ReferenceValueId.ToString, moReferenceValueDrop)
                ControlMgr.SetEnableControl(Me, moReferenceValueDrop, True)
                If Me.State.IsGridVisible AndAlso moReferenceValueDrop.SelectedIndex > 0 Then
                    ClearForConsentType()
                    Me.PopulateConsentType()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateConsentType()
            Try
                Me.moConsentTypeDrop.Populate(CommonConfigManager.Current.ListManager.GetList("CONSENT_TYPE", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                                                {.AddBlankItem = True,
                                                                 .BlankItemValue = String.Empty,
                                                                 .ValueFunc = AddressOf PopulateOptions.GetExtendedCode
                                                                })
                BindSelectItem(Me.State.ConsentType, moConsentTypeDrop)
                ControlMgr.SetEnableControl(Me, moConsentTypeDrop, True)
                If Me.State.IsGridVisible AndAlso moConsentTypeDrop.SelectedIndex > 0 Then
                    Me.PopulateConsentField()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateConsentField()
            Try
                Me.moConsentFieldNameDrop.Populate(CommonConfigManager.Current.ListManager.GetList("CONSENT_FIELD_NAME", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                                                {
                                                                    .AddBlankItem = True,
                                                                    .BlankItemValue = String.Empty,
                                                                    .ValueFunc = AddressOf PopulateOptions.GetExtendedCode
                                                                })
                BindSelectItem(Me.State.ConsentField, moConsentFieldNameDrop)
                ControlMgr.SetEnableControl(Me, moConsentFieldNameDrop, True)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub


        Private Function GetDealerListByCompanyForUser() As Assurant.Elita.CommonConfiguration.DataElements.ListItem()
            Dim Index As Integer
            Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext

            Dim UserCompanies As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies

            Dim oDealerList As New Collections.Generic.List(Of Assurant.Elita.CommonConfiguration.DataElements.ListItem)

            For Index = 0 To UserCompanies.Count - 1
                'UserCompanyList &= ",'" & GuidControl.GuidToHexString(UserCompanyies(Index))
                oListContext.CompanyId = UserCompanies(Index)
                Dim oDealerListForCompany As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerListByCompany", context:=oListContext)
                If oDealerListForCompany.Count > 0 Then
                    If Not oDealerList Is Nothing Then
                        oDealerList.AddRange(oDealerListForCompany)
                    Else
                        oDealerList = oDealerListForCompany.Clone()
                    End If

                End If
            Next

            Return oDealerList.ToArray()

        End Function

        Private Function GetCountriesListByForUser() As Assurant.Elita.CommonConfiguration.DataElements.ListItem()
            Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
            oListContext.UserId = ElitaPlusIdentity.Current.ActiveUser.Id
            Dim oCountriesList As New Collections.Generic.List(Of Assurant.Elita.CommonConfiguration.DataElements.ListItem)

            Dim oCountriesListForUser As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="UserCountries", context:=oListContext)
            If oCountriesListForUser.Count > 0 Then
                If Not oCountriesList Is Nothing Then
                    oCountriesList.AddRange(oCountriesListForUser)
                Else
                    oCountriesList = oCountriesListForUser.Clone()
                End If

            End If

            Return oCountriesList.ToArray()

        End Function

        Private Sub PopulateGrid()
            Try

                Dim sortBy As String = String.Empty
                If (Me.State.searchDV Is Nothing) Then
                    If (Not (Me.State.selectedSortById.Equals(Guid.Empty))) Then
                        sortBy = LookupListNew.GetCodeFromId("REFERENCE_TYPE", Me.State.selectedSortById)
                    End If
                    Me.State.searchDV = ConsentActions.getConsentActionsList(Me.State.ReferenceType,
                                                                             Me.State.ReferenceValueId,
                                                                             Me.State.ConsentType,
                                                                             Me.State.ConsentField,
                                                                             ElitaPlusIdentity.Current.ActiveUser.LanguageId)


                    If Me.State.searchClick Then
                        Me.ValidSearchResultCountNew(Me.State.searchDV.Count, True)
                        Me.State.searchClick = False
                    End If
                End If

                Grid.PageSize = CType(cboPageSize.SelectedValue, Integer)
                SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.selectedConsentId, Me.Grid, Me.State.PageIndex)
                Me.Grid.DataSource = Me.State.searchDV
                Me.State.PageIndex = Me.Grid.PageIndex
                If (Not Me.State.SortExpression.Equals(String.Empty)) Then
                    Me.State.searchDV.Sort = Me.State.SortExpression
                Else
                    Me.State.SortExpression = sortBy
                    Me.State.searchDV.Sort = Me.State.SortExpression
                End If

                HighLightSortColumn(Me.Grid, Me.State.SortExpression, Me.IsNewUI)
                Me.Grid.DataBind()

                ControlMgr.SetVisibleControl(Me, Grid, True)
                ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)
                Session("recCount") = Me.State.searchDV.Count

                If Me.Grid.Visible Then
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If

                If State.searchDV.Count = 0 Then
                    For Each gvRow As GridViewRow In Grid.Rows
                        gvRow.Visible = False
                        gvRow.Controls.Clear()
                    Next
                    lblPageSize.Visible = False
                    cboPageSize.Visible = False
                    colonSepertor.Visible = False
                Else
                    lblPageSize.Visible = True
                    cboPageSize.Visible = True
                    colonSepertor.Visible = True
                End If

            Catch ex As Exception
                Dim GetExceptionType As String = ex.GetBaseException.GetType().Name
                If ((Not GetExceptionType.Equals(String.Empty)) And GetExceptionType.Equals("BOValidationException")) Then
                    ControlMgr.SetVisibleControl(Me, Grid, False)
                    lblPageSize.Visible = False
                    lblRecordCount.Visible = False
                    cboPageSize.Visible = False
                    colonSepertor.Visible = False
                End If
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub EnableDropDowns(ByVal bIsEnable As Boolean)
            ControlMgr.SetEnableControl(Me, moReferenceValueDrop, bIsEnable)
            ControlMgr.SetEnableControl(Me, moConsentTypeDrop, bIsEnable)
            ControlMgr.SetEnableControl(Me, moConsentFieldNameDrop, bIsEnable)
        End Sub

#End Region

#Region "Clear"

        Private Sub ClearForReferenceType()
            ClearForReferenceValue()
            Me.ClearList(moReferenceValueDrop)
            ControlMgr.SetEnableControl(Me, moReferenceValueDrop, False)
            'moRiskDrop.Enabled = False
        End Sub

        Private Sub ClearForReferenceValue()
            ClearForConsentType()
            Me.ClearList(moConsentTypeDrop)
            ControlMgr.SetEnableControl(Me, moConsentTypeDrop, False)
            'moRiskDrop.Enabled = False
        End Sub

        Private Sub ClearForConsentType()
            Me.ClearList(moConsentFieldNameDrop)
            ControlMgr.SetEnableControl(Me, moConsentFieldNameDrop, False)
            'moCoverageTypeDrop.Enabled = False
        End Sub

        'Private Sub ClearForDealer()
        '    ClearForProduct()
        '    Me.ClearList(moProductDrop)
        '    ControlMgr.SetEnableControl(Me, moProductDrop, False)
        '    'moProductDrop.Enabled = False
        'End Sub

        Private Sub ClearSearch()
            moReferenceTypeDrop.SelectedIndex = 0
            ClearForReferenceType()
        End Sub

#End Region

#Region "Other"
        Private Sub UpdateBreadCrum()
            If (Not Me.State Is Nothing) Then
                Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator &
                    TranslationBase.TranslateLabelOrMessage("CONSENT_ACTIONS")
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("CONSENT_ACTIONS")
            End If
        End Sub

        Protected Sub ClearAllSearchOptions()
            Try
                moReferenceTypeDrop.SelectedIndex = DEFAULT_ITEM
                If moReferenceValueDrop.SelectedIndex > 0 Then
                    moReferenceValueDrop.SelectedIndex = DEFAULT_ITEM

                End If
                If moConsentTypeDrop.SelectedIndex > 0 Then
                    moConsentTypeDrop.SelectedIndex = DEFAULT_ITEM

                End If
                If moConsentFieldNameDrop.SelectedIndex > 0 Then
                    moConsentFieldNameDrop.SelectedIndex = DEFAULT_ITEM

                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
        Protected Sub ClearStateValues()
            'clear State
            State.ReferenceValueId = Nothing
            State.ReferenceType = String.Empty
            State.ConsentField = String.Empty
            State.ConsentType = String.Empty
            State.SortColumn = DefaultSortColumn 'String.Empty
            State.TotalResultCountFound = 0
        End Sub

        Private Sub SetStateProperties()
            If State Is Nothing Then
                Trace(Me, "Restoring State")
                RestoreState(New MyState)
            End If

            ClearStateValues()


            State.ReferenceType = GetSelectedValue(moReferenceTypeDrop)
            State.ReferenceValueId = GetSelectedItem(moReferenceValueDrop)
            State.ConsentType = GetSelectedValue(moConsentTypeDrop)
            State.ConsentField = GetSelectedValue(moConsentFieldNameDrop)

            State.SortColumn = GetSelectedValue(moReferenceTypeDrop)
        End Sub

#End Region

#Region "Grid Action"

        Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                If (e.Row.RowType = DataControlRowType.DataRow) _
                OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                    e.Row.Cells(GRID_COL_REFERENCE_TYPE_IDX).Text = dvRow(ConsentActions.ConsentActionsSearchDV.COL_REFERENCE_TYPE).ToString
                    e.Row.Cells(GRID_COL_REFERENCE_VALUE_IDX).Text = dvRow(ConsentActions.ConsentActionsSearchDV.COL_REFERENCE_VALUE).ToString
                    e.Row.Cells(GRID_COL_CONSENT_TYPE_IDX).Text = dvRow(ConsentActions.ConsentActionsSearchDV.COL_CONSENT_TYPE).ToString
                    e.Row.Cells(GRID_COL_CONSENT_FIELD_NAME_IDX).Text = dvRow(ConsentActions.ConsentActionsSearchDV.COL_CONSENT_FIELD_NAME).ToString
                    e.Row.Cells(GRID_COL_CONSENT_FIELD_VALUE_IDX).Text = dvRow(ConsentActions.ConsentActionsSearchDV.COL_CONSENT_FIELD_VALUE).ToString
                    e.Row.Cells(GRID_COL_EFFECTIVE_IDX).Text = dvRow(ConsentActions.ConsentActionsSearchDV.COL_EFFECTIVE).ToString
                    e.Row.Cells(GRID_COL_EXPIRATION_IDX).Text = dvRow(ConsentActions.ConsentActionsSearchDV.COL_EXPIRATION).ToString
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.PageIndexChanged
            Try
                Me.State.PageIndex = Grid.PageIndex
                Me.State.selectedConsentId = Guid.Empty
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

        Private Sub Grid_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Helper functions"
        Protected Sub CheckIfComingFromDeleteConfirm()
            Dim confResponse As String = Me.HiddenDeletePromptResponse.Value
            If Not confResponse Is Nothing AndAlso confResponse = MSG_VALUE_YES Then
                If Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete Then
                    DoDelete()
                End If
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Delete
                End Select
            ElseIf Not confResponse Is Nothing AndAlso confResponse = MSG_VALUE_NO Then
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

            Dim obj As ConsentActions = New ConsentActions(Me.State.ConsentActionsId)

            obj.DeleteAndSave()

            Me.MasterPage.MessageController.AddSuccess(MSG_RECORD_DELETED_OK, True)

            Me.State.PageIndex = Grid.PageIndex

            'Set the IsAfterSave flag to TRUE so that the Paging logic gets invoked
            Me.State.IsAfterSave = True
            Me.State.searchDV = Nothing
            PopulateGrid()
            Me.State.PageIndex = Grid.PageIndex
            Me.State.IsEditMode = False
        End Sub
        Protected Sub Grid_RowCommand(sender As Object, e As GridViewCommandEventArgs)
            Try
                Dim index As Integer
                If (e.CommandName = DELETE_COMMAND) Then
                    index = CInt(e.CommandArgument)
                    Me.State.ConsentActionsId = New Guid(CType(Me.Grid.Rows(index).Cells(GRID_COL_CONSENT_ID_IDX).FindControl(GRID_CTRL_NAME_LABLE_CONSENT_ID), Label).Text)
                    Me.DisplayMessage(Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenDeletePromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete

                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
#End Region
    End Class
End Namespace