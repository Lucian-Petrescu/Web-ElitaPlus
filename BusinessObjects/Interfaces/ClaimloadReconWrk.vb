'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (4/2/2010)  ********************

Public Class ClaimloadReconWrk
    Inherits BusinessObjectBase
    Implements IFileLoadReconWork

#Region "Constants"

    Public Const COL_NAME_CLAIMLOAD_RECON_WRK_ID As String = ClaimloadReconWrkDAL.COL_NAME_CLAIMLOAD_RECON_WRK_ID
    Public Const COL_NAME_CLAIMLOAD_FILE_PROCESSED_ID As String = ClaimloadReconWrkDAL.COL_NAME_CLAIMLOAD_FILE_PROCESSED_ID
    Public Const COL_NAME_REJECT_CODE As String = ClaimloadReconWrkDAL.COL_NAME_REJECT_CODE
    Public Const COL_NAME_REJECT_REASON As String = ClaimloadReconWrkDAL.COL_NAME_REJECT_REASON
    Public Const COL_NAME_CLAIM_LOADED As String = ClaimloadReconWrkDAL.COL_NAME_CLAIM_LOADED
    Public Const COL_NAME_DEALER_ID As String = ClaimloadReconWrkDAL.COL_NAME_DEALER_ID
    Public Const COL_NAME_RECORD_TYPE As String = ClaimloadReconWrkDAL.COL_NAME_RECORD_TYPE
    Public Const COL_NAME_DEALER_CODE As String = ClaimloadReconWrkDAL.COL_NAME_DEALER_CODE
    Public Const COL_NAME_CERTIFICATE As String = ClaimloadReconWrkDAL.COL_NAME_CERTIFICATE
    Public Const COL_NAME_CLAIM_TYPE As String = ClaimloadReconWrkDAL.COL_NAME_CLAIM_TYPE
    Public Const COL_NAME_AUTHORIZATION_NUM As String = ClaimloadReconWrkDAL.COL_NAME_AUTHORIZATION_NUM
    Public Const COL_NAME_EXTERNAL_CREATED_DATE As String = ClaimloadReconWrkDAL.COL_NAME_EXTERNAL_CREATED_DATE
    Public Const COL_NAME_COVERAGE_CODE As String = ClaimloadReconWrkDAL.COL_NAME_COVERAGE_CODE
    Public Const COL_NAME_DATE_OF_LOSS As String = ClaimloadReconWrkDAL.COL_NAME_DATE_OF_LOSS
    Public Const COL_NAME_CAUSE_OF_LOSS As String = ClaimloadReconWrkDAL.COL_NAME_CAUSE_OF_LOSS
    Public Const COL_NAME_PROBLEM_DESCRIPTION As String = ClaimloadReconWrkDAL.COL_NAME_PROBLEM_DESCRIPTION
    Public Const COL_NAME_COMMENTS As String = ClaimloadReconWrkDAL.COL_NAME_COMMENTS
    Public Const COL_NAME_SPECIAL_INSTRUCTIONS As String = ClaimloadReconWrkDAL.COL_NAME_SPECIAL_INSTRUCTIONS
    Public Const COL_NAME_REASON_CLOSED As String = ClaimloadReconWrkDAL.COL_NAME_REASON_CLOSED
    Public Const COL_NAME_MANUFACTURER As String = ClaimloadReconWrkDAL.COL_NAME_MANUFACTURER
    Public Const COL_NAME_MODEL As String = ClaimloadReconWrkDAL.COL_NAME_MODEL
    Public Const COL_NAME_SERIAL_NUMBER As String = ClaimloadReconWrkDAL.COL_NAME_SERIAL_NUMBER
    Public Const COL_NAME_SERVICE_CENTER_CODE As String = ClaimloadReconWrkDAL.COL_NAME_SERVICE_CENTER_CODE
    Public Const COL_NAME_INVOICE_NUMBER As String = ClaimloadReconWrkDAL.COL_NAME_INVOICE_NUMBER
    Public Const COL_NAME_AMOUNT As String = ClaimloadReconWrkDAL.COL_NAME_AMOUNT
    Public Const COL_NAME_ESTIMATE_AMOUNT As String = ClaimloadReconWrkDAL.COL_NAME_ESTIMATE_AMOUNT
    Public Const COL_NAME_PRODUCT_CODE As String = ClaimloadReconWrkDAL.COL_NAME_PRODUCT_CODE
    Public Const COL_NAME_DEFECT_REASON As String = ClaimloadReconWrkDAL.COL_NAME_DEFECT_REASON
    Public Const COL_NAME_REPAIR_CODE As String = ClaimloadReconWrkDAL.COL_NAME_REPAIR_CODE
    Public Const COL_NAME_CALLER_NAME As String = ClaimloadReconWrkDAL.COL_NAME_CALLER_NAME
    Public Const COL_NAME_CONTACT_NAME As String = ClaimloadReconWrkDAL.COL_NAME_CONTACT_NAME
    Public Const COL_NAME_CREATED_BY As String = ClaimloadReconWrkDAL.COL_NAME_CREATED_BY
    Public Const COL_NAME_CREATED_DATE As String = ClaimloadReconWrkDAL.COL_NAME_CREATED_DATE
    Public Const COL_NAME_MODIFIED_BY As String = ClaimloadReconWrkDAL.COL_NAME_MODIFIED_BY
    Public Const COL_NAME_MODIFIED_DATE As String = ClaimloadReconWrkDAL.COL_NAME_MODIFIED_DATE
    Public Const COL_NAME_REPAIR_DATE As String = ClaimloadReconWrkDAL.COL_NAME_REPAIR_DATE
    Public Const COL_NAME_VISIT_DATE As String = ClaimloadReconWrkDAL.COL_NAME_VISIT_DATE
    Public Const COL_NAME_INVOICE_DATE As String = ClaimloadReconWrkDAL.COL_NAME_INVOICE_DATE
    Public Const COL_NAME_PICKUP_DATE As String = ClaimloadReconWrkDAL.COL_NAME_PICKUP_DATE
    Public Const COL_NAME_CERT_ID As String = ClaimloadReconWrkDAL.COL_NAME_CERT_ID
    Public Const COL_NAME_CERT_ITEM_COV_ID As String = ClaimloadReconWrkDAL.COL_NAME_CERT_ITEM_COV_ID
    Public Const COL_NAME_ORIGINAL_CLAIM_ID As String = ClaimloadReconWrkDAL.COL_NAME_ORIGINAL_CLAIM_ID
    Public Const COL_NAME_SERVICE_CENTER_ID As String = ClaimloadReconWrkDAL.COL_NAME_SERVICE_CENTER_ID
    Public Const COL_NAME_POLICE_REPORT_NUM As String = ClaimloadReconWrkDAL.COL_NAME_POLICE_REPORT_NUM
    Public Const COL_NAME_OFFICER_NAME As String = ClaimloadReconWrkDAL.COL_NAME_OFFICER_NAME
    Public Const COL_NAME_POLICE_STATION_CODE As String = ClaimloadReconWrkDAL.COL_NAME_POLICE_STATION_CODE
    Public Const COL_NAME_DOCUMENT_TYPE As String = ClaimloadReconWrkDAL.COL_NAME_DOCUMENT_TYPE
    Public Const COL_NAME_RG_NUMBER As String = ClaimloadReconWrkDAL.COL_NAME_RG_NUMBER
    Public Const COL_NAME_DOCUMENT_AGENCY As String = ClaimloadReconWrkDAL.COL_NAME_DOCUMENT_AGENCY
    Public Const COL_NAME_DOCUMENT_ISSUE_DATE As String = ClaimloadReconWrkDAL.COL_NAME_DOCUMENT_ISSUE_DATE
    Public Const COL_NAME_ID_TYPE As String = ClaimloadReconWrkDAL.COL_NAME_ID_TYPE
    Public Const COL_NAME_REJECT_MSG_PARMS As String = ClaimloadReconWrkDAL.COL_NAME_REJECT_MSG_PARMS
    Public Const COL_NAME_DEVICE_RECEPTION_DATE As String = ClaimloadReconWrkDAL.COL_NAME_DEVICE_RECEPTION_DATE
    Public Const COL_NAME_REPLACEMENT_TYPE As String = ClaimloadReconWrkDAL.COL_NAME_REPLACEMENT_TYPE
    Public Const COL_NAME_REPLACEMENT_MANUFACTURER As String = ClaimloadReconWrkDAL.COL_NAME_REPLACEMENT_MANUFACTURER
    Public Const COL_NAME_REPLACEMENT_MODEL As String = ClaimloadReconWrkDAL.COL_NAME_REPLACEMENT_MODEL
    Public Const COL_NAME_REPLACEMENT_SERIAL_NUMBER As String = ClaimloadReconWrkDAL.COL_NAME_REPLACEMENT_SERIAL_NUMBER
    Public Const COL_NAME_REPLACEMENT_SKU As String = ClaimloadReconWrkDAL.COL_NAME_REPLACEMENT_SKU
    Public Const COL_NAME_DEDUCTIBLE_COLLECTED As String = ClaimloadReconWrkDAL.COL_NAME_DEDUCTIBLE_COLLECTED
    Public Const COL_NAME_LABOR_AMOUNT As String = ClaimloadReconWrkDAL.COL_NAME_LABOR_AMOUNT
    Public Const COL_NAME_PARTS_AMOUNT As String = ClaimloadReconWrkDAL.COL_NAME_PARTS_AMOUNT
    Public Const COL_NAME_SERVICE_AMOUNT As String = ClaimloadReconWrkDAL.COL_NAME_SERVICE_AMOUNT
    Public Const COL_NAME_SHIPPING_AMOUNT As String = ClaimloadReconWrkDAL.COL_NAME_SHIPPING_AMOUNT
    Public Const COL_NAME_PART_1_SKU As String = ClaimloadReconWrkDAL.COL_NAME_PART_1_SKU
    Public Const COL_NAME_PART_1_DESCRIPTION As String = ClaimloadReconWrkDAL.COL_NAME_PART_1_DESCRIPTION
    Public Const COL_NAME_PART_2_SKU As String = ClaimloadReconWrkDAL.COL_NAME_PART_2_SKU
    Public Const COL_NAME_PART_2_DESCRIPTION As String = ClaimloadReconWrkDAL.COL_NAME_PART_2_DESCRIPTION
    Public Const COL_NAME_PART_3_SKU As String = ClaimloadReconWrkDAL.COL_NAME_PART_3_SKU
    Public Const COL_NAME_PART_3_DESCRIPTION As String = ClaimloadReconWrkDAL.COL_NAME_PART_3_DESCRIPTION
    Public Const COL_NAME_PART_4_SKU As String = ClaimloadReconWrkDAL.COL_NAME_PART_4_SKU
    Public Const COL_NAME_PART_4_DESCRIPTION As String = ClaimloadReconWrkDAL.COL_NAME_PART_4_DESCRIPTION
    Public Const COL_NAME_PART_5_SKU As String = ClaimloadReconWrkDAL.COL_NAME_PART_5_SKU
    Public Const COL_NAME_PART_5_DESCRIPTION As String = ClaimloadReconWrkDAL.COL_NAME_PART_5_DESCRIPTION
    Public Const COL_NAME_SERVICE_LEVEL As String = ClaimloadReconWrkDAL.COL_NAME_SERVICE_LEVEL
    Public Const COL_NAME_DEALER_REFERENCE As String = ClaimloadReconWrkDAL.COL_NAME_DEALER_REFERENCE
    Public Const COL_NAME_POS As String = ClaimloadReconWrkDAL.COL_NAME_POS
    Public Const COL_NAME_PROBLEM_FOUND As String = ClaimloadReconWrkDAL.COL_NAME_PROBLEM_FOUND
    Public Const COL_NAME_FINAL_STATUS As String = ClaimloadReconWrkDAL.COL_NAME_FINAL_STATUS
    Public Const COL_NAME_TECHNICAL_REPORT As String = ClaimloadReconWrkDAL.COL_NAME_TECHNICAL_REPORT
    Public Const COL_NAME_DELIVERY_DATE As String = ClaimloadReconWrkDAL.COL_NAME_DELIVERY_DATE
    Public Const COL_NAME_BATCH_NUMBER As String = ClaimloadReconWrkDAL.COL_NAME_BATCH_NUMBER
    Public Const COL_NAME_INCOMING_ENTIRE_RECORD As String = ClaimloadReconWrkDAL.COL_NAME_INCOMING_ENTIRE_RECORD
    Public Const COL_NAME_COMMENT_TYPE_ID As String = ClaimloadReconWrkDAL.COL_NAME_COMMENT_TYPE_ID
    Public Const COL_NAME_COMMENT_TYPE As String = ClaimloadReconWrkDAL.COL_NAME_COMMENT_TYPE
    Public Const COL_NAME_TRACKING_NUMBER As String = ClaimloadReconWrkDAL.COL_NAME_TRACKING_NUMBER
    Public Const COL_NAME_RISK_TYPE_ENGLISH As String = ClaimloadReconWrkDAL.COL_NAME_RISK_TYPE_ENGLISH
    Public Const COL_NAME_ITEM_DESCRIPTION As String = ClaimloadReconWrkDAL.COL_NAME_ITEM_DESCRIPTION
    Public Const COL_NAME_TRIP_AMOUNT As String = ClaimloadReconWrkDAL.COL_NAME_TRIP_AMOUNT
    Public Const COL_NAME_OTHER_AMOUNT As String = ClaimloadReconWrkDAL.COL_NAME_OTHER_AMOUNT
    Public Const COL_NAME_OTHER_EXPLANATION As String = ClaimloadReconWrkDAL.COL_NAME_OTHER_EXPLANATION
    Public Const COL_NAME_CLAIM_NUMBER As String = ClaimloadReconWrkDAL.COL_NAME_CLAIM_NUMBER
    Public Const COL_NAME_EXTENDED_STATUS_CODE As String = ClaimloadReconWrkDAL.COL_NAME_EXTENDED_STATUS_CODE
    Public Const COL_NAME_EXTENDED_STATUS_DATE As String = ClaimloadReconWrkDAL.COL_NAME_EXTENDED_STATUS_DATE
    Public Const COL_NAME_TRACKING_NUM_TO_SC As String = ClaimloadReconWrkDAL.COL_NAME_TRACKING_NUM_TO_SC
    Public Const COL_NAME_TRACKING_NUM_TO_CUST As String = ClaimloadReconWrkDAL.COL_NAME_TRACKING_NUM_TO_CUST
    Public Const COL_NAME_DEVICE_SHIPPED_TO_SC_DATE As String = ClaimloadReconWrkDAL.COL_NAME_DEVICE_SHIPPED_TO_SC_DATE

    Public Const COL_NAME_DEDUCTILE_INCLUDED As String = ClaimloadReconWrkDAL.COL_NAME_Deductile_Included
#End Region

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Dataset = New DataSet
        Load(id)
    End Sub

    Public Sub New(ByVal id As Guid, ByVal sModifiedDate As String)
        MyBase.New()
        Dataset = New DataSet
        Load(id)
        VerifyConcurrency(sModifiedDate)
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
            Dim dal As New ClaimloadReconWrkDAL
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
            Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New ClaimloadReconWrkDAL
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
#End Region

#Region "Private Members"
    'Initialization code for new objects
    Private Sub Initialize()
    End Sub
#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid Implements IFileLoadReconWork.Id
        Get
            If Row(ClaimloadReconWrkDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimloadReconWrkDAL.COL_NAME_CLAIMLOAD_RECON_WRK_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property ClaimloadFileProcessedId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_CLAIMLOAD_FILE_PROCESSED_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimloadReconWrkDAL.COL_NAME_CLAIMLOAD_FILE_PROCESSED_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_CLAIMLOAD_FILE_PROCESSED_ID, Value)
        End Set
    End Property

    Public ReadOnly Property ParentId As Guid Implements IFileLoadReconWork.ParentId
        Get
            Return ClaimloadFileProcessedId
        End Get
    End Property


    <ValidStringLength("", Max:=5)> _
    Public Property RejectCode() As String Implements IFileLoadReconWork.RejectCode
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_REJECT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_REJECT_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_REJECT_CODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=60)> _
    Public Property RejectReason() As String Implements IFileLoadReconWork.RejectReason
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_REJECT_REASON) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_REJECT_REASON), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_REJECT_REASON, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1)> _
    Public Property ClaimLoaded() As String Implements IFileLoadReconWork.Loaded
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_CLAIM_LOADED) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_CLAIM_LOADED), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_CLAIM_LOADED, Value)
        End Set
    End Property

    Public Property DealerId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimloadReconWrkDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=2)> _
    Public Property RecordType() As String Implements IFileLoadReconWork.RecordType
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_RECORD_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_RECORD_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_RECORD_TYPE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=5)> _
    Public Property DealerCode() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_DEALER_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_DEALER_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_DEALER_CODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=20)> _
    Public Property Certificate() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_CERTIFICATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_CERTIFICATE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_CERTIFICATE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=4)> _
    Public Property ClaimType() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_CLAIM_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_CLAIM_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_CLAIM_TYPE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=10)> _
    Public Property AuthorizationNum() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_AUTHORIZATION_NUM) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_AUTHORIZATION_NUM), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_AUTHORIZATION_NUM, Value)
        End Set
    End Property



    Public Property ExternalCreatedDate() As DateType
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_EXTERNAL_CREATED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ClaimloadReconWrkDAL.COL_NAME_EXTERNAL_CREATED_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_EXTERNAL_CREATED_DATE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=4)> _
    Public Property CoverageCode() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_COVERAGE_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_COVERAGE_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_COVERAGE_CODE, Value)
        End Set
    End Property



    Public Property DateOfLoss() As DateType
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_DATE_OF_LOSS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ClaimloadReconWrkDAL.COL_NAME_DATE_OF_LOSS), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_DATE_OF_LOSS, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=30)> _
    Public Property CauseOfLoss() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_CAUSE_OF_LOSS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_CAUSE_OF_LOSS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_CAUSE_OF_LOSS, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=125)> _
    Public Property ProblemDescription() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_PROBLEM_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_PROBLEM_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_PROBLEM_DESCRIPTION, Value)
        End Set
    End Property

    'DEF-2589. Max length changed to 1000
    <ValidStringLength("", Max:=1000)> _
    Public Property Comments() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_COMMENTS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_COMMENTS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_COMMENTS, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=125)> _
    Public Property SpecialInstructions() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_SPECIAL_INSTRUCTIONS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_SPECIAL_INSTRUCTIONS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_SPECIAL_INSTRUCTIONS, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=5)> _
    Public Property ReasonClosed() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_REASON_CLOSED) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_REASON_CLOSED), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_REASON_CLOSED, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)> _
    Public Property Manufacturer() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_MANUFACTURER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_MANUFACTURER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_MANUFACTURER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=100)>
    Public Property Model() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_MODEL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_MODEL), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_MODEL, Value)
        End Set
    End Property

    'Def-25138: Increased the max length of SerialNumber validation attribute to 30.
    <ValidStringLength("", Max:=30)> _
    Public Property SerialNumber() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_SERIAL_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_SERIAL_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_SERIAL_NUMBER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=16)> _
    Public Property ServiceCenterCode() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_SERVICE_CENTER_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_SERVICE_CENTER_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_SERVICE_CENTER_CODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=20)> _
    Public Property InvoiceNumber() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_INVOICE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_INVOICE_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_INVOICE_NUMBER, Value)
        End Set
    End Property



    Public Property Amount() As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ClaimloadReconWrkDAL.COL_NAME_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_AMOUNT, Value)
        End Set
    End Property



    Public Property EstimateAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_ESTIMATE_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ClaimloadReconWrkDAL.COL_NAME_ESTIMATE_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_ESTIMATE_AMOUNT, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=5)> _
    Public Property ProductCode() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_PRODUCT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_PRODUCT_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_PRODUCT_CODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)> _
    Public Property DefectReason() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_DEFECT_REASON) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_DEFECT_REASON), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_DEFECT_REASON, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)> _
    Public Property RepairCode() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_REPAIR_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_REPAIR_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_REPAIR_CODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)> _
    Public Property CallerName() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_CALLER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_CALLER_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_CALLER_NAME, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)> _
    Public Property ContactName() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_CONTACT_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_CONTACT_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_CONTACT_NAME, Value)
        End Set
    End Property



    Public Property RepairDate() As DateType
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_REPAIR_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ClaimloadReconWrkDAL.COL_NAME_REPAIR_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_REPAIR_DATE, Value)
        End Set
    End Property



    Public Property VisitDate() As DateType
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_VISIT_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ClaimloadReconWrkDAL.COL_NAME_VISIT_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_VISIT_DATE, Value)
        End Set
    End Property



    Public Property PickupDate() As DateType
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_PICKUP_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ClaimloadReconWrkDAL.COL_NAME_PICKUP_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_PICKUP_DATE, Value)
        End Set
    End Property



    Public Property CertId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_CERT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimloadReconWrkDAL.COL_NAME_CERT_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_CERT_ID, Value)
        End Set
    End Property



    Public Property CertItemCovId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_CERT_ITEM_COV_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimloadReconWrkDAL.COL_NAME_CERT_ITEM_COV_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_CERT_ITEM_COV_ID, Value)
        End Set
    End Property



    Public Property OriginalClaimId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_ORIGINAL_CLAIM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimloadReconWrkDAL.COL_NAME_ORIGINAL_CLAIM_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_ORIGINAL_CLAIM_ID, Value)
        End Set
    End Property



    Public Property ServiceCenterId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_SERVICE_CENTER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimloadReconWrkDAL.COL_NAME_SERVICE_CENTER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_SERVICE_CENTER_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)> _
    Public Property PoliceReportNum() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_POLICE_REPORT_NUM) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_POLICE_REPORT_NUM), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_POLICE_REPORT_NUM, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200)> _
    Public Property OfficerName() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_OFFICER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_OFFICER_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_OFFICER_NAME, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200)> _
    Public Property PoliceStationCode() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_POLICE_STATION_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_POLICE_STATION_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_POLICE_STATION_CODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=4)> _
    Public Property DocumentType() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_DOCUMENT_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_DOCUMENT_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_DOCUMENT_TYPE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=20)> _
    Public Property RgNumber() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_RG_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_RG_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_RG_NUMBER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=15)> _
    Public Property DocumentAgency() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_DOCUMENT_AGENCY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_DOCUMENT_AGENCY), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_DOCUMENT_AGENCY, Value)
        End Set
    End Property



    Public Property DocumentIssueDate() As DateType
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_DOCUMENT_ISSUE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ClaimloadReconWrkDAL.COL_NAME_DOCUMENT_ISSUE_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_DOCUMENT_ISSUE_DATE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=10)> _
    Public Property IdType() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_ID_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_ID_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_ID_TYPE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=100)> _
    Public Property RejectMsgParms() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_REJECT_MSG_PARMS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_REJECT_MSG_PARMS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_REJECT_MSG_PARMS, Value)
        End Set
    End Property



    Public Property DeviceReceptionDate() As DateType
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_DEVICE_RECEPTION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ClaimloadReconWrkDAL.COL_NAME_DEVICE_RECEPTION_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_DEVICE_RECEPTION_DATE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=3)> _
    Public Property ReplacementType() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_REPLACEMENT_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_REPLACEMENT_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_REPLACEMENT_TYPE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=255)> _
    Public Property ReplacementManufacturer() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_REPLACEMENT_MANUFACTURER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_REPLACEMENT_MANUFACTURER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_REPLACEMENT_MANUFACTURER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=100)>
    Public Property ReplacementModel() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_REPLACEMENT_MODEL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_REPLACEMENT_MODEL), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_REPLACEMENT_MODEL, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=30)> _
    Public Property ReplacementSerialNumber() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_REPLACEMENT_SERIAL_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_REPLACEMENT_SERIAL_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_REPLACEMENT_SERIAL_NUMBER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=18)> _
    Public Property ReplacementSku() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_REPLACEMENT_SKU) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_REPLACEMENT_SKU), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_REPLACEMENT_SKU, Value)
        End Set
    End Property



    Public Property DeductibleCollected() As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_DEDUCTIBLE_COLLECTED) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ClaimloadReconWrkDAL.COL_NAME_DEDUCTIBLE_COLLECTED), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_DEDUCTIBLE_COLLECTED, Value)
        End Set
    End Property



    Public Property LaborAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_LABOR_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ClaimloadReconWrkDAL.COL_NAME_LABOR_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_LABOR_AMOUNT, Value)
        End Set
    End Property



    Public Property PartsAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_PARTS_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ClaimloadReconWrkDAL.COL_NAME_PARTS_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_PARTS_AMOUNT, Value)
        End Set
    End Property



    Public Property ServiceAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_SERVICE_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ClaimloadReconWrkDAL.COL_NAME_SERVICE_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_SERVICE_AMOUNT, Value)
        End Set
    End Property



    Public Property ShippingAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_SHIPPING_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ClaimloadReconWrkDAL.COL_NAME_SHIPPING_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_SHIPPING_AMOUNT, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=20)>
    Public Property Part1Sku() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_PART_1_SKU) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_PART_1_SKU), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_PART_1_SKU, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)> _
    Public Property Part1Description() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_PART_1_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_PART_1_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_PART_1_DESCRIPTION, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=20)>
    Public Property Part2Sku() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_PART_2_SKU) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_PART_2_SKU), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_PART_2_SKU, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)> _
    Public Property Part2Description() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_PART_2_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_PART_2_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_PART_2_DESCRIPTION, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=20)>
    Public Property Part3Sku() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_PART_3_SKU) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_PART_3_SKU), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_PART_3_SKU, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)> _
    Public Property Part3Description() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_PART_3_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_PART_3_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_PART_3_DESCRIPTION, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=20)>
    Public Property Part4Sku() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_PART_4_SKU) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_PART_4_SKU), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_PART_4_SKU, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)> _
    Public Property Part4Description() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_PART_4_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_PART_4_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_PART_4_DESCRIPTION, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=20)>
    Public Property Part5Sku() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_PART_5_SKU) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_PART_5_SKU), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_PART_5_SKU, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)> _
    Public Property Part5Description() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_PART_5_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_PART_5_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_PART_5_DESCRIPTION, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=5)> _
    Public Property ServiceLevel() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_SERVICE_LEVEL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_SERVICE_LEVEL), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_SERVICE_LEVEL, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=10)> _
    Public Property DealerReference() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_DEALER_REFERENCE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_DEALER_REFERENCE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_DEALER_REFERENCE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=10)> _
    Public Property Pos() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_POS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_POS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_POS, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200)> _
    Public Property ProblemFound() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_PROBLEM_FOUND) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_PROBLEM_FOUND), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_PROBLEM_FOUND, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=11)> _
    Public Property FinalStatus() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_FINAL_STATUS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_FINAL_STATUS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_FINAL_STATUS, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=500)>
    Public Property TechnicalReport() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_TECHNICAL_REPORT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_TECHNICAL_REPORT), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_TECHNICAL_REPORT, Value)
        End Set
    End Property



    Public Property DeliveryDate() As DateType
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_DELIVERY_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ClaimloadReconWrkDAL.COL_NAME_DELIVERY_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_DELIVERY_DATE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=10)> _
    Public Property BatchNumber() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_BATCH_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_BATCH_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_BATCH_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=1000)> _
    Public Property IncomingEntireRecord() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_INCOMING_ENTIRE_RECORD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_INCOMING_ENTIRE_RECORD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_INCOMING_ENTIRE_RECORD, Value)
        End Set
    End Property

    Public Property CommentTypeId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_COMMENT_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimloadReconWrkDAL.COL_NAME_COMMENT_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_COMMENT_TYPE_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=255)> _
    Public Property CommentType() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_COMMENT_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_COMMENT_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_COMMENT_TYPE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)> _
    Public Property TrackingNumber() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_TRACKING_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_TRACKING_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_TRACKING_NUMBER, Value)
        End Set
    End Property

    Public Property ClaimAuthorizationId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_CLAIM_AUTHORIZATION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimloadReconWrkDAL.COL_NAME_CLAIM_AUTHORIZATION_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_CLAIM_AUTHORIZATION_ID, Value)
        End Set
    End Property

    Public Property InvoiceDate() As DateType
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_INVOICE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ClaimloadReconWrkDAL.COL_NAME_INVOICE_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_INVOICE_DATE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=60)>
    Public Property RiskTypeEnglish() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_RISK_TYPE_ENGLISH) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_RISK_TYPE_ENGLISH), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_RISK_TYPE_ENGLISH, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=200)>
    Public Property ItemDescription() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_ITEM_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_ITEM_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_ITEM_DESCRIPTION, Value)
        End Set
    End Property

    Public Property TripAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_TRIP_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ClaimloadReconWrkDAL.COL_NAME_TRIP_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_TRIP_AMOUNT, Value)
        End Set
    End Property

    Public Property OtherAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_OTHER_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ClaimloadReconWrkDAL.COL_NAME_OTHER_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_OTHER_AMOUNT, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=800)>
    Public Property OtherExplanation() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_OTHER_EXPLANATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_OTHER_EXPLANATION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_OTHER_EXPLANATION, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=20)>
    Public Property ClaimNumber() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_CLAIM_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_CLAIM_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_CLAIM_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=10)>
    Public Property ExtendedStatusCode() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_EXTENDED_STATUS_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_EXTENDED_STATUS_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_EXTENDED_STATUS_CODE, Value)
        End Set
    End Property

    Public Property ExtendedStatusDate() As DateType
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_EXTENDED_STATUS_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ClaimloadReconWrkDAL.COL_NAME_EXTENDED_STATUS_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_EXTENDED_STATUS_DATE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50)>
    Public Property TrackingNumberToCust() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_TRACKING_NUM_TO_CUST) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_TRACKING_NUM_TO_CUST), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_TRACKING_NUM_TO_CUST, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50)>
    Public Property TrackingNumberToSC() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_TRACKING_NUM_TO_SC) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_TRACKING_NUM_TO_SC), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_TRACKING_NUM_TO_SC, Value)
        End Set
    End Property

    Public Property DateDeviceShippedToSC() As DateType
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_DEVICE_SHIPPED_TO_SC_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ClaimloadReconWrkDAL.COL_NAME_DEVICE_SHIPPED_TO_SC_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_DEVICE_SHIPPED_TO_SC_DATE, Value)
        End Set
    End Property

    Public ReadOnly Property FamilyDataSet As DataSet Implements IFileLoadReconWork.FamilyDataSet
        Get
            Return Dataset
        End Get
    End Property


    <ValidStringLength("", Max:=1)> _
    Public Property DeductibleIncluded() As String
        Get
            CheckDeleted()
            If Row(ClaimloadReconWrkDAL.COL_NAME_Deductile_Included) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadReconWrkDAL.COL_NAME_Deductile_Included), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ClaimloadReconWrkDAL.COL_NAME_Deductile_Included, Value)
        End Set
    End Property


#End Region

#Region "Public Members"
    Public Overrides Sub Save() Implements IFileLoadReconWork.Save
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ClaimloadReconWrkDAL
                dal.Update(Row)
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
#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function LoadList(ByVal strFileName As String) As DataView
        Dim dal As New ClaimloadReconWrkDAL
        Return dal.LoadList(strFileName, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(ClaimloadReconWrkDAL.TABLE_NAME).DefaultView
    End Function
#End Region

End Class


