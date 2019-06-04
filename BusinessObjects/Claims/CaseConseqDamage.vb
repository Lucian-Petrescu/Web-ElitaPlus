'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (11/15/2017)  ********************

Public Class CaseConseqDamage
    Inherits BusinessObjectBase
#Region "Constants"
    Public Const CLAIM_ISSUE_LIST As String = "CLMISSUESTATUS"
#End Region

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
    Public Sub New(ByVal id As Guid, ByVal familyDs As DataSet)
        MyBase.New(False)
        Dataset = familyDs
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDs As DataSet)
        MyBase.New(False)
        Dataset = familyDs
        Load()
    End Sub

    Public Sub New(row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            If Dataset.Tables.IndexOf(CaseConseqDamageDal.TableName) < 0 Then
                Dim dal As New CaseConseqDamageDal
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(CaseConseqDamageDal.TableName).NewRow
            Dataset.Tables(CaseConseqDamageDal.TableName).Rows.Add(newRow)
            Row = newRow
            SetValue(CaseConseqDamageDal.TableKeyName, Guid.NewGuid)
            Initialize()
        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try

            If _isDSCreator Then
                If Not Row Is Nothing Then
                    Dataset.Tables(CaseConseqDamageDal.TableName).Rows.Remove(Row)
                End If
            End If
            Row = Nothing
            If Dataset.Tables.IndexOf(CaseConseqDamageDal.TableName) >= 0 Then
                Row = FindRow(id, CaseConseqDamageDal.TableKeyName, Dataset.Tables(CaseConseqDamageDal.TableName))
            End If
            If Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                Dim dal As New CaseConseqDamageDal
                dal.Load(Dataset, id)
                Row = FindRow(id, CaseConseqDamageDal.TableKeyName, Dataset.Tables(CaseConseqDamageDal.TableName))
            End If
            If Row Is Nothing Then
                Throw New DataNotFoundException
            End If
        Catch ex As DataBaseAccessException
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
            If Row(CaseConseqDamageDal.TableKeyName) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CaseConseqDamageDal.ColNameCaseConseqDamageId), Byte()))
            End If
        End Get
    End Property


    Public Property CaseId() As Guid
        Get
            CheckDeleted()
            If Row(CaseConseqDamageDal.ColNameCaseId) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CaseConseqDamageDal.ColNameCaseId), Byte()))
            End If
        End Get
        Set(ByVal value As Guid)
            CheckDeleted()
            SetValue(CaseConseqDamageDal.ColNameCaseId, value)
        End Set
    End Property



    Public Property ClaimId() As Guid
        Get
            CheckDeleted()
            If Row(CaseConseqDamageDal.ColNameClaimId) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CaseConseqDamageDal.ColNameClaimId), Byte()))
            End If
        End Get
        Set(ByVal value As Guid)
            CheckDeleted()
            SetValue(CaseConseqDamageDal.ColNameClaimId, value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=200)>
    Public Property ConseqDamageXcd() As String
        Get
            CheckDeleted()
            If Row(CaseConseqDamageDal.ColNameConseqDamageXcd) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CaseConseqDamageDal.ColNameConseqDamageXcd), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            SetValue(CaseConseqDamageDal.ColNameConseqDamageXcd, value)
        End Set
    End Property



    Public Property CoverageConseqDamageId() As Guid
        Get
            CheckDeleted()
            If Row(CaseConseqDamageDal.ColNameCoverageConseqDamageId) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CaseConseqDamageDal.ColNameCoverageConseqDamageId), Byte()))
            End If
        End Get
        Set(ByVal value As Guid)
            CheckDeleted()
            SetValue(CaseConseqDamageDal.ColNameCoverageConseqDamageId, value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=200)>
    Public Property StatusXcd() As String
        Get
            CheckDeleted()
            If Row(CaseConseqDamageDal.ColNameStatusXcd) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CaseConseqDamageDal.ColNameStatusXcd), String)
            End If
        End Get
        Set(ByVal value As String)
            CheckDeleted()
            SetValue(CaseConseqDamageDal.ColNameStatusXcd, value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property RequestedAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(CaseConseqDamageDal.ColNameRequestedAmount) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CaseConseqDamageDal.ColNameRequestedAmount), Decimal))
            End If
        End Get
        Set(ByVal value As DecimalType)
            CheckDeleted()
            SetValue(CaseConseqDamageDal.ColNameRequestedAmount, value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property ApprovedAmount() As DecimalType
        Get
            CheckDeleted()
            If Row(CaseConseqDamageDal.ColNameApprovedAmount) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CaseConseqDamageDal.ColNameApprovedAmount), Decimal))
            End If
        End Get
        Set(ByVal value As DecimalType)
            CheckDeleted()
            SetValue(CaseConseqDamageDal.ColNameApprovedAmount, value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New CaseConseqDamageDal
                dal.Update(Row)
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Id
                    Dataset = New DataSet
                    Row = Nothing
                    Load(objId)
                End If
            End If
        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
#End Region

#Region "DataView Retrieveing Methods"
    Shared Function LoadListConsequentialDamage(ByVal claimId As Guid) As DataView
        Try
            Dim dal As New CaseConseqDamageDal
            Dim ds As DataSet

            ds = dal.LoadListConsequentialDamage(claimId, Authentication.LangId)
            Return (ds.Tables(CaseConseqDamageDal.TableName).DefaultView)
        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#Region "Consequential Damage Issues Dataview"

    Public Function GetConsequentialDamageIssuesView() As ConsequentialDamageIssuesView


        Dim t As DataTable = ConsequentialDamageIssuesView.CreateTable
        Dim detail As ClaimIssue
        Dim filteredTable As DataTable

        Try

            For Each detail In Me.ConseqDamageIssueList
                Dim row As DataRow = t.NewRow
                row(ConsequentialDamageIssuesView.COL_ENTITY_ISSUE_ID) = detail.ClaimIssueId.ToByteArray
                row(ConsequentialDamageIssuesView.COL_ISSUE_DESC) = detail.IssueDescription
                row(ConsequentialDamageIssuesView.COL_CREATED_BY) = detail.CreatedBy
                row(ConsequentialDamageIssuesView.COL_CREATED_DATE) = detail.CreatedDate
                row(ConsequentialDamageIssuesView.COL_PROCESSED_BY) = detail.ProcessedBy
                row(ConsequentialDamageIssuesView.COL_PROCESSED_DATE) = detail.ProcessedDate
                row(ConsequentialDamageIssuesView.COL_STATUS) = LookupListNew.GetDescriptionFromId(CLAIM_ISSUE_LIST, detail.StatusId)
                row(ConsequentialDamageIssuesView.COL_STATUS_CODE) = detail.StatusCode
                t.Rows.Add(row)

            Next

            Return New ConsequentialDamageIssuesView(t)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Public Class ConsequentialDamageIssuesView
        Inherits DataView
        Public Const COL_ENTITY_ISSUE_ID As String = "entity_issue_id"
        Public Const COL_ISSUE_DESC As String = "Issue_description"
        Public Const COL_CREATED_DATE As String = "Created_Date"
        Public Const COL_CREATED_BY As String = "Created_By"
        Public Const COL_PROCESSED_DATE As String = "Processed_Date"
        Public Const COL_PROCESSED_BY As String = "Processed_By"
        Public Const COL_STATUS As String = "Status"
        Public Const COL_STATUS_ID As String = "Status_Id"
        Public Const COL_STATUS_CODE As String = "Status_Code"


        Public Sub New(ByVal Table As DataTable)
            MyBase.New(Table)
        End Sub

        Public Shared Function CreateTable() As DataTable
            Dim t As New DataTable
            t.Columns.Add(COL_ENTITY_ISSUE_ID, GetType(Byte()))
            t.Columns.Add(COL_ISSUE_DESC, GetType(String))
            t.Columns.Add(COL_CREATED_DATE, GetType(String))
            t.Columns.Add(COL_CREATED_BY, GetType(String))
            t.Columns.Add(COL_PROCESSED_DATE, GetType(String))
            t.Columns.Add(COL_PROCESSED_BY, GetType(String))
            t.Columns.Add(COL_STATUS, GetType(String))
            t.Columns.Add(COL_STATUS_CODE, GetType(String))
            Return t
        End Function
    End Class
#End Region
#End Region
#Region "Consequential Issues"
    Public ReadOnly Property ConseqDamageIssueList() As ClaimIssue.ConseqDamageIssueList
        Get
            Return New ClaimIssue.ConseqDamageIssueList(Me)
        End Get
    End Property

    Public ReadOnly Property IssuesStatus() As String
        Get
            Return EvaluateIssues()
        End Get
    End Property

    Public ReadOnly Property HasIssues() As Boolean
        Get
            If (Me.ConseqDamageIssueList.Count > 0) Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    Public ReadOnly Property AllIssuesResolvedOrWaived() As Boolean
        Get
            If HasIssues Then
                Dim issueStatus As String = EvaluateIssues()
                If (issueStatus = Codes.CONSEQUENTIAL_DAMAGE_STATUS__APPROVED) Then
                    Return True
                End If
                Return False
            End If
            Return False
        End Get
    End Property

    Private Function EvaluateIssues() As String
        Dim issuesStatus As String = Codes.CONSEQUENTIAL_DAMAGE_STATUS__APPROVED

        For Each Item As ClaimIssue In Me.ConseqDamageIssueList
            If (Item.StatusCode = Codes.CLAIMISSUE_STATUS__REJECTED) Then
                issuesStatus = Codes.CONSEQUENTIAL_DAMAGE_STATUS__DENIED
                Exit For
            End If
            If (Item.StatusCode = Codes.CLAIMISSUE_STATUS__OPEN Or Item.StatusCode = Codes.CLAIMISSUE_STATUS__PENDING) Then
                issuesStatus = Codes.CONSEQUENTIAL_DAMAGE_STATUS__REQUESTED
            End If
        Next

        Return issuesStatus
    End Function
#End Region
End Class


