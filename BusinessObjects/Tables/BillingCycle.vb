'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (2/10/2012)  ********************

Public Class BillingCycle
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
            Dim dal As New BillingCycleDAL
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
            Dim dal As New BillingCycleDAL
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
            If Row(BillingCycleDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(BillingCycleDAL.COL_NAME_BILLING_CYCLE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property DealerId() As Guid
        Get
            CheckDeleted()
            If Row(BillingCycleDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(BillingCycleDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(BillingCycleDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property

    Public ReadOnly Property Dealer() As String
        Get
            CheckDeleted()
            If Row(BillingCycleDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return String.Empty
            Else
                Return LookupListNew.GetCodeFromId(LookupListNew.LK_DEALERS, New Guid(CType(Row(BillingCycleDAL.COL_NAME_DEALER_ID), Byte())))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=255), CheckDuplicate("")> _
    Public Property BillingCycleCode() As String
        Get
            CheckDeleted()
            If Row(BillingCycleDAL.COL_NAME_BILLING_CYCLE_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(BillingCycleDAL.COL_NAME_BILLING_CYCLE_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(BillingCycleDAL.COL_NAME_BILLING_CYCLE_CODE, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidNumericRange("", Max:=31, Min:=1)> _
    Public Property StartDay() As LongType
        Get
            CheckDeleted()
            If Row(BillingCycleDAL.COL_NAME_START_DAY) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(BillingCycleDAL.COL_NAME_START_DAY), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(BillingCycleDAL.COL_NAME_START_DAY, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidNumericRange("", Max:=31, Min:=1)> _
    Public Property EndDay() As LongType
        Get
            CheckDeleted()
            If Row(BillingCycleDAL.COL_NAME_END_DAY) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(BillingCycleDAL.COL_NAME_END_DAY), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(BillingCycleDAL.COL_NAME_END_DAY, Value)
        End Set
    End Property

    <ValidNumericRange("", Max:=99, Min:=-99)> _
     Public Property BillingRunDateOffsetDays() As LongType
        Get
            CheckDeleted()
            If Row(BillingCycleDAL.COL_NAME_BILLING_RUN_DATE_OFFSET_DAYS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(BillingCycleDAL.COL_NAME_BILLING_RUN_DATE_OFFSET_DAYS), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(BillingCycleDAL.COL_NAME_BILLING_RUN_DATE_OFFSET_DAYS, Value)
        End Set
    End Property

    Public Property DateOfPaymentOptionId() As Guid
        Get
            CheckDeleted()
            If Row(BillingCycleDAL.COL_NAME_DATE_OF_PAYMENT_OPTION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(BillingCycleDAL.COL_NAME_DATE_OF_PAYMENT_OPTION_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(BillingCycleDAL.COL_NAME_DATE_OF_PAYMENT_OPTION_ID, Value)
        End Set
    End Property

    <ValidNumericRange("", Max:=99, Min:=-99)>
    Public Property DateOfPaymentOffsetDays() As LongType
        Get
            CheckDeleted()
            If Row(BillingCycleDAL.COL_NAME_DATE_OF_PAYMENT_OFFSET_DAYS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(BillingCycleDAL.COL_NAME_DATE_OF_PAYMENT_OFFSET_DAYS), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(BillingCycleDAL.COL_NAME_DATE_OF_PAYMENT_OFFSET_DAYS, Value)
        End Set
    End Property

    Public Property NumberOfDigitsRoundOffId() As Guid
        Get
            CheckDeleted()
            If Row(BillingCycleDAL.COL_NAME_NUMBER_OF_DIGITS_ROUNDOFF_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(BillingCycleDAL.COL_NAME_NUMBER_OF_DIGITS_ROUNDOFF_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(BillingCycleDAL.COL_NAME_NUMBER_OF_DIGITS_ROUNDOFF_ID, Value)
        End Set
    End Property

    <ValidNumericRange("", Max:=99, Min:=-99)>
    Public Property BillingCoolOffDays() As LongType
        Get
            CheckDeleted()
            If row(BillingCycleDAL.COL_NAME_BILLING_COOL_OFF_DAYS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(BillingCycleDAL.COL_NAME_BILLING_COOL_OFF_DAYS), Long))
            End If
        End Get
        Set(ByVal value As LongType)
            CheckDeleted()
            SetValue(BillingCycleDAL.COL_NAME_BILLING_COOL_OFF_DAYS, value)
        End Set
    End Property
#End Region

#Region "Public Members"
    Public Shared Sub DeleteBillingCycle(ByVal billingCycleId As Guid)
        Dim dal As New BillingCycleDAL
        dal.Delete(billingCycleId)
    End Sub

    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New BillingCycleDAL
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

    Public Overrides ReadOnly Property IsDirty() As Boolean
        Get
            Dim bDirty As Boolean

            bDirty = MyBase.IsDirty OrElse Me.IsChildrenDirty

            Return bDirty
        End Get
    End Property

    Public Sub Copy(ByVal original As BillingCycle)
        If Not Me.IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Billing Cycle")
        End If
        'Copy myself
        Me.CopyFrom(original)
    End Sub
#End Region

#Region "Private Members"

    Private Function CheckDuplicateBillingCycleCode() As Boolean
        Dim dal As New BillingCycleDAL
        Dim companyIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
        Dim companyGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
        Dim dv As BillingCycle.BillingCycleSearchDV = GetList(Me.DealerId, Me.BillingCycleCode)

        For Each dr As DataRow In dv.Table.Rows
            If (dr(BillingCycleDAL.COL_NAME_BILLING_CYCLE_CODE).ToString().ToUpper() = Me.BillingCycleCode.ToUpper()) Then
                If (Not New Guid(CType(dr(BillingCycleDAL.COL_NAME_BILLING_CYCLE_ID), Byte())).Equals(Me.Id)) Then
                    Return True
                End If
            End If
        Next
        Return False
    End Function
#End Region

#Region "DataView Retrieveing Methods"
    Public Class BillingCycleSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_NAME_BILLING_CYCLE_ID As String = BillingCycleDAL.COL_NAME_BILLING_CYCLE_ID
        Public Const COL_NAME_DEALER_NAME As String = BillingCycleDAL.COL_NAME_DEALER_NAME
        Public Const COL_NAME_BILLING_CYCLE_CODE As String = BillingCycleDAL.COL_NAME_BILLING_CYCLE_CODE
        Public Const COL_NAME_START_DAY As String = BillingCycleDAL.COL_NAME_START_DAY
        Public Const COL_NAME_END_DAY As String = BillingCycleDAL.COL_NAME_END_DAY
        Public Const COL_NAME_BILLING_RUN_DATE_OFFSET_DAYS As String = BillingCycleDAL.COL_NAME_BILLING_RUN_DATE_OFFSET_DAYS
#End Region

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property BillingCycleId(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_BILLING_CYCLE_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property Dealer(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_DEALER_NAME).ToString
            End Get
        End Property

        Public Shared ReadOnly Property BillingCycleCode(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_BILLING_CYCLE_CODE).ToString
            End Get
        End Property

        Public Shared ReadOnly Property StartDay(ByVal row) As String
            Get
                Return row(COL_NAME_START_DAY).ToString
            End Get
        End Property

        Public Shared ReadOnly Property EndDay(ByVal row) As String
            Get
                Return row(COL_NAME_END_DAY).ToString
            End Get
        End Property
    End Class

    Shared Function GetList(ByVal dealerId As Guid, ByVal billingCycleCode As String) As BillingCycleSearchDV
        Try
            Dim dal As New BillingCycleDAL
            Dim companyIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Dim companyGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            Return New BillingCycleSearchDV(dal.LoadList(dealerId, billingCycleCode, companyIds, companyGroupId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetNewDataViewRow(ByVal dv As BillingCycleSearchDV, ByVal bo As BillingCycle) As BillingCycleSearchDV

        Dim dt As DataTable
        dt = dv.Table

        If bo.IsNew Then
            Dim row As DataRow = dt.NewRow

            row(BillingCycleSearchDV.COL_NAME_BILLING_CYCLE_CODE) = bo.BillingCycleCode
            row(BillingCycleSearchDV.COL_NAME_BILLING_CYCLE_ID) = bo.Id.ToByteArray
            row(BillingCycleSearchDV.COL_NAME_DEALER_NAME) = bo.Dealer
            If (bo.StartDay Is Nothing) Then
                row(BillingCycleSearchDV.COL_NAME_START_DAY) = DBNull.Value
            Else
                row(BillingCycleSearchDV.COL_NAME_START_DAY) = bo.StartDay
            End If
            If (bo.EndDay Is Nothing) Then
                row(BillingCycleSearchDV.COL_NAME_END_DAY) = DBNull.Value
            Else
                row(BillingCycleSearchDV.COL_NAME_END_DAY) = bo.EndDay
            End If
            dt.Rows.Add(row)
        End If

        Return (dv)

    End Function
#End Region

#Region "Custom Validators"
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class CheckDuplicate
        Inherits ValidBaseAttribute
        Private Const DUPLICATE_BILLING_CYCLE_CODE As String = "DUPLICATE_BILLING_CYCLE_CODE"

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, DUPLICATE_BILLING_CYCLE_CODE)
        End Sub

        Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As BillingCycle = CType(objectToValidate, BillingCycle)
            If (obj.CheckDuplicateBillingCycleCode()) Then
                Return False
            Else
                Return True
            End If
        End Function
    End Class
#End Region

End Class


