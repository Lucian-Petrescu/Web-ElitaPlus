Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports System.Threading

Namespace Tables

    Partial Public Class ClaimStatusLetterSearchForm
        Inherits ElitaPlusSearchPage

#Region "Constants"
        Public Const PAGETITLE As String = "CLAIM_STATUS_LETTER"
        Public Const PAGETAB As String = "TABLES"

        Private Const LABEL_DEALER As String = "DEALER"

        Public Const GRID_COL_EDIT As Integer = 0
        Public Const GRID_COL_STATUS_LETTER_ID As Integer = 1
        Public Const GRID_COL_DEALER_CODE As Integer = 2
        Public Const GRID_COL_DEALER_NAME As Integer = 3
        Public Const GRID_COL_CLAIM_STATUS As Integer = 4
        Public Const GRID_COL_LETTER_TYPE As Integer = 5
        Public Const GRID_COL_NUMBER_OF_DAYS As Integer = 6
        Public Const GRID_TOTAL_COLUMNS As Integer = 7

#End Region

#Region "Variables"

        Protected WithEvents multipleDropControl As Global.Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl
        Private IsReturningFromChild As Boolean = False

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

#Region "Page State"

        Class MyState
            Public searchDV As ClaimStatusLetter.ClaimStatusLetterSearchDV = Nothing
            Public SortExpression As String = ClaimStatusLetter.ClaimStatusLetterSearchDV.COL_DEALER_CODE
            Public PageIndex As Integer = 0
            Public PageSort As String
            Public PageSize As Integer = DEFAULT_PAGE_SIZE
            Public SearchDataView As ClaimStatusLetter.ClaimStatusLetterSearchDV
            Public moStatusLetterId As Guid = Guid.Empty
            Private moClaimStatusByGroupId As Guid = Guid.Empty
            Private moDealerId As Guid = Guid.Empty

            Public Property DealerId() As Guid
                Get
                    Return moDealerId
                End Get
                Set(Value As Guid)
                    moDealerId = Value
                End Set
            End Property

            Public Property ClaimStatusByGroupId() As Guid
                Get
                    Return moClaimStatusByGroupId
                End Get
                Set(Value As Guid)
                    moClaimStatusByGroupId = Value
                End Set
            End Property

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
                            State.moStatusLetterId = retObj.moStatusLetterId
                        Case Else
                            State.moStatusLetterId = Guid.Empty
                    End Select
                    Grid.PageIndex = State.PageIndex
                    Grid.PageSize = State.PageSize
                    cboPageSize.SelectedValue = CType(State.PageSize, String)
                    Grid.PageSize = State.PageSize
                    ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
                    PopulateDealer()
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Public Class ReturnType
            Public LastOperation As ElitaPlusPage.DetailPageCommand
            Public moStatusLetterId As Guid
            Public BoChanged As Boolean = False

            Public Sub New(LastOp As ElitaPlusPage.DetailPageCommand, oStatusLetterId As Guid, Optional ByVal boChanged As Boolean = False)
                LastOperation = LastOp
                moStatusLetterId = oStatusLetterId
                Me.BoChanged = boChanged
            End Sub
        End Class

#End Region

#End Region

#Region "Handlers"

#Region "Hanlers-Init"
        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            ErrControllerMaster.Clear_Hide()
            Try
                If Not IsPostBack Then

                    SetFormTitle(PAGETITLE)
                    SetFormTab(PAGETAB)
                    TranslateGridHeader(Grid)
                    TranslateGridControls(Grid)
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)

                    SetGridItemStyleColor(Grid)
                    If Not IsReturningFromChild Then
                        ControlMgr.SetVisibleControl(Me, trPageSize, False)
                        PopulateDealer()
                        PopulateExtendedClaimStatus()
                    Else
                        PopulateGrid(POPULATE_ACTION_SAVE)
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
            ShowMissingTranslations(ErrControllerMaster)
        End Sub

#End Region

#Region "Handlers-Grid"

        Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                State.PageSize = Grid.PageSize
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanged(sender As Object, e As System.EventArgs) Handles Grid.PageIndexChanged
            Try
                State.PageIndex = Grid.PageIndex
                State.PageIndex = Grid.PageIndex
                State.moStatusLetterId = Guid.Empty
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanging(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                Grid.PageIndex = e.NewPageIndex
                State.PageIndex = Grid.PageIndex
                'PopulateGrid(POPULATE_ACTION_NO_EDIT)
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_ItemCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_ItemCommand(source As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand

            Try
                If e.CommandName = "SelectUser" Then
                    Dim lblCtrl As Label
                    Dim row As GridViewRow = CType(CType(e.CommandSource, Control).Parent.Parent, GridViewRow)
                    Dim RowInd As Integer = row.RowIndex
                    lblCtrl = CType(Grid.Rows(RowInd).Cells(GRID_COL_STATUS_LETTER_ID).FindControl("moStatusLetterId"), Label)
                    State.moStatusLetterId = New Guid(lblCtrl.Text)
                    callPage(ClaimStatusLetterForm.URL, State.moStatusLetterId)
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

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
                Grid.PageIndex = 0
                Grid.SelectedIndex = -1
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

#End Region

#Region "Handlers-Buttons"

        Protected Sub moBtnSearch_Click(sender As Object, e As EventArgs) Handles moBtnSearch.Click
            Try
                Grid.PageIndex = NO_PAGE_INDEX
                Grid.DataMember = Nothing
                State.searchDV = Nothing
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Protected Sub moBtnClear_Click(sender As Object, e As EventArgs) Handles moBtnClear.Click
            Try
                ClearSearch()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub


        Protected Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
            Try
                State.moStatusLetterId = Guid.Empty
                SetSession()
                callPage(ClaimStatusLetterForm.URL, State.moStatusLetterId)
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

#End Region

#End Region

#Region "Controlling Logic"

        Private Sub PopulateDealer()
            Try
                Dim oDataView As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
                TheDealerControl.Caption = TranslationBase.TranslateLabelOrMessage(LABEL_DEALER)
                TheDealerControl.NothingSelected = True
                TheDealerControl.BindData(oDataView)
                TheDealerControl.AutoPostBackDD = False
                TheDealerControl.NothingSelected = True
                TheDealerControl.SelectedGuid = State.DealerId

            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub PopulateExtendedClaimStatus()
            Try
                Dim listcontext As ListContext = New ListContext()
                listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                listcontext.LanguageId = Thread.CurrentPrincipal.GetLanguageId()

                Dim ClaimStatusList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="ClaimStatusByCompanyGroup", context:=listcontext)
                ExtendedClaimStatusDropdownList.Populate(ClaimStatusList, New PopulateOptions() With
                {
                   .AddBlankItem = True
                })

                'Dim dvClaimStatus As DataView = New DataView(ClaimStatusByGroup.LoadListByCompanyGroup(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id).Tables(0))
                'Me.BindListControlToDataView(Me.ExtendedClaimStatusDropdownList, dvClaimStatus, "description", "claim_status_by_group_id", True)
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub PopulateGrid(Optional ByVal oAction As String = POPULATE_ACTION_NONE)
            Try
                Dim oDataView As DataView
                Dim sortBy As String = "DEALER_CODE"
                If (State.searchDV Is Nothing) Then
                    State.searchDV = ClaimStatusLetter.getList(TheDealerControl.SelectedGuid, GetSelectedItem(ExtendedClaimStatusDropdownList))
                End If

                Grid.PageSize = State.PageSize
                If State.searchDV.Count = 0 Then
                    Dim dv As ClaimStatusLetter.ClaimStatusLetterSearchDV = State.searchDV.AddNewRowToEmptyDV
                    SetPageAndSelectedIndexFromGuid(dv, State.moStatusLetterId, Grid, State.PageIndex)
                    Grid.DataSource = dv
                Else
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.moStatusLetterId, Grid, State.PageIndex)
                    Grid.DataSource = State.searchDV
                End If

                State.PageIndex = Grid.PageIndex
                Grid.AllowSorting = False
                Grid.DataBind()

                HighLightGridViewSortColumn(Grid, sortBy)
                ControlMgr.SetVisibleControl(Me, Grid, True)
                ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
                Session("recCount") = State.searchDV.Count

                If Grid.Visible Then
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
                If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

                If State.searchDV.Count = 0 Then
                    For Each gvRow As GridViewRow In Grid.Rows
                        gvRow.Visible = False
                        gvRow.Controls.Clear()
                    Next
                End If

            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Private Sub ClearSearch()
            TheDealerControl.SelectedIndex = 0
            State.moStatusLetterId = Guid.Empty
            ExtendedClaimStatusDropdownList.SelectedIndex = 0
        End Sub

#End Region

#Region "State-Management"

        Private Sub SetSession()
            With State
                .DealerId = TheDealerControl.SelectedGuid
                .PageIndex = Grid.PageIndex
                .PageSize = Grid.PageSize
                .PageSort = State.SortExpression
                .SearchDataView = State.searchDV
                .ClaimStatusByGroupId = GetSelectedItem(ExtendedClaimStatusDropdownList)
            End With
        End Sub

#End Region


    End Class

End Namespace
