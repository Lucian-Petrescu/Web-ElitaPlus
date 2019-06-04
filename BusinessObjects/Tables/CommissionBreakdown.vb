'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (11/22/2004)  ********************

Public Class CommissionBreakdown
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
            Dim dal As New CommissionBreakdownDAL
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
            Dim dal As New CommissionBreakdownDAL
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
            If row(CommissionBreakdownDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CommissionBreakdownDAL.COL_NAME_COMMISSION_BREAKDOWN_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CommissionPeriodId() As Guid
        Get
            CheckDeleted()
            If row(CommissionBreakdownDAL.COL_NAME_COMMISSION_PERIOD_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CommissionBreakdownDAL.COL_NAME_COMMISSION_PERIOD_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CommissionBreakdownDAL.COL_NAME_COMMISSION_PERIOD_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("", Max:=100, Min:=0)> _
    Public Property AllowedMarkupPct() As DecimalType
        Get
            CheckDeleted()
            If Row(CommissionBreakdownDAL.COL_NAME_ALLOWED_MARKUP_PCT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CommissionBreakdownDAL.COL_NAME_ALLOWED_MARKUP_PCT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CommissionBreakdownDAL.COL_NAME_ALLOWED_MARKUP_PCT, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property Tolerance() As DecimalType
        Get
            CheckDeleted()
            If Row(CommissionBreakdownDAL.COL_NAME_TOLERANCE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CommissionBreakdownDAL.COL_NAME_TOLERANCE), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CommissionBreakdownDAL.COL_NAME_TOLERANCE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("", Max:=100, Min:=0), ValidMarkup("")> _
    Public Property DealerMarkupPct() As DecimalType
        Get
            CheckDeleted()
            If Row(CommissionBreakdownDAL.COL_NAME_DEALER_MARKUP_PCT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CommissionBreakdownDAL.COL_NAME_DEALER_MARKUP_PCT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CommissionBreakdownDAL.COL_NAME_DEALER_MARKUP_PCT, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("", Max:=100, Min:=0), ValidComm("")> _
    Public Property DealerCommPct() As DecimalType
        Get
            CheckDeleted()
            If Row(CommissionBreakdownDAL.COL_NAME_DEALER_COMM_PCT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CommissionBreakdownDAL.COL_NAME_DEALER_COMM_PCT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CommissionBreakdownDAL.COL_NAME_DEALER_COMM_PCT, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("", Max:=100, Min:=0)> _
    Public Property BrokerMarkupPct() As DecimalType
        Get
            CheckDeleted()
            If Row(CommissionBreakdownDAL.COL_NAME_BROKER_MARKUP_PCT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CommissionBreakdownDAL.COL_NAME_BROKER_MARKUP_PCT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CommissionBreakdownDAL.COL_NAME_BROKER_MARKUP_PCT, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("", Max:=100, Min:=0)> _
    Public Property BrokerCommPct() As DecimalType
        Get
            CheckDeleted()
            If Row(CommissionBreakdownDAL.COL_NAME_BROKER_COMM_PCT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CommissionBreakdownDAL.COL_NAME_BROKER_COMM_PCT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CommissionBreakdownDAL.COL_NAME_BROKER_COMM_PCT, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("", Max:=100, Min:=0)> _
    Public Property Broker2MarkupPct() As DecimalType
        Get
            CheckDeleted()
            If Row(CommissionBreakdownDAL.COL_NAME_BROKER2_MARKUP_PCT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CommissionBreakdownDAL.COL_NAME_BROKER2_MARKUP_PCT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CommissionBreakdownDAL.COL_NAME_BROKER2_MARKUP_PCT, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("", Max:=100, Min:=0)> _
    Public Property Broker2CommPct() As DecimalType
        Get
            CheckDeleted()
            If Row(CommissionBreakdownDAL.COL_NAME_BROKER2_COMM_PCT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CommissionBreakdownDAL.COL_NAME_BROKER2_COMM_PCT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CommissionBreakdownDAL.COL_NAME_BROKER2_COMM_PCT, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("", Max:=100, Min:=0)> _
    Public Property Broker3MarkupPct() As DecimalType
        Get
            CheckDeleted()
            If Row(CommissionBreakdownDAL.COL_NAME_BROKER3_MARKUP_PCT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CommissionBreakdownDAL.COL_NAME_BROKER3_MARKUP_PCT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CommissionBreakdownDAL.COL_NAME_BROKER3_MARKUP_PCT, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("", Max:=100, Min:=0)> _
    Public Property Broker3CommPct() As DecimalType
        Get
            CheckDeleted()
            If Row(CommissionBreakdownDAL.COL_NAME_BROKER3_COMM_PCT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CommissionBreakdownDAL.COL_NAME_BROKER3_COMM_PCT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CommissionBreakdownDAL.COL_NAME_BROKER3_COMM_PCT, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("", Max:=100, Min:=0)> _
    Public Property Broker4MarkupPct() As DecimalType
        Get
            CheckDeleted()
            If Row(CommissionBreakdownDAL.COL_NAME_BROKER4_MARKUP_PCT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CommissionBreakdownDAL.COL_NAME_BROKER4_MARKUP_PCT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CommissionBreakdownDAL.COL_NAME_BROKER4_MARKUP_PCT, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("", Max:=100, Min:=0)> _
    Public Property Broker4CommPct() As DecimalType
        Get
            CheckDeleted()
            If Row(CommissionBreakdownDAL.COL_NAME_BROKER4_COMM_PCT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CommissionBreakdownDAL.COL_NAME_BROKER4_COMM_PCT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CommissionBreakdownDAL.COL_NAME_BROKER4_COMM_PCT, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New CommissionBreakdownDAL
                dal.Update(Me.Row)
                'Reload the Data from the DB
                If Me.Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Me.Id
                    Me.Dataset = New Dataset
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

    Public Shared Function LoadList(ByVal oData As Object) As DataView
        Try
            Dim oCommissionBreakdownData As CommissionBreakdownData = CType(oData, CommissionBreakdownData)
            Dim dal As New CommissionBreakdownDAL
            Dim ds As Dataset

            ds = dal.LoadList(oCommissionBreakdownData)
            Return (ds.Tables(CommissionBreakdownDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

#End Region

#Region "Custom Validation"

    

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidMarkup
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_COMM_BREAK_MARKUP_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim bIsOk As Boolean = True
            Dim oBreakdown As CommissionBreakdown = CType(objectToValidate, CommissionBreakdown)
            Dim nSumm As Double

            If (Me.DisplayName = "AllowedMarkupPct") AndAlso _
                    (GetRestrictMarkup(oBreakdown) = False) Then Return True
            With oBreakdown
                '.AllowedMarkupPct.Value +
                If ((.DealerMarkupPct Is Nothing) OrElse (.BrokerMarkupPct Is Nothing) _
                    OrElse (.Broker2MarkupPct Is Nothing) OrElse (.Broker3MarkupPct Is Nothing) _
                    OrElse (.Broker4MarkupPct Is Nothing)) Then
                    bIsOk = False
                Else
                    nSumm = .DealerMarkupPct.Value + .BrokerMarkupPct.Value + _
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

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidComm
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_COMM_BREAK_COMM_ERR)
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
                    nSumm = .DealerCommPct.Value + .BrokerCommPct.Value + _
                            .Broker2CommPct.Value + .Broker3CommPct.Value + .Broker4CommPct.Value
                    If nSumm > 75 Then
                        bIsOk = False
                    End If
                End If
            End With

            Return bIsOk

        End Function
    End Class
#End Region
End Class



