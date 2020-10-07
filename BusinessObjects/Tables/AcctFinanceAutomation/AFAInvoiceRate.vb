'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (1/20/2015)  ********************

Public Class AfaInvoiceRate
    Inherits BusinessObjectBase
    Implements IExpirable

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
            Dim dal As New AFAInvoiceRateDAL
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
            Dim dal As New AFAInvoiceRateDAL
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

    '#Region "Constants"
    '    Public Const UNIQUE_BY_TIER As String = "BY_TIER"
    '    Public Const UNIQUE_BY_INSCD As String = "BY_INSCD"
    '#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid Implements IExpirable.ID
        Get
            If Row(AFAInvoiceRateDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AFAInvoiceRateDAL.COL_NAME_AFA_INVOICE_RATE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")>
    Public Property AfaProductId As Guid
        Get
            CheckDeleted()
            If Row(AFAInvoiceRateDAL.COL_NAME_AFA_PRODUCT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(AFAInvoiceRateDAL.COL_NAME_AFA_PRODUCT_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AFAInvoiceRateDAL.COL_NAME_AFA_PRODUCT_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=100)>
    Public Property InsuranceCode As String
        Get
            CheckDeleted()
            If Row(AFAInvoiceRateDAL.COL_NAME_INSURANCE_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AFAInvoiceRateDAL.COL_NAME_INSURANCE_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AFAInvoiceRateDAL.COL_NAME_INSURANCE_CODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=10)>
    Public Property Tier As String
        Get
            CheckDeleted()
            If Row(AFAInvoiceRateDAL.COL_NAME_TIER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AFAInvoiceRateDAL.COL_NAME_TIER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AFAInvoiceRateDAL.COL_NAME_TIER, Value)
        End Set
    End Property

    Public Property RegulatoryState As String
        Get
            CheckDeleted()
            If Row(AFAInvoiceRateDAL.COL_NAME_REGULATORY_STATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AFAInvoiceRateDAL.COL_NAME_REGULATORY_STATE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AFAInvoiceRateDAL.COL_NAME_REGULATORY_STATE, Value)
        End Set
    End Property
    <ValueMandatory(""), ValidStringLength("", Max:=50), IsRateDefinitionUnique("LossType")>
    Public Property LossType As String
        Get
            CheckDeleted()
            If Row(AFAInvoiceRateDAL.COL_NAME_LOSS_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AFAInvoiceRateDAL.COL_NAME_LOSS_TYPE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AFAInvoiceRateDAL.COL_NAME_LOSS_TYPE, Value)
        End Set
    End Property



    Public Property RetailAmt As DecimalType
        Get
            CheckDeleted()
            If Row(AFAInvoiceRateDAL.COL_NAME_RETAIL_AMT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(AFAInvoiceRateDAL.COL_NAME_RETAIL_AMT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AFAInvoiceRateDAL.COL_NAME_RETAIL_AMT, Value)
        End Set
    End Property



    Public Property PremiumAmt As DecimalType
        Get
            CheckDeleted()
            If Row(AFAInvoiceRateDAL.COL_NAME_PREMIUM_AMT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(AFAInvoiceRateDAL.COL_NAME_PREMIUM_AMT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AFAInvoiceRateDAL.COL_NAME_PREMIUM_AMT, Value)
        End Set
    End Property



    Public Property CommAmt As DecimalType
        Get
            CheckDeleted()
            If Row(AFAInvoiceRateDAL.COL_NAME_COMM_AMT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(AFAInvoiceRateDAL.COL_NAME_COMM_AMT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AFAInvoiceRateDAL.COL_NAME_COMM_AMT, Value)
        End Set
    End Property



    Public Property AdminAmt As DecimalType
        Get
            CheckDeleted()
            If Row(AFAInvoiceRateDAL.COL_NAME_ADMIN_AMT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(AFAInvoiceRateDAL.COL_NAME_ADMIN_AMT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AFAInvoiceRateDAL.COL_NAME_ADMIN_AMT, Value)
        End Set
    End Property



    Public Property AncillaryAmt As DecimalType
        Get
            CheckDeleted()
            If Row(AFAInvoiceRateDAL.COL_NAME_ANCILLARY_AMT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(AFAInvoiceRateDAL.COL_NAME_ANCILLARY_AMT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AFAInvoiceRateDAL.COL_NAME_ANCILLARY_AMT, Value)
        End Set
    End Property



    Public Property OtherAmt As DecimalType
        Get
            CheckDeleted()
            If Row(AFAInvoiceRateDAL.COL_NAME_OTHER_AMT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(AFAInvoiceRateDAL.COL_NAME_OTHER_AMT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AFAInvoiceRateDAL.COL_NAME_OTHER_AMT, Value)
        End Set
    End Property


    <ValueMandatory(""), NonPastDateValidation(Codes.EFFECTIVE), RejectOverlapsOrGaps("")>
    Public Property Effective As DateTimeType Implements IExpirable.Effective
        Get
            CheckDeleted()
            If Row(AFAInvoiceRateDAL.COL_NAME_EFFECTIVE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateTimeType(CType(Row(AFAInvoiceRateDAL.COL_NAME_EFFECTIVE_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AFAInvoiceRateDAL.COL_NAME_EFFECTIVE_DATE, Value)
        End Set
    End Property


    <ValueMandatory(""), NonPastDateValidation(Codes.EXPIRATION), EffectiveExpirationDateValidation(Codes.EXPIRATION)>
    Public Property Expiration As DateTimeType Implements IExpirable.Expiration
        Get
            CheckDeleted()
            If Row(AFAInvoiceRateDAL.COL_NAME_EXPIRATION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateTimeType(CType(Row(AFAInvoiceRateDAL.COL_NAME_EXPIRATION_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AFAInvoiceRateDAL.COL_NAME_EXPIRATION_DATE, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New AFAInvoiceRateDAL
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
    Public Shared Function getList(ByVal afaProdcutId As Guid) As InvRateSearchDV
        Try
            Dim dal As New AFAInvoiceRateDAL
            Return New InvRateSearchDV(dal.LoadList(afaProdcutId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region


#Region "SearchDV"
    Public Class InvRateSearchDV
        Inherits DataView

        Public Const COL_AFA_INVOICE_RATE_ID As String = "afa_invoice_rate_id"
        Public Const COL_AFA_PRODUCT_ID As String = "afa_product_id"
        Public Const COL_INSURANCE_CODE As String = "insurance_code"
        Public Const COL_TIER As String = "tier"
        Public Const COL_LOSS_TYPE As String = "loss_type"
        Public Const COL_REGULATORY_STATE As String = "regulatory_state"
        Public Const COL_RETAIL_AMT As String = "retail_amt"
        Public Const COL_PREMIUM_AMT As String = "premium_amt"
        Public Const COL_COMM_AMT As String = "comm_amt"
        Public Const COL_ADMIN_AMT As String = "admin_amt"
        Public Const COL_ANCILLARY_AMT As String = "ancillary_amt"
        Public Const COL_OTHER_AMT As String = "other_amt"
        Public Const COL_EFFECTIVE_DATE As String = "effective_date"
        Public Const COL_EXPIRATION_DATE As String = "expiration_date"


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

    Public Shared Sub AddNewRowToSearchDV(ByRef dv As InvRateSearchDV, ByVal NewBO As AfaInvoiceRate)
        Dim dt As DataTable, blnEmptyTbl As Boolean = False

        If NewBO.IsNew Then
            Dim row As DataRow
            If dv Is Nothing Then
                Dim guidTemp As New Guid
                blnEmptyTbl = True
                dt = New DataTable
                dt.Columns.Add(InvRateSearchDV.COL_AFA_INVOICE_RATE_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(InvRateSearchDV.COL_AFA_PRODUCT_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(InvRateSearchDV.COL_INSURANCE_CODE, GetType(String))
                dt.Columns.Add(InvRateSearchDV.COL_REGULATORY_STATE, GetType(String))
                dt.Columns.Add(InvRateSearchDV.COL_TIER, GetType(String))
                dt.Columns.Add(InvRateSearchDV.COL_LOSS_TYPE, GetType(Long))
                dt.Columns.Add(InvRateSearchDV.COL_RETAIL_AMT, GetType(Long))
                dt.Columns.Add(InvRateSearchDV.COL_PREMIUM_AMT, GetType(Long))
                dt.Columns.Add(InvRateSearchDV.COL_COMM_AMT, GetType(String))
                dt.Columns.Add(InvRateSearchDV.COL_ADMIN_AMT, GetType(String))
                dt.Columns.Add(InvRateSearchDV.COL_ANCILLARY_AMT, GetType(String))
                dt.Columns.Add(InvRateSearchDV.COL_OTHER_AMT, GetType(String))
                dt.Columns.Add(InvRateSearchDV.COL_EFFECTIVE_DATE, GetType(String))
                dt.Columns.Add(InvRateSearchDV.COL_EXPIRATION_DATE, GetType(String))
            Else
                dt = dv.Table
            End If
            row = dt.NewRow
            row(InvRateSearchDV.COL_AFA_INVOICE_RATE_ID) = NewBO.Id.ToByteArray
            row(InvRateSearchDV.COL_AFA_PRODUCT_ID) = NewBO.AfaProductId.ToByteArray
            row(InvRateSearchDV.COL_INSURANCE_CODE) = NewBO.InsuranceCode
            row(InvRateSearchDV.COL_TIER) = NewBO.Tier

            If Not NewBO.RegulatoryState Is Nothing Then
                row(InvRateSearchDV.COL_REGULATORY_STATE) = NewBO.RegulatoryState
            End If

            If Not NewBO.LossType Is Nothing Then
                row(InvRateSearchDV.COL_LOSS_TYPE) = NewBO.LossType
            End If
            If Not NewBO.RetailAmt Is Nothing Then
                row(InvRateSearchDV.COL_RETAIL_AMT) = NewBO.RetailAmt.Value
            End If
            If Not NewBO.PremiumAmt Is Nothing Then
                row(InvRateSearchDV.COL_RETAIL_AMT) = NewBO.PremiumAmt.Value
            End If
            If Not NewBO.CommAmt Is Nothing Then
                row(InvRateSearchDV.COL_RETAIL_AMT) = NewBO.CommAmt.Value
            End If
            If Not NewBO.AdminAmt Is Nothing Then
                row(InvRateSearchDV.COL_RETAIL_AMT) = NewBO.AdminAmt.Value
            End If
            If Not NewBO.AncillaryAmt Is Nothing Then
                row(InvRateSearchDV.COL_RETAIL_AMT) = NewBO.AncillaryAmt.Value
            End If
            If Not NewBO.OtherAmt Is Nothing Then
                row(InvRateSearchDV.COL_RETAIL_AMT) = NewBO.OtherAmt.Value
            End If
            If Not NewBO.Effective Is Nothing Then
                row(InvRateSearchDV.COL_RETAIL_AMT) = NewBO.Effective.Value
            End If
            If Not NewBO.Expiration Is Nothing Then
                row(InvRateSearchDV.COL_RETAIL_AMT) = NewBO.Expiration.Value
            End If

            dt.Rows.Add(row)
            If blnEmptyTbl Then dv = New InvRateSearchDV(dt)
        End If
    End Sub


#End Region

#Region "Dummy Properties Needed for Iexpirable Interface"
    Public Function Accept(ByRef visitor As IVisitor) As Boolean Implements IElement.Accept

    End Function

    Public Property Code As String Implements IExpirable.Code
        Get

        End Get
        Set

        End Set
    End Property

    Public Property parent_id As System.Guid Implements IExpirable.parent_id
        Get

        End Get
        Set

        End Set
    End Property

    Public ReadOnly Property IsNew As Boolean Implements IElement.IsNew
        Get
            Return MyBase.IsNew
        End Get
    End Property

#End Region

    Public ReadOnly Property OriginalEffectiveDate As DateType
        Get
            Return New DateType(CType(Row(AFAInvoiceRateDAL.COL_NAME_EFFECTIVE_DATE, DataRowVersion.Original), Date))
        End Get
    End Property

    Public ReadOnly Property OriginalExpirationDate As DateType
        Get
            Return New DateType(CType(Row(AFAInvoiceRateDAL.COL_NAME_EXPIRATION_DATE, DataRowVersion.Original), Date))
        End Get
    End Property

    Public ReadOnly Property OriginalLossType As String
        Get
            If Row(AFAInvoiceRateDAL.COL_NAME_LOSS_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AFAInvoiceRateDAL.COL_NAME_LOSS_TYPE, DataRowVersion.Original), String)
            End If
        End Get
    End Property

    Public ReadOnly Property OriginalTier As String
        Get
            If Row(AFAInvoiceRateDAL.COL_NAME_TIER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AFAInvoiceRateDAL.COL_NAME_TIER, DataRowVersion.Original), String)
            End If
        End Get
    End Property

    Public ReadOnly Property OriginalInsuranceCode As String
        Get
            If Row(AFAInvoiceRateDAL.COL_NAME_INSURANCE_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AFAInvoiceRateDAL.COL_NAME_INSURANCE_CODE, DataRowVersion.Original), String)
            End If
        End Get
    End Property


#Region "Custom Validations"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class IsRateDefinitionUnique
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.RATE_DEFINITION_NOT_UNIQUE)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As AfaInvoiceRate = CType(objectToValidate, AfaInvoiceRate)
            Dim dal As New AFAInvoiceRateDAL

            If obj.Effective Is Nothing OrElse obj.Expiration Is Nothing Then
                Return False
            End If

            If (Not obj.AfaProductId = Guid.Empty) AndAlso (obj.LossType <> String.Empty) Then

                If Not dal.IsRateDefinitionUnique(obj.AfaProductId, obj.LossType, obj.InsuranceCode, obj.RegulatoryState,
                                                  obj.Tier, obj.Effective, obj.Expiration, obj.Id) Then
                    Return False
                End If

            End If

            Return True

        End Function
    End Class


    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class RejectOverlapsOrGaps
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_GAP_OR_OVERLAP_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As AfaInvoiceRate = CType(objectToValidate, AfaInvoiceRate)
            If obj.AfaProductId.Equals(Guid.Empty) Then Return True
            If obj.Effective Is Nothing OrElse obj.Expiration Is Nothing Then Return True

            If obj.Effective Is Nothing OrElse obj.Expiration Is Nothing Then
                Return False
            End If

            If obj.Effective.Value >= obj.Expiration.Value Then
                Return False
            End If

            Dim minMax As New MinEffDateMaxExpDate(obj)
            'Dim objDealer As New Dealer((New AfAProduct(obj.AfaProductId)).DealerId)
            If obj.IsNew Then
                If minMax.MaxExpiration = Nothing Then
                    Return True
                ElseIf obj.Effective.Value = minMax.MaxExpiration.AddDays(1) Then
                    Return True
                Else
                    Return False
                End If
            Else 'Dirty
                If minMax.HasGap Then
                    Return False
                End If
                If minMax.HasOverlap Then
                    Return False
                End If
                If minMax.IsLast AndAlso minMax.IsFirst Then
                    Return True
                End If
                If minMax.IsLast AndAlso (obj.Effective.Value = obj.OriginalEffectiveDate.Value) Then
                    Return True
                ElseIf minMax.IsFirst AndAlso (obj.Expiration.Value = obj.OriginalExpirationDate.Value) Then
                    Return True
                ElseIf (obj.Effective.Value = obj.OriginalEffectiveDate.Value) AndAlso
                       (obj.Expiration.Value = obj.OriginalExpirationDate.Value) Then
                    Return True
                    'ElseIf objDealer.Dealer <> "TPHP" AndAlso objDealer.Dealer <> "TMHP" AndAlso _
                    '       (obj.Effective.Value = obj.OriginalEffectiveDate.Value) AndAlso _
                    '       (obj.Expiration.Value = obj.OriginalExpirationDate.Value) AndAlso _
                    '       (obj.LossType = obj.OriginalLossType) AndAlso _
                    '       (obj.InsuranceCode = obj.OriginalInsuranceCode) Then
                    '    Return True
                End If
            End If

            Return False

        End Function
    End Class

    Public Overrides Sub Delete()
        Try
            CheckDeleted()
            If Not IsNew Then
                Dim minMax As New MinEffDateMaxExpDate(Me)
                If Not minMax.IsLast Then
                    Dim err As New ValidationError(Common.ErrorCodes.INVALID_GAP_OR_OVERLAP_ERR, [GetType], GetType(RejectOverlapsOrGaps), "Effective", Effective)
                    Throw New BOValidationException(New ValidationError() {err}, [GetType].Name, UniqueId)
                End If
            End If
            MyBase.Delete()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub


    Public Class MinEffDateMaxExpDate
        Public MinEffective As Date
        Public MaxExpiration As Date
        Public IsFirst As Boolean = True
        Public IsLast As Boolean = True
        Public HasGap As Boolean = False
        Public HasOverlap As Boolean = False

        Public Sub New(ByVal obj As AfaInvoiceRate)
            Dim tempEffective As Date
            Dim tempExpiration As Date
            Try
                Dim dal As New AFAInvoiceRateDAL
                Dim minMaxDs As DataSet = dal.LoadMinEffectiveMaxExpiration(obj.AfaProductId, obj.InsuranceCode, obj.LossType, obj.Tier, obj.RegulatoryState)
                If minMaxDs.Tables(0).Rows.Count > 0 AndAlso (Not minMaxDs.Tables(0).Rows(0)(dal.COL_MIN_EFECTIVE) Is DBNull.Value) AndAlso (Not minMaxDs.Tables(0).Rows(0)(dal.COL_MAX_EXPIRATION) Is DBNull.Value) Then
                    MinEffective = CType(minMaxDs.Tables(0).Rows(0)(dal.COL_MIN_EFECTIVE), Date)
                    MaxExpiration = CType(minMaxDs.Tables(0).Rows(0)(dal.COL_MAX_EXPIRATION), Date)

                    IsFirst = Not obj.IsNew AndAlso (obj.OriginalEffectiveDate.Value = MinEffective)
                    IsLast = Not obj.IsNew AndAlso (obj.OriginalExpirationDate.Value = MaxExpiration)

                End If

                Dim OtherRatesDs As DataSet = dal.LoadRatesWithSameDefinition(obj.AfaProductId, obj.Id, obj.InsuranceCode, obj.LossType, obj.Tier, obj.RegulatoryState)
                If OtherRatesDs.Tables(0).Rows.Count > 0 Then 'Not obj.IsNew AndAlso
                    'Determine Overlap when :
                    '1. The current record has expiration less than or equall to any of the other records with Same Rate Definition 
                    For Each otherRow As DataRow In OtherRatesDs.Tables(0).Rows
                        tempEffective = CType(otherRow(dal.COL_MIN_EFECTIVE), Date)
                        tempExpiration = CType(otherRow(dal.COL_MAX_EXPIRATION), Date)
                        If obj.Expiration.Value > tempEffective AndAlso obj.Expiration.Value <= tempExpiration Then
                            HasOverlap = True
                            Exit For
                        End If
                    Next
                    'Determine Overlap when :
                    '2. Given that the current record has expiration greater than all other expiration dates,  
                    '   check if the Current record has effective date less than any of the expiration dates of other records
                    If HasOverlap = False Then
                        For Each otherRow As DataRow In OtherRatesDs.Tables(0).Rows
                            tempEffective = CType(otherRow(dal.COL_MIN_EFECTIVE), Date)
                            tempExpiration = CType(otherRow(dal.COL_MAX_EXPIRATION), Date)
                            If obj.Expiration.Value > tempEffective AndAlso obj.Effective.Value <= tempExpiration Then
                                HasOverlap = True
                                Exit For
                            End If
                        Next
                    End If

                    'Determine GAP when :
                    '-------------------------------------------------------
                    '1. If current record has expiration greater than other record and 
                    '   current effective is greater than the other record's expiration by more than a day 
                    '2. If current record has expiration lesser than other record and 
                    '   current expiration is lesser than the other records effective by more than a day
                    If HasOverlap = False Then
                        For Each otherRow As DataRow In OtherRatesDs.Tables(0).Rows
                            tempEffective = CType(otherRow(dal.COL_MIN_EFECTIVE), Date)
                            tempExpiration = CType(otherRow(dal.COL_MAX_EXPIRATION), Date)
                            If obj.Expiration.Value > tempExpiration AndAlso obj.Effective.Value > tempExpiration.AddDays(1) Then
                                HasGap = True
                                Exit For
                            End If
                            If obj.Expiration.Value < tempExpiration AndAlso tempEffective > obj.Expiration.Value.AddDays(1) Then
                                HasGap = True
                                Exit For
                            End If
                        Next
                    End If
                End If

            Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End Sub
    End Class



#End Region

End Class