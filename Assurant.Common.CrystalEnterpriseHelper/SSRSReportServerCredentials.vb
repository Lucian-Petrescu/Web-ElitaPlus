Imports System.Net
Imports System.Security.Principal
Imports Microsoft.Reporting.WebForms

<Serializable()>
Public NotInheritable Class SSRSReportServerCredentials
    Implements IReportServerCredentials

    Public ReadOnly Property ImpersonationUser() As WindowsIdentity _
            Implements IReportServerCredentials.ImpersonationUser
        Get

            'Use the default windows user.  Credentials will be
            'provided by the NetworkCredentials property.
            Return Nothing

        End Get
    End Property

    Public ReadOnly Property NetworkCredentials() As ICredentials _
            Implements IReportServerCredentials.NetworkCredentials
        Get

            'Read the user information from the web.config file.  
            'By reading the information on demand instead of storing 
            'it, the credentials will not be stored in session, 
            'reducing the vulnerable surface area to the web.config 
            'file, which can be secured with an ACL.

            'User name
            'Dim userName As String =
            '    ConfigurationManager.AppSettings("MyReportViewerUser")

            'If (String.IsNullOrEmpty(userName)) Then
            '    Throw New Exception("Missing user name from web.config file")
            'End If

            ''Password
            'Dim password As String =
            '    ConfigurationManager.AppSettings("MyReportViewerPassword")

            'If (String.IsNullOrEmpty(password)) Then
            '    Throw New Exception("Missing password from web.config file")
            'End If

            ''Domain
            'Dim domain As String =
            '    ConfigurationManager.AppSettings("MyReportViewerDomain")

            'If (String.IsNullOrEmpty(domain)) Then
            '    Throw New Exception("Missing domain from web.config file")
            'End If

            Return New NetworkCredential("os08rp", "Taaz2017", "prodcead")

        End Get
    End Property

    Public Function GetFormsCredentials(ByRef authCookie As Cookie,
                                        ByRef userName As String,
                                        ByRef password As String,
                                        ByRef authority As String) _
                                        As Boolean _
            Implements IReportServerCredentials.GetFormsCredentials

        authCookie = Nothing
        userName = Nothing
        password = Nothing
        authority = Nothing

        'Not using form credentials
        Return False

    End Function

End Class
