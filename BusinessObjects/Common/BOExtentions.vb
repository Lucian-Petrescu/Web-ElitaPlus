Imports System.Runtime.CompilerServices
Imports System.Text

Module BOExtentions
    <Extension> _
    Public Function FindValidationErrors(Of TBusinessObject As IBusinessObjectBase)(ByVal objectToValidate As TBusinessObject) As ValidationError()
        Dim validator As New Validator
        validator.IsValid(objectToValidate)
        Return validator.Messages
    End Function


    <Extension> _
    Public Function Validate(Of TBusinessObject As IBusinessObjectBase)(ByVal objectToValidate As TBusinessObject) As ValidationError()
        Dim errors() As ValidationError = objectToValidate.FindValidationErrors()

        If Not errors Is Nothing AndAlso errors.Length > 0 Then
            Throw New BOValidationException(errors, objectToValidate.GetType.FullName, objectToValidate.UniqueId)
        End If
    End Function

    <Extension> _
    Public Function ToDate(ByVal inputString As String, ByVal defaultValue As Nullable(Of DateTime)) As Nullable(Of DateTime)
        Dim returnValue As DateTime
        If (DateTime.TryParse(inputString, returnValue)) Then
            Return returnValue
        Else
            Return defaultValue
        End If
    End Function

    <Extension> _
    Public Function ToDecimal(ByVal inputString As String, ByVal defaultValue As Nullable(Of Decimal)) As Nullable(Of Decimal)
        Dim returnValue As Decimal
        If (Decimal.TryParse(inputString, returnValue)) Then
            Return returnValue
        Else
            Return defaultValue
        End If
    End Function

    <Extension> _
    Public Function ToGuid(ByVal inputString As String, ByVal defaultValue As Nullable(Of Guid)) As Nullable(Of Guid)
        If (inputString Is Nothing OrElse inputString.Trim.Length = 0) Then Return defaultValue
        Try
            Return GuidControl.ByteArrayToGuid(GuidControl.HexToByteArray(inputString))
        Catch ex As Exception
            Return defaultValue
        End Try
    End Function

    <Extension> _
    Public Function ToString(ByVal inputGuid As Guid, Optional ByVal defaultValue As String = Nothing) As String
        If (inputGuid.Equals(Guid.Empty)) Then Return defaultValue
        Return GuidControl.GuidToHexString(inputGuid)
    End Function

    <Extension> _
    Public Function ToRejectReason(ByVal ex As BOValidationException) As String
        Dim sb As New StringBuilder
        If (Not ex Is Nothing) Then
            For Each oValidationError As ValidationError In ex.ValidationErrorList()
                If sb.Length > 0 Then sb.Append(", ")
                sb.AppendFormat("{0} : {1}", oValidationError.PropertyName, TranslationBase.TranslateLabelOrMessage(oValidationError.Message))
            Next
        End If
        Return sb.ToString()
    End Function

End Module
