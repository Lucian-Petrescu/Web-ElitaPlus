Imports System.Configuration
Imports System.IO
Imports System.IO.Compression
Imports System.Threading
Imports Assurant.Common.Ftp
Imports Assurant.Elita.WorkerFramework
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports Assurant.ElitaPlus.Common
Imports Assurant.ElitaPlus.Security
Imports ElitaHarvesterService.Workers.DocumentUpload.NameParsers
Imports JetBrains.Annotations


Namespace Workers.DocumentUpload
    Public Class DocumentUploadWorker
        Inherits BaseWorker

        'Private Const CompanyCode As String = "AIF"
        Private ReadOnly _workerAdapter As IWorkerAdapter

        Private Const OutputNameFormat As String = "{0}_{1}_CONTRACTS_{2}.txt"
        Private Const WorkingFolder As String = "Work"

        Private Shared ReadOnly DealerParsers As IReadOnlyList(Of DocumentNameParser) = New List(Of DocumentNameParser) From {New DartyDocNameParser(), New SfrDocNameParser(), New MsgDocNameParser()}

        Private ReadOnly Property Configurations As DocumentUploadWorkerConfig
            Get
                Return DirectCast(DirectCast(_workerAdapter, WorkerAdapter).Configuration, DocumentUploadWorkerConfig)
            End Get
        End Property

        Public Sub New(pWorkerAdapter As IWorkerAdapter)
            MyBase.New(pWorkerAdapter)
            _workerAdapter = pWorkerAdapter
        End Sub

        Private Sub Login()

            Dim networkId = ConfigurationManager.AppSettings("NETWORKID")
            Dim hub = Configurations.Hub
            Dim domain = Configurations.MachineDomain

            Dim oAuthentication = New Authentication()

            Try
                oAuthentication.CreatePrincipalForServices(networkId, hub, domain)
            Catch ex As Exception
                Logger.WriteTrace(String.Format("Login Failed for Name : {0}, Hub : {1}, Machine Domain {2}", networkId, hub, domain))
                Throw
            End Try

        End Sub

        Public Overrides Sub Execute()

            '' Step 1 - Login - Create Principle and Identity
            Login()

            '' Step 2 - Write Folder Information in Trace
            Logger.WriteTrace(
                String.Format(
                    "Drop Folder : {0}, Compressed File Search Pattern : {1}, Staging Folder : {2}, Error Folder : {3}, Process Folder : {4}",
                    Configurations.DropFolder,
                    Configurations.CompressedFileSearchPattern,
                    Configurations.StageingFolder,
                    Configurations.ErrorFolder,
                    Configurations.ProcessedFolder))

            '' Step 3 - Find Files from Drop Folder with Matching Compressed File Pattern, when no matching file found then do nothing
            Dim hasWork = ProcessDropFolder()

            '' Step 4 - Find Files from Staging Folder, when no files found then do nothing
            hasWork = hasWork Or ProcessStagingFolder()

            '' Step 5 - Determine if System needs to Sleep or Work
            If (Not hasWork) Then
                NoWork()
            End If
        End Sub

        ''' <summary>
        ''' Processes contents of Staging Folder
        ''' </summary>
        ''' <returns>true when atleast 1 File is processed, false otherwise</returns>
        Private Function ProcessStagingFolder() As Boolean

            Dim di = New DirectoryInfo(Configurations.StageingFolder)
            Dim documentFiles = di.GetFiles("*", SearchOption.AllDirectories)

            If documentFiles Is Nothing OrElse documentFiles.Length = 0 Then
                Return False
            End If

            Dim datas = New List(Of DocumentNameParser)()

            For Each documentFile In documentFiles

                If documentFile.Name.ToUpperInvariant() = "THUMBS.DB" Then
                    Continue For
                End If
                '' Read File Name

                Dim errorFileName As String

                Dim data As DocumentNameParser = GetNameParser(documentFile)

                If data Is Nothing Then
                    Logger.WriteWarning(String.Format("File {0} from Staging Errored - The Name format doesn't match any parser", documentFile.FullName))
                    errorFileName = String.Format("{0}{1}{2}", documentFile.Name.Substring(0, documentFile.Name.Length - documentFile.Extension.Length), "_UnknownNamingFormat", documentFile.Extension)
                    MoveToErrorFolder(documentFile, errorFileName)
                    Continue For
                End If

                If Not data.ValidSize() Then
                    Logger.WriteWarning(String.Format("File {0} from Staging Errored - File size more than 10 MB", documentFile.FullName))
                    MoveToErrorFolder(data.Document, data.ErrorTuple.Item1, data.ErrorTuple.Item2)
                    Continue For
                End If

                'This part is probably not needed. Let it there for some extreme scenario - Leinier
                If Not data.ValidProperties() Then
                    Logger.WriteWarning(String.Format("File {0} from Staging Errored - Certificate or Document Blank", documentFile.FullName))
                    MoveToErrorFolder(data.Document, data.ErrorTuple.Item1, data.ErrorTuple.Item2)
                    Continue For
                End If

                Dim documentTypeId As Guid = data.ValidDocumentType()
                If documentTypeId = Guid.Empty Then
                    Logger.WriteWarning(String.Format("File {0} from Staging Errored - Document Type Not Found", documentFile.FullName))
                    MoveToErrorFolder(data.Document, data.ErrorTuple.Item1, data.ErrorTuple.Item2)
                    Continue For
                End If

                Dim certificateId As Guid = data.ValidCertificate()
                If certificateId = Guid.Empty Then
                    Logger.WriteWarning(String.Format("File {0} from Staging Errored - Certificate Not Found", documentFile.FullName))
                    MoveToErrorFolder(data.Document, data.ErrorTuple.Item1, data.ErrorTuple.Item2)
                    Continue For
                End If

                If Not data.ValidStatus() Then
                    Logger.WriteWarning(String.Format("File {0} from Staging Errored - Invalid Status Found", documentFile.FullName))
                    MoveToErrorFolder(data.Document, data.ErrorTuple.Item1, data.ErrorTuple.Item2)
                    Continue For
                End If

                Dim oCertificate = New Certificate(certificateId)

                Try
                    oCertificate.AttachImage(
                    documentTypeId,
                    DateTime.Now,
                    documentFile.Name,
                    "Added By Contract Verification Process",
                    Thread.CurrentPrincipal.GetNetworkId(),
                    File.ReadAllBytes(documentFile.FullName))


                Catch ex As DocumentFormatNotFound
                    'Throw New FaultException(Of FileTypeNotValidFault)(New FileTypeNotValidFault) 
                    Logger.WriteWarning(String.Format("File {0} from Staging Errored - Invalid File Type", documentFile.FullName))
                    errorFileName = String.Format("{0}{1}{2}", documentFile.Name.Substring(0, documentFile.Name.Length - documentFile.Extension.Length), "_InvalidFileFormat", documentFile.Extension)
                    MoveToErrorFolder(documentFile, errorFileName)
                    Continue For
                Catch ex As ImageRepositoryNotFound
                    'Throw New FaultException(Of ImageRepositoryNotFoundFault)(New ImageRepositoryNotFoundFault() With {.RepositoryCode = ex.RepositoryName})
                    Logger.WriteWarning(String.Format("File {0} from Staging Errored - Image Repoistory Not Found", documentFile.FullName))
                    errorFileName = String.Format("{0}{1}{2}", documentFile.Name.Substring(0, documentFile.Name.Length - documentFile.Extension.Length), "_ImageRepositoryNotFound", documentFile.Extension)
                    MoveToErrorFolder(documentFile, errorFileName)
                    Continue For
                Catch ex As Exception
                    Logger.WriteWarning(String.Format("File {0} from Staging Errored - Attach Image Error", documentFile.FullName))
                    Logger.WriteException(ex)
                    errorFileName = String.Format("{0}{1}{2}", documentFile.Name.Substring(0, documentFile.Name.Length - documentFile.Extension.Length), "_ImageError", documentFile.Extension)
                    MoveToErrorFolder(documentFile, errorFileName)
                    Continue For
                End Try
                '' Move file to Processed Folder

                Try
                    File.Move(documentFile.FullName, String.Format("{0}{1}{2}", Configurations.ProcessedFolder, Path.DirectorySeparatorChar, documentFile.Name))
                Catch ex As Exception
                    Continue For
                End Try

                'Add the processed file to the list to generate outputs if needed
                datas.Add(data)
            Next

            'Generate output for processed files when GeneratesOutput = True
            Dim outputAndTriggers = GenerateOutput(datas)

            'Uploads the generated output files to the FTP folder
            FtpUploadOutput(outputAndTriggers)

            Return True

        End Function

        ''' <summary>
        ''' Processes contents of Drop Folder
        ''' </summary>
        ''' <returns>true when atleast 1 File is processed, false otherwise</returns>
        Private Function ProcessDropFolder() As Boolean

            Dim zipFileNames() = Directory.GetFiles(Configurations.DropFolder, Configurations.CompressedFileSearchPattern)
            If zipFileNames Is Nothing OrElse zipFileNames.Length = 0 Then
                Return False
            End If

            For Each zipFileName In zipFileNames
                '' For each compressed file, uncompress contents into StageingFolder
                Dim fi = New FileInfo(zipFileName)
                Dim filename As String = fi.Name

                Try
                    '' UnCompress the File
                    ZipFile.ExtractToDirectory(zipFileName, Configurations.StageingFolder)

                    '' Move Compressed file to Processed Folder
                    File.Move(zipFileName, String.Format("{0}{1}{2}", Configurations.ProcessedFolder, Path.DirectorySeparatorChar, filename))

                Catch ex As Exception
                    Logger.WriteException(ex)
                    '' Move Compressed file to Error Folder
                    File.Move(zipFileName, String.Format("{0}{1}{2}", Configurations.ErrorFolder, Path.DirectorySeparatorChar, filename))
                End Try
            Next

            Return True

        End Function

        Private Function GenerateOutput(datas As IEnumerable(Of DocumentNameParser)) As IEnumerable(Of Tuple(Of String, String))
            Dim outputAndTriggers = New List(Of Tuple(Of String, String))()
            'gets all the files that the parser generates output
            Dim validFiles = datas.Where(Function(p) p.GeneratesOutput).[Select](Function(x) New With {
                                                                                    x.CompanyCode,
                                                                                    x.DealerFileType,
                                                                                    x.LayoutTuple,
                                                                                    x.FileData})
            'grouping the data by parser 
            Dim groupedData = validFiles.
                    GroupBy(Function(data) New With {
                               Key .CompanyCode = data.CompanyCode,
                               Key .DealerFileType = data.DealerFileType,
                               Key .LayouTuple = data.LayoutTuple}).
                    [Select](Function(x) New With {
                                x.Key.CompanyCode,
                                x.Key.DealerFileType,
                                x.Key.LayouTuple,
                                Key .FileDatas = x.[Select](Function(fd) fd.FileData).ToList()})

            'create the outputs
            For Each dealerGroup In groupedData
                Dim fileDatas = dealerGroup.FileDatas
                Try
                    'added later as we need to put all the flags in a single row.
                    Dim outFileData = fileDatas.
                            GroupBy(Function(fd) New With {Key.CertificateNumber = fd.CertificateNumber}).
                            [Select](Function(c) New OutFileData With {
                                        .DealerFileType = fileDatas.FirstOrDefault(Function(sm) sm.CertificateNumber = c.Key.CertificateNumber)?.DealerFileType, 
                                        .CertificateNumber = c.Key.CertificateNumber, 
                                        .SepaMandateSignedStatus = c.FirstOrDefault(Function(sm) sm.DocumentType = "SEPA")?.Status, 
                                        .SepaVerificationDate = c.FirstOrDefault(Function(sm) sm.DocumentType = "SEPA")?.FileDate, 
                                        .CertificateSigned = c.FirstOrDefault(Function(sm) sm.DocumentType = "BA")?.Status, 
                                        .CertificateVerificationDate = c.FirstOrDefault(Function(sm) sm.DocumentType = "BA")?.FileDate, 
                                        .CheckSigned = c.FirstOrDefault(Function(sm) sm.DocumentType = "CHECK")?.Status, 
                                        .CheckVerificationDate = c.FirstOrDefault(Function(sm) sm.DocumentType = "CHECK")?.FileDate, 
                                        .CustomerName = c.FirstOrDefault(Function(sm) sm.DocumentType = "CHECK")?.CustomerName, 
                                        .CheckNumber = c.FirstOrDefault(Function(sm) sm.DocumentType = "CHECK")?.CheckNumber, 
                                        .Amount = c.FirstOrDefault(Function(sm) sm.DocumentType = "CHECK")?.Amount}).ToList()

                    Dim outputName As String = String.Format(OutputNameFormat, dealerGroup.CompanyCode, dealerGroup.DealerFileType, DateTime.Now.ToString("yyyMMddHHmm"))
                    Dim workDir As DirectoryInfo = Directory.CreateDirectory(Path.Combine(Configurations.StageingFolder, WorkingFolder))
                    Dim outputFullPath As String = Path.Combine(workDir.FullName, outputName)
                    Dim trcFullPath As String = Path.ChangeExtension(outputFullPath, ".tmp")

                    'create output file if not exists
                    If Not File.Exists(outputFullPath) Then
                        Dim outputFile = File.Create(outputFullPath)
                        outputFile.Close()
                    End If

                    'create trigger file if not exists
                    If Not File.Exists(trcFullPath) Then
                        Dim trcFile = File.Create(trcFullPath)
                        trcFile.Close()
                    End If

                    'write the data to the output file
                    Using writer = New StreamWriter(outputFullPath, True)
                        Try
                            writer.WriteLine(outFileData.First().GetHeaders(dealerGroup.LayouTuple.Item2))
                            For Each fileData In outFileData
                                writer.WriteLine(fileData.GetData(dealerGroup.LayouTuple.Item2))
                            Next

                            writer.Close()
                        Catch ex As Exception
                            Logger.WriteWarning(String.Format("Error writing data to output file {0}", outputFullPath))
                            Logger.WriteException(ex)
                        End Try
                    End Using

                    'write the layout to the trigger file
                    Using writer = New StreamWriter(trcFullPath, True)
                        Try
                            writer.WriteLine(dealerGroup.LayouTuple.Item1)
                            writer.Close()
                        Catch ex As Exception
                            Logger.WriteWarning(String.Format("Error writing data to trigger file {0}", trcFullPath))
                            Logger.WriteException(ex)
                        End Try
                    End Using
                    'rename  trigger file with the correct extension
                    File.Move(trcFullPath, Path.ChangeExtension(trcFullPath, AppConfig.UnixServer.FtpTriggerExtension))
                    'add tuple to the FTP to-do list
                    outputAndTriggers.Add(New Tuple(Of String, String)(outputFullPath, Path.ChangeExtension(trcFullPath, AppConfig.UnixServer.FtpTriggerExtension)))
                Catch ex As Exception
                    Logger.WriteWarning(String.Format("Error creating output for company {0}, dealer {1}", dealerGroup.CompanyCode, dealerGroup.DealerFileType))
                    Logger.WriteException(ex)
                End Try
            Next

            Return outputAndTriggers
        End Function

        Private Sub FtpUploadOutput(outputAndTriggers As IEnumerable(Of Tuple(Of String, String)))
            Dim unixPath As String = AppConfig.UnixServer.FtpDirectory
            Dim objUnixFtp As New sFtp(AppConfig.UnixServer.HostName, unixPath, AppConfig.UnixServer.UserId,
                                       AppConfig.UnixServer.Password)

            For Each tuple In outputAndTriggers
                Try
                    If Not String.IsNullOrWhiteSpace(tuple.Item1) And Not String.IsNullOrWhiteSpace(tuple.Item2) Then
                        'upload files
                        objUnixFtp.UploadFile(tuple.Item1)
                        objUnixFtp.UploadFile(tuple.Item2)
                        'delete files after upload
                        File.Delete(tuple.Item1)
                        File.Delete(tuple.Item2)
                    End If
                Catch ex As Exception
                    Logger.WriteWarning(String.Format("Error uploading file tuple output: {0} - trigger: {1}", tuple.Item1, tuple.Item2))
                    Logger.WriteException(ex)
                Finally
                End Try
            Next
        End Sub

        <UsedImplicitly>
        Private Function GetNameParser(filename As String) As DocumentNameParser
            Dim parserType As Type = DealerParsers.Where(Function(x) x.NameFormat.IsMatch(filename)).[Select](Function(p) p.[GetType]()).FirstOrDefault()
            If parserType Is Nothing Then Return Nothing
            Dim parser = TryCast(Activator.CreateInstance(parserType, New Object() {New FileInfo(filename)}), DocumentNameParser)
            Return parser
        End Function

        Private Function GetNameParser(fileInfo As FileInfo) As DocumentNameParser
            Dim parserType As Type = DealerParsers.Where(Function(x) x.NameFormat.IsMatch(fileInfo.Name)).[Select](Function(p) p.[GetType]()).FirstOrDefault()
            If parserType Is Nothing Then Return Nothing
            Dim parser = TryCast(Activator.CreateInstance(parserType, New Object() {fileInfo}), DocumentNameParser)
            Return parser
        End Function

        Private Sub MoveToErrorFolder(document As FileInfo, errorFileName As String, ByVal Optional additionalPath As String = "")
            Dim errDir As DirectoryInfo = Directory.CreateDirectory(Path.Combine(Configurations.ErrorFolder, additionalPath))
            If File.Exists(Path.Combine(errDir.FullName, errorFileName)) Then
                File.Move(Path.Combine(errDir.FullName, errorFileName), Path.Combine(errDir.FullName, String.Format("{0}{1}", Guid.NewGuid(), errorFileName)))
            End If

            File.Move(document.FullName, String.Format("{0}{1}{2}", errDir.FullName, Path.DirectorySeparatorChar, errorFileName))
        End Sub

    End Class
End Namespace
