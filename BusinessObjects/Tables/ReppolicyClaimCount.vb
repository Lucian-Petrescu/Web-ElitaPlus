'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (2/11/2014)  ********************

Public Class ReppolicyClaimCount
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
            Dim dal As New ReppolicyClaimCountDAL
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
            Dim dal As New ReppolicyClaimCountDAL
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
            If row(ReppolicyClaimCountDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ReppolicyClaimCountDAL.COL_NAME_REPPOLICY_CLAIM_COUNT_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property ContractId() As Guid
        Get
            CheckDeleted()
            If row(ReppolicyClaimCountDAL.COL_NAME_CONTRACT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ReppolicyClaimCountDAL.COL_NAME_CONTRACT_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ReppolicyClaimCountDAL.COL_NAME_CONTRACT_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)> _
    Public Property ProductCode() As String
        Get
            CheckDeleted()
            If Row(ReppolicyClaimCountDAL.COL_NAME_PRODUCT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ReppolicyClaimCountDAL.COL_NAME_PRODUCT_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ReppolicyClaimCountDAL.COL_NAME_PRODUCT_CODE, Value)
        End Set
    End Property

    <ValidNumericRange("", Min:=1, Max:=9999, MaxExclusive:=False)> _
    Public Property CertDuration() As LongType
        Get
            CheckDeleted()
            If Row(ReppolicyClaimCountDAL.COL_NAME_CERT_DURATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ReppolicyClaimCountDAL.COL_NAME_CERT_DURATION), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(ReppolicyClaimCountDAL.COL_NAME_CERT_DURATION, Value)
        End Set
    End Property

    <Config_Criteria_Valid(""), Duplicate_Config_Exists("")> _
    Public Property ConverageTypeId() As Guid
        Get
            CheckDeleted()
            If Row(ReppolicyClaimCountDAL.COL_NAME_CONVERAGE_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ReppolicyClaimCountDAL.COL_NAME_CONVERAGE_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ReppolicyClaimCountDAL.COL_NAME_CONVERAGE_TYPE_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("", Min:=1, Max:=99, MaxExclusive:=False)> _
    Public Property ReplacementPolicyClaimCount() As LongType
        Get
            CheckDeleted()
            If Row(ReppolicyClaimCountDAL.COL_NAME_REPLACEMENT_POLICY_CLAIM_COUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ReppolicyClaimCountDAL.COL_NAME_REPLACEMENT_POLICY_CLAIM_COUNT), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(ReppolicyClaimCountDAL.COL_NAME_REPLACEMENT_POLICY_CLAIM_COUNT, Value)
        End Set
    End Property

    Public ReadOnly Property CoverageTypeDescription() As String
        Get
            Return LookupListNew.GetDescriptionFromId(LookupListNew.LK_COVERAGE_TYPES, ConverageTypeId, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
        End Get
    End Property


#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New ReppolicyClaimCountDAL
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

    'Save to database even not the dataset creator
    Public Sub SaveWithoutCheckDSCreator()
        Try
            MyBase.Save()
            If Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New ReppolicyClaimCountDAL
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

    Public Shared Function GetReplacementPolicyClaimCntByClaim(ByVal ContractID As Guid, ByVal ClaimID As Guid) As Long
        Dim dal As New ReppolicyClaimCountDAL
        Return dal.LoadReplacementPolicyClaimCntByClaim(ContractID, ClaimID)
    End Function

    Public Shared Function GetReplacementPolicyClaimCntConfigByContract(ByVal ContractID As Guid) As Collections.Generic.List(Of ReppolicyClaimCount)
        Dim dal As New ReppolicyClaimCountDAL
        Dim ds As DataSet = dal.LoadListByContract(ContractID)
        Dim RPCCList As New Collections.Generic.List(Of ReppolicyClaimCount)
        For Each dr As DataRow In ds.Tables(0).Rows
            RPCCList.Add(New ReppolicyClaimCount(dr))
        Next
        Return RPCCList
    End Function

    Public Shared Function GetCoverageTypeListByDealer(ByVal dealer_id As Guid) As DataView
        Dim dal As New ReppolicyClaimCountDAL
        Dim ds As DataSet = dal.LoadCoverageTypeByDealer(dealer_id, Authentication.CurrentUser.LanguageId)
        Return ds.Tables(0).DefaultView
    End Function

    Public Shared Function GetAvailCertDurationByDealer(ByVal dealer_id As Guid) As DataView
        Dim dal As New ReppolicyClaimCountDAL
        Dim ds As DataSet = dal.LoadAvailCertDurationByDealer(dealer_id)
        Return ds.Tables(0).DefaultView
    End Function

    'Verify that value allows in one and only one of the 3 fields
    Public Function OneAndOnlyOneConfigCriteriaHasValue() As Boolean
        Dim intCnt As Integer = 0
        If (Not Me.ProductCode Is Nothing) AndAlso Me.ProductCode <> String.Empty Then
            intCnt = intCnt + 1
        End If

        If Me.ConverageTypeId <> Guid.Empty Then
            intCnt = intCnt + 1
        End If

        If (Not Me.CertDuration Is Nothing) AndAlso Me.CertDuration.Value > 0 Then
            intCnt = intCnt + 1
        End If

        Return (intCnt = 1)
    End Function

    Public Function DuplicateExists(ByVal ListToCheck As Collections.Generic.List(Of ReppolicyClaimCount)) As Boolean
        'Dim objInd As Integer = State.RepPolicyList.FindIndex(Function(r) r.Id = State.RepPolicyWorkingItem.Id)
        Dim blnDup As Boolean = False
        If (Not Me.CertDuration Is Nothing) AndAlso Me.CertDuration.Value > 0 Then
            If ListToCheck.Exists(Function(r) (Not r.CertDuration Is Nothing) AndAlso r.CertDuration = Me.CertDuration AndAlso r.Id <> Me.Id) Then
                blnDup = True
            End If
        ElseIf (Not Me.ProductCode Is Nothing) AndAlso Me.ProductCode <> String.Empty Then
            If ListToCheck.Exists(Function(r) (Not r.ProductCode Is Nothing) AndAlso r.ProductCode.Trim.ToUpper = Me.ProductCode.Trim.ToUpper AndAlso r.Id <> Me.Id) Then
                blnDup = True
            End If
        ElseIf Me.ConverageTypeId <> Guid.Empty Then
            If ListToCheck.Exists(Function(r) r.ConverageTypeId = Me.ConverageTypeId AndAlso r.Id <> Me.Id) Then
                blnDup = True
            End If
        End If
        Return blnDup
    End Function

#End Region

#Region "DataView Retrieveing Methods"

#End Region

#Region "Custom Validation"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class Config_Criteria_Valid
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, "REPLACEMENT_POLICY_CONFIG_CRITERIA_INVALID")
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ReppolicyClaimCount = CType(objectToValidate, ReppolicyClaimCount)
            Return obj.OneAndOnlyOneConfigCriteriaHasValue
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class Duplicate_Config_Exists
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, "REPLACEMENT_POLICY_DUPLICATE_CONFIG")
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As ReppolicyClaimCount = CType(objectToValidate, ReppolicyClaimCount)
            Dim mylist As Collections.Generic.List(Of ReppolicyClaimCount) = obj.GetReplacementPolicyClaimCntConfigByContract(obj.ContractId)
            Return (Not obj.DuplicateExists(mylist))
        End Function
    End Class
#End Region
End Class


