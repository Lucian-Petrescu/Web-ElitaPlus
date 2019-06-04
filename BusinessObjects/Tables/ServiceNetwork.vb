'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (8/15/2006)  ********************
'Namespace Table
Public Class ServiceNetwork
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
            Dim dal As New ServiceNetworkDAL
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
            Dim dal As New ServiceNetworkDAL
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
            If Row(ServiceNetworkDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ServiceNetworkDAL.COL_NAME_SERVICE_NETWORK_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CompanyGroupId() As Guid
        Get
            CheckDeleted()
            If Row(ServiceNetworkDAL.COL_NAME_COMPANY_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ServiceNetworkDAL.COL_NAME_COMPANY_GROUP_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ServiceNetworkDAL.COL_NAME_COMPANY_GROUP_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=80)> _
    Public Property ShortDesc() As String
        Get
            CheckDeleted()
            If Row(ServiceNetworkDAL.COL_NAME_SHORT_DESC) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServiceNetworkDAL.COL_NAME_SHORT_DESC), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ServiceNetworkDAL.COL_NAME_SHORT_DESC, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=200)> _
    Public Property Description() As String
        Get
            CheckDeleted()
            If Row(ServiceNetworkDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServiceNetworkDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ServiceNetworkDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property

    Dim _Route As Route
    Public ReadOnly Property moRoute() As Route
        Get
            If (_Route Is Nothing) Then
                _Route = New Route(Me.Id, Nothing)
            End If

            Return (_Route)
        End Get

    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New ServiceNetworkDAL
                dal.UpdateFamily(Me.Dataset) 'New Code Added Manually
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
            Return MyBase.IsDirty OrElse Me.IsChildrenDirty 
        End Get
    End Property

    Public Sub Copy(ByVal original As ServiceNetwork)
        If Not Me.IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Service Center")
        End If
        'Copy myself
        Me.CopyFrom(original)

        'copy the children       
        'Manufacturers
        Dim selSvrDv As DataView = original.GetSelectedServiceCenters
        Dim selSvrList As New ArrayList
        Dim i As Integer = 0
        For i = 0 To selSvrDv.Count - 1
            selSvrList.Add(New Guid(CType(selSvrDv(i)(LookupListNew.COL_ID_NAME), Byte())).ToString)
        Next
        Me.AttachServiceCenters(selSvrList)

    End Sub

#End Region

#Region "Children Related"

    Public ReadOnly Property ServiceNetworkSvcChildren() As ServiceNetworkSVCList
        Get
            Return New ServiceNetworkSVCList(Me)
        End Get
    End Property

    Public Sub AttachServiceCenters(ByVal selectedServiceCenterGuidStrCollection As ArrayList)
        Dim snSrvIdStr As String
        For Each snSrvIdStr In selectedServiceCenterGuidStrCollection
            Dim snSrvBO As ServiceNetworkSvc = Me.ServiceNetworkSvcChildren.GetNewChild
            snSrvBO.ServiceCenterId = New Guid(snSrvIdStr)
            snSrvBO.ServiceNetworkId = Me.Id
            snSrvBO.Save()
        Next
    End Sub

    Public Sub DetachServiceCenters(ByVal selectedServiceCenterGuidStrCollection As ArrayList)
        Dim snSrvIdStr As String
        For Each snSrvIdStr In selectedServiceCenterGuidStrCollection
            Dim snSrvBO As ServiceNetworkSvc = Me.ServiceNetworkSvcChildren.Find(New Guid(snSrvIdStr))
            Dim servCenter As New ServiceCenter(snSrvBO.ServiceCenterId, Me.Dataset)
            If Not servCenter.RouteId.Equals(Guid.Empty) Then
                Dim moRoute As New Route(servCenter.RouteId)
                If moRoute.ServiceNetworkId = Me.Id Then
                    servCenter.RouteId = Guid.Empty
                    servCenter.Save()
                End If
            End If
            snSrvBO.Delete()
            snSrvBO.Save()
        Next
    End Sub

    Public Function GetAvailableServiceCenters() As DataView

        Dim dv As DataView = LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Countries)
        Dim sequenceCondition As String = GetServiceCentersLookupListSelectedSequenceFilter(dv, False)
        If dv.RowFilter Is Nothing OrElse dv.RowFilter.Trim.Length = 0 Then
            dv.RowFilter = sequenceCondition
        Else
            dv.RowFilter = "(" & dv.RowFilter & ") AND (" & sequenceCondition & ")"
        End If
        Return dv
    End Function

    Public Function GetSelectedServiceCenters() As DataView
        Dim dv As DataView = LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Countries)
        Dim sequenceCondition As String = GetServiceCentersLookupListSelectedSequenceFilter(dv, True)
        If dv.RowFilter Is Nothing OrElse dv.RowFilter.Trim.Length = 0 Then
            dv.RowFilter = sequenceCondition
        Else
            dv.RowFilter = "(" & dv.RowFilter & ") AND (" & sequenceCondition & ")"
        End If
        Return dv
    End Function

    Protected Function GetServiceCentersLookupListSelectedSequenceFilter(ByVal dv As DataView, ByVal isFilterInclusive As Boolean) As String

        Dim snSrvBO As ServiceNetworkSvc
        Dim inClause As String = "(-1"
        For Each snSrvBO In Me.ServiceNetworkSvcChildren
            inClause &= "," & LookupListNew.GetSequenceFromId(dv, snSrvBO.ServiceCenterId)
        Next
        inClause &= ")"
        Dim rowFilter As String = BusinessObjectBase.SYSTEM_SEQUENCE_COL_NAME
        If isFilterInclusive Then
            rowFilter &= " IN " & inClause
        Else
            rowFilter &= " NOT IN " & inClause
        End If
        Return rowFilter

    End Function


#End Region

#Region "DataView Retrieveing Methods"

    Public Shared Function getList(ByVal searchCode As String, ByVal searchDesc As String) As ServiceNetworkSearchDV
        Try
            Dim dal As New ServiceNetworkDAL
            Dim oCompany As New ElitaPlus.BusinessObjectsNew.Company(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
            Dim oCompanyGroupId As Guid = oCompany.CompanyGroupId

            Return New ServiceNetworkSearchDV(dal.LoadList(oCompany.CompanyGroupId, searchCode, searchDesc).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function


    Public Class ServiceNetworkSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_NAME_DESCRIPTION As String = ServiceNetworkDAL.COL_NAME_DESCRIPTION
        Public Const COL_NAME_SHORT_DESC As String = ServiceNetworkDAL.COL_NAME_SHORT_DESC
        Public Const COL_NAME_SERVICE_NETWORK_ID As String = ServiceNetworkDAL.COL_NAME_SERVICE_NETWORK_ID
#End Region

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property ServiceNetworkId(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_SERVICE_NETWORK_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property Description(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_DESCRIPTION).ToString
            End Get
        End Property

        Public Shared ReadOnly Property ShortDescription(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_SHORT_DESC).ToString
            End Get
        End Property


    End Class
#End Region

End Class
'End Namespace


