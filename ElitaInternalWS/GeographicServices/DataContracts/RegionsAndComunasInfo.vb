Imports System.Collections.Generic
Imports System.Runtime.Serialization
Imports Assurant.ElitaPlus.Business
Imports Assurant.ElitaPlus.DataEntities

Namespace GeographicServices
    <DataContract(Namespace:="http://elita.assurant.com/GeographicServices/RegionsAndComunas", Name:="RegionsAndComunasInfo")>
    Public Class RegionsAndComunasInfo

        <DataMember(IsRequired:=True, Name:="RegionCode")>
        Public Property RegionCode As String

        <DataMember(IsRequired:=True, Name:="RegionDescription")>
        Public Property RegionDescription As String

        <DataMember(IsRequired:=False, Name:="Comunas")>
        Public Property Comunas As IEnumerable(Of ComunasInfo)

        Public Sub New()

        End Sub

        Friend Sub New(RegionsAndComunasDV As DataView)

            If RegionsAndComunasDV IsNot Nothing AndAlso RegionsAndComunasDV.Count > 0 Then
                RegionCode = RegionsAndComunasDV.Item(0).Item("region_code")
                RegionDescription = RegionsAndComunasDV.Item(0).Item("region_description")

                Dim Comunas As New List(Of ComunasInfo)
                For Each rowView As DataRowView In RegionsAndComunasDV
                    Dim row As DataRow = rowView.Row
                    If Not row.Item("comuna") Is DBNull.Value Then
                        Dim oComunasInfo As ComunasInfo = New ComunasInfo(row.Item("comuna"))
                        Comunas.Add(oComunasInfo)
                    End If


                Next

                Me.Comunas = Comunas

            End If

        End Sub


    End Class
End Namespace
