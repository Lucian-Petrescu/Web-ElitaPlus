Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Microsoft.VisualBasic
Imports System.Globalization
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.Web.Forms

Namespace Reports

    Partial Public Class FelitaInterfaceDetailForm
        Inherits ElitaPlusPage


#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "FELITA_INTERFACE_DETAIL"
        Private Const RPT_FILENAME As String = "FelitaInterfaceDetail"
        Public Const ALL As String = "*"
        Public Const YES As String = "Y"
        Protected WithEvents btnViewHidden As System.Web.UI.WebControls.Button
        Public Const NO As String = "N"
        Private Const LABEL_SELECT_COMPANY As String = "SELECT_COMPANY"


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
        Public ReadOnly Property CompanyMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If moCompanyMultipleDrop Is Nothing Then
                    moCompanyMultipleDrop = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
                End If
                Return moCompanyMultipleDrop
            End Get
        End Property
#End Region

#Region "variables"
        Private moReportFormat As ReportCeBaseForm.RptFormat
        Public endDate As String
        Public beginDate As String
#End Region

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Protected WithEvents moCompanyMultipleDrop As MultipleColumnDDLabelControl
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
        Private Sub OnFromDrop_Changed(ByVal fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
                   Handles multipleDropControl.SelectedDropChanged
            Try
                If moBeginDateText.Text <> String.Empty Then
                    PopulateFileName()
                End If
            Catch ex As Exception
                HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub


#End Region

#Region "Clear"

        Private Sub ClearErrLabels()
            Me.ClearLabelErrSign(CompanyMultipleDrop.CaptionLabel)
            Me.ClearLabelErrSign(moBeginDateLabel)
            Me.ClearLabelErrSign(moEndDateLabel)
            Me.ClearLabelErrSign(lblFileName)
        End Sub

#End Region

#Region "Populate"

        Sub PopulateCompanyDropDown()

            Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()
            If dv.Count <= 1 Then
                CompanyMultipleDrop.SetControl(False, CompanyMultipleDrop.MODES.NEW_MODE, False, dv, "* " & TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY), True)
            Else
                CompanyMultipleDrop.SetControl(True, CompanyMultipleDrop.MODES.NEW_MODE, True, dv, "* " & TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY), True)
            End If
        End Sub
        Sub PopulateFileName()

            Me.cboFileName.Items.Clear()
            If CompanyMultipleDrop.SelectedGuid.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(CompanyMultipleDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
            End If

            If moBeginDateText.Text.Trim.ToString <> String.Empty And moEndDateText.Text.Trim.ToString <> String.Empty Then
                ReportCeBase.ValidateBeginEndDate(moBeginDateLabel, moBeginDateText.Text, moEndDateLabel, moEndDateText.Text)
                endDate = ReportCeBase.FormatDate(moEndDateLabel, moEndDateText.Text)
                beginDate = ReportCeBase.FormatDate(moBeginDateLabel, moBeginDateText.Text)

                Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
                oListContext.CompanyId = CompanyMultipleDrop.SelectedGuid
                Dim acctFileLst As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="AccountingTransmissionFileByCompany", context:=oListContext)
                Dim filteredAcctFileLst As DataElements.ListItem() = (From acclist In acctFileLst
                                                                      Where ((Date.ParseExact(acclist.ExtendedCode, "MMddyyyy", DateTimeFormatInfo.InvariantInfo) >= Date.ParseExact(beginDate, "yyyyMMdd", DateTimeFormatInfo.InvariantInfo)) And
                                                                              Date.ParseExact(acclist.ExtendedCode, "MMddyyyy", DateTimeFormatInfo.InvariantInfo) <= Date.ParseExact(endDate, "yyyyMMdd", DateTimeFormatInfo.InvariantInfo))
                                                                      Select acclist)
                Me.cboFileName.Populate(acctFileLst, New PopulateOptions() With
                                {
                                    .AddBlankItem = True
                                })
                'Me.BindListControlToDataView(Me.cboFileName, LookupListNew.getAccountingFileNames(CompanyMultipleDrop.SelectedGuid, beginDate, endDate), , , True)

            End If

        End Sub

        Private Sub InitializeForm()
            PopulateCompanyDropDown()
            Me.moBeginDateText.Text = String.Empty 'GetDateFormattedString(Date.Now.AddDays(0))  
            Me.moEndDateText.Text = String.Empty
            Me.rAllFiles.Checked = True
            TheRptCeInputControl.populateReportLanguages(RPT_FILENAME)
        End Sub

#End Region

#Region "Crystal Enterprise"

        Function SetParameters(ByVal CompanyId As String, ByVal CompanyDesc As String, ByVal FileName As String,
                                  ByVal FileType As String, ByVal BeginDate As String, ByVal EndDate As String) As ReportCeBaseForm.Params


            Dim culturevalue As String = TheRptCeInputControl.getCultureValue(False)
            Dim reportName As String = TheRptCeInputControl.getReportName(RPT_FILENAME, False)

            Dim params As New ReportCeBaseForm.Params
            Dim moReportFormat As ReportCeBaseForm.RptFormat
            moReportFormat = ReportCeBase.GetReportFormat(Me)

            Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
                                    {
                                     New ReportCeBaseForm.RptParam("V_COMPANY_ID", CompanyId),
                                     New ReportCeBaseForm.RptParam("V_COMPANY_DESC", CompanyDesc),
                                     New ReportCeBaseForm.RptParam("V_FILENAME", FileName),
                                     New ReportCeBaseForm.RptParam("V_FILETYPE", FileType),
                                     New ReportCeBaseForm.RptParam("V_BEGIN_DATE", BeginDate),
                                     New ReportCeBaseForm.RptParam("V_END_DATE", EndDate),
                                     New ReportCeBaseForm.RptParam("LANG_CULTURE_VALUE", culturevalue)}

            Me.rptWindowTitle.InnerText = TheRptCeInputControl.getReportWindowTitle(TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW))

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
            Dim userId As Guid = ElitaPlusIdentity.Current.ActiveUser.Id
            Dim CompanyId As Guid = CompanyMultipleDrop.SelectedGuid
            Dim CompanyDesc As String = CompanyMultipleDrop.SelectedDesc 'LookupListNew.GetCodeFromId(dv, selectedDealerId)
            Dim oCountryId As Guid = ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID).Id
            Dim oCountry As New Country(oCountryId)
            Dim params As ReportCeBaseForm.Params
            Dim FileName As String ' = cboFileName.SelectedItem.ToString
            Dim Filetype As String
            'Currency
            Dim Currencyid As Guid = oCountry.PrimaryCurrencyId
            Dim strCurrency As String = LookupListNew.GetDescriptionFromId("CURRENCIES", Currencyid)

            Dim arrfileype() As String
            'Dim a As New ArrayList


            If CompanyId.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(CompanyMultipleDrop.CaptionLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
            End If

            'Dates
            ReportCeBase.ValidateBeginEndDate(moBeginDateLabel, moBeginDateText.Text, moEndDateLabel, moEndDateText.Text)
            endDate = ReportCeBase.FormatDate(moEndDateLabel, moEndDateText.Text)
            beginDate = ReportCeBase.FormatDate(moBeginDateLabel, moBeginDateText.Text)

            If rAllFiles.Checked = True Then
                FileName = ALL
                Filetype = String.Empty
            Else
                FileName = cboFileName.SelectedItem.ToString()
                arrfileype = FileName.Split(CChar("-"))
                Filetype = arrfileype(1)
            End If

            'If cboFileName.SelectedIndex = 0 Then
            '    ElitaPlusPage.SetLabelError(lblFileName)
            '    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_FILE_NAME_IS_REQUIRED)
            'Else
            '    FileName = cboFileName.SelectedItem.ToString()
            '    arrfileype = FileName.Split(CChar("-"))
            '    Filetype = arrfileype(1)
            'End If

            ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

            moReportFormat = ReportCeBase.GetReportFormat(Me)

            params = SetParameters(GuidControl.GuidToHexString(CompanyId), CompanyDesc, FileName, Filetype, beginDate, endDate)

            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

        End Sub
#End Region

        Private Sub moBeginDateText_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles moBeginDateText.TextChanged
            Try
                PopulateFileName()
            Catch ex As Exception
                HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Protected Sub moEndDateText_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles moEndDateText.TextChanged
            Try
                PopulateFileName()
            Catch ex As Exception
                HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub
    End Class

End Namespace