Imports System.Runtime.Serialization

<DataContract(Name:="PreInvoice")> _
Public Class PreInvoiceDC

#Region "DataMember"

    <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="Batch_Number", Order:=1)> _
    Private _BatchNumber As String
    Public Property Batch_Number() As String
        Get
            Return _BatchNumber
        End Get
        Set(ByVal value As String)
            _BatchNumber = value
        End Set
    End Property

    <DataMember(EmitDefaultValue:=True, IsRequired:=True, Name:="PreInvoice_Date", Order:=2)> _
    Private _PreInvoiceDate As String
    Public Property PreInvoice_Date() As String
        Get
            Return _PreInvoiceDate
        End Get
        Set(ByVal value As String)
            _PreInvoiceDate = value
        End Set
    End Property

#End Region
    
End Class
