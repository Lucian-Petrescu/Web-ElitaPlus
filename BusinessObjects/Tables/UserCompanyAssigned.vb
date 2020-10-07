'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (8/9/2010)  ********************

Public Class UserCompanyAssigned
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const COL_USER_ID As String = UserDAL.COL_NAME_USER_ID
    Public Const COL_COMPANY_ID As String = UserDAL.COL_NAME_COMPANY_ID
    Public Const COL_COMPANY_CODE As String = UserDAL.COL_NAME_COMPANY_CODE
    Public Const COL_DESCRIPTION As String = UserDAL.COL_NAME_DESCRIPTION
    Public Const COL_AUTHORIZATION_LIMIT As String = UserCompanyAssignedDAL.COL_NAME_AUTHORIZATION_LIMIT
    Public Const COL_PAYMENT_LIMIT As String = UserCompanyAssignedDAL.COL_NAME_PAYMENT_LIMIT
    Public Const COL_LIABILITY_OVERRIDE_LIMIT As String = UserCompanyAssignedDAL.COL_NAME_LIABILITY_OVERRIDE_LIMIT
    Public Const COL_IS_LOADED As String = UserCompanyAssignedDAL.COL_NAME_IS_LOADED
#End Region

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

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As DataSet, ByVal oUserId As Guid, ByVal oCompanyId As Guid)
        MyBase.New(False)
        Dataset = familyDS
        LoadByUserIdCompanyId(oUserId, oCompanyId)
    End Sub

    Protected Sub LoadByUserIdCompanyId(ByVal oUserId As Guid, ByVal oCompanyId As Guid)
        Try
            Dim dal As New UserCompanyAssignedDAL

            If _isDSCreator Then
                If Not Row Is Nothing Then
                    Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Row)
                End If
            End If
            Row = Nothing
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Row = FindRow(oUserId, dal.COL_NAME_USER_ID, oCompanyId, dal.COL_NAME_COMPANY_ID, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.LoadByUserIdCompanyID(Dataset, oUserId, oCompanyId)
                Row = FindRow(oUserId, dal.COL_NAME_USER_ID, oCompanyId, dal.COL_NAME_COMPANY_ID, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then
                Throw New DataNotFoundException
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New UserCompanyAssignedDAL
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
            Dim dal As New UserCompanyAssignedDAL
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
    Public ReadOnly Property Id() As Guid
        Get
            If row(UserCompanyAssignedDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(UserCompanyAssignedDAL.COL_NAME_USER_COMPANY_ASSIGNED_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property UserId() As Guid
        Get
            CheckDeleted()
            If row(UserCompanyAssignedDAL.COL_NAME_USER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(UserCompanyAssignedDAL.COL_NAME_USER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(UserCompanyAssignedDAL.COL_NAME_USER_ID, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidNumericRange("", MIN:=0, Max:=Decimal.MaxValue, Message:=Common.ErrorCodes.INVALID_AUTHORIZED_AMOUNT_ERR)> _
    Public Property PaymentLimit() As DecimalType
        Get
            CheckDeleted()
            If Row(UserCompanyAssignedDAL.COL_NAME_PAYMENT_LIMIT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(UserCompanyAssignedDAL.COL_NAME_PAYMENT_LIMIT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            SetValue(UserCompanyAssignedDAL.COL_NAME_PAYMENT_LIMIT, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidNumericRange("", MIN:=0, Max:=Decimal.MaxValue, Message:=Common.ErrorCodes.INVALID_PAYMENT_AMOUNT_ERR)> _
    Public Property AuthorizationLimit() As DecimalType
        Get
            CheckDeleted()
            If Row(UserCompanyAssignedDAL.COL_NAME_AUTHORIZATION_LIMIT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(UserCompanyAssignedDAL.COL_NAME_AUTHORIZATION_LIMIT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            SetValue(UserCompanyAssignedDAL.COL_NAME_AUTHORIZATION_LIMIT, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidNumericRange("", MIN:=0, Max:=Decimal.MaxValue, Message:=Common.ErrorCodes.INVALID_LIABILITY_OVERRIDE_AMOUNT_ERR)> _
    Public Property LiabilityOverrideLimit() As DecimalType
        Get
            CheckDeleted()
            If Row(UserCompanyAssignedDAL.COL_NAME_LIABILITY_OVERRIDE_LIMIT) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(UserCompanyAssignedDAL.COL_NAME_LIABILITY_OVERRIDE_LIMIT), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            SetValue(UserCompanyAssignedDAL.COL_NAME_LIABILITY_OVERRIDE_LIMIT, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property CompanyId() As Guid
        Get
            CheckDeleted()
            If row(UserCompanyAssignedDAL.COL_NAME_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(UserCompanyAssignedDAL.COL_NAME_COMPANY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(UserCompanyAssignedDAL.COL_NAME_COMPANY_ID, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New UserCompanyAssignedDAL
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

    Public Sub InitTable()
        Dataset.Tables(UserCompanyAssignedDAL.TABLE_NAME).Rows.Clear()
    End Sub
#End Region

#Region "DataView Retrieveing Methods"

#End Region

    Public Class UserCompanyAssignedDV
        Inherits DataView

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property UserId(ByVal row As DataRow) As String
            Get
                Return row(COL_USER_ID).ToString
            End Get
        End Property

        Public Shared ReadOnly Property CompanyId(ByVal row As DataRow) As String
            Get
                Return row(COL_COMPANY_ID).ToString
            End Get
        End Property

        Public Shared ReadOnly Property CompanyCode(ByVal row As DataRow) As String
            Get
                Return row(COL_COMPANY_CODE).ToString
            End Get
        End Property

        Public Shared ReadOnly Property Description(ByVal row As DataRow) As String
            Get
                Return row(COL_DESCRIPTION).ToString
            End Get
        End Property

        Public Shared ReadOnly Property AuthorizationLimit(ByVal row As DataRow) As Decimal
            Get
                Return row(COL_AUTHORIZATION_LIMIT).ToString
            End Get
        End Property

        Public Shared ReadOnly Property PaymentLimit(ByVal row As DataRow) As Decimal
            Get
                Return row(COL_PAYMENT_LIMIT).ToString
            End Get
        End Property

        Public Shared ReadOnly Property LiabilityOverrideLimit(ByVal row As DataRow) As Decimal
            Get
                Return row(COL_LIABILITY_OVERRIDE_LIMIT).ToString
            End Get
        End Property

        Public Shared ReadOnly Property IsLoaded(ByVal row As DataRow) As Boolean
            Get
                Return If(row(COL_IS_LOADED).ToString = "Y", True, False)
            End Get
        End Property

    End Class
End Class


