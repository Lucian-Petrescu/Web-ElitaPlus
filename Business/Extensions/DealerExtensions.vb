Imports System.Runtime.CompilerServices
Imports Assurant.ElitaPlus.DataEntities

Public Module DealerExtensions
    <Extension()>
    Public Function GetCompany(pDealer As Dealer,
                               pCompanyManager As ICompanyManager) As Company
        Return pCompanyManager.GetCompany(pDealer.CompanyId)
    End Function

End Module
