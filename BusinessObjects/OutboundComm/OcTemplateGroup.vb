'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (8/15/2017)  ********************

Public Class OcTemplateGroup
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
            Dim dal As New OcTemplateGroupDAL
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
            Dim dal As New OcTemplateGroupDAL
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
            If row(OcTemplateGroupDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(OcTemplateGroupDAL.COL_NAME_OC_TEMPLATE_GROUP_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=400), CheckDuplicate("")>
    Public Property Code As String
        Get
            CheckDeleted()
            If row(OcTemplateGroupDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(OcTemplateGroupDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(OcTemplateGroupDAL.COL_NAME_CODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=2000)>
    Public Property Description As String
        Get
            CheckDeleted()
            If row(OcTemplateGroupDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(OcTemplateGroupDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(OcTemplateGroupDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=400)>
    Public Property GroupAccountUserName As String
        Get
            CheckDeleted()
            If row(OcTemplateGroupDAL.COL_NAME_GROUP_ACCOUNT_USER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(OcTemplateGroupDAL.COL_NAME_GROUP_ACCOUNT_USER_NAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(OcTemplateGroupDAL.COL_NAME_GROUP_ACCOUNT_USER_NAME, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=400)>
    Public Property GroupAccountPassword As String
        Get
            CheckDeleted()
            If row(OcTemplateGroupDAL.COL_NAME_GROUP_ACCOUNT_PASSWORD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(OcTemplateGroupDAL.COL_NAME_GROUP_ACCOUNT_PASSWORD), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(OcTemplateGroupDAL.COL_NAME_GROUP_ACCOUNT_PASSWORD, Value)
        End Set
    End Property

    Public ReadOnly Property DealerList As OcTemplateGroupDealerList
        Get
            Return New OcTemplateGroupDealerList(Me)
        End Get
    End Property

    Public ReadOnly Property TemplateList As OcTemplateList
        Get
            Return New OcTemplateList(Me)
        End Get
    End Property
#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso (IsDirty OrElse IsFamilyDirty) AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New OcTemplateGroupDAL
                'dal.Update(Me.Row)
                UpdateFamily(Dataset)
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

    Public Sub Copy(original As OcTemplateGroup)
        If Not IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Template Group")
        End If
        'Copy myself
        CopyFrom(original)
    End Sub

    Public Sub AttachDealers(selectedDealerGuidStringCollection As ArrayList)
        Dim dealerGuid As String
        For Each dealerGuid In selectedDealerGuidStringCollection
            Dim templateGroupDealer As OcTemplateGroupDealer = DealerList.GetNewChild
            templateGroupDealer.DealerId = New Guid(dealerGuid)
            templateGroupDealer.OcTemplateGroupId = Id
            templateGroupDealer.Save()
        Next
    End Sub

    Public Sub DetachDealers(selectedDealerGuidStringCollection As ArrayList)
        Dim dealerGuid As String
        For Each dealerGuid In selectedDealerGuidStringCollection
            Dim templateGroupDealer = DealerList.Find(New Guid(dealerGuid))
            templateGroupDealer.Delete()
            templateGroupDealer.Save()
        Next
    End Sub
#End Region

#Region "Private Members"
    Private Function CheckDuplicateCode() As Boolean
        Dim dal As New OcTemplateGroupDAL
        'Dim companyIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
        'Dim companyGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
        Dim dv As OcTemplateGroup.OcTemplateGroupSearchDV = GetList(Code)

        For Each dr As DataRow In dv.Table.Rows
            If (Not New Guid(CType(dr(OcTemplateGroupDAL.COL_NAME_OC_TEMPLATE_GROUP_ID), Byte())).Equals(Id)) Then
                Return True
            End If
        Next
        Return False
    End Function
#End Region

#Region "DataView Retrieveing Methods"
    Public Class OcTemplateGroupSearchDV
        Inherits DataView

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub
    End Class

    Shared Function GetList(code As String) As OcTemplateGroupSearchDV
        Try
            Dim dal As New OcTemplateGroupDAL
            Return New OcTemplateGroupSearchDV(dal.LoadList(code).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetAssociatedTemplateCount(templateGroupId As Guid) As Integer
        Try
            Dim dal As New OcTemplateGroupDAL
            Dim dataSet As DataSet = dal.GetAssociatedTemplateCount(templateGroupId)
            If dataSet IsNot Nothing AndAlso dataSet.Tables.Count > 0 AndAlso dataSet.Tables(0).Rows.Count > 0 Then
                If dataSet.Tables(0).Rows(0)(OcTemplateGroupDAL.COL_NAME_NUMBER_OF_TEMPLATES) Is DBNull.Value Then
                    Return 0
                Else
                    Return CType(dataSet.Tables(0).Rows(0)(OcTemplateGroupDAL.COL_NAME_NUMBER_OF_TEMPLATES), UInt64)
                End If
            Else
                Return 0
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

#Region "Custom Validators"
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class CheckDuplicate
        Inherits ValidBaseAttribute
        Private Const DUPLICATE_TEMPLATE_GROUP_CODE As String = "DUPLICATE_TEMPLATE_GROUP_CODE"

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, DUPLICATE_TEMPLATE_GROUP_CODE)
        End Sub

        Public Overrides Function IsValid(objectToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As OcTemplateGroup = CType(objectToValidate, OcTemplateGroup)
            If (obj.CheckDuplicateCode()) Then
                Return False
            Else
                Return True
            End If
        End Function
    End Class
#End Region

    Private Function GetDealers(companies As ArrayList) As DataView
        Return New DataView(LookupListNew.GetDealerLookupList(companies).ToTable)
    End Function

    Public Function GetAvailableDealers(companies As ArrayList) As DataView
        Dim dv As DataView
        Dim sequenceCondition As String

        dv = GetDealers(companies)
        sequenceCondition = GetDealersLookupListSelectedSequenceFilter(dv, False)

        If dv.RowFilter Is Nothing OrElse dv.RowFilter.Trim.Length = 0 Then
            dv.RowFilter = sequenceCondition
        Else
            dv.RowFilter = "(" & dv.RowFilter & ") AND (" & sequenceCondition & ")"
        End If

        Return dv
    End Function

    Public Function GetSelectedDealers(companies As ArrayList) As DataView
        Dim dv As DataView
        Dim sequenceCondition As String

        dv = GetDealers(companies)
        sequenceCondition = GetDealersLookupListSelectedSequenceFilter(dv, True)

        If dv.RowFilter Is Nothing OrElse dv.RowFilter.Trim.Length = 0 Then
            dv.RowFilter = sequenceCondition
        Else
            dv.RowFilter = "(" & dv.RowFilter & ") AND (" & sequenceCondition & ")"
        End If

        Return dv
    End Function

    Protected Function GetDealersLookupListSelectedSequenceFilter(dv As DataView, isFilterInclusive As Boolean) As String
        Dim templateGroupDealer As OcTemplateGroupDealer
        Dim inClause As String = "(-1"

        For Each templateGroupDealer In DealerList
            inClause &= "," & LookupListNew.GetSequenceFromId(dv, templateGroupDealer.DealerId)
        Next

        inClause &= ")"
        Dim rowFilter As String = SYSTEM_SEQUENCE_COL_NAME

        If isFilterInclusive Then
            rowFilter &= " IN " & inClause
        Else
            rowFilter &= " NOT IN " & inClause
        End If

        Return rowFilter

    End Function
End Class


