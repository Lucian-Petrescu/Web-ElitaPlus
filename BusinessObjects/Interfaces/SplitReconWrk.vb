'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (12/29/2005)  ********************

Public Class SplitReconWrk
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
            Dim dal As New SplitReconWrkDAL
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
            Dim dal As New SplitReconWrkDAL
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
            If row(SplitReconWrkDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(SplitReconWrkDAL.COL_NAME_SPLIT_RECON_WRK_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property SplitfileProcessedId As Guid
        Get
            CheckDeleted()
            If row(SplitReconWrkDAL.COL_NAME_SPLITFILE_PROCESSED_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(SplitReconWrkDAL.COL_NAME_SPLITFILE_PROCESSED_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SplitReconWrkDAL.COL_NAME_SPLITFILE_PROCESSED_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=4)> _
    Public Property RecordType As String
        Get
            CheckDeleted()
            If row(SplitReconWrkDAL.COL_NAME_RECORD_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(SplitReconWrkDAL.COL_NAME_RECORD_TYPE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SplitReconWrkDAL.COL_NAME_RECORD_TYPE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=2000)> _
    Public Property Rest As String
        Get
            CheckDeleted()
            If row(SplitReconWrkDAL.COL_NAME_REST) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(SplitReconWrkDAL.COL_NAME_REST), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SplitReconWrkDAL.COL_NAME_REST, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=1)> _
    Public Property SplitProcessed As String
        Get
            CheckDeleted()
            If row(SplitReconWrkDAL.COL_NAME_SPLIT_PROCESSED) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(SplitReconWrkDAL.COL_NAME_SPLIT_PROCESSED), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SplitReconWrkDAL.COL_NAME_SPLIT_PROCESSED, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=20)> _
    Public Property OutfileName As String
        Get
            CheckDeleted()
            If row(SplitReconWrkDAL.COL_NAME_OUTFILE_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(SplitReconWrkDAL.COL_NAME_OUTFILE_NAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SplitReconWrkDAL.COL_NAME_OUTFILE_NAME, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New SplitReconWrkDAL
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
            Dim oSplitReconWrk As SplitReconWrk = CType(oData, SplitReconWrk)
            Dim dal As New SplitReconWrkDAL
            Dim ds As Dataset

            ds = dal.LoadList(oSplitReconWrk.SplitfileProcessedId)
            Return (ds.Tables(SplitReconWrkDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

#End Region

End Class


