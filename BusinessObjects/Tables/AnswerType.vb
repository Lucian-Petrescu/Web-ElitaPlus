'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (8/31/2012)  ********************

Public Class AnswerType
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
            Dim dal As New AnswerTypeDAL
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
            Dim dal As New AnswerTypeDAL
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
            If row(AnswerTypeDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AnswerTypeDAL.COL_NAME_ANSWER_TYPE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=1020)> _
    Public Property Code As String
        Get
            CheckDeleted()
            If row(AnswerTypeDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(AnswerTypeDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AnswerTypeDAL.COL_NAME_CODE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=4000)> _
    Public Property Description As String
        Get
            CheckDeleted()
            If row(AnswerTypeDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(AnswerTypeDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AnswerTypeDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=4000)> _
    Public Property ValidationRegex As String
        Get
            CheckDeleted()
            If row(AnswerTypeDAL.COL_NAME_VALIDATION_REGEX) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(AnswerTypeDAL.COL_NAME_VALIDATION_REGEX), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AnswerTypeDAL.COL_NAME_VALIDATION_REGEX, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property AnswerTypeItemId As Guid
        Get
            CheckDeleted()
            If row(AnswerTypeDAL.COL_NAME_ANSWER_TYPE_ITEM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AnswerTypeDAL.COL_NAME_ANSWER_TYPE_ITEM_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AnswerTypeDAL.COL_NAME_ANSWER_TYPE_ITEM_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200)> _
    Public Property LookUpListCode As String
        Get
            CheckDeleted()
            If row(AnswerTypeDAL.COL_NAME_LOOK_UP_LIST_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(AnswerTypeDAL.COL_NAME_LOOK_UP_LIST_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AnswerTypeDAL.COL_NAME_LOOK_UP_LIST_CODE, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New AnswerTypeDAL
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


