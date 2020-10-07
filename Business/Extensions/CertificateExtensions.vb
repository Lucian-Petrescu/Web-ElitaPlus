Option Strict On
Imports System.Runtime.CompilerServices
Imports Assurant.ElitaPlus.DataEntities

Public Module CertificateExtensions
    <Extension()>
    Public Function GetDealer(pCertificate As Certificate,
                              pDealerManager As IDealerManager) As Dealer
        Return pDealerManager.GetDealerById(pCertificate.DealerId)
        
    End Function


End Module
