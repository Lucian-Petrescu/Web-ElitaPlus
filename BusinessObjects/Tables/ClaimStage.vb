Imports Assurant.Common.Localization

Public Class ClaimStage
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(id As Guid)
        MyBase.New()
        Dataset = New DataSet
        Load(id)
    End Sub

    'Exiting BO
    Public Sub New(id As Guid, languageid As Guid)
        MyBase.New()
        Dataset = New DataSet
        Load(id)
        If Me.LanguageId = Guid.Empty Then
            Me.LanguageId = languageid
        End If
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
            Dim dal As New ClaimStageDAL
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
            Dim dal As New ClaimStageDAL
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

    Protected Sub LoadChildren(reloadData As Boolean, languageid As Guid)
        If Me.LanguageId = Guid.Empty Then
            Me.LanguageId = languageid
        End If

        Try
            If reloadData Then
                Dim tableIdx As Integer = Dataset.Tables.IndexOf(StageEndStatusDAL.TABLE_NAME)
                If tableIdx <> -1 Then
                    Dataset.Tables.Remove(StageEndStatusDAL.TABLE_NAME)
                End If
            End If

            If Dataset.Tables.IndexOf(StageEndStatusDAL.TABLE_NAME) < 0 Then
                Dim _claimstageDAL As New ClaimStageDAL
                _claimstageDAL.LoadEndStatusList(Dataset, StageEndStatusDAL.TABLE_NAME, Id, languageid)
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
    <ValidOnlyOneEntity(""), ValidOneEntitySelected("")>
    Public ReadOnly Property Id As Guid
        Get
            If Row(ClaimStageDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimStageDAL.COL_NAME_STAGE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property StageNameId As Guid
        Get
            CheckDeleted()
            If Row(ClaimStageDAL.COL_NAME_STAGE_NAME_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimStageDAL.COL_NAME_STAGE_NAME_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimStageDAL.COL_NAME_STAGE_NAME_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property StartStatusId As Guid
        Get
            CheckDeleted()
            If Row(ClaimStageDAL.COL_NAME_START_STATUS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimStageDAL.COL_NAME_START_STATUS_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimStageDAL.COL_NAME_START_STATUS_ID, Value)
        End Set
    End Property

    Public Property CompanyGroupId As Guid
        Get
            CheckDeleted()
            If Row(ClaimStageDAL.COL_NAME_COMPANY_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimStageDAL.COL_NAME_COMPANY_GROUP_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimStageDAL.COL_NAME_COMPANY_GROUP_ID, Value)
        End Set
    End Property

    Public Property CompanyId As Guid
        Get
            CheckDeleted()
            If Row(ClaimStageDAL.COL_NAME_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimStageDAL.COL_NAME_COMPANY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimStageDAL.COL_NAME_COMPANY_ID, Value)
        End Set
    End Property

    Public Property DealerId As Guid
        Get
            CheckDeleted()
            If Row(ClaimStageDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimStageDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimStageDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=5), ValidProductCode("")> _
    Public Property ProductCode As String
        Get
            CheckDeleted()
            If Row(ClaimStageDAL.COL_NAME_PRODUCT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimStageDAL.COL_NAME_PRODUCT_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimStageDAL.COL_NAME_PRODUCT_CODE, Value)
        End Set
    End Property

    <ValidCoverageType("")> _
    Public Property CoverageTypeId As Guid
        Get
            CheckDeleted()
            If Row(ClaimStageDAL.COL_NAME_COVERAGE_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimStageDAL.COL_NAME_COVERAGE_TYPE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimStageDAL.COL_NAME_COVERAGE_TYPE_ID, Value)
        End Set
    End Property

    <ValueMandatory(""), RejectOverlapsOrGaps("")> _
    Public Property EffectiveDate As DateTime?
        Get
            CheckDeleted()
            If Row(ClaimStageDAL.COL_NAME_EFFECTIVE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimStageDAL.COL_NAME_EFFECTIVE_DATE), Date)
            End If
        End Get
        Set
            If Value.HasValue Then
                CheckDeleted()
                SetValue(ClaimStageDAL.COL_NAME_EFFECTIVE_DATE, Value)
            End If
        End Set
    End Property

    <ValueMandatory(""), EffectiveExpirationDateValidation("ExpirationDate")> _
    Public Property ExpirationDate As DateTime?
        Get
            CheckDeleted()
            If Row(ClaimStageDAL.COL_NAME_EXPIRATION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimStageDAL.COL_NAME_EXPIRATION_DATE), Date)
            End If
        End Get
        Set
            If Value.HasValue Then
                CheckDeleted()
                SetValue(ClaimStageDAL.COL_NAME_EXPIRATION_DATE, Value)
            End If
        End Set
    End Property

    <ValueMandatory(""), ValidNumericRange("", Min:=0, Max:=99)> _
    Public Property Sequence As LongType
        Get
            CheckDeleted()
            If Row(ClaimStageDAL.COL_NAME_SEQUENCE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ClaimStageDAL.COL_NAME_SEQUENCE), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimStageDAL.COL_NAME_SEQUENCE, Value)
        End Set
    End Property

    <ValidScreenValue("")> _
    Public Property ScreenId As Guid
        Get
            CheckDeleted()
            If Row(ClaimStageDAL.COL_NAME_SCREEN_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimStageDAL.COL_NAME_SCREEN_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimStageDAL.COL_NAME_SCREEN_ID, Value)
        End Set
    End Property

    <ValidPortalValue("")> _
    Public Property PortalId As Guid
        Get
            CheckDeleted()
            If Row(ClaimStageDAL.COL_NAME_PORTAL_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimStageDAL.COL_NAME_PORTAL_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimStageDAL.COL_NAME_PORTAL_ID, Value)
        End Set
    End Property

    Public ReadOnly Property OriginalEffectiveDate As DateType
        Get
            Return New DateType(CType(Row(AFAInvoiceRateDAL.COL_NAME_EFFECTIVE_DATE, DataRowVersion.Original), Date))
        End Get
    End Property

    Public ReadOnly Property OriginalExpirationDate As DateType
        Get
            Return New DateType(CType(Row(AFAInvoiceRateDAL.COL_NAME_EXPIRATION_DATE, DataRowVersion.Original), Date))
        End Get
    End Property

    Public Property LanguageId As Guid

#End Region

#Region "Public Members"

    Public Overrides ReadOnly Property IsDirty As Boolean
        Get
            Return MyBase.IsFamilyDirty()
        End Get
    End Property

    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsFamilyDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ClaimStageDAL
                'dal.Update(Me.Row)
                dal.UpdateFamily(Dataset) ' changed to take care of join table
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then
                    Load(Id)
                    LoadChildren(True, LanguageId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    Public Sub AttachStageEndStatus(stageEndStatusGuidStr As String, _languageid As Guid)
        LoadChildren(False, _languageid)

        If stageEndStatusGuidStr IsNot Nothing AndAlso stageEndStatusGuidStr.Length > 0 Then
            Dim i As Integer
            Dim StgEndStatusId As Guid = New Guid(stageEndStatusGuidStr)
            Dim stgEndStatus As StageEndStatus = StageEndStatus.Find(Dataset, Id, StgEndStatusId)
            If stgEndStatus Is Nothing Then
                stgEndStatus = New StageEndStatus(Dataset)
                stgEndStatus.EndStatusId = StgEndStatusId
                stgEndStatus.StageId = Id
                stgEndStatus.Save()
            End If
        End If
    End Sub

    Public Sub DetachStageEndStatus(stageEndStatusGuidStr As String, _languageid As Guid)
        LoadChildren(False, _languageid)

        If stageEndStatusGuidStr IsNot Nothing AndAlso stageEndStatusGuidStr.Length > 0 Then
            Dim i As Integer
            Dim stgEndStatus As StageEndStatus = StageEndStatus.Find(Dataset, Id, New Guid(stageEndStatusGuidStr))

            If stgEndStatus IsNot Nothing Then
                stgEndStatus.Delete()
                stgEndStatus.Save()
            End If
        End If
    End Sub

#End Region

#Region "SearchDV"
    Public Class ClaimStageSearchDV
        Inherits DataView

        Public Const COL_STAGE_ID As String = "STAGE_ID"
        Public Const COL_STAGE_NAME_ID As String = "stage_name_id"
        Public Const COL_STAGE_NAME_DESC As String = "stage_name_desc"
        Public Const COL_START_STATUS_ID As String = "start_status_id"
        Public Const COL_START_STATUS_DESC As String = "start_status_desc"
        Public Const COL_COMPANY_GROUP_ID As String = "company_group_id"
        Public Const COL_COMPANY_GROUP_DESC As String = "company_group_desc"
        Public Const COL_COMPANY_ID As String = "company_id"
        Public Const COL_COMPANY_DESC As String = "company_desc"
        Public Const COL_DEALER_ID As String = "dealer_id"
        Public Const COL_DEALER_DESC As String = "dealer_desc"
        Public Const COL_COVERAGE_TYPE_ID As String = "coverage_type_id"
        Public Const COL_COVERAGE_TYPE_DESC As String = "coverage_type_desc"
        Public Const COL_EFFECTIVE_DATE As String = "effective_date"
        Public Const COL_EXPIRATION_DATE As String = "expiration_date"
        Public Const COL_SEQUENCE As String = "sequence"
        Public Const COL_SCREEN_ID As String = "screen_id"
        Public Const COL_SCREEN_DESC As String = "screen_desc"
        Public Const COL_PORTAL_ID As String = "portal_id"
        Public Const COL_PORTAL_DESC As String = "portal_desc"

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

    Public Shared Sub AddNewRowToSearchDV(ByRef dv As ClaimStageSearchDV, NewBO As ClaimStage)
        Dim dt As DataTable, blnEmptyTbl As Boolean = False

        If NewBO.IsNew Then
            Dim row As DataRow
            If dv Is Nothing Then
                Dim guidTemp As New Guid
                blnEmptyTbl = True
                dt = New DataTable
                dt.Columns.Add(ClaimStageSearchDV.COL_STAGE_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(ClaimStageSearchDV.COL_STAGE_NAME_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(ClaimStageSearchDV.COL_STAGE_NAME_DESC, GetType(String))
                dt.Columns.Add(ClaimStageSearchDV.COL_START_STATUS_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(ClaimStageSearchDV.COL_START_STATUS_DESC, GetType(String))
                dt.Columns.Add(ClaimStageSearchDV.COL_COMPANY_GROUP_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(ClaimStageSearchDV.COL_COMPANY_GROUP_DESC, GetType(String))
                dt.Columns.Add(ClaimStageSearchDV.COL_COMPANY_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(ClaimStageSearchDV.COL_COMPANY_DESC, GetType(String))
                dt.Columns.Add(ClaimStageSearchDV.COL_DEALER_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(ClaimStageSearchDV.COL_DEALER_DESC, GetType(String))
                dt.Columns.Add(ClaimStageSearchDV.COL_COVERAGE_TYPE_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(ClaimStageSearchDV.COL_COVERAGE_TYPE_DESC, GetType(String))
                dt.Columns.Add(ClaimStageSearchDV.COL_EFFECTIVE_DATE, GetType(DateTime))
                dt.Columns.Add(ClaimStageSearchDV.COL_EXPIRATION_DATE, GetType(DateTime))
                dt.Columns.Add(ClaimStageSearchDV.COL_SEQUENCE, GetType(LongType))
                dt.Columns.Add(ClaimStageSearchDV.COL_SCREEN_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(ClaimStageSearchDV.COL_SCREEN_DESC, GetType(String))
                dt.Columns.Add(ClaimStageSearchDV.COL_PORTAL_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(ClaimStageSearchDV.COL_PORTAL_DESC, GetType(String))
            Else
                dt = dv.Table
            End If

            row = dt.NewRow
            row(ClaimStageSearchDV.COL_STAGE_ID) = NewBO.Id.ToByteArray
            row(ClaimStageSearchDV.COL_STAGE_NAME_ID) = NewBO.StageNameId.ToByteArray
            row(ClaimStageSearchDV.COL_START_STATUS_ID) = NewBO.StartStatusId.ToByteArray
            row(ClaimStageSearchDV.COL_COMPANY_GROUP_ID) = NewBO.CompanyGroupId.ToByteArray
            row(ClaimStageSearchDV.COL_COMPANY_ID) = NewBO.CompanyId.ToByteArray
            row(ClaimStageSearchDV.COL_DEALER_ID) = NewBO.DealerId.ToByteArray
            row(ClaimStageSearchDV.COL_COVERAGE_TYPE_ID) = NewBO.CoverageTypeId.ToByteArray
            row(ClaimStageSearchDV.COL_EFFECTIVE_DATE) = DateTime.MinValue
            row(ClaimStageSearchDV.COL_EXPIRATION_DATE) = DateTime.MinValue
            row(ClaimStageSearchDV.COL_SEQUENCE) = NewBO.Sequence
            row(ClaimStageSearchDV.COL_SCREEN_ID) = NewBO.ScreenId.ToByteArray
            row(ClaimStageSearchDV.COL_PORTAL_ID) = NewBO.PortalId.ToByteArray

            dt.Rows.Add(row)
            If blnEmptyTbl Then dv = New ClaimStageSearchDV(dt)
        End If
    End Sub

    Public Shared Function getList(StageNameID As Guid, CompGrpID As Guid, CompanyID As Guid, DealerID As Guid, _
                             CoverageTypeID As Guid, ActiveOn As DateType, Sequence As String, _
                             ScreenID As Guid, PortalID As Guid) As ClaimStageSearchDV
        Try
            Dim dal As New ClaimStageDAL
            Return New ClaimStageSearchDV(dal.LoadList(StageNameID, CompGrpID, CompanyID, DealerID, CoverageTypeID, ActiveOn, Sequence, ScreenID, PortalID, ElitaPlusIdentity.Current.ActiveUser.LanguageId, ElitaPlusIdentity.Current.ActiveUser.Companies, ElitaPlusIdentity.Current.ActiveUser.Countries).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

#Region "Custom Validation"

    Public NotInheritable Class ValidProductCode
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.DEALER_IS_REQUIRED)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As ClaimStage = CType(objectToValidate, ClaimStage)

            If valueToCheck Is Nothing Then Return True

            If Not String.IsNullOrEmpty(obj.ProductCode) Then
                If obj.DealerId = Guid.Empty Then
                    Return False
                Else
                    Return True
                End If
            Else
                Return True
            End If
        End Function
    End Class

    Public NotInheritable Class ValidOnlyOneEntity
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.ONLY_ONE_ALLOWED_COMPGRP_COMANPY_DEALER)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            If (valueToCheck IsNot Nothing) AndAlso valueToCheck <> Guid.Empty Then
                Dim obj As ClaimStage = CType(objectToValidate, ClaimStage)

                If valueToCheck Is Nothing Then Return True

                If obj.CompanyGroupId <> Guid.Empty Then
                    If obj.CompanyId <> Guid.Empty OrElse obj.DealerId <> Guid.Empty Then
                        Return False
                    End If
                ElseIf obj.CompanyId <> Guid.Empty Then
                    If obj.CompanyGroupId <> Guid.Empty OrElse obj.DealerId <> Guid.Empty Then
                        Return False
                    End If
                ElseIf obj.DealerId <> Guid.Empty Then
                    If obj.CompanyGroupId <> Guid.Empty OrElse obj.CompanyId <> Guid.Empty Then
                        Return False
                    End If
                End If
            End If
            Return True

        End Function
    End Class

    Public NotInheritable Class ValidCoverageType
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.PRODUCT_CODE_AND_DEALER_IS_REQUIRED)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As ClaimStage = CType(objectToValidate, ClaimStage)

            If valueToCheck Is Nothing Then Return True

            If Not obj.CoverageTypeId = Guid.Empty Then
                If obj.DealerId = Guid.Empty OrElse String.IsNullOrEmpty(obj.ProductCode) Then
                    Return False
                Else
                    Return True
                End If
            Else
                Return True
            End If
        End Function
    End Class

    Public NotInheritable Class ValidOneEntitySelected
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.SELECT_ONE_COMPGRP_COMANPY_DEALER)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As ClaimStage = CType(objectToValidate, ClaimStage)

            If valueToCheck Is Nothing Then Return True

            If obj.CompanyGroupId = Guid.Empty AndAlso obj.CompanyId = Guid.Empty AndAlso obj.DealerId = Guid.Empty Then
                Return False
            Else
                Return True
            End If
        End Function
    End Class

    Public NotInheritable Class ValidScreenValue
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_INVALID_VALUE)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As ClaimStage = CType(objectToValidate, ClaimStage)

            If valueToCheck Is Nothing Then Return True

            If (obj.ScreenId = Guid.Empty OrElse obj.ScreenId = Nothing) Then
                Return False
            Else
                Return True
            End If

        End Function
    End Class

    Public NotInheritable Class ValidPortalValue
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.GUI_INVALID_VALUE)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As ClaimStage = CType(objectToValidate, ClaimStage)

            If valueToCheck Is Nothing Then Return True

            If (obj.PortalId = Guid.Empty OrElse obj.PortalId = Nothing) Then
                Return False
            Else
                Return True
            End If

        End Function
    End Class

    Public NotInheritable Class EffectiveExpirationDateValidation
        Inherits ValidBaseAttribute
        Implements IValidatorAttribute

        Public Sub New(FieldNamestring As String)
            MyBase.New(FieldNamestring, Messages.INVALID_EXP_DATE)
        End Sub

        Public Overrides Function IsValid(objectToCheck As Object, context As Object) As Boolean
            Dim obj As ClaimStage = CType(context, ClaimStage)

            If objectToCheck Is Nothing Then Return True

            Try
                Dim objDate As DateTime? = CType(objectToCheck, DateTime?)
                If Not objDate = Nothing And obj.EffectiveDate IsNot Nothing Then
                    If obj.EffectiveDate.Value > objDate.Value Then Return False
                End If
            Catch ex As FormatException
                Return False
            End Try

            Return True

        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class RejectOverlapsOrGaps
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_OVERLAP_OR_GAP_ERR)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As ClaimStage = CType(objectToValidate, ClaimStage)

            If valueToCheck Is Nothing Then Return True

            ' No Stage Name Selected
            If obj.StageNameId.Equals(Guid.Empty) Then Return True

            ' No Effective and Expiration dates for validation
            If obj.EffectiveDate Is Nothing OrElse obj.ExpirationDate Is Nothing Then Return True

            ' Invalid Dates - Effective Date is greater than Expiration Date
            If obj.EffectiveDate.Value >= obj.ExpirationDate.Value Then
                Return False
            End If

            Dim minMax As New MinEffDateMaxExpDate(obj)

            If obj.IsNew Then
                If minMax.IsLast Then
                    Return True
                ElseIf minMax.HasOverlap Then
                    Return False
                End If
            Else 'Dirty
                If minMax.IsLast Then
                    Return True
                ElseIf (obj.EffectiveDate.Value = obj.OriginalEffectiveDate.Value) AndAlso(obj.ExpirationDate.Value = obj.OriginalExpirationDate.Value) Then
                    Return True
                ElseIf minMax.HasOverlap Then
                    Return False
                End If
            End If

            Return True

        End Function
    End Class

    Public Class MinEffDateMaxExpDate
        Public IsLast As Boolean = False
        Public HasOverlap As Boolean = False

        Public Sub New(obj As ClaimStage)
            Dim tempEffective As Date
            Dim tempExpiration As Date
            Try
                Dim dal As New ClaimStageDAL
                Dim OtherRatesDs As DataSet = dal.LoadStagesWithSameDefinition(obj.Id, obj.StageNameId, obj.CompanyGroupId, obj.CompanyId, obj.DealerId, obj.ProductCode, obj.CoverageTypeId, obj.StartStatusId)
                If OtherRatesDs.Tables(0).Rows.Count > 0 Then
                    'Determine Overlap when :
                    '1. The current record has expiration less than or equal to any of the other records with Same Claim Stage Definition 
                    For Each otherRow As DataRow In OtherRatesDs.Tables(0).Rows
                        tempEffective = CType(otherRow(dal.COL_MIN_EFFECTIVE), Date)
                        tempExpiration = CType(otherRow(dal.COL_MAX_EXPIRATION), Date)
                        HasOverlap = ((obj.EffectiveDate.Value < tempExpiration) AndAlso (tempEffective < obj.ExpirationDate.Value))
                        If HasOverlap Then
                            Exit For
                        End If
                    Next
                Else ' If there is only one record then IsLast is True
                    IsLast = True
                End If

            Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End Sub
    End Class

#End Region

#Region "Children Related"

    Public Function GetAvailableStageStartStatusList(company_group_id As Guid, language_id As Guid) As DataView
        Dim dal As New ClaimStageDAL
        Return dal.GetAvailableStageStartStatusList(company_group_id, language_id)
    End Function

    Public Function GetAvailableStageEndStatusList(company_group_id As Guid, language_id As Guid) As DataView
        LoadChildren(False, language_id)
        Dim dal As New ClaimStageDAL
        Return dal.GetAvailableStageEndStatusList(company_group_id, language_id)
    End Function

    Public Function GetSelectedStageEndStatusList(stage_id As Guid, language_id As Guid) As DataView
        Dim dal As New ClaimStageDAL
        Return dal.GetSelectedStageEndStatusList(stage_id, language_id)
    End Function

#End Region

End Class