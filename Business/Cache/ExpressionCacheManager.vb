Imports System.Runtime.Caching
Imports Assurant.ElitaPlus.DataAccessInterface
Imports Assurant.ElitaPlus.DataEntities

Public Class ExpressionCacheManager
    Implements ICacheManager

    Private Property CommonRepository As ICommonRepository(Of Expression)
    Friend Sub New(commonRepository As ICommonRepository(Of Expression))
        Me.CommonRepository = commonRepository
    End Sub

    Private Const CacheKeyValue As String = "Expression"

    Friend Function CacheKeyForExpression(pExpressionID As Guid) As String
        Return String.Format("{0}#{1}", CacheKeyValue, pExpressionID)
    End Function

    Friend Function BuildCacheForExpression(pExpressionID As Guid) As Expression
        Return CommonRepository.Get(Function(e) e.ExpressionId = pExpressionID).FirstOrDefault()
    End Function


    Friend Function GetPolicy() As CacheItemPolicy Implements ICacheManager.GetPolicy
        Dim policy As CacheItemPolicy
        policy = New CacheItemPolicy()
        policy.SlidingExpiration = New TimeSpan(0, 30, 0) ' 30 Mins
        Return policy
    End Function
End Class
