Imports Assurant.ElitaPlus.DataAccessInterface
Imports Assurant.ElitaPlus.DataEntities

Public NotInheritable Class CommonRepository(Of TType As {BaseEntity, IRecordCreateModifyInfo, ICommonEntity})
    Inherits Repository(Of TType, CommonContext)
    Implements ICommonRepository(Of TType)

    Public Sub New()
        MyBase.New(New Lazy(Of CommonContext)())
    End Sub

    Public Function GetList(ByVal listCode As String, ByVal languageCode As String) As IEnumerable(Of ElitaListItem) Implements ICommonRepository(Of TType).GetList
        Dim QueryForListItems = (From li As ListItem In Context.ListItems
                                 Join l As List In Context.Lists On l.ListId Equals li.ListId
                                 Join dit As DictionaryItemTranslation In Context.DictionaryItemTranslations On dit.DictionaryItemId Equals li.DictionaryItemId
                                 Where dit.Language.Code = languageCode And l.Code = listCode
                                 Select li.Code, dit.Translation, li.ListItemId, li.ExtendedCode)

        Dim result = New List(Of ElitaListItem)
        For Each Item In QueryForListItems
            Dim ElitaItem As ElitaListItem
            ElitaItem.Code = Item.Code
            ElitaItem.Description = Item.Translation
            ElitaItem.ExtendedCode = Item.ExtendedCode
            ElitaItem.ListItemId = Item.ListItemId
            result.Add(ElitaItem)
        Next

        Return result.ToArray()
    End Function

    Private Function GetLabelTranslation(uiProgCode As String, ByVal Optional languageCode As String = "") As IEnumerable(Of LabelTranslation) Implements ICommonRepository(Of TType).GetLableTranslation
        If (languageCode = String.Empty) Then
            languageCode = LanguageCodes.USEnglish
        End If
        Return GetTranslation(uiProgCode, languageCode)

    End Function

    Private Function GetTranslation(uiProgCode As String, languageCode As String) As IEnumerable(Of LabelTranslation)
        Return (From l As Label In Context.Labels
                Join di As DictionaryItem In Context.DictionaryItems On l.DictionaryItemId Equals di.DictionaryItemId
                Join dit As DictionaryItemTranslation In Context.DictionaryItemTranslations On dit.DictionaryItemId Equals l.DictionaryItemId
                Where dit.Language.Code = languageCode And l.UiProgCode = uiProgCode
                Select New LabelTranslation() With
                    {
                    .LabelId = l.LabelId,
                    .Translation = dit.Translation,
                    .uiProgCode = uiProgCode
                    }).AsEnumerable()

        'Where l.UiProgCode = uiProgCode).FirstOrDefault.dit.Translation
    End Function

End Class

