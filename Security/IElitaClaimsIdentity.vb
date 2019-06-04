Imports System.IdentityModel.Claims
Imports System.Security.Principal

Public Interface IElitaClaimsIdentity
    Inherits IIdentity
    ReadOnly Property Claims As IEnumerable(Of Claim)
End Interface
