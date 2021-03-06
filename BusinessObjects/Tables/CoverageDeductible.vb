﻿Public Class CoverageDeductible
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const COL_NAME_COVERAGE_DED_ID As String = CoverageDeductibleDAL.COL_NAME_COVERAGE_DED_ID
    Public Const COL_NAME_COVERAGE_ID As String = CoverageDeductibleDAL.COL_NAME_COVERAGE_ID
    Public Const COL_NAME_METHOD_OF_REPAIR_ID As String = CoverageDeductibleDAL.COL_NAME_METHOD_OF_REPAIR_ID
    Public Const COL_NAME_DEDUCTIBLE_BASED_ON_ID As String = CoverageDeductibleDAL.COL_NAME_DEDUCTIBLE_BASED_ON_ID
    Public Const COL_NAME_DEDUCTIBLE As String = CoverageDeductibleDAL.COL_NAME_DEDUCTIBLE
    Public Const COL_NAME_METHOD_OF_REPAIR As String = CoverageDeductibleDAL.COL_NAME_METHOD_OF_REPAIR
    Public Const COL_NAME_DEDUCTIBLE_BASED_ON As String = CoverageDeductibleDAL.COL_NAME_DEDUCTIBLE_BASED_ON
    Public Const COL_NAME_DEDUCTIBLE_EXPRESSION_ID As String = CoverageDeductibleDAL.COL_NAME_DEDUCTIBLE_EXPRESSION_ID

#End Region

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
            Dim dal As New CoverageDeductibleDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New CoverageDeductibleDAL
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
    <ValueMandatory("")> _
    Public ReadOnly Property Id() As Guid
        Get
            CheckDeleted()
            If Row(CoverageDeductibleDAL.COL_NAME_COVERAGE_DED_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CoverageDeductibleDAL.COL_NAME_COVERAGE_DED_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CoverageId() As Guid
        Get
            CheckDeleted()
            If Row(CoverageDeductibleDAL.COL_NAME_COVERAGE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CoverageDeductibleDAL.COL_NAME_COVERAGE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CoverageDeductibleDAL.COL_NAME_COVERAGE_ID, Value)
        End Set
    End Property

    <ValueMandatory(""), MethodOfRepairValidator("")> _
    Public Property MethodOfRepairId() As Guid
        Get
            CheckDeleted()
            If Row(CoverageDeductibleDAL.COL_NAME_METHOD_OF_REPAIR_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CoverageDeductibleDAL.COL_NAME_METHOD_OF_REPAIR_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CoverageDeductibleDAL.COL_NAME_METHOD_OF_REPAIR_ID, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property DeductibleBasedOnId() As Guid
        Get
            CheckDeleted()
            If Row(CoverageDeductibleDAL.COL_NAME_DEDUCTIBLE_BASED_ON_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CoverageDeductibleDAL.COL_NAME_DEDUCTIBLE_BASED_ON_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CoverageDeductibleDAL.COL_NAME_DEDUCTIBLE_BASED_ON_ID, Value)
        End Set
    End Property

    Public Property DeductibleExpressionId() As Guid
        Get
            CheckDeleted()
            If Row(CoverageDeductibleDAL.COL_NAME_DEDUCTIBLE_EXPRESSION_ID) Is DBNull.Value Then
                Return Guid.Empty
            Else
                Return New Guid(CType(Row(CoverageDeductibleDAL.COL_NAME_DEDUCTIBLE_EXPRESSION_ID), Byte()))
            End If
        End Get
        Set(ByVal value As Guid)
            CheckDeleted()
            Me.SetValue(CoverageDeductibleDAL.COL_NAME_DEDUCTIBLE_EXPRESSION_ID, value)
        End Set
    End Property

    ''TODO- Add Custom Validation, The value should not exceed 100 if percentage and ok to exceed otherwise
    <ValueMandatory(""), ValidNumericRange("", Min:=0, Max:=Decimal.MaxValue), DeductibleAmount("")>
    Public Property Deductible() As DecimalType
        Get
            CheckDeleted()
            If Row(CoverageDeductibleDAL.COL_NAME_DEDUCTIBLE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CoverageDeductibleDAL.COL_NAME_DEDUCTIBLE), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CoverageDeductibleDAL.COL_NAME_DEDUCTIBLE, Value)
        End Set
    End Property
#End Region

#Region "Public Members"
  
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New CoverageDeductibleDAL
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

    Public Sub Copy(ByVal original As CoverageDeductible)
        If Not Me.IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Best Replacement.")
        End If
        MyBase.CopyFrom(original)
    End Sub
#End Region

#Region "Custom Validation"
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class DeductibleAmount
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_DEDUCTIBLE_VALUE)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As CoverageDeductible = CType(objectToValidate, CoverageDeductible)
            If valueToCheck Is Nothing Then Return False
            If obj.Deductible = 0 Then Return True
            If (LookupListNew.GetCodeFromId(LookupListNew.LK_DEDUCTIBLE_BASED_ON, obj.DeductibleBasedOnId) <> "FIXED") Then
                If (valueToCheck.Value > 100) Then
                    Return False
                End If
            End If
            Return True
        End Function
    End Class
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class MethodOfRepairValidator
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.DUPLICATE_METHOD_OF_REPAIR)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As CoverageDeductible = CType(objectToValidate, CoverageDeductible)
            Dim dal As New CoverageDeductibleDAL
            Dim languageid As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim dtvw As DataView = LookupListNew.GetMethodOfRepairLookupList(languageid)
            Dim strmethodofrepair As String = LookupListNew.GetDescriptionFromId(dtvw, valueToCheck)
            Dim dtView As DataTable = dal.LoadList(obj.CoverageId, languageid).Tables(0)
            For Each dtrow As DataRow In dtView.Rows
                If dtrow("METHOD_OF_REPAIR") = strmethodofrepair And obj.Id <> New Guid(CType(dtrow("COVERAGE_DED_ID"), Byte())) Then
                    Return False
                End If
            Next
            Return True
        End Function
    End Class

#End Region
#Region "DataView Retrieveing Methods"

    Public Shared Function GetList(ByVal CoverageId As Guid, ByVal languageID As Guid) As DataView
        Try
            Dim dal As New CoverageDeductibleDAL
            Return New DataView(dal.LoadList(CoverageId, languageID).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

End Class
