'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (4/19/2007)  ********************

Public Class AcctSetting
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

    Public Sub New(AccountingCompanyId As Guid, Code As String, VendType As AcctSettingDAL.VendorType, Optional ByVal AcctType As String = "")
        MyBase.New()
        Dataset = New DataSet

        Dim _asID As Guid
        Dim dal As New AcctSettingDAL

        If AcctType.Equals("") Then
            If VendType = AcctSettingDAL.VendorType.ServiceCenter Then
                AcctType = ACCT_TYPE_CREDITOR
            Else
                AcctType = ACCT_TYPE_DEBITOR
            End If
        End If

        _asID = dal.LoadByVendor(Code, AccountingCompanyId, VendType, AcctType)
        If _asID.Equals(Guid.Empty) Then
            Load()
        Else
            Load(_asID)
        End If

    End Sub

    'For Branches
    Public Sub New(AccountingCompanyId As Guid, DealerCode As String, BranchCode As String, Optional ByVal AcctType As String = ACCT_TYPE_DEBITOR)
        MyBase.New()
        Dataset = New DataSet

        Dim _asID As Guid
        Dim dal As New AcctSettingDAL

        _asID = dal.LoadByVendor(DealerCode, AccountingCompanyId, AcctSettingDAL.VendorType.Branch, AcctType, BranchCode)
        If _asID.Equals(Guid.Empty) Then
            Load()
        Else
            Load(_asID)
        End If

    End Sub
    Protected Sub Load()
        Try
            Dim dal As New AcctSettingDAL
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
            Dim dal As New AcctSettingDAL
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

#Region "CONSTANTS"

    Public Const SERVICE_TYPE_RECOVERY As String = "9"
    Private Const ACCT_TYPE_CREDITOR As String = "1"
    Private Const ACCT_TYPE_DEBITOR As String = "0"

#End Region

#Region "Private Members"
    'Initialization code for new objects
    Private _AcctSettingType As String
    Private Sub Initialize()
    End Sub
#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid
        Get
            If Row(AcctSettingDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AcctSettingDAL.COL_NAME_ACCT_SETTINGS_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property AcctCompanyId As Guid
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_ACCT_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AcctSettingDAL.COL_NAME_ACCT_COMPANY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_ACCT_COMPANY_ID, Value)
        End Set
    End Property

    Public Property ServiceCenterId As Guid
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_SERVICE_CENTER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AcctSettingDAL.COL_NAME_SERVICE_CENTER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_SERVICE_CENTER_ID, Value)
        End Set
    End Property

    Public Property DealerGroupId As Guid
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_DEALER_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AcctSettingDAL.COL_NAME_DEALER_GROUP_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_DEALER_GROUP_ID, Value)
        End Set
    End Property

    Public Property DealerId As Guid
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AcctSettingDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property

    Public Property BranchId As Guid
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_BRANCH_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AcctSettingDAL.COL_NAME_BRANCH_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_BRANCH_ID, Value)
        End Set
    End Property

    Public Property CommissionEntityId As Guid
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_COMMISSION_ENTITY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AcctSettingDAL.COL_NAME_COMMISSION_ENTITY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_COMMISSION_ENTITY_ID, Value)
        End Set
    End Property

    <ValidAccountNumber(""), ValueMandatory(""), ValidStringLength("", Max:=15)> _
    Public Property AccountCode As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_ACCOUNT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_ACCOUNT_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            If Value IsNot Nothing Then Value = Value.Trim
            SetValue(AcctSettingDAL.COL_NAME_ACCOUNT_CODE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=15)> _
    Public Property AddressLookupCode As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_ADDRESS_LOOKUP_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_ADDRESS_LOOKUP_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_ADDRESS_LOOKUP_CODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=3)> _
    Public Property AddressSequenceNumber As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_ADDRESS_SEQUENCE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_ADDRESS_SEQUENCE_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_ADDRESS_SEQUENCE_NUMBER, Value)
        End Set
    End Property


    <ValueMandatory(" "), ValidStringLength("", Max:=1)> _
    Public Property AddressStatus As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_ADDRESS_STATUS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_ADDRESS_STATUS), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_ADDRESS_STATUS, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=1)> _
    Public Property AccountType As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_ACCOUNT_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_ACCOUNT_TYPE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_ACCOUNT_TYPE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=1)> _
    Public Property BalanceType As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_BALANCE_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_BALANCE_TYPE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_BALANCE_TYPE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=1)> _
    Public Property ConversionCodeControl As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_CONVERSION_CODE_CONTROL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_CONVERSION_CODE_CONTROL), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_CONVERSION_CODE_CONTROL, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=1)> _
    Public Property AccountAnalysis1 As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_1) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_1), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_1, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=1)> _
    Public Property AccountAnalysis2 As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_2) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_2), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_2, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=1)> _
    Public Property AccountAnalysis3 As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_3) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_3), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_3, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=1)> _
    Public Property AccountAnalysis4 As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_4) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_4), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_4, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=1)> _
    Public Property AccountAnalysis5 As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_5) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_5), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_5, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=1)> _
    Public Property AccountAnalysis6 As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_6) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_6), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_6, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=1)> _
    Public Property AccountAnalysis7 As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_7) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_7), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_7, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=1)> _
    Public Property AccountAnalysis8 As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_8) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_8), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_8, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=1)> _
    Public Property AccountAnalysis9 As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_9) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_9), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_9, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=1)> _
    Public Property AccountAnalysis10 As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_10) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_10), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_10, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=15)> _
    Public Property AccountAnalysisCode1 As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_CODE_1) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_CODE_1), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_CODE_1, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=15)> _
    Public Property AccountAnalysisCode2 As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_CODE_2) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_CODE_2), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_CODE_2, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=15)> _
    Public Property AccountAnalysisCode3 As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_CODE_3) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_CODE_3), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_CODE_3, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=15)> _
    Public Property AccountAnalysisCode4 As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_CODE_4) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_CODE_4), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_CODE_4, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=15)> _
    Public Property AccountAnalysisCode5 As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_CODE_5) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_CODE_5), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_CODE_5, Value)
        End Set
    End Property


    <ValidSSVendorId(""), ValidStringLength("", Max:=15)> _
    Public Property AccountAnalysisCode6 As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_CODE_6) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_CODE_6), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_CODE_6, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=15)> _
    Public Property AccountAnalysisCode7 As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_CODE_7) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_CODE_7), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_CODE_7, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=15)> _
    Public Property AccountAnalysisCode8 As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_CODE_8) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_CODE_8), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_CODE_8, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=15)> _
    Public Property AccountAnalysisCode9 As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_CODE_9) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_CODE_9), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_CODE_9, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=15)> _
    Public Property AccountAnalysisCode10 As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_CODE_10) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_CODE_10), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_CODE_10, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=15)> _
    Public Property AccountAnalysisACode1 As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_A_1) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_A_1), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_A_1, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=15)> _
    Public Property AccountAnalysisACode2 As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_A_2) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_A_2), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_A_2, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=15)> _
    Public Property AccountAnalysisACode3 As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_A_3) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_A_3), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_A_3, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=15)> _
    Public Property AccountAnalysisACode4 As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_A_4) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_A_4), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_A_4, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=15)> _
    Public Property AccountAnalysisACode5 As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_A_5) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_A_5), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_A_5, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=15)> _
    Public Property AccountAnalysisACode6 As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_A_6) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_A_6), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_A_6, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=15)> _
    Public Property AccountAnalysisACode7 As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_A_7) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_A_7), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_A_7, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=15)> _
    Public Property AccountAnalysisACode8 As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_A_8) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_A_8), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_A_8, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=15)> _
    Public Property AccountAnalysisACode9 As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_A_9) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_A_9), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_A_9, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=15)> _
    Public Property AccountAnalysisACode10 As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_A_10) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_A_10), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_ACCOUNT_ANALYSIS_A_10, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=1)> _
    Public Property ReportConversionControl As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_REPORT_CONVERSION_CONTROL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_REPORT_CONVERSION_CONTROL), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_REPORT_CONVERSION_CONTROL, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=15)> _
    Public Property DataAccessGroupCode As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_DATA_ACCESS_GROUP_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_DATA_ACCESS_GROUP_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_DATA_ACCESS_GROUP_CODE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=30)> _
    Public Property UserArea As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_USER_AREA) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_USER_AREA), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_USER_AREA, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=3)> _
    Public Property DefaultCurrencyCode As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_DEFAULT_CURRENCY_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_DEFAULT_CURRENCY_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_DEFAULT_CURRENCY_CODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1)> _
    Public Property AccountStatus As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_ACCOUNT_STATUS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_ACCOUNT_STATUS), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_ACCOUNT_STATUS, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1)> _
    Public Property SuppressRevaluation As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_SUPPRESS_REVALUATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_SUPPRESS_REVALUATION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_SUPPRESS_REVALUATION, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1)> _
    Public Property PayAsPaidAccountType As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_PAY_AS_PAID_ACCOUNT_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_PAY_AS_PAID_ACCOUNT_TYPE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_PAY_AS_PAID_ACCOUNT_TYPE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=15)> _
    Public Property SupplierLookupCode As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_SUPPLIER_LOOKUP_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_SUPPLIER_LOOKUP_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_SUPPLIER_LOOKUP_CODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=20)> _
    Public Property PaymentMethod As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_PAYMENT_METHOD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_PAYMENT_METHOD), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_PAYMENT_METHOD, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=8)> _
    Public Property SupplierStatus As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_SUPPLIER_STATUS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_SUPPLIER_STATUS), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_SUPPLIER_STATUS, Value)
        End Set
    End Property

    <ValidSupplierAnalysisCode1(""), ValidStringLength("", Max:=15)> _
    Public Property SupplierAnalysisCode1 As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_SUPPLIER_ANALYSIS_CODE_1) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_SUPPLIER_ANALYSIS_CODE_1), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_SUPPLIER_ANALYSIS_CODE_1, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=15)> _
    Public Property SupplierAnalysisCode2 As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_SUPPLIER_ANALYSIS_CODE_2) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_SUPPLIER_ANALYSIS_CODE_2), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_SUPPLIER_ANALYSIS_CODE_2, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=15)> _
    Public Property SupplierAnalysisCode3 As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_SUPPLIER_ANALYSIS_CODE_3) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_SUPPLIER_ANALYSIS_CODE_3), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_SUPPLIER_ANALYSIS_CODE_3, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=15)> _
    Public Property SupplierAnalysisCode4 As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_SUPPLIER_ANALYSIS_CODE_4) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_SUPPLIER_ANALYSIS_CODE_4), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_SUPPLIER_ANALYSIS_CODE_4, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=15)> _
    Public Property SupplierAnalysisCode5 As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_SUPPLIER_ANALYSIS_CODE_5) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_SUPPLIER_ANALYSIS_CODE_5), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_SUPPLIER_ANALYSIS_CODE_5, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=15)> _
    Public Property SupplierAnalysisCode6 As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_SUPPLIER_ANALYSIS_CODE_6) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_SUPPLIER_ANALYSIS_CODE_6), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_SUPPLIER_ANALYSIS_CODE_6, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=15)> _
    Public Property SupplierAnalysisCode7 As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_SUPPLIER_ANALYSIS_CODE_7) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_SUPPLIER_ANALYSIS_CODE_7), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_SUPPLIER_ANALYSIS_CODE_7, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=15)> _
    Public Property SupplierAnalysisCode8 As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_SUPPLIER_ANALYSIS_CODE_8) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_SUPPLIER_ANALYSIS_CODE_8), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_SUPPLIER_ANALYSIS_CODE_8, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=15)> _
    Public Property SupplierAnalysisCode9 As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_SUPPLIER_ANALYSIS_CODE_9) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_SUPPLIER_ANALYSIS_CODE_9), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_SUPPLIER_ANALYSIS_CODE_9, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=15)> _
    Public Property SupplierAnalysisCode10 As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_SUPPLIER_ANALYSIS_CODE_10) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_SUPPLIER_ANALYSIS_CODE_10), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_SUPPLIER_ANALYSIS_CODE_10, Value)
        End Set
    End Property

    <ValidPaymentTerms("")> _
    Public Property PaymentTermsId As Guid
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_PAYMENT_TERMS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AcctSettingDAL.COL_NAME_PAYMENT_TERMS_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_PAYMENT_TERMS_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=200)> _
    Public Property Description As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=5)> _
    Public Property DefaultBankSubCode As String
        Get
            CheckDeleted()
            If Row(AcctSettingDAL.COL_NAME_DEFAULT_BANK_SUB_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctSettingDAL.COL_NAME_DEFAULT_BANK_SUB_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctSettingDAL.COL_NAME_DEFAULT_BANK_SUB_CODE, Value)
        End Set
    End Property

    'Local property - not attached to the record
    Public Property AcctSettingType As String
        Get
            Return _AcctSettingType
        End Get
        Set
            _AcctSettingType = Value
        End Set
    End Property

#End Region

#Region "Public Members"

    Public VendorType As AcctSettingDAL.VendorType

    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New AcctSettingDAL
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

    Public Sub Copy(original As AcctSetting)
        If Not IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing accounting setting.")
        End If
        'Copy myself
        CopyFrom(original)
    End Sub

    Public Shared Function IsIDXAcctSettingAndCode(accountCompanyId As Guid, accountCode As String) As Boolean
        Dim dal As New AcctSettingDAL

        Return dal.IsIDXAcctSettingAndCode(accountCompanyId, accountCode)

    End Function
#End Region

#Region "DataView Retrieveing Methods"
    Public Function GetCounterPartObjectID() As Guid
        Dim dal As New AcctSettingDAL
        Return dal.LoadCounterPartById(Id)
    End Function
#Region "Dealer Group"
    Public Shared Function GetDealerGroups(strDealerGroupName As String, strDealerGroupCode As String) As DealerGroupAcctSettingsDV

        Dim _dal As New AcctSettingDAL
        Dim ds As DataSet
        Try
            ds = _dal.LoadDealerGroups(strDealerGroupName, strDealerGroupCode, ElitaPlusIdentity.Current.ActiveUser.Company.CompanyGroupId)
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                Return New DealerGroupAcctSettingsDV(ds.Tables(0))
            Else
                Return New DealerGroupAcctSettingsDV
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        End Try

    End Function

    Public Shared Function GetDealerGroupForAcctSetting() As DealerGroupAcctSettingsDV
        Dim _dal As New AcctSettingDAL
        Dim ds As DataSet

        Try
            ds = _dal.GetDealerGroupForAcctSetting(ElitaPlusIdentity.Current.ActiveUser.Company.CompanyGroupId)
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                Return New DealerGroupAcctSettingsDV(ds.Tables(0))
            Else
                Return New DealerGroupAcctSettingsDV
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        End Try
    End Function

    Public Class DealerGroupAcctSettingsDV
        Inherits DataView

        Public Const ACCT_SETTINGS_ID As String = "acct_settings_id"
        Public Const DEALER_GROUP_ID As String = "dealer_Group_id"
        Public Const DEALER_GROUP_NAME As String = "DESCRIPTION"
        Public Const DEALER_GROUP_CODE As String = "CODE"

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

        Public Function AddNewRowToEmptyDV() As DealerGroupAcctSettingsDV
            Dim dt As DataTable = Table.Clone()
            Dim row As DataRow = dt.NewRow
            Dim guidO As Guid = New Guid
            row(ACCT_SETTINGS_ID) = guidO.ToByteArray
            row(DEALER_GROUP_ID) = guidO.ToByteArray
            row(DEALER_GROUP_NAME) = ""
            row(DEALER_GROUP_CODE) = ""
            dt.Rows.Add(row)
            Return New DealerGroupAcctSettingsDV(dt)
        End Function
    End Class


#End Region

#Region "Commission Entity"
    Public Shared Function GetCommissionEntities(strCommissionEntityName As String) As CommissionEntityAcctSettingsDV

        Dim _dal As New AcctSettingDAL
        Dim ds As DataSet
        Try
            ds = _dal.LoadCommissionEntities(strCommissionEntityName, ElitaPlusIdentity.Current.ActiveUser.Company.CompanyGroupId)
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                Return New CommissionEntityAcctSettingsDV(ds.Tables(0))
            Else
                Return New CommissionEntityAcctSettingsDV
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        End Try
    End Function

    Public Shared Function GetCommissionEntityListForAcctSetting() As CommissionEntityAcctSettingsDV
        Dim _dal As New AcctSettingDAL
        Dim ds As DataSet

        Try
            ds = _dal.GetCommissionEntityListForAcctSetting(ElitaPlusIdentity.Current.ActiveUser.Company.CompanyGroupId)
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                Return New CommissionEntityAcctSettingsDV(ds.Tables(0))
            Else
                Return New CommissionEntityAcctSettingsDV
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        End Try
    End Function

    Public Class CommissionEntityAcctSettingsDV
        Inherits DataView

        Public Const ACCT_SETTINGS_ID As String = "acct_settings_id"
        Public Const ENTITY_ID As String = "ENTITY_ID"
        Public Const ENTITY_NAME As String = "ENTITY_NAME"


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

        Public Function AddNewRowToEmptyDV() As CommissionEntityAcctSettingsDV
            Dim dt As DataTable = Table.Clone()
            Dim row As DataRow = dt.NewRow
            Dim guidO As Guid = New Guid
            row(ACCT_SETTINGS_ID) = guidO.ToByteArray
            row(ENTITY_ID) = guidO.ToByteArray
            row(ENTITY_NAME) = ""
            dt.Rows.Add(row)
            Return New CommissionEntityAcctSettingsDV(dt)
        End Function
    End Class
#End Region
    Public Function GetDealers(strDealerName As String, strDealerCode As String) As DealerAcctSettingsDV

        Dim _dal As New AcctSettingDAL
        Dim ds As DataSet
        Try
            'TODO -- Add code to filter Dealers by exists with acct_company or not
            ds = _dal.LoadDealers(strDealerName, strDealerCode, ElitaPlusIdentity.Current.ActiveUser.Companies)
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                Return New DealerAcctSettingsDV(ds.Tables(0))
            Else
                Return New DealerAcctSettingsDV
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        End Try

    End Function

    Public Shared Function GetDealersForAcctSetting(oCompanyIds As ArrayList) As DealerAcctSettingsDV
        Dim _dal As New AcctSettingDAL
        Dim ds As DataSet
        Try
            ds = _dal.GetDealersForAcctSetting(oCompanyIds)
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                Return New DealerAcctSettingsDV(ds.Tables(0))
            Else
                Return New DealerAcctSettingsDV
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        End Try
    End Function



    Public Shared Function GetAccountingCompanies(Companies As ArrayList) As AcctCompanyDV

        Try
            Dim _AcctDAL As New AcctCompanyDAL
            Dim ds As DataSet = _AcctDAL.GetByCompanies(Companies)
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                Return New AcctCompanyDV(ds.Tables(0))
            Else
                Return New AcctCompanyDV
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)

        End Try
    End Function

    Public Class DealerAcctSettingsDV
        Inherits DataView

        Public Const ACCT_SETTINGS_ID As String = "acct_settings_id"
        Public Const DEALER_ID As String = "dealer_id"
        Public Const DEALER_NAME As String = "dealer_name"
        Public Const DEALER As String = "dealer"

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

        Public Function AddNewRowToEmptyDV() As DealerAcctSettingsDV
            Dim dt As DataTable = Table.Clone()
            Dim row As DataRow = dt.NewRow
            Dim guidO As Guid = New Guid
            row(ACCT_SETTINGS_ID) = guidO.ToByteArray
            row(DEALER_ID) = guidO.ToByteArray
            row(DEALER_NAME) = ""
            row(DEALER) = ""
            dt.Rows.Add(row)
            Return New DealerAcctSettingsDV(dt)
        End Function
    End Class

    Public Function GetServiceCenters(strSCName As String, strSCCode As String) As ServiceCenterAcctSettingsDV

        Dim _dal As New AcctSettingDAL
        Dim ds As DataSet

        Try
            ds = _dal.LoadServiceCenters(strSCName, strSCCode, ElitaPlusIdentity.Current.ActiveUser.Countries)
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                Return New ServiceCenterAcctSettingsDV(ds.Tables(0))
            Else
                Return New ServiceCenterAcctSettingsDV
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        End Try

    End Function

    Public Shared Function GetServiceCentersByAcctCompany(oAcctCompanyIds As ArrayList, Optional ByVal oSVCIDs As Generic.List(Of Guid) = Nothing) As DataSet
        Dim _dal As New AcctSettingDAL
        Try
            Return _dal.GetServiceCentersByAcctCompanies(oAcctCompanyIds, oSVCIDs)
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        End Try
    End Function

    Public Shared Function GetServiceCentersByCountry(oAcctCountryIds As ArrayList) As DataSet
        Dim _dal As New AcctSettingDAL
        Try
            Return _dal.GetServiceCentersByCountries(oAcctCountryIds)
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        End Try
    End Function


    Public Shared Function GetServiceCentersForAcctSetting(oCountryIds As ArrayList) As ServiceCenterAcctSettingsDV
        Dim _dal As New AcctSettingDAL
        Dim ds As DataSet

        Try
            ds = _dal.GetServiceCentersForAcctSetting(oCountryIds)
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                Return New ServiceCenterAcctSettingsDV(ds.Tables(0))
            Else
                Return New ServiceCenterAcctSettingsDV
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        End Try
    End Function

    Public Class ServiceCenterAcctSettingsDV
        Inherits DataView

        Public Const ACCT_SETTINGS_ID As String = "acct_settings_id"
        Public Const SERVICE_CENTER_ID As String = "service_center_id"
        Public Const CODE As String = "code"
        Public Const DESCRIPTION As String = "description"
        Public Const STATUS As String = "status_code"

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

        Public Function AddNewRowToEmptyDV() As ServiceCenterAcctSettingsDV
            Dim dt As DataTable = Table.Clone()
            Dim row As DataRow = dt.NewRow
            Dim guidO As Guid = New Guid
            row(ACCT_SETTINGS_ID) = guidO.ToByteArray
            row(SERVICE_CENTER_ID) = guidO.ToByteArray
            row(CODE) = ""
            row(DESCRIPTION) = ""
            row(STATUS) = ""
            dt.Rows.Add(row)
            Return New ServiceCenterAcctSettingsDV(dt)
        End Function
    End Class

    Public Class AcctCompanyDV
        Inherits DataView

        Public Const ACCT_COMPANY_ID As String = "acct_company_id"
        Public Const DESCRIPTION As String = "description"

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub
    End Class

#Region "Branch"

    Public Function GetBranches(strBranchName As String, strBranchCode As String) As BranchAcctSettingsDV

        Dim _dal As New AcctSettingDAL
        Dim ds As DataSet
        Try
            'TODO -- Add code to filter Dealers by exists with acct_company or not
            ds = _dal.LoadBranches(strBranchName, strBranchCode, ElitaPlusIdentity.Current.ActiveUser.Companies)
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                Return New BranchAcctSettingsDV(ds.Tables(0))
            Else
                Return New BranchAcctSettingsDV
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        End Try

    End Function

    Public Shared Function GetDealersForBranch(oCompanyIds As ArrayList) As DealerAcctSettingsDV
        Dim _dal As New AcctSettingDAL
        Dim ds As DataSet
        Try
            ds = _dal.GetDealersForBranch(oCompanyIds)
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                Return New DealerAcctSettingsDV(ds.Tables(0))
            Else
                Return New DealerAcctSettingsDV
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        End Try
    End Function

    Public Shared Function GetBranchesForAcctSetting(oDealerId As Guid) As BranchAcctSettingsDV
        Dim _dal As New AcctSettingDAL
        Dim ds As DataSet
        Try
            ds = _dal.GetBranchesForAcctSetting(oDealerId)
            If ds IsNot Nothing AndAlso ds.Tables.Count > 0 Then
                Return New BranchAcctSettingsDV(ds.Tables(0))
            Else
                Return New BranchAcctSettingsDV
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message, ex)
        End Try
    End Function

    Public Class BranchAcctSettingsDV
        Inherits DataView

        Public Const ACCT_SETTINGS_ID As String = "acct_settings_id"
        Public Const BRANCH_ID As String = "branch_id"
        Public Const DEALER_NAME As String = "dealer_name"
        Public Const BRANCH_CODE As String = "branch_code"
        Public Const BRANCH_NAME As String = "branch_name"

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

        Public Function AddNewRowToEmptyDV() As BranchAcctSettingsDV
            Dim dt As DataTable = Table.Clone()
            Dim row As DataRow = dt.NewRow
            Dim guidO As Guid = New Guid
            row(ACCT_SETTINGS_ID) = guidO.ToByteArray
            row(BRANCH_ID) = guidO.ToByteArray
            row(DEALER_NAME) = ""
            row(BRANCH_CODE) = ""
            row(BRANCH_NAME) = ""
            dt.Rows.Add(row)
            Return New BranchAcctSettingsDV(dt)
        End Function
    End Class

#End Region
#End Region

#Region "Custom Validation"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidPaymentTerms
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.ERR_BO_PAYMENT_TERMS_REQD)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As AcctSetting = CType(objectToValidate, AcctSetting)
            If (obj.PaymentTermsId = Guid.Empty) Then
                Dim _acctCompany As New AcctCompany(obj.AcctCompanyId)
                If LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListCache.LK_ACCT_SYSTEM, ElitaPlusIdentity.Current.ActiveUser.LanguageId), _acctCompany.AcctSystemId) = FelitaEngine.FELITA_PREFIX Then
                    Return False 'Required for FELITA system
                End If
            End If

            Return True

        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidSSVendorId
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR)

        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As AcctSetting = CType(objectToValidate, AcctSetting)
            If (obj.AccountAnalysisCode6 Is Nothing OrElse obj.AccountAnalysisCode6.Trim = String.Empty) Then
                Dim _acctCompany As New AcctCompany(obj.AcctCompanyId)
                If LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListCache.LK_ACCT_SYSTEM, ElitaPlusIdentity.Current.ActiveUser.LanguageId), _acctCompany.AcctSystemId) = FelitaEngine.SMARTSTREAM_PREFIX Then
                    Return False 'Required for SmartStream system
                End If
            End If

            Return True

        End Function

        Public Overrides Function isMandatory(PropName As String, objectToValidate As Object) As Boolean
            Dim obj As AcctSetting = CType(objectToValidate, AcctSetting)
            Dim reqProps() As String = New String() {"ACCOUNTANALYSISCODE6"}

            Try

                If reqProps.Contains(PropName) Then

                    If Not obj.AcctCompanyId.Equals(Guid.Empty) Then
                        Dim _acctCompany As New AcctCompany(obj.AcctCompanyId)
                        If LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListCache.LK_ACCT_SYSTEM, ElitaPlusIdentity.Current.ActiveUser.LanguageId), _acctCompany.AcctSystemId) = FelitaEngine.SMARTSTREAM_PREFIX Then
                            Return True 'Required for SmartStream system
                        End If
                    End If

                End If

                Return False

            Catch ex As Exception
                Return False
            End Try

        End Function

    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidAccountNumber
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_VALUE_IS_TOO_SHORT_OR_TOO_LONG)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim bIsOk As Boolean = True

            If valueToCheck IsNot Nothing Then
                Dim oAcctSetting As AcctSetting = CType(objectToValidate, AcctSetting)
                If oAcctSetting.AccountCode IsNot Nothing AndAlso oAcctSetting.AccountCode.Trim.Length > 0 Then
                    Dim oAc As New AcctCompany(oAcctSetting.AcctCompanyId)
                    Dim _acctExtension As String = LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListCache.LK_ACCT_SYSTEM, ElitaPlusIdentity.Current.ActiveUser.LanguageId, False), oAc.AcctSystemId)
                    If _acctExtension = FelitaEngine.SMARTSTREAM_PREFIX Then
                        If oAcctSetting.AccountCode.Trim.Length > 8 Then
                            Return Not bIsOk
                        End If
                    End If
                End If
            End If

            Return bIsOk

        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidSupplierAnalysisCode1
        Inherits ValidBaseAttribute


        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR)

        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As AcctSetting = CType(objectToValidate, AcctSetting)


            If (obj.SupplierAnalysisCode1 Is Nothing OrElse obj.SupplierAnalysisCode1.Trim = String.Empty) AndAlso obj.AccountType = ACCT_TYPE_CREDITOR Then
                Dim _acctCompany As New AcctCompany(obj.AcctCompanyId)
                If LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListCache.LK_ACCT_SYSTEM, ElitaPlusIdentity.Current.ActiveUser.LanguageId), _acctCompany.AcctSystemId) = FelitaEngine.FELITA_PREFIX Then
                    Return False 'Required for Felita system
                End If
            End If
            Return True

        End Function

        Public Overrides Function isMandatory(PropName As String, objectToValidate As Object) As Boolean
            Dim obj As AcctSetting = CType(objectToValidate, AcctSetting)
            Dim reqProps() As String = New String() {"SUPPLIERANALYSISCODE1"}

            Try

                If reqProps.Contains(PropName) Then

                    If Not obj.AcctCompanyId.Equals(Guid.Empty) Then
                        Dim _acctCompany As New AcctCompany(obj.AcctCompanyId)
                        If LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListCache.LK_ACCT_SYSTEM, ElitaPlusIdentity.Current.ActiveUser.LanguageId), _acctCompany.AcctSystemId) = FelitaEngine.FELITA_PREFIX Then
                            Return True 'Required for SmartStream system
                        End If
                    End If
                End If

                Return False

            Catch ex As Exception
                Return False
            End Try

        End Function

    End Class
#End Region

End Class


