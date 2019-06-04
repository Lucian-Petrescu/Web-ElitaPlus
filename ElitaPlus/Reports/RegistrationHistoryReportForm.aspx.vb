Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Namespace Reports
    Partial Public Class RegistrationHistoryReportForm
        Inherits ElitaPlusPage

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "REGISTRATION_HISTORY"
        Private Const RPT_FILENAME As String = "RegistrationHistory"
        Public Const ALL As String = "*"
        Public Const YES As String = "Y"
        Protected WithEvents btnViewHidden As System.Web.UI.WebControls.Button
        Public Const NO As String = "N"
        Private Const LABEL_SELECT_COMPANY As String = "SELECT_COMPANY"
        Private Const RPT_FILENAME_EXPORT As String = "RegistrationHistory-Exp"

        Private Const LABEL_SELECT_DEALER As String = "OR A SINGLE DEALER"

        Private Const TOTALPARAMS As Integer = 6  ' 23
        Private Const TOTALEXPPARAMS As Integer = 6  ' 7
        Private Const PARAMS_PER_REPORT As Integer = 6 '8
        Private Const ONE_ITEM As Integer = 1
        'Private Const LABEL_OR_A_SINGLE_DEALER As String = "OR A SINGLE DEALER"


#End Region

#Region "parameters"
        Public Structure ReportParams
            Public companyId As String
            Public companyDesc As String
            Public dealerCode As String
            Public dealerDesc As String
            Public beginDate As String
            Public endDate As String
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
                    moDealerMultipleDrop = CType(FindControl("moDealerMultipleDrop"), MultipleColumnDDLabelControl)
                End If
                Return moDealerMultipleDrop
            End Get
        End Property

        Public ReadOnly Property UserCompanyMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If moUserCompanyMultipleDrop Is Nothing Then
                    moUserCompanyMultipleDrop = CType(FindControl("moUserCompanyMultipleDrop"), MultipleColumnDDLabelControl)
                End If
                Return moUserCompanyMultipleDrop
            End Get
        End Property
#End Region

#Region "Handlers"


#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents moReportCeInputControl As ReportCeInputControl
        Protected WithEvents ErrorCtrl As ErrorController
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
            Try
                If Not Me.IsPostBack Then
                    InitializeForm()
                    TheRptCeInputControl.ExcludeExport()
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

#Region "Handlers-DropDown"

        Private Sub OnFromDrop_Changed(ByVal fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
            Handles moUserCompanyMultipleDrop.SelectedDropChanged
            Try
                PopulateDealerDropDown()
            Catch ex As Exception
                HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

#End Region

#End Region

#Region "Clear"

        Private Sub ClearErrLabels()
            Me.ClearLabelErrSign(moBeginDateLabel)
            Me.ClearLabelErrSign(moEndDateLabel)
            Me.ClearLabelErrSign(DealerMultipleDrop.CaptionLabel)
            Me.ClearLabelErrSign(UserCompanyMultipleDrop.CaptionLabel)
            If Me.rdealer.Checked Then DealerMultipleDrop.SelectedIndex = -1
        End Sub

#End Region

#Region "Populate"

        Private Sub PopulateCompaniesDropdown()
            Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()

            UserCompanyMultipleDrop.NothingSelected = True
            UserCompanyMultipleDrop.SetControl(True, UserCompanyMultipleDrop.MODES.NEW_MODE, True, dv, TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY), True)
            If dv.Count.Equals(ONE_ITEM) Then
                HideHtmlElement("ddSeparator")
                UserCompanyMultipleDrop.SelectedIndex = Me.ONE_ITEM
                UserCompanyMultipleDrop.Visible = False

            End If
        End Sub
        Private Sub PopulateDealerDropDown()
            Dim dv As DataView = LookupListNew.GetDealerLookupList(New Guid(UserCompanyMultipleDrop.SelectedGuid.ToString))
            DealerMultipleDrop.NothingSelected = True
            DealerMultipleDrop.SetControl(False,
                                              DealerMultipleDrop.MODES.NEW_MODE,
                                              True,
                                              dv,
                                              TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER),
                                              True,
                                              False,
                                              " document.forms[0].rdealer.checked = false;",
                                              "moDealerMultipleDrop_moMultipleColumnDrop",
                                              "moDealerMultipleDrop_moMultipleColumnDropDesc", "moDealerMultipleDrop_lb_DropDown")

        End Sub

        Private Sub InitializeForm()

            PopulateCompaniesDropdown()
            PopulateDealerDropDown()
            Dim t As Date = Date.Now.AddMonths(-1).AddDays(1)
            Me.moBeginDateText.Text = GetDateFormattedString(t)
            Me.moEndDateText.Text = GetDateFormattedString(Date.Now)
            Me.rdealer.Checked = True
            TheRptCeInputControl.populateReportLanguages(RPT_FILENAME)
        End Sub

#End Region

#Region "Crystal Enterprise"

        Function SetParameters(ByVal companyid As String, ByVal companydesc As String, ByVal dealerCode As String, ByVal dealerDesc As String, ByVal beginDate As String,
                                  ByVal endDate As String) As ReportCeBaseForm.Params

            Dim Params As New ReportCeBaseForm.Params
            Dim repParams(TOTALPARAMS) As ReportCeBaseForm.RptParam
            Dim rptParams As ReportParams
            moReportFormat = ReportCeBase.GetReportFormat(Me)
            Dim culturevalue As String = TheRptCeInputControl.getCultureValue(False)
            Dim reportName As String = TheRptCeInputControl.getReportName(RPT_FILENAME, False)

            With rptParams
                .companyId = companyid
                .companyDesc = companydesc
                .dealerCode = dealerCode
                .dealerDesc = dealerDesc
                .beginDate = beginDate
                .endDate = endDate
                .culturevalue = culturevalue
            End With
            SetReportParams(rptParams, repParams, String.Empty, PARAMS_PER_REPORT * 0)     ' Main Report            

            Me.rptWindowTitle.InnerText = TheRptCeInputControl.getReportWindowTitle(TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW))

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
                repParams(startIndex) = New ReportCeBaseForm.RptParam("V_COMPANY_ID", .companyId, reportName)
                repParams(startIndex + 1) = New ReportCeBaseForm.RptParam("V_COMPANYDESC", .companyDesc, reportName)
                repParams(startIndex + 2) = New ReportCeBaseForm.RptParam("V_DEALER", .dealerCode, reportName)
                repParams(startIndex + 3) = New ReportCeBaseForm.RptParam("V_DEALER_DESC", .dealerDesc, reportName)
                repParams(startIndex + 4) = New ReportCeBaseForm.RptParam("V_BEGIN_DATE", .beginDate, reportName)
                repParams(startIndex + 5) = New ReportCeBaseForm.RptParam("V_END_DATE", .endDate, reportName)
                repParams(startIndex + 6) = New ReportCeBaseForm.RptParam("LANG_CULTURE_VALUE", .culturevalue, reportName)
            End With
            '  End If
        End Sub
        Private Sub GenerateReport()
            Dim userId As Guid = ElitaPlusIdentity.Current.ActiveUser.Id
            Dim selectedDealerId As Guid = DealerMultipleDrop.SelectedGuid 'Me.GetSelectedItem(Me.cboDealerDec)
            Dim dealerCode As String = DealerMultipleDrop.SelectedCode 'LookupListNew.GetCodeFromId(dv, selectedDealerId)
            Dim dealerDesc As String = DealerMultipleDrop.SelectedDesc
            Dim endDate As String
            Dim beginDate As String
            'Dim oCompanyId As Guid = ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID).Id
            Dim CompanyCode As String = UserCompanyMultipleDrop.SelectedCode
            Dim CompanyDesc As String = UserCompanyMultipleDrop.SelectedDesc
            Dim params As ReportCeBaseForm.Params
            Dim totalsByCov As String

            'Validating the Company selection
            If UserCompanyMultipleDrop.SelectedGuid.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(UserCompanyMultipleDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
            End If

            'Dates
            ReportCeBase.ValidateBeginEndDate(moBeginDateLabel, moBeginDateText.Text, moEndDateLabel, moEndDateText.Text)
            endDate = ReportCeBase.FormatDate(moEndDateLabel, moEndDateText.Text)
            beginDate = ReportCeBase.FormatDate(moBeginDateLabel, moBeginDateText.Text)

            'Dealer
            If Me.rdealer.Checked Then
                dealerCode = ALL
            Else
                If selectedDealerId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(DealerMultipleDrop.CaptionLabel)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                End If
            End If

            ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

            params = SetParameters(GuidControl.GuidToHexString(UserCompanyMultipleDrop.SelectedGuid), CompanyDesc, dealerCode, dealerDesc, beginDate, endDate)

            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

        End Sub

#End Region
    End Class
End Namespace
