Option Strict On
Imports System.Runtime.CompilerServices
Imports Assurant.ElitaPlus.DataEntities

Public Module CertificateExtensions
    <Extension()>
    Public Function GetDealer(ByVal pCertificate As Certificate,
                              ByVal pDealerManager As IDealerManager) As Dealer
        Return pDealerManager.GetDealerById(pCertificate.DealerId)
        
    End Function


End Module
