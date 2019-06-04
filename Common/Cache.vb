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
    Public Sub New(ByVal commonEntryExpirationTimeInMin As Long)
        Me.expTimeInMin = commonEntryExpirationTimeInMin
        Me.dalLookupTable = New dalLookupTable
    End Sub

    Public Sub New()
        Me.expTimeInMin = DEFAULT_EXPIRATION_TIME
        Me.dalLookupTable = New dalLookupTable
    End Sub
#End Region

#Region "Class Methods"
    'Expires according to Cache Configuration
    Public Sub AddEntry(ByVal key As String, ByVal obj As Object)
        Me.AddEntry(key, obj, Me.expTimeInMin)
    End Sub

    'Never Expires
    Public Sub AddPermanentEntry(ByVal key As String, ByVal obj As Object)
        Me.AddEntry(key, obj, NEVER_EXPIRE_AGE)
    End Sub

    'Expires according to "expTimeInMin" parameter value 
    '"expTimeInMin" = 0 WILL NEVER EXPIRE
    Public Sub AddEntry(ByVal key As String, ByVal obj As Object, ByVal expTimeInMin As Long)
        Dim entry As New CacheEntry
        With entry
            .obj = obj
            If expTimeInMin = 0 Then
                .expireAt = Date.Now.AddYears(100) 'Never Expires
            Else
                .expireAt = Date.Now.AddMinutes(expTimeInMin)
            End If
        End With
        If Me.cacheTable.ContainsKey(key) Then
            Throw New CacheException(DUPLICATE_ENTRY_ERR_MSG)
        Else
            Me.cacheTable.Add(key, entry)
        End If
    End Sub

    Public Function GetEntry(ByVal key As String) As Object
        Dim entry As CacheEntry = Me.cacheTable.Item(key)
        Dim obj As Object = Nothing
        If Not entry Is Nothing Then
            If entry.expireAt <= Date.Now Then
                Me.InvalidateEntry(key)
            Else
                obj = entry.obj
            End If
        End If
        Return obj
    End Function

    Public Sub InvalidateEntry(ByVal key As String)
        Me.cacheTable.Remove(key)
    End Sub

    Public Sub InvalidateAllEntries()
        Me.cacheTable.Clear()
    End Sub

    Public Function GetDalLookupTable() As dalLookupTable
        Return Me.dalLookupTable
    End Function
#End Region

#Region "CacheEntry Class"
    Private Class CacheEntry
        Public obj As Object
        Public expireAt As Date
    End Class
#End Region


End Class
