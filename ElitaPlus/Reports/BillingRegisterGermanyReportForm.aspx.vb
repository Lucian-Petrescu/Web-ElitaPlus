Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.DALObjects
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Reports

    Partial Public Class BillingRegisterGermanyReportForm
        Inherits ElitaPlusPage

#Region "Parameters"

        Public Structure ReportParams
            Public companyCode As String
            Public beginDate As String
            Public endDate As String

        End Structure

#End Region

#Region "Constants"

        '   Private Const RPT_FILENAME As String = "Claims Following Service Warranty English (USA)_TEST7"
        Private Const RPT_FILENAME_WINDOW As String = "Billing Register Germany"
        'Private Const RPT_FILENAME As String = "BillingRegisterDetailArgentina"
        Private Const RPT_DEALERFILENAME As String = "BillingRegisterGermanyDealer"
        Private Const RPT_DEALERFILENAME_EXPORT As String = "BillingRegisterGermanyDealer-Exp"
        Private Const RPT_DEALERGROUPFILENAME As String = "BillingRegisterGermanyDealerGroup"
        Private Const RPT_DEALERGROUPFILENAME_EXPORT As String = "BillingRegisterGermanyDealerGroup-Exp"

        Public Const ALL As String = "*"
        Public Const NOTHING_SELECTED As Int16 = -1
        Public Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Public Const YES As String = "Y"
        Public Const NO As String = "N"

        Protected WithEvents isProgressVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents isReportCeVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents moReportCeStatus As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents moDaysActiveLabel As System.Web.UI.WebControls.Label

        Public Const DEALER_CODE As String = "0"
        Public Const DEALER_NAME As String = "1"
        Dim sortOrder As String
        Private Const ONE_ITEM As Integer = 1

        Private Const LABEL_SELECT_COMPANY As String = "SELECT_COMPANY"

#End Region

#Region "variables"
        Dim moReportFormat As ReportCeBaseForm.RptFormat
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

#Region "Handlers-DropDown"


#End Region

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents Label1 As System.Web.UI.WebControls.Label

        Protected WithEvents ErrorCtrl As ErrorController
        Protected WithEvents moReportCeInputControl As ReportCeInputControl

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            ErrorCtrl.Clear_Hide()
            Try
                If Not IsPostBack Then
                    InitializeForm()
                    TheRptCeInputControl.populateReportLanguages(RPT_DEALERFILENAME)
                    'Date Calendars
                    AddCalendar(BtnBeginDate, moBeginDateText)
                    AddCalendar(BtnEndDate, moEndDateText)
                Else
                    ClearErrLabels()
                End If
                InstallProgressBar()
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
            ShowMissingTranslations(ErrorCtrl)
        End Sub

        Sub PopulateDealerDropDown()
            Dim dealerist As New Collections.Generic.List(Of DataElements.ListItem)
            Dim oListContext As New ListContext

            For Each company_id As Guid In ElitaPlusIdentity.Current.ActiveUser.Companies
                oListContext.CompanyId = company_id
                Dim dealerListForCompany As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerListByCompany", context:=oListContext)
                If dealerListForCompany.Count > 0 Then
                    If dealerist IsNot Nothing Then
                        dealerist.AddRange(dealerListForCompany)
                    Else
                        dealerist = dealerListForCompany.Clone()
                    End If
                End If
            Next

            Dim dealerTextFunc As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                                                                               Return li.ExtendedCode + " - " + li.Translation
                                                                           End Function

            cbodealer.Populate(dealerist.ToArray(), New PopulateOptions() With
                {
                    .AddBlankItem = True,
                    .TextFunc = dealerTextFunc,
                    .ValueFunc = AddressOf .GetListItemId
                })

            'Me.BindListControlToDataView(Me.cbodealer, LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies), , , True)
        End Sub

        Sub PopulateDealerGroupDropDown()
            Dim oListContext As New ListContext
            oListContext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            Dim dealerGroupListForCompanyGroup As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerGroupByCompanyGroup", context:=oListContext)

            cboDealerGrp.Populate(dealerGroupListForCompanyGroup.ToArray(), New PopulateOptions() With
                {
                    .AddBlankItem = True
                })
            'Me.BindListControlToDataView(Me.cboDealerGrp, LookupListNew.GetDealerGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id))

            cboadealergrp.Populate(dealerGroupListForCompanyGroup.ToArray(), New PopulateOptions() With
                {
                    .AddBlankItem = True
                })
            'Me.BindListControlToDataView(Me.cboadealergrp, LookupListNew.GetDealerGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id))
        End Sub

        Private Sub InitializeForm()
            PopulateDealerDropDown()
            PopulateDealerGroupDropDown()
            Dim t As Date = Date.Now.AddDays(-1)
            moBeginDateText.Text = GetDateFormattedString(t)
            moEndDateText.Text = GetDateFormattedString(Date.Now)
            RadiobuttonTotalsOnly.Checked = True
            rdealer.Checked = True
        End Sub

        Private Sub ClearErrLabels()
            ClearLabelErrSign(moBeginDateLabel)
            ClearLabelErrSign(moEndDateLabel)
        End Sub

        Private Sub btnGenRpt_Click(sender As System.Object, e As System.EventArgs) Handles btnGenRpt.Click
            Try
                GenerateReport()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Private Sub GenerateReport()
            Dim oCompanyGrpId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            Dim endDate As String
            Dim beginDate As String

            'Dates
            ReportCeBase.ValidateBeginEndDate(moBeginDateLabel, moBeginDateText.Text, moEndDateLabel, moEndDateText.Text)
            endDate = ReportCeBase.FormatDate(moEndDateLabel, moEndDateText.Text)
            beginDate = ReportCeBase.FormatDate(moBeginDateLabel, moBeginDateText.Text)

            Dim dealerID As Guid = GetSelectedItem(cbodealer)
            Dim dv As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
            Dim dealerCode As String '= LookupListNew.GetCodeFromId(dv, dealerID)
            Dim selectedDealer As String = LookupListNew.GetDescriptionFromId(dv, dealerID)

            Dim dealergrpID As Guid = GetSelectedItem(cboDealerGrp)
            Dim dvdealergrp As DataView = LookupListNew.GetDealerGroupLookupList(oCompanyGrpId)
            Dim dealergrpCode As String '= LookupListNew.GetCodeFromId(dv, dealergrpID)
            Dim selectedDealerGrp As String = LookupListNew.GetDescriptionFromId(dv, dealergrpID)
            'dealergrpCode = ALL

            Dim alldealergrpId As Guid = GetSelectedItem(cboadealergrp)
            Dim AlldealergrpCode As String '= LookupListNew.GetCodeFromId(dv, alldealergrpId)
            Dim selectedAllDealerGrp As String = LookupListNew.GetDescriptionFromId(dv, alldealergrpId)

            Dim detailCode As String
            'Dim customerRefunds As String = YES

            If RadiobuttonTotalsOnly.Checked Then
                detailCode = NO
            Else
                detailCode = YES
            End If

            Select Case rdReportSortOrder.SelectedValue()
                Case DEALER_CODE
                    sortOrder = "C"
                Case DEALER_NAME
                    sortOrder = "N"
            End Select

            If rdealer.Checked Then
                dealerCode = ALL
            ElseIf Not dealerID.Equals(Guid.Empty) Then
                dealerCode = LookupListNew.GetCodeFromId(dv, dealerID)
            ElseIf Not dealergrpID.Equals(Guid.Empty) Then
                dealergrpCode = LookupListNew.GetCodeFromId(dvdealergrp, dealergrpID)
                'AlldealergrpCode = LookupListNew.GetCodeFromId(dvdealergrp, dealergrpID)
            ElseIf rdealergrp.Checked = True Then
                AlldealergrpCode = ALL
            ElseIf Not alldealergrpId.Equals(Guid.Empty) Then
                AlldealergrpCode = LookupListNew.GetCodeFromId(dvdealergrp, alldealergrpId)
            Else
                Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
            End If

            ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

            If AlldealergrpCode = Nothing Then
                Dim params As ReportCeBaseForm.Params = SetParameters(GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.Id), dealerCode, dealergrpCode, beginDate, endDate, detailCode, sortOrder, True)
                Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params
            Else
                Dim params As ReportCeBaseForm.Params = SetParameters(GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.Id), dealerCode, AlldealergrpCode, beginDate, endDate, detailCode, sortOrder, False)
                Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params
            End If
        End Sub

        Function SetParameters(UserId As String, dealerCode As String, dealergrpCode As String, begindate As String, enddate As String, isSummary As String, sortorder As String, isdealer As Boolean) As ReportCeBaseForm.Params

            Dim params As New ReportCeBaseForm.Params
            ' Dim reportName As String = RPT_DEALERFILENAME
            Dim exportData As String = NO
            Dim culturevalue As String
            Dim reportName As String
            Dim refundtype As String = "E"

            If isdealer = True Then
                culturevalue = TheRptCeInputControl.getCultureValue(False)
                reportName = TheRptCeInputControl.getReportName(RPT_DEALERFILENAME, False)
            Else
                culturevalue = TheRptCeInputControl.getCultureValue(False)
                reportName = TheRptCeInputControl.getReportName(RPT_DEALERGROUPFILENAME, False)
            End If

            moReportFormat = ReportCeBase.GetReportFormat(Me)
            If (moReportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (moReportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                isSummary = YES
                refundtype = "Exp"
                If isdealer = True Then
                    'reportName = RPT_DEALERFILENAME_EXPORT
                    reportName = TheRptCeInputControl.getReportName(RPT_DEALERFILENAME_EXPORT, True)
                    culturevalue = TheRptCeInputControl.getCultureValue(True)
                Else
                    reportName = TheRptCeInputControl.getReportName(RPT_DEALERGROUPFILENAME_EXPORT, True)
                    culturevalue = TheRptCeInputControl.getCultureValue(True)
                End If
            End If

            ' Dim isSummary As String = YES
            If isdealer = True And dealergrpCode IsNot Nothing Then
                isdealer = False
            End If


            Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
            {
             New ReportCeBaseForm.RptParam("V_USER_KEY", UserId),
             New ReportCeBaseForm.RptParam("V_DEALER", dealerCode),
             New ReportCeBaseForm.RptParam("V_DEALERGROUP", dealergrpCode),
             New ReportCeBaseForm.RptParam("V_BEGIN_DATE", begindate),
             New ReportCeBaseForm.RptParam("V_END_DATE", enddate),
             New ReportCeBaseForm.RptParam("V_REFUNDS", refundtype),
             New ReportCeBaseForm.RptParam("V_ISIDEALER", isdealer.ToString),
             New ReportCeBaseForm.RptParam("V_IS_SUMMARY", isSummary),
             New ReportCeBaseForm.RptParam("V_SORT_ORDER", sortorder),
             New ReportCeBaseForm.RptParam("LANG_CULTURE_VALUE", culturevalue)}

            rptWindowTitle.InnerText = TheRptCeInputControl.getReportWindowTitle(RPT_FILENAME_WINDOW)

            With params
                .msRptName = reportName
                .msRptWindowName = TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW)
                .moRptFormat = moReportFormat
                .moAction = ReportCeBaseForm.RptAction.SCHEDULE_VIEW
                .moRptParams = repParams
            End With
            Return params
        End Function

    End Class
End Namespace