Imports System.Transactions
Imports System.Text
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports Assurant.ElitaPlus.Common

<TestClass()>
Public Class InvoiceTest
    Private testContextInstance As TestContext
    Private Shared CountryInstance As Country
    Private Shared ServiceCenterInstance As ServiceCenter

    '''<summary>
    '''Gets or sets the test context which provides
    '''information about and functionality for the current test run.
    '''</summary>
    Public Property TestContext() As TestContext
        Get
            Return testContextInstance
        End Get
        Set(ByVal value As TestContext)
            testContextInstance = value
        End Set
    End Property

#Region "Additional test attributes"
    '
    ' You can use the following additional attributes as you write your tests:
    '
    ' Use ClassInitialize to run code before running the first test in the class
    <ClassInitialize()> _
    Public Shared Sub MyClassInitialize(ByVal testContext As TestContext)
        TestUtility.Login()
        Dim dvSc As DataView
        Dim dv As Country.CountrySearchDV = Country.getList("*", "*")
        If (dv.Count = 0) Then Throw New Exception("No Countries are Configured")
        For Each dr As DataRowView In dv
            CountryInstance = New Country(New Guid(CType(dr(Country.CountrySearchDV.COL_COUNTRY_ID), Byte())))
            dvSc = ServiceCenter.GetServiceCenterForCountry(CountryInstance.Id)
            If (dvSc.Count > 0) Then
                ServiceCenterInstance = New ServiceCenter(New Guid(CType(dvSc(0)(ServiceCenter.ServiceCenterSearchDV.COL_SERVICE_CENTER_ID), Byte())))
                Exit For
            End If
        Next
    End Sub
    '
    ' Use ClassCleanup to run code after all tests in a class have run
    ' <ClassCleanup()> Public Shared Sub MyClassCleanup()
    ' End Sub
    '
    ' Use TestInitialize to run code before running each test
    <TestInitialize()> Public Sub MyTestInitialize()
        TestUtility.Login()
    End Sub
    '
    ' Use TestCleanup to run code after each test has run
    ' <TestCleanup()> Public Sub MyTestCleanup()
    ' End Sub
    '
#End Region

    <TestMethod()> _
    Public Sub GetInvoice()
        'Dim invoice As Invoice
    End Sub

    <TestMethod()> _
    Public Sub InitializeNewInvoice()
        Dim invoice As Invoice
        invoice = New Invoice()
        Assert.AreEqual(invoice.ServiceCenterId, Guid.Empty)
        Assert.IsNull(invoice.InvoiceNumber)
        Assert.AreEqual(Date.Today, invoice.InvoiceDate)
        Assert.AreEqual(0D, invoice.InvoiceAmount)
        Assert.AreEqual(LookupListNew.GetIdFromCode(LookupListNew.LK_INVOICE_STATUS, Codes.INVOICE_STATUS__NEW), invoice.InvoiceStatusId)
        Assert.AreEqual(LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_N), invoice.IsCompleteId)
        Assert.IsNull(invoice.Source)
        Assert.IsNull(invoice.DueDate)
    End Sub

    <TestMethod()> _
    Public Sub CreateNewInvoice()
        Dim invoice As Invoice
        Dim ds As New DataSet
        Dim ticks As Long = DateTime.Now.Ticks

        invoice = New Invoice
        invoice.ServiceCenterId = ServiceCenterInstance.Id
        invoice.InvoiceNumber = String.Format("UT{0}", ticks)
        invoice.InvoiceDate = DateTime.Today
        invoice.InvoiceAmount = 500
        'invoice.InvoiceStatusId = LookupListNew.GetIdFromCode(Codes.INVOICE_STATUS, Codes.INVOICE_STATUS__NEW)
        invoice.InvoiceStatusId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, Codes.YESNO_N)
        invoice.Source = String.Format("SRC{0}", ticks)
        invoice.DueDate = DateTime.Today.AddDays(10)

        Using scope As New TransactionScope()

            invoice.Save()
            scope.Dispose()
        End Using

        Dim readInvoice As New Invoice(invoice.Id)
        Assert.AreEqual(readInvoice.Id, invoice.Id)
        Assert.AreEqual(readInvoice.ServiceCenterId, invoice.ServiceCenterId)
        Assert.AreEqual(readInvoice.InvoiceDate, invoice.InvoiceDate)
        Assert.AreEqual(readInvoice.InvoiceAmount, invoice.InvoiceAmount)
        Assert.AreEqual(readInvoice.InvoiceStatusId, invoice.InvoiceStatusId)
        Assert.AreEqual(readInvoice.Source, invoice.Source)
        Assert.AreEqual(readInvoice.DueDate, invoice.DueDate)
        Assert.AreEqual(readInvoice.ServiceCenter.Id, invoice.ServiceCenterId)
    End Sub

End Class
