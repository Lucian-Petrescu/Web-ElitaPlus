Imports Assurant.ElitaPlus.DataAccessInterface
Imports Assurant.ElitaPlus.DataEntities
Public NotInheritable Class CurrencyRepository(Of TType As {BaseEntity, IRecordCreateModifyInfo, ICurrencyEntity})
    Inherits Repository(Of TType, currencyContext)
    Implements ICurrencyRepository(Of TType)

    Public Sub New()
        MyBase.New(New Lazy(Of currencyContext)())
    End Sub
End Class