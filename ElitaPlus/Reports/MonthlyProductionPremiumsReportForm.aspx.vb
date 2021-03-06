Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Reports

    Partial Class MonthlyProductionPremiumsReportForm
        Inherits ElitaPlusPage

#Region "ParameterData"

        Public Structure ParameterData
            Public userId As String
            Public dealerGroupCode As String
            Public selectedGroupId As String
            Public dealerCode As String
            Public year As String
            Public highDate As String
            Public summarize As String
            Public currency As String
        End Structure

#End Region

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "MONTHLY PRODUCTION - PREMIUMS REPORT CRITERIA"
        Private Const RPT_FILENAME As String = "Premiums Monthly Production"
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
                Else
                    Me.ClearLabelsErrSign()
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
        Public Sub ClearLabelsErrSign()
            Try
                Me.ClearLabelErrSign(moYearLabel)
                Me.ClearLabelErrSign(DealerMultipleDrop.CaptionLabel)
                Me.ClearLabelErrSign(moDealerGroupLabel)
                If Me.rdealer.Checked Then DealerMultipleDrop.SelectedIndex = -1
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub
#End Region

#End Region

#Region "Populate"

        Sub PopulateYear()
            ' Dim dv As DataView = AccountingCloseInfo.GetClosingYears(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
            'Me.BindListTextToDataView(Me.moYearDrop, dv, , , False)
            Dim YearList As DataElements.ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:="ClosingYearsByCompany",
                                                                context:=New ListContext() With
                                                                {
                                                                  .CompanyId = ElitaPlusIdentity.Current.ActiveUser.CompanyId
                                                                })
            Me.moYearDrop.Populate(YearList.ToArray(), New PopulateOptions() With
                                   {
                                   .ValueFunc = AddressOf .GetCode
                                   })

            moYearDrop.SelectedIndex = moYearDrop.Items.Count - 1
            ' Dim oDescrip As String = Me.GetSelectedDescription(Me.moYearDrop)
        End Sub

        Private Sub PopulateDealerDropDown()
            '''Me.BindListControlToDataView(Me.cboDealerDescription, LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE"))
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

        '8/2/06 ALR - Changed group list retrieval to filter on all companies that the user belongs to.
        Sub PopulateDealerGroupDropDown()

            'Dim DealerGroupDataView As DataView = LookupListNew.GetDealerGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
            'Me.BindListControlToDataView(cboDealerGroup, DealerGroupDataView)


            Dim DealerGroups As DataElements.ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:="DealerGroupByCompanyGroup",
                                                                context:=New ListContext() With
                                                                {
                                                                  .CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                                                                })

            Me.cboDealerGroup.Populate(DealerGroups.ToArray(),
                                        New PopulateOptions() With
                                        {
                                         .AddBlankItem = True
                                        })

        End Sub

        Private Sub InitializeForm()
            PopulateYear()
            PopulateDealerGroupDropDown()
            PopulateDealerDropDown()
            Me.RadiobuttonTotalsOnly.Checked = True
            TheRptCeInputControl.ExcludeExport()
        End Sub

#End Region


#Region "Crystal Enterprise"

        Function SetParameters(ByVal data As ParameterData) As ReportCeBaseForm.Params
            Dim reportFormat As ReportCeBaseForm.RptFormat
            Dim reportName As String = RPT_FILENAME
            Dim params As New ReportCeBaseForm.Params
            Dim repParams() As ReportCeBaseForm.RptParam

            With data
                repParams = New ReportCeBaseForm.RptParam() _
                {
                 New ReportCeBaseForm.RptParam("V_USER_KEY", .userId),
                 New ReportCeBaseForm.RptParam("V_DEALER_GROUP", .dealerGroupCode),
                 New ReportCeBaseForm.RptParam("V_DEALER", .dealerCode),
                 New ReportCeBaseForm.RptParam("P_SUMMARIZE", .summarize),
                 New ReportCeBaseForm.RptParam("V_YEAR", .year),
                 New ReportCeBaseForm.RptParam("V_GROUP_ID", .selectedGroupId),
                 New ReportCeBaseForm.RptParam("P_CURRENCY", .currency)
                }
            End With

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
            Dim UserId As String = GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.Id)
            Dim dealerGroupId As Guid
            Dim dtSelectedDate, dtLatestCloseDate As Date
            Dim selectByGroup As String
            Dim selectedGroupId As Guid = Me.GetSelectedItem(Me.cboDealerGroup)

            Dim selectedDealerId As Guid = DealerMultipleDrop.SelectedGuid 'Me.GetSelectedItem(Me.cboDealerCode)
            'Dim dvDealer As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "CODE")
            Dim dealerCode As String = DealerMultipleDrop.SelectedCode 'LookupListNew.GetCodeFromId(dvDealer, selectedDealerId)

            Dim countryId As Guid = ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID).Id
            Dim oCountry As New Country(countryId)
            Dim currencyId As Guid = oCountry.PrimaryCurrencyId
            Dim strCurrency As String = LookupListNew.GetDescriptionFromId("CURRENCIES", currencyId)


            With data
                .userId = UserId
                .year = Me.GetSelectedDescription(Me.moYearDrop)
                .selectedGroupId = NO
                .currency = strCurrency
                If Me.rdealer.Checked Then
                    .dealerCode = ALL
                ElseIf Me.rGroup.Checked Then
                    .selectedGroupId = ALL
                    .dealerGroupCode = ALL
                ElseIf selectedDealerId.Equals(Guid.Empty) And selectedGroupId.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(DealerMultipleDrop.CaptionLabel)
                    Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                End If

                If Not selectedGroupId.Equals(Guid.Empty) Then
                    .dealerGroupCode = DALBase.GuidToSQLString(selectedGroupId)
                    .selectedGroupId = YES
                ElseIf Not selectedDealerId.Equals(Guid.Empty) Then
                    .dealerCode = DealerMultipleDrop.SelectedCode()
                End If

                ' ----------------------------------------- 
                ' Get The Latest Accounting Close Date for the selected year
                dtSelectedDate = Date.Parse(("01/01/" & .year), System.Globalization.CultureInfo.InvariantCulture).AddYears(1)
                If dtSelectedDate > Date.Today Then
                    dtSelectedDate = Date.Today
                End If
                dtLatestCloseDate = AccountingCloseInfo.GetAccountingCloseDate(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID, dtSelectedDate)
                .highDate = dtLatestCloseDate.ToString(System.Globalization.CultureInfo.InvariantCulture)

                If Me.RadiobuttonTotalsOnly.Checked Then
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
