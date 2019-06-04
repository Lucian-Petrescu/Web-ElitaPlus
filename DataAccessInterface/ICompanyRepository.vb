Imports Assurant.ElitaPlus.DataEntities

Public Interface ICompanyRepository(Of TEntity As {BaseEntity, ICompanyEntity})
    Inherits IRepository(Of TEntity)

End Interface