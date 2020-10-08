'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (5/24/2012)  ********************

Public Class Rule
    Inherits BusinessObjectBase
    Implements IExpirable


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
            Dim dal As New RuleDAL
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
            Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(id As Guid)
        Try
            Dim dal As New RuleDAL
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
        Code = String.Empty
        Description = String.Empty
        RuleExecutionPoint = String.Empty
        Effective = Date.Now
        Expiration = New Date(2499, 12, 31, 23, 59, 59)
    End Sub
#End Region


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid Implements IExpirable.ID
        Get
            If Row(RuleDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(RuleDAL.COL_NAME_RULE_ID), Byte()))
            End If
        End Get
    End Property

    ', ValidateDuplicateCode("")
    <ValueMandatory(""), ValidStringLength("", Max:=1020)> _
    Public Property Code As String Implements IExpirable.Code
        Get
            CheckDeleted()
            If Row(RuleDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RuleDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(RuleDAL.COL_NAME_CODE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=4000)> _
    Public Property Description As String
        Get
            CheckDeleted()
            If Row(RuleDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RuleDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(RuleDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property



    Public Property RuleTypeId As Guid
        Get
            CheckDeleted()
            If Row(RuleDAL.COL_NAME_RULE_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(RuleDAL.COL_NAME_RULE_TYPE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(RuleDAL.COL_NAME_RULE_TYPE_ID, Value)
        End Set
    End Property



    Public Property RuleCategoryId As Guid
        Get
            CheckDeleted()
            If Row(RuleDAL.COL_NAME_RULE_CATEGORY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(RuleDAL.COL_NAME_RULE_CATEGORY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(RuleDAL.COL_NAME_RULE_CATEGORY_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1020)> _
    Public Property RuleExecutionPoint As String
        Get
            CheckDeleted()
            If Row(RuleDAL.COL_NAME_RULE_EXECUTION_POINT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RuleDAL.COL_NAME_RULE_EXECUTION_POINT), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(RuleDAL.COL_NAME_RULE_EXECUTION_POINT, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=4000)> _
    Public Property RuleDataSet As String
        Get
            CheckDeleted()
            If Row(RuleDAL.COL_NAME_RULE_DATA_SET) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RuleDAL.COL_NAME_RULE_DATA_SET), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(RuleDAL.COL_NAME_RULE_DATA_SET, Value)
        End Set
    End Property


    <ValueMandatory(""), NonPastDateValidation(Codes.EFFECTIVE)> _
    Public Property Effective As DateTimeType Implements IExpirable.Effective
        Get
            CheckDeleted()
            If Row(RuleDAL.COL_NAME_EFFECTIVE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateTimeType(DateHelper.GetDateValue(Row(RuleDAL.COL_NAME_EFFECTIVE).ToString()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(RuleDAL.COL_NAME_EFFECTIVE, Value)
        End Set
    End Property


    <ValueMandatory(""), NonPastDateValidation(Codes.EXPIRATION), EffectiveExpirationDateValidation(Codes.EXPIRATION)> _
    Public Property Expiration As DateTimeType Implements IExpirable.Expiration
        Get
            CheckDeleted()
            If Row(RuleDAL.COL_NAME_EXPIRATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateTimeType(DateHelper.GetDateValue(Row(RuleDAL.COL_NAME_EXPIRATION).ToString()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(RuleDAL.COL_NAME_EXPIRATION, Value)
        End Set
    End Property




#End Region


#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New RuleDAL
                dal.UpdateFamily(Dataset)
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

    Public Overrides ReadOnly Property IsDirty As Boolean
        Get
            Return MyBase.IsDirty OrElse IsChildrenDirty
        End Get
    End Property

    Public Sub Copy(original As Assurant.ElitaPlus.BusinessObjectsNew.Rule)
        If Not IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Rule")
        End If
        MyBase.CopyFrom(original)
        Effective = Date.Now
        Expiration = New Date(2499, 12, 31, 23, 59, 59)
        'copy the children

        'Rule issue
        For Each detail As RuleIssue In original.IssueRuleChildren
            Dim newDetail As RuleIssue = IssueRuleChildren.GetNewChild
            Dim tempGuid As Guid = newDetail.Id
            newDetail.Copy(detail)
            newDetail.RuleId = Id
            newDetail.IssueId = detail.IssueId
            newDetail.Effective = Effective
            newDetail.Expiration = Expiration
            newDetail.Save()
        Next

        'Rule Process
        For Each detail As RuleProcess In original.ProcessRuleChildren
            Dim newDetail As RuleProcess = ProcessRuleChildren.GetNewChild
            newDetail.Copy(detail)
            newDetail.RuleId = Id
            newDetail.ProcessId = detail.ProcessId
            newDetail.Effective = Effective.Value
            newDetail.Expiration = Expiration.Value
            newDetail.Save()
        Next
    End Sub

    Public Overrides ReadOnly Property IsNew As Boolean Implements IExpirable.IsNew
        Get
            Return MyBase.IsNew
        End Get
    End Property


    Function GetAvailableIssues() As DataView
        Try
            Dim issueListDal As New Issue
            Return issueListDal.GetList("*", "*", Date.Now)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Function

    Function GetAvailableProcesses() As DataView
        Try
            Dim processListDal As New Process
            Return processListDal.GetList("*", "*", Date.Now)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Function

    Public Function IsIssueAssignedtoRule() As Boolean
        Try
            Return (New RuleDAL).IsIssueAssignedtoRule(Id)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)

        End Try
    End Function

#End Region

#Region "DataView Retrieveing Methods"

#End Region


#Region "Visitor"
    ''' <summary>
    ''' Accept member of IElement interface
    ''' </summary>
    ''' <param name="Visitor"></param>
    ''' <returns>Returns True if Overlapping Records are found</returns>
    ''' <remarks></remarks>
    Public Function Accept(ByRef Visitor As IVisitor) As Boolean Implements IElement.Accept
        Try
            Return Visitor.Visit(Me)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

#Region "Expiring Logic"

    Public Function ExpireOverLappingRules() As Boolean
        Try
            Dim overlap As New OverlapValidationVisitorDAL
            Dim ds As New DataSet
            ds = overlap.LoadList(Id, [GetType].Name, Code, Effective, Expiration, Guid.Empty)
            If ds.Tables(0).Rows.Count > 0 Then
                For Each dtrow As DataRow In ds.Tables(0).Rows
                    Dim rId As Guid = New Guid(CType(dtrow(RuleDAL.COL_NAME_RULE_ID), Byte()))
                    Dim ExpRule As New Rule(rId, Dataset)

                    If Effective.Value < ExpRule.Expiration.Value Then
                        'Expire overlapping Rule 1 second before current Rule
                        ExpRule.Accept(New ExpirationVisitor(Effective))
                    End If

                    'If ExpQuestion.IsDirty Then
                    '    ExpQuestion.Save()
                    'End If
                Next
                Return True         'expired successfully
            End If
            Return False
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#End Region

    'dummy property
    Private Property parent_id As Guid Implements IExpirable.parent_id
        Get
            Return Guid.Empty
        End Get
        Set
            'do nothing
        End Set
    End Property


#Region "Search Form related"
    Public Class RuleSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_NAME_RULE_ID As String = RuleDAL.COL_NAME_RULE_ID
        Public Const COL_NAME_DESCRIPTION As String = RuleDAL.COL_NAME_DESCRIPTION
        Public Const COL_NAME_CODE As String = RuleDAL.COL_NAME_CODE
        Public Const COL_NAME_EFFECTIVE As String = RuleDAL.COL_NAME_EFFECTIVE
        Public Const COL_NAME_EXPIRATION As String = RuleDAL.COL_NAME_EXPIRATION
        Public Const COL_NAME_RULE_TYPE As String = "RULE_TYPE"
        Public Const COL_NAME_RULE_CATEGORY As String = "RULE_CATEGORY"
#End Region

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property RuleId(row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_RULE_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property Code(row As DataRow) As String
            Get
                Return row(COL_NAME_CODE).ToString
            End Get
        End Property

        Public Shared ReadOnly Property Description(row As DataRow) As String
            Get
                Return row(COL_NAME_DESCRIPTION).ToString
            End Get
        End Property

        Public Shared ReadOnly Property Effective(row) As String
            Get
                Return row(COL_NAME_EFFECTIVE).ToString
            End Get
        End Property

        Public Shared ReadOnly Property Expiration(row) As String
            Get
                Return row(COL_NAME_EXPIRATION).ToString
            End Get
        End Property

        Public Shared ReadOnly Property RuleCategory(row) As String
            Get
                Return row(COL_NAME_RULE_CATEGORY).ToString
            End Get
        End Property

        Public Shared ReadOnly Property RuleType(row) As String
            Get
                Return row(COL_NAME_RULE_TYPE).ToString
            End Get
        End Property

    End Class
#End Region

#Region "Load List"
    Shared Function GetList(Code As String, Description As String, activeon As DateTimeType, _
                                        ruleType As Guid, ruleCategory As Guid, lang_id As Guid) As RuleSearchDV
        Try
            Dim dal As New RuleDAL
            Return New RuleSearchDV(dal.getList(Code, Description, activeon, ruleType, ruleCategory, lang_id))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try

    End Function

    Public Shared Function GetRulesByDealerAndCompany(dealerId As Guid, companyId As Guid) As DataView
        Try
            Dim dal As New RuleDAL
            Return dal.LoadRulesByDealerAndCompnay(dealerId, companyId)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#End Region

#Region "Rule Issue View"
    Public ReadOnly Property IssueRuleChildren As RuleIssue.IssueRuleDetailView
        Get
            Return New RuleIssue.IssueRuleDetailView(Me)
        End Get
    End Property

    Public Function GetIssueRuleSelectionView() As IssueRuleDetailView
        Dim t As DataTable = IssueRuleDetailView.CreateTable

        For Each detail As RuleIssue In IssueRuleChildren
            Dim row As DataRow = t.NewRow

            row(IssueRuleDetailView.COL_NAME_RULE_ISSUE_ID) = detail.Id.ToByteArray
            row(IssueRuleDetailView.COL_NAME_RULE_ID) = detail.RuleId.ToByteArray
            row(IssueRuleDetailView.COL_NAME_ISSUE_ID) = detail.IssueId.ToByteArray
            If IsNew Then
                row(IssueRuleDetailView.COL_NAME_DESCRIPTION) = (New Issue(detail.IssueId)).Description
            Else
                row(IssueRuleDetailView.COL_NAME_DESCRIPTION) = detail.Description
            End If
            row(IssueRuleDetailView.COL_NAME_EFFECTIVE) = detail.Effective.ToString
            row(IssueRuleDetailView.COL_NAME_EFFECTIVE) = detail.Effective.ToString
            row(IssueRuleDetailView.COL_NAME_EXPIRATION) = detail.Expiration.ToString
            t.Rows.Add(row)
        Next
        Return New IssueRuleDetailView(t)
    End Function

    Public Class IssueRuleDetailView
        Inherits DataView
        Public Const COL_NAME_RULE_ISSUE_ID As String = RuleIssueDAL.COL_NAME_RULE_ISSUE_ID
        Public Const COL_NAME_ISSUE_ID As String = RuleIssueDAL.COL_NAME_ISSUE_ID
        Public Const COL_NAME_RULE_ID As String = RuleIssueDAL.COL_NAME_RULE_ID
        Public Const COL_NAME_DESCRIPTION As String = RuleIssueDAL.COL_NAME_DESCRIPTION
        Public Const COL_NAME_EFFECTIVE As String = RuleListDAL.COL_NAME_EFFECTIVE
        Public Const COL_NAME_EXPIRATION As String = RuleListDAL.COL_NAME_EXPIRATION



        Public Sub New(Table As DataTable)
            MyBase.New(Table)
        End Sub

        Public Shared Function CreateTable() As DataTable
            Dim t As New DataTable
            t.Columns.Add(COL_NAME_RULE_ISSUE_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_ISSUE_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_RULE_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_DESCRIPTION, GetType(String))
            t.Columns.Add(COL_NAME_EFFECTIVE, GetType(String))
            t.Columns.Add(COL_NAME_EXPIRATION, GetType(String))

            Return t
        End Function
    End Class

    Public Function GetRuleDetailChild(childId As Guid) As RuleIssue
        Return CType(IssueRuleChildren.GetChild(childId), RuleIssue)
    End Function

    Public Function GetNewRuleDetailChild() As RuleIssue
        Dim newRuleDetail As RuleIssue = CType(IssueRuleChildren.GetNewChild, RuleIssue)
        With newRuleDetail
            .RuleId = Id
            .Effective = DateTime.Now
            .Expiration = Expiration
        End With
        Return newRuleDetail
    End Function

    Sub PopulateIssueList(issuelist As ArrayList)
        Try
            'compare with what we have and what is there in the user control
            'user control will always have the final selection so remove from our list what we don't find
            For Each issuerule As RuleIssue In IssueRuleChildren
                Dim dFound As Boolean = False
                For Each Str As String In issuelist
                    Dim issue_id As Guid = New Guid(Str)
                    If issuerule.IssueId = issue_id Then
                        dFound = True : Exit For
                    End If
                Next
                If Not dFound Then
                    issuerule.BeginEdit()
                    issuerule.Delete()
                    issuerule.EndEdit()
                    issuerule.Save()
                    dFound = True
                End If
            Next
            'next now add those items which are there in user control but we don't have it
            For Each Str As String In issuelist
                Dim dFound As Boolean = False
                For Each issuerule As RuleIssue In IssueRuleChildren
                    Dim issue_id As Guid = New Guid(Str)
                    If issuerule.IssueId = issue_id Then
                        dFound = True : Exit For
                    End If
                Next
                If Not dFound Then
                    Dim newIssuerule As RuleIssue = GetNewRuleDetailChild
                    newIssuerule.BeginEdit()
                    newIssuerule.IssueId = New Guid(Str)
                    newIssuerule.EndEdit()
                    newIssuerule.Save()
                End If
            Next

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
#End Region


#Region "Rule Process View"
    Public ReadOnly Property ProcessRuleChildren As RuleProcess.ProcessRuleDetailView
        Get
            Return New RuleProcess.ProcessRuleDetailView(Me)
        End Get
    End Property

    Public Function GetProcessRuleSelectionView() As ProcessRuleDetailView
        Dim t As DataTable = ProcessRuleDetailView.CreateTable

        For Each detail As RuleProcess In ProcessRuleChildren
            Dim row As DataRow = t.NewRow

            row(ProcessRuleDetailView.COL_NAME_RULE_PROCESS_ID) = detail.Id.ToByteArray
            row(ProcessRuleDetailView.COL_NAME_RULE_ID) = detail.RuleId.ToByteArray
            row(ProcessRuleDetailView.COL_NAME_PROCESS_ID) = detail.ProcessId.ToByteArray
            If IsNew Then
                row(ProcessRuleDetailView.COL_NAME_DESCRIPTION) = (New Process(detail.ProcessId)).Description
            Else
                row(ProcessRuleDetailView.COL_NAME_DESCRIPTION) = detail.Description
            End If
            row(ProcessRuleDetailView.COL_NAME_EFFECTIVE) = detail.Effective.ToString
            row(ProcessRuleDetailView.COL_NAME_EFFECTIVE) = detail.Effective.ToString
            row(ProcessRuleDetailView.COL_NAME_EXPIRATION) = detail.Expiration.ToString
            row(ProcessRuleDetailView.COL_NAME_PROCESS_ORDER) = LongType.Parse(detail.ProcessOrder)

            t.Rows.Add(row)
        Next
        Return New ProcessRuleDetailView(t)
    End Function

    Public Class ProcessRuleDetailView
        Inherits DataView
        Public Const COL_NAME_RULE_PROCESS_ID As String = RuleProcessDAL.COL_NAME_RULE_PROCESS_ID
        Public Const COL_NAME_PROCESS_ID As String = RuleProcessDAL.COL_NAME_PROCESS_ID
        Public Const COL_NAME_RULE_ID As String = RuleProcessDAL.COL_NAME_RULE_ID
        Public Const COL_NAME_DESCRIPTION As String = RuleProcessDAL.COL_NAME_DESCRIPTION
        Public Const COL_NAME_EFFECTIVE As String = RuleProcessDAL.COL_NAME_EFFECTIVE
        Public Const COL_NAME_EXPIRATION As String = RuleProcessDAL.COL_NAME_EXPIRATION
        Public Const COL_NAME_PROCESS_ORDER As String = RuleProcessDAL.COL_NAME_PROCESS_ORDER



        Public Sub New(Table As DataTable)
            MyBase.New(Table)
        End Sub

        Public Shared Function CreateTable() As DataTable
            Dim t As New DataTable
            t.Columns.Add(COL_NAME_RULE_PROCESS_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_PROCESS_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_RULE_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_DESCRIPTION, GetType(String))
            t.Columns.Add(COL_NAME_EFFECTIVE, GetType(String))
            t.Columns.Add(COL_NAME_EXPIRATION, GetType(String))
            t.Columns.Add(COL_NAME_PROCESS_ORDER, GetType(LongType))
            Return t
        End Function
    End Class

    Public Function GetRuleProcessChild(childId As Guid) As RuleProcess
        Return CType(ProcessRuleChildren.GetChild(childId), RuleProcess)
    End Function

    Public Function GetNewRuleProcessChild() As RuleProcess
        Dim newRuleProcess As RuleProcess = CType(ProcessRuleChildren.GetNewChild, RuleProcess)
        With newRuleProcess
            .RuleId = Id
            .Effective = DateTime.Now
            .Expiration = Expiration.Value
        End With
        Return newRuleProcess
    End Function

    Sub PopulateProcessList(processlist As ArrayList)
        Try
            'compare with what we have and what is there in the user control
            'user control will always have the final selection so remove from our list what we don't find
            For Each processrule As RuleProcess In ProcessRuleChildren
                Dim dFound As Boolean = False
                For Each Str As String In processlist
                    Dim process_id As Guid = New Guid(Str)
                    If processrule.ProcessId = process_id Then
                        dFound = True : Exit For
                    End If
                Next
                If Not dFound Then
                    processrule.BeginEdit()
                    processrule.Delete()
                    processrule.EndEdit()
                    processrule.Save()
                    dFound = True
                End If
            Next
            'next now add those items which are there in user control but we don't have it
            For Each Str As String In processlist
                Dim dFound As Boolean = False
                For Each processrule As RuleProcess In ProcessRuleChildren
                    Dim process_id As Guid = New Guid(Str)
                    If processrule.ProcessId = process_id Then
                        dFound = True : Exit For
                    End If
                Next
                If Not dFound Then
                    Dim newProcessrule As RuleProcess = GetNewRuleProcessChild
                    newProcessrule.BeginEdit()
                    newProcessrule.ProcessId = New Guid(Str)
                    newProcessrule.EndEdit()
                    newProcessrule.Save()
                End If
            Next

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
#End Region

End Class


