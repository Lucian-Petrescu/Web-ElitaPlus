Imports Assurant.ElitaPlus.DataEntities

Friend MustInherit Class BaseEquipment
    Friend ReadOnly ClaimEquipment As ClaimEquipment
    Friend ReadOnly CommonManager As CommonManager

    Public Sub New(pClaimEquipment As ClaimEquipment, pCommonManager As CommonManager)
        ClaimEquipment = pClaimEquipment
        CommonManager = pCommonManager
    End Sub

End Class




