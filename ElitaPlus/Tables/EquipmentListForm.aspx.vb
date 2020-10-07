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

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
            Dim strCaller As String = Request.QueryString.Get("CALLER")
            If strCaller = CALLER_CERT_ITEM Then
                MenuEnabled = False
            End If
        End Sub

#End Region

#Region "Page Return Type"
        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public EditingBo As Equipment
            Public HasDataChanged As Boolean
            Public SKU As String
            Public Sub New(LastOp As DetailPageCommand,
                           hasDataChanged As Boolean,
                           equip As Equipment,
                           strSKU As String)
                LastOperation = LastOp
                EditingBo = equip
                Me.HasDataChanged = hasDataChanged
                SKU = strSKU
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
                    If NavController.State Is Nothing Then NavController.State = New MyState
                    Return CType(NavController.State, MyState)
                Else
                    Return CType(MyBase.State, MyState)
                End If
            End Get
        End Property

        Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
            Try
                MenuEnabled = True
                IsReturningFromChild = True
                Dim retObj As EquipmentForm.ReturnType = CType(ReturnPar, EquipmentForm.ReturnType)
                State.HasDataChanged = retObj.HasDataChanged
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If retObj IsNot Nothing Then
                            If Not retObj.EditingBo.IsNew Then
                                State.SelectedEquipmentId = retObj.EditingBo.Id
                            End If
                            State.IsGridVisible = True
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        DisplayMessage(Message.DELETE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                End Select
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub SaveGuiState()
            Try
                State.Model = moModelText.Text
                State.Description = moDescriptionText.Text
                State.ManufacturerName = GetSelectedDescription(moManufacturerDrop)
                State.EquipmentClassName = GetSelectedDescription(moEquipmentClassDrop)
                State.EquipmentTypeName = GetSelectedDescription(moEquipmentTypeDrop)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub RestoreGuiState()
            moDescriptionText.Text = State.Model
            moDescriptionText.Text = State.Description
            SetSelectedItemByText(moManufacturerDrop, State.ManufacturerName)
            SetSelectedItemByText(moEquipmentClassDrop, State.EquipmentClassName)
            SetSelectedItemByText(moEquipmentTypeDrop, State.EquipmentTypeName)
        End Sub

        Private Sub UpdateBreadCrum()
            If (State IsNot Nothing) Then
                If (State IsNot Nothing) Then
                    Select Case State.Caller_Form
                        Case CALLER_CERT_ITEM
                            MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("Certificates")
                            MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator &
                                TranslationBase.TranslateLabelOrMessage("Item") & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("CHANGE_EQUIPMENT")
                            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("CHANGE_EQUIPMENT")
                        Case Else
                            MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator &
                                TranslationBase.TranslateLabelOrMessage("EQUIPMENT_SEARCH")
                            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("EQUIPMENT_SEARCH")
                    End Select
                End If
            End If
        End Sub
#End Region

#Region "Page_Events"
        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                MasterPage.MessageController.Clear_Hide()
                If Not IsPostBack Then
                    State.Caller_Form = Request.QueryString.Get("CALLER")

                    SetDefaultButton(moModelText, btnSearch)
                    SetDefaultButton(moDescriptionText, btnSearch)
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    PopulateDropDowns()
                    RestoreGuiState()
                    If Not (State.SelectedPageSize = DEFAULT_NEW_UI_PAGE_SIZE) Then
                        State.SelectedPageSize = DEFAULT_NEW_UI_PAGE_SIZE
                        cboPageSize.SelectedValue = CType(State.SelectedPageSize, String)
                        Grid.PageSize = State.SelectedPageSize
                    End If
                    SetGridItemStyleColor(Grid)
                    UpdateBreadCrum()
                    If State.Caller_Form = CALLER_CERT_ITEM Then
                        ShowHideControls(True)
                        'set the equipmetn list code in state
                        Dim Cert As Certificate = CType(NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE), Certificate)
                        If Cert IsNot Nothing Then
                            State.Equip_List_code = Cert.Dealer.EquipmentListCode

                            Dim ManufacturerList As DataElements.ListItem() =
                                CommonConfigManager.Current.ListManager.GetList(listCode:=ListCodes.ManufacturerByEquipment,
                                                                context:=New ListContext() With
                                                                {
                                                                  .EquipmentListCode = State.Equip_List_code
                                                                })

                            moManufacturerDrop.Populate(ManufacturerList.ToArray(),
                                New PopulateOptions() With
                                {
                                    .AddBlankItem = True
                                })

                            'Me.BindListControlToDataView(moManufacturerDrop, LookupListNew.GetManufacturerbyEquipmentList(Me.State.Equip_List_code, DateTime.Now))
                        End If
                    End If
                End If
                ShowMissingTranslations(MasterPage.MessageController)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Sub ShowHideControls(toggle As Boolean)
            Try
                btnback_WRITE.Visible = toggle
                btnAdd_WRITE.Visible = Not toggle
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
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

            moManufacturerDrop.Populate(ManufacturerList.ToArray(),
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
            If ((State.SearchDV Is Nothing) OrElse (State.HasDataChanged)) Then
                State.SearchDV = Equipment.GetList(moDescriptionText.Text, moModelText.Text, State.ManufacturerName, State.EquipmentClassName,
                    State.EquipmentTypeName, txtSKU.Text, State.Equip_List_code)
            End If
            State.SearchDV.Sort = State.SortExpression

            Grid.AutoGenerateColumns = False
            Grid.Columns(GRID_COL_DESCRIPTION_IDX).SortExpression = Equipment.EquipmentSearchDV.COL_NAME_DESCRIPTION
            Grid.Columns(GRID_COL_MODEL_IDX).SortExpression = Equipment.EquipmentSearchDV.COL_NAME_MODEL
            Grid.Columns(GRID_COL_SKU_IDX).SortExpression = Equipment.EquipmentSearchDV.COL_NAME_SKU
            Grid.Columns(GRID_COL_MANUFACTURER_IDX).SortExpression = Equipment.EquipmentSearchDV.COL_NAME_MANUFACTURER
            Grid.Columns(GRID_COL_EQUIPMENT_CLASS_IDX).SortExpression = Equipment.EquipmentSearchDV.COL_NAME_EQUIPMENT_CLASS
            Grid.Columns(GRID_COL_EQUIPMENT_TYPE_IDX).SortExpression = Equipment.EquipmentSearchDV.COL_NAME_EQUIPMENT_TYPE

            Grid.Columns(GRID_COL_COLOR_XCD_IDX).SortExpression = Equipment.EquipmentSearchDV.COL_NAME_COLOR_XCD_ID
            Grid.Columns(GRID_COL_MEMORY_XCD_IDX).SortExpression = Equipment.EquipmentSearchDV.COL_NAME_MEMORY_XCD_ID
            Grid.Columns(GRID_COL_CARRIER_XCD_IDX).SortExpression = Equipment.EquipmentSearchDV.COL_NAME_CARRIER_XCD_ID



            SetPageAndSelectedIndexFromGuid(State.SearchDV, State.SelectedEquipmentId, Grid, State.PageIndex)
            SortAndBindGrid()
        End Sub

        Private Sub SortAndBindGrid()
            State.PageIndex = Grid.CurrentPageIndex
            Grid.DataSource = State.SearchDV
            HighLightSortColumn(Grid, State.SortExpression)
            Grid.DataBind()

            ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)

            ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

            Session("recCount") = State.SearchDV.Count

            If State.SearchDV.Count > 0 Then

                If Grid.Visible Then
                    lblRecordCount.Text = State.SearchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            Else
                If Grid.Visible Then
                    lblRecordCount.Text = State.SearchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
        End Sub
#End Region

#Region " Datagrid Related "

        'The Binding Logic is here  
        Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
            Try
                Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)
                Dim btnEditItem As LinkButton
                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    e.Item.Cells(GRID_COL_EQUIPMENT_ID_IDX).Text = New Guid(CType(dvRow(Equipment.EquipmentSearchDV.COL_NAME_EQUIPMENT_ID), Byte())).ToString
                    e.Item.Cells(GRID_COL_MODEL_IDX).Text = dvRow(Equipment.EquipmentSearchDV.COL_NAME_MODEL).ToString
                    'e.Item.Cells(Me.GRID_COL_DESCRIPTION_IDX).Text = dvRow(Equipment.EquipmentSearchDV.COL_NAME_DESCRIPTION).ToString
                    If (e.Item.Cells(GRID_COL_DESCRIPTION_IDX).FindControl(GRID_COL_CTRL_EQUIPMENT_DESC) IsNot Nothing) Then
                        btnEditItem = CType(e.Item.Cells(GRID_COL_DESCRIPTION_IDX).FindControl(GRID_COL_CTRL_EQUIPMENT_DESC), LinkButton)
                        btnEditItem.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(Equipment.EquipmentSearchDV.COL_NAME_EQUIPMENT_ID), Byte()))
                        btnEditItem.CommandName = SELECT_ACTION_COMMAND
                        btnEditItem.Text = dvRow(Equipment.EquipmentSearchDV.COL_NAME_DESCRIPTION).ToString
                    End If
                    e.Item.Cells(GRID_COL_SKU_IDX).Text = dvRow(Equipment.EquipmentSearchDV.COL_NAME_SKU).ToString
                    e.Item.Cells(GRID_COL_MANUFACTURER_IDX).Text = dvRow(Equipment.EquipmentSearchDV.COL_NAME_MANUFACTURER).ToString
                    e.Item.Cells(GRID_COL_EQUIPMENT_TYPE_IDX).Text = dvRow(Equipment.EquipmentSearchDV.COL_NAME_EQUIPMENT_TYPE).ToString
                    e.Item.Cells(GRID_COL_EQUIPMENT_CLASS_IDX).Text = dvRow(Equipment.EquipmentSearchDV.COL_NAME_EQUIPMENT_CLASS).ToString
                    e.Item.Cells(GRID_COL_RISK_TYPE_IDX).Text = dvRow(Equipment.EquipmentSearchDV.COL_NAME_RISK_TYPE_ID).ToString

                    e.Item.Cells(GRID_COL_COLOR_XCD_IDX).Text = dvRow(Equipment.EquipmentSearchDV.COL_NAME_COLOR_XCD_ID).ToString
                    e.Item.Cells(GRID_COL_MEMORY_XCD_IDX).Text = dvRow(Equipment.EquipmentSearchDV.COL_NAME_MEMORY_XCD_ID).ToString
                    e.Item.Cells(GRID_COL_CARRIER_XCD_IDX).Text = dvRow(Equipment.EquipmentSearchDV.COL_NAME_CARRIER_XCD_ID).ToString
                    
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.CurrentPageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_SortCommand(source As System.Object, e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles Grid.SortCommand
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
                State.PageIndex = 0
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Sub ItemCommand(source As System.Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs)
            Try
                If e.CommandName = "SelectAction" Then
                    If State.Caller_Form = CALLER_CERT_ITEM Then
                        State.SelectedEquipmentId = New Guid(e.Item.Cells(GRID_COL_EQUIPMENT_ID_IDX).Text)
                        State.selectedEquipmentsku = e.Item.Cells(GRID_COL_SKU_IDX).Text
                        If e.Item.Cells(GRID_COL_RISK_TYPE_IDX).Text IsNot Nothing AndAlso Not e.Item.Cells(GRID_COL_RISK_TYPE_IDX).Text = "" Then
                            State.selectedRiskTypeId = New Guid(e.Item.Cells(GRID_COL_RISK_TYPE_IDX).Text)
                        End If
                        'navigate back to cert item form
                        Dim retobj As Certificates.CertItemDetailForm.ReturnType = New Certificates.CertItemDetailForm.ReturnType(
                                ElitaPlusPage.DetailPageCommand.Save, State.SelectedEquipmentId,
                                State.selectedEquipmentsku, True, "equipment_selected", State.selectedRiskTypeId)
                        NavController.Navigate(Me, "equipment_selected", retobj)
                    Else
                        State.SelectedEquipmentId = New Guid(e.Item.Cells(GRID_COL_EQUIPMENT_ID_IDX).Text)
                        callPage(EquipmentForm.URL, State.SelectedEquipmentId)
                    End If
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Public Sub ItemCreated(sender As System.Object, e As System.Web.UI.WebControls.DataGridItemEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub Grid_PageIndexChanged(source As System.Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged
            Try
                State.PageIndex = e.NewPageIndex
                State.SelectedEquipmentId = Guid.Empty
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Button Clicks "

        Protected Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
            Try
                State.PageIndex = 0
                State.SelectedEquipmentId = Guid.Empty
                State.IsGridVisible = True
                State.SearchDV = Nothing
                State.HasDataChanged = False
                SaveGuiState()
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub btnAdd_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnAdd_WRITE.Click
            Try
                callPage(EquipmentForm.URL)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub btnClearSearch_Click(sender As Object, e As System.EventArgs) Handles btnClearSearch.Click
            Try
                moModelText.Text = ""
                moDescriptionText.Text = ""
                moManufacturerDrop.SelectedIndex = BLANK_ITEM_SELECTED
                moEquipmentClassDrop.SelectedIndex = BLANK_ITEM_SELECTED
                moEquipmentTypeDrop.SelectedIndex = BLANK_ITEM_SELECTED
                txtSKU.Text = ""
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Error Handling"


#End Region

        Private Sub btnback_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnback_WRITE.Click
            Try
                Dim retObj As ReturnType = New ReturnType(ElitaPlusPage.DetailPageCommand.Back, False, Nothing, String.Empty)
                NavController.Navigate(Me, "back", retObj)

            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

    End Class

End Namespace