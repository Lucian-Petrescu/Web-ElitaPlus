'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (6/18/2015)  ********************
Imports System.Configuration
Imports Assurant.ElitaPlus.DALObjects.Documents
Imports Microsoft.WindowsAzure.Storage
Imports Microsoft.WindowsAzure.Storage.Blob

Namespace Documents
    Public Class Repository
        Inherits BusinessObjectBase

#Region "Constants"
        Private Const DUPLICATE_REPOSITORY_CODE As String = "DUPLICATE_REPOSITORY_CODE"
        Private Const DUPLICATE_REPOSITORY_STORAGE_PATH As String = "DUPLICATE_REPOSITORY_STORAGE_PATH"
        Private Const REPOSITORY_STORAGE_PHYSICAL_PATH_READ_WRITE_FAILED As String = "REPOSITORY_STORAGE_PHYSICAL_PATH_READ_WRITE_FAILED"
#End Region
#Region "Constructors"

        'New BO attaching to a BO family
        Friend Sub New(pDataTable As DataTable)
            MyBase.New(False)
            Dataset = pDataTable.DataSet
            Dim newRow As DataRow = pDataTable.NewRow
            pDataTable.Rows.Add(newRow)
            Row = newRow
            SetValue(RepositoryDAL.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        End Sub

        Friend Sub New(row As DataRow)
            MyBase.New(False)
            Dataset = row.Table.DataSet
            Me.Row = row
        End Sub

#End Region

#Region "Private Members"
        'Initialization code for new objects
        Private Sub Initialize()
        End Sub
#End Region

#Region "Image Properties"
        Public Function NewDocument() As Document
            Return New Document()
        End Function

        Public Sub Upload(pDocument As Document)
            pDocument.RepositoryCode = Code
            If (Not pDocument.FileName Is Nothing) AndAlso (pDocument.FileType.Length > 0) Then
                pDocument.FileType = pDocument.FileName.Split(New Char() {"."}).Last().ToUpper()
            End If
            If (Not pDocument.Data Is Nothing) AndAlso (pDocument.Data.Length > 0) Then
                pDocument.HashValue = Convert.ToBase64String(System.Security.Cryptography.SHA256Managed.Create().ComputeHash(pDocument.Data))
            End If

            pDocument.Validate()

            Select Case pDocument.Repository.RepositoryTypeXcd
                Case Codes.DOCUMENT_REPOSITORY_TYPE & "-" & Codes.DOCUMENT_REPOSITORY_TYPE__FILE_SYSTEM
                    FileSystemUpload(pDocument)
                Case Codes.DOCUMENT_REPOSITORY_TYPE & "-" & Codes.DOCUMENT_REPOSITORY_TYPE__AZURE_BLOB
                    AzureBlobUpload(pDocument)
                Case Else
                    Throw New NotSupportedException(String.Format("Repository Type {0} is not supported in Repository::Upload", pDocument.Repository.RepositoryTypeXcd))
            End Select

            pDocument.Save()
        End Sub

        Private Sub AzureBlobUpload(pDocument As Document)
            Dim storeageConnectionString As String = ConfigurationManager.AppSettings("AzureBlobConnectionString")
            Dim storageAccount As CloudStorageAccount
            If (Not CloudStorageAccount.TryParse(storeageConnectionString, storageAccount)) Then
                Throw New BOInvalidOperationException("Unable to open connection to Azure Blog Storage")
            End If

            Dim cloudBlobClient As CloudBlobClient = storageAccount.CreateCloudBlobClient()
            Dim cloudBlobContainer As CloudBlobContainer = cloudBlobClient.GetContainerReference(StoragePath)
            Dim cloudBlockBlob As CloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(pDocument.Id.ToString() + ".file")
            cloudBlockBlob.UploadFromByteArray(pDocument.Data, 0, pDocument.Data.Length)
        End Sub
        Private Sub FileSystemUpload(pDocument As Document)
            Dim fileName As String = pDocument.AbsoluteFileName
            System.IO.File.WriteAllBytes(fileName, pDocument.Data)
        End Sub

        Public Function Download(pDocumentId As Guid) As Document
            Dim oDocument As Document = New Document(pDocumentId)
            Return oDocument
        End Function

#End Region

#Region "Properties"

        'Key Property
        Public ReadOnly Property Id As Guid
            Get
                If Row(RepositoryDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return New Guid(CType(Row(RepositoryDAL.COL_NAME_REPOSITORY_ID), Byte()))
                End If
            End Get
        End Property

        <ValueMandatory(""), ValidStringLength("", Max:=50), CheckDuplicateCode("")>
        Public Property Code As String
            Get
                CheckDeleted()
                If Row(RepositoryDAL.COL_NAME_CODE) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return CType(Row(RepositoryDAL.COL_NAME_CODE), String)
                End If
            End Get
            Set
                CheckDeleted()
                SetValue(RepositoryDAL.COL_NAME_CODE, Value)
            End Set
        End Property


        <ValueMandatory(""), ValidStringLength("", Max:=500)>
        Public Property Description As String
            Get
                CheckDeleted()
                If Row(RepositoryDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return CType(Row(RepositoryDAL.COL_NAME_DESCRIPTION), String)
                End If
            End Get
            Set
                CheckDeleted()
                SetValue(RepositoryDAL.COL_NAME_DESCRIPTION, Value)
            End Set
        End Property


        <ValueMandatory(""), ValidStringLength("", Max:=100), CheckDuplicateStoragePath(""),
            CheckReadWriteTest("")>
        Public Property StoragePath As String
            Get
                CheckDeleted()
                If Row(RepositoryDAL.COL_NAME_STORAGE_PATH) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return CType(Row(RepositoryDAL.COL_NAME_STORAGE_PATH), String)
                End If
            End Get
            Set
                CheckDeleted()
                SetValue(RepositoryDAL.COL_NAME_STORAGE_PATH, Value)
            End Set
        End Property

        <ValidStringLength("", Max:=50)>
        Public Property RepositoryTypeXcd As String
            Get
                CheckDeleted()
                If Row(RepositoryDAL.COL_NAME_REPOSITORY_TYPE_XCD) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return CType(Row(RepositoryDAL.COL_NAME_REPOSITORY_TYPE_XCD), String)
                End If
            End Get
            Set
                CheckDeleted()
                SetValue(RepositoryDAL.COL_NAME_REPOSITORY_TYPE_XCD, Value)
            End Set
        End Property

#End Region

#Region "Public Members"
        <Obsolete("Use DocumentManager.Save to make sure cache is updated properly")>
        Public Overrides Sub Save()
            Try
                MyBase.Save()
                If IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                    Dim dal As New RepositoryDAL
                    dal.Update(Row)
                    'Reload the Data from the DB
                    If Row.RowState <> DataRowState.Detached Then
                        Dim objId As Guid = Id
                        Dataset = New DataSet
                        Row = Nothing
                    End If
                End If
            Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
            End Try
        End Sub
#End Region

#Region "DataView Retrieveing Methods"

#End Region

        <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
        Public NotInheritable Class CheckDuplicateCode
            Inherits ValidBaseAttribute

            Public Sub New(fieldDisplayName As String)
                MyBase.New(fieldDisplayName, DUPLICATE_REPOSITORY_CODE)
            End Sub

            Public Overrides Function IsValid(objectToCheck As Object, objectToValidate As Object) As Boolean
                Dim obj As Repository = CType(objectToValidate, Repository)
                If (DocumentManager.Current.Repositories.Where(Function(r) r.Code = obj.Code AndAlso r.Id <> obj.Id).Count() = 0) Then
                    Return True
                Else
                    Return False
                End If
            End Function
        End Class

        <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
        Public NotInheritable Class CheckDuplicateStoragePath
            Inherits ValidBaseAttribute

            Public Sub New(fieldDisplayName As String)
                MyBase.New(fieldDisplayName, DUPLICATE_REPOSITORY_STORAGE_PATH)
            End Sub

            Public Overrides Function IsValid(objectToCheck As Object, objectToValidate As Object) As Boolean
                Dim obj As Repository = CType(objectToValidate, Repository)
                If (DocumentManager.Current.Repositories.Where(Function(r) r.StoragePath = obj.StoragePath AndAlso r.Id <> obj.Id).Count() = 0) Then
                    Return True
                Else
                    Return False
                End If
            End Function
        End Class

        <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
        Public NotInheritable Class CheckReadWriteTest
            Inherits ValidBaseAttribute

            Public Sub New(fieldDisplayName As String)
                MyBase.New(fieldDisplayName, REPOSITORY_STORAGE_PHYSICAL_PATH_READ_WRITE_FAILED)
            End Sub

            Public Overrides Function IsValid(objectToCheck As Object, objectToValidate As Object) As Boolean
                Dim obj As Repository = CType(objectToValidate, Repository)
                If (obj.StoragePath Is Nothing) OrElse (obj.StoragePath.Trim().Length = 0) Then
                    Return False
                End If

                If (Not System.IO.Directory.Exists(obj.StoragePath)) Then
                    Return False
                End If

                Dim buffer() As Byte = New Byte(100) {}
                Dim rnd As New Random
                rnd.NextBytes(buffer)
                Dim fileName As String = String.Format("{0}{1}Dummy.bin", obj.StoragePath, System.IO.Path.DirectorySeparatorChar)

                Try
                    If (System.IO.File.Exists(fileName)) Then
                        System.IO.File.Delete(fileName)
                    End If

                    System.IO.File.WriteAllBytes(fileName, buffer)

                    Dim newBuffer As Byte() = New Byte(100) {}
                    newBuffer = System.IO.File.ReadAllBytes(fileName)

                    Return buffer.SequenceEqual(newBuffer)
                Catch ex As Exception
                    Return False
                End Try

                Return True

            End Function
        End Class

    End Class

End Namespace
