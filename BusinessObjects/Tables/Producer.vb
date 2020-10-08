'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (1/31/2019)  ********************

Public Class Producer
    Inherits BusinessObjectBase

#Region "Variables"
    Private _company As Company = Nothing
#End Region
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
            Dim dal As New ProducerDAL
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
            Dim dal As New ProducerDAL
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
#End Region

#Region "Address"

    Private _address As Address = Nothing
    Public ReadOnly Property Address As Address
        Get
            If _address Is Nothing Then
                If AddressId.Equals(Guid.Empty) Then
                    _address = New Address(Dataset, Nothing)
                    _address.CountryId = Company.BusinessCountryId
                Else
                    _address = New Address(AddressId, Dataset, Nothing)
                End If
            End If
            Return _address
        End Get
    End Property
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
            If Row(ProducerDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProducerDAL.COL_NAME_PRODUCER_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=5)>
    Public Property Code As String
        Get
            CheckDeleted()
            If Row(ProducerDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ProducerDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ProducerDAL.COL_NAME_CODE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=50)>
    Public Property Description As String
        Get
            CheckDeleted()
            If Row(ProducerDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ProducerDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ProducerDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=40)>
    Public Property ProducerTypeXcd As String
        Get
            CheckDeleted()
            If Row(ProducerDAL.COL_NAME_PRODUCER_TYPE_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ProducerDAL.COL_NAME_PRODUCER_TYPE_XCD), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ProducerDAL.COL_NAME_PRODUCER_TYPE_XCD, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property CompanyId As Guid
        Get
            CheckDeleted()
            If Row(ProducerDAL.COL_NAME_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProducerDAL.COL_NAME_COMPANY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ProducerDAL.COL_NAME_COMPANY_ID, Value)
        End Set
    End Property



    Public Property AddressId As Guid
        Get
            CheckDeleted()
            If Row(ProducerDAL.COL_NAME_ADDRESS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProducerDAL.COL_NAME_ADDRESS_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ProducerDAL.COL_NAME_ADDRESS_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)>
    Public Property TaxIdNumber As String
        Get
            CheckDeleted()
            If Row(ProducerDAL.COL_NAME_TAX_ID_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ProducerDAL.COL_NAME_TAX_ID_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ProducerDAL.COL_NAME_TAX_ID_NUMBER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)>
    Public Property RegulatorRegistrationId As String
        Get
            CheckDeleted()
            If Row(ProducerDAL.COL_NAME_REGULATOR_REGISTRATION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ProducerDAL.COL_NAME_REGULATOR_REGISTRATION_ID), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ProducerDAL.COL_NAME_REGULATOR_REGISTRATION_ID, Value)
        End Set
    End Property

    Public ReadOnly Property Company As Company
        Get
            If _company Is Nothing Then
                If Not (CompanyId.Equals(Guid.Empty)) Then
                    _company = New Company(CompanyId)
                Else
                    Return Nothing
                End If
            End If
            Return _company
        End Get
    End Property


#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ProducerDAL
                dal.UpdateFamily(Dataset)
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Id
                    Dataset = New DataSet
                    Row = Nothing
                    Load(objId)
                    _address = Nothing
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    Public Sub DeleteAndSave()
        Dim addr As Address = Address
        CheckDeleted()
        BeginEdit()
        Try
            Delete()
            addr.Delete()
            Save()
        Catch ex As Exception
            cancelEdit()
            Throw ex
        End Try
    End Sub

    Public Sub Copy(original As Producer)
        If Not IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing producer")
        End If
        'Copy myself
        CopyFrom(original)
        AddressId = Guid.Empty
        Address.CopyFrom(original.Address)
    End Sub

    Public Overrides ReadOnly Property IsDirty As Boolean
        Get
            Dim blnIsDirty As Boolean = False
            If MyBase.IsDirty Then
                blnIsDirty = True
            ElseIf (Not Address.IsNew AndAlso Address.IsDirty) OrElse (Address.IsNew AndAlso Not Address.IsEmpty) Then
                blnIsDirty = True
            End If
            Return blnIsDirty
        End Get
    End Property
#End Region

#Region "ProducerSearchDV"
    Public Class ProducerSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_PRODUCER_ID As String = "producer_id"
        Public Const COL_DESCRIPTION As String = "description"
        Public Const COL_CODE As String = "code"
        Public Const COL_PRODUCER_TYPE As String = "producer_type"
#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region
#Region "DataView Retrieveing Methods"
    Public Shared Function getList(descriptionMask As String, codeMask As String) As ProducerSearchDV
        Try
            Dim dal As New ProducerDAL
            Dim compIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Return New ProducerSearchDV(dal.LoadList(descriptionMask, codeMask, compIds).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#End Region

End Class


