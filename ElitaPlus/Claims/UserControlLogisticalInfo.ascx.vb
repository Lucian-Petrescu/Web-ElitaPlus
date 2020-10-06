Public Class UserControlLogisticalInfo
    Inherits System.Web.UI.UserControl

    'Public Delegate Sub RequestData(ByVal sender As Object, ByRef e As RequestDataEventArgs)

    'Public Event RequestClaimLogisticalData As RequestData

#Region "Constants"
    Private Const EDIT_COMMAND As String = "SelectAction"

    Private Const GRID_COL_CLAIM_SHIPPING_ID_IDX As Integer = 0
    Private Const GRID_COL_SHIPPING_TYPE_IDX As Integer = 1
    Private Const GRID_COL_SHIPPING_DATE_IDX As Integer = 2
    Private Const GRID_COL_TRACKING_NUMBER_IDX As Integer = 3
    Private Const GRID_COL_RECEIVED_DATE_IDX As Integer = 4

    Private Const NO_ROW_SELECTED_INDEX As Integer = -1

    Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
    Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

#End Region

#Region "Page State"
    ' This class keeps the current state for the search page.
    Class MyState
        Public MyBO As ClaimShipping
        Public claimId As Guid = Guid.Empty
        Public dtClaimCloseRules As DataTable
        Public SelectedClaimShippingID As Guid
        'Public SelectedShippingTypeId As Guid
        Public SelectedShippingDate As DateType
        Public PageIndex As Integer = 0
        Public PageSize As Integer = 10
        Public claimShippingDV As ClaimShipping.ClaimShippingDV = Nothing
        Public IsEditMode As Boolean = False
        Public ActionInProgress As Integer = ElitaPlusPage.DetailPageCommand.Nothing_

    End Class

    Protected ReadOnly Property TheState() As MyState
        Get
            Try
                If ThePage.StateSession.Item(UniqueID) Is Nothing Then
                    ThePage.StateSession.Item(UniqueID) = New MyState
                End If
                Return CType(ThePage.StateSession.Item(UniqueID), MyState)

            Catch ex As Exception
                'When we are in design mode there is no session object
                Return Nothing
            End Try
        End Get
    End Property

    Private Shadows ReadOnly Property ThePage() As ElitaPlusSearchPage
        Get
            Return CType(MyBase.Page, ElitaPlusSearchPage)
        End Get
    End Property

    Public ReadOnly Property IsGridInEditMode() As Boolean
        Get
            Return LogisticsGrid.EditIndex > ThePage.NO_ITEM_SELECTED_INDEX
        End Get
    End Property

    Public Property SortDirection() As String
        Get
            If ViewState("SortDirection") IsNot Nothing Then
                Return ViewState("SortDirection").ToString
            Else
                Return String.Empty
            End If

        End Get
        Set(value As String)
            ViewState("SortDirection") = value
        End Set
    End Property

    Public Property claimId As Guid
        Get
            Return TheState.claimId
        End Get
        Set(value As Guid)
            TheState.claimId = value
        End Set

    End Property

#End Region
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

    End Sub

    Public Sub PopulateGrid()
        If (Not Page.IsPostBack) Then
            ThePage.TranslateGridHeader(LogisticsGrid)
        End If

        Dim blnNewSearch As Boolean = False
        TheState.PageIndex = LogisticsGrid.PageIndex
        Dim objClaimShipping As New ClaimShipping

        Try
            With TheState
                If (.claimShippingDV Is Nothing) Then
                    objClaimShipping.ClaimId = TheState.claimId
                    .claimShippingDV = objClaimShipping.LoadClaimShippingData()
                End If
            End With

            If TheState.claimShippingDV.Count > 0 Then
                LogisticsGrid.AutoGenerateColumns = False
                If (TheState.IsEditMode) Then
                    ThePage.SetPageAndSelectedIndexFromGuid(TheState.claimShippingDV, TheState.SelectedClaimShippingID, LogisticsGrid, TheState.PageIndex, TheState.IsEditMode)
                End If
            End If
            LogisticsGrid.DataSource = TheState.claimShippingDV
            LogisticsGrid.DataBind()
        Catch ex As Exception
            ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)
        End Try

    End Sub
    Public Sub RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles LogisticsGrid.RowCreated
        Try
            ThePage.BaseItemCreated(sender, e)
        Catch ex As Exception
            ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles LogisticsGrid.RowDataBound
        Try
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim selectedId As Guid = Guid.Empty
            If e.Row.RowType = DataControlRowType.DataRow Then

                CType(e.Row.Cells(GRID_COL_SHIPPING_DATE_IDX).FindControl("TextboxShippingDate"), TextBox).Text = ElitaPlusPage.GetDateFormattedStringNullable(CType(dvRow("shipping_date"), Date))

                If TheState.IsEditMode Then
                    selectedId = GuidControl.ByteArrayToGuid(dvRow(ClaimShipping.ClaimShippingDV.COL_CLAIM_SHIPPING_ID))

                    If (TheState.IsEditMode AndAlso TheState.SelectedClaimShippingID.Equals(selectedId)) Then
                        CType(e.Row.Cells(GRID_COL_TRACKING_NUMBER_IDX).FindControl("TextboxTrackingNumber"), TextBox).Text = dvRow("tracking_number").ToString
                        CType(e.Row.Cells(GRID_COL_RECEIVED_DATE_IDX).FindControl("TextboxReceivedDate"), TextBox).Text = If(dvRow("received_date").ToString() <> String.Empty _
                        , ElitaPlusPage.GetLongDateFormattedString(CType(dvRow("received_date"), Date)), "")
                        Dim oRecdDateImage As ImageButton = CType(e.Row.Cells(GRID_COL_RECEIVED_DATE_IDX).FindControl("ImageButtonRecdDate"), ImageButton)
                        'once received date is populated, value cannot be updated
                        If (dvRow("received_date") Is Nothing Or dvRow("received_date").ToString() = String.Empty) Then
                            oRecdDateImage.Visible = True
                            CType(e.Row.Cells(GRID_COL_RECEIVED_DATE_IDX).FindControl("TextboxReceivedDate"), TextBox).ReadOnly = False
                            If (oRecdDateImage IsNot Nothing) Then
                                ThePage.AddCalendarwithTime_New(oRecdDateImage, CType(e.Row.Cells(GRID_COL_RECEIVED_DATE_IDX).FindControl("TextboxReceivedDate"), TextBox))
                            End If
                        Else
                            CType(e.Row.Cells(GRID_COL_RECEIVED_DATE_IDX).FindControl("TextboxReceivedDate"), TextBox).ReadOnly = True
                            oRecdDateImage.Visible = False
                        End If
                    Else 'format the rows that are not being edited
                        CType(e.Row.Cells(GRID_COL_TRACKING_NUMBER_IDX).FindControl("lblTrackingNumber"), Label).Text = dvRow("tracking_number").ToString
                        CType(e.Row.Cells(GRID_COL_RECEIVED_DATE_IDX).FindControl("lblReceivedDate"), Label).Text = If(dvRow("received_date").ToString() <> String.Empty _
                           , ElitaPlusPage.GetDateFormattedString(CType(dvRow("received_date"), Date)), "")
                    End If
                Else 'view mode
                    CType(e.Row.Cells(GRID_COL_TRACKING_NUMBER_IDX).FindControl("lblTrackingNumber"), Label).Text = dvRow("tracking_number").ToString
                    CType(e.Row.Cells(GRID_COL_RECEIVED_DATE_IDX).FindControl("lblReceivedDate"), Label).Text = If(dvRow("received_date").ToString() <> String.Empty _
                        , ElitaPlusPage.GetDateFormattedString(CType(dvRow("received_date"), Date)), "")
                End If
            End If
        Catch ex As Exception
            ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)
        End Try

    End Sub

    Public Sub RowCommand(source As System.Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles LogisticsGrid.RowCommand

        Try
            Dim index As Integer

            If (e.CommandName = EDIT_COMMAND) Then
                index = CInt(e.CommandArgument)
                'Do the Edit here

                'Set the IsEditMode flag to TRUE
                TheState.IsEditMode = True

                TheState.SelectedClaimShippingID = GuidControl.ByteArrayToGuid(LogisticsGrid.DataKeys(index).Values(0))

                TheState.MyBO = New ClaimShipping(TheState.SelectedClaimShippingID)

                PopulateGrid()

                TheState.PageIndex = LogisticsGrid.PageIndex

            End If

        Catch ex As Exception
            ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)
        End Try
    End Sub

    Private Function PopulateBOFromForm() As Boolean
        Dim objTrackingNumberTxt As TextBox
        Dim objReceivedDateTxt As TextBox
        Dim objShippingDateTxt As TextBox

        objTrackingNumberTxt = CType(LogisticsGrid.Rows(LogisticsGrid.EditIndex).Cells(GRID_COL_TRACKING_NUMBER_IDX).FindControl("TextboxTrackingNumber"), TextBox)
        objReceivedDateTxt = CType(LogisticsGrid.Rows(LogisticsGrid.EditIndex).Cells(GRID_COL_TRACKING_NUMBER_IDX).FindControl("TextboxReceivedDate"), TextBox)
        objShippingDateTxt = CType(LogisticsGrid.Rows(LogisticsGrid.EditIndex).Cells(GRID_COL_SHIPPING_DATE_IDX).FindControl("textboxShippingDate"), TextBox)

        ThePage.PopulateBOProperty(TheState.MyBO, "ClaimId", TheState.MyBO.ClaimId)
        ThePage.PopulateBOProperty(TheState.MyBO, "ShippingTypeId", TheState.MyBO.ShippingTypeId)
        ThePage.PopulateBOProperty(TheState.MyBO, "ShippingDate", objShippingDateTxt)
        ThePage.PopulateBOProperty(TheState.MyBO, "TrackingNumber", objTrackingNumberTxt)
        ThePage.PopulateBOProperty(TheState.MyBO, "ReceivedDate", objReceivedDateTxt)

        If ThePage.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Function

    Private Sub ReturnFromEditing()

        LogisticsGrid.EditIndex = NO_ROW_SELECTED_INDEX

        If (LogisticsGrid.PageCount = 0) Then
            ControlMgr.SetVisibleControl(ThePage, LogisticsGrid, False)
        Else
            ControlMgr.SetVisibleControl(ThePage, LogisticsGrid, True)
        End If

        TheState.IsEditMode = False
        PopulateGrid()
        TheState.PageIndex = LogisticsGrid.PageIndex

    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs)

        Try
            PopulateBOFromForm()

            If (TheState.MyBO.IsDirty) Then
                TheState.MyBO.Save()
                ThePage.MasterPage.MessageController.AddSuccess(MSG_RECORD_SAVED_OK, True)
                TheState.claimShippingDV = Nothing
                TheState.MyBO = Nothing
                ReturnFromEditing()
            Else
                ThePage.MasterPage.MessageController.AddWarning(MSG_RECORD_NOT_SAVED, True)
                ReturnFromEditing()
            End If
        Catch ex As Exception
            ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)
        End Try

    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        Try
            With TheState
                .SelectedClaimShippingID = Guid.Empty
                TheState.MyBO = Nothing
                .IsEditMode = False
            End With
            LogisticsGrid.EditIndex = NO_ROW_SELECTED_INDEX
            PopulateGrid()

        Catch ex As Exception
            ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)
        End Try
    End Sub
End Class