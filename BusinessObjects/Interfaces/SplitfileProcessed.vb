'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (12/29/2005)  ********************

Public Class SplitfileProcessed
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
            Dim dal As New SplitfileProcessedDAL
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
            Dim dal As New SplitfileProcessedDAL
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
            If row(SplitfileProcessedDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(SplitfileProcessedDAL.COL_NAME_SPLITFILE_PROCESSED_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property SplitSystemId As Guid
        Get
            CheckDeleted()
            If row(SplitfileProcessedDAL.COL_NAME_SPLIT_SYSTEM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(SplitfileProcessedDAL.COL_NAME_SPLIT_SYSTEM_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SplitfileProcessedDAL.COL_NAME_SPLIT_SYSTEM_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=20)> _
    Public Property Filename As String
        Get
            CheckDeleted()
            If row(SplitfileProcessedDAL.COL_NAME_FILENAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(SplitfileProcessedDAL.COL_NAME_FILENAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SplitfileProcessedDAL.COL_NAME_FILENAME, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1)> _
    Public Property ProcessFlag As String
        Get
            CheckDeleted()
            If row(SplitfileProcessedDAL.COL_NAME_PROCESS_FLAG) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(SplitfileProcessedDAL.COL_NAME_PROCESS_FLAG), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SplitfileProcessedDAL.COL_NAME_PROCESS_FLAG, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property Received As LongType
        Get
            CheckDeleted()
            If row(SplitfileProcessedDAL.COL_NAME_RECEIVED) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(SplitfileProcessedDAL.COL_NAME_RECEIVED), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SplitfileProcessedDAL.COL_NAME_RECEIVED, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property Counted As LongType
        Get
            CheckDeleted()
            If row(SplitfileProcessedDAL.COL_NAME_COUNTED) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(SplitfileProcessedDAL.COL_NAME_COUNTED), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SplitfileProcessedDAL.COL_NAME_COUNTED, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property Split As LongType
        Get
            CheckDeleted()
            If row(SplitfileProcessedDAL.COL_NAME_SPLIT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(SplitfileProcessedDAL.COL_NAME_SPLIT), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SplitfileProcessedDAL.COL_NAME_SPLIT, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New SplitfileProcessedDAL
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

    Public Shared Function LoadList(ByVal oData As Object) As DataView
        Try
            Dim oSplitFileProcessedData As SplitfileProcessed = CType(oData, SplitfileProcessed)
            Dim dal As New SplitfileProcessedDAL
            Dim ds As DataSet

            ds = dal.LoadList(oSplitFileProcessedData.SplitSystemId)
            Return (ds.Tables(SplitfileProcessedDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function LoadTotalRecordByFile(ByVal SplitfileProcessedId As Guid) As DataView
        Try
            Dim dal As New SplitfileProcessedDAL
            Dim ds As Dataset

            ds = dal.LoadTotalRecordsByFile(SplitfileProcessedId)
            Return (ds.Tables(SplitfileProcessedDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

#End Region

#Region "StoreProcedures Control"

    Public Shared Sub SplitFile(ByVal oData As Object)
        Try
            Dim oSplitFileProcessedData As SplitFileProcessedData = CType(oData, SplitFileProcessedData)
            Dim dal As New SplitfileProcessedDAL

            oSplitFileProcessedData.interfaceStatus_id = InterfaceStatusWrk.CreateInterfaceStatus(InterfaceStatusWrkDAL.DESC_SPLIT)
            dal.SplitFile(oSplitFileProcessedData)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Sub

    Public Shared Sub DeleteFile(ByVal oData As Object)
        Try
            Dim oSplitFileProcessedData As SplitFileProcessedData = CType(oData, SplitFileProcessedData)
            Dim dal As New SplitfileProcessedDAL

            oSplitFileProcessedData.interfaceStatus_id = InterfaceStatusWrk.CreateInterfaceStatus(InterfaceStatusWrkDAL.DESC_DELETE)
            dal.DeleteFile(oSplitFileProcessedData)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Sub

#End Region

End Class


