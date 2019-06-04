Imports System.Text
Imports Assurant.ElitaPlus.Business
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()> Public Class Initialize

    <TestMethod()> Public Sub Negative1()

        Try
            Dim svc As New FVSTMobileApplicationService(Nothing, New Moq.Mock(Of ICommonManager)().Object)
        Catch ex As ArgumentNullException
            Assert.AreEqual(Of String)(ex.ParamName, "pCertificateManager")
        End Try

    End Sub

    <TestMethod()> Public Sub Negative2()

        Try
            Dim svc As New FVSTMobileApplicationService(New Moq.Mock(Of ICertificateManager)().Object, Nothing)
        Catch ex As ArgumentNullException
            Assert.AreEqual(Of String)(ex.ParamName, "pCommonManager")
        End Try

    End Sub

    Private Function GetInstance(Optional ByVal pCertificateManager As ICertificateManager = Nothing,
                                 Optional ByVal pCommonManager As ICommonManager = Nothing) As FVSTMobileApplicationService

        If (pCommonManager Is Nothing) Then

        End If

        If (pCertificateManager Is Nothing) Then

        End If

        Return New FVSTMobileApplicationService(pCertificateManager, pCommonManager)

    End Function


End Class