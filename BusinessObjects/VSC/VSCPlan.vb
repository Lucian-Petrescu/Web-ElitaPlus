'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (3/14/2007)  ********************

Public Class VSCPlan
    Inherits BusinessObjectBase

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
            Dim dal As New VSCPlanDAL
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
            Dim dal As New VSCPlanDAL
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
            If row(VscPlanDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(VscPlanDAL.COL_NAME_VSC_PLAN_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CompanyGroupId() As Guid
        Get
            CheckDeleted()
            If row(VscPlanDAL.COL_NAME_COMPANY_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(VscPlanDAL.COL_NAME_COMPANY_GROUP_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(VscPlanDAL.COL_NAME_COMPANY_GROUP_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property RiskTypeId() As Guid
        Get
            CheckDeleted()
            If row(VscPlanDAL.COL_NAME_RISK_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(VscPlanDAL.COL_NAME_RISK_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(VscPlanDAL.COL_NAME_RISK_TYPE_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property RiskGroupId() As Guid
        Get
            CheckDeleted()
            If row(VscPlanDAL.COL_NAME_RISK_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(VscPlanDAL.COL_NAME_RISK_GROUP_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(VscPlanDAL.COL_NAME_RISK_GROUP_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=20)> _
    Public Property Code() As String
        Get
            CheckDeleted()
            If row(VscPlanDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(VscPlanDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(VscPlanDAL.COL_NAME_CODE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=200)> _
    Public Property Description() As String
        Get
            CheckDeleted()
            If row(VscPlanDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(VscPlanDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(VscPlanDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=4)> _
    Public Property IsWrapPlan() As String
        Get
            CheckDeleted()
            If Row(VSCPlanDAL.COL_NAME_IS_WRAP_PLAN) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(VSCPlanDAL.COL_NAME_IS_WRAP_PLAN), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(VSCPlanDAL.COL_NAME_IS_WRAP_PLAN, Value)
        End Set
    End Property



#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New VSCPlanDAL
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

    Public Overrides ReadOnly Property IsDirty() As Boolean
        Get
            Return MyBase.IsDirty
        End Get
    End Property

    Public Sub DeleteAndSave()
        Me.CheckDeleted()
        Me.BeginEdit()
        Try
            Me.Delete()
            Me.Save()
        Catch ex As Exception
            Me.cancelEdit()
            Throw ex
        End Try
    End Sub

    Public Sub Copy(ByVal original As VSCPlan)
        If Not Me.IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing plan object")
        End If
        'Copy myself
        Me.CopyFrom(original)
    End Sub
#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function GetVSCPlan(ByVal dealerID As Guid, ByVal VSCPlanDate As Date) As VSCPlan

        'Dim ProductCodeID As Guid
        'Dim dv As DataView = getList(dealerID)
        'dv.Sort = ProductCode.ProductCodeSearchDV.COL_EFFECTIVE & " DESC," & ProductCode.ProductCodeSearchDV.COL_EXPIRATION & " DESC"
        'Dim dt As DataTable = dv.Table

        'For Each row As DataRow In dt.Rows
        '    Dim MinEffective As Date = CType(row(ProductCode.ProductCodeSearchDV.COL_EFFECTIVE), Date)
        '    Dim MaxExpiration As Date = CType(row(ProductCode.ProductCodeSearchDV.COL_EXPIRATION), Date)
        '    If (ProductCodeDate >= MinEffective) And (ProductCodeDate < MaxExpiration) Then

        '        ProductCodeID = New Guid(CType(row(ProductCode.ProductCodeSearchDV.COL_ProductCode_ID), Byte()))
        '        Return New ProductCode(ProductCodeID)
        '    End If
        'Next



        Return Nothing
    End Function

    Public Shared Function getList(ByVal companyGroup As ArrayList) As VSCPlanSearchDV
        Try
            Dim dal As New VSCPlanDAL
            Return New VSCPlanSearchDV(dal.LoadList(companyGroup).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function getPlan(ByVal companyGroup As ArrayList, ByVal planId As Guid) As VSCPlanSearchDV
        Try
            Dim dal As New VSCPlanDAL
            Return New VSCPlanSearchDV(dal.LoadPlan(companyGroup, planId).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Class VSCPlanSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_VSCPlan_ID As String = "id"
        Public Const COL_COPMANY_ID As String = "company_id"
        Public Const COL_CODE As String = "code"
        Public Const COL_DESCRIPTION As String = "description"
        Public Const COL_RISK_GROUP As String = "risk_group_id"
        Public Const COL_RISK_TYPE As String = "risk_type_id"
        Public Const COL_ACTIVE_ID As String = "active_id"


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


