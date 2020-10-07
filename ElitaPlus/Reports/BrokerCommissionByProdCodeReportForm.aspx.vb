Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Reports
    Public Class BrokerCommissionByProdCodeReportForm
        Inherits ElitaPlusPage

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "BROKER_COMMISSION_BY_PRODUCT_CODE"
        Private Const RPT_FILENAME As String = "BrokerCommissionByProdCode"
        Private Const RPT_FILENAME_EXPORT As String = "BrokerCommissionByProdCode-Exp"

        Private Const TOTAL_PARAMS As Integer = 7 ' 8 Elements
        Private Const TOTAL_EXP_PARAMS As Integer = 7 ' 4 Elements
        Private Const PARAMS_PER_REPORT As Integer = 7 ' 4 Elements
        Private Const ALL As String = "*"
        Private Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Private Const YES As String = "Y"
        Private Const NO As String = "N"
        Private Const LABEL_SELECT_DEALER As String = "OR A SINGLE DEALER"
        Public Const PAGETITLE As String = "BROKER_COMMISSION_BY_PRODUCT_CODE"
        Public Const PAGETAB As String = "REPORTS"
        Public Const DOCTITLE As String = "BROKER_COMMISSION_BY_PRODUCT_CODE"
        Private Const ONE_ITEM As Integer = 1
#End Region

#Region "Parameters"

        Public Structure ReportParams
            Public userId As String
            Public beginDate As String
            Public endDate As String
            Public dealerCode As String
            Public detailCode As String
            Public productcode As String
            Public culturecode As String
        End Structure

#End Region

#Region "JavaScript"

        Public Sub JavascriptCalls()
            rdealer.Attributes.Add("onclick", "return ToggleDualDropDownsSelection('" + multipleDropControl.CodeDropDown.ClientID + "','" + multipleDropControl.DescDropDown.ClientID + "','" + rdealer.ClientID + "', " + "false , '');")
            rbProduct.Attributes.Add("onclick", "return ToggleSingleDropDownSelection('" + cboProduct.ClientID + "','" + rbProduct.ClientID + "',false);")
            cboProduct.Attributes.Add("onchange", "return ToggleSingleDropDownSelection('" + cboProduct.ClientID + "','" + rbProduct.ClientID + "',true);")
            RadiobuttonTotalsOnly.Attributes.Add("onclick", "return toggleRadioButtonSelection('" + RadiobuttonTotalsOnly.ClientID + "','" + RadiobuttonDetail.ClientID + "',false);")
            RadiobuttonDetail.Attributes.Add("onclick", "return toggleRadioButtonSelection('" + RadiobuttonTotalsOnly.ClientID + "','" + RadiobuttonDetail.ClientID + "',true);")
        End Sub
#End Region

#Region "variables"
        Private reportFormat As ReportCeBaseForm.RptFormat
        Private reportName As String = RPT_FILENAME
#End Region

#Region "Properties"
        Public ReadOnly Property DealerMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If multipleDropControl Is Nothing Then
                    multipleDropControl = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
                End If
                Return multipleDropControl
            End Get
        End Property
#End Region

#Region "Handlers"

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        ' Protected WithEvents ErrControllerMaster As ErrorController
        Protected WithEvents moReportCeInputControl As ReportCeInputControl
        Protected WithEvents moDealerMultipleDrop As MultipleColumnDDLabelControl
        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Handlers-Init"


        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            ErrControllerMaster.Clear_Hide()
            ClearLabelsErrSign()
            Title = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
            Try
                If Not IsPostBack Then
                    SetFormTitle(PAGETITLE)
                    SetFormTab(PAGETAB)
                    JavascriptCalls()
                    InitializeForm()
                    AddCalendar(BtnBeginDate, BeginDateText)
                    AddCalendar(BtnEndDate, EndDateText)
                End If
                InstallProgressBar()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
            ShowMissingTranslations(ErrControllerMaster)
        End Sub

        Public Sub ClearLabelsErrSign()
            Try
                ClearLabelErrSign(BeginDateLabel)
                ClearLabelErrSign(EndDateLabel)
                ClearLabelErrSign(DealerMultipleDrop.CaptionLabel)
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub
#End Region

#Region "Handlers-Buttons"

        Private Sub btnGenRpt_Click(sender As System.Object, e As System.EventArgs) Handles btnGenRpt.Click
            Try
                GenerateReport()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

#End Region

#Region "Handlers-DropDowns"

        ' Multiple Dealer Drop 
        Private Sub OnFromDrop_Changed(fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
           Handles multipleDropControl.SelectedDropChanged
            Try
                PopulateProductCodeDropDown()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub
#End Region

#End Region

#Region "Populate"

        'ALR Need to create new method to pull full list based on multiple company membership
        Private Sub PopulateDealerDropDown()
            Dim dv As DataView = LookupListNew.GetDealersCommPrdLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
            DealerMultipleDrop.NothingSelected = True
            DealerMultipleDrop.SetControl(True, DealerMultipleDrop.MODES.NEW_MODE, True, dv, TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER), True, True, " document.forms[0]." + rdealer.ClientID + ".checked = false;")
        End Sub

        Sub PopulateProductCodeDropDown()
            If DealerMultipleDrop.SelectedGuid <> Guid.Empty Then
                Dim listcontext As ListContext = New ListContext()
                listcontext.DealerId = DealerMultipleDrop.SelectedGuid

                Dim productcodeList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="ProductCodeByDealer", context:=listcontext)
                cboProduct.Populate(productcodeList, New PopulateOptions() With
                {
                   .AddBlankItem = True,
                   .TextFunc = AddressOf .GetCode,
                   .ValueFunc = AddressOf .GetListItemId,
                   .SortFunc = AddressOf .GetCode
                })
                'Me.BindListControlToDataView(Me.cboProduct, LookupListNew.GetProductCodeLookupList(DealerMultipleDrop.SelectedGuid), "CODE")
            End If
        End Sub

        Private Sub InitializeForm()
            Dim t As Date = Date.Now.AddDays(-1)
            BeginDateText.Text = GetDateFormattedString(t)
            EndDateText.Text = GetDateFormattedString(Date.Now)
            PopulateDealerDropDown()
            PopulateProductCodeDropDown()
            rdealer.Checked = True
            rbProduct.Checked = True
            RadiobuttonTotalsOnly.Checked = True
            TheReportCeInputControl.populateReportLanguages(RPT_FILENAME)
        End Sub

#End Region


#Region "Crystal Enterprise"

        Function SetParameters(userId As String, dealerCode As String, productCode As String, _
                               beginDate As String, endDate As String, _
                               detailCode As String) As ReportCeBaseForm.Params

            Dim reportFormat As ReportCeBaseForm.RptFormat
            Dim params As New ReportCeBaseForm.Params
            Dim repParams(TOTAL_PARAMS) As ReportCeBaseForm.RptParam
            Dim rptParams As ReportParams
            Dim culturecode As String = TheReportCeInputControl.getCultureValue(False)
            Dim reportName As String = TheReportCeInputControl.getReportName(RPT_FILENAME, False)


            With rptParams
                .userId = userId
                .beginDate = beginDate
                .endDate = endDate
                .dealerCode = dealerCode
                .productcode = productCode
                .detailCode = detailCode
                .culturecode = culturecode
            End With

            reportFormat = ReportCeBase.GetReportFormat(Me)

            Dim exportData As String = NO

            If (reportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (reportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                detailCode = YES
                exportData = YES
                reportName = TheReportCeInputControl.getReportName(RPT_FILENAME_EXPORT, True)
            End If

            repParams = New ReportCeBaseForm.RptParam() _
            { _
              New ReportCeBaseForm.RptParam("V_USER_KEY", userId), _
              New ReportCeBaseForm.RptParam("V_DEALER_CODE", dealerCode), _
              New ReportCeBaseForm.RptParam("V_PRODUCT_CODE", productCode), _
              New ReportCeBaseForm.RptParam("V_BEGIN_DATE", beginDate), _
               New ReportCeBaseForm.RptParam("V_END_DATE", endDate), _
              New ReportCeBaseForm.RptParam("V_IS_SUMMARY", detailCode), _
              New ReportCeBaseForm.RptParam("LANG_CULTURE_VALUE", culturecode) _
            }

            With params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = reportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With

            Return params
        End Function

        Private Sub GenerateReport()

            Dim userId As String = GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.Id)
            Dim selectedDealerId As Guid = DealerMultipleDrop.SelectedGuid 'Me.GetSelectedItem(Me.cboDealerDec)
            'Dim dvDealer As DataView = LookupListNew.GetDealerCommissionBreakDownLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE")
            Dim dealerCode As String = DealerMultipleDrop.SelectedCode 'LookupListNew.GetCodeFromId(dvDealer, selectedDealerId)
            Dim detailCode As String
            Dim params As ReportCeBaseForm.Params
            Dim endDate As String
            Dim beginDate As String
            Dim selectedProduct As String = GetSelectedDescription(cboProduct)

            If RadiobuttonTotalsOnly.Checked Then
                detailCode = YES
            Else
                detailCode = NO
            End If

            'Dates
            ReportCeBase.ValidateBeginEndDate(BeginDateLabel, BeginDateText.Text, EndDateLabel, EndDateText.Text)
            endDate = ReportCeBase.FormatDate(EndDateLabel, EndDateText.Text)
            beginDate = ReportCeBase.FormatDate(BeginDateLabel, BeginDateText.Text)

            'Validating the dealer selection
            If rdealer.Checked Then
                dealerCode = ALL
            Else

                If selectedDealerId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(DealerMultipleDrop.CaptionLabel)
                    Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                End If
            End If

            If rbProduct.Checked Then
                selectedProduct = ALL
            Else
                If selectedProduct.Equals(String.Empty) Then
                    ElitaPlusPage.SetLabelError(moProductLabel)
                    Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_PRODUCT_CODE_MUST_BE_SELECTED_ERR)
                End If
            End If

            ReportCeBase.EnableReportCe(Me, TheReportCeInputControl)

            reportFormat = ReportCeBase.GetReportFormat(Me)

            If (reportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (reportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                'Export Report
                detailCode = NO
                params = SetExpParameters(userId, beginDate, endDate, dealerCode, selectedProduct, detailCode)
            Else
                'View Report
                params = SetParameters(userId, dealerCode, selectedProduct, beginDate, endDate, detailCode)
            End If

            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params
        End Sub

        Function SetExpParameters(userId As String, beginDate As String, _
                                  endDate As String, dealerCode As String, _
                                  productCode As String, detailCode As String) As ReportCeBaseForm.Params

            Dim params As New ReportCeBaseForm.Params
            Dim repParams(TOTAL_EXP_PARAMS) As ReportCeBaseForm.RptParam
            Dim rptParams As ReportParams
            Dim culturecode As String = TheReportCeInputControl.getCultureValue(True)
            Dim reportName As String = TheReportCeInputControl.getReportName(RPT_FILENAME_EXPORT, True)


            With rptParams
                .userId = userId
                .beginDate = beginDate
                .endDate = endDate
                .dealerCode = dealerCode
                .productcode = productCode
                .detailCode = detailCode
                .culturecode = culturecode
            End With

            SetReportParams(rptParams, repParams, String.Empty, PARAMS_PER_REPORT * 0)     ' Main Report            

            With params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = reportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With

            Return params
        End Function

        Sub SetReportParams(rptParams As ReportParams, repParams() As ReportCeBaseForm.RptParam, _
                            rptName As String, startIndex As Integer)

            With rptParams
                repParams(startIndex) = New ReportCeBaseForm.RptParam("V_USER_KEY", .userId, rptName)
                repParams(startIndex + 1) = New ReportCeBaseForm.RptParam("V_DEALER_CODE", .dealerCode, rptName)
                repParams(startIndex + 2) = New ReportCeBaseForm.RptParam("V_PRODUCT_CODE", .productcode, rptName)
                repParams(startIndex + 3) = New ReportCeBaseForm.RptParam("V_BEGIN_DATE", .beginDate, rptName)
                repParams(startIndex + 4) = New ReportCeBaseForm.RptParam("V_END_DATE", .endDate, rptName)
                repParams(startIndex + 5) = New ReportCeBaseForm.RptParam("V_IS_SUMMARY", .detailCode, rptName)
                repParams(startIndex + 6) = New ReportCeBaseForm.RptParam("LANG_CULTURE_VALUE", .culturecode, rptName)
            End With

        End Sub

#End Region

    End Class
End Namespace

