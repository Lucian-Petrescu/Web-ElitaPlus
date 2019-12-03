'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (7/4/2017)  ********************

Public Class CaseAction
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
            Dim dal As New CaseActionDAL
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
            Dim dal As New CaseActionDAL            
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
            If row(CaseActionDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CaseActionDAL.COL_NAME_CASE_ACTION_ID), Byte()))
            End If
        End Get
    End Property
	
    
    Public Property CaseId() As Guid
        Get
            CheckDeleted()
            If row(CaseActionDAL.COL_NAME_CASE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CaseActionDAL.COL_NAME_CASE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CaseActionDAL.COL_NAME_CASE_ID, Value)
        End Set
    End Property
	
	
    
    Public Property ClaimId() As Guid
        Get
            CheckDeleted()
            If row(CaseActionDAL.COL_NAME_CLAIM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CaseActionDAL.COL_NAME_CLAIM_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CaseActionDAL.COL_NAME_CLAIM_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=200)>
    Public Property ActionOwnerXcd() As String
        Get
            CheckDeleted()
            If row(CaseActionDAL.COL_NAME_ACTION_OWNER_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(CaseActionDAL.COL_NAME_ACTION_OWNER_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CaseActionDAL.COL_NAME_ACTION_OWNER_XCD, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=200)>
    Public Property ActionOwner() As String
        Get
            CheckDeleted()
            If Row(CaseActionDAL.COL_NAME_ACTION_OWNER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CaseActionDAL.COL_NAME_ACTION_OWNER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CaseActionDAL.COL_NAME_ACTION_OWNER, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=200)>
    Public Property ActionTypeXcd() As String
        Get
            CheckDeleted()
            If row(CaseActionDAL.COL_NAME_ACTION_TYPE_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(CaseActionDAL.COL_NAME_ACTION_TYPE_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CaseActionDAL.COL_NAME_ACTION_TYPE_XCD, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=200)>
    Public Property ActionType() As String
        Get
            CheckDeleted()
            If Row(CaseActionDAL.COL_NAME_ACTION_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CaseActionDAL.COL_NAME_ACTION_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CaseActionDAL.COL_NAME_ACTION_TYPE, Value)
        End Set
    End Property


    <ValueMandatory(""),ValidStringLength("", Max:=200)> _
    Public Property StatusXcd() As String
        Get
            CheckDeleted()
            If row(CaseActionDAL.COL_NAME_STATUS_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(CaseActionDAL.COL_NAME_STATUS_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CaseActionDAL.COL_NAME_STATUS_XCD, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200)>
    Public Property Status() As String
        Get
            CheckDeleted()
            If Row(CaseActionDAL.COL_NAME_STATUS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CaseActionDAL.COL_NAME_STATUS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CaseActionDAL.COL_NAME_STATUS, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=120)> _
    Public Property RefSource() As String
        Get
            CheckDeleted()
            If row(CaseActionDAL.COL_NAME_REF_SOURCE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(CaseActionDAL.COL_NAME_REF_SOURCE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CaseActionDAL.COL_NAME_REF_SOURCE, Value)
        End Set
    End Property
	
	
    
    Public Property RefId() As Guid
        Get
            CheckDeleted()
            If row(CaseActionDAL.COL_NAME_REF_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CaseActionDAL.COL_NAME_REF_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CaseActionDAL.COL_NAME_REF_ID, Value)
        End Set
    End Property
	
	
   

#End Region

#Region "Public Members"
    Public Overrides Sub Save()         
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New CaseActionDAL
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
    Public Shared Function GetCaseActionList(ByVal CaseId As Guid, ByVal LanguageId As Guid) As CaseActionDV
        Try
            Dim dal As New CaseActionDAL

            Return New CaseActionDV(dal.LoadCaseActionList(CaseId, LanguageId).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Public Shared Function GetClaimActionList(ByVal ClaimId As Guid, ByVal LanguageId As Guid) As CaseActionDV
        Try
            Dim dal As New CaseActionDAL

            Return New CaseActionDV(dal.LoadClaimActionList(ClaimId, LanguageId).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region
    Public Class CaseActionDV
        Inherits DataView

#Region "Constants"
        Public Const ColActionCreatedDate As String = "created_date"
#End Region
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub


    End Class

End Class


