Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Namespace Reports
    Partial Class DealerReconciliationReportForm
        Inherits ElitaPlusPage
#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "DEALER RECONCILIATION"
        Private Const RPT_FILENAME As String = "DealerReconciliation"
        Private Const RPT_FILENAME_EXPORT As String = "DealerReconciliation-Exp"

        Public Const CRYSTAL As String = "0"
        Public Const HTML As String = "2"
        Public Const CSV As String = "3"

        Public Const VIEW_REPORT As String = "1"
        Public Const PDF As String = "2"
        Public Const TAB As String = "3"
        Public Const EXCEL As String = "4"

        Public Const EXCEL_DATA_ONLY As String = "5"
        Public Const JAVA As String = "6"
        Public Const ALL As String = "*"
        Public Const NOTHING_SELECTED As Int16 = -1
        Public Const YES As String = "Y"
        Protected WithEvents Label2 As System.Web.UI.WebControls.Label
        Protected WithEvents rdReportFormat_NO_TRANSLATE As System.Web.UI.WebControls.RadioButtonList
        Public Const NO As String = "N"
        Protected WithEvents isReportCeVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents isProgressVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents moReportCeStatus As System.Web.UI.HtmlControls.HtmlInputHidden

        Private Const ONE_ITEM As Integer = 1

        Private Const TOTALPARAMS As Integer = 5
        Private Const TOTALEXPPARAMS As Integer = 5
        Private Const PARAMS_PER_REPORT As Integer = 5

        Private Const LABEL_SELECT_DEALER As String = "SELECT_DEALER"
        Private Const LABEL_SELECT_COMPANY As String = "SELECT_COMPANY"

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

#Region "parameters"
        Public Structure ReportParams
            Public compcode As String
            Public compdesc As String
            Public dealerCode As String
            Public dealerName As String
            Public culturevalue As String
        End Structure
#End Region

#Region "Clear"

        Private Sub ClearErrLabels()
            ClearLabelErrSign(CompanyMultiDrop.CaptionLabel)
            ClearLabelErrSign(DealerMultipleDrop.CaptionLabel)
        End Sub

#End Region

#Region "Handlers"

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents Label1 As System.Web.UI.WebControls.Label
        Protected WithEvents moExportDropDownList As System.Web.UI.WebControls.DropDownList
        Protected WithEvents rdReportType As System.Web.UI.WebControls.RadioButtonList
        Protected WithEvents ErrorCtrl As ErrorController
        Protected WithEvents moReportCeInputControl As ReportCeInputControl
        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            ErrorCtrl.Clear_Hide()

            If CompanyMultiDrop.Visible = False Then
                HideHtmlElement("trsep")
            End If
            ScriptManager1.RegisterAsyncPostBackControl(UserCompanyMultiDrop)
            Try
                If Not IsPostBack Then
                    InitializeForm()
                Else
                    ClearErrLabels()
                End If
                InstallProgressBar()
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
            ShowMissingTranslations(ErrorCtrl)

        End Sub

        Private Sub InitializeForm()
            PopulateCompaniesDropdown()
            PopulateDealerDropDown()
            TheRptCeInputControl.populateReportLanguages(RPT_FILENAME)
        End Sub

#Region "Handlers-Buttons"

        Private Sub btnGenRpt_Click(sender As System.Object, e As System.EventArgs) Handles btnGenRpt.Click
            Try
                Dim oCompanyId As Guid = CompanyMultiDrop.SelectedGuid
                Dim compCode As String = LookupListNew.GetCodeFromId("COMPANIES", oCompanyId)
                Dim compDesc As String = LookupListNew.GetDescriptionFromId("COMPANIES", oCompanyId)
                Dim selectedDealerId As Guid = DealerMultipleDrop.SelectedGuid 'Me.GetSelectedItem(Me.cboDealerDec)        
                Dim dealerCode As String = DealerMultipleDrop.SelectedCode 'LookupListNew.GetCodeFromId(dv, selectedDealerId)
                Dim dealerName As String = DealerMultipleDrop.SelectedDesc 'LookupListNew.GetCodeFromId(dv, selectedDealerId)

                If oCompanyId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(CompanyMultiDrop.CaptionLabel)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
                End If


                If selectedDealerId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(DealerMultipleDrop.CaptionLabel)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                End If

                ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

                Dim params As ReportCeBaseForm.Params = SetParameters(compCode, compDesc, dealerCode, dealerName)
                Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub


#End Region

#Region "Handlers-DropDown"

        Protected Sub OnFromDrop_Changed(fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
             Handles UserCompanyMultiDrop.SelectedDropChanged
            Try
                PopulateDealerDropDown()
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

#End Region

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
                CompanyMultiDrop.SelectedIndex = ONE_ITEM
                CompanyMultiDrop.Visible = False
            End If

        End Sub
#End Region

#Region "Crystal Enterprise"

        Function SetParameters(compcode As String, compdesc As String, dealerCode As String, dealerName As String) As ReportCeBaseForm.Params

            Dim Params As New ReportCeBaseForm.Params
            Dim repParams(TOTALPARAMS) As ReportCeBaseForm.RptParam
            Dim rptParams As ReportParams
            moReportFormat = ReportCeBase.GetReportFormat(Me)

            Dim culturevalue As String = TheRptCeInputControl.getCultureValue(False, GuidControl.GuidToHexString(CompanyMultiDrop.SelectedGuid))
            Dim reportName As String = TheRptCeInputControl.getReportName(RPT_FILENAME, False)

            If (moReportFormat = ReportCeBase.RptFormat.TEXT_TAB OrElse
                moReportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                reportName = TheRptCeInputControl.getReportName(RPT_FILENAME_EXPORT, True)
                culturevalue = TheRptCeInputControl.getCultureValue(True)
            End If

            With rptParams
                .compcode = compcode
                .compdesc = compdesc
                .dealerCode = dealerCode
                .dealerName = dealerName
                .culturevalue = culturevalue
            End With
            SetReportParams(rptParams, repParams, String.Empty, PARAMS_PER_REPORT * 0)     ' Main Report

            rptWindowTitle.InnerText = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)

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
                repParams(startIndex) = New ReportCeBaseForm.RptParam("V_COMP_CODE", .compcode, reportName)
                repParams(startIndex + 1) = New ReportCeBaseForm.RptParam("V_COMP_DESC", .compdesc, reportName)
                repParams(startIndex + 2) = New ReportCeBaseForm.RptParam("V_DEALER_CODE", .dealerCode, reportName)
                repParams(startIndex + 3) = New ReportCeBaseForm.RptParam("V_DEALER_NAME", .dealerName, reportName)
                repParams(startIndex + 4) = New ReportCeBaseForm.RptParam("LANG_CULTURE_VALUE", .culturevalue, reportName)
            End With
            '  End If
        End Sub

#End Region

    End Class
End Namespace

