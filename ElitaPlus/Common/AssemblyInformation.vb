Imports System.Reflection
Imports Microsoft.VisualBasic

Namespace Generic

    Public Class AssemblyInformation

#Region "Attributes"

        Private Shared ReadOnly moAllTypes As Hashtable = New Hashtable()

#End Region

        Shared Sub New()
            Dim oAssembly As [Assembly] = [Assembly].GetExecutingAssembly
            Dim oType As Type
            For Each oType In oAssembly.GetTypes
                If InStr(oType.FullName, "+") = 0 Then
                    If Not (moAllTypes.ContainsKey(oType.Name.ToUpper)) Then
                        moAllTypes.Add(oType.Name.ToUpper, oType)
                    End If
                End If
            Next
        End Sub

        Public Shared Function FromNameToType(name As String) As Type
            Dim oType As Type

            If Not moAllTypes.Contains(name.ToUpper) Then Return Nothing
            oType = CType(moAllTypes.Item(name.ToUpper), Type)
            Return oType

        End Function

        Public Shared Function GetControlsByFormName(formName As String) As System.Collections.Generic.List(Of String)
            Dim oControlNames As New System.Collections.Generic.List(Of String)
            Dim oControlType As Type, blnIsWebControl As Boolean

            oControlType = FromNameToType(formName)
            If oControlType Is Nothing Then Return Nothing
            oControlType = CType(moAllTypes.Item(formName), Type)
            Dim oControls As FieldInfo() = oControlType.GetFields(Reflection.BindingFlags.DeclaredOnly Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance)
            Dim oControlField As FieldInfo

            For Each oControlField In oControls
                If oControlField.Name.Chars(0) = "_" Then  ' Private Control Variable
                    'If oControlField.FieldType.IsSubclassOf(GetType(System.Web.UI.WebControls.WebControl)) OrElse oControlField.FieldType.IsSubclassOf(GetType(System.Web.UI.UserControl)) Then
                    '    oControlNames.Add(oControlField.Name)
                    'End If
                    oControlNames.Add(oControlField.Name)
                    GetChildrenControls(oControlField.Name, oControlField.FieldType, oControlNames)
                End If
            Next
            Return oControlNames
        End Function

        Private Shared Sub GetChildrenControls(userControlName As String, userControlType As Type, controlNames As System.Collections.Generic.List(Of String))
            Dim oControlType As Type = userControlType
            Dim oControls As FieldInfo() = oControlType.GetFields(Reflection.BindingFlags.DeclaredOnly Or Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance)
            Dim oControlField As FieldInfo

            For Each oControlField In oControls
                If oControlField.Name.Chars(0) = "_" Then  ' Private Control Variable
                    If oControlField.FieldType.IsSubclassOf(GetType(System.Web.UI.WebControls.WebControl)) OrElse oControlField.FieldType.IsSubclassOf(GetType(System.Web.UI.UserControl)) Then
                        Dim sName As String = userControlName & "." & oControlField.Name
                        controlNames.Add(sName)
                    End If
                End If

            Next
        End Sub
    End Class

End Namespace
