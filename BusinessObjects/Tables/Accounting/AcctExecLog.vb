'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (3/6/2008)  ********************

Public Class AcctExecLog
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

    'Overload to get by Event Id and Company
    Public Sub New(Companyid As Guid, AcctEventId As Guid)
        MyBase.New()
        Dataset = New DataSet
        Dim dal As New AcctExecLogDAL
        Load(dal.LoadByEvent(Companyid, AcctEventId))
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New AcctExecLogDAL
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
            Dim dal As New AcctExecLogDAL
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
            If row(AcctExecLogDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AcctExecLogDAL.COL_NAME_ACCT_EXEC_LOG_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CompanyId As Guid
        Get
            CheckDeleted()
            If row(AcctExecLogDAL.COL_NAME_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AcctExecLogDAL.COL_NAME_COMPANY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctExecLogDAL.COL_NAME_COMPANY_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property AcctEventId As Guid
        Get
            CheckDeleted()
            If row(AcctExecLogDAL.COL_NAME_ACCT_EVENT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AcctExecLogDAL.COL_NAME_ACCT_EVENT_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctExecLogDAL.COL_NAME_ACCT_EVENT_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property LastRunDate As DateType
        Get
            CheckDeleted()
            If row(AcctExecLogDAL.COL_NAME_LAST_RUN_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(AcctExecLogDAL.COL_NAME_LAST_RUN_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctExecLogDAL.COL_NAME_LAST_RUN_DATE, Value)
        End Set
    End Property


    Public Property PreviousRunDate As DateType
        Get
            CheckDeleted()
            If Row(AcctExecLogDAL.COL_NAME_PREVIOUS_RUN_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(AcctExecLogDAL.COL_NAME_PREVIOUS_RUN_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctExecLogDAL.COL_NAME_PREVIOUS_RUN_DATE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1)> _
    Public Property Status As String
        Get
            CheckDeleted()
            If Row(AcctExecLogDAL.COL_NAME_STATUS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctExecLogDAL.COL_NAME_STATUS), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(AcctExecLogDAL.COL_NAME_STATUS, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New AcctExecLogDAL
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

    Public Function UpdateExecStatus(Success As Boolean) As Boolean

        Try
            If Success Then
                Status = String.Empty
            Else
                If PreviousRunDate IsNot Nothing Then LastRunDate = PreviousRunDate
                Status = "F"
            End If

            Save()

        Catch ex As Exception
            Throw New ElitaPlusException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, "", ex)
        End Try

    End Function
#End Region

#Region "DataView Retrieveing Methods"

#End Region

End Class


