'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (8/30/2004)  ********************

Public Class Manufacturer
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
        SetValue(ManufacturerDAL.COL_NAME_COMPANY_GROUP_ID, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

    End Sub

    'New BO
    Public Sub New(sDesc As String, CompanyGroupID As Guid)
        MyBase.New()
        Dataset = New DataSet
        'Me.SetValue(ManufacturerDAL.COL_NAME_DESCRIPTION, sDesc.ToUpper)
        'Me.SetValue(ManufacturerDAL.COL_NAME_COMPANY_GROUP_ID, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
        Load(sDesc, CompanyGroupID)

    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(id As Guid, familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load()
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New ManufacturerDAL
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
            Dim dal As New ManufacturerDAL
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

    Protected Sub Load(sDesc As String, CompanyGroupID As Guid)
        Try
            Dim dal As New ManufacturerDAL
            If _isDSCreator Then
                If Row IsNot Nothing Then
                    Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Row)
                End If
            End If
            Row = Nothing
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Row = FindRow(sDesc, dal.COL_NAME_MANUFACTURER_ID, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.Load(Dataset, sDesc, CompanyGroupID)
                Row = FindRow(sDesc, dal.COL_NAME_DESCRIPTION, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then
                Throw New DataNotFoundException
            End If

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

#End Region

#Region "Constants"
    Public Const COL_NAME_DESCRIPTION = "description"
    Public Const COL_NAME_MANUFACTURER_ID = "manufacturer_id"
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
            If Row(ManufacturerDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ManufacturerDAL.COL_NAME_MANUFACTURER_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=255)> _
    Public Property Description As String
        Get
            CheckDeleted()
            If Row(ManufacturerDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ManufacturerDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ManufacturerDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public ReadOnly Property companyGroupId As Guid
        Get
            CheckDeleted()
            If Row(ManufacturerDAL.COL_NAME_COMPANY_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ManufacturerDAL.COL_NAME_COMPANY_GROUP_ID), Byte()))
            End If
        End Get
        'Set(ByVal Value As Guid)
        '    CheckDeleted()
        '    Me.SetValue(ManufacturerDAL.COL_NAME_COMPANY_ID, Value)
        'End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ManufacturerDAL
                dal.Update(Row)
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then Load(Id)
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    Public Shared Function ResolveManufacturer(manufacturerName As String, companyGroupId As Guid) As Guid
        Dim dal As New ManufacturerDAL
        Try
            Return dal.ResolveManufacturer(manufacturerName, companyGroupId)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Function
#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function LoadList(descriptionMask As String, _
                                      companyGroupId As Guid) As DataView
        Try
            Dim dal As New ManufacturerDAL
            Dim ds As Dataset
            ds = dal.LoadList(descriptionMask, companyGroupId)
            Return (ds.Tables(ManufacturerDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetNewDataViewRow(dv As DataView, id As Guid) As DataView

        Dim dt As DataTable
        dt = dv.Table

        Dim row As DataRow = dt.NewRow

        row(ManufacturerDAL.COL_NAME_DESCRIPTION) = String.Empty
        row(ManufacturerDAL.COL_NAME_MANUFACTURER_ID) = id.ToByteArray

        dt.Rows.Add(row)

        Return (dv)

    End Function

#End Region

#Region "Lookup Functions"
    Public Shared Function GetDescription(pManufacturerId As Guid) As String
        If (pManufacturerId = Guid.Empty) Then
            Return String.Empty
        End If
        Return New Manufacturer(pManufacturerId).Description
    End Function

#End Region

#Region "Web Service Methods"

    'Public Function GetMakes(ByVal DS As VSCGetMakesInputDs) As String
    '    Try

    '        Dim sWSConsumer As String = ""
    '        Dim XMLData As String
    '        Dim VSCUtil As New WSUtility

    '        'If Not DS.VSCGetMakesInput.Count = 0 Then
    '        '    With DS.VSCGetMakesInput.Item(0)
    '        '        sWSConsumer = .wsConsumer
    '        '    End With
    '        'End If

    '        Dim companyGroupID As Guid = Authentication.CompanyGroupId

    '        Dim dv As DataView = LoadList("", companyGroupID)

    '        Dim dsRetMsg As New DataSet
    '        dsRetMsg.DataSetName = "VSCGetMakesResultset"
    '        dsRetMsg.Tables.Add(dv.Table.Copy)
    '        XMLData = XMLHelper.FromDatasetToXML(dsRetMsg)
    '        dsRetMsg.Dispose()

    '        dv.Dispose()
    '        DS.Dispose()

    '        Return XMLData

    '    Catch ex As Exception
    '        Throw New ElitaWSException(ex.Message)
    '    End Try
    'End Function
    Public Shared Function GetVSCMakes(CompanyGroupId As Guid) As DataSet
        Try
            Dim dal As New ManufacturerDAL
            Return dal.LoadVSCMakes(CompanyGroupId)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetMakesForWS(CompanyGroupId As Guid) As DataSet
        Try
            Dim dal As New ManufacturerDAL
            Return dal.LoadMakesForWS(CompanyGroupId)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetMakesForWSByWarrantyMaster(dealerId As Guid, CompanyGroupId As Guid) As DataSet
        Try
            Dim dal As New ManufacturerDAL
            Return dal.GetMakesForWSByWarrantyMaster(dealerId, CompanyGroupId)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function
#End Region

End Class


