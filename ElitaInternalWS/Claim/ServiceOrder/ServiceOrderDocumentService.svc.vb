Imports System.ServiceModel
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports Assurant.ElitaPlus.Business
Imports System.Text

Namespace ServiceOrderDocument
    <ServiceBehavior(Namespace:="http://elita.assurant.com/Claim/ServiceOrder")>
    Public Class ServiceOrderDocumentService
        Implements IServiceOrderDocumentService

#Region "Member Variables"

        Private _company_id As Guid = Guid.Empty
        Private _claimId As Guid = Guid.Empty
        Private ImageId As Guid = Guid.Empty
        Private _claimBo As Claim = Nothing
        Private _oServiceOrder As ServiceOrder

#End Region

#Region "Extended Properties"

        Public Property CompanyId() As Guid
            Get
                Return _company_id
            End Get
            Set(Value As Guid)
                _company_id = Value
            End Set
        End Property

        Private Property ClaimBO() As Claim
            Get
                Return _claimBo
            End Get
            Set(value As Claim)
                _claimBo = value
            End Set
        End Property

        Private ReadOnly Property ClaimId() As Guid
            Get
                Return _claimId
            End Get
        End Property

        Private Property oServiceOrder As ServiceOrder
            Get
                Return _oServiceOrder
            End Get
            Set(value As ServiceOrder)
                _oServiceOrder = value
            End Set
        End Property

#End Region

        Public Function DownloadDocument(request As DownloadServiceOrderDocumentRequest) As DownloadServiceOrderDocumentResponse Implements IServiceOrderDocumentService.DownloadDocument

            request.Validate("request").HandleFault()

            Dim response As New DownloadServiceOrderDocumentResponse()
            Dim objCompaniesAL As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Dim i As Integer = Nothing
            Dim ServiceOrderData As Byte()
            Dim serviceOrderID As Guid


            Try
                For i = 0 To objCompaniesAL.Count - 1
                    Dim objCompany As New Company(CType(objCompaniesAL.Item(i), Guid))
                    If objCompany IsNot Nothing AndAlso objCompany.Code.Equals(request.CompanyCode.ToUpper) Then
                        CompanyId = objCompany.Id
                    End If
                Next
                If CompanyId.Equals(Guid.Empty) Then
                    Throw New FaultException(Of CompanyNotFoundFault)(New CompanyNotFoundFault(), "Company Not Found : " & request.CompanyCode)
                End If
            Catch conf As CompanyNotFoundException
                Throw New FaultException(Of CompanyNotFoundFault)(New CompanyNotFoundFault(), "Company Not Found : " & request.CompanyCode)
            End Try

            Try
                _claimId = Claim.GetClaimID(ElitaPlusIdentity.Current.ActiveUser.Companies, request.ClaimNumber)

                If Not ClaimId.Equals(Guid.Empty) Then
                    _claimBo = ClaimFacade.Instance.GetClaim(Of Claim)(_claimId)
                Else
                    Throw New FaultException(Of ClaimNotFoundFault)(New ClaimNotFoundFault(), "Claim Not Found : " & request.ClaimNumber)
                End If
            Catch ex As ClaimNotFoundException
                Throw New FaultException(Of ClaimNotFoundFault)(New ClaimNotFoundFault(), "Claim Not Found : " & request.ClaimNumber)
            End Try

            Try
                serviceOrderID = ServiceOrder.GetLatestServiceOrderID(ClaimId)
                oServiceOrder = New ServiceOrder(serviceOrderID)
                ServiceOrderData = Encoding.UTF8.GetBytes(oServiceOrder.GetReportHtmlData())

                If ServiceOrderData IsNot Nothing Then
                    response.ServiceOrderImageData = ServiceOrderData
                    Return response
                Else
                    Throw New FaultException(Of ServiceOrderNotFoundFault)(New ServiceOrderNotFoundFault(), "Service Order Not Found")
                End If
            Catch ex As Exception
                Throw New FaultException(Of ServiceOrderNotFoundFault)(New ServiceOrderNotFoundFault(), "Service Order Not Found")
            End Try
        End Function
    End Class
End Namespace