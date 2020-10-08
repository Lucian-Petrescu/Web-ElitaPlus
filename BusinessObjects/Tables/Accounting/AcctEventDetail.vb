'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (4/19/2007)  ********************

Public Class AcctEventDetail
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
            Dim dal As New AcctEventDetailDAL
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
            Dim dal As New AcctEventDetailDAL
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
        IncludeExcludeInd = "E" 'set default value for property
    End Sub

    Private Sub ValidateEvent()

        If UsePayeeSettings Is Nothing OrElse UsePayeeSettings = "N" Then
            If AccountCode Is Nothing OrElse AccountCode.Trim.Length = 0 Then
                Dim errors() As ValidationError = {New ValidationError(Messages.VALUE_MANDATORY_ERR, GetType(AcctEventDetail), GetType(ValueMandatoryAttribute), "AccountCode", "")}
                Throw New BOValidationException(errors, [GetType].FullName, UniqueId)
            End If
        End If
    End Sub
#End Region

#Region "CONSTANTS"

    Private Const YES_STRING = "Y"
    Private Const NO_STRING = "N"

#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid
        Get
            If row(AcctEventDetailDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AcctEventDetailDAL.COL_NAME_ACCT_EVENT_DETAIL_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property AcctEventId As Guid
        Get
            CheckDeleted()
            If row(AcctEventDetailDAL.COL_NAME_ACCT_EVENT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AcctEventDetailDAL.COL_NAME_ACCT_EVENT_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctEventDetailDAL.COL_NAME_ACCT_EVENT_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=1)> _
    Public Property DebitCredit As String
        Get
            CheckDeleted()
            If Row(AcctEventDetailDAL.COL_NAME_DEBIT_CREDIT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctEventDetailDAL.COL_NAME_DEBIT_CREDIT), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctEventDetailDAL.COL_NAME_DEBIT_CREDIT, Value)
        End Set
    End Property


    <ValidAccountNumber(""), ValidStringLength("", Max:=15)> _
    Public Property AccountCode As String
        Get
            CheckDeleted()
            If Row(AcctEventDetailDAL.COL_NAME_ACCOUNT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctEventDetailDAL.COL_NAME_ACCOUNT_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            If Not Value Is Nothing Then Value = Value.Trim
            SetValue(AcctEventDetailDAL.COL_NAME_ACCOUNT_CODE, Value)
        End Set
    End Property


    <ValidPayeeSettings(""), ValueMandatory(""), ValidStringLength("", Max:=1)> _
    Public Property UsePayeeSettings As String
        Get
            CheckDeleted()
            If Row(AcctEventDetailDAL.COL_NAME_USE_PAYEE_SETTINGS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctEventDetailDAL.COL_NAME_USE_PAYEE_SETTINGS), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctEventDetailDAL.COL_NAME_USE_PAYEE_SETTINGS, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=15)> _
    Public Property AnalysisCode1 As String
        Get
            CheckDeleted()
            If Row(AcctEventDetailDAL.COL_NAME_ANALYSIS_CODE_1) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctEventDetailDAL.COL_NAME_ANALYSIS_CODE_1), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctEventDetailDAL.COL_NAME_ANALYSIS_CODE_1, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=15)> _
    Public Property AnalysisCode2 As String
        Get
            CheckDeleted()
            If Row(AcctEventDetailDAL.COL_NAME_ANALYSIS_CODE_2) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctEventDetailDAL.COL_NAME_ANALYSIS_CODE_2), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctEventDetailDAL.COL_NAME_ANALYSIS_CODE_2, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=15)> _
    Public Property AnalysisCode3 As String
        Get
            CheckDeleted()
            If Row(AcctEventDetailDAL.COL_NAME_ANALYSIS_CODE_3) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctEventDetailDAL.COL_NAME_ANALYSIS_CODE_3), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctEventDetailDAL.COL_NAME_ANALYSIS_CODE_3, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=15)> _
    Public Property AnalysisCode4 As String
        Get
            CheckDeleted()
            If Row(AcctEventDetailDAL.COL_NAME_ANALYSIS_CODE_4) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctEventDetailDAL.COL_NAME_ANALYSIS_CODE_4), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctEventDetailDAL.COL_NAME_ANALYSIS_CODE_4, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=15)> _
    Public Property AnalysisCode5 As String
        Get
            CheckDeleted()
            If Row(AcctEventDetailDAL.COL_NAME_ANALYSIS_CODE_5) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctEventDetailDAL.COL_NAME_ANALYSIS_CODE_5), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctEventDetailDAL.COL_NAME_ANALYSIS_CODE_5, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=15)> _
    Public Property AnalysisCode6 As String
        Get
            CheckDeleted()
            If Row(AcctEventDetailDAL.COL_NAME_ANALYSIS_CODE_6) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctEventDetailDAL.COL_NAME_ANALYSIS_CODE_6), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctEventDetailDAL.COL_NAME_ANALYSIS_CODE_6, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=15)> _
    Public Property AnalysisCode7 As String
        Get
            CheckDeleted()
            If Row(AcctEventDetailDAL.COL_NAME_ANALYSIS_CODE_7) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctEventDetailDAL.COL_NAME_ANALYSIS_CODE_7), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctEventDetailDAL.COL_NAME_ANALYSIS_CODE_7, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=15)> _
    Public Property AnalysisCode8 As String
        Get
            CheckDeleted()
            If Row(AcctEventDetailDAL.COL_NAME_ANALYSIS_CODE_8) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctEventDetailDAL.COL_NAME_ANALYSIS_CODE_8), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctEventDetailDAL.COL_NAME_ANALYSIS_CODE_8, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=15)> _
    Public Property AnalysisCode9 As String
        Get
            CheckDeleted()
            If Row(AcctEventDetailDAL.COL_NAME_ANALYSIS_CODE_9) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctEventDetailDAL.COL_NAME_ANALYSIS_CODE_9), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctEventDetailDAL.COL_NAME_ANALYSIS_CODE_9, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=15)> _
    Public Property AnalysisCode10 As String
        Get
            CheckDeleted()
            If Row(AcctEventDetailDAL.COL_NAME_ANALYSIS_CODE_10) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctEventDetailDAL.COL_NAME_ANALYSIS_CODE_10), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctEventDetailDAL.COL_NAME_ANALYSIS_CODE_10, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property FieldTypeId As Guid
        Get
            CheckDeleted()
            If Row(AcctEventDetailDAL.COL_NAME_FIELD_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AcctEventDetailDAL.COL_NAME_FIELD_TYPE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctEventDetailDAL.COL_NAME_FIELD_TYPE_ID, Value)
        End Set
    End Property

    <ValidNumericRange("", Max:=100.0, Min:=0.0)> _
    Public Property Calculation As DecimalType
        Get
            CheckDeleted()
            If Row(AcctEventDetailDAL.COL_NAME_CALCULATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(AcctEventDetailDAL.COL_NAME_CALCULATION), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctEventDetailDAL.COL_NAME_CALCULATION, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=1)> _
    Public Property AllocationMarker As String
        Get
            CheckDeleted()
            If Row(AcctEventDetailDAL.COL_NAME_ALLOCATION_MARKER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctEventDetailDAL.COL_NAME_ALLOCATION_MARKER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctEventDetailDAL.COL_NAME_ALLOCATION_MARKER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=5)> _
    Public Property JournalType As String
        Get
            CheckDeleted()
            If Row(AcctEventDetailDAL.COL_NAME_JOURNAL_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctEventDetailDAL.COL_NAME_JOURNAL_TYPE), String)
            End If
        End Get
        Set
            CheckDeleted()
            If Not Value Is Nothing Then Value = Value.Trim
            SetValue(AcctEventDetailDAL.COL_NAME_JOURNAL_TYPE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=20)> _
    Public Property JournalIDSuffix As String
        Get
            CheckDeleted()
            If Row(AcctEventDetailDAL.COL_NAME_JOURNAL_ID_SUFFIX) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctEventDetailDAL.COL_NAME_JOURNAL_ID_SUFFIX), String)
            End If
        End Get
        Set
            CheckDeleted()
            If Not Value Is Nothing Then Value = Value.Trim
            SetValue(AcctEventDetailDAL.COL_NAME_JOURNAL_ID_SUFFIX, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property AcctBusinessUnitId As Guid
        Get
            CheckDeleted()
            If Row(AcctEventDetailDAL.COL_NAME_ACCT_BUSINESS_UNIT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AcctEventDetailDAL.COL_NAME_ACCT_BUSINESS_UNIT_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctEventDetailDAL.COL_NAME_ACCT_BUSINESS_UNIT_ID, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=1)> _
    Public Property AccountType As String
        Get
            CheckDeleted()
            If Row(AcctEventDetailDAL.COL_NAME_ACCOUNT_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctEventDetailDAL.COL_NAME_ACCOUNT_TYPE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctEventDetailDAL.COL_NAME_ACCOUNT_TYPE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50)> _
    Public Property Description As String
        Get
            CheckDeleted()
            If Row(AcctEventDetailDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctEventDetailDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctEventDetailDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property

    Public Property BusinessEntityId As Guid
        Get
            CheckDeleted()
            If Row(AcctEventDetailDAL.COL_NAME_BUSINESS_ENTITY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AcctEventDetailDAL.COL_NAME_BUSINESS_ENTITY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctEventDetailDAL.COL_NAME_BUSINESS_ENTITY_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property AnalysisSource1Id As Guid
        Get
            CheckDeleted()
            If Row(AcctEventDetailDAL.COL_NAME_ANALYSIS_SRC_1_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AcctEventDetailDAL.COL_NAME_ANALYSIS_SRC_1_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctEventDetailDAL.COL_NAME_ANALYSIS_SRC_1_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property AnalysisSource2Id As Guid
        Get
            CheckDeleted()
            If Row(AcctEventDetailDAL.COL_NAME_ANALYSIS_SRC_2_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AcctEventDetailDAL.COL_NAME_ANALYSIS_SRC_2_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctEventDetailDAL.COL_NAME_ANALYSIS_SRC_2_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property AnalysisSource3Id As Guid
        Get
            CheckDeleted()
            If Row(AcctEventDetailDAL.COL_NAME_ANALYSIS_SRC_3_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AcctEventDetailDAL.COL_NAME_ANALYSIS_SRC_3_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctEventDetailDAL.COL_NAME_ANALYSIS_SRC_3_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property AnalysisSource4Id As Guid
        Get
            CheckDeleted()
            If Row(AcctEventDetailDAL.COL_NAME_ANALYSIS_SRC_4_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AcctEventDetailDAL.COL_NAME_ANALYSIS_SRC_4_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctEventDetailDAL.COL_NAME_ANALYSIS_SRC_4_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property AnalysisSource5Id As Guid
        Get
            CheckDeleted()
            If Row(AcctEventDetailDAL.COL_NAME_ANALYSIS_SRC_5_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AcctEventDetailDAL.COL_NAME_ANALYSIS_SRC_5_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctEventDetailDAL.COL_NAME_ANALYSIS_SRC_5_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property AnalysisSource6Id As Guid
        Get
            CheckDeleted()
            If Row(AcctEventDetailDAL.COL_NAME_ANALYSIS_SRC_6_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AcctEventDetailDAL.COL_NAME_ANALYSIS_SRC_6_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctEventDetailDAL.COL_NAME_ANALYSIS_SRC_6_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property AnalysisSource7Id As Guid
        Get
            CheckDeleted()
            If Row(AcctEventDetailDAL.COL_NAME_ANALYSIS_SRC_7_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AcctEventDetailDAL.COL_NAME_ANALYSIS_SRC_7_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctEventDetailDAL.COL_NAME_ANALYSIS_SRC_7_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property AnalysisSource8Id As Guid
        Get
            CheckDeleted()
            If Row(AcctEventDetailDAL.COL_NAME_ANALYSIS_SRC_8_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AcctEventDetailDAL.COL_NAME_ANALYSIS_SRC_8_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctEventDetailDAL.COL_NAME_ANALYSIS_SRC_8_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property AnalysisSource9Id As Guid
        Get
            CheckDeleted()
            If Row(AcctEventDetailDAL.COL_NAME_ANALYSIS_SRC_9_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AcctEventDetailDAL.COL_NAME_ANALYSIS_SRC_9_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctEventDetailDAL.COL_NAME_ANALYSIS_SRC_9_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property AnalysisSource10Id As Guid
        Get
            CheckDeleted()
            If Row(AcctEventDetailDAL.COL_NAME_ANALYSIS_SRC_10_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AcctEventDetailDAL.COL_NAME_ANALYSIS_SRC_10_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctEventDetailDAL.COL_NAME_ANALYSIS_SRC_10_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property DescriptionSourceId As Guid
        Get
            CheckDeleted()
            If Row(AcctEventDetailDAL.COL_NAME_DESCRIPTION_SRC_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AcctEventDetailDAL.COL_NAME_DESCRIPTION_SRC_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctEventDetailDAL.COL_NAME_DESCRIPTION_SRC_ID, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=1), ValidInclusionExclusionConfig("")> _
    Public Property IncludeExcludeInd As String
        Get
            CheckDeleted()
            If Row(AcctEventDetailDAL.COL_NAME_INCLUDE_EXCLUDE_IND) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctEventDetailDAL.COL_NAME_INCLUDE_EXCLUDE_IND), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctEventDetailDAL.COL_NAME_INCLUDE_EXCLUDE_IND, Value)
        End Set
    End Property
#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            If Not IsDeleted Then ValidateEvent()
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New AcctEventDetailDAL
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
#Region "Custom Validation"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidPayeeSettings
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_BUSINESS_UNIT_VENDOR)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim bIsOk As Boolean = True

            Try
                If Not valueToCheck Is Nothing Then
                    If valueToCheck.ToString = YES_STRING Then
                        Dim oAcctEventDetail As AcctEventDetail = CType(objectToValidate, AcctEventDetail)
                        Dim oBU As New AcctBusinessUnit(oAcctEventDetail.AcctBusinessUnitId)
                        If oBU.SuppressVendors = YES_STRING Then
                            Return Not bIsOk
                        End If
                    End If
                Else
                    Dim oAcctEventDetail As AcctEventDetail = CType(objectToValidate, AcctEventDetail)
                    oAcctEventDetail.UsePayeeSettings = NO_STRING
                End If

                Return bIsOk
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

            Try
                If Not valueToCheck Is Nothing Then
                    Dim oAcctEventDetail As AcctEventDetail = CType(objectToValidate, AcctEventDetail)
                    Dim oBU As New AcctBusinessUnit(oAcctEventDetail.AcctBusinessUnitId)
                    Dim oAc As New AcctCompany(oBU.AcctCompanyId)
                    Dim _acctExtension As String = LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCT_SYSTEM, ElitaPlusIdentity.Current.ActiveUser.LanguageId, False), oAc.AcctSystemId)
                    If _acctExtension = FelitaEngine.SMARTSTREAM_PREFIX Then
                        If Not oAcctEventDetail.AccountCode Is Nothing AndAlso oAcctEventDetail.AccountCode.Trim.Length > 8 Then
                            Return Not bIsOk
                        End If
                    End If
                End If

                Return bIsOk
            Catch ex As Exception
                Return False
            End Try

        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidInclusionExclusionConfig
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, "AT_LEAST_ONE_INC_EXC_CONFIG_REQUIRED")
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim oAcctEventDetail As AcctEventDetail = CType(objectToValidate, AcctEventDetail)

            If oAcctEventDetail.IsNew = False AndAlso oAcctEventDetail.IncludeExcludeInd = "I" Then ' validation for existing object only, new object validation are done in form
                Dim objList As Collections.Generic.List(Of AcctEventDetailIncExc)
                objList = AcctEventDetailIncExc.GetInclusionExclusionConfigByAcctEventDetail(oAcctEventDetail.Id)
                If objList Is Nothing OrElse objList.Count = 0 Then
                    Return False
                End If
            End If
            Return True
        End Function
    End Class
#End Region


#Region "DataView Retrieveing Methods"
#Region "AcctEventDetailSearchDV"
    Public Class AcctEventDetailSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_ACCT_EVENT_DETAIL_ID As String = "acct_event_detail_id"
        Public Const COL_ACCT_EVENT_ID As String = "acct_event_id"
        Public Const COL_DEBIT_CREDIT As String = "debit_credit"
        Public Const COL_ACCOUNT_CODE As String = "account_code"
        Public Const COL_FIELD_TYPE As String = "field_type"
        Public Const COL_ACCT_BUSINESS_UNIT_ID As String = "acct_business_unit_id"
        Public Const COL_BUSINESS_UNIT As String = "business_unit"
        Public Const COL_BUSINESS_ENTITY_ID As String = "business_entity_id"

#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region

    Public Shared Function getList(AcctEventMask As Guid) As AcctEventDetailSearchDV
        Try
            Dim dal As New AcctEventDetailDAL
            Return New AcctEventDetailSearchDV(dal.LoadList(AcctEventMask, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#End Region

End Class


