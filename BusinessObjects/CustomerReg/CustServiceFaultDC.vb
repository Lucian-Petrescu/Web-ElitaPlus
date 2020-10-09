Imports System.Collections.Generic
Imports System.Linq
Imports System.Text.RegularExpressions
Imports System.ServiceModel
Imports System.Runtime.Serialization

<DataContract(Name:="CustServiceFault")> _
Public Class CustServiceFaultDC

#Region "Private Members"

    Private __faultDetail As String

#End Region

#Region "DataMembers"

    <DataMember(Name:="FaultDetail")> _
    Public Property FaultDetail As String
        Get
            Return __faultDetail
        End Get
        Set
            __faultDetail = value
        End Set
    End Property

#End Region

End Class
