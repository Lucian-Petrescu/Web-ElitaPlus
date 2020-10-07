Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common

Namespace Reports
    Public Class ClaimEventCountPerCertificateReportForm
        Inherits ElitaPlusPage

#Region "Constants"
        Public Const PAGETITLE As String = "CLAIM_EVENT_COUNT_PER_CERTIFICATE"
        Public Const PAGETAB As String = "REPORTS"
        Public Const DOCTITLE As String = "CLAIM_EVENT_COUNT_PER_CERTIFICATE"
        Public Const ALL As String = "*"
        Public Const NOTHING_SELECTED As Int16 = -1
        Public Const UC_DEALER_AVASEL_AVA_TEXT_COLUMN = "DEALER"
        Public Const UC_DEALER_AVASEL_AVA_GUID_COLUMN = "DEALER_ID"
        Public Const UC_DEALER_AVASEL_SEL_TEXT_COLUMN = "DEALER"
        Public Const UC_DEALER_AVASEL_SEL_GUID_COLUMN = "DEALER_ID"
        Private Const LABEL_SELECT_DEALER_GROUP As String = "DEALER_GROUP"
#End Region

#Region " Web Form Designer Generated Code "
        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
        End Sub

        Protected WithEvents moErrorController As ErrorController
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

        Private Sub OnFromDrop_Changed(fromMultipleDrop As MultipleColumnDDLabelControl_New) Handles ddlcDealerGroup.SelectedDropChanged
            Try
                ucDealerAvaSel.ClearLists()
                If Not ddlcDealerGroup.SelectedGuid = Guid.Empty Then
                    BindAvailableDealers(ddlcDealerGroup.SelectedGuid)
                End If
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub
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

#Region "Helper Functions"
        Private Sub BindAvailableDealers(dealerGroupId As Guid)
            Dim dvSelected As DataView = Dealer.getList(Guid.Empty, ddlcDealerGroup.SelectedGuid, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
            If dvSelected IsNot Nothing Then
                With ucDealerAvaSel
                    .SetAvailableData(dvSelected, UC_DEALER_AVASEL_AVA_TEXT_COLUMN, UC_DEALER_AVASEL_AVA_GUID_COLUMN)
                End With
            End If
        End Sub
#End Region

#Region "Handlers-Init"
        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            MasterPage.MessageController.Clear_Hide()
            Try
                If Not IsPostBack Then
                    TheReportExtractInputControl.ViewVisible = False
                    TheReportExtractInputControl.PdfVisible = False
                    TheReportExtractInputControl.ExportDataVisible = False
                    TheReportExtractInputControl.DestinationVisible = False
                    MasterPage.UsePageTabTitleInBreadCrum = False
                    SetFormTab(PAGETAB)
                    UpdateBreadCrum()
                    InitializeForm()
                End If
                InstallDisplayNewReportProgressBar()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub

#End Region


        Private Sub InitializeForm()
            'Set Calendar
            AddCalendar_New(BtnDate, moDateText)
            PopulateDealerGroupDropDown()
        End Sub


#Region "Pupulate"
        Private Sub UpdateBreadCrum()
            MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(PAGETITLE)
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        End Sub

        Protected Sub PopulateDealerGroupDropDown()
            Dim dv As DataView = LookupListNew.GetDealerGroupLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
            ddlcDealerGroup.SetControl(True, MultipleColumnDDLabelControl_New.MODES.NEW_MODE, True, dv, "00000", True, True)
        End Sub
#End Region

#Region "Handlers-Buttons"


        Private Sub btnGenRpt_Click(sender As System.Object, e As System.EventArgs) Handles btnGenRpt.Click
            Try
                GenerateReport()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub


#End Region

#Region "Clear"
        Public Sub ClearLabelsErrSign()
            Try
                ClearLabelErrSign(lblDate)
                ClearLabelErrSign(lblDealers)
                ClearLabelErrSign(lblRepairedClaims)
                ClearLabelErrSign(lblReplacedClaims)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region


        Private Sub GenerateReport()
            ClearLabelsErrSign()
            Dim runReport As Boolean = True

            If ucDealerAvaSel.SelectedList.Count = 0 Then
                'Dealer Selection is Mandatory
                ElitaPlusPage.SetLabelError(lblDealers)
                Try
                    Throw New GUIException(Message.MSG_SELECT_DEALERS, Assurant.ElitaPlus.Common.ErrorCodes.GUI_SELECT_DEALER)
                Catch ex As Exception
                    HandleErrors(ex, MasterPage.MessageController)
                    runReport = False
                End Try
            End If

            Dim reportDate As New Date

            If (String.IsNullOrWhiteSpace(moDateText.Text)) Then
                'Date is Missing
                SetLabelError(lblDate)
                Try
                    Throw New GUIException(Message.MSG_DATE_MANDATORY, Assurant.ElitaPlus.Common.ErrorCodes.GUI_EMPTY_DATE)
                Catch ex As Exception
                    HandleErrors(ex, MasterPage.MessageController)
                    runReport = False
                End Try
            Else
                Try
                    reportDate = Convert.ToDateTime(moDateText.Text)
                Catch
                    'Invalid Date
                    SetLabelError(lblDate)
                    Try
                        Throw New GUIException(Message.MSG_INVALID_DATE, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_DATE_ERR)
                    Catch ex As Exception
                        HandleErrors(ex, MasterPage.MessageController)
                        runReport = False
                    End Try
                End Try
                'Future Date Validation
                If reportDate > Date.Now Then
                    'Date in Future
                    SetLabelError(lblDate)
                    Try
                        Throw New GUIException(Message.MSG_FUTURE_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_FUTURE_DATE)
                    Catch ex As Exception
                        HandleErrors(ex, MasterPage.MessageController)
                        runReport = False
                    End Try
                End If
            End If
            If String.IsNullOrWhiteSpace(moRepairedClaimsText.Text) And String.IsNullOrWhiteSpace(moReplacedClaimsText.Text) Then
                'Both (Number of Replaced Claims and Number of Replacement Claims) are Null
                SetLabelError(lblRepairedClaims)
                SetLabelError(lblReplacedClaims)
                Try
                    Throw New GUIException(Message.MSG_CLAIM_NUMBERS_MANDATORY, Assurant.ElitaPlus.Common.ErrorCodes.GUI_CLAIM_NUMBERS_MANDATORY)
                Catch ex As Exception
                    HandleErrors(ex, MasterPage.MessageController)
                    runReport = False
                End Try
            End If

            If Not String.IsNullOrWhiteSpace(moRepairedClaimsText.Text) Then
                If System.Char.IsNumber(moRepairedClaimsText.Text) = False Then
                    'Repaired Claim Number is Invalid
                    SetLabelError(lblRepairedClaims)
                    Try
                        Throw New GUIException(Message.MSG_INVALID_NUMBER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_NUMBER)
                    Catch ex As Exception
                        HandleErrors(ex, MasterPage.MessageController)
                        runReport = False
                    End Try
                End If

                Try
                    If CInt(moRepairedClaimsText.Text) < 0 Then
                        'Repair Claim Number is Negative
                        SetLabelError(lblRepairedClaims)
                        Try
                            Throw New GUIException(Message.MSG_NUMBER_NEGATIVE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_NUMBER)
                        Catch ex As Exception
                            HandleErrors(ex, MasterPage.MessageController)
                            runReport = False
                        End Try
                    End If
                Catch ex As OverflowException
                    Try
                        'Repair Claim Number is too long
                        SetLabelError(lblRepairedClaims)
                        Throw New GUIException(Message.MSG_NUMBER_TOO_LONG, Assurant.ElitaPlus.Common.ErrorCodes.GUI_NUMBER_TOO_LONG)
                    Catch ex1 As Exception
                        HandleErrors(ex1, MasterPage.MessageController)
                        runReport = False
                    End Try
                End Try
            End If

            If Not String.IsNullOrWhiteSpace(moReplacedClaimsText.Text) Then
                If System.Char.IsNumber(moReplacedClaimsText.Text) = False Then
                    'Replacement Claim Number is Invalid
                    SetLabelError(lblReplacedClaims)
                    Try
                        Throw New GUIException(Message.MSG_INVALID_NUMBER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_NUMBER)
                    Catch ex As Exception
                        HandleErrors(ex, MasterPage.MessageController)
                        runReport = False
                    End Try
                End If

                Try
                    If CInt(moReplacedClaimsText.Text) < 0 Then
                        'Repalcement Claim Number is Negative
                        ElitaPlusPage.SetLabelError(lblRepairedClaims)
                        Try
                            Throw New GUIException(Message.MSG_NUMBER_NEGATIVE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_NUMBER)
                        Catch ex As Exception
                            HandleErrors(ex, MasterPage.MessageController)
                            runReport = False
                        End Try
                    End If
                Catch ex As OverflowException
                    Try
                        'Repalcement Claim Number is too long
                        ElitaPlusPage.SetLabelError(lblRepairedClaims)
                        Throw New GUIException(Message.MSG_NUMBER_TOO_LONG, Assurant.ElitaPlus.Common.ErrorCodes.GUI_NUMBER_TOO_LONG)
                    Catch ex1 As Exception
                        HandleErrors(ex1, MasterPage.MessageController)
                        runReport = False
                    End Try
                End Try
            End If

            If runReport Then 'One ore more invalid input(s)
                Dim reportParams As New System.Text.StringBuilder
                reportParams.AppendFormat("pi_dealer => '{0}',", String.Join(",", ucDealerAvaSel.SelectedList.ToArray().Select(Function(did) DALObjects.DALBase.GuidToSQLString(New Guid(DirectCast(did, String))))))
                reportParams.AppendFormat("pi_date => '{0}',", moDateText.Text)

                If String.IsNullOrWhiteSpace(moRepairedClaimsText.Text) Then
                    reportParams.AppendFormat("pi_expected_repair => 0,")
                Else
                    reportParams.AppendFormat("pi_expected_repair => '{0}',", moRepairedClaimsText.Text)
                End If

                If String.IsNullOrWhiteSpace(moReplacedClaimsText.Text) Then
                    reportParams.AppendFormat("pi_expected_replacement => 0")
                Else
                    reportParams.AppendFormat("pi_expected_replacement => '{0}'", moReplacedClaimsText.Text)
                End If

                State.MyBO = New ReportRequests
                State.ForEdit = True
                PopulateBOProperty(State.MyBO, "ReportType", "CLAIMCOUNTREPORT")
                PopulateBOProperty(State.MyBO, "ReportProc", "R_DealerClaimEvents.Report")
                PopulateBOProperty(State.MyBO, "ReportParameters", reportParams.ToString())
                PopulateBOProperty(State.MyBO, "UserEmailAddress", ElitaPlusIdentity.Current.EmailAddress)

                ScheduleReport()
            End If
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

                    btnGenRpt.Enabled = False

                Else
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
    End Class
End Namespace