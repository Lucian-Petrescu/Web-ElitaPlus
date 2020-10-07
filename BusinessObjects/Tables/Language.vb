'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (3/13/2009)  ********************

Public Class Language
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
            Dim dal As New LanguageDAL
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
            Dim dal As New LanguageDAL
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
            If row(LanguageDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(LanguageDAL.COL_NAME_LANGUAGE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=160)> _
    Public Property Description() As String
        Get
            CheckDeleted()
            If row(LanguageDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(LanguageDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(LanguageDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=20)> _
    Public Property Code() As String
        Get
            CheckDeleted()
            If row(LanguageDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(LanguageDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(LanguageDAL.COL_NAME_CODE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=160)> _
    Public Property CultureCode() As String
        Get
            CheckDeleted()
            If row(LanguageDAL.COL_NAME_CULTURE_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(LanguageDAL.COL_NAME_CULTURE_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(LanguageDAL.COL_NAME_CULTURE_CODE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=4)> _
    Public Property ActiveFlag() As String
        Get
            CheckDeleted()
            If row(LanguageDAL.COL_NAME_ACTIVE_FLAG) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(LanguageDAL.COL_NAME_ACTIVE_FLAG), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(LanguageDAL.COL_NAME_ACTIVE_FLAG, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200)> _
    Public Property Territory() As String
        Get
            CheckDeleted()
            If row(LanguageDAL.COL_NAME_TERRITORY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(LanguageDAL.COL_NAME_TERRITORY), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(LanguageDAL.COL_NAME_TERRITORY, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New LanguageDAL
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

    Public Shared Sub AddNewRowToLanguageSearchDV(ByRef dv As LanguageSearchDV, ByVal NewLanguageBO As Language)
        Dim dt As DataTable, blnEmptyTbl As Boolean = False

        If NewLanguageBO.IsNew Then
            Dim row As DataRow
            If dv Is Nothing Then
                Dim guidTemp As New Guid
                blnEmptyTbl = True
                dt = New DataTable
                dt.Columns.Add(LanguageSearchDV.COL_NAME_Language_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(LanguageSearchDV.COL_NAME_CODE, GetType(String))
                dt.Columns.Add(LanguageSearchDV.COL_NAME_DESCRIPTION, GetType(String))
                dt.Columns.Add(LanguageSearchDV.COL_NAME_CULTURE, GetType(String))
                dt.Columns.Add(LanguageSearchDV.COL_NAME_TERRITORY, GetType(String))
            Else
                dt = dv.Table
            End If
            row = dt.NewRow
            row(LanguageSearchDV.COL_NAME_Language_ID) = NewLanguageBO.Id.ToByteArray
            row(LanguageSearchDV.COL_NAME_CODE) = NewLanguageBO.Code
            row(LanguageSearchDV.COL_NAME_DESCRIPTION) = NewLanguageBO.Description
            row(LanguageSearchDV.COL_NAME_CULTURE) = NewLanguageBO.CultureCode
            row(LanguageSearchDV.COL_NAME_TERRITORY) = NewLanguageBO.Territory
            dt.Rows.Add(row)
            If blnEmptyTbl Then dv = New LanguageSearchDV(dt)
        End If
    End Sub

    Public Shared Function getList(ByVal strCode As String, ByVal strDescription As String, ByVal strNotation As String, ByVal strISOCode As String) As LanguageSearchDV
        Try
            Dim dal As New LanguageDAL
            Return New LanguageSearchDV(dal.LoadList(strCode, strDescription, strNotation, strISOCode).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region
#Region "RouteSearchDV"

    Public Class LanguageSearchDV
        Inherits DataView

#Region "Constants"

        Public Const COL_NAME_LANGUAGE_ID As String = LanguageDAL.COL_NAME_LANGUAGE_ID
        Public Const COL_NAME_DESCRIPTION As String = LanguageDAL.COL_NAME_DESCRIPTION
        Public Const COL_NAME_CODE As String = LanguageDAL.COL_NAME_CODE
        Public Const COL_NAME_CULTURE As String = LanguageDAL.COL_NAME_CULTURE_CODE
        Public Const COL_NAME_TERRITORY As String = LanguageDAL.COL_NAME_TERRITORY


#End Region

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property LanguageId(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_LANGUAGE_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property Description(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_DESCRIPTION).ToString
            End Get
        End Property

        Public Shared ReadOnly Property Code(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_CODE).ToString
            End Get
        End Property


        Public Shared ReadOnly Property CultureCode(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_CULTURE).ToString
            End Get
        End Property

        Public Shared ReadOnly Property Territory(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_TERRITORY).ToString
            End Get
        End Property

    End Class
#End Region
End Class


