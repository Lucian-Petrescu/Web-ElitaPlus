Imports System.Text
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports Assurant.ElitaPlus.DALObjects
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()> Public Class LogisticStageAddressTest
    Private testContextInstance As TestContext
    <ClassInitialize()>
    Public Shared Sub MyClassInitialize(ByVal testContext As TestContext)
        TestUtility.Login()
    End Sub
    <TestInitialize()> Public Sub MyTestInitialize()
        TestUtility.Login()
    End Sub
    Public Property TestContext() As TestContext
        Get
            Return testContextInstance
        End Get
        Set(ByVal value As TestContext)
            testContextInstance = value
        End Set
    End Property
    <TestMethod()> Public Sub LogisticStageData_Load_Test()

        Dim addressData As New Address With {
        .Address1 = "Address1",
        .Address2 = "Address2",
        .Address3 = "Address3",
        .City = "Miami",
        .ZipLocator = "0987",
        .PostalCode = "0987",
        .RegionId = New Guid,
        .CountryId = New Guid
        }
        Dim logisticStageAddressData As New LogisticStageAddress With {
            .LogisticStageName = "Test Logistic Addresses",
            .LogisticStageAddress = addressData
            }
        Assert.IsNull(logisticStageAddressData)
    End Sub
End Class