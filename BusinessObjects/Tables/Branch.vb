'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (3/19/2007)  ********************

Public Class Branch
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
            Dim dal As New BranchDAL
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
            Dim dal As New BranchDAL
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

#Region "Private Members"
    'Initialization code for new objects
    Private Shared Sub Initialize()
    End Sub
#End Region

#Region "CONSTANTS"

    Private Const BRANCH_NAME_REQUIRED As String = "BRANCH_NAME_REQUIRED"
    Private Const MIN_BRANCH_NAME_LENGTH As String = "1"
    Private Const MAX_BRANCH_NAME_LENGTH As String = "200"

    Private Const MIN_BRANCH_CODE_LENGTH As String = "1"
    Private Const MAX_BRANCH_CODE_LENGTH As String = "5"
    Private Const BRANCH_CODE_REQUIRED As String = "BRANCH_CODE_REQUIRED"

    'Public Const COL_BRANCH_ID As String = "BRANCH_ID"
    Public Const COL_BRANCH_CODE As String = "BRANCH_CODE"
    Public Const COL_BRANCH_NAME As String = "BRANCH_NAME"
    Public Const WILDCARD_CHAR As Char = "%"
    Public Const ASTERISK As Char = "*"
    Private Const DSNAME As String = "LIST"

#End Region


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid
        Get
            If Row(BranchDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(BranchDAL.COL_NAME_BRANCH_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=10)> _
    Public Property BranchCode As String
        Get
            CheckDeleted()
            If Row(BranchDAL.COL_NAME_BRANCH_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(BranchDAL.COL_NAME_BRANCH_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BranchDAL.COL_NAME_BRANCH_CODE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property DealerId As Guid
        Get
            CheckDeleted()
            If Row(BranchDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(BranchDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BranchDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=100)>
    Public Property Address1 As String
        Get
            CheckDeleted()
            If Row(BranchDAL.COL_NAME_ADDRESS1) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(BranchDAL.COL_NAME_ADDRESS1), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BranchDAL.COL_NAME_ADDRESS1, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=100)>
    Public Property Address2 As String
        Get
            CheckDeleted()
            If Row(BranchDAL.COL_NAME_ADDRESS2) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(BranchDAL.COL_NAME_ADDRESS2), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BranchDAL.COL_NAME_ADDRESS2, Value)
        End Set
    End Property

    ''Def-1574
    <ValidStringLength("", Max:=100)>
    Public Property Address3 As String
        Get
            CheckDeleted()
            If Row(BranchDAL.COL_NAME_ADDRESS3) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(BranchDAL.COL_NAME_ADDRESS3), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BranchDAL.COL_NAME_ADDRESS3, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50)> _
    Public Property City As String
        Get
            CheckDeleted()
            If Row(BranchDAL.COL_NAME_CITY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(BranchDAL.COL_NAME_CITY), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BranchDAL.COL_NAME_CITY, Value)
        End Set
    End Property


    Public Property RegionId As Guid
        Get
            CheckDeleted()
            If Row(BranchDAL.COL_NAME_REGION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(BranchDAL.COL_NAME_REGION_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BranchDAL.COL_NAME_REGION_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property CountryId As Guid
        Get
            CheckDeleted()
            If Row(BranchDAL.COL_NAME_COUNTRY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(BranchDAL.COL_NAME_COUNTRY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BranchDAL.COL_NAME_COUNTRY_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=40)> _
     Public Property PostalCode As String
        Get
            CheckDeleted()
            If Row(BranchDAL.COL_NAME_POSTAL_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(BranchDAL.COL_NAME_POSTAL_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BranchDAL.COL_NAME_POSTAL_CODE, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=130)>
    Public Property BranchName As String
        Get
            CheckDeleted()
            If Row(BranchDAL.COL_NAME_BRANCH_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(BranchDAL.COL_NAME_BRANCH_NAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BranchDAL.COL_NAME_BRANCH_NAME, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=30)> _
    Public Property ContactPhone As String
        Get
            CheckDeleted()
            If Row(BranchDAL.COL_NAME_CONTACT_PHONE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(BranchDAL.COL_NAME_CONTACT_PHONE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BranchDAL.COL_NAME_CONTACT_PHONE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=5)> _
    Public Property ContactExt As String
        Get
            CheckDeleted()
            If Row(BranchDAL.COL_NAME_CONTACT_EXT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(BranchDAL.COL_NAME_CONTACT_EXT), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BranchDAL.COL_NAME_CONTACT_EXT, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=15)> _
    Public Property ContactFax As String
        Get
            CheckDeleted()
            If Row(BranchDAL.COL_NAME_CONTACT_FAX) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(BranchDAL.COL_NAME_CONTACT_FAX), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BranchDAL.COL_NAME_CONTACT_FAX, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=150), EmailAddress("")>
    Public Property ContactEmail As String
        Get
            CheckDeleted()
            If Row(BranchDAL.COL_NAME_CONTACT_EMAIL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(BranchDAL.COL_NAME_CONTACT_EMAIL), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BranchDAL.COL_NAME_CONTACT_EMAIL, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=30)> _
    Public Property Market As String
        Get
            CheckDeleted()
            If row(BranchDAL.COL_NAME_MARKET) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(BranchDAL.COL_NAME_MARKET), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BranchDAL.COL_NAME_MARKET, Value)
        End Set
    End Property

    Public Property BankInfoId As Guid
        Get
            CheckDeleted()
            If Row(BranchDAL.COL_NAME_BANK_INFO_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(BranchDAL.COL_NAME_BANK_INFO_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BranchDAL.COL_NAME_BANK_INFO_ID, Value)
        End Set
    End Property

    Public Property BranchTypeId As Guid
        Get
            CheckDeleted()
            If row(BranchDAL.COL_NAME_BRANCH_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(BranchDAL.COL_NAME_BRANCH_TYPE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BranchDAL.COL_NAME_BRANCH_TYPE_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)> _
    Public Property StoreManager As String
        Get
            CheckDeleted()
            If Row(BranchDAL.COL_NAME_STORE_MANAGER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(BranchDAL.COL_NAME_STORE_MANAGER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BranchDAL.COL_NAME_STORE_MANAGER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)>
    Public Property MarketingRegion As String
        Get
            CheckDeleted()
            If Row(BranchDAL.COL_NAME_MARKETING_REGION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(BranchDAL.COL_NAME_MARKETING_REGION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BranchDAL.COL_NAME_MARKETING_REGION, Value)
        End Set
    End Property

    Public Property OpenDate As DateType
        Get
            CheckDeleted()
            If Row(BranchDAL.COL_NAME_OPEN_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(BranchDAL.COL_NAME_OPEN_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BranchDAL.COL_NAME_OPEN_DATE, Value)
        End Set
    End Property

    Public Property CloseDate As DateType
        Get
            CheckDeleted()
            If Row(BranchDAL.COL_NAME_CLOSE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(BranchDAL.COL_NAME_CLOSE_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BranchDAL.COL_NAME_CLOSE_DATE, Value)
        End Set
    End Property


#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New BranchDAL
                dal.Update(Row)
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Id
                    Dataset = New Dataset
                    Row = Nothing
                    Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    Public Overrides ReadOnly Property IsDirty As Boolean
        Get
            Return MyBase.IsDirty
        End Get
    End Property

    Public Sub DeleteAndSave()
        CheckDeleted()
        BeginEdit()
        Try
            Delete()
            Save()
        Catch ex As Exception
            cancelEdit()
            Throw ex
        End Try
    End Sub

    Public Sub Copy(original As Branch)
        If Not IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing branch")
        End If
        'Copy myself
        CopyFrom(original)
    End Sub

#End Region

#Region "DataView Retrieveing Methods"
#Region "BranchSearchDV"
    Public Class BranchSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_BRANCH_ID As String = "branch_id"
        Public Const COL_DEALER_NAME As String = "dealer_name"
        Public Const COL_BRANCH_CODE As String = "branch_code"
        Public Const COL_BRANCH_NAME As String = "branch_name"
        Public Const COL_BRANCH As String = "branch"
        Public Const COL_DEALER_ID As String = "dealer_id"
        Public Const COL_BANK_INFO_ID As String = "bank_info_id"
#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region

    Public Shared Function getList(descriptionMask As String, codeMask As String, DealerMask As Guid) As BranchSearchDV
        Try
            Dim dal As New BranchDAL
            Return New BranchSearchDV(dal.LoadList(descriptionMask, codeMask, DealerMask).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function getListByDealer(DealerId As Guid) As BranchSearchDV
        Try
            Dim dal As New BranchDAL
            Return New BranchSearchDV(dal.LoadListByDealer(DealerId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function getListByDealerForWS(DealerId As Guid) As BranchSearchDV
        Try
            Dim dal As New BranchDAL
            Dim objDealer As New Dealer(DealerId)
            Dim EditBranch_Flag As String = LookupListNew.GetCodeFromId(LookupListCache.LK_YESNO, objDealer.EditBranchId)
            Dim Branch_Validation_Flag As String = LookupListNew.GetCodeFromId(LookupListCache.LK_YESNO, objDealer.BranchValidationId)
            If EditBranch_Flag.Equals(Codes.YESNO_Y) Then
                Return New BranchSearchDV(dal.LoadListFromBranchStandardizationByDealerForWS(DealerId).Tables(0))
            ElseIf EditBranch_Flag.Equals(Codes.YESNO_N) AndAlso Branch_Validation_Flag.Equals(Codes.YESNO_Y) Then
                Return New BranchSearchDV(dal.LoadListByDealer(DealerId).Tables(0))
            Else
                'return blank table
                Dim dt As DataTable = New DataTable()
                dt.Columns.Add(BranchSearchDV.COL_BRANCH_ID, GetType(Guid))
                dt.Columns.Add(BranchSearchDV.COL_BRANCH_CODE, GetType(String))
                dt.Columns.Add(BranchSearchDV.COL_BRANCH_NAME, GetType(String))
                Return New BranchSearchDV(dt)
            End If

            Return New BranchSearchDV(dal.LoadListByDealer(DealerId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function


#End Region

#Region "Custom Validation"
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
Public NotInheritable Class EmailAddress
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_EMAIL_IS_INVALID_ERR)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As Branch = CType(objectToValidate, Branch)

            If obj.ContactEmail Is Nothing Then
                Return True
            End If

            Return MiscUtil.EmailAddressValidation(obj.ContactEmail)

        End Function

    End Class
#End Region
End Class



