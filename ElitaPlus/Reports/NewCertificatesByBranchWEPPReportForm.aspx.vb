Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Namespace Reports

    Partial Class NewCertificatesByBranchWEPPReportForm
        Inherits ElitaPlusPage

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "NEW CERTIFICATES BY BRANCH"
        Private Const RPT_FILENAME As String = "NewCertificatesByBranchWEPP"
        Private Const RPT_FILENAME_EXPORT As String = "NewCertificatesByBranchWEPP-Exp"

        Public Const ALL As String = "*"
        '   Public Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Public Const YES As String = "Y"
        Protected WithEvents btnViewHidden As System.Web.UI.WebControls.Button
        Public Const NO As String = "N"
        Private Const DATE_ADDED As String = "A"
        Private Const DATE_SOLD As String = "S"
        Private Const TOTALPARAMS As Integer = 21 ' 15  ' 14
        Private Const TOTALEXPPARAMS As Integer = 11 '6  ' 7
        Private Const PARAMS_PER_REPORT As Integer = 11

        Private Const ONE_ITEM As Integer = 1
        Private Const LABEL_SELECT_DEALER As String = "SELECT_DEALER"
        Private Const LABEL_SELECT_COMPANY As String = "SELECT_COMPANY"

#End Region

#Region "variables"
        Private moReportFormat As ReportCeBaseForm.RptFormat
        Dim reportName As String
        Dim culturecode As String
#End Region

#Region "Properties"
        Public ReadOnly Property TheRptCeInputControl() As ReportCeInputControl
            Get
                If moReportCeInputControl Is Nothing Then
                    moReportCeInputControl = CType(FindControl("moReportCeInputControl"), ReportCeInputControl)
                End If
                Return moReportCeInputControl
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
        Public ReadOnly Property CompanyMultiDrop() As MultipleColumnDDLabelControl
            Get
                If UserCompanyMultiDrop Is Nothing Then
                    UserCompanyMultiDrop = CType(FindControl("UserCompanyMultiDrop"), MultipleColumnDDLabelControl)
                End If
                Return UserCompanyMultiDrop
            End Get
        End Property
#End Region

#Region "Parameters"

        Public Structure ReportParams
            Public compcode As String
            Public compdesc As String
            'Public companyCode As String
            Public dealerCode As String
            Public dealerName As String
            Public beginDate As String
            Public endDate As String
            Public detailCode As String
            Public langCode As String
            Public dateAddedSold As String
            Public byBranch As String
            Public lang_culture_value As String
        End Structure

#End Region

#Region "Handlers"

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Protected WithEvents moReportCeInputControl As ReportCeInputControl
        Protected WithEvents ErrorCtrl As ErrorController
        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Handlers-Init"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Me.ErrorCtrl.Clear_Hide()
            If CompanyMultiDrop.Visible = False Then
                HideHtmlElement("trsep")
            End If
            Me.ScriptManager1.RegisterAsyncPostBackControl(Me.UserCompanyMultiDrop)
            Try
                If Not Me.IsPostBack Then
                    InitializeForm()
                    'Date Calendars
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

#End Region

#Region "Handlers-Buttons"

        Private Sub btnGenRpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRpt.Click
            Try
                GenerateReport()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

#End Region

#End Region

#Region "Clear"

        Private Sub ClearErrLabels()
            Me.ClearLabelErrSign(moBeginDateLabel)
            Me.ClearLabelErrSign(moEndDateLabel)
            Me.ClearLabelErrSign(CompanyMultiDrop.CaptionLabel)
            Me.ClearLabelErrSign(DealerMultipleDrop.CaptionLabel)
        End Sub

#End Region

#Region "Handlers-DropDown"

        Protected Sub OnFromDrop_Changed(ByVal fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
             Handles UserCompanyMultiDrop.SelectedDropChanged
            Try
                PopulateDealerDropDown()
            Catch ex As Exception
                HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        'Private Sub UserCompanyMultiDrop_SelectedIndexChanged(ByVal UserCompanyMultiDrop As Common.MultipleColumnDDLabelControl) Handles UserCompanyMultiDrop.SelectedDropChanged
        '    Try
        '        If Me.UserCompanyMultiDrop.SelectedIndex = 0 Then
        '            Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
        '            Exit Sub
        '        End If
        '        PopulateDealerDropDown()
        '    Catch ex As Exception
        '        Me.HandleErrors(ex, Me.ErrorCtrl)
        '    End Try
        'End Sub

#End Region

#Region "Populate"


        Sub PopulateDealerDropDown()

            If Not CompanyMultiDrop.SelectedGuid = Guid.Empty Then
                Dim dv As DataView = LookupListNew.GetDealerLookupList(CompanyMultiDrop.SelectedGuid)
                DealerMultipleDrop.NothingSelected = True
                DealerMultipleDrop.SetControl(False, DealerMultipleDrop.MODES.NEW_MODE, True, dv, "* " + TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER), True, True)
            Else
                Dim dv As DataView = LookupListNew.GetDealerLookupList(CompanyMultiDrop.SelectedGuid)
                DealerMultipleDrop.NothingSelected = True
                DealerMultipleDrop.SetControl(False, DealerMultipleDrop.MODES.NEW_MODE, True, dv, "* " + TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER), True, True)
            End If

        End Sub
        Private Sub PopulateCompaniesDropdown()

            Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()
            CompanyMultiDrop.NothingSelected = True
            CompanyMultiDrop.SetControl(True, CompanyMultiDrop.MODES.NEW_MODE, True, dv, "* " + TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY), True)
            'CompanyMultipleDrop.CaptionLabel
            If dv.Count.Equals(ONE_ITEM) Then
                HideHtmlElement("trsep")
                CompanyMultiDrop.SelectedIndex = Me.ONE_ITEM
                CompanyMultiDrop.Visible = False
            End If

        End Sub

        'Sub PopulateDealerDropDown()            
        '    Me.BindListControlToDataView(Me.cboDealer, LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE"), , , True)
        'End Sub

        Private Sub InitializeForm()
            PopulateCompaniesDropdown()
            PopulateDealerDropDown()
            Dim t As Date = Date.Today.AddDays(-14)
            Me.moBeginDateText.Text = GetDateFormattedString(t)
            Me.moEndDateText.Text = GetDateFormattedString(Date.Today)
            Me.RadiobuttonTotalsOnly.Checked = True
            TheRptCeInputControl.populateReportLanguages(RPT_FILENAME)

        End Sub

#End Region

#Region "Crystal Enterprise"

        Function SetParameters(ByVal compcode As String, ByVal compdesc As String, ByVal dealerCode As String, ByVal dealerName As String, ByVal beginDate As String,
                                  ByVal endDate As String, ByVal detailCode As String, ByVal langCode As String, ByVal dateAddedSold As String, ByVal bybranch As String) As ReportCeBaseForm.Params


            Dim reportFormat As ReportCeBaseForm.RptFormat
            'Dim reportName As String = RPT_FILENAME
            Dim params As New ReportCeBaseForm.Params
            Dim repParams(TOTALPARAMS) As ReportCeBaseForm.RptParam
            Dim oReportParams As ReportParams
            Dim subReportName As String = "SubREP New Certificates By Branch.rpt"
            reportName = TheRptCeInputControl.getReportName(RPT_FILENAME, False)
            culturecode = TheRptCeInputControl.getCultureValue(False)

            Me.rptWindowTitle.InnerText = TheRptCeInputControl.getReportWindowTitle(TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW))

            With oReportParams
                .compcode = compcode
                .compdesc = compdesc
                .dealerCode = dealerCode
                .dealerName = dealerName
                .beginDate = beginDate
                .endDate = endDate
                .detailCode = detailCode
                .langCode = langCode
                .dateAddedSold = dateAddedSold
                .byBranch = bybranch
                .lang_culture_value = culturecode
            End With
            SetReportParams(oReportParams, repParams, String.Empty, PARAMS_PER_REPORT * 0)     ' Main Report
            oReportParams.detailCode = "N"

            With params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = moReportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With


            Return params
        End Function

        Private Sub GenerateReport()
            Dim oCompanyId As Guid = CompanyMultiDrop.SelectedGuid
            Dim compCode As String = LookupListNew.GetCodeFromId("COMPANIES", oCompanyId)
            Dim compDesc As String = LookupListNew.GetDescriptionFromId("COMPANIES", oCompanyId)
            Dim selectedDealerId As Guid = DealerMultipleDrop.SelectedGuid
            Dim dealerCode As String = DealerMultipleDrop.SelectedCode
            Dim dealerName As String = DealerMultipleDrop.SelectedDesc
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim langCode As String = LookupListNew.GetCodeFromId("LANGUAGES", langId)
            Dim params As ReportCeBaseForm.Params
            Dim endDate As String
            Dim beginDate As String
            Dim dateAddedSold As String
            Dim bybranch As String
            Dim detailCode As String

            'Dates
            ReportCeBase.ValidateBeginEndDate(moBeginDateLabel, moBeginDateText.Text, moEndDateLabel, moEndDateText.Text)
            endDate = ReportCeBase.FormatDate(moEndDateLabel, moEndDateText.Text)
            beginDate = ReportCeBase.FormatDate(moBeginDateLabel, moBeginDateText.Text)

            If Me.RadiobuttonTotalsOnly.Checked Then
                detailCode = NO
            Else
                detailCode = YES
            End If

            If Me.RadiobuttonSold.Checked Then
                dateAddedSold = DATE_SOLD
            Else
                dateAddedSold = DATE_ADDED
            End If

            If Me.rbyBranch.Checked Then
                bybranch = YES
            Else
                bybranch = NO
            End If

            If oCompanyId.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(CompanyMultiDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
            End If


            If selectedDealerId.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(DealerMultipleDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
            End If

            ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

            moReportFormat = ReportCeBase.GetReportFormat(Me)
            If (moReportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (moReportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                'Export Report
                detailCode = YES
                params = SetExpParameters(compCode, compDesc, dealerCode, dealerName, beginDate,
                              endDate, detailCode, langCode, dateAddedSold, bybranch)
            Else
                'View Report
                params = SetParameters(compCode, compDesc, dealerCode, dealerName, beginDate,
                              endDate, detailCode, langCode, dateAddedSold, bybranch)
            End If

            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

        End Sub

        Sub SetReportParams(ByVal oReportParams As ReportParams, ByVal repParams() As ReportCeBaseForm.RptParam,
                                    ByVal reportName As String, ByVal startIndex As Integer)
            With oReportParams
                repParams(startIndex) = New ReportCeBaseForm.RptParam("V_COMP_CODE", .compcode, reportName)
                repParams(startIndex + 1) = New ReportCeBaseForm.RptParam("V_COMP_DESC", .compdesc, reportName)
                repParams(startIndex + 2) = New ReportCeBaseForm.RptParam("V_DEALER_CODE", .dealerCode, reportName)
                repParams(startIndex + 3) = New ReportCeBaseForm.RptParam("V_DEALER_NAME", .dealerName, reportName)
                repParams(startIndex + 4) = New ReportCeBaseForm.RptParam("V_FROM_DATE", .beginDate, reportName)
                repParams(startIndex + 5) = New ReportCeBaseForm.RptParam("V_TO_DATE", .endDate, reportName)
                repParams(startIndex + 6) = New ReportCeBaseForm.RptParam("V_DETAIL_CODE", .detailCode, reportName)
                repParams(startIndex + 7) = New ReportCeBaseForm.RptParam("V_LANGUAGE_CODE", .langCode, reportName)
                repParams(startIndex + 8) = New ReportCeBaseForm.RptParam("V_ADDED_SOLD", .dateAddedSold, reportName)
                repParams(startIndex + 9) = New ReportCeBaseForm.RptParam("BY_BRANCH", .byBranch, reportName)
                repParams(startIndex + 10) = New ReportCeBaseForm.RptParam("LANG_CULTURE_VALUE", .lang_culture_value, reportName)
            End With

        End Sub


        Function SetExpParameters(ByVal compcode As String, ByVal compdesc As String, ByVal dealerCode As String, ByVal dealerName As String, ByVal beginDate As String,
                                  ByVal endDate As String, ByVal detailCode As String, ByVal langCode As String, ByVal dateAddedSold As String, ByVal bybranch As String) As ReportCeBaseForm.Params

            Dim reportFormat As ReportCeBaseForm.RptFormat
            Dim reportName As String = RPT_FILENAME_EXPORT
            Dim params As New ReportCeBaseForm.Params
            Dim repParams(TOTALEXPPARAMS) As ReportCeBaseForm.RptParam
            Dim oReportParams As ReportParams
            reportName = TheRptCeInputControl.getReportName(RPT_FILENAME_EXPORT, True)
            culturecode = TheRptCeInputControl.getCultureValue(True)

            Me.rptWindowTitle.InnerText = TheRptCeInputControl.getReportWindowTitle(TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW))

            With oReportParams
                .compcode = compcode
                .compdesc = compdesc
                .dealerCode = dealerCode
                .dealerName = dealerName
                .beginDate = beginDate
                .endDate = endDate
                .detailCode = detailCode
                .langCode = langCode
                .dateAddedSold = dateAddedSold
                .byBranch = bybranch
                .lang_culture_value = culturecode
            End With
            SetReportParams(oReportParams, repParams, String.Empty, PARAMS_PER_REPORT * 0)     ' Main Report

            With params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = moReportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With

            Return params
        End Function

#End Region


    End Class
End Namespace
