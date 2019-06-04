'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (11/18/2004)  ********************
Imports System.Globalization

Public Class CommissionPeriod
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
            Dim dal As New CommissionPeriodDAL
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
            Dim dal As New CommissionPeriodDAL
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

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If Row(CommissionPeriodDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CommissionPeriodDAL.COL_NAME_COMMISSION_PERIOD_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property DealerId() As Guid
        Get
            CheckDeleted()
            If Row(CommissionPeriodDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CommissionPeriodDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CommissionPeriodDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidIntervalDate("", Common.ErrorCodes.INVALID_EFFECTIVE_BIGGER_EXPIRATION_ERR, _
                        Common.ErrorCodes.INVALID_EFFECTIVE_SMALLER_MAXEXPIRATION_ERR, _
                        Common.ErrorCodes.INVALID_DELETE_SMALLER_MAXEXPIRATION_ERR)> _
    Public Property EffectiveDate() As DateType
        Get
            CheckDeleted()
            If Row(CommissionPeriodDAL.COL_NAME_EFFECTIVE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(CommissionPeriodDAL.COL_NAME_EFFECTIVE_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(CommissionPeriodDAL.COL_NAME_EFFECTIVE_DATE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidIntervalDate("", Common.ErrorCodes.INVALID_EFFECTIVE_BIGGER_EXPIRATION_ERR, _
                        Common.ErrorCodes.INVALID_EFFECTIVE_SMALLER_MAXEXPIRATION_ERR, _
                        Common.ErrorCodes.INVALID_DELETE_SMALLER_MAXEXPIRATION_ERR)> _
    Public Property ExpirationDate() As DateType
        Get
            CheckDeleted()
            If Row(CommissionPeriodDAL.COL_NAME_EXPIRATION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(CommissionPeriodDAL.COL_NAME_EXPIRATION_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(CommissionPeriodDAL.COL_NAME_EXPIRATION_DATE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ComputeMethodId() As Guid
        Get
            CheckDeleted()
            If Row(CommissionPeriodDAL.COL_NAME_COMPUTE_METHOD_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CommissionPeriodDAL.COL_NAME_COMPUTE_METHOD_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CommissionPeriodDAL.COL_NAME_COMPUTE_METHOD_ID, Value)
        End Set
    End Property

    Public Sub AttachPeriodEntity(ByVal newEntityId As Guid, ByVal position As Integer, ByVal PayeeTypeId As Guid, Optional ByVal newObject As CommissionPeriodEntity = Nothing)

        Dim newBO As CommissionPeriodEntity = New CommissionPeriodEntity(Me.Dataset)
        If Not newBO Is Nothing Then
            newBO.CommissionPeriodId = Me.Id
            newBO.EntityId = newEntityId
            newBO.Position = position
            newBO.PayeeTypeId = PayeeTypeId
            newBO.Save()
        End If

    End Sub

    Public Sub DetachPeriodEntity(ByVal periodEntity As CommissionPeriodEntity)

        If Not periodEntity Is Nothing Then
            periodEntity.Delete()
            periodEntity.Save()
        End If

    End Sub

    Public ReadOnly Property AssociatedCommPeriodEntity() As CommissionPeriodEntity.PeriodEntityList
        Get
            Return New CommissionPeriodEntity.PeriodEntityList(Me)
        End Get
    End Property

    Public ReadOnly Property AssociatedCommTolerance() As CommissionTolerance.ToleranceList
        Get
            Return New CommissionTolerance.ToleranceList(Me)
        End Get
    End Property

    'Public ReadOnly Property AddAssocComm(ByVal toleranceid As Guid) As AssociateCommissions.AssocCommList
    '    Get
    '        Return New AssociateCommissions.AssocCommList(Me, toleranceid)
    '    End Get

    'End Property
    Public Sub AttachTolerance(Optional ByVal NewObject As CommissionTolerance = Nothing)

        Dim newBO As CommissionTolerance = New CommissionTolerance(Me.Dataset)
        newBO.Copy(NewObject, Me.Dataset)

        If Not newBO Is Nothing Then
            newBO.CommissionPeriodId = Me.Id
            newBO.Save()
        End If

    End Sub

    Public Function AddCommTolerance(ByVal commToleranceID As Guid) As CommissionTolerance
        If commToleranceID.Equals(Guid.Empty) Then
            Dim objcommTolerance As New CommissionTolerance(Me.Dataset)
            objcommTolerance.CommissionPeriodId = Me.Id
            Return objcommTolerance
        Else
            Dim objcommTolerance As New CommissionTolerance(commToleranceID, Me.Dataset)
            Return objcommTolerance
        End If
    End Function

    Public Function AddAssocComm(ByVal assCommID As Guid) As AssociateCommissions
        If assCommID.Equals(Guid.Empty) Then
            Dim objAssocComm As New AssociateCommissions(Me.Dataset)
            Return objAssocComm
        Else
            Dim objAssocComm As New AssociateCommissions(assCommID, Me.Dataset)
            Return objAssocComm
        End If
    End Function

    Public Overrides ReadOnly Property IsDirty() As Boolean
        Get
            Return MyBase.IsDirty OrElse Me.IsChildrenDirty 'OrElse IsFamilyDirty
        End Get
    End Property

#Region "Properties-Expiration"

    Public ReadOnly Property MaxExpiration(ByVal oData As Object) As Date
        Get
            Dim ds As Dataset
            Dim oExpiration As Date

            '    If moRowExpiration Is Nothing Then
            Dim oCommissionPeriodData As CommissionPeriodData = CType(oData, CommissionPeriodData)
            Dim dal As New CommissionPeriodDAL
            ds = dal.LoadExpiration(oCommissionPeriodData)
            If ds.Tables(CommissionPeriodDAL.TABLE_NAME).Rows.Count = 0 Then
                ' oExpiration = CType(GenericConstants.INFINITE_DATE, Date)
                oExpiration = GenericConstants.INFINITE_DATE
            Else
                moRowExpiration = ds.Tables(CommissionPeriodDAL.TABLE_NAME).Rows(0)
                oExpiration = CType(moRowExpiration(CommissionPeriodDAL.COL_NAME_MAX_EXPIRATION), Date)
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
            Dim oCommissionPeriodData As CommissionPeriodData = CType(oData, CommissionPeriodData)
            Dim dal As New CommissionPeriodDAL
            ds = dal.LoadExpiration(oCommissionPeriodData)
            If ds.Tables(CommissionPeriodDAL.TABLE_NAME).Rows.Count = 0 Then
                nExpiration = 0
            Else
                moRowExpiration = ds.Tables(CommissionPeriodDAL.TABLE_NAME).Rows(0)
                nExpiration = CType(moRowExpiration(CommissionPeriodDAL.COL_NAME_EXPIRATION_COUNT), Integer)
            End If
            '  Else
            '   nExpiration = CType(moRowExpiration(CommissionPeriodDAL.COL_NAME_EXPIRATION_COUNT), Integer)
            '   End If

            Return nExpiration
        End Get
    End Property

#End Region

#End Region

#Region "Public Members"
    Public Sub Copy(ByVal original As CommissionPeriod)
        If Not Me.IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Dealer")
        End If
        'Copy myself
        Me.CopyFrom(original)

        'copy the children 

        Dim oPeriodEntity As CommissionPeriodEntity
        For Each oPeriodEntity In original.AssociatedCommPeriodEntity
            Dim newPeriodEntity As New CommissionPeriodEntity
            newPeriodEntity.CopyFrom(oPeriodEntity)
            AttachPeriodEntity(newPeriodEntity.EntityId, newPeriodEntity.Position, newPeriodEntity.PayeeTypeId)
        Next

        Dim oCommTolerance As CommissionTolerance
        For Each oCommTolerance In original.AssociatedCommTolerance
            'Dim newTolerance As New CommissionTolerance
            'newTolerance.Copy(oCommTolerance, Me.Dataset)
            AttachTolerance(oCommTolerance)
        Next

    End Sub
    'Public Overrides Sub Save()
    '    Try
    '        MyBase.Save()
    '        If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
    '            Dim dal As New CommissionPeriodDAL
    '            dal.Update(Me.Row)
    '            'Reload the Data from the DB
    '            If Me.Row.RowState <> DataRowState.Detached Then
    '                Dim objId As Guid = Me.Id
    '                Me.Dataset = New Dataset
    '                Me.Row = Nothing
    '                Me.Load(objId)
    '            End If
    '        End If
    '    Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
    '        Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
    '    End Try
    'End Sub


    Public Overrides Sub Save()
        Try
            MyBase.Save()
            'If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
            If Me._isDSCreator AndAlso Me.IsFamilyDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New CommissionPeriodDAL
                dal.UpdateFamily(Me.Dataset) 'New Code Added Manually
                'Reload the Data from the DB
                If Me.Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Me.Id
                    Me.Dataset = New DataSet
                    Me.Row = Nothing
                    Me.Load(objId)
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

    'Public Shared Function LoadList(ByVal oCommissionPeriodData As CommissionPeriodData) As DataView
    '    Try
    '        'Dim oCommissionPeriodData As CommissionPeriodData = CType(oData, CommissionPeriodData)
    '        Dim dal As New CommissionPeriodDAL
    '        Dim ds As DataSet

    '        ds = dal.LoadList(oCommissionPeriodData)
    '        Return (ds.Tables(CommissionPeriodDAL.TABLE_NAME).DefaultView)

    '    Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
    '        Throw New DataBaseAccessException(ex.ErrorType, ex)
    '    End Try

    'End Function

    Public Shared Function getList(ByVal oCommissionPeriodData As CommissionPeriodData) As CommissionPeriodSearchDV
        Try
            Dim dal As New CommissionPeriodDAL
            Return New CommissionPeriodSearchDV(dal.LoadList(oCommissionPeriodData).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function getCommPrdList(ByVal oCommPrdData As CommPrdData) As CommPrdPeriodSearchDV
        Try
            Dim dal As New CommissionPeriodDAL
            Return New CommPrdPeriodSearchDV(dal.LoadListCommPrd(oCommPrdData).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Public Shared Function GetRestrictMarkup(ByVal oPeriodData As CommissionPeriodData, ByVal Optional loggedinuserspecific As Boolean = True) As Boolean
        Dim oContract As Contract
        Dim oRestrictMarkupId, oYesRestrictMarkupId As Guid

        With oPeriodData
            oContract = Contract.GetCurrentContract(.dealerId, loggedinuserspecific)
        End With
        If oContract Is Nothing Then
            Dim errors() As ValidationError = {New ValidationError(COMMISSIONPERIODFORM_FORM001, GetType(CommissionPeriod), Nothing, Nothing, Nothing)}
            Throw New BOValidationException(errors, GetType(CommissionPeriod).FullName)
        End If
        oRestrictMarkupId = oContract.RestrictMarkupId
        oYesRestrictMarkupId = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, "Y")

        Return oRestrictMarkupId.Equals(oYesRestrictMarkupId)
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
            Dim oCommissionPeriodData As New CommissionPeriodData
            With oCommissionPeriodData
                .dealerId = Me.DealerId
            End With
            Return New DateType(MaxExpiration(oCommissionPeriodData))
        End Get
    End Property

    Public ReadOnly Property IIsNew() As Boolean Implements IValidateIntervalDate.IIsNew
        Get
            Return Me.IsNew
        End Get
    End Property

    ReadOnly Property IIsDeleted() As Boolean Implements IValidateIntervalDate.IIsDeleted
        Get
            Return Me.IsDeleted
        End Get
    End Property


#End Region

#Region "CommissionPeriodSearchDV"
    Public Class CommissionPeriodSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_COMMISSION_PERIOD_ID As String = CommissionPeriodDAL.COL_NAME_COMMISSION_PERIOD_ID
        Public Const COL_COMPANY_CODE As String = CommissionPeriodDAL.COL_NAME_COMPANY_CODE
        Public Const COL_DEALER_NAME As String = CommissionPeriodDAL.COL_NAME_DEALER_NAME
        Public Const COL_EFFECTIVE_DATE As String = CommissionPeriodDAL.COL_NAME_EFFECTIVE
        Public Const COL_EXPIRATION_DATE As String = CommissionPeriodDAL.COL_NAME_EXPIRATION

#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region

#Region "CommissionPeriodSearchDV"
    Public Class CommPrdPeriodSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_COMM_P_CODE_ID As String = CommissionPeriodDAL.COL_NAME_COMM_P_CODE_ID
        Public Const COL_COMPANY_CODE As String = CommissionPeriodDAL.COL_NAME_COMPANY_CODE
        Public Const COL_DEALER_NAME As String = CommissionPeriodDAL.COL_NAME_DEALER_NAME
        Public Const COL_PRODUCT_CODE As String = CommissionPeriodDAL.COL_NAME_PRODUCT_CODE
        Public Const COL_EFFECTIVE_DATE As String = CommissionPeriodDAL.COL_NAME_EFFECTIVE
        Public Const COL_EXPIRATION_DATE As String = CommissionPeriodDAL.COL_NAME_EXPIRATION

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



