Imports System.Text
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports Assurant.ElitaPlus.DALObjects
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()>
Public Class ClaimAuthorizationTest

    Private pldIdString = "59CE94747ED922A6E0530D28480A8F9A"
    Private pldID As Guid = New Guid(DotNetToOracle(pldIdString))

    Private caIdString = "3CE237EFC4722647ABF44EFC5077E403"
    Private cadID As Guid = New Guid(DotNetToOracle(caIdString))

    <TestInitialize()> Public Sub MyTestInitialize()
        TestUtility.Login()
    End Sub

    <TestMethod()> Public Sub GetPriceListDetails_RecordFound()

        Dim ds As DataSet = New DataSet()

        Dim pldBO As PriceListDetail = New PriceListDetail(pldID, ds)

        Dim ca As ClaimAuthorization = New ClaimAuthorization(cadID)
        Dim caDAL As ClaimAuthorizationDAL = New ClaimAuthorizationDAL()

        Dim claimId As Guid = New Guid(DotNetToOracle("EECB6706A8174F47874D41942B4D1A16"))
        '9/24/2017
        Dim caDS As DataSet = caDAL.LoadPriceListDetails(ca.ServiceCenterId,
                                                         New DateTime(2017, 9, 24),
                                                         ElitaPlusIdentity.Current.ActiveUser.LanguageId,
                                                         pldBO.ServiceClassId,
                                                         pldBO.ServiceTypeId,
                                                         pldBO.RiskTypeId,
                                                         pldBO.EquipmentClassId,
                                                         pldBO.EquipmentId,
                                                         pldBO.ConditionId,
                                                         pldBO.VendorSku,
                                                         pldBO.VendorSkuDescription)

        Assert.IsNotNull(caDS)
        Assert.IsTrue(caDS.Tables(0).Rows.Count > 0)
        Assert.IsTrue(CType(caDS.Tables(0).Rows(0)("service_class_id"), Byte()).SequenceEqual(pldBO.ServiceClassId.ToByteArray))

    End Sub

    Private Function OracleToDotNet(text As String) As String

        Dim bytes() As Byte = ParseHex(text)
        Dim guid As Guid = New Guid(bytes)
        Return guid.ToString("N").ToUpperInvariant()

    End Function

    Private Function DotNetToOracle(text As String) As String

        Dim guid As Guid = New Guid(text)
        Return BitConverter.ToString(guid.ToByteArray()).Replace("-", String.Empty).Insert(8, "-").Insert(13, "-").Insert(18, "-").Insert(23, "-")
    End Function


    Private Function ParseHex(text As String) As Byte()

        Dim ret() As Byte = New Byte(text.Length / 2) {}
        For i As Integer = 0 To ret.Length - 1
            ret(i) = Convert.ToByte(text.Substring(i * 2, 2), 16)
        Next

        Return ret
    End Function

End Class
