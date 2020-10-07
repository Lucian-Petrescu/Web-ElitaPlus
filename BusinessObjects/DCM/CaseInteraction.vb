'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (7/1/2017)  ********************

Public Class CaseInteraction
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
            Dim dal As New CaseInteractionDAL
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
            Dim dal As New CaseInteractionDAL            
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
            If row(CaseInteractionDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CaseInteractionDAL.COL_NAME_CASE_INTERACTION_ID), Byte()))
            End If
        End Get
    End Property
	
    <ValueMandatory("")> _
    Public Property CaseId() As Guid
        Get
            CheckDeleted()
            If row(CaseInteractionDAL.COL_NAME_CASE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CaseInteractionDAL.COL_NAME_CASE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(CaseInteractionDAL.COL_NAME_CASE_ID, Value)
        End Set
    End Property
	
	
    <ValueMandatory("")> _
    Public Property InteractionNumber() As DecimalType
        Get
            CheckDeleted()
            If row(CaseInteractionDAL.COL_NAME_INTERACTION_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(CaseInteractionDAL.COL_NAME_INTERACTION_NUMBER), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            SetValue(CaseInteractionDAL.COL_NAME_INTERACTION_NUMBER, Value)
        End Set
    End Property
	
	
    <ValueMandatory(""),ValidStringLength("", Max:=200)> _
    Public Property InteractionPurposeXcd() As String
        Get
            CheckDeleted()
            If row(CaseInteractionDAL.COL_NAME_INTERACTION_PURPOSE_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(CaseInteractionDAL.COL_NAME_INTERACTION_PURPOSE_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(CaseInteractionDAL.COL_NAME_INTERACTION_PURPOSE_XCD, Value)
        End Set
    End Property
	
	
    <ValueMandatory(""),ValidStringLength("", Max:=200)> _
    Public Property ChannelXcd() As String
        Get
            CheckDeleted()
            If row(CaseInteractionDAL.COL_NAME_CHANNEL_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(CaseInteractionDAL.COL_NAME_CHANNEL_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(CaseInteractionDAL.COL_NAME_CHANNEL_XCD, Value)
        End Set
    End Property
	
	
    <ValueMandatory("")> _
    Public Property CallerId() As Guid
        Get
            CheckDeleted()
            If row(CaseInteractionDAL.COL_NAME_CALLER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CaseInteractionDAL.COL_NAME_CALLER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(CaseInteractionDAL.COL_NAME_CALLER_ID, Value)
        End Set
    End Property
	
	
    <ValueMandatory("")> _
    Public Property InteractionDate() As DateType
        Get
            CheckDeleted()
            If row(CaseInteractionDAL.COL_NAME_INTERACTION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(CaseInteractionDAL.COL_NAME_INTERACTION_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            SetValue(CaseInteractionDAL.COL_NAME_INTERACTION_DATE, Value)
        End Set
    End Property
	
	
    <ValueMandatory(""),ValidStringLength("", Max:=200)> _
    Public Property IsCallerAuthenticatedXcd() As String
        Get
            CheckDeleted()
            If row(CaseInteractionDAL.COL_NAME_IS_CALLER_AUTHENTICATED_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(CaseInteractionDAL.COL_NAME_IS_CALLER_AUTHENTICATED_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(CaseInteractionDAL.COL_NAME_IS_CALLER_AUTHENTICATED_XCD, Value)
        End Set
    End Property
	
	
    <ValidStringLength("", Max:=200)> _
    Public Property CallerAuthenctionMethodXcd() As String
        Get
            CheckDeleted()
            If row(CaseInteractionDAL.COL_NAME_CALLER_AUTHENCTION_METHOD_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(CaseInteractionDAL.COL_NAME_CALLER_AUTHENCTION_METHOD_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(CaseInteractionDAL.COL_NAME_CALLER_AUTHENCTION_METHOD_XCD, Value)
        End Set
    End Property
	
	
    <ValueMandatory(""),ValidStringLength("", Max:=40)> _
    Public Property CultureCode() As String
        Get
            CheckDeleted()
            If row(CaseInteractionDAL.COL_NAME_CULTURE_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(CaseInteractionDAL.COL_NAME_CULTURE_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(CaseInteractionDAL.COL_NAME_CULTURE_CODE, Value)
        End Set
    End Property
	
	
   

#End Region

#Region "Public Members"
    Public Overrides Sub Save()         
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New CaseInteractionDAL
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
    Public Shared Function GetCaseInteractionList(ByVal CaseId As Guid, ByVal LanguageId As Guid) As CaseInteractionDV
        Try
            Dim dal As New CaseInteractionDAL

            Return New CaseInteractionDV(dal.LoadCaseInteractionList(CaseId, LanguageId).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region
    Public Class CaseInteractionDV
        Inherits DataView

#Region "Constants"

#End Region
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub


    End Class
End Class


