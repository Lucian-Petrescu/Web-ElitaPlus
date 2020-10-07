Imports Assurant.Common.Validation
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports System.Reflection
Imports System.ServiceModel
Imports System.ComponentModel.DataAnnotations
Imports System.Collections.Generic

Friend Module ExtensionMethods

    <System.Runtime.CompilerServices.Extension()> _
    Public Function IsNullOrWhiteSpace(value As String) As Boolean
        If (value Is Nothing) Then Return True

        Return String.IsNullOrEmpty(value.Trim())

    End Function

    <System.Runtime.CompilerServices.Extension()> _
    Public Function ToValidationFault(value As BOValidationException) As ValidationFault
        Dim returnValue As New ValidationFault

        For Each va As ValidationError In value.ValidationErrorList()
            returnValue.ValidationErrors.Add(va.PropertyName, va.Message)
        Next

        Return returnValue
    End Function

    Public Sub Validate(objectToValidate As Object)
        Dim oValidationErrors As New Dictionary(Of String, String)
        Validate(objectToValidate, oValidationErrors)
        If (oValidationErrors.Count > 0) Then
            Throw New FaultException(Of ValidationFault)(New ValidationFault(oValidationErrors), "Validation Fault")
        End If
    End Sub

    Private Sub Validate(objectToValidate As Object, ByRef pValidationErrors As Dictionary(Of String, String))
        If (objectToValidate Is Nothing) Then Exit Sub
        For Each pi As PropertyInfo In objectToValidate.GetType().GetProperties()
            If (pi.PropertyType().Equals(GetType(String))) Then
                For Each a As Object In pi.GetCustomAttributes(False)
                    If (a.GetType().Equals(GetType(StringLengthAttribute))) Then
                        If (Not DirectCast(a, StringLengthAttribute).IsValid(pi.GetValue(objectToValidate, Nothing))) Then
                            pValidationErrors.Add(pi.Name, DirectCast(a, StringLengthAttribute).ErrorMessage)
                        End If
                    End If

                    If (a.GetType().Equals(GetType(RequiredAttribute))) Then
                        If (Not DirectCast(a, RequiredAttribute).IsValid(pi.GetValue(objectToValidate, Nothing))) Then
                            pValidationErrors.Add(pi.Name, DirectCast(a, RequiredAttribute).ErrorMessage)
                        End If
                    End If
                Next
            ElseIf (pi.PropertyType().Equals(GetType(Byte()))) Then
                For Each a As Object In pi.GetCustomAttributes(False)
                    If (GetType(ValidationAttribute).IsAssignableFrom(a.GetType())) Then
                        If (Not DirectCast(a, ValidationAttribute).IsValid(pi.GetValue(objectToValidate, Nothing))) Then
                            pValidationErrors.Add(pi.Name, DirectCast(a, ValidationAttribute).ErrorMessage)
                        End If
                    End If
                Next
            ElseIf (pi.PropertyType().Equals(GetType(Nullable(Of DateTime)))) Then
                For Each a As Object In pi.GetCustomAttributes(False)
                    If (GetType(ValidationAttribute).IsAssignableFrom(a.GetType())) Then
                        If (Not DirectCast(a, ValidationAttribute).IsValid(pi.GetValue(objectToValidate, Nothing))) Then
                            pValidationErrors.Add(pi.Name, DirectCast(a, ValidationAttribute).ErrorMessage)
                        End If
                    End If
                Next
            ElseIf (GetType(IEnumerable)).IsAssignableFrom(pi.PropertyType) Then
                For Each o As Object In pi.GetValue(objectToValidate, Nothing)
                    Validate(o, pValidationErrors)
                Next
            Else
                Validate(pi.GetValue(objectToValidate, Nothing), pValidationErrors)
            End If
        Next
    End Sub

End Module

