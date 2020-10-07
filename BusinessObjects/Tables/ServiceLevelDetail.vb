'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (3/5/2012)  ********************

Public Class ServiceLevelDetail
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
            Dim dal As New ServiceLevelDetailDAL
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
            Dim dal As New ServiceLevelDetailDAL
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
            If row(ServiceLevelDetailDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ServiceLevelDetailDAL.COL_NAME_SERVICE_LEVEL_DETAIL_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property ServiceLevelGroupId As Guid
        Get
            CheckDeleted()
            If row(ServiceLevelDetailDAL.COL_NAME_SERVICE_LEVEL_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ServiceLevelDetailDAL.COL_NAME_SERVICE_LEVEL_GROUP_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceLevelDetailDAL.COL_NAME_SERVICE_LEVEL_GROUP_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=20)> _
    Public Property Code As String
        Get
            CheckDeleted()
            If row(ServiceLevelDetailDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ServiceLevelDetailDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceLevelDetailDAL.COL_NAME_CODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=400)> _
    Public Property Description As String
        Get
            CheckDeleted()
            If row(ServiceLevelDetailDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ServiceLevelDetailDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceLevelDetailDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property



    Public Property RiskTypeId As Guid
        Get
            CheckDeleted()
            If row(ServiceLevelDetailDAL.COL_NAME_RISK_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ServiceLevelDetailDAL.COL_NAME_RISK_TYPE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceLevelDetailDAL.COL_NAME_RISK_TYPE_ID, Value)
        End Set
    End Property



    Public Property CostTypeId As Guid
        Get
            CheckDeleted()
            If row(ServiceLevelDetailDAL.COL_NAME_COST_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ServiceLevelDetailDAL.COL_NAME_COST_TYPE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceLevelDetailDAL.COL_NAME_COST_TYPE_ID, Value)
        End Set
    End Property



    Public Property ServiceLevelCost As DecimalType
        Get
            CheckDeleted()
            If row(ServiceLevelDetailDAL.COL_NAME_SERVICE_LEVEL_COST) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(ServiceLevelDetailDAL.COL_NAME_SERVICE_LEVEL_COST), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceLevelDetailDAL.COL_NAME_SERVICE_LEVEL_COST, Value)
        End Set
    End Property



    Public Property EffectiveDate As DateType
        Get
            CheckDeleted()
            If row(ServiceLevelDetailDAL.COL_NAME_EFFECTIVE_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(ServiceLevelDetailDAL.COL_NAME_EFFECTIVE_DATE).ToString()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceLevelDetailDAL.COL_NAME_EFFECTIVE_DATE, Value)
        End Set
    End Property



    Public Property ExpirationDate As DateType
        Get
            CheckDeleted()
            If row(ServiceLevelDetailDAL.COL_NAME_EXPIRATION_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(ServiceLevelDetailDAL.COL_NAME_EXPIRATION_DATE).ToString()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceLevelDetailDAL.COL_NAME_EXPIRATION_DATE, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ServiceLevelDetailDAL
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

    Public Shared Sub DeleteServiceLevelDetail(ByVal service_level_detail_id As Guid)

        Dim dal As New ServiceLevelDetailDAL
        dal.Delete(service_level_detail_id)
    End Sub
#End Region

#Region "DataView Retrieveing Methods"

    Public Shared Function getList(ByVal ServiceLevelGroupId As Guid, ByVal svcLevelCode As String, ByVal svcLevelDesc As String, ByVal sDate As String) As ServiceLevelDetailSearchDV ' , ByVal company_groupId As Guid
        Try
            Dim dal As New ServiceLevelDetailDAL
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId

            Return New ServiceLevelDetailSearchDV(dal.LoadList(ServiceLevelGroupId, svcLevelCode, svcLevelDesc, langId, sDate).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetNewDataViewRow(ByVal dv As ServiceLevelDetailSearchDV, ByVal bo As ServiceLevelDetail) As ServiceLevelDetailSearchDV

        Dim dt As DataTable
        dt = dv.Table

        If bo.IsNew Then
            Dim row As DataRow = dt.NewRow

            row(ServiceLevelDetailSearchDV.COL_SERVICE_LEVEL_DETAIL_ID) = bo.Id.ToByteArray
            row(ServiceLevelDetailSearchDV.COL_SERVICE_LEVEL_GROUP_ID) = bo.ServiceLevelGroupId.ToByteArray
            row(ServiceLevelDetailSearchDV.COL_SERVICE_LEVEL_CODE) = bo.Code
            row(ServiceLevelDetailSearchDV.COL_SERVICE_LEVEL_DESCRIPTION) = bo.Description
            row(ServiceLevelDetailSearchDV.COL_RISK_TYPE_ID) = bo.RiskTypeId.ToByteArray
            row(ServiceLevelDetailSearchDV.COL_COST_TYPE_ID) = bo.CostTypeId.ToByteArray
            If (bo.ServiceLevelCost Is Nothing) Then
                row(ServiceLevelDetailSearchDV.COL_SERVICE_LEVEL_COST) = DBNull.Value
            Else
                row(ServiceLevelDetailSearchDV.COL_SERVICE_LEVEL_COST) = CType(bo.ServiceLevelCost, Decimal)
            End If
            If (bo.EffectiveDate Is Nothing) Then
                row(ServiceLevelDetailSearchDV.COL_EFFECTIVE_DATE) = DBNull.Value
            Else
                row(ServiceLevelDetailSearchDV.COL_EFFECTIVE_DATE) = CType(bo.EffectiveDate, Date)
            End If
            If (bo.ExpirationDate Is Nothing) Then
                row(ServiceLevelDetailSearchDV.COL_EXPIRATION_DATE) = DBNull.Value
            Else
                row(ServiceLevelDetailSearchDV.COL_EXPIRATION_DATE) = CType(bo.ExpirationDate, Date)
            End If
            
            dt.Rows.Add(row)
        End If

        Return (dv)

    End Function

    Public Shared Function IsServiceLevelDetailValid(ByVal slgId As Guid, ByVal sCode As String, ByVal riskypeId As Guid, ByVal costTypeId As Guid, ByVal effectiveDate As Date) As Boolean
        Dim dal As New ServiceLevelDetailDAL
        Dim ds As DataSet
        Dim isValid As Boolean = True
        Dim intCnt As Integer = 0
        ds = dal.IsServiceLevelDetailValid(slgId, sCode, riskypeId, costTypeId, effectiveDate)
        If (Not ds.Tables(0) Is Nothing) AndAlso (ds.Tables(0).Rows.Count > 0) Then
            intCnt = CType(ds.Tables(0).Rows(0)("sld_count"), Integer)
            If intCnt > 0 Then
                isValid = False
            End If
        End If
        Return isValid

    End Function

#End Region

    Public Class ServiceLevelDetailSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_SERVICE_LEVEL_CODE As String = "code"
        Public Const COL_SERVICE_LEVEL_DESCRIPTION As String = "description"
        Public Const COL_RISK_TYPE As String = "risk_type"
        Public Const COL_RISK_TYPE_ID As String = "risk_type_id"
        Public Const COL_COST_TYPE As String = "cost_type"
        Public Const COL_COST_TYPE_ID As String = "cost_type_id"
        Public Const COL_SERVICE_LEVEL_COST As String = "service_level_cost"
        Public Const COL_EFFECTIVE_DATE As String = "effective_date"
        Public Const COL_EXPIRATION_DATE As String = "expiration_date"
        Public Const COL_SERVICE_LEVEL_DETAIL_ID As String = "service_level_detail_id"
        Public Const COL_SERVICE_LEVEL_GROUP_ID As String = "service_level_group_id"

#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class



End Class



