Public Class CertItemCoverageDeductible
    Inherits BusinessObjectBase

#Region "Constants"
    Friend Const INVALID_DEDUCTIBLE As String = "INVALID_DEDUCTIBLE" ' Invalid Deductible.
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
            Dim dal As New CertItemCoverageDeductibleDAL
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

    Protected Sub Load(id As Guid)
        Try
            Dim dal As New CertItemCoverageDeductibleDAL
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
    <ValueMandatory("")> _
    Public ReadOnly Property Id As Guid
        Get
            If Row(CertItemCoverageDeductibleDAL.COL_NAME_CERT_ITEM_CVG_DED_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertItemCoverageDeductibleDAL.COL_NAME_CERT_ITEM_CVG_DED_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CertItemCoverageId As Guid
        Get
            CheckDeleted()
            If Row(CertItemCoverageDeductibleDAL.COL_NAME_CERT_ITEM_COVERAGE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertItemCoverageDeductibleDAL.COL_NAME_CERT_ITEM_COVERAGE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertItemCoverageDeductibleDAL.COL_NAME_CERT_ITEM_COVERAGE_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property MethodOfRepairId As Guid
        Get
            CheckDeleted()
            If Row(CertItemCoverageDeductibleDAL.COL_NAME_METHOD_OF_REPAIR_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertItemCoverageDeductibleDAL.COL_NAME_METHOD_OF_REPAIR_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertItemCoverageDeductibleDAL.COL_NAME_METHOD_OF_REPAIR_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property DeductibleBasedOnId As Guid
        Get
            CheckDeleted()
            If Row(CertItemCoverageDeductibleDAL.COL_NAME_DEDUCTIBLE_BASED_ON_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertItemCoverageDeductibleDAL.COL_NAME_DEDUCTIBLE_BASED_ON_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertItemCoverageDeductibleDAL.COL_NAME_DEDUCTIBLE_BASED_ON_ID, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidNumericRange("", Min:=0), ValidateDeductible("")>
    Public Property Deductible As DecimalType
        Get
            CheckDeleted()
            If Row(CertItemCoverageDeductibleDAL.COL_NAME_DEDUCTIBLE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(CertItemCoverageDeductibleDAL.COL_NAME_DEDUCTIBLE), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertItemCoverageDeductibleDAL.COL_NAME_DEDUCTIBLE, Value)
        End Set
    End Property

    Public Property DeductibleExpressionId As Guid
        Get
            CheckDeleted()
            If Row(CertItemCoverageDeductibleDAL.COL_NAME_DEDUCTIBLE_EXPRESSION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CertItemCoverageDeductibleDAL.COL_NAME_DEDUCTIBLE_EXPRESSION_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertItemCoverageDeductibleDAL.COL_NAME_DEDUCTIBLE_EXPRESSION_ID, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Shared Function GetDeductible(certItemCoverageId As Guid, methodOfRepairId As Guid) As CertItemCoverageDeductible
        Try
            Dim dal As New CertItemCoverageDeductibleDAL
            Dim ds As DataSet
            ds = dal.Load(certItemCoverageId, methodOfRepairId)
            If (ds Is Nothing) Then Return Nothing
            If (ds.Tables.Count = 0) Then Return Nothing
            If (ds.Tables(dal.TABLE_NAME).Rows.Count <> 1) Then Return Nothing
            Return New CertItemCoverageDeductible(ds.Tables(dal.TABLE_NAME).Rows(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    'Public Overrides Sub Save()
    '    Try
    '        MyBase.Save()
    '        If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
    '            Dim dal As New CertItemCoverageDeductibleDAL
    '            dal.Update(Me.Row)
    '            'Reload the Data from the DB
    '            If Me.Row.RowState <> DataRowState.Detached Then
    '                Dim objId As Guid = Me.Id
    '                Me.Dataset = New DataSet
    '                Me.Row = Nothing
    '                Me.Load(objId)
    '            End If
    '        End If
    '    Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
    '        Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
    '    End Try
    'End Sub

    Public Sub Copy(original As CertItemCoverageDeductible)
        If Not IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Certificate Item Coverage Deductible.")
        End If
        MyBase.CopyFrom(original)
    End Sub
#End Region

#Region "Custom Validations"
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidateDeductible
        Inherits ValidBaseAttribute
        Private _fieldDisplayName As String
        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, INVALID_DEDUCTIBLE)
            _fieldDisplayName = fieldDisplayName
        End Sub

        Public Overrides Function IsValid(objectToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As CertItemCoverageDeductible = CType(objectToValidate, CertItemCoverageDeductible)
            If (LookupListNew.GetCodeFromId(LookupListNew.LK_DEDUCTIBLE_BASED_ON, obj.DeductibleBasedOnId) <> "FIXED") Then
                If (obj.Deductible.Value > 100) Then
                    Return False
                End If
            End If
            Return True
        End Function
    End Class
#End Region

End Class
