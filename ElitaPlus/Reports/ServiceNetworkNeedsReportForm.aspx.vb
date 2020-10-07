Imports Assurant.ElitaPlus.DALObjects

Namespace Reports

    Partial Class ServiceNetworkNeedsReportForm
        Inherits ElitaPlusPage

#Region "Constants"
        Private Const RPT_FILENAME_WINDOW As String = "Service_Network_Needs"
        Private Const RPT_FILENAME As String = "ServiceNetworkNeeds"
        Private Const RPT_FILENAME_EXPORT As String = "ServiceNetworkNeeds-Exp"
        Public Const ALL As String = "*"
        Private Const YES As String = "D"
        Private Const NO As String = "N"
        Private Const TOTALPARAMS As Integer = 4
        Private Const PARAMS_PER_REPORT As Integer = 4

#End Region
#Region "Parameters"

        Public Structure ReportParams
            Public City As String
            Public LangId As String
            Public UserId As String
            Public SortBy As String
            Public DetailCode As String
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
#End Region

#Region "Handlers"


#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub


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
                End If
                InstallProgressBar()
                ClearErrLabels()
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

#Region "Clear"

        Private Sub ClearErrLabels()
            ClearLabelErrSign(moSelectACityLabel)
        End Sub
#End Region
#End Region

#Region "Populate"

        Private Sub InitializeForm()
            OptSelectAllCities.Checked = True
            rdReportSortOrder.Items(0).Selected = True
            RadiobuttonTotalsOnly.Checked = True
        End Sub

#End Region


#Region "Crystal Enterprise"

        Function SetParameters(city As String, userId As String,
                               sortBy As String, detailCode As String) As ReportCeBaseForm.Params
            Dim moReportFormat As ReportCeBaseForm.RptFormat
            Dim params As New ReportCeBaseForm.Params
            Dim repParams(TOTALPARAMS) As ReportCeBaseForm.RptParam
            Dim reportName As String = RPT_FILENAME ' TheReportCeInputControl.getReportName(RPT_FILENAME, False)
            Dim rptParams As ReportParams

            moReportFormat = ReportCeBase.GetReportFormat(Me)

            If (moReportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (moReportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                reportName = RPT_FILENAME_EXPORT ' TheReportCeInputControl.getReportName(RPT_FILENAME_EXPORT, False)
                detailCode = YES
            End If

            With rptParams
                .City = city
                .UserId = userId
                .SortBy = sortBy
                .DetailCode = detailCode
            End With

            SetReportParams(rptParams, repParams, String.Empty, PARAMS_PER_REPORT * 0)     ' Main Report

            If detailCode = YES Then
                reportName = RPT_FILENAME_EXPORT
            End If

            rptWindowTitle.InnerText = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)

            With params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = moReportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With

            Return params

        End Function

        Sub SetReportParams(rptParams As ReportParams, repParams() As ReportCeBaseForm.RptParam,
                            reportName As String, startIndex As Integer)

            With rptParams
                repParams(startIndex) = New ReportCeBaseForm.RptParam("V_CITY", .City, reportName)
                repParams(startIndex + 1) = New ReportCeBaseForm.RptParam("V_USER_ID", .UserId, reportName)
                repParams(startIndex + 2) = New ReportCeBaseForm.RptParam("V_SORT_BY", .SortBy, reportName)
                repParams(startIndex + 3) = New ReportCeBaseForm.RptParam("V_DETAIL_CODE", .DetailCode, reportName)
            End With
        End Sub
        Private Sub GenerateReport()
            Dim UserId As String = DALBase.GuidToSQLString(ElitaPlusIdentity.Current.ActiveUser.Id)
            Dim sortBy As String = rdReportSortOrder.SelectedValue
            Dim detailCode As String
            Dim moCity As String
            If OptSelectAllCities.Checked Then
                moCity = ALL
            Else
                If moCityText.Text = "" Then
                    ElitaPlusPage.SetLabelError(moSelectACityLabel)
                    Throw New GUIException(Message.MSG_GUI_INVALID_VALUE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_CITY_MUST_BE_ENTERED_ERR)
                Else
                    moCity = moCityText.Text
                End If
            End If

            If RadiobuttonTotalsOnly.Checked Then
                detailCode = NO
            Else
                detailCode = YES
            End If

            ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

            Dim params As ReportCeBaseForm.Params = SetParameters(moCity, UserId, sortBy, detailCode)
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

        End Sub

#End Region


    End Class

End Namespace