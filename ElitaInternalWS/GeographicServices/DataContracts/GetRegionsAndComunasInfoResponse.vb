Imports System.Collections.Generic
Imports System.Runtime.Serialization
Imports Assurant.ElitaPlus.Business
Imports Assurant.ElitaPlus.DataAccessInterface
Imports Assurant.ElitaPlus.DataEntities

Namespace GeographicServices
    <DataContract(Namespace:="http://elita.assurant.com/GeographicServices/RegionsAndComunas", Name:="GetRegionsAndComunasInfoResponse")>
    Public Class GetRegionsAndComunasInfoResponse

        <DataMember(IsRequired:=True, Name:="RegionsAndComunas")>
        Public Property RegionsAndComunas As IEnumerable(Of RegionsAndComunasInfo)

        Friend Sub New(RegionsAndComunasDV As DataView)

            If RegionsAndComunasDV IsNot Nothing AndAlso RegionsAndComunasDV.Count > 0 Then
                RegionsAndComunasDV.Sort = "region_code"
                Dim CurrentRegion As String = ""
                Dim CopyRegionsAndComunasDT As DataTable = RegionsAndComunasDV.Table.Copy()
                Dim CopyRegionsAndComunasDV As DataView = CopyRegionsAndComunasDT.DefaultView

                Dim RegionsAndComunas As New List(Of RegionsAndComunasInfo)
                For Each rowView As DataRowView In RegionsAndComunasDV
                    Dim row As DataRow = rowView.Row
                    If Not CurrentRegion.Equals(row.Item("region_code")) Then
                        CopyRegionsAndComunasDV.RowFilter = "region_code='" + row.Item("region_code") + "'"
                        Dim oRegionsAndComunasInfo As RegionsAndComunasInfo = New RegionsAndComunasInfo(CopyRegionsAndComunasDV)
                        RegionsAndComunas.Add(oRegionsAndComunasInfo)
                    End If
                    CurrentRegion = row.Item("region_code")
                Next

                Me.RegionsAndComunas = RegionsAndComunas
            End If


        End Sub

    End Class

End Namespace