Imports Assurant.ElitaPlus.Business


<TestClass()> Public Class GetFVSTMonths

    <TestMethod()>
    Public Sub PositiveTesting1()

        Dim beginDate As Date = New Date(2015, 01, 01)
        Dim endDate As Date = New Date(2015, 01, 14)

        Assert.AreEqual(0, beginDate.GetFVSTMonths(endDate))

    End Sub

    <TestMethod()>
    Public Sub PositiveTesting2()

        Dim beginDate As Date = New Date(2015, 01, 01)
        Dim endDate As Date = New Date(2015, 01, 15)

        Assert.AreEqual(1, beginDate.GetFVSTMonths(endDate))

    End Sub

    <TestMethod()>
    Public Sub PositiveTesting3()

        Dim beginDate As Date = New Date(2015, 01, 01)
        Dim endDate As Date = New Date(2015, 02, 1)

        Assert.AreEqual(1, beginDate.GetFVSTMonths(endDate))

    End Sub


    <TestMethod()>
    Public Sub PositiveTesting4()

        Dim beginDate As Date = New Date(2015, 01, 01)
        Dim endDate As Date = New Date(2015, 02, 14)

        Assert.AreEqual(1, beginDate.GetFVSTMonths(endDate))

    End Sub

    <TestMethod()>
    Public Sub PositiveTesting5()

        Dim beginDate As Date = New Date(2015, 01, 01)
        Dim endDate As Date = New Date(2015, 02, 15)

        Assert.AreEqual(2, beginDate.GetFVSTMonths(endDate))

    End Sub

    <TestMethod()>
    Public Sub PositiveTesting6()

        Dim beginDate As Date = New Date(2015, 01, 01)
        Dim endDate As Date = New Date(2015, 01, 01)

        Assert.AreEqual(0, beginDate.GetFVSTMonths(endDate))

    End Sub

    <TestMethod(), ExpectedException(GetType(InvalidOperationException))>
    Public Sub NegativeTesting1()

        Dim beginDate As Date = New Date(2015, 02, 15)
        Dim endDate As Date = New Date(2015, 01, 01)

        beginDate.GetFVSTMonths(endDate)

    End Sub


End Class