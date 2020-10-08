'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (10/25/2004)  ********************

Public Class ZipDistrictDetail
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
    Public Sub New(familyDS As DataSet)
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
            Dim dal As New ZipDistrictDetailDAL
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
            Dim dal As New ZipDistrictDetailDAL
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
            If row(ZipDistrictDetailDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ZipDistrictDetailDAL.COL_NAME_ZIP_DISTRICT_DETAIL_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property ZipDistrictId As Guid
        Get
            CheckDeleted()
            If row(ZipDistrictDetailDAL.COL_NAME_ZIP_DISTRICT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ZipDistrictDetailDAL.COL_NAME_ZIP_DISTRICT_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ZipDistrictDetailDAL.COL_NAME_ZIP_DISTRICT_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=25)> _
    Public Property ZipCode As String
        Get
            CheckDeleted()
            If Row(ZipDistrictDetailDAL.COL_NAME_ZIP_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ZipDistrictDetailDAL.COL_NAME_ZIP_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ZipDistrictDetailDAL.COL_NAME_ZIP_CODE, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ZipDistrictDetailDAL
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
#End Region

#Region "DataView Retrieveing Methods"

#End Region

#Region "Shared Methods"
    Public Shared Function GetZipCodesUsedInOtherZipDistricts(zipDistrictId As Guid) As DataView
        Dim dal As New ZipDistrictDetailDAL
        Dim objCompany As New Company(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID)
        'Dim objCompanyCountry As New CompanyCountry(objCompany.CountryId)
        'Dim objCountry As New Country(objCompany.CountryId)
        Dim dv As DataView = dal.LoadNegativeList(objCompany.CountryId, zipDistrictId).Tables(dal.TABLE_NAME).DefaultView
        dv.Sort = dal.COL_NAME_ZIP_CODE
        Return dv
    End Function
#End Region

End Class



