Imports System.Collections.Generic
Imports Assurant.ElitaPlus.BusinessObjectsNew.DocAdmin
Imports System.ServiceModel
Imports DocAdminService = Assurant.ElitaPlus.BusinessObjectsNew.DocAdmin

Namespace DocAdminImaging
    Partial Public Class DocAdmin
        Private Shared syncRoot As Object = New Object()
        Private Shared oDocAdminServiceClient As DocumentAdminClient

        Private Shared ReadOnly Property ClientProxy As DocumentAdminClient
            Get
                Dim wrkQueClient As DocumentAdminClient
                If (oDocAdminServiceClient Is Nothing OrElse oDocAdminServiceClient.State <> CommunicationState.Opened) Then
                    SyncLock syncRoot
                        If (oDocAdminServiceClient Is Nothing OrElse oDocAdminServiceClient.State <> CommunicationState.Opened) Then
                            oDocAdminServiceClient = ServiceHelper.CreateDocumentAdminClient()
                        End If
                    End SyncLock
                End If
                Return oDocAdminServiceClient
            End Get
        End Property

#Region "DataView Retrieveing Methods"

        Public Shared Function GetRepositoryList(RepositoriesName As String, actionCode As String, activeOn As Nullable(Of Date)) As DocumentRepository()
            Dim userName As String = ElitaPlusIdentity.Current.ActiveUser.NetworkId
            Dim resultList() As DocumentRepository = New DocumentRepository() {}
            Try
                resultList = ClientProxy.GetRepositories()
            Catch ex As FaultException(Of DocAdminService.NotFoundFault)
                Throw New DataNotFoundException()
            Catch ex As FaultException(Of DocAdminService.RepositoryFault)
                Throw New ServiceException("DocumentAdmin", "GetRepositories", ex)
            End Try
            Return resultList
        End Function

        Public Shared Function GetDocumentFormats(RepositoriesName As String, actionCode As String, activeOn As Nullable(Of Date)) As DocumentFormat()
            Dim userName As String = ElitaPlusIdentity.Current.ActiveUser.NetworkId
            Dim resultList() As DocumentFormat = New DocumentFormat() {}
            Try
                resultList = ClientProxy.GetDocumentFormats()
            Catch ex As FaultException(Of DocAdminService.NotFoundFault)
                Throw New DataNotFoundException()
            Catch ex As FaultException(Of DocAdminService.RepositoryFault)
                Throw New ServiceException("DocumentAdmin", "GetDocumentFormats", ex)
            End Try
            Return resultList
        End Function

        Public Shared Function GetDocumentFormat(RepositoriesName As String, actionCode As String, activeOn As Nullable(Of Date), id As Guid) As DocumentFormat
            Try
                Dim userName As String = ElitaPlusIdentity.Current.ActiveUser.NetworkId
                Dim resultList As DocumentFormat = New DocumentFormat()
                Try
                    resultList = ClientProxy.GetDocumentFormat(id)
                Catch ex As FaultException(Of DocAdminService.NotFoundFault)
                    Throw New DataNotFoundException()
                Catch ex As FaultException(Of DocAdminService.ValidationFault)
                    Throw ex.AsBOValidationException()
                Catch ex As FaultException(Of DocAdminService.RepositoryFault)
                    Throw New ServiceException("DocumentAdmin", "GetDocumentFormat", ex)
                End Try
                Return resultList
            Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
                Throw New DataBaseAccessException(ex.ErrorType, ex)
            End Try
        End Function

        Public Shared Function GetRepositoryById(RepositoriesName As String, actionCode As String, activeOn As Nullable(Of Date), id As Guid) As DocumentRepository
            Dim userName As String = ElitaPlusIdentity.Current.ActiveUser.NetworkId
            Dim resultList As DocumentRepository = New DocumentRepository
            Try
                resultList = ClientProxy.GetRepositoryById(id)
            Catch ex As FaultException(Of DocAdminService.NotFoundFault)
                Throw New DataNotFoundException()
            Catch ex As FaultException(Of DocAdminService.ValidationFault)
                Throw ex.AsBOValidationException()
            Catch ex As FaultException(Of DocAdminService.RepositoryFault)
                Throw New ServiceException("DocumentAdmin", "GetRepositoryById", ex)
            End Try
            Return resultList
        End Function

        Public Shared Function GetRepositoryByName(RepositoriesName As String, actionCode As String, activeOn As Nullable(Of Date), RepoName As String) As DocumentRepository
            Dim userName As String = ElitaPlusIdentity.Current.ActiveUser.NetworkId
            Dim resultList As DocumentRepository = New DocumentRepository
            Try
                resultList = ClientProxy.GetRepositoryByName(RepoName)
            Catch ex As FaultException(Of DocAdminService.NotFoundFault)
                Throw New DataNotFoundException()
            Catch ex As FaultException(Of DocAdminService.ValidationFault)
                Throw ex.AsBOValidationException()
            Catch ex As FaultException(Of DocAdminService.RepositoryFault)
                Throw New ServiceException("DocumentAdmin", "GetRepositoryByName", ex)
            End Try
            Return resultList
        End Function

#End Region

    End Class

End Namespace

