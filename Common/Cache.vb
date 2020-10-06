Public Class Cache

#Region "Constants"
    Private Const DEFAULT_EXPIRATION_TIME As Long = 20 '20 minutes
    Public Const DUPLICATE_ENTRY_ERR_MSG As String = "DUPLICATE_ENTRY_ERR_MSG"
    Public Const NEVER_EXPIRE_AGE As Long = 0
#End Region

#Region "Private Attributes"
    Private cacheTable As Hashtable = New Hashtable
    Private dalLookupTable As New dalLookupTable
    Private expTimeInMin As Long
#End Region

#Region "Constructors"
    Public Sub New(commonEntryExpirationTimeInMin As Long)
        expTimeInMin = commonEntryExpirationTimeInMin
        dalLookupTable = New dalLookupTable
    End Sub

    Public Sub New()
        expTimeInMin = DEFAULT_EXPIRATION_TIME
        dalLookupTable = New dalLookupTable
    End Sub
#End Region

#Region "Class Methods"
    'Expires according to Cache Configuration
    Public Sub AddEntry(key As String, obj As Object)
        AddEntry(key, obj, expTimeInMin)
    End Sub

    'Never Expires
    Public Sub AddPermanentEntry(key As String, obj As Object)
        AddEntry(key, obj, NEVER_EXPIRE_AGE)
    End Sub

    'Expires according to "expTimeInMin" parameter value 
    '"expTimeInMin" = 0 WILL NEVER EXPIRE
    Public Sub AddEntry(key As String, obj As Object, expTimeInMin As Long)
        Dim entry As New CacheEntry
        With entry
            .obj = obj
            If expTimeInMin = 0 Then
                .expireAt = Date.Now.AddYears(100) 'Never Expires
            Else
                .expireAt = Date.Now.AddMinutes(expTimeInMin)
            End If
        End With
        If cacheTable.ContainsKey(key) Then
            Throw New CacheException(DUPLICATE_ENTRY_ERR_MSG)
        Else
            cacheTable.Add(key, entry)
        End If
    End Sub

    Public Function GetEntry(key As String) As Object
        Dim entry As CacheEntry = cacheTable.Item(key)
        Dim obj As Object = Nothing
        If Not entry Is Nothing Then
            If entry.expireAt <= Date.Now Then
                InvalidateEntry(key)
            Else
                obj = entry.obj
            End If
        End If
        Return obj
    End Function

    Public Sub InvalidateEntry(key As String)
        cacheTable.Remove(key)
    End Sub

    Public Sub InvalidateAllEntries()
        cacheTable.Clear()
    End Sub

    Public Function GetDalLookupTable() As dalLookupTable
        Return dalLookupTable
    End Function
#End Region

#Region "CacheEntry Class"
    Private Class CacheEntry
        Public obj As Object
        Public expireAt As Date
    End Class
#End Region


End Class
