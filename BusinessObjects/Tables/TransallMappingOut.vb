'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (4/27/2011)  ********************

Public Class TransallMappingOut
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
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
            Dim dal As New TransallMappingOutDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            setvalue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New TransallMappingOutDAL
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

    Public Shared Function GetList(ByVal TransAllMappingId As Guid) As DataView

        Try
            Dim dal As New DALObjects.TransallMappingOutDAL
            Return New System.Data.DataView(dal.LoadList(TransAllMappingId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

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
            If row(TransallMappingOutDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(TransallMappingOutDAL.COL_NAME_TRANSALL_MAPPING_OUT_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property TransallMappingId() As Guid
        Get
            CheckDeleted()
            If row(TransallMappingOutDAL.COL_NAME_TRANSALL_MAPPING_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(TransallMappingOutDAL.COL_NAME_TRANSALL_MAPPING_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(TransallMappingOutDAL.COL_NAME_TRANSALL_MAPPING_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=400)> _
    Public Property OutputMask() As String
        Get
            CheckDeleted()
            If row(TransallMappingOutDAL.COL_NAME_OUTPUT_MASK) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransallMappingOutDAL.COL_NAME_OUTPUT_MASK), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(TransallMappingOutDAL.COL_NAME_OUTPUT_MASK, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property LayoutCodeId() As Guid
        Get
            CheckDeleted()
            If row(TransallMappingOutDAL.COL_NAME_LAYOUT_CODE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(TransallMappingOutDAL.COL_NAME_LAYOUT_CODE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(TransallMappingOutDAL.COL_NAME_LAYOUT_CODE_ID, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New TransallMappingOutDAL
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

#Region "DataView Retrieveing Methods"

    Public Shared Function GetNewDataViewRow(ByVal dv As DataView, ByVal id As Guid, ByVal bo As TransallMappingOut) As DataView

        Dim dt As DataTable
        dt = dv.Table

        If bo.IsNew Then
            Dim row As DataRow = dt.NewRow

            row(TransallMappingOutDAL.COL_NAME_TRANSALL_MAPPING_OUT_ID) = bo.Id.ToByteArray
            row(TransallMappingOutDAL.COL_NAME_TRANSALL_MAPPING_ID) = bo.TransallMappingId.ToByteArray
            row(TransallMappingOutDAL.COL_NAME_OUTPUT_MASK) = bo.OutputMask
            row(TransallMappingOutDAL.COL_NAME_LAYOUT_CODE_ID) = bo.LayoutCodeId.ToByteArray

            dt.Rows.Add(row)
        End If

        Return (dv)

    End Function

    Public Class TransallMappingOutDV
        Inherits DataView

#Region "Constants"
        Public Const COL_TRANSALL_MAPPING_OUT_ID As String = TransallMappingOutDAL.COL_NAME_TRANSALL_MAPPING_OUT_ID
        Public Const COL_ACCT_OUTPUT_MASK As String = TransallMappingOutDAL.COL_NAME_OUTPUT_MASK
        Public Const COL_ACCT_TRANSALL_MAPPING_ID As String = TransallMappingOutDAL.COL_NAME_TRANSALL_MAPPING_ID
        Public Const COL_LAYOUT_CODE_ID As String = TransallMappingOutDAL.COL_NAME_LAYOUT_CODE_ID

#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

#End Region

End Class


