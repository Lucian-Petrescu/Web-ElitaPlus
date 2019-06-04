Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Text
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Reports
    Partial Class MonthlyLossAndPremiumForm
        Inherits ElitaPlusPage

#Region "Constants"

        Private Const LOSS_RPT_FILENAME_WINDOW As String = "MONTHLY_LOSS"
        Private Const PREMIUN_RPT_FILENAME_WINDOW As String = "MONTHLY_PREMIUM"
        'Private Const RPT_FILENAME As String = "MonthlyLossAndPremium_Exp"
        Private Const LOSS_RPT_FILENAME_EXPORT As String = "MonthlyLoss_Exp"
        Private Const PREMIUM_RPT_FILENAME_EXPORT As String = "MonthlyPremium_Exp"


        Public Const CRYSTAL As String = "0"
        'Public Const PDF As String = "1"
        Public Const HTML As String = "2"
        Public Const CSV As String = "3"
        'Public Const EXCEL As String = "4"

        Public Const VIEW_REPORT As String = "1"
        Public Const PDF As String = "2"
        Public Const TAB As String = "3"
        Public Const EXCEL As String = "4"
        Public Const EXCEL_DATA_ONLY As String = "5"
        Public Const JAVA As String = "6"
        Public Const ALL As String = "*"
        Public Const NOTHING_SELECTED As Int16 = -1
        Public Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Public Const YES As String = "Y"
        Public Const NO As String = "N"
        Private Const ONE_ITEM As Integer = 1
        Private Const LABEL_SELECT_COMPANY As String = "SELECT_COMPANY"

#End Region

#Region "variables"
        Dim reportFormat As ReportCeBaseForm.RptFormat
        Dim reportName As String = LOSS_RPT_FILENAME_EXPORT
        Dim fileName As String = LOSS_RPT_FILENAME_WINDOW
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
        Protected WithEvents ErrorCtrl As ErrorController
        Protected WithEvents moReportCeInputControl As ReportCeInputControl
        Protected WithEvents moUserCompanyMultipleDrop As MultipleColumnDDLabelControl
        'Protected WithEvents moUserCompanyMultipleDrop as 

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Handlers-DropDown"

        Private Sub OnFromDrop_Changed(ByVal fromMultipleDrop As MultipleColumnDDLabelControl) _
                        Handles moUserCompanyMultipleDrop.SelectedDropChanged
            Try
                PopulateDealerDropDown()
            Catch ex As Exception
                HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Me.ErrorCtrl.Clear_Hide()
            Me.ClearLabelsErrSign()
            Try
                If Not Me.IsPostBack Then
                    InitializeForm()
                End If
                Me.InstallProgressBar()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
            Me.ShowMissingTranslations(Me.ErrorCtrl)

        End Sub
        Public Sub ClearLabelsErrSign()
            Try
                Me.ClearLabelErrSign(MonthYearLabel)
                Me.ClearLabelErrSign(UserCompanyMultipleDrop.CaptionLabel)
                Me.ClearLabelErrSign(DealerLabel)

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
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
            UserCompanyMultipleDrop.SetControl(True, UserCompanyMultipleDrop.MODES.NEW_MODE, True, dv, TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY), True)
            If dv.Count.Equals(ONE_ITEM) Then
                HideHtmlElement("ddSeparator")
                UserCompanyMultipleDrop.SelectedIndex = Me.ONE_ITEM
                UserCompanyMultipleDrop.Visible = False
            End If
        End Sub

        Private Sub PopulateDealerDropDown()
            '  Me.BindListControlToDataView(Me.cboDealer, LookupListNew.GetDealerLookupList(UserCompanyMultipleDrop.SelectedGuid)) 'DealerListByCompany
            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyId = UserCompanyMultipleDrop.SelectedGuid
            Dim countyLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("DealerListByCompany", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            Me.cboDealer.Populate(countyLkl, New PopulateOptions() With
                     {
                  .AddBlankItem = True
                    })


        End Sub

        Private Sub PopulateYearsDropdown()
            ' Dim dv As DataView = AccountingCloseInfo.GetClosingYears(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
            'Me.BindListTextToDataView(Me.YearDropDownList, dv, , , True)
            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyId = ElitaPlusIdentity.Current.ActiveUser.CompanyId
            Dim YearListLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ClosingYearsByCompany", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            Me.YearDropDownList.Populate(YearListLkl, New PopulateOptions() With
           {
          .AddBlankItem = True,
          .ValueFunc = AddressOf PopulateOptions.GetCode,
           .BlankItemValue = "0"
          })
            'Dim yearMonth As String = Me.GetSelectedDescription(Me.YearDropDownList)
        End Sub

        Private Sub PopulateMonthsDropdown()
            'Dim dv As DataView = LookupListNew.GetMonthsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            ' dv.Sort = "CODE"
            'Me.BindListControlToDataView(Me.MonthDropDownList, dv, , , True)
            Dim monthLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("MONTH", Thread.CurrentPrincipal.GetLanguageCode())
            Me.MonthDropDownList.Populate(monthLkl, New PopulateOptions() With
           {
              .AddBlankItem = True
           })

        End Sub

        Private Sub InitializeForm()
            PopulateCompaniesDropdown()
            PopulateYearsDropdown()
            PopulateMonthsDropdown()
            PopulateDealerDropDown()
            TheRptCeInputControl.SetExportOnly()
            Me.rdealer.Checked = True
            ' Me.RadiobuttonLossReport.Checked = True
        End Sub

        Private Sub btnGenRpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRpt.Click

            Try
                'Dim companyId As Guid = Me.GetApplicationUser.CompanyID
                'Dim compCode As String = LookupListNew.GetCodeFromId("COMPANIES", companyId)
                Dim compCode As String = UserCompanyMultipleDrop.SelectedCode
                Dim selectedYear As String = Me.GetSelectedDescription(Me.YearDropDownList)
                Dim selectedMonthID As Guid = Me.GetSelectedItem(Me.MonthDropDownList)
                Dim selectedMonth As String = LookupListNew.GetCodeFromId(LookupListNew.LK_MONTHS, selectedMonthID)
                Dim selectedYearMonth As String = selectedYear & selectedMonth
                Dim selectedDealerId As Guid = Me.GetSelectedItem(Me.cboDealer)
                Dim dvDealer As DataView = LookupListNew.GetDealerLookupList(UserCompanyMultipleDrop.SelectedGuid)
                Dim dealerCode As String = LookupListNew.GetCodeFromId(dvDealer, selectedDealerId)

                'Validating the Year-Month selection
                If selectedMonthID.Equals(Guid.Empty) OrElse selectedYear.Equals(String.Empty) Then
                    ElitaPlusPage.SetLabelError(Me.MonthYearLabel)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_YEARMONTH_MUST_BE_SELECTED_ERR)
                End If

                'Validating the Company selection
                If UserCompanyMultipleDrop.SelectedGuid.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(UserCompanyMultipleDrop.CaptionLabel)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
                End If

                'Validating the Dealer selection
                If Me.rdealer.Checked Then
                    dealerCode = ALL
                Else
                    If selectedDealerId.Equals(Guid.Empty) Then
                        ElitaPlusPage.SetLabelError(DealerLabel)
                        Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                    End If
                End If

                ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)
                Dim params As ReportCeBaseForm.Params = SetParameters(compCode, selectedYearMonth, dealerCode)
                Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try

        End Sub

        Function SetParameters(ByVal companyCode As String, ByVal selectedYearMonth As String, ByVal dealerCode As String) As ReportCeBaseForm.Params

            Dim reportFormat As ReportCeBaseForm.RptFormat
            Dim params As New ReportCeBaseForm.Params

            Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
                    { _
                     New ReportCeBaseForm.RptParam("V_COMPANY", companyCode), _
                     New ReportCeBaseForm.RptParam("V_DEALER", dealerCode), _
                     New ReportCeBaseForm.RptParam("V_ACCOUNTING_DATE", selectedYearMonth)}

            reportFormat = ReportCeBase.GetReportFormat(Me)

            With params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(fileName)
                .moRptFormat = reportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With
            Return params
        End Function

    End Class

End Namespace