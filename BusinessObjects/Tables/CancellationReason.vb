'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (8/13/2004)  ********************

Public Class CancellationReason
    Inherits BusinessObjectBase

#Region "Constructors"

    'Existing BO
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
        If ElitaPlusIdentity.Current.ActiveUser.Companies.Count = 1 Then SetValue(CancellationReasonDAL.COL_NAME_COMPANY_ID, ElitaPlusIdentity.Current.ActiveUser.CompanyId)
    End Sub

    'Existing BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDS As Dataset)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As Dataset)
        MyBase.New(False)
        Dataset = familyDS
        Load()
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New CancellationReasonDAL
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
            Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            setvalue(dal.TABLE_KEY_NAME, Guid.NewGuid)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Row = Nothing
            Dim dal As New CancellationReasonDAL
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

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If row(CancellationReasonDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CancellationReasonDAL.COL_NAME_CANCELLATION_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=50)> _
    Public Property Description() As String
        Get
            CheckDeleted()
            If row(CancellationReasonDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Dim retStr As String = CType(row(CancellationReasonDAL.COL_NAME_DESCRIPTION), String)
                If retStr Is Nothing Then
                    Return Nothing
                Else
                    Return retStr.ToUpper()
                End If
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Dim tempValue As String = Value
            If Not tempValue Is Nothing Then
                tempValue = tempValue.Trim().ToUpper()
            End If
            SetValue(CancellationReasonDAL.COL_NAME_DESCRIPTION, tempValue)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=5)> _
    Public Property Code() As String
        Get
            CheckDeleted()
            If row(CancellationReasonDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Dim retStr As String = CType(row(CancellationReasonDAL.COL_NAME_CODE), String)
                If retStr Is Nothing Then
                    Return Nothing
                Else
                    Return retStr.ToUpper()
                End If
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Dim tempValue As String = Value
            If Not tempValue Is Nothing Then
                tempValue = tempValue.Trim().ToUpper()
            End If
            SetValue(CancellationReasonDAL.COL_NAME_CODE, tempValue)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property CompanyId() As Guid
        Get
            CheckDeleted()
            If Row(CancellationReasonDAL.COL_NAME_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CancellationReasonDAL.COL_NAME_COMPANY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(CancellationReasonDAL.COL_NAME_COMPANY_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property RefundComputeMethodId() As Guid
        Get
            CheckDeleted()
            If Row(CancellationReasonDAL.COL_NAME_REFUND_COMPUTE_METHOD_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CancellationReasonDAL.COL_NAME_REFUND_COMPUTE_METHOD_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(CancellationReasonDAL.COL_NAME_REFUND_COMPUTE_METHOD_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property RefundDestinationId() As Guid
        Get
            CheckDeleted()
            If Row(CancellationReasonDAL.COL_NAME_REFUND_DESTINATION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CancellationReasonDAL.COL_NAME_REFUND_DESTINATION_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(CancellationReasonDAL.COL_NAME_REFUND_DESTINATION_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property InputAmtReqId() As Guid
        Get
            CheckDeleted()
            If Row(CancellationReasonDAL.COL_NAME_INPUT_AMT_REQ_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CancellationReasonDAL.COL_NAME_INPUT_AMT_REQ_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(CancellationReasonDAL.COL_NAME_INPUT_AMT_REQ_ID, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property DisplayCodeId() As Guid
        Get
            CheckDeleted()
            If Row(CancellationReasonDAL.COL_DISPLAY_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CancellationReasonDAL.COL_DISPLAY_CODE), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(CancellationReasonDAL.COL_DISPLAY_CODE, Value)
        End Set
    End Property

    Public Property DefRefundPaymentMethodId() As Guid
        Get
            CheckDeleted()
            If Row(CancellationReasonDAL.COL_DEF_REFUND_PAYMENT_METHOD_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CancellationReasonDAL.COL_DEF_REFUND_PAYMENT_METHOD_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(CancellationReasonDAL.COL_DEF_REFUND_PAYMENT_METHOD_ID, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property IsLawful() As String
        Get
            CheckDeleted()
            If Row(CancellationReasonDAL.COL_NAME_IS_LAWFUL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CancellationReasonDAL.COL_NAME_IS_LAWFUL), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(CancellationReasonDAL.COL_NAME_IS_LAWFUL, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=100)> _
    Public Property BenefitCancelReasonCode() As String
        Get
            CheckDeleted()
            If row(CancellationReasonDAL.COL_NAME_BENEFIT_CANCEL_REASON_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(CancellationReasonDAL.COL_NAME_BENEFIT_CANCEL_REASON_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(CancellationReasonDAL.COL_NAME_BENEFIT_CANCEL_REASON_CODE, Value)
        End Set
    End Property
#End Region

#Region "Constants"

    Dim EMPTY_GRID_ID As String = "00000000000000000000000000000000"

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            Dim dal As New CancellationReasonDAL
            'dal.Update(Me.Dataset)
            MyBase.UpdateFamily(Dataset)
            dal.UpdateFamily(Dataset)
            'Reload the Data
            If _isDSCreator AndAlso Row.RowState <> DataRowState.Detached Then
                'Reload the Data from the DB
                Load(Id)
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try

    End Sub

    Public Overrides ReadOnly Property IsDirty() As Boolean
        Get
            Dim bDirty As Boolean

            bDirty = MyBase.IsDirty OrElse IsChildrenDirty

            Return bDirty
        End Get
    End Property



    Public Sub Copy(ByVal original As CancellationReason)
        If Not IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Product")
        End If
        'Copy myself
        CopyFrom(original)

        Dim selRoleDv As DataView '= original.GetSelectedMethodOfRepair
        Dim selRoleList As New ArrayList
        Dim CountryId As Guid

        'child Regions                            
        selROleDv = original.GetSelectedRoles()
        For n As Integer = 0 To selRoleDv.Count - 1
            selRoleList.Add(New Guid(CType(selRoleDv(n)(LookupListNew.COL_ID_NAME), Byte())).ToString)
        Next
        AttachRoles(selRoleList)

    End Sub
#End Region

#Region "DataView Retrieveing Methods"

    Public Shared Function getList(ByVal descriptionMask As String, ByVal codeMask As String) As CancellationReasonSearchDV
        Try
            Dim dal As New CancellationReasonDAL
            Dim compIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Return New CancellationReasonSearchDV(dal.LoadList(descriptionMask, codeMask, compIds).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function getListbyRoleExlusion(ByVal UserId As Guid, ByVal CompId As Guid) As CancellationReasonSearchDV
        Try
            Dim dal As New CancellationReasonDAL
            Return New CancellationReasonSearchDV(dal.LoadListbyRoleExlusion(UserId, CompId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function




#Region "CancellationReasonSearchDV"
    Public Class CancellationReasonSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_CANCELLATIONREASON_ID As String = "cancellation_id"
        Public Const COL_DESCRIPTION As String = "description"
        Public Const COL_CODE As String = "code"
        Public Const COL_COMPANY As String = "company_code"
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

#Region "Custom Validation"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValueMandatoryConditionally
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_VALUE_MANDATORY_ERR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As CancellationReason = CType(objectToValidate, CancellationReason)

            If Not obj.IsNew Then
                Return True
            ElseIf obj.DisplayCodeId.Equals(Guid.Empty) Then
                Return False
            End If
            Return True

        End Function
    End Class


#End Region

#Region "ExcludeCancReasonByRoles"


    Public ReadOnly Property ExclCancelReasonByRoleChildren() As ExcludeCancReasonByRole.ExcludeCancReasonByRoleList
        Get
            Return New ExcludeCancReasonByRole.ExcludeCancReasonByRoleList(Me)
        End Get
    End Property

    Public Sub UpdateRoles(ByVal selectedRolesGuidStrCollection As Hashtable)
        If selectedRolesGuidStrCollection.Count = 0 Then
            If Not IsDeleted Then Delete()
        Else
            'first Pass
            Dim bo As ExcludeCancReasonByRole
            For Each bo In ExclCancelReasonByRoleChildren
                If Not selectedRolesGuidStrCollection.Contains(bo.RoleId.ToString) Then
                    'delete
                    bo.Delete()
                    bo.Save()
                End If
            Next
            'Second Pass
            Dim entry As DictionaryEntry
            For Each entry In selectedRolesGuidStrCollection
                If ExclCancelReasonByRoleChildren.Find(New Guid(entry.Key.ToString)) Is Nothing Then
                    'add
                    Dim Cancreasonrole As ExcludeCancReasonByRole = ExclCancelReasonByRoleChildren.GetNewChild()
                    Cancreasonrole.RoleId = New Guid(entry.Key.ToString)
                    Cancreasonrole.CancellationReasonId = Id
                    Cancreasonrole.Save()
                End If
            Next
        End If
    End Sub
    Public Sub AttachRoles(ByVal selectedRegionGuidStrCollection As ArrayList)
        Dim CancreasonRolestr As String
        For Each CancreasonRolestr In selectedRegionGuidStrCollection
            Dim Cancreasonrole As ExcludeCancReasonByRole = ExclCancelReasonByRoleChildren.GetNewChild
            Cancreasonrole.RoleId = New Guid(CancreasonRolestr)
            Cancreasonrole.CancellationReasonId = Id
            Cancreasonrole.Save()
        Next
    End Sub


    Public Function AddRolesChild(ByVal Roleid As Guid) As ExcludeCancReasonByRole
        Dim oCancreasonrole As ExcludeCancReasonByRole

        oCancreasonrole = New ExcludeCancReasonByRole(Dataset)
        oCancreasonrole.RoleId = Roleid
        oCancreasonrole.CancellationReasonId = Id
        Return oCancreasonrole

    End Function
    Public Sub DetachRoles(ByVal selectedRegionGuidStrCollection As ArrayList)
        Dim CancreasonRolestr As String
        For Each CancreasonRolestr In selectedRegionGuidStrCollection
            Dim Cancreasonrole As ExcludeCancReasonByRole = ExclCancelReasonByRoleChildren.Find(New Guid(CancreasonRolestr))
            Cancreasonrole.Delete()
            Cancreasonrole.Save()
        Next
    End Sub

    Public Function GetAvailableRoles() As DataView
        'Dim dv As DataView = LookupListNew.GetProductCodeByCompanyLookupList(ElitaPlusIdentity.Current.'ActiveUser.Companies)
        '       Dim sequenceCondition As String = GetProductCodesLookupListSelectedSequenceFilter(dv, False)
        Dim dv As DataView
        Dim sequenceCondition As String        
            'dv = LookupListNew.GetProductCodeByCompanyLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
            'Else
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

        Dim ProdRoleBO As ExcludeCancReasonByRole
        Dim inClause As String = "(-1"
        For Each ProdRoleBO In ExclCancelReasonByRoleChildren
            inClause &= "," & LookupListNew.GetSequenceFromId(dv, ProdRoleBO.RoleId)
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

End Class



