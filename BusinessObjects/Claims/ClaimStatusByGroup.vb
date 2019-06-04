'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (6/11/2008)  ********************

Public Class ClaimStatusByGroup
    Inherits BusinessObjectBase

    Public Const URL As String = "ClaimStatusByGroupForm.aspx"
    Public Const ERR_STATUS_ORDER_OUT_OF_BOUND As String = "ERR_STATUS_ORDER_OUT_OF_BOUND"
    Public Const ERR_STATUS_ORDER_EXIST As String = "ERR_STATUS_ORDER_EXIST"
    Public Const ERR_DEALER_OR_COMPANY_GROUP_REQUIRED As String = "ERR_DEALER_OR_COMPANY_GROUP_REQUIRED"
    Private Const ERR_INVALID_STATUS_ORDER As String = "ERR_INVALID_STATUS_ORDER"
    Public Const MIN_ORDER As Integer = 1
    Public Const MAX_ORDER As Integer = 9999

    Public Class ClaimStatusByGroupSearchDV
        Inherits DataView

        Public Const TABLE_NAME As String = "ELP_CLAIM_STATUS_BY_GROUP"
        Public Const TABLE_KEY_NAME As String = "claim_status_by_group_id"

        Public Const COL_NAME_CLAIM_STATUS_BY_GROUP_ID As String = "claim_status_by_group_id"
        Public Const COL_NAME_COMPANY_GROUP_ID As String = "company_group_id"
        Public Const COL_NAME_DEALER_ID As String = "dealer_id"
        Public Const COL_NAME_LIST_ITEM_ID As String = "list_item_id"
        Public Const COL_NAME_STATUS_ORDER As String = "status_order"
        Public Const COL_NAME_OWNER_ID As String = "owner_id"
        Public Const COL_NAME_SKIPPING_ALLOWED_ID As String = "skipping_allowed_id"
        Public Const COL_NAME_STATUS_DATE As String = "status_date"
        Public Const COL_NAME_ACTIVE_ID As String = "active_id"
        Public Const COL_NAME_GROUP_NUMBER As String = "group_number"

        Public Const COL_NAME_COMPANY_GROUP_NAME As String = "company_group_name"
        Public Const COL_NAME_COMPANY_GROUP_CODE As String = "company_group_code"
        Public Const COL_NAME_DEALER_NAME As String = "dealer_name"
        Public Const COL_NAME_DEALER_CODE As String = "dealer_code"

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

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
            Dim dal As New ClaimStatusByGroupDAL
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
            Dim dal As New ClaimStatusByGroupDAL
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
            If row(ClaimStatusByGroupDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimStatusByGroupDAL.COL_NAME_CLAIM_STATUS_BY_GROUP_ID), Byte()))
            End If
        End Get
    End Property

    Public Property CompanyGroupId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimStatusByGroupDAL.COL_NAME_COMPANY_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimStatusByGroupDAL.COL_NAME_COMPANY_GROUP_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimStatusByGroupDAL.COL_NAME_COMPANY_GROUP_ID, Value)
        End Set
    End Property

    <ValidateDealer("")> _
    Public Property DealerId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimStatusByGroupDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimStatusByGroupDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimStatusByGroupDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ListItemId() As Guid
        Get
            CheckDeleted()
            If row(ClaimStatusByGroupDAL.COL_NAME_LIST_ITEM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimStatusByGroupDAL.COL_NAME_LIST_ITEM_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimStatusByGroupDAL.COL_NAME_LIST_ITEM_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("StatusOrder", MIN:=MIN_ORDER, Max:=MAX_ORDER, Message:=ERR_INVALID_STATUS_ORDER), ValidStatusOrder("")> _
    Public Property StatusOrder() As LongType
        Get
            CheckDeleted()
            If Row(ClaimStatusByGroupDAL.COL_NAME_STATUS_ORDER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ClaimStatusByGroupDAL.COL_NAME_STATUS_ORDER), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(ClaimStatusByGroupDAL.COL_NAME_STATUS_ORDER, Value)
        End Set
    End Property

    Public Property OwnerId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimStatusByGroupDAL.COL_NAME_OWNER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimStatusByGroupDAL.COL_NAME_OWNER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimStatusByGroupDAL.COL_NAME_OWNER_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property SkippingAllowedId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimStatusByGroupDAL.COL_NAME_SKIPPING_ALLOWED_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimStatusByGroupDAL.COL_NAME_SKIPPING_ALLOWED_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimStatusByGroupDAL.COL_NAME_SKIPPING_ALLOWED_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property ActiveId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimStatusByGroupDAL.COL_NAME_ACTIVE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimStatusByGroupDAL.COL_NAME_ACTIVE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimStatusByGroupDAL.COL_NAME_ACTIVE_ID, Value)
        End Set
    End Property

    Public Property GroupNumber() As LongType
        Get
            CheckDeleted()
            If Row(ClaimStatusByGroupDAL.COL_NAME_GROUP_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ClaimStatusByGroupDAL.COL_NAME_GROUP_NUMBER), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(ClaimStatusByGroupDAL.COL_NAME_GROUP_NUMBER, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New ClaimStatusByGroupDAL
                Me.UpdateFamily(Me.Dataset)
                dal.UpdateFamily(Me.Dataset)
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

    Public Function AddClaimStatusByGroup(ByVal claimStatusGroupId As Guid) As ClaimStatusByGroup
        Dim objClaimStatusByGroup As ClaimStatusByGroup

        If Not claimStatusGroupId.Equals(Guid.Empty) Then
            objClaimStatusByGroup = New ClaimStatusByGroup(claimStatusGroupId, Me.Dataset)
        Else
            objClaimStatusByGroup = New ClaimStatusByGroup(Me.Dataset)
        End If

        Return objClaimStatusByGroup
    End Function

    Public Shared Function getList(ByVal SearchBy As Integer, ByVal CompanyGroupId As Guid, ByVal dealerId As Guid) As DataView
        Try
            Dim dal As New ClaimStatusByGroupDAL
            Return New DataView(dal.LoadList(SearchBy, CompanyGroupId, dealerId, ElitaPlusIdentity.Current.ActiveUser.Companies).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function getListByCompanyGroupOrDealer(ByVal SearchBy As Integer, ByVal CompanyGroupId As Guid, ByVal dealerId As Guid) As DataView
        Try
            Dim dal As New ClaimStatusByGroupDAL

            Return New DataView(dal.LoadListByCompanyGroupOrDealer(SearchBy, CompanyGroupId, dealerId, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function LoadListByCompanyGroup(ByVal companyGroupId As Guid) As DataSet
        Try
            Dim dal As New ClaimStatusByGroupDAL
            Return dal.LoadListByCompanyGroup(companyGroupId, ElitaPlusIdentity.Current.ActiveUser.LanguageId)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetClaimStatusByGroupID(ByVal statusCode As String) As Guid
        Dim claimStatusByGroupId As Guid = Guid.Empty

        Try
            Dim dal As New ClaimStatusByGroupDAL
            Dim dtClaim As DataTable = dal.GetClaimStatusByGroupID(statusCode, ElitaPlusIdentity.Current.ActiveUser.LanguageId, _
                                                                ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id).Tables(0)

            If Not dtClaim Is Nothing AndAlso dtClaim.Rows.Count > 0 Then
                claimStatusByGroupId = New Guid(CType(dtClaim.Rows(0)(ClaimStatusByGroupDAL.COL_NAME_CLAIM_STATUS_BY_GROUP_ID), Byte()))
            End If

            Return claimStatusByGroupId
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function
#End Region

#Region "Custom Validation"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidateDealer
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, ERR_DEALER_OR_COMPANY_GROUP_REQUIRED)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ClaimStatusByGroup = CType(objectToValidate, ClaimStatusByGroup)
            Dim bValid As Boolean = False

            If obj.CompanyGroupId.Equals(Guid.Empty) AndAlso obj.DealerId.Equals(Guid.Empty) Then
                bValid = False
            ElseIf Not obj.CompanyGroupId.Equals(Guid.Empty) AndAlso Not obj.DealerId.Equals(Guid.Empty) Then
                bValid = False
            Else
                bValid = True
            End If

            Return bValid
        End Function

    End Class


    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidStatusOrder
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, ERR_STATUS_ORDER_EXIST)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ClaimStatusByGroup = CType(objectToValidate, ClaimStatusByGroup)
            Dim bValid As Boolean = False

            bValid = Not IsStatusOrderExist(obj.Id, obj.DealerId, obj.CompanyGroupId, obj.StatusOrder)

            Return bValid
        End Function

    End Class

    Public Shared Function IsStatusOrderExist(ByVal claimStatusGroupId As Guid, ByVal dealerId As Guid, ByVal companyGroupId As Guid, ByVal statusOrder As Integer) As Boolean
        Try
            Dim dal As New ClaimStatusByGroupDAL
            Return dal.IsStatusOrderExist(claimStatusGroupId, companyGroupId, dealerId, statusOrder)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function IsClaimStatusExist(ByVal SearchBy As Integer, ByVal CompanyGroupId As Guid, ByVal dealerId As Guid) As Boolean
        Try
            Dim dal As New ClaimStatusByGroupDAL
            Return dal.IsClaimStatusExist(SearchBy, CompanyGroupId, dealerId)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function IsDeletable(ByVal listItemId As String, ByVal dealerId As Guid, ByVal CompanyGroupId As Guid, ByVal searchBy As Integer) As Boolean
        Try
            Dim dal As New ClaimStatusByGroupDAL
            Return dal.IsDeletable(listItemId, CompanyGroupId, dealerId, searchBy)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#End Region

End Class


