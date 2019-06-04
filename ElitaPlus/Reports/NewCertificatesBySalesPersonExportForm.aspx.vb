Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common

Namespace Reports
    Partial Public Class NewCertificatesBySalesPersonExportForm
        Inherits ElitaPlusPage
#Region "Constants"
        Private Const RPT_FILENAME_WINDOW As String = "NEW CERTIFICATES BY SALES REP"
        Private Const RPT_FILENAME As String = "NewCertificatesBySalesPerson-Exp_EN"
        Public Const YES As String = "Y"
        Public Const NO As String = "N"

        Public Const ALL As String = "*"
        Private Const LABEL_SELECT_DEALER As String = "OR A SINGLE DEALER"
        Private Const LABEL_SELECT_COMPANY As String = "SELECT_COMPANY"

        Private Const TOTALEXPPARAMS As Integer = 6
#End Region

#Region "Properties"
        Dim moReportFormat As ReportCeBaseForm.RptFormat

        Public ReadOnly Property TheRptCeInputControl() As ReportExtractInputControl
            Get
                If moReportExtractInputControl Is Nothing Then
                    moReportExtractInputControl = CType(FindControl("moReportExtractInputControl"), ReportExtractInputControl)
                End If
                Return moReportExtractInputControl
            End Get
        End Property

        Public ReadOnly Property CompanyMultiDrop() As MultipleColumnDDLabelControl
            Get
                If UserCompanyMultiDrop Is Nothing Then
                    UserCompanyMultiDrop = CType(FindControl("UserCompanyMultiDrop"), MultipleColumnDDLabelControl)
                End If
                Return UserCompanyMultiDrop
            End Get
        End Property

        Public ReadOnly Property DealerMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If multipleDropControl Is Nothing Then
                    multipleDropControl = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
                End If
                Return multipleDropControl
            End Get
        End Property
#End Region

#Region "Page State"

        Class MyState
            Public MyBO As ReportRequests
            Public IsNew As Boolean = False
            Public ForEdit As Boolean = False
            Public HasDataChanged As Boolean
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

#Region "Parameters"

        Public Structure ReportParams
            Public compcode As String
            Public dealerCode As String
            Public beginDate As String
            Public endDate As String
            Public dateAddedSold As String
        End Structure

#End Region

#Region "Page event handler"
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Me.ErrorCtrl.Clear_Hide()

            If CompanyMultiDrop.Visible = False Then
                HideHtmlElement("CompTRsprt")
            End If

            Me.ScriptManager1.RegisterAsyncPostBackControl(Me.UserCompanyMultiDrop)
            Try
                If Not Me.IsPostBack Then
                    InitializeForm()
                    Me.AddCalendar(Me.BtnBeginDate, Me.moBeginDateText)
                    Me.AddCalendar(Me.BtnEndDate, Me.moEndDateText)
                Else
                    ClearErrLabels()
                End If
                Me.InstallProgressBar()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
            Me.ShowMissingTranslations(Me.ErrorCtrl)
        End Sub

        Private Sub InitializeForm()
            'TheReportCeInputControl.populateReportLanguages(RPT_FILENAME)
            TheRptCeInputControl.SetExportOnly()
            PopulateCompaniesDropdown()
            PopulateDealerDropDown()
            Dim t As Date = Date.Now.AddMonths(-1).AddDays(1)
            Me.moBeginDateText.Text = GetDateFormattedString(t)
            Me.moEndDateText.Text = GetDateFormattedString(Date.Now)
            Me.rdealer.Checked = True
        End Sub
#End Region

#Region "Helper functions"
        Sub PopulateDealerDropDown()
            If CompanyMultiDrop.SelectedIndex > Me.NO_ITEM_SELECTED_INDEX Then
                Dim dv As DataView = LookupListNew.GetDealerLookupList(CompanyMultiDrop.SelectedGuid)
                DealerMultipleDrop.NothingSelected = True
                DealerMultipleDrop.SetControl(False, DealerMultipleDrop.MODES.NEW_MODE, True, dv, TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER), True, True, " document.forms[0].rdealer.checked = false;")
                rdealer.Checked = True
            End If
        End Sub
        Private Sub PopulateCompaniesDropdown()
            Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()
            CompanyMultiDrop.NothingSelected = True
            CompanyMultiDrop.SetControl(True, CompanyMultiDrop.MODES.NEW_MODE, True, dv, "* " + TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY), True)
            If dv.Count = 1 Then
                HideHtmlElement("CompTRsprt")
                CompanyMultiDrop.SelectedIndex = 1
                CompanyMultiDrop.Visible = False
            End If
        End Sub

        Private Sub ClearErrLabels()
            Me.ClearLabelErrSign(moBeginDateLabel)
            Me.ClearLabelErrSign(moEndDateLabel)
            Me.ClearLabelErrSign(multipleDropControl.CaptionLabel)
            If Me.rdealer.Checked Then multipleDropControl.SelectedIndex = -1
        End Sub

        Function SetParameters(ByVal rptParams As ReportParams) As ReportCeBaseForm.Params

            Dim params As New ReportCeBaseForm.Params
            moReportFormat = ReportCeBase.GetReportFormat(Me)

            Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() {
                     New ReportCeBaseForm.RptParam("V_COMP_CODE", rptParams.compcode),
                     New ReportCeBaseForm.RptParam("V_DEALER_CODE", rptParams.dealerCode),
                     New ReportCeBaseForm.RptParam("V_FROM_DATE", rptParams.beginDate),
                     New ReportCeBaseForm.RptParam("V_TO_DATE", rptParams.endDate),
                     New ReportCeBaseForm.RptParam("V_ADDED_SOLD", rptParams.dateAddedSold)}

            With params
                .msRptName = RPT_FILENAME
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = moReportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With
            Return params
        End Function

        Private Sub GenerateReport()
            Dim CompanyCode As String
            Dim dealerCode As String
            Dim params As ReportCeBaseForm.Params
            Dim endDate As String
            Dim beginDate As String
            Dim dateAddedSold As String
            Dim reportParams As New System.Text.StringBuilder

            If CompanyMultiDrop.SelectedGuid.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(CompanyMultiDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
            Else
                CompanyCode = CompanyMultiDrop.SelectedCode.Trim
            End If

            If rdealer.Checked Then
                dealerCode = ALL 'all dealers if none selected
            Else
                If DealerMultipleDrop.SelectedGuid.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(DealerMultipleDrop.CaptionLabel)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                Else
                    dealerCode = DealerMultipleDrop.SelectedCode.Trim
                End If
            End If

            'Dates
            ReportCeBase.ValidateBeginEndDate(moBeginDateLabel, moBeginDateText.Text, moEndDateLabel, moEndDateText.Text)
            endDate = ReportCeBase.FormatDate(moEndDateLabel, moEndDateText.Text)
            beginDate = ReportCeBase.FormatDate(moBeginDateLabel, moBeginDateText.Text)

            If Me.RadiobuttonSold.Checked Then
                dateAddedSold = "S"
            Else
                dateAddedSold = "A"
            End If

            reportParams.AppendFormat("pi_comp_code => '{0}',", CompanyCode)
            reportParams.AppendFormat("pi_dealer_code => '{0}',", dealerCode)
            reportParams.AppendFormat("pi_from_date => '{0}',", beginDate)
            reportParams.AppendFormat("pi_to_date => '{0}',", endDate)
            reportParams.AppendFormat("pi_added_sold => '{0}'", dateAddedSold)


            Me.State.MyBO = New ReportRequests
            Me.State.ForEdit = True
            Me.PopulateBOProperty(Me.State.MyBO, "ReportType", "NEW_CERTS_BY_SALES_PERSON")
            Me.PopulateBOProperty(Me.State.MyBO, "ReportProc", "R_NewCertsBySalesPerson.Oralce_Export")
            Me.PopulateBOProperty(Me.State.MyBO, "ReportParameters", ReportParams.ToString())
            Me.PopulateBOProperty(Me.State.MyBO, "UserEmailAddress", ElitaPlusIdentity.Current.EmailAddress)

            'ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)
            'moReportFormat = ReportCeBase.GetReportFormat(Me)
            'If (moReportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (moReportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
            '    'Export Report
            '    params = SetExpParameters(CompanyCode, dealerCode, beginDate, endDate, dateAddedSold)
            'End If

            'Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params
            ScheduleReport()
        End Sub


        Private Sub ScheduleReport()
            Try
                Dim scheduleDate As DateTime = TheRptCeInputControl.GetSchedDate()
                If Me.State.MyBO.IsDirty Then
                    Me.State.MyBO.Save()

                    Me.State.IsNew = False
                    Me.State.HasDataChanged = True
                    Me.State.MyBO.CreateJob(scheduleDate)

                    If String.IsNullOrEmpty(ElitaPlusIdentity.Current.EmailAddress) Then
                        Me.DisplayMessage(Message.MSG_Email_not_configured, "", Me.MSG_BTN_OK, Me.MSG_TYPE_ALERT, , True)
                    Else
                        Me.DisplayMessage(Message.MSG_REPORT_REQUEST_IS_GENERATED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_ALERT, , True)
                    End If

                    btnGenRpt.Enabled = False

                Else
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub



        Function SetExpParameters(ByVal compcode As String, ByVal dealerCode As String, ByVal beginDate As String,
                                  ByVal endDate As String, ByVal dateAddedSold As String) As ReportCeBaseForm.Params

            Dim reportFormat As ReportCeBaseForm.RptFormat
            Dim reportName As String = RPT_FILENAME
            Dim params As New ReportCeBaseForm.Params
            Dim repParams(TOTALEXPPARAMS) As ReportCeBaseForm.RptParam
            Dim oReportParams As ReportParams

            'reportName = TheReportCeInputControl.getReportName(RPT_FILENAME, True)
            'culturecode = TheReportCeInputControl.getCultureValue(True)

            Me.rptWindowTitle.InnerText = TheRptCeInputControl.getReportWindowTitle(TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW))

            With oReportParams
                .compcode = compcode
                .dealerCode = dealerCode
                .beginDate = beginDate
                .endDate = endDate
                .dateAddedSold = dateAddedSold
            End With

            params = SetParameters(oReportParams)
            Return params
        End Function
#End Region

#Region "Control Event Handlers"

        Private Sub btnGenRpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRpt.Click
            Try
                GenerateReport()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Private Sub UserCompanyMultiDrop_SelectedDropChanged(ByVal aSrc As Common.MultipleColumnDDLabelControl) Handles UserCompanyMultiDrop.SelectedDropChanged
            Try
                PopulateDealerDropDown()
            Catch ex As Exception
                HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub
#End Region


    End Class
End Namespace