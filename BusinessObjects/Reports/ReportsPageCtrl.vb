
'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (8/9/2010)  ********************

Public Class ReportsPageCtrl
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
            Dim dal As New ReportsPagectrlDAL
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
            Dim dal As New ReportsPagectrlDAL
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
    Public ReadOnly Property Id() As Guid
        Get
            If Row(ReportsPagectrlDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ReportsPagectrlDAL.COL_NAME_PAGECTRL_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=800)> _
    Public Property ReportName() As String
        Get
            CheckDeleted()
            If Row(ReportsPagectrlDAL.COL_NAME_REPORT_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ReportsPagectrlDAL.COL_NAME_REPORT_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ReportsPagectrlDAL.COL_NAME_REPORT_NAME, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=48)> _
    Public Property PeriodGenerated() As String
        Get
            CheckDeleted()
            If Row(ReportsPagectrlDAL.COL_NAME_PERIOD_GENERATED) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ReportsPagectrlDAL.COL_NAME_PERIOD_GENERATED), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ReportsPagectrlDAL.COL_NAME_PERIOD_GENERATED, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property LastPagenum() As DecimalType
        Get
            CheckDeleted()
            If Row(ReportsPagectrlDAL.COL_NAME_LAST_PAGENUM) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(ReportsPagectrlDAL.COL_NAME_LAST_PAGENUM), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            SetValue(ReportsPagectrlDAL.COL_NAME_LAST_PAGENUM, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=200)> _
    Public Property Status() As String
        Get
            CheckDeleted()
            If Row(ReportsPagectrlDAL.COL_NAME_STATUS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ReportsPagectrlDAL.COL_NAME_STATUS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(ReportsPagectrlDAL.COL_NAME_STATUS, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property CompanyId() As Guid
        Get
            CheckDeleted()
            If Row(ReportsPagectrlDAL.COL_NAME_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ReportsPagectrlDAL.COL_NAME_COMPANY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(ReportsPagectrlDAL.COL_NAME_COMPANY_ID, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ReportsPagectrlDAL
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

    Public Function GetRptRunDateAndPageNum(ByVal RptName As String, ByVal CompanyId As Guid) As DataSet
        Dim ds As DataSet = Nothing
        Dim dal As New ReportsPagectrlDAL
        Try
            ds = dal.GetRptRunDateAndPageNum(RptName, CompanyId)
            Return ds
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Function GetRunningPeriodDetails(ByVal RptName As String, ByVal CompanyId As Guid) As DataSet
        Dim ds As DataSet = Nothing
        Dim dal As New ReportsPagectrlDAL
        Try
            ds = dal.GetRunningPeriod(RptName, CompanyId)
            Return ds
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Function ChkRptRunningForPeriod(ByVal RptName As String, ByVal ReportPeriod As String, ByVal CompanyId As Guid) As Boolean
        Dim ds As DataSet = Nothing
        Dim dal As New ReportsPagectrlDAL
        Try
            ds = dal.GetRptStatusForAPeriod(RptName, ReportPeriod, CompanyId)
            If ds.Tables(dal.TABLE_NAME).Rows.Count > 0 Then
                If ds.Tables(dal.TABLE_NAME).Rows(0)(dal.COL_NAME_STATUS).ToString() = dal.STATUS_RUNNING Then
                    Return True
                End If
            End If
            Return False
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Function ChkPeriodIsOpen(ByVal ReportPeriod As String, ByVal CompanyId As Guid) As Boolean
        Dim dal As New ReportsPageCtrlDAL
        Dim ClosingDate As Date = Date.Today
        Dim BeginDate As Date

        Try
            Dim ReturnVal As String
            Dim ds As DataSet = Nothing
            ClosingDate = New Date(Int16.Parse(ReportPeriod.Substring(2, 4)), Int16.Parse(ReportPeriod.Substring(0, 2)), 1)
            ds = dal.GetAccountingStartDate(CompanyId:=CompanyId, ClosingDate:=ClosingDate)

            If ds.Tables(dal.TABLE_NAME).Rows.Count > 0 Then
                ReturnVal = ds.Tables(dal.TABLE_NAME).Rows(0)(dal.COL_NAME_BEGIN_DATE).ToString()
                BeginDate = New Date(Int16.Parse(ReturnVal.Substring(0, 4)), Int16.Parse(ReturnVal.Substring(4, 2)), Int16.Parse(ReturnVal.Substring(6, 2)))
                ds = Nothing
                ds = dal.GetAccountingCloseDate(CompanyId:=CompanyId, BeginDate:=BeginDate)
                If ds.Tables(dal.TABLE_NAME).Rows.Count > 0 Then
                    ReturnVal = ds.Tables(dal.TABLE_NAME).Rows(0)(dal.COL_NAME_CLOSING_DATE).ToString()
                    ClosingDate = New Date(Int16.Parse(ReturnVal.Substring(0, 4)), Int16.Parse(ReturnVal.Substring(4, 2)), Int16.Parse(ReturnVal.Substring(6, 2)))
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

        If (Date.Today <= ClosingDate) Then
            Return True
        End If

        Return False
    End Function

#End Region

#Region "DataView Retrieveing Methods"

#End Region

End Class


