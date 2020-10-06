Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.Common.Localization
Imports Assurant.Common.Ftp
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Reports
Imports Assurant.ElitaPlus.ElitaPlusWebApp.ElitaPlusPage
Imports Assurant.ElitaPlus.Common
Imports System.Data
Imports Microsoft.VisualBasic
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Interfaces

    Public Class DealerReinsuranceReconWrkForm
        Inherits ElitaPlusSearchPage


#Region "Page State"

        Class MyState
            Public DealerfileProcessedId As Guid
            Public PageIndex As Integer = 0
            Public ActionInProgress As DetailPageCommand = ElitaPlusPage.DetailPageCommand.Nothing_
            Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
            Public selectedPageIndex As Integer = DEFAULT_PAGE_INDEX
            Public RecordMode As String
            Public ParentFile As String = "N"
            Public RejRecNotUpdatable As String = "N"
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
            State.DealerfileProcessedId = CType(CallingParameters, Guid)
        End Sub

        Private Sub SetQueryStringParam()
            Try
                If Request.QueryString("RECORDMODE") IsNot Nothing Then
                    State.RecordMode = Request.QueryString("RECORDMODE")
                End If

                If Request.QueryString("REJRECNOTUPDATABLE") IsNot Nothing Then
                    State.RejRecNotUpdatable = Request.QueryString("REJRECNOTUPDATABLE")
                End If
            Catch ex As Exception

            End Try
        End Sub
#End Region


#Region "Properties"

        Public ReadOnly Property IsEditing() As Boolean
            Get
                IsEditing = (moDataGrid.EditIndex > NO_ITEM_SELECTED_INDEX)
            End Get
        End Property

        Private Shadows ReadOnly Property ThePage() As ElitaPlusSearchPage
            Get
                Return CType(MyBase.Page, ElitaPlusSearchPage)
            End Get
        End Property

        Public Property SortDirection() As String
            Get
                Return ViewState("SortDirection").ToString
            End Get
            Set(value As String)
                ViewState("SortDirection") = value
            End Set
        End Property

#End Region
#Region "Constants"
        Public Const URL As String = "DealerReinsuranceReconWrkForm.aspx"

        Private Const ID_COL As Integer = 1
        Private Const RECORD_TYPE_COL As Integer = 2
        Private Const REJECT_REASON_COL As Integer = 3

        Private Const CERTIFICATE_COL As Integer = 4
        Private Const REINSURANCE_REJECT_REASON_COL As Integer = 5
        Private Const MODIFIED_DATE_COL As Integer = 6


        ' Property Name
        Private Const RECORD_TYPE_PROPERTY As String = "RecordType"
        Private Const REJECT_REASON_PROPERTY As String = "RejectReason"

        Private Const CERTIFICATE_PROPERTY As String = "Certificate"

        Private Const REINSURANCE_REJECT_REASON_PROPERTY As String = "ReinsuranceRejectReason"


        Private Const EMPTY As String = ""
        Private Const DEFAULT_PAGE_INDEX As Integer = 0

        'Actions
        Private Const ACTION_NONE As String = "ACTION_NONE"
        Private Const ACTION_SAVE As String = "ACTION_SAVE"
        Private Const ACTION_NO_EDIT As String = "ACTION_NO_EDIT"
        Private Const ACTION_EDIT As String = "ACTION_EDIT"
        Protected WithEvents Button1 As System.Web.UI.WebControls.Button
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

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Handlers-Init"

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            MasterPage.MessageController.Clear_Hide()
            SetStateProperties()
            If Not Page.IsPostBack Then
                MasterPage.MessageController.Clear()
                MasterPage.UsePageTabTitleInBreadCrum = False
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Interfaces")
                UpdateBreadCrum()
                SetQueryStringParam()
                SortDirection = EMPTY
                SetGridItemStyleColor(moDataGrid)
                ShowMissingTranslations(MasterPage.MessageController)
                State.PageIndex = 0
                TranslateGridHeader(moDataGrid)
                TranslateGridControls(moDataGrid)
                BaseSetButtonsState(False)
                PopulateReadOnly()
                PopulateGrid()
                cboPageSize.SelectedValue = State.selectedPageSize.ToString()
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
                    Dim retType As New DealerFileProcessedController_New.ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.DealerfileProcessedId)
                    ReturnToCallingPage(retType)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnSave_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnSave_WRITE.Click
            Try
                SavePage()
                DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                HiddenIsPageDirty.Value = EMPTY
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try
                PopulateGrid()
                HiddenIsPageDirty.Value = EMPTY
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Handlers-Grid"

        Private Sub moDataGrid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles moDataGrid.PageIndexChanging
            Try
                State.selectedPageIndex = e.NewPageIndex
                If IsDataGPageDirty() Then
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSavePagePromptResponse)
                Else
                    moDataGrid.PageIndex = e.NewPageIndex
                    PopulateGrid()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moDataGrid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                If IsDataGPageDirty() Then
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.GridPageSize
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSavePagePromptResponse)
                Else

                    State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                    PopulateGrid()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
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
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles moDataGrid.Sorting
            Try
                Dim spaceIndex As Integer = SortDirection.LastIndexOf(" ")
                If spaceIndex > 0 AndAlso SortDirection.Substring(0, spaceIndex).Equals(e.SortExpression) Then
                    If SortDirection.EndsWith(" ASC") Then
                        SortDirection = e.SortExpression + " DESC"
                    Else
                        SortDirection = e.SortExpression + " ASC"
                    End If
                Else
                    SortDirection = e.SortExpression + " ASC"
                End If


                State.PageIndex = 0
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub
        Private Sub ItemBound(source As Object, e As GridViewRowEventArgs) Handles moDataGrid.RowDataBound
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

            Dim oTextBox As TextBox

            If (itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem) AndAlso e.Row.RowIndex <> -1 Then
                '   Display Only
                With e.Row
                    PopulateControlFromBOProperty(.Cells(ID_COL), dvRow(DealerReinsReconWrkDAL.COL_NAME_DEALER_REINS_RECON_WRK_ID))

                    Dim oDrop As DropDownList = CType(e.Row.FindControl("moRecordTypeDrop"), DropDownList)
                    oDrop.Attributes.Add("onchange", "setDirty()")
                    PopulateRecordTypeDrop(oDrop)
                    PopulateRecordTypeDrop(oDrop)
                    Dim oValue As String = CType(dvRow(DealerReinsReconWrkDAL.COL_NAME_RECORD_TYPE), String)
                    SetSelectedItemByText(oDrop, oValue)

                    oTextBox = CType(e.Row.Cells(REJECT_REASON_COL).FindControl("RejectReasonTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")


                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerReinsReconWrkDAL.COL_NAME_REJECT_REASON))

                    'oTextBox = CType(e.Row.Cells(DEALER_COL).FindControl("moDealerTextGrid"), TextBox)
                    'Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerPmtReconWrkDAL.COL_NAME_DEALER))

                    oTextBox = CType(e.Row.Cells(CERTIFICATE_COL).FindControl("moCertificateTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerReinsReconWrkDAL.COL_NAME_CERTIFICATE))


                    oTextBox = CType(e.Row.Cells(REINSURANCE_REJECT_REASON_COL).FindControl("moReinsuranceRejectReasonGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(DealerReinsReconWrkDAL.COL_NAME_REINSURANCE_REJECT_REASON))

                End With

            End If
            BaseItemBound(source, e)
        End Sub



        Protected Sub ItemCreated(sender As Object, e As GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Protected Sub BindBoPropertiesToGridHeaders(dealerReconWrkInfo As DealerReinsReconWrk)
            BindBOPropertyToGridHeader(dealerReconWrkInfo, RECORD_TYPE_PROPERTY, moDataGrid.Columns(RECORD_TYPE_COL))
            BindBOPropertyToGridHeader(dealerReconWrkInfo, REJECT_REASON_PROPERTY, moDataGrid.Columns(REJECT_REASON_COL))
            BindBOPropertyToGridHeader(dealerReconWrkInfo, CERTIFICATE_PROPERTY, moDataGrid.Columns(CERTIFICATE_COL))
            BindBOPropertyToGridHeader(dealerReconWrkInfo, REINSURANCE_REJECT_REASON_PROPERTY, moDataGrid.Columns(REINSURANCE_REJECT_REASON_COL))

            ClearGridViewHeadersAndLabelsErrSign()
        End Sub

#End Region

#End Region

#Region "Controlling Logic"
        Private Sub UpdateBreadCrum()

            MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("DEALER_REINSURANCE")
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("DEALER_REINSURANCE")

        End Sub
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
                            Dim retType As New DealerFileProcessedController_New.ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.DealerfileProcessedId)
                            ReturnToCallingPage(retType)
                        Case ElitaPlusPage.DetailPageCommand.GridPageSize
                            moDataGrid.PageIndex = NewCurrentPageIndex(moDataGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                            State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                        Case Else
                            moDataGrid.PageIndex = State.selectedPageIndex
                    End Select
                    PopulateGrid()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Function CreateBoFromGrid(index As Integer) As DealerReinsReconWrk
            Dim DealerReconWrkId As Guid
            Dim dealerReconWrkInfo As DealerReinsReconWrk
            Dim sModifiedDate As String

            moDataGrid.SelectedIndex = index
            DealerReconWrkId = New Guid(moDataGrid.Rows(index).Cells(ID_COL).Text)
            sModifiedDate = GetGridText(moDataGrid, index, MODIFIED_DATE_COL)
            dealerReconWrkInfo = New DealerReinsReconWrk(DealerReconWrkId, sModifiedDate)
            Return dealerReconWrkInfo
        End Function

        Private Sub SavePage()
            Dim index As Integer = 0
            Dim dealerReconWrkInfo As DealerReinsReconWrk
            Dim totItems As Integer = moDataGrid.Rows.Count

            If totItems > 0 Then
                dealerReconWrkInfo = CreateBoFromGrid(0)
                BindBoPropertiesToGridHeaders(dealerReconWrkInfo)
                PopulateBOFromForm(dealerReconWrkInfo)
                dealerReconWrkInfo.Save()
            End If

            totItems = totItems - 1
            For index = 1 To totItems
                dealerReconWrkInfo = CreateBoFromGrid(index)
                BindBoPropertiesToGridHeaders(dealerReconWrkInfo)
                PopulateBOFromForm(dealerReconWrkInfo)
                dealerReconWrkInfo.Save()
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

            Else

                btnBack.Visible = True
                ControlMgr.SetVisibleControl(Me, btnBack, True)
            End If

        End Sub

#End Region

#Region "Populate"

        Private Sub PopulateReadOnly()
            Try
                Dim oDealerFile As DealerFileProcessed = New DealerFileProcessed(State.DealerfileProcessedId)
                With oDealerFile
                    If State.ParentFile = "N" Then
                        moDealerNameText.Text = .DealerNameLoad
                    Else
                        moDealerNameText.Text = EMPTY
                    End If

                    moFileNameText.Text = .Filename
                End With
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateGrid(Optional ByVal oAction As String = ACTION_NONE)

            Dim dv As DataView
            Dim recCount As Integer = 0

            Try
                dv = GetDV()

                If Not SortDirection.Equals(EMPTY) Then
                    dv.Sort = SortDirection
                    HighLightSortColumn(moDataGrid, SortDirection)
                End If
                recCount = dv.Count
                Session("recCount") = recCount
                moDataGrid.PageSize = State.selectedPageSize
                moDataGrid.DataSource = dv.ToTable
                moDataGrid.DataBind()
                lblRecordCount.Text = recCount & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)

                If State.RecordMode IsNot Nothing AndAlso State.RecordMode = "REJ" AndAlso State.RejRecNotUpdatable = "Y" Then
                    ControlMgr.DisableAllGridControlsIfNotEditAuth(Me, moDataGrid, True)
                Else
                    ControlMgr.DisableAllGridControlsIfNotEditAuth(Me, moDataGrid)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Function GetDV() As DataView

            Dim dv As DataView

            dv = DealerReinsReconWrk.LoadList(State.DealerfileProcessedId, State.RecordMode)
            dv.Sort = moDataGrid.DataMember()

            Return (dv)

        End Function

        Private Sub PopulateBOItem(dealerReconWrkInfo As DealerReinsReconWrk, oPropertyName As String, oCellPosition As Integer)
            PopulateBOProperty(dealerReconWrkInfo, oPropertyName,
                                            CType(GetSelectedGridControl(moDataGrid, oCellPosition), TextBox))
        End Sub

        Private Sub PopulateBODrop(dealerReconWrkInfo As DealerReinsReconWrk, oPropertyName As String, oCellPosition As Integer)
            PopulateBOProperty(dealerReconWrkInfo, oPropertyName,
                                CType(GetSelectedGridControl(moDataGrid, oCellPosition), DropDownList), False)
        End Sub

        Private Sub PopulateBOFromForm(dealerReconWrkInfo As DealerReinsReconWrk)
            PopulateBODrop(dealerReconWrkInfo, RECORD_TYPE_PROPERTY, RECORD_TYPE_COL)
            PopulateBOItem(dealerReconWrkInfo, REJECT_REASON_PROPERTY, REJECT_REASON_COL)
            PopulateBOItem(dealerReconWrkInfo, CERTIFICATE_PROPERTY, CERTIFICATE_COL)

            PopulateBOItem(dealerReconWrkInfo, REINSURANCE_REJECT_REASON_PROPERTY, REINSURANCE_REJECT_REASON_COL)

            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Private Sub PopulateFormItem(oCellPosition As Integer, oPropertyValue As Object)
            PopulateControlFromBOProperty(GetSelectedGridControl(moDataGrid, oCellPosition), oPropertyValue)
        End Sub

        Sub PopulateRecordTypeDrop(recordTypeDrop As DropDownList, Optional ByVal AddNothingSelected As Boolean = False)
            Try
                Dim oLangId As Guid = Authentication.LangId
                '   Dim recordTypeList As DataView = LookupListNew.GetReinsuranceRecordTypeLookupList(oLangId)
                'Me.BindListControlToDataView(recordTypeDrop, recordTypeList, , , AddNothingSelected)
                recordTypeDrop.Populate(CommonConfigManager.Current.ListManager.GetList("REINSRECTYP", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                 {
                  .AddBlankItem = AddNothingSelected
                 })
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region



    End Class
End Namespace
