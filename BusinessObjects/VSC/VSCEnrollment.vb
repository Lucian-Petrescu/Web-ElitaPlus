'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (4/11/2007)  ********************

Public Class VSCEnrollment
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
    Public Sub New(ByVal id As Guid, ByVal familyDS As Dataset)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As Dataset)
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
            Dim dal As New VSCEnrollmentDAL
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
            Dim dal As New VSCEnrollmentDAL
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
    Public ReadOnly Property Id As Guid
        Get
            If Row(VSCEnrollmentDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(VSCEnrollmentDAL.COL_NAME_ENROLLMENT_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=80)> _
    Public Property CertificateNumber As String
        Get
            CheckDeleted()
            If Row(VSCEnrollmentDAL.COL_NAME_CERTIFICATE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(VSCEnrollmentDAL.COL_NAME_CERTIFICATE_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCEnrollmentDAL.COL_NAME_CERTIFICATE_NUMBER, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=200)> _
    Public Property Address1 As String
        Get
            CheckDeleted()
            If Row(VSCEnrollmentDAL.COL_NAME_ADDRESS1) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(VSCEnrollmentDAL.COL_NAME_ADDRESS1), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCEnrollmentDAL.COL_NAME_ADDRESS1, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=200)> _
    Public Property City As String
        Get
            CheckDeleted()
            If Row(VSCEnrollmentDAL.COL_NAME_CITY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(VSCEnrollmentDAL.COL_NAME_CITY), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCEnrollmentDAL.COL_NAME_CITY, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=200)> _
    Public Property Region As String
        Get
            CheckDeleted()
            If Row(VSCEnrollmentDAL.COL_NAME_REGION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(VSCEnrollmentDAL.COL_NAME_REGION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCEnrollmentDAL.COL_NAME_REGION, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=40)> _
    Public Property PostalCode As String
        Get
            CheckDeleted()
            If Row(VSCEnrollmentDAL.COL_NAME_POSTAL_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(VSCEnrollmentDAL.COL_NAME_POSTAL_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCEnrollmentDAL.COL_NAME_POSTAL_CODE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=60)> _
    Public Property HomePhone As String
        Get
            CheckDeleted()
            If Row(VSCEnrollmentDAL.COL_NAME_HOME_PHONE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(VSCEnrollmentDAL.COL_NAME_HOME_PHONE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCEnrollmentDAL.COL_NAME_HOME_PHONE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ModelYear As LongType
        Get
            CheckDeleted()
            If Row(VSCEnrollmentDAL.COL_NAME_MODEL_YEAR) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(VSCEnrollmentDAL.COL_NAME_MODEL_YEAR), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCEnrollmentDAL.COL_NAME_MODEL_YEAR, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=800)> _
    Public Property Model As String
        Get
            CheckDeleted()
            If Row(VSCEnrollmentDAL.COL_NAME_MODEL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(VSCEnrollmentDAL.COL_NAME_MODEL), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCEnrollmentDAL.COL_NAME_MODEL, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=800)> _
    Public Property Manufacturer As String
        Get
            CheckDeleted()
            If Row(VSCEnrollmentDAL.COL_NAME_MANUFACTURER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(VSCEnrollmentDAL.COL_NAME_MANUFACTURER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCEnrollmentDAL.COL_NAME_MANUFACTURER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=800)> _
    Public Property EngineVersion As String
        Get
            CheckDeleted()
            If Row(VSCEnrollmentDAL.COL_NAME_ENGINE_VERSION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(VSCEnrollmentDAL.COL_NAME_ENGINE_VERSION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCEnrollmentDAL.COL_NAME_ENGINE_VERSION, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=480)> _
    Public Property VehicleLicenseTag As String
        Get
            CheckDeleted()
            If Row(VSCEnrollmentDAL.COL_NAME_VEHICLE_LICENSE_TAG) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(VSCEnrollmentDAL.COL_NAME_VEHICLE_LICENSE_TAG), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCEnrollmentDAL.COL_NAME_VEHICLE_LICENSE_TAG, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property Odometer As LongType
        Get
            CheckDeleted()
            If Row(VSCEnrollmentDAL.COL_NAME_ODOMETER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(VSCEnrollmentDAL.COL_NAME_ODOMETER), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCEnrollmentDAL.COL_NAME_ODOMETER, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=200)> _
    Public Property Vin As String
        Get
            CheckDeleted()
            If Row(VSCEnrollmentDAL.COL_NAME_VIN) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(VSCEnrollmentDAL.COL_NAME_VIN), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCEnrollmentDAL.COL_NAME_VIN, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property PurchasePrice As DecimalType
        Get
            CheckDeleted()
            If Row(VSCEnrollmentDAL.COL_NAME_PURCHASE_PRICE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(VSCEnrollmentDAL.COL_NAME_PURCHASE_PRICE), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCEnrollmentDAL.COL_NAME_PURCHASE_PRICE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property PurchaseDate As DateType
        Get
            CheckDeleted()
            If Row(VSCEnrollmentDAL.COL_NAME_PURCHASE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(VSCEnrollmentDAL.COL_NAME_PURCHASE_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCEnrollmentDAL.COL_NAME_PURCHASE_DATE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property InServiceDate As DateType
        Get
            CheckDeleted()
            If Row(VSCEnrollmentDAL.COL_NAME_IN_SERVICE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(VSCEnrollmentDAL.COL_NAME_IN_SERVICE_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCEnrollmentDAL.COL_NAME_IN_SERVICE_DATE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property DeliveryDate As DateType
        Get
            CheckDeleted()
            If Row(VSCEnrollmentDAL.COL_NAME_DELIVERY_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(VSCEnrollmentDAL.COL_NAME_DELIVERY_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCEnrollmentDAL.COL_NAME_DELIVERY_DATE, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=200)> _
    Public Property PlanCode As String
        Get
            CheckDeleted()
            If Row(VSCEnrollmentDAL.COL_NAME_PLAN_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(VSCEnrollmentDAL.COL_NAME_PLAN_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCEnrollmentDAL.COL_NAME_PLAN_CODE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property Deductible As LongType
        Get
            CheckDeleted()
            If Row(VSCEnrollmentDAL.COL_NAME_DEDUCTIBLE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(VSCEnrollmentDAL.COL_NAME_DEDUCTIBLE), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCEnrollmentDAL.COL_NAME_DEDUCTIBLE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property TermMonths As LongType
        Get
            CheckDeleted()
            If Row(VSCEnrollmentDAL.COL_NAME_TERM_MONTHS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(VSCEnrollmentDAL.COL_NAME_TERM_MONTHS), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCEnrollmentDAL.COL_NAME_TERM_MONTHS, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property TermKmMi As LongType
        Get
            CheckDeleted()
            If Row(VSCEnrollmentDAL.COL_NAME_TERM_KM_MI) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(VSCEnrollmentDAL.COL_NAME_TERM_KM_MI), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCEnrollmentDAL.COL_NAME_TERM_KM_MI, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=120)> _
    Public Property DealerCode As String
        Get
            CheckDeleted()
            If Row(VSCEnrollmentDAL.COL_NAME_DEALER_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(VSCEnrollmentDAL.COL_NAME_DEALER_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCEnrollmentDAL.COL_NAME_DEALER_CODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200)> _
    Public Property AgentNumber As String
        Get
            CheckDeleted()
            If Row(VSCEnrollmentDAL.COL_NAME_AGENT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(VSCEnrollmentDAL.COL_NAME_AGENT_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCEnrollmentDAL.COL_NAME_AGENT_NUMBER, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property WarrantySaleDate As DateType
        Get
            CheckDeleted()
            If Row(VSCEnrollmentDAL.COL_NAME_WARRANTY_SALE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(VSCEnrollmentDAL.COL_NAME_WARRANTY_SALE_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCEnrollmentDAL.COL_NAME_WARRANTY_SALE_DATE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property PlanAmount As DecimalType
        Get
            CheckDeleted()
            If Row(VSCEnrollmentDAL.COL_NAME_PLAN_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(VSCEnrollmentDAL.COL_NAME_PLAN_AMOUNT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCEnrollmentDAL.COL_NAME_PLAN_AMOUNT, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=16)> _
    Public Property DocumentType As String
        Get
            CheckDeleted()
            If Row(VSCEnrollmentDAL.COL_NAME_DOCUMENT_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(VSCEnrollmentDAL.COL_NAME_DOCUMENT_TYPE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCEnrollmentDAL.COL_NAME_DOCUMENT_TYPE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=80)> _
    Public Property IdentityDocumentNo As String
        Get
            CheckDeleted()
            If Row(VSCEnrollmentDAL.COL_NAME_IDENTITY_DOCUMENT_NO) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(VSCEnrollmentDAL.COL_NAME_IDENTITY_DOCUMENT_NO), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCEnrollmentDAL.COL_NAME_IDENTITY_DOCUMENT_NO, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=32)> _
    Public Property RgNo As String
        Get
            CheckDeleted()
            If Row(VSCEnrollmentDAL.COL_NAME_RG_NO) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(VSCEnrollmentDAL.COL_NAME_RG_NO), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCEnrollmentDAL.COL_NAME_RG_NO, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=160)> _
    Public Property IdType As String
        Get
            CheckDeleted()
            If Row(VSCEnrollmentDAL.COL_NAME_ID_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(VSCEnrollmentDAL.COL_NAME_ID_TYPE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCEnrollmentDAL.COL_NAME_ID_TYPE, Value)
        End Set
    End Property



    Public Property DocumentIssueDate As DateType
        Get
            CheckDeleted()
            If Row(VSCEnrollmentDAL.COL_NAME_DOCUMENT_ISSUE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(VSCEnrollmentDAL.COL_NAME_DOCUMENT_ISSUE_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCEnrollmentDAL.COL_NAME_DOCUMENT_ISSUE_DATE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=240)> _
    Public Property DocumentAgency As String
        Get
            CheckDeleted()
            If Row(VSCEnrollmentDAL.COL_NAME_DOCUMENT_AGENCY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(VSCEnrollmentDAL.COL_NAME_DOCUMENT_AGENCY), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCEnrollmentDAL.COL_NAME_DOCUMENT_AGENCY, Value)
        End Set
    End Property


    Public Property DateOfBirth As DateType
        Get
            CheckDeleted()
            If Row(VSCEnrollmentDAL.COL_NAME_DATE_OF_BIRTH) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(VSCEnrollmentDAL.COL_NAME_DATE_OF_BIRTH), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCEnrollmentDAL.COL_NAME_DATE_OF_BIRTH, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=15)> _
    Public Property WorkPhone As String
        Get
            CheckDeleted()
            If Row(VSCEnrollmentDAL.COL_NAME_WORK_PHONE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(VSCEnrollmentDAL.COL_NAME_WORK_PHONE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCEnrollmentDAL.COL_NAME_WORK_PHONE, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=4)> _
    Public Property PaymentTypeCode As String
        Get
            If Row(VSCEnrollmentDAL.COL_NAME_PAYMENT_TYPE_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(VSCEnrollmentDAL.COL_NAME_PAYMENT_TYPE_CODE), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCEnrollmentDAL.COL_NAME_PAYMENT_TYPE_CODE, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property InstallmentsNumber As Integer
        Get
            If Row(VSCEnrollmentDAL.COL_NAME_INSTALLMENTS_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(VSCEnrollmentDAL.COL_NAME_INSTALLMENTS_NUMBER), Integer))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCEnrollmentDAL.COL_NAME_INSTALLMENTS_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=4)> _
    Public Property CreditCardTypeCode As String
        Get
            If Row(VSCEnrollmentDAL.COL_NAME_CREDIT_CARD_TYPE_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(VSCEnrollmentDAL.COL_NAME_CREDIT_CARD_TYPE_CODE), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCEnrollmentDAL.COL_NAME_CREDIT_CARD_TYPE_CODE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50)> _
    Public Property NameOnCreditCard As String
        Get
            If Row(VSCEnrollmentDAL.COL_NAME_NAME_ON_CREDIT_CARD) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(VSCEnrollmentDAL.COL_NAME_NAME_ON_CREDIT_CARD), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCEnrollmentDAL.COL_NAME_NAME_ON_CREDIT_CARD, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=16)> _
    Public Property CreditCardNumber As String
        Get
            If Row(VSCEnrollmentDAL.COL_NAME_CREDIT_CARD_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(VSCEnrollmentDAL.COL_NAME_CREDIT_CARD_NUMBER), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCEnrollmentDAL.COL_NAME_CREDIT_CARD_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=7)> _
    Public Property ExpirationDate As String
        Get
            If Row(VSCEnrollmentDAL.COL_NAME_EXPIRATION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(VSCEnrollmentDAL.COL_NAME_EXPIRATION_DATE), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCEnrollmentDAL.COL_NAME_EXPIRATION_DATE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=10)> _
    Public Property BankID As String
        Get
            If Row(VSCEnrollmentDAL.COL_NAME_BANK_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(VSCEnrollmentDAL.COL_NAME_BANK_ID), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCEnrollmentDAL.COL_NAME_BANK_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=29)> _
    Public Property AccountNumber As String
        Get
            If Row(VSCEnrollmentDAL.COL_NAME_ACCOUNT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(VSCEnrollmentDAL.COL_NAME_ACCOUNT_NUMBER), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCEnrollmentDAL.COL_NAME_ACCOUNT_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50)> _
    Public Property NameOnAccount As String
        Get
            If Row(VSCEnrollmentDAL.COL_NAME_NAME_ON_ACCOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(VSCEnrollmentDAL.COL_NAME_NAME_ON_ACCOUNT), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCEnrollmentDAL.COL_NAME_NAME_ON_ACCOUNT, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=15)> _
    Public Property ExternalCarCode As String
        Get
            CheckDeleted()
            If Row(VSCEnrollmentDAL.COL_NAME_EXTERNAL_CAR_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(VSCEnrollmentDAL.COL_NAME_EXTERNAL_CAR_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCEnrollmentDAL.COL_NAME_EXTERNAL_CAR_CODE, Value)
        End Set
    End Property
#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New VSCEnrollmentDAL
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

#End Region

#Region "Shared Methods"
    Public Shared Function AddManufacturerCoverage(ByVal certId As Guid) As Guid
       Dim dal As New VSCEnrollmentDAL
       Return dal.AddManufacturerCoverage(certId)       
    End Function

#End Region

End Class


