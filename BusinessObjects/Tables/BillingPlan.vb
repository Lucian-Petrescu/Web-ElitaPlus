'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (11/19/2004)  ********************

Public Class BillingPlan
    Inherits BusinessObjectBase

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
            Dim dal As New BillingPlanDAL
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
            Dim dal As New BillingPlanDAL
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

    'Protected Sub Load(ByVal Company_id As Guid, ByVal Dealer As String)
    '    Try
    '        Dim dal As New BillingPlanDAL
    '        If Me._isDSCreator Then
    '            If Not Me.Row Is Nothing Then
    '                Me.Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Me.Row)
    '            End If
    '        End If
    '        Me.Row = Nothing
    '        If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
    '            Me.Row = Me.FindRow(Company_id, dal.COL_NAME_COMPANY_ID, Dealer, dal.COL_NAME_DEALER, Me.Dataset.Tables(dal.TABLE_NAME))
    '        End If
    '        If Me.Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
    '            dal.Load(Me.Dataset, Company_id, Dealer)
    '            Me.Row = Me.FindRow(Company_id, dal.COL_NAME_COMPANY_ID, Dealer, dal.COL_NAME_DEALER, Me.Dataset.Tables(dal.TABLE_NAME))
    '        End If
    '        If Me.Row Is Nothing Then
    '            Throw New DataNotFoundException
    '        End If
    '    Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
    '        Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
    '    End Try
    'End Sub
#End Region



#Region "Private Members"
    'Initialization code for new objects
    Private Sub Initialize()
    End Sub
#End Region

#Region "CONSTANTS"

    Public Const COL_BILLING_PLAN_ID As String = "BILLING_PLAN_ID"
    Public Const COL_DEALER_GROUP_ID As String = "DEALER_GROUP_ID"
    Public Const COL_DEALER_ID As String = "DEALER_ID"
    Public Const COL_CODE As String = "CODE"
    Public Const COL_DESCRIPTION As String = "DESCRIPTION"
#End Region



#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If Row(BillingPlanDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(BillingPlanDAL.COL_NAME_BILLING_PLAN_ID), Byte()))
            End If
        End Get
    End Property





    <ValueMandatory(""), ValidStringLength("", Max:=10)> _
    Public Property BillingPlanCode() As String
        Get
            CheckDeleted()
            If Row(BillingPlanDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(BillingPlanDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(BillingPlanDAL.COL_NAME_CODE, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=50)> _
    Public Property BillingPlanDescription() As String
        Get
            CheckDeleted()
            If Row(BillingPlanDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(BillingPlanDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(BillingPlanDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property




    Public Property DealerId() As Guid
        Get
            CheckDeleted()
            If Row(BillingPlanDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(BillingPlanDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(BillingPlanDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property

    Public Property DealerGroupId() As Guid
        Get
            CheckDeleted()
            If Row(BillingPlanDAL.COL_NAME_DEALER_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(BillingPlanDAL.COL_NAME_DEALER_GROUP_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(BillingPlanDAL.COL_NAME_DEALER_GROUP_ID, Value)
        End Set
    End Property



#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New BillingPlanDAL
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


    Public Shared Sub DeleteBillingPlan(ByVal billing_plan_id As Guid)

        Dim dal As New BillingPlanDAL
        dal.Delete(billing_plan_id)
    End Sub




#End Region





#Region "DataView Retrieveing Methods"
    

    Public Shared Function getList(ByVal DealerId As Guid, ByVal dealer_group_id As Guid, ByVal billingPlan As String) As BillingPlanSearchDV ' , ByVal company_groupId As Guid
        Try
            Dim dal As New BillingPlanDAL
            Dim compIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Dim compGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            Return New BillingPlanSearchDV(dal.LoadList(DealerId, dealer_group_id, billingPlan, compIds, compGroupId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetNewDataViewRow(ByVal dv As BillingPlanSearchDV, ByVal bo As BillingPlan) As BillingPlanSearchDV

        Dim dt As DataTable
        dt = dv.Table

        If bo.IsNew Then
            Dim row As DataRow = dt.NewRow

            row(BillingPlanSearchDV.COL_BILLING_PLAN_CODE) = bo.BillingPlanCode 'String.Empty
            row(BillingPlanSearchDV.COL_BILLING_PLAN) = bo.BillingPlanDescription 'String.Empty
            row(BillingPlanSearchDV.COL_BILLING_PLAN_ID) = bo.Id.ToByteArray
            row(BillingPlanSearchDV.COL_DEALER_ID) = bo.DealerId.ToByteArray
            row(BillingPlanSearchDV.COL_DEALER_GROUP_ID) = bo.DealerGroupId.ToByteArray
            'row(BillingPlanDAL.COL_NAME_ACCTING_BY_GROUP_DESC) = LookupListNew.GetDescriptionFromId(LookupListNew.LK_YESNO, bo.AcctingByGroupId)
            dt.Rows.Add(row)
        End If

        Return (dv)

    End Function
#End Region
#Region "BillingPlanSearchDV"
    Public Class BillingPlanSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_DEALER_ID As String = "dealer_id"
        Public Const COL_DEALER_CODE As String = "dealer_code"
        Public Const COL_DEALER_NAME As String = "dealer_name"
        Public Const COL_DEALER_GROUP_ID As String = "dealer_group_id"
        Public Const COL_DEALER_GROUP_CODE As String = "dealer_group_code"
        Public Const COL_DEALER_GROUP_NAME As String = "dealer_group"
        Public Const COL_BILLING_PLAN_CODE As String = "billing_plan_code"
        Public Const COL_BILLING_PLAN As String = "billing_plan"
        Public Const COL_BILLING_PLAN_ID As String = "billing_plan_id"

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



