﻿'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (2/11/2014)  ********************

Public Class ReppolicyClaimCount
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
            Dim dal As New ReppolicyClaimCountDAL
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
            Dim dal As New ReppolicyClaimCountDAL
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
            If row(ReppolicyClaimCountDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ReppolicyClaimCountDAL.COL_NAME_REPPOLICY_CLAIM_COUNT_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property ContractId As Guid
        Get
            CheckDeleted()
            If row(ReppolicyClaimCountDAL.COL_NAME_CONTRACT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ReppolicyClaimCountDAL.COL_NAME_CONTRACT_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ReppolicyClaimCountDAL.COL_NAME_CONTRACT_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)> _
    Public Property ProductCode As String
        Get
            CheckDeleted()
            If Row(ReppolicyClaimCountDAL.COL_NAME_PRODUCT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ReppolicyClaimCountDAL.COL_NAME_PRODUCT_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ReppolicyClaimCountDAL.COL_NAME_PRODUCT_CODE, Value)
        End Set
    End Property

    <ValidNumericRange("", Min:=1, Max:=9999, MaxExclusive:=False)> _
    Public Property CertDuration As LongType
        Get
            CheckDeleted()
            If Row(ReppolicyClaimCountDAL.COL_NAME_CERT_DURATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ReppolicyClaimCountDAL.COL_NAME_CERT_DURATION), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ReppolicyClaimCountDAL.COL_NAME_CERT_DURATION, Value)
        End Set
    End Property

    <Config_Criteria_Valid(""), Duplicate_Config_Exists("")> _
    Public Property ConverageTypeId As Guid
        Get
            CheckDeleted()
            If Row(ReppolicyClaimCountDAL.COL_NAME_CONVERAGE_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ReppolicyClaimCountDAL.COL_NAME_CONVERAGE_TYPE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ReppolicyClaimCountDAL.COL_NAME_CONVERAGE_TYPE_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("", Min:=1, Max:=99, MaxExclusive:=False)> _
    Public Property ReplacementPolicyClaimCount As LongType
        Get
            CheckDeleted()
            If Row(ReppolicyClaimCountDAL.COL_NAME_REPLACEMENT_POLICY_CLAIM_COUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ReppolicyClaimCountDAL.COL_NAME_REPLACEMENT_POLICY_CLAIM_COUNT), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ReppolicyClaimCountDAL.COL_NAME_REPLACEMENT_POLICY_CLAIM_COUNT, Value)
        End Set
    End Property

    Public ReadOnly Property CoverageTypeDescription As String
        Get
            Return LookupListNew.GetDescriptionFromId(LookupListCache.LK_COVERAGE_TYPES, ConverageTypeId, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
        End Get
    End Property


#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ReppolicyClaimCountDAL
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

    'Save to database even not the dataset creator
    Public Sub SaveWithoutCheckDSCreator()
        Try
            MyBase.Save()
            If IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ReppolicyClaimCountDAL
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

    Public Shared Function GetReplacementPolicyClaimCntByClaim(ContractID As Guid, ClaimID As Guid) As Long
        Dim dal As New ReppolicyClaimCountDAL
        Return dal.LoadReplacementPolicyClaimCntByClaim(ContractID, ClaimID)
    End Function

    Public Shared Function GetReplacementPolicyClaimCntConfigByContract(ContractID As Guid) As Collections.Generic.List(Of ReppolicyClaimCount)
        Dim dal As New ReppolicyClaimCountDAL
        Dim ds As DataSet = dal.LoadListByContract(ContractID)
        Dim RPCCList As New Collections.Generic.List(Of ReppolicyClaimCount)
        For Each dr As DataRow In ds.Tables(0).Rows
            RPCCList.Add(New ReppolicyClaimCount(dr))
        Next
        Return RPCCList
    End Function

    Public Shared Function GetCoverageTypeListByDealer(dealer_id As Guid) As DataView
        Dim dal As New ReppolicyClaimCountDAL
        Dim ds As DataSet = dal.LoadCoverageTypeByDealer(dealer_id, Authentication.CurrentUser.LanguageId)
        Return ds.Tables(0).DefaultView
    End Function

    Public Shared Function GetAvailCertDurationByDealer(dealer_id As Guid) As DataView
        Dim dal As New ReppolicyClaimCountDAL
        Dim ds As DataSet = dal.LoadAvailCertDurationByDealer(dealer_id)
        Return ds.Tables(0).DefaultView
    End Function

    'Verify that value allows in one and only one of the 3 fields
    Public Function OneAndOnlyOneConfigCriteriaHasValue() As Boolean
        Dim intCnt As Integer = 0
        If (ProductCode IsNot Nothing) AndAlso ProductCode <> String.Empty Then
            intCnt = intCnt + 1
        End If

        If ConverageTypeId <> Guid.Empty Then
            intCnt = intCnt + 1
        End If

        If (CertDuration IsNot Nothing) AndAlso CertDuration.Value > 0 Then
            intCnt = intCnt + 1
        End If

        Return (intCnt = 1)
    End Function

    Public Function DuplicateExists(ListToCheck As Collections.Generic.List(Of ReppolicyClaimCount)) As Boolean
        'Dim objInd As Integer = State.RepPolicyList.FindIndex(Function(r) r.Id = State.RepPolicyWorkingItem.Id)
        Dim blnDup As Boolean = False
        If (CertDuration IsNot Nothing) AndAlso CertDuration.Value > 0 Then
            If ListToCheck.Exists(Function(r) (r.CertDuration IsNot Nothing) AndAlso r.CertDuration = CertDuration AndAlso r.Id <> Id) Then
                blnDup = True
            End If
        ElseIf (ProductCode IsNot Nothing) AndAlso ProductCode <> String.Empty Then
            If ListToCheck.Exists(Function(r) (r.ProductCode IsNot Nothing) AndAlso r.ProductCode.Trim.ToUpper = ProductCode.Trim.ToUpper AndAlso r.Id <> Id) Then
                blnDup = True
            End If
        ElseIf ConverageTypeId <> Guid.Empty Then
            If ListToCheck.Exists(Function(r) r.ConverageTypeId = ConverageTypeId AndAlso r.Id <> Id) Then
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

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, "REPLACEMENT_POLICY_CONFIG_CRITERIA_INVALID")
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As ReppolicyClaimCount = CType(objectToValidate, ReppolicyClaimCount)
            Return obj.OneAndOnlyOneConfigCriteriaHasValue
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class Duplicate_Config_Exists
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, "REPLACEMENT_POLICY_DUPLICATE_CONFIG")
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As ReppolicyClaimCount = CType(objectToValidate, ReppolicyClaimCount)
            Dim mylist As Collections.Generic.List(Of ReppolicyClaimCount) = obj.GetReplacementPolicyClaimCntConfigByContract(obj.ContractId)
            Return (Not obj.DuplicateExists(mylist))
        End Function
    End Class
#End Region
End Class


