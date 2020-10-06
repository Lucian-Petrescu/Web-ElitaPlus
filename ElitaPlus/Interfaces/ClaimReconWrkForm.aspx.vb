Namespace Interfaces

    Partial Class ClaimReconWrkForm
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
        Public Const URL As String = "ClaimReconWrkForm.aspx"
        Private Const ID_COL As Integer = 1
        Private Const REJECT_REASON_COL As Integer = 2
        Private Const DO_NOT_PROCESS_COL As Integer = 3
        Private Const DEALER_CODE_COL As Integer = 4
        Private Const CERTIFICATE_COL As Integer = 5
        Private Const CERTIFICATE_SALES_DATE_COL As Integer = 6
        Private Const AUTHORIZATION_NUMBER_COL As Integer = 7
        Private Const AUTHORIZATION_CREATION_DATE_COL As Integer = 8
        Private Const AUTHORIZATION_CODE_COL As Integer = 9
        Private Const PROBLEM_DESCRIPTION_COL As Integer = 10
        Private Const PRODUCT_CODE_COL As Integer = 11
        Private Const ADDITIONAL_PRODUCT_CODE_COL As Integer = 12
        Private Const MANUFACTURER_COL As Integer = 13
        Private Const MODEL_COL As Integer = 14
        Private Const SERIAL_NUMBER_COL As Integer = 15
        Private Const SERVICE_CENTER_CODE_COL As Integer = 16
        Private Const AMOUNT_COL As Integer = 17
        Private Const MODIFIED_DATE_COL As Integer = 18

        ' Property Name
        Private Const CLAIM_RECON_WRK_ID_PROPERTY As String = "claimreconwrkid"
        Private Const CLAIMFILE_PROCESSED_ID_PROPERTY As String = "ClaimfileProcessedId"
        Private Const REJECT_REASON_PROPERTY As String = "RejectReason"
        Private Const CLAIM_LOADED_PROPERTY As String = "ClaimLoaded"
        Private Const DEALER_CODE_PROPERTY As String = "DealerCode"
        Private Const CERTIFICATE_PROPERTY As String = "Certificate"
        Private Const CERTIFICATE_SALES_DATE_PROPERTY As String = "CertificateSalesDate"
        Private Const AUTHORIZATION_NUMBER_PROPERTY As String = "AuthorizationNumber"
        Private Const AUTHORIZATION_CREATION_DATE_PROPERTY As String = "AuthorizationCreationDate"
        Private Const AUTHORIZATION_CODE_PROPERTY As String = "AuthorizationCode"
        Private Const PROBLEM_DESCRIPTION_PROPERTY As String = "ProblemDescription"
        Private Const PRODUCT_CODE_PROPERTY As String = "ProductCode"
        Private Const ADDITIONAL_PRODUCT_CODE_PROPERTY As String = "AdditionalProductCode"
        Private Const MANUFACTURER_PROPERTY As String = "Manufacturer"
        Private Const MODEL_PROPERTY As String = "Model"
        Private Const SERIAL_NUMBER_PROPERTY As String = "SerialNumber"
        Private Const SERVICE_CENTER_CODE_PROPERTY As String = "ServiceCenterCode"
        Private Const AMOUNT_PROPERTY As String = "Amount"
        Private Const DO_NOT_PROCESS_PROPERTY As String = "DoNotProcess"
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
        ' Protected WithEvents moDoNotProcessTextGrid As System.Web.UI.WebControls.TextBox
        Protected WithEvents moDateClaimClosedTextGrid As System.Web.UI.WebControls.TextBox
        Protected WithEvents moStatusCodeTextGrid As System.Web.UI.WebControls.TextBox
        Protected WithEvents Button1 As System.Web.UI.WebControls.Button
        Protected WithEvents SaveButton_WRITE As System.Web.UI.WebControls.Button
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

            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
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

                    oTextBox = CType(e.Item.Cells(CERTIFICATE_SALES_DATE_COL).FindControl("moCertificateSalesDateTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Dim oCertificateSalesDate As ImageButton = CType(e.Item.Cells(CERTIFICATE_SALES_DATE_COL).FindControl("ImgCertificateSalesDate"), ImageButton)
                    If (oCertificateSalesDate IsNot Nothing) Then
                        AddCalendar(oCertificateSalesDate, oTextBox)
                    End If
                    PopulateControlFromBOProperty(oTextBox, dvRow(ClaimReconWrk.COL_NAME_CERTIFICATE_SALES_DATE))

                    oTextBox = CType(e.Item.Cells(AUTHORIZATION_NUMBER_COL).FindControl("moAuthorizationNumberTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(ClaimReconWrk.COL_NAME_AUTHORIZATION_NUMBER))

                    oTextBox = CType(e.Item.Cells(AUTHORIZATION_CREATION_DATE_COL).FindControl("moAuthorizationCreationDateTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Dim oAuthorizationCreationDate As ImageButton = CType(e.Item.Cells(AUTHORIZATION_CREATION_DATE_COL).FindControl("ImgAuthorizationCreationDate"), ImageButton)
                    If (oAuthorizationCreationDate IsNot Nothing) Then
                        AddCalendar(oAuthorizationCreationDate, oTextBox)
                    End If
                    PopulateControlFromBOProperty(oTextBox, dvRow(ClaimReconWrk.COL_NAME_AUTHORIZATION_CREATION_DATE))

                    oTextBox = CType(e.Item.Cells(AUTHORIZATION_CODE_COL).FindControl("moAuthorizationCodeTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(ClaimReconWrk.COL_NAME_AUTHORIZATION_CODE))

                    oTextBox = CType(e.Item.Cells(PROBLEM_DESCRIPTION_COL).FindControl("moProblemDescriptionTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(ClaimReconWrk.COL_NAME_PROBLEM_DESCRIPTION))

                    oTextBox = CType(e.Item.Cells(PRODUCT_CODE_COL).FindControl("moProductCodeTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(ClaimReconWrk.COL_NAME_PRODUCT_CODE))

                    oTextBox = CType(e.Item.Cells(ADDITIONAL_PRODUCT_CODE_COL).FindControl("moAdditionalProductCodeTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(ClaimReconWrk.COL_NAME_ADDITIONAL_PRODUCT_CODE))

                    oTextBox = CType(e.Item.Cells(MANUFACTURER_COL).FindControl("moManufacturerTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(ClaimReconWrk.COL_NAME_MANUFACTURER))

                    oTextBox = CType(e.Item.Cells(MODEL_COL).FindControl("moModelTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(ClaimReconWrk.COL_NAME_MODEL))

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

                End With
            End If
            BaseItemBound(sender, e)
        End Sub

        Protected Sub ItemCreated(sender As Object, e As DataGridItemEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Protected Sub BindBoPropertiesToGridHeaders(claimReconWrkInfo As ClaimReconWrk)
            BindBOPropertyToGridHeader(claimReconWrkInfo, "RejectReason", moDataGrid.Columns(REJECT_REASON_COL))
            BindBOPropertyToGridHeader(claimReconWrkInfo, "Certificate", moDataGrid.Columns(CERTIFICATE_COL))
            BindBOPropertyToGridHeader(claimReconWrkInfo, "CertificateSalesDate", moDataGrid.Columns(CERTIFICATE_SALES_DATE_COL))
            BindBOPropertyToGridHeader(claimReconWrkInfo, "AuthorizationNumber", moDataGrid.Columns(AUTHORIZATION_NUMBER_COL))
            BindBOPropertyToGridHeader(claimReconWrkInfo, "AuthorizationCreationDate", moDataGrid.Columns(AUTHORIZATION_CREATION_DATE_COL))
            BindBOPropertyToGridHeader(claimReconWrkInfo, "AuthorizationCode", moDataGrid.Columns(AUTHORIZATION_CODE_COL))
            BindBOPropertyToGridHeader(claimReconWrkInfo, "ProblemDescription", moDataGrid.Columns(PROBLEM_DESCRIPTION_COL))
            BindBOPropertyToGridHeader(claimReconWrkInfo, "ProductCode", moDataGrid.Columns(PRODUCT_CODE_COL))
            BindBOPropertyToGridHeader(claimReconWrkInfo, "AdditionalProductCode", moDataGrid.Columns(ADDITIONAL_PRODUCT_CODE_COL))
            BindBOPropertyToGridHeader(claimReconWrkInfo, "Manufacturer", moDataGrid.Columns(MANUFACTURER_COL))
            BindBOPropertyToGridHeader(claimReconWrkInfo, "Model", moDataGrid.Columns(MODEL_COL))
            BindBOPropertyToGridHeader(claimReconWrkInfo, "SerialNumber", moDataGrid.Columns(SERIAL_NUMBER_COL))
            BindBOPropertyToGridHeader(claimReconWrkInfo, "ServiceCenterCode", moDataGrid.Columns(SERVICE_CENTER_CODE_COL))
            BindBOPropertyToGridHeader(claimReconWrkInfo, "Amount", moDataGrid.Columns(AMOUNT_COL))
            BindBOPropertyToGridHeader(claimReconWrkInfo, "DoNotProcess", moDataGrid.Columns(DO_NOT_PROCESS_COL))
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
            If (bIsEdit = False) Then
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
            PopulateBOItem(claimReconWrkInfo, CERTIFICATE_SALES_DATE_PROPERTY, CERTIFICATE_SALES_DATE_COL)
            PopulateBOItem(claimReconWrkInfo, AUTHORIZATION_NUMBER_PROPERTY, AUTHORIZATION_NUMBER_COL)
            PopulateBOItem(claimReconWrkInfo, AUTHORIZATION_CREATION_DATE_PROPERTY, AUTHORIZATION_CREATION_DATE_COL)
            PopulateBOItem(claimReconWrkInfo, AUTHORIZATION_CODE_PROPERTY, AUTHORIZATION_CODE_COL)
            PopulateBOItem(claimReconWrkInfo, PROBLEM_DESCRIPTION_PROPERTY, PROBLEM_DESCRIPTION_COL)
            PopulateBOItem(claimReconWrkInfo, PRODUCT_CODE_PROPERTY, PRODUCT_CODE_COL)
            PopulateBOItem(claimReconWrkInfo, ADDITIONAL_PRODUCT_CODE_PROPERTY, ADDITIONAL_PRODUCT_CODE_COL)
            PopulateBOItem(claimReconWrkInfo, MANUFACTURER_PROPERTY, MANUFACTURER_COL)
            PopulateBOItem(claimReconWrkInfo, MODEL_PROPERTY, MODEL_COL)
            PopulateBOItem(claimReconWrkInfo, SERIAL_NUMBER_PROPERTY, SERIAL_NUMBER_COL)
            PopulateBOItem(claimReconWrkInfo, SERVICE_CENTER_CODE_PROPERTY, SERVICE_CENTER_CODE_COL)
            PopulateBOItem(claimReconWrkInfo, AMOUNT_PROPERTY, AMOUNT_COL)
            PopulateBOItem(claimReconWrkInfo, DO_NOT_PROCESS_PROPERTY, DO_NOT_PROCESS_COL)


            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub


#End Region



    End Class

End Namespace
