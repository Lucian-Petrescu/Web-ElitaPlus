Public Class EquipmentImage
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
            Dim dal As New EquipmentImageDAL
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
            Dim dal As New EquipmentImageDAL
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
    Public ReadOnly Property Id As Guid
        Get
            CheckDeleted()
            If Row(EquipmentImageDAL.COL_NAME_EQUIPMENT_IMAGE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(EquipmentImageDAL.COL_NAME_EQUIPMENT_IMAGE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property EquipmentId As Guid
        Get
            CheckDeleted()
            If Row(EquipmentImageDAL.COL_NAME_EQUIPMENT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(EquipmentImageDAL.COL_NAME_EQUIPMENT_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EquipmentImageDAL.COL_NAME_EQUIPMENT_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property ImageTypeId As Guid
        Get
            CheckDeleted()
            If Row(EquipmentImageDAL.COL_NAME_IMAGE_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(EquipmentImageDAL.COL_NAME_IMAGE_TYPE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EquipmentImageDAL.COL_NAME_IMAGE_TYPE_ID, Value)
            'Set Image Type
            Dim dv As DataView = LookupListNew.GetImageTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId, False)
            Dim imageTypeDescription As String = LookupListNew.GetDescriptionFromId(dv, Value)
            SetValue(EquipmentImageDAL.COL_NAME_IMAGE_TYPE, imageTypeDescription)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property ImageType As String
        Get
            CheckDeleted()
            If Row(EquipmentImageDAL.COL_NAME_IMAGE_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return Row(EquipmentImageDAL.COL_NAME_IMAGE_TYPE).ToString()
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EquipmentImageDAL.COL_NAME_IMAGE_TYPE, Value)
        End Set
    End Property

    <ValueMandatory(""), CheckDuplicateCode("")> _
    Public Property Code As String
        Get
            CheckDeleted()
            If Row(EquipmentImageDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(EquipmentImageDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EquipmentImageDAL.COL_NAME_CODE, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property Description As String
        Get
            CheckDeleted()
            If Row(EquipmentImageDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(EquipmentImageDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EquipmentImageDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property

    <ValueMandatory(""), CheckDuplicatePath("")> _
    Public Property Path As String
        Get
            CheckDeleted()
            If Row(EquipmentImageDAL.COL_NAME_EQUIPMENT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(EquipmentImageDAL.COL_NAME_EQUIPMENT_ID), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EquipmentImageDAL.COL_NAME_EQUIPMENT_ID, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New EquipmentImageDAL
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

    Public Sub Copy(ByVal original As EquipmentImage)
        If Not IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Best Replacement.")
        End If
        MyBase.CopyFrom(original)
    End Sub
#End Region

#Region "Custom Validation"
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class CheckDuplicatePathAttribute
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Equipment.EQUIPMENT_FORM001)
        End Sub

        Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As EquipmentImage = CType(objectToValidate, EquipmentImage)
            If (obj.CheckDuplicatePath()) Then
                Return False
            Else
                Return True
            End If
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class CheckDuplicateCodeAttribute
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Equipment.EQUIPMENT_FORM002)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As EquipmentImage = CType(objectToValidate, EquipmentImage)
            If (obj.CheckDuplicateCode()) Then
                Return False
            Else
                Return True
            End If
        End Function
    End Class

    Protected Function CheckDuplicateCode() As Boolean
        Dim row As DataRow
        For Each row In Dataset.Tables(EquipmentImageDAL.TABLE_NAME).Rows
            If row.RowState <> DataRowState.Deleted And row.RowState <> DataRowState.Detached Then
                Dim bo As New EquipmentImage(row)
                ' Check if Code is Unique
                If Not bo.Id.Equals(Id) AndAlso Code = bo.Code Then
                    Return True
                End If
            End If
        Next
        Return False
    End Function

    Protected Function CheckDuplicatePath() As Boolean
        Dim row As DataRow
        For Each row In Dataset.Tables(EquipmentImageDAL.TABLE_NAME).Rows
            If row.RowState <> DataRowState.Deleted And row.RowState <> DataRowState.Detached Then
                Dim bo As New EquipmentImage(row)
                ' Check if Path is Unique
                If Not bo.Id.Equals(Id) AndAlso Path = bo.Path Then
                    Return True
                End If
            End If
        Next
        Return False
    End Function

#End Region

    Public Class EquipmentImageList
        Inherits BusinessObjectListBase

        Public Sub New(ByVal parent As Equipment)
            MyBase.New(LoadTable(parent), GetType(EquipmentImage), parent)
        End Sub

        Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
            Return CType(bo, EquipmentImage).EquipmentId.Equals(CType(Parent, Equipment).Id)
        End Function

        Private Shared Function LoadTable(ByVal parent As Equipment) As DataTable
            Try
                If Not parent.IsChildrenCollectionLoaded(GetType(EquipmentImageList)) Then
                    Dim dal As New EquipmentImageDAL
                    dal.LoadList(parent.Dataset, parent.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                    parent.AddChildrenCollection(GetType(EquipmentImageList))
                End If
                Return parent.Dataset.Tables(EquipmentImageDAL.TABLE_NAME)
            Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End Function

    End Class
End Class
