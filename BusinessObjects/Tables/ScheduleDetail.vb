'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (8/24/2012)  ********************

Public Class ScheduleDetail
    Inherits BusinessObjectBase

    Public Class ScheduleDetailSearchDV
        Inherits DataView

        Public Const COL_SCHEDULE_DETAIL_ID As String = "schedule_detail_id"
        Public Const COL_SCHEDULE_ID As String = "schedule_id"
        Public Const COL_DAY_OF_WEEK_ID As String = "day_of_week_id"
        Public Const COL_FROM_TIME As String = "from_time"
        Public Const COL_TO_TIME As String = "to_time"

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

    Public ReadOnly Property DataTable As DataTable
        Get
            Return Row.Table
        End Get
    End Property

    Public ReadOnly Property DataRow As DataRow
        Get
            Return Row
        End Get
    End Property

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
            Dim dal As New ScheduleDetailDAL
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
            Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New ScheduleDetailDAL
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

    Public Sub New(ByVal id As Guid, ByVal familyDS As DataSet, ByVal SecondaryKeyName As String)
        MyBase.New(False)
        Dataset = familyDS
        Load(id, SecondaryKeyName)
    End Sub

    Protected Sub Load(ByVal id As Guid, ByVal SecondaryKeyName As String)
        Try
            Dim dal As New ScheduleDetailDAL
            If _isDSCreator Then
                If Not Row Is Nothing Then
                    Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Row)
                End If
            End If
            Row = Nothing
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Row = FindRow(id, SecondaryKeyName, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.Load(Dataset, id)
                Row = FindRow(id, SecondaryKeyName, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then
                Throw New DataNotFoundException
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Shared Function LoadScheduleDetail(ByVal ScheduleId As Guid) As DataView
        Try
            Dim dal As New ScheduleDetailDAL
            Dim ds As DataSet

            ds = dal.LoadScheduleDetail(ScheduleId)
            Return (ds.Tables(ScheduleDetailDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetNewDataViewRow(ByVal dv As DataView, ByVal id As Guid, ByVal bo As ScheduleDetail) As DataView

        Dim dt As DataTable
        dt = dv.Table

        If bo.IsNew Then
            Dim row As DataRow = dt.NewRow

            If bo.FromTime Is Nothing Then
                row(ScheduleDetail.ScheduleDetailSearchDV.COL_FROM_TIME) = DBNull.Value
            Else
                row(ScheduleDetail.ScheduleDetailSearchDV.COL_FROM_TIME) = bo.FromTime
            End If

            If bo.ToTime Is Nothing Then
                row(ScheduleDetail.ScheduleDetailSearchDV.COL_TO_TIME) = DBNull.Value
            Else
                row(ScheduleDetail.ScheduleDetailSearchDV.COL_TO_TIME) = bo.ToTime
            End If

            row(ScheduleDetail.ScheduleDetailSearchDV.COL_DAY_OF_WEEK_ID) = bo.DayOfWeekId.ToByteArray
            row(ScheduleDetail.ScheduleDetailSearchDV.COL_SCHEDULE_DETAIL_ID) = bo.Id.ToByteArray
            row(ScheduleDetail.ScheduleDetailSearchDV.COL_SCHEDULE_ID) = bo.ScheduleId.ToByteArray

            dt.Rows.Add(row)
        End If

        Return (dv)

    End Function


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
            If Row(ScheduleDetailDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ScheduleDetailDAL.COL_NAME_SCHEDULE_DETAIL_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property ScheduleId() As Guid
        Get
            CheckDeleted()
            If Row(ScheduleDetailDAL.COL_NAME_SCHEDULE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ScheduleDetailDAL.COL_NAME_SCHEDULE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(ScheduleDetailDAL.COL_NAME_SCHEDULE_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property DayOfWeekId() As Guid
        Get
            CheckDeleted()
            If Row(ScheduleDetailDAL.COL_NAME_DAY_OF_WEEK_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ScheduleDetailDAL.COL_NAME_DAY_OF_WEEK_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(ScheduleDetailDAL.COL_NAME_DAY_OF_WEEK_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property FromTime() As DateType
        Get
            CheckDeleted()
            If Row(ScheduleDetailDAL.COL_NAME_FROM_TIME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(ScheduleDetailDAL.COL_NAME_FROM_TIME).ToString()))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            SetValue(ScheduleDetailDAL.COL_NAME_FROM_TIME, Value)
        End Set
    End Property

    <ValueMandatory(""), DateCompareValidator("", Assurant.ElitaPlus.Common.ErrorCodes.INVALID_FROM_TIME_HIGHER_THAN_TO_TIME, "FromTime", DateCompareValidatorAttribute.CompareType.GreaterThan), _
         OverlapValidator("", DataRowPropertyName:="DataRow", DataTablePropertyName:="DataTable", EffectiveDateColumnName:=ScheduleDetailDAL.COL_NAME_FROM_TIME, ExpirationDateColumnName:=ScheduleDetailDAL.COL_NAME_TO_TIME, _
        KeyColumns:=New String() {ScheduleDetailDAL.COL_NAME_DAY_OF_WEEK_ID})> _
    Public Property ToTime() As DateType
        Get
            CheckDeleted()
            If Row(ScheduleDetailDAL.COL_NAME_TO_TIME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(ScheduleDetailDAL.COL_NAME_TO_TIME).ToString()))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            SetValue(ScheduleDetailDAL.COL_NAME_TO_TIME, Value)
        End Set
    End Property



#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ScheduleDetailDAL
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

    Public Class ScheduleDetailList
        Inherits BusinessObjectListBase

        Public Sub New(ByVal parent As Schedule)
            MyBase.New(LoadTable(parent), GetType(ScheduleDetail), parent)
        End Sub

        Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
            Return CType(bo, ScheduleDetail).ScheduleId.Equals(CType(Parent, Schedule).Id)
        End Function

        Private Shared Function LoadTable(ByVal parent As Schedule) As DataTable
            Try
                If Not parent.IsChildrenCollectionLoaded(GetType(ScheduleDetailList)) Then
                    Dim dal As New ScheduleDetailDAL
                    dal.LoadScheduleDetail(parent.Dataset, parent.Id)
                    parent.AddChildrenCollection(GetType(ScheduleDetailList))
                End If
                Return parent.Dataset.Tables(ScheduleDetailDAL.TABLE_NAME)
            Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End Function

    End Class

End Class