Imports Assurant.ElitaPlus.DALObjects
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Interfaces

    Partial Class TeleMrktFileForm
        Inherits ElitaPlusSearchPage


#Region "Page State"

        Class MyState
            Public DealerfileProcessedId As Guid
            Public certNumber As String = String.Empty
            Public campaignNumber As String = String.Empty
            Public TMKLoaded As String = String.Empty
            Public TMKLoadedId As Guid
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
            Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Me.State.DealerfileProcessedId = CType(Me.CallingParameters, Guid)
            Me.State.certNumber = moCertNumberText.Text
            Me.State.campaignNumber = moCampaignNumberText.Text
            Me.State.TMKLoadedId = GetSelectedItem(cboStatus)
            If Not Me.State.TMKLoadedId.Equals(Guid.Empty) Then
                Me.State.TMKLoaded = LookupListNew.GetCodeFromId(LookupListNew.GetTMKStatusLookupList(oLanguageId), Me.State.TMKLoadedId)
            Else
                Me.State.TMKLoaded = String.Empty
            End If
        End Sub

#End Region

#Region "Constants"
        Public Const URL As String = "TeleMrktFileForm.aspx"
        Private Const ID_COL As Integer = 1
        Private Const REJECT_REASON_COL As Integer = 2
        Private Const DEALERCODE_COL As Integer = 3
        Private Const CERTIFICATE_COL As Integer = 4
        Private Const FIRSTNAME_COL As Integer = 5
        Private Const LASTNAME_COL As Integer = 6
        Private Const SALESDATE_COL As Integer = 7
        Private Const CAMPAIGN_NUMBER_COL As Integer = 8
        Private Const TMK_LOADED_COL As Integer = 9
        Private Const MODIFIED_DATE_COL As Integer = 10

        ' Property Name
        Private Const RECORD_TYPE_PROPERTY As String = "RecordType"
        Private Const REJECT_CODE_PROPERTY As String = "RejectCode"
        Private Const REJECT_REASON_PROPERTY As String = "RejectReason"
        Private Const TMK_LOADED_PROPERTY As String = "TMKLoaded"
        Private Const CERTIFICATE_PROPERTY As String = "Certificate"
        Private Const DEALERCODE_PROPERTY As String = "DealerCode"
        Private Const FIRSTNAME_PROPERTY As String = "FirstName"
        Private Const LASTNAME_PROPERTY As String = "LastName"
        Private Const SALESDATE_PROPERTY As String = "SalesDate"
        Private Const CAMPAIGN_NUMBER_PROPERTY As String = "CampaignNumber"
        Private Const LAYOUT_PROPERTY As String = "Layout"

        Private Const EMPTY As String = ""
        Private Const DEFAULT_PAGE_INDEX As Integer = 0

        'Actions
        Private Const ACTION_NONE As String = "ACTION_NONE"
        Private Const ACTION_SAVE As String = "ACTION_SAVE"
        Private Const ACTION_NO_EDIT As String = "ACTION_NO_EDIT"
        Private Const ACTION_EDIT As String = "ACTION_EDIT"
        Private Const ACTION_NEW As String = "ACTION_NEW"

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
            If Not Page.IsPostBack Then
                Me.SetGridItemStyleColor(moDataGrid)
                Me.ShowMissingTranslations(moErrorController)
                BaseSetButtonsState(False)
                PopulateReadOnly()
                PopulateStatus()
                PopulateGrid()
                Me.TranslateGridHeader(moDataGrid)
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
                    Dim str As String = Me.State.DealerfileProcessedId.ToString()
                    Dim retType As New TeleMrktInterfaceForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.DealerfileProcessedId)
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

        Private Sub moDataGrid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles moDataGrid.PageIndexChanging
            Try
                Me.State.selectedPageIndex = e.NewPageIndex
                If IsDataGPageDirty() Then
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSavePagePromptResponse)
                Else
                    Me.moDataGrid.PageIndex = e.NewPageIndex
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
                    moDataGrid.PageIndex = NewCurrentPageIndex(moDataGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                    Me.State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                    Me.PopulateGrid()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try
        End Sub

        Protected Sub ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
            Try
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

        Private Sub moDataGrid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles moDataGrid.RowDataBound
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim oDateOfPayText As TextBox
            Dim oExtWarrSaleDateText As TextBox
            Dim oTextBox As TextBox

            If (itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem) AndAlso e.Row.RowIndex <> -1 Then
                oTextBox = CType(e.Row.FindControl("moRejectReasonTextGrid"), TextBox)
                oTextBox.Attributes.Add("onchange", "setDirty()")
                ControlMgr.SetEnableControl(Me, oTextBox, False)
                Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerTmkReconWrkDAL.COL_NAME_REJECT_REASON))
                'If e.Row.RowIndex() = 0 Then
                'SetFocus(oTextBox)
                'End If

                oTextBox = CType(e.Row.FindControl("moDealerCodeTextGrid"), TextBox)
                oTextBox.Attributes.Add("onchange", "setDirty()")
                ControlMgr.SetEnableControl(Me, oTextBox, False)
                Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerTmkReconWrkDAL.COL_NAME_DEALERCODE))

                oTextBox = CType(e.Row.FindControl("moCertificateTextGrid"), TextBox)
                oTextBox.Attributes.Add("onchange", "setDirty()")
                If dvRow(DealerTmkReconWrkDAL.COL_NAME_REJECT_REASON).ToString = String.Empty Then
                    ControlMgr.SetEnableControl(Me, oTextBox, False)
                Else
                    ControlMgr.SetEnableControl(Me, oTextBox, True)
                End If
                Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerTmkReconWrkDAL.COL_NAME_CERTIFICATE))

                oTextBox = CType(e.Row.FindControl("moFirstNameTextGrid"), TextBox)
                oTextBox.Attributes.Add("onchange", "setDirty()")
                ControlMgr.SetEnableControl(Me, oTextBox, False)
                Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerTmkReconWrkDAL.COL_NAME_FIRSTNAME))

                oTextBox = CType(e.Row.FindControl("moLastNameTextGrid"), TextBox)
                oTextBox.Attributes.Add("onchange", "setDirty()")
                ControlMgr.SetEnableControl(Me, oTextBox, False)
                Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerTmkReconWrkDAL.COL_NAME_LASTNAME))

                oTextBox = CType(e.Row.FindControl("moSalesDateTextGrid"), TextBox)
                oTextBox.Attributes.Add("onchange", "setDirty()")
                ControlMgr.SetEnableControl(Me, oTextBox, False)
                Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerTmkReconWrkDAL.COL_NAME_SALESDATE))

                oTextBox = CType(e.Row.FindControl("moCampaignNumberTextGrid"), TextBox)
                oTextBox.Attributes.Add("onchange", "setDirty()")
                If dvRow(DealerTmkReconWrkDAL.COL_NAME_REJECT_REASON).ToString = String.Empty Then
                    ControlMgr.SetEnableControl(Me, oTextBox, False)
                Else
                    ControlMgr.SetEnableControl(Me, oTextBox, True)
                End If
                Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerTmkReconWrkDAL.COL_NAME_CAMPAIGN_NUMBER))

                oTextBox = CType(e.Row.FindControl("moTMKLoadedTextGrid"), TextBox)
                oTextBox.Attributes.Add("onchange", "setDirty()")
                ControlMgr.SetEnableControl(Me, oTextBox, False)
                Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerTmkReconWrkDAL.COL_NAME_TMK_LOADED_DESC))

            End If
            BaseItemBound(sender, e)
        End Sub

        Protected Overloads Sub ItemCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub
        Protected Sub BindBoPropertiesToGridHeaders(ByVal DealerTmkReconWrkInfo As DealerTmkReconWrk)
            Me.BindBOPropertyToGridHeader(DealerTmkReconWrkInfo, "Certificate", Me.moDataGrid.Columns(Me.CERTIFICATE_COL))
            Me.BindBOPropertyToGridHeader(DealerTmkReconWrkInfo, "CampaignNumber", Me.moDataGrid.Columns(Me.CAMPAIGN_NUMBER_COL))
            Me.ClearGridViewHeadersAndLabelsErrSign()
        End Sub

#End Region

#End Region

#Region "Controlling Logic"

        Private Sub PopulateStatus()
            Try
                Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                ' Me.BindListControlToDataView(cboStatus, LookupListNew.GetTMKStatusLookupList(oLanguageId), , , True) 'TMKSTATUS
                cboStatus.Populate(CommonConfigManager.Current.ListManager.GetList("TMKSTATUS", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                 {
                  .AddBlankItem = True
                 })
                ControlMgr.SetEnableControl(Me, cboStatus, True)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try
        End Sub
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
                            Dim retType As New ClaimFileProcessedController.ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.DealerfileProcessedId)
                            Me.ReturnToCallingPage(retType)
                        Case ElitaPlusPage.DetailPageCommand.GridPageSize
                            Me.moDataGrid.PageIndex = NewCurrentPageIndex(moDataGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                            Me.State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                        Case Else
                            Me.moDataGrid.PageIndex = Me.State.selectedPageIndex
                    End Select
                    PopulateGrid()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try
        End Sub

        Private Function CreateBoFromGrid(ByVal index As Integer) As DealerTmkReconWrk
            Dim dealerTmkReconWrkId As Guid
            Dim dealerTmkReconWrkInfo As DealerTmkReconWrk
            Dim sModifiedDate As String

            moDataGrid.SelectedIndex = index
            dealerTmkReconWrkId = New Guid(CType(moDataGrid.Rows(index).FindControl("moDealerTmkReconWrkIdLabel"), Label).Text)
            sModifiedDate = Me.GetGridText(moDataGrid, index, Me.MODIFIED_DATE_COL)
            dealerTmkReconWrkInfo = New DealerTmkReconWrk(dealerTmkReconWrkId, sModifiedDate)
            Return dealerTmkReconWrkInfo
        End Function

        Private Sub SavePage()
            Dim index As Integer = 0
            Dim dealerTmkReconWrkInfo As DealerTmkReconWrk
            Dim totItems As Integer = Me.moDataGrid.Rows.Count

            If totItems > 0 Then
                dealerTmkReconWrkInfo = CreateBoFromGrid(0)
                BindBoPropertiesToGridHeaders(dealerTmkReconWrkInfo)
                PopulateBOFromForm(dealerTmkReconWrkInfo)
                dealerTmkReconWrkInfo.Save()
            End If

            totItems = totItems - 1
            For index = 1 To totItems
                dealerTmkReconWrkInfo = CreateBoFromGrid(index)
                PopulateBOFromForm(dealerTmkReconWrkInfo)
                dealerTmkReconWrkInfo.Save()
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
            If (bIsEdit = False) Then
                btnBack.Visible = True
                ControlMgr.SetVisibleControl(Me, btnBack, True)
            End If

        End Sub

        Private Sub btnClearSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
            Try
                ClearSearch()
            Catch ex As Exception
                Me.HandleErrors(ex, moErrorController)
            End Try
        End Sub

        Private Sub ClearSearch()
            moCampaignNumberText.Text = String.Empty
            moCertNumberText.Text = String.Empty
            cboStatus.SelectedIndex = 0
        End Sub

        Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            SetStateProperties()
            PopulateGrid()
        End Sub
#End Region

#Region "Populate"

        Private Sub PopulateReadOnly()
            Try
                Dim oDealerFile As DealerFileProcessed = New DealerFileProcessed(Me.State.DealerfileProcessedId)
                With oDealerFile
                    moDealerNameText.Text = .DealerNameLoad
                    moFileNameText.Text = .Filename
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, moErrorController)
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

            dv = DealerTmkReconWrk.LoadList(State.DealerfileProcessedId, Me.State.certNumber, Me.State.campaignNumber, Me.State.TMKLoaded)
            dv.Sort = moDataGrid.DataMember()

            Return (dv)

        End Function


        Private Sub PopulateBOItem(ByVal dealerTmkReconWrkInfo As DealerTmkReconWrk, ByVal oPropertyName As String, ByVal oCellPosition As Integer)
            Me.PopulateBOProperty(dealerTmkReconWrkInfo, oPropertyName,
                                            CType(Me.GetSelectedGridControl(moDataGrid, oCellPosition), TextBox))
        End Sub

        Private Sub PopulateBOFromForm(ByVal dealerTmkReconWrkInfo As DealerTmkReconWrk)
            PopulateBOItem(dealerTmkReconWrkInfo, CERTIFICATE_PROPERTY, CERTIFICATE_COL)
            PopulateBOItem(dealerTmkReconWrkInfo, CAMPAIGN_NUMBER_PROPERTY, CAMPAIGN_NUMBER_COL)

            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub


#End Region


    End Class

End Namespace
