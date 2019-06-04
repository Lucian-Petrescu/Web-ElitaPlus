Public Class StageEndStatus
    Inherits BusinessObjectBase

#Region "Constants"

#End Region

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load(id)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDS As DataSet)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As DataSet)
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
            Dim dal As New StageEndStatusDAL
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
            Dim dal As New StageEndStatusDAL
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

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If Row(StageEndStatusDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(StageEndStatusDAL.COL_NAME_STAGE_END_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property StageId() As Guid
        Get
            CheckDeleted()
            If Row(StageEndStatusDAL.COL_NAME_STAGE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(StageEndStatusDAL.COL_NAME_STAGE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(StageEndStatusDAL.COL_NAME_STAGE_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property EndStatusId() As Guid
        Get
            CheckDeleted()
            If Row(StageEndStatusDAL.COL_NAME_END_STATUS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(StageEndStatusDAL.COL_NAME_END_STATUS_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(StageEndStatusDAL.COL_NAME_END_STATUS_ID, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New StageEndStatusDAL
                dal.Update(Me.Row)
                'Reload the Data from the DB
                If Me.Row.RowState <> DataRowState.Detached Then Me.Load(Me.Id)
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
#End Region

#Region "List Methods"
    Public Shared Sub LoadList(ByVal ds As DataSet, ByVal stageid As Guid, ByVal reloadData As Boolean, ByVal languageid As Guid)
        Try
            If reloadData Then
                Dim tableIdx As Integer = ds.Tables.IndexOf(StageEndStatusDAL.TABLE_NAME)
                If tableIdx <> -1 Then
                    ds.Tables.Remove(StageEndStatusDAL.TABLE_NAME)
                End If
            End If
            Dim _stageendstatusDAL As New StageEndStatusDAL
            If ds.Tables.IndexOf(StageEndStatusDAL.TABLE_NAME) < 0 Then
                _stageendstatusDAL.LoadList(ds, stageid, languageid)
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Shared Function Find(ByVal ds As DataSet, ByVal stageid As Guid, ByVal endStatusId As Guid) As StageEndStatus
        Dim i As Integer
        For i = 0 To ds.Tables(StageEndStatusDAL.TABLE_NAME).Rows.Count - 1
            Dim row As DataRow = ds.Tables(StageEndStatusDAL.TABLE_NAME).Rows(i)
            If Not (row.RowState = DataRowState.Deleted Or row.RowState = DataRowState.Detached) Then
                Dim stgEndStatus As New StageEndStatus(row)
                If stgEndStatus.StageId.Equals(stageid) AndAlso stgEndStatus.EndStatusId.Equals(endStatusId) Then
                    Return stgEndStatus
                End If
            End If
        Next
        Return Nothing
    End Function

    Public Shared Function Find(ByVal ds As DataSet, ByVal stageid As Guid) As StageEndStatus
        Dim row As DataRow = FindRow(stageid, StageEndStatusDAL.COL_NAME_STAGE_ID, ds.Tables(StageEndStatusDAL.TABLE_NAME))
        If row Is Nothing Then
            Return Nothing
        Else
            Return New StageEndStatus(row)
        End If
    End Function

    Public Shared Function GetSelectedStageEndStatus(ByVal ds As DataSet, ByVal stageid As Guid, ByVal languageid As Guid) As DataView
        Dim _stageendstatusDAL As New StageEndStatusDAL
        Return _stageendstatusDAL.GetSelectedStageEndStatus(ds, stageid, languageid)
    End Function

    'Public Shared Function GetAvailableStageEndStatus(ByVal ds As DataSet, ByVal companygroupid As Guid) As DataView
    '    Return GetFilteredPostalCodeFormat(ds, companygroupid, False)
    'End Function

#End Region

#Region "DataView Retrieveing Methods"

#End Region

End Class


