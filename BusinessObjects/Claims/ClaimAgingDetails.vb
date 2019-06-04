Public Class ClaimAgingDetails
    Inherits BusinessObjectBase

#Region "Constructors"

    'Existing BO
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

    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Me.Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New ClaimAgingDetailsDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New ClaimAgingDetailsDAL
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

#Region "Public Members"

#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function Load_List(ByVal claim_id As Guid, ByVal language_Id As Guid) As ClaimAgingDetailsDV
        Try
            Dim dal As New ClaimAgingDetailsDAL
            Return New ClaimAgingDetailsDV(dal.LoadList(claim_id, language_Id).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function Load_List(ByVal claim_id As Guid) As ClaimAgingDetailsDV
        Try
            Dim dal As New ClaimAgingDetailsDAL
            Return New ClaimAgingDetailsDV(dal.LoadList(claim_id).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

    Public Class ClaimAgingDetailsDV
        Inherits DataView

        Public Const COL_CLAIM_STAGE_ID As String = "claim_stage_id"
        Public Const COL_STAGE_NAME As String = "stage_name"
        Public Const COL_STAGE_NAME_ID As String = "stage_name_Id"
        Public Const COL_AGING_START_STATUS As String = "aging_start_status"
        Public Const COL_AGING_START_STATUS_ID As String = "aging_start_status_id"
        Public Const COL_AGING_START_DATETIME As String = "aging_start_datetime"
        Public Const COL_AGING_END_STATUS As String = "aging_end_status"
        Public Const COL_AGING_END_STATUS_ID As String = "aging_end_status_id"
        Public Const COL_AGING_END_DATETIME As String = "aging_end_datetime"
        Public Const COL_AGING_DAYS As String = "aging_days"
        Public Const COL_AGING_HOURS As String = "aging_hours"
        Public Const COL_AGING_SINCE_CLAIM_DAYS As String = "aging_since_claim_days"
        Public Const COL_AGING_SINCE_CLAIM_HOURS As String = "aging_since_claim_hours"

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property AgingStartDateTime(ByVal row As DataRow) As String
            Get
                If (row(COL_AGING_START_DATETIME) Is DBNull.Value) Then
                    Return String.Empty
                End If

                Return DateHelper.GetDateValue(row(COL_AGING_START_DATETIME).ToString()).ToString("dd-MMM-yyyy hh:mm:ss tt", System.Threading.Thread.CurrentThread.CurrentCulture)
            End Get
        End Property

        Public Shared ReadOnly Property AgingEndDateTime(ByVal row As DataRow) As String
            Get
                If (row(COL_AGING_END_DATETIME) Is DBNull.Value) Then
                    Return String.Empty
                End If
                Return DateHelper.GetDateValue(row(COL_AGING_END_DATETIME).ToString()).ToString("dd-MMM-yyyy hh:mm:ss tt", System.Threading.Thread.CurrentThread.CurrentCulture)
            End Get
        End Property

        Public Shared ReadOnly Property AgingDays(ByVal row As DataRow) As String
            Get
                If (row(COL_AGING_DAYS) Is DBNull.Value) Then
                    Return "0 Day(s)"
                End If

                Return row(COL_AGING_DAYS).ToString() + " Day(s)"
            End Get
        End Property

        Public Shared ReadOnly Property AgingHours(ByVal row As DataRow) As String
            Get
                If (row(COL_AGING_HOURS) Is DBNull.Value) Then
                    Return "0 Hour(s)"
                End If

                Return row(COL_AGING_HOURS).ToString() + " Hour(s)"
            End Get
        End Property

        Public Shared ReadOnly Property AgingClaimDays(ByVal row As DataRow) As String
            Get
                If (row(COL_AGING_SINCE_CLAIM_DAYS) Is DBNull.Value) Then
                    Return "0 Day(s)"
                End If

                Return row(COL_AGING_SINCE_CLAIM_DAYS).ToString() + " Day(s)"
            End Get
        End Property

        Public Shared ReadOnly Property AgingClaimHours(ByVal row As DataRow) As String
            Get
                If (row(COL_AGING_SINCE_CLAIM_HOURS) Is DBNull.Value) Then
                    Return "0 Hour(s)"
                End If

                Return row(COL_AGING_SINCE_CLAIM_HOURS).ToString() + " Hour(s)"
            End Get
        End Property

    End Class

    Public Shared Sub AddNewRowToSearchDV(ByRef dv As ClaimAgingDetailsDV)
        Dim dt As DataTable, blnEmptyTbl As Boolean = False

        Dim row As DataRow
        If dv Is Nothing Then
            Dim guidTemp As New Guid
            blnEmptyTbl = True
            dt = New DataTable
            dt.Columns.Add(ClaimAgingDetailsDV.COL_CLAIM_STAGE_ID, guidTemp.ToByteArray.GetType)
            dt.Columns.Add(ClaimAgingDetailsDV.COL_STAGE_NAME, GetType(String))
            dt.Columns.Add(ClaimAgingDetailsDV.COL_AGING_START_STATUS, GetType(String))
            dt.Columns.Add(ClaimAgingDetailsDV.COL_AGING_START_DATETIME, GetType(DateTime))
            dt.Columns.Add(ClaimAgingDetailsDV.COL_AGING_END_STATUS, GetType(String))
            dt.Columns.Add(ClaimAgingDetailsDV.COL_AGING_END_DATETIME, GetType(DateTime))
            dt.Columns.Add(ClaimAgingDetailsDV.COL_AGING_DAYS, GetType(Integer))
            dt.Columns.Add(ClaimAgingDetailsDV.COL_AGING_HOURS, GetType(Integer))
            dt.Columns.Add(ClaimAgingDetailsDV.COL_AGING_SINCE_CLAIM_DAYS, GetType(Integer))
            dt.Columns.Add(ClaimAgingDetailsDV.COL_AGING_SINCE_CLAIM_HOURS, GetType(Integer))
        Else
            dt = dv.Table
        End If

        row = dt.NewRow
        row(ClaimAgingDetailsDV.COL_CLAIM_STAGE_ID) = Guid.Empty.ToByteArray
        row(ClaimAgingDetailsDV.COL_STAGE_NAME) = String.Empty
        row(ClaimAgingDetailsDV.COL_AGING_START_STATUS) = String.Empty
        'row(ClaimAgingDetailsDV.COL_AGING_START_DATETIME) = NewBO.CountryId.ToByteArray
        row(ClaimAgingDetailsDV.COL_AGING_END_STATUS) = String.Empty
        'row(ClaimAgingDetailsDV.COL_AGING_END_DATETIME) = NewBO.ProductCode
        row(ClaimAgingDetailsDV.COL_AGING_DAYS) = 0
        row(ClaimAgingDetailsDV.COL_AGING_HOURS) = 0
        row(ClaimAgingDetailsDV.COL_AGING_SINCE_CLAIM_DAYS) = 0
        row(ClaimAgingDetailsDV.COL_AGING_SINCE_CLAIM_HOURS) = 0

        dt.Rows.Add(row)
        If blnEmptyTbl Then dv = New ClaimAgingDetailsDV(dt)

    End Sub

End Class
