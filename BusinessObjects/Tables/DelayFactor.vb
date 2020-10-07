'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (5/7/2008)  ********************
Imports Common = Assurant.ElitaPlus.Common

Public Class DelayFactor
    Inherits BusinessObjectBase

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
            Dim dal As New DelayFactorDAL
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

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New DelayFactorDAL
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

    Public Shared Function LoadList(ByVal DealerId As Guid, ByVal effective As Date, ByVal expiration As Date) As DataView
        Try
            Dim dal As New DelayFactorDAL
            Dim ds As DataSet

            ds = dal.LoadList(DealerId, effective, expiration)
            Return (ds.Tables(DelayFactorDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetDelayFactorList(ByVal DealerId As Guid, ByVal effective As Date, ByVal expiration As Date) As DataSet
        Try
            Dim dal As New DelayFactorDAL
            Dim ds As DataSet

            ds = dal.LoadList(DealerId, effective, expiration)
            Return ds

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function LoadListByDealer(ByVal DealerId As Guid) As DataSet
        Try
            Dim dal As New DelayFactorDAL
            Dim ds As DataSet

            ds = dal.LoadListByDealer(DealerId)
            Return ds

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

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
            If row(DelayFactorDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(DelayFactorDAL.COL_NAME_DELAY_FACTOR_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property DealerId As Guid
        Get
            CheckDeleted()
            If row(DelayFactorDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(DelayFactorDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DelayFactorDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidDelayFactorPeriod("")> _
    Public Property EffectiveDate As DateType
        Get
            CheckDeleted()
            If Row(DelayFactorDAL.COL_NAME_EFFECTIVE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(DelayFactorDAL.COL_NAME_EFFECTIVE_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DelayFactorDAL.COL_NAME_EFFECTIVE_DATE, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property ExpirationDate As DateType
        Get
            CheckDeleted()
            If row(DelayFactorDAL.COL_NAME_EXPIRATION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(DelayFactorDAL.COL_NAME_EXPIRATION_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DelayFactorDAL.COL_NAME_EXPIRATION_DATE, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidNumericRange("LowNumberOfDays", MIN:=MIN_DAY, Max:=MAX_DAY, Message:=ERR_LOW_DAY_OUT_OF_BOUND), ValidDelayFactor("")> _
    Public Property LowNumberOfDays As LongType
        Get
            CheckDeleted()
            If Row(DelayFactorDAL.COL_NAME_LOW_NUMBER_OF_DAYS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(DelayFactorDAL.COL_NAME_LOW_NUMBER_OF_DAYS), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DelayFactorDAL.COL_NAME_LOW_NUMBER_OF_DAYS, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidNumericRange("", MIN:=MIN_DAY, Max:=MAX_DAY, Message:=ERR_HIGH_DAY_OUT_OF_BOUND)> _
    Public Property HighNumberOfDays As LongType
        Get
            CheckDeleted()
            If Row(DelayFactorDAL.COL_NAME_HIGH_NUMBER_OF_DAYS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(DelayFactorDAL.COL_NAME_HIGH_NUMBER_OF_DAYS), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DelayFactorDAL.COL_NAME_HIGH_NUMBER_OF_DAYS, Value)
        End Set
    End Property

    <ValidNumericRange("", MIN:=MIN_FACTOR, Max:=MAX_FACTOR, Message:=ERR_DELAY_FACTOR_OUT_OF_BOUND), ValidDelayFactorPercent("")> _
    Public Property Factor As DecimalType
        Get
            CheckDeleted()
            If Row(DelayFactorDAL.COL_NAME_FACTOR) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(DelayFactorDAL.COL_NAME_FACTOR), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DelayFactorDAL.COL_NAME_FACTOR, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New DelayFactorDAL
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

    Public Shared Function GetDelayFactorByDealer(ByVal dealerID As Guid, ByVal compId As ArrayList) As DelayFactorSearchDV
        Try
            Dim dal As New DelayFactorDAL
            Dim dv As New DelayFactorSearchDV(dal.GetDelayFactorByDealer(dealerID, compId).Tables(0))
            dv.Sort = DelayFactor.DelayFactorSearchDV.COL_EFFECTIVE_DATE & " DESC"

            Return dv
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetNewDataViewRow(ByVal dv As DataView, ByVal id As Guid, ByVal bo As DelayFactor) As DataView

        Dim dt As DataTable
        dt = dv.Table

        If bo.IsNew Then
            Dim row As DataRow = dt.NewRow

            If bo.Factor Is Nothing Then
                row(DelayFactor.DelayFactorSearchDV.COL_FACTOR) = DBNull.Value
            Else
                row(DelayFactor.DelayFactorSearchDV.COL_FACTOR) = bo.Factor
            End If

            If bo.HighNumberOfDays Is Nothing Then
                row(DelayFactor.DelayFactorSearchDV.COL_HIGH_NUMBER_OF_DAYS) = DBNull.Value
            Else
                row(DelayFactor.DelayFactorSearchDV.COL_HIGH_NUMBER_OF_DAYS) = bo.HighNumberOfDays
            End If

            If bo.LowNumberOfDays Is Nothing Then
                row(DelayFactor.DelayFactorSearchDV.COL_LOW_NUMBER_OF_DAYS) = DBNull.Value
            Else
                row(DelayFactor.DelayFactorSearchDV.COL_LOW_NUMBER_OF_DAYS) = bo.LowNumberOfDays
            End If

            If bo.EffectiveDate Is Nothing Then
                row(DelayFactor.DelayFactorSearchDV.COL_EFFECTIVE_DATE) = Date.MinValue
            Else
                row(DelayFactor.DelayFactorSearchDV.COL_EFFECTIVE_DATE) = bo.EffectiveDate
            End If

            If bo.ExpirationDate Is Nothing Then
                row(DelayFactor.DelayFactorSearchDV.COL_EXPIRATION_DATE) = Date.MinValue
            Else
                row(DelayFactor.DelayFactorSearchDV.COL_EXPIRATION_DATE) = bo.ExpirationDate
            End If

            row(DelayFactor.DelayFactorSearchDV.COL_DELAY_FACTOR_ID) = bo.Id.ToByteArray
            row(DelayFactor.DelayFactorSearchDV.COL_DEALER_ID) = bo.DealerId.ToByteArray

            dt.Rows.Add(row)
        End If

        Return (dv)

    End Function

#End Region

#Region "Constants"

    Public Const DELAY_FACTOR_ID_IDX As Integer = 0
    Public Const DEALER_ID_IDX As Integer = 1
    Public Const EFFECTIVE_DATE_IDX As Integer = 2
    Public Const EXPIRATION_DATE_IDX As Integer = 3
    Public Const LOW_DAY_IDX As Integer = 4
    Public Const HIGH_DAY_IDX As Integer = 5
    Public Const FACTOR_IDX As Integer = 6

    Public Const MIN_DAY As Integer = 0
    Public Const MAX_DAY As Integer = 9999
    Public Const THRESHOLD As Integer = 1
    Public Const MIN_FACTOR As Double = 1.0
    Public Const MAX_FACTOR As Double = 9999.99

    Public Const DELAY_FACTOR As String = "DELAY FACTOR"
    Public Const ERR_LOW_DAY_OUT_OF_BOUND = "ERR_LOW_DAY_OUT_OF_BOUND"  ' Low # Of Day Must Be Between 0 And 9999
    Public Const ERR_LOW_DAY_MORE_THAN_HIGH_DAY = "ERR_LOW_DAY_MORE_THAN_HIGH_DAY" ' Low # Of Day Must Be Less Than or Equal To High # Of Day
    Public Const ERR_LOW_DAY_AND_HIGH_DAY_OVERLAPS = "ERR_LOW_DAY_AND_HIGH_DAY_OVERLAPS" ' Low # Of Day And High # Of Day Overlaps
    Public Const ERR_HIGH_DAY_OUT_OF_BOUND = "ERR_HIGH_DAY_OUT_OF_BOUND"  ' High # Of Day Must Be Between 0 And 9999
    Public Const ERR_DELAY_FACTOR_OUT_OF_BOUND = "ERR_DELAY_FACTOR_OUT_OF_BOUND"  ' Factor Must Be Between 1.00 And 9999.99
    Public Const INVALID_DELAY_FACTOR_PERIOD As String = "INVALID_DELAY_FACTOR_PERIOD" ' Effective Must Be One Day Higher Than the Expiration And No Overlaps Allowed
    Public Const INVALID_DELAY_FACTOR_PERCENT As String = "INVALID_DELAY_FACTOR_PERCENT" ' The factor of the next period should be equal to or higher than the factor of the preceding period
    Public Const ERR_DELAY_FACTOR_REQUIRED As String = "ERR_DELAY_FACTOR_REQUIRED" ' Delay Factor Required

    Public Class DelayFactorSearchDV
        Inherits DataView

        Public Const COL_DELAY_FACTOR_ID As String = "delay_factor_id"
        Public Const COL_DEALER_ID As String = "dealer_id"
        Public Const COL_EFFECTIVE_DATE As String = "effective_date"
        Public Const COL_EXPIRATION_DATE As String = "expiration_date"
        Public Const COL_LOW_NUMBER_OF_DAYS As String = "low_number_of_days"
        Public Const COL_HIGH_NUMBER_OF_DAYS As String = "high_number_of_days"
        Public Const COL_FACTOR As String = "factor"
        Public Const COL_DEALER_CODE As String = "dealer_code"
        Public Const COL_DEALER_NAME As String = "dealer_name"

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region

#Region "Custom Validation"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidDelayFactor
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, ERR_LOW_DAY_MORE_THAN_HIGH_DAY)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As DelayFactor = CType(objectToValidate, DelayFactor)
            Dim bValid As Boolean = True

            If obj.DealerId.Equals(Guid.Empty) OrElse obj.EffectiveDate Is Nothing OrElse obj.ExpirationDate Is Nothing Then
                bValid = True
            Else
                If Not obj.LowNumberOfDays Is Nothing And Not obj.HighNumberOfDays Is Nothing Then
                    If obj.LowNumberOfDays.Value > obj.HighNumberOfDays.Value Then
                        Message = ERR_LOW_DAY_MORE_THAN_HIGH_DAY
                        bValid = False
                    ElseIf ValidateRange(obj.LowNumberOfDays, obj.HighNumberOfDays, obj) = False Then
                        Message = ERR_LOW_DAY_AND_HIGH_DAY_OVERLAPS
                        bValid = False
                    End If
                End If
            End If

            Return bValid

        End Function

        Private Function ValidateRange(ByVal sNewLow As Assurant.Common.Types.LongType, ByVal sNewHigh As Assurant.Common.Types.LongType, ByVal oDelayFactor As DelayFactor) As Boolean
            Dim bValid As Boolean = False
            Dim oNewLow As Long = sNewLow.Value
            Dim oNewHigh As Long = sNewHigh.Value
            Dim oDelayFactorId As Guid = oDelayFactor.Id
            Dim oLow, oHigh As Long
            Dim prevLow As Long = MIN_DAY - THRESHOLD
            Dim prevHigh As Long = MIN_DAY - THRESHOLD
            Dim dvDelayFactor As DataView = LoadList(oDelayFactor.DealerId, oDelayFactor.EffectiveDate, oDelayFactor.ExpirationDate)
            Dim oRows As DataRowCollection = dvDelayFactor.Table.Rows
            Dim oRow As DataRow
            Dim oCount As Integer = 0
            If oRows.Count = 0 Then
                'Inserting only one record
                bValid = True
            Else
                For Each oRow In oRows
                    oDelayFactorId = New Guid(CType(oRow(DELAY_FACTOR_ID_IDX), Byte()))
                    oLow = oRow(LOW_DAY_IDX)
                    oHigh = oRow(HIGH_DAY_IDX)
                    oCount = oCount + 1
                    If oDelayFactor.Id.Equals(oDelayFactorId) Then
                        If oRows.Count = 1 Then
                            ' Updating only one record
                            bValid = True
                            Exit For
                        ElseIf oRows.Count = oCount And prevHigh + THRESHOLD = oNewLow Then
                            ' Updating the last record
                            bValid = True
                            Exit For
                        End If
                    Else
                        If prevHigh < MIN_DAY And oNewHigh + THRESHOLD = oLow Then
                            bValid = True
                            Exit For
                        ElseIf oCount = oRows.Count And oHigh + THRESHOLD = oNewLow Then
                            bValid = True
                            Exit For
                        ElseIf prevHigh + THRESHOLD = oNewLow And oNewHigh + THRESHOLD = oLow Then
                            bValid = True
                            Exit For
                        End If
                        prevLow = oLow
                        prevHigh = oHigh
                    End If
                Next
            End If

            Return bValid
        End Function

    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
Public NotInheritable Class ValidDelayFactorPeriod
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, INVALID_DELAY_FACTOR_PERIOD)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean

            Dim obj As DelayFactor = CType(objectToValidate, DelayFactor)
            Dim bValid As Boolean = True
            Dim dal As New DelayFactorDAL
            Dim ds As New DataSet
            Dim currRow, prevRow, nextRow As DataRow

            If obj.EffectiveDate Is Nothing Or obj.EffectiveDate Is Nothing Then
                Return True  ' Skip validation. Rely on mandatory field validation to report exception
            End If

            ds = LoadListByDealer(obj.DealerId)

            Dim recCount As Integer = 0
            If ds.Tables.Count > 0 Then
                recCount = ds.Tables(0).Rows.Count
                Dim lastRowId As Guid
                Dim currRowPos As Integer = 0
                If recCount > 0 Then
                    lastRowId = New Guid(CType(ds.Tables(0).Rows(recCount - 1)(DelayFactor.DelayFactorSearchDV.COL_DELAY_FACTOR_ID), Byte()))
                    Dim minEffective As Date = ds.Tables(0).Rows(0)(DelayFactor.DelayFactorSearchDV.COL_EFFECTIVE_DATE)
                    Dim maxExpiration As Date = ds.Tables(0).Rows(recCount - 1)(DelayFactor.DelayFactorSearchDV.COL_EXPIRATION_DATE)
                    ' Same period allowed since validation is on the day range
                    If obj.EffectiveDate = minEffective AndAlso obj.ExpirationDate = maxExpiration Then
                        Return True
                    End If
                    ' Inserting at the begining
                    If obj.ExpirationDate.Value.AddDays(1) = minEffective Then
                        Return True
                    End If
                    ' Inserting at the end
                    If obj.EffectiveDate.Value.AddDays(-1) = maxExpiration Then
                        Return True
                    End If
                    ' Find a spot in the middle
                    For Each currRow In ds.Tables(0).Rows
                        If obj.ExpirationDate.Value = currRow(DelayFactor.DelayFactorSearchDV.COL_EXPIRATION_DATE) And _
                            obj.EffectiveDate.Value = currRow(DelayFactor.DelayFactorSearchDV.COL_EFFECTIVE_DATE) Then
                            ' Trying to insert a Duplicate - Reject!
                            Return False
                        ElseIf Not prevRow Is Nothing Then
                            ' Inserting in the middle (Allow to fix any GAPS)
                            If obj.EffectiveDate.Value.AddDays(-1) = prevRow(DelayFactor.DelayFactorSearchDV.COL_EXPIRATION_DATE) And _
                               obj.ExpirationDate.Value.AddDays(1) = currRow(DelayFactor.DelayFactorSearchDV.COL_EFFECTIVE_DATE) Then
                                Return True
                            End If
                        End If
                        prevRow = currRow
                        currRowPos += 1
                    Next
                    bValid = False
                End If
            End If

            Return bValid

        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
  Public NotInheritable Class ValidDelayFactorPercent
        Inherits ValidBaseAttribute

        Dim ar As New ArrayList
        Dim bValidFactor As Boolean = False

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, INVALID_DELAY_FACTOR_PERCENT)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As DelayFactor = CType(objectToValidate, DelayFactor)
            Dim bValid As Boolean = True

            If Not obj.LowNumberOfDays Is Nothing And Not obj.HighNumberOfDays Is Nothing Then
                If (obj.Factor Is Nothing) Then
                    Message = ERR_DELAY_FACTOR_REQUIRED
                    bValid = False
                Else
                    If obj.DealerId.Equals(Guid.Empty) OrElse obj.EffectiveDate Is Nothing OrElse obj.ExpirationDate Is Nothing Then
                        bValid = True
                    Else
                        bValid = ValidateRange(obj)
                        If bValidFactor = True Then
                            If Not ar Is Nothing And ar.Count > 0 Then
                                Message = CType(ar(0), String)
                            End If
                        Else
                            bValid = True
                        End If
                    End If
                End If
            End If

            Return bValid

        End Function

        Private Function ValidateRange(ByVal oDelayFactor As DelayFactor) As Boolean
            Dim bValid As Boolean = False
            Dim oNewLow As Long = oDelayFactor.LowNumberOfDays
            Dim oNewHigh As Long = oDelayFactor.HighNumberOfDays
            Dim oDelayFactorId As Guid = oDelayFactor.Id
            Dim oLow, oHigh As Long
            Dim oFactor, preFactor As Double
            Dim prevLow As Long = MIN_DAY - THRESHOLD
            Dim prevHigh As Long = MIN_DAY - THRESHOLD
            Dim dvDelayFactor As DataView = LoadList(oDelayFactor.DealerId, oDelayFactor.EffectiveDate, oDelayFactor.ExpirationDate)
            Dim oRows As DataRowCollection = dvDelayFactor.Table.Rows
            Dim oRow As DataRow
            Dim oCount As Integer = 0
            If oRows.Count = 0 Then
                'Inserting only one record
                bValid = True
            Else
                For Each oRow In oRows
                    oDelayFactorId = New Guid(CType(oRow(DELAY_FACTOR_ID_IDX), Byte()))
                    oLow = CType(oRow(LOW_DAY_IDX), Long)
                    oHigh = CType(oRow(HIGH_DAY_IDX), Long)

                    If (oRow(FACTOR_IDX).Equals(DBNull.Value)) Then
                        oFactor = Nothing
                    Else
                        oFactor = CType(oRow(FACTOR_IDX), Double)
                    End If

                    oCount = oCount + 1
                    If oDelayFactor.Id.Equals(oDelayFactorId) Then
                        If oRows.Count = 1 Then
                            ' Updating only one record
                            bValid = True
                            bValidFactor = True
                            Exit For
                        ElseIf oRows.Count = oCount And prevHigh + THRESHOLD = oNewLow Then
                            ' Updating the last record
                            bValidFactor = True
                            bValid = ValidateDelayFactorSequence(oDelayFactor.Factor, preFactor)
                            Exit For
                        End If
                    Else
                        If prevHigh < MIN_DAY And oNewHigh + THRESHOLD = oLow Then
                            bValidFactor = True
                            bValid = ValidateDelayFactorSequence(oFactor, oDelayFactor.Factor)
                            Exit For
                        ElseIf oCount = oRows.Count And oHigh + THRESHOLD = oNewLow Then
                            bValidFactor = True
                            bValid = ValidateDelayFactorSequence(oDelayFactor.Factor, oFactor)
                            Exit For
                        ElseIf prevHigh + THRESHOLD = oNewLow And oNewHigh + THRESHOLD = oLow Then
                            bValidFactor = True
                            bValid = ValidateDelayFactorSequence(oFactor, oDelayFactor.Factor)
                            Exit For
                        End If
                        prevLow = oLow
                        prevHigh = oHigh
                    End If

                    preFactor = oFactor
                Next
            End If

            Return bValid

        End Function

        Public Function ValidateDelayFactorSequence(ByVal oNewFactor As DecimalType, ByVal oFactor As DecimalType) As Boolean
            Dim bValid As Boolean = False

            If (Not oNewFactor Is Nothing AndAlso Not oFactor Is Nothing) Then
                If CType(oNewFactor, Double) > 0 Then
                    If (CType(oNewFactor, Double) >= CType(oFactor, Double)) Then
                        bValid = True
                    Else
                        bValid = False
                        ar.Add(INVALID_DELAY_FACTOR_PERCENT)
                    End If
                Else
                    bValid = True
                End If
            End If

            Return bValid

        End Function

    End Class

#End Region
End Class


