Imports Assurant.ElitaPlus.DataAccessInterface
Imports Assurant.ElitaPlus.DataEntities
Public NotInheritable Class AddressRepository(Of TType As {BaseEntity, IRecordCreateModifyInfo, ICurrencyEntity})
    Inherits Repository(Of TType, AddressContext)
    Implements IAddressRepository(Of TType)

    Public Sub New()
        MyBase.New(New Lazy(Of AddressContext)())
    End Sub
End Class