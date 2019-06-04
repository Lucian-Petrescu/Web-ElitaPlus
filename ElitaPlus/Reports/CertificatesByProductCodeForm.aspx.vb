Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Reports


    Partial Class CertificatesByProductCodeForm
        Inherits ElitaPlusPage

#Region "Parameters"

        Public Structure ReportParams
            Public companyCode As String
            Public dealerCode As String
            Public svcCenterCode As String
            Public numberActiveDays As String
            Public includeAllClaims As String
            Public sortOrder As String
            Public langCode As String
            Public svcCenterName As String
        End Structure

#End Region

#Region "Constants"

        '   Private Const RPT_FILENAME As String = "Claims Following Service Warranty English (USA)_TEST7"
        Private Const RPT_FILENAME_WINDOW As String = "CERTIFICATES BY PRODUCT CODE REPORT CRITERIA"
        Private Const RPT_FILENAME As String = "Certificates By Product Code"
        Private Const RPT_SUBREPORT As String = "Certificates By Product Code SubReport.rpt"
        Private Const RPT_SUBREPORT2 As String = "Certificates By Product Code SubReport Totals.rpt"
        Private Const RPT_FILENAME_EXPORT As String = "Certificates By Product Code-Exp"

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

        Public Const BY_DEALER As String = "0"
        Public Const BY_PRODUCT_CODE As String = "1"

        Public Const SORT_BY_DEALER As String = "D"
        Public Const SORT_BY_PRODUCT_CODE As String = "P"

        Public Const ALL As String = "*"
        Public Const NOTHING_SELECTED As Int16 = -1
        Public Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Public Const YES As String = "Y"
        Public Const NO As String = "N"

        Private Const TOTALPARAMS As Integer = 23  ' 24
        Private Const TOTALEXPPARAMS As Integer = 7  ' 8
        Private Const PARAMS_PER_REPORT As Integer = 8
        Private Const LABEL_SELECT_DEALER As String = "OR A SINGLE DEALER"

        Protected WithEvents isProgressVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents isReportCeVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents moReportCeStatus As System.Web.UI.HtmlControls.HtmlInputHidden
        Public Const DEFAULT_NUMBER_ACTIVE_DAYS As String = "3"
        Protected WithEvents moDaysActiveLabel As System.Web.UI.WebControls.Label
        Protected WithEvents lblCode As System.Web.UI.WebControls.Label
        Protected WithEvents lblDescription As System.Web.UI.WebControls.Label

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
#End Region

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents RadiobuttonEXCEL As System.Web.UI.WebControls.RadioButton
        Protected WithEvents RadiobuttonAllClaims As System.Web.UI.WebControls.RadioButton
        Protected WithEvents RadiobuttonExcludeRepairedClaims As System.Web.UI.WebControls.RadioButton
        Protected WithEvents txtActiveDays As System.Web.UI.WebControls.TextBox
        Protected WithEvents moSortByLabel As System.Web.UI.WebControls.Label
        Protected WithEvents ErrorCtrl As ErrorController
        Protected WithEvents moReportCeInputControl As ReportCeInputControl
        Protected WithEvents moDealerMultipleDrop As MultipleColumnDDLabelControl

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
            Try
                If Not Me.IsPostBack Then
                    InitializeForm()
                Else
                    ClearErrLabels()
                End If
                '    Me.DisplayProgressBarOnClick(Me.btnGenRpt, "Loading_Claims")
                Me.InstallProgressBar()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
            Me.ShowMissingTranslations(Me.ErrorCtrl)
        End Sub

        Sub PopulateDealerDropDown()
            Dim dv As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE")
            DealerMultipleDrop.NothingSelected = True
            DealerMultipleDrop.SetControl(False, DealerMultipleDrop.MODES.NEW_MODE, True, dv, TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER), True, True, " document.forms[0].rdealer.checked = false;")
        End Sub

        Sub PopulateProductCodeDropDown()
            Dim productcodeList As New Collections.Generic.List(Of DataElements.ListItem)
            Dim oListContext As New ListContext

            For Each company_id As Guid In ElitaPlusIdentity.Current.ActiveUser.Companies
                oListContext.CompanyId = company_id
                Dim productcodeListForCompany As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="ProductCodeByCompany", context:=oListContext)
                If productcodeListForCompany.Count > 0 Then
                    If Not productcodeList Is Nothing Then
                        productcodeList.AddRange(productcodeListForCompany)
                    Else
                        productcodeList = productcodeListForCompany.Clone()
                    End If
                End If
            Next

            Me.cboProduct.Populate(productcodeList.ToArray(), New PopulateOptions() With
                {
                   .AddBlankItem = True,
                   .TextFunc = AddressOf .GetCode,
                   .ValueFunc = AddressOf .GetListItemId,
                   .SortFunc = AddressOf .GetCode
                })

            'Me.BindListControlToDataView(Me.cboProduct, LookupListNew.GetProductCodeByCompanyLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies), "CODE")
        End Sub

        Private Sub OnFromDrop_Changed(ByVal fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
         Handles multipleDropControl.SelectedDropChanged
            Try
                If DealerMultipleDrop.SelectedIndex > 0 Then
                    PopulateCampaignNumbersDropdown()
                End If
            Catch ex As Exception
                HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Sub PopulateCampaignNumbersDropdown()
            Dim dv As DataView
            Dim i As Integer
            dv = LookupListNew.GetCampaignNumberLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
            Me.cboCampaignNumber.Items.Clear()
            Me.cboCampaignNumber.Items.Add(New ListItem("", ""))
            If Not dv Is Nothing Then
                For i = 0 To dv.Count - 1
                    Me.cboCampaignNumber.Items.Add(New ListItem(dv(i)("campaign_number").ToString, dv(i)("campaign_number").ToString))
                Next
            End If
        End Sub

        Private Sub InitializeForm()
            PopulateDealerDropDown()
            PopulateProductCodeDropDown()
            PopulateCampaignNumbersDropdown()
            Me.rbProduct.Checked = True
            Me.rdealer.Checked = True
            Me.rbCampaignNumber.Checked = True
            RadiobuttonTotalsOnly.Checked = True
            Me.rdReportSortOrder.Items(0).Selected = True
        End Sub


        Private Sub ClearErrLabels()
            Me.ClearLabelErrSign(moProductLabel)
            Me.ClearLabelErrSign(moCampaignNumberLabel)
            Me.ClearLabelErrSign(DealerMultipleDrop.CaptionLabel)
            If Me.rdealer.Checked Then DealerMultipleDrop.SelectedIndex = -1
        End Sub

        Private Sub btnGenRpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRpt.Click
            Try
                GenerateReport()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Function SetParameters(ByVal userId As String,
                               ByVal dealerCode As String,
                               ByVal productCode As String,
                               ByVal campaign As String,
                               ByVal isSummary As String,
                               ByVal sortOrder As String) As ReportCeBaseForm.Params

            Dim params As New ReportCeBaseForm.Params
            Dim reportName As String = RPT_FILENAME
            Dim moReportFormat As ReportCeBaseForm.RptFormat
            Dim exportData As String = NO
            Dim repParams() As ReportCeBaseForm.RptParam

            moReportFormat = ReportCeBase.GetReportFormat(Me)

            If (moReportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (moReportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                exportData = YES
                reportName = RPT_FILENAME_EXPORT
                isSummary = NO
                repParams = New ReportCeBaseForm.RptParam() _
                                {
                                 New ReportCeBaseForm.RptParam("V_USER_ID", userId),
                                 New ReportCeBaseForm.RptParam("V_DEALER", dealerCode),
                                 New ReportCeBaseForm.RptParam("V_PRODUCT_CODE", productCode),
                                 New ReportCeBaseForm.RptParam("V_CAMPAIGN_NUMBER", campaign),
                                 New ReportCeBaseForm.RptParam("V_IS_SUMMARY", isSummary),
                                 New ReportCeBaseForm.RptParam("V_SORT_ORDER", sortOrder)}
            Else
                repParams = New ReportCeBaseForm.RptParam() _
                                {
                                New ReportCeBaseForm.RptParam("V_USER_ID", userId),
                                New ReportCeBaseForm.RptParam("V_DEALER", dealerCode),
                                New ReportCeBaseForm.RptParam("V_PRODUCT_CODE", productCode),
                                New ReportCeBaseForm.RptParam("V_CAMPAIGN_NUMBER", campaign),
                                New ReportCeBaseForm.RptParam("V_IS_SUMMARY", isSummary),
                                New ReportCeBaseForm.RptParam("V_SORT_ORDER", sortOrder)} ' _
                'New ReportCeBaseForm.RptParam("V_IS_SUMMARY", "Y", RPT_SUBREPORT), _
                'New ReportCeBaseForm.RptParam("V_IS_SUMMARY", "Y", RPT_SUBREPORT2)                
            End If

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
            Dim userID As Guid = ElitaPlusIdentity.Current.ActiveUser.Id
            Dim dealerID As Guid = DealerMultipleDrop.SelectedGuid 'Me.GetSelectedItem(Me.cboDealer)
            Dim selectedProduct As String = Me.GetSelectedDescription(Me.cboProduct)
            Dim selectedCampaign As String = Me.GetSelectedDescription(Me.cboCampaignNumber)
            'Dim dv As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE")
            Dim dealerCode As String = DealerMultipleDrop.SelectedCode 'LookupListNew.GetCodeFromId(dv, dealerID)
            Dim selectedDealer As String = DealerMultipleDrop.SelectedDesc
            Dim isSummary As String = NO
            Dim sortOrder As String

            If Me.rdealer.Checked Then
                dealerCode = ALL
            Else
                If dealerID.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(DealerMultipleDrop.CaptionLabel)
                    Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                End If
            End If

            If Me.rbProduct.Checked Then
                selectedProduct = ALL
            Else
                If selectedProduct.Equals(String.Empty) Then
                    ElitaPlusPage.SetLabelError(moProductLabel)
                    Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_PRODUCT_CODE_MUST_BE_SELECTED_ERR)
                End If
            End If

            If Me.rbCampaignNumber.Checked Then
                selectedCampaign = ALL
            Else
                If selectedCampaign.Equals(String.Empty) Then
                    ElitaPlusPage.SetLabelError(moCampaignNumberLabel)
                    Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_CAMPAIGN_MUST_BE_SELECTED_ERR)
                End If
            End If

            Select Case Me.rdReportSortOrder.SelectedValue()
                Case BY_DEALER
                    sortOrder = SORT_BY_DEALER
                Case BY_PRODUCT_CODE
                    sortOrder = SORT_BY_PRODUCT_CODE
            End Select

            If Me.RadiobuttonTotalsOnly.Checked() Then
                isSummary = YES
            End If

            ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

            Dim params As ReportCeBaseForm.Params = SetParameters(GuidControl.GuidToHexString(userID), _
                                                                  dealerCode, _
                                                                  selectedProduct, _
                                                                  selectedCampaign, _
                                                                  isSummary, _
                                                                  sortOrder)
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

        End Sub

    End Class
End Namespace

