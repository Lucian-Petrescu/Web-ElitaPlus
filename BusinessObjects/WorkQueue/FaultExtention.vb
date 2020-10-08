Imports System.Runtime.CompilerServices
Imports System.ServiceModel
Imports System.Reflection

Public Module FaultExtention
    <Extension> _
    Public Function AsBOValidationException(ex As FaultException(Of WrkQueue.ValidationFault)) As BOValidationException
        Return AsBOValidationException(ex, Nothing)
    End Function
    <Extension> _
    Public Function AsBOValidationException(ex As FaultException(Of WrkQueue.ValidationFault), businessObject As Object) As BOValidationException
        Dim boEx As New BOValidationException
        Dim oValue As Object
        Dim oType As Type = Nothing
        Dim validationErrors(ex.Detail.Details.Length - 1) As ValidationError
        Dim propInfo As PropertyInfo
        If (businessObject IsNot Nothing) Then
            oType = businessObject.GetType()
        Else
            oType = GetType(BusinessObjectBase)
        End If
        For i As Integer = 0 To validationErrors.Length - 1
            With ex.Detail.Details(i)
                If (businessObject IsNot Nothing) Then
                    propInfo = businessObject.GetType().GetProperty(.Key)
                    If (propInfo IsNot Nothing) Then
                        oValue = propInfo.GetValue(businessObject, Nothing)
                    Else
                        oValue = Nothing
                    End If
                Else
                    oValue = Nothing
                End If
                validationErrors(i) = New ValidationError(.Message, oType, Nothing, .Key, oValue)
            End With
        Next
        Return New BOValidationException(validationErrors, oType.Name)
    End Function

    <Extension> _
    Public Function AsBOValidationException(ex As FaultException(Of DocAdmin.ValidationFault)) As BOValidationException
        Return AsBOValidationException(ex, Nothing)
    End Function
    <Extension> _
    Public Function AsBOValidationException(ex As FaultException(Of DocAdmin.ValidationFault), businessObject As Object) As BOValidationException
        Dim boEx As New BOValidationException
        Dim oValue As Object
        Dim oType As Type = Nothing
        Dim validationErrors(ex.Detail.Details.Length - 1) As ValidationError
        Dim propInfo As PropertyInfo
        If (businessObject IsNot Nothing) Then
            oType = businessObject.GetType()
        Else
            oType = GetType(BusinessObjectBase)
        End If
        For i As Integer = 0 To validationErrors.Length - 1
            With ex.Detail.Details(i)
                If (businessObject IsNot Nothing) Then
                    propInfo = businessObject.GetType().GetProperty(.Key)
                    If (propInfo IsNot Nothing) Then
                        oValue = propInfo.GetValue(businessObject, Nothing)
                    Else
                        oValue = Nothing
                    End If
                Else
                    oValue = Nothing
                End If
                validationErrors(i) = New ValidationError(.Message, oType, Nothing, .Key, oValue)
            End With
        Next
        Return New BOValidationException(validationErrors, oType.Name)
    End Function

    <Extension> _
    Public Function AsBOValidationException(ex As FaultException(Of Doc.ValidationFault)) As BOValidationException
        Return AsBOValidationException(ex, Nothing)
    End Function
    <Extension> _
    Public Function AsBOValidationException(ex As FaultException(Of Doc.ValidationFault), businessObject As Object) As BOValidationException
        Dim boEx As New BOValidationException
        Dim oValue As Object
        Dim oType As Type = Nothing
        Dim validationErrors(ex.Detail.Details.Length - 1) As ValidationError
        Dim propInfo As PropertyInfo
        If (businessObject IsNot Nothing) Then
            oType = businessObject.GetType()
        Else
            oType = GetType(BusinessObjectBase)
        End If
        For i As Integer = 0 To validationErrors.Length - 1
            With ex.Detail.Details(i)
                If (businessObject IsNot Nothing) Then
                    propInfo = businessObject.GetType().GetProperty(.Key)
                    If (propInfo IsNot Nothing) Then
                        oValue = propInfo.GetValue(businessObject, Nothing)
                    Else
                        oValue = Nothing
                    End If
                Else
                    oValue = Nothing
                End If
                validationErrors(i) = New ValidationError(.Message, oType, Nothing, .Key, oValue)
            End With
        Next
        Return New BOValidationException(validationErrors, oType.Name)
    End Function

    <Extension> _
    Public Function AsBOValidationException(ex As FaultException(Of Auth.ValidationFault)) As BOValidationException
        Return AsBOValidationException(ex, Nothing)
    End Function
    <Extension> _
    Public Function AsBOValidationException(ex As FaultException(Of Auth.ValidationFault), businessObject As Object) As BOValidationException
        Dim boEx As New BOValidationException
        Dim oValue As Object
        Dim oType As Type = Nothing
        Dim validationErrors(ex.Detail.Details.Length - 1) As ValidationError
        Dim propInfo As PropertyInfo
        If (businessObject IsNot Nothing) Then
            oType = businessObject.GetType()
        Else
            oType = GetType(BusinessObjectBase)
        End If
        For i As Integer = 0 To validationErrors.Length - 1
            With ex.Detail.Details(i)
                If (businessObject IsNot Nothing) Then
                    propInfo = businessObject.GetType().GetProperty(.Key)
                    If (propInfo IsNot Nothing) Then
                        oValue = propInfo.GetValue(businessObject, Nothing)
                    Else
                        oValue = Nothing
                    End If
                Else
                    oValue = Nothing
                End If
                validationErrors(i) = New ValidationError(.Message, oType, Nothing, .Key, oValue)
            End With
        Next
        Return New BOValidationException(validationErrors, oType.Name)
    End Function
End Module
