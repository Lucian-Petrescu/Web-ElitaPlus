'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (2/26/2018)  ********************

Public Class CommPlanExtract
    Inherits BusinessObjectBase

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
            Dim dal As New CommPlanExtractDAL
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
            Dim dal As New CommPlanExtractDAL
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
            If Row(CommPlanExtractDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CommPlanExtractDAL.COL_NAME_COMM_PLAN_EXTRACT_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")>
    Public Property CommissionPlanId As Guid
        Get
            CheckDeleted()
            If Row(CommPlanExtractDAL.COL_NAME_COMMISSION_PLAN_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CommPlanExtractDAL.COL_NAME_COMMISSION_PLAN_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommPlanExtractDAL.COL_NAME_COMMISSION_PLAN_ID, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property CommExtractPackageId As Guid
        Get
            CheckDeleted()
            If Row(CommPlanExtractDAL.COL_NAME_COMM_EXTRACT_PACKAGE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CommPlanExtractDAL.COL_NAME_COMM_EXTRACT_PACKAGE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommPlanExtractDAL.COL_NAME_COMM_EXTRACT_PACKAGE_ID, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property SequenceNumber As LongType
        Get
            CheckDeleted()
            If Row(CommPlanExtractDAL.COL_NAME_SEQUENCE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(CommPlanExtractDAL.COL_NAME_SEQUENCE_NUMBER), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommPlanExtractDAL.COL_NAME_SEQUENCE_NUMBER, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property EffectiveDate As DateType
        Get
            CheckDeleted()
            If Row(CommPlanExtractDAL.COL_NAME_EFFECTIVE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(CommPlanExtractDAL.COL_NAME_EFFECTIVE_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommPlanExtractDAL.COL_NAME_EFFECTIVE_DATE, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property ExpirationDate As DateType
        Get
            CheckDeleted()
            If Row(CommPlanExtractDAL.COL_NAME_EXPIRATION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(CommPlanExtractDAL.COL_NAME_EXPIRATION_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommPlanExtractDAL.COL_NAME_EXPIRATION_DATE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=100)>
    Public Property CommTitleXcd As String
        Get
            CheckDeleted()
            If Row(CommPlanExtractDAL.COL_NAME_COMM_TITLE_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CommPlanExtractDAL.COL_NAME_COMM_TITLE_XCD), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommPlanExtractDAL.COL_NAME_COMM_TITLE_XCD, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)>
    Public Property CycleFrequencyXcd As String
        Get
            CheckDeleted()
            If Row(CommPlanExtractDAL.COL_NAME_CYCLE_FREQUENCY_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CommPlanExtractDAL.COL_NAME_CYCLE_FREQUENCY_XCD), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommPlanExtractDAL.COL_NAME_CYCLE_FREQUENCY_XCD, Value)
        End Set
    End Property



    Public Property CycleCutOffDay As LongType
        Get
            CheckDeleted()
            If Row(CommPlanExtractDAL.COL_NAME_CYCLE_CUT_OFF_DAY) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(CommPlanExtractDAL.COL_NAME_CYCLE_CUT_OFF_DAY), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommPlanExtractDAL.COL_NAME_CYCLE_CUT_OFF_DAY, Value)
        End Set
    End Property



    Public Property CycleRunDay As LongType
        Get
            CheckDeleted()
            If Row(CommPlanExtractDAL.COL_NAME_CYCLE_RUN_DAY) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(CommPlanExtractDAL.COL_NAME_CYCLE_RUN_DAY), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommPlanExtractDAL.COL_NAME_CYCLE_RUN_DAY, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=50)>
    Public Property AmountSourceXcd As String
        Get
            CheckDeleted()
            If Row(CommPlanExtractDAL.COL_NAME_AMOUNT_SOURCE_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CommPlanExtractDAL.COL_NAME_AMOUNT_SOURCE_XCD), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommPlanExtractDAL.COL_NAME_AMOUNT_SOURCE_XCD, Value)
        End Set
    End Property


    Public Property CommissionPercentage As LongType
        Get
            CheckDeleted()
            If Row(CommPlanExtractDAL.COL_NAME_COMMISSION_PERCENTAGE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(CommPlanExtractDAL.COL_NAME_COMMISSION_PERCENTAGE), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommPlanExtractDAL.COL_NAME_COMMISSION_PERCENTAGE, Value)
        End Set
    End Property


    Public Property CommissionAmount As LongType
        Get
            CheckDeleted()
            If Row(CommPlanExtractDAL.COL_NAME_COMMISSION_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(CommPlanExtractDAL.COL_NAME_COMMISSION_AMOUNT), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommPlanExtractDAL.COL_NAME_COMMISSION_AMOUNT, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)>
    Public Property CycleCutOffSourceXcd As String
        Get
            CheckDeleted()
            If Row(CommPlanExtractDAL.COL_NAME_CYCLE_CUT_OFF_SOURCE_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CommPlanExtractDAL.COL_NAME_CYCLE_CUT_OFF_SOURCE_XCD), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommPlanExtractDAL.COL_NAME_CYCLE_CUT_OFF_SOURCE_XCD, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=50), ValidUniqueCode("")>
    Public Property Code As String
        Get
            CheckDeleted()
            If Row(CommPlanExtractDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CommPlanExtractDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommPlanExtractDAL.COL_NAME_CODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=200), ValueMandatory("")>
    Public Property Description As String
        Get
            CheckDeleted()
            If Row(CommPlanExtractDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CommPlanExtractDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommPlanExtractDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=50)>
    Public Property CommAtRateXcd As String
        Get
            CheckDeleted()
            If Row(CommPlanExtractDAL.COL_NAME_COMM_AT_RATE_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CommPlanExtractDAL.COL_NAME_COMM_AT_RATE_XCD), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CommPlanExtractDAL.COL_NAME_COMM_AT_RATE_XCD, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New CommPlanExtractDAL
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

    Public Overrides ReadOnly Property IsDirty As Boolean
        Get
            Return MyBase.IsDirty
        End Get
    End Property

    Public Sub DeleteAndSave()
        CheckDeleted()
        BeginEdit()
        Try
            Delete()
            Save()
        Catch ex As Exception
            cancelEdit()
            Throw ex
        End Try
    End Sub

    Public Sub Copy(original As CommPlanExtract)
        If Not IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Commission Plan Extract")
        End If
        'Copy myself
        CopyFrom(original)
    End Sub

#End Region

#Region "DataView Retrieveing Methods"

#Region "CommPlanExtractSearchDV"
    Public Class CommPlanExtractSearchDV
        Inherits DataView

#Region "Constants"

#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region

#Region "Custom Validation"
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class ValidUniqueCode
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, "DUPLICATE_CODE")
            'Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_EFFEC_DATE_ERR)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As CommPlanExtract = CType(objectToValidate, CommPlanExtract)
            Dim dal As New CommPlanExtractDAL

            Dim codeCountForOtherDealers = dal.IsCodeInUse(obj.CommissionPlanId, obj.Code)

            Return codeCountForOtherDealers = 0
        End Function
    End Class
#End Region
    Public Shared Function getList(CommissionPlanId As Guid) As CommPlanExtractSearchDV
        Try
            Dim dal As New CommPlanExtractDAL
            Return New CommPlanExtractSearchDV(dal.LoadList(CommissionPlanId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#End Region

End Class


