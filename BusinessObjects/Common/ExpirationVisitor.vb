Imports System.Reflection

Public Class ExpirationVisitor
    Implements IVisitor
    Dim DateofExpiration As DateTimeType
    ''' <summary>
    ''' Default way the expiration date will be current date and time
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ExpirationDate As DateTimeType)
        DateofExpiration = ExpirationDate.Value.AddSeconds(-1)
    End Sub
    Public Sub New()
    End Sub
    Public Function Visit(element As IElement) As Boolean Implements IVisitor.Visit
        If Not element.GetType.GetInterface("IExpirable", True) Is Nothing Then
            Dim iface As IExpirable = DirectCast(element, IExpirable)
            If DateofExpiration Is Nothing Then
                If iface.Effective.Value > DateTime.Now Then
                    iface.Expiration = iface.Effective.Value.AddSeconds(1)
                Else
                    iface.Expiration = DateTime.Now
                End If
            Else
                iface.Expiration = DateofExpiration
            End If
            Dim Parent As Type = element.GetType
            For Each prop As PropertyInfo In Parent.GetProperties
                If prop.PropertyType.BaseType.Name = "BusinessObjectListBase" And (TypeOf prop Is IExpirable) Then
                    For Each childIFace As IExpirable In prop.GetValue(Convert.ChangeType(element, Parent), Nothing)
                        childIFace.Expiration = iface.Expiration
                    Next
                End If
            Next
        End If
    End Function

End Class
