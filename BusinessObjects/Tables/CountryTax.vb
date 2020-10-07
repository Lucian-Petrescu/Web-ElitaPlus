'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (8/17/2007)  ********************
Imports System.Globalization

Public Class CountryTax
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
            Dim dal As New CountryTaxDAL
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
            Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            setvalue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
            With ElitaPlusIdentity.Current.ActiveUser
                CountryId = .Country(.FirstCompanyID).Id
            End With
            CompanyTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMPANY_TYPE, COMPANY_TYPE_SERVICES)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New CountryTaxDAL
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

#Region "Constant"
    Public Const MAX_NUMBER_OF_TAX_GROUPS As Integer = 6

    Const Col_TaxGroup_Description As String = "TAX{0}_DESCRIPTION"
    Const Col_TaxGroup_PercentFlag As String = "TAX{0}_PERCENT_FLAG_ID"
    Const Col_TaxGroup_ComputeMethod As String = "TAX{0}_COMPUTE_METHOD_ID"
    Const Col_TaxGroup_Percent As String = "TAX{0}_PERCENT"

    Public Const MANUAL_COMPUTE_METHOD As String = "I"
    Public Const TAX_PERCENT_CODE As String = "P"
    Public Const TAX_STATE_CODE As String = "S"
    Public Const TAX_PERCENT_FLAG As String = "PERCT"
    Private Const TAX_COMPUTE_METHOD As String = "TCOMP"


    Public Const COMPANY_TYPE_SERVICES As String = "2"


    Public Const INVALID_EFFECTIVE_DATE As String = "EFFECTIVE DATE MUST BE LOWER THAN EXPIRATION DATE"
    Public Const EFFECTIVE_DATE_NOT_GREATER As String = "EFFECTIVE DATE MUST BE ONE DAY HIGHER THAN PREVIOUS EXPIRATION DATE"
    Public Const LAST_ENTRY_ONLY As String = "ONLY THE LAST ENTRY CAN BE DELETED"
    Public Const PERCENT_GREATER_THAN_ZERO As String = "PERCENT MUST BE GRATER THAN ZERO FOR TAX{0} GROUP"
    Public Const ALL_ENTRIES_REQUIRED As String = "ALL ENTRIES MUST BE FILLED FOR :TAX{0} GROUP"
    Public Const ONE_TAX_GROUP_REQUIRED As String = "GROUP_ONE_MUST_BE_FILL"

    Public Class TaxTypeCode
        Public Const POS As String = "1"
        Public Const PREMIUMS As String = "2"
        Public Const CREDITCARDS As String = "3"
        Public Const INVOICE As String = "4"
        Public Const COMMISSIONS As String = "6"
        Public Const REPAIRS As String = "7"
        Public Const REPLACEMENT As String = "8"

        Public Const REPAIR__CLAIM_DIAGNOSTICS = "RPR_CDIAG"
        Public Const REPAIR__CLAIM_DISPOSITION = "RPR_CDISP"
        Public Const REPAIR__CLAIM_LABOR = "RPR_CLABR"
        Public Const REPAIR__CLAIM_OTHER = "RPR_COTHR"
        Public Const REPAIR__CLAIM_PARTS = "RPR_CPART"
        Public Const REPAIR__CLAIM_SERVICE = "RPR_CSRVC"
        Public Const REPAIR__CLAIM_SHIPPING = "RPR_CSHIP"
        Public Const REPAIR__CLAIM_TRIP = "RPR_CTRIP"
        Public Const REPAIR__CLAIM_DEFAULT = "RPR_CDFLT"

        Public Const REPLACEMENT__CLAIM_DIAGNOSTICS = "RPL_CDIAG"
        Public Const REPLACEMENT__CLAIM_DISPOSITION = "RPL_CDISP"
        Public Const REPLACEMENT__CLAIM_LABOR = "RPL_CLABR"
        Public Const REPLACEMENT__CLAIM_OTHER = "RPL_COTHR"
        Public Const REPLACEMENT__CLAIM_PARTS = "RPL_CPART"
        Public Const REPLACEMENT__CLAIM_SERVICE = "RPL_CSRVC"
        Public Const REPLACEMENT__CLAIM_SHIPPING = "RPL_CSHIP"
        Public Const REPLACEMENT__CLAIM_TRIP = "RPL_CTRIP"
        Public Const REPLACEMENT__CLAIM_DEFAULT = "RPL_CDFLT"


    End Class
#End Region


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid
        Get
            If row(CountryTaxDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CountryTaxDAL.COL_NAME_COUNTRY_TAX_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CountryId As Guid
        Get
            CheckDeleted()
            If row(CountryTaxDAL.COL_NAME_COUNTRY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CountryTaxDAL.COL_NAME_COUNTRY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CountryTaxDAL.COL_NAME_COUNTRY_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property TaxTypeId As Guid
        Get
            CheckDeleted()
            If Row(CountryTaxDAL.COL_NAME_TAX_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CountryTaxDAL.COL_NAME_TAX_TYPE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CountryTaxDAL.COL_NAME_TAX_TYPE_ID, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidEffectiveDate(""), ValidNewEffectiveDate("")> _
    Public Property EffectiveDate As DateType
        Get
            CheckDeleted()
            If Row(CountryTaxDAL.COL_NAME_EFFECTIVE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(CountryTaxDAL.COL_NAME_EFFECTIVE_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CountryTaxDAL.COL_NAME_EFFECTIVE_DATE, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidLastEntryForDelete("")> _
    Public Property ExpirationDate As DateType
        Get
            CheckDeleted()
            If Row(CountryTaxDAL.COL_NAME_EXPIRATION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(CountryTaxDAL.COL_NAME_EXPIRATION_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CountryTaxDAL.COL_NAME_EXPIRATION_DATE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=120), ValidTaxGroup("", 1)> _
    Public Property Tax1Description As String
        Get
            CheckDeleted()
            If Row(CountryTaxDAL.COL_NAME_TAX1_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CountryTaxDAL.COL_NAME_TAX1_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CountryTaxDAL.COL_NAME_TAX1_DESCRIPTION, Value)
        End Set
    End Property

    Public Property Tax1PercentFlagId As Guid
        Get
            CheckDeleted()
            If Row(CountryTaxDAL.COL_NAME_TAX1_PERCENT_FLAG_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CountryTaxDAL.COL_NAME_TAX1_PERCENT_FLAG_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CountryTaxDAL.COL_NAME_TAX1_PERCENT_FLAG_ID, Value)
        End Set
    End Property

    <RequiredAtLeastOneTaxgroupFilled("")> _
    Public Property Tax1ComputeMethodId As Guid
        Get
            CheckDeleted()
            If Row(CountryTaxDAL.COL_NAME_TAX1_COMPUTE_METHOD_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CountryTaxDAL.COL_NAME_TAX1_COMPUTE_METHOD_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CountryTaxDAL.COL_NAME_TAX1_COMPUTE_METHOD_ID, Value)
        End Set
    End Property

    <ValidTaxPercent("", 1)> _
    Public Property Tax1Percent As DecimalType
        Get
            CheckDeleted()
            If Row(CountryTaxDAL.COL_NAME_TAX1_PERCENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CountryTaxDAL.COL_NAME_TAX1_PERCENT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CountryTaxDAL.COL_NAME_TAX1_PERCENT, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=120), ValidTaxGroup("", 2)> _
    Public Property Tax2Description As String
        Get
            CheckDeleted()
            If Row(CountryTaxDAL.COL_NAME_TAX2_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CountryTaxDAL.COL_NAME_TAX2_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CountryTaxDAL.COL_NAME_TAX2_DESCRIPTION, Value)
        End Set
    End Property

    Public Property Tax2ComputeMethodId As Guid
        Get
            CheckDeleted()
            If Row(CountryTaxDAL.COL_NAME_TAX2_COMPUTE_METHOD_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CountryTaxDAL.COL_NAME_TAX2_COMPUTE_METHOD_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CountryTaxDAL.COL_NAME_TAX2_COMPUTE_METHOD_ID, Value)
        End Set
    End Property

    Public Property Tax2PercentFlagId As Guid
        Get
            CheckDeleted()
            If Row(CountryTaxDAL.COL_NAME_TAX2_PERCENT_FLAG_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CountryTaxDAL.COL_NAME_TAX2_PERCENT_FLAG_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CountryTaxDAL.COL_NAME_TAX2_PERCENT_FLAG_ID, Value)
        End Set
    End Property

    <ValidTaxPercent("", 2)> _
    Public Property Tax2Percent As DecimalType
        Get
            CheckDeleted()
            If Row(CountryTaxDAL.COL_NAME_TAX2_PERCENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CountryTaxDAL.COL_NAME_TAX2_PERCENT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CountryTaxDAL.COL_NAME_TAX2_PERCENT, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=120), ValidTaxGroup("", 3)> _
    Public Property Tax3Description As String
        Get
            CheckDeleted()
            If Row(CountryTaxDAL.COL_NAME_TAX3_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CountryTaxDAL.COL_NAME_TAX3_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CountryTaxDAL.COL_NAME_TAX3_DESCRIPTION, Value)
        End Set
    End Property

    Public Property Tax3ComputeMethodId As Guid
        Get
            CheckDeleted()
            If row(CountryTaxDAL.COL_NAME_TAX3_COMPUTE_METHOD_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CountryTaxDAL.COL_NAME_TAX3_COMPUTE_METHOD_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CountryTaxDAL.COL_NAME_TAX3_COMPUTE_METHOD_ID, Value)
        End Set
    End Property

    Public Property Tax3PercentFlagId As Guid
        Get
            CheckDeleted()
            If row(CountryTaxDAL.COL_NAME_TAX3_PERCENT_FLAG_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CountryTaxDAL.COL_NAME_TAX3_PERCENT_FLAG_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CountryTaxDAL.COL_NAME_TAX3_PERCENT_FLAG_ID, Value)
        End Set
    End Property

    <ValidTaxPercent("", 3)> _
    Public Property Tax3Percent As DecimalType
        Get
            CheckDeleted()
            If Row(CountryTaxDAL.COL_NAME_TAX3_PERCENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CountryTaxDAL.COL_NAME_TAX3_PERCENT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CountryTaxDAL.COL_NAME_TAX3_PERCENT, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=120), ValidTaxGroup("", 4)> _
    Public Property Tax4Description As String
        Get
            CheckDeleted()
            If Row(CountryTaxDAL.COL_NAME_TAX4_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CountryTaxDAL.COL_NAME_TAX4_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CountryTaxDAL.COL_NAME_TAX4_DESCRIPTION, Value)
        End Set
    End Property

    Public Property Tax4ComputeMethodId As Guid
        Get
            CheckDeleted()
            If row(CountryTaxDAL.COL_NAME_TAX4_COMPUTE_METHOD_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CountryTaxDAL.COL_NAME_TAX4_COMPUTE_METHOD_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CountryTaxDAL.COL_NAME_TAX4_COMPUTE_METHOD_ID, Value)
        End Set
    End Property

    Public Property Tax4PercentFlagId As Guid
        Get
            CheckDeleted()
            If row(CountryTaxDAL.COL_NAME_TAX4_PERCENT_FLAG_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CountryTaxDAL.COL_NAME_TAX4_PERCENT_FLAG_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CountryTaxDAL.COL_NAME_TAX4_PERCENT_FLAG_ID, Value)
        End Set
    End Property

    <ValidTaxPercent("", 4)> _
    Public Property Tax4Percent As DecimalType
        Get
            CheckDeleted()
            If Row(CountryTaxDAL.COL_NAME_TAX4_PERCENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CountryTaxDAL.COL_NAME_TAX4_PERCENT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CountryTaxDAL.COL_NAME_TAX4_PERCENT, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=120), ValidTaxGroup("", 5)> _
    Public Property Tax5Description As String
        Get
            CheckDeleted()
            If Row(CountryTaxDAL.COL_NAME_TAX5_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CountryTaxDAL.COL_NAME_TAX5_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CountryTaxDAL.COL_NAME_TAX5_DESCRIPTION, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1)>
    Public Property ApplyWithholdingFlag As String
        Get
            CheckDeleted()
            If Row(CountryTaxDAL.COL_NAME_APPLY_WITHHOLDING_FLAG) Is DBNull.Value Then
                Return "N"
            Else
                Return CType(Row(CountryTaxDAL.COL_NAME_APPLY_WITHHOLDING_FLAG), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CountryTaxDAL.COL_NAME_APPLY_WITHHOLDING_FLAG, Value)
        End Set
    End Property

    Public Property Tax5ComputeMethodId As Guid
        Get
            CheckDeleted()
            If row(CountryTaxDAL.COL_NAME_TAX5_COMPUTE_METHOD_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CountryTaxDAL.COL_NAME_TAX5_COMPUTE_METHOD_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CountryTaxDAL.COL_NAME_TAX5_COMPUTE_METHOD_ID, Value)
        End Set
    End Property

    Public Property Tax5PercentFlagId As Guid
        Get
            CheckDeleted()
            If row(CountryTaxDAL.COL_NAME_TAX5_PERCENT_FLAG_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CountryTaxDAL.COL_NAME_TAX5_PERCENT_FLAG_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CountryTaxDAL.COL_NAME_TAX5_PERCENT_FLAG_ID, Value)
        End Set
    End Property

    <ValidTaxPercent("", 5)> _
    Public Property Tax5Percent As DecimalType
        Get
            CheckDeleted()
            If Row(CountryTaxDAL.COL_NAME_TAX5_PERCENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CountryTaxDAL.COL_NAME_TAX5_PERCENT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CountryTaxDAL.COL_NAME_TAX5_PERCENT, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=120), ValidTaxGroup("", 6)> _
    Public Property Tax6Description As String
        Get
            CheckDeleted()
            If Row(CountryTaxDAL.COL_NAME_TAX6_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CountryTaxDAL.COL_NAME_TAX6_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CountryTaxDAL.COL_NAME_TAX6_DESCRIPTION, Value)
        End Set
    End Property

    Public Property Tax6ComputeMethodId As Guid
        Get
            CheckDeleted()
            If row(CountryTaxDAL.COL_NAME_TAX6_COMPUTE_METHOD_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CountryTaxDAL.COL_NAME_TAX6_COMPUTE_METHOD_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CountryTaxDAL.COL_NAME_TAX6_COMPUTE_METHOD_ID, Value)
        End Set
    End Property

    Public Property Tax6PercentFlagId As Guid
        Get
            CheckDeleted()
            If row(CountryTaxDAL.COL_NAME_TAX6_PERCENT_FLAG_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CountryTaxDAL.COL_NAME_TAX6_PERCENT_FLAG_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CountryTaxDAL.COL_NAME_TAX6_PERCENT_FLAG_ID, Value)
        End Set
    End Property

    <ValidTaxPercent("", 6)> _
    Public Property Tax6Percent As DecimalType
        Get
            CheckDeleted()
            If Row(CountryTaxDAL.COL_NAME_TAX6_PERCENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CountryTaxDAL.COL_NAME_TAX6_PERCENT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CountryTaxDAL.COL_NAME_TAX6_PERCENT, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property CompanyTypeId As Guid
        Get
            CheckDeleted()
            If row(CountryTaxDAL.COL_NAME_COMPANY_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CountryTaxDAL.COL_NAME_COMPANY_TYPE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CountryTaxDAL.COL_NAME_COMPANY_TYPE_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property ProductTaxTypeId As Guid
        Get
            CheckDeleted()
            If Row(CountryTaxDAL.COL_NAME_PRODUCT_TAX_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CountryTaxDAL.COL_NAME_PRODUCT_TAX_TYPE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CountryTaxDAL.COL_NAME_PRODUCT_TAX_TYPE_ID, Value)
        End Set
    End Property
    'REQ-1150
    Public Property DealerId As Guid
        Get
            CheckDeleted()
            If Row(CountryTaxDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CountryTaxDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CountryTaxDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property

    'Public ReadOnly Property ComputeMethodCode() As String
    '    Get
    '        Return LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(TAX_PERCENT_FLAG, _
    '                   ElitaPlusIdentity.Current.ActiveUser.LanguageId, True), Me.PercentFlag) = TAX_PERCENT_CODE

    '        'Return LookupListNew.GetCodeFromId(LookupListNew.LK_TAX_TYPES, Me.TaxTypeId)
    '    End Get

    'End Property

    Public Shared ReadOnly Property isInvoiceTaxEnabled As Boolean
        Get
            Try
                Dim dv As DataView
                dv = CountryTax.getList(ElitaPlusIdentity.Current.ActiveUser.Countries, ElitaPlusIdentity.Current.ActiveUser.Companies, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                If Not dv Is Nothing AndAlso dv.Count > 0 Then
                    dv.RowFilter = String.Format("{0} = '{1}'", CountryTaxDAL.COL_NAME_LIST_TAX_CODE, TaxTypeCode.INVOICE)
                    If dv.Count > 0 Then Return True
                End If
            Catch ex As Exception
                Return False
            End Try

            Return False

        End Get
    End Property

#End Region

#Region "Derived Properties"


    Public ReadOnly Property TaxGroup(ByVal taxGroupNum As Integer) As TaxGroupData
        Get
            CheckDeleted()
            If taxGroupNum < 1 OrElse taxGroupNum > MAX_NUMBER_OF_TAX_GROUPS Then
                Throw New DataNotFoundException(String.Format("Max tax group number of country tax is {0}", MAX_NUMBER_OF_TAX_GROUPS))
            End If

            Dim sColName As String
            Dim oTG As TaxGroupData = New TaxGroupData()

            With oTG

                sColName = String.Format(Col_TaxGroup_Description, taxGroupNum)
                If Row(sColName) Is DBNull.Value Then
                    .Description = Nothing
                Else
                    .Description = Row(sColName)
                End If

                sColName = String.Format(Col_TaxGroup_ComputeMethod, taxGroupNum)
                If Row(sColName) Is DBNull.Value Then
                    .CompMethod = Nothing
                Else
                    .CompMethod = New Guid(CType(Row(sColName), Byte()))
                End If

                sColName = String.Format(Col_TaxGroup_PercentFlag, taxGroupNum)
                If Row(sColName) Is DBNull.Value Then
                    .PercentFlag = Nothing
                Else
                    .PercentFlag = New Guid(CType(Row(sColName), Byte()))
                End If

                sColName = String.Format(Col_TaxGroup_Percent, taxGroupNum)
                If Row(sColName) Is DBNull.Value Then
                    .Percent = Nothing
                Else
                    .Percent = Row(sColName)
                End If


            End With

            Return oTG
        End Get
    End Property

    Public Shared ReadOnly Property NumberOfTaxGroups As Integer
        Get
            Return MAX_NUMBER_OF_TAX_GROUPS
        End Get
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New CountryTaxDAL
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

    Public Sub SetEffectiveExpirationDates()

        Dim myCal As Calendar = CultureInfo.InvariantCulture.Calendar
        Dim NewEffectivedate As Date = GetMaxExpirationDate(True).ToLongDateString
        Dim NewExpirationDate As Date = Date.Now.ToLongDateString


        If NewEffectivedate <> Date.Now.ToLongDateString Then
            NewEffectivedate = NewEffectivedate.AddDays(1)
            NewExpirationDate = NewEffectivedate
        End If

        NewExpirationDate = myCal.AddDays(NewExpirationDate, -1)
        NewExpirationDate = myCal.AddYears(NewExpirationDate, 1)

        EffectiveDate = NewEffectivedate
        ExpirationDate = NewExpirationDate

    End Sub

    Public Sub Copy(ByVal original As CountryTax)
        If Not IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing country tax")
        End If
        'Copy myself
        CopyFrom(original)
    End Sub

    Public Sub DeleteAndSave()
        CheckDeleted()
        'Added for Def - 809
        CheckRegionTax()
        'end

        BeginEdit()
        Try
            Delete()
            Save()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            cancelEdit()
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Sub

    'Added for Def - 809
    Public Sub CheckRegionTax()
        Try
            Dim dal As New CountryTaxDAL
            Dim recCount As Integer

            recCount = dal.GetRegionTaxCount(CountryId, TaxTypeId, ProductTaxTypeId, DealerId)
            If recCount > 0 Then
                Dim errors() As ValidationError = {New ValidationError("Cannot be deleted as Region Tax records exist for this Country Tax.", GetType(CountryTax), Nothing, "", Nothing)}
                Throw New BOValidationException(errors, GetType(CountryTax).FullName, Nothing)
                'Throw New BOValidationException("Delete Error: ", "Cannot be deleted as Region Tax records exist for this Country Tax.")
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Sub

#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function getEmptyList(ByVal dv As DataView) As System.Data.DataView
        Try
            Dim dsv As DataSet
            dsv = dv.Table().DataSet

            Dim row As DataRow = dsv.Tables(0).NewRow()
            row.Item("country_tax_id") = System.Guid.NewGuid.ToByteArray
            dsv.Tables(0).Rows.Add(row)
            Return New System.Data.DataView(dsv.Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Public Shared Function getList(ByVal oCountryIds As ArrayList, ByVal oCompanyIds As ArrayList, ByVal LanguageId As Guid) As System.Data.DataView
        Try
            Dim dal As New CountryTaxDAL
            Return New System.Data.DataView(dal.LoadList(oCountryIds, oCompanyIds, LanguageId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Function GetMaxExpirationDate(Optional ByVal UseCurrentDate As Boolean = False) As Date
        Try
            Dim dal As New CountryTaxDAL
            Return dal.LoadMaxExpirationDate(CountryId, TaxTypeId, CompanyTypeId, UseCurrentDate, _
                                             ProductTaxTypeId, DealerId)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Sub GetMinEffDateAndMaxExpDate(ByRef MinEffDate As Date, ByRef MaxExpDate As Date, ByRef RcdCount As Integer)
        Try
            Dim dal As New CountryTaxDAL
            dal.LoadMinEffDateMaxExpDate(MinEffDate, MaxExpDate, RcdCount, CountryId, _
                                         TaxTypeId, CompanyTypeId, ProductTaxTypeId, DealerId)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Sub

    Public Shared Function GetTaxRate(ByVal guidcountryID As Guid, ByVal guidTaxTypeID As Guid, _
                                      ByVal guidRegionID As Guid, ByVal dtEffectiveDate As Date, ByVal guidDealerId As Guid) As Decimal
        Dim dal As New CountryTaxDAL
        Return dal.LoadTaxRate(guidcountryID, guidTaxTypeID, guidRegionID, dtEffectiveDate, guidDealerId)
    End Function

    Public Shared Function getManualTaxesByTaxType(ByVal oCountryId As Guid, ByVal TaxTypeCode As String, ByVal dtEffective As Date, ByVal DealerId As Guid) As System.Data.DataView
        Try
            Dim dal As New CountryTaxDAL
            Return New System.Data.DataView(dal.LoadManualTaxes(oCountryId, TaxTypeCode, dtEffective, DealerId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#End Region

#Region "TaxGroupData class definition"
    <Serializable> Public Class TaxGroupData
        Public Description As String
        Public CompMethod As Guid
        Public PercentFlag As Guid
        Public Percent As Double
    End Class
#End Region

#Region "Custom Validation"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidTaxGroup
        Inherits ValidBaseAttribute

        Public ReadOnly TaxGroupNum As Integer

        Public Sub New(ByVal fieldDisplayName As String, ByVal taxGroupID As Integer)
            MyBase.New(fieldDisplayName, String.Format(ALL_ENTRIES_REQUIRED, taxGroupID))
            TaxGroupNum = taxGroupID
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As CountryTax = CType(objectToValidate, CountryTax)
            Dim oTG As TaxGroupData = obj.TaxGroup(TaxGroupNum)

            If LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(TAX_COMPUTE_METHOD, _
                       ElitaPlusIdentity.Current.ActiveUser.LanguageId, True), oTG.CompMethod) = MANUAL_COMPUTE_METHOD Then
                Return True
            End If

            'Check all fields have been filled for tax group if any of the 3 fields is not empty
            With oTG
                If .Description = "" AndAlso ((Not .CompMethod.Equals(Guid.Empty)) OrElse (Not .PercentFlag.Equals(Guid.Empty))) Then
                    Return False
                ElseIf .Description <> "" AndAlso ((.CompMethod.Equals(Guid.Empty)) OrElse (.PercentFlag.Equals(Guid.Empty))) Then
                    Return False
                Else
                    Return True
                End If
            End With
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidTaxPercent
        Inherits ValidBaseAttribute

        Public ReadOnly TaxGroupNum As Integer

        Public Sub New(ByVal fieldDisplayName As String, ByVal taxGroupID As Integer)
            MyBase.New(fieldDisplayName, String.Format(PERCENT_GREATER_THAN_ZERO, taxGroupID))
            TaxGroupNum = taxGroupID
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As CountryTax = CType(objectToValidate, CountryTax)

            Dim oTG As TaxGroupData = obj.TaxGroup(TaxGroupNum)

            If LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(TAX_COMPUTE_METHOD, _
                       ElitaPlusIdentity.Current.ActiveUser.LanguageId, True), oTG.CompMethod) = MANUAL_COMPUTE_METHOD Then
                Return True
            End If

            'Check tax Percent is > 0 if is the Percent flag is Percent
            With oTG
                If Not .CompMethod.Equals(Guid.Empty) Then
                    If LookupListNew.GetCodeFromId(LookupListNew.DropdownLookupList(TAX_PERCENT_FLAG, _
                        ElitaPlusIdentity.Current.ActiveUser.LanguageId, True), .PercentFlag) = TAX_PERCENT_CODE Then
                        If .Percent < 0 Then
                            Return False
                        End If
                    End If
                End If
            End With

            Return True

        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidLastEntryForDelete
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, LAST_ENTRY_ONLY)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As CountryTax = CType(objectToValidate, CountryTax)

            If obj.IsDeleted Then 'when deleting
                Dim dtMaxExpiration As Date = obj.GetMaxExpirationDate()
                If (obj.ExpirationDate.Value <> dtMaxExpiration) Then
                    Return False
                End If
            End If
            Return True
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidEffectiveDate
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, INVALID_EFFECTIVE_DATE)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As CountryTax = CType(objectToValidate, CountryTax)

            If Not obj.IsDeleted Then 'Edit or add new
                If (Not obj.EffectiveDate Is Nothing) And (Not obj.ExpirationDate Is Nothing) Then
                    If (obj.EffectiveDate.Value >= obj.ExpirationDate.Value) Then
                        Return False
                    End If
                End If
            End If

            Return True
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidNewEffectiveDate
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, EFFECTIVE_DATE_NOT_GREATER)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As CountryTax = CType(objectToValidate, CountryTax)

            If obj.IsNew Then ' when add new 
                Dim dtMaxExpiration As Date = obj.GetMaxExpirationDate()
                If (dtMaxExpiration <> Date.Parse(CountryTaxDAL.INFINITE_DATE_STR, System.Globalization.CultureInfo.InvariantCulture)) _
                    AndAlso (obj.EffectiveDate <> dtMaxExpiration.AddDays(1)) Then
                    Return False
                End If
            End If

            Return True
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class RequiredAtLeastOneTaxgroupFilled
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, ONE_TAX_GROUP_REQUIRED)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As CountryTax = CType(objectToValidate, CountryTax)
            Dim i As Integer, blnFound As Boolean

            blnFound = False
            For i = 1 To CountryTax.NumberOfTaxGroups
                If Not obj.TaxGroup(i).CompMethod.Equals(Guid.Empty) Then
                    blnFound = True
                    Exit For
                End If
            Next
            Return blnFound
        End Function
    End Class
#End Region
End Class


