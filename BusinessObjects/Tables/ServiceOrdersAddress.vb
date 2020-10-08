'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (10/25/2007)  ********************

Public Class ServiceOrdersAddress
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


    'Exiting BO attaching to a BO family using parent's Id
    Public Sub New(familyDS As DataSet, id As Guid)
        MyBase.New(False)
        Dataset = familyDS
        LoadUsingParentId(id)
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
            Dim dal As New ServiceOrdersAddressDAL
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

    Protected Sub Load(id As Guid)
        Try
            Dim dal As New ServiceOrdersAddressDAL
            If _isDSCreator Then
                If Row IsNot Nothing Then
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
    Protected Sub LoadUsingParentId(id As Guid)
        Try
            Dim dal As New ServiceOrdersAddressDAL
            If _isDSCreator Then
                If Row IsNot Nothing Then
                    Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Row)
                End If
            End If
            Row = Nothing
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Row = FindRow(id, dal.COL_NAME_DEALER_ID, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.Load(id, Dataset)
                Row = FindRow(id, dal.COL_NAME_DEALER_ID, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then
                ' Throw New DataNotFoundException
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
            If row(ServiceOrdersAddressDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ServiceOrdersAddressDAL.COL_NAME_SERVICE_ORDERS_ADDRESS_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property DealerId As Guid
        Get
            CheckDeleted()
            If row(ServiceOrdersAddressDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ServiceOrdersAddressDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceOrdersAddressDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=200)> _
    Public Property Name As String
        Get
            CheckDeleted()
            If row(ServiceOrdersAddressDAL.COL_NAME_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ServiceOrdersAddressDAL.COL_NAME_NAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceOrdersAddressDAL.COL_NAME_NAME, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=15)> _
    Public Property TaxIdNumber As String
        Get
            CheckDeleted()
            If Row(ServiceOrdersAddressDAL.COL_NAME_TAX_ID_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServiceOrdersAddressDAL.COL_NAME_TAX_ID_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceOrdersAddressDAL.COL_NAME_TAX_ID_NUMBER, Value)
        End Set
    End Property



    Public Property AddressId As Guid
        Get
            CheckDeleted()
            If row(ServiceOrdersAddressDAL.COL_NAME_ADDRESS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ServiceOrdersAddressDAL.COL_NAME_ADDRESS_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceOrdersAddressDAL.COL_NAME_ADDRESS_ID, Value)
        End Set
    End Property


    Private _Address As Address = Nothing
    Public ReadOnly Property Address As Address
        Get
            If _Address Is Nothing Then
                If AddressId.Equals(Guid.Empty) Then
                    'If Me.IsNew Then
                    _Address = New Address(Dataset, Nothing)
                    If CompanyId.Equals(Guid.Empty) Then
                        Dim oDealer As New Dealer(DealerId)
                        CompanyId = oDealer.CompanyId
                    End If
                    Dim oCompany As New Company(CompanyId)
                    _Address.CountryId = oCompany.BusinessCountryId
                    '   Me.AddressId = Me._Address.Id
                    'End If
                Else
                    _Address = New Address(AddressId, Dataset, Nothing)
                End If
            End If
            Return _Address

        End Get
    End Property

    Private _CompanyId As Guid = Guid.Empty
    Public Property CompanyId As Guid
        Get
            Return _CompanyId
        End Get
        Set
            _CompanyId = value
        End Set
    End Property


#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ServiceOrdersAddressDAL
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

    Public Sub Copy(original As ServiceOrdersAddress)
        If Not IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Dealer")
        End If
        'Copy myself
        CopyFrom(original)

        'copy the children       

        AddressId = Guid.Empty
        Address.CopyFrom(original.Address)

    End Sub

    'Added manually to the code
    Public Overrides ReadOnly Property IsDirty As Boolean
        Get
            Return MyBase.IsDirty OrElse IsChildrenDirty OrElse _
            (Not Address.IsNew AndAlso Address.IsDirty) OrElse _
            (Address.IsNew AndAlso Not Address.IsEmpty)
        End Get
    End Property

    Public ReadOnly Property IsEmpty As Boolean
        Get
            If (Not IsEmptyString(Name)) OrElse (Not IsEmptyString(TaxIdNumber))  Then
                Return False
            End If
            Return True
        End Get
    End Property

    Private Function IsEmptyString(value As String)
        Return (value Is Nothing OrElse value.Trim.Length = 0)
    End Function

#End Region

#Region "DataView Retrieveing Methods"

#End Region
#Region "Custom Validation"

    '  <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    'Public NotInheritable Class CNPJ_TaxIdValidation
    '      Inherits ValidBaseAttribute

    '      Public Sub New(ByVal fieldDisplayName As String)
    '          MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_INVALID_CPF_DOCUMENT_NUMBER_ERR)
    '      End Sub

    '      Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
    '          Dim obj As Dealer = CType(objectToValidate, Dealer)
    '          Dim dal As New DealerDAL
    '          Dim oErrMess As String

    '          Try
    '              If obj.TaxIdNumber Is Nothing Then Return True ' the ValueMandatoryConditionally will catch this validation

    '              If obj.DealerTypeDesc = obj.DEALER_TYPE_DESC Then
    '                  oErrMess = dal.ExecuteSP(Codes.DOCUMENT_TYPE__CNPJ, obj.TaxIdNumber)
    '                  If Not oErrMess Is Nothing Then
    '                      MyBase.Message = UCase(oErrMess)
    '                      Return False
    '                  End If
    '              End If

    '              Return True

    '          Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
    '              Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
    '          End Try

    '      End Function

    '  End Class

#End Region
End Class


