'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (8/21/2012)  ********************

Public Class Schedule
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
            Dim dal As New ScheduleDAL
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
            Dim dal As New ScheduleDAL
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
            If row(ScheduleDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ScheduleDAL.COL_NAME_SCHEDULE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=10), ScheduleCodeValidator("")> _
    Public Property Code As String
        Get
            CheckDeleted()
            If Row(ScheduleDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ScheduleDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ScheduleDAL.COL_NAME_CODE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=50)> _
    Public Property Description As String
        Get
            CheckDeleted()
            If Row(ScheduleDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ScheduleDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ScheduleDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property

#End Region

#Region "Public Members"

    Public Sub Copy(original As Schedule)
        If Not IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Schedule")
        End If
        'Copy myself
        CopyFrom(original)
    End Sub

    Public Overrides Sub Save()
        Try
            MyBase.Save()

            If (_isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached) Then
                Dim dal As New ScheduleDAL
                'dal.Update(Me.Row) 'Original code generated replaced by the code below
                dal.UpdateFamily(Dataset) 'New Code Added Manually

                'Update Work Queue and the Entity Schedule with any changes
                UpdateWorkQueue()

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

    Private Sub UpdateWorkQueue()
        Dim anyUpdates As Boolean = False
        Dim dt As DataTable
        ' Find all the Work Queues which are using this Schedule as of today or in future.
        dt = EntitySchedule.GetList(Id, EntitySchedule.TABLE_NAME_WORKQUEUE)
        ' For each Work Queue, check if User is in Admin Role, if not then ReadOnly = True
        For Each dr As DataRow In dt.Rows
            anyUpdates = False
            Dim wqId As Guid
            Dim wq As WorkQueue
            wqId = New Guid(CType(dr(EntityScheduleDAL.COL_NAME_ENTITY_ID), Byte()))
            wq = New WorkQueue(wqId)
            For Each drv As DataRowView In wq.GetScheduleSelectionView()
                Dim es As EntitySchedule
                Dim newEs As EntitySchedule
                Dim orgExpiration As DateTimeType
                es = wq.GetScheduleChild(New Guid(CType(drv(EntityScheduleDAL.COL_NAME_ENTITY_SCHEDULE_ID), Byte())))
                If (es.ScheduleId = Id AndAlso es.Expiration > wq.ConvertTimeFromUtc(DateTime.UtcNow)) Then
                    anyUpdates = True
                    orgExpiration = es.Expiration
                    If (es.Effective > wq.ConvertTimeFromUtc(DateTime.UtcNow)) Then
                        ' Full Record is in Future
                        es.Expiration = es.Effective.Value.AddSeconds(1)
                        es.EndEdit()
                        newEs = wq.GetNewScheduleChild()
                        newEs.ScheduleId = es.ScheduleId
                        newEs.ScheduleCode = es.ScheduleCode
                        newEs.ScheduleDescription = es.ScheduleDescription
                        newEs.Effective = es.Effective.Value.AddSeconds(2)
                        newEs.Expiration = orgExpiration
                        newEs.EndEdit()
                    Else
                        ' Partial Record is in Future
                        es.Expiration = wq.ConvertTimeFromUtc(DateTime.UtcNow.AddSeconds(-1))
                        es.EndEdit()
                        newEs = wq.GetNewScheduleChild()
                        newEs.ScheduleId = es.ScheduleId
                        newEs.ScheduleCode = es.ScheduleCode
                        newEs.ScheduleDescription = es.ScheduleDescription
                        newEs.Effective = wq.ConvertTimeFromUtc(DateTime.UtcNow)
                        newEs.Expiration = orgExpiration
                        newEs.EndEdit()
                    End If
                End If
            Next
            If (anyUpdates) Then wq.Save()
        Next
    End Sub

    'Added manually to the code
    Public Overrides ReadOnly Property IsDirty As Boolean
        Get
            Return MyBase.IsDirty OrElse IsChildrenDirty
        End Get
    End Property

#Region "ScheduleDetail"
    Public ReadOnly Property ScheduleDetailChildren As ScheduleDetail.ScheduleDetailList
        Get
            Return New ScheduleDetail.ScheduleDetailList(Me)
        End Get
    End Property

    Public Class ScheduleDetailSelectionView
        Inherits DataView
        Public Const COL_NAME_SCHEDULE_DETAIL_ID As String = ScheduleDetailDAL.COL_NAME_SCHEDULE_DETAIL_ID
        Public Const COL_NAME_SCHEDULE_ID As String = ScheduleDetailDAL.COL_NAME_SCHEDULE_ID
        Public Const COL_NAME_FROM_TIME As String = ScheduleDetailDAL.COL_NAME_FROM_TIME
        Public Const COL_NAME_TO_TIME As String = ScheduleDetailDAL.COL_NAME_TO_TIME
        Public Const COL_NAME_DAY_OF_WEEK_ID As String = ScheduleDetailDAL.COL_NAME_DAY_OF_WEEK_ID

        Public Sub New(Table As DataTable)
            MyBase.New(Table)
        End Sub

        Public Shared Function CreateTable() As DataTable
            Dim t As New DataTable
            t.Columns.Add(COL_NAME_SCHEDULE_DETAIL_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_SCHEDULE_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_FROM_TIME, GetType(String))
            t.Columns.Add(COL_NAME_TO_TIME, GetType(String))
            t.Columns.Add(COL_NAME_DAY_OF_WEEK_ID, GetType(Byte()))
            Return t
        End Function
    End Class

    Public Function GetScheduleDetailSelectionView() As ScheduleDetailSelectionView
        Dim t As DataTable = ScheduleDetailSelectionView.CreateTable
        Dim detail As ScheduleDetail

        For Each detail In ScheduleDetailChildren
            Dim row As DataRow = t.NewRow
            row(ScheduleDetailSelectionView.COL_NAME_SCHEDULE_DETAIL_ID) = detail.Id.ToByteArray()
            row(ScheduleDetailSelectionView.COL_NAME_SCHEDULE_ID) = detail.ScheduleId.ToByteArray()
            row(ScheduleDetailSelectionView.COL_NAME_FROM_TIME) = detail.FromTime
            row(ScheduleDetailSelectionView.COL_NAME_TO_TIME) = detail.ToTime
            row(ScheduleDetailSelectionView.COL_NAME_DAY_OF_WEEK_ID) = detail.DayOfWeekId.ToByteArray
            t.Rows.Add(row)
        Next
        Return New ScheduleDetailSelectionView(t)
    End Function

    Public Function GetScheduleDetailChild(childId As Guid) As ScheduleDetail
        Return CType(ScheduleDetailChildren.GetChild(childId), ScheduleDetail)
    End Function

    Public Function GetNewScheduleDetailChild() As ScheduleDetail
        Dim newScheduleDetail As ScheduleDetail = CType(ScheduleDetailChildren.GetNewChild, ScheduleDetail)
        newScheduleDetail.ScheduleId = Id
        Return newScheduleDetail
    End Function
#End Region


#End Region



#Region "ScheduleSearchDV"

    Public Class ScheduleSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_NAME_SCHEDULE_ID As String = "schedule_id"
        Public Const COL_NAME_CODE As String = "code"
        Public Const COL_NAME_DESCRIPTION As String = "description"
#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

#End Region

#Region "DataView Retrieveing Methods"

    Public Shared Function GetSchedulesList(scheduleCode As String, scheduleDescription As String) As ScheduleSearchDV
        Try
            Dim dal As New ScheduleDAL
            Dim errors() As ValidationError = {New ValidationError(Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR, GetType(Schedule), Nothing, "Search", Nothing)}

            'Convert the scheduleCode to UPPER Case
            If (Not (scheduleCode.Equals(String.Empty))) Then
                scheduleCode = scheduleCode.ToUpper
            End If

            'Convert the scheduleDescription to UPPER Case
            If (Not (scheduleDescription.Equals(String.Empty))) Then
                scheduleDescription = scheduleDescription.ToUpper
            End If

            'Check if the user has entered any search criteria... if NOT, then display an error
            If (scheduleCode.Equals(String.Empty) AndAlso scheduleDescription.Equals(String.Empty)) Then
                Throw New BOValidationException(errors, GetType(Schedule).FullName)
            End If

            Return New ScheduleSearchDV(dal.GetSchedulesList(scheduleCode, scheduleDescription).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function


#End Region


#Region "Custom Validations"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ScheduleCodeValidator
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.DUPLICATE_SCHEDULE_CODE_ERR)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As Schedule = CType(objectToValidate, Schedule)
            Dim dal As New ScheduleDAL

            If (obj.Code IsNot Nothing) AndAlso (obj.Code.Trim <> String.Empty) Then

                If Not dal.IsScheduleCodeUnique(obj.Code, obj.Id) Then
                    Return False
                End If
            End If
            Return True

        End Function
    End Class
#End Region


End Class