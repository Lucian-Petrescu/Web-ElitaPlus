'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (10/9/2013)  ********************

Public Class ApsPublishingLog
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
            Dim dal As New ApsPublishingLogDAL
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
            Dim dal As New ApsPublishingLogDAL            
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
            If row(ApsPublishingLogDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ApsPublishingLogDAL.COL_NAME_ID), Byte()))
            End If
        End Get
    End Property
	
    <ValueMandatory(""),ValidStringLength("", Max:=1200)> _
    Public Property Header() As String
        Get
            CheckDeleted()
            If row(ApsPublishingLogDAL.COL_NAME_HEADER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ApsPublishingLogDAL.COL_NAME_HEADER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ApsPublishingLogDAL.COL_NAME_HEADER, Value)
        End Set
    End Property
	
	
    <ValueMandatory(""),ValidStringLength("", Max:=200)> _
    Public Property Type() As String
        Get
            CheckDeleted()
            If row(ApsPublishingLogDAL.COL_NAME_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ApsPublishingLogDAL.COL_NAME_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ApsPublishingLogDAL.COL_NAME_TYPE, Value)
        End Set
    End Property
	
	
    <ValidStringLength("", Max:=200)> _
    Public Property Code() As String
        Get
            CheckDeleted()
            If row(ApsPublishingLogDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ApsPublishingLogDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ApsPublishingLogDAL.COL_NAME_CODE, Value)
        End Set
    End Property
	
	
    <ValueMandatory(""),ValidStringLength("", Max:=800)> _
    Public Property MachineName() As String
        Get
            CheckDeleted()
            If row(ApsPublishingLogDAL.COL_NAME_MACHINE_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ApsPublishingLogDAL.COL_NAME_MACHINE_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ApsPublishingLogDAL.COL_NAME_MACHINE_NAME, Value)
        End Set
    End Property
	
	
    <ValueMandatory(""),ValidStringLength("", Max:=800)> _
    Public Property ApplicationName() As String
        Get
            CheckDeleted()
            If row(ApsPublishingLogDAL.COL_NAME_APPLICATION_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ApsPublishingLogDAL.COL_NAME_APPLICATION_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ApsPublishingLogDAL.COL_NAME_APPLICATION_NAME, Value)
        End Set
    End Property
	
	
    <ValidStringLength("", Max:=800)> _
    Public Property UserName() As String
        Get
            CheckDeleted()
            If row(ApsPublishingLogDAL.COL_NAME_USER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ApsPublishingLogDAL.COL_NAME_USER_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ApsPublishingLogDAL.COL_NAME_USER_NAME, Value)
        End Set
    End Property
	
	
    <ValidStringLength("", Max:=4000)> _
    Public Property ExtendedContent() As String
        Get
            CheckDeleted()
            If row(ApsPublishingLogDAL.COL_NAME_EXTENDED_CONTENT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ApsPublishingLogDAL.COL_NAME_EXTENDED_CONTENT), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ApsPublishingLogDAL.COL_NAME_EXTENDED_CONTENT, Value)
        End Set
    End Property
	
	
    <ValueMandatory(""), NotMoreThan10DaysGenerationDate("")> _
    Public Property GenerationDateTime() As DateType
        Get
            CheckDeleted()
            If Row(ApsPublishingLogDAL.COL_NAME_GENERATION_DATE_TIME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ApsPublishingLogDAL.COL_NAME_GENERATION_DATE_TIME), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(ApsPublishingLogDAL.COL_NAME_GENERATION_DATE_TIME, Value)
        End Set
    End Property
	
	
    <ValueMandatory("")> _
    Public Property RecordedDateTime() As DateType
        Get
            CheckDeleted()
            If row(ApsPublishingLogDAL.COL_NAME_RECORDED_DATE_TIME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(ApsPublishingLogDAL.COL_NAME_RECORDED_DATE_TIME), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(ApsPublishingLogDAL.COL_NAME_RECORDED_DATE_TIME, Value)
        End Set
    End Property
	
	
    <ValidStringLength("", Max:=4000)> _
    Public Property ExtendedContent2() As String
        Get
            CheckDeleted()
            If row(ApsPublishingLogDAL.COL_NAME_EXTENDED_CONTENT2) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ApsPublishingLogDAL.COL_NAME_EXTENDED_CONTENT2), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ApsPublishingLogDAL.COL_NAME_EXTENDED_CONTENT2, Value)
        End Set
    End Property
	
	
   

#End Region

#Region "Public Members"
    Public Overrides Sub Save()         
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New ApsPublishingLogDAL
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

    Public Shared Function GetAPSPublishingLogsList(ByVal Header As String, _
                                  ByVal Code As String, _
                                  ByVal MachineName As String, _
                                  ByVal UserName As String, _
                                  ByVal TypeName As String, _
                                  ByVal TableName As String, _
                                  ByVal generationDate As SearchCriteriaStructType(Of Date)) As APSOracleLogsSearchDV

        Try

            Return New APSOracleLogsSearchDV((New ApsPublishingLogDAL(TableName).LoadList(Header, Code, MachineName, UserName, TypeName, generationDate, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0)))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
#End Region

#Region "Custom Validation"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class NotMoreThan10DaysGenerationDate
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_GENERATION_DATE_NOT_BEYOND_10_DAYS)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim oApsPublishing As ApsPublishingLog = CType(objectToValidate, ApsPublishingLog)
            Dim ts As TimeSpan = (DateTime.Now - oApsPublishing.GenerationDateTime)
            If ts.Days > 10 Then
                Return False
            End If
            Return True
        End Function
    End Class

#End Region

#Region "DataView Retrieveing Methods"

#End Region

#Region "APS Publishing Search Dataview"
    Public Class APSOracleLogsSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_NAME_HEADER As String = ApsPublishingLogDAL.COL_NAME_HEADER
        Public Const COL_NAME_TYPE As String = ApsPublishingLogDAL.COL_NAME_TYPE
        Public Const COL_NAME_GENERATION_DATE_TIIME As String = ApsPublishingLogDAL.COL_NAME_GENERATION_DATE_TIME
        Public Const COL_NAME_MACHINE_NAME As String = ApsPublishingLogDAL.COL_NAME_MACHINE_NAME
        Public Const COL_NAME_USER_NAME As String = ApsPublishingLogDAL.COL_NAME_USER_NAME
        Public Const COL_NAME_CODE As String = ApsPublishingLogDAL.COL_NAME_CODE
        Public Const COL_NAME_APPLICATION_NAME As String = ApsPublishingLogDAL.COL_NAME_APPLICATION_NAME
        Public Const COL_NAME_EXTENDED_CONTENT As String = ApsPublishingLogDAL.COL_NAME_EXTENDED_CONTENT
        Public Const COL_NAME_EXTENDED_CONTENT2 As String = ApsPublishingLogDAL.COL_NAME_EXTENDED_CONTENT2
#End Region

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property Header(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_HEADER), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property Type(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_TYPE).ToString
            End Get
        End Property

        Public Shared ReadOnly Property GenerationDateTime(ByVal row As DataRow) As Date
            Get
                Return row(COL_NAME_GENERATION_DATE_TIIME).ToString
            End Get
        End Property

        Public Shared ReadOnly Property MachineName(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_MACHINE_NAME).ToString
            End Get
        End Property

        Public Shared ReadOnly Property UserName(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_USER_NAME).ToString
            End Get
        End Property

        Public Shared ReadOnly Property Code(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_CODE).ToString
            End Get
        End Property
        Public Shared ReadOnly Property ApplicationName(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_APPLICATION_NAME).ToString
            End Get
        End Property
        Public Shared ReadOnly Property ExendedContent(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_EXTENDED_CONTENT).ToString
            End Get
        End Property
        Public Shared ReadOnly Property ExendedContent2(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_EXTENDED_CONTENT2).ToString
            End Get
        End Property

    End Class

#End Region

End Class


