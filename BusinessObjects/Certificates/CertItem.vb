Imports System.Collections.Generic

'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (11/8/2004)  ********************

Public Class CertItem
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load(id)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDS As DataSet)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As DataSet)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load()
    End Sub

    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Me.Dataset = row.Table.DataSet
        Me.Row = row
    End Sub


    Protected Sub Load()
        Try
            Dim dal As New CertItemDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New CertItemDAL
            If Me._isDSCreator Then
                If Not Me.Row Is Nothing Then
                    Me.Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Me.Row)
                End If
            End If
            Me.Row = Nothing
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Me.Row = Me.FindRow(id, dal.TABLE_KEY_NAME, Me.Dataset.Tables(dal.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.Load(Me.Dataset, id)
                Me.Row = Me.FindRow(id, dal.TABLE_KEY_NAME, Me.Dataset.Tables(dal.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then
                Throw New DataNotFoundException
            End If
            'Me.LoadMfgDeductible(id)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
#End Region

#Region "Attributes"
    Dim coverageTypeDesc As String
    Dim coverageTypeCode As String
    Dim certIsDirty As Boolean
    Dim ReinsuranceStatusDesc As String
#End Region

#Region "Private Members"
    'Initialization code for new objects
    Private Sub Initialize()
    End Sub

    Public Function VerifyEquipment(Optional ByVal Effective_on_date As Date = Nothing) As Boolean
        Dim retVal As Boolean = False, Dealer_EquipmentList_code As String
        Try
            If IsNothing(Effective_on_date) Then
                Effective_on_date = DateTime.Now
            End If
            Dim cert As Certificate = New Certificate(Me.CertId, Me.Dataset)
            Dim dealer As Dealer = New Dealer(cert.DealerId, Me.Dataset)
            Dealer_EquipmentList_code = dealer.EquipmentListCode
            Me.EquipmentId = Guid.Empty
            If Me.IsEquipmentRequired Then
                Me.EquipmentId = Equipment.GetEquipmentIdByEquipmentList(dealer.EquipmentListCode, Effective_on_date, Me.ManufacturerId, Me.Model)
                If (Not Me.EquipmentId.Equals(Guid.Empty)) Then
                    Dim eqp As New Equipment(Me.EquipmentId)
                    Me.ItemDescription = eqp.Description
                    Me.Save()
                    retVal = True
                Else
                    Me.ItemDescription = String.Empty
                End If
            End If
            Return retVal
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If Row(CertItemDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertItemDAL.COL_NAME_CERT_ITEM_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CertId() As Guid
        Get
            CheckDeleted()
            If Row(CertItemDAL.COL_NAME_CERT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertItemDAL.COL_NAME_CERT_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertItemDAL.COL_NAME_CERT_ID, Value)
        End Set
    End Property

    Public Property SkuNumber() As String
        Get
            CheckDeleted()
            If Row(CertItemDAL.COL_NAME_SKU_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertItemDAL.COL_NAME_SKU_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertItemDAL.COL_NAME_SKU_NUMBER, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property ItemNumber() As LongType
        Get
            CheckDeleted()
            If Row(CertItemDAL.COL_NAME_ITEM_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(CertItemDAL.COL_NAME_ITEM_NUMBER), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(CertItemDAL.COL_NAME_ITEM_NUMBER, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property RiskTypeId() As Guid
        Get
            CheckDeleted()
            If Row(CertItemDAL.COL_NAME_RISK_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertItemDAL.COL_NAME_RISK_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertItemDAL.COL_NAME_RISK_TYPE_ID, Value)
        End Set
    End Property

    ' Commenting this Custom Validator to allow the Equipment to be updated as null eventhough the Dealer is using the 
    ' Equipment for REQ-918 - To Save the Cert Item Details in the Claim Equipment Table
    '<ValidateEquipment("")> _
    Public Property EquipmentId() As Guid
        Get
            CheckDeleted()
            If Row(CertItemDAL.COL_NAME_EQUIPMENT_ID) Is DBNull.Value Then
                Return Guid.Empty
            Else
                Return New Guid(CType(Row(CertItemDAL.COL_NAME_EQUIPMENT_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertItemDAL.COL_NAME_EQUIPMENT_ID, Value)
        End Set
    End Property

    Public Property ManufacturerId() As Guid
        Get
            CheckDeleted()
            If Row(CertItemDAL.COL_NAME_MANUFACTURER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertItemDAL.COL_NAME_MANUFACTURER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertItemDAL.COL_NAME_MANUFACTURER_ID, Value)
        End Set
    End Property

    Public Property MaxReplacementCost() As DecimalType
        Get
            CheckDeleted()
            If Row(CertItemDAL.COL_NAME_MAX_REPLACEMENT_COST) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertItemDAL.COL_NAME_MAX_REPLACEMENT_COST), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertItemDAL.COL_NAME_MAX_REPLACEMENT_COST, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=30), SerialNumberValidator("")> _
    Public Property SerialNumber() As String
        Get
            CheckDeleted()
            If Row(CertItemDAL.COL_NAME_SERIAL_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertItemDAL.COL_NAME_SERIAL_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertItemDAL.COL_NAME_SERIAL_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=30), SerialNumberValidator("")> _
    Public Property IMEINumber() As String
        Get
            CheckDeleted()
            If Row(CertItemDAL.COL_NAME_IMEI_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertItemDAL.COL_NAME_IMEI_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertItemDAL.COL_NAME_IMEI_NUMBER, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=100)>
    Public Property Model() As String
        Get
            CheckDeleted()
            If Row(CertItemDAL.COL_NAME_MODEL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertItemDAL.COL_NAME_MODEL), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertItemDAL.COL_NAME_MODEL, Value)
        End Set
    End Property

    Public ReadOnly Property GetCoverageTypeDescription(ByVal coverageID As Guid) As String
        Get
            Dim moCoverage As New CertItemCoverage
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim coverageDV As DataView

            coverageDV = LookupListNew.GetCoverageTypeLookupList(langId)
            coverageTypeDesc = LookupListNew.GetDescriptionFromId(coverageDV, coverageID)

            Return coverageTypeDesc
        End Get
    End Property

    Public ReadOnly Property GetCoverageTypeCode(ByVal coverageID As Guid) As String
        Get
            Dim moCoverage As New CertItemCoverage
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim coverageDV As DataView

            coverageDV = LookupListNew.GetCoverageTypeLookupList(langId)
            coverageTypeCode = LookupListNew.GetCodeFromId(LookupListNew.LK_COVERAGE_TYPES, coverageID)

            Return coverageTypeCode
        End Get
    End Property

    Public ReadOnly Property GetCertificate(ByVal certID As Guid) As Certificate
        Get
            Return New Certificate(certID, Me.Dataset)
        End Get
    End Property

    Public ReadOnly Property GetReinsuranceStatusDescription(ByVal ReinsuranceStatusID As Guid) As String
        Get
            Dim moCoverage As New CertItemCoverage
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim ReinsuranceStatusDV As DataView

            ReinsuranceStatusDV = LookupListNew.GetReInsStatusLookupList(langId)
            ReinsuranceStatusDesc = LookupListNew.GetDescriptionFromId(ReinsuranceStatusDV, ReinsuranceStatusID)

            Return ReinsuranceStatusDesc
        End Get
    End Property


    <ValueMandatory("")> _
    Public Property CompanyId() As Guid
        Get
            CheckDeleted()
            If Row(CertItemDAL.COL_NAME_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertItemDAL.COL_NAME_COMPANY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertItemDAL.COL_NAME_COMPANY_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50)> _
    Public Property ItemDescription() As String
        Get
            CheckDeleted()
            If Row(CertItemDAL.COL_NAME_ITEM_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertItemDAL.COL_NAME_ITEM_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertItemDAL.COL_NAME_ITEM_DESCRIPTION, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=10)> _
    Public Property ItemCode() As String
        Get
            CheckDeleted()
            If Row(CertItemDAL.COL_NAME_ITEM_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertItemDAL.COL_NAME_ITEM_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertItemDAL.COL_NAME_ITEM_CODE, Value)
        End Set
    End Property

    Public Property ItemRetailPrice() As DecimalType
        Get
            CheckDeleted()
            If Row(CertItemDAL.COL_NAME_ITEM_RETAIL_PRICE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertItemDAL.COL_NAME_ITEM_RETAIL_PRICE), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertItemDAL.COL_NAME_ITEM_RETAIL_PRICE, Value)
        End Set
    End Property

    Public Property ItemReplaceReturnDate() As DateType
        Get
            CheckDeleted()
            If Row(CertItemDAL.COL_NAME_ITEM_REPLACE_RETURN_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(CertItemDAL.COL_NAME_ITEM_REPLACE_RETURN_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(CertItemDAL.COL_NAME_ITEM_REPLACE_RETURN_DATE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=5)> _
    Public Property ProductCode() As String
        Get
            CheckDeleted()
            If Row(CertItemDAL.COL_NAME_PRODUCT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertItemDAL.COL_NAME_PRODUCT_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertItemDAL.COL_NAME_PRODUCT_CODE, Value)
        End Set
    End Property

    Public Overrides ReadOnly Property IsDirty() As Boolean
        Get
            Return MyBase.IsDirty OrElse Me.IsChildrenDirty
        End Get
    End Property

    Private _CertItemCoverageId As Guid
    Public Property CertItemCoverageId() As Guid
        Get
            Return _CertItemCoverageId
        End Get
        Set(ByVal Value As Guid)
            _CertItemCoverageId = Value
        End Set
    End Property

    Private _certItemCov As CertItemCoverage
    'Public ReadOnly Property CertItemCoverage() As CertItemCoverage
    '    Get
    '        If _certItemCov Is Nothing Then
    '            Me._certItemCov = New CertItemCoverage(Me.CertItemCoverageId, Me.Dataset)
    '        End If
    '        Return Me._certItemCov
    '    End Get
    'End Property
    Public Function AddNewItemCoverage() As CertItemCoverage
        Dim objCertItemCov As CertItemCoverage
        objCertItemCov = New CertItemCoverage(Me.Dataset)

        Return objCertItemCov
    End Function
    Private _cert As Certificate
    Private _address As Address
    Public ReadOnly Property Cert() As Certificate
        Get
            If _cert Is Nothing Then
                Me._cert = New Certificate(Me.CertId, Me.Dataset)
            End If
            Return Me._cert
        End Get
    End Property

    Public ReadOnly Property Address() As Address
        Get
            If _address Is Nothing Then
                Me._address = New Address(Me.Cert.AddressId, Me.Dataset, Nothing)
            End If
            Return Me._address
        End Get
    End Property
    Public ReadOnly Property DeductibleByMfgFlag() As Boolean
        Get
            Try
                Dim oContract As New Contract(Contract.GetContractID(CertId))
                If oContract.DeductibleByManufacturerId.Equals(Guid.Empty) Then
                    Return False
                Else
                    Dim code As String = LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, oContract.DeductibleByManufacturerId)
                    If code.Equals(Codes.YESNO_Y) Then
                        Return True
                    Else
                        Return False
                    End If
                End If
            Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End Get
    End Property

    Public ReadOnly Property AreManufacturerAndModelMissing() As Boolean
        Get
            Dim dr As DataRow
            If Me.Dataset.Tables(CertItemDAL.TABLE_NAME_MFG_DEDUCT).Rows.Count > 0 Then
                dr = Me.Dataset.Tables(CertItemDAL.TABLE_NAME_MFG_DEDUCT).Rows(0)
            End If
            CheckDeleted()
            If Not dr Is Nothing AndAlso dr(CertItemDAL.COL_NAME_MFG_DEDUCTIBLE) Is DBNull.Value Then
                Return False

            Else
                If Not dr Is Nothing Then
                    Return True
                Else
                    Return False
                End If
            End If
        End Get

    End Property

    Public Property OriginalRetailPrice() As DecimalType
        Get
            CheckDeleted()
            If Row(CertItemDAL.COL_NAME_ORIGINAL_RETAIL_PRICE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertItemDAL.COL_NAME_ORIGINAL_RETAIL_PRICE), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertItemDAL.COL_NAME_ORIGINAL_RETAIL_PRICE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=1)> _
    Public Property MobileType() As String
        Get
            CheckDeleted()
            If Row(CertItemDAL.COL_NAME_MOBILE_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertItemDAL.COL_NAME_MOBILE_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertItemDAL.COL_NAME_MOBILE_TYPE, Value)
        End Set
    End Property

    Public Property FirstUseDate() As DateType
        Get
            CheckDeleted()
            If Row(CertItemDAL.COL_NAME_FIRST_USE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(CertItemDAL.COL_NAME_FIRST_USE_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(CertItemDAL.COL_NAME_FIRST_USE_DATE, Value)
        End Set
    End Property

    Public Property LastUseDate() As DateType
        Get
            CheckDeleted()
            If Row(CertItemDAL.COL_NAME_LAST_USE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(CertItemDAL.COL_NAME_LAST_USE_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(CertItemDAL.COL_NAME_LAST_USE_DATE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=20)> _
    Public Property SimCardNumber() As String
        Get
            CheckDeleted()
            If Row(CertItemDAL.COL_NAME_SIM_CARD_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertItemDAL.COL_NAME_SIM_CARD_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertItemDAL.COL_NAME_SIM_CARD_NUMBER, Value)
        End Set
    End Property

    Public ReadOnly Property IsEquipmentRequired() As Boolean
        Get
            CheckDeleted()
            Dim cert As Certificate = New Certificate(Me.CertId, Me.Dataset)
            Dim dealer As Dealer = New Dealer(cert.DealerId, Me.Dataset)
            If (dealer.UseEquipmentId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, "Y")) Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    Public Property EffectiveDate() As DateType
        Get
            CheckDeleted()
            If Row(CertItemDAL.COL_NAME_EFFECTIVE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(CertItemDAL.COL_NAME_EFFECTIVE_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(CertItemDAL.COL_NAME_EFFECTIVE_DATE, Value)
        End Set
    End Property

    Public Property ExpirationDate() As DateType
        Get
            CheckDeleted()
            If Row(CertItemDAL.COL_NAME_EXPIRATION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(CertItemDAL.COL_NAME_EXPIRATION_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(CertItemDAL.COL_NAME_EXPIRATION_DATE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=5)> _
    Public Property CertProductCode() As String
        Get
            CheckDeleted()
            If Row(CertItemDAL.COL_NAME_CERT_PROD_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertItemDAL.COL_NAME_CERT_PROD_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertItemDAL.COL_NAME_CERT_PROD_CODE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=10)>
    Public Property ImeiUpdateSource() As String
        Get
            CheckDeleted()
            If Row(CertItemDAL.COL_NAME_IMEI_UPDATE_SOURCE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertItemDAL.COL_NAME_IMEI_UPDATE_SOURCE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertItemDAL.COL_NAME_IMEI_UPDATE_SOURCE, Value)
        End Set
    End Property

    <ValidStringLength("")>
    Public Property AllowedEvents() As String
        Get
            CheckDeleted()
            If Row(CertItemDAL.COL_NAME_ALLOWED_EVENTS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertItemDAL.COL_NAME_ALLOWED_EVENTS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertItemDAL.COL_NAME_ALLOWED_EVENTS, Value)
        End Set
    End Property
    <ValidStringLength("")>
    Public Property MaxInsuredAmount() As String
        Get
            CheckDeleted()
            If Row(CertItemDAL.COL_NAME_MAX_INSURED_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertItemDAL.COL_NAME_MAX_INSURED_AMOUNT), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertItemDAL.COL_NAME_MAX_INSURED_AMOUNT, Value)
        End Set
    End Property

     Public Property BenefitStatus() As String
        Get
            CheckDeleted()
            If Row(CertItemDAL.COL_NAME_BENEFIT_STATUS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertItemDAL.COL_NAME_BENEFIT_STATUS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertItemDAL.COL_NAME_BENEFIT_STATUS, Value)
        End Set
    End Property

     Public Property IneligibilityReason() As String
        Get
            CheckDeleted()
            If Row(CertItemDAL.COL_NAME_INELIGIBILITY_REASON) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CertItemDAL.COL_NAME_INELIGIBILITY_REASON), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertItemDAL.COL_NAME_INELIGIBILITY_REASON, Value)
        End Set
    End Property


#End Region

#Region "Navigation Properties"

    Public ReadOnly Property Coverages As IEnumerable(Of CertItemCoverage)
        Get
            Return CertItemCoverage.GetItemCovListForCertificate(Me.Cert.Id, Me.Cert) _
                .AsQueryable() _
                .Where(Function(ci) Me.Id = ci.CertItemId) _
                .AsEnumerable()
        End Get
    End Property

#End Region

#Region "Public Members"
    Public Sub UpdateSKU(ByVal force As Boolean)
        Dim bMakeModelChanged As Boolean = False
        If (force) Then
            bMakeModelChanged = True
        Else
            ' Check if Make and Model is Modified since Last DB Update
            If (Me.Row.HasVersion(DataRowVersion.Original)) Then
                Dim manufacturerId As Guid
                Dim originalManufacturerId As Guid
                manufacturerId = Me.ManufacturerId
                If (Me.Row(CertItemDAL.COL_NAME_MANUFACTURER_ID, DataRowVersion.Original) Is DBNull.Value) Then
                    originalManufacturerId = Nothing
                Else
                    originalManufacturerId = New Guid(CType(Me.Row(CertItemDAL.COL_NAME_MANUFACTURER_ID, DataRowVersion.Original), Byte()))
                End If
                If Not (manufacturerId.Equals(originalManufacturerId)) Then
                    bMakeModelChanged = True
                End If

                Dim originalModel As String
                If (Me.Row(CertItemDAL.COL_NAME_MODEL, DataRowVersion.Original) Is DBNull.Value) Then
                    originalModel = String.Empty
                Else
                    originalModel = CType(Row(CertItemDAL.COL_NAME_MODEL, DataRowVersion.Original), String)
                End If
                If (Me.Model <> originalModel) Then
                    bMakeModelChanged = True
                End If

                ' Check if SKU is Changed, If Make and Model are changed and SKU is also Chaned then there is chance that
                ' User entered the SKU or is resolved by System
                Dim originalSKU As String
                If (Me.Row(CertItemDAL.COL_NAME_SKU_NUMBER, DataRowVersion.Original) Is DBNull.Value) Then
                    originalSKU = String.Empty
                Else
                    originalSKU = CType(Row(CertItemDAL.COL_NAME_SKU_NUMBER, DataRowVersion.Original), String)
                End If
                If (Me.SkuNumber <> originalSKU) Then
                    bMakeModelChanged = False
                End If
            End If
        End If

        If (bMakeModelChanged) Then
            ' Check if SKU can be resolved
            If (Not Me.ManufacturerId.Equals(Guid.Empty)) AndAlso Len(Trim(Me.Model)) > 0 Then
                Me.SkuNumber = ListPrice.GetSKU(Cert.DealerId, LookupListNew.GetDescriptionFromId(LookupListNew.LK_MANUFACTURERS, Me.ManufacturerId), Me.Model)
            End If
        End If
    End Sub

    Public Overrides Sub Save()
        Try
            UpdateSKU(False)
            Dim moCert As Certificate
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsFamilyDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New CertItemDAL
                dal.UpdateFamily(Me.Dataset)
                'Reload the Data from the DB
                If Me.Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Me.Id
                    Me.Dataset = New DataSet
                    Me.Row = Nothing
                    Me.Load(objId)
                    moCert = Me.GetCertificate(Me.CertId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    Public Sub SaveItem()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsFamilyDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New CertItemDAL
                dal.UpdateCertItemAndCov(Me.Dataset)
                'Reload the Data from the DB
                If Me.Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Me.Id
                    Me.Dataset = New DataSet
                    Me.Row = Nothing
                    Me.Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    Public Function CopyEnrolledEquip_into_ClaimedEquip() As ClaimEquipment
        '#@REQ 1106
        Dim objClaimedEquipment As New ClaimEquipment()
        Dim cert As Certificate = New Certificate(Me.CertId, Me.Dataset)
        Dim dealer As Dealer = New Dealer(cert.DealerId, Me.Dataset)
        Try
            With objClaimedEquipment
                .ManufacturerId = Me.ManufacturerId
                .Model = Me.Model
                .SKU = Me.SkuNumber
                'Resolve the equipment
                .EquipmentId = Equipment.GetEquipmentIdByEquipmentList(dealer.EquipmentListCode, DateTime.Today, Me.ManufacturerId, Me.Model)
                '.EquipmentId = Me.EquipmentId
                .SerialNumber = Me.SerialNumber
                .IMEINumber = Me.IMEINumber
                .ClaimEquipmentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_EQUIPMENT_TYPE, "C")
                .EquipmentId = Me.EquipmentId
            End With
            Return objClaimedEquipment
        Catch ex As Exception
            'equipment not found exception may come so eating that
        End Try
    End Function

    Public Function ProcessAppleCareEnrollment(attValue As String) As String
        Dim strErrMsg As String
        Dim dal As New CertItemDAL
        dal.ProcessAppleCareEnrolledItem(Me.Id, attValue, strErrMsg)
        Return strErrMsg
    End Function


#End Region

#Region "DataView Retrieveing Methods"


    Public Class ItemList
        Inherits BusinessObjectListEnumerableBase(Of Certificate, CertItem)

        Public Sub New(ByVal parent As Certificate)
            MyBase.New(parent.Dataset.Tables(CertItemDAL.TABLE_NAME), parent)
        End Sub

        Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
            Return True
        End Function
    End Class

    Public Shared Function GetItemListForCertificate(ByVal certId As Guid, ByVal parent As BusinessObjectBase) As ItemList
        If parent.Dataset.Tables.IndexOf(CertItemDAL.TABLE_NAME) < 0 Then
            Dim dal As New CertItemDAL
            dal.LoadAllItemsForCertificate(certId, parent.Dataset)
        End If
        Return New ItemList(parent)
    End Function

    Public Shared Function GetItems(ByVal certId As Guid) As CertItemSearchDV
        Try
            Dim dal As New CertItemDAL

            Return New CertItemSearchDV(dal.LoadList(certId).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Public Shared Function GetRegisteredItems(ByVal certId As Guid) As CertRegItemSearchDV
        Try
            Dim dal As New CertItemDAL

            Return New CertRegItemSearchDV(dal.LoadRegItemsList(certId, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetNextItemNumber(ByVal certId As Guid) As Long
        Try
            Dim dal As New CertItemDAL
            Dim ds As DataSet = dal.GetMaxItemNumber(certId)

            If (ds.Tables.Count > 0 And ds.Tables(0).Rows.Count > 0) Then
                Dim dr As DataRow = ds.Tables(0).Rows(0)
                GetNextItemNumber = CType(dr(dal.COL_NAME_ITEM_NUMBER), Long) + 1
            End If

            Return GetNextItemNumber


        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetItemsForWS(ByVal certIds As ArrayList) As DataTable
        Try
            Dim dal As New CertItemDAL

            Return dal.LoadList(certIds).Tables(0)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Class CertItemSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_CERT_ITEM_ID As String = "cert_item_id"
        Public Const COL_ITEM_NUMBER As String = "item_number"
        Public Const COL_RISK_TYPE As String = "risk_type"
        Public Const COL_ITEM_DESCRIPTION As String = "item_desc"
        Public Const COL_MAKE As String = "make"
        Public Const COL_MODEL As String = "model"
        Public Const COL_EXPIRATION_DATE As String = "expiration_date"
        Public Const COL_ORIGINAL_RETAIL_PRICE As String = "original_retail_price"
        Public Const COL_SERIAL_NUMBER As String = "Serial_Number"
        Public const COL_BENEFIT_STATUS As String ="benefit_status"

#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Function IsSerialNumberExist(ByVal serialNo As String) As Boolean
            Me.RowFilter = String.Format("serial_number = '{0}'", serialNo)
            Return IIf(Me.Count = 0, False, True)
        End Function

        Public Function GetOriginalRetailPrice(ByVal serialNo As String) As Nullable(Of Decimal)
            Dim originalRetailPrice As Nullable(Of Decimal)
            Me.Sort = "effective_date desc"
            If Not String.IsNullOrEmpty(serialNo) Then
                Me.RowFilter = String.Format("serial_number = '{0}'", serialNo)
            End If
            If Me.Count > 0 Then
                If Not IsDBNull(Me(0)("original_retail_price")) Then
                    originalRetailPrice = Convert.ToDecimal(Me(0)("original_retail_price"))
                Else
                    originalRetailPrice = Convert.ToDecimal(0)
                End If
            End If


            Return originalRetailPrice
        End Function

    End Class

    Public Class CertRegItemSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_CERT_REGISTERED_ITEM_ID As String = "cert_registered_item_id"
        Public Const COL_REGISTERED_ITEM_NAME As String = "registered_item_name"
        Public Const COL_DEVICE_TYPE As String = "device_type"
        Public Const COL_ITEM_DESCRIPTION As String = "item_desc"
        Public Const COL_MAKE As String = "make"
        Public Const COL_MODEL As String = "model"
        Public Const COL_PURCHASE_DATE As String = "purchased_date"
        Public Const COL_PURCHASE_PRICE As String = "purchase_price"
        Public Const COL_SERIAL_NUMBER As String = "Serial_Number"
        'REQ-6002
        Public Const COL_REGISTRATION_DATE As String = "registration_date"
        Public Const COL_RETAIL_PRICE As String = "retail_price"
        Public Const COL_EXPIRATION_DATE As String = "expiration_date"
        Public Const COL_ITEM_STATUS As String = "item_status"
        Public Const COL_INDIXID As String = "indixid"



#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region

#Region "Certificate Children"

    Public Function AddCertificateChild() As Certificate
        If Not Me.CertId.Equals(Guid.Empty) Then
            Return New Certificate(Me.CertId, Me.Dataset)
        End If
        Return Nothing
    End Function

    Public Function AddClaimExtendedStatus(ByVal claimStatusID As Guid) As ClaimStatus
        If claimStatusID.Equals(Guid.Empty) Then
            Dim objClaimStatus As New ClaimStatus(Me.Dataset)
            Return objClaimStatus
        Else
            Dim objClaimStatus As New ClaimStatus(claimStatusID, Me.Dataset)
            Return objClaimStatus
        End If
    End Function
#End Region

#Region "Functions"

    Public Shared Function ValidateSerialNumber(ByVal SerialNumber As String, ByVal CertNumber As String, ByVal CompanyGroupId As Guid) As DataView
        Dim dal As New CertItemDAL
        Dim ds As DataSet

        ds = dal.ValidateSerialNumber(SerialNumber, CertNumber, CompanyGroupId)
        Return ds.Tables(0).DefaultView

    End Function

    Public Function IsCustomerAddressRequired() As Boolean
        Dim objContractId As Guid = Contract.GetContractID(Me.CertId)
        Dim objContract As New Contract(objContractId)
        Dim yesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y")

        If objContract.CustmerAddressRequiredId.Equals(yesId) Then
            Dim addressNotComplet As Boolean = True
            If Me.Cert.AddressId = Guid.Empty OrElse Me.Address Is Nothing Then
                Return True
            Else
                Return ValidateAddressField()
                '' ''If Me.Address.Address1 Is Nothing OrElse Me.Address.Address1.Equals(String.Empty) _
                '' ''    OrElse Me.Address.City Is Nothing OrElse Me.Address.City.Equals(String.Empty) _
                '' ''    OrElse Me.Address.PostalCode Is Nothing OrElse Me.Address.PostalCode.Equals(String.Empty) _
                '' ''    OrElse Me.Address.RegionId.Equals(Guid.Empty) _
                '' ''    OrElse Me.Address.CountryId.Equals(Guid.Empty) Then
                '' ''    Return True
                '' ''End If
            End If
        End If
        Return False
    End Function

    Private Function ValidateAddressField() As Boolean
        Dim addressErrorExist As Boolean = False

        Dim strAddFmt As String = New Country(Me.Address.CountryId).MailAddrFormat.ToUpper
        If Me.Address.IsAddressComponentRequired(strAddFmt, "ADR1") Then
            If Me.Address.Address1 Is Nothing OrElse Me.Address.Address1.Equals(String.Empty) Then
                addressErrorExist = True
            End If
        End If


        If Me.Address.IsAddressComponentRequired(strAddFmt, "CITY") Then
            If Me.Address.City Is Nothing OrElse Me.Address.City.Equals(String.Empty) Then
                addressErrorExist = True
            End If
        End If

        If Me.Address.IsAddressComponentRequired(strAddFmt, "ZIP") Then
            If Me.Address.PostalCode Is Nothing OrElse Me.Address.PostalCode.Equals(String.Empty) Then
                addressErrorExist = True
            End If
        End If

        If Me.Address.IsAddressComponentRequired(strAddFmt, "RGNAME") OrElse _
           Me.Address.IsAddressComponentRequired(strAddFmt, "RGCODE") Then
            If Me.Address.RegionId.Equals(Guid.Empty) Then
                addressErrorExist = True
            End If
        End If

        If Me.Address.IsAddressComponentRequired(strAddFmt, "COU") Then
            If Me.Address.CountryId.Equals(Guid.Empty) Then
                addressErrorExist = True
            End If
        End If

        Return addressErrorExist

    End Function

    Public Function CreateClaimedEquipmentFromEnrolledEquipment(ByRef claimEquipment As ClaimEquipment, ByRef msgList As List(Of String)) As Boolean
        Dim flag As Boolean = False
        If Me.IsEquipmentRequired Then
            If Not Me.EquipmentId.Equals(Guid.Empty) Then
                'If equipmentid exists resolves create claim equipment
                claimEquipment = Me.CopyEnrolledEquip_into_ClaimedEquip()
                flag = True
            Else
                If Not Me.ManufacturerId.Equals(Guid.Empty) AndAlso Not String.IsNullOrEmpty(Me.Model) Then
                    'If make and model found try to resolve equipment
                    Me.VerifyEquipment()
                    claimEquipment = Me.CopyEnrolledEquip_into_ClaimedEquip()
                    If Me.EquipmentId.Equals(Guid.Empty) Then
                        msgList.Add("EQUIPMENT_NOT_CONFIGURED")
                        flag = True
                    End If
                Else
                    msgList.Add("EQUIPMENT_NOT_FOUND_MANUAL_CLAIM_EQUIPMENT_TO_PROCEED")
                End If
            End If
        End If
        Return flag
    End Function


    Public Shared Function LoadSku(ByVal equipmentId As Guid, ByVal dealerId As Guid) As DataView

        Dim dal As New CertItemDAL
        Dim dv As DataView

        Try
            dv = dal.LoadSKUs(equipmentId, dealerId)
            If (Not dv Is Nothing) Then
                dv.Sort = dv.Table.Columns(0).ColumnName
            End If
            Return dv
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
#End Region

#Region "Custom Validations"
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidateEquipment
        Inherits ValidBaseAttribute
        Private _fieldDisplayName As String
        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Certificate.EQUIPMENT_NOT_FOUND)
            _fieldDisplayName = fieldDisplayName
        End Sub

        Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As CertItem = CType(objectToValidate, CertItem)
            If (obj.IsEquipmentRequired) Then
                Dim vma As ValueMandatoryAttribute = New ValueMandatoryAttribute(_fieldDisplayName)
                Return vma.IsValid(objectToCheck, objectToValidate)
            Else
                Return True
            End If
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class SerialNumberValidator
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.SERIAL_NUMBER_ALREADY_EXIST)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As CertItem = CType(objectToValidate, CertItem)
            Dim dal As New CertItemDAL

            If (Not obj.SerialNumber Is Nothing) AndAlso (obj.SerialNumber.Trim <> String.Empty) Then

                If Not dal.IsSerialNumberUnique(obj.Cert.DealerId, obj.Id, obj.SerialNumber.Trim) Then
                    Return False
                End If
            End If
            Return True

        End Function
    End Class
#End Region

   
End Class


