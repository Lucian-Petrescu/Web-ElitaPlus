'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (8/15/2006)  ********************
'Namespace Table
Public Class ServiceNetwork
    Inherits BusinessObjectBase

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
            Dim dal As New ServiceNetworkDAL
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
            Dim dal As New ServiceNetworkDAL
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
            SetValue(ServiceNetworkDAL.COL_NAME_COMPANY_GROUP_ID, Value)
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
            SetValue(ServiceNetworkDAL.COL_NAME_SHORT_DESC, Value)
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
            SetValue(ServiceNetworkDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property

    Dim _Route As Route
    Public ReadOnly Property moRoute() As Route
        Get
            If (_Route Is Nothing) Then
                _Route = New Route(Id, Nothing)
            End If

            Return (_Route)
        End Get

    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ServiceNetworkDAL
                dal.UpdateFamily(Dataset) 'New Code Added Manually
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

    Public Overrides ReadOnly Property IsDirty() As Boolean
        Get
            Return MyBase.IsDirty OrElse IsChildrenDirty 
        End Get
    End Property

    Public Sub Copy(ByVal original As ServiceNetwork)
        If Not IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Service Center")
        End If
        'Copy myself
        CopyFrom(original)

        'copy the children       
        'Manufacturers
        Dim selSvrDv As DataView = original.GetSelectedServiceCenters
        Dim selSvrList As New ArrayList
        Dim i As Integer = 0
        For i = 0 To selSvrDv.Count - 1
            selSvrList.Add(New Guid(CType(selSvrDv(i)(LookupListNew.COL_ID_NAME), Byte())).ToString)
        Next
        AttachServiceCenters(selSvrList)

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
            Dim snSrvBO As ServiceNetworkSvc = ServiceNetworkSvcChildren.GetNewChild
            snSrvBO.ServiceCenterId = New Guid(snSrvIdStr)
            snSrvBO.ServiceNetworkId = Id
            snSrvBO.Save()
        Next
    End Sub

    Public Sub DetachServiceCenters(ByVal selectedServiceCenterGuidStrCollection As ArrayList)
        Dim snSrvIdStr As String
        For Each snSrvIdStr In selectedServiceCenterGuidStrCollection
            Dim snSrvBO As ServiceNetworkSvc = ServiceNetworkSvcChildren.Find(New Guid(snSrvIdStr))
            Dim servCenter As New ServiceCenter(snSrvBO.ServiceCenterId, Dataset)
            If Not servCenter.RouteId.Equals(Guid.Empty) Then
                Dim moRoute As New Route(servCenter.RouteId)
                If moRoute.ServiceNetworkId = Id Then
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
        For Each snSrvBO In ServiceNetworkSvcChildren
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


