Imports System.Text.RegularExpressions
Imports System.Security.Cryptography.X509Certificates
Imports Assurant.ElitaPlus.DALObjects
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common

Namespace Reports

    Partial Class BillingRegisterPortugueseBrazilReportForm
        Inherits ElitaPlusPage


#Region "Parameters"

        Public Structure ReportParams
            Public companyCode As String
            Public beginDate As String
            Public endDate As String

        End Structure

#End Region

#Region "Constants"

        '   Private Const RPT_FILENAME As String = "Claims Following Service Warranty English (USA)_TEST7"
        Private Const RPT_FILENAME_WINDOW As String = "BRAZIL BILLING REGISTER"
        Private Const RPT_FILENAME As String = "BillingRegisterPortugueseBrazil"
        Private Const RPT_FILENAME_EXPORT As String = "BillingRegisterPortugueseBrazil_Exp"

        Private Const ONE_ITEM As Integer = 1

        Public Const JAN As String = "1"
        Public Const FEB As String = "2"
        Public Const MARCH As String = "3"
        Public Const APRIL As String = "4"
        Public Const MAY As String = "5"
        Public Const JUNE As String = "6"
        Public Const JULY As String = "7"
        Public Const AUGUST As String = "8"
        Public Const SEPTEMBER As String = "9"
        Public Const OCTOBER As String = "10"
        Public Const NOVEMBER As String = "11"
        Public Const DECEMBER As String = "12"

        Public Const Y2001 As String = "2001"
        Public Const Y2002 As String = "2002"
        Public Const Y2003 As String = "2003"
        Public Const Y2004 As String = "2004"
        Public Const Y2005 As String = "2005"


        Public Const ALL As String = "*"
        Public Const NOTHING_SELECTED As Int16 = -1
        Public Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Public Const YES As String = "Y"
        Public Const NO As String = "N"

        Private Const TOTALPARAMS As Integer = 23  ' 24
        Private Const TOTALEXPPARAMS As Integer = 7  ' 8
        Private Const PARAMS_PER_REPORT As Integer = 8

        Protected WithEvents isProgressVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents isReportCeVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents moReportCeStatus As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents moDaysActiveLabel As System.Web.UI.WebControls.Label
        Protected WithEvents moDealerLabel As System.Web.UI.WebControls.Label

        Private Const LABEL_SELECT_COMPANY As String = "SELECT_COMPANY"
#End Region

#Region "variables"
        Dim moReportFormat As ReportCeBaseForm.RptFormat
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

        Public ReadOnly Property UserCompanyMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If moUserCompanyMultipleDrop Is Nothing Then
                    moUserCompanyMultipleDrop = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
                End If
                Return moUserCompanyMultipleDrop
            End Get
        End Property
#End Region

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents Label1 As System.Web.UI.WebControls.Label
        Protected WithEvents ErrorCtrl As ErrorController
        Protected WithEvents moReportCeInputControl As ReportCeInputControl
        Protected WithEvents moUserCompanyMultipleDrop As MultipleColumnDDLabelControl

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Me.ErrorCtrl.Clear_Hide()
            Me.ClearLabelErrSign(UserCompanyMultipleDrop.CaptionLabel)
            Try
                If Not Me.IsPostBack Then
                    InitializeForm()
                Else
                    ClearErrLabels()
                End If
                Me.InstallProgressBar()
                ' Me.DisplayProgressBarOnClick(Me.btnGenRpt, "LOADING_REPORT")
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
            Me.ShowMissingTranslations(Me.ErrorCtrl)
        End Sub

        Private Sub PopulateYearsDropdown()
            'Dim dv As DataView = LookupListNew.GetAccountingClosingYearsLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
            'Me.BindListControlToDataView(Me.moYearDropDownList, dv, , , False)
            ' Dim dv As DataView = AccountingCloseInfo.GetClosingYears(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
            ' Me.BindListTextToDataView(Me.moYearDropDownList, dv, , , True)
            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyId = ElitaPlusIdentity.Current.ActiveUser.CompanyId
            Dim YearListLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ClosingYearsByCompany", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            Me.moYearDropDownList.Populate(YearListLkl, New PopulateOptions() With
             {
            .AddBlankItem = True,
            .ValueFunc = AddressOf PopulateOptions.GetCode,
           .BlankItemValue = "0"
             })
            Dim oDescrip As String = Me.GetSelectedDescription(Me.moYearDropDownList)
        End Sub

        Private Sub ClearErrLabels()
            Me.ClearLabelErrSign(moMonthYear)
            Me.ClearLabelErrSign(UserCompanyMultipleDrop.CaptionLabel)
        End Sub

        Private Sub PopulateMonthsDropdown()
            'Dim dv As DataView = LookupListNew.GetMonthsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            ' dv.Sort = "CODE"
            'Me.BindListControlToDataView(Me.moMonthDropDownList, dv, , , True)
            Dim monthLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("MONTH", ElitaPlusIdentity.Current.ActiveUser.LanguageCode)
            Me.moMonthDropDownList.Populate(monthLkl, New PopulateOptions() With
           {
              .AddBlankItem = True
           })
        End Sub

        Private Sub PopulateCompaniesDropdown()
            Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()

            '''Me.UserCompanyMultipleDrop.NothingSelected = False
            '''Me.UserCompanyMultipleDrop.Caption = Me.TranslateLabelOrMessage("SELECT_COMPANY")
            '''UserCompanyMultipleDrop.BindData(dv)
            '''If UserCompanyMultipleDrop.Count.Equals(ONE_ITEM) Then
            '''    HideHtmlElement("ddSeparator")
            '''    UserCompanyMultipleDrop.Visible = False
            '''End If

            UserCompanyMultipleDrop.NothingSelected = True
            UserCompanyMultipleDrop.SetControl(False, UserCompanyMultipleDrop.MODES.NEW_MODE, True, dv, "* " & TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY), True)
            If dv.Count.Equals(ONE_ITEM) Then
                HideHtmlElement("ddSeparator")
                UserCompanyMultipleDrop.SelectedIndex = Me.ONE_ITEM
                UserCompanyMultipleDrop.Visible = False
            End If
        End Sub

        Private Sub InitializeForm()
            PopulateYearsDropdown()
            PopulateMonthsDropdown()
            PopulateCompaniesDropdown()
        End Sub

        Private Sub btnGenRpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRpt.Click
            Try
                'Dim oCompanyId As Guid = Me.GetApplicationUser.CompanyID
                'Dim compCode As String = LookupListNew.GetCodeFromId("COMPANIES", oCompanyId)

                Dim compCode As String = UserCompanyMultipleDrop.SelectedCode
                Dim compDesc As String = UserCompanyMultipleDrop.SelectedDesc

                Dim selectedYear As String = Me.GetSelectedDescription(Me.moYearDropDownList)
                Dim selectedMonthID As Guid = Me.GetSelectedItem(Me.moMonthDropDownList)
                Dim selectedMonth As String = LookupListNew.GetCodeFromId(LookupListNew.LK_MONTHS, selectedMonthID)
                Dim selectedYearMonth As String = selectedYear & selectedMonth

                If selectedMonthID.Equals(Guid.Empty) OrElse selectedYear.Equals(String.Empty) Then
                    ElitaPlusPage.SetLabelError(Me.moMonthYear)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_YEARMONTH_MUST_BE_SELECTED_ERR)
                End If

                'Validating the Company selection
                If UserCompanyMultipleDrop.SelectedGuid.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(UserCompanyMultipleDrop.CaptionLabel)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
                End If

                ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

                Dim params As ReportCeBaseForm.Params = SetParameters(compCode, compDesc, selectedYearMonth)
                Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Function SetParameters(ByVal companyCode As String, ByVal companyDesc As String, ByVal selectedYearMonth As String) As ReportCeBaseForm.Params

            Dim params As New ReportCeBaseForm.Params
            Dim reportName As String = RPT_FILENAME

            Dim exportData As String = NO
            Dim isSummary As String = YES

            moReportFormat = ReportCeBase.GetReportFormat(Me)
            If (moReportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (moReportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                reportName = RPT_FILENAME_EXPORT
                isSummary = NO
            End If

            Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
                    { _
                     New ReportCeBaseForm.RptParam("V_COMPANY", companyCode), _
                     New ReportCeBaseForm.RptParam("V_COMPANYDESC", companyDesc), _
                     New ReportCeBaseForm.RptParam("V_YEARMONTH", selectedYearMonth), _
                     New ReportCeBaseForm.RptParam("V_IS_SUMMARY", isSummary)}

            With params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = moReportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With
            Return params
        End Function



    End Class
End Namespace