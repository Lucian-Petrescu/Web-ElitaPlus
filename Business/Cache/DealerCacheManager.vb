Imports System.Runtime.Caching
Imports Assurant.ElitaPlus.Business
Imports Assurant.ElitaPlus.DataAccessInterface
Imports Assurant.ElitaPlus.DataEntities

Public Class DealerCacheManager
    Implements ICacheManager

    Private Property DealerRepository As IDealerRepository(Of Dealer)
    Private Property ProductRepository As IDealerRepository(Of Product)
    Private Property ContractRepository As IDealerRepository(Of Contract)

    Friend Sub New(ByVal dealerRepository As IDealerRepository(Of Dealer),
                   productRepository As IDealerRepository(Of Product))
        Me.DealerRepository = dealerRepository
        Me.ProductRepository = productRepository
        'Me.ContractRepository = ContractRepository
    End Sub

    Private Const CacheKeyValue As String = "Dealers"

    Friend Function CacheKey(ByVal dealerCode As String) As String
        Return String.Format("{0}#{1}", CacheKeyValue, dealerCode.ToUpperInvariant())
    End Function

    Friend Function BuildCache(ByVal dealerCode As String) As Dealer
        Return DealerRepository.Get(Function(d) d.DealerCode = dealerCode, Nothing, "Contracts,SuspendedReasons,DealerRuleLists,DealerRuleLists.RuleList,DealerRuleLists.RuleList.RuleListDetails,DealerRuleLists.RuleList.RuleListDetails.Rule,DealerRuleLists.RuleList.RuleListDetails.Rule.RuleIssues,DealerRuleLists.RuleList.RuleListDetails.Rule.RuleIssues.Issues,DealerRuleLists.RuleList.RuleListDetails.Rule.RuleIssues.Issues.IssueQuestions,DealerRuleLists.RuleList.RuleListDetails.Rule.RuleIssues.Issues.IssueQuestions.SoftQuestion,DealerGroup").FirstOrDefault()
    End Function

    Friend Function CacheKey(ByVal dealerId As Guid) As String
        Return String.Format("{0}#{1}", CacheKeyValue, dealerId)
    End Function

    Friend Function BuildCache(ByVal dealerId As Guid) As Dealer
        Return DealerRepository.Get(Function(d) d.DealerId = dealerId, Nothing, "Contracts,SuspendedReasons,DealerRuleLists,DealerRuleLists.RuleList,DealerRuleLists.RuleList.RuleListDetails,DealerRuleLists.RuleList.RuleListDetails.Rule,DealerRuleLists.RuleList.RuleListDetails.Rule.RuleIssues,DealerRuleLists.RuleList.RuleListDetails.Rule.RuleIssues.Issues,DealerRuleLists.RuleList.RuleListDetails.Rule.RuleIssues.Issues.IssueQuestions,DealerRuleLists.RuleList.RuleListDetails.Rule.RuleIssues.Issues.IssueQuestions.SoftQuestion,DealerGroup").FirstOrDefault()
    End Function
    ''' <summary>
    ''' To Fetch the product object based on Dealer Code and Product Code
    ''' </summary>
    ''' <param name="dealerCode"></param>
    ''' <param name="productCode"></param>
    ''' <returns></returns>
    Friend Function CacheKey(ByVal dealerCode As String, productCode As String) As String
        Return String.Format("{0}#{1}#{2}", CacheKeyValue, dealerCode.ToUpperInvariant(), productCode.ToUpperInvariant())
    End Function

    Friend Function BuildCache(ByVal dealerCode As String, productCode As String) As Product
        Return ProductRepository.Get(Function(p) p.ProductCode = productCode And p.Dealer.DealerCode = dealerCode, Nothing, "Items,Items.Coverages").FirstOrDefault
    End Function
    ''' <summary>
    ''' To fetch the Contract object based on Dealer Code and Warranty Sales Date
    ''' </summary>
    ''' <param name="dealerCode"></param>
    ''' <param name="warrantySalesDate"></param>
    ''' <returns></returns>
    Friend Function CacheKey(ByVal dealerCode As String, warrantySalesDate As Date) As String
        Return String.Format("{0}#{1}#{2}", CacheKeyValue, dealerCode.ToUpperInvariant(), warrantySalesDate)
    End Function

    Friend Function BuildCache(ByVal dealerCode As String, warrantySalesDate As Date) As Contract
        'Return ContractRepository.Get(Function(c) warrantySalesDate > c.Effective AndAlso warrantySalesDate <= c.Expiration And c.Dealer.DealerCode = dealerCode).FirstOrDefault
        Return DealerRepository.Get(Function(d) d.DealerCode = dealerCode, Nothing).FirstOrDefault.Contracts.Where(Function(c) warrantySalesDate >= c.Effective AndAlso warrantySalesDate <= c.Expiration).FirstOrDefault
    End Function

    Public Function GetPolicy() As CacheItemPolicy Implements ICacheManager.GetPolicy
        Dim policy As CacheItemPolicy
        policy = New CacheItemPolicy()
        policy.SlidingExpiration = New TimeSpan(0, 30, 0) ' 30 Mins
        Return policy
    End Function

    Friend Function CacheKey(ByVal dealerId As Guid, ByVal Code As String, ByVal cacheType As String) As String
        Return String.Format("{0}#{1}#{2}", cacheType, dealerId.ToString(), Code.ToUpperInvariant())
    End Function

    Friend Function BuildBranchCache(ByVal dealerId As Guid, ByVal BranchCode As String) As Branch
        Return DealerRepository.Get(Function(d) d.DealerId = dealerId, Nothing, "Branches").FirstOrDefault.Branches.Where(Function(b) b.BranchCode = BranchCode).FirstOrDefault
    End Function
End Class
