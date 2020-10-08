
Public Class OverlapValidationVisitor
    Implements IVisitor

    Public Function Visit(element As IElement) As Boolean Implements IVisitor.Visit
        If Not element.GetType.GetInterface("IExpirable", True) Is Nothing Then
            Dim iface As IExpirable = DirectCast(element, IExpirable)
            'validation starts here
            Dim olapDal As New OverlapValidationVisitorDAL
            Dim ds As DataSet = olapDal.LoadList(element.ID, element.GetType.Name, _
                                                  iface.Code, iface.Effective, iface.Expiration, iface.parent_id)
            If ds.Tables(0).Rows.Count > 0 Then
                Return True
            End If
        End If
    End Function

End Class
