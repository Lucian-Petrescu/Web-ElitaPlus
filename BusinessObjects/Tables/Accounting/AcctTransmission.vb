'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (4/19/2007)  ********************

Public Class AcctTransmission
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Dataset = New DataSet
        Load(id)
    End Sub

    'Exiting BO
    Public Sub New(ByVal id As Guid, ByVal OmitText As Boolean)
        MyBase.New()
        Dataset = New DataSet
        Load(id, OmitText)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Dataset = New DataSet
        Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load()
    End Sub

    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New AcctTransmissionDAL

            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If

            Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow

            Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow

            setvalue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid, Optional ByVal OmitText As Boolean = True)
        Try
            Dim dal As New AcctTransmissionDAL

            If _isDSCreator Then
                If Not Row Is Nothing Then
                    Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Row)
                End If
            End If

            Row = Nothing

            If Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Row = FindRow(id, dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_NAME))
            End If

            If Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.Load(Dataset, id, OmitText)
                Row = FindRow(id, dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_NAME))
            End If

            If Row Is Nothing Then
                Throw New DataNotFoundException
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
#End Region

#Region "Private Members"
    'Initialization code for new objects
    Private Sub Initialize()
    End Sub

    Private Const XML_FIELD_TRANSACTIONAMOUNT As String = "transactionamount"
    Private Const XML_FIELD_LINEAMOUNT As String = "line_amount"
    Private Const XML_FIELD_TOTALAMOUNT As String = "TotalDebits"

    Public Const STATUS_CODE_DELETED As String = "4"
    Public Const STATUS_CODE_SUCCESSFUL As String = "3"
    Public Const STATUS_CODE_FAILED As String = "2"
    Public Const STATUS_CODE_PENDING As String = "1"
    Public Const STATUS_CODE_PROCESSING As String = "0"

#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If row(AcctTransmissionDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AcctTransmissionDAL.COL_NAME_ACCT_TRANSMISSION_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=1000)> _
    Public Property FileName() As String
        Get
            CheckDeleted()
            If Row(AcctTransmissionDAL.COL_NAME_FILE_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctTransmissionDAL.COL_NAME_FILE_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(AcctTransmissionDAL.COL_NAME_FILE_NAME, Value)
        End Set
    End Property

    Public Property FileText() As String
        Get
            CheckDeleted()
            If Row(AcctTransmissionDAL.COL_NAME_FILE_TEXT) Is DBNull.Value Then
                Return Nothing
            Else
                Return Row(AcctTransmissionDAL.COL_NAME_FILE_TEXT).ToString()
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(AcctTransmissionDAL.COL_NAME_FILE_TEXT, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=30)> _
    Public Property JournalType() As String
        Get
            CheckDeleted()
            If Row(AcctTransmissionDAL.COL_NAME_JOURNAL_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(AcctTransmissionDAL.COL_NAME_JOURNAL_TYPE), String))
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(AcctTransmissionDAL.COL_NAME_JOURNAL_TYPE, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property TransmissionDate() As DateType
        Get
            CheckDeleted()
            If row(AcctTransmissionDAL.COL_NAME_TRANSMISSION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(AcctTransmissionDAL.COL_NAME_TRANSMISSION_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            SetValue(AcctTransmissionDAL.COL_NAME_TRANSMISSION_DATE, Value)
        End Set
    End Property

    Public Property TransmissionReceived() As DateType
        Get
            CheckDeleted()
            If row(AcctTransmissionDAL.COL_NAME_TRANSMISSION_RECEIVED) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(AcctTransmissionDAL.COL_NAME_TRANSMISSION_RECEIVED), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            SetValue(AcctTransmissionDAL.COL_NAME_TRANSMISSION_RECEIVED, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property TransmissionCount() As LongType
        Get
            CheckDeleted()
            If row(AcctTransmissionDAL.COL_NAME_TRANSMISSION_COUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(AcctTransmissionDAL.COL_NAME_TRANSMISSION_COUNT), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            SetValue(AcctTransmissionDAL.COL_NAME_TRANSMISSION_COUNT, Value)
        End Set
    End Property

    <ValueMandatory("")> _
      Public Property CompanyId() As Guid
        Get
            CheckDeleted()
            If Row(AcctTransmissionDAL.COL_NAME_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AcctTransmissionDAL.COL_NAME_COMPANY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(AcctTransmissionDAL.COL_NAME_COMPANY_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property DebitAmount() As Decimal
        Get
            CheckDeleted()
            If Row(AcctTransmissionDAL.COL_NAME_DEBIT_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctTransmissionDAL.COL_NAME_DEBIT_AMOUNT), Decimal)
            End If
        End Get
        Set(ByVal Value As Decimal)
            CheckDeleted()
            SetValue(AcctTransmissionDAL.COL_NAME_DEBIT_AMOUNT, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property CreditAmount() As Decimal
        Get
            CheckDeleted()
            If Row(AcctTransmissionDAL.COL_NAME_CREDIT_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctTransmissionDAL.COL_NAME_CREDIT_AMOUNT), Decimal)
            End If
        End Get
        Set(ByVal Value As Decimal)
            CheckDeleted()
            SetValue(AcctTransmissionDAL.COL_NAME_CREDIT_AMOUNT, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=250)> _
    Public Property RejectReason() As String
        Get
            CheckDeleted()
            If Row(AcctTransmissionDAL.COL_NAME_REJECT_REASON) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(AcctTransmissionDAL.COL_NAME_REJECT_REASON), String))
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(AcctTransmissionDAL.COL_NAME_REJECT_REASON, Value)
        End Set
    End Property

    Public Property FileTypeFlag() As Integer
        Get
            CheckDeleted()
            If Row(AcctTransmissionDAL.COL_NAME_FILE_TYPE_FLAG) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctTransmissionDAL.COL_NAME_FILE_TYPE_FLAG), Integer)
            End If
        End Get
        Set(ByVal value As Integer)
            CheckDeleted()
            SetValue(AcctTransmissionDAL.COL_NAME_FILE_TYPE_FLAG, value)
        End Set
    End Property

    Public Property FileSubTypeFlag() As Integer
        Get
            CheckDeleted()
            If Row(AcctTransmissionDAL.COL_NAME_FILE_SUB_TYPE_FLAG) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctTransmissionDAL.COL_NAME_FILE_SUB_TYPE_FLAG), Integer)
            End If
        End Get
        Set(ByVal value As Integer)
            CheckDeleted()
            SetValue(AcctTransmissionDAL.COL_NAME_FILE_SUB_TYPE_FLAG, value)
        End Set
    End Property

    Public Property NumTransactionsSent() As Integer
        Get
            CheckDeleted()
            If Row(AcctTransmissionDAL.COL_NAME_NUM_TRANSACTIONS_SENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctTransmissionDAL.COL_NAME_NUM_TRANSACTIONS_SENT), Integer)
            End If
        End Get
        Set(ByVal value As Integer)
            CheckDeleted()
            SetValue(AcctTransmissionDAL.COL_NAME_NUM_TRANSACTIONS_SENT, value)
        End Set
    End Property

    Public Property NumTransactionsReceived() As Integer
        Get
            CheckDeleted()
            If Row(AcctTransmissionDAL.COL_NAME_NUM_TRANSACTIONS_RECEIVED) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctTransmissionDAL.COL_NAME_NUM_TRANSACTIONS_RECEIVED), Integer)
            End If
        End Get
        Set(ByVal value As Integer)
            CheckDeleted()
            SetValue(AcctTransmissionDAL.COL_NAME_NUM_TRANSACTIONS_RECEIVED, value)
        End Set
    End Property

    Public Property DebitAmountReceived() As Decimal
        Get
            CheckDeleted()
            If Row(AcctTransmissionDAL.COL_NAME_DEBIT_AMOUNT_RECEIVED) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctTransmissionDAL.COL_NAME_DEBIT_AMOUNT_RECEIVED), Decimal)
            End If
        End Get
        Set(ByVal Value As Decimal)
            CheckDeleted()
            SetValue(AcctTransmissionDAL.COL_NAME_DEBIT_AMOUNT_RECEIVED, Value)
        End Set
    End Property

    Public Property CreditAmountReceived() As Decimal
        Get
            CheckDeleted()
            If Row(AcctTransmissionDAL.COL_NAME_CREDIT_AMOUNT_RECEIVED) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctTransmissionDAL.COL_NAME_CREDIT_AMOUNT_RECEIVED), Decimal)
            End If
        End Get
        Set(ByVal Value As Decimal)
            CheckDeleted()
            SetValue(AcctTransmissionDAL.COL_NAME_CREDIT_AMOUNT_RECEIVED, Value)
        End Set
    End Property

    Public Property DateReceived() As DateType
        Get
            CheckDeleted()
            If Row(AcctTransmissionDAL.COL_NAME_DATE_RECEIVED) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(AcctTransmissionDAL.COL_NAME_DATE_RECEIVED), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            SetValue(AcctTransmissionDAL.COL_NAME_DATE_RECEIVED, Value)
        End Set
    End Property

    Public Property BatchNumber() As String
        Get
            CheckDeleted()
            If Row(AcctTransmissionDAL.COL_NAME_BATCH_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(AcctTransmissionDAL.COL_NAME_BATCH_NUMBER), String))
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(AcctTransmissionDAL.COL_NAME_BATCH_NUMBER, Value)
        End Set
    End Property

    Public Property RejectReasonDetail() As String
        Get
            CheckDeleted()
            If Row(AcctTransmissionDAL.COL_NAME_REJECT_REASON_DETAIL) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(AcctTransmissionDAL.COL_NAME_REJECT_REASON_DETAIL), String))
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(AcctTransmissionDAL.COL_NAME_REJECT_REASON_DETAIL, Value)
        End Set
    End Property

    <ValueMandatory("")> _
      Public Property StatusId() As Guid
        Get
            CheckDeleted()
            If Row(AcctTransmissionDAL.COL_NAME_STATUS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AcctTransmissionDAL.COL_NAME_STATUS_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(AcctTransmissionDAL.COL_NAME_STATUS_ID, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()

        Dim dal As AcctTransmissionDAL

        Try
            'Overriding the generic save routine as the insert will fail.  Need to manually add the
            ' paramters in the DAL
            If Me.Row.RowState = DataRowState.Added Then
                dal = New AcctTransmissionDAL
                dal.InsertCustom(Row, ElitaPlusIdentity.Current.ActiveUser.NetworkId)
                'Reload the Data from the DB
                Dim objId As Guid = Id
                Dataset = New DataSet
                Row = Nothing
                Load(objId)
            ElseIf Me.Row.RowState = DataRowState.Deleted Or Me.Row.RowState = DataRowState.Detached Then
                dal = New AcctTransmissionDAL
                Dim objId As Guid = New Guid(CType(Row(AcctTransmissionDAL.COL_NAME_ACCT_TRANSMISSION_ID, DataRowVersion.Original), Byte()))
                dal.DeleteCustom(objId, LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCOUNTING_TRANS_STATUS, ElitaPlusIdentity.Current.ActiveUser.LanguageId), STATUS_CODE_DELETED))
            Else
                MyBase.Save()
                If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                    dal = New AcctTransmissionDAL
                    dal.UpdateWithParam(Row)
                    'Reload the Data from the DB
                    If Row.RowState <> DataRowState.Detached Then
                        Dim objId As Guid = Id
                        Dataset = New DataSet
                        Row = Nothing
                        Load(objId)
                    End If
                End If
            End If

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    Public Sub ValidateBalance()

        Try
            Dim ds As DataSet
            Dim tbl As DataTable
            Dim tblName As String = ""
            Dim dcField As String
            Dim _smartStream As Boolean = False
            Dim _apFile As Boolean = False

            ds = GetDataSet()

            If ds Is Nothing Then
                CreditAmount = 0
                DebitAmount = 0
                NumTransactionsSent = 0
                Exit Sub
            End If

            For i As Integer = 0 To ds.Tables.Count - 1
                If ds.Tables(i).Columns.Contains(XML_FIELD_TRANSACTIONAMOUNT) _
                            OrElse ds.Tables(i).Columns.Contains(XML_FIELD_LINEAMOUNT) _
                            OrElse ds.Tables(i).Columns.Contains(XML_FIELD_TOTALAMOUNT) Then
                    tblName = ds.Tables(i).TableName

                    If FileSubTypeFlag = FelitaEngine.FileSubType.CONTROL Then
                        Exit For
                    Else

                        If ds.Tables(tblName).Columns.Contains("DebitCredit") Then
                            dcField = "DebitCredit"
                        ElseIf ds.Tables(tblName).Columns.Contains("PrimaryDRCRCode") Then
                            dcField = "PrimaryDRCRCode"
                            _smartStream = True
                        ElseIf ds.Tables(tblName).TableName = AcctTransLogDAL.Table_AP_INVOICE Then
                            _apFile = True
                        Else
                            RejectReason = TranslationBase.TranslateLabelOrMessage(Assurant.ElitaPlus.Common.ErrorCodes.FILE_OUT_OF_BALANCE)
                            Exit Sub
                        End If

                        Exit For
                    End If
                End If

            Next

            tbl = ds.Tables(tblName)
            If Not tbl Is Nothing Then

                If tbl.Rows.Count = 0 Then
                    CreditAmount = 0
                    DebitAmount = 0
                    NumTransactionsSent = 0
                    Exit Sub
                End If

                tbl.Columns.Add(AcctTransLogDAL.COL_NAME_PAYMENT_AMOUNT, GetType(Decimal), If(_apFile, XML_FIELD_LINEAMOUNT, If(tblName.Equals("ControlRecord"), XML_FIELD_TOTALAMOUNT, XML_FIELD_TRANSACTIONAMOUNT)))

                NumTransactionsSent = tbl.Rows.Count

                If _apFile Then
                    DebitAmount = Math.Round(CType(tbl.Compute("sum(" & AcctTransLogDAL.COL_NAME_PAYMENT_AMOUNT & ")", ""), Decimal), 2)
                    CreditAmount = DebitAmount
                    Exit Sub
                ElseIf FileSubTypeFlag = FelitaEngine.FileSubType.CONTROL Then
                    DebitAmount = Math.Round(CType(tbl.Compute("sum(" & AcctTransLogDAL.COL_NAME_PAYMENT_AMOUNT & ")", ""), Decimal), 2)
                    CreditAmount = DebitAmount
                    Exit Sub
                ElseIf tbl.Compute(String.Format("Count({0})", dcField), String.Format("{0} = 'C'", dcField)) Is DBNull.Value OrElse tbl.Compute(String.Format("Count({0})", dcField), String.Format("{0} = 'C'", dcField)) = 0 Then
                    CreditAmount = 0
                Else
                    CreditAmount = Math.Round(CType(tbl.Compute("sum(" & AcctTransLogDAL.COL_NAME_PAYMENT_AMOUNT & ")", String.Format("{0} = 'C'", dcField)), Decimal), 2)
                    If _smartStream Then
                        CreditAmount = Math.Abs(CreditAmount)
                    End If
                End If

                If tbl.Compute(String.Format("Count({0})", dcField), String.Format("{0} = 'D'", dcField)) Is DBNull.Value OrElse tbl.Compute(String.Format("Count({0})", dcField), String.Format("{0} = 'D'", dcField)) = 0 Then
                    DebitAmount = 0
                Else
                    DebitAmount = Math.Round(CType(tbl.Compute("sum(" & AcctTransLogDAL.COL_NAME_PAYMENT_AMOUNT & ")", String.Format("{0} = 'D'", dcField)), Decimal), 2)
                End If

                If CreditAmount <> DebitAmount Then
                    RejectReason = TranslationBase.TranslateLabelOrMessage(Assurant.ElitaPlus.Common.ErrorCodes.FILE_OUT_OF_BALANCE)
                End If

            Else
                CreditAmount = 0
                DebitAmount = 0
                NumTransactionsSent = 0
            End If

        Catch ex As Exception
            RejectReason = TranslationBase.TranslateLabelOrMessage(Assurant.ElitaPlus.Common.ErrorCodes.FILE_OUT_OF_BALANCE)
        End Try

    End Sub

    Private Function GetDataSet() As DataSet

        Dim ds As New DataSet

        'If xml is found, fill the dataset with 
        If FileText.ToString.ToLower.Contains(String.Format("<{0}>", XML_FIELD_TRANSACTIONAMOUNT)) _
            OrElse FileText.ToString.ToLower.Contains(String.Format("<{0}>", XML_FIELD_LINEAMOUNT)) _
            OrElse FileText.ToString.ToLower.Contains(String.Format("<{0}>", XML_FIELD_TOTALAMOUNT.ToLower)) Then

            Dim txtRead As System.IO.TextReader = New System.IO.StringReader(FileText)
            Dim xlr As System.Xml.XmlReader = System.Xml.XmlReader.Create(txtRead)

            ds.ReadXml(xlr)

        Else

            Return Nothing


        End If

        Return ds

    End Function

#End Region

#Region "DataView Retrieveing Methods"

    Public Class AcctTransmissionSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_ACCT_TRANSMISSION_ID As String = "acct_transmission_id"
        Public Const COL_FILE_NAME As String = "file_name"
        Public Const COL_BATCH_NUMBER As String = "batch_number_id"
        Public Const COL_CREATED_DATE As String = "created_date"
        Public Const COL_REJECT_REASON As String = "reject_reason"
        Public Const COL_DEBIT_AMOUNT As String = "debit_amount"
        Public Const COL_CREDIT_AMOUNT As String = "credit_amount"
        Public Const COL_STATUS As String = "status"
#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

    Public Shared Function GetFailures(ByVal CompanyId As Guid) As AcctTransmissionSearchDV

        Try
            Dim _acctTransDAL As New AcctTransmissionDAL
            Dim _arrStatus As New ArrayList

            _arrStatus.Add(LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCOUNTING_TRANS_STATUS, ElitaPlusIdentity.Current.ActiveUser.LanguageId), AcctTransmission.STATUS_CODE_PENDING))
            _arrStatus.Add(LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCOUNTING_TRANS_STATUS, ElitaPlusIdentity.Current.ActiveUser.LanguageId), AcctTransmission.STATUS_CODE_FAILED))

            Return New AcctTransmissionSearchDV(_acctTransDAL.LoadList(CompanyId, _arrStatus, ElitaPlusIdentity.Current.ActiveUser.LanguageId, False).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetAssociatedFailures(ByVal ParentFileName As String, ByVal ParentBatchNumber As String) As AcctTransmissionSearchDV

        Try
            Dim _acctTransDAL As New AcctTransmissionDAL
            Return New AcctTransmissionSearchDV(_acctTransDAL.LoadList(ParentFileName, ParentBatchNumber, TranslationBase.TranslateLabelOrMessage(Common.ErrorCodes.FTP_FAILURE), LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCOUNTING_TRANS_STATUS, ElitaPlusIdentity.Current.ActiveUser.LanguageId), STATUS_CODE_DELETED), ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetFilesForReversal(ByVal CompanyId As Guid, ByRef StartDate As Date, ByRef EndDate As Date) As AcctTransmissionSearchDV

        Try
            Dim _acctTransDAL As New AcctTransmissionDAL
            Return New AcctTransmissionSearchDV(_acctTransDAL.LoadList(CompanyId, StartDate, EndDate, String.Empty, LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCOUNTING_TRANS_STATUS, ElitaPlusIdentity.Current.ActiveUser.LanguageId), STATUS_CODE_DELETED), ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetFilesForReversal(ByVal CompanyId As Guid, _
                                                ByRef BatchNumber As String) As AcctTransmissionSearchDV

        Try
            Dim _acctTransDAL As New AcctTransmissionDAL
            Return New AcctTransmissionSearchDV(_acctTransDAL.LoadList(CompanyId, Date.MinValue, Date.MinValue, BatchNumber, LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCOUNTING_TRANS_STATUS, ElitaPlusIdentity.Current.ActiveUser.LanguageId), STATUS_CODE_DELETED), ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetTransmissionsForReport(ByVal CompanyId As Guid, _
                                                     ByVal FileName As String, _
                                                     ByVal AcctTransmissionId As Guid) As AcctTransmissionSearchDV

        Try
            Dim _acctTransDAL As New AcctTransmissionDAL
            Return New AcctTransmissionSearchDV(_acctTransDAL.LoadList(CompanyId, FileName, AcctTransmissionId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

#End Region

#Region "XML Migration Methods"

    Public Shared Sub MigrateTransmissions(ByVal rowcount As Integer, ByVal modified_by As String)
        Try
            Dim _acctTransDAL As New AcctTransmissionDAL
            _acctTransDAL.MigrateXML(rowcount, modified_by)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Sub

    Public Shared Function TransmissionsToMigrate() As Integer
        Try
            Dim _acctTransDAL As New AcctTransmissionDAL
            Return _acctTransDAL.TransmissionsToMigrate()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#End Region

End Class


