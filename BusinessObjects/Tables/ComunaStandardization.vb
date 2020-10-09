'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (10/8/2009)  ********************

Public Class ComunaStandardization
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
            Dim dal As New ComunaStandardizationDAL
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
            Dim dal As New ComunaStandardizationDAL
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

    Public Shared Function LoadList(ComunaAliasMask As String, ComunaMask As String) As ComunaStdSearchDV
        Try
            Dim dal As New ComunaStandardizationDAL
            Dim UserId As Guid = ElitaPlusIdentity.Current.ActiveUser.Id
            Return New ComunaStdSearchDV(dal.LoadList(ComunaAliasMask, ComunaMask, UserId).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetComunaList() As DataView
        Try
            Dim dal As New ComunaStandardizationDAL
            Dim UserId As Guid = ElitaPlusIdentity.Current.ActiveUser.Id
            Return New DataView(dal.GetComunaList(UserId).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetComunaStanderization(ComunaAliasMask As String) As DataView
        Try
            Dim dal As New ComunaStandardizationDAL
            Dim UserId As Guid = ElitaPlusIdentity.Current.ActiveUser.Id
            Return New DataView(dal.GetComunaStanderization(UserId, ComunaAliasMask).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Sub AddNewRowToSearchDV(ByRef dv As ComunaStdSearchDV, NewComunaStdBO As ComunaStandardization)
        Dim dt As DataTable, blnEmptyTbl As Boolean = False

        dv.Sort = ""
        If NewComunaStdBO.IsNew Then
            Dim row As DataRow
            If dv Is Nothing Then
                Dim guidTemp As New Guid
                blnEmptyTbl = True
                dt = New DataTable
                dt.Columns.Add(ComunaStdSearchDV.COL_COMUNA_ALIAS_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(ComunaStdSearchDV.COL_COMUNA, GetType(String))
                dt.Columns.Add(ComunaStdSearchDV.COL_COMUNA_ALIAS, GetType(String))
                dt.Columns.Add(ComunaStdSearchDV.COL_COMUNA_CODE_ID, guidTemp.ToByteArray.GetType)
            Else
                dt = dv.Table
            End If
            row = dt.NewRow
            row(ComunaStdSearchDV.COL_COMUNA_ALIAS_ID) = NewComunaStdBO.Id.ToByteArray
            row(ComunaStdSearchDV.COL_COMUNA) = String.Empty
            row(ComunaStdSearchDV.COL_COMUNA_ALIAS) = String.Empty
            row(ComunaStdSearchDV.COL_COMUNA_CODE_ID) = NewComunaStdBO.ComunaCodeId.ToByteArray
            dt.Rows.Add(row)
            If blnEmptyTbl Then dv = New ComunaStdSearchDV(dt)
        End If
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
            If row(ComunaStandardizationDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ComunaStandardizationDAL.COL_NAME_COMUNA_ALIAS_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=200)> _
    Public Property ComunaAlias As String
        Get
            CheckDeleted()
            If row(ComunaStandardizationDAL.COL_NAME_COMUNA_ALIAS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ComunaStandardizationDAL.COL_NAME_COMUNA_ALIAS), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ComunaStandardizationDAL.COL_NAME_COMUNA_ALIAS, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ComunaCodeId As Guid
        Get
            CheckDeleted()
            If row(ComunaStandardizationDAL.COL_NAME_COMUNA_CODE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ComunaStandardizationDAL.COL_NAME_COMUNA_CODE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ComunaStandardizationDAL.COL_NAME_COMUNA_CODE_ID, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ComunaStandardizationDAL
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

#Region "SearchDV"
    Public Class ComunaStdSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_COMUNA_ALIAS_ID As String = ComunaStandardizationDAL.COL_NAME_COMUNA_ALIAS_ID
        Public Const COL_COMUNA_ALIAS As String = ComunaStandardizationDAL.COL_NAME_COMUNA_ALIAS
        Public Const COL_COMUNA As String = ComunaStandardizationDAL.COL_NAME_COMUNA
        Public Const COL_COMUNA_CODE_ID As String = ComunaStandardizationDAL.COL_NAME_COMUNA_CODE_ID
#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

        Public Function AddNewRowToEmptyDV() As ComunaStdSearchDV
            Dim dt As DataTable = Table.Clone()
            Dim row As DataRow = dt.NewRow
            row(COL_COMUNA_ALIAS_ID) = (New Guid()).ToByteArray
            row(COL_COMUNA) = ""
            row(COL_COMUNA_ALIAS) = ""
            row(COL_COMUNA_CODE_ID) = Guid.Empty.ToByteArray
            dt.Rows.Add(row)
            Return New ComunaStdSearchDV(dt)
        End Function

    End Class


#End Region
End Class



