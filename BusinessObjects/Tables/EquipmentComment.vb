Public Class EquipmentComment
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
            Dim dal As New EquipmentCommentDAL
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
            Dim dal As New EquipmentCommentDAL
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
    <ValueMandatory("")> _
    Public ReadOnly Property Id() As Guid
        Get
            CheckDeleted()
            If Row(EquipmentCommentDAL.COL_NAME_EQUIPMENT_COMMENT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(EquipmentCommentDAL.COL_NAME_EQUIPMENT_COMMENT_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property EquipmentId() As Guid
        Get
            CheckDeleted()
            If Row(EquipmentCommentDAL.COL_NAME_EQUIPMENT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(EquipmentCommentDAL.COL_NAME_EQUIPMENT_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(EquipmentCommentDAL.COL_NAME_EQUIPMENT_ID, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=4000)> _
    Public Property Comment() As String
        Get
            CheckDeleted()
            If Row(EquipmentCommentDAL.COL_NAME_COMMENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return Row(EquipmentCommentDAL.COL_NAME_COMMENT).ToString()
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(EquipmentCommentDAL.COL_NAME_COMMENT, Value)
        End Set
    End Property
#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New EquipmentCommentDAL
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

    Public Sub Copy(ByVal original As EquipmentComment)
        If Not IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Best Replacement.")
        End If
        MyBase.CopyFrom(original)
    End Sub
#End Region

    Public Class EquipmentCommentList
        Inherits BusinessObjectListBase

        Public Sub New(ByVal parent As Equipment)
            MyBase.New(LoadTable(parent), GetType(EquipmentComment), parent)
        End Sub

        Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
            Return CType(bo, EquipmentComment).EquipmentId.Equals(CType(Parent, Equipment).Id)
        End Function

        Private Shared Function LoadTable(ByVal parent As Equipment) As DataTable
            Try
                If Not parent.IsChildrenCollectionLoaded(GetType(EquipmentCommentList)) Then
                    Dim dal As New EquipmentCommentDAL
                    dal.LoadList(parent.Dataset, parent.Id)
                    parent.AddChildrenCollection(GetType(EquipmentCommentList))
                End If
                Return parent.Dataset.Tables(EquipmentCommentDAL.TABLE_NAME)
            Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End Function

    End Class

End Class
