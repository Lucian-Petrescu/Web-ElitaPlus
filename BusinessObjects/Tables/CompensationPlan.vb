'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (11/18/2004)  ********************
Imports System.Globalization

Public Class CompensationPlan
    Inherits BusinessObjectBase
    Implements IValidateIntervalDate

#Region "Constants"

    Private Const COMPENSAFORM_FORM001 As String = "COMPENSATIONPLAN_FORM001" ' There is not a current contract for this dealer

#End Region

#Region "Variables"

    Private moRowExpiration As DataRow
    Private moRowCode As DataRow

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

    Protected Sub Load()
        Try
            Dim dal As New CompensationPlanDAL
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
            Dim dal As New CompensationPlanDAL
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

    Private Function CheckDuplicateCompensationPlanCode() As Boolean
        Dim dal As New CompensationPlanDAL
        Dim companyIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
        Return dal.CodeExistsForOtherDealers(DealerId, Code)
    End Function


#End Region

#Region "CONSTANTS"

    Private Const CODE_REQUIRED As String = "CODE_REQUIRED"
    Private Const DESCRIPTION_REQUIRED As String = "DESCRIPTION_REQUIRED"

#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If Row(CompensationPlanDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CompensationPlanDAL.COL_NAME_COMMISSION_PLAN_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")>
    Public Property DealerId() As Guid
        Get
            CheckDeleted()
            If Row(CompensationPlanDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CompensationPlanDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(CompensationPlanDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=100), CheckDuplicate("")> 
    Public Property Code() As String
        Get
            CheckDeleted()
            If Row(CompensationPlanDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(CompensationPlanDAL.COL_NAME_CODE), String))
            End If
        End Get

        Set(ByVal value As String)
            CheckDeleted()
            SetValue(CompensationPlanDAL.COL_NAME_CODE, value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=100)>
    Public Property Description() As String
        Get
            CheckDeleted()
            If Row(CompensationPlanDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(CompensationPlanDAL.COL_NAME_DESCRIPTION), String))
            End If
        End Get

        Set(ByVal value As String)
            CheckDeleted()
            SetValue(CompensationPlanDAL.COL_NAME_DESCRIPTION, value)
        End Set
    End Property


    <ValueMandatory(""), ValidIntervalDate("", Common.ErrorCodes.INVALID_EFFECTIVE_BIGGER_EXPIRATION_ERR,
                        Common.ErrorCodes.INVALID_EFFECTIVE_SMALLER_MAX_EXPIRATION_ERR1,
                        Common.ErrorCodes.INVALID_DELETE_SMALLER_MAXEXPIRATION_ERR)>
    Public Property EffectiveDate() As DateType
        Get
            CheckDeleted()
            If Row(CompensationPlanDAL.COL_NAME_EFFECTIVE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(CompensationPlanDAL.COL_NAME_EFFECTIVE_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            SetValue(CompensationPlanDAL.COL_NAME_EFFECTIVE_DATE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidIntervalDate("", Common.ErrorCodes.INVALID_EFFECTIVE_BIGGER_EXPIRATION_ERR,
                        Common.ErrorCodes.INVALID_EFFECTIVE_SMALLER_MAX_EXPIRATION_ERR1,
                        Common.ErrorCodes.INVALID_DELETE_SMALLER_MAXEXPIRATION_ERR)>
    Public Property ExpirationDate() As DateType
        Get
            CheckDeleted()
            If Row(CompensationPlanDAL.COL_NAME_EXPIRATION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(CompensationPlanDAL.COL_NAME_EXPIRATION_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            SetValue(CompensationPlanDAL.COL_NAME_EXPIRATION_DATE, Value)
        End Set
    End Property

    Public Overrides ReadOnly Property IsDirty() As Boolean
        Get
            Return MyBase.IsDirty OrElse IsChildrenDirty 'OrElse IsFamilyDirty
        End Get
    End Property

#Region "Properties-Expiration"

    Public ReadOnly Property MaxExpiration(ByVal oData As Object) As Date
        Get
            Dim ds As DataSet
            Dim oExpiration As Date

            '    If moRowExpiration Is Nothing Then
            Dim oCompensationPlanData As CompensationPlanData = CType(oData, CompensationPlanData)
            Dim dal As New CompensationPlanDAL
            ds = dal.LoadExpiration(oCompensationPlanData)
            If ds.Tables(CompensationPlanDAL.TABLE_NAME).Rows.Count = 0 Then
                ' oExpiration = CType(GenericConstants.INFINITE_DATE, Date)
                oExpiration = GenericConstants.INFINITE_DATE
            Else
                moRowExpiration = ds.Tables(CompensationPlanDAL.TABLE_NAME).Rows(0)
                oExpiration = CType(moRowExpiration(CompensationPlanDAL.COL_NAME_MAX_EXPIRATION), Date)
            End If

            Return oExpiration
        End Get
    End Property

    Public ReadOnly Property ExpirationCount(ByVal oData As Object) As Integer
        Get
            Dim ds As DataSet
            Dim nExpiration As Integer

            '   If moRowExpiration Is Nothing Then
            Dim oCommissionPlanData As CompensationPlanData = CType(oData, CompensationPlanData)
            Dim dal As New CompensationPlanDAL
            ds = dal.LoadExpiration(oCommissionPlanData)
            If ds.Tables(CompensationPlanDAL.TABLE_NAME).Rows.Count = 0 Then
                nExpiration = 0
            Else
                moRowExpiration = ds.Tables(CompensationPlanDAL.TABLE_NAME).Rows(0)
                nExpiration = CType(moRowExpiration(CompensationPlanDAL.COL_NAME_EXPIRATION_COUNT), Integer)
            End If

            Return nExpiration
        End Get
    End Property

#End Region

#End Region

#Region "Public Members"
    Public Sub Copy(ByVal original As CompensationPlan)
        If Not IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Dealer")
        End If
        'Copy myself
        CopyFrom(original)
        'copy the children 
    End Sub

    Public Overrides Sub Save()
        Try
            MyBase.Save()
            'If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
            If _isDSCreator AndAlso IsFamilyDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New CompensationPlanDAL
                dal.update(Dataset)
                If Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Id
                    Dataset = New DataSet
                    Row = Nothing
                    Load(objId)
                    'Me._address = Nothing
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            'Me._address = Nothing
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

#End Region

#Region "DataView Retrieveing Methods"

    Public Shared Function getList(ByVal oCompensationPlanData As CompensationPlanData) As CompensationPlanSearchDV
        Try
            Dim dal As New CompensationPlanDAL
            Return New CompensationPlanSearchDV(dal.LoadList(oCompensationPlanData).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function


#End Region

#Region "Validation"

    ReadOnly Property IEffective() As DateType Implements IValidateIntervalDate.IEffective
        Get
            Return EffectiveDate
        End Get
    End Property

    ReadOnly Property ICode() As String
        Get
            Return Code
        End Get

    End Property
    ReadOnly Property IDescription() As String
        Get
            Return Description
        End Get

    End Property
    ReadOnly Property IExpiration() As DateType Implements IValidateIntervalDate.IExpiration
        Get
            Return ExpirationDate
        End Get
    End Property

    ReadOnly Property IMaxExpiration() As DateType Implements IValidateIntervalDate.IMaxExpiration
        Get
            Dim oCompensationPlanData As New CompensationPlanData
            With oCompensationPlanData
                .dealerId = DealerId
            End With
            Return New DateType(MaxExpiration(oCompensationPlanData))
        End Get
    End Property

    Public ReadOnly Property IIsNew() As Boolean Implements IValidateIntervalDate.IIsNew
        Get
            Return IsNew
        End Get
    End Property

    ReadOnly Property IIsDeleted() As Boolean Implements IValidateIntervalDate.IIsDeleted
        Get
            Return IsDeleted
        End Get
    End Property


#End Region

#Region "CompensationPlanSearchDV"
    Public Class CompensationPlanSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_COMMISSION_PLAN_ID As String = CompensationPlanDAL.COL_NAME_COMMISSION_PLAN_ID
        Public Const COL_COMPANY_CODE As String = CompensationPlanDAL.COL_NAME_COMPANY_CODE
        Public Const COL_DEALER_NAME As String = CompensationPlanDAL.COL_NAME_DEALER_NAME
        Public Const COL_CODE As String = CompensationPlanDAL.COL_NAME_CODE
        Public Const COL_EFFECTIVE_DATE As String = CompensationPlanDAL.COL_NAME_EFFECTIVE
        Public Const COL_EXPIRATION_DATE As String = CompensationPlanDAL.COL_NAME_EXPIRATION

#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region
#Region "Custom Validation"
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class CheckDuplicate
        Inherits ValidBaseAttribute
        Private Const DUPLICATE_CODE As String = "DUPLICATE_COMMISSION_CODE"

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, DUPLICATE_CODE)
        End Sub

        Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal objectToValidate As Object) As Boolean

            Dim obj As CompensationPlan = CType(objectToValidate, CompensationPlan)
            Dim dal As New CompensationPlanDAL
            Dim companyIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies

            Dim codeCountForOtherDealers = dal.CodeExistsForOtherDealers(obj.DealerId, obj.Code)

            Return codeCountForOtherDealers = 0

        End Function
    End Class
#End Region

End Class



