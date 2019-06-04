Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Reports

    Partial Class DealerPaymentReportForm
        Inherits ElitaPlusPage

#Region "Constants"

        Private Const RPT_FILENAME_WINDOW As String = "DEALER PAYMENT"
        Private Const RPT_FILENAME As String = "DealerPayment"
        Private Const RPT_FILENAME_EXPORT As String = "DealerPayment-Exp"

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

        Protected WithEvents moReportCeInputControl As ReportCeInputControl
        Protected WithEvents ErrorCtrl As ErrorController

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
                End If
                Me.InstallProgressBar()
                ClearErrLabels()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
            Me.ShowMissingTranslations(Me.ErrorCtrl)
        End Sub

#End Region

#Region "Handlers-DropDown"

        Private Sub moDealerDrop_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moDealerDrop.SelectedIndexChanged
            Try
                If Me.moDealerDrop.SelectedIndex > Me.BLANK_ITEM_SELECTED Then
                    PopulateFile()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
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

#End Region

#End Region

#Region "Populate"

        Private Sub InitializeForm()
            PopulateDealerDropDown()
            TheRptCeInputControl.populateReportLanguages(RPT_FILENAME)
            'Me.rdealer.Checked = True
            'Me.RadiobuttonTotalsOnly.Checked = True
        End Sub

        Sub PopulateDealerDropDown()
            Dim dealerist As New Collections.Generic.List(Of DataElements.ListItem)
            Dim oListContext As New ListContext

            For Each company_id As Guid In ElitaPlusIdentity.Current.ActiveUser.Companies
                oListContext.CompanyId = company_id
                Dim dealerListForCompany As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerMonthlyBillingByCompany", context:=oListContext)
                If dealerListForCompany.Count > 0 Then
                    If Not dealerist Is Nothing Then
                        dealerist.AddRange(dealerListForCompany)
                    Else
                        dealerist = dealerListForCompany.Clone()
                    End If
                End If
            Next

            moDealerDrop.Populate(dealerist.ToArray(), New PopulateOptions() With
                {
                    .AddBlankItem = True
                })

            'Me.BindListControlToDataView(moDealerDrop, LookupListNew.GetDealerMonthlyBillingLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies))
        End Sub

        'Private Function GetDataView() As DataView
        '    Dim oDealerFileData As DealerFileProcessedData = New DealerFileProcessedData
        '    Dim oDataView As DataView

        '    With oDealerFileData
        '        .dealerCode = LookupListNew.GetCodeFromId(LookupListNew.LK_DEALERS, Me.GetSelectedItem(moDealerDrop))
        '        .fileTypeCode = DealerFileProcessedData.InterfaceTypeCode.PAYM
        '        oDataView = DealerFileProcessed.LoadList(ElitaPlusIdentity.Current.ActiveUser.Companies, oDealerFileData)
        '    End With

        '    oDataView.Table.Columns.Item(0).ColumnName = "ID"
        '    oDataView.Table.Columns.Item(1).ColumnName = "DESCRIPTION"
        '    Return oDataView
        'End Function

        Private Sub PopulateFile()
            Try
                'Dim oDataView As DataView
                'oDataView = GetDataView()
                'oDataView.Sort = "DESCRIPTION"
                'Me.BindListControlToDataView(moPaymentFileDrop, oDataView)

                Dim oListContext As New ListContext
                oListContext.DealerId = Me.GetSelectedItem(moDealerDrop)
                Dim paymentFileListForDealer As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerPaymentFile", context:=oListContext)
                moPaymentFileDrop.Populate(paymentFileListForDealer, New PopulateOptions() With
                    {
                        .AddBlankItem = True
                    })

            Catch ex As Exception
                Me.HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

#End Region

#Region "Clear"

        Private Sub ClearErrLabels()
            Me.ClearLabelErrSign(moDealerLabel)
            Me.ClearLabelErrSign(moPaymentFileLabel)
        End Sub

#End Region

#Region "Crystal Enterprise"

        Function SetParameters(ByVal userId As String, ByVal dealerCode As String, ByVal companyFile As String,
                               ByVal onlyRejected As String, ByVal summary As String) As ReportCeBaseForm.Params
            Dim moReportFormat As ReportCeBaseForm.RptFormat
            Dim params As New ReportCeBaseForm.Params
            Dim reportName As String = TheRptCeInputControl.getReportName(RPT_FILENAME, False)
            Dim culturevalue As String = TheRptCeInputControl.getCultureValue(False)
            moReportFormat = ReportCeBase.GetReportFormat(Me)
            If (moReportFormat = ReportCeBase.RptFormat.TEXT_TAB) OrElse (moReportFormat = ReportCeBase.RptFormat.TEXT_CSV) Then
                reportName = TheRptCeInputControl.getReportName(RPT_FILENAME_EXPORT, False)
                culturevalue = TheRptCeInputControl.getCultureValue(True)
                summary = "N"
                onlyRejected = "N"
            End If

            Me.rptWindowTitle.InnerText = TheRptCeInputControl.getReportWindowTitle(TranslationBase.TranslateLabelOrMessage(RPT_FILENAME_WINDOW))

            Dim repParams() As ReportCeBaseForm.RptParam = New ReportCeBaseForm.RptParam() _
                    {
                     New ReportCeBaseForm.RptParam("V_USER_KEY", userId),
                     New ReportCeBaseForm.RptParam("V_DEALER", dealerCode),
                     New ReportCeBaseForm.RptParam("V_PAYMENT_FILE", companyFile),
                     New ReportCeBaseForm.RptParam("V_REJECTED", onlyRejected),
                     New ReportCeBaseForm.RptParam("V_IS_SUMMARY", summary),
                     New ReportCeBaseForm.RptParam("V_LANG_CULTURE_CODE", culturevalue)}
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

            Dim UserId As String = GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.Id)

            Dim oDealerId As Guid = Me.GetSelectedItem(moDealerDrop)
            Dim dealerCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_DEALERS, oDealerId)
            Dim paymentFile As String = String.Empty

            If Me.moPaymentFileDrop.SelectedIndex <> -1 Then
                paymentFile = Me.moPaymentFileDrop.Items(Me.moPaymentFileDrop.SelectedIndex).Text
            End If

            If oDealerId.Equals(Guid.Empty) Then
                ElitaPlusPage.SetLabelError(moDealerLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
            End If

            If paymentFile.Equals(String.Empty) Then
                ElitaPlusPage.SetLabelError(moPaymentFileLabel)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_PAYMENT_FILE_MUST_BE_SELECTED_ERR)
            End If

            Dim summary As String = "N"
            Dim onlyRejected As String = "N"

            Select Case moCriteriaRadioList.SelectedIndex()
                Case 0
                    summary = "Y"
                Case 1
                    onlyRejected = "Y"
                Case Else
                    summary = "N"
            End Select

            ReportCeBase.EnableReportCe(Me, TheRptCeInputControl)

            Dim params As ReportCeBaseForm.Params = SetParameters(UserId, dealerCode, paymentFile, onlyRejected, summary)
            Session(ReportCeBaseForm.SESSION_PARAMETERS_KEY) = params

        End Sub

#End Region


    End Class

End Namespace
