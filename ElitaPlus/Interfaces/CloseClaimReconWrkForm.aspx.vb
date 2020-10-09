Namespace Interfaces

    Partial Class CloseClaimReconWrkForm
        Inherits ElitaPlusSearchPage


#Region "Page State"

        Class MyState
            Public ClaimfileProcessedId As Guid
            Public ActionInProgress As DetailPageCommand = ElitaPlusPage.DetailPageCommand.Nothing_
            Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
            Public selectedPageIndex As Integer = DEFAULT_PAGE_INDEX
            Public sortBy As String
        End Class

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property

        Private Sub SetStateProperties()
            State.ClaimfileProcessedId = CType(CallingParameters, Guid)
        End Sub

#End Region

#Region "Constants"
        Public Const URL As String = "CloseClaimReconWrkForm.aspx"

        Private Const ID_COL As Integer = 1
        Private Const REJECT_REASON_COL As Integer = 2
        Private Const DO_NOT_PROCESS_COL As Integer = 3
        Private Const DEALER_CODE_COL As Integer = 4
        Private Const CERTIFICATE_COL As Integer = 5
        Private Const DATE_CLAIM_CLOSED_COL As Integer = 6
        Private Const AUTHORIZATION_NUMBER_COL As Integer = 7
        Private Const SERVICE_CENTER_CODE_COL As Integer = 8
        Private Const SERIAL_NUMBER_COL As Integer = 9
        Private Const AMOUNT_COL As Integer = 10
        Private Const STATUS_CODE_COL As Integer = 11
        Private Const MODIFIED_DATE_COL As Integer = 12
        Private otest As ClaimReconWrk
        ' Property Name
        Private Const CLAIM_RECON_WRK_ID_PROPERTY As String = "claimreconwrkid"
        Private Const CLAIMFILE_PROCESSED_ID_PROPERTY As String = "ClaimfileProcessedId"
        Private Const REJECT_REASON_PROPERTY As String = "RejectReason"
        Private Const DEALER_CODE_PROPERTY As String = "DealerCode"
        Private Const CERTIFICATE_PROPERTY As String = "Certificate"
        Private Const AUTHORIZATION_NUMBER_PROPERTY As String = "AuthorizationNumber"
        Private Const SERIAL_NUMBER_PROPERTY As String = "SerialNumber"
        Private Const SERVICE_CENTER_CODE_PROPERTY As String = "ServiceCenterCode"
        Private Const AMOUNT_PROPERTY As String = "Amount"
        Private Const DO_NOT_PROCESS_PROPERTY As String = "DoNotProcess"
        Private Const DATE_CLAIM_CLOSED_PROPERTY As String = "DateClaimClosed"
        Private Const STATUS_CODE_PROPERTY As String = "StatusCode"
        Private Const EMPTY As String = ""
        Private Const DEFAULT_PAGE_INDEX As Integer = 0

        'Actions
        Private Const ACTION_NONE As String = "ACTION_NONE"
        Private Const ACTION_SAVE As String = "ACTION_SAVE"
        Private Const ACTION_NO_EDIT As String = "ACTION_NO_EDIT"
        Private Const ACTION_EDIT As String = "ACTION_EDIT"

#End Region

#Region "Member Variables"

        Protected WithEvents moClaimLoadedTextGrid As System.Web.UI.WebControls.TextBox
        Protected WithEvents moCertificateSalesDateTextGrid As System.Web.UI.WebControls.TextBox
        Protected WithEvents moAuthorizationCreationDateTextGrid As System.Web.UI.WebControls.TextBox
        Protected WithEvents moAuthorizationCodeTextGrid As System.Web.UI.WebControls.TextBox
        Protected WithEvents moProblemDescriptionTextGrid As System.Web.UI.WebControls.TextBox
        Protected WithEvents moProductCodeTextGrid As System.Web.UI.WebControls.TextBox
        Protected WithEvents moAdditionalProductCodeTextGrid As System.Web.UI.WebControls.TextBox
        Protected WithEvents moManufacturerTextGrid As System.Web.UI.WebControls.TextBox
        Protected WithEvents moModelTextGrid As System.Web.UI.WebControls.TextBox
        'Protected WithEvents moDoNotProcessTextGrid As System.Web.UI.WebControls.TextBox
        Protected WithEvents Button1 As System.Web.UI.WebControls.Button
        Protected WithEvents SaveButton_WRITE As System.Web.UI.WebControls.Button
        Protected WithEvents Label1 As System.Web.UI.WebControls.Label
        Private Const ACTION_NEW As String = "ACTION_NEW"
        Protected YesNoDataView As DataView

#End Region

#Region "Handlers"

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Protected WithEvents moErrorController As ErrorController

        Protected WithEvents CancelButton As System.Web.UI.WebControls.Button

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Handlers-Init"

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            moErrorController.Clear_Hide()
            SetStateProperties()
            PopulateYesNoDropDown()
            If Not Page.IsPostBack Then
                SetGridItemStyleColor(moDataGrid)
                ShowMissingTranslations(moErrorController)
                BaseSetButtonsState(False)
                PopulateReadOnly()
                PopulateGrid()

            Else
                CheckIfComingFromSaveConfirm()
            End If
        End Sub
#End Region

#Region "Handlers-Buttons"

        Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
            Try
                If IsDataGPageDirty() Then
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSavePagePromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    Dim str As String = State.ClaimfileProcessedId.ToString()
                    Dim retType As New ClaimFileProcessedController.ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.ClaimfileProcessedId)
                    ReturnToCallingPage(retType)
                End If
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub btnSave_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnSave_WRITE.Click
            Try
                SavePage()
                HiddenIsPageDirty.Value = EMPTY
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try
                PopulateGrid()
                HiddenIsPageDirty.Value = EMPTY
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

#End Region

#Region "Handlers-Grid"

        Private Sub moDataGrid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles moDataGrid.PageIndexChanged
            Try
                State.selectedPageIndex = e.NewPageIndex
                If IsDataGPageDirty() Then
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSavePagePromptResponse)
                Else
                    moDataGrid.CurrentPageIndex = e.NewPageIndex
                    PopulateGrid()
                End If
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub moDataGrid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                If IsDataGPageDirty() Then
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.GridPageSize
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSavePagePromptResponse)
                Else
                    moDataGrid.CurrentPageIndex = NewCurrentPageIndex(moDataGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                    State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                    PopulateGrid()
                End If
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Protected Sub ItemCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs)
            Try
                Dim nIndex As Integer = e.Item.ItemIndex
                If (e.CommandName = SORT_COMMAND_NAME) Then
                    State.sortBy = CType(e.CommandArgument, String)
                    If IsDataGPageDirty() Then
                        State.ActionInProgress = ElitaPlusPage.DetailPageCommand.GridColSort
                        DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSavePagePromptResponse)
                    Else
                        PopulateGrid()
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub moDataGrid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles moDataGrid.ItemDataBound
            Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)
            Dim oDateOfPayText As TextBox
            Dim oExtWarrSaleDateText As TextBox
            Dim oTextBox As TextBox

            If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                '   Display Only
                With e.Item
                    PopulateControlFromBOProperty(.Cells(ID_COL), dvRow(ClaimReconWrk.COL_NAME_CLAIM_RECON_WRK_ID))
                    oTextBox = CType(e.Item.Cells(REJECT_REASON_COL).FindControl("moRejectReasonTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    If e.Item.ItemIndex() = 0 Then
                        SetFocus(oTextBox)
                    End If

                    PopulateControlFromBOProperty(oTextBox, dvRow(ClaimReconWrk.COL_NAME_REJECT_REASON))
                    oTextBox = CType(e.Item.Cells(DEALER_CODE_COL).FindControl("moDealerCodeTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(ClaimReconWrk.COL_NAME_DEALER_CODE))

                    oTextBox = CType(e.Item.Cells(CERTIFICATE_COL).FindControl("moCertificateTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(ClaimReconWrk.COL_NAME_CERTIFICATE))

                    oTextBox = CType(e.Item.Cells(AUTHORIZATION_NUMBER_COL).FindControl("moAuthorizationNumberTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(ClaimReconWrk.COL_NAME_AUTHORIZATION_NUMBER))

                    oTextBox = CType(e.Item.Cells(SERIAL_NUMBER_COL).FindControl("moSerialNumberTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(ClaimReconWrk.COL_NAME_SERIAL_NUMBER))

                    oTextBox = CType(e.Item.Cells(SERVICE_CENTER_CODE_COL).FindControl("moServiceCenterCodeTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(ClaimReconWrk.COL_NAME_SERVICE_CENTER_CODE))

                    oTextBox = CType(e.Item.Cells(AMOUNT_COL).FindControl("moAmountTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(ClaimReconWrk.COL_NAME_AMOUNT))

                    oTextBox = CType(e.Item.Cells(DO_NOT_PROCESS_COL).FindControl("moDoNotProcessTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(ClaimReconWrk.COL_NAME_DO_NOT_PROCESS))
                    ' Dim ddl As DropDownList = CType(e.Item.FindControl("moDoNotProcessTextGrid"), DropDownList)
                    ' ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByValue(oTextBox.Text))
                    'ddl.Attributes.Add("onchange", "setDirty()")

                    oTextBox = CType(e.Item.Cells(DATE_CLAIM_CLOSED_COL).FindControl("moDateClaimClosedTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Dim oDateClaimClosed As ImageButton = CType(e.Item.Cells(DATE_CLAIM_CLOSED_COL).FindControl("ImgDateClaimClosedTextGrid"), ImageButton)
                    If (oDateClaimClosed IsNot Nothing) Then
                        AddCalendar(oDateClaimClosed, oTextBox)
                    End If
                    PopulateControlFromBOProperty(oTextBox, dvRow(ClaimReconWrk.COL_NAME_AUTHORIZATION_CREATION_DATE))

                    PopulateControlFromBOProperty(oTextBox, dvRow(ClaimReconWrk.COL_NAME_DATE_CLAIM_CLOSED))

                    oTextBox = CType(e.Item.Cells(STATUS_CODE_COL).FindControl("moStatusCodeTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(ClaimReconWrk.COL_NAME_STATUS_CODE))
                End With
            End If
            BaseItemBound(sender, e)
        End Sub

        Protected Sub ItemCreated(sender As Object, e As DataGridItemEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Protected Sub BindBoPropertiesToGridHeaders(claimReconWrkInfo As ClaimReconWrk)
            BindBOPropertyToGridHeader(claimReconWrkInfo, "RejectReason", moDataGrid.Columns(REJECT_REASON_COL))
            BindBOPropertyToGridHeader(claimReconWrkInfo, "DealerCode", moDataGrid.Columns(DEALER_CODE_COL))
            BindBOPropertyToGridHeader(claimReconWrkInfo, "Certificate", moDataGrid.Columns(CERTIFICATE_COL))
            BindBOPropertyToGridHeader(claimReconWrkInfo, "AuthorizationNumber", moDataGrid.Columns(AUTHORIZATION_NUMBER_COL))
            BindBOPropertyToGridHeader(claimReconWrkInfo, "SerialNumber", moDataGrid.Columns(SERIAL_NUMBER_COL))
            BindBOPropertyToGridHeader(claimReconWrkInfo, "ServiceCenterCode", moDataGrid.Columns(SERVICE_CENTER_CODE_COL))
            BindBOPropertyToGridHeader(claimReconWrkInfo, "Amount", moDataGrid.Columns(AMOUNT_COL))
            BindBOPropertyToGridHeader(claimReconWrkInfo, "DoNotProcess", moDataGrid.Columns(DO_NOT_PROCESS_COL))
            BindBOPropertyToGridHeader(claimReconWrkInfo, "DateClaimClosed", moDataGrid.Columns(DATE_CLAIM_CLOSED_COL))
            BindBOPropertyToGridHeader(claimReconWrkInfo, "StatusCode", moDataGrid.Columns(STATUS_CODE_COL))

            ClearGridHeadersAndLabelsErrSign()
        End Sub

#End Region

#End Region

#Region "Controlling Logic"

        Protected Sub CheckIfComingFromSaveConfirm()
            Dim confResponse As String = HiddenSavePagePromptResponse.Value

            Try
                If Not confResponse.Equals(EMPTY) Then
                    If confResponse = MSG_VALUE_YES Then
                        SavePage()
                    End If
                    HiddenSavePagePromptResponse.Value = EMPTY
                    HiddenIsPageDirty.Value = EMPTY

                    Select Case State.ActionInProgress
                        Case ElitaPlusPage.DetailPageCommand.Back
                            Dim retType As New ClaimFileProcessedController.ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.ClaimfileProcessedId)
                            ReturnToCallingPage(retType)
                        Case ElitaPlusPage.DetailPageCommand.GridPageSize
                            moDataGrid.CurrentPageIndex = NewCurrentPageIndex(moDataGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                            State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                        Case Else
                            moDataGrid.CurrentPageIndex = State.selectedPageIndex
                    End Select
                    PopulateGrid()
                End If
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Function CreateBoFromGrid(index As Integer) As ClaimReconWrk
            Dim ClaimReconWrkId As Guid
            Dim claimReconWrkInfo As ClaimReconWrk
            Dim sModifiedDate As String

            moDataGrid.SelectedIndex = index
            ClaimReconWrkId = New Guid(moDataGrid.Items(index).Cells(ID_COL).Text)
            sModifiedDate = GetGridText(moDataGrid, index, MODIFIED_DATE_COL)
            claimReconWrkInfo = New ClaimReconWrk(ClaimReconWrkId, sModifiedDate)
            Return claimReconWrkInfo
        End Function

        Private Sub SavePage()
            Dim index As Integer = 0
            Dim claimReconWrkInfo As ClaimReconWrk
            Dim totItems As Integer = moDataGrid.Items.Count

            If totItems > 0 Then
                claimReconWrkInfo = CreateBoFromGrid(0)
                BindBoPropertiesToGridHeaders(claimReconWrkInfo)
                PopulateBOFromForm(claimReconWrkInfo)
                claimReconWrkInfo.Save()
            End If

            totItems = totItems - 1
            For index = 1 To totItems
                claimReconWrkInfo = CreateBoFromGrid(index)
                PopulateBOFromForm(claimReconWrkInfo)
                claimReconWrkInfo.Save()
            Next
        End Sub

        Function IsDataGPageDirty() As Boolean
            Dim Result As String = HiddenIsPageDirty.Value

            Return Result.Equals("YES")
        End Function

#End Region

#Region "Button-Management"

        Public Overrides Sub BaseSetButtonsState(bIsEdit As Boolean)
            SetButtonsState(bIsEdit)
        End Sub

        Private Sub SetButtonsState(bIsEdit As Boolean)
            If (bIsEdit = True) Then
                'SaveButton_WRITE.Visible = True
                'CancelButton.Visible = True
                'btnBack.Visible = False
            Else
                'SaveButton_WRITE.Visible = False
                'CancelButton.Visible = False
                btnBack.Visible = True
                ControlMgr.SetVisibleControl(Me, btnBack, True)
            End If

        End Sub

#End Region

#Region "Populate"

        Private Sub PopulateReadOnly()
            Try
                Dim oClaimFile As ClaimFileProcessed = New ClaimFileProcessed(State.ClaimfileProcessedId)
                With oClaimFile
                    moDealerNameText.Text = .ClaimNameLoad
                    moFileNameText.Text = .Filename
                End With
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Sub PopulateYesNoDropDown()
            Try
                Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId

                YesNoDataView = LookupListNew.GetYesNoLookupList(oLanguageId, False)

            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub PopulateGrid(Optional ByVal oAction As String = ACTION_NONE)

            Dim dv As DataView
            Dim recCount As Integer = 0

            Try
                dv = GetDV()
                dv.Sort = State.sortBy
                recCount = dv.Count
                Session("recCount") = recCount
                TranslateGridControls(moDataGrid)
                moDataGrid.DataSource = dv
                moDataGrid.DataBind()
                lblRecordCount.Text = recCount & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                ControlMgr.DisableAllGridControlsIfNotEditAuth(Me, moDataGrid)
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try

        End Sub

        Private Function GetDV() As DataView

            Dim dv As DataView

            dv = ClaimReconWrk.LoadList(State.ClaimfileProcessedId)
            dv.Sort = moDataGrid.DataMember()

            Return (dv)

        End Function

        Private Sub PopulateBOItem(ClaimReconWrkInfo As ClaimReconWrk, oPropertyName As String, oCellPosition As Integer)
            PopulateBOProperty(ClaimReconWrkInfo, oPropertyName,
                                            CType(GetSelectedGridControl(moDataGrid, oCellPosition), TextBox))
        End Sub

        Private Sub PopulateBOFromForm(claimReconWrkInfo As ClaimReconWrk)
            PopulateBOItem(claimReconWrkInfo, REJECT_REASON_PROPERTY, REJECT_REASON_COL)
            PopulateBOItem(claimReconWrkInfo, DEALER_CODE_PROPERTY, DEALER_CODE_COL)
            PopulateBOItem(claimReconWrkInfo, CERTIFICATE_PROPERTY, CERTIFICATE_COL)
            PopulateBOItem(claimReconWrkInfo, AUTHORIZATION_NUMBER_PROPERTY, AUTHORIZATION_NUMBER_COL)
            PopulateBOItem(claimReconWrkInfo, SERIAL_NUMBER_PROPERTY, SERIAL_NUMBER_COL)
            PopulateBOItem(claimReconWrkInfo, SERVICE_CENTER_CODE_PROPERTY, SERVICE_CENTER_CODE_COL)
            PopulateBOItem(claimReconWrkInfo, AMOUNT_PROPERTY, AMOUNT_COL)
            PopulateBOItem(claimReconWrkInfo, DO_NOT_PROCESS_PROPERTY, DO_NOT_PROCESS_COL)
            PopulateBOItem(claimReconWrkInfo, DATE_CLAIM_CLOSED_PROPERTY, DATE_CLAIM_CLOSED_COL)
            PopulateBOItem(claimReconWrkInfo, STATUS_CODE_PROPERTY, STATUS_CODE_COL)

            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Private Sub PopulateFormItem(oCellPosition As Integer, oPropertyValue As Object)
            PopulateControlFromBOProperty(GetSelectedGridControl(moDataGrid, oCellPosition), oPropertyValue)
        End Sub

#End Region



    End Class

End Namespace
