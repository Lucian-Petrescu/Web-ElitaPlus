Imports System.Linq

Public MustInherit Class BasePermission(Of TDataAccessType As {BasePermissionDAL(Of TDataAccessType), New}, TPermission As BasePermission(Of TDataAccessType, TPermission))
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Dataset = New DataSet
        Load(id)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Dataset = New DataSet
        Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load()
    End Sub

    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New TDataAccessType
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
            Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New TDataAccessType
            If _isDSCreator Then
                If Not Row Is Nothing Then
                    Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Row)
                End If
            End If
            Row = Nothing
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Row = FindRow(id, dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.Load(Dataset, id)
                Row = FindRow(id, dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then
                Throw New DataNotFoundException
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
#End Region

#Region "Private Members"
    'Initialization code for new objects
    Protected Overridable Sub Initialize()
    End Sub

    Private dataAccessObject As TDataAccessType = New TDataAccessType()

#End Region

#Region "Properties"
    <ValueMandatory("")> _
    Public ReadOnly Property Id() As Guid
        Get
            CheckDeleted()
            If Row(dataAccessObject.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(dataAccessObject.TABLE_KEY_NAME), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property PermissionId() As Guid
        Get
            CheckDeleted()
            If Row(dataAccessObject.COL_NAME_PERMISSION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(dataAccessObject.COL_NAME_PERMISSION_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(dataAccessObject.COL_NAME_PERMISSION_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property EntityId() As Guid
        Get
            CheckDeleted()
            If Row(dataAccessObject.COL_NAME_ENTITY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(dataAccessObject.COL_NAME_ENTITY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(dataAccessObject.COL_NAME_ENTITY_ID, Value)
        End Set
    End Property
#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New TDataAccessType
                dal.Update(Row)
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Id
                    Dataset = New DataSet
                    Row = Nothing
                    Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    Public Sub Copy(ByVal original As TPermission)
        If Not IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Best Replacement.")
        End If
        MyBase.CopyFrom(original)
    End Sub
#End Region

End Class

Public MustInherit Class PermissionList(Of _
                                          TParentType As {BusinessObjectBase, IPermissionParent}, _
                                          TDataAccessType As {BasePermissionDAL(Of TDataAccessType), New}, _
                                          TPermission As BasePermission(Of TDataAccessType, TPermission),
                                          TPermissionList As PermissionList(Of TParentType, TDataAccessType, TPermission, TPermissionList))
    Inherits BusinessObjectListEnumerableBase(Of TParentType, TPermission)

    Private ReadOnly dvPermissions As DataView

    Public Sub New(ByVal parent As TParentType)
        MyBase.New(LoadTable(Of TPermissionList)(parent), parent)
        If (ElitaPlusIdentity.Current Is Nothing) OrElse (ElitaPlusIdentity.Current.ActiveUser Is Nothing) Then
            dvPermissions = LookupListNew.DropdownLanguageLookupList(LookupListNew.LK_USER_ROLE_PERMISSION, CType(CType(parent, Object), User).LanguageId)
        Else
            dvPermissions = LookupListNew.DropdownLanguageLookupList(LookupListNew.LK_USER_ROLE_PERMISSION, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
        End If


    End Sub

    Public Overrides Function GetChild(ByVal childId As System.Guid) As BusinessObjectBase
        Return (From permission As TPermission In Me
                Where permission.PermissionId = childId
                Select permission).FirstOrDefault()
    End Function

    Public MustOverride Function GetNewChild(ByVal parentId As System.Guid) As BusinessObjectBase

    Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
        Return CType(bo, TPermission).EntityId.Equals(CType(Parent, TParentType).Id)
    End Function

    Private Shared Function LoadTable(Of TPermissionList)(ByVal parent As TParentType) As DataTable
        Try
            Dim dal As New TDataAccessType
            If Not parent.IsChildrenCollectionLoaded(GetType(TPermissionList)) Then
                dal.LoadList(parent.Dataset, parent.Id)
                parent.AddChildrenCollection(GetType(TPermissionList))
            End If
            Return parent.Dataset.Tables(dal.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Sub Grant(ByVal permissionId As Guid)
        Dim permission As TPermission = GetNewChild(DirectCast(MyBase.Parent, TParentType).Id)
        permission.PermissionId = permissionId
        permission.Save()
    End Sub

    Public Function HasPermission(ByVal permissionCode As String) As Boolean
        Dim result As DataTable = dvPermissions.Table.Copy()
        Dim permissionId As Nullable(Of Guid) = Nothing
        permissionCode = permissionCode.ToUpperInvariant()

        For index = result.Rows.Count - 1 To 0 Step -1
            Dim dr As DataRow = result(index)
            Dim code As String = CType(dr(DALBase.COL_NAME_CODE), String)
            If (permissionCode = code.ToUpperInvariant()) Then
                permissionId = New Guid(CType(dr(DALBase.COL_NAME_ID), Byte()))
            End If
        Next

        If (Not permissionId.HasValue) Then Return False

        Dim permission As TPermission = (From p As TPermission In Me
                                         Where p.PermissionId = permissionId.Value
                                         Select p).FirstOrDefault()

        Return Not (permission Is Nothing)

    End Function

    Public Sub Revoke(ByVal permissionId As Guid)
        Dim permission As TPermission = (From p As TPermission In Me
                                         Where p.PermissionId = permissionId
                                         Select p).FirstOrDefault()

        If (Not (permission Is Nothing)) Then
            permission.Delete()
            permission.Save()
        End If
    End Sub

    Public Sub RevokeAll()
        For index = Count - 1 To 0 Step -1
            Dim p As TPermission = Me(index)
            p.Delete()
            p.Save()
        Next

    End Sub

    Public Function GetAvailablePermissions() As DataView
        Dim selectedPermissions As Guid() = (From p As TPermission In Me
                                             Select p.PermissionId).ToArray()

        Dim result As DataTable = dvPermissions.Table.Copy()

        For index = result.Rows.Count - 1 To 0 Step -1
            Dim dr As DataRow = result(index)
            Dim permissionId As Guid = New Guid(CType(dr(DALBase.COL_NAME_ID), Byte()))
            If (selectedPermissions.Contains(permissionId)) Then
                dr.Delete()
            End If
        Next

        Return New DataView(result)
    End Function

    Public Function GetSelectedPermissions() As DataView
        Dim selectedPermissions As Guid() = (From p As TPermission In Me
                                             Select p.PermissionId).ToArray()

        Dim result As DataTable = dvPermissions.Table.Copy()

        For index = result.Rows.Count - 1 To 0 Step -1
            Dim dr As DataRow = result(index)
            Dim permissionId As Guid = New Guid(CType(dr(DALBase.COL_NAME_ID), Byte()))
            If (Not selectedPermissions.Contains(permissionId)) Then
                dr.Delete()
            End If
        Next

        Return New DataView(result)
    End Function

End Class

Public Interface IPermissionParent
    ReadOnly Property Id As Guid
End Interface
