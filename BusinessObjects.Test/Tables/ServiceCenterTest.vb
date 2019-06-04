Imports System.Text
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()>
Public Class ServiceCenterTest

    <TestInitialize()> Public Sub MyTestInitialize()
        TestUtility.Login()
    End Sub

    <TestMethod()> Public Sub GetQuantityView_RecordFound()
        Dim scIdString = "DC4E1C8FBA63744BBD8725CFD5D219BC"
        Dim scID As Guid = New Guid(DotNetToOracle(scIdString))

        Dim scBO As ServiceCenter = New ServiceCenter()

        Dim dv As DataView = scBO.GetQuantityView(scID)

        Assert.IsNotNull(dv)
        Assert.IsNotNull(dv.Table)
        Assert.IsTrue(dv.Table.Rows.Count > 0)
        Assert.IsTrue(CType(dv.Table.Rows(0)("reference_id"), Byte()).SequenceEqual(scID.ToByteArray))
    End Sub

End Class
