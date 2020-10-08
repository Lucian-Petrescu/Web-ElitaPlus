Imports System
Imports System.Configuration
Imports System.IO
Imports System.Xml
Imports System.Reflection
Imports Assurant.Common.Ftp
Imports System.ServiceModel
Imports System.Security.Cryptography.X509Certificates
Imports System.Collections.Generic
Imports System.Text


Public Class FelitaEngine
    Inherits BusinessObjectBase

#Region "Private Members"

    'ArrayList of Structure AccountingFiles to hold the set of files, names, and data to transmit
    Private FileSet As ArrayList
    Private FailureFiles As List(Of AccountingFiles)
    Private StartDate As Date
    Private TimeStamp As Long
    Private _AcctCompany As AcctCompany
    Private _Company As Company
    Private xmlDoc As XmlDocument
    Private _acctExtension As String = ""
    Private BusinessUnit As String = ""
    Private EVENT_TYPE As EventType = Nothing
    Private beginDate As Date
    Private endDate As Date

#End Region

#Region "Constants"

    Public Const DATA_COL_NAME_ACCOUNTING_COMPANY_ID As String = "ACCOUNTINGCOMPANYID"
    Public Const DATA_COL_NAME_COMPANY_ID As String = "COMPANYID"
    Public Const DATA_COL_NAME_ACCOUNTING_EVENT_ID As String = "ACCOUNTINGEVENTID"
    Public Const DATA_COL_NAME_ACCOUNTING_PERIOD As String = "ACCOUNTINGPERIOD"
    Public Const DATA_COL_NAME_VENDOR_FILES As String = "VENDORFILES"
    Public Const DATA_COL_NAME_USER_ID As String = "user_id"
    Public Const DATA_COL_NAME_TIMESTAMP As String = "TIMESTAMP"
    Public Const DATA_COL_NAME_SEQUENCE As String = "JOURNALSEQUENCE"
    Public Const DATA_COL_NAME_DESCRIPTION As String = "DESCRIPTION"
    Public Const DATA_COL_NAME_JOURNAL_HEADER_ID As String = "JOURNALHEADERID"

    Public Const SMARTSTREAM_PREFIX As String = "SS"
    Public Const FELITA_PREFIX As String = "FEL"

    Public Const NO_EVENTS As String = "NONE"
    Public Const NO_STRING As String = "N"
    Public Const YES_STRING As String = "Y"

    Public Const TABLE_NAME As String = "FelitaEngine"

    Private SEND_SUCCESSFUL As Boolean = True

    Public Enum FileType
        Journal = 0
        Address = 1
        Account = 2
        Supplier = 3
        BankDetails = 4
        Client = 5
        trigger = 6
    End Enum

    Public Enum FileSubType
        GL = 0
        AP1 = 1
        AP2 = 2
        VEND = 3
        AP = 4
        CONTROL = 5
    End Enum

    Public Enum EventType
        CLAIM
        IBNR
        UPR
        CLAIMRES
        REFUNDS
        PREM
        INV
        REVERSAL
        VEND
        COMMISSIONS
    End Enum

    Public Enum DateFilter
        GreaterThan
        LessThan
        SpecificDate
    End Enum

    Public Enum ProcessMethod As Short
        PROCESS_IMMEDIATELY = 0
        HOLD_GL = 1
        HOLD_AP = 2
        HOLD_ALL = 3
    End Enum

    Private Structure AccountingFiles
        Public FileType As FileType
        Public FileSubType As FileSubType
        Public XMLData As String
        Public FileName As String
        Public AcctTransmissionId As Guid
        Public isValid As Boolean
        Public isResend As Boolean
        Public BatchNumber As String
        Public JournalType As String
        Public EventType As EventType
        Public RejectReason As String
    End Structure

    Private Structure BatchId
        Public Batch_Number As String
        Public Event_Type As EventType
        Public FileName As String
        Public isResend As Boolean
    End Structure

    Public Structure FelitaEngineData

        Public AccountingCompanyId, CompanyId, EventId, UserId As Guid
        Public AllEvents, NoEvents, isVendorFile As Boolean

    End Structure

#End Region

#Region "Constructors"

    Public Sub New(ds As FelitaEngineDs)

        MyBase.New()
        FileSet = New ArrayList
        StartDate = Now
        TimeStamp = Now.Ticks

        MapDataSet(ds)
        Load(ds)

    End Sub

    Public Sub New(AcctCompanyId As Guid)
        MyBase.New()
        FileSet = New ArrayList
        StartDate = Now
        TimeStamp = Now.Ticks

        _AcctCompany = New AcctCompany(AcctCompanyId)
        _acctExtension = LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCT_SYSTEM, ElitaPlusIdentity.Current.ActiveUser.LanguageId, False), _AcctCompany.AcctSystemId)

    End Sub

    Public Sub New()
        MyBase.New()
        FileSet = New ArrayList
        StartDate = Now
        TimeStamp = Now.Ticks

    End Sub

#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String

        Dim encode As New System.Text.ASCIIEncoding
        Dim oRow As DataRow
        Dim oFelitaEngineData As New FelitaEngineData
        Dim _bArry() As Byte
        Dim Counter As Integer = 1

        Try

            AppConfig.DebugMessage.Trace("ACCOUNTING_ENGINE", "PROCESS REQUEST", "Step 1/6 Enter Logic_ " + StartDate.Ticks.ToString)

            'Set the company based on the company code sent in.
            Try
                SetCompany(CompanyId)
            Catch ex As Exception
                AppConfig.DebugMessage.Trace("ACCOUNTING_ENGINE", "PROCESS REQUEST", "Error setting the company Id: " + CompanyId)
                Throw New Exception("Error setting the company Id: " + CompanyId)
            End Try

            With oFelitaEngineData
                .UserId = ElitaPlusIdentity.Current.ActiveUser.Id
                .AccountingCompanyId = _Company.AcctCompanyId
                .CompanyId = _Company.Id
                .isVendorFile = VendorFiles

                If AccountingEventId = NO_EVENTS Then
                    .NoEvents = True
                End If

                If AccountingEventId Is Nothing OrElse AccountingEventId = String.Empty Then
                    .AllEvents = True
                Else
                    .AllEvents = False
                    If Not .NoEvents Then
                        If AccountingEventId.Length = 32 Then
                            .EventId = GuidControl.ByteArrayToGuid(GuidControl.HexToByteArray(AccountingEventId))
                        Else
                            .EventId = LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCOUNTING_EVENT_TYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId, False), AccountingEventId)
                        End If
                    End If
                End If
            End With

            Validate()
            Dim _AcctBO As New AcctTransLog
            Dim dsSet() As DataSet
            Dim RECORDS_EXIST As Boolean = True

            Try
                _AcctCompany = New AcctCompany(oFelitaEngineData.AccountingCompanyId)
            Catch ex As Exception
                AppConfig.Debug("FelitaEngine AcctCompany exceptionAcct Comp ID: " + MiscUtil.GuidToSQLString(oFelitaEngineData.AccountingCompanyId) + " Company id:" + MiscUtil.GuidToSQLString(_Company.Id))
                Throw New Exception("Error loading the accounting company, Acct Comp ID: " + MiscUtil.GuidToSQLString(oFelitaEngineData.AccountingCompanyId) + " Company id:" + MiscUtil.GuidToSQLString(_Company.Id) + " DB Server:" + AppConfig.DataBase.Server, ex)
            End Try

            If _AcctCompany Is Nothing OrElse _AcctCompany.UseAccounting = NO_STRING Then
                Return "0"
            End If
            _acctExtension = LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCT_SYSTEM, ElitaPlusIdentity.Current.ActiveUser.LanguageId, False), _AcctCompany.AcctSystemId)

            'Retrieve an array of datasets (likely only 1 though).  We will have additional if we have multiple business units
            dsSet = _AcctBO.GetAccountingInterfaceTables(oFelitaEngineData, TimeStamp.ToString)
            AppConfig.DebugMessage.Trace("ACCOUNTING_ENGINE", "PROCESS REQUEST", "Step 2/6 Retrieved Data_ " + StartDate.Ticks.ToString)

            If dsSet.Length > 0 Then
                For Each ds As DataSet In dsSet
                    If Not ds.Tables(AcctTransLogDAL.Table_HEADER) Is Nothing Then
                        ProcessAccountingData(ds, Counter)
                        Counter += 1
                    End If
                Next
                dsSet = Nothing
                Counter = 1
            Else
                RECORDS_EXIST = False
            End If

            If SEND_SUCCESSFUL Then
                Return "0"
            Else
                SendFailures()
                Return Common.ErrorCodes.FTP_FAILURE
            End If

        Catch ex As StoredProcedureGeneratedException
            Throw ex
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        Catch ex As Exception
            Throw New ElitaPlusException("ACCOUNTING_ENGINE Error", "PROCESS REQUEST", ex)
        End Try

    End Function

    Public Function ResendFile(AcctTransmissionId As Guid) As String

        Try

            Dim _acctFile As New AccountingFiles
            Dim _acctTransmission As New AcctTransmission(AcctTransmissionId, False)

            'Check for balance.  If not balanced, Return error
            If Not _acctTransmission.DebitAmount.Equals(_acctTransmission.CreditAmount) Then
                Return Common.ErrorCodes.FILE_OUT_OF_BALANCE
            End If

            _acctFile.AcctTransmissionId = AcctTransmissionId
            _acctFile.FileName = _acctTransmission.FileName
            _acctFile.XMLData = _acctTransmission.FileText
            _acctFile.isValid = True
            _acctFile.isResend = True
            _acctFile.FileType = _acctTransmission.FileTypeFlag
            _acctFile.FileSubType = _acctTransmission.FileSubTypeFlag
            _acctFile.BatchNumber = _acctTransmission.BatchNumber
            _acctFile.JournalType = _acctTransmission.JournalType

            FileSet.Add(_acctFile)

            'Look for associated files (vendor records)
            Dim dv As AcctTransmission.AcctTransmissionSearchDV
            Dim _assocAcctTrans As AcctTransmission

            dv = AcctTransmission.GetAssociatedFailures(_acctTransmission.FileName, _acctTransmission.BatchNumber)

            For i As Integer = 0 To dv.Count - 1
                _assocAcctTrans = New AcctTransmission(GuidControl.ByteArrayToGuid(dv.Item(i)(AcctTransmission.AcctTransmissionSearchDV.COL_ACCT_TRANSMISSION_ID)), False)
                _acctFile = New AccountingFiles
                _acctFile.AcctTransmissionId = _assocAcctTrans.Id
                _acctFile.FileName = _assocAcctTrans.FileName
                _acctFile.XMLData = _assocAcctTrans.FileText
                _acctFile.FileType = _assocAcctTrans.FileTypeFlag
                _acctFile.FileSubType = _assocAcctTrans.FileSubTypeFlag
                _acctFile.BatchNumber = _assocAcctTrans.BatchNumber
                _acctFile.isValid = True
                _acctFile.isResend = True
                _acctFile.JournalType = _assocAcctTrans.JournalType
                FileSet.Add(_acctFile)
            Next

            'Send the files
            If _acctExtension = SMARTSTREAM_PREFIX Then
                SendFiles_SS()
            Else
                'Get Business Unit
                If _acctFile.FileSubType = FileSubType.CONTROL Then
                    BusinessUnit = _acctFile.FileName.Substring(_acctFile.FileName.IndexOf("_") + 1, (_acctFile.FileName.IndexOf("_", _acctFile.FileName.IndexOf("_") + 1) - _acctFile.FileName.IndexOf("_") - 1))
                Else
                    BusinessUnit = _acctFile.FileName.Substring(_acctFile.FileName.IndexOf("-", 3) + 1, (_acctFile.FileName.IndexOf("-", _acctFile.FileName.IndexOf("-", 3) + 1) - _acctFile.FileName.IndexOf("-", 3) - 1))
                End If

                SendFiles_FTP()
            End If

            If SEND_SUCCESSFUL Then
                Return "0"
            Else
                SendFailures()
                Return Common.ErrorCodes.FTP_FAILURE
            End If

        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("ACCOUNTING_ENGINE Error", "RESEND", ex)
        End Try

    End Function

    Public Function ReverseAccountingEntries(AcctTransmissionId As Guid, AcctEventCode As String, ReversalDate As Date, DateMovement As DateFilter, Optional ByVal Comment As String = "") As String

        Try

            Dim _acctFile As New AccountingFiles
            Dim _acctTransmission As New AcctTransmission(AcctTransmissionId, False)
            Dim ds As New DataSet
            Dim drFilter() As DataRow
            Dim acctPeriod As String = String.Empty

            'Set the reversal event type
            EVENT_TYPE = EventType.REVERSAL

            'Set the accounting company
            _Company = New Company(_acctTransmission.CompanyId)
            _AcctCompany = New AcctCompany(_Company.AcctCompanyId)
            _acctExtension = LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCT_SYSTEM, ElitaPlusIdentity.Current.ActiveUser.LanguageId, False), _AcctCompany.AcctSystemId)

            'Logic below for Felita only
            If _acctExtension = FELITA_PREFIX Then

                'fill the dataset with the old data
                Dim txtRdr As New System.IO.StringReader(_acctTransmission.FileText)
                ds.ReadXml(txtRdr)

                'Update Header Record batch number for the reversal (add description column if it doesn't already exist)
                If ds.Tables(AcctTransLogDAL.TABLE_POSTINGPARAMETERS).Columns("Description") Is Nothing Then
                    ds.Tables(AcctTransLogDAL.TABLE_POSTINGPARAMETERS).Columns.Add(New DataColumn("Description", GetType(String), TimeStamp.ToString))
                Else
                    ds.Tables(AcctTransLogDAL.TABLE_POSTINGPARAMETERS).Columns("Description").Expression = TimeStamp.ToString
                End If

                'Update the batch numbers
                If ds.Tables(AcctTransLogDAL.Table_LINEITEM_DETAIL).Columns("GeneralDescription14") Is Nothing Then
                    ds.Tables(AcctTransLogDAL.Table_LINEITEM_DETAIL).Columns.Add(New DataColumn("GeneralDescription14", GetType(String), _acctTransmission.FileName.Substring(_acctTransmission.FileName.LastIndexOf("-") + 1).Replace(".XML", "")))
                Else
                    ds.Tables(AcctTransLogDAL.Table_LINEITEM_DETAIL).Columns("GeneralDescription14").Expression = _acctTransmission.FileName.Substring(_acctTransmission.FileName.LastIndexOf("-") + 1).Replace(".XML", "")
                End If
                If ds.Tables(AcctTransLogDAL.Table_LINEITEM_DETAIL).Columns("GeneralDescription11") Is Nothing Then
                    ds.Tables(AcctTransLogDAL.Table_LINEITEM_DETAIL).Columns.Add(New DataColumn("GeneralDescription11", GetType(String), TimeStamp.ToString))
                Else
                    ds.Tables(AcctTransLogDAL.Table_LINEITEM_DETAIL).Columns("GeneralDescription11").Expression = TimeStamp.ToString
                End If

                'Apply the filters

                'If a specific event is included, remove all of the other events
                If Not AcctEventCode.Equals(String.Empty) Then
                    drFilter = ds.Tables(AcctTransLogDAL.Table_LINEITEM_DETAIL).Select(String.Format("NOT GeneralDescription24 = '{0}'", AcctEventCode))
                    Dim drParent As DataRow
                    For Each dr As DataRow In drFilter
                        drParent = dr.GetParentRow("Line_DetailLad")
                        ds.Tables(AcctTransLogDAL.Table_LINEITEM_DETAIL).Rows.Remove(dr)
                        ds.Tables(AcctTransLogDAL.Table_LINEITEM).Rows.Remove(drParent)
                    Next
                End If

                'If a date filter is needed, add a new date column, fill it, search, purge, and drop the new column
                If Not ReversalDate.Equals(Date.MinValue) Then

                    Dim dt As Date
                    Dim strDate As String
                    Dim drArrList As New ArrayList

                    For Each dr As DataRow In ds.Tables(AcctTransLogDAL.Table_LINEITEM).Rows
                        strDate = String.Format("{0}/{1}/{2}", dr("TransactionDate").ToString.Substring(2, 2), dr("TransactionDate").ToString.Substring(0, 2), dr("TransactionDate").ToString.Substring(4))
                        dt = Date.Parse(strDate)

                        Select Case DateMovement
                            Case DateFilter.GreaterThan
                                If dt <= ReversalDate Then
                                    drArrList.Add(dr)
                                    '  ds.Tables(AcctTransLogDAL.Table_LINEITEM).Rows.Remove(dr)
                                End If
                            Case DateFilter.LessThan
                                If dt >= ReversalDate Then
                                    drArrList.Add(dr)
                                    'ds.Tables(AcctTransLogDAL.Table_LINEITEM).Rows.Remove(dr)
                                End If
                            Case DateFilter.SpecificDate
                                If dt <> ReversalDate Then
                                    drArrList.Add(dr)
                                    'ds.Tables(AcctTransLogDAL.Table_LINEITEM).Rows.Remove(dr)
                                End If
                        End Select
                    Next

                    If drArrList.Count > 0 Then
                        drFilter = drArrList.ToArray(GetType(DataRow))
                        Dim drChild() As DataRow
                        For Each dr As DataRow In drFilter
                            drChild = dr.GetChildRows("Line_DetailLad")
                            If drChild.Count > 0 Then
                                For Each drC As DataRow In drChild
                                    ds.Tables(AcctTransLogDAL.Table_LINEITEM_DETAIL).Rows.Remove(drC)
                                Next
                            End If

                            ds.Tables(AcctTransLogDAL.Table_LINEITEM).Rows.Remove(dr)

                        Next
                    End If
                End If

                For Each dr As DataRow In ds.Tables(AcctTransLogDAL.Table_LINEITEM).Rows

                    'Reverse the debit / credit markers
                    If dr("DebitCredit") = "C" Then
                        dr("DebitCredit") = "D"
                    ElseIf dr("DebitCredit") = "D" Then
                        dr("DebitCredit") = "C"
                    End If

                    If Not Comment.Equals(String.Empty) Then
                        dr("Description") = Comment
                    End If

                    dr("TransactionDate") = Now.ToString("ddMMyyyy")

                    If acctPeriod.Equals(String.Empty) Then
                        acctPeriod = String.Format("0{0}", AccountingCloseInfo.GetAccountingCloseDate(_Company.Id, Now).ToString("MMyyyy"))
                    End If
                    dr("AccountingPeriod") = acctPeriod

                Next

                _acctFile.FileType = _acctTransmission.FileTypeFlag
                _acctFile.FileSubType = _acctTransmission.FileSubTypeFlag
                _acctFile.isResend = False
                _acctFile.isValid = True
                _acctFile.XMLData = ds.GetXml
                _acctFile.FileName = GetReversalFileName(_acctTransmission.FileName)
                _acctFile.BatchNumber = TimeStamp.ToString
                _acctFile.JournalType = GetJournalType(ds.GetXml, _acctTransmission.FileName, _acctTransmission.FileTypeFlag, _acctTransmission.FileSubTypeFlag)
                _acctFile.EventType = EventType.REVERSAL
                FileSet.Add(_acctFile)

                'Save the files to the DB And if successful, send them
                If SaveFiles() Then
                    AppConfig.DebugMessage.Trace("ACCOUNTING_ENGINE", "REVERSEACCOUNTING", "Files SAVED_ " + StartDate.Ticks.ToString)

                    SendFiles_FTP()
                    AppConfig.DebugMessage.Trace("ACCOUNTING_ENGINE", "REVERSEACCOUNTING", "Files Transmitted_ " + StartDate.Ticks.ToString)

                    'Once Files are sent, empty the fileset
                    FileSet.Clear()
                    FileSet = Nothing
                End If

            End If

            If SEND_SUCCESSFUL Then
                Return "0"
            Else
                SendFailures()
                Return Common.ErrorCodes.FTP_FAILURE
            End If

        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("ACCOUNTING_ENGINE Error", "FELITA", ex)
        End Try

    End Function

#End Region

#Region "Private Members"

    Private Function GetJournalType(inputXML As String, fileName As String, fileType As FelitaEngine.FileType, fileSubType As FelitaEngine.FileSubType) As String
        Select Case fileType
            Case FelitaEngine.FileType.Journal
                Select Case fileSubType
                    Case FelitaEngine.FileSubType.CONTROL
                        If InStr(1, fileName, "_") = 0 Then
                            Return "-CONTROL"
                        Else
                            Return String.Concat(fileName.Substring(InStr(3, fileName, "_"), InStr(InStr(3, fileName, "_") + 1, fileName, "_") - 1 - InStr(3, fileName, "_")), "-CONTROL")
                        End If
                    Case Else
                        If inputXML.IndexOf("<JournalType>") > 0 Then
                            Return inputXML.Substring(inputXML.IndexOf("<JournalType>") + Len("<JournalType>"), inputXML.IndexOf("</JournalType>") - (inputXML.IndexOf("<JournalType>") + Len("<JournalType>")))
                        End If
                End Select
                Return String.Empty
            Case Else
                Return "Vendor"
        End Select
    End Function

    Private Sub MapDataSet(ds As FelitaEngineDs)

        Dim schema As String = ds.GetXmlSchema

        Dim t As Integer
        Dim i As Integer

        For t = 0 To ds.Tables.Count - 1
            For i = 0 To ds.Tables(t).Columns.Count - 1
                ds.Tables(t).Columns(i).ColumnName = ds.Tables(t).Columns(i).ColumnName.ToUpper
            Next
        Next

        Dataset = New DataSet
        Dataset.ReadXmlSchema(XMLHelper.GetXMLStream(schema))

    End Sub

    'Initialization code for new objects
    Private Sub Initialize()
    End Sub

    'Shadow the checkdeleted method so we don't validate like the DB objects
    Protected Shadows Sub CheckDeleted()
    End Sub

    Private Sub Load(ds As FelitaEngineDs)
        Try
            Initialize()
            Dim newRow As DataRow = Dataset.Tables(TABLE_NAME).NewRow
            Row = newRow
            PopulateBOFromWebService(ds)
            Dataset.Tables(0).Rows.Add(newRow)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("Felita Engine Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ds As FelitaEngineDs)
        Try
            If ds.FelitaEngine.Count = 0 Then Exit Sub
            With ds.FelitaEngine.Item(0)
                CompanyId = .CompanyId
                If Not IsNothing(.AccountingEventId) Then AccountingEventId = .AccountingEventId
                If Not IsNothing(.VendorFiles) Then
                    If .VendorFiles = "0" Then
                        VendorFiles = Boolean.Parse(False)
                    Else
                        VendorFiles = Boolean.Parse(True)
                    End If
                End If
            End With

        Catch ex As Exception
            Throw New ElitaPlusException("Felita Invalid Parameters Error", Common.ErrorCodes.BO_INVALID_DATA, ex)
        End Try
    End Sub

    Private Sub SetCompany(CompanyCode As String)

        Dim dv As New Company.CompanySearchDV

        dv = Company.getList("", CompanyCode)
        If dv.Count = 1 Then
            _Company = New Company(GuidControl.ByteArrayToGuid(CType(dv(0)(dv.COL_COMPANY_ID), Byte())))
            If Not _Company Is Nothing Then Exit Sub
        End If

        'If it gets here, we have not gotten a company from the code and need to throw an error
        Throw New BOValidationException(Common.ErrorCodes.INVALID_COMPANYID_REQUIRED)

    End Sub

    'This is the routine that builds the actual files and sends the data
    Private Sub ProcessAccountingData(AccountingData_Dataset As DataSet, Optional ByVal Counter As Integer = 1)

        If Not AccountingData_Dataset.Tables(AcctTransLogDAL.Table_HEADER) Is Nothing _
            AndAlso AccountingData_Dataset.Tables(AcctTransLogDAL.Table_HEADER).Rows.Count > 0 Then
            BusinessUnit = AccountingData_Dataset.Tables(AcctTransLogDAL.Table_HEADER).Rows(0)(AcctTransLogDAL.COL_TABLE_HEADER_BUSINESS_UNIT).ToString()
        Else
            Exit Sub
        End If

        beginDate = Today
        endDate = Today

        If AccountingData_Dataset.Tables.Contains(AcctTransLogDAL.Table_LINEITEM) Then
            If AccountingData_Dataset.Tables(AcctTransLogDAL.Table_LINEITEM).Rows.Count > 0 Then
                'ALR - Ticket 1274262 - Don't spread the dates.  Should be today only 
                'beginDate = Date.Parse(AccountingData_Dataset.Tables(AcctTransLogDAL.Table_LINEITEM).Compute("MIN(" & AcctTransLogDAL.COL_NAME_CREATED_DATE & ")", ""))
                'endDate = Date.Parse(AccountingData_Dataset.Tables(AcctTransLogDAL.Table_LINEITEM).Compute("MAX(" & AcctTransLogDAL.COL_NAME_CREATED_DATE & ")", ""))
            Else
                If AccountingData_Dataset.Relations(AcctTransLogDAL.REL_JOURNAL_TYPE) IsNot Nothing Then
                    AccountingData_Dataset.Relations.Remove(AcctTransLogDAL.REL_JOURNAL_TYPE)
                End If
                If AccountingData_Dataset.Tables(AcctTransLogDAL.Table_LINEITEM).Constraints(AcctTransLogDAL.REL_JOURNAL_TYPE) IsNot Nothing Then
                    AccountingData_Dataset.Tables(AcctTransLogDAL.Table_LINEITEM).Constraints.Remove(AcctTransLogDAL.REL_JOURNAL_TYPE)
                End If
                AccountingData_Dataset.Tables.Remove(AcctTransLogDAL.Table_LINEITEM)
            End If
        End If

        'Check if we are building smartstream or Felita Files
        If _acctExtension = SMARTSTREAM_PREFIX Then

            BuildFile(FileType.Journal, AccountingData_Dataset, BusinessUnit, beginDate, endDate)

            'Now that we are done with the accounting dataset, we clear the variable and call GC in the finallizer
            If Not xmlDoc Is Nothing Then
                xmlDoc = Nothing
            End If

        Else

            'Determine if we are building vendor files or journal files.
            If VendorFiles AndAlso Not AccountingData_Dataset.Tables(AcctTransLogDAL.Table_VENDOR) Is Nothing _
               AndAlso AccountingData_Dataset.Tables(AcctTransLogDAL.Table_VENDOR).Rows.Count > 0 Then
                BuildFile(FileType.Account, AccountingData_Dataset, BusinessUnit, beginDate, endDate)
                BuildFile(FileType.Supplier, AccountingData_Dataset, BusinessUnit, beginDate, endDate)
                BuildFile(FileType.Client, AccountingData_Dataset, BusinessUnit, beginDate, endDate)
                BuildFile(FileType.BankDetails, AccountingData_Dataset, BusinessUnit, beginDate, endDate)
                BuildFile(FileType.Address, AccountingData_Dataset, BusinessUnit, beginDate, endDate)
            End If

            If AcctTransLog.isDSValid(AccountingData_Dataset, False) Then
                BuildFile(FileType.Journal, AccountingData_Dataset, BusinessUnit, beginDate, endDate)
            End If

            'Now that we are done with the accounting dataset, we clear the variable and call GC in the finallizer
            If Not xmlDoc Is Nothing Then
                xmlDoc = Nothing
            End If

        End If

        AppConfig.DebugMessage.Trace("ACCOUNTING_ENGINE", "PROCESS REQUEST", "Step 3/6 Files Created_ " + StartDate.Ticks.ToString)

        'Save the files to the DB And if successful, send them
        If SaveFiles() Then

            AppConfig.DebugMessage.Trace("ACCOUNTING_ENGINE", "PROCESS REQUEST", "Step 4/6 Files Saved_ " + StartDate.Ticks.ToString)

            'Once files are saved, purge the translog of the items in the dataset
            AcctTransLog.PurgeTransLog(AccountingData_Dataset, Boolean.Parse(Counter = 1))
            AppConfig.DebugMessage.Trace("ACCOUNTING_ENGINE", "PROCESS REQUEST", "Step 5/6 Log Purged_ " + StartDate.Ticks.ToString)

            'Determine whether we are sending the files or Holding...
            SendFiles()

            AppConfig.DebugMessage.Trace("ACCOUNTING_ENGINE", "PROCESS REQUEST", "Step 6/6 Files Transmitted_ " + StartDate.Ticks.ToString)

            'Once Files are sent, empty the fileset
            FileSet.Clear()
            FileSet = Nothing
        End If

        System.GC.Collect()
        System.GC.WaitForPendingFinalizers()

    End Sub

    Private Function BuildFile(_FileType As FileType, AccountingData_DataSet As DataSet, BusinessUnit As String, beginDate As Date, endDate As Date) As Boolean

        'If filetype = journal, loop through the dataset and grab all the lineitem tables in case there are failures
        If _FileType = FileType.Journal Then


            For Each dt As DataTable In AccountingData_DataSet.Tables

                'Add the seq column (smartstream) into the datatable so that it can be used in the journal file
                If dt.TableName.Contains(AcctTransLogDAL.Table_LINEITEM) OrElse dt.TableName.Contains(AcctTransLogDAL.Table_AP_LINEITEM) Then
                    If Not dt.Columns(DATA_COL_NAME_SEQUENCE) Is Nothing Then
                        dt.Columns.Remove(DATA_COL_NAME_SEQUENCE)
                    End If
                    Dim rnd As New Random(Now.Millisecond)
                    dt.Columns.Add(New DataColumn(DATA_COL_NAME_SEQUENCE, GetType(String), "'" + (((Now.Minute + 1) * Now.Hour) + Now.Second + Now.Millisecond + rnd.Next(1, 4000)).ToString + "'"))
                End If

                'ALR - 03/23/2010 - Check to be sure there are lines before creating file.
                If dt.TableName.Contains(AcctTransLogDAL.Table_LINEITEM) Then

                    'If we are Felita and there is more than one journal type, then copy the DS and create multiple files
                    If Not _acctExtension = SMARTSTREAM_PREFIX AndAlso AccountingData_DataSet.Tables(AcctTransLogDAL.TABLE_POSTINGPARAMETERS) IsNot Nothing _
                      AndAlso AccountingData_DataSet.Tables(AcctTransLogDAL.TABLE_POSTINGPARAMETERS).Rows.Count > 1 Then
                        Dim dsTemp As DataSet
                        Dim dtPosting As DataTable = AccountingData_DataSet.Tables(AcctTransLogDAL.TABLE_POSTINGPARAMETERS).Clone
                        Dim dtLines As DataTable = AccountingData_DataSet.Tables(AcctTransLogDAL.Table_LINEITEM).Clone
                        Dim drArr() As DataRow

                        For Each dr As DataRow In AccountingData_DataSet.Tables(AcctTransLogDAL.TABLE_POSTINGPARAMETERS).Rows

                            'Remove the xmldoc if it exists to refresh the data
                            If xmlDoc IsNot Nothing Then xmlDoc = Nothing

                            dsTemp = New DataSet
                            dsTemp.DataSetName = AcctTransLogDAL.DatasetName

                            If dtPosting.Rows.Count > 0 Then dtPosting.Rows.Clear()
                            If dtLines.Rows.Count > 0 Then dtLines.Rows.Clear()

                            If dtPosting.DataSet IsNot Nothing Then
                                dtPosting.DataSet.Tables.Remove(dtPosting)
                                dtLines.DataSet.Tables.Remove(dtLines)
                            End If

                            dsTemp.Tables.Add(AccountingData_DataSet.Tables(AcctTransLogDAL.Table_HEADER).Copy)

                            dtPosting.ImportRow(dr)
                            drArr = dr.GetChildRows(AcctTransLogDAL.REL_JOURNAL_TYPE)
                            For Each drLine As DataRow In drArr
                                dtLines.ImportRow(drLine)
                            Next

                            ''ALR - Moved Batch Number logic to it's own routine
                            ''Add the timestamp column (Felita) into the datatable so that it can be used in the journal file
                            'If Not dtLines.Columns(DATA_COL_NAME_TIMESTAMP) Is Nothing Then
                            '    dtLines.Columns.Remove(DATA_COL_NAME_TIMESTAMP)
                            'End If
                            'dtLines.Columns.Add(New DataColumn(DATA_COL_NAME_TIMESTAMP, GetType(String), "'" + TimeStamp.ToString + "'"))

                            dsTemp.Tables.Add(dtPosting.Copy)
                            dsTemp.Tables.Add(dtLines.Copy)

                            If dsTemp.Tables.Contains(AcctTransLogDAL.TABLE_POSTINGPARAMETERS) Then
                                'ALR - Moved Batch Number logic to it's own routine
                                ''''dsTemp.Tables(AcctTransLogDAL.TABLE_POSTINGPARAMETERS).Columns(DATA_COL_NAME_DESCRIPTION).Expression = "'" + TimeStamp.ToString + "'"

                                'Add a Journal Header id column to the dataset
                                If Not dsTemp.Tables(AcctTransLogDAL.TABLE_POSTINGPARAMETERS).Columns(DATA_COL_NAME_JOURNAL_HEADER_ID) Is Nothing Then
                                    dsTemp.Tables(AcctTransLogDAL.TABLE_POSTINGPARAMETERS).Columns.Remove(DATA_COL_NAME_JOURNAL_HEADER_ID)
                                End If
                                dsTemp.Tables(AcctTransLogDAL.TABLE_POSTINGPARAMETERS).Columns.Add(New DataColumn(DATA_COL_NAME_JOURNAL_HEADER_ID, GetType(String), "'" + Guid.NewGuid.ToString + "'"))
                            End If

                            'Set the Event Type based on the event in the current table
                            If dsTemp.Tables(dt.TableName) IsNot Nothing AndAlso dsTemp.Tables(dt.TableName).Columns(AcctTransLog.COL_EVENT_TYPE) IsNot Nothing AndAlso dsTemp.Tables(dt.TableName).Rows.Count > 0 Then
                                EVENT_TYPE = [Enum].Parse(GetType(EventType), dsTemp.Tables(dt.TableName).Rows(0)(AcctTransLog.COL_EVENT_TYPE).ToString)
                            End If

                            BuildFileBody(_FileType, FileSubType.GL, dsTemp, BusinessUnit, beginDate, endDate, dt.TableName)

                            'Set a new batch number
                            TimeStamp = Now.Ticks

                        Next

                    Else
                        If AccountingData_DataSet.Tables.Contains(AcctTransLogDAL.TABLE_POSTINGPARAMETERS) Then
                            'ALR - Moved Batch Number logic to it's own routine
                            '    AccountingData_DataSet.Tables(AcctTransLogDAL.TABLE_POSTINGPARAMETERS).Columns(DATA_COL_NAME_DESCRIPTION).Expression = "'" + TimeStamp.ToString + "'"
                            'Add a Journal Header id column to the dataset
                            If Not AccountingData_DataSet.Tables(AcctTransLogDAL.TABLE_POSTINGPARAMETERS).Columns(DATA_COL_NAME_JOURNAL_HEADER_ID) Is Nothing Then
                                AccountingData_DataSet.Tables(AcctTransLogDAL.TABLE_POSTINGPARAMETERS).Columns.Remove(DATA_COL_NAME_JOURNAL_HEADER_ID)
                            End If
                            AccountingData_DataSet.Tables(AcctTransLogDAL.TABLE_POSTINGPARAMETERS).Columns.Add(New DataColumn(DATA_COL_NAME_JOURNAL_HEADER_ID, GetType(String), "'" + Guid.NewGuid.ToString + "'"))
                        End If

                        'Set the Event Type based on the event in the current table
                        If dt.Columns(AcctTransLog.COL_EVENT_TYPE) IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                            EVENT_TYPE = [Enum].Parse(GetType(EventType), dt.Rows(0)(AcctTransLog.COL_EVENT_TYPE).ToString)
                        End If

                        BuildFileBody(_FileType, FileSubType.GL, AccountingData_DataSet, BusinessUnit, beginDate, endDate, dt.TableName)

                    End If

                End If

                If dt.TableName.Contains(AcctTransLogDAL.Table_AP_LINEITEM) Then

                    'This call is for the SmartStream vendor load file.  Only build if the vendor table exists in the file.
                    If Not AccountingData_DataSet.Tables(AcctTransLogDAL.Table_AP_VENDORS) Is Nothing Then
                        AccountingData_DataSet.Tables(AcctTransLogDAL.Table_AP_VENDORS).DefaultView.RowFilter = "PAYMENT_TO_CUSTOMER = 'Y'"
                        If AccountingData_DataSet.Tables(AcctTransLogDAL.Table_AP_VENDORS).DefaultView.Count > 0 Then
                            BuildFileBody(_FileType, FileSubType.AP2, AccountingData_DataSet, BusinessUnit, beginDate, endDate, dt.TableName, "AP2")
                        End If
                    End If

                    If Not AccountingData_DataSet.Tables(AcctTransLogDAL.Table_AP_LINEITEM) Is Nothing AndAlso AccountingData_DataSet.Tables(AcctTransLogDAL.Table_AP_LINEITEM).Rows.Count > 0 Then
                        'SmartStream AP Invoice File
                        BuildFileBody(_FileType, FileSubType.AP, AccountingData_DataSet, BusinessUnit, beginDate, endDate, dt.TableName, "AP")
                    End If

                End If

            Next
        Else
            Return BuildFileBody(_FileType, FileSubType.VEND, AccountingData_DataSet, BusinessUnit, beginDate, endDate)
        End If

    End Function

    Private Function BuildFileBody(_FileType As FileType,
                                   _FileSubType As FileSubType,
                                   AccountingData_DataSet As DataSet,
                                   BusinessUnit As String,
                                   beginDate As Date,
                                   endDate As Date,
                                   Optional ByVal TableName As String = "",
                                   Optional ByVal fileExtension As String = "") As Boolean

        Dim str As Stream
        Dim xmlr As XmlReader
        Dim xlTransform As New Xml.Xsl.XslCompiledTransform
        Dim buffer As MemoryStream
        Dim sw As StreamWriter
        ' Dim xlr As XmlReader
        'Dim xmlDoc As XmlDocument
        Dim chrs() As Byte
        Dim XMLData As String
        Dim _AcctFile As AccountingFiles
        Dim txtRead As TextReader
        Dim xmlTempDoc As XmlDocument
        Dim _JournalType As String = ""

        Try

            str = GetResource(System.Enum.GetName(GetType(FileType), _FileType) + If(_FileType = FileType.Journal, String.Format("_{0}", _acctExtension), String.Empty).ToString + If(fileExtension.Equals(String.Empty), String.Empty, String.Format("_{0}", fileExtension)).ToString + ".xslt")

            xmlr = XmlReader.Create(str)

            'Create the transform object to process the stylesheet and data
            xlTransform.Load(xmlr)

            'Create the xmlwriter to accept the output of the transformation
            buffer = New MemoryStream

            sw = New StreamWriter(buffer)

            'Create the Reader for the data after setting the Dataset's namespace
            If xmlDoc Is Nothing Then
                AccountingData_DataSet.Namespace = "http://tempuri.org/AssurantElitaFelita.xsd"
                txtRead = New StringReader(AccountingData_DataSet.GetXml)
                'xlr = XmlReader.Create(txtRead)
                xmlDoc = New XmlDocument
                xmlDoc.Load(txtRead)
            End If

            'if Journal File, we want to find the correct table to put use for our xmldoc.  
            'We will create a temp xmldoc to use for the process and process only the correct table.
            xmlTempDoc = xmlDoc.Clone
            If _FileType = FileType.Journal Then
                Dim lineNodes As XmlNodeList = xmlTempDoc.DocumentElement.ChildNodes
                Dim i, j As Integer
                Dim lineNode As XmlNode

                j = lineNodes.Count - 1

                For i = j To 0 Step -1
                    lineNode = lineNodes(i)
                    If lineNode.Name.Contains(AcctTransLogDAL.Table_LINEITEM) _
                     AndAlso Not lineNode.Name.Equals(TableName) Then
                        xmlTempDoc.DocumentElement.RemoveChild(lineNode)
                    End If
                Next

                'Now, there should be only 1.  If it is not called "Line", we need to rename it.
                lineNodes = xmlTempDoc.DocumentElement.ChildNodes

                Dim xmlNewNode As XmlNode
                For i = 0 To lineNodes.Count - 1
                    If lineNodes(i).Name.Contains(AcctTransLogDAL.Table_LINEITEM) AndAlso Not lineNodes(i).Name.Equals(AcctTransLogDAL.Table_LINEITEM) AndAlso Not TableName.Equals(AcctTransLogDAL.Table_LINEITEM) Then
                        xmlNewNode = xmlTempDoc.CreateNode(lineNodes(i).NodeType, AcctTransLogDAL.Table_LINEITEM, lineNodes(i).NamespaceURI)
                        xmlNewNode.InnerXml = lineNodes(i).InnerXml.Replace(lineNodes.Item(i).Name, AcctTransLogDAL.Table_LINEITEM)
                        xmlTempDoc.DocumentElement.AppendChild(xmlNewNode)
                    End If
                Next

            End If

            'Set the batch Number in the document
            Dim xnBatch As XmlElement
            xnBatch = xmlTempDoc.CreateElement(DATA_COL_NAME_TIMESTAMP, xmlTempDoc.FirstChild.NamespaceURI)
            xnBatch.InnerText = TimeStamp.ToString
            For Each xn As XmlNode In xmlTempDoc.FirstChild.ChildNodes
                If xn.Name.Contains(AcctTransLogDAL.Table_LINEITEM) Or xn.Name.Contains(AcctTransLogDAL.TABLE_POSTINGPARAMETERS) Then
                    xn.AppendChild(xnBatch.Clone)
                End If
            Next

            'Get The journalType out of the document
            For Each xn As XmlNode In xmlTempDoc.FirstChild.ChildNodes
                If xn.Name.Contains(AcctTransLogDAL.TABLE_POSTINGPARAMETERS) Then
                    If Not xn.Item(AcctTransLogDAL.JOURNAL_COL_NAME_JOURNAL_TYPE) Is Nothing Then
                        _JournalType = xn.Item(AcctTransLogDAL.JOURNAL_COL_NAME_JOURNAL_TYPE).InnerText
                    End If
                End If
            Next


            xlTransform.Transform(xmlTempDoc, Nothing, sw)

            'Create the character array to write the buffer to (process of creating the string)
            chrs = buffer.ToArray

            XMLData = System.Text.Encoding.UTF8.GetString(chrs)

            _AcctFile.FileType = _FileType
            _AcctFile.FileSubType = _FileSubType
            _AcctFile.XMLData = XMLData
            _AcctFile.FileName = GetFileName(_FileType, _FileSubType, BusinessUnit, beginDate, endDate, TableName, _acctExtension, _JournalType)
            _AcctFile.isResend = False
            _AcctFile.BatchNumber = TimeStamp.ToString
            _AcctFile.JournalType = GetJournalType(XMLData, _AcctFile.FileName, _FileType, _FileSubType)
            _AcctFile.EventType = If(_FileSubType = FileSubType.VEND, EventType.VEND, EVENT_TYPE)

            'Check if the fileset is available.  if not, initialize it.
            If FileSet Is Nothing Then FileSet = New ArrayList
            FileSet.Add(_AcctFile)

            Return True
        Catch ex As Exception
            Throw New Exception("ACCOUNTING: ERROR BUILDING FILE: " & System.Enum.GetName(GetType(FileType), _FileType), ex)
        Finally
            'Clean up.  Dispose what you can, set everything else to nothing to clear
            If Not str Is Nothing Then str.Dispose()
            If Not buffer Is Nothing Then buffer.Dispose()
            If Not sw Is Nothing Then sw.Dispose()
            If Not txtRead Is Nothing Then txtRead.Dispose()
            xmlr = Nothing
            xlTransform = Nothing
            chrs = Nothing
            XMLData = Nothing
            _AcctFile = Nothing
            System.GC.Collect()
            System.GC.WaitForPendingFinalizers()

        End Try
    End Function

    Private Function BuildFile(_FileType As FileType, BusinessUnit As String, MinDate As Date, MaxDate As Date, batchNumber As BatchId) As Boolean

        Try

            If _FileType = FileType.trigger Then
                Dim _AcctFile As New AccountingFiles
                _AcctFile.FileType = _FileType
                _AcctFile.XMLData = ""
                _AcctFile.FileName = GetFileName(_FileType, BusinessUnit, MinDate, MaxDate, True, batchNumber)
                _AcctFile.isResend = False
                _AcctFile.BatchNumber = batchNumber.Batch_Number

                If FileSet Is Nothing Then FileSet = New ArrayList
                FileSet.Add(_AcctFile)
            Else
                Return False
            End If

            Return True
        Catch ex As Exception
            Throw New Exception("ACCOUNTING: ERROR BUILDING FILE: " & System.Enum.GetName(GetType(FileType), _FileType), ex)
        End Try
    End Function

    'Builds trigger file based on Journal file Name
    Private Function BuildFile(FileName As String, BatchNumber As String) As Boolean

        Try

            Dim _AcctFile As New AccountingFiles
            _AcctFile.FileType = FileType.trigger
            _AcctFile.XMLData = ""
            _AcctFile.isResend = True
            _AcctFile.BatchNumber = BatchNumber

            Try
                _AcctFile.FileName = FileName.Replace(FileName.Substring(FileName.IndexOf("-") + 1, FileName.IndexOf("-", 3) - 2), "trigger")
            Catch ex As Exception
                Return True
            End Try

            If FileSet Is Nothing Then FileSet = New ArrayList
            FileSet.Add(_AcctFile)

            Return True
        Catch ex As Exception
            Throw New Exception("ACCOUNTING: ERROR BUILDING FILE: " & System.Enum.GetName(GetType(FileType), FileType.trigger), ex)
        End Try
    End Function


    Private Function BuildFile(_FileType As FileType, BusinessUnit As String) As Boolean

        Try

            If _FileType = FileType.trigger Then
                'Create the xmlwriter to accept the output of the transformation
                Dim _AcctFile As New AccountingFiles
                _AcctFile.FileType = _FileType
                _AcctFile.XMLData = ""
                _AcctFile.FileName = GetFileName(_FileType, BusinessUnit)
                _AcctFile.isResend = False
                _AcctFile.BatchNumber = TimeStamp.ToString

                If FileSet Is Nothing Then FileSet = New ArrayList
                FileSet.Add(_AcctFile)
            Else
                Return False
            End If

            Return True
        Catch ex As Exception
            Throw New Exception("ACCOUNTING: ERROR BUILDING FILE: " & System.Enum.GetName(GetType(FileType), _FileType), ex)
        End Try
    End Function

    Private Function SaveFiles() As Boolean

        Dim CurrentFile As AccountingFiles
        Dim _acctTransmit As AcctTransmission
        Dim i As Integer = 0
        Dim _newArrList As New ArrayList
        Dim bEntriesExist As Boolean = True

        Try

            For Each CurrentFile In FileSet

                bEntriesExist = True

                If Not CurrentFile.FileType = FileType.trigger Then
                    'Save it to the log table
                    _acctTransmit = New AcctTransmission
                    _acctTransmit.FileName = CurrentFile.FileName
                    _acctTransmit.FileText = CurrentFile.XMLData
                    _acctTransmit.CompanyId = _Company.Id
                    _acctTransmit.TransmissionCount = 0
                    _acctTransmit.FileTypeFlag = CurrentFile.FileType
                    _acctTransmit.FileSubTypeFlag = CurrentFile.FileSubType
                    _acctTransmit.BatchNumber = CurrentFile.BatchNumber
                    _acctTransmit.JournalType = CurrentFile.JournalType
                    _acctTransmit.StatusId = LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCOUNTING_TRANS_STATUS, ElitaPlusIdentity.Current.ActiveUser.LanguageId), AcctTransmission.STATUS_CODE_PROCESSING)
                    If CurrentFile.FileType = FileType.Journal Then _acctTransmit.ValidateBalance()

                    If (Not CurrentFile.FileType = FileType.Journal) OrElse
                        _acctTransmit.NumTransactionsSent > 0 OrElse
                        CurrentFile.FileSubType = FileSubType.AP2 OrElse
                        CurrentFile.FileSubType = FileSubType.CONTROL Then

                        _acctTransmit.Save()
                        'Put the Id in the arraylist
                        CurrentFile.AcctTransmissionId = _acctTransmit.Id
                    Else
                        bEntriesExist = False
                    End If

                End If

                If bEntriesExist Then _newArrList.Add(CurrentFile)

            Next

            FileSet.Clear()
            FileSet = _newArrList.Clone
            Return True
        Catch ex As Exception
            Throw New ElitaPlusException(Common.ErrorCodes.DB_WRITE_ERROR, "", ex)
        End Try

    End Function

    Private Function SendFiles()

        Dim HoldType As String = LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCOUNTING_PROCESS_METHOD, ElitaPlusIdentity.Current.ActiveUser.LanguageId, False), _AcctCompany.ProcessMethodId)
        Dim _newList As New ArrayList
        Dim _acctTransmission As AcctTransmission
        Dim _boolChanges As Boolean = False

        Select Case HoldType

            Case CType(ProcessMethod.PROCESS_IMMEDIATELY, String)
                Exit Select
            Case CType(ProcessMethod.HOLD_ALL, String)
                For i As Integer = 0 To FileSet.Count - 1
                    If CType(FileSet(i), AccountingFiles).FileSubType = FileSubType.AP Or CType(FileSet(i), AccountingFiles).FileSubType = FileSubType.GL Then
                        _acctTransmission = New AcctTransmission(CType(FileSet(i), AccountingFiles).AcctTransmissionId, True)
                        _acctTransmission.StatusId = LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCOUNTING_TRANS_STATUS, ElitaPlusIdentity.Current.ActiveUser.LanguageId), AcctTransmission.STATUS_CODE_PENDING)
                        _acctTransmission.Save()
                        _boolChanges = True
                    Else
                        _newList.Add(FileSet(i))
                    End If
                Next
            Case CType(ProcessMethod.HOLD_AP, String)
                For i As Integer = 0 To FileSet.Count - 1
                    If (CType(FileSet(i), AccountingFiles).FileSubType = FileSubType.AP) OrElse (CType(FileSet(i), AccountingFiles).FileSubType = FileSubType.GL And _acctExtension = FELITA_PREFIX) Then
                        _acctTransmission = New AcctTransmission(CType(FileSet(i), AccountingFiles).AcctTransmissionId, True)
                        _acctTransmission.StatusId = LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCOUNTING_TRANS_STATUS, ElitaPlusIdentity.Current.ActiveUser.LanguageId), AcctTransmission.STATUS_CODE_PENDING)
                        _acctTransmission.Save()
                        _boolChanges = True
                    Else
                        _newList.Add(FileSet(i))
                    End If
                Next
            Case CType(ProcessMethod.HOLD_GL, String)
                For i As Integer = 0 To FileSet.Count - 1
                    If (CType(FileSet(i), AccountingFiles).FileSubType = FileSubType.GL) OrElse (CType(FileSet(i), AccountingFiles).FileSubType = FileSubType.AP And _acctExtension = FELITA_PREFIX) Then
                        _acctTransmission = New AcctTransmission(CType(FileSet(i), AccountingFiles).AcctTransmissionId, True)
                        _acctTransmission.StatusId = LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCOUNTING_TRANS_STATUS, ElitaPlusIdentity.Current.ActiveUser.LanguageId), AcctTransmission.STATUS_CODE_PENDING)
                        _acctTransmission.Save()
                        _boolChanges = True
                    Else
                        _newList.Add(FileSet(i))
                    End If
                Next
        End Select

        If Not _newList.Count = FileSet.Count AndAlso _boolChanges Then
            FileSet.Clear()
            FileSet.AddRange(_newList)
        End If

        If FileSet.Count > 0 Then
            If _acctExtension = SMARTSTREAM_PREFIX Then
                SendFiles_SS()
            Else
                SendFiles_FTP()
            End If
        End If

    End Function

    Private Function SendFiles_FTP()

        Dim CurrentFile As AccountingFiles
        Dim _file As AccountingFiles
        Dim _ftp As New aFtp
        Dim _ftpControl As New aFtp
        Dim _acctTransmit As AcctTransmission
        Dim buffer As MemoryStream
        Dim bufferForRecon As MemoryStream
        Dim xmlData() As Byte
        Dim xmlDataForRecon() As Byte
        Dim isBalanced As Boolean = True
        Dim fs As FileStream
        Dim batchNumbers As New List(Of BatchId)
        Dim CurrentBatch As BatchId
        Dim isNewBatchNumber As Boolean
        Dim containsVendors As Boolean = False
        Dim fileName As String = ""

        Try

            'Set FTP Parameters
            If _acctExtension = FELITA_PREFIX Then
                _ftp.RemoteHostFTPServer = ElitaPlusIdentity.Current.FelitaFtpHostname
                _ftp.RemoteUser = AppConfig.Felita.UserId
                _ftp.RemotePassword = AppConfig.Felita.Password

                _ftpControl.RemoteHostFTPServer = ElitaPlusIdentity.Current.AcctBalanceHostName
                _ftpControl.RemoteUser = AppConfig.Felita.ReconUserId
                _ftpControl.RemotePassword = AppConfig.Felita.ReconPassword
                _ftpControl.RemotePort = 21

            Else
                _ftp.RemoteHostFTPServer = AppConfig.SmartStream.FTPHostName
                _ftp.RemoteUser = AppConfig.SmartStream.FTPUserId
                _ftp.RemotePassword = AppConfig.SmartStream.FTPPassword
            End If

            If String.IsNullOrEmpty(_ftp.RemoteHostFTPServer) Then
                AppConfig.Log(CType(New ElitaPlusException("FTP_FAILURE - No FTP Server Set", ""), Exception))
                Exit Function
            End If

            _ftp.RemotePath = _AcctCompany.FTPDirectory
            _ftpControl.RemotePath = _AcctCompany.BalanceDirectory
            _ftp.RemotePort = 21

            'ALR - DEF-958 - Check if the set contains vendor files and set the flag to know whether to send trigger later.
            For Each acctFile As AccountingFiles In FileSet
                If acctFile.FileType = FileType.Account OrElse
                        acctFile.FileType = FileType.Address OrElse
                        acctFile.FileType = FileType.BankDetails OrElse
                        acctFile.FileType = FileType.Supplier OrElse
                        acctFile.FileType = FileType.Client Then
                    containsVendors = True
                    Exit For
                End If
            Next

            'For Each CurrentFile In FileSet
            If Not FileSet Is Nothing Then

                For i As Integer = 0 To FileSet.Count - 1

                    If CType(FileSet(i), AccountingFiles).FileSubType = FileSubType.AP1 Or
                        CType(FileSet(i), AccountingFiles).FileSubType = FileSubType.AP2 Or
                        _acctExtension.Equals(FELITA_PREFIX) Then

                        'Get an instance of the file
                        If Not FileSet(i).FileType = FileType.trigger Then
                            _acctTransmit = New AcctTransmission(CType(FileSet(i), AccountingFiles).AcctTransmissionId, True)
                            _acctTransmit.TransmissionDate = StartDate
                            isBalanced = _acctTransmit.DebitAmount.Equals(_acctTransmit.CreditAmount)
                        End If

                        Try
                            'Check the balances.  If they don't match, exit the loop and don't send
                            If (FileSet.Count >= 1) _
                              OrElse (FileSet(i).FileType <> FileType.Journal) Then

                                'ALR - DEF-958 - remove hard count of files and just determine whether vendor files were included.
                                'If only 2 files (journal & trigger) and not balanced, don't send trigger.
                                If ((Not containsVendors) AndAlso (Not isBalanced) AndAlso (FileSet(i).FileType = FileType.trigger)) _
                                    Or ((Not isBalanced) And (FileSet(i).FileType = FileType.Journal)) Then
                                    Exit Try
                                End If

                                buffer = New MemoryStream
                                xmlData = System.Text.Encoding.UTF8.GetBytes(CType(FileSet(i), AccountingFiles).XMLData)
                                buffer.Write(xmlData, 0, xmlData.Length)

                                'Upload the file.  
                                If CType(FileSet(i), AccountingFiles).FileSubType = FileSubType.CONTROL Then

                                    Try
                                        _ftpControl.UploadFile(buffer, FileSet(i).FileName, False)
                                    Catch ex As Exception
                                        'If the upload failed, wait 5 seconds and try again to see if it is a connection problem
                                        Threading.Thread.Sleep(5000)
                                        _ftpControl.UploadFile(buffer, FileSet(i).FileName, False)
                                    End Try

                                Else
                                    'Check to see if we should be FTPing or just sending the file via UNC
                                    If _AcctCompany.FTPDirectory.Contains("\") Then
                                        Dim _fil As New FileInfo(String.Format("\\{0}\{1}\{2}", _ftp.RemoteHostFTPServer, _AcctCompany.FTPDirectory, CType(FileSet(i), AccountingFiles).FileName))

                                        If _fil.Exists Then
                                            _fil.Delete()
                                        End If

                                        fs = _fil.Create()
                                        fs.Write(xmlData, 0, xmlData.Length)
                                        fs.Close()

                                    Else
                                        Try
                                            _ftp.UploadFile(buffer, FileSet(i).FileName, False)
                                        Catch ex As Exception
                                            'If the upload failed, wait 5 seconds and try again to see if it is a connection problem
                                            Threading.Thread.Sleep(5000)
                                            _ftp.UploadFile(buffer, FileSet(i).FileName, False)
                                        End Try
                                    End If
                                End If

                                ' INC-1860 code added to upload other type of encoded file
                                fileName = FileSet(i).FileName.ToString()
                                If (fileName.EndsWith(".tmp")) Then
                                    bufferForRecon = New MemoryStream
                                    'format ISO-8859-1
                                    xmlDataForRecon = System.Text.Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding("ISO-8859-1"), Encoding.UTF8.GetBytes(CType(FileSet(i), AccountingFiles).XMLData))
                                    bufferForRecon.Write(xmlDataForRecon, 0, xmlDataForRecon.Length)
                                    'generate the New file name
                                    fileName = fileName.Replace(".tmp", "_iso.tmp")

                                    'upload the file. need to write common function.. after first round of testing
                                    If CType(FileSet(i), AccountingFiles).FileSubType = FileSubType.CONTROL Then

                                        Try
                                            _ftpControl.UploadFile(bufferForRecon, fileName, False)
                                        Catch ex As Exception
                                            'If the upload failed, wait 5 seconds and try again to see if it is a connection problem
                                            Threading.Thread.Sleep(5000)
                                            _ftpControl.UploadFile(bufferForRecon, fileName, False)
                                        End Try

                                    Else
                                        'Check to see if we should be FTPing or just sending the file via UNC
                                        If _AcctCompany.FTPDirectory.Contains("\") Then
                                            Dim _fil As New FileInfo(String.Format("\\{0}\{1}\{2}", _ftp.RemoteHostFTPServer, _AcctCompany.FTPDirectory, fileName))

                                            If _fil.Exists Then
                                                _fil.Delete()
                                            End If

                                            fs = _fil.Create()
                                            fs.Write(xmlDataForRecon, 0, xmlDataForRecon.Length)
                                            fs.Close()

                                        Else
                                            Try
                                                _ftp.UploadFile(bufferForRecon, fileName, False)
                                            Catch ex As Exception
                                                'If the upload failed, wait 5 seconds and try again to see if it is a connection problem
                                                Threading.Thread.Sleep(5000)
                                                _ftp.UploadFile(bufferForRecon, fileName, False)
                                            End Try
                                        End If
                                    End If
                                End If
                                ' INC-1860 end code 

                                'Set the counter on the transmission to +1
                                If Not CType(FileSet(i), AccountingFiles).FileType = FileType.trigger Then
                                    _acctTransmit.TransmissionCount = Integer.Parse(_acctTransmit.TransmissionCount) + 1
                                    _acctTransmit.RejectReason = ""
                                    _acctTransmit.TransmissionDate = StartDate
                                    _acctTransmit.StatusId = LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCOUNTING_TRANS_STATUS, ElitaPlusIdentity.Current.ActiveUser.LanguageId), AcctTransmission.STATUS_CODE_SUCCESSFUL)
                                End If

                                If Not CType(FileSet(i), AccountingFiles).FileType = FileType.trigger Then
                                    isNewBatchNumber = True

                                    If batchNumbers.Count > 0 Then
                                        For Each _batchId As BatchId In batchNumbers
                                            If _batchId.Batch_Number.Equals(CType(FileSet(i), AccountingFiles).BatchNumber) AndAlso _batchId.Event_Type = CType(FileSet(i), AccountingFiles).EventType Then
                                                isNewBatchNumber = False
                                                Exit For
                                            End If
                                        Next
                                    End If
                                    If isNewBatchNumber Then
                                        CurrentBatch = New BatchId
                                        CurrentBatch.Batch_Number = CType(FileSet(i), AccountingFiles).BatchNumber
                                        CurrentBatch.Event_Type = CType(FileSet(i), AccountingFiles).EventType
                                        CurrentBatch.isResend = CType(FileSet(i), AccountingFiles).isResend
                                        CurrentBatch.FileName = CType(FileSet(i), AccountingFiles).FileName
                                        batchNumbers.Add(CurrentBatch)
                                    End If
                                End If

                            ElseIf FileSet(i).FileType = FileType.Journal AndAlso _acctTransmit.DebitAmount <> _acctTransmit.CreditAmount Then
                                isBalanced = False
                            End If

                        Catch ex As Exception
                            'Add an error message to the reject reason.  Leave the counter as is
                            If Not FileSet(i).FileType = FileType.trigger Then
                                _acctTransmit.RejectReason = TranslationBase.TranslateLabelOrMessage(Common.ErrorCodes.FTP_FAILURE)
                                _acctTransmit.StatusId = LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCOUNTING_TRANS_STATUS, ElitaPlusIdentity.Current.ActiveUser.LanguageId), AcctTransmission.STATUS_CODE_FAILED)
                                _acctTransmit.RejectReasonDetail = String.Format("{0} : {1} : {2}", ex.Message, Environment.NewLine, ex.StackTrace)
                            End If
                            'Set the send successful flag to false to inform that the file was processed, but not send
                            SEND_SUCCESSFUL = False

                            'If the FTP send process breaks, continue processing, but log the failure
                            AppConfig.Log(CType(New ElitaPlusException("FTP_FAILURE", "", ex), Exception))

                            If FailureFiles Is Nothing Then FailureFiles = New List(Of AccountingFiles)
                            _file = FileSet(i)
                            _file.RejectReason = TranslationBase.TranslateLabelOrMessage(Common.ErrorCodes.FTP_FAILURE).ToString
                            FailureFiles.Add(_file)

                        Finally
                            'Save the updated transmission record
                            If Not FileSet(i).FileType = FileType.trigger AndAlso Not _acctTransmit Is Nothing Then
                                _acctTransmit.Save()
                            End If
                        End Try

                    End If

                Next

                'Close the ftp connection
                Try
                    _ftp.CloseConnection()
                Catch ex As Exception
                    'Try/Catch here in case connection is already closed
                End Try
            End If

            'Now that we are done, create and send triggers
            If batchNumbers IsNot Nothing AndAlso batchNumbers.Count > 0 Then

                FileSet.Clear()
                FileSet = Nothing

                For Each CurrentBatch In batchNumbers
                    If Not CurrentBatch.isResend Then
                        'Generate Trigger File now that we are done with the rest
                        BuildFile(FileType.trigger, BusinessUnit, beginDate, endDate, CurrentBatch)
                    Else
                        BuildFile(CurrentBatch.FileName, CurrentBatch.Batch_Number)
                    End If
                Next

                SendFiles_FTP()
            End If

        Catch ex As Exception
            Throw New ElitaPlusException("ACCOUNTING: ERROR FAILURE IN FTP PROCESS OF ACCOUNTING PROCESS", "", ex)
        Finally
            _ftp = Nothing
        End Try
    End Function

    Private Function SendFiles_SS()

        Dim CurrentFile As AccountingFiles
        Dim _acctTransmit As AcctTransmission
        Dim buffer As MemoryStream
        Dim xmlData() As Byte
        Dim isBalanced As Boolean = True
        Dim hasAPFiles As Boolean = False
        Dim _file As AccountingFiles

        Dim eab As EndpointAddressBuilder
        Dim ea As EndpointAddress

        Dim _bind As New NetTcpBinding()

        _bind.TransferMode = TransferMode.Streamed
        _bind.TransactionFlow = False
        _bind.TransactionProtocol = System.ServiceModel.TransactionProtocol.OleTransactions
        _bind.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard
        _bind.ListenBacklog = 10
        _bind.MaxBufferPoolSize = 524288
        _bind.MaxBufferSize = 65536
        _bind.MaxReceivedMessageSize = 65536
        _bind.Name = "NetTcpBinding_JournalManagerServiceContract"
        _bind.CloseTimeout = TimeSpan.Parse("00:10:00")
        _bind.OpenTimeout = TimeSpan.Parse("00:10:00")
        _bind.ReceiveTimeout = TimeSpan.Parse("00:10:00")
        _bind.SendTimeout = TimeSpan.Parse("00:10:00")

        _bind.Security.Mode = SecurityMode.TransportWithMessageCredential
        _bind.Security.Message.ClientCredentialType = MessageCredentialType.UserName
        _bind.Security.Transport.ClientCredentialType = TcpClientCredentialType.Windows
        _bind.Security.Transport.ProtectionLevel = Net.Security.ProtectionLevel.EncryptAndSign

        _bind.ReaderQuotas.MaxDepth = 32
        _bind.ReaderQuotas.MaxStringContentLength = 8192
        _bind.ReaderQuotas.MaxArrayLength = 16384
        _bind.ReaderQuotas.MaxBytesPerRead = 4096
        _bind.ReaderQuotas.MaxNameTableCharCount = 16384

        _bind.ReliableSession.Ordered = True
        _bind.ReliableSession.InactivityTimeout = TimeSpan.Parse("00:10:00")
        _bind.ReliableSession.Enabled = False

        Try

            'For Each CurrentFile In FileSet
            For i As Integer = 0 To FileSet.Count - 1

                If CType(FileSet(i), AccountingFiles).FileSubType = FileSubType.GL _
                    OrElse CType(FileSet(i), AccountingFiles).FileSubType = FileSubType.AP _
                    OrElse CType(FileSet(i), AccountingFiles).FileSubType = FileSubType.AP2 _
                    OrElse CType(FileSet(i), AccountingFiles).FileSubType = FileSubType.CONTROL Then

                    _acctTransmit = New AcctTransmission(CType(FileSet(i), AccountingFiles).AcctTransmissionId, True)
                    _acctTransmit.TransmissionDate = StartDate

                    Try
                        'Check the balances.  If they don't match, exit the loop and don't send
                        If ((FileSet(i).FileType = FileType.Journal AndAlso
                            _acctTransmit.DebitAmount = _acctTransmit.CreditAmount)) OrElse
                             CType(FileSet(i), AccountingFiles).FileSubType = FileSubType.AP OrElse
                             CType(FileSet(i), AccountingFiles).FileSubType = FileSubType.AP2 OrElse
                             CType(FileSet(i), AccountingFiles).FileSubType = FileSubType.CONTROL Then

                            buffer = New MemoryStream
                            xmlData = System.Text.Encoding.UTF8.GetBytes(CType(FileSet(i), AccountingFiles).XMLData)
                            buffer.Write(xmlData, 0, xmlData.Length)

                            Select Case CType(FileSet(i), AccountingFiles).FileSubType
                                Case FileSubType.GL

                                    Dim _ss As SmartStream.JournalManagerServiceContractClient
                                    Dim ty As SmartStream.BatchSummaryTypes

                                    eab = New EndpointAddressBuilder
                                    eab.Uri = New Uri(AppConfig.SmartStream.WCFGLUploadEndpoint)
                                    eab.Identity = EndpointIdentity.CreateDnsIdentity(AppConfig.SmartStream.WCFGLCertificateName)
                                    ea = eab.ToEndpointAddress
                                    _ss = New SmartStream.JournalManagerServiceContractClient(_bind, ea)
                                    _ss.ClientCredentials.UserName.UserName = AppConfig.SmartStream.WCFGLUserId
                                    _ss.ClientCredentials.UserName.Password = AppConfig.SmartStream.WCFGLPassword

                                    'DEF-TBD add noCheck to revocation certificate to avoid trust issue
                                    _ss.ClientCredentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck

                                    Try
                                        _ss.Open()
                                        buffer.Seek(0, SeekOrigin.Begin)
                                        ty = _ss.JournalUpload(buffer)
                                        ProcessBalancingResponse(ty, _acctTransmit)
                                    Catch ex As Exception
                                        Throw ex
                                    Finally
                                        Try
                                            If _ss.State <> CommunicationState.Closed AndAlso _ss.State <> CommunicationState.Faulted Then _ss.Close()
                                        Catch ex As Exception
                                        End Try
                                    End Try

                                Case FileSubType.AP, FileSubType.AP2

                                    Dim _ss As SmartStream.AP.InvoiceManagerServiceContractClient
                                    Dim ty As Object

                                    eab = New EndpointAddressBuilder
                                    eab.Uri = New Uri(AppConfig.SmartStream.WCFAPUploadEndpoint)
                                    eab.Identity = EndpointIdentity.CreateDnsIdentity(AppConfig.SmartStream.WCFAPCertificateName)
                                    ea = eab.ToEndpointAddress
                                    _ss = New SmartStream.AP.InvoiceManagerServiceContractClient(_bind, ea)
                                    _ss.ClientCredentials.UserName.UserName = AppConfig.SmartStream.WCFAPUserId
                                    _ss.ClientCredentials.UserName.Password = AppConfig.SmartStream.WCFAPPassword

                                    'DEF-TBD add noCheck to revocation certificate to avoid trust issue
                                    _ss.ClientCredentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck

                                    Try
                                        _ss.Open()
                                        buffer.Seek(0, SeekOrigin.Begin)

                                        If CType(FileSet(i), AccountingFiles).FileSubType = FileSubType.AP Then
                                            ty = _ss.InvoiceUpload(buffer)
                                            ProcessBalancingResponse(CType(ty, SmartStream.AP.BatchSummaryType), _acctTransmit)
                                        Else
                                            ty = _ss.VendorUpload(buffer)
                                            ProcessBalancingResponse(CType(ty, SmartStream.AP.VendorSummaryType), _acctTransmit)
                                        End If

                                    Catch ex As Exception
                                        Throw ex
                                    Finally
                                        Try
                                            If _ss.State <> CommunicationState.Closed AndAlso _ss.State <> CommunicationState.Faulted Then _ss.Close()
                                        Catch ex As Exception
                                        End Try
                                    End Try

                                Case FileSubType.CONTROL

                                    Dim _ss As SmartStream.JournalManagerServiceContractClient
                                    Dim ctlType As SmartStream.ControlSummaryType

                                    eab = New EndpointAddressBuilder
                                    eab.Uri = New Uri(AppConfig.SmartStream.WCFGLUploadEndpoint)
                                    eab.Identity = EndpointIdentity.CreateDnsIdentity(AppConfig.SmartStream.WCFGLCertificateName)
                                    ea = eab.ToEndpointAddress
                                    _ss = New SmartStream.JournalManagerServiceContractClient(_bind, ea)
                                    _ss.ClientCredentials.UserName.UserName = AppConfig.SmartStream.WCFGLUserId
                                    _ss.ClientCredentials.UserName.Password = AppConfig.SmartStream.WCFGLPassword

                                    Try
                                        _ss.Open()
                                        buffer.Seek(0, SeekOrigin.Begin)
                                        ctlType = _ss.ControlDataUpload(buffer)
                                        ProcessBalancingResponse(ctlType, _acctTransmit)
                                    Catch ex As Exception
                                        Throw ex
                                    Finally
                                        Try
                                            If _ss.State <> CommunicationState.Closed AndAlso _ss.State <> CommunicationState.Faulted Then _ss.Close()
                                        Catch ex As Exception
                                        End Try
                                    End Try

                            End Select

                            'Set the counter on the transmission to +1
                            _acctTransmit.TransmissionCount = Integer.Parse(_acctTransmit.TransmissionCount) + 1
                            _acctTransmit.RejectReason = ""
                            _acctTransmit.RejectReasonDetail = ""
                            _acctTransmit.TransmissionDate = StartDate
                            _acctTransmit.StatusId = LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCOUNTING_TRANS_STATUS, ElitaPlusIdentity.Current.ActiveUser.LanguageId), AcctTransmission.STATUS_CODE_SUCCESSFUL)

                        ElseIf FileSet(i).FileType = FileType.Journal AndAlso _acctTransmit.DebitAmount <> _acctTransmit.CreditAmount Then
                            isBalanced = False
                        End If

                    Catch ex As Exception
                        'Add an error message to the reject reason.  Leave the counter as is
                        _acctTransmit.RejectReason = TranslationBase.TranslateLabelOrMessage(Common.ErrorCodes.FTP_FAILURE)
                        _acctTransmit.RejectReasonDetail = String.Format("{0} : {1} : {2}", ex.Message, Environment.NewLine, ex.StackTrace)
                        _acctTransmit.StatusId = LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCOUNTING_TRANS_STATUS, ElitaPlusIdentity.Current.ActiveUser.LanguageId), AcctTransmission.STATUS_CODE_FAILED)
                        'Set the send successful flag to false to inform that the file was processed, but not send
                        SEND_SUCCESSFUL = False

                        'If the FTP send process breaks, continue processing, but log the failure
                        AppConfig.Log(CType(New ElitaPlusException("FTP_FAILURE", "", ex), Exception))

                        If FailureFiles Is Nothing Then FailureFiles = New List(Of AccountingFiles)
                        _file = FileSet(i)
                        _file.RejectReason = TranslationBase.TranslateLabelOrMessage(Common.ErrorCodes.FTP_FAILURE).ToString
                        FailureFiles.Add(_file)

                    Finally
                        'Save the updated transmission record
                        If Not _acctTransmit Is Nothing Then
                            _acctTransmit.Save()
                        End If

                    End Try

                Else
                    hasAPFiles = True
                End If
            Next

            'If the Fileset also has AP files, send them via FTP using the original SendFiles method
            If hasAPFiles Then SendFiles_FTP()

        Catch ex As Exception
            Throw New ElitaPlusException("ACCOUNTING: ERROR FAILURE IN SMARTSTREAM WEBSERVICE PROCESS OF ACCOUNTING PROCESS", "", ex)
        End Try

    End Function

    Private Sub SendFailures()

        If Not FailureFiles Is Nothing AndAlso FailureFiles.Count > 0 AndAlso Not _AcctCompany.NotifyEmail Is Nothing AndAlso _AcctCompany.NotifyEmail.Trim.Length > 0 Then
            Try

                Dim _mess As New System.Net.Mail.MailMessage
                Dim _messBody As New Text.StringBuilder
                Dim _smtp As System.Net.Mail.SmtpClient
                Dim _attach As System.Net.Mail.Attachment
                Dim _addresses() As String

                Dim buffer As MemoryStream
                Dim xmlData() As Byte


                If _AcctCompany.NotifyEmail.Contains(",") Then
                    _addresses = _AcctCompany.NotifyEmail.Split(",")
                    For Each strAddress As String In _addresses
                        If Not strAddress.Contains("@") Then
                            strAddress += "@assurant.com"
                        End If
                        _mess.To.Add(strAddress)
                    Next
                Else
                    If Not _AcctCompany.NotifyEmail.Contains("@") Then
                        _AcctCompany.NotifyEmail += "@assurant.com"
                    End If
                    _mess.To.Add(_AcctCompany.NotifyEmail)
                End If


                _mess.From = New System.Net.Mail.MailAddress(ElitaPlusIdentity.Current.ActiveUser.Company.Email)
                _mess.Subject = TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.ACCT_ERR_SENDING_FILES_SUB)
                _messBody.AppendLine(TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.ACCT_ERR_SENDING_FILES_MESS))
                _messBody.AppendLine("_____________________________________________________________")
                _messBody.AppendLine("")
                _messBody.AppendLine("")

                For Each _file As AccountingFiles In FailureFiles
                    _messBody.AppendLine(String.Format("{0} : File type:  {2}-{3} : Batch # {1} ", _file.FileName, _file.BatchNumber, [Enum].GetName(GetType(FileType), _file.FileType), [Enum].GetName(GetType(FileSubType), _file.FileSubType)))
                    _messBody.AppendLine(_file.RejectReason)
                    _messBody.AppendLine("_____________________________________________________________")

                    buffer = New MemoryStream
                    xmlData = System.Text.Encoding.UTF8.GetBytes(_file.XMLData)
                    buffer.Write(xmlData, 0, xmlData.Length)
                    buffer.Position = 0

                    _attach = New System.Net.Mail.Attachment(buffer, _file.FileName, System.Net.Mime.MediaTypeNames.Text.Plain)
                    _attach.TransferEncoding = System.Net.Mime.TransferEncoding.QuotedPrintable
                    _attach.ContentDisposition.Inline = False
                    _attach.ContentDisposition.DispositionType = "Attachment"

                    _mess.Attachments.Add(_attach)

                Next

                _mess.Body = _messBody.ToString

                _smtp = New System.Net.Mail.SmtpClient(AppConfig.ServiceOrderEmail.SmtpServer)
                _smtp.Send(_mess)
            Catch ex As Exception
            End Try

        End If

    End Sub

    'Gets the transformation xslt file
    Private Function GetResource(ResourceName As String) As System.IO.Stream

        Try
            Dim RName As String
            Dim _assembly As Assembly = Assembly.GetExecutingAssembly()
            RName = String.Format("{0}.{1}", _assembly.GetName().Name, ResourceName)

            Dim str As Stream
            str = _assembly.GetManifestResourceStream(RName)

            Return str

        Catch ex As Exception
            Throw New ElitaPlusException("ACCOUNTING: ERROR RETRIEVING ASSEMBLY RESOURCE", "", ex)
        End Try

    End Function

    'Helper function to build the filename of the current file
    Private Function GetFileName(_FileType As FileType, BusinessUnit As String) As String
        Return "E-" & System.Enum.GetName(GetType(FileType), _FileType) & "-" & BusinessUnit & "-" & StartDate.ToString("yyyy-MM-dd") & "-" & StartDate.ToString("yyyy-MM-dd") & GetFileNameEntryType() & TimeStamp.ToString & ".xml"
    End Function

    'Helper function to build the filename of the current file
    Private Function GetFileName(_FileType As FileType, BusinessUnit As String, MinDate As Date, MaxDate As Date) As String
        Return "E-" & System.Enum.GetName(GetType(FileType), _FileType) & "-" & BusinessUnit & "-" & MinDate.ToString("yyyy-MM-dd") & "-" & MaxDate.ToString("yyyy-MM-dd") & GetFileNameEntryType() & TimeStamp.ToString & ".xml"
    End Function

    'Helper function to build the filename of the current file
    Private Function GetFileName(_FileType As FileType, BusinessUnit As String, MinDate As Date, MaxDate As Date, IncludeBatch As Boolean, batchNumber As String) As String
        Return "E-" & System.Enum.GetName(GetType(FileType), _FileType) & "-" & BusinessUnit & "-" & MinDate.ToString("yyyy-MM-dd") & "-" & MaxDate.ToString("yyyy-MM-dd") & GetFileNameEntryType() & batchNumber & ".xml"
    End Function

    'Helper function to build the filename of the current file
    Private Function GetFileName(_FileType As FileType, BusinessUnit As String, MinDate As Date, MaxDate As Date, IncludeBatch As Boolean, batchNumber As BatchId) As String
        Return "E-" & System.Enum.GetName(GetType(FileType), _FileType) & "-" & BusinessUnit & "-" & MinDate.ToString("yyyy-MM-dd") & "-" & MaxDate.ToString("yyyy-MM-dd") & GetFileNameEntryType(batchNumber.Event_Type) & batchNumber.Batch_Number & ".xml"
    End Function

    'Helper function to build the filename of the current file
    Private Function GetFileName(_FileType As FileType, BusinessUnit As String, MinDate As Date, MaxDate As Date, TableName As String) As String
        Dim _filePrefix As String = System.Enum.GetName(GetType(FileType), _FileType)
        Return "E-" & _filePrefix & "-" & BusinessUnit & "-" & MinDate.ToString("yyyy-MM-dd") & "-" & MaxDate.ToString("yyyy-MM-dd") & GetFileNameEntryType() & TimeStamp.ToString & ".xml"
    End Function

    'Helper function to build the filename of the current file
    Private Function GetReversalFileName(FileName As String) As String
        BusinessUnit = FileName.Substring(FileName.IndexOf("-", 3) + 1, (FileName.IndexOf("-", FileName.IndexOf("-", 3) + 1) - FileName.IndexOf("-", 3) - 1))
        Return GetFileName(FileType.Journal, BusinessUnit, Today, Today)
    End Function

    Private Function GetFileName(_FileType As FileType, _FileSubType As FileSubType, BusinessUnit As String, MinDate As Date, MaxDate As Date,
                                 TableName As String, _AcctExt As String, JournalType As String) As String

        If _AcctExt = FELITA_PREFIX Then
            If Not _FileSubType = FileSubType.CONTROL Then
                Return GetFileName(_FileType, BusinessUnit, MinDate, MaxDate, TableName)
            Else
                Return String.Format("E_{0}_{1}_{2}.tmp", BusinessUnit, JournalType, Now.ToString("yyyyMMddHHmmss"))
            End If

        Else

            Dim env As String
            env = If(EnvironmentContext.Current.EnvironmentName.Substring(0, 1).Equals("P"), EnvironmentContext.Current.EnvironmentName.Substring(0, 1), "M").ToString

            Select Case _FileSubType
                Case FileSubType.AP1, FileSubType.AP
                    Return String.Format("{0}.AC.FTP.ELITA.SS.INVOICE.LOAD.{1}", env, MinDate.ToString("MM/dd/yyyy"))
                Case FileSubType.AP2
                    Return String.Format("{0}.AC.FTP.ELITA.SS.VENDOR.LOAD", env)
                Case FileSubType.GL
                    Return String.Format("{0}.AC.FTP.ELITA.SS.GL.LOAD.{1}", env, MinDate.ToString("MM/dd/yyyy"))
                Case FileSubType.CONTROL
                    Return String.Format("{0}.AC.FTP.ELITA.SS.GL.CONTROL.{1}", env, MinDate.ToString("MM/dd/yyyy"))
            End Select
        End If

    End Function

    Private Function GetFileNameEntryType() As String

        Dim strCode As String = ""

        Select Case EVENT_TYPE
            Case EventType.CLAIM, EventType.PREM
                strCode = "-Entries-L-"
            Case EventType.IBNR, EventType.UPR, EventType.CLAIMRES
                strCode = "-Auto-R-"
            Case EventType.REFUNDS, EventType.INV
                strCode = "-INVOICE-C-"
            Case EventType.REVERSAL
                strCode = "-REGEN-R-"
            Case Else
                strCode = "-Entries-L-"
        End Select

        Return strCode

    End Function

    Private Function GetFileNameEntryType(_EventType As EventType) As String

        Dim strCode As String = ""

        Select Case _EventType
            Case EventType.CLAIM, EventType.PREM
                strCode = "-Entries-L-"
            Case EventType.IBNR, EventType.UPR, EventType.CLAIMRES
                strCode = "-Auto-R-"
            Case EventType.REFUNDS, EventType.INV
                strCode = "-INVOICE-C-"
            Case EventType.REVERSAL
                strCode = "-REGEN-R-"
            Case Else
                strCode = "-Entries-L-"
        End Select

        Return strCode

    End Function

    ''' <summary>
    ''' Sets the balancing fields in Elita
    ''' </summary>
    ''' <param name="ret"></param>
    ''' <param name="AcctTrans"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Sub ProcessBalancingResponse(ret As Object, AcctTrans As AcctTransmission, Optional ByVal _fel As Felita.FelitaConnectServiceClient = Nothing)


        If ret.GetType Is GetType(SmartStream.BatchSummaryTypes) Then

            Dim drAmount, crAmount As Decimal

            For Each _bt As SmartStream.BatchSummaryType In CType(ret, SmartStream.BatchSummaryTypes)
                AcctTrans.NumTransactionsReceived += _bt.RecordCount
                AcctTrans.RejectReasonDetail += _bt.ErrorDescription
                AcctTrans.DateReceived = _bt.LoadTime
                drAmount += _bt.TotalDebits
                crAmount += _bt.TotalCredits
            Next

            AcctTrans.DebitAmountReceived = Math.Abs(drAmount)
            AcctTrans.CreditAmountReceived = Math.Abs(crAmount)

        ElseIf ret.GetType Is GetType(SmartStream.ControlSummaryType) Then

            AcctTrans.NumTransactionsReceived = CType(ret, SmartStream.ControlSummaryType).RecordCount
            AcctTrans.RejectReasonDetail = CType(ret, SmartStream.ControlSummaryType).ErrorDescription
            AcctTrans.DateReceived = CType(ret, SmartStream.ControlSummaryType).LoadTime
            AcctTrans.DebitAmountReceived = CType(ret, SmartStream.ControlSummaryType).TotalDebits
            AcctTrans.CreditAmountReceived = CType(ret, SmartStream.ControlSummaryType).TotalCredits

        ElseIf ret.GetType Is GetType(SmartStream.AP.BatchSummaryType) Then

            AcctTrans.NumTransactionsReceived = CType(ret, SmartStream.AP.BatchSummaryType).RecordCount
            AcctTrans.RejectReasonDetail = CType(ret, SmartStream.AP.BatchSummaryType).ErrorDescription
            AcctTrans.DateReceived = CType(ret, SmartStream.AP.BatchSummaryType).LoadTime

        ElseIf ret.GetType Is GetType(SmartStream.AP.VendorSummaryType) Then

            AcctTrans.NumTransactionsReceived = CType(ret, SmartStream.AP.VendorSummaryType).RecordCount
            AcctTrans.RejectReasonDetail = CType(ret, SmartStream.AP.VendorSummaryType).ErrorDescription
            AcctTrans.DateReceived = CType(ret, SmartStream.AP.VendorSummaryType).LoadTime

        Else

            If CType(ret, Object()).Length > 0 Then
                Dim _errs As New System.Text.StringBuilder
                For Each o As Object In CType(ret, Object())
                    _errs.AppendLine(o.ToString)
                Next
                AcctTrans.RejectReasonDetail = _errs.ToString
            End If

            If AcctTrans.FileTypeFlag = FelitaEngine.FileType.Journal Then

                Dim _felTbl As Felita.dsJournalInfo.JournalInfoDataTable
                Dim eab As EndpointAddressBuilder
                Dim ea As EndpointAddress
                Dim _bind As New BasicHttpBinding

                If _fel Is Nothing Then
                    eab = New EndpointAddressBuilder
                    eab.Uri = New Uri(AppConfig.Felita.WCFUploadEndpoint)
                    ea = eab.ToEndpointAddress
                    _fel = New Felita.FelitaConnectServiceClient(_bind, ea)
                End If

                Try

                    _felTbl = _fel.GetBatchStatus(AcctTrans.BatchNumber, BusinessUnit)

                    If Not _felTbl Is Nothing AndAlso _felTbl.Rows.Count = 1 Then
                        AcctTrans.NumTransactionsReceived = If(IsDBNull(_felTbl(0)(_felTbl.TransactionTotalColumn.Ordinal)), 0, _felTbl(0)(_felTbl.TransactionTotalColumn.Ordinal))
                        AcctTrans.DateReceived = If(IsDBNull(_felTbl(0)(_felTbl.DateProcessedColumn.Ordinal)), Date.MinValue, _felTbl(0)(_felTbl.DateProcessedColumn.Ordinal))
                        AcctTrans.DebitAmountReceived = _felTbl(0)(_felTbl.DebitAmountColumn.Ordinal)
                        AcctTrans.CreditAmountReceived = _felTbl(0)(_felTbl.CreditAmountColumn.Ordinal)
                    End If
                Catch ex As Exception
                    AcctTrans.RejectReasonDetail += String.Format("Error retrieving Felita Batch Status: {0} : {1}", ex.Message, ex.StackTrace)
                End Try

            End If

        End If

    End Sub

#End Region

#Region "Public Properties"

    Public Property AccountingEventId As String
        Get
            If Row(DATA_COL_NAME_ACCOUNTING_EVENT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_ACCOUNTING_EVENT_ID), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_ACCOUNTING_EVENT_ID, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property CompanyId As String
        Get
            If Row(DATA_COL_NAME_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_COMPANY_ID), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_COMPANY_ID, Value)
        End Set
    End Property

    Public Property VendorFiles As Boolean
        Get
            If Row(DATA_COL_NAME_VENDOR_FILES) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_VENDOR_FILES), Boolean))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_VENDOR_FILES, Value)
        End Set
    End Property

#End Region

End Class
