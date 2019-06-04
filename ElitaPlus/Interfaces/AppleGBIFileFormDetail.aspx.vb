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
            With Me.State
                .PageIndex = Grid.PageIndex
                .PageSize = Grid.PageSize
                .PageSort = Me.State.SortExpression
                .searchDV = Me.State.searchDV
            End With
        End Sub
#End Region

#Region "Handlers-Buttons-Methods"
        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try

                GoBack()

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub GoBack()

            Dim retType As New AppleGBIFileForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.FileProcessedId, False)
            Me.ReturnToCallingPage(retType)

        End Sub

        Private Sub SaveButton_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton_WRITE.Click
            Try
                SavePage()

                ' Initiate the file process if data is changed
                If State.IsFileDataEdited Then
                    AppleGBIFileReconWrk.ProcessFile(State.FileProcessedId)
                    State.IsFileDataEdited = False
                End If

                ControlMgr.SetEnableControl(Me, Me.SaveButton_WRITE, False)

                Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)

                'Select Case SaveBundles()
                '    Case 1, 2
                '        Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                'End Select
                'Me.HiddenIsPageDirty.Value = Empty
                'PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
        Protected Sub btnReprocess_Click(sender As Object, e As EventArgs) Handles btnReprocess.Click
            AppleGBIFileReconWrk.ProcessFile(State.FileProcessedId)
            ControlMgr.SetEnableControl(Me, btnReprocess, False)

            Me.DisplayMessage(Message.REPROCESS_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)

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

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Try
                Me.MasterPage.MessageController.Clear_Hide()

                If Not Page.IsPostBack Then
                    Me.MasterPage.MessageController.Clear()
                    Me.MasterPage.UsePageTabTitleInBreadCrum = False
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")
                    UpdateBreadCrum()
                    TranslateGridHeader(Grid)

                    If Not Me.State.IsReturningFromChild Then
                        ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    End If

                    If Me.State.IsGridVisible Then
                        If Not (Me.State.SelectedPageSize = DEFAULT_PAGE_SIZE) Then
                            cboPageSize.SelectedValue = CType(Me.State.SelectedPageSize, String)
                            Grid.PageSize = Me.State.SelectedPageSize
                        End If
                    End If
                    Me.SetGridItemStyleColor(Me.Grid)

                    If Not State.IsGridVisible Then
                        cboPageSize.SelectedValue = CType(State.PageSize, String)
                        If Not (Grid.PageSize = DEFAULT_NEW_UI_PAGE_SIZE) Then
                            cboPageSize.SelectedValue = CType(State.PageSize, String)
                            Grid.PageSize = State.PageSize
                        End If
                        Me.State.IsGridVisible = True
                    End If

                    EnableDisableControl()

                    REM Me.SaveButton_WRITE.Enabled = True


                    SetSummary()
                    SetSession()
                    PopulateGrid()

                End If


            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

            If Me.State.LastOperation = DetailPageCommand.Redirect_ Then
                Me.MasterPage.MessageController.Clear_Hide()
                Me.State.LastOperation = DetailPageCommand.Nothing_
            Else
                Me.ShowMissingTranslations(Me.MasterPage.MessageController)
            End If
        End Sub
        Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles Me.PageReturn
            Dim retObj As ProductPriceRangeByRepairMethod.ReturnType = CType(ReturnPar, ProductPriceRangeByRepairMethod.ReturnType)

            'Me.State.BenefitProductCodeId = retObj.EditingId
            'Me.SetStateProperties()
            Me.State.LastOperation = DetailPageCommand.Redirect_

        End Sub

        Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
            Try
                If Not Me.CallingParameters Is Nothing Then
                    Dim pageParameters As PageParameters = CType(Me.CallingParameters, PageParameters)
                    If Not pageParameters Is Nothing Then
                        Me.State.Status = pageParameters.Status
                        Me.State.FileProcessedId = pageParameters.FileProcessedId
                        Me.State.StartDate = pageParameters.StartDate
                        Me.State.EndDate = pageParameters.EndDate
                        Me.State.Filename = pageParameters.Filename
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub
#End Region

#Region "Methods"

        Private Sub UpdateBreadCrum()
            If (Not Me.State Is Nothing) Then
                If (Not Me.State Is Nothing) Then
                    Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("Apple_Gbi_File")
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Apple_Gbi_File")
                End If
            End If
        End Sub

        Public Sub SetSummary()
            Try
                Me.moFileNameText.Text = Me.State.Filename
                Me.moStatusType.Text = Me.State.Status
                Me.moStartDateText.Text = DateHelper.GetEnglishDate(Me.State.StartDate)
                Me.moEndDateText.Text = DateHelper.GetEnglishDate(Me.State.EndDate)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub SavePage()
            Dim index As Integer = 0
            Dim appleGBIFileReconWrkInfo As AppleGBIFileReconWrk
            Dim totItems As Integer = Me.Grid.Rows.Count

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

        Private Function CreateBoFromGrid(ByVal index As Integer) As AppleGBIFileReconWrk
            Dim appleGBIFileReconWrkId As Guid
            Dim appleGBIFileReconWrkInfo As AppleGBIFileReconWrk

            Me.Grid.SelectedIndex = index
            appleGBIFileReconWrkId = New Guid(CType(Me.Grid.Rows(index).FindControl("moReconWrkId"), Label).Text)
            appleGBIFileReconWrkInfo = New AppleGBIFileReconWrk(appleGBIFileReconWrkId)
            Return appleGBIFileReconWrkInfo
        End Function

        Protected Sub BindBoPropertiesToGridHeaders(ByVal appleGBIFileReconWrkInfo As AppleGBIFileReconWrk)

            BindBOPropertyToGridHeader(appleGBIFileReconWrkInfo, "Id", Me.Grid.Columns(GridDefenitionEnum.GbiClaimReconWrkId))
            BindBOPropertyToGridHeader(appleGBIFileReconWrkInfo, "RejectCode", Me.Grid.Columns(GridDefenitionEnum.RejectCode))
            BindBOPropertyToGridHeader(appleGBIFileReconWrkInfo, "RejectReason", Me.Grid.Columns(GridDefenitionEnum.RejectReason))
            BindBOPropertyToGridHeader(appleGBIFileReconWrkInfo, "CustomerId", Me.Grid.Columns(GridDefenitionEnum.CustomerId))
            BindBOPropertyToGridHeader(appleGBIFileReconWrkInfo, "ShipToId", Me.Grid.Columns(GridDefenitionEnum.ShipToId))
            BindBOPropertyToGridHeader(appleGBIFileReconWrkInfo, "AgreementId", Me.Grid.Columns(GridDefenitionEnum.AgreementId))
            BindBOPropertyToGridHeader(appleGBIFileReconWrkInfo, "UniqueIdentifier", Me.Grid.Columns(GridDefenitionEnum.UniqueIdentifier))
            BindBOPropertyToGridHeader(appleGBIFileReconWrkInfo, "OriginalSerialNumber", Me.Grid.Columns(GridDefenitionEnum.OriginalSerialNumber))
            BindBOPropertyToGridHeader(appleGBIFileReconWrkInfo, "OriginalImeiNumber", Me.Grid.Columns(GridDefenitionEnum.OriginalImeiNumber))
            BindBOPropertyToGridHeader(appleGBIFileReconWrkInfo, "NewSerialNumber", Me.Grid.Columns(GridDefenitionEnum.NewSerialNumber))
            BindBOPropertyToGridHeader(appleGBIFileReconWrkInfo, "NewImeiNumber", Me.Grid.Columns(GridDefenitionEnum.NewImeiNumber))
            BindBOPropertyToGridHeader(appleGBIFileReconWrkInfo, "RepairCompletionDate", Me.Grid.Columns(GridDefenitionEnum.RepairCompletionDate))
            BindBOPropertyToGridHeader(appleGBIFileReconWrkInfo, "ClaimType", Me.Grid.Columns(GridDefenitionEnum.ClaimType))
            BindBOPropertyToGridHeader(appleGBIFileReconWrkInfo, "Channel", Me.Grid.Columns(GridDefenitionEnum.Channel))
            BindBOPropertyToGridHeader(appleGBIFileReconWrkInfo, "IncidentFee", Me.Grid.Columns(GridDefenitionEnum.IncidentFee))
            BindBOPropertyToGridHeader(appleGBIFileReconWrkInfo, "NotifCreateDate", Me.Grid.Columns(GridDefenitionEnum.NotifCreateDate))
            BindBOPropertyToGridHeader(appleGBIFileReconWrkInfo, "RepairCompleted", Me.Grid.Columns(GridDefenitionEnum.RepairCompleted))
            BindBOPropertyToGridHeader(appleGBIFileReconWrkInfo, "RepairCompletedDate", Me.Grid.Columns(GridDefenitionEnum.RepairCompletedDate))
            BindBOPropertyToGridHeader(appleGBIFileReconWrkInfo, "ClaimCancelled", Me.Grid.Columns(GridDefenitionEnum.ClaimCancelled))
            BindBOPropertyToGridHeader(appleGBIFileReconWrkInfo, "Description", Me.Grid.Columns(GridDefenitionEnum.Description))

            ClearGridViewHeadersAndLabelsErrSign()
        End Sub

        Private Sub PopulateBOFromForm(ByVal appleGBIFileReconWrkInfo As AppleGBIFileReconWrk)

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

            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Private Sub PopulateBOItem(ByVal appleGBIFileReconWrkInfo As AppleGBIFileReconWrk, ByVal oPropertyName As String, ByVal oCellPosition As Integer)
            Me.PopulateBOProperty(appleGBIFileReconWrkInfo, oPropertyName, CType(Me.GetSelectedGridControl(Me.Grid, oCellPosition), TextBox))
        End Sub

        Private Sub EnableDisableControl()
            Try
                Select Case Me.State.Status
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
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Grid Handles"
        Public Property SortDirection() As String
            Get
                Return ViewState("SortDirection")?.ToString
            End Get
            Set(ByVal value As String)
                ViewState("SortDirection") = value
            End Set
        End Property
        Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                Me.State.PageIndex = Grid.PageSize
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                Me.State.PageIndex = e.NewPageIndex
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Public Sub Grid_RowCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
            Try
                'If e.CommandName = GRID_COMMAND_SHOW_PROCESSED Then

                'End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Public Sub Grid_RowCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
            BaseItemCreated(sender, e)
        End Sub

        Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                Dim oTextBox As TextBox
                Dim oLabel As Label
                If Not dvRow Is Nothing Then
                    If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then

                        REM e.Row.Cells(GridDefenitionEnum.GbiClaimReconWrkId).Text = GetGuidStringFromByteArray(CType(dvRow(AppleGBIFileReconWrk.COL_NAME_DET_BEN_GBICLAIM_RECON_WRK_ID), Byte()))
                        oLabel = CType(e.Row.FindControl("moReconWrkId"), Label)
                        Me.PopulateControlFromBOProperty(oLabel, GetGuidStringFromByteArray(CType(dvRow(AppleGBIFileReconWrk.COL_NAME_DET_BEN_GBICLAIM_QUEUE_ID), Byte())))


                        Me.PopulateControlFromBOProperty(GetReadOnlyTextBox(e, "moRejectCode"), dvRow(AppleGBIFileReconWrk.COL_NAME_DET_REJECT_CODE))
                        Me.PopulateControlFromBOProperty(GetReadOnlyTextBox(e, "moRejectReason"), dvRow(AppleGBIFileReconWrk.COL_NAME_DET_REJECT_REASON))
                        Me.PopulateControlFromBOProperty(GetTextBox(e, "moCustomerId"), dvRow(AppleGBIFileReconWrk.COL_NAME_DET_CUSTOMER_ID))
                        Me.PopulateControlFromBOProperty(GetTextBox(e, "moShipToId"), dvRow(AppleGBIFileReconWrk.COL_NAME_DET_SHIP_TO_ID))
                        Me.PopulateControlFromBOProperty(GetTextBox(e, "moAgreementId"), dvRow(AppleGBIFileReconWrk.COL_NAME_DET_AGREEMENT_ID))
                        Me.PopulateControlFromBOProperty(GetReadOnlyTextBox(e, "moUniqueIdentifier"), dvRow(AppleGBIFileReconWrk.COL_NAME_DET_UNIQUE_IDENTIFIER))
                        Me.PopulateControlFromBOProperty(GetTextBox(e, "moOriginialSerialNumber"), dvRow(AppleGBIFileReconWrk.COL_NAME_DET_ORIGINAL_SERIAL_NUMBER))
                        Me.PopulateControlFromBOProperty(GetTextBox(e, "moOriginalImeiNumber"), dvRow(AppleGBIFileReconWrk.COL_NAME_DET_ORIGINAL_IMEI_NUMBER))
                        Me.PopulateControlFromBOProperty(GetTextBox(e, "moNewSerialNumber"), dvRow(AppleGBIFileReconWrk.COL_NAME_DET_NEW_SERIAL_NUMBER))
                        Me.PopulateControlFromBOProperty(GetTextBox(e, "moNewImeiNumber"), dvRow(AppleGBIFileReconWrk.COL_NAME_DET_NEW_IMEI_NUMBER))

                        SetTextBoxWithCalendar(dvRow, AppleGBIFileReconWrk.COL_NAME_DET_REPAIR_COMPLETION_DATE, e, "moRepairCompletionDateText", "moRepairCompletionDateImage")

                        Me.PopulateControlFromBOProperty(GetTextBox(e, "moClaimType"), dvRow(AppleGBIFileReconWrk.COL_NAME_DET_CLAIM_TYPE))
                        Me.PopulateControlFromBOProperty(GetTextBox(e, "moChannel"), dvRow(AppleGBIFileReconWrk.COL_NAME_DET_CHANNEL))
                        Me.PopulateControlFromBOProperty(GetTextBox(e, "moIncidentFee"), dvRow(AppleGBIFileReconWrk.COL_NAME_DET_INCIDENT_FEE))

                        SetTextBoxWithCalendar(dvRow, AppleGBIFileReconWrk.COL_NAME_DET_NOTIF_CREATE_DATE, e, "moNotifCreateDateText", "moNotifCreateDateImage")

                        Me.PopulateControlFromBOProperty(GetTextBox(e, "moRepairCompleted"), dvRow(AppleGBIFileReconWrk.COL_NAME_DET_REPAIR_COMPLETED))

                        SetTextBoxWithCalendar(dvRow, AppleGBIFileReconWrk.COL_NAME_DET_REPAIR_COMPLETED_DATE, e, "moRepairCompletedDateText", "moRepairCompletedDateImage")

                        Me.PopulateControlFromBOProperty(GetReadOnlyTextBox(e, "moClaimCancelled"), dvRow(AppleGBIFileReconWrk.COL_NAME_DET_CLAIM_CANCELLED))
                        Me.PopulateControlFromBOProperty(GetTextBox(e, "moDescription"), dvRow(AppleGBIFileReconWrk.COL_NAME_DET_DESCRIPTION))
                        PopulateControlFromBOProperty(GetReadOnlyTextBox(e, "moClaimNumber"), dvRow(AppleGBIFileReconWrk.COL_NAME_DET_CLAIM_NUMBER))

                        Me.PopulateControlFromBOProperty(GetReadOnlyTextBox(e, "moDeviceType"), dvRow(AppleGBIFileReconWrk.COL_NAME_DEVICE_TYPE))

                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub SetTextBoxWithCalendar(dvRow As DataRowView, columnName As String, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs, txtControlName As String, calendarControlName As String)
            Dim oTextBox As TextBox

            oTextBox = GetTextBox(e, txtControlName)
            If oTextBox IsNot Nothing Then
                Dim oDateCompImage As ImageButton = CType(e.Row.FindControl(calendarControlName), ImageButton)
                If (oDateCompImage IsNot Nothing) Then
                    Me.AddCalendar(oDateCompImage, oTextBox)
                End If

                If dvRow.Row(columnName) IsNot DBNull.Value Then
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(columnName))
                End If
            End If
        End Sub


        Private Function IsFailedStatus() As Boolean
            If Me.State.Status.Equals(STATUS_FAILED) Then
                Return True
            Else
                Return False
            End If
        End Function
        Private Function GetTextBox(ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs, ByVal controlName As String) As TextBox
            Dim oTextBox As TextBox
            oTextBox = CType(e.Row.FindControl(controlName), TextBox)
            oTextBox.Attributes.Add("onchange", "setDirty()")
            If Me.State.Status.Equals(STATUS_FAILED) Then
                oTextBox.ReadOnly = False
            Else
                oTextBox.ReadOnly = True
            End If
            Return oTextBox
        End Function
        Private Function GetReadOnlyTextBox(ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs, ByVal controlName As String) As TextBox
            Dim oTextBox As TextBox
            oTextBox = CType(e.Row.FindControl(controlName), TextBox)
            oTextBox.Attributes.Add("onchange", "setDirty()")
            oTextBox.ReadOnly = True
            Return oTextBox
        End Function

        Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
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
                Me.State.SortExpression = Me.SortDirection
                Me.State.PageIndex = 0
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Populate"
        Private Sub PopulateGrid(Optional ByVal refreshData As Boolean = False)
            Try

                If Me.State.searchDV Is Nothing OrElse refreshData Then GetGBIFileRecords(refreshData)

                If (Me.State.searchDV.Count = 0) Then

                    Me.State.bnoRow = True
                    CreateHeaderForEmptyGrid(Grid, Me.SortDirection)
                Else
                    Me.State.bnoRow = False
                    Me.Grid.Enabled = True
                    Me.Grid.Visible = True
                End If

                Grid.AutoGenerateColumns = False
                Me.Grid.AllowSorting = True
                Me.State.searchDV.Sort = Me.State.SortExpression

                Grid.Columns(GridDefenitionEnum.OriginalSerialNumber).SortExpression = AppleGBIFileReconWrk.COL_NAME_DET_ORIGINAL_SERIAL_NUMBER
                HighLightSortColumn(Grid, Me.State.SortExpression)

                SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Guid.Empty, Me.Grid, Me.State.PageIndex)

                Me.Grid.DataSource = Me.State.searchDV
                HighLightSortColumn(Grid, Me.State.SortExpression)
                Me.Grid.DataBind()

                ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)
                ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)

                Session("recCount") = Me.State.searchDV.Count

                If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

                If Me.Grid.Visible Then
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub GetGBIFileRecords(Optional ByVal refreshData As Boolean = False)
            Dim strTemp As String = String.Empty

            Try

                State.searchDV = AppleGBIFileReconWrk.LoadDeatils(Me.State.FileProcessedId, Me.State.Status)


            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub


#End Region
    End Class
End Namespace