'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (7/26/2012)  ********************

Public Class ClaimIssueStatus
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
            Dim dal As New ClaimIssueStatusDAL
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
            Dim dal As New ClaimIssueStatusDAL
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
            If row(ClaimIssueStatusDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimIssueStatusDAL.COL_NAME_CLAIM_ISSUE_STATUS_ID), Byte()))
            End If
        End Get
    End Property

    Private _claimIssue As ClaimIssue

    Public Property ClaimIssue As ClaimIssue
        Get
            If (_claimIssue Is Nothing) Then
                If Not Me.ClaimIssueId.Equals(Guid.Empty) Then
                    Me.ClaimIssue = New ClaimIssue(Me.ClaimIssueId, Me.Dataset)
                End If
            End If
            Return _claimIssue
        End Get
        Private Set(ByVal value As ClaimIssue)
            _claimIssue = value
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property ClaimIssueId() As Guid
        Get
            CheckDeleted()
            If row(ClaimIssueStatusDAL.COL_NAME_CLAIM_ISSUE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimIssueStatusDAL.COL_NAME_CLAIM_ISSUE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimIssueStatusDAL.COL_NAME_CLAIM_ISSUE_ID, Value)
            Me.ClaimIssue = Nothing
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ClaimIssueStatusCId() As Guid
        Get
            CheckDeleted()
            If row(ClaimIssueStatusDAL.COL_NAME_CLAIM_ISSUE_STATUS_C_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimIssueStatusDAL.COL_NAME_CLAIM_ISSUE_STATUS_C_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimIssueStatusDAL.COL_NAME_CLAIM_ISSUE_STATUS_C_ID, Value)
        End Set
    End Property


    Public Property Comments() As String

        Get
            CheckDeleted()
            If Row(ClaimIssueStatusDAL.COL_NAME_COMMENTS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimIssueStatusDAL.COL_NAME_COMMENTS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimIssueStatusDAL.COL_NAME_COMMENTS, Value)
        End Set
    End Property

    Public Property ProcessedDate() As DateType
        Get
            CheckDeleted()            
            If Row(ClaimIssueStatusDAL.COL_NAME_CREATED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(ClaimIssueStatusDAL.COL_NAME_CREATED_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(ClaimIssueStatusDAL.COL_NAME_CREATED_DATE, Value)
        End Set
    End Property
    Public Property IssueProcessReasonId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimIssueStatusDAL.COL_NAME_ISSUE_PROCESS_REASON_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimIssueStatusDAL.COL_NAME_ISSUE_PROCESS_REASON_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimIssueStatusDAL.COL_NAME_ISSUE_PROCESS_REASON_ID, Value)
        End Set
    End Property


    Public Property ProcessedBy() As String
        Get
            CheckDeleted()
            If Row(ClaimIssueStatusDAL.COL_NAME_PROCESSED_BY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimIssueStatusDAL.COL_NAME_PROCESSED_BY), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimIssueStatusDAL.COL_NAME_PROCESSED_BY, Value)
        End Set        
    End Property

    Public Property CreatedBy() As String
        Get
            CheckDeleted()
            If Row(ClaimIssueStatusDAL.COL_NAME_CREATED_BY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimIssueStatusDAL.COL_NAME_CREATED_BY), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimIssueStatusDAL.COL_NAME_CREATED_BY, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New ClaimIssueStatusDAL
                dal.Update(Me.Row)

                'argumentsToAddEvent = PublishedTask.REGISTRATION_ID & ":" & DALBase.GuidToSQLString(custRegBO.Id) & ";" & PublishedTask.REGISTRATION_ITEM_ID & ":" & DALBase.GuidToSQLString(custItemBO.Id)
                'sender:="CustomerRegistrationSvc_ActivateItem",
                Me.PublishEvent()

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

    Friend Sub PublishEvent()
        With Me.ClaimIssue.Claim
            Dim argumentsToAddEvent As String = "ClaimId:" & DALBase.GuidToSQLString(.Id) & ";ClaimIssueId:" & DALBase.GuidToSQLString(Me.ClaimIssueId)
            PublishedTask.AddEvent(
                companyGroupId:=.Company.CompanyGroupId,
                companyId:=.CompanyId,
                countryId:=.Company.CountryId,
                dealerId:=.Dealer.Id,
                productCode:=.Certificate.ProductCode,
                coverageTypeId:=.CertificateItemCoverage.CoverageTypeId,
                sender:="ClaimIssue_StatusChange",
                arguments:=argumentsToAddEvent,
                eventDate:=DateTime.UtcNow,
                eventTypeId:=Me.EventTypeId,
                eventArgumentId:=Me.ClaimIssue.Issue.Id)
        End With
    End Sub

    Private ReadOnly Property ClaimIssueStatusCode As String
        Get
            If (Me.ClaimIssueStatusCId = Guid.Empty) Then
                Return Nothing
            Else
                Return LookupListNew.GetCodeFromId(Codes.CLAIM_ISSUE_STATUS, Me.ClaimIssueStatusCId)
            End If
        End Get
    End Property

    Private ReadOnly Property EventTypeId As Guid
        Get
            Select Case Me.ClaimIssueStatusCode
                Case Codes.CLAIM_ISSUE_STATUS__WAIVED
                    Return LookupListNew.GetIdFromCode(Codes.EVNT_TYP, Codes.EVNT_TYP__ISSUE_WAIVED)
                Case Codes.CLAIM_ISSUE_STATUS__RESOLVED
                    Return LookupListNew.GetIdFromCode(Codes.EVNT_TYP, Codes.EVNT_TYP__ISSUE_RESOLVED)
                Case Codes.CLAIM_ISSUE_STATUS__REJECTED
                    Return LookupListNew.GetIdFromCode(Codes.EVNT_TYP, Codes.EVNT_TYP__ISSUE_REJECTED)
                Case Codes.CLAIM_ISSUE_STATUS__PENDING
                    Return LookupListNew.GetIdFromCode(Codes.EVNT_TYP, Codes.EVNT_TYP__ISSUE_PENDING)
                Case Codes.CLAIM_ISSUE_STATUS__OPEN
                    Return LookupListNew.GetIdFromCode(Codes.EVNT_TYP, Codes.EVNT_TYP__ISSUE_OPENED)
                Case Codes.CLAIM_ISSUE_STATUS__CLOSED
                    Return LookupListNew.GetIdFromCode(Codes.EVNT_TYP, Codes.EVNT_TYP__ISSUE_CLOSED)
                Case Else
                    Throw New NotSupportedException()
            End Select

        End Get
    End Property

#End Region

#Region "DataView Retrieveing Methods"

#End Region

#Region "Claim Issue Status List Selection View"
    Public Class ClaimIssueStatusList
        Inherits BusinessObjectListBase

        Public Sub New(ByVal parent As ClaimIssue)
            MyBase.New(LoadTable(parent), GetType(ClaimIssueStatus), parent)
        End Sub

        Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
            Return CType(bo, ClaimIssueStatus).ClaimIssueId.Equals(CType(Parent, ClaimIssue).ClaimIssueId)
        End Function

        Private Shared Function LoadTable(ByVal parent As ClaimIssue) As DataTable
            Try
                If Not parent.IsChildrenCollectionLoaded(GetType(ClaimIssueStatusList)) Then
                    Dim dal As New ClaimIssueStatusDAL
                    If parent.Dataset.Tables.Contains(ClaimIssueStatusDAL.TABLE_NAME) Then
                        parent.Dataset.Tables.Remove(ClaimIssueStatusDAL.TABLE_NAME)
                    End If
                    dal.LoadList(parent.Dataset, parent.ClaimIssueId)
                    parent.AddChildrenCollection(GetType(ClaimIssueStatusList))
                End If
                Return parent.Dataset.Tables(ClaimIssueStatusDAL.TABLE_NAME)
            Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End Function

    End Class
#End Region


End Class


