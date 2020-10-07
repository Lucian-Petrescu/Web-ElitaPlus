Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
'Imports Assurant.Common.Framework

Namespace Reports

    Partial Class MonthlyProductionCertificatesReportForm
        Inherits ElitaPlusPage

#Region "ParameterData"

        Public Structure ParameterData
            Public userId As String
            Public dealerGroupCode As String
            Public selectedGroupId As String
            Public dealercode As String
            Public year As String
            Public highDate As String
            Public summarize As String
        End Structure

#End Region

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "MONTHLY PRODUCTION - CERTIFICATES REPORT CRITERIA"
        Private Const RPT_FILENAME As String = "Monthly Production - Certificates"
        Private Const SUBRPT_SUM_FILENAME As String = "Summary Monthly Production - Certificates"
        Private Const SUBRPT_CHART_FILENAME As String = "Monthly Production - Certificates Chart.rpt"
        Public Const ALL As String = "*"
        Public Const YES As String = "Y"
        Public Const NO As String = "N"
        Private Const LABEL_OR_A_SINGLE_DEALER As String = "OR A SINGLE DEALER"
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
#End Region

#Region "Handlers"


#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents ErrorCtrl As ErrorController
        Protected WithEvents moReportCeInputControl As ReportCeInputControl
        Protected WithEvents multipleDropControl As MultipleColumnDDLabelControl

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
                Else
                    ClearLabelsErrSign()
                End If
                InstallProgressBar()
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

#End Region

#Region "Populate"

        Sub PopulateYear()
            'Dim dv As DataView = AccountingCloseInfo.GetClosingYears(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
            'Me.BindListTextToDataView(Me.moYearDrop, dv, , , False)

            Dim YearList As DataElements.ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:=ListCodes.ClosingYearsByCompany,
                                                                context:=New ListContext() With
                                                                {
                                                                  .CompanyId = ElitaPlusIdentity.Current.ActiveUser.CompanyId
                                                                })
            moYearDrop.Populate(YearList.ToArray(), New PopulateOptions())

            moYearDrop.SelectedIndex = moYearDrop.Items.Count - 1
            ' Dim oDescrip As String = Me.GetSelectedDescription(Me.moYearDrop)
        End Sub

        '8/2/06 ALR - Changed group list retrieval to filter on all companies that the user belongs to.
        Sub PopulateDealerGroup()

            'Dim DealerGroupDataView As DataView = LookupListNew.GetDealerGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
            'Me.BindListControlToDataView(cboDealerGroup, DealerGroupDataView)

            Dim DealerGroups As DataElements.ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:=ListCodes.DealerGroupByCompanyGroup,
                                                                context:=New ListContext() With
                                                                {
                                                                  .CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                                                                })

            cboDealerGroup.Populate(DealerGroups.ToArray(),
                                        New PopulateOptions() With
                                        {
                                         .AddBlankItem = True
                                        })

        End Sub

        Private Sub PopulateDealerDropDown()

            '''Me.BindListControlToDataView(Me.cboDealer, LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE"), , , True)
            '''Dim DealersLookupListSortedByCode As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE")
            '''DealersLookupListSortedByCode.Sort = "CODE"
            '''Me.BindListControlToDataView(Me.cboDealerCode, DealersLookupListSortedByCode, "CODE", , True)
            Dim dv As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE")
            DealerMultipleDrop.NothingSelected = True
            DealerMultipleDrop.SetControl(False,
                                              DealerMultipleDrop.MODES.NEW_MODE,
                                              True,
                                              dv,
                                              TranslationBase.TranslateLabelOrMessage(LABEL_OR_A_SINGLE_DEALER),
                                              True,
                                              False,
                                              " document.forms[0].rdealer.checked = false;  document.forms[0].rGroup.checked = false; document.forms[0].cboDealerGroup.selectedIndex = -1; ")

        End Sub

        Private Sub InitializeForm()
            PopulateYear()
            PopulateDealerDropDown()
            PopulateDealerGroup()
            RadiobuttonTotalsOnly.Checked = True
            TheRptCeInputControl.ExcludeExport()
        End Sub

        Public Sub ClearLabelsErrSign()
            Try
                ClearLabelErrSign(moYearLabel)
                ClearLabelErrSign(DealerMultipleDrop.CaptionLabel)
                ClearLabelErrSign(moDealerGroupLabel)
                If rdealer.Checked Then DealerMultipleDrop.SelectedIndex = -1
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

#End Region


#Region "Crystal Enterprise"

        Function SetParameters(data As ParameterData) As ReportCeBaseForm.Params
            Dim reportFormat As ReportCeBaseForm.RptFormat
            Dim reportName As String = RPT_FILENAME
            Dim params As New ReportCeBaseForm.Params
            Dim repParams() As ReportCeBaseForm.RptParam

            With data
                repParams = New ReportCeBaseForm.RptParam() _
                {
                 New ReportCeBaseForm.RptParam("V_USER_KEY", .userId),
                 New ReportCeBaseForm.RptParam("V_DEALER_GROUP", .dealerGroupCode),
                 New ReportCeBaseForm.RptParam("V_DEALER", .dealercode),
                 New ReportCeBaseForm.RptParam("P_SUMMARIZE", .summarize),
                 New ReportCeBaseForm.RptParam("V_YEAR", .year),
                 New ReportCeBaseForm.RptParam("V_GROUP_ID", .selectedGroupId),
                 New ReportCeBaseForm.RptParam("P_CURRENCY", "")
                }
            End With
            'New ReportCeBaseForm.RptParam("P_CURRENCY", "", SUBRPT_CHART_FILENAME), _


            'New ReportCeBaseForm.RptParam("P_HIGHDATE", .highDate), _

            reportFormat = ReportCeBase.GetReportFormat(Me)

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
            Dim data As ParameterData

            Dim userId As String = GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.Id)
            Dim dealerGroupId As Guid
            Dim dtSelectedDate, dtLatestCloseDate As Date
            Dim selectByGroup As String

            Dim selectedGroupId As Guid = GetSelectedItem(cboDealerGroup)
            Dim selectedDealerId As Guid = DealerMultipleDrop.SelectedGuid 'Me.GetSelectedItem(Me.cboDealer)
            'Dim dvDealer As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE")
            Dim dealerCode As String = DealerMultipleDrop.SelectedCode 'LookupListNew.GetCodeFromId(dvDealer, selectedDealerId)

            With data
                .userId = userId
                .year = GetSelectedDescription(moYearDrop)
                '.dealercode = dealerCode
                '.dealerGroupCode = dealerCode

                ' -----------------------------------------
                .selectedGroupId = NO

                If rdealer.Checked Then
                    .dealercode = ALL
                ElseIf rGroup.Checked Then
                    .selectedGroupId = ALL
                    .dealerGroupCode = ALL
                ElseIf selectedDealerId.Equals(Guid.Empty) AndAlso selectedGroupId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(DealerMultipleDrop.CaptionLabel)
                    Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                End If

                If Not selectedGroupId.Equals(Guid.Empty) Then
                    .dealerGroupCode = DALBase.GuidToSQLString(selectedGroupId)
                    .selectedGroupId = YES
                ElseIf Not selectedDealerId.Equals(Guid.Empty) Then
                    '.dealercode = DALBase.GuidToSQLString(selectedDealerId)
                    .dealercode = DealerMultipleDrop.SelectedCode()
                End If

                ' ----------------------------------------- 

                ' Get The Latest Accounting Close Date for the selected year
                dtSelectedDate = Date.Parse(("01/01/" & .year), System.Globalization.CultureInfo.InvariantCulture).AddYears(1)
                If dtSelectedDate > Date.Today Then
                    dtSelectedDate = Date.Today
                End If
                dtLatestCloseDate = AccountingCloseInfo.GetAccountingCloseDate(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID, dtSelectedDate)
                .highDate = dtLatestCloseDate.ToString(System.Globalization.CultureInfo.InvariantCulture)

                If RadiobuttonTotalsOnly.Checked Then
                    .summarize = YES
                Else
                    .summarize = NO
                End If


            End With

            ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

            Dim params As ReportCeBaseForm.Params = SetParameters(data)
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params
        End Sub
#End Region

    End Class

End Namespace
