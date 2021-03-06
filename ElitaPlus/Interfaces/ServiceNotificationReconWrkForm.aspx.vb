﻿Namespace Interfaces

    Partial Class ServiceNotificationReconWrkForm
        Inherits ElitaPlusSearchPage


#Region "Page State"

        Class MyState
            Public PageIndex As Integer = 0
            Public ServiceNotificationfileProcessedId As Guid
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
            Me.State.ServiceNotificationfileProcessedId = CType(Me.CallingParameters, Guid)
        End Sub

#End Region

#Region "Constants"
        Public Const URL As String = "ServiceNotificationReconWrkForm.aspx"

        Private Const ID_COL As Integer = 1
        Private Const REJECT_REASON_COL As Integer = 2
        Private Const SVC_NOTIFICATION_TYPE As Integer = 3
        'Private Const MODIFIED_DATE As Integer = 34
        Private Const DESCRIPTION_COL As Integer = 4
        Private Const PRP_COD_AMT_COL As Integer = 5
        Private Const REJECT_CODE_COL As Integer = 6
        Private Const SVC_NOTIFICATION_NUMBER As Integer = 7
        Private Const ARTICLE_NUMBER As Integer = 8
        'Private Const CLAIM_LOADED As Integer = 10
        'Private Const ENTIRE_RECORD As Integer = 11
        Private Const CUST_ACCT_NUMBER As Integer = 9
        Private Const CUST_NAME_1 As Integer = 10
        Private Const CUST_NAME_2 As Integer = 11
        Private Const CUST_CITY As Integer = 12
        Private Const CUST_POSTAL_CODE As Integer = 13
        Private Const CUST_REGION As Integer = 14
        Private Const CUST_ADDRESS As Integer = 15
        Private Const CUST_PHONE_NUMBER As Integer = 16
        Private Const CUST_FAX_NUMBER As Integer = 17
        Private Const EQUIPMENT As Integer = 18
        Private Const MFG_NAME As Integer = 19
        Private Const MODEL_NUMBER As Integer = 20
        Private Const MFG_PART_NUMBER As Integer = 21
        Private Const SERIAL_NUMBER As Integer = 22
        Private Const SVC_NOTIFICATION_STATUS As Integer = 23
        Private Const SEQ_TASK_NUMBER As Integer = 24
        Private Const SEQ_TASK_DESCRIPTION As Integer = 25
        Private Const CONSECUTIVE_ACTIVITY_NUMBER As Integer = 26
        Private Const ACTIVITY_TEXT As Integer = 27
        Private Const PROBLEM_DESCRIPTION As Integer = 28
        Private Const SITE_COL As Integer = 29
        'Private Const OP_INDICATOR As Integer = 33
        Private Const TRANSACTION_NUMBER As Integer = 30
        Private Const CREATED_ON As Integer = 31
        Private Const CHANGED_ON As Integer = 32

        ' Property Name
        Private Const cNOTIFICATION_RECON_WRK_ID_PROPERTY As String = "SvcNotificationReconWrkId"
        Private Const cNOTIFICATIONFILE_PROCESSED_ID_PROPERTY As String = "NotificationfileProcessedId"
        ''Private Const cREJECT_REASON_PROPERTY As String = "RejectReason"
        ''Private Const cNOTIFICATION_NUMBER_PROPERTY As String = "NotificationNumber"
        Private Const cREQUESTED_END_DATE As String = "RequestedEndDate"
        Private Const cAMOUNT_PROPERTY As String = "PrpCodAmt"
        Private Const cSVC_NOTIFICATION_RECON_WRK_ID As String = "SvcNotificationReconWrkId"
        Private Const cSVC_NOTIFICATION_PROCESSED_ID As String = "SvcNotificationProcessedId"
        Private Const cREJECT_CODE As String = "RejectCode"
        Private Const cREASON As String = "RejectReason"
        Private Const cCLAIM_LOADED As String = "ClaimLoaded"
        Private Const cENTIRE_RECORD As String = "EntireRecord"
        Private Const cSVC_NOTIFICATION_NUMBER As String = "SvcNotificationNumber"
        Private Const cSVC_NOTIFICATION_TYPE As String = "SvcNotificationType"
        Private Const cDESCRIPTION As String = "Description"
        Private Const cCREATED_ON As String = "CreatedOn"
        Private Const cCHANGED_ON As String = "ChangedOn"
        Private Const cCOL_NAME_REQUIRED_START_TIME As String = "RequiredStartTime"
        Private Const cREQUIRED_END_DATE As String = "RequiredEndDate"
        Private Const cREQUESTED_END_TIME As String = "RequestEdendTime"
        Private Const cARTICLE_NUMBER As String = "ArticleNumber"
        Private Const cCUST_ACCT_NUMBER As String = "CustAcctNumber"
        Private Const cCUST_NAME_1 As String = "CustName1"
        Private Const cCUST_NAME_2 As String = "CustName2"
        Private Const cCUST_CITY As String = "CustCity"
        Private Const cCUST_POSTAL_CODE As String = "CustPostalCode"
        Private Const cCUST_REGION As String = "CustRegion"
        Private Const cCUST_ADDRESS As String = "CustAddress"
        Private Const cCUST_PHONE_NUMBER As String = "CustPhoneNumber"
        Private Const cCUST_FAX_NUMBER As String = "CustFaxNumber"
        Private Const cEQUIPMENT As String = "Equipment"
        Private Const cMFG_NAME As String = "MfgName"
        Private Const cMODEL_NUMBER As String = "ModelNumber"
        Private Const cMFG_PART_NUMBER As String = "MfgPartNumber"
        Private Const cSERIAL_NUMBER As String = "SerialNumber"
        Private Const cSVC_NOTIFICATION_STATUS As String = "SvcNotificationStatus"
        Private Const cSEQ_TASK_NUMBER As String = "SeqTaskNumber"
        Private Const cSEQ_TASK_DESCRIPTION As String = "SeqTaskDescription"
        Private Const cCONSECUTIVE_ACTIVITY_NUMBER As String = "ConsecutiveActivityNumber"
        Private Const cACTIVITY_TEXT As String = "ActivityText"
        Private Const cPROBLEM_DESCRIPTION As String = "ProblemDescription"
        Private Const cSITE As String = "Site"
        Private Const cPRP_COD_AMT As String = "PrpCodAmt"
        Private Const cOP_INDICATOR As String = "OpIndicator"
        Private Const cCREATED_DATE As String = "CreatedDate"
        Private Const cMODIFIED_DATE As String = "ModifiedDate"
        Private Const cCREATED_BY As String = "CreatedBy"
        Private Const cMODIFIED_BY As String = "ModifiedBy"
        Private Const cTRANSACTION_NUMBER As String = "TransactionNumber"
        Private Const EMPTY As String = ""
        Private Const DEFAULT_PAGE_INDEX As Integer = 0

        'Actions
        Private Const ACTION_NONE As String = "ACTION_NONE"
        Private Const ACTION_SAVE As String = "ACTION_SAVE"
        Private Const ACTION_NO_EDIT As String = "ACTION_NO_EDIT"
        Private Const ACTION_EDIT As String = "ACTION_EDIT"

#End Region

#Region "Handlers"


#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Protected WithEvents moErrorController As ErrorController

        Protected YesNoDataView As DataView

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
                Me.SortDirection = EMPTY
                Me.SetGridItemStyleColor(moDataGrid)
                Me.ShowMissingTranslations(moErrorController)
                BaseSetButtonsState(False)
                Me.TranslateGridHeader(moDataGrid)
                Me.TranslateGridControls(moDataGrid)
                PopulateReadOnly()
                PopulateGrid()

            Else
                CheckIfComingFromSaveConfirm()
            End If
        End Sub

#End Region
#Region "Properties"


        Private Shadows ReadOnly Property ThePage() As ElitaPlusSearchPage
            Get
                Return CType(MyBase.Page, ElitaPlusSearchPage)
            End Get
        End Property

        Public Property SortDirection() As String
            Get
                Return ViewState("SortDirection").ToString
            End Get
            Set(ByVal value As String)
                ViewState("SortDirection") = value
            End Set
        End Property

#End Region

#Region "Handlers-Buttons"

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try
                If IsDataGPageDirty() Then
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSavePagePromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    Dim str As String = Me.State.ServiceNotificationfileProcessedId.ToString()
                    Dim retType As New SvrNotificationFileProcessedController.ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.ServiceNotificationfileProcessedId)
                    Me.ReturnToCallingPage(retType)
                End If
            Catch ex As Exception
            Catch ex As Threading.ThreadAbortException

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
        Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles moDataGrid.Sorting
            Try
                Dim spaceIndex As Integer = Me.SortDirection.LastIndexOf(" ")
                If spaceIndex > 0 AndAlso Me.SortDirection.Substring(0, spaceIndex).Equals(e.SortExpression) Then
                    If Me.SortDirection.EndsWith(" ASC") Then
                        Me.SortDirection = e.SortExpression + " DESC"
                    Else
                        Me.SortDirection = e.SortExpression + " ASC"
                    End If
                Else
                    Me.SortDirection = e.SortExpression + " ASC"
                End If


                Me.State.PageIndex = 0
                Me.PopulateGrid()
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

        Protected Sub ItemBound(ByVal source As Object, ByVal e As GridViewRowEventArgs) Handles moDataGrid.RowDataBound
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim oDateOfPayText As TextBox
            Dim oExtWarrSaleDateText As TextBox
            Dim oTextBox As TextBox
            Dim oLabel As Label

            If (itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem) AndAlso e.Row.RowIndex <> -1 Then
                '   Display Only
                With e.Row
                    Me.PopulateControlFromBOProperty(.FindControl("moDealerReconWrkIdLabel"), dvRow(SvcNotificationReconWrk.COL_NAME_SVC_NOTIFICATION_RECON_WRK_ID))
                    oLabel = CType(e.Row.Cells(REJECT_REASON_COL).FindControl("moRejectReasonTextGrid"), Label)
                    oLabel.Attributes.Add("onchange", "setDirty()")
                    If e.Row.RowIndex() = 0 Then
                        SetFocus(oLabel)
                    End If
                    Me.PopulateControlFromBOProperty(oLabel, dvRow(SvcNotificationReconWrk.COL_NAME_REJECT_REASON))

                    oTextBox = CType(e.Row.Cells(SVC_NOTIFICATION_TYPE).FindControl("moServiceNotificationTypeTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(SvcNotificationReconWrk.COL_NAME_SVC_NOTIFICATION_TYPE))

                    oTextBox = CType(e.Row.Cells(CREATED_ON).FindControl("moCreatedOnDate"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Dim oReplacementDate As ImageButton = CType(e.Row.Cells(CREATED_ON).FindControl("ImgReplacementDateTextGrid"), ImageButton)
                    If (Not oReplacementDate Is Nothing) Then
                        Me.AddCalendar(oReplacementDate, oTextBox)
                    End If
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(SvcNotificationReconWrk.COL_NAME_CREATED_ON))

                    oTextBox = CType(e.Row.Cells(CHANGED_ON).FindControl("moChangedOnDate"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    'Dim oChangedOnDate As ImageButton = CType(e.Row.Cells(CHANGED_ON).FindControl("ImgChangedOnDateTextGrid"), ImageButton)
                    'If (Not oChangedOnDate Is Nothing) Then
                    '    Me.AddCalendar(oChangedOnDate, oTextBox)
                    'End If
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(SvcNotificationReconWrk.COL_NAME_CHANGED_ON))

                    oTextBox = CType(e.Row.Cells(DESCRIPTION_COL).FindControl("moDescriptionTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(SvcNotificationReconWrk.COL_NAME_DESCRIPTION))

                    oTextBox = CType(e.Row.Cells(PRP_COD_AMT_COL).FindControl("moAmountTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(SvcNotificationReconWrk.COL_NAME_PRP_COD_AMT))

                    oLabel = CType(e.Row.Cells(REJECT_CODE_COL).FindControl("moRejectcodeTextGrid"), Label)
                    oLabel.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oLabel, dvRow(SvcNotificationReconWrk.COL_NAME_REJECT_CODE))

                    oTextBox = CType(e.Row.Cells(SVC_NOTIFICATION_NUMBER).FindControl("moServiceNotificationNumberTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(SvcNotificationReconWrk.COL_NAME_SVC_NOTIFICATION_NUMBER))

                    oTextBox = CType(e.Row.Cells(ARTICLE_NUMBER).FindControl("moArticleNumberTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(SvcNotificationReconWrk.COL_NAME_ARTICLE_NUMBER))

                    'oTextBox = CType(e.Row.Cells(CLAIM_LOADED).FindControl("moClaimLoadedTextGrid"), TextBox)
                    'oTextBox.Attributes.Add("onchange", "setDirty()")
                    'Me.PopulateControlFromBOProperty(oTextBox, dvRow(SvcNotificationReconWrk.COL_NAME_CLAIM_LOADED))

                    'oTextBox = CType(e.Row.Cells(ENTIRE_RECORD).FindControl("moEntireRecordTextGrid"), TextBox)
                    'oTextBox.Attributes.Add("onchange", "setDirty()")
                    'Me.PopulateControlFromBOProperty(oTextBox, dvRow(SvcNotificationReconWrk.COL_NAME_ENTIRE_RECORD))

                    oTextBox = CType(e.Row.Cells(CUST_ACCT_NUMBER).FindControl("moAccountCustomerNumberTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(SvcNotificationReconWrk.COL_NAME_CUST_ACCT_NUMBER))

                    oTextBox = CType(e.Row.Cells(CUST_NAME_1).FindControl("moCustomerName1TextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(SvcNotificationReconWrk.COL_NAME_CUST_NAME_1))

                    oTextBox = CType(e.Row.Cells(CUST_NAME_2).FindControl("moCustomerName2TextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(SvcNotificationReconWrk.COL_NAME_CUST_NAME_2))

                    oTextBox = CType(e.Row.Cells(CUST_CITY).FindControl("moCustomerCityTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(SvcNotificationReconWrk.COL_NAME_CUST_CITY))

                    oTextBox = CType(e.Row.Cells(CUST_POSTAL_CODE).FindControl("moCustomerPostalCode"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(SvcNotificationReconWrk.COL_NAME_CUST_POSTAL_CODE))

                    oTextBox = CType(e.Row.Cells(CUST_REGION).FindControl("moCustomerRegion"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(SvcNotificationReconWrk.COL_NAME_CUST_REGION))

                    oTextBox = CType(e.Row.Cells(CUST_ADDRESS).FindControl("moCustomerAddress"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(SvcNotificationReconWrk.COL_NAME_CUST_ADDRESS))

                    oTextBox = CType(e.Row.Cells(CUST_PHONE_NUMBER).FindControl("moCustomerPhoneNumber"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(SvcNotificationReconWrk.COL_NAME_CUST_PHONE_NUMBER))

                    oTextBox = CType(e.Row.Cells(CUST_FAX_NUMBER).FindControl("moCustomerFaxNumber"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(SvcNotificationReconWrk.COL_NAME_CUST_FAX_NUMBER))

                    oTextBox = CType(e.Row.Cells(EQUIPMENT).FindControl("moEquipment"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(SvcNotificationReconWrk.COL_NAME_EQUIPMENT))

                    oTextBox = CType(e.Row.Cells(MFG_NAME).FindControl("moMfgName"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(SvcNotificationReconWrk.COL_NAME_MFG_NAME))

                    oTextBox = CType(e.Row.Cells(MODEL_NUMBER).FindControl("moModelNumber"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(SvcNotificationReconWrk.COL_NAME_MODEL_NUMBER))

                    oTextBox = CType(e.Row.Cells(MFG_PART_NUMBER).FindControl("moMfgPartNumber"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(SvcNotificationReconWrk.COL_NAME_MFG_PART_NUMBER))

                    oTextBox = CType(e.Row.Cells(SERIAL_NUMBER).FindControl("moSerialNumber"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(SvcNotificationReconWrk.COL_NAME_SERIAL_NUMBER))

                    oTextBox = CType(e.Row.Cells(SVC_NOTIFICATION_STATUS).FindControl("moSvcNotificationStatus"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(SvcNotificationReconWrk.COL_NAME_SVC_NOTIFICATION_STATUS))

                    oTextBox = CType(e.Row.Cells(SEQ_TASK_NUMBER).FindControl("moSeqTaskNumber"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(SvcNotificationReconWrk.COL_NAME_SEQ_TASK_NUMBER))

                    oTextBox = CType(e.Row.Cells(SEQ_TASK_DESCRIPTION).FindControl("moSeqTaskDescription"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(SvcNotificationReconWrk.COL_NAME_SEQ_TASK_DESCRIPTION))

                    oTextBox = CType(e.Row.Cells(CONSECUTIVE_ACTIVITY_NUMBER).FindControl("moConsecutiveActivityNumber"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(SvcNotificationReconWrk.COL_NAME_CONSECUTIVE_ACTIVITY_NUMBER))

                    oTextBox = CType(e.Row.Cells(ACTIVITY_TEXT).FindControl("moActivityText"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(SvcNotificationReconWrk.COL_NAME_ACTIVITY_TEXT))

                    oTextBox = CType(e.Row.Cells(PROBLEM_DESCRIPTION).FindControl("moProblemDescription"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(SvcNotificationReconWrk.COL_NAME_PROBLEM_DESCRIPTION))

                    oTextBox = CType(e.Row.Cells(SITE_COL).FindControl("moSite"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(SvcNotificationReconWrk.COL_NAME_SITE))


                    'oTextBox = CType(e.Row.Cells(OP_INDICATOR).FindControl("moOpIndicator"), TextBox)
                    'oTextBox.Attributes.Add("onchange", "setDirty()")
                    'Me.PopulateControlFromBOProperty(oTextBox, dvRow(SvcNotificationReconWrk.COL_NAME_OP_INDICATOR))

                    oTextBox = CType(e.Row.Cells(TRANSACTION_NUMBER).FindControl("moTransactionNumber"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(SvcNotificationReconWrk.COL_NAME_TRANSACTION_NUMBER))


                End With
            End If
            BaseItemBound(source, e)
        End Sub

        Protected Sub ItemCreated(ByVal source As Object, ByVal e As GridViewRowEventArgs)
            BaseItemCreated(source, e)
        End Sub

        Protected Sub BindBoPropertiesToGridHeaders(ByVal notificationReconWrkInfo As SvcNotificationReconWrk)


            Me.BindBOPropertyToGridHeader(notificationReconWrkInfo, cREASON, Me.moDataGrid.Columns(Me.REJECT_REASON_COL))
            Me.BindBOPropertyToGridHeader(notificationReconWrkInfo, cDESCRIPTION, Me.moDataGrid.Columns(Me.DESCRIPTION_COL))
            Me.BindBOPropertyToGridHeader(notificationReconWrkInfo, cSITE, Me.moDataGrid.Columns(Me.SITE_COL))
            Me.BindBOPropertyToGridHeader(notificationReconWrkInfo, cPRP_COD_AMT, Me.moDataGrid.Columns(Me.PRP_COD_AMT_COL))
            'Me.BindBOPropertyToGridHeader(notificationReconWrkInfo, cMODIFIED_DATE, Me.moDataGrid.Columns(Me.MODIFIED_DATE))
            Me.BindBOPropertyToGridHeader(notificationReconWrkInfo, cREJECT_CODE, Me.moDataGrid.Columns(Me.REJECT_CODE_COL))
            Me.BindBOPropertyToGridHeader(notificationReconWrkInfo, cSVC_NOTIFICATION_NUMBER, Me.moDataGrid.Columns(Me.SVC_NOTIFICATION_NUMBER))
            Me.BindBOPropertyToGridHeader(notificationReconWrkInfo, cARTICLE_NUMBER, Me.moDataGrid.Columns(Me.ARTICLE_NUMBER))
            ' Me.BindBOPropertyToGridHeader(notificationReconWrkInfo, cCLAIM_LOADED, Me.moDataGrid.Columns(Me.CLAIM_LOADED))
            ' Me.BindBOPropertyToGridHeader(notificationReconWrkInfo, cENTIRE_RECORD, Me.moDataGrid.Columns(Me.ENTIRE_RECORD))
            Me.BindBOPropertyToGridHeader(notificationReconWrkInfo, cCUST_ACCT_NUMBER, Me.moDataGrid.Columns(Me.CUST_ACCT_NUMBER))
            Me.BindBOPropertyToGridHeader(notificationReconWrkInfo, cCUST_NAME_1, Me.moDataGrid.Columns(Me.CUST_NAME_1))
            Me.BindBOPropertyToGridHeader(notificationReconWrkInfo, cCUST_NAME_2, Me.moDataGrid.Columns(Me.CUST_NAME_2))
            Me.BindBOPropertyToGridHeader(notificationReconWrkInfo, cCUST_CITY, Me.moDataGrid.Columns(Me.CUST_CITY))
            Me.BindBOPropertyToGridHeader(notificationReconWrkInfo, cCUST_POSTAL_CODE, Me.moDataGrid.Columns(Me.CUST_POSTAL_CODE))
            Me.BindBOPropertyToGridHeader(notificationReconWrkInfo, cCUST_REGION, Me.moDataGrid.Columns(Me.CUST_REGION))
            Me.BindBOPropertyToGridHeader(notificationReconWrkInfo, cCUST_ADDRESS, Me.moDataGrid.Columns(Me.CUST_ADDRESS))
            Me.BindBOPropertyToGridHeader(notificationReconWrkInfo, cCUST_PHONE_NUMBER, Me.moDataGrid.Columns(Me.CUST_PHONE_NUMBER))
            Me.BindBOPropertyToGridHeader(notificationReconWrkInfo, cCUST_FAX_NUMBER, Me.moDataGrid.Columns(Me.CUST_FAX_NUMBER))
            Me.BindBOPropertyToGridHeader(notificationReconWrkInfo, cEQUIPMENT, Me.moDataGrid.Columns(Me.EQUIPMENT))
            Me.BindBOPropertyToGridHeader(notificationReconWrkInfo, cMFG_NAME, Me.moDataGrid.Columns(Me.MFG_NAME))
            Me.BindBOPropertyToGridHeader(notificationReconWrkInfo, cMODEL_NUMBER, Me.moDataGrid.Columns(Me.MODEL_NUMBER))
            Me.BindBOPropertyToGridHeader(notificationReconWrkInfo, cMFG_PART_NUMBER, Me.moDataGrid.Columns(Me.MFG_PART_NUMBER))
            Me.BindBOPropertyToGridHeader(notificationReconWrkInfo, cSERIAL_NUMBER, Me.moDataGrid.Columns(Me.SERIAL_NUMBER))
            Me.BindBOPropertyToGridHeader(notificationReconWrkInfo, cSVC_NOTIFICATION_STATUS, Me.moDataGrid.Columns(Me.SVC_NOTIFICATION_STATUS))
            Me.BindBOPropertyToGridHeader(notificationReconWrkInfo, cSEQ_TASK_NUMBER, Me.moDataGrid.Columns(Me.SEQ_TASK_NUMBER))
            Me.BindBOPropertyToGridHeader(notificationReconWrkInfo, cSEQ_TASK_DESCRIPTION, Me.moDataGrid.Columns(Me.SEQ_TASK_DESCRIPTION))
            Me.BindBOPropertyToGridHeader(notificationReconWrkInfo, cCONSECUTIVE_ACTIVITY_NUMBER, Me.moDataGrid.Columns(Me.CONSECUTIVE_ACTIVITY_NUMBER))
            Me.BindBOPropertyToGridHeader(notificationReconWrkInfo, cACTIVITY_TEXT, Me.moDataGrid.Columns(Me.ACTIVITY_TEXT))
            Me.BindBOPropertyToGridHeader(notificationReconWrkInfo, cPROBLEM_DESCRIPTION, Me.moDataGrid.Columns(Me.PROBLEM_DESCRIPTION))
            'Me.BindBOPropertyToGridHeader(notificationReconWrkInfo, cOP_INDICATOR, Me.moDataGrid.Columns(Me.OP_INDICATOR))
            Me.BindBOPropertyToGridHeader(notificationReconWrkInfo, cTRANSACTION_NUMBER, Me.moDataGrid.Columns(Me.TRANSACTION_NUMBER))
            Me.BindBOPropertyToGridHeader(notificationReconWrkInfo, cSVC_NOTIFICATION_TYPE, Me.moDataGrid.Columns(Me.SVC_NOTIFICATION_TYPE))
            Me.BindBOPropertyToGridHeader(notificationReconWrkInfo, cCREATED_ON, Me.moDataGrid.Columns(Me.CREATED_ON))
            Me.BindBOPropertyToGridHeader(notificationReconWrkInfo, cCHANGED_ON, Me.moDataGrid.Columns(Me.CHANGED_ON))


            Me.ClearGridViewHeadersAndLabelsErrSign()
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
                            Dim retType As New SvrNotificationFileProcessedController.ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.ServiceNotificationfileProcessedId)
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

        Private Function CreateBoFromGrid(ByVal index As Integer) As SvcNotificationReconWrk
            Dim NotificationReconWrkId As Guid
            Dim notificationReconWrkInfo As SvcNotificationReconWrk
            Dim sModifiedDate As String

            moDataGrid.SelectedIndex = index
            NotificationReconWrkId = New Guid(CType(moDataGrid.Rows(index).FindControl("moDealerReconWrkIdLabel"), Label).Text)

            'sModifiedDate = Me.GetGridText(moDataGrid, index, Me.MODIFIED_DATE)
            sModifiedDate = SvcNotificationReconWrk.COL_NAME_MODIFIED_DATE
            notificationReconWrkInfo = New SvcNotificationReconWrk(NotificationReconWrkId, sModifiedDate)
            Return notificationReconWrkInfo
        End Function

        Private Sub SavePage()
            Dim index As Integer = 0
            Dim notificationReconWrkInfo As SvcNotificationReconWrk
            Dim totItems As Integer = Me.moDataGrid.Rows.Count

            If totItems > 0 Then
                notificationReconWrkInfo = CreateBoFromGrid(0)
                BindBoPropertiesToGridHeaders(notificationReconWrkInfo)
                PopulateBOFromForm(notificationReconWrkInfo)
                notificationReconWrkInfo.Save()
            End If

            totItems = totItems - 1
            For index = 1 To totItems
                notificationReconWrkInfo = CreateBoFromGrid(index)
                BindBoPropertiesToGridHeaders(notificationReconWrkInfo)
                PopulateBOFromForm(notificationReconWrkInfo)
                notificationReconWrkInfo.Save()
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
                '   btnBack.Visible = True
                ControlMgr.SetVisibleControl(Me, btnBack, True)
            End If

        End Sub

#End Region

#Region "Populate"

        Private Sub PopulateReadOnly()
            Try
                Dim oNotificationFile As ServiceNotificationFileProcessed = New ServiceNotificationFileProcessed(Me.State.ServiceNotificationfileProcessedId)
                With oNotificationFile
                    moDealerNameText.Text = .COL_NAME_DEALERFILE_PROCESSED_ID
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
                'dv.Sort = Me.State.sortBy
                If Not Me.SortDirection.Equals(EMPTY) Then
                    dv.Sort = Me.SortDirection
                    HighLightSortColumn(moDataGrid, Me.SortDirection)
                End If
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

            dv = SvcNotificationReconWrk.LoadList(State.ServiceNotificationfileProcessedId)
            dv.Sort = moDataGrid.DataMember()

            Return (dv)

        End Function

        Private Sub PopulateBOItem(ByVal NotificationReconWrkInfo As SvcNotificationReconWrk, ByVal oPropertyName As String, ByVal oCellPosition As Integer)
            Me.PopulateBOProperty(NotificationReconWrkInfo, oPropertyName,
                                            CType(Me.GetSelectedGridControl(moDataGrid, oCellPosition), TextBox))
        End Sub

        Private Sub PopulateBOFromForm(ByVal notificationReconWrkInfo As SvcNotificationReconWrk)

            'PopulateBOItem(notificationReconWrkInfo, cREASON, REJECT_REASON_COL)
            PopulateBOItem(notificationReconWrkInfo, cDESCRIPTION, DESCRIPTION_COL)
            PopulateBOItem(notificationReconWrkInfo, cSITE, SITE_COL)
            PopulateBOItem(notificationReconWrkInfo, cAMOUNT_PROPERTY, PRP_COD_AMT_COL)
            'PopulateBOItem(notificationReconWrkInfo, cMODIFIED_DATE, MODIFIED_DATE)
            'PopulateBOItem(notificationReconWrkInfo, cREJECT_CODE, REJECT_CODE_COL)
            'PopulateBOItem(notificationReconWrkInfo, cCLAIM_LOADED, CLAIM_LOADED)
            'PopulateBOItem(notificationReconWrkInfo, cENTIRE_RECORD, ENTIRE_RECORD)
            PopulateBOItem(notificationReconWrkInfo, cSVC_NOTIFICATION_NUMBER, SVC_NOTIFICATION_NUMBER)
            PopulateBOItem(notificationReconWrkInfo, cCREATED_ON, CREATED_ON)
            PopulateBOItem(notificationReconWrkInfo, cCHANGED_ON, CHANGED_ON)
            'PopulateBOItem(notificationReconWrkInfo, cCOL_NAME_REQUIRED_START_TIME, COL_NAME_REQUIRED_START_TIME)
            'PopulateBOItem(notificationReconWrkInfo, cREQUIRED_END_DATE, REQUIRED_END_DATE)
            'PopulateBOItem(notificationReconWrkInfo, cREQUESTED_END_TIME, REQUESTED_END_TIME)
            PopulateBOItem(notificationReconWrkInfo, cARTICLE_NUMBER, ARTICLE_NUMBER)
            'PopulateBOItem(notificationReconWrkInfo, cCLAIM_LOADED, CLAIM_LOADED)
            'PopulateBOItem(notificationReconWrkInfo, cENTIRE_RECORD, ENTIRE_RECORD)
            PopulateBOItem(notificationReconWrkInfo, cCUST_ACCT_NUMBER, CUST_ACCT_NUMBER)
            PopulateBOItem(notificationReconWrkInfo, cCUST_NAME_1, CUST_NAME_1)
            PopulateBOItem(notificationReconWrkInfo, cCUST_NAME_2, CUST_NAME_2)
            PopulateBOItem(notificationReconWrkInfo, cCUST_CITY, CUST_CITY)
            PopulateBOItem(notificationReconWrkInfo, cCUST_POSTAL_CODE, CUST_POSTAL_CODE)
            PopulateBOItem(notificationReconWrkInfo, cCUST_REGION, CUST_REGION)
            PopulateBOItem(notificationReconWrkInfo, cCUST_ADDRESS, CUST_ADDRESS)
            PopulateBOItem(notificationReconWrkInfo, cCUST_PHONE_NUMBER, CUST_PHONE_NUMBER)
            PopulateBOItem(notificationReconWrkInfo, cCUST_FAX_NUMBER, CUST_FAX_NUMBER)
            PopulateBOItem(notificationReconWrkInfo, cEQUIPMENT, EQUIPMENT)
            PopulateBOItem(notificationReconWrkInfo, cMFG_NAME, MFG_NAME)
            PopulateBOItem(notificationReconWrkInfo, cMODEL_NUMBER, MODEL_NUMBER)
            PopulateBOItem(notificationReconWrkInfo, cMFG_PART_NUMBER, MFG_PART_NUMBER)
            PopulateBOItem(notificationReconWrkInfo, cSERIAL_NUMBER, SERIAL_NUMBER)
            PopulateBOItem(notificationReconWrkInfo, cSVC_NOTIFICATION_STATUS, SVC_NOTIFICATION_STATUS)
            PopulateBOItem(notificationReconWrkInfo, cSEQ_TASK_NUMBER, SEQ_TASK_NUMBER)
            PopulateBOItem(notificationReconWrkInfo, cSEQ_TASK_DESCRIPTION, SEQ_TASK_DESCRIPTION)
            PopulateBOItem(notificationReconWrkInfo, cCONSECUTIVE_ACTIVITY_NUMBER, CONSECUTIVE_ACTIVITY_NUMBER)
            PopulateBOItem(notificationReconWrkInfo, cACTIVITY_TEXT, ACTIVITY_TEXT)
            PopulateBOItem(notificationReconWrkInfo, cPROBLEM_DESCRIPTION, PROBLEM_DESCRIPTION)
            'PopulateBOItem(notificationReconWrkInfo, cOP_INDICATOR, OP_INDICATOR)
            'PopulateBOItem(notificationReconWrkInfo, cCREATED_DATE, CREATED_DATE)
            'PopulateBOItem(notificationReconWrkInfo, cMODIFIED_DATE, MODIFIED_DATE)
            'PopulateBOItem(notificationReconWrkInfo, cCREATED_BY, CREATED_BY)
            'PopulateBOItem(notificationReconWrkInfo, cMODIFIED_BY, MODIFIED_BY)
            PopulateBOItem(notificationReconWrkInfo, cTRANSACTION_NUMBER, TRANSACTION_NUMBER)
            PopulateBOItem(notificationReconWrkInfo, cSVC_NOTIFICATION_TYPE, SVC_NOTIFICATION_TYPE)

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