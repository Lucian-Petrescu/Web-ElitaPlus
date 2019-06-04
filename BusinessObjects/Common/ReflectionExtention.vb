Imports System.Reflection
Imports System.Runtime.CompilerServices

'Module for reflection type functions
Public Module ReflectionExtention

    ''' <summary>
    ''' Extension for 'Object' that copies the properties to a destination object.
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="destination"></param>
    ''' <remarks></remarks>
    <System.Runtime.CompilerServices.Extension()> _
    Public Sub CopyProperties(ByVal source As Object, ByVal destination As Object)
        ' If any this null throw an exception 
        If source Is Nothing OrElse destination Is Nothing Then
            Throw New Exception("Source or/and Destination Objects are null")
        End If
        ' Getting the Types of the objects 
        Dim typeDest As Type = destination.[GetType]()
        Dim typeSrc As Type = source.[GetType]()
        ' Iterate the Properties of the source instance and    
        ' populate them from their desination counterparts   
        Dim srcProps As PropertyInfo() = typeSrc.GetProperties()
        For Each srcProp As PropertyInfo In srcProps

            If Not srcProp.CanRead Then

                Continue For
            End If
            Dim targetProperty As PropertyInfo = typeDest.GetProperty(srcProp.Name)
            If targetProperty Is Nothing Then

                Continue For
            End If
            If Not targetProperty.CanWrite Then

                Continue For
            End If
            If (targetProperty.GetSetMethod().Attributes And MethodAttributes.[Static]) <> 0 Then

                Continue For
            End If
            If Not targetProperty.PropertyType.IsAssignableFrom(srcProp.PropertyType) Then

                Continue For
            End If
            ' Passed all tests, lets set the value  
            targetProperty.SetValue(destination, srcProp.GetValue(source, Nothing), Nothing)
        Next

    End Sub


End Module
