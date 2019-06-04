'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (5/16/2012)  ********************

Public Class TurnAroundTimeRange
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load(id)
        Me._modefiedObjectId = id
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
        Me._modefiedObjectId = id
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
            Dim dal As New TurnAroundTimeRangeDAL
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
            Dim dal As New TurnAroundTimeRangeDAL
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

#Region "Constants"
    Public Const COL_NAME_TURN_AROUND_TIME_RANGE_ID As String = TurnAroundTimeRangeDAL.COL_NAME_TURN_AROUND_TIME_RANGE_ID
    Public Const COL_NAME_COMPANY_GROUP_ID As String = TurnAroundTimeRangeDAL.COL_NAME_COMPANY_GROUP_ID
    Public Const COL_NAME_COLOR_ID As String = TurnAroundTimeRangeDAL.COL_NAME_COLOR_ID
    Public Const COL_NAME_COLOR_NAME As String = TurnAroundTimeRangeDAL.COL_NAME_COLOR_NAME
    Public Const COL_NAME_CODE As String = TurnAroundTimeRangeDAL.COL_NAME_CODE
    Public Const COL_NAME_DESCRIPTION As String = TurnAroundTimeRangeDAL.COL_NAME_DESCRIPTION
    Public Const COL_NAME_MIN_DAYS As String = TurnAroundTimeRangeDAL.COL_NAME_MIN_DAYS
    Public Const COL_NAME_MAX_DAYS As String = TurnAroundTimeRangeDAL.COL_NAME_MAX_DAYS
#End Region

#Region "Private Members"
    Private _modefiedObjectId As Guid = Guid.Empty

    'Initialization code for new objects
    Private Sub Initialize()
    End Sub
    Private Function IsValidAction() As Boolean
        Dim dv As DataView = Me.LoadList ' This list is alrady ordered by min_days asc
        If Not dv Is Nothing AndAlso dv.Count > 0 Then

            Dim minValue As Integer
            Dim maxValue As Integer
            ' CHeck the deletion
            If Me.IsDeleted Then
                Dim i As Integer
                For i = 0 To dv.Count - 1
                    If i = 0 Or i = dv.Count - 1 Then
                        Dim id As Guid = New Guid(CType(dv.Table.Rows(i)(TurnAroundTimeRangeDAL.COL_NAME_TURN_AROUND_TIME_RANGE_ID), Byte()))
                        If Me.ModefiedObjectId.Equals(id) Then
                            Return True
                        End If
                    End If
                Next
                Return False
            End If

            'Check the minValue vs the maxValue
            If Me.MinDays.Value >= Me.MaxDays.Value Then
                Dim errors() As ValidationError = {New ValidationError(ElitaPlus.Common.ErrorCodes.MIN_VALUE_MUST_BE_LESS_THAN_MAX_VALUE, GetType(TurnAroundTimeRange), Nothing, "MIN_DAYS", Nothing)}
                Throw New BOValidationException(errors, GetType(TurnAroundTimeRange).FullName, Me.UniqueId)
            End If

            ' Check the editing: only first or last record
            If Not Me.IsNew Then
                Dim editingOnOtherFieldsOccurs As Boolean = False
                Dim i As Integer
                For i = 0 To dv.Count - 1
                    If i = 0 Then ' Only min value can be changed
                        Dim id As Guid = New Guid(CType(dv.Table.Rows(i)(TurnAroundTimeRangeDAL.COL_NAME_TURN_AROUND_TIME_RANGE_ID), Byte()))
                        Dim minV = CType(dv.Table.Rows(i)(TurnAroundTimeRangeDAL.COL_NAME_MIN_DAYS), Integer)
                        Dim maxV = CType(dv.Table.Rows(i)(TurnAroundTimeRangeDAL.COL_NAME_MAX_DAYS), Integer)
                        If Me.ModefiedObjectId.Equals(id) AndAlso maxV = Me.MaxDays.Value AndAlso Me.MinDays.Value < Me.MaxDays.Value And Me.MinDays.Value >= 0 Then
                            Return True
                        End If
                    End If

                    If i = dv.Count - 1 Then ' Only max value can be changed
                        Dim id As Guid = New Guid(CType(dv.Table.Rows(i)(TurnAroundTimeRangeDAL.COL_NAME_TURN_AROUND_TIME_RANGE_ID), Byte()))
                        Dim minV = CType(dv.Table.Rows(i)(TurnAroundTimeRangeDAL.COL_NAME_MIN_DAYS), Integer)
                        Dim maxV = CType(dv.Table.Rows(i)(TurnAroundTimeRangeDAL.COL_NAME_MAX_DAYS), Integer)
                        If Me.ModefiedObjectId.Equals(id) AndAlso minV = Me.MinDays.Value AndAlso Me.MinDays.Value < Me.MaxDays.Value And Me.MaxDays.Value <= 9999 Then
                            Return True
                        End If
                    End If
                    Dim code As String = dv.Table.Rows(i)(TurnAroundTimeRangeDAL.COL_NAME_CODE)
                    Dim description As String = dv.Table.Rows(i)(TurnAroundTimeRangeDAL.COL_NAME_DESCRIPTION)
                    Dim colorValue As Guid = New Guid(CType(dv.Table.Rows(i)(TurnAroundTimeRangeDAL.COL_NAME_COLOR_ID), Byte()))
                    If Not Me.Code.Equals(code) OrElse Not Me.Description.Equals(description) OrElse Not Me.ColorId.Equals(colorValue) Then
                        editingOnOtherFieldsOccurs = True
                    End If
                Next

                Return (editingOnOtherFieldsOccurs Or False)

            End If


            Dim rowFirst As DataRow = dv.Table.Rows(0)
            Dim rowLast As DataRow = dv.Table.Rows(dv.Table.Rows.Count - 1)

            Dim rowValueMin As Object = rowFirst(TurnAroundTimeRangeDAL.COL_NAME_MIN_DAYS)
            Dim rowValueMax As Object = rowLast(TurnAroundTimeRangeDAL.COL_NAME_MAX_DAYS)
            If (Not rowValueMin Is DBNull.Value) Then
                minValue = CType(rowValueMin, Integer)
            Else
                Dim Errs() As ValidationError = {New ValidationError(ElitaPlus.Common.ErrorCodes.EXISTING_TAT_RECORD_IS_INVALID, GetType(TurnAroundTimeRange), Nothing, "MIN_DAYS", Nothing)}
                Throw New BOValidationException(Errs, GetType(TurnAroundTimeRange).FullName)
            End If
            If (Not rowValueMax Is DBNull.Value) Then
                maxValue = CType(rowValueMax, Integer)
            Else
                Dim Errs() As ValidationError = {New ValidationError(ElitaPlus.Common.ErrorCodes.EXISTING_TAT_RECORD_IS_INVALID, GetType(TurnAroundTimeRange), Nothing, "MAX_DAYS", Nothing)}
                Throw New BOValidationException(Errs, GetType(TurnAroundTimeRange).FullName)
            End If

            If (Me.MaxDays.Value + 1) = minValue AndAlso Me.MinDays.Value < Me.MaxDays.Value AndAlso Me.MinDays.Value >= 0 Then
                Return True
            ElseIf (Me.MinDays.Value - 1) = maxValue AndAlso Me.MinDays.Value < Me.MaxDays.Value AndAlso Me.MaxDays.Value <= 9999 Then
                Return True
            Else
                Return False
            End If

        ElseIf Me.MinDays.Value >= 0 AndAlso Me.MinDays.Value < Me.MaxDays.Value AndAlso Me.MaxDays.Value <= 9999 Then
            Return True
        Else
            Return False
        End If

    End Function
#End Region


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If row(TurnAroundTimeRangeDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(TurnAroundTimeRangeDAL.COL_NAME_TURN_AROUND_TIME_RANGE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CompanyGroupId() As Guid
        Get
            CheckDeleted()
            If row(TurnAroundTimeRangeDAL.COL_NAME_COMPANY_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(TurnAroundTimeRangeDAL.COL_NAME_COMPANY_GROUP_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(TurnAroundTimeRangeDAL.COL_NAME_COMPANY_GROUP_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ColorId() As Guid
        Get
            CheckDeleted()
            If row(TurnAroundTimeRangeDAL.COL_NAME_COLOR_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(TurnAroundTimeRangeDAL.COL_NAME_COLOR_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(TurnAroundTimeRangeDAL.COL_NAME_COLOR_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=10)> _
    Public Property Code() As String
        Get
            CheckDeleted()
            If Row(TurnAroundTimeRangeDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(TurnAroundTimeRangeDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(TurnAroundTimeRangeDAL.COL_NAME_CODE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=50)> _
    Public Property Description() As String
        Get
            CheckDeleted()
            If Row(TurnAroundTimeRangeDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(TurnAroundTimeRangeDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(TurnAroundTimeRangeDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("", Min:=0, Max:=9998)> _
    Public Property MinDays() As LongType
        Get
            CheckDeleted()
            If row(TurnAroundTimeRangeDAL.COL_NAME_MIN_DAYS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(TurnAroundTimeRangeDAL.COL_NAME_MIN_DAYS), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(TurnAroundTimeRangeDAL.COL_NAME_MIN_DAYS, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("", Min:=1, Max:=9999)> _
    Public Property MaxDays() As LongType
        Get
            CheckDeleted()
            If row(TurnAroundTimeRangeDAL.COL_NAME_MAX_DAYS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(TurnAroundTimeRangeDAL.COL_NAME_MAX_DAYS), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(TurnAroundTimeRangeDAL.COL_NAME_MAX_DAYS, Value)
        End Set
    End Property

    Public ReadOnly Property ModefiedObjectId() As Guid
        Get
            Return _modefiedObjectId
        End Get
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Not Me.IsValidAction Then
                Dim errors() As ValidationError = {New ValidationError(ElitaPlus.Common.ErrorCodes.OVERLAPS_OR_GAPS_NOT_ALLOWED, GetType(TurnAroundTimeRange), Nothing, "MIN_DAYS", Nothing)}
                Throw New BOValidationException(errors, GetType(TurnAroundTimeRange).FullName, Me.UniqueId)
            End If
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New TurnAroundTimeRangeDAL
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
    Public Shared Function LoadList() As DataView
        Try
            Dim dal As New TurnAroundTimeRangeDAL
            Dim ds As DataSet
            ds = dal.LoadList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            Return (ds.Tables(TurnAroundTimeRangeDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function LoadListWithColor() As DataView
        Try
            Dim dal As New TurnAroundTimeRangeDAL
            Dim ds As DataSet
            ds = dal.LoadListWithColor(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            Return (ds.Tables(TurnAroundTimeRangeDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function


    Public Shared Function GetNewDataViewRow(ByVal dv As DataView, ByVal id As Guid) As DataView

        Dim dt As DataTable
        dt = dv.Table

        Dim row As DataRow = dt.NewRow
        row(TurnAroundTimeRangeDAL.COL_NAME_CODE) = String.Empty
        row(TurnAroundTimeRangeDAL.COL_NAME_DESCRIPTION) = String.Empty
        row(TurnAroundTimeRangeDAL.COL_NAME_TURN_AROUND_TIME_RANGE_ID) = id.ToByteArray
        row(TurnAroundTimeRangeDAL.COL_NAME_MIN_DAYS) = DBNull.Value
        row(TurnAroundTimeRangeDAL.COL_NAME_MAX_DAYS) = DBNull.Value
        row(TurnAroundTimeRangeDAL.COL_NAME_COLOR_ID) = id.ToByteArray

        dt.Rows.Add(row)

        Return (dv)

    End Function

    Public Shared Function getEmptyList(ByVal dv As DataView) As System.Data.DataView
        Try

            Dim dsv As DataSet
            dsv = dv.Table().DataSet

            Dim row As DataRow = dsv.Tables(0).NewRow()
            row.Item(TurnAroundTimeRangeDAL.COL_NAME_TURN_AROUND_TIME_RANGE_ID) = System.Guid.NewGuid.ToByteArray
            ' row.Item(TurnAroundTimeRangeDAL.COL_NAME_COMPANY_GROUP_ID) = System.Guid.NewGuid.ToByteArray

            row.Item(TurnAroundTimeRangeDAL.COL_NAME_CODE) = String.Empty
            row.Item(TurnAroundTimeRangeDAL.COL_NAME_DESCRIPTION) = String.Empty
            row.Item(TurnAroundTimeRangeDAL.COL_NAME_MIN_DAYS) = DBNull.Value
            row.Item(TurnAroundTimeRangeDAL.COL_NAME_MAX_DAYS) = DBNull.Value
            row.Item(TurnAroundTimeRangeDAL.COL_NAME_COLOR_ID) = System.Guid.NewGuid.ToByteArray


            dsv.Tables(0).Rows.Add(row)
            Return New System.Data.DataView(dsv.Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region


End Class


