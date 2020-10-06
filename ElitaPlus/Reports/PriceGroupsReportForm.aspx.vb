Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common

Namespace Reports

    Partial Class PriceGroupsReportForm
        Inherits ElitaPlusPage

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "PRICE_LISTS"
        Private Const RPT_FILENAME As String = "PriceLists"
        Private Const RPT_FILENAME_EXPORT As String = "PriceLists_Exp"

        Private Const TOTAL_PARAMS As Integer = 7 ' 8 Elements
        Private Const TOTAL_EXP_PARAMS As Integer = 3 ' 4 Elements
        Private Const PARAMS_PER_REPORT As Integer = 4 ' 4 Elements
        Private Const ALL As String = "*"
        Private Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Private Const YES As String = "Y"
        Private Const NO As String = "N"
        Private Const LABEL_SELECT_PRICE_LIST As String = "Or Only Price List"

#End Region

#Region "Parameters"

        Public Structure ReportParams
            Public companyCode As String
            Public priceList As String
        End Structure

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
        Public ReadOnly Property PriceListMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If moPriceGroupMultipleDrop Is Nothing Then
                    moPriceGroupMultipleDrop = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
                End If
                Return moPriceGroupMultipleDrop
            End Get
        End Property
#End Region

#Region "variables"
        Private reportFormat As ReportCeBaseForm.RptFormat
        Private reportName As String = RPT_FILENAME
#End Region

#Region "Handlers"


#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents moPriceGroupMultipleDrop As MultipleColumnDDLabelControl
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

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            ErrorCtrl.Clear_Hide()
            Try
                If Not IsPostBack Then
                    InitializeForm()
                    'TheReportCeInputControl.populateReportLanguages(RPT_FILENAME)
                Else
                    ClearErrLabels()
                End If
                InstallProgressBar()
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
            ShowMissingTranslations(ErrorCtrl)
        End Sub

        Private Sub ClearErrLabels()
            ClearLabelErrSign(PriceListMultipleDrop.CaptionLabel)
            If rPriceLists.Checked Then PriceListMultipleDrop.SelectedIndex = -1
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

#End Region

#Region "Populate"

        Private Sub PopulatePriceListsDropDown()

            '''Me.BindListControlToDataView(Me.cboPriceGroupsDec, _
            '''    LookupListNew.GetPriceGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID).Id), , , True)
            '''Dim PriceGroupLookupListSortedByCode As DataView = LookupListNew.GetPriceGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID).Id)
            '''PriceGroupLookupListSortedByCode.Sort = "CODE"
            '''Me.BindListControlToDataView(Me.cboPriceGroupsCode, PriceGroupLookupListSortedByCode, "CODE", , True)
            ''''Me.BindListControlToDataView(Me.cboPriceGroupsCode, LookupListNew.GetPriceGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyId), "CODE", , True)

            'Dim dv As DataView = LookupListNew.GetPriceGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID).Id)
            Dim dv As DataView = LookupListNew.GetPriceListLookupList(ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID).Id)
            PriceListMultipleDrop.NothingSelected = True
            PriceListMultipleDrop.SetControl(False, PriceListMultipleDrop.MODES.NEW_MODE, True, dv, TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_PRICE_LIST), True, True, " document.forms[0].rPriceLists.checked = false;")

        End Sub

        Private Sub InitializeForm()
            PopulatePriceListsDropDown()
            rPriceLists.Checked = True
        End Sub

#End Region

#Region "Crystal Enterprise"

        Function SetParameters(companyCode As String, priceList As String) As ReportCeBaseForm.Params

            Dim reportFormat As ReportCeBaseForm.RptFormat
            'reportName = TheReportCeInputControl.getReportName(RPT_FILENAME, False)
            'Dim culturevalue As String = TheReportCeInputControl.getCultureValue(False)
            Dim params As New ReportCeBaseForm.Params
            '''Dim repParams(TOTAL_PARAMS) As ReportCeBaseForm.RptParam
            'Dim repParams() As ReportCeBaseForm.RptParam
            Dim rptParams As ReportParams

            With rptParams
                .companyCode = companyCode
                .priceList = priceList
            End With

            reportFormat = ReportCeBase.GetReportFormat(Me)

            Dim exportData As String = NO

            If (reportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (reportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                exportData = YES
                reportName = RPT_FILENAME_EXPORT
                'reportName = TheReportCeInputControl.getReportName(RPT_FILENAME_EXPORT, True)
                'culturevalue = TheReportCeInputControl.getCultureValue(True)
            End If

            'Me.rptWindowTitle.InnerText = TheReportCeInputControl.getReportWindowTitle(RPT_FILENAME_WINDOW)

            Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
                         {
                           New ReportCeBaseForm.RptParam("V_COMPANY", companyCode),
                           New ReportCeBaseForm.RptParam("V_PRICE_LIST", priceList)}
            ' New ReportCeBaseForm.RptParam("LANG_CULTURE_VALUE", culturevalue)

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

            Dim companyId As Guid = ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID
            Dim compCode As String = LookupListNew.GetCodeFromId("COMPANIES", companyId)
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim languageCode As String = LookupListNew.GetCodeFromId("LANGUAGES", langId)
            Dim selectedPriceListId As Guid
            'Dim dvPriceGroups As DataView = LookupListNew.GetPriceGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID).Id)
            Dim priceList As String
            Dim detailCode As String



            If rPriceLists.Checked Then
                priceList = ALL
            Else
                selectedPriceListId = PriceListMultipleDrop.SelectedGuid 'Me.GetSelectedItem(Me.cboPriceGroupsCode)
                priceList = PriceListMultipleDrop.SelectedCode 'LookupListNew.GetCodeFromId(dvPriceGroups, selectedPriceGroupId)
                If selectedPriceListId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(PriceListMultipleDrop.CaptionLabel)
                    Throw New GUIException(Message.MSG_INVALID_PRICE_LIST, Assurant.ElitaPlus.Common.ErrorCodes.GUI_PRICE_LIST_MUST_BE_SELECTED_ERR)
                End If
            End If

            ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

            reportFormat = ReportCeBase.GetReportFormat(Me)

            Dim params As ReportCeBaseForm.Params = SetParameters(compCode, priceList)
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

        End Sub

        '''Sub SetReportParams(ByVal rptParams As ReportParams, ByVal repParams() As ReportCeBaseForm.RptParam, _
        '''                    ByVal rptName As String, ByVal startIndex As Integer)

        '''    With rptParams
        '''        repParams(startIndex) = New ReportCeBaseForm.RptParam("V_COMPANY", .companyCode, rptName)
        '''        repParams(startIndex + 1) = New ReportCeBaseForm.RptParam("V_DEALER_CODE", .priceGroup, rptName)
        '''    End With

        '''End Sub

#End Region

    End Class

End Namespace
