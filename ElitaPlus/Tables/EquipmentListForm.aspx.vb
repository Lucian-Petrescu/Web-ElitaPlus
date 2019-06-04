Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports System.Threading
Namespace Tables

    Partial Public Class EquipmentListForm
        Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
            Dim strCaller As String = Request.QueryString.Get("CALLER")
            If strCaller = CALLER_CERT_ITEM Then
                Me.MenuEnabled = False
            End If
        End Sub

#End Region

#Region "Page Return Type"
        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public EditingBo As Equipment
            Public HasDataChanged As Boolean
            Public SKU As String
            Public Sub New(ByVal LastOp As DetailPageCommand,
                           ByVal hasDataChanged As Boolean,
                           ByVal equip As Equipment,
                           ByVal strSKU As String)
                Me.LastOperation = LastOp
                Me.EditingBo = equip
                Me.HasDataChanged = hasDataChanged
                Me.SKU = strSKU
            End Sub
        End Class
#End Region

#Region "Constants"
        Private Const GRID_COL_EQUIPMENT_ID_IDX As Integer = 0
        Private Const GRID_COL_DESCRIPTION_IDX As Integer = 1
        Private Const GRID_COL_MODEL_IDX As Integer = 2
        Private Const GRID_COL_MANUFACTURER_IDX As Integer = 3
        Private Const GRID_COL_SKU_IDX As Integer = 4
        Private Const GRID_COL_EQUIPMENT_CLASS_IDX As Integer = 5
        Private Const GRID_COL_EQUIPMENT_TYPE_IDX As Integer = 6
        Private Const GRID_COL_RISK_TYPE_IDX As Integer = 7

        Private Const GRID_COL_COLOR_XCD_IDX As Integer = 8
        Private Const GRID_COL_MEMORY_XCD_IDX As Integer = 9
        Private Const GRID_COL_CARRIER_XCD_IDX As Integer = 10
       
        Private Const GRID_COL_CTRL_EQUIPMENT_DESC As String = "btnEdit"
        Private Const SELECT_ACTION_COMMAND As String = "SelectAction"
        Private Const CALLER_CERT_ITEM As String = "CERT_ITEM"
        Public Const CERT_ITEM_QUERYSTRING As String = "?CALLER=" & CALLER_CERT_ITEM
        Public Const URL As String = "~/tables/equipmentlistform.aspx"
#End Region

#Region "Page State"
        Private IsReturningFromChild As Boolean = False

        Class MyState
            Public SelectedEquipmentId As Guid = Guid.Empty
            Public selectedEquipmentsku As String = String.Empty
            Public ManufacturerName As String = String.Empty
            Public Description As String = String.Empty
            Public Model As String = String.Empty
            Public EquipmentClassName As String = String.Empty
            Public EquipmentTypeName As String = String.Empty
            Public SortExpression As String = Equipment.EquipmentSearchDV.COL_NAME_MODEL
            Public PageIndex As Integer
            Public SearchDV As Equipment.EquipmentSearchDV
            Public HasDataChanged As Boolean
            Public IsGridVisible As Boolean
            Public SelectedPageSize As Integer
            Public Caller_Form As String = String.Empty
            Public ActionInProgress As ElitaPlusPage.DetailPageCommand
            Public selectedRiskTypeId As Guid = Guid.Empty
            Public Equip_List_code As String = String.Empty
            Sub New()
            End Sub
        End Class

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                If Request.QueryString.Get("CALLER") = CALLER_CERT_ITEM Then
                    If Me.NavController.State Is Nothing Then Me.NavController.State = New MyState
                    Return CType(Me.NavController.State, MyState)
                Else
                    Return CType(MyBase.State, MyState)
                End If
            End Get
        End Property

        Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
            Try
                Me.MenuEnabled = True
                Me.IsReturningFromChild = True
                Dim retObj As EquipmentForm.ReturnType = CType(ReturnPar, EquipmentForm.ReturnType)
                Me.State.HasDataChanged = retObj.HasDataChanged
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If Not retObj Is Nothing Then
                            If Not retObj.EditingBo.IsNew Then
                                Me.State.SelectedEquipmentId = retObj.EditingBo.Id
                            End If
                            Me.State.IsGridVisible = True
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        Me.DisplayMessage(Message.DELETE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                End Select
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub SaveGuiState()
            Try
                Me.State.Model = Me.moModelText.Text
                Me.State.Description = Me.moDescriptionText.Text
                Me.State.ManufacturerName = Me.GetSelectedDescription(moManufacturerDrop)
                Me.State.EquipmentClassName = Me.GetSelectedDescription(moEquipmentClassDrop)
                Me.State.EquipmentTypeName = Me.GetSelectedDescription(moEquipmentTypeDrop)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub RestoreGuiState()
            Me.moDescriptionText.Text = Me.State.Model
            Me.moDescriptionText.Text = Me.State.Description
            Me.SetSelectedItemByText(moManufacturerDrop, Me.State.ManufacturerName)
            Me.SetSelectedItemByText(moEquipmentClassDrop, Me.State.EquipmentClassName)
            Me.SetSelectedItemByText(moEquipmentTypeDrop, Me.State.EquipmentTypeName)
        End Sub

        Private Sub UpdateBreadCrum()
            If (Not Me.State Is Nothing) Then
                If (Not Me.State Is Nothing) Then
                    Select Case Me.State.Caller_Form
                        Case CALLER_CERT_ITEM
                            Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("Certificates")
                            Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator &
                                TranslationBase.TranslateLabelOrMessage("Item") & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("CHANGE_EQUIPMENT")
                            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("CHANGE_EQUIPMENT")
                        Case Else
                            Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator &
                                TranslationBase.TranslateLabelOrMessage("EQUIPMENT_SEARCH")
                            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("EQUIPMENT_SEARCH")
                    End Select
                End If
            End If
        End Sub
#End Region

#Region "Page_Events"
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                Me.MasterPage.MessageController.Clear_Hide()
                If Not Me.IsPostBack Then
                    Me.State.Caller_Form = Request.QueryString.Get("CALLER")

                    Me.SetDefaultButton(Me.moModelText, btnSearch)
                    Me.SetDefaultButton(Me.moDescriptionText, btnSearch)
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    PopulateDropDowns()
                    Me.RestoreGuiState()
                    If Not (Me.State.SelectedPageSize = DEFAULT_NEW_UI_PAGE_SIZE) Then
                        Me.State.SelectedPageSize = DEFAULT_NEW_UI_PAGE_SIZE
                        cboPageSize.SelectedValue = CType(Me.State.SelectedPageSize, String)
                        Grid.PageSize = Me.State.SelectedPageSize
                    End If
                    Me.SetGridItemStyleColor(Me.Grid)
                    UpdateBreadCrum()
                    If Me.State.Caller_Form = CALLER_CERT_ITEM Then
                        ShowHideControls(True)
                        'set the equipmetn list code in state
                        Dim Cert As Certificate = CType(Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE), Certificate)
                        If Not Cert Is Nothing Then
                            Me.State.Equip_List_code = Cert.Dealer.EquipmentListCode

                            Dim ManufacturerList As DataElements.ListItem() =
                                CommonConfigManager.Current.ListManager.GetList(listCode:=ListCodes.ManufacturerByEquipment,
                                                                context:=New ListContext() With
                                                                {
                                                                  .EquipmentListCode = Me.State.Equip_List_code
                                                                })

                            Me.moManufacturerDrop.Populate(ManufacturerList.ToArray(),
                                New PopulateOptions() With
                                {
                                    .AddBlankItem = True
                                })

                            'Me.BindListControlToDataView(moManufacturerDrop, LookupListNew.GetManufacturerbyEquipmentList(Me.State.Equip_List_code, DateTime.Now))
                        End If
                    End If
                End If
                Me.ShowMissingTranslations(Me.MasterPage.MessageController)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
        Private Sub ShowHideControls(toggle As Boolean)
            Try
                btnback_WRITE.Visible = toggle
                btnAdd_WRITE.Visible = Not toggle
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
#End Region


#Region "Controlling Logic"

        Private Sub PopulateDropDowns()
            'Me.BindListControlToDataView(moManufacturerDrop, LookupListNew.GetManufacturerLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id))

            Dim ManufacturerList As DataElements.ListItem() =
                                CommonConfigManager.Current.ListManager.GetList(listCode:=ListCodes.ManufacturerByCompanyGroup,
                                                                context:=New ListContext() With
                                                                {
                                                                  .CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                                                                })

            Me.moManufacturerDrop.Populate(ManufacturerList.ToArray(),
                                New PopulateOptions() With
                                {
                                    .AddBlankItem = True
                                })

            Dim EquipmentClass As ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:="EQPCLS",
                                                                languageCode:=Thread.CurrentPrincipal.GetLanguageCode())

            moEquipmentClassDrop.Populate(EquipmentClass.ToArray(),
                                          New PopulateOptions() With
                                            {
                                            .AddBlankItem = True
                                            })

            'Me.BindListControlToDataView(moEquipmentClassDrop, LookupListNew.GetEquipmentClassLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId))

            Dim EquipmentTypes As ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:="EQPTYPE",
                                                                languageCode:=Thread.CurrentPrincipal.GetLanguageCode())

            moEquipmentTypeDrop.Populate(EquipmentTypes,
                                         New PopulateOptions() With
                                            {
                                            .AddBlankItem = True
                                            })

            'Me.BindListControlToDataView(moEquipmentTypeDrop, LookupListNew.GetEquipmentTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId))
        End Sub

        Public Sub PopulateGrid()
            If ((Me.State.SearchDV Is Nothing) OrElse (Me.State.HasDataChanged)) Then
                Me.State.SearchDV = Equipment.GetList(Me.moDescriptionText.Text, Me.moModelText.Text, Me.State.ManufacturerName, Me.State.EquipmentClassName,
                    Me.State.EquipmentTypeName, txtSKU.Text, Me.State.Equip_List_code)
            End If
            Me.State.SearchDV.Sort = Me.State.SortExpression

            Me.Grid.AutoGenerateColumns = False
            Me.Grid.Columns(Me.GRID_COL_DESCRIPTION_IDX).SortExpression = Equipment.EquipmentSearchDV.COL_NAME_DESCRIPTION
            Me.Grid.Columns(Me.GRID_COL_MODEL_IDX).SortExpression = Equipment.EquipmentSearchDV.COL_NAME_MODEL
            Me.Grid.Columns(Me.GRID_COL_SKU_IDX).SortExpression = Equipment.EquipmentSearchDV.COL_NAME_SKU
            Me.Grid.Columns(Me.GRID_COL_MANUFACTURER_IDX).SortExpression = Equipment.EquipmentSearchDV.COL_NAME_MANUFACTURER
            Me.Grid.Columns(Me.GRID_COL_EQUIPMENT_CLASS_IDX).SortExpression = Equipment.EquipmentSearchDV.COL_NAME_EQUIPMENT_CLASS
            Me.Grid.Columns(Me.GRID_COL_EQUIPMENT_TYPE_IDX).SortExpression = Equipment.EquipmentSearchDV.COL_NAME_EQUIPMENT_TYPE

            Me.Grid.Columns(Me.GRID_COL_COLOR_XCD_IDX).SortExpression = Equipment.EquipmentSearchDV.COL_NAME_COLOR_XCD_ID
            Me.Grid.Columns(Me.GRID_COL_MEMORY_XCD_IDX).SortExpression = Equipment.EquipmentSearchDV.COL_NAME_MEMORY_XCD_ID
            Me.Grid.Columns(Me.GRID_COL_CARRIER_XCD_IDX).SortExpression = Equipment.EquipmentSearchDV.COL_NAME_CARRIER_XCD_ID



            SetPageAndSelectedIndexFromGuid(Me.State.SearchDV, Me.State.SelectedEquipmentId, Me.Grid, Me.State.PageIndex)
            Me.SortAndBindGrid()
        End Sub

        Private Sub SortAndBindGrid()
            Me.State.PageIndex = Me.Grid.CurrentPageIndex
            Me.Grid.DataSource = Me.State.SearchDV
            HighLightSortColumn(Grid, Me.State.SortExpression)
            Me.Grid.DataBind()

            ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)

            ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)

            Session("recCount") = Me.State.SearchDV.Count

            If Me.State.SearchDV.Count > 0 Then

                If Me.Grid.Visible Then
                    Me.lblRecordCount.Text = Me.State.SearchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            Else
                If Me.Grid.Visible Then
                    Me.lblRecordCount.Text = Me.State.SearchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
        End Sub
#End Region

#Region " Datagrid Related "

        'The Binding Logic is here  
        Private Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
            Try
                Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)
                Dim btnEditItem As LinkButton
                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    e.Item.Cells(Me.GRID_COL_EQUIPMENT_ID_IDX).Text = New Guid(CType(dvRow(Equipment.EquipmentSearchDV.COL_NAME_EQUIPMENT_ID), Byte())).ToString
                    e.Item.Cells(Me.GRID_COL_MODEL_IDX).Text = dvRow(Equipment.EquipmentSearchDV.COL_NAME_MODEL).ToString
                    'e.Item.Cells(Me.GRID_COL_DESCRIPTION_IDX).Text = dvRow(Equipment.EquipmentSearchDV.COL_NAME_DESCRIPTION).ToString
                    If (Not e.Item.Cells(Me.GRID_COL_DESCRIPTION_IDX).FindControl(GRID_COL_CTRL_EQUIPMENT_DESC) Is Nothing) Then
                        btnEditItem = CType(e.Item.Cells(Me.GRID_COL_DESCRIPTION_IDX).FindControl(GRID_COL_CTRL_EQUIPMENT_DESC), LinkButton)
                        btnEditItem.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(Equipment.EquipmentSearchDV.COL_NAME_EQUIPMENT_ID), Byte()))
                        btnEditItem.CommandName = SELECT_ACTION_COMMAND
                        btnEditItem.Text = dvRow(Equipment.EquipmentSearchDV.COL_NAME_DESCRIPTION).ToString
                    End If
                    e.Item.Cells(Me.GRID_COL_SKU_IDX).Text = dvRow(Equipment.EquipmentSearchDV.COL_NAME_SKU).ToString
                    e.Item.Cells(Me.GRID_COL_MANUFACTURER_IDX).Text = dvRow(Equipment.EquipmentSearchDV.COL_NAME_MANUFACTURER).ToString
                    e.Item.Cells(Me.GRID_COL_EQUIPMENT_TYPE_IDX).Text = dvRow(Equipment.EquipmentSearchDV.COL_NAME_EQUIPMENT_TYPE).ToString
                    e.Item.Cells(Me.GRID_COL_EQUIPMENT_CLASS_IDX).Text = dvRow(Equipment.EquipmentSearchDV.COL_NAME_EQUIPMENT_CLASS).ToString
                    e.Item.Cells(Me.GRID_COL_RISK_TYPE_IDX).Text = dvRow(Equipment.EquipmentSearchDV.COL_NAME_RISK_TYPE_ID).ToString

                    e.Item.Cells(Me.GRID_COL_COLOR_XCD_IDX).Text = dvRow(Equipment.EquipmentSearchDV.COL_NAME_COLOR_XCD_ID).ToString
                    e.Item.Cells(Me.GRID_COL_MEMORY_XCD_IDX).Text = dvRow(Equipment.EquipmentSearchDV.COL_NAME_MEMORY_XCD_ID).ToString
                    e.Item.Cells(Me.GRID_COL_CARRIER_XCD_IDX).Text = dvRow(Equipment.EquipmentSearchDV.COL_NAME_CARRIER_XCD_ID).ToString
                    
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.CurrentPageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles Grid.SortCommand
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

        Public Sub ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
            Try
                If e.CommandName = "SelectAction" Then
                    If Me.State.Caller_Form = CALLER_CERT_ITEM Then
                        Me.State.SelectedEquipmentId = New Guid(e.Item.Cells(Me.GRID_COL_EQUIPMENT_ID_IDX).Text)
                        Me.State.selectedEquipmentsku = e.Item.Cells(Me.GRID_COL_SKU_IDX).Text
                        If Not e.Item.Cells(Me.GRID_COL_RISK_TYPE_IDX).Text Is Nothing AndAlso Not e.Item.Cells(Me.GRID_COL_RISK_TYPE_IDX).Text = "" Then
                            Me.State.selectedRiskTypeId = New Guid(e.Item.Cells(Me.GRID_COL_RISK_TYPE_IDX).Text)
                        End If
                        'navigate back to cert item form
                        Dim retobj As Certificates.CertItemDetailForm.ReturnType = New Certificates.CertItemDetailForm.ReturnType(
                                ElitaPlusPage.DetailPageCommand.Save, Me.State.SelectedEquipmentId,
                                Me.State.selectedEquipmentsku, True, "equipment_selected", Me.State.selectedRiskTypeId)
                        Me.NavController.Navigate(Me, "equipment_selected", retobj)
                    Else
                        Me.State.SelectedEquipmentId = New Guid(e.Item.Cells(Me.GRID_COL_EQUIPMENT_ID_IDX).Text)
                        Me.callPage(EquipmentForm.URL, Me.State.SelectedEquipmentId)
                    End If
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Public Sub ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub Grid_PageIndexChanged(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged
            Try
                Me.State.PageIndex = e.NewPageIndex
                Me.State.SelectedEquipmentId = Guid.Empty
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Button Clicks "

        Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            Try
                Me.State.PageIndex = 0
                Me.State.SelectedEquipmentId = Guid.Empty
                Me.State.IsGridVisible = True
                Me.State.SearchDV = Nothing
                Me.State.HasDataChanged = False
                Me.SaveGuiState()
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub btnAdd_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd_WRITE.Click
            Try
                Me.callPage(EquipmentForm.URL)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub btnClearSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
            Try
                Me.moModelText.Text = ""
                Me.moDescriptionText.Text = ""
                Me.moManufacturerDrop.SelectedIndex = Me.BLANK_ITEM_SELECTED
                Me.moEquipmentClassDrop.SelectedIndex = Me.BLANK_ITEM_SELECTED
                Me.moEquipmentTypeDrop.SelectedIndex = Me.BLANK_ITEM_SELECTED
                Me.txtSKU.Text = ""
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Error Handling"


#End Region

        Private Sub btnback_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnback_WRITE.Click
            Try
                Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, False, Nothing, String.Empty)
                Me.NavController.Navigate(Me, "back", retObj)

            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

    End Class

End Namespace