Imports System.Collections.Generic
Imports System.Runtime.Serialization
Imports Assurant.ElitaPlus.Business
Imports Assurant.ElitaPlus.DataEntities

Namespace GeographicServices
    <DataContract(Namespace:="http://elita.assurant.com/GeographicServices/RegionsAndComunas", Name:="ComunasInfo")>
    Public Class ComunasInfo

        <DataMember(IsRequired:=True, Name:="ComunaCode")>
        Public Property ComunaCode As String

        Public Sub New(pComunaCode As String)
            Me.ComunaCode = pComunaCode

        End Sub
    End Class
End Namespace
