Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Partial Public Class CertAddBundleItemForm
    Inherits ElitaPlusSearchPage

#Region "Constants"
    Public Const URL As String = "~/Certificates/CertAddBundleItemForm.aspx"
    Public Const PAGETITLE As String = "Bundle"
    Public Const PAGETAB As String = "ADD_CERTIFICATE"

    Private Const EDIT_COMMAND As String = "EditRecord"
    Private Const DELETE_COMMAND As String = "DeleteRecord"
    Private Const NO_ROW_SELECTED_INDEX As Integer = -1
    Private Const COL_IDX_MAKE As Integer = 2
    Private Const COL_IDX_MODEL As Integer = 3
    Private Const COL_IDX_SERIAL_NUM As Integer = 4
    Private Const COL_IDX_DESCRIPTION As Integer = 5
    Private Const COL_IDX_PRICE As Integer = 6
    Private Const COL_IDX_PRODUCT_CODE As Integer = 7
    Private Const COL_IDX_MFG_WARRANTY As Integer = 8

    Private Const GRID_CONTROL_NAME_MAKE As String = "ddlMake"
    Private Const GRID_CONTROL_NAME_MODEL As String = "txtModel"
    Private Const GRID_CONTROL_NAME_SERIAL_NUM As String = "txtSerialNum"
    Private Const GRID_CONTROL_NAME_DESCRIPTION As String = "txtDesc"
    Private Const GRID_CONTROL_NAME_PRICE As String = "txtPrice"
    Private Const GRID_CONTROL_NAME_PRODUCT_CODE As String = "txtProdCode"
    Private Const GRID_CONTROL_NAME_MFG_WARRANTY As String = "txtMfgWarranty"

    Public Enum EditAction
        NoAction
        Add
        Edit
        Delete
    End Enum
#End Region

#Region "Page State"
    ' This class keeps the current state for the page.
    Class MyState
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public ControllerBO As CertAddController
        Public EditMode As EditAction = EditAction.NoAction
        Public ItemIdx As Integer = -1
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

#Region "Page events"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CheckIfComingFromSaveConfirm()
        Me.ErrControllerMaster.Clear_Hide()
        Try
            If Not Me.IsPostBack Then
                Me.SetFormTitle(PAGETITLE)
                Me.SetFormTab(PAGETAB)

                PopulateGrid()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
        Me.ShowMissingTranslations(Me.ErrControllerMaster)
    End Sub

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Dim objBO As CertAddController
        Try
            If Not Me.CallingParameters Is Nothing Then
                'Get the id from the parent
                objBO = CType(Me.CallingParameters, CertAddController)
                Me.State.ControllerBO = objBO
                If State.ControllerBO.BundledItems Is Nothing Then
                    State.ControllerBO.InitBundle()
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster, False)
        End Try
    End Sub

    Private Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Dim intHight As Integer = 200 + (10 - State.ControllerBO.GetBundledItemCount) * 10
        If ErrControllerMaster.Visible Then
            intHight = 200 + (10 - State.ControllerBO.GetBundledItemCount) * 10
        Else
            intHight = 1
        End If
        Me.spanFiller.Text = String.Format("<tr><td colspan=""2"" style=""height:{0}px"">&nbsp;</td></tr>", intHight)
    End Sub
#End Region

#Region "Helper functions"
    Protected Sub CheckIfComingFromSaveConfirm()

    End Sub

    Private Sub PopulateGrid()
        Try
            Grid.DataSource = State.ControllerBO.BundledItems
            Grid.DataBind()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
        EnableDisablePageControls()
    End Sub

    Private Sub EnableDisablePageControls()
        If State.EditMode = EditAction.Add OrElse State.EditMode = EditAction.Edit Then
            Me.btnNew.Visible = False
            Me.btnBack.Visible = False
            Me.btnCancel.Visible = True
            Me.btnSave.Visible = True
            SetGridControls(Me.Grid, False)
        Else
            If State.ControllerBO.GetBundledItemCount < CertAddController.MAX_BUNDLED_ITEMS_ALLOWED Then
                Me.btnNew.Visible = True
            End If
            Me.btnBack.Visible = True
            Me.btnCancel.Visible = False
            Me.btnSave.Visible = False
        End If
    End Sub

    Private Sub SetFocusOnEditableFieldInGrid(ByVal grid As DataGrid, ByVal cellPosition As Integer, ByVal controlName As String, ByVal itemIndex As Integer)
        'Set focus on the Description TextBox for the EditItemIndex row
        Dim desc As WebControl = CType(grid.Items(itemIndex).Cells(cellPosition).FindControl(controlName), WebControl)
        SetFocus(desc)
    End Sub
#End Region

#Region "Button click handlers"
    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.ReturnToCallingPage(State.ControllerBO)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub
    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            If State.EditMode = EditAction.Add Then
                State.ControllerBO.DeleteBundledItem(Grid.EditItemIndex + 1)
            End If
            Grid.EditItemIndex = NO_ROW_SELECTED_INDEX
            State.EditMode = EditAction.NoAction
            PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            Dim objItem As CertAddController.BundledItem = New CertAddController.BundledItem
            Dim strErr As String
            State.ControllerBO.AddBundledItem(objItem, strErr)
            Grid.EditItemIndex = State.ControllerBO.GetBundledItemCount - 1
            State.EditMode = EditAction.Add
            PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim strMake As String, strModel As String, strSerialNum As String, strDesc As String, strProdCode As String
        Dim strTemp As String, intMfgWarranty As Integer, dblPrice As Double
        Dim errMsg As Collections.Generic.List(Of String) = New Collections.Generic.List(Of String), hasErr As Boolean = False
        Dim idxItem As Integer = Grid.EditItemIndex

        Dim txtCtl As TextBox, ddlCtl As DropDownList
        ddlCtl = CType(Me.Grid.Items(idxItem).Cells(Me.COL_IDX_MAKE).FindControl(Me.GRID_CONTROL_NAME_MAKE), DropDownList)
        strMake = ddlCtl.SelectedItem.Text
        txtCtl = CType(Me.Grid.Items(idxItem).Cells(Me.COL_IDX_MODEL).FindControl(Me.GRID_CONTROL_NAME_MODEL), TextBox)
        strModel = txtCtl.Text.Trim
        txtCtl = CType(Me.Grid.Items(idxItem).Cells(Me.COL_IDX_SERIAL_NUM).FindControl(Me.GRID_CONTROL_NAME_SERIAL_NUM), TextBox)
        strSerialNum = txtCtl.Text.Trim
        txtCtl = CType(Me.Grid.Items(idxItem).Cells(Me.COL_IDX_DESCRIPTION).FindControl(Me.GRID_CONTROL_NAME_DESCRIPTION), TextBox)
        strDesc = txtCtl.Text.Trim
        txtCtl = CType(Me.Grid.Items(idxItem).Cells(Me.COL_IDX_PRODUCT_CODE).FindControl(Me.GRID_CONTROL_NAME_PRODUCT_CODE), TextBox)
        strProdCode = txtCtl.Text.Trim
        txtCtl = CType(Me.Grid.Items(idxItem).Cells(Me.COL_IDX_PRICE).FindControl(Me.GRID_CONTROL_NAME_PRICE), TextBox)
        strTemp = txtCtl.Text.Trim
        If Not Double.TryParse(strTemp, dblPrice) Then
            hasErr = True
            errMsg.Add(Grid.Columns(Me.COL_IDX_PRICE).HeaderText & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_INVALID_NUMBER))
        End If
        txtCtl = CType(Me.Grid.Items(idxItem).Cells(Me.COL_IDX_MFG_WARRANTY).FindControl(Me.GRID_CONTROL_NAME_MFG_WARRANTY), TextBox)
        strTemp = txtCtl.Text.Trim
        If Not Integer.TryParse(strTemp, intMfgWarranty) Then
            hasErr = True
            errMsg.Add(Grid.Columns(Me.COL_IDX_MFG_WARRANTY).HeaderText & ":" & TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_INVALID_NUMBER))
        End If
        If Not hasErr Then
            Dim objItem As CertAddController.BundledItem = State.ControllerBO.GetBundledItem(idxItem + 1)
            With objItem
                .Make = strMake
                .Model = strModel
                .SerialNumber = strSerialNum
                .Description = strDesc
                .Price = dblPrice
                .MfgWarranty = intMfgWarranty
                .ProductCode = strProdCode
            End With
            Grid.EditItemIndex = NO_ROW_SELECTED_INDEX
            State.EditMode = EditAction.NoAction
            PopulateGrid()
        Else
            ErrControllerMaster.AddErrorAndShow(errMsg.ToArray, False)
        End If
    End Sub
#End Region

#Region "Grid related functions"

    Protected Sub ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Try
            Dim index As Integer = e.Item.ItemIndex
            If (e.CommandName = Me.EDIT_COMMAND) Then
                'Do the Edit here
                'Set the IsEditMode flag to TRUE
                State.EditMode = EditAction.Edit
                Grid.EditItemIndex = index
                Me.PopulateGrid()
                'Set focus on the Description TextBox for the EditItemIndex row
                'Me.SetFocusOnEditableFieldInGrid(Me.Grid, Me.COL_IDX_MAKE, Me.GRID_CONTROL_NAME_MAKE, index)
            ElseIf (e.CommandName = Me.DELETE_COMMAND) Then
                'Do the delete here
                Grid.SelectedIndex = Me.NO_ROW_SELECTED_INDEX
                Grid.EditItemIndex = Me.NO_ITEM_SELECTED_INDEX
                State.EditMode = EditAction.NoAction
                'Delete the item from the list
                State.ControllerBO.DeleteBundledItem(index + 1)
                PopulateGrid()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Protected Sub ItemBound(ByVal source As Object, ByVal e As DataGridItemEventArgs) Handles Grid.ItemDataBound

        Try
            BaseItemBound(source, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Protected Sub ItemCreated(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
        Try
            Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            Dim ctlddl As DropDownList
            If (itemType = ListItemType.EditItem) Then
                Dim objItem As CertAddController.BundledItem = CType(e.Item.DataItem, CertAddController.BundledItem)
                ctlddl = CType(e.Item.Cells(Me.COL_IDX_MAKE).FindControl(Me.GRID_CONTROL_NAME_MAKE), DropDownList)
                'BindListTextToDataView(ctlddl, LookupListNew.GetManufacturerLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id), "DESCRIPTION", "DESCRIPTION", True)

                Dim Manufacturer As DataElements.ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:=ListCodes.ManufacturerByCompanyGroup,
                                                                context:=New ListContext() With
                                                                {
                                                                  .CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                                                                })

                ctlddl.Populate(Manufacturer.ToArray(),
                                New PopulateOptions() With
                                {
                                    .AddBlankItem = True,
                                    .BlankItemValue = "0",
                                    .ValueFunc = AddressOf .GetDescription
                                })

                ctlddl.SelectedValue = objItem.Make
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub
#End Region

End Class