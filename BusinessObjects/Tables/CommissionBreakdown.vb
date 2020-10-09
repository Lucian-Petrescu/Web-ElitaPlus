'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (11/22/2004)  ********************

Public Class CommissionBreakdown
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
            Dim dal As New CommissionBreakdownDAL
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
            Dim dal As New CommissionBreakdownDAL
            If _isDSCreator Then
                If Row IsNot Nothing Then
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


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid
        Get
            If row(CommissionBreakdownDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CommissionBreakdownDAL.COL_NAME_COMMISSION_BREAKDOWN_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CommissionPeriodId As Guid
        Get
            CheckDeleted()
            If row(CommissionBreakdownDAL.COL_NAME_COMMISSION_PERIOD_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CommissionBreakdownDAL.COL_NAME_COMMISSION_PERIOD_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommissionBreakdownDAL.COL_NAME_COMMISSION_PERIOD_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("", Max:=100, Min:=0)> _
    Public Property AllowedMarkupPct As DecimalType
        Get
            CheckDeleted()
            If Row(CommissionBreakdownDAL.COL_NAME_ALLOWED_MARKUP_PCT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CommissionBreakdownDAL.COL_NAME_ALLOWED_MARKUP_PCT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommissionBreakdownDAL.COL_NAME_ALLOWED_MARKUP_PCT, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property Tolerance As DecimalType
        Get
            CheckDeleted()
            If Row(CommissionBreakdownDAL.COL_NAME_TOLERANCE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CommissionBreakdownDAL.COL_NAME_TOLERANCE), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommissionBreakdownDAL.COL_NAME_TOLERANCE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("", Max:=100, Min:=0), ValidMarkup("")> _
    Public Property DealerMarkupPct As DecimalType
        Get
            CheckDeleted()
            If Row(CommissionBreakdownDAL.COL_NAME_DEALER_MARKUP_PCT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CommissionBreakdownDAL.COL_NAME_DEALER_MARKUP_PCT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommissionBreakdownDAL.COL_NAME_DEALER_MARKUP_PCT, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("", Max:=100, Min:=0), ValidComm("")> _
    Public Property DealerCommPct As DecimalType
        Get
            CheckDeleted()
            If Row(CommissionBreakdownDAL.COL_NAME_DEALER_COMM_PCT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CommissionBreakdownDAL.COL_NAME_DEALER_COMM_PCT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommissionBreakdownDAL.COL_NAME_DEALER_COMM_PCT, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("", Max:=100, Min:=0)> _
    Public Property BrokerMarkupPct As DecimalType
        Get
            CheckDeleted()
            If Row(CommissionBreakdownDAL.COL_NAME_BROKER_MARKUP_PCT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CommissionBreakdownDAL.COL_NAME_BROKER_MARKUP_PCT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommissionBreakdownDAL.COL_NAME_BROKER_MARKUP_PCT, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("", Max:=100, Min:=0)> _
    Public Property BrokerCommPct As DecimalType
        Get
            CheckDeleted()
            If Row(CommissionBreakdownDAL.COL_NAME_BROKER_COMM_PCT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CommissionBreakdownDAL.COL_NAME_BROKER_COMM_PCT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommissionBreakdownDAL.COL_NAME_BROKER_COMM_PCT, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("", Max:=100, Min:=0)> _
    Public Property Broker2MarkupPct As DecimalType
        Get
            CheckDeleted()
            If Row(CommissionBreakdownDAL.COL_NAME_BROKER2_MARKUP_PCT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CommissionBreakdownDAL.COL_NAME_BROKER2_MARKUP_PCT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommissionBreakdownDAL.COL_NAME_BROKER2_MARKUP_PCT, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("", Max:=100, Min:=0)> _
    Public Property Broker2CommPct As DecimalType
        Get
            CheckDeleted()
            If Row(CommissionBreakdownDAL.COL_NAME_BROKER2_COMM_PCT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CommissionBreakdownDAL.COL_NAME_BROKER2_COMM_PCT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommissionBreakdownDAL.COL_NAME_BROKER2_COMM_PCT, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("", Max:=100, Min:=0)> _
    Public Property Broker3MarkupPct As DecimalType
        Get
            CheckDeleted()
            If Row(CommissionBreakdownDAL.COL_NAME_BROKER3_MARKUP_PCT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CommissionBreakdownDAL.COL_NAME_BROKER3_MARKUP_PCT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommissionBreakdownDAL.COL_NAME_BROKER3_MARKUP_PCT, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("", Max:=100, Min:=0)> _
    Public Property Broker3CommPct As DecimalType
        Get
            CheckDeleted()
            If Row(CommissionBreakdownDAL.COL_NAME_BROKER3_COMM_PCT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CommissionBreakdownDAL.COL_NAME_BROKER3_COMM_PCT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommissionBreakdownDAL.COL_NAME_BROKER3_COMM_PCT, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("", Max:=100, Min:=0)> _
    Public Property Broker4MarkupPct As DecimalType
        Get
            CheckDeleted()
            If Row(CommissionBreakdownDAL.COL_NAME_BROKER4_MARKUP_PCT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CommissionBreakdownDAL.COL_NAME_BROKER4_MARKUP_PCT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommissionBreakdownDAL.COL_NAME_BROKER4_MARKUP_PCT, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("", Max:=100, Min:=0)> _
    Public Property Broker4CommPct As DecimalType
        Get
            CheckDeleted()
            If Row(CommissionBreakdownDAL.COL_NAME_BROKER4_COMM_PCT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CommissionBreakdownDAL.COL_NAME_BROKER4_COMM_PCT), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommissionBreakdownDAL.COL_NAME_BROKER4_COMM_PCT, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New CommissionBreakdownDAL
                dal.Update(Row)
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Id
                    Dataset = New Dataset
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

    Public Shared Function LoadList(oData As Object) As DataView
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

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_COMM_BREAK_MARKUP_ERR)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim bIsOk As Boolean = True
            Dim oBreakdown As CommissionBreakdown = CType(objectToValidate, CommissionBreakdown)
            Dim nSumm As Double

            If (DisplayName = "AllowedMarkupPct") AndAlso _
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

        Private Function GetRestrictMarkup(oBreakdown As CommissionBreakdown) As Boolean
            Dim oPeriodData As New CommissionPeriodData
            Dim oPeriod As New CommissionPeriod(oBreakdown.CommissionPeriodId)

            oPeriodData.dealerId = oPeriod.DealerId
            Return CommissionPeriod.GetRestrictMarkup(oPeriodData)
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidComm
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_COMM_BREAK_COMM_ERR)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
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



