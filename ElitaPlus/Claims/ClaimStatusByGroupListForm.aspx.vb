Imports System.Threading
Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common

Partial Public Class ClaimStatusByGroupListForm
    Inherits ElitaPlusSearchPage

#Region "Constants"

    Public Const URL As String = "Claims/ClaimStatusByGroupListForm.aspx"
    Public Const PAGETITLE As String = "EXTENDED_CLAIM_STATUS"
    Public Const PAGETAB As String = "TABLES"

    Public Const NO_CAPTION As String = "00000"
    Private Const BRANCH_LIST_FORM001 As String = "BRANCH_LIST_FORM001" ' Maintain Branch List Exception

    Private Const NOTHING_SELECTED As String = "00000000-0000-0000-0000-000000000000"
    Private Const GRID_COL_EDIT_IDX As Integer = 0
    Private Const GRID_COL_ID_IDX As Integer = 1
    Private Const GRID_COL_COMPANY_GROUP_NAME_IDX As Integer = 2
    Private Const GRID_COL_COMPANY_GROUP_CODE_IDX As Integer = 3

    Private Const GRID_CONTROL_NAME_COMPANYGROUPNAME As String = "lblCompanyGroupName"
    Private Const GRID_CONTROL_NAME_COMPANYGROUPCODE As String = "lblCompanyGroupCode"

    Private Const GRID_NO_SELECTEDITEM_INX As Integer = -1

    Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
    Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
    Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

    Private IsNew As String = "N"

    Public params As New ArrayList

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
    Private IsReturningFromChild As Boolean = False

    ' This class keeps the current state for the search page.
    Class MyState
        Public MyBO As ClaimStatusByGroup
        Public EditRowNum As Integer
        Public PageIndex As Integer = 0
        Public IsGridVisible As Boolean
        Public PageSize As Integer = DEFAULT_PAGE_SIZE
        Public searchDV As DataView = Nothing
        Public HasDataChanged As Boolean
        Public IsEditMode As Boolean = False
        Public bDealer As Boolean = False
        Public bCompanyGroup As Boolean = False
        Public dealerId As Guid = Guid.Empty

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

#Region "State-Management"
    Private Sub SetSession()
        With State
            .PageIndex = Grid.CurrentPageIndex
            .PageSize = Grid.PageSize
        End With
    End Sub

    Private Sub GetSession()
        With State
            Grid.PageSize = .PageSize
            cboPageSize.SelectedValue = CType(.PageSize, String)
        End With
    End Sub
#End Region

#Region "Page Return"

#End Region

#Region "Page Events"
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        ErrControllerMaster.Clear_Hide()
        Try
            If Not IsPostBack Then
                SetFormTitle(PAGETITLE)
                SetFormTab(PAGETAB)
                SetDefaultButton(rdoDealer, btnSearch)
                SetDefaultButton(rdoCompanyGroup, btnSearch)
                SetButtonState()
                PopulateDealer()

                If IsReturningFromChild Then
                    cboPageSize.SelectedValue = CType(State.PageSize, String)
                    Grid.PageSize = State.PageSize
                    PopulateGrid()
                End If
            End If

        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
        ShowMissingTranslations(ErrControllerMaster)
    End Sub

    Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
        Try
            MenuEnabled = True
            IsReturningFromChild = True
            'Me.State.searchDV = Nothing

            Dim retObj As ClaimStatusByGroupForm.ReturnType = CType(ReturnPar, ClaimStatusByGroupForm.ReturnType)
            State.HasDataChanged = retObj.HasDataChanged

            Select Case retObj.LastOperation
                Case DetailPageCommand.Back
                    If retObj IsNot Nothing Then
                        State.dealerId = retObj.dealerId
                        If retObj.ObjectType = ClaimStatusByGroupForm.ReturnType.TargetType.CompanyGroup Then
                            rdoCompanyGroup.Checked = True
                            rdoDealer.Checked = False
                        Else
                            rdoDealer.Checked = True
                            rdoCompanyGroup.Checked = False
                        End If

                        State.IsGridVisible = True
                    End If
            End Select

        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub Page_LoadComplete(sender As Object, e As EventArgs) Handles MyBase.LoadComplete
        EnableDisableFields()
    End Sub
#End Region

#Region "Helper functions"

    Private Sub PopulateDealer()
        Try
            Dim oDealerview As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
            TheDealerControl.SetControl(False, _
                                        MultipleColumnDDLabelControl.MODES.NEW_MODE, _
                                        True, _
                                        oDealerview, _
                                        NO_CAPTION, _
                                        True, False, _
                                        , _
                                        "multipleDropControl_moMultipleColumnDrop", _
                                        "multipleDropControl_moMultipleColumnDropDesc", _
                                        "multipleDropControl_lb_DropDown", _
                                        False, _
                                        0)
            TheDealerControl.SelectedGuid = State.dealerId
        Catch ex As Exception
            ErrControllerMaster.AddError(BRANCH_LIST_FORM001)
            ErrControllerMaster.AddError(ex.Message, False)
            ErrControllerMaster.Show()
        End Try
    End Sub

    Private Sub PopulateGrid()

        State.dealerId = TheDealerControl.SelectedGuid

        If State.searchDV Is Nothing Then SearchClaimStatusByGroup()

        Grid.AutoGenerateColumns = False

        If State.dealerId.Equals(Guid.Empty) Then
            SetPageAndSelectedIndexFromGuid(State.searchDV, Authentication.CurrentUser.CompanyGroup.Id, Grid, State.PageIndex, (Grid.EditItemIndex > GRID_NO_SELECTEDITEM_INX))
        Else
            SetPageAndSelectedIndexFromGuid(State.searchDV, State.dealerId, Grid, State.PageIndex, (Grid.EditItemIndex > GRID_NO_SELECTEDITEM_INX))
        End If

        State.PageIndex = Grid.CurrentPageIndex
        Grid.DataSource = State.searchDV
        Grid.DataBind()
        ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)
        ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

        'Session("recCount") = Me.State.searchDV.Count
        If Grid.Visible Then
            lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
        End If

    End Sub

    Private Sub SearchClaimStatusByGroup()

        State.searchDV = ClaimStatusByGroup.getList(GetSearchBy(), Authentication.CurrentUser.CompanyGroup.Id, State.dealerId)

        With State
            .bDealer = rdoDealer.Checked
            .bCompanyGroup = rdoCompanyGroup.Checked
        End With

    End Sub

    Private Sub EnableDisableFields()
    End Sub

#End Region

#Region "Grid related"
    Public Sub ItemCommand(source As Object, e As DataGridCommandEventArgs)
        Try
            If e.CommandName = "SelectAction" Then
                IsNew = "N"
                params.Add(GetSearchBy())
                params.Add(State.dealerId)
                params.Add(IsNew)
                callPage(ClaimStatusByGroup.URL, params)
            End If
        Catch ex As ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Public Sub ItemCreated(sender As Object, e As DataGridItemEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles Grid.ItemDataBound
        Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

        Try
            If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_COMPANY_GROUP_NAME_IDX), dvRow(ClaimStatusByGroup.ClaimStatusByGroupSearchDV.COL_NAME_COMPANY_GROUP_NAME))
                PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_COMPANY_GROUP_CODE_IDX), dvRow(ClaimStatusByGroup.ClaimStatusByGroupSearchDV.COL_NAME_COMPANY_GROUP_CODE))
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged
        Try
            State.PageIndex = e.NewPageIndex
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Protected Sub cboPageSize_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.PageSize)
            Grid.CurrentPageIndex = State.PageIndex
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub
#End Region

#Region "Button event handlers"
    Protected Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click
        rdoCompanyGroup.Checked = True
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            State.PageIndex = 0
            State.IsGridVisible = True
            State.searchDV = Nothing
            State.HasDataChanged = False
            PopulateGrid()
        Catch ex As Exception
            State.IsGridVisible = False
            ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, trPageSize, State.IsGridVisible)
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Function GetSearchBy() As ClaimStatusByGroupDAL.SearchByType
        Dim SearchBy As ClaimStatusByGroupDAL.SearchByType

        If rdoDealer.Checked Then
            SearchBy = ClaimStatusByGroupDAL.SearchByType.Dealer
        Else
            SearchBy = ClaimStatusByGroupDAL.SearchByType.CompanyGroup
        End If

        Return SearchBy
    End Function

    Private Sub BtnNew_WRITE_Click(sender As Object, e As EventArgs) Handles BtnNew_WRITE.Click
        Try
            IsNew = "Y"
            params.Add(GetSearchBy())                               ' SearchBy: 1=Dealer; 2=ComanpyGroup
            params.Add(Guid.Empty)
            params.Add(IsNew)
            callPage(ClaimStatusByGroup.URL, params)
        Catch ex As ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Public Sub onchange_rdoDealer(sender As Object, e As EventArgs) Handles rdoDealer.CheckedChanged
        Try
            State.dealerId = Guid.Empty
            State.searchDV = Nothing
            PopulateDealer()
            SetButtonState()
            'Me.PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Public Sub onchange_rdoCompanyGroup(sender As Object, e As EventArgs) Handles rdoCompanyGroup.CheckedChanged
        State.dealerId = Guid.Empty
        State.searchDV = Nothing
        PopulateDealer()
        SetButtonState()
        'Me.PopulateGrid()
    End Sub

    Private Sub SetButtonState()
        Try
            If GetSearchBy() = ClaimStatusByGroupForm.SearchByType.CompanyGroup Then
                If ClaimStatusByGroup.IsClaimStatusExist(ClaimStatusByGroupForm.SearchByType.CompanyGroup, Authentication.CurrentUser.CompanyGroup.Id, Guid.Empty) Then
                    ControlMgr.SetEnableControl(Me, BtnNew_WRITE, False)
                Else
                    ControlMgr.SetEnableControl(Me, BtnNew_WRITE, True)
                End If
            Else
                ControlMgr.SetEnableControl(Me, BtnNew_WRITE, True)
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

#End Region

End Class