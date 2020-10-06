Imports System.Collections.Generic
Imports System.Runtime.CompilerServices
Imports Assurant.ElitaPlus.Business
Imports Assurant.ElitaPlus.DataEntities
Imports Assurant.ElitaPlus.Security
Imports Microsoft.Practices.Unity

Public Module ListHelper

    Private commonManager As ICommonManager

    Sub New()
        commonManager = ApplicationContext.Current.Container.Resolve(Of ICommonManager)()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    Public Enum ListValueType
        ''' <summary>
        ''' 
        ''' </summary>
        Code

        ''' <summary>
        ''' 
        ''' </summary>
        Description

        ''' <summary>
        ''' 
        ''' </summary>
        ExtendedCode

        ''' <summary>
        ''' 
        ''' </summary>
        Id
    End Enum

    ''' <summary>
    ''' 
    ''' </summary>
    Public Enum PopulateBehavior
        ''' <summary>
        ''' 
        ''' </summary>
        None

        ''' <summary>
        ''' 
        ''' </summary>
        AddBlankListItem

        ''' <summary>
        ''' 
        ''' </summary>
        AddSelectListItem
    End Enum

    ''' <summary>
    ''' Renaming Populate to PopulateOld to aviod conflits 
    ''' </summary>
    ''' <param name="pListControl"></param>
    ''' <param name="pListCode"></param>
    ''' <param name="pTextValueType"></param>
    ''' <param name="pValueValueType"></param>
    ''' <param name="pSpecialValueBehavior"></param>
    ''' <param name="pSpecialValue"></param>
    ''' <param name="pSortBy"></param>
    <Extension()>
    Public Sub PopulateOld(
                       pListControl As ListControl,
                       pListCode As String,
                       pTextValueType As ListValueType,
                       pValueValueType As ListValueType,
                       pSpecialValueBehavior As PopulateBehavior,
                       pSpecialValue As String,
                       pSortBy As ListValueType)
        Dim i As Integer

        pListControl.Items.Clear()

        If pSpecialValueBehavior = PopulateBehavior.AddBlankListItem Then
            pListControl.Items.Add(New WebControls.ListItem(String.Empty, pSpecialValue))
        End If

        If pSpecialValueBehavior = PopulateBehavior.AddSelectListItem Then
            pListControl.Items.Add(New WebControls.ListItem("Select", pSpecialValue))
        End If

        '' Get List from Manager?
        Dim data As IEnumerable(Of DataEntities.ElitaListItem)
        data = commonManager.GetListItems(pListCode, Threading.Thread.CurrentPrincipal.GetLanguageCode())


        Dim oElitaListItems As ElitaListItem()
        oElitaListItems = If(TryCast(data, ElitaListItem()), DirectCast(data, ElitaListItem()))
        If (data Is Nothing) OrElse (oElitaListItems Is Nothing) OrElse oElitaListItems.Length = 0 Then
            Exit Sub
        End If

        Select Case pSortBy
            Case ListValueType.Code
                data = oElitaListItems.OrderBy("Code", LinqExtentions.SortDirection.Ascending)
            Case ListValueType.Description
                data = oElitaListItems.OrderBy("Description", LinqExtentions.SortDirection.Ascending)
            Case ListValueType.ExtendedCode
                data = oElitaListItems.OrderBy("ExtendedCode", LinqExtentions.SortDirection.Ascending)
            Case ListValueType.Id
                data = oElitaListItems.OrderBy("ListItemId", LinqExtentions.SortDirection.Ascending)
            Case Else
                Throw New NotSupportedException()
        End Select

        'For i = 0 To data.Count - 1
        '    pListControl.Items.Add(New ListItem(data(i).GetValue(pValueValueType)))
        'Next
        'pListControl.DataSource = data.Select(Function(e) New ListItem(e.GetValue(pTextValueType), e.GetValue(pValueValueType))).ToList()

        For Each oItem As ElitaListItem In oElitaListItems
            pListControl.Items.Add(New WebControls.ListItem(oItem.GetValue(pTextValueType), oItem.GetValue(pValueValueType)))
        Next


        'pListControl.Items.AddRange(
        '    data.Select(
        '        Function(eli) New WebControls.ListItem(
        '            eli.GetValue(pTextValueType),
        '            eli.GetValue(pValueValueType))))

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pValue"></param>
    ''' <param name="pValueType"></param>
    ''' <returns></returns>
    <Extension()>
    Private Function GetValue(pValue As DataEntities.ElitaListItem, pValueType As ListValueType) As String
        Select Case pValueType
            Case ListValueType.Code
                Return pValue.Code
            Case ListValueType.Description
                Return pValue.Description
            Case ListValueType.ExtendedCode
                Return pValue.ExtendedCode
            Case ListValueType.Id
                Return pValue.ListItemId.ToString()
            Case Else
                Throw New NotSupportedException()
        End Select

    End Function



End Module

