Public Class OcMessageParams
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
            Dim dal As New OcMessageParamsDAL
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

    Protected Sub Load(id As Guid)
        Try
            Dim dal As New OcMessageParamsDAL
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
            If Row(OcMessageParamsDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(OcMessageParamsDAL.COL_NAME_OC_MESSAGE_PARAMS_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")>
    Public Property OcMessageId As Guid
        Get
            CheckDeleted()
            If Row(OcMessageParamsDAL.COL_NAME_OC_MESSAGE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(OcMessageParamsDAL.COL_NAME_OC_MESSAGE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(OcMessageParamsDAL.COL_NAME_OC_MESSAGE_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=600)>
    Public Property ParamName As String
        Get
            CheckDeleted()
            If Row(OcMessageParamsDAL.COL_NAME_PARAM_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(OcMessageParamsDAL.COL_NAME_PARAM_NAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(OcMessageParamsDAL.COL_NAME_PARAM_NAME, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=400)>
    Public Property ParamValue As String
        Get
            CheckDeleted()
            If Row(OcMessageParamsDAL.COL_NAME_PARAM_VALUE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(OcMessageParamsDAL.COL_NAME_PARAM_VALUE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(OcMessageParamsDAL.COL_NAME_PARAM_VALUE, Value)
        End Set
    End Property

#End Region

#Region "Public Members"

#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function GetList(messageId As Guid) As DataView
        Try
            Dim dal As New OcMessageParamsDAL
            Return New DataView(dal.LoadList(messageId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#Region "MessageParamsDV"
    Public Class MessageParamsDV
        Inherits DataView

#Region "Constants"
        Public Const COL_OC_MESSAGE_PARAMS_ID As String = "OC_MESSAGE_PARAMS_ID"
        Public Const COL_OC_MESSAGE_ID As String = "OC_MESSAGE_ID"
        Public Const COL_PARAM_NAME As String = "PARAM_NAME"
        Public Const COL_PARAM_VALUE As String = "PARAM_VALUE"
        Public Const COL_CREATED_BY As String = "CREATED_BY"
        Public Const COL_CREATED_DATE As String = "CREATED_DATE"
        Public Const COL_MODIFIED_BY As String = "MODIFIED_BY"
        Public Const COL_MODIFIED_DATE As String = "MODIFIED_DATE"
#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

        Public Function AddNewRowToEmptyDV() As MessageParamsDV
            Dim dt As DataTable = Table.Clone()
            Dim row As DataRow = dt.NewRow
            row(MessageParamsDV.COL_OC_MESSAGE_PARAMS_ID) = (New Guid()).ToByteArray
            row(MessageParamsDV.COL_OC_MESSAGE_ID) = Guid.Empty.ToByteArray
            row(MessageParamsDV.COL_PARAM_NAME) = DBNull.Value
            row(MessageParamsDV.COL_PARAM_VALUE) = DBNull.Value
            row(MessageParamsDV.COL_CREATED_BY) = DBNull.Value
            row(MessageParamsDV.COL_CREATED_DATE) = DBNull.Value
            row(MessageParamsDV.COL_MODIFIED_BY) = DBNull.Value
            row(MessageParamsDV.COL_MODIFIED_DATE) = DBNull.Value
            dt.Rows.Add(row)
            Return New MessageParamsDV(dt)
        End Function
    End Class
#End Region
#End Region

End Class
