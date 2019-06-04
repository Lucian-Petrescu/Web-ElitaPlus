Imports Assurant.ElitaPlus.DataAccessInterface
Imports Assurant.ElitaPlus.DataEntities

Public Interface ISpecializedCertificateManager
    Property CertificateItemCoverageRepository() As ICertificateRepository(Of CertificateItemCoverage)

    Property CertificateRepository() As ICertificateRepository(Of Certificate)

End Interface
