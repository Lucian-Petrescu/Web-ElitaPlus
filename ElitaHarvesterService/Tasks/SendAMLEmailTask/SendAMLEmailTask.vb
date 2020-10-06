Imports System.IO
Imports System.Net.Mail
Imports System.Xml
Imports System.Xml.Xsl
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports Assurant.ElitaPlus.Common
Imports Assurant.ElitaPlus.DALObjects


Public Class SendAMLEmailTask
    Inherits TaskBase

    Dim oClaim As ClaimBase

    Public Sub New(machineName As String, processThreadName As String)
        MyBase.New(machineName, processThreadName)
    End Sub

    Protected Friend Overrides Sub Execute()

        Dim claimId As Guid
        Dim emailAddresses As String()

        '' Step #1 - Get Claim Information based on Arguments
        If (Not String.IsNullOrEmpty(MyBase.PublishedTask(PublishedTask.CLAIM_ID))) Then
            claimId = GuidControl.ByteArrayToGuid(GuidControl.HexToByteArray(MyBase.PublishedTask(PublishedTask.CLAIM_ID)))
            If (Not claimId.Equals(Guid.Empty)) Then
                oClaim = ClaimFacade.Instance.GetClaim(Of ClaimBase)(claimId)


                '' Step #2 - Get Email Addresses for Compliance Officers based on LAN IDs
                emailAddresses = ElitaPlusIdentity.GetEmailAddresses(User.GetUsers(oClaim.Company.CountryId, pPermissionCode:=Codes.PREMISSION_COMPLIANCE_OFFICER))


                '' Step #3 - Send Email
                SendEmail(pToEmail:=emailAddresses,
                          pFromEmailAddress:=MyBase.PublishedTask.Task.TaskParameter(PublishedTask.FROM_EMAIL_ADDRESS),
                          pSmtpServerAddress:=MyBase.PublishedTask.Task.TaskParameter(PublishedTask.SMTP_SERVER_ADDRESS))

            End If
        End If


    End Sub

    Private Sub SendEmail(pToEmail As String(),
                          pFromEmailAddress As String,
                          pSmtpServerAddress As String)
        Dim mail As MailMessage = New System.Net.Mail.MailMessage()
        Dim ser As SmtpClient = New System.Net.Mail.SmtpClient(pSmtpServerAddress)
        Dim oAddress As New Address(oClaim.Certificate.AddressId)
        Dim strXMLName As String = String.Empty
        Dim strXSLTName As String = String.Empty
        Dim strXML As New StringWriter
        Dim strXML1 As String = String.Empty
        Dim dv As New DataView

        For Each toAddress As String In pToEmail
            If (Not String.IsNullOrWhiteSpace(toAddress)) Then
                mail.To.Add(New MailAddress(toAddress))
            End If
        Next

        If (mail.To.Count = 0) Then
            Throw New ApplicationException("None of the users have EMail Addresses in LDAP")
        End If

        mail.Subject = String.Format("[SECURE]{0} AML - Action Required", IIf(EnvironmentContext.Current.Environment = Environments.Production, String.Empty, EnvironmentContext.Current.EnvironmentShortName))
            mail.From = New MailAddress(pFromEmailAddress)

        dv = oClaim.IsCustomerPresentInUFIList(oClaim.Company.CountryId, oClaim.Certificate.IdentificationNumber)

        Dim emailBody As New EmailBodyInfo

        emailBody.Nationality = LookupListNew.GetDescriptionFromId(LookupListNew.LK_NATIONALITY, oClaim.Certificate.Nationality)
        emailBody.Gender = LookupListNew.GetDescriptionFromId(LookupListNew.LK_GENDER, oClaim.Certificate.Gender)
        If Not oAddress Is Nothing Then
            emailBody.Address1 = oAddress.Address1
            emailBody.Address2 = oAddress.Address2
            emailBody.Address3 = oAddress.Address3
            emailBody.City = oAddress.City
            emailBody.Postal_code = oAddress.PostalCode
        End If
        emailBody.Claim_Number = oClaim.ClaimNumber
        emailBody.Authorized_Amount = CDec(oClaim.AuthorizedAmount)
        emailBody.Deductible = CDec(oClaim.Deductible)
        emailBody.Cert_Number = oClaim.Certificate.CertNumber
        emailBody.Dealer_Name = oClaim.Dealer.Dealer
        If Not dv Is Nothing Then
            emailBody.TaxId = Convert.ToString(dv.Table.Rows(0)("tax_id"))
            emailBody.CustomerFirstName = Convert.ToString(dv.Table.Rows(0)("first_name"))
            emailBody.CustomerLastName = Convert.ToString(dv.Table.Rows(0)("last_name"))
            emailBody.UFI_Begin_dt = Convert.ToDateTime(dv.Table.Rows(0)("begin_date"))
            emailBody.UFI_End_dt = Convert.ToDateTime(dv.Table.Rows(0)("end_date"))
        End If

        Dim eb As New System.Xml.Serialization.XmlSerializer(emailBody.GetType)
        eb.Serialize(strXML, emailBody)
        strXML1 = strXML.ToString()



        Dim xdoc As New System.Xml.XmlDocument
        xdoc.LoadXml(strXML1)

        Dim output As String

        Using oStream As Stream = GetType(SendAMLEmailTask).Assembly.GetManifestResourceStream("ElitaHarvesterService.EmailInfo.xslt")
            Using xsltReader As XmlReader = XmlReader.Create(oStream)
                Dim process As XslCompiledTransform = New XslCompiledTransform()
                process.Load(xsltReader)

                Using sr As StringReader = New StringReader(strXML1)
                    Using xr As XmlReader = XmlReader.Create(sr)
                        Using sw As StringWriter = New StringWriter()
                            process.Transform(xr, Nothing, sw)
                            output = sw.ToString()
                        End Using
                    End Using
                End Using
            End Using
        End Using

        mail.IsBodyHtml = True
        mail.Body = output
        ser.Send(mail)
    End Sub


End Class
Public Class EmailBodyInfo
    Public TaxId As String
    Public CustomerFirstName As String
    Public CustomerLastName As String
    Public Nationality As String
    Public Gender As String
    Public Address1 As String
    Public Address2 As String
    Public Address3 As String
    Public City As String
    Public Postal_code As String
    Public Claim_Number As String
    Public Authorized_Amount As Decimal
    Public Deductible As Decimal
    Public Cert_Number As String
    Public Dealer_Name As String
    Public UFI_Begin_dt As Date
    Public UFI_End_dt As Date
End Class
