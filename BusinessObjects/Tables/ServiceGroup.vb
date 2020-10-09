'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (9/20/2004)  ********************

Public Class ServiceGroup
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(id As Guid)
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
    Public Sub New(id As Guid, familyDS As DataSet)
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

    'Exiting BO 
    Public Sub New(row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub


    Protected Sub Load()
        Try
            Dim dal As New ServiceGroupDAL
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
            Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            setvalue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            'Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(id As Guid)
        Try
            Dim dal As New ServiceGroupDAL
            If _isDSCreator Then
                If Row IsNot Nothing Then
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
    'Private Sub Initialize()
    '    Me.CountryId = CType(ElitaPlusIdentity.Current.ActiveUser.Countries.Item(0), Guid)
    'End Sub
#End Region


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid
        Get
            If Row(ServiceGroupDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ServiceGroupDAL.COL_NAME_SERVICE_GROUP_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CountryId As Guid
        Get
            CheckDeleted()
            If Row(ServiceGroupDAL.COL_NAME_COUNTRY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ServiceGroupDAL.COL_NAME_COUNTRY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceGroupDAL.COL_NAME_COUNTRY_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=10)> _
    Public Property ShortDesc As String
        Get
            CheckDeleted()
            If Row(ServiceGroupDAL.COL_NAME_SHORT_DESC) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServiceGroupDAL.COL_NAME_SHORT_DESC), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceGroupDAL.COL_NAME_SHORT_DESC, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=50)> _
    Public Property Description As String
        Get
            CheckDeleted()
            If Row(ServiceGroupDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServiceGroupDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceGroupDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ServiceGroupDAL
                'dal.Update(Me.Row) 'Original code generated replced by the code below
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

    Public Sub sgrtmanusave(ServiceGroupId As Guid, risktypeid As Guid, sgrtmanu As String)
        MyBase.Save()
        If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
            Dim dal As New ServiceGroupDAL
            dal.sgrtmanusave(ServiceGroupId, risktypeid, sgrtmanu)
        End If

    End Sub

    Public Function countofrecords(servicegroupid As Guid) As Double
        Dim ds As New DataSet
        Dim dal As New ServiceGroupDAL
        ds = dal.countofrecords(servicegroupid)
        Return CType(ds.Tables(0).Rows(0)(0), Double)

    End Function

    Public Function LoadGrid(servicegroupID As Guid,
                                        PageIndex As Integer,
                                        SortExpression As String) As DataSet
        Try
            Dim dal As New ServiceGroupDAL
            Dim ds As DataSet
            ds = dal.LoadGrid(servicegroupID, PageIndex, SortExpression)
            Return (ds)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    'Added manually to the code
    Public Overrides ReadOnly Property IsDirty As Boolean
        Get
            Return MyBase.IsDirty OrElse IsChildrenDirty
        End Get
    End Property

    Public Sub Copy(original As ServiceGroup)
        If Not IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Service Group")
        End If
        'Copy myself
        CopyFrom(original)
        'copy the childrens        
        Dim sgRt As ServiceGroupRiskType
        For Each sgRt In original.ServiceGroupRiskTypeChildren
            If sgRt.IsAssociatedForAnyManufacturer Then
                AttachRiskTypeForAnyManufacturer(sgRt.RiskTypeId)
            Else
                Dim selManDv As DataView = sgRt.GetSelectedManufacturers
                Dim selManList As New ArrayList
                Dim i As Integer = 0
                For i = 0 To selManDv.Count - 1
                    selManList.Add(New Guid(CType(selManDv(i)(LookupListNew.COL_ID_NAME), Byte())).ToString)
                Next
                AttachManufacturers(sgRt.RiskTypeId, selManList)
            End If
        Next
    End Sub



#End Region

#Region "Children Related"
    Public ReadOnly Property ServiceGroupRiskTypeChildren As ServiceGroupRiskTypeList
        Get
            Return New ServiceGroupRiskTypeList(Me)
        End Get
    End Property


    'METHODS ADDED MANUALLY. BEGIN

    Public Sub UpdateManufacturers(riskTypeId As Guid, selectedManufacturerGuidStrCollection As Hashtable)
        Dim sgRt As ServiceGroupRiskType = ServiceGroupRiskTypeChildren.Find(riskTypeId)
        If sgRt Is Nothing AndAlso selectedManufacturerGuidStrCollection.Count > 0 Then 'add it
            sgRt = ServiceGroupRiskTypeChildren.GetNewChild()
            sgRt.RiskTypeId = riskTypeId
            sgRt.ServiceGroupId = Id
            sgRt.Save()
        End If
        If sgRt IsNot Nothing Then
            sgRt.UpdateManufaturers(selectedManufacturerGuidStrCollection)
        End If
    End Sub

    Public Sub AttachManufacturers(riskTypeId As Guid, selectedManufacturerGuidStrCollection As ArrayList)
        Dim sgRt As ServiceGroupRiskType = ServiceGroupRiskTypeChildren.Find(riskTypeId)
        If sgRt Is Nothing AndAlso selectedManufacturerGuidStrCollection.Count > 0 Then 'add it
            sgRt = ServiceGroupRiskTypeChildren.GetNewChild()
            sgRt.RiskTypeId = riskTypeId
            sgRt.ServiceGroupId = Id
            sgRt.Save()
        End If
        If sgRt IsNot Nothing Then
            sgRt.AttachManufaturers(selectedManufacturerGuidStrCollection)
        End If
    End Sub

    Public Sub DetachManufacturers(riskTypeId As Guid, selectedManufacturerGuidStrCollection As ArrayList)
        Dim sgRt As ServiceGroupRiskType = ServiceGroupRiskTypeChildren.Find(riskTypeId)
        If sgRt IsNot Nothing Then
            sgRt.DetachManufaturers(selectedManufacturerGuidStrCollection)
            If sgRt.SgRtManufacturerChildren.Count = 0 Then
                sgRt.Delete()
                sgRt.Save()
            End If
        End If
    End Sub


    'For RiskTypes associated to "Any" Manufacturer will return nothing
    Public Function GetAvailableManufacturers(riskTypeId As Guid) As DataView
        Dim sgRt As ServiceGroupRiskType = ServiceGroupRiskTypeChildren.Find(riskTypeId)
        If sgRt Is Nothing Then
            'Return all
            Return LookupListNew.GetManufacturerLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
        Else
            Return sgRt.GetAvailableManufacturers()
        End If
    End Function

    Public Function GetSelectedManufacturers(riskTypeId As Guid) As DataView
        Dim sgRt As ServiceGroupRiskType = ServiceGroupRiskTypeChildren.Find(riskTypeId)
        If sgRt Is Nothing Then
            Return Nothing
        Else
            Return sgRt.GetSelectedManufacturers()
        End If
    End Function

    Public Function IsRiskTypeAssociatedForAnyManufacturer(riskTypeId As Guid) As Boolean
        Dim sgRt As ServiceGroupRiskType = ServiceGroupRiskTypeChildren.Find(riskTypeId)
        If sgRt Is Nothing Then
            Return False
        Else
            Return sgRt.IsAssociatedForAnyManufacturer()
        End If
    End Function

    Public Sub DetachRiskType(riskTypeId As Guid)
        Dim sgRt As ServiceGroupRiskType = ServiceGroupRiskTypeChildren.Find(riskTypeId)
        If sgRt IsNot Nothing Then
            sgRt.Delete()
            sgRt.Save()
        End If
    End Sub

    Public Sub AttachRiskTypeForAnyManufacturer(riskTypeId As Guid)
        DetachRiskType(riskTypeId)
        Dim sgRt As ServiceGroupRiskType = ServiceGroupRiskTypeChildren.GetNewChild()
        sgRt.RiskTypeId = riskTypeId
        sgRt.ServiceGroupId = Id
        sgRt.Save()
    End Sub



    Public Function GetRiskTypeManufacturersView() As RiskTypeManufacturerDataView
        Dim table As New DataTable
        table.Columns.Add(RiskTypeManufacturerDataView.RISK_TYPE_COL_NAME, GetType(String))
        table.Columns.Add(RiskTypeManufacturerDataView.MANUFACTURER_COL_NAME, GetType(String))
        table.Columns.Add(RiskTypeManufacturerDataView.SERVICE_GROUP_RISK_COL_ID, GetType(Guid))
        table.Columns.Add(RiskTypeManufacturerDataView.SERVICE_GROUP_RISK_MAN_COL_ID, GetType(Guid))
        Dim sgRtBo As ServiceGroupRiskType
        For Each sgRtBo In ServiceGroupRiskTypeChildren
            If sgRtBo.IsAssociatedForAnyManufacturer() Then
                Dim row As DataRow = table.NewRow
                row(RiskTypeManufacturerDataView.RISK_TYPE_COL_NAME) = sgRtBo.RiskTypeDescription
                row(RiskTypeManufacturerDataView.MANUFACTURER_COL_NAME) = "*"
                row(RiskTypeManufacturerDataView.SERVICE_GROUP_RISK_COL_ID) = sgRtBo.Id
                table.Rows.Add(row)
            Else
                Dim sgRtMan As SgRtManufacturer
                For Each sgRtMan In sgRtBo.SgRtManufacturerChildren
                    Dim row As DataRow = table.NewRow
                    row(RiskTypeManufacturerDataView.RISK_TYPE_COL_NAME) = sgRtBo.RiskTypeDescription
                    row(RiskTypeManufacturerDataView.MANUFACTURER_COL_NAME) = sgRtMan.ManufacturerDescription
                    row(RiskTypeManufacturerDataView.SERVICE_GROUP_RISK_COL_ID) = sgRtBo.Id
                    row(RiskTypeManufacturerDataView.SERVICE_GROUP_RISK_MAN_COL_ID) = sgRtMan.Id
                    table.Rows.Add(row)
                Next
            End If
        Next
        Return New RiskTypeManufacturerDataView(table)
    End Function

    Public Class RiskTypeManufacturerDataView
        Inherits DataView
        Public Const RISK_TYPE_COL_NAME As String = "RiskTypeDescription"
        Public Const MANUFACTURER_COL_NAME As String = "ManufacturerDescription"
        Public Const SERVICE_GROUP_RISK_COL_ID As String = "ServiceGroupRiskColId"
        Public Const SERVICE_GROUP_RISK_MAN_COL_ID As String = "ServiceGroupRiskManColId"

        Public Sub New(Table As DataTable)
            MyBase.New(Table)
        End Sub
        Public ReadOnly Property RiskTypeDescription(row As DataRow) As String
            Get
                Return CType(row(RISK_TYPE_COL_NAME), String)
            End Get
        End Property
        Public ReadOnly Property ManufacturerDescription(row As DataRow) As String
            Get
                Return CType(row(MANUFACTURER_COL_NAME), String)
            End Get
        End Property
        Public ReadOnly Property ServiceGroupRiskColId(row As DataRow) As Guid
            Get
                Return New Guid(CType(row(SERVICE_GROUP_RISK_COL_ID), String))
            End Get
        End Property
        Public ReadOnly Property ServiceGroupRiskManColId(row As DataRow) As Guid
            Get
                Return New Guid(CType(row(SERVICE_GROUP_RISK_MAN_COL_ID), String))
            End Get
        End Property
    End Class


    'METHODS ADDED MANUALLY. END
#End Region

#Region "DataView Retrieveing Methods"

    Public Shared Function getList(searchCode As String, searchDesc As String, oCountryId As Guid) As ServiceGroupSearchDV
        Try
            Dim dal As New ServiceGroupDAL
            Dim oCountryIds As ArrayList

            If DALBase.IsNothing(oCountryId) Then
                ' Get All User Countries
                oCountryIds = ElitaPlusIdentity.Current.ActiveUser.Countries
            Else
                oCountryIds = New ArrayList
                oCountryIds.Add(oCountryId)
            End If

            Return New ServiceGroupSearchDV(dal.LoadList(oCountryIds, searchCode, searchDesc).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function


    Public Class ServiceGroupSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_COUNTRY_DESC As String = ServiceGroupDAL.COL_NAME_COUNTRY_DESC
        Public Const COL_NAME_DESCRIPTION As String = ServiceGroupDAL.COL_NAME_DESCRIPTION
        Public Const COL_NAME_SHORT_DESC As String = ServiceGroupDAL.COL_NAME_SHORT_DESC
        Public Const COL_NAME_SERVICE_GROUP_ID As String = ServiceGroupDAL.COL_NAME_SERVICE_GROUP_ID
#End Region

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property ServiceGroupId(row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_SERVICE_GROUP_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property Description(row As DataRow) As String
            Get
                Return row(COL_NAME_DESCRIPTION).ToString
            End Get
        End Property

        Public Shared ReadOnly Property ShortDescription(row As DataRow) As String
            Get
                Return row(COL_NAME_SHORT_DESC).ToString
            End Get
        End Property


    End Class

#End Region

End Class



