Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports Assurant.ElitaPlus.Security

<Serializable()>
Public Class ElitaPlusIdentity
    Inherits ElitaPlusParameters
    Implements IElitaClaimsIdentity

    Private mClaims As List(Of System.IdentityModel.Claims.Claim)
    Private Const X509_Claim_Type_Code As String = "X509_CERTIFICATE"
    Private Const ClientIP_Claim_Type_Code As String = "CLIENT_IP_ADDRESS"

    Public ReadOnly Property Claims As IEnumerable(Of System.IdentityModel.Claims.Claim) Implements IElitaClaimsIdentity.Claims
        Get
            If (mClaims Is Nothing) Then
                mClaims = BuildClaims()
            End If
            Return New ReadOnlyCollection(Of System.IdentityModel.Claims.Claim)(mClaims).AsEnumerable()
        End Get
    End Property

    Private Function BuildClaims() As List(Of System.IdentityModel.Claims.Claim)
        Dim returnValue As New List(Of System.IdentityModel.Claims.Claim)
        ' Add Network ID Claim
        returnValue.Add(New System.IdentityModel.Claims.Claim(ClaimTypes.NetworkId, _user.NetworkId, System.IdentityModel.Claims.Rights.PossessProperty))
        ' Add Language Code Claims
        Dim language As New Language(_user.LanguageId)
        returnValue.Add(New IdentityModel.Claims.Claim(ClaimTypes.LanguageCode, language.Code, System.IdentityModel.Claims.Rights.PossessProperty))
        returnValue.Add(New IdentityModel.Claims.Claim(ClaimTypes.LanguageId, _user.LanguageId.ToString(), System.IdentityModel.Claims.Rights.PossessProperty))
        ' Add Company Claims
        Dim userCompanyAssignedDv As UserCompanyAssigned.UserCompanyAssignedDV = User.GetSelectedAssignedCompanies(_user.Id)
        For Each dr As DataRowView In userCompanyAssignedDv
            returnValue.Add(New IdentityModel.Claims.Claim(ClaimTypes.CompanyId, New Guid(CType(dr(UserCompanyAssigned.COL_COMPANY_ID), Byte())).ToString(), System.IdentityModel.Claims.Rights.PossessProperty))
            returnValue.Add(New IdentityModel.Claims.Claim(ClaimTypes.CompanyCode, CType(dr(CompanyDAL.COL_NAME_CODE), String), System.IdentityModel.Claims.Rights.PossessProperty))
            returnValue.Add(New IdentityModel.Claims.Claim(ClaimTypes.CompanyAuthorizationLimit, CType(dr(CompanyDAL.COL_NAME_CODE), String), CType(dr(UserCompanyAssigned.COL_AUTHORIZATION_LIMIT), Decimal).ToString()))
            returnValue.Add(New IdentityModel.Claims.Claim(ClaimTypes.CompanyPaymentLimit, CType(dr(CompanyDAL.COL_NAME_CODE), String), CType(dr(UserCompanyAssigned.COL_PAYMENT_LIMIT), Decimal).ToString()))
        Next
        Dim dvPermission As DataView = LookupListNew.DropdownLanguageLookupList(LookupListNew.LK_USER_ROLE_PERMISSION, _user.LanguageId)
        ' Add Permissions
        For Each up As UserPermission In _user.UserPermission
            returnValue.Add(New System.IdentityModel.Claims.Claim(ClaimTypes.PermissionId, up.PermissionId.ToString(), System.IdentityModel.Claims.Rights.PossessProperty))
            returnValue.Add(New System.IdentityModel.Claims.Claim(ClaimTypes.PermissionCode, LookupListNew.GetCodeFromId(dvPermission, up.PermissionId).ToUpperInvariant(), System.IdentityModel.Claims.Rights.PossessProperty))
        Next
        ''X509 Thumbprints
        Dim dvSpClaims As DataView = User.GetSpUserClaims(_user.Id, _user.LanguageId, X509_Claim_Type_Code)
        For Each row As DataRow In dvSpClaims.Table.Rows
            If row.Item("code") = X509_Claim_Type_Code Then
                returnValue.Add(New IdentityModel.Claims.Claim(ClaimTypes.X509Thumbprint, row("sp_claim_value"), IdentityModel.Claims.Rights.PossessProperty))
                returnValue.Add(New IdentityModel.Claims.Claim(ClaimTypes.X509ExpirationDate, row("sp_claim_value"), row("expiration_date")))
            ElseIf row.Item("code") = ClientIP_Claim_Type_Code Then
                returnValue.Add(New IdentityModel.Claims.Claim(ClaimTypes.ClientIP, row("sp_claim_value"), IdentityModel.Claims.Rights.PossessProperty))
                returnValue.Add(New IdentityModel.Claims.Claim(ClaimTypes.ClientIPEffectiveDate, row("sp_claim_value"), row("effective_date")))
                returnValue.Add(New IdentityModel.Claims.Claim(ClaimTypes.ClientIPExpirationDate, row("sp_claim_value"), row("expiration_date")))
            End If
        Next

        Return returnValue
    End Function


#Region " Member Variables "

    Private _user As User

#End Region


#Region " Constructors "

    Public Sub New(ByVal authenticationType As String)

        MyBase.New()
    End Sub


#End Region



#Region " Public Members"
    Public ReadOnly Property BaseUrl
        Get
            Return Me.ServiceOrderImageHostName.ToLowerInvariant().Replace("elitalogos/".ToLowerInvariant(), String.Empty)
        End Get
    End Property

    Private _emailAddress As String = Nothing

    Public ReadOnly Property EmailAddress As String
        Get
            If (_emailAddress = Nothing) Then
                _emailAddress = PopulateLDAPProperties(Me.ActiveUser.NetworkId)
            End If
            Return _emailAddress
        End Get
    End Property

    'Private _privacyUserType As AppConfig.DataProtectionPrivacyLevel
    'Public Property PrivacyUserType() As AppConfig.DataProtectionPrivacyLevel
    '    Get
    '        Return _privacyUserType
    '    End Get
    '    Set(ByVal Value As AppConfig.DataProtectionPrivacyLevel)
    '        _privacyUserType = Value
    '    End Set
    'End Property


    'Private _dbPrivacyUserType As String
    'Public Property DBPrivacyUserType() As String
    '    Get
    '        Return _dbPrivacyUserType
    '    End Get
    '    Set(ByVal Value As String)
    '        _dbPrivacyUserType = Value
    '    End Set
    'End Property

    Public Shared Function GetEmailAddresses(ByVal pUserIds As String()) As String()
        Return pUserIds.Select(Of String)(Function(lan) PopulateLDAPProperties(lan)).ToArray()
    End Function

    Private Shared Function PopulateLDAPProperties(ByVal pUserId As String) As String
        Dim ldapProperties As Dictionary(Of String, Object) = Authentication.GetLdapFields(pUserId, New String() {"mail"})

        If (ldapProperties.ContainsKey("mail")) Then
            Try

                If TypeOf ldapProperties("mail") Is System.DirectoryServices.ResultPropertyValueCollection Then
                    Dim resultCollection As System.DirectoryServices.ResultPropertyValueCollection = ldapProperties("mail")


                    For Each Item In resultCollection
                        If Item.EndsWith("@assurant.com") Then
                            Return Item.ToString()
                        End If
                    Next
                ElseIf TypeOf ldapProperties("mail") Is String Then
                    Return ldapProperties("mail")
                End If


            Catch ex As Exception
                Return ldapProperties("mail")
            End Try

        End If
    End Function

    <Obsolete("Create/Use one of extension method from Assurant.Elita.Security.IdentityExtensions")>
    Public ReadOnly Property ActiveUser() As User
        Get
            Return Me._user
        End Get
    End Property

    Public Overrides ReadOnly Property Name() As String
        Get
            '  Dim sUserName As String = "DB Connection Problem Or the User is Invalid "
            Dim sUserName As String = Nothing
            If Not Me._user Is Nothing Then
                sUserName = Me._user.UserName
            End If
            Return sUserName
        End Get
    End Property

    Public Function isInRole(ByVal roleCode As String) As Boolean
        Return Me._user.isInRole(roleCode)
    End Function

    Public Sub CreateUser(ByVal userNetworkID As String)
        Me._user = New User(userNetworkID)
    End Sub

    Public Function IsValidUser() As Boolean
        Dim bValid As Boolean = False

        If ((Not ActiveUser() Is Nothing) AndAlso (ActiveUser().Active = "Y")) Then
            bValid = True
        End If
        Return bValid
    End Function
#End Region


#Region "Static Methods"

    Public Shared ReadOnly Property Current() As ElitaPlusIdentity
        Get
            Return CType(ElitaPlusPrincipal.Current.Identity, ElitaPlusIdentity)
        End Get
    End Property


#End Region



End Class





