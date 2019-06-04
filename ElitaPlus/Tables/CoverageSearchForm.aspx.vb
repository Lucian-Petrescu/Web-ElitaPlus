Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Imports Microsoft.VisualBasic

Namespace Tables

    Partial Class CoverageSearchForm
        Inherits ElitaPlusSearchPage

#Region "Page State"
        '  Private IsReturningFromChild As Boolean = False

        ' This class keeps the current state for the search page.
        Class MyState
            ' Public PageIndex As Integer = 0
            '   Public PageSize As Integer = DEFAULT_PAGE_SIZE
            Public IsGridVisible As Boolean = False
            Public searchDV As DataView = Nothing
            Public searchBtnClicked As Boolean = False

            Public SortExpression As String = Coverage.COL_DEALER
            Public boChangedStr As String = "FALSE"

            '   Public moCoverageData As Coverage
            Public mnPageIndex As Integer = 0
            Public msPageSort As String
            Public mnPageSize As Integer = DEFAULT_PAGE_SIZE
            Private moSearchDataView As DataView
            Public moCoverageId As Guid = Guid.Empty
            Public DealerId As Guid = Guid.Empty
            Public ProductCodeId As Guid = Guid.Empty
            Public ItemRiskTypeId As Guid = Guid.Empty
            Public CoverageTypeId As Guid = Guid.Empty
            Public CertificateDuration As LongType = 0
            Public CertificateDurationId As Guid = Guid.Empty
            Public CoverageDuration As LongType = 0
            Public CoverageDurationDurationId As Guid = Guid.Empty

            ' these variables are used to store the sorting columns information.
            Public SortColumns(GRID_TOTAL_COLUMNS - 1) As String
            Public IsSortDesc(GRID_TOTAL_COLUMNS - 1) As Boolean

#Region "State-Properties"

            'Public Property DealerId() As Guid
            '    Get
            '        Return moCoverageData.DealerId
            '    End Get
            '    Set(ByVal Value As Guid)
            '        moCoverageData.DealerId = Value
            '    End Set
            'End Property

            'Public Property ProductCodeId() As Guid
            '    Get
            '        Return moCoverageData.ProductCodeId
            '    End Get
            '    Set(ByVal Value As Guid)
            '        moCoverageData.ProductCodeId = Value
            '    End Set
            'End Property

            'Public Property ItemRiskTypeId() As Guid
            '    Get
            '        Return moCoverageData.ItemId
            '    End Get
            '    Set(ByVal Value As Guid)
            '        moCoverageData.ItemId = Value
            '    End Set
            'End Property

            'Public Property CoverageTypeId() As Guid
            '    Get
            '        Return moCoverageData.CoverageTypeId
            '    End Get
            '    Set(ByVal Value As Guid)
            '        moCoverageData.CoverageTypeId = Value
            '    End Set
            'End Property

            'Public Property CertificateDuration() As LongType
            '    Get
            '        Return moCoverageData.CertificateDuration
            '    End Get
            '    Set(ByVal Value As LongType)
            '        moCoverageData.CertificateDuration = Value
            '    End Set
            'End Property

            'Public Property CoverageDuration() As LongType
            '    Get
            '        Return moCoverageData.CoverageDuration
            '    End Get
            '    Set(ByVal Value As LongType)
            '        moCoverageData.CoverageDuration = Value
            '    End Set
            'End Property

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
                SortColumns(GRID_COL_DEALER_NAME) = Coverage.COL_DEALER
                SortColumns(GRID_COL_PRODUCT_CODE) = Coverage.COL_PRODUCT_CODE
                SortColumns(GRID_COL_RISK_TYPE) = Coverage.COL_RISK_TYPE
                SortColumns(GRID_COL_ITEM_NUMBER) = Coverage.COL_ITEM_NUMBER
                SortColumns(GRID_COL_COVERAGE_TYPE) = Coverage.COL_COVERAGE_TYPE
                SortColumns(GRID_COL_CERTIFICATE_DURATION) = Coverage.COL_CERTIFICATE_DURATION
                SortColumns(GRID_COL_COVERAGE_DURATION) = Coverage.COL_COVERAGE_DURATION
                SortColumns(GRID_COL_EFFECTIVE) = Coverage.COL_EFFECTIVE
                SortColumns(GRID_COL_EXPIRATION) = Coverage.COL_EXPIRATION

                IsSortDesc(GRID_COL_DEALER_NAME) = False
                IsSortDesc(GRID_COL_PRODUCT_CODE) = False
                IsSortDesc(GRID_COL_RISK_TYPE) = False
                IsSortDesc(GRID_COL_ITEM_NUMBER) = False
                IsSortDesc(GRID_COL_COVERAGE_TYPE) = False
                IsSortDesc(GRID_COL_CERTIFICATE_DURATION) = False
                IsSortDesc(GRID_COL_COVERAGE_DURATION) = False
                IsSortDesc(GRID_COL_EFFECTIVE) = False
                IsSortDesc(GRID_COL_EXPIRATION) = False
                '  moCoverageData = New Coverage
            End Sub

            ' this will be called before the populate list to get the correct sort order
            Public ReadOnly Property CurrentSortExpresion1() As String
                Get
                    Dim s As String
                    Dim i As Integer
                    Dim sortExp As String = ""
                    For i = 0 To Me.SortColumns.Length - 1
                        If Not Me.SortColumns(i) Is Nothing Then
                            sortExp &= Me.SortColumns(i)
                            If Me.IsSortDesc(i) Then sortExp &= " DESC"
                            sortExp &= ","
                        End If
                    Next
                    Return sortExp.Substring(0, sortExp.Length - 1) 'to remove the last comma
                End Get
            End Property

            Public Sub ToggleSort1(ByVal gridColIndex As Integer)
                IsSortDesc(gridColIndex) = Not IsSortDesc(gridColIndex)
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

        Private IsReturningFromChild As Boolean = False

        Public Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
            Try
                Me.IsReturningFromChild = True
                If Me.State.searchDV Is Nothing Then
                    Me.State.IsGridVisible = False
                Else
                    Me.State.IsGridVisible = True
                End If
                Dim retObj As ReturnType = CType(ReturnPar, ReturnType)
                If Not retObj Is Nothing AndAlso retObj.BoChanged Then
                    Me.State.searchDV = Nothing
                End If
                If Not retObj Is Nothing Then

                    Select Case retObj.LastOperation
                        Case ElitaPlusPage.DetailPageCommand.Back
                            Me.State.moCoverageId = retObj.moCoverageId
                        Case Else
                            Me.State.moCoverageId = Guid.Empty
                    End Select
                    If Me.State.IsGridVisible Then
                        moDataGrid.CurrentPageIndex = Me.State.PageIndex
                        moDataGrid.PageSize = Me.State.PageSize
                        cboPageSize.SelectedValue = CType(Me.State.PageSize, String)
                        moDataGrid.PageSize = Me.State.PageSize
                        ControlMgr.SetVisibleControl(Me, trPageSize, moDataGrid.Visible)
                    End If

                    PopulateDealer()

                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Public Class ReturnType
            Public LastOperation As ElitaPlusPage.DetailPageCommand
            Public moCoverageId As Guid
            Public BoChanged As Boolean = False

            Public Sub New(ByVal LastOp As ElitaPlusPage.DetailPageCommand, ByVal oCoverageId As Guid, Optional ByVal boChanged As Boolean = False)
                Me.LastOperation = LastOp
                Me.moCoverageId = oCoverageId
                Me.BoChanged = boChanged
            End Sub


        End Class

#End Region

#End Region

#Region "Constants"
        Private Const EDIT_BUTTON_NAME As String = "BtnEdit"
        Private Const COVERAGE_ID As Integer = 1
        Private Const CONTROL_POS As Integer = 1
        Private Const UPDATE_COMMAND As String = "Update"
        Private Const NEW_COMMAND As String = "New"

        Private Const COVERAGE_SEARCH_FORM001 As String = "COVERAGE_SEARCH_FORM001" ' Coverage Search List Exception
        Private Const COVERAGE_SEARCH_FORM002 As String = "COVERAGE_SEARCH_FORM002" ' Coverage Search Field Exception
        Private Const COVERAGE_DETAIL_PAGE As String = "CoverageForm.aspx"
        Private Const COVERAGE_STATE As String = "CoverageState"

        Public Const GRID_TOTAL_COLUMNS As Integer = 11
        Public Const GRID_COL_EDIT_IDX As Integer = 0
        Public Const GRID_COL_COVERAGE_IDX As Integer = 1
        Public Const GRID_COL_DEALER_NAME As Integer = 2
        Public Const GRID_COL_PRODUCT_CODE As Integer = 3
        Public Const GRID_COL_RISK_TYPE As Integer = 4
        Public Const GRID_COL_ITEM_NUMBER As Integer = 5
        Public Const GRID_COL_COVERAGE_TYPE As Integer = 6
        Public Const GRID_COL_CERTIFICATE_DURATION As Integer = 7
        Public Const GRID_COL_COVERAGE_DURATION As Integer = 8
        Public Const GRID_COL_EFFECTIVE As Integer = 9
        Public Const GRID_COL_EXPIRATION As Integer = 10
        Private Const LABEL_SELECT_DEALERCODE As String = "DEALER"
#End Region

#Region "Attributes"


#End Region

#Region "Properties"
        Public ReadOnly Property TheDealerControl() As MultipleColumnDDLabelControl
            Get
                If multipleDropControl Is Nothing Then
                    multipleDropControl = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
                End If
                Return multipleDropControl
            End Get
        End Property
#End Region

#Region "Handlers"

#Region "Handlers-Init"


        Protected WithEvents moErrorController As ErrorController

        'Protected WithEvents moDealerLabel As System.Web.UI.WebControls.Label
        'Protected WithEvents moDealerDrop As System.Web.UI.WebControls.DropDownList
        Protected WithEvents multipleDropControl As MultipleColumnDDLabelControl

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

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                'moErrorController.Clear_Hide() ' REQ-1295
                Me.MasterPage.MessageController.Clear_Hide() ' REQ-1295

                If Not Page.IsPostBack Then

                    'REQ-1295
                    Me.MasterPage.MessageController.Clear()
                    Me.MasterPage.UsePageTabTitleInBreadCrum = False
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")
                    UpdateBreadCrum()
                    'REQ-1295 : Changes Completed 

                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    Me.SetGridItemStyleColor(moDataGrid)
                    If Not Me.IsReturningFromChild Then
                        ' It is The First Time
                        ' It is not Returning from Detail
                        ControlMgr.SetVisibleControl(Me, trPageSize, False)
                        PopulateDealer()
                        EnableDropDowns(False)
                    Else
                        ' It is returning from detail
                        ControlMgr.SetVisibleControl(Me, moDataGrid, Me.State.IsGridVisible)
                        ControlMgr.SetVisibleControl(Me, trPageSize, Me.State.IsGridVisible)
                        If Me.State.IsGridVisible Then
                            PopulateProductCode()
                            Me.PopulateGrid(Me.POPULATE_ACTION_SAVE)
                        End If
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)
        End Sub


        'REQ-1295
        Private Sub UpdateBreadCrum()
            If (Not Me.State Is Nothing) Then
                If (Not Me.State Is Nothing) Then
                    Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("COVERAGE")
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("COVERAGE")
                End If
            End If
        End Sub

#End Region

#Region "Handlers-DropDown"

        'Private Sub moDealerDrop_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moDealerDrop.SelectedIndexChanged
        '    Try
        '        ClearForDealer()
        '        If moDealerDrop.SelectedIndex > 0 Then
        '            '   moBtnSearch.Enabled = True
        '            PopulateProductCode()
        '            'Else
        '            '    moBtnSearch.Enabled = False
        '        End If
        '    Catch ex As Exception
        '        Me.HandleErrors(ex, Me.MasterPage.MessageController)
        '    End Try
        'End Sub

        Private Sub moProductDrop_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moProductDrop.SelectedIndexChanged
            Try
                ClearForProduct()
                If moProductDrop.SelectedIndex > 0 Then
                    PopulateRiskType()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moRiskDrop_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moRiskDrop.SelectedIndexChanged
            Try
                ClearForRisk()
                If moRiskDrop.SelectedIndex > 0 Then
                    PopulateCoverageType()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moCoverageTypeDrop_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moCoverageTypeDrop.SelectedIndexChanged
            Try
                ClearForCoverageType()
                If moCoverageTypeDrop.SelectedIndex > 0 Then
                    PopulateCertificateDuration()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub


        Private Sub moCertificateDurationDrop_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moCertificateDurationDrop.SelectedIndexChanged
            Try
                ClearForCertificateDuration()
                If moCertificateDurationDrop.SelectedIndex > 0 Then
                    PopulateCoverageDuration()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub moBtnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moBtnSearch.Click
            Try
                If TheDealerControl.SelectedIndex > Me.NO_ITEM_SELECTED_INDEX Then
                    moDataGrid.CurrentPageIndex = Me.NO_PAGE_INDEX
                    Me.State.SortExpression = Coverage.COL_DEALER
                    moDataGrid.DataMember = Nothing
                    Me.State.searchDV = Nothing
                    Me.State.searchBtnClicked = True
                    PopulateGrid()
                    Me.State.searchBtnClicked = False
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moBtnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moBtnClear.Click
            Try
                ClearSearch()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub BtnNew_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew_WRITE.Click
            Try
                'SetSession()
                'Response.Redirect(COVERAGE_DETAIL_PAGE)
                Me.State.moCoverageId = Guid.Empty
                SetSession()
                Me.callPage(CoverageForm.URL, Me.State.moCoverageId)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Handlers-Grid"

        Private Sub moDataGrid_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles moDataGrid.PageIndexChanged
            Try
                moDataGrid.CurrentPageIndex = e.NewPageIndex
                PopulateGrid(POPULATE_ACTION_NO_EDIT)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Public Sub ItemCreated(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Sub ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
            Dim sCoverageId As String
            Try
                If e.CommandSource.GetType.Equals(GetType(ImageButton)) Then
                    'this only runs when they click the pencil button for editing.
                    sCoverageId = CType(e.Item.FindControl("moCoverageId"), Label).Text
                    Me.State.moCoverageId = Me.GetGuidFromString(sCoverageId)
                    SetSession()
                    Me.callPage(CoverageForm.URL, Me.State.moCoverageId)
                Else
                    '  If e.CommandName = Me.SORT_COMMAND_NAME Then
                    '        moDataGrid.DataMember = e.CommandArgument.ToString
                    '        PopulateGrid()
                    '    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moDataGrid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles moDataGrid.SortCommand
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
                Me.moDataGrid.CurrentPageIndex = 0
                Me.moDataGrid.SelectedIndex = -1
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#End Region

#Region "Populate"

        Private Sub PopulateDealer()

            Dim oDealerview As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
            'TheDealerControl.SetControl(True, _
            '                            TheDealerControl.MODES.NEW_MODE, _
            '                            True, _
            '                            oDealerview, _
            '                            TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALERCODE), _
            '                            True, True, _
            '                            , _
            '                            "multipleDropControl_moMultipleColumnDrop", _
            '                            "multipleDropControl_moMultipleColumnDropDesc", _
            '                            "multipleDropControl_lb_DropDown", _
            '                            False, _
            '                            0)
            'TheDealerControl.SelectedGuid = Me.State.DealerId
            Dim oDataView As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
            TheDealerControl.Caption = TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALERCODE)
            TheDealerControl.NothingSelected = True
            TheDealerControl.BindData(oDataView)
            TheDealerControl.SelectedGuid = Me.State.DealerId
            TheDealerControl.AutoPostBackDD = True
        End Sub
        'Try
        '    Dim oCompanyList As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
        '    Me.BindListControlToDataView(moDealerDrop, LookupListNew.GetDealerLookupList(oCompanyList), , , True)
        '    BindSelectItem(Me.State.DealerId.ToString, moDealerDrop)
        'Catch ex As Exception
        '    moErrorController.AddError(ApplicationMessages.GetApplicationMessage(COVERAGE_SEARCH_FORM002, ElitaPlusIdentity.Current.ActiveUser.LanguageId))
        '    moErrorController.AddError(ex.Message)
        '    moErrorController.Show()
        'End Try

        Private Sub PopulateProductCode()
            '   Dim oDealerId As Guid = TheDealerControl.SelectedGuid
            Try
                'Me.BindListControlToDataView(moProductDrop, LookupListNew.GetProductCodeLookupList(oDealerId), "CODE", , True)
                Dim listcontext As ListContext = New ListContext()
                listcontext.DealerId = TheDealerControl.SelectedGuid
                Dim ProdCodeLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ProductCodeByDealer", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                moProductDrop.Populate(ProdCodeLkl, New PopulateOptions() With
                     {
                         .AddBlankItem = True,
                         .TextFunc = AddressOf .GetCode,
                         .SortFunc = AddressOf .GetCode
                    })
                BindSelectItem(Me.State.ProductCodeId.ToString, moProductDrop)
                ControlMgr.SetEnableControl(Me, moProductDrop, True)
                If Me.State.IsGridVisible Then
                    ClearForRisk()
                    Me.PopulateRiskType()
                End If
                'moProductDrop.Enabled = True
            Catch ex As Exception
                moErrorController.AddError(TranslationBase.TranslateLabelOrMessage(COVERAGE_SEARCH_FORM002, ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                moErrorController.AddError(ex.Message)
                moErrorController.Show()
            End Try
        End Sub

        Private Sub PopulateRiskType()
            Dim oProductId As Guid = Me.GetSelectedItem(moProductDrop)
            Try
                'Me.BindListControlToDataView(moRiskDrop, LookupListNew.GetItemRiskTypeLookupList(oProductId), , , True)
                Dim listcontext As ListContext = New ListContext()
                listcontext.ProductCodeId = oProductId
                Dim ProdCodeLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ItemIdByProduct", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                moRiskDrop.Populate(ProdCodeLkl, New PopulateOptions() With
                     {
                         .AddBlankItem = True
                    })
                BindSelectItem(Me.State.ItemRiskTypeId.ToString, moRiskDrop)
                ControlMgr.SetEnableControl(Me, moRiskDrop, True)
                If Me.State.IsGridVisible AndAlso moRiskDrop.SelectedIndex > 0 Then
                    ClearForCoverageType()
                    Me.PopulateCoverageType()
                End If
                'moRiskDrop.Enabled = True
            Catch ex As Exception
                moErrorController.AddError(TranslationBase.TranslateLabelOrMessage(COVERAGE_SEARCH_FORM002, ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                moErrorController.AddError(ex.Message)
                moErrorController.Show()
            End Try
        End Sub

        Private Sub PopulateCoverageType()
            Dim oItemId As Guid = Me.GetSelectedItem(moRiskDrop)
            Try
                'Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                'Me.BindListControlToDataView(moCoverageTypeDrop, LookupListNew.GetCoverageTypeLookupList(oItemId, oLanguageId), , , True) 'coveragetypebyitem
                Dim listcontext As ListContext = New ListContext()
                listcontext.ItemId = oItemId
                listcontext.LanguageId = Thread.CurrentPrincipal.GetLanguageId()
                Dim coverageLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.CoverageTypeByItem, Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                moCoverageTypeDrop.Populate(coverageLkl, New PopulateOptions() With
                     {
                         .AddBlankItem = True
                    })
                'BindSelectItem(Me.State.CoverageTypeId.ToString, moCoverageTypeDrop)
                ControlMgr.SetEnableControl(Me, moCoverageTypeDrop, True)
                If Me.State.IsGridVisible AndAlso moCoverageTypeDrop.SelectedIndex > 0 Then
                    ClearForCertificateDuration()
                    Me.PopulateCertificateDuration()
                End If
                'moCoverageTypeDrop.Enabled = True
            Catch ex As Exception
                moErrorController.AddError(TranslationBase.TranslateLabelOrMessage(COVERAGE_SEARCH_FORM002, ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                moErrorController.AddError(ex.Message)
                moErrorController.Show()
            End Try
        End Sub

        Private Sub PopulateCertificateDuration()
            Dim oItemId As Guid = Me.GetSelectedItem(moRiskDrop)
            Dim oCoverageTypeId As Guid = Me.GetSelectedItem(moCoverageTypeDrop)
            Dim sCertificateDuration As String = Nothing
            Try
                'Me.BindListControlToDataView(moCertificateDurationDrop,
                'LookupListNew.GetCertificateDurationLookupList(oItemId, oCoverageTypeId),
                ' "DESCRIPTION", "ID", True)
                Dim listcontext As ListContext = New ListContext()
                listcontext.ItemId = oItemId
                listcontext.CoverageTypeId = oCoverageTypeId
                Dim compListLKl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.CertificateDurationByItemAndCoverageType, Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                moCertificateDurationDrop.Populate(compListLKl, New PopulateOptions() With
                  {
                      .AddBlankItem = True
                  })
                If Not Me.State.CertificateDuration Is Nothing Then
                    sCertificateDuration = Me.State.CertificateDuration.ToString
                End If
                BindSelectItem(Me.State.CertificateDurationId.ToString, moCertificateDurationDrop)
                ControlMgr.SetEnableControl(Me, moCertificateDurationDrop, True)
                If Me.State.IsGridVisible AndAlso moCertificateDurationDrop.SelectedIndex > 0 Then
                    'ClearForCertificateDuration()
                    Me.PopulateCoverageDuration()
                End If
                'moCertificateDurationDrop.Enabled = True
            Catch ex As Exception
                moErrorController.AddError(TranslationBase.TranslateLabelOrMessage(COVERAGE_SEARCH_FORM002, ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                moErrorController.AddError(ex.Message)
                moErrorController.Show()
            End Try


        End Sub

        Private Sub PopulateCoverageDuration()
            Dim oItemId As Guid = Me.GetSelectedItem(moRiskDrop)
            Dim oCoverageTypeId As Guid = Me.GetSelectedItem(moCoverageTypeDrop)
            Dim sCertificateDuration As String = moCertificateDurationDrop.SelectedItem.Text
            Dim sCoverageDuration As String = Nothing
            Try
                ' Me.BindListControlToDataView(moCoverageDurationDrop,
                'LookupListNew.GetCoverageDurationLookupList(oItemId, oCoverageTypeId, sCertificateDuration),
                '       "DESCRIPTION", "ID", True) 'CoverageDurationByItemCoverageTypeAndCertificateDuration
                Dim listcontext As ListContext = New ListContext()
                listcontext.ItemId = oItemId
                listcontext.CoverageTypeId = oCoverageTypeId
                listcontext.CertificateDuration = sCertificateDuration
                Dim compListLKl As ListItem() = CommonConfigManager.Current.ListManager.GetList("CoverageDurationByItemCoverageTypeAndCertificateDuration", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                moCoverageDurationDrop.Populate(compListLKl, New PopulateOptions() With
                  {
                     .AddBlankItem = True
                  })
                If Not Me.State.CoverageDuration Is Nothing Then
                    sCoverageDuration = Me.State.CoverageDuration.ToString
                End If
                BindSelectItem(Me.State.CoverageDurationDurationId.ToString, moCoverageDurationDrop)
                ControlMgr.SetEnableControl(Me, moCoverageDurationDrop, True)
                'moCoverageDurationDrop.Enabled = True
            Catch ex As Exception
                moErrorController.AddError(TranslationBase.TranslateLabelOrMessage(COVERAGE_SEARCH_FORM002, ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                moErrorController.AddError(ex.Message)
                moErrorController.Show()
            End Try
        End Sub

        Private Sub BindDataGrid(ByVal oDataView As DataView)
            moDataGrid.DataSource = oDataView
            moDataGrid.DataBind()
        End Sub

        Private Sub PopulateGrid(Optional ByVal oAction As String = POPULATE_ACTION_NONE)
            Dim oDataView As DataView

            Try
                If (Me.State.searchDV Is Nothing) Then
                    Me.State.searchDV = GetDataView()
                    ControlMgr.SetVisibleControl(Me, moDataGrid, True)
                    'Ticket # 748479 - Search grids in Tables tab should not show pop-up message when number of retrieved record is over 1,000
                    'If Me.State.searchBtnClicked Then
                    '    'Me.ValidSearchResultCount(Me.State.searchDV.Count, True)
                    'End If
                End If

                Me.State.searchDV.Sort = Me.State.SortExpression
                moDataGrid.AutoGenerateColumns = False
                moDataGrid.Columns(Me.GRID_COL_DEALER_NAME).SortExpression = Coverage.COL_DEALER
                moDataGrid.Columns(Me.GRID_COL_PRODUCT_CODE).SortExpression = Coverage.COL_PRODUCT_CODE
                moDataGrid.Columns(Me.GRID_COL_RISK_TYPE).SortExpression = Coverage.COL_RISK_TYPE
                moDataGrid.Columns(Me.GRID_COL_ITEM_NUMBER).SortExpression = Coverage.COL_ITEM_NUMBER
                moDataGrid.Columns(Me.GRID_COL_COVERAGE_TYPE).SortExpression = Coverage.COL_COVERAGE_TYPE
                moDataGrid.Columns(Me.GRID_COL_CERTIFICATE_DURATION).SortExpression = Coverage.COL_CERTIFICATE_DURATION
                moDataGrid.Columns(Me.GRID_COL_COVERAGE_DURATION).SortExpression = Coverage.COL_COVERAGE_DURATION
                moDataGrid.Columns(Me.GRID_COL_EFFECTIVE).SortExpression = Coverage.COL_EFFECTIVE_DATE_FORMAT
                moDataGrid.Columns(Me.GRID_COL_EXPIRATION).SortExpression = Coverage.COL_EXPIRATION_DATE_FORMAT
                HighLightSortColumn(moDataGrid, Me.State.SortExpression)
                BasePopulateGrid(moDataGrid, Me.State.searchDV, Me.State.moCoverageId, oAction)


                ControlMgr.SetVisibleControl(Me, trPageSize, moDataGrid.Visible)

                Session("recCount") = Me.State.searchDV.Count

                If Me.moDataGrid.Visible Then
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub EnableDropDowns(ByVal bIsEnable As Boolean)
            ControlMgr.SetEnableControl(Me, moProductDrop, bIsEnable)
            ControlMgr.SetEnableControl(Me, moRiskDrop, bIsEnable)
            ControlMgr.SetEnableControl(Me, moCoverageTypeDrop, bIsEnable)
            ControlMgr.SetEnableControl(Me, moCertificateDurationDrop, bIsEnable)
            ControlMgr.SetEnableControl(Me, moCoverageDurationDrop, bIsEnable)

            'moProductDrop.Enabled = bIsEnable
            'moRiskDrop.Enabled = bIsEnable
            'moCoverageTypeDrop.Enabled = bIsEnable
            'moCertificateDurationDrop.Enabled = bIsEnable
            'moCoverageDurationDrop.Enabled = bIsEnable
        End Sub

        Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Me.moDataGrid.CurrentPageIndex = NewCurrentPageIndex(moDataGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try
        End Sub


#End Region

#Region "Clear"

        'Private Sub ClearDropDown(ByVal aDropDown As DropDownList)
        '    Dim nCount As Integer = aDropDown.Items.Count
        '    Dim nIndex As Integer

        '    For nIndex = 0 To nCount - 1
        '        aDropDown.Items.RemoveAt(0)
        '    Next
        'End Sub

        Private Sub ClearForCertificateDuration()
            Me.ClearList(moCoverageDurationDrop)
            ControlMgr.SetEnableControl(Me, moCoverageDurationDrop, False)
            'moCoverageDurationDrop.Enabled = False
            'moDataGrid.CurrentPageIndex = 0
        End Sub

        Private Sub ClearForCoverageType()
            ClearForCertificateDuration()
            Me.ClearList(moCertificateDurationDrop)
            ControlMgr.SetEnableControl(Me, moCertificateDurationDrop, False)
            'moCertificateDurationDrop.Enabled = False
            'moDataGrid.DataSource = Nothing
            'moDataGrid.DataBind()
            'ControlMgr.SetVisibleControl(Me, trPageSize, False)
        End Sub

        Private Sub ClearForRisk()
            ClearForCoverageType()
            Me.ClearList(moCoverageTypeDrop)
            ControlMgr.SetEnableControl(Me, moCoverageTypeDrop, False)
            'moCoverageTypeDrop.Enabled = False
        End Sub

        Private Sub ClearForProduct()
            ClearForRisk()
            Me.ClearList(moRiskDrop)
            ControlMgr.SetEnableControl(Me, moRiskDrop, False)
            'moRiskDrop.Enabled = False
        End Sub

        Private Sub ClearForDealer()
            ClearForProduct()
            Me.ClearList(moProductDrop)
            ControlMgr.SetEnableControl(Me, moProductDrop, False)
            'moProductDrop.Enabled = False
        End Sub

        Private Sub ClearSearch()
            TheDealerControl.SelectedIndex = 0
            '     moBtnSearch.Enabled = False
            ClearForDealer()
            Me.State.moCoverageId = Guid.Empty
        End Sub

#End Region

#Region "State-Management"

        Private Sub SetSession()
            With Me.State
                If TheDealerControl.SelectedIndex > 0 Then
                    .DealerId = TheDealerControl.SelectedGuid
                Else
                    .DealerId = Guid.Empty
                End If
                If moProductDrop.SelectedIndex > 0 Then
                    .ProductCodeId = Me.GetSelectedItem(moProductDrop)
                Else
                    .ProductCodeId = Guid.Empty
                End If
                If moRiskDrop.SelectedIndex > 0 Then
                    .ItemRiskTypeId = Me.GetSelectedItem(moRiskDrop)
                Else
                    .ItemRiskTypeId = Guid.Empty
                End If
                If moCoverageTypeDrop.SelectedIndex > 0 Then
                    .CoverageTypeId = Me.GetSelectedItem(moCoverageTypeDrop)
                Else
                    .CoverageTypeId = Guid.Empty
                End If
                If moCertificateDurationDrop.SelectedIndex > 0 Then
                    .CertificateDuration = New Assurant.Common.Types.LongType(CType(moCertificateDurationDrop.SelectedItem.Text, Long))
                    .CertificateDurationId = Me.GetSelectedItem(moCertificateDurationDrop)
                Else
                    .CertificateDuration = New Assurant.Common.Types.LongType(0)
                End If
                If moCoverageDurationDrop.SelectedIndex > 0 Then
                    .CoverageDuration = New Assurant.Common.Types.LongType(CType(moCoverageDurationDrop.SelectedItem.Text, Long))
                    .CoverageDurationDurationId = Me.GetSelectedItem(moCoverageDurationDrop)
                Else
                    .CoverageDuration = New Assurant.Common.Types.LongType(0)
                End If

                .PageIndex = moDataGrid.CurrentPageIndex
                .PageSort = Me.State.SortExpression
                .PageSize = moDataGrid.PageSize
                .SearchDataView = Me.State.searchDV
            End With
        End Sub

#End Region

#Region "Business Part"

        Private Function GetDataView() As DataView
            Dim oCoverage As Coverage = New Coverage
            Dim oDataView As DataView
            Dim CompanyIdList As ArrayList
            Dim sCertificateDuration, sCoverageDuration As String

            With oCoverage
                CompanyIdList = ElitaPlusIdentity.Current.ActiveUser.Companies
                If TheDealerControl.SelectedIndex > 0 Then
                    Me.PopulateBOProperty(oCoverage, "DealerId", TheDealerControl.SelectedGuid)
                End If
                If moProductDrop.SelectedIndex > 0 Then
                    Me.PopulateBOProperty(oCoverage, "ProductCodeId", moProductDrop)
                End If
                If moRiskDrop.SelectedIndex > 0 Then
                    Me.PopulateBOProperty(oCoverage, "ItemId", moRiskDrop)
                End If
                If moCoverageTypeDrop.SelectedIndex > 0 Then
                    Me.PopulateBOProperty(oCoverage, "CoverageTypeId", moCoverageTypeDrop)
                End If
                If moCertificateDurationDrop.SelectedIndex > 0 Then
                    .CertificateDuration = New Assurant.Common.Types.LongType(CType(moCertificateDurationDrop.SelectedItem.Text, Long))
                End If
                If moCoverageDurationDrop.SelectedIndex > 0 Then
                    .CoverageDuration = New Assurant.Common.Types.LongType(CType(moCoverageDurationDrop.SelectedItem.Text, Long))
                End If
                oDataView = .GetList(.DealerId, .ProductCodeId, .ItemId, .CoverageTypeId, .CertificateDuration, .CoverageDuration)
            End With
            Return oDataView
        End Function

#End Region

        Private Sub OnFromDrop_Changed(ByVal fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
          Handles multipleDropControl.SelectedDropChanged
            Try
                Me.State.DealerId = TheDealerControl.SelectedGuid
                PopulateDealer()
                If TheDealerControl.SelectedIndex > 0 Then
                    PopulateProductCode()
                End If
            Catch ex As Exception
                HandleErrors(ex, Me.moErrorController)
            End Try
        End Sub

    End Class

End Namespace
