Imports System.Text
Imports System.Threading
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports System.Runtime.CompilerServices

Public Class MigrateCompressedXML
    Inherits ElitaPlusSearchPage

    Private Const PAGETAB As String = "ADMIN"
    Private Const PAGETITLE As String = "MIGRATE_COMPRESSED_XML_DATA"
    Private Shared IsRunning As Boolean = False
    Private Shared outputStringBuilder As StringBuilder = New StringBuilder()

    Private Sub UpdateBreadCrum()
        MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
        MasterPage.UsePageTabTitleInBreadCrum = True
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            MasterPage.MessageController.Clear()
            UpdateBreadCrum()
            UpdateUI()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub

    Private Sub UpdateUI()
        Output.InnerHtml = "<div><table width=""100%""><tr><td></td></tr></table></div><ul>" & outputStringBuilder.ToString() & "</ul>"

        btnCheckRemainingRecords.Enabled = Not IsRunning
        btnCompressXML.Enabled = Not IsRunning

        If IsRunning Then
            RegisterStartupScript("autoPostBack", "<script type=text/javascript>setInterval(function(){" + Page.GetPostBackEventReference(btnRefresh) + "},10000);</script>")
        End If
    End Sub

    Protected Sub btnRefresh_Click(sender As Object, e As EventArgs)
        ' Do nothing
    End Sub

    Protected Sub btnCompressXML_Click(sender As Object, e As EventArgs)
        Dim compressXMLThread As Thread
        compressXMLThread = New Thread(New ThreadStart(AddressOf CompressXML))
        compressXMLThread.Start()

        Thread.CurrentThread.Sleep(100)

        UpdateUI()
    End Sub

    Protected Sub btnCheckRemainingRecords_Click(sender As Object, e As EventArgs) Handles btnCheckRemainingRecords.Click
        Dim numberofrecords As Integer = Assurant.ElitaPlus.BusinessObjectsNew.AcctTransmission.TransmissionsToMigrate()
        outputStringBuilder.AddInformation(String.Format("{0} XML Files to Migrate.", If(numberofrecords = 0, "No", numberofrecords.ToString())))
        Output.InnerHtml = "<div><table width=""100%""><tr><td></td></tr></table></div><ul>" & outputStringBuilder.ToString() & "</ul>"
    End Sub

    Private Sub CompressXML()
        Try
            Dim sw As New System.Diagnostics.Stopwatch
            IsRunning = True
            outputStringBuilder = New StringBuilder()
            outputStringBuilder.AddInformation("Starting Compressing XML")
            sw.Start()

            Dim i As Integer = 0
            Dim networkId As String = ElitaPlusIdentity.Current.ActiveUser.NetworkId
            Dim numberofrecords As Integer = Assurant.ElitaPlus.BusinessObjectsNew.AcctTransmission.TransmissionsToMigrate()
            outputStringBuilder.AddInformation(String.Format("{0} XML Files to Migrate.", If(numberofrecords = 0, "No", numberofrecords.ToString())))

            For i = 1 To numberofrecords Step 100
                Try
                    'Migrating XML Data
                    Assurant.ElitaPlus.BusinessObjectsNew.AcctTransmission.MigrateTransmissions(100, networkId)
                    outputStringBuilder.AddInformation(String.Format("Compressed {0} of {1} XML files.", If(i <= numberofrecords, i, numberofrecords), numberofrecords))
                Catch ex As Exception
                    outputStringBuilder.AddError(String.Format("Failed to Compress XML file {0} because {1}", i, ex.Message()))
                End Try
            Next

            sw.Stop()

            If numberofrecords <> 0 Then
                outputStringBuilder.AddInformation(String.Format("Compressed {0} of {1} XML files.", If(i <= numberofrecords, i, numberofrecords), numberofrecords))
                outputStringBuilder.AddInformation(String.Format("The process took {0} Seconds", sw.ElapsedMilliseconds / 1000))
                outputStringBuilder.AddSuccess("Compressing XML Completed")
            End If

        Catch ex As Exception
            outputStringBuilder.AddError("An error occured in execution")
        Finally
            IsRunning = False
        End Try
    End Sub

End Class
