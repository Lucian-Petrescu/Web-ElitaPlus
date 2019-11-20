Imports System.IO
Imports System.Text
Imports System.Web.Mail
Imports Assurant.Common.AppNavigationControl


Public Class ProcessAllServiceOrders
    Implements IStateController

#Region "Private Atributes"
    Private NavController As INavigationController
    Private CallingPage As ElitaPlusPage

#End Region

#Region "Internal State Managment"
    Enum ProcessingStage
        Processing_Start
        Processing_New_SO
        Processing_Old_SO
    End Enum
    Class MyState
        Public Stage As ProcessingStage = ProcessingStage.Processing_Start
    End Class

    Public ReadOnly Property State() As MyState
        Get
            If Me.NavController.State Is Nothing Then
                Me.NavController.State = New MyState
            End If
            Return CType(Me.NavController.State, MyState)
        End Get
    End Property
#End Region

    Public Sub Process(ByVal callingPage As System.Web.UI.Page, ByVal navCtrl As INavigationController) Implements IStateController.Process
        Me.NavController = navCtrl
        Me.CallingPage = CType(callingPage, ElitaPlusPage)

        Try
            Select Case Me.State.Stage
                Case ProcessingStage.Processing_Start
                    If Not NavController.FlowSession(FlowSessionKeys.SESSION_SERVICE_ORDER) Is Nothing Then
                        Me.State.Stage = ProcessingStage.Processing_New_SO
                        NavController.Navigate(callingPage, FlowEvents.EVENT_PROCESS_SERVICEORDER, NavController.FlowSession(FlowSessionKeys.SESSION_SERVICE_ORDER))
                    ElseIf Not NavController.FlowSession(FlowSessionKeys.SESSION_OLD_SERVICE_ORDER) Is Nothing Then
                        Me.State.Stage = ProcessingStage.Processing_Old_SO
                        NavController.Navigate(callingPage, FlowEvents.EVENT_PROCESS_SERVICEORDER, NavController.FlowSession(FlowSessionKeys.SESSION_OLD_SERVICE_ORDER))
                    Else
                        EndProcess()
                    End If
                Case ProcessingStage.Processing_New_SO
                    If Not NavController.FlowSession(FlowSessionKeys.SESSION_OLD_SERVICE_ORDER) Is Nothing Then
                        Me.State.Stage = ProcessingStage.Processing_Old_SO
                        NavController.Navigate(callingPage, FlowEvents.EVENT_PROCESS_SERVICEORDER, NavController.FlowSession(FlowSessionKeys.SESSION_OLD_SERVICE_ORDER))
                    Else
                        EndProcess()
                    End If
                Case ProcessingStage.Processing_Old_SO
                    EndProcess()
            End Select
        Catch ex As Threading.ThreadAbortException
        End Try

    End Sub

    Sub EndProcess()
        Me.NavController.FlowSession(FlowSessionKeys.SESSION_PREV_SERVICEORDER) = Nothing
        Me.NavController.FlowSession(FlowSessionKeys.SESSION_NEXT_SERVICEORDER) = Nothing
        Me.NavController.FlowSession(FlowSessionKeys.SESSION_EMAIL_SENT) = Nothing
        Me.NavController.Navigate(Me.CallingPage, FlowEvents.EVENT_BACK)
    End Sub


End Class

Public Class ProcessServiceOrder
    Implements IStateController

#Region "Private Atributes"
    Private NavController As INavigationController
    Private CallingPage As ElitaPlusPage
    Private ServiceOrderBO As Assurant.ElitaPlus.BusinessObjectsNew.ServiceOrder
#End Region

    Public Sub Process(ByVal callingPage As System.Web.UI.Page, ByVal navCtrl As INavigationController) Implements IStateController.Process
        Try
            Me.NavController = navCtrl
            Me.CallingPage = CType(callingPage, ElitaPlusPage)
            Me.ServiceOrderBO = CType(Me.NavController.ParametersPassed, Assurant.ElitaPlus.BusinessObjectsNew.ServiceOrder)

            Dim claimBO As ClaimBase = ClaimFacade.Instance.GetClaim(Of ClaimBase)(Me.ServiceOrderBO.ClaimId)

            Me.NavController.FlowSession(FlowSessionKeys.SESSION_PREV_SERVICEORDER) = Me.NavController.FlowSession(FlowSessionKeys.SESSION_NEXT_SERVICEORDER)
            Me.NavController.FlowSession(FlowSessionKeys.SESSION_NEXT_SERVICEORDER) = ServiceOrderBO

            Dim oServiceCenter As ServiceCenter

            Dim isCustomerEmail As Boolean = False
            Dim isServiceCenterEmail As Boolean = False
            Dim isSalvageCenterEmail As Boolean = False
            Dim isPreview As Boolean = False
            Boolean.TryParse(CStr(Me.NavController.FlowSession(FlowSessionKeys.SESSION_IS_CUSTOMER_EMAIL)), isCustomerEmail)
            Boolean.TryParse(CStr(Me.NavController.FlowSession(FlowSessionKeys.SESSION_IS_SERVICE_CENTER_EMAIL)), isServiceCenterEmail)
            Boolean.TryParse(CStr(Me.NavController.FlowSession(FlowSessionKeys.SESSION_IS_SALVAGE_CENTER_EMAIL)), isSalvageCenterEmail)

            If (claimBO.ClaimAuthorizationType = ClaimAuthorizationType.Multiple) Then
                isPreview = True
            Else
                If claimBO.LoanerCenterId.Equals(Guid.Empty) Then
                    oServiceCenter = New ServiceCenter(claimBO.ServiceCenterId)
                Else
                    oServiceCenter = New ServiceCenter(claimBO.LoanerCenterId)
                End If

                If oServiceCenter.DefaultToEmailFlag Then
                Dim strMsg As String
                Dim strCcMsg As String

                If EnvironmentContext.Current.Environment <> Environments.Production Then
                    strMsg = TranslationBase.TranslateLabelOrMessage(Message.MSG_SERVICEORDER_SEND_EMAIL_CONFIRM) & " Test_" & oServiceCenter.Email
                    If Not oServiceCenter.CcEmail Is Nothing AndAlso oServiceCenter.CcEmail.Length > 0 Then
                        strCcMsg = TranslationBase.TranslateLabelOrMessage(Message.MSG_AND_A_COPY_TO) & " Test_" & oServiceCenter.CcEmail
                        strMsg = strMsg & " " & strCcMsg
                    End If
                    strMsg = strMsg & " ?"
                Else
                    strMsg = TranslationBase.TranslateLabelOrMessage(Message.MSG_SERVICEORDER_SEND_EMAIL_CONFIRM) & " " & oServiceCenter.Email & " "
                    If Not oServiceCenter.CcEmail Is Nothing AndAlso oServiceCenter.CcEmail.Length > 0 Then
                        strCcMsg = TranslationBase.TranslateLabelOrMessage(Message.MSG_AND_A_COPY_TO) & " " & oServiceCenter.CcEmail
                        strMsg = strMsg & " " & strCcMsg
                    End If
                    strMsg = strMsg & " ?"
                End If

                If (Me.NavController.CurrentFlow.Name = "CREATE_CLAIM_FROM_CERTIFICATE") Then
                    Me.NavController.Navigate(Me.CallingPage, FlowEvents.EVENT_EMAIL_PROMPT, New StateControllerYesNoPrompt.Parameters(strMsg, False, False, isServiceCenterEmail))
                Else
                    Me.NavController.Navigate(Me.CallingPage, FlowEvents.EVENT_EMAIL_PROMPT, New StateControllerYesNoPrompt.Parameters(strMsg, False))
                End If
            Else
                Dim oCert As New Certificate(claimBO.Certificate.Id)
                Dim strMsg As String
                Dim email As String

                email = If(EnvironmentContext.Current.Environment <> Environments.Production, "test_" + oCert.Email, oCert.Email)

                    If (Not oCert.Email Is Nothing) AndAlso (oCert.Email.Trim.Length > 0) AndAlso System.Text.RegularExpressions.Regex.IsMatch(email, ElitaPlus.Common.RegExConstants.EMAIL_REGEX) Then
                        strMsg = TranslationBase.TranslateLabelOrMessage(Message.MSG_SERVICEORDER_SEND_CUSTOMER_EMAIL_CONFIRM) & " " & email
                        If (Me.NavController.CurrentFlow.Name = "CREATE_CLAIM_FROM_CERTIFICATE") Then
                            Me.NavController.Navigate(Me.CallingPage, FlowEvents.EVENT_CUSTOMER_EMAIL_PROMPT, New StateControllerYesNoPrompt.Parameters(strMsg, False, False, isCustomerEmail))
                        Else
                            Me.NavController.Navigate(Me.CallingPage, FlowEvents.EVENT_CUSTOMER_EMAIL_PROMPT, New StateControllerYesNoPrompt.Parameters(strMsg, False))
                        End If
                    Else
                        'Me.NavController.Navigate(Me.CallingPage, FlowEvents.EVENT_PREVIEW_SERVICE_ORDER)
                        isPreview = True
                    End If
                End If
            End If

            If isSalvageCenterEmail Then
                Dim strMsg As String
                Dim strCcMsg As String
                Dim oSalvageCenter As ServiceCenter = New ServiceCenter(claimBO.Dealer.DefaultSalvgeCenterId)
                If Not oSalvageCenter.Email = Nothing Then
                    If EnvironmentContext.Current.Environment <> Environments.Production Then
                        strMsg = TranslationBase.TranslateLabelOrMessage(Message.MSG_SERVICEORDER_SEND_EMAIL_CONFIRM) & " Test_" & oSalvageCenter.Email
                        If Not oSalvageCenter.CcEmail Is Nothing AndAlso oSalvageCenter.CcEmail.Length > 0 Then
                            strCcMsg = TranslationBase.TranslateLabelOrMessage(Message.MSG_AND_A_COPY_TO) & " Test_" & oSalvageCenter.CcEmail
                            strMsg = strMsg & " " & strCcMsg
                        End If
                        strMsg = strMsg & " ?"
                    Else
                        strMsg = TranslationBase.TranslateLabelOrMessage(Message.MSG_SERVICEORDER_SEND_EMAIL_CONFIRM) & " " & oSalvageCenter.Email & " "
                        If Not oSalvageCenter.CcEmail Is Nothing AndAlso oSalvageCenter.CcEmail.Length > 0 Then
                            strCcMsg = TranslationBase.TranslateLabelOrMessage(Message.MSG_AND_A_COPY_TO) & " " & oSalvageCenter.CcEmail
                            strMsg = strMsg & " " & strCcMsg
                        End If
                        strMsg = strMsg & " ?"
                    End If

                    If (Me.NavController.CurrentFlow.Name = "CREATE_CLAIM_FROM_CERTIFICATE") Then
                        Me.NavController.Navigate(Me.CallingPage, FlowEvents.EVENT_SALVAGE_EMAIL_PROMPT, New StateControllerYesNoPrompt.Parameters(strMsg, False, False, isSalvageCenterEmail))
                    Else
                        Me.NavController.Navigate(Me.CallingPage, FlowEvents.EVENT_SALVAGE_EMAIL_PROMPT, New StateControllerYesNoPrompt.Parameters(strMsg, False))
                    End If
                End If
            End If

            If isPreview Then
                Me.NavController.Navigate(Me.CallingPage, FlowEvents.EVENT_PREVIEW_SERVICE_ORDER)
            End If
        Catch ex As Threading.ThreadAbortException
        End Try

    End Sub
End Class

Public Class ProcessServiceOrderCustomerEmail
    Implements IStateController

#Region "Private Atributes"
    Private NavController As INavigationController
    Private CallingPage As ElitaPlusPage
    Private ServiceOrderBO As Assurant.ElitaPlus.BusinessObjectsNew.ServiceOrder
#End Region

    Public Sub Process(ByVal callingPage As System.Web.UI.Page, ByVal navCtrl As INavigationController) Implements IStateController.Process
        Try
            Me.NavController = navCtrl
            Me.CallingPage = CType(callingPage, ElitaPlusPage)

            If Not NavController.FlowSession(FlowSessionKeys.SESSION_SERVICE_ORDER) Is Nothing Then
                Me.ServiceOrderBO = CType(NavController.FlowSession(FlowSessionKeys.SESSION_SERVICE_ORDER), ServiceOrder)
            ElseIf Not NavController.FlowSession(FlowSessionKeys.SESSION_OLD_SERVICE_ORDER) Is Nothing Then
                Me.ServiceOrderBO = CType(NavController.FlowSession(FlowSessionKeys.SESSION_OLD_SERVICE_ORDER), ServiceOrder)
            End If

            Dim claimBO As ClaimBase = ClaimFacade.Instance.GetClaim(Of ClaimBase)(Me.ServiceOrderBO.ClaimId)

            'Me.NavController.FlowSession(FlowSessionKeys.SESSION_PREV_SERVICEORDER) = Me.NavController.FlowSession(FlowSessionKeys.SESSION_NEXT_SERVICEORDER)
            'Me.NavController.FlowSession(FlowSessionKeys.SESSION_NEXT_SERVICEORDER) = ServiceOrderBO

            Dim oCert As New Certificate(claimBO.CertificateId)
            Dim strMsg As String
            Dim strCcMsg As String
            Dim email As String

            email = If(EnvironmentContext.Current.Environment <> Environments.Production, "test_" + oCert.Email, oCert.Email)

            If (Not oCert.Email Is Nothing) AndAlso (oCert.Email.Trim.Length > 0) AndAlso System.Text.RegularExpressions.Regex.IsMatch(email, ElitaPlus.Common.RegExConstants.EMAIL_REGEX) Then
                strMsg = TranslationBase.TranslateLabelOrMessage(Message.MSG_SERVICEORDER_SEND_CUSTOMER_EMAIL_CONFIRM) & " " & email
                Me.NavController.Navigate(Me.CallingPage, FlowEvents.EVENT_CUSTOMER_EMAIL_PROMPT, New StateControllerYesNoPrompt.Parameters(strMsg, False))
            Else
                If Me.NavController.FlowSession(FlowSessionKeys.SESSION_EMAIL_SENT) Is Nothing OrElse Me.NavController.FlowSession(FlowSessionKeys.SESSION_EMAIL_SENT).ToString = "N" Then
                    Me.NavController.Navigate(Me.CallingPage, FlowEvents.EVENT_PREVIEW_SERVICE_ORDER)
                Else
                    Me.NavController.Navigate(Me.CallingPage, "sent_email", Message.MSG_EMAIL_SENT)
                End If
            End If

        Catch ex As Threading.ThreadAbortException
        End Try

    End Sub
End Class



Public Class ProcessEmail
    Implements IStateController

#Region "Private Atributes"
    Private NavController As INavigationController
    Private CallingPage As ElitaPlusPage
    Private FILE_NAME_INVALID_CHARACTERS As String() = {"\", "/", ":", "*", "?", """", "<", ">", "|"} 'Operating system invalid characters in a file name.

#End Region

    Public Sub Process(ByVal callingPage As System.Web.UI.Page, ByVal navCtrl As INavigationController) Implements IStateController.Process
        Try
            Me.NavController = navCtrl
            Me.CallingPage = CType(callingPage, ElitaPlusPage)

            Dim ServiceOrderBO As Assurant.ElitaPlus.BusinessObjectsNew.ServiceOrder = _
                    CType(Me.NavController.FlowSession(FlowSessionKeys.SESSION_NEXT_SERVICEORDER), Assurant.ElitaPlus.BusinessObjectsNew.ServiceOrder)

            Dim ClaimBo As ClaimBase = ClaimFacade.Instance.GetClaim(Of ClaimBase)(ServiceOrderBO.ClaimId)
            ProcessEmail(ServiceOrderBO, ClaimBo)
            Me.NavController.FlowSession(FlowSessionKeys.SESSION_EMAIL_SENT) = "Y"

            Me.NavController.Navigate(Me.CallingPage, "sent_email", Message.MSG_EMAIL_SENT)
            '    Me.NavController.Navigate(Me.CallingPage, FlowEvents.EVENT_BACK)

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.NavController.Navigate(Me.CallingPage, "sent_email", Message.MSG_EMAIL_NOT_SENT)
        End Try

    End Sub

    Private Sub ProcessEmail(ByVal ServiceOrderBO As Assurant.ElitaPlus.BusinessObjectsNew.ServiceOrder, ByVal oClaim As ClaimBase)
        Dim oServiceCenter As ServiceCenter
        If oClaim.LoanerCenterId.Equals(Guid.Empty) Then
            oServiceCenter = New ServiceCenter(oClaim.ServiceCenterId)
        Else
            oServiceCenter = New ServiceCenter(oClaim.LoanerCenterId)
        End If

        Dim companyBO As Company = New Company(oClaim.CompanyId)

        Dim emailAddressTo As String = oServiceCenter.Email
        Dim emailAddressCc As String = oServiceCenter.CcEmail

        Dim emailAddressFrom As String = companyBO.Email
        If EnvironmentContext.Current.Environment <> Environments.Production Then
            emailAddressTo = "Test_" & emailAddressTo
            emailAddressCc = "Test_" & emailAddressCc
            emailAddressFrom = "Test_" & emailAddressFrom
        End If

        Dim subjectStr As String = TranslationBase.TranslateLabelOrMessage(Message.MSG_SERVICEORDER_SUBJECT, companyBO.LanguageId) & " : " & oClaim.ClaimNumber
        If Not oClaim.AuthorizationNumber Is Nothing AndAlso oClaim.AuthorizationNumber <> "" Then
            subjectStr = subjectStr & " / " & TranslationBase.TranslateLabelOrMessage(Message.MSG_SERVICEORDER_SUBJECT_AUTH, companyBO.LanguageId) & " : " & oClaim.AuthorizationNumber
        End If

        Dim pdfPath As String = AppConfig.ServiceOrderEmail.AttachDir
        Dim pdfTempFileName As String = pdfPath &
             ElitaPlusPrincipal.Current.Identity.Name & "_" & RemoveInvalidChar(Date.Now.ToString) _
               & oClaim.ClaimNumber & ".pdf"

        Dim msgStr As String = "Original Email Address = " & emailAddressTo & Environment.NewLine
        If EnvironmentContext.Current.Environment = Environments.Production Then

            '08/24/2006 - ALR - Added the check for the serviceOrderImage as nothing.  If not nothing
            '                   continues as usual.  If nothing, calls the serviceOrderController to reprint
            '                   based on the serviceOrderImageData
            If Not ServiceOrderBO.ServiceOrderImage Is Nothing Then
                SendEmail(oClaim.ClaimNumber, msgStr & companyBO.LegalDisclaimer, "BCC_" & companyBO.Email, companyBO.Email, subjectStr, ServiceOrderBO.ServiceOrderImage, True, pdfTempFileName)
                SendEmail(oClaim.ClaimNumber, ". " & companyBO.LegalDisclaimer, emailAddressTo, companyBO.Email, subjectStr, ServiceOrderBO.ServiceOrderImage, False, pdfTempFileName, emailAddressCc)
            Else


                Dim soController As New ServiceOrderController
                Dim strReportName As String = HttpContext.Current.Request.ApplicationPath + "/Reports/" + soController.GenerateReportName(ServiceOrderBO.ClaimId, ServiceOrderBO.ClaimAuthorizationId) + ".xslt"

                If Not System.IO.File.Exists(HttpContext.Current.Server.MapPath(strReportName)) Then
                    'To Do - Display error message on UI.
                Else

                    Dim TempFileName As String = pdfPath &
                                 ElitaPlusPrincipal.Current.Identity.Name & "_" & RemoveInvalidChar(Date.Now.ToString) _
                                   & oClaim.ClaimNumber & ".htm"

                    'Gets HTML Service Order based on Service order Data
                    Dim sb As String = ServiceOrderBO.GetReportHtmlData()

                    SendEmail(oClaim.ClaimNumber, msgStr & companyBO.LegalDisclaimer, "BCC_" & companyBO.Email, companyBO.Email, subjectStr, sb.ToString, TempFileName, emailAddressCc)
                    SendEmail(oClaim.ClaimNumber, ". " & companyBO.LegalDisclaimer, emailAddressTo, companyBO.Email, subjectStr, sb.ToString, TempFileName, emailAddressCc)

                End If

            End If
        ElseIf EnvironmentContext.Current.Environment = Environments.Test OrElse EnvironmentContext.Current.Environment = Environments.Model Then

            ' This section is for test only. The request is to have this capability only for test.
            ' Test Emial ID: mailinelita.so.test@assurant.com
            Dim testEmailID As String = "mailinelita.so.test@assurant.com"
            If Not ServiceOrderBO.ServiceOrderImage Is Nothing Then
                SendEmail(oClaim.ClaimNumber, msgStr & companyBO.LegalDisclaimer, testEmailID, testEmailID, subjectStr, ServiceOrderBO.ServiceOrderImage, True, pdfTempFileName)
                SendEmail(oClaim.ClaimNumber, ". " & companyBO.LegalDisclaimer, testEmailID, testEmailID, subjectStr, ServiceOrderBO.ServiceOrderImage, False, pdfTempFileName, testEmailID)
            Else


                Dim soController As New ServiceOrderController
                Dim strReportName As String = HttpContext.Current.Request.ApplicationPath + "/Reports/" + soController.GenerateReportName(ServiceOrderBO.ClaimId, ServiceOrderBO.ClaimAuthorizationId) + ".xslt"

                If Not System.IO.File.Exists(HttpContext.Current.Server.MapPath(strReportName)) Then
                    'To Do - Display error message on UI.
                Else

                    Dim TempFileName As String = pdfPath &
                                 ElitaPlusPrincipal.Current.Identity.Name & "_" & RemoveInvalidChar(Date.Now.ToString) _
                                   & oClaim.ClaimNumber & ".htm"

                    'Gets HTML Service Order based on Service order Data
                    Dim sb As String = ServiceOrderBO.GetReportHtmlData()

                    SendEmail(oClaim.ClaimNumber, msgStr & companyBO.LegalDisclaimer, testEmailID, testEmailID, subjectStr, sb.ToString, TempFileName, testEmailID)
                    SendEmail(oClaim.ClaimNumber, ". " & companyBO.LegalDisclaimer, testEmailID, testEmailID, subjectStr, sb.ToString, TempFileName, testEmailID)

                End If

            End If
        End If
    End Sub

#Region "Email Related"
    Private Function RemoveInvalidChar(ByVal filename As String) As String
        Dim index As Integer
        For index = 0 To FILE_NAME_INVALID_CHARACTERS.Length - 1
            'replace the invalid character with blank
            filename = filename.Replace(FILE_NAME_INVALID_CHARACTERS(index), " ")
        Next
        Return filename
    End Function

    Private Sub SendEmail(ByVal claimNumber As String, _
                              ByVal strMessage As String, _
                              ByVal strToAddress As String, _
                              ByVal strFromAddress As String, _
                              ByVal strSubject As String, _
                              ByVal objPDF As Byte(), _
                              ByVal isFirstMail As Boolean, _
                              ByVal pdfTempFileName As String, _
                              Optional ByVal strCcAddress As String = "")
        Try
            '  Dim pdfPath As String = System.AppDomain.CurrentDomain.BaseDirectory & "Temporary Files/"
            'Dim pdfPath As String = AppConfig.ServiceOrderEmail.AttachDir
            'Dim pdfTempFileName As String = pdfPath & _
            '     ElitaPlusPrincipal.Current.Identity.Name & "_" & RemoveInvalidChar(Date.Now.ToString) _
            '       & claimNumber & ".pdf"
            If isFirstMail Then
                Dim fs As FileStream
                fs = New FileStream(pdfTempFileName, FileMode.Create)
                fs.Write(objPDF, 0, objPDF.Length)
                fs.Flush()
                fs.Close()
            End If

            Try
                'Dim mailObj As New MailMessage
                '   System.Threading.Thread.CurrentThread.Sleep(5000)
                Dim maAttach As MailAttachment
                maAttach = New MailAttachment(pdfTempFileName)

                Dim m As Web.Mail.MailMessage = New Web.Mail.MailMessage
                With m
                    '.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", 2)
                    .From = strFromAddress
                    .To = strToAddress
                    .Cc = strCcAddress
                    .Body = strMessage
                    .Subject = strSubject
                    .BodyFormat = MailFormat.Text
                    .Attachments.Add(maAttach)
                End With

                SmtpMail.SmtpServer = AppConfig.ServiceOrderEmail.SmtpServer

                '   System.Threading.Thread.CurrentThread.Sleep(5000)
                ' Simualte an problem with sending the e-mail JLR
                ' Throw New Exception("Email Failed")
                SmtpMail.Send(m)

                '   System.Threading.Thread.CurrentThread.Sleep(5000)
            Catch ex As Exception
                Throw ex
            Finally
                '   If File.Exists(pdfTempFileName) Then
                'File.Delete(pdfTempFileName)
                ' End If
            End Try
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    '09/08/2006 - This alternate sendEmail method is used to create and send the HTML attachment 
    '             instead of the PDF from above
    Private Sub SendEmail(ByVal claimNumber As String, _
                              ByVal strMessage As String, _
                              ByVal strToAddress As String, _
                              ByVal strFromAddress As String, _
                              ByVal strSubject As String, _
                              ByVal sServiceOrder As String, _
                              ByVal TempFileName As String, _
                              Optional ByVal strCcAddress As String = "")


        Dim fs As FileStream
        Dim sw As StreamWriter

        fs = New FileStream(TempFileName, FileMode.Create, FileAccess.Write)
        sw = New StreamWriter(fs)
        sw.Write(sServiceOrder)
        sw.Flush()
        sw.Close()
        fs.Close()

        Try

            Dim maAttach As MailAttachment
            If Not sServiceOrder Is Nothing Then
                maAttach = New MailAttachment(TempFileName)
            End If

            Dim m As Web.Mail.MailMessage = New Web.Mail.MailMessage
            m.BodyEncoding = System.Text.Encoding.UTF8
            With m
                .From = strFromAddress
                .To = strToAddress
                .Cc = strCcAddress
                .Body = strMessage
                .Subject = strSubject
                .BodyFormat = MailFormat.Text
                If Not sServiceOrder Is Nothing Then .Attachments.Add(maAttach)
            End With

            SmtpMail.SmtpServer = AppConfig.ServiceOrderEmail.SmtpServer
            ' Simualte an problem with sending the e-mail JLR
            ' Throw New Exception("Email Failed")
            SmtpMail.Send(m)
        Catch ex As Exception
            Throw ex
        Finally
            'If File.Exists(TempFileName) Then
            '   File.Delete(TempFileName)
            ' End If
        End Try
    End Sub


#End Region



End Class


Public Class ProcessCustomerEmail
    Implements IStateController

#Region "Private Atributes"
    Private NavController As INavigationController
    Private CallingPage As ElitaPlusPage
    Private FILE_NAME_INVALID_CHARACTERS As String() = {"\", "/", ":", "*", "?", """", "<", ">", "|"} 'Operating system invalid characters in a file name.

#End Region

    Public Sub Process(ByVal callingPage As System.Web.UI.Page, ByVal navCtrl As INavigationController) Implements IStateController.Process
        Try
            Me.NavController = navCtrl
            Me.CallingPage = CType(callingPage, ElitaPlusPage)

            Dim ServiceOrderBO As Assurant.ElitaPlus.BusinessObjectsNew.ServiceOrder = _
                    CType(Me.NavController.FlowSession(FlowSessionKeys.SESSION_NEXT_SERVICEORDER), Assurant.ElitaPlus.BusinessObjectsNew.ServiceOrder)

            Dim ClaimBo As ClaimBase = ClaimFacade.Instance.GetClaim(Of ClaimBase)(ServiceOrderBO.ClaimId)
            ProcessEmail(ServiceOrderBO, ClaimBo)
            Me.NavController.Navigate(Me.CallingPage, "sent_email", Message.MSG_EMAIL_SENT)
            '    Me.NavController.Navigate(Me.CallingPage, FlowEvents.EVENT_BACK)

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.NavController.Navigate(Me.CallingPage, "sent_email", Message.MSG_EMAIL_NOT_SENT)
        End Try

    End Sub

    Private Sub ProcessEmail(ByVal ServiceOrderBO As Assurant.ElitaPlus.BusinessObjectsNew.ServiceOrder, ByVal oClaim As ClaimBase)
        Dim oServiceCenter As ServiceCenter
        Dim oCert As Certificate

        If oClaim.LoanerCenterId.Equals(Guid.Empty) Then
            oServiceCenter = New ServiceCenter(oClaim.ServiceCenterId)
        Else
            oServiceCenter = New ServiceCenter(oClaim.LoanerCenterId)
        End If

        Dim companyBO As Company = New Company(oClaim.CompanyId)
        oCert = New Certificate(oClaim.CertificateId)

        Dim emailAddressTo As String = oCert.Email

        Dim emailAddressFrom As String = companyBO.Email
        If EnvironmentContext.Current.Environment <> Environments.Production Then
            emailAddressTo = "Test_" & emailAddressTo
            emailAddressFrom = "Test_" & emailAddressFrom
        End If

        Dim emailBody As String = ServiceOrder.GetSericeOrderEmailContent(companyBO.Id)

        Dim subjectStr As String = TranslationBase.TranslateLabelOrMessage(Message.MSG_SERVICEORDER_SUBJECT, companyBO.LanguageId) & " : " & oClaim.ClaimNumber
        If Not oClaim.AuthorizationNumber Is Nothing AndAlso oClaim.AuthorizationNumber <> "" Then
            subjectStr = subjectStr & " / " & TranslationBase.TranslateLabelOrMessage(Message.MSG_SERVICEORDER_SUBJECT_AUTH, companyBO.LanguageId) & " : " & oClaim.AuthorizationNumber
        End If

        Dim pdfPath As String = AppConfig.ServiceOrderEmail.AttachDir
        Dim pdfTempFileName As String = pdfPath &
             ElitaPlusPrincipal.Current.Identity.Name & "_" & RemoveInvalidChar(Date.Now.ToString) _
               & oClaim.ClaimNumber & ".pdf"

        Dim msgStr As String = "Original Email Address = " & emailAddressTo & Environment.NewLine
        If EnvironmentContext.Current.Environment = Environments.Production Then

            '08/24/2006 - ALR - Added the check for the serviceOrderImage as nothing.  If not nothing
            '                   continues as usual.  If nothing, calls the serviceOrderController to reprint
            '                   based on the serviceOrderImageData
            If Not ServiceOrderBO.ServiceOrderImage Is Nothing Then
                SendEmail(oClaim.ClaimNumber, ". " & companyBO.LegalDisclaimer, emailAddressTo, companyBO.Email, subjectStr, ServiceOrderBO.ServiceOrderImage, False, pdfTempFileName, "", emailBody)
            Else


                Dim soController As New ServiceOrderController
                Dim strReportName As String = HttpContext.Current.Request.ApplicationPath + "/Reports/" + soController.GenerateReportName(ServiceOrderBO.ClaimId, ServiceOrderBO.ClaimAuthorizationId) + ".xslt"

                If Not System.IO.File.Exists(HttpContext.Current.Server.MapPath(strReportName)) Then
                    'To Do - Display error message on UI.
                Else

                    Dim TempFileName As String = pdfPath &
                                 ElitaPlusPrincipal.Current.Identity.Name & "_" & RemoveInvalidChar(Date.Now.ToString) _
                                   & oClaim.ClaimNumber & ".htm"

                    'Gets HTML Service Order based on Service order Data
                    Dim sb As String = ServiceOrderBO.GetReportHtmlData()

                    SendEmail(oClaim.ClaimNumber, ". " & companyBO.LegalDisclaimer, emailAddressTo, companyBO.Email, subjectStr, sb.ToString, TempFileName, "", emailBody)

                End If

            End If
        ElseIf EnvironmentContext.Current.Environment = Environments.Test Then

            ' This section is for test only. The request is to have this capability only for test.
            ' Test Emial ID: mailinelita.so.test@assurant.com
            Dim testEmailID As String = "mailinelita.so.test@assurant.com"
            If Not ServiceOrderBO.ServiceOrderImage Is Nothing Then
                SendEmail(oClaim.ClaimNumber, ". " & companyBO.LegalDisclaimer, testEmailID, testEmailID, subjectStr, ServiceOrderBO.ServiceOrderImage, False, pdfTempFileName, testEmailID, emailBody)
            Else


                Dim soController As New ServiceOrderController
                Dim strReportName As String = HttpContext.Current.Request.ApplicationPath + "/Reports/" + soController.GenerateReportName(ServiceOrderBO.ClaimId, ServiceOrderBO.ClaimAuthorizationId) + ".xslt"

                If Not System.IO.File.Exists(HttpContext.Current.Server.MapPath(strReportName)) Then
                    'To Do - Display error message on UI.
                Else

                    Dim TempFileName As String = pdfPath &
                                 ElitaPlusPrincipal.Current.Identity.Name & "_" & RemoveInvalidChar(Date.Now.ToString) _
                                   & oClaim.ClaimNumber & ".htm"

                    'Gets HTML Service Order based on Service order Data
                    Dim sb As String = ServiceOrderBO.GetReportHtmlData()

                    SendEmail(oClaim.ClaimNumber, ". " & companyBO.LegalDisclaimer, testEmailID, testEmailID, subjectStr, sb.ToString, TempFileName, testEmailID, emailBody)

                End If

            End If
        End If
    End Sub

#Region "Email Related"
    Private Function RemoveInvalidChar(ByVal filename As String) As String
        Dim index As Integer
        For index = 0 To FILE_NAME_INVALID_CHARACTERS.Length - 1
            'replace the invalid character with blank
            filename = filename.Replace(FILE_NAME_INVALID_CHARACTERS(index), " ")
        Next
        Return filename
    End Function

    Private Sub SendEmail(ByVal claimNumber As String,
                              ByVal strMessage As String,
                              ByVal strToAddress As String,
                              ByVal strFromAddress As String,
                              ByVal strSubject As String,
                              ByVal objPDF As Byte(),
                              ByVal isFirstMail As Boolean,
                              ByVal pdfTempFileName As String,
                              Optional ByVal strCcAddress As String = "",
                              Optional ByVal strEmailBody As String = ""
                          )
        Try
            '  Dim pdfPath As String = System.AppDomain.CurrentDomain.BaseDirectory & "Temporary Files/"
            'Dim pdfPath As String = AppConfig.ServiceOrderEmail.AttachDir
            'Dim pdfTempFileName As String = pdfPath & _
            '     ElitaPlusPrincipal.Current.Identity.Name & "_" & RemoveInvalidChar(Date.Now.ToString) _
            '       & claimNumber & ".pdf"
            If isFirstMail Then
                Dim fs As FileStream
                fs = New FileStream(pdfTempFileName, FileMode.Create)
                fs.Write(objPDF, 0, objPDF.Length)
                fs.Flush()
                fs.Close()
            End If

            Try
                'Dim mailObj As New MailMessage
                '   System.Threading.Thread.CurrentThread.Sleep(5000)
                Dim maAttach As MailAttachment
                maAttach = New MailAttachment(pdfTempFileName)

                Dim m As Web.Mail.MailMessage = New Web.Mail.MailMessage
                With m
                    '.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", 2)
                    .From = strFromAddress
                    .To = strToAddress
                    .Cc = strCcAddress
                    .Body = strMessage
                    .Subject = strSubject
                    .BodyFormat = MailFormat.Text
                    .Body = strEmailBody
                    .BodyEncoding = System.Text.Encoding.UTF8
                    .Attachments.Add(maAttach)
                End With

                SmtpMail.SmtpServer = AppConfig.ServiceOrderEmail.SmtpServer

                '   System.Threading.Thread.CurrentThread.Sleep(5000)
                ' Simualte an problem with sending the e-mail JLR
                ' Throw New Exception("Email Failed")
                SmtpMail.Send(m)

                '   System.Threading.Thread.CurrentThread.Sleep(5000)
            Catch ex As Exception
                Throw ex
            Finally
                '   If File.Exists(pdfTempFileName) Then
                'File.Delete(pdfTempFileName)
                ' End If
            End Try
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    '09/08/2006 - This alternate sendEmail method is used to create and send the HTML attachment 
    '             instead of the PDF from above
    Private Sub SendEmail(ByVal claimNumber As String,
                              ByVal strMessage As String,
                              ByVal strToAddress As String,
                              ByVal strFromAddress As String,
                              ByVal strSubject As String,
                              ByVal sServiceOrder As String,
                              ByVal TempFileName As String,
                              Optional ByVal strCcAddress As String = "",
                              Optional ByVal strEmailBody As String = "")


        Dim fs As FileStream
        Dim sw As StreamWriter

        fs = New FileStream(TempFileName, FileMode.Create, FileAccess.Write)
        sw = New StreamWriter(fs)
        sw.Write(sServiceOrder)
        sw.Flush()
        sw.Close()
        fs.Close()

        Try

            Dim maAttach As MailAttachment
            If Not sServiceOrder Is Nothing Then
                maAttach = New MailAttachment(TempFileName)
            End If

            Dim m As Web.Mail.MailMessage = New Web.Mail.MailMessage
            m.BodyEncoding = System.Text.Encoding.UTF8
            With m
                .From = strFromAddress
                .To = strToAddress
                .Cc = strCcAddress
                .Body = strMessage
                .Subject = strSubject
                .BodyFormat = MailFormat.Text
                .Body = strEmailBody
                .BodyEncoding = System.Text.Encoding.UTF8
                If Not sServiceOrder Is Nothing Then .Attachments.Add(maAttach)
            End With

            SmtpMail.SmtpServer = AppConfig.ServiceOrderEmail.SmtpServer
            ' Simualte an problem with sending the e-mail JLR
            ' Throw New Exception("Email Failed")
            SmtpMail.Send(m)
        Catch ex As Exception
            Throw ex
        Finally
            'If File.Exists(TempFileName) Then
            '   File.Delete(TempFileName)
            ' End If
        End Try
    End Sub


#End Region



End Class


Public Class ProcessSalvageCenterEmail
    Implements IStateController

#Region "Private Atributes"
    Private NavController As INavigationController
    Private CallingPage As ElitaPlusPage
    Private FILE_NAME_INVALID_CHARACTERS As String() = {"\", "/", ":", "*", "?", """", "<", ">", "|"} 'Operating system invalid characters in a file name.

#End Region

    Public Sub Process(ByVal callingPage As System.Web.UI.Page, ByVal navCtrl As INavigationController) Implements IStateController.Process
        Try
            Me.NavController = navCtrl
            Me.CallingPage = CType(callingPage, ElitaPlusPage)

            Dim ServiceOrderBO As Assurant.ElitaPlus.BusinessObjectsNew.ServiceOrder = _
                    CType(Me.NavController.FlowSession(FlowSessionKeys.SESSION_NEXT_SERVICEORDER), Assurant.ElitaPlus.BusinessObjectsNew.ServiceOrder)

            Dim ClaimBo As ClaimBase = ClaimFacade.Instance.GetClaim(Of ClaimBase)(ServiceOrderBO.ClaimId)
            ProcessSalvageCenterEmail(ServiceOrderBO, ClaimBo)
            Me.NavController.FlowSession(FlowSessionKeys.SESSION_EMAIL_SENT) = "Y"

            Me.NavController.Navigate(Me.CallingPage, "sent_email", Message.MSG_EMAIL_SENT)
            '    Me.NavController.Navigate(Me.CallingPage, FlowEvents.EVENT_BACK)

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.NavController.Navigate(Me.CallingPage, "sent_email", Message.MSG_EMAIL_NOT_SENT)
        End Try

    End Sub

    Private Sub ProcessSalvageCenterEmail(ByVal ServiceOrderBO As Assurant.ElitaPlus.BusinessObjectsNew.ServiceOrder, ByVal oClaim As ClaimBase)
        Dim oServiceCenter As ServiceCenter
        oServiceCenter = New ServiceCenter(oClaim.Dealer.DefaultSalvgeCenterId)
        Dim companyBO As Company = New Company(oClaim.CompanyId)

        Dim emailAddressTo As String = oServiceCenter.Email
        Dim emailAddressCc As String = oServiceCenter.CcEmail

        Dim emailAddressFrom As String = companyBO.Email
        If EnvironmentContext.Current.Environment <> Environments.Production Then
            emailAddressTo = "Test_" & emailAddressTo
            emailAddressCc = "Test_" & emailAddressCc
            emailAddressFrom = "Test_" & emailAddressFrom
        End If

        Dim subjectStr As String = TranslationBase.TranslateLabelOrMessage(Message.MSG_SERVICEORDER_SUBJECT, companyBO.LanguageId) & " : " & oClaim.ClaimNumber
        If Not oClaim.AuthorizationNumber Is Nothing AndAlso oClaim.AuthorizationNumber <> "" Then
            subjectStr = subjectStr & " / " & TranslationBase.TranslateLabelOrMessage(Message.MSG_SERVICEORDER_SUBJECT_AUTH, companyBO.LanguageId) & " : " & oClaim.AuthorizationNumber
        End If

        Dim pdfPath As String = AppConfig.ServiceOrderEmail.AttachDir
        Dim pdfTempFileName As String = pdfPath &
             ElitaPlusPrincipal.Current.Identity.Name & "_" & RemoveInvalidChar(Date.Now.ToString) _
               & oClaim.ClaimNumber & ".pdf"

        Dim msgStr As String = "Original Email Address = " & emailAddressTo & Environment.NewLine
        If EnvironmentContext.Current.Environment = Environments.Production Then

            '08/24/2006 - ALR - Added the check for the serviceOrderImage as nothing.  If not nothing
            '                   continues as usual.  If nothing, calls the serviceOrderController to reprint
            '                   based on the serviceOrderImageData
            If Not ServiceOrderBO.ServiceOrderImage Is Nothing Then
                SendEmail(oClaim.ClaimNumber, msgStr & companyBO.LegalDisclaimer, "BCC_" & companyBO.Email, companyBO.Email, subjectStr, ServiceOrderBO.ServiceOrderImage, True, pdfTempFileName)
                SendEmail(oClaim.ClaimNumber, ". " & companyBO.LegalDisclaimer, emailAddressTo, companyBO.Email, subjectStr, ServiceOrderBO.ServiceOrderImage, False, pdfTempFileName, emailAddressCc)
            Else


                Dim soController As New ServiceOrderController
                Dim strReportName As String = HttpContext.Current.Request.ApplicationPath + "/Reports/" + soController.GenerateReportName(ServiceOrderBO.ClaimId, ServiceOrderBO.ClaimAuthorizationId) + ".xslt"

                If Not System.IO.File.Exists(HttpContext.Current.Server.MapPath(strReportName)) Then
                    'To Do - Display error message on UI.
                Else

                    Dim TempFileName As String = pdfPath &
                                 ElitaPlusPrincipal.Current.Identity.Name & "_" & RemoveInvalidChar(Date.Now.ToString) _
                                   & oClaim.ClaimNumber & ".htm"

                    'Gets HTML Service Order based on Service order Data
                    Dim sb As String = ServiceOrderBO.GetReportHtmlData()

                    SendEmail(oClaim.ClaimNumber, msgStr & companyBO.LegalDisclaimer, "BCC_" & companyBO.Email, companyBO.Email, subjectStr, sb.ToString, TempFileName, emailAddressCc)
                    SendEmail(oClaim.ClaimNumber, ". " & companyBO.LegalDisclaimer, emailAddressTo, companyBO.Email, subjectStr, sb.ToString, TempFileName, emailAddressCc)

                End If

            End If
        ElseIf EnvironmentContext.Current.Environment = Environments.Test OrElse EnvironmentContext.Current.Environment = Environments.Model Then

            ' This section is for test only. The request is to have this capability only for test.
            ' Test Emial ID: mailinelita.so.test@assurant.com
            Dim testEmailID As String = "mailinelita.so.test@assurant.com"
            If Not ServiceOrderBO.ServiceOrderImage Is Nothing Then
                SendEmail(oClaim.ClaimNumber, msgStr & companyBO.LegalDisclaimer, testEmailID, testEmailID, subjectStr, ServiceOrderBO.ServiceOrderImage, True, pdfTempFileName)
                SendEmail(oClaim.ClaimNumber, ". " & companyBO.LegalDisclaimer, testEmailID, testEmailID, subjectStr, ServiceOrderBO.ServiceOrderImage, False, pdfTempFileName, testEmailID)
            Else


                Dim soController As New ServiceOrderController
                Dim strReportName As String = HttpContext.Current.Request.ApplicationPath + "/Reports/" + soController.GenerateReportName(ServiceOrderBO.ClaimId, ServiceOrderBO.ClaimAuthorizationId) + ".xslt"

                If Not System.IO.File.Exists(HttpContext.Current.Server.MapPath(strReportName)) Then
                    'To Do - Display error message on UI.
                Else

                    Dim TempFileName As String = pdfPath &
                                 ElitaPlusPrincipal.Current.Identity.Name & "_" & RemoveInvalidChar(Date.Now.ToString) _
                                   & oClaim.ClaimNumber & ".htm"

                    'Gets HTML Service Order based on Service order Data
                    Dim sb As String = ServiceOrderBO.GetReportHtmlData()

                    SendEmail(oClaim.ClaimNumber, msgStr & companyBO.LegalDisclaimer, testEmailID, testEmailID, subjectStr, sb.ToString, TempFileName, testEmailID)
                    SendEmail(oClaim.ClaimNumber, ". " & companyBO.LegalDisclaimer, testEmailID, testEmailID, subjectStr, sb.ToString, TempFileName, testEmailID)

                End If

            End If
        End If
    End Sub

#Region "Email Related"
    Private Function RemoveInvalidChar(ByVal filename As String) As String
        Dim index As Integer
        For index = 0 To FILE_NAME_INVALID_CHARACTERS.Length - 1
            'replace the invalid character with blank
            filename = filename.Replace(FILE_NAME_INVALID_CHARACTERS(index), " ")
        Next
        Return filename
    End Function

    Private Sub SendEmail(ByVal claimNumber As String, _
                              ByVal strMessage As String, _
                              ByVal strToAddress As String, _
                              ByVal strFromAddress As String, _
                              ByVal strSubject As String, _
                              ByVal objPDF As Byte(), _
                              ByVal isFirstMail As Boolean, _
                              ByVal pdfTempFileName As String, _
                              Optional ByVal strCcAddress As String = "")
        Try
            '  Dim pdfPath As String = System.AppDomain.CurrentDomain.BaseDirectory & "Temporary Files/"
            'Dim pdfPath As String = AppConfig.ServiceOrderEmail.AttachDir
            'Dim pdfTempFileName As String = pdfPath & _
            '     ElitaPlusPrincipal.Current.Identity.Name & "_" & RemoveInvalidChar(Date.Now.ToString) _
            '       & claimNumber & ".pdf"
            If isFirstMail Then
                Dim fs As FileStream
                fs = New FileStream(pdfTempFileName, FileMode.Create)
                fs.Write(objPDF, 0, objPDF.Length)
                fs.Flush()
                fs.Close()
            End If

            Try
                'Dim mailObj As New MailMessage
                '   System.Threading.Thread.CurrentThread.Sleep(5000)
                Dim maAttach As MailAttachment
                maAttach = New MailAttachment(pdfTempFileName)

                Dim m As Web.Mail.MailMessage = New Web.Mail.MailMessage
                With m
                    '.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", 2)
                    .From = strFromAddress
                    .To = strToAddress
                    .Cc = strCcAddress
                    .Body = strMessage
                    .Subject = strSubject
                    .BodyFormat = MailFormat.Text
                    .Attachments.Add(maAttach)
                End With

                SmtpMail.SmtpServer = AppConfig.ServiceOrderEmail.SmtpServer

                '   System.Threading.Thread.CurrentThread.Sleep(5000)
                ' Simualte an problem with sending the e-mail JLR
                ' Throw New Exception("Email Failed")
                SmtpMail.Send(m)

                '   System.Threading.Thread.CurrentThread.Sleep(5000)
            Catch ex As Exception
                Throw ex
            Finally
                '   If File.Exists(pdfTempFileName) Then
                'File.Delete(pdfTempFileName)
                ' End If
            End Try
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    '09/08/2006 - This alternate sendEmail method is used to create and send the HTML attachment 
    '             instead of the PDF from above
    Private Sub SendEmail(ByVal claimNumber As String, _
                              ByVal strMessage As String, _
                              ByVal strToAddress As String, _
                              ByVal strFromAddress As String, _
                              ByVal strSubject As String, _
                              ByVal sServiceOrder As String, _
                              ByVal TempFileName As String, _
                              Optional ByVal strCcAddress As String = "")


        Dim fs As FileStream
        Dim sw As StreamWriter

        fs = New FileStream(TempFileName, FileMode.Create, FileAccess.Write)
        sw = New StreamWriter(fs)
        sw.Write(sServiceOrder)
        sw.Flush()
        sw.Close()
        fs.Close()

        Try

            Dim maAttach As MailAttachment
            If Not sServiceOrder Is Nothing Then
                maAttach = New MailAttachment(TempFileName)
            End If

            Dim m As Web.Mail.MailMessage = New Web.Mail.MailMessage
            m.BodyEncoding = System.Text.Encoding.UTF8
            With m
                .From = strFromAddress
                .To = strToAddress
                .Cc = strCcAddress
                .Body = strMessage
                .Subject = strSubject
                .BodyFormat = MailFormat.Text
                If Not sServiceOrder Is Nothing Then .Attachments.Add(maAttach)
            End With

            SmtpMail.SmtpServer = AppConfig.ServiceOrderEmail.SmtpServer
            ' Simualte an problem with sending the e-mail JLR
            ' Throw New Exception("Email Failed")
            SmtpMail.Send(m)
        Catch ex As Exception
            Throw ex
        Finally
            'If File.Exists(TempFileName) Then
            '   File.Delete(TempFileName)
            ' End If
        End Try
    End Sub


#End Region



End Class
