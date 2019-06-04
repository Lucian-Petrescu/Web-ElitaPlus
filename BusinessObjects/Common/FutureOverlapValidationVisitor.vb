
Public Class FutureOverlapValidationVisitor
    Implements IVisitor

    Public Function Visit(ByVal element As IElement) As Boolean Implements IVisitor.Visit
        If Not element.GetType.GetInterface("IExpirable", True) Is Nothing Then
            Dim iface As IExpirable = DirectCast(element, IExpirable)
            'validation starts here
            Dim olapDal As New OverlapValidationVisitorDAL
            Dim ds As DataSet = olapDal.LoadList(element.ID, element.GetType.Name, _
                                                  iface.Code, iface.Effective, iface.Expiration, iface.parent_id)
            If ds.Tables(0).Rows.Count > 0 Then
                For Each row As System.Data.DataRow In ds.Tables(0).Rows

                    Dim OldEffDate As DateTime = row("EFFECTIVE")
                    Dim OldExpDate As DateTime = row("EXPIRATION")

                    If OldEffDate > iface.Effective And (iface.Expiration > OldEffDate And iface.Expiration < OldExpDate) Then
                        Return True
                    End If

                    If OldEffDate > iface.Effective And (iface.Expiration > OldEffDate And iface.Expiration > OldExpDate) Then
                        Return True
                    End If
                Next
                Return False
            End If
        End If
    End Function

End Class
