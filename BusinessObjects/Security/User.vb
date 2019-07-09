'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (7/26/2004)  ********************
Imports System.Collections.Generic
Imports System.ServiceModel

Public Class User
    Inherits BusinessObjectBase
    Implements IPermissionParent

#Region "CONSTANTS"

    Private Const FLAG_IMAGE As String = "Flag.gif"
    Private Const SPLASH_IMAGE As String = "Splash.jpg"

#End Region

#Region "Variables"

    Private _extendedUser As Auth.ExtendedUser = Nothing
    Private _syncRoot As New Object

    Private moCompanies As Hashtable

    ''' <summary>
    ''' Gets users who have specific previledge code assigned to them
    ''' </summary>
    ''' <param name="pPrivilegeCode"></param>
    ''' <returns></returns>

        Public Shared Function GetUsers(ByVal country_id As Guid, ByVal pPermissionCode As String) As String()
        Dim dal As New UserDAL
        Dim oLANIdDs As DataSet
        Dim lanIdList As New List(Of String)

        oLANIdDs = dal.LoadUsersBasedOnPermission(country_id, pPermissionCode)
        If oLANIdDs.Tables(0).Rows.Count > 0 Then
            Dim index As Integer

            ' Create Array
            For index = 0 To oLANIdDs.Tables(0).Rows.Count - 1
                If Not oLANIdDs.Tables(0).Rows(index)("NETWORK_ID") Is System.DBNull.Value Then
                    lanIdList.Add(oLANIdDs.Tables(0).Rows(index)("NETWORK_ID").ToString())
                End If
            Next
        End If
        Return lanIdList.ToArray()
    End Function

    Private moIsIHQRole As Integer = 0

    Private _authorizationLimits As Dictionary(Of Guid, DecimalType) = New Dictionary(Of Guid, DecimalType)
    Private _paymentLimits As Dictionary(Of Guid, DecimalType) = New Dictionary(Of Guid, DecimalType)
    Private _liabilityOverrideLimits As Dictionary(Of Guid, DecimalType) = New Dictionary(Of Guid, DecimalType)

    Public SelectedCompanyId As Guid
#End Region

#Region "Variables: External User"
    Public Property UserPermission As UserPermissionList
    Private userCompanies As ArrayList
    Private moServiceCenter_Or_DealerIDs As ArrayList
    Private userAccountingCompanies As AcctCompany()
#End Region

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load(id)
        Me.userPermission = New UserPermissionList(Me)
    End Sub

    'Exiting BO using networkId
    Public Sub New(ByVal networkId As String)
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load(networkId)
        Me.UserPermission = New UserPermissionList(Me)
    End Sub


    'New BO
    Public Sub New()
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load()
        Me.UserPermission = New UserPermissionList(Me)
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDS As DataSet)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load(id)
        Me.UserPermission = New UserPermissionList(Me)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As DataSet)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load()
        Me.UserPermission = New UserPermissionList(Me)
    End Sub

    Protected Sub Load()
        Dim dal As New UserDAL
        If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
            dal.LoadSchema(Me.Dataset)
        End If
        Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
        Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
        Me.Row = newRow
        SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Me.Row = Nothing
        Dim dal As New UserDAL
        If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
            Me.Row = Me.FindRow(id, dal.TABLE_KEY_NAME, Me.Dataset.Tables(dal.TABLE_NAME))
        End If
        If Me.Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
            dal.Load(Me.Dataset, id)
            Me.Row = Me.FindRow(id, dal.TABLE_KEY_NAME, Me.Dataset.Tables(dal.TABLE_NAME))
        End If
        If Me.Row Is Nothing Then
            Throw New DataNotFoundException
        End If
        LoadCompanyAssigned(id)
    End Sub

    Protected Sub Load(ByVal networkId As String)
        Me.Row = Nothing
        Dim dal As New UserDAL
        If Me.Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
            dal.LoadByNetworkId(Me.Dataset, networkId)
            Me.Row = Me.Dataset.Tables(dal.TABLE_NAME).Rows(0)
        End If
        If Me.Row Is Nothing Then
            Throw New DataNotFoundException
        End If
        LoadCompanyAssigned(New Guid(CType(Me.Row(UserDAL.COL_NAME_USER_ID), Byte())))
    End Sub

    Private Sub LoadCompanyAssigned(ByVal id As Guid)
        If Not Me.Row Is Nothing Then
            Dim userCompanyAssignedDv As UserCompanyAssigned.UserCompanyAssignedDV = GetSelectedAssignedCompanies(id)
            _paymentLimits = New Dictionary(Of Guid, DecimalType)
            _authorizationLimits = New Dictionary(Of Guid, DecimalType)
            _liabilityOverrideLimits= New Dictionary(Of Guid, DecimalType)
            Dim companyId As Guid
            For Each dr As DataRowView In userCompanyAssignedDv
                companyId = New Guid(CType(dr(UserCompanyAssigned.COL_COMPANY_ID), Byte()))
                _paymentLimits.Add(companyId, New DecimalType(CType(dr(UserCompanyAssigned.COL_PAYMENT_LIMIT), Decimal)))
                _authorizationLimits.Add(companyId, New DecimalType(CType(dr(UserCompanyAssigned.COL_AUTHORIZATION_LIMIT), Decimal)))
                _liabilityOverrideLimits.Add(companyId, New DecimalType(CType(dr(UserCompanyAssigned.COL_LIABILITY_OVERRIDE_LIMIT), Decimal)))
            Next
        End If
    End Sub
#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid Implements IPermissionParent.Id
        Get
            If Row(UserDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(UserDAL.COL_NAME_USER_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=8)> _
    Public Property NetworkId() As String
        Get
            CheckDeleted()
            If Row(UserDAL.COL_NAME_NETWORK_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(UserDAL.COL_NAME_NETWORK_ID), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(UserDAL.COL_NAME_NETWORK_ID, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=70)> _
    Public Property UserName() As String
        Get
            CheckDeleted()
            If Row(UserDAL.COL_NAME_USER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(UserDAL.COL_NAME_USER_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(UserDAL.COL_NAME_USER_NAME, Value)
        End Set
    End Property

    ''' <summary>
    ''' Gets Authorization Limit specific to Company ID. This property is ready only and to be used only to fetch limits using User Identity Object. This is NOT used for updating the Authorization Limits to Data Store.
    ''' </summary>
    ''' <param name="companyId">Company ID for which Authorization Limit is to be fetched.</param>
    ''' <returns><see cref="DecimalType" /> object containing value of Authorization Limit for Company ID specified in Parameter.</returns>
    ''' <remarks>Returns Nothing when Company ID not found or Authorization Limits not initialized.</remarks>
    Public ReadOnly Property AuthorizationLimit(ByVal companyId As Guid) As DecimalType
        Get
            CheckDeleted()
            If (_authorizationLimits Is Nothing) Then
                Return New DecimalType(0)
            End If

            If (_authorizationLimits.ContainsKey(companyId)) Then
                Return _authorizationLimits(companyId)
            Else
                Return New DecimalType(0)
            End If
        End Get
    End Property

    ''' <summary>
    ''' Gets Payment Limit specific to Company ID. This property is ready only and to be used only to fetch limits using User Identity Object. This is NOT used for updating the Payment Limits to Data Store.
    ''' </summary>
    ''' <param name="companyId">Company ID for which Payment Limit is to be fetched.</param>
    ''' <returns><see cref="DecimalType" /> object containing value of Payment Limit for Company ID specified in Parameter.</returns>
    ''' <remarks>Returns Nothing when Company ID not found or Payment Limits not initialized.</remarks>
    Public ReadOnly Property PaymentLimit(ByVal companyId As Guid) As DecimalType
        Get
            CheckDeleted()
            If (_paymentLimits Is Nothing) Then
                Return New DecimalType(0)
            End If

            If (_paymentLimits.ContainsKey(companyId)) Then
                Return _paymentLimits(companyId)
            Else
                Return New DecimalType(0)
            End If
        End Get
    End Property

    Public ReadOnly Property LiabilityOverrideLimit(ByVal companyId As Guid) As DecimalType
        Get
            CheckDeleted()
            If (_liabilityOverrideLimits Is Nothing) Then
                Return New DecimalType(0)
            End If

            If (_liabilityOverrideLimits.ContainsKey(companyId)) Then
                Return _liabilityOverrideLimits(companyId)
            Else
                Return New DecimalType(0)
            End If
        End Get
    End Property

    Private Sub AddCompanyAssigned(ByVal oCompanyId As Guid, Optional ByVal oAuthorizationLimit As DecimalType = Nothing, Optional ByVal oPaymentLimit As DecimalType = Nothing,
                                   Optional ByVal oLiabilityOverrideLimit As DecimalType = Nothing)
        If (oAuthorizationLimit = Nothing) Then
            oAuthorizationLimit = New DecimalType(0)
        End If

        If (oPaymentLimit = Nothing) Then
            oPaymentLimit = New DecimalType(0)
        End If

        If (oLiabilityOverrideLimit = Nothing) Then
            oLiabilityOverrideLimit = New DecimalType(0)
        End If

        '_authorizationLimits.Add(oCompanyId, oAuthorizationLimit)
        '_paymentLimits.Add(oCompanyId, oPaymentLimit)
    End Sub
    Public ReadOnly Property CompanyId() As Guid
        Get
            Return Me.FirstCompanyID
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property LanguageId() As Guid
        Get
            CheckDeleted()
            If Row(UserDAL.COL_NAME_LANGUAGE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(UserDAL.COL_NAME_LANGUAGE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(UserDAL.COL_NAME_LANGUAGE_ID, Value)
        End Set
    End Property

    Public ReadOnly Property LanguageCode() As String
        Get
            If Me.LanguageId = Nothing Then
                Return String.Empty
            Else
                Return New Language(Me.LanguageId).Code
            End If
        End Get
    End Property

    <ValidStringLength("", Max:=20)> _
    Public Property Id1() As String
        Get
            CheckDeleted()
            If Row(UserDAL.COL_NAME_ID1) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(UserDAL.COL_NAME_ID1), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(UserDAL.COL_NAME_ID1, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=20)> _
    Public Property Id2() As String
        Get
            CheckDeleted()
            If Row(UserDAL.COL_NAME_ID2) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(UserDAL.COL_NAME_ID2), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(UserDAL.COL_NAME_ID2, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=1)> _
    Public Property Active() As String
        Get
            CheckDeleted()
            If Row(UserDAL.COL_NAME_ACTIVE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(UserDAL.COL_NAME_ACTIVE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(UserDAL.COL_NAME_ACTIVE, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=1)> _
    Public Property External() As String
        Get
            CheckDeleted()
            If Row(UserDAL.COL_NAME_EXTERNAL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(UserDAL.COL_NAME_EXTERNAL), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(UserDAL.COL_NAME_EXTERNAL, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=20)> _
    Public Property Password() As String
        Get
            CheckDeleted()
            If Row(UserDAL.COL_NAME_PASSWORD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(UserDAL.COL_NAME_PASSWORD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(UserDAL.COL_NAME_PASSWORD, Value)
        End Set
    End Property



#End Region

#Region "External Properties"


    Public Property ExternalTypeId() As Guid
        Get
            CheckDeleted()
            If Row(UserDAL.COL_NAME_EXTERNAL_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(UserDAL.COL_NAME_EXTERNAL_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(UserDAL.COL_NAME_EXTERNAL_TYPE_ID, Value)
        End Set
    End Property

    Public Property ScDealerId() As Guid
        Get
            CheckDeleted()
            If Row(UserDAL.COL_NAME_SC_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(UserDAL.COL_NAME_SC_DEALER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(UserDAL.COL_NAME_SC_DEALER_ID, Value)
        End Set
    End Property


    ' First Company
    Public ReadOnly Property Company() As Company
        Get
            Dim oCompany = Company(FirstCompanyID)
            Return oCompany
        End Get
    End Property

    Public ReadOnly Property Company(ByVal oCompanyId As Guid) As Company
        Get
            Dim oCompany As Company
            If moCompanies.ContainsKey(oCompanyId.ToString) Then
                oCompany = CType(moCompanies.Item(oCompanyId.ToString), Company)
            End If

            Return oCompany
        End Get
    End Property

    Public ReadOnly Property Country(ByVal oCompanyId As Guid) As Country
        Get
            Dim oCountry As New Country(Company(oCompanyId).CountryId)

            Return oCountry
        End Get
    End Property

    Public ReadOnly Property CompanyGroup() As CompanyGroup
        Get
            Dim oCompanyGroup = New CompanyGroup(Me.Company.CompanyGroupId)

            Return oCompanyGroup
        End Get
    End Property

    Public ReadOnly Property FirstCompanyID() As Guid
        Get
            Dim oFirstId As Guid = Guid.Empty

            If Me.Companies.Count > 0 Then
                oFirstId = CType((Me.Companies())(0), Guid)
            End If
            Return oFirstId
        End Get
    End Property

    Public ReadOnly Property ExtendedUser As Auth.ExtendedUser
        Get
            If (Me.IsDeleted) Then
                _extendedUser = Nothing
                Return Nothing
            End If
            If (_extendedUser Is Nothing) Then
                SyncLock (_syncRoot)
                    If (_extendedUser Is Nothing) Then
                        Try
                            _extendedUser = ServiceHelper.CreateAuthorizationClient().GetUserForServiceByKey(Me.NetworkId, ServiceHelper.WORKQUEUE_SERVICE_NAME)
                        Catch ex As FaultException(Of WrkQueue.ValidationFault)
                            Throw ex.AsBOValidationException()
                        Catch ex As FaultException(Of Auth.AuthorizationFault)
                            Throw New ServiceException("Authorization", "GetUserForServiceByKey", ex)
                        End Try
                    End If
                End SyncLock
            End If
            Return _extendedUser
        End Get
    End Property

    Friend Sub ResetExtendedUser()
        Me._extendedUser = Nothing
    End Sub


    Public ReadOnly Property CompaniesCountryFlagImage() As String
        Get
            Dim sCompanyCountry As String = Country(FirstCompanyID).Description

            Return sCompanyCountry & FLAG_IMAGE
        End Get

    End Property

    Public ReadOnly Property CompaniesCountrySplashImage() As String
        Get
            Dim sCompanyCountry As String = Country(FirstCompanyID).Description

            Return sCompanyCountry & SPLASH_IMAGE
        End Get

    End Property
#End Region

#Region "Properties: External User"

    Public ReadOnly Property IsExternal() As Boolean
        Get
            Dim bExternal As Boolean = False
            If Me.External = "Y" Then
                bExternal = True
            End If
            Return bExternal
        End Get
    End Property

    Public ReadOnly Property IsDealerGroup() As Boolean
        Get
            Dim bDealerGroup As Boolean = False
            If IsExternal AndAlso _
               Me.ExternalTypeId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_EXTERNAL_USER_TYPES, Codes.EXTERNAL_USER_TYPE__DEALER_GROUP)) Then
                bDealerGroup = True
            End If
            Return bDealerGroup
        End Get
    End Property
    Public ReadOnly Property IsDealer() As Boolean
        Get
            Dim bDealer As Boolean = False
            If IsExternal AndAlso _
               Me.ExternalTypeId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_EXTERNAL_USER_TYPES, Codes.EXTERNAL_USER_TYPE__DEALER)) Then
                bDealer = True
            End If
            Return bDealer
        End Get
    End Property

    Public ReadOnly Property IsServiceCenter() As Boolean
        Get
            Dim bServiceCenter As Boolean = False
            If IsExternal AndAlso _
               Me.ExternalTypeId.Equals(LookupListNew.GetIdFromCode(LookupListNew.LK_EXTERNAL_USER_TYPES, Codes.EXTERNAL_USER_TYPE__SERVICE_CENTER)) Then
                bServiceCenter = True
            End If
            Return bServiceCenter
        End Get
    End Property

    Public ReadOnly Property IsIHQRole() As Boolean
        Get
            If moIsIHQRole = 0 Then
                Dim dal As New UserDAL
                Dim ds As DataSet = dal.LoadUserIHQRoles(Me.Id)
                If (Not ds Is Nothing) AndAlso ds.Tables(0).Rows.Count > 0 Then
                    moIsIHQRole = 1
                Else
                    moIsIHQRole = -1
                End If
            End If
            Return (moIsIHQRole > 0)
        End Get
    End Property


#End Region

#Region "Public Members"
    Private Sub UpdateRemoteUser()
        Dim provider As New AuthorizationServiceRoleProvider
        Select Case Me.Row.RowState
            Case DataRowState.Added
                If (provider.SupportsCreateUser()) Then
                    provider.CreateUser(Me.Id, Me.NetworkId.ToUpperInvariant(), Me.UserName, Me.Active = "Y")
                End If
            Case DataRowState.Deleted
                If (provider.SupportsDeleteUser()) Then
                    provider.DeleteUser(New Guid(CType(Row(UserDAL.COL_NAME_USER_ID, DataRowVersion.Original), Byte())), CType(Row(UserDAL.COL_NAME_NETWORK_ID, DataRowVersion.Original), String).ToUpperInvariant(), CType(Row(UserDAL.COL_NAME_USER_NAME, DataRowVersion.Original), String))
                End If
            Case DataRowState.Modified
                If (provider.SupportsUpdateUser()) Then
                    provider.UpdateUser(Me.Id, Me.Row(UserDAL.COL_NAME_NETWORK_ID, DataRowVersion.Original).ToString().ToUpperInvariant(), Me.NetworkId.ToUpperInvariant(), Me.UserName, Me.Active = "Y")
                End If
        End Select

        If (Me.Row.RowState <> DataRowState.Deleted) Then
            ' Update Grants and Revokes on Remote System
            Dim oRole As Role
            Dim oUserRole As UserRole
            Dim oDataTable As DataTable
            If (Not Me.Dataset.Tables(UserRoleDAL.TABLE_NAME) Is Nothing) Then
                oDataTable = Me.Dataset.Tables(UserRoleDAL.TABLE_NAME).GetChanges(DataRowState.Deleted)
                If (Not oDataTable Is Nothing) Then
                    For Each dr As DataRow In oDataTable.Rows
                        oRole = New Role(New Guid(CType(dr(UserRoleDAL.COL_NAME_ROLE_ID, DataRowVersion.Original), Byte())))
                        If (Not oRole.RemoteRoleId.Equals(Guid.Empty)) Then
                            provider.Revoke(oRole.Id, oRole.RemoteRoleId, oRole.Code, Me.Id, Me.NetworkId, Me.UserName)
                        End If
                    Next
                End If
                oDataTable = Me.Dataset.Tables(UserRoleDAL.TABLE_NAME).GetChanges(DataRowState.Added Or DataRowState.Modified)
                If (Not oDataTable Is Nothing) Then
                    For Each dr As DataRow In oDataTable.Rows
                        oUserRole = New UserRole(dr)
                        oRole = New Role(oUserRole.RoleId)
                        If (Not oRole.RemoteRoleId.Equals(Guid.Empty)) Then
                            provider.Grant(oRole.Id, oRole.RemoteRoleId, oRole.Code, Me.Id, Me.NetworkId, Me.UserName)
                        End If
                    Next
                End If
            End If
        End If
    End Sub

    Public Overrides Sub Save()
        MyBase.Save()

        Dim dal As New UserDAL

        ' Call Web Service to Manage User on Remote System

        If Assurant.Elita.Configuration.ElitaConfig.Current.General.IntegrateWorkQueueImagingServices = True Then
            UpdateRemoteUser()
        End If

        dal.UpdateFamily(Me.Dataset)
        '  dal.Update(Me.Dataset)
        'Reload the Data
        If Me._isDSCreator AndAlso Me.Row.RowState <> DataRowState.Detached Then
            'Reload the Data from the DB
            Dim objId As Guid = Me.Id
            Me.Dataset = New DataSet
            Me.Row = Nothing
            Me.Load(objId)
            Me.UserPermission = New UserPermissionList(Me)
            ' Me.Load(Me.Id)
        End If
    End Sub
#End Region

#Region "External BOs"

    Public Shared Function GetUserRoles(ByVal userId As Guid) As DataView
        Dim dal As New UserDAL
        Dim ds As DataSet

        ds = dal.LoadUserRoles(userId)
        Return ds.Tables(UserDAL.TABLE_USER_ROLES).DefaultView
    End Function

    Public Shared Function GetAvailableRoles(ByVal userId As Guid) As DataView
        Dim dal As New UserDAL
        Dim ds As DataSet

        ds = dal.LoadAvailableRoles(userId)
        Return ds.Tables(UserDAL.TABLE_USER_ROLES).DefaultView
    End Function

    Public Shared Function LoadUserCompanyAssigned(ByVal grpID As Guid, ByVal userId As Guid) As DataView
        Dim dal As New UserDAL
        Dim ds As DataSet

        ds = dal.LoadUserCompanyAssigned(grpID, userId)
        Return ds.Tables(UserDAL.TABLE_USER_COMPANY_ASSIGNED).DefaultView
    End Function

    Public Shared Function GetAvailableCompanies(ByVal grpID As Guid, ByVal userId As Guid) As DataView
        Dim dal As New UserDAL
        Dim ds As DataSet

        ds = dal.LoadAvailableCompanies(grpID, userId)
        Return ds.Tables(UserDAL.TABLE_USER_COMPANIES).DefaultView
    End Function

    Public Shared Function GetSelectedCompanies(ByVal grpID As Guid, ByVal userId As Guid) As DataView
        Dim dal As New UserDAL
        Dim ds As DataSet

        ds = dal.LoadSelectedCompanies(grpID, userId)
        Return ds.Tables(UserDAL.TABLE_USER_COMPANIES).DefaultView
    End Function

    Public Shared Function GetAvailableAssignedCompanies(ByVal userId As Guid) As DataView
        Dim dal As New UserDAL
        Dim ds As DataSet

        ds = dal.LoadAvailableAssignedCompanies(userId)
        Return ds.Tables(UserDAL.TABLE_USER_COMPANIES).DefaultView
    End Function

    Public Shared Function GetSelectedAssignedCompanies(ByVal userId As Guid) As UserCompanyAssigned.UserCompanyAssignedDV
        Dim dal As New UserDAL
        Dim ds As DataSet

        ds = dal.LoadSelectedAssignedCompanies(userId)
        Return New UserCompanyAssigned.UserCompanyAssignedDV(ds.Tables(UserCompanyAssignedDAL.TABLE_NAME))
    End Function

    Public Shared Function GetAvailableCompanyGroup(ByVal userId As Guid) As DataView
        Dim dal As New UserDAL
        Dim ds As DataSet
        ds = dal.LoadAvailableCompanyGroup(userId)
        Return ds.Tables(UserDAL.TABLE_USER_COMPANY_GROUPS).DefaultView
    End Function

    Public Shared Function GetUserCountries(ByVal userId As Guid) As DataView
        Dim dal As New UserDAL
        Dim ds As DataSet

        ds = dal.LoadUserCountries(userId)
        Return ds.Tables(UserDAL.TABLE_USER_COMPANIES).DefaultView
    End Function

    Public Shared Function GetUserCompanies(ByVal userId As Guid) As DataView
        Dim dal As New UserDAL
        Dim ds As DataSet

        ds = dal.LoadUserCompanies(userId)
        Return ds.Tables(UserDAL.TABLE_USER_COMPANIES).DefaultView
    End Function

    Public Shared Function GetUserCompanies(ByVal userId As Guid, ByVal oCountryId As Guid) As DataView
        Dim dal As New UserDAL
        Dim ds As DataSet

        ds = dal.LoadUserCompanies(userId, oCountryId)
        Return ds.Tables(UserDAL.TABLE_USER_COUNTRY_COMPANIES).DefaultView
    End Function

    Public Shared Function GetUserBasedOnPermission(ByVal userId As Guid, ByVal companyId As Guid, ByVal permission_type_code As String) As DataView
        Dim dal As New UserDAL
        Dim ds As DataSet

        ds = dal.LoadUsersBasedOnPermission(userId, companyId, permission_type_code)
        Return ds.Tables(UserDAL.TABLE_USER_COMPANIES).DefaultView
    End Function

    'Public Shared Function GetUserDealers(ByVal groupId As Guid) As DataView
    '    Dim dal As New UserDAL
    '    Dim ds As Dataset

    '    ds = dal.LoadUserDealers(groupId)
    '    Return ds.Tables(UserDAL.TABLE_USER_DEALERS).DefaultView
    'End Function

    'Public Shared Function GetUserSC(ByVal groupId As Guid) As DataView
    '    Dim dal As New UserDAL
    '    Dim ds As Dataset

    '    ds = dal.LoadUserSC(groupId)
    '    Return ds.Tables(UserDAL.TABLE_USER_SC).DefaultView
    'End Function



    Private userRoles As DataView
    Public Function isInRole(ByVal roleCode As String) As Boolean
        If userRoles Is Nothing Then
            userRoles = Me.GetUserRoles(Me.Id)
            userRoles.Sort = UserDAL.COL_NAME_ROLE_CODE
        End If
        If Not userRoles Is Nothing Then
            Return userRoles.Find(roleCode) >= 0
        End If
        Return False
    End Function

    Public Function Companies() As ArrayList
        Dim oCompaniesDv As DataView
        If userCompanies Is Nothing Then
            oCompaniesDv = Me.GetUserCompanies(Me.Id)
            moCompanies = Nothing
            userCompanies = New ArrayList

            If oCompaniesDv.Table.Rows.Count > 0 Then
                Dim index As Integer
                ' Create BOs
                moCompanies = New Hashtable
                For index = 0 To oCompaniesDv.Table.Rows.Count - 1
                    If Not oCompaniesDv.Table.Rows(index)("COMPANY_ID") Is System.DBNull.Value Then
                        Dim oCompanyId As New Guid(CType(oCompaniesDv.Table.Rows(index)("COMPANY_ID"), Byte()))
                        moCompanies.Add(oCompanyId.ToString, New Company(oCompanyId))
                    End If
                Next

                ' Create Array
                For index = 0 To oCompaniesDv.Table.Rows.Count - 1
                    If Not oCompaniesDv.Table.Rows(index)("COMPANY_ID") Is System.DBNull.Value Then
                        userCompanies.Add(New Guid(CType(oCompaniesDv.Table.Rows(index)("COMPANY_ID"), Byte())))
                    End If
                Next
            End If
        End If
        Return userCompanies
    End Function

    Public Function Companies(ByVal oCountryId As Guid) As ArrayList
        Dim oCompaniesDv As DataView
        Dim oCompanies = New ArrayList

        oCompaniesDv = Me.GetUserCompanies(Me.Id, oCountryId)
        If oCompaniesDv.Table.Rows.Count > 0 Then
            Dim index As Integer
            ' Create Array
            For index = 0 To oCompaniesDv.Table.Rows.Count - 1
                If Not oCompaniesDv.Table.Rows(index)("COMPANY_ID") Is System.DBNull.Value Then
                    oCompanies.Add(New Guid(CType(oCompaniesDv.Table.Rows(index)("COMPANY_ID"), Byte())))
                End If
            Next
        End If
        Return oCompanies
    End Function

    Public Function AccountingCompanies() As AcctCompany()

        If Not userAccountingCompanies Is Nothing Then
            Return userAccountingCompanies
        End If

        Try
            userAccountingCompanies = AcctCompany.GetAccountingCompanies(Me.Companies)
            Return userAccountingCompanies
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Sub AccountingCompaniesClearCache()
        userAccountingCompanies = Nothing
    End Sub

    Public Function Countries() As ArrayList
        Dim oCountriesDv = Me.GetUserCountries(Me.Id)
        Dim oCountriesArr = New ArrayList

        If oCountriesDv.Table.Rows.Count > 0 Then
            Dim index As Integer

            ' Create Array
            For index = 0 To oCountriesDv.Table.Rows.Count - 1
                If Not oCountriesDv.Table.Rows(index)("COUNTRY_ID") Is System.DBNull.Value Then
                    oCountriesArr.Add(New Guid(CType(oCountriesDv.Table.Rows(index)("COUNTRY_ID"), Byte())))
                End If
            Next
        End If

        Return oCountriesArr
    End Function

    Public Shared Function GetUserList() As DataView
        Dim dal As New UserDAL
        Dim ds As DataSet

        Try
            ds = dal.LoadList()
            '  Return ds.Tables(UserDAL.TABLE_USERS_ROLES_SEARCH).DefaultView
            Return ds.Tables(UserDAL.TABLE_USERS_SEARCH).DefaultView()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Sub InitUserRoleTable()
        Dim oUserRole As UserRole

        oUserRole = New UserRole(Me.Dataset)
        oUserRole.InitTable()
    End Sub

    Public Function IsUserAssignedRoles() As Boolean
        Dim blnRoleAssigned As Boolean = False
        Dim dr As DataRow, intRoles As Integer, intDeleted As Integer, intAdded As Integer

        If Me.IsNew Then 'new BO
            If Me.Dataset.Tables(UserRoleDAL.TABLE_NAME) Is Nothing Then
                Return False
            Else
                For Each dr In Me.Dataset.Tables(UserRoleDAL.TABLE_NAME).Rows
                    If Not dr.RowState = DataRowState.Deleted Then
                        blnRoleAssigned = True
                        Exit For
                    End If
                Next
                Return blnRoleAssigned
            End If
        Else 'EXISTING BO
            intRoles = GetUserRoles(Me.Id).Count
            If Me.Dataset.Tables(UserRoleDAL.TABLE_NAME) Is Nothing Then
                If intRoles = 0 Then
                    Return False
                Else
                    Return True
                End If
            Else
                For Each dr In Me.Dataset.Tables(UserRoleDAL.TABLE_NAME).Rows
                    If dr.RowState = DataRowState.Deleted Then
                        intDeleted = intDeleted + 1
                    ElseIf dr.RowState = DataRowState.Added Then
                        intAdded = intAdded + 1
                    End If
                Next
                If intRoles + intAdded - intDeleted > 0 Then
                    Return True
                Else
                    Return False
                End If
            End If
        End If
    End Function

    Public Sub ValidateUserRolesAssigned()
        If Not IsDeleted Then
            If Not IsUserAssignedRoles() Then 'user role is required
                Dim objUR As UserRole = New UserRole()
                Dim err() As ValidationError = {New ValidationError(Common.ErrorCodes.GUI_ROLE_MUST_BE_SELECTED_ERR, objUR.GetType, Nothing, "RoleId", Nothing)}
                Throw New BOValidationException(err, objUR.GetType().FullName)
            End If
        End If
    End Sub

    Public Function AddUserRoleChild(ByVal roleId As Guid) As UserRole
        Dim oUserRole As UserRole

        oUserRole = New UserRole(Me.Dataset)
        oUserRole.UserId = Me.Id
        oUserRole.RoleId = roleId
        Return oUserRole
    End Function

    Public Function GetUserRoleChild(ByVal roleId As Guid) As UserRole
        Dim oUserRole As UserRole

        oUserRole = New UserRole(Me.Dataset, Me.Id, roleId)
        Return oUserRole
    End Function

    Public Sub AttachUserRoles(ByVal selectedRoleGuidStrCollection As ArrayList)
        Dim userRoleIdStr As String
        For Each userRoleIdStr In selectedRoleGuidStrCollection
            Dim userRoleBO As UserRole = AddRoleChild(New Guid(userRoleIdStr))
            userRoleBO.Save()
        Next
    End Sub

    Public Sub DetachUserRoles(ByVal selectedRoleGuidStrCollection As ArrayList)
        Dim userRoleIdStr As String
        For Each userRoleIdStr In selectedRoleGuidStrCollection
            Dim userRoleBO As UserRole = Me.GetRoleChild(New Guid(userRoleIdStr))
            userRoleBO.Delete()
            userRoleBO.Save()
        Next
    End Sub

    Public Function AddRoleChild(ByVal RoleId As Guid) As UserRole
        Dim oUserRole As UserRole

        oUserRole = New UserRole(Me.Dataset)
        oUserRole.UserId = Me.Id
        oUserRole.RoleId = RoleId
        Return oUserRole

    End Function

    Public Function GetRoleChild(ByVal RoleId As Guid) As UserRole
        Dim oUserRole As UserRole

        oUserRole = New UserRole(Me.Dataset, Me.Id, RoleId)
        Return oUserRole
    End Function

    Public Sub InitCompanyGrpTable()
        Dim oUserCompanyAssigned As UserCompanyAssigned
        oUserCompanyAssigned = New UserCompanyAssigned(Me.Dataset)
        oUserCompanyAssigned.InitTable()

        Dim oUserCompany As UserCompany
        oUserCompany = New UserCompany(Me.Dataset)
        oUserCompany.InitTable()
    End Sub

    Public Function AddCompanyGrpChild(ByVal CompanyId As Guid, ByVal AuthorizationLimit As Decimal, ByVal PaymentLimit As Decimal,ByVal LiabilityOverrideLimit As Decimal) As UserCompanyAssigned
        Dim oUserCompanyAssigned As UserCompanyAssigned

        oUserCompanyAssigned = New UserCompanyAssigned(Me.Dataset)
        oUserCompanyAssigned.UserId = Me.Id
        oUserCompanyAssigned.CompanyId = CompanyId
        oUserCompanyAssigned.AuthorizationLimit = New DecimalType(AuthorizationLimit)
        oUserCompanyAssigned.PaymentLimit = New DecimalType(PaymentLimit)
        oUserCompanyAssigned.LiabilityOverrideLimit = New DecimalType(LiabilityOverrideLimit)
        Return oUserCompanyAssigned
    End Function

    Public Function GetCompanyGrpChild(ByVal CompanyId As Guid) As UserCompanyAssigned
        Dim oUserCompanyAssigned As UserCompanyAssigned

        oUserCompanyAssigned = New UserCompanyAssigned(Me.Dataset, Me.Id, CompanyId)
        Return oUserCompanyAssigned
    End Function

    Public Function AddUserCompanyGrpChild(ByVal CompanyId As Guid) As UserCompany
        Dim oUserCompany As UserCompany
        oUserCompany = New UserCompany(Me.Dataset)
        oUserCompany.UserId = Me.Id
        oUserCompany.CompanyId = CompanyId
        Return oUserCompany
    End Function

    Public Function GetUserCompanyGrpChild(ByVal CompanyId As Guid) As UserCompany
        Dim oUserCompany As UserCompany
        oUserCompany = New UserCompany(Me.Dataset, Me.Id, CompanyId)
        Return oUserCompany
    End Function

    Public Function UpdateUserCompanies(ByVal oCompanies As ArrayList) As ArrayList
        Dim oCompanyId As Guid
        Dim oUserCompany As UserCompany
        Dim oDataset As DataSet = New DataSet
        Dim oUserDAL As New UserDAL
        Try
            ' Create The New User Companies
            For Each oCompanyId In oCompanies
                oUserCompany = New UserCompany(oDataset)
                oUserCompany.UserId = Me.Id
                oUserCompany.CompanyId = oCompanyId
                oUserCompany.Save()
            Next
            ' Update Delete, Insert
            oUserDAL.UpdateUserCompanies(Me.Id, oDataset)
            ResetUserCompany()
            Return Companies()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Function

    Public Sub ResetUserCompany()
        userCompanies = Nothing
        AppConfig.ClearCache()
    End Sub


    Public Shared Function LoadUserOtherCompaniesIDs(ByVal UserFirstCompany_id As Guid, ByVal companyGroup_id As Guid) As ArrayList
        ' Since the Active user companies may not have all the companies of the user's company_group,
        ' this method is intended to provide the companies (IDs only) within the company group of the active user.
        ' The active user first company will be excluded.
        ' This fuction is being used by the AccountingCloseDates screen.
        Dim dal As New UserDAL
        Dim ds As DataSet
        ds = dal.LoadUserOtherCompaniesIDs(UserFirstCompany_id, companyGroup_id)

        Dim oCompaniesDataTable As DataTable = ds.Tables(UserDAL.TABLE_USER_COMPANIES)
        Dim oCompaniesArr = New ArrayList
        If oCompaniesDataTable.Rows.Count > 0 Then
            ' Create Array
            For index As Integer = 0 To oCompaniesDataTable.Rows.Count - 1
                If Not oCompaniesDataTable.Rows(index)("company_id") Is System.DBNull.Value Then
                    oCompaniesArr.Add(New Guid(CType(oCompaniesDataTable.Rows(index)("company_id"), Byte())))
                End If
            Next
        End If

        Return oCompaniesArr
    End Function
    Public Shared Function GetSpUserClaims(ByVal userId As Guid, ByVal languageId As Guid, ByVal SpClaimCode As String) As SpUserClaims.SpUserClaimsDV
        Dim dal As New UserDAL
        Dim ds As DataSet

        ds = dal.LoadSpUserClaims(userId, languageId, SpClaimCode)
        Return New SpUserClaims.SpUserClaimsDV(ds.Tables(SpUserClaimsDAL.TABLE_NAME))
    End Function
    Public Function AddSecurityClaim(ByVal SpClaimTypeID As Guid, ByVal SpClaimValue As String, ByVal Effective As DateTime, ByVal Expiration As DateTime) As SpUserClaims
        Dim oSpUserClaims As SpUserClaims

        oSpUserClaims = New SpUserClaims(Me.Dataset)
        oSpUserClaims.UserId = Me.Id
        oSpUserClaims.SpClaimTypeId = SpClaimTypeID
        oSpUserClaims.SpClaimValue = SpClaimValue
        oSpUserClaims.EffectiveDate = Effective
        oSpUserClaims.ExpirationDate = Expiration
        Return oSpUserClaims
    End Function
    Public Function GetSecurityClaim(ByVal SpClaimsID As Guid) As SpUserClaims
        Dim oSpUserClaims As SpUserClaims
        oSpUserClaims = New SpUserClaims(SpClaimsID, Me.Dataset)
        Return oSpUserClaims
    End Function
    Public Function NeedPERMtoViewPrivacyData() As boolean
        Dim oRole As Role
        Dim rowRole as DataRowView        
        Dim authNeededRolePermId as Guid = LookupListNew.GetIdFromCode(LookupListNew.DropdownLanguageLookupList(LookupListNew.LK_USER_ROLE_PERMISSION, ElitaPlusIdentity.Current.ActiveUser.LanguageId),"AUTH_NEEDED_VIEW_SEC_FIELDS")
        dim dvRoles as dataview = GetUserRoles(New User(Authentication.CurrentUser.NetworkId).Id)
        for each rowRole In dvRoles
            oRole = New Role(New Guid(CType(rowRole("Id"), Byte())))
            If oRole.RolePermission.Where(Function(up) up.PermissionId = authNeededRolePermId).Any Then
                return True
            End If
        Next
        Return False
    End Function

#End Region

#Region "External User Methods"

    Public Shared Function GetExternalUserServiceCenters(ByVal oServiceCenterId As Guid) As DataView
        Dim dal As New UserDAL
        Dim ds As DataSet

        ds = dal.LoadExternalUserServiceCenters(oServiceCenterId)
        Return ds.Tables(UserDAL.TABLE_EXTERNAL_USER_SERVICE_CENTER).DefaultView
    End Function

    Public Shared Function GetExternalUserDealers(ByVal oDealerId As Guid) As DataView
        Return Nothing
    End Function

    Public Function DealerOrSvcList() As ArrayList
        Dim oDealerOrSvcDv As DataView
        If moServiceCenter_Or_DealerIDs Is Nothing Then
            If Me.IsServiceCenter = True Then
                oDealerOrSvcDv = GetExternalUserServiceCenters(Me.ScDealerId)
            ElseIf Me.IsDealer = True Then
                oDealerOrSvcDv = GetExternalUserDealers(Me.ScDealerId)
            Else
                Return Nothing
            End If

            moServiceCenter_Or_DealerIDs = New ArrayList

            If oDealerOrSvcDv.Table.Rows.Count > 0 Then
                Dim index As Integer
                ' Create Array
                For index = 0 To oDealerOrSvcDv.Table.Rows.Count - 1
                    If Not oDealerOrSvcDv.Table.Rows(index)(UserDAL.COL_NAME_DEALER_SERVICE_CENTER_ID) Is System.DBNull.Value Then
                        moServiceCenter_Or_DealerIDs.Add(New Guid(CType(oDealerOrSvcDv.Table.Rows(index)(UserDAL.COL_NAME_DEALER_SERVICE_CENTER_ID), Byte())))
                    End If
                Next
            End If
        End If
        Return moServiceCenter_Or_DealerIDs
    End Function

#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function GetUserNewList(ByVal NetworkIDMask As String, ByVal userNameMask As String, _
                                          ByVal roleMask As String, ByVal companyCodeMask As String _
                                          ) As UserSearchDV
        Try
            'Dim compIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Dim dal As New UserDAL
            Dim ds As DataSet
            Dim dv As DataView
            Dim filterCondition As String = ""
            Dim errors() As ValidationError = {New ValidationError(ElitaPlus.Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR, GetType(User), Nothing, "Search", Nothing)}

            'Convert the User Network ID to UPPER Case
            If (Not (NetworkIDMask.Equals(String.Empty))) Then
                NetworkIDMask = NetworkIDMask.ToUpper
            End If

            'Convert the User Name to UPPER Case
            If (Not (userNameMask.Equals(String.Empty))) Then
                userNameMask = userNameMask.ToUpper
            End If

            'Convert the Security Role to UPPER Case
            If (Not (roleMask.Equals(String.Empty))) Then
                roleMask = roleMask.ToUpper
                filterCondition &= dal.COL_NAME_ROLE_SEARCH_CODE & " like '%" & roleMask & "%'"
            End If

            'Convert the Company Code to UPPER Case
            If (Not (companyCodeMask.Equals(String.Empty))) Then
                companyCodeMask = companyCodeMask.ToUpper
                If filterCondition = "" Then
                    filterCondition &= dal.COL_NAME_COMPANY_SEARCH_CODE & " like '%" & companyCodeMask & "%'"
                Else
                    filterCondition &= "AND " & dal.COL_NAME_COMPANY_SEARCH_CODE & " like '%" & companyCodeMask & "%'"
                End If
            End If

            If (NetworkIDMask.Equals(String.Empty) AndAlso userNameMask.Equals(String.Empty) AndAlso _
                roleMask.Equals(String.Empty) AndAlso companyCodeMask.Equals(String.Empty)) Then
                Throw New BOValidationException(errors, GetType(User).FullName)
            End If

            dv = New UserSearchDV(dal.LoadUserList(NetworkIDMask, userNameMask, _
                                                   roleMask, companyCodeMask).Tables(0))

            If Not filterCondition = "" Then
                dv.RowFilter = filterCondition
            End If

            Return dv

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Class UserSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_USER_ID As String = UserDAL.COL_NAME_USER_ID
        Public Const COL_NETWORK_ID As String = UserDAL.COL_NAME_NETWORK_ID
        Public Const COL_USER_NAME As String = UserDAL.COL_NAME_USER_NAME
        Public Const COL_ROLE_CODE As String = UserDAL.COL_NAME_ROLE_SEARCH_CODE
        Public Const COL_ACTIVE As String = UserDAL.COL_NAME_ACTIVE
        Public Const COL_COMPANY_CODE As String = UserDAL.COL_NAME_COMPANY_SEARCH_CODE
#End Region

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property UserId(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_USER_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property UserName(ByVal row As DataRow) As String
            Get
                Return row(COL_USER_NAME).ToString
            End Get
        End Property

        Public Shared ReadOnly Property NetworkID(ByVal row As DataRow) As String
            Get
                Return row(COL_NETWORK_ID).ToString
            End Get
        End Property

        Public Shared ReadOnly Property IsActive(ByVal row As DataRow) As String
            Get
                Return row(COL_ACTIVE).ToString
            End Get
        End Property

    End Class

#End Region

#Region "Work Queue User integration"
    Public ReadOnly Property WorkQueueAssignChildren() As WorkQueueAssign.WorkQueueAssignList
        Get
            Return New WorkQueueAssign.WorkQueueAssignList(Me)
        End Get
    End Property

    Public Function GetNewWorkQueueAsignChild() As WorkQueueAssign
        Dim NewNotesList As WorkQueueAssign = CType(Me.WorkQueueAssignChildren.GetNewChild, WorkQueueAssign)
        NewNotesList.UserId = Me.Id
        Return NewNotesList
    End Function

    Public Function GetWQAssignSelectionView(CompanyId As Guid) As WorkQueueAssignSelectionView
        Dim t As DataTable = WorkQueueAssignSelectionView.CreateTable
        Dim detail As WorkQueueAssign

        Dim WQ_List As WrkQueue.WorkQueue() = WorkQueue.GetList()

        For Each detail In Me.WorkQueueAssignChildren
            If detail.CompanyId = CompanyId Then
                Dim row As DataRow = t.NewRow
                row(WorkQueueAssignSelectionView.COL_NAME_WORKQUEUE_ASSIGN_ID) = detail.Id.ToByteArray
                row(WorkQueueAssignSelectionView.COL_NAME_NAME_WORKQUEUE_ID) = detail.WorkqueueId.ToByteArray

                row(WorkQueueAssignSelectionView.COL_NAME_NAME_WORKQUEUE_DESC) = (From wq In WQ_List _
                    Where wq.Id = detail.WorkqueueId _
                    Select wq.Name).FirstOrDefault
                row(WorkQueueAssignSelectionView.COL_NAME_COMPANY_ID) = detail.CompanyId.ToByteArray
                row(WorkQueueAssignSelectionView.COL_NAME_USER_ID) = detail.UserId.ToByteArray
                t.Rows.Add(row)
            End If
        Next
        Return New WorkQueueAssignSelectionView(t)
    End Function

    Public Class WorkQueueAssignSelectionView
        Inherits DataView
        Public Const COL_NAME_WORKQUEUE_ASSIGN_ID As String = WorkQueueAssignDAL.COL_NAME_WORKQUEUE_ASSIGN_ID
        Public Const COL_NAME_NAME_WORKQUEUE_ID As String = WorkQueueAssignDAL.COL_NAME_WORKQUEUE_ID
        Public Const COL_NAME_NAME_WORKQUEUE_DESC As String = "DESCRIPTION"
        Public Const COL_NAME_COMPANY_ID As String = WorkQueueAssignDAL.COL_NAME_COMPANY_ID
        Public Const COL_NAME_USER_ID As String = WorkQueueAssignDAL.COL_NAME_USER_ID

        Public Sub New(ByVal Table As DataTable)
            MyBase.New(Table)
        End Sub

        Public Shared Function CreateTable() As DataTable
            Dim t As New DataTable
            t.Columns.Add(COL_NAME_WORKQUEUE_ASSIGN_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_NAME_WORKQUEUE_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_NAME_WORKQUEUE_DESC, GetType(String))
            t.Columns.Add(COL_NAME_COMPANY_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_USER_ID, GetType(Byte()))
            Return t
        End Function
    End Class

    Public Shared Function GetAvailableWorkQueueAssign(CompanyId As Guid) As WorkQueueAssignSelectionView

        Dim t As DataTable = WorkQueueAssignSelectionView.CreateTable
        Dim WQ_List As WrkQueue.WorkQueue()
        Dim CompCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_COMPANY, CompanyId)
        WQ_List = WorkQueue.GetList("*", CompCode, Nothing, Nothing, False)
        For Each WQ As WrkQueue.WorkQueue In WQ_List
            Dim row As DataRow = t.NewRow
            'row(WorkQueueAssignSelectionView.COL_NAME_WORKQUEUE_ASSIGN_ID) = 
            row(WorkQueueAssignSelectionView.COL_NAME_NAME_WORKQUEUE_ID) = WQ.Id.ToByteArray
            row(WorkQueueAssignSelectionView.COL_NAME_NAME_WORKQUEUE_DESC) = WQ.Name
            row(WorkQueueAssignSelectionView.COL_NAME_COMPANY_ID) = CompanyId.ToByteArray
            'row(WorkQueueAssignSelectionView.COL_NAME_USER_ID) =
            t.Rows.Add(row)
        Next
        Return New WorkQueueAssignSelectionView(t)
    End Function

    Public Function SaveWorkQueueUser(SelectedQueue As ArrayList) As Boolean
        Try
            'If SelectedQueue.Count = 0 Then Exit Function
            'compare with what we have and what is there in the user control
            'user control will always have the final selection so remove from our list what we don't find
            For Each WQ_user As WorkQueueAssign In Me.WorkQueueAssignChildren
                Dim dFound As Boolean = False
                For Each Str As String In SelectedQueue
                    Dim WorkQueue_id As Guid = New Guid(Str)
                    If WQ_user.WorkqueueId = WorkQueue_id And WQ_user.CompanyId = Me.SelectedCompanyId Then
                        dFound = True : Exit For
                    End If
                Next
                If Not dFound And WQ_user.CompanyId = Me.SelectedCompanyId Then
                    'Revoke Work queue permission - Process
                    WorkQueue.RevokeProcessWQPermission((New WorkQueue(WQ_user.WorkqueueId)).WorkQueue.Name, Me.NetworkId)
                    WQ_user.BeginEdit()
                    WQ_user.Delete()
                    WQ_user.EndEdit()
                    WQ_user.Save()
                End If
            Next

            'next now add those items which are there in user control but we don't have it
            For Each Str As String In SelectedQueue
                Dim dFound As Boolean = False
                For Each WQ_user As WorkQueueAssign In Me.WorkQueueAssignChildren
                    Dim WorkQueue_id As Guid = New Guid(Str)
                    If WQ_user.WorkqueueId = WorkQueue_id And WQ_user.CompanyId = Me.SelectedCompanyId Then
                        dFound = True : Exit For
                    End If
                Next
                If Not dFound Then
                    'Assign work queue permission - Process
                    WorkQueue.GrantProcessWQPermission(New WorkQueue(New Guid(Str)).WorkQueue.Name, Me.NetworkId)
                    Dim CompWQ As WorkQueueAssign = Me.GetNewWorkQueueAsignChild()
                    CompWQ.BeginEdit()
                    CompWQ.WorkqueueId = New Guid(Str)
                    CompWQ.CompanyId = SelectedCompanyId
                    CompWQ.EndEdit()
                    CompWQ.Save()
                End If
            Next
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Function

    Sub UpdateWorkQueueUserAssign()
        Try
            Dim WorkQueueAssDal As New WorkQueueAssignDAL
            WorkQueueAssDal.UpdateUserWorkQueue(Me.Dataset)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
    Public Function CheckWorkQueueAssigned(Comp_id As Guid) As Boolean
        For Each WQ_user As WorkQueueAssign In Me.WorkQueueAssignChildren
            If WQ_user.CompanyId = Comp_id Then
                Return False
            End If
        Next
        Return True
    End Function
#End Region

#Region "User Claim Validations"

    Public Shared Function IsDealerValidForUserClaim(ByVal userId As Guid, ByVal dealerCode As String) As Boolean
        Dim dal As New UserDAL
        
        return dal.IsDealerValidForUserClaim(userId, dealerCode)
        
    End Function

    Public Shared Function IsDealerGroupValidForUserClaim(ByVal userId As Guid, ByVal dealerGroupCode As String) As Boolean
        Dim dal As New UserDAL
        
        return dal.IsDealerGroupValidForUserClaim(userId, dealerGroupCode)
        
    End Function

    Public Shared Function IsCompanyGroupValidForUserClaim(ByVal userId As Guid, ByVal companyGroupCode As String) As Boolean
        Dim dal As New UserDAL

        return dal.IsCompanyGroupValidForUserClaim(userId, companyGroupCode)
        
    End Function

    Public Shared Function IsServiceCenterValidForUserClaim(ByVal userId As Guid, ByVal serviceCenterCode As String, countryCode As string) As Boolean
        Dim dal As New UserDAL

        return dal.IsServiceCenterValidForUserClaim(userId, serviceCenterCode, countryCode)
        
    End Function
#End Region

End Class

