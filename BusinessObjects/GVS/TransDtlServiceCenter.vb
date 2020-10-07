'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (11/12/2008)  ********************

Public Class TransDtlServiceCenter
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
            Dim dal As New TransDtlServiceCenterDAL
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
            Dim dal As New TransDtlServiceCenterDAL
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
            If row(TransDtlServiceCenterDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(TransDtlServiceCenterDAL.COL_NAME_TRANS_DTL_SERVICE_CENTER_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property TransactionLogHeaderId As Guid
        Get
            CheckDeleted()
            If row(TransDtlServiceCenterDAL.COL_NAME_TRANSACTION_LOG_HEADER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(TransDtlServiceCenterDAL.COL_NAME_TRANSACTION_LOG_HEADER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransDtlServiceCenterDAL.COL_NAME_TRANSACTION_LOG_HEADER_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ItemNumber As LongType
        Get
            CheckDeleted()
            If row(TransDtlServiceCenterDAL.COL_NAME_ITEM_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(TransDtlServiceCenterDAL.COL_NAME_ITEM_NUMBER), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransDtlServiceCenterDAL.COL_NAME_ITEM_NUMBER, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=40)> _
    Public Property Response As String
        Get
            CheckDeleted()
            If row(TransDtlServiceCenterDAL.COL_NAME_RESPONSE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransDtlServiceCenterDAL.COL_NAME_RESPONSE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransDtlServiceCenterDAL.COL_NAME_RESPONSE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=800)> _
    Public Property ResponseDetail As String
        Get
            CheckDeleted()
            If row(TransDtlServiceCenterDAL.COL_NAME_RESPONSE_DETAIL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransDtlServiceCenterDAL.COL_NAME_RESPONSE_DETAIL), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransDtlServiceCenterDAL.COL_NAME_RESPONSE_DETAIL, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=40)> _
    Public Property XmlServiceCenterCode As String
        Get
            CheckDeleted()
            If row(TransDtlServiceCenterDAL.COL_NAME_XML_SERVICE_CENTER_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransDtlServiceCenterDAL.COL_NAME_XML_SERVICE_CENTER_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransDtlServiceCenterDAL.COL_NAME_XML_SERVICE_CENTER_CODE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=200)> _
    Public Property XmlDescription As String
        Get
            CheckDeleted()
            If row(TransDtlServiceCenterDAL.COL_NAME_XML_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransDtlServiceCenterDAL.COL_NAME_XML_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransDtlServiceCenterDAL.COL_NAME_XML_DESCRIPTION, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=320)> _
    Public Property XmlTaxId As String
        Get
            CheckDeleted()
            If row(TransDtlServiceCenterDAL.COL_NAME_XML_TAX_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransDtlServiceCenterDAL.COL_NAME_XML_TAX_ID), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransDtlServiceCenterDAL.COL_NAME_XML_TAX_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=16)> _
    Public Property XmlStatusCode As String
        Get
            CheckDeleted()
            If row(TransDtlServiceCenterDAL.COL_NAME_XML_STATUS_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransDtlServiceCenterDAL.COL_NAME_XML_STATUS_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransDtlServiceCenterDAL.COL_NAME_XML_STATUS_CODE, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New TransDtlServiceCenterDAL
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

End Class


