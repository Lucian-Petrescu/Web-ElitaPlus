Imports System.Runtime.CompilerServices

Public Module XcdEnumHelpers
    <Extension()>
    Function GetXcdEnum(Of TEnum As Structure)(ByVal code As String) As TEnum
        Return GetType(TEnum).GetFields().[Select](Function(member) New With {
            Key .Attribute = CType(member.GetCustomAttributes(GetType(XcdAttribute), False).FirstOrDefault(), XcdAttribute),
            Key .Member = member
        }).Where(Function(pair) pair.Attribute IsNot Nothing AndAlso (pair.Attribute.Match(code) OrElse pair.Attribute.MatchXcd(code))).[Select](Function(x) CType([Enum].Parse(GetType(TEnum), x.Member.Name), TEnum)).FirstOrDefault()
    End Function

    <Extension()>
    Function XcdValue(Of TEnum As Structure)(ByVal item As TEnum?) As String
        If Not GetType(TEnum).IsEnum Then Throw New ArgumentException($"Generic type {GetType(TEnum).FullName} is not enumerated type")
        Return If(item?.[GetType]().GetFields().First(Function(fi) fi.Name = item.ToString()).GetCustomAttributes(GetType(XcdAttribute), False).[Select](Function(ca1) CType(ca1, XcdAttribute)).FirstOrDefault()?.XcdValue, New InvalidOperationException($"Value {item.ToString()} is not configured in type {GetType(TEnum).FullName}"))
    End Function
End Module
