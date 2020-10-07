'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (4/8/2014)  ********************

Public Class ExcludeListitemByRole
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
            Dim dal As New ExcludeListitemByRoleDAL
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
            Dim dal As New ExcludeListitemByRoleDAL
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

#Region "Constants"

    Dim EMPTY_GRID_ID As String = "00000000000000000000000000000000"

#End Region

#Region "Private Members"
    Private _ExcludedRolesCount As Integer
    'Initialization code for new objects
    Private Sub Initialize()
    End Sub
#End Region


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid
        Get
            If row(ExcludeListitemByRoleDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ExcludeListitemByRoleDAL.COL_NAME_EXCLUDE_LISTITEM_ROLE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property ListItemId As Guid
        Get
            CheckDeleted()
            If row(ExcludeListitemByRoleDAL.COL_NAME_LIST_ITEM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ExcludeListitemByRoleDAL.COL_NAME_LIST_ITEM_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ExcludeListitemByRoleDAL.COL_NAME_LIST_ITEM_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property CompanyId As Guid
        Get
            CheckDeleted()
            If row(ExcludeListitemByRoleDAL.COL_NAME_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ExcludeListitemByRoleDAL.COL_NAME_COMPANY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ExcludeListitemByRoleDAL.COL_NAME_COMPANY_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property ListId As Guid
        Get
            CheckDeleted()
            If row(ExcludeListitemByRoleDAL.COL_NAME_LIST_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ExcludeListitemByRoleDAL.COL_NAME_LIST_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ExcludeListitemByRoleDAL.COL_NAME_LIST_ID, Value)
        End Set
    End Property

    <ValidExcludedRolesCount("")> _
    Public Property ExcludedRolesCount As Integer
        Get
            Return _ExcludedRolesCount
        End Get
        Set
            CheckDeleted()
            _ExcludedRolesCount = Value
        End Set
    End Property
#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ExcludeListitemByRoleDAL
                'dal.Update(Me.Row)
                MyBase.UpdateFamily(Dataset)
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
            Dim bDirty As Boolean

            bDirty = MyBase.IsDirty OrElse IsChildrenDirty

            Return bDirty
        End Get
    End Property

    Public Sub Copy(ByVal original As ExcludeListitemByRole)
        If Not IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Product")
        End If
        'Copy myself
        CopyFrom(original)

        Dim selROLEDv As DataView '= original.GetSelectedMethodOfRepair
        Dim selRoleList As New ArrayList
        Dim CountryId As Guid

        'child Roles
        selROLEDv = original.GetSelectedRoles()
        For n As Integer = 0 To selROLEDv.Count - 1
            selRoleList.Add(New Guid(CType(selROLEDv(n)(LookupListNew.COL_ID_NAME), Byte())).ToString)
        Next
        AttachRoles(selRoleList)

    End Sub
#End Region

#Region "DataView Retrieveing Methods"

    Public Shared Function getList(ByVal compId As Guid, ByVal ListId As Guid, _
                         ByVal ListItemId As Guid, ByVal RoleId As Guid, ByVal LanguageId As Guid) As ExcludeListitemByRoleSearchDV
        Try
            Dim dal As New ExcludeListitemByRoleDAL
            Return New ExcludeListitemByRoleSearchDV(dal.LoadList(compId, ListId, ListItemId, RoleId, LanguageId).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#Region "ExcludeListitemByRoleSearchDV"
    Public Class ExcludeListitemByRoleSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_EXCLUDE_LIST_ITEM_BY_ROLE_ID As String = "exclude_listitem_role_id"
        Public Const COL_COMPANY_NAME As String = "company_name"
        Public Const COL_LIST_ITEM_DESCRIPTION As String = "list_item_description"
        Public Const COL_LIST_DESCRIPTION As String = "list_description"
        Public Const COL_ROLE_DESCRIPTION As String = "role_description"

#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region
#End Region

#Region "Children Related"

    Public ReadOnly Property ExcludedLiRoleChildren As ExcludedLiRolesList
        Get
            Return New ExcludedLiRolesList(Me)
        End Get
    End Property

    Public Sub UpdateRoles(ByVal selectedRoleGuidStrCollection As Hashtable)
        If selectedRoleGuidStrCollection.Count = 0 Then
            If Not IsDeleted Then Delete()
        Else
            'first Pass
            Dim bo As ExcludedLiRoles
            For Each bo In ExcludedLiRoleChildren
                If Not selectedRoleGuidStrCollection.Contains(bo.RoleId.ToString) Then
                    'delete
                    bo.Delete()
                    bo.Save()
                End If
            Next
            'Second Pass
            Dim entry As DictionaryEntry
            For Each entry In selectedRoleGuidStrCollection
                If ExcludedLiRoleChildren.Find(New Guid(entry.Key.ToString)) Is Nothing Then
                    'add
                    Dim ExRoles As ExcludedLiRoles = ExcludedLiRoleChildren.GetNewChild()
                    ExRoles.RoleId = New Guid(entry.Key.ToString)
                    ExRoles.ExcludeListitemRoleId = Id
                    ExRoles.Save()
                End If
            Next
        End If
    End Sub
    Public Sub AttachRoles(ByVal selectedRoleGuidStrCollection As ArrayList)
        Dim ExRolestr As String
        For Each ExRolestr In selectedRoleGuidStrCollection
            Dim ExRoles As ExcludedLiRoles = ExcludedLiRoleChildren.GetNewChild
            ExRoles.RoleId = New Guid(ExRolestr)
            ExRoles.ExcludeListitemRoleId = Id
            ExRoles.Save()
        Next
    End Sub


    Public Function AddRolesChild(ByVal RoleId As Guid) As ExcludedLiRoles
        Dim oExRoles As ExcludedLiRoles

        oExRoles = New ExcludedLiRoles(Dataset)
        oExRoles.RoleId = RoleId
        oExRoles.ExcludeListitemRoleId = Id
        Return oExRoles

    End Function
    Public Sub DetachRoles(ByVal selectedRoleGuidStrCollection As ArrayList)
        Dim ExRolestr As String
        For Each ExRolestr In selectedRoleGuidStrCollection
            Dim oExRole As ExcludedLiRoles = ExcludedLiRoleChildren.Find(New Guid(ExRolestr))
            oExRole.Delete()
            oExRole.Save()
        Next
    End Sub

    Public Function GetAvailableRoles() As DataView

        Dim dv As DataView
        Dim sequenceCondition As String

        dv = Role.GetRolesList()
        sequenceCondition = GetRolesLookupListSelectedSequenceFilter(dv, False)

        If dv.RowFilter Is Nothing OrElse dv.RowFilter.Trim.Length = 0 Then
            dv.RowFilter = sequenceCondition
        Else
            dv.RowFilter = "(" & dv.RowFilter & ") AND (" & sequenceCondition & ")"
        End If

        Return dv
    End Function

    Public Function GetSelectedRoles() As DataView

        Dim dv As DataView
        Dim sequenceCondition As String

        dv = Role.GetRolesList()
        sequenceCondition = GetRolesLookupListSelectedSequenceFilter(dv, True)

        If dv.RowFilter Is Nothing OrElse dv.RowFilter.Trim.Length = 0 Then
            dv.RowFilter = sequenceCondition
        Else
            dv.RowFilter = "(" & dv.RowFilter & ") AND (" & sequenceCondition & ")"
        End If

        Return dv
    End Function

    Protected Function GetRolesLookupListSelectedSequenceFilter(ByVal dv As DataView, ByVal isFilterInclusive As Boolean) As String

        Dim oExRolesBO As ExcludedLiRoles
        Dim inClause As String = "(-1"
        For Each oExRolesBO In ExcludedLiRoleChildren
            inClause &= "," & LookupListNew.GetSequenceFromId(dv, oExRolesBO.RoleId)
        Next
        inClause &= ")"
        Dim rowFilter As String = BusinessObjectBase.SYSTEM_SEQUENCE_COL_NAME
        If isFilterInclusive Then
            rowFilter &= " IN " & inClause
        Else
            rowFilter &= " NOT IN " & inClause
        End If
        Return rowFilter

    End Function

#End Region


#Region "Custom Validation"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidExcludedRolesCount
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.BO_EXCLUDED_ROLES_MUST_BE_SELECTED_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ExcludeListitemByRole = CType(objectToValidate, ExcludeListitemByRole)

            If obj.ExcludedRolesCount > 0 Then
                Return True
            Else
                Return False
            End If

        End Function

    End Class

#End Region

End Class


