
Imports Assurant.ElitaPlus.DataEntities

Public Class CommonManager
    Implements ICommonManager

    Private ReadOnly m_CacheFacade As ICacheFacade

    Public Sub New(cacheFacade As ICacheFacade)
        m_CacheFacade = cacheFacade
    End Sub

    Private ReadOnly Property CacheFacade As ICacheFacade
        Get
            Return m_CacheFacade
        End Get
    End Property

    Public Function GetListItems(pCode As String) As ElitaListItem() Implements ICommonManager.GetListItems
        Return CacheFacade.GetList(pCode.ToUpperInvariant())

    End Function

    Public Function GetListItems(pCode As String, pLanguageCode As String) As ElitaListItem() Implements ICommonManager.GetListItems
        Return CacheFacade.GetList(pCode.ToUpperInvariant(), pLanguageCode.ToUpperInvariant())

    End Function

    Public Function GetLabelTranslations(pUiCode As String) As IEnumerable(Of LabelTranslation) Implements ICommonManager.GetLabelTranslations
        Return CacheFacade.GetLabelTranslation(pUiCode.ToUpperInvariant())
    End Function

    Public Function GetLabelTranslations(pUiCode As String, pLanguageCode As String) As IEnumerable(Of Label) Implements ICommonManager.GetLabelTranslations
        Return CacheFacade.GetLabelTranslation(pUiCode.ToUpperInvariant(), pLanguageCode)
    End Function

    Public Function GetExpression(pExpressionId As Guid) As Expression Implements ICommonManager.GetExpression
        Return CacheFacade.GetExpression(pExpressionId)
    End Function
End Class
