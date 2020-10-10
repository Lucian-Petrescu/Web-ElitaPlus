'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (6/18/2015)  ********************
Imports System.Configuration
Imports System.Security.Cryptography
Imports Microsoft.WindowsAzure.Storage
Imports Microsoft.WindowsAzure.Storage.Blob


Namespace Documents
    Public Class Document
        Inherits BusinessObjectBase

#Region "Constants"
        Private Const FILE_SIZE_ZERO As String = "FILE_SIZE_ZERO"
        Private Const FILE_SIZE_CAN_NOT_EXCEED_10_MB As String = "FILE_SIZE_CAN_NOT_EXCEED_10_MB"
        Private Const FILE_SIZE_MISMATCH As String = "FILE_SIZE_MISMATCH"
        Private Const INVALID_REPOSITORY As String = "INVALID_REPOSITORY"
        Private Const INVALID_FILE_TYPE As String = "INVALID_FILE_TYPE"
#End Region

#Region "Constructors"

        'Exiting BO
        Friend Sub New(id As Guid)
            MyBase.New()
            Dataset = New DataSet
            Load(id)
        End Sub

        'New BO
        Friend Sub New()
            MyBase.New()
            Dataset = New DataSet
            Load()
        End Sub

        'Exiting BO attaching to a BO family
        Friend Sub New(id As Guid, familyDS As DataSet)
            MyBase.New(False)
            Dataset = familyDS
            Load(id)
        End Sub

        'New BO attaching to a BO family
        Friend Sub New(familyDS As DataSet)
            MyBase.New(False)
            Dataset = familyDS
            Load()
        End Sub

        Friend Sub New(row As DataRow)
            MyBase.New(False)
            Dataset = row.Table.DataSet
            Me.Row = row
        End Sub

        Protected Sub Load()
            Try
                Dim dal As New DocumentDAL
                If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                    dal.LoadSchema(Dataset)
                End If
                Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
                Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
                Row = newRow
                SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
                Initialize()
            Catch ex As DataBaseAccessException
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End Sub

        Protected Sub Load(id As Guid)
            Try
                Dim dal As New DocumentDAL
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
            Catch ex As DataBaseAccessException
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
                If Row(DocumentDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return New Guid(CType(Row(DocumentDAL.COL_NAME_DOCUMENT_ID), Byte()))
                End If
            End Get
        End Property

        <ValueMandatory(""), ValidStringLength("", Max:=50), CheckFileType("")>
        Public Property FileType As String
            Get
                CheckDeleted()
                If Row(DocumentDAL.COL_NAME_FILE_TYPE) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return CType(Row(DocumentDAL.COL_NAME_FILE_TYPE), String)
                End If
            End Get
            Set
                CheckDeleted()
                SetValue(DocumentDAL.COL_NAME_FILE_TYPE, Value)
            End Set
        End Property

        <ValueMandatory(""), ValidStringLength("", Max:=100)>
        Public Property HashValue As String
            Get
                CheckDeleted()
                If Row(DocumentDAL.COL_NAME_HASH_VALUE) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return CType(Row(DocumentDAL.COL_NAME_HASH_VALUE), String)
                End If
            End Get
            Set
                CheckDeleted()
                If (IsNew) Then
                    SetValue(DocumentDAL.COL_NAME_HASH_VALUE, Value)
                Else
                    Throw New InvalidOperationException()
                End If

            End Set
        End Property

        <ValueMandatory(""), ValidStringLength("", Max:=500)>
        Public Property FileName As String
            Get
                CheckDeleted()
                If Row(DocumentDAL.COL_NAME_FILE_NAME) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return CType(Row(DocumentDAL.COL_NAME_FILE_NAME), String)
                End If
            End Get
            Set
                CheckDeleted()
                SetValue(DocumentDAL.COL_NAME_FILE_NAME, Value)
            End Set
        End Property


        <ValueMandatory(""), ValidStringLength("", Max:=50), CheckRepository("")>
        Public Property RepositoryCode As String
            Get
                CheckDeleted()
                If Row(DocumentDAL.COL_NAME_REPOSITORY_CODE) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return CType(Row(DocumentDAL.COL_NAME_REPOSITORY_CODE), String)
                End If
            End Get
            Set
                CheckDeleted()
                SetValue(DocumentDAL.COL_NAME_REPOSITORY_CODE, Value)
            End Set
        End Property


        <ValueMandatory(""), CheckFileSize("")>
        Public Property FileSizeBytes As LongType
            Get
                CheckDeleted()
                If Row(DocumentDAL.COL_NAME_FILE_SIZE_BYTES) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return New LongType(CType(Row(DocumentDAL.COL_NAME_FILE_SIZE_BYTES), Long))
                End If
            End Get
            Set
                CheckDeleted()
                SetValue(DocumentDAL.COL_NAME_FILE_SIZE_BYTES, Value)
            End Set
        End Property

        Public ReadOnly Property Repository As Repository
            Get
                If (RepositoryCode Is Nothing) OrElse (RepositoryCode.Trim().Length = 0) Then
                    Return Nothing
                End If
                Return DocumentManager.Current.Repositories.FirstOrDefault(Function (r) r.Code = RepositoryCode)
            End Get
        End Property

        Public ReadOnly Property FileTypeInfo As FileType
            Get
                If (FileType Is Nothing) OrElse (FileType.Length = 0) Then
                    Return Nothing
                End If
                Return DocumentManager.Current.FileTypes.FirstOrDefault(Function (rt) rt.Code = FileType)
            End Get
        End Property

        Friend ReadOnly Property AbsoluteFileName As String
            Get
                Return String.Format("{0}{1}{2}.file", Repository.StoragePath, IO.Path.DirectorySeparatorChar, Id.ToString())
            End Get
        End Property

        Private _data As Byte() = Nothing

        <CheckData(""), CheckMaxData("")>
        Public Property Data As Byte()
            Get
                CheckDeleted()
                If _data Is Nothing Then
                    If (IsNew) Then
                        Return Nothing
                    Else
                        Try
                            Select Case Repository.RepositoryTypeXcd
                                Case Codes.DOCUMENT_REPOSITORY_TYPE & "-" & Codes.DOCUMENT_REPOSITORY_TYPE__FILE_SYSTEM
                                    _data = IO.File.ReadAllBytes(AbsoluteFileName)
                                Case Codes.DOCUMENT_REPOSITORY_TYPE & "-" & Codes.DOCUMENT_REPOSITORY_TYPE__AZURE_BLOB
                                    Dim storeageConnectionString As String = ConfigurationManager.AppSettings("AzureBlobConnectionString")
                                    Dim storageAccount As CloudStorageAccount
                                    If (Not CloudStorageAccount.TryParse(storeageConnectionString, storageAccount)) Then
                                        Throw New BOInvalidOperationException("Unable to open connection to Azure Blog Storage")
                                    End If

                                    Dim cloudBlobClient As CloudBlobClient = storageAccount.CreateCloudBlobClient()
                                    Dim cloudBlobContainer As CloudBlobContainer = cloudBlobClient.GetContainerReference(Repository.StoragePath)
                                    Dim cloudBlockBlob As CloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(Id.ToString() + ".file")
                                    cloudBlockBlob.FetchAttributes()
                                    Dim buffer(cloudBlockBlob.Properties.Length - 1) As Byte
                                    cloudBlockBlob.DownloadToByteArray(buffer, 0)
                                    _data = buffer
                                Case Else
                                    Throw New NotSupportedException(String.Format("Repository Type {0} is not supported in Document::Data", Repository.RepositoryTypeXcd))
                            End Select

                        Catch ex As Exception
                            Throw New DocumentDownloadFailedException(Repository.Code, Repository.StoragePath, AbsoluteFileName, ex)
                        End Try

                        Dim sha256 = New SHA256CryptoServiceProvider()
                        Dim computedHash As String = Convert.ToBase64String(sha256.ComputeHash(_data))

                        If (computedHash <> HashValue) Then
                            Throw New FileIntegrityFailedException(Repository.Code, Repository.StoragePath, AbsoluteFileName, HashValue, computedHash, Nothing)
                        End If
                        Return _data
                    End If
                Else
                    Return _data
                End If
            End Get
            Set
                CheckDeleted()
                If (IsNew) Then
                    _data = value
                Else
                    Throw New InvalidOperationException()
                End If
            End Set
        End Property

#End Region

#Region "Public Members"
        <Obsolete("Use DocumentManager.Upload")>
        Public Overrides Sub Save()
            Try
                MyBase.Save()
                If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                    Dim dal As New DocumentDAL
                    dal.Update(Row)
                    'Reload the Data from the DB
                    If Row.RowState <> DataRowState.Detached Then
                        Dim objId As Guid = Id
                        Dataset = New DataSet
                        Row = Nothing
                        Load(objId)
                    End If
                End If
            Catch ex As DataBaseAccessException
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
            End Try
        End Sub
#End Region

#Region "DataView Retrieveing Methods"
        <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
        Public NotInheritable Class CheckFileType
            Inherits ValidBaseAttribute

            Public Sub New(fieldDisplayName As String)
                MyBase.New(fieldDisplayName, INVALID_FILE_TYPE)
            End Sub

            Public Overrides Function IsValid(objectToCheck As Object, objectToValidate As Object) As Boolean
                Dim obj As Document = CType(objectToValidate, Document)
                If (DocumentManager.Current.FileTypes.Where(Function(ft) ft.Code = obj.FileType).Count() = 1) Then
                    Return True
                Else
                    Return False
                End If
            End Function
        End Class

        <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
        Public NotInheritable Class CheckRepository
            Inherits ValidBaseAttribute

            Public Sub New(fieldDisplayName As String)
                MyBase.New(fieldDisplayName, INVALID_REPOSITORY)
            End Sub

            Public Overrides Function IsValid(objectToCheck As Object, objectToValidate As Object) As Boolean
                Dim obj As Document = CType(objectToValidate, Document)
                If (DocumentManager.Current.Repositories.Where(Function(r) r.Code = obj.RepositoryCode).Count() = 1) Then
                    Return True
                Else
                    Return False
                End If
            End Function
        End Class

        <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
        Public NotInheritable Class CheckFileSize
            Inherits ValidBaseAttribute

            Public Sub New(fieldDisplayName As String)
                MyBase.New(fieldDisplayName, FILE_SIZE_MISMATCH)
            End Sub

            Public Overrides Function IsValid(objectToCheck As Object, objectToValidate As Object) As Boolean
                Dim obj As Document = CType(objectToValidate, Document)
                If (Not obj.IsNew) Then
                    Return True
                End If
                Return (obj.Data.Length = obj.FileSizeBytes)
            End Function
        End Class

        <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
        Public NotInheritable Class CheckData
            Inherits ValidBaseAttribute

            Public Sub New(fieldDisplayName As String)
                MyBase.New(fieldDisplayName, FILE_SIZE_ZERO)
            End Sub

            Public Overrides Function IsValid(objectToCheck As Object, objectToValidate As Object) As Boolean
                Dim obj As Document = CType(objectToValidate, Document)
                If (Not obj.IsNew) Then
                    Return True
                End If
                Return (obj.Data.Length > 0)
            End Function
        End Class

        Public NotInheritable Class CheckMaxData
            Inherits ValidBaseAttribute

            Public Sub New(fieldDisplayName As String)
                MyBase.New(fieldDisplayName, FILE_SIZE_CAN_NOT_EXCEED_10_MB)
            End Sub

            Public Overrides Function IsValid(objectToCheck As Object, objectToValidate As Object) As Boolean
                Dim obj As Document = CType(objectToValidate, Document)
                If (Not obj.IsNew) Then
                    Return True
                End If
                Return (obj.Data.Length <= (1024 * 1024 * 10))
            End Function
        End Class

#End Region

    End Class

End Namespace