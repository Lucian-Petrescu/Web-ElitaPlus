Imports Assurant.ElitaPlus.Business
Imports Assurant.ElitaPlus.DataEntities
Imports Assurant.ElitaPlus.Security

Public Class CurrencyManager
    Implements ICurrencyManager

    Private ReadOnly m_CacheFacade As ICacheFacade

    Public Sub New(ByVal pCacheFacade As ICacheFacade)
        m_CacheFacade = pCacheFacade
    End Sub

    Private ReadOnly Property CacheFacade As ICacheFacade
        Get
            Return m_CacheFacade
        End Get
    End Property

    Public Function GetCurrency(pCurrencyId As Guid) As Currency Implements ICurrencyManager.GetCurrency
        Dim oCurrency As Currency = CacheFacade.GetCurrency(pCurrencyId)

        Return oCurrency
    End Function

End Class
