'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (6/18/2015)  ********************
Imports System.Configuration
Imports Assurant.ElitaPlus.DALObjects.Documents
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
        Friend Sub New(ByVal id As Guid)
            MyBase.New()
            Me.Dataset = New DataSet
            Me.Load(id)
        End Sub

        'New BO
        Friend Sub New()
            MyBase.New()
            Me.Dataset = New DataSet
            Me.Load()
        End Sub

        'Exiting BO attaching to a BO family
        Friend Sub New(ByVal id As Guid, ByVal familyDS As DataSet)
            MyBase.New(False)
            Me.Dataset = familyDS
            Me.Load(id)
        End Sub

        'New BO attaching to a BO family
        Friend Sub New(ByVal familyDS As DataSet)
            MyBase.New(False)
            Me.Dataset = familyDS
            Me.Load()
        End Sub

        Friend Sub New(ByVal row As DataRow)
            MyBase.New(False)
            Me.Dataset = row.Table.DataSet
            Me.Row = row
        End Sub

        Protected Sub Load()
            Try
                Dim dal As New DocumentDAL
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
                Dim dal As New DocumentDAL
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
                If Row(DocumentDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return New Guid(CType(Row(DocumentDAL.COL_NAME_DOCUMENT_ID), Byte()))
                End If
            End Get
        End Property

        <ValueMandatory(""), ValidStringLength("", Max:=50), CheckFileType("")>
        Public Property FileType() As String
            Get
                CheckDeleted()
                If Row(DocumentDAL.COL_NAME_FILE_TYPE) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return CType(Row(DocumentDAL.COL_NAME_FILE_TYPE), String)
                End If
            End Get
            Set(ByVal Value As String)
                CheckDeleted()
                Me.SetValue(DocumentDAL.COL_NAME_FILE_TYPE, Value)
            End Set
        End Property

        <ValueMandatory(""), ValidStringLength("", Max:=100)>
        Public Property HashValue() As String
            Get
                CheckDeleted()
                If Row(DocumentDAL.COL_NAME_HASH_VALUE) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return CType(Row(DocumentDAL.COL_NAME_HASH_VALUE), String)
                End If
            End Get
            Set(ByVal Value As String)
                CheckDeleted()
                If (Me.IsNew) Then
                    Me.SetValue(DocumentDAL.COL_NAME_HASH_VALUE, Value)
                Else
                    Throw New InvalidOperationException()
                End If

            End Set
        End Property

        <ValueMandatory(""), ValidStringLength("", Max:=500)>
        Public Property FileName() As String
            Get
                CheckDeleted()
                If Row(DocumentDAL.COL_NAME_FILE_NAME) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return CType(Row(DocumentDAL.COL_NAME_FILE_NAME), String)
                End If
            End Get
            Set(ByVal Value As String)
                CheckDeleted()
                Me.SetValue(DocumentDAL.COL_NAME_FILE_NAME, Value)
            End Set
        End Property


        <ValueMandatory(""), ValidStringLength("", Max:=50), CheckRepository("")>
        Public Property RepositoryCode() As String
            Get
                CheckDeleted()
                If Row(DocumentDAL.COL_NAME_REPOSITORY_CODE) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return CType(Row(DocumentDAL.COL_NAME_REPOSITORY_CODE), String)
                End If
            End Get
            Set(ByVal Value As String)
                CheckDeleted()
                Me.SetValue(DocumentDAL.COL_NAME_REPOSITORY_CODE, Value)
            End Set
        End Property


        <ValueMandatory(""), CheckFileSize("")>
        Public Property FileSizeBytes() As LongType
            Get
                CheckDeleted()
                If Row(DocumentDAL.COL_NAME_FILE_SIZE_BYTES) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return New LongType(CType(Row(DocumentDAL.COL_NAME_FILE_SIZE_BYTES), Long))
                End If
            End Get
            Set(ByVal Value As LongType)
                CheckDeleted()
                Me.SetValue(DocumentDAL.COL_NAME_FILE_SIZE_BYTES, Value)
            End Set
        End Property

        Public ReadOnly Property Repository As Repository
            Get
                If (Me.RepositoryCode Is Nothing) OrElse (Me.RepositoryCode.Trim().Length = 0) Then
                    Return Nothing
                End If
                Return DocumentManager.Current.Repositories.Where(Function(r) r.Code = Me.RepositoryCode).FirstOrDefault()
            End Get
        End Property

        Public ReadOnly Property FileTypeInfo As FileType
            Get
                If (Me.FileType Is Nothing) OrElse (Me.FileType.Length = 0) Then
                    Return Nothing
                End If
                Return DocumentManager.Current.FileTypes.Where(Function(rt) rt.Code = Me.FileType).FirstOrDefault()
            End Get
        End Property

        Friend ReadOnly Property AbsoluteFileName As String
            Get
                Return String.Format("{0}{1}{2}.file", Me.Repository.StoragePath, System.IO.Path.DirectorySeparatorChar, Me.Id.ToString())
            End Get
        End Property

        Private _data As Byte() = Nothing

        <CheckData(""), CheckMaxData("")>
        Public Property Data() As Byte()
            Get
                CheckDeleted()
                If _data Is Nothing Then
                    If (Me.IsNew) Then
                        Return Nothing
                    Else
                        Try
                            Select Case Me.Repository.RepositoryTypeXcd
                                Case Codes.DOCUMENT_REPOSITORY_TYPE & "-" & Codes.DOCUMENT_REPOSITORY_TYPE__FILE_SYSTEM
                                    _data = System.IO.File.ReadAllBytes(Me.AbsoluteFileName)
                                Case Codes.DOCUMENT_REPOSITORY_TYPE & "-" & Codes.DOCUMENT_REPOSITORY_TYPE__AZURE_BLOB
                                    Dim storeageConnectionString As String = ConfigurationManager.AppSettings("AzureBlobConnectionString")
                                    Dim storageAccount As CloudStorageAccount
                                    If (Not CloudStorageAccount.TryParse(storeageConnectionString, storageAccount)) Then
                                        Throw New BOInvalidOperationException("Unable to open connection to Azure Blog Storage")
                                    End If

                                    Dim cloudBlobClient As CloudBlobClient = storageAccount.CreateCloudBlobClient()
                                    Dim cloudBlobContainer as CloudBlobContainer = cloudBlobClient.GetContainerReference(Me.Repository.StoragePath)
                                    dim cloudBlockBlob As CloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(Id.ToString() + ".file")
                                    cloudBlockBlob.FetchAttributes()
                                    dim buffer(cloudBlockBlob.Properties.Length - 1) as Byte
                                    cloudBlockBlob.DownloadToByteArray(buffer, 0)
                                    _data = buffer
                                Case Else
                                    Throw New NotSupportedException(String.Format("Repository Type {0} is not supported in Document::Data", Me.Repository.RepositoryTypeXcd))
                            End Select

                        Catch ex As Exception
                            Throw New DocumentDownloadFailedException(Me.Repository.Code, Me.Repository.StoragePath, Me.AbsoluteFileName, ex)
                        End Try

                        Dim computedHash As String = Convert.ToBase64String(System.Security.Cryptography.SHA256Managed.Create().ComputeHash(_data))

                        If (computedHash <> Me.HashValue) Then
                            Throw New FileIntegrityFailedException(Me.Repository.Code, Me.Repository.StoragePath, Me.AbsoluteFileName, Me.HashValue, computedHash, Nothing)
                        End If
                        Return _data
                    End If
                Else
                    Return _data
                End If
            End Get
            Set(ByVal value As Byte())
                CheckDeleted()
                If (Me.IsNew) Then
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
                If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                    Dim dal As New DocumentDAL
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

#Region "DataView Retrieveing Methods"
        <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
        Public NotInheritable Class CheckFileType
            Inherits ValidBaseAttribute

            Public Sub New(ByVal fieldDisplayName As String)
                MyBase.New(fieldDisplayName, INVALID_FILE_TYPE)
            End Sub

            Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal objectToValidate As Object) As Boolean
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

            Public Sub New(ByVal fieldDisplayName As String)
                MyBase.New(fieldDisplayName, INVALID_REPOSITORY)
            End Sub

            Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal objectToValidate As Object) As Boolean
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

            Public Sub New(ByVal fieldDisplayName As String)
                MyBase.New(fieldDisplayName, FILE_SIZE_MISMATCH)
            End Sub

            Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal objectToValidate As Object) As Boolean
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

            Public Sub New(ByVal fieldDisplayName As String)
                MyBase.New(fieldDisplayName, FILE_SIZE_ZERO)
            End Sub

            Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal objectToValidate As Object) As Boolean
                Dim obj As Document = CType(objectToValidate, Document)
                If (Not obj.IsNew) Then
                    Return True
                End If
                Return (obj.Data.Length > 0)
            End Function
        End Class

        Public NotInheritable Class CheckMaxData
            Inherits ValidBaseAttribute

            Public Sub New(ByVal fieldDisplayName As String)
                MyBase.New(fieldDisplayName, FILE_SIZE_CAN_NOT_EXCEED_10_MB)
            End Sub

            Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal objectToValidate As Object) As Boolean
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