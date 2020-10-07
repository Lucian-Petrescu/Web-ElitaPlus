Namespace Certificates
    Partial Public Class CertItemHistoryForm
        Inherits ElitaPlusSearchPage

#Region "Constants"
        Public Const PAGETITLE As String = "ITEM_HISTORY"
        Public Const PAGETAB As String = "CERTIFICATES"
        Public Const GRID_COL_IMEI_NUMBER_IDX As Integer = 0
        Public Const GRID_COL_SERIAL_NUMBER_IDX As Integer = 1
        Public Const GRID_COL_MANUFACTURER_IDX As Integer = 2
        Public Const GRID_COL_MODEL_IDX As Integer = 3
        Public Const GRID_COL_SKU_NUMBER_IDX As Integer = 4
        Public Const GRID_COL_RISK_TYPE_IDX As Integer = 5
        Public Const GRID_COL_MODIFIED_DATE_IDX As Integer = 6
        Public Const GRID_COL_CERT_ITEMHISTORY_ID_IDX As Integer = 7

        Public Const GRID_TOTAL_COLUMNS As Integer = 4
#End Region

#Region "Page State"
        ' This class keeps the current state for the page.
        Class MyState
            Public MyBO As CertItemHistory
            Public CertItemBO As CertItem
            Public oCert As Certificate
            Public CertItemId As Guid = Guid.Empty
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
                    Me.State.CertItemBO = New CertItem(CType(NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_ITEM_ID), Guid))
                    Me.State.CertItemId = Me.State.CertItemBO.Id

                    moCertificate = Me.State.CertItemBO.GetCertificate(Me.State.CertItemBO.CertId)
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
                    moCertificateInfoController.InitController(State.oCert.Id, , GetCompanyCode)
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
                TranslationBase.TranslateLabelOrMessage("item_history")
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("item_history")
        End Sub
#End Region

#Region "Controlling Logic"

        Private Sub PopulateGrid()
            State.searchDV = CertItemHistory.getHistoryList(State.certificateId, State.CertItemId)

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

                    e.Row.Cells(GRID_COL_IMEI_NUMBER_IDX).Text = dvRow(CertItemHistory.CertItemHistSearchDV.COL_IMEI_NUMBER).ToString
                    e.Row.Cells(GRID_COL_SERIAL_NUMBER_IDX).Text = dvRow(CertItemHistory.CertItemHistSearchDV.COL_SERIAL_NUMBER).ToString
                    e.Row.Cells(GRID_COL_MANUFACTURER_IDX).Text = dvRow(CertItemHistory.CertItemHistSearchDV.COL_MANUFACTURER).ToString
                    e.Row.Cells(GRID_COL_MODEL_IDX).Text = dvRow(CertItemHistory.CertItemHistSearchDV.COL_MODEL).ToString
                    e.Row.Cells(GRID_COL_SKU_NUMBER_IDX).Text = dvRow(CertItemHistory.CertItemHistSearchDV.COL_SKU_NUMBER).ToString
                    e.Row.Cells(GRID_COL_RISK_TYPE_IDX).Text = dvRow(CertItemHistory.CertItemHistSearchDV.COL_RISK_TYPE).ToString

                    sModified_Date = dvRow(CertItemHistory.CertItemHistSearchDV.COL_ITEM_MODIFIED_DATE).ToString & ""

                    If sModified_Date = "" Then
                        e.Row.Cells(GRID_COL_MODIFIED_DATE_IDX).Text = ""
                    Else
                        e.Row.Cells(GRID_COL_MODIFIED_DATE_IDX).Text = GetDateFormattedString(CType(sModified_Date, Date))
                    End If

                    e.Row.Cells(GRID_COL_CERT_ITEMHISTORY_ID_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(CertItemHistory.CertItemHistSearchDV.COL_CERT_ITEM_HIST_ID), Byte()))
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
