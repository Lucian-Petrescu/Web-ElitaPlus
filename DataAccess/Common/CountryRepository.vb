Imports Assurant.ElitaPlus.DataAccessInterface
Imports Assurant.ElitaPlus.DataEntities
Public NotInheritable Class CountryRepository(Of TType As {BaseEntity, IRecordCreateModifyInfo, ICountryEntity})
    Inherits Repository(Of TType, CountryContext)
    Implements ICountryRepository(Of TType)

    Public Sub New()
        MyBase.New(New Lazy(Of CountryContext)())
    End Sub
End Class