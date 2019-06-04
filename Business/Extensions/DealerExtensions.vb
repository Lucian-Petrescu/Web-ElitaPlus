Imports System.Runtime.CompilerServices
Imports Assurant.ElitaPlus.DataEntities

Public Module DealerExtensions
    <Extension()>
    Public Function GetCompany(ByVal pDealer As Dealer,
                               ByVal pCompanyManager As ICompanyManager) As Company
        Return pCompanyManager.GetCompany(pDealer.CompanyId)
    End Function

End Module
