Imports System.Globalization

Public Class GenericConstants

    Public Const WILDCARD As Char = "%"
    '  Public Shared ReadOnly INFINITE_DATE As String = "12/31/2999".ToString(CultureInfo.CurrentCulture)
    Public Shared ReadOnly INFINITE_DATE_CONS As String = "12/31/2999"
    Public Shared ReadOnly INFINITE_DATE As Date = Date.Parse(INFINITE_DATE_CONS, System.Globalization.CultureInfo.InvariantCulture)
    Public Const ELITA_PLUS_VERSION As String = "19.1.18.0"
End Class
