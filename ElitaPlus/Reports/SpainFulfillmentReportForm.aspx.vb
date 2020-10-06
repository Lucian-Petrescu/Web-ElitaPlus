Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Namespace Reports
    Partial Public Class SpainFulfillmentReportForm
        Inherits ElitaPlusPage

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "EXPORT_FULFILLMENT"
        'Private Const RPT_FILENAME As String = "SpainFulfillment"
        Public Const ALL As String = "*"
        Public Const YES As String = "Y"
        Protected WithEvents btnViewHidden As System.Web.UI.WebControls.Button
        Public Const NO As String = "N"
        Private Const LABEL_SELECT_COMPANY As String = "SELECT_COMPANY"
        Private Const RPT_FILENAME_EXPORT As String = "Fulfillments-Exp"

        Private Const LABEL_SELECT_DEALER As String = "OR A SINGLE DEALER"

        Private Const TOTALPARAMS As Integer = 8  ' 23
        Private Const TOTALEXPPARAMS As Integer = 8  ' 7
        Private Const PARAMS_PER_REPORT As Integer = 8 '8
        Private Const ONE_ITEM As Integer = 1
        'Private Const LABEL_OR_A_SINGLE_DEALER As String = "OR A SINGLE DEALER"
        Public Const Branch_Code As String = "1"
        Public Const Warranty_Sales_Date As String = "2"
        Public Const Dealer_product_code As String = "3"
        Private Const SelectionTypeDates As String = "Dates"
        Private Const SelectionTypeCertNum As String = "CertNum"

#End Region

#Region "parameters"
        Public Structure ReportParams
            Public userid As String
            Public dealerCode As String
            Public begindate As String
            Public enddate As String
            Public begincertnum As String
            Public endcertnum As String
            Public selectiontype As String
            Public langcode As String
            Public culturevalue As String
        End Structure
#End Region

#Region "variables"
        Private moReportFormat As ReportCeBaseForm.RptFormat
#End Region

#Region "Properties"

        Public ReadOnly Property TheExportCeInputControl() As ReportCeInputControl
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
                    moDealerMultipleDrop = CType(FindControl("moDealerMultipleDrop"), MultipleColumnDDLabelControl)
                End If
                Return moDealerMultipleDrop
            End Get
        End Property

        Private Sub EnableOrDisableControls()
            If rSelectDates.Checked = True Then
                ControlMgr.SetEnableControl(Me, txtbegindate, True)
                ControlMgr.SetEnableControl(Me, txtenddate, True)
                ControlMgr.SetEnableControl(Me, txtbegincertnum, False)
                ControlMgr.SetEnableControl(Me, txtendcertnum, False)
            Else
                ControlMgr.SetEnableControl(Me, txtbegindate, False)
                ControlMgr.SetEnableControl(Me, txtenddate, False)
                ControlMgr.SetEnableControl(Me, txtbegincertnum, True)
                ControlMgr.SetEnableControl(Me, txtendcertnum, True)

            End If
        End Sub
#End Region

#Region "Handlers"


#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Handlers-Init"

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            ErrorCtrl.Clear_Hide()
            Try
                If Not IsPostBack Then
                    InitializeForm()
                    AddCalendar(BtnBeginDate, txtbegindate)
                    AddCalendar(BtnEndDate, txtenddate)
                Else
                    ClearErrLabels()
                    EnableOrDisableControls()
                End If
                InstallProgressBar()
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
            ShowMissingTranslations(ErrorCtrl)
        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub btnGenRpt_Click(sender As System.Object, e As System.EventArgs) Handles btnGenRpt.Click
            Try
                GenerateReport()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

#End Region

#Region "Handlers-DropDown"

#End Region

#End Region

#Region "Clear"

        Private Sub ClearErrLabels()
            ' Me.ClearLabelErrSign(MonthYearLabel)
            ClearLabelErrSign(lblbegindate)
            ClearLabelErrSign(lblenddate)
            ClearLabelErrSign(lblbegincertnum)
            ClearLabelErrSign(lblendcertnum)
            ClearLabelErrSign(DealerMultipleDrop.CaptionLabel)
            If rdealer.Checked Then DealerMultipleDrop.SelectedIndex = -1
        End Sub

#End Region

#Region "Populate"

        Private Sub PopulateDealerDropDown()
            Dim dv As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
            DealerMultipleDrop.NothingSelected = True
            DealerMultipleDrop.SetControl(False, _
                                              DealerMultipleDrop.MODES.NEW_MODE, _
                                              True, _
                                              dv, _
                                              TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER), _
                                              True, _
                                              False, _
                                              " document.forms[0].rdealer.checked = false;", _
                                              "moDealerMultipleDrop_moMultipleColumnDrop", _
                                              "moDealerMultipleDrop_moMultipleColumnDropDesc", "moDealerMultipleDrop_lb_DropDown")

        End Sub

        Private Sub InitializeForm()
            Dim t As Date = Date.Now.AddMonths(-1).AddDays(1)
            txtbegindate.Text = GetDateFormattedString(t)
            txtenddate.Text = GetDateFormattedString(Date.Now)
            rdealer.Checked = True
            rSelectDates.Checked = True
            ControlMgr.SetEnableControl(Me, txtbegincertnum, False)
            ControlMgr.SetEnableControl(Me, txtendcertnum, False)
            PopulateDealerDropDown()
            TheExportCeInputControl.SetExportOnly()
            TheExportCeInputControl.populateReportLanguages(RPT_FILENAME_EXPORT)
        End Sub

#End Region

#Region "Crystal Enterprise"
        Function SetExpParameters(userid As String, dealerCode As String, _
                                  begindate As String, enddate As String, begincertnum As String, endcertnum As String, selectiontype As String, langcode As String) As ReportCeBaseForm.Params

            'Dim reportName As String = RPT_FILENAME_EXPORT
            Dim Params As New ReportCeBaseForm.Params
            Dim repParams(TOTALEXPPARAMS) As ReportCeBaseForm.RptParam
            Dim rptParams As ReportParams
            moReportFormat = ReportCeBase.GetReportFormat(Me)
            Dim culturevalue As String = TheExportCeInputControl.getCultureValue(True)
            Dim reportName As String = TheExportCeInputControl.getReportName(RPT_FILENAME_EXPORT, True)

            With rptParams
                .userid = userid
                .dealerCode = dealerCode
                .begindate = begindate
                .enddate = enddate
                .begincertnum = begincertnum
                .endcertnum = endcertnum
                .selectiontype = selectiontype
                .langcode = langcode
                .culturevalue = culturevalue
            End With
            SetReportParams(rptParams, repParams, String.Empty, PARAMS_PER_REPORT * 0) ' Main Report

            rptWindowTitle.InnerText = TheExportCeInputControl.getReportWindowTitle(TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW))

            With Params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = moReportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With

            Return Params
        End Function

        Sub SetReportParams(rptParams As ReportParams, repParams() As ReportCeBaseForm.RptParam, _
                          reportName As String, startIndex As Integer)

            With rptParams
                repParams(startIndex) = New ReportCeBaseForm.RptParam("V_USER_ID", .userid, reportName)
                repParams(startIndex + 1) = New ReportCeBaseForm.RptParam("V_DEALER_CODE", .dealerCode, reportName)
                repParams(startIndex + 2) = New ReportCeBaseForm.RptParam("V_BEGIN_DATE", .begindate, reportName)
                repParams(startIndex + 3) = New ReportCeBaseForm.RptParam("V_END_DATE", .enddate, reportName)
                repParams(startIndex + 4) = New ReportCeBaseForm.RptParam("V_BEGIN_CERT_NUM", .begincertnum, reportName)
                repParams(startIndex + 5) = New ReportCeBaseForm.RptParam("V_END_CERT_NUM", .endcertnum, reportName)
                repParams(startIndex + 6) = New ReportCeBaseForm.RptParam("V_SELECT_TYPE", .selectiontype, reportName)
                repParams(startIndex + 7) = New ReportCeBaseForm.RptParam("V_LANG_CODE", .langcode, reportName)
                repParams(startIndex + 8) = New ReportCeBaseForm.RptParam("LANG_CULTURE_VALUE", .culturevalue, reportName)
            End With
            '  End If
        End Sub
        Private Sub GenerateReport()
            Dim userId As Guid = ElitaPlusIdentity.Current.ActiveUser.Id
            Dim selectedDealerId As Guid = DealerMultipleDrop.SelectedGuid

            Dim dealerCode As String = DealerMultipleDrop.SelectedCode
            Dim dealerDesc As String = DealerMultipleDrop.SelectedDesc
            Dim langCode As String = LookupListNew.GetCodeFromId("LANGUAGES", ElitaPlusIdentity.Current.ActiveUser.LanguageId)

            Dim endDate As String
            Dim beginDate As String
            Dim sortorder As String
            Dim selectiontype As String
            Dim endcertnum As String
            Dim begincertnum As String
            Dim params As ReportCeBaseForm.Params

            'Dates
            If rSelectDates.Checked = True Then
                If Not txtbegindate.Text.Trim.ToString = String.Empty AndAlso Not txtenddate.Text.Trim.ToString = String.Empty Then
                    ReportCeBase.ValidateBeginEndDate(lblbegindate, txtbegindate.Text, lblenddate, txtenddate.Text)
                    endDate = ReportCeBase.FormatDate(lblenddate, txtenddate.Text)
                    beginDate = ReportCeBase.FormatDate(lblbegindate, txtbegindate.Text)
                    selectiontype = SelectionTypeDates
                Else
                    ElitaPlusPage.SetLabelError(lblbegindate)
                    ElitaPlusPage.SetLabelError(lblenddate)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_YEARMONTH_MUST_BE_SELECTED_ERR)
                End If
            Else
                If rselectcertnum.Checked = True Then
                    If Not txtbegincertnum.Text.Trim.ToString = String.Empty AndAlso Not txtendcertnum.Text.Trim.ToString = String.Empty Then
                        begincertnum = txtbegincertnum.Text.Trim.ToString
                        endcertnum = txtendcertnum.Text.Trim.ToString
                        selectiontype = SelectionTypeCertNum
                    Else
                        ElitaPlusPage.SetLabelError(lblbegincertnum)
                        ElitaPlusPage.SetLabelError(lblendcertnum)
                        Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_CERTIFICATE_NUMBER_IS_REQUIRED_ERRR)
                    End If
                End If
            End If

            'Dealer
            If rdealer.Checked Then
                dealerCode = ALL
            Else
                If selectedDealerId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(DealerMultipleDrop.CaptionLabel)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                End If
            End If

            ReportCeBase.EnableReportCe(Me, TheExportCeInputControl)

            moReportFormat = ReportCeBase.GetReportFormat(Me)
            If (moReportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (moReportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                params = SetExpParameters(GuidControl.GuidToHexString(UserId), dealerCode, beginDate, endDate, begincertnum, endcertnum, selectiontype, langCode)
            End If

            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

        End Sub

#End Region

    End Class
End Namespace
