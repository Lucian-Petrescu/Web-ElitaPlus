'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (1/19/2016)  ********************
Imports System.Reflection
Imports System.Collections.Generic
Public Class ClaimBonusSettings
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
            Dim dal As New ClaimBonusSettingsDAL
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

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New ClaimBonusSettingsDAL
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
        Effective = Date.Now
        Expiration = New Date(2499, 12, 31, 23, 59, 59)
    End Sub



#End Region


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid
        Get
            If Row(ClaimBonusSettingsDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimBonusSettingsDAL.COL_NAME_CLAIM_BONUS_SETTINGS_ID), Byte()))
            End If
        End Get
    End Property


    Public Property ServiceCenterId As Guid
        Get
            CheckDeleted()
            If Row(ClaimBonusSettingsDAL.COL_NAME_SERVICE_CENTER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimBonusSettingsDAL.COL_NAME_SERVICE_CENTER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimBonusSettingsDAL.COL_NAME_SERVICE_CENTER_ID, Value)
        End Set
    End Property

    Public ReadOnly Property ServiceCenter As String
        Get
            CheckDeleted()
            If Row(ClaimBonusSettingsDAL.COL_NAME_SERVICE_CENTER_ID) Is DBNull.Value Then
                Return String.Empty
            Else
                Return LookupListNew.GetCodeFromId(LookupListNew.LK_SERVICE_CENTERS, New Guid(CType(Row(ClaimBonusSettingsDAL.COL_NAME_SERVICE_CENTER_ID), Byte())))
            End If
        End Get
    End Property

    Public Property DealerId As Guid
        Get
            CheckDeleted()
            If Row(ClaimBonusSettingsDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimBonusSettingsDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimBonusSettingsDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property

    Public ReadOnly Property Dealer As String
        Get
            CheckDeleted()
            If Row(ClaimBonusSettingsDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return String.Empty
            Else
                Return LookupListNew.GetCodeFromId(LookupListNew.LK_DEALERS, New Guid(CType(Row(ClaimBonusSettingsDAL.COL_NAME_DEALER_ID), Byte())))
            End If
        End Get
    End Property
    Public Property ProductCodeId As Guid
        Get
            CheckDeleted()
            If Row(ClaimBonusSettingsDAL.COL_NAME_PRODUCT_CODE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimBonusSettingsDAL.COL_NAME_PRODUCT_CODE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimBonusSettingsDAL.COL_NAME_PRODUCT_CODE_ID, Value)
        End Set
    End Property
    Public ReadOnly Property ProductCode As String
        Get
            CheckDeleted()
            If Row(ClaimBonusSettingsDAL.COL_NAME_PRODUCT_CODE_ID) Is DBNull.Value Then
                Return String.Empty
            Else
                Return LookupListNew.GetCodeFromId(LookupListNew.LK_PRODUCTCODE_BY_DEALER, New Guid(CType(Row(ClaimBonusSettingsDAL.COL_NAME_PRODUCT_CODE_ID), Byte())))
            End If
        End Get
    End Property

    <ValueMandatory("")>
    Public Property BonusComputeMethodId As Guid
        Get
            CheckDeleted()
            If Row(ClaimBonusSettingsDAL.COL_NAME_BONUS_COMPUTE_METHOD_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimBonusSettingsDAL.COL_NAME_BONUS_COMPUTE_METHOD_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimBonusSettingsDAL.COL_NAME_BONUS_COMPUTE_METHOD_ID, Value)
        End Set
    End Property

    Public ReadOnly Property BonusComputeMethod As String
        Get
            CheckDeleted()
            If Row(ClaimBonusSettingsDAL.COL_NAME_BONUS_COMPUTE_METHOD_ID) Is DBNull.Value Then
                Return String.Empty
            Else
                Return LookupListNew.GetCodeFromId(LookupListNew.LK_BONUS_COMPUTATION_METHOD, New Guid(CType(Row(ClaimBonusSettingsDAL.COL_NAME_BONUS_COMPUTE_METHOD_ID), Byte())))
            End If
        End Get
    End Property
    <ValueMandatory(""), ValidNumericRange("", Max:=6, Min:=1, Message:="VALID_BONUS_MONTH_VALUE")>
    Public Property BonusAmountPeriodMonth As LongType
        Get
            CheckDeleted()
            If Row(ClaimBonusSettingsDAL.COL_NAME_BONUS_AMOUNT_PERIOD_MONTH) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ClaimBonusSettingsDAL.COL_NAME_BONUS_AMOUNT_PERIOD_MONTH), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimBonusSettingsDAL.COL_NAME_BONUS_AMOUNT_PERIOD_MONTH, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("", Max:=100, Min:=1, Message:="VALID_REPLACEMENT_PERCENTAGE")>
    Public Property ScReplacementPct As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimBonusSettingsDAL.COL_NAME_SC_REPLACEMENT_PCT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ClaimBonusSettingsDAL.COL_NAME_SC_REPLACEMENT_PCT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimBonusSettingsDAL.COL_NAME_SC_REPLACEMENT_PCT, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidNumericRange("", Max:=99, Min:=1)>
    Public Property ScAvgTat As LongType
        Get
            CheckDeleted()
            If Row(ClaimBonusSettingsDAL.COL_NAME_SC_AVG_TAT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ClaimBonusSettingsDAL.COL_NAME_SC_AVG_TAT), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimBonusSettingsDAL.COL_NAME_SC_AVG_TAT, Value)
        End Set
    End Property


    Public Property Pecoramount As DecimalType
        Get
            CheckDeleted()
            If Row(ClaimBonusSettingsDAL.COL_NAME_PECORAMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ClaimBonusSettingsDAL.COL_NAME_PECORAMOUNT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimBonusSettingsDAL.COL_NAME_PECORAMOUNT, Value)
        End Set
    End Property


    <ValidNumericRange("", Max:=99, Min:=0, Message:="VALID_BONUS_SETTINGS_PRIORITY")>
    Public Property Priority As LongType
        Get
            CheckDeleted()
            If Row(ClaimBonusSettingsDAL.COL_NAME_PRIORITY) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ClaimBonusSettingsDAL.COL_NAME_PRIORITY), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimBonusSettingsDAL.COL_NAME_PRIORITY, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property Effective As DateTimeType
        Get
            CheckDeleted()
            If Row(ClaimBonusSettingsDAL.COL_NAME_EFFECTIVE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateTimeType(CType(Row(ClaimBonusSettingsDAL.COL_NAME_EFFECTIVE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimBonusSettingsDAL.COL_NAME_EFFECTIVE, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property Expiration As DateTimeType
        Get
            CheckDeleted()
            If Row(ClaimBonusSettingsDAL.COL_NAME_EXPIRATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateTimeType(CType(Row(ClaimBonusSettingsDAL.COL_NAME_EXPIRATION), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimBonusSettingsDAL.COL_NAME_EXPIRATION, Value)
        End Set
    End Property




#End Region


#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ClaimBonusSettingsDAL
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

    Public Overrides ReadOnly Property IsDirty As Boolean
        Get
            Dim bDirty As Boolean

            bDirty = MyBase.IsDirty OrElse IsChildrenDirty

            Return bDirty
        End Get
    End Property

    Public Sub Copy(ByVal original As ClaimBonusSettings)
        If Not IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Bonus Structure")
        End If
        'Copy myself
        CopyFrom(original)
    End Sub

    Public Shared Sub DeleteBonusSettings(ByVal bonussettingid As Guid)
        Dim dal As New ClaimBonusSettingsDAL
        dal.Delete(bonussettingid)
    End Sub


    Public Sub DeleteAndSave()
        CheckDeleted()
        'Dim addr As Address = Me.Address
        BeginEdit()
        'addr.BeginEdit()
        Try
            Delete()
            'addr.Delete()
            Save()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            cancelEdit()
            'addr.cancelEdit()
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Sub

#End Region

#Region "Private Memebers"

    Private Function CheckDuplicateBonusStructure() As Boolean
        Dim dal As New ClaimBonusSettingsDAL
        Dim companyGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
        Dim dv As ClaimBonusSettings.BonusSettingsDV = GetList(DealerId, ServiceCenterId, ProductCodeId)

        For Each dr As DataRow In dv.Table.Rows
            If (dr(ClaimBonusSettingsDAL.COL_NAME_SERVICE_CENTER_ID) = ServiceCenterId) AndAlso (dr(ClaimBonusSettingsDAL.COL_NAME_DEALER_ID) = DealerId) AndAlso (dr(ClaimBonusSettingsDAL.COL_NAME_PRODUCT_CODE_ID) = ProductCodeId) Then
                If (Not New Guid(CType(dr(ClaimBonusSettingsDAL.COL_NAME_CLAIM_BONUS_SETTINGS_ID), Byte())).Equals(Id)) Then
                    Return True
                End If
            End If
        Next
        Return False
    End Function

#End Region

#Region "DataView Retrieveing Methods"

#End Region

#Region "Bonus Settings Search Dataview"
    Public Class BonusSettingsDV
        Inherits DataView

#Region "Constants"
        Public Const COL_NAME_CLAIM_BONUS_SETTINGS_ID As String = ClaimBonusSettingsDAL.COL_NAME_CLAIM_BONUS_SETTINGS_ID
        Public Const COL_NAME_SERVICE_CENTER As String = ClaimBonusSettingsDAL.COL_NAME_SERVICE_CENTER
        Public Const COL_NAME_DEALER_NAME As String = ClaimBonusSettingsDAL.COL_NAME_DEALER_NAME
        Public Const COL_NAME_PRODUCT_CODE As String = ClaimBonusSettingsDAL.COL_NAME_PRODUCT_CODE
        Public Const COL_NAME_BONUS_COMPUTE_METHOD As String = ClaimBonusSettingsDAL.COL_NAME_BONUS_COMPUTE_METHOD
        Public Const COL_NAME_SC_REPLACEMENT_PCT As String = ClaimBonusSettingsDAL.COL_NAME_SC_REPLACEMENT_PCT
        Public Const COL_NAME_SC_AVG_TAT As String = ClaimBonusSettingsDAL.COL_NAME_SC_AVG_TAT
        Public Const COL_NAME_PERCENTAGE_OR_AMOUNT As String = ClaimBonusSettingsDAL.COL_NAME_PECORAMOUNT
        Public Const COL_NAME_PRIORITY As String = ClaimBonusSettingsDAL.COL_NAME_PRIORITY
        Public Const COL_NAME_BONUS_AMOUNT_PERIOD As String = ClaimBonusSettingsDAL.COL_NAME_BONUS_AMOUNT_PERIOD_MONTH
        Public Const COL_NAME_EFFECTIVE_DATE As String = ClaimBonusSettingsDAL.COL_NAME_EFFECTIVE
        Public Const COL_NAME_EXPIRATION_DATE As String = ClaimBonusSettingsDAL.COL_NAME_EXPIRATION


#End Region
        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property BonusSettingsId(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_CLAIM_BONUS_SETTINGS_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property Dealer(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_DEALER_NAME).ToString
            End Get
        End Property
        Public Shared ReadOnly Property ServiceCenter(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_SERVICE_CENTER).ToString
            End Get
        End Property
        Public Shared ReadOnly Property ProductCode(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_PRODUCT_CODE).ToString
            End Get
        End Property

        Public Shared ReadOnly Property BonuscomputationMethod(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_BONUS_COMPUTE_METHOD).ToString
            End Get
        End Property

        Public Shared ReadOnly Property SCReplacementPercentage(ByVal row) As String
            Get
                Return row(COL_NAME_SC_REPLACEMENT_PCT).ToString
            End Get
        End Property

        Public Shared ReadOnly Property SCAvgTurnAroundTime(ByVal row) As String
            Get
                Return row(COL_NAME_SC_AVG_TAT).ToString
            End Get
        End Property
        Public Shared ReadOnly Property BonusAmountPeriod(ByVal row) As String
            Get
                Return row(COL_NAME_BONUS_AMOUNT_PERIOD).ToString
            End Get
        End Property
        Public Shared ReadOnly Property EffectiveDate(ByVal row) As String
            Get
                Return row(COL_NAME_EFFECTIVE_DATE).ToString
            End Get
        End Property
        Public Shared ReadOnly Property ExpirationDate(ByVal row) As String
            Get
                Return row(COL_NAME_EXPIRATION_DATE).ToString
            End Get
        End Property

    End Class

    Shared Function GetList(ByVal dealerId As Guid, ByVal servicecenterId As Guid, ByVal productcodeId As Guid) As BonusSettingsDV
        Try
            Dim dal As New ClaimBonusSettingsDAL
            Dim companyIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Dim companyGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            Return New BonusSettingsDV(dal.LoadList(dealerId, servicecenterId, productcodeId, companyIds, companyGroupId).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetNewDataViewRow(ByVal dv As BonusSettingsDV, ByVal bo As ClaimBonusSettings) As BonusSettingsDV

        Dim dt As DataTable
        dt = dv.Table

        If bo.IsNew Then
            Dim row As DataRow = dt.NewRow

            row(BonusSettingsDV.COL_NAME_SERVICE_CENTER) = bo.ServiceCenter
            row(BonusSettingsDV.COL_NAME_CLAIM_BONUS_SETTINGS_ID) = bo.Id.ToByteArray
            row(BonusSettingsDV.COL_NAME_DEALER_NAME) = bo.Dealer
            row(BonusSettingsDV.COL_NAME_PRODUCT_CODE) = bo.ProductCode


            row(BonusSettingsDV.COL_NAME_BONUS_COMPUTE_METHOD) = bo.BonusComputeMethod
            row(BonusSettingsDV.COL_NAME_SC_REPLACEMENT_PCT) = bo.ScReplacementPct

            row(BonusSettingsDV.COL_NAME_SC_AVG_TAT) = bo.ScAvgTat

            row(BonusSettingsDV.COL_NAME_BONUS_AMOUNT_PERIOD) = bo.BonusAmountPeriodMonth

            row(BonusSettingsDV.COL_NAME_EFFECTIVE_DATE) = bo.Effective


            If (bo.Expiration Is Nothing) Then
                row(BonusSettingsDV.COL_NAME_EXPIRATION_DATE) = DBNull.Value
            Else
                row(BonusSettingsDV.COL_NAME_EXPIRATION_DATE) = bo.Expiration
            End If
            dt.Rows.Add(row)
        End If


    End Function

    Public Shared Function GetClaimBonusSettingCount(ByVal dealerId As Guid, ByVal servicecenterId As Guid, ByVal productcodeId As Guid, ByVal bonussettingsId As Guid) As Integer

        Dim dal As New ClaimBonusSettingsDAL

        Dim constant As Integer = CType(dal.ClaimBonusSettingsCount(dealerId, servicecenterId, productcodeId, bonussettingsId), Integer)

        Return constant

    End Function


#End Region

#Region "Custom Validators"
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class CheckDuplicate
        Inherits ValidBaseAttribute
        Private Const DUPLICATE_BONUS_STRUCTURE As String = "DUPLICATE_BONUS_STRUCTURE"

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, DUPLICATE_BONUS_STRUCTURE)
        End Sub

        Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ClaimBonusSettings = CType(objectToValidate, ClaimBonusSettings)
            If (obj.CheckDuplicateBonusStructure()) Then
                Return False
            Else
                Return True
            End If
        End Function
    End Class
#End Region

End Class






