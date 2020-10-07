Imports System.Globalization

Public Class CommPlan
    Inherits BusinessObjectBase
    Implements IValidateIntervalDate

#Region "Constants"

    Private Const COMMISSIONPERIODFORM_FORM001 As String = "COMMISSIONPERIODFORM_FORM001" ' There is not a current contract for this dealer

#End Region

#Region "Variables"

    Private moRowExpiration As DataRow

#End Region

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Dataset = New Dataset
        Load(id)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Dataset = New Dataset
        Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDS As Dataset)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As Dataset)
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
            Dim dal As New CommPlanDAL
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
            Dim dal As New CommPlanDAL
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
            If Row(CommPlanDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CommPlanDAL.COL_NAME_COMM_PLAN_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property DealerId() As Guid
        Get
            CheckDeleted()
            If Row(CommPlanDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CommPlanDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(CommPlanDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property
    
    <ValueMandatory("")> _
    Public Property Code() As String
        Get
            CheckDeleted()
            If Row(CommPlanDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CommPlanDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(CommPlanDAL.COL_NAME_CODE, Value)
        End Set
    End Property
    
    <ValueMandatory("")> _
    Public Property Description() As String
        Get
            CheckDeleted()
            If Row(CommPlanDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CommPlanDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(CommPlanDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidIntervalDate("", Common.ErrorCodes.INVALID_EFFECTIVE_BIGGER_EXPIRATION_ERR, _
                        Common.ErrorCodes.INVALID_EFFECTIVE_SMALLER_MAXEXPIRATION_ERR, _
                        Common.ErrorCodes.INVALID_DELETE_SMALLER_MAXEXPIRATION_ERR)> _
    Public Property EffectiveDate() As DateType
        Get
            CheckDeleted()
            If Row(CommPlanDAL.COL_NAME_EFFECTIVE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(CommPlanDAL.COL_NAME_EFFECTIVE_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            SetValue(CommPlanDAL.COL_NAME_EFFECTIVE_DATE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidIntervalDate("", Common.ErrorCodes.INVALID_EFFECTIVE_BIGGER_EXPIRATION_ERR, _
                        Common.ErrorCodes.INVALID_EFFECTIVE_SMALLER_MAXEXPIRATION_ERR, _
                        Common.ErrorCodes.INVALID_DELETE_SMALLER_MAXEXPIRATION_ERR)> _
    Public Property ExpirationDate() As DateType
        Get
            CheckDeleted()
            If Row(CommPlanDAL.COL_NAME_EXPIRATION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(CommPlanDAL.COL_NAME_EXPIRATION_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            SetValue(CommPlanDAL.COL_NAME_EXPIRATION_DATE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ReferenceSource() As String
        Get
            CheckDeleted()
            If Row(CommPlanDAL.COL_NAME_RFERENCE_SOURCE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(CommPlanDAL.COL_NAME_RFERENCE_SOURCE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(CommPlanDAL.COL_NAME_RFERENCE_SOURCE, Value)
        End Set
    End Property

    Public Function AddCommPlanDistribution(ByVal assDistID As Guid) As CommPlanDistribution
        If assDistID.Equals(Guid.Empty) Then
            Dim objCommDist As New CommPlanDistribution(Dataset)
            Return objCommDist
        Else
            Dim objCommDist As New CommPlanDistribution(assDistID, Dataset)
            Return objCommDist
        End If
    End Function

    Public Overrides ReadOnly Property IsDirty() As Boolean
        Get
            Return MyBase.IsDirty OrElse IsChildrenDirty 'OrElse IsFamilyDirty
        End Get
    End Property

#Region "Properties-Expiration"

    Public ReadOnly Property MaxExpiration(ByVal oData As Object) As Date
        Get
            Dim ds As Dataset
            Dim oExpiration As Date

            '    If moRowExpiration Is Nothing Then
            Dim oCommPlanData As CommPlanData = CType(oData, CommPlanData)
            Dim dal As New CommPlanDAL
            ds = dal.LoadExpiration(oCommPlanData)
            If ds.Tables(CommPlanDAL.TABLE_NAME).Rows.Count = 0 Then
                ' oExpiration = CType(GenericConstants.INFINITE_DATE, Date)
                oExpiration = GenericConstants.INFINITE_DATE
            Else
                moRowExpiration = ds.Tables(CommPlanDAL.TABLE_NAME).Rows(0)
                oExpiration = CType(moRowExpiration(CommPlanDAL.COL_NAME_MAX_EXPIRATION), Date)
            End If
            '  Else
            '  oExpiration = CType(moRowExpiration(CommissionPeriodDAL.COL_NAME_MAX_EXPIRATION), Date)
            '  End If

            Return oExpiration
        End Get
    End Property

    Public ReadOnly Property ExpirationCount(ByVal oData As Object) As Integer
        Get
            Dim ds As Dataset
            Dim nExpiration As Integer

            '   If moRowExpiration Is Nothing Then
            Dim oCommPlanData As CommPlanData = CType(oData, CommPlanData)
            Dim dal As New CommPlanDAL
            ds = dal.LoadExpiration(oCommPlanData)
            If ds.Tables(CommPlanDAL.TABLE_NAME).Rows.Count = 0 Then
                nExpiration = 0
            Else
                moRowExpiration = ds.Tables(CommPlanDAL.TABLE_NAME).Rows(0)
                nExpiration = CType(moRowExpiration(CommPlanDAL.COL_NAME_EXPIRATION_COUNT), Integer)
            End If
            '  Else
            '   nExpiration = CType(moRowExpiration(CommissionPeriodDAL.COL_NAME_EXPIRATION_COUNT), Integer)
            '   End If

            Return nExpiration
        End Get
    End Property

    Public ReadOnly Property ExpirationOverlapping(ByVal oData As Object) As Integer
        Get
            Dim ds As Dataset
            Dim nExpiration As Integer

            '   If moRowExpiration Is Nothing Then
            Dim oCommPlanDataExp As CommPlanDataExp = CType(oData, CommPlanDataExp)
            Dim dal As New CommPlanDAL
            ds = dal.GetExpirationOverlap(oCommPlanDataExp)
            If ds.Tables(CommPlanDAL.TABLE_NAME).Rows.Count = 0 Then
                nExpiration = 0
            Else
                moRowExpiration = ds.Tables(CommPlanDAL.TABLE_NAME).Rows(0)
                nExpiration = CType(moRowExpiration(CommPlanDAL.COL_NAME_EXPIRATION_COUNT), Integer)
            End If
            '  Else
            '   nExpiration = CType(moRowExpiration(CommissionPeriodDAL.COL_NAME_EXPIRATION_COUNT), Integer)
            '   End If

            Return nExpiration
        End Get
    End Property

    Public Shared Function CommPaymentExist(ByVal pi_commmission_plan_id As Guid) As String
        Try
            Dim dal As New CommPlanDAL
            Return dal.CommPaymentExist(pi_commmission_plan_id)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    'Public Shared Function CheckDatesOverLap(ByVal pi_dealer_id As Guid, byval pi_effective_date As Date, pi_expiration_date as Date ) As String
    Public Shared Function CheckDatesOverLap(ByVal pi_dealer_id As Guid, ByVal pi_expiration_date as Date, ByVal pi_commmission_plan_id As Guid ) As String
        Try
            Dim dal As New CommPlanDAL
            'Return dal.CheckDatesOverLap(pi_dealer_id ,pi_effective_date , pi_expiration_date )
            Return dal.CheckDatesOverLap(pi_dealer_id, pi_expiration_date, pi_commmission_plan_id )
       Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

#End Region

#End Region
    
#Region "Public Members"
    Public Sub Copy(ByVal original As CommPlan)
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
                Dim dal As New CommPlanDAL
                dal.UpdateFamily(Dataset) 'New Code Added Manually
                'Reload the Data from the DB
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

    Public Shared Function getList(ByVal oCommPlanData As CommPlanData) As CommPlanSearchDV
        Try
            Dim dal As New CommPlanDAL
            Return New CommPlanSearchDV(dal.LoadList(oCommPlanData, ElitaPlusIdentity.Current.ActiveUser.Id).Tables(0))
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

    ReadOnly Property IExpiration() As DateType Implements IValidateIntervalDate.IExpiration
        Get
            Return ExpirationDate
        End Get
    End Property

    ReadOnly Property IMaxExpiration() As DateType Implements IValidateIntervalDate.IMaxExpiration
        Get
            Dim oCommPlanData As New CommPlanData
            With oCommPlanData
                .dealerId = DealerId
            End With
            Return New DateType(MaxExpiration(oCommPlanData))
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

#Region "CommPlanSearchDV"
    Public Class CommPlanSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_COMMISSION_PERIOD_ID As String = CommPlanDAL.COL_NAME_COMM_PLAN_ID
        Public Const COL_COMPANY_CODE As String = CommPlanDAL.COL_NAME_COMPANY_CODE
        Public Const COL_DEALER_NAME As String = CommPlanDAL.COL_NAME_DEALER_NAME
        Public Const COL_CODE As String = CommPlanDAL.COL_NAME_CODE
        Public Const COL_DESCRIPTION As String = CommPlanDAL.COL_NAME_DESCRIPTION
        Public Const COL_EFFECTIVE_DATE As String = CommPlanDAL.COL_NAME_EFFECTIVE_DATE
        Public Const COL_EXPIRATION_DATE As String = CommPlanDAL.COL_NAME_EXPIRATION_DATE
#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region
End Class