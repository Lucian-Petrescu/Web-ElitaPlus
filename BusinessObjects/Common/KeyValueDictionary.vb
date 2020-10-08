Imports System.Text
Imports System.Collections.Generic

Public Class KeyValueDictionary
    Inherits Dictionary(Of String, String)

    Public Const DEFAULT_KEY_VALUE_SEPERATOR As Char = ":"
    Public Const DEFAULT_PAIR_SEPERATOR As Char = ";"
    Public Const DEFAULT_DELIMITER As Char = """"

    Public Sub New()
        Me.New(DEFAULT_KEY_VALUE_SEPERATOR, DEFAULT_PAIR_SEPERATOR, DEFAULT_DELIMITER, String.Empty)
    End Sub

    Public Sub New(keyValueSeperator As Char, pairSeperator As Char, delimiter As Char)
        Me.New(keyValueSeperator, pairSeperator, delimiter, String.Empty)
    End Sub

    Public Sub New(input As String)
        Me.New(DEFAULT_KEY_VALUE_SEPERATOR, DEFAULT_PAIR_SEPERATOR, DEFAULT_DELIMITER, input)
    End Sub

    Public Sub New(keyValueSeperator As Char, pairSeperator As Char, delimiter As Char, input As String)
        MyBase.New()
        Me.KeyValueSeperator = keyValueSeperator
        Me.PairSeperator = pairSeperator
        Me.Delimiter = delimiter

        If (Not (input Is Nothing OrElse String.IsNullOrEmpty(input.Trim()))) Then
            Dim key As String
            Dim value As String
            Dim keyValuePairStringArray As String()
            For Each keyValuePairString As String In input.Split(New Char() {Me.PairSeperator})
                keyValuePairStringArray = keyValuePairString.Split(New Char() {Me.KeyValueSeperator})
                key = keyValuePairStringArray(0).TrimStart(New Char() {Me.Delimiter}).TrimEnd(New Char() {Me.Delimiter})
                value = keyValuePairStringArray(1).TrimStart(New Char() {Me.Delimiter}).TrimEnd(New Char() {Me.Delimiter})
                MyBase.Add(key, value)
            Next
        End If
    End Sub

    Private _KeyValueSeperator As Char
    Private _pairSeperator As Char
    Private _delimiter As Char

    Public Property KeyValueSeperator As Char
        Get
            Return _KeyValueSeperator
        End Get
        Private Set
            _KeyValueSeperator = value
        End Set
    End Property

    Public Property PairSeperator As Char
        Get
            Return _pairSeperator
        End Get
        Private Set
            _pairSeperator = value
        End Set
    End Property

    Public Property Delimiter As Char
        Get
            Return _delimiter
        End Get
        Private Set
            _delimiter = value
        End Set
    End Property

    Public Overrides Function ToString() As String
        Return ToString(False)
    End Function

    Public Overloads Function ToString(useDelimiter As Boolean) As String
        Dim sb As New StringBuilder
        Dim delimiter As String = String.Empty
        Dim notFirstEntry As Boolean = False
        For Each entry As KeyValuePair(Of String, String) In Me
            If (useDelimiter) Then delimiter = Me.Delimiter.ToString()
            If (notFirstEntry) Then
                sb.Append(PairSeperator.ToString())
            End If
            sb.AppendFormat("{0}{1}{0}{2}{0}{3}{0}", delimiter, entry.Key, KeyValueSeperator.ToString(), entry.Value)
            notFirstEntry = True
        Next
        Return sb.ToString()
    End Function



End Class

