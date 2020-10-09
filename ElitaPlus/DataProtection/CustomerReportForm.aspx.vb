
Imports System.Text.RegularExpressions
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Reports

Namespace DataProtection
    Public Class CustomerReportInput
        Public Sub New(userId As String, requestId As String, phoneNumber As String, certificateNumber As String, email As String, taxIdNumber As String, serialNo As String, accountNo As String, invoiceNo As String, languageID As String, cultureCode As String, emailAddress As String)
            Me.UserId = userId
            Me.RequestId = requestId
            Me.PhoneNumber = phoneNumber
            Me.CertificateNumber = certificateNumber
            Me.TaxIdNumber = taxIdNumber
            Me.SerialNo = serialNo
            Me.AccountNo = accountNo
            Me.InvoiceNo = invoiceNo
            Me.LanguageId = languageID
            Me.CultureCode = cultureCode
            Me.Email =email
            Me.EmailAddress = emailAddress
        End Sub

        Public Property UserId As String
        Public Property RequestId As String
        Public Property PhoneNumber As String
        Public Property CertificateNumber As String
        Public Property Email As String
        Public Property TaxIdNumber As String
        Public Property SerialNo As String
        Public Property AccountNo As String
        Public Property InvoiceNo As String
        Public Property LanguageId As String
        Public Property CultureCode As String
        Public Property EmailAddress As String
    End Class

    Public Class CustomerReportForm
        Inherits ElitaPlusPage

#Region "Page State"

        Class MyState
            Public MyBO As ReportRequests
            Public DataProtectionBO As DataProtectionHistory
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

#Region "Page Clear"

        Public Sub ClearSearch()
            Try
                txtCertificate.Text = String.Empty
                txtEmail.Text = String.Empty
                txtTaxIDNumber.Text = String.Empty
                txtInvoice.Text = String.Empty
                txtSerial.Text = String.Empty
                txtAccount.Text = String.Empty
                txtPhoneNumber.Text = String.Empty
                txtRequestID.Text = String.Empty

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Handlers-Pages"

        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            MasterPage.MessageController.Clear()
            ErrorCtrl.Clear_Hide()
            Form.DefaultButton = btnSearch.UniqueID
            Try
                If Not IsPostBack Then
                    MasterPage.MessageController.Clear()
                    MasterPage.UsePageTabTitleInBreadCrum = False
                    UpdateBreadCrum()
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(ErrorCtrl)
        End Sub


        Private Sub UpdateBreadCrum()

            MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator &
                        TranslationBase.TranslateLabelOrMessage("CUSTOMER_PII_REPORT")
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("CUSTOMER_PII_REPORT")

        End Sub


#End Region

#Region "Events Handlers"
        Private Sub btnSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnSearch.Click
            Try
                Dim errors() As ValidationError
                If txtRequestID.Text.Trim().Equals(String.Empty) Then
                    errors = {New ValidationError(ElitaPlus.Common.ErrorCodes.REQUESTID_IS_REQUIRED_ERR, GetType(SearchConsumerForm), Nothing, String.Empty, Nothing)}
                    Throw New BOValidationException(errors, GetType(SearchConsumerForm).FullName)

                ElseIf (txtPhoneNumber.Text.Trim().Equals(String.Empty) _
                                AndAlso txtTaxIDNumber.Text.Trim().Equals(String.Empty) AndAlso txtEmail.Text.Trim().Equals(String.Empty) _
                                AndAlso txtCertificate.Text.Trim().Equals(String.Empty) _
                                AndAlso txtSerial.Text.Trim().Equals(String.Empty) AndAlso txtInvoice.Text.Trim().Equals(String.Empty) AndAlso txtAccount.Text.Trim().Equals(String.Empty)) Then
                    errors = {New ValidationError(ElitaPlus.Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR, GetType(SearchConsumerForm), Nothing, String.Empty, Nothing)}
                    Throw New BOValidationException(errors, GetType(SearchConsumerForm).FullName)
                End If

                If Not txtEmail.Text.Trim().Equals(String.Empty) Then
                    If Not IsValidEmail(txtEmail.Text.Trim()) Then
                        errors = {New ValidationError(ElitaPlus.Common.ErrorCodes.GUI_EMAIL_IS_INVALID_ERR, GetType(SearchConsumerForm), Nothing, String.Empty, Nothing)}
                        Throw New BOValidationException(errors, GetType(SearchConsumerForm).FullName)
                    End If
                End If

                Dim userId As String = GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.Id)
                Dim languageID As String = GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                Dim TheRptCeInputControl As New ReportCeInputControl
                Dim cultureCode As String = TheRptCeInputControl.getCultureValue(True)



                GenerateExtract(New CustomerReportInput(userId, txtRequestID.Text.TrimEnd(), txtPhoneNumber.Text.TrimEnd(),
                                                  txtCertificate.Text.TrimEnd(), txtEmail.Text.TrimEnd(), txtTaxIDNumber.Text.Trim(), txtSerial.Text.Trim(),
                                                  txtAccount.Text.Trim(), txtInvoice.Text.TrimEnd(), languageID, cultureCode, ElitaPlusIdentity.Current.EmailAddress))
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try


        End Sub

        Protected Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click
            ClearSearch()
        End Sub
#End Region

#Region "IsValidEmail"
        Public Function IsValidEmail(email As String) As Boolean
            Dim pattern As String
            pattern = "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
            If Regex.IsMatch(email, pattern) Then
                Return True
            Else
                Return False
            End If

        End Function
#End Region

#Region "Helper functions"
        Public Sub DisplayMessageDilog(strMsg As String, title As String, buttons As String, type As String, Optional ByVal ReturnResponseIn As HtmlInputHidden = Nothing, Optional ByVal Translate As Boolean = True)
            Dim translatedMsg As String = strMsg
            If Translate Then translatedMsg = TranslationBase.TranslateLabelOrMessage(strMsg)
            Dim sJavaScript As String

            Dim id As String = "null"
            If ReturnResponseIn IsNot Nothing Then
                id = ReturnResponseIn.ClientID
            End If
            sJavaScript = "<SCRIPT>" & Environment.NewLine
            sJavaScript &= "try{resizeForm();}catch(e){} showMessageAfterLoaded('" & translatedMsg & "', '" & title & "', '" & buttons & "', '" & type & "', '" & id & "');" & Environment.NewLine
            sJavaScript &= "</SCRIPT>" & Environment.NewLine
            RegisterStartupScript("ShowConfirmation", sJavaScript)

        End Sub
        Public Overridable Function TranslateMesage(label As String) As String
            Return TranslationBase.TranslateLabelOrMessage(label)
        End Function

        ''Private Sub GenerateExtract()
        Private Sub GenerateExtract(consumerInput As CustomerReportInput)
            Dim searchParams As New System.Text.StringBuilder
            searchParams.AppendFormat("PI_IPR_REQUEST_ID => '{0}',", consumerInput.RequestId)
            searchParams.AppendFormat("PI_PHONE_NUMBER => '{0}',", consumerInput.PhoneNumber)
            searchParams.AppendFormat("PI_CERTIFICATE_NUMBER => '{0}',", consumerInput.CertificateNumber)
            searchParams.AppendFormat("PI_EMAIL => '{0}',", consumerInput.Email)
            searchParams.AppendFormat("PI_ID_NUMBER => '{0}',", consumerInput.TaxIdNumber)
            searchParams.AppendFormat("PI_SERIAL_NUMBER => '{0}',", consumerInput.SerialNo)
            searchParams.AppendFormat("PI_ACCOUNT_NUMBER => '{0}',", consumerInput.AccountNo)
            searchParams.AppendFormat("PI_INVOICE => '{0}',", consumerInput.InvoiceNo)
            searchParams.AppendFormat("PI_REPORT_TYPE => '{0}',", "CSV")
            searchParams.AppendFormat("PI_LANGUAGE_ID => '{0}'", consumerInput.LanguageId)
            State.MyBO = New ReportRequests
            PopulateBOProperty(State.MyBO, "ReportType", "Customer_PII_Data_Report")
            PopulateBOProperty(State.MyBO, "ReportProc", "R_CUSTOMER_PII_DATA_EXTRACT.Generate_Customer_Report")
            PopulateBOProperty(State.MyBO, "ReportParameters", searchParams.ToString())
            PopulateBOProperty(State.MyBO, "UserEmailAddress", consumerInput.EmailAddress)

            ScheduleExtract()
        End Sub
        Private Sub ScheduleExtract()
            Try
                Dim reportParams As New System.Text.StringBuilder
                Dim oPage As ElitaPlusPage = CType(Page, ElitaPlusPage)

                If String.IsNullOrEmpty(ElitaPlusIdentity.Current.EmailAddress) Then
                    DisplayMessageDilog(Message.MSG_Email_not_configured, "", oPage.MSG_BTN_OK, oPage.MSG_TYPE_ALERT, , True)
                Else

                    Dim scheduleDate As Date
                    scheduleDate = DateHelper.GetDateValue(DateTime.Now.ToString())
                    State.MyBO.Save()
                    State.MyBO.CreateJob(scheduleDate)
                    DisplayMessageDilog(Message.MSG_REPORT_REQUEST_IS_GENERATED, "", oPage.MSG_BTN_OK, oPage.MSG_TYPE_ALERT, , True)
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub
#End Region
    End Class

End Namespace
