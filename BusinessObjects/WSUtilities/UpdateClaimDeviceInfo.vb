Public Class UpdateClaimDeviceInfo
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const DATA_COL_NAME_DEALER_CODE As String = "DEALER_CODE"
    Public Const DATA_COL_NAME_CERT_NUMBER As String = "CERT_NUMBER"
    Public Const DATA_COL_NAME_CLAIM_NUMBER As String = "CLAIM_NUMBER"
    Public Const DATA_COL_NAME_CLAIM_TYPE As String = "CLAIM_TYPE"
    Public Const DATA_COL_NAME_NEW_IMEI_DATE As String = "NEW_IMEI_DATE"
    Public Const DATA_COL_NAME_MAKE As String = "MAKE"
    Public Const DATA_COL_NAME_MODEL As String = "MODEL"
    Public Const DATA_COL_NAME_SERIAL_NUMBER As String = "SERIAL_NUMBER"
    Public Const DATA_COL_NAME_SKU As String = "SKU"

    Private Const TABLE_NAME As String = "UpdateClaimDeviceInfo"
    Private Const TABLE_NAME__GET_CUSTOMER_FUNCTIONS_RESPONSE As String = "UpdateClaimDeviceInfoResponse"
    Private Const TABLE_NAME__RESPONSE_STATUS As String = "ResponseStatus"
    Private Const DATASET_NAME__CLAIM_CHECK_RESPONSE As String = "ClaimCheckResponse"

    Private Const CLAIM_TYPE_REPAIR = "RPR"
    Private Const CLAIM_TYPE_REPLACEMENT = "RT"

    Private Const SKU_VALIDATE_NO = "NO"



#End Region

#Region "Variables"
    Private _dealerId As Guid = Guid.Empty
    Private _certId As Guid = Guid.Empty
    Private _claimId As Guid = Guid.Empty
    Private _manufacturerId As Guid = Guid.Empty
    Private _validateSku As String = String.Empty
    Private _warrantyMasterModel As String = String.Empty
#End Region

#Region "Constructors"

    Public Sub New(ds As UpdateClaimDeviceInfoDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

    End Sub

#End Region

#Region "Properties"

    <ValueMandatory("")> _
    Public Property DealerCode As String
        Get
            If Row(DATA_COL_NAME_DEALER_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_DEALER_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_DEALER_CODE, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property CertificateNumber As String
        Get
            If Row(DATA_COL_NAME_CERT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_CERT_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_CERT_NUMBER, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property ClaimNumber As String
        Get
            If Row(DATA_COL_NAME_CLAIM_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_CLAIM_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_CLAIM_NUMBER, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property ClaimType As String
        Get
            If Row(DATA_COL_NAME_CLAIM_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_CLAIM_TYPE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_CLAIM_TYPE, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property NewIMEIDate As DateType
        Get
            If Row(DATA_COL_NAME_NEW_IMEI_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_NEW_IMEI_DATE), DateTime)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_NEW_IMEI_DATE, Value)
        End Set
    End Property

    Public Property Make As String
        Get
            If Row(DATA_COL_NAME_MAKE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_MAKE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_MAKE, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property Model As String
        Get
            If Row(DATA_COL_NAME_MODEL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_MODEL), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_MODEL, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property SerialNumber As String
        Get
            If Row(DATA_COL_NAME_SERIAL_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_SERIAL_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_SERIAL_NUMBER, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property SKU As String
        Get
            If Row(DATA_COL_NAME_SKU) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_SKU), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_SKU, Value)
        End Set
    End Property

#End Region

#Region "Private Members"

    Private Sub MapDataSet(ds As UpdateClaimDeviceInfoDs)

        Dim schema As String = ds.GetXmlSchema '.Replace(SOURCE_COL_MAKE, DATA_COL_NAME_MANUFACTURER).Replace(SOURCE_COL_MILEAGE, DATA_COL_NAME_ODOMETER).Replace(SOURCE_COL_NEWUSED, DATA_COL_NAME_CONDITION)

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

    Private Sub Load(ds As UpdateClaimDeviceInfoDs)
        Try
            Initialize()
            Dim newRow As DataRow = Dataset.Tables(TABLE_NAME).NewRow
            Row = newRow
            PopulateBOFromWebService(ds)
            Dataset.Tables(TABLE_NAME).Rows.Add(newRow)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As BOValidationException
            Throw ex
        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("UpdateClaimDeviceInfo Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ds As UpdateClaimDeviceInfoDs)
        Try
            If ds.UpdateClaimDeviceInfo.Count = 0 Then Exit Sub
            With ds.UpdateClaimDeviceInfo.Item(0)
                DealerCode = .DEALER_CODE
                CertificateNumber = .CERT_NUMBER
                ClaimNumber = .CLAIM_NUMBER
                ClaimType = .CLAIM_TYPE
                NewIMEIDate = .NEW_IMEI_DATE
                Make = .MAKE
                Model = .MODEL
                SerialNumber = .SERIAL_NUMBER
                SKU = .SKU

            End With
        Catch ex As BOValidationException
            Throw ex
        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("UpdateClaimDeviceInfo Invalid Parameters Error", Common.ErrorCodes.BO_INVALID_DATA, ex)
        End Try
    End Sub

    Protected Shadows Sub CheckDeleted()
    End Sub

    Private Function FindDealer()

        Dim list As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)

        _dealerId = LookupListNew.GetIdFromCode(list, DealerCode)

        If _dealerId = Guid.Empty Then
            Throw New BOValidationException("UpdateClaimDeviceInfo Error: ", Common.ErrorCodes.WS_DEALER_NOT_FOUND)
        End If

    End Function

    Private Function FindCertificate()

        Dim dal As New CertificateDAL
        Dim dsCert As DataSet = dal.GetCertIDWithCertNumAndDealer(CertificateNumber, _dealerId)
        Dim strResult As String = String.Empty

        If dsCert IsNot Nothing AndAlso dsCert.Tables.Count > 0 AndAlso dsCert.Tables(0).Rows.Count = 1 Then
            If dsCert.Tables(0).Rows(0).Item("Cert_ID") Is DBNull.Value Then
                Throw New BOValidationException("UpdateClaimDeviceInfo Error: ", Common.ErrorCodes.ERR_NO_CERTIFICATE_FOUND)
            Else
                _certId = New Guid(CType(dsCert.Tables(0).Rows(0).Item("Cert_ID"), Byte()))
                If _certId.Equals(Guid.Empty) Then
                    Throw New BOValidationException("UpdateClaimDeviceInfo Error: ", Common.ErrorCodes.ERR_NO_CERTIFICATE_FOUND)
                End If
                'check whether the certificate is active
                If (dsCert.Tables(0).Rows(0)("status_code") Is String.Empty OrElse dsCert.Tables(0).Rows(0)("status_code") <> "A") Then
                    Throw New BOValidationException("UpdateClaimDeviceInfo Error: ", Common.ErrorCodes.MSG_INVALID_CERTIFICATE_STATUS)
                End If
            End If
        Else
            Throw New BOValidationException("UpdateClaimDeviceInfo Error: ", Common.ErrorCodes.ERR_NO_CERTIFICATE_FOUND)
        End If
        dsCert.Dispose()


    End Function

    Private Function FindClaim()

        Dim dal As New ClaimDAL
        'what will happen if the certificate has > 1 claim with the same claim number, which one to pick
        Dim dsClaim As DataSet = dal.GetClaimIDWithCertAndDealer(_certId, _dealerId, ClaimNumber)
        Dim strResult As String = String.Empty
        Dim ResponseStatus As DataTable

        If dsClaim IsNot Nothing AndAlso dsClaim.Tables.Count > 0 AndAlso dsClaim.Tables(0).Rows.Count = 1 Then
            If dsClaim.Tables(0).Rows(0).Item("Claim_ID") Is DBNull.Value Then
                Throw New BOValidationException("UpdateClaimDeviceInfo Error: ", Common.ErrorCodes.WS_CLAIM_NOT_FOUND)
            Else
                _claimId = New Guid(CType(dsClaim.Tables(0).Rows(0).Item("Claim_ID"), Byte()))
                If _claimId.Equals(Guid.Empty) Then
                    Throw New BOValidationException("UpdateClaimDeviceInfo Error: ", Common.ErrorCodes.WS_CLAIM_NOT_FOUND)
                End If
            End If
        Else
            Throw New BOValidationException("UpdateClaimDeviceInfo Error: ", Common.ErrorCodes.WS_CLAIM_NOT_FOUND)
        End If

        dsClaim.Dispose()

    End Function

    Private Function validateSKU()

        Dim dealerBO As Dealer = New Dealer(_dealerId)
        Dim WMDAL As New WarrantyMasterDAL
        _validateSku = LookupListNew.GetCodeFromId("SKUVAL", dealerBO.ValidateSKUId)
        If (_validateSku <> SKU_VALIDATE_NO) Then

            Dim dsMakeModel As DataSet = WMDAL.GetMakeAndModelForDealerFromWM(SKU, _dealerId)
            Dim dsMfgModel As DataSet
            If dsMakeModel IsNot Nothing AndAlso dsMakeModel.Tables.Count > 0 AndAlso dsMakeModel.Tables(0).Rows.Count = 1 Then

                _manufacturerId = New Guid(CType(dsMakeModel.Tables(0).Rows(0)("Internal_manufacturer_id"), Byte()))
                _warrantyMasterModel = dsMakeModel.Tables(0).Rows(0)("Model_Number")

                'check whether this make and model are present in elp_mfg_model table

                Dim MfgModelDAL As New MfgModelDAL
                dsMfgModel = MfgModelDAL.GetMakeAndModelForDealer(_manufacturerId, _warrantyMasterModel, _dealerId)

                'reject if the make and model are available in the table
                If dsMfgModel IsNot Nothing AndAlso dsMfgModel.Tables.Count > 0 AndAlso dsMfgModel.Tables(0).Rows.Count = 1 Then
                    Throw New BOValidationException("UpdateClaimDeviceInfo Error: ", Common.ErrorCodes.INVALID_MAKE_MODEL_ERR)
                End If

            Else
                'invalid sku error message
                Throw New BOValidationException("UpdateClaimDeviceInfo Error: ", Common.ErrorCodes.INVALID_SKU)
            End If

            dsMfgModel.Dispose()
            dsMakeModel.Dispose()

        End If
        Return String.Empty

    End Function
#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String
        Try

            Validate()

            'validate the dealer code
            If DealerCode IsNot Nothing AndAlso DealerCode.Trim <> String.Empty Then
                FindDealer()
            End If

            'validate the certificate number
            If CertificateNumber IsNot Nothing AndAlso CertificateNumber.Trim <> String.Empty Then
                FindCertificate()
            End If

            'validate the claim number
            If ClaimNumber IsNot Nothing AndAlso ClaimNumber.Trim <> String.Empty Then
                FindClaim()
            End If

            'Get the Claim information
            Dim myBO As Claim = New Claim(_claimId)

            'validate the incoming IMEI Date
            If (NewIMEIDate < myBO.GetShortDate(myBO.LossDate)) Then
                Throw New BOValidationException("UpdateClaimDeviceInfo Error: ", Common.ErrorCodes.MSG_INVALID_NEW_IMEI_DATE)
            End If

            'validate SKU
            validateSKU()

            'Get the Claim Equipment BO
            Dim ClaimEquipBO As ClaimEquipment = myBO.ClaimedEquipment
            ClaimEquipBO.ClaimEquipmentDate = NewIMEIDate
            ClaimEquipBO.ManufacturerId = _manufacturerId
            ClaimEquipBO.Model = _warrantyMasterModel
            ClaimEquipBO.SerialNumber = SerialNumber
            ClaimEquipBO.SKU = SKU

            'set the claim type 
            If (ClaimType = CLAIM_TYPE_REPAIR) Then
                myBO.MethodOfRepairId = LookupListNew.GetIdFromCode(LookupListCache.LK_METHODS_OF_REPAIR, Codes.METHOD_OF_REPAIR__CARRY_IN)
            ElseIf (ClaimType = CLAIM_TYPE_REPLACEMENT) Then
                myBO.MethodOfRepairId = LookupListNew.GetIdFromCode(LookupListCache.LK_METHODS_OF_REPAIR, Codes.METHOD_OF_REPAIR__REPLACEMENT)
            Else
                Throw New BOValidationException("UpdateClaimDeviceInfo Error: ", Common.ErrorCodes.INVALID_CLAIM_TYPE_CODE)
            End If

            'save the data
            ClaimEquipBO.Save()
            myBO.Save()

            ' Set the acknoledge OK response
            Return XMLHelper.GetXML_OK_Response

        Catch ex As BOValidationException
            Throw ex
        Catch ex As StoredProcedureGeneratedException
            Throw ex
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

#End Region

#Region "Extended Properties"
#End Region
End Class
