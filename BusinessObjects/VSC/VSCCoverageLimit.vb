'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (5/23/2007)  ********************

Public Class VSCCoverageLimit
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
            Dim dal As New VSCCoverageLimitDAL
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
            Dim dal As New VSCCoverageLimitDAL
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

#Region "Constants"
  
    Private Const MIN_LONG As Double = 0.0
    
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
            If Row(VSCCoverageLimitDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(VSCCoverageLimitDAL.COL_NAME_VSC_COVERAGE_LIMIT_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidNumericRange("", MIN:=MIN_LONG), ValidateZeros("")> _
    Public Property CoverageKmMi As LongType
        Get
            CheckDeleted()
            If Row(VSCCoverageLimitDAL.COL_NAME_COVERAGE_KM_MI) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(VSCCoverageLimitDAL.COL_NAME_COVERAGE_KM_MI), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCCoverageLimitDAL.COL_NAME_COVERAGE_KM_MI, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidNumericRange("", MIN:=MIN_LONG), ValidateZeros("")> _
    Public Property CoverageMonths As LongType
        Get
            CheckDeleted()
            If Row(VSCCoverageLimitDAL.COL_NAME_COVERAGE_MONTHS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(VSCCoverageLimitDAL.COL_NAME_COVERAGE_MONTHS), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCCoverageLimitDAL.COL_NAME_COVERAGE_MONTHS, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property CoverageTypeId As Guid
        Get
            CheckDeleted()
            If Row(VSCCoverageLimitDAL.COL_NAME_COVERAGE_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(VSCCoverageLimitDAL.COL_NAME_COVERAGE_TYPE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCCoverageLimitDAL.COL_NAME_COVERAGE_TYPE_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property CoverageLimitCode As LongType
        Get
            CheckDeleted()
            If Row(VSCCoverageLimitDAL.COL_NAME_COVERAGE_LIMIT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(VSCCoverageLimitDAL.COL_NAME_COVERAGE_LIMIT_CODE), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCCoverageLimitDAL.COL_NAME_COVERAGE_LIMIT_CODE, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property CompanyGroupId As Guid
        Get
            CheckDeleted()
            If Row(VSCCoverageLimitDAL.COL_NAME_COMPANY_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(VSCCoverageLimitDAL.COL_NAME_COMPANY_GROUP_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCCoverageLimitDAL.COL_NAME_COMPANY_GROUP_ID, Value)
        End Set
    End Property


#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New VSCCoverageLimitDAL
                dal.Update(Row)
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Id
                    Dataset = New Dataset
                    Row = Nothing
                    Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub


    Public Class isCoverageOptional
        Public isOptional As Boolean = False

        Public Sub New(ByVal id As Guid)
            Try
                Dim dal As New VSCCoverageLimitDAL
                Dim covOptDs As DataSet = dal.GetOptionalCoverage(id, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

                If covOptDs.Tables(0).Rows.Count > 0 AndAlso (Not covOptDs.Tables(0).Rows(0)(dal.COL_NAME_COVERAGE_OPT) Is DBNull.Value) Then
                    isOptional = (covOptDs.Tables(0).Rows(0)(dal.COL_NAME_COVERAGE_OPT) > 0)
                End If
            Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
            End Try
        End Sub

    End Class



#End Region

#Region "DataView Retrieveing Methods"
#Region "CoverageLimitSearchDV"
    Public Class CoverageLimitSearchDV
        Inherits DataView

#Region "Constants"

        Public Const COL_NAME_VSC_COVERAGE_LIMIT_ID As String = VSCCoverageLimitDAL.COL_NAME_VSC_COVERAGE_LIMIT_ID
        Public Const COL_NAME_COVERAGE_KM_MI As String = VSCCoverageLimitDAL.COL_NAME_COVERAGE_KM_MI
        Public Const COL_NAME_COVERAGE_MONTHS As String = VSCCoverageLimitDAL.COL_NAME_COVERAGE_MONTHS
        Public Const COL_NAME_COVERAGE_TYPE_ID As String = VSCCoverageLimitDAL.COL_NAME_COVERAGE_TYPE_ID
        Public Const COL_NAME_COVERAGE_LIMIT_CODE As String = VSCCoverageLimitDAL.COL_NAME_COVERAGE_LIMIT_CODE
        Public Const COL_NAME_COVERAGE_TYPE_DESC As String = "description"
        Public Const COL_NAME_VSC_COVERAGE_TYPE_ID As String = VSCCoverageLimitDAL.COL_NAME_COVERAGE_TYPE_ID


#End Region
        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region

    Public Shared Function getList(ByVal LimitCodeMask As String, ByVal CoverageTypeMask As Guid, ByVal MonthMask As String, ByVal KmMask As String) As CoverageLimitSearchDV

        Try
            Dim dal As New VSCCoverageLimitDAL
            Dim LanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId


            Return New CoverageLimitSearchDV(dal.LoadList(LimitCodeMask, CoverageTypeMask, MonthMask, KmMask, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, LanguageId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#End Region


#Region "CustomValidation"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidateZeros
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            'fields can not simultaneously be set at zero
            MyBase.New(fieldDisplayName, Common.ErrorCodes.INVALID_NOT_ZEROS)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As VSCCoverageLimit = CType(objectToValidate, VSCCoverageLimit)
            Dim covopt As New isCoverageOptional(obj.Id)

            If Not covopt.isOptional Then
                If ((Not (obj.CoverageKmMi Is Nothing) AndAlso obj.CoverageKmMi.Value = 0) AndAlso _
                    (Not (obj.CoverageMonths Is Nothing) AndAlso obj.CoverageMonths.Value = 0)) Then
                    Return False
                End If
            End If
            Return True

        End Function

    End Class

#End Region
End Class













