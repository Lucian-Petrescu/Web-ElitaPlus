Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Tables

    Partial Public Class WarrantyMasterSearchForm
        Inherits ElitaPlusSearchPage

#Region "Constants"
        Private Const EDIT_COMMAND As String = "EditRecord"

        Public Const PAGETITLE As String = "WARRANTY_MASTER"
        Public Const PAGETAB As String = "TABLES"

        Private Const LABEL_DEALER As String = "DEALER"

        Public Const GRID_COL_EDIT As Integer = 0
        Public Const GRID_COL_WARRANTY_MASTER_ID As Integer = 1
        Public Const GRID_COL_DEALER_CODE As Integer = 2
        Public Const GRID_COL_SKU_NUMBER As Integer = 3
        Public Const GRID_COL_SKU_DESCRIPTION As Integer = 4
        Public Const GRID_COL_MANUFACTURER_NAME As Integer = 5
        Public Const GRID_COL_MODEL_NUMBER As Integer = 6
        Public Const GRID_COL_WARRANTY_DURATION_PARTS As Integer = 7
        Public Const GRID_COL_WARRANTY_DURATION_LABOR As Integer = 8
        Public Const GRID_COL_WARRANTY_TYPE As Integer = 9
        Public Const GRID_COL_RISK_TYPE As Integer = 10
        Public Const GRID_COL_IS_DELETED As Integer = 11

        Public Const GRID_CONTROL_RISK_TYPE As String = "ddlRiskType"

        Public Const GRID_TOTAL_COLUMNS As Integer = 12
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

        Public ReadOnly Property IsEditing() As Boolean
            Get
                IsEditing = (Me.moWarrantyMasterGrid.EditItemIndex > NO_ITEM_SELECTED_INDEX)
            End Get
        End Property
#End Region

#Region "Page State"

        ' This class keeps the current state for the search page.
        Class MyState
            Public searchDV As WarrantyMaster.WarrantyMasterSearchDV = Nothing
            Public SortExpression As String = WarrantyMaster.WarrantyMasterSearchDV.COL_SKU_NUMBER

            Public myBO As WarrantyMaster
            Public PageIndex As Integer = 0
            Public PageSort As String
            Public PageSize As Integer = DEFAULT_PAGE_SIZE
            Public SearchDataView As WarrantyMaster.WarrantyMasterSearchDV
            Public moWarrantyMasterId As Guid = Guid.Empty

            Public IsEditMode As Boolean = False
            'Public EditItemIndex As Integer = -1
            Public WarrantyMasterID As Guid
            Private moDealerId As Guid = Guid.Empty

#Region "State-Properties"
            Public Property DealerId() As Guid
                Get
                    Return moDealerId
                End Get
                Set(ByVal Value As Guid)
                    moDealerId = Value
                End Set
            End Property
#End Region

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
                            Me.State.moWarrantyMasterId = retObj.moRegistrationLetterId
                        Case Else
                            Me.State.moWarrantyMasterId = Guid.Empty
                    End Select
                    moWarrantyMasterGrid.CurrentPageIndex = Me.State.PageIndex
                    moWarrantyMasterGrid.PageSize = Me.State.PageSize
                    cboPageSize.SelectedValue = CType(Me.State.PageSize, String)
                    moWarrantyMasterGrid.PageSize = Me.State.PageSize
                    ControlMgr.SetVisibleControl(Me, trPageSize, moWarrantyMasterGrid.Visible)
                    PopulateDealer()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Public Class ReturnType
            Public LastOperation As ElitaPlusPage.DetailPageCommand
            Public moRegistrationLetterId As Guid
            Public BoChanged As Boolean = False

            Public Sub New(ByVal LastOp As ElitaPlusPage.DetailPageCommand, ByVal oRegistrationLetterId As Guid, Optional ByVal boChanged As Boolean = False)
                Me.LastOperation = LastOp
                Me.moRegistrationLetterId = oRegistrationLetterId
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
                Page.Master.Page.Form.DefaultButton = moBtnSearch.UniqueID

                If Not Me.IsPostBack Then

                    Me.SetFormTitle(PAGETITLE)
                    Me.SetFormTab(PAGETAB)
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)

                    Me.SetGridItemStyleColor(moWarrantyMasterGrid)
                    If Not Me.IsReturningFromChild Then
                        ' It is The First Time
                        ' It is not Returning from Detail
                        ControlMgr.SetVisibleControl(Me, trPageSize, False)
                        PopulateDealer()
                    Else
                        ' It is returning from detail
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
                moWarrantyMasterGrid.CurrentPageIndex = NewCurrentPageIndex(moWarrantyMasterGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                Me.State.PageSize = moWarrantyMasterGrid.PageSize
                PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub moWarrantyMasterGrid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles moWarrantyMasterGrid.PageIndexChanged
            Try
                moWarrantyMasterGrid.CurrentPageIndex = e.NewPageIndex
                PopulateGrid(POPULATE_ACTION_NO_EDIT)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub moWarrantyMasterGrid_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles moWarrantyMasterGrid.ItemCreated
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub moWarrantyMasterGrid_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles moWarrantyMasterGrid.ItemCommand
            Try
                Dim index As Integer = e.Item.ItemIndex

                If (e.CommandName = Me.EDIT_COMMAND) Then
                    Me.State.IsEditMode = True
                    Me.State.WarrantyMasterID = New Guid(Me.moWarrantyMasterGrid.Items(e.Item.ItemIndex).Cells(Me.GRID_COL_WARRANTY_MASTER_ID).Text)
                    Me.State.myBO = New WarrantyMaster(Me.State.WarrantyMasterID)

                    Me.PopulateGrid()

                    Me.State.PageIndex = moWarrantyMasterGrid.CurrentPageIndex

                    'Disable all Edit and Delete icon buttons on the Grid
                    SetGridControls(Me.moWarrantyMasterGrid, False)

                    'Dim ddl As DropDownList = CType(moWarrantyMasterGrid.Items(index).Cells(GRID_COL_RISK_TYPE).FindControl(GRID_CONTROL_RISK_TYPE), DropDownList)
                    'If Not ddl Is Nothing Then
                    '    Try
                    '        SetFocus(ddl)
                    '        Me.BindListControlToDataView(ddl, LookupListNew.GetRiskTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id))
                    '        Me.SetSelectedItem(ddl, Me.State.myBO.RiskTypeId)
                    '    Catch ex As Exception
                    '    End Try
                    'End If

                    Me.SetButtonsState()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub moWarrantyMasterGrid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles moWarrantyMasterGrid.SortCommand
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
                Me.moWarrantyMasterGrid.CurrentPageIndex = 0
                Me.moWarrantyMasterGrid.SelectedIndex = -1
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub moWarrantyMasterGrid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles moWarrantyMasterGrid.ItemDataBound
            Try
                Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

                If (itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem _
                    OrElse itemType = ListItemType.SelectedItem) Then
                    e.Item.Cells(Me.GRID_COL_WARRANTY_MASTER_ID).Text = GetGuidStringFromByteArray(CType(dvRow(WarrantyMaster.WarrantyMasterSearchDV.COL_WARRANTY_MASTER_ID), Byte()))
                    If Me.State.IsEditMode = True Then e.Item.Cells(Me.GRID_COL_EDIT).Text = ""
                ElseIf itemType = ListItemType.EditItem Then
                    e.Item.Cells(Me.GRID_COL_WARRANTY_MASTER_ID).Text = GetGuidStringFromByteArray(CType(dvRow(WarrantyMaster.WarrantyMasterSearchDV.COL_WARRANTY_MASTER_ID), Byte()))
                    e.Item.Cells(Me.GRID_COL_EDIT).Text = ""
                    Dim ddl As DropDownList = CType(e.Item.Cells(GRID_COL_RISK_TYPE).FindControl(GRID_CONTROL_RISK_TYPE), DropDownList)
                    If Not ddl Is Nothing Then
                        Try
                            SetFocus(ddl)
                            'Me.BindListControlToDataView(ddl, LookupListNew.GetRiskTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id))
                            Dim listcontext As ListContext = New ListContext()
                            listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                            Dim riskTypeLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("RiskTypeByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                            ddl.Populate(riskTypeLkl, New PopulateOptions() With
                            {
                            .AddBlankItem = True
                            })
                            Me.SetSelectedItem(ddl, Me.State.myBO.RiskTypeId)
                        Catch ex As Exception
                        End Try
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub
#End Region

#Region "Handlers-Buttons"

        Protected Sub moBtnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles moBtnSearch.Click
            Try
                moWarrantyMasterGrid.CurrentPageIndex = Me.NO_PAGE_INDEX
                moWarrantyMasterGrid.DataMember = Nothing
                moWarrantyMasterGrid.SelectedIndex = NO_ITEM_SELECTED_INDEX
                Me.State.searchDV = Nothing
                Me.PopulateGrid()
                SetButtonsState()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Protected Sub moBtnClear_Click(ByVal sender As Object, ByVal e As EventArgs) Handles moBtnClear.Click
            Try
                ClearSearch()
                SetButtonsState()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Protected Sub SaveButton_WRITE_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SaveButton_WRITE.Click
            Try
                Dim ddl As DropDownList = CType(Me.moWarrantyMasterGrid.Items(moWarrantyMasterGrid.EditItemIndex).Cells(GRID_COL_RISK_TYPE).FindControl(GRID_CONTROL_RISK_TYPE), DropDownList)
                Me.PopulateBOProperty(Me.State.myBO, "RiskTypeId", ddl)
                If (Me.State.myBO.IsDirty) Then
                    Me.State.myBO.Save()
                    Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                    Me.State.searchDV = Nothing
                    Me.State.IsEditMode = False
                    Me.ReturnFromEditing()
                Else
                    Me.AddInfoMsg(Message.MSG_RECORD_NOT_SAVED)
                    Me.State.IsEditMode = False
                    Me.ReturnFromEditing()
                End If
            Catch ex As Exception
                Me.PopulateGrid()
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Protected Sub CancelButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CancelButton.Click
            Try
                Me.moWarrantyMasterGrid.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX
                Me.State.IsEditMode = False
                ReturnFromEditing()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub
#End Region

#End Region

#Region "Controlling Logic"

        Private Sub ReturnFromEditing()
            moWarrantyMasterGrid.EditItemIndex = NO_ITEM_SELECTED_INDEX
            SetGridControls(moWarrantyMasterGrid, True)
            Me.State.IsEditMode = False
            Me.PopulateGrid()
            Me.State.PageIndex = moWarrantyMasterGrid.CurrentPageIndex
            SetButtonsState()
        End Sub

        Private Sub SetButtonsState()
            If (Me.State.IsEditMode) Then
                ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, True)
                ControlMgr.SetVisibleControl(Me, CancelButton, True)
                ControlMgr.SetEnableControl(Me, Me.SaveButton_WRITE, True)
                ControlMgr.SetEnableControl(Me, Me.CancelButton, True)
                ControlMgr.SetEnableControl(Me, Me.moBtnClear, False)
                ControlMgr.SetEnableControl(Me, Me.moBtnSearch, False)
                Me.MenuEnabled = False
                If (Me.cboPageSize.Visible) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, False)
                End If
            Else
                ControlMgr.SetVisibleControl(Me, SaveButton_WRITE, False)
                ControlMgr.SetVisibleControl(Me, CancelButton, False)
                ControlMgr.SetEnableControl(Me, Me.moBtnClear, True)
                ControlMgr.SetEnableControl(Me, Me.moBtnSearch, True)
                Me.MenuEnabled = True
                If (Me.cboPageSize.Visible) Then
                    ControlMgr.SetEnableControl(Me, cboPageSize, True)
                End If
            End If
        End Sub

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

        Private Sub PopulateGrid(Optional ByVal oAction As String = POPULATE_ACTION_NONE)
            Dim oDataView As DataView

            Try
                If (Me.State.searchDV Is Nothing) Then
                    Me.State.searchDV = WarrantyMaster.getList(ElitaPlusIdentity.Current.ActiveUser.Companies, _
                                        TheDealerControl.SelectedGuid, Me.tbSearchSKU.Text, tbSearchManufacturer.Text, _
                                        tbSearchModel.Text, ddlWarrantyType.SelectedValue.Trim)
                End If


                If chkOrderByRiskType.Checked Then
                    Me.State.searchDV.Sort = WarrantyMaster.WarrantyMasterSearchDV.COL_RISK_TYPE & ", " & WarrantyMaster.WarrantyMasterSearchDV.COL_SKU_NUMBER
                    Me.State.SortExpression = WarrantyMaster.WarrantyMasterSearchDV.COL_RISK_TYPE
                Else
                    Me.State.searchDV.Sort = WarrantyMaster.WarrantyMasterSearchDV.COL_SKU_NUMBER
                    Me.State.SortExpression = WarrantyMaster.WarrantyMasterSearchDV.COL_SKU_NUMBER
                End If
                
                If (Me.State.IsEditMode) Then
                    Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.myBO.Id, Me.moWarrantyMasterGrid, Me.State.PageIndex, Me.State.IsEditMode)
                End If

                Me.State.PageIndex = Me.moWarrantyMasterGrid.CurrentPageIndex
                Me.moWarrantyMasterGrid.DataSource = Me.State.searchDV
                HighLightSortColumn(moWarrantyMasterGrid, Me.State.SortExpression)
                Me.moWarrantyMasterGrid.DataBind()

                ControlMgr.SetVisibleControl(Me, trPageSize, moWarrantyMasterGrid.Visible)

                Session("recCount") = Me.State.searchDV.Count

                If Me.moWarrantyMasterGrid.Visible Then
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub ClearSearch()
            TheDealerControl.SelectedIndex = 0
            Me.State.moWarrantyMasterId = Guid.Empty
            Me.tbSearchSKU.Text = ""
            Me.tbSearchManufacturer.Text = ""
            Me.tbSearchModel.Text = ""
            Me.ddlWarrantyType.SelectedIndex = -1
            Me.chkOrderByRiskType.Checked = False
        End Sub

#End Region

#Region "State-Management"

        Private Sub SetSession()
            With Me.State
                .DealerId = TheDealerControl.SelectedGuid
                .PageIndex = moWarrantyMasterGrid.CurrentPageIndex
                .PageSize = moWarrantyMasterGrid.PageSize
                .PageSort = Me.State.SortExpression
                .SearchDataView = Me.State.searchDV
            End With
        End Sub

#End Region

        
 
    End Class

End Namespace
