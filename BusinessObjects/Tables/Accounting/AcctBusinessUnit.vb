'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (4/19/2007)  ********************

Public Class AcctBusinessUnit
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
            Dim dal As New AcctBusinessUnitDAL
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

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New AcctBusinessUnitDAL
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
    Public ReadOnly Property Id() As Guid
        Get
            If row(AcctBusinessUnitDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AcctBusinessUnitDAL.COL_NAME_ACCT_BUSINESS_UNIT_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property AcctCompanyId() As Guid
        Get
            CheckDeleted()
            If row(AcctBusinessUnitDAL.COL_NAME_ACCT_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AcctBusinessUnitDAL.COL_NAME_ACCT_COMPANY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(AcctBusinessUnitDAL.COL_NAME_ACCT_COMPANY_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=10)> _
    Public Property BusinessUnit() As String
        Get
            CheckDeleted()
            If Row(AcctBusinessUnitDAL.COL_NAME_BUSINESS_UNIT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctBusinessUnitDAL.COL_NAME_BUSINESS_UNIT), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(AcctBusinessUnitDAL.COL_NAME_BUSINESS_UNIT, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=15)> _
    Public Property Code() As String
        Get
            CheckDeleted()
            If Row(AcctBusinessUnitDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctBusinessUnitDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(AcctBusinessUnitDAL.COL_NAME_CODE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=1)> _
    Public Property SuppressVendors() As String
        Get
            CheckDeleted()
            If Row(AcctBusinessUnitDAL.COL_NAME_SUPPRESS_VENDORS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctBusinessUnitDAL.COL_NAME_SUPPRESS_VENDORS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(AcctBusinessUnitDAL.COL_NAME_SUPPRESS_VENDORS, Value)
        End Set
    End Property

    Public Property PaymentMethodId() As Guid
        Get
            CheckDeleted()
            If Row(AcctBusinessUnitDAL.COL_NAME_PAYMENT_METHOD_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AcctBusinessUnitDAL.COL_NAME_PAYMENT_METHOD_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(AcctBusinessUnitDAL.COL_NAME_PAYMENT_METHOD_ID, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New AcctBusinessUnitDAL
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

#Region "AcctBusinessUnitSearchDV"
    Public Class AcctBusinessUnitSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_ACCT_COMPANY_ID As String = AcctBusinessUnitDAL.COL_NAME_ACCT_COMPANY_ID                   '"acct_company_id"
        Public Const COL_ACCT_COMPANY_DESCRIPTION As String = AcctBusinessUnitDAL.COL_NAME_ACCT_COMPANY_DESCRIPTION '"acct company description"
        Public Const COL_ACCT_BUSINESS_UNIT_ID As String = AcctBusinessUnitDAL.COL_NAME_ACCT_BUSINESS_UNIT_ID       '"acct_business_unit_id"
        Public Const COL_BUSINESS_UNIT As String = AcctBusinessUnitDAL.COL_NAME_BUSINESS_UNIT                       '"business_unit"
        Public Const COL_CODE As String = AcctBusinessUnitDAL.COL_NAME_CODE                      '"code"
        Public Const COL_SUPPRESS_VENDORS As String = AcctBusinessUnitDAL.COL_NAME_SUPPRESS_VENDORS                      '"suppress_vendors"
        Public Const COL_PAYMENT_METHOD As String = AcctBusinessUnitDAL.COL_NAME_PAYMENT_METHOD_ID                      '"payment_method_id"

#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region

    Public Shared Function getList(ByVal AcctCompanyMask As Guid, ByVal BusinessUnitNameMask As String) As AcctBusinessUnitSearchDV
        Try
            Dim dal As New AcctBusinessUnitDAL
            Return New AcctBusinessUnitSearchDV(dal.LoadList(BusinessUnitNameMask, AcctCompanyMask).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function LoadList(ByVal descriptionMask As String, ByVal acctCompany As Guid, ByVal myAcctCompany As ArrayList, Optional ByVal getCovTypeChidrens As Boolean = False) As DataView
        Try
            Dim dal As New AcctBusinessUnitDAL
            Dim ds As Dataset

            ds = dal.LoadList(descriptionMask, acctCompany, myAcctCompany)
            Return (ds.Tables(AcctBusinessUnitDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function


    Public Shared Function GetNewDataViewRow(ByVal dv As DataView, ByVal id As Guid, ByVal bo As AcctBusinessUnit) As DataView

        Dim dt As DataTable
        dt = dv.Table

        If bo.IsNew Then
            Dim row As DataRow = dt.NewRow

            row(AcctBusinessUnitDAL.COL_NAME_ACCT_BUSINESS_UNIT_ID) = bo.Id.ToByteArray
            row(AcctBusinessUnitDAL.COL_NAME_BUSINESS_UNIT) = bo.BusinessUnit
            row(AcctBusinessUnitDAL.COL_NAME_CODE) = bo.Code
            row(AcctBusinessUnitDAL.COL_NAME_ACCT_COMPANY_ID) = bo.AcctCompanyId.ToByteArray
            row(AcctBusinessUnitDAL.COL_NAME_ACCT_COMPANY_DESCRIPTION) = bo.AcctBusinessUnitSearchDV.COL_ACCT_COMPANY_DESCRIPTION
            row(AcctBusinessUnitDAL.COL_NAME_SUPPRESS_VENDORS) = bo.SuppressVendors
            row(AcctBusinessUnitDAL.COL_NAME_PAYMENT_METHOD_ID) = bo.PaymentMethodId.ToByteArray

            dt.Rows.Add(row)
        End If

        Return (dv)

    End Function


#End Region

End Class


