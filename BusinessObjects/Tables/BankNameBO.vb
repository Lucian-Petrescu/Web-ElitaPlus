'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (3/19/2007)  ********************

Public Class BankName
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
            Dim dal As New BankNameDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New BankNameDAL
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
    Public ReadOnly Property Id() As Guid
        Get
            If Row(BankNameDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(BankNameDAL.COL_NAME_BANK_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=10)> _
    Public Property Code() As String
        Get
            CheckDeleted()
            If Row(BankNameDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(BankNameDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(BankNameDAL.COL_NAME_CODE, Value)
        End Set
    End Property



    <ValueMandatory(""), ValidStringLength("", Max:=100)> _
    Public Property Description() As String
        Get
            CheckDeleted()
            If Row(BankNameDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(BankNameDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(BankNameDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property

    Public Property CountryID() As Guid
        Get
            CheckDeleted()
            If Row(BankNameDAL.COL_NAME_COUNTRY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(BankNameDAL.COL_NAME_COUNTRY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(BankNameDAL.COL_NAME_COUNTRY_ID, Value)
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

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region

    Public Shared Function getList(ByVal BankName As String, ByVal codeMask As String, ByVal CountryID As Guid) As BankNameSearchDV
        Try
            Dim dal As New BankNameDAL
            Return New BankNameSearchDV(dal.LoadList(BankName, codeMask, CountryID).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function LoadBankNameByCountry(ByVal CountryID As Guid) As DataTable
        Try
            Dim dal As New BankNameDAL
            Return dal.LoadBankNameByCountry(CountryID).Tables(0)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Function GetNewDataViewRow(ByVal dv As DataView, ByVal id As Guid, ByVal CountryID As Guid) As BankNameSearchDV

        Dim dt As DataTable
        dt = dv.Table
        Dim newrow As DataRow = dt.NewRow

        newrow(BankNameDAL.COL_NAME_BANK_ID) = id.ToByteArray
        newrow(BankNameDAL.COL_NAME_CODE) = String.Empty
        newrow(BankNameDAL.COL_NAME_DESCRIPTION) = String.Empty
        newrow(BankNameDAL.COL_NAME_COUNTRY_ID) = CountryID.ToByteArray
        dt.Rows.Add(newrow)
        Me.Row = newrow
        Return New BankNameSearchDV(dt)

    End Function

    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New BankNameDAL
                dal.Update(Me.Row)
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
#End Region

End Class



