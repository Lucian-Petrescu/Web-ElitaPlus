'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (12/20/2012)  ********************

Public Class EventTask
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
            Dim dal As New EventTaskDAL
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
            Dim dal As New EventTaskDAL
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
    <ValidOnlyOneEntity(""), ValidOneEntitySelected("")> _
    Public ReadOnly Property Id() As Guid
        Get
            If Row(EventTaskDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(EventTaskDAL.COL_NAME_EVENT_TASK_ID), Byte()))
            End If
        End Get
    End Property

    Public Property CompanyGroupId() As Guid
        Get
            CheckDeleted()
            If Row(EventTaskDAL.COL_NAME_COMPANY_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(EventTaskDAL.COL_NAME_COMPANY_GROUP_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(EventTaskDAL.COL_NAME_COMPANY_GROUP_ID, Value)
        End Set
    End Property

    Public Property CompanyId() As Guid
        Get
            CheckDeleted()
            If Row(EventTaskDAL.COL_NAME_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(EventTaskDAL.COL_NAME_COMPANY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(EventTaskDAL.COL_NAME_COMPANY_ID, Value)
        End Set
    End Property

    Public Property CountryId() As Guid
        Get
            CheckDeleted()
            If Row(EventTaskDAL.COL_NAME_COUNTRY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(EventTaskDAL.COL_NAME_COUNTRY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(EventTaskDAL.COL_NAME_COUNTRY_ID, Value)
        End Set
    End Property

    Public Property DealerGroupId() As Guid
        Get
            CheckDeleted()
            If Row(EventTaskDAL.COL_NAME_DEALER_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(EventTaskDAL.COL_NAME_DEALER_GROUP_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(EventTaskDAL.COL_NAME_DEALER_GROUP_ID, Value)
        End Set
    End Property

    Public Property DealerId() As Guid
        Get
            CheckDeleted()
            If Row(EventTaskDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(EventTaskDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(EventTaskDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=5), ValidProductCode("")> _
    Public Property ProductCode() As String
        Get
            CheckDeleted()
            If Row(EventTaskDAL.COL_NAME_PRODUCT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(EventTaskDAL.COL_NAME_PRODUCT_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(EventTaskDAL.COL_NAME_PRODUCT_CODE, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property EventTypeId() As Guid
        Get
            CheckDeleted()
            If Row(EventTaskDAL.COL_NAME_EVENT_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(EventTaskDAL.COL_NAME_EVENT_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(EventTaskDAL.COL_NAME_EVENT_TYPE_ID, Value)
        End Set
    End Property

    'Public Property EventArguments() As String
    '    Get
    '        CheckDeleted()
    '        If Row(EventTaskDAL.COL_NAME_EVENT_ARGUMENTS) Is DBNull.Value Then
    '            Return Nothing
    '        Else
    '            Return CType(Row(EventTaskDAL.COL_NAME_EVENT_ARGUMENTS), String)
    '        End If
    '    End Get
    '    Set(ByVal Value As String)
    '        CheckDeleted()
    '        Me.SetValue(EventTaskDAL.COL_NAME_EVENT_ARGUMENTS, Value)
    '    End Set
    'End Property

    Public Property EventTaskParameters() As String
        Get
            CheckDeleted()
            If Row(EventTaskDAL.COL_NAME_EVENT_TASK_PARAMETERS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(EventTaskDAL.COL_NAME_EVENT_TASK_PARAMETERS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(EventTaskDAL.COL_NAME_EVENT_TASK_PARAMETERS, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property TaskId() As Guid
        Get
            CheckDeleted()
            If Row(EventTaskDAL.COL_NAME_TASK_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(EventTaskDAL.COL_NAME_TASK_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(EventTaskDAL.COL_NAME_TASK_ID, Value)
        End Set
    End Property

    ' <ValidCoverageType("")> _
    Public Property CoverageTypeId() As Guid
        Get
            CheckDeleted()
            If Row(EventTaskDAL.COL_NAME_COVERAGE_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(EventTaskDAL.COL_NAME_COVERAGE_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(EventTaskDAL.COL_NAME_COVERAGE_TYPE_ID, Value)
        End Set
    End Property

    <ValidNumericRange("", Min:=0, Max:=99)> _
    Public Property RetryCount() As LongType
        Get
            CheckDeleted()
            If Row(EventTaskDAL.COL_NAME_RETRY_COUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(EventTaskDAL.COL_NAME_RETRY_COUNT), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            SetValue(EventTaskDAL.COL_NAME_RETRY_COUNT, Value)
        End Set
    End Property

    <ValidNumericRange("", Min:=0, Max:=999999)> _
    Public Property RetryDelaySeconds() As LongType
        Get
            CheckDeleted()
            If Row(EventTaskDAL.COL_NAME_RETRY_DELAY_SECONDS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(EventTaskDAL.COL_NAME_RETRY_DELAY_SECONDS), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            SetValue(EventTaskDAL.COL_NAME_RETRY_DELAY_SECONDS, Value)
        End Set
    End Property

    <ValidNumericRange("", Min:=0, Max:=999999)> _
    Public Property TimeoutSeconds() As LongType
        Get
            CheckDeleted()
            If Row(EventTaskDAL.COL_NAME_TIMEOUT_SECONDS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(EventTaskDAL.COL_NAME_TIMEOUT_SECONDS), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            SetValue(EventTaskDAL.COL_NAME_TIMEOUT_SECONDS, Value)
        End Set
    End Property

    Public Property EventArgumentId() As Guid
        Get
            CheckDeleted()
            If Row(EventTaskDAL.COL_NAME_EVENT_ARGUMENT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(EventTaskDAL.COL_NAME_EVENT_ARGUMENT_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(EventTaskDAL.COL_NAME_EVENT_ARGUMENT_ID, Value)
        End Set
    End Property

    <ValidNumericRange("", Min:=0, Max:=999999)>
    Public Property InitDelayMinutes() As LongType
        Get
            CheckDeleted()
            If Row(EventTaskDAL.COL_NAME_INIT_DELAY_MINUTES) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(EventTaskDAL.COL_NAME_INIT_DELAY_MINUTES), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            SetValue(EventTaskDAL.COL_NAME_INIT_DELAY_MINUTES, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New EventTaskDAL
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

    Public Sub DeleteAndSave()
        BeginEdit()

        Try
            Delete()
            Save()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            cancelEdit()
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        Catch ex As RowNotInTableException
            ex = Nothing
        Catch ex As Exception
            cancelEdit()
            Throw ex
        End Try
    End Sub

#End Region

#Region "SearchDV"
    Public Class EventTaskSearchDV
        Inherits DataView

        Public Const COL_EVENT_TASK_ID As String = "EVENT_TASK_ID"
        Public Const COL_COMPANY_GROUP_ID As String = "company_group_id"
        Public Const COL_COMPANY_GROUP_DESC As String = "company_group_desc"
        Public Const COL_COMPANY_ID As String = "company_id"
        Public Const COL_COMPANY_DESC As String = "company_desc"
        Public Const COL_COUNTRY_ID As String = "country_id"
        Public Const COL_COUNTRY_DESC As String = "country_desc"
        Public Const COL_DEALER_GROUP_ID As String = "dealer_group_id"
        Public Const COL_DEALER_GROUP_DESC As String = "dealer_group_desc"
        Public Const COL_DEALER_ID As String = "dealer_id"
        Public Const COL_DEALER_DESC As String = "dealer_desc"
        Public Const COL_EVENT_TYPE_ID As String = "event_type_id"
        Public Const COL_EVENT_TYPE_DESC As String = "event_type_desc"
        Public Const COL_PRODUCT_CODE As String = "product_code"
        Public Const COL_TASK_ID As String = "task_id"
        Public Const COL_TASK_DESC As String = "task_desc"
        Public Const COL_RETRY_COUNT As String = "RETRY_COUNT"
        Public Const COL_RETRY_DELAY_SECONDS As String = "RETRY_DELAY_SECONDS"
        Public Const COL_TIMEOUT_SECONDS As String = "TIMEOUT_SECONDS"
        Public Const COL_COVERAGE_TYPE_ID As String = "coverage_type_id"
        Public Const COL_COVERAGE_TYPE_DESC As String = "coverage_type_desc"
        Public Const COL_EVENT_ARGUMENT_ID As String = "event_argument_id"
        Public Const COL_EVENT_ARGUMENT_DESC As String = "event_argument_desc"
        Public Const COL_EVENT_TASK_PARAMETERS As String = "event_task_parameters"
        Public Const COL_INIT_DELAY_MINUTES As String = "init_delay_minutes"

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

    Public Shared Sub AddNewRowToSearchDV(ByRef dv As EventTaskSearchDV, ByVal NewBO As EventTask)
        Dim dt As DataTable, blnEmptyTbl As Boolean = False

        If NewBO.IsNew Then
            Dim row As DataRow
            If dv Is Nothing Then
                Dim guidTemp As New Guid
                blnEmptyTbl = True
                dt = New DataTable
                dt.Columns.Add(EventTaskSearchDV.COL_EVENT_TASK_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(EventTaskSearchDV.COL_COMPANY_GROUP_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(EventTaskSearchDV.COL_COMPANY_GROUP_DESC, GetType(String))
                dt.Columns.Add(EventTaskSearchDV.COL_COMPANY_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(EventTaskSearchDV.COL_COMPANY_DESC, GetType(String))
                dt.Columns.Add(EventTaskSearchDV.COL_COUNTRY_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(EventTaskSearchDV.COL_COUNTRY_DESC, GetType(String))
                dt.Columns.Add(EventTaskSearchDV.COL_DEALER_GROUP_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(EventTaskSearchDV.COL_DEALER_GROUP_DESC, GetType(String))
                dt.Columns.Add(EventTaskSearchDV.COL_DEALER_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(EventTaskSearchDV.COL_DEALER_DESC, GetType(String))
                dt.Columns.Add(EventTaskSearchDV.COL_PRODUCT_CODE, GetType(String))
                dt.Columns.Add(EventTaskSearchDV.COL_EVENT_TYPE_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(EventTaskSearchDV.COL_EVENT_TYPE_DESC, GetType(String))
                dt.Columns.Add(EventTaskSearchDV.COL_TASK_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(EventTaskSearchDV.COL_TASK_DESC, GetType(String))
                dt.Columns.Add(EventTaskSearchDV.COL_RETRY_COUNT, GetType(Integer))
                dt.Columns.Add(EventTaskSearchDV.COL_RETRY_DELAY_SECONDS, GetType(Integer))
                dt.Columns.Add(EventTaskSearchDV.COL_TIMEOUT_SECONDS, GetType(Integer))
                dt.Columns.Add(EventTaskSearchDV.COL_COVERAGE_TYPE_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(EventTaskSearchDV.COL_COVERAGE_TYPE_DESC, GetType(String))
                dt.Columns.Add(EventTaskSearchDV.COL_EVENT_ARGUMENT_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(EventTaskSearchDV.COL_EVENT_ARGUMENT_DESC, GetType(String))
                dt.Columns.Add(EventTaskSearchDV.COL_EVENT_TASK_PARAMETERS, GetType(String))
                dt.Columns.Add(EventTaskSearchDV.COL_INIT_DELAY_MINUTES, GetType(String))
            Else
                dt = dv.Table
            End If

            row = dt.NewRow
            row(EventTaskSearchDV.COL_EVENT_TASK_ID) = NewBO.Id.ToByteArray
            row(EventTaskSearchDV.COL_COMPANY_GROUP_ID) = NewBO.CompanyGroupId.ToByteArray
            row(EventTaskSearchDV.COL_COMPANY_ID) = NewBO.CompanyId.ToByteArray
            row(EventTaskSearchDV.COL_COUNTRY_ID) = NewBO.CountryId.ToByteArray
            row(EventTaskSearchDV.COL_DEALER_GROUP_ID) = NewBO.CompanyGroupId.ToByteArray
            row(EventTaskSearchDV.COL_DEALER_ID) = NewBO.DealerId.ToByteArray
            row(EventTaskSearchDV.COL_PRODUCT_CODE) = NewBO.ProductCode
            row(EventTaskSearchDV.COL_EVENT_TYPE_ID) = NewBO.EventTypeId.ToByteArray
            row(EventTaskSearchDV.COL_TASK_ID) = NewBO.TaskId.ToByteArray
            row(EventTaskSearchDV.COL_COVERAGE_TYPE_ID) = NewBO.CoverageTypeId.ToByteArray
            row(EventTaskSearchDV.COL_EVENT_ARGUMENT_ID) = NewBO.EventArgumentId.ToByteArray
            row(EventTaskSearchDV.COL_EVENT_TASK_PARAMETERS) = NewBO.EventTaskParameters

            dt.Rows.Add(row)
            If blnEmptyTbl Then dv = New EventTaskSearchDV(dt)
        End If
    End Sub

    Public Shared Function getList(ByVal CompGrpID As Guid, ByVal CompanyID As Guid, ByVal CountryID As Guid,
                               ByVal DealerGrpID As Guid, ByVal DealerID As Guid, ByVal strProdCode As String, ByVal EventTypeID As Guid,
                             ByVal TaskID As Guid, ByVal CoverageTypeID As Guid) As EventTaskSearchDV
        Try
            Dim dal As New EventTaskDAL
            Return New EventTaskSearchDV(dal.LoadList(CompGrpID, CompanyID, CountryID, DealerGrpID, DealerID, strProdCode, EventTypeID, TaskID, CoverageTypeID, ElitaPlusIdentity.Current.ActiveUser.LanguageId, ElitaPlusIdentity.Current.ActiveUser.NetworkId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

#Region "Custom Validation"
    Public NotInheritable Class ValidProductCode
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, "DEALER OR DEALER GROUP IS REQUIRED")
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As EventTask = CType(objectToValidate, EventTask)

            If Not String.IsNullOrEmpty(obj.ProductCode) Then
                If obj.DealerId = Guid.Empty AndAlso obj.DealerGroupId = Guid.Empty Then
                    Return False
                Else
                    Return True
                End If
            Else
                Return True
            End If

        End Function
    End Class

    Public NotInheritable Class ValidCoverageType
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, "PRODUCT CODE AND DEALER IS REQUIRED")
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As EventTask = CType(objectToValidate, EventTask)

            If Not obj.CoverageTypeId = Guid.Empty Then
                If obj.DealerId = Guid.Empty Or obj.DealerGroupId = Guid.Empty Or String.IsNullOrEmpty(obj.ProductCode) Then
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

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, "ONLY_ONE_ALLOWED_COMPGRP_COMANPY_COUNTRY_DEALERGRP_DEALER")
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            If (Not valueToCheck Is Nothing) AndAlso valueToCheck <> Guid.Empty Then
                Dim obj As EventTask = CType(objectToValidate, EventTask)
                If obj.CompanyGroupId <> Guid.Empty Then
                    If obj.CompanyId <> Guid.Empty OrElse obj.CountryId <> Guid.Empty OrElse obj.DealerGroupId <> Guid.Empty OrElse obj.DealerId <> Guid.Empty Then
                        Return False
                    End If
                ElseIf obj.CompanyId <> Guid.Empty Then
                    If obj.CompanyGroupId <> Guid.Empty OrElse obj.CountryId <> Guid.Empty OrElse obj.DealerGroupId <> Guid.Empty OrElse obj.DealerId <> Guid.Empty Then
                        Return False
                    End If
                ElseIf obj.CountryId <> Guid.Empty Then
                    If obj.CompanyGroupId <> Guid.Empty OrElse obj.CompanyId <> Guid.Empty OrElse obj.DealerGroupId <> Guid.Empty OrElse obj.DealerId <> Guid.Empty Then
                        Return False
                    End If
                ElseIf obj.DealerGroupId <> Guid.Empty Then
                    If obj.CompanyGroupId <> Guid.Empty OrElse obj.CountryId <> Guid.Empty OrElse obj.CompanyId <> Guid.Empty OrElse obj.DealerId <> Guid.Empty Then
                        Return False
                    End If
                ElseIf obj.DealerId <> Guid.Empty Then
                    If obj.CompanyGroupId <> Guid.Empty OrElse obj.CountryId <> Guid.Empty OrElse obj.CompanyId <> Guid.Empty OrElse obj.DealerGroupId <> Guid.Empty Then
                        Return False
                    End If
                End If
            End If
            Return True

        End Function
    End Class

    Public NotInheritable Class ValidOneEntitySelected
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, "SELECT_ONE_COMPGRP_COMANPY_COUNTRY_DEALERGRP_DEALER")
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As EventTask = CType(objectToValidate, EventTask)
            If obj.CompanyGroupId = Guid.Empty AndAlso obj.CompanyId = Guid.Empty AndAlso obj.CountryId = Guid.Empty AndAlso obj.DealerGroupId = Guid.Empty AndAlso obj.DealerId = Guid.Empty Then
                Return False
            Else
                Return True
            End If
        End Function
    End Class
#End Region

End Class


