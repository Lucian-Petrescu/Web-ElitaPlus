'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (11/12/2008)  ********************

Public Class CancellationReqException
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Dataset = New DataSet
        Load(id)
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
            Dim dal As New TransactionLogHeaderDAL
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
            Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            setvalue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()

        Catch ex As BOValidationException
            Throw ex
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New TransactionLogHeaderDAL
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
                dal.Load(Dataset, id)
                Row = FindRow(id, dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then
                Throw New DataNotFoundException
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function AddTransactionLogHeader(ByVal transactionLogHeaderId As Guid) As TransactionLogHeader
        Dim objTransLogHeader As TransactionLogHeader

        If Not transactionLogHeaderId.Equals(Guid.Empty) Then
            objTransLogHeader = New TransactionLogHeader(transactionLogHeaderId, Dataset)
        Else
            objTransLogHeader = New TransactionLogHeader(Dataset)
        End If

        Return objTransLogHeader
    End Function

#End Region

#Region "Private Members"
    'Initialization code for new objects
    Private Sub Initialize()
    End Sub

    Const PARAM_NOTIFY_INTERVAL As Integer = 48
    Public Const SYSTEM_TYPE_ELITA As String = "Elita"
    Public Const SYSTEM_TYPE_GVS As String = "GVS"

    Const TEST_EMAIL As String = "elitaba@assurant.com"

#End Region


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If row(TransactionLogHeaderDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(TransactionLogHeaderDAL.COL_NAME_TRANSACTION_LOG_HEADER_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CompanyGroupId() As Guid
        Get
            CheckDeleted()
            If Row(TransactionLogHeaderDAL.COL_NAME_COMPANY_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(TransactionLogHeaderDAL.COL_NAME_COMPANY_GROUP_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(TransactionLogHeaderDAL.COL_NAME_COMPANY_GROUP_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property FunctionTypeID() As Guid
        Get
            CheckDeleted()
            If Row(TransactionLogHeaderDAL.COL_NAME_FUNCTION_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(TransactionLogHeaderDAL.COL_NAME_FUNCTION_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(TransactionLogHeaderDAL.COL_NAME_FUNCTION_TYPE_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property TransactionXml() As Object
        Get
            CheckDeleted()
            If row(TransactionLogHeaderDAL.COL_NAME_TRANSACTION_XML) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(TransactionLogHeaderDAL.COL_NAME_TRANSACTION_XML), Object)
            End If
        End Get
        Set(ByVal Value As Object)
            CheckDeleted()
            SetValue(TransactionLogHeaderDAL.COL_NAME_TRANSACTION_XML, Value)
        End Set
    End Property

    Public Property TransactionProcessedDate() As DateType
        Get
            CheckDeleted()
            If row(TransactionLogHeaderDAL.COL_NAME_TRANSACTION_PROCESSED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(TransactionLogHeaderDAL.COL_NAME_TRANSACTION_PROCESSED_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            SetValue(TransactionLogHeaderDAL.COL_NAME_TRANSACTION_PROCESSED_DATE, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property TransactionStatusID() As Guid
        Get
            CheckDeleted()
            If Row(TransactionLogHeaderDAL.COL_NAME_TRANSACTION_STATUS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(TransactionLogHeaderDAL.COL_NAME_TRANSACTION_STATUS_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(TransactionLogHeaderDAL.COL_NAME_TRANSACTION_STATUS_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=100)> _
    Public Property GVSoriginalTransNo() As String
        Get
            CheckDeleted()
            If Row(TransactionLogHeaderDAL.COL_NAME_GVS_ORIGINAL_TRANS_NO) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(TransactionLogHeaderDAL.COL_NAME_GVS_ORIGINAL_TRANS_NO), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(TransactionLogHeaderDAL.COL_NAME_GVS_ORIGINAL_TRANS_NO, Value)
        End Set
    End Property

    Public Property OriginalTransLogHdrID() As Guid
        Get
            CheckDeleted()
            If Row(TransactionLogHeaderDAL.COL_NAME_ORIGINAL_TRANS_LOG_HDR_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(TransactionLogHeaderDAL.COL_NAME_ORIGINAL_TRANS_LOG_HDR_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(TransactionLogHeaderDAL.COL_NAME_ORIGINAL_TRANS_LOG_HDR_ID, Value)
        End Set
    End Property


    Public ReadOnly Property MyDataSet() As DataSet
        Get
            Return Dataset
        End Get
    End Property

    Public Property KeyID() As Guid
        Get
            CheckDeleted()
            If Row(TransactionLogHeaderDAL.COL_NAME_KEY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(TransactionLogHeaderDAL.COL_NAME_KEY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(TransactionLogHeaderDAL.COL_NAME_KEY_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=1)> _
    Public Property Hide() As String
        Get
            CheckDeleted()
            If Row(TransactionLogHeaderDAL.COL_NAME_HIDE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(TransactionLogHeaderDAL.COL_NAME_HIDE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(TransactionLogHeaderDAL.COL_NAME_HIDE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=1)> _
    Public Property Resend() As String
        Get
            CheckDeleted()
            If Row(TransactionLogHeaderDAL.COL_NAME_RESEND) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(TransactionLogHeaderDAL.COL_NAME_RESEND), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(TransactionLogHeaderDAL.COL_NAME_RESEND, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsFamilyDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New TransactionLogHeaderDAL
                MyBase.UpdateFamily(Dataset)
                dal.InsertCustom(Dataset)
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Id
                    Dataset = New DataSet
                    Row = Nothing
                    Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    Public Sub UpdateTransaction()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New TransactionLogHeaderDAL
                dal.UpdateCustom(Row)
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Id
                    Dataset = New DataSet
                    Row = Nothing
                    Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    'Public Shared Function CreateTranLogHeaderForClaim(ByVal dsClaim As DataSet, ByVal claimObj As Claim) As Boolean
    '    Dim ret As Boolean = False

    '    If Not claimObj Is Nothing AndAlso Not claimObj.ServiceCenterObject Is Nothing AndAlso claimObj.ServiceCenterObject.IntegratedWithGVS Then
    '        Dim logHeader As TransactionLogHeader = Nothing

    '        If claimObj.IsNew Then
    '            ' GVS Function Type = NEW_CLAIM
    '            logHeader = claimObj.AddTransactionLogHeader(Guid.Empty)
    '            logHeader.TransactionStatusID = LookupListNew.GetIdFromCode(LookupListNew.GetGVSTransactionStatusList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), DALObjects.TransactionLogHeaderDAL.TRANSACTION_STATUS_NEW)
    '            logHeader.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
    '            logHeader.FunctionTypeID = LookupListNew.GetIdFromCode(LookupListNew.GetGVSFunctionTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), DALObjects.TransactionLogHeaderDAL.FUNCTION_TYPE_CODE_GVS_NEW_CLAIM)
    '            logHeader.TransactionXml = GetTransactionXML_NewClaim(claimObj, logHeader.Id)
    '        Else
    '            ' GVS Function Type = UPDATE_CLAIM
    '            ' Seperated transaction from the claim family since the claim auth detail information is not saved until 
    '            ' saving the claim information. Thus, can't compose the Parts List xml correctly. 
    '            logHeader = New TransactionLogHeader
    '            logHeader.TransactionStatusID = LookupListNew.GetIdFromCode(LookupListNew.GetGVSTransactionStatusList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), DALObjects.TransactionLogHeaderDAL.TRANSACTION_STATUS_NEW)
    '            logHeader.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
    '            logHeader.FunctionTypeID = LookupListNew.GetIdFromCode(LookupListNew.GetGVSFunctionTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), DALObjects.TransactionLogHeaderDAL.FUNCTION_TYPE_CODE_GVS_UPDATE_CLAIM)
    '            logHeader.TransactionXml = GetTransactionXML_UpdateClaim(dsClaim, claimObj, logHeader.Id)
    '            logHeader.Save()
    '        End If

    '        ret = True
    '    End If

    '    Return ret
    'End Function

    'Public Shared Function GetTransactionXML_UpdateClaim(ByVal dsClaim As DataSet, ByVal claimObj As Claim, ByVal transactionId As Guid) As String
    '    Dim xml As String = ""
    '    Dim itemNumber As String = "1"

    '    Dim ds As DataSet = New DataSet("TRANSACTION")

    '    Dim dtHeader As DataTable = New DataTable("TRANSACTION_HEADER")
    '    dtHeader.Columns.Add("TRANSACTION_ID", GetType(String))
    '    dtHeader.Columns.Add("FUNCTION_TYPE", GetType(String))
    '    Dim rwHeader As DataRow = dtHeader.NewRow
    '    rwHeader(0) = GuidControl.GuidToHexString(transactionId)
    '    rwHeader(1) = DALObjects.TransactionLogHeaderDAL.FUNCTION_TYPE_CODE_GVS_UPDATE_CLAIM
    '    dtHeader.Rows.Add(rwHeader)
    '    ds.Tables.Add(dtHeader)

    '    Dim dt As DataTable = New DataTable("TRANSACTION_DATA_RECORD")
    '    dt.Columns.Add("CLAIM_ID", GetType(String))
    '    dt.Columns.Add("ITEM_NUMBER", GetType(String))
    '    dt.Columns.Add("CLAIM_NUMBER", GetType(String))
    '    dt.Columns.Add("CLAIM_STATUS", GetType(String))
    '    dt.Columns.Add("REASON_CLOSE_CODE", GetType(String))
    '    dt.Columns.Add("LABOR_AMOUNT", GetType(String))
    '    dt.Columns.Add("TRIP_AMOUNT", GetType(String))
    '    dt.Columns.Add("SHIPPING", GetType(String))
    '    dt.Columns.Add("APPROVED_QUOTE_DATE", GetType(String))
    '    dt.Columns.Add("INVOICE_NUMBER", GetType(String))
    '    dt.Columns.Add("INVOICE_PROCESSED_DATE", GetType(String))

    '    Dim rw As DataRow = dt.NewRow
    '    rw("CLAIM_ID") = GuidControl.GuidToHexString(claimObj.Id)
    '    rw("ITEM_NUMBER") = itemNumber
    '    rw("CLAIM_NUMBER") = claimObj.ClaimNumber
    '    rw("CLAIM_STATUS") = claimObj.StatusCode
    '    rw("REASON_CLOSE_CODE") = claimObj.ReasonClosedCode

    '    If Not dsClaim Is Nothing AndAlso Not dsClaim.Tables(ClaimAuthDetailDAL.TABLE_NAME) Is Nothing _
    '        AndAlso dsClaim.Tables(ClaimAuthDetailDAL.TABLE_NAME).Rows.Count > 0 Then
    '        If Not dsClaim.Tables(ClaimAuthDetailDAL.TABLE_NAME).Rows(0)(ClaimAuthDetailDAL.COL_NAME_LABOR_AMOUNT) Is DBNull.Value Then
    '            rw("LABOR_AMOUNT") = CType(dsClaim.Tables(ClaimAuthDetailDAL.TABLE_NAME).Rows(0)(ClaimAuthDetailDAL.COL_NAME_LABOR_AMOUNT), String)
    '        End If

    '        If Not dsClaim.Tables(ClaimAuthDetailDAL.TABLE_NAME).Rows(0)(ClaimAuthDetailDAL.COL_NAME_TRIP_AMOUNT) Is DBNull.Value Then
    '            rw("TRIP_AMOUNT") = CType(dsClaim.Tables(ClaimAuthDetailDAL.TABLE_NAME).Rows(0)(ClaimAuthDetailDAL.COL_NAME_TRIP_AMOUNT), String)
    '        End If

    '        If Not dsClaim.Tables(ClaimAuthDetailDAL.TABLE_NAME).Rows(0)(ClaimAuthDetailDAL.COL_NAME_SHIPPING_AMOUNT) Is DBNull.Value Then
    '            rw("SHIPPING") = CType(dsClaim.Tables(ClaimAuthDetailDAL.TABLE_NAME).Rows(0)(ClaimAuthDetailDAL.COL_NAME_SHIPPING_AMOUNT), String)
    '        End If

    '        If Not dsClaim.Tables(ClaimAuthDetailDAL.TABLE_NAME).Rows(0)(ClaimAuthDetailDAL.COL_NAME_CREATED_DATE) Is DBNull.Value Then
    '            rw("APPROVED_QUOTE_DATE") = CType(dsClaim.Tables(ClaimAuthDetailDAL.TABLE_NAME).Rows(0)(ClaimAuthDetailDAL.COL_NAME_CREATED_DATE), Date).ToString("s")
    '        End If
    '    End If

    '    Dim objCert As Certificate = New Certificate(claimObj.CertificateId)
    '    rw("INVOICE_NUMBER") = objCert.InvoiceNumber

    '    If Not claimObj.InvoiceProcessDate Is Nothing Then
    '        rw("INVOICE_PROCESSED_DATE") = CType(claimObj.InvoiceProcessDate, Date).ToString("s")
    '    End If

    '    dt.Rows.Add(rw)
    '    ds.Tables.Add(dt)

    '    Dim excludeTags As ArrayList = New ArrayList()
    '    excludeTags.Add("/TRANSACTION/TRANSACTION_DATA_RECORD/CLAIM_ID")


    '    ' Create PARTS_LIST data table for PartsInfo of the claim
    '    Dim dtPart As DataTable = New DataTable("PARTS_LIST")
    '    dtPart.Columns.Add(PartsInfoDAL.COL_NAME_PARTS_INFO_ID, GetType(String))
    '    dtPart.Columns.Add("CLAIM_ID", GetType(String))
    '    dtPart.Columns.Add("ITEM_NUMBER", GetType(String))
    '    dtPart.Columns.Add("CLAIM_NUMBER", GetType(String))
    '    dtPart.Columns.Add("PART_DESCRIPTION_CODE", GetType(String))
    '    dtPart.Columns.Add("PART_COST", GetType(String))

    '    ' Load PartsInfo to PARTS_LIST data table from database
    '    Dim dvPartInfo As PartsInfo.PartsInfoDV = PartsInfo.getSelectedList(claimObj.Id)
    '    If Not dvPartInfo Is Nothing AndAlso dvPartInfo.Count > 0 Then

    '        For Each dr As DataRowView In dvPartInfo
    '            Dim rwPart As DataRow = dtPart.NewRow
    '            itemNumber = itemNumber + 1
    '            rwPart(PartsInfoDAL.COL_NAME_PARTS_INFO_ID) = GuidControl.GuidToHexString(New Guid(CType(dr(PartsInfoDAL.COL_NAME_PARTS_INFO_ID), Byte())))
    '            rwPart("CLAIM_ID") = GuidControl.GuidToHexString(claimObj.Id)
    '            rwPart("ITEM_NUMBER") = itemNumber.ToString
    '            rwPart("CLAIM_NUMBER") = claimObj.ClaimNumber

    '            If Not dr(PartsInfoDAL.COL_NAME_PARTS_DESCRIPTION) Is DBNull.Value Then
    '                rwPart("PART_DESCRIPTION_CODE") = CType(dr(PartsInfoDAL.COL_NAME_PARTS_DESCRIPTION), String)
    '            End If

    '            If Not dr(PartsInfoDAL.COL_NAME_COST) Is DBNull.Value Then
    '                rwPart("PART_COST") = CType(dr(PartsInfoDAL.COL_NAME_COST), String)
    '            End If

    '            dtPart.Rows.Add(rwPart)
    '        Next
    '    End If

    '    ' Attach PartsInfo to the xml if there is any
    '    If Not dtPart Is Nothing AndAlso dtPart.Rows.Count > 0 Then
    '        ds.Tables.Add(dtPart)
    '        Dim claimPartRel As New DataRelation("CLAIM_PART_RELATION", _
    '                                 ds.Tables("TRANSACTION_DATA_RECORD").Columns("CLAIM_ID"), _
    '                                 ds.Tables("PARTS_LIST").Columns("CLAIM_ID"))
    '        claimPartRel.Nested = True
    '        ds.Relations.Add(claimPartRel)
    '        excludeTags.Add("/TRANSACTION/TRANSACTION_DATA_RECORD/PARTS_LIST/CLAIM_ID")
    '        excludeTags.Add("/TRANSACTION/TRANSACTION_DATA_RECORD/PARTS_LIST/" & PartsInfoDAL.COL_NAME_PARTS_INFO_ID)
    '    End If

    '    xml = XMLHelper.FromDatasetToXML(ds, excludeTags, False, True, Nothing, True)

    '    Return xml
    'End Function

    'Public Shared Function GetTransactionXML_NewClaim(ByVal claimObj As Claim, ByVal transactionId As Guid) As String
    '    Dim xml As String = ""
    '    Dim itemNumber As String = "1"
    '    Dim ds As DataSet = New DataSet("TRANSACTION")

    '    Dim dtHeader As DataTable = New DataTable("TRANSACTION_HEADER")
    '    dtHeader.Columns.Add("TRANSACTION_ID", GetType(String))
    '    dtHeader.Columns.Add("FUNCTION_TYPE", GetType(String))
    '    Dim rwHeader As DataRow = dtHeader.NewRow
    '    rwHeader(0) = GuidControl.GuidToHexString(transactionId)
    '    rwHeader(1) = DALObjects.TransactionLogHeaderDAL.FUNCTION_TYPE_CODE_GVS_NEW_CLAIM
    '    dtHeader.Rows.Add(rwHeader)
    '    ds.Tables.Add(dtHeader)

    '    Dim dt As DataTable = New DataTable("TRANSACTION_DATA_RECORD")

    '    dt.Columns.Add("ITEM_NUMBER", GetType(String))
    '    dt.Columns.Add("CLAIM_NUMBER", GetType(String))
    '    dt.Columns.Add("CREATED_DATE", GetType(String))
    '    dt.Columns.Add("SERVICE_CENTER_CODE", GetType(String))
    '    dt.Columns.Add("CUSTOMER_NAME", GetType(String))
    '    dt.Columns.Add("IDENTIFICATION_NUMBER", GetType(String))
    '    dt.Columns.Add("ADDRESS1", GetType(String))
    '    dt.Columns.Add("ADDRESS2", GetType(String))
    '    dt.Columns.Add("CITY", GetType(String))
    '    dt.Columns.Add("REGION", GetType(String))
    '    dt.Columns.Add("POSTAL_CODE", GetType(String))
    '    dt.Columns.Add("HOME_PHONE", GetType(String))
    '    dt.Columns.Add("WORK_PHONE", GetType(String))
    '    dt.Columns.Add("EMAIL", GetType(String))
    '    dt.Columns.Add("CONTACT_NAME", GetType(String))
    '    dt.Columns.Add("PRODUCT_CODE", GetType(String))
    '    dt.Columns.Add("DESCRIPTION", GetType(String))
    '    dt.Columns.Add("ITEM_DESCRIPTION", GetType(String))
    '    dt.Columns.Add("SERIAL_NUMBER", GetType(String))
    '    dt.Columns.Add("INVOICE_NUMBER", GetType(String))
    '    dt.Columns.Add("PROBLEM_DESCRIPTION", GetType(String))
    '    dt.Columns.Add("METHOD_OF_REPAIR_CODE", GetType(String))

    '    Dim rw As DataRow = dt.NewRow
    '    rw(0) = itemNumber
    '    rw(1) = claimObj.ClaimNumber
    '    rw(2) = CType(Date.Now, Date).ToString("s")
    '    rw(3) = claimObj.ServiceCenterObject.Code
    '    rw(4) = claimObj.CustomerName

    '    Dim objCert As Certificate = New Certificate(claimObj.CertificateId)
    '    rw(5) = objCert.IdentificationNumber

    '    If Not Guid.Empty.Equals(objCert.AddressId) Then
    '        Dim objAddr As Address = New Address(objCert.AddressId)
    '        rw(6) = objAddr.Address1
    '        rw(7) = objAddr.Address2
    '        rw(8) = objAddr.City

    '        If Not Guid.Empty.Equals(objAddr.RegionId) Then
    '            Dim objRegion As Region = New Region(objAddr.RegionId)
    '            rw(9) = objRegion.Description
    '        End If

    '        rw(10) = objAddr.PostalCode
    '    End If

    '    rw(11) = objCert.HomePhone
    '    rw(12) = objCert.WorkPhone
    '    rw(13) = objCert.Email
    '    rw(14) = claimObj.ContactName
    '    rw(15) = objCert.ProductCode
    '    rw(16) = objCert.GetProdCodeDesc

    '    Dim objCertItemCoverage As CertItemCoverage = New CertItemCoverage(claimObj.CertItemCoverageId)
    '    Dim objCertItem As CertItem = New CertItem(objCertItemCoverage.CertItemId)
    '    rw(17) = objCertItem.ItemDescription
    '    rw(18) = objCertItem.SerialNumber

    '    rw(19) = objCert.InvoiceNumber
    '    rw(20) = claimObj.ProblemDescription
    '    rw(21) = claimObj.MethodOfRepairCode

    '    dt.Rows.Add(rw)
    '    ds.Tables.Add(dt)

    '    xml = XMLHelper.FromDatasetToXML(ds, Nothing, False, True, Nothing, True)

    '    Return xml
    'End Function
#End Region

#Region "DataView Retrieveing Methods"

    Public Class ExceptionSearchDV
        Inherits DataView

        Public Const COL_TRANS_TMX_DEACTIVATE_ID As String = "trans_tmx_deactivate_id"
        Public Const COL_TRANS_TYPE As String = "trans_type"
        Public Const COL_TRANS_SCHEDULED_DATE As String = "trans_scheduled_date"
        Public Const COL_CERT_NUMBER As String = "cert_number"
        Public Const COL_ATTEMPT As String = "attempt"
        Public Const COL_MOBILE_NUMBER As String = "mobile_number"
        Public Const COL_SUPPLEMENTARY_SERVICE_CODE As String = "supplementary_service_code"
        Public Const COL_COMMERCIAL_SERVICE_CODE As String = "commercial_service_code"
        Public Const COL_ERROR_CODE As String = "error_code"
        Public Const COL_ERROR_MSG As String = "error_msg"
        Public Const COL_RESPONSE_XML As String = "response_xml"
        Public Const COL_RESPONSE_DATE As String = "response_date"
        Public Const COL_HIDE As String = "hide"
        Public Const COL_CREATED_DATE As String = "created_date"
        Public Const COL_CREATED_BY As String = "created_by"
        Public Const COL_MODIFIED_DATE As String = "modified_date"
        Public Const COL_MODIFIED_BY As String = "modified_by"

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Function AddNewRowToEmptyDV() As ExceptionSearchDV
            Dim dt As DataTable = Table.Clone()
            Dim row As DataRow = dt.NewRow
            row(ExceptionSearchDV.COL_TRANS_TMX_DEACTIVATE_ID) = (New Guid()).ToByteArray
            row(ExceptionSearchDV.COL_CERT_NUMBER) = 0
            row(ExceptionSearchDV.COL_MOBILE_NUMBER) = ""
            row(ExceptionSearchDV.COL_TRANS_TYPE) = ""
            row(ExceptionSearchDV.COL_TRANS_SCHEDULED_DATE) = DateTime.MinValue
            row(ExceptionSearchDV.COL_ATTEMPT) = 0
            row(ExceptionSearchDV.COL_ERROR_MSG) = ""
            dt.Rows.Add(Row)
            Return New ExceptionSearchDV(dt)
        End Function
    End Class

    Public Class TransactionStatusDV
        Inherits DataView

        Public Const COL_TRANS_TMX_DEACTIVATE_ID As String = "trans_tmx_deactivate_id"
        Public Const COL_ITEM_NUMBER As String = "item_number"
        Public Const COL_EXTENDED_STATUS_CODE As String = "extended_status_code"
        Public Const COL_EXTENDED_STATUS_DATE As String = "extended_status_date"
        Public Const COL_EXTENDED_STATUS_COMMENT As String = "extended_status_comment"

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared Sub AddNewRowToEmptyDV(ByRef dv As TransactionStatusDV)
            Dim dt As DataTable, blnEmptyTbl As Boolean = False
            Dim row As DataRow

            If dv Is Nothing Then
                Dim guidTemp As New Guid
                blnEmptyTbl = True
                dt = New DataTable
                dt.Columns.Add(TransactionStatusDV.COL_TRANS_TMX_DEACTIVATE_ID, Guid.Empty.ToByteArray.GetType)
                dt.Columns.Add(TransactionStatusDV.COL_ITEM_NUMBER, GetType(String))
                dt.Columns.Add(TransactionStatusDV.COL_EXTENDED_STATUS_CODE, GetType(String))
                dt.Columns.Add(TransactionStatusDV.COL_EXTENDED_STATUS_DATE, GetType(Date))
                dt.Columns.Add(TransactionStatusDV.COL_EXTENDED_STATUS_COMMENT, GetType(String))
            Else
                dt = dv.Table
            End If

            row = dt.NewRow
            row(TransactionStatusDV.COL_TRANS_TMX_DEACTIVATE_ID) = (New Guid()).ToByteArray
            row(TransactionStatusDV.COL_ITEM_NUMBER) = ""
            row(TransactionStatusDV.COL_EXTENDED_STATUS_CODE) = ""
            'row(TransactionStatusDV.COL_EXTENDED_STATUS_DATE) = ""
            row(TransactionStatusDV.COL_EXTENDED_STATUS_COMMENT) = ""
            dt.Rows.Add(row)

            If blnEmptyTbl Then dv = New TransactionStatusDV(dt)
        End Sub

    End Class

    Public Class TransactionPartDV
        Inherits DataView

        Public Const COL_TRANS_TMX_DEACTIVATE_ID As String = "trans_tmx_deactivate_id"
        Public Const COL_ITEM_NUMBER As String = "item_number"
        Public Const COL_PART_CODE As String = "part_code"
        Public Const COL_PART_COST As String = "part_cost"
        Public Const COL_PART_DEFECT As String = "part_defect"
        Public Const COL_IN_STOCK As String = "in_stock"

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        'Public Function AddNewRowToEmptyDV() As TransactionPartDV
        '    Dim dt As DataTable = Me.Table.Clone()
        '    Dim row As DataRow = dt.NewRow
        '    row(TransactionPartDV.COL_TRANSACTION_LOG_HEADER_ID) = (New Guid()).ToByteArray
        '    row(TransactionPartDV.COL_ITEM_NUMBER) = ""
        '    row(TransactionPartDV.COL_PART_CODE) = ""
        '    row(TransactionPartDV.COL_PART_COST) = ""
        '    row(TransactionPartDV.COL_PART_DEFECT) = ""
        '    row(TransactionPartDV.COL_IN_STOCK) = ""
        '    dt.Rows.Add(row)
        '    Return New TransactionPartDV(dt)
        'End Function

        Public Shared Sub AddNewRowToEmptyDV(ByRef dv As TransactionPartDV)
            Dim dt As DataTable, blnEmptyTbl As Boolean = False
            Dim row As DataRow

            If dv Is Nothing Then
                Dim guidTemp As New Guid
                blnEmptyTbl = True
                dt = New DataTable
                dt.Columns.Add(TransactionPartDV.COL_TRANS_TMX_DEACTIVATE_ID, Guid.Empty.ToByteArray.GetType)
                dt.Columns.Add(TransactionPartDV.COL_ITEM_NUMBER, GetType(String))
                dt.Columns.Add(TransactionPartDV.COL_PART_CODE, GetType(String))
                dt.Columns.Add(TransactionPartDV.COL_PART_COST, GetType(Decimal))
                dt.Columns.Add(TransactionPartDV.COL_PART_DEFECT, GetType(String))
                dt.Columns.Add(TransactionPartDV.COL_IN_STOCK, GetType(String))
            Else
                dt = dv.Table
            End If

            row = dt.NewRow
            row(TransactionPartDV.COL_TRANS_TMX_DEACTIVATE_ID) = (New Guid()).ToByteArray
            row(TransactionPartDV.COL_ITEM_NUMBER) = ""
            row(TransactionPartDV.COL_PART_CODE) = ""
            row(TransactionPartDV.COL_PART_COST) = ""
            row(TransactionPartDV.COL_PART_DEFECT) = ""
            row(TransactionPartDV.COL_IN_STOCK) = ""
            dt.Rows.Add(row)

            If blnEmptyTbl Then dv = New TransactionPartDV(dt)
        End Sub

    End Class

    Public Class TransactionDataDV
        Inherits DataView

        Public Const COL_TRANS_TMX_DEACTIVATE_ID As String = "trans_tmx_deactivate_id"
        Public Const COL_TRANS_TYPE_ID As String = "trans_type_id"
        Public Const COL_TRANS_SCHEDULED_DATE As String = "trans_scheduled_date"
        Public Const COL_CERT_ID As String = "cert_id"
        Public Const COL_ATTEMPT As String = "attempt"
        Public Const COL_MOBILE_NUMBER As String = "mobile_number"
        Public Const COL_SUPPLEMENTARY_SERVICE_CODE As String = "supplementary_service_code"
        Public Const COL_COMMERCIAL_SERVICE_CODE As String = "commercial_service_code"
        Public Const COL_ERROR_CODE As String = "error_code"
        Public Const COL_ERROR_MSG As String = "error_msg"
        Public Const COL_RESPONSE_XML As String = "response_xml"
        Public Const COL_RESPONSE_DATE As String = "response_date"
        Public Const COL_HIDE As String = "hide"
        Public Const COL_CREATED_DATE As String = "created_date"
        Public Const COL_CREATED_BY As String = "created_by"
        Public Const COL_MODIFIED_DATE As String = "modified_date"
        Public Const COL_MODIFIED_BY As String = "modified_by"

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub
    End Class

    Public Class TransactionFollowUpDV
        Inherits DataView

        Public Const COL_TRANS_TMX_DEACTIVATE_ID As String = "trans_tmx_deactivate_id"
        Public Const COL_ITEM_NUMBER As String = "item_number"
        Public Const COL_COMMENT_CREATED_DATE As String = "comment_created_date"
        Public Const COL_COMMENT_TYPE_CODE As String = "comment_type_code"
        Public Const COL_COMMENTS As String = "comments"
        Public Const COL_CALLER_NAME As String = "caller_name"

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared Sub AddNewRowToEmptyDV(ByRef dv As TransactionFollowUpDV)
            Dim dt As DataTable, blnEmptyTbl As Boolean = False
            Dim row As DataRow

            If dv Is Nothing Then
                Dim guidTemp As New Guid
                blnEmptyTbl = True
                dt = New DataTable
                dt.Columns.Add(TransactionFollowUpDV.COL_TRANS_TMX_DEACTIVATE_ID, Guid.Empty.ToByteArray.GetType)
                dt.Columns.Add(TransactionFollowUpDV.COL_ITEM_NUMBER, GetType(String))
                dt.Columns.Add(TransactionFollowUpDV.COL_COMMENT_CREATED_DATE, GetType(Date))
                dt.Columns.Add(TransactionFollowUpDV.COL_COMMENT_TYPE_CODE, GetType(String))
                dt.Columns.Add(TransactionFollowUpDV.COL_COMMENTS, GetType(String))
                dt.Columns.Add(TransactionFollowUpDV.COL_CALLER_NAME, GetType(String))
            Else
                dt = dv.Table
            End If

            row = dt.NewRow
            row(TransactionFollowUpDV.COL_TRANS_TMX_DEACTIVATE_ID) = (New Guid()).ToByteArray
            row(TransactionFollowUpDV.COL_ITEM_NUMBER) = ""
            'row(TransactionFollowUpDV.COL_COMMENT_CREATED_DATE) = ""
            row(TransactionFollowUpDV.COL_COMMENT_TYPE_CODE) = ""
            row(TransactionFollowUpDV.COL_COMMENTS) = ""
            row(TransactionFollowUpDV.COL_CALLER_NAME) = ""
            dt.Rows.Add(row)

            If blnEmptyTbl Then dv = New TransactionFollowUpDV(dt)
        End Sub

    End Class

    Public Shared Function IsTransactionExist(ByVal gvsOriginalTransID As String) As Boolean
        Dim objTransactionLogHeaderDAL As New TransactionLogHeaderDAL
        Dim ds As DataSet = objTransactionLogHeaderDAL.IsTransactionExist(gvsOriginalTransID)
        If Not ds Is Nothing AndAlso ds.Tables.Count > 0 AndAlso ds.Tables(0).Rows.Count > 0 Then
            If CType(ds.Tables(0).Rows(0)(0), Integer) > 0 Then
                Return True
            Else
                Return False
            End If
        Else
            Throw New ElitaPlusException("TransactionLogHeader.IsTransactionExist Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End If

    End Function

    Public Shared Function GetTransactionExceptionHeader() As DataSet
        Dim objTransactionLogHeaderDAL As New TransactionLogHeaderDAL
        Dim ds As DataSet = objTransactionLogHeaderDAL.GetTransactionExceptionHeader(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
        Return ds
    End Function

    Public Shared Function GetLastSuccessfulTransmissionTime() As DateTimeType
        Dim objTransactionLogHeaderDAL As New TransactionLogHeaderDAL
        Return objTransactionLogHeaderDAL.GetLastSuccessfulTransmissionTime(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
    End Function

    Public Shared Function GetLastSuccessfulTransmissionTimeByType() As DataView

        Try
            Dim objCancellationReqExceptionDAL As New CancellationReqExceptionDAL
            Return objCancellationReqExceptionDAL.GetLastSuccessfulTransmissionTimeByType(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function CheckLastSuccessfulTransmissionTimeByType(Optional ByVal NotifyInterval As Integer = PARAM_NOTIFY_INTERVAL) As DataView

        Dim objTransactionLogHeaderDAL As New TransactionLogHeaderDAL
        Dim dv As DataView
        Dim bValidElita As Boolean = False
        Dim bValidGVS As Boolean = False
        Dim transDateElita, transDateGVS As DateTime
        Dim transDateElitaStr, transDateGVSStr As String

        Try
            dv = objTransactionLogHeaderDAL.GetLastSuccessfulTransmissionTimeByType(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

        'Check Returned records for valid dates
        For Each dItem As DataRowView In dv
            If Not dItem(objTransactionLogHeaderDAL.COL_NAME_TRANSACTION_STATUS_SYSTEM) Is Nothing AndAlso dItem(objTransactionLogHeaderDAL.COL_NAME_TRANSACTION_STATUS_SYSTEM).ToString = TransactionLogHeader.SYSTEM_TYPE_ELITA Then
                If Not dItem(objTransactionLogHeaderDAL.COL_NAME_TRANSACTION_STATUS_DATE) Is Nothing AndAlso IsDate(dItem(objTransactionLogHeaderDAL.COL_NAME_TRANSACTION_STATUS_DATE)) Then
                    transDateElita = CType(dItem(objTransactionLogHeaderDAL.COL_NAME_TRANSACTION_STATUS_DATE), DateTime)
                    If transDateElita > DateAdd(DateInterval.Hour, NotifyInterval * -1, Now) Then
                        bValidElita = True
                        bValidGVS = True
                    ElseIf transDateElita = DateTime.MinValue Then
                        transDateElitaStr = TranslationBase.TranslateLabelOrMessage("GREATERTHAN5", ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                    Else
                        transDateElitaStr = transDateElita.ToString("dd-MMM-yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    End If
                Else
                    transDateElitaStr = TranslationBase.TranslateLabelOrMessage("GREATERTHAN5", ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                End If
            ElseIf Not dItem(objTransactionLogHeaderDAL.COL_NAME_TRANSACTION_STATUS_SYSTEM) Is Nothing AndAlso dItem(objTransactionLogHeaderDAL.COL_NAME_TRANSACTION_STATUS_SYSTEM).ToString = TransactionLogHeader.SYSTEM_TYPE_GVS Then
                If Not dItem(objTransactionLogHeaderDAL.COL_NAME_TRANSACTION_STATUS_DATE) Is Nothing AndAlso IsDate(dItem(objTransactionLogHeaderDAL.COL_NAME_TRANSACTION_STATUS_DATE)) Then
                    transDateGVS = CType(dItem(objTransactionLogHeaderDAL.COL_NAME_TRANSACTION_STATUS_DATE), DateTime)
                    If transDateGVS > DateAdd(DateInterval.Hour, NotifyInterval * -1, Now) Then
                        bValidGVS = True
                    ElseIf transDateGVS = DateTime.MinValue Then
                        transDateGVSStr = TranslationBase.TranslateLabelOrMessage("GREATERTHAN5", ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                    Else
                        transDateGVSStr = transDateGVS.ToString("dd-MMM-yyyy", System.Globalization.CultureInfo.CurrentCulture)
                    End If
                Else
                    transDateGVSStr = TranslationBase.TranslateLabelOrMessage("GREATERTHAN5", ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                End If
            End If
        Next

        Try
            'Check status values and act as necessary.
            If Not bValidElita Or Not bValidGVS Then
                Dim msg As System.Net.Mail.MailMessage
                Dim mail As System.Net.Mail.SmtpClient

                If Not bValidGVS Then
                    mail = New System.Net.Mail.SmtpClient(AppConfig.ServiceOrderEmail.SmtpServer)
                    msg = New System.Net.Mail.MailMessage
                    msg.To.Add(String.Format("{0}", If(EnvironmentContext.Current.Environment = Environments.Production, ElitaPlusIdentity.Current.ActiveUser.Company.Email, TEST_EMAIL).ToString))
                    msg.From = New System.Net.Mail.MailAddress(ElitaPlusIdentity.Current.ActiveUser.Company.Email)
                    msg.Subject = TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.MSG_TRANSACTION_LOG_GVS_OVERDUE_SUBJ)
                    msg.Body = String.Format(TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.MSG_TRANSACTION_LOG_GVS_OVERDUE_BODY), Now.ToString("dd-MMM-yyyy", System.Globalization.CultureInfo.CurrentCulture), transDateGVSStr)
                    mail.Send(msg)
                End If

                If Not bValidElita Then
                    mail = New System.Net.Mail.SmtpClient(AppConfig.ServiceOrderEmail.SmtpServer)
                    msg = New System.Net.Mail.MailMessage
                    msg.To.Add(String.Format("{0}", If(EnvironmentContext.Current.Environment = Environments.Production, ElitaPlusIdentity.Current.ActiveUser.Company.Email, TEST_EMAIL).ToString))
                    msg.From = New System.Net.Mail.MailAddress(ElitaPlusIdentity.Current.ActiveUser.Company.Email)
                    msg.Subject = TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.MSG_TRANSACTION_LOG_ELITA_OVERDUE_SUBJ)
                    msg.Body = String.Format(TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.MSG_TRANSACTION_LOG_ELITA_OVERDUE_BODY), Now.ToString("dd-MMM-yyyy", System.Globalization.CultureInfo.CurrentCulture), transDateElitaStr)
                    mail.Send(msg)
                End If

            End If
        Catch ex As Exception
            Throw ex
        End Try

        Return dv

    End Function

    Public Shared Function GetExceptionList(ByVal TransactionType As String, ByVal MobileNumber As String, ByVal langId As Guid, ByVal transDateFrom As Date, ByVal transDateTo As Date, ByVal errorCode As String) As ExceptionSearchDV
        Try
            Dim dal As New CancellationReqExceptionDAL
            Return New ExceptionSearchDV(dal.GetExceptionList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, TransactionType, MobileNumber, langId, transDateFrom, transDateTo, errorCode).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetStatusList(ByVal transactionLogHeaderId As String) As TransactionStatusDV
        Try
            Dim dal As New TransactionLogHeaderDAL
            Return New TransactionStatusDV(dal.GetStatusList(transactionLogHeaderId, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetPartList(ByVal transactionLogHeaderId As String) As TransactionPartDV
        Try
            Dim dal As New TransactionLogHeaderDAL
            Return New TransactionPartDV(dal.GetPartList(transactionLogHeaderId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetFollowUpList(ByVal transactionLogHeaderId As String) As TransactionFollowUpDV
        Try
            Dim dal As New TransactionLogHeaderDAL
            Return New TransactionFollowUpDV(dal.GetFollowUpList(transactionLogHeaderId, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetTransactionData(ByVal transactionLogHeaderId As String) As TransactionDataDV
        Try
            Dim dal As New TransactionLogHeaderDAL
            Return New TransactionDataDV(dal.GetTransactionData(transactionLogHeaderId, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function ResendOrHideTransaction(ByVal cmd As String, ByVal transLogHeaderId As Guid, Optional ByVal newComunaValue As String = Nothing) As DBHelper.DBHelperParameter()
        Try
            Dim dal As New TransactionLogHeaderDAL
            Return dal.ResendOrHideTransaction(cmd, transLogHeaderId, newComunaValue)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function ProcessRecords(ByVal cmd As String, ByVal transLogHeaderIds As String) As DBHelper.DBHelperParameter()
        Try
            Dim dal As New CancellationReqExceptionDAL
            Return dal.ProcessRecords(cmd, transLogHeaderIds)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function HideRecords(ByVal cmd As String, ByVal transLogHeaderIds As String) As DBHelper.DBHelperParameter()
        Try
            Dim dal As New CancellationReqExceptionDAL
            Return dal.ProcessRecords(cmd, transLogHeaderIds)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetRejectionMessage(ByVal compGroupId As Guid, ByVal transLogHeaderId As Guid) As String
        Try
            Dim dal As New TransactionLogHeaderDAL
            Return dal.GetRejectionMessage(compGroupId, transLogHeaderId)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function


    'Public Shared Function UpdateComuna(ByVal transLogHeaderId As Guid, ByVal newComunaValue As String) As Boolean
    '    Try
    '        Dim dal As New TransactionLogHeaderDAL
    '        Return dal.UpdateComuna(transLogHeaderId, newComunaValue)
    '    Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
    '        Throw New DataBaseAccessException(ex.ErrorType, ex)
    '    End Try
    'End Function


#End Region

End Class


