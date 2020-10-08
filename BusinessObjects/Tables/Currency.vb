'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (10/21/2004)  ********************

Public Class Currency
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
            Dim dal As New CurrencyDAL
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
            Dim dal As New CurrencyDAL
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
            If row(CurrencyDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CurrencyDAL.COL_NAME_CURRENCY_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=40)> _
    Public Property Description As String
        Get
            CheckDeleted()
            If row(CurrencyDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(CurrencyDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CurrencyDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=2)> _
    Public Property Code As String
        Get
            CheckDeleted()
            If row(CurrencyDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(CurrencyDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CurrencyDAL.COL_NAME_CODE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=3)> _
    Public Property Notation As String
        Get
            CheckDeleted()
            If row(CurrencyDAL.COL_NAME_NOTATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(CurrencyDAL.COL_NAME_NOTATION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CurrencyDAL.COL_NAME_NOTATION, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=3)> _
    Public Property IsoCode As String
        Get
            CheckDeleted()
            If row(CurrencyDAL.COL_NAME_ISO_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(CurrencyDAL.COL_NAME_ISO_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CurrencyDAL.COL_NAME_ISO_CODE, Value)
        End Set
    End Property



#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New CurrencyDAL
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

    Public Shared Sub AddNewRowToCurrencySearchDV(ByRef dv As CurrencySearchDV, NewCurrencyBO As Currency)
        Dim dt As DataTable, blnEmptyTbl As Boolean = False

        If NewCurrencyBO.IsNew Then
            Dim row As DataRow
            If dv Is Nothing Then
                Dim guidTemp As New Guid
                blnEmptyTbl = True
                dt = New DataTable
                dt.Columns.Add(CurrencySearchDV.COL_NAME_CURRENCY_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(CurrencySearchDV.COL_NAME_CODE, GetType(String))
                dt.Columns.Add(CurrencySearchDV.COL_NAME_DESCRIPTION, GetType(String))
                dt.Columns.Add(CurrencySearchDV.COL_NAME_NOTATION, GetType(String))
                dt.Columns.Add(CurrencySearchDV.COL_NAME_ISO_CODE, GetType(String))
            Else
                dt = dv.Table
            End If
            row = dt.NewRow
            row(CurrencySearchDV.COL_NAME_CURRENCY_ID) = NewCurrencyBO.Id.ToByteArray
            row(CurrencySearchDV.COL_NAME_CODE) = NewCurrencyBO.Code
            row(CurrencySearchDV.COL_NAME_DESCRIPTION) = NewCurrencyBO.Description
            row(CurrencySearchDV.COL_NAME_NOTATION) = NewCurrencyBO.Notation
            row(CurrencySearchDV.COL_NAME_DESCRIPTION) = NewCurrencyBO.IsoCode
            dt.Rows.Add(row)
            If blnEmptyTbl Then dv = New CurrencySearchDV(dt)
        End If
    End Sub

    Public Shared Function getList(strCode As String, strDescription As String, strNotation As String, strISOCode As String) As CurrencySearchDV
        Try
            Dim dal As New CurrencyDAL
            Return New CurrencySearchDV(dal.LoadList(strCode, strDescription, strNotation, strISOCode).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region
#Region "RouteSearchDV"

    Public Class CurrencySearchDV
        Inherits DataView

#Region "Constants"

        Public Const COL_NAME_CURRENCY_ID As String = CurrencyDAL.COL_NAME_CURRENCY_ID
        Public Const COL_NAME_DESCRIPTION As String = CurrencyDAL.COL_NAME_DESCRIPTION
        Public Const COL_NAME_CODE As String = CurrencyDAL.COL_NAME_CODE
        Public Const COL_NAME_NOTATION As String = CurrencyDAL.COL_NAME_NOTATION
        Public Const COL_NAME_ISO_CODE As String = CurrencyDAL.COL_NAME_ISO_CODE


#End Region

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property CurrencyId(row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_CURRENCY_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property Description(row As DataRow) As String
            Get
                Return row(COL_NAME_DESCRIPTION).ToString
            End Get
        End Property

        Public Shared ReadOnly Property Code(row As DataRow) As String
            Get
                Return row(COL_NAME_CODE).ToString
            End Get
        End Property


        Public Shared ReadOnly Property Notation(row As DataRow) As String
            Get
                Return row(COL_NAME_NOTATION).ToString
            End Get
        End Property

        Public Shared ReadOnly Property ISOCode(row As DataRow) As String
            Get
                Return row(COL_NAME_ISO_CODE).ToString
            End Get
        End Property

    End Class
#End Region
End Class

