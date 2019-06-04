Imports System.Text
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.ElitaPlus.Common

<TestClass()>
Public Class AutoBalancingTest
    Private TestDataPath As String = "c:\Elita+\Source\Elita+\BusinessObjects.Test\Claims\AutoBalancingTestData"


    ' Use TestInitialize to run code before running each test
    <TestInitialize()> Public Sub MyTestInitialize()
        TestUtility.Login()
    End Sub

    Private Function GetDataSet(ByVal fileName As String) As DataSet
        Dim ds As New DataSet
        'Dim c As MultiAuthClaim = ClaimFacade.Instance.GetClaim(Of MultiAuthClaim)(New Guid(GuidControl.HexToByteArray("5B178CBDE1F3584CB54AE8DEED0E0CFA")), ds)
        'Dim i As Integer = c.ClaimAuthorizationChildren.First().ClaimAuthorizationHistoryChildren.Count
        'Dim inv As New Invoice(New Guid(GuidControl.HexToByteArray("D7E614FC011832BDE0400A0A3105422A")), ds)
        'Dim s As String = inv.InvoiceItemChildren.Where(Function(item) item.InvoiceReconciliationId <> Guid.Empty).First().InvoiceReconciliation.ReconciledAmount.ToString()
        ds.ReadXmlSchema(String.Format("{0}{1}{2}", TestDataPath, IO.Path.DirectorySeparatorChar, "TestSchema.xml"))
        ds.ReadXml(String.Format("{0}{1}{2}", TestDataPath, IO.Path.DirectorySeparatorChar, fileName))
        Return ds
    End Function

    ''' <summary>
    ''' Claim Authorization with one Service Class/Type, Contains Deductible = False, Same Amounts
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase01()
        Dim ds As DataSet = GetDataSet("TestCase01.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Reconsiled, .ClaimAuthStatus)
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), .ClaimAuthorizationItemChildren.Sum(Function(item) item.Amount.Value))
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), 25D)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Claim Authorization with multiple Service Class/Type, Contains Deductible = False, Same Amounts
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase02()
        Dim ds As DataSet = GetDataSet("TestCase02.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Reconsiled, .ClaimAuthStatus)
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), .ClaimAuthorizationItemChildren.Sum(Function(item) item.Amount.Value))
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), 65D)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Claim Authorization with one Service Class/Type, Contains Deductible = False, Invoice Deficit Amounts
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase03()
        Dim ds As DataSet = GetDataSet("TestCase03.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Reconsiled, .ClaimAuthStatus)
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), .ClaimAuthorizationItemChildren.Sum(Function(item) item.Amount.Value))
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), 25D)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Claim Authorization with multiple Service Class/Type, Contains Deductible = False, Invoice Deficit Amounts
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase04()
        Dim ds As DataSet = GetDataSet("TestCase04.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Reconsiled, .ClaimAuthStatus)
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), .ClaimAuthorizationItemChildren.Sum(Function(item) item.Amount.Value))
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), 40D)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Claim Authorization with one Service Class/Type, Contains Deductible = False, Authorization Deficit Amounts
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase05()
        Dim ds As DataSet = GetDataSet("TestCase05.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Reconsiled, .ClaimAuthStatus)
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), .ClaimAuthorizationItemChildren.Sum(Function(item) item.Amount.Value))
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), 15D)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Claim Authorization with multiple Service Class/Type, Contains Deductible = False, Authorization Deficit Amounts
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase06()
        Dim ds As DataSet = GetDataSet("TestCase06.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Reconsiled, .ClaimAuthStatus)
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), .ClaimAuthorizationItemChildren.Sum(Function(item) item.Amount.Value))
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), 65D)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Claim Authorization with multiple Service Class/Type, Contains Deductible = False, Mixied Deficit Amounts
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase07()
        Dim ds As DataSet = GetDataSet("TestCase07.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Reconsiled, .ClaimAuthStatus)
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), .ClaimAuthorizationItemChildren.Sum(Function(item) item.Amount.Value))
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), 65D)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Claim Authorization with one Service Class/Type, Contains Deductible = True, Same Amounts
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase08()
        Dim ds As DataSet = GetDataSet("TestCase08.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Reconsiled, .ClaimAuthStatus)
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), .ClaimAuthorizationItemChildren.Sum(Function(item) item.Amount.Value))
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), 15D)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Claim Authorization with multiple Service Class/Type, Contains Deductible = True, Same Amounts
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase09()
        Dim ds As DataSet = GetDataSet("TestCase09.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Reconsiled, .ClaimAuthStatus)
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), .ClaimAuthorizationItemChildren.Sum(Function(item) item.Amount.Value))
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), 55D)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Claim Authorization with one Service Class/Type, Contains Deductible = True, Invoice Deficit Amounts
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase10()
        Dim ds As DataSet = GetDataSet("TestCase10.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Reconsiled, .ClaimAuthStatus)
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), .ClaimAuthorizationItemChildren.Sum(Function(item) item.Amount.Value))
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), 15D)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Claim Authorization with multiple Service Class/Type, Contains Deductible = True, Invoice Deficit Amounts
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase11()
        Dim ds As DataSet = GetDataSet("TestCase11.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Reconsiled, .ClaimAuthStatus)
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), .ClaimAuthorizationItemChildren.Sum(Function(item) item.Amount.Value))
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), 30D)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Claim Authorization with one Service Class/Type, Contains Deductible = True, Authorization Deficit Amounts
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase12()
        Dim ds As DataSet = GetDataSet("TestCase12.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Reconsiled, .ClaimAuthStatus)
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), .ClaimAuthorizationItemChildren.Sum(Function(item) item.Amount.Value))
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), 5D)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Claim Authorization with multiple Service Class/Type, Contains Deductible = True, Authorization Deficit Amounts
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase13()
        Dim ds As DataSet = GetDataSet("TestCase13.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Reconsiled, .ClaimAuthStatus)
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), .ClaimAuthorizationItemChildren.Sum(Function(item) item.Amount.Value))
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), 55D)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Claim Authorization with multiple Service Class/Type, Contains Deductible = True, Mexied Deficit Amounts
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase14()
        Dim ds As DataSet = GetDataSet("TestCase14.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Reconsiled, .ClaimAuthStatus)
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), .ClaimAuthorizationItemChildren.Sum(Function(item) item.Amount.Value))
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), 55D)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Claim Authorization with one Service Class/Type, Contains Deductible = True, Same Amounts, Pay Deductible
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase15()
        Dim ds As DataSet = GetDataSet("TestCase15.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Reconsiled, .ClaimAuthStatus)
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), .ClaimAuthorizationItemChildren.Sum(Function(item) item.Amount.Value))
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), 25D)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Claim Authorization with multiple Service Class/Type, Contains Deductible = True, Same Amounts, Pay Deductible
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase16()
        Dim ds As DataSet = GetDataSet("TestCase16.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Reconsiled, .ClaimAuthStatus)
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), .ClaimAuthorizationItemChildren.Sum(Function(item) item.Amount.Value))
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), 65D)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Claim Authorization with one Service Class/Type, Contains Deductible = True, Invoice Deficit Amounts, Pay Deductible
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase17()
        Dim ds As DataSet = GetDataSet("TestCase17.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Reconsiled, .ClaimAuthStatus)
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), .ClaimAuthorizationItemChildren.Sum(Function(item) item.Amount.Value))
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), 25D)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Claim Authorization with multiple Service Class/Type, Contains Deductible = True, Invoice Deficit Amounts, Pay Deductible
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase18()
        Dim ds As DataSet = GetDataSet("TestCase18.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Reconsiled, .ClaimAuthStatus)
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), .ClaimAuthorizationItemChildren.Sum(Function(item) item.Amount.Value))
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), 40D)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Claim Authorization with one Service Class/Type, Contains Deductible = True, Authorization Deficit Amounts, Pay Deductible
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase19()
        Dim ds As DataSet = GetDataSet("TestCase19.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Reconsiled, .ClaimAuthStatus)
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), .ClaimAuthorizationItemChildren.Sum(Function(item) item.Amount.Value))
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), 15D)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Claim Authorization with multiple Service Class/Type, Contains Deductible = True, Authorization Deficit Amounts, Pay Deductible
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase20()
        Dim ds As DataSet = GetDataSet("TestCase20.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Reconsiled, .ClaimAuthStatus)
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), .ClaimAuthorizationItemChildren.Sum(Function(item) item.Amount.Value))
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), 65D)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Claim Authorization with multiple Service Class/Type, Contains Deductible = True, Mexied Deficit Amounts, Pay Deductible
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase21()
        Dim ds As DataSet = GetDataSet("TestCase21.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Reconsiled, .ClaimAuthStatus)
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), .ClaimAuthorizationItemChildren.Sum(Function(item) item.Amount.Value))
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), 65D)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Claim Authorization with multiple Service Class/Type, Contains Deductible = False, Same Amounts, Missing Item in Invoice
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase22()
        Dim ds As DataSet = GetDataSet("TestCase22.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Fulfilled, .ClaimAuthStatus)
                Assert.AreEqual(0, ds.Tables(InvoiceReconciliationDAL.TABLE_NAME).Rows.Count)
                Assert.AreEqual(2, ds.Tables(ClaimAuthItemDAL.TABLE_NAME).Rows.Count)
                Assert.AreEqual(1, ds.Tables(InvoiceItemDAL.TABLE_NAME).Rows.Count)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Claim Authorization with multiple Service Class/Type, Contains Deductible = False, Invoice Deficit Amounts, Missing Item in Invoice
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase23()
        Dim ds As DataSet = GetDataSet("TestCase23.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Fulfilled, .ClaimAuthStatus)
                Assert.AreEqual(0, ds.Tables(InvoiceReconciliationDAL.TABLE_NAME).Rows.Count)
                Assert.AreEqual(2, ds.Tables(ClaimAuthItemDAL.TABLE_NAME).Rows.Count)
                Assert.AreEqual(1, ds.Tables(InvoiceItemDAL.TABLE_NAME).Rows.Count)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Claim Authorization with multiple Service Class/Type, Contains Deductible = False, Authorization Deficit Amounts, Missing Item in Invoice
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase24()
        Dim ds As DataSet = GetDataSet("TestCase24.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Fulfilled, .ClaimAuthStatus)
                Assert.AreEqual(0, ds.Tables(InvoiceReconciliationDAL.TABLE_NAME).Rows.Count)
                Assert.AreEqual(2, ds.Tables(ClaimAuthItemDAL.TABLE_NAME).Rows.Count)
                Assert.AreEqual(1, ds.Tables(InvoiceItemDAL.TABLE_NAME).Rows.Count)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Claim Authorization with multiple Service Class/Type, Contains Deductible = False, Same Amounts, Missing Item in Authorization
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase25()
        Dim ds As DataSet = GetDataSet("TestCase25.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Reconsiled, .ClaimAuthStatus)
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), .ClaimAuthorizationItemChildren.Sum(Function(item) item.Amount.Value))
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), 25D)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Claim Authorization with multiple Service Class/Type, Contains Deductible = False, Invoice Deficit Amounts, Missing Item in Authorization
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase26()
        Dim ds As DataSet = GetDataSet("TestCase26.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Reconsiled, .ClaimAuthStatus)
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), .ClaimAuthorizationItemChildren.Sum(Function(item) item.Amount.Value))
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), 15D)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Claim Authorization with multiple Service Class/Type, Contains Deductible = False, Authorization Deficit Amounts, Missing Item in Authorization
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase27()
        Dim ds As DataSet = GetDataSet("TestCase27.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Reconsiled, .ClaimAuthStatus)
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), .ClaimAuthorizationItemChildren.Sum(Function(item) item.Amount.Value))
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), 20D)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Claim Authorization with multiple Service Class/Type, Contains Deductible = True, Same Amounts, Missing Item in Invoice
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase28()
        Dim ds As DataSet = GetDataSet("TestCase28.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Fulfilled, .ClaimAuthStatus)
                Assert.AreEqual(0, ds.Tables(InvoiceReconciliationDAL.TABLE_NAME).Rows.Count)
                Assert.AreEqual(2, ds.Tables(ClaimAuthItemDAL.TABLE_NAME).Rows.Count)
                Assert.AreEqual(1, ds.Tables(InvoiceItemDAL.TABLE_NAME).Rows.Count)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Claim Authorization with multiple Service Class/Type, Contains Deductible = True, Invoice Deficit Amounts, Missing Item in Invoice
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase29()
        Dim ds As DataSet = GetDataSet("TestCase29.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Fulfilled, .ClaimAuthStatus)
                Assert.AreEqual(0, ds.Tables(InvoiceReconciliationDAL.TABLE_NAME).Rows.Count)
                Assert.AreEqual(2, ds.Tables(ClaimAuthItemDAL.TABLE_NAME).Rows.Count)
                Assert.AreEqual(1, ds.Tables(InvoiceItemDAL.TABLE_NAME).Rows.Count)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Claim Authorization with multiple Service Class/Type, Contains Deductible = True, Authorization Deficit Amounts, Missing Item in Invoice
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase30()
        Dim ds As DataSet = GetDataSet("TestCase30.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Fulfilled, .ClaimAuthStatus)
                Assert.AreEqual(0, ds.Tables(InvoiceReconciliationDAL.TABLE_NAME).Rows.Count)
                Assert.AreEqual(2, ds.Tables(ClaimAuthItemDAL.TABLE_NAME).Rows.Count)
                Assert.AreEqual(1, ds.Tables(InvoiceItemDAL.TABLE_NAME).Rows.Count)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Claim Authorization with multiple Service Class/Type, Contains Deductible = True, Same Amounts, Missing Item in Authorization
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase31()
        Dim ds As DataSet = GetDataSet("TestCase31.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Reconsiled, .ClaimAuthStatus)
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), .ClaimAuthorizationItemChildren.Sum(Function(item) item.Amount.Value))
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), 15D)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Claim Authorization with multiple Service Class/Type, Contains Deductible = True, Invoice Deficit Amounts, Missing Item in Authorization
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase32()
        Dim ds As DataSet = GetDataSet("TestCase32.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Reconsiled, .ClaimAuthStatus)
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), .ClaimAuthorizationItemChildren.Sum(Function(item) item.Amount.Value))
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), 5D)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Claim Authorization with multiple Service Class/Type, Contains Deductible = True, Authorization Deficit Amounts, Missing Item in Authorization
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase33()
        Dim ds As DataSet = GetDataSet("TestCase33.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Reconsiled, .ClaimAuthStatus)
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), .ClaimAuthorizationItemChildren.Sum(Function(item) item.Amount.Value))
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), 10D)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Claim Authorization with multiple Service Class/Type, Contains Deductible = True, Same Amounts, Missing Item in Invoice, Pay Deductible = True
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase34()
        Dim ds As DataSet = GetDataSet("TestCase34.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Fulfilled, .ClaimAuthStatus)
                Assert.AreEqual(0, ds.Tables(InvoiceReconciliationDAL.TABLE_NAME).Rows.Count)
                Assert.AreEqual(3, ds.Tables(ClaimAuthItemDAL.TABLE_NAME).Rows.Count)
                Assert.AreEqual(1, ds.Tables(InvoiceItemDAL.TABLE_NAME).Rows.Count)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Claim Authorization with multiple Service Class/Type, Contains Deductible = True, Invoice Deficit Amounts, Missing Item in Invoice, Pay Deductible = True
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase35()
        Dim ds As DataSet = GetDataSet("TestCase35.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Fulfilled, .ClaimAuthStatus)
                Assert.AreEqual(0, ds.Tables(InvoiceReconciliationDAL.TABLE_NAME).Rows.Count)
                Assert.AreEqual(3, ds.Tables(ClaimAuthItemDAL.TABLE_NAME).Rows.Count)
                Assert.AreEqual(1, ds.Tables(InvoiceItemDAL.TABLE_NAME).Rows.Count)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Claim Authorization with multiple Service Class/Type, Contains Deductible = True, Authorization Deficit Amounts, Missing Item in Invoice, Pay Deductible = True
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase36()
        Dim ds As DataSet = GetDataSet("TestCase36.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Fulfilled, .ClaimAuthStatus)
                Assert.AreEqual(0, ds.Tables(InvoiceReconciliationDAL.TABLE_NAME).Rows.Count)
                Assert.AreEqual(3, ds.Tables(ClaimAuthItemDAL.TABLE_NAME).Rows.Count)
                Assert.AreEqual(1, ds.Tables(InvoiceItemDAL.TABLE_NAME).Rows.Count)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Claim Authorization with multiple Service Class/Type, Contains Deductible = True, Same Amounts, Missing Item in Authorization, Pay Deductible = True
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase37()
        Dim ds As DataSet = GetDataSet("TestCase37.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Reconsiled, .ClaimAuthStatus)
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), .ClaimAuthorizationItemChildren.Sum(Function(item) item.Amount.Value))
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), 25D)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Claim Authorization with multiple Service Class/Type, Contains Deductible = True, Invoice Deficit Amounts, Missing Item in Authorization, Pay Deductible = True
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase38()
        Dim ds As DataSet = GetDataSet("TestCase38.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Reconsiled, .ClaimAuthStatus)
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), .ClaimAuthorizationItemChildren.Sum(Function(item) item.Amount.Value))
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), 15D)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Claim Authorization with multiple Service Class/Type, Contains Deductible = True, Authorization Deficit Amounts, Missing Item in Authorization, Pay Deductible = True
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase39()
        Dim ds As DataSet = GetDataSet("TestCase39.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Reconsiled, .ClaimAuthStatus)
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), .ClaimAuthorizationItemChildren.Sum(Function(item) item.Amount.Value))
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), 20D)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Try to Balance Authorization which is not in Fulfilled Status
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase40()
        Dim ds As DataSet = GetDataSet("TestCase40.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            Dim cl As ClaimAuthorizationStatus
            With oInvoice.ClaimAuthorizations.First()
                cl = .ClaimAuthStatus
                oInvoice.Balance.Execute()
                Assert.AreEqual(cl, .ClaimAuthStatus)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Claim Authorization with one Service Class/Type, Contains Deductible = True, Same Amounts with Deductible Line Item in Auth
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase41()
        Dim ds As DataSet = GetDataSet("TestCase41.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Reconsiled, .ClaimAuthStatus)
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), .ClaimAuthorizationItemChildren.Sum(Function(item) item.Amount.Value))
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), 15D)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Contains Claim Authorization with 2 Line Items for Service Type Deductible
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod(), ExpectedException(GetType(BOInvalidOperationException))>
    Public Sub TestCase42()
        Dim ds As DataSet = GetDataSet("TestCase42.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
        Next
    End Sub

    ''' <summary>
    ''' Contains Claim Authorization with 2 Line Items for Service Type Pay Deductible
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod(), ExpectedException(GetType(BOInvalidOperationException))>
    Public Sub TestCase43()
        Dim ds As DataSet = GetDataSet("TestCase43.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
        Next
    End Sub

    ''' <summary>
    ''' Invoice Line Item conatins Exact Entry for Deductible
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase44()
        Dim ds As DataSet = GetDataSet("TestCase44.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Reconsiled, .ClaimAuthStatus)
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), .ClaimAuthorizationItemChildren.Sum(Function(item) item.Amount.Value))
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), 15D)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Invoice Line Item conatins Partial Entry for Deductible
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase45()
        Dim ds As DataSet = GetDataSet("TestCase45.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Reconsiled, .ClaimAuthStatus)
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), .ClaimAuthorizationItemChildren.Sum(Function(item) item.Amount.Value))
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), 15D)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Invoice Line Item conatins Excess Entry for Deductible
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase46()
        Dim ds As DataSet = GetDataSet("TestCase46.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Reconsiled, .ClaimAuthStatus)
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), .ClaimAuthorizationItemChildren.Sum(Function(item) item.Amount.Value))
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), 10D)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Invoice Line Item contains Exact Pay Deductible Entry
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase47()
        Dim ds As DataSet = GetDataSet("TestCase47.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Reconsiled, .ClaimAuthStatus)
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), .ClaimAuthorizationItemChildren.Sum(Function(item) item.Amount.Value))
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), 25D)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Invoice Line Item contains Partial Pay Deductible Entry
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase48()
        Dim ds As DataSet = GetDataSet("TestCase48.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Reconsiled, .ClaimAuthStatus)
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), .ClaimAuthorizationItemChildren.Sum(Function(item) item.Amount.Value))
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), 25D)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Invoice Line Item contains Excess Pay Deductible Entry
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase49()
        Dim ds As DataSet = GetDataSet("TestCase49.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Reconsiled, .ClaimAuthStatus)
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), .ClaimAuthorizationItemChildren.Sum(Function(item) item.Amount.Value))
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), 25D)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Contains Invoice Line Item with 2 Line Items for Service Type Deductible
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod(), ExpectedException(GetType(BOInvalidOperationException))>
    Public Sub TestCase50()
        Dim ds As DataSet = GetDataSet("TestCase50.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
        Next
    End Sub

    ''' <summary>
    ''' Contains Invoice Line Item with 2 Line Items for Service Type Pay Deductible
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod(), ExpectedException(GetType(BOInvalidOperationException))>
    Public Sub TestCase51()
        Dim ds As DataSet = GetDataSet("TestCase51.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
        Next
    End Sub

    ''' <summary>
    ''' Claim Authorization with one Service Class/Type, Contains Deductible = False, Same Amounts, Contains Exact Deductible Line in Invoice
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase52()
        Dim ds As DataSet = GetDataSet("TestCase52.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Fulfilled, .ClaimAuthStatus)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Claim Authorization with one Service Class/Type, Contains Deductible = False, Same Amounts, Contains Partial Deductible Line in Invoice
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase53()
        Dim ds As DataSet = GetDataSet("TestCase53.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Fulfilled, .ClaimAuthStatus)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Claim Authorization with one Service Class/Type, Contains Deductible = False, Same Amounts, Contains Excess Deductible Line in Invoice
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase54()
        Dim ds As DataSet = GetDataSet("TestCase54.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Fulfilled, .ClaimAuthStatus)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Claim Authorization with one Service Class/Type, Contains Deductible = False, Same Amounts, Contains Exact Pay Deductible Line in Invoice
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase55()
        Dim ds As DataSet = GetDataSet("TestCase55.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Fulfilled, .ClaimAuthStatus)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Claim Authorization with one Service Class/Type, Contains Deductible = False, Same Amounts, Contains Partial Pay Deductible Line in Invoice
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase56()
        Dim ds As DataSet = GetDataSet("TestCase56.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Fulfilled, .ClaimAuthStatus)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Claim Authorization with one Service Class/Type, Contains Deductible = False, Same Amounts, Contains Excess Pay Deductible Line in Invoice
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase57()
        Dim ds As DataSet = GetDataSet("TestCase57.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Fulfilled, .ClaimAuthStatus)
            End With
        Next
    End Sub

    <TestMethod()>
    Public Sub TestCase58()
        Dim ds As DataSet = GetDataSet("TestCase58.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Reconsiled, .ClaimAuthStatus)
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), .ClaimAuthorizationItemChildren.Sum(Function(item) item.Amount.Value))
                Assert.AreEqual(325D, oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value))
            End With
        Next
    End Sub

    ''' <summary>
    ''' Claim Authorization with one Service Class/Type, Contains Deductible = False, Same Amounts, Amount Less than Liability Limit
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase59()
        Dim ds As DataSet = GetDataSet("TestCase59.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Reconsiled, .ClaimAuthStatus)
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), .ClaimAuthorizationItemChildren.Sum(Function(item) item.Amount.Value))
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), 25D)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Claim Authorization with one Service Class/Type, Contains Deductible = False, Same Amounts, Amount Equal Liability Limit
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase60()
        Dim ds As DataSet = GetDataSet("TestCase60.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Reconsiled, .ClaimAuthStatus)
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), .ClaimAuthorizationItemChildren.Sum(Function(item) item.Amount.Value))
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), 25D)
            End With
        Next
    End Sub

    ''' <summary>
    ''' Claim Authorization with one Service Class/Type, Contains Deductible = False, Same Amounts, Amount Greater Than Liability Limit
    ''' </summary>
    ''' <remarks></remarks>
    <TestMethod()>
    Public Sub TestCase61()
        Dim ds As DataSet = GetDataSet("TestCase61.xml")
        For Each dr As DataRow In ds.Tables(InvoiceDAL.TABLE_NAME).Rows
            Dim oInvoice As New Invoice(dr)
            oInvoice.Balance.Execute()
            With oInvoice.ClaimAuthorizations.First()
                Assert.AreEqual(ClaimAuthorizationStatus.Reconsiled, .ClaimAuthStatus)
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), .ClaimAuthorizationItemChildren.Sum(Function(item) item.Amount.Value))
                Assert.AreEqual(oInvoice.InvoiceItemChildren.Sum(Function(item) item.Amount.Value), 20D)
            End With
        Next
    End Sub
End Class
