Public Class RoleAuthCtrlExclusion
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load(id)
    End Sub

    'Exiting BO
    Public Sub New(ByVal oFormId As Guid, ByVal oRoleId As Guid, ByVal sControlName As String)
        MyBase.New()
        Me.Dataset = New Dataset
        Me.Load(oFormId, oRoleId, sControlName)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Me.Dataset = New Dataset
        Me.Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDS As Dataset)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As Dataset)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load()
    End Sub

    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Me.Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New RoleAuthCtrlExclusionDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            setvalue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New RoleAuthCtrlExclusionDAL
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

    Protected Sub Load(ByVal oFormId As Guid, ByVal oRoleId As Guid, ByVal sControlName As String)
        Try
            Dim dal As New RoleAuthCtrlExclusionDAL
            If Me._isDSCreator Then
                If Not Me.Row Is Nothing Then
                    Me.Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Me.Row)
                End If
            End If
            Me.Row = Nothing
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Me.Row = Me.FindRow(oFormId, dal.COL_NAME_FORM_ID, oRoleId, dal.COL_NAME_ROLE_ID, _
                            sControlName, dal.COL_NAME_CONTROL_NAME, _
                           Me.Dataset.Tables(dal.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.Load(Me.Dataset, oFormId, oRoleId, sControlName)
                Me.Row = Me.FindRow(oFormId, dal.COL_NAME_FORM_ID, oRoleId, dal.COL_NAME_ROLE_ID, _
                            sControlName, dal.COL_NAME_CONTROL_NAME, _
                            Me.Dataset.Tables(dal.TABLE_NAME))
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

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If row(RoleAuthCtrlExclusionDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(RoleAuthCtrlExclusionDAL.COL_NAME_AUTH_CTRL_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property RoleId() As Guid
        Get
            CheckDeleted()
            If row(RoleAuthCtrlExclusionDAL.COL_NAME_ROLE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(RoleAuthCtrlExclusionDAL.COL_NAME_ROLE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(RoleAuthCtrlExclusionDAL.COL_NAME_ROLE_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property FormId() As Guid
        Get
            CheckDeleted()
            If row(RoleAuthCtrlExclusionDAL.COL_NAME_FORM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(RoleAuthCtrlExclusionDAL.COL_NAME_FORM_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(RoleAuthCtrlExclusionDAL.COL_NAME_FORM_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=100)> _
    Public Property ControlName() As String
        Get
            CheckDeleted()
            If row(RoleAuthCtrlExclusionDAL.COL_NAME_CONTROL_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(RoleAuthCtrlExclusionDAL.COL_NAME_CONTROL_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(RoleAuthCtrlExclusionDAL.COL_NAME_CONTROL_NAME, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=1)> _
    Public Property PermissionType() As String
        Get
            CheckDeleted()
            If Row(RoleAuthCtrlExclusionDAL.COL_NAME_PERMISSION_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RoleAuthCtrlExclusionDAL.COL_NAME_PERMISSION_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(RoleAuthCtrlExclusionDAL.COL_NAME_PERMISSION_TYPE, Value)
        End Set
    End Property
#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New RoleAuthCtrlExclusionDAL
                dal.Update(Me.Row)
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
#End Region

#Region "DATA ACCESS ROUTINES"

    Public Shared Function PopulateList(ByVal oLanguageID As Guid) As DataView
        Dim oDs As Dataset

        Try
            Dim dal As New RoleAuthCtrlExclusionDAL

            oDs = dal.PopulateList(oLanguageID)
            Return oDs.Tables(RoleAuthCtrlExclusionDAL.TABLE_NAME).DefaultView
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function ObtainControlPermissions(ByVal sFormId As String, ByVal oControlNames As ArrayList) As DataView
        Dim oDv As DataView

        Try
            Dim dal As New RoleAuthCtrlExclusionDAL

            oDv = dal.ObtainControlPermissions(sFormId, oControlNames)
            Return oDv
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetControlPermissionList(ByVal sFormCode As String, ByVal oControlName As String) As DataView
        Try
            Dim dal As New RoleAuthCtrlExclusionDAL
            Return dal.GetControlPermissionAllRoles(sFormCode, oControlName).Tables(0).DefaultView
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#End Region

End Class
