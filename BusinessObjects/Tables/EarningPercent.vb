'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (12/11/2006)  ********************

Public Class EarningPercent
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
            Dim dal As New EarningPercentDAL
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
            Dim dal As New EarningPercentDAL
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

#Region "Constants"

    Private Const MIN_TERM As Integer = 1
    Private Const MAX_TERM As Integer = 999
    Private Const MIN_PERCENT As Decimal = 0.0
    Private Const MAX_PERCENT As Decimal = 100.0

    Private Const EARNING_PERCENT_ID As Integer = 0
    Private Const EARNING_PATTERN_ID As Integer = 1
    Private Const EARNING_TERM As Integer = 2
    Private Const EARNING_PERCENT As Integer = 3
    Private Const MIM_DECIMAL_NUMBERS As Integer = 4
    Private Const EARNING_PERCENT_FORM012 As String = "COVERAGE_RATE_FORM012" ' Only 4 Digits Allowed After Decimal Point

#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If Row(EarningPercentDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(EarningPercentDAL.COL_NAME_EARNING_PERCENT_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property EarningPatternId() As Guid
        Get
            CheckDeleted()
            If Row(EarningPercentDAL.COL_NAME_EARNING_PATTERN_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(EarningPercentDAL.COL_NAME_EARNING_PATTERN_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(EarningPercentDAL.COL_NAME_EARNING_PATTERN_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("EarningTerm", MIN:=MIN_TERM, Max:=MAX_TERM, Message:=ElitaPlus.Common.ErrorCodes.EARNING_MONTH_MUST_BE_BETWEEN_1_AND_999), ValidTerm("")> _
    Public Property EarningTerm() As LongType
        Get
            CheckDeleted()
            If Row(EarningPercentDAL.COL_NAME_EARNING_TERM) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(EarningPercentDAL.COL_NAME_EARNING_TERM), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(EarningPercentDAL.COL_NAME_EARNING_TERM, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("EarningPercent", MIN:=MIN_PERCENT, Max:=MAX_PERCENT, Message:=ElitaPlus.Common.ErrorCodes.EARNING_PERCENT_MUST_BE_LESS_THAN_100), ValidPercent(""), ValidateDecimalNumber("", DecimalValue:=MIM_DECIMAL_NUMBERS, Message:=EARNING_PERCENT_FORM012)> _
    Public Property EarningPercent() As DecimalType
        Get
            CheckDeleted()
            If Row(EarningPercentDAL.COL_NAME_EARNING_PERCENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(EarningPercentDAL.COL_NAME_EARNING_PERCENT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(EarningPercentDAL.COL_NAME_EARNING_PERCENT, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New EarningPercentDAL
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
    Public Shared Function GetList(ByVal EarningPatternId As Guid) As DataView
        Try
            Dim dal As New EarningPercentDAL
            Return New DataView(dal.LoadList(EarningPatternId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Function IsLastterm() As Boolean
        Dim obj As EarningPercent = Me

        Dim bValid As Boolean = True
        If Not obj.EarningTerm Is Nothing Then
            Dim dal As New EarningPercentDAL
            Dim termCnt As Integer = dal.TermCount(obj.MAX_TERM, obj.EarningPatternId, Guid.Empty)
            If termCnt <> obj.EarningTerm.Value Then bValid = False
        End If

        Return bValid

    End Function
#End Region

#Region "Custom Validation"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidTerm
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, ElitaPlus.Common.ErrorCodes.EARNING_MONTH_IS_INVALID)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As EarningPercent = CType(objectToValidate, EarningPercent)

            Dim bValid As Boolean = True
            If Not obj.EarningTerm Is Nothing Then
                Dim dal As New EarningPercentDAL
                Dim termCnt As Integer = dal.TermCount(obj.EarningTerm.Value, obj.EarningPatternId, obj.Id)
                If termCnt <> obj.EarningTerm.Value - 1 Then bValid = False
            End If


            Return bValid

        End Function

    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidPercent
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, ElitaPlus.Common.ErrorCodes.EARNING_PERCENT_IS_INVALID)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As EarningPercent = CType(objectToValidate, EarningPercent)
            Dim bValid As Boolean = True

            If Not (obj.EarningPercent Is Nothing Or obj.EarningTerm Is Nothing) Then
                If obj.EarningPercent.Value <= MAX_PERCENT Then ' Check only if value is valid to avoid double msg.
                    Dim dal As New EarningPercentDAL
                    Dim prevPct As Integer = dal.TermPercent(obj.EarningTerm.Value - 1.0, obj.EarningPatternId) * 10000
                    Dim nextPct As Integer = dal.TermPercent(obj.EarningTerm.Value + 1.0, obj.EarningPatternId) * 10000
                    Dim currPct As Integer = obj.EarningPercent.Value * 10000
                    If prevPct < 0 Then prevPct = MIN_PERCENT * 10000
                    If nextPct < 0 Then nextPct = MAX_PERCENT * 10000
                    If prevPct > currPct Or currPct > nextPct Then bValid = False
                End If
            End If

            Return bValid

        End Function

    End Class

#End Region

End Class


