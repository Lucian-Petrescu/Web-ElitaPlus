Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Namespace Reports
    Partial Public Class SalesReconciliationReportForm
        Inherits ElitaPlusPage
#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "SALES_RECONCILIATION"
        Private Const RPT_FILENAME As String = "SalesReconciliation"
        Public Const ALL As String = "*"
        Public Const YES As String = "Y"
        Protected WithEvents btnViewHidden As System.Web.UI.WebControls.Button
        Public Const NO As String = "N"
        Private Const LABEL_SELECT_COMPANY As String = "SELECT_COMPANY"
        Private Const RPT_FILENAME_EXPORT As String = "SalesReconciliation-Exp"

        Private Const LABEL_SELECT_DEALER As String = "SELECT_DEALER"

        Private Const TOTALPARAMS As Integer = 15
        Private Const TOTALEXPPARAMS As Integer = 8
        Private Const PARAMS_PER_REPORT As Integer = 8

        Private Const ONE_ITEM As Integer = 1

#End Region

#Region "parameters"
        Public Structure ReportParams
            Public compcode As String
            Public compdesc As String
            Public dealerCode As String
            Public dealerName As String
            Public beginDate As String
            Public endDate As String
            Public isSummary As String
            Public culturevalue As String
        End Structure
#End Region

#Region "variables"
        Private moReportFormat As ReportCeBaseForm.RptFormat
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
                If moDealerMultipleDrop Is Nothing Then
                    moDealerMultipleDrop = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
                End If
                Return moDealerMultipleDrop
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

#Region "Handlers"


#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents moDealerMultipleDrop As MultipleColumnDDLabelControl
        'Protected WithEvents UserCompanyMultiDrop As Common.MultipleColumnDDLabelControl

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
            'AddHandler CompanyMultiDrop.SelectedDropChanged, AddressOf UserCompanyMultiDrop_SelectedIndexChanged

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
        Private Sub InitializeForm()
            PopulateCompaniesDropdown()
            PopulateDealerDropDown()
            Dim t As Date = Date.Now.AddMonths(-1).AddDays(1)
            Me.moBeginDateText.Text = GetDateFormattedString(t)
            Me.moEndDateText.Text = GetDateFormattedString(Date.Now)
            Me.RadiobuttonTotalsOnly.Checked = True
            TheRptCeInputControl.populateReportLanguages(RPT_FILENAME)
        End Sub

#End Region

#Region "Crystal Enterprise"

        Function SetParameters(ByVal compcode As String, ByVal compdesc As String, ByVal dealerCode As String, ByVal dealerName As String, ByVal beginDate As String,
                                  ByVal endDate As String, ByVal isSummary As String) As ReportCeBaseForm.Params

            Dim Params As New ReportCeBaseForm.Params
            Dim repParams(TOTALPARAMS) As ReportCeBaseForm.RptParam
            Dim rptParams As ReportParams
            moReportFormat = ReportCeBase.GetReportFormat(Me)
            Dim culturevalue As String = TheRptCeInputControl.getCultureValue(False, GuidControl.GuidToHexString(CompanyMultiDrop.SelectedGuid))
            Dim reportName As String = TheRptCeInputControl.getReportName(RPT_FILENAME, False)

            With rptParams
                .compcode = compcode
                .compdesc = compdesc
                .dealerCode = dealerCode
                .dealerName = dealerName
                .beginDate = beginDate
                .endDate = endDate
                .isSummary = isSummary
                .culturevalue = culturevalue
            End With
            SetReportParams(rptParams, repParams, String.Empty, PARAMS_PER_REPORT * 0)     ' Main Report
            rptParams.isSummary = "N"

            Me.rptWindowTitle.InnerText = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)

            With Params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = moReportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With

            Return Params

        End Function
        Function SetExpParameters(ByVal compcode As String, ByVal compdesc As String, ByVal dealerCode As String, ByVal dealerName As String, ByVal beginDate As String,
                                  ByVal endDate As String, ByVal isSummary As String) As ReportCeBaseForm.Params

            'Dim reportName As String = RPT_FILENAME_EXPORT
            Dim Params As New ReportCeBaseForm.Params
            Dim repParams(TOTALEXPPARAMS) As ReportCeBaseForm.RptParam
            Dim rptParams As ReportParams
            moReportFormat = ReportCeBase.GetReportFormat(Me)
            Dim culturevalue As String = TheRptCeInputControl.getCultureValue(True, GuidControl.GuidToHexString(CompanyMultiDrop.SelectedGuid))
            Dim reportName As String = TheRptCeInputControl.getReportName(RPT_FILENAME_EXPORT, True)

            With rptParams
                .compcode = compcode
                .compdesc = compdesc
                .dealerCode = dealerCode
                .dealerName = dealerName
                .beginDate = beginDate
                .endDate = endDate
                .isSummary = "Y"
                .culturevalue = culturevalue
            End With
            SetReportParams(rptParams, repParams, String.Empty, PARAMS_PER_REPORT * 0)     ' Main Report

            Me.rptWindowTitle.InnerText = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)

            With Params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = moReportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With

            Return Params
        End Function

        Sub SetReportParams(ByVal rptParams As ReportParams, ByVal repParams() As ReportCeBaseForm.RptParam,
                          ByVal reportName As String, ByVal startIndex As Integer)

            With rptParams
                repParams(startIndex) = New ReportCeBaseForm.RptParam("V_COMP_CODE", .compcode, reportName)
                repParams(startIndex + 1) = New ReportCeBaseForm.RptParam("V_COMP_DESC", .compdesc, reportName)
                repParams(startIndex + 2) = New ReportCeBaseForm.RptParam("V_DEALER_CODE", .dealerCode, reportName)
                repParams(startIndex + 3) = New ReportCeBaseForm.RptParam("V_DEALER_NAME", .dealerName, reportName)
                repParams(startIndex + 4) = New ReportCeBaseForm.RptParam("V_BEGIN_DATE", .beginDate, reportName)
                repParams(startIndex + 5) = New ReportCeBaseForm.RptParam("V_END_DATE", .endDate, reportName)
                repParams(startIndex + 6) = New ReportCeBaseForm.RptParam("V_IS_SUMMARY", .isSummary, reportName)
                repParams(startIndex + 7) = New ReportCeBaseForm.RptParam("LANG_CULTURE_VALUE", .culturevalue, reportName)
            End With
            '  End If
        End Sub
        Private Sub GenerateReport()
            Dim oCompanyId As Guid = CompanyMultiDrop.SelectedGuid
            Dim compCode As String = LookupListNew.GetCodeFromId("COMPANIES", oCompanyId)
            Dim compDesc As String = LookupListNew.GetDescriptionFromId("COMPANIES", oCompanyId)
            Dim selectedDealerId As Guid = DealerMultipleDrop.SelectedGuid 'Me.GetSelectedItem(Me.cboDealerDec)        
            Dim dealerCode As String = DealerMultipleDrop.SelectedCode 'LookupListNew.GetCodeFromId(dv, selectedDealerId)
            Dim dealerName As String = DealerMultipleDrop.SelectedDesc 'LookupListNew.GetCodeFromId(dv, selectedDealerId)
            Dim isSummary As String
            Dim endDate As String
            Dim beginDate As String
            Dim oCountryId As Guid = ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID).Id
            Dim oCountry As New Country(oCountryId)
            Dim params As ReportCeBaseForm.Params
            Dim totalsByCov As String

            'Currency
            Dim Currencyid As Guid = oCountry.PrimaryCurrencyId
            Dim strCurrency As String = LookupListNew.GetDescriptionFromId("CURRENCIES", Currencyid)

            'Dates
            ReportCeBase.ValidateBeginEndDate(moBeginDateLabel, moBeginDateText.Text, moEndDateLabel, moEndDateText.Text)
            endDate = ReportCeBase.FormatDate(moEndDateLabel, moEndDateText.Text)
            beginDate = ReportCeBase.FormatDate(moBeginDateLabel, moBeginDateText.Text)

            If oCompanyId.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(CompanyMultiDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
            End If

            If Me.RadiobuttonTotalsOnly.Checked Then
                isSummary = NO
            Else
                isSummary = YES
            End If


            If selectedDealerId.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(DealerMultipleDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
            End If

            ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

            moReportFormat = ReportCeBase.GetReportFormat(Me)
            If (moReportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (moReportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                'Export Report

                params = SetExpParameters(compCode, compDesc, dealerCode, dealerName, beginDate, endDate, isSummary)
            Else
                'View Report
                params = SetParameters(compCode, compDesc, dealerCode, dealerName, beginDate, endDate, isSummary)
            End If
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

        End Sub

#End Region



    End Class

End Namespace

