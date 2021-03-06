Imports Assurant.ElitaPlus.BusinessObjectsNew.ContactInfo

'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (1/3/2013)  ********************

Public Class CustRegistration
    Inherits BusinessObjectBase
    Implements IContactInfoUser

#Region "Constants"

    Public Const NO_RECORDS_FOUND = "NO RECORDS FOUND."
    Public Const OK_RESPONSE = "OK"
    Public Const DEFAULT_CELL_NUMBER = "0000000000"

#End Region

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
            Dim dal As New CustRegistrationDAL
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
            Dim dal As New CustRegistrationDAL
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
            If Row(CustRegistrationDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CustRegistrationDAL.COL_NAME_REGISTRATION_ID), Byte()))
            End If
        End Get
    End Property

    <ValidStringLength("", Max:=80)> _
    Public Property TaxId() As String
        Get
            CheckDeleted()
            If Row(CustRegistrationDAL.COL_NAME_TAX_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CustRegistrationDAL.COL_NAME_TAX_ID), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CustRegistrationDAL.COL_NAME_TAX_ID, Value)
        End Set
    End Property

    Public Property DealerId() As Guid
        Get
            CheckDeleted()
            If Row(CustRegistrationDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CustRegistrationDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CustRegistrationDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property

    Public Property ContactInfoId() As Guid Implements IContactInfoUser.ContactInfoId
        Get
            CheckDeleted()
            If Row(CustRegistrationDAL.COL_NAME_CONTACT_INFO_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CustRegistrationDAL.COL_NAME_CONTACT_INFO_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CustRegistrationDAL.COL_NAME_CONTACT_INFO_ID, Value)
        End Set
    End Property

    Private _contactInfo As ContactInfo = Nothing
    Public ReadOnly Property ContactInfo() As ContactInfo
        Get
            If Me._contactInfo Is Nothing Then
                If Me.ContactInfoId.Equals(Guid.Empty) Then
                    Me._contactInfo = New ContactInfo(Me.Dataset, Me)
                    Me.ContactInfoId = Me._contactInfo.Id
                Else
                    Me._contactInfo = New ContactInfo(Me.ContactInfoId, Me.Dataset, Me)
                End If
            End If
            Return Me._contactInfo
        End Get
    End Property

    Public Shared Function GetCountryID(ByVal countryCode As String) As Guid
        Dim countryID As Guid = Guid.Empty
        Dim list As DataView = LookupListNew.GetCountryLookupList()

        countryID = LookupListNew.GetIdFromCode(list, countryCode)

        If countryID = Guid.Empty Then
            Dim errors() As ValidationError = {New ValidationError(Common.ErrorCodes.INVALID_COUNTRY, GetType(CustRegistration), Nothing, "", Nothing)}
            Throw New BOValidationException(errors, GetType(CustRegistration).FullName)
        End If

        Return countryID
    End Function

    Public Shared Function GetRegionID(ByVal regionCode As String, ByVal countryID As Guid) As Guid
        Dim regionID As Guid = Guid.Empty
        If Not regionCode.Trim = String.Empty Then
            Dim list As DataView = LookupListNew.GetRegionLookupList(countryID)

            regionID = LookupListNew.GetIdFromCode(list, regionCode)

            If regionID = Guid.Empty Then
                Dim errors() As ValidationError = {New ValidationError(Common.ErrorCodes.INVALID_REGION_CODE, GetType(CustRegistration), Nothing, "", Nothing)}
                Throw New BOValidationException(errors, GetType(CustRegistration).FullName)
            End If
        End If
        Return regionID
    End Function

    Public Shared Function GetDealerID(ByVal dealerCode As String) As Guid
        Dim dealerId As Guid = Guid.Empty
        Dim list As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)

        dealerId = LookupListNew.GetIdFromCode(list, dealerCode)

        If dealerId = Guid.Empty Then
            Dim errors() As ValidationError = {New ValidationError(Common.ErrorCodes.INVALID_DEALER_CODE, GetType(CustRegistration), Nothing, "", Nothing)}
            Throw New BOValidationException(errors, GetType(CustRegistration).FullName)
        End If

        Return dealerId
    End Function

    Public Shared Function GetAddressTypeID(ByVal addressTypeCode As String) As Guid
        Dim addressTypeId As Guid = Guid.Empty
        Dim list As DataView = LookupListNew.GetAddressTypeList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)

        addressTypeId = LookupListNew.GetIdFromCode(list, addressTypeCode)

        Return addressTypeId
    End Function

    Public Shared Function IsEmailIDValid(ByVal emailId As String) As Boolean
        If MiscUtil.EmailAddressValidation(emailId) = False Then
            Dim errors() As ValidationError = {New ValidationError(Common.ErrorCodes.GUI_EMAIL_IS_INVALID_ERR, GetType(CustRegistration), Nothing, "", Nothing)}
            Throw New BOValidationException(errors, GetType(CustRegistration).FullName)
        Else
            Return True
        End If
    End Function

    Public Function IsEmailIDUsed(ByVal emailId As String, ByVal dealerId As Guid) As Boolean
        Dim dal As New CustRegistrationDAL

        Dim cnt As Integer

        cnt = dal.CheckEmail(emailId, GetAddressTypeID(Codes.ADDRESS_TYPE__BILLING), dealerId).Tables(0).Rows(0)("cnt")

        If cnt > 0 Then
            Dim errors() As ValidationError = {New ValidationError(Common.ErrorCodes.EMAIL_IS_IN_USE, GetType(CustRegistration), Nothing, "", Nothing)}
            Throw New BOValidationException(errors, GetType(CustRegistration).FullName)
        Else
            Return False
        End If

    End Function

#End Region

#Region "Public Members"
    Public Function CreateRegistrationElements(ByVal customerRegistration As CustRegistrationDC) As String
        Try
            Dim countryId As Guid
            Dim regionID As Guid
            Dim dealerId As Guid
            Dim addressTypeId As Guid


            If IsEmailIDValid(customerRegistration.EmailID) Then
                countryId = GetCountryID(customerRegistration.CountryCode)
                If Not customerRegistration.State Is Nothing Then
                    regionID = GetRegionID(customerRegistration.State, countryId)
                End If
                dealerId = GetDealerID(customerRegistration.DealerCode)
                addressTypeId = GetAddressTypeID(Codes.ADDRESS_TYPE__BILLING)

                If Not IsEmailIDUsed(customerRegistration.EmailID, dealerId) Then
                    'assign properties to the BO and child BOs
                    Me.TaxId = customerRegistration.TaxID
                    Me.DealerId = dealerId
                    'add other properties
                    Me.ContactInfo.AddressTypeId = addressTypeId
                    Me.ContactInfo.FirstName = customerRegistration.FirstName
                    Me.ContactInfo.LastName = customerRegistration.LastName
                    Me.ContactInfo.Name = customerRegistration.FirstName + customerRegistration.LastName
                    If customerRegistration.Phone Is Nothing OrElse customerRegistration.Phone.Trim = String.Empty Then
                        Me.ContactInfo.CellPhone = DEFAULT_CELL_NUMBER
                    Else
                        Me.ContactInfo.CellPhone = customerRegistration.Phone
                    End If
                    Me.ContactInfo.Email = customerRegistration.EmailID
                    'add other properties
                    Me.ContactInfo.Address(Me.Dataset).Address1 = customerRegistration.Address1
                    Me.ContactInfo.Address(Me.Dataset).Address2 = customerRegistration.Address2
                    Me.ContactInfo.Address(Me.Dataset).City = customerRegistration.City
                    Me.ContactInfo.Address(Me.Dataset).RegionId = regionID
                    Me.ContactInfo.Address(Me.Dataset).PostalCode = customerRegistration.PostalCode
                    Me.ContactInfo.Address(Me.Dataset).CountryId = countryId

                    'Calls required to invoke validator
                    Me.ContactInfo.Address.Save()
                    Me.ContactInfo.Save()

                    Me.Save()
                    Return OK_RESPONSE
                End If

            End If
        Catch ex As BOValidationException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdateRegistrationElements(ByVal customerRegistration As CustRegistrationDC) As String
        Try
            Dim countryId As Guid
            Dim regionID As Guid
            Dim dealerId As Guid
            Dim addressTypeId As Guid


            If IsEmailIDValid(customerRegistration.EmailID) Then
                countryId = GetCountryID(customerRegistration.CountryCode)
                If Not customerRegistration.State Is Nothing Then
                    regionID = GetRegionID(customerRegistration.State, countryId)
                End If
                'dealerId = GetDealerID(customerRegistration.DealerCode)
                'addressTypeId = GetAddressTypeID(ADDRESS_TYPE_CODE_BILLING)

                'Me.BeginEdit()
                'assign properties to the BO and child BOs
                'Me.TaxId = customerRegistration.TaxID

                'add other properties                
                Me.ContactInfo.FirstName = customerRegistration.FirstName
                Me.ContactInfo.LastName = customerRegistration.LastName
                Me.ContactInfo.Name = customerRegistration.FirstName + customerRegistration.LastName
                If customerRegistration.Phone Is Nothing OrElse customerRegistration.Phone.Trim = String.Empty Then
                    Me.ContactInfo.CellPhone = DEFAULT_CELL_NUMBER
                Else
                    Me.ContactInfo.CellPhone = customerRegistration.Phone
                End If

                'add other properties
                Me.ContactInfo.Address(Me.Dataset).Address1 = customerRegistration.Address1
                Me.ContactInfo.Address(Me.Dataset).Address2 = customerRegistration.Address2
                Me.ContactInfo.Address(Me.Dataset).City = customerRegistration.City
                Me.ContactInfo.Address(Me.Dataset).RegionId = regionID
                Me.ContactInfo.Address(Me.Dataset).PostalCode = customerRegistration.PostalCode
                Me.ContactInfo.Address(Me.Dataset).CountryId = countryId

                'Calls required to invoke validator
                Me.ContactInfo.Address.Save()
                Me.ContactInfo.Save()

                Me.Save()

                Return OK_RESPONSE

            End If
        Catch ex As BOValidationException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New CustRegistrationDAL
                'dal.Update(Me.Row)
                dal.UpdateFamily(Me.Dataset)
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

    'Added manually to the code
    Public Overrides ReadOnly Property IsDirty() As Boolean
        Get
            Return MyBase.IsDirty OrElse Me.IsFamilyDirty
        End Get
    End Property
#End Region

#Region "Data Retrieveing Methods"
    Public Shared Function GetRegistration(ByVal emailId As String, ByVal dealerId As Guid) As DataSet
        Try
            Dim dal As New CustRegistrationDAL
            Dim ds As DataSet

            If (emailId = String.Empty) Then
                Dim errors() As ValidationError = {New ValidationError(Common.ErrorCodes.GUI_EMAIL_IS_REQUIRED_ERR, GetType(CustRegistration), Nothing, "", Nothing)}
                Throw New BOValidationException(errors, GetType(CustRegistration).FullName)
            ElseIf IsEmailIDValid(emailId) Then
                ds = dal.LoadList(emailId, GetAddressTypeID(Codes.ADDRESS_TYPE__BILLING), dealerId)
                If ds.Tables(0).Rows.Count = 0 Then
                    Dim errors() As ValidationError = {New ValidationError(Common.ErrorCodes.WS_NO_RECORDS_FOUND, GetType(CustRegistration), Nothing, "", Nothing)}
                    Throw New BOValidationException(errors, GetType(CustRegistration).FullName)
                Else
                    Return ds
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetRegistration(ByVal emailId As String, ByVal dealerCode As String) As Guid
        Try
            'call methods to retrieve addressTypeId and dealerId from code
            Dim dealerId As Guid
            Dim addressTypeId As Guid
            Dim registrationId As Guid

            Dim dal As New CustRegistrationDAL

            dealerId = GetDealerID(dealerCode)
            addressTypeId = GetAddressTypeID(Codes.ADDRESS_TYPE__BILLING)

            If (emailId = String.Empty) Then
                Dim errors() As ValidationError = {New ValidationError(Common.ErrorCodes.GUI_EMAIL_IS_REQUIRED_ERR, GetType(CustRegistration), Nothing, "", Nothing)}
                Throw New BOValidationException(errors, GetType(CustRegistration).FullName)
            ElseIf IsEmailIDValid(emailId) Then
                registrationId = dal.GetRegistration(emailId, addressTypeId, dealerId)
                If registrationId = Guid.Empty Then
                    Dim errors() As ValidationError = {New ValidationError(Common.ErrorCodes.WS_NO_RECORDS_FOUND, GetType(CustRegistration), Nothing, "", Nothing)}
                    Throw New BOValidationException(errors, GetType(CustRegistration).FullName)
                Else
                    Return registrationId
                End If
            End If

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

End Class


