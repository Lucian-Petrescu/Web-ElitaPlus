Imports Assurant.ElitaPlus.DataEntities

Public Interface ICommonManager
    Function GetListItems(pCode As String) As ElitaListItem()
    Function GetListItems(pCode As String, pLanguageCode As String) As ElitaListItem()
    Function GetLabelTranslations(pUiCode As String) As IEnumerable(Of LabelTranslation)
    Function GetLabelTranslations(pUiCode As String, pLanguageCode As String) As IEnumerable(Of Label)
    Function GetExpression(pExpressionId As Guid) As Expression
End Interface
