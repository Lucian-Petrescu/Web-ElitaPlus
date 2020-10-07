Imports System.Text.RegularExpressions
Imports System.Security.Cryptography.X509Certificates
Imports Assurant.Common.Zip.aZip
Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.Common.Ftp
Imports Assurant.ElitaPlus.Common.MiscUtil
Imports Assurant.ElitaPlus.ElitaPlusWebApp.DownLoad
Imports System.IO
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Imports System.Threading
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Public Class AuditListForm
    Inherits ElitaPlusSearchPage

#Region "Constants"
    Public Const SHOW_CERTIFICATE_CONTENT_COMMAND As String = "ShowCertificateContent"
    Private Const ADMIN As String = "Admin"
    Private Const AUDITS As String = "Audits" 'Page Title

    Private Const AUDITS_TABLE_NAME As String = "ELP_AUDIT_SECURITY_LOGS"
    Private Const SEARCH_EXCEPTION As String = ""

    Public Const GRID_COL_CERTIFCATE_IMAGE_BUTTON_CTRL As String = "btnCERTIFICATE"
    Public Const GRID_COL_CERTIFCATE_IMAGE_BUTTON_IDX As Integer = 0

#End Region

#Region "Page State"

    Class MyState

        Public AuditSource As String
        Public UserName As String
        Public AuditSecurityTypeCode As String
        Public IPAddress As String
        Public AuditBeginDate As String
        Public AuditEndDate As String
        Public AuditLogsSearchDV As AuditSecurityLogs.AuditLogsSearchDV

        Public SortExpression As String = "AUDIT_DATE"

        Public HasDataChanged As Boolean
        Public IsGridVisible As Boolean
        Public SelectedPageSize As Integer
        Public SearchClick As Boolean = False
        Public bnoRow As Boolean = False
        Public PageIndex As Integer = 0
        Public PageSize As Integer = 30

        Public Caller As String

        Sub New()
        End Sub
    End Class

    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            Return CType(MyBase.State, MyState)
        End Get
    End Property

#End Region

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        MasterPage.MessageController.Clear()
        Form.DefaultButton = btnSearch.UniqueID
        MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(ADMIN)

        Try
            If Not IsPostBack Then
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                'RestoreGuiState()
                SetQuerystringValues()

                ' Populate the header and bredcrumb
                MasterPage.UsePageTabTitleInBreadCrum = False
                UpdateBreadCrum()
                TranslateGridHeader(Grid)

                PopulateDropDowns()

                If State.IsGridVisible Then
                    PopulateGrid()
                End If
                cboPageSize.SelectedValue = State.PageSize.ToString()

                AddCalendarwithTime(BtnAuditBeginDate, txtAuditBeginDate)
                AddCalendarwithTime(BtnAuditEndDate, txtAuditEndDate)
                PopulateDefaultDates()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)

    End Sub

    Private Sub SetQuerystringValues()
        Try
            If Request.QueryString("CALLER") IsNot Nothing Then
                State.Caller = Request.QueryString("CALLER")
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
        Try

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub UpdateBreadCrum()
        MasterPage.UsePageTabTitleInBreadCrum = False
        MasterPage.DisplayRequiredFieldNote = False

        Dim strTranslation As String = String.Empty
        If State.Caller = "APS" Then
            strTranslation = TranslationBase.TranslateLabelOrMessage(AUDITS)
            MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & strTranslation
            MasterPage.PageTitle = strTranslation
        Else
            strTranslation = TranslationBase.TranslateLabelOrMessage(AUDITS)
            MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & strTranslation
            MasterPage.PageTitle = strTranslation
        End If

        MasterPage.MessageController.Clear()

    End Sub

    Sub PopulateDropDowns()
        Try
            'Dim AuditSourceDV As DataView = LookupListNew.GetAuditSourceList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            'Me.BindListControlToDataView(Me.ddlAuditSource, AuditSourceDV, "DESCRIPTION", , True)

            Dim auditList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="AUDIT_SOURCE", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
            ddlAuditSource.Populate(auditList, New PopulateOptions() With
                {
                    .AddBlankItem = True
                })

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub PopulateDefaultDates()
        'Default begin Date = Today – 1 Month, Default End Date = Today.
        Dim t As Date = Date.Now.AddMonths(-1).AddDays(1)
        txtAuditBeginDate.Text = GetLongDate12FormattedString(t)
        txtAuditEndDate.Text = GetLongDate12FormattedString(Date.Now)
    End Sub

    Public Function IsIpAddressValid(addrString As String) As Boolean
        If txtIPAddress.Text.Trim <> String.Empty Then
            Dim regex As Regex = New Regex("\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b")
            Dim match As Match = regex.Match(addrString)
            Return match.Success
        Else
            Return True
        End If

    End Function


    Private Sub btnSearch_Click(sender As Object, e As System.EventArgs) Handles btnSearch.Click
        Try


            State.PageIndex = 0
            State.IsGridVisible = True
            State.SearchClick = True
            State.AuditLogsSearchDV = Nothing

            PopulateGrid()

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#Region "Clear Button Related"
    Private Sub btnClearSearch_Click(sender As Object, e As System.EventArgs) Handles btnClearSearch.Click
        Try
            ' Clear all search options typed or selected by the user
            ClearAllSearchOptions()

            ' Update the Bo state properties with the new value
            ClearStateValues()

            ' Me.SetStateProperties()

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub ClearStateValues()
        Try
            'clear State
            State.AuditSecurityTypeCode = String.Empty
            State.AuditSource = String.Empty
            State.IPAddress = String.Empty
            State.AuditSource = String.Empty
            State.UserName = String.Empty
            State.AuditBeginDate = String.Empty
            State.AuditEndDate = String.Empty
            PopulateDefaultDates()

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub ClearAllSearchOptions()
        txtAuditSecurityTypeCode.Text = String.Empty
        ddlAuditSource.SelectedIndex = 0
        txtIPAddress.Text = String.Empty
        txtUserName.Text = String.Empty
        txtAuditBeginDate.Text = String.Empty

    End Sub
#End Region

#Region " GridView Related "
    Public Sub PopulateGrid()
        Try
            'Notification dates
            If txtAuditBeginDate.Text.Trim() <> String.Empty Then
                GUIException.ValidateDate(lblDateRange1, txtAuditBeginDate.Text)
            Else
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_BEGIN_AND_END_DATE_REQUIRED_ERR)
            End If

            If txtAuditEndDate.Text.Trim() <> String.Empty Then
                GUIException.ValidateDate(lblDateRange1, txtAuditEndDate.Text)

                If DateHelper.GetDateValue(txtAuditBeginDate.Text) > DateHelper.GetDateValue(txtAuditEndDate.Text) Then
                    ElitaPlusPage.SetLabelError(lblDateRange1)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_BEGIN_END_DATE_ERR)
                End If
            Else
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_BEGIN_AND_END_DATE_REQUIRED_ERR)
            End If

            '("MM-dd-yyyy HH:mm:ss")
            ' Dim TheDate As DateTime = DateTime.Parse("January 01 2011")

            Dim ts As TimeSpan = (DateHelper.GetDateValue(txtAuditEndDate.Text.Trim()) - DateHelper.GetDateValue(txtAuditBeginDate.Text.Trim()))
            If ts.Days > 30 Then
                Dim Errs() As ValidationError = {New ValidationError("AUDIT_DATES_NOT_BEYOND_30_DAYS", GetType(ApsPublishingLog), Nothing, "Search", Nothing)}
                ElitaPlusPage.SetLabelError(lblDateRange1)
                Throw New BOValidationException(Errs, GetType(ApsPublishingLog).FullName)
            End If

            State.AuditBeginDate = DateHelper.GetDateValue(txtAuditBeginDate.Text)
            State.AuditEndDate = DateHelper.GetDateValue(txtAuditEndDate.Text)

            If ddlAuditSource.SelectedIndex = 0 Then
                State.AuditSource = ""
            Else
                State.AuditSource = "AUDIT_SOURCE-" & LookupListNew.GetCodeFromId(LookupListNew.LK_AUDIT_SOURCE, ElitaPlusPage.GetSelectedItem(ddlAuditSource))

            End If
            State.AuditSecurityTypeCode = txtAuditSecurityTypeCode.Text

            If IsIpAddressValid(txtIPAddress.Text) Then
                State.IPAddress = txtIPAddress.Text
            Else
                State.IPAddress = ""
                Dim Errs() As ValidationError = {New ValidationError("INVALID_IP_ADDRESS", GetType(ApsPublishingLog), Nothing, "Search", Nothing)}
                ElitaPlusPage.SetLabelError(lblIPAddress)
                Throw New BOValidationException(Errs, GetType(ApsPublishingLog).FullName)
            End If

            State.UserName = txtUserName.Text

            lblDateRange1.ForeColor = lblAuditSource.ForeColor
            lblIPAddress.ForeColor = lblAuditSource.ForeColor

            If (State.AuditLogsSearchDV Is Nothing) Then

                State.AuditLogsSearchDV = AuditSecurityLogs.GetAuditLogsList(State.AuditBeginDate,
                                                                                    State.AuditEndDate,
                                                                                    State.AuditSource,
                                                                                    State.AuditSecurityTypeCode,
                                                                                    State.IPAddress,
                                                                                    State.UserName)
            End If

            If State.SearchClick Then
                ValidSearchResultCountNew(State.AuditLogsSearchDV.Count, True)
                State.SearchClick = False
            End If
            State.AuditLogsSearchDV.Sort = State.SortExpression
            SortAndBindGrid()

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub SortAndBindGrid()
        State.bnoRow = False
        Grid.AutoGenerateColumns = False
        Grid.PageSize = State.PageSize

        State.PageIndex = Grid.PageIndex
        Grid.DataSource = State.AuditLogsSearchDV
        HighLightSortColumn(Grid, State.SortExpression)
        Grid.DataBind()

        ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)
        ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

        Session("recCount") = State.AuditLogsSearchDV.Count
        lblRecordCount.Text = State.AuditLogsSearchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
    End Sub

    Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
        Try
            If State.SortExpression.StartsWith(e.SortExpression) Then
                If State.SortExpression.EndsWith(" DESC") Then
                    State.SortExpression = e.SortExpression
                Else
                    State.SortExpression &= " DESC"
                End If
            Else
                State.SortExpression = e.SortExpression
            End If
            State.PageIndex = 0
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Private Sub cboPageSize_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            State.SelectedPageSize = State.PageSize
            'Me.State.PageIndex = NewCurrentPageIndex(Grid, State.SearchDV.Count, State.PageSize)
            Grid.PageIndex = State.PageIndex
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanged(sender As Object, e As System.EventArgs) Handles Grid.PageIndexChanged
        Try
            State.PageIndex = Grid.PageIndex

            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Grid.PageIndex = e.NewPageIndex
            State.PageIndex = Grid.PageIndex
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        'Dim itemType As ListItemType = CType(e.Row.ItemType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
        Dim btnViewCertificate As ImageButton

        Try
            If e.Row.RowType = DataControlRowType.DataRow OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                If (e.Row.Cells(GRID_COL_CERTIFCATE_IMAGE_BUTTON_IDX).FindControl(GRID_COL_CERTIFCATE_IMAGE_BUTTON_CTRL) IsNot Nothing) Then

                    '  Dim rowIndex As Integer = TryCast(TryCast(sender, Button).NamingContainer, GridViewRow).RowIndex

                    'Get the value of column from the DataKeys.
                    '                    Dim objCertificate As Object = Grid.DataKeys(0).Values(0)
                    If dvRow(AuditSecurityLogs.AuditLogsSearchDV.COL_NAME_X509_CERTIFICATE) Is System.DBNull.Value Then
                        btnViewCertificate = CType(e.Row.Cells(GRID_COL_CERTIFCATE_IMAGE_BUTTON_IDX).FindControl(GRID_COL_CERTIFCATE_IMAGE_BUTTON_CTRL), ImageButton)
                        btnViewCertificate.Visible = False
                    End If


                End If



            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
        Try
            If e.CommandName = SHOW_CERTIFICATE_CONTENT_COMMAND Then
                Dim row As GridViewRow = CType(CType(e.CommandSource, Control).Parent.Parent, GridViewRow)
                Dim RowInd As Integer = row.RowIndex
                Try
                    'PopulateCertificateDetail(HttpUtility.HtmlEncode(Grid.DataKeys(RowInd).Values(0).ToString()))
                    PopulateCertificateDetail(Grid.DataKeys(RowInd).Values(0).ToString())
                    mdlPopup.Show()
                Catch ex As Exception
                    txtCertificateContent.Text = "Certificate data is invalid"
                    mdlPopup.Show()
                End Try

            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Sub PopulateCertificateDetail(rawData As String)

        Dim x509 As New X509Certificate2()
        x509.Import(System.Text.Encoding.Unicode.GetBytes(rawData))
        txtCertificateContent.Text = "        Subject:" & x509.Subject & Environment.NewLine &
                                     "         Issuer:" & x509.Issuer & Environment.NewLine &
                                     "        Version:" & x509.Version & Environment.NewLine &
                                     "      NotBefore:" & x509.NotBefore & Environment.NewLine &
                                     "       NotAfter:" & x509.NotAfter & Environment.NewLine &
                                     "     Thumbprint:" & x509.Thumbprint & Environment.NewLine &
                                     "   SerialNumber:" & x509.SerialNumber & Environment.NewLine &
                                     "   FriendlyName:" & x509.FriendlyName & Environment.NewLine

    End Sub

#End Region

    ''' <summary>
    ''' Modal pupup cancel button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' 
    Private Sub btnNewItemCancel_Click(sender As Object, e As System.EventArgs) Handles btnCERTIFICATEPopupCancel.Click
        Try
            mdlPopup.Hide()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

End Class