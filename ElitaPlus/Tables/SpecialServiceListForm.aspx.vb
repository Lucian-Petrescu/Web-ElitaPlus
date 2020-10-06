Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms


Namespace Tables

    Partial Class SpecialServiceListForm
        Inherits ElitaPlusSearchPage

        Protected WithEvents moErrorController As ErrorController

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub


        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Constants"

        Private Const SPECIALSERVICE_LIST_FORM001 As String = "SPECIALSERVICE_LIST_FORM001" ' Maintain Product Code List Exception
        Private Const SPECIALSERVICE_DETAIL_PAGE As String = "SpecialServiceForm.aspx"
        Private Const SPECIALSERVICE_STATE As String = "SpecialServiceState"
        Public Const GRID_TOTAL_COLUMNS As Integer = 6
        Public Const GRID_COL_EDIT_IDX As Integer = 0
        Public Const GRID_COL_SPECIAL_SERVICE_IDX As Integer = 1
        Public Const GRID_COL_DEALER_NAME As Integer = 0
        Public Const GRID_COL_COVERAGE_TYPE As Integer = 1
        Public Const GRID_COL_CAUSE_OF_LOSS As Integer = 2
        Private Const LABEL_DEALER As String = "DEALER"
        Private Const SPECIAL_SERVICE As String = "SPECIAL_SERVICE"
#End Region

#Region "Properties"

        Public ReadOnly Property TheDealerControl() As MultipleColumnDDLabelControl_New
            Get
                If multipleDropControl Is Nothing Then
                    multipleDropControl = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl_New)
                End If
                Return multipleDropControl
            End Get
        End Property

#End Region

#Region "Page State"

        ' This class keeps the current state for the search page.
        Class MyState

            Public IsGridVisible As Boolean = False
            Public searchDV As SPECIALSERVICE.SPECIALSERVICESearchDV = Nothing

            Public SortExpression As String = SpecialService.SpecialServiceSearchDV.COL_COVERAGE_TYPE
            Public PageIndex As Integer = 0
            Public PageSort As String
            Public PageSize As Integer = 30
            Public SearchDataView As SPECIALSERVICE.SPECIALSERVICESearchDV
            Public moSPECIALSERVICEId As Guid = Guid.Empty
            'Public inputParameters As Parameters

            ' these variables are used to store the sorting columns information.
            'Public SortColumns(GRID_TOTAL_COLUMNS - 1) As String
            'Public IsSortDesc(GRID_TOTAL_COLUMNS - 1) As Boolean

            Public SPECIALSERVICEMask As String
            Public DealerId As Guid = Guid.Empty
            Public CoverageTypeId As Guid = Guid.Empty

            Sub New()
                '    SortColumns(GRID_COL_DEALER_NAME) = SPECIALSERVICE.SPECIALSERVICESearchDV.COL_DEALER_NAME
                '    SortColumns(GRID_COL_PRODUCT_CODE) = SPECIALSERVICE.SPECIALSERVICESearchDV.COL_PRODUCT_CODE
                '    SortColumns(GRID_COL_RISK_GROUP) = SPECIALSERVICE.SPECIALSERVICESearchDV.COL_RISK_GROUP
                '    SortColumns(GRID_COL_DESCRIPTION) = SPECIALSERVICE.SPECIALSERVICESearchDV.COL_DESCRIPTION

                '    IsSortDesc(GRID_COL_DEALER_NAME) = False
                '    IsSortDesc(GRID_COL_PRODUCT_CODE) = False
                '    IsSortDesc(GRID_COL_RISK_GROUP) = False
                '    IsSortDesc(GRID_COL_DESCRIPTION) = False
                ''moSPECIALSERVICEData = New SPECIALSERVICEData
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

#End Region

#Region "Page Return"

        Private IsReturningFromChild As Boolean = False

        Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles Me.PageReturn
            Try
                IsReturningFromChild = True
                Dim retObj As ReturnType = CType(ReturnPar, ReturnType)
                If retObj IsNot Nothing AndAlso retObj.BoChanged Then
                    State.searchDV = Nothing
                End If
                If retObj IsNot Nothing Then
                    Select Case retObj.LastOperation
                        Case ElitaPlusPage.DetailPageCommand.Back
                            State.moSPECIALSERVICEId = retObj.moSPECIALSERVICEId
                        Case ElitaPlusPage.DetailPageCommand.Delete
                            AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
                            State.moSPECIALSERVICEId = Guid.Empty
                        Case Else
                            State.moSPECIALSERVICEId = Guid.Empty
                    End Select
                    moSplServiceGrid.PageIndex = State.PageIndex
                    moSplServiceGrid.PageSize = State.PageSize
                    cboPageSize.SelectedValue = CType(State.PageSize, String)
                    moSplServiceGrid.PageSize = State.PageSize
                    ControlMgr.SetVisibleControl(Me, trPageSize, moSplServiceGrid.Visible)
                    'PopulateDealer()
                    'PopulateRiskGroup()
                End If
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub



#End Region

        Public Class ReturnType
            Public LastOperation As ElitaPlusPage.DetailPageCommand
            Public moSPECIALSERVICEId As Guid
            Public BoChanged As Boolean = False

            Public Sub New(LastOp As ElitaPlusPage.DetailPageCommand, oSPECIALSERVICEId As Guid, Optional ByVal boChanged As Boolean = False)
                LastOperation = LastOp
                moSPECIALSERVICEId = oSPECIALSERVICEId
                Me.BoChanged = boChanged
            End Sub

        End Class

        '#End Region
#Region "Page_Events"

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            

            Try
                moErrorController.Clear_Hide()
                MasterPage.MessageController.Clear()

                'Setting the bread crum navigation
                MasterPage.UsePageTabTitleInBreadCrum = False
                MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("TABLES")
                UpdateBreadCrum()
                'SetSession()
                'moSplServiceGrid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
                If Not Page.IsPostBack Then
                    SetDefaultButton(cboCoverageType, btnSearch)
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    '   Me.MenuEnabled = True
                    SetGridItemStyleColor(moSplServiceGrid)
                    TranslateGridHeader(moSplServiceGrid)
                    PopulateDealer()
                    PopulateCoverageType()
                    If Not IsReturningFromChild Then
                        ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    Else
                        ' It is returning from detail
                        PopulateGrid(POPULATE_ACTION_SAVE)
                    End If
                    cboPageSize.SelectedValue = State.PageSize.ToString()
                End If
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
            ShowMissingTranslations(moErrorController)
        End Sub

#End Region

#Region "Controlling Logic"
        Private Sub UpdateBreadCrum()
            'If (Not Me.State Is Nothing) Then
            'If (Not Me.State.searchDV Is Nothing) Then
            MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & _
                TranslationBase.TranslateLabelOrMessage(SPECIAL_SERVICE)
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(SPECIAL_SERVICE)
            'End If
            'End If
        End Sub

        Private Sub PopulateDealer()
            Try

                Dim oDataView As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
                TheDealerControl.Caption = TranslationBase.TranslateLabelOrMessage(LABEL_DEALER)
                'TheDealerControl.NO_CAPTION
                TheDealerControl.NothingSelected = True
                TheDealerControl.BindData(oDataView)
                TheDealerControl.AutoPostBackDD = False
                TheDealerControl.NothingSelected = True
                'TheDealerControl.Mode = TheDealerControl.MODES.NEW_MODE
                TheDealerControl.SelectedGuid = State.DealerId

            Catch ex As Exception
                moErrorController.AddError(SPECIALSERVICE_LIST_FORM001)
                moErrorController.AddError(ex.Message, False)
                moErrorController.Show()
            End Try
        End Sub

        Private Sub PopulateCoverageType()
            Try
                Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                'Me.BindListControlToDataView(cboCoverageType, LookupListNew.GetCoverageTypeByCompanyGroupLookupList(oLanguageId, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, False), , , True)
                Dim listcontext As ListContext = New ListContext()
                listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id

                Dim covTypeLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("CoverageTypeByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                cboCoverageType.Populate(covTypeLkl, New PopulateOptions() With
               {
                .AddBlankItem = True
               })
                BindSelectItem(State.CoverageTypeId.ToString, cboCoverageType)
            Catch ex As Exception
                moErrorController.AddError(SPECIALSERVICE_LIST_FORM001)
                moErrorController.AddError(ex.Message, False)
                moErrorController.Show()
            End Try
        End Sub

        Private Sub BindDataGrid(oDataView As DataView)
            moSplServiceGrid.DataSource = oDataView
            moSplServiceGrid.DataBind()
        End Sub


        Private Sub PopulateGrid(Optional ByVal oAction As String = POPULATE_ACTION_NONE)
            Dim oDataView As DataView

            Try
                If (State.searchDV Is Nothing) Then
                    State.searchDV = SpecialService.getList(ElitaPlusIdentity.Current.ActiveUser.LanguageId, ElitaPlusIdentity.Current.ActiveUser.Companies, TheDealerControl.SelectedGuid, GetSelectedItem(cboCoverageType))
                End If

                State.searchDV.Sort = State.SortExpression
                moSplServiceGrid.AutoGenerateColumns = False

                moSplServiceGrid.Columns(GRID_COL_DEALER_NAME).SortExpression = SPECIALSERVICE.SPECIALSERVICESearchDV.COL_DEALER_NAME
                moSplServiceGrid.Columns(GRID_COL_COVERAGE_TYPE).SortExpression = SPECIALSERVICE.SPECIALSERVICESearchDV.COL_COVERAGE_TYPE
                moSplServiceGrid.Columns(GRID_COL_CAUSE_OF_LOSS).SortExpression = SpecialService.SpecialServiceSearchDV.COL_CAUSE_OF_LOSS
                HighLightSortColumn(moSplServiceGrid, State.SortExpression)
                BasePopulateGrid(moSplServiceGrid, State.searchDV, State.moSPECIALSERVICEId, oAction)

                ControlMgr.SetVisibleControl(Me, trPageSize, moSplServiceGrid.Visible)

                Session("recCount") = State.searchDV.Count

                If moSplServiceGrid.Visible Then
                    RecordCountLabel.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If

            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                State.PageSize = CType(cboPageSize.SelectedValue, Integer)
                moSplServiceGrid.PageIndex = NewCurrentPageIndex(moSplServiceGrid, CType(Session("recCount"), Int32), State.PageSize)
                'Me.State.PageSize = moSplServiceGrid.PageSize
                State.PageIndex = NewCurrentPageIndex(moSplServiceGrid, State.searchDV.Count, State.PageSize)
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub ClearSearch()
            TheDealerControl.SelectedIndex = 0
            cboCoverageType.SelectedIndex = 0
            State.moSPECIALSERVICEId = Guid.Empty
        End Sub

        Private Function GetDataView() As DataView

            Return SpecialService.getList(ElitaPlusIdentity.Current.ActiveUser.LanguageId, ElitaPlusIdentity.Current.ActiveUser.Companies, TheDealerControl.SelectedGuid, GetSelectedItem(cboCoverageType))

        End Function

#End Region

#Region "Datagrid Related"
        Private Sub moSplServiceGrid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles moSplServiceGrid.PageIndexChanging
            Try
                moSplServiceGrid.PageIndex = e.NewPageIndex
                PopulateGrid(POPULATE_ACTION_NO_EDIT)
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub moSplServiceGrid_ItemCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles moSplServiceGrid.RowCreated
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub moSplServiceGrid_ItemCommand(source As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles moSplServiceGrid.RowCommand
            Dim sSPECIALSERVICEId As String
            Try
                'If e.CommandSource.GetType.Equals(GetType(ImageButton)) Then
                If e.CommandName = "SelectUser" Then
                    'this only runs when they click the pencil button for editing.
                    'sSPECIALSERVICEId = CType(e.Item.FindControl("moSplServiceId"), Label).Text
                    sSPECIALSERVICEId = CType(e.CommandArgument, String)
                    State.moSPECIALSERVICEId = GetGuidFromString(sSPECIALSERVICEId)
                    SetSession()

                    Dim param As New SpecialServiceForm.MyState
                    param.moSpecialServiceId = State.moSPECIALSERVICEId
                    param.CoverageTypeId = State.CoverageTypeId

                    callPage(SpecialServiceForm.URL, param)
                Else
                End If
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub moSplServiceGrid_SortCommand(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles moSplServiceGrid.Sorting
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
                moSplServiceGrid.PageIndex = 0
                moSplServiceGrid.SelectedIndex = -1
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

#End Region

#Region " Button Clicks "
        Private Sub moBtnSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnSearch.Click
            Try
                ' Dim oState As TheState
                SetSession()
                moSplServiceGrid.PageIndex = NO_PAGE_INDEX
                moSplServiceGrid.DataMember = Nothing
                State.searchDV = Nothing
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub moBtnClear_Click(sender As System.Object, e As System.EventArgs) Handles btnClear.Click
            Try
                ClearSearch()
                'hide the error controller
                moErrorController.Clear_Hide()
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub btnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNew.Click
            Try
                State.moSPECIALSERVICEId = Guid.Empty
                SetSession()

                Dim param As New SpecialServiceForm.MyState
                param.moSpecialServiceId = State.moSPECIALSERVICEId
                param.CoverageTypeId = State.CoverageTypeId

                callPage(SpecialServiceForm.URL, param)
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub
#End Region

#Region "State-Management"

        Private Sub SetSession()
            With State
                .DealerId = TheDealerControl.SelectedGuid
                .CoverageTypeId = GetSelectedItem(cboCoverageType)
                .PageIndex = moSplServiceGrid.PageIndex
                .PageSize = moSplServiceGrid.PageSize
                .PageSort = State.SortExpression
                .SearchDataView = State.searchDV
            End With
        End Sub


#End Region
    End Class
End Namespace
