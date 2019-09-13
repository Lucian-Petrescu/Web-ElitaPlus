Public Class ProductRewards
    Inherits BusinessObjectBase

#Region "Constants"
    Private Const PRODUCT_EQUIPMENT_FORM001 As String = "PRODUCT_EQUIPMENT_FORM001" ' Expiration date must be greater than or equal to Effective date
    Private Const PRODUCT_EQUIPMENT_FORM003 As String = "PRODUCT_EQUIPMENT_FORM003" ' Effective date can not be in the past or today
    Private Const PRODUCT_REWARDS_FORM002 As String = "PRODUCT_REWARDS_FORM002" ' Duplicate Record : Reward Type, Effective Date and Expiration Date Combination must be unique
    Private Const PRODUCT_REWARDS_FORM004 As String = "PRODUCT_REWARDS_FORM004" ' Overlap reccord
    Private Const PRODUCT_REWARDS_FORM005 As String = "PRODUCT_REWARDS_FORM005" 'To Renewal must be greater than or equal to From Renewal
    Private Const PRODUCT_REWARDS_FORM006 As String = "PRODUCT_REWARDS_FORM006" 'Overlapping not allowed for From Renewal and To Renewal
#End Region

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
            Dim dal As New ProductRewardsDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New ProductRewardsDAL
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


    Public Sub InitTable()
        Me.Dataset.Tables(ProductRewardsDAL.TABLE_NAME).Rows.Clear()
    End Sub

    Public Sub AddRowsToTable(ByVal rowval As DataRow, Optional ByVal updateRowVal As Boolean = False)
        Dim dal As New ProductRewardsDAL
        Me.Row = Me.FindRow(Id, dal.TABLE_KEY_NAME, Me.Dataset.Tables(dal.TABLE_NAME))
        Me.Row(1) = rowval(1)
        Me.Row(2) = rowval(2)
        Me.Row(3) = rowval(3)

    End Sub

#End Region

#Region "Private Members"
    'Initialization code for new objects
    Private Sub Initialize()
    End Sub
#End Region


#Region "Properties"

    'Key Property
    <ValidUniqueCombination(""), ValidateOverlapping("")>
    Public ReadOnly Property Id() As Guid
        Get
            If Row(ProductRewardsDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductRewardsDAL.COL_NAME_PROD_REWARD_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")>
    Public Property ProductCodeId() As Guid
        Get
            CheckDeleted()
            If Row(ProductRewardsDAL.COL_NAME_PRODUCT_CODE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ProductRewardsDAL.COL_NAME_PRODUCT_CODE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ProductRewardsDAL.COL_NAME_PRODUCT_CODE_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=100)>
    Public Property RewardName() As String
        Get
            CheckDeleted()
            If Row(ProductRewardsDAL.COL_NAME_REWARD_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ProductRewardsDAL.COL_NAME_REWARD_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ProductRewardsDAL.COL_NAME_REWARD_NAME, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=100)>
    Public Property RewardType() As String
        Get
            CheckDeleted()
            If Row(ProductRewardsDAL.COL_NAME_REWARD_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ProductRewardsDAL.COL_NAME_REWARD_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ProductRewardsDAL.COL_NAME_REWARD_TYPE, Value)
        End Set
    End Property
    <ValueMandatory(""), ValidNumericRange("", Min:=0, Max:=NEW_MAX_DOUBLE)>
    Public Property RewardAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(ProductRewardsDAL.COL_NAME_REWARD_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ProductRewardsDAL.COL_NAME_REWARD_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ProductRewardsDAL.COL_NAME_REWARD_AMOUNT, Value)
        End Set
    End Property

    <ValidNumericRange("", Min:=0, Max:=NEW_MAX_DOUBLE)>
    Public Property MinPurchasePrice() As DecimalType
        Get
            CheckDeleted()
            If Row(ProductRewardsDAL.COL_NAME_MIN_PURCHASE_PRICE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ProductRewardsDAL.COL_NAME_MIN_PURCHASE_PRICE), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(ProductRewardsDAL.COL_NAME_MIN_PURCHASE_PRICE, Value)
        End Set
    End Property

    <ValidNumericRange("", Min:=0, Max:=9999, MaxExclusive:=False)>
    Public Property DaysToRedeem() As LongType
        Get
            CheckDeleted()
            If Row(ProductRewardsDAL.COL_NAME_DAYS_TO_REDEEM) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ProductRewardsDAL.COL_NAME_DAYS_TO_REDEEM), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(ProductRewardsDAL.COL_NAME_DAYS_TO_REDEEM, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidEffectiveDate("")>
    Public Property EffectiveDate() As DateType
        Get
            CheckDeleted()
            If Row(ProductRewardsDAL.COL_NAME_EFFECTIVE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ProductRewardsDAL.COL_NAME_EFFECTIVE_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(ProductRewardsDAL.COL_NAME_EFFECTIVE_DATE, Value)
        End Set
    End Property
    <ValueMandatory(""), ValidExpirationDate("")>
    Public Property ExpirationDate() As DateType
        Get
            CheckDeleted()
            If Row(ProductRewardsDAL.COL_NAME_EXPIRATION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ProductRewardsDAL.COL_NAME_EXPIRATION_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(ProductRewardsDAL.COL_NAME_EXPIRATION_DATE, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidNumericRange("", Min:=0, Max:=99), ValidateFromRenewal("")>
    Public Property FromRenewal() As LongType
        Get
            CheckDeleted()
            If Row(ProductRewardsDAL.COL_NAME_FROM_RENEWAL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ProductRewardsDAL.COL_NAME_FROM_RENEWAL), Long)
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(ProductRewardsDAL.COL_NAME_FROM_RENEWAL, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidNumericRange("", Min:=0, Max:=99), ValidateToRenewal("")>
    Public Property ToRenewal() As LongType
        Get
            CheckDeleted()
            If Row(ProductRewardsDAL.COL_NAME_TO_RENEWAL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ProductRewardsDAL.COL_NAME_TO_RENEWAL), Long)
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(ProductRewardsDAL.COL_NAME_TO_RENEWAL, Value)
        End Set
    End Property

#End Region

#Region "ProductRewardsSearchDV"


    Public Class ProductRewardsSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_PRODUCT_REWARD_ID As String = "prod_reward_id"
        Public Const COL_PRODUCT_CODE_ID As String = "Product_Code_Id"
        Public Const COL_NAME_REWARD_NAME As String = "reward_name"
        Public Const COL_NAME_REWARD_TYPE As String = "reward_type"
        Public Const COL_NAME_REWARD_AMOUNT As String = "reward_amount"
        Public Const COL_NAME_MIN_PURCHASE_PRICE As String = "min_purchase_price"
        Public Const COL_NAME_DAYS_TO_REDEEM As String = "days_to_redeem"
        Public Const COL_NAME_EFFECTIVE_DATE As String = "effective_date"
        Public Const COL_NAME_EXPIRATION_DATE As String = "expiration_date"
        Public Const COL_NAME_FROM_RENEWAL As String = "from_renewal"
        Public Const COL_NAME_TO_RENEWAL As String = "to_renewal"
#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Function AddNewRowToEmptyDV() As ProductRewardsSearchDV
            Dim dt As DataTable = Me.Table.Clone()
            Dim row As DataRow = dt.NewRow
            row(ProductRewardsSearchDV.COL_PRODUCT_REWARD_ID) = (New Guid()).ToByteArray
            row(ProductRewardsSearchDV.COL_PRODUCT_CODE_ID) = Guid.Empty.ToByteArray
            row(ProductRewardsSearchDV.COL_NAME_REWARD_NAME) = DBNull.Value
            row(ProductRewardsSearchDV.COL_NAME_REWARD_TYPE) = DBNull.Value
            row(ProductRewardsSearchDV.COL_NAME_REWARD_AMOUNT) = DBNull.Value
            row(ProductRewardsSearchDV.COL_NAME_MIN_PURCHASE_PRICE) = DBNull.Value
            row(ProductRewardsSearchDV.COL_NAME_DAYS_TO_REDEEM) = DBNull.Value
            row(ProductRewardsSearchDV.COL_NAME_FROM_RENEWAL) = DBNull.Value
            row(ProductRewardsSearchDV.COL_NAME_TO_RENEWAL) = DBNull.Value
            row(ProductRewardsSearchDV.COL_NAME_EFFECTIVE_DATE) = DBNull.Value
            row(ProductRewardsSearchDV.COL_NAME_EXPIRATION_DATE) = DBNull.Value
            dt.Rows.Add(row)
            Return New ProductRewardsSearchDV(dt)
        End Function

    End Class
#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New ProductRewardsDAL
                dal.Update(Me.Row)
                'Reload the Data from the DB
                If Me.Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Me.Id
                    Me.Dataset = New DataSet
                    Me.Row = Nothing
                    Me.Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function LoadList(ByVal ProductCodeId As Guid) As ProductRewardsSearchDV
        Try
            Dim dal As New ProductRewardsDAL
            Dim BOProd As ProductCode
            '  Return New ProductPolicySearchDV(BOProd.ProductPolicyDetailChildren)
            Return New ProductRewardsSearchDV(dal.LoadList(ElitaPlusIdentity.Current.ActiveUser.LanguageId, ProductCodeId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Private Function ValidateUniqueCombination(ByVal ProductId As Guid, ByVal RewardType As String, ByVal EffectiveDate As Date, ByVal ExpirationDate As Date) As DataView
        Try
            Dim dal As New ProductRewardsDAL
            Return New DataView(dal.ValidateUniqueCombination(ProductId, RewardType, EffectiveDate, ExpirationDate).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Private Function ValidateOverlap(ByVal ProductId As Guid, ByVal RewardType As String, ByVal EffectiveDate As Date, ByVal ExpirationDate As Date) As DataView
        Try
            Dim dal As New ProductRewardsDAL
            Return New DataView(dal.ValidateOverlap(ProductId, RewardType, EffectiveDate, ExpirationDate).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Private Function ValidateRenewalOverlap(ByVal ProductId As Guid, ByVal ProductRewardId As Guid) As DataView
        Try
            Dim dal As New ProductRewardsDAL
            Return New DataView(dal.ValidateRenewalOverlap(ProductId, ProductRewardId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

    Public Class ProductRewardsDetailList
        Inherits BusinessObjectListBase

        Public Sub New(ByVal parent As ProductCode)
            MyBase.New(LoadTable(parent), GetType(ProductRewards), parent)
        End Sub

        Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
            Return CType(bo, ProductRewards).ProductCodeId.Equals(CType(Parent, ProductCode).Id)
        End Function

        Public Function Find(ByVal ProductRewardsId As Guid) As ProductRewards
            Dim bo As ProductRewards
            For Each bo In Me
                If bo.Id.Equals(ProductRewardsId) Then Return bo
            Next
            Return Nothing
        End Function

        Public Function Delete(ByVal ProductRewardsId As Guid)
            Dim bo As ProductRewards
            Dim dr As DataRow

            dr = FindRow(ProductRewardsId, ProductRewardsSearchDV.COL_PRODUCT_REWARD_ID, Parent.Dataset.Tables(ProductRewardsDAL.TABLE_NAME))

            ' dr = Parent.Dataset.Tables(ProductPolicyDAL.TABLE_NAME).Rows.Find(ProductPolicyId)
            If Not (dr Is Nothing) Then
                Parent.Dataset.Tables(ProductRewardsDAL.TABLE_NAME).Rows.Remove(dr)
            End If

        End Function

        Private Shared Function LoadTable(ByVal parent As ProductCode) As DataTable
            Try
                If Not parent.IsChildrenCollectionLoaded(GetType(ProductRewardsDetailList)) Then
                    Dim dal As New ProductRewardsDAL
                    dal.LoadList(parent.Id, parent.Dataset)
                    ' dal.GetPaymentGroupDetail(parent.Dataset, parent.Id)
                    parent.AddChildrenCollection(GetType(ProductRewardsDetailList))
                End If
                Return parent.Dataset.Tables(ProductRewardsDAL.TABLE_NAME)
            Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End Function

    End Class
#Region "Custom Validation"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidExpirationDate
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, PRODUCT_EQUIPMENT_FORM001)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ProductRewards = CType(objectToValidate, ProductRewards)

            Dim bValid As Boolean = True

            If Not obj.ExpirationDate Is Nothing And Not obj.EffectiveDate Is Nothing Then
                If Convert.ToDateTime(obj.EffectiveDate.Value) > Convert.ToDateTime(obj.ExpirationDate.Value) Then
                    Me.Message = PRODUCT_EQUIPMENT_FORM001
                    bValid = False

                End If
            End If
            Return bValid

        End Function

    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidEffectiveDate
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, PRODUCT_EQUIPMENT_FORM003)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ProductRewards = CType(objectToValidate, ProductRewards)

            Dim bValid As Boolean = True

            If Not obj.EffectiveDate Is Nothing Then
                If obj.EffectiveDate <= DateTime.Now.Date Then
                    Me.Message = PRODUCT_EQUIPMENT_FORM003
                    bValid = False

                End If
            End If
            Return bValid

        End Function
    End Class



    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidUniqueCombination
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, PRODUCT_REWARDS_FORM002)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ProductRewards = CType(objectToValidate, ProductRewards)
            Dim bValid As Boolean = True

            If Not obj.ProductCodeId = Guid.Empty Then

                If Not obj.RewardType Is Nothing AndAlso Not obj.EffectiveDate Is Nothing AndAlso Not obj.ExpirationDate Is Nothing Then
                    Dim oProductRewards As DataView = obj.ValidateUniqueCombination(obj.ProductCodeId, obj.RewardType, obj.EffectiveDate, obj.ExpirationDate)
                    Dim ProductRewardsRows As DataRowCollection = oProductRewards.Table.Rows
                    Dim ProductRewardsRow As DataRow

                    If ProductRewardsRows.Count > 1 Then
                        bValid = False
                    Else
                        'only one record for the combination
                        bValid = True
                    End If

                End If
            End If
            Return bValid
        End Function
    End Class
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidateOverlapping
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, PRODUCT_REWARDS_FORM004)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ProductRewards = CType(objectToValidate, ProductRewards)
            Dim bValid As Boolean = True

            If Not obj.ProductCodeId = Guid.Empty Then

                If Not obj.RewardType Is Nothing AndAlso Not obj.EffectiveDate Is Nothing AndAlso Not obj.ExpirationDate Is Nothing Then
                    Dim oProductRewards As DataView = obj.ValidateOverlap(obj.ProductCodeId, obj.RewardType, obj.EffectiveDate, obj.ExpirationDate)
                    Dim ProductRewardsRows As DataRowCollection = oProductRewards.Table.Rows
                    Dim ProductRewardsRow As DataRow

                    If ProductRewardsRows.Count > 1 Then

                        bValid = False
                    Else
                        'No Overlap
                        bValid = True
                    End If

                End If
            End If
            Return bValid
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidateToRenewal
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, PRODUCT_REWARDS_FORM005)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ProductRewards = CType(objectToValidate, ProductRewards)

            Dim bValid As Boolean = True

            If Not obj.FromRenewal Is Nothing And Not obj.ToRenewal Is Nothing Then
                If obj.FromRenewal.Value > obj.ToRenewal.Value Then
                    Me.Message = PRODUCT_REWARDS_FORM005
                    bValid = False

                End If
            End If
            Return bValid

        End Function

    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidateFromRenewal
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, PRODUCT_REWARDS_FORM006)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ProductRewards = CType(objectToValidate, ProductRewards)

            Dim bValid As Boolean = True
            Dim oProductRewards As DataView = obj.ValidateRenewalOverlap(obj.ProductCodeId, obj.Id)
            Dim ProductRewardsRows As DataRowCollection = oProductRewards.Table.Rows
            Dim ProductRewardsRow As DataRow

            If oProductRewards.Count = 1 And Not obj.FromRenewal Is Nothing Then
                If oProductRewards.Item(0)(0).ToString = String.Empty Then
                    bValid = True
                Else
                    If CType(oProductRewards.Item(0)(0), Long) >= obj.FromRenewal.Value Then
                        bValid = False
                    Else
                        'No Overlap
                        bValid = True
                    End If
                End If
            Else
                bValid = True
            End If
            Return bValid

        End Function

    End Class
#End Region
End Class
