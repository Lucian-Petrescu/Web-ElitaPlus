Imports System.Threading

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


        Protected Shadows ReadOnly Property State As MyState
            Get
                If Me.NavController.State Is Nothing Then
                    Me.NavController.State = New MyState
                    Me.State.CertItemBO = New CertItem(CType(Me.NavController.FlowSession(FlowSessionKeys.SESSION_CERTIFICATE_ITEM_ID), Guid))
                    Me.State.CertItemId = Me.State.CertItemBO.Id

                    moCertificate = Me.State.CertItemBO.GetCertificate(Me.State.CertItemBO.CertId)
                    Me.State.certificateId = moCertificate.Id
                    Me.State.certificateCompanyId = moCertificate.CompanyId
                End If
                Return CType(Me.NavController.State, MyState)
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
                Me.LastOperation = LastOp
                Me.EditingBo = curEditingBo
                Me.HasDataChanged = hasDataChanged
            End Sub
        End Class
#End Region
#Region "Properties"


        Public ReadOnly Property UserCertificateCtr As UserControlCertificateInfo_New
            Get
                If moCertificateInfoController Is Nothing Then
                    moCertificateInfoController = CType(FindControl("moCertificateInfoController"), UserControlCertificateInfo_New)
                End If
                Return moCertificateInfoController
            End Get
        End Property

        Public ReadOnly Property GetCompanyCode As String
            Get
                Dim companyBO As Company = New Company(Me.State.oCert.CompanyId)

                Return companyBO.Code
            End Get

        End Property

        Public Property moCertificate As Certificate
            Get
                Return Me.State.oCert
            End Get
            Set
                Me.State.oCert = Value

            End Set
        End Property
#End Region

#Region "Page Events"
        Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
            Me.MasterPage.MessageController.Clear_Hide()
            Try
                Me.MasterPage.UsePageTabTitleInBreadCrum = False
                Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("Certificates")
                If Not Me.IsPostBack Then
                    UpdateBreadCrum()
                    Me.MenuEnabled = False
                    Me.TranslateGridHeader(Me.Grid)
                    Me.TranslateGridControls(Me.Grid)
                    moCertificateInfoController = Me.UserCertificateCtr
                    moCertificateInfoController.InitController(Me.State.oCert.Id, , GetCompanyCode)
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    PopulateGrid()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)
        End Sub

        Private Sub UpdateBreadCrum()
            Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator &
                TranslationBase.TranslateLabelOrMessage("Item") & ElitaBase.Sperator &
                TranslationBase.TranslateLabelOrMessage("item_history")
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("item_history")
        End Sub
#End Region

#Region "Controlling Logic"

        Private Sub PopulateGrid()
            Me.State.searchDV = CertItemHistory.getHistoryList(Me.State.certificateId, Me.State.CertItemId)

            Me.Grid.AutoGenerateColumns = False
            SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Guid.Empty, Me.Grid, Me.State.PageIndex)
            Me.SortAndBindGrid()

        End Sub

        Private Sub SortAndBindGrid()
            Me.State.PageIndex = Me.Grid.PageIndex
            If (Me.State.searchDV.Count = 0) Then

                Me.State.bnoRow = True
                CreateHeaderForEmptyGrid(Grid, String.Empty)
            Else
                Me.State.bnoRow = False
                Me.Grid.Enabled = True
                Me.Grid.DataSource = Me.State.searchDV
                Me.Grid.DataBind()
            End If
            If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

            ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)

            Session("recCount") = Me.State.searchDV.Count

            If Me.State.searchDV.Count > 0 Then
                Me.State.bnoRow = False
                If Me.Grid.Visible Then
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            Else
                Me.State.bnoRow = True
                If Me.Grid.Visible Then
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
        End Sub

#End Region

#Region "GridView Related"

        'The Binding Logic is here
        Private Sub Grid_ItemDataBound(sender As Object, e As GridViewRowEventArgs) Handles Grid.RowDataBound
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim sModified_Date As String

            If dvRow IsNot Nothing AndAlso Not Me.State.bnoRow Then
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

        Public Sub ItemCreated(sender As Object, e As GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub Grid_PageSizeChanged(source As Object, e As EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanged(source As Object, e As GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                Me.State.PageIndex = e.NewPageIndex
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub


#End Region


#Region "Button Handlers"
        Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
            Try
                Me.NavController.Navigate(Me, "back")
            Catch ex As ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
#End Region


    End Class
End Namespace
