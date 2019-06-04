Imports System.Globalization
Imports System.IO
Imports Assurant.Common.Ftp
Imports Assurant.Common.Zip
Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms


Partial Class DealerInvoiceForm
    Inherits ElitaPlusPage


#Region "Constants"
    Public Const URL As String = "DealerInvoiceDisplay.aspx"
    Public Const PAGETITLE As String = "DEALER_INVOICE"
    Public Const PAGETAB As String = "FINANCE_AUTOMATION"
    'Public Const SUMMARYTITLE As String = "DEALER_INVOICE"
#End Region

#Region "Page Events"
    Private Sub UpdateBreadCrum()
        Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
        Me.MasterPage.UsePageTabTitleInBreadCrum = False
        Me.MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(PAGETAB) + ElitaBase.Sperator + TranslationBase.TranslateLabelOrMessage(PAGETITLE)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.MasterPage.MessageController.Clear()


        Me.UpdateBreadCrum()
        If Not Me.IsPostBack Then
            PopulateDropdowns()
        End If
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub

    Private Sub PopulateDropdowns()
        ' Me.BindCodeNameToListControl(ddlDealer, LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, True, "Code"), , , , False)
        Dim oDealerList = GetDealerListByCompanyForUser()
        Dim dealerTextFunc As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                                                                           Return li.ExtendedCode + " - " + li.Translation + " " + "(" + li.Code + ")"
                                                                       End Function
        ddlDealer.Populate(oDealerList, New PopulateOptions() With
                                           {
                                            .TextFunc = dealerTextFunc
                                           })
        Dim intYear As Integer = DateTime.Today.Year
        For i As Integer = (intYear - 7) To intYear
            ddlAcctPeriodYear.Items.Add(New System.Web.UI.WebControls.ListItem(i.ToString, i.ToString))
        Next
        ddlAcctPeriodYear.SelectedValue = intYear.ToString

        Dim monthName As String
        For month As Integer = 1 To 12
            monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month)
            ddlAcctPeriodMonth.Items.Add(New System.Web.UI.WebControls.ListItem(monthName, month.ToString().PadLeft(2, CChar("0"))))
        Next

        Dim strMonth As String = "0" & DateTime.Today.Month.ToString
        strMonth = strMonth.Substring(strMonth.Length - 2)
        ddlAcctPeriodMonth.SelectedValue = strMonth

    End Sub
    Private Function GetDealerListByCompanyForUser() As Assurant.Elita.CommonConfiguration.DataElements.ListItem()
        Dim Index As Integer
        Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext

        Dim UserCompanies As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies

        Dim oDealerList As New Collections.Generic.List(Of Assurant.Elita.CommonConfiguration.DataElements.ListItem)

        For Index = 0 To UserCompanies.Count - 1
            'UserCompanyList &= ",'" & GuidControl.GuidToHexString(UserCompanyies(Index))
            oListContext.CompanyId = UserCompanies(Index)
            Dim oDealerListForCompany As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerListByCompany", context:=oListContext)
            If oDealerListForCompany.Count > 0 Then
                If Not oDealerList Is Nothing Then
                    oDealerList.AddRange(oDealerListForCompany)
                Else
                    oDealerList = oDealerListForCompany.Clone()
                End If

            End If
        Next

        Return oDealerList.ToArray()

    End Function
#End Region

#Region "Button Handler"
    'Private Sub btnDownLoadXML_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDownLoadXML_WRITE.Click
    '    Try
    '        DownLoadXML()
    '    Catch ex As Threading.ThreadAbortException
    '    Catch ex As Exception
    '        Me.HandleErrors(ex, Me.MasterPage.MessageController)
    '    End Try
    'End Sub
#End Region

#Region "Method Logic"
    Private Sub DownLoadXML()

        Try
            If ddlDealer.SelectedItem Is Nothing Then
                Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.DEALER_IS_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.DEALER_IS_REQUIRED)
            End If
            If ddlAcctPeriodYear.SelectedItem Is Nothing Then
                Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.ACCT_PERIOD_YEAR_IS_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.ACCT_PERIOD_YEAR_IS_REQUIRED)
            End If
            If ddlAcctPeriodMonth.SelectedItem Is Nothing Then
                Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.ACCT_PERIOD_MONTH_IS_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.ACCT_PERIOD_MONTH_IS_REQUIRED)
            End If

            Dim dealerId As Guid = New Guid(ddlDealer.SelectedItem.Value)
            Dim acctPeriodMonth As String = String.Empty
            Dim acctYear As Integer = ddlAcctPeriodYear.SelectedItem.Value
            Dim acctMonth As Integer = ddlAcctPeriodMonth.SelectedItem.Value

            acctPeriodMonth = $"{acctYear.ToString("0000")}{acctMonth.ToString("00")}"
            Dim afaInvoiceData As New AfaInvoiceData(dealerId, acctPeriodMonth)

            If Not afaInvoiceData Is Nothing AndAlso
                Not String.IsNullOrEmpty(afaInvoiceData.DirectoryName) AndAlso
                Not String.IsNullOrEmpty(afaInvoiceData.Filename) Then

                TransferFilesUnixWebServer(afaInvoiceData.Filename, afaInvoiceData.DirectoryName)
            Else
                Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.ACCT_ATTACHMENT_NOT_FOUND, Assurant.ElitaPlus.Common.ErrorCodes.ACCT_ATTACHMENT_NOT_FOUND)
            End If

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try



    End Sub

    Private Sub TransferFilesUnixWebServer(ByVal fileName As String, Optional ByVal directoryName As String = "afa_data")

        Dim objUnixFTP As New sFtp(AppConfig.UnixServer.HostName, AppConfig.UnixServer.FtpDirectory.Replace("/ftp", $"/{directoryName}"), AppConfig.UnixServer.UserId, AppConfig.UnixServer.Password)

        Try

            Dim userPathWebServer As String = MiscUtil.GetUniqueDirectory(AppConfig.UnixServer.InterfaceDirectory, ElitaPlusPrincipal.Current.Identity.Name)
            MiscUtil.CreateFolder(userPathWebServer)

            Dim downloadFileName As String = Path.Combine(userPathWebServer, fileName)
            objUnixFTP.DownloadFile(fileName, downloadFileName)

            Dim downloadResponse As System.Web.HttpResponse
            downloadResponse = HttpContext.Current.Response
            downloadResponse.ClearContent()
            downloadResponse.ClearHeaders()
            downloadResponse.ContentType = "text/csv"
            downloadResponse.AddHeader("Content-Disposition", "attachment; filename=" & downloadFileName)
            downloadResponse.BinaryWrite(File.ReadAllBytes(downloadFileName))
            downloadResponse.Flush()
            downloadResponse.End()
        Finally

        End Try
    End Sub
#End Region
End Class

