'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (8/19/2004)  ********************
'Imports Assurant.Common.Framework.BusinessObject

Public Class Region
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
        Dataset = New DataSet
        Load()
        Dim company As New ElitaPlus.BusinessObjectsNew.Company(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
        SetValue(RegionDAL.COL_NAME_COUNTRY_ID, company.BusinessCountryId)
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
        Dim dal As New RegionDAL
        If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
            dal.LoadSchema(Dataset)
        End If
        Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
        Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
        Row = newRow
        setvalue(dal.TABLE_KEY_NAME, Guid.NewGuid)
    End Sub

    Protected Sub Load(id As Guid)
        Row = Nothing
        Dim dal As New RegionDAL
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
    End Sub
#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid
        Get
            If row(RegionDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(RegionDAL.COL_NAME_REGION_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=30), RegionDescriptionValidator("")> _
    Public Property Description As String
        Get
            CheckDeleted()
            If Row(RegionDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RegionDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            If Value IsNot Nothing Then
                SetValue(RegionDAL.COL_NAME_DESCRIPTION, Value.Trim())
            Else
                SetValue(RegionDAL.COL_NAME_DESCRIPTION, Value)
            End If
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property CountryId As Guid
        Get
            CheckDeleted()
            If Row(RegionDAL.COL_NAME_COUNTRY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(RegionDAL.COL_NAME_COUNTRY_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(RegionDAL.COL_NAME_COUNTRY_ID, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=5), RegionCodeValidator("")> _
    Public Property ShortDesc As String
        Get
            CheckDeleted()
            If Row(RegionDAL.COL_NAME_SHORT_DESC) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RegionDAL.COL_NAME_SHORT_DESC), String)
            End If
        End Get
        Set
            CheckDeleted()
            If Value IsNot Nothing Then
                SetValue(RegionDAL.COL_NAME_SHORT_DESC, Value.Trim())
            Else
                SetValue(RegionDAL.COL_NAME_SHORT_DESC, Value)
            End If

        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=5)> _
       Public Property AccountingCode As String
        Get
            CheckDeleted()
            If Row(RegionDAL.COL_NAME_ACCOUNTING_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RegionDAL.COL_NAME_ACCOUNTING_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            If Value IsNot Nothing Then
                SetValue(RegionDAL.COL_NAME_ACCOUNTING_CODE, Value.Trim())
            Else
                SetValue(RegionDAL.COL_NAME_ACCOUNTING_CODE, Value)
            End If

        End Set
    End Property

    <ValidStringLength("", Max:=15)>
    Public Property InvoiceTaxGLAcct As String
        Get
            CheckDeleted()
            If Row(RegionDAL.COL_NAME_INVOICE_TAX_GL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RegionDAL.COL_NAME_INVOICE_TAX_GL), String)
            End If
        End Get
        Set
            CheckDeleted()
            If Value IsNot Nothing Then
                SetValue(RegionDAL.COL_NAME_INVOICE_TAX_GL, Value.Trim())
            Else
                SetValue(RegionDAL.COL_NAME_INVOICE_TAX_GL, Value)
            End If

        End Set
    End Property

    <ValidStringLength("", Max:=15)>
    Public Property ExtendedCode As String
        Get
            CheckDeleted()
            If Row(RegionDAL.COL_NAME_EXTENDED_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RegionDAL.COL_NAME_EXTENDED_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            If Value IsNot Nothing Then
                SetValue(RegionDAL.COL_NAME_EXTENDED_CODE, Value.Trim())
            Else
                SetValue(RegionDAL.COL_NAME_EXTENDED_CODE, Value)
            End If

        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        MyBase.Save()
        Dim dal As New RegionDAL
        dal.Update(Dataset)
        'Reload the Data
        If _isDSCreator AndAlso Row.RowState <> DataRowState.Detached Then
            'Reload the Data from the DB
            Load(Id)
        End If
    End Sub
#End Region

#Region "DataView Retrieveing Methods"

    Public Shared Function LoadList(descriptionMask As String, _
                                    codeMask As String, _
                                    countryID As Guid) As DataView
        Try
            Dim dal As New RegionDAL
            Dim ds As Dataset
            ds = dal.LoadList(descriptionMask, codeMask, countryID)
            Return (ds.Tables(RegionDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function LoadList(countryID As Guid) As Dataset
        Try
            Dim dal As New RegionDAL
            Return dal.LoadList(countryID)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetRegionsAndComunas(countryID As Guid, Optional regionCode As String = "") As DataView
        Try
            Dim dal As New RegionDAL
            Dim ds As DataSet
            ds = dal.GetRegionsAndComunas(countryID, regionCode)

            Return (ds.Tables(RegionDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function LoadList(descriptionMask As String, _
                                codeMask As String, _
                                userCompanies As ArrayList) As DataView
        Try
            Dim dal As New RegionDAL
            Dim ds As Dataset

            If userCompanies.Count > 0 Then
                ds = dal.LoadList(descriptionMask, codeMask, Country.GetCountries(userCompanies))
            Else
                Dim err As New ValidationError(Common.ErrorCodes.INVALID_COMPANYID_REQUIRED, GetType(Region), Nothing, "CompanyID", Nothing)
                Throw New BOValidationException(New ValidationError() {err}, "Company ID")
            End If

            Return (ds.Tables(RegionDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetNewDataViewRow(dv As DataView, id As Guid) As DataView

        Dim dt As DataTable
        dt = dv.Table

        Dim row As DataRow = dt.NewRow

        row(RegionDAL.COL_NAME_DESCRIPTION) = String.Empty
        row(RegionDAL.COL_NAME_REGION_ID) = id.ToByteArray

        dt.Rows.Add(row)

        Return (dv)

    End Function

    Public Shared Function LoadListForWS(oCountriesIds As ArrayList) As DataSet
        Try
            Dim dal As New RegionDAL
            Return dal.LoadListForWS(oCountriesIds)


        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetRegionsByUserCountries(CountryId As Guid) As DataView
        Try
            Dim dal As New RegionDAL
            Dim ds As DataSet

            ds = dal.LoadRegionsByUserCountries(CountryId)
            Return (ds.Tables(RegionDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function


    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class RegionCodeValidator
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.REGION_CODE_ALREADY_EXIST)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As Region = CType(objectToValidate, Region)
            Dim dal As New RegionDAL

            If (obj.ShortDesc IsNot Nothing) AndAlso (obj.ShortDesc.Trim <> String.Empty) Then

                If Not dal.IsRegionCodeUnique(obj.CountryId, obj.ShortDesc.Trim, obj.Id) Then
                    Return False
                End If
            End If
            Return True

        End Function

    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class RegionDescriptionValidator
        Inherits ValidBaseAttribute

        Public Sub New(fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Common.ErrorCodes.REGION_DESCRIPTION_ALREADY_EXIST)
        End Sub

        Public Overrides Function IsValid(valueToCheck As Object, objectToValidate As Object) As Boolean
            Dim obj As Region = CType(objectToValidate, Region)
            Dim dal As New RegionDAL

            If (obj.Description IsNot Nothing) AndAlso (obj.Description.Trim <> String.Empty) Then

                If Not dal.IsRegionDescriptionUnique(obj.CountryId, obj.Description.Trim, obj.Id) Then
                    Return False
                End If
            End If
            Return True

        End Function

    End Class



#Region "SearchDV"
    Public Class RegionSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_REGION_ID As String = RegionDAL.COL_NAME_REGION_ID
        Public Const COL_CODE As String = RegionDAL.COL_NAME_SHORT_DESC
        Public Const COL_DESCRIPTION As String = RegionDAL.COL_NAME_DESCRIPTION
        Public Const COL_COUNTRY_ID As String = RegionDAL.COL_NAME_COUNTRY_ID
        Public Const COL_COUNTRY_NAME As String = RegionDAL.COL_NAME_COUNTRY_NAME
        Public Const COL_ACCOUNTING_CODE As String = RegionDAL.COL_NAME_ACCOUNTING_CODE
        Public Const COL_INVOICE_TAX_GL As String = RegionDAL.COL_NAME_INVOICE_TAX_GL
        Public Const COL_EXTENDED_CODE As String = RegionDAL.COL_NAME_EXTENDED_CODE

#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

#End Region

#End Region


End Class


