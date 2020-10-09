
Public Class PremiumAdjustmentSettings
    Inherits BusinessObjectBase

#Region "Constructors"

    'Existing BO
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

    'Existing BO attaching to a BO family
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

    Protected Sub Load()
        Try
            Dim dal As New PremiumAdjustmentSettingsDAL
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
            Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            setvalue(dal.TABLE_KEY_NAME, Guid.NewGuid)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Sub

    Protected Sub Load(id As Guid)
        Try
            Row = Nothing
            Dim dal As New PremiumAdjustmentSettingsDAL
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

    Public Const NEW_MAX_PERCENTAGE As Double = 99.9999

#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid
        Get
            If row(PremiumAdjustmentSettingsDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PremiumAdjustmentSettingsDAL.COL_NAME_PREMIUM_ADJUSTMENT_SETTING_ID), Byte()))
            End If
        End Get
    End Property


    <ValueMandatory("")> _
    Public Property DealerId As Guid
        Get
            CheckDeleted()
            If Row(PremiumAdjustmentSettingsDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PremiumAdjustmentSettingsDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PremiumAdjustmentSettingsDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property AdjustmentBy As Guid
        Get
            CheckDeleted()
            If Row(PremiumAdjustmentSettingsDAL.COL_NAME_ADJUSTMENT_BY) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PremiumAdjustmentSettingsDAL.COL_NAME_ADJUSTMENT_BY), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PremiumAdjustmentSettingsDAL.COL_NAME_ADJUSTMENT_BY, Value)
        End Set
    End Property



    Public Property AdjustmentBasedOn As Guid
        Get
            CheckDeleted()
            If Row(PremiumAdjustmentSettingsDAL.COL_NAME_ADJUSTMENT_BASED_ON) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PremiumAdjustmentSettingsDAL.COL_NAME_ADJUSTMENT_BASED_ON), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PremiumAdjustmentSettingsDAL.COL_NAME_ADJUSTMENT_BASED_ON, Value)
        End Set
    End Property



    Public Property EffectiveDate As DateType
        Get
            CheckDeleted()
            If Row(PremiumAdjustmentSettingsDAL.COL_NAME_EFFECTIVE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(PremiumAdjustmentSettingsDAL.COL_NAME_EFFECTIVE_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PremiumAdjustmentSettingsDAL.COL_NAME_EFFECTIVE_DATE, Value)
        End Set
    End Property

    Public Property ExpirationDate As DateType
        Get
            CheckDeleted()
            If Row(PremiumAdjustmentSettingsDAL.COL_NAME_EXPIRATION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(PremiumAdjustmentSettingsDAL.COL_NAME_EXPIRATION_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PremiumAdjustmentSettingsDAL.COL_NAME_EXPIRATION_DATE, Value)
        End Set
    End Property

    <ValidNumericRange("", Max:=NEW_MAX_PERCENTAGE)> _
    Public Property AdjustmentPercentage As DecimalType
        Get
            CheckDeleted()
            If Row(PremiumAdjustmentSettingsDAL.COL_NAME_ADJUSTMENT_PERCENTAGE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(PremiumAdjustmentSettingsDAL.COL_NAME_ADJUSTMENT_PERCENTAGE), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PremiumAdjustmentSettingsDAL.COL_NAME_ADJUSTMENT_PERCENTAGE, Value)
        End Set
    End Property

    <ValidNumericRange("", Max:=NEW_MAX_DOUBLE)> _
    Public Property AdjustmentAmount As DecimalType
        Get
            CheckDeleted()
            If Row(PremiumAdjustmentSettingsDAL.COL_NAME_ADJUSTMENT_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(PremiumAdjustmentSettingsDAL.COL_NAME_ADJUSTMENT_AMOUNT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PremiumAdjustmentSettingsDAL.COL_NAME_ADJUSTMENT_AMOUNT, Value)
        End Set
    End Property

#End Region




#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            Dim dal As New PremiumAdjustmentSettingsDAL
            dal.Update(Dataset)
            'Reload the Data
            If _isDSCreator AndAlso Row.RowState <> DataRowState.Detached Then
                'Reload the Data from the DB
                Load(Id)
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try

    End Sub



    'Public Shared Sub Delete(ByVal Premium_Adjustment_Setting_Id As Guid)

    '    Dim dal As New PremiumAdjustmentSettingsDAL
    '    dal.Delete(Premium_Adjustment_Setting_Id)
    'End Sub

#End Region


#Region "DataView Retrieveing Methods"

    Public Shared Function getList(DealerId As Guid) As PremiumAdjustnmentSettingSearchDV
        Try
            Dim dal As New PremiumAdjustmentSettingsDAL
            Dim compIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Return New PremiumAdjustnmentSettingSearchDV(dal.LoadList(DealerId, compIds).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function



#Region "PremiumAdjustnmentSettingSearchDV"
    Public Class PremiumAdjustnmentSettingSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_PREMIUM_ADJUSTMENT_SETTING_ID As String = "premium_adjustment_setting_id"
        Public Const COL_DEALER_CODE As String = "dealer_code"
        Public Const COL_ADJUSTMENT_BY As String = "adjustment_by"
        Public Const COL_EFFECTIVE_DATE As String = "effective_date"
#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region

#End Region
End Class
