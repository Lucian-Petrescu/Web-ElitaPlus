Imports Assurant.ElitaPlus.DataEntities

Public Interface ICommonRepository(Of TEntity As {BaseEntity, ICommonEntity})
    Inherits IRepository(Of TEntity)

    Function GetList(ByVal listCode As String, ByVal languageCode As String) As IEnumerable(Of ElitaListItem)

    Function GetLableTranslation(ByVal uiCode As String, ByVal Optional languageCode As String = "") As IEnumerable(Of LabelTranslation)

End Interface