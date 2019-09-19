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
                If ThePage.StateSession.Item(Me.UniqueID) Is Nothing Then
                    ThePage.StateSession.Item(Me.UniqueID) = New MyState
                End If
                Return CType(ThePage.StateSession.Item(Me.UniqueID), MyState)

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
            Return Me.LogisticsGrid.EditIndex > ThePage.NO_ITEM_SELECTED_INDEX
        End Get
    End Property

    Public Property SortDirection() As String
        Get
            If Not ViewState("SortDirection") Is Nothing Then
                Return ViewState("SortDirection").ToString
            Else
                Return String.Empty
            End If

        End Get
        Set(ByVal value As String)
            ViewState("SortDirection") = value
        End Set
    End Property

    Public Property claimId As Guid
        Get
            Return Me.TheState.claimId
        End Get
        Set(ByVal value As Guid)
            Me.TheState.claimId = value
        End Set

    End Property

#End Region
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Sub PopulateGrid()
        If (Not Page.IsPostBack) Then
            Me.ThePage.TranslateGridHeader(LogisticsGrid)
        End If

        Dim blnNewSearch As Boolean = False
        Me.TheState.PageIndex = LogisticsGrid.PageIndex
        Dim objClaimShipping As New ClaimShipping

        Try
            With TheState
                If (.claimShippingDV Is Nothing) Then
                    objClaimShipping.ClaimId = Me.TheState.claimId
                    .claimShippingDV = objClaimShipping.LoadClaimShippingData()
                End If
            End With

            If Me.TheState.claimShippingDV.Count > 0 Then
                Me.LogisticsGrid.AutoGenerateColumns = False
                If (Me.TheState.IsEditMode) Then
                    Me.ThePage.SetPageAndSelectedIndexFromGuid(TheState.claimShippingDV, TheState.SelectedClaimShippingID, LogisticsGrid, TheState.PageIndex, TheState.IsEditMode)
                End If
            End If
            LogisticsGrid.DataSource = Me.TheState.claimShippingDV
            LogisticsGrid.DataBind()
        Catch ex As Exception
            Me.ThePage.HandleErrors(ex, Me.ThePage.MasterPage.MessageController)
        End Try

    End Sub
    Public Sub RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles LogisticsGrid.RowCreated
        Try
            ThePage.BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.ThePage.HandleErrors(ex, Me.ThePage.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles LogisticsGrid.RowDataBound
        Try
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim selectedId As Guid = Guid.Empty
            If e.Row.RowType = DataControlRowType.DataRow Then

                CType(e.Row.Cells(GRID_COL_SHIPPING_DATE_IDX).FindControl("TextboxShippingDate"), TextBox).Text = ElitaPlusPage.GetDateFormattedString(CType(dvRow("shipping_date"), Date))
                'Sridhar CType(e.Row.Cells(GRID_COL_SHIPPING_DATE_IDX).FindControl("TextboxShippingDate"), TextBox).Text = CType(dvRow("shipping_date"), Date).ToString(ElitaPlusPage.DATE_FORMAT)

                If Me.TheState.IsEditMode Then
                    selectedId = GuidControl.ByteArrayToGuid(dvRow(ClaimShipping.ClaimShippingDV.COL_CLAIM_SHIPPING_ID))

                    If (TheState.IsEditMode AndAlso TheState.SelectedClaimShippingID.Equals(selectedId)) Then
                        CType(e.Row.Cells(GRID_COL_TRACKING_NUMBER_IDX).FindControl("TextboxTrackingNumber"), TextBox).Text = dvRow("tracking_number").ToString
                        'Sridhar CType(e.Row.Cells(GRID_COL_RECEIVED_DATE_IDX).FindControl("TextboxReceivedDate"), TextBox).Text = If(dvRow("received_date").ToString() <> String.Empty _
                        ', CType(dvRow("received_date"), Date).ToString(ElitaPlusPage.DATE_TIME_FORMAT), "")
                        CType(e.Row.Cells(GRID_COL_RECEIVED_DATE_IDX).FindControl("TextboxReceivedDate"), TextBox).Text = If(dvRow("received_date").ToString() <> String.Empty _
                        , ElitaPlusPage.GetLongDateFormattedString(CType(dvRow("received_date"), Date)), "")
                        Dim oRecdDateImage As ImageButton = CType(e.Row.Cells(GRID_COL_RECEIVED_DATE_IDX).FindControl("ImageButtonRecdDate"), ImageButton)
                        'once received date is populated, value cannot be updated
                        If (dvRow("received_date") Is Nothing Or dvRow("received_date").ToString() = String.Empty) Then
                            oRecdDateImage.Visible = True
                            CType(e.Row.Cells(GRID_COL_RECEIVED_DATE_IDX).FindControl("TextboxReceivedDate"), TextBox).ReadOnly = False
                            If (Not oRecdDateImage Is Nothing) Then
                                ThePage.AddCalendarwithTime_New(oRecdDateImage, CType(e.Row.Cells(GRID_COL_RECEIVED_DATE_IDX).FindControl("TextboxReceivedDate"), TextBox))
                            End If
                        Else
                            CType(e.Row.Cells(GRID_COL_RECEIVED_DATE_IDX).FindControl("TextboxReceivedDate"), TextBox).ReadOnly = True
                            oRecdDateImage.Visible = False
                        End If
                    Else 'format the rows that are not being edited
                        CType(e.Row.Cells(GRID_COL_TRACKING_NUMBER_IDX).FindControl("lblTrackingNumber"), Label).Text = dvRow("tracking_number").ToString
                        'Sridhar CType(e.Row.Cells(GRID_COL_RECEIVED_DATE_IDX).FindControl("lblReceivedDate"), Label).Text = If(dvRow("received_date").ToString() <> String.Empty _
                        '    , CType(dvRow("received_date"), Date).ToString("dd-MMM-yyyy"), "")
                        CType(e.Row.Cells(GRID_COL_RECEIVED_DATE_IDX).FindControl("lblReceivedDate"), Label).Text = If(dvRow("received_date").ToString() <> String.Empty _
                           , ElitaPlusPage.GetDateFormattedString(CType(dvRow("received_date"), Date)), "")
                    End If
                Else 'view mode
                    CType(e.Row.Cells(GRID_COL_TRACKING_NUMBER_IDX).FindControl("lblTrackingNumber"), Label).Text = dvRow("tracking_number").ToString
                    'Sridhar CType(e.Row.Cells(GRID_COL_RECEIVED_DATE_IDX).FindControl("lblReceivedDate"), Label).Text = If(dvRow("received_date").ToString() <> String.Empty _
                    '    , CType(dvRow("received_date"), Date).ToString("dd-MMM-yyyy"), "")
                    CType(e.Row.Cells(GRID_COL_RECEIVED_DATE_IDX).FindControl("lblReceivedDate"), Label).Text = If(dvRow("received_date").ToString() <> String.Empty _
                        , ElitaPlusPage.GetDateFormattedString(CType(dvRow("received_date"), Date)), "")
                End If
            End If
        Catch ex As Exception
            Me.ThePage.HandleErrors(ex, Me.ThePage.MasterPage.MessageController)
        End Try

    End Sub

    Public Sub RowCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles LogisticsGrid.RowCommand

        Try
            Dim index As Integer

            If (e.CommandName = EDIT_COMMAND) Then
                index = CInt(e.CommandArgument)
                'Do the Edit here

                'Set the IsEditMode flag to TRUE
                Me.TheState.IsEditMode = True

                TheState.SelectedClaimShippingID = GuidControl.ByteArrayToGuid(LogisticsGrid.DataKeys(index).Values(0))

                Me.TheState.MyBO = New ClaimShipping(TheState.SelectedClaimShippingID)

                PopulateGrid()

                Me.TheState.PageIndex = LogisticsGrid.PageIndex

            End If

        Catch ex As Exception
            Me.ThePage.HandleErrors(ex, Me.ThePage.MasterPage.MessageController)
        End Try
    End Sub

    Private Function PopulateBOFromForm() As Boolean
        Dim objTrackingNumberTxt As TextBox
        Dim objReceivedDateTxt As TextBox
        Dim objShippingDateTxt As TextBox

        objTrackingNumberTxt = CType(LogisticsGrid.Rows(Me.LogisticsGrid.EditIndex).Cells(GRID_COL_TRACKING_NUMBER_IDX).FindControl("TextboxTrackingNumber"), TextBox)
        objReceivedDateTxt = CType(LogisticsGrid.Rows(Me.LogisticsGrid.EditIndex).Cells(GRID_COL_TRACKING_NUMBER_IDX).FindControl("TextboxReceivedDate"), TextBox)
        objShippingDateTxt = CType(LogisticsGrid.Rows(Me.LogisticsGrid.EditIndex).Cells(GRID_COL_SHIPPING_DATE_IDX).FindControl("textboxShippingDate"), TextBox)

        ThePage.PopulateBOProperty(TheState.MyBO, "ClaimId", TheState.MyBO.ClaimId)
        ThePage.PopulateBOProperty(TheState.MyBO, "ShippingTypeId", TheState.MyBO.ShippingTypeId)
        ThePage.PopulateBOProperty(TheState.MyBO, "ShippingDate", objShippingDateTxt)
        ThePage.PopulateBOProperty(TheState.MyBO, "TrackingNumber", objTrackingNumberTxt)
        ThePage.PopulateBOProperty(TheState.MyBO, "ReceivedDate", objReceivedDateTxt)

        If Me.ThePage.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Function

    Private Sub ReturnFromEditing()

        LogisticsGrid.EditIndex = NO_ROW_SELECTED_INDEX

        If (Me.LogisticsGrid.PageCount = 0) Then
            ControlMgr.SetVisibleControl(Me.ThePage, LogisticsGrid, False)
        Else
            ControlMgr.SetVisibleControl(Me.ThePage, LogisticsGrid, True)
        End If

        Me.TheState.IsEditMode = False
        Me.PopulateGrid()
        Me.TheState.PageIndex = LogisticsGrid.PageIndex

    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs)

        Try
            PopulateBOFromForm()

            If (Me.TheState.MyBO.IsDirty) Then
                Me.TheState.MyBO.Save()
                Me.ThePage.MasterPage.MessageController.AddSuccess(MSG_RECORD_SAVED_OK, True)
                Me.TheState.claimShippingDV = Nothing
                Me.TheState.MyBO = Nothing
                Me.ReturnFromEditing()
            Else
                Me.ThePage.MasterPage.MessageController.AddWarning(MSG_RECORD_NOT_SAVED, True)
                Me.ReturnFromEditing()
            End If
        Catch ex As Exception
            Me.ThePage.HandleErrors(ex, Me.ThePage.MasterPage.MessageController)
        End Try

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As EventArgs)
        Try
            With TheState
                .SelectedClaimShippingID = Guid.Empty
                Me.TheState.MyBO = Nothing
                .IsEditMode = False
            End With
            LogisticsGrid.EditIndex = NO_ROW_SELECTED_INDEX
            PopulateGrid()

        Catch ex As Exception
            Me.ThePage.HandleErrors(ex, Me.ThePage.MasterPage.MessageController)
        End Try
    End Sub
End Class