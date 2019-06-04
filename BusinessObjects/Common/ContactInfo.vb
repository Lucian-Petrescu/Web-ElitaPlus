'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (11/2/2011)  ********************

Public Class ContactInfo
    Inherits BusinessObjectBase

    Public Interface IContactInfoUser
        Property ContactInfoId() As Guid
    End Interface

#Region "Private Attributes"

    Private _userObj As IContactInfoUser = Nothing

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

    'Exiting BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDS As DataSet, ByVal userObj As IContactInfoUser)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load(id)
        Me._userObj = userObj
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDS As DataSet, ByVal userObj As IContactInfoUser, ByVal Flg As Boolean)
        MyBase.New(Flg)
        Me.Dataset = familyDS
        Me.Load(id)
        Me._userObj = userObj
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As DataSet, ByVal userObj As IContactInfoUser)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load()
        Me._userObj = userObj
    End Sub



    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Me.Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New ContactInfoDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            setvalue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New ContactInfoDAL
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

        ''REQ-784
        'Me._address = Me.Address(Me.Dataset)
        'Me._address.CountryId =??

    End Sub

    Private _countryObject As Country

#End Region

    ''    ''REQ-784
    ''#Region "public Members"
    ''    Public AddressBO As Address
    ''#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If row(ContactInfoDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ContactInfoDAL.COL_NAME_CONTACT_INFO_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property AddressTypeId() As Guid
        Get
            CheckDeleted()
            If Row(ContactInfoDAL.COL_NAME_ADDRESS_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContactInfoDAL.COL_NAME_ADDRESS_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContactInfoDAL.COL_NAME_ADDRESS_TYPE_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property AddressId() As Guid
        Get
            CheckDeleted()
            If row(ContactInfoDAL.COL_NAME_ADDRESS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ContactInfoDAL.COL_NAME_ADDRESS_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContactInfoDAL.COL_NAME_ADDRESS_ID, Value)
        End Set
    End Property


    <RequiredFieldBySetting("", Nothing, "[SALU]")> _
    Public Property SalutationId() As Guid
        Get
            CheckDeleted()
            If Row(ContactInfoDAL.COL_NAME_SALUTATION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ContactInfoDAL.COL_NAME_SALUTATION_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ContactInfoDAL.COL_NAME_SALUTATION_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50), RequiredFieldBySetting("", Nothing, "[NAME]")> _
    Public Property Name() As String
        Get
            CheckDeleted()
            If Row(ContactInfoDAL.COL_NAME_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ContactInfoDAL.COL_NAME_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ContactInfoDAL.COL_NAME_NAME, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=20), RequiredFieldBySetting("", Nothing, "[HPHONE]")> _
    Public Property HomePhone() As String
        Get
            CheckDeleted()
            If Row(ContactInfoDAL.COL_NAME_HOME_PHONE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ContactInfoDAL.COL_NAME_HOME_PHONE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ContactInfoDAL.COL_NAME_HOME_PHONE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=20), RequiredFieldBySetting("", Nothing, "[WPHONE]")> _
    Public Property WorkPhone() As String
        Get
            CheckDeleted()
            If Row(ContactInfoDAL.COL_NAME_WORK_PHONE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ContactInfoDAL.COL_NAME_WORK_PHONE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ContactInfoDAL.COL_NAME_WORK_PHONE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50), RequiredFieldBySetting("", Nothing, "[EMAIL]")> _
    Public Property Email() As String
        Get
            CheckDeleted()
            If Row(ContactInfoDAL.COL_NAME_EMAIL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ContactInfoDAL.COL_NAME_EMAIL), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ContactInfoDAL.COL_NAME_EMAIL, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=20), RequiredFieldBySetting("", Nothing, "[CPHONE]")> _
    Public Property CellPhone() As String
        Get
            CheckDeleted()
            If Row(ContactInfoDAL.COL_NAME_CELL_PHONE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ContactInfoDAL.COL_NAME_CELL_PHONE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ContactInfoDAL.COL_NAME_CELL_PHONE, Value)
        End Set
    End Property

    Private _address As Address = Nothing
    Public ReadOnly Property Address(ByVal parentDataSet As DataSet) As Address
        Get
            If Me._address Is Nothing Then
                If Me.AddressId.Equals(Guid.Empty) Then
                    Me._address = New Address(parentDataSet, Nothing)
                    Me.AddressId = Me._address.Id
                Else
                    Me._address = New Address(Me.AddressId, parentDataSet, Nothing)
                End If
            End If
            Return Me._address
        End Get
    End Property

    Public ReadOnly Property Address() As Address
        Get
            Return Me.Address(Me.Dataset)
        End Get
    End Property

    Public ReadOnly Property ContactInfoReqFields() As String
        Get
            If Me._countryObject Is Nothing Then
                If Not Me._address Is Nothing AndAlso Not Me._address.CountryId.Equals(Guid.Empty) Then
                    _countryObject = New Country(Me._address.CountryId)
                Else
                    _countryObject = ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID)
                End If
            End If
            Return _countryObject.ContactInfoReqFields
        End Get
    End Property

    <ValidStringLength("", Max:=200)> _
    Public Property Company() As String
        Get
            CheckDeleted()
            If row(ContactInfoDAL.COL_NAME_COMPANY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ContactInfoDAL.COL_NAME_COMPANY), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ContactInfoDAL.COL_NAME_COMPANY, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200)> _
    Public Property JobTitle() As String
        Get
            CheckDeleted()
            If row(ContactInfoDAL.COL_NAME_JOB_TITLE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ContactInfoDAL.COL_NAME_JOB_TITLE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ContactInfoDAL.COL_NAME_JOB_TITLE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=25)> _
    Public Property FirstName() As String
        Get
            CheckDeleted()
            If Row(ContactInfoDAL.COL_NAME_FIRST_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ContactInfoDAL.COL_NAME_FIRST_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ContactInfoDAL.COL_NAME_FIRST_NAME, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=25)> _
    Public Property LastName() As String
        Get
            CheckDeleted()
            If Row(ContactInfoDAL.COL_NAME_LAST_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ContactInfoDAL.COL_NAME_LAST_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ContactInfoDAL.COL_NAME_LAST_NAME, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New ContactInfoDAL
                dal.Update(Me.Row)
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
#End Region

#Region "DataView Retrieveing Methods"

#End Region

#Region "Handle Contact Info Required Fields"
    Public Class ContactInfoRequiredFieldsItem
        Private _ItemName As String
        Private _Required As Boolean
        Public Sub New()
            _ItemName = String.Empty
            _Required = True
        End Sub
        Public Sub New(ByVal ItemName As String, Optional ByVal Required As Boolean = True)
            _ItemName = ItemName
            _Required = Required
        End Sub
        Public Property ItemName() As String
            Get
                Return _ItemName
            End Get
            Set(ByVal Value As String)
                _ItemName = Value
            End Set
        End Property
        Public Property Required() As Boolean
            Get
                Return _Required
            End Get
            Set(ByVal Value As Boolean)
                Required = Value
            End Set
        End Property
    End Class
    Public Shared Sub SplitContactInfoRequiredFieldsString(ByVal ContactInfoReqFieldsStr As String, ByRef AddressComponents As Collections.Generic.List(Of ContactInfoRequiredFieldsItem))
        ContactInfoReqFieldsStr = ContactInfoReqFieldsStr.Trim
        'MailAddrFmtStr = "[ADR1][-][\n][ADR2][\n][ZIP][Space][CITY][Space][COU][\n][RGNAME]*[,][Space][RGCODE]"
        If ContactInfoReqFieldsStr.Trim <> "" Then
            AddressComponents = New Collections.Generic.List(Of ContactInfoRequiredFieldsItem)(15)
            Dim RegExp As Text.RegularExpressions.Regex, blnRequired As Boolean
            RegExp = New Text.RegularExpressions.Regex("\[(.)+?\](\*)*")
            Dim m As Text.RegularExpressions.Match = RegExp.Match(ContactInfoReqFieldsStr)
            While (m.Success)
                blnRequired = True
                If m.Value.Trim.EndsWith("]*") Then blnRequired = False
                AddressComponents.Add(New ContactInfoRequiredFieldsItem(m.Value.Trim, blnRequired))
                m = m.NextMatch()
            End While
        End If
    End Sub
    Public Shared Function IsAddressComponentRequired(ByVal MailAddrFmtStr As String, ByVal strComponent As String) As Boolean
        Dim blnRequired As Boolean = False
        MailAddrFmtStr = MailAddrFmtStr.Trim
        If MailAddrFmtStr.Trim <> "" Then
            Dim RegExp As Text.RegularExpressions.Regex = New Text.RegularExpressions.Regex("\[(.+?)\](\*)*")
            Dim m As Text.RegularExpressions.Match = RegExp.Match(MailAddrFmtStr)
            While (m.Success)
                If m.Groups(1).Value.Trim = strComponent.Trim AndAlso (Not m.Value.Trim.EndsWith("]*")) Then
                    blnRequired = True
                    Exit While
                End If
                m = m.NextMatch()
            End While
        End If
        Return blnRequired
    End Function
#End Region

#Region "Custom Validation"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class RequiredFieldBySetting
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String, ByVal x As String, ByVal shortName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR)
            propertyShortName = shortName
        End Sub

        Private _propertyShortName As String = Nothing
        Private Property propertyShortName() As String
            Get
                Return _propertyShortName
            End Get
            Set(ByVal Value As String)
                _propertyShortName = Value
            End Set
        End Property

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ContactInfo = CType(objectToValidate, ContactInfo)

            If InStr(obj.ContactInfoReqFields, propertyShortName) > 0 Then
                If valueToCheck Is Nothing OrElse valueToCheck.Equals(String.Empty) Then
                    Return False
                Else
                    Return True
                End If
            Else
                Return True
            End If

        End Function
    End Class
#End Region

End Class


