'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (3/14/2007)  ********************

Public Class VSCPlan
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(id As Guid)
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
    Public Sub New(id As Guid, familyDS As Dataset)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(familyDS As Dataset)
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
            Dim dal As New VSCPlanDAL
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

    Protected Sub Load(id As Guid)
        Try
            Dim dal As New VSCPlanDAL
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
            If row(VscPlanDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(VscPlanDAL.COL_NAME_VSC_PLAN_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CompanyGroupId As Guid
        Get
            CheckDeleted()
            If row(VscPlanDAL.COL_NAME_COMPANY_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(VscPlanDAL.COL_NAME_COMPANY_GROUP_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VscPlanDAL.COL_NAME_COMPANY_GROUP_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property RiskTypeId As Guid
        Get
            CheckDeleted()
            If row(VscPlanDAL.COL_NAME_RISK_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(VscPlanDAL.COL_NAME_RISK_TYPE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VscPlanDAL.COL_NAME_RISK_TYPE_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property RiskGroupId As Guid
        Get
            CheckDeleted()
            If row(VscPlanDAL.COL_NAME_RISK_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(VscPlanDAL.COL_NAME_RISK_GROUP_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VscPlanDAL.COL_NAME_RISK_GROUP_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=20)> _
    Public Property Code As String
        Get
            CheckDeleted()
            If row(VscPlanDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(VscPlanDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VscPlanDAL.COL_NAME_CODE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=200)> _
    Public Property Description As String
        Get
            CheckDeleted()
            If row(VscPlanDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(VscPlanDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VscPlanDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=4)> _
    Public Property IsWrapPlan As String
        Get
            CheckDeleted()
            If Row(VSCPlanDAL.COL_NAME_IS_WRAP_PLAN) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(VSCPlanDAL.COL_NAME_IS_WRAP_PLAN), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VSCPlanDAL.COL_NAME_IS_WRAP_PLAN, Value)
        End Set
    End Property



#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New VSCPlanDAL
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

    Public Sub Copy(original As VSCPlan)
        If Not IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing plan object")
        End If
        'Copy myself
        CopyFrom(original)
    End Sub
#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function GetVSCPlan(dealerID As Guid, VSCPlanDate As Date) As VSCPlan

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

    Public Shared Function getList(companyGroup As ArrayList) As VSCPlanSearchDV
        Try
            Dim dal As New VSCPlanDAL
            Return New VSCPlanSearchDV(dal.LoadList(companyGroup).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function getPlan(companyGroup As ArrayList, planId As Guid) As VSCPlanSearchDV
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

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region

End Class


