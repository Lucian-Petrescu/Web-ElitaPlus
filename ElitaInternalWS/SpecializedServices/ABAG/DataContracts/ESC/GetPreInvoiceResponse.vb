Imports System.Collections.Generic
Imports System.Runtime.Serialization

Namespace SpecializedServices.Abag

    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Abag/GetPreInvoiceRequest", Name:="GetPreInvoiceResponse")>
    Public Class GetPreInvoiceResponse

        <DataMember>
        Public Property PreInvoices As IEnumerable(Of PreInvoiceInfo)
    End Class
End Namespace
