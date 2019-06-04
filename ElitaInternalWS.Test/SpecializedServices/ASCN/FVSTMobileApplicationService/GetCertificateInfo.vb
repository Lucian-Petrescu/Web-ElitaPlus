Imports System.ServiceModel
Imports Moq
Imports System.Text
Imports Assurant.ElitaPlus.Business
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Assurant.ElitaPlus.DataEntities

<TestClass()> Public Class GetCertificateInfo

    <TestMethod(), ExpectedException(GetType(FaultException(Of ValidationFault)))>
    Public Sub Negative1()

        Dim certMgr As Moq.Mock(Of ICertificateManager) = New Moq.Mock(Of ICertificateManager)()
        Dim commonManager As Moq.Mock(Of ICommonManager) = New Moq.Mock(Of ICommonManager)()

        Dim svc As FVSTMobileApplicationService = New FVSTMobileApplicationService(certMgr.Object, commonManager.Object)
        svc.GetCertificateInfo(Nothing)

    End Sub

    <TestMethod(), ExpectedException(GetType(FaultException(Of ElitaInternalWS.SpecializedServices.CertificateNotFoundFault)))>
    Public Sub Negative2()

        Dim svc As FVSTMobileApplicationService = GetInstance()

        Dim request As GetCertificateInfoRequest = New GetCertificateInfoRequest() With
            {
                .CertificateNumber = "C00002"
            }

        svc.GetCertificateInfo(request)

    End Sub

    <TestMethod()>
    Public Sub Positive()
        Dim svc As FVSTMobileApplicationService = GetInstance()
        ConfigureThread.Configure()

        Dim request As GetCertificateInfoRequest = New GetCertificateInfoRequest() With
        {
            .CertificateNumber = "C00001"
        }
        Dim response As GetCertificateInfoResponse = svc.GetCertificateInfo(request)
        Assert.AreEqual(2, response.Coverages.Count)
        Dim cdtls As CertificateDetails
        cdtls = response.Coverages.Where(Function(c) c.CoverageChinese = String.Format("{0}#{1}#{2}", ListCodes.CoverageType, LanguageCodes.Chinese, CoverageTypeCodes.MechanicalBreakdown)).FirstOrDefault()
        Assert.IsNotNull(cdtls)

        Assert.AreEqual(Of String)("WFC00001", cdtls.CellNumber)
        Assert.AreEqual(Of String)("C00001", cdtls.CertificateNumber)
        Assert.AreEqual(Of String)("CNC00001", cdtls.CustomerName)
        Assert.AreEqual(Of String)("D0001", cdtls.ItemDescription)
        Assert.AreEqual(Of String)(String.Format("{0}#{1}#{2}", ListCodes.CertificateStatus, LanguageCodes.Chinese, CertificateStatusCodes.Active), cdtls.StatusChinese)
        Assert.AreEqual(Of String)(String.Format("{0}#{1}#{2}", ListCodes.CertificateStatus, LanguageCodes.USEnglish, CertificateStatusCodes.Active), cdtls.StatusEnglish)
        Assert.AreEqual(Of String)(String.Format("{0}#{1}#{2}", ListCodes.CoverageType, LanguageCodes.Chinese, CoverageTypeCodes.MechanicalBreakdown), cdtls.CoverageChinese)
        Assert.AreEqual(Of String)(String.Format("{0}#{1}#{2}", ListCodes.CoverageType, LanguageCodes.USEnglish, CoverageTypeCodes.MechanicalBreakdown), cdtls.CoverageEnglish)
        Assert.AreEqual(Of String)(DateTime.Today.ToString("yyyy/MM/dd"), cdtls.WarrantyPurchaseDate)
        Assert.AreEqual(Of Int16)(1, cdtls.CoverageDuration)

    End Sub

    Private Function GetInstance() As FVSTMobileApplicationService
        Dim certMgr As Moq.Mock(Of ICertificateManager) = New Moq.Mock(Of ICertificateManager)()

        certMgr.Setup(Of Certificate)(
            Function(x) x.GetCertificate("ARPS", "C00002")).Returns(
            Function(ByVal dealerCode As String, ByVal certNumber As String) As Certificate
                Return Nothing
            End Function)

        certMgr.Setup(Of Certificate)(
            Function(x) x.GetCertificate("ARPS", It.IsNotIn(Of String)({"C00002"}))).Returns(
            Function(ByVal dealerCode As String, ByVal certNumber As String) As Certificate

                Dim returnValue As Certificate
                returnValue = New Certificate() With
                {
                    .CertificateNumber = certNumber,
                    .WorkPhone = "WF" & certNumber,
                    .StatusCode = CertificateStatusCodes.Active,
                    .CustomerName = "CN" & certNumber,
                    .WarrantySalesDate = DateTime.Today
                }

                returnValue.Items = New List(Of CertificateItem)(
                    {
                        New CertificateItem() With
                        {
                            .ItemNumber = 1,
                            .ItemDescription = "D0001"
                        },
                        New CertificateItem() With
                        {
                            .ItemNumber = 2,
                            .ItemDescription = "D002"
                        }
                    })

                With returnValue.Items.Where(Function(i) i.ItemNumber = 1).First()
                    .Coverages = New List(Of CertificateItemCoverage)()
                    Dim cvg = New CertificateItemCoverage With
                        {
                            .BeginDate = New Date(2015, 01, 01),
                            .EndDate = New Date(2015, 01, 31),
                            .Item = returnValue.Items.Where(Function(i) i.ItemNumber = 1).First(),
                            .Certificate = returnValue,
                            .CoverageTypeId = CommonManager.CoverageType_Accidental
                        }
                    .Coverages.Add(cvg)
                    returnValue.ItemCoverages.Add(cvg)

                    cvg = New CertificateItemCoverage With
                        {
                            .BeginDate = New Date(2015, 01, 01),
                            .EndDate = New Date(2015, 01, 31),
                            .Item = returnValue.Items.Where(Function(i) i.ItemNumber = 1).First(),
                            .Certificate = returnValue,
                            .CoverageTypeId = CommonManager.CoverageType_MechanicalBreakdown
                        }
                    .Coverages.Add(cvg)
                    returnValue.ItemCoverages.Add(cvg)
                End With

                With returnValue.Items.Where(Function(i) i.ItemNumber = 2).First()
                    .Coverages = New List(Of CertificateItemCoverage)()
                    Dim cvg = New CertificateItemCoverage With
                        {
                            .BeginDate = New Date(2015, 01, 01),
                            .EndDate = New Date(2015, 01, 31),
                            .Item = returnValue.Items.Where(Function(i) i.ItemNumber = 2).First(),
                            .Certificate = returnValue,
                            .CoverageTypeId = CommonManager.CoverageType_Accidental
                        }
                    .Coverages.Add(cvg)
                    returnValue.ItemCoverages.Add(cvg)

                    cvg = New CertificateItemCoverage With
                        {
                            .BeginDate = New Date(2015, 01, 01),
                            .EndDate = New Date(2015, 01, 31),
                            .Item = returnValue.Items.Where(Function(i) i.ItemNumber = 2).First(),
                            .Certificate = returnValue,
                            .CoverageTypeId = CommonManager.CoverageType_MechanicalBreakdown
                        }
                    .Coverages.Add(cvg)
                    returnValue.ItemCoverages.Add(cvg)
                End With

                Return returnValue

            End Function)

        Dim svc As FVSTMobileApplicationService = New FVSTMobileApplicationService(certMgr.Object, CommonManager.Current)
        Return svc

    End Function

End Class