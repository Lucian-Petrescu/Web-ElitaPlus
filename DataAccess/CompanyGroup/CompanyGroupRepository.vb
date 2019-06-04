Imports Assurant.ElitaPlus.DataAccessInterface
Imports Assurant.ElitaPlus.DataEntities

Public NotInheritable Class CompanyGroupRepository(Of TType As {BaseEntity, IRecordCreateModifyInfo, ICompanyGroupEntity})
    Inherits Repository(Of TType, CompanyGroupContext)
    Implements ICompanyGroupRepository(Of TType)

    Public Sub New()
        MyBase.New(New Lazy(Of CompanyGroupContext)())
    End Sub
End Class
