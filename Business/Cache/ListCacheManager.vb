Imports System.Runtime.Caching
Imports Assurant.ElitaPlus.DataAccessInterface
Imports Assurant.ElitaPlus.DataEntities

Friend Class ListCacheManager
    Implements ICacheManager

    Private Property CommonRepository As ICommonRepository(Of List)
    Friend Sub New(commonRepository As ICommonRepository(Of List))
        Me.CommonRepository = commonRepository
    End Sub

    Private Const CacheKeyValue As String = "List"

    Friend Function CacheKey(listCode As String, languageCode As String) As String
        Return String.Format("{0}#{1}#{2}", CacheKeyValue, listCode.ToUpperInvariant(), languageCode.ToUpperInvariant())
    End Function

    Friend Function BuildCache(listCode As String, languageCode As String) As ElitaListItem()
        Return CommonRepository.GetList(listCode, languageCode)
    End Function


    Friend Function CacheKeyForLabelTranslations(uiCode As String, languageCode As String) As String
        Return String.Format("{0}#{1}#{2}", CacheKeyValue, uiCode.ToUpperInvariant(), languageCode.ToUpperInvariant())
    End Function

    Friend Function BuildCacheForLabelTranslations(uiCode As String, languageCode As String) As IEnumerable(Of LabelTranslation)
        Return CommonRepository.GetLableTranslation(uiCode, languageCode)
    End Function

    Friend Function GetPolicy() As CacheItemPolicy Implements ICacheManager.GetPolicy
        Dim policy As CacheItemPolicy
        policy = New CacheItemPolicy()
        policy.SlidingExpiration = New TimeSpan(0, 30, 0) ' 30 Mins
        Return policy
    End Function

End Class
