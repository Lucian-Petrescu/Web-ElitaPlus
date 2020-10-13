Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports System.ServiceModel
Imports Assurant.ElitaPlus.Common
Imports ElitaInternalWS.Security
Imports System.Collections.Generic
Imports System.Runtime.Serialization

Friend Module CertificateServiceHelper

    Friend Function GetSerialNumber(pCertificateLookupRequest As CertificateLookup) As String
        If (pCertificateLookupRequest.GetType() Is GetType(CertificateSerialTaxLookup)) Then
            Return DirectCast(pCertificateLookupRequest, CertificateSerialTaxLookup).SerialNumber
        ElseIf (pCertificateLookupRequest.GetType() Is GetType(CertificateAccountNumberLookup)) Then
            Return DirectCast(pCertificateLookupRequest, CertificateAccountNumberLookup).SerialNumber
        ElseIf (pCertificateLookupRequest.GetType() Is GetType(CertificateDealerSerialLookUp)) Then
            Return DirectCast(pCertificateLookupRequest, CertificateDealerSerialLookUp).SerialNumber
        Else
            Return Nothing
        End If

    End Function

    Friend Function GetCertificate(CertificateLookupRequest As CertificateLookup, Optional ByVal ReqDetails As CertificateDetailTypes = CertificateDetailTypes.None) As Certificate
        Dim certificateId As Nullable(Of Guid)


        Try
            If (CertificateLookupRequest.GetType() Is GetType(CertificateSerialTaxLookup)) Then

                Dim req As CertificateSerialTaxLookup = DirectCast(CertificateLookupRequest, CertificateSerialTaxLookup)

                Dim dvDealers As DataView
                dvDealers = LookupListNew.GetUserDealerAssignedLookupList(ElitaPlusIdentity.Current.ActiveUser.Id, req.DealerCode)
                If (dvDealers.Count = 0) Then
                    ' User does not have permission to deal with Dealer
                    Throw New FaultException(Of DealerNotFoundFault)(New DealerNotFoundFault() With {.DealerCode = req.DealerCode}, "Dealer Not Found")
                End If

                certificateId = CertificateFacade.Instance.GetCertificatebyCertNumber(req.CertificateNumber, req.DealerCode, Nothing)

            ElseIf (CertificateLookupRequest.GetType() Is GetType(CertificateAccountNumberLookup)) Then

                Dim req As CertificateAccountNumberLookup = DirectCast(CertificateLookupRequest, CertificateAccountNumberLookup)

                Dim DealerDV As DataView
                DealerDV = LookupListNew.GetUserDealerAssignedLookupList(ElitaPlusIdentity.Current.ActiveUser.Id, req.DealerCode)
                If (DealerDV.Count = 0) Then
                    ' User does not have permission to deal with Dealer
                    Throw New FaultException(Of DealerNotFoundFault)(New DealerNotFoundFault() With {.DealerCode = req.DealerCode}, "Dealer Not Found")
                End If

                certificateId = CertificateFacade.Instance.GetCertificatebyAcctNumber(req.BillingAccountNumber, req.DealerCode)

            ElseIf (CertificateLookupRequest.GetType() Is GetType(CertificateDealerSerialLookUp)) Then

                Dim req As CertificateDealerSerialLookUp = DirectCast(CertificateLookupRequest, CertificateDealerSerialLookUp)

                Dim DealerDV As DataView
                DealerDV = LookupListNew.GetUserDealerAssignedLookupList(ElitaPlusIdentity.Current.ActiveUser.Id, req.DealerCode)
                If (DealerDV.Count = 0) Then
                    ' User does not have permission to deal with Dealer
                    Throw New FaultException(Of DealerNotFoundFault)(New DealerNotFoundFault() With {.DealerCode = req.DealerCode}, "Dealer Not Found")
                End If
                If (ReqDetails And CertificateDetailTypes.UpgradeCert) = CertificateDetailTypes.UpgradeCert Then
                    certificateId = CertificateFacade.Instance.GetCertificatebySerialNumber(req.DealerCode.Trim(), req.SerialNumber, Codes.YESNO_Y)
                Else
                    certificateId = CertificateFacade.Instance.GetCertificatebySerialNumber(req.DealerCode.Trim(), req.SerialNumber, Nothing)
                End If

            ElseIf (CertificateLookupRequest.GetType() Is GetType(CertAfterUpgradeLookup)) Then

                Dim req As CertAfterUpgradeLookup = DirectCast(CertificateLookupRequest, CertAfterUpgradeLookup)

                Dim DealerDV As DataView
                DealerDV = LookupListNew.GetUserDealerAssignedLookupList(ElitaPlusIdentity.Current.ActiveUser.Id, req.DealerCode)
                If (DealerDV.Count = 0) Then
                    ' User does not have permission to deal with Dealer
                    Throw New FaultException(Of DealerNotFoundFault)(New DealerNotFoundFault() With {.DealerCode = req.DealerCode}, "Dealer Not Found")
                End If
                certificateId = CertificateFacade.Instance.GetCertAfterUpgrade(req.DealerCode, req.SerialNumber, req.IdentificationNumber, req.UpgradeDate)

            ElseIf (CertificateLookupRequest.GetType() Is GetType(CertificateNumberLookup)) Then
                Dim req As CertificateNumberLookup = DirectCast(CertificateLookupRequest, CertificateNumberLookup)
                Dim DealerDV As DataView
                DealerDV = LookupListNew.GetUserDealerAssignedLookupList(ElitaPlusIdentity.Current.ActiveUser.Id, req.DealerCode)
                If (DealerDV.Count = 0) Then
                    ' User does not have permission to deal with Dealer
                    Throw New FaultException(Of DealerNotFoundFault)(New DealerNotFoundFault() With {.DealerCode = req.DealerCode}, "Dealer Not Found")
                End If
                If (ReqDetails And CertificateDetailTypes.UpgradeCert) = CertificateDetailTypes.UpgradeCert Then
                    certificateId = CertificateFacade.Instance.GetCertificatebyCertNumber(req.CertificateNumber, req.DealerCode, Codes.YESNO_Y)
                Else
                    certificateId = CertificateFacade.Instance.GetCertificatebyCertNumber(req.CertificateNumber, req.DealerCode, Nothing)
                End If
            ElseIf (CertificateLookupRequest.GetType() Is GetType(CertificateByCompanyLookup)) Then '''''REQ-5812 - Movistar Chile Upgrade
                Dim req As CertificateByCompanyLookup = DirectCast(CertificateLookupRequest, CertificateByCompanyLookup)
                Dim CompanyDV As DataView
                CompanyDV = LookupListNew.GetUserCompaniesLookupList()

                If (CompanyDV.Count = 0) Then
                    Throw New FaultException(Of CompanyNotFoundFault)(New CompanyNotFoundFault() With {.CompanyCode = req.CompanyCode}, "Company Not Found")
                Else
                    CompanyDV.RowFilter = "CODE ='" & req.CompanyCode & "'"
                    If (CompanyDV.Count = 0) Then
                        Throw New FaultException(Of CompanyNotFoundFault)(New CompanyNotFoundFault() With {.CompanyCode = req.CompanyCode}, "Company Not Found")
                    End If
                End If

                certificateId = CertificateFacade.Instance.GetCertificatebyCertNumberAndCompanyCode(req.CertificateNumber, req.CompanyCode)
            ElseIf (CertificateLookupRequest.GetType() Is GetType(CertficateDealerPhoneLookUp)) Then
                Dim req As CertficateDealerPhoneLookUp = DirectCast(CertificateLookupRequest, CertficateDealerPhoneLookUp)
                Dim DealerDV As DataView
                DealerDV = LookupListNew.GetUserDealerAssignedLookupList(ElitaPlusIdentity.Current.ActiveUser.Id, req.DealerCode)
                If (DealerDV.Count = 0) Then
                    ' User does not have permission to deal with Dealer
                    Throw New FaultException(Of DealerNotFoundFault)(New DealerNotFoundFault() With {.DealerCode = req.DealerCode}, "Dealer Not Found")
                End If
                If (ReqDetails And CertificateDetailTypes.UpgradeCert) = CertificateDetailTypes.UpgradeCert Then
                    certificateId = CertificateFacade.Instance.GetCertificatebyMobileNumber(req.MobilePhone.Trim(), req.DealerCode.Trim(), Codes.YESNO_Y)
                Else
                    certificateId = CertificateFacade.Instance.GetCertificatebyMobileNumber(req.MobilePhone.Trim(), req.DealerCode.Trim(), Nothing)
                End If
            Else
                Throw New NotSupportedException()
            End If
        Catch ex As StoredProcedureGeneratedException
            Throw New FaultException(Of CertificateNotFound)(New CertificateNotFound() With {.CertificateSearch = CertificateLookupRequest}, "Certificate Not Found")
        Catch ex As DataBaseAccessException
            If (CertificateLookupRequest.GetType() Is GetType(CertificateDealerSerialLookUp)) AndAlso (certificateId Is Nothing) Then
                Throw New CertificateNotFoundException(CertificateLookupRequest)
            ElseIf (CertificateLookupRequest.GetType() Is GetType(CertAfterUpgradeLookup)) AndAlso (certificateId Is Nothing) Then
                If ex.InnerException.Message.Contains("Duplicate_Certificate_Error") Then
                    Throw New FaultException(Of DuplicateCertFound)(New DuplicateCertFound() With {.CertificateSearch = CertificateLookupRequest}, "Duplicate Certificate Found")
                ElseIf ex.InnerException.Message.Contains("Coverage_Not_Found_Error") Then
                    Throw New FaultException(Of CoverageNotFoundFault)(New CoverageNotFoundFault() With {.CertificateSearch = CertificateLookupRequest}, "Coverage Not Found")
                Else
                    Throw New FaultException(Of CertificateNotFound)(New CertificateNotFound() With {.CertificateSearch = CertificateLookupRequest}, "Certificate Not Found")
                End If
            Else
                Throw New FaultException(Of CertificateNotFound)(New CertificateNotFound() With {.CertificateSearch = CertificateLookupRequest}, "Certificate Not Found")
            End If
            'Catch ex As Exception
            '    Throw New FaultException(Of CertificateNotFound)(New CertificateNotFound() With {.CertificateSearch = CertificateLookupRequest}, "Certificate Not Found")
        End Try

        If (certificateId Is Nothing) Then
            Throw New CertificateNotFoundException(CertificateLookupRequest)
        End If

        Return New Certificate(certificateId.Value)

    End Function


    <Serializable>
    Public Class CertificateNotFoundException
        Inherits Exception

        Public Property CertificateSearch As CertificateLookup

        Public Sub New(pCertificateSearch As CertificateLookup)
            CertificateSearch = pCertificateSearch
        End Sub
        Public Sub New(pCertificateSearch As CertificateLookup, pMessage As String)
            MyBase.New(pMessage)
            CertificateSearch = pCertificateSearch
        End Sub
        Public Sub New(pCertificateSearch As CertificateLookup, pMessage As String, pInner As Exception)
            MyBase.New(pMessage, pInner)
            CertificateSearch = pCertificateSearch
        End Sub
        Protected Sub New(info As SerializationInfo, context As StreamingContext)
            MyBase.New(info, context)
        End Sub

    End Class
End Module



