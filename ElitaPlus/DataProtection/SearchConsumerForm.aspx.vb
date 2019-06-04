Imports System.Text.RegularExpressions
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Reports
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace DataProtection
    Public Class CustomerInput
        Public Sub New(userId As String, requestId As String, phoneNumber As String, certificateNumber As String, customerName As String, email As String, address As String, zipCode As String, gender As String, taxIdNumber As String, birtDate As String, dealerID As String, serialNo As String, accountNo As String, invoiceNo As String, languageID As String, cultureCode As String, emailAddress As String,includeRecon As String)
            Me.UserId = userId
            Me.RequestId = requestId
            Me.PhoneNumber = phoneNumber
            Me.CertificateNumber = certificateNumber
            Me.CustomerName = customerName
            Me.Email = email
            Me.Address = address
            Me.ZipCode = zipCode
            Me.Gender = gender
            Me.TaxIdNumber = taxIdNumber
            Me.BirtDate = birtDate
            Me.DealerId = dealerID
            Me.SerialNo = serialNo
            Me.AccountNo = accountNo
            Me.InvoiceNo = invoiceNo
            Me.LanguageId = languageID
            Me.CultureCode = cultureCode
            Me.EmailAddress = emailAddress
            Me.IncludeRecon = includeRecon
        End Sub

        Public Property UserId As String
        Public Property RequestId As String
        Public Property PhoneNumber As String
        Public Property CertificateNumber As String
        Public Property CustomerName As String
        Public Property Email As String
        Public Property Address As String
        Public Property ZipCode As String
        Public Property Gender As String
        Public Property TaxIdNumber As String
        Public Property BirtDate As String
        Public Property DealerId As String
        Public Property SerialNo As String
        Public Property AccountNo As String
        Public Property InvoiceNo As String
        Public Property LanguageId As String
        Public Property CultureCode As String
        Public Property EmailAddress As String
        Public Property IncludeRecon As String
    End Class

    Public Class SearchConsumerForm
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
                Me.txtCustomerName.Text = String.Empty
                Me.txtEmail.Text = String.Empty
                Me.txtAddress.Text = String.Empty
                Me.txtZip.Text = String.Empty
                Me.txtTaxIDNumber.Text = String.Empty
                Me.txtInvoice.Text = String.Empty
                Me.txtBirthDate.Text = String.Empty
                Me.txtSerial.Text = String.Empty
                Me.txtAccount.Text = String.Empty
                Me.ddlDealer.SelectedIndex = 0
                Me.ddlGender.SelectedIndex = 0
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
                    Me.AddCalendar_New(Me.btntxtBirthDate, Me.txtBirthDate)
                    PopulateDropDowns()
                    UpdateBreadCrum()
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.ErrorCtrl)
        End Sub


        Private Sub UpdateBreadCrum()

            Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator &
                        TranslationBase.TranslateLabelOrMessage("CONSUMER_SEARCH")
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("CONSUMER_SEARCH")

        End Sub


#End Region

#Region "Events Handlers"
        Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            Try
                Dim errors() As ValidationError
                If txtRequestID.Text.Trim().Equals(String.Empty) Then
                    errors = {New ValidationError(ElitaPlus.Common.ErrorCodes.REQUESTID_IS_REQUIRED_ERR, GetType(SearchConsumerForm), Nothing, String.Empty, Nothing)}
                    Throw New BOValidationException(errors, GetType(SearchConsumerForm).FullName)
                ElseIf (txtPhoneNumber.Text.Trim().Equals(String.Empty) AndAlso txtCustomerName.Text.Trim().Equals(String.Empty) AndAlso
                    txtZip.Text.Trim().Equals(String.Empty) AndAlso txtAddress.Text.Trim().Equals(String.Empty) AndAlso ddlGender.SelectedItem.Text.Trim().ToLower().Equals(String.Empty) _
                    AndAlso txtTaxIDNumber.Text.Trim().Equals(String.Empty) AndAlso txtEmail.Text.Trim().Equals(String.Empty) _
                    AndAlso txtBirthDate.Text.Trim().Equals(String.Empty) AndAlso ddlDealer.SelectedItem.Text.Trim().Equals(String.Empty) AndAlso txtCertificate.Text.Trim().Equals(String.Empty) _
                    AndAlso txtSerial.Text.Trim().Equals(String.Empty) AndAlso txtInvoice.Text.Trim().Equals(String.Empty) AndAlso txtAccount.Text.Trim().Equals(String.Empty)) Then
                    errors = {New ValidationError(ElitaPlus.Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR, GetType(SearchConsumerForm), Nothing, String.Empty, Nothing)}
                    Throw New BOValidationException(errors, GetType(SearchConsumerForm).FullName)

                ElseIf (Not txtZip.Text.Trim().Equals(String.Empty)) And (txtPhoneNumber.Text.Trim().Trim().Equals(String.Empty) _
                        AndAlso txtCustomerName.Text.Trim().Equals(String.Empty) _
                    AndAlso txtTaxIDNumber.Text.Trim().Equals(String.Empty) AndAlso txtEmail.Text.Trim().Equals(String.Empty) _
                     AndAlso txtCertificate.Text.Trim().Equals(String.Empty) _
                    AndAlso txtSerial.Text.Trim().Equals(String.Empty) AndAlso txtInvoice.Text.Trim().Equals(String.Empty) AndAlso txtAccount.Text.Trim().Equals(String.Empty)) Then
                    errors = {New ValidationError(ElitaPlus.Common.ErrorCodes.GUI_ERR_ADDITIONAL_SEARCH_CRITERION, GetType(SearchConsumerForm), Nothing, String.Empty, Nothing)}
                    Throw New BOValidationException(errors, GetType(SearchConsumerForm).FullName)

                ElseIf (Not txtAddress.Text.Trim().Equals(String.Empty)) And (txtPhoneNumber.Text.Trim().Equals(String.Empty) _
                   AndAlso txtCustomerName.Text.Trim().Equals(String.Empty) AndAlso
                     txtTaxIDNumber.Text.Trim().Equals(String.Empty) AndAlso txtEmail.Text.Trim().Equals(String.Empty) _
              AndAlso txtCertificate.Text.Trim().Equals(String.Empty) _
              AndAlso txtSerial.Text.Trim().Equals(String.Empty) AndAlso txtInvoice.Text.Trim().Equals(String.Empty) AndAlso txtAccount.Text.Trim().Equals(String.Empty)) Then
                    errors = {New ValidationError(ElitaPlus.Common.ErrorCodes.GUI_ERR_ADDITIONAL_SEARCH_CRITERION, GetType(SearchConsumerForm), Nothing, String.Empty, Nothing)}
                    Throw New BOValidationException(errors, GetType(SearchConsumerForm).FullName)

                ElseIf (Not ddlGender.SelectedItem.Text.Trim().Equals(String.Empty)) And (txtPhoneNumber.Text.Trim().Equals(String.Empty) _
                        AndAlso txtCustomerName.Text.Trim().Equals(String.Empty) AndAlso
                     txtTaxIDNumber.Text.Trim().Equals(String.Empty) AndAlso txtEmail.Text.Trim().Equals(String.Empty) _
                   AndAlso txtCertificate.Text.Trim().Equals(String.Empty) _
                    AndAlso txtSerial.Text.Trim().Equals(String.Empty) AndAlso txtInvoice.Text.Trim().Equals(String.Empty) AndAlso txtAccount.Text.Trim().Equals(String.Empty)) Then
                    errors = {New ValidationError(ElitaPlus.Common.ErrorCodes.GUI_ERR_ADDITIONAL_SEARCH_CRITERION, GetType(SearchConsumerForm), Nothing, String.Empty, Nothing)}
                    Throw New BOValidationException(errors, GetType(SearchConsumerForm).FullName)

                ElseIf (Not txtBirthDate.Text.Trim().Equals(String.Empty)) And (txtPhoneNumber.Text.Trim().Equals(String.Empty) _
                   AndAlso txtCustomerName.Text.Trim().Equals(String.Empty) _
               AndAlso txtTaxIDNumber.Text.Trim().Equals(String.Empty) AndAlso txtEmail.Text.Trim().Equals(String.Empty) _
               AndAlso txtCertificate.Text.Trim().Equals(String.Empty) _
               AndAlso txtSerial.Text.Trim().Equals(String.Empty) AndAlso txtInvoice.Text.Trim().Equals(String.Empty) AndAlso txtAccount.Text.Trim().Equals(String.Empty)) Then
                    errors = {New ValidationError(ElitaPlus.Common.ErrorCodes.GUI_ERR_ADDITIONAL_SEARCH_CRITERION, GetType(SearchConsumerForm), Nothing, String.Empty, Nothing)}
                    Throw New BOValidationException(errors, GetType(SearchConsumerForm).FullName)

                ElseIf (Not ddlDealer.SelectedItem.Text.Trim().Equals(String.Empty)) And (txtPhoneNumber.Text.Trim().Equals(String.Empty) _
                   AndAlso txtCustomerName.Text.Trim().Equals(String.Empty) _
               AndAlso txtTaxIDNumber.Text.Trim().Equals(String.Empty) AndAlso txtEmail.Text.Trim().Equals(String.Empty) _
               AndAlso txtCertificate.Text.Trim().Equals(String.Empty) _
               AndAlso txtSerial.Text.Trim().Equals(String.Empty) AndAlso txtInvoice.Text.Trim().Equals(String.Empty) AndAlso txtAccount.Text.Trim().Equals(String.Empty)) Then
                    errors = {New ValidationError(ElitaPlus.Common.ErrorCodes.GUI_ERR_ADDITIONAL_SEARCH_CRITERION, GetType(SearchConsumerForm), Nothing, String.Empty, Nothing)}
                    Throw New BOValidationException(errors, GetType(SearchConsumerForm).FullName)
                End If

                If Not txtEmail.Text.Trim().Equals(String.Empty) Then
                    If Not IsValidEmail(txtEmail.Text.Trim()) Then
                        errors = {New ValidationError(ElitaPlus.Common.ErrorCodes.GUI_EMAIL_IS_INVALID_ERR, GetType(SearchConsumerForm), Nothing, String.Empty, Nothing)}
                        Throw New BOValidationException(errors, GetType(SearchConsumerForm).FullName)
                    End If
                End If

                If Not txtBirthDate.Text.Trim().Equals(String.Empty) Then
                    If DateHelper.GetDateValue(txtBirthDate.Text.Trim()) = DateTime.MinValue Then
                        errors = {New ValidationError(TranslationBase.TranslateLabelOrMessage("BIRTH_DATE") & " : " & TranslationBase.TranslateLabelOrMessage(Messages.INVALID_DATE_ERR), GetType(Certificate), Nothing, "Inforce Date", Nothing)}
                        Throw New BOValidationException(errors, GetType(Certificate).FullName)
                    ElseIf txtBirthDate.Text.Trim() > Date.Now Then
                        errors = {New ValidationError(ElitaPlus.Common.ErrorCodes.GUI_SEARCH_WITH_BIRTH_DATE_VAL_ERROR, GetType(SearchConsumerForm), Nothing, String.Empty, Nothing)}
                        Throw New BOValidationException(errors, GetType(SearchConsumerForm).FullName)
                    End If

                End If

                If Me.State.DataProtectionBO.GetRequestIdUsedInfo(txtRequestID.Text.Trim()) Then
                    errors = {New ValidationError(ElitaPlus.Common.ErrorCodes.REQUEST_ID_IS_USED_ERR, GetType(SearchConsumerForm), Nothing, String.Empty, Nothing)}
                    Throw New BOValidationException(errors, GetType(SearchConsumerForm).FullName)
                End If


                Dim userId As String = GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.Id)
                Dim gender As String = GuidControl.GuidToHexString(Me.GetSelectedItem(Me.ddlGender))
                Dim dealerID As String = GuidControl.GuidToHexString(Me.GetSelectedItem(Me.ddlDealer))
                Dim languageID As String = GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                Dim TheRptCeInputControl As New ReportCeInputControl
                Dim cultureCode As String = TheRptCeInputControl.getCultureValue(True)
                Dim includeRecon As String = "N"
                If chkIncludeRecon.Checked Then
                    includeRecon = "Y"
                Else
                    includeRecon = "N"
                End If

                GenerateExtract(New CustomerInput(userId, txtRequestID.Text.TrimEnd(), txtPhoneNumber.Text.TrimEnd(),
                                                  txtCertificate.Text.TrimEnd(), txtCustomerName.Text.TrimEnd(), txtEmail.Text.TrimEnd(),
                                                  txtAddress.Text.TrimEnd(), txtZip.Text.TrimEnd(), gender, txtTaxIDNumber.Text.TrimEnd(),
                                                  txtBirthDate.Text.TrimEnd(), dealerID, txtSerial.Text.TrimEnd(), txtAccount.Text.TrimEnd(),
                                                  txtInvoice.Text.TrimEnd(), languageID, cultureCode, ElitaPlusIdentity.Current.EmailAddress, includeRecon))
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
        Private Sub PopulateDropDowns()
            Try
                'populate dealer list
                'Dim dvDealer As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "Code")
                'Me.BindListControlToDataView(ddlDealer, dvDealer, , , True, True, False)

                'Dim dvGender As DataView = LookupListNew.GetGenderLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                'Me.BindListControlToDataView(ddlGender, dvGender, , , True, True, False)

                Dim DealerList As New Collections.Generic.List(Of DataElements.ListItem)
                For Each CompanyId As Guid In ElitaPlusIdentity.Current.ActiveUser.Companies
                    Dim Dealers As DataElements.ListItem() =
                    CommonConfigManager.Current.ListManager.GetList(listCode:="DealerListByCompany",
                                                        context:=New ListContext() With
                                                        {
                                                          .CompanyId = CompanyId
                                                        })

                    If Dealers.Count > 0 Then
                        If Not DealerList Is Nothing Then
                            DealerList.AddRange(Dealers)
                        Else
                            DealerList = Dealers.Clone()
                        End If
                    End If
                Next

                ddlDealer.Populate(DealerList.ToArray(),
                        New PopulateOptions() With
                        {
                            .AddBlankItem = True
                        })

                Dim Gender As DataElements.ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:="GENDER",
                                                                languageCode:=Thread.CurrentPrincipal.GetLanguageCode())

                ddlGender.Populate(Gender.ToArray(),
                                New PopulateOptions() With
                                {
                                    .AddBlankItem = True
                                })

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub
        ''Private Sub GenerateExtract()
        Private Sub GenerateExtract(consumerInput As CustomerInput)
            Dim searchParams As New System.Text.StringBuilder
            searchParams.AppendFormat("V_USER_KEY => '{0}',", consumerInput.UserId)
            searchParams.AppendFormat("V_REQUEST_ID => '{0}',", consumerInput.RequestId)
            searchParams.AppendFormat("V_PHONE_NUMBER => '{0}',", consumerInput.PhoneNumber)
            searchParams.AppendFormat("V_CERTIFICATE_NUMBER => '{0}',", consumerInput.CertificateNumber)
            searchParams.AppendFormat("V_CUSTOMER_NAME => '{0}',", consumerInput.CustomerName)
            searchParams.AppendFormat("V_EMAIL => '{0}',", consumerInput.Email)
            searchParams.AppendFormat("V_ADDRESS => '{0}',", consumerInput.Address)
            searchParams.AppendFormat("V_ZIP => '{0}',", consumerInput.ZipCode)
            searchParams.AppendFormat("V_GENDER => '{0}',", consumerInput.Gender)
            searchParams.AppendFormat("V_ID_NUMBER => '{0}',", consumerInput.TaxIdNumber)
            searchParams.AppendFormat("V_BIRTH_DATE => '{0}',", consumerInput.BirtDate)
            searchParams.AppendFormat("V_DEALER => '{0}',", consumerInput.DealerId)
            searchParams.AppendFormat("V_SERIAL_NUMBER => '{0}',", consumerInput.SerialNo)
            searchParams.AppendFormat("V_ACCOUNT_NUMBER => '{0}',", consumerInput.AccountNo)
            searchParams.AppendFormat("V_INVOICE => '{0}',", consumerInput.InvoiceNo)
            searchParams.AppendFormat("V_REPORT_TYPE => '{0}',", "CSV")
            searchParams.AppendFormat("V_LANG_CULTURE_CODE => '{0}',", consumerInput.CultureCode)
            searchParams.AppendFormat("V_LANGUAGE_ID => '{0}',", consumerInput.LanguageId)
            searchParams.AppendFormat("V_RECON_RECORDS => '{0}'", consumerInput.IncludeRecon)
            Me.State.MyBO = New ReportRequests
            Me.PopulateBOProperty(Me.State.MyBO, "ReportType", "CUSTOMER_SEARCH_EXTRACT")
            Me.PopulateBOProperty(Me.State.MyBO, "ReportProc", "R_CONSUMER_SEARCH.Generate_Consumer_Search")
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
