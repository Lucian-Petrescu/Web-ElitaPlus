'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (5/25/2012)  ********************

Public Class RuleList
    Inherits BusinessObjectBase
    Implements IExpirable
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
            Dim dal As New RuleListDAL
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
            Dim dal As New RuleListDAL
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
        Effective = Date.Now
        Expiration = New Date(2499, 12, 31, 23, 59, 59)
    End Sub
#End Region


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid Implements IExpirable.ID
        Get
            If Row(RuleListDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(RuleListDAL.COL_NAME_RULE_LIST_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=255)> _
    Public Property Code As String Implements IExpirable.Code
        Get
            CheckDeleted()
            If Row(RuleListDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RuleListDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(RuleListDAL.COL_NAME_CODE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=2000)> _
    Public Property Description As String
        Get
            CheckDeleted()
            If Row(RuleListDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RuleListDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(RuleListDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property


    <ValueMandatory(""), NonPastDateValidation(Codes.EFFECTIVE)> _
    Public Property Effective As DateTimeType Implements IExpirable.Effective
        Get
            CheckDeleted()
            If Row(RuleListDAL.COL_NAME_EFFECTIVE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateTimeType(CType(Row(RuleListDAL.COL_NAME_EFFECTIVE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(RuleListDAL.COL_NAME_EFFECTIVE, Value)
        End Set
    End Property


    <ValueMandatory(""), NonPastDateValidation(Codes.EXPIRATION), EffectiveExpirationDateValidation(Codes.EXPIRATION)> _
    Public Property Expiration As DateTimeType Implements IExpirable.Expiration
        Get
            CheckDeleted()
            If Row(RuleListDAL.COL_NAME_EXPIRATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateTimeType(CType(Row(RuleListDAL.COL_NAME_EXPIRATION), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(RuleListDAL.COL_NAME_EXPIRATION, Value)
        End Set
    End Property

    Public Overrides ReadOnly Property IsNew As Boolean Implements IExpirable.IsNew
        Get
            Return MyBase.IsNew
        End Get
    End Property

    Public Property parent_id As Guid Implements IExpirable.parent_id
        Get
            Return Nothing
        End Get
        Set
            'do nothing
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New RuleListDAL
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

    Public Shared Function GetList(ByVal code As String, ByVal description As String, ByVal ActiveOn As DateTimeType) As RuleListSearchDV

        Try
            Return New RuleListSearchDV((New RuleListDAL).GetList(code, description, ActiveOn).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Function

    Public Sub Copy(ByVal original As RuleList)
        If Not IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Question")
        End If
        MyBase.CopyFrom(original)
        Effective = Date.Now
        Expiration = New Date(2499, 12, 31, 23, 59, 59)
        ''copy the childrens     
        
        For Each detail As RuleListDetail In original.RuleChildren
            Dim newDetail As RuleListDetail = RuleChildren.GetNewChild
            newDetail.Copy(detail)
            newDetail.RuleListId = Id
            newDetail.Effective = Effective
            newDetail.Expiration = Expiration
            newDetail.Save()
        Next

    End Sub


    Function GetAvailableDealers() As DataView
        Try
            Dim dealerListDal As New DealerRuleListDAL
            'Removed for DEF-2659
            'Dim compIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            'Return dealerListDal.getAvailableDealers(compIds)
            Return dealerListDal.getAvailableDealers()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Function

    Function GetAvailableRules() As DataView
        Try
            Dim ruleListDetaildal As New RuleListDetailDAL
            'DEF-2368 - Added new parameter date
            Return ruleListDetaildal.GetAvailableRules(Date.Now)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Function

    Function GetAvailableCompanys() As DataView
        Try
            Dim companyListDal As New CompanyRuleListDAL
            'Removed for DEF-2659
            'Dim compIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            'Return dealerListDal.getAvailableDealers(compIds)
            Return companyListDal.getAvailableCompanys()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Function

    'Added manually to the code
    Public Overrides ReadOnly Property IsDirty As Boolean
        Get
            Return MyBase.IsDirty OrElse IsChildrenDirty
        End Get
    End Property
#End Region

#Region "DataView Retrieveing Methods"

#End Region

#Region "Rule List Search Dataview"
    Public Class RuleListSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_NAME_RULE_LIST_ID As String = RuleListDAL.COL_NAME_RULE_LIST_ID
        Public Const COL_NAME_DESCRIPTION As String = RuleListDAL.COL_NAME_DESCRIPTION
        Public Const COL_NAME_CODE As String = RuleListDAL.COL_NAME_CODE
        Public Const COL_NAME_EFFECTIVE As String = RuleListDAL.COL_NAME_EFFECTIVE
        Public Const COL_NAME_EXPIRATION As String = RuleListDAL.COL_NAME_EXPIRATION
#End Region

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property RuleListId(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_RULE_LIST_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property Code(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_CODE).ToString
            End Get
        End Property

        Public Shared ReadOnly Property Description(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_DESCRIPTION).ToString
            End Get
        End Property

        Public Shared ReadOnly Property Effective(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_EFFECTIVE).ToString
            End Get
        End Property

        Public Shared ReadOnly Property Expiration(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_EXPIRATION).ToString
            End Get
        End Property

    End Class

#End Region

#Region "Rule List Detail View"
    Public ReadOnly Property RuleChildren As RuleListDetail.RuleListDetailView
        Get
            Return New RuleListDetail.RuleListDetailView(Me)
        End Get
    End Property

    Public Function GetRuleListSelectionView() As RuleListDetailSelectionView
        Dim t As DataTable = RuleListDetailSelectionView.CreateTable

        For Each detail As RuleListDetail In RuleChildren
            Dim row As DataRow = t.NewRow
            row(RuleListDetailSelectionView.COL_NAME_RULE_ID) = detail.RuleId.ToByteArray
            row(RuleListDetailSelectionView.COL_NAME_RULE_LIST_DETAIL_ID) = detail.Id.ToByteArray
            row(RuleListDetailSelectionView.COL_NAME_RULE_LIST_ID) = detail.RuleListId.ToByteArray
            row(RuleListDetailSelectionView.COL_NAME_DESCRIPTION) = detail.RuleDescription
            row(RuleListDetailSelectionView.COL_NAME_EFFECTIVE) = detail.Effective.ToString
            row(RuleListDetailSelectionView.COL_NAME_EFFECTIVE) = detail.Effective.ToString
            row(RuleListDetailSelectionView.COL_NAME_EXPIRATION) = detail.Expiration.ToString
            t.Rows.Add(row)
        Next
        Return New RuleListDetailSelectionView(t)
    End Function

    Public Class RuleListDetailSelectionView
        Inherits DataView
        Public Const COL_NAME_RULE_LIST_ID As String = RuleListDAL.COL_NAME_RULE_LIST_ID
        Public Const COL_NAME_RULE_LIST_DETAIL_ID As String = RuleListDetailDAL.COL_NAME_RULE_LIST_DETAIL_ID
        Public Const COL_NAME_RULE_ID As String = RuleListDetailDAL.COL_NAME_RULE_ID
        Public Const COL_NAME_DESCRIPTION As String = "DESCRIPTION"
        Public Const COL_NAME_EFFECTIVE As String = RuleListDAL.COL_NAME_EFFECTIVE
        Public Const COL_NAME_EXPIRATION As String = RuleListDAL.COL_NAME_EXPIRATION

        Public Sub New(ByVal Table As DataTable)
            MyBase.New(Table)
        End Sub

        Public Shared Function CreateTable() As DataTable
            Dim t As New DataTable
            t.Columns.Add(COL_NAME_RULE_LIST_DETAIL_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_RULE_LIST_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_RULE_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_DESCRIPTION, GetType(String))
            t.Columns.Add(COL_NAME_EFFECTIVE, GetType(String))
            t.Columns.Add(COL_NAME_EXPIRATION, GetType(String))
            Return t
        End Function
    End Class

    Public Function GetRuleListDetailChild(ByVal childId As Guid) As RuleListDetail
        Return CType(RuleChildren.GetChild(childId), RuleListDetail)
    End Function

    Public Function GetNewRRuleListDetailChild() As RuleListDetail
        Dim newRuleListDetail As RuleListDetail = CType(RuleChildren.GetNewChild, RuleListDetail)
        With newRuleListDetail
            .RuleListId = Id
            .Effective = DateTime.Now
            .Expiration = Expiration
        End With
        Return newRuleListDetail
    End Function

    Sub PopulateDealerList(ByVal dealerlist As ArrayList)
        Try
            'compare with what we have and what is there in the user control
            'user control will always have the final selection so remove from our list what we don't find
            For Each dealerrule As DealerRuleList In DealerRuleChildren
                Dim dFound As Boolean = False
                For Each Str As String In dealerlist
                    Dim dealer_id As Guid = New Guid(Str)
                    If dealerrule.DealerId = dealer_id Then
                        dFound = True : Exit For
                    End If
                Next
                If Not dFound Then
                    dealerrule.BeginEdit()
                    dealerrule.Delete()
                    dealerrule.EndEdit()
                    dealerrule.Save()
                    dFound = True
                End If
            Next
            'next now add those items which are there in user control but we don't have it
            For Each Str As String In dealerlist
                Dim dFound As Boolean = False
                For Each dealerrule As DealerRuleList In DealerRuleChildren
                    Dim dealer_id As Guid = New Guid(Str)
                    If dealerrule.DealerId = dealer_id Then
                        dFound = True : Exit For
                    End If
                Next
                If Not dFound Then
                    Dim newDealerrule As DealerRuleList = GetNewRDealerRuleListChild()
                    newDealerrule.BeginEdit()
                    newDealerrule.DealerId = New Guid(Str)
                    newDealerrule.EndEdit()
                    newDealerrule.Save()
                End If
            Next

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    Sub PopulateCompanyList(ByVal companylist As ArrayList)
        Try
            'compare with what we have and what is there in the user control
            'user control will always have the final selection so remove from our list what we don't find
            For Each companyrule As CompanyRuleList In CompanyRuleChildren
                Dim dFound As Boolean = False
                For Each Str As String In companylist
                    Dim company_id As Guid = New Guid(Str)
                    If companyrule.CompanyId = company_id Then
                        dFound = True : Exit For
                    End If
                Next
                If Not dFound Then
                    companyrule.BeginEdit()
                    companyrule.Delete()
                    companyrule.EndEdit()
                    companyrule.Save()
                    dFound = True
                End If
            Next
            'next now add those items which are there in user control but we don't have it
            For Each Str As String In companylist
                Dim dFound As Boolean = False
                For Each companyrule As CompanyRuleList In CompanyRuleChildren
                    Dim company_id As Guid = New Guid(Str)
                    If companyrule.CompanyId = company_id Then
                        dFound = True : Exit For
                    End If
                Next
                If Not dFound Then
                    Dim newCompanyrule As CompanyRuleList = GetNewRCompanyRuleListChild()
                    newCompanyrule.BeginEdit()
                    newCompanyrule.CompanyId = New Guid(Str)
                    newCompanyrule.EndEdit()
                    newCompanyrule.Save()
                End If
            Next

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
#End Region

#Region "Dealer Rule View"
    Public ReadOnly Property DealerRuleChildren As DealerRuleList.DealerRuleListDetailView
        Get
            Return New DealerRuleList.DealerRuleListDetailView(Me)
        End Get
    End Property
    Public Function GetDealerRuleListSelectionView() As DealerRuleListDetailSelectionView
        Dim t As DataTable = DealerRuleListDetailSelectionView.CreateTable
        For Each detail As DealerRuleList In DealerRuleChildren
            Dim row As DataRow = t.NewRow
            row(DealerRuleListDetailSelectionView.COL_NAME_DEALER_ID) = detail.DealerId.ToByteArray
            row(DealerRuleListDetailSelectionView.COL_NAME_DEALER_RULE_LIST_ID) = detail.Id.ToByteArray
            row(DealerRuleListDetailSelectionView.COL_NAME_DESCRIPTION) = detail.Description
            row(DealerRuleListDetailSelectionView.COL_NAME_RULE_LIST_ID) = detail.RuleListId.ToByteArray
            row(DealerRuleListDetailSelectionView.COL_NAME_EFFECTIVE) = detail.Effective.ToString
            row(DealerRuleListDetailSelectionView.COL_NAME_EXPIRATION) = detail.Expiration.ToString
            t.Rows.Add(row)
        Next
        Return New DealerRuleListDetailSelectionView(t)
    End Function

    Public Class DealerRuleListDetailSelectionView
        Inherits DataView
        Public Const COL_NAME_RULE_LIST_ID As String = DealerRuleListDAL.COL_NAME_RULE_LIST_ID
        Public Const COL_NAME_DEALER_RULE_LIST_ID As String = DealerRuleListDAL.COL_NAME_DEALER_RULE_LIST_ID
        Public Const COL_NAME_DEALER_ID As String = DealerRuleListDAL.COL_NAME_DEALER_ID
        Public Const COL_NAME_DESCRIPTION As String = "DESCRIPTION"
        Public Const COL_NAME_EFFECTIVE As String = DealerRuleListDAL.COL_NAME_EFFECTIVE
        Public Const COL_NAME_EXPIRATION As String = DealerRuleListDAL.COL_NAME_EXPIRATION

        Public Sub New(ByVal Table As DataTable)
            MyBase.New(Table)
        End Sub

        Public Shared Function CreateTable() As DataTable
            Dim t As New DataTable
            t.Columns.Add(COL_NAME_DEALER_RULE_LIST_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_RULE_LIST_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_DEALER_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_DESCRIPTION, GetType(String))
            t.Columns.Add(COL_NAME_EFFECTIVE, GetType(String))
            t.Columns.Add(COL_NAME_EXPIRATION, GetType(String))
            Return t
        End Function
    End Class

    Public Function GetDealerRuleListChild(ByVal childId As Guid) As DealerRuleList
        Return CType(RuleChildren.GetChild(childId), DealerRuleList)
    End Function

    Public Function GetNewRDealerRuleListChild() As DealerRuleList
        Dim newDealerRuleList As DealerRuleList = CType(DealerRuleChildren.GetNewChild, DealerRuleList)
        With newDealerRuleList
            .RuleListId = Id
            .Effective = DateTime.Now
            .Expiration = Expiration
        End With
        Return newDealerRuleList
    End Function

    Sub PopulateRuleList(ByVal RuleDetail As ArrayList)
        Try
            'compare with what we have and what is there in the user control
            'user control will always have the final selection so remove from our list what we don't find
            For Each dealerrule As RuleListDetail In RuleChildren
                Dim dFound As Boolean = False
                For Each Str As String In RuleDetail
                    Dim dealer_id As Guid = New Guid(Str)
                    If dealerrule.RuleId = dealer_id Then
                        dFound = True : Exit For
                    End If
                Next
                If Not dFound Then
                    dealerrule.BeginEdit()
                    dealerrule.Delete()
                    dealerrule.EndEdit()
                    dealerrule.Save()
                End If
            Next
            'next now add those items which are there in user control but we don't have it
            For Each Str As String In RuleDetail
                Dim dFound As Boolean = False
                For Each dealerrule As RuleListDetail In RuleChildren
                    Dim dealer_id As Guid = New Guid(Str)
                    If dealerrule.RuleId = dealer_id Then
                        dFound = True : Exit For
                    End If
                Next
                If Not dFound Then
                    Dim newDealerrule As RuleListDetail = GetNewRRuleListDetailChild()
                    newDealerrule.BeginEdit()
                    newDealerrule.RuleId = New Guid(Str)
                    newDealerrule.EndEdit()
                    newDealerrule.Save()
                End If
            Next
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
#End Region

#Region "Company Rule View"
    Public ReadOnly Property CompanyRuleChildren As CompanyRuleList.CompanyRuleListDetailView
        Get
            Return New CompanyRuleList.CompanyRuleListDetailView(Me)
        End Get
    End Property
    Public Function GetCompanyRuleListSelectionView() As CompanyRuleListDetailSelectionView
        Dim t As DataTable = CompanyRuleListDetailSelectionView.CreateTable
        For Each detail As CompanyRuleList In CompanyRuleChildren
            Dim row As DataRow = t.NewRow
            row(CompanyRuleListDetailSelectionView.COL_NAME_COMPANY_ID) = detail.CompanyId.ToByteArray
            row(CompanyRuleListDetailSelectionView.COL_NAME_COMPANY_RULE_LIST_ID) = detail.Id.ToByteArray
            row(CompanyRuleListDetailSelectionView.COL_NAME_DESCRIPTION) = detail.Description
            row(CompanyRuleListDetailSelectionView.COL_NAME_RULE_LIST_ID) = detail.RuleListId.ToByteArray
            row(CompanyRuleListDetailSelectionView.COL_NAME_EFFECTIVE) = detail.Effective.ToString
            row(CompanyRuleListDetailSelectionView.COL_NAME_EXPIRATION) = detail.Expiration.ToString
            t.Rows.Add(row)
        Next
        Return New CompanyRuleListDetailSelectionView(t)
    End Function

    Public Class CompanyRuleListDetailSelectionView
        Inherits DataView
        Public Const COL_NAME_RULE_LIST_ID As String = CompanyRuleListDAL.COL_NAME_RULE_LIST_ID
        Public Const COL_NAME_COMPANY_RULE_LIST_ID As String = CompanyRuleListDAL.COL_NAME_COMPANY_RULE_LIST_ID
        Public Const COL_NAME_COMPANY_ID As String = CompanyRuleListDAL.COL_NAME_COMPANY_ID
        Public Const COL_NAME_DESCRIPTION As String = "DESCRIPTION"
        Public Const COL_NAME_EFFECTIVE As String = CompanyRuleListDAL.COL_NAME_EFFECTIVE
        Public Const COL_NAME_EXPIRATION As String = CompanyRuleListDAL.COL_NAME_EXPIRATION

        Public Sub New(ByVal Table As DataTable)
            MyBase.New(Table)
        End Sub

        Public Shared Function CreateTable() As DataTable
            Dim t As New DataTable
            t.Columns.Add(COL_NAME_COMPANY_RULE_LIST_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_RULE_LIST_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_COMPANY_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_DESCRIPTION, GetType(String))
            t.Columns.Add(COL_NAME_EFFECTIVE, GetType(String))
            t.Columns.Add(COL_NAME_EXPIRATION, GetType(String))
            Return t
        End Function
    End Class

    Public Function GetCompanyRuleListChild(ByVal childId As Guid) As CompanyRuleList
        Return CType(RuleChildren.GetChild(childId), CompanyRuleList)
    End Function

    Public Function GetNewRCompanyRuleListChild() As CompanyRuleList
        Dim newCompanyRuleList As CompanyRuleList = CType(CompanyRuleChildren.GetNewChild, CompanyRuleList)
        With newCompanyRuleList
            .RuleListId = Id
            .Effective = DateTime.Now
            .Expiration = Expiration
        End With
        Return newCompanyRuleList
    End Function


#End Region

#Region "Visitor"
    Public Function Accept(ByRef visitor As IVisitor) As Boolean Implements IExpirable.Accept
        Return visitor.Visit(Me)
    End Function
#End Region

    ' Addin for DEF 2344
    Public Function ExpireOverLappingQuestions() As Boolean
        Try
            Dim overlap As New OverlapValidationVisitorDAL
            Dim ds As New DataSet
            ds = overlap.LoadList(Id, [GetType].Name, Code, Effective, Expiration, Guid.Empty)
            If ds.Tables(0).Rows.Count > 0 Then
                For Each dtrow As DataRow In ds.Tables(0).Rows
                    Dim qId As Guid = New Guid(CType(dtrow(RuleListDAL.TABLE_KEY_NAME), Byte()))
                    Dim ExpRuleList As New RuleList(qId, Dataset)

                    If Effective.Value < ExpRuleList.Expiration.Value Then
                        'Expire overlapping question 1 second before current question
                        ExpRuleList.Accept(New ExpirationVisitor(Effective))
                    End If

                Next
                Return True         'expired successfully
            End If
            Return False
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Function CheckIfListIsAssignedToDealer() As Boolean
        If DealerRuleChildren.Count > 0 Then
            Return True
        End If
        Return False
    End Function

End Class


