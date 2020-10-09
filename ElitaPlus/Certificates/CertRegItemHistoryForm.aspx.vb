Namespace Certificates
    Partial Public Class CertRegItemHistoryForm
        Inherits ElitaPlusSearchPage

#Region "Constants"
        Public Const PAGETITLE As String = "REGISTER_ITEM_HISTORY"
        Public Const PAGETAB As String = "CERTIFICATES"

        Public Const GRID_COL_REGISTERED_ITEM_NAME_IDX As Integer = 0
        Public Const GRID_COL_DEVICE_TYPE_IDX As Integer = 1
        Public Const GRID_COL_ITEM_DESCRIPTION_IDX As Integer = 2
        Public Const GRID_COL_MAKE_IDX As Integer = 3
        Public Const GRID_COL_MODEL_IDX As Integer = 4
        Public Const GRID_COL_PURCHASE_DATE_IDX As Integer = 5
        Public Const GRID_COL_PURCHASE_PRICE_IDX As Integer = 6
        Public Const GRID_COL_SERIAL_NUMBER_IDX As Integer = 7
        Public Const GRID_COL_MODIFIED_DATE_IDX As Integer = 8
        Public Const GRID_COL_ITEM_STATUS_IDX As Integer = 9
        Public Const GRID_COL_CERT_REGITEMHISTORY_ID_IDX As Integer = 10
        'REQ-6002
        Public Const GRID_COL_CERT_REGISTRATION_DATE_IDX As Integer = 11
        Public Const GRID_COL_CERT_RETAIL_PRICE_IDX As Integer = 12
        Public Const GRID_COL_CERT_INDIXID_IDX As Integer = 13
        Public Const GRID_COL_EXPIRATION_DATE_IDX As Integer = 14

        Public Const GRID_TOTAL_COLUMNS As Integer = 4
#End Region

#Region "Page State"
        ' This class keeps the current state for the page.
        Class MyState
            Public MyBO As CertItemHistory
            Public CertItemBO As CertItem
            Public CertRegisteredItemBO As CertRegisteredItem
            Public oCert As Certificate
            Public CertItemId As Guid = Guid.Empty
            Public CertRegisteredItemId As Guid = Guid.Empty
            Public certificateId As Guid = Guid.Empty
            Public searchDV As DataView = Nothing
            Public IsGridVisible As Boolean
            Public bnoRow As Boolean = False
            Public certificateCompanyId As Guid
            Public boChanged As Boolean = False
            Public PageIndex As Integer = 0
        End Class

        Public Sub New()
            MyBase.New(New MyState)
        End Sub


        Protected Shadows ReadOnly Property State() As MyState
            Get
                If NavController.State Is Nothing Then
                    NavController.State = New MyState
                    Me.State.CertRegisteredItemBO = New CertRegisteredItem(CType(NavController.FlowSession(FlowSessionKeys.SESSION_CERT_REGISTERED_ITEM_ID), Guid))
                    Me.State.CertRegisteredItemId = Me.State.CertRegisteredItemBO.Id
                    Me.State.CertItemBO = New CertItem(Me.State.CertRegisteredItemBO.CertItemId)
                    moCertificate = Me.State.CertItemBO.GetCertificate(Me.State.CertRegisteredItemBO.CertId)
                    Me.State.certificateId = moCertificate.Id
                    Me.State.certificateCompanyId = moCertificate.CompanyId
                End If
                Return CType(NavController.State, MyState)
                Return CType(MyBase.State, MyState)
            End Get
        End Property
#End Region

#Region "Page Return Type"
        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public EditingBo As CertItemHistory
            Public HasDataChanged As Boolean
            Public Sub New(LastOp As DetailPageCommand, curEditingBo As CertItemHistory, hasDataChanged As Boolean)
                LastOperation = LastOp
                EditingBo = curEditingBo
                Me.HasDataChanged = hasDataChanged
            End Sub
        End Class
#End Region
#Region "Properties"


        Public ReadOnly Property UserCertificateCtr() As UserControlCertificateInfo_New
            Get
                If moCertificateInfoController Is Nothing Then
                    moCertificateInfoController = CType(FindControl("moCertificateInfoController"), UserControlCertificateInfo_New)
                End If
                Return moCertificateInfoController
            End Get
        End Property

        Public ReadOnly Property GetCompanyCode() As String
            Get
                Dim companyBO As Company = New Company(State.oCert.CompanyId)

                Return companyBO.Code
            End Get

        End Property

        Public Property moCertificate() As Certificate
            Get
                Return State.oCert
            End Get
            Set(Value As Certificate)
                State.oCert = Value

            End Set
        End Property
#End Region

#Region "Page Events"
        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            MasterPage.MessageController.Clear_Hide()
            Try
                MasterPage.UsePageTabTitleInBreadCrum = False
                MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("Certificates")
                If Not IsPostBack Then
                    UpdateBreadCrum()
                    MenuEnabled = False
                    TranslateGridHeader(Grid)
                    TranslateGridControls(Grid)
                    moCertificateInfoController = UserCertificateCtr
                    moCertificateInfoController.InitController(State.certificateId, , GetCompanyCode)
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    PopulateGrid()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub

        Private Sub UpdateBreadCrum()
            MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator &
                TranslationBase.TranslateLabelOrMessage("Item") & ElitaBase.Sperator &
                TranslationBase.TranslateLabelOrMessage("register_item_history")
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("register_item_history")
        End Sub
#End Region

#Region "Controlling Logic"

        Private Sub PopulateGrid()
            State.searchDV = CertRegisteredItemHist.getHistoryList(State.CertRegisteredItemId)

            Grid.AutoGenerateColumns = False
            SetPageAndSelectedIndexFromGuid(State.searchDV, Guid.Empty, Grid, State.PageIndex)
            SortAndBindGrid()

        End Sub

        Private Sub SortAndBindGrid()
            State.PageIndex = Grid.PageIndex
            If (State.searchDV.Count = 0) Then

                State.bnoRow = True
                CreateHeaderForEmptyGrid(Grid, String.Empty)
            Else
                State.bnoRow = False
                Grid.Enabled = True
                Grid.DataSource = State.searchDV
                Grid.DataBind()
            End If
            If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

            ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

            Session("recCount") = State.searchDV.Count

            If State.searchDV.Count > 0 Then
                State.bnoRow = False
                If Grid.Visible Then
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            Else
                State.bnoRow = True
                If Grid.Visible Then
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
        End Sub

#End Region

#Region "GridView Related"

        'The Binding Logic is here
        Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim sModified_Date As String

            If dvRow IsNot Nothing AndAlso Not State.bnoRow Then
                If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then

                    e.Row.Cells(GRID_COL_REGISTERED_ITEM_NAME_IDX).Text = dvRow(CertRegisteredItemHist.CertRegItemHistSearchDV.COL_REGISTERED_ITEM_NAME).ToString
                    e.Row.Cells(GRID_COL_DEVICE_TYPE_IDX).Text = dvRow(CertRegisteredItemHist.CertRegItemHistSearchDV.COL_DEVICE_TYPE).ToString
                    e.Row.Cells(GRID_COL_ITEM_DESCRIPTION_IDX).Text = dvRow(CertRegisteredItemHist.CertRegItemHistSearchDV.COL_ITEM_DESCRIPTION).ToString
                    e.Row.Cells(GRID_COL_MAKE_IDX).Text = dvRow(CertRegisteredItemHist.CertRegItemHistSearchDV.COL_MAKE).ToString
                    e.Row.Cells(GRID_COL_MODEL_IDX).Text = dvRow(CertRegisteredItemHist.CertRegItemHistSearchDV.COL_MODEL).ToString
                    e.Row.Cells(GRID_COL_PURCHASE_DATE_IDX).Text = GetDateFormattedString(CType(dvRow(CertRegisteredItemHist.CertRegItemHistSearchDV.COL_PURCHASE_DATE).ToString, Date))
                    e.Row.Cells(GRID_COL_PURCHASE_PRICE_IDX).Text = dvRow(CertRegisteredItemHist.CertRegItemHistSearchDV.COL_PURCHASE_PRICE).ToString
                    e.Row.Cells(GRID_COL_SERIAL_NUMBER_IDX).Text = dvRow(CertRegisteredItemHist.CertRegItemHistSearchDV.COL_SERIAL_NUMBER).ToString
                    e.Row.Cells(GRID_COL_MODIFIED_DATE_IDX).Text = dvRow(CertRegisteredItemHist.CertRegItemHistSearchDV.COL_MODIFIED_DATE).ToString
                    e.Row.Cells(GRID_COL_ITEM_STATUS_IDX).Text = dvRow(CertRegisteredItemHist.CertRegItemHistSearchDV.COL_ITEM_REGISTRATION_STATUS).ToString

                    sModified_Date = dvRow(CertRegisteredItemHist.CertRegItemHistSearchDV.COL_MODIFIED_DATE).ToString & ""

                    If sModified_Date = "" Then
                        e.Row.Cells(GRID_COL_MODIFIED_DATE_IDX).Text = ""
                    Else
                        e.Row.Cells(GRID_COL_MODIFIED_DATE_IDX).Text = GetDateFormattedString(CType(sModified_Date, Date))
                    End If

                    e.Row.Cells(GRID_COL_CERT_REGITEMHISTORY_ID_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(CertRegisteredItemHist.CertRegItemHistSearchDV.COL_CERT_REGISTERED_ITEM_HIST_ID), Byte()))

                    'REQ-6002
                    If (dvRow(CertItem.CertRegItemSearchDV.COL_REGISTRATION_DATE) IsNot DBNull.Value) Then
                        e.Row.Cells(GRID_COL_CERT_REGISTRATION_DATE_IDX).Text = GetDateFormattedString(CType(dvRow(CertRegisteredItemHist.CertRegItemHistSearchDV.COL_ITEM_REGISTRATION_DATE), Date))
                    End If
                    If (dvRow(CertItem.CertRegItemSearchDV.COL_RETAIL_PRICE) IsNot DBNull.Value) Then
                        e.Row.Cells(GRID_COL_CERT_RETAIL_PRICE_IDX).Text = GetAmountFormattedString(CType(dvRow(CertRegisteredItemHist.CertRegItemHistSearchDV.COL_ITEM_RETAIL_PRICE), Decimal))
                    End If
                    If (dvRow(CertItem.CertRegItemSearchDV.COL_INDIXID) IsNot DBNull.Value) Then
                        e.Row.Cells(GRID_COL_CERT_INDIXID_IDX).Text = dvRow(CertRegisteredItemHist.CertRegItemHistSearchDV.COL_ITEM_INDIXID)
                    End If
                    If (dvRow(CertItem.CertRegItemSearchDV.COL_EXPIRATION_DATE) IsNot DBNull.Value) Then
                        e.Row.Cells(GRID_COL_EXPIRATION_DATE_IDX).Text = GetDateFormattedString(CType(dvRow(CertRegisteredItemHist.CertRegItemHistSearchDV.COL_EXPIRATION_DATE), Date))
                    End If
                End If
            End If
        End Sub

        Public Sub ItemCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
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


#End Region


#Region "Button Handlers"
        Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
            Try
                NavController.Navigate(Me, "back")
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region


    End Class
End Namespace
