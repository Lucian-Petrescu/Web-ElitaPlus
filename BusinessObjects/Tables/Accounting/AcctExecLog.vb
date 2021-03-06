'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (3/6/2008)  ********************

Public Class AcctExecLog
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

    'Overload to get by Event Id and Company
    Public Sub New(ByVal Companyid As Guid, ByVal AcctEventId As Guid)
        MyBase.New()
        Me.Dataset = New DataSet
        Dim dal As New AcctExecLogDAL
        Me.Load(dal.LoadByEvent(Companyid, AcctEventId))
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New AcctExecLogDAL
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
            Dim dal As New AcctExecLogDAL
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
    Private Sub Initialize()
    End Sub
#End Region


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If row(AcctExecLogDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AcctExecLogDAL.COL_NAME_ACCT_EXEC_LOG_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CompanyId() As Guid
        Get
            CheckDeleted()
            If row(AcctExecLogDAL.COL_NAME_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AcctExecLogDAL.COL_NAME_COMPANY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(AcctExecLogDAL.COL_NAME_COMPANY_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property AcctEventId() As Guid
        Get
            CheckDeleted()
            If row(AcctExecLogDAL.COL_NAME_ACCT_EVENT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(AcctExecLogDAL.COL_NAME_ACCT_EVENT_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(AcctExecLogDAL.COL_NAME_ACCT_EVENT_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property LastRunDate() As DateType
        Get
            CheckDeleted()
            If row(AcctExecLogDAL.COL_NAME_LAST_RUN_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(AcctExecLogDAL.COL_NAME_LAST_RUN_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(AcctExecLogDAL.COL_NAME_LAST_RUN_DATE, Value)
        End Set
    End Property


    Public Property PreviousRunDate() As DateType
        Get
            CheckDeleted()
            If Row(AcctExecLogDAL.COL_NAME_PREVIOUS_RUN_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(AcctExecLogDAL.COL_NAME_PREVIOUS_RUN_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(AcctExecLogDAL.COL_NAME_PREVIOUS_RUN_DATE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1)> _
    Public Property Status() As String
        Get
            CheckDeleted()
            If Row(AcctExecLogDAL.COL_NAME_STATUS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(AcctExecLogDAL.COL_NAME_STATUS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(AcctExecLogDAL.COL_NAME_STATUS, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New AcctExecLogDAL
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

    Public Function UpdateExecStatus(ByVal Success As Boolean) As Boolean

        Try
            If Success Then
                Me.Status = String.Empty
            Else
                If Not Me.PreviousRunDate Is Nothing Then Me.LastRunDate = Me.PreviousRunDate
                Me.Status = "F"
            End If

            Me.Save()

        Catch ex As Exception
            Throw New ElitaPlusException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, "", ex)
        End Try

    End Function
#End Region

#Region "DataView Retrieveing Methods"

#End Region

End Class


