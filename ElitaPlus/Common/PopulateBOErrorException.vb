Imports Assurant.ElitaPlus.Common
Imports System.Runtime.Serialization

<Serializable()> Public Class PopulateBOErrorException
    Inherits ElitaPlusException

    Public Sub New()
        MyBase.New("Error Populating the Business Data", ErrorCodes.POPULATE_BO_ERR)
    End Sub

    Protected Sub New(info As SerializationInfo, context As StreamingContext)
        MyBase.New(info, context)
    End Sub
End Class
