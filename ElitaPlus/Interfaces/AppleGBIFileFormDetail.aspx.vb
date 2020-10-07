Imports Assurant.ElitaPlus.DALObjects

Namespace Interfaces
    Public Class AppleGBIFileFormDetail
        Inherits ElitaPlusSearchPage

        Public Class PageParameters
            Public Property Status As String
            Public Property FileProcessedId As Guid
            Public Property Filename As String
            Public Property StartDate As Date
            Public Property EndDate As Date

        End Class
#Region "Constants"
        Public Const URL As String = "AppleGBIFileFormDetail.aspx"
        REM This enum must identical to the columns in the Grid
        Public Enum GridDefenitionEnum
            GbiClaimReconWrkId = 0
            RejectCode
            RejectReason
            CustomerId
            ShipToId
            AgreementId
            UniqueIdentifier
            OriginalSerialNumber
            OriginalImeiNumber
            NewSerialNumber
            NewImeiNumber
            RepairCompletionDate
            ClaimType
            Channel
            IncidentFee
            NotifCreateDate
            RepairCompleted
            RepairCompletedDate
            ClaimCancelled
            Description
            DeviceType
        End Enum

        Public Const STATUS_PROCESSED As String = "P"
        Public Const STATUS_CANCELLED As String = "C"
        Public Const STATUS_PENDING_VALIDATION As String = "V"
        Public Const STATUS_FAILED As String = "F"
        Public Const STATUS_REPROCESS As String = "R"
        Public Const STATUS_PENDING_CLAIM_CREATION As String = "C"

#End Region

#Region "MyState"
        Class MyState
            Public Status As String
            Public Filename As String
            Public FileProcessedId As Guid
            REM ---------
            Public LastOperation As DetailPageCommand = DetailPageCommand.Nothing_
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            REM ---------
            Public PageIndex As Integer
            Public PageSize As Integer = 30
            Public PageSort As String
            Public searchDV As DataView = Nothing
            Public SortExpression As String = $"{AppleGBIFileReconWrk.COL_NAME_DET_AGREEMENT_ID} asc "
            Public bnoRow As Boolean = False
            Public IsGridVisible As Boolean
            Public SelectedPageSize As Integer = DEFAULT_PAGE_SIZE
            Public IsReturningFromChild As Boolean
            REM ---------
            Public StartDate As Date
            Public EndDate As Date
            Public IsFileDataEdited As Boolean = False
        End Class
#End Region

#Region "Page State"
        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property
#End Region

#Region "State-Management"

        Protected Sub ComingFromBack()

            ' Return from the Back Button
            GoBack()

        End Sub

        Private Sub SetSession()
            With State
                .PageIndex = Grid.PageIndex
                .PageSize = Grid.PageSize
                .PageSort = State.SortExpression
                .searchDV = State.searchDV
            End With
        End Sub
#End Region

#Region "Handlers-Buttons-Methods"
        Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
            Try

                GoBack()

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub GoBack()

            Dim retType As New AppleGBIFileForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.FileProcessedId, False)
            ReturnToCallingPage(retType)

        End Sub

        Private Sub SaveButton_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles SaveButton_WRITE.Click
            Try
                SavePage()

                ' Initiate the file process if data is changed
                If State.IsFileDataEdited Then
                    AppleGBIFileReconWrk.ProcessFile(State.FileProcessedId)
                    State.IsFileDataEdited = False
                End If

                ControlMgr.SetEnableControl(Me, SaveButton_WRITE, False)

                DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)

                'Select Case SaveBundles()
                '    Case 1, 2
                '        Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                'End Select
                'Me.HiddenIsPageDirty.Value = Empty
                'PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Protected Sub btnReprocess_Click(sender As Object, e As EventArgs) Handles btnReprocess.Click
            AppleGBIFileReconWrk.ProcessFile(State.FileProcessedId)
            ControlMgr.SetEnableControl(Me, btnReprocess, False)

            DisplayMessage(Message.REPROCESS_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)

        End Sub

        'Private Sub btnUndo_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_WRITE.Click
        '    Try
        '        'Me.State.BundlesHashTable = Nothing
        '        'PopulateGrid()
        '        'Me.HiddenIsPageDirty.Value = Empty
        '    Catch ex As Exception
        '        Me.HandleErrors(ex, Me.MasterPage.MessageController)
        '    End Try
        'End Sub
#End Region

#Region "Page Events"

        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            Try
                MasterPage.MessageController.Clear_Hide()

                If Not Page.IsPostBack Then
                    MasterPage.MessageController.Clear()
                    MasterPage.UsePageTabTitleInBreadCrum = False
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")
                    UpdateBreadCrum()
                    TranslateGridHeader(Grid)

                    If Not State.IsReturningFromChild Then
                        ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    End If

                    If State.IsGridVisible Then
                        If Not (State.SelectedPageSize = DEFAULT_PAGE_SIZE) Then
                            cboPageSize.SelectedValue = CType(State.SelectedPageSize, String)
                            Grid.PageSize = State.SelectedPageSize
                        End If
                    End If
                    SetGridItemStyleColor(Grid)

                    If Not State.IsGridVisible Then
                        cboPageSize.SelectedValue = CType(State.PageSize, String)
                        If Not (Grid.PageSize = DEFAULT_NEW_UI_PAGE_SIZE) Then
                            cboPageSize.SelectedValue = CType(State.PageSize, String)
                            Grid.PageSize = State.PageSize
                        End If
                        State.IsGridVisible = True
                    End If

                    EnableDisableControl()

                    REM Me.SaveButton_WRITE.Enabled = True


                    SetSummary()
                    SetSession()
                    PopulateGrid()

                End If


            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

            If Me.State.LastOperation = DetailPageCommand.Redirect_ Then
                MasterPage.MessageController.Clear_Hide()
                State.LastOperation = DetailPageCommand.Nothing_
            Else
                ShowMissingTranslations(MasterPage.MessageController)
            End If
        End Sub
        Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles Me.PageReturn
            Dim retObj As ProductPriceRangeByRepairMethod.ReturnType = CType(ReturnPar, ProductPriceRangeByRepairMethod.ReturnType)

            'Me.State.BenefitProductCodeId = retObj.EditingId
            'Me.SetStateProperties()
            State.LastOperation = DetailPageCommand.Redirect_

        End Sub

        Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
            Try
                If CallingParameters IsNot Nothing Then
                    Dim pageParameters As PageParameters = CType(CallingParameters, PageParameters)
                    If pageParameters IsNot Nothing Then
                        State.Status = pageParameters.Status
                        State.FileProcessedId = pageParameters.FileProcessedId
                        State.StartDate = pageParameters.StartDate
                        State.EndDate = pageParameters.EndDate
                        State.Filename = pageParameters.Filename
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub
#End Region

#Region "Methods"

        Private Sub UpdateBreadCrum()
            If (State IsNot Nothing) Then
                If (State IsNot Nothing) Then
                    MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("Apple_Gbi_File")
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Apple_Gbi_File")
                End If
            End If
        End Sub

        Public Sub SetSummary()
            Try
                moFileNameText.Text = State.Filename
                moStatusType.Text = State.Status
                moStartDateText.Text = DateHelper.GetEnglishDate(State.StartDate)
                moEndDateText.Text = DateHelper.GetEnglishDate(State.EndDate)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub SavePage()
            Dim index As Integer = 0
            Dim appleGBIFileReconWrkInfo As AppleGBIFileReconWrk
            Dim totItems As Integer = Grid.Rows.Count

            If totItems > 0 Then
                appleGBIFileReconWrkInfo = CreateBoFromGrid(0)
                Diagnostics.Debug.WriteLine($"Id: {appleGBIFileReconWrkInfo.Id}")
                BindBoPropertiesToGridHeaders(appleGBIFileReconWrkInfo)
                PopulateBOFromForm(appleGBIFileReconWrkInfo)
                If appleGBIFileReconWrkInfo.IsDirty Then
                    appleGBIFileReconWrkInfo.RejectCode = ""
                    appleGBIFileReconWrkInfo.RejectReason = ""
                    appleGBIFileReconWrkInfo.RecordStatus = STATUS_REPROCESS
                    State.IsFileDataEdited = True
                End If
                appleGBIFileReconWrkInfo.Save()
            End If

            totItems = totItems - 1
            For index = 1 To totItems
                appleGBIFileReconWrkInfo = CreateBoFromGrid(index)
                Diagnostics.Debug.WriteLine($"Id: {appleGBIFileReconWrkInfo.Id}")
                BindBoPropertiesToGridHeaders(appleGBIFileReconWrkInfo)
                PopulateBOFromForm(appleGBIFileReconWrkInfo)
                If appleGBIFileReconWrkInfo.IsDirty Then
                    appleGBIFileReconWrkInfo.RejectCode = ""
                    appleGBIFileReconWrkInfo.RejectReason = ""
                    appleGBIFileReconWrkInfo.RecordStatus = STATUS_REPROCESS
                    State.IsFileDataEdited = True
                End If
                appleGBIFileReconWrkInfo.Save()
            Next
            REM DealerReconWrk.UpdateHeaderCount(appleGBIFileReconWrkInfo.DealerfileProcessedId)
        End Sub

        Private Function CreateBoFromGrid(index As Integer) As AppleGBIFileReconWrk
            Dim appleGBIFileReconWrkId As Guid
            Dim appleGBIFileReconWrkInfo As AppleGBIFileReconWrk

            Grid.SelectedIndex = index
            appleGBIFileReconWrkId = New Guid(CType(Grid.Rows(index).FindControl("moReconWrkId"), Label).Text)
            appleGBIFileReconWrkInfo = New AppleGBIFileReconWrk(appleGBIFileReconWrkId)
            Return appleGBIFileReconWrkInfo
        End Function

        Protected Sub BindBoPropertiesToGridHeaders(appleGBIFileReconWrkInfo As AppleGBIFileReconWrk)

            BindBOPropertyToGridHeader(appleGBIFileReconWrkInfo, "Id", Grid.Columns(GridDefenitionEnum.GbiClaimReconWrkId))
            BindBOPropertyToGridHeader(appleGBIFileReconWrkInfo, "RejectCode", Grid.Columns(GridDefenitionEnum.RejectCode))
            BindBOPropertyToGridHeader(appleGBIFileReconWrkInfo, "RejectReason", Grid.Columns(GridDefenitionEnum.RejectReason))
            BindBOPropertyToGridHeader(appleGBIFileReconWrkInfo, "CustomerId", Grid.Columns(GridDefenitionEnum.CustomerId))
            BindBOPropertyToGridHeader(appleGBIFileReconWrkInfo, "ShipToId", Grid.Columns(GridDefenitionEnum.ShipToId))
            BindBOPropertyToGridHeader(appleGBIFileReconWrkInfo, "AgreementId", Grid.Columns(GridDefenitionEnum.AgreementId))
            BindBOPropertyToGridHeader(appleGBIFileReconWrkInfo, "UniqueIdentifier", Grid.Columns(GridDefenitionEnum.UniqueIdentifier))
            BindBOPropertyToGridHeader(appleGBIFileReconWrkInfo, "OriginalSerialNumber", Grid.Columns(GridDefenitionEnum.OriginalSerialNumber))
            BindBOPropertyToGridHeader(appleGBIFileReconWrkInfo, "OriginalImeiNumber", Grid.Columns(GridDefenitionEnum.OriginalImeiNumber))
            BindBOPropertyToGridHeader(appleGBIFileReconWrkInfo, "NewSerialNumber", Grid.Columns(GridDefenitionEnum.NewSerialNumber))
            BindBOPropertyToGridHeader(appleGBIFileReconWrkInfo, "NewImeiNumber", Grid.Columns(GridDefenitionEnum.NewImeiNumber))
            BindBOPropertyToGridHeader(appleGBIFileReconWrkInfo, "RepairCompletionDate", Grid.Columns(GridDefenitionEnum.RepairCompletionDate))
            BindBOPropertyToGridHeader(appleGBIFileReconWrkInfo, "ClaimType", Grid.Columns(GridDefenitionEnum.ClaimType))
            BindBOPropertyToGridHeader(appleGBIFileReconWrkInfo, "Channel", Grid.Columns(GridDefenitionEnum.Channel))
            BindBOPropertyToGridHeader(appleGBIFileReconWrkInfo, "IncidentFee", Grid.Columns(GridDefenitionEnum.IncidentFee))
            BindBOPropertyToGridHeader(appleGBIFileReconWrkInfo, "NotifCreateDate", Grid.Columns(GridDefenitionEnum.NotifCreateDate))
            BindBOPropertyToGridHeader(appleGBIFileReconWrkInfo, "RepairCompleted", Grid.Columns(GridDefenitionEnum.RepairCompleted))
            BindBOPropertyToGridHeader(appleGBIFileReconWrkInfo, "RepairCompletedDate", Grid.Columns(GridDefenitionEnum.RepairCompletedDate))
            BindBOPropertyToGridHeader(appleGBIFileReconWrkInfo, "ClaimCancelled", Grid.Columns(GridDefenitionEnum.ClaimCancelled))
            BindBOPropertyToGridHeader(appleGBIFileReconWrkInfo, "Description", Grid.Columns(GridDefenitionEnum.Description))

            ClearGridViewHeadersAndLabelsErrSign()
        End Sub

        Private Sub PopulateBOFromForm(appleGBIFileReconWrkInfo As AppleGBIFileReconWrk)

            REM PopulateBOItem(appleGBIFileReconWrkInfo, "Id", GridDefenitionEnum.GbiClaimReconWrkId)
            PopulateBOItem(appleGBIFileReconWrkInfo, "RejectCode", GridDefenitionEnum.RejectCode)
            PopulateBOItem(appleGBIFileReconWrkInfo, "RejectReason", GridDefenitionEnum.RejectReason)
            PopulateBOItem(appleGBIFileReconWrkInfo, "CustomerId", GridDefenitionEnum.CustomerId)
            PopulateBOItem(appleGBIFileReconWrkInfo, "ShipToId", GridDefenitionEnum.ShipToId)
            PopulateBOItem(appleGBIFileReconWrkInfo, "AgreementId", GridDefenitionEnum.AgreementId)
            PopulateBOItem(appleGBIFileReconWrkInfo, "UniqueIdentifier", GridDefenitionEnum.UniqueIdentifier)
            PopulateBOItem(appleGBIFileReconWrkInfo, "OriginalSerialNumber", GridDefenitionEnum.OriginalSerialNumber)
            PopulateBOItem(appleGBIFileReconWrkInfo, "OriginalImeiNumber", GridDefenitionEnum.OriginalImeiNumber)
            PopulateBOItem(appleGBIFileReconWrkInfo, "NewSerialNumber", GridDefenitionEnum.NewSerialNumber)
            PopulateBOItem(appleGBIFileReconWrkInfo, "NewImeiNumber", GridDefenitionEnum.NewImeiNumber)
            PopulateBOItem(appleGBIFileReconWrkInfo, "RepairCompletionDate", GridDefenitionEnum.RepairCompletionDate)
            PopulateBOItem(appleGBIFileReconWrkInfo, "ClaimType", GridDefenitionEnum.ClaimType)
            PopulateBOItem(appleGBIFileReconWrkInfo, "Channel", GridDefenitionEnum.Channel)
            PopulateBOItem(appleGBIFileReconWrkInfo, "IncidentFee", GridDefenitionEnum.IncidentFee)
            PopulateBOItem(appleGBIFileReconWrkInfo, "NotifCreateDate", GridDefenitionEnum.NotifCreateDate)
            PopulateBOItem(appleGBIFileReconWrkInfo, "RepairCompleted", GridDefenitionEnum.RepairCompleted)
            PopulateBOItem(appleGBIFileReconWrkInfo, "RepairCompletedDate", GridDefenitionEnum.RepairCompletedDate)
            PopulateBOItem(appleGBIFileReconWrkInfo, "ClaimCancelled", GridDefenitionEnum.ClaimCancelled)
            PopulateBOItem(appleGBIFileReconWrkInfo, "Description", GridDefenitionEnum.Description)

            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Private Sub PopulateBOItem(appleGBIFileReconWrkInfo As AppleGBIFileReconWrk, oPropertyName As String, oCellPosition As Integer)
            PopulateBOProperty(appleGBIFileReconWrkInfo, oPropertyName, CType(GetSelectedGridControl(Grid, oCellPosition), TextBox))
        End Sub

        Private Sub EnableDisableControl()
            Try
                Select Case State.Status
                    Case STATUS_FAILED
                        ControlMgr.SetEnableControl(Me, SaveButton_WRITE, True)
                        ControlMgr.SetEnableControl(Me, btnReprocess, False)
                    Case STATUS_REPROCESS
                        ControlMgr.SetEnableControl(Me, btnReprocess, True)
                        ControlMgr.SetEnableControl(Me, SaveButton_WRITE, False)
                    Case Else
                        ControlMgr.SetEnableControl(Me, SaveButton_WRITE, False)
                        ControlMgr.SetEnableControl(Me, btnReprocess, False)
                End Select

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Grid Handles"
        Public Property SortDirection() As String
            Get
                Return ViewState("SortDirection")?.ToString
            End Get
            Set(value As String)
                ViewState("SortDirection") = value
            End Set
        End Property
        Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                State.PageIndex = Grid.PageSize
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                State.PageIndex = e.NewPageIndex
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Sub Grid_RowCommand(source As System.Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
            Try
                'If e.CommandName = GRID_COMMAND_SHOW_PROCESSED Then

                'End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Public Sub Grid_RowCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
            BaseItemCreated(sender, e)
        End Sub

        Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                Dim oTextBox As TextBox
                Dim oLabel As Label
                If dvRow IsNot Nothing Then
                    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then

                        REM e.Row.Cells(GridDefenitionEnum.GbiClaimReconWrkId).Text = GetGuidStringFromByteArray(CType(dvRow(AppleGBIFileReconWrk.COL_NAME_DET_BEN_GBICLAIM_RECON_WRK_ID), Byte()))
                        oLabel = CType(e.Row.FindControl("moReconWrkId"), Label)
                        PopulateControlFromBOProperty(oLabel, GetGuidStringFromByteArray(CType(dvRow(AppleGBIFileReconWrk.COL_NAME_DET_BEN_GBICLAIM_QUEUE_ID), Byte())))


                        PopulateControlFromBOProperty(GetReadOnlyTextBox(e, "moRejectCode"), dvRow(AppleGBIFileReconWrk.COL_NAME_DET_REJECT_CODE))
                        PopulateControlFromBOProperty(GetReadOnlyTextBox(e, "moRejectReason"), dvRow(AppleGBIFileReconWrk.COL_NAME_DET_REJECT_REASON))
                        PopulateControlFromBOProperty(GetTextBox(e, "moCustomerId"), dvRow(AppleGBIFileReconWrk.COL_NAME_DET_CUSTOMER_ID))
                        PopulateControlFromBOProperty(GetTextBox(e, "moShipToId"), dvRow(AppleGBIFileReconWrk.COL_NAME_DET_SHIP_TO_ID))
                        PopulateControlFromBOProperty(GetTextBox(e, "moAgreementId"), dvRow(AppleGBIFileReconWrk.COL_NAME_DET_AGREEMENT_ID))
                        PopulateControlFromBOProperty(GetReadOnlyTextBox(e, "moUniqueIdentifier"), dvRow(AppleGBIFileReconWrk.COL_NAME_DET_UNIQUE_IDENTIFIER))
                        PopulateControlFromBOProperty(GetTextBox(e, "moOriginialSerialNumber"), dvRow(AppleGBIFileReconWrk.COL_NAME_DET_ORIGINAL_SERIAL_NUMBER))
                        PopulateControlFromBOProperty(GetTextBox(e, "moOriginalImeiNumber"), dvRow(AppleGBIFileReconWrk.COL_NAME_DET_ORIGINAL_IMEI_NUMBER))
                        PopulateControlFromBOProperty(GetTextBox(e, "moNewSerialNumber"), dvRow(AppleGBIFileReconWrk.COL_NAME_DET_NEW_SERIAL_NUMBER))
                        PopulateControlFromBOProperty(GetTextBox(e, "moNewImeiNumber"), dvRow(AppleGBIFileReconWrk.COL_NAME_DET_NEW_IMEI_NUMBER))

                        SetTextBoxWithCalendar(dvRow, AppleGBIFileReconWrk.COL_NAME_DET_REPAIR_COMPLETION_DATE, e, "moRepairCompletionDateText", "moRepairCompletionDateImage")

                        PopulateControlFromBOProperty(GetTextBox(e, "moClaimType"), dvRow(AppleGBIFileReconWrk.COL_NAME_DET_CLAIM_TYPE))
                        PopulateControlFromBOProperty(GetTextBox(e, "moChannel"), dvRow(AppleGBIFileReconWrk.COL_NAME_DET_CHANNEL))
                        PopulateControlFromBOProperty(GetTextBox(e, "moIncidentFee"), dvRow(AppleGBIFileReconWrk.COL_NAME_DET_INCIDENT_FEE))

                        SetTextBoxWithCalendar(dvRow, AppleGBIFileReconWrk.COL_NAME_DET_NOTIF_CREATE_DATE, e, "moNotifCreateDateText", "moNotifCreateDateImage")

                        PopulateControlFromBOProperty(GetTextBox(e, "moRepairCompleted"), dvRow(AppleGBIFileReconWrk.COL_NAME_DET_REPAIR_COMPLETED))

                        SetTextBoxWithCalendar(dvRow, AppleGBIFileReconWrk.COL_NAME_DET_REPAIR_COMPLETED_DATE, e, "moRepairCompletedDateText", "moRepairCompletedDateImage")

                        PopulateControlFromBOProperty(GetReadOnlyTextBox(e, "moClaimCancelled"), dvRow(AppleGBIFileReconWrk.COL_NAME_DET_CLAIM_CANCELLED))
                        PopulateControlFromBOProperty(GetTextBox(e, "moDescription"), dvRow(AppleGBIFileReconWrk.COL_NAME_DET_DESCRIPTION))
                        PopulateControlFromBOProperty(GetReadOnlyTextBox(e, "moClaimNumber"), dvRow(AppleGBIFileReconWrk.COL_NAME_DET_CLAIM_NUMBER))

                        PopulateControlFromBOProperty(GetReadOnlyTextBox(e, "moDeviceType"), dvRow(AppleGBIFileReconWrk.COL_NAME_DEVICE_TYPE))

                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub SetTextBoxWithCalendar(dvRow As DataRowView, columnName As String, e As System.Web.UI.WebControls.GridViewRowEventArgs, txtControlName As String, calendarControlName As String)
            Dim oTextBox As TextBox

            oTextBox = GetTextBox(e, txtControlName)
            If oTextBox IsNot Nothing Then
                Dim oDateCompImage As ImageButton = CType(e.Row.FindControl(calendarControlName), ImageButton)
                If (oDateCompImage IsNot Nothing) Then
                    AddCalendar(oDateCompImage, oTextBox)
                End If

                If dvRow.Row(columnName) IsNot DBNull.Value Then
                    PopulateControlFromBOProperty(oTextBox, dvRow(columnName))
                End If
            End If
        End Sub


        Private Function IsFailedStatus() As Boolean
            If State.Status.Equals(STATUS_FAILED) Then
                Return True
            Else
                Return False
            End If
        End Function
        Private Function GetTextBox(e As System.Web.UI.WebControls.GridViewRowEventArgs, controlName As String) As TextBox
            Dim oTextBox As TextBox
            oTextBox = CType(e.Row.FindControl(controlName), TextBox)
            oTextBox.Attributes.Add("onchange", "setDirty()")
            If State.Status.Equals(STATUS_FAILED) Then
                oTextBox.ReadOnly = False
            Else
                oTextBox.ReadOnly = True
            End If
            Return oTextBox
        End Function
        Private Function GetReadOnlyTextBox(e As System.Web.UI.WebControls.GridViewRowEventArgs, controlName As String) As TextBox
            Dim oTextBox As TextBox
            oTextBox = CType(e.Row.FindControl(controlName), TextBox)
            oTextBox.Attributes.Add("onchange", "setDirty()")
            oTextBox.ReadOnly = True
            Return oTextBox
        End Function

        Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
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
                State.SortExpression = SortDirection
                State.PageIndex = 0
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Populate"
        Private Sub PopulateGrid(Optional ByVal refreshData As Boolean = False)
            Try

                If State.searchDV Is Nothing OrElse refreshData Then GetGBIFileRecords(refreshData)

                If (State.searchDV.Count = 0) Then

                    State.bnoRow = True
                    CreateHeaderForEmptyGrid(Grid, SortDirection)
                Else
                    State.bnoRow = False
                    Grid.Enabled = True
                    Grid.Visible = True
                End If

                Grid.AutoGenerateColumns = False
                Grid.AllowSorting = True
                State.searchDV.Sort = State.SortExpression

                Grid.Columns(GridDefenitionEnum.OriginalSerialNumber).SortExpression = AppleGBIFileReconWrk.COL_NAME_DET_ORIGINAL_SERIAL_NUMBER
                HighLightSortColumn(Grid, State.SortExpression)

                SetPageAndSelectedIndexFromGuid(State.searchDV, Guid.Empty, Grid, State.PageIndex)

                Grid.DataSource = State.searchDV
                HighLightSortColumn(Grid, State.SortExpression)
                Grid.DataBind()

                ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
                ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)

                Session("recCount") = State.searchDV.Count

                If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

                If Grid.Visible Then
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub GetGBIFileRecords(Optional ByVal refreshData As Boolean = False)
            Dim strTemp As String = String.Empty

            Try

                State.searchDV = AppleGBIFileReconWrk.LoadDeatils(State.FileProcessedId, State.Status)


            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub


#End Region
    End Class
End Namespace