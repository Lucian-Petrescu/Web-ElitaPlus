'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (5/7/2008)  ********************
Imports Common = Assurant.ElitaPlus.Common

Public Class InstallmentFactor
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
            Dim dal As New InstallmentFactorDAL
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
            Dim dal As New InstallmentFactorDAL
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
            Dim dal As New InstallmentFactorDAL
            Dim ds As DataSet

            ds = dal.LoadList(DealerId, effective, expiration)
            Return (ds.Tables(InstallmentFactorDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetInstallmentFactorList(ByVal DealerId As Guid, ByVal effective As Date, ByVal expiration As Date) As DataSet
        Try
            Dim dal As New InstallmentFactorDAL
            Dim ds As DataSet

            ds = dal.LoadList(DealerId, effective, expiration)
            Return ds

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function LoadListByDealer(ByVal DealerId As Guid) As DataSet
        Try
            Dim dal As New InstallmentFactorDAL
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
            If Row(InstallmentFactorDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(InstallmentFactorDAL.COL_NAME_INSTALLMENT_FACTOR_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property DealerId As Guid
        Get
            CheckDeleted()
            If Row(InstallmentFactorDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(InstallmentFactorDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(InstallmentFactorDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidInstallmentFactorPeriod("")> _
    Public Property EffectiveDate As DateType
        Get
            CheckDeleted()
            If Row(InstallmentFactorDAL.COL_NAME_EFFECTIVE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(InstallmentFactorDAL.COL_NAME_EFFECTIVE_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(InstallmentFactorDAL.COL_NAME_EFFECTIVE_DATE, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property ExpirationDate As DateType
        Get
            CheckDeleted()
            If Row(InstallmentFactorDAL.COL_NAME_EXPIRATION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(InstallmentFactorDAL.COL_NAME_EXPIRATION_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(InstallmentFactorDAL.COL_NAME_EXPIRATION_DATE, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidNumericRange("LowNumberOfPayments", MIN:=MIN_PAYMENT, Max:=MAX_PAYMENT, Message:=ERR_LOW_PAYMENT_OUT_OF_BOUND), ValidInstallmentFactor("")> _
    Public Property LowNumberOfPayments As LongType
        Get
            CheckDeleted()
            If Row(InstallmentFactorDAL.COL_NAME_LOW_NUMBER_OF_PAYMENTS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(InstallmentFactorDAL.COL_NAME_LOW_NUMBER_OF_PAYMENTS), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(InstallmentFactorDAL.COL_NAME_LOW_NUMBER_OF_PAYMENTS, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidNumericRange("", MIN:=MIN_PAYMENT, Max:=MAX_PAYMENT, Message:=ERR_HIGH_PAYMENT_OUT_OF_BOUND)> _
    Public Property HighNumberOfPayments As LongType
        Get
            CheckDeleted()
            If Row(InstallmentFactorDAL.COL_NAME_HIGH_NUMBER_OF_PAYMENTS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(InstallmentFactorDAL.COL_NAME_HIGH_NUMBER_OF_PAYMENTS), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(InstallmentFactorDAL.COL_NAME_HIGH_NUMBER_OF_PAYMENTS, Value)
        End Set
    End Property

    <ValidNumericRange("", MIN:=MIN_FACTOR, Max:=MAX_FACTOR, Message:=ERR_INSTALLMENT_FACTOR_OUT_OF_BOUND), ValidInstallmentFactorPercent("")> _
    Public Property Factor As DecimalType
        Get
            CheckDeleted()
            If Row(InstallmentFactorDAL.COL_NAME_FACTOR) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(InstallmentFactorDAL.COL_NAME_FACTOR), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(InstallmentFactorDAL.COL_NAME_FACTOR, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New InstallmentFactorDAL
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

    Public Shared Function GetInstallmentFactorByDealer(ByVal dealerID As Guid, ByVal compId As ArrayList) As InstallmentFactorSearchDV
        Try
            Dim dal As New InstallmentFactorDAL
            Dim dv As New InstallmentFactorSearchDV(dal.GetInstallmentFactorByDealer(dealerID, compId).Tables(0))
            dv.Sort = InstallmentFactor.InstallmentFactorSearchDV.COL_EFFECTIVE_DATE & " DESC"

            Return dv
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetNewDataViewRow(ByVal dv As DataView, ByVal id As Guid, ByVal bo As InstallmentFactor) As DataView

        Dim dt As DataTable
        dt = dv.Table

        If bo.IsNew Then
            Dim row As DataRow = dt.NewRow

            If bo.Factor Is Nothing Then
                row(InstallmentFactor.InstallmentFactorSearchDV.COL_FACTOR) = DBNull.Value
            Else
                row(InstallmentFactor.InstallmentFactorSearchDV.COL_FACTOR) = bo.Factor
            End If

            If bo.HighNumberOfPayments Is Nothing Then
                row(InstallmentFactor.InstallmentFactorSearchDV.COL_HIGH_NUMBER_OF_PAYMENTS) = DBNull.Value
            Else
                row(InstallmentFactor.InstallmentFactorSearchDV.COL_HIGH_NUMBER_OF_PAYMENTS) = bo.HighNumberOfPayments
            End If

            If bo.LowNumberOfPayments Is Nothing Then
                row(InstallmentFactor.InstallmentFactorSearchDV.COL_LOW_NUMBER_OF_PAYMENTS) = DBNull.Value
            Else
                row(InstallmentFactor.InstallmentFactorSearchDV.COL_LOW_NUMBER_OF_PAYMENTS) = bo.LowNumberOfPayments
            End If

            If bo.EffectiveDate Is Nothing Then
                row(InstallmentFactor.InstallmentFactorSearchDV.COL_EFFECTIVE_DATE) = Date.MinValue
            Else
                row(InstallmentFactor.InstallmentFactorSearchDV.COL_EFFECTIVE_DATE) = bo.EffectiveDate
            End If

            If bo.ExpirationDate Is Nothing Then
                row(InstallmentFactor.InstallmentFactorSearchDV.COL_EXPIRATION_DATE) = Date.MinValue
            Else
                row(InstallmentFactor.InstallmentFactorSearchDV.COL_EXPIRATION_DATE) = bo.ExpirationDate
            End If

            row(InstallmentFactor.InstallmentFactorSearchDV.COL_INSTALLMENT_FACTOR_ID) = bo.Id.ToByteArray
            row(InstallmentFactor.InstallmentFactorSearchDV.COL_DEALER_ID) = bo.DealerId.ToByteArray

            dt.Rows.Add(row)
        End If

        Return (dv)

    End Function

#End Region

#Region "Constants"

    Public Const INSTALLMENT_FACTOR_ID_IDX As Integer = 0
    Public Const DEALER_ID_IDX As Integer = 1
    Public Const EFFECTIVE_DATE_IDX As Integer = 2
    Public Const EXPIRATION_DATE_IDX As Integer = 3
    Public Const LOW_PAYMENT_IDX As Integer = 4
    Public Const HIGH_PAYMENT_IDX As Integer = 5
    Public Const FACTOR_IDX As Integer = 6

    Public Const MIN_PAYMENT As Integer = 1
    Public Const MAX_PAYMENT As Integer = 12
    Public Const THRESHOLD As Integer = 1
    Public Const MIN_FACTOR As Double = 1.0
    Public Const MAX_FACTOR As Double = 9999.99

    Public Const INSTALLMENT_FACTOR As String = "INSTALLMENT FACTOR"
    Public Const ERR_LOW_PAYMENT_OUT_OF_BOUND = "ERR_LOW_PAYMENT_OUT_OF_BOUND"  ' Low # Of Payment Must Be Between 1 And 12
    Public Const ERR_LOW_PAYMENT_MORE_THAN_HIGH_PAYMENT = "ERR_LOW_PAYMENT_MORE_THAN_HIGH_PAYMENT" ' Low # Of Payment Must Be Less Than or Equal To High # Of Payment
    Public Const ERR_LOW_PAYMENT_AND_HIGH_PAYMENT_OVERLAPS = "ERR_LOW_PAYMENT_AND_HIGH_PAYMENT_OVERLAPS" ' Low # Of Payment And High # Of Payment Overlaps
    Public Const ERR_HIGH_PAYMENT_OUT_OF_BOUND = "ERR_HIGH_PAYMENT_OUT_OF_BOUND"  ' High # Of Payment Must Be Between 1 And 12
    Public Const ERR_INSTALLMENT_FACTOR_OUT_OF_BOUND = "ERR_INSTALLMENT_FACTOR_OUT_OF_BOUND"  ' Factor Must Be Between 1.00 And 9999.99
    Public Const INVALID_INSTALLMENT_FACTOR_PERIOD As String = "INVALID_INSTALLMENT_FACTOR_PERIOD" ' Effective Must Be One Day Higher Than the Expiration And No Overlaps Allowed
    Public Const INVALID_INSTALLMENT_FACTOR_PERCENT As String = "INVALID_INSTALLMENT_FACTOR_PERCENT" ' The factor of the next period should be equal to or higher than the factor of the preceding period
    Public Const ERR_INSTALLMENT_FACTOR_REQUIRED As String = "ERR_INSTALLMENT_FACTOR_REQUIRED" ' Delay Factor Required

    Public Class InstallmentFactorSearchDV
        Inherits DataView

        Public Const COL_INSTALLMENT_FACTOR_ID As String = "installment_factor_id"
        Public Const COL_DEALER_ID As String = "dealer_id"
        Public Const COL_EFFECTIVE_DATE As String = "effective_date"
        Public Const COL_EXPIRATION_DATE As String = "expiration_date"
        Public Const COL_LOW_NUMBER_OF_PAYMENTS As String = "low_number_of_payments"
        Public Const COL_HIGH_NUMBER_OF_PAYMENTS As String = "high_number_of_payments"
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
    Public NotInheritable Class ValidInstallmentFactor
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, ERR_LOW_PAYMENT_MORE_THAN_HIGH_PAYMENT)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As InstallmentFactor = CType(objectToValidate, InstallmentFactor)
            Dim bValid As Boolean = True

            If Not obj.LowNumberOfPayments Is Nothing And Not obj.HighNumberOfPayments Is Nothing Then
                If obj.LowNumberOfPayments.Value > obj.HighNumberOfPayments.Value Then
                    Message = ERR_LOW_PAYMENT_MORE_THAN_HIGH_PAYMENT
                    bValid = False
                ElseIf ValidateRange(obj.LowNumberOfPayments, obj.HighNumberOfPayments, obj) = False Then
                    Message = ERR_LOW_PAYMENT_AND_HIGH_PAYMENT_OVERLAPS
                    bValid = False
                End If
            End If

            Return bValid

        End Function

        Private Function ValidateRange(ByVal sNewLow As Assurant.Common.Types.LongType, ByVal sNewHigh As Assurant.Common.Types.LongType, ByVal oInstallmentFactor As InstallmentFactor) As Boolean
            Dim bValid As Boolean = False
            Dim oNewLow As Long = sNewLow.Value
            Dim oNewHigh As Long = sNewHigh.Value
            Dim oInstallmentFactorId As Guid = oInstallmentFactor.Id
            Dim oLow, oHigh As Long
            Dim prevLow As Long = MIN_PAYMENT - THRESHOLD
            Dim prevHigh As Long = MIN_PAYMENT - THRESHOLD
            Dim dvInstallmentFactor As DataView = LoadList(oInstallmentFactor.DealerId, oInstallmentFactor.EffectiveDate, oInstallmentFactor.ExpirationDate)
            Dim oRows As DataRowCollection = dvInstallmentFactor.Table.Rows
            Dim oRow As DataRow
            Dim oCount As Integer = 0
            If oRows.Count = 0 Then
                'Inserting only one record
                bValid = True
            Else
                For Each oRow In oRows
                    oInstallmentFactorId = New Guid(CType(oRow(INSTALLMENT_FACTOR_ID_IDX), Byte()))
                    oLow = oRow(LOW_PAYMENT_IDX)
                    oHigh = oRow(HIGH_PAYMENT_IDX)
                    oCount = oCount + 1
                    If oInstallmentFactor.Id.Equals(oInstallmentFactorId) Then
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
                        If prevHigh < MIN_PAYMENT And oNewHigh + THRESHOLD = oLow Then
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
Public NotInheritable Class ValidInstallmentFactorPeriod
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, INVALID_INSTALLMENT_FACTOR_PERIOD)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean

            Dim obj As InstallmentFactor = CType(objectToValidate, InstallmentFactor)
            Dim bValid As Boolean = True
            Dim dal As New InstallmentFactorDAL
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
                    lastRowId = New Guid(CType(ds.Tables(0).Rows(recCount - 1)(InstallmentFactor.InstallmentFactorSearchDV.COL_INSTALLMENT_FACTOR_ID), Byte()))
                    Dim minEffective As Date = ds.Tables(0).Rows(0)(InstallmentFactor.InstallmentFactorSearchDV.COL_EFFECTIVE_DATE)
                    Dim maxExpiration As Date = ds.Tables(0).Rows(recCount - 1)(InstallmentFactor.InstallmentFactorSearchDV.COL_EXPIRATION_DATE)
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
                        If obj.ExpirationDate.Value = currRow(InstallmentFactor.InstallmentFactorSearchDV.COL_EXPIRATION_DATE) And _
                            obj.EffectiveDate.Value = currRow(InstallmentFactor.InstallmentFactorSearchDV.COL_EFFECTIVE_DATE) Then
                            ' Trying to insert a Duplicate - Reject!
                            Return False
                        ElseIf Not prevRow Is Nothing Then
                            ' Inserting in the middle (Allow to fix any GAPS)
                            If obj.EffectiveDate.Value.AddDays(-1) = prevRow(InstallmentFactor.InstallmentFactorSearchDV.COL_EXPIRATION_DATE) And _
                               obj.ExpirationDate.Value.AddDays(1) = currRow(InstallmentFactor.InstallmentFactorSearchDV.COL_EFFECTIVE_DATE) Then
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
  Public NotInheritable Class ValidInstallmentFactorPercent
        Inherits ValidBaseAttribute

        Dim ar As New ArrayList
        Dim bValidFactor As Boolean = False

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, INVALID_INSTALLMENT_FACTOR_PERCENT)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As InstallmentFactor = CType(objectToValidate, InstallmentFactor)
            Dim bValid As Boolean = True

            If Not obj.LowNumberOfPayments Is Nothing And Not obj.HighNumberOfPayments Is Nothing Then
                If (obj.Factor Is Nothing) Then
                    Message = ERR_INSTALLMENT_FACTOR_REQUIRED
                    bValid = False
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

            Return bValid

        End Function

        Private Function ValidateRange(ByVal oInstallmentFactor As InstallmentFactor) As Boolean
            Dim bValid As Boolean = False
            Dim oNewLow As Long = oInstallmentFactor.LowNumberOfPayments
            Dim oNewHigh As Long = oInstallmentFactor.HighNumberOfPayments
            Dim oInstallmentFactorId As Guid = oInstallmentFactor.Id
            Dim oLow, oHigh As Long
            Dim oFactor, preFactor As Double
            Dim prevLow As Long = MIN_PAYMENT - THRESHOLD
            Dim prevHigh As Long = MIN_PAYMENT - THRESHOLD
            Dim dvInstallmentFactor As DataView = LoadList(oInstallmentFactor.DealerId, oInstallmentFactor.EffectiveDate, oInstallmentFactor.ExpirationDate)
            Dim oRows As DataRowCollection = dvInstallmentFactor.Table.Rows
            Dim oRow As DataRow
            Dim oCount As Integer = 0
            If oRows.Count = 0 Then
                'Inserting only one record
                bValid = True
            Else
                For Each oRow In oRows
                    oInstallmentFactorId = New Guid(CType(oRow(INSTALLMENT_FACTOR_ID_IDX), Byte()))
                    oLow = CType(oRow(LOW_PAYMENT_IDX), Long)
                    oHigh = CType(oRow(HIGH_PAYMENT_IDX), Long)

                    If (oRow(FACTOR_IDX).Equals(DBNull.Value)) Then
                        oFactor = Nothing
                    Else
                        oFactor = CType(oRow(FACTOR_IDX), Double)
                    End If

                    oCount = oCount + 1
                    If oInstallmentFactor.Id.Equals(oInstallmentFactorId) Then
                        If oRows.Count = 1 Then
                            ' Updating only one record
                            bValid = True
                            bValidFactor = True
                            Exit For
                        ElseIf oRows.Count = oCount And prevHigh + THRESHOLD = oNewLow Then
                            ' Updating the last record
                            bValidFactor = True
                            bValid = ValidateInstallmentFactorSequence(oInstallmentFactor.Factor, preFactor)
                            Exit For
                        End If
                    Else
                        If prevHigh < MIN_PAYMENT And oNewHigh + THRESHOLD = oLow Then
                            bValidFactor = True
                            bValid = ValidateInstallmentFactorSequence(oFactor, oInstallmentFactor.Factor)
                            Exit For
                        ElseIf oCount = oRows.Count And oHigh + THRESHOLD = oNewLow Then
                            bValidFactor = True
                            bValid = ValidateInstallmentFactorSequence(oInstallmentFactor.Factor, oFactor)
                            Exit For
                        ElseIf prevHigh + THRESHOLD = oNewLow And oNewHigh + THRESHOLD = oLow Then
                            bValidFactor = True
                            bValid = ValidateInstallmentFactorSequence(oFactor, oInstallmentFactor.Factor)
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

        Public Function ValidateInstallmentFactorSequence(ByVal oNewFactor As DecimalType, ByVal oFactor As DecimalType) As Boolean
            Dim bValid As Boolean = False

            If (Not oNewFactor Is Nothing AndAlso Not oFactor Is Nothing) Then
                If CType(oNewFactor, Double) > 0 Then
                    If (CType(oNewFactor, Double) >= CType(oFactor, Double)) Then
                        bValid = True
                    Else
                        bValid = False
                        ar.Add(INVALID_INSTALLMENT_FACTOR_PERCENT)
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


