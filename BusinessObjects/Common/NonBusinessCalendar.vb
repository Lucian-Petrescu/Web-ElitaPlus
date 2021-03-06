'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (8/28/2007)  ********************

Public Class NonbusinessCalendar
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

    Public Sub New(ByVal companyGroupId As Guid, ByVal nonBusinessDate As String)
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load(companyGroupId, nonBusinessDate)
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

    Protected Sub Load(ByVal companyGroupId As Guid, ByVal nonBusinessDate As String)
        Try
            Dim dal As New NonBusinessCalendarDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            newRow(NonBusinessCalendarDAL.COL_NAME_COMPANY_GROUP_ID) = companyGroupId.ToByteArray
            newRow(NonBusinessCalendarDAL.COL_NAME_NONBUSINESS_DATE) = nonBusinessDate
            Me.Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub


    Protected Sub Load()
        Try
            Dim dal As New NonbusinessCalendarDAL
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
            Dim dal As New NonbusinessCalendarDAL
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
            If Row(NonBusinessCalendarDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(NonBusinessCalendarDAL.COL_NAME_NONBUSINESS_CALENDAR_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CompanyGroupId() As Guid
        Get
            CheckDeleted()
            If Row(NonBusinessCalendarDAL.COL_NAME_COMPANY_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(NonBusinessCalendarDAL.COL_NAME_COMPANY_GROUP_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(NonBusinessCalendarDAL.COL_NAME_COMPANY_GROUP_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property NonbusinessDate() As DateType
        Get
            CheckDeleted()
            If Row(NonBusinessCalendarDAL.COL_NAME_NONBUSINESS_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(NonBusinessCalendarDAL.COL_NAME_NONBUSINESS_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(NonBusinessCalendarDAL.COL_NAME_NONBUSINESS_DATE, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New NonBusinessCalendarDAL
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

    Public Shared Function LoadList(ByVal companyGroupID As Guid) As DataView
        Try
            Dim dal As New NonbusinessCalendarDAL
            Dim ds As DataSet

            ds = dal.LoadList(companyGroupID)
            Return (ds.Tables(NonbusinessCalendarDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetNonBusinessDaysCount(ByVal defaultFollowUp As Integer, ByVal companyGroupID As Guid) As Integer
        Dim ds As DataSet
        Dim nonBusinessCalendarDAL As New NonBusinessCalendarDAL

        ds = nonBusinessCalendarDAL.GetNonBusinessDaysCount(defaultFollowUp, companyGroupID)
        Return ds.Tables(nonBusinessCalendarDAL.TABLE_NAME).Rows(0).Item(nonBusinessCalendarDAL.COL_NAME_NONBUSINESS_DAY_COUNT)
    End Function

    Public Shared Function GetSameBusinessDaysCount(ByVal followupDate As Date, ByVal companyGroupID As Guid) As Integer
        Dim ds As DataSet
        Dim nonBusinessCalendarDAL As New NonBusinessCalendarDAL

        ds = nonBusinessCalendarDAL.GetSameBusinessDaysCount(followupDate, companyGroupID)
        Return ds.Tables(nonBusinessCalendarDAL.TABLE_NAME).Rows(0).Item(nonBusinessCalendarDAL.COL_NAME_SAMEBUSINESS_DAY_COUNT)
    End Function

    Public Shared Function GetNextBusinessDate(ByVal defaultFollowUp As Integer, ByVal companyGroupID As Guid) As Date
        Dim nonBusinessCalendarDAL As New NonBusinessCalendarDAL

        Return nonBusinessCalendarDAL.GetNextBusinessDate(defaultFollowUp, companyGroupID)
    End Function

    Public Shared Function GetNonBusinessDates(ByVal CompanyGroupCode As String, ByVal dtStart As Date, ByVal dtEnd As Date) As DataSet
        Dim ds As DataSet
        Dim nonBusinessCalendarDAL As New NonBusinessCalendarDAL
        ds = nonBusinessCalendarDAL.GetNonBusinessDates(CompanyGroupCode, dtStart, dtEnd)
        Return ds
    End Function
#End Region

#Region "DataView Retrieveing Methods"

#End Region

End Class


