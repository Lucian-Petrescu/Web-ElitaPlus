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
            Me.State.ClaimfileProcessedId = CType(Me.CallingParameters, Guid)
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

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Handlers-Init"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Me.moErrorController.Clear_Hide()
            Me.SetStateProperties()
            PopulateYesNoDropDown()
            If Not Page.IsPostBack Then
                Me.SetGridItemStyleColor(moDataGrid)
                Me.ShowMissingTranslations(moErrorController)
                BaseSetButtonsState(False)
                PopulateReadOnly()
                PopulateGrid()

            Else
                CheckIfComingFromSaveConfirm()
            End If
        End Sub
#End Region

#Region "Handlers-Buttons"

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try
                If IsDataGPageDirty() Then
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSavePagePromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    Dim str As String = Me.State.ClaimfileProcessedId.ToString()
                    Dim retType As New ClaimFileProcessedController.ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.ClaimfileProcessedId)
                    Me.ReturnToCallingPage(retType)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub btnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
            Try
                SavePage()
                Me.HiddenIsPageDirty.Value = EMPTY
                PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try
                PopulateGrid()
                Me.HiddenIsPageDirty.Value = EMPTY
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try
        End Sub

#End Region

#Region "Handlers-Grid"

        Private Sub moDataGrid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles moDataGrid.PageIndexChanged
            Try
                Me.State.selectedPageIndex = e.NewPageIndex
                If IsDataGPageDirty() Then
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSavePagePromptResponse)
                Else
                    Me.moDataGrid.CurrentPageIndex = e.NewPageIndex
                    PopulateGrid()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try
        End Sub

        Private Sub moDataGrid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                If IsDataGPageDirty() Then
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.GridPageSize
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSavePagePromptResponse)
                Else
                    moDataGrid.CurrentPageIndex = NewCurrentPageIndex(moDataGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                    Me.State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                    Me.PopulateGrid()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try
        End Sub

        Protected Sub ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
            Try
                Dim nIndex As Integer = e.Item.ItemIndex
                If (e.CommandName = SORT_COMMAND_NAME) Then
                    Me.State.sortBy = CType(e.CommandArgument, String)
                    If IsDataGPageDirty() Then
                        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.GridColSort
                        DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSavePagePromptResponse)
                    Else
                        PopulateGrid()
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub moDataGrid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles moDataGrid.ItemDataBound
            Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)
            Dim oDateOfPayText As TextBox
            Dim oExtWarrSaleDateText As TextBox
            Dim oTextBox As TextBox

            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                '   Display Only
                With e.Item
                    Me.PopulateControlFromBOProperty(.Cells(Me.ID_COL), dvRow(ClaimReconWrk.COL_NAME_CLAIM_RECON_WRK_ID))
                    oTextBox = CType(e.Item.Cells(REJECT_REASON_COL).FindControl("moRejectReasonTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    If e.Item.ItemIndex() = 0 Then
                        SetFocus(oTextBox)
                    End If

                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(ClaimReconWrk.COL_NAME_REJECT_REASON))
                    oTextBox = CType(e.Item.Cells(DEALER_CODE_COL).FindControl("moDealerCodeTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(ClaimReconWrk.COL_NAME_DEALER_CODE))

                    oTextBox = CType(e.Item.Cells(CERTIFICATE_COL).FindControl("moCertificateTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(ClaimReconWrk.COL_NAME_CERTIFICATE))

                    oTextBox = CType(e.Item.Cells(AUTHORIZATION_NUMBER_COL).FindControl("moAuthorizationNumberTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(ClaimReconWrk.COL_NAME_AUTHORIZATION_NUMBER))

                    oTextBox = CType(e.Item.Cells(SERIAL_NUMBER_COL).FindControl("moSerialNumberTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(ClaimReconWrk.COL_NAME_SERIAL_NUMBER))

                    oTextBox = CType(e.Item.Cells(SERVICE_CENTER_CODE_COL).FindControl("moServiceCenterCodeTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(ClaimReconWrk.COL_NAME_SERVICE_CENTER_CODE))

                    oTextBox = CType(e.Item.Cells(AMOUNT_COL).FindControl("moAmountTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(ClaimReconWrk.COL_NAME_AMOUNT))

                    oTextBox = CType(e.Item.Cells(DO_NOT_PROCESS_COL).FindControl("moDoNotProcessTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(ClaimReconWrk.COL_NAME_DO_NOT_PROCESS))
                    ' Dim ddl As DropDownList = CType(e.Item.FindControl("moDoNotProcessTextGrid"), DropDownList)
                    ' ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByValue(oTextBox.Text))
                    'ddl.Attributes.Add("onchange", "setDirty()")

                    oTextBox = CType(e.Item.Cells(DATE_CLAIM_CLOSED_COL).FindControl("moDateClaimClosedTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Dim oDateClaimClosed As ImageButton = CType(e.Item.Cells(DATE_CLAIM_CLOSED_COL).FindControl("ImgDateClaimClosedTextGrid"), ImageButton)
                    If (Not oDateClaimClosed Is Nothing) Then
                        Me.AddCalendar(oDateClaimClosed, oTextBox)
                    End If
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(ClaimReconWrk.COL_NAME_AUTHORIZATION_CREATION_DATE))

                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(ClaimReconWrk.COL_NAME_DATE_CLAIM_CLOSED))

                    oTextBox = CType(e.Item.Cells(STATUS_CODE_COL).FindControl("moStatusCodeTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(ClaimReconWrk.COL_NAME_STATUS_CODE))
                End With
            End If
            BaseItemBound(sender, e)
        End Sub

        Protected Sub ItemCreated(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Protected Sub BindBoPropertiesToGridHeaders(ByVal claimReconWrkInfo As ClaimReconWrk)
            Me.BindBOPropertyToGridHeader(claimReconWrkInfo, "RejectReason", Me.moDataGrid.Columns(Me.REJECT_REASON_COL))
            Me.BindBOPropertyToGridHeader(claimReconWrkInfo, "DealerCode", Me.moDataGrid.Columns(Me.DEALER_CODE_COL))
            Me.BindBOPropertyToGridHeader(claimReconWrkInfo, "Certificate", Me.moDataGrid.Columns(Me.CERTIFICATE_COL))
            Me.BindBOPropertyToGridHeader(claimReconWrkInfo, "AuthorizationNumber", Me.moDataGrid.Columns(Me.AUTHORIZATION_NUMBER_COL))
            Me.BindBOPropertyToGridHeader(claimReconWrkInfo, "SerialNumber", Me.moDataGrid.Columns(Me.SERIAL_NUMBER_COL))
            Me.BindBOPropertyToGridHeader(claimReconWrkInfo, "ServiceCenterCode", Me.moDataGrid.Columns(Me.SERVICE_CENTER_CODE_COL))
            Me.BindBOPropertyToGridHeader(claimReconWrkInfo, "Amount", Me.moDataGrid.Columns(Me.AMOUNT_COL))
            Me.BindBOPropertyToGridHeader(claimReconWrkInfo, "DoNotProcess", Me.moDataGrid.Columns(Me.DO_NOT_PROCESS_COL))
            Me.BindBOPropertyToGridHeader(claimReconWrkInfo, "DateClaimClosed", Me.moDataGrid.Columns(Me.DATE_CLAIM_CLOSED_COL))
            Me.BindBOPropertyToGridHeader(claimReconWrkInfo, "StatusCode", Me.moDataGrid.Columns(Me.STATUS_CODE_COL))

            Me.ClearGridHeadersAndLabelsErrSign()
        End Sub

#End Region

#End Region

#Region "Controlling Logic"

        Protected Sub CheckIfComingFromSaveConfirm()
            Dim confResponse As String = Me.HiddenSavePagePromptResponse.Value

            Try
                If Not confResponse.Equals(EMPTY) Then
                    If confResponse = Me.MSG_VALUE_YES Then
                        SavePage()
                    End If
                    Me.HiddenSavePagePromptResponse.Value = EMPTY
                    Me.HiddenIsPageDirty.Value = EMPTY

                    Select Case Me.State.ActionInProgress
                        Case ElitaPlusPage.DetailPageCommand.Back
                            Dim retType As New ClaimFileProcessedController.ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.ClaimfileProcessedId)
                            Me.ReturnToCallingPage(retType)
                        Case ElitaPlusPage.DetailPageCommand.GridPageSize
                            Me.moDataGrid.CurrentPageIndex = NewCurrentPageIndex(moDataGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                            Me.State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                        Case Else
                            Me.moDataGrid.CurrentPageIndex = Me.State.selectedPageIndex
                    End Select
                    PopulateGrid()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try
        End Sub

        Private Function CreateBoFromGrid(ByVal index As Integer) As ClaimReconWrk
            Dim ClaimReconWrkId As Guid
            Dim claimReconWrkInfo As ClaimReconWrk
            Dim sModifiedDate As String

            moDataGrid.SelectedIndex = index
            ClaimReconWrkId = New Guid(moDataGrid.Items(index).Cells(Me.ID_COL).Text)
            sModifiedDate = Me.GetGridText(moDataGrid, index, Me.MODIFIED_DATE_COL)
            claimReconWrkInfo = New ClaimReconWrk(ClaimReconWrkId, sModifiedDate)
            Return claimReconWrkInfo
        End Function

        Private Sub SavePage()
            Dim index As Integer = 0
            Dim claimReconWrkInfo As ClaimReconWrk
            Dim totItems As Integer = Me.moDataGrid.Items.Count

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
            Dim Result As String = Me.HiddenIsPageDirty.Value

            Return Result.Equals("YES")
        End Function

#End Region

#Region "Button-Management"

        Public Overrides Sub BaseSetButtonsState(ByVal bIsEdit As Boolean)
            SetButtonsState(bIsEdit)
        End Sub

        Private Sub SetButtonsState(ByVal bIsEdit As Boolean)
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
                Dim oClaimFile As ClaimFileProcessed = New ClaimFileProcessed(Me.State.ClaimfileProcessedId)
                With oClaimFile
                    moDealerNameText.Text = .ClaimNameLoad
                    moFileNameText.Text = .Filename
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, moErrorController)
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
                dv.Sort = Me.State.sortBy
                recCount = dv.Count
                Session("recCount") = recCount
                Me.TranslateGridControls(moDataGrid)
                moDataGrid.DataSource = dv
                moDataGrid.DataBind()
                Me.lblRecordCount.Text = recCount & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                ControlMgr.DisableAllGridControlsIfNotEditAuth(Me, moDataGrid)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try

        End Sub

        Private Function GetDV() As DataView

            Dim dv As DataView

            dv = ClaimReconWrk.LoadList(State.ClaimfileProcessedId)
            dv.Sort = moDataGrid.DataMember()

            Return (dv)

        End Function

        Private Sub PopulateBOItem(ByVal ClaimReconWrkInfo As ClaimReconWrk, ByVal oPropertyName As String, ByVal oCellPosition As Integer)
            Me.PopulateBOProperty(ClaimReconWrkInfo, oPropertyName,
                                            CType(Me.GetSelectedGridControl(moDataGrid, oCellPosition), TextBox))
        End Sub

        Private Sub PopulateBOFromForm(ByVal claimReconWrkInfo As ClaimReconWrk)
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

            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Private Sub PopulateFormItem(ByVal oCellPosition As Integer, ByVal oPropertyValue As Object)
            Me.PopulateControlFromBOProperty(Me.GetSelectedGridControl(moDataGrid, oCellPosition), oPropertyValue)
        End Sub

#End Region



    End Class

End Namespace
