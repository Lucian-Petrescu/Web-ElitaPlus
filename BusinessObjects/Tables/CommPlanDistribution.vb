Public Class CommPlanDistribution
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
            Dim dal As New CommPlanDistributionDAL
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
            Dim dal As New CommPlanDistributionDAL
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
            If Row(CommPlanDistributionDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CommPlanDistributionDAL.COL_NAME_COMM_PLAN_DIST_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")>
    Public Property CommissionPlanId() As Guid
        Get
            CheckDeleted()
            If Row(CommPlanDistributionDAL.COL_NAME_COMM_PLAN_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CommPlanDistributionDAL.COL_NAME_COMM_PLAN_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CommPlanDistributionDAL.COL_NAME_COMM_PLAN_ID, Value)
        End Set
    End Property
    
    <ValueMandatory("")>
    Public Property EntityId() As Guid
        Get
            CheckDeleted()
            If Row(CommPlanDistributionDAL.COL_NAME_ENTITY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CommPlanDistributionDAL.COL_NAME_ENTITY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CommPlanDistributionDAL.COL_NAME_ENTITY_ID, Value)
        End Set
    End Property

    '<ValueMandatory(""), ValidNumericRange("", Max:=100, Min:=0)>
    '<ValueMandatory("")>
    Public Property CommissionAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(CommPlanDistributionDAL.COL_NAME_COMM_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CommPlanDistributionDAL.COL_NAME_COMM_AMOUNT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CommPlanDistributionDAL.COL_NAME_COMM_AMOUNT, Value)
        End Set
    End Property

    '<ValueMandatory(""), ValidNumericRange("", Max:=100, Min:=0)>
    '<ValueMandatory("")>
    Public Property CommissionPercent() As DecimalType
        Get
            CheckDeleted()
            If Row(CommPlanDistributionDAL.COL_NAME_COMMISSION_PERCENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CommPlanDistributionDAL.COL_NAME_COMMISSION_PERCENT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CommPlanDistributionDAL.COL_NAME_COMMISSION_PERCENT, Value)
        End Set
    End Property

    Public Property Position() As LongType
        Get
            CheckDeleted()
            If Row(CommPlanDistributionDAL.COL_NAME_POSITION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(CommPlanDistributionDAL.COL_NAME_POSITION), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(CommPlanDistributionDAL.COL_NAME_POSITION, Value)
        End Set
    End Property

    Dim _MarkupTotal As DecimalType
    '<ValidNumericRange("", Max:=100, Min:=100), ValidMarkup("")>
    '<ValidMarkup("")>
    Public Property MarkupTotal() As DecimalType
        Get
            Return _MarkupTotal
        End Get
        Set(ByVal Value As DecimalType)
            _MarkupTotal = Value
        End Set
    End Property

    Dim _commTotal As DecimalType
    '<ValidNumericRange("", Max:=100, Min:=100), ValidComm("")>
    '<ValidComm("")>
    Public Property CommTotal() As DecimalType
        Get
            Return _commTotal
        End Get
        Set(ByVal Value As DecimalType)
            _commTotal = Value
        End Set
    End Property
    
    <ValidStringLength("", Max:=35, Message:="CommissionsPercentSourceXcd should be between 1 to 35 chars.")>
    Public Property CommissionsPercentSourceXcd() As String
        Get
            CheckDeleted()
            If Row(CommPlanDistributionDAL.COL_NAME_COMMISSION_SOURCE_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CommPlanDistributionDAL.COL_NAME_COMMISSION_SOURCE_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CommPlanDistributionDAL.COL_NAME_COMMISSION_SOURCE_XCD, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=35, Message:="PayeeTypeXcd should be between 1 to 35 chars.")>
    Public Property PayeeTypeXcd() As String
        Get
            CheckDeleted()
            If Row(CommPlanDistributionDAL.COL_NAME_PAYEE_TYPE_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CommPlanDistributionDAL.COL_NAME_PAYEE_TYPE_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CommPlanDistributionDAL.COL_NAME_PAYEE_TYPE_XCD, Value)
        End Set
    End Property
#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New CommPlanDistributionDAL
                'dal.Update(Me.Row)
                dal.UpdateFamily(Me.Dataset)
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
    'Public Function HasDealerConfiguredForAcctBucket(ByVal CommissionTolerenceId As Guid) As Boolean

    '    Dim oCommissionTolerence As New CommissionTolerance(CommissionTolerenceId)
    '    Dim oCommissionPeriod As New CommissionPeriod(oCommissionTolerence.CommissionPeriodId)
    '    Dim oDealer As New Dealer(oCommissionPeriod.DealerId)
    '    Dim isDealerConfiguredForAcctBucket As Boolean = False

    '    If (oCommissionPeriod.DealerId <> Guid.Empty) Then
    '        If Not oDealer.AcctBucketsWithSourceXcd Is Nothing Then
    '            If oDealer.AcctBucketsWithSourceXcd.Equals(Codes.EXT_YESNO_Y) Then
    '                isDealerConfiguredForAcctBucket = True
    '            Else
    '                isDealerConfiguredForAcctBucket = False
    '            End If
    '        Else
    '            isDealerConfiguredForAcctBucket = False
    '        End If
    '    Else
    '        isDealerConfiguredForAcctBucket = False
    '    End If

    '    Return isDealerConfiguredForAcctBucket
    'End Function
#Region "DataView Retrieveing Methods"

    Public Shared Function getList(ByVal CommissionToleranceId As Guid) As SearchDV
        Try
            Dim dal As New CommPlanDistributionDAL

            Return New SearchDV(dal.LoadList(CommissionToleranceId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function getPlanList(ByVal commPlanId As Guid) As DataView
        Try
            Dim dal As New CommPlanDistributionDAL

            Return New DataView(dal.LoadListPLan(commPlanId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function


    Private Shared Function GetAssocCommList(ByVal parent As CommissionTolerance, ByVal id As Guid) As DataTable

        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(CommPlanDistList)) Then
                Dim dal As New CommPlanDistributionDAL
                dal.LoadList(id, parent.Dataset)
                parent.AddChildrenCollection(GetType(CommPlanDistList))
            End If
            'Return New CommPlanDistList(parent)
            Return parent.Dataset.Tables(CommPlanDistributionDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region

#Region "SearchDV"

    Public Class SearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_NAME_COMMISSION_PLAN_DIST_ID As String = CommPlanDistributionDAL.COL_NAME_COMM_PLAN_DIST_ID
        Public Const COL_NAME_COMMISSION_PLAN_ID As String = CommPlanDistributionDAL.COL_NAME_COMM_PLAN_ID
        Public Const COL_NAME_PAYEE_TYPE_XCD As String = CommPlanDistributionDAL.COL_NAME_PAYEE_TYPE_XCD
        Public Const COL_NAME_COMM_ENTITY_ID As String = CommPlanDistributionDAL.COL_NAME_ENTITY_ID
        Public Const COL_NAME_COMM_AMT As String = CommPlanDistributionDAL.COL_NAME_COMM_AMOUNT
        Public Const COL_NAME_COMMISSION_PERCENT As String = CommPlanDistributionDAL.COL_NAME_COMMISSION_PERCENT
        Public Const COL_NAME_COMMISSION_SOURCE_XCD As String = CommPlanDistributionDAL.COL_NAME_COMMISSION_SOURCE_XCD
        Public Const COL_NAME_POSITION As String = CommPlanDistributionDAL.COL_NAME_POSITION
#End Region

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property CommissionPLanDistId(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_COMMISSION_PLAN_DIST_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property CommissionPlanId(ByVal row As DataRow) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_COMMISSION_PLAN_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property PayeeTypeXcd(ByVal row As DataRow) As String
            Get
                Return CType(row(COL_NAME_PAYEE_TYPE_XCD), String)
            End Get
        End Property
        Public Shared ReadOnly Property EntityId(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_COMM_ENTITY_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property CommissionAmount(ByVal row As DataRow) As LongType
            Get
                Return CType(row(COL_NAME_COMM_AMT), LongType)
            End Get
        End Property

        Public Shared ReadOnly Property CommissionPercent(ByVal row As DataRow) As LongType
            Get
                Return CType(row(COL_NAME_COMMISSION_PERCENT), LongType)
            End Get
        End Property

        Public Shared ReadOnly Property Comm_Source_Xcd(ByVal row As DataRow) As String
            Get
                Return CType(row(COL_NAME_COMMISSION_SOURCE_XCD), String)
            End Get
        End Property

        Public Shared ReadOnly Property Position(ByVal row As DataRow) As LongType
            Get
                Return CType(row(COL_NAME_POSITION), LongType)
            End Get
        End Property


        'Public ReadOnly Property AssociatedCommPeriodEntity() As CommPlanDistList
        'Get
        '    Return New CommPlanDistList(Me)
        'End Get
        'End Property
    End Class

#End Region
#Region "List Methods"
    Public Class CommPlanDistList
        Inherits BusinessObjectListBase
        Public Sub New(ByVal parent As Object, ByVal id As Guid)
            MyBase.New(GetAssocCommList(parent, id), GetType(CommPlanDistribution), parent)
        End Sub

        'Public Sub New(ByVal parent As CommPlanDistribution)
        '    MyBase.New(GetAssocCommList(parent, parent.Id), GetType(CommPlanDistribution), parent)
        'End Sub

        Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
            Return True
        End Function

        Public Function FindById(ByVal assocCommId As Guid) As CommPlanDistribution
            Dim bo As CommPlanDistribution
            For Each bo In Me
                If bo.Id.Equals(assocCommId) Then Return bo
            Next
            Return Nothing
        End Function

        'Public Function FindByPosition(ByVal position As LongType) As CommDistributionPlan
        '    Dim bo As CommDistributionPlan
        '    For Each bo In Me
        '        If bo.Position.Equals(position) Then Return bo
        '    Next
        '    Return Nothing
        'End Function

#Region "Custom Validation"

        <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
        Public NotInheritable Class ValidMarkup
            Inherits ValidBaseAttribute

            Public Sub New(ByVal fieldDisplayName As String)
                MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_COMM_BREAK_MARKUP_ERR)
            End Sub

            Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
                Dim bIsOk As Boolean = True
                Dim oBreakdown As CommissionBreakdown = CType(objectToValidate, CommissionBreakdown)
                Dim nSumm As Double

                If (Me.DisplayName = "AllowedMarkupPct") AndAlso
                        (GetRestrictMarkup(oBreakdown) = False) Then Return True
                With oBreakdown
                    '.AllowedMarkupPct.Value +
                    If ((.DealerMarkupPct Is Nothing) OrElse (.BrokerMarkupPct Is Nothing) _
                        OrElse (.Broker2MarkupPct Is Nothing) OrElse (.Broker3MarkupPct Is Nothing) _
                        OrElse (.Broker4MarkupPct Is Nothing)) Then
                        bIsOk = False
                    Else
                        nSumm = .DealerMarkupPct.Value + .BrokerMarkupPct.Value +
                                                .Broker2MarkupPct.Value + .Broker3MarkupPct.Value + .Broker4MarkupPct.Value
                        If nSumm <> 100 Then
                            bIsOk = False
                        End If
                    End If

                End With

                Return bIsOk

            End Function

            Private Function GetRestrictMarkup(ByVal oBreakdown As CommissionBreakdown) As Boolean
                Dim oPeriodData As New CommissionPeriodData
                Dim oPeriod As New CommissionPeriod(oBreakdown.CommissionPeriodId)

                oPeriodData.dealerId = oPeriod.DealerId
                Return CommissionPeriod.GetRestrictMarkup(oPeriodData)
            End Function
        End Class

        <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
        Public NotInheritable Class ValidComm
            Inherits ValidBaseAttribute

            Public Sub New(ByVal fieldDisplayName As String)
                MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_COMM_BREAK_COMM_PCT_ERR)
            End Sub

            Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
                Dim bIsOk As Boolean = True
                Dim oBreakdown As CommissionBreakdown = CType(objectToValidate, CommissionBreakdown)
                Dim nSumm As Double

                With oBreakdown
                    If ((.DealerCommPct Is Nothing) OrElse (.BrokerCommPct Is Nothing) _
                        OrElse (.Broker2CommPct Is Nothing) OrElse (.Broker3CommPct Is Nothing) _
                        OrElse (.Broker4CommPct Is Nothing)) Then
                        bIsOk = False
                    Else
                        nSumm = .DealerCommPct.Value + .BrokerCommPct.Value +
                                .Broker2CommPct.Value + .Broker3CommPct.Value + .Broker4CommPct.Value
                        If nSumm > 100 Then
                            bIsOk = False
                        End If
                    End If
                End With

                Return bIsOk

            End Function
        End Class
#End Region
    End Class
#End Region
#Region "Custom Validation"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidMarkup
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_COMM_BREAK_MARKUP_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim bIsOk As Boolean = True
            Dim oAssocComm As CommPlanDistribution = CType(objectToValidate, CommPlanDistribution)
            'If oAssocComm.HasDealerConfiguredForAcctBucket(oAssocComm.CommissionToleranceId) = False Then

                If (Me.DisplayName = "AllowedMarkupPct") AndAlso
                        (GetRestrictMarkup(oAssocComm) = False) Then
                    Return True
                End If

                If oAssocComm.MarkupTotal.Value <> 100 Then
                    bIsOk = False
                End If
            'End If
            Return bIsOk

        End Function

        Private Function GetRestrictMarkup(ByVal oAssocComm As CommPlanDistribution) As Boolean
            Dim oPeriodData As New CommissionPeriodData
            Dim oTolerance As New CommissionTolerance(oAssocComm.CommissionPlanId)
            Dim oPeriod As New CommissionPeriod(oTolerance.CommissionPeriodId)

            oPeriodData.dealerId = oPeriod.DealerId
            Return CommissionPeriod.GetRestrictMarkup(oPeriodData)
        End Function
    End Class


    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidComm
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_COMM_BREAK_COMM_PCT_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim bIsOk As Boolean = True
            Dim oAssocComm As AssociateCommissions = CType(objectToValidate, AssociateCommissions)
            'If oAssocComm.HasDealerConfiguredForAcctBucket(oAssocComm.CommissionToleranceId) = False Then
                If oAssocComm.CommTotal.Value <> 100 Then
                    bIsOk = False
                End If
            'End If
            Return bIsOk

        End Function
    End Class
#End Region

End Class





