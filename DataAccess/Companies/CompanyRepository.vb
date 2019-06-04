Imports Assurant.ElitaPlus.DataAccessInterface
Imports Assurant.ElitaPlus.DataEntities

Public NotInheritable Class CompanyRepository(Of TType As {BaseEntity, IRecordCreateModifyInfo, ICompanyEntity})
    Inherits Repository(Of TType, CompanyContext)
    Implements ICompanyRepository(Of TType)

    Public Sub New()
        MyBase.New(New Lazy(Of CompanyContext)())
    End Sub
End Class
