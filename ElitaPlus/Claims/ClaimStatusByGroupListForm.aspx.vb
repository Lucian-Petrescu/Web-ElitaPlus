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
        With Me.State
            .PageIndex = Grid.CurrentPageIndex
            .PageSize = Grid.PageSize
        End With
    End Sub

    Private Sub GetSession()
        With Me.State
            Me.Grid.PageSize = .PageSize
            cboPageSize.SelectedValue = CType(.PageSize, String)
        End With
    End Sub
#End Region

#Region "Page Return"

#End Region

#Region "Page Events"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.ErrControllerMaster.Clear_Hide()
        Try
            If Not Me.IsPostBack Then
                Me.SetFormTitle(PAGETITLE)
                Me.SetFormTab(PAGETAB)
                Me.SetDefaultButton(Me.rdoDealer, btnSearch)
                Me.SetDefaultButton(Me.rdoCompanyGroup, btnSearch)
                SetButtonState()
                PopulateDealer()

                If Me.IsReturningFromChild Then
                    cboPageSize.SelectedValue = CType(Me.State.PageSize, String)
                    Grid.PageSize = Me.State.PageSize
                    Me.PopulateGrid()
                End If
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
        Me.ShowMissingTranslations(Me.ErrControllerMaster)
    End Sub

    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        Try
            Me.MenuEnabled = True
            Me.IsReturningFromChild = True
            'Me.State.searchDV = Nothing

            Dim retObj As ClaimStatusByGroupForm.ReturnType = CType(ReturnPar, ClaimStatusByGroupForm.ReturnType)
            Me.State.HasDataChanged = retObj.HasDataChanged

            Select Case retObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Back
                    If Not retObj Is Nothing Then
                        Me.State.dealerId = retObj.dealerId
                        If retObj.ObjectType = retObj.TargetType.CompanyGroup Then
                            Me.rdoCompanyGroup.Checked = True
                            Me.rdoDealer.Checked = False
                        Else
                            Me.rdoDealer.Checked = True
                            Me.rdoCompanyGroup.Checked = False
                        End If

                        Me.State.IsGridVisible = True
                    End If
            End Select

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub Page_LoadComplete(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.LoadComplete
        EnableDisableFields()
    End Sub
#End Region

#Region "Helper functions"

    Private Sub PopulateDealer()
        Try
            Dim oDealerview As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
            TheDealerControl.SetControl(False, _
                                        TheDealerControl.MODES.NEW_MODE, _
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
            Me.TheDealerControl.SelectedGuid = Me.State.dealerId
        Catch ex As Exception
            Me.ErrControllerMaster.AddError(BRANCH_LIST_FORM001)
            Me.ErrControllerMaster.AddError(ex.Message, False)
            Me.ErrControllerMaster.Show()
        End Try
    End Sub

    Private Sub PopulateGrid()

        Me.State.dealerId = TheDealerControl.SelectedGuid

        If Me.State.searchDV Is Nothing Then SearchClaimStatusByGroup()

        Me.Grid.AutoGenerateColumns = False

        If Me.State.dealerId.Equals(Guid.Empty) Then
            SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Authentication.CurrentUser.CompanyGroup.Id, Me.Grid, Me.State.PageIndex, (Grid.EditItemIndex > GRID_NO_SELECTEDITEM_INX))
        Else
            SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.dealerId, Me.Grid, Me.State.PageIndex, (Grid.EditItemIndex > GRID_NO_SELECTEDITEM_INX))
        End If

        Me.State.PageIndex = Me.Grid.CurrentPageIndex
        Me.Grid.DataSource = Me.State.searchDV
        Me.Grid.DataBind()
        ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)
        ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)

        'Session("recCount") = Me.State.searchDV.Count
        If Me.Grid.Visible Then
            Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
        End If

    End Sub

    Private Sub SearchClaimStatusByGroup()

        State.searchDV = ClaimStatusByGroup.getList(GetSearchBy(), Authentication.CurrentUser.CompanyGroup.Id, Me.State.dealerId)

        With Me.State
            .bDealer = Me.rdoDealer.Checked
            .bCompanyGroup = Me.rdoCompanyGroup.Checked
        End With

    End Sub

    Private Sub EnableDisableFields()
    End Sub

#End Region

#Region "Grid related"
    Public Sub ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Try
            If e.CommandName = "SelectAction" Then
                Me.IsNew = "N"
                params.Add(GetSearchBy())
                params.Add(Me.State.dealerId)
                params.Add(Me.IsNew)
                Me.callPage(ClaimStatusByGroup.URL, params)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Public Sub ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
        Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

        Try
            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_COMPANY_GROUP_NAME_IDX), dvRow(ClaimStatusByGroup.ClaimStatusByGroupSearchDV.COL_NAME_COMPANY_GROUP_NAME))
                Me.PopulateControlFromBOProperty(e.Item.Cells(Me.GRID_COL_COMPANY_GROUP_CODE_IDX), dvRow(ClaimStatusByGroup.ClaimStatusByGroupSearchDV.COL_NAME_COMPANY_GROUP_CODE))
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged
        Try
            Me.State.PageIndex = e.NewPageIndex
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Protected Sub cboPageSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            Me.State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.PageSize)
            Me.Grid.CurrentPageIndex = Me.State.PageIndex
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub
#End Region

#Region "Button event handlers"
    Protected Sub btnClearSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
        Me.rdoCompanyGroup.Checked = True
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Me.State.PageIndex = 0
            Me.State.IsGridVisible = True
            Me.State.searchDV = Nothing
            Me.State.HasDataChanged = False
            Me.PopulateGrid()
        Catch ex As Exception
            Me.State.IsGridVisible = False
            ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, trPageSize, Me.State.IsGridVisible)
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Function GetSearchBy() As Assurant.ElitaPlus.DALObjects.ClaimStatusByGroupDAL.SearchByType
        Dim SearchBy As Assurant.ElitaPlus.DALObjects.ClaimStatusByGroupDAL.SearchByType

        If Me.rdoDealer.Checked Then
            SearchBy = DALObjects.ClaimStatusByGroupDAL.SearchByType.Dealer
        Else
            SearchBy = DALObjects.ClaimStatusByGroupDAL.SearchByType.CompanyGroup
        End If

        Return SearchBy
    End Function

    Private Sub BtnNew_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew_WRITE.Click
        Try
            Me.IsNew = "Y"
            params.Add(GetSearchBy())                               ' SearchBy: 1=Dealer; 2=ComanpyGroup
            params.Add(Guid.Empty)
            params.Add(Me.IsNew)
            Me.callPage(ClaimStatusByGroup.URL, params)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Public Sub onchange_rdoDealer(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoDealer.CheckedChanged
        Try
            Me.State.dealerId = Guid.Empty
            Me.State.searchDV = Nothing
            PopulateDealer()
            SetButtonState()
            'Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Public Sub onchange_rdoCompanyGroup(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoCompanyGroup.CheckedChanged
        Me.State.dealerId = Guid.Empty
        Me.State.searchDV = Nothing
        PopulateDealer()
        SetButtonState()
        'Me.PopulateGrid()
    End Sub

    Private Sub SetButtonState()
        Try
            If GetSearchBy() = ClaimStatusByGroupForm.SearchByType.CompanyGroup Then
                If ClaimStatusByGroup.IsClaimStatusExist(ClaimStatusByGroupForm.SearchByType.CompanyGroup, Authentication.CurrentUser.CompanyGroup.Id, Guid.Empty) Then
                    ControlMgr.SetEnableControl(Me, Me.BtnNew_WRITE, False)
                Else
                    ControlMgr.SetEnableControl(Me, Me.BtnNew_WRITE, True)
                End If
            Else
                ControlMgr.SetEnableControl(Me, Me.BtnNew_WRITE, True)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

#End Region

End Class