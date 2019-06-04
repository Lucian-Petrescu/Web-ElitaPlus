Imports System.Text
Imports Assurant.ElitaPlus.DALObjects
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()>
Public Class VendorQuantityTest

    <TestInitialize()> Public Sub MyTestInitialize()
        TestUtility.Login()
    End Sub

    <TestMethod()> Public Sub LoadList_RecordFound()
        Dim scIdString = "DC4E1C8FBA63744BBD8725CFD5D219BC"
        Dim scID As Guid = New Guid(TestUtility.DotNetToOracle(scIdString))


        Dim ds As DataSet = New DataSet

        Dim vqBO As VendorQuantityDAL = New VendorQuantityDAL()

        vqBO.LoadList(ds, scID)

        Assert.IsNotNull(ds)
        Assert.IsNotNull(ds.Tables.Count > 0)
        Assert.IsTrue(ds.Tables(0).Rows.Count > 0)
        Assert.IsTrue(CType(ds.Tables(0).Rows(0)("service_center_id"), Byte()).SequenceEqual(scID.ToByteArray))
    End Sub



End Class
