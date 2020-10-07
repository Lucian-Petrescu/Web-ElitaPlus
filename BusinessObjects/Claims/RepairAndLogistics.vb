'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (4/4/2012)  ********************

Public Class RepairAndLogistics
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(id As Guid)
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
    Public Sub New(id As Guid, familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load()
    End Sub

    Public Sub New(row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New RepairAndLogisticsDAL
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

    Protected Sub Load(id As Guid)
        Try
            Dim dal As New RepairAndLogisticsDAL
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
                dal.Load(Dataset, id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
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
    Public ReadOnly Property Id() As Guid
        Get
            If Row(RepairAndLogisticsDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(RepairAndLogisticsDAL.COL_NAME_ID), Byte()))
            End If
        End Get
    End Property

    <ValidStringLength("", Max:=200)> _
    Public Property CustomerName() As String
        Get
            CheckDeleted()
            If Row(RepairAndLogisticsDAL.COL_NAME_CUSTOMER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RepairAndLogisticsDAL.COL_NAME_CUSTOMER_NAME), String)
            End If
        End Get
        Set(Value As String)
            CheckDeleted()
            SetValue(RepairAndLogisticsDAL.COL_NAME_CUSTOMER_NAME, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=4000)> _
    Public Property CoverageType() As String
        Get
            CheckDeleted()
            If Row(RepairAndLogisticsDAL.COL_NAME_COVERAGE_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RepairAndLogisticsDAL.COL_NAME_COVERAGE_TYPE), String)
            End If
        End Get
        Set(Value As String)
            CheckDeleted()
            SetValue(RepairAndLogisticsDAL.COL_NAME_COVERAGE_TYPE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=20)> _
    Public Property ProductCode() As String
        Get
            CheckDeleted()
            If Row(RepairAndLogisticsDAL.COL_NAME_PRODUCT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RepairAndLogisticsDAL.COL_NAME_PRODUCT_CODE), String)
            End If
        End Get
        Set(Value As String)
            CheckDeleted()
            SetValue(RepairAndLogisticsDAL.COL_NAME_PRODUCT_CODE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=40)> _
    Public Property ClaimNumber() As String
        Get
            CheckDeleted()
            If Row(RepairAndLogisticsDAL.COL_NAME_CLAIM_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RepairAndLogisticsDAL.COL_NAME_CLAIM_NUMBER), String)
            End If
        End Get
        Set(Value As String)
            CheckDeleted()
            SetValue(RepairAndLogisticsDAL.COL_NAME_CLAIM_NUMBER, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=4)> _
    Public Property ClaimStatus() As String
        Get
            CheckDeleted()
            If Row(RepairAndLogisticsDAL.COL_NAME_CLAIM_STATUS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RepairAndLogisticsDAL.COL_NAME_CLAIM_STATUS), String)
            End If
        End Get
        Set(Value As String)
            CheckDeleted()
            SetValue(RepairAndLogisticsDAL.COL_NAME_CLAIM_STATUS, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property DateOfClaim() As DateType
        Get
            CheckDeleted()
            If Row(RepairAndLogisticsDAL.COL_NAME_DATE_OF_CLAIM) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(RepairAndLogisticsDAL.COL_NAME_DATE_OF_CLAIM), Date))
            End If
        End Get
        Set(Value As DateType)
            CheckDeleted()
            SetValue(RepairAndLogisticsDAL.COL_NAME_DATE_OF_CLAIM, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=40)> _
    Public Property Pos() As String
        Get
            CheckDeleted()
            If Row(RepairAndLogisticsDAL.COL_NAME_POS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RepairAndLogisticsDAL.COL_NAME_POS), String)
            End If
        End Get
        Set(Value As String)
            CheckDeleted()
            SetValue(RepairAndLogisticsDAL.COL_NAME_POS, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=200)> _
    Public Property ServiceCenter() As String
        Get
            CheckDeleted()
            If Row(RepairAndLogisticsDAL.COL_NAME_SERVICE_CENTER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RepairAndLogisticsDAL.COL_NAME_SERVICE_CENTER), String)
            End If
        End Get
        Set(Value As String)
            CheckDeleted()
            SetValue(RepairAndLogisticsDAL.COL_NAME_SERVICE_CENTER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1020)> _
    Public Property ClaimedDeviceManufacturer() As String
        Get
            CheckDeleted()
            If Row(RepairAndLogisticsDAL.COL_NAME_CLAIMED_DEVICE_MANUFACTURER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RepairAndLogisticsDAL.COL_NAME_CLAIMED_DEVICE_MANUFACTURER), String)
            End If
        End Get
        Set(Value As String)
            CheckDeleted()
            SetValue(RepairAndLogisticsDAL.COL_NAME_CLAIMED_DEVICE_MANUFACTURER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=160)> _
    Public Property ClaimedDeviceModel() As String
        Get
            CheckDeleted()
            If Row(RepairAndLogisticsDAL.COL_NAME_CLAIMED_DEVICE_MODEL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RepairAndLogisticsDAL.COL_NAME_CLAIMED_DEVICE_MODEL), String)
            End If
        End Get
        Set(Value As String)
            CheckDeleted()
            SetValue(RepairAndLogisticsDAL.COL_NAME_CLAIMED_DEVICE_MODEL, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=72)> _
    Public Property ClaimedDeviceSku() As String
        Get
            CheckDeleted()
            If Row(RepairAndLogisticsDAL.COL_NAME_CLAIMED_DEVICE_SKU) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RepairAndLogisticsDAL.COL_NAME_CLAIMED_DEVICE_SKU), String)
            End If
        End Get
        Set(Value As String)
            CheckDeleted()
            SetValue(RepairAndLogisticsDAL.COL_NAME_CLAIMED_DEVICE_SKU, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=120)> _
    Public Property ClaimedDeviceSerialNumber() As String
        Get
            CheckDeleted()
            If Row(RepairAndLogisticsDAL.COL_NAME_CLAIMED_DEVICE_SERIAL_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RepairAndLogisticsDAL.COL_NAME_CLAIMED_DEVICE_SERIAL_NUMBER), String)
            End If
        End Get
        Set(Value As String)
            CheckDeleted()
            SetValue(RepairAndLogisticsDAL.COL_NAME_CLAIMED_DEVICE_SERIAL_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=30)> _
    Public Property ClaimedDeviceIMEINumber() As String
        Get
            CheckDeleted()
            If Row(RepairAndLogisticsDAL.COL_NAME_CLAIMED_DEVICE_IMEI_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RepairAndLogisticsDAL.COL_NAME_CLAIMED_DEVICE_IMEI_NUMBER), String)
            End If
        End Get
        Set(Value As String)
            CheckDeleted()
            SetValue(RepairAndLogisticsDAL.COL_NAME_CLAIMED_DEVICE_IMEI_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=2000)> _
    Public Property ProblemDescription() As String
        Get
            CheckDeleted()
            If Row(RepairAndLogisticsDAL.COL_NAME_PROBLEM_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RepairAndLogisticsDAL.COL_NAME_PROBLEM_DESCRIPTION), String)
            End If
        End Get
        Set(Value As String)
            CheckDeleted()
            SetValue(RepairAndLogisticsDAL.COL_NAME_PROBLEM_DESCRIPTION, Value)
        End Set
    End Property



    Public Property DeviceReceptionDate() As DateType
        Get
            CheckDeleted()
            If Row(RepairAndLogisticsDAL.COL_NAME_DEVICE_RECEPTION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(RepairAndLogisticsDAL.COL_NAME_DEVICE_RECEPTION_DATE), Date))
            End If
        End Get
        Set(Value As DateType)
            CheckDeleted()
            SetValue(RepairAndLogisticsDAL.COL_NAME_DEVICE_RECEPTION_DATE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=4000)> _
    Public Property ReplacementType() As String
        Get
            CheckDeleted()
            If Row(RepairAndLogisticsDAL.COL_NAME_REPLACEMENT_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RepairAndLogisticsDAL.COL_NAME_REPLACEMENT_TYPE), String)
            End If
        End Get
        Set(Value As String)
            CheckDeleted()
            SetValue(RepairAndLogisticsDAL.COL_NAME_REPLACEMENT_TYPE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1020)> _
    Public Property ReplacedDeviceManufacturer() As String
        Get
            CheckDeleted()
            If Row(RepairAndLogisticsDAL.COL_NAME_REPLACED_DEVICE_MANUFACTURER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RepairAndLogisticsDAL.COL_NAME_REPLACED_DEVICE_MANUFACTURER), String)
            End If
        End Get
        Set(Value As String)
            CheckDeleted()
            SetValue(RepairAndLogisticsDAL.COL_NAME_REPLACED_DEVICE_MANUFACTURER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=120)> _
    Public Property ReplacedDeviceModel() As String
        Get
            CheckDeleted()
            If Row(RepairAndLogisticsDAL.COL_NAME_REPLACED_DEVICE_MODEL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RepairAndLogisticsDAL.COL_NAME_REPLACED_DEVICE_MODEL), String)
            End If
        End Get
        Set(Value As String)
            CheckDeleted()
            SetValue(RepairAndLogisticsDAL.COL_NAME_REPLACED_DEVICE_MODEL, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=120)> _
    Public Property ReplacedDeviceSerialNumber() As String
        Get
            CheckDeleted()
            If Row(RepairAndLogisticsDAL.COL_NAME_REPLACED_DEVICE_SERIAL_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RepairAndLogisticsDAL.COL_NAME_REPLACED_DEVICE_SERIAL_NUMBER), String)
            End If
        End Get
        Set(Value As String)
            CheckDeleted()
            SetValue(RepairAndLogisticsDAL.COL_NAME_REPLACED_DEVICE_SERIAL_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=30)> _
    Public Property ReplacedDeviceIMEINumber() As String
        Get
            CheckDeleted()
            If Row(RepairAndLogisticsDAL.COL_NAME_REPLACED_DEVICE_IMEI_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RepairAndLogisticsDAL.COL_NAME_REPLACED_DEVICE_IMEI_NUMBER), String)
            End If
        End Get
        Set(Value As String)
            CheckDeleted()
            SetValue(RepairAndLogisticsDAL.COL_NAME_REPLACED_DEVICE_IMEI_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=72)> _
    Public Property ReplacedDeviceSku() As String
        Get
            CheckDeleted()
            If Row(RepairAndLogisticsDAL.COL_NAME_REPLACED_DEVICE_SKU) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RepairAndLogisticsDAL.COL_NAME_REPLACED_DEVICE_SKU), String)
            End If
        End Get
        Set(Value As String)
            CheckDeleted()
            SetValue(RepairAndLogisticsDAL.COL_NAME_REPLACED_DEVICE_SKU, Value)
        End Set
    End Property



    Public Property LaborAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(RepairAndLogisticsDAL.COL_NAME_LABOR_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(RepairAndLogisticsDAL.COL_NAME_LABOR_AMOUNT), Decimal))
            End If
        End Get
        Set(Value As DecimalType)
            CheckDeleted()
            SetValue(RepairAndLogisticsDAL.COL_NAME_LABOR_AMOUNT, Value)
        End Set
    End Property



    Public Property PartAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(RepairAndLogisticsDAL.COL_NAME_PART_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(RepairAndLogisticsDAL.COL_NAME_PART_AMOUNT), Decimal))
            End If
        End Get
        Set(Value As DecimalType)
            CheckDeleted()
            SetValue(RepairAndLogisticsDAL.COL_NAME_PART_AMOUNT, Value)
        End Set
    End Property



    Public Property ServiceCharge() As DecimalType
        Get
            CheckDeleted()
            If Row(RepairAndLogisticsDAL.COL_NAME_SERVICE_CHARGE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(RepairAndLogisticsDAL.COL_NAME_SERVICE_CHARGE), Decimal))
            End If
        End Get
        Set(Value As DecimalType)
            CheckDeleted()
            SetValue(RepairAndLogisticsDAL.COL_NAME_SERVICE_CHARGE, Value)
        End Set
    End Property



    Public Property ShippingAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(RepairAndLogisticsDAL.COL_NAME_SHIPPING_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(RepairAndLogisticsDAL.COL_NAME_SHIPPING_AMOUNT), Decimal))
            End If
        End Get
        Set(Value As DecimalType)
            CheckDeleted()
            SetValue(RepairAndLogisticsDAL.COL_NAME_SHIPPING_AMOUNT, Value)
        End Set
    End Property



    Public Property AuthorizedAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(RepairAndLogisticsDAL.COL_NAME_AUTHORIZED_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(RepairAndLogisticsDAL.COL_NAME_AUTHORIZED_AMOUNT), Decimal))
            End If
        End Get
        Set(Value As DecimalType)
            CheckDeleted()
            SetValue(RepairAndLogisticsDAL.COL_NAME_AUTHORIZED_AMOUNT, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=400)> _
    Public Property ServiceLevel() As String
        Get
            CheckDeleted()
            If Row(RepairAndLogisticsDAL.COL_NAME_SERVICE_LEVEL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RepairAndLogisticsDAL.COL_NAME_SERVICE_LEVEL), String)
            End If
        End Get
        Set(Value As String)
            CheckDeleted()
            SetValue(RepairAndLogisticsDAL.COL_NAME_SERVICE_LEVEL, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=800)> _
    Public Property ProblemFound() As String
        Get
            CheckDeleted()
            If Row(RepairAndLogisticsDAL.COL_NAME_PROBLEM_FOUND) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RepairAndLogisticsDAL.COL_NAME_PROBLEM_FOUND), String)
            End If
        End Get
        Set(Value As String)
            CheckDeleted()
            SetValue(RepairAndLogisticsDAL.COL_NAME_PROBLEM_FOUND, Value)
        End Set
    End Property

    Public Property VerificationNumber() As String
        Get
            CheckDeleted()
            If Row(RepairAndLogisticsDAL.COL_NAME_VERIFICATION_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RepairAndLogisticsDAL.COL_NAME_VERIFICATION_NUMBER), String)
            End If
        End Get
        Set(Value As String)
            CheckDeleted()
            SetValue(RepairAndLogisticsDAL.COL_NAME_VERIFICATION_NUMBER, Value)
        End Set
    End Property

    Public Property Company() As String
        Get
            CheckDeleted()
            If Row(RepairAndLogisticsDAL.COL_NAME_COMPANY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RepairAndLogisticsDAL.COL_NAME_COMPANY), String)
            End If
        End Get
        Set(Value As String)
            CheckDeleted()
            SetValue(RepairAndLogisticsDAL.COL_NAME_COMPANY, Value)
        End Set
    End Property

    Public Property ClaimVerificationNumLength() As LongType
        Get
            CheckDeleted()
            If Row(RepairAndLogisticsDAL.COL_NAME_CLAIM_VERIFICATION_NUM_LENGTH) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(RepairAndLogisticsDAL.COL_NAME_CLAIM_VERIFICATION_NUM_LENGTH), Long))
            End If
        End Get
        Set(Value As LongType)
            CheckDeleted()
            SetValue(RepairAndLogisticsDAL.COL_NAME_CLAIM_VERIFICATION_NUM_LENGTH, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New RepairAndLogisticsDAL
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


    Public Function UpdateVerificationNumber(strVerificationNumber As String, claimId As Nullable(Of Guid), claimAuthorizationId As Nullable(Of Guid)) As Boolean
        Try
            If ((claimAuthorizationId.HasValue) AndAlso (Not claimAuthorizationId.Value.Equals(Guid.Empty))) Then
                Dim oClaimAuthorization As ClaimAuthorization
                oClaimAuthorization = New ClaimAuthorization(claimAuthorizationId)
                oClaimAuthorization.VerificationNumber = strVerificationNumber
                oClaimAuthorization.Save()
            Else
                Dim dal As New RepairAndLogisticsDAL
                Return dal.UpdateVerificationNumber(strVerificationNumber, claimId)
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function getListFromArray(claimNumber As String, serialNumber As String, customerName As String, _
                                   taxID As String, authorizationNumber As String, _
                                   cellPhoneNumber As String, serviceCenterIds As ArrayList, _
                                   claimAuthorizationNumber As String, Optional ByVal sortBy As String = "CLNUM") As RepairLogisticsSearchDV

        Try
            Dim dal As New RepairAndLogisticsDAL
            Dim externalUserServiceCenterIds As ArrayList '= ElitaPlusIdentity.Current.ActiveUser.DealerOrSvcList
            Dim compIds As ArrayList, externalUserDealerId As Guid = Guid.Empty
            Dim dealerGroupCode As String = ""
            With ElitaPlusIdentity.Current.ActiveUser
                compIds = .Companies
                If .IsServiceCenter Then
                    externalUserServiceCenterIds = .DealerOrSvcList
                ElseIf .IsDealer Then
                    externalUserDealerId = .ScDealerId
                ElseIf .IsDealerGroup Then
                    Dim ExternalUserDealergroup As String = LookupListNew.GetCodeFromId("DEALER_GROUPS", .ScDealerId)
                    dealerGroupCode = ExternalUserDealergroup
                End If
            End With

            Dim errors() As ValidationError = {New ValidationError(ElitaPlus.Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR, GetType(Claim), Nothing, "Search", Nothing)}

            'Convert the Claim Number to UPPER Case
            If (Not (claimNumber.Equals(String.Empty))) Then
                claimNumber = claimNumber.ToUpper
            End If

            'Convert the Serial Number to UPPER Case
            If (Not (serialNumber.Equals(String.Empty))) Then
                serialNumber = serialNumber.ToUpper
            End If

            'Convert the Customer Name to UPPER Case
            If (Not (customerName.Equals(String.Empty))) Then
                customerName = customerName.ToUpper
            End If
            'Convert the Tax ID  to UPPER Case
            If (Not (taxID Is Nothing)) AndAlso (Not (taxID.Equals(String.Empty))) Then
                taxID = taxID.ToUpper
            End If
            'Convert the Authorization Number  to UPPER Case
            If (Not (claimAuthorizationNumber Is Nothing)) AndAlso (Not (claimAuthorizationNumber.Equals(String.Empty))) Then
                claimAuthorizationNumber = claimAuthorizationNumber.ToUpper
            End If
            'Convert the Authorization Number  to UPPER Case
            If (Not (authorizationNumber Is Nothing)) AndAlso (Not (authorizationNumber.Equals(String.Empty))) Then
                authorizationNumber = authorizationNumber.ToUpper
            End If


            'Check if the user has entered any search criteria... if NOT, then display an error
            If (claimNumber.Equals(String.Empty) AndAlso customerName.Equals(String.Empty) AndAlso _
                taxID.Equals(String.Empty) AndAlso authorizationNumber.Equals(String.Empty) AndAlso _
                serialNumber.Equals(String.Empty) AndAlso cellPhoneNumber.Equals(String.Empty) AndAlso _
                serviceCenterIds.Count = 0 AndAlso claimAuthorizationNumber.Equals(String.Empty)) Then
                Throw New BOValidationException(errors, GetType(Claim).FullName)
            End If
            'If oAppUser.IsServiceCenter Then
            '    Me.State.selectedServiceCenterIds = oAppUser.DealerOrSvcList
            Return New RepairLogisticsSearchDV(dal.LoadList(compIds, _
                                    claimNumber, serialNumber, customerName, taxID, _
                                    authorizationNumber, cellPhoneNumber, sortBy, _
                                    externalUserServiceCenterIds, serviceCenterIds, claimAuthorizationNumber, externalUserDealerId, dealerGroupCode).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Class RepairLogisticsSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_NAME_CLAIM_ID As String = "claim_id" 'RepairAndLogisticsDAL.COL_NAME_CLAIM_ID"
        Public Const COL_NAME_CLAIM_NUMBER As String = "clnum" 'RepairAndLogisticsDAL.COL_NAME_CLAIM_NUMBER"
        Public Const COL_NAME_CLAIM_STATUS As String = "status_code" 'RepairAndLogisticsDAL.COL_NAME_CLAIM_STATUS"
        Public Const COL_NAME_CUSTOMER_NAME As String = "custnm" 'RepairAndLogisticsDAL.COL_NAME_CUSTOMER_NAME"
        Public Const COL_NAME_TAX_ID As String = "taxid" 'RepairAndLogisticsDAL.COL_NAME_TAX_ID
        Public Const COL_NAME_CELL_PHONE_NUMBER As String = "cellnum" 'RepairAndLogisticsDAL.COL_NAME_CELL_PHONE_NUMBER
        Public Const COL_NAME_SERIAL_NUMBER As String = "sernum"
        Public Const COL_NAME_SERVICE_CENTER As String = "service_center" 'RepairAndLogisticsDAL.COL_NAME_SERVICE_CENTER"
        Public Const COL_NAME_VERIFICATION_NUMBER As String = "authnum" 'RepairAndLogisticsDAL.COL_NAME_AUTHORIZATION_NUMBER"
        Public Const COL_NAME_AUTHORIZATION_ID As String = "claim_authorization_id" 'RepairAndLogisticsDAL.COL_NAME_CLAIM_ID"
        Public Const COL_NAME_AUTHORIZATION_NUMBER As String = "authorization_number" 'RepairAndLogisticsDAL.COL_NAME_CLAIM_NUMBER"
        Public Const COL_NAME_AUTHORIZATION_STATUS_ID As String = "claim_authorization_status_id" 'RepairAndLogisticsDAL.COL_NAME_CLAIM_STATUS"



#End Region

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

#End Region

End Class



