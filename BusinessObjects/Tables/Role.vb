'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (3/13/2009)  ********************

Public Class Role
    Inherits BusinessObjectBase
    Implements IPermissionParent

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load(id)
        Me.RolePermission = New RolePermissionList(Me)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load()
        Me.RolePermission = New RolePermissionList(Me)
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDS As DataSet)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load(id)
        Me.RolePermission = New RolePermissionList(Me)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As DataSet)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load()
        Me.RolePermission = New RolePermissionList(Me)
    End Sub

    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Me.Dataset = row.Table.DataSet
        Me.Row = row
        Me.RolePermission = New RolePermissionList(Me)
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New RoleDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New RoleDAL
            If Me._isDSCreator Then
                If Not Me.Row Is Nothing Then
                    Me.Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Me.Row)
                End If
            End If
            Me.Row = Nothing
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
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
#End Region

#Region "Private Members"
    'Initialization code for new objects
    Private Sub Initialize()
    End Sub
#End Region


#Region "Properties"
    Public Property RolePermission As RolePermissionList

    'Key Property
    Public ReadOnly Property Id() As Guid Implements IPermissionParent.Id
        Get
            If Row(RoleDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(RoleDAL.COL_NAME_ROLE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=5)> _
    Public Property Code() As String
        Get
            CheckDeleted()
            If Row(RoleDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RoleDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(RoleDAL.COL_NAME_CODE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=280)> _
    Public Property Description() As String
        Get
            CheckDeleted()
            If Row(RoleDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RoleDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(RoleDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=4)> _
    Public Property IhqOnly() As String
        Get
            CheckDeleted()
            If Row(RoleDAL.COL_NAME_IHQ_ONLY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RoleDAL.COL_NAME_IHQ_ONLY), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(RoleDAL.COL_NAME_IHQ_ONLY, Value)
        End Set
    End Property

    Public Property RoleProviderId() As Guid
        Get
            CheckDeleted()
            If Row(RoleDAL.COL_NAME_ROLE_PROVIDER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(RoleDAL.COL_NAME_ROLE_PROVIDER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(RoleDAL.COL_NAME_ROLE_PROVIDER_ID, Value)
        End Set
    End Property

    'Changes for DEF : 3119
    Public ReadOnly Property RemoteRoleId() As Guid
        Get
            CheckDeleted()
            ' As per DEF 3119, Commented Existing logic. 
            'If Row(RoleDAL.COL_NAME_REMOTE_ROLE_ID) Is DBNull.Value Then
            '    Return Nothing
            'Else
            '    Return New Guid(CType(Row(RoleDAL.COL_NAME_REMOTE_ROLE_ID), Byte()))
            'End If
            ' Commenting Completed : DEF : 3119

            ' Get RoleID from AuthorizationService Role Provider 
            Dim remoteRoleProvider As IRemoteRoleProvider
            If (Not Me.RoleProviderId.Equals(Guid.Empty)) Then
                remoteRoleProvider = BaseRoleProvider.CreateRoleProvider(Me.RoleProviderId)

                'For Getting ID in Remote Service need to pass Original Code else no information will be available 
                Dim originalRoleCode As String = CType(Me.Row(RoleDAL.COL_NAME_CODE, DataRowVersion.Original), String)

                If (Not remoteRoleProvider Is Nothing AndAlso Not String.IsNullOrEmpty(originalRoleCode.Trim())) Then
                    Return remoteRoleProvider.GetRoleId(originalRoleCode.Trim())
                End If

            End If

            Return Guid.Empty

        End Get

        ' Changes for DEF: 3119 (Set this property as readonly)
        'Set(ByVal Value As Guid)
        '    CheckDeleted()
        '    Me.SetValue(RoleDAL.COL_NAME_REMOTE_ROLE_ID, Value)
        'End Set

    End Property
    'End of change DEF:3119


#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsFamilyDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New RoleDAL
                ' Call Web Service to Manage Role on Remote System
                If Assurant.Elita.Configuration.ElitaConfig.Current.General.IntegrateWorkQueueImagingServices = True Then
                    UpdateRemoteRole()
                End If
                dal.UpdateFamily(Me.Dataset)
                    'Reload the Data from the DB
                    If Me.Row.RowState <> DataRowState.Detached Then
                        Dim objId As Guid = Me.Id
                        Me.Dataset = New DataSet
                        Me.Row = Nothing
                        Me.Load(objId)
                    End If
                End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    Private Sub UpdateRemoteRole()
        Dim remoteRoleProvider As IRemoteRoleProvider
        Select Case Me.Row.RowState
            Case DataRowState.Added
                ' Check if Remote Provider is Selected, if not clear remote role ID
                If (Me.RoleProviderId.Equals(Guid.Empty)) Then
                    ' DEF  :3119 (Commented as this property is readonly now)
                    'Me.RemoteRoleId = Guid.Empty
                    'DEF:3119 : Change completed 
                    Exit Sub
                End If

                If (Not Me.RoleProviderId.Equals(Guid.Empty)) Then
                    remoteRoleProvider = BaseRoleProvider.CreateRoleProvider(Me.RoleProviderId)
                End If


                If (remoteRoleProvider.SupportsCreateRole()) Then
                    ' DEF  :3119 (Commented as this property is readonly now)
                    '    Me.RemoteRoleId = remoteRoleProvider.CreateRole(Me.Id, Me.Code)
                    'DEF:3119 : Change completed 
                    remoteRoleProvider.CreateRole(Me.Id, Me.Code)   'Add new role to Remote system 
                End If


            Case DataRowState.Deleted
                ' Check if Remote Provider is in original version, if not clear remote role ID in original record
                Dim roleProviderId As Guid = Guid.Empty
                If (Not Me.Row.Item(RoleDAL.COL_NAME_ROLE_PROVIDER_ID, DataRowVersion.Original) Is DBNull.Value) Then
                    roleProviderId = New Guid(CType(Me.Row.Item(RoleDAL.COL_NAME_ROLE_PROVIDER_ID, DataRowVersion.Original), Byte()))
                End If
                If (roleProviderId.Equals(Guid.Empty)) Then
                    Exit Sub
                End If
                remoteRoleProvider = BaseRoleProvider.CreateRoleProvider(roleProviderId)
                If (remoteRoleProvider.SupportsDeleteRole()) Then
                    ' Get Remote Role ID (Changes for DEF-3119)
                    'remoteRoleProvider.DeleteRole(New Guid(CType(Me.Row(RoleDAL.COL_NAME_ROLE_ID, DataRowVersion.Original), Byte())), New Guid(CType(Me.Row(RoleDAL.COL_NAME_REMOTE_ROLE_ID, DataRowVersion.Original), Byte())), CType(Me.Row(RoleDAL.COL_NAME_CODE, DataRowVersion.Original), String))
                    'Get GUID from Webservice (RemoID property cannot be called as it first checks Deleted Row status ans from there throws exception)
                    Dim remoteRoleID As Guid = remoteRoleProvider.GetRoleId(Me.Row(RoleDAL.COL_NAME_CODE, DataRowVersion.Original))
                    remoteRoleProvider.DeleteRole(New Guid(CType(Me.Row(RoleDAL.COL_NAME_ROLE_ID, DataRowVersion.Original), Byte())), remoteRoleID, CType(Me.Row(RoleDAL.COL_NAME_CODE, DataRowVersion.Original), String))
                    'Changes for DEF-3119 ended 
                End If
            Case DataRowState.Modified
                ' Check if Remote Provider is in original version, if not clear remote role ID in original record
                Dim originalRoleProviderId As Guid
                Dim currentRoleProviderId As Guid
                If Row(RoleDAL.COL_NAME_ROLE_PROVIDER_ID, DataRowVersion.Original) Is DBNull.Value Then
                    originalRoleProviderId = Guid.Empty
                Else
                    originalRoleProviderId = New Guid(CType(Me.Row.Item(RoleDAL.COL_NAME_ROLE_PROVIDER_ID, DataRowVersion.Original), Byte()))
                End If
                currentRoleProviderId = Me.RoleProviderId
                ' Check if Role Provider has changed
                If (originalRoleProviderId.Equals(currentRoleProviderId)) Then


                    If (Me.RoleProviderId.Equals(Guid.Empty)) Then
                        ' DEF  :3119 (Commented as this property is readonly now)
                        'Me.RemoteRoleId = Guid.Empty
                        ' DEF  :3119 Changes completed 
                        Exit Sub
                    End If


                    ' Check if Code has changed
                    If (CType(Me.Row(RoleDAL.COL_NAME_CODE, DataRowVersion.Current), String) <> CType(Me.Row(RoleDAL.COL_NAME_CODE, DataRowVersion.Original), String)) Then
                        remoteRoleProvider = BaseRoleProvider.CreateRoleProvider(RoleProviderId)
                        If (remoteRoleProvider.SupportsUpdateRole()) Then
                            remoteRoleProvider.UpdateRole(Me.Id, Me.RemoteRoleId, Me.Code)
                        End If
                    End If
                Else
                    Dim originalRoleProvider As IRemoteRoleProvider
                    Dim currentRoleProvider As IRemoteRoleProvider
                    If (Not originalRoleProviderId.Equals(Guid.Empty)) Then
                        originalRoleProvider = BaseRoleProvider.CreateRoleProvider(originalRoleProviderId)
                        If (originalRoleProvider.SupportsCreateRole()) Then
                            'Changed as per DEF : 3119
                            'originalRoleProvider.DeleteRole(Me.Id, New Guid(CType(Me.Row(RoleDAL.COL_NAME_REMOTE_ROLE_ID, DataRowVersion.Original), Byte())), CType(Me.Row(RoleDAL.COL_NAME_CODE, DataRowVersion.Original), String))
                            originalRoleProvider.DeleteRole(Me.Id, Me.RemoteRoleId, CType(Me.Row(RoleDAL.COL_NAME_CODE, DataRowVersion.Original), String))
                            'DEF-3119 Completed 
                        End If
                    End If
                    If (Not currentRoleProviderId.Equals(Guid.Empty)) Then
                        currentRoleProvider = BaseRoleProvider.CreateRoleProvider(currentRoleProviderId)

                        If (currentRoleProvider.SupportsCreateRole()) Then
                            ' DEF  :3119 (Commented as this property is readonly now)
                            '    Me.RemoteRoleId = currentRoleProvider.CreateRole(Me.Id, Me.Code)
                            currentRoleProvider.CreateRole(Me.Id, Me.Code)
                            ' DEF  :3119 Changes completed 
                        End If
                        ' DEF  :3119 (Commented as this property is readonly now)
                        'Else
                        '    Me.RemoteRoleId = Guid.Empty
                        ' DEF  :3119 Changes completed 
                    End If
                End If
        End Select
    End Sub
#End Region

#Region "DataView Retrieveing Methods"

    Public Shared Sub AddNewRowToRoleSearchDV(ByRef dv As RoleSearchDV, ByVal NewRoleBO As Role)
        Dim dt As DataTable, blnEmptyTbl As Boolean = False

        If NewRoleBO.IsNew Then
            Dim row As DataRow
            If dv Is Nothing Then
                Dim guidTemp As New Guid
                blnEmptyTbl = True
                dt = New DataTable
                dt.Columns.Add(RoleSearchDV.COL_NAME_ROLE_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(RoleSearchDV.COL_NAME_CODE, GetType(String))
                dt.Columns.Add(RoleSearchDV.COL_NAME_DESCRIPTION, GetType(String))
                dt.Columns.Add(RoleSearchDV.COL_NAME_IHQ_ONLY, GetType(String))
                dt.Columns.Add(RoleSearchDV.COL_NAME_ROLE_PROVIDER_ID, guidTemp.ToByteArray.GetType)
                ' dt.Columns.Add(RoleSearchDV.COL_NAME_REMOTE_ROLE_ID, guidTemp.ToByteArray.GetType)  : DEF-3119 (Removed from DB)
            Else
                dt = dv.Table
            End If
            row = dt.NewRow
            row(RoleSearchDV.COL_NAME_ROLE_ID) = NewRoleBO.Id.ToByteArray
            row(RoleSearchDV.COL_NAME_CODE) = NewRoleBO.Code
            row(RoleSearchDV.COL_NAME_DESCRIPTION) = NewRoleBO.Description
            row(RoleSearchDV.COL_NAME_IHQ_ONLY) = NewRoleBO.IhqOnly
            row(RoleSearchDV.COL_NAME_ROLE_PROVIDER_ID) = NewRoleBO.RoleProviderId.ToByteArray
            ' row(RoleSearchDV.COL_NAME_REMOTE_ROLE_ID) = NewRoleBO.RemoteRoleId.ToByteArray  : DEF-3119 (Removed from DB)
            dt.Rows.Add(row)
            If blnEmptyTbl Then dv = New RoleSearchDV(dt)
        End If
    End Sub

    Public Shared Function getList(ByVal strCode As String, ByVal strDescription As String) As RoleSearchDV
        Try
            Dim dal As New RoleDAL
            Return New RoleSearchDV(dal.LoadList(strCode, strDescription).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function


    Public Shared Function GetRolesList() As DataView
        Try
            Dim dal As New RoleDAL
            Dim ds As DataSet

            ds = dal.LoadList(String.Empty, String.Empty)
            Return (ds.Tables(RoleDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function
#End Region

#Region "RouteSearchDV"

    Public Class RoleSearchDV
        Inherits DataView

#Region "Constants"

        Public Const COL_NAME_ROLE_ID As String = RoleDAL.COL_NAME_ROLE_ID
        Public Const COL_NAME_DESCRIPTION As String = RoleDAL.COL_NAME_DESCRIPTION
        Public Const COL_NAME_CODE As String = RoleDAL.COL_NAME_CODE
        Public Const COL_NAME_IHQ_ONLY As String = RoleDAL.COL_NAME_IHQ_ONLY
        Public Const COL_NAME_ROLE_PROVIDER_ID As String = RoleDAL.COL_NAME_ROLE_PROVIDER_ID
        ' Public Const COL_NAME_REMOTE_ROLE_ID As String = RoleDAL.COL_NAME_REMOTE_ROLE_ID  : DEF 3119 : (Removed from Database)
#End Region

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property RoleId(ByVal row As DataRowView) As String
            Get
                Return New Guid(CType(row(COL_NAME_ROLE_ID), Byte())).ToString()
            End Get
        End Property

        Public Shared ReadOnly Property IhqOnly(ByVal row As DataRowView) As String
            Get
                Dim languageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                Dim dvYesNo As DataView = LookupListNew.GetYesNoLookupList(languageId)
                Return LookupListNew.GetDescriptionFromCode(dvYesNo, row(COL_NAME_IHQ_ONLY).ToString)
            End Get
        End Property

        Public Shared ReadOnly Property RoleProvider(ByVal row As DataRowView) As String
            Get
                If row(COL_NAME_ROLE_PROVIDER_ID) Is DBNull.Value Then
                    Return String.Empty
                End If

                Dim languageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                Dim dvRoleProvider As DataView = LookupListNew.GetRoleProviderList(languageId)
                Return LookupListNew.GetDescriptionFromId(dvRoleProvider, New Guid(CType(row(COL_NAME_ROLE_PROVIDER_ID), Byte())))
            End Get
        End Property


    End Class
#End Region

End Class




