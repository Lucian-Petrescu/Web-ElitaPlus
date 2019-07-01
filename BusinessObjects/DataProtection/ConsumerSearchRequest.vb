'Imports Assurant.Common.CrystalEnterpriseHelper

Public Class ConsumerSearchRequest
    Inherits BusinessObjectBase


#Region "Constants"
    Private Const NO_INSTANCE As Long = -1


#End Region
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
            Dim dal As New ReportRequestsDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
    'Initialization code for new objects
    Private Sub Initialize()
    End Sub
    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New ReportRequestsDAL
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



#Region "Page Parameters"

    Public Enum RptFormat
        JAVA
        PDF
        TEXT_CSV
        TEXT_TAB
    End Enum

    Public Enum RptAction
        SCHEDULE
        SCHEDULE_VIEW
        VIEW
    End Enum



    Public Class CsvSeparator
        Public Const CSV_SEPARATOR_0166 As String = "¦" ' 
        Public Const CSV_SEPARATOR_COMMA As String = "," ' ,
    End Class

    Public Class CsvDelimiter
        Public Shared ReadOnly CSV_DELIMITER_NONE As String = String.Empty
        Public Shared ReadOnly CSV_DELIMITER_DQUOTE As String = """"
    End Class


#Region "Report Parameter Class"

    'Public Class RptParam
    '    Public moCeHelperParameter As ceHelperParameter

    '    Public Sub New(ByVal parmName As String, ByVal parmValue As String,
    '        Optional ByVal parmSubReportName As String = ceHelperParameter.EMPTY_CEPARAM)
    '        moCeHelperParameter = New ceHelperParameter(parmName, parmValue, parmSubReportName)
    '    End Sub

    'End Class
#End Region

    Public Class Params
        Public msRptName As String
        Public msRptWindowName As String
        Public moRptFormat As RptFormat
        Public moAction As RptAction
        Public instanceId As Long
        'Public moRptParams() As RptParam
        Public msCsvDelimiter As String
        Public msCsvSeparator As String
        'Public moDest As ceDestination
        'Public moSched As ceSchedule

        Public Sub New()
            ' Default Values
            instanceId = NO_INSTANCE
            msCsvDelimiter = CsvDelimiter.CSV_DELIMITER_NONE
            msCsvSeparator = CsvSeparator.CSV_SEPARATOR_0166
        End Sub

    End Class

#End Region


#Region "Variables"
    'Private moError As SSHelper.RptError

#End Region

#Region " Properties "
    'Public Property Status() As SSHelper.RptStatus
    '    Get
    '        Return moError.status
    '    End Get
    '    Set(ByVal Value As SSHelper.RptStatus)
    '        moError.status = Value
    '    End Set
    'End Property

    'Public Property ErrorMsg() As String
    '    Get
    '        Return moError.msg
    '    End Get
    '    Set(ByVal Value As String)
    '        moError.msg = Value
    '    End Set
    'End Property

#End Region
    <ValueMandatory(""), ValidStringLength("", Max:=800)>
    Public Property ReportType() As String
        Get
            CheckDeleted()
            If Row(ReportRequestsDAL.COL_NAME_REPORT_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ReportRequestsDAL.COL_NAME_REPORT_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ReportRequestsDAL.COL_NAME_REPORT_TYPE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=800)>
    Public Property ReportProc() As String
        Get
            CheckDeleted()
            If Row(ReportRequestsDAL.COL_NAME_REPORT_PROC) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ReportRequestsDAL.COL_NAME_REPORT_PROC), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ReportRequestsDAL.COL_NAME_REPORT_PROC, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=4000)>
    Public Property ReportParameters() As String
        Get
            CheckDeleted()
            If Row(ReportRequestsDAL.COL_NAME_REPORT_PARAMETERS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ReportRequestsDAL.COL_NAME_REPORT_PARAMETERS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ReportRequestsDAL.COL_NAME_REPORT_PARAMETERS, Value)
        End Set
    End Property

    Protected Sub CheckDeleted()
        If Me.IsDeleted Then
            Throw New BOInvalidOperationException(Common.ErrorCodes.BO_IS_DELETED)
        End If
    End Sub


    <ValidStringLength("", Max:=200)>
    Public Property UserEmailAddress() As String
        Get
            CheckDeleted()
            If Row(ReportRequestsDAL.COL_NAME_USER_EMAIL_ADDRESS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ReportRequestsDAL.COL_NAME_USER_EMAIL_ADDRESS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ReportRequestsDAL.COL_NAME_USER_EMAIL_ADDRESS, Value)
        End Set
    End Property
    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If Row(ReportRequestsDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ReportRequestsDAL.COL_NAME_REPORT_REQUEST_ID), Byte()))
            End If
        End Get
    End Property

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New ReportRequestsDAL
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
    Public Sub CreateJob(ScheduledDate As DateTime)
        Try
            Dim dal As New ReportRequestsDAL
            dal.CreateJob(Id, ScheduledDate)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
End Class
