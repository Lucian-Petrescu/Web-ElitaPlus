'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (9/10/2007)  ********************

Public Class AssociateCommissions
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
            Dim dal As New AssociateCommissionsDAL
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
            Dim dal As New AssociateCommissionsDAL
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
            If row(AssociateCommissionsDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AssociateCommissionsDAL.COL_NAME_ASSOCIATE_COMMISSIONS_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CommissionToleranceId() As Guid
        Get
            CheckDeleted()
            If row(AssociateCommissionsDAL.COL_NAME_COMMISSION_TOLERANCE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AssociateCommissionsDAL.COL_NAME_COMMISSION_TOLERANCE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(AssociateCommissionsDAL.COL_NAME_COMMISSION_TOLERANCE_ID, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidNumericRange("", Max:=100, Min:=0)> _
    Public Property MarkupPercent() As DecimalType
        Get
            CheckDeleted()
            If Row(AssociateCommissionsDAL.COL_NAME_MARKUP_PERCENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(AssociateCommissionsDAL.COL_NAME_MARKUP_PERCENT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(AssociateCommissionsDAL.COL_NAME_MARKUP_PERCENT, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidNumericRange("", Max:=100, Min:=0)> _
    Public Property CommissionPercent() As DecimalType
        Get
            CheckDeleted()
            If Row(AssociateCommissionsDAL.COL_NAME_COMMISSION_PERCENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(AssociateCommissionsDAL.COL_NAME_COMMISSION_PERCENT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(AssociateCommissionsDAL.COL_NAME_COMMISSION_PERCENT, Value)
        End Set
    End Property

    Public Property Position() As LongType
        Get
            CheckDeleted()
            If row(AssociateCommissionsDAL.COL_NAME_POSITION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(AssociateCommissionsDAL.COL_NAME_POSITION), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(AssociateCommissionsDAL.COL_NAME_POSITION, Value)
        End Set
    End Property

    Dim _MarkupTotal As DecimalType
    <ValidNumericRange("", Max:=100, Min:=100), ValidMarkup("")> _
    Public Property MarkupTotal() As DecimalType
        Get
            Return _MarkupTotal
        End Get
        Set(ByVal Value As DecimalType)
            _MarkupTotal = Value
        End Set
    End Property

    Dim _commTotal As DecimalType
    <ValidNumericRange("", Max:=100, Min:=100), ValidComm("")> _
    Public Property CommTotal() As DecimalType
        Get
            Return _commTotal
        End Get
        Set(ByVal Value As DecimalType)
            _commTotal = Value
        End Set
    End Property
#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New AssociateCommissionsDAL
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

    Public Shared Function getList(ByVal CommissionToleranceId As Guid) As SearchDV
        Try
            Dim dal As New AssociateCommissionsDAL

            Return New SearchDV(dal.LoadList(CommissionToleranceId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function


    Private Shared Function GetAssocCommList(ByVal parent As CommissionTolerance, ByVal id As Guid) As DataTable

        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(AssocCommList)) Then
                Dim dal As New AssociateCommissionsDAL
                dal.LoadList(id, parent.Dataset)
                parent.AddChildrenCollection(GetType(AssocCommList))
            End If
            'Return New AssocCommList(parent)
            Return parent.Dataset.Tables(AssociateCommissionsDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region

#Region "SearchDV"

    Public Class SearchDV
        Inherits DataView

#Region "Constants"

        Public Const COL_NAME_ASSOCIATE_COMMISSIONS_ID As String = AssociateCommissionsDAL.COL_NAME_ASSOCIATE_COMMISSIONS_ID
        Public Const COL_NAME_COMMISSION_TOLERANCE_ID As String = AssociateCommissionsDAL.COL_NAME_COMMISSION_TOLERANCE_ID
        Public Const COL_NAME_MARKUP_PERCENT As String = AssociateCommissionsDAL.COL_NAME_MARKUP_PERCENT
        Public Const COL_NAME_COMMISSION_PERCENT As String = AssociateCommissionsDAL.COL_NAME_COMMISSION_PERCENT
        Public Const COL_NAME_POSITION As String = AssociateCommissionsDAL.COL_NAME_POSITION
        Public Const COL_NAME_COMM_ENTITY_NAME As String = AssociateCommissionsDAL.COL_NAME_ENTITY_NAME
        Public Const COL_NAME_PAYEE_TYPE_ID As String = AssociateCommissionsDAL.COL_NAME_PAYEE_TYPE_ID
#End Region

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property AssociateCommissionId(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_ASSOCIATE_COMMISSIONS_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property CommissionToleranceId(ByVal row As DataRow) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_COMMISSION_TOLERANCE_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property MarkupPercent(ByVal row As DataRow) As LongType
            Get
                Return CType(row(COL_NAME_MARKUP_PERCENT), LongType)
            End Get
        End Property


        Public Shared ReadOnly Property CommissionPercent(ByVal row As DataRow) As LongType
            Get
                Return CType(row(COL_NAME_COMMISSION_PERCENT), LongType)
            End Get
        End Property

        Public Shared ReadOnly Property Position(ByVal row As DataRow) As LongType
            Get
                Return CType(row(COL_NAME_POSITION), LongType)
            End Get
        End Property

        Public Shared ReadOnly Property EntityName(ByVal row As DataRow) As String
            Get
                Return CType(row(COL_NAME_COMM_ENTITY_NAME), String)
            End Get
        End Property

        Public Shared ReadOnly Property PayeeTypeId(ByVal row As DataRow) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_PAYEE_TYPE_ID), Byte()))
            End Get
        End Property
    End Class

#End Region
#Region "List Methods"
    Public Class AssocCommList
        Inherits BusinessObjectListBase
        Public Sub New(ByVal parent As Object, ByVal id As Guid)
            MyBase.New(GetAssocCommList(parent, id), GetType(AssociateCommissions), parent)
        End Sub

        Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
            Return True
        End Function

        Public Function FindById(ByVal assocCommId As Guid) As AssociateCommissions
            Dim bo As AssociateCommissions
            For Each bo In Me
                If bo.Id.Equals(assocCommId) Then Return bo
            Next
            Return Nothing
        End Function

        Public Function FindByPosition(ByVal position As LongType) As AssociateCommissions
            Dim bo As AssociateCommissions
            For Each bo In Me
                If bo.Position.Equals(position) Then Return bo
            Next
            Return Nothing
        End Function

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
                        nSumm = .DealerCommPct.Value + .BrokerCommPct.Value + _
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

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidMarkup
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_COMM_BREAK_MARKUP_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim bIsOk As Boolean = True
            Dim oAssocComm As AssociateCommissions = CType(objectToValidate, AssociateCommissions)

            If (Me.DisplayName = "AllowedMarkupPct") AndAlso _
                    (GetRestrictMarkup(oAssocComm) = False) Then
                Return True
            End If


            If oAssocComm.MarkupTotal.Value <> 100 Then
                bIsOk = False
            End If

            Return bIsOk

        End Function

        Private Function GetRestrictMarkup(ByVal oAssocComm As AssociateCommissions) As Boolean
            Dim oPeriodData As New CommissionPeriodData
            Dim oTolerance As New CommissionTolerance(oAssocComm.CommissionToleranceId)
            Dim oPeriod As New CommissionPeriod(oTolerance.CommissionPeriodId)

            oPeriodData.dealerId = oPeriod.DealerId
            Return CommissionPeriod.GetRestrictMarkup(oPeriodData)
        End Function
    End Class


    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidComm
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_COMM_BREAK_COMM_PCT_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim bIsOk As Boolean = True
            Dim oAssocComm As AssociateCommissions = CType(objectToValidate, AssociateCommissions)

            If oAssocComm.CommTotal.Value <> 100 Then
                bIsOk = False
            End If

            Return bIsOk

        End Function
    End Class
#End Region

End Class





