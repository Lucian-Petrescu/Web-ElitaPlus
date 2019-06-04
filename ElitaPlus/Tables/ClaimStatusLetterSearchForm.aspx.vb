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
                Set(ByVal Value As Guid)
                    moDealerId = Value
                End Set
            End Property

            Public Property ClaimStatusByGroupId() As Guid
                Get
                    Return moClaimStatusByGroupId
                End Get
                Set(ByVal Value As Guid)
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
                            Me.State.moStatusLetterId = retObj.moStatusLetterId
                        Case Else
                            Me.State.moStatusLetterId = Guid.Empty
                    End Select
                    Me.Grid.PageIndex = Me.State.PageIndex
                    Me.Grid.PageSize = Me.State.PageSize
                    cboPageSize.SelectedValue = CType(Me.State.PageSize, String)
                    Me.Grid.PageSize = Me.State.PageSize
                    ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)
                    PopulateDealer()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Public Class ReturnType
            Public LastOperation As ElitaPlusPage.DetailPageCommand
            Public moStatusLetterId As Guid
            Public BoChanged As Boolean = False

            Public Sub New(ByVal LastOp As ElitaPlusPage.DetailPageCommand, ByVal oStatusLetterId As Guid, Optional ByVal boChanged As Boolean = False)
                Me.LastOperation = LastOp
                Me.moStatusLetterId = oStatusLetterId
                Me.BoChanged = boChanged
            End Sub
        End Class

#End Region

#End Region

#Region "Handlers"

#Region "Hanlers-Init"
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Me.ErrControllerMaster.Clear_Hide()
            Try
                If Not Me.IsPostBack Then

                    Me.SetFormTitle(PAGETITLE)
                    Me.SetFormTab(PAGETAB)
                    Me.TranslateGridHeader(Grid)
                    Me.TranslateGridControls(Grid)
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)

                    Me.SetGridItemStyleColor(Me.Grid)
                    If Not Me.IsReturningFromChild Then
                        ControlMgr.SetVisibleControl(Me, trPageSize, False)
                        PopulateDealer()
                        PopulateExtendedClaimStatus()
                    Else
                        Me.PopulateGrid(Me.POPULATE_ACTION_SAVE)
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
            Me.ShowMissingTranslations(Me.ErrControllerMaster)
        End Sub

#End Region

#Region "Handlers-Grid"

        Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Me.Grid.PageIndex = NewCurrentPageIndex(Me.Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                Me.State.PageSize = Me.Grid.PageSize
                PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.PageIndexChanged
            Try
                Me.State.PageIndex = Grid.PageIndex
                State.PageIndex = Grid.PageIndex
                Me.State.moStatusLetterId = Guid.Empty
                PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanging(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                Me.Grid.PageIndex = e.NewPageIndex
                State.PageIndex = Grid.PageIndex
                'PopulateGrid(POPULATE_ACTION_NO_EDIT)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand

            Try
                If e.CommandName = "SelectUser" Then
                    Dim lblCtrl As Label
                    Dim row As GridViewRow = CType(CType(e.CommandSource, Control).Parent.Parent, GridViewRow)
                    Dim RowInd As Integer = row.RowIndex
                    lblCtrl = CType(Grid.Rows(RowInd).Cells(GRID_COL_STATUS_LETTER_ID).FindControl("moStatusLetterId"), Label)
                    Me.State.moStatusLetterId = New Guid(lblCtrl.Text)
                    Me.callPage(ClaimStatusLetterForm.URL, Me.State.moStatusLetterId)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
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
                Me.Grid.PageIndex = 0
                Me.Grid.SelectedIndex = -1
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

#End Region

#Region "Handlers-Buttons"

        Protected Sub moBtnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles moBtnSearch.Click
            Try
                Me.Grid.PageIndex = Me.NO_PAGE_INDEX
                Me.Grid.DataMember = Nothing
                Me.State.searchDV = Nothing
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Protected Sub moBtnClear_Click(ByVal sender As Object, ByVal e As EventArgs) Handles moBtnClear.Click
            Try
                ClearSearch()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub


        Protected Sub btnNew_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNew.Click
            Try
                Me.State.moStatusLetterId = Guid.Empty
                SetSession()
                Me.callPage(ClaimStatusLetterForm.URL, Me.State.moStatusLetterId)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
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
                TheDealerControl.SelectedGuid = Me.State.DealerId

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub PopulateExtendedClaimStatus()
            Try
                Dim listcontext As ListContext = New ListContext()
                listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                listcontext.LanguageId = Thread.CurrentPrincipal.GetLanguageId()

                Dim ClaimStatusList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="ClaimStatusByCompanyGroup", context:=listcontext)
                Me.ExtendedClaimStatusDropdownList.Populate(ClaimStatusList, New PopulateOptions() With
                {
                   .AddBlankItem = True
                })

                'Dim dvClaimStatus As DataView = New DataView(ClaimStatusByGroup.LoadListByCompanyGroup(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id).Tables(0))
                'Me.BindListControlToDataView(Me.ExtendedClaimStatusDropdownList, dvClaimStatus, "description", "claim_status_by_group_id", True)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub PopulateGrid(Optional ByVal oAction As String = POPULATE_ACTION_NONE)
            Try
                Dim oDataView As DataView
                Dim sortBy As String = "DEALER_CODE"
                If (Me.State.searchDV Is Nothing) Then
                    Me.State.searchDV = ClaimStatusLetter.getList(TheDealerControl.SelectedGuid, Me.GetSelectedItem(Me.ExtendedClaimStatusDropdownList))
                End If

                Grid.PageSize = State.PageSize
                If State.searchDV.Count = 0 Then
                    Dim dv As ClaimStatusLetter.ClaimStatusLetterSearchDV = State.searchDV.AddNewRowToEmptyDV
                    SetPageAndSelectedIndexFromGuid(dv, Me.State.moStatusLetterId, Me.Grid, Me.State.PageIndex)
                    Me.Grid.DataSource = dv
                Else
                    SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.moStatusLetterId, Me.Grid, Me.State.PageIndex)
                    Me.Grid.DataSource = Me.State.searchDV
                End If

                Me.State.PageIndex = Me.Grid.PageIndex
                Me.Grid.AllowSorting = False
                Me.Grid.DataBind()

                HighLightGridViewSortColumn(Grid, sortBy)
                ControlMgr.SetVisibleControl(Me, Grid, True)
                ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)
                Session("recCount") = Me.State.searchDV.Count

                If Me.Grid.Visible Then
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
                If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

                If State.searchDV.Count = 0 Then
                    For Each gvRow As GridViewRow In Grid.Rows
                        gvRow.Visible = False
                        gvRow.Controls.Clear()
                    Next
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Private Sub ClearSearch()
            TheDealerControl.SelectedIndex = 0
            Me.State.moStatusLetterId = Guid.Empty
            ExtendedClaimStatusDropdownList.SelectedIndex = 0
        End Sub

#End Region

#Region "State-Management"

        Private Sub SetSession()
            With Me.State
                .DealerId = TheDealerControl.SelectedGuid
                .PageIndex = Me.Grid.PageIndex
                .PageSize = Me.Grid.PageSize
                .PageSort = Me.State.SortExpression
                .SearchDataView = Me.State.searchDV
                .ClaimStatusByGroupId = Me.GetSelectedItem(Me.ExtendedClaimStatusDropdownList)
            End With
        End Sub

#End Region


    End Class

End Namespace
