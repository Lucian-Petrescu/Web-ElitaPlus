'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (3/19/2007)  ********************

Public Class BankName
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
            Dim dal As New BankNameDAL
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
            Dim dal As New BankNameDAL
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
    Private Shared Sub Initialize()
    End Sub
#End Region

#Region "CONSTANTS"

    Public Const WILDCARD_CHAR As Char = "%"
    Public Const ASTERISK As Char = "*"
    'Private Const DSNAME As String = "LIST"

#End Region


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid
        Get
            If Row(BankNameDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(BankNameDAL.COL_NAME_BANK_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=10)> _
    Public Property Code As String
        Get
            CheckDeleted()
            If Row(BankNameDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(BankNameDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BankNameDAL.COL_NAME_CODE, Value)
        End Set
    End Property



    <ValueMandatory(""), ValidStringLength("", Max:=100)> _
    Public Property Description As String
        Get
            CheckDeleted()
            If Row(BankNameDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(BankNameDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BankNameDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property

    Public Property CountryID As Guid
        Get
            CheckDeleted()
            If Row(BankNameDAL.COL_NAME_COUNTRY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(BankNameDAL.COL_NAME_COUNTRY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(BankNameDAL.COL_NAME_COUNTRY_ID, Value)
        End Set
    End Property

#End Region

#Region "DataView Retrieveing Methods"
#Region "BankNameSearchDV"
    Public Class BankNameSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_BANK_ID As String = "bank_id"
        Public Const COL_CODE As String = "code"
        Public Const COL_DESCRIPTION As String = "description"
        Public Const COL_COUNTRY_ID As String = "country_id"
#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region

    Public Shared Function getList(BankName As String, codeMask As String, CountryID As Guid) As BankNameSearchDV
        Try
            Dim dal As New BankNameDAL
            Return New BankNameSearchDV(dal.LoadList(BankName, codeMask, CountryID).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function LoadBankNameByCountry(CountryID As Guid) As DataTable
        Try
            Dim dal As New BankNameDAL
            Return dal.LoadBankNameByCountry(CountryID).Tables(0)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Function GetNewDataViewRow(dv As DataView, id As Guid, CountryID As Guid) As BankNameSearchDV

        Dim dt As DataTable
        dt = dv.Table
        Dim newrow As DataRow = dt.NewRow

        newrow(BankNameDAL.COL_NAME_BANK_ID) = id.ToByteArray
        newrow(BankNameDAL.COL_NAME_CODE) = String.Empty
        newrow(BankNameDAL.COL_NAME_DESCRIPTION) = String.Empty
        newrow(BankNameDAL.COL_NAME_COUNTRY_ID) = CountryID.ToByteArray
        dt.Rows.Add(newrow)
        Row = newrow
        Return New BankNameSearchDV(dt)

    End Function

    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New BankNameDAL
                dal.Update(Row)
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
#End Region

End Class



