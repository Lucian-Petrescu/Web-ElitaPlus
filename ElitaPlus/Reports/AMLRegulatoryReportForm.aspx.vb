﻿Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Reports
    Partial Public Class AMLRegulatoryReportForm
        Inherits ElitaPlusPage

#Region "Constants"

        Public Const PAGETITLE As String = "AML_REGULATORY"
        Public Const ALL As String = "*"


#Region "Handlers"

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

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

#Region "Handlers-Init"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Me.MasterPage.MessageController.Clear_Hide()
            '    Me.ClearLabelsErrSign()
            Try
                If Not Me.IsPostBack Then
                    Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                    TheReportExtractInputControl.ViewVisible = False
                    TheReportExtractInputControl.PdfVisible = False
                    TheReportExtractInputControl.ExportDataVisible = False
                    TheReportExtractInputControl.DestinationVisible = False
                    Me.MasterPage.UsePageTabTitleInBreadCrum = False
                    'BindCodeNameToListControl(ddlAuthority, LookupListNew.DropdownLookupList("UFI_AUTHORITY", langId, True), , , , True)
                    ddlAuthority.Populate(CommonConfigManager.Current.ListManager.GetList("UFI_AUTHORITY", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                        {
                            .AddBlankItem = True,
                            .TextFunc = Function(x)
                                            Return x.Translation + " (" + x.Code + ")"
                                        End Function
                        })

                    UpdateBreadCrum()
                End If
                Me.InstallDisplayNewReportProgressBar()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)
        End Sub

#End Region

#Region "Handlers-Buttons"


        Private Sub btnGenRpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenRpt.Click
            Try
                GenerateReport()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub


#End Region

#End Region

        Private Sub UpdateBreadCrum()
            Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(PAGETITLE)
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        End Sub

#End Region
        Private Sub GenerateReport()


            Dim reportParams As New System.Text.StringBuilder
            Dim selectedAuthorityId As Guid = GetSelectedItem(Me.ddlAuthority)
            Dim selectedAuthority As String = LookupListNew.GetCodeFromId("UFI_AUTHORITY", selectedAuthorityId)
            'Dim objCountry As Country = ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.CompanyId)

            If ElitaPlusIdentity.Current.ActiveUser.Companies.Count > 1 Then
                Me.DisplayMessage(Message.MSG_Email_not_configured, "", MSG_BTN_OK, MSG_TYPE_ALERT, , True)
            Else
                Dim objCountry As Country = ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
                reportParams.AppendFormat("pi_country_code => '{0}',", objCountry.Code)
                reportParams.AppendFormat("pi_authority => '{0}'", selectedAuthority)
                'reportParams.AppendFormat("pi_country_code => '{0}' , pi_authority => '{1}'", objCountry.Code, selectedAuthority)
                Me.State.MyBO = New ReportRequests

                Me.PopulateBOProperty(Me.State.MyBO, "ReportType", "AML Regulatory")
                Me.PopulateBOProperty(Me.State.MyBO, "ReportProc", "R_UFIEXTRACT.report")
                Me.PopulateBOProperty(Me.State.MyBO, "ReportParameters", reportParams.ToString())
                Me.PopulateBOProperty(Me.State.MyBO, "UserEmailAddress", ElitaPlusIdentity.Current.EmailAddress)

                ScheduleReport()
            End If


        End Sub

        Private Sub ScheduleReport()
            Try
                Dim scheduleDate As DateTime = TheReportExtractInputControl.GetSchedDate()
                If Me.State.MyBO.IsDirty Then
                    Me.State.MyBO.Save()

                    Me.State.IsNew = False
                    Me.State.HasDataChanged = True
                    Me.State.MyBO.CreateJob(scheduleDate)
                    If String.IsNullOrEmpty(ElitaPlusIdentity.Current.EmailAddress) Then
                        Me.DisplayMessage(Message.MSG_Email_not_configured, "", MSG_BTN_OK, MSG_TYPE_ALERT, , True)
                    Else
                        Me.DisplayMessage(Message.MSG_REPORT_REQUEST_IS_GENERATED, "", MSG_BTN_OK, MSG_TYPE_ALERT, , True)
                    End If
                    btnGenRpt.Enabled = False
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

    End Class
End Namespace

