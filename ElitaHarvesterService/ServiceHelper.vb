Imports Assurant.ElitaPlus.BusinessObjectsNew
Public NotInheritable Class ServiceHelper

    Public Shared Function CreateActivateProductClient() As Antivirus.ActivateProduct.ActivateProductServiceClient
        Dim activateProductClient As Antivirus.ActivateProduct.ActivateProductServiceClient
        Dim oWebPasswd As WebPasswd
        oWebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, "TO BE DONE"), True)
        activateProductClient = New Antivirus.ActivateProduct.ActivateProductServiceClient("", oWebPasswd.Url)
        activateProductClient.ClientCredentials.UserName.UserName = oWebPasswd.UserId
        activateProductClient.ClientCredentials.UserName.Password = oWebPasswd.Password
        Return activateProductClient
    End Function

    Public Shared Function CreateCancelProductClient() As Antivirus.CancelProduct.CancelProductClient
        Dim cancelProductClient As Antivirus.CancelProduct.CancelProductClient
        Dim oWebPasswd As WebPasswd
        oWebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, "TO BE DONE"), True)
        cancelProductClient = New Antivirus.CancelProduct.CancelProductClient("", oWebPasswd.Url)
        cancelProductClient.ClientCredentials.UserName.UserName = oWebPasswd.UserId
        cancelProductClient.ClientCredentials.UserName.Password = oWebPasswd.Password
        Return cancelProductClient
    End Function

    Public Shared Function CreateUpdateProductClient() As Antivirus.UpdateProduct.UpdateProductClient
        Dim updateProductClient As Antivirus.UpdateProduct.UpdateProductClient
        Dim oWebPasswd As WebPasswd
        oWebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, "TO BE DONE"), True)
        updateProductClient = New Antivirus.UpdateProduct.UpdateProductClient("", oWebPasswd.Url)
        updateProductClient.ClientCredentials.UserName.UserName = oWebPasswd.UserId
        updateProductClient.ClientCredentials.UserName.Password = oWebPasswd.Password
        Return updateProductClient
    End Function

End Class
