﻿
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common

Namespace Reports
    Public Class CollectionAmdocsReportForm
        Inherits ElitaPlusPage


#Region "Parameters"

        Public Structure ReportParams
            Public companyCode As String
            Public beginDate As String
            Public endDate As String

        End Structure

#End Region

#Region "Constants"

        Public Const ALL As String = "*"
        Public Const NOTHING_SELECTED As Int16 = -1
        Public Const SP_DATE_FORMAT As String = "yyyyMMdd"
        Public Const YES As String = "Y"
        Public Const NO As String = "N"

        Public Const PAGETITLE As String = "AMDOCS_Collection_Extract_Report"
        Public Const PAGETAB As String = "REPORTS"
        Public Const DOCTITLE As String = "AMDOCS_Collection_Extract_Report"

        Private Const LABEL_DEALER As String = "DEALER_NAME"
        Private Const SELECT_ALL_DEALERS As String = "SELECT_ALL_DEALERS"
        Private Const SELECT_SINGLE_DEALER As String = "SELECT_SINGLE_DEALER"

        Private Const BASED_ON_BILLING_DATE As String = "BASED_ON_BILLING_DATE"
        Private Const BASED_ON_COLLECTION_DATE As String = "BASED_ON_COLLECTION_DATE"
        Private Const BASED_ON_PROCESSED_DATE As String = "BASED_ON_PROCESSED_DATE"

        Private Const OPT_DEALER_ALL As String = "DEALER_ALL"
        Private Const OPT_DEALER_SINGLE As String = "DEALER_SINGLE"

        Private Const OPT_DEALER_ALL_VALUE As String = "ALL"


        Private Const OPT_DATEBASEON_BILLING As String = "DATEBASEON_BILLING"
        Private Const OPT_DATEBASEON_COLLECTION As String = "DATEBASEON_COLLECTION"
        Private Const OPT_DATEBASEON_PROCESSED As String = "DATEBASEON_PROCESSED"

        Private Const OPT_DATEBASEON_BILLING_VALUE As String = "B"
        Private Const OPT_DATEBASEON_COLLECTION_VALUE As String = "C"
        Private Const OPT_DATEBASEON_PROCESSED_VALUE As String = "P"

#End Region

#Region "variables"
        'Dim moReportFormat As ReportCeBaseForm.RptFormat
        Private dtLatestAccountingCloseDate As Date
#End Region

#Region "Properties"
        Public ReadOnly Property TheDealerControl() As MultipleColumnDDLabelControl_New
            Get
                If DealerDropControl Is Nothing Then
                    DealerDropControl = CType(FindControl("DealerDropControl"), MultipleColumnDDLabelControl_New)
                End If
                Return DealerDropControl
            End Get
        End Property

#End Region

#Region "Handlers-DropDown"



#End Region
#Region "Page State"

        Class MyState
            Public MyBO As ReportRequests
            Public IsNew As Boolean = False
            Public IsACopy As Boolean

            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public LastErrMsg As String
            Public HasDataChanged As Boolean
            Public ForEdit As Boolean = False
        End Class

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property

#End Region

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents Label1 As System.Web.UI.WebControls.Label

        Protected WithEvents isProgressVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents isReportCeVisible As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents moReportCeStatus As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents moDaysActiveLabel As System.Web.UI.WebControls.Label

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Populate"
        Private Sub PopulateDealer()
            Dim oCompanyList As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Try
                Dim dv As DataView = LookupListNew.GetDealerLookupList(oCompanyList, True)
                TheDealerControl.SetControl(True, TheDealerControl.MODES.NEW_MODE, True, dv, "*" + TranslationBase.TranslateLabelOrMessage(LABEL_DEALER), True, True)

                TheDealerControl.ChangeEnabledControlProperty(True)

            Catch ex As Exception
                MasterPage.MessageController.AddError("")
                MasterPage.MessageController.AddError(ex.Message, False)
                MasterPage.MessageController.Show()
            End Try
        End Sub
#End Region
        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            MasterPage.MessageController.Clear_Hide()
            ClearLabelsErrSign()

            Try
                If Not IsPostBack Then

                    TheReportExtractInputControl.ViewVisible = False
                    TheReportExtractInputControl.PdfVisible = False
                    TheReportExtractInputControl.ExportDataVisible = False
                    TheReportExtractInputControl.DestinationVisible = False
                    MasterPage.UsePageTabTitleInBreadCrum = False
                    SetFormTab(PAGETAB)
                    UpdateBreadCrum()
                    AddCalendar(btnBeginDate, txtBeginDate)
                    AddCalendar(btnEndDate, txtEndDate)

                    PopulateDealer()
                End If

                InstallDisplayNewReportProgressBar()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub
        Public Sub ClearLabelsErrSign()
            Try
                ClearLabelErrSign(lblEndDate)
                ClearLabelErrSign(lblBeginDate)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub UpdateBreadCrum()
            MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(PAGETITLE)
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        End Sub

        Private Sub btnGenRpt_Click(sender As System.Object, e As System.EventArgs) Handles btnGenRpt.Click
            Try
                GenerateReport()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub GenerateReport()
            Try

                Dim reportParams As New System.Text.StringBuilder
                Dim endDate As String
                Dim beginDate As String
                Dim SelectedReportBasedOn As String
                Dim userId As String = GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.Id)
                Dim companygrpId As String = GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
                Dim companyCode As String = ElitaPlusIdentity.Current.ActiveUser.Company.Code
                Dim dealerCode As String = String.Empty

                If String.IsNullOrEmpty(companyCode) Then
                    Throw New GUIException(Message.MSG_COMPANY_IS_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_COMPANYID_REQUIRED)
                End If

                If txtBeginDate.Text.Equals(String.Empty) AndAlso txtEndDate.Text.Equals(String.Empty) Then
                    ElitaPlusPage.SetLabelError(lblBeginDate)
                    ElitaPlusPage.SetLabelError(lblEndDate)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_SELECTION)
                ElseIf ((Not txtBeginDate.Text.Equals(String.Empty) AndAlso txtEndDate.Text.Equals(String.Empty)) OrElse (txtBeginDate.Text.Equals(String.Empty) AndAlso Not txtEndDate.Text.Equals(String.Empty))) Then
                    ElitaPlusPage.SetLabelError(lblBeginDate)
                    ElitaPlusPage.SetLabelError(lblEndDate)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_BEGIN_AND_END_DATES_MUST_BE_SELECTED_ERR)
                ElseIf Not txtBeginDate.Text.Equals(String.Empty) AndAlso Not txtEndDate.Text.Equals(String.Empty) Then
                    ReportExtractBase.ValidateBeginEndDate(lblBeginDate, txtBeginDate.Text, lblEndDate, txtEndDate.Text)
                End If

                SelectedReportBasedOn = moDealerOptionList.SelectedValue
                endDate = ReportExtractBase.FormatDate(lblEndDate, txtEndDate.Text)
                beginDate = ReportExtractBase.FormatDate(lblBeginDate, txtBeginDate.Text)

                Dim selectedDealerOption As String = moDealerOptionList.SelectedValue
                If selectedDealerOption = OPT_DEALER_ALL Then
                    dealerCode = OPT_DEALER_ALL_VALUE
                ElseIf selectedDealerOption = OPT_DEALER_SINGLE Then
                    dealerCode = TheDealerControl.SelectedCode

                    If String.IsNullOrEmpty(dealerCode) Then
                        Throw New GUIException(Message.MSG_SELECT_A_SINGLE_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_SELECTION)
                    End If

                    Dim dealer As Dealer = New Dealer(TheDealerControl.SelectedGuid)
                    If dealer Is Nothing Then
                        Throw New GUIException(Message.MSG_SELECT_A_SINGLE_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_SELECTION)
                    End If

                    Dim company As Company = New Company(dealer.CompanyId)
                    companyCode = company.Code
                End If

                Dim reportBaseOn As String = String.Empty

                If moReportBasedOnList.SelectedValue = OPT_DATEBASEON_BILLING Then
                    reportBaseOn = OPT_DATEBASEON_BILLING_VALUE
                ElseIf moReportBasedOnList.SelectedValue = OPT_DATEBASEON_COLLECTION Then
                    reportBaseOn = OPT_DATEBASEON_COLLECTION_VALUE
                ElseIf moReportBasedOnList.SelectedValue = OPT_DATEBASEON_PROCESSED Then
                    reportBaseOn = OPT_DATEBASEON_PROCESSED_VALUE
                Else
                    Throw New GUIException(Message.MSG_INVALID_BASEON_SELECTION, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_SELECTION)
                End If

                'Dim ftpFilename As String = $"{companyCode}_{dealerCode}_amdocs_billing_{Date.Today.ToString("yyyyMMdd")}_{Date.Now.ToString("hhmmss")}.csv"
                Dim ftpFilename As String = String.Format("{0}_{1}_amdocs_collection_{2}_{3}.txt", companyCode, dealerCode, Date.Today.ToString("yyyyMMdd"), Date.Now.ToString("HHmmss"))

                'reportParams.AppendFormat("pi_User_Id=> '{0}',", userId)
                reportParams.AppendFormat("pi_company_code => '{0}',", companyCode)
                reportParams.AppendFormat("pi_dealer_code => '{0}',", dealerCode)
                reportParams.AppendFormat("pi_start_date => '{0}',", beginDate)
                reportParams.AppendFormat("pi_end_date => '{0}',", endDate)
                reportParams.AppendFormat("pi_baseon => '{0}',", reportBaseOn)
                reportParams.AppendFormat("pi_filename => '{0}'", ftpFilename)


                State.MyBO = New ReportRequests
                State.ForEdit = True
                PopulateBOProperty(State.MyBO, "ReportType", "Collection Amdocs Extract Report")
                PopulateBOProperty(State.MyBO, "ReportProc", "R_AMDOCS_COLLECTION_REPORT.export")
                PopulateBOProperty(State.MyBO, "ReportParameters", reportParams.ToString())
                PopulateBOProperty(State.MyBO, "UserEmailAddress", ElitaPlusIdentity.Current.EmailAddress)
                PopulateBOProperty(State.MyBO, "FtpFilename", ftpFilename)


                ScheduleReport()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub ScheduleReport()
            Try
                Dim scheduleDate As DateTime = TheReportExtractInputControl.GetSchedDate()
                If State.MyBO.IsDirty Then
                    State.MyBO.Save()

                    State.IsNew = False
                    State.HasDataChanged = True
                    State.MyBO.CreateJob(scheduleDate)

                    If String.IsNullOrEmpty(ElitaPlusIdentity.Current.EmailAddress) Then
                        DisplayMessage(Message.MSG_Email_not_configured, "", MSG_BTN_OK, MSG_TYPE_ALERT, , True)
                    Else
                        DisplayMessage(Message.MSG_REPORT_REQUEST_IS_GENERATED, "", MSG_BTN_OK, MSG_TYPE_ALERT, , True)
                    End If

                    'btnGenRpt.Enabled = False

                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub moDealerOptionList_SelectedIndexChanged(sender As Object, e As EventArgs) Handles moDealerOptionList.SelectedIndexChanged
            Try
                If moDealerOptionList.SelectedValue = OPT_DEALER_ALL Then
                    pnlDealerDropControl.Visible = False
                ElseIf moDealerOptionList.SelectedValue = OPT_DEALER_SINGLE Then
                    pnlDealerDropControl.Visible = True
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
    End Class
End Namespace
