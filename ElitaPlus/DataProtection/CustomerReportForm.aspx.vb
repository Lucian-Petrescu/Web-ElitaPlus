
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
                Me.txtCertificate.Text = String.Empty
                Me.txtEmail.Text = String.Empty
                Me.txtTaxIDNumber.Text = String.Empty
                Me.txtInvoice.Text = String.Empty
                Me.txtSerial.Text = String.Empty
                Me.txtAccount.Text = String.Empty
                Me.txtPhoneNumber.Text = String.Empty
                Me.txtRequestID.Text = String.Empty

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Handlers-Pages"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Me.MasterPage.MessageController.Clear()
            Me.ErrorCtrl.Clear_Hide()
            Me.Form.DefaultButton = btnSearch.UniqueID
            Try
                If Not Me.IsPostBack Then
                    Me.MasterPage.MessageController.Clear()
                    Me.MasterPage.UsePageTabTitleInBreadCrum = False
                    UpdateBreadCrum()
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.ErrorCtrl)
        End Sub


        Private Sub UpdateBreadCrum()

            Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator &
                        TranslationBase.TranslateLabelOrMessage("CUSTOMER_PII_REPORT")
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("CUSTOMER_PII_REPORT")

        End Sub


#End Region

#Region "Events Handlers"
        Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
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
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
        Public Sub DisplayMessageDilog(ByVal strMsg As String, ByVal title As String, ByVal buttons As String, ByVal type As String, Optional ByVal ReturnResponseIn As HtmlInputHidden = Nothing, Optional ByVal Translate As Boolean = True)
            Dim translatedMsg As String = strMsg
            If Translate Then translatedMsg = TranslationBase.TranslateLabelOrMessage(strMsg)
            Dim sJavaScript As String

            Dim id As String = "null"
            If Not ReturnResponseIn Is Nothing Then
                id = ReturnResponseIn.ClientID
            End If
            sJavaScript = "<SCRIPT>" & Environment.NewLine
            sJavaScript &= "try{resizeForm();}catch(e){} showMessageAfterLoaded('" & translatedMsg & "', '" & title & "', '" & buttons & "', '" & type & "', '" & id & "');" & Environment.NewLine
            sJavaScript &= "</SCRIPT>" & Environment.NewLine
            Me.RegisterStartupScript("ShowConfirmation", sJavaScript)

        End Sub
        Public Overridable Function TranslateMesage(ByVal label As String) As String
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
            Me.State.MyBO = New ReportRequests
            Me.PopulateBOProperty(Me.State.MyBO, "ReportType", "Customer_PII_Data_Report")
            Me.PopulateBOProperty(Me.State.MyBO, "ReportProc", "R_CUSTOMER_PII_DATA_EXTRACT.Generate_Customer_Report")
            Me.PopulateBOProperty(Me.State.MyBO, "ReportParameters", searchParams.ToString())
            Me.PopulateBOProperty(Me.State.MyBO, "UserEmailAddress", consumerInput.EmailAddress)

            ScheduleExtract()
        End Sub
        Private Sub ScheduleExtract()
            Try
                Dim reportParams As New System.Text.StringBuilder
                Dim oPage As ElitaPlusPage = CType(Me.Page, ElitaPlusPage)

                If String.IsNullOrEmpty(ElitaPlusIdentity.Current.EmailAddress) Then
                    DisplayMessageDilog(Message.MSG_Email_not_configured, "", oPage.MSG_BTN_OK, oPage.MSG_TYPE_ALERT, , True)
                Else

                    Dim scheduleDate As Date
                    scheduleDate = DateHelper.GetDateValue(DateTime.Now.ToString())
                    Me.State.MyBO.Save()
                    Me.State.MyBO.CreateJob(scheduleDate)
                    DisplayMessageDilog(Message.MSG_REPORT_REQUEST_IS_GENERATED, "", oPage.MSG_BTN_OK, oPage.MSG_TYPE_ALERT, , True)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub
#End Region
    End Class

End Namespace
