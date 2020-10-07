Imports System.Collections.Generic
'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (4/23/2013)  ********************

Public Class ServiceClassType
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
            Dim dal As New ServiceClassTypeDAL
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
            Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New ServiceClassTypeDAL
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
    Public ReadOnly Property Id As Guid
        Get
            If Row(ServiceClassTypeDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ServiceClassTypeDAL.COL_NAME_SERVICE_CLASS_TYPE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")>
    Public Property ServiceClassId As Guid
        Get
            CheckDeleted()
            If Row(ServiceClassTypeDAL.COL_NAME_SERVICE_CLASS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ServiceClassTypeDAL.COL_NAME_SERVICE_CLASS_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceClassTypeDAL.COL_NAME_SERVICE_CLASS_ID, Value)
        End Set
    End Property

    Public Property ServiceTypeId As Guid
        Get
            CheckDeleted()
            If Row(ServiceClassTypeDAL.COL_NAME_SERVICE_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ServiceClassTypeDAL.COL_NAME_SERVICE_TYPE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceClassTypeDAL.COL_NAME_SERVICE_TYPE_ID, Value)
        End Set
    End Property

    Public Property IsDeductibleId As Guid
        Get
            CheckDeleted()
            If Row(ServiceClassTypeDAL.COL_NAME_IS_DEDUCTIBLE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ServiceClassTypeDAL.COL_NAME_IS_DEDUCTIBLE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceClassTypeDAL.COL_NAME_IS_DEDUCTIBLE_ID, Value)
        End Set
    End Property

    Public Property IsStandardId As Guid
        Get
            CheckDeleted()
            If Row(ServiceClassTypeDAL.COL_NAME_IS_STANDARD_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ServiceClassTypeDAL.COL_NAME_IS_STANDARD_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceClassTypeDAL.COL_NAME_IS_STANDARD_ID, Value)
        End Set
    End Property

    Public Property ContainsDeductibleId As Guid
        Get
            CheckDeleted()
            If Row(ServiceClassTypeDAL.COL_NAME_CONTAINS_DEDUCTIBLE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ServiceClassTypeDAL.COL_NAME_CONTAINS_DEDUCTIBLE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceClassTypeDAL.COL_NAME_CONTAINS_DEDUCTIBLE_ID, Value)
        End Set
    End Property

    Public ReadOnly Property ServiceClassCode As String
        Get
            Return LookupListNew.GetCodeFromId(Codes.SERVICE_CLASS, ServiceClassId)
        End Get
    End Property

    Public ReadOnly Property ServiceTypeCode As String
        Get
            Return LookupListNew.GetCodeFromId(Codes.SERVICE_CLASS_TYPE, ServiceTypeId)
        End Get
    End Property

    Public ReadOnly Property IsDeductibleApplicable As Boolean
        Get
            If (IsDeductibleId = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y")) Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    Public ReadOnly Property IsStandardItem As Boolean
        Get
            If (IsStandardId = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y")) Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

    Public ReadOnly Property ContainsDeductible As Boolean
        Get
            If (ContainsDeductibleId = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "Y")) Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property

#End Region

#Region "Public Members"

    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ServiceClassTypeDAL
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

End Class

Public Class ServiceCLassTypeList
    Inherits List(Of ServiceClassType)

    ''' <summary>
    ''' Thread Syncronization Context for Singleton Object Creation 
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared _syncRoot As Object

    ''' <summary>
    ''' Static Instance Variable to store Singleton Instance
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared _instance As Dictionary(Of String, ServiceCLassTypeList)

    Private Sub New()
    End Sub

    Shared Sub New()
        _syncRoot = New Object()
    End Sub

    Friend Shared ReadOnly Property Instance As ServiceCLassTypeList
        Get
            Dim parameters As ElitaPlusParameters = Nothing
            Try
                parameters = CType(Threading.Thread.CurrentThread.CurrentPrincipal.Identity, ElitaPlusParameters)
            Catch ex As Exception
            End Try
            If (_instance Is Nothing) Then
                SyncLock (_syncRoot)
                    If (_instance Is Nothing) Then
                        _instance = New Dictionary(Of String, ServiceCLassTypeList)
                    End If
                End SyncLock
            End If
            If Not _instance.ContainsKey(parameters.ConnectionType.ToUpperInvariant()) Then
                SyncLock (_syncRoot)
                    _instance.Add(parameters.ConnectionType.ToUpperInvariant(), Load())
                End SyncLock
            End If
            Return _instance(parameters.ConnectionType.ToUpperInvariant())
        End Get
    End Property



    Private Shared Function Load() As List(Of ServiceClassType)
        Dim list As New ServiceCLassTypeList
        Dim dal As New ServiceClassTypeDAL
        Dim ds As DataSet = dal.LoadList()
        For Each item As DataRow In ds.Tables(ServiceClassTypeDAL.TABLE_NAME).Rows
            Dim var As New ServiceClassType
            var.ServiceClassId = New Guid(CType(item(ServiceClassTypeDAL.COL_NAME_SERVICE_CLASS_ID), Byte()))
            var.ServiceTypeId = New Guid(CType(item(ServiceClassTypeDAL.COL_NAME_SERVICE_TYPE_ID), Byte()))
            var.IsDeductibleId = New Guid(CType(item(ServiceClassTypeDAL.COL_NAME_IS_DEDUCTIBLE_ID), Byte()))
            var.IsStandardId = New Guid(CType(item(ServiceClassTypeDAL.COL_NAME_IS_STANDARD_ID), Byte()))
            var.ContainsDeductibleId = New Guid(CType(item(ServiceClassTypeDAL.COL_NAME_CONTAINS_DEDUCTIBLE_ID), Byte()))
            list.Add(var)
        Next
        Return list
    End Function

    Public Shared Function GetDetails(ByVal serviceClassCode As String, ByVal serviceTypeCode As String) As ServiceClassType
        Dim record As ServiceClassType
        For Each item As ServiceClassType In Instance
            If (item.ServiceClassCode = serviceClassCode) And item.ServiceTypeCode = serviceTypeCode Then
                record = item
                Exit For
            End If
        Next
        Return record
    End Function

    Public Shared Function GetDetails(ByVal serviceClassId As Guid, ByVal serviceTypeId As Guid) As ServiceClassType
        Dim record As ServiceClassType
        For Each item As ServiceClassType In Instance
            If (item.ServiceClassId = serviceClassId) And item.ServiceTypeId = serviceTypeId Then
                record = item
                Exit For
            End If
        Next
        Return record
    End Function

    Public Shared Function IsDeductibleApplicable(ByVal serviceClassId As Guid, ByVal serviceTypeId As Guid) As Boolean
        Dim serviceClassType as ServiceClassType = GetDetails(serviceClassId, serviceTypeId)
        If serviceClassType Is Nothing Then
            Return False
        Else
            Return serviceClassType.IsDeductibleApplicable
        End If
    End Function

End Class




