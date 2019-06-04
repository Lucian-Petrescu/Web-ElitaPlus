'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (3/14/2007)  ********************

Public Class VSCCoverage
    Inherits BusinessObjectBase

#Region "Constructors"
    Public Class VSCCoverageDV
        Inherits DataView


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Me.Dataset = New Dataset
        Me.Load(id)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Me.Dataset = New Dataset
        Me.Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDS As Dataset)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As Dataset)
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
            Dim dal As New VSCCoverageDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            setvalue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New VSCCoverageDAL
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
    Public Const COL_VSC_PLAN_CODE As String = "PLANID"
    Public Const COL_PLAN_CODE As String = "DESCRIPTION"
    Public Const COL_DEALER_CODE As String = "CODE"
    Public Const COL_DEALER_NAME As String = "DEALER_NAME"
    Public Const COL_DEALER_GROUP As String = "CODE"
    Public Const COL_EFFECTIVE_DATE As String = "EFFECTIVE_DATE"

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If Row(VSCCoverageDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(VSCCoverageDAL.COL_NAME_VSC_COVERAGE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property VscPlanId() As Guid
        Get
            CheckDeleted()
            If Row(VSCCoverageDAL.COL_NAME_VSC_PLAN_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(VSCCoverageDAL.COL_NAME_VSC_PLAN_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(VSCCoverageDAL.COL_NAME_VSC_PLAN_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property CoverageTypeId() As Guid
        Get
            CheckDeleted()
            If Row(VSCCoverageDAL.COL_NAME_COVERAGE_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(VSCCoverageDAL.COL_NAME_COVERAGE_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(VSCCoverageDAL.COL_NAME_COVERAGE_TYPE_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property AllocationPercentUsed() As DecimalType
        Get
            CheckDeleted()
            If Row(VSCCoverageDAL.COL_NAME_ALLOCATION_PERCENT_USED) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(VSCCoverageDAL.COL_NAME_ALLOCATION_PERCENT_USED), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(VSCCoverageDAL.COL_NAME_ALLOCATION_PERCENT_USED, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property AllocationPercentNew() As DecimalType
        Get
            CheckDeleted()
            If Row(VSCCoverageDAL.COL_NAME_ALLOCATION_PERCENT_NEW) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(VSCCoverageDAL.COL_NAME_ALLOCATION_PERCENT_NEW), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(VSCCoverageDAL.COL_NAME_ALLOCATION_PERCENT_NEW, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property IsBasePlanId() As Guid
        Get
            CheckDeleted()
            If Row(VSCCoverageDAL.COL_NAME_IS_BASE_PLAN_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(VSCCoverageDAL.COL_NAME_IS_BASE_PLAN_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(VSCCoverageDAL.COL_NAME_IS_BASE_PLAN_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property IsDealerDiscountId() As Guid
        Get
            CheckDeleted()
            If Row(VSCCoverageDAL.COL_NAME_IS_DEALER_DISCOUNT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(VSCCoverageDAL.COL_NAME_IS_DEALER_DISCOUNT_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(VSCCoverageDAL.COL_NAME_IS_DEALER_DISCOUNT_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property AddToPlanId() As Guid
        Get
            CheckDeleted()
            If Row(VSCCoverageDAL.COL_NAME_ADD_TO_PLAN_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(VSCCoverageDAL.COL_NAME_ADD_TO_PLAN_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(VSCCoverageDAL.COL_NAME_ADD_TO_PLAN_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property IsClaimAllowedId() As Guid
        Get
            CheckDeleted()
            If Row(VSCCoverageDAL.COL_NAME_IS_CLAIM_ALLOWED_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(VSCCoverageDAL.COL_NAME_IS_CLAIM_ALLOWED_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(VSCCoverageDAL.COL_NAME_IS_CLAIM_ALLOWED_ID, Value)
        End Set
    End Property
    <ValueMandatory("")> _
       Public Property PLANID() As Guid
        Get
            'CheckDeleted()
            If Row(VSCCoverageDAL.COL_NAME_VSC_PLAN_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(VSCCoverageDAL.COL_NAME_VSC_PLAN_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(VSCCoverageDAL.COL_NAME_VSC_PLAN_ID, Value)
        End Set
    End Property
    <ValueMandatory("")> _
   Public Property DEALER_CODE() As String
        Get
            'CheckDeleted()
            If Row(VSCCoverageDAL.COL_NAME_DEALER_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New String(CType(Row(VSCCoverageDAL.COL_NAME_DEALER_CODE), String))
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(VSCCoverageDAL.COL_NAME_DEALER_CODE, Value)
        End Set
    End Property
    <ValueMandatory("")> _
Public Property DEALER_NAME() As String
        Get
            CheckDeleted()
            If Row(VSCCoverageDAL.COL_NAME_DEALER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New String(CType(Row(VSCCoverageDAL.COL_NAME_DEALER_NAME), String))
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(VSCCoverageDAL.COL_NAME_DEALER_NAME, Value)
        End Set
    End Property
    <ValueMandatory("")> _
Public Property DEALER_GROUP() As String
        Get
            CheckDeleted()
            If Row(VSCCoverageDAL.COL_NAME_DEALER_GROUP) Is DBNull.Value Then
                Return Nothing
            Else
                Return New String(CType(Row(VSCCoverageDAL.COL_NAME_DEALER_GROUP), String))
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(VSCCoverageDAL.COL_NAME_DEALER_GROUP, Value)
        End Set
    End Property
    <ValueMandatory("")> _
Public Property Effective() As String
        Get
            'CheckDeleted()
            If Row(VSCCoverageDAL.COL_NAME_EFFECTIVE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New String(CType(Row(VSCCoverageDAL.COL_NAME_EFFECTIVE_DATE), String))
            End If
        End Get
        Set(ByVal Value As String)
            ' CheckDeleted()
            Me.SetValue(VSCCoverageDAL.COL_NAME_EFFECTIVE_DATE, Value)
        End Set
    End Property
#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New VSCCoverageDAL
                dal.Update(Me.Row)
                'Reload the Data from the DB
                If Me.Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Me.Id
                    Me.Dataset = New Dataset
                    Me.Row = Nothing
                    Me.Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
#End Region

#Region "DataView Retrieveing Methods"
    '.PLANID, .DEALER_CODE, .DEALER_NAME, .DEALER_GROUP, .EFFECTIVE_DATE
    Public Shared Function GetList(ByVal PLANID As Guid, ByVal DEALER_CODE As String, ByVal DEALER_NAME As String, ByVal DEALER_GROUP As String, ByVal EFFECTIVE_DATE As String, ByVal companygroupId As Guid) As VSCCoverageDV '
        Try
            Dim dal As New VSCCoverageDAL

            Return New VSCCoverageDV(dal.Load_VSC_Coverage_Plan(PLANID, DEALER_CODE, DEALER_NAME, DEALER_GROUP, EFFECTIVE_DATE, companygroupId).Tables(0))  ' , DEALER_CODE, EFFECTIVE_DATE DEALER_CODE, ElitaPlusIdentity.Current.ActiveUser.LanguageId(0))ElitaPlusIdentity.Current.ActiveUser.Companies, 
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetCoverageRateList(ByVal PLANID As Guid) As VSCCoverageDV '
        Try
            Dim dal As New VSCCoverageDAL

            Return New VSCCoverageDV(dal.Load_VSC_Coverage(PLANID).Tables(0))  ' , DEALER_CODE, EFFECTIVE_DATE DEALER_CODE, ElitaPlusIdentity.Current.ActiveUser.LanguageId(0))ElitaPlusIdentity.Current.ActiveUser.Companies, 
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetCoverageList(ByVal LanguageID As Guid, ByVal PlanID As Guid) As DataView
        Try
            Dim dal As New VSCCoverageDAL
            Return dal.LoadListByPlanID(LanguageID, PlanID).Tables(0).DefaultView
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

End Class


