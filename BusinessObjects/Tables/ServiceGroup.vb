'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (9/20/2004)  ********************

Public Class ServiceGroup
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
    Public Sub New(ByVal id As Guid, ByVal familyDS As DataSet)
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

    'Exiting BO 
    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Me.Dataset = row.Table.DataSet
        Me.Row = row
    End Sub


    Protected Sub Load()
        Try
            Dim dal As New ServiceGroupDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            setvalue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            'Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New ServiceGroupDAL
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
    'Private Sub Initialize()
    '    Me.CountryId = CType(ElitaPlusIdentity.Current.ActiveUser.Countries.Item(0), Guid)
    'End Sub
#End Region


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If Row(ServiceGroupDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ServiceGroupDAL.COL_NAME_SERVICE_GROUP_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CountryId() As Guid
        Get
            CheckDeleted()
            If Row(ServiceGroupDAL.COL_NAME_COUNTRY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ServiceGroupDAL.COL_NAME_COUNTRY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ServiceGroupDAL.COL_NAME_COUNTRY_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=10)> _
    Public Property ShortDesc() As String
        Get
            CheckDeleted()
            If Row(ServiceGroupDAL.COL_NAME_SHORT_DESC) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServiceGroupDAL.COL_NAME_SHORT_DESC), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ServiceGroupDAL.COL_NAME_SHORT_DESC, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=50)> _
    Public Property Description() As String
        Get
            CheckDeleted()
            If Row(ServiceGroupDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ServiceGroupDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ServiceGroupDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New ServiceGroupDAL
                'dal.Update(Me.Row) 'Original code generated replced by the code below
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

    Public Function sgrtmanusave(ByVal ServiceGroupId As Guid, ByVal risktypeid As Guid, ByVal sgrtmanu As String, ByVal result As Integer) As Integer
        MyBase.Save()
        If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
            Dim dal As New ServiceGroupDAL
            result = dal.sgrtmanusave(ServiceGroupId, risktypeid, sgrtmanu, result)
        End If

    End Function

    Public Function countofrecords(ByVal servicegroupid As Guid) As Double
        Dim ds As New DataSet
        Dim dal As New ServiceGroupDAL
        ds = dal.countofrecords(servicegroupid)
        Return CType(ds.Tables(0).Rows(0)(0), Double)

    End Function

    Public Function LoadGrid(ByVal servicegroupID As Guid,
                                        ByVal PageIndex As Integer,
                                        ByVal SortExpression As String) As DataSet
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
    Public Overrides ReadOnly Property IsDirty() As Boolean
        Get
            Return MyBase.IsDirty OrElse Me.IsChildrenDirty
        End Get
    End Property

    Public Sub Copy(ByVal original As ServiceGroup)
        If Not Me.IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Service Group")
        End If
        'Copy myself
        Me.CopyFrom(original)
        'copy the childrens        
        Dim sgRt As ServiceGroupRiskType
        For Each sgRt In original.ServiceGroupRiskTypeChildren
            If sgRt.IsAssociatedForAnyManufacturer Then
                Me.AttachRiskTypeForAnyManufacturer(sgRt.RiskTypeId)
            Else
                Dim selManDv As DataView = sgRt.GetSelectedManufacturers
                Dim selManList As New ArrayList
                Dim i As Integer = 0
                For i = 0 To selManDv.Count - 1
                    selManList.Add(New Guid(CType(selManDv(i)(LookupListNew.COL_ID_NAME), Byte())).ToString)
                Next
                Me.AttachManufacturers(sgRt.RiskTypeId, selManList)
            End If
        Next
    End Sub



#End Region

#Region "Children Related"
    Public ReadOnly Property ServiceGroupRiskTypeChildren() As ServiceGroupRiskTypeList
        Get
            Return New ServiceGroupRiskTypeList(Me)
        End Get
    End Property


    'METHODS ADDED MANUALLY. BEGIN

    Public Sub UpdateManufacturers(ByVal riskTypeId As Guid, ByVal selectedManufacturerGuidStrCollection As Hashtable)
        Dim sgRt As ServiceGroupRiskType = Me.ServiceGroupRiskTypeChildren.Find(riskTypeId)
        If sgRt Is Nothing AndAlso selectedManufacturerGuidStrCollection.Count > 0 Then 'add it
            sgRt = Me.ServiceGroupRiskTypeChildren.GetNewChild()
            sgRt.RiskTypeId = riskTypeId
            sgRt.ServiceGroupId = Me.Id
            sgRt.Save()
        End If
        If Not sgRt Is Nothing Then
            sgRt.UpdateManufaturers(selectedManufacturerGuidStrCollection)
        End If
    End Sub

    Public Sub AttachManufacturers(ByVal riskTypeId As Guid, ByVal selectedManufacturerGuidStrCollection As ArrayList)
        Dim sgRt As ServiceGroupRiskType = Me.ServiceGroupRiskTypeChildren.Find(riskTypeId)
        If sgRt Is Nothing AndAlso selectedManufacturerGuidStrCollection.Count > 0 Then 'add it
            sgRt = Me.ServiceGroupRiskTypeChildren.GetNewChild()
            sgRt.RiskTypeId = riskTypeId
            sgRt.ServiceGroupId = Me.Id
            sgRt.Save()
        End If
        If Not sgRt Is Nothing Then
            sgRt.AttachManufaturers(selectedManufacturerGuidStrCollection)
        End If
    End Sub

    Public Sub DetachManufacturers(ByVal riskTypeId As Guid, ByVal selectedManufacturerGuidStrCollection As ArrayList)
        Dim sgRt As ServiceGroupRiskType = Me.ServiceGroupRiskTypeChildren.Find(riskTypeId)
        If Not sgRt Is Nothing Then
            sgRt.DetachManufaturers(selectedManufacturerGuidStrCollection)
            If sgRt.SgRtManufacturerChildren.Count = 0 Then
                sgRt.Delete()
                sgRt.Save()
            End If
        End If
    End Sub


    'For RiskTypes associated to "Any" Manufacturer will return nothing
    Public Function GetAvailableManufacturers(ByVal riskTypeId As Guid) As DataView
        Dim sgRt As ServiceGroupRiskType = Me.ServiceGroupRiskTypeChildren.Find(riskTypeId)
        If sgRt Is Nothing Then
            'Return all
            Return LookupListNew.GetManufacturerLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
        Else
            Return sgRt.GetAvailableManufacturers()
        End If
    End Function

    Public Function GetSelectedManufacturers(ByVal riskTypeId As Guid) As DataView
        Dim sgRt As ServiceGroupRiskType = Me.ServiceGroupRiskTypeChildren.Find(riskTypeId)
        If sgRt Is Nothing Then
            Return Nothing
        Else
            Return sgRt.GetSelectedManufacturers()
        End If
    End Function

    Public Function IsRiskTypeAssociatedForAnyManufacturer(ByVal riskTypeId As Guid) As Boolean
        Dim sgRt As ServiceGroupRiskType = Me.ServiceGroupRiskTypeChildren.Find(riskTypeId)
        If sgRt Is Nothing Then
            Return False
        Else
            Return sgRt.IsAssociatedForAnyManufacturer()
        End If
    End Function

    Public Sub DetachRiskType(ByVal riskTypeId As Guid)
        Dim sgRt As ServiceGroupRiskType = Me.ServiceGroupRiskTypeChildren.Find(riskTypeId)
        If Not sgRt Is Nothing Then
            sgRt.Delete()
            sgRt.Save()
        End If
    End Sub

    Public Sub AttachRiskTypeForAnyManufacturer(ByVal riskTypeId As Guid)
        Me.DetachRiskType(riskTypeId)
        Dim sgRt As ServiceGroupRiskType = Me.ServiceGroupRiskTypeChildren.GetNewChild()
        sgRt.RiskTypeId = riskTypeId
        sgRt.ServiceGroupId = Me.Id
        sgRt.Save()
    End Sub



    Public Function GetRiskTypeManufacturersView() As RiskTypeManufacturerDataView
        Dim table As New DataTable
        table.Columns.Add(RiskTypeManufacturerDataView.RISK_TYPE_COL_NAME, GetType(String))
        table.Columns.Add(RiskTypeManufacturerDataView.MANUFACTURER_COL_NAME, GetType(String))
        table.Columns.Add(RiskTypeManufacturerDataView.SERVICE_GROUP_RISK_COL_ID, GetType(Guid))
        table.Columns.Add(RiskTypeManufacturerDataView.SERVICE_GROUP_RISK_MAN_COL_ID, GetType(Guid))
        Dim sgRtBo As ServiceGroupRiskType
        For Each sgRtBo In Me.ServiceGroupRiskTypeChildren
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

        Public Sub New(ByVal Table As DataTable)
            MyBase.New(Table)
        End Sub
        Public ReadOnly Property RiskTypeDescription(ByVal row As DataRow) As String
            Get
                Return CType(row(Me.RISK_TYPE_COL_NAME), String)
            End Get
        End Property
        Public ReadOnly Property ManufacturerDescription(ByVal row As DataRow) As String
            Get
                Return CType(row(Me.MANUFACTURER_COL_NAME), String)
            End Get
        End Property
        Public ReadOnly Property ServiceGroupRiskColId(ByVal row As DataRow) As Guid
            Get
                Return New Guid(CType(row(Me.SERVICE_GROUP_RISK_COL_ID), String))
            End Get
        End Property
        Public ReadOnly Property ServiceGroupRiskManColId(ByVal row As DataRow) As Guid
            Get
                Return New Guid(CType(row(Me.SERVICE_GROUP_RISK_MAN_COL_ID), String))
            End Get
        End Property
    End Class


    'METHODS ADDED MANUALLY. END
#End Region

#Region "DataView Retrieveing Methods"

    Public Shared Function getList(ByVal searchCode As String, ByVal searchDesc As String, ByVal oCountryId As Guid) As ServiceGroupSearchDV
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

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property ServiceGroupId(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_SERVICE_GROUP_ID), Byte()))
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



