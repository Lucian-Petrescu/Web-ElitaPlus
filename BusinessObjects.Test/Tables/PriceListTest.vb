Imports System.Text
Imports Assurant.Common.Types
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()> Public Class PriceListTest

    Private pldIdString = "59CE94747ED922A6E0530D28480A8F9A"
    Private pldID As Guid = New Guid(DotNetToOracle(pldIdString))

    <TestInitialize()> Public Sub MyTestInitialize()
        TestUtility.Login()
    End Sub
    <TestMethod()> Public Sub LoadList_RecordFound()
        Dim scIdString As String = DotNetToOracle("F08A47B65648BE43BF770C25DECE56EF") '"bd74ec57-6074-9bb5-e053-0c28480a9ff0" '("59CE94747ED922A6E0530D28480A8F9A")
        Dim scId As Guid = New Guid(scIdString)
        Dim code As String = "YAMATO"
        Dim description As String = "YAMATO PRICE LIST"
        Dim serviceType As Guid = Guid.Empty
        Dim countryList As String = String.Empty
        Dim serviceCenter As String = String.Empty
        Dim activeOn As DateType = New DateTime(2018, 1, 1)

        Dim dv As PriceList.PriceListSearchDV = PriceList.GetList(code,
                                                                  description,
                                                                  serviceType,
                                                                  countryList,
                                                                  serviceCenter,
                                                                  activeOn)

        Assert.IsNotNull(dv, "DV")
        Assert.IsNotNull(dv.Table, "Table")
        Assert.IsTrue(dv.Table.Rows.Count > 0, "Rows")
        Assert.IsTrue(dv.Table.Rows(0)("code").ToString.Trim.Equals(code, StringComparison.InvariantCultureIgnoreCase), "Code")
    End Sub


    <TestMethod()> Public Sub LoadList_NoRecordFound()
        Dim scIdString As String = DotNetToOracle("F08A47B65648BE43BF770C25DECE56EF") '"bd74ec57-6074-9bb5-e053-0c28480a9ff0" '("59CE94747ED922A6E0530D28480A8F9A")
        Dim scId As Guid = New Guid(scIdString)
        Dim code As String = "YAMATO"
        Dim description As String = "YAMATO PRICE LIST- 1"
        Dim serviceType As Guid = Guid.Empty
        Dim countryList As String = String.Empty
        Dim serviceCenter As String = String.Empty
        Dim activeOn As DateType = New DateTime(2018, 1, 1)

        Dim dv As PriceList.PriceListSearchDV = PriceList.GetList(code,
                                                                  description,
                                                                  serviceType,
                                                                  countryList,
                                                                  serviceCenter,
                                                                  activeOn)

        Assert.IsNotNull(dv, "DV")
        Assert.IsNotNull(dv.Table, "Table")
        Assert.IsTrue(dv.Table.Rows.Count = 0, "Rows")
    End Sub

End Class