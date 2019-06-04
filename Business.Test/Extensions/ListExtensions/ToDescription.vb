Imports Assurant.ElitaPlus.DataEntities

<TestClass()> Public Class ToDescription


#Region "ToDescription(Guid, ICommonManager, String)"
    <TestMethod()> Public Sub PositiveA1()
        ConfigureThread.Configure()

        Dim value As Guid
        value = CommonManager.CoverageType_Accidental

        Assert.AreEqual(Of String)(
            String.Format("{0}#{1}#{2}", ListCodes.CoverageType, LanguageCodes.USEnglish, CoverageTypeCodes.Accidental),
            value.ToDescription(CommonManager.Current, ListCodes.CoverageType))

    End Sub

    <TestMethod()> Public Sub PositiveA2()
        ConfigureThread.Configure()

        Assert.AreEqual(Of String)(String.Empty, Guid.Empty.ToDescription(CommonManager.Current, ListCodes.CoverageType))
    End Sub

    <TestMethod()> Public Sub PositiveA3()
        ConfigureThread.Configure()

        Assert.AreEqual(Of String)(String.Empty, Guid.NewGuid().ToDescription(CommonManager.Current, ListCodes.CoverageType))
    End Sub

    <TestMethod()> Public Sub NegativeA1()
        ConfigureThread.Configure()

        Dim value As Guid
        value = CommonManager.CoverageType_Accidental

        Try
            value.ToDescription(Nothing, ListCodes.CoverageType)
        Catch ex As ArgumentNullException
            Assert.AreEqual(Of String)("pCommonManager", ex.ParamName)
        End Try
    End Sub

    <TestMethod()> Public Sub NegativeA2()
        ConfigureThread.Configure()

        Dim value As Guid = CommonManager.CoverageType_Accidental

        Try
            value.ToDescription(CommonManager.Current, String.Empty)
        Catch ex As ArgumentNullException
            Assert.AreEqual(Of String)("pListCode", ex.ParamName)
        End Try
    End Sub

    <TestMethod()> Public Sub NegativeA3()
        ConfigureThread.Configure()

        Dim value As Guid = CommonManager.CoverageType_Accidental

        Try
            value.ToDescription(CommonManager.Current, Nothing)
        Catch ex As ArgumentNullException
            Assert.AreEqual(Of String)("pListCode", ex.ParamName)
        End Try
    End Sub

#End Region

#Region "ToDescription(Guid, ICommonManager, String, String)"
    <TestMethod()> Public Sub PositiveB1()
        ConfigureThread.Configure()

        Dim value As Guid
        value = CommonManager.CoverageType_Accidental

        Assert.AreEqual(Of String)(
            String.Format("{0}#{1}#{2}", ListCodes.CoverageType, LanguageCodes.Chinese, CoverageTypeCodes.Accidental),
            value.ToDescription(CommonManager.Current, ListCodes.CoverageType, LanguageCodes.Chinese))

    End Sub

    <TestMethod()> Public Sub PositiveB2()
        ConfigureThread.Configure()

        Assert.AreEqual(Of String)(String.Empty, Guid.Empty.ToDescription(CommonManager.Current, ListCodes.CoverageType, LanguageCodes.Chinese))
    End Sub

    <TestMethod()> Public Sub PositiveB3()
        ConfigureThread.Configure()

        Assert.AreEqual(Of String)(String.Empty, Guid.NewGuid().ToDescription(CommonManager.Current, ListCodes.CoverageType, LanguageCodes.USEnglish))
    End Sub

    <TestMethod()> Public Sub NegativeB1()
        ConfigureThread.Configure()

        Dim value As Guid
        value = CommonManager.CoverageType_Accidental

        Try
            value.ToDescription(Nothing, ListCodes.CoverageType, LanguageCodes.Chinese)
        Catch ex As ArgumentNullException
            Assert.AreEqual(Of String)("pCommonManager", ex.ParamName)
        End Try
    End Sub

    <TestMethod()> Public Sub NegativeB2()
        ConfigureThread.Configure()

        Dim value As Guid = CommonManager.CoverageType_Accidental

        Try
            value.ToDescription(CommonManager.Current, String.Empty, LanguageCodes.Chinese)
        Catch ex As ArgumentNullException
            Assert.AreEqual(Of String)("pListCode", ex.ParamName)
        End Try
    End Sub

    <TestMethod()> Public Sub NegativeB3()
        ConfigureThread.Configure()

        Dim value As Guid = CommonManager.CoverageType_Accidental

        Try
            value.ToDescription(CommonManager.Current, Nothing, LanguageCodes.Chinese)
        Catch ex As ArgumentNullException
            Assert.AreEqual(Of String)("pListCode", ex.ParamName)
        End Try
    End Sub

    <TestMethod()> Public Sub NegativeB4()
        ConfigureThread.Configure()

        Dim value As Guid = CommonManager.CoverageType_Accidental

        Try
            value.ToDescription(CommonManager.Current, ListCodes.CoverageType, String.Empty)
        Catch ex As ArgumentNullException
            Assert.AreEqual(Of String)("pLanguageCode", ex.ParamName)
        End Try
    End Sub

    <TestMethod()> Public Sub NegativeB5()
        ConfigureThread.Configure()

        Dim value As Guid = CommonManager.CoverageType_Accidental

        Try
            value.ToDescription(CommonManager.Current, ListCodes.CoverageType, Nothing)
        Catch ex As ArgumentNullException
            Assert.AreEqual(Of String)("pLanguageCode", ex.ParamName)
        End Try
    End Sub
#End Region

#Region "ToDescription(String, ICommonManager, String)"
    <TestMethod()> Public Sub PositiveC1()
        ConfigureThread.Configure()

        Dim value As String
        value = CoverageTypeCodes.Accidental

        Assert.AreEqual(Of String)(
            String.Format("{0}#{1}#{2}", ListCodes.CoverageType, LanguageCodes.USEnglish, CoverageTypeCodes.Accidental),
            value.ToDescription(CommonManager.Current, ListCodes.CoverageType))

    End Sub

    <TestMethod()> Public Sub PositiveC2()
        ConfigureThread.Configure()

        Assert.AreEqual(Of String)(String.Empty, String.Empty.ToDescription(CommonManager.Current, ListCodes.CoverageType))
    End Sub

    <TestMethod()> Public Sub PositiveC3()
        ConfigureThread.Configure()

        Assert.AreEqual(Of String)(String.Empty, "ABCDEFGHIJK".ToDescription(CommonManager.Current, ListCodes.CoverageType))
    End Sub

    <TestMethod()> Public Sub NegativeC1()
        ConfigureThread.Configure()

        Dim value As String
        value = CoverageTypeCodes.Accidental

        Try
            value.ToDescription(Nothing, ListCodes.CoverageType)
        Catch ex As ArgumentNullException
            Assert.AreEqual(Of String)("pCommonManager", ex.ParamName)
        End Try
    End Sub

    <TestMethod()> Public Sub NegativeC2()
        ConfigureThread.Configure()

        Dim value As String = CoverageTypeCodes.Accidental

        Try
            value.ToDescription(CommonManager.Current, String.Empty)
        Catch ex As ArgumentNullException
            Assert.AreEqual(Of String)("pListCode", ex.ParamName)
        End Try
    End Sub

    <TestMethod()> Public Sub NegativeC3()
        ConfigureThread.Configure()

        Dim value As String = CoverageTypeCodes.Accidental

        Try
            value.ToDescription(CommonManager.Current, Nothing)
        Catch ex As ArgumentNullException
            Assert.AreEqual(Of String)("pListCode", ex.ParamName)
        End Try
    End Sub

#End Region

#Region "ToDescription(String, ICommonManager, String, String)"
    <TestMethod()> Public Sub PositiveD1()
        ConfigureThread.Configure()

        Dim value As String
        value = CoverageTypeCodes.Accidental

        Assert.AreEqual(Of String)(
            String.Format("{0}#{1}#{2}", ListCodes.CoverageType, LanguageCodes.Chinese, CoverageTypeCodes.Accidental),
            value.ToDescription(CommonManager.Current, ListCodes.CoverageType, LanguageCodes.Chinese))

    End Sub

    <TestMethod()> Public Sub PositiveD2()
        ConfigureThread.Configure()

        Assert.AreEqual(Of String)(String.Empty, String.Empty.ToDescription(CommonManager.Current, ListCodes.CoverageType, LanguageCodes.Chinese))
    End Sub

    <TestMethod()> Public Sub PositiveD3()
        ConfigureThread.Configure()

        Assert.AreEqual(Of String)(String.Empty, "ABCDEFGHIJK".ToDescription(CommonManager.Current, ListCodes.CoverageType, LanguageCodes.USEnglish))
    End Sub

    <TestMethod()> Public Sub NegativeD1()
        ConfigureThread.Configure()

        Dim value As String
        value = CoverageTypeCodes.Accidental

        Try
            value.ToDescription(Nothing, ListCodes.CoverageType, LanguageCodes.Chinese)
        Catch ex As ArgumentNullException
            Assert.AreEqual(Of String)("pCommonManager", ex.ParamName)
        End Try
    End Sub

    <TestMethod()> Public Sub NegativeD2()
        ConfigureThread.Configure()

        Dim value As String = CoverageTypeCodes.Accidental

        Try
            value.ToDescription(CommonManager.Current, String.Empty, LanguageCodes.Chinese)
        Catch ex As ArgumentNullException
            Assert.AreEqual(Of String)("pListCode", ex.ParamName)
        End Try
    End Sub

    <TestMethod()> Public Sub NegativeD3()
        ConfigureThread.Configure()

        Dim value As String = CoverageTypeCodes.Accidental

        Try
            value.ToDescription(CommonManager.Current, Nothing, LanguageCodes.Chinese)
        Catch ex As ArgumentNullException
            Assert.AreEqual(Of String)("pListCode", ex.ParamName)
        End Try
    End Sub

    <TestMethod()> Public Sub NegativeD4()
        ConfigureThread.Configure()

        Dim value As String = CoverageTypeCodes.Accidental

        Try
            value.ToDescription(CommonManager.Current, ListCodes.CoverageType, String.Empty)
        Catch ex As ArgumentNullException
            Assert.AreEqual(Of String)("pLanguageCode", ex.ParamName)
        End Try
    End Sub

    <TestMethod()> Public Sub NegativeD5()
        ConfigureThread.Configure()

        Dim value As String = CoverageTypeCodes.Accidental

        Try
            value.ToDescription(CommonManager.Current, ListCodes.CoverageType, Nothing)
        Catch ex As ArgumentNullException
            Assert.AreEqual(Of String)("pLanguageCode", ex.ParamName)
        End Try
    End Sub
#End Region

End Class