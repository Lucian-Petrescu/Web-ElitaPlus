
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Text
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms

Namespace Reports
    Partial Class CertificatesBillingBrazilReportForm
        Inherits ElitaPlusPage

#Region "Constants"

        Private Const PAGETITLE As String = "CO_BILLING_EXTRACT"
        Public Const PAGETAB As String = "REPORTS"

#End Region

#Region "Properties"

#End Region

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <DebuggerStepThrough()>
        Private Sub InitializeComponent()
        End Sub

        Protected WithEvents IsProgressVisible As HtmlInputHidden
        Protected WithEvents IsReportCeVisible As HtmlInputHidden

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As Object

        Private Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Page State"

        Class MyState
            Public MyBO As ReportRequests
            Public IsNew As Boolean = False
            Public IsACopy As Boolean
            Public CompanyGroupIdId As Guid
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public LastErrMsg As String
            Public HasDataChanged As Boolean
            Public ForEdit As Boolean = False
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

#Region "Handlers-Init"

        Private Sub InitializeForm()
            PopulateDropDowns()
            Dim t As Date = Date.Now
            Me.moRunDateText.Text = GetDateFormattedString(t)
        End Sub

        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load, Me.Load
            If Not String.IsNullOrEmpty(Request.QueryString("rid")) Then

            End If
            'Put user code to initialize the page here
            Me.MasterPage.MessageController.Clear_Hide()
            Me.ClearLabelsErrSign()
            Try
                If Not Me.IsPostBack Then
                    InitializeForm()
                    TheReportExtractInputControl.ViewVisible = False
                    TheReportExtractInputControl.PdfVisible = False
                    TheReportExtractInputControl.ExportDataVisible = False
                    TheReportExtractInputControl.DestinationVisible = False
                    MasterPage.UsePageTabTitleInBreadCrum = False
                    UpdateBreadCrum()
                    'Date Calendars
                    Me.AddCalendar(Me.BtnRunDate, Me.moRunDateText)
                Else
                    btnGenRpt.Enabled = True
                    'If (Me.moDealerList.SelectedValue.Equals("SINGLE")) Then
                    '    cboDealer.Enabled = True
                    'Else
                    '    cboDealer.Enabled = False
                    '    cboDealer.ClearSelection()
                    'End If
                End If
                Me.InstallProgressBar()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            ShowMissingTranslations(Me.MasterPage.MessageController)
        End Sub

        Private Sub UpdateBreadCrum()
            Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
            Me.MasterPage.UsePageTabTitleInBreadCrum = False
            Me.MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(PAGETAB) + ElitaBase.Sperator + TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        End Sub

#End Region

#Region "Populate"

        Private Sub PopulateDropDowns()
            PopulateDealerDropDown()
        End Sub

        Private Sub PopulateDealerDropDown()
            Dim oListContext As New ListContext
            Dim oDealerList As New List(Of ListItem)
            For Each _company As Guid In ElitaPlusIdentity.Current.ActiveUser.Companies
                oListContext.CompanyId = _company
                Dim oDealerListForCompany As ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerListByCompany", context:=oListContext)
                If oDealerListForCompany.Count > 0 Then
                    If Not oDealerList Is Nothing Then
                        oDealerList.AddRange(oDealerListForCompany)
                    Else
                        oDealerList = oDealerListForCompany.Clone()
                    End If
                End If
            Next

            Me.cboDealer.Items.Clear()
            Me.cboDealer.Populate(oDealerList.ToArray(), New PopulateOptions() With
                                     {
                                     .AddBlankItem = True,
                                     .TextFunc = Function(ListItem) ListItem.Code + " - " + ListItem.Translation
                                     })
        End Sub

#End Region

#Region "Clear"

        Public Sub ClearLabelsErrSign()
            Try
                ClearLabelErrSign(moRunDateLabel)
                ClearLabelErrSign(moDealerLabel)
                ClearLabelErrSign(lblRejectedRecords)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub btnGenRpt_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGenRpt.Click
            Try
                GenerateReport()
            Catch ex As ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

        Private Sub GenerateReport()
            Dim reportParams As New StringBuilder

            Dim selectedDealerId As Guid = GetSelectedItem(Me.cboDealer)

            If selectedDealerId.Equals(Guid.Empty) Then
                SetLabelError(moDealerLabel)
                Throw New GUIException(Message.MSG_INVALID_DEALER, ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
            End If

            Dim dvDealer As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE")
            Dim dealerCode As String = LookupListNew.GetCodeFromId(dvDealer, selectedDealerId)

            Dim selectedDealer As Dealer = New Dealer(selectedDealerId)
            Dim companyCode As String = selectedDealer.Company.Code

            ReportExtractBase.FormatDate(moRunDateLabel, moRunDateText.Text)

            reportParams.AppendFormat("p_company => '{0}',", companyCode)
            reportParams.AppendFormat("p_dealer => '{0}',", dealerCode)
            reportParams.AppendFormat("p_run_date => '{0}',", moRunDateText.Text)
            reportParams.AppendFormat("p_rejected => {0}", chkRejectedRecords.Checked.ToString().ToLower())

            Me.State.MyBO = New ReportRequests
            Me.State.ForEdit = True
            Me.PopulateBOProperty(Me.State.MyBO, "ReportType", PAGETITLE)
            Me.PopulateBOProperty(Me.State.MyBO, "ReportProc", "r_ascelx_enrollment_extract.report_by_dealer")
            Me.PopulateBOProperty(Me.State.MyBO, "ReportParameters", reportParams.ToString())
            Me.PopulateBOProperty(Me.State.MyBO, "UserEmailAddress", ElitaPlusIdentity.Current.EmailAddress)

            ScheduleReport()
        End Sub

        Private Sub ScheduleReport()
            Try
                Dim scheduleDate As DateTime = TheReportExtractInputControl.GetSchedDate()
                If Me.State.MyBO.IsDirty Then
                    'Me.State.MyBO.Save()

                    Me.State.IsNew = False
                    Me.State.HasDataChanged = True
                    'Me.State.MyBO.CreateJob(scheduleDate)

                    Me.State.MyBO.CreateReportRequest(scheduleDate)

                    If String.IsNullOrEmpty(ElitaPlusIdentity.Current.EmailAddress) Then
                        Me.DisplayMessage(Message.MSG_Email_not_configured, "", MSG_BTN_OK, MSG_TYPE_ALERT, , True)
                    Else
                        Me.DisplayMessage(Message.MSG_REPORT_REQUEST_IS_GENERATED, "", MSG_BTN_OK, MSG_TYPE_ALERT, , True)
                    End If

                    btnGenRpt.Enabled = False
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub PopulateBosFromForm()
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Try
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
    End Class
End Namespace
