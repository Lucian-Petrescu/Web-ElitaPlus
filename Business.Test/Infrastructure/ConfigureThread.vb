Imports Assurant.ElitaPlus.Security
Imports System.Security.Principal
Imports Moq
Imports System.IdentityModel.Claims

Public Module ConfigureThread

    Public Sub Configure()
        Dim principle As New Mock(Of IPrincipal)
        Dim identity As New Mock(Of IElitaClaimsIdentity)

        identity.Setup(Function(ei) ei.Claims).Returns(New List(Of Claim)(
                                                    {
                                                        New Claim(Assurant.ElitaPlus.Security.ClaimTypes.LanguageCode, "US", Nothing)
                                                    }))

        principle.Setup(Function(p) p.Identity).Returns(identity.Object)
        Threading.Thread.CurrentPrincipal = principle.Object
    End Sub

End Module
