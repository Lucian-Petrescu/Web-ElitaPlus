'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (12/20/2012)  ********************

Public Class EventConfig
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
            Dim dal As New EventConfigDAL
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
            Dim dal As New EventConfigDAL
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

#Region "Properties"

    'Key Property
    <ValidOnlyOneEntity(""), ValidOneEntitySelected("")>
    Public ReadOnly Property Id As Guid
        Get
            If Row(EventConfigDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(EventConfigDAL.COL_NAME_EVENT_CONFIG_ID), Byte()))
            End If
        End Get
    End Property


    Public Property CompanyGroupId As Guid
        Get
            CheckDeleted()
            If Row(EventConfigDAL.COL_NAME_COMPANY_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(EventConfigDAL.COL_NAME_COMPANY_GROUP_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EventConfigDAL.COL_NAME_COMPANY_GROUP_ID, Value)
        End Set
    End Property

    Public Property CompanyId As Guid
        Get
            CheckDeleted()
            If Row(EventConfigDAL.COL_NAME_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(EventConfigDAL.COL_NAME_COMPANY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EventConfigDAL.COL_NAME_COMPANY_ID, Value)
        End Set
    End Property


    Public Property CountryId As Guid
        Get
            CheckDeleted()
            If Row(EventConfigDAL.COL_NAME_COUNTRY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(EventConfigDAL.COL_NAME_COUNTRY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EventConfigDAL.COL_NAME_COUNTRY_ID, Value)
        End Set
    End Property

    Public Property DealerGroupId As Guid
        Get
            CheckDeleted()
            If Row(EventConfigDAL.COL_NAME_DEALER_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(EventConfigDAL.COL_NAME_DEALER_GROUP_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EventConfigDAL.COL_NAME_DEALER_GROUP_ID, Value)
        End Set
    End Property

    Public Property DealerId As Guid
        Get
            CheckDeleted()
            If Row(EventConfigDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(EventConfigDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EventConfigDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=5), ValidProductCode("")>
    Public Property ProductCode As String
        Get
            CheckDeleted()
            If Row(EventConfigDAL.COL_NAME_PRODUCT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(EventConfigDAL.COL_NAME_PRODUCT_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EventConfigDAL.COL_NAME_PRODUCT_CODE, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property EventTypeId As Guid
        Get
            CheckDeleted()
            If Row(EventConfigDAL.COL_NAME_EVENT_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(EventConfigDAL.COL_NAME_EVENT_TYPE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EventConfigDAL.COL_NAME_EVENT_TYPE_ID, Value)
        End Set
    End Property

    '<ValidCoverageType("")>
    Public Property CoverageTypeId As Guid
        Get
            CheckDeleted()
            If Row(EventConfigDAL.COL_NAME_COVERAGE_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(EventConfigDAL.COL_NAME_COVERAGE_TYPE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EventConfigDAL.COL_NAME_COVERAGE_TYPE_ID, Value)
        End Set
    End Property

    Public Property EventArgumentId As Guid
        Get
            CheckDeleted()
            If Row(EventConfigDAL.COL_NAME_EVENT_ARGUMENT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(EventConfigDAL.COL_NAME_EVENT_ARGUMENT_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(EventConfigDAL.COL_NAME_EVENT_ARGUMENT_ID, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New EventConfigDAL
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
#End Region

#Region "DataView Retrieveing Methods"

#End Region

#Region "SearchDV"
    Public Class EventConfigSearchDV
        Inherits DataView

        Public Const COL_EVENT_CONFIG_ID As String = "event_config_id"
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
        Public Const COL_COVERAGE_TYPE_ID As String = "coverage_type_id"
        Public Const COL_COVERAGE_TYPE_DESC As String = "coverage_type_desc"
        Public Const COL_EVENT_ARGUMENT_ID As String = "event_argument_id"
        Public Const COL_EVENT_ARGUMENT_DESC As String = "event_argument_desc"
        Public Const COL_PRODUCT_CODE As String = "product_code"

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

    Public Shared Sub AddNewRowToSearchDV(ByRef dv As EventConfigSearchDV, ByVal NewBO As EventConfig)
        Dim dt As DataTable, blnEmptyTbl As Boolean = False

        If NewBO.IsNew Then
            Dim row As DataRow
            If dv Is Nothing Then
                Dim guidTemp As New Guid
                blnEmptyTbl = True
                dt = New DataTable
                dt.Columns.Add(EventConfigSearchDV.COL_EVENT_CONFIG_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(EventConfigSearchDV.COL_COMPANY_GROUP_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(EventConfigSearchDV.COL_COMPANY_GROUP_DESC, GetType(String))
                dt.Columns.Add(EventConfigSearchDV.COL_COMPANY_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(EventConfigSearchDV.COL_COMPANY_DESC, GetType(String))
                dt.Columns.Add(EventConfigSearchDV.COL_COUNTRY_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(EventConfigSearchDV.COL_COUNTRY_DESC, GetType(String))
                dt.Columns.Add(EventConfigSearchDV.COL_DEALER_GROUP_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(EventConfigSearchDV.COL_DEALER_GROUP_DESC, GetType(String))
                dt.Columns.Add(EventConfigSearchDV.COL_DEALER_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(EventConfigSearchDV.COL_DEALER_DESC, GetType(String))
                dt.Columns.Add(EventConfigSearchDV.COL_PRODUCT_CODE, GetType(String))
                dt.Columns.Add(EventConfigSearchDV.COL_EVENT_TYPE_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(EventConfigSearchDV.COL_EVENT_TYPE_DESC, GetType(String))
                dt.Columns.Add(EventConfigSearchDV.COL_COVERAGE_TYPE_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(EventConfigSearchDV.COL_COVERAGE_TYPE_DESC, GetType(String))
                dt.Columns.Add(EventConfigSearchDV.COL_EVENT_ARGUMENT_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(EventConfigSearchDV.COL_EVENT_ARGUMENT_DESC, GetType(String))
            Else
                dt = dv.Table
            End If

            row = dt.NewRow
            row(EventConfigSearchDV.COL_EVENT_CONFIG_ID) = NewBO.Id.ToByteArray
            row(EventConfigSearchDV.COL_COMPANY_GROUP_ID) = NewBO.CompanyGroupId.ToByteArray
            row(EventConfigSearchDV.COL_COMPANY_ID) = NewBO.CompanyId.ToByteArray
            row(EventConfigSearchDV.COL_COUNTRY_ID) = NewBO.CountryId.ToByteArray
            row(EventConfigSearchDV.COL_DEALER_GROUP_ID) = NewBO.DealerGroupId.ToByteArray
            row(EventConfigSearchDV.COL_DEALER_ID) = NewBO.DealerId.ToByteArray
            row(EventConfigSearchDV.COL_PRODUCT_CODE) = NewBO.ProductCode
            row(EventConfigSearchDV.COL_EVENT_TYPE_ID) = NewBO.EventTypeId.ToByteArray
            row(EventConfigSearchDV.COL_COVERAGE_TYPE_ID) = NewBO.CoverageTypeId.ToByteArray
            row(EventConfigSearchDV.COL_EVENT_ARGUMENT_ID) = NewBO.EventArgumentId.ToByteArray
            dt.Rows.Add(row)
            If blnEmptyTbl Then dv = New EventConfigSearchDV(dt)
        End If
    End Sub

    Public Shared Function getList(ByVal CompGrpID As Guid, ByVal CompanyID As Guid, ByVal CountryID As Guid, ByVal DealerGroupID As Guid,
                             ByVal DealerID As Guid, ByVal strProdCode As String, ByVal CoverageTypeID As Guid) As EventConfigSearchDV
        Try
            Dim dal As New EventConfigDAL
            Return New EventConfigSearchDV(dal.LoadList(CompGrpID, CompanyID, CountryID, DealerGroupID, DealerID, strProdCode, CoverageTypeID, ElitaPlusIdentity.Current.ActiveUser.LanguageId, ElitaPlusIdentity.Current.ActiveUser.NetworkId.ToUpper()).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

#Region "Custom Validation"
    Public NotInheritable Class ValidProductCode
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, "DEALER IS REQUIRED")
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As EventConfig = CType(objectToValidate, EventConfig)

            If Not String.IsNullOrEmpty(obj.ProductCode) Then
                If obj.DealerId = Guid.Empty And obj.DealerGroupId = Guid.Empty Then
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
            Dim obj As EventConfig = CType(objectToValidate, EventConfig)

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
                Dim obj As EventConfig = CType(objectToValidate, EventConfig)

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
            Dim obj As EventConfig = CType(objectToValidate, EventConfig)
            If obj.CompanyGroupId = Guid.Empty AndAlso obj.CompanyId = Guid.Empty AndAlso obj.CountryId = Guid.Empty AndAlso obj.DealerGroupId = Guid.Empty AndAlso obj.DealerId = Guid.Empty Then
                Return False
            Else
                Return True
            End If
        End Function
    End Class
#End Region
End Class


