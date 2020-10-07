'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (10/1/2017)  ********************

Public Class DepreciationScd
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
    Public Sub New(ByVal id As Guid, ByVal familyDs As DataSet)
        MyBase.New(False)
        Dataset = familyDs
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDs As DataSet)
        MyBase.New(False)
        Dataset = familyDs
        Load()
    End Sub

    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try

            If Dataset.Tables.IndexOf(DepreciationScdDal.TableName) < 0 Then
                Dim dal As New DepreciationScdDal
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(DepreciationScdDal.TableName).NewRow
            Dataset.Tables(DepreciationScdDal.TableName).Rows.Add(newRow)
            Row = newRow
            SetValue(DepreciationScdDal.TableKeyName, Guid.NewGuid)
            Initialize()
        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try

            If _isDSCreator Then
                If Not Row Is Nothing Then
                    Dataset.Tables(DepreciationScdDal.TableName).Rows.Remove(Row)
                End If
            End If
            Row = Nothing
            If Dataset.Tables.IndexOf(DepreciationScdDal.TableName) >= 0 Then
                Row = FindRow(id, DepreciationScdDal.TableKeyName, Dataset.Tables(DepreciationScdDal.TableName))
            End If
            If Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                Dim dal As New DepreciationScdDal
                dal.Load(Dataset, id)
                Row = FindRow(id, DepreciationScdDal.TableKeyName, Dataset.Tables(DepreciationScdDal.TableName))
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
#Region "Constant"
    Public Const DepreciationScheduleCode001 As String = "DEPRECIATION_SCHEDULE_CODE_001" ' Duplicate Depreciation Schedule Code per company
    Public Const ColDepreciationScheduleCode As String = DepreciationScdDal.ColNameCode
    Public Const ColDepreciationScheduleDescription As String = DepreciationScdDal.ColNameDescription
    Public Const ColDepreciationScheduleActive As String = DepreciationScdDal.ColNameActive
    Public Const ColDepreciationScheduleId As String = DepreciationScdDal.ColNameDepreciationScheduleId
#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid
        Get
            If Row(DepreciationScdDal.TableKeyName) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DepreciationScdDal.ColNameDepreciationScheduleId), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")>
    Public Property CompanyId As Guid
        Get
            CheckDeleted()
            If Row(DepreciationScdDal.ColNameCompanyId) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DepreciationScdDal.ColNameCompanyId), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DepreciationScdDal.ColNameCompanyId, value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=1020), ValidateDepreciationCode("")>
    Public Property Code As String
        Get
            CheckDeleted()
            If Row(DepreciationScdDal.ColNameCode) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DepreciationScdDal.ColNameCode), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DepreciationScdDal.ColNameCode, value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=1020)>
    Public Property Description As String
        Get
            CheckDeleted()
            If Row(DepreciationScdDal.ColNameDescription) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DepreciationScdDal.ColNameDescription), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DepreciationScdDal.ColNameDescription, value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=40)>
    Public Property ActiveXcd As String
        Get
            CheckDeleted()
            If Row(DepreciationScdDal.ColNameActiveXcd) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DepreciationScdDal.ColNameActiveXcd), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DepreciationScdDal.ColNameActiveXcd, value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New DepreciationScdDal
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
    Shared Function LoadList(ByVal companyId As Guid) As DataView
        Try
            Dim dal As New DepreciationScdDal
            Dim ds As DataSet

            ds = dal.LoadList(companyId, Authentication.LangId)
            Return (ds.Tables(DepreciationScdDal.TableName).DefaultView)
        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function CheckIfDepreciationScheduleCodeAlreadyExists(companyId As Guid, ByVal code As String) As Boolean
        Try
            Dim result As Boolean = False
            Dim dv As DataView
            dv = LoadList(companyId)
            dv.Sort = ColDepreciationScheduleCode
            Dim idx As Integer = dv.Find(code)
            If (idx >= 0) Then
                result = True
            End If
            Return result
        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

#Region "Custom Validation"
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidateDepreciationCode
        Inherits ValidBaseAttribute
        Implements IValidatorAttribute
        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, DepreciationScheduleCode001)
        End Sub
        Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal objectToValidate As Object) As Boolean
            If objectToCheck Is Nothing Then Return True
            Dim obj As DepreciationScd = CType(objectToValidate, DepreciationScd)
            If obj.IsNew AndAlso CheckIfDepreciationScheduleCodeAlreadyExists(obj.CompanyId, obj.Code) Then
                Return False
            End If
            Return True      'if nothing came as result then validation passes also if not a new record then this validation should be skipped
        End Function
    End Class
#End Region
End Class


