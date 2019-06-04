
Imports System.Runtime.Serialization

Namespace SpecializedServices.Abag

    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/Abag/PreInvoiceInfo", Name:="PreInvoiceInfo")>
    Public Class PreInvoiceInfo


#Region "DataMember"

        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="BatchNumber", Order:=1)>
        Private _BatchNumber As String
        Public Property BatchNumber() As String
            Get
                Return _BatchNumber
            End Get
            Set(ByVal value As String)
                _BatchNumber = value
            End Set
        End Property

        <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="PreInvoiceDate", Order:=2)>
        Private _PreInvoiceDate As Date
        Public Property PreInvoiceDate() As Date
            Get
                Return _PreInvoiceDate
            End Get
            Set(ByVal value As Date)
                _PreInvoiceDate = value
            End Set
        End Property

#End Region


    End Class
End Namespace

