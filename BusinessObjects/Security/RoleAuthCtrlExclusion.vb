Public Class RoleAuthCtrlExclusion
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(id As Guid)
        MyBase.New()
        Dataset = New DataSet
        Load(id)
    End Sub

    'Exiting BO
    Public Sub New(oFormId As Guid, oRoleId As Guid, sControlName As String)
        MyBase.New()
        Dataset = New Dataset
        Load(oFormId, oRoleId, sControlName)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Dataset = New Dataset
        Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(id As Guid, familyDS As Dataset)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(familyDS As Dataset)
        MyBase.New(False)
        Dataset = familyDS
        Load()
    End Sub

    Public Sub New(row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New RoleAuthCtrlExclusionDAL
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
            Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            setvalue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(id As Guid)
        Try
            Dim dal As New RoleAuthCtrlExclusionDAL
            If _isDSCreator Then
                If Row IsNot Nothing Then
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

    Protected Sub Load(oFormId As Guid, oRoleId As Guid, sControlName As String)
        Try
            Dim dal As New RoleAuthCtrlExclusionDAL
            If _isDSCreator Then
                If Row IsNot Nothing Then
                    Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Row)
                End If
            End If
            Row = Nothing
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Row = FindRow(oFormId, dal.COL_NAME_FORM_ID, oRoleId, dal.COL_NAME_ROLE_ID, _
                            sControlName, dal.COL_NAME_CONTROL_NAME, _
                           Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.Load(Dataset, oFormId, oRoleId, sControlName)
                Row = FindRow(oFormId, dal.COL_NAME_FORM_ID, oRoleId, dal.COL_NAME_ROLE_ID, _
                            sControlName, dal.COL_NAME_CONTROL_NAME, _
                            Dataset.Tables(dal.TABLE_NAME))
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
    Private Sub Initialize()
    End Sub
#End Region


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid
        Get
            If row(RoleAuthCtrlExclusionDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(RoleAuthCtrlExclusionDAL.COL_NAME_AUTH_CTRL_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property RoleId As Guid
        Get
            CheckDeleted()
            If row(RoleAuthCtrlExclusionDAL.COL_NAME_ROLE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(RoleAuthCtrlExclusionDAL.COL_NAME_ROLE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(RoleAuthCtrlExclusionDAL.COL_NAME_ROLE_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property FormId As Guid
        Get
            CheckDeleted()
            If row(RoleAuthCtrlExclusionDAL.COL_NAME_FORM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(RoleAuthCtrlExclusionDAL.COL_NAME_FORM_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(RoleAuthCtrlExclusionDAL.COL_NAME_FORM_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=100)> _
    Public Property ControlName As String
        Get
            CheckDeleted()
            If row(RoleAuthCtrlExclusionDAL.COL_NAME_CONTROL_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(RoleAuthCtrlExclusionDAL.COL_NAME_CONTROL_NAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(RoleAuthCtrlExclusionDAL.COL_NAME_CONTROL_NAME, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=1)> _
    Public Property PermissionType As String
        Get
            CheckDeleted()
            If Row(RoleAuthCtrlExclusionDAL.COL_NAME_PERMISSION_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RoleAuthCtrlExclusionDAL.COL_NAME_PERMISSION_TYPE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(RoleAuthCtrlExclusionDAL.COL_NAME_PERMISSION_TYPE, Value)
        End Set
    End Property
#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New RoleAuthCtrlExclusionDAL
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
#End Region

#Region "DATA ACCESS ROUTINES"

    Public Shared Function PopulateList(oLanguageID As Guid) As DataView
        Dim oDs As Dataset

        Try
            Dim dal As New RoleAuthCtrlExclusionDAL

            oDs = dal.PopulateList(oLanguageID)
            Return oDs.Tables(RoleAuthCtrlExclusionDAL.TABLE_NAME).DefaultView
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function ObtainControlPermissions(sFormId As String, oControlNames As ArrayList) As DataView
        Dim oDv As DataView

        Try
            Dim dal As New RoleAuthCtrlExclusionDAL

            oDv = dal.ObtainControlPermissions(sFormId, oControlNames)
            Return oDv
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetControlPermissionList(sFormCode As String, oControlName As String) As DataView
        Try
            Dim dal As New RoleAuthCtrlExclusionDAL
            Return dal.GetControlPermissionAllRoles(sFormCode, oControlName).Tables(0).DefaultView
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#End Region

End Class
