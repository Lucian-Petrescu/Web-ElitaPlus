﻿Public Class ClaimHistory
    '************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (9/12/2008)  ********************

    Public Class ClaimHistory
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
                Dim dal As New ClaimHistoryDAL
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
                Dim dal As New ClaimHistoryDAL
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
                If row(ClaimHistoryDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return New Guid(CType(row(ClaimHistoryDAL.COL_NAME_CLAIM_HISTORY_ID), Byte()))
                End If
            End Get
        End Property

        <ValueMandatory("")> _
        Public Property ClaimId() As Guid
            Get
                CheckDeleted()
                If row(ClaimHistoryDAL.COL_NAME_CLAIM_ID) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return New Guid(CType(row(ClaimHistoryDAL.COL_NAME_CLAIM_ID), Byte()))
                End If
            End Get
            Set(ByVal Value As Guid)
                CheckDeleted()
                Me.SetValue(ClaimHistoryDAL.COL_NAME_CLAIM_ID, Value)
            End Set
        End Property


        <ValidStringLength("", Max:=4)> _
        Public Property StatusCodeOld() As String
            Get
                CheckDeleted()
                If row(ClaimHistoryDAL.COL_NAME_STATUS_CODE_OLD) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return CType(row(ClaimHistoryDAL.COL_NAME_STATUS_CODE_OLD), String)
                End If
            End Get
            Set(ByVal Value As String)
                CheckDeleted()
                Me.SetValue(ClaimHistoryDAL.COL_NAME_STATUS_CODE_OLD, Value)
            End Set
        End Property


        <ValidStringLength("", Max:=4)> _
        Public Property StatusCodeNew() As String
            Get
                CheckDeleted()
                If row(ClaimHistoryDAL.COL_NAME_STATUS_CODE_NEW) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return CType(row(ClaimHistoryDAL.COL_NAME_STATUS_CODE_NEW), String)
                End If
            End Get
            Set(ByVal Value As String)
                CheckDeleted()
                Me.SetValue(ClaimHistoryDAL.COL_NAME_STATUS_CODE_NEW, Value)
            End Set
        End Property



        Public Property AuthorizedAmountOld() As DecimalType
            Get
                CheckDeleted()
                If row(ClaimHistoryDAL.COL_NAME_AUTHORIZED_AMOUNT_OLD) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return New DecimalType(CType(row(ClaimHistoryDAL.COL_NAME_AUTHORIZED_AMOUNT_OLD), Decimal))
                End If
            End Get
            Set(ByVal Value As DecimalType)
                CheckDeleted()
                Me.SetValue(ClaimHistoryDAL.COL_NAME_AUTHORIZED_AMOUNT_OLD, Value)
            End Set
        End Property



        Public Property AuthorizedAmountNew() As DecimalType
            Get
                CheckDeleted()
                If row(ClaimHistoryDAL.COL_NAME_AUTHORIZED_AMOUNT_NEW) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return New DecimalType(CType(row(ClaimHistoryDAL.COL_NAME_AUTHORIZED_AMOUNT_NEW), Decimal))
                End If
            End Get
            Set(ByVal Value As DecimalType)
                CheckDeleted()
                Me.SetValue(ClaimHistoryDAL.COL_NAME_AUTHORIZED_AMOUNT_NEW, Value)
            End Set
        End Property



        Public Property ClaimClosedDateOld() As DateType
            Get
                CheckDeleted()
                If row(ClaimHistoryDAL.COL_NAME_CLAIM_CLOSED_DATE_OLD) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return New DateType(CType(row(ClaimHistoryDAL.COL_NAME_CLAIM_CLOSED_DATE_OLD), Date))
                End If
            End Get
            Set(ByVal Value As DateType)
                CheckDeleted()
                Me.SetValue(ClaimHistoryDAL.COL_NAME_CLAIM_CLOSED_DATE_OLD, Value)
            End Set
        End Property



        Public Property ClaimClosedDateNew() As DateType
            Get
                CheckDeleted()
                If row(ClaimHistoryDAL.COL_NAME_CLAIM_CLOSED_DATE_NEW) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return New DateType(CType(row(ClaimHistoryDAL.COL_NAME_CLAIM_CLOSED_DATE_NEW), Date))
                End If
            End Get
            Set(ByVal Value As DateType)
                CheckDeleted()
                Me.SetValue(ClaimHistoryDAL.COL_NAME_CLAIM_CLOSED_DATE_NEW, Value)
            End Set
        End Property



        Public Property RepairDateOld() As DateType
            Get
                CheckDeleted()
                If row(ClaimHistoryDAL.COL_NAME_REPAIR_DATE_OLD) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return New DateType(CType(row(ClaimHistoryDAL.COL_NAME_REPAIR_DATE_OLD), Date))
                End If
            End Get
            Set(ByVal Value As DateType)
                CheckDeleted()
                Me.SetValue(ClaimHistoryDAL.COL_NAME_REPAIR_DATE_OLD, Value)
            End Set
        End Property



        Public Property RepairDateNew() As DateType
            Get
                CheckDeleted()
                If row(ClaimHistoryDAL.COL_NAME_REPAIR_DATE_NEW) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return New DateType(CType(row(ClaimHistoryDAL.COL_NAME_REPAIR_DATE_NEW), Date))
                End If
            End Get
            Set(ByVal Value As DateType)
                CheckDeleted()
                Me.SetValue(ClaimHistoryDAL.COL_NAME_REPAIR_DATE_NEW, Value)
            End Set
        End Property


        <ValueMandatory("")> _
        Public Property ClaimModifiedDateNew() As DateType
            Get
                CheckDeleted()
                If row(ClaimHistoryDAL.COL_NAME_CLAIM_MODIFIED_DATE_NEW) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return New DateType(CType(row(ClaimHistoryDAL.COL_NAME_CLAIM_MODIFIED_DATE_NEW), Date))
                End If
            End Get
            Set(ByVal Value As DateType)
                CheckDeleted()
                Me.SetValue(ClaimHistoryDAL.COL_NAME_CLAIM_MODIFIED_DATE_NEW, Value)
            End Set
        End Property


        <ValueMandatory(""), ValidStringLength("", Max:=120)> _
        Public Property ClaimModifiedByNew() As String
            Get
                CheckDeleted()
                If row(ClaimHistoryDAL.COL_NAME_CLAIM_MODIFIED_BY_NEW) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return CType(row(ClaimHistoryDAL.COL_NAME_CLAIM_MODIFIED_BY_NEW), String)
                End If
            End Get
            Set(ByVal Value As String)
                CheckDeleted()
                Me.SetValue(ClaimHistoryDAL.COL_NAME_CLAIM_MODIFIED_BY_NEW, Value)
            End Set
        End Property



        Public Property ClaimModifiedDateOld() As DateType
            Get
                CheckDeleted()
                If row(ClaimHistoryDAL.COL_NAME_CLAIM_MODIFIED_DATE_OLD) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return New DateType(CType(row(ClaimHistoryDAL.COL_NAME_CLAIM_MODIFIED_DATE_OLD), Date))
                End If
            End Get
            Set(ByVal Value As DateType)
                CheckDeleted()
                Me.SetValue(ClaimHistoryDAL.COL_NAME_CLAIM_MODIFIED_DATE_OLD, Value)
            End Set
        End Property


        <ValidStringLength("", Max:=120)> _
        Public Property ClaimModifiedByOld() As String
            Get
                CheckDeleted()
                If row(ClaimHistoryDAL.COL_NAME_CLAIM_MODIFIED_BY_OLD) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return CType(row(ClaimHistoryDAL.COL_NAME_CLAIM_MODIFIED_BY_OLD), String)
                End If
            End Get
            Set(ByVal Value As String)
                CheckDeleted()
                Me.SetValue(ClaimHistoryDAL.COL_NAME_CLAIM_MODIFIED_BY_OLD, Value)
            End Set
        End Property



        Public Property LiabilityLimitOld() As DecimalType
            Get
                CheckDeleted()
                If row(ClaimHistoryDAL.COL_NAME_LIABILITY_LIMIT_OLD) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return New DecimalType(CType(row(ClaimHistoryDAL.COL_NAME_LIABILITY_LIMIT_OLD), Decimal))
                End If
            End Get
            Set(ByVal Value As DecimalType)
                CheckDeleted()
                Me.SetValue(ClaimHistoryDAL.COL_NAME_LIABILITY_LIMIT_OLD, Value)
            End Set
        End Property



        Public Property LiabilityLimitNew() As DecimalType
            Get
                CheckDeleted()
                If row(ClaimHistoryDAL.COL_NAME_LIABILITY_LIMIT_NEW) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return New DecimalType(CType(row(ClaimHistoryDAL.COL_NAME_LIABILITY_LIMIT_NEW), Decimal))
                End If
            End Get
            Set(ByVal Value As DecimalType)
                CheckDeleted()
                Me.SetValue(ClaimHistoryDAL.COL_NAME_LIABILITY_LIMIT_NEW, Value)
            End Set
        End Property



        Public Property CertItemCoverageIdOld() As Guid
            Get
                CheckDeleted()
                If row(ClaimHistoryDAL.COL_NAME_CERT_ITEM_COVERAGE_ID_OLD) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return New Guid(CType(row(ClaimHistoryDAL.COL_NAME_CERT_ITEM_COVERAGE_ID_OLD), Byte()))
                End If
            End Get
            Set(ByVal Value As Guid)
                CheckDeleted()
                Me.SetValue(ClaimHistoryDAL.COL_NAME_CERT_ITEM_COVERAGE_ID_OLD, Value)
            End Set
        End Property



        Public Property CertItemCoverageIdNew() As Guid
            Get
                CheckDeleted()
                If row(ClaimHistoryDAL.COL_NAME_CERT_ITEM_COVERAGE_ID_NEW) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return New Guid(CType(row(ClaimHistoryDAL.COL_NAME_CERT_ITEM_COVERAGE_ID_NEW), Byte()))
                End If
            End Get
            Set(ByVal Value As Guid)
                CheckDeleted()
                Me.SetValue(ClaimHistoryDAL.COL_NAME_CERT_ITEM_COVERAGE_ID_NEW, Value)
            End Set
        End Property



        Public Property DeductibleNew() As DecimalType
            Get
                CheckDeleted()
                If row(ClaimHistoryDAL.COL_NAME_DEDUCTIBLE_NEW) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return New DecimalType(CType(row(ClaimHistoryDAL.COL_NAME_DEDUCTIBLE_NEW), Decimal))
                End If
            End Get
            Set(ByVal Value As DecimalType)
                CheckDeleted()
                Me.SetValue(ClaimHistoryDAL.COL_NAME_DEDUCTIBLE_NEW, Value)
            End Set
        End Property



        Public Property DeductibleOld() As DecimalType
            Get
                CheckDeleted()
                If row(ClaimHistoryDAL.COL_NAME_DEDUCTIBLE_OLD) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return New DecimalType(CType(row(ClaimHistoryDAL.COL_NAME_DEDUCTIBLE_OLD), Decimal))
                End If
            End Get
            Set(ByVal Value As DecimalType)
                CheckDeleted()
                Me.SetValue(ClaimHistoryDAL.COL_NAME_DEDUCTIBLE_OLD, Value)
            End Set
        End Property


        Public Property ServiceCenterNew() As String
            Get
                CheckDeleted()
                If Row(ClaimHistoryDAL.COL_NAME_SERVICE_CENTER_NEW) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return CType(Row(ClaimHistoryDAL.COL_NAME_SERVICE_CENTER_NEW), String)
                End If
            End Get
            Set(ByVal Value As String)
                CheckDeleted()
                Me.SetValue(ClaimHistoryDAL.COL_NAME_SERVICE_CENTER_NEW, Value)
            End Set
        End Property

        Public Property ServiceCenterOld() As String
            Get
                CheckDeleted()
                If Row(ClaimHistoryDAL.COL_NAME_SERVICE_CENTER_OLD) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return CType(Row(ClaimHistoryDAL.COL_NAME_SERVICE_CENTER_OLD), String)
                End If
            End Get
            Set(ByVal Value As String)
                CheckDeleted()
                Me.SetValue(ClaimHistoryDAL.COL_NAME_SERVICE_CENTER_OLD, Value)
            End Set
        End Property

        <ValidStringLength("", Max:=15)>
        Public Property BatchNumberNew() As String
            Get
                CheckDeleted()
                If Row(ClaimHistoryDAL.COL_NAME_BATCH_NUMBER_NEW) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return CType(Row(ClaimHistoryDAL.COL_NAME_BATCH_NUMBER_NEW), String)
                End If
            End Get
            Set(ByVal Value As String)
                CheckDeleted()
                Me.SetValue(ClaimHistoryDAL.COL_NAME_BATCH_NUMBER_NEW, Value)
            End Set
        End Property

        <ValidStringLength("", Max:=10)> _
        Public Property BatchNumberOld() As String
            Get
                CheckDeleted()
                If Row(ClaimHistoryDAL.COL_NAME_BATCH_NUMBER_OLD) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return CType(Row(ClaimHistoryDAL.COL_NAME_BATCH_NUMBER_OLD), String)
                End If
            End Get
            Set(ByVal Value As String)
                CheckDeleted()
                Me.SetValue(ClaimHistoryDAL.COL_NAME_BATCH_NUMBER_OLD, Value)
            End Set
        End Property

        Public Property IsLawsuitIdOld() As Guid
            Get
                CheckDeleted()
                If Row(ClaimHistoryDAL.COL_NAME_IS_LAWSUIT_ID_OLD) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return New Guid(CType(Row(ClaimHistoryDAL.COL_NAME_IS_LAWSUIT_ID_OLD), Byte()))
                End If
            End Get
            Set(ByVal Value As Guid)
                CheckDeleted()
                Me.SetValue(ClaimHistoryDAL.COL_NAME_IS_LAWSUIT_ID_OLD, Value)
            End Set
        End Property

        Public Property IsLawsuitIdNew() As Guid
            Get
                CheckDeleted()
                If Row(ClaimHistoryDAL.COL_NAME_IS_LAWSUIT_ID_NEW) Is DBNull.Value Then
                    Return Nothing
                Else
                    Return New Guid(CType(Row(ClaimHistoryDAL.COL_NAME_IS_LAWSUIT_ID_NEW), Byte()))
                End If
            End Get
            Set(ByVal Value As Guid)
                CheckDeleted()
                Me.SetValue(ClaimHistoryDAL.COL_NAME_IS_LAWSUIT_ID_NEW, Value)
            End Set
        End Property
#End Region

#Region "Public Members"
        Public Overrides Sub Save()
            Try
                MyBase.Save()
                If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                    Dim dal As New ClaimHistoryDAL
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
#End Region

#Region "DataView Retrieveing Methods"

#End Region

    End Class

    Public Class ClaimHistoryList
        Inherits BusinessObjectListEnumerableBase(Of ClaimBase, ClaimHistory)

        Public Sub New(ByVal parent As ClaimBase)
            MyBase.New(LoadTable(parent), parent)
        End Sub

        Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
            Return CType(bo, ClaimHistory).ClaimId.Equals(CType(Parent, ClaimBase).Id)
        End Function

        Private Shared Function LoadTable(ByVal parent As ClaimBase) As DataTable
            Try
                If Not parent.IsChildrenCollectionLoaded(GetType(ClaimHistory)) Then
                    Dim dal As New ClaimHistoryDAL
                    dal.LoadList(parent.Dataset, parent.Id)
                    parent.AddChildrenCollection(GetType(ClaimHistoryList))
                End If
                Return parent.Dataset.Tables(ClaimHistoryDAL.TABLE_NAME)
            Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End Function
    End Class



End Class
