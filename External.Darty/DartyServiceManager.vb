Imports System.Configuration
Imports Assurant.ElitaPlus.External.Interfaces
Imports Assurant.ElitaPlus.External.Interfaces.Darty

Public Class DartyServiceManager
    Implements IDartyServiceManager

    Private Const DartyUrl As String = "DARTY_URL"
    Private Const DartyExpDays As String = "DARTY_EXP_DAYS"

    Public Function ActivateDartyGiftCard(ByVal request As GenerateGiftCardRequest) As GenerateGiftCardResponse Implements IDartyServiceManager.ActivateDartyGiftCard

        Dim url As String = ConfigurationManager.AppSettings(DartyUrl)
        Dim expirationDays As String = ConfigurationManager.AppSettings(DartyExpDays)


        Dim dartyServiceProxy As New GererCarteCadeauService(url)

        Dim response As New GenerateGiftCardResponse()

        Dim dartyRequest As New List(Of DemandeActiverCartePersoType)
        Dim result() As ReponseActiverCartePersoType = Nothing

        Using dartyServiceProxy
            dartyRequest.Add(New DemandeActiverCartePersoType With {
                               .applicationsource = request.ApplicationSource,
                               .numeroCommande = request.ReferenceNumber,
                               .codeTypeCarte = request.GiftCardType,
                               .montantCarte = Decimal.Parse(request.Amount),
                               .domiciliation = request.Domiciliation,
                               .libelleOperation = request.OperationFor,
                               .nom = request.FirstName,
                               .prenom = request.LastName,
                               .codePostal = request.ZipCode,
                               .telephone = request.PhoneNumber,
                               .email = request.Email
                                                            })



            dartyServiceProxy.activerCartePerso(dartyRequest.ToArray(), result)

            With result.FirstOrDefault()
                response.ClaimNumber = .numeroCommande
                response.GiftCardBarCodeNumber = .numeroCarteProsodie
                response.GiftCardSerialNumber = .numeroSerieProsodie
                response.CodePin1 = .codePin1
                response.CodePin2 = .codePin2
                response.GiftCardExpirationDate = DateTime.Today.AddDays(CDbl(expirationDays))
                response.ReturnCode = .codeRetour
                response.ErrorCode = .libelleErreur
                response.ErrorMessage = .detailErreur
                response.Amount = request.Amount

            End With

        End Using

        Return response

    End Function


End Class
