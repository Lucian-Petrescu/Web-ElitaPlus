Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports Assurant.ElitaPlus.DALObjects.Documents

Namespace Documents
    Public Class DocumentManager

#Region "Singleton Implementation"
        Private Shared _instance As DocumentManager
        Private Shared _syncRoot As New Object

        Private Sub New()
            _cacheSyncRoot = New Object
        End Sub

        Public Shared ReadOnly Property Current As DocumentManager
            Get
                If (_instance Is Nothing) Then
                    SyncLock (_syncRoot)
                        If (_instance Is Nothing) Then
                            _instance = New DocumentManager()
                        End If
                    End SyncLock
                End If
                Return _instance
            End Get
        End Property
#End Region

#Region "Cache Properties"
        Private _cacheSyncRoot As Object
        Private _cacheUpdateTimestamp As Nullable(Of DateTime)
        Private _repositories As List(Of Repository)
        Private _fileTypes As List(Of FileType)
        Private _ds As DataSet

        Private Sub ValidateCache()
            If (Not IsCacheValid()) Then
                RebuildCache()
            End If
        End Sub

        Private Function IsCacheValid() As Boolean
            If (_repositories Is Nothing) OrElse (_fileTypes Is Nothing) OrElse (Not _cacheUpdateTimestamp.HasValue) Then
                Return False
            End If

            If (_cacheUpdateTimestamp.Value.AddMinutes(15) >= DateTime.Now) Then
                Return False
            End If

            Return True

        End Function

        Private Sub RebuildCache()
            SyncLock (_cacheSyncRoot)
                If (Not IsCacheValid()) Then
                    _cacheUpdateTimestamp = DateTime.Now
                    LoadList(_ds, _repositories, _fileTypes)
                End If
            End SyncLock
        End Sub

        Public ReadOnly Property Repositories As ReadOnlyCollection(Of Repository)
            Get
                ValidateCache()
                Return _repositories.AsReadOnly()
            End Get
        End Property

        Public ReadOnly Property FileTypes As ReadOnlyCollection(Of FileType)
            Get
                ValidateCache()
                Return _fileTypes.AsReadOnly()
            End Get
        End Property
#End Region

#Region "Data Access"
        Private Sub LoadList(ByRef pDataSet As DataSet, ByRef pRepositories As List(Of Repository), ByRef pFileTypes As List(Of FileType))
            Dim dal As New RepositoryDAL
            pDataSet = dal.LoadList()
            pRepositories = New List(Of Repository)
            pFileTypes = New List(Of FileType)
            For Each dr As DataRow In pDataSet.Tables(RepositoryDAL.TABLE_NAME).Rows
                pRepositories.Add(New Repository(dr))
            Next
            For Each dr As DataRow In pDataSet.Tables(FileTypeDAL.TABLE_NAME).Rows
                pFileTypes.Add(New FileType(dr))
            Next
        End Sub

#End Region

#Region "File Type Functions"
        Public Function NewFileType() As FileType
            Dim repositories As List(Of Repository)
            Dim fileTypes As List(Of FileType)
            Dim ds As DataSet
            LoadList(ds, repositories, fileTypes)
            Dim ft As FileType = New FileType(ds.Tables(FileTypeDAL.TABLE_NAME))
            Return ft
        End Function

        Public Sub Save(ByVal pFileType As FileType)
            pFileType.Save()
            _repositories = Nothing
            _fileTypes = Nothing
        End Sub

#End Region

#Region "Repository Functions"

        Public Function NewRepository() As Repository
            Dim repositories As List(Of Repository)
            Dim fileTypes As List(Of FileType)
            Dim ds As DataSet
            LoadList(ds, repositories, fileTypes)
            Dim r As Repository = New Repository(ds.Tables(RepositoryDAL.TABLE_NAME))
            Return r
        End Function

        Public Sub Save(ByVal pRepository As Repository)
            pRepository.Save()
            _repositories = Nothing
            _fileTypes = Nothing
        End Sub

#End Region
    End Class
End Namespace
