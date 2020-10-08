Public Class StageEndStatus
    Inherits BusinessObjectBase

#Region "Constants"

#End Region

#Region "Constructors"

    'Exiting BO
    Public Sub New(id As Guid)
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
    Public Sub New(id As Guid, familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(familyDS As DataSet)
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
            Dim dal As New StageEndStatusDAL
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

    Protected Sub Load(id As Guid)
        Try
            Dim dal As New StageEndStatusDAL
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
    Private Sub Initialize()
    End Sub
#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid
        Get
            If Row(StageEndStatusDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(StageEndStatusDAL.COL_NAME_STAGE_END_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property StageId As Guid
        Get
            CheckDeleted()
            If Row(StageEndStatusDAL.COL_NAME_STAGE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(StageEndStatusDAL.COL_NAME_STAGE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(StageEndStatusDAL.COL_NAME_STAGE_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property EndStatusId As Guid
        Get
            CheckDeleted()
            If Row(StageEndStatusDAL.COL_NAME_END_STATUS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(StageEndStatusDAL.COL_NAME_END_STATUS_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(StageEndStatusDAL.COL_NAME_END_STATUS_ID, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New StageEndStatusDAL
                dal.Update(Row)
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then Load(Id)
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
#End Region

#Region "List Methods"
    Public Shared Sub LoadList(ds As DataSet, stageid As Guid, reloadData As Boolean, languageid As Guid)
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

    Public Shared Function Find(ds As DataSet, stageid As Guid, endStatusId As Guid) As StageEndStatus
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

    Public Shared Function Find(ds As DataSet, stageid As Guid) As StageEndStatus
        Dim row As DataRow = FindRow(stageid, StageEndStatusDAL.COL_NAME_STAGE_ID, ds.Tables(StageEndStatusDAL.TABLE_NAME))
        If row Is Nothing Then
            Return Nothing
        Else
            Return New StageEndStatus(row)
        End If
    End Function

    Public Shared Function GetSelectedStageEndStatus(ds As DataSet, stageid As Guid, languageid As Guid) As DataView
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


