'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (9/24/2009)  ********************

Public Class Webservices
    Inherits BusinessObjectBase


#Region "Constants"
    Public Const COL_NAME_WEB_SERVICE_NAME = WebServicesDAL.COL_NAME_WEB_SERVICE_NAME
    Public Const COL_NAME_WEBSERVICE_ID = WebServicesDAL.COL_NAME_WEBSERVICE_ID
#End Region
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
            Dim dal As New WebservicesDAL

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
            Dim dal As New WebservicesDAL
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
            If row(WebservicesDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(WebservicesDAL.COL_NAME_WEBSERVICE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=120)> _
    Public Property WebServiceName As String
        Get
            CheckDeleted()
            If row(WebservicesDAL.COL_NAME_WEB_SERVICE_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(WebservicesDAL.COL_NAME_WEB_SERVICE_NAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(WebservicesDAL.COL_NAME_WEB_SERVICE_NAME, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property OnLineId As Guid
        Get
            CheckDeleted()
            If row(WebservicesDAL.COL_NAME_ON_LINE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(WebservicesDAL.COL_NAME_ON_LINE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(WebservicesDAL.COL_NAME_ON_LINE_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=800)> _
    Public Property OffLineMessage As String
        Get
            CheckDeleted()
            If row(WebservicesDAL.COL_NAME_OFF_LINE_MESSAGE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(WebservicesDAL.COL_NAME_OFF_LINE_MESSAGE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(WebservicesDAL.COL_NAME_OFF_LINE_MESSAGE, Value)
        End Set
    End Property

    Public Property LastOperationDate As DateType
        Get
            CheckDeleted()
            If Row(WebServicesDAL.COL_NAME_LAST_OPERATION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(WebServicesDAL.COL_NAME_LAST_OPERATION_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(WebServicesDAL.COL_NAME_LAST_OPERATION_DATE, Value)
        End Set
    End Property


#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New WebservicesDAL
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

    Public Shared Function GetNewDataViewRow(dv As DataView, id As Guid) As DataView

        Dim dt As DataTable
        dt = dv.Table

        Dim row As DataRow = dt.NewRow

        row(WebServicesDAL.COL_NAME_WEB_SERVICE_NAME) = String.Empty
        row(WebServicesDAL.COL_NAME_WEBSERVICE_ID) = id.ToByteArray
        row(WebServicesDAL.COL_NAME_OFF_LINE_MESSAGE) = String.Empty
        row(WebServicesDAL.COL_NAME_ON_LINE_ID) = (Guid.Empty).ToByteArray

        dt.Rows.Add(row)

        Return (dv)

    End Function
#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function GetWebServices(web_service_name As String, on_line_id As Guid) As WebServicesSearchDV

        Try
            Dim dal As New WebservicesDAL
            Return New WebServicesSearchDV(dal.LoadList(web_service_name, on_line_id).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try


    End Function


    Public Shared Sub WebUserLog(Web_UserLog_id As Guid, Url As String, functionToProcess As String,
                                          networkId As String, Environment As String, Hub As String,
                                          _xml As String, Created_date As Date)
        Try
            Dim Dal As New WebServicesDAL
            Dal.WebUserLog(Web_UserLog_id, Url, functionToProcess, networkId, Environment, Hub, _xml, Created_date)
        Catch ex As Exception
            ex = Nothing
        End Try
    End Sub


    Public Class WebServicesSearchDV
        Inherits DataView

#Region "Constants"

        Public Const COL_WEBSERVICE_ID As String = "webservice_id"
        Public Const COL_WEB_SERVICE_NAME As String = "web_service_name"
        Public Const COL_ON_LINE As String = "on_line"
        Public Const COL_OFF_LINE_MESSAGE As String = "off_line_message"
        Public Const COL_LAST_CHANGED_BY_BY As String = "last_change_by"
        Public Const COL_LAST_OPERATION_DATE As String = "last_operation_date"

#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region

End Class

