Public Class DepreciationScdDetails
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
    Public Sub New(id As Guid, familyDs As DataSet)
        MyBase.New(False)
        Dataset = familyDs
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(familyDs As DataSet)
        MyBase.New(False)
        Dataset = familyDs
        Load()
    End Sub

    Public Sub New(row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            If Dataset.Tables.IndexOf(DepreciationSchDetailsDal.TableName) < 0 Then
                Dim dal As New DepreciationSchDetailsDal
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(DepreciationSchDetailsDal.TableName).NewRow
            Dataset.Tables(DepreciationSchDetailsDal.TableName).Rows.Add(newRow)
            Row = newRow
            SetValue(DepreciationSchDetailsDal.TableKeyName, Guid.NewGuid)
            Initialize()
        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(id As Guid)
        Try
            If _isDSCreator Then
                If Not Row Is Nothing Then
                    Dataset.Tables(DepreciationSchDetailsDal.TableName).Rows.Remove(Row)
                End If
            End If
            Row = Nothing
            If Dataset.Tables.IndexOf(DepreciationSchDetailsDal.TableName) >= 0 Then
                Row = FindRow(id, DepreciationSchDetailsDal.TableKeyName, Dataset.Tables(DepreciationSchDetailsDal.TableName))
            End If
            If Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                Dim dal As New DepreciationSchDetailsDal
                dal.Load(Dataset, id)
                Row = FindRow(id, DepreciationSchDetailsDal.TableKeyName, Dataset.Tables(DepreciationSchDetailsDal.TableName))
            End If
            If Row Is Nothing Then
                Throw New DataNotFoundException
            End If
        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
#End Region

#Region "Constant"

    Public Const DepreciationIdIdx As Integer = 0
    Public Const ContractIdIdx As Integer = 1
    Public Const LowMonthIdx As Integer = 2
    Public Const HighMonthIdx As Integer = 3
    Public Const PercentIdx As Integer = 4
    Public Const AmountIdx As Integer = 5

    Public Const DepreciationScheduleIdColName As String = "depreciation_schedule_id"
    Public Const LowMonthColName As String = "low_month"
    Public Const HighMonthColName As String = "high_month"
    Public Const PercentColName As String = "percent"
    Public Const AmountColName As String = "amount"

    Public Const MinMonth As Integer = 1
    Public Const MaxMonth As Integer = 9999
    Public Const MinPercent As Integer = 0
    Public Const MaxPercent As Integer = 100
    Public Const MinAmount As Integer = 0
    Public Const MaxAmount As Integer = 99999
    Public Const Threshold As Integer = 1

    Public Const DepreciationSchedule As String = "DEPRECIATION SCHEDULE"
    Public Const DepreciationScheduleForm001 As String = "DEPRECIATION_SCHEDULE_FORM001" ' 0 < LowMonth <1*10^6
    Public Const DepreciationScheduleForm002 As String = "DEPRECIATION_SCHEDULE_FORM002" ' 0 < HighMonth <1*10^6
    Public Const DepreciationScheduleForm003 As String = "DEPRECIATION_SCHEDULE_FORM003" ' 0 <= Percent <1*10^6
    Public Const DepreciationScheduleForm004 As String = "DEPRECIATION_SCHEDULE_FORM004" ' 0 <= Amount <1*10^6
    Public Const DepreciationScheduleForm005 As String = "DEPRECIATION_SCHEDULE_FORM005" ' LowMonth Must be less or equal than HighMonth
    Public Const DepreciationScheduleForm006 As String = "DEPRECIATION_SCHEDULE_FORM006" ' There should be no overlaps (LowMonth/HighMonth)
    Public Const DepreciationScheduleForm007 As String = "DEPRECIATION_SCHEDULE_FORM007" ' Only allow to enter either Percent or Amount not both
    Public Const DepreciationScheduleForm008 As String = "DEPRECIATION_SCHEDULE_FORM008" ' Percent or Amount must be whole number
    Public Const DepreciationScheduleForm009 As String = "DEPRECIATION_SCHEDULE_FORM009" ' LowMonth and HighMonth must be whole number
    Public Const DepreciationScheduleForm010 As String = "DEPRECIATION_SCHEDULE_FORM010" ' Must enter either Percent or Amount not both
    Public Const DepreciationScheduleForm011 As String = "DEPRECIATION_SCHEDULE_FORM011" ' The percentage/amount of the next period should be equal to or higher than the percentage of the preceding period

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
            If Row(DepreciationSchDetailsDal.TableKeyName) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DepreciationSchDetailsDal.ColNameDepreciationScheduleItemId), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")>
    Public Property DepreciationScheduleId As Guid
        Get
            CheckDeleted()
            If Row(DepreciationSchDetailsDal.ColNameDepreciationScheduleId) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DepreciationSchDetailsDal.ColNameDepreciationScheduleId), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DepreciationSchDetailsDal.ColNameDepreciationScheduleId, value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("LowMonth", Min:=MinMonth, Max:=MaxMonth, Message:=DepreciationScheduleForm001), ValidDepreciationSchedule("")>
    Public Property LowMonth As LongType
        Get
            CheckDeleted()
            If Row(DepreciationSchDetailsDal.ColNameLowMonth) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(DepreciationSchDetailsDal.ColNameLowMonth), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DepreciationSchDetailsDal.ColNameLowMonth, value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("", Min:=MinMonth, Max:=MaxMonth, Message:=DepreciationScheduleForm002)>
    Public Property HighMonth As LongType
        Get
            CheckDeleted()
            If Row(DepreciationSchDetailsDal.ColNameHighMonth) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(DepreciationSchDetailsDal.ColNameHighMonth), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DepreciationSchDetailsDal.ColNameHighMonth, value)
        End Set
    End Property


    <ValidNumericRange("", Min:=MinPercent, Max:=MaxPercent, Message:=DepreciationScheduleForm003), ValidDepreciationSchedulePercentAmount("")>
    Public Property Percent As LongType
        Get
            CheckDeleted()
            If Row(DepreciationSchDetailsDal.ColNamePercent) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(DepreciationSchDetailsDal.ColNamePercent), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DepreciationSchDetailsDal.ColNamePercent, value)
        End Set
    End Property

    <ValidNumericRange("", Min:=MinAmount, Max:=NEW_MAX_LONG, Message:=DepreciationScheduleForm004)>
    Public Property Amount As LongType
        Get
            CheckDeleted()
            If Row(DepreciationSchDetailsDal.ColNameAmount) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(DepreciationSchDetailsDal.ColNameAmount), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DepreciationSchDetailsDal.ColNameAmount, value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New DepreciationSchDetailsDal
                dal.Update(Row)
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Id
                    Dataset = New DataSet
                    Row = Nothing
                    Load(objId)
                End If
            End If
        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
#End Region

#Region "DataView Retrieveing Methods"

    Public Shared Function LoadList() As DataView
        Return LoadList(New Guid)
    End Function

    Public Shared Function LoadList(depreciationScheduleId As Guid) As DataView
        Try
            Dim dal As New DepreciationSchDetailsDal
            Dim ds As DataSet

            ds = dal.LoadList(depreciationScheduleId, Authentication.LangId)
            Return (ds.Tables(DepreciationSchDetailsDal.TableName).DefaultView)

        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetNewDataViewRow(dv As DataView, id As Guid, bo As DepreciationScdDetails) As DataView

        Dim dt As DataTable
        dt = dv.Table

        If bo.IsNew Then
            Dim row As DataRow = dt.NewRow

            If bo.Amount Is Nothing Then
                row(DepreciationSchDetailsDal.ColNameAmount) = DBNull.Value
            Else
                row(DepreciationSchDetailsDal.ColNameAmount) = bo.Amount
            End If

            If bo.HighMonth Is Nothing Then
                row(DepreciationSchDetailsDal.ColNameHighMonth) = DBNull.Value
            Else
                row(DepreciationSchDetailsDal.ColNameHighMonth) = bo.HighMonth
            End If

            If bo.LowMonth Is Nothing Then
                row(DepreciationSchDetailsDal.ColNameLowMonth) = DBNull.Value
            Else
                row(DepreciationSchDetailsDal.ColNameLowMonth) = bo.LowMonth
            End If

            If bo.Percent Is Nothing Then
                row(DepreciationSchDetailsDal.ColNamePercent) = DBNull.Value
            Else
                row(DepreciationSchDetailsDal.ColNamePercent) = bo.Percent
            End If

            row(DepreciationSchDetailsDal.ColNameDepreciationScheduleItemId) = bo.Id.ToByteArray
            row(DepreciationSchDetailsDal.ColNameDepreciationScheduleId) = bo.DepreciationScheduleId.ToByteArray

            dt.Rows.Add(row)
        End If

        Return (dv)

    End Function

#End Region

#Region "Custom Validation"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidDepreciationSchedule
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, DepreciationScheduleForm005)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As DepreciationScdDetails = CType(objectToValidate, DepreciationScdDetails)
            Dim bValid As Boolean = True

            If Not obj.LowMonth Is Nothing And Not obj.HighMonth Is Nothing Then
                If obj.LowMonth.Value > obj.HighMonth.Value Then
                    Message = DepreciationScheduleForm005
                    bValid = False
                ElseIf ValidateRange(obj.LowMonth, obj.HighMonth, obj) = False Then
                    Message = DepreciationScheduleForm006
                    bValid = False
                End If
            End If

            Return bValid

        End Function

        Private Function ValidateRange(sNewLow As LongType, sNewHigh As LongType, oDepreciationSchedule As DepreciationScdDetails) As Boolean
            Dim bValid As Boolean = False
            Dim oNewLow As Long = sNewLow.Value
            Dim oNewHigh As Long = sNewHigh.Value
            Dim oDepreciationId As Guid
            Dim oLow, oHigh As Long
            Dim prevHigh As Long = MinMonth - Threshold
            Dim oDepreciationSchedules As DataView = LoadList(oDepreciationSchedule.DepreciationScheduleId)
            Dim oRows As DataRowCollection = oDepreciationSchedules.Table.Rows
            Dim oRow As DataRow
            Dim oCount As Integer = 0
            If oRows.Count = 0 Then
                'Inserting only one record
                bValid = True
            Else
                For Each oRow In oRows
                    oDepreciationId = New Guid(CType(oRow(DepreciationIdIdx), Byte()))
                    oLow = oRow(LowMonthIdx)
                    oHigh = oRow(HighMonthIdx)
                    oCount = oCount + 1
                    If oDepreciationSchedule.Id.Equals(oDepreciationId) Then
                        If oRows.Count = 1 Then
                            ' Updating only one record
                            bValid = True
                            Exit For
                        ElseIf oRows.Count = oCount And prevHigh + Threshold = oNewLow Then
                            ' Updating the last record
                            bValid = True
                            Exit For
                        End If
                    Else
                        If prevHigh < MinMonth And oNewHigh + Threshold = oLow Then
                            bValid = True
                            Exit For
                        ElseIf oCount = oRows.Count And oHigh + Threshold = oNewLow Then
                            bValid = True
                            Exit For
                        ElseIf prevHigh + Threshold = oNewLow And oNewHigh + Threshold = oLow Then
                            bValid = True
                            Exit For
                        End If
                        prevHigh = oHigh
                    End If
                Next
            End If

            Return bValid
        End Function

    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidDepreciationSchedulePercentAmount
        Inherits ValidBaseAttribute

        Dim _ar As New ArrayList
        Dim _bValidMonth As Boolean = False

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, DepreciationScheduleForm007)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As DepreciationScdDetails = CType(objectToValidate, DepreciationScdDetails)
            Dim bValid As Boolean = True

            If Not obj.LowMonth Is Nothing And Not obj.HighMonth Is Nothing Then
                If (obj.Percent Is Nothing And obj.Amount Is Nothing) Then
                    Message = DepreciationScheduleForm010
                    bValid = False
                ElseIf (Not obj.Percent Is Nothing And Not obj.Amount Is Nothing) Then
                    Message = DepreciationScheduleForm007
                    bValid = False
                Else
                    bValid = ValidateRange(obj)
                    If _bValidMonth = True Then
                        If Not _ar Is Nothing And _ar.Count > 0 Then
                            Message = CType(_ar(0), String)
                        End If
                    Else
                        bValid = True
                    End If
                End If
            End If

            Return bValid

        End Function

        Private Function ValidateRange(oDepreciationSchedule As DepreciationScdDetails) As Boolean
            Dim bValid As Boolean = False
            Dim oNewLow As Long = oDepreciationSchedule.LowMonth
            Dim oNewHigh As Long = oDepreciationSchedule.HighMonth
            Dim oDepreciationId As Guid
            Dim oLow, oHigh, oPercent, oAmount, prePercent, preAmount As Long
            Dim prevHigh As Long = MinMonth - Threshold
            Dim oDepreciationSchedules As DataView = LoadList(oDepreciationSchedule.DepreciationScheduleId)
            Dim oRows As DataRowCollection = oDepreciationSchedules.Table.Rows
            Dim oRow As DataRow
            Dim oCount As Integer = 0
            If oRows.Count = 0 Then
                'Inserting only one record
                bValid = True
            Else
                For Each oRow In oRows
                    oDepreciationId = New Guid(CType(oRow(DepreciationIdIdx), Byte()))
                    oLow = CType(oRow(LowMonthIdx), Long)
                    oHigh = CType(oRow(HighMonthIdx), Long)

                    If (oRow(PercentIdx).Equals(DBNull.Value)) Then
                        oPercent = Nothing
                    Else
                        oPercent = CType(oRow(PercentIdx), Long)
                    End If

                    If (oRow(AmountIdx).Equals(DBNull.Value)) Then
                        oAmount = Nothing
                    Else
                        oAmount = CType(oRow(AmountIdx), Long)
                    End If

                    oCount = oCount + 1
                    If oDepreciationSchedule.Id.Equals(oDepreciationId) Then
                        If oRows.Count = 1 Then
                            ' Updating only one record
                            bValid = True
                            _bValidMonth = True
                            Exit For
                        ElseIf oRows.Count = oCount And prevHigh + Threshold = oNewLow Then
                            ' Updating the last record
                            _bValidMonth = True
                            bValid = ValidateDepreciationSequence(oDepreciationSchedule.Percent, prePercent, oDepreciationSchedule.Amount, preAmount)
                            Exit For
                        End If
                    Else
                        If prevHigh < MinMonth And oNewHigh + Threshold = oLow Then
                            _bValidMonth = True
                            bValid = ValidateDepreciationSequence(oPercent, oDepreciationSchedule.Percent, oAmount, oDepreciationSchedule.Amount)
                            Exit For
                        ElseIf oCount = oRows.Count And oHigh + Threshold = oNewLow Then
                            _bValidMonth = True
                            bValid = ValidateDepreciationSequence(oDepreciationSchedule.Percent, oPercent, oDepreciationSchedule.Amount, oAmount)
                            Exit For
                        ElseIf prevHigh + Threshold = oNewLow And oNewHigh + Threshold = oLow Then
                            _bValidMonth = True
                            bValid = ValidateDepreciationSequence(oPercent, oDepreciationSchedule.Percent, oAmount, oDepreciationSchedule.Amount)
                            Exit For
                        End If
                        prevHigh = oHigh
                    End If

                    prePercent = oPercent
                    preAmount = oAmount
                Next
            End If

            Return bValid

        End Function

        Public Function ValidateDepreciationSequence(oNewPercent As LongType, oPercent As LongType, oNewAmount As LongType, oAmount As LongType) As Boolean
            Dim bValid As Boolean = False

            If (Not oNewPercent Is Nothing AndAlso Not oPercent Is Nothing) Then
                If CType(oNewPercent, Long) > 0 Then
                    If (CType(oNewPercent, Long) > CType(oPercent, Long)) Then
                        bValid = True
                    Else
                        bValid = False
                        _ar.Add(DepreciationScheduleForm011)
                        'Me.Message = DEPRECIATION_SCHEDULE_FORM011
                    End If
                Else
                    bValid = True
                End If
            Else
                If (Not oNewAmount Is Nothing AndAlso Not oAmount Is Nothing) Then
                    If CType(oNewAmount, Long) > 0 Then
                        If (CType(oNewAmount, Long) > CType(oAmount, Long)) Then
                            bValid = True
                        Else
                            bValid = False
                            _ar.Add(DepreciationScheduleForm011)
                            'Me.Message = DEPRECIATION_SCHEDULE_FORM011
                        End If
                    Else
                        bValid = True
                    End If
                End If
            End If

            Return bValid

        End Function


    End Class

#End Region

#Region "DepreciationScheduleDV"
    Public Class DepreciationScheduleDv
        Inherits DataView

        Public Const ColDepreciationScheduleId As String = DepreciationSchDetailsDal.ColNameDepreciationScheduleId
        Public Const ColLowMonth As String = DepreciationSchDetailsDal.ColNameLowMonth
        Public Const ColHighMonth As String = DepreciationSchDetailsDal.ColNameHighMonth
        Public Const ColAmount As String = DepreciationSchDetailsDal.ColNameAmount
        Public Const ColPercent As String = DepreciationSchDetailsDal.ColNamePercent

        'Public Const COL_DEALER As String = DepreciationScheduleDAL.COL_NAME_DEALER
        'Public Const COL_DEALER_NAME As String = DepreciationScheduleDAL.COL_NAME_DEALER_NAME
        Public Const ColEffective As String = DepreciationSchDetailsDal.ColNameEffective
        Public Const ColExpiration As String = DepreciationSchDetailsDal.ColNameExpiration
        'Public Const COL_DEALER_ID As String = DepreciationScheduleDAL.COL_NAME_DEALER_ID


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region


End Class

