Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.Web.Forms

Namespace Reports
    Partial Class ServiceWarrantyExperienceForm
        Inherits ElitaPlusPage

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "Service Warranty Experience"
        Private Const RPT_FILENAME As String = "Service Warranty Experience"
        Private Const RPT_FILENAME_EXPORT As String = "Service Warranty Experience_Exp"

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
        Public Const NO As String = "N"

#End Region

#Region "variables"
        Private moReportFormat As ReportCeBaseForm.RptFormat
        Private reportName As String = RPT_FILENAME
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

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents isProgressVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents isReportCeVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents moReportCeStatus As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents ErrorCtrl As ErrorController
        Protected WithEvents moReportCeInputControl As ReportCeInputControl

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
                    Me.AddCalendar(Me.BtnBeginDate, Me.moBeginDateText)
                    Me.AddCalendar(Me.BtnEndDate, Me.moEndDateText)
                End If
                Me.InstallProgressBar()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
            Me.ShowMissingTranslations(Me.ErrorCtrl)

        End Sub

        Private Sub btnGenRpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRpt.Click
            Try
                Dim companyId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyId
                Dim compCode As String = LookupListNew.GetCodeFromId("COMPANIES", companyId)
                Dim selectedSvcCtrId As Guid = Me.GetSelectedItem(Me.cboSvcCtr)
                Dim dvSvcCtr As DataView = LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID).Id)
                Dim svcCtrCode As String = LookupListNew.GetCodeFromId(dvSvcCtr, selectedSvcCtrId)
                Dim endDate As String
                Dim beginDate As String

                'Dates
                ReportCeBase.ValidateBeginEndDate(moBeginDateLabel, moBeginDateText.Text, moEndDateLabel, moEndDateText.Text)
                endDate = ReportCeBase.FormatDate(moEndDateLabel, moEndDateText.Text)
                beginDate = ReportCeBase.FormatDate(moBeginDateLabel, moBeginDateText.Text)

                If Me.rSvcCtr.Checked Then
                    svcCtrCode = ALL
                Else
                    If selectedSvcCtrId.Equals(Guid.Empty) Then
                        Throw New GUIException(Message.MSG_INVALID_SERVICE_CENTER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_SERVICE_CENTER_MUST_BE_SELECTED_ERR)
                    End If
                End If

                ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

                Dim params As ReportCeBaseForm.Params = SetParameters(compCode, svcCtrCode, beginDate, endDate)
                Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Function SetParameters(ByVal companyCode As String, ByVal svcCtrCode As String, ByVal beginDate As String,
                                  ByVal endDate As String) As ReportCeBaseForm.Params

            Dim reportFormat As ReportCeBaseForm.RptFormat
            Dim params As New ReportCeBaseForm.Params
            Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
                    {
                     New ReportCeBaseForm.RptParam("V_COMPANY", companyCode),
                     New ReportCeBaseForm.RptParam("V_BEGIN_DATE", beginDate),
                     New ReportCeBaseForm.RptParam("V_END_DATE", endDate),
                     New ReportCeBaseForm.RptParam("V_SERVICE_CENTER_CODE", svcCtrCode)}

            reportFormat = ReportCeBase.GetReportFormat(Me)

            If (reportFormat = ReportCeBase.RptFormat.TEXT_TAB OrElse
                reportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                reportName = RPT_FILENAME_EXPORT
            End If

            With params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = reportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With
            Return params
        End Function

        Private Sub InitializeForm()
            PopulateDropDowns()
            Dim t As Date = Date.Now.AddDays(-1)
            Me.moBeginDateText.Text = GetDateFormattedString(t)
            Me.moEndDateText.Text = GetDateFormattedString(Date.Now)
            Me.rSvcCtr.Checked = True
        End Sub

        Private Sub PopulateDropDowns()
            PopulateSvcCtrDropDown()
        End Sub

        Private Sub PopulateSvcCtrDropDown()
            'Me.BindListControlToDataView(Me.cboSvcCtr, LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Countries))
            Dim ServiceCenterList As New Collections.Generic.List(Of DataElements.ListItem)

            For Each Country_id As Guid In ElitaPlusIdentity.Current.ActiveUser.Countries
                Dim ServiceCenters As DataElements.ListItem() =
                    CommonConfigManager.Current.ListManager.GetList(listCode:="ServiceCenterListByCountry",
                                                                    context:=New ListContext() With
                                                                    {
                                                                      .CountryId = Country_id
                                                                    })

                If ServiceCenters.Count > 0 Then
                    If Not ServiceCenterList Is Nothing Then
                        ServiceCenterList.AddRange(ServiceCenters)
                    Else
                        ServiceCenterList = ServiceCenters.Clone()
                    End If
                End If
            Next

            Me.cboSvcCtr.Populate(ServiceCenterList.ToArray(),
                                    New PopulateOptions() With
                                    {
                                        .AddBlankItem = True
                                    })
        End Sub

    End Class
End Namespace