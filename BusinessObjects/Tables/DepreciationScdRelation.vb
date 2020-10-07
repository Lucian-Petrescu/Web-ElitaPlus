'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (10/9/2017)  ********************
Imports System.Collections.Generic

Public Class DepreciationScdRelation
    Inherits BusinessObjectBase
#Region "Constants"
    Public Const ContractTableName As String = ContractDAL.TABLE_NAME
#End Region

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
    Public Sub New(id As Guid, familyDs As DataSet)
        MyBase.New(False)
        Dataset = familyDs
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(familyDs As DataSet)
        MyBase.New(False)
        Dataset = familyDs
        Load()
    End Sub

    Public Sub New(row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try

            If Dataset.Tables.IndexOf(DepreciationScdRelationDal.TableName) < 0 Then
                Dim dal As New DepreciationScdRelationDal
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(DepreciationScdRelationDal.TableName).NewRow
            Dataset.Tables(DepreciationScdRelationDal.TableName).Rows.Add(newRow)
            Row = newRow
            SetValue(DepreciationScdRelationDal.TableKeyName, Guid.NewGuid)
            Initialize()
        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(id As Guid)
        Try
            Dim dal As New DepreciationScdRelationDal
            If _isDSCreator Then
                If Not Row Is Nothing Then
                    Dataset.Tables(DepreciationScdRelationDal.TableName).Rows.Remove(Row)
                End If
            End If
            Row = Nothing
            If Dataset.Tables.IndexOf(DepreciationScdRelationDal.TableName) >= 0 Then
                Row = FindRow(id, DepreciationScdRelationDal.TableKeyName, Dataset.Tables(DepreciationScdRelationDal.TableName))
            End If
            If Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.Load(Dataset, id)
                Row = FindRow(id, DepreciationScdRelationDal.TableKeyName, Dataset.Tables(DepreciationScdRelationDal.TableName))
            End If
            If Row Is Nothing Then
                Throw New DataNotFoundException
            End If
        Catch ex As DataBaseAccessException
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
            If Row(DepreciationScdRelationDal.TableKeyName) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DepreciationScdRelationDal.ColNameDepreciationScdRelationId), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), OverlapExists("")>
    Public Property DepreciationScheduleId As Guid
        Get
            CheckDeleted()
            If Row(DepreciationScdRelationDal.ColNameDepreciationScheduleId) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DepreciationScdRelationDal.ColNameDepreciationScheduleId), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DepreciationScdRelationDal.ColNameDepreciationScheduleId, value)
        End Set
    End Property


    <ValidStringLength("", Max:=120)>
    Public Property DepreciationScheduleCode As String
        Get
            CheckDeleted()
            If Row(DepreciationScdRelationDal.ColNameDepreciationScheduleCode) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DepreciationScdRelationDal.ColNameDepreciationScheduleCode), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DepreciationScdRelationDal.ColNameDepreciationScheduleCode, value)
        End Set
    End Property
    <ValueMandatory(""), ValidStringLength("", Max:=120)>
    Public Property TableReference As String
        Get
            CheckDeleted()
            If Row(DepreciationScdRelationDal.ColNameTableReference) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DepreciationScdRelationDal.ColNameTableReference), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DepreciationScdRelationDal.ColNameTableReference, value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property TableReferenceId As Guid
        Get
            CheckDeleted()
            If Row(DepreciationScdRelationDal.ColNameTableReferenceId) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DepreciationScdRelationDal.ColNameTableReferenceId), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DepreciationScdRelationDal.ColNameTableReferenceId, value)
        End Set
    End Property


    <ValueMandatory(""),
        DateCompareValidatorAttribute("", Common.ErrorCodes.GUI_INVALID_EFFECTIVE_HIGHER_EXPIRATION_DATE,
            "ExpirationDate", DateCompareValidatorAttribute.CompareType.LessThanOrEqual, DefaultCompareToValue:=DateCompareValidatorAttribute.DefaultType.MaxDate,
            DefaultCompareValue:=DateCompareValidatorAttribute.DefaultType.MinDate),
        DateCompareValidatorAttribute("", Common.ErrorCodes.GUI_INVALID_EFFECTIVE_DATE_SMALLER_EQUAL_THAN_SYSDATE,
            "", DateCompareValidatorAttribute.CompareType.GreaterThan, CheckWhenNew:=True, CompareToType:=DateCompareValidatorAttribute.CompareToPropertyType.Nothing, DefaultCompareToValue:=DateCompareValidatorAttribute.DefaultType.Today)
           >
    Public Property EffectiveDate As DateType
        Get
            CheckDeleted()
            If Row(DepreciationScdRelationDal.ColNameEffectiveDate) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(DepreciationScdRelationDal.ColNameEffectiveDate), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DepreciationScdRelationDal.ColNameEffectiveDate, value)
        End Set
    End Property


    <ValueMandatory(""), DateCompareValidatorAttribute("", Common.ErrorCodes.GUI_INVALID_EFFECTIVE_HIGHER_EXPIRATION_DATE,
            "EffectiveDate", DateCompareValidatorAttribute.CompareType.GreaterThanOrEqual, DefaultCompareToValue:=DateCompareValidatorAttribute.DefaultType.MinDate,
            DefaultCompareValue:=DateCompareValidatorAttribute.DefaultType.MaxDate),
            DateCompareValidatorAttribute("", Common.ErrorCodes.GUI_INVALID_EXPIRATION_DATE_SMALLER_THAN_SYSDATE,
            "", DateCompareValidatorAttribute.CompareType.GreaterThanOrEqual, CompareToType:=DateCompareValidatorAttribute.CompareToPropertyType.Nothing, DefaultCompareToValue:=DateCompareValidatorAttribute.DefaultType.Today)>
    Public Property ExpirationDate As DateType
        Get
            CheckDeleted()
            If Row(DepreciationScdRelationDal.ColNameExpirationDate) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(DepreciationScdRelationDal.ColNameExpirationDate), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DepreciationScdRelationDal.ColNameExpirationDate, value)
        End Set
    End Property


    <ValidStringLength("", Max:=120), ValueMandatory("")>
    Public Property DepreciationScheduleUsageXcd As String
        Get
            CheckDeleted()
            If Row(DepreciationScdRelationDal.ColNameDepreciationSchUsageXcd) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DepreciationScdRelationDal.ColNameDepreciationSchUsageXcd), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DepreciationScdRelationDal.ColNameDepreciationSchUsageXcd, value)
        End Set
    End Property
    <ValidStringLength("", Max:=120)>
    Public Property DepreciationScheduleUsage As String
        Get
            CheckDeleted()
            If Row(DepreciationScdRelationDal.ColNameDepreciationSchUsage) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DepreciationScdRelationDal.ColNameDepreciationSchUsage), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DepreciationScdRelationDal.ColNameDepreciationSchUsage, value)
        End Set
    End Property



#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New DepreciationScdRelationDal
                dal.Update(Row)
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Id
                    Dataset = New DataSet
                    Row = Nothing
                    Load(objId)
                End If
            End If
        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
    Public Sub SaveWithoutCheckDsCreator()
        Try
            MyBase.Save()
            If IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New DepreciationScdRelationDal
                dal.Update(Row)
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Id
                    Dataset = New DataSet
                    Row = Nothing
                    Load(objId)
                End If
            End If
        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

#End Region

#Region "DataView Retrieveing Methods"

    Public Function LoadList(referenceId As Guid) As DataView
        Try
            Dim dal As New DepreciationScdRelationDal
            Dim ds As DataSet

            ds = dal.LoadList(referenceId) ' referenceId can be contract Id, Product ID ...
            Return (ds.Tables(DepreciationScdRelationDal.TableName).DefaultView)

        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetDepreciationScheduleList() As List(Of DepreciationScdRelation)
        Return GetDepreciationScheduleList(New Guid)
    End Function

    Public Shared Function GetDepreciationScheduleList(id As Guid) As List(Of DepreciationScdRelation)
        Dim dal As New DepreciationScdRelationDal
        Dim ds As DataSet = dal.LoadList(id) ' Id can be contract Id, Product ID ...
        Dim dsList As New List(Of DepreciationScdRelation)
        For Each dr As DataRow In ds.Tables(0).Rows
            dsList.Add(New DepreciationScdRelation(dr))
        Next
        Return dsList
    End Function
#End Region
    Public Function IsExpirationDateEditable() As Boolean
        Dim blnEdit As Boolean = True
        If Not IsNothing(ExpirationDate) Then
            If (ExpirationDate.Value <= Date.UtcNow) Then
                Return False
            End If
        End If
        Return blnEdit
    End Function
    Public Function IsDeleteAllowed() As Boolean
        Dim blnEdit As Boolean = True
        If Not IsNothing(EffectiveDate) Then
            If (EffectiveDate.Value <= Date.UtcNow) Then
                Return False
            End If
        End If
        Return blnEdit
    End Function
    Public Function OverlappingExists(listToCheck As List(Of DepreciationScdRelation)) As Boolean
        Dim blnDup As Boolean = False

        If listToCheck.Exists(Function(r) (r.Id <> Id) _
                    AndAlso (r.DepreciationScheduleId.Equals(DepreciationScheduleId)) _
                    AndAlso (r.DepreciationScheduleUsageXcd.Equals(DepreciationScheduleUsageXcd)) _
                    AndAlso ((EffectiveDate.Value >= r.EffectiveDate.Value And EffectiveDate.Value <= r.ExpirationDate.Value) _
                              OrElse (ExpirationDate.Value >= r.EffectiveDate.Value And ExpirationDate.Value <= r.ExpirationDate.Value)
                            )) Then
            blnDup = True
        End If
        Return blnDup
    End Function
#Region "Custom Validation"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class OverlapExists
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, "DEPRECIATION_SCHEDULE_OVERLAPPING")
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As DepreciationScdRelation = CType(objectToValidate, DepreciationScdRelation)
            Dim mylist As List(Of DepreciationScdRelation) = GetDepreciationScheduleList(obj.TableReferenceId)
            Return (Not obj.OverlappingExists(mylist))
        End Function
    End Class
#End Region

    Public Class ProductDepreciationScdList
        Inherits BusinessObjectListBase

        Public Sub New(parent As ProductCode)
            MyBase.New(LoadTable(parent), GetType(DepreciationScdRelation), parent)
        End Sub

        Public Overrides Function Belong(bo As BusinessObjectBase) As Boolean
            Return CType(bo, DepreciationScdRelation).TableReferenceId.Equals(CType(Parent, ProductCode).Id)
        End Function

        Public Function Find(productCodeId As Guid) As DepreciationScdRelation
            Dim bo As DepreciationScdRelation
            For Each bo In Me
                If bo.TableReferenceId.Equals(productCodeId) Then Return bo
            Next
            Return Nothing
        End Function

        Public Sub Delete(depreciationScdRelationId As Guid)

            Dim dr As DataRow

            dr = FindRow(depreciationScdRelationId, DepreciationScdRelationDal.ColNameDepreciationScdRelationId, Parent.Dataset.Tables(DepreciationScdRelationDal.TableName))
            If Not (dr Is Nothing) Then
                Parent.Dataset.Tables(DepreciationScdRelationDal.TableName).Rows.Remove(dr)
            End If

        End Sub

        Private Shared Function LoadTable(parent As ProductCode) As DataTable
            Try
                If Not parent.IsChildrenCollectionLoaded(GetType(ProductDepreciationScdList)) Then
                    Dim dal As New DepreciationScdRelationDal
                    dal.LoadList(parent.Id, parent.Dataset)
                    parent.AddChildrenCollection(GetType(ProductDepreciationScdList))
                End If
                Return parent.Dataset.Tables(DepreciationScdRelationDal.TableName)
            Catch ex As DataBaseAccessException
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End Function

    End Class

    Public Class CoverageDepreciationScdList
        Inherits BusinessObjectListBase

        Public Sub New(parent As Coverage)
            MyBase.New(LoadTable(parent), GetType(DepreciationScdRelation), parent)
        End Sub

        Public Overrides Function Belong(bo As BusinessObjectBase) As Boolean
            Return CType(bo, DepreciationScdRelation).TableReferenceId.Equals(CType(Parent, Coverage).Id)
        End Function

        Public Function Find(coverageCode As Guid) As DepreciationScdRelation
            Dim bo As DepreciationScdRelation
            For Each bo In Me
                If bo.TableReferenceId.Equals(coverageCode) Then Return bo
            Next
            Return Nothing
        End Function

        Public Sub Delete(depreciationScdRelationId As Guid)

            Dim dr As DataRow

            dr = FindRow(depreciationScdRelationId, DepreciationScdRelationDal.ColNameDepreciationScdRelationId, Parent.Dataset.Tables(DepreciationScdRelationDal.TableName))
            If Not (dr Is Nothing) Then
                Parent.Dataset.Tables(DepreciationScdRelationDal.TableName).Rows.Remove(dr)
            End If

        End Sub

        Private Shared Function LoadTable(parent As Coverage) As DataTable
            Try
                If Not parent.IsChildrenCollectionLoaded(GetType(CoverageDepreciationScdList)) Then
                    Dim dal As New DepreciationScdRelationDal
                    dal.LoadList(parent.Id, parent.Dataset)
                    parent.AddChildrenCollection(GetType(CoverageDepreciationScdList))
                End If
                Return parent.Dataset.Tables(DepreciationScdRelationDal.TableName)
            Catch ex As DataBaseAccessException
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End Function

    End Class
End Class


