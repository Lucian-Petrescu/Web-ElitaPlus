Imports System.IO
Imports System.Xml.Serialization

Public Module SerializationExtensions

    Public Function Serialize(Of TType)(xmlContent As String) As TType
        Dim serializer As New XmlSerializer(GetType(TType))
        Dim result As TType

        Using reader As TextReader = New StringReader(xmlContent)
            result = DirectCast(serializer.Deserialize(reader), TType)
        End Using

        Return result

    End Function

End Module
