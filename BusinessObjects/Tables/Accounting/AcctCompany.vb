'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (4/19/2007)  ********************

Public Class AcctCompany
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
            Dim dal As New AcctCompanyDAL
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
            Dim dal As New AcctCompanyDAL
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
            If row(AcctCompanyDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AcctCompanyDAL.COL_NAME_ACCT_COMPANY_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength(Nothing, max:=100)> _
    Public Property Description() As String
        Get
            CheckDeleted()
            If Row(AcctCompanyDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctCompanyDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctCompanyDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property

    <ValidStringLength(Nothing, max:=100)> _
   Public Property FTPDirectory() As String
        Get
            CheckDeleted()
            If Row(AcctCompanyDAL.COL_NAME_FTP_DIRECTORY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctCompanyDAL.COL_NAME_FTP_DIRECTORY), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctCompanyDAL.COL_NAME_FTP_DIRECTORY, Value)
        End Set
    End Property

    <ValidStringLength(Nothing, max:=100)> _
    Public Property BalanceDirectory() As String
        Get
            CheckDeleted()
            If Row(AcctCompanyDAL.COL_NAME_BALANCE_DIRECTORY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctCompanyDAL.COL_NAME_BALANCE_DIRECTORY), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctCompanyDAL.COL_NAME_BALANCE_DIRECTORY, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength(Nothing, max:=1, min:=1)> _
  Public Property UseAccounting() As String
        Get
            CheckDeleted()
            If Row(AcctCompanyDAL.COL_NAME_USE_ACCOUNTING) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctCompanyDAL.COL_NAME_USE_ACCOUNTING), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctCompanyDAL.COL_NAME_USE_ACCOUNTING, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property UseElitaBankInfoId() As Guid
        Get
            CheckDeleted()
            If Row(AcctCompanyDAL.COL_NAME_USE_ELITA_BANK_INFO_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AcctCompanyDAL.COL_NAME_USE_ELITA_BANK_INFO_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(AcctCompanyDAL.COL_NAME_USE_ELITA_BANK_INFO_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
   Public Property AcctSystemId() As Guid
        Get
            CheckDeleted()
            If Row(AcctCompanyDAL.COL_NAME_ACCT_SYSTEM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AcctCompanyDAL.COL_NAME_ACCT_SYSTEM_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(AcctCompanyDAL.COL_NAME_ACCT_SYSTEM_ID, Value)
        End Set
    End Property

    <ValidStringLength(Nothing, max:=15)> _
   Public Property Code() As String
        Get
            CheckDeleted()
            If Row(AcctCompanyDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctCompanyDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctCompanyDAL.COL_NAME_CODE, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength(Nothing, max:=1, min:=1)> _
    Public Property ReportCommissionBreakdown() As String
        Get
            CheckDeleted()
            If Row(AcctCompanyDAL.COL_NAME_RPT_COMMISSION_BREAKDOWN) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctCompanyDAL.COL_NAME_RPT_COMMISSION_BREAKDOWN), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctCompanyDAL.COL_NAME_RPT_COMMISSION_BREAKDOWN, Value)
        End Set
    End Property

    <ValueMandatory("")> _
   Public Property ProcessMethodId() As Guid
        Get
            CheckDeleted()
            If Row(AcctCompanyDAL.COL_NAME_PROCESS_METHOD_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AcctCompanyDAL.COL_NAME_PROCESS_METHOD_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(AcctCompanyDAL.COL_NAME_PROCESS_METHOD_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property CoverageEntityByRegion() As Guid
        Get
            CheckDeleted()
            If Row(AcctCompanyDAL.COL_NAME_COV_ENTITY_BY_REGION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AcctCompanyDAL.COL_NAME_COV_ENTITY_BY_REGION), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(AcctCompanyDAL.COL_NAME_COV_ENTITY_BY_REGION, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property UseCoverageEntityId() As Guid
        Get
            CheckDeleted()
            If Row(AcctCompanyDAL.COL_NAME_USE_COVERAGE_ENTITY) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AcctCompanyDAL.COL_NAME_USE_COVERAGE_ENTITY), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(AcctCompanyDAL.COL_NAME_USE_COVERAGE_ENTITY, Value)
        End Set
    End Property

    <ValidStringLength(Nothing, max:=200)> _
    Public Property NotifyEmail() As String
        Get
            CheckDeleted()
            If Row(AcctCompanyDAL.COL_NAME_NOTIFY_EMAIL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctCompanyDAL.COL_NAME_NOTIFY_EMAIL), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctCompanyDAL.COL_NAME_NOTIFY_EMAIL, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New AcctCompanyDAL
                dal.Update(Me.Row)
                'Reload the Data from the DB
                If Me.Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Me.Id
                    Me.Dataset = New Dataset
                    Me.Row = Nothing
                    Me.Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    Public Shared Function GetAccountingCompanies(ByVal Companies As ArrayList) As AcctCompany()

        Try
            Dim _AcctDAL As New AcctCompanyDAL
            Dim ds As Dataset = _AcctDAL.GetByCompanies(Companies)
            Dim _acctCo(0) As AcctCompany

            If Not ds Is Nothing AndAlso ds.Tables.Count = 1 Then

                'If no acctCompanies are found, return 1 new acct_company in the object.
                If ds.Tables(0).Rows.Count = 0 Then
                    _acctCo(0) = New AcctCompany
                    Return _acctCo
                End If

                Dim i As Integer
                Dim dr As DataRow
                ReDim _acctCo(ds.Tables(0).Rows.Count - 1)
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    dr = ds.Tables(0).Rows(i)
                    _acctCo(i) = New AcctCompany(New Guid(CType(dr(0), Byte())))
                Next

                Return _acctCo

            End If

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try

    End Function

#End Region

#Region "DataView Retrieveing Methods"

    Public Shared Function LoadList(ByVal descriptionMask As String) As DataView
        Try
            Dim dal As New AcctCompanyDAL
            Dim ds As DataSet

            ds = dal.LoadList(descriptionMask, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            Return (ds.Tables(AcctCompanyDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetNewDataViewRow(ByVal dv As DataView, ByVal id As Guid, ByVal bo As AcctCompany) As DataView

        Dim dt As DataTable
        dt = dv.Table

        If bo.IsNew Then
            Dim row As DataRow = dt.NewRow

            row(AcctCompanyDAL.COL_NAME_DESCRIPTION) = bo.Description 'String.Empty
            row(AcctCompanyDAL.COL_NAME_ACCT_COMPANY_ID) = bo.Id.ToByteArray
            row(AcctCompanyDAL.COL_NAME_FTP_DIRECTORY) = bo.FTPDirectory
            'row(AcctCompanyDAL.COL_NAME_USE_ELITA_BANK_INFO_ID) = bo.UseElitaBankInfoId.ToByteArray


            dt.Rows.Add(row)
        End If

        Return (dv)

    End Function
    Public Function IsCompUsingNewAccounting(ByVal acctCompanyId As Guid) As Boolean
        Try
            Dim objAcctCompDAL As New AcctCompanyDAL
            Dim ds As DataSet = objAcctCompDAL.GetCompUsingNewAccForAccCompany(acctCompanyId)
            If ds.Tables(0).Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try

    End Function
#End Region


#Region "AcctCompanyDV"
    Public Class AcctCompanyDV
        Inherits DataView

#Region "Constants"
        Public Const COL_ACCT_COMPANY_ID As String = AcctCompanyDAL.COL_NAME_ACCT_COMPANY_ID
        Public Const COL_DESCRIPTION As String = AcctCompanyDAL.COL_NAME_DESCRIPTION
        Public Const COL_FTP_DIRECTORY As String = AcctCompanyDAL.COL_NAME_FTP_DIRECTORY
        Public Const COL_USE_ACCOUNTING As String = AcctCompanyDAL.COL_NAME_USE_ACCOUNTING
        Public Const COL_USE_ELITA_BANK_INFO As String = AcctCompanyDAL.COL_NAME_USE_ELITA_BANK_INFO_ID
        Public Const COL_COV_ENTITY_BY_REGION As String = AcctCompanyDAL.COL_NAME_COV_ENTITY_BY_REGION
        Public Const COL_ACCT_SYSTEM_ID As String = AcctCompanyDAL.COL_NAME_ACCT_SYSTEM_ID
        Public Const COL_CODE As String = AcctCompanyDAL.COL_NAME_CODE
        Public Const COL_RPT_COMMISSION_BREAKDOWN As String = AcctCompanyDAL.COL_NAME_RPT_COMMISSION_BREAKDOWN
        Public Const COL_PROCESS_METHOD_ID As String = AcctCompanyDAL.COL_NAME_PROCESS_METHOD_ID
        Public Const COL_BALANCE_DIRECTORY As String = AcctCompanyDAL.COL_NAME_BALANCE_DIRECTORY
        Public Const COL_NOTIFY_EMAIL As String = AcctCompanyDAL.COL_NAME_NOTIFY_EMAIL

#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region

End Class


