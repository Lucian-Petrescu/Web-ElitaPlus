#Region "TranslationItem"

Public Class TranslationItem

    Public TextToTranslate As String
    Public Translation As String
    Public ControlID As String
End Class

#End Region

#Region "TranslationItemArray"


Public Class TranslationItemArray
    Inherits CollectionBase

    Private mList As New Hashtable

    Public Sub Add(oControlInfo As TranslationItem)

        'ARF 9/13/04. Commented out to avoid duplicate keys. mList.Add(oControlInfo.TextToTranslate, oControlInfo)
        mList.Item(oControlInfo.TextToTranslate) = oControlInfo

    End Sub

    Public ReadOnly Property Items As ICollection

        Get
            Return mList.Values
        End Get

    End Property

    Public ReadOnly Property CurrentCount As Integer

        Get

            Return mList.Count

        End Get

    End Property


    Public Function Item(sUniqueID As String) As TranslationItem


        Return CType(mList.Item(sUniqueID), TranslationItem)


    End Function

    Public Function Contains(SearchCriteria As String) As Boolean

        If mList.ContainsKey(SearchCriteria) Then

            Return True

        Else

            Return False

        End If


    End Function

End Class

#End Region