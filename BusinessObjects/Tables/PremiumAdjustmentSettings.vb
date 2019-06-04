
Public Class PremiumAdjustmentSettings
    Inherits BusinessObjectBase

#Region "Constructors"

    'Existing BO
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

    'Existing BO attaching to a BO family
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

    Protected Sub Load()
        Try
            Dim dal As New PremiumAdjustmentSettingsDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            setvalue(dal.TABLE_KEY_NAME, Guid.NewGuid)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Me.Row = Nothing
            Dim dal As New PremiumAdjustmentSettingsDAL
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

#Region "CONSTANTS"

    Public Const NEW_MAX_PERCENTAGE As Double = 99.9999

#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If row(PremiumAdjustmentSettingsDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PremiumAdjustmentSettingsDAL.COL_NAME_PREMIUM_ADJUSTMENT_SETTING_ID), Byte()))
            End If
        End Get
    End Property


    <ValueMandatory("")> _
    Public Property DealerId() As Guid
        Get
            CheckDeleted()
            If Row(PremiumAdjustmentSettingsDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PremiumAdjustmentSettingsDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(PremiumAdjustmentSettingsDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property AdjustmentBy() As Guid
        Get
            CheckDeleted()
            If Row(PremiumAdjustmentSettingsDAL.COL_NAME_ADJUSTMENT_BY) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PremiumAdjustmentSettingsDAL.COL_NAME_ADJUSTMENT_BY), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(PremiumAdjustmentSettingsDAL.COL_NAME_ADJUSTMENT_BY, Value)
        End Set
    End Property



    Public Property AdjustmentBasedOn() As Guid
        Get
            CheckDeleted()
            If Row(PremiumAdjustmentSettingsDAL.COL_NAME_ADJUSTMENT_BASED_ON) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(PremiumAdjustmentSettingsDAL.COL_NAME_ADJUSTMENT_BASED_ON), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(PremiumAdjustmentSettingsDAL.COL_NAME_ADJUSTMENT_BASED_ON, Value)
        End Set
    End Property



    Public Property EffectiveDate() As DateType
        Get
            CheckDeleted()
            If Row(PremiumAdjustmentSettingsDAL.COL_NAME_EFFECTIVE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(PremiumAdjustmentSettingsDAL.COL_NAME_EFFECTIVE_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(PremiumAdjustmentSettingsDAL.COL_NAME_EFFECTIVE_DATE, Value)
        End Set
    End Property

    Public Property ExpirationDate() As DateType
        Get
            CheckDeleted()
            If Row(PremiumAdjustmentSettingsDAL.COL_NAME_EXPIRATION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(PremiumAdjustmentSettingsDAL.COL_NAME_EXPIRATION_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(PremiumAdjustmentSettingsDAL.COL_NAME_EXPIRATION_DATE, Value)
        End Set
    End Property

    <ValidNumericRange("", Max:=NEW_MAX_PERCENTAGE)> _
    Public Property AdjustmentPercentage() As DecimalType
        Get
            CheckDeleted()
            If Row(PremiumAdjustmentSettingsDAL.COL_NAME_ADJUSTMENT_PERCENTAGE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(PremiumAdjustmentSettingsDAL.COL_NAME_ADJUSTMENT_PERCENTAGE), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(PremiumAdjustmentSettingsDAL.COL_NAME_ADJUSTMENT_PERCENTAGE, Value)
        End Set
    End Property

    <ValidNumericRange("", Max:=NEW_MAX_DOUBLE)> _
    Public Property AdjustmentAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(PremiumAdjustmentSettingsDAL.COL_NAME_ADJUSTMENT_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(PremiumAdjustmentSettingsDAL.COL_NAME_ADJUSTMENT_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(PremiumAdjustmentSettingsDAL.COL_NAME_ADJUSTMENT_AMOUNT, Value)
        End Set
    End Property

#End Region




#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            Dim dal As New PremiumAdjustmentSettingsDAL
            dal.Update(Me.Dataset)
            'Reload the Data
            If Me._isDSCreator AndAlso Me.Row.RowState <> DataRowState.Detached Then
                'Reload the Data from the DB
                Me.Load(Me.Id)
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

    Public Shared Function getList(ByVal DealerId As Guid) As PremiumAdjustnmentSettingSearchDV
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

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region

#End Region
End Class
