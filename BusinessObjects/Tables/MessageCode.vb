'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (10/29/2008)  ********************

Public Class MessageCode
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
            Dim dal As New MessageCodeDAL
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
            Dim dal As New MessageCodeDAL
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
        MsgParameterCount = 0
    End Sub
#End Region

#Region "Constants"
    Public Const Err_MSG_CODE_EXISTS As String = "MSG_CODE_EXISTS"
#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If row(MessageCodeDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(MessageCodeDAL.COL_NAME_MSG_CODE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property MsgType() As Guid
        Get
            CheckDeleted()
            If row(MessageCodeDAL.COL_NAME_MSG_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(MessageCodeDAL.COL_NAME_MSG_TYPE), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(MessageCodeDAL.COL_NAME_MSG_TYPE, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=20)> _
    Public Property MsgCode() As String
        Get
            CheckDeleted()
            If Row(MessageCodeDAL.COL_NAME_MSG_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(MessageCodeDAL.COL_NAME_MSG_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(MessageCodeDAL.COL_NAME_MSG_CODE, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property LabelId() As Guid
        Get
            CheckDeleted()
            If row(MessageCodeDAL.COL_NAME_LABEL_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(MessageCodeDAL.COL_NAME_LABEL_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(MessageCodeDAL.COL_NAME_LABEL_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property MsgParameterCount() As LongType
        Get
            CheckDeleted()
            If row(MessageCodeDAL.COL_NAME_MSG_PARAMETER_COUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(MessageCodeDAL.COL_NAME_MSG_PARAMETER_COUNT), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            SetValue(MessageCodeDAL.COL_NAME_MSG_PARAMETER_COUNT, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New MessageCodeDAL
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

    Public Shared Function IsNewMsgCode(ByVal strMsgCode As String, ByVal strMsgType As String, ByVal strUIProgCode As String) As Boolean
        Dim blnResult As Boolean = True, dal As New MessageCodeDAL
        Dim ds As DataSet = dal.getExistingMSGCode(strMsgCode, strMsgType, strUIProgCode)
        If Not ds Is Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
            blnResult = False
        End If
        Return blnResult
    End Function

    Public Shared Function GetMsgIdFromMsgCode(ByVal strMsgCode As String, ByVal strMsgType As String) As Guid
        Dim dal As New MessageCodeDAL, guidID As Guid
        Dim ds As DataSet = dal.loadMsgIdFromMsgCode(strMsgCode, strMsgType)
        guidID = Guid.Empty
        If Not ds Is Nothing AndAlso ds.Tables(0).Rows.Count > 0 Then
            guidID = New Guid(CType(ds.Tables(0).Rows(0).Item(0), Byte()))
        End If
        Return guidID
    End Function
#End Region

#Region "DataView Retrieveing Methods"
    Public Class MessageCodeSearchDV
        Inherits DataView

        Public Const COL_MSG_CODE_ID As String = "MSG_CODE_ID"
        Public Const COL_MSG_TYPE As String = "MSG_TYPE"
        Public Const COL_MSG_CODE As String = "MSG_CODE"
        Public Const COL_LABEL_ID As String = "LABEL_ID"
        Public Const COL_MSG_PARAMETER_COUNT As String = "MSG_PARAMETER_COUNT"
        Public Const COL_UI_PROG_CODE As String = "UI_PROG_CODE"

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

    Public Shared Sub AddNewRowToMSGCodeSearchDV(ByRef dv As MessageCodeSearchDV, ByVal NewMSGCodeBO As MessageCode)
        Dim dt As DataTable, blnEmptyTbl As Boolean = False

        If NewMSGCodeBO.IsNew Then
            Dim row As DataRow
            If dv Is Nothing Then
                Dim guidTemp As New Guid
                blnEmptyTbl = True
                dt = New DataTable
                dt.Columns.Add(MessageCodeSearchDV.COL_MSG_CODE_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(MessageCodeSearchDV.COL_MSG_TYPE, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(MessageCodeSearchDV.COL_MSG_CODE, GetType(String))
                dt.Columns.Add(MessageCodeSearchDV.COL_LABEL_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(MessageCodeSearchDV.COL_MSG_PARAMETER_COUNT, GetType(Integer))
                dt.Columns.Add(MessageCodeSearchDV.COL_UI_PROG_CODE, GetType(String))
            Else
                dt = dv.Table
            End If
            row = dt.NewRow
            row(MessageCodeSearchDV.COL_MSG_CODE_ID) = NewMSGCodeBO.Id.ToByteArray
            row(MessageCodeSearchDV.COL_MSG_TYPE) = NewMSGCodeBO.MsgType.ToByteArray
            row(MessageCodeSearchDV.COL_MSG_CODE) = NewMSGCodeBO.MsgCode
            row(MessageCodeSearchDV.COL_LABEL_ID) = NewMSGCodeBO.LabelId.ToByteArray
            row(MessageCodeSearchDV.COL_MSG_PARAMETER_COUNT) = NewMSGCodeBO.MsgParameterCount.Value
            row(MessageCodeSearchDV.COL_UI_PROG_CODE) = ""
            dt.Rows.Add(row)
            If blnEmptyTbl Then dv = New MessageCodeSearchDV(dt)
        End If
    End Sub

    Public Shared Function getList(ByVal guidMsgType As Guid, ByVal strMsgCode As String, ByVal strUIProgCode As String) As MessageCodeSearchDV
        Try
            Dim dal As New MessageCodeDAL
            Return New MessageCodeSearchDV(dal.LoadList(guidMsgType, strMsgCode, strUIProgCode).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

End Class


