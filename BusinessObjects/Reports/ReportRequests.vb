'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (5/18/2015)  ********************

Public Class ReportRequests
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
            Dim dal As New ReportRequestsDAL
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
            Dim dal As New ReportRequestsDAL
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
            If row(ReportRequestsDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ReportRequestsDAL.COL_NAME_REPORT_REQUEST_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=800)> _
    Public Property ReportType As String
        Get
            CheckDeleted()
            If row(ReportRequestsDAL.COL_NAME_REPORT_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ReportRequestsDAL.COL_NAME_REPORT_TYPE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ReportRequestsDAL.COL_NAME_REPORT_TYPE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=800)> _
    Public Property FtpFilename As String
        Get
            CheckDeleted()
            If row(ReportRequestsDAL.COL_NAME_FTP_FILENAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ReportRequestsDAL.COL_NAME_FTP_FILENAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ReportRequestsDAL.COL_NAME_FTP_FILENAME, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=4000)> _
    Public Property ReportParameters As String
        Get
            CheckDeleted()
            If row(ReportRequestsDAL.COL_NAME_REPORT_PARAMETERS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ReportRequestsDAL.COL_NAME_REPORT_PARAMETERS), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ReportRequestsDAL.COL_NAME_REPORT_PARAMETERS, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200)> _
    Public Property Status As String
        Get
            CheckDeleted()
            If row(ReportRequestsDAL.COL_NAME_STATUS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ReportRequestsDAL.COL_NAME_STATUS), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ReportRequestsDAL.COL_NAME_STATUS, Value)
        End Set
    End Property



    Public Property StartDate As DateType
        Get
            CheckDeleted()
            If row(ReportRequestsDAL.COL_NAME_START_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(ReportRequestsDAL.COL_NAME_START_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ReportRequestsDAL.COL_NAME_START_DATE, Value)
        End Set
    End Property



    Public Property EndDate As DateType
        Get
            CheckDeleted()
            If row(ReportRequestsDAL.COL_NAME_END_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(ReportRequestsDAL.COL_NAME_END_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ReportRequestsDAL.COL_NAME_END_DATE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=800)> _
    Public Property ErrorMessage As String
        Get
            CheckDeleted()
            If row(ReportRequestsDAL.COL_NAME_ERROR_MESSAGE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ReportRequestsDAL.COL_NAME_ERROR_MESSAGE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ReportRequestsDAL.COL_NAME_ERROR_MESSAGE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200)> _
    Public Property UserEmailAddress As String
        Get
            CheckDeleted()
            If row(ReportRequestsDAL.COL_NAME_USER_EMAIL_ADDRESS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ReportRequestsDAL.COL_NAME_USER_EMAIL_ADDRESS), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ReportRequestsDAL.COL_NAME_USER_EMAIL_ADDRESS, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=800)> _
    Public Property ReportProc As String
        Get
            CheckDeleted()
            If row(ReportRequestsDAL.COL_NAME_REPORT_PROC) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ReportRequestsDAL.COL_NAME_REPORT_PROC), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ReportRequestsDAL.COL_NAME_REPORT_PROC, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=800)> _
    Public Property Sourceurl As String
        Get
            CheckDeleted()
            If row(ReportRequestsDAL.COL_NAME_SOURCEURL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ReportRequestsDAL.COL_NAME_SOURCEURL), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ReportRequestsDAL.COL_NAME_SOURCEURL, Value)
        End Set
    End Property

    Public ReadOnly Property Requester As String
        Get
            Return ElitaPlusIdentity.Current.ActiveUser.NetworkId
        End Get
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ReportRequestsDAL
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

#End Region
    Public Sub CreateJob(ScheduledDate As DateTime)
        Try
            Dim dal As New ReportRequestsDAL
            dal.CreateJob(Id, ScheduledDate)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    Public Sub CreateReportRequest(ByVal ScheduledDate As DateTime)
        Try
            Dim dal As New ReportRequestsDAL
            dal.CreateReportRequest(ReportType, Requester, FtpFilename, ReportParameters, UserEmailAddress, ReportProc, ScheduledDate)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    Public Shared Function GetAccessCountByUser(ByVal userId As String) As Integer

        Try
            Dim dal As New ReportRequestsDAL

            Return dal.GetAccessCountByUser(userId)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetReportsByUser(ByVal userId As String) As DataTable

        Try
            Dim dal As New ReportRequestsDAL

            Return dal.GetReportsByUser(userId).Tables(0)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function
    Public Shared Function LoadRequestsByUser(ByVal userId As String, ByVal reportType As String) As DataTable

        Try
            Dim dal As New ReportRequestsDAL

            Return dal.LoadRequestsByUser(userId, reportType).Tables(0)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function
    Public Shared Function LoadRequestsByReportKey(ByVal userId As String, ByVal requestId As String) As DataTable

        Try
            Dim dal As New ReportRequestsDAL

            Return dal.LoadRequestsByReportKey(userId, requestId).Tables(0)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function LoadRequests(ByVal requestId As String, ByVal userId As String) As DataTable

        Try
            Dim dal As New ReportRequestsDAL

            Return dal.LoadRequests(requestId, userId).Tables(0)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Class ReportRequestsDV
        Inherits DataView

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

    Public Shared Function CheckExchaneRate(ByVal reprortingmonthyear As String,
                                            ByVal companyCode As String,
                                            ByVal dealerCode As String,
                                            ByVal groupid As String,
                                            ByVal dealerwithcurrency As String,
                                            ByVal currencyid As String) As String
        Try
            Dim dal As New ReportRequestsDAL

            Return dal.CheckExchangeRate(reprortingmonthyear, companyCode, dealerCode, groupid, dealerwithcurrency, currencyid)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetReportParams(ByVal reporttype As String) As DataTable

        Try
            Dim dal As New ReportRequestsDAL

            Return dal.LoadReportParams(reporttype).Tables(0)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function
End Class


