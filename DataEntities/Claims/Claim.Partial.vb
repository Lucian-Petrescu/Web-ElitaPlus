Imports Assurant.ElitaPlus.DataEntities

Partial Public Class Claim
    Implements ISupportsIssues
    Implements ISupportsEquipment
    Implements ICloneable


    Public Sub AddIssue(pIssue As Issue, ByVal pRule As Rule) Implements ISupportsIssues.AddIssue
        Me.EntityIssues.Add(New EntityIssue() With
                            {
                                .EntityIssueId = Guid.NewGuid(),
                                .IssueId = pIssue.IssueId,
                                .Entity = "Claim",
                                .EntityId = ClaimId
                            })
    End Sub

    Public Sub AddEquipment(pCertificateItem As CertificateItem, ClaimedEquipmentTypeId As Guid) Implements ISupportsEquipment.AddEquipment
        Me.ClaimEquipments.Add(New ClaimEquipment() With {
                        .ClaimEquipmentDate = pCertificateItem.CreatedDate,
                        .ManufacturerId = pCertificateItem.ManufacturerId,
                        .Model = pCertificateItem.Model,
                        .Sku = pCertificateItem.SkuNumber,
                        .ImeiNumber = pCertificateItem.ImeiNumber,
                        .EquipmentId = If(Not pCertificateItem.EquipmentId.Equals(Guid.Empty), pCertificateItem.EquipmentId, Guid.Empty),
                        .ClaimEquipmentTypeId = ClaimedEquipmentTypeId
                               })
    End Sub

    Public Function Clone() As Object Implements ICloneable.Clone
        Dim returnClaim As Claim = DirectCast(Me.ShalowClone(), Claim)

        With returnClaim
            .ClaimEquipments =
                Me.ClaimEquipments.Select(Function(ce)
                                              Dim cce As ClaimEquipment = ce.ShalowClone()
                                              cce.Claim = returnClaim
                                              cce.ClaimId = returnClaim.ClaimId
                                              Return cce
                                          End Function).ToList()

            .ClaimStatuses = New List(Of ClaimStatus)

            .Comments =
                Me.Comments.Select(Function(com)
                                       Dim ccom As Comment = com.ShalowClone()
                                       ccom.Claim = returnClaim
                                       ccom.ClaimId = returnClaim.ClaimId
                                       Return ccom
                                   End Function).ToList()

            .EntityIssues = Me.EntityIssues.Select(Function(ei)
                                                       Dim cei As EntityIssue = ei.ShalowClone()
                                                       cei.Claim = returnClaim
                                                       cei.EntityId = returnClaim.ClaimId
                                                       Return cei
                                                   End Function).ToList()


        End With

        Return returnClaim
    End Function
End Class
