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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.MasterPage.MessageController.Clear()
        Me.Form.DefaultButton = btnSearch.UniqueID
        Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(ADMIN)

        Try
            If Not Me.IsPostBack Then
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                'RestoreGuiState()
                SetQuerystringValues()

                ' Populate the header and bredcrumb
                Me.MasterPage.UsePageTabTitleInBreadCrum = False
                UpdateBreadCrum()
                TranslateGridHeader(Grid)

                PopulateDropDowns()

                If Me.State.IsGridVisible Then
                    Me.PopulateGrid()
                End If
                cboPageSize.SelectedValue = Me.State.PageSize.ToString()

                Me.AddCalendarwithTime(Me.BtnAuditBeginDate, Me.txtAuditBeginDate)
                Me.AddCalendarwithTime(Me.BtnAuditEndDate, Me.txtAuditEndDate)
                PopulateDefaultDates()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)

    End Sub

    Private Sub SetQuerystringValues()
        Try
            If Not Request.QueryString("CALLER") Is Nothing Then
                Me.State.Caller = Request.QueryString("CALLER")
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        Try

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub UpdateBreadCrum()
        Me.MasterPage.UsePageTabTitleInBreadCrum = False
        Me.MasterPage.DisplayRequiredFieldNote = False

        Dim strTranslation As String = String.Empty
        If Me.State.Caller = "APS" Then
            strTranslation = TranslationBase.TranslateLabelOrMessage(AUDITS)
            Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & strTranslation
            Me.MasterPage.PageTitle = strTranslation
        Else
            strTranslation = TranslationBase.TranslateLabelOrMessage(AUDITS)
            Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & strTranslation
            Me.MasterPage.PageTitle = strTranslation
        End If

        Me.MasterPage.MessageController.Clear()

    End Sub

    Sub PopulateDropDowns()
        Try
            'Dim AuditSourceDV As DataView = LookupListNew.GetAuditSourceList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            'Me.BindListControlToDataView(Me.ddlAuditSource, AuditSourceDV, "DESCRIPTION", , True)

            Dim auditList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="AUDIT_SOURCE", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
            Me.ddlAuditSource.Populate(auditList, New PopulateOptions() With
                {
                    .AddBlankItem = True
                })

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub PopulateDefaultDates()
        'Default begin Date = Today – 1 Month, Default End Date = Today.
        Dim t As Date = Date.Now.AddMonths(-1).AddDays(1)
        Me.txtAuditBeginDate.Text = GetLongDate12FormattedString(t)
        Me.txtAuditEndDate.Text = GetLongDate12FormattedString(Date.Now)
    End Sub

    Public Function IsIpAddressValid(ByVal addrString As String) As Boolean
        If Me.txtIPAddress.Text.Trim <> String.Empty Then
            Dim regex As Regex = New Regex("\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b")
            Dim match As Match = regex.Match(addrString)
            Return match.Success
        Else
            Return True
        End If

    End Function


    Private Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try


            Me.State.PageIndex = 0
            Me.State.IsGridVisible = True
            Me.State.SearchClick = True
            Me.State.AuditLogsSearchDV = Nothing

            Me.PopulateGrid()

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#Region "Clear Button Related"
    Private Sub btnClearSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
        Try
            ' Clear all search options typed or selected by the user
            Me.ClearAllSearchOptions()

            ' Update the Bo state properties with the new value
            Me.ClearStateValues()

            ' Me.SetStateProperties()

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub ClearStateValues()
        Try
            'clear State
            Me.State.AuditSecurityTypeCode = String.Empty
            Me.State.AuditSource = String.Empty
            Me.State.IPAddress = String.Empty
            Me.State.AuditSource = String.Empty
            Me.State.UserName = String.Empty
            Me.State.AuditBeginDate = String.Empty
            Me.State.AuditEndDate = String.Empty
            PopulateDefaultDates()

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub ClearAllSearchOptions()
        Me.txtAuditSecurityTypeCode.Text = String.Empty
        Me.ddlAuditSource.SelectedIndex = 0
        Me.txtIPAddress.Text = String.Empty
        Me.txtUserName.Text = String.Empty
        Me.txtAuditBeginDate.Text = String.Empty

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

            Me.State.AuditBeginDate = DateHelper.GetDateValue(Me.txtAuditBeginDate.Text)
            Me.State.AuditEndDate = DateHelper.GetDateValue(Me.txtAuditEndDate.Text)

            If Me.ddlAuditSource.SelectedIndex = 0 Then
                Me.State.AuditSource = ""
            Else
                Me.State.AuditSource = "AUDIT_SOURCE-" & LookupListNew.GetCodeFromId(LookupListNew.LK_AUDIT_SOURCE, ElitaPlusPage.GetSelectedItem(Me.ddlAuditSource))

            End If
            Me.State.AuditSecurityTypeCode = Me.txtAuditSecurityTypeCode.Text

            If IsIpAddressValid(Me.txtIPAddress.Text) Then
                Me.State.IPAddress = Me.txtIPAddress.Text
            Else
                Me.State.IPAddress = ""
                Dim Errs() As ValidationError = {New ValidationError("INVALID_IP_ADDRESS", GetType(ApsPublishingLog), Nothing, "Search", Nothing)}
                ElitaPlusPage.SetLabelError(lblIPAddress)
                Throw New BOValidationException(Errs, GetType(ApsPublishingLog).FullName)
            End If

            Me.State.UserName = Me.txtUserName.Text

            Me.lblDateRange1.ForeColor = Me.lblAuditSource.ForeColor
            Me.lblIPAddress.ForeColor = Me.lblAuditSource.ForeColor

            If (Me.State.AuditLogsSearchDV Is Nothing) Then

                Me.State.AuditLogsSearchDV = AuditSecurityLogs.GetAuditLogsList(Me.State.AuditBeginDate,
                                                                                    Me.State.AuditEndDate,
                                                                                    Me.State.AuditSource,
                                                                                    Me.State.AuditSecurityTypeCode,
                                                                                    Me.State.IPAddress,
                                                                                    Me.State.UserName)
            End If

            If Me.State.SearchClick Then
                Me.ValidSearchResultCountNew(Me.State.AuditLogsSearchDV.Count, True)
                Me.State.SearchClick = False
            End If
            Me.State.AuditLogsSearchDV.Sort = Me.State.SortExpression
            Me.SortAndBindGrid()

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub SortAndBindGrid()
        Me.State.bnoRow = False
        Me.Grid.AutoGenerateColumns = False
        Me.Grid.PageSize = Me.State.PageSize

        Me.State.PageIndex = Me.Grid.PageIndex
        Me.Grid.DataSource = Me.State.AuditLogsSearchDV
        HighLightSortColumn(Grid, Me.State.SortExpression)
        Me.Grid.DataBind()

        ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)
        ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)

        Session("recCount") = Me.State.AuditLogsSearchDV.Count
        Me.lblRecordCount.Text = Me.State.AuditLogsSearchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
    End Sub

    Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
        Try
            If Me.State.SortExpression.StartsWith(e.SortExpression) Then
                If Me.State.SortExpression.EndsWith(" DESC") Then
                    Me.State.SortExpression = e.SortExpression
                Else
                    Me.State.SortExpression &= " DESC"
                End If
            Else
                Me.State.SortExpression = e.SortExpression
            End If
            Me.State.PageIndex = 0
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub cboPageSize_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Me.State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            Me.State.SelectedPageSize = Me.State.PageSize
            'Me.State.PageIndex = NewCurrentPageIndex(Grid, State.SearchDV.Count, State.PageSize)
            Me.Grid.PageIndex = Me.State.PageIndex
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.PageIndexChanged
        Try
            Me.State.PageIndex = Grid.PageIndex

            PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Grid.PageIndex = e.NewPageIndex
            State.PageIndex = Grid.PageIndex
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        'Dim itemType As ListItemType = CType(e.Row.ItemType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
        Dim btnViewCertificate As ImageButton

        Try
            If e.Row.RowType = DataControlRowType.DataRow OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                If (Not e.Row.Cells(Me.GRID_COL_CERTIFCATE_IMAGE_BUTTON_IDX).FindControl(GRID_COL_CERTIFCATE_IMAGE_BUTTON_CTRL) Is Nothing) Then

                    '  Dim rowIndex As Integer = TryCast(TryCast(sender, Button).NamingContainer, GridViewRow).RowIndex

                    'Get the value of column from the DataKeys.
                    '                    Dim objCertificate As Object = Grid.DataKeys(0).Values(0)
                    If dvRow(AuditSecurityLogs.AuditLogsSearchDV.COL_NAME_X509_CERTIFICATE) Is System.DBNull.Value Then
                        btnViewCertificate = CType(e.Row.Cells(Me.GRID_COL_CERTIFCATE_IMAGE_BUTTON_IDX).FindControl(GRID_COL_CERTIFCATE_IMAGE_BUTTON_CTRL), ImageButton)
                        btnViewCertificate.Visible = False
                    End If


                End If



            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
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
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Sub PopulateCertificateDetail(ByVal rawData As String)

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
    Private Sub btnNewItemCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCERTIFICATEPopupCancel.Click
        Try
            mdlPopup.Hide()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

End Class