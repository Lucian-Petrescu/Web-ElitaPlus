Imports System.Collections.Generic
Imports Assurant.ElitaPlus.BusinessObjectsNew.Doc
Imports System.ServiceModel
Imports DocService = Assurant.ElitaPlus.BusinessObjectsNew.Doc

Namespace DocumentImaging
    Partial Public Class Doc
        Private Shared syncRoot As Object = New Object()
        Private Shared oDocClient As DocumentClient

        Private Shared ReadOnly Property ClientProxy As DocumentClient
            Get
                Dim oDocClient As DocumentClient
                If (oDocClient Is Nothing OrElse oDocClient.State <> ServiceModel.CommunicationState.Opened) Then
                    SyncLock syncRoot
                        If (oDocClient Is Nothing OrElse oDocClient.State <> ServiceModel.CommunicationState.Opened) Then
                            oDocClient = ServiceHelper.CreateDocumentClient()
                        End If
                    End SyncLock
                End If
                Return oDocClient
            End Get
        End Property

#Region "DataView Retrieveing Methods"

        Public Shared Function FindDocument(ByVal fr As FindRequest) As DocumentInfo()

            Dim userName As String = ElitaPlusIdentity.Current.ActiveUser.NetworkId
            Dim resultList() As DocumentInfo = New DocumentInfo() {}
            Try
                resultList = ClientProxy.Find(fr)
            Catch ex As FaultException(Of DocService.NotFoundFault)
                Throw New DataNotFoundException()
            Catch ex As FaultException(Of DocService.ValidationFault)
                Throw ex.AsBOValidationException()
            Catch ex As FaultException(Of DocService.DocumentFault)
                Throw New ServiceException("Document", "Find", ex)
            End Try
            Return resultList
        End Function

        Public Shared Function UpdateDocument(ByVal di As DocumentInfo) As DocumentInfo
            Dim userName As String = ElitaPlusIdentity.Current.ActiveUser.NetworkId
            Dim resultList As New DocumentInfo
            Try
                resultList = ClientProxy.Update(di)
            Catch ex As FaultException(Of DocService.NotFoundFault)
                Throw New DataNotFoundException()
            Catch ex As FaultException(Of DocService.ValidationFault)
                Throw ex.AsBOValidationException(di)
            Catch ex As FaultException(Of DocService.DocumentFault)
                Throw New ServiceException("Document", "Update", ex)
            End Try
            Return resultList
        End Function

        Public Shared Function DownloadDocument(ByVal id As Guid) As Document
            Dim userName As String = ElitaPlusIdentity.Current.ActiveUser.NetworkId
            Dim resultList As New Document
            Try
                resultList = ClientProxy.Download(id)
            Catch ex As FaultException(Of DocService.NotFoundFault)
                Throw New DataNotFoundException()
            Catch ex As FaultException(Of DocService.ValidationFault)
                Throw ex.AsBOValidationException()
            Catch ex As FaultException(Of DocService.DocumentFault)
                Throw New ServiceException("Document", "Download", ex)
            End Try
            Return resultList
        End Function

        Public Shared Function UploadDocument(ByVal QueueName As String, ByVal actionCode As String, ByVal activeOn As Nullable(Of Date), ByVal doc As Document) As Document
            Dim userName As String = ElitaPlusIdentity.Current.ActiveUser.NetworkId
            Dim resultList As New Document
            Try
                resultList = ClientProxy.Upload(doc)
            Catch ex As FaultException(Of DocService.NotFoundFault)
                Throw New DataNotFoundException()
            Catch ex As FaultException(Of DocService.ValidationFault)
                Throw ex.AsBOValidationException(doc)
            Catch ex As FaultException(Of DocService.DocumentFault)
                Throw New ServiceException("Document", "Upload", ex)
            End Try
            Return resultList

        End Function

#End Region
    End Class
End Namespace

