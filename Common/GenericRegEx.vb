Imports System.Text.RegularExpressions
Imports Microsoft.VisualBasic

#Region "Constants"

Public Enum CaseValues
    UpperCase = 1
    LowerCase = 2
    Both = 3
End Enum


Public Enum RegExTypeValues
    SpaceRegEx = 1
    NumericRegEx = 2
    AlphaRegEx = 3
    AlphaNumericRegEx = 4
    SpecialCharRegEx = 5
End Enum

#End Region

#Region "Main Class"

Public Class RegExConstants
    Public Const EMAIL_REGEX As String = "^[\w\.\-]+@[a-zA-Z0-9\-]+(\.[a-zA-Z0-9\-]{1,})*(\.[a-zA-Z]{2,3}){1,2}$"
    Public Const TIME_REGEX As String = "^(((20|21|22|23|[01]\d|\d)(([:.][0-5]\d){1,2}))|((10|11|12|[0]*[1-9])(([:.][0-5]\d){1,2})[\s]*[A|P]M))$"
    Public Const QUERY_STRING_REGEX As String = "^(([A-Z][\w]*)=[\w]+(&([A-Z][\w]*)=[\w]+)*){0,1}$"

End Class

Public Class GenericRegEx
    Private regExType As Integer
    Private optionalVal As Boolean
    Private minLength As Integer
    Private maxLength As Integer
    Private disallowedVal As String



    Public Sub New(rExType As Integer, isOptional As Boolean, minLen As Integer, maxLen As Integer, Optional ByVal disallowedValueList As String = "")
        RegularExType = rExType
        OptionalValue = isOptional
        MinimumLength = minLen
        MaximumLength = maxLen
        DisallowedValues = disallowedValueList
        If OptionalValue Then
            MinimumLength = 0
        End If
        If MaximumLength < MinimumLength Then
            Throw New Exception("Maximum Length Cannot be Less than Minimum Length")
        ElseIf MaximumLength = 0 Then
            Throw New Exception("Maximum Length Cannot be Zero")
        End If
    End Sub
    Public Sub New(rExType As Integer, isOptional As Boolean, maxLen As Integer, Optional ByVal disallowedValueList As String = "")
        RegularExType = rExType
        OptionalValue = isOptional
        MinimumLength = 0
        MaximumLength = maxLen
        DisallowedValues = disallowedValueList
        If OptionalValue Then
            MinimumLength = 0
        End If
        If MaximumLength < MinimumLength Then
            Throw New Exception("Maximum Length Cannot be Less than Minimum Length")

        End If
    End Sub


    Public Property RegularExType() As Integer
        Get
            Return regExType
        End Get
        Set(Value As Integer)
            regExType = Value
        End Set
    End Property

    Public Property OptionalValue() As Boolean
        Get
            Return optionalVal
        End Get
        Set(Value As Boolean)
            optionalVal = Value
        End Set
    End Property

    Public Property MinimumLength() As Integer
        Get
            Return minLength
        End Get
        Set(Value As Integer)
            minLength = Value
        End Set
    End Property

    Public Property MaximumLength() As Integer
        Get
            Return maxLength
        End Get
        Set(Value As Integer)
            maxLength = Value
        End Set
    End Property

    Public Property DisallowedValues() As String
        Get
            Return disallowedVal
        End Get
        Set(Value As String)
            disallowedVal = Value
        End Set
    End Property

    Public Overridable ReadOnly Property RegExString() As String
        Get
            Dim retStr As String = ""
            If MaximumLength > 0 Then
                If MinimumLength = MaximumLength Then
                    retStr = "{" & MaximumLength.ToString() & "}"
                Else
                    retStr = "{" & MinimumLength.ToString() & "," & MaximumLength.ToString() & "}"
                End If
            End If
            Return retStr
        End Get
    End Property

End Class

#End Region

#Region "Space Regular Expression Class"

Public Class SpaceRegEx
    Inherits GenericRegEx
    Sub New(isOptional As Boolean, minLen As Integer, maxLen As Integer)
        MyBase.New(RegExTypeValues.SpaceRegEx, isOptional, minLen, maxLen)
    End Sub
    Public Overrides ReadOnly Property RegExString() As String
        Get
            Return "\s" & (MyBase.RegExString())
        End Get
    End Property
End Class

#End Region

#Region "Numeric Regular Expression Class"

Public Class NumericRegEx
    Inherits GenericRegEx

    Private numericString As String = "0,1,2,3,4,5,6,7,8,9"

    Sub New(isOptional As Boolean, minLen As Integer, _
                      maxLen As Integer, Optional ByVal disallowedValueList As String = "")
        MyBase.New(RegExTypeValues.NumericRegEx, isOptional, minLen, maxLen, disallowedValueList)
    End Sub

    Private Function GetAllCharSet() As String
        If DisallowedValues.Trim().Length = 0 Then
            'Return "[0-9]"
            Return "\d"
        Else
            Dim numericArray As String() = numericString.Split(",".Chars(0))
            Dim disallowedArray As String() = DisallowedValues.Split(",".Chars(0))
            Dim retStr As String = ""
            For iNumericArray As Integer = 0 To numericArray.Length - 1
                Dim bValueDisallowed As Boolean = False
                For iDisallowedArray As Integer = 0 To disallowedArray.Length - 1
                    If numericArray(iNumericArray) = disallowedArray(iDisallowedArray) Then
                        bValueDisallowed = True
                        Exit For
                    End If
                Next iDisallowedArray
                If Not bValueDisallowed Then
                    retStr = retStr & numericArray(iNumericArray)
                End If
            Next iNumericArray
            Return "[" & retStr & "]"
        End If
    End Function

    Private Function GetNumericCharSet() As String
        If DisallowedValues.Trim().Length = 0 Then
            'Return "[0-9]"
            Return "\d"
        Else
            Dim numericArray As String() = numericString.Split(",".Chars(0))
            Dim disallowedArray As String() = DisallowedValues.Split(",".Chars(0))
            Dim allowedValues As String = ""
            For iNumericArray As Integer = 0 To numericArray.Length - 1
                Dim bValueDisallowed As Boolean = False
                For iDisallowedArray As Integer = 0 To disallowedArray.Length - 1
                    If numericArray(iNumericArray) = disallowedArray(iDisallowedArray) Then
                        bValueDisallowed = True
                        Exit For
                    End If
                Next iDisallowedArray
                If (Not bValueDisallowed) Then
                    If allowedValues.Trim().Length > 0 Then
                        allowedValues = allowedValues & "," & numericArray(iNumericArray)
                    Else
                        allowedValues = numericArray(iNumericArray)
                    End If
                End If
            Next iNumericArray
            Dim allowedArray As String() = allowedValues.Split(",".Chars(0))

            Dim retStr As String = ""
            If allowedArray.Length > 0 Then
                retStr = allowedArray(0)
                If allowedArray.Length > 1 Then
                    If (CType(allowedArray(1), Integer) - CType(allowedArray(0), Integer)) > 1 Then
                        retStr = retStr & allowedArray(1)
                    End If
                End If
            End If
            For iAllowedArray As Integer = 1 To allowedArray.Length - 1
                If (iAllowedArray <> 1) And ((CType(allowedArray(iAllowedArray), Integer) - CType(allowedArray(iAllowedArray - 1), Integer)) > 1) Then
                    retStr = retStr & allowedArray(iAllowedArray - 1) & allowedArray(iAllowedArray)
                ElseIf retStr.Substring(retStr.Length - 1, 1) <> "-" Then
                    retStr = retStr & "-"
                End If
            Next iAllowedArray
            If retStr.Length > 0 Then
                If retStr.Substring(retStr.Length - 1, 1) = "-" Then
                    retStr = retStr & allowedArray(allowedArray.Length - 1)
                End If
            Else
                Throw New Exception("You are eliminating all numeric values, so it is not a valid expression")
            End If
            Return "[" & retStr & "]"
        End If

    End Function

    Public Overrides ReadOnly Property RegExString() As String
        Get
            Return GetNumericCharSet() & (MyBase.RegExString())
        End Get
    End Property
End Class

#End Region

#Region "Alpha Regular Expression Class"

Public Class AlphaRegEx
    Inherits GenericRegEx

    Private upperAlphaString As String = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z"
    Private lowerAlphaString As String = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z"
    Private iCase As Integer

    Public Property CaseType() As Integer
        Get
            Return iCase
        End Get
        Set(Value As Integer)
            iCase = Value
        End Set
    End Property

    Sub New(isOptional As Boolean, minLen As Integer, _
                      maxLen As Integer, Optional ByVal iCaseType As CaseValues = CaseValues.Both, Optional ByVal disallowedValueList As String = "")
        MyBase.New(RegExTypeValues.AlphaRegEx, isOptional, minLen, maxLen, disallowedValueList)
        CaseType = iCaseType
    End Sub
    Private Function GetAllowedCharSet(charString As String) As String
        'CHR(65) = A
        'CHR(97) = a
        Dim charArray As String() = charString.Split(",".Chars(0))
        Dim disallowedArray As String() = DisallowedValues.Split(",".Chars(0))
        Dim allowedValues As String = ""
        For icharArray As Integer = 0 To charArray.Length - 1
            Dim bValueDisallowed As Boolean = False
            For iDisallowedArray As Integer = 0 To disallowedArray.Length - 1
                If charArray(icharArray) = disallowedArray(iDisallowedArray) Then
                    bValueDisallowed = True
                    Exit For
                End If
            Next iDisallowedArray
            If (Not bValueDisallowed) Then
                If allowedValues.Trim().Length > 0 Then
                    allowedValues = allowedValues & "," & charArray(icharArray)
                Else
                    allowedValues = charArray(icharArray)
                End If
            End If
        Next icharArray
        Dim allowedArray As String() = allowedValues.Split(",".Chars(0))

        Dim retStr As String = ""
        If allowedArray.Length > 0 Then
            retStr = allowedArray(0)
            If allowedArray.Length > 1 Then
                If (Asc(allowedArray(1)) - Asc(allowedArray(0))) > 1 Then
                    retStr = retStr & allowedArray(1)
                End If
            End If
        End If
        For iAllowedArray As Integer = 1 To allowedArray.Length - 1
            If (iAllowedArray <> 1) And ((Asc(allowedArray(iAllowedArray)) - Asc(allowedArray(iAllowedArray - 1))) > 1) Then
                retStr = retStr & allowedArray(iAllowedArray - 1) & allowedArray(iAllowedArray)
            ElseIf retStr.Substring(retStr.Length - 1, 1) <> "-" Then
                retStr = retStr & "-"
            End If
        Next iAllowedArray
        If retStr.Length > 0 Then
            If retStr.Substring(retStr.Length - 1, 1) = "-" Then
                retStr = retStr & allowedArray(allowedArray.Length - 1)
            End If
        Else
            Throw New Exception("You are eliminating all alphabets values, so it is not a valid expression")
        End If
        Return retStr
    End Function

    Private Function GetAlphaCharSet() As String
        If DisallowedValues.Trim().Length = 0 Then
            Select Case CaseType
                Case CaseValues.Both
                    Return "[A-Za-z]"
                Case CaseValues.UpperCase
                    Return "[A-Z]"
                Case CaseValues.LowerCase
                    Return "[a-z]"
            End Select
        Else
            Dim retStr As String = ""
            Select Case CaseType
                Case CaseValues.Both
                    retStr = GetAllowedCharSet(upperAlphaString) & GetAllowedCharSet(lowerAlphaString)
                Case CaseValues.UpperCase
                    retStr = GetAllowedCharSet(upperAlphaString)
                Case CaseValues.LowerCase
                    retStr = GetAllowedCharSet(lowerAlphaString)
            End Select
            Return "[" & retStr & "]"
        End If
    End Function

    Public Overrides ReadOnly Property RegExString() As String
        Get
            Return GetAlphaCharSet() & (MyBase.RegExString())
        End Get
    End Property
End Class

#End Region

#Region "Alpha Numeric Regular Expression Class"

Public Class AlphaNumericRegEx
    Inherits GenericRegEx

    Private upperAlphaString As String = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z"
    Private lowerAlphaString As String = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z"
    Private iCase As Integer
    Private numericString As String = "0,1,2,3,4,5,6,7,8,9"

    Sub New(isOptional As Boolean, minLen As Integer, _
                         maxLen As Integer, Optional ByVal iCaseType As CaseValues = CaseValues.Both, Optional ByVal disallowedValueList As String = "")
        MyBase.New(RegExTypeValues.AlphaNumericRegEx, isOptional, minLen, maxLen, disallowedValueList)
        CaseType = iCaseType
    End Sub

    Private Function GetNumericCharSet() As String
        If DisallowedValues.Trim().Length = 0 Then
            Return "0-9"
        Else
            Dim numericArray As String() = numericString.Split(",".Chars(0))
            Dim disallowedArray As String() = DisallowedValues.Split(",".Chars(0))
            Dim allowedValues As String = ""
            For iNumericArray As Integer = 0 To numericArray.Length - 1
                Dim bValueDisallowed As Boolean = False
                For iDisallowedArray As Integer = 0 To disallowedArray.Length - 1
                    If numericArray(iNumericArray) = disallowedArray(iDisallowedArray) Then
                        bValueDisallowed = True
                        Exit For
                    End If
                Next iDisallowedArray
                If (Not bValueDisallowed) Then
                    If allowedValues.Trim().Length > 0 Then
                        allowedValues = allowedValues & "," & numericArray(iNumericArray)
                    Else
                        allowedValues = numericArray(iNumericArray)
                    End If
                End If
            Next iNumericArray
            Dim allowedArray As String() = allowedValues.Split(",".Chars(0))

            Dim retStr As String = ""
            If allowedArray.Length > 0 Then
                retStr = allowedArray(0)
                If allowedArray.Length > 1 Then
                    If (CType(allowedArray(1), Integer) - CType(allowedArray(0), Integer)) > 1 Then
                        retStr = retStr & allowedArray(1)
                    End If
                End If
            End If
            For iAllowedArray As Integer = 1 To allowedArray.Length - 1
                If (iAllowedArray <> 1) And ((CType(allowedArray(iAllowedArray), Integer) - CType(allowedArray(iAllowedArray - 1), Integer)) > 1) Then
                    retStr = retStr & allowedArray(iAllowedArray - 1) & allowedArray(iAllowedArray)
                ElseIf retStr.Substring(retStr.Length - 1, 1) <> "-" Then
                    retStr = retStr & "-"
                End If
            Next iAllowedArray
            If retStr.Length > 0 Then
                If retStr.Substring(retStr.Length - 1, 1) = "-" Then
                    retStr = retStr & allowedArray(allowedArray.Length - 1)
                End If
            End If
            Return retStr
        End If
    End Function

    Public Property CaseType() As Integer
        Get
            Return iCase
        End Get
        Set(Value As Integer)
            iCase = Value
        End Set
    End Property

    Private Function GetAllowedCharSet(charString As String) As String
        'CHR(65) = A
        'CHR(97) = a
        Dim charArray As String() = charString.Split(",".Chars(0))
        Dim disallowedArray As String() = DisallowedValues.Split(",".Chars(0))
        Dim allowedValues As String = ""
        For icharArray As Integer = 0 To charArray.Length - 1
            Dim bValueDisallowed As Boolean = False
            For iDisallowedArray As Integer = 0 To disallowedArray.Length - 1
                If charArray(icharArray) = disallowedArray(iDisallowedArray) Then
                    bValueDisallowed = True
                    Exit For
                End If
            Next iDisallowedArray
            If (Not bValueDisallowed) Then
                If allowedValues.Trim().Length > 0 Then
                    allowedValues = allowedValues & "," & charArray(icharArray)
                Else
                    allowedValues = charArray(icharArray)
                End If
            End If
        Next icharArray
        Dim allowedArray As String() = allowedValues.Split(",".Chars(0))

        Dim retStr As String = ""
        If allowedArray.Length > 0 Then
            retStr = allowedArray(0)
            If allowedArray.Length > 1 Then
                If (Asc(allowedArray(1)) - Asc(allowedArray(0))) > 1 Then
                    retStr = retStr & allowedArray(1)
                End If
            End If
        End If
        For iAllowedArray As Integer = 1 To allowedArray.Length - 1
            If (iAllowedArray <> 1) And ((Asc(allowedArray(iAllowedArray)) - Asc(allowedArray(iAllowedArray - 1))) > 1) Then
                retStr = retStr & allowedArray(iAllowedArray - 1) & allowedArray(iAllowedArray)
            ElseIf retStr.Substring(retStr.Length - 1, 1) <> "-" Then
                retStr = retStr & "-"
            End If
        Next iAllowedArray
        If retStr.Length > 0 Then
            If retStr.Substring(retStr.Length - 1, 1) = "-" Then
                retStr = retStr & allowedArray(allowedArray.Length - 1)
            End If
        End If
        Return retStr
    End Function
    Private Function GetAlphaCharSet() As String
        If DisallowedValues.Trim().Length = 0 Then
            Select Case CaseType
                Case CaseValues.Both
                    Return "Aa-Zz"
                Case CaseValues.UpperCase
                    Return "A-Z"
                Case CaseValues.LowerCase
                    Return "a-z"
            End Select
        Else
            Dim retStr As String = ""
            Select Case CaseType
                Case CaseValues.Both
                    retStr = GetAllowedCharSet(upperAlphaString) & GetAllowedCharSet(lowerAlphaString)
                Case CaseValues.UpperCase
                    retStr = GetAllowedCharSet(upperAlphaString)
                Case CaseValues.LowerCase
                    retStr = GetAllowedCharSet(lowerAlphaString)
            End Select
            Return retStr
        End If
    End Function

    Private Function GetAlphaNumericCharSet() As String
        Dim retStr As String = GetNumericCharSet() & GetAlphaCharSet()
        If retStr.Trim().Length = 0 Then
            Throw New Exception("You are eliminating all numbers and alphabets , so it is not a valid expression")
        Else
            Return "[" & retStr & "]"
        End If
    End Function

    Public Overrides ReadOnly Property RegExString() As String
        Get
            Return GetAlphaNumericCharSet() & (MyBase.RegExString())
        End Get
    End Property
End Class

#End Region

#Region "Special Char Regular Expression Class"

Public Class SpecialCharRegEx
    Inherits GenericRegEx

    Private sSpecialChar As String
    Public Property SpecialChar() As String
        Get
            Return sSpecialChar
        End Get
        Set(Value As String)
            sSpecialChar = Value
        End Set
    End Property

    Sub New(isOptional As Boolean, minLen As Integer, maxLen As Integer, specialCharVal As String)
        MyBase.New(RegExTypeValues.SpecialCharRegEx, isOptional, maxLen)
        SpecialChar = specialCharVal
    End Sub

    Public Overrides ReadOnly Property RegExString() As String
        Get
            'Return SpecialChar & "\w" & (MyBase.RegExString())
            Return SpecialChar & (MyBase.RegExString())
        End Get
    End Property
End Class

#End Region

#Region "Generic RegEx Factory Class"

Public Class GenericRegExFactory
    Private regEx As GenericRegEx
    Private optionalVal As Boolean
    Private minLength As Integer
    Private maxLength As Integer
    Private disallowedVal As String
    Private specialChar As String
    Private caseType As Integer

    Public ReadOnly Property RegularExp() As GenericRegEx
        Get
            Return regEx
        End Get
    End Property

    Public Sub New(regExStr As String)
        If regExStr.Trim().Length > 0 Then
            ' determine the type...
            GetLengths(regExStr)
            If (regExStr.IndexOf("{") >= 0) Then
                specialChar = regExStr.Substring(0, regExStr.IndexOf("{"))
            Else
                specialChar = "~"
            End If

            If regExStr.StartsWith("\s") Then
                regEx = New SpaceRegEx(optionalVal, minLength, maxLength)
            ElseIf regExStr.StartsWith("\d") Then
                regEx = New NumericRegEx(optionalVal, minLength, maxLength)
            ElseIf HasAlphaNumericVal(regExStr) Then
                GetDisallowedValues(regExStr, RegExTypeValues.AlphaNumericRegEx)
                regEx = New AlphaNumericRegEx(optionalVal, minLength, maxLength, caseType, disallowedVal)
            ElseIf HasNumbers(regExStr) Then
                GetDisallowedValues(regExStr, RegExTypeValues.NumericRegEx)
                regEx = New NumericRegEx(optionalVal, minLength, maxLength, disallowedVal)
            ElseIf HasAlphaVal(regExStr) Then
                GetDisallowedValues(regExStr, RegExTypeValues.AlphaRegEx)
                regEx = New AlphaRegEx(optionalVal, minLength, maxLength, caseType, disallowedVal)
            Else
                regEx = New SpecialCharRegEx(optionalVal, minLength, maxLength, specialChar)
            End If
        End If
    End Sub

    Private Sub GetLengths(strVal As String)
        Dim openBracketStart As Integer = strVal.IndexOf("{")
        Dim closeBracketStart As Integer = strVal.IndexOf("}")

        If openBracketStart <> -1 And closeBracketStart <> -1 Then
            Dim tempStr As String = strVal.Substring(openBracketStart + 1, closeBracketStart - openBracketStart - 1)
            If tempStr.Trim().Length > 0 Then
                Dim tempLengths As String() = tempStr.Split(",".Chars(0))
                If tempLengths.Length >= 1 Then
                    optionalVal = (tempLengths(0) = "0")
                    If tempLengths.Length = 3 Then
                        minLength = CType(tempLengths(0), Integer)
                        maxLength = CType(tempLengths(2), Integer)
                    Else
                        minLength = CType(tempLengths(0), Integer)
                        maxLength = CType(tempLengths(0), Integer)
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub GetDisallowedValues(strVal As String, regExType As Integer)
        Dim upperAlphaString As String = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z"
        Dim lowerAlphaString As String = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z"
        Dim numericString As String = "0,1,2,3,4,5,6,7,8,9"
        Dim openBracketStart As Integer = strVal.IndexOf("[")
        Dim closeBracketStart As Integer = strVal.IndexOf("]")

        disallowedVal = ""
        If openBracketStart <> -1 And closeBracketStart <> -1 Then
            Dim tempStr As String = strVal.Substring(openBracketStart, closeBracketStart - openBracketStart + 1)
            If tempStr.Trim().Length > 0 Then
                tempStr = "^" + tempStr + "{1}$"

                Dim tempRegEx As Regex = New Regex(tempStr)
                Dim compareStr As String = ""
                Select Case regExType
                    Case RegExTypeValues.AlphaRegEx
                        compareStr = upperAlphaString & "," & lowerAlphaString
                    Case RegExTypeValues.AlphaNumericRegEx
                        compareStr = upperAlphaString & "," & lowerAlphaString & "," & numericString
                    Case RegExTypeValues.NumericRegEx
                        compareStr = numericString
                End Select

                For Each chrVal As Char In compareStr.Split(",".Chars(0))
                    Dim M As Match = tempRegEx.Match(chrVal)

                    If Not M.Success Then
                        If disallowedVal.Trim().Length = 0 Then
                            disallowedVal = chrVal
                        Else
                            disallowedVal = disallowedVal & "," & chrVal
                        End If
                    End If
                Next

                If (regExType = RegExTypeValues.AlphaRegEx) Or (regExType = RegExTypeValues.AlphaNumericRegEx) Then
                    If disallowedVal.Trim().IndexOf(upperAlphaString) <> -1 Then
                        caseType = CaseValues.LowerCase
                        If disallowedVal.Trim() = upperAlphaString.Trim() Then
                            disallowedVal = ""
                        Else
                            disallowedVal = disallowedVal.Trim().Replace(upperAlphaString.Trim(), ",")
                            disallowedVal = disallowedVal.Trim().Replace(",,", "")
                        End If
                    ElseIf disallowedVal.Trim().IndexOf(lowerAlphaString) <> -1 Then
                        caseType = CaseValues.UpperCase
                        If disallowedVal.Trim() = lowerAlphaString.Trim() Then
                            disallowedVal = ""
                        Else
                            disallowedVal = disallowedVal.Trim().Replace(lowerAlphaString.Trim(), ",")
                            disallowedVal = disallowedVal.Trim().Replace(",,", "")
                        End If
                    Else
                        caseType = CaseValues.Both
                    End If
                End If

            End If
        End If
    End Sub
    Private Function HasNumbers(strVal As String) As Boolean
        Dim openBracketStart As Integer = strVal.IndexOf("[")
        Dim closeBracketStart As Integer = strVal.IndexOf("]")

        If openBracketStart <> -1 And closeBracketStart <> -1 Then
            Dim tempStr As String = strVal.Substring(openBracketStart + 1, closeBracketStart - openBracketStart - 1)
            For Each chrVal As Char In tempStr.ToCharArray()
                If (Asc(chrVal) >= Asc("0")) And (Asc(chrVal) <= Asc("9")) Then
                    Return True
                End If
            Next
        End If

        Return False
    End Function
    Private Function HasAlphaVal(strVal As String) As Boolean
        Dim openBracketStart As Integer = strVal.IndexOf("[")
        Dim closeBracketStart As Integer = strVal.IndexOf("]")

        If openBracketStart <> -1 And closeBracketStart <> -1 Then
            Dim tempStr As String = strVal.Substring(openBracketStart + 1, closeBracketStart - openBracketStart - 1)
            For Each chrVal As Char In tempStr.ToCharArray()
                If ((Asc(chrVal) >= Asc("a")) And (Asc(chrVal) <= Asc("z"))) Or ((Asc(chrVal) >= Asc("A")) And (Asc(chrVal) <= Asc("Z"))) Then
                    Return True
                End If
            Next
        End If
        Return False
    End Function
    Private Function HasAlphaNumericVal(strVal As String) As Boolean
        Return HasNumbers(strVal) And HasAlphaVal(strVal)
    End Function

End Class

#End Region

#Region "Regex Validator Class"

Public Class RegExValidator

    Private _isValid As Boolean
    Private _regExFormat As String
    Private _regExVal As String
    Private _reformatFlag As Boolean
    Private _reformattedString As String


    Public Sub New(regExFormat As String, regExVal As String, Optional ByVal reformatFlag As Boolean = False)
        _regExFormat = regExFormat
        _regExVal = regExVal
        _reformatFlag = reformatFlag

        ValidateRegEx()
    End Sub

    Public ReadOnly Property RegularExpFormat() As String
        Get
            Return _regExFormat
        End Get
    End Property

    Public ReadOnly Property RegularExpVal() As String
        Get
            Return _regExVal
        End Get
    End Property
    Public ReadOnly Property ReformattedString() As String
        Get
            Return _reformattedString
        End Get
    End Property
    Public ReadOnly Property IsValid() As Boolean
        Get
            Return _isValid
        End Get
    End Property
    Public ReadOnly Property ReformatFlag() As String
        Get
            Return _reformatFlag
        End Get
    End Property

    Private Sub ValidateRegEx()
        Dim matchRegEx As Regex = New Regex(RegularExpFormat.Trim())
        Dim M As Match = matchRegEx.Match(RegularExpVal.Trim())

        'if successful, then we need to populate the result structure
        'else try to reformat if the reformat flag is set to true in the 
        'parameter and also in the database.

        If M.Success Then
            _isValid = True
            _regExVal = RegularExpVal
            _reformattedString = RegularExpVal
        ElseIf ReformatFlag Then

            Dim regexPattern As String = ""
            Dim regexSegment As String = ""
            Dim segmentIdx As Integer = 0
            If RegularExpFormat.Trim().Length > 0 Then
                ' to remove the ^ and $ signs 
                Dim pNewFormat As String = RegularExpFormat.Substring(1, RegularExpFormat.Trim().Length - 2)
                For Each tempStr As String In pNewFormat.Trim().Split("}".Chars(0))
                    If tempStr.Trim().Length > 0 Then
                        tempStr = tempStr + "}"
                        Dim tempRegEx As GenericRegExFactory = New GenericRegExFactory(tempStr)
                        Select Case tempRegEx.RegularExp.RegularExType
                            Case RegExTypeValues.SpaceRegEx
                                regexSegment = regexSegment & " "
                            Case RegExTypeValues.SpecialCharRegEx
                                regexSegment = regexSegment & New String(CType(tempRegEx.RegularExp, SpecialCharRegEx).SpecialChar.Chars(0), tempRegEx.RegularExp.MaximumLength)
                            Case RegExTypeValues.NumericRegEx
                                segmentIdx = segmentIdx + 1
                                regexSegment = regexSegment & "$" & segmentIdx.ToString()
                                regexPattern = regexPattern & "(" & tempRegEx.RegularExp.RegExString() & ")"
                            Case RegExTypeValues.AlphaNumericRegEx
                                segmentIdx = segmentIdx + 1
                                regexSegment = regexSegment & "$" & segmentIdx.ToString()
                                regexPattern = regexPattern & "(" & tempRegEx.RegularExp.RegExString() & ")"
                            Case RegExTypeValues.AlphaRegEx
                                segmentIdx = segmentIdx + 1
                                regexSegment = regexSegment & "$" & segmentIdx.ToString()
                                regexPattern = regexPattern & "(" & tempRegEx.RegularExp.RegExString() & ")"
                        End Select
                    End If
                Next

                regexPattern = "^" & regexPattern & "$"
                matchRegEx = New Regex(regexPattern)
                M = matchRegEx.Match(RegularExpVal.Trim())

                If M.Success Then
                    _isValid = True
                    _regExVal = RegularExpVal
                    _reformattedString = matchRegEx.Replace(RegularExpVal, regexSegment)
                End If

            End If
        End If

    End Sub

End Class

#End Region
