Public Interface IElement
    ReadOnly Property ID As Guid
    ReadOnly Property IsNew As Boolean
    Function Accept(ByRef visitor As IVisitor) As Boolean
End Interface