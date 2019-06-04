Imports Assurant.ElitaPlus.Business
Imports Assurant.ElitaPlus.DataEntities
Public Class ClarMaxValueServiceHelper

    Public Function GetManufacturerName(pCompanyGroupManager As CompanyGroupManager,
                                        pCompanyGroupId As Guid,
                                        pManufacturerId As Guid) As String

        '''''''check whether the item make is Apple
        Dim companyGroup As CompanyGroup = pCompanyGroupManager.GetCompanyGroup(pCompanyGroupId)

        Dim oManufacturer As String = companyGroup.Manufacturers.Where(Function(m) m.ManufacturerId = pManufacturerId).FirstOrDefault.Description

        Return oManufacturer
        ''''''


    End Function
End Class
