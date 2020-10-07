'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (9/24/2012)  ********************

Public Class ClaimImage
    Inherits BusinessObjectBase

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
    Public Sub New(familyDS As DataSet, parentId As Guid)
        MyBase.New(False)
        Dataset = familyDS
        Load()
        Initialize(parentId)
    End Sub

    Public Sub New(row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New ClaimImageDAL
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
            Dim dal As New ClaimImageDAL
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
    Private Sub Initialize(parentId As Guid)
        ClaimId = parentId
        IsLocalRepository = "N"
    End Sub

    'Initialization code for new objects
    Private Sub Initialize()
    End Sub
#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid
        Get
            If row(ClaimImageDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimImageDAL.COL_NAME_CLAIM_IMAGE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property ClaimId As Guid
        Get
            CheckDeleted()
            If row(ClaimImageDAL.COL_NAME_CLAIM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimImageDAL.COL_NAME_CLAIM_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimImageDAL.COL_NAME_CLAIM_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ImageId As Guid
        Get
            CheckDeleted()
            If row(ClaimImageDAL.COL_NAME_IMAGE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimImageDAL.COL_NAME_IMAGE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimImageDAL.COL_NAME_IMAGE_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property DocumentTypeId As Guid
        Get
            CheckDeleted()
            If row(ClaimImageDAL.COL_NAME_DOCUMENT_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimImageDAL.COL_NAME_DOCUMENT_TYPE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimImageDAL.COL_NAME_DOCUMENT_TYPE_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ImageStatusId As Guid
        Get
            CheckDeleted()
            If Row(ClaimImageDAL.COL_NAME_IMAGE_STATUS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimImageDAL.COL_NAME_IMAGE_STATUS_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimImageDAL.COL_NAME_IMAGE_STATUS_ID, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=1)> _
    Public Property IsLocalRepository As String
        Get
            CheckDeleted()
            If Row(ClaimImageDAL.COL_NAME_IS_LOCAL_REPOSITORY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimImageDAL.COL_NAME_IS_LOCAL_REPOSITORY), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimImageDAL.COL_NAME_IS_LOCAL_REPOSITORY, Value)
        End Set
    End Property

    Public Property ScanDate As DateType
        Get
            CheckDeleted()
            If Row(ClaimImageDAL.COL_NAME_SCAN_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ClaimImageDAL.COL_NAME_SCAN_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimImageDAL.COL_NAME_SCAN_DATE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=200), ValidateDuplicateFileName("")> _
    Public Property FileName As String
        Get
            CheckDeleted()
            If Row(ClaimImageDAL.COL_NAME_FILE_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimImageDAL.COL_NAME_FILE_NAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimImageDAL.COL_NAME_FILE_NAME, Value)
        End Set
    End Property

    Public Property FileSizeBytes As LongType
        Get
            CheckDeleted()
            If Row(ClaimImageDAL.COL_NAME_FILE_SIZE_BYTES) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ClaimImageDAL.COL_NAME_FILE_SIZE_BYTES), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimImageDAL.COL_NAME_FILE_SIZE_BYTES, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=4000)> _
    Public Property Comments As String
        Get
            CheckDeleted()
            If Row(ClaimImageDAL.COL_NAME_COMMENTS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimImageDAL.COL_NAME_COMMENTS), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimImageDAL.COL_NAME_COMMENTS, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=100)> _
    Public Property UserName As String
        Get
            CheckDeleted()
            If Row(ClaimImageDAL.COL_NAME_USER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimImageDAL.COL_NAME_USER_NAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimImageDAL.COL_NAME_USER_NAME, Value)
        End Set
    End Property

    Public Property DeleteFlag As String
        Get
            CheckDeleted()
            If Row(ClaimImageDAL.COL_NAME_DELETE_FLAG) Is DBNull.Value Then
                Return "N"
            Else
                Return CType(Row(ClaimImageDAL.COL_NAME_DELETE_FLAG), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimImageDAL.COL_NAME_DELETE_FLAG, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()            
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ClaimImageDAL
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

     Public Sub UpdateDocumentDeleteFlag(modifiedById As String)
        Try
            Dim dal As New ClaimImageDAL
            dal.UpdateDocumentDeleteFlag(imageId, deleteFlag, modifiedById)                
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
    
#End Region


#Region "Claim Image List Selection View"
    Public Class ClaimImagesList
        Inherits BusinessObjectListBase

        Public Sub New(parent As ClaimBase, Optional ByVal loadAllFiles As Boolean = False)
            MyBase.New(LoadTable(parent, loadAllFiles), GetType(ClaimImage), parent)
        End Sub

        Public Overrides Function Belong(bo As BusinessObjectBase) As Boolean
            Return CType(bo, ClaimImage).ClaimId.Equals(CType(Parent, ClaimBase).Id)
        End Function

        Private Shared Function LoadTable(parent As ClaimBase, Optional ByVal loadAllFiles As Boolean = False) As DataTable
            Try
                If Not parent.IsChildrenCollectionLoaded(GetType(ClaimImagesList)) Then
                    Dim dal As New ClaimImageDAL
                    dal.LoadList(parent.Dataset, parent.Id, loadAllFiles)
                    parent.AddChildrenCollection(GetType(ClaimImagesList))
                End If
                Return parent.Dataset.Tables(ClaimImageDAL.TABLE_NAME)
            Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End Function

    End Class
#End Region

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidateDuplicateFileNameAttribute
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.DUPLICATE_FILE_NAME)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As ClaimImage = CType(objectToValidate, ClaimImage)

            For Each dr As DataRow In obj.Row.Table.Rows
                Try

                    If (New Guid(CType(dr(ClaimImageDAL.COL_NAME_CLAIM_IMAGE_ID), Byte())) <> obj.Id) AndAlso _
                        (CType(dr(ClaimImageDAL.COL_NAME_FILE_NAME), String).ToUpperInvariant() = obj.FileName.ToUpperInvariant()) Then
                        Return False
                    End If
                Catch ex As Exception
                    Return False
                End Try

            Next

            Return True

        End Function
    End Class

End Class


