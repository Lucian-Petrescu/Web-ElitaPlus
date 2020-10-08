'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (4/16/2015)  ********************

Public Class ClaimCloseRules
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
            Dim dal As New ClaimCloseRulesDAL
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
            Dim dal As New ClaimCloseRulesDAL
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
    Public ReadOnly Property Id As Guid
        Get
            If row(ClaimCloseRulesDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimCloseRulesDAL.COL_NAME_CLAIM_CLOSE_RULE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")>
    Public Property CompanyId As Guid
        Get
            CheckDeleted()
            If row(ClaimCloseRulesDAL.COL_NAME_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimCloseRulesDAL.COL_NAME_COMPANY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimCloseRulesDAL.COL_NAME_COMPANY_ID, Value)
        End Set
    End Property

    Public Property DealerId As Guid
        Get
            CheckDeleted()
            If Row(ClaimCloseRulesDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimCloseRulesDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimCloseRulesDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property CloseRuleBasedOnId As Guid
        Get
            CheckDeleted()
            If Row(ClaimCloseRulesDAL.COL_NAME_CLOSE_RULE_BASED_ON_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimCloseRulesDAL.COL_NAME_CLOSE_RULE_BASED_ON_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimCloseRulesDAL.COL_NAME_CLOSE_RULE_BASED_ON_ID, Value)
        End Set
    End Property

    <ValidClaimStatusByGroup("")>
    Public Property ClaimStatusByGroupId As Guid
        Get
            CheckDeleted()
            If Row(ClaimCloseRulesDAL.COL_NAME_CLAIM_STATUS_BY_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimCloseRulesDAL.COL_NAME_CLAIM_STATUS_BY_GROUP_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimCloseRulesDAL.COL_NAME_CLAIM_STATUS_BY_GROUP_ID, Value)
        End Set
    End Property

    <ValidClaimIssue("")>
    Public Property ClaimIssueId As Guid
        Get
            CheckDeleted()
            If Row(ClaimCloseRulesDAL.COL_NAME_CLAIM_ISSUE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimCloseRulesDAL.COL_NAME_CLAIM_ISSUE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimCloseRulesDAL.COL_NAME_CLAIM_ISSUE_ID, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property TimePeriod As LongType
        Get
            CheckDeleted()
            If Row(ClaimCloseRulesDAL.COL_NAME_TIME_PERIOD) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ClaimCloseRulesDAL.COL_NAME_TIME_PERIOD), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimCloseRulesDAL.COL_NAME_TIME_PERIOD, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property ReasonClosedId As Guid
        Get
            CheckDeleted()
            If row(ClaimCloseRulesDAL.COL_NAME_REASON_CLOSED_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimCloseRulesDAL.COL_NAME_REASON_CLOSED_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimCloseRulesDAL.COL_NAME_REASON_CLOSED_ID, Value)
        End Set
    End Property

    Public Property ParentClaimCloseRuleId As Guid
        Get
            CheckDeleted()
            If Row(ClaimCloseRulesDAL.COL_NAME_PARENT_CLAIM_CLOSE_RULE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimCloseRulesDAL.COL_NAME_PARENT_CLAIM_CLOSE_RULE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimCloseRulesDAL.COL_NAME_PARENT_CLAIM_CLOSE_RULE_ID, Value)
        End Set
    End Property


    Public Property ActiveFlag As String
        Get
            CheckDeleted()
            If Row(ClaimCloseRulesDAL.COL_NAME_ACTIVE_FLAG) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimCloseRulesDAL.COL_NAME_ACTIVE_FLAG), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimCloseRulesDAL.COL_NAME_ACTIVE_FLAG, Value)
        End Set
    End Property




#End Region

#Region "Public Members"


    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ClaimCloseRulesDAL
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

    Public Function GetClaimCloseRules() As CloseClaimRulesDV
        Dim claimcloseRulesDAL As New ClaimCloseRulesDAL
        If (DealerId.Equals(Guid.Empty)) Then
            Return New CloseClaimRulesDV(claimcloseRulesDAL.LoadClaimCloseRulesByCompany(CompanyId).Tables(0))
        Else
            Return New CloseClaimRulesDV(claimcloseRulesDAL.LoadClaimCloseRulesByDealer(CompanyId, DealerId).Tables(0))
        End If

    End Function

    Public Shared Function UpdateClaimCloseRuleInActive(claimCloseRuleId As Guid) As Integer
        Dim claimcloseRulesDAL As New ClaimCloseRulesDAL
        Return claimcloseRulesDAL.UpdateClaimRuleInactive(claimCloseRuleId)

    End Function

    'Def-25716: Added function to validate if the claim close rules is already exists.
    Public Shared Function ValidateClaimCloseRule(companyId As Guid, dealerId As Guid, closeRuleBasedOnId As Guid, claimStatusByGroupId As Guid, entityType As String, claimIssueId As Guid) As Integer
        Dim claimcloseRulesDAL As New ClaimCloseRulesDAL
        Return claimcloseRulesDAL.ValidateClaimRule(companyId, dealerId, closeRuleBasedOnId, claimStatusByGroupId, entityType, claimIssueId)

    End Function
    Public Shared Function CopyClaimCloseRulesToNewCompany(OldcompanyId As Guid, NewCompanyId As Guid) As Integer
        Dim claimcloseRulesDAL As New ClaimCloseRulesDAL
        claimcloseRulesDAL.CopyClaimCloseRulesToNewCompany(OldcompanyId, NewCompanyId)

    End Function

    Public Shared Function CopyClaimCloseRulesToNewDealer(companyId As Guid, OlddealerId As Guid, NewDealerId As Guid) As Integer
        Dim claimcloseRulesDAL As New ClaimCloseRulesDAL
        claimcloseRulesDAL.CopyClaimCloseRulesToNewDealer(companyId, OlddealerId, NewDealerId)

    End Function
#End Region

#Region "DataView Retrieveing Methods"

#End Region

    Public Class CloseClaimRulesDV
        Inherits DataView

        Public Const COL_CLAIM_CLOSE_RULE_ID As String = "claim_close_rule_id"
        Public Const COL_COMPANY_ID As String = "company_id"
        Public Const COL_COMPANY_CODE As String = "company_code"
        Public Const COL_DEALER_ID As String = "dealer_id"
        Public Const COL_DEALER As String = "dealer"
        Public Const COL_CLOSE_RULE_BASED_ON_ID As String = "close_rule_based_on_id"
        Public Const COL_CLOSE_RULE_BASED_ON As String = "close_rule_based_on"
        Public Const COL_CLAIM_STATUS_BY_GROUP_ID As String = "claim_status_by_group_id"
        Public Const COL_CLAIM_STATUS_BY_GROUP As String = "claim_status_by_group"
        Public Const COL_CLAIM_ISSUE_ID As String = "claim_issue_id"
        Public Const COL_CLAIM_ISSUE As String = "claim_issue"
        Public Const COL_TIME_PERIOD As String = "time_period"
        Public Const COL_REASON_CLOSED_ID As String = "reason_closed_id"
        Public Const COL_REASON_CLOSED As String = "reason_closed"
        Public Const COL_PARENT_CLAIM_CLOSE_RULE_ID As String = "parent_claim_close_rule_id"
        Public Const COL_ACTIVE_FLAG As String = "active_flag"

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

    Public Shared Sub AddNewRowToSearchDV(ByRef dv As CloseClaimRulesDV, NewBO As ClaimCloseRules)
        Dim dt As DataTable, blnEmptyTbl As Boolean = False

        If NewBO.IsNew Then
            Dim row As DataRow
            If dv Is Nothing Then
                Dim guidTemp As New Guid
                blnEmptyTbl = True
                dt = New DataTable
                dt.Columns.Add(CloseClaimRulesDV.COL_CLAIM_CLOSE_RULE_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(CloseClaimRulesDV.COL_COMPANY_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(CloseClaimRulesDV.COL_COMPANY_CODE, GetType(String))
                dt.Columns.Add(CloseClaimRulesDV.COL_DEALER_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(CloseClaimRulesDV.COL_DEALER, GetType(String))
                dt.Columns.Add(CloseClaimRulesDV.COL_CLOSE_RULE_BASED_ON_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(CloseClaimRulesDV.COL_CLOSE_RULE_BASED_ON, GetType(String))
                dt.Columns.Add(CloseClaimRulesDV.COL_CLAIM_STATUS_BY_GROUP_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(CloseClaimRulesDV.COL_CLAIM_STATUS_BY_GROUP, GetType(String))
                dt.Columns.Add(CloseClaimRulesDV.COL_CLAIM_ISSUE_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(CloseClaimRulesDV.COL_CLAIM_ISSUE, GetType(String))
                dt.Columns.Add(CloseClaimRulesDV.COL_TIME_PERIOD, GetType(String))
                dt.Columns.Add(CloseClaimRulesDV.COL_REASON_CLOSED_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(CloseClaimRulesDV.COL_REASON_CLOSED, GetType(String))
                dt.Columns.Add(CloseClaimRulesDV.COL_PARENT_CLAIM_CLOSE_RULE_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(CloseClaimRulesDV.COL_ACTIVE_FLAG, GetType(String))

            Else
                dt = dv.Table
            End If
            row = dt.NewRow
            row(CloseClaimRulesDV.COL_CLAIM_CLOSE_RULE_ID) = NewBO.Id.ToByteArray
            row(CloseClaimRulesDV.COL_COMPANY_ID) = NewBO.CompanyId.ToByteArray
            row(CloseClaimRulesDV.COL_DEALER_ID) = NewBO.DealerId.ToByteArray
            row(CloseClaimRulesDV.COL_CLOSE_RULE_BASED_ON_ID) = NewBO.CloseRuleBasedOnId.ToByteArray
            row(CloseClaimRulesDV.COL_CLAIM_STATUS_BY_GROUP_ID) = NewBO.ClaimStatusByGroupId.ToByteArray
            row(CloseClaimRulesDV.COL_CLAIM_ISSUE_ID) = NewBO.ClaimIssueId.ToByteArray
            row(CloseClaimRulesDV.COL_REASON_CLOSED_ID) = NewBO.ReasonClosedId.ToByteArray
            row(CloseClaimRulesDV.COL_PARENT_CLAIM_CLOSE_RULE_ID) = NewBO.ParentClaimCloseRuleId.ToByteArray

            dt.Rows.Add(row)
            If blnEmptyTbl Then dv = New CloseClaimRulesDV(dt)
        End If
    End Sub

    Public Shared Function getEmptyList(dv As DataView) As System.Data.DataView
        Try

            Dim dsv As DataSet
            dsv = dv.Table().DataSet

            Dim row As DataRow = dsv.Tables(0).NewRow()
            row.Item(CloseClaimRulesDV.COL_CLAIM_CLOSE_RULE_ID) = Guid.NewGuid.ToByteArray

            row(CloseClaimRulesDV.COL_COMPANY_ID) = Guid.NewGuid.ToByteArray
            row.Item(CloseClaimRulesDV.COL_COMPANY_CODE) = String.Empty

            row(CloseClaimRulesDV.COL_DEALER_ID) = Guid.NewGuid.ToByteArray
            row.Item(CloseClaimRulesDV.COL_DEALER) = String.Empty

            row(CloseClaimRulesDV.COL_CLOSE_RULE_BASED_ON_ID) = Guid.NewGuid.ToByteArray
            row.Item(CloseClaimRulesDV.COL_CLOSE_RULE_BASED_ON) = String.Empty

            row(CloseClaimRulesDV.COL_CLAIM_STATUS_BY_GROUP_ID) = Guid.NewGuid.ToByteArray
            row.Item(CloseClaimRulesDV.COL_CLAIM_STATUS_BY_GROUP) = String.Empty

            row(CloseClaimRulesDV.COL_CLAIM_ISSUE_ID) = Guid.NewGuid.ToByteArray
            row.Item(CloseClaimRulesDV.COL_CLAIM_ISSUE) = String.Empty

            row(CloseClaimRulesDV.COL_TIME_PERIOD) = 0D

            row(CloseClaimRulesDV.COL_REASON_CLOSED_ID) = Guid.NewGuid.ToByteArray
            row(CloseClaimRulesDV.COL_REASON_CLOSED) = String.Empty

            row.Item(CloseClaimRulesDV.COL_PARENT_CLAIM_CLOSE_RULE_ID) = Guid.NewGuid.ToByteArray

            dsv.Tables(0).Rows.Add(row)
            Return New System.Data.DataView(dsv.Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
End Class

<AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
Public NotInheritable Class ValidClaimIssue
    Inherits ValidBaseAttribute

    Public Sub New(fieldDisplayName As String)
        MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_CLAIM_STATUS_ISSUE_COMBINATION_ERR)
    End Sub

    Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
        Dim obj As ClaimCloseRules = CType(objectToValidate, ClaimCloseRules)

        Return ClaimCloseRulesHelper.IsValidClaimStatusIssueCombination(obj.ClaimStatusByGroupId, obj.ClaimIssueId)
    End Function
End Class

<AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
Public NotInheritable Class ValidClaimStatusByGroup
    Inherits ValidBaseAttribute

    Public Sub New(fieldDisplayName As String)
        MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_CLAIM_STATUS_ISSUE_COMBINATION_ERR)
    End Sub

    Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
        Dim obj As ClaimCloseRules = CType(objectToValidate, ClaimCloseRules)

        Return ClaimCloseRulesHelper.IsValidClaimStatusIssueCombination(obj.ClaimStatusByGroupId, obj.ClaimIssueId)
    End Function
End Class

Friend Class ClaimCloseRulesHelper
    Friend Shared Function IsValidClaimStatusIssueCombination(ClaimStatusByGroupId As Guid, ClaimIssueId As Guid) As Boolean
        Return _
            (ClaimStatusByGroupId = Guid.Empty And Not ClaimIssueId = Guid.Empty) Or
            (ClaimIssueId = Guid.Empty And Not ClaimStatusByGroupId = Guid.Empty)
    End Function
End Class



