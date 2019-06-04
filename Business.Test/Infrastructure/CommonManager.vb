Imports Assurant.ElitaPlus.DataEntities
Imports Moq
Imports Assurant.ElitaPlus.Security
Public Module CommonManager

    Public ReadOnly Current As ICommonManager

    Public ReadOnly Data As Dictionary(Of String, IEnumerable(Of ElitaListItem))

    Public ReadOnly CertificateStatus_Active As Guid = Guid.NewGuid()
    Public ReadOnly CertificateStatus_Cancelled As Guid = Guid.NewGuid()
    Public ReadOnly CoverageType_MechanicalBreakdown As Guid = Guid.NewGuid()
    Public ReadOnly CoverageType_Accidental As Guid = Guid.NewGuid()

    Sub New()

        Data = BuildData()

        Dim cm As Mock(Of ICommonManager) = New Mock(Of ICommonManager)

        cm.Setup(Of IEnumerable(Of ElitaListItem))(
            Function(icm) icm.GetListItems(It.IsAny(Of String), It.IsAny(Of String))).Returns(
            Function(ByVal pListCode As String, ByVal pLanguageCode As String) As IEnumerable(Of ElitaListItem)
                Return Data(String.Format("{0}#{1}", pListCode, pLanguageCode))
            End Function)

        cm.Setup(Of IEnumerable(Of ElitaListItem))(
            Function(icm) icm.GetListItems(It.IsAny(Of String))).Returns(
            Function(ByVal pListCode As String) As IEnumerable(Of ElitaListItem)
                Return Data(String.Format("{0}#{1}", pListCode, Threading.Thread.CurrentPrincipal.GetLanguageCode()))
            End Function)

        Current = cm.Object

    End Sub

    Private Function BuildData() As Dictionary(Of String, IEnumerable(Of ElitaListItem))
        Dim returnValue As New Dictionary(Of String, IEnumerable(Of ElitaListItem))
        AddCertificateStatusList(returnValue, LanguageCodes.Chinese)
        AddCertificateStatusList(returnValue, LanguageCodes.USEnglish)
        AddCoverageTypeList(returnValue, LanguageCodes.Chinese)
        AddCoverageTypeList(returnValue, LanguageCodes.USEnglish)
        Return returnValue
    End Function

    Private Sub AddCertificateStatusList(returnValue As Dictionary(Of String, IEnumerable(Of ElitaListItem)), ByVal pLanguageCode As String)
        ' Build Certificate Status List (Chineese)
        returnValue.Add(String.Format("{0}#{1}", ListCodes.CertificateStatus, pLanguageCode),
                        New List(Of ElitaListItem)(
                        {
                            New ElitaListItem() With
                            {
                                .Code = CertificateStatusCodes.Active,
                                .Description = String.Format("{0}#{1}#{2}", ListCodes.CertificateStatus, pLanguageCode, CertificateStatusCodes.Active),
                                .ListItemId = CertificateStatus_Active
                            },
                            New ElitaListItem() With
                            {
                                .Code = CertificateStatusCodes.Cancelled,
                                .Description = String.Format("{0}#{1}#{2}", ListCodes.CertificateStatus, pLanguageCode, CertificateStatusCodes.Cancelled),
                                .ListItemId = CertificateStatus_Cancelled
                            }
                        }))
    End Sub

    Private Sub AddCoverageTypeList(returnValue As Dictionary(Of String, IEnumerable(Of ElitaListItem)), ByVal pLanguageCode As String)
        ' Build Certificate Status List (Chineese)
        returnValue.Add(String.Format("{0}#{1}", ListCodes.CoverageType, pLanguageCode),
                        New List(Of ElitaListItem)(
                        {
                            New ElitaListItem() With
                            {
                                .Code = CoverageTypeCodes.MechanicalBreakdown,
                                .Description = String.Format("{0}#{1}#{2}", ListCodes.CoverageType, pLanguageCode, CoverageTypeCodes.MechanicalBreakdown),
                                .ListItemId = CoverageType_MechanicalBreakdown
                            },
                            New ElitaListItem() With
                            {
                                .Code = CoverageTypeCodes.Accidental,
                                .Description = String.Format("{0}#{1}#{2}", ListCodes.CoverageType, pLanguageCode, CoverageTypeCodes.Accidental),
                                .ListItemId = CoverageType_Accidental
                            }
                        }))
    End Sub
End Module
